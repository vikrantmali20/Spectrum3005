Imports System.Text
Imports SpectrumBL
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Public Class frmNewReservation

#Region "Global Variables"
    Dim x As Integer = 0
    Dim y As Integer = 0
    Dim prev As Object
    Dim current As Object
    Dim NewOrderOfTable As New Button
    Dim NewLabelOfTable As New Label
    Dim objCM As New clsCashMemo
    Dim objCustm As New clsSOCustomer
    Dim objcomm As New clsCommon
    Dim objReservation As New clsReservation
    Dim dtDinNumber As New DataTable
    Dim dtOrderNumber As New DataTable
    Dim dineInTableColor As Object
    Dim assignTableProperty As Boolean = False
    Dim isTableLoad As Boolean = False
    Dim TableNo As String = ""
    Dim IsEditLoad As Boolean = False
    Dim dtReserv As New DataTable
    Dim NewDineInTable As Button
    Dim CrntSelectedTables As New List(Of Short)
    Dim PreviousEditTables As New List(Of Short)
    Dim dscustData As New DataSet
#End Region

#Region "Properties"
    Dim _IsChangeTime As Boolean = False
    Public Property IsChangeTime() As Boolean
        Get
            Return _IsChangeTime
        End Get
        Set(ByVal value As Boolean)
            _IsChangeTime = value
        End Set
    End Property
    Dim _IsCheckIn As Boolean = False
    Public Property IsCheckIn() As Boolean
        Get
            Return _IsCheckIn
        End Get
        Set(ByVal value As Boolean)
            _IsCheckIn = value
        End Set
    End Property

    Dim _IsEdit As Boolean = False
    Public Property IsEdit() As Boolean
        Get
            Return _IsEdit
        End Get
        Set(ByVal value As Boolean)
            _IsEdit = value
        End Set
    End Property

    Dim _SwitchTable As Boolean = False
    Public Property SwitchTable As Boolean
        Get
            Return _SwitchTable
        End Get
        Set(ByVal value As Boolean)
            _SwitchTable = value
        End Set
    End Property
    Dim _IsGenerateBillColor As Integer = 0
    Public Property IsGenerateBillColor As Integer
        Get
            Return _IsGenerateBillColor
        End Get
        Set(ByVal value As Integer)
            _IsGenerateBillColor = value
        End Set
    End Property
    Dim _DinInBillNo As String
    Public Property DinInBillNo As String
        Get
            Return _DinInBillNo
        End Get
        Set(ByVal value As String)
            _DinInBillNo = value
        End Set
    End Property
    Dim _OldDinInBillNo As String
    Public Property OldDinInBillNo As String
        Get
            Return _OldDinInBillNo
        End Get
        Set(ByVal value As String)
            _OldDinInBillNo = value
        End Set
    End Property
    Dim _CustomerNo As String
    Public Property CustomerNo As String
        Get
            Return _CustomerNo
        End Get
        Set(ByVal value As String)
            _CustomerNo = value
        End Set
    End Property
    Dim _ReservationIdEdit As String
    Public Property ReservationIdEdit As String
        Get
            Return _ReservationIdEdit
        End Get
        Set(ByVal value As String)
            _ReservationIdEdit = value
        End Set
    End Property


    Dim _DineInProcess As String
    Public Property DineInProcess As String
        Get
            Return _DineInProcess
        End Get
        Set(ByVal value As String)
            _DineInProcess = value
        End Set
    End Property

    Dim _AddToDineInTable As Boolean
    Public Property AddToDineInTable As Boolean
        Get
            Return _AddToDineInTable
        End Get
        Set(ByVal value As Boolean)
            _AddToDineInTable = value
        End Set
    End Property

    Dim _DineInNumber As String
    Public Property DineInNumber As String
        Get
            Return _DineInNumber
        End Get
        Set(ByVal value As String)
            _DineInNumber = value
        End Set
    End Property
    Dim _CustomerName As String
    Public Property CustomerName As String
        Get
            Return _CustomerName
        End Get
        Set(ByVal value As String)
            _CustomerName = value
        End Set
    End Property
    Dim _NoOfPeople As String
    Public Property NoOfPeople As String
        Get
            Return _NoOfPeople
        End Get
        Set(ByVal value As String)
            _NoOfPeople = value
        End Set
    End Property
    Dim _Phone As String
    Public Property Phone As String
        Get
            Return _Phone
        End Get
        Set(ByVal value As String)
            _Phone = value
        End Set
    End Property
    Dim _custDate As DateTime
    Public Property custDate As DateTime
        Get
            Return _custDate
        End Get
        Set(ByVal value As DateTime)
            _custDate = value
        End Set
    End Property
    Dim _FromTime As DateTime
    Public Property FromTime As DateTime
        Get
            Return _FromTime
        End Get
        Set(ByVal value As DateTime)
            _FromTime = value
        End Set
    End Property

    Dim _ToTime As DateTime
    Public Property ToTime As DateTime
        Get
            Return _ToTime
        End Get
        Set(ByVal value As DateTime)
            _ToTime = value
        End Set
    End Property

    Dim _PreviousFromTime As DateTime
    Public Property PreviousFromTime As DateTime
        Get
            Return _PreviousFromTime
        End Get
        Set(ByVal value As DateTime)
            _PreviousFromTime = value
        End Set
    End Property

    Dim _PreviousToTime As DateTime
    Public Property PreviousToTime As DateTime
        Get
            Return _PreviousToTime
        End Get
        Set(ByVal value As DateTime)
            _PreviousToTime = value
        End Set
    End Property

    Dim _OldDineInNumber As String
    Public Property OldDineInNumber As String
        Get
            Return _OldDineInNumber
        End Get
        Set(ByVal value As String)
            _OldDineInNumber = value
        End Set
    End Property

    Private ReadOnly Property ButtonSize As System.Drawing.Size
        Get
            Return New Size(80, 80)
        End Get
    End Property
    Private ReadOnly Property ButtonSize1 As System.Drawing.Size
        Get
            Return New Size(150, 40)
        End Get
    End Property
