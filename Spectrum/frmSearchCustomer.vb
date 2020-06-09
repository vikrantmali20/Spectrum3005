Imports SpectrumBL
Imports System.Data
Imports System.Data.SqlClient
Public Class frmSearchCustomer
    Dim dvCustmInfo As DataView
    Dim dtCustmData As New DataTable
    Dim objClp As New clsCLPCustomer
    Dim _CardType As String
    Dim _isMobileNoViewAllowed As Boolean
    Dim _IsVaidyNoteButtonClicked As Boolean = False
    Public vCustomerNo As String = ""
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
    Dim _dtCustmInfo As DataTable
    Public Property dtCustmInfo() As DataTable
        Get
            Return _dtCustmInfo
        End Get
        Set(ByVal value As DataTable)
            _dtCustmInfo = value
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

    Private _IsViewOrderHistory As Boolean = True
    Public Property IsViewOrderHistory() As Boolean
        Get
            Return _IsViewOrderHistory
        End Get
        Set(ByVal value As Boolean)
            _IsViewOrderHistory = value
        End Set
    End Property
    Private _IsTransactionRights As Boolean = False
    Public Property CheckTransactionRights() As Boolean
        Get
            Return _IsTransactionRights
        End Get
        Set(ByVal value As Boolean)
            _IsTransactionRights = value
        End Set
    End Property
    Private IsSearchDb As Boolean = True


    Private frmName As String
    Public Property FormName() As String
        Get
            Return FrmName
        End Get
        Set(ByVal value As String)
            frmName = value
        End Set
    End Property
    Private _dtPrescDtlSrchAddToBill As DataTable
    Public Property dtPrescDtlSrchAddToBill() As DataTable
        Get
            Return _dtPrescDtlSrchAddToBill
        End Get
        Set(ByVal value As DataTable)
            _dtPrescDtlSrchAddToBill = value
        End Set
    End Property

    Private Sub frmSearchCustomer_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim objClsCommon As New clsCommon
            objClsCommon.DisplayHelpFile(ParentForm, "524-search-customers.htm")
        End If
        If e.KeyCode = Keys.Up Then
            If (grdCLPCustomerList.Rows.Count > 1) AndAlso grdCLPCustomerList.Row > 1 Then
                grdCLPCustomerList.Select(grdCLPCustomerList.Row - 1, 3)
            End If
        ElseIf e.KeyCode = Keys.Down Then
            If (grdCLPCustomerList.Rows.Count > 1) AndAlso grdCLPCustomerList.Row < grdCLPCustomerList.Rows.Count - 1 Then
                grdCLPCustomerList.Select(grdCLPCustomerList.Row + 1, 3)
            End If
        End If
    End Sub

    Private Sub frmSearchCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim IsHOInstance As Boolean = False
            Call setResoureces()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ThemeChange()
            End If
            If UCase(ReadSpectrumParamFile("InstanceName")) = "HO" Then
                IsHOInstance = True
            End If
            If CheckTransactionRights Then
                If CheckAuthorisation(clsAdmin.UserCode, "NewCLPCustomer") = False Then
                    BtnNewCustomer.Visible = False
                    BtnEditCustomer.Visible = False
                Else
                    BtnNewCustomer.Visible = True AndAlso Not IsHOInstance
                    BtnEditCustomer.Visible = True AndAlso Not IsHOInstance
                End If
            End If

            BtnViewOrderHistory.Visible = IsViewOrderHistory

            If CheckAuthorisation(clsAdmin.UserCode, "CmOrderHistory") = False Then
                BtnViewOrderHistory.Visible = False
            Else
                BtnViewOrderHistory.Visible = True AndAlso Not IsHOInstance
            End If

            If CheckAuthorisation(clsAdmin.UserCode, "SoOrderHistory") = False Then
                btnViewSalesOrderHistory.Visible = False
            Else
                btnViewSalesOrderHistory.Visible = True AndAlso Not IsHOInstance
            End If

            _isMobileNoViewAllowed = CheckAuthorisation(clsAdmin.UserCode, "CLP_VIEW")

            AddHandler RadioBtnCLPCustm.CheckedChanged, AddressOf RadioCLPCustmer_CheckedChanged
            AddHandler RadioBtnSalesCustm.CheckedChanged, AddressOf RadioSalesCustmer_CheckedChanged

            RadioBtnSalesCustm.Checked = False
            RadioBtnCLPCustm.Checked = True
            '----------- Disable both radio buttons added by mahesh 
            RadioBtnSalesCustm.Enabled = False
            RadioBtnCLPCustm.Enabled = False
            If clsDefaultConfiguration.CUSTOMERSEARCHUSINGPHONENUMBER Then
                lblPhone.Visible = True
                txtSearchByPhn.Visible = True
                txtSearchByPhn.Select()
                txtSearchByPhn.Select(txtSearchByPhn.Text.Length, 0)
            Else
                lblPhone.Visible = False
                txtSearchByPhn.Visible = False
                txtFilterCustomer.Select()
                txtFilterCustomer.Select(txtFilterCustomer.Text.Length, 0)
            End If
            'RadioCLPCustmer_CheckedChanged(Nothing, EventArgs.Empty)
            If clsDefaultConfiguration.EnableHealthCare = True Then
                btnViewVaidyaNotes.Visible = True
            Else
                btnViewVaidyaNotes.Visible = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub setResoureces()
        Try
            Me.Text = getValueByKey("frmSearchCustomer")
            If FormName <> Nothing Then
                Select Case FormName.ToUpper.Trim
                    Case "HOMEDELIVERY"
                        Me.Text = getValueByKey("frmSearchCustomer.HomeDelivery")
                    Case "DINEIN"
                        Me.Text = getValueByKey("frmSearchCustomer.DineIn")
                    Case "TAKEAWAY"
                        Me.Text = getValueByKey("frmSearchCustomer.TakeAway")
                    Case Else
                        Me.Text = getValueByKey("frmSearchCustomer")
                End Select
            End If
            Me.RadioBtnCLPCustm.Text = getValueByKey("frmSearchCustomer.RadioBtnCLPCustm")
            Me.RadioBtnSalesCustm.Text = getValueByKey("frmSearchCustomer.RadioBtnSalesCustm")
            Me.lblsearch.Text = getValueByKey("frmSearchCustomer.lblsearch")
            Me.BtnViewOrderHistory.Text = getValueByKey("frmSearchCustomer.BtnViewOrderHistory")
            Me.BtnNewCustomer.Text = getValueByKey("frmSearchCustomer.BtnNewCustomer")
            Me.BtnEditCustomer.Text = getValueByKey("frmSearchCustomer.BtnEditCustomer")
            Me.btnOk.Text = getValueByKey("frmSearchCustomer.btnOk")
            Me.btnExit.Text = getValueByKey("frmSearchCustomer.btnExit")
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub GridColumnSettings()
        '     If grdCLPCustomerList.Cols.Count > 0 Then
        'Dim displayColumns As String = "CustomerName,MobileNo,CustomerNo,FirstName,SurName,EmailID,BirthDate,BalancePoint,AddressLn1,City,State,Country"
        Dim displayColumns As String
        If clsDefaultConfiguration.DetailedCustomerCreationformat = "0" Then
            'code added by vipul for showing customer name with level
            If clsDefaultConfiguration.customerwisepricemanagement = True Then
                displayColumns = "CustomerName,Level,MobileNo,RESPHONE,Address,CustomerNo,BalancePoint,EmailID,BirthDate,City"
            Else
                displayColumns = "CustomerName,MobileNo,RESPHONE,Address,CustomerNo,BalancePoint,EmailID,BirthDate,City"
            End If
        Else

            displayColumns = "SrNo,CustomerName,CompanyName,Department,MobileNo,Residence Number,Address,Email Address,BIRTHDATE,BalancePoint,CustomerNo"
        End If
        Dim columnsList = displayColumns.ToUpper().Split(",")

        For colIndex = 0 To grdCLPCustomerList.Cols.Count - 1 Step 1
            If columnsList.Contains(grdCLPCustomerList.Cols(colIndex).Name.ToUpper()) Then
                grdCLPCustomerList.Cols(colIndex).Visible = True
            Else
                grdCLPCustomerList.Cols(colIndex).Visible = False
            End If
        Next
        grdCLPCustomerList.Cols("SrNo").Caption = "Sr.No."
        grdCLPCustomerList.Cols("MobileNo").Visible = _isMobileNoViewAllowed
        grdCLPCustomerList.Cols("MobileNo").Caption = getValueByKey("frmSearchCustomer.grdCLPCustomerList.MobileNo")
        grdCLPCustomerList.Cols("RESPHONE").Caption = getValueByKey("frmSearchCustomer.grdCLPCustomerList.RESPHONE")
        grdCLPCustomerList.Cols("CustomerNo").Caption = getValueByKey("frmSearchCustomer.grdCLPCustomerList.CustomerNo")
        grdCLPCustomerList.Cols("CustomerName").Caption = getValueByKey("frmSearchCustomer.grdCLPCustomerList.CustomerName")
        grdCLPCustomerList.Cols("CompanyName").Caption = "Company Name"
        grdCLPCustomerList.Cols("FirstName").Caption = getValueByKey("frmSearchCustomer.grdCLPCustomerList.FirstName")
        grdCLPCustomerList.Cols("SurName").Caption = getValueByKey("frmSearchCustomer.grdCLPCustomerList.SurName")
        grdCLPCustomerList.Cols("EmailID").Caption = getValueByKey("frmSearchCustomer.grdCLPCustomerList.EmailID")
        grdCLPCustomerList.Cols("BirthDate").Caption = getValueByKey("frmSearchCustomer.grdCLPCustomerList.BirthDate")
        grdCLPCustomerList.Cols("Address").Caption = getValueByKey("frmSearchCustomer.grdCLPCustomerList.Address")
        grdCLPCustomerList.Cols("City").Caption = getValueByKey("frmSearchCustomer.grdCLPCustomerList.City")
        grdCLPCustomerList.Cols("State").Caption = getValueByKey("frmSearchCustomer.grdCLPCustomerList.State")
        grdCLPCustomerList.Cols("Country").Caption = getValueByKey("frmSearchCustomer.grdCLPCustomerList.Country")


        If (grdCLPCustomerList.Cols("BalancePoint") IsNot Nothing) Then
            grdCLPCustomerList.Cols("BalancePoint").Caption = getValueByKey("frmSearchCustomer.grdCLPCustomerList.BalancePoint")
        End If
        grdCLPCustomerList.AutoSizeCols()
        grdCLPCustomerList.Cols("Address").Width = 375
        grdCLPCustomerList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            For i = 0 To grdCLPCustomerList.Cols.Count - 1
                grdCLPCustomerList.Cols(i).Caption = grdCLPCustomerList.Cols(i).Caption.ToUpper
            Next
        End If
        '    End If
    End Sub

    Private Sub RadioCLPCustmer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If RadioBtnCLPCustm.Checked Then
                CustmType = "CLP"
                RadioBtnSalesCustm.Checked = False
                BtnNewCustomer.Enabled = True
                BtnEditCustomer.Enabled = True
                objClp.CustomerSearchParameters = clsDefaultConfiguration.CustSearchParameter
                dtCustmData = objClp.GetCustomerInformation(CustmType, clsAdmin.SiteCode, clsAdmin.CLPProgram, String.Empty, isAddressCombined:=True, vFilterVal:=txtFilterCustomer.Text, CustFormat:=clsDefaultConfiguration.DetailedCustomerCreationformat, IsNewSalesOrder:=clsDefaultConfiguration.IsNewSalesOrder, CustomerWisePrice:=clsDefaultConfiguration.customerwisepricemanagement)
                grdCLPCustomerList.DataSource = dtCustmData
                GridColumnSettings()
                If dtCustmData.Rows.Count > 0 Then
                    grdCLPCustomerList.Select()
                End If

            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub RadioSalesCustmer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If RadioBtnSalesCustm.Checked Then
                CustmType = "SO"
                RadioBtnCLPCustm.Checked = False
                BtnNewCustomer.Visible = True
                BtnNewCustomer.Enabled = False
                BtnEditCustomer.Enabled = False
                txtFilterCustomer.Value = String.Empty

                dtCustmData = objClp.GetCustomerInformation(CustmType, clsAdmin.SiteCode, clsAdmin.CLPProgram, String.Empty, True, CustomerWisePrice:=clsDefaultConfiguration.customerwisepricemanagement)
                grdCLPCustomerList.DataSource = dtCustmData

                If dtCustmData.Rows.Count > 0 Then
                    GridColumnSettings()
                    grdCLPCustomerList.Select()
                Else
                    ShowMessage("No Data Found", "Information")
                End If

                txtFilterCustomer.Select()
                txtFilterCustomer.Select(txtFilterCustomer.Text.Length, 0)
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnNewCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNewCustomer.Click
        Try
            If RadioBtnCLPCustm.Checked Then

                If FormName = "frmNLoyalityCustomer" Then

                    If clsDefaultConfiguration.DetailedCustomerCreationformat = "0" Then
                        Dim objClpCustomer As New frmNLoyalityCustomer
                        objClpCustomer.NewClpCustomer = True

                        If (objClpCustomer.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                            If (objClpCustomer.dtCustmInfo IsNot Nothing AndAlso objClpCustomer.dtCustmInfo.Rows.Count > 0) Then
                                Me.vCustomerNo = objClpCustomer.dtCustmInfo().Rows(0)("CustomerNo")
                                _dtCustmInfo = objClpCustomer.dtCustmInfo()
                                Me.DialogResult = Windows.Forms.DialogResult.OK
                            End If
                            Me.Close()
                        End If
                        objClpCustomer.Dispose()
                    Else

                        Dim objClpCustomer As New frmNewCustomer
                        objClpCustomer.CustomerNo = String.Empty

                        If (objClpCustomer.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                            If (objClpCustomer.dtCustmInfo IsNot Nothing AndAlso objClpCustomer.dtCustmInfo.Rows.Count > 0) Then
                                Me.vCustomerNo = objClpCustomer.dtCustmInfo().Rows(0)("CustomerNo")
                                _dtCustmInfo = objClpCustomer.dtCustmInfo()
                                Me.DialogResult = Windows.Forms.DialogResult.OK
                            End If
                            Me.Close()
                        End If
                        objClpCustomer.Dispose()

                    End If


                Else
                    If Not String.IsNullOrEmpty(txtFilterCustomer.Text) Then
                        If IsNumeric(txtFilterCustomer.Text) Then
                            Dim cardNo = getCLPcustomerByMobileNo(clsAdmin.CLPProgram, txtFilterCustomer.Text)
                            If (cardNo IsNot Nothing AndAlso Not String.IsNullOrEmpty(cardNo)) Then
                                Dim vFilterCustmInfo As String = "CUSTOMERNO='" & cardNo & "' "
                                dvCustmInfo = New DataView(dtCustmData, vFilterCustmInfo, "", DataViewRowState.OriginalRows)
                                _dtCustmInfo = dvCustmInfo.ToTable()
                                Me.DialogResult = Windows.Forms.DialogResult.OK
                                Me.Close()
                                Exit Sub
                            End If
                        End If
                    End If

                    If clsDefaultConfiguration.DetailedCustomerCreationformat = "0" Then
                        Dim objClpCustomer As New frmNSearchCustomer
                        objClpCustomer.CustomerNo = String.Empty
                        objClpCustomer.AccessCustomerOutside = True
                        objClpCustomer.ShowSO = RadioBtnSalesCustm.Checked
                        objClpCustomer.ShowCLP = RadioBtnCLPCustm.Checked
                        If Not String.IsNullOrEmpty(txtFilterCustomer.Text) Then
                            objClpCustomer.SearchedValue = txtFilterCustomer.Text
                        End If
                        If Not String.IsNullOrEmpty(txtSearchByPhn.Text) Then
                            objClpCustomer.SearchedValue = txtSearchByPhn.Text
                        End If

                        If (objClpCustomer.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                            If (objClpCustomer.dtCustmInfo IsNot Nothing AndAlso objClpCustomer.dtCustmInfo.Rows.Count > 0) Then
                                Me.vCustomerNo = objClpCustomer.dtCustmInfo().Rows(0)("CustomerNo")
                                _dtCustmInfo = objClpCustomer.dtCustmInfo()
                                CustmType = objClpCustomer.dtCustmInfo().Rows(0)("CUSTOMERTYPE")
                                Me.DialogResult = Windows.Forms.DialogResult.OK
                            End If
                            Me.Close()
                        End If
                        objClpCustomer.Dispose()
                    Else
                        Dim objClpCustomer As New frmNewCustomer
                        objClpCustomer.CustomerNo = String.Empty
                        If Not String.IsNullOrEmpty(txtFilterCustomer.Text) Then
                            objClpCustomer.SearchedValue = txtFilterCustomer.Text
                        End If
                        If (objClpCustomer.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                            If (objClpCustomer.dtCustmInfo IsNot Nothing AndAlso objClpCustomer.dtCustmInfo.Rows.Count > 0) Then
                                Me.vCustomerNo = objClpCustomer.dtCustmInfo().Rows(0)("CustomerNo")
                                _dtCustmInfo = objClpCustomer.dtCustmInfo()
                                Me.DialogResult = Windows.Forms.DialogResult.OK
                            End If
                            Me.Close()
                        End If
                        objClpCustomer.Dispose()
                    End If
                End If



            ElseIf RadioBtnSalesCustm.Checked Then
                Dim objCreateNewCustm As New frmNSOCustomer
                objCreateNewCustm.Tag = "NEW"
                objCreateNewCustm.ShowDialog()

                Me.vCustomerNo = objCreateNewCustm.vCustomerNo
                objCreateNewCustm.Dispose()

            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnEditCustomer_Click(sender As System.Object, e As System.EventArgs) Handles BtnEditCustomer.Click
        Try
            If Not (grdCLPCustomerList.Row = Nothing) AndAlso grdCLPCustomerList.Row >= 0 Then

                If RadioBtnCLPCustm.Checked Then
                    If FormName = "frmNLoyalityCustomer" Then
                        If clsDefaultConfiguration.DetailedCustomerCreationformat = "0" Then
                            btnOk_Click(sender, e)
                        Else
                            Dim objClpCustomer As New frmNewCustomer
                            objClpCustomer.CustomerNo = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "CustomerNo")

                            If (objClpCustomer.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                                If (objClpCustomer.dtCustmInfo IsNot Nothing AndAlso objClpCustomer.dtCustmInfo.Rows.Count > 0) Then
                                    Me.vCustomerNo = objClpCustomer.dtCustmInfo().Rows(0)("CustomerNo")
                                    _dtCustmInfo = objClpCustomer.dtCustmInfo()
                                    Me.DialogResult = Windows.Forms.DialogResult.OK
                                End If
                                objClpCustomer.Dispose()
                                Me.Close()
                            End If
                        End If
                    Else
                        Me.vCustomerNo = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "CustomerNo")
                        If clsDefaultConfiguration.DetailedCustomerCreationformat = "0" Then
                            Dim objClpCustomer As New frmNSearchCustomer
                            objClpCustomer.CustomerNo = Me.vCustomerNo
                            objClpCustomer.AccessCustomerOutside = True
                            objClpCustomer._SOCustomer = False

                            If (objClpCustomer.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                                If (objClpCustomer.dtCustmInfo IsNot Nothing AndAlso objClpCustomer.dtCustmInfo.Rows.Count > 0) Then
                                    Me.vCustomerNo = objClpCustomer.dtCustmInfo().Rows(0)("CustomerNo")
                                    _dtCustmInfo = objClpCustomer.dtCustmInfo()
                                    Me.DialogResult = Windows.Forms.DialogResult.OK
                                End If
                                objClpCustomer.Dispose()
                                Me.Close()
                            End If
                        Else
                            Dim objClpCustomer As New frmNewCustomer
                            objClpCustomer.CustomerNo = Me.vCustomerNo

                            If (objClpCustomer.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                                If (objClpCustomer.dtCustmInfo IsNot Nothing AndAlso objClpCustomer.dtCustmInfo.Rows.Count > 0) Then
                                    Me.vCustomerNo = objClpCustomer.dtCustmInfo().Rows(0)("CustomerNo")
                                    _dtCustmInfo = objClpCustomer.dtCustmInfo()
                                    Me.DialogResult = Windows.Forms.DialogResult.OK
                                End If
                                objClpCustomer.Dispose()
                                Me.Close()
                            End If
                        End If

                    End If
                ElseIf RadioBtnSalesCustm.Checked Then
                    Dim objCreateNewCustm As New frmNSOCustomer
                    objCreateNewCustm.Tag = "NEW"
                    objCreateNewCustm.ShowDialog()

                    Me.vCustomerNo = objCreateNewCustm.vCustomerNo
                    objCreateNewCustm.Dispose()

                End If
            Else
                ShowMessage("Please select a customer first", getValueByKey("CLAE04"))
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try
            If grdCLPCustomerList.Rows.Count > 1 Then
                Dim vFilterCustmInfo As String = "CUSTOMERNO='" & grdCLPCustomerList.Item(grdCLPCustomerList.Row, "CUSTOMERNO") & "' "
                If Not IsDBNull(grdCLPCustomerList.Item(grdCLPCustomerList.Row, "AddressType")) Then
                    _AddressType = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "AddressType")
                    'Else
                    '    _AddressType = 1
                End If

                ' _AddressType = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "AddressType")

                If CustmType = "CLP" Then
                    If Not IsDBNull(grdCLPCustomerList.Item(grdCLPCustomerList.Row, "CARDTYPE")) Then
                        _CardType = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "CARDTYPE")
                    Else
                        _CardType = ""
                    End If
                Else
                    _CardType = ""
                End If
                dvCustmInfo = New DataView(dtCustmData, vFilterCustmInfo, "", DataViewRowState.OriginalRows)
                _dtCustmInfo = dvCustmInfo.ToTable()
                If clsDefaultConfiguration.DetailedCustomerCreationformat = "1" Then
                    Me.vCustomerNo = _dtCustmInfo.Rows(0)("CustomerNo")
                End If
                If _IsVaidyNoteButtonClicked = True Then
                    Me.DialogResult = Windows.Forms.DialogResult.Yes
                Else
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                End If
                Me.Close()
                If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
                    Dim drp() = Process.GetProcessesByName("osk")
                    If drp.Length > 0 Then
                        Dim proc As New Process
                        For Each pr As Process In Process.GetProcesses()
                            If pr.ProcessName = "osk" Then
                                pr.Kill()
                            End If

                        Next
                    End If
                End If
            Else
                MsgBox(getValueByKey("CLPLK001"), , "CLPLK001 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        _CustmType = ""
        _AddressType = ""
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        _dtCustmInfo = Nothing
        Me.Close()
    End Sub

    Private Sub grdCLPCustomerList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCLPCustomerList.DoubleClick
        Try
            Try
                If FormName = "frmNLoyalityCustomer" Then
                    If clsDefaultConfiguration.DetailedCustomerCreationformat = "0" Then
                        btnOk_Click(sender, e)
                    Else
                        Dim objClpCustomer As New frmNewCustomer
                        objClpCustomer.CustomerNo = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "CustomerNo")

                        If (objClpCustomer.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                            If (objClpCustomer.dtCustmInfo IsNot Nothing AndAlso objClpCustomer.dtCustmInfo.Rows.Count > 0) Then
                                Me.vCustomerNo = objClpCustomer.dtCustmInfo().Rows(0)("CustomerNo")
                                _dtCustmInfo = objClpCustomer.dtCustmInfo()
                                Me.DialogResult = Windows.Forms.DialogResult.OK
                            End If
                            objClpCustomer.Dispose()
                            Me.Close()
                        End If
                    End If
                ElseIf FormName = "frmMembershipEnrollment" Then
                    If clsDefaultConfiguration.IsMembership Then
                        If clsDefaultConfiguration.DetailedCustomerCreationformat = "0" Then
                            btnOk_Click(sender, e)
                        Else
                            Dim objClpCustomer As New frmNewCustomer
                            objClpCustomer.CustomerNo = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "CustomerNo")

                            If (objClpCustomer.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                                If (objClpCustomer.dtCustmInfo IsNot Nothing AndAlso objClpCustomer.dtCustmInfo.Rows.Count > 0) Then
                                    Me.vCustomerNo = objClpCustomer.dtCustmInfo().Rows(0)("CustomerNo")
                                    _dtCustmInfo = objClpCustomer.dtCustmInfo()
                                    Me.DialogResult = Windows.Forms.DialogResult.OK
                                End If
                                objClpCustomer.Dispose()
                                Me.Close()
                            End If
                        End If
                    End If
                Else
                    btnOk_Click(sender, e)
                End If

            Catch ex As Exception
                LogException(ex)
            End Try
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private Sub grdCLPCustomerList_PreviewKeyDown(sender As Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles grdCLPCustomerList.PreviewKeyDown
        If (e.KeyValue = Keys.Enter) Then
            btnOk_Click(sender, e)
        End If
    End Sub

    Private Sub FilterCustomer()
        Try
            Dim rowfilterString As String = String.Empty
            Dim filterString As New System.Text.StringBuilder()


            'Dim displayColumns As String = "MobileNo,CustomerNo,CustomerName,FirstName,SurName,EmailID,BirthDate,BalancePoint,AddressLn1,City,State,Country"
            'Dim columnsList = displayColumns.ToUpper().Split(",")

            'If (Not String.IsNullOrEmpty(txtFilterCustomer.Text.Trim())) Then
            '    For colIndex = 0 To grdCLPCustomerList.Cols.Count - 1 Step 1
            '        If columnsList.Contains(grdCLPCustomerList.Cols(colIndex).Name.ToUpper()) AndAlso _
            '            Not grdCLPCustomerList.Cols(colIndex).Name.ToUpper.Equals("BirthDate".ToUpper()) AndAlso _
            '            Not grdCLPCustomerList.Cols(colIndex).Name.ToUpper.Equals("BalancePoint".ToUpper()) Then
            '            filterString.AppendFormat("{0} LIKE '%{1}%' OR ", grdCLPCustomerList.Cols(colIndex).Name, txtFilterCustomer.Text.Trim())

            '        End If

            '        If columnsList.Contains(grdCLPCustomerList.Cols(colIndex).Name.ToUpper()) AndAlso _
            '                               grdCLPCustomerList.Cols(colIndex).Name.ToUpper.Equals("BalancePoint".ToUpper()) Then
            '            filterString.AppendFormat("Convert({0}, 'System.String') LIKE '%{1}%' OR ", grdCLPCustomerList.Cols(colIndex).Name, txtFilterCustomer.Text.Trim())

            '        End If
            '    Next
            '    rowfilterString = filterString.ToString().Substring(0, filterString.ToString().Length - 3)
            '    'rowfilterString = "FirstName LIKE '%" & txtFilterCustomer.Text.Trim() & "%' OR Convert(BalancePoint, 'System.String') LIKE '%" & txtFilterCustomer.Text.Trim() & "%'"

            'End If
            Dim filterText() As String = txtFilterCustomer.Text.ToString().Trim().Split(Space(1))

            For index = 0 To filterText.Count - 1
                filterString.AppendFormat("Convert({0}, 'System.String') LIKE '%{1}%' AND ", "FILTER", filterText(index))
            Next




            rowfilterString = filterString.ToString().Substring(0, filterString.ToString().Length - 4)
            dtCustmData.DefaultView.RowFilter = rowfilterString
            grdCLPCustomerList.DataSource = dtCustmData.DefaultView
            GridColumnSettings()

            If (grdCLPCustomerList.Rows.Count > 1) Then
                grdCLPCustomerList.Select(1, 3)
            End If
            txtFilterCustomer.Select()
            txtFilterCustomer.Select(txtFilterCustomer.Text.Length, 0)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtFilterCustomer_PreviewKeyDown(sender As Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtFilterCustomer.PreviewKeyDown
        If (e.KeyValue = Keys.Enter) Then
            ''' Added perform Okey Click
            If FormName = "frmNLoyalityCustomer" Then
                If clsDefaultConfiguration.DetailedCustomerCreationformat = "0" Then
                    btnOk_Click(btnOk, New System.EventArgs)
                Else
                    Dim objClpCustomer As New frmNewCustomer
                    objClpCustomer.CustomerNo = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "CustomerNo")

                    If (objClpCustomer.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                        If (objClpCustomer.dtCustmInfo IsNot Nothing AndAlso objClpCustomer.dtCustmInfo.Rows.Count > 0) Then
                            Me.vCustomerNo = objClpCustomer.dtCustmInfo().Rows(0)("CustomerNo")
                            _dtCustmInfo = objClpCustomer.dtCustmInfo()
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                        End If
                        objClpCustomer.Dispose()
                        Me.Close()
                    End If
                End If
            Else
                btnOk_Click(btnOk, New System.EventArgs)
            End If
        End If
    End Sub

    Private Sub txtFilterCustomer_TextChanged(sender As Object, e As System.EventArgs) Handles txtFilterCustomer.TextChanged
        If txtFilterCustomer.Text.ToString.Length >= clsDefaultConfiguration.CustSearchCharpos Then
            If IsSearchDb = False AndAlso Not dtCustmData Is Nothing AndAlso dtCustmData.Rows.Count > 0 Then
                FilterCustomer()
            Else
                RadioCLPCustmer_CheckedChanged(Nothing, EventArgs.Empty)
                IsSearchDb = False
            End If
        Else
            IsSearchDb = True
        End If
        txtFilterCustomer.Select()
        txtFilterCustomer.Select(txtFilterCustomer.Text.Length, 0)
    End Sub

    Private Sub txtFilterCustomer_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtFilterCustomer.KeyDown
        Try
            'If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            '    e.Handled = True
            'End If
            If e.Shift AndAlso (e.KeyCode = Keys.D0 Or e.KeyCode = Keys.D1 Or e.KeyCode = Keys.D2 _
                     Or e.KeyCode = Keys.D3 Or e.KeyCode = Keys.D4 Or e.KeyCode = Keys.D5 _
                     Or e.KeyCode = Keys.D6 Or e.KeyCode = Keys.D7 Or e.KeyCode = Keys.D8 Or e.KeyCode = Keys.OemQuestion Or e.KeyCode = Keys.OemPeriod _
                     Or e.KeyCode = Keys.D9 Or e.KeyCode = Keys.O Or e.KeyCode = Keys.Oemtilde Or e.KeyCode = Keys.OemMinus Or e.KeyCode = Keys.Oemplus) Then
                e.SuppressKeyPress = True
            End If

            If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Or e.KeyCode = Keys.OemBackslash Or e.KeyCode = Keys.OemCloseBrackets Or e.KeyCode = Keys.Oemcomma _
               Or e.KeyCode = Keys.OemMinus Or e.KeyCode = Keys.OemOpenBrackets Or e.KeyCode = Keys.OemPipe Or e.KeyCode = Keys.Oemplus Or e.KeyCode = Keys.OemQuestion _
               Or e.KeyCode = Keys.OemQuotes Or e.KeyCode = Keys.OemSemicolon Or e.KeyCode = Keys.Oemtilde Or e.KeyCode = Keys.OemPeriod Then
                e.Handled = True
                e.SuppressKeyPress = True
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnViewOrderHistory_Click(sender As System.Object, e As System.EventArgs) Handles BtnViewOrderHistory.Click
        Try
            If Not (grdCLPCustomerList.Row = Nothing) AndAlso grdCLPCustomerList.Row >= 0 Then

                Dim ChildForm As New Spectrum.frmViewOrderDetails
                ChildForm.pSearchCust = "VIEW"
                ChildForm.CustomerNo = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "CustomerNo")
                ChildForm.CustomerName = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "CustomerName")
                ChildForm.BalancePoint = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "BalancePoint")
                ChildForm.ShowDialog()
            Else
                ShowMessage("Please select a customer first", getValueByKey("CLAE04"))
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub btnViewSalesOrderHistory_Click(sender As Object, e As EventArgs) Handles btnViewSalesOrderHistory.Click
        If Not (grdCLPCustomerList.Row = Nothing) AndAlso grdCLPCustomerList.Row >= 0 Then

            Dim ChildForm As New Spectrum.frmPCSalesOrderHistory
            ' ChildForm.pSearchCust = "VIEW"
            ChildForm.CustomerNo = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "CustomerNo")
            ChildForm.CustomerName = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "CustomerName")
            ChildForm.BalancePoint = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "BalancePoint")
            ChildForm.CompName = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "CompanyName")
            ChildForm.DepartmentName = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "Department")
            ChildForm.MobileNo = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "MobileNo")
            ChildForm.ShowDialog()
        Else
            ShowMessage("Please select a customer first", getValueByKey("CLAE04"))
        End If
    End Sub

    Private Sub txtSearchByPhn_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchByPhn.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            If RadioBtnCLPCustm.Checked Then
                CustmType = "CLP"
                RadioBtnSalesCustm.Checked = False
                BtnNewCustomer.Enabled = True
                BtnEditCustomer.Enabled = True
                objClp.CustomerSearchParameters = "Mobile No."
                dtCustmData = objClp.GetCustomerInformation(CustmType, clsAdmin.SiteCode, clsAdmin.CLPProgram, String.Empty, isAddressCombined:=True, vFilterVal:=txtSearchByPhn.Text.Trim, CustFormat:=clsDefaultConfiguration.DetailedCustomerCreationformat, IsNewSalesOrder:=clsDefaultConfiguration.IsNewSalesOrder, SearchByPhone:=clsDefaultConfiguration.CUSTOMERSEARCHUSINGPHONENUMBER, CustomerWisePrice:=clsDefaultConfiguration.customerwisepricemanagement)
                grdCLPCustomerList.DataSource = dtCustmData
                GridColumnSettings()
                If dtCustmData.Rows.Count >= 1 Then
                    grdCLPCustomerList.Select()
                Else
                    txtSearchByPhn.Select()
                    txtSearchByPhn.Select(txtSearchByPhn.Text.Length, 0)
                    ShowMessage("No Record Found", getValueByKey("CLAE04"))
                End If

            End If
        End If
    End Sub
    'Private Sub btnViewVaidyaNotes_Click(sender As Object, e As EventArgs) Handles btnViewVaidyaNotes.Click
    
    'End Sub
    Private Function ThemeChange()
        Dim Panel1 As New Panel
        grdCLPCustomerList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
        grdCLPCustomerList.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        grdCLPCustomerList.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        grdCLPCustomerList.Rows.MinSize = 24
        grdCLPCustomerList.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        grdCLPCustomerList.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdCLPCustomerList.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdCLPCustomerList.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
        grdCLPCustomerList.Styles.Focus.BackColor = Color.FromArgb(212, 212, 212)

        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Insert(0, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.30853!))
        Me.TableLayoutPanel1.ColumnStyles.Insert(1, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.69147!))
        Me.TableLayoutPanel1.ColumnStyles.Insert(2, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.topButtonPanel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.grdCLPCustomerList, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.buttomButtonPanel, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.None
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Insert(0, New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.RowStyles.Insert(1, New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Insert(2, New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 137.0!))
        Me.TableLayoutPanel1.RowStyles.Insert(3, New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(902, 474)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'topButtonPanel

        Me.topButtonPanel.BackColor = Color.FromArgb(134, 134, 134)
        Me.topButtonPanel.ColumnCount = 9
        Me.TableLayoutPanel1.SetColumnSpan(Me.topButtonPanel, 3)
        Me.topButtonPanel.ColumnStyles.Insert(0, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.topButtonPanel.ColumnStyles.Insert(1, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.topButtonPanel.ColumnStyles.Insert(2, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.topButtonPanel.ColumnStyles.Insert(3, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 19.0!))
        Me.topButtonPanel.ColumnStyles.Insert(4, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230.0!))
        Me.topButtonPanel.ColumnStyles.Insert(5, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.topButtonPanel.ColumnStyles.Insert(6, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142.0!))
        Me.topButtonPanel.ColumnStyles.Insert(7, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.topButtonPanel.ColumnStyles.Insert(8, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.topButtonPanel.Controls.Add(Me.lblPhone, 7, 0)
        Me.topButtonPanel.Controls.Add(Me.lblsearch, 5, 0)
        Me.topButtonPanel.Controls.Add(Me.txtSearchByPhn, 6, 0)
        Me.topButtonPanel.Controls.Add(Me.txtFilterCustomer, 4, 0)
        Me.topButtonPanel.Controls.Add(Panel1, 1, 0)
        Me.topButtonPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.topButtonPanel.Location = New System.Drawing.Point(3, 3)
        Me.topButtonPanel.Name = "topButtonPanel"
        Me.topButtonPanel.RowCount = 1
        Me.topButtonPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.topButtonPanel.Size = New System.Drawing.Size(896, 29)
        Me.topButtonPanel.TabIndex = 7






        '   lblsearch

        Me.lblsearch.AutoSize = True
        Me.lblsearch.BackColor = System.Drawing.Color.Transparent
        Me.lblsearch.BorderColor = System.Drawing.Color.Transparent
        Me.lblsearch.BorderStyle = BorderStyle.None
        Me.lblsearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblsearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsearch.ForeColor = System.Drawing.Color.Black
        Me.lblsearch.Image = Global.Spectrum.My.Resources.Resources.SearchItems1
        Me.lblsearch.Location = New System.Drawing.Point(576, 0)
        Me.lblsearch.Size = New System.Drawing.Size(34, 29)
        Me.lblsearch.Text = ""
        Me.lblsearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPhone
        '

        Me.lblPhone.BackColor = System.Drawing.Color.Transparent
        Me.lblPhone.BorderColor = System.Drawing.Color.Transparent
        Me.lblPhone.BorderStyle = BorderStyle.None
        Me.lblPhone.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhone.ForeColor = System.Drawing.Color.Black
        Me.lblPhone.Image = Global.Spectrum.My.Resources.Resources.SwipeCard
        Me.lblPhone.Location = New System.Drawing.Point(758, 0)
        Me.lblPhone.Size = New System.Drawing.Size(36, 29)
        Me.lblPhone.Text = ""
        Me.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSearchByPhn
        '
        Me.txtSearchByPhn.AutoSize = False
        Me.txtSearchByPhn.Dock = System.Windows.Forms.DockStyle.None
        Me.txtSearchByPhn.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtSearchByPhn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchByPhn.Location = New System.Drawing.Point(616, 3)
        Me.txtSearchByPhn.MaximumSize = New System.Drawing.Size(400, 21)
        Me.txtSearchByPhn.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtSearchByPhn.Size = New System.Drawing.Size(136, 21)

        '
        'txtFilterCustomer
        '
        Me.txtFilterCustomer.AutoSize = False
        Me.txtFilterCustomer.Dock = System.Windows.Forms.DockStyle.None
        Me.txtFilterCustomer.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtFilterCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilterCustomer.Location = New System.Drawing.Point(346, 3)
        Me.txtFilterCustomer.MaximumSize = New System.Drawing.Size(400, 21)
        Me.txtFilterCustomer.MinimumSize = New System.Drawing.Size(10, 21)
        txtFilterCustomer.Width = 224
        Me.txtFilterCustomer.Size = New System.Drawing.Size(224, 21)

        ''Panel1
        ''
        Panel1.BackColor = System.Drawing.Color.White
        Panel1.Dock = System.Windows.Forms.DockStyle.None
        Me.topButtonPanel.SetColumnSpan(Panel1, 1)
        'Panel1.Controls.Add(Me.RadioBtnSalesCustm)
        Panel1.Controls.Add(Me.RadioBtnCLPCustm)
        Panel1.Location = New System.Drawing.Point(27, 3)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(294, 23)
        '
        'RadioBtnSalesCustm
        '
        Me.RadioBtnSalesCustm.BackColor = System.Drawing.Color.White
        RadioBtnSalesCustm.Dock = System.Windows.Forms.DockStyle.None
        Me.RadioBtnSalesCustm.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.RadioBtnSalesCustm.Location = New System.Drawing.Point(151, -1)
        Me.RadioBtnSalesCustm.Size = New System.Drawing.Size(150, 21)
        '
        'RadioBtnCLPCustm
        '

        Me.RadioBtnCLPCustm.BackColor = System.Drawing.Color.White
        RadioBtnCLPCustm.Dock = System.Windows.Forms.DockStyle.None
        Me.RadioBtnCLPCustm.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.RadioBtnCLPCustm.Location = New System.Drawing.Point(19, 0)
        Me.RadioBtnCLPCustm.Size = New System.Drawing.Size(100, 21)
        ''
        ''grdCLPCustomerList

        Me.grdCLPCustomerList.Size = New System.Drawing.Size(896, 276)
        '  Me.grdCLPCustomerList.StyleInfo = Resources.GetString("grdCLPCustomerList.StyleInfo")
        Me.grdCLPCustomerList.TabIndex = 1
        ''
        ''buttomButtonPanel
        ''
        Me.buttomButtonPanel.ColumnCount = 8
        Me.TableLayoutPanel1.SetColumnSpan(Me.buttomButtonPanel, 3)
        Me.buttomButtonPanel.ColumnStyles.Insert(0, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.94172!))
        Me.buttomButtonPanel.ColumnStyles.Insert(1, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 154.0!))
        Me.buttomButtonPanel.ColumnStyles.Insert(2, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.buttomButtonPanel.ColumnStyles.Insert(3, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.buttomButtonPanel.ColumnStyles.Insert(4, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.buttomButtonPanel.ColumnStyles.Insert(5, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.buttomButtonPanel.ColumnStyles.Insert(6, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.08392!))
        Me.buttomButtonPanel.ColumnStyles.Insert(7, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.97436!))
        '  Me.buttomButtonPanel.Controls.Add(Me.BtnEditCustomer, 5, 0)
        '  Me.buttomButtonPanel.Controls.Add(Me.BtnNewCustomer, 4, 0)
        'code added for issue id 1521 by vipul
        Me.buttomButtonPanel.Controls.Add(Me.BtnEditCustomer, 4, 0)
        Me.buttomButtonPanel.Controls.Add(Me.BtnNewCustomer, 3, 0)

        Me.buttomButtonPanel.Controls.Add(Me.btnViewSalesOrderHistory, 3, 0)
        Me.buttomButtonPanel.Controls.Add(Me.BtnViewOrderHistory, 2, 0)
        Me.buttomButtonPanel.Controls.Add(Me.btnExit, 4, 2)
        Me.buttomButtonPanel.Controls.Add(Me.btnOk, 3, 2)
        Me.buttomButtonPanel.Location = New System.Drawing.Point(3, 320)
        Me.buttomButtonPanel.Name = "buttomButtonPanel"
        Me.buttomButtonPanel.RowCount = 3
        Me.buttomButtonPanel.RowStyles.Insert(0, New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.buttomButtonPanel.RowStyles.Insert(1, New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.0!))
        Me.buttomButtonPanel.RowStyles.Insert(2, New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38.0!))
        Me.buttomButtonPanel.Size = New System.Drawing.Size(896, 131)
        Me.buttomButtonPanel.TabIndex = 6
        '


        'BtnEditCustomer
        '
        Me.BtnEditCustomer.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.BtnEditCustomer.Image = Global.Spectrum.My.Resources.Resources.EditCustomer_Normla
        Me.BtnEditCustomer.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnEditCustomer.Location = New System.Drawing.Point(542, 3)
        Me.BtnEditCustomer.Name = "BtnEditCustomer"
        Me.BtnEditCustomer.Size = New System.Drawing.Size(94, 68)
        Me.BtnEditCustomer.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnEditCustomer.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.BtnEditCustomer.BackColor = Color.White
        BtnEditCustomer.Text = "&Edit Customer"
        BtnEditCustomer.Text = BtnEditCustomer.Text.ToUpper
        '
        'BtnNewCustomer
        '
        Me.BtnNewCustomer.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.BtnNewCustomer.Image = Global.Spectrum.My.Resources.Resources.NewCustomer_Normal
        Me.BtnNewCustomer.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnNewCustomer.Location = New System.Drawing.Point(442, 3)
        Me.BtnNewCustomer.Size = New System.Drawing.Size(94, 68)
        Me.BtnNewCustomer.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnNewCustomer.BackColor = Color.White
        Me.BtnNewCustomer.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.BtnNewCustomer.BackColor = Color.White
        BtnNewCustomer.Text = "&New Customer"
        BtnNewCustomer.Text = BtnNewCustomer.Text.ToUpper
        '
        'btnViewSalesOrderHistory
        '
        Me.btnViewSalesOrderHistory.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnViewSalesOrderHistory.Image = Global.Spectrum.My.Resources.Resources.SalesOrderHistory_Normal
        Me.btnViewSalesOrderHistory.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnViewSalesOrderHistory.Location = New System.Drawing.Point(342, 3)
        Me.btnViewSalesOrderHistory.Size = New System.Drawing.Size(94, 68)
        Me.btnViewSalesOrderHistory.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnViewSalesOrderHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnViewSalesOrderHistory.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnViewSalesOrderHistory.BackColor = Color.White
        btnViewSalesOrderHistory.Text = btnViewSalesOrderHistory.Text.ToUpper
        btnViewSalesOrderHistory.Text = btnViewSalesOrderHistory.Text.ToUpper
        '
        'BtnViewOrderHistory
        '
        Me.BtnViewOrderHistory.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.BtnViewOrderHistory.Image = Global.Spectrum.My.Resources.Resources.Order_Normal
        Me.BtnViewOrderHistory.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnViewOrderHistory.Location = New System.Drawing.Point(242, 3)
        Me.BtnViewOrderHistory.Size = New System.Drawing.Size(94, 68)
        Me.BtnViewOrderHistory.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnViewOrderHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnViewOrderHistory.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.BtnViewOrderHistory.BackColor = Color.White
        BtnViewOrderHistory.Text = BtnViewOrderHistory.Text.ToUpper
        '
        'btnExit
        '
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnExit.Location = New System.Drawing.Point(442, 96)
        Me.btnExit.Size = New System.Drawing.Size(94, 30)
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnExit.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnExit.BackColor = Color.Transparent
        btnExit.BackColor = Color.FromArgb(0, 107, 163)
        btnExit.ForeColor = Color.FromArgb(255, 255, 255)
        btnExit.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        btnExit.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnExit.FlatStyle = FlatStyle.Flat
        btnExit.Text = btnExit.Text.ToUpper
        btnExit.MaximumSize = New Size(92, 32)
        '
        'btnOk
        '
        Me.btnOk.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnOk.Location = New System.Drawing.Point(342, 96)
        Me.btnOk.Size = New System.Drawing.Size(94, 32)
        Me.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnOk.BackColor = Color.Transparent
        btnOk.BackColor = Color.FromArgb(0, 107, 163)
        btnOk.ForeColor = Color.FromArgb(255, 255, 255)
        btnOk.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        btnOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnOk.FlatStyle = FlatStyle.Flat
        btnOk.Text = btnOk.Text.ToUpper
        ''
        ''frmSearchCustomer
        ''

        Me.BackColor = System.Drawing.Color.White
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    End Function

    'Private Sub btnViewVaidyaNotes_Click(sender As System.Object, e As System.EventArgs) Handles btnViewVaidyaNotes.Click
    '    If grdCLPCustomerList.Rows.Count > 1 Then
    '        Dim CustomerId As String = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "CUSTOMERNO")

    '        If CustomerId <> String.Empty AndAlso CustomerId <> Nothing Then
    '            Dim objPatientinfo As New clsHcPatientInfo
    '            If objPatientinfo.IsPatientPrescriptionAvaliable(CustomerId) Then
    '                Dim hcClsNote As New frmHCConsultantsNotes
    '                'hcClsNote.dtConsultantAndPatientDtl = Nothing
    '                hcClsNote.vPatientID = CustomerId
    '                hcClsNote.ShowDialog()
    '                If hcClsNote.DialogResult = Windows.Forms.DialogResult.Yes Then
    '                    dtPrescDtlSrchAddToBill = hcClsNote.dtPrescDtlAddToBill.Copy()
    '                    Me.DialogResult = Windows.Forms.DialogResult.Yes
    '                    _IsVaidyNoteButtonClicked = True
    '                    btnOk_Click(btnOk, New System.EventArgs)
    '                    Me.Close()
    '                Else
    '                    _IsVaidyNoteButtonClicked = False
    '                End If
    '            Else
    '                ShowMessage("The Selected Customer is not a Patient", "Inforamtion")
    '                Exit Sub
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub btnViewVaidyaNotes_Click(sender As System.Object, e As System.EventArgs) Handles btnViewVaidyaNotes.Click
        If grdCLPCustomerList.Rows.Count > 1 Then
            Dim CustomerId As String = grdCLPCustomerList.Item(grdCLPCustomerList.Row, "CUSTOMERNO")

            If CustomerId <> String.Empty AndAlso CustomerId <> Nothing Then
                Dim objPatientinfo As New clsHcPatientInfo
                If objPatientinfo.IsPatientPrescriptionAvaliable(CustomerId) Then
                    Dim hcClsNote As New frmHCConsultantsNotes
                    'hcClsNote.dtConsultantAndPatientDtl = Nothing
                    hcClsNote.vPatientID = CustomerId
                    hcClsNote.ShowDialog()
                    If hcClsNote.DialogResult = Windows.Forms.DialogResult.Yes Then
                        dtPrescDtlSrchAddToBill = hcClsNote.dtPrescDtlAddToBill.Copy()
                        Me.DialogResult = Windows.Forms.DialogResult.Yes
                        _IsVaidyNoteButtonClicked = True
                        btnOk_Click(btnOk, New System.EventArgs)
                        Me.Close()
                    Else
                        _IsVaidyNoteButtonClicked = False
                    End If
                Else
                    ShowMessage("The Selected Customer is not a Patient", "Inforamtion")
                    Exit Sub
                End If
            End If
        Else
            ShowMessage("Patient not selected", "Inforamtion")
            Exit Sub
        End If
    End Sub
End Class