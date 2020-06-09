Imports SpectrumBL
Imports C1.Win.C1FlexGrid
Imports System.IO
Imports System.Text
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net
Imports SpectrumPrint

Public Class frmHostCreateReservation


#Region "Globle variables for class"
    Dim objHotelreservation As New clsHotelReservation
    Dim objCustm As New clsSOCustomer
    Dim objReservation As New clsReservation
    Dim objcomm As New clsCommon
    Dim objCM As New clsCashMemo


    Dim dsCombo As DataSet = New DataSet
    Dim dsMemb As New DataSet
    Dim dsMain As New DataSet
    Dim dsMainHost As New DataSet
    Dim dsCashMemoComboDtl As New DataSet

    Dim dtHotelAllTaxes As New DataTable
    Dim dtRoomTypeWisePromotions As New DataTable '--- all promotion w .r.t room type
    Dim dtReserv As New DataTable
    Dim dtGuest As New DataTable
    Dim dtReservationTabSaveSchema As New DataTable
    Dim dtReservation As DataTable
    Dim dtSummary As New DataTable
    Dim dtReservationTaxMap As New DataTable
    Dim dtReservationReceipt As New DataTable
    Dim dtPromotions As New DataTable ' prepare all selected promtoion in it and pass it to db for saving
    Dim dtSelectedPromoRoomTypeWise As New DataTable
    Dim dtPromotionTotalDiscountValues As New DataTable
    Dim dtPromotionSelected As New DataTable  ' for temp getting data when select any promotion

    Dim bedNo As Integer
    Dim roomtype As String
    Dim NoOfPeole As String
    Dim noOfNight As Integer
    Dim noOfDays As Integer
    Dim SelectedRoomsCount As Integer = 0
    Dim SelectedRoomsCost As Integer = 0 ' as total cost on create reservation tab
    Dim currentGuest As Integer
    Dim MaxId As Integer = 0
    Dim RoomTypePromotionCount = 0
    Dim AllPromotionTotalDiscountAmount As Integer = 0
    Dim Age As String
    Public filterImage As String = "All files (*.*)|*.*"
    Dim fileLocation As String
    Dim FullNameWithExtension As String
    Dim _selectedRoomTypeId As String = String.Empty
    Dim _selectedRoomNumber As String = String.Empty
    Dim _selectedPromotion As String = String.Empty
    Public _remarks As String
    Private _strRemarks As String
    Public _paidAmt As String
    Public _billAmt As String
    Dim _reservationNumber As String = ""
    Dim CLPCardType, CLPCustomerId, clpCustomerMobileNo As String
    Dim GiftMsg As String = ""
    Dim PaymentTermId As String = ""
    Dim _selectedPromotionDiscount As String = String.Empty
    Dim documentNo As String

    Dim IsFileUpload As Boolean = False
    Dim EditGuest As Boolean = False
    Dim IsArticleWiseKot As Boolean = False
    Dim IsCounterCopy As Boolean = False
    Dim IsFinalReceipt As Boolean = False
    Dim UpdateFlag As Boolean = False
    Dim isCashierPromoSelected As Boolean
    Private IsTenderCheque As Boolean = False
    Dim uncheckedChang As Boolean = False
    Dim isCheckIn As Boolean = False

    Protected controlList As New ArrayList
    Private ImagePatient As Image
    Private OFileDialog As OpenFileDialog
    Public GVBasedAricleReturnList As New Dictionary(Of String, String)

    Private _dDueDate As Date
    Dim _chkInDate As DateTime
    Dim ISCallFromPayment As Boolean = False
#End Region
#Region "Properties"

    Public Property checkIndate As DateTime
        Get
            Return _chkInDate
        End Get
        Set(ByVal value As DateTime)
            _chkInDate = value
        End Set
    End Property

    Dim _ChkoutDate As DateTime
    Public Property checkOutdate As DateTime
        Get
            Return _ChkoutDate
        End Get
        Set(value As DateTime)
            _ChkoutDate = value
        End Set
    End Property

    Shared _roomTypeRoomsCount As DataTable
    Public Shared Property dtRoomTypeRoomsCount() As DataTable
        Get
            Return _roomTypeRoomsCount
        End Get
        Set(value As DataTable)
            _roomTypeRoomsCount = value
        End Set
    End Property
    Private _reservationId As String = ""
    Public Property ReservationId() As String
        Get
            Return _reservationId
        End Get
        Set(ByVal value As String)
            _reservationId = value
        End Set
    End Property
    Private _reservationGuestName As String
    Public Property ReservationGuestName() As String
        Get
            Return _reservationGuestName
        End Get
        Set(ByVal value As String)
            _reservationGuestName = value
        End Set
    End Property
    Private _reservationRNumber As String
    Public Property ReservationRNumber() As String
        Get
            Return _reservationRNumber
        End Get
        Set(ByVal value As String)
            _reservationRNumber = value
        End Set
    End Property

    Private _reservationDate As String
    Public Property ReservationDate() As String
        Get
            Return _reservationDate
        End Get
        Set(ByVal value As String)
            _reservationDate = value
        End Set
    End Property
#End Region



#Region "GridsColumnSetting"
    'gridReservation
    Private Sub gridReservationDetailsSetting()
        Try
            grdReservation.DataSource = dtReserv
            grdReservation.Cols("Selects").DataType = Type.GetType("System.Boolean")
            grdReservation.Cols("Selects").AllowEditing = True
            grdReservation.Cols("Selects").Caption = "Select"
            grdReservation.Cols("Selects").Width = 53

            grdReservation.Cols("reservationId").Visible = False
            grdReservation.Cols("mstRoomId").Width = 100
            grdReservation.Cols("mstRoomId").DataType = Type.GetType("System.String")
            grdReservation.Cols("mstRoomId").AllowEditing = False
            grdReservation.Cols("mstRoomId").Name = "mstRoomId"
            grdReservation.Cols("mstRoomId").TextAlign = TextAlignEnum.LeftCenter
            grdReservation.Cols("mstRoomId").Visible = False

            grdReservation.Cols("RoomTypeId").Width = 100
            grdReservation.Cols("RoomTypeId").DataType = Type.GetType("System.String")
            grdReservation.Cols("RoomTypeId").AllowEditing = False
            grdReservation.Cols("RoomTypeId").Name = "RoomTypeId"
            grdReservation.Cols("RoomTypeId").TextAlign = TextAlignEnum.LeftCenter
            grdReservation.Cols("RoomTypeId").Visible = False

            grdReservation.Cols("RoomNumber").Width = 120
            grdReservation.Cols("RoomNumber").DataType = Type.GetType("System.String")
            grdReservation.Cols("RoomNumber").AllowEditing = False
            grdReservation.Cols("RoomNumber").Name = "RoomNumber"
            grdReservation.Cols("RoomNumber").TextAlign = TextAlignEnum.LeftCenter

            grdReservation.Cols("RoomType").Width = 125
            grdReservation.Cols("Roomtype").AllowEditing = False
            grdReservation.Cols("RoomType").DataType = Type.GetType("System.String")

            grdReservation.Cols("RoomType").Name = "RoomType"
            grdReservation.Cols("RoomType").TextAlign = TextAlignEnum.LeftCenter

            grdReservation.Cols("Amenities").Width = 526
            grdReservation.Cols("Amenities").AllowEditing = False
            grdReservation.Cols("Amenities").DataType = Type.GetType("System.String")

            grdReservation.Cols("Amenities").Name = "Amenities"
            grdReservation.Cols("Amenities").TextAlign = TextAlignEnum.LeftCenter

            grdReservation.Cols("Cost").Width = 200
            grdReservation.Cols("Cost").DataType = Type.GetType("System.String")
            grdReservation.Cols("Cost").AllowEditing = False
            grdReservation.Cols("Cost").Name = "Cost"
            grdReservation.Cols("Cost").TextAlign = TextAlignEnum.LeftCenter

            grdReservation.Cols("Tax").Width = 100
            grdReservation.Cols("Tax").AllowEditing = False
            grdReservation.Cols("Tax").DataType = Type.GetType("System.Decimal")
            grdReservation.Cols("Tax").Format = "0"
            grdReservation.Cols("Tax").Name = "Tax"
            grdReservation.Cols("Tax").TextAlign = TextAlignEnum.LeftCenter
            grdReservation.Cols("Tax").Visible = False

            grdReservation.Cols("BED").Width = 180
            grdReservation.Cols("BED").DataType = Type.GetType("System.String")
            grdReservation.Cols("BED").AllowEditing = False
            grdReservation.Cols("BED").Name = "BED"
            grdReservation.Cols("BED").TextAlign = TextAlignEnum.LeftCenter

            grdReservation.Cols("ReservationStatusID").Visible = False

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    'grdGuestDetails
    Private Sub gridGuestDetailsSetting()
        Try
            grdGuestDetails.DataSource = dtGuest
            grdGuestDetails.Cols("Delete").Caption = ""
            grdGuestDetails.Cols("Delete").Width = 20
            grdGuestDetails.Cols("Delete").ComboList = "..."
            grdGuestDetails.Cols("SrNo").Visible = True

            grdGuestDetails.Cols("FirstName").Width = 100
            grdGuestDetails.Cols("FirstName").DataType = Type.GetType("System.String")
            grdGuestDetails.Cols("FirstName").Name = "FirstName"
            grdGuestDetails.Cols("FirstName").TextAlign = TextAlignEnum.LeftCenter

            grdGuestDetails.Cols("MiddleName").Width = 100
            grdGuestDetails.Cols("MiddleName").DataType = Type.GetType("System.String")
            grdGuestDetails.Cols("MiddleName").Name = "MiddleName"
            grdGuestDetails.Cols("MiddleName").TextAlign = TextAlignEnum.LeftCenter

            grdGuestDetails.Cols("LastName").Width = 100
            grdGuestDetails.Cols("LastName").DataType = Type.GetType("System.String")
            grdGuestDetails.Cols("LastName").Name = "LastName"
            grdGuestDetails.Cols("LastName").TextAlign = TextAlignEnum.LeftCenter

            grdGuestDetails.Cols("MobileNumber").Width = 150
            grdGuestDetails.Cols("MobileNumber").DataType = Type.GetType("System.String")
            'grdGuestDetails.Cols("MobileNumber").Format = "0"
            grdGuestDetails.Cols("MobileNumber").Name = "MobileNumber"
            grdGuestDetails.Cols("MobileNumber").TextAlign = TextAlignEnum.LeftCenter

            grdGuestDetails.Cols("EmailId").Width = 200 'EmailId
            grdGuestDetails.Cols("EmailId").DataType = Type.GetType("System.String")
            ' grdGuestDetails.Cols("EmailId").Format = "0"
            grdGuestDetails.Cols("EmailId").Name = "EmailId"
            grdGuestDetails.Cols("EmailId").TextAlign = TextAlignEnum.LeftCenter

            grdGuestDetails.Cols("Age").Width = 30
            grdGuestDetails.Cols("Age").DataType = Type.GetType("System.Numeric")
            grdGuestDetails.Cols("Age").Format = "0"
            grdGuestDetails.Cols("Age").Name = "Age"
            grdGuestDetails.Cols("Age").TextAlign = TextAlignEnum.LeftCenter

            grdGuestDetails.Cols("Gender").Width = 50
            grdGuestDetails.Cols("Gender").DataType = Type.GetType("System.String")
            'grdGuestDetails.Cols(Gender").Format = "0"
            grdGuestDetails.Cols("Gender").Name = "Gender"
            grdGuestDetails.Cols("Gender").TextAlign = TextAlignEnum.LeftCenter

            grdGuestDetails.Cols("DocumentType").Width = 150
            grdGuestDetails.Cols("DocumentType").DataType = Type.GetType("System.String")
            grdGuestDetails.Cols("DocumentType").Name = "DocumentType"
            grdGuestDetails.Cols("DocumentType").TextAlign = TextAlignEnum.LeftCenter

            grdGuestDetails.Cols("PrimaryGuest").Width = 30
            grdGuestDetails.Cols("PrimaryGuest").DataType = Type.GetType("System.Boolean")
            grdGuestDetails.Cols("PrimaryGuest").Name = "PrimaryGuest"
            grdGuestDetails.Cols("PrimaryGuest").TextAlign = TextAlignEnum.LeftCenter

            grdGuestDetails.Cols("supportedDocumentDetails").Width = 130
            grdGuestDetails.Cols("supportedDocumentDetails").DataType = Type.GetType("System.String")
            grdGuestDetails.Cols("supportedDocumentDetails").Name = "supportedDocumentDetails"
            grdGuestDetails.Cols("supportedDocumentDetails").TextAlign = TextAlignEnum.LeftCenter
            '-------------To Enable Checkbox if Dispatch Time is There and Make Enabling False
            For row = 1 To grdGuestDetails.Rows.Count - 1
                'If Not String.IsNullOrEmpty(grdGuestDetails.Rows(row)("PrimaryGuest").ToString) Then


                '    grdGuestDetails.Rows(row)("PrimaryGuest") = True
                '  End If
            Next


        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

#End Region