#End Region

#Region "Functions"
    Private Function Themechange()
        'Me.BackgroundColor = Color.FromArgb(255, 255, 255)
        'cmdAssign.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'cmdAssign.TextAlign = ContentAlignment.MiddleCenter
        'cmdAssign.BackColor = Color.Transparent
        'cmdAssign.BackColor = Color.FromArgb(0, 107, 163)
        'cmdAssign.BackColor = Color.FromArgb(102, 102, 255)
        'cmdAssign.ForeColor = Color.FromArgb(255, 255, 255)
        'cmdAssign.Font = New Font("Neo Sans", 11, FontStyle.Bold)
        'cmdAssign.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        'cmdAssign.FlatStyle = FlatStyle.Flat
        'cmdAssign.FlatAppearance.BorderSize = 0
        'cmdAssign.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        'Button1.FlatAppearance.BorderSize = 5
        'lblTableName.BackColor = Color.FromArgb(229, 229, 229)
        'lblTableName.AutoSize = False
        'lblTableName.TextAlign = ContentAlignment.MiddleCenter
        'lblTableName.Dock = DockStyle.Top
        'lblTableName.BorderStyle = BorderStyle.None
        'Panel1.BorderStyle = BorderStyle.FixedSingle
        'cmdAssign.Location = New Point(100, 47)
        'cmdAssign.Size = New Size(140, 37)
        'cmdAssign.TextAlign = ContentAlignment.MiddleCenter
        'articlePanel.BorderStyle = BorderStyle.FixedSingle
        'lblTableName.ForeColor = Color.FromArgb(0, 81, 120)
        'lblTableName.Font = New Font("Neo Sans", 13, FontStyle.Bold)
        'lblTableName.MinimumSize = New Size(0, 50)
        'lblTableName.Size = New Size(520, 50)
        btnBook.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnBook.BackColor = Color.Transparent
        btnBook.BackColor = Color.FromArgb(0, 107, 163)
        btnBook.ForeColor = Color.FromArgb(255, 255, 255)
        btnBook.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnBook.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnBook.FlatStyle = FlatStyle.Flat
        btnBook.FlatAppearance.BorderSize = 0
        btnBook.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnBook.Size = New Size(98, 36)
        btnBook.TextAlign = ContentAlignment.MiddleCenter

        btnSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnSearch.BackColor = Color.Transparent
        btnSearch.BackColor = Color.FromArgb(0, 107, 163)
        btnSearch.ForeColor = Color.FromArgb(255, 255, 255)
        btnSearch.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSearch.FlatStyle = FlatStyle.Flat
        btnSearch.FlatAppearance.BorderSize = 0
        btnSearch.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnSearch.Size = New Size(120, 36)
        btnSearch.TextAlign = ContentAlignment.MiddleCenter

    End Function
    Private Sub AddDineInTable(ByVal DineInNumber As Integer)
        Try
            Dim Str As New StringBuilder
            NewDineInTable = New Button
            'New Spectrum.CtrlBtn
            NewDineInTable.Enabled = True
            NewDineInTable.Size = New Size(65, 65)
            NewDineInTable.Name = "btn" & DineInNumber.ToString
            Dim drDineInTable As DataRow = dtDinNumber.Rows(DineInNumber - 1)
            If drDineInTable("TableStatus").ToString() = "Booked" Then
                If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                    'NewDineInTable.BackgroundImage = My.Resources.DineRed
                    NewDineInTable.BackgroundImage = My.Resources.blue
                    NewDineInTable.BackColor = Color.Blue
                Else
                    NewDineInTable.BackgroundImage = My.Resources.blue
                    NewDineInTable.BackColor = Color.Blue
                End If
                'NewDineInTable.BackgroundImage = System.Drawing.Image.FromFile("Product.jpg")
            Else
                If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                    NewDineInTable.BackgroundImage = My.Resources.DineGreen
                    NewDineInTable.BackColor = Color.Green
                Else
                    NewDineInTable.BackgroundImage = My.Resources.DineGreen
                    NewDineInTable.BackColor = Color.Green
                End If
            End If
            Str.Append(drDineInTable("DineInNumber").ToString())
            'NewDineInTable.Text = "T" & drDineInTable("DineInNumber").ToString()
            NewDineInTable.Text = Str.ToString()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                NewDineInTable.Text = NewDineInTable.Text.ToUpper
                NewDineInTable.Font = New Font("New Sans Intel", 18.0F, FontStyle.Bold)
            Else
                NewDineInTable.Font = New Font("New Sans Intel", 18.0F, FontStyle.Bold)
            End If

            NewDineInTable.ForeColor = Color.White
            ' Use Tag property to store "which button" information
            NewDineInTable.Tag = DineInNumber
            ' Add button click handler
            AddHandler NewDineInTable.Click, AddressOf NewDineInTable_Click
            Me.ReservationPanel.Controls.Add(NewDineInTable)

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Function ValidatedSOCustomer() As Boolean
        Try
            If CrntSelectedTables.Count = 0 Then
                ShowMessage("Please select atleast one table", getValueByKey("CLAE04"))
                Return IsValidated
                Exit Function
            ElseIf txtName.Text = "" Then
                ShowMessage("Please enter name", getValueByKey("CLAE04"))
                txtName.Focus()
                Return IsValidated
                Exit Function
            ElseIf txtPeople.Text = "" Then
                ShowMessage("Please Enter no. of People", getValueByKey("CLAE04"))
                txtPeople.Focus()
                Return IsValidated
                Exit Function
            ElseIf String.IsNullOrEmpty(txtPhone.Text) Then
                ShowMessage("Mobile Number is Mandatory", getValueByKey("CLAE04"))
                txtPhone.Focus()
                Return IsValidated
            ElseIf dtpDate.Value Is DBNull.Value Then
                ShowMessage("Please select date", getValueByKey("CLAE04"))
                dtpDate.Focus()
                Return IsValidated
                Exit Function
            ElseIf dtpFromTime.Value Is DBNull.Value Then
                ShowMessage("Please select From time", getValueByKey("CLAE04"))
                dtpFromTime.Focus()
                Return IsValidated
                Exit Function
            ElseIf txtPhone.Text.Length <> 10 Then
                ShowMessage("Mobile No. can't be Less than 10 digits", "Information")
                txtPhone.Focus()
                Return IsValidated
                Exit Function
            Else
                If IsEdit AndAlso Not IsCheckIn Then
                    If txtNewTableNo.Text = "" Then
                        ShowMessage("Select New Table", getValueByKey("CLAE04"))
                        txtNewTableNo.Focus()
                        Return IsValidated
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Return False
        End Try
    End Function
    Private Function PrepareDataForSave(ByVal Name As String, ByVal CardNumber As String, ByVal ReservationId As String, ByVal NoPeople As Integer, ByVal TableList As List(Of Short), ByVal MobileNo As String, ByVal dDate As DateTime, ByVal FromTime As DateTime, Optional ByRef dtTableStatus As DataTable = Nothing) As Boolean
        Try
            Dim findKey(4) As Object
            dtReserv = objReservation.GetReservationTableStructure(CardNumber, clsAdmin.SiteCode, IsEdit)
            ' Dim ReservationIdEdit As String = ""
            If Not dtReserv Is Nothing AndAlso dtReserv.Rows.Count > 0 Then
                'Dim dr() = dtReserv.Select("FromTime='" & PreviousFromTime.ToString("yyyy-MM-dd HH:mm:ss.fff") & "'")
                'If dr.Count > 0 Then
                '    ReservationIdEdit = dr(0)("ReservationId")
                'End If
            End If
            If IsEdit Then
                Dim drdtls As DataRow
                For Each dr As DataRow In dtTableStatus.Rows
                    findKey(0) = clsAdmin.SiteCode
                    findKey(1) = clsAdmin.Financialyear
                    findKey(2) = CardNumber
                    findKey(3) = dr("TableNo")
                    findKey(4) = ReservationIdEdit
                    Dim objclscomm As New clsCommon
                    Dim ServerDate = objclscomm.GetCurrentDate()
                    drdtls = dtReserv.Rows.Find(findKey)
                    If drdtls Is Nothing Then
                        drdtls = dtReserv.NewRow
                        drdtls("SiteCode") = clsAdmin.SiteCode
                        drdtls("FinYear") = clsAdmin.Financialyear
                        drdtls("CustomerNo") = CardNumber
                        drdtls("TableNo") = dr("TableNo")
                        drdtls("MobileNo") = MobileNo
                        drdtls("Date") = dDate
                        drdtls("FromTime") = FromTime
                        ' drdtls("ToTime") = ToTime
                        drdtls("ReservationId") = ReservationIdEdit
                        drdtls("CREATEDAT") = clsAdmin.SiteCode
                        drdtls("CREATEDBY") = clsAdmin.UserCode
                        drdtls("CREATEDON") = ServerDate
                        drdtls("UPDATEDAT") = clsAdmin.SiteCode
                        dtReserv.Rows.Add(drdtls)
                    Else
                        drdtls("ReservationId") = ReservationIdEdit
                    End If
                    drdtls("NoOfPeople") = NoPeople
                    drdtls("UPDATEDBY") = clsAdmin.UserCode
                    drdtls("UPDATEDON") = ServerDate
                    drdtls("Remark") = ""
                    drdtls("STATUS") = dr("Status")
                Next
            Else
                If IsChangeTime Then
                    For Each drResStatus As DataRow In dtReserv.Select("ReservationId='" & ReservationIdEdit & "'")
                        drResStatus("Status") = False
                    Next
                End If

                For i As Integer = 0 To TableList.Count - 1
                    Dim dr = dtReserv.NewRow
                    Dim objclscomm As New clsCommon
                    Dim ServerDate = objclscomm.GetCurrentDate()
                    dr("SiteCode") = clsAdmin.SiteCode
                    dr("FinYear") = clsAdmin.Financialyear
                    dr("ReservationId") = ReservationId
                    dr("CustomerNo") = CardNumber
                    dr("NoOfPeople") = NoPeople
                    dr("TableNo") = TableList(i)
                    dr("MobileNo") = MobileNo
                    dr("Date") = dDate
                    dr("FromTime") = FromTime
                    ' dr("ToTime") = ToTime
                    dr("CREATEDAT") = clsAdmin.SiteCode
                    dr("CREATEDBY") = clsAdmin.UserCode
                    dr("CREATEDON") = ServerDate
                    dr("UPDATEDAT") = clsAdmin.SiteCode
                    dr("UPDATEDBY") = clsAdmin.UserCode
                    dr("UPDATEDON") = ServerDate
                    dr("STATUS") = True
                    dr("Remark") = ""
                    dtReserv.Rows.Add(dr)
                Next
            End If


            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Function
    Private Function PrepareCustomerDataForSave(ByVal CustName As String, ByVal CardNumber As String, ByVal MobileNo As String, ByVal SiteCode As String, ByVal UserCode As String, ByVal ClpProgramId As String) As Boolean
        Try
            Dim findKey(2) As Object
            Dim FullName = CustName.Split(" ").ToArray
            Dim FirstName As String = "", MiddleName As String = "", LastName As String = ""
            If FullName.Length = 3 Then
                FirstName = FullName(0)
                MiddleName = FullName(1)
                LastName = FullName(2)
            ElseIf FullName.Length = 2 Then
                FirstName = FullName(0)
                LastName = FullName(1)
            ElseIf FullName.Length = 1 Then
                FirstName = FullName(0)
            End If
            Dim drCustdtls As DataRow
            dscustData = objReservation.GetCustomerDataSet(clsAdmin.SiteCode, CardNumber)
            Dim ReservationIdEdit As String = ""
            findKey(0) = CardNumber
            findKey(1) = ClpProgramId
            findKey(2) = clsAdmin.SiteCode
            drCustdtls = dscustData.Tables(0).Rows.Find(findKey)
            If drCustdtls Is Nothing Then
                drCustdtls = dscustData.Tables(0).NewRow()
                drCustdtls("ClpProgramId") = ClpProgramId
                drCustdtls("CardNo") = CardNumber
                drCustdtls("AccountNo") = CardNumber
                drCustdtls("SiteCode") = clsAdmin.SiteCode
                drCustdtls("Title") = ""
                drCustdtls("BirthDate") = DBNull.Value
                drCustdtls("CardType") = ""
                drCustdtls("Gender") = ""
                drCustdtls("Res_Phone") = ""
                drCustdtls("MobileNo") = MobileNo
                drCustdtls("OfficeNo") = ""
                drCustdtls("Occupation") = ""
                drCustdtls("Education") = ""
                drCustdtls("EmailId") = ""
                drCustdtls("PromotionInfobyEmail") = 0
                drCustdtls("PromotionInfobySMS") = 0
                drCustdtls("ReferedBy") = ""
                drCustdtls("RelationWithPrimaryCard") = ""
                drCustdtls("IsAddOnCard") = 0
                drCustdtls("OldCardType") = ""
                drCustdtls("CustomerGroup") = ""
                drCustdtls("resgistrationstatus") = "Enrolled"
                drCustdtls("SpouseTitle") = ""
                drCustdtls("SpouseFirstName") = ""
                drCustdtls("SpouseSurName") = ""
                drCustdtls("SpouseMiddleName") = ""
                drCustdtls("SpouseDOB") = DBNull.Value
                drCustdtls("SpouseOccupation") = ""
                drCustdtls("TotalBalancePoint") = 0
                drCustdtls("PointsAccumlated") = 0
                drCustdtls("PointsRedeemed") = 0
                drCustdtls("MarriageAnivDate") = DBNull.Value
                drCustdtls("CardExpiryDT") = DateTime.MaxValue.ToString("yyyy-MM-dd")
                drCustdtls("CardIssueDT") = DBNull.Value
                drCustdtls("CardIsActive") = True
                drCustdtls("MaritalStatus") = ""
                drCustdtls("CREATEDAT") = clsAdmin.SiteCode
                drCustdtls("CREATEDBY") = clsAdmin.UserCode
                drCustdtls("CREATEDON") = Now
            End If
            drCustdtls("FirstName") = FirstName
            drCustdtls("MiddleName") = MiddleName
            drCustdtls("SurName") = LastName
            drCustdtls("NameOnCard") = CustName
            drCustdtls("UPDATEDAT") = clsAdmin.SiteCode
            drCustdtls("UPDATEDON") = Now
            drCustdtls("UPDATEDBY") = clsAdmin.UserCode
            drCustdtls("STATUS") = True

            If drCustdtls Is Nothing Then
                dscustData.Tables(0).Rows.Add(drCustdtls)
            End If
            Dim drCustdtlAdds As DataRow
            Dim findnxtKey(1) As Object
            findnxtKey(0) = CardNumber
            findnxtKey(1) = ClpProgramId
            drCustdtlAdds = dscustData.Tables(1).Rows.Find(findnxtKey)
            If drCustdtlAdds Is Nothing Then
                drCustdtlAdds = dscustData.Tables(1).NewRow
                drCustdtlAdds("ClpProgramId") = ClpProgramId
                drCustdtlAdds("CardNo") = CardNumber
                drCustdtlAdds("AddressType") = 1
                drCustdtlAdds("AddressLn1") = ""
                drCustdtlAdds("AddressLn2") = ""
                drCustdtlAdds("AddressLn3") = ""
                drCustdtlAdds("AddressLn4") = ""
                drCustdtlAdds("Pincode") = ""
                drCustdtlAdds("CityCode") = ""
                drCustdtlAdds("StateCode") = ""
                drCustdtlAdds("CountryCode") = ""
                drCustdtlAdds("Defaults") = 1
                drCustdtlAdds("CREATEDAT") = clsAdmin.SiteCode
                drCustdtlAdds("CREATEDBY") = clsAdmin.UserCode
                drCustdtlAdds("CREATEDON") = Now
                drCustdtlAdds("SrNo") = 0
            End If
            drCustdtlAdds("UPDATEDAT") = clsAdmin.SiteCode
            drCustdtlAdds("UPDATEDON") = Now
            drCustdtlAdds("UPDATEDBY") = clsAdmin.UserCode
            drCustdtlAdds("STATUS") = True

            If drCustdtls Is Nothing Then
                dscustData.Tables(1).Rows.Add(drCustdtlAdds)
            End If
            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function
