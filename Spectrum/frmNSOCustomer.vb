Imports SpectrumBL
Imports System.Data
Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid
Public Class frmNSOCustomer
    Dim objCustm As New clsSOCustomer
    Dim objComn As New clsCommon
    Public vCustomerNo As String = ""
    Dim IsFormLoaded As Boolean = False
    Dim dsCombo As DataSet = New DataSet
    Dim dsCombo1 As DataSet = New DataSet
    Dim CustomerType As String = "SO"
    'Dim vDateFormat As String = clsAdmin.SqlDBDateFormat
    Public pSearchCust As String = ""
    'Dim clsadmin.sitecode As String = clsAdmin.SiteCode
    'Dim vUserName As String = clsAdmin.UserName
    Dim dvCity, dvCity1, dvState, dvState1 As DataView
    Dim drCustmInfo As DataRow
    Dim drCustmAdds As DataRow
    Dim findKeyInfo(1) As Object
    Dim findKeyAdds(3) As Object
    Dim _dsMain As New DataSet
    Public _CustomerNoToSearch As String = String.Empty
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

    ''' <summary>
    ''' Create New Customer for Sales Order and Other Customers
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmSOCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            _dsMain = objCustm.GetCustomerDataSet(clsAdmin.SiteCode, "0")
            dsCombo = objCustm.GetComboDataSet
            dsCombo1 = dsCombo.Copy
            'PopulateComboBox(dsCombo.Tables("CityTab"), cboCity)
            'pC1ComboSetDisplayMember(cboCity)
            'PopulateComboBox(dsCombo.Tables("StateTab"), cboState)
            'pC1ComboSetDisplayMember(cboState)
            PopulateComboBox(dsCombo.Tables("CountryTab"), cboCountry)
            pC1ComboSetDisplayMember(cboCountry)

            dvState1 = New DataView(dsCombo1.Tables("StateTab"), "", "", DataViewRowState.CurrentRows)
            cboState1.DataSource = dvState1
            cboState1.DisplayMember = "Description"
            cboState1.ValueMember = "AreaCode"
            pC1ComboSetDisplayMember(cboState1)
            dvState = New DataView(dsCombo.Tables("StateTab"), "", "", DataViewRowState.CurrentRows)
            cboState.DataSource = dvState
            cboState.DisplayMember = "Description"
            cboState.ValueMember = "AreaCode"
            pC1ComboSetDisplayMember(cboState)

            dvCity1 = New DataView(dsCombo1.Tables("CityTab"), "", "", DataViewRowState.CurrentRows)
            cboCity1.DataSource = dvCity1
            cboCity1.DisplayMember = "Description"
            cboCity1.ValueMember = "AreaCode"
            pC1ComboSetDisplayMember(cboCity1)

            dvCity = New DataView(dsCombo.Tables("CityTab"), "", "", DataViewRowState.CurrentRows)
            cboCity.DataSource = dvCity
            cboCity.DisplayMember = "Description"
            cboCity.ValueMember = "AreaCode"
            pC1ComboSetDisplayMember(cboCity)
            'PopulateComboBox(dsCombo1.Tables("CityTab"), cboCity1)
            'pC1ComboSetDisplayMember(cboCity1)
            'PopulateComboBox(dsCombo1.Tables("StateTab"), cboState1)

            PopulateComboBox(dsCombo1.Tables("CountryTab"), cboCountry1)
            pC1ComboSetDisplayMember(cboCountry1)
            PopulateComboBox(dsCombo.Tables("TitleTab"), cboTitle)
            pC1ComboSetDisplayMember(cboTitle)
            PopulateComboBox(dsCombo.Tables("GenderTab"), cboGender)
            pC1ComboSetDisplayMember(cboGender)
            PopulateComboBox(dsCombo.Tables("MaritalTab"), cboMaritalStatus)
            pC1ComboSetDisplayMember(cboMaritalStatus)
            PopulateComboBox(dsCombo.Tables("EducationTab"), cboEducation)
            pC1ComboSetDisplayMember(cboEducation)
            PopulateComboBox(dsCombo.Tables("OccupationTab"), cboOccupation)
            pC1ComboSetDisplayMember(cboOccupation)
            'PopulateComboBox(dsCombo.Tables("CustomerTypeTab"), cboCustomerType)

            'lblSODate.Text = Format(Now.Date, vDateFormat)
            'lblUserId.Text = clsAdmin.UserName

            'dtpDOB.DisplayFormat.CustomFormat = vDateFormat
            'dtpDOB.EditFormat.CustomFormat = vDateFormat
            'dtpDOB.Value = Format(Now.Date, vDateFormat)

            'dtpMarriageDate.DisplayFormat.CustomFormat = vDateFormat
            'dtpMarriageDate.EditFormat.CustomFormat = vDateFormat
            'dtpMarriageDate.Value = Format(Now.Date, vDateFormat)

            SetCulture(Me, Me.Name)
            SetEnableDisableFields(True)
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
            End If
            'BtnNew_Click(sender, e)

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
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
                BtnNew.Text = getValueByKey("frmnsocustomer.btnnewcancel")
                BtnNew.Tag = "Cancel"
                'BtnEdit.Text = "&Save"
                BtnEdit.Text = getValueByKey("frmnsocustomer.btneditsave")
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
                dvCity.RowFilter = "1=0"
                dvCity1.RowFilter = "1=0"
                dvState.RowFilter = "1=0"
                dvState1.RowFilter = "1=0"
            ElseIf BtnNew.Tag = "Cancel" Then
                If MsgBox(getValueByKey("SOC001"), MsgBoxStyle.YesNo, "SOC001 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then

                    Me.Close()
                    Return

                    SetEnableDisableFields(True)
                    'txtCustomerCode.Text = ""

                    If Not (BtnEdit.Tag = "Update") Then
                        SetClearFields()
                    End If

                    IsFormLoaded = False
                    'BtnNew.Text = "&New"
                    BtnNew.Text = getValueByKey("frmnsocustomer.btnnew")
                    BtnNew.Tag = "New"
                    'BtnEdit.Tag = "Edit"
                    BtnEdit.Tag = "Edit"
                    BtnEdit.Text = getValueByKey("frmnsocustomer.btnedit")
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
    Public Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            If BtnEdit.Tag = "Save" Then

                If ValidatedSOCustomer() = True AndAlso SaveCustomerInformation() = True _
                AndAlso objCustm.SaveData(dsMain, "SOCustomer", True) = True Then

                    txtCustomerCode.Text = vCustomerNo
                    'ShowMessage(getValueByKey("SOC018"), getValueByKey("CLAE04"))
                    MessageBox.Show(String.Format(getValueByKey("SOC018"), vCustomerNo), "SOC018 - " & getValueByKey("CLAE04"))
                    _Customerno = txtCustomerCode.Text
                    '_dsMain.Tables("CustomerSaleOrder").Rows.Clear()
                    '_dsMain.Tables("CustomerAddress").Rows.Clear()
                    SetEnableDisableFields(True)
                    'BtnNew.Text = "&New"
                    getValueByKey("frmnsocustomer.btnnew")
                    BtnNew.Tag = "New"
                    'BtnEdit.Text = "&Edit"
                    getValueByKey("frmnsocustomer.btnedit")
                    BtnEdit.Tag = "Edit"
                    BtnSearchCustomer.Enabled = True
                    'BtnDelete.Enabled = True
                    BtnDelete.Visible = False
                    BtnOk.Enabled = True
                    txtCustomerCode.Visible = True
                    lblCustomerCode.Visible = True
                    BtnSearchCustomer.Visible = True
                    Me.Close()
                End If

            ElseIf BtnEdit.Tag = "Update" Then

                If ValidatedSOCustomer() = True AndAlso UpdateCustomerInformation() = True _
                AndAlso objCustm.SaveData(dsMain, "SOCustomer", True) = True Then

                    ShowMessage(getValueByKey("SOC002"), "SOC002 - " & getValueByKey("CLAE04"))

                    '_dsMain.Tables("CustomerSaleOrder").Rows.Clear()
                    '_dsMain.Tables("CustomerAddress").Rows.Clear()

                    SetEnableDisableFields(True)
                    'BtnNew.Text = "&New"
                    BtnNew.Text = getValueByKey("frmnsocustomer.btnnew")
                    BtnNew.Tag = "New"
                    'BtnEdit.Text = "&Edit"
                    BtnEdit.Text = getValueByKey("frmnsocustomer.btnedit")
                    BtnEdit.Tag = "Edit"

                    BtnEdit.Enabled = True
                    'BtnDelete.Enabled = True
                    BtnDelete.Visible = False
                    BtnSearchCustomer.Enabled = True
                    BtnOk.Enabled = True
                    BtnExit.Enabled = True
                Else
                    BtnEdit.Text = getValueByKey("frmnsocustomer.btnedit")
                    BtnEdit.Tag = "Edit"
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
                    BtnEdit.Tag = "Update"
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
            txtFirstName.Text = ""
            txtMiddleName.Text = ""
            txtLastName.Text = ""
            cboGender.SelectedIndex = -1
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
            'dtpMarriageDate.Enabled = tmpvar
            cboEducation.SelectedIndex = -1
            cboOccupation.SelectedIndex = -1
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
            Dim docNo As String = objComn.getDocumentNo("SOCustomer", clsAdmin.SiteCode)
            Dim prefixDocNo = "COS" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3, 3)

            'String.Concat(otherCharacters,docNo.PadLeft(15-otherCharacters.Length,"0"))	"CLSHM1000000008"
            vCustomerNo = objComn.GenDocNo(prefixDocNo, 15, docNo)

            drCustmInfo = dsMain.Tables("CustomerSaleOrder").NewRow()
            If PrepareCustmInfoData(drCustmInfo) = True Then
                dsMain.Tables("CustomerSaleOrder").Rows.Add(drCustmInfo)
            End If
            'rohit

            'If Not (txtAddress1.Text.Trim = "") Then
            drCustmAdds = dsMain.Tables("CustomerAddress").NewRow()
            If PrepareCustmAddsData(drCustmAdds, 1) = True Then
                dsMain.Tables("CustomerAddress").Rows.Add(drCustmAdds)
            End If
            'Else
            'ShowMessage(getValueByKey("SOC019"), getValueByKey("CLAE04"))
            'Return False
            'End If

            'If Not (txtAddress11.Text.Trim = "") Then 'AndAlso Not (cboCity1.Text = "") And Not (txtPincode1.Text.Trim = "") Then
            drCustmAdds = dsMain.Tables("CustomerAddress").NewRow()
            If PrepareCustmAddsData(drCustmAdds, 2) = True Then
                dsMain.Tables("CustomerAddress").Rows.Add(drCustmAdds)
            End If
            'End If

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
            drCustInfo("MobilePhone") = IIf(txtMobile.Text = Nothing, DBNull.Value, txtMobile.Text.Trim)
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
    Private Function ValidatedSOCustomer() As Boolean
        Try
            'If cboCustomerType.Text = "" Then
            '    MsgBox("Please Select Customer Type ")
            '    cboCustomerType.Focus()
            '    Return IsValidated
            '    Exit Function
            'Else
            'If cboTitle.Text = "" Then
            '    ShowMessage("Please Select Title ", "information")
            '    cboTitle.Focus()
            '    Return IsValidated
            '    Exit Function
            'Else
            If txtFirstName.Text = "" Then
                ShowMessage(getValueByKey("SOC008"), "SOC008 - " & getValueByKey("CLAE04"))
                txtFirstName.Focus()
                Return IsValidated
                Exit Function
            ElseIf txtLastName.Text = "" Then
                ShowMessage(getValueByKey("SOC009"), "SOC009 - " & getValueByKey("CLAE04"))
                txtLastName.Focus()
                Return IsValidated
                Exit Function
            ElseIf String.IsNullOrEmpty(txtMobile.Text) Then
                ShowMessage("Mobile Number is Mandatory", getValueByKey("CLAE04"))
                txtMobile.Focus()
                Return IsValidated
            ElseIf String.IsNullOrEmpty(txtAddress1.Text) Then
                ShowMessage(getValueByKey("SOC019"), getValueByKey("CLAE04"))
                txtAddress1.Focus()
                Return IsValidated
                Exit Function
            ElseIf cboGender.Text = "" Then
                ShowMessage(getValueByKey("SOC010"), "SOC010 - " & getValueByKey("CLAE04"))
                cboGender.Focus()
                Return IsValidated
                Exit Function
                '    'ElseIf dtpDOB.Text = "" AndAlso Format(dtpDOB.Value, "yyyy") < Format(Now.Date, "yyyy") Then
                '    '    MsgBox("Please Select Date Of Birth ")
                '         msgbox(getValueByKey("SOC014"), ,"SOC014 - " & getValueByKey("CLAE04"))
                '    '    dtpDOB.Focus()
                '    '    Return IsValidated
                '    '    Exit Function
                'ElseIf txtAddress1.Text = "" Then
                '    ShowMessage("Please Enter Residential Address ", "information")
                'ShowMessage(getValueByKey("SOC011"), ,"SOC011 - " & getValueByKey("CLAE04"))
                '    txtAddress1.Focus()
                '    Return IsValidated
                '    Exit Function
                'ElseIf cboCountry.Text = "" Then
                '    ShowMessage("Please Select Residential Country ", "information")
                '    ShowMessage(getValueByKey("SOC012"), ,"SOC012 - " & getValueByKey("CLAE04"))
                '    cboCountry.Focus()
                '    Return IsValidated
                '    Exit Function
                'ElseIf Not (txtPincode.Text = "") AndAlso IsNumeric(txtPincode.Text) = False Then
                '    ShowMessage(getValueByKey("SOC013"), "SOC013 - " & getValueByKey("CLAE04"))
                '    txtPincode.Focus()
                '    Return IsValidated
                '    Exit Function
                'ElseIf Not (txtAddress11.Text = "") Then
                '    If cboCountry1.Text = "" Then
                '        MsgBox("Please Select Office Country ")
                '        cboCountry1.Focus()
                '        Return IsValidated
                '        Exit Function
            ElseIf txtEmail.Text <> String.Empty AndAlso validateEmailId(txtEmail.Text) = False Then
                ShowMessage(getValueByKey("HMD003"), "HMD003 - " & getValueByKey("CLAE04"))
                txtEmail.Focus()
                Return IsValidated
                Exit Function
            End If

            'If Not (txtPincode1.Text = "") AndAlso IsNumeric(txtPincode1.Text) = False Then
            '    MsgBox("Please Enter Numeric Pin Code ")
            '    txtPincode1.Focus()
            '    Return IsValidated
            '    Exit Function
            'End If
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

            'Dim FetchData As New frmNSearchCustomer
            'FetchData.ShowDialog()
            '---------------------- search-----------------

            Dim objBrwClp As New frmNSearchCLPLookUp("SO", clsAdmin.SiteCode, _CustomerNoToSearch)
            'objBrwClp.Text = "Search Customer"
            objBrwClp.Text = getValueByKey("frmnsearchclplookupothercust")
            objBrwClp.WindowState = FormWindowState.Maximized
            objBrwClp.ShowDialog()

            If objBrwClp.dtCustmInfo Is Nothing Then
                If pSearchCust = "SEARCH" Then
                    Exit Sub
                End If
            ElseIf objBrwClp.dtCustmInfo.Rows.Count > 0 Then
                SelectedCustmCode = objBrwClp.dtCustmInfo.Rows(0)("CustomerNo")
            Else

            End If

            objBrwClp.Dispose()
            '---------------------- search-----------------


            If Not String.IsNullOrEmpty(SelectedCustmCode) Then
                _dsMain = objCustm.GetCustomerDataSet(clsAdmin.SiteCode, SelectedCustmCode)

                Try
                    drCustmInfo = Nothing
                    drCustmInfo = dsMain.Tables("CustomerSaleOrder").Rows(0)

                    If Not (drCustmInfo Is Nothing) Then
                        vCustomerNo = drCustmInfo("CustomerNo")

                        txtCustomerCode.Text = drCustmInfo("CustomerNo")
                        'cboCustomerType.SelectedIndex = IIf(drCustmInfo("CustomerType") = "SO", 1, 0)
                        cboTitle.SelectedValue = IIf(drCustmInfo("TitleCode") Is DBNull.Value, -1, drCustmInfo("TitleCode"))
                        txtFirstName.Text = IIf(drCustmInfo("FirstName") Is DBNull.Value, "", drCustmInfo("FirstName"))
                        txtMiddleName.Text = IIf(drCustmInfo("MiddleName") Is DBNull.Value, "", drCustmInfo("MiddleName"))
                        txtLastName.Text = IIf(drCustmInfo("LastName") Is DBNull.Value, "", drCustmInfo("LastName"))
                        dtpDOB.Value = IIf(drCustmInfo("DateofBirth") Is DBNull.Value, DBNull.Value, drCustmInfo("DateofBirth"))
                        txtMobile.Text = IIf(drCustmInfo("MobilePhone") Is DBNull.Value, "", drCustmInfo("MobilePhone"))
                        txtResPhone.Text = IIf(drCustmInfo("ResidencePhone") Is DBNull.Value, "", drCustmInfo("ResidencePhone"))
                        txtOffPhone.Text = IIf(drCustmInfo("OfficePhone") Is DBNull.Value, "", drCustmInfo("OfficePhone"))
                        cboOccupation.SelectedValue = IIf(drCustmInfo("Occupation") Is DBNull.Value, -1, drCustmInfo("Occupation"))
                        cboEducation.SelectedValue = IIf(drCustmInfo("Education") Is DBNull.Value, -1, drCustmInfo("Education"))
                        txtEmail.Text = IIf(drCustmInfo("EmailId") Is DBNull.Value, "", drCustmInfo("EmailId"))
                        cboGender.SelectedValue = IIf(drCustmInfo("Gender") Is DBNull.Value, "", drCustmInfo("Gender"))
                        cboMaritalStatus.SelectedValue = IIf(drCustmInfo("MaritalStatus") Is DBNull.Value, -1, drCustmInfo("MaritalStatus"))
                        dtpMarriageDate.Value = IIf(drCustmInfo("MarriageDt") Is DBNull.Value, "", drCustmInfo("MarriageDt"))
                    End If
                Catch ex As Exception
                    ShowMessage(ex.Message, getValueByKey("CLAE05"))
                    LogException(ex)
                End Try

                Try
                    drCustmAdds = Nothing
                    drCustmAdds = dsMain.Tables("CustomerAddress").Rows(0)
                    If Not (drCustmAdds Is Nothing) Then
                        txtAddress1.Text = IIf(drCustmAdds("AddressLn1") Is DBNull.Value, "", drCustmAdds("AddressLn1"))
                        txtAddress2.Text = IIf(drCustmAdds("AddressLn2") Is DBNull.Value, "", drCustmAdds("AddressLn2"))
                        txtAddress3.Text = IIf(drCustmAdds("AddressLn3") Is DBNull.Value, "", drCustmAdds("AddressLn3"))
                        txtAddress4.Text = IIf(drCustmAdds("AddressLn4") Is DBNull.Value, "", drCustmAdds("AddressLn4"))
                        cboCountry.SelectedValue = IIf(drCustmAdds("CountryCode") Is DBNull.Value, "", drCustmAdds("CountryCode"))
                        cboState.SelectedValue = IIf(drCustmAdds("StateCode") Is DBNull.Value, "", drCustmAdds("StateCode"))
                        cboCity.SelectedValue = IIf(drCustmAdds("CityCode") Is DBNull.Value, "", drCustmAdds("CityCode"))
                        txtPincode.Text = IIf(drCustmAdds("PinCode") Is DBNull.Value, "", drCustmAdds("PinCode"))
                    End If
                Catch ex As Exception
                    ShowMessage(ex.Message, getValueByKey("CLAE05"))
                    LogException(ex)
                End Try

                Try
                    If dsMain.Tables("CustomerAddress").Rows.Count > 1 Then
                        drCustmAdds = Nothing
                        drCustmAdds = dsMain.Tables("CustomerAddress").Rows(1)

                        If Not (drCustmAdds Is Nothing) Then
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
    Private Sub frmAddCustSO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "new-customer.htm")
            End If
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
    Private Sub cboMaritalStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMaritalStatus.SelectedValueChanged

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
    Private Sub dtpMarriageDate_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpMarriageDate.Leave

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

    Private Sub dtpDOB_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtpDOB.Validating
        Try
            If dtpDOB.Value Is DBNull.Value Then
                'ShowMessage("Date of Birth cannot be Blank.", "Customer Information")
                dtpDOB.Value = DBNull.Value
            Else
                If Not (DateDiff(DateInterval.Day, dtpDOB.Value, Now) > 0) AndAlso Not (txtFirstName.Text = String.Empty) Then
                    ShowMessage(getValueByKey("SOC015"), "SOC015 - " & getValueByKey("CLAE04"))
                    dtpDOB.Value = Nothing
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Sub dtpDOB_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDOB.ValueChanged
       

    End Sub
    Private Sub txtFirstName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFirstName.KeyDown, txtMiddleName.KeyDown, txtLastName.KeyDown
        If (e.KeyValue > 64 AndAlso e.KeyValue < 91) Or e.KeyValue = 8 Or e.KeyValue = 32 Or e.KeyValue = 46 Then
            e.SuppressKeyPress = False
        Else
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub cboCountry1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCountry1.SelectedValueChanged
        Try
            dvState1.RowFilter = "ParentCode='" & cboCountry1.SelectedValue & "'"
            dvCity1.RowFilter = "1=0"
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cboCountry_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCountry.SelectedValueChanged
        Try
            dvState.RowFilter = "ParentCode='" & cboCountry.SelectedValue & "'"
            dvCity.RowFilter = "1=0"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboCity1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboState1.SelectedValueChanged
        Try
            dvCity1.RowFilter = "ParentCode='" & cboState1.SelectedValue & "'"
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cboCity_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboState.SelectedValueChanged
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

    Private Sub txtPincode1_PreviewKeyDown(sender As System.Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtPincode1.PreviewKeyDown
        If e.KeyCode = Keys.Tab Then
            tbAddress.SelectedIndex = 1
        End If
    End Sub
End Class