#Region "-----------------------------Create Reservation Details tab  "
#Region "event"
    Private Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click

        txtCRoomSelected.Text = 0
        txtCTotalCostAount.Text = 0




        'code added for issue id 1669 by aditya 30-05-2017
        If cmdChkInDate.Value Is Nothing Or IsDBNull(cmdChkInDate.Value) Then
            ShowMessage("Please select check in Date", "Information")
            cmdChkInDate.Focus()
            Exit Sub
        ElseIf cmdCheckoutDate.Value Is Nothing Or IsDBNull(cmdCheckoutDate.Value) Then
            ShowMessage(" Please select check out Date", "Information")
            cmdChkInDate.Focus()
            Exit Sub
        End If
        If cmdChkInDate.Value < clsAdmin.CurrentDate.Date Then
            ShowMessage(" Check-In date should not be less than current date", "Information")
            cmdChkInDate.Focus()
            Exit Sub
        End If
        If cmdChkInDate.Text < Convert.ToDateTime(clsAdmin.CurrentDate) Then
            ShowMessage("Check-In Date Should not be less than Current Date Time", "Information")
            cmdChkInDate.Focus()
            Exit Sub
        End If
        'If cmdChkInDate.Value < clsAdmin.CurrentDate.Date Then
        '    ShowMessage(" Check-In date should not be less than current date", "Information")
        '    cmdChkInDate.Focus()
        '    Exit Sub
        'End If
        'If checkIndate.ToString("dd-MM-yyyy") < checkOutdate.ToString("dd-MM-yyyy") Then
        '    ShowMessage(" Check-In date should not be less than current date", "Information")
        '    cmdChkInDate.Focus()
        '    Exit Sub
        'End If
        ' If cmdChkInDate.Text > cmdCheckoutDate.Text Then
        If Convert.ToDateTime(cmdChkInDate.Text) > Convert.ToDateTime(cmdCheckoutDate.Text) Then
            ShowMessage(" Check-Out Date Should not be less than Check-In Date", "Information")
            cmdChkInDate.Focus()
            Exit Sub
        End If


        checkIndate = cmdChkInDate.Value
        checkOutdate = cmdCheckoutDate.Value
        roomtype = cmbRoomType.SelectedValue
        bedNo = CmbBed.SelectedValue
        NoOfPeole = cmbNoOfBed.SelectedValue
        grdReservation.Clear()

        '''  Call bindRoomReservation(checkIndate.ToString("MM-dd-yyyy"), checkOutdate.ToString("MM-dd-yyyy"), roomtype, bedNo, NoOfPeole) commented by roshan on 6/4/2018

        Call bindRoomReservation(CDate(checkIndate), CDate(checkOutdate), roomtype, bedNo, NoOfPeole)

        gridReservationDetailsSetting()
    End Sub
    'Host_SP_PromotionDetails '0','1111-01-01','1111','0,0'  
    Private Sub frmHostCreateReservation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            PrepareDataTable()
            btnPreviousTab.Visible = False
            btnSaveAndFinish.Enabled = False
            btnSaveAndCheckIn.Enabled = False
            ' getHOSTBinding()

            'txtCheckedIn.TextDetached = True
            'txtCheckedOut.TextDetached = True
            ' cmdChkInDate.TextDetached = True
            ' cmdCheckoutDate.TextDetached = True
            ' txtCheckedOut.Text = cmdCheckoutDate.Text
            ' cmdChkInDate.Text = Date.Now.ToShortDateString   'clsAdmin.CurrentDate
            ' cmdCheckoutDate.Text = Date.Now.ToShortDateString '' Date.Now.ToShortDateString
            Dim dtRoomType As DataTable
            Dim dtBedType As DataTable
            Dim dtNoOfBedType As DataTable
            Dim dtDocumentType As DataTable
            'cmbRoomType.Text = "ALL"
            dtRoomType = objHotelreservation.GetRoomType(clsAdmin.SiteCode)
            dtBedType = objHotelreservation.GetBedType(clsAdmin.SiteCode)
            dtNoOfBedType = objHotelreservation.GetNoOfBedType(clsAdmin.SiteCode)
            dtDocumentType = objHotelreservation.GetDocumentType(clsAdmin.SiteCode)
            dsCombo = objCustm.GetComboDataSet
            If dsCombo.Tables("GenderTab").Rows.Count > 0 Then
                BindComboBoxes(dsCombo.Tables("GenderTab"), cmbGender)
            End If
            If dtRoomType.Rows.Count > 0 Then
                BindComboBoxes(dtRoomType, cmbRoomType)
                cmbRoomType.SelectedIndex = 0
            End If
            If dtBedType.Rows.Count > 0 Then
                BindComboBoxes(dtBedType, CmbBed)
                CmbBed.SelectedIndex = 0
            End If
            If dtNoOfBedType.Rows.Count > 0 Then
                BindComboBoxes(dtNoOfBedType, cmbNoOfBed)
                'cmbNoOfBed.SelectedIndex = 1
                cmbNoOfBed.SelectedText = dtNoOfBedType.Rows.Count - 1
            End If
            If dtDocumentType.Rows.Count > 0 Then
                BindComboBoxes(dtDocumentType, cmdDocumentType)
            End If
            'txtCRoomSelected.ReadOnly = True
            'txtCTotalCostAount.ReadOnly = True

            '' Create -First TAB
            LoadReservationData()
            gridReservationDetailsSetting()
            tpCreateDetails.Show()
            DefaultTheme()
            tpPromotionsDetails.Enabled = False
            tpGuestDetails.Enabled = False
            tpSummaryDetails.Enabled = False
            tpPaymentsDetails.Enabled = False
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub BindComboBoxes(ByVal dt As DataTable, ByRef objComboBox As Spectrum.ctrlCombo)
        PopulateComboBox(dt, objComboBox)
        pC1ComboSetDisplayMember(objComboBox)
    End Sub
    Public Sub PrepareDataTable()
        dtGuest = objHotelreservation.GetDetailsForGuest()
        dtGuest.Clear()
        dtReservationTaxMap = objHotelreservation.GetDetailsForReservationTaxMap()
        dtReservationTaxMap.Clear()
        dtPromotions = objHotelreservation.GetDetailsForReservationPromotionMap()
        dtPromotions.Clear()
        dtReservationReceipt = objHotelreservation.GetDetailsForReservationReceiptSchema()
        dtReservationReceipt.Clear()
        dtHotelAllTaxes = objHotelreservation.GetHotelTax(clsAdmin.SiteCode, "RES")
        dtPromotionTotalDiscountValues = objHotelreservation.GetPromotionTotalDisountSchema()
        dtPromotionTotalDiscountValues.Clear()
    End Sub

    Private Sub grdReservation_AfterEdit(sender As Object, e As RowColEventArgs) Handles grdReservation.AfterEdit
        Try
            SelectedRoomsCost = 0
            SelectedRoomsCount = 0
            Dim tempRoomTypeId As String = ""
            Dim tempRoomNumber As String = ""

            For index = 1 To grdReservation.Rows.Count - 1
                Dim pertucularRoomTypeCount = 0
                Dim PreviousRoomTypeId As String = ""
                If grdReservation.Rows(index)(0) = True Then
                    tempRoomTypeId = grdReservation.Rows(index)("RoomTypeId")
                    tempRoomNumber = grdReservation.Rows(index)("RoomNumber")
                    SelectedRoomsCost = SelectedRoomsCost + grdReservation.Rows(index)("Cost")
                    SelectedRoomsCount = SelectedRoomsCount + 1
                    pertucularRoomTypeCount = pertucularRoomTypeCount + 1
                    UpdateRoomTypeCount(tempRoomTypeId, tempRoomNumber, pertucularRoomTypeCount, False)  'add room number
                Else
                    'tempRoomTypeId = grdReservation.Rows(index)("RoomTypeId")
                    'tempRoomNumber = grdReservation.Rows(index)("RoomNumber")
                    'SelectedRoomsCount = SelectedRoomsCount - 1
                    'UpdateRoomTypeCount(tempRoomTypeId, tempRoomNumber, SelectedRoomsCount, True)  ' remove room number
                End If
            Next
            If tempRoomTypeId <> "" Then
                txtCRoomSelected.Text = SelectedRoomsCount
                txtCTotalCostAount.Text = SelectedRoomsCost
            Else
                txtCRoomSelected.Text = "0"
                txtCTotalCostAount.Text = "0"
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub grdReservation_CellChecked(sender As Object, e As RowColEventArgs)


        'grdReservation.Rows(index)(0)
        'For intex = 1 To dtRoomTypeRoomsCount.Rows.Count - 1
        '    If dtRoomTypeRoomsCount.Rows(intex)("mstRoomTypeId") = "1" Then
        '        dtRoomTypeRoomsCount.Rows(intex)("RoomCount") = dtRoomTypeRoomsCount.Rows(e.Row)("RoomCount") + 1
        '    End If

        'Next
    End Sub

    Private Sub cmdChkInDate_TextChanged(sender As Object, e As EventArgs)
        'Dim ChkInDate As DateTime

        'If cmdChkInDate.Value < clsAdmin.CurrentDate Then 'Date.Now.ToString Then  ' ) Or IsDBNull(cmdChkInDate.Text) Then
        '    ShowMessage("Check-In Date cannot be less than the Current Date", "Information")
        '    cmdChkInDate.Text = clsAdmin.CurrentDate.ToString("dd-MM-yyyy")
        '    Exit Sub
        '    ' Else
        '    '     ChkInDate = cmdChkInDate.Value
        'End If


    End Sub

    Private Sub cmdCheckoutDate_TextChanged(sender As Object, e As EventArgs)
        'Dim ChkOut As DateTime
        'ChkOut = cmdCheckoutDate.Value
        'If cmdCheckoutDate.Value < cmdChkInDate.Value Then
        '    ShowMessage(" Check-Out Date cannot be less than Check-In Date ", "Information")
        '    cmdCheckoutDate.Text = clsAdmin.CurrentDate.ToString("dd-MM-yyyy")
        '    Exit Sub
        'End If
    End Sub

#End Region
    '"--end Events"
#Region "Functions"

    Public Sub LoadReservationData()
        ' Call bindRoomReservation(Now.Date, Now.Date, roomtype, bedNo, NoOfPeole)
        dtRoomTypeRoomsCount = objHotelreservation.GetRoomTypeWiseRoomCount()
        Call bindRoomReservation(DateTime.Now, DateTime.Now, cmbRoomType.SelectedValue, CmbBed.SelectedValue, cmbNoOfBed.SelectedValue)
    End Sub
    'added by khusrao adil on 15-02-2017 for room count update
    Public Function UpdateRoomTypeCount(ByVal mstRoomTypeID As String, ByVal RoomNumberList As String, Optional ByVal RoomCount As String = "0", Optional ByVal isRemoved As Boolean = False, Optional changePromotion As Boolean = False, Optional ByVal PromotionCount As String = "0")
        Try
            For Each drr In dtRoomTypeRoomsCount.Rows
                If drr("mstRoomTypeId") = mstRoomTypeID Then
                    If changePromotion = True Then
                        drr("PromotionCount") = PromotionCount
                    Else
                        If isRemoved = True Then
                            drr("RoomCount") = RoomCount
                            Dim AllString As String = drr("RoomNumberList")
                            AllString = Replace(AllString, RoomNumberList, "")
                            drr("RoomNumberList") = AllString
                        Else
                            drr("RoomCount") = RoomCount
                            drr("PromotionCount") = PromotionCount
                            Dim strMyString As String = drr("RoomNumberList").ToString()
                            If strMyString.Contains(RoomNumberList) Then
                                ' drr("RoomNumberList") = drr("RoomNumberList") + "," + RoomNumberList
                            Else
                                Dim strAllString As String = drr("RoomNumberList").ToString()
                                If strAllString <> "" Then
                                    drr("RoomNumberList") = drr("RoomNumberList") + RoomNumberList
                                Else
                                    drr("RoomNumberList") = RoomNumberList + ","
                                End If
                            End If
                        End If
                    End If
                    dtRoomTypeRoomsCount.AcceptChanges()
                End If
            Next
            dtRoomTypeRoomsCount.AcceptChanges()
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    Private Sub BindNoOfBed()
        Try
            Dim dt As DataTable
            cmbNoOfBed.Text = ""
            dt = objHotelreservation.GetNoOfBedType(clsAdmin.SiteCode)
            If Not dt Is Nothing And dt.Rows.Count > 0 Then
                cmbNoOfBed.DataSource = dt
                cmbNoOfBed.DisplayMember = dt.Columns("noOfBeds").ToString()
                cmbNoOfBed.ValueMember = dt.Columns("noOfBeds").ToString()
                cmbNoOfBed.ExtendRightColumn = True
                For Each r As C1.Win.C1List.Split In cmbNoOfBed.Splits
                    Dim i As Integer
                    For i = 0 To r.DisplayColumns.Count - 1
                        If r.DisplayColumns(i).Name <> cmbNoOfBed.DisplayMember Then
                            r.DisplayColumns(i).Visible = False
                        End If
                    Next
                Next
                cmbNoOfBed.SelectedIndex = 1
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function getSelectedRoom() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("RoomTypeId", System.Type.GetType("System.String"))
        dt.Columns.Add("RoomNumber", System.Type.GetType("System.String"))
        Dim SelCount As Integer = 0
        For i = 1 To grdReservation.Rows.Count - 1
            If grdReservation.Rows(i)("Selects") = True Then
                Dim dr As DataRow
                dr = dt.NewRow()
                dr("RoomNumber") = IIf(IsDBNull(grdReservation.Rows(i)("RoomNumber")), String.Empty, grdReservation.Rows(i)("RoomNumber"))
                dr("RoomTypeId") = IIf(IsDBNull(grdReservation.Rows(i)("RoomTypeId")), String.Empty, grdReservation.Rows(i)("RoomTypeId"))

                dt.Rows.Add(dr)
            End If
        Next
        For Each dr In dt.Rows
            _selectedRoomTypeId = _selectedRoomTypeId + "," + dr("RoomTypeId")
        Next
        _selectedRoomTypeId.Replace(_selectedRoomTypeId, "")
        ' Dim sum As Integer = dt.Compute("Sum(RoomNumber)", "")
        ' Dim sum As Integer = dt.AsEnumerable().Sum(Function(row) row.Field(Of Integer)("RoomNumber"))
    End Function

    Private Sub bindRoomReservation(Checkin As DateTime, checkout As DateTime, Optional ByVal Roomtype As String = Nothing, Optional ByVal bedNo As String = "", Optional ByVal noOfPeople As String = "") ''checkout As DateTime,
        Try
            Dim year As String = Checkin.Year
            If Roomtype = 10 Then
                Roomtype = "All"
            End If
            dtReservation = objHotelreservation.GetReservationRooms(clsAdmin.SiteCode, Checkin, year, Roomtype, bedNo, noOfPeople, checkout) ''checkout.ToString("dd-MM-yyyy"),
            If dtReservation IsNot Nothing Then
                grdReservation.DataSource = dtReservation.DefaultView
                'AddReservationDetailsToGrid(dtReservation)
                dtReserv = dtReservation
            Else
                ShowMessage(getValueByKey(" Room not Available"), "Information - " & "Information")
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

#End Region
    ' "--end Functions"
#End Region
#Region "-----------------------------Promotion Details tab  "

#Region "-----Events"


#End Region
    '"--end Events"
#Region "-----Functions"
    'added by khusrao adil on 10-03-2017 to  prapare selected promo
    Public Function RoomTypeWiseSelectedPromotions(ByVal _mstRoomTypeId As String, ByVal _mstPromotionId As String, ByVal _mstSiteRoomTypeMap As String, ByVal _promotionName As String,
                                    ByVal _discountInPercent As String, ByVal _rate As Decimal, _discountInAmount As Decimal, Optional ByVal IsRemoved As Boolean = False)
        Try
            Dim StrFilter = ""
            If _mstPromotionId <> "0" Then
                ' StrFilter = "and mstPromotionId='" + _mstPromotionId + "'"
            End If
            Dim rowMTDt = dtPromotions.Select("RoomTypeId='" + _mstRoomTypeId + "' " + StrFilter + " ")
            If IsRemoved = False Then
                If rowMTDt.Length = 0 Then
                    Dim drPromo As DataRow
                    drPromo = dtPromotions.NewRow()
                    drPromo("Selects") = True
                    drPromo("RoomTypeId") = _mstRoomTypeId
                    drPromo("mstSiteRoomTypeMap") = _mstSiteRoomTypeMap
                    drPromo("mstPromotionID") = _mstPromotionId
                    drPromo("mstPromotionName") = _promotionName
                    drPromo("discountInPercent") = _discountInPercent
                    drPromo("discountAmount") = _discountInAmount
                    drPromo("Rate") = _rate
                    drPromo("RecordStatus") = objHotelreservation.enumRecordStatus.Inserted.ToString()
                    dtPromotions.Rows.Add(drPromo)
                Else
                End If
            Else
                If rowMTDt.Length > 0 Then
                    For Each row As DataRow In rowMTDt
                        dtPromotions.Rows.Remove(row)
                    Next
                End If
                Dim rowMTDtPRO = dtPromotionTotalDiscountValues.Select("RoomTypeId='" + _mstRoomTypeId + "'")
                If rowMTDtPRO.Length > 0 Then
                    For Each row As DataRow In rowMTDtPRO
                        dtPromotionTotalDiscountValues.Rows.Remove(row)
                    Next
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao on 15-02-2017 for promotion tab amount details and calculation
    Sub PromotionAmountDetails(ByVal strSelectedRoomsCost As String, ByVal strpromotionsTotalDiscount As String)
        Try
            Dim SelectedRoomsCost As Decimal = Convert.ToDecimal(strSelectedRoomsCost)

            Dim promotionsTotalDiscount As Decimal = Convert.ToDecimal(strpromotionsTotalDiscount)
            Dim promotionFinalCost As Decimal
            Dim _calculateTax As Decimal
            Dim promotiontotalTax As Decimal = 0.0

            '  Dim promotiontotalTax As Decimal = Convert.ToDecimal(dtHotelAllTaxes.Select("Sum(Tax)").FirstOrDefault())
            'dtChildTable.Compute("Sum(Price)", "");
            If dtHotelAllTaxes.Rows.Count > 0 Then
                promotiontotalTax = Convert.ToDecimal(dtHotelAllTaxes.Compute("Sum(Tax)", ""))
            End If
            _calculateTax = ((SelectedRoomsCost - promotionsTotalDiscount) / 100) * promotiontotalTax
            promotionFinalCost = (SelectedRoomsCost - promotionsTotalDiscount) + _calculateTax
            'Math.Round(before, 1, MidpointRounding.AwayFromZero)
            'Math.Round(before2, 1)
            txtPromoDiscount.TextDetached = True
            txtPromoTax.TextDetached = True
            txtPromoCost.TextDetached = True
            txtPromoDiscount.Text = Math.Round(promotionsTotalDiscount, 2)
            'txtPromoTax.Text = promotiontotalTax.ToString()
            txtPromoTax.Text = Math.Round(_calculateTax, 2)
            txtPromoCost.Text = Math.Round(promotionFinalCost, 2)
            ' AllPromotionTotalDiscountAmount = 0
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
#End Region
    ' "--end Functions"
#End Region
#Region "-----------------------------Guest Details tab  "

#Region "--Events"
  
    Private Sub btnGuestAdd_Click(sender As Object, e As EventArgs) Handles btnGuestAdd.Click
        Try
            If ValidatedGuestDetails() Then

                If GuestDetailsVAlidationExpression() = False Then
                    Exit Sub
                End If
                If EditGuest = True Then
                    'dtGuest.Rows(currentGuest - 1)("SrNo") = SelCount
                    If dtGuest.Rows.Count > 0 Then
                        For index = 0 To dtGuest.Rows.Count - 1
                            If chkBxPrimaryGuest.Checked = True Then
                                If dtGuest.Rows(index)("PrimaryGuest") = True Then
                                    dtGuest.Rows(index)("PrimaryGuest") = False
                                End If
                            End If
                        Next
                    End If
                    dtGuest.Rows(currentGuest - 1)("FirstName") = txtGuestFirstName.Text
                    dtGuest.Rows(currentGuest - 1)("MiddleName") = txtGuestMiddleName.Text
                    dtGuest.Rows(currentGuest - 1)("LastName") = txtGuestLastName.Text
                    dtGuest.Rows(currentGuest - 1)("MobileNumber") = txtGuestMobileNumber.Text
                    dtGuest.Rows(currentGuest - 1)("EmailId") = txtGuestEmailId.Text
                    dtGuest.Rows(currentGuest - 1)("Age") = txtAge.Text
                    dtGuest.Rows(currentGuest - 1)("Gender") = cmbGender.Text
                    dtGuest.Rows(currentGuest - 1)("DocumentType") = cmdDocumentType.Text.Trim()
                    dtGuest.Rows(currentGuest - 1)("DocumentTypeId") = cmdDocumentType.SelectedIndex
                    dtGuest.Rows(currentGuest - 1)("PrimaryGuest") = IIf(chkBxPrimaryGuest.Checked, True, False)
                    dtGuest.Rows(currentGuest - 1)("supportedDocumentDetails") = txtDocumentNo.Text
                    EditGuest = False
                Else


                    If dtGuest.Rows.Count > 0 Then
                        For index = 0 To dtGuest.Rows.Count - 1
                            'Dim dtRow As Int32 = -1
                            Dim result As DataRow() = dtGuest.Select("MobileNumber='" + txtGuestMobileNumber.Text.Trim + "' and EmailID='" + txtGuestEmailId.Text.Trim + "'")
                            If result.Length > 0 Then
                                ShowMessage("Record Already exist", "Information")
                                Exit Sub
                            End If
                            If chkBxPrimaryGuest.Checked = True Then
                                If dtGuest.Rows(index)("PrimaryGuest") = True Then
                                    dtGuest.Rows(index)("PrimaryGuest") = False
                                End If
                            End If

                        Next

                    End If

                    'Code is added by irfan on 04/04/2018 for hotel reservation.
                    Dim RecordStatus As String
                    Dim cardno As String
                    Dim dt1 As DataTable = Nothing
                    dt1 = objCM.IsCustomerExists(txtGuestMobileNumber.Text.ToString())
                    If Not dt1 Is Nothing Then
                        ' drcus("RecordStatus") = "Updated"
                        '  rowGuest("RecordStatus") = objHotelreservation.enumRecordStatus.Inserted.ToString()
                        cardno = dt1.Rows(0)("CardNo")
                        RecordStatus = "Updated"
                    Else
                        documentNo = objcomm.getDocumentNo("Customer Loyalty", clsAdmin.SiteCode)
                        documentNo = documentNo + 1
                        Dim otherCharacters = "CLS" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3, 3)
                        cardno = objcomm.GenDocNo(otherCharacters, 15, documentNo)
                    End If
                    Dim clpid As String = objcomm.GetProgramId(clsAdmin.SiteCode)
                    '============================================================'
                    Dim rowGuest As DataRow
                    rowGuest = dtGuest.NewRow()
                    rowGuest("SrNo") = dtGuest.Rows.Count + 1
                    rowGuest("FirstName") = txtGuestFirstName.Text
                    rowGuest("MiddleName") = txtGuestMiddleName.Text
                    rowGuest("LastName") = txtGuestLastName.Text
                    rowGuest("MobileNumber") = txtGuestMobileNumber.Text
                    rowGuest("EmailID") = txtGuestEmailId.Text
                    rowGuest("Age") = txtAge.Text
                    rowGuest("Gender") = cmbGender.Text
                    rowGuest("DocumentType") = cmdDocumentType.Text
                    rowGuest("DocumentTypeId") = cmdDocumentType.SelectedValue
                    rowGuest("PrimaryGuest") = IIf(chkBxPrimaryGuest.Checked, True, False)
                    rowGuest("GuestImage") = objHotelreservation.ImageToByteArray(ImagePatient)
                    rowGuest("CardNo") = cardno
                    rowGuest("ClpProgramId") = clpid
                    rowGuest("supportedDocumentDetails") = txtDocumentNo.Text
                    If Not String.IsNullOrEmpty(RecordStatus) Then
                        rowGuest("RecordStatus") = RecordStatus
                    End If
                    'rowGuest("mstStatusID") = "1"
                    dtGuest.Rows.Add(rowGuest)
                End If
                gridGuestDetailsSetting()
                ClearGuestDetail()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        OFileDialog = New OpenFileDialog
        OFileDialog.Filter = filterImage
        If (OFileDialog.ShowDialog() = DialogResult.OK) Then
            Dim ext As String = Path.GetExtension(OFileDialog.FileName).ToLowerInvariant()
            If ext = ".jpg" Or ext = ".jpeg" Or ext = ".png" Then
                ImagePatient = Image.FromFile(OFileDialog.FileName) ' byte convert 
                fileLocation = OFileDialog.FileName
                Dim fileInfo As New FileInfo(fileLocation)
                Dim FileSize As Decimal = ((fileInfo.Length) / (1024))
                If FileSize < 500 Then
                    Dim files() As String
                    files = fileLocation.Split(".")

                    Dim filePath = Application.StartupPath & "\Images\"
                    If Directory.Exists(filePath) = False Then
                        Call Directory.CreateDirectory(filePath)
                    End If
                    Dim strpath As String = System.IO.Path.GetFullPath(fileLocation)
                    Dim fileName As String = System.IO.Path.GetFileNameWithoutExtension(fileLocation)
                    Dim extension As String = System.IO.Path.GetExtension(fileLocation)
                    FullNameWithExtension = fileName + extension
                    Dim dest As String = Path.Combine(filePath, Path.GetFileName(OFileDialog.FileName))
                    ' File.Copy(fileLocation, Path.Combine(Path.GetFileName(FullNameWithExtension)), True)
                    File.Copy(fileLocation, Path.Combine(filePath, Path.GetFileName(FullNameWithExtension)), True)
                    'File.Copy(OFileDialog.FileName, dest)
                    ' File.Copy(OFileDialog.FileName, filePath)

                    IsFileUpload = True
                Else
                    ShowMessage("File size Should be Less than 500KB", "Information")
                End If
            Else
                ShowMessage("File Should be .jpg ,.jpeg or.png only", "Information")
            End If
        End If

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnGuestClear.Click
        txtGuestFirstName.Text = String.Empty
        txtGuestMiddleName.Text = String.Empty
        txtGuestLastName.Text = String.Empty
        txtGuestMobileNumber.Text = String.Empty
        txtGuestEmailId.Text = String.Empty
        txtAge.Text = ""
        txtDocumentNo.Text = ""
        cmdDocumentType.SelectedText = ""
        cmbGender.Text = String.Empty
        chkBxPrimaryGuest.Checked = False
        btnGuestAdd.Text = "Add"
        EditGuest = False
    End Sub

    Private Sub grdGuestDetails_CellButtonClick(sender As Object, e As RowColEventArgs) Handles grdGuestDetails.CellButtonClick
        Try
            Dim SrNo = grdGuestDetails.Item(grdGuestDetails.Row, "SrNo")
            DeleteGuestDetails(SrNo)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub grdGuestDetails_DoubleClick(sender As Object, e As EventArgs) Handles grdGuestDetails.DoubleClick
        Try
            Dim currentrow = grdGuestDetails.RowSel
            currentGuest = currentrow
            Dim strSrNo = grdGuestDetails.Item(grdGuestDetails.Row, "SrNo")
            Dim strFirstName = grdGuestDetails.Item(grdGuestDetails.Row, "FirstName")
            Dim strMiddleName = grdGuestDetails.Item(grdGuestDetails.Row, "MiddleName")
            Dim strLastName = grdGuestDetails.Item(grdGuestDetails.Row, "LastName")
            Dim strMobileNumber = grdGuestDetails.Item(grdGuestDetails.Row, "MobileNumber")
            Dim strEmailId = grdGuestDetails.Item(grdGuestDetails.Row, "EmailId")
            Dim strAge = grdGuestDetails.Item(grdGuestDetails.Row, "Age")
            Dim strGender = grdGuestDetails.Item(grdGuestDetails.Row, "Gender")
            Dim strDoctumentType = grdGuestDetails.Item(grdGuestDetails.Row, "DocumentType")
            Dim strPrimaryGuest = grdGuestDetails.Item(grdGuestDetails.Row, "PrimaryGuest")
            Dim strDocumentNo = grdGuestDetails.Item(grdGuestDetails.Row, "supportedDocumentDetails")
            txtGuestFirstName.Text = strFirstName
            txtGuestMiddleName.Text = strMiddleName
            txtGuestLastName.Text = strLastName
            txtGuestMobileNumber.Text = strMobileNumber
            txtGuestEmailId.Text = strEmailId
            txtAge.Text = strAge
            txtDocumentNo.Text = strDocumentNo
            cmbGender.Text = strGender
            cmdDocumentType.Text = strDoctumentType
            chkBxPrimaryGuest.Checked = IIf(strPrimaryGuest, True, False)
            EditGuest = True
            btnGuestAdd.Text = "Update"
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    'added by khusrao adil on 06-03-2017 by srs 5.0
    Private Sub txtGuestMobileNumber_KeyDown(sender As Object, e As KeyEventArgs) Handles txtGuestMobileNumber.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim dt = objHotelreservation.GetClpCustomer(txtGuestMobileNumber.Text, True)
            If dt.Rows.Count > 0 Then
                txtGuestFirstName.Text = dt.Rows(0)("guestFirstName").ToString()
                txtGuestMiddleName.Text = dt.Rows(0)("guestMiddleName").ToString()
                txtGuestLastName.Text = dt.Rows(0)("guestLastName").ToString()
                txtGuestEmailId.Text = dt.Rows(0)("guestEmailID").ToString()
                txtAge.Text = dt.Rows(0)("guestAgeInYears").ToString()
                cmbGender.Text = dt.Rows(0)("guestGender").ToString()
                cmdDocumentType.Text = dt.Rows(0)("supportedDocumentDetails_1").ToString()
                txtDocumentNo.Text = dt.Rows(0)("supportedDocumentDetails").ToString()
                Dim _selectecIndex As Integer = dt.Rows(0)("mstSupportedDocumentTypeID_1").ToString()

                cmdDocumentType.SelectedIndex = _selectecIndex - 1
                chkBxPrimaryGuest.Checked = dt.Rows(0)("isprimaryguest")
            Else
                ShowMessage("Guest Not Available", "Information")
                ClearGuestDetail()
                Exit Sub
            End If
        End If

    End Sub
#End Region
    '"--end Events"
#Region "--Functions"
    'added by khusrao adil for guest entries validation on 12-04-2017
    Private Function GuestDetailsVAlidationExpression() As Boolean
        Try
            If txtGuestFirstName.Text.Trim() <> "" Then
                If objHotelreservation.ValidateAlphabet(txtGuestFirstName.Text.Trim()) = False Then
                    ShowMessage("Special characters are not allowed", "Information")
                    txtGuestFirstName.Focus()
                    Exit Function
                End If
            End If
            If txtGuestMiddleName.Text.Trim() <> "" Then
                If objHotelreservation.ValidateAlphabet(txtGuestMiddleName.Text.Trim()) = False Then
                    ShowMessage("Special characters are not allowed", "Information")
                    txtGuestMiddleName.Focus()
                    Exit Function
                End If
            End If
            If txtGuestLastName.Text.Trim() <> "" Then
                If objHotelreservation.ValidateAlphabet(txtGuestLastName.Text.Trim()) = False Then
                    ShowMessage("Special characters are not allowed", "Information")
                    txtGuestLastName.Focus()
                    Exit Function
                End If
            End If
            If txtGuestMobileNumber.Text.Trim() <> "" Then
                If objHotelreservation.ValidateNumberic(txtGuestMobileNumber.Text.Trim()) = False Then
                    ShowMessage("Invalid mobile number", "Information")
                    Exit Function
                End If
                If txtGuestMobileNumber.Text.Trim().Count < 10 Then
                    ShowMessage("Mobile number must be in 10 digit", "Information")
                    txtGuestMobileNumber.Focus()
                    Exit Function
                End If
            End If
            If txtGuestEmailId.Text.Trim() <> "" Then
                If objHotelreservation.ValidateEmail(txtGuestEmailId.Text.Trim()) = False Then
                    ShowMessage("Please provide email id in proper format, e.g abc@gmail.com", "Information")
                    txtGuestEmailId.Focus()
                    Exit Function
                End If
            End If
            If txtAge.Text.Trim() <> "" Then
                If objHotelreservation.ValidateNumberic(txtAge.Text.Trim()) = False Then
                    ShowMessage("Invalid age", "Information")
                    txtAge.Focus()
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
    Private Function ValidatedGuestDetails() As Boolean
        Try

            If txtGuestFirstName.Text.Trim() = "" Then
                txtGuestFirstName.Focus()
                Exit Function
            ElseIf txtGuestLastName.Text.Trim() = "" Then
                txtGuestLastName.Focus()
                Exit Function
            ElseIf txtGuestMobileNumber.Text.Trim() = "" Then
                txtGuestMobileNumber.Focus()
                Exit Function
                'ElseIf txtGuestEmailId.Text.Trim() = "" Then
                '    txtGuestEmailId.Focus()
                '    Exit Function
            ElseIf txtAge.Text.Trim() = "" Then
                ' ShowMessage(getValueByKey("SOC009"), "SOC009 - " & getValueByKey("CLAE04"))
                txtAge.Focus()
                Exit Function
            ElseIf txtDocumentNo.Text.Trim() = "" Then
                ' ShowMessage(getValueByKey("SOC009"), "SOC009 - " & getValueByKey("CLAE04"))
                txtDocumentNo.Focus()
                Exit Function
            ElseIf String.IsNullOrEmpty(cmbGender.Text.Trim()) Then
                ' ShowMessage("Mobile Number is Mandatory", getValueByKey("CLAE04"))
                cmbGender.Focus()
                Exit Function
            End If

            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)

            Return False
        End Try
    End Function
    Private Function DeleteGuestDetails(srNo As Integer) As Boolean
        Try
            If dtGuest.Rows.Count > 0 Then
                Dim drDtl() = dtGuest.Select("SrNo=" & srNo & "")
                If drDtl.Count > 0 Then
                    For Each row As DataRow In drDtl
                        dtGuest.Rows.Remove(row)
                    Next
                End If
                dtGuest.AcceptChanges()
                Dim count = 1
                For index = 0 To dtGuest.Rows.Count - 1
                    dtGuest.Rows(index)("SrNo") = count
                    count += 1
                Next
                srNo = count
            End If
            gridGuestDetailsSetting()
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    'added by khusrao adil on 08-03-2017
    Private Function ClearGuestDetail()
        txtGuestFirstName.Text = String.Empty
        txtGuestMiddleName.Text = String.Empty
        txtGuestLastName.Text = String.Empty
        txtGuestEmailId.Text = String.Empty
        txtGuestMobileNumber.Text = String.Empty
        txtAge.Text = String.Empty
        cmbGender.Text = String.Empty
        txtDocumentNo.Text = String.Empty
        'cmdDocumentType.SelectedValue = String.Empty
        'cmdDocumentType.SelectedText = String.Empty
        chkBxPrimaryGuest.Checked = False
        btnGuestAdd.Text = "Add"
    End Function
#End Region
    ' "--end Functions"
#End Region
#Region "-----------------------------Summary Details Tab "

    Public Sub ReadonlySummaryData()
        Try
            'txtSumcheckin.ReadOnly = True
            'txtSumcheckout.ReadOnly = True
            'txtSumNumberOfAdult.ReadOnly = True
            'txtSumChildren.ReadOnly = True
            'txtSumNight.ReadOnly = True
            'txtSumDiscountAmount.ReadOnly = True
            'txtSumTotalTaxAmount.ReadOnly = True
            'txtSumFinalCost.ReadOnly = True
            'txtSumPhoneNo.ReadOnly = True
            'txtSumEmail.ReadOnly = True
            'txtSumGuestName.ReadOnly = True
            'txtSumPaidAmt.ReadOnly = True
            'txtSumPaymentRemaining.ReadOnly = True
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Public Sub SummaryInformation()
        Try
            txtSumcheckin.TextDetached = True
            txtSumcheckout.TextDetached = True
            txtSumcheckin.Text = checkIndate
            txtSumcheckout.Text = checkOutdate
            'txtAdult.Text = txtAdult.Text.Trim
            'txtSumChildren.Text = txtChild.Text.Trim
            'Dim ts As TimeSpan = DateTime.Parse(checkOutdate) - DateTime.Parse(checkIndate)
            noOfNight = DateDiff(DateInterval.Day, checkIndate, checkOutdate)
            noOfDays = DateInterval.Day

            txtSumNight.TextDetached = True
            txtSumDiscountAmount.TextDetached = True
            txtSumFinalCost.TextDetached = True
            txtSumTotalTaxAmount.TextDetached = True
            txtSumPaidAmt.TextDetached = True
            txtSumPaymentRemaining.TextDetached = True
            txtSumNight.Text = noOfNight.ToString()
            txtSumDiscountAmount.Text = txtPromoDiscount.Text.Trim
            txtSumFinalCost.Text = txtPromoCost.Text.Trim
            txtSumTotalTaxAmount.Text = txtPromoTax.Text
            txtSumPaidAmt.Text = 0
            txtSumPaymentRemaining.Text = txtSumFinalCost.Text
            For Each dr As DataRow In dtGuest.Select("PrimaryGuest=True")
                txtSumPhoneNo.TextDetached = True
                txtSumEmail.TextDetached = True
                ' txtSumGuestName.TextDetached = True
                txtSumPhoneNo.Text = dr("MobileNumber")
                txtSumEmail.Text = dr("EmailId")
                txtSumGuestName.Text = dr("FirstName") & " " & dr("LastName")
            Next
            'Dim Adult = dtGuest.Compute("Count(Gender)", "")
            Dim Adult = dtGuest.Select("age>18")
            txtSumNumberOfAdult.TextDetached = True
            txtSumNumberOfAdult.Text = Adult.Count
            txtSumChildren.TextDetached = True
            Dim Children = dtGuest.Select("age<18")
            txtSumChildren.Text = Children.Count
            ReadonlySummaryData()
            'If grdGuestDetails.Rows.Count > 0 Then
            '    ' If grdGuestDetails.Rows(0)("Sel") = True Then
            '    txtPhoneNo.Text = grdGuestDetails.Rows(0)("PhoneNumber")
            '    txtEmail.Text = grdGuestDetails.Rows(0)("Email-ID")
            '    txtguestName.Text = grdGuestDetails.Rows(0)("FirstName") & " " & grdGuestDetails.Rows(0)("LastName")
            '    '   End If
            'Else
            '    ShowMessage("Please Select Guest", "Information")
            '    Exit Sub
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub gridSummaryDetailsSetting()
        Try
            Me.gridSummaryDetails.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
            '  Me.gridSummaryDetails.Size = New Size(1375, 263) x
            Me.gridSummaryDetails.Styles.Fixed.BackColor = Color.FromArgb(208, 208, 208)
            Me.gridSummaryDetails.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
            Me.gridSummaryDetails.Styles.Fixed.Font = New Font("Callibri", 10, FontStyle.Bold)
            Me.gridSummaryDetails.Styles.Focus.Font = New Font("Callibri", 10, FontStyle.Bold)
            Me.gridSummaryDetails.Styles.Highlight.BackColor = Color.FromArgb(252, 252, 252)
            Me.gridSummaryDetails.Focus()
            gridSummaryDetails.DataSource = dtSummary

            gridSummaryDetails.Cols("RoomNo").Width = 100
            gridSummaryDetails.Cols("RoomNo").DataType = Type.GetType("System.String")
            gridSummaryDetails.Cols("RoomNo").AllowEditing = False
            gridSummaryDetails.Cols("RoomNo").Name = "RoomNo"
            gridSummaryDetails.Cols("RoomNo").Visible = True


            gridSummaryDetails.Cols("RoomType").Width = 100
            gridSummaryDetails.Cols("RoomType").DataType = Type.GetType("System.String")
            gridSummaryDetails.Cols("RoomType").AllowEditing = False
            gridSummaryDetails.Cols("RoomType").Name = "RoomType"
            ' gridSummaryDetails.Cols("RoomType").TextAlign = TextAlignEnum.LeftCenter

            gridSummaryDetails.Cols("Ameneties").Width = 451
            gridSummaryDetails.Cols("Ameneties").DataType = Type.GetType("System.String")
            gridSummaryDetails.Cols("Ameneties").AllowEditing = False
            gridSummaryDetails.Cols("Ameneties").Name = "Ameneties"
            '  gridSummaryDetails.Cols("Ameneties").TextAlign = TextAlignEnum.LeftCenter

            gridSummaryDetails.Cols("Promotions").Width = 100
            gridSummaryDetails.Cols("Promotions").DataType = Type.GetType("System.String")
            gridSummaryDetails.Cols("Promotions").AllowEditing = False
            gridSummaryDetails.Cols("Promotions").Name = "Promotions"
            ' gridSummaryDetails.Cols("Promotions").TextAlign = TextAlignEnum.LeftCenter

            gridSummaryDetails.Cols("Price").Width = 100 'Price
            gridSummaryDetails.Cols("Price").DataType = Type.GetType("System.Decimal")
            gridSummaryDetails.Cols("Price").AllowEditing = False
            gridSummaryDetails.Cols("Price").Name = "Price"
            gridSummaryDetails.Cols("Price").TextAlign = TextAlignEnum.LeftCenter

            gridSummaryDetails.Cols("Tax").Width = 100
            gridSummaryDetails.Cols("Tax").DataType = Type.GetType("System.Decimal")
            gridSummaryDetails.Cols("Tax").Format = "0"
            gridSummaryDetails.Cols("Tax").Name = "Tax"
            gridSummaryDetails.Cols("Tax").AllowEditing = False
            gridSummaryDetails.Cols("Tax").TextAlign = TextAlignEnum.LeftCenter

            gridSummaryDetails.Cols("Discount").Width = 100
            gridSummaryDetails.Cols("Discount").DataType = Type.GetType("System.Decimal")
            gridSummaryDetails.Cols("Discount").AllowEditing = False
            gridSummaryDetails.Cols("Discount").Name = "Discount"
            gridSummaryDetails.Cols("Discount").TextAlign = TextAlignEnum.LeftCenter

            gridSummaryDetails.Cols("FinalCost").Width = 150
            gridSummaryDetails.Cols("FinalCost").DataType = Type.GetType("System.Decimal")
            gridSummaryDetails.Cols("FinalCost").Name = "FinalCost"
            gridSummaryDetails.Cols("FinalCost").TextAlign = TextAlignEnum.LeftCenter
            gridSummaryDetails.Cols("FinalCost").AllowEditing = False
            gridSummaryDetails.Cols("Selected").Visible = False

            gridSummaryDetails.Cols("reservationRoomMapID").Visible = False
            gridSummaryDetails.Cols("reservationID").Visible = False
            gridSummaryDetails.Cols("MstRoomId").Visible = False

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub


    Public Sub LoadSummaryInformationIntoGrid(ByVal dtRoomwisePromotion As DataTable) ''ByVal dtSummary As DataTable
        Try
            dtSummary = objHotelreservation.GetDetailsForSummaryTab()
            dtSummary.Rows.Clear()
            'dtRoomwisePromotion.Merge(dtReservation)
            For Each dr As DataRow In dtReservation.Select("Selects=True")
                Dim SelectedRoomsCost As Decimal = 0.0
                Dim promotionsDiscountAmount As Decimal = 0.0
                Dim promotionsDiscountInPercent As Decimal = 0.0
                Dim FinalCost As Decimal = 0.0
                Dim _calculateTax As Decimal = 0
                Dim TotalTax As Decimal = 0.0
                Dim Promotions As String = String.Empty
                Dim _totalTaxt As Decimal = 0.0
                Dim tCost As Double
                Dim tTax As Double
                Dim TotalCost As Double
                Dim st1 As String
                Dim drResult As DataRow()

                _selectedPromotion = ""
                Dim rowReservation As DataRow
                rowReservation = dtSummary.NewRow
                ' drResult = dtRoomwisePromotion.Select("mstRoomID= '" + dr("mstRoomID").ToString + "'")
                drResult = dtRoomwisePromotion.Select("RoomTypeId= '" + dr("RoomTypeId").ToString + "'")
                If drResult.Length > 0 Then
                    promotionsDiscountAmount = Convert.ToDecimal(drResult(0)("DiscountAmount"))
                    Promotions = drResult(0)("MSTPROMOTIONNAME")
                    promotionsDiscountInPercent = If(drResult(0)("discountInPercent") Is String.Empty, 0, drResult(0)("discountInPercent"))
                End If
                rowReservation("Promotions") = Promotions
                Dim stringPromotionsDiscountInPercent As String = Convert.ToString(promotionsDiscountInPercent)
                rowReservation("Discount") = stringPromotionsDiscountInPercent + " %"
                SelectedRoomsCost = Convert.ToDecimal(dr("Cost"))
                rowReservation("RoomNo") = dr("RoomNumber")
                rowReservation("MstRoomId") = dr("MstRoomId")
                rowReservation("RoomType") = dr("RoomType")
                rowReservation("Ameneties") = dr("Amenities")
                rowReservation("Price") = If(dr("Cost") Is DBNull.Value, 0, dr("Cost"))
                If dtHotelAllTaxes.Rows.Count > 0 Then
                    TotalTax = Convert.ToDecimal(dtHotelAllTaxes.Compute("Sum(Tax)", ""))
                End If
                _calculateTax = ((SelectedRoomsCost - promotionsDiscountAmount) / 100) * TotalTax
                FinalCost = (SelectedRoomsCost - promotionsDiscountAmount) + _calculateTax
                rowReservation("FinalCost") = Math.Round(FinalCost, 2)
                Dim StringPromotionalTax As String = Convert.ToString(TotalTax)
                rowReservation("Tax") = StringPromotionalTax + " %"
                'tCost = Convert.ToDouble(dr("Cost"))
                'Dim tax = +If(dr("Tax") Is String.Empty, 0, dr("Tax"))
                'tTax = Convert.ToDouble(tax)
                'TotalCost = tCost + tTax
                'If drResult.Count > 0 Then
                '    rowReservation("Promotions") = Convert.ToString(drResult(0)("MSTPROMOTIONNAME"))
                '    rowReservation("Discount") = Convert.ToString(drResult(0)("discountInPercent"))
                '    rowReservation("FinalCost") = TotalCost - rowReservation("Discount")
                'End If



                dtSummary.Rows.Add(rowReservation)
            Next
            gridSummaryDetailsSetting()
            SummaryInformation()
            ' ReadonlySummaryData()   (If(dr("Tax") Is DBNull.Value, 0, dr("Tax")))
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

#End Region
#Region "-----------------------------Payment Details tab  "

    Public Sub UpdatePaymentPrevStru(ByRef dt As DataTable)



        Try
            dt.TableName = "MstRecieptType"
            dt.Columns("receiptLineNo").ColumnName = "SRNO"
            dt.Columns("TENDERTYPECODE").ColumnName = "RECIEPTTYPE"
            'dt.Columns("ChangeLineNo").ColumnName = "RECIEPT"
            dt.Columns("TENDERHEADCODE").ColumnName = "RECIEPTTYPECODE"
            dt.Columns("AMOUNTTENDERED").ColumnName = "AMOUNT"
            dt.Columns("CARDNO").ColumnName = "NUMBER"
            dt.Columns("REFDATE").ColumnName = "DATE"
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function ValidateAll() As Boolean
        Try
            Dim TotalNetValue As Double = txtAdvFinalCost.Text
            Dim TotalCollection As Double = txtAdvFinalCost.Text '''Val(dsMain.Tables("CASHMEMORECEIPT").Compute("Sum(AMOUNTTENDERED)", "").ToString())
            'changed by ram dt : 24.05.2009 action: changed
            'If TotalNetValue <> TotalCollection Then
            '    ShowMessage("Payment is not Settle", "Information")
            '    Return False
            'End If

            If TotalNetValue < 0 AndAlso clsDefaultConfiguration.IssueCreditVoucher = False Then

                ShowMessage(getValueByKey("CM042"), "CM042 - " & getValueByKey("CLAE04"))
                Return False
            End If

            If Math.Round(Math.Abs(TotalNetValue), 2) <> Math.Round(Math.Abs(TotalCollection), 2) Then
                ShowMessage(getValueByKey("CM043"), "CM043 - " & getValueByKey("CLAE04"))

                Return False
            End If

            If TotalNetValue < 0 AndAlso clsAdmin.CVProgram = String.Empty Then
                ShowMessage(getValueByKey("CM056"), "CM056 - " & getValueByKey("CLAE05"))
                Return False
            End If



            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Sub PaymentGridSetting()
        Try
            'Payment.CtrlListPayment.Columns("RECIEPTTYPE").Caption = getValueByKey("frmnbirthlistsales.ctrlpayment1.ctrllistpayment.payment mode")
            'Payment.CtrlListPayment.Columns("AMOUNTRECEIVED").Caption = getValueByKey("frmnbirthlistsales.ctrlpayment1.ctrllistpayment.amount")
            'For Each r As C1.Win.C1List.Split In Payment.CtrlListPayment.Splits
            '    Dim i As Integer
            '    For i = 0 To r.DisplayColumns.Count - 1
            '        If r.DisplayColumns(i).DataColumn.DataField.ToUpper() <> "AMOUNTRECEIVED".ToUpper() And r.DisplayColumns(i).DataColumn.DataField.ToUpper() <> "RECIEPTTYPE".ToUpper() Then
            '            r.DisplayColumns(i).Visible = False
            '        End If
            '    Next
            'Next
            'Payment.CtrlListPayment.ExtendRightColumn = True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub getBinding()
        Try
            dsMain = objCM.GetStruc("0", "0")
            dsMain.Tables("CASHMEMODTL").Columns("IsPriceChanged").DefaultValue = False
            'DvItemDetail = New DataView(dsMain.Tables("CASHMEMODTL"), "", "BillLineNo Desc", DataViewRowState.CurrentRows)
            'dsMain.Tables("CASHMEMODTL").DefaultView.Sort = "BillLineNo Desc"
            gridSummaryDetails.DataSource = dsMain.Tables("CASHMEMODTL")

            'Payment.CtrlListPayment.DataSource = dsMain.Tables("CASHMEMORECEIPT")
            'CustInfo.CtrlTxtCustomerNo.DataBindings.Add("Text", dsMain.Tables("CASHMEMOHDR"), "CLPNO")
            'CashSummary.CtrllblDiscAmt.DataBindings.Add("Text", dsMain.Tables("CASHMEMOHDR"), "TOTALDISCOUNT")
            'CashSummary.CtrllblGrossAmt.DataBindings.Add("Text", dsMain.Tables("CASHMEMOHDR"), "GROSSAMT")
            'CashSummary.CtrllblNetAmt.DataBindings.Add("Text", dsMain.Tables("CASHMEMOHDR"), "NETAMT")


            'dtMainTax = objCM.getTax("", "", "", "0", "")
            'dtMainTax.TableName = "CASHMEMOTAXDTLS"

            ''GridSettings(UpdateFlag)


        Catch ex As Exception
            ShowMessage(getValueByKey("CM005"), "CM005 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Transaction Not properly Binded", "Error")
        End Try
    End Sub

    Private Sub GetCashMemoDetails(ByVal strCashMemo As String, ByVal strSiteCode As String)
        Try
            Dim dsTemp As DataSet
            dsTemp = objCM.GetStruc(strCashMemo, strSiteCode)
            dsMain.Clear()
            If Not dsTemp Is Nothing AndAlso dsTemp.Tables.Count > 0 Then
                dsMain.Tables("CASHMEMOHDR").Merge(dsTemp.Tables(0), False, MissingSchemaAction.Ignore)
                dsMain.Tables("CASHMEMODTL").Merge(dsTemp.Tables(1), False, MissingSchemaAction.Ignore)
                dsMain.Tables("CASHMEMORECEIPT").Merge(dsTemp.Tables(2), False, MissingSchemaAction.Ignore)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub UpdatePaymentDataSetStru(ByRef ds As DataSet, ByVal UpdateFlag As Boolean)
        Try

            ds.Tables(0).TableName = "Host_ReservationReceipt"
            ds.Tables(0).Columns("SRNO").ColumnName = "RECEIPTLINENO"
            ds.Tables(0).Columns("RECIEPTTYPE").ColumnName = "TENDERHEADCODE"
            ds.Tables(0).Columns("RECIEPTTYPECODE").ColumnName = "TENDERTYPECODE"
            ds.Tables(0).Columns("RECIEPT").ColumnName = "CHANGELINE"
            ds.Tables(0).Columns("AMOUNT").ColumnName = "AMOUNTTENDERED"
            ds.Tables(0).Columns("CURRENCYCODE").ColumnName = "CURRENCYCODE"
            ds.Tables(0).Columns("AMOUNTINCURRENCY").ColumnName = "AMOUNTINCURRENCY"
            ds.Tables(0).Columns("EXCHANGERATE").ColumnName = "EXCHANGERATE"
            ds.Tables(0).Columns("NUMBER").ColumnName = "CARDNO"
            ds.Tables(0).Columns("DATE").ColumnName = "BILLRECEIPTDATE"
            ds.Tables(0).Columns("RefNo_3").ColumnName = "billReceiptTime"
            If UpdateFlag = True Then
                For Each dr As DataRow In ds.Tables(0).Rows
                    If dr.RowState = DataRowState.Added Then
                        dr("Sitecode") = dsMain.Tables("CashMemoHdr").Rows(0)("Sitecode")
                        dr("Billno") = dsMain.Tables("CashMemoHdr").Rows(0)("billno")
                        dr("TerminalId") = clsAdmin.TerminalID   'dsMain.Tables("CashMemoHdr").Rows(0)("Sitecode")
                        dr("CMRCPTDATE") = Now
                        dr("CMRCPTTIME") = Now
                    End If
                Next
            Else
                Dim dv As New DataView(ds.Tables(0), "TenderTypeCode='CASH'", "", DataViewRowState.CurrentRows)
                If dv.Count > 0 Then
                    dv.AllowEdit = True
                    For Each dr As DataRowView In dv
                        dr("CARDNO") = Nothing
                    Next
                End If
                ds.Tables(0).AcceptChanges()
            End If

            If ds.Tables.Contains("CheckDtls") Then
                Dim dtCheckDtls As New DataTable
                dtCheckDtls = ds.Tables("CheckDtls").Copy
                dtCheckDtls.TableName = "CheckDtls"
                dtCheckDtls.AcceptChanges()
                If Not dsMain.Tables.Contains("CheckDtls") Then
                    dsMain.Tables.Add(dtCheckDtls)
                Else
                    dsMain.Tables("CheckDtls").Merge(dtCheckDtls)
                End If

            End If

            PaymentGridSetting()
        Catch ex As Exception
            ShowMessage(getValueByKey("CM011"), "CM011 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in updating the payment data Structure", "Error")
        End Try
    End Sub

    Public Sub loadPaymentDetails()
        Try

            '''''''''''''''''''with Pay
            txtAdvTotalDiscountAmount.TextDetached = True
            txtAdvTotalTaxAmount.TextDetached = True
            txtAdvFinalCost.TextDetached = True
            txtAdvPaidAmount.TextDetached = True
            txtAdvPaymentRemaining.TextDetached = True

            txtAdvCheckin.TextDetached = True
            txtAdvCheckout.TextDetached = True
            txtAdvGuestName.TextDetached = True
            txtAdvPhoneNo.TextDetached = True
            txtAdvNoOfAdult.TextDetached = True
            txtAdvEmail.TextDetached = True
            txtAdvNoOfNight.TextDetached = True
            txtAdvNoOfChildren.TextDetached = True

            'txtAdvTotalDiscountAmount.ReadOnly = True
            'txtAdvTotalTaxAmount.ReadOnly = True
            'txtAdvFinalCost.ReadOnly = True
            'txtAdvPaidAmount.ReadOnly = True
            'txtAdvPaymentRemaining.ReadOnly = True

            'txtAdvCheckin.ReadOnly = True
            'txtAdvCheckout.ReadOnly = True
            'txtAdvGuestName.ReadOnly = True
            'txtAdvPhoneNo.ReadOnly = True
            'txtAdvNoOfAdult.ReadOnly = True
            'txtAdvEmail.ReadOnly = True
            'txtAdvNoOfNight.ReadOnly = True
            'txtAdvNoOfChildren.ReadOnly = True

            txtAdvTotalDiscountAmount.Text = txtSumDiscountAmount.Text
            txtAdvTotalTaxAmount.Text = txtSumTotalTaxAmount.Text
            txtAdvFinalCost.Text = txtSumFinalCost.Text
            txtAdvPaidAmount.Text = 0
            txtAdvPaymentRemaining.Text = txtSumFinalCost.Text

            txtAdvCheckin.Text = txtCheckedIn.Text
            txtAdvCheckout.Text = txtCheckedOut.Text
            txtAdvGuestName.Text = txtSumGuestName.Text
            txtAdvPhoneNo.Text = txtSumPhoneNo.Text
            txtAdvNoOfAdult.Text = txtSumNumberOfAdult.Text
            txtAdvEmail.Text = txtSumEmail.Text
            txtAdvNoOfNight.Text = txtSumNight.Text
            txtAdvNoOfChildren.Text = txtSumChildren.Text


            '---------------------------without pay
            txtWPayDiscountAmot.TextDetached = True
            txtWPayTotalTaxAmount.TextDetached = True
            txtWPayFinalCost.TextDetached = True
            txtAdvFinalCost.TextDetached = True
            txtWPayPaidAmount.TextDetached = True
            txtWPayRemainingPayment.TextDetached = True

            txtWPayCheckin.TextDetached = True
            txtWPayCheckout.TextDetached = True
            txtWPayGuestName.TextDetached = True
            txtWPayPhoneNo.TextDetached = True
            txtWPayNoOfAdult.TextDetached = True
            txtWPayEmail.TextDetached = True
            txtWPayNoOfNight.TextDetached = True
            txtWPayNoOfChildren.TextDetached = True

            txtWPayDiscountAmot.TextDetached = True
            txtWPayTotalTaxAmount.TextDetached = True
            txtWPayFinalCost.TextDetached = True
            txtAdvFinalCost.TextDetached = True
            txtWPayPaidAmount.TextDetached = True
            txtWPayRemainingPayment.TextDetached = True

            'txtWPayCheckin.ReadOnly = True
            'txtWPayCheckout.ReadOnly = True
            'txtWPayGuestName.ReadOnly = True
            'txtWPayPhoneNo.ReadOnly = True
            'txtWPayNoOfAdult.ReadOnly = True
            'txtWPayEmail.ReadOnly = True
            'txtWPayNoOfNight.ReadOnly = True
            'txtWPayNoOfChildren.ReadOnly = True
            'txtWPayDiscountAmot.ReadOnly = True
            'txtWPayTotalTaxAmount.ReadOnly = True
            'txtWPayFinalCost.ReadOnly = True
            'txtAdvFinalCost.ReadOnly = True
            'txtWPayPaidAmount.ReadOnly = True
            'txtWPayRemainingPayment.ReadOnly = True


            txtWPayDiscountAmot.Text = txtSumDiscountAmount.Text
            txtWPayTotalTaxAmount.Text = txtSumTotalTaxAmount.Text
            txtWPayFinalCost.Text = txtSumFinalCost.Text
            txtWPayPaidAmount.Text = 0
            txtWPayRemainingPayment.Text = txtSumFinalCost.Text

            txtWPayCheckin.Text = txtCheckedIn.Text
            txtWPayCheckout.Text = txtCheckedOut.Text
            txtWPayGuestName.Text = txtSumGuestName.Text
            txtWPayPhoneNo.Text = txtSumPhoneNo.Text
            txtWPayNoOfAdult.Text = txtSumNumberOfAdult.Text
            txtWPayEmail.Text = txtSumEmail.Text
            txtWPayNoOfNight.Text = txtSumNight.Text
            txtWPayNoOfChildren.Text = txtSumChildren.Text

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

