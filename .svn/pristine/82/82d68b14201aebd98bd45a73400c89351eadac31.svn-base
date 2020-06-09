Imports SpectrumBL
Imports System.Data
Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid
Public Class frmCLPCustomer
    Public vCustomerNo As String = ""
    Dim dvCity, dvCity1, dvState, dvState1 As DataView
    Dim objCustm As New clsSOCustomer
    Dim _dsMain As New DataSet
    Dim dsCombo As DataSet = New DataSet
    Dim dsCombo1 As DataSet = New DataSet
    Public Property dsMain() As DataSet
        Get
            Return _dsMain
        End Get
        Set(ByVal value As DataSet)
            _dsMain = value
        End Set
    End Property
    Public Sub New()
        InitializeComponent()

        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
    End Sub

    Public Sub frmCLPCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objCLPCustm As New clsCLPCustomer
        SetCulture(Me, Me.Name)
        txtCustomerCode.Visible = False
        BtnSearchCustomer.Visible = False
        lblCustomerCode.Visible = False
        _dsMain = objCLPCustm.GetCLPCustomerDataSet("0", "0")
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
    End Sub

    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        txtCustomerCode.Visible = False
        BtnSearchCustomer.Visible = False
        lblCustomerCode.Visible = False
        SetClearFields()
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        txtCustomerCode.Visible = True
        BtnSearchCustomer.Visible = True
        lblCustomerCode.Visible = True
        SetClearFields()
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

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

    Public Function SaveCustomerInformation() As Boolean
        Try
            Dim drCustmInfo As DataRow
            Dim drCustmAdds As DataRow
            'vCustomerNo = objComn.getDocumentNo("SOCustomer")

            drCustmInfo = dsMain.Tables("CLPCustomers").NewRow()
            If PrepareCustmInfoData(drCustmInfo) = True Then
                dsMain.Tables("CLPCustomers").Rows.Add(drCustmInfo)
            End If
            'rohit

            'If Not (txtAddress1.Text.Trim = "") Then
            drCustmAdds = dsMain.Tables("CLPCustomers").NewRow()
            If PrepareCustmAddsData(drCustmAdds, 1) = True Then
                dsMain.Tables("CLPCustomers").Rows.Add(drCustmAdds)
            End If
            'Else
            'ShowMessage(getValueByKey("SOC019"), getValueByKey("CLAE04"))
            'Return False
            'End If

            If Not (txtAddress11.Text.Trim = "") Then 'AndAlso Not (cboCity1.Text = "") And Not (txtPincode1.Text.Trim = "") Then
                drCustmAdds = dsMain.Tables("CLPCustomers").NewRow()
                If PrepareCustmAddsData(drCustmAdds, 2) = True Then
                    dsMain.Tables("CLPCustomers").Rows.Add(drCustmAdds)
                End If
            End If

            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Return False
        End Try

    End Function

    Public Function PrepareCustmInfoData(ByRef drCustInfo As DataRow) As Boolean
        Try
            drCustInfo("ClpProgramId") = "Will take from service" 'Will take from service'
            drCustInfo("CardNo") = "Will take from service" 'Will take from service'
            drCustInfo("SiteCode") = clsAdmin.SiteCode
            drCustInfo("TitleCode") = cboTitle.SelectedValue 'TODO: Verify Select code'
            drCustInfo("FirstName") = txtFirstName.Text
            drCustInfo("MiddleName") = IIf(txtMiddleName.Text = Nothing, DBNull.Value, txtMiddleName.Text)
            drCustInfo("SurName") = txtLastName.Text
            drCustInfo("NameOnCard") = txtFirstName.Text & " " & txtMiddleName.Text & " " & txtLastName.Text
            If IsDBNull(dtpDOB.Value) Then
                dtpDOB.Value = DBNull.Value
            ElseIf dtpDOB.Value = "#12:00:00 AM#" Then
                dtpDOB.Value = DBNull.Value
            End If

            drCustInfo("DateofBirth") = dtpDOB.Value

            drCustInfo("ResidencePhone") = IIf(txtMobile.Text = Nothing, DBNull.Value, txtMobile.Text)
            drCustInfo("MobilePhone") = txtResPhone.Text
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

    Public Function PrepareCustmAddsData(ByRef drCustAdds As DataRow, ByVal vAddressType As Integer) As Boolean
        Try
            drCustAdds("SiteCode") = clsAdmin.SiteCode
            drCustAdds("CustomerNo") = vCustomerNo        
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
            Dim drCustmAdds As DataRow
            Dim objBrwClp As New frmNSearchCLPLookUp("SO", clsAdmin.SiteCode, String.Empty)
            'objBrwClp.Text = "Search Customer"
            objBrwClp.Text = getValueByKey("frmnsearchclplookupothercust")
            objBrwClp.WindowState = FormWindowState.Maximized
            objBrwClp.ShowDialog()

            If objBrwClp.dtCustmInfo Is Nothing Then
                'If pSearchCust = "SEARCH" Then
                Exit Sub
                'End If
            ElseIf objBrwClp.dtCustmInfo.Rows.Count > 0 Then
            SelectedCustmCode = objBrwClp.dtCustmInfo.Rows(0)("CustomerNo")
            Else

            End If

            objBrwClp.Dispose()
            '---------------------- search-----------------


            If CDbl(SelectedCustmCode) > 0 Then
                _dsMain = objCustm.GetCustomerDataSet(clsAdmin.SiteCode, SelectedCustmCode)

                Try
                    Dim drCustmInfo As DataRow                    
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
                        txtMobile.Text = IIf(drCustmInfo("ResidencePhone") Is DBNull.Value, "", drCustmInfo("ResidencePhone"))
                        txtResPhone.Text = IIf(drCustmInfo("MobilePhone") Is DBNull.Value, "", drCustmInfo("MobilePhone"))
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
                        cboCity.SelectedValue = IIf(drCustmAdds("CityCode") Is DBNull.Value, "", drCustmAdds("CityCode"))
                        cboState.SelectedValue = IIf(drCustmAdds("StateCode") Is DBNull.Value, "", drCustmAdds("StateCode"))
                        cboCountry.SelectedValue = IIf(drCustmAdds("CountryCode") Is DBNull.Value, "", drCustmAdds("CountryCode"))
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
                            cboCity1.SelectedValue = IIf(drCustmAdds("CityCode") Is DBNull.Value, "", drCustmAdds("CityCode"))
                            cboState1.SelectedValue = IIf(drCustmAdds("StateCode") Is DBNull.Value, "", drCustmAdds("StateCode"))
                            cboCountry1.SelectedValue = IIf(drCustmAdds("CountryCode") Is DBNull.Value, "", drCustmAdds("CountryCode"))
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
    End Sub

    ''' <summary>
    ''' Update function for Customer Information
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''    
    Dim findKeyInfo(1) As Object
    Dim findKeyAdds(3) As Object
    Public Function UpdateCustomerInformation() As Boolean
        Try

            findKeyInfo(0) = clsAdmin.SiteCode
            findKeyInfo(1) = txtCustomerCode.Text.Trim
            Dim drCustmInfo As DataRow
            Dim drCustmAdds As DataRow
            drCustmInfo = dsMain.Tables("CustomerSaleOrder").Rows.Find(findKeyInfo)
            If Not (drCustmInfo Is Nothing) Then
                PrepareCustmInfoData(drCustmInfo)
            End If

            For AddressRow = 1 To 2
                findKeyAdds(0) = clsAdmin.SiteCode
                findKeyAdds(1) = txtCustomerCode.Text.Trim
                'findKeyAdds(2) = CustomerType
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
End Class