#End Region

#Region "Events"

    ''' <summary>
    ''' Displaying Controls and Data as per modes Change Time,Change Table or CheckIn
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub NewReservation_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            ReservationPanel.Controls.Clear()
            If IsEdit Then
                txtName.Enabled = True
                txtPeople.Enabled = True
                txtPhone.Enabled = True
                If IsCheckIn Then
                    txtPeople.Enabled = True
                End If
                txtName.Text = CustomerName
                txtPeople.Text = NoOfPeople
                txtPhone.Text = Phone
                If IsCheckIn Then
                    txtOldTableNo.Visible = False
                    lblOldTableNo.Visible = False
                    lblNewTableNo.Text = "Table No."
                Else
                    txtOldTableNo.Enabled = True
                    txtOldTableNo.Text = DineInNumber
                End If

                IsEditLoad = True
                dtpDate.Value = custDate
                dtpFromTime.Value = FromTime
                IsEditLoad = False
                dtpFromTime.BackColor = Color.Gray
                dtpDate.BackColor = Color.Gray
                dtpFromTime.Enabled = False
                dtpDate.Enabled = False
                txtName.Enabled = False
                txtPhone.Enabled = False
                btnSearch.Enabled = False
                If IsCheckIn Then
                    txtPeople.Enabled = False
                    btnBook.Text = "CHECK-IN"
                    Me.Text = "Check In"
                Else
                    Me.Text = "Change Table"
                End If
                txtOldTableNo.Enabled = False
                txtNewTableNo.Enabled = False
                btnSearch_Click(sender, e)
            ElseIf IsChangeTime Then
                btnBook.Enabled = True
                btnSearch.Enabled = True
                txtName.Enabled = True
                txtPhone.Enabled = True
                txtPeople.Enabled = True
                txtNewTableNo.Enabled = True
                FromTime = Nothing
                IsEditLoad = True
                dtpDate.Value = DateTime.Now.Date
                dtpFromTime.Value = Nothing
                IsEditLoad = False
                IsEdit = False
                txtName.Text = CustomerName
                txtPeople.Text = NoOfPeople
                txtPhone.Text = Phone
                lblOldTableNo.Visible = False
                lblNewTableNo.Text = "Table No."
                'lblNewTableNo.Visible = False
                txtOldTableNo.Visible = False
                '  txtNewTableNo.Visible = False
                DineInNumber = 0
                CrntSelectedTables.Clear()
                btnBook.Enabled = False
                btnSearch.Enabled = False
                txtName.Enabled = False
                txtPhone.Enabled = False
                txtPeople.Enabled = False
                txtNewTableNo.Enabled = False
                Me.Text = "Change Time"
            Else
                FromTime = Nothing
                CustomerName = ""
                NoOfPeople = ""
                Phone = ""
                IsEditLoad = True
                dtpDate.Value = DateTime.Now.Date
                dtpFromTime.Value = Nothing
                IsEditLoad = False
                txtName.Text = CustomerName
                txtPeople.Text = NoOfPeople
                txtPhone.Text = Phone
                lblOldTableNo.Visible = False
                lblNewTableNo.Visible = False
                txtOldTableNo.Visible = False
                txtNewTableNo.Visible = False
                txtName.Text = ""
                txtPeople.Text = ""
                txtPhone.Text = ""
                DineInNumber = 0
                CrntSelectedTables.Clear()
                btnBook.Enabled = False
                btnSearch.Enabled = False
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Click Events of Tables which are dynamically generated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub NewDineInTable_Click(sender As Object, e As EventArgs)
        Try
            OldDinInBillNo = DinInBillNo
            OldDineInNumber = DineInNumber
            DineInNumber = sender.tag
            prev = sender
            If Not prev.backcolor Is Nothing Then
                If prev.backcolor = Color.Green Then
                    prev.backcolor = Color.Orange
                    prev.BackgroundImage = Spectrum.My.Resources.Resources.DineOrange
                    If IsEdit Then
                        If CrntSelectedTables.Contains(DineInNumber) Then
                        Else
                            CrntSelectedTables.Add(DineInNumber)
                        End If

                    Else
                        CrntSelectedTables.Add(DineInNumber)
                    End If

                ElseIf prev.backcolor = Color.Orange Then
                    prev.backcolor = Color.Green
                    prev.BackgroundImage = Spectrum.My.Resources.Resources.DineGreen
                    If IsEdit Then
                        If CrntSelectedTables.Contains(DineInNumber) Then
                            CrntSelectedTables.Remove(DineInNumber)
                        Else
                            ' CrntSelectedTables.Add(DineInNumber)
                        End If
                    Else
                        CrntSelectedTables.Remove(DineInNumber)
                    End If

                ElseIf prev.backcolor = Color.Blue Then
                    prev.backcolor = Color.Green
                    prev.BackgroundImage = Spectrum.My.Resources.Resources.DineGreen
                    If IsEdit Then
                        If CrntSelectedTables.Contains(DineInNumber) Then
                            CrntSelectedTables.Remove(DineInNumber)
                        Else
                            ' CrntSelectedTables.Add(DineInNumber)
                        End If
                    Else
                        CrntSelectedTables.Remove(DineInNumber)
                    End If

                End If

            End If
            If CrntSelectedTables.Count > 0 Then
                btnBook.Enabled = True
            Else
                btnBook.Enabled = False
            End If
            If (OldDineInNumber = DineInNumber) And isTableLoad Then
                Exit Sub
            End If
            If IsEdit OrElse IsChangeTime Then
                txtNewTableNo.Enabled = True
                txtNewTableNo.Text = DineInNumber
                txtNewTableNo.Enabled = False
            End If
            dineInTableColor = sender.backcolor
            If dineInTableColor = Color.Blue AndAlso txtNewTableNo.Text <> txtOldTableNo.Text Then '-----if current selected table is green
                ShowMessage("Table is already reserved can't update", getValueByKey("CLAE04"))
                txtNewTableNo.Text = txtOldTableNo.Text
                DineInNumber = txtNewTableNo.Text
                Exit Sub
            End If

            '------For Color blue





            'If Not prev Is Nothing Then
            '    If prev.Backcolor = Color.Green Then
            '        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            '            prev.BackgroundImage = Spectrum.My.Resources.Resources.DineGreen
            '        Else
            '            prev.BackgroundImage = Spectrum.My.Resources.Resources.green
            '        End If
            '    ElseIf prev.Backcolor = Color.Blue Then
            '        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            '            prev.BackgroundImage = Spectrum.My.Resources.Resources.blue
            '        Else
            '            prev.BackgroundImage = Spectrum.My.Resources.Resources.blue
            '        End If
            '    End If
            'End If
            '  prev = sender
            'If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            '    prev.BackgroundImage = Spectrum.My.Resources.Resources.DineOrange
            'Else
            '    prev.BackgroundImage = Spectrum.My.Resources.Resources.DineOrange
            'End If
            ' lblTableName.Visible = True
            'cmdAssign.Visible = True
            If AddToDineInTable = True Then
                '  cmdAssign.Enabled = True
            Else
                ' cmdAssign.Enabled = False
            End If

            'lblTableName.Text = "Table No." & sender.tag

            ' articlePanel.Controls.Clear()
            dtOrderNumber = objCM.GetOrderTableWise(clsAdmin.SiteCode, clsAdmin.DayOpenDate, DineInNumber)
            If dtOrderNumber.Rows.Count > 0 Then
                For OrderNumber As Int16 = 0 To dtOrderNumber.Rows.Count - 1
                    Dim orderno As String = dtOrderNumber(OrderNumber)("BillNo").ToString()
                    Dim orderstatus = dtOrderNumber(OrderNumber)("orderstatus")
                    Dim totalprice As String = objCM.GetTotalAmountOrderWise(clsAdmin.SiteCode, clsAdmin.DayOpenDate, orderno)
                    '  AddButton(orderno, FormatNumber(totalprice, 2), orderstatus)
                Next OrderNumber
                isTableLoad = True
            Else
                ' articlePanel.Controls.Clear()
            End If

            '  cmdAssign.ForeColor = Color.White
        Catch ex As Exception
            'ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try


    End Sub

    ''' <summary>
    ''' Saving data of Customer and Reservation as per different modes Change Time,Change Table or CheckIn
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBook_Click(sender As Object, e As EventArgs) Handles btnBook.Click
        Try
            Dim tran As SqlTransaction = Nothing
            Dim objcust As New clsCLPCustomer
            Dim CardNumber As String
            ' TableNo = DineInNumber
            For i = 0 To CrntSelectedTables.Count - 1
                If i = 0 Then
                    TableNo = CrntSelectedTables(i)
                    Continue For
                End If
                TableNo = TableNo.Trim & "," & CrntSelectedTables(i)
            Next
            Dim docNoforReservarion As String = objcomm.getDocumentNo("RSDine", clsAdmin.SiteCode)
            Dim otherCharactersforReservation = "RSD" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3, 3)
            Dim Reservationid = GenDocNo(otherCharactersforReservation, 15, docNoforReservarion)

            If ValidatedSOCustomer() = True Then
                Dim dtCustmInfo = objcust.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, String.Empty, isAddressCombined:=True, vFilterVal:=txtPhone.Text.Trim, CustFormat:=clsDefaultConfiguration.DetailedCustomerCreationformat, IsNewSalesOrder:=clsDefaultConfiguration.IsNewSalesOrder, SearchByPhone:=clsDefaultConfiguration.CUSTOMERSEARCHUSINGPHONENUMBER)

                If Not dtCustmInfo Is Nothing AndAlso dtCustmInfo.Rows.Count > 0 Then
                    CardNumber = dtCustmInfo.Rows(0)("CustomerNo")
                    If PrepareCustomerDataForSave(txtName.Text, CardNumber, txtPhone.Text, clsAdmin.SiteCode, clsAdmin.UserCode, clsAdmin.CLPProgram) = False Then
                        Exit Sub
                    End If
                Else
                    Dim docNo As String = objcomm.getDocumentNo("Customer Loyalty", clsAdmin.SiteCode)
                    Dim otherCharacters = "CLS" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3, 3)
                    CardNumber = GenDocNo(otherCharacters, 15, docNo)
                    Dim TiersList = CLP_Data.GetCLPTiers(clsAdmin.CLPProgram)
                    Dim CardType As String
                    If TiersList.Count > 0 Then
                        CardType = TiersList(0)
                    End If
                    objReservation.InsertCustomerDetails(txtName.Text, CardNumber, txtPhone.Text, clsAdmin.SiteCode, clsAdmin.UserCode, clsAdmin.CLPProgram, CardType)
                End If
                If IsEdit Then
                    Dim dtTableStatus As New DataTable
                    dtTableStatus.Columns.Add("TableNo", System.Type.GetType("System.String"))
                    dtTableStatus.Columns.Add("Status", System.Type.GetType("System.Boolean"))
                    For i = 0 To PreviousEditTables.Count - 1
                        Dim drTS As DataRow = dtTableStatus.NewRow()
                        drTS("TableNo") = PreviousEditTables(i)
                        drTS("Status") = False
                        dtTableStatus.Rows.Add(drTS)
                    Next

                    For j = 0 To CrntSelectedTables.Count - 1
                        Dim drSel() = dtTableStatus.Select("TableNo='" & CrntSelectedTables(j) & "'")
                        If drSel.Count > 0 Then
                            drSel(0)("Status") = True
                        Else
                            Dim drTS As DataRow = dtTableStatus.NewRow()
                            drTS("TableNo") = CrntSelectedTables(j)
                            drTS("Status") = True
                            dtTableStatus.Rows.Add(drTS)
                        End If
                    Next
                    If PrepareDataForSave(txtName.Text, CardNumber, Reservationid, txtPeople.Text, CrntSelectedTables, txtPhone.Text, dtpDate.Value, dtpFromTime.Value, dtTableStatus) = False Then
                        Exit Sub
                    End If

                    If objReservation.UpdateCustDetails(dscustData, True) Then
                        '-Save Sucessfully Customer Details
                    End If
                    If objReservation.UpdateReservDetails(dtReserv) Then
                        If Not IsCheckIn Then
                            ShowMessage("Table Changed Successfully" & vbCrLf & "Name :" & " " & txtName.Text & vbCrLf & "Table No :" & " " & TableNo & vbCrLf & "Date :" & dtpDate.Value & vbCrLf & "Time :" & " " & Convert.ToDateTime(dtpFromTime.Value).ToString("HH:mm tt"), "Reservation Details")
                            Me.Close()
                        End If
                    End If
                    If IsCheckIn Then
                        If objReservation.UpdateCheckInDetails(ReservationIdEdit, CrntSelectedTables, clsAdmin.SiteCode) Then
                            Me.Close()
                        End If
                    End If
                    ' objReservation.UpdateReservationDetails(txtName.Text, txtPeople.Text, TableNo, txtPhone.Text, dtpDeliveryDate.Value, dtpFromTime.Value, txtOldTableNo.Text, txtNewTableNo.Text, clsAdmin.UserName, tran)
                Else
                    If PrepareDataForSave(txtName.Text, CardNumber, Reservationid, txtPeople.Text, CrntSelectedTables, txtPhone.Text, dtpDate.Value, dtpFromTime.Value) = False Then
                        Exit Sub
                    End If
                    If objReservation.UpdateCustDetails(dscustData, True) Then
                        '-Save Sucessfully Customer Details
                    End If
                    If True Then
                    End If
                    If objReservation.UpdateReservDetails(dtReserv) Then
                        If IsChangeTime Then
                            ShowMessage("Time changed successfully" & vbCrLf & "Name :" & " " & txtName.Text & vbCrLf & "Table No :" & " " & TableNo & vbCrLf & "Date :" & dtpDate.Value & vbCrLf & "Time :" & " " & Convert.ToDateTime(dtpFromTime.Value).ToString("hh:mm tt"), "Reservation Details")
                            Me.Close()
                        Else
                            ShowMessage("Reservation successful" & vbCrLf & "Name :" & " " & txtName.Text & vbCrLf & "Table No :" & " " & TableNo & vbCrLf & "Date :" & dtpDate.Value & vbCrLf & "Time :" & " " & Convert.ToDateTime(dtpFromTime.Value).ToString("hh:mm tt"), "Reservation Details")
                        End If

                    End If
                    ' objReservation.InsertReservationDetails(txtName.Text, CardNumber, Reservationid, txtPeople.Text, CrntSelectedTables, txtPhone.Text, dtpDeliveryDate.Value, dtpFromTime.Value, dtpToTime.Value, clsAdmin.SiteCode, clsAdmin.UserCode, clsAdmin.Financialyear, tran)

                End If
                If IsEdit Then
                    CustomerName = txtName.Text
                    NoOfPeople = txtPeople.Text
                    Phone = txtPhone.Text
                    custDate = dtpDate.Value
                    FromTime = dtpFromTime.Value
                    TableNo = ""
                End If
                NewReservation_Load(sender, e)
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub


    ''' <summary>
    ''' on entering mobile no. the data should be fetched from db and displaying on controls
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtPhone_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPhone.KeyDown
        Try
            If (e.KeyCode = Keys.Enter) Then
                If txtPhone.Text.Length <> 10 Then
                    ShowMessage("Mobile No. can't be Less than 10 digits", "Information")
                    txtPhone.Focus()
                    Exit Sub
                End If
                Dim dtCustData As New DataTable
                Dim objClp As New clsCLPCustomer
                dtCustData = objClp.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, String.Empty, isAddressCombined:=True, vFilterVal:=txtPhone.Text.Trim, CustFormat:=clsDefaultConfiguration.DetailedCustomerCreationformat, IsNewSalesOrder:=clsDefaultConfiguration.IsNewSalesOrder, SearchByPhone:=clsDefaultConfiguration.CUSTOMERSEARCHUSINGPHONENUMBER)
                If Not dtCustData Is Nothing AndAlso dtCustData.Rows.Count > 0 Then
                    txtName.Text = dtCustData.Rows(0)("CustomerName")
                    txtPhone.Text = dtCustData.Rows(0)("MobileNo")
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPeople_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPeople.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPhone.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtOldTableNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOldTableNo.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtNewTableNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewTableNo.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Validation for Date not be backdated and Time not be backdated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dtpDeliveryDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpDate.ValueChanged
        If Not IsEditLoad Then
            If dtpDate.Value Is DBNull.Value Then
                Exit Sub
            End If
            If DateTime.Compare(Convert.ToDateTime(dtpDate.Value.Date), Convert.ToDateTime(DateTime.Now.Date)) < 0 Then
                ShowMessage("Date Can't be Backdated", "Information")
                dtpDate.Value = DateTime.Now
            ElseIf DateTime.Compare(Convert.ToDateTime(dtpDate.Value.Date), Convert.ToDateTime(DateTime.Now.Date)) = 0 Then
                If DateTime.Compare((Convert.ToDateTime(dtpFromTime.Value).ToLongTimeString), Convert.ToDateTime(DateTime.Now).ToLongTimeString) < 0 Then
                    ShowMessage("Time Can't be Backdated", "Information")
                    dtpFromTime.Value = DateTime.Now
                End If
            End If
        End If

    End Sub

    ''' <summary>
    '''  Validation for Date not be backdated and Time not be backdated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dtpFromTime_ValueChanged(sender As Object, e As EventArgs) Handles dtpFromTime.ValueChanged
        Try
            If Not IsEditLoad Then
                If dtpFromTime.Value Is DBNull.Value OrElse dtpDate.Value Is DBNull.Value Then
                    Exit Sub
                End If
                If DateTime.Compare(Convert.ToDateTime(dtpDate.Value.Date), Convert.ToDateTime(DateTime.Now.Date)) < 0 Then
                    ShowMessage("Date Can't be Backdated", "Information")
                    dtpDate.Value = DateTime.Now
                ElseIf DateTime.Compare(Convert.ToDateTime(dtpDate.Value.Date), Convert.ToDateTime(DateTime.Now.Date)) = 0 Then
                    If DateTime.Compare((Convert.ToDateTime(dtpFromTime.Value).ToLongTimeString), Convert.ToDateTime(DateTime.Now).ToLongTimeString) < 0 Then
                        ShowMessage("Time Can't be Backdated", "Information")
                        dtpFromTime.Value = DateTime.Now
                    End If
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' on click of search the tables should be fetched from db depends on mode
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            CrntSelectedTables.Clear()
            PreviousEditTables.Clear()
            ReservationPanel.Controls.Clear()
            Dim timeflag = 60
            CustomerName = txtName.Text
            NoOfPeople = txtPeople.Text
            Phone = txtPhone.Text
            custDate = dtpDate.Value
            FromTime = dtpFromTime.Value
            ToTime = Convert.ToDateTime(dtpFromTime.Value).AddMinutes(clsDefaultConfiguration.ReservationTime)
            dtDinNumber = objReservation.GetResevationTableNumber(IsEdit, custDate, FromTime, ToTime, clsAdmin.SiteCode, "", clsAdmin.DayOpenDate, clsDefaultConfiguration.CustStayTime, CustomerNo, clsDefaultConfiguration.ReservationTime)
            For DineInNumber As Int16 = 1 To dtDinNumber.Rows.Count
                If Not IsEdit Then
                    If dtDinNumber.Rows(DineInNumber - 1)("TableStatus") = "Available" Then
                        AddDineInTable(DineInNumber:=DineInNumber)
                    End If
                Else
                    If (dtDinNumber.Rows(DineInNumber - 1)("TableStatus") = "Available" AndAlso dtDinNumber.Rows(DineInNumber - 1)("SearchCust") = "A") OrElse (dtDinNumber.Rows(DineInNumber - 1)("TableStatus") = "Booked" AndAlso dtDinNumber.Rows(DineInNumber - 1)("SearchCust") = "A") AndAlso dtDinNumber.Rows(DineInNumber - 1)("ReservationId") = ReservationIdEdit Then
                        AddDineInTable(DineInNumber:=DineInNumber)
                    End If

                    If dtDinNumber.Rows(DineInNumber - 1)("TableStatus") = "Booked" AndAlso dtDinNumber.Rows(DineInNumber - 1)("SearchCust") = "A" AndAlso dtDinNumber.Rows(DineInNumber - 1)("ReservationId") = ReservationIdEdit Then
                        PreviousEditTables.Add(DineInNumber)
                    End If
                End If
            Next DineInNumber
            If IsEdit Then
                For i = 0 To PreviousEditTables.Count - 1
                    CrntSelectedTables.Add(PreviousEditTables(i))
                Next
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub dtpFromTime_TextChanged(sender As Object, e As EventArgs) Handles dtpFromTime.TextChanged
        If Not String.IsNullOrEmpty(sender.Text) AndAlso Not String.IsNullOrEmpty(dtpDate.Text) Then
            btnSearch.Enabled = True
        Else
            btnSearch.Enabled = False
        End If
    End Sub

    Private Sub dtpDate_TextChanged(sender As Object, e As EventArgs) Handles dtpDate.TextChanged
        If Not String.IsNullOrEmpty(sender.Text) AndAlso Not String.IsNullOrEmpty(dtpFromTime.Text) Then
            btnSearch.Enabled = True
        Else
            btnSearch.Enabled = False
        End If
    End Sub

    Private Sub frmNewReservation_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

End Class