#Region "-----------------------------Page events"

    Private Sub frmHostCreateReservation_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
            End If

            If Me.tpPaymentsDetails.IsSelected = True Then
                If Me.tpAdvancePayment.IsSelected = True Then
                    If e.KeyCode = Keys.F4 Then
                        cmdPayment_Click(cmdPayment, New System.EventArgs)
                    End If
                    If e.KeyCode = Keys.F5 Then
                        cmdCash_Click(cmdCash, New System.EventArgs)
                    End If
                    If e.KeyCode = Keys.F6 Then
                        cmdCard_Click(cmdCard, New System.EventArgs)
                    End If
                    If e.KeyCode = Keys.F7 Then
                        cmdCheque_Click(cmdCheque, New System.EventArgs)
                    End If
                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdPayment_Click(sender As Object, e As EventArgs) Handles cmdPayment.Click
        Try
            ISCallFromPayment = True
            PrepareDataSave()
            Dim ds As DataSet
            Dim objPayment As frmNAcceptPayment
            '  CLP_Data.Sitecode = clsAdmin.SiteCode
            ' getclpsettings()

            'If Not CLP_Data.CLPConfigdata.Tables("CLPHeader") Is Nothing AndAlso CLP_Data.CLPConfigdata.Tables("CLPHeader").Rows.Count > 0 AndAlso CLP_Data.CLPConfigdata.Tables("CLPHeader")(0)("RedemptionType").ToString().ToLower() = "rdt1" Then
            '    objPayment = New frmNAcceptPayment(dsMain.Tables("CASHMEMODTL"))
            'Else
            '    objPayment = New frmNAcceptPayment()
            'End If
            objPayment = New frmNAcceptPayment()
            objPayment.IsTenderChange = UpdateFlag
            objPayment.remarksTextbox.Text = _remarks
            objPayment.IsFastCashMemo = True
            objPayment.remarksTextbox.Text = _remarks
            objPayment.TotalBillAmount = CDbl(txtAdvFinalCost.Text)
            objPayment.ParentRelation = "CashMemo"
            objPayment._IsCashierPromoSelected = isCashierPromoSelected
            ' If dsMainHost.Tables("Host_ReservationReceipt").Rows.Count > 0 Then
            ds = objPayment.ReciptTotalAmount
            Dim dt As DataTable = dsMainHost.Tables("Host_ReservationReceipt").Copy()
            ' ds = New DataSet()
            ds.Tables.Add(dt)
            UpdatePaymentPrevStru(ds.Tables("Host_ReservationReceipt"))

            If Not ds.Tables("MstRecieptType").Columns.Contains("NOCLP") Then
                ds.Tables("MstRecieptType").Columns.Add("NOCLP", System.Type.GetType("System.Boolean"))
            End If

            objPayment.AcceptEditBillDataSet = ds
            objPayment.PaymentType = clsAcceptPayment.PaymentType.EditBill
            ' End If

            objPayment.TopMost = True
            objPayment.RoundAt = clsDefaultConfiguration.BillRoundOffAt
            objPayment.ShowDialog(Me)
            PaymentTermId = objPayment.PaymentTermNameId
            If True Then
                _billAmt = objPayment.TotalBillAmount
                _paidAmt = objPayment.TotalCustomerPadiAmount
            End If

            If objPayment.IsCancelAcceptPayment = False Then

                'Added by Rohit for CR5938
                _dDueDate = objPayment.dDueDate
                _strRemarks = objPayment.strRemarks

                ds = objPayment.ReciptTotalAmount()
                '  cmdLoyalty_Click(sender, e, ds.Tables("MSTRecieptType"))
                If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
                    ' UpdatePaymentDataSetStru(ds, UpdateFlag)


                    If objPayment.Action = "Save" Then

                        If PrepareReceiptData(ds.Tables(0), dsMainHost) = True Then
                            'error
                        End If
                        ' SaveAndPrintReservation()
                        ReservationId = dsMainHost.Tables("Host_Reservation").Rows(0)("reservationID").ToString()
                        ReservationRNumber = dsMainHost.Tables("Host_Reservation").Rows(0)("reservationNumber").ToString()
                        'Dim disStatusMgs = "Reservation Saved Successfully for"  'for reservation
                        '"Check-In Successfull for"  ' for check in
                        'ReservationRNumber = "RN0010000000020"
                        'ReservationGuestName = "Khusrao Khan"
                        ReservationDate = System.DateTime.Now.ToString("dd/MM/yyyy")
                        If SaveAndPrintReservation(False) = True Then
                            ShowMessage("Save Successfull for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
                        Else
                            ShowMessage("Save Failed for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
                        End If
                    End If
                    Me.Close()
                    'cmdSavePrint_Click(sender, e)

                    'If cmdSavePrint_Click(sender, e) Then
                    '    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                    'End If
                ElseIf objPayment.Action = "Gift" Then

                    '*********************Rahul Changes Start 02 Nov*********************.
                    'Dim obj As New frmSpecialPrompt("Gift Message ")
                    'obj.ShowTextBox = True
                    'obj.ShowDialog()
                    'If Not obj.GetResult Is Nothing Then
                    '    GiftMsg = obj.GetResult
                    'End If
                    GiftMsg = objPayment.GiftReceiptMessage
                    '*********************Rahul Changes End 02 Nov*********************.

                    'If cmdGiftPrint_Click(sender, e) Then
                    '    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                    'End If
                End If
            ElseIf ds.Tables.Count = 0 AndAlso CDbl(txtSumFinalCost.Text) = 0 Then
                '  cmdHold.Enabled = False
                If objPayment.Action = "Save" Then
                    cmdSavePrint_Click(sender, e)
                End If
            End If
            'End If
            ''--------------------------------------------------------------------------------------------------------------------------------------------------------------
            If clsDefaultConfiguration.IsTablet Then
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
            End If
            ' End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM031"), "CM031 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in updating the payment data to main", "Error")
        End Try
    End Sub


    Private Sub cmdCash_Click(sender As Object, e As EventArgs) Handles cmdCash.Click
        Try
            ISCallFromPayment = True
            PrepareDataSave()
            ' If IsTenderCash Then
            Dim objPaymentByCash As New frmNAcceptPaymentByCash
            objPaymentByCash.txtRemark.Text = _remarks
            objPaymentByCash._IsCashierPromoSelected = isCashierPromoSelected
            objPaymentByCash.TotalBillAmount = CDbl(txtAdvFinalCost.Text)
            objPaymentByCash.ShowDialog()
            If Not (objPaymentByCash.IsCancelAcceptPayment) Then
                ISCallFromPayment = True
                If Not objPaymentByCash.ReciptTotalAmount Is Nothing And objPaymentByCash.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                    Dim ds As DataSet = objPaymentByCash.ReciptTotalAmount
                    _billAmt = objPaymentByCash.TotalBillAmount
                    _paidAmt = objPaymentByCash.TotalCustomerPadiAmount
                    _remarks = objPaymentByCash.txtRemark.Text
                    txtAdvPaymentRemaining.Text = _paidAmt - _billAmt
                    objPaymentByCash.Close()
                    'If Not ds Is Nothing Then
                    UpdatePaymentDataSetStru(ds, UpdateFlag)
                    ' cmdHold.Enabled = False
                    PaymentGridSetting()
                    If UpdateFlag = False Then
                        Dim dt As New DataTable
                        dt = ds.Tables("Host_ReservationReceipt").Copy()
                        dt.Columns("TenderHeadCode").ColumnName = "Reciepttypecode"
                        'cmdLoyalty_Click(sender, e, dt)
                        dt.Dispose()
                    End If
                    If objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeSave Then
                        'cmdSavePrint_Click(sender, e)
                        If PrepareReceiptData(ds.Tables(0), dsMainHost) = True Then
                            'error
                        End If
                        ReservationId = dsMainHost.Tables("Host_Reservation").Rows(0)("reservationID").ToString()
                        ReservationRNumber = dsMainHost.Tables("Host_Reservation").Rows(0)("reservationNumber").ToString()
                        'Dim disStatusMgs = "Reservation Saved Successfully for"  'for reservation
                        '"Check-In Successfull for"  ' for check in
                        'ReservationRNumber = "RN0010000000020"
                        'ReservationGuestName = "Khusrao Khan"
                        ReservationDate = System.DateTime.Now.ToString("dd/MM/yyyy")
                        If SaveAndPrintReservation(False) = True Then
                            cmdCash.Enabled = False 'vipin
                            cmdCard.Enabled = False
                            cmdCheque.Enabled = False
                            cmdPayment.Enabled = False
                            ShowMessage("Saved Successfully for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
                        Else
                            ShowMessage("Save Failed for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
                        End If
                        ' Me.Close()
                    End If
                Else
                    ShowMessage(getValueByKey("BL095"), "BL095 - " & getValueByKey("CLAE05"))
                End If
            End If
            ' End If
            If clsDefaultConfiguration.IsTablet Then
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
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdCard_Click(sender As Object, e As EventArgs) Handles cmdCard.Click
        Try
            ISCallFromPayment = True
            PrepareDataSave()
            Dim objPayment As New frmNAcceptPaymentByCard()
            objPayment.TotalBillAmount = CDbl(txtAdvFinalCost.Text)
            'objPayment.cboCurrency.SelectedIndex = 1
            objPayment.ShowDialog()
            Dim selectedTenderName As String = objPayment.SelectedTenderName
            Dim strCardTenderCode As String = objPayment.CardTenderCode
            objPayment.Close()
            If Not (objPayment.IsCancelAcceptPayment) Then
                ISCallFromPayment = True
                If Not objPayment.ReciptTotalAmount Is Nothing And objPayment.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                    Dim ds As DataSet = objPayment.ReciptTotalAmount
                    'Dim ds As New DataSet()
                    'ds.Tables.Add(dt)
                    txtAdvPaymentRemaining.Text = _paidAmt - _billAmt
                    objPayment.Close()
                    'If Not ds Is Nothing Then
                    UpdatePaymentDataSetStru(ds, UpdateFlag)

                    PaymentGridSetting()
                    If UpdateFlag = False Then

                        Dim dt As New DataTable
                        dt = ds.Tables("Host_ReservationReceipt").Copy()
                        dt.Columns("TenderHeadCode").ColumnName = "Reciepttypecode"
                        ' cmdLoyalty_Click(sender, e, dt)
                        dt.Dispose()
                    End If
                    If objPayment.Action = My.Resources.AcceptPaymentActionTypeSave Then
                        If PrepareReceiptData(ds.Tables(0), dsMainHost) = True Then
                            'error
                        End If

                        Dim ReservationId = dsMainHost.Tables("Host_Reservation").Rows(0)("reservationID").ToString()
                        Dim ReservationRNumber = dsMainHost.Tables("Host_Reservation").Rows(0)("reservationNumber").ToString()
                        'Dim disStatusMgs = "Reservation Saved Successfully for"  'for reservation
                        '"Check-In Successfull for"  ' for check in
                        'ReservationRNumber = "RN0010000000020"
                        'ReservationGuestName = "Khusrao Khan"
                        ReservationDate = System.DateTime.Now.ToString("dd/MM/yyyy")
                        If SaveAndPrintReservation(False) = True Then
                            cmdCash.Enabled = False 'vipin
                            cmdCard.Enabled = False
                            cmdCheque.Enabled = False
                            cmdPayment.Enabled = False
                            ShowMessage("Save Successfully for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
                        Else
                            ShowMessage("Save Failed for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")

                        End If
                        '   Me.Close()
                        'If cmdSavePrint_Click(sender, e) Then
                        '    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                        'End If

                    ElseIf objPayment.Action = My.Resources.AcceptPaymentActionTypeGift Then
                        'Rahul Changes Start 02 Nov. 

                        'Dim obj As New frmSpecialPrompt("Gift Message ")
                        'obj.ShowTextBox = True
                        'obj.ShowDialog()
                        'If Not obj.GetResult Is Nothing Then
                        '    GiftMsg = obj.GetResult
                        'End If 
                        GiftMsg = objPayment.GiftReceiptMessage
                        'Rahul Changes End 02 Nov. 

                        'If cmdGiftPrint_Click(sender, e) Then
                        '    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                        'End If

                    End If
                Else
                    ShowMessage(getValueByKey("BL095"), "BL095 - " & getValueByKey("CLAE05"))
                End If
            End If
            If clsDefaultConfiguration.IsTablet Then
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
            End If
            ' End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdCheque_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCheque.Click
        Try
            ISCallFromPayment = True
            PrepareDataSave()
            Dim objCheck As New frmNCheckPayment
            objCheck.BillAmount = CDbl(txtAdvFinalCost.Text)
            objCheck.ShowDialog()
            If objCheck.IsCancelAcceptPayment = False Then
                ISCallFromPayment = True
                If Not objCheck.ReciptTotalAmount Is Nothing And objCheck.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                    Dim ds As DataSet = objCheck.ReciptTotalAmount
                    'Dim ds As New DataSet()
                    'ds.Tables.Add(dt)
                    txtAdvPaymentRemaining.Text = _paidAmt - _billAmt
                    objCheck.Close()
                    'If Not ds Is Nothing Then
                    UpdatePaymentDataSetStru(ds, UpdateFlag)

                    PaymentGridSetting()
                    If UpdateFlag = False Then

                        Dim dt As New DataTable
                        dt = ds.Tables("Host_ReservationReceipt").Copy()
                        dt.Columns("TenderHeadCode").ColumnName = "Reciepttypecode"
                        ' cmdLoyalty_Click(sender, e, dt)
                        dt.Dispose()
                    End If
                    If objCheck.Action = My.Resources.AcceptPaymentActionTypeSave Then

                        If PrepareReceiptData(ds.Tables(0), dsMainHost) = True Then
                            'error
                            'ElseIf dsMainHost.Tables.Contains("CheckDtls") Then
                            '    PrepareCheckPaymentDetails(ds.Tables(1), dsMainHost)
                        End If
                        Dim ReservationId = dsMainHost.Tables("Host_Reservation").Rows(0)("reservationID").ToString()
                        Dim ReservationRNumber = dsMainHost.Tables("Host_Reservation").Rows(0)("reservationNumber").ToString()
                        If SaveAndPrintReservation(False) = True Then
                            cmdCash.Enabled = False 'vipin
                            cmdCard.Enabled = False
                            cmdCheque.Enabled = False
                            cmdPayment.Enabled = False
                            ShowMessage("Save Successfully for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")

                        Else
                            ShowMessage("Save Failed for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
                        End If
                        'If cmdSavePrint_Click(sender, e) Then
                        '    ' AutoLogout(FrmTranCode, Me, lblLoggedIn)
                        'End If
                        Me.Close()
                    ElseIf objCheck.Action = My.Resources.AcceptPaymentActionTypeGift Then
                        'GiftMsg = objCheck.GiftReceiptMessage
                    End If
                    'End If
                Else
                    ShowMessage(getValueByKey("BL095"), "BL095 - " & getValueByKey("CLAE05"))
                End If
            End If
            If clsDefaultConfiguration.IsTablet Then
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
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function cmdSavePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) As Boolean
        'Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
        Try

            Cursor.Current = Cursors.WaitCursor
            Dim StrError As String = ""
            Dim billNo As String
            Dim dsTemp As DataSet
            objCM.CashMemoResetonDayClose = clsDefaultConfiguration.CashMemoResetonDayClose
            If ValidateAll() = True Then
                ' dsTemp = dsMain.Copy()
                If UpdateFlag = True Then
                    'If CheckInterTransactionAuth("UpdateBill", dsMain.Tables("CashMemoHdr")) = False Then Exit Sub
                    Dim strReason As String = String.Empty
                    Dim dt As DataTable = objCM.GetReasons("CMS")
                    Dim objReason As New frmNCommonView

                    objReason.SetData = dt
                    Array.Resize(objReason.ColumnName, dt.Columns.Count)
                    objReason.ColumnName(0) = "TRNSEQUENCENAME"
                    objReason.ShowDialog()
                    If objReason.search Is Nothing Then Exit Function
                    If Not objReason.search Is Nothing Then
                        strReason = objReason.search(0).ToString()
                    End If
                    '  dsTemp.Tables("CASHMEMOHDR").Rows(0)("AuthUserRemarks") = strReason
                    ' billNo = lblCMNo.Text
                    Dim dtTempGv As DataTable
                    'If Not dtGV Is Nothing Then
                    '    dtTempGv = dtGV.Copy()
                    'End If

                    For Each dr As DataRow In dsTemp.Tables("cashmemodtl").Rows
                        Dim qty As Decimal = Convert.ToDecimal(dr("Quantity"))
                        dr("Quantity") = Math.Round(qty, 3)
                    Next

                    For Each drh As DataRow In dsTemp.Tables("CASHMEMOHDR").Rows
                        Dim qty As Decimal
                        qty = Math.Round(Convert.ToDecimal(drh("TOTALITEMS")), 3)
                        drh("TOTALITEMS") = qty
                    Next

                    'If objCM.SaveCashMemo(clsAdmin.DayOpenDate, OnlineConnect, dsTemp, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.UserCode, UpdateFlag, billNo, StrError, clsAdmin.Financialyear, clsDefaultConfiguration.CashMemoStorageLocation, "CMS") Then
                    'Added by Rohit for Cr-5938
                    objCM.dDueDate = _dDueDate
                    objCM.strRemarks = _strRemarks
                    objCM.CashMemoResetonDayClose = clsDefaultConfiguration.CashMemoResetonDayClose
                    If objCM.SaveCashMemo(clsAdmin.DayOpenDate, OnlineConnect, dsTemp, Nothing, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.UserCode, UpdateFlag, billNo, StrError, clsAdmin.Financialyear, clsDefaultConfiguration.StockStorageLocation, "CMS", dtTempGv, clsAdmin.ClpArticle, CLPCustomerId, clsAdmin.CLPProgram, "", clsAdmin.CVProgram, clsAdmin.CreditValidDays, , , clsDefaultConfiguration.UpdateBillTime, clsDefaultConfiguration.IsMembership, UpdateStockAtStoreLevel:=clsDefaultConfiguration.UpdateStockAtStoreLevel) Then ''FloatAmt,

                        Dim objPrint As New clsCashMemoPrint(billNo, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving) '0000413

                        objPrint.DisplayArticleFullName = clsDefaultConfiguration.PrintItemFullName
                        objPrint.AllowDecimalQty = clsDefaultConfiguration.AllowDecimalQty
                        objPrint.WeightScaleEnabled = clsDefaultConfiguration.WeightScaleEnabled

                        objPrint.TokenNoRequiredInKOT = clsDefaultConfiguration.TokenNoRequiredInKOT
                        objPrint.CashMemoResetonDayClose = clsDefaultConfiguration.CashMemoResetonDayClose
                        objPrint.AllowBillingOnlyAfterSelectionOfSalesType = clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType
                        objPrint.PrintFormatNo = clsDefaultConfiguration.PrintFormatNo
                        objPrint.IsKotFontLarge = clsDefaultConfiguration.IsKotFontLarge
                        objPrint.IsKotFontBold = clsDefaultConfiguration.IsKotFontBold
                        objPrint.KOTPrintFormatNo = clsDefaultConfiguration.KOTPrintFormatNo
                        objPrint.MettlerConnString = clsDefaultConfiguration.MettlerConnString
                        objPrint.RoundOff = clsDefaultConfiguration.BillRoundOffAt
                        objPrint.IsDintInEnabled = clsDefaultConfiguration.DineInProcess

                        Dim ErrorMsg As String = ""

                        ''--- Code Added By Mahesh for delivery Person Name in edit Mode
                        'Dim deliveryPersonName As String = String.Empty
                        'If (dsTemp.Tables("CashMemoHdr").Rows.Count > 0 AndAlso EditMode_IsupdateDeliveryPersonAllowed) Then
                        '    deliveryPersonName = dsTemp.Tables("CashMemoHdr").Rows(0)("DeliveryPersonID").ToString()
                        '    objPrint.DeliveryPersonName = deliveryPersonName
                        'End If

                        'objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Duplicate", "", "", ErrorMsg, "", _billAmt, _paidAmt, Nothing, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath) '0000413
                        objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Duplicate", "", "", ErrorMsg, "", _billAmt, _paidAmt, Nothing, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsCounterCopyKot:=True, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges) '0000413



                        Dim TotalPay As Double
                        Dim clsVoucher As New clsPrintVoucher
                        If Not dtTempGv Is Nothing AndAlso dtTempGv.Rows.Count > 0 Then
                            Dim dv As New DataView(dtTempGv, "", "VOURCHERSERIALNBR", DataViewRowState.CurrentRows)
                            If dv.Count > 0 Then
                                For Each dr As DataRowView In dv
                                    ' objPrint.PrintGiftVoucher(dr("VOURCHERSERIALNBR").ToString(), dr("ValueOfVoucher").ToString(), CDate(IIf(dr("ExpiryDate") Is DBNull.Value, Now, dr("ExpiryDate"))), DateDiff(DateInterval.Day, dr("ExpiryDate"), dr("issuedondate")))
                                    clsVoucher.PrintGiftVoucherAndCreditNote("CMS", clsAdmin.SiteCode, "GiftVoucher", dr("VOURCHERSERIALNBR"), dr("ValueOfVoucher"), CDate(IIf(dr("ExpiryDate") Is DBNull.Value, Now, dr("ExpiryDate"))), clsAdmin.UserName, dr("IssuedDocNumber"), clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                                Next
                            End If
                        End If
                        For Each dr As DataRow In dsTemp.Tables("CashMemoReceipt").Select("TenderTypeCode='CreditVouc(I)'", "", DataViewRowState.CurrentRows)
                            TotalPay = IIf(dr("AmountTendered") > 0, dr("AmountTendered"), dr("AmountTendered") * -1)
                            'objPrint.PrintVoucher("CMS", TotalPay, clsDefaultConfiguration.VoucherText, clsAdmin.SiteCode, Errormsg, clsAdmin.CurrencyCode)
                            clsVoucher.PrintGiftVoucherAndCreditNote("CMS", clsAdmin.SiteCode, "CreditNote", String.Empty, TotalPay, String.Empty, clsAdmin.UserName, billNo, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                        Next
                        If ErrorMsg <> String.Empty Then
                            ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
                        End If
                        ' ClearData()
                        'cmdNew_Click(sender, e)
                        Return True
                    Else
                        ShowMessage(StrError, getValueByKey("CLAE05"))
                        Try
                            Throw New Exception(StrError & "line No 3070 cmd_savePrint_click for save")
                        Catch ex As Exception
                            LogException(ex)
                        End Try
                        Return False
                    End If
                Else
                    objCM.dDueDate = _dDueDate
                    objCM.strRemarks = _strRemarks

                    'If PrepareDataforSave(dsTemp) Then
                    ' RemoveDeletedRow(dsTemp.Tables("CASHMEMODTL"))
                    ' CreatingLineNO(dsTemp, "CASHMEMODTL")
                    Dim dtTempGv As DataTable

                    'For Each dr As DataRow In dsTemp.Tables("cashmemodtl").Rows

                    '    dr("Quantity") = Math.Round(Val(dr("Quantity")), 3)
                    '    'Math.Round(qty, 2)
                    'Next
                    'For Each drh As DataRow In dsTemp.Tables("CASHMEMOHDR").Rows

                    '    drh("TOTALITEMS") = Math.Round(Val(drh("TOTALITEMS")), 3)
                    '    'Math.Round(qty, 2)
                    'Next
                    Dim TotalPoints, RedemptionPoints As Double
                    Dim CLPRedemptionflag As Boolean = True
                    'If CLPCustomerId <> String.Empty Then
                    TotalPoints = txtAdvFinalCost.Text ''IIf(dsTemp.Tables("CashMemoDtl").Compute("SUM(CLPPoints)", "") Is DBNull.Value, 0, dsTemp.Tables("CashMemoDtl").Compute("SUM(CLPPoints)", ""))
                    If TotalPoints = 0 Then
                        TotalPoints = IIf(dsTemp.Tables("CashMemoDtl").Compute("SUM(CLPDiscount)", "") Is DBNull.Value, 0, dsTemp.Tables("CashMemoDtl").Compute("SUM(CLPDiscount)", ""))
                        If TotalPoints > 0 Then
                            TotalPoints = Convert.ToDouble(txtAdvFinalCost.Text)
                        End If
                    End If
                    If TotalPoints <> 0 Then
                        CLPRedemptionflag = False

                    End If
                    'End If
                    'If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count > 0 Then

                    '    SetCostPrice(clsDefaultConfiguration.isMAPbasedCost, dsCashMemoComboDtl.Tables("CashMemoComboDtl"), clsAdmin.SiteCode, "CostPrice")
                    'End If
                    'If Not IsDBNull(comboArticleCopy) Then
                    '    objCM.comboArticleCopy = comboArticleCopy
                    'End If

                    If objCM.SaveCashMemo(clsAdmin.DayOpenDate, OnlineConnect, dsTemp, dsCashMemoComboDtl, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.UserCode, UpdateFlag, billNo, StrError, clsAdmin.Financialyear, clsDefaultConfiguration.StockStorageLocation, "CMS", dtTempGv, clsAdmin.ClpArticle, CLPCustomerId, clsAdmin.CLPProgram, "", clsAdmin.CVProgram, clsAdmin.CreditValidDays, CLPRedemptionflag, RedemptionPoints, clsDefaultConfiguration.UpdateBillTime, clsDefaultConfiguration.IsMembership, UpdateStockAtStoreLevel:=clsDefaultConfiguration.UpdateStockAtStoreLevel) Then  ''FloatAmt,


                        isCashierPromoSelected = False
                        'If TotalPoints <> 0 Then
                        '    If objCM.SaveClpData(dsTemp, clsAdmin.CLPProgram, CLPCustomerId, TotalPoints, RedemptionPoints) = False Then  ''CustomerBalancePoint

                        '        ShowMessage(getValueByKey("CM049"), "CM049 - " & getValueByKey("CLAE04"))
                        '    Else
                        '        '  SendSMS(clpCustomerMobileNo, "Dear Customer You have earn " & TotalPoints & " Points on your current shopping. Thank you for shopping with us.")
                        '    End If
                        'End If
                        'End If
                        Dim TotalPay As Double = Val(txtAdvFinalCost.Text)

                        '''Code ln 3330 Start Added BY Mahesh In case of AllowBillPrintForCreditSales false(Naturals) & Payment by Credit Sale for Home Delivery bill Print not required , instead a pop up Message “Cash Memo Created successfully ” will shown .
                        If Not clsDefaultConfiguration.AllowBillPrintForCreditSales _
                                AndAlso dsTemp.Tables("CashMemoReceipt").Rows.Count = 1 Then _
                               ' AndAlso CustomerSaleType = enumCustomerSaleType.Home_Delivery Then
                            '------Check payment only from Credit Sale ---
                            Dim dr() = dsTemp.Tables("CashMemoReceipt").Select("TenderTypeCode='Credit'")
                            If dr.Count > 0 Then
                                ShowMessage(getValueByKey("CM072"), "CM072 - " & getValueByKey("CLAE04"))
                                '  ClearData()
                                clsCashMemo.dsCashMemoPrinting = Nothing
                                ' cmdNew_Click(sender, e)
                                Return True
                            End If
                        End If


                        Dim objPrint As New clsCashMemoPrint(billNo, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
                        objPrint.DisplayArticleFullName = clsDefaultConfiguration.PrintItemFullName
                        objPrint.AllowDecimalQty = clsDefaultConfiguration.AllowDecimalQty
                        objPrint.WeightScaleEnabled = clsDefaultConfiguration.WeightScaleEnabled
                        objPrint.KOTBillPrintingRequired = clsDefaultConfiguration.KOTPrintRequired
                        objPrint.CustomerNameRequiredInKOT = clsDefaultConfiguration.CustomerNameRequiredInKOT
                        objPrint.TokenNoRequiredInKOT = clsDefaultConfiguration.TokenNoRequiredInKOT
                        objPrint.CashMemoResetonDayClose = clsDefaultConfiguration.CashMemoResetonDayClose
                        objPrint.AllowBillingOnlyAfterSelectionOfSalesType = clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType
                        objPrint.PrintFormatNo = clsDefaultConfiguration.PrintFormatNo
                        objPrint.KOTPrintEachlineItem = clsDefaultConfiguration.IsKOTPrintEachlineItem
                        objPrint.KOTPrintForEachQuantity = clsDefaultConfiguration.IsKOTPrintQuantityWise
                        objPrint.IsKotFontLarge = clsDefaultConfiguration.IsKotFontLarge
                        objPrint.IsKotFontBold = clsDefaultConfiguration.IsKotFontBold
                        objPrint.KOTPrintFormatNo = clsDefaultConfiguration.KOTPrintFormatNo
                        objPrint.MettlerConnString = clsDefaultConfiguration.MettlerConnString
                        objPrint.RoundOff = clsDefaultConfiguration.BillRoundOffAt
                        objPrint.IsDintInEnabled = clsDefaultConfiguration.DineInProcess
                        'Select Case CustomerSaleType
                        '    Case enumCustomerSaleType.Dine_In
                        '        objPrint.CustomerSaleType = "Dine In"
                        '    Case enumCustomerSaleType.Home_Delivery
                        '        objPrint.CustomerSaleType = "Home Delivery"
                        '    Case enumCustomerSaleType.Take_Away
                        '        objPrint.CustomerSaleType = "Take Away"
                        '    Case Else

                        'End Select

                        'objPrint.CompositeTaxReqOnPrint = clsDefaultConfiguration.CompositeTaxReqOnPrint
                        'objPrint.TaxDetailsRequired = clsDefaultConfiguration.PrintingTaxInfo

                        '    objPrint.DeliveryPersonName = deliveryPersonName
                        Dim Errormsg As String = ""

                        'Rakesh:09-July-2013-->Start: Template based cashmemo bill printing
                        If (clsDefaultConfiguration.TemplatePrintingAllowed) Then
                            objPrint.PrintTemplateCashMemoBillDetails(billNo, clsAdmin.SiteCode, clsAdmin.CurrencyCode, String.Empty)
                        Else
                            If clsDefaultConfiguration.IsMembership Then
                                ' If Not String.IsNullOrEmpty(Membershipid) Then
                                ' CashMemoPrintforMemberShip(billNo, clsDefaultConfiguration.ClientName, clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMS", "", "", "", Errormsg, GiftMsg, _billAmt, _paidAmt, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy)
                                'Else
                                objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMS", "", "", "", Errormsg, "", _billAmt, _paidAmt, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges)
                                '  End If
                            Else
                                objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMS", "", "", "", Errormsg, "", _billAmt, _paidAmt, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges)

                            End If

                        End If
                        'Rakesh:09-July-2013-->End: Template based cashmemo bill printing
                        'If clsDefaultConfiguration.UpdateStockAtStoreLevel Then
                        '    If Not objCM.UpdateStockForArticle(clsAdmin.SiteCode, clsAdmin.UserCode, clsDefaultConfiguration.StockStorageLocation, billNo, clsAdmin.Financialyear, dsTemp, dsCashMemoComboDtl) Then
                        '        ActivityLogForShift(Nothing, "Update Stock Fail", "")
                        '    End If
                        'End If
                        dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Clear()
                        '  comboArticleCopy.Rows.Clear()
                        'Transfer data to kds tables


                        Dim obj As New clsAuthorization
                        Dim DtKdsTran As DataTable = obj.GetSitedAllowedTran(clsAdmin.SiteCode, "KDS")
                        If Not DtKdsTran Is Nothing AndAlso DtKdsTran.Rows.Count > 0 Then
                            objCM.TransferKdsData(billNo, clsAdmin.SiteCode)
                        End If

                        _remarks = String.Empty
                        If Errormsg <> String.Empty Then
                            ShowMessage(Errormsg, getValueByKey("CLAE05"))
                            Try
                                Throw New Exception(Errormsg & "line No 3070 cmd_savePrint_click for print")
                            Catch ex As Exception
                                LogException(ex)
                            End Try
                        End If

                        Dim clsVoucher As New clsPrintVoucher
                        If Not dtTempGv Is Nothing AndAlso dtTempGv.Rows.Count > 0 Then
                            Dim dv As New DataView(dtTempGv, "", "VOURCHERSERIALNBR", DataViewRowState.CurrentRows)
                            If dv.Count > 0 Then

                                For Each dr As DataRowView In dv
                                    SendSMS(clpCustomerMobileNo, "E-Voucher of Couture Value Rs." & dr("ValueOfVoucher") & ". Voucher No : " & dr("VOURCHERSERIALNBR"))
                                    'objPrint.PrintGiftVoucher(dr("VOURCHERSERIALNBR").ToString(), dr("ValueOfVoucher").ToString(), CDate(IIf(dr("ExpiryDate") Is DBNull.Value, Now, dr("ExpiryDate"))), DateDiff(DateInterval.Day, dr("ExpiryDate"), dr("issuedondate")))
                                    clsVoucher.PrintGiftVoucherAndCreditNote("CMS", clsAdmin.SiteCode, "GiftVoucher", dr("VOURCHERSERIALNBR"), dr("ValueOfVoucher"), CDate(IIf(dr("ExpiryDate") Is DBNull.Value, Now, dr("ExpiryDate"))), clsAdmin.UserName, dr("IssuedDocNumber"), clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                                Next
                                ' SendSMS(clpCustomerMobileNo, "Your balance points are " & CustomerBalancePoint)
                            End If
                        End If

                        If (dsTemp.Tables("CashMemoReceipt") IsNot Nothing) Then
                            For Each dr As DataRow In dsTemp.Tables("CashMemoReceipt").Select("TenderTypeCode='CreditVouc(I)'", "", DataViewRowState.CurrentRows)
                                TotalPay = IIf(dr("AmountTendered") > 0, dr("AmountTendered"), dr("AmountTendered") * -1)
                                'objPrint.PrintVoucher("CMS", TotalPay, clsDefaultConfiguration.VoucherText, clsAdmin.SiteCode, Errormsg, clsAdmin.CurrencyCode)
                                clsVoucher.PrintGiftVoucherAndCreditNote("CMS", clsAdmin.SiteCode, "CreditNote", String.Empty, TotalPay, String.Empty, clsAdmin.UserName, billNo, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                            Next
                        End If

                        Dim objPromo As New clsApplyPromotion
                        Dim PromoText As String = ""
                        objPromo.CheckForPromotionList(clsAdmin.SiteCode, TotalPay, PromoText, 1)
                        If PromoText <> String.Empty Then
                            fnPrint(PromoText, "PRN")
                        End If
                        If clsDefaultConfiguration.CLPRegistration = True AndAlso TotalPay >= clsDefaultConfiguration.CLPRegistrationAmt Then
                            ShowMessage(getValueByKey("CM023"), "CM023 - " & getValueByKey("CLAE04"))
                            'ShowMessage("Customer is Eligible For CLP Registration", "Information")
                        End If

                        clsCashMemo.dsCashMemoPrinting = Nothing
                        'cmdNew_Click(sender, e)

                        Return True
                    Else
                        ShowMessage(StrError, getValueByKey("CLAE05"))
                        Try
                            Throw New Exception(StrError & "line No 3070 cmd_savePrint_click")
                        Catch ex As Exception
                            LogException(ex)
                        End Try

                        Return False
                    End If
                    ' Else
                    Return False
                End If
            End If
            ' Else
            Return False
            ' End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CM024"), "CM024 - " & getValueByKey("CLAE05"))
            LogException(ex)
            Return False
            'ShowMessage("Error in Saving the Details", "Save Error")
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Function

#End Region
#End Region
#Region "-----------------------------Host Page  "

#Region "--Events"
    Private Sub btnNextTab_Click(sender As Object, e As EventArgs) Handles btnNextTab.Click
        Try
            '------------------resevation promotion
            If tpCreateDetails.IsSelected Then
                If String.IsNullOrEmpty(cmdChkInDate.Text) Or IsDBNull(cmdChkInDate.Text) Then
                    ShowMessage(" Please select check in Date", "Information")
                    cmdChkInDate.Focus()
                    Exit Sub
                End If
                If String.IsNullOrEmpty(cmdCheckoutDate.Text) Or IsDBNull(cmdCheckoutDate.Text) Then
                    ShowMessage("Please select check out Date", "Information")
                    cmdCheckoutDate.Focus()
                    Exit Sub
                End If
                If Convert.ToDateTime(cmdChkInDate.Value) < clsAdmin.CurrentDate.Date Then
                    ShowMessage(" Check-In date should not be less than current date", "Information")
                    cmdChkInDate.Focus()
                    Exit Sub
                End If
                If cmdChkInDate.Text < Convert.ToDateTime(clsAdmin.CurrentDate) Then
                    ShowMessage("Check-In Date Should not be less than Current Date Time", "Information")
                    cmdChkInDate.Focus()
                    Exit Sub
                End If
                'If CDate(cmdChkInDate.Text) < clsAdmin.CurrentDate Then
                '    ShowMessage(" Check-In date should not be less than current date", "Information")
                '    cmdChkInDate.Focus()
                '    Exit Sub
                'End If
                If Convert.ToDateTime(cmdChkInDate.Text) > Convert.ToDateTime(cmdCheckoutDate.Text) Then
                    ShowMessage("Check-Out Date Should not be less than Check-In Date", "Information")
                    cmdChkInDate.Focus()
                    Exit Sub
                End If
                _chkInDate = cmdChkInDate.Value
                _ChkoutDate = cmdCheckoutDate.Value
                txtCheckedIn.TextDetached = True
                txtCheckedIn.Text = cmdChkInDate.Value
                txtCheckedOut.TextDetached = True
                txtCheckedOut.Text = cmdCheckoutDate.Value
                _selectedRoomNumber = ""
                _selectedRoomTypeId = ""
                Dim numberOfRecords As Integer = dtReserv.Select("Selects = True").Length
                If numberOfRecords > 0 Then
                    For i = 1 To grdReservation.Rows.Count - 1
                        If grdReservation.Rows(i)("Selects") = True Then
                            _selectedRoomNumber = _selectedRoomNumber + "," + grdReservation.Rows(i)("RoomNumber").ToString()
                            '  _selectedRoomTypeId = _selectedRoomTypeId + ",'" + grdReservation.Rows(i)("RoomTypeId").ToString() + "'"
                            _selectedRoomTypeId = _selectedRoomTypeId + "," + grdReservation.Rows(i)("RoomTypeId").ToString()
                            '+ IIf(grdReservation.Rows(i)("RoomTypeId").ToString(), String.Empty, grdReservation.Rows(i)("RoomTypeId").ToString())
                        End If
                    Next
                    _selectedRoomNumber = (_selectedRoomNumber.Remove(0, 1))
                    _selectedRoomTypeId = (_selectedRoomTypeId.Remove(0, 1))
                Else
                    ShowMessage("Please select atleast one room", "Information")
                    Exit Sub
                End If
                dtRoomTypeWisePromotions.Clear()
                dtRoomTypeWisePromotions = objHotelreservation.GetRoomTypeWisePromotions(clsAdmin.SiteCode, _chkInDate.Date, clsAdmin.CurrentDate.Year, _selectedRoomTypeId) '_selectedRoomTypeId
                HideTableLayoutPanle(False)
                BindPromotionsToCombo(dtRoomTypeWisePromotions)
                'If dtPromotionTotalDiscountValues.Rows.Count > 0 Then

                '    Dim vl = dtPromotionTotalDiscountValues.Compute("Sum(TotalPromotionCost)", "")
                '    If vl.ToString() <> "" Then
                '        PromotionAmountDetails(SelectedRoomsCost, vl)
                '    Else
                '        PromotionAmountDetails(SelectedRoomsCost, "0")
                '    End If
                'End If
                '  BindToChkBxList(dtRoomTypeWisePromotions)
                If dtPromotions.Rows.Count = 0 Then
                    PromotionAmountDetails(SelectedRoomsCost, "0")
                End If
                tpCreateDetails.Enabled = False
                tpPromotionsDetails.Enabled = True
                tpPromotionsDetails.Show()
                Exit Sub
            End If
            '------------------------------ promotion guest
            If tpPromotionsDetails.IsSelected Then
                Dim DtFilter = dtRoomTypeWisePromotions.DefaultView.ToTable(True, "mstRoomTypeId")
                Promotion(CtrllblRoomTypeName1.Tag, CtrllblPromoAppliedValue1.Tag)
                Promotion(CtrllblRoomTypeName2.Tag, CtrllblPromoAppliedValue2.Tag)
                Promotion(CtrllblRoomTypeName3.Tag, CtrllblPromoAppliedValue3.Tag)
                Promotion(CtrllblRoomTypeName4.Tag, CtrllblPromoAppliedValue4.Tag)
                Promotion(CtrllblRoomTypeName5.Tag, CtrllblPromoAppliedValue5.Tag)
                Promotion(CtrllblRoomTypeName6.Tag, CtrllblPromoAppliedValue6.Tag)
                Promotion(CtrllblRoomTypeName7.Tag, CtrllblPromoAppliedValue7.Tag)
                Promotion(CtrllblRoomTypeName8.Tag, CtrllblPromoAppliedValue8.Tag)
                Promotion(CtrllblRoomTypeName9.Tag, CtrllblPromoAppliedValue9.Tag)
                Promotion(CtrllblRoomTypeName10.Tag, CtrllblPromoAppliedValue10.Tag)

                tpPromotionsDetails.Enabled = False
                tpGuestDetails.Enabled = True
                tpGuestDetails.Show()
                Exit Sub
            End If
            '-----------------------------guest sumary
            If tpGuestDetails.IsSelected Then
                If dtGuest.Rows.Count > 0 Then
                    If dtGuest.Select("PrimaryGuest").Count > 0 Then
                        tpGuestDetails.Enabled = False
                        tpSummaryDetails.Enabled = True
                        tpSummaryDetails.Show()
                        'LoadSummaryInformationIntoGrid(dtRoomTypeWisePromotions)
                        LoadSummaryInformationIntoGrid(dtPromotions)
                    Else
                        ShowMessage("Please fill primary guest details", "Information")
                    End If
                Else
                    ShowMessage("Please fill guest details", "Information")
                End If
                Exit Sub
            End If
            '---------------------------- summary payment
            If tpSummaryDetails.IsSelected Then
                loadPaymentDetails()
                tpSummaryDetails.Enabled = False
                tpPaymentsDetails.Enabled = True
                tpPaymentsDetails.Show()

                btnSaveAndFinish.Enabled = True
                btnSaveAndCheckIn.Enabled = True
                btnNextTab.Enabled = False

                btnSaveAndFinish.BackColor = Color.FromArgb(0, 113, 188)
                Me.btnSaveAndFinish.Font = New Font("Callibri", 7, FontStyle.Bold)
                btnSaveAndCheckIn.BackColor = Color.FromArgb(0, 113, 188)
                Me.btnSaveAndCheckIn.Font = New Font("Callibri", 7, FontStyle.Bold)
                btnNextTab.BackColor = Color.FromArgb(208, 208, 208)


                If tpPaymentsDetails.IsSelected Then
                    tpAdvancePayment.Show()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    'added by khusrao adil on 29-03-2017 for display default values promotion
    Public Function DisplayPromotionValues(ByRef CtrlComboPromotion As ctrlCombo, ByRef CtrllblPromoAppliedValue As CtrlLabel, ByVal strSel As Object)
        Try
            CtrllblPromoAppliedValue.Tag = strSel(0)("mstPromotionId").ToString()
            CtrlComboPromotion.Tag = strSel(0)("mstPromotionId").ToString()
            CtrlComboPromotion.Text = strSel(0)("PromotionName").ToString()
            CtrllblPromoAppliedValue.Text = CtrlComboPromotion.Text
            Return 0
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 09-03-2017 for promotions combo boxes
    Public Function BindPromotionsToCombo(ByVal dtpromo As DataTable)
        Try
            Dim DtFilter = dtpromo.DefaultView.ToTable(True, "mstRoomTypeId")
            Dim drRowCount = DtFilter.Rows.Count  ' 2
            If DtFilter.Rows.Count > 2 Then
                FlowLayoutPanelHeaderHolder.AutoScroll = True
            Else
                FlowLayoutPanelHeaderHolder.AutoScroll = False
            End If
            For index = 0 To DtFilter.Rows.Count - 1
                Dim DtComobo As DataTable = dtpromo.Select("mstRoomTypeId='" + DtFilter.Rows(index)("mstRoomTypeId").ToString() + "'").CopyToDataTable
                Dim DimComboFilter As DataTable = DtComobo.DefaultView.ToTable(True, "mstPromotionId", "PromotionName").Copy()
                Dim RoomNumberList = dtRoomTypeRoomsCount.Select("mstRoomTypeId='" + DtFilter.Rows(index)("mstRoomTypeId").ToString() + "'")
                Dim tempRoom As String = RoomNumberList(0)("RoomNumberList").ToString() '(_selectedRoomNumber.Remove(0, 1))
                Dim tempPromoAppliedCount As String = RoomNumberList(0)("PromotionCount").ToString()
                Dim answer As Char
                answer = tempRoom.Substring(tempRoom.Length - 1, 1)
                If answer = "," Then
                    tempRoom = (tempRoom.Remove(tempRoom.Length - 1, 1))
                End If
                Dim xdtRoomTypeIdWise = dtpromo.Select("mstRoomTypeId='" + DtFilter.Rows(index)("mstRoomTypeId").ToString() + "'").ToList()  '7
                Dim row As DataRow
                row = DimComboFilter.NewRow()
                row("MstPromotionId") = "0000"
                row("PromotionName") = "No Promotion"
                DimComboFilter.Rows.Add(row)
                'Dim dataView As New DataView(DimComboFilter)
                'dataView.Sort = "MstPromotionId Asc"
                'DimComboFilter=DimComboFilter.DefaultView.Sort(
                If index = 0 Then
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx1, True)
                    BindComboBoxes(DimComboFilter, CtrlComboPromotion1)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue1.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue1.Text = d("rate").ToString + " INR"
                            CtrllblRoomTypeName1.Text = d("RoomTypeName")
                            CtrllblRoomTypeName1.Tag = d("mstRoomTypeId")
                            CtrllblRoomNumber1.Text = tempRoom
                            If dtPromotions.Rows.Count > 0 Then
                                Dim dtDefaultPromo = dtPromotions.Select("roomTypeId='" + d("mstRoomTypeId").ToString() + "'")
                                If dtDefaultPromo.Length <> 0 Then
                                    Dim mstPromotionId As String = dtDefaultPromo(0)("mstPromotionId")
                                    Dim strSel = DimComboFilter.Select("MstPromotionId='" + mstPromotionId + "'")
                                    DisplayPromotionValues(CtrlComboPromotion1, CtrllblPromoAppliedValue1, strSel)
                                Else
                                    CtrllblPromoAppliedValue1.Text = "0"
                                    CtrllblPromoAppliedValue1.Tag = ""
                                End If
                            End If
                        End If
                        counter = counter + 1
                    Next
                ElseIf index = 1 Then
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx2, True)
                    BindComboBoxes(DimComboFilter, CtrlComboPromotion2)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue2.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue2.Text = d("rate").ToString + " INR"
                            CtrllblRoomTypeName2.Text = d("RoomTypeName")
                            CtrllblRoomTypeName2.Tag = d("mstRoomTypeId")
                            CtrllblRoomNumber2.Text = tempRoom
                            If dtPromotions.Rows.Count > 0 Then
                                Dim dtDefaultPromo = dtPromotions.Select("roomTypeId='" + d("mstRoomTypeId").ToString() + "'")
                                If dtDefaultPromo.Length <> 0 Then
                                    Dim mstPromotionId As String = dtDefaultPromo(0)("mstPromotionId")
                                    Dim strSel = DimComboFilter.Select("MstPromotionId='" + mstPromotionId + "'")
                                    DisplayPromotionValues(CtrlComboPromotion2, CtrllblPromoAppliedValue2, strSel)
                                End If
                            End If
                        End If
                        counter = counter + 1
                    Next
                ElseIf index = 2 Then
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx3, True)
                    BindComboBoxes(DimComboFilter, CtrlComboPromotion3)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue3.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue3.Text = d("rate").ToString + " INR"
                            CtrllblRoomTypeName3.Text = d("RoomTypeName")
                            CtrllblRoomTypeName3.Tag = d("mstRoomTypeId")
                            CtrllblRoomNumber3.Text = tempRoom

                            Dim dtDefaultPromo = dtPromotions.Select("roomTypeId='" + d("mstRoomTypeId").ToString() + "'")
                            If dtDefaultPromo.Length <> 0 Then
                                Dim mstPromotionId As String = dtDefaultPromo(0)("mstPromotionId")
                                Dim strSel = DimComboFilter.Select("MstPromotionId='" + mstPromotionId + "'")
                                DisplayPromotionValues(CtrlComboPromotion3, CtrllblPromoAppliedValue3, strSel)
                            End If
                        End If
                        counter = counter + 1
                    Next
                ElseIf index = 3 Then
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx4, True)
                    BindComboBoxes(DimComboFilter, CtrlComboPromotion4)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue4.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue4.Text = d("rate").ToString + " INR"
                            CtrllblRoomTypeName4.Text = d("RoomTypeName")
                            CtrllblRoomTypeName4.Tag = d("mstRoomTypeId")
                            CtrllblRoomNumber4.Text = tempRoom

                            Dim dtDefaultPromo = dtPromotions.Select("roomTypeId='" + d("mstRoomTypeId").ToString() + "'")
                            If dtDefaultPromo.Length <> 0 Then
                                Dim mstPromotionId As String = dtDefaultPromo(0)("mstPromotionId")
                                Dim strSel = DimComboFilter.Select("MstPromotionId='" + mstPromotionId + "'")
                                DisplayPromotionValues(CtrlComboPromotion4, CtrllblPromoAppliedValue4, strSel)
                            End If
                        End If
                        counter = counter + 1
                    Next
                ElseIf index = 4 Then
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx5, True)
                    BindComboBoxes(DimComboFilter, CtrlComboPromotion5)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue5.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue5.Text = d("rate").ToString + " INR"
                            CtrllblRoomTypeName5.Text = d("RoomTypeName")
                            CtrllblRoomTypeName5.Tag = d("mstRoomTypeId")
                            CtrllblRoomNumber5.Text = tempRoom

                            Dim dtDefaultPromo = dtPromotions.Select("roomTypeId='" + d("mstRoomTypeId").ToString() + "'")
                            If dtDefaultPromo.Length <> 0 Then
                                Dim mstPromotionId As String = dtDefaultPromo(0)("mstPromotionId")
                                Dim strSel = DimComboFilter.Select("MstPromotionId='" + mstPromotionId + "'")
                                DisplayPromotionValues(CtrlComboPromotion5, CtrllblPromoAppliedValue5, strSel)
                            End If
                        End If
                        counter = counter + 1
                    Next
                ElseIf index = 5 Then
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx6, True)
                    BindComboBoxes(DimComboFilter, CtrlComboPromotion6)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue6.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue6.Text = d("rate").ToString + " INR"
                            CtrllblRoomTypeName6.Text = d("RoomTypeName")
                            CtrllblRoomTypeName6.Tag = d("mstRoomTypeId")
                            CtrllblRoomNumber6.Text = tempRoom

                            Dim dtDefaultPromo = dtPromotions.Select("roomTypeId='" + d("mstRoomTypeId").ToString() + "'")
                            If dtDefaultPromo.Length <> 0 Then
                                Dim mstPromotionId As String = dtDefaultPromo(0)("mstPromotionId")
                                Dim strSel = DimComboFilter.Select("MstPromotionId='" + mstPromotionId + "'")
                                DisplayPromotionValues(CtrlComboPromotion6, CtrllblPromoAppliedValue6, strSel)
                            End If
                        End If
                        counter = counter + 1
                    Next
                ElseIf index = 6 Then
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx7, True)
                    BindComboBoxes(DimComboFilter, CtrlComboPromotion7)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue7.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue7.Text = d("rate").ToString + " INR"
                            CtrllblRoomTypeName7.Text = d("RoomTypeName")
                            CtrllblRoomTypeName7.Tag = d("mstRoomTypeId")
                            CtrllblRoomNumber7.Text = tempRoom

                            Dim dtDefaultPromo = dtPromotions.Select("roomTypeId='" + d("mstRoomTypeId").ToString() + "'")
                            If dtDefaultPromo.Length <> 0 Then
                                Dim mstPromotionId As String = dtDefaultPromo(0)("mstPromotionId")
                                Dim strSel = DimComboFilter.Select("MstPromotionId='" + mstPromotionId + "'")
                                DisplayPromotionValues(CtrlComboPromotion7, CtrllblPromoAppliedValue7, strSel)
                            End If
                        End If
                        counter = counter + 1
                    Next
                ElseIf index = 7 Then
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx8, True)
                    BindComboBoxes(DimComboFilter, CtrlComboPromotion8)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue8.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue8.Text = d("rate").ToString + " INR"
                            CtrllblRoomTypeName8.Text = d("RoomTypeName")
                            CtrllblRoomTypeName8.Tag = d("mstRoomTypeId")
                            CtrllblRoomNumber8.Text = tempRoom

                            Dim dtDefaultPromo = dtPromotions.Select("roomTypeId='" + d("mstRoomTypeId").ToString() + "'")
                            If dtDefaultPromo.Length <> 0 Then
                                Dim mstPromotionId As String = dtDefaultPromo(0)("mstPromotionId")
                                Dim strSel = DimComboFilter.Select("MstPromotionId='" + mstPromotionId + "'")
                                DisplayPromotionValues(CtrlComboPromotion8, CtrllblPromoAppliedValue8, strSel)
                            End If
                        End If
                        counter = counter + 1
                    Next
                ElseIf index = 8 Then
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx9, True)
                    BindComboBoxes(DimComboFilter, CtrlComboPromotion9)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue9.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue9.Text = d("rate").ToString + " INR"
                            CtrllblRoomTypeName9.Text = d("RoomTypeName")
                            CtrllblRoomTypeName9.Tag = d("mstRoomTypeId")
                            CtrllblRoomNumber9.Text = tempRoom

                            Dim dtDefaultPromo = dtPromotions.Select("roomTypeId='" + d("mstRoomTypeId").ToString() + "'")
                            If dtDefaultPromo.Length <> 0 Then
                                Dim mstPromotionId As String = dtDefaultPromo(0)("mstPromotionId")
                                Dim strSel = DimComboFilter.Select("MstPromotionId='" + mstPromotionId + "'")
                                DisplayPromotionValues(CtrlComboPromotion9, CtrllblPromoAppliedValue9, strSel)
                            End If
                        End If
                        counter = counter + 1
                    Next
                ElseIf index = 9 Then
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx6, True)
                    ' BindComboBoxes(DimComboFilter, CtrlComboPromotion10)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue10.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue10.Text = d("rate").ToString + " INR"
                            CtrllblRoomTypeName10.Text = d("RoomTypeName")
                            CtrllblRoomTypeName10.Tag = d("mstRoomTypeId")
                            CtrllblRoomNumber10.Text = tempRoom

                            Dim dtDefaultPromo = dtPromotions.Select("roomTypeId='" + d("mstRoomTypeId").ToString() + "'")
                            If dtDefaultPromo.Length <> 0 Then
                                Dim mstPromotionId As String = dtDefaultPromo(0)("mstPromotionId")
                                Dim strSel = DimComboFilter.Select("MstPromotionId='" + mstPromotionId + "'")
                                DisplayPromotionValues(CtrlComboPromotion10, CtrllblPromoAppliedValue10, strSel)
                            End If
                        End If
                        counter = counter + 1
                    Next
                End If
            Next
            If DtFilter.Rows.Count = 0 Then
                If dtPromotions.Rows.Count > 0 Then
                    dtPromotions.Rows.Clear()
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 21-02-2017 for promotion fiexed value

    Public Sub CheckedChangeCheckBox(ByRef chk As CheckBox, ByVal condition As Boolean)
        chk.Checked = condition
    End Sub
    'added by khusrao adil on 21-02-2017 from promotion check boxex visiblity
    Public Sub VisibleCheckBox(ByRef chk As CheckBox, ByVal condition As Boolean)
        chk.Visible = condition
    End Sub
    'added by khusrao adil on 22-02-2017 from promotion table layout pnl visiblity
    Public Sub VisibleTableLayoutPanel(ByRef TblPnl As TableLayoutPanel, ByVal condition As Boolean)
        TblPnl.Visible = condition
        TblPnl.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset
    End Sub
    Public Sub HideTableLayoutPanle(ByVal condition As Boolean)
        TblLayoutPnlChkBx1.Visible = condition
        TblLayoutPnlChkBx2.Visible = condition
        TblLayoutPnlChkBx4.Visible = condition
        TblLayoutPnlChkBx4.Visible = condition
        TblLayoutPnlChkBx5.Visible = condition
        TblLayoutPnlChkBx6.Visible = condition
        TblLayoutPnlChkBx7.Visible = condition
        TblLayoutPnlChkBx8.Visible = condition
        TblLayoutPnlChkBx9.Visible = condition
        TblLayoutPnlChkBx10.Visible = condition
    End Sub

    Private Sub btnPreviousTab_Click(sender As Object, e As EventArgs) Handles btnPreviousTab.Click
        If tpCreateDetails.IsSelected Then
            'btnPreviousTab.Visible = False
        End If
        If tpPromotionsDetails.IsSelected Then
            tpCreateDetails.Enabled = True
            tpPromotionsDetails.Enabled = False
            tpCreateDetails.Show()
            ' btnPreviousTab.Visible = False
        End If
        If tpGuestDetails.IsSelected Then
            tpGuestDetails.Enabled = False
            tpPromotionsDetails.Enabled = True
            tpPromotionsDetails.Show()
            ' btnPreviousTab.Visible = False
        End If
        If tpSummaryDetails.IsSelected Then
            tpGuestDetails.Enabled = True
            tpSummaryDetails.Enabled = False
            tpGuestDetails.Show()
            ' btnPreviousTab.Visible = False
        End If
        If tpPaymentsDetails.IsSelected Then
            tpSummaryDetails.Enabled = True
            tpPaymentsDetails.Enabled = False
            tpSummaryDetails.Show()
            btnSaveAndFinish.Enabled = False
            btnSaveAndCheckIn.Enabled = False
            btnNextTab.Enabled = True
            Me.btnNextTab.BackColor = Color.FromArgb(0, 113, 180)
            Me.btnSaveAndFinish.BackColor = Color.FromArgb(208, 208, 208)
            Me.btnSaveAndCheckIn.BackColor = Color.FromArgb(208, 208, 208)
            gridSummaryDetailsSetting()
        End If
    End Sub

    Private Sub tabReservation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabReservation.SelectedIndexChanged
        If tpPromotionsDetails.IsSelected Then
            btnPreviousTab.Visible = True
        End If
        If tpCreateDetails.IsSelected Then
            btnPreviousTab.Visible = False
        End If
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        If MsgBox(" If you cancel the reservation, you will lose unsaved data. Do you want to continue?", MsgBoxStyle.YesNo, "Information") = MsgBoxResult.Yes Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Else
            Me.DialogResult = Windows.Forms.DialogResult.None
        End If
    End Sub
    'added by khusrao adil on 02-02-2017
    Private Sub btnSaveAndFinish_Click(sender As Object, e As EventArgs) Handles btnSaveAndFinish.Click
        isCheckIn = False
        PrepareDataSave()
        If dsMainHost.Tables("Host_ReservationGuestDetail").Rows.Count = 0 Then
            ShowMessage("please go back and enter the guest details before check-in", "Information")
            Exit Sub
        End If
        isCheckIn = False
        ReservationId = dsMainHost.Tables("Host_Reservation").Rows(0)("reservationID").ToString()
        ReservationRNumber = dsMainHost.Tables("Host_Reservation").Rows(0)("reservationNumber").ToString()
        'Dim disStatusMgs = "Reservation Saved Successfully for"  'for reservation
        '"Check-In Successfull for"  ' for check in
        'ReservationRNumber = "RN0010000000020"
        'ReservationGuestName = "Khusrao Khan"
        Dim isPrimaryGuest As String = dsMainHost.Tables("Host_ReservationGuestDetail").Rows(0)("isPrimaryGuest").ToString()
        Dim dtdetail As DataTable = dsMainHost.Tables("Host_ReservationGuestDetail").Copy
        For Each dr As DataRow In dtdetail.Rows
            If isPrimaryGuest = True Then
                ' dsMainHost.Tables("Host_Reservation").Rows(0)("primaryGuestDocumentNumber") = dr("supportedDocumentNumber_1")

                ' ReservationGuestName = dr("guestFirstName").ToString() + " " + dr("guestMiddleName").ToString() + " " + dr("guestLastName").ToString()
                ReservationGuestName = dsMainHost.Tables("Host_ReservationGuestDetail").Rows(0)("guestFirstName").ToString() & vbCrLf & dsMainHost.Tables("Host_ReservationGuestDetail").Rows(0)("guestMiddleName").ToString() & vbCrLf & dsMainHost.Tables("Host_ReservationGuestDetail").Rows(0)("guestLastName").ToString()
                'ReservationDate = System.DateTime.Now.ToString()
            End If
        Next
        ReservationDate = System.DateTime.Now.ToString("dd/MM/yyyy")
        If SaveAndPrintReservation(True) = True Then
            If objcomm.updateGLNO(documentNo) Then
                ShowMessage("Saved Successfully for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
                dtGuest.Clear()
                dtReservationTaxMap.Clear()
                dtPromotions.Clear()
                dtReservationReceipt.Clear()
                dtHotelAllTaxes.Clear()
                dtPromotionTotalDiscountValues.Clear()
                dtReservation.Rows.Clear()

                tpCreateDetails.Enabled = True
                tpCreateDetails.Show()
                frmHostCreateReservation_Load(sender, e)

                btnPreviousTab.Visible = False
                btnSaveAndFinish.Enabled = False
                btnSaveAndCheckIn.Enabled = False
                btnNextTab.Enabled = True
                cmdCheckoutDate.Value = DateTime.Now
                cmdChkInDate.Value = DateTime.Now
            End If
        Else
            ShowMessage("Save Failed for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
        End If

        '  Me.Close()
    End Sub
#End Region
    '"--end Events"
#Region "--Functions"
    'added by khusrao adil on  02-02-2017  for schema 
    Public Sub getHOSTBinding()
        Try
            dsMainHost = objHotelreservation.HOSTGetStruc()
        Catch ex As Exception

        End Try
    End Sub

    'added by khusrao adil on  02-02-2017 for saving reservation
    Public Sub PrepareDataSave()
        Try
            getHOSTBinding()
            '------------------------calucalte tax
            CalculateTax()
            '------------------------
            If PrepareReservationCreateData(dsMainHost) Then   ' reservation  table
                Exit Sub
            ElseIf PrepareRoomMapData(dtSummary, dsMainHost) Then       'room map table
                Exit Sub

            ElseIf PrepareTaxData(dtReservationTaxMap, dsMainHost) Then  'room tax map table
                Exit Sub
            ElseIf PreparePromotionData(dtPromotions, dsMainHost) Then  ' room map prmotions table
                Exit Sub
            ElseIf PrepareCustomersDetailsData(dtGuest, dsMainHost) Then  'clpcustomer table
                Exit Sub
            ElseIf PrepareGuestDetailData(dtGuest, dsMainHost) Then  'guest table
                Exit Sub
            End If
            'service
            'If objHotelreservation.SaveReservationAllData(dsMainHost) Then
            '    Dim Name As String = txtGuestFirstName.Text & " " & txtGuestMiddleName.Text & " " & txtGuestLastName.Text
            '    ShowMessage("Reservation save successfully for" & vbCrLf & " Guest Name :" & " " & Name & vbCrLf & "Res Id :" & " " & _reservationNumber & vbCrLf & "Date :" & _chkInDate.Date & vbCrLf, "Reservation Details")
            'Else
            '    ShowMessage("Reservation save failed ", "Reservation Details")
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try


    End Sub
    'added by khusrao adil on 10-02-2017 for tax calclutaion
    Public Sub CalculateTax()
        dtReservationTaxMap.Clear()
        Dim _totalCostAmount As Decimal
        Dim _totalTaxAmount As Decimal
        Dim _TaxAmount As Decimal
        For Each dr In dtHotelAllTaxes.Rows
            Dim drTax As DataRow
            drTax = dtReservationTaxMap.NewRow
            drTax("SiteCode") = dr("SiteCode")
            drTax("TaxLineNo") = dr("TaxLineNo")
            drTax("DocumentType") = dr("DocumentType")
            drTax("TaxCode") = dr("TaxCode")
            drTax("TaxValueInPercent") = dr("Tax")
            _TaxAmount = dr("Tax")
            _totalCostAmount = Convert.ToDecimal(txtCTotalCostAount.Text)
            _totalTaxAmount = (_totalCostAmount / 100) * _TaxAmount
            drTax("TaxValueInAmount") = _totalTaxAmount
            drTax("TaxValueInAmount") = _totalTaxAmount
            dtReservationTaxMap.Rows.Add(drTax)
        Next
    End Sub
    'added by khusrao adil on 15-02-2017 for final save in db and print report
    'if SaveWithoutPayment is true then reservation save without payment details
    Public Function SaveAndPrintReservation(Optional ByVal SaveWithoutPayment As Boolean = False) As Boolean
        Try
            If objHotelreservation.SaveReservationAllData(dsMainHost, SaveWithoutPayment) Then
                'print report 
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
#Region "----------------------------------Data prepare"
    'added by khusrao adil on 02-02-2017
    Public Function PrepareReservationCreateData(ByRef dsMmain As DataSet) As Boolean
        Try
            Dim _totalDicountAmount As Decimal = 0
            Dim _totalNetAmount As Decimal = 0
            Dim _totalGrossAmount As Decimal = 0
            Dim _totalTaxAmount As Decimal = 0
            _totalGrossAmount = Convert.ToDecimal(txtSumFinalCost.Text)
            _totalDicountAmount = Convert.ToDecimal(txtSumDiscountAmount.Text)
            'Dim _ttax = If(dr("tax") IsNot DBNull.Value, 0, dr("tax"))
            _totalTaxAmount = Convert.ToDecimal(txtSumTotalTaxAmount.Text)
            _totalNetAmount = (_totalGrossAmount + _totalTaxAmount) - _totalDicountAmount

            Dim dtTEMP = dsMainHost.Tables("Host_Reservation").Copy()
            MaxId = objHotelreservation.GetMaxId("Host_Reservation", "reservationID")
            dsMainHost.Tables("Host_Reservation").Clear()
            Dim docNo As String = objcomm.getDocumentNo("Reservation Number", clsAdmin.SiteCode)
            Dim otherCharacters = "RN" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3, 3)
            _reservationNumber = GenDocNo(otherCharacters, 15, docNo)
            Dim drReservation As DataRow
            drReservation = dsMainHost.Tables("Host_Reservation").NewRow
            drReservation("reservationID") = MaxId
            drReservation("reservationStatusID") = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RESERVATION_STATUS.ToString(), objHotelreservation.enumStatus.RESERVED.ToString())
            drReservation("mstSupportedDocumentTypeID") = cmdDocumentType.SelectedValue
            drReservation("TerminalID") = clsAdmin.TerminalID
            drReservation("siteCode") = clsAdmin.SiteCode
            drReservation("yearInyyyy") = _chkInDate.Year
            drReservation("reservationNumber") = _reservationNumber
            drReservation("reservationDateTime") = _chkInDate
            drReservation("checkInDateTime") = _chkInDate
            drReservation("checkOutDateTime") = _ChkoutDate
            drReservation("totalDaysOfStay") = noOfDays
            drReservation("totalNightsOfStay") = noOfNight
            drReservation("totalNoOfRooms") = txtCRoomSelected.Text
            drReservation("totalNetAmount") = _totalNetAmount
            drReservation("totalTaxAmount") = txtSumTotalTaxAmount.Text
            drReservation("totalPromotionDiscountAmount") = txtPromoDiscount.Text
            drReservation("totalGrossAmount") = _totalGrossAmount
            'If isCheckIn = True Then
            '    drReservation("totalAmountPaid") = txtSumPaidAmt.Text
            'Else
            '    drReservation("totalAmountPaid") = "0.00"
            'End If
            If isCheckIn = True Then
                drReservation("totalAmountPaid") = txtSumPaidAmt.Text
            Else
                If ISCallFromPayment = True Then
                    drReservation("totalAmountPaid") = txtSumPaymentRemaining.Text
                Else
                    drReservation("totalAmountPaid") = txtSumPaidAmt.Text
                End If
            End If
            'drReservation("totalAmountPaid") = txtSumPaymentRemaining.Text
            ' drReservation("remainingAmountToPay") = txtSumPaymentRemaining.Text
            drReservation("remainingAmountToPay") = _totalGrossAmount - CDbl(drReservation("totalAmountPaid"))
            drReservation("totalAccommodationCharges") = dtReservation.Compute("Sum(Cost)", "")
            drReservation("totalServicesCharges") = "0"
            drReservation("totalFoodCharges") = "0"
            drReservation("primaryGuestDocumentNumber") = "0"
            drReservation("primaryGuestDocumentDetails") = "0"
            drReservation("primaryGuestDocumentImage") = Nothing
            drReservation("totalNoOfAdults") = txtSumNumberOfAdult.Text
            drReservation("totalNoOfChildren") = txtSumChildren.Text
            drReservation("remarks1") = "0"
            drReservation("remarks2") = "0"

            drReservation("CreatedOn") = clsAdmin.CurrentDate
            drReservation("CreatedAt") = clsAdmin.SiteCode
            drReservation("CreatedBy") = clsAdmin.UserName
            drReservation("UpdatedOn") = clsAdmin.CurrentDate
            drReservation("UpdatedAt") = clsAdmin.SiteCode
            drReservation("UpdatedBy") = clsAdmin.UserName
            drReservation("mstStatusID") = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RECORD_STATUS.ToString(), objHotelreservation.enumStatus.ACTIVE.ToString())
            dsMmain.Tables("Host_Reservation").Rows.Add(drReservation)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 03-02-2017
    Public Function PrepareRoomMapData(ByVal dtRoomMap As DataTable, ByRef dsMainHost As DataSet) As Boolean
        Try
            Dim _rmDicountAmount As Decimal = 0
            Dim _rmNetAmount As Decimal = 0
            Dim _rmGrossAmount As Decimal = 0
            Dim _rmTaxAmount As Decimal = 0
            MaxId = objHotelreservation.GetMaxId("Host_ReservationRoomMap", "reservationRoomMapID")
            dsMainHost.Tables("Host_ReservationRoomMap").Clear()
            Dim dtTEMP = dsMainHost.Tables("Host_ReservationRoomMap").Copy()
            For Each dr In dtRoomMap.Rows
                _rmGrossAmount = Convert.ToDecimal(dr("Price"))
                Dim _PerDiscountAmount As String = dr("Discount").ToString()
                If _PerDiscountAmount.Contains("%") Then
                    _PerDiscountAmount = _PerDiscountAmount.Replace("%", "")
                End If
                Dim _Pertax As String = dr("tax").ToString() 'If(dr("tax") IsNot DBNull.Value, 0, dr("tax"))
                If _Pertax.Contains("%") Then
                    _Pertax = _Pertax.Replace("%", "")
                End If
                _rmDicountAmount = Convert.ToDecimal(_PerDiscountAmount)
                Dim _ttax = _Pertax
                _rmTaxAmount = Convert.ToDecimal(_ttax)
                _rmNetAmount = (_rmGrossAmount + _rmTaxAmount) - _rmDicountAmount
                Dim drRoomMap As DataRow
                drRoomMap = dsMainHost.Tables("Host_ReservationRoomMap").NewRow
                drRoomMap("reservationRoomMapID") = MaxId
                drRoomMap("ReservationId") = dsMainHost.Tables("Host_Reservation").Rows(0)("ReservationId")
                'Code is added by irfan for hotel reservation on 04/04/2018
                Dim roomid As String = dr("mstRoomID")
                Dim roomrateid As String
                Dim checkin As Date = Convert.ToDateTime(txtCheckedIn.Text)
                roomrateid = objcomm.GetRoomRateId(roomid, checkin)
                '==============================================
                drRoomMap("mstRoomID") = dr("mstRoomID")
                drRoomMap("mstStandardRoomRateID") = roomrateid
                drRoomMap("rateDate") = _chkInDate.Date
                drRoomMap("yearInyyyy") = _chkInDate.Year
                drRoomMap("weekDay") = _chkInDate.DayOfWeek
                drRoomMap("rate") = dr("Price")
                drRoomMap("totalTaxInPercent") = "0" 'FormatPercent(_rmTaxAmount.t)
                drRoomMap("totalTaxInAmount") = _rmTaxAmount 'amount
                drRoomMap("totalPromotionDiscountInPercent") = "0" 'FormatPercent(_rmDicountAmount.ToString())
                drRoomMap("totalPromotionDiscountInAmount") = _rmDicountAmount
                drRoomMap("totalNetAmount") = _rmNetAmount
                drRoomMap("remarks") = "rmk " ' dr("remarks")
                drRoomMap("totalGrossAmount") = _rmGrossAmount
                drRoomMap("CreatedOn") = DateTime.Now
                drRoomMap("CreatedAt") = clsAdmin.SiteCode
                drRoomMap("CreatedBy") = clsAdmin.UserName
                drRoomMap("UpdatedOn") = DateTime.Now
                drRoomMap("UpdatedAt") = clsAdmin.SiteCode
                drRoomMap("UpdatedBy") = clsAdmin.UserName
                drRoomMap("mstStatusID") = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RECORD_STATUS.ToString(), objHotelreservation.enumStatus.ACTIVE.ToString())
                dsMainHost.Tables("Host_ReservationRoomMap").Rows.Add(drRoomMap)
                MaxId = MaxId + 1
            Next
            MaxId = 0
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 06-02-2017
    Public Function PreparePromotionData(ByVal dtRoomPromoMap As DataTable, ByRef dsMainHost As DataSet) As Boolean
        Try
            MaxId = objHotelreservation.GetMaxId("Host_ReservationRoomTypePromotionMap", "reservationRoomTypePromotionMapID")
            dsMainHost.Tables("Host_ReservationRoomTypePromotionMap").Clear()
            Dim dtTEMP = dsMainHost.Tables("Host_ReservationRoomTypePromotionMap").Copy()
            Dim dstemp = dsMainHost.Copy
            For Each dr In dtRoomPromoMap.Rows
                Dim droomPromoMap As DataRow
                droomPromoMap = dsMainHost.Tables("Host_ReservationRoomTypePromotionMap").NewRow
                droomPromoMap("reservationRoomTypePromotionMapID") = MaxId
                droomPromoMap("ReservationId") = dsMainHost.Tables("Host_Reservation").Rows(0)("ReservationId")
                droomPromoMap("rateDate") = _chkInDate.Date
                droomPromoMap("mstPromotionID") = dr("mstPromotionID")
                droomPromoMap("promotionName") = dr("mstPromotionName")
                droomPromoMap("discountInPercent") = dr("discountInPercent")
                droomPromoMap("discountAmount") = dr("discountAmount")
                droomPromoMap("mstSiteRoomTypeMap") = dr("mstSiteRoomTypeMap")
                droomPromoMap("yearInyyyy") = _chkInDate.Date.Year.ToString()
                droomPromoMap("weekDay") = _chkInDate.Date.DayOfWeek
                droomPromoMap("applicableWithOtherPromotions") = 0 'dr("applicableWithOtherPromotions")
                droomPromoMap("description") = dr("description")
                droomPromoMap("CreatedOn") = DateTime.Now
                droomPromoMap("CreatedAt") = clsAdmin.SiteCode
                droomPromoMap("CreatedBy") = clsAdmin.UserName
                droomPromoMap("UpdatedOn") = DateTime.Now
                droomPromoMap("UpdatedAt") = clsAdmin.SiteCode
                droomPromoMap("UpdatedBy") = clsAdmin.UserName
                droomPromoMap("mstStatusID") = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RECORD_STATUS.ToString(), objHotelreservation.enumStatus.ACTIVE.ToString())
                dsMainHost.Tables("Host_ReservationRoomTypePromotionMap").Rows.Add(droomPromoMap)
                MaxId = MaxId + 1
                dsMainHost.AcceptChanges()
            Next
            MaxId = 0
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    'Code is added by irfan on 04/04/2018 for hotel reservation
    Public Function PrepareCustomersDetailsData(ByVal dtCustDetail As DataTable, ByRef dsMainHost As DataSet)
        Try
            For Each dr In dtCustDetail.Rows
                Dim dtcust As DataRow
                dtcust = dsMainHost.Tables("clpcustomers").NewRow
                dtcust("FirstName") = dr("FirstName")
                dtcust("MiddleName") = dr("MiddleName")
                dtcust("SurName") = dr("LastName")
                dtcust("Mobileno") = dr("MobileNumber")
                dtcust("EmailId") = dr("EmailId")
                dtcust("Gender") = dr("Gender")
                dtcust("CardNo") = dr("CardNo")
                dtcust("ClpProgramId") = dr("ClpProgramId")
                dtcust("SiteCode") = clsAdmin.SiteCode
                dtcust("Status") = True
                dtcust("CREATEDAT") = clsAdmin.SiteCode
                dtcust("CREATEDBY") = clsAdmin.UserName
                dtcust("CREATEDON") = clsAdmin.CurrentDate
                dtcust("UPDATEDAT") = clsAdmin.SiteCode
                dtcust("UPDATEDBY") = clsAdmin.UserName
                dtcust("UPDATEDON") = clsAdmin.CurrentDate
                dtcust("CardisActive") = True
                dtcust("AccountNo") = dr("CardNo")
                Dim expry As DateTime
                expry = objcomm.GetCardExpiryDate(dr("ClpProgramId"))
                dtcust("CardExpiryDT") = expry.ToString("yyyy-MM-dd")
                dtcust("RecordStatus") = dr("RecordStatus")
                dsMainHost.Tables("clpcustomers").Rows.Add(dtcust)
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 02-02-2017 for prepareing guest details
    Public Function PrepareGuestDetailData(ByVal dtGuestDatail As DataTable, ByRef dsMainHost As DataSet) As Boolean
        Try
            MaxId = objHotelreservation.GetMaxId("Host_ReservationGuestDetail", "reservationGuestDetailID")
            dsMainHost.Tables("Host_ReservationGuestDetail").Clear()
            Dim tempReservationGuestDetail As DataTable = dsMainHost.Tables("Host_ReservationGuestDetail").Copy()
            Dim tempReservation As DataTable = dsMainHost.Tables("Host_Reservation").Copy()
            For Each dr In dtGuestDatail.Rows
                Dim drGuest As DataRow
                drGuest = dsMainHost.Tables("Host_ReservationGuestDetail").NewRow
                drGuest("ReservationGuestDetailId") = MaxId
                drGuest("ReservationId") = dsMainHost.Tables("Host_Reservation").Rows(0)("ReservationId")
                drGuest("guestFirstName") = dr("FirstName")
                drGuest("guestMiddleName") = dr("MiddleName")
                drGuest("guestLastName") = dr("LastName")
                drGuest("guestEmailID") = dr("EmailId")
                drGuest("guestMobileNumber") = dr("MobileNumber")
                drGuest("guestAgeInYears") = dr("Age")
                drGuest("guestGender") = dr("Gender")
                drGuest("mstSupportedDocumentTypeID_1") = dr("DocumentTypeId")
                'drGuest("supportedDocumentNumber_1") = dr("DocumentType")
                drGuest("supportedDocumentDetails_1") = dr("DocumentType")
                drGuest("mstSupportedDocumentTypeID_2") = dr("DocumentTypeId")
                '  drGuest("supportedDocumentNumber_2") = dr("DocumentType")
                drGuest("supportedDocumentDetails_2") = dr("DocumentType")
                drGuest("mstSupportedDocumentTypeID_3") = dr("DocumentTypeId")
                '  drGuest("supportedDocumentNumber_3") = dr("DocumentType")
                drGuest("supportedDocumentDetails_3") = dr("DocumentType")
                ' drGuest("remarks") = dr("ReservationId")
                drGuest("isPrimaryGuest") = dr("PrimaryGuest")
                drGuest("CardNo") = dr("CardNo")
                drGuest("ClpProgramId") = dr("ClpProgramId")
                drGuest("CreatedOn") = DateTime.Now
                drGuest("CreatedAt") = clsAdmin.SiteCode
                drGuest("CreatedBy") = clsAdmin.UserName
                drGuest("UpdatedOn") = DateTime.Now
                drGuest("UpdatedAt") = clsAdmin.SiteCode
                drGuest("UpdatedBy") = clsAdmin.UserName
                drGuest("supportedDocumentDetails") = dr("supportedDocumentDetails")
                drGuest("mstStatusID") = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RECORD_STATUS.ToString(), objHotelreservation.enumStatus.ACTIVE.ToString())
                dsMainHost.Tables("Host_ReservationGuestDetail").Rows.Add(drGuest)
                MaxId = MaxId + 1
            Next
            MaxId = 0
            tempReservationGuestDetail = dsMainHost.Tables("Host_ReservationGuestDetail").Copy
            SaveGuestInCLPCustomer(dsMainHost.Tables("Host_ReservationGuestDetail"))
            'For Each drTemp In tempReservationGuestDetail.Rows
            '    Dim dt = objHotelreservation.GetClpCustomer(drTemp("guestMobileNumber"))
            '    If dt.Rows.Count > 0 Then

            '        drTemp("CardNo") = dt.Rows(0)("CardNo")
            '        drTemp("ClpProgramId") = dt.Rows(0)("ClpProgramId")
            '        tempReservationGuestDetail.AcceptChanges()
            '    End If
            '    If drTemp("isPrimaryGuest") = True Then
            '        ' dsMainHost.Tables("Host_Reservation").Rows(0)("primaryGuestDocumentNumber") = dr("supportedDocumentNumber_1")
            '        tempReservation.Rows(0)("primaryGuestDocumentDetails") = drTemp("supportedDocumentDetails_1")
            '        tempReservation.Rows(0)("primaryGuestDocumentImage") = drTemp("supportedDocumentImage_1")
            '        tempReservation.AcceptChanges()
            '    End If
            'Next
            'If tempReservation.Rows.Count > 1 Then
            '    dsMainHost.Tables("Host_Reservation").Rows(0)("primaryGuestDocumentDetails") = tempReservation.Rows(0)("primaryGuestDocumentDetails")
            '    dsMainHost.Tables("Host_Reservation").Rows(0)("primaryGuestDocumentImage") = tempReservation.Rows(0)("primaryGuestDocumentImage")
            'End If
            'dsMainHost.Merge(tempReservation)
            'dsMainHost.Merge(tempReservationGuestDetail)
            'dsMainHost.Tables.Remove("Host_Reservation")
            'dsMainHost.Tables.Add(tempReservation)
            'dsMainHost.Tables(0).Clear()
            'dsMainHost.Tables(0).Merge(tempReservation, True)

            'dsMainHost.Tables(1).Clear()
            'dsMainHost.Tables(1).Merge(tempReservationGuestDetail, True)
            'dsMainHost.Tables(0).AcceptChanges()
            'dsMainHost.Tables(1).AcceptChanges()
            'dsMainHost.AcceptChanges()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 09-02-2017
    Public Function PrepareTaxData(ByVal dtMapTaxDetails As DataTable, ByRef dsMainHost As DataSet) As Boolean
        Try
            MaxId = objHotelreservation.GetMaxId("Host_ReservationTaxMap", "reservationTaxMapID")
            dsMainHost.Tables("Host_ReservationTaxMap").Clear()
            Dim temp As DataTable = dsMainHost.Tables("Host_ReservationTaxMap").Copy()
            For Each dr In dtMapTaxDetails.Rows
                Dim drMapTaxDetails As DataRow
                drMapTaxDetails = dsMainHost.Tables("Host_ReservationTaxMap").NewRow
                drMapTaxDetails("reservationTaxMapID") = MaxId
                drMapTaxDetails("ReservationId") = dsMainHost.Tables("Host_Reservation").Rows(0)("ReservationId")
                drMapTaxDetails("SiteCode") = dr("SiteCode")
                drMapTaxDetails("TaxLineNo") = dr("TaxLineNo")
                drMapTaxDetails("DocumentType") = dr("DocumentType")
                drMapTaxDetails("TaxCode") = dr("TaxCode")
                drMapTaxDetails("TaxValueInPercent") = dr("TaxValueInPercent")
                drMapTaxDetails("TaxValueInAmount") = dr("TaxValueInAmount")
                drMapTaxDetails("CreatedOn") = DateTime.Now
                drMapTaxDetails("CreatedAt") = clsAdmin.SiteCode
                drMapTaxDetails("CreatedBy") = clsAdmin.UserName
                drMapTaxDetails("UpdatedOn") = DateTime.Now
                drMapTaxDetails("UpdatedAt") = clsAdmin.SiteCode
                drMapTaxDetails("UpdatedBy") = clsAdmin.UserName
                drMapTaxDetails("mstStatusID") = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RECORD_STATUS.ToString(), objHotelreservation.enumStatus.ACTIVE.ToString())
                dsMainHost.Tables("Host_ReservationTaxMap").Rows.Add(drMapTaxDetails)
                MaxId = MaxId + 1
            Next
            MaxId = 0
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 13-02-2017 for reservation receipt
    Public Function PrepareReceiptData(ByVal dtReceiptDetails As DataTable, ByRef dsMainHost As DataSet) As Boolean
        Try
            MaxId = objHotelreservation.GetMaxId("Host_ReservationReceipt", "reservationReceiptID")
            dsMainHost.Tables("Host_ReservationReceipt").Clear()
            Dim temp As DataTable = dsMainHost.Tables("Host_ReservationReceipt").Copy()
            For Each dr In dtReceiptDetails.Rows
                Dim drReceiptDetails As DataRow
                drReceiptDetails = dsMainHost.Tables("Host_ReservationReceipt").NewRow
                drReceiptDetails("reservationReceiptID") = MaxId
                drReceiptDetails("ReservationId") = dsMainHost.Tables("Host_Reservation").Rows(0)("ReservationId")
                If dtReceiptDetails.Columns.Contains("TenderHeadCode") Then
                    drReceiptDetails("TenderHeadCode") = dr("TenderHeadCode")
                Else
                    drReceiptDetails("TenderHeadCode") = dr("RecieptType")
                End If
                If dtReceiptDetails.Columns.Contains("TenderTypeCode") Then
                    drReceiptDetails("TenderTypeCode") = dr("TenderTypeCode")
                Else
                    drReceiptDetails("TenderTypeCode") = dr("RecieptTypeCode")
                End If
                drReceiptDetails("CurrencyCode") = dr("CurrencyCode")

                'If cmdChkInDate.Value Is Nothing Or IsDBNull(cmdChkInDate.Value) Then
                '    ShowMessage(getValueByKey(" Please select check in Date"), "Information - " & "Information")
                '    cmdChkInDate.Focus()
                'End If
                'If cmdCheckoutDate.Value Is Nothing Or IsDBNull(cmdCheckoutDate.Value) Then
                '    ShowMessage(getValueByKey(" Please select check out Date"), "Information - " & "Information")
                '    cmdChkInDate.Focus()
                'End If

                If dr("billReceiptDate") Is Nothing Or Not IsDBNull(dr("billReceiptDate")) Then
                    drReceiptDetails("billReceiptDate") = dr("billReceiptDate")
                Else
                    drReceiptDetails("billReceiptDate") = System.DateTime.Now
                End If
                If dr("billReceiptTime") Is Nothing Or Not IsDBNull(dr("billReceiptTime")) Then
                    drReceiptDetails("billReceiptTime") = dr("billReceiptTime")
                Else
                    drReceiptDetails("billReceiptTime") = System.DateTime.Now
                End If
                drReceiptDetails("StaffID") = ""
                If dr("AmountinCurrency") Is Nothing Or Not IsDBNull(dr("AmountinCurrency")) Then
                    drReceiptDetails("AmountinCurrency") = dr("AmountinCurrency")
                End If


                If dtReceiptDetails.Columns.Contains("AmountTendered") Then
                    drReceiptDetails("AmountTendered") = dr("AmountTendered")
                Else
                    drReceiptDetails("AmountTendered") = dr("Amount")
                    'If dr("billReceiptTime") Is Nothing Or Not IsDBNull(dr("billReceiptTime")) Then
                    If drReceiptDetails("AmountinCurrency") Is Nothing Or IsDBNull(drReceiptDetails("AmountinCurrency")) Then
                        drReceiptDetails("AmountinCurrency") = dr("Amount")
                    End If
                End If
                If dtReceiptDetails.Columns.Contains("receiptLineNo") Then
                    drReceiptDetails("receiptLineNo") = dr("receiptLineNo")
                Else
                    drReceiptDetails("receiptLineNo") = dr("SrNo")
                End If


                drReceiptDetails("SiteCode") = clsAdmin.SiteCode
                drReceiptDetails("yearInyyyy") = _chkInDate.Year
                drReceiptDetails("reservationNumber") = dsMainHost.Tables("Host_Reservation").Rows(0)("reservationNumber")
                drReceiptDetails("ExchangeRate") = If(dr("ExchangeRate") Is DBNull.Value, 0, dr("ExchangeRate"))
                drReceiptDetails("ManagersKeytoUpdate") = 1
                drReceiptDetails("ChangeLine") = dr("ChangeLine")
                drReceiptDetails("BankAccNo") = dr("BankAccNo")
                drReceiptDetails("TerminalID") = clsAdmin.TerminalID
                If dtReceiptDetails.Columns.Contains("CardNo") Then
                    drReceiptDetails("CardNo") = dr("CardNo")
                Else
                    drReceiptDetails("CardNo") = dr("number")
                End If

                drReceiptDetails("RefNo_1") = ""
                drReceiptDetails("RefNo_2") = ""
                drReceiptDetails("RefNo_3") = ""
                drReceiptDetails("RefNo_4") = ""
                If dr("billReceiptDate") Is Nothing Or Not IsDBNull(dr("billReceiptDate")) Then
                    drReceiptDetails("RefDate") = dr("billReceiptDate")
                Else
                    drReceiptDetails("RefDate") = System.DateTime.Now
                End If


                drReceiptDetails("remarks") = _remarks
                drReceiptDetails("CreatedOn") = DateTime.Now
                drReceiptDetails("CreatedAt") = clsAdmin.SiteCode
                drReceiptDetails("CreatedBy") = clsAdmin.UserName
                drReceiptDetails("UpdatedOn") = DateTime.Now
                drReceiptDetails("UpdatedAt") = clsAdmin.SiteCode
                drReceiptDetails("UpdatedBy") = clsAdmin.UserName
                drReceiptDetails("mstStatusID") = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RECORD_STATUS.ToString(), objHotelreservation.enumStatus.ACTIVE.ToString())
                dsMainHost.Tables("Host_ReservationReceipt").Rows.Add(drReceiptDetails)
                MaxId = MaxId + 1
            Next
            MaxId = 0
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    'added by khusrao adil on 14-02-2017 for payment by check 
    Public Function PrepareCheckPaymentDetails(ByVal dtReceiptCheckPaymentDetails As DataTable, ByRef dsMainHost As DataSet) As Boolean
        Try
            If dsMainHost.Tables.Contains("CheckDtls") Then
                'MaxId = objHotelreservation.GetMaxId("CheckDtls", "reservationReceiptID")
                dsMainHost.Tables("CheckDtls").Clear()
                Dim temp As DataTable = dsMainHost.Tables("CheckDtls").Copy()
                For Each dr In dtReceiptCheckPaymentDetails.Rows
                    Dim drReceiptCheckPaymentDetails As DataRow
                    drReceiptCheckPaymentDetails = dsMainHost.Tables("CheckDtls").NewRow
                    drReceiptCheckPaymentDetails("SiteCode") = clsAdmin.SiteCode
                    drReceiptCheckPaymentDetails("ReservationId") = dsMainHost.Tables("Host_Reservation").Rows(0)("ReservationId")
                    drReceiptCheckPaymentDetails("FinYear") = clsAdmin.Financialyear
                    drReceiptCheckPaymentDetails("BillNo") = dsMainHost.Tables("Host_Reservation").Rows(0)("reservationNumber")
                    drReceiptCheckPaymentDetails("PayLineNo") = dr("PayLineNo")
                    drReceiptCheckPaymentDetails("CheckNo") = dr("CheckNo")
                    drReceiptCheckPaymentDetails("DocumentNo") = dr("DocumentNo")
                    drReceiptCheckPaymentDetails("DocumentType") = dr("DocumentType")
                    drReceiptCheckPaymentDetails("BillDate") = dr("BillDate")
                    drReceiptCheckPaymentDetails("Amount") = dr("Amount")
                    drReceiptCheckPaymentDetails("DueDate") = dr("DueDate")
                    drReceiptCheckPaymentDetails("Remarks") = dr("Remarks")
                    drReceiptCheckPaymentDetails("STATUS") = dr("STATUS")
                    drReceiptCheckPaymentDetails("BankName") = dr("BankName")
                    drReceiptCheckPaymentDetails("CustomerName") = dr("CustomerName")
                    drReceiptCheckPaymentDetails("TelephoneNumber") = dr("TelephoneNumber")
                    drReceiptCheckPaymentDetails("CreatedOn") = DateTime.Now
                    drReceiptCheckPaymentDetails("CreatedAt") = clsAdmin.SiteCode
                    drReceiptCheckPaymentDetails("CreatedBy") = clsAdmin.UserName
                    drReceiptCheckPaymentDetails("UpdatedOn") = DateTime.Now
                    drReceiptCheckPaymentDetails("UpdatedAt") = clsAdmin.SiteCode
                    drReceiptCheckPaymentDetails("UpdatedBy") = clsAdmin.UserName
                    dsMainHost.Tables("CheckDtls").Rows.Add(drReceiptCheckPaymentDetails)
                    MaxId = MaxId + 1
                Next
                MaxId = 0
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function SaveGuestInCLPCustomer(ByRef dtGuestClp As DataTable)
        Try
            Dim primmaryGustMobileNumber As String = ""
            For Each dr In dtGuestClp.Rows
                Dim docNo As String = objcomm.getDocumentNo("Customer Loyalty", clsAdmin.SiteCode)
                Dim otherCharacters = "CLS" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3, 3)
                Dim CardNumber = GenDocNo(otherCharacters, 15, docNo)
                Dim TiersList = CLP_Data.GetCLPTiers(clsAdmin.CLPProgram)
                Dim CardType As String
                If TiersList.Count > 0 Then
                    CardType = TiersList(0)
                End If
                If objReservation.InsertCustomerDetails(dr("guestFirstName") + " " + dr("guestMiddleName") + " " + dr("guestLastName"), CardNumber, dr("guestMobileNumber"), clsAdmin.SiteCode, clsAdmin.UserCode, clsAdmin.CLPProgram, CardType) = True Then

                    Dim dt = objHotelreservation.GetClpCustomer(dr("guestMobileNumber"))
                    If dt.Rows.Count > 0 Then
                        dr("CardNo") = dt.Rows(0)("CardNo")
                        dr("ClpProgramId") = dt.Rows(0)("ClpProgramId")
                        dtGuestClp.AcceptChanges()
                    End If
                    If dr("isPrimaryGuest") = True Then
                        ' dsMainHost.Tables("Host_Reservation").Rows(0)("primaryGuestDocumentNumber") = dr("supportedDocumentNumber_1")
                        dsMainHost.Tables("Host_Reservation").Rows(0)("primaryGuestDocumentDetails") = dr("supportedDocumentDetails_1")
                        dsMainHost.Tables("Host_Reservation").Rows(0)("primaryGuestDocumentImage") = dr("supportedDocumentImage_1")
                        dsMainHost.AcceptChanges()
                        ReservationGuestName = dr("guestFirstName").ToString() + " " + dr("guestMiddleName").ToString() + " " + dr("guestLastName").ToString()
                        ReservationDate = System.DateTime.Now.ToString()
                    End If
                End If


            Next
            dtGuestClp.AcceptChanges()
            dsMainHost.AcceptChanges()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

#End Region


#End Region
    ' "--end Functions"
#End Region

    Private Sub btnSaveAndCheckIn_Click(sender As Object, e As EventArgs) Handles btnSaveAndCheckIn.Click
        isCheckIn = True
        PrepareDataSave()
        If dsMainHost.Tables("Host_ReservationGuestDetail").Rows.Count = 0 Then
            ShowMessage("please go back and enter the guest details before check-in", "Information")
            Exit Sub
        End If
        isCheckIn = False
        SaveAndPrintReservation(True)
        Dim ReservationId = dsMainHost.Tables("Host_Reservation").Rows(0)("reservationID").ToString()
        ReservationRNumber = dsMainHost.Tables("Host_Reservation").Rows(0)("reservationNumber").ToString()
        'Dim disStatusMgs = "Reservation Saved Successfully for"  'for reservation
        '"Check-In Successfull for"  ' for check in
        'ReservationRNumber = "RN0010000000020"
        'ReservationGuestName = "Khusrao Khan"
        ReservationDate = System.DateTime.Now.ToString("dd/MM/yyyy")
        If ReservationCheckIn(ReservationId) = True Then
            'documentNo
            ShowMessage("Check-In Successfully for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
            dtGuest.Clear()
            dtReservationTaxMap.Clear()
            dtPromotions.Clear()
            dtReservationReceipt.Clear()
            dtHotelAllTaxes.Clear()
            dtPromotionTotalDiscountValues.Clear()
            dtReservation.Rows.Clear()

            tpCreateDetails.Enabled = True
            tpCreateDetails.Show()
            frmHostCreateReservation_Load(sender, e)

            btnPreviousTab.Visible = False
            btnSaveAndFinish.Enabled = False
            btnSaveAndCheckIn.Enabled = False
            btnNextTab.Enabled = True
            cmdCheckoutDate.Value = DateTime.Now
            cmdChkInDate.Value = DateTime.Now
        Else
            ShowMessage("Check-In Failed for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
        End If
        '   Me.Close()
    End Sub
    Public Function ReservationCheckIn(ByVal reservationid As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Dim CheckInStatusId = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RESERVATION_STATUS.ToString(), objHotelreservation.enumStatus.CHECKED_IN.ToString())

        If objHotelreservation.UpdateReservationDetails(clsAdmin.SiteCode, reservationid, CheckInStatusId, clsAdmin.UserName, tran) Then
            Return True
        Else
            Return False
        End If
    End Function


#Region " promotion combos event -------------"
    'added by khusrao adil on 09-03-2017 for room promotions
    Dim _roomTypeDiscountAmountRemove1 As Decimal = "0.0"
    Dim _roomTypeDiscountAmountRemove2 As Decimal = "0.0"
    Dim _roomTypeDiscountAmountRemove3 As Decimal = "0.0"
    Dim _roomTypeDiscountAmountRemove4 As Decimal = "0.0"
    Dim _roomTypeDiscountAmountRemove5 As Decimal = "0.0"
    Dim _roomTypeDiscountAmountRemove6 As Decimal = "0.0"
    Dim _roomTypeDiscountAmountRemove7 As Decimal = "0.0"
    Dim _roomTypeDiscountAmountRemove8 As Decimal = "0.0"
    Dim _roomTypeDiscountAmountRemove9 As Decimal = "0.0"
    Dim _roomTypeDiscountAmountRemove10 As Decimal = "0.0"
    'added by khusrao adil on 29-3-2017 
    ' method prepare promtion values for saving with according to their roomtypes
    Public Function Promotion(ByVal roomTypeId As String, ByVal promotionId As String)
        Try
            'dtPromotionSelected
            If roomTypeId Is Nothing Then
                Exit Function
            End If
            If promotionId Is Nothing Then
                Exit Function
            End If

            Dim dtCheck = dtPromotions.Select("RoomTypeId='" + roomTypeId + "'")
            If dtCheck.Length > 0 Then
                Dim drValue = dtRoomTypeWisePromotions.Select("mstRoomTypeId='" + roomTypeId + "' and mstPromotionID='" + promotionId + "'")

                Dim _rate As Decimal = Convert.ToDecimal(drValue(0)("rate"))
                Dim _DiscountInPercent As Decimal = Convert.ToDecimal(drValue(0)("discountInPercent"))
                Dim _roomTypeDiscountAmount As Decimal = "0.0"
                _roomTypeDiscountAmount = (_rate / 100) * _DiscountInPercent
                dtCheck(0)("Selects") = True
                dtCheck(0)("RoomTypeId") = roomTypeId
                dtCheck(0)("mstPromotionID") = promotionId
                dtCheck(0)("discountInPercent") = _DiscountInPercent
                dtCheck(0)("discountAmount") = _roomTypeDiscountAmount
                dtCheck(0)("Rate") = _rate
                dtCheck(0)("mstPromotionName") = drValue(0)("PromotionName")
                dtCheck(0)("mstSiteRoomTypeMap") = drValue(0)("mstSiteRoomTypeMap")
                'dtCheck(0)("reservationId") = drValue(0)("mstSiteRoomTypeMap")
                dtCheck(0)("yearInyyyy") = Date.Now.Year
                dtCheck(0)("weekDay") = Date.Now.DayOfWeek
                'dtCheck(0)(" reservationroommapid") = drValue(0)("mstSiteRoomTypeMap")
                'dtCheck(0)("reservationRoomTypePromotionMapID") = drValue(0)("mstSiteRoomTypeMap")
                dtCheck(0)("RateDate") = cmdChkInDate.Value

                dtCheck(0)("RecordStatus") = ""
            Else
                Dim drValue = dtRoomTypeWisePromotions.Select("mstRoomTypeId='" + roomTypeId + "' and mstPromotionID='" + promotionId + "'")
                Dim drRow As DataRow
                drRow = dtPromotions.NewRow()
                Dim _rate As Decimal = Convert.ToDecimal(drValue(0)("rate"))
                Dim _DiscountInPercent As Decimal = Convert.ToDecimal(drValue(0)("discountInPercent"))
                Dim _roomTypeDiscountAmount As Decimal = "0.0"
                _roomTypeDiscountAmount = (_rate / 100) * _DiscountInPercent
                drRow("Selects") = True
                drRow("RoomTypeId") = roomTypeId
                drRow("mstPromotionID") = promotionId
                drRow("discountInPercent") = _DiscountInPercent
                drRow("discountAmount") = _roomTypeDiscountAmount
                drRow("Rate") = _rate
                drRow("mstPromotionName") = drValue(0)("PromotionName")
                drRow("mstSiteRoomTypeMap") = drValue(0)("mstSiteRoomTypeMap")
                'drRow("reservationId") = dtPromotions.Rows(0)("reservationId")
                drRow("yearInyyyy") = Date.Now.Year
                drRow("weekDay") = Date.Now.DayOfWeek
                drRow("RateDate") = cmdChkInDate.Value
                drRow("RecordStatus") = ""
                dtPromotions.Rows.Add(drRow)
            End If
            dtPromotions.AcceptChanges()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 10-03-2017 handle all promotions drop downs events
    Private Function CtrlComboEvent(ByRef ctrlComboBox As ctrlCombo, ByRef lblRoomTypeName As CtrlLabel, ByRef lblPromoAppliedName As CtrlLabel,
                                    ByRef lblCostAfterPromoValue As CtrlLabel, ByRef lblCostBeforPromoValue As CtrlLabel, ByRef _roomTypeDiscountAmountRemove As Decimal)
        Try
            Dim _mstRoomTypeId As String = lblRoomTypeName.Tag
            Dim _mstPromotionId As String = ctrlComboBox.SelectedValue
            If ctrlComboBox.SelectedValue <> "0" Then
                If _mstRoomTypeId <> Nothing Then
                    'Dim rowMTDt = dtPromotions.Select("RoomTypeId='" + _mstRoomTypeId + "'")
                    'If rowMTDt.Length > 0 Then
                    '    For Each row As DataRow In rowMTDt
                    '        dtPromotions.Rows.Remove(row)
                    '    Next
                    'End If
                    If ctrlComboBox.SelectedValue = Nothing Then
                        _mstPromotionId = ctrlComboBox.Tag
                    End If
                    If _mstPromotionId Is Nothing Then
                        Exit Function
                    End If
                    Dim rowMTDtPRO = dtPromotionTotalDiscountValues.Select("RoomTypeId='" + _mstRoomTypeId + "'")
                    If rowMTDtPRO.Length > 0 Then
                        For Each row As DataRow In rowMTDtPRO
                            dtPromotionTotalDiscountValues.Rows.Remove(row)
                        Next
                    End If



                    Dim dtpromoSelected = dtRoomTypeWisePromotions.Select("MstRoomTypeId='" + _mstRoomTypeId + "' and mstPromotionId='" + _mstPromotionId + "'")
                    If dtpromoSelected.Length <> 0 Then
                        Dim _promotionName As String = dtpromoSelected(0)("PromotionName").ToString()
                        Dim _mstSiteRoomTypeMap As String = dtpromoSelected(0)("mstSiteRoomTypeMap").ToString()
                        Dim _rate As Decimal = Convert.ToDecimal(dtpromoSelected(0)("Rate").ToString())
                        Dim _DiscountInPercent As Decimal = Convert.ToDecimal(dtpromoSelected(0)("discountInPercent").ToString())
                        Dim _roomTypeDiscountAmount As Decimal = "0.0"
                        Dim _roomTypeAmountAfterPromoApplied As Decimal = "0.0"

                        _roomTypeDiscountAmount = (_rate / 100) * _DiscountInPercent
                        _roomTypeAmountAfterPromoApplied = _rate - _roomTypeDiscountAmount
                        lblCostAfterPromoValue.Text = _roomTypeAmountAfterPromoApplied.ToString()
                        _roomTypeDiscountAmountRemove = _roomTypeDiscountAmount
                        If AllPromotionTotalDiscountAmount = 0 Then
                            AllPromotionTotalDiscountAmount = AllPromotionTotalDiscountAmount + _roomTypeDiscountAmount
                        Else
                            AllPromotionTotalDiscountAmount = AllPromotionTotalDiscountAmount - _roomTypeDiscountAmountRemove
                            AllPromotionTotalDiscountAmount = AllPromotionTotalDiscountAmount + _roomTypeDiscountAmount
                        End If

                        Dim drPRO As DataRow
                        drPRO = dtPromotionTotalDiscountValues.NewRow()
                        drPRO("RoomTypeId") = _mstRoomTypeId
                        drPRO("TotalPromotionCost") = _roomTypeDiscountAmount
                        dtPromotionTotalDiscountValues.Rows.Add(drPRO)
                        dtPromotionTotalDiscountValues.AcceptChanges()
                        lblPromoAppliedName.Text = _promotionName
                        lblPromoAppliedName.Tag = _mstPromotionId
                        'RoomTypeWiseSelectedPromotions(_mstRoomTypeId, _mstPromotionId, _mstSiteRoomTypeMap, _promotionName, _DiscountInPercent, _rate, _roomTypeDiscountAmount)
                    Else
                        lblPromoAppliedName.Text = "No Promotion"
                        lblPromoAppliedName.Tag = "0"
                        lblCostAfterPromoValue.Text = lblCostBeforPromoValue.Text
                    End If
                End If
            Else
                AllPromotionTotalDiscountAmount = AllPromotionTotalDiscountAmount - _roomTypeDiscountAmountRemove
                '  PromotionAmountDetails(SelectedRoomsCost, AllPromotionTotalDiscountAmount)
                ' RoomTypeWiseSelectedPromotions(_mstRoomTypeId, _mstPromotionId, "", "", "0.0", "0.0", "0.0", True)
                lblCostAfterPromoValue.Text = lblCostBeforPromoValue.Text
                lblPromoAppliedName.Text = "No Promotion"
                Dim rowMTDtPRO = dtPromotionTotalDiscountValues.Select("RoomTypeId='" + _mstRoomTypeId + "'")
                If rowMTDtPRO.Length > 0 Then
                    For Each row As DataRow In rowMTDtPRO
                        dtPromotionTotalDiscountValues.Rows.Remove(row)
                    Next
                End If
                If ctrlComboBox.SelectedValue = Nothing Then
                    _mstPromotionId = ctrlComboBox.Tag
                End If
            End If
            If ctrlComboBox.SelectedValue <> Nothing Or ctrlComboBox.Text <> "" Then
                If dtPromotionTotalDiscountValues.Rows.Count Then
                    Dim vl = dtPromotionTotalDiscountValues.Compute("SUM(TotalPromotionCost)", "")
                    If vl.ToString() <> "" Then
                        PromotionAmountDetails(SelectedRoomsCost, vl)
                    Else
                        PromotionAmountDetails(SelectedRoomsCost, "0")
                    End If
                Else
                    PromotionAmountDetails(SelectedRoomsCost, "0")
                End If
            End If
            'Dim vl = dtPromotionTotalDiscountValues.Compute("SUM(TotalPromotionCost)", "")
            'If vl.ToString() <> "" Then
            '    PromotionAmountDetails(SelectedRoomsCost, vl)
            'Else
            '    PromotionAmountDetails(SelectedRoomsCost, "0")
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Sub CtrlComboPromotion1_TextChanged(sender As Object, e As EventArgs) Handles CtrlComboPromotion1.TextChanged
        CtrlComboEvent(CtrlComboPromotion1, CtrllblRoomTypeName1, CtrllblPromoAppliedValue1,
                       CtrllblCostAfterPromoValue1, CtrllblCostBeforePromoValue1, _roomTypeDiscountAmountRemove1)
    End Sub
    Private Sub CtrlComboPromotion2_TextChanged(sender As Object, e As EventArgs) Handles CtrlComboPromotion2.TextChanged
        CtrlComboEvent(CtrlComboPromotion2, CtrllblRoomTypeName2, CtrllblPromoAppliedValue2,
                       CtrllblCostAfterPromoValue2, CtrllblCostBeforePromoValue2, _roomTypeDiscountAmountRemove2)
    End Sub

    Private Sub CtrlComboPromotion3_TextChanged(sender As Object, e As EventArgs) Handles CtrlComboPromotion3.TextChanged
        CtrlComboEvent(CtrlComboPromotion3, CtrllblRoomTypeName3, CtrllblPromoAppliedValue3,
                      CtrllblCostAfterPromoValue3, CtrllblCostBeforePromoValue3, _roomTypeDiscountAmountRemove3)
    End Sub

    Private Sub CtrlComboPromotion4_TextChanged(sender As Object, e As EventArgs) Handles CtrlComboPromotion4.TextChanged
        CtrlComboEvent(CtrlComboPromotion4, CtrllblRoomTypeName4, CtrllblPromoAppliedValue4,
                          CtrllblCostAfterPromoValue4, CtrllblCostBeforePromoValue4, _roomTypeDiscountAmountRemove4)
    End Sub

    Private Sub CtrlComboPromotion5_TextChanged(sender As Object, e As EventArgs) Handles CtrlComboPromotion5.TextChanged
        CtrlComboEvent(CtrlComboPromotion5, CtrllblRoomTypeName5, CtrllblPromoAppliedValue5,
                          CtrllblCostAfterPromoValue5, CtrllblCostBeforePromoValue5, _roomTypeDiscountAmountRemove5)
    End Sub

    Private Sub CtrlComboPromotion6_TextChanged(sender As Object, e As EventArgs) Handles CtrlComboPromotion6.TextChanged
        CtrlComboEvent(CtrlComboPromotion6, CtrllblRoomTypeName6, CtrllblPromoAppliedValue6,
                          CtrllblCostAfterPromoValue6, CtrllblCostBeforePromoValue6, _roomTypeDiscountAmountRemove6)
    End Sub

    Private Sub CtrlComboPromotion7_TextChanged(sender As Object, e As EventArgs) Handles CtrlComboPromotion7.TextChanged
        CtrlComboEvent(CtrlComboPromotion7, CtrllblRoomTypeName7, CtrllblPromoAppliedValue7,
                          CtrllblCostAfterPromoValue7, CtrllblCostBeforePromoValue7, _roomTypeDiscountAmountRemove7)
    End Sub

    Private Sub CtrlComboPromotion8_TextChanged(sender As Object, e As EventArgs) Handles CtrlComboPromotion8.TextChanged
        CtrlComboEvent(CtrlComboPromotion8, CtrllblRoomTypeName8, CtrllblPromoAppliedValue8,
                          CtrllblCostAfterPromoValue8, CtrllblCostBeforePromoValue8, _roomTypeDiscountAmountRemove8)
    End Sub

    Private Sub CtrlComboPromotion9_TextChanged(sender As Object, e As EventArgs) Handles CtrlComboPromotion9.TextChanged
        CtrlComboEvent(CtrlComboPromotion9, CtrllblRoomTypeName9, CtrllblPromoAppliedValue9,
                          CtrllblCostAfterPromoValue9, CtrllblCostBeforePromoValue9, _roomTypeDiscountAmountRemove9)
    End Sub

    Private Sub CtrlComboPromotion10_TextChanged(sender As Object, e As EventArgs) Handles CtrlComboPromotion10.TextChanged
        CtrlComboEvent(CtrlComboPromotion10, CtrllblRoomTypeName10, CtrllblPromoAppliedValue10,
                        CtrllblCostAfterPromoValue10, CtrllblCostBeforePromoValue10, _roomTypeDiscountAmountRemove10)
    End Sub
#End Region



    Public Sub DefaultTheme()
        ' create reservation
        Me.PictureBox1.Image = Global.Spectrum.My.Resources.Resources.promotion_image
        Me.grdReservation.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        ' Me.grdReservation.Size = New Size(1500, 263) x
        Me.grdReservation.Styles.Fixed.BackColor = Color.FromArgb(208, 208, 208)
        Me.grdReservation.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.grdReservation.Styles.Fixed.Font = New Font("Callibri", 10, FontStyle.Bold)
        Me.grdReservation.Styles.Focus.Font = New Font("Callibri", 10, FontStyle.Bold)
        Me.grdReservation.Styles.Highlight.BackColor = Color.FromArgb(252, 252, 252)
        'Me.grdReservation.
        'Me.grdReservation.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(208, 208, 208)
        '' Me.grdReservation.Styles.Normal.BackColor = Color.FromArgb(246, 246, 246)
        '' Me.grdReservation.Styles.Normal.Border.Style = BorderStyleEnum.Flat
        '' Me.grdReservation.Styles.Normal.Border.Width = 1

        'Me.grdReservation.Styles.Highlight.BackColor = Color.FromArgb(252, 252, 252)

        ''Me.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black

        Me.grdGuestDetails.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.grdGuestDetails.Size = New Size(1375, 263)
        Me.grdGuestDetails.Styles.Fixed.BackColor = Color.FromArgb(208, 208, 208)
        Me.grdGuestDetails.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.grdGuestDetails.Styles.Fixed.Font = New Font("Callibri", 10, FontStyle.Bold)
        Me.grdGuestDetails.Styles.Focus.Font = New Font("Callibri", 10, FontStyle.Bold)
        Me.grdGuestDetails.Styles.Highlight.BackColor = Color.FromArgb(252, 252, 252)

        Me.gridSummaryDetails.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        '  Me.gridSummaryDetails.Size = New Size(1375, 263) x
        Me.gridSummaryDetails.Styles.Fixed.BackColor = Color.FromArgb(208, 208, 208)
        Me.gridSummaryDetails.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.gridSummaryDetails.Styles.Fixed.Font = New Font("Callibri", 10, FontStyle.Bold)
        Me.gridSummaryDetails.Styles.Focus.Font = New Font("Callibri", 10, FontStyle.Bold)
        Me.gridSummaryDetails.Styles.Highlight.BackColor = Color.FromArgb(252, 252, 252)

        Me.cmdSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdSearch.FlatStyle = FlatStyle.Flat
        Me.cmdSearch.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdSearch.FlatAppearance.BorderSize = 0
        Me.cmdSearch.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmdSearch.TextAlign = ContentAlignment.MiddleCenter
        Me.cmdSearch.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdSearch.ForeColor = Color.White

        Me.btnPreviousTab.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnPreviousTab.FlatStyle = FlatStyle.Flat
        Me.btnPreviousTab.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.btnPreviousTab.FlatAppearance.BorderSize = 0
        Me.btnPreviousTab.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.btnPreviousTab.TextAlign = ContentAlignment.MiddleCenter
        Me.btnPreviousTab.BackColor = Color.FromArgb(0, 113, 188)
        Me.btnPreviousTab.ForeColor = Color.White

        Me.btnNextTab.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnNextTab.FlatStyle = FlatStyle.Flat
        Me.btnNextTab.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.btnNextTab.FlatAppearance.BorderSize = 0
        Me.btnNextTab.BackColor = Color.FromArgb(0, 113, 188)
        Me.btnNextTab.TextAlign = ContentAlignment.MiddleCenter
        Me.btnNextTab.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.btnNextTab.ForeColor = Color.White

        Me.btnSaveAndFinish.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnSaveAndFinish.FlatStyle = FlatStyle.Flat
        Me.btnSaveAndFinish.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.btnSaveAndFinish.FlatAppearance.BorderSize = 0
        Me.btnSaveAndFinish.BackColor = Color.FromArgb(208, 208, 208)
        Me.btnSaveAndFinish.TextAlign = ContentAlignment.MiddleCenter
        Me.btnSaveAndFinish.Font = New Font("Callibri", 7)
        Me.btnSaveAndFinish.ForeColor = Color.White

        Me.btnSaveAndCheckIn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnSaveAndCheckIn.FlatStyle = FlatStyle.Flat
        Me.btnSaveAndCheckIn.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.btnSaveAndCheckIn.FlatAppearance.BorderSize = 0
        Me.btnSaveAndCheckIn.BackColor = Color.FromArgb(208, 208, 208)
        Me.btnSaveAndCheckIn.TextAlign = ContentAlignment.MiddleCenter
        Me.btnSaveAndCheckIn.Font = New Font("Callibri", 7)
        Me.btnSaveAndCheckIn.ForeColor = Color.White

        Me.cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdCancel.FlatStyle = FlatStyle.Flat
        Me.cmdCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdCancel.TextAlign = ContentAlignment.MiddleCenter
        Me.cmdCancel.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmdCancel.ForeColor = Color.White

        ' btnGuestAdd
        Me.btnGuestAdd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnGuestAdd.FlatStyle = FlatStyle.Flat
        Me.btnGuestAdd.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.btnGuestAdd.FlatAppearance.BorderSize = 0
        Me.btnGuestAdd.BackColor = Color.FromArgb(0, 113, 188)
        Me.btnGuestAdd.ForeColor = Color.White
        Me.btnGuestAdd.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.btnGuestAdd.TextAlign = ContentAlignment.MiddleCenter

        Me.btnGuestClear.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnGuestClear.FlatStyle = FlatStyle.Flat
        Me.btnGuestClear.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.btnGuestClear.FlatAppearance.BorderSize = 0
        Me.btnGuestClear.BackColor = Color.FromArgb(0, 113, 188)
        Me.btnGuestClear.ForeColor = Color.White
        Me.btnGuestClear.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.btnGuestClear.TextAlign = ContentAlignment.MiddleCenter

        'btnUpload
        Me.btnUpload.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnUpload.FlatStyle = FlatStyle.Flat
        Me.btnUpload.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.btnUpload.FlatAppearance.BorderSize = 0
        Me.btnUpload.BackColor = Color.FromArgb(0, 113, 188)
        Me.btnUpload.ForeColor = Color.White
        Me.btnUpload.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.btnUpload.TextAlign = ContentAlignment.MiddleCenter

        ' summary
        ' payment 1
        Me.cmdPayment.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdPayment.FlatStyle = FlatStyle.Flat
        Me.cmdPayment.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdPayment.FlatAppearance.BorderSize = 0
        Me.cmdPayment.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmdPayment.TextAlign = ContentAlignment.BottomCenter
        Me.cmdPayment.ImageAlign = ContentAlignment.MiddleCenter
        Me.cmdPayment.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdPayment.ForeColor = Color.White

        Me.cmdCash.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdCash.FlatStyle = FlatStyle.Flat
        Me.cmdCash.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdCash.FlatAppearance.BorderSize = 0
        Me.cmdCash.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmdCash.TextAlign = ContentAlignment.BottomCenter
        Me.cmdCash.ImageAlign = ContentAlignment.MiddleCenter
        Me.cmdCash.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdCash.ForeColor = Color.White

        Me.cmdCard.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdCard.FlatStyle = FlatStyle.Flat
        Me.cmdCard.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdCard.FlatAppearance.BorderSize = 0
        Me.cmdCard.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmdCard.TextAlign = ContentAlignment.BottomCenter
        Me.cmdCard.ImageAlign = ContentAlignment.MiddleCenter
        Me.cmdCard.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdCard.ForeColor = Color.White

        Me.cmdCheque.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdCheque.FlatStyle = FlatStyle.Flat
        Me.cmdCheque.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdCheque.FlatAppearance.BorderSize = 0
        Me.cmdCheque.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmdCheque.TextAlign = ContentAlignment.BottomCenter
        Me.cmdCheque.ImageAlign = ContentAlignment.MiddleCenter
        Me.cmdCheque.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdCheque.ForeColor = Color.White
        Me.tpPaymentsDetails.TabForeColorSelected = Color.FromArgb(208, 208, 208)
    End Sub




End Class
