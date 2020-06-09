Imports SpectrumBL
Imports System.Data
Imports System.Data.SqlClient
Imports SpectrumCommon
Public Class CtrlCustSearch
    Dim ObjCustDtls As Object
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Event btnSearchCustom(ByVal dtCustmInfo As DataTable)
    Public Event CustomerChanged(ByVal CLPCustomer As CLPCustomerDTO)
    Dim objCustm As New clsCLPCustomer()

    Dim dvAddressType As DataView
    Dim drAddress As DataRow
    Dim vSiteCode As String = clsAdmin.SiteCode
    Dim _dtCustmInfo As DataTable
    Dim _CustmType As String

    Public Property dtCustmInfo() As DataTable
        Get
            Return _dtCustmInfo
        End Get
        Set(ByVal value As DataTable)
            _dtCustmInfo = value
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
    Public Property CardType() As String
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



    Private _BorderColorVisible As Boolean
    Public Property BorderColorVisible() As Boolean
        Get
            Return _BorderColorVisible
        End Get
        Set(ByVal value As Boolean)
            _BorderColorVisible = value
            If _BorderColorVisible = False Then
                C1Sizer1.Border.Color = Color.Transparent
            End If

        End Set
    End Property

    Private _pshowSwapeCard As Boolean = False
    Public Property pshowSwapeCard() As Boolean
        Get
            Return _pshowSwapeCard
        End Get
        Set(ByVal value As Boolean)
            _pshowSwapeCard = value
            lblSwapeCard.Visible = value
            CtrlTxtSwapeCard.Visible = value
        End Set
    End Property

    Sub pClearCtrl()
        CtrlTxtCustNo.Clear()
        CtrlTxtSwapeCard.Clear()
        If Me.Parent.Controls("CtrlCustDtls1") IsNot Nothing Then
            ObjCustDtls = Me.Parent.Controls("CtrlCustDtls1")
            ObjCustDtls.pDisplayDtls(dtCustmInfo)
        End If
    End Sub

    Private Sub rbCLPMember_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbCLPMember.CheckedChanged
        pClearCtrl()
        ObjCustDtls.pclear()
        pshowSwapeCard = Not (rbOtherCust.Checked)
        If rbCLPMember.Checked = True Then
            _CustmType = "CLP"
            If clsDefaultConfiguration.IsManualCLPCustomerSearch = False Then
                CtrlTxtCustNo.ReadOnly = True
            Else
                CtrlTxtCustNo.ReadOnly = False
            End If
        Else
            _CustmType = "SO"
            CtrlTxtCustNo.ReadOnly = False
        End If
    End Sub

    Private Sub CtrlCustSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If clsDefaultConfiguration.IsManualCLPCustomerSearch = False Then
                CtrlTxtCustNo.ReadOnly = True
            Else
                CtrlTxtCustNo.ReadOnly = False
            End If
            CtrlTxtCustNo.ReadOnly = True
            rbOtherCust.Enabled = False
            CtrlBtnNew.Visible = clsDefaultConfiguration.IsOtherCustomerRequired
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception

        End Try
       
    End Sub

    Public Sub CtrlBtn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlBtn1.Click
        If clsDefaultConfiguration.IsOtherCustomerRequired = False Then
            If clsDefaultConfiguration.DetailedCustomerCreationformat = "0" Then
                Dim objCust As New frmNSearchCustomer
                objCust.ShowSO = False
                objCust.IsCustSearch = True
                objCust.BtnSearchCustomer.Enabled = True
                objCust.txtCustomerCode.ReadOnly = False
                objCust.ShowDialog()
                _dtCustmInfo = objCust.dtCustmInfo()
            Else
                Dim objCust As New frmNewCustomer
                objCust.IsCustSearch = True
                objCust.ShowDialog()
                _dtCustmInfo = objCust.dtCustmInfo()
            End If
            SetCustomerInformationInForm(_dtCustmInfo)
        Else
            If rbOtherCust.Checked = False Then
                _CustmType = "CLP"

                If clsDefaultConfiguration.IsManualCLPCustomerSearch = True Or clsDefaultConfiguration.IsManualCLPCustomerSearch Then
                    If OnlineConnect = False Then
                        ShowMessage(getValueByKey("CLCSTS01"), getValueByKey("CLAE04"))
                        Exit Sub
                    Else
                        '----Code Changed By Mahesh Nagar for changes Search Customer function ...
                        'Dim objBrwClp As New frmNSearchCLPLookUp(CustmType, vSiteCode, String.Empty)
                        'objBrwClp.Text = getValueByKey("frmnsearchclplookupclp")
                        'objBrwClp.WindowState = FormWindowState.Maximized
                        'objBrwClp.ShowDialog()
                        'objBrwClp.Focus()

                        '_dtCustmInfo = objBrwClp.dtCustmInfo
                        '_AddressType = objBrwClp.AddressType
                        '_CardType = objBrwClp.CardType
                        'SetCustomerInformationInForm(_dtCustmInfo)
                        'objBrwClp.Dispose()

                        'Dim objCust As New frmNSearchCustomer
                        'objCust.ShowSO = False
                        'objCust.IsCustSearch = True
                        'objCust.BtnSearchCustomer.Enabled = True
                        'objCust.txtCustomerCode.ReadOnly = False
                        'objCust.ShowDialog()

                        Dim dtCust As New DataTable
                        If clsDefaultConfiguration.DetailedCustomerCreationformat = "0" Then
                            Dim objCust As New frmNSearchCustomer
                            objCust.ShowSO = False
                            objCust.IsCustSearch = True
                            objCust.BtnSearchCustomer.Enabled = True
                            objCust.txtCustomerCode.ReadOnly = False
                            objCust.ShowDialog()
                            dtCust = objCust.dtCustmInfo()
                        Else
                            Dim objCust As New frmNewCustomer
                            objCust.IsCustSearch = True
                            objCust.ShowDialog()
                            dtCust = objCust.dtCustmInfo()

                        End If

                        If dtCust IsNot Nothing Then
                            _dtCustmInfo = dtCust
                            If Not _dtCustmInfo Is Nothing AndAlso _dtCustmInfo.Rows.Count > 0 Then
                                _AddressType = dtCustmInfo.Rows(0)("AddressType").ToString
                                _CardType = IIf(dtCustmInfo.Rows(0)("CardType") Is DBNull.Value, 0, dtCustmInfo.Rows(0)("CardType"))                  ' dtCustmInfo.Rows(0)("CardType")
                            Else
                                _AddressType = String.Empty
                                _CardType = String.Empty
                            End If
                            SetCustomerInformationInForm(_dtCustmInfo)
                        End If
                    End If

                Else
                    MsgBox(getValueByKey("CLCSTS03"), MsgBoxStyle.Critical, getValueByKey("CLAE04"))
                End If
            Else
                CtrlTxtCustNo.ReadOnly = False
                _CustmType = "SO"
                Dim objBrwClp As New frmNSearchCLPLookUp(CustmType, vSiteCode, String.Empty)
                objBrwClp.WindowState = FormWindowState.Maximized
                objBrwClp.Text = getValueByKey("frmnsearchclplookupothercust")
                objBrwClp.ShowDialog()
                objBrwClp.Focus()

                _dtCustmInfo = objBrwClp.dtCustmInfo
                _AddressType = objBrwClp.AddressType
                _CardType = objBrwClp.CardType
                SetCustomerInformationInForm(_dtCustmInfo)
                objBrwClp.Dispose()
            End If
            RaiseEvent btnSearchCustom(_dtCustmInfo)
        End If






    End Sub
    Private Function SetCustomerInformationInForm(ByVal dtCustmInfo As DataTable) As Boolean

        Try
            If Not dtCustmInfo Is Nothing AndAlso dtCustmInfo.Rows.Count > 0 Then

                CtrlTxtCustNo.Text = dtCustmInfo.Rows(0).Item("CUSTOMERNO")
                ObjCustDtls.pDisplayDtls(dtCustmInfo)
                SetFocusToScanItemFld()
                Dim customers = SetCustomerVal(dtCustmInfo)
                RaiseEvent CustomerChanged(customers)
            Else
                Dim tempCustNo As String = ""
                tempCustNo = CtrlTxtCustNo.Text
                ObjCustDtls._dtCustmInfo = Nothing
                ObjCustDtls.pclear()
                pClearCtrl()
                CtrlTxtCustNo.Value = tempCustNo

                'Rakesh-29.10.2013:8232
                'ShowMessage(getValueByKey("CLCSTS02"), getValueByKey("CLAE04"))
            End If

        Catch ex As Exception
        End Try
    End Function
    Private Function SetCustomerVal(ByVal dt As DataTable) As CLPCustomerDTO
        If dt.Rows.Count > 0 Then
            Dim add As New List(Of CLPCustomerAddressDTO)

            For Each dr As DataRow In dt.Rows
                Dim address As New CLPCustomerAddressDTO() With {.AddLine1 = dr("ADDRESSLN1").ToString(), .AddLine2 = dr("AddressLN2").ToString(), .AddLine3 = dr("AddressLN3").ToString(), .AddLine4 = dr("AddressLN4").ToString(), .AddressType = dr("AddressType").ToString(), .CardNumber = dr("AccountNO").ToString(), .City = dr("City").ToString(), .ClpProgId = dr("CLPProgramID").ToString(), .Country = dr("Country").ToString(), .State = dr("State").ToString()}
                add.Add(address)
            Next
            Dim clpcustoer = New CLPCustomerDTO() With {.BalancePoints = dt(0)("BalancePoint"), .CardNumber = dt(0)("AccountNo"), .ClpProgId = dt(0)("CLPProgramID"), .EmailId = dt(0)("EmailId"), .AddressList = add}
            Date.TryParse(dt(0)("BirthDate"), clpcustoer.BirthDate)
            Return clpcustoer
        End If

    End Function

    Private Sub CtrlTxtCustNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CtrlTxtCustNo.KeyDown
        Try
            If (e.KeyCode = Keys.Enter) Then
                If Not (CtrlTxtCustNo.Text = "") AndAlso Not (CtrlTxtCustNo.Text = String.Empty) Then
                    _dtCustmInfo = objCustm.GetCustomerInformation(CustmType, vSiteCode, clsAdmin.CLPProgram, CtrlTxtCustNo.Text)
                    SetCustomerInformationInForm(dtCustmInfo)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    'ADDSP
    Public Sub CtrlBtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlBtnNew.Click
        If rbOtherCust.Checked = True Then
            Dim ChildForm As New Spectrum.frmNSOCustomer
            ChildForm.Tag = "NEW"
            ChildForm.ShowDialog()
            If ChildForm.Customerno <> String.Empty Then
                CtrlTxtCustNo.Text = ChildForm.Customerno
                _dtCustmInfo = objCustm.GetCustomerInformation(CustmType, vSiteCode, clsAdmin.CLPProgram, CtrlTxtCustNo.Text)
                SetCustomerInformationInForm(dtCustmInfo)
            End If
        ElseIf rbCLPMember.Checked = True Then
            'MsgBox(getValueByKey("CLCSTS04"), MsgBoxStyle.Information, getValueByKey("CLAE04"))
        End If

    End Sub
    'ADDSP
    Private Sub SetFocusToScanItemFld()
        Try
            If UCase(Me.ParentForm.Name) = "FRMNSALESORDERCREATION" Or UCase(Me.ParentForm.Name) = "FRMNSALESORDERUPDATE" Then
                Me.ParentForm.Controls("c1sizergrid").Controls("CtrlSalesPersons").Controls("c1Sizer1").Controls("ctrltxtbox").Select()
            ElseIf UCase(Me.ParentForm.Name) = "FRMNBIRTHLISTCREATE" Or UCase(Me.ParentForm.Name) = "FRMNBIRTHLISTSALES" Or UCase(Me.ParentForm.Name) = "FRMNBIRTHLISTUPDATE" Then
                Me.ParentForm.Controls("c1sizergrid").Controls("CtrlSalesPerson1").Controls("c1Sizer1").Controls("ctrltxtbox").Select()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function Themechange()

        C1Sizer1.SplitterWidth = 1
        Me.CtrlLblCustNo.BackColor = Color.FromArgb(212, 212, 212)
        Me.CtrlLblCustNo.Font = New Font("NeoSans", 7, FontStyle.Regular)
        Me.CtrlLblCustNo.TextAlign = ContentAlignment.MiddleLeft
        Me.CtrlLblCustNo.ForeColor = Color.FromArgb(37, 37, 37)
        Me.CtrlLblCustNo.Text = Me.CtrlLblCustNo.Text.ToUpper
        CtrlLblCustNo.BorderStyle = Windows.Forms.BorderStyle.None


        Me.rbOtherCust.BackColor = Color.FromArgb(212, 212, 212)
        Me.rbOtherCust.TextAlign = ContentAlignment.MiddleLeft
        Me.rbOtherCust.ForeColor = Color.FromArgb(37, 37, 37)
        rbOtherCust.Text = "Other Cust".ToUpper
        Me.rbOtherCust.Font = New Font("Neo Sans", 7, FontStyle.Regular)
        rbOtherCust.FlatAppearance.BorderSize = 0
        rbOtherCust.CheckAlign = ContentAlignment.MiddleRight
        Me.CtrlTxtCustNo.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        rbCLPMember.FlatAppearance.BorderSize = 0
        rbCLPMember.CheckAlign = ContentAlignment.MiddleRight
        Me.rbCLPMember.BackColor = Color.FromArgb(212, 212, 212)
        Me.rbCLPMember.TextAlign = ContentAlignment.MiddleLeft
        rbCLPMember.Size = New Size(116, 24)
        Me.rbCLPMember.ForeColor = Color.FromArgb(37, 37, 37)
        Me.rbCLPMember.Font = New Font("Neo Sans", 7, FontStyle.Regular)
        rbCLPMember.Text = rbCLPMember.Text.ToUpper
        CtrlBtn1.FlatAppearance.BorderSize = 0
        CtrlBtn1.VisualStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtn1.Image = Nothing
        CtrlBtn1.BackgroundImage = Global.Spectrum.My.Resources.SearchItems
        CtrlBtn1.BackgroundImageLayout = ImageLayout.Stretch
        CtrlBtn1.FlatStyle = FlatStyle.Flat
        CtrlBtn1.Location = New Point(176, 2)
        CtrlBtn1.BringToFront()

        CtrlBtnNew.Location = New Point(201, 2)
        CtrlBtnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnNew.Text = ""
        CtrlBtnNew.FlatAppearance.BorderSize = 0
        CtrlBtnNew.VisualStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnNew.Image = Nothing
        CtrlBtnNew.BackgroundImage = Global.Spectrum.My.Resources.AddPlus
        CtrlBtnNew.BackgroundImageLayout = ImageLayout.Stretch
        CtrlBtnNew.FlatStyle = FlatStyle.Flat


        'CtrlBtnNew.BackColor = Color.Transparent
        'CtrlBtnNew.BackColor = Color.FromArgb(0, 107, 163)
        'CtrlBtnNew.ForeColor = Color.FromArgb(255, 255, 255)
        'CtrlBtnNew.Font = New Font("Neo Sans", 12, FontStyle.Bold)
        'CtrlBtnNew.FlatAppearance.BorderSize = 0
        'CtrlBtnNew.FlatStyle = FlatStyle.Flat
        ' CtrlBtnNew.TextAlign = ContentAlignment.TopCenter

        Me.lblSwapeCard.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblSwapeCard.Font = New Font("NeoSans", 7, FontStyle.Regular)
        Me.lblSwapeCard.TextAlign = ContentAlignment.MiddleLeft
        Me.lblSwapeCard.ForeColor = Color.FromArgb(37, 37, 37)
        Me.lblSwapeCard.Text = Me.lblSwapeCard.Text.ToUpper
        lblSwapeCard.BorderStyle = Windows.Forms.BorderStyle.None
        Me.CtrlTxtSwapeCard.Font = New Font("Neo Sans", 9, FontStyle.Regular)

    End Function
End Class
