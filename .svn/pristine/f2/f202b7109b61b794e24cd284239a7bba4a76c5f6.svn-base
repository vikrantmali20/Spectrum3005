Imports SpectrumBL
Imports System.Data
Imports System.Data.DataSet
Imports System.Data.SqlClient
Imports SpectrumCommon
Imports System.Collections.Specialized

Public Class frmNSearchCustomer
    Dim objCustm As New clsCLPCustomer()
    Dim objcom As New clsCommon()
    Dim objHlthCustomer As New clsHealthCare
    Dim dtAddressType As New DataTable
    Dim dvAddressType As DataView
    Dim drAddress As DataRow
    Public _SOCustomer As Boolean = True
    Dim _CLPCustomer As Boolean = True
    Dim vSiteCode As String = clsAdmin.SiteCode
    Dim _dtCustmInfo As DataTable
    Dim dsCLPprog As DataSet
    Dim dtCustmData As DataTable
    Dim oldGSTNo As String

    Public Property dtCustmInfo() As DataTable
        Get
            Return _dtCustmInfo
        End Get
        Set(ByVal value As DataTable)
            _dtCustmInfo = value
        End Set
    End Property

    Dim _CustmType As String
    Public WriteOnly Property ShowCLP()
        Set(ByVal value)
            _CLPCustomer = value
        End Set
    End Property
    Public WriteOnly Property ShowSO()
        Set(ByVal value)
            _SOCustomer = value
        End Set
    End Property
    Public Property CustmType() As String
        Get
            Return _CustmType
        End Get
        Set(ByVal value As String)
            _CustmType = value
        End Set
    End Property

    Dim _CardType As String
    Private Property CardType() As String
        Get
            Return _CardType
        End Get
        Set(ByVal value As String)
            _CardType = value
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

    Private _CustomerMaster As GetCustomerMasterResponse
    Public Property CustomerMaster As GetCustomerMasterResponse
        Get
            Return _CustomerMaster
        End Get
        Set(value As GetCustomerMasterResponse)
            _CustomerMaster = value
        End Set
    End Property

    Private _CustomerGroup As GetCustomerMasterResponse
    Public Property CustomerGroup As GetCustomerMasterResponse
        Get
            Return _CustomerGroup
        End Get
        Set(value As GetCustomerMasterResponse)
            _CustomerGroup = value
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

    Private _AccessCustomerOutside As Boolean
    Public Property AccessCustomerOutside() As Boolean
        Get
            Return _AccessCustomerOutside
        End Get
        Set(ByVal value As Boolean)
            _AccessCustomerOutside = value
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

    Private _IsCustSearch As Boolean
    Public Property IsCustSearch() As Boolean
        Get
            Return _IsCustSearch
        End Get
        Set(ByVal value As Boolean)
            _IsCustSearch = value
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
    Private _isHashTagApplicable As Boolean
    Public Property isHashTagApplicable() As Boolean
        Get
            Return _isHashTagApplicable
        End Get
        Set(ByVal value As Boolean)
            _isHashTagApplicable = value
        End Set
    End Property

    Private _IsCustomerfromPhonePe As Boolean = False
    Public Property IsCustomerfromPhonePe() As Boolean
        Get
            Return _IsCustomerfromPhonePe
        End Get
        Set(ByVal value As Boolean)
            _IsCustomerfromPhonePe = value
        End Set
    End Property



    Private Sub BtnSearchCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchCustomer.Click

        'Try
        '    If RadioBtnCLPCustm.Checked = True Then
        '        _CustmType = "CLP"

        '        If clsDefaultConfiguration.IsManualCLPCustomerSearch = True Then
        '            CLP_Data.Sitecode = clsAdmin.SiteCode
        '            dsCLPprog = CLP_Data.getclpdata()
        '            Dim OnlyAtSiteofCreation As Boolean = False
        '            If dsCLPprog.Tables("CLPHeader") IsNot Nothing AndAlso dsCLPprog.Tables("CLPHeader").Rows.Count > 0 AndAlso dsCLPprog.Tables("CLPHeader").Rows(0)("ONLYATCREATEDSITE") Then
        '                OnlyAtSiteofCreation = True
        '            End If
        '            Dim objBrwClp As New frmNSearchCLPLookUp(CustmType, vSiteCode, String.Empty, OnlyAtSiteofCreation)
        '            objBrwClp.Text = getValueByKey("frmnsearchclplookupclp")
        '            objBrwClp.WindowState = FormWindowState.Maximized
        '            objBrwClp.ShowDialog()
        '            _dtCustmInfo = objBrwClp.dtCustmInfo
        '            _AddressType = objBrwClp.AddressType
        '            _CardType = objBrwClp.CardType
        '            txtEmailId.Enabled = True
        '            If dtCustmInfo IsNot Nothing Then
        '                SetCustomerInformationInForm(dtCustmInfo)
        '            End If
        '            'CboAddressTypeChanged(dtCustmInfo)
        '            CustomerResponse = CustomerBL.GetCustomerInfo(clsAdmin.CLPProgram, txtTelPhone.Text)
        '            Clearsfield()
        '            AssignFields()
        '            objBrwClp.Dispose()
        '            'Me.Close()
        '        Else
        '            MsgBox(getValueByKey("SC003"), , "SC003 - " & getValueByKey("CLAE04"))
        '        End If
        '    ElseIf RadioBtnSalesCustm.Checked = True Then
        '        _CustmType = "SO"
        '        Dim objBrwClp As New frmNSearchCLPLookUp(CustmType, vSiteCode, String.Empty)
        '        objBrwClp.Text = getValueByKey("frmnsearchclplookupothercust")
        '        objBrwClp.WindowState = FormWindowState.Maximized
        '        objBrwClp.ShowDialog()
        '        _dtCustmInfo = objBrwClp.dtCustmInfo
        '        _AddressType = objBrwClp.AddressType
        '        _CardType = objBrwClp.CardType
        '        objBrwClp.Dispose()
        '        If dtCustmInfo IsNot Nothing Then
        '            SetCustomerInformationInForm(dtCustmInfo)
        '        End If
        '    End If
        'Catch ex As Exception
        '    LogException(ex)
        'End Try

        Try
            Dim objCust As New frmSearchCustomer
            Dim result As DialogResult = objCust.ShowDialog()
            If result = Windows.Forms.DialogResult.Cancel Then
                BtnExit_Click(Nothing, Nothing)
                Exit Sub
            End If

            Dim dt As DataTable
            Dim objCustm As New clsCLPCustomer
            Dim dtCust As DataTable = objCust.dtCustmInfo()

            'Dim type As String = objCust.AddressType
            If Not dtCust Is Nothing AndAlso dtCust.Rows.Count > 0 Then
                SetCustomerInformationInForm(dtCust)

            ElseIf objCust.vCustomerNo <> String.Empty Then
                dt = objCustm.GetCustomerInformation("SO", clsAdmin.SiteCode, clsAdmin.CLPProgram, objCust.vCustomerNo)
                SetCustomerInformationInForm(dt)
            End If
            
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub txtCustomerCode_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtCustomerCode.PreviewKeyDown
        Try
            If (e.KeyCode = Keys.Enter) Then
                If Not (txtCustomerCode.Text = "") AndAlso Not (txtCustomerCode.Text = String.Empty) Then

                    If RadioBtnCLPCustm.Checked = True Then
                        _CustmType = "CLP"
                        _dtCustmInfo = objCustm.GetCustomerInformation("CLP", vSiteCode, clsAdmin.CLPProgram, txtCustomerCode.Text)

                    ElseIf RadioBtnSalesCustm.Checked = True Then
                        _CustmType = "SO"
                        _dtCustmInfo = objCustm.GetCustomerInformation("SO", vSiteCode, String.Empty, txtCustomerCode.Text)
                    End If

                    SetCustomerInformationInForm(dtCustmInfo)
                    CboAddressTypeChanged(dtCustmInfo)
                    'If _dtCustmInfo.Rows.Count > 0 Then
                    '    btnOk.Focus()
                    'End If
                End If
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub txtSwipeCard_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtSwipeCard.PreviewKeyDown
        Try
            If (e.KeyCode = Keys.Enter) Then
                If Not (txtSwipeCard.Text = "") AndAlso Not (txtSwipeCard.Text = String.Empty) Then
                    _dtCustmInfo = objCustm.GetCustomerInformation(CustmType, vSiteCode, clsAdmin.CLPProgram, txtSwipeCard.Text)

                    SetCustomerInformationInForm(dtCustmInfo)
                    CboAddressTypeChanged(dtCustmInfo)
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function SetCustomerInformationInForm(ByVal dtCustmInfo As DataTable) As Boolean
        Try
            If dtCustmInfo IsNot Nothing AndAlso dtCustmInfo.Rows.Count > 0 Then

                If (CustmType = "SO") Then
                    EnableOrDisableFields(False)
                End If

                txtCustomerCode.Text = dtCustmInfo.Rows(0).Item("CUSTOMERNO").ToString()
                txtCustomerName.Text = dtCustmInfo.Rows(0).Item("FIRSTNAME").ToString()
                'txtCustomerAddress.Text = dtCustmInfo.Rows(0).Item("ADDRESS").ToString()
                'txtCity.Text = dtCustmInfo.Rows(0).Item("CITY")
                'txtZipCode.Text = dtCustmInfo.Rows(0).Item("PINCODE").ToString()
                txtTelPhone.Text = dtCustmInfo.Rows(0).Item("Mobileno").ToString()
                txtEmailId.Text = dtCustmInfo.Rows(0).Item("EMAILID").ToString()

                If (dtCustmInfo.Columns.Contains("RegistrationStatus") AndAlso dtCustmInfo.Rows(0).Item("RegistrationStatus").ToString().Equals("Registered")) Then
                    txtEmailId.Enabled = False
                End If

                txtMiddleName.Text = dtCustmInfo.Rows(0).Item("MiddleName").ToString()
                txtLastName.Text = dtCustmInfo.Rows(0).Item("SURNAME").ToString()
                'dtDOB.Value = Convert.ToDateTime(dtCustmInfo.Rows(0).Item("BirthDate").ToString("dd-MM-yyyy"))
                'Dim birth As String = dtCustmInfo.Rows(0).Item("BirthDate").ToString()
                'Dim birthday As Date = Date.Parse(birth)
                'dtDOB.Value = birthday

                dtDOB.Value = dtCustmInfo.Rows(0).Item("BirthDate").ToString()
                'Dim birthday As String = dtCustmInfo.Rows(0).Item("BirthDate").ToString
                'dtDOB.Value = Convert.ToDateTime(birthday)
                If clsDefaultConfiguration.EvasPizzaChanges = True Then
                    txtReminderComment.Text = dtCustmInfo.Rows(0).Item("ReminderComments").ToString()
                End If
                cboGender.SelectedItem = dtCustmInfo.Rows(0).Item("Gender").ToString()
                CtrlLblStatusValue.Text = dtCustmInfo.Rows(0).Item("RegistrationStatus").ToString()
                CtrlCombo1.Text = dtCustmInfo.Rows(0).Item("Level").ToString()
                cboCustomerGroup.Text = dtCustmInfo.Rows(0).Item("CustomerGroup").ToString()             'Code Added by irfan for Mantis issue on 25/1/2018
                If clsDefaultConfiguration.IsNewMembership Then
                    CtrlCombo2.Text = dtCustmInfo.Rows(0).Item("CARDTYPE").ToString()
                End If
                If CtrlLblStatusValue.Text = "Enrolled" Then
                    btnPassKey.Enabled = False
                ElseIf CtrlLblStatusValue.Text = "Registered" Then
                    btnPassKey.Enabled = True
                End If

                '---- Added By Mahesh in case flag AllowMobileNoEditable is set true 
                If clsDefaultConfiguration.AllowMobileNoEditable Then
                    txtTelPhone.Enabled = True
                Else
                    txtTelPhone.Enabled = False
                End If

                If _CustmType = "CLP" Then
                    _CardType = dtCustmInfo.Rows(0).Item("CARDTYPE").ToString()
                    txtSwipeCard.Text = dtCustmInfo.Rows(0).Item("CUSTOMERNO").ToString()
                    txtBalancePoint.Text = dtCustmInfo.Rows(0).Item("BalancePoint").ToString()
                    txtTotalPoints.Text = dtCustmInfo.Rows(0).Item("PointsAccumlated").ToString()
                    txtResidenceNumber.Text = dtCustmInfo.Rows(0).Item("RESPHONE").ToString()
                    ShowCLP = True
                    ShowSO = False
                End If

                txtCustomerAddress.Text = dtCustmInfo.Rows(0).Item("AddressLn1").ToString()
                txtAddress2.Text = dtCustmInfo.Rows(0).Item("AddressLn2").ToString()
                txtAddress3.Text = dtCustmInfo.Rows(0).Item("AddressLn3").ToString()
                txtAddress4.Text = dtCustmInfo.Rows(0).Item("AddressLn4").ToString()
                cboCountry.SelectedValue = dtCustmInfo.Rows(0).Item("CountryCode").ToString()
                cboState.SelectedValue = dtCustmInfo.Rows(0).Item("StateCode").ToString()
                cboCity.SelectedValue = dtCustmInfo.Rows(0).Item("CityCode").ToString()
                txtZipCode.Text = dtCustmInfo.Rows(0).Item("Pincode").ToString()
                'code added by irfan for GST NO on 21-07-2017
                txtGST.Text = dtCustmInfo.Rows(0).Item("GSTNo").ToString()
                oldGSTNo = txtGST.Text
                If clsDefaultConfiguration.CustomerClassSelection Then
                    If Not IsDBNull(dtCustmInfo.Rows(0).Item("CUSTOMERNO").ToString()) Then
                        Dim dtCustClass As DataTable = objHlthCustomer.GetCustomerClassDataByCustomerNo(dtCustmInfo.Rows(0).Item("CUSTOMERNO").ToString(), clsAdmin.CLPProgram, clsAdmin.SiteCode)
                        If Not dtCustClass Is Nothing AndAlso dtCustClass.Rows.Count > 0 Then
                            If Not IsDBNull(dtCustClass.Rows(0)("Classify")) Then
                                cboCustomerClass.Text = dtCustClass.Rows(0)("Classify")
                                If dtCustClass.Rows(0)("Classify") = "Both" Then
                                    cboCustomerClass.Enabled = False
                                End If
                            End If
                        End If
                    End If
                End If
                If (CustmType = "SO") Then
                    EnableOrDisableFields(True)
                End If
            Else
                Clearsfield()
                ShowMessage(getValueByKey("SC001"), "SC001 - " & getValueByKey("CLAE04"))
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    ''' <summary>
    ''' Changed Customer Address 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub cboAddressType_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAddressType.SelectedValueChanged
    '    If Not (cboAddressType.Text Is String.Empty) Then
    '        dvAddressType = New DataView(dtCustmInfo, "AddressType='" & cboAddressType.SelectedValue & "'", "", DataViewRowState.CurrentRows)
    '        dtAddressType = dvAddressType.ToTable
    '        SetCustomerAddressInfo(dtAddressType)
    '    End If
    'End Sub

    Private Function SetCustomerAddressInfo(ByVal dtCustmTemp As DataTable) As Boolean
        Try
            If Not (dtCustmTemp Is Nothing) AndAlso dtCustmTemp.Rows.Count > 0 Then

                txtCustomerAddress.Text = dtCustmTemp.Rows(0).Item("Address")
                'txtCity.Text = dtCustmTemp.Rows(0).Item("City")
                txtZipCode.Text = dtCustmTemp.Rows(0).Item("PinCode")
                txtTelPhone.Text = dtCustmTemp.Rows(0).Item("ResPhone").ToString
                txtEmailId.Value = dtCustmTemp.Rows(0).Item("EmailId").ToString

                If (dtCustmInfo.Columns.Contains("RegistrationStatus") AndAlso dtCustmInfo.Rows(0).Item("RegistrationStatus").ToString().Equals("Registered")) Then
                    txtEmailId.Enabled = False
                End If
            Else
                txtCustomerAddress.Text = ""
                'txtCity.Text = ""
                txtZipCode.Text = ""
                txtTelPhone.Text = ""
                txtEmailId.Text = ""
                cboAddressType.SelectedIndex = -1
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("SC002"), "SC002 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Set Address Type values in Combo Box 
    ''' </summary>
    ''' <param name="dtCombo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CboAddressTypeChanged(ByRef dtCombo As DataTable)
        Try
            cboAddressType.DataSource = dtCombo
            cboAddressType.ValueMember = dtCombo.Columns("AddressType").ColumnName
            cboAddressType.DisplayMember = dtCombo.Columns("AddressTypeName").ColumnName
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

        Return ""
    End Function

    Public Sub setAddressAutoComplete() '--ByRef dtCombo As DataTable)
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

    Private Sub BtnCreateNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCreateNew.Click
        Try
            Dim objCreateNewCustm As New frmNSOCustomer
            objCreateNewCustm.ShowDialog()
            objCreateNewCustm.Dispose()

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try
            '_AddressType = cboAddressType.SelectedValue
            '' added by ketan old Customer issue for MOD Client 
            If Not cboAddressType.SelectedValue Is DBNull.Value Then
                _AddressType = cboAddressType.SelectedValue
            End If
            If RadioBtnCLPCustm.Checked AndAlso dsCLPprog IsNot Nothing AndAlso dsCLPprog.Tables("CLPHeader") Is Nothing Then
                ShowMessage(False, getValueByKey("frmnsearchcustomer.noclpprogram"), getValueByKey("CLAE04"))
                Return
            ElseIf RadioBtnSalesCustm.Checked Then
                Me.Close()
                Return
            End If

            If Not ValidateData() Then
                Exit Sub
            End If
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
                    CustomerRequest.CLPCustomer.AddressList = CustomerResponse.CLPCustomer.AddressList
                    CustomerRequest.CreatedAt = clsAdmin.SiteCode
                    CustomerRequest.CreatedBy = clsAdmin.UserCode
                End If
                CustomerRequest.CLPCustomer.FirstName = txtCustomerName.Text
                CustomerRequest.CLPCustomer.MiddleName = txtMiddleName.Text
                CustomerRequest.CLPCustomer.LastName = txtLastName.Text
                CustomerRequest.CLPCustomer.GSTNo = txtGST.Text.Trim
                CustomerRequest.CLPCustomer.CustomerGroup = cboCustomerGroup.Text

                Dim SendSMSRequired As Boolean = False
                If (dsCLPprog IsNot Nothing AndAlso dsCLPprog.Tables("CLPHeader").Rows.Count > 0 AndAlso Not dsCLPprog.Tables("CLPHeader").Rows(0)("IsJoiningRewardOnEnrollment")) Then
                    If (CustomerResponse.CLPCustomer.RegistrationStatus.Equals("Enrolled")) AndAlso CtrlLblStatusValue.Text.Equals("Registered") Then
                        CustomerRequest.CLPCustomer.BalancePoints = Val(dsCLPprog.Tables("CLPHeader").Rows(0)("ValueJoiningReward").ToString())
                        CustomerRequest.CLPCustomer.PointsAccumlated = Val(dsCLPprog.Tables("CLPHeader").Rows(0)("ValueJoiningReward").ToString())
                        CustomerRequest.CLPCustomer.IsJoiningPointAccumlated = True
                        SendSMSRequired = True
                    End If
                End If


                If IsDBNull(dtDOB.Value) = False AndAlso dtDOB.Value IsNot Nothing Then CustomerRequest.CLPCustomer.BirthDate = dtDOB.Value
                If clsDefaultConfiguration.EvasPizzaChanges = True Then
                    CustomerRequest.CLPCustomer.ReminderComment = txtReminderComment.Text
                End If
                'CustomerRequest.CLPCustomer.CustomerGroup = cboCustomerGroup.SelectedValue
                CustomerRequest.CLPCustomer.Gender = cboGender.SelectedItem
                '   CustomerRequest.CLPCustomer.CardType = txtTierType.Text 'cmbTiers.SelectedValue
                If clsDefaultConfiguration.IsNewMembership = True Then
                    CustomerRequest.CLPCustomer.CardType = CtrlCombo2.Text
                Else
                    CustomerRequest.CLPCustomer.CardType = txtTierType.Text
                End If

                CustomerRequest.CLPCustomer.MobileNo = txtTelPhone.Text.Trim

                If (Not String.IsNullOrEmpty(CustomerResponse.CLPCustomer.EmailId)) Then
                    CustomerRequest.CLPCustomer.EmailId = CustomerResponse.CLPCustomer.EmailId
                    CustomerRequest.CLPCustomer.RegistrationStatus = CustomerResponse.CLPCustomer.RegistrationStatus
                Else
                    CustomerRequest.CLPCustomer.EmailId = txtEmailId.Text
                    CustomerRequest.CLPCustomer.RegistrationStatus = CtrlLblStatusValue.Text
                End If

                CustomerRequest.CLPCustomer.ResidenceNumber = txtResidenceNumber.Text
                CustomerRequest.CLPCustomer.Level = CtrlCombo1.Text

                If CustomerRequest.CLPCustomer.AddressList.Count > 0 Then
                    CustomerRequest.CLPCustomer.AddressList(0).AddressType = SpectrumCommon.AddressType.Residential
                    CustomerRequest.CLPCustomer.AddressList(0).AddLine1 = txtCustomerAddress.Text
                    CustomerRequest.CLPCustomer.AddressList(0).AddLine2 = txtAddress2.Text
                    CustomerRequest.CLPCustomer.AddressList(0).AddLine3 = txtAddress3.Text
                    CustomerRequest.CLPCustomer.AddressList(0).AddLine4 = txtAddress4.Text
                    CustomerRequest.CLPCustomer.AddressList(0).Country = cboCountry.SelectedValue
                    CustomerRequest.CLPCustomer.AddressList(0).State = cboState.SelectedValue
                    CustomerRequest.CLPCustomer.AddressList(0).City = cboCity.SelectedValue
                    CustomerRequest.CLPCustomer.AddressList(0).PinCode = txtZipCode.Text
                End If
                CustomerBL.SaveCustomerInfo(CustomerRequest)
                If clsDefaultConfiguration.CustomerClassSelection Then
                    objHlthCustomer.UpdateCustomerClassData(CustomerRequest.CLPCustomer.CardNumber, clsAdmin.CLPProgram, clsAdmin.SiteCode, clsAdmin.UserCode, cboCustomerClass.Text)
                End If
                If SendSMSRequired Then
                    CustomerResponse.CLPCustomer.MessageType = MessageType.CLPRegistration
                    SendSMS2Customer(CustomerRequest.CLPCustomer, MessageType.CLPRegistration)
                End If

                'ShowMessage(getValueByKey("CLPCustomerRegistrationMsg"), getValueByKey("CLAE04"))
                _dtCustmInfo = objCustm.GetCustomerInformation("CLP", vSiteCode, clsAdmin.CLPProgram, txtCustomerCode.Text, IsNewSalesOrder:=clsDefaultConfiguration.IsNewSalesOrder)

                'ShowMessage(getValueByKey("CLPCustomerRegistrationMsg"), getValueByKey("CLAE04"))

            Else
                txtMobileNo_KeyDown(sender, New KeyEventArgs(Keys.Enter))

                If (Not String.IsNullOrEmpty(txtCustomerCode.Text.Trim())) Then
                    '   _dtCustmInfo = objCustm.GetCustomerInformation("CLP", vSiteCode, clsAdmin.CLPProgram, txtCustomerCode.Text)
                    _dtCustmInfo = objCustm.GetCustomerInformation("CLP", vSiteCode, clsAdmin.CLPProgram, txtCustomerCode.Text, False, False, "", "0", clsDefaultConfiguration.IsNewSalesOrder) 'vipin 16.05.2018 PC Changes merge
                End If
            End If

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()

        Catch ex As Exception
            ShowMessage("Error in saving data", getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            _CustmType = ""
            _AddressType = ""
            _dtCustmInfo = Nothing
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub


    Private Sub RadioBtnCLPCustm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioBtnCLPCustm.Click
        Try
            RadioBtnCLPCustm.Checked = True
            RadioBtnSalesCustm.Checked = False
            _CustmType = "CLP"

            'LbCustomer.Text = "CLP No."
            If getValueByKey("frmnsearchcustomer.lbcustomer") <> String.Empty Then
                lbCustomer.Text = getValueByKey("frmnsearchcustomer.lbcustomer")
            End If
            'sizforCLP.Visible = True
            lblSwipeCard.Visible = True
            txtSwipeCard.Visible = True
            lblBalancePoint.Visible = True
            txtBalancePoint.Visible = True
            CtrLblStatus.Visible = True
            CtrlLblStatusValue.Visible = True
            CtrlLbltiers.Visible = True
            txtTierType.Visible = True

            btnPassKey.Visible = True
            btnPassKey.Enabled = False
            BtnCreateNew.Enabled = False
            EnableOrDisableFields(False)

            Clearsfield()
            CLP_Data.Sitecode = clsAdmin.SiteCode
            dsCLPprog = CLP_Data.getclpdata()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub RadioBtnSalesCustm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioBtnSalesCustm.Click
        Try
            RadioBtnCLPCustm.Checked = False
            RadioBtnSalesCustm.Checked = True
            _CustmType = "SO"

            'lbCustomer.Text = "Customer No."
            lbCustomer.Text = getValueByKey("frmnsearchcustomer.lbcustomer1")
            'sizforCLP.Visible = False
            lblSwipeCard.Visible = False
            txtSwipeCard.Visible = False
            lblBalancePoint.Visible = False
            txtBalancePoint.Visible = False
            CtrLblStatus.Visible = False
            CtrlLblStatusValue.Visible = False
            CtrlLbltiers.Visible = False
            txtTierType.Visible = False
            EnableOrDisableFields(True)
            btnPassKey.Visible = False
            btnPassKey.Enabled = False

            BtnCreateNew.Enabled = True

            Clearsfield()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function Clearsfield(Optional ByVal isFastRegisteration As Boolean = False) As Boolean
        Try
            cboAddressType.DataSource = Nothing
            txtCustomerCode.Text = ""
            txtCustomerName.Text = ""
            txtCustomerAddress.Text = ""
            'txtCity.Text = ""
            txtZipCode.Text = ""
            If isFastRegisteration = False Then
                txtTelPhone.Text = ""
            End If
            txtEmailId.Text = ""
            txtSwipeCard.Text = ""
            txtBalancePoint.Text = ""
            txtTotalPoints.Text = ""
            txtMiddleName.Text = ""
            txtLastName.Text = ""
            'txtTierType.Text = ""
            dtDOB.Value = Nothing
            cboGender.SelectedIndex = -1
            'cboCustomerGroup.SelectedIndex = -1
            cboCountry.SelectedIndex = -1
            cboCountry.SelectedIndex = -1
            'cboState.SelectedIndex = -1
            'cboCity.SelectedIndex = -1
            txtAddress2.Text = ""
            txtAddress3.Text = ""
            txtCustomerCode.Focus()
            txtTelPhone.Enabled = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Sub frmSearchCustomer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If clsDefaultConfiguration.EvasPizzaChanges = True Then
                cboCustomerGroup.Visible = False
                lblCustomerGroup.Visible = False
                lblReminderComment.Text = "Customer Instructions"
                txtReminderComment.Visible = True
            Else
                lblReminderComment.Visible = False
                txtReminderComment.Visible = False
                cboCustomerGroup.Visible = True
                lblCustomerGroup.Visible = True
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            CustomerMaster = CustomerBL.GetCustomerMaster()
            CustomerGroup = CustomerBL.GetCustomerGroup()                            'Code Added by irfan on 25/1/2018 for mantis issues
            If clsDefaultConfiguration.CustomerClassSelection Then
                Dim dtHCustclassData As New DataTable
                dtHCustclassData = objHlthCustomer.GetCustomerClassData()
                PopulateComboBox(dtHCustclassData, cboCustomerClass)
                pC1ComboSetDisplayMember(cboCustomerClass)
            End If
            InitializeComboBox()
            If _CLPCustomer = False Then
                RadioBtnCLPCustm.Enabled = False
                RadioBtnSalesCustm.Checked = True
                BtnCreateNew.Enabled = True
                btnPassKey.Visible = False
                btnPassKey.Enabled = False
            End If

            If _SOCustomer = False Then
                ' RadioBtnSalesCustm.Text = "Other Customer"
                RadioBtnSalesCustm.Text = getValueByKey("frmnsocustomer")
                RadioBtnSalesCustm.Enabled = False
                BtnCreateNew.Enabled = False
                RadioBtnCLPCustm.Checked = True
                btnPassKey.Visible = True
                '---- Changed by Mahesh in Case of CLP Customer this passkey button must be visible and enable ...
                'btnPassKey.Enabled = False
                btnPassKey.Enabled = True
                CtrlLbltiers.Visible = True

                'cmbTiers.Visible = True
                CLP_Data.Sitecode = clsAdmin.SiteCode
                dsCLPprog = CLP_Data.getclpdata()

                If dsCLPprog IsNot Nothing AndAlso dsCLPprog.Tables("CLPHeader") IsNot Nothing AndAlso dsCLPprog.Tables("CLPHeader").Rows.Count > 0 Then
                    Dim tierslist = CLP_Data.GetCLPTiers(dsCLPprog.Tables("CLPHeader")(0)("CLPPROGRAMID"))
                    If tierslist.Count > 0 Then

                        txtTierType.Text = tierslist(0)
                        'cmbTiers.DataSource = tierslist
                        'cmbTiers.SelectedIndex = 0
                    End If
                End If
            End If

            RadioBtnSalesCustm.Visible = clsDefaultConfiguration.IsOtherCustomerRequired
            RadioBtnSalesCustm.Enabled = clsDefaultConfiguration.IsOtherCustomerRequired
            BtnCreateNew.Visible = clsDefaultConfiguration.IsOtherCustomerRequired

            RadioBtnCLPCustm.Enabled = RadioBtnSalesCustm.Visible
            If objcom.GetBoLevelChanges() = True Then
                CtrlCombo1.Visible = True
                CtrlLabel2.Visible = True
                Dim level As DataTable
                level = objcom.GetLevels()
                PopulateComboBox(level, CtrlCombo1)
                pC1ComboSetDisplayMember(CtrlCombo1)
            Else
                CtrlCombo1.Visible = False
                CtrlLabel2.Visible = False
            End If

            If clsDefaultConfiguration.IsNewMembership Then
                Dim CardT As DataTable
                CardT = objcom.GetClpProgramType(clsAdmin.SiteCode)
                PopulateComboBox(CardT, CtrlCombo2)
                pC1ComboSetDisplayMember(CtrlCombo2)
            End If

            If clsDefaultConfiguration.IsManualCLPCustomerSearch = False Then
                ' BtnSearchCustomer.Enabled = False
                txtCustomerCode.ReadOnly = True
            End If
            If Not clsDefaultConfiguration.CustomerClassSelection Then
                lblCustomerClass.Visible = False
                cboCustomerClass.Visible = False
            End If
            SetCulture(Me, Me.Name)
            RadioBtnCLPCustm.TabStop = False
            _CustmType = "CLP"
            txtCustomerCode.Enabled = False
            If (AccessCustomerOutside) Then
                HideOtherCustomerFields(CustomerNo)
            End If
            Call setAddressAutoComplete()
            '----- Added By Mahesh for changing flow landed to search Screen 
            If IsCustSearch Then Call BtnSearchCustomer_Click(BtnSearchCustomer, New System.EventArgs)

            If SearchedValue <> Nothing Then
                If IsNumeric(SearchedValue.ToString()) Then
                    txtTelPhone.Text = SearchedValue

                Else
                    txtCustomerName.Text = SearchedValue
                End If
            End If
            txtGST.Visible = True
            lblGST.Visible = True
            txtTelPhone.Select()
            txtTelPhone.Select(txtTelPhone.Text.Length, 0)
            If clsDefaultConfiguration.IsNewMembership Then
                CtrlCombo2.Visible = True
                CtrlLbltiers.Text = "Membership Type"
                txtTierType.Visible = False
            Else
                CtrlLbltiers.Text = "Tiers"
                CtrlCombo2.Visible = False
                txtTierType.Visible = True
            End If

            If clsDefaultConfiguration.IsNewMembership Then
                If (Not String.IsNullOrEmpty(CustomerNo)) Then
                    CtrlCombo2.ReadOnly = True
                Else
                    CtrlCombo2.ReadOnly = False
                End If
            End If
            If isHashTagApplicable = True Then
                BtnOK_Click(Nothing, Nothing)
                ' Me.DialogResult = Windows.Forms.DialogResult.OK
                '  Me.Close()
            End If
        Catch ex As Exception
            Me.Close()
            ShowMessage("Unable to load screen", getValueByKey("CLAE05"))
        End Try
    End Sub

    Private Sub InitializeComboBox()
        cboGender.DataSource = New List(Of String) From {"Male", "Female"}
        cboGender.SelectedIndex = -1
        cboCustomerGroup.DisplayMember = "GroupName"
        cboCustomerGroup.ValueMember = "GroupCode"
        cboCustomerGroup.DataSource = CustomerGroup.CustomerGroupList
        cboCustomerGroup.SelectedValue = ""
        'cboAddressType.DisplayMember = "AddressTypeName"
        'cboAddressType.ValueMember = "AddressTypeCode"
        'cboAddressType.DataSource = CustomerMaster.AddressTypeList        
        cboCountry.DisplayMember = "AreaName"
        cboCountry.ValueMember = "AreaCode"
        cboCountry.DataSource = CustomerMaster.AreaInfoList.Where(Function(x) x.AreaType = AreaType.Country).ToList()
        cboCountry.SelectedValue = ""
        'cboState.SelectedValue = ""
        'cboCity.SelectedValue = ""
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

    Private Sub txtMobileNo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTelPhone.KeyDown
        Try
            Dim telPhone As String
            telPhone = Replace(txtTelPhone.Text.Trim, "'", "")
            If e.KeyCode = Keys.Enter AndAlso String.IsNullOrEmpty(txtTelPhone.Text) = False Then
                If Not ValidateData() Then
                    Exit Sub
                End If
                CustomerResponse = CustomerBL.GetCustomerInfo(clsAdmin.CLPProgram, txtCustomerCode.Text, KeyIsMobileNo:=False)
                If CustomerResponse.CLPCustomer Is Nothing Then
                    CustomerResponse = CustomerBL.GetCustomerInfo(clsAdmin.CLPProgram, txtTelPhone.Text, KeyIsMobileNo:=True)
                    If CustomerResponse.CLPCustomer Is Nothing Then

                        Dim eventType As Int32 = 1
                        '''' message commented by Mahesh as per pooja/Rakesh instruction ..
                        '   ShowMessage("Customer does not exist. Do you want to register a new customer ?", getValueByKey("CLAE04"), eventType, True, getValueByKey("mod009"))
                        If eventType = 1 Then
                            Dim messageType As MessageType
                            Dim GroupName As String

                            If cboCustomerGroup.Text <> "" Then
                                GroupName = DirectCast(cboCustomerGroup.SelectedItem, SpectrumCommon.CustomerGroupDetails).GroupName
                            Else
                                GroupName = ""
                            End If
                            Dim request As New SaveCustomerRequest With {.CreatedAt = clsAdmin.SiteCode, .CreatedBy = clsAdmin.UserCode}
                            '  request.CLPCustomer = New CLPCustomerDTO() With {.MobileNo = telPhone, .ResidenceNumber = txtResidenceNumber.Text, .SiteCode = clsAdmin.SiteCode, .CustomerGroup = IIf(cboCustomerGroup.SelectedItem Is Nothing, "", GroupName.ToString), .CreatedAt = clsAdmin.SiteCode, .CreatedBy = clsAdmin.UserCode, .EmailId = txtEmailId.Text, .FirstName = txtCustomerName.Text, .Gender = If(cboGender.SelectedItem Is Nothing, "", cboGender.SelectedItem.ToString()), .LastName = txtLastName.Text, .MiddleName = txtMiddleName.Text, .RegistrationStatus = CtrlLblStatusValue.Text, .CardType = txtTierType.Text, .ReminderComment = txtReminderComment.Text, .GSTNo = txtGST.Text.Trim(), .Level = CtrlCombo1.Text}
                            If clsDefaultConfiguration.IsNewMembership Then
                                request.CLPCustomer = New CLPCustomerDTO() With {.MobileNo = telPhone, .ResidenceNumber = txtResidenceNumber.Text, .SiteCode = clsAdmin.SiteCode, .CustomerGroup = IIf(cboCustomerGroup.SelectedItem Is Nothing, "", GroupName.ToString), .CreatedAt = clsAdmin.SiteCode, .CreatedBy = clsAdmin.UserCode, .EmailId = txtEmailId.Text, .FirstName = txtCustomerName.Text, .Gender = If(cboGender.SelectedItem Is Nothing, "", cboGender.SelectedItem.ToString()), .LastName = txtLastName.Text, .MiddleName = txtMiddleName.Text, .RegistrationStatus = CtrlLblStatusValue.Text, .CardType = IIf(CtrlCombo2.Text Is Nothing, "GOLD", CtrlCombo2.Text), .ReminderComment = txtReminderComment.Text, .GSTNo = txtGST.Text.Trim(), .Level = CtrlCombo1.Text}
                            Else
                                request.CLPCustomer = New CLPCustomerDTO() With {.MobileNo = telPhone, .ResidenceNumber = txtResidenceNumber.Text, .SiteCode = clsAdmin.SiteCode, .CustomerGroup = IIf(cboCustomerGroup.SelectedItem Is Nothing, "", GroupName.ToString), .CreatedAt = clsAdmin.SiteCode, .CreatedBy = clsAdmin.UserCode, .EmailId = txtEmailId.Text, .FirstName = txtCustomerName.Text, .Gender = If(cboGender.SelectedItem Is Nothing, "", cboGender.SelectedItem.ToString()), .LastName = txtLastName.Text, .MiddleName = txtMiddleName.Text, .RegistrationStatus = CtrlLblStatusValue.Text, .CardType = txtTierType.Text, .ReminderComment = txtReminderComment.Text, .GSTNo = txtGST.Text.Trim(), .Level = CtrlCombo1.Text}
                            End If
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
                            request.CLPCustomer.AddressList.Add(New CLPCustomerAddressDTO With {.AddressType = SpectrumCommon.AddressType.Residential, .AddLine1 = txtCustomerAddress.Text, .AddLine2 = txtAddress2.Text, .AddLine3 = txtAddress3.Text, .AddLine4 = txtAddress4.Text, .City = If(cboCity.SelectedItem Is Nothing, "", DirectCast(cboCity.SelectedItem, SpectrumCommon.AreaInfo).AreaCode), .State = If(cboState.SelectedItem Is Nothing, "", DirectCast(cboState.SelectedItem, SpectrumCommon.AreaInfo).AreaCode), .Country = If(cboCountry.SelectedItem Is Nothing, "", DirectCast(cboCountry.SelectedItem, SpectrumCommon.AreaInfo).AreaCode), .PinCode = txtZipCode.Text})

                            'Date.TryParse(dtDOB.Value.ToString(), request.CLPCustomer.BirthDate)
                            request.CLPCustomer.BirthDate = IIf(IsDBNull(dtDOB.Value), DateTime.MinValue, dtDOB.Value)
                            Decimal.TryParse(txtBalancePoint.Text, request.CLPCustomer.BalancePoints)

                            CustomerBL.SaveCustomerInfo(request)
                            If clsDefaultConfiguration.CustomerClassSelection Then
                                objHlthCustomer.InsertCustomerClassData(request.CLPCustomer.CardNumber, clsAdmin.CLPProgram, clsAdmin.SiteCode, clsAdmin.UserCode, cboCustomerClass.Text)
                            End If
                            'If (clsDefaultConfiguration.ClpSMSAllowed) Then
                            SendSMS2Customer(request.CLPCustomer, messageType)
                            'End If

                            If IsCustomerfromPhonePe = False Then   'Jayesh (PhonePe do not show data save popup)
                                ShowMessage(getValueByKey("CLPCustomerRegistrationMsg"), getValueByKey("CLAE04"))
                            End If



                            If (request.CLPCustomer.CardNumber IsNot Nothing AndAlso Not String.IsNullOrEmpty(request.CLPCustomer.CardNumber.ToString())) Then
                                _dtCustmInfo = objCustm.GetCustomerInformation("CLP", vSiteCode, clsAdmin.CLPProgram, request.CLPCustomer.CardNumber)
                            End If

                            CustomerRequest = request
                            txtCustomerCode.Text = CustomerRequest.CLPCustomer.CardNumber
                            txtCustomerCode.ReadOnly = True
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                        End If
                    Else
                        Clearsfield(True)
                        AssignFields()
                    End If
                End If
            End If

        Catch ex As Exception
            ShowMessage("Unable to load screen", getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Delegate Sub SendSMSToClpCustomer(ByVal passKey As String, ByVal points As Integer, ByVal mobile As String)

    Public Function SendSMStoCustomer(ByVal passKey As String, ByVal clpPoints As String, ByVal mobileNo As String) As Boolean
        Try
            Dim messageText As String = String.Empty

            If (String.IsNullOrEmpty(passKey)) Then
                messageText = String.Format("Hi, thanks for Registering with MOD CLP, now you can reedeem the MOD $ in you’re a\\c across MOD stores (except airport Kiosks), your current MOD A\\c balance is {0}", clpPoints.Trim())
            Else
                messageText = String.Format("Hi, thanks for opting to reedeem {0} points, your passkey is {1}, announce this at your nearest MOD Store to get your goodies!!", clpPoints.Trim(), passKey.Trim())
            End If

            Dim postData As String = clsDefaultConfiguration.SMSUrlParameters
            postData = postData.Replace("$number", mobileNo.Trim)
            postData = postData.Replace("$msg", messageText)

            Dim sendSMSUrl As String = String.Format("{0}?{1}", clsDefaultConfiguration.SMSUrl, postData)

            Dim webRequest = System.Net.WebRequest.Create(sendSMSUrl)
            Dim result = webRequest.GetResponse()

            If (DirectCast(result, System.Net.HttpWebResponse).StatusCode = System.Net.HttpStatusCode.OK) Then
                Console.WriteLine("Message sent")
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

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
            System.Net.ServicePointManager.Expect100Continue = False

            'clsDefaultConfiguration.CustomerWebServiceUrl = "http://10.10.180.68:8080/posSeam/webservices/ClpCustomer?wsdl"

            'Specify the binding to be used for the client.
            Dim binding As New System.ServiceModel.BasicHttpBinding()

            'Specify the address to be used for the client.
            Dim address As New System.ServiceModel.EndpointAddress(clsDefaultConfiguration.CustomerWebServiceUrl)

            'Create a client that is configured with this address and binding.
            Dim client As New CustomerServices.ClpCustomerClient(binding, address)

            ''--- Add by Mahesh for Check Error Message ...
            'client.sendSMS(sendSMS)

            client.sendSMSAsync(sendSMS)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub AssignFields()
        txtCustomerCode.Text = CustomerResponse.CLPCustomer.CardNumber
        txtCustomerName.Text = CustomerResponse.CLPCustomer.FirstName
        txtMiddleName.Text = CustomerResponse.CLPCustomer.MiddleName
        txtLastName.Text = CustomerResponse.CLPCustomer.LastName
        dtDOB.Value = IIf(CustomerResponse.CLPCustomer.BirthDate = DateTime.MinValue, Nothing, CustomerResponse.CLPCustomer.BirthDate)
        If clsDefaultConfiguration.EvasPizzaChanges = True Then
            txtReminderComment.Text = CustomerResponse.CLPCustomer.ReminderComment
        Else
        End If
        'cboCustomerGroup.SelectedValue = CustomerResponse.CLPCustomer.CustomerGroup
        txtTierType.Text = CustomerResponse.CLPCustomer.CardType
        cboGender.SelectedItem = CustomerResponse.CLPCustomer.Gender
        txtTelPhone.Text = CustomerResponse.CLPCustomer.MobileNo
        txtTelPhone.Enabled = False
        If clsDefaultConfiguration.AllowMobileNoEditable Then
            txtTelPhone.Enabled = True
        Else
            txtTelPhone.Enabled = False
        End If
        CtrlLblStatusValue.Text = CustomerResponse.CLPCustomer.RegistrationStatus
        txtEmailId.Text = CustomerResponse.CLPCustomer.EmailId

        If (CustomerResponse.CLPCustomer.RegistrationStatus.Equals("Registered")) Then
            txtEmailId.Enabled = False
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
                        btnPassKey.Enabled = True
                    Else
                        btnPassKey.Enabled = False
                    End If
                End If

                If dsCLPprog.Tables("CLPHeader").Rows(0)("IsPOSPasskey") Then
                    passkeyallowedatpos = True
                End If
            End If
        End If

        btnPassKey.Visible = passkeyallowedatpos

        If CustomerResponse.CLPCustomer.AddressList.Count > 0 Then
            cboAddressType.SelectedValue = CustomerResponse.CLPCustomer.AddressList(0).AddressType
            txtCustomerAddress.Text = CustomerResponse.CLPCustomer.AddressList(0).AddLine1
            txtAddress2.Text = CustomerResponse.CLPCustomer.AddressList(0).AddLine2
            txtAddress3.Text = CustomerResponse.CLPCustomer.AddressList(0).AddLine3
            txtAddress4.Text = CustomerResponse.CLPCustomer.AddressList(0).AddLine4
            cboCountry.SelectedValue = CustomerResponse.CLPCustomer.AddressList(0).Country
            cboState.SelectedValue = CustomerResponse.CLPCustomer.AddressList(0).State
            cboCity.SelectedValue = CustomerResponse.CLPCustomer.AddressList(0).City
            txtZipCode.Text = CustomerResponse.CLPCustomer.AddressList(0).PinCode
        End If

        If (Not String.IsNullOrEmpty(txtCustomerCode.Text.Trim())) Then
            _dtCustmInfo = objCustm.GetCustomerInformation("CLP", vSiteCode, clsAdmin.CLPProgram, txtCustomerCode.Text)

            If _CustmType = "CLP" AndAlso _dtCustmInfo IsNot Nothing AndAlso _dtCustmInfo.Rows.Count > 0 Then
                txtSwipeCard.Text = dtCustmInfo.Rows(0).Item("CUSTOMERNO")
                txtBalancePoint.Text = IIf(dtCustmInfo.Rows(0).Item("BalancePoint") Is DBNull.Value, 0, dtCustmInfo.Rows(0).Item("BalancePoint"))
                txtTotalPoints.Text = IIf(dtCustmInfo.Rows(0).Item("PointsAccumlated") Is DBNull.Value, 0, dtCustmInfo.Rows(0).Item("PointsAccumlated"))
            End If
            If _dtCustmInfo IsNot Nothing AndAlso _dtCustmInfo.Rows.Count > 0 Then
                If (dtCustmInfo.Rows(0).Item("PhysicalCard") IsNot DBNull.Value AndAlso dtCustmInfo.Rows(0).Item("PhysicalCard")) Then
                    txtTelPhone.ReadOnly = False
                End If
            Else
                txtTelPhone.ReadOnly = True
            End If
        End If

    End Sub

    Public Sub New()
        Try
            InitializeComponent()
        Catch ex As Exception

        End Try
        ' This call is required by the Windows Form Designer.

        ' Add any initialization after the InitializeComponent() call.
        Me.MaximizeBox = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private Function ValidateData() As Boolean
        Dim dec As New Decimal
        If String.IsNullOrEmpty(txtTelPhone.Text.Trim()) Then
            MessageBox.Show(getValueByKey("frmnsearchcustomer.mobileno"))
            Return False
            '' -- Changed By Mahesh Now mobile no size no restriction
            ''ElseIf Not String.IsNullOrEmpty(txtTelPhone.Text.Trim()) AndAlso txtTelPhone.Text.Trim().Length < 10 Then
            '        MessageBox.Show(getValueByKey("SearchCustomerValidation"))
            'Return False
        End If
        If clsDefaultConfiguration.CustomerClassSelection Then
            If cboCustomerClass.Text = String.Empty Then
                MessageBox.Show("Please select Customer Class Type")
                Return False
            End If
        End If

        'code added by irfan 
        If oldGSTNo <> txtGST.Text.ToString() Then
            If txtGST.Text <> "" Then
                'Dim objcomm As New clsCommon
                If objCustm.CheckGStNoExist(txtGST.Text.Trim()) = True Then
                    ShowMessage("GST No. already exist", "Information")
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    Private Sub txtEmailIdNPhoneNo_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtEmailId.TextChanged, txtTelPhone.TextChanged

        If Not String.IsNullOrEmpty(txtEmailId.Text) AndAlso Not String.IsNullOrEmpty(txtTelPhone.Text) Then
            CtrlLblStatusValue.Text = getValueByKey([Enum].GetName(GetType(SpectrumCommon.RegistrationType), SpectrumCommon.RegistrationType.Registered))
        Else
            CtrlLblStatusValue.Text = getValueByKey([Enum].GetName(GetType(SpectrumCommon.RegistrationType), SpectrumCommon.RegistrationType.Enrolled))
        End If

    End Sub

    Private Sub txtEmailID_Validating(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles txtEmailId.Validating
        If txtEmailId.Text <> "" Then
            If emailaddresscheck(txtEmailId.Text) Then
                txtEmailId.BackColor = Color.Green
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
    Private Sub btnPassKey_Click(sender As System.Object, e As System.EventArgs) Handles btnPassKey.Click
        Try
            If dsCLPprog IsNot Nothing AndAlso dsCLPprog.Tables.Contains("CLPHeader") Then
                If dsCLPprog.Tables("CLPHeader").Rows.Count > 0 Then
                    If dsCLPprog.Tables("CLPHeader").Rows(0)("IsPOSPasskey") Then
                        CustomerResponse = CustomerBL.GetCustomerInfo(clsAdmin.CLPProgram, txtCustomerCode.Text, KeyIsMobileNo:=False)
                        If CustomerResponse IsNot Nothing AndAlso CustomerResponse.CLPCustomer IsNot Nothing Then
                            Dim passkey As New PassKeyGenerator(CustomerResponse.CLPCustomer, dsCLPprog)
                            Dim result = passkey.ShowDialog()
                            If result = Windows.Forms.DialogResult.OK Then
                                passkey.PassKey.ExpiryDateTime = DateTime.Now.AddMinutes(dsCLPprog.Tables("CLPHeader").Rows(0)("PosPasskeyValidity"))
                                CLP_Data.GeneratePassKey(passkey.PassKey)

                                'If (clsDefaultConfiguration.ClpSMSAllowed AndAlso Not String.IsNullOrEmpty(CustomerResponse.CLPCustomer.MobileNo) AndAlso CustomerResponse.CLPCustomer.MobileNo.Length >= 10) Then
                                CustomerResponse.CLPCustomer.Passkey = passkey.PassKey.Passkey
                                CustomerResponse.CLPCustomer.PasskeyValue = passkey.PassKey.PasskeyValue
                                SendSMS2Customer(CustomerResponse.CLPCustomer, MessageType.CLPPOSPassKey)

                                'SendSMStoCustomer(passkey.PassKey.PasskeyNumber, passkey.PassKey.PasskeyValue, CustomerResponse.CLPCustomer.MobileNo)
                                'End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
            MessageBox.Show("Problem Occurred while generating Passkey!!")
        End Try
    End Sub

    Private Sub EnableOrDisableFields(ByVal isEnable As Boolean)
        Try
            txtCustomerName.ReadOnly = isEnable
            txtMiddleName.ReadOnly = isEnable
            txtLastName.ReadOnly = isEnable
            dtDOB.ReadOnly = isEnable
            cboGender.Enabled = IIf(isEnable, False, True)
            txtTelPhone.ReadOnly = isEnable
            txtEmailId.ReadOnly = isEnable
            txtResidenceNumber.ReadOnly = isEnable

            txtCustomerAddress.ReadOnly = isEnable
            txtAddress2.ReadOnly = isEnable
            txtAddress3.ReadOnly = isEnable
            cboCountry.Enabled = IIf(isEnable, False, True)
            cboState.Enabled = IIf(isEnable, False, True)
            cboCity.Enabled = IIf(isEnable, False, True)
            txtZipCode.ReadOnly = isEnable

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub HideOtherCustomerFields(ByVal customerNo As String)
        RadioBtnCLPCustm.Checked = True
        RadioBtnCLPCustm.Enabled = False
        RadioBtnSalesCustm.Enabled = False
        BtnCreateNew.Enabled = False

        BtnSearchCustomer.Visible = False
        txtTierType.Visible = True
        CtrlLbltiers.Visible = True

        lblSwipeCard.Visible = False
        txtSwipeCard.Visible = False


        CtrLblStatus.Visible = True

        '--- Commented By Mahesh becoz we are not handling other cust here its for CLP CUSTomer 

        'btnPassKey.Visible = False
        'lbCustomer.Visible = False
        'txtCustomerCode.Visible = False
        'lblBalancePoint.Visible = False
        'txtBalancePoint.Visible = False
        'lblTotalPoints.Visible = False
        'txtTotalPoints.Visible = False
        '-----

        If (Not String.IsNullOrEmpty(customerNo)) Then
            _dtCustmInfo = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, customerNo)
            SetCustomerInformationInForm(dtCustmInfo)
            CboAddressTypeChanged(dtCustmInfo)
            txtTelPhone.Enabled = False
            '---- Added By Mahesh in case flag AllowMobileNoEditable is set true 
            If clsDefaultConfiguration.AllowMobileNoEditable Then
                txtTelPhone.Enabled = True
            Else
                txtTelPhone.Enabled = False
            End If

        End If
    End Sub
    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        lblMessage.BorderStyle = BorderStyle.None
        lblMessage.BackColor = Color.Transparent
        lblMessage.ForeColor = Color.FromArgb(255, 255, 255)
        lblMessage.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        RadioBtnCLPCustm.Location = New Point(79, 32)
        RadioBtnCLPCustm.BackColor = Color.FromArgb(212, 212, 212)
        RadioBtnCLPCustm.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        RadioBtnSalesCustm.Location = New Point(245, 32)
        RadioBtnSalesCustm.BackColor = Color.FromArgb(212, 212, 212)
        RadioBtnSalesCustm.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lbCustomer.BackColor = Color.FromArgb(212, 212, 212)
        lbCustomer.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lbCustomer.AutoSize = False
        lbCustomer.Size = New Size(155, 18)
        txtCustomerCode.MinimumSize = New Size(10, 18)
        txtCustomerCode.Height = 18
        lblTelno.BackColor = Color.FromArgb(212, 212, 212)
        lblTelno.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblTelno.AutoSize = False
        lblTelno.Size = New Size(155, 18)
        txtTelPhone.MinimumSize = New Size(10, 18)
        txtTelPhone.Height = 18
        lblName.BackColor = Color.FromArgb(212, 212, 212)
        lblName.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblName.AutoSize = False
        lblName.Size = New Size(155, 18)
        txtCustomerName.MinimumSize = New Size(10, 18)
        txtCustomerName.Height = 18
        lblMiddleName.BackColor = Color.FromArgb(212, 212, 212)
        lblMiddleName.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblMiddleName.AutoSize = False
        lblMiddleName.Size = New Size(155, 18)
        txtMiddleName.MinimumSize = New Size(10, 18)
        txtMiddleName.Height = 18
        lblLastName.BackColor = Color.FromArgb(212, 212, 212)
        lblLastName.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblLastName.AutoSize = False
        lblLastName.Size = New Size(155, 18)
        txtLastName.MinimumSize = New Size(10, 18)
        txtLastName.Height = 18
        CtrlLbltiers.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLbltiers.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        CtrlLbltiers.AutoSize = False
        CtrlLbltiers.Size = New Size(155, 18)
        CtrlLbltiers.Location = New Point(12, 165)
        txtTierType.MinimumSize = New Size(10, 18)
        txtTierType.Height = 18
        lblDOB.BackColor = Color.FromArgb(212, 212, 212)
        lblDOB.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblDOB.AutoSize = False
        lblDOB.Size = New Size(155, 18)
        dtDOB.Location = New Point(166, 205)
        dtDOB.MinimumSize = New Size(0, 17)
        dtDOB.Height = 17
        If clsDefaultConfiguration.EvasPizzaChanges = True Then
            txtReminderComment.Height = 18
            lblReminderComment.Location = New Point(12, 225)
            lblReminderComment.BackColor = Color.FromArgb(212, 212, 212)
            lblReminderComment.Font = New Font("Neo Sans", 9, FontStyle.Regular)
            lblReminderComment.AutoSize = False
            lblReminderComment.Size = New Size(155, 20)
        Else
            lblCustomerGroup.Location = New Point(12, 228)
            lblCustomerGroup.BackColor = Color.FromArgb(212, 212, 212)
            lblCustomerGroup.Font = New Font("Neo Sans", 9, FontStyle.Regular)
            lblCustomerGroup.AutoSize = False
            lblCustomerGroup.Size = New Size(155, 18)
            cboCustomerGroup.Height = 18
        End If
        lblGender.BackColor = Color.FromArgb(212, 212, 212)
        lblGender.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblGender.AutoSize = False
        lblGender.Size = New Size(155, 20)
        cboGender.Height = 16
        cboGender.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        lblAddressType.BackColor = Color.FromArgb(212, 212, 212)
        lblAddressType.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblAddressType.AutoSize = False
        lblAddressType.Size = New Size(155, 18)
        cboAddressType.Height = 18
        lblAddress.BackColor = Color.FromArgb(212, 212, 212)
        lblAddress.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblAddress.AutoSize = False
        lblAddress.Size = New Size(155, 18)
        txtCustomerAddress.MinimumSize = New Size(0, 18)
        txtCustomerAddress.Height = 18
        lbladdress2.BackColor = Color.FromArgb(212, 212, 212)
        lbladdress2.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lbladdress2.AutoSize = False
        lbladdress2.Size = New Size(155, 18)
        txtAddress2.MinimumSize = New Size(0, 18)
        txtAddress2.Font = New Font("Neo Sans", 7.5, FontStyle.Regular)
        txtAddress2.Height = 18
        lbladdress3.BackColor = Color.FromArgb(212, 212, 212)
        lbladdress3.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lbladdress3.AutoSize = False
        lbladdress3.Size = New Size(155, 18)
        txtAddress3.MinimumSize = New Size(0, 18)
        txtAddress3.Height = 18
        txtAddress3.Location = New Point(166, 334)
        txtAddress3.Font = New Font("Neo Sans", 7.5, FontStyle.Regular)
        CtrlLabel1.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel1.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        CtrlLabel1.AutoSize = False
        CtrlLabel1.Size = New Size(155, 18)
        txtAddress4.MinimumSize = New Size(0, 18)
        txtAddress4.Height = 18
        txtAddress4.Location = New Point(166, 356)
        txtAddress4.Font = New Font("Neo Sans", 7.5, FontStyle.Regular)
        lblCountry.BackColor = Color.FromArgb(212, 212, 212)
        lblCountry.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblCountry.AutoSize = False
        lblCountry.Size = New Size(155, 18)
        cboCountry.Height = 16
        cboCountry.Font = New Font("Neo Sans", 7.8, FontStyle.Regular)
        lblState.BackColor = Color.FromArgb(212, 212, 212)
        lblState.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblState.AutoSize = False
        lblState.Size = New Size(155, 18)
        cboState.Font = New Font("Neo Sans", 7.8, FontStyle.Regular)

        lblCity.BackColor = Color.FromArgb(212, 212, 212)
        lblCity.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblCity.AutoSize = False
        lblCity.Size = New Size(155, 18)
        cboCity.Height = 15
        cboCity.Font = New Font("Neo Sans", 7.8, FontStyle.Regular)
        '-------------------
        lblGST.BackColor = Color.FromArgb(212, 212, 212)
        lblGST.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblGST.AutoSize = False
        lblGST.Size = New Size(70, 16)
        lblGST.Location = New Point(318, 475)

        txtGST.Location = New Point(385, 475)
        txtGST.AutoSize = False
        txtGST.Size = New Size(143, 16)
        txtGST.MinimumSize = New Size(143, 16)
        txtGST.Height = 16

        '-----------------
        lblZipCode.BackColor = Color.FromArgb(212, 212, 212)
        lblZipCode.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblZipCode.AutoSize = False
        lblZipCode.Size = New Size(102, 18)
        lblZipCode.Location = New Point(322, 404)
        txtZipCode.MinimumSize = New Size(10, 18)
        txtZipCode.Height = 18
        lblEmail.BackColor = Color.FromArgb(212, 212, 212)
        lblEmail.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblEmail.AutoSize = False
        lblEmail.Size = New Size(155, 18)
        txtEmailId.MinimumSize = New Size(10, 18)
        txtEmailId.Height = 18

        lblResidenceNumber.BackColor = Color.FromArgb(212, 212, 212)
        lblResidenceNumber.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblResidenceNumber.AutoSize = False
        lblResidenceNumber.Size = New Size(155, 18)
        txtResidenceNumber.MinimumSize = New Size(10, 18)
        txtResidenceNumber.Height = 18
        CtrLblStatus.BackColor = Color.FromArgb(212, 212, 212)
        CtrLblStatus.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        CtrLblStatus.AutoSize = False
        CtrLblStatus.Size = New Size(155, 18)

        lblSwipeCard.BackColor = Color.FromArgb(212, 212, 212)
        lblSwipeCard.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblSwipeCard.AutoSize = False
        lblSwipeCard.Size = New Size(155, 18)
        txtSwipeCard.MinimumSize = New Size(10, 18)
        txtSwipeCard.Height = 18

        lblBalancePoint.BackColor = Color.FromArgb(212, 212, 212)
        lblBalancePoint.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblBalancePoint.AutoSize = False
        lblBalancePoint.Size = New Size(155, 18)
        lblBalancePoint.Location = New Point(12, 525)
        txtBalancePoint.MinimumSize = New Size(10, 18)
        txtBalancePoint.Height = 18

        txtBalancePoint.Location = New Point(166, 525)

        CtrlLblStatusValue.BackColor = Color.Transparent
        CtrlLblStatusValue.ForeColor = Color.White
        CtrlLblStatusValue.BorderStyle = BorderStyle.None
        CtrlLblStatusValue.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        lblTotalPoints.BackColor = Color.FromArgb(212, 212, 212)
        lblTotalPoints.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblTotalPoints.AutoSize = False
        lblTotalPoints.Size = New Size(155, 18)


        btnOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnOk.BackColor = Color.Transparent
        btnOk.BackColor = Color.FromArgb(0, 107, 163)
        btnOk.ForeColor = Color.FromArgb(255, 255, 255)
        btnOk.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        btnOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnOk.FlatStyle = FlatStyle.Flat
        btnOk.FlatAppearance.BorderSize = 0
        btnOk.Height = 26
        btnOk.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        btnExit.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnExit.BackColor = Color.Transparent
        btnExit.BackColor = Color.FromArgb(0, 107, 163)
        btnExit.ForeColor = Color.FromArgb(255, 255, 255)
        btnOk.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnExit.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnExit.FlatStyle = FlatStyle.Flat
        btnExit.FlatAppearance.BorderSize = 0
        btnExit.Height = 26
        btnExit.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        btnPassKey.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnPassKey.BackColor = Color.Transparent
        btnPassKey.BackColor = Color.FromArgb(0, 107, 163)
        btnPassKey.ForeColor = Color.FromArgb(255, 255, 255)
        btnPassKey.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        btnPassKey.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnPassKey.FlatStyle = FlatStyle.Flat
        btnPassKey.FlatAppearance.BorderSize = 0
        btnPassKey.Height = 26
        btnPassKey.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)


        BtnCreateNew.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnCreateNew.BackColor = Color.Transparent
        BtnCreateNew.BackColor = Color.FromArgb(0, 107, 163)
        BtnCreateNew.ForeColor = Color.FromArgb(255, 255, 255)
        BtnCreateNew.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        BtnCreateNew.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnCreateNew.FlatStyle = FlatStyle.Flat
        BtnCreateNew.FlatAppearance.BorderSize = 0
        '  btnPassKey.Height = 26
        BtnCreateNew.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)


    End Function

    Private Sub txtGST_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtGST.KeyDown
        If oldGSTNo <> txtGST.Text.ToString() Then
            If e.KeyCode = Keys.Enter Then
                If txtGST.Text <> "" Then
                    'Dim objcomm As New clsCommon
                    If objCustm.CheckGStNoExist(txtGST.Text.Trim()) = True Then
                        ShowMessage("GST No. already exist", "Information")
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If
        End If
    End Sub
    'Code Added by irfan on 25/1/2018 for Mantis issue==========================================================
    Private Sub dtDOB_Leave(sender As System.Object, e As System.EventArgs) Handles dtDOB.Leave
        If dtDOB.Text <> "" Then
            If dtDOB.Text > Now Then
                ShowMessage("Date of Birth Can't be Greater Than Current Date", "Information")
                dtDOB.SelectedText = ""
                dtDOB.Focus()
            End If
        End If
    End Sub

    Private Sub txtTelPhone_Leave(sender As System.Object, e As System.EventArgs) Handles txtTelPhone.Leave
        If txtTelPhone.Text.Length > 10 Then
            ShowMessage("Phone numbers must be 10 digits only.", "Information")
            txtTelPhone.Text = ""
            txtTelPhone.Focus()
            Exit Sub
        End If
        If Not IsNumeric(txtTelPhone.Text) Then
            ShowMessage("Phone numbers  Must be Number.", "Information")
            txtTelPhone.Text = ""
            txtTelPhone.Focus()
            Exit Sub
        End If
    End Sub
    '==========================================================================================================
End Class
