Imports SpectrumBL
Imports System.Data
Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid
Imports SpectrumCommon

Public Class frmNLoyalityCustomer
    Dim objCustm As New clsCLPCustomer
    Dim objComn As New clsCommon
    Public vCustomerNo As String = ""
    Dim IsFormLoaded As Boolean = False
    Dim dsCombo As DataSet = New DataSet
    Dim dsCombo1 As DataSet = New DataSet
    Dim CustomerType As String = "CLP"
    Public pSearchCust As String = ""
    Dim dvCity, dvCity1, dvState, dvState1 As DataView
    Dim objHlthCustomer As New clsHealthCare
    Dim dsCLPprog As DataSet
    Dim drCustmInfo As DataRow
    Dim drCustmAdds As DataRow
    Dim findKeyInfo(1) As Object
    Dim findKeyAdds(3) As Object
    Dim _dsMain As New DataSet
    Dim OldGstNo As String
    Dim _dtCustmInfo As DataTable
    Public NewClpCustomer As Boolean = False
    Public Property dtCustmInfo() As DataTable
        Get
            Return _dtCustmInfo
        End Get
        Set(ByVal value As DataTable)
            _dtCustmInfo = value
        End Set
    End Property

    Public _CustomerNoToSearch As String = String.Empty
    Dim vSiteCode As String = clsAdmin.SiteCode
    Dim _Customerno As String
    Public ReadOnly Property Customerno() As String
        Get
            Return _Customerno
        End Get
    End Property
    Public Property dsMain() As DataSet
        Get
            Return _dsMain
        End Get
        Set(ByVal value As DataSet)
            _dsMain = value
        End Set
    End Property
    Dim _dsCustmInfo As DataSet
    Public Property dsCustmInfo() As DataSet
        Get
            Return _dsCustmInfo
        End Get
        Set(ByVal value As DataSet)
            _dsCustmInfo = value
        End Set
    End Property

    Private _CustomerResponse As GetCustomerResponse
    Public Property CustomerResponse As GetCustomerResponse
        Get
            Return _CustomerResponse
        End Get
        Set(value As GetCustomerResponse)
            _CustomerResponse = value
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

    Private _CustomerMaster As GetCustomerMasterResponse
    Public Property CustomerMaster As GetCustomerMasterResponse
        Get
            Return _CustomerMaster
        End Get
        Set(value As GetCustomerMasterResponse)
            _CustomerMaster = value
        End Set
    End Property

    Private _CustomerMaster1 As GetCustomerMasterResponse
    Public Property CustomerMaster1 As GetCustomerMasterResponse
        Get
            Return _CustomerMaster1
        End Get
        Set(value As GetCustomerMasterResponse)
            _CustomerMaster1 = value
        End Set
    End Property


    Private _CustomerBL As ICreateCustomer
    Public Property CustomerBL As ICreateCustomer
        Get
            If _CustomerBL Is Nothing Then
                _CustomerBL = New CreateCustomer()
            End If
            Return _CustomerBL
        End Get
        Set(value As ICreateCustomer)
            _CustomerBL = value
        End Set
    End Property
    'Dim _FormName As String
    'Public Property FormName() As String
    '    Get
    '        Return _FormName
    '    End Get
    '    Set(ByVal value As String)
    '        _FormName = value
    '    End Set
    'End Property
    ''' <summary>
    ''' Create New Customer for CLP Customers
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub frmNLoyalityCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            _dsMain = objCustm.GetCustomerDataSet(clsAdmin.SiteCode, "0")
            dsCombo = objCustm.GetComboDataSet
            dsCombo1 = dsCombo.Copy

            'PopulateComboBox(dsCombo.Tables("CountryTab"), cboCountry)
            'pC1ComboSetDisplayMember(cboCountry)

            'dvState = New DataView(dsCombo.Tables("StateTab"), "", "", DataViewRowState.CurrentRows)
            'cboState.DataSource = dvState
            'cboState.DisplayMember = "Description"
            'cboState.ValueMember = "AreaCode"
            'pC1ComboSetDisplayMember(cboState)


            'dvCity = New DataView(dsCombo.Tables("CityTab"), "", "", DataViewRowState.CurrentRows)
            'cboCity.DataSource = dvCity
            'cboCity.DisplayMember = "Description"
            'cboCity.ValueMember = "AreaCode"
            'pC1ComboSetDisplayMember(cboCity)

            'PopulateComboBox(dsCombo1.Tables("CountryTab"), cboCountry1)
            'pC1ComboSetDisplayMember(cboCountry1)

            'dvState1 = New DataView(dsCombo1.Tables("StateTab"), "", "", DataViewRowState.CurrentRows)
            'cboState1.DataSource = dvState1
            'cboState1.DisplayMember = "Description"
            'cboState1.ValueMember = "AreaCode"
            'pC1ComboSetDisplayMember(cboState1)

            'dvCity1 = New DataView(dsCombo1.Tables("CityTab"), "", "", DataViewRowState.CurrentRows)
            'cboCity1.DataSource = dvCity1
            'cboCity1.DisplayMember = "Description"
            'cboCity1.ValueMember = "AreaCode"
            'pC1ComboSetDisplayMember(cboCity1)
            CustomerMaster = CustomerBL.GetCustomerMaster()
            CustomerMaster1 = CustomerBL.GetCustomerMaster()
            InitializeComboBox() '-----For Country , State , City 

            PopulateComboBox(dsCombo.Tables("TitleTab"), cboTitle)
            pC1ComboSetDisplayMember(cboTitle)

            PopulateComboBox(dsCombo1.Tables("TitleTab"), cboSpouseTitle)
            pC1ComboSetDisplayMember(cboSpouseTitle)

            PopulateComboBox(dsCombo.Tables("GenderTab"), cboGender)
            pC1ComboSetDisplayMember(cboGender)

            PopulateComboBox(dsCombo.Tables("MaritalTab"), cboMaritalStatus)
            pC1ComboSetDisplayMember(cboMaritalStatus)

            PopulateComboBox(dsCombo.Tables("EducationTab"), cboEducation)
            pC1ComboSetDisplayMember(cboEducation)

            PopulateComboBox(dsCombo.Tables("OccupationTab"), cboOccupation)
            pC1ComboSetDisplayMember(cboOccupation)

            PopulateComboBox(dsCombo1.Tables("OccupationTab"), cboSpouseOccupation)
            pC1ComboSetDisplayMember(cboSpouseOccupation)

            If clsDefaultConfiguration.CustomerClassSelection Then
                Dim dtHCustclassData As New DataTable
                dtHCustclassData = objHlthCustomer.GetCustomerClassData()
                PopulateComboBox(dtHCustclassData, cboCustomerClass)
                pC1ComboSetDisplayMember(cboCustomerClass)
            End If
            '------------------------
            CLP_Data.Sitecode = clsAdmin.SiteCode
            dsCLPprog = CLP_Data.getclpdata()

            If dsCLPprog IsNot Nothing AndAlso dsCLPprog.Tables("CLPHeader") IsNot Nothing AndAlso dsCLPprog.Tables("CLPHeader").Rows.Count > 0 Then
                Dim tierslist = CLP_Data.GetCLPTiers(dsCLPprog.Tables("CLPHeader")(0)("CLPPROGRAMID"))
                If tierslist.Count > 0 Then
                    txtTierType.Text = tierslist(0)
                End If
            End If
            '---------------------
            
            SetCulture(Me, Me.Name)
            SetEnableDisableFields(True)
            If Not clsDefaultConfiguration.CustomerClassSelection Then
                lblCustomerClass.Visible = False
                cboCustomerClass.Visible = False
            End If
            '------ Apply Theme here 
            If clsDefaultConfiguration.ThemeSelect <> "Default" Then
                Select Case clsDefaultConfiguration.ThemeSelect
                    Case "Theme 1"
                        ThemeChange()
                    Case 2

                    Case Else

                End Select
            End If

            Call setAddressAutoComplete()
            tbAddress.pInit()
            tbAddress.SelectedTab = tbAddress.TabPages("tpAddress")
            If UCase(pSearchCust) = "SEARCH" AndAlso Me.Tag = String.Empty Then
                Me.BtnSearchCustomer_Click(BtnSearchCustomer, e)
                If txtCustomerCode.Text = String.Empty Then
                    Me.Hide()
                End If
                Exit Sub
            ElseIf UCase(pSearchCust) = "SEARCH" AndAlso Me.Tag = "NEW" Then
                Me.BtnSearchCustomer_Click(BtnSearchCustomer, e)
                If txtCustomerCode.Text = String.Empty Then
                    Me.Close()
                End If
                Exit Sub
            ElseIf UCase(Me.Tag) = "NEW" Then
                BtnNew_Click(sender, e)
                Exit Sub
            ElseIf NewClpCustomer = True Then
                BtnNew_Click(sender, e)
                Exit Sub

            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub tbAddress_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles tbAddress.SelectedIndexChanged
        'If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
        '    For Each ctrl As Control In tbAddress.TabPages
        '        If TypeOf ctrl Is C1.Win.C1Command.C1DockingTabPage Then
        '            DirectCast(ctrl, C1.Win.C1Command.C1DockingTabPage).TabBackColor = Color.FromArgb(212, 212, 212)
        '        End If
        '    Next
        '    tbAddress.SelectedTab.TabBackColor = Color.FromArgb(0, 107, 163)
        'End If
        If clsDefaultConfiguration.ThemeSelect <> "Default" Then
            Select Case clsDefaultConfiguration.ThemeSelect
                Case "Theme 1"
                    tbAddress.TabPages(0).TabBackColor = Color.FromArgb(212, 212, 212)
                    tbAddress.TabPages(0).BackColor = Color.FromArgb(134, 134, 134)

                    tbAddress.TabPages(1).TabBackColor = Color.FromArgb(212, 212, 212)
                    tbAddress.TabPages(1).BackColor = Color.FromArgb(134, 134, 134)

                    tbAddress.TabPages(2).TabBackColor = Color.FromArgb(212, 212, 212)
                    tbAddress.TabPages(2).BackColor = Color.FromArgb(134, 134, 134)

                    tbAddress.TabPages(3).TabBackColor = Color.FromArgb(212, 212, 212)
                    tbAddress.TabPages(3).BackColor = Color.FromArgb(134, 134, 134)

                    tbAddress.SelectedTab.TabBackColor = Color.FromArgb(0, 107, 163)

                Case 2

                Case Else

            End Select
        End If
        
    End Sub

    Public Sub ThemeChange()
        Try
            sizTop.BackColor = Color.FromArgb(134, 134, 134)

            Dim Lblbackcolor As Color = Color.FromArgb(134, 134, 134)
            Dim lblFont As New Font("Neo Sans", 10, FontStyle.Regular)
            Dim btnBackColor = Color.FromArgb(0, 181, 120)
            Dim btnForeColor = Color.FromArgb(0, 107, 163)
            '--- Labels 
            lblGender.BackColor = Lblbackcolor
            lblGender.Font = lblFont

            '---- Tab Pages ...  
            tbAddress.TabPages(0).TabBackColor = Color.FromArgb(212, 212, 212)
            tbAddress.TabPages(0).BackColor = Color.FromArgb(134, 134, 134)

            tbAddress.TabPages(1).TabBackColor = Color.FromArgb(212, 212, 212)
            tbAddress.TabPages(1).BackColor = Color.FromArgb(134, 134, 134)

            tbAddress.TabPages(2).TabBackColor = Color.FromArgb(212, 212, 212)
            tbAddress.TabPages(2).BackColor = Color.FromArgb(134, 134, 134)

            tbAddress.TabPages(3).TabBackColor = Color.FromArgb(212, 212, 212)
            tbAddress.TabPages(3).BackColor = Color.FromArgb(134, 134, 134)

            tbAddress.SelectedTab.TabBackColor = Color.FromArgb(0, 107, 163)

            C1Sizer1.BackColor = Color.White

            tbAddress.TabPages(0).TabBackColorSelected = Color.FromArgb(0, 81, 120)
            tbAddress.TabPages(1).TabBackColorSelected = Color.FromArgb(0, 81, 120)
            tbAddress.TabPages(2).TabBackColorSelected = Color.FromArgb(0, 81, 120)
            tbAddress.TabPages(3).TabBackColorSelected = Color.FromArgb(0, 81, 120)

            tbAddress.TabPages(0).TabForeColorSelected = Color.White
            tbAddress.TabPages(1).TabForeColorSelected = Color.White
            tbAddress.TabPages(2).TabForeColorSelected = Color.White
            tbAddress.TabPages(3).TabForeColorSelected = Color.White

            '--- Set Button .

            'With BtnOk
            '    '.FlatStyle = FlatStyle.System
            '    '.FlatAppearance.BorderSize = 0
            '    '.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
            '    '.BackColor = Color.Red
            '    .Location = New Point(C1Sizer1.Left + C1Sizer1.Width - BtnOk.Width, .Height)
            'End With

            'With BtnExit
            '    '.FlatStyle = FlatStyle.System
            '    '.FlatAppearance.BorderSize = 0
            '    '.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
            '    '.BackColor = Color.Red
            '    .Location = New Point(C1Sizer1.Left + C1Sizer1.Width + BtnOk.Width + 10 - BtnExit.Width, .Height)
            'End With
            'btndelete=&Delete
            'btnedit=&Edit
            'btneditsave=&Save
            'btnexit=&Cancel 
            'btnnew=&New
            'btnnewcancel=&Cancel
            'BtnOk = Ok

            With BtnOk
                .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                .BackColor = Color.Transparent
                .BackColor = Color.FromArgb(0, 107, 163)
                .ForeColor = Color.FromArgb(255, 255, 255)
                .Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                .TextAlign = ContentAlignment.MiddleCenter
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
                .FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
            End With
            With BtnExit
                .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                .BackColor = Color.Transparent
                .BackColor = Color.FromArgb(0, 107, 163)
                .ForeColor = Color.FromArgb(255, 255, 255)
                .TextAlign = ContentAlignment.MiddleCenter
                .Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
                .FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
            End With

            With BtnDelete
                .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                .BackColor = Color.Transparent
                .BackColor = Color.FromArgb(0, 107, 163)
                .ForeColor = Color.FromArgb(255, 255, 255)
                .TextAlign = ContentAlignment.MiddleCenter
                .Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
                .FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
            End With

            With BtnEdit
                .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                .BackColor = Color.Transparent
                .BackColor = Color.FromArgb(0, 107, 163)
                .TextAlign = ContentAlignment.MiddleCenter
                .ForeColor = Color.FromArgb(255, 255, 255)
                .Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
                .FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
            End With

            With BtnNew
                .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                .BackColor = Color.Transparent
                .BackColor = Color.FromArgb(0, 107, 163)
                .ForeColor = Color.FromArgb(255, 255, 255)
                .TextAlign = ContentAlignment.MiddleCenter
                .Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
                .FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
            End With

            With ChkBoxByEmail
                .Font = lblFont
                .BackColor = Color.FromArgb(212, 212, 212)
                .TextAlign = ContentAlignment.MiddleLeft
            End With
            With ChkBoxBySMS
                .Font = lblFont
                .BackColor = Color.FromArgb(212, 212, 212)
                .TextAlign = ContentAlignment.MiddleLeft
            End With
            With grpOffAdd
                .Font = lblFont
                .BackColor = Color.FromArgb(134, 134, 134)
            End With
            With GrpResiAdd
                .Font = lblFont
                .BackColor = Color.FromArgb(134, 134, 134)
            End With

            With lblGender
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(cboGender.Left - .Left, cboGender.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblTitle
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(cboTitle.Left - .Left, cboTitle.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblFirstName
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtFirstName.Left - .Left, txtFirstName.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblMiddleName
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtMiddleName.Left - .Left, txtMiddleName.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With lblLastName
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtLastName.Left - .Left, txtLastName.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblResAddress
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                '.Size = New Size(txta.Left - .Left, txtAddress1.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblCity1
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(cboCity1.Left - .Left, cboCity1.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblCity
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(cboCity.Left - .Left, cboCity.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblCountry1
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(cboCountry1.Left - .Left, cboCountry1.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblCountry
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(cboCountry.Left - .Left, cboCountry.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblCustomerCode
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtCustomerCode.Left - .Left, txtCustomerCode.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblDOB
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(dtpDOB.Left - .Left, dtpDOB.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblEducation
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(cboEducation.Left - .Left, cboEducation.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblEmailId
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtEmail.Left - .Left, txtEmail.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With lblMaritalStatus
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(cboMaritalStatus.Left - .Left, cboMaritalStatus.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblMarriageDate
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(dtpMarriageDate.Left - .Left, dtpMarriageDate.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With lblMobileNo
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtMobile.Left - .Left, txtMobile.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With


            '------------
            With lblGST
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(dtpMarriageDate.Left - .Left, dtpMarriageDate.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            '------------
            With lblOcupation
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(cboOccupation.Left - .Left, cboOccupation.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblOfficePhone
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtOffPhone.Left - .Left, txtOffPhone.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblPincode1
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtPincode1.Left - .Left, txtPincode1.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblPincode
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtPincode.Left - .Left, txtPincode.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblResidence
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .BackColor = Color.FromArgb(212, 212, 212)
                .Size = New Size(txtResPhone.Left - .Left, txtResPhone.Height)
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblState1
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(cboState1.Left - .Left, cboState1.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblState
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(cboState.Left - .Left, cboState.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With lblSpouseTitle
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(cboSpouseTitle.Left - .Left, cboSpouseTitle.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblSpouseFirstName
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtSpouseFirstName.Left - .Left, txtSpouseFirstName.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblSpouseMiddleName
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtSpouseMiddleName.Left - .Left, txtSpouseMiddleName.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblSpouseLastName
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtSpouseLastName.Left - .Left, txtSpouseLastName.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblSpouseDOB
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(dtpSpouseDOB.Left - .Left, dtpSpouseDOB.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblSpouseOccupation
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(cboSpouseOccupation.Left - .Left, cboSpouseOccupation.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblFacebookID
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtFacebookID.Left - .Left, txtFacebookID.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblTwitterId
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtTwitterId.Left - .Left, txtTwitterId.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblLinkedinID
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtLinkedInID.Left - .Left, txtLinkedInID.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblGooglePulsId
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtGooglePulsId.Left - .Left, txtGooglePulsId.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblHi5Id
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtHi5ID.Left - .Left, txtHi5ID.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblMySpaceId
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtMySpaceId.Left - .Left, txtMySpaceId.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblIbiboId
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtIbiboId.Left - .Left, txtIbiboId.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblFourSqureId
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtFourSqureId.Left - .Left, txtFourSqureId.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblOrkutId
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtOrkutId.Left - .Left, txtOrkutId.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With
            With lblSkypeId
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtSkypeId.Left - .Left, txtSkypeId.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With lblResAddress1
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtAddress1.Left - .Left, txtAddress1.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With lblDelAddress1
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtAddress11.Left - .Left, txtAddress11.Height)

                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With lblResAddress2
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtAddress2.Left - .Left, txtAddress2.Height)

                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With lblDelAddress2
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtAddress12.Left - .Left, txtAddress12.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With lblResAddress3
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtAddress3.Left - .Left, txtAddress3.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With lblDelAddress3
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtAddress13.Left - .Left, txtAddress13.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With lblResAddress4
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtAddress4.Left - .Left, txtAddress4.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

            With lblDelAddress4
                .Font = lblFont
                '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
                .Size = New Size(txtAddress14.Left - .Left, txtAddress14.Height)
                .BackColor = Color.FromArgb(212, 212, 212)
                .AutoSize = False
                .TextAlign = ContentAlignment.MiddleLeft
                .BorderStyle = BorderStyle.None
            End With

        Catch ex As Exception

        End Try
    End Sub

    Sub setThemeLabel(ByRef lbl As CtrlLabel, ByRef ctrl As Control, ByRef lblFont As Font)
        With lbl
            .Font = lblFont
            '  .Font = New Font("Neo Sans", 13, FontStyle.Bold)
            .Size = New Size(txtAddress1.Left - .Left, txtAddress1.Height)
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With
    End Sub


    Private Sub InitializeComboBox()

        cboCountry.DisplayMember = "AreaName"
        cboCountry.ValueMember = "AreaCode"
        cboCountry.DataSource = CustomerMaster.AreaInfoList.Where(Function(x) x.AreaType = AreaType.Country).ToList()
        cboCountry.SelectedValue = ""

        cboCountry1.DisplayMember = "AreaName"
        cboCountry1.ValueMember = "AreaCode"
        cboCountry1.DataSource = CustomerMaster1.AreaInfoList.Where(Function(x) x.AreaType = AreaType.Country).ToList()
        cboCountry1.SelectedValue = ""

    End Sub

    Private Sub cboCountry_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCountry.SelectedValueChanged
        Try
            If Not String.IsNullOrEmpty(cboCountry.SelectedValue) Then
                cboState.DisplayMember = "AreaName"
                cboState.ValueMember = "AreaCode"
                cboState.DataSource = CustomerMaster.AreaInfoList.Where(Function(x) x.AreaType = AreaType.State AndAlso x.ParentAreaCode = cboCountry.SelectedValue).ToList()
            Else
                cboState.DataSource = Nothing
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cboCountry1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCountry1.SelectedValueChanged
        Try
            If Not String.IsNullOrEmpty(cboCountry1.SelectedValue) Then
                cboState1.DisplayMember = "AreaName"
                cboState1.ValueMember = "AreaCode"
                cboState1.DataSource = CustomerMaster1.AreaInfoList.Where(Function(x) x.AreaType = AreaType.State AndAlso x.ParentAreaCode = cboCountry1.SelectedValue).ToList()
            Else
                cboState1.DataSource = Nothing
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cboState_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboState.SelectedValueChanged
        Try
            If Not String.IsNullOrEmpty(cboState.SelectedValue) Then
                cboCity.DisplayMember = "AreaName"
                cboCity.ValueMember = "AreaCode"
                cboCity.DataSource = CustomerMaster.AreaInfoList.Where(Function(x) x.AreaType = AreaType.City AndAlso x.ParentAreaCode = cboState.SelectedValue).ToList()
            Else
                cboCity.DataSource = Nothing
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cboState1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboState1.SelectedValueChanged
        Try
            If Not String.IsNullOrEmpty(cboState1.SelectedValue) Then
                cboCity1.DisplayMember = "AreaName"
                cboCity1.ValueMember = "AreaCode"
                cboCity1.DataSource = CustomerMaster1.AreaInfoList.Where(Function(x) x.AreaType = AreaType.City AndAlso x.ParentAreaCode = cboState1.SelectedValue).ToList()
            Else
                cboCity1.DataSource = Nothing
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub



    ''' <summary>
    ''' Reset Application Filed Values
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            If BtnNew.Tag = "New" Then

                SetClearFields()
                SetEnableDisableFields(False)

                IsFormLoaded = True
                'BtnNew.Text = "&Cancel"
                BtnNew.Text = getValueByKey("frmnloyalitycustomer.btnnewcancel")
                BtnNew.Tag = "Cancel"
                'BtnEdit.Text = "&Save"
                BtnEdit.Text = getValueByKey("frmnloyalitycustomer.btneditsave")
                BtnEdit.Tag = "Save"
                BtnEdit.Enabled = True
                'BtnDelete.Enabled = False
                BtnDelete.Visible = False
                BtnOk.Enabled = False
                BtnSearchCustomer.Enabled = False
                BtnExit.Enabled = False
                txtCustomerCode.Visible = False
                lblCustomerCode.Visible = False
                BtnSearchCustomer.Visible = False
            ElseIf BtnNew.Tag = "Cancel" Then
                If MsgBox(getValueByKey("SOC001"), MsgBoxStyle.YesNo, "SOC001 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                    Me.Close()
                    Return
                    SetEnableDisableFields(True)
                    If Not (BtnEdit.Tag = "Update") Then
                        SetClearFields()
                    End If
                    IsFormLoaded = False
                    'BtnNew.Text = "&New"
                    BtnNew.Text = getValueByKey("frmnloyalitycustomer.btnnew")
                    BtnNew.Tag = "New"
                    'BtnEdit.Tag = "Edit"
                    BtnEdit.Tag = "Edit"
                    BtnEdit.Text = getValueByKey("frmnloyalitycustomer.btnedit")
                    BtnEdit.Enabled = True
                    'BtnDelete.Enabled = True
                    BtnDelete.Visible = False
                    BtnOk.Enabled = True
                    BtnSearchCustomer.Enabled = True
                    BtnExit.Enabled = True
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Save/Update Customer Information
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 

    Public Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            If BtnEdit.Tag = "Save" Then

                If dsCLPprog IsNot Nothing AndAlso dsCLPprog.Tables("CLPHeader") Is Nothing Then
                    ShowMessage(False, getValueByKey("frmnsearchcustomer.noclpprogram"), getValueByKey("CLAE04"))
                    Return
                End If
                If ValidatedLoyaltyCustomer() Then
                    '----- Chanded by Mahesh -- Change mobile no parameter to Card No.
                    CustomerResponse = CustomerBL.GetCustomerInfo(clsAdmin.CLPProgram, txtCustomerCode.Text, KeyIsMobileNo:=False)
                    If CustomerResponse.CLPCustomer IsNot Nothing Then
                        txtCustomerCode.Text = CustomerResponse.CLPCustomer.CardNumber
                    End If

                    If Not String.IsNullOrEmpty(txtCustomerCode.Text) AndAlso (CustomerRequest IsNot Nothing OrElse CustomerResponse IsNot Nothing) Then
                        If CustomerRequest Is Nothing Then
                            CustomerRequest = New SaveCustomerRequest()
                            CustomerRequest.CLPCustomer = New CLPCustomerDTO()
                            CustomerRequest.CLPCustomer.SiteCode = clsAdmin.SiteCode
                            CustomerRequest.CLPCustomer.CardNumber = CustomerResponse.CLPCustomer.CardNumber
                            CustomerRequest.CLPCustomer.ClpProgId = CustomerResponse.CLPCustomer.ClpProgId

                            'CustomerRequest.CLPCustomer.AddressList = CustomerResponse.CLPCustomer.AddressList
                            CustomerRequest.CreatedAt = clsAdmin.SiteCode
                            CustomerRequest.CreatedBy = clsAdmin.UserCode
                        End If
                        CustomerRequest.CLPCustomer.FirstName = txtFirstName.Text
                        CustomerRequest.CLPCustomer.MiddleName = txtMiddleName.Text
                        CustomerRequest.CLPCustomer.LastName = txtLastName.Text
                        CustomerRequest.CLPCustomer.Title = cboTitle.SelectedValue

                        CustomerRequest.CLPCustomer.SpouseFirstName = txtSpouseFirstName.Text
                        CustomerRequest.CLPCustomer.SpouseMiddleName = txtSpouseMiddleName.Text
                        CustomerRequest.CLPCustomer.SpouseLastName = txtSpouseLastName.Text
                        CustomerRequest.CLPCustomer.SpouseTitle = cboSpouseTitle.SelectedValue

                        '----  Social Sites Ids START ...

                        CustomerRequest.CLPCustomer.FacebookId = txtFacebookID.Text
                        CustomerRequest.CLPCustomer.TwitterId = txtTwitterId.Text
                        CustomerRequest.CLPCustomer.LinkedInID = txtLinkedInID.Text
                        CustomerRequest.CLPCustomer.GooglePlusId = txtGooglePulsId.Text
                        CustomerRequest.CLPCustomer.Hi5Id = txtHi5ID.Text
                        CustomerRequest.CLPCustomer.MySpaceId = txtMySpaceId.Text
                        CustomerRequest.CLPCustomer.IbiboId = txtIbiboId.Text
                        CustomerRequest.CLPCustomer.FourSquareId = txtFourSqureId.Text
                        'code commented by irfan
                        CustomerRequest.CLPCustomer.OrkutId = txtOrkutId.Text
                        CustomerRequest.CLPCustomer.SkypeId = txtSkypeId.Text

                        '----  Social Sites Ids END ...

                        Dim SendSMSRequired As Boolean = False
                        If (dsCLPprog IsNot Nothing AndAlso dsCLPprog.Tables("CLPHeader").Rows.Count > 0 AndAlso Not dsCLPprog.Tables("CLPHeader").Rows(0)("IsJoiningRewardOnEnrollment")) Then
                            If (CustomerResponse.CLPCustomer.RegistrationStatus.Equals("Enrolled")) AndAlso CtrlLblStatusValue.Text.Equals("Registered") Then
                                CustomerRequest.CLPCustomer.BalancePoints = Val(dsCLPprog.Tables("CLPHeader").Rows(0)("ValueJoiningReward").ToString())
                                CustomerRequest.CLPCustomer.PointsAccumlated = Val(dsCLPprog.Tables("CLPHeader").Rows(0)("ValueJoiningReward").ToString())
                                CustomerRequest.CLPCustomer.IsJoiningPointAccumlated = True
                                SendSMSRequired = True
                            End If
                        End If

                        If IsDBNull(dtpDOB.Value) = False AndAlso dtpDOB.Value IsNot Nothing Then CustomerRequest.CLPCustomer.BirthDate = dtpDOB.Value
                        If IsDBNull(dtpSpouseDOB.Value) = False AndAlso dtpSpouseDOB.Value IsNot Nothing Then CustomerRequest.CLPCustomer.SpouseBirthDate = dtpSpouseDOB.Value
                        If IsDBNull(dtpMarriageDate.Value) = False AndAlso dtpMarriageDate.Value IsNot Nothing Then CustomerRequest.CLPCustomer.MarriageAnivDate = dtpMarriageDate.Value

                        'CustomerRequest.CLPCustomer.CustomerGroup = cboCustomerGroup.Text
                        CustomerRequest.CLPCustomer.Gender = cboGender.SelectedValue
                        CustomerRequest.CLPCustomer.CardType = txtTierType.Text 'cmbTiers.Text
                        CustomerRequest.CLPCustomer.MobileNo = txtMobile.Text.Trim
                        CustomerRequest.CLPCustomer.OfficeNumber = txtOffPhone.Text
                        CustomerRequest.CLPCustomer.ResidenceNumber = txtResPhone.Text

                        If (Not String.IsNullOrEmpty(CustomerResponse.CLPCustomer.EmailId)) Then
                            CustomerRequest.CLPCustomer.EmailId = CustomerResponse.CLPCustomer.EmailId
                            CustomerRequest.CLPCustomer.RegistrationStatus = CustomerResponse.CLPCustomer.RegistrationStatus
                        Else
                            CustomerRequest.CLPCustomer.EmailId = txtEmail.Text
                            CustomerRequest.CLPCustomer.RegistrationStatus = CtrlLblStatusValue.Text
                        End If

                        CustomerRequest.CLPCustomer.Education = cboEducation.SelectedValue
                        CustomerRequest.CLPCustomer.Occupation = cboOccupation.SelectedValue
                        CustomerRequest.CLPCustomer.SpouseOccupation = cboSpouseOccupation.SelectedValue
                        CustomerRequest.CLPCustomer.GSTNo = txtGST.Text
                        CustomerRequest.CLPCustomer.MaritalStatus = cboMaritalStatus.SelectedValue

                        If ChkBoxByEmail.Checked Then CustomerRequest.CLPCustomer.PromotionInfobyEmail = 1
                        If ChkBoxBySMS.Checked Then CustomerRequest.CLPCustomer.PromotionInfobySMS = 1

                        CustomerRequest.CLPCustomer.AddressList.Clear()
                        'If String.IsNullOrEmpty(txtAddress1.Text.Trim()) AndAlso String.IsNullOrEmpty(txtAddress2.Text.Trim()) AndAlso String.IsNullOrEmpty(txtAddress3.Text.Trim()) AndAlso String.IsNullOrEmpty(txtAddress4.Text.Trim()) AndAlso cboCity.SelectedItem Is Nothing AndAlso cboState.SelectedItem Is Nothing AndAlso cboCountry.SelectedItem Is Nothing AndAlso String.IsNullOrEmpty(txtPincode.Text.Trim()) Then
                        'Else
                        CustomerRequest.CLPCustomer.AddressList.Add(New CLPCustomerAddressDTO _
                                                     With {.AddressType = SpectrumCommon.AddressType.Residential, _
                                                             .AddLine1 = txtAddress1.Text, _
                                                             .AddLine2 = txtAddress2.Text, _
                                                             .AddLine3 = txtAddress3.Text, _
                                                             .AddLine4 = txtAddress4.Text, _
                                                             .City = If(cboCity.SelectedItem Is Nothing, "", DirectCast(cboCity.SelectedItem, SpectrumCommon.AreaInfo).AreaCode),
                                                             .State = If(cboState.SelectedItem Is Nothing, "", DirectCast(cboState.SelectedItem, SpectrumCommon.AreaInfo).AreaCode), _
                                                             .Country = If(cboCountry.SelectedItem Is Nothing, "", DirectCast(cboCountry.SelectedItem, SpectrumCommon.AreaInfo).AreaCode), _
                                                             .PinCode = txtPincode.Text})
                        'End If

                        If String.IsNullOrEmpty(txtAddress11.Text.Trim()) AndAlso String.IsNullOrEmpty(txtAddress12.Text.Trim()) AndAlso String.IsNullOrEmpty(txtAddress13.Text.Trim()) AndAlso String.IsNullOrEmpty(txtAddress14.Text.Trim()) AndAlso cboCity1.SelectedItem Is Nothing AndAlso cboState1.SelectedItem Is Nothing AndAlso cboCountry1.SelectedItem Is Nothing AndAlso String.IsNullOrEmpty(txtPincode1.Text.Trim()) Then
                        Else
                            CustomerRequest.CLPCustomer.AddressList.Add(New CLPCustomerAddressDTO _
                                                              With {.AddressType = SpectrumCommon.AddressType.Office, _
                                                                      .AddLine1 = txtAddress11.Text, _
                                                                      .AddLine2 = txtAddress12.Text, _
                                                                      .AddLine3 = txtAddress13.Text, _
                                                                      .AddLine4 = txtAddress14.Text, _
                                                                      .City = If(cboCity1.SelectedItem Is Nothing, "", DirectCast(cboCity1.SelectedItem, SpectrumCommon.AreaInfo).AreaCode), _
                                                                      .State = If(cboState1.SelectedItem Is Nothing, "", DirectCast(cboState1.SelectedItem, SpectrumCommon.AreaInfo).AreaCode), _
                                                                      .Country = If(cboCountry1.SelectedItem Is Nothing, "", DirectCast(cboCountry1.SelectedItem, SpectrumCommon.AreaInfo).AreaCode), _
                                                                      .PinCode = txtPincode1.Text})
                        End If

                        CustomerBL.SaveCustomerInfo(CustomerRequest)
                        If clsDefaultConfiguration.CustomerClassSelection Then
                            objHlthCustomer.UpdateCustomerClassData(CustomerRequest.CLPCustomer.CardNumber, clsAdmin.CLPProgram, clsAdmin.SiteCode, clsAdmin.UserCode, cboCustomerClass.Text)
                        End If
                        If SendSMSRequired Then
                            CustomerResponse.CLPCustomer.MessageType = MessageType.CLPRegistration
                            SendSMS2Customer(CustomerRequest.CLPCustomer, MessageType.CLPRegistration)
                        End If
                        txtCustomerCode.Text = vCustomerNo
                        'code commented by irfan
                        'MessageBox.Show(String.Format(getValueByKey("SOC018"), vCustomerNo), "SOC018 - " & getValueByKey("CLAE04"))
                        'Code is Added by irfan For Mantis Issue 2811
                        MessageBox.Show(String.Format("Customer No. " & vCustomerNo & " has been Generated successfully"), "SOC018 - " & getValueByKey("CLAE04"))
                        _Customerno = txtCustomerCode.Text
                        SetEnableDisableFields(True)
                        BtnNew.Text = getValueByKey("frmnloyalitycustomer.btnnew")
                        BtnNew.Tag = "New"
                        BtnEdit.Text = getValueByKey("frmnloyalitycustomer.btnedit")
                        BtnEdit.Tag = "Edit"
                        BtnSearchCustomer.Enabled = True
                        BtnDelete.Visible = False
                        BtnOk.Enabled = True
                        txtCustomerCode.Visible = True
                        lblCustomerCode.Visible = True
                        BtnSearchCustomer.Visible = True

                    Else
                        txtMobile_KeyDown(sender, New KeyEventArgs(Keys.Enter))
                        SetClearFields()
                    End If
                End If
            Else
                If Not (txtCustomerCode.Text = "") Then
                    SetEnableDisableFields(False)
                    txtCustomerCode.ReadOnly = True
                    'BtnNew.Text = "&Cancel"
                    BtnNew.Text = getValueByKey("frmnsocustomer.btnnewcancel")
                    BtnNew.Tag = "Cancel"
                    'BtnEdit.Text = "&Update"
                    BtnEdit.Text = getValueByKey("frmnsocustomer.btneditsave")
                    BtnEdit.Tag = "Save"
                    BtnSearchCustomer.Enabled = False
                    'BtnDelete.Enabled = False
                    BtnDelete.Visible = False
                    BtnOk.Enabled = False
                    BtnExit.Enabled = False
                Else
                    MsgBox(getValueByKey("SOC003"), , "SOC003 - " & getValueByKey("CLAE04"))
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub


    Private Sub SendSMS2Customer(ByVal customer As CLPCustomerDTO, ByVal messageType As MessageType)
        Try
            'If (Not String.IsNullOrEmpty(CustomerResponse.CLPCustomer.MobileNo) AndAlso CustomerResponse.CLPCustomer.MobileNo.Length >= 10) Then
            '    Dim SendSMSToClpCustomer As New SendSMSToClpCustomer(AddressOf SendSMStoCustomer)
            '    SendSMSToClpCustomer.BeginInvoke(String.Empty, CustomerResponse.CLPCustomer.BalancePoints.ToString(), CustomerResponse.CLPCustomer.MobileNo, Nothing, Nothing)
            'End If

            Dim smsRequestDTO As New CustomerServices.clpCustomerDTO
            smsRequestDTO.siteCode = customer.SiteCode
            smsRequestDTO.firstName = customer.FirstName
            smsRequestDTO.middleName = customer.MiddleName
            smsRequestDTO.surName = customer.LastName
            smsRequestDTO.registrationStatus = customer.RegistrationStatus
            smsRequestDTO.cardNo = customer.CardNumber
            smsRequestDTO.cardType = customer.CardType
            smsRequestDTO.birthDate = customer.BirthDate
            smsRequestDTO.accountNo = customer.CardNumber
            smsRequestDTO.gender = customer.Gender
            smsRequestDTO.mobileNo = customer.MobileNo
            smsRequestDTO.emailId = customer.EmailId
            smsRequestDTO.pointsAccumlated = customer.BalancePoints
            smsRequestDTO.totalBalancePoint = customer.BalancePoints
            smsRequestDTO.processSubType = messageType.ToString()
            smsRequestDTO.passKey = customer.Passkey
            smsRequestDTO.passKeyValue = customer.PasskeyValue

            Dim sendSMS As New CustomerServices.sendSMS
            sendSMS.arg0 = smsRequestDTO

            'clsDefaultConfiguration.CustomerWebServiceUrl = "http://10.10.180.68:8080/posSeam/webservices/ClpCustomer?wsdl"

            'Specify the binding to be used for the client.
            Dim binding As New System.ServiceModel.BasicHttpBinding()

            'Specify the address to be used for the client.
            Dim address As New System.ServiceModel.EndpointAddress(clsDefaultConfiguration.CustomerWebServiceUrl)

            'Create a client that is configured with this address and binding.
            Dim client As New CustomerServices.ClpCustomerClient(binding, address)

            client.sendSMSAsync(sendSMS)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Delete Old Customer Information
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            If Not (txtCustomerCode.Text = "") Then

                If MsgBox(getValueByKey("SOC004"), MsgBoxStyle.YesNo, "SOC004 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                    findKeyInfo(0) = clsAdmin.SiteCode
                    findKeyInfo(1) = txtCustomerCode.Text.Trim

                    drCustmInfo = dsMain.Tables("CustomerSaleOrder").Rows.Find(findKeyInfo)
                    If Not (drCustmInfo Is Nothing) Then
                        drCustmInfo.AcceptChanges()
                        drCustmInfo("Status") = 0
                    End If

                    For AddressRow = 1 To 2
                        findKeyAdds(0) = clsAdmin.SiteCode
                        findKeyAdds(1) = txtCustomerCode.Text.Trim
                        findKeyAdds(2) = CustomerType
                        findKeyAdds(3) = AddressRow

                        drCustmAdds = dsMain.Tables("CustomerAddress").Rows.Find(findKeyAdds)
                        If Not (drCustmAdds Is Nothing) Then
                            drCustmAdds.AcceptChanges()
                            drCustmAdds("Status") = 0
                        End If
                    Next

                    If objCustm.SaveData(dsMain, "SOCustomer", False) = True Then
                        SetClearFields()
                        SetEnableDisableFields(True)
                    End If
                End If
            Else
                MsgBox(getValueByKey("SOC005"), , "SOC005 - " & getValueByKey("CLAE04"))
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Close 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        If Not (txtCustomerCode.Text.Trim = String.Empty) Then
            _SelectedCustmCode = txtCustomerCode.Text.Trim
            Me.Close()
        Else
            ShowMessage(getValueByKey("SOC006"), "SOC006 - " & getValueByKey("CLAE04"))
        End If

    End Sub
    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        _SelectedCustmCode = 0
        'If ((BtnEdit.Text = "Save" OrElse BtnEdit.Text = "Update") AndAlso Not txtFirstName.Text = String.Empty) Then
        If ((BtnEdit.Tag = "Save" OrElse BtnEdit.Tag = "Update") AndAlso Not txtFirstName.Text = String.Empty) Then
            If MsgBox(getValueByKey("BL090"), MsgBoxStyle.YesNo, "BL090 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub
    ''' <summary>
    ''' Clear Fields
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetClearFields() As Boolean
        Try
            'cboCustomerType.Focus()

            'cboCustomerType.SelectedIndex = 0
            txtCustomerCode.Text = ""
            cboTitle.SelectedIndex = -1
            cboSpouseTitle.SelectedIndex = -1

            txtFirstName.Text = ""
            txtMiddleName.Text = ""
            txtLastName.Text = ""
            cboGender.SelectedIndex = -1
            txtSpouseFirstName.Text = ""
            txtSpouseMiddleName.Text = ""
            txtSpouseLastName.Text = ""
            'Dim tmpvar As Boolean = dtpDOB.Enabled
            'dtpDOB.Visible = True
            'dtpDOB.Enabled = True
            dtpDOB.ValueIsDbNull = True
            'dtpDOB.Enabled = tmpvar
            cboMaritalStatus.SelectedIndex = -1
            'tmpvar = dtpMarriageDate.Enabled
            'dtpMarriageDate.Enabled = True
            'dtpMarriageDate.Visible = True
            dtpMarriageDate.ValueIsDbNull = True
            dtpDOB.ValueIsDbNull = True
            dtpSpouseDOB.ValueIsDbNull = True

            'dtpMarriageDate.Enabled = tmpvar
            cboEducation.SelectedIndex = -1
            cboOccupation.SelectedIndex = -1
            cboSpouseOccupation.SelectedIndex = -1
            txtMobile.Text = ""
            txtOffPhone.Text = ""
            txtResPhone.Text = ""
            txtEmail.Text = ""

            'cboAddressType.SelectedIndex = -1
            txtAddress1.Text = ""
            txtAddress2.Text = ""
            txtAddress3.Text = ""
            txtAddress4.Text = ""

            cboCity.SelectedIndex = -1
            cboState.SelectedIndex = -1
            cboCountry.SelectedIndex = -1
            txtPincode.Text = ""

            txtAddress11.Text = ""
            txtAddress12.Text = ""
            txtAddress13.Text = ""
            txtAddress14.Text = ""

            cboCity1.SelectedIndex = -1
            cboState1.SelectedIndex = -1
            cboCountry1.SelectedIndex = -1
            txtPincode1.Text = ""

            txtFacebookID.Text = String.Empty
            txtTwitterId.Text = String.Empty
            txtLinkedInID.Text = String.Empty
            txtGooglePulsId.Text = String.Empty
            txtHi5ID.Text = String.Empty
            txtMySpaceId.Text = String.Empty
            txtIbiboId.Text = String.Empty
            txtFourSqureId.Text = String.Empty
            txtOrkutId.Text = String.Empty
            txtSkypeId.Text = String.Empty
            txtGST.Text = String.Empty
            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function
    ''' <summary>
    ''' Enabled or Disable Fields
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetEnableDisableFields(ByVal IsEnabled As Boolean) As Boolean
        Try
            BtnSearchCustomer.Focus()

            'cboCustomerType.Enabled = IIf(IsEnabled = True, False, True)
            cboTitle.Enabled = IIf(IsEnabled = True, False, True)
            cboGender.Enabled = IIf(IsEnabled = True, False, True)
            dtpDOB.Enabled = IIf(IsEnabled = True, False, True)
            cboMaritalStatus.Enabled = IIf(IsEnabled = True, False, True)
            dtpMarriageDate.Enabled = IIf(IsEnabled = True, False, True)
            cboEducation.Enabled = IIf(IsEnabled = True, False, True)
            cboOccupation.Enabled = IIf(IsEnabled = True, False, True)

            cboSpouseOccupation.Enabled = IIf(IsEnabled = True, False, True)
            cboSpouseTitle.Enabled = IIf(IsEnabled = True, False, True)

            cboCity.Enabled = IIf(IsEnabled = True, False, True)
            cboState.Enabled = IIf(IsEnabled = True, False, True)
            cboCountry.Enabled = IIf(IsEnabled = True, False, True)
            cboCity1.Enabled = IIf(IsEnabled = True, False, True)
            cboState1.Enabled = IIf(IsEnabled = True, False, True)
            cboCountry1.Enabled = IIf(IsEnabled = True, False, True)


            txtCustomerCode.ReadOnly = IsEnabled
            txtFirstName.ReadOnly = IsEnabled
            txtMiddleName.ReadOnly = IsEnabled
            txtLastName.ReadOnly = IsEnabled
            txtMobile.ReadOnly = IsEnabled
            txtOffPhone.ReadOnly = IsEnabled
            txtResPhone.ReadOnly = IsEnabled
            txtEmail.ReadOnly = IsEnabled

            txtSpouseFirstName.ReadOnly = IsEnabled
            txtSpouseMiddleName.ReadOnly = IsEnabled
            txtSpouseLastName.ReadOnly = IsEnabled


            'cboAddressType.Enabled = False
            txtAddress1.ReadOnly = IsEnabled
            txtAddress2.ReadOnly = IsEnabled
            txtAddress3.ReadOnly = IsEnabled
            txtAddress4.ReadOnly = IsEnabled
            txtPincode.ReadOnly = IsEnabled

            txtAddress11.ReadOnly = IsEnabled
            txtAddress12.ReadOnly = IsEnabled
            txtAddress13.ReadOnly = IsEnabled
            txtAddress14.ReadOnly = IsEnabled
            txtPincode1.ReadOnly = IsEnabled

            txtFacebookID.ReadOnly = IsEnabled
            txtTwitterId.ReadOnly = IsEnabled
            txtLinkedInID.ReadOnly = IsEnabled
            txtGooglePulsId.ReadOnly = IsEnabled
            txtHi5ID.ReadOnly = IsEnabled
            txtMySpaceId.ReadOnly = IsEnabled
            txtIbiboId.ReadOnly = IsEnabled
            txtFourSqureId.ReadOnly = IsEnabled
            txtOrkutId.ReadOnly = IsEnabled
            txtSkypeId.ReadOnly = IsEnabled
            txtGST.ReadOnly = IsEnabled
            If clsDefaultConfiguration.CustomerClassSelection Then
                cboCustomerClass.Enabled = IIf(IsEnabled = True, False, True)
                If cboCustomerClass.Text = "Both" Then
                    cboCustomerClass.Enabled = False
                End If
            End If
            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function
    ''' <summary>
    ''' Save Customer Information
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCustomerInformation() As Boolean
        Try
            Dim docNo As String = objComn.getDocumentNo("Customer Loyalty", clsAdmin.SiteCode)
            Dim prefixDocNo = "CLS" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3, 3)

            vCustomerNo = objComn.GenDocNo(prefixDocNo, 15, docNo)

            drCustmInfo = dsMain.Tables("CLPCustomers").NewRow()
            If PrepareCustmInfoData(drCustmInfo) = True Then
                dsMain.Tables("CLPCustomers").Rows.Add(drCustmInfo)
            End If

            'If Not (txtAddress1.Text.Trim = "") Then
            drCustmAdds = dsMain.Tables("CustomerAddress").NewRow()
            If PrepareCustmAddsData(drCustmAdds, 1) = True Then
                dsMain.Tables("CustomerAddress").Rows.Add(drCustmAdds)
            End If
            'Else
            'ShowMessage(getValueByKey("SOC019"), getValueByKey("CLAE04"))
            'Return False
            'End If

            If Not (txtAddress11.Text.Trim = "") Then 'AndAlso Not (cboCity1.Text = "") And Not (txtPincode1.Text.Trim = "") Then
                drCustmAdds = dsMain.Tables("CustomerAddress").NewRow()
                If PrepareCustmAddsData(drCustmAdds, 2) = True Then
                    dsMain.Tables("CustomerAddress").Rows.Add(drCustmAdds)
                End If
            End If

            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Return False
        End Try

    End Function
    ''' <summary>
    ''' Update function for Customer Information
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateCustomerInformation() As Boolean
        Try

            findKeyInfo(0) = clsAdmin.SiteCode
            findKeyInfo(1) = txtCustomerCode.Text.Trim

            drCustmInfo = dsMain.Tables("CustomerSaleOrder").Rows.Find(findKeyInfo)
            If Not (drCustmInfo Is Nothing) Then
                PrepareCustmInfoData(drCustmInfo)
            End If

            For AddressRow = 1 To 2
                findKeyAdds(0) = clsAdmin.SiteCode
                findKeyAdds(1) = txtCustomerCode.Text.Trim
                findKeyAdds(2) = CustomerType
                findKeyAdds(3) = AddressRow

                'Edited by Rohit to avoid addition of blank records in Customer Details.
                If AddressRow = 1 AndAlso Not (txtAddress1.Text.Trim = "") Then
                    drCustmAdds = dsMain.Tables("CustomerAddress").Rows.Find(findKeyAdds)
                    If Not (drCustmAdds Is Nothing) Then
                        PrepareCustmAddsData(drCustmAdds, AddressRow)
                    Else
                        drCustmAdds = dsMain.Tables("CustomerAddress").NewRow()
                        If PrepareCustmAddsData(drCustmAdds, 1) = True Then
                            dsMain.Tables("CustomerAddress").Rows.Add(drCustmAdds)
                        End If
                    End If
                ElseIf Not (txtAddress11.Text.Trim = "") Then
                    drCustmAdds = dsMain.Tables("CustomerAddress").Rows.Find(findKeyAdds)
                    If Not (drCustmAdds Is Nothing) Then
                        PrepareCustmAddsData(drCustmAdds, AddressRow)
                    Else
                        drCustmAdds = dsMain.Tables("CustomerAddress").NewRow()
                        If PrepareCustmAddsData(drCustmAdds, 2) = True Then
                            dsMain.Tables("CustomerAddress").Rows.Add(drCustmAdds)
                        End If
                    End If
                End If

            Next

            'objCustm.SaveData(dsCustmInfo, "SOCustomer", False)

            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Return False
        End Try

    End Function
    ''' <summary>
    ''' Prepare Customer Information Data
    ''' </summary>
    ''' <param name="drCustInfo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PrepareCustmInfoData(ByRef drCustInfo As DataRow) As Boolean
        Try

            drCustInfo("SiteCode") = clsAdmin.SiteCode

            'query = "select top(1) ClpProgramId from CLPProgramSiteMap where Sitecode='" & request.CLPCustomer.SiteCode & "' AND Status=1 "
            'Using dataReader As SqlDataReader = GetReader(query)
            '    If dataReader.HasRows Then
            '        Do While dataReader.Read()
            '            Dim customerGroup As New CustomerGroupDetails
            '            request.CLPCustomer.ClpProgId = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
            '        Loop
            '    Else
            '        Throw New CLPProgramNotMapped("No Clp program is mapped to this site.")
            '    End If
            'End Using


            drCustInfo("CustomerNo") = vCustomerNo
            drCustInfo("CustomerType") = CustomerType
            drCustInfo("TitleCode") = cboTitle.SelectedValue
            drCustInfo("FirstName") = txtFirstName.Text
            drCustInfo("MiddleName") = IIf(txtMiddleName.Text = Nothing, DBNull.Value, txtMiddleName.Text)
            drCustInfo("LastName") = txtLastName.Text
            drCustInfo("CustomerName") = txtFirstName.Text & " " & txtMiddleName.Text & " " & txtLastName.Text
            If IsDBNull(dtpDOB.Value) Then
                dtpDOB.Value = DBNull.Value
            ElseIf dtpDOB.Value = "#12:00:00 AM#" Then
                dtpDOB.Value = DBNull.Value
            End If

            drCustInfo("DateofBirth") = dtpDOB.Value

            drCustInfo("ResidencePhone") = txtResPhone.Text
            drCustInfo("MobilePhone") = IIf(txtMobile.Text = Nothing, DBNull.Value, txtMobile.Text)
            drCustInfo("OfficePhone") = IIf(txtOffPhone.Text = Nothing, DBNull.Value, txtOffPhone.Text)
            drCustInfo("Occupation") = IIf(cboOccupation.Text = Nothing, DBNull.Value, cboOccupation.SelectedValue)
            drCustInfo("Education") = IIf(cboEducation.Text = Nothing, DBNull.Value, cboEducation.SelectedValue)
            drCustInfo("EmailId") = IIf(txtEmail.Text = Nothing, DBNull.Value, txtEmail.Text)
            drCustInfo("PromotionInfobyEmail") = ChkBoxByEmail.Checked
            drCustInfo("PromotionInfobySMS") = ChkBoxBySMS.Checked
            drCustInfo("Gender") = cboGender.SelectedValue

            drCustInfo("MaritalStatus") = IIf(cboMaritalStatus.Text = Nothing, DBNull.Value, cboMaritalStatus.SelectedValue)
            If dtpMarriageDate.Visible = True Then
                'drCustInfo("MarriageDt") = CType(Format(dtpMarriageDate.Value, vDateFormat), Date)
                drCustInfo("MarriageDt") = dtpMarriageDate.Value
            Else
                drCustInfo("MarriageDt") = DBNull.Value
            End If

            drCustInfo("CREATEDAT") = clsAdmin.SiteCode
            drCustInfo("CREATEDBY") = clsAdmin.UserCode
            drCustInfo("CREATEDON") = Now
            drCustInfo("UPDATEDAT") = clsAdmin.SiteCode
            drCustInfo("UPDATEDON") = Now
            drCustInfo("UPDATEDBY") = clsAdmin.UserCode
            drCustInfo("STATUS") = True

            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Prepare Customer Address Information
    ''' </summary>
    ''' <param name="drCustAdds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PrepareCustmAddsData(ByRef drCustAdds As DataRow, ByVal vAddressType As Integer) As Boolean
        Try
            drCustAdds("SiteCode") = clsAdmin.SiteCode
            drCustAdds("CustomerNo") = vCustomerNo
            drCustAdds("CustomerType") = CustomerType
            If vAddressType = 1 Then

                drCustAdds("AddressType") = vAddressType
                drCustAdds("AddressLn1") = txtAddress1.Text
                drCustAdds("AddressLn2") = txtAddress2.Text
                drCustAdds("AddressLn3") = txtAddress3.Text
                drCustAdds("AddressLn4") = txtAddress4.Text
                drCustAdds("PinCode") = txtPincode.Text
                drCustAdds("CityCode") = cboCity.SelectedValue
                drCustAdds("StateCode") = cboState.SelectedValue
                drCustAdds("CountryCode") = cboCountry.SelectedValue

            ElseIf vAddressType = 2 Then
                drCustAdds("AddressType") = vAddressType
                drCustAdds("AddressLn1") = txtAddress11.Text
                drCustAdds("AddressLn2") = txtAddress12.Text
                drCustAdds("AddressLn3") = txtAddress13.Text
                drCustAdds("AddressLn4") = txtAddress14.Text
                drCustAdds("PinCode") = txtPincode1.Text
                drCustAdds("CityCode") = cboCity1.SelectedValue
                drCustAdds("StateCode") = cboState1.SelectedValue
                drCustAdds("CountryCode") = cboCountry1.SelectedValue
            End If

            drCustAdds("CREATEDAT") = clsAdmin.SiteCode
            drCustAdds("CREATEDBY") = clsAdmin.UserCode
            drCustAdds("CREATEDON") = Now
            drCustAdds("UPDATEDAT") = clsAdmin.SiteCode
            drCustAdds("UPDATEDON") = Now
            drCustAdds("UPDATEDBY") = clsAdmin.UserCode
            drCustAdds("STATUS") = True

            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Validate function for Customer Information
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidatedLoyaltyCustomer() As Boolean
        Try
            If txtFirstName.Text = "" Then
                ShowMessage(getValueByKey("SOC008"), "SOC008 - " & getValueByKey("CLAE04"))
                txtFirstName.Focus()
                Return IsValidated
                Exit Function
                'ElseIf txtLastName.Text = "" Then
                '    ShowMessage(getValueByKey("SOC009"), "SOC009 - " & getValueByKey("CLAE04"))
                '    txtLastName.Focus()
                '    Return IsValidated
                '    Exit Function
            ElseIf String.IsNullOrEmpty(txtMobile.Text) Then
                ShowMessage("Mobile Number is Mandatory", getValueByKey("CLAE04"))
                txtMobile.Focus()
                Return IsValidated
                'ElseIf String.IsNullOrEmpty(txtAddress1.Text) Then
                '    ShowMessage(getValueByKey("SOC019"), getValueByKey("CLAE04"))
                '    txtAddress1.Focus()
                '    Return IsValidated
                '    Exit Function            
            ElseIf cboGender.Text = "" Then
                ShowMessage(getValueByKey("SOC010"), "SOC010 - " & getValueByKey("CLAE04"))
                cboGender.Focus()
                Return IsValidated
                Exit Function
            ElseIf txtEmail.Text <> String.Empty AndAlso validateEmailId(txtEmail.Text) = False Then
                ShowMessage(getValueByKey("HMD003"), "HMD003 - " & getValueByKey("CLAE04"))
                txtEmail.Focus()
                Return IsValidated
                Exit Function
            
            End If
            'code added by irfan
            If OldGstNo <> txtGST.Text.ToString Then
                If txtGST.Text <> "" Then
                    'Dim objcomm As New clsCommon
                    If objCustm.CheckGStNoExist(txtGST.Text.Trim()) = True Then
                        ShowMessage("GST No. already exist", "Information")
                        Return False
                    End If
                End If
            End If

            '===========================================================================
            If Not String.IsNullOrEmpty(txtMobile.Text) Then
                If txtMobile.Text.Length > 10 Then
                    ShowMessage("Phone numbers must be 10 digits only.", "Information")
                    txtMobile.Text = ""
                    txtMobile.Focus()
                    Exit Function
                End If
                If txtMobile.Text.Length <> 10 Then
                    ShowMessage("Please Enter 10 digits Mobile Number only.", "Information")
                    txtMobile.Text = ""
                    txtMobile.Focus()
                    Exit Function
                End If
                If Not IsNumeric(txtMobile.Text) Then
                    ShowMessage("Phone numbers  Must be Number.", "Information")
                    txtMobile.Text = ""
                    txtMobile.Focus()
                    Exit Function
                End If
            End If
          

            If Not String.IsNullOrEmpty(txtOffPhone.Text) Then
                If txtOffPhone.Text.Length > 10 Then
                    ShowMessage("Office numbers must be 10 digits only.", "Information")
                    txtOffPhone.Text = ""
                    txtOffPhone.Focus()
                    Exit Function
                End If
                If txtOffPhone.Text.Length <> 10 Then
                    ShowMessage("Contact Must 10 digits  Number only.", "Information")
                    txtOffPhone.Text = ""
                    txtOffPhone.Focus()
                    Exit Function
                End If
                If Not IsNumeric(txtOffPhone.Text) Then
                    ShowMessage("Office numbers  Must be Number.", "Information")
                    txtOffPhone.Text = ""
                    txtOffPhone.Focus()
                    Exit Function
                End If
            End If

            If Not String.IsNullOrEmpty(txtResPhone.Text) Then
                If txtResPhone.Text.Length > 10 Then
                    ShowMessage("Residence numbers must be 10 digits only.", "Information")
                    txtResPhone.Text = ""
                    txtResPhone.Focus()
                    Exit Function
                End If

                If txtResPhone.Text.Length <> 10 Then
                    ShowMessage("Please Enter 10 digits Residence Number only.", "Information")
                    txtResPhone.Text = ""
                    txtResPhone.Focus()
                    Exit Function
                End If
                If Not IsNumeric(txtResPhone.Text) Then
                    ShowMessage("Residence numbers  Must be Number.", "Information")
                    txtResPhone.Text = ""
                    txtResPhone.Focus()
                    Exit Function
                End If
            End If

            '============================================================================
            If clsDefaultConfiguration.CustomerClassSelection Then
                If cboCustomerClass.Text = String.Empty Then
                    ShowMessage("Please select Customer Class Type", getValueByKey("CLAE04"))
                    cboCustomerClass.Focus()
                    Return IsValidated
                    Exit Function
                End If
            End If
            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Search Customer Information
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnSearchCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchCustomer.Click
        Try
            Dim objCust As New frmSearchCustomer
            objCust.CheckTransactionRights = True
            objCust.FormName = Me.Name()
            objCust.ShowDialog()

            If clsDefaultConfiguration.DetailedCustomerCreationformat = "1" Then
                Exit Sub
            End If

            Dim dt As DataTable
            Dim objCustm As New clsCLPCustomer
            Dim dtCust As DataTable = objCust.dtCustmInfo()

            If objCust.dtCustmInfo Is Nothing Then
                If pSearchCust = "SEARCH" Then
                    Exit Sub
                End If
            ElseIf objCust.dtCustmInfo.Rows.Count > 0 Then
                SelectedCustmCode = objCust.dtCustmInfo.Rows(0)("CustomerNo")
            Else

            End If
            'If Not dtCust Is Nothing AndAlso dtCust.Rows.Count > 0 Then
            '    SetCustomerInformationInForm(dtCust)

            'ElseIf objCust.vCustomerNo <> String.Empty Then
            '    dt = objCustm.GetCustomerInformation("SO", clsAdmin.SiteCode, clsAdmin.CLPProgram, objCust.vCustomerNo)
            '    SetCustomerInformationInForm(dt)
            'End If

            objCust.Dispose()
            '---------------------- search-----------------
            If Not String.IsNullOrEmpty(SelectedCustmCode) Then
                _dsMain = objCustm.GetCustomerDataSet(clsAdmin.SiteCode, SelectedCustmCode)

                Try
                    drCustmInfo = Nothing
                    drCustmInfo = dsMain.Tables("CLPCustomer").Rows(0)

                    If Not (drCustmInfo Is Nothing) Then
                        vCustomerNo = drCustmInfo("CardNo")

                        txtCustomerCode.Text = drCustmInfo("CardNo")
                        'cboCustomerType.SelectedIndex = IIf(drCustmInfo("CustomerType") = "SO", 1, 0)
                        cboTitle.SelectedValue = IIf(drCustmInfo("Title") Is DBNull.Value, -1, drCustmInfo("Title"))
                        txtFirstName.Text = IIf(drCustmInfo("FirstName") Is DBNull.Value, "", drCustmInfo("FirstName"))
                        txtMiddleName.Text = IIf(drCustmInfo("MiddleName") Is DBNull.Value, "", drCustmInfo("MiddleName"))
                        txtLastName.Text = IIf(drCustmInfo("SurName") Is DBNull.Value, "", drCustmInfo("SurName"))
                        dtpDOB.Value = IIf(drCustmInfo("BirthDate") Is DBNull.Value, DBNull.Value, drCustmInfo("BirthDate"))
                        txtMobile.Text = IIf(drCustmInfo("Mobileno") Is DBNull.Value, "", drCustmInfo("Mobileno"))
                        txtResPhone.Text = IIf(drCustmInfo("Res_Phone") Is DBNull.Value, "", drCustmInfo("Res_Phone"))
                        txtOffPhone.Text = IIf(drCustmInfo("OfficeNo") Is DBNull.Value, "", drCustmInfo("OfficeNo"))
                        cboOccupation.SelectedValue = IIf(drCustmInfo("Occupation") Is DBNull.Value, -1, drCustmInfo("Occupation"))
                        cboEducation.SelectedValue = IIf(drCustmInfo("Education") Is DBNull.Value, -1, drCustmInfo("Education"))
                        txtEmail.Text = IIf(drCustmInfo("EmailId") Is DBNull.Value, "", drCustmInfo("EmailId"))
                        cboGender.SelectedValue = IIf(drCustmInfo("Gender") Is DBNull.Value, "", drCustmInfo("Gender"))
                        cboMaritalStatus.SelectedValue = IIf(drCustmInfo("MaritalStatus") Is DBNull.Value, -1, drCustmInfo("MaritalStatus"))
                        dtpMarriageDate.Value = IIf(drCustmInfo("MarriageAnivDate") Is DBNull.Value, "", drCustmInfo("MarriageAnivDate"))

                        CtrlLblStatusValue.Text = IIf(drCustmInfo("resgistrationstatus") Is DBNull.Value, "", drCustmInfo("resgistrationstatus"))
                        txtTierType.Text = IIf(drCustmInfo("CardType") Is DBNull.Value, "", drCustmInfo("CardType"))

                        cboSpouseTitle.SelectedValue = IIf(drCustmInfo("SpouseTitle") Is DBNull.Value, -1, drCustmInfo("SpouseTitle"))
                        txtSpouseFirstName.Text = IIf(drCustmInfo("SpouseFirstName") Is DBNull.Value, "", drCustmInfo("SpouseFirstName"))
                        txtSpouseMiddleName.Text = IIf(drCustmInfo("SpouseMiddleName") Is DBNull.Value, "", drCustmInfo("SpouseMiddleName"))
                        txtSpouseLastName.Text = IIf(drCustmInfo("SpouseSurName") Is DBNull.Value, "", drCustmInfo("SpouseSurName"))
                        dtpSpouseDOB.Value = IIf(drCustmInfo("SpouseDob") Is DBNull.Value, DBNull.Value, drCustmInfo("SpouseDob"))
                        cboSpouseOccupation.SelectedValue = IIf(drCustmInfo("SpouseOccupation") Is DBNull.Value, -1, drCustmInfo("SpouseOccupation"))

                        txtFacebookID.Text = IIf(drCustmInfo("FacebookId") Is DBNull.Value, "", drCustmInfo("FacebookId"))
                        txtTwitterId.Text = IIf(drCustmInfo("TwitterId") Is DBNull.Value, "", drCustmInfo("TwitterId"))
                        txtLinkedInID.Text = IIf(drCustmInfo("LinkedInID") Is DBNull.Value, "", drCustmInfo("LinkedInID"))
                        txtGooglePulsId.Text = IIf(drCustmInfo("GooglePlusId") Is DBNull.Value, "", drCustmInfo("GooglePlusId"))
                        txtHi5ID.Text = IIf(drCustmInfo("Hi5Id") Is DBNull.Value, "", drCustmInfo("Hi5Id"))
                        txtMySpaceId.Text = IIf(drCustmInfo("MySpaceId") Is DBNull.Value, "", drCustmInfo("MySpaceId"))
                        txtIbiboId.Text = IIf(drCustmInfo("IbiboId") Is DBNull.Value, "", drCustmInfo("IbiboId"))
                        txtFourSqureId.Text = IIf(drCustmInfo("FourSquareId") Is DBNull.Value, "", drCustmInfo("FourSquareId"))
                        ' txtOrkutId.Text = IIf(drCustmInfo("OrkutId") Is DBNull.Value, "", drCustmInfo("OrkutId"))
                        txtGST.Text = IIf(drCustmInfo("OrkutId") Is DBNull.Value, "", drCustmInfo("OrkutId"))
                        OldGstNo = txtGST.Text
                        txtSkypeId.Text = IIf(drCustmInfo("SkypeId") Is DBNull.Value, "", drCustmInfo("SkypeId"))
                        
                        ChkBoxByEmail.Checked = False
                        If Not IsDBNull(drCustmInfo("PromotionInfobyEmail")) AndAlso drCustmInfo("PromotionInfobyEmail") Then
                            ChkBoxByEmail.Checked = True
                        End If
                        ChkBoxBySMS.Checked = False
                        If Not IsDBNull(drCustmInfo("PromotionInfobySMS")) AndAlso drCustmInfo("PromotionInfobySMS") Then
                            ChkBoxBySMS.Checked = True
                        End If
                        txtBalancePoint.Text = IIf(drCustmInfo("TotalBalancePoint") Is DBNull.Value, "", drCustmInfo("TotalBalancePoint"))
                    End If
                Catch ex As Exception
                    ShowMessage(ex.Message, getValueByKey("CLAE05"))
                    LogException(ex)
                End Try

                Try
                    drCustmAdds = Nothing
                    For index = 0 To dsMain.Tables("CLPCustomerAddress").Rows.Count - 1
                        drCustmAdds = dsMain.Tables("CLPCustomerAddress").Rows(index)
                        If Not (drCustmAdds Is Nothing) Then
                            If dsMain.Tables("CLPCustomerAddress").Rows(index)("AddressType") = 1 Then
                                txtAddress1.Text = IIf(drCustmAdds("AddressLn1") Is DBNull.Value, "", drCustmAdds("AddressLn1"))
                                txtAddress2.Text = IIf(drCustmAdds("AddressLn2") Is DBNull.Value, "", drCustmAdds("AddressLn2"))
                                txtAddress3.Text = IIf(drCustmAdds("AddressLn3") Is DBNull.Value, "", drCustmAdds("AddressLn3"))
                                txtAddress4.Text = IIf(drCustmAdds("AddressLn4") Is DBNull.Value, "", drCustmAdds("AddressLn4"))
                                cboCountry.SelectedValue = IIf(drCustmAdds("CountryCode") Is DBNull.Value, "", drCustmAdds("CountryCode"))
                                cboState.SelectedValue = IIf(drCustmAdds("StateCode") Is DBNull.Value, "", drCustmAdds("StateCode"))
                                cboCity.SelectedValue = IIf(drCustmAdds("CityCode") Is DBNull.Value, "", drCustmAdds("CityCode"))
                                txtPincode.Text = IIf(drCustmAdds("PinCode") Is DBNull.Value, "", drCustmAdds("PinCode"))

                            Else
                                txtAddress11.Text = IIf(drCustmAdds("AddressLn1") Is DBNull.Value, "", drCustmAdds("AddressLn1"))
                                txtAddress12.Text = IIf(drCustmAdds("AddressLn2") Is DBNull.Value, "", drCustmAdds("AddressLn2"))
                                txtAddress13.Text = IIf(drCustmAdds("AddressLn3") Is DBNull.Value, "", drCustmAdds("AddressLn3"))
                                txtAddress14.Text = IIf(drCustmAdds("AddressLn4") Is DBNull.Value, "", drCustmAdds("AddressLn4"))
                                cboCountry1.SelectedValue = IIf(drCustmAdds("CountryCode") Is DBNull.Value, "", drCustmAdds("CountryCode"))
                                cboState1.SelectedValue = IIf(drCustmAdds("StateCode") Is DBNull.Value, "", drCustmAdds("StateCode"))
                                cboCity1.SelectedValue = IIf(drCustmAdds("CityCode") Is DBNull.Value, "", drCustmAdds("CityCode"))
                                txtPincode1.Text = IIf(drCustmAdds("PinCode") Is DBNull.Value, "", drCustmAdds("PinCode"))
                            End If
                            End If
                    Next index
                    If clsDefaultConfiguration.CustomerClassSelection Then
                        If Not String.IsNullOrEmpty(SelectedCustmCode) Then
                            Dim dtCustClass As DataTable = objHlthCustomer.GetCustomerClassDataByCustomerNo(SelectedCustmCode, clsAdmin.CLPProgram, clsAdmin.SiteCode)
                            If Not dtCustClass Is Nothing And dtCustClass.Rows.Count > 0 Then
                                If Not IsDBNull(dtCustClass.Rows(0)("Classify")) Then
                                    cboCustomerClass.Text = dtCustClass.Rows(0)("Classify")
                                    If dtCustClass.Rows(0)("Classify") = "Both" Then
                                        cboCustomerClass.Enabled = False
                                    End If
                                End If
                            End If
                        End If
                    End If
                Catch ex As Exception
                    ShowMessage(ex.Message, getValueByKey("CLAE05"))
                    LogException(ex)
                End Try
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
        BtnEdit.Enabled = True
        'BtnDelete.Enabled = True
        BtnDelete.Visible = False
    End Sub
    ''' <summary>
    ''' Set Key on Search Button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmAddCustLoyalty_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                BtnSearchCustomer_Click(sender, New EventArgs)
            End If
            If e.KeyCode = Keys.Escape Then
                BtnExit_Click(sender, New EventArgs)
            End If
            'If e.KeyCode = Keys.F + e.KeyCode = Keys.ControlKey Then
            '    BtnSearchCustomer_Click(sender, New EventArgs)
            'End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE04"))
        End Try

    End Sub
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub cboMaritalStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        If cboMaritalStatus.Text = "Married" Then
            lblMarriageDate.Visible = True
            dtpMarriageDate.Visible = True
        Else
            lblMarriageDate.Visible = False
            dtpMarriageDate.Visible = False
        End If

    End Sub
    'Private Sub dtpDOB_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDOB.Leave



    'End Sub
    Private Sub dtpMarriageDate_Leave(ByVal sender As Object, ByVal e As System.EventArgs)

        'If dtpMarriageDate.Value Is DBNull.Value Then
        '    ShowMessage("Date of Marriage cannot be Blank.", "Customer Information")
        '    dtpMarriageDate.Value = FormatDateTime(Now, DateFormat.ShortDate)
        'Else
        'If Not dtpMarriageDate.Value Is DBNull.Value AndAlso DateDiff(DateInterval.Day, Now, dtpMarriageDate.Value) > 0 Then
        '    ShowMessage("Date of Marriage cannot greater than Today", "Customer Information")
        '    dtpMarriageDate.Value = FormatDateTime(Now, DateFormat.ShortDate)
        'End If
        'End If

    End Sub
    Private Sub btnFormClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub dtpDOB_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtpDOB.Validating, dtpSpouseDOB.Validating
        Try
            If dtpDOB.Value Is DBNull.Value Then
                'ShowMessage("Date of Birth cannot be Blank.", "Customer Information")
                dtpDOB.Value = DBNull.Value
            Else
                If Not (DateDiff(DateInterval.Day, dtpDOB.Value, Now) > 0) AndAlso Not (txtFirstName.Text = String.Empty) Then
                    ShowMessage(getValueByKey("SOC015"), "SOC015 - " & getValueByKey("CLAE04"))
                    dtpDOB.Value = Nothing
                End If
                'code added by irfan on 16/1/2018 for Maintis issue 2812
                If dtpDOB.Value > Now Then
                    ShowMessage(getValueByKey("SOC015"), "SOC015 - " & getValueByKey("CLAE04"))
                    dtpDOB.Value = Nothing
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Sub dtpDOB_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDOB.ValueChanged, dtpSpouseDOB.ValueChanged


    End Sub
    Private Sub txtFirstName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFirstName.KeyDown, txtMiddleName.KeyDown, txtLastName.KeyDown, txtSpouseLastName.KeyDown, txtSpouseMiddleName.KeyDown, txtSpouseFirstName.KeyDown
        If (e.KeyValue > 64 AndAlso e.KeyValue < 91) Or e.KeyValue = 8 Or e.KeyValue = 32 Or e.KeyValue = 46 Then
            e.SuppressKeyPress = False
        Else
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub cboCountry1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            dvState1.RowFilter = "ParentCode='" & cboCountry1.SelectedValue & "'"
            dvCity1.RowFilter = "1=0"
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cboCountry_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            dvState.RowFilter = "ParentCode='" & cboCountry.SelectedValue & "'"
            dvCity.RowFilter = "1=0"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cbostate1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            dvCity1.RowFilter = "ParentCode='" & cboState1.SelectedValue & "'"
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cboState_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            dvCity.RowFilter = "ParentCode='" & cboState.SelectedValue & "'"
        Catch ex As Exception

        End Try
    End Sub
    'commented for bug no 1549
    'Private Sub txtOffPhone_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOffPhone.KeyDown, txtResPhone.KeyDown, txtMobile.KeyDown
    '    Try
    '        If (e.KeyValue >= 48 And e.KeyValue < 58) Or (e.KeyValue >= 96 And e.KeyValue < 106) Or e.KeyValue = 8 Or e.KeyValue = 32 Or e.KeyValue = 46 Then
    '            e.SuppressKeyPress = False
    '        Else
    '            e.SuppressKeyPress = True
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub txtPincode1_PreviewKeyDown(sender As System.Object, e As System.Windows.Forms.PreviewKeyDownEventArgs)
        If e.KeyCode = Keys.Tab Then
            tbAddress.SelectedIndex = 1
        End If
    End Sub

    Public Sub setAddressAutoComplete() '--ByRef dtCombo As DataTable)
        Try
            Dim objClp As New clsCLPCustomer
            Dim dtCustmData = objClp.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, String.Empty, isAddressCombined:=True)

            Dim Addressarray1 As String() = (From row In dtCustmData
                                  Select Convert.ToString(row("ADDRESSLN1"))).Distinct().ToArray()

            txtAddress1.Values = Addressarray1
            txtAddress11.Values = Addressarray1

            Dim Addressarray2 As String() = (From row In dtCustmData
                                  Select Convert.ToString(row("ADDRESSLN2"))).Distinct().ToArray()

            txtAddress2.Values = Addressarray2
            txtAddress12.Values = Addressarray2

            Dim Addressarray3 As String() = (From row In dtCustmData
                                  Select Convert.ToString(row("ADDRESSLN3"))).Distinct().ToArray()

            txtAddress3.Values = Addressarray3
            txtAddress13.Values = Addressarray3

            Dim Addressarray4 As String() = (From row In dtCustmData
                                  Select Convert.ToString(row("ADDRESSLN4"))).Distinct().ToArray()

            txtAddress4.Values = Addressarray4
            txtAddress14.Values = Addressarray4

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub txtMobile_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtMobile.KeyDown
        Try

            If e.KeyCode = Keys.Enter AndAlso String.IsNullOrEmpty(txtMobile.Text) = False Then
                If Not ValidatedLoyaltyCustomer() Then
                    Exit Sub
                End If
                CustomerResponse = CustomerBL.GetCustomerInfo(clsAdmin.CLPProgram, txtCustomerCode.Text, KeyIsMobileNo:=False)
                If CustomerResponse.CLPCustomer Is Nothing Then
                    CustomerResponse = CustomerBL.GetCustomerInfo(clsAdmin.CLPProgram, txtMobile.Text, KeyIsMobileNo:=True)
                    If CustomerResponse.CLPCustomer Is Nothing Then

                        Dim eventType As Int32 = 1
                        Dim PromotionInfobyEmail As Single = 0
                        Dim PromotionInfobySMS As Single = 0
                        If ChkBoxByEmail.Checked Then PromotionInfobyEmail = 1
                        If ChkBoxBySMS.Checked Then PromotionInfobySMS = 1

                        If eventType = 1 Then
                            Dim messageType As MessageType
                            Dim request As New SaveCustomerRequest With {.CreatedAt = clsAdmin.SiteCode, .CreatedBy = clsAdmin.UserCode}
                            request.CLPCustomer = New CLPCustomerDTO() _
                            With {.MobileNo = txtMobile.Text.Trim, .SiteCode = clsAdmin.SiteCode,
                                  .CreatedAt = clsAdmin.SiteCode, .CreatedBy = clsAdmin.UserCode,
                                  .EmailId = txtEmail.Text, .Title = cboTitle.SelectedValue, .FirstName = txtFirstName.Text,
                                  .Gender = If(cboGender.SelectedValue Is Nothing, "", cboGender.SelectedValue.ToString()),
                                  .LastName = txtLastName.Text, .MiddleName = txtMiddleName.Text,
                                  .RegistrationStatus = CtrlLblStatusValue.Text, .CardType = txtTierType.Text,
                                  .SpouseTitle = cboSpouseTitle.SelectedValue, .SpouseFirstName = txtSpouseFirstName.Text,
                                  .SpouseMiddleName = txtSpouseMiddleName.Text, .SpouseLastName = txtSpouseLastName.Text,
                                  .SpouseOccupation = cboSpouseOccupation.SelectedValue,
                                  .FacebookId = txtFacebookID.Text, .TwitterId = txtTwitterId.Text, .LinkedInID = txtLinkedInID.Text,
                                  .GooglePlusId = txtGooglePulsId.Text, .Hi5Id = txtHi5ID.Text, .MySpaceId = txtMySpaceId.Text,
                                  .IbiboId = txtIbiboId.Text, .FourSquareId = txtFourSqureId.Text,
                                  .OrkutId = txtOrkutId.Text,
                                  .SkypeId = txtSkypeId.Text,
                                  .ResidenceNumber = txtResPhone.Text, .OfficeNumber = txtOffPhone.Text, .Education = cboEducation.SelectedValue,
                                  .Occupation = cboOccupation.SelectedValue, .MaritalStatus = cboMaritalStatus.SelectedValue,
                                  .PromotionInfobyEmail = PromotionInfobyEmail, .PromotionInfobySMS = PromotionInfobySMS, .GSTNo = txtGST.Text
                                 }

                            If dsCLPprog IsNot Nothing AndAlso dsCLPprog.Tables.Contains("CLPHeader") Then
                                If dsCLPprog.Tables("CLPHeader").Rows.Count > 0 Then

                                    txtBalancePoint.Text = "0"

                                    If dsCLPprog.Tables("CLPHeader").Rows(0)("IsJoiningReward") Then

                                        If (dsCLPprog.Tables("CLPHeader").Rows(0)("IsJoiningRewardOnEnrollment")) Then
                                            txtBalancePoint.Text = dsCLPprog.Tables("CLPHeader").Rows(0)("ValueJoiningReward")

                                        ElseIf (CtrlLblStatusValue.Text.Equals(SpectrumCommon.RegistrationType.Registered.ToString())) Then
                                            txtBalancePoint.Text = dsCLPprog.Tables("CLPHeader").Rows(0)("ValueJoiningReward")
                                        End If
                                    End If

                                    If (CtrlLblStatusValue.Text.Equals(SpectrumCommon.RegistrationType.Registered.ToString())) Then
                                        messageType = SpectrumCommon.MessageType.CLPRegistration
                                    Else
                                        messageType = SpectrumCommon.MessageType.CLPEnrollment
                                    End If
                                End If
                            End If

                            If dsCLPprog IsNot Nothing AndAlso dsCLPprog.Tables.Contains("CLPHeader") Then
                                If dsCLPprog.Tables("CLPHeader").Rows.Count > 0 Then
                                    If dsCLPprog.Tables("CLPHeader").Rows(0)("IsJoiningReward") Then

                                        If (dsCLPprog.Tables("CLPHeader").Rows(0)("IsJoiningRewardOnEnrollment")) Then
                                            txtBalancePoint.Text = dsCLPprog.Tables("CLPHeader").Rows(0)("ValueJoiningReward")
                                            messageType = SpectrumCommon.MessageType.CLPEnrollment

                                        ElseIf (CtrlLblStatusValue.Text.Equals(SpectrumCommon.RegistrationType.Registered.ToString())) Then
                                            txtBalancePoint.Text = dsCLPprog.Tables("CLPHeader").Rows(0)("ValueJoiningReward")
                                            messageType = SpectrumCommon.MessageType.CLPRegistration
                                        End If
                                    End If
                                End If
                            End If
                            'If String.IsNullOrEmpty(txtAddress1.Text.Trim()) AndAlso String.IsNullOrEmpty(txtAddress2.Text.Trim()) AndAlso String.IsNullOrEmpty(txtAddress3.Text.Trim()) AndAlso String.IsNullOrEmpty(txtAddress4.Text.Trim()) AndAlso cboCity.SelectedItem Is Nothing AndAlso cboState.SelectedItem Is Nothing AndAlso cboCountry.SelectedItem Is Nothing AndAlso String.IsNullOrEmpty(txtPincode.Text.Trim()) Then
                            'Else
                            request.CLPCustomer.AddressList.Add(New CLPCustomerAddressDTO _
                                                                                            With {.AddressType = SpectrumCommon.AddressType.Residential, _
                                                                                                    .AddLine1 = txtAddress1.Text, _
                                                                                                    .AddLine2 = txtAddress2.Text, _
                                                                                                    .AddLine3 = txtAddress3.Text, _
                                                                                                    .AddLine4 = txtAddress4.Text, _
                                                                                                    .City = If(cboCity.SelectedItem Is Nothing, "", DirectCast(cboCity.SelectedItem, SpectrumCommon.AreaInfo).AreaCode),
                                                                                                    .State = If(cboState.SelectedItem Is Nothing, "", DirectCast(cboState.SelectedItem, SpectrumCommon.AreaInfo).AreaCode), _
                                                                                                    .Country = If(cboCountry.SelectedItem Is Nothing, "", DirectCast(cboCountry.SelectedItem, SpectrumCommon.AreaInfo).AreaCode), _
                                                                                                    .PinCode = txtPincode.Text})
                            'End If
                            If String.IsNullOrEmpty(txtAddress11.Text.Trim()) AndAlso String.IsNullOrEmpty(txtAddress12.Text.Trim()) AndAlso String.IsNullOrEmpty(txtAddress13.Text.Trim()) AndAlso String.IsNullOrEmpty(txtAddress14.Text.Trim()) AndAlso cboCity1.SelectedItem Is Nothing AndAlso cboState1.SelectedItem Is Nothing AndAlso cboCountry1.SelectedItem Is Nothing AndAlso String.IsNullOrEmpty(txtPincode1.Text.Trim()) Then
                            Else
                                request.CLPCustomer.AddressList.Add(New CLPCustomerAddressDTO _
                                                              With {.AddressType = SpectrumCommon.AddressType.Office, _
                                                                      .AddLine1 = txtAddress11.Text, _
                                                                      .AddLine2 = txtAddress12.Text, _
                                                                      .AddLine3 = txtAddress13.Text, _
                                                                      .AddLine4 = txtAddress14.Text, _
                                                                      .City = If(cboCity1.SelectedItem Is Nothing, "", DirectCast(cboCity1.SelectedItem, SpectrumCommon.AreaInfo).AreaCode), _
                                                                      .State = If(cboState1.SelectedItem Is Nothing, "", DirectCast(cboState1.SelectedItem, SpectrumCommon.AreaInfo).AreaCode), _
                                                                      .Country = If(cboCountry1.SelectedItem Is Nothing, "", DirectCast(cboCountry1.SelectedItem, SpectrumCommon.AreaInfo).AreaCode), _
                                                                      .PinCode = txtPincode1.Text})
                            End If



                            'Date.TryParse(dtDOB.Value.ToString(), request.CLPCustomer.BirthDate)
                            request.CLPCustomer.BirthDate = IIf(IsDBNull(dtpDOB.Value), DateTime.MinValue, dtpDOB.Value)
                            request.CLPCustomer.SpouseBirthDate = IIf(IsDBNull(dtpSpouseDOB.Value), DateTime.MinValue, dtpSpouseDOB.Value)
                            request.CLPCustomer.MarriageAnivDate = IIf(IsDBNull(dtpMarriageDate.Value), DateTime.MinValue, dtpMarriageDate.Value)

                            Decimal.TryParse(txtBalancePoint.Text, request.CLPCustomer.BalancePoints)

                            CustomerBL.SaveCustomerInfo(request)
                            If clsDefaultConfiguration.CustomerClassSelection Then
                                objHlthCustomer.InsertCustomerClassData(request.CLPCustomer.CardNumber, clsAdmin.CLPProgram, clsAdmin.SiteCode, clsAdmin.UserCode, cboCustomerClass.Text)
                            End If
                            'If (clsDefaultConfiguration.ClpSMSAllowed) Then
                            SendSMS2Customer(request.CLPCustomer, messageType)
                            'End If

                            ShowMessage(getValueByKey("CLPCustomerRegistrationMsg"), getValueByKey("CLAE04"))
                            txtGST.Text = ""
                            CustomerRequest = request
                            txtCustomerCode.Text = CustomerRequest.CLPCustomer.CardNumber
                            txtCustomerCode.ReadOnly = True
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                        End If
                    Else
                        SetClearFields()
                        AssignFields()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowMessage("Unable to load screen", getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub


    Private Sub AssignFields()
        txtCustomerCode.Text = CustomerResponse.CLPCustomer.CardNumber
        txtFirstName.Text = CustomerResponse.CLPCustomer.FirstName
        txtMiddleName.Text = CustomerResponse.CLPCustomer.MiddleName
        txtLastName.Text = CustomerResponse.CLPCustomer.LastName
        dtpDOB.Value = IIf(CustomerResponse.CLPCustomer.BirthDate = DateTime.MinValue, Nothing, CustomerResponse.CLPCustomer.BirthDate)

        txtTierType.Text = CustomerResponse.CLPCustomer.CardType
        cboGender.SelectedValue = CustomerResponse.CLPCustomer.Gender
        txtMobile.Text = CustomerResponse.CLPCustomer.MobileNo.Trim
        txtMobile.Enabled = False
        CtrlLblStatusValue.Text = CustomerResponse.CLPCustomer.RegistrationStatus
        txtEmail.Text = CustomerResponse.CLPCustomer.EmailId

        If (CustomerResponse.CLPCustomer.RegistrationStatus.Equals("Registered")) Then
            txtEmail.Enabled = False
        End If

        Dim RredeemptiononEnrollment As Boolean = False
        Dim passkeyallowedatpos As Boolean = False

        If dsCLPprog IsNot Nothing AndAlso dsCLPprog.Tables.Contains("CLPHeader") Then
            If dsCLPprog.Tables("CLPHeader").Rows.Count > 0 Then
                If dsCLPprog.Tables("CLPHeader").Rows(0)("IsRedemptionOnEnrollment") Then
                    RredeemptiononEnrollment = True
                End If

                If dsCLPprog.Tables("CLPHeader").Rows(0)("RedemptionApplicable") Then
                    If (CustomerResponse.CLPCustomer.RegistrationStatus = [Enum].GetName(GetType(SpectrumCommon.RegistrationType), SpectrumCommon.RegistrationType.Registered)) OrElse RredeemptiononEnrollment Then
                        '    btnPassKey.Enabled = True
                    Else
                        '   btnPassKey.Enabled = False
                    End If
                End If

                If dsCLPprog.Tables("CLPHeader").Rows(0)("IsPOSPasskey") Then
                    passkeyallowedatpos = True
                End If
            End If
        End If

        '   btnPassKey.Visible = passkeyallowedatpos

        If CustomerResponse.CLPCustomer.AddressList.Count > 0 Then
            txtAddress1.Text = CustomerResponse.CLPCustomer.AddressList(0).AddLine1
            txtAddress2.Text = CustomerResponse.CLPCustomer.AddressList(0).AddLine2
            txtAddress3.Text = CustomerResponse.CLPCustomer.AddressList(0).AddLine3
            txtAddress4.Text = CustomerResponse.CLPCustomer.AddressList(0).AddLine4
            cboCountry.SelectedValue = CustomerResponse.CLPCustomer.AddressList(0).Country
            cboState.SelectedValue = CustomerResponse.CLPCustomer.AddressList(0).State
            cboCity.SelectedValue = CustomerResponse.CLPCustomer.AddressList(0).City
            txtPincode1.Text = CustomerResponse.CLPCustomer.AddressList(0).PinCode
        End If

        If (Not String.IsNullOrEmpty(txtCustomerCode.Text.Trim())) Then
            _dtCustmInfo = objCustm.GetCustomerInformation("CLP", vSiteCode, clsAdmin.CLPProgram, txtCustomerCode.Text)

            If _dtCustmInfo IsNot Nothing AndAlso _dtCustmInfo.Rows.Count > 0 Then
                txtSwipeCard.Text = dtCustmInfo.Rows(0).Item("CUSTOMERNO")
                txtBalancePoint.Text = IIf(dtCustmInfo.Rows(0).Item("BalancePoint") Is DBNull.Value, 0, dtCustmInfo.Rows(0).Item("BalancePoint"))
                txtTotalPoints.Text = IIf(dtCustmInfo.Rows(0).Item("PointsAccumlated") Is DBNull.Value, 0, dtCustmInfo.Rows(0).Item("PointsAccumlated"))
            End If

            If (dtCustmInfo.Rows(0).Item("PhysicalCard") IsNot DBNull.Value AndAlso dtCustmInfo.Rows(0).Item("PhysicalCard")) Then
                txtMobile.ReadOnly = False
            Else
                txtMobile.ReadOnly = True
            End If
        End If

    End Sub


    Private Sub txtMobile_TextChanged(sender As Object, e As EventArgs) Handles txtMobile.TextChanged, txtEmail.TextChanged
        If Not String.IsNullOrEmpty(txtEmail.Text) AndAlso Not String.IsNullOrEmpty(txtMobile.Text) Then
            CtrlLblStatusValue.Text = getValueByKey([Enum].GetName(GetType(SpectrumCommon.RegistrationType), SpectrumCommon.RegistrationType.Registered))
        Else
            CtrlLblStatusValue.Text = getValueByKey([Enum].GetName(GetType(SpectrumCommon.RegistrationType), SpectrumCommon.RegistrationType.Enrolled))
        End If
    End Sub


    Private Sub txtEmail_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtEmail.Validating
        If txtEmail.Text <> "" Then
            If emailaddresscheck(txtEmail.Text) Then
                txtEmail.BackColor = Color.Green
            Else
                e.Cancel = True
                txtEmail.ErrorInfo.ErrorMessage = "Please provide correct email ID"
                txtEmail.ErrorInfo.ErrorMessageCaption = "Error"
                txtEmail.BackColor = Color.Red
            End If
        Else
            txtEmail.BackColor = Color.White
        End If
    End Sub
    Private Sub txtGST_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtGST.KeyDown
        If txtGST.ReadOnly = True Then
            Exit Sub
        Else
            If OldGstNo <> txtGST.Text.ToString Then
                If e.KeyCode = Keys.Enter Then
                    If txtGST.Text <> "" Then
                        If objCustm.CheckGStNoExist(txtGST.Text.Trim()) = True Then
                            ShowMessage("GST No. is already exist", "information")
                            Exit Sub
                        End If
                    Else
                        Exit Sub
                    End If
                End If
            End If
        End If
    End Sub
    'Code is added by irfan on 23/1/2018 for mantis issue
    'Private Sub txtMobile_Leave(sender As System.Object, e As System.EventArgs) Handles txtMobile.Leave
    '    If txtMobile.Text.Length > 10 Then
    '        ShowMessage("Phone numbers must be 10 digits only.", "Information")
    '        txtMobile.Text = ""
    '        txtMobile.Focus()
    '        Exit Sub
    '    End If
    '    If txtMobile.Text.Length <> 10 Then
    '        ShowMessage("Please Enter 10 digits Mobile Number only.", "Information")
    '        txtMobile.Text = ""
    '        txtMobile.Focus()
    '        Exit Sub
    '    End If
    '    If Not IsNumeric(txtMobile.Text) Then
    '        ShowMessage("Phone numbers  Must be Number.", "Information")
    '        txtMobile.Text = ""
    '        txtMobile.Focus()
    '        Exit Sub
    '    End If
    'End Sub

    'Private Sub txtOffPhone_Leave(sender As System.Object, e As System.EventArgs) Handles txtOffPhone.Leave
    '    If txtOffPhone.Text.Length > 10 Then
    '        ShowMessage("Office numbers must be 10 digits only.", "Information")
    '        txtOffPhone.Text = ""
    '        txtOffPhone.Focus()
    '        Exit Sub
    '    End If
    '    If txtOffPhone.Text.Length <> 10 Then
    '        ShowMessage("Please Enter 10 digits Office Number only.", "Information")
    '        txtOffPhone.Text = ""
    '        txtOffPhone.Focus()
    '        Exit Sub
    '    End If
    '    If Not IsNumeric(txtOffPhone.Text) Then
    '        ShowMessage("Office numbers  Must be Number.", "Information")
    '        txtOffPhone.Text = ""
    '        txtOffPhone.Focus()
    '        Exit Sub
    '    End If
    'End Sub

    'Private Sub txtResPhone_Leave(sender As System.Object, e As System.EventArgs) Handles txtResPhone.Leave
    '    If txtResPhone.Text.Length > 10 Then
    '        ShowMessage("Residence numbers must be 10 digits only.", "Information")
    '        txtResPhone.Text = ""
    '        txtResPhone.Focus()
    '        Exit Sub
    '    End If
    '    If txtResPhone.Text.Length <> 10 Then
    '        ShowMessage("Please Enter 10 digits Residence Number only.", "Information")
    '        txtResPhone.Text = ""
    '        txtResPhone.Focus()
    '        Exit Sub
    '    End If
    '    If Not IsNumeric(txtResPhone.Text) Then
    '        ShowMessage("Residence numbers  Must be Number.", "Information")
    '        txtResPhone.Text = ""
    '        txtResPhone.Focus()
    '        Exit Sub
    '    End If
    'End Sub
End Class