Imports SpectrumBL
Imports C1.Win.C1FlexGrid

Imports System.IO
Imports System.Text
Imports System.Data.SqlClient

Imports System.Net
Imports SpectrumPrint
Public Class frmHostEditCheckinReservation

#Region "Globle variables for class"
    Dim objHotelreservation As New clsHotelReservation
    Dim objCustm As New clsSOCustomer
    Dim objReservation As New clsReservation
    Dim objcomm As New clsCommon
    Dim objCM As New clsCashMemo

    Dim dsMain As New DataSet
    Dim dsMainHostEdit As New DataSet
    Dim dsCombo As DataSet = New DataSet
    Dim dsCashMemoComboDtl As New DataSet
    Dim dsMemb As New DataSet

    Dim dtHotelAllTaxes As New DataTable
    Dim dtRoomTypeWisePromotions As New DataTable
    Dim dtReserv As New DataTable
    Dim dtGuest As New DataTable
    Dim dtReservationTabSaveSchema As New DataTable
    Dim dtReservation As DataTable
    Dim dtSummary As New DataTable
    Dim dtReservationTaxMap As New DataTable
    Dim dtReservationReceipt As New DataTable
    Dim dtPromotions As New DataTable  ' prepare all selected promtoion in it and pass it to db for saving
    Dim dtPromotionTotalDiscountValues As New DataTable
    Dim dtPromotionSelected As New DataTable  ' for temp getting data when select any promotion

    Dim roomtype As String
    Dim guest As String
    Dim Age As String
    Dim GstGender As String
    Dim Documentype As String
    Dim currenDetails As Integer
    Dim getColumnType As String = ""
    Public filterImage As String = "All files (*.*)|*.*"
    Dim fileLocation As String
    Dim FullNameWithExtension As String
    Dim str As String = String.Empty
    Dim documentNo As String

    Dim _selectedRoomTypeId As String = String.Empty
    Dim _selectedRoomNumber As String = String.Empty
    Dim _selectedPromotion As String = String.Empty
    Public _remarks As String
    Private _strRemarks As String
    Public _paidAmt As String
    Public _billAmt As String
    Dim GiftMsg As String = ""
    Public GVBasedAricleReturnList As New Dictionary(Of String, String)
    Dim CLPCardType, CLPCustomerId, clpCustomerMobileNo As String
    Dim _reservationNumber As String = ""
    Dim _selectedPromotionDiscount As String = String.Empty

    Dim MaxId As Integer = 0
    Dim SelectedRoomsCount As Integer = 0
    Dim SelectedRoomsCost As Integer = 0
    Dim bedNo As Integer
    Dim NoOfPeole As Integer
    Dim noOfNight As Integer
    Dim noOfDays As Integer
    Dim currentDetal As Integer
    Dim currentGuest As Integer
    Dim srno As Integer = 1
    Dim RoomTypePromotionCount = 0
    Dim AllPromotionTotalDiscountAmount As Integer = 0
    Dim IsFileUpload As Boolean = False
    Dim EditGuest As Boolean = False
    Dim IsArticleWiseKot As Boolean = False
    Dim IsCounterCopy As Boolean = False
    Dim IsFinalReceipt As Boolean = False
    Dim isCashierPromoSelected As Boolean
    Private IsTenderCheque As Boolean = False
    Dim uncheckedChang As Boolean = False
    Dim UpdateFlag As Boolean = False
    Dim isCheckIn As Boolean = False

    Protected controlList As New ArrayList
    Private ImagePatient As Image
    Private OFileDialog As OpenFileDialog

    Dim rowGuest As DataRow
    Dim newRow As DataRow
    Dim rowReservation As DataRow

    Private _dDueDate As Date
    Dim _chkInDate As DateTime
    Dim ISCallFromPayment As Boolean = False
#End Region
#Region "Properties"
    Public Shared _paymentTermId As String
    Public Shared Property PaymentTermId() As String
        Get
            Return _paymentTermId
        End Get
        Set(value As String)
            _paymentTermId = value
        End Set
    End Property
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
    Dim _VReservationId As String
    Public Property VReservationId As String
        Get
            Return _VReservationId
        End Get
        Set(value As String)
            _VReservationId = value
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
#Region "Class Events"

#End Region

#Region "GridsColumnSetting"
    'grdReservation
    Private Sub gridReservationCoumnSetting()
        Try
            grdReservation.AllowEditing = False
            If grdReservation.Cols.Count > 0 Then
                Dim DisplayColumns As String = "Sel,RoomNo,Roomtype,Ameneties,Cost,Tax,Bed"
                Dim ColumnList = DisplayColumns.ToUpper().Split(",")
                For colIndex = 0 To grdReservation.Cols.Count - 1 Step 1
                    If ColumnList.Contains(grdReservation.Cols(colIndex).Name.ToUpper()) Then
                        grdReservation.Cols(colIndex).Visible = True
                    Else
                        grdReservation.Cols(colIndex).Visible = False
                    End If
                    grdReservation.Cols(colIndex).AllowEditing = False
                Next
                grdReservation.Cols("Sel").DataType = Type.GetType("System.Boolean")
                grdReservation.Cols("Sel").Caption = ""
                grdReservation.Cols("Sel").AllowEditing = True
                grdReservation.Cols("RoomNo").Caption = "Room No"
                grdReservation.Cols("Roomtype").Caption = "Room type"
                grdReservation.Cols("Ameneties").Caption = "Ameneties"
                grdReservation.Cols("Cost").Caption = "Cost"
                grdReservation.Cols("Tax").Caption = "Tax"
                grdReservation.Cols("Bed").Caption = "Bed"
                grdReservation.Cols("Sel").Width = 40
                grdReservation.Cols("RoomNo").Width = 150
                grdReservation.Cols("Roomtype").Width = 142
                grdReservation.Cols("Ameneties").Width = 230
                grdReservation.Cols("Cost").Width = 140
                grdReservation.Cols("Bed").Width = 230
                Me.grdReservation.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
                Me.grdReservation.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
            End If
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

            grdGuestDetails.Cols("Gender").Width = 100
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
        ' grdReservation.Clear()
        'code added for issue id 1670 by aditya 30-05-2017
        checkIndate = cmdChkInDate.Value
        checkOutdate = cmdCheckoutDate.Value

        If cmdChkInDate.Value > cmdCheckoutDate.Value Then
            ShowMessage(" Check-Out Date Should not be less than Check-In Date", "Information")
            cmdChkInDate.Focus()
            Exit Sub
        End If
        grdReservation.Clear()
        txtCRoomSelected.Text = 0
        txtCTotalCostAmount.Text = 0

        '  checkIndate = cmdChkInDate.Value
        ' checkOutdate = cmdCheckoutDate.Value
        roomtype = cmbRoomType.SelectedValue
        bedNo = CmbBed.SelectedValue
        NoOfPeole = cmbNoOfBed.SelectedValue

        ''   Call bindRoomReservation(checkIndate.ToString("dd-MM-yyyy"), checkOutdate.ToString("dd-MM-yyyy"), roomtype, bedNo, NoOfPeole)  ' added by roshan
        Call bindRoomReservation(CDate(checkIndate), CDate(checkOutdate), roomtype, bedNo, NoOfPeole)

        gridReservationDetailsSetting()
    End Sub

    Private Sub frmHostEditCheckinReservation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Size = New System.Drawing.Size(1258, 670)
            DefaultTheme()
            tabReservation.TabStyle = C1.Win.C1Command.TabStyleEnum.Classic
            'Me.Location = New Point(32, 44)
            btnPreviousTab.Visible = False
            btnSaveAndFinish.Enabled = False
            btnSaveAndCheckIn.Enabled = False
            btnPreviousTab.Enabled = False
            tpCreateDetails.Show()
            dsMainHostEdit.Clear()
            dsMainHostEdit = objHotelreservation.HOSTGetStruc(VReservationId)
            LoadReservationData()
            gridReservationDetailsSetting()
            Dim dCheckInDate As Date
            Dim dCheckOutDate As Date
            dCheckInDate = Date.Parse(dsMainHostEdit.Tables("Host_Reservation").Rows(0)("checkInDateTime"))
            dCheckOutDate = Date.Parse(dsMainHostEdit.Tables("Host_Reservation").Rows(0)("checkOutDateTime"))
            dtHotelAllTaxes = objHotelreservation.GetHotelTax(clsAdmin.SiteCode, "RES")
            cmdChkInDate.Value = dCheckInDate
            cmdCheckoutDate.Value = dCheckOutDate
            Dim dtRoomType As DataTable
            Dim dtBedType As DataTable
            Dim dtNoOfBedType As DataTable
            Dim dtDocumentType As DataTable
            dtRoomType = objHotelreservation.GetRoomType(clsAdmin.SiteCode)
            dtBedType = objHotelreservation.GetBedType(clsAdmin.SiteCode)
            dtNoOfBedType = objHotelreservation.GetNoOfBedType(clsAdmin.SiteCode)
            dtDocumentType = objHotelreservation.GetDocumentType(clsAdmin.SiteCode)
            dsCombo = objCustm.GetComboDataSet
            BindComboBoxes(dsCombo.Tables("GenderTab"), cmbGender)
            BindComboBoxes(dtRoomType, cmbRoomType)
            BindComboBoxes(dtBedType, CmbBed)
            BindComboBoxes(dtNoOfBedType, cmbNoOfBed)
            BindComboBoxes(dtDocumentType, cmdDocumentType)
            cmbRoomType.SelectedValue = 10
            CmbBed.SelectedIndex = 0
            If True Then
                cmbNoOfBed.SelectedIndex = 0
            ElseIf True Then
            ElseIf True Then

            End If

            '' Create -First TAB

            PrepareDataTable()
            Dim dt As DataTable = dsMainHostEdit.Tables("Host_ReservationRoomTypePromotionMap").Copy()
            For Each dr As DataRow In dt.Rows
                Dim drPRO As DataRow
                drPRO = dtPromotionTotalDiscountValues.NewRow()
                Dim rmT As DataTable = objHotelreservation.GetMstSiteRoomTypeMap(dr("mstSiteRoomTypeMap").ToString())
                drPRO("RoomTypeId") = Convert.ToInt32(rmT(0)("mstRoomTypeID"))
                drPRO("TotalPromotionCost") = dr("DiscountAmount")
                dtPromotionTotalDiscountValues.Rows.Add(drPRO)
                dtPromotionTotalDiscountValues.AcceptChanges()
            Next

            dtPromotions.Clear()
            Dim dtPromotionSelectedTemp As DataTable = objHotelreservation.GetSelectedPromotionByReservationId(VReservationId)
            For Each drRow As DataRow In dtPromotionSelectedTemp.Rows
                Dim drPromoSelected As DataRow
                Dim dd = dtPromotions.Select("RoomTypeId='" + drRow("mstRoomTypeId").ToString() + "' and mstPromotionID='" + drRow("mstPromotionID").ToString() + "'")
                If dd.Length > 0 Then
                Else
                    drPromoSelected = dtPromotions.NewRow()
                    drPromoSelected("Selects") = True
                    drPromoSelected("reservationRoomTypePromotionMapID") = drRow("reservationRoomTypePromotionMapID")
                    drPromoSelected("reservationRoomMapID") = drRow("reservationRoomMapID")
                    drPromoSelected("RoomTypeId") = drRow("mstRoomTypeId")
                    drPromoSelected("mstSiteRoomTypeMap") = drRow("mstSiteRoomTypeMap")
                    drPromoSelected("mstPromotionID") = drRow("mstPromotionID")
                    drPromoSelected("mstPromotionName") = drRow("PromotionName")
                    drPromoSelected("discountInPercent") = drRow("discountInPercent")
                    drPromoSelected("discountAmount") = drRow("discountAmount")
                    drPromoSelected("Rate") = drRow("Rate")
                    drPromoSelected("RecordStatus") = "Updated"
                    drPromoSelected("ReservationId") = drRow("ReservationId")
                    dtPromotions.Rows.Add(drPromoSelected)
                End If
            Next

            'dtPromotionSelected.Clear()
            'Dim dtPromotionSelectedTemp As DataTable = objHotelreservation.GetSelectedPromotionByReservationId(VReservationId)
            'For Each drRow As DataRow In dtPromotionSelectedTemp.Rows
            '    Dim drPromoSelected As DataRow
            '    Dim dd = dtPromotionSelected.Select("RoomTypeId='" + drRow("mstRoomTypeId").ToString() + "' and mstPromotionID='" + drRow("mstPromotionID").ToString() + "'")
            '    If dd.Length > 0 Then
            '    Else
            '        drPromoSelected = dtPromotionSelected.NewRow()
            '        drPromoSelected("Selects") = True
            '        drPromoSelected("reservationRoomTypePromotionMapID") = drRow("reservationRoomTypePromotionMapID")
            '        drPromoSelected("reservationRoomMapID") = drRow("reservationRoomMapID")
            '        drPromoSelected("RoomTypeId") = drRow("mstRoomTypeId")
            '        drPromoSelected("mstSiteRoomTypeMap") = drRow("mstSiteRoomTypeMap")
            '        drPromoSelected("mstPromotionID") = drRow("mstPromotionID")
            '        drPromoSelected("mstPromotionName") = drRow("PromotionName")
            '        drPromoSelected("discountInPercent") = drRow("discountInPercent")
            '        drPromoSelected("discountAmount") = drRow("discountAmount")
            '        drPromoSelected("Rate") = drRow("Rate")
            '        drPromoSelected("RecordStatus") = "Updated"
            '        drPromoSelected("ReservationId") = drRow("ReservationId")
            '        dtPromotionSelected.Rows.Add(drPromoSelected)
            '    End If
            'Next

            cmdChkInDate.Enabled = False
            cmbRoomType.Enabled = False
            CmbBed.Enabled = False
            cmbNoOfBed.Enabled = False

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
        dtPromotionTotalDiscountValues = objHotelreservation.GetPromotionTotalDisountSchema()
        dtPromotionTotalDiscountValues.Clear()
        dtPromotionSelected = dtPromotions.Copy()
        dtPromotionSelected.Clear()
    End Sub

    Private Sub grdReservation_AfterEdit(sender As Object, e As RowColEventArgs) Handles grdReservation.AfterEdit
        Try
            ''SelectedRoomsCost = 0
            ''SelectedRoomsCount = 0
            'Dim SelectedCount As Integer = SelectedRoomsCount
            'Dim SelectedCost As Integer = SelectedRoomsCost
            'Dim RoomRent As Integer = 0
            'Dim tempRoomTypeId As String = ""
            'Dim tempRoomNumber As String = ""
            'For index = 1 To grdReservation.Rows.Count - 1
            '    Dim pertucularRoomTypeCount = 0
            '    Dim PreviousRoomTypeId As String = ""
            '    If grdReservation.Rows(index)(0) = True Then
            '        SelectedCount = SelectedCount + 1
            '        RoomRent = RoomRent + grdReservation.Rows(index)("Cost")
            '        tempRoomTypeId = grdReservation.Rows(index)("RoomTypeId")
            '        tempRoomNumber = grdReservation.Rows(index)("RoomNumber")
            '        SelectedRoomsCost = SelectedRoomsCost + grdReservation.Rows(index)("Cost")

            '        SelectedRoomsCount = SelectedRoomsCount + 1
            '        pertucularRoomTypeCount = pertucularRoomTypeCount + 1
            '        UpdateRoomTypeCount(tempRoomTypeId, tempRoomNumber, pertucularRoomTypeCount, False)  'add room number
            '    Else
            '        tempRoomTypeId = grdReservation.Rows(index)("RoomTypeId")
            '        tempRoomNumber = grdReservation.Rows(index)("RoomNumber")
            '        SelectedRoomsCount = SelectedRoomsCount - 1
            '        UpdateRoomTypeCount(tempRoomTypeId, tempRoomNumber, SelectedRoomsCount, True)  ' remove room number
            '    End If
            'Next
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
                txtCTotalCostAmount.Text = SelectedRoomsCost
                Dim OldRoomCount = dsMainHostEdit.Tables("Host_ReservationRoomMap").Rows.Count
                Dim NewSelectedRoomCount = SelectedRoomsCount - OldRoomCount
                txtCNewRoomSelected.Text = NewSelectedRoomCount
            Else
                txtCRoomSelected.Text = "0"
                txtCTotalCostAmount.Text = "0"
                txtCNewRoomSelected.Text = "0"
            End If

            'txtCRoomSelected.Text = SelectedCount
            'txtCTotalCostAmount.Text = RoomRent
        Catch ex As Exception

        End Try
    End Sub
#End Region
    '"--end Events"
#Region "Functions"

    Public Sub LoadReservationData(Optional ByVal strVReservationId As String = "")
        Try


            ' Call bindRoomReservation(Now.Date, Now.Date, roomtype, bedNo, NoOfPeole)
            dtRoomTypeRoomsCount = objHotelreservation.GetRoomTypeWiseRoomCount()
            'Call bindRoomReservation(strVReservationId) ' today
            Dim _dtcheckInDate As Date = dsMainHostEdit.Tables("Host_Reservation").Rows(0)("checkIndateTime")
            Dim _dtcheckOutDate As Date = dsMainHostEdit.Tables("Host_Reservation").Rows(0)("checkOutdateTime")
            Call bindRoomReservation(_dtcheckInDate, _dtcheckOutDate, cmbRoomType.SelectedValue, CmbBed.SelectedValue, cmbNoOfBed.SelectedValue, VReservationId)

            cmdChkInDate.ReadOnly = False
            'cmdChkInDate.Text = dsMainHostEdit.Tables("Host_Reservation").Rows(0)("checkIndateTime").ToString
            'cmdCheckoutDate.Text = dsMainHostEdit.Tables("Host_Reservation").Rows(0)("checkOutdateTime")
            txtCRoomSelected.TextDetached = True
            txtCTotalCostAmount.TextDetached = True
            txtCNewRoomSelected.TextDetached = True
            txtCRoomSelected.Text = dsMainHostEdit.Tables("Host_Reservation").Rows(0)("totalNoOfRooms")
            'txtRTotalCost.Text = dsMainHostEdit.Tables("Host_Reservation").Rows(0)("totalGrossAmount")
            txtCTotalCostAmount.Text = dsMainHostEdit.Tables("Host_ReservationRoomMap").Compute("Sum(rate)", "")
            'Dim promotiontotalTax As Decimal = Convert.ToDecimal(dtHotelAllTaxes.Compute("Sum(Tax)", ""))
            txtCNewRoomSelected.Text = "0"
        Catch ex As Exception
            LogException(ex)
        End Try

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

    Private Sub BindRoomTypeDate()
        Try
            Dim dt As DataTable
            cmbRoomType.Text = "ALL"
            dt = objHotelreservation.GetRoomType(clsAdmin.SiteCode)
            If Not dt Is Nothing And dt.Rows.Count > 0 Then
                cmbRoomType.DataSource = dt
                cmbRoomType.DisplayMember = dt.Columns("Name").ToString()
                cmbRoomType.ValueMember = dt.Columns("mstRoomTypeId").ToString()
                cmbRoomType.ExtendRightColumn = True
                For Each r As C1.Win.C1List.Split In cmbRoomType.Splits
                    Dim i As Integer
                    For i = 0 To r.DisplayColumns.Count - 1
                        If r.DisplayColumns(i).Name <> cmbRoomType.DisplayMember Then
                            r.DisplayColumns(i).Visible = False
                        End If
                    Next
                Next
                cmbRoomType.SelectedIndex = -1
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindBedType()
        Try
            Dim dt As DataTable
            CmbBed.Text = ""
            dt = objHotelreservation.GetBedType(clsAdmin.SiteCode)
            If Not dt Is Nothing And dt.Rows.Count > 0 Then
                CmbBed.DataSource = dt
                CmbBed.DisplayMember = dt.Columns("Name").ToString()
                CmbBed.ValueMember = dt.Columns("mstBedTypeId").ToString()
                CmbBed.ExtendRightColumn = True
                For Each r As C1.Win.C1List.Split In CmbBed.Splits
                    Dim i As Integer
                    For i = 0 To r.DisplayColumns.Count - 1
                        If r.DisplayColumns(i).Name <> CmbBed.DisplayMember Then
                            r.DisplayColumns(i).Visible = False
                        End If
                    Next
                Next
                CmbBed.SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try

    End Sub

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


    Private Sub bindRoomReservation(Checkin As DateTime, checkout As DateTime, Optional ByVal Roomtype As String = Nothing, Optional ByVal bedNo As String = "", Optional ByVal noOfPeople As String = "", Optional ReservationId As String = "") ''checkout As DateTime,
        Try
            Dim year As String = Checkin.Year
            dtReservation = objHotelreservation.GetReservationRooms(clsAdmin.SiteCode, Checkin, year, Roomtype, bedNo, noOfPeople, checkout, ReservationId) ''checkout.ToString("dd-MM-yyyy"),
            'Code is added by irfan on 04/04/2018 for hotel reservation.
            Dim i As Integer
            For i = 0 To dtReservation.Rows.Count - 1
                If dtReservation.Rows(i)("Selects") = False Then
                    dtReservation.Rows.RemoveAt(i)
                End If
            Next
            Dim DtAllRoomsDetails As DataTable = objHotelreservation.GetReservationRooms(clsAdmin.SiteCode, Checkin, year, "0", bedNo, noOfPeople, checkout) ''checkout.ToString("dd-MM-yyyy"),
            If dtReservation IsNot Nothing Then
                If DtAllRoomsDetails.Rows.Count > 0 Then
                    For Each drAllRoomDetails In DtAllRoomsDetails.Rows
                        Dim drMerg As DataRow
                        drMerg = dtReservation.NewRow()
                        drMerg("Selects") = drAllRoomDetails("Selects")
                        ' drMerg("ReservationId") = drAllRoomDetails("ReservationId").ToString()
                        drMerg("MstRoomId") = drAllRoomDetails("MstRoomId")
                        drMerg("RoomNumber") = drAllRoomDetails("RoomNumber")
                        drMerg("RoomType") = drAllRoomDetails("RoomType")
                        drMerg("RoomTypeId") = drAllRoomDetails("RoomTypeId")
                        drMerg("Amenities") = drAllRoomDetails("Amenities")
                        drMerg("Cost") = drAllRoomDetails("Cost")
                        drMerg("TAX") = drAllRoomDetails("TAX")
                        drMerg("BED") = drAllRoomDetails("BED")
                        '     drMerg("ReservationStatusId") = drAllRoomDetails("ReservationStatusId")
                        dtReservation.Rows.Add(drMerg)
                    Next
                End If

                grdReservation.DataSource = dtReservation.DefaultView
                'AddReservationDetailsToGrid(dtReservation)
                dtReserv = dtReservation
                chkReserved.Checked = True
                SelectedRoomsCost = 0
                SelectedRoomsCount = 0
                Dim tempRoomTypeId As String = ""
                Dim tempRoomNumber As String = ""
                For index = 1 To grdReservation.Rows.Count - 1
                    If grdReservation.Rows(index)(0) = True Then
                        SelectedRoomsCount = SelectedRoomsCount + 1
                        SelectedRoomsCost = SelectedRoomsCost + grdReservation.Rows(index)("Cost")
                        tempRoomTypeId = grdReservation.Rows(index)("RoomTypeId")
                        tempRoomNumber = grdReservation.Rows(index)("RoomNumber")
                        UpdateRoomTypeCount(grdReservation.Rows(index)("RoomTypeId"), grdReservation.Rows(index)("RoomNumber"), SelectedRoomsCount)
                    End If
                Next

            Else
                ShowMessage(getValueByKey("Room not Available"), "Information - " & "Information")
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub gridReservationDetailsSetting()
        Try
            Me.grdReservation.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
            Me.grdReservation.Styles.Fixed.BackColor = Color.FromArgb(208, 208, 208)
            Me.grdReservation.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
            Me.grdReservation.Styles.Fixed.Font = New Font("Callibri", 10, FontStyle.Bold)
            Me.grdReservation.Styles.Focus.Font = New Font("Callibri", 10, FontStyle.Bold)
            Me.grdReservation.Styles.Highlight.BackColor = Color.FromArgb(252, 252, 252)
            Me.grdReservation.Focus()

            grdReservation.DataSource = dtReserv

            grdReservation.Cols("Selects").DataType = Type.GetType("System.Boolean")
            grdReservation.Cols("Selects").AllowEditing = True
            grdReservation.Cols("Selects").Caption = "Select"
            grdReservation.Cols("Selects").Width = 75

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

            grdReservation.Cols("RoomNumber").Width = 100
            grdReservation.Cols("RoomNumber").DataType = Type.GetType("System.String")
            grdReservation.Cols("RoomNumber").AllowEditing = False
            grdReservation.Cols("RoomNumber").Name = "RoomNumber"
            grdReservation.Cols("RoomNumber").TextAlign = TextAlignEnum.LeftCenter

            grdReservation.Cols("RoomType").Width = 125
            grdReservation.Cols("Roomtype").AllowEditing = False
            grdReservation.Cols("RoomType").DataType = Type.GetType("System.String")

            grdReservation.Cols("RoomType").Name = "RoomType"
            grdReservation.Cols("RoomType").TextAlign = TextAlignEnum.LeftCenter

            grdReservation.Cols("Amenities").Width = 550
            grdReservation.Cols("Amenities").AllowEditing = False
            grdReservation.Cols("Amenities").DataType = Type.GetType("System.String")

            grdReservation.Cols("Amenities").Name = "Amenities"
            grdReservation.Cols("Amenities").TextAlign = TextAlignEnum.LeftCenter

            grdReservation.Cols("Cost").Width = 200
            grdReservation.Cols("Cost").DataType = Type.GetType("System.String")
            grdReservation.Cols("Cost").AllowEditing = False
            grdReservation.Cols("Cost").Name = "Cost"
            grdReservation.Cols("Cost").TextAlign = TextAlignEnum.LeftCenter

            grdReservation.Cols("Tax").Width = 50
            grdReservation.Cols("Tax").AllowEditing = False
            grdReservation.Cols("Tax").DataType = Type.GetType("System.Decimal")
            grdReservation.Cols("Tax").Format = "0"
            grdReservation.Cols("Tax").Name = "Tax"
            grdReservation.Cols("Tax").TextAlign = TextAlignEnum.LeftCenter
            grdReservation.Cols("Tax").Visible = False

            grdReservation.Cols("BED").Width = 150
            grdReservation.Cols("BED").DataType = Type.GetType("System.String")
            grdReservation.Cols("BED").AllowEditing = False
            grdReservation.Cols("BED").Name = "BED"
            grdReservation.Cols("BED").TextAlign = TextAlignEnum.LeftCenter
            grdReservation.Cols("ReservationId").Visible = False
            grdReservation.Cols("ReservationStatusId").Visible = False

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
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

    Sub CopyAfterCostPromotion(ByRef Ddt As DataTable, ByVal _FinaliseCostAfterPromo As String, ByVal _mstRoomTypeID As String)
        For T As Integer = 0 To CInt(Ddt.Rows.Count - 1)
            '    If Ddt.Rows(T)("FinaliseCostAfterPromo") = "0" AndAlso Ddt.Rows(T)("mstRoomTypeID").ToString() = _mstRoomTypeID Then
            If Ddt.Rows(T)("mstRoomTypeID").ToString() = _mstRoomTypeID Then
                Ddt.Rows(T)("FinaliseCostAfterPromo") = _FinaliseCostAfterPromo
            Else
                Ddt.Rows(T)("FinaliseCostAfterPromo") = "0"
            End If
        Next
    End Sub
    'added by khusrao on 15-02-2017 for promotion tab amount details and calculation
    Sub PromotionAmountDetails(ByVal strSelectedRoomsCost As String, ByVal strpromotionsTotalDiscount As String)
        Try
            Dim SelectedRoomsCost As Decimal = Convert.ToDecimal(strSelectedRoomsCost)
            Dim promotionsTotalDiscount As Decimal = Convert.ToDecimal(strpromotionsTotalDiscount)
            Dim promotionFinalCost As Decimal
            Dim _calculateTax As Decimal
            '  Dim promotiontotalTax As Decimal = Convert.ToDecimal(dtHotelAllTaxes.Select("Sum(Tax)").FirstOrDefault())
            'dtChildTable.Compute("Sum(Price)", "");
            Dim promotiontotalTax As Decimal
            If dtHotelAllTaxes.Rows.Count > 0 Then
                promotiontotalTax = Convert.ToDecimal(dtHotelAllTaxes.Compute("Sum(Tax)", ""))
            End If
            _calculateTax = ((SelectedRoomsCost - promotionsTotalDiscount) / 100) * promotiontotalTax
            promotionFinalCost = (SelectedRoomsCost - promotionsTotalDiscount) + _calculateTax

            txtPromoDiscount.TextDetached = True
            txtPromoTax.TextDetached = True
            txtPromoCost.TextDetached = True
            txtPromoDiscount.Text = Math.Round(promotionsTotalDiscount, 2)
            txtPromoTax.Text = Math.Round(_calculateTax, 2)
            txtPromoCost.Text = Math.Round(promotionFinalCost, 2)
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
                    dtGuest.Rows(currentGuest - 1)("GuestImage") = objHotelreservation.ImageToByteArray(ImagePatient)
                    dtGuest.Rows(currentGuest - 1)("supportedDocumentDetails") = txtdocumentNo.Text
                    dtGuest.Rows(currentGuest - 1)("RecordStatus") = objHotelreservation.enumRecordStatus.Updated.ToString()
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
                    documentNo = objcomm.getDocumentNo("Customer Loyalty", clsAdmin.SiteCode)
                    documentNo = documentNo + 1
                    Dim otherCharacters = "CLS" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3, 3)
                    Dim cardno As String = objcomm.GenDocNo(otherCharacters, 15, documentNo)
                    Dim clpid As String = objcomm.GetProgramId(clsAdmin.SiteCode)
                    '============================================================'
                    rowGuest = dtGuest.NewRow()
                    rowGuest("SrNo") = dtGuest.Rows.Count + 1
                    rowGuest("FirstName") = txtGuestFirstName.Text
                    rowGuest("MiddleName") = txtGuestMiddleName.Text
                    rowGuest("LastName") = txtGuestLastName.Text
                    rowGuest("MobileNumber") = txtGuestMobileNumber.Text
                    rowGuest("EmailID") = txtGuestEmailId.Text
                    rowGuest("Age") = txtAge.Text
                    rowGuest("Gender") = cmbGender.Text
                    rowGuest("DocumentType") = cmdDocumentType.SelectedText
                    rowGuest("DocumentTypeId") = cmdDocumentType.SelectedValue
                    rowGuest("PrimaryGuest") = IIf(chkBxPrimaryGuest.Checked, True, False)
                    rowGuest("GuestImage") = objHotelreservation.ImageToByteArray(ImagePatient)
                    rowGuest("RecordStatus") = objHotelreservation.enumRecordStatus.Inserted.ToString()
                    rowGuest("CardNo") = cardno
                    rowGuest("ClpProgramId") = clpid
                    rowGuest("supportedDocumentDetails") = txtdocumentNo.Text
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
            ImagePatient = Image.FromFile(OFileDialog.FileName) ' byte convert 
            fileLocation = OFileDialog.FileName
            Dim files() As String
            files = fileLocation.Split(".")
            Dim fileInfo As New FileInfo(fileLocation)
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
        End If
    End Sub


    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnGuestClear.Click
        txtGuestFirstName.Text = String.Empty
        txtGuestMiddleName.Text = String.Empty
        txtGuestLastName.Text = String.Empty
        txtGuestMobileNumber.Text = String.Empty
        txtGuestEmailId.Text = String.Empty
        txtAge.Text = ""
        txtdocumentNo.Text = ""
        cmdDocumentType.SelectedValue = String.Empty
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
            txtdocumentNo.Text = strDocumentNo
            cmbGender.Text = strGender
            cmdDocumentType.Text = strDoctumentType
            chkBxPrimaryGuest.Checked = IIf(strPrimaryGuest, True, False)
            EditGuest = True
            btnGuestAdd.Text = "Update"
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtGuestMobileNumber_KeyDown(sender As Object, e As KeyEventArgs) Handles txtGuestMobileNumber.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim dt = objHotelreservation.GetClpCustomer(txtGuestMobileNumber.Text, True)
            If dt.Rows.Count > 0 Then
                txtGuestFirstName.Text = dt.Rows(0)("guestFirstName").ToString()
                txtGuestMiddleName.Text = dt.Rows(0)("guestMiddleName").ToString()
                txtGuestLastName.Text = dt.Rows(0)("guestLastName").ToString()
                txtGuestEmailId.Text = dt.Rows(0)("guestEmailId").ToString()
                txtAge.Text = dt.Rows(0)("guestAgeInYears").ToString()
                cmbGender.Text = dt.Rows(0)("guestGender").ToString()
                cmdDocumentType.SelectedText = dt.Rows(0)("supportedDocumentDetails_1").ToString()
                txtdocumentNo.Text = dt.Rows(0)("supportedDocumentDetails").ToString()
                Dim _selectecIndex As Integer = dt.Rows(0)("mstSupportedDocumentTypeID_1").ToString()

                cmdDocumentType.SelectedIndex = _selectecIndex - 1
                chkBxPrimaryGuest.Checked = True
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
    Private Sub BindDocumentType()
        Try
            Dim dt As DataTable
            cmdDocumentType.Text = ""
            dt = objHotelreservation.GetDocumentType(clsAdmin.SiteCode)
            If Not dt Is Nothing And dt.Rows.Count > 0 Then
                cmdDocumentType.DataSource = dt
                cmdDocumentType.DisplayMember = dt.Columns("DocumentName").ToString()
                cmdDocumentType.ValueMember = dt.Columns("mstDocumentId").ToString()
                cmdDocumentType.ExtendRightColumn = True
                For Each r As C1.Win.C1List.Split In cmdDocumentType.Splits
                    Dim i As Integer
                    For i = 0 To r.DisplayColumns.Count - 1
                        If r.DisplayColumns(i).Name <> cmdDocumentType.DisplayMember Then
                            r.DisplayColumns(i).Visible = False
                        End If
                    Next
                Next
                cmdDocumentType.SelectedIndex = 1
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function ValidatedGuestDetails() As Boolean
        Try

            If txtGuestFirstName.Text.Trim() = "" Then
                'ShowMessage(getValueByKey("SOC008"), "SOC008 - " & getValueByKey("CLAE04"))
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
        txtAge.Text = String.Empty
        cmbGender.Text = String.Empty
        txtdocumentNo.Text = ""
        cmdDocumentType.SelectedValue = String.Empty
        'cmdDocumentType.SelectedText = String.Empty
        chkBxPrimaryGuest.Checked = False
        btnGuestAdd.Text = "Add"
    End Function
#End Region
    ' "--end Functions"
#End Region
#Region "-----------------------------Summary Details Tab "

    Public Sub ReadonlySummaryData()
        'txtSumcheckin.ReadOnly = True
        'txtSumcheckout.ReadOnly = True
        'txtSumNumberOfAdult.ReadOnly = True
        'txtChildren.ReadOnly = True
        'txtNight.ReadOnly = True
        'txtSumDiscountAmount.ReadOnly = True
        'txtSumTotalTaxAmount.ReadOnly = True
        'txtSumFinalCost.ReadOnly = True
        'txtSumPhoneNo.ReadOnly = True
        'txtSumEmail.ReadOnly = True
        'txtSumGuestName.ReadOnly = True
        'txtSumPaidAmt.ReadOnly = True
        'txtSumPaymentRemaining.ReadOnly = True

    End Sub

    Public Sub SummaryInformation()
        Try
            txtSumcheckin.Text = checkIndate
            txtSumcheckout.Text = checkOutdate
            '  txtAdult.Text = txtAdult.Text.Trim
            'txtChildren.Text = txtChild.Text.Trim

            noOfNight = DateDiff(DateInterval.Day, checkIndate, checkOutdate)
            noOfDays = DateInterval.Day

            txtNight.Text = noOfNight.ToString()
            txtSumDiscountAmount.Text = txtPromoDiscount.Text.Trim
            txtSumFinalCost.Text = txtPromoCost.Text.Trim
            txtSumPaidAmt.Text = dsMainHostEdit.Tables("Host_reservation").Rows(0)("totalAmountPaid").ToString()
            txtSumTotalTaxAmount.Text = txtPromoTax.Text
            'If dsMainHostEdit.Tables("Host_reservation").Rows(0)("totalAmountPaid").ToString() = "0" Then
            '    txtSumPaymentRemaining.Text = txtSumFinalCost.Text
            'Else
            '    txtSumPaymentRemaining.Text = dsMainHostEdit.Tables("Host_reservation").Rows(0)("remainingAmountToPay").ToString()
            'End If
            txtSumPaymentRemaining.Text = txtPromoCost.Text - txtSumPaidAmt.Text
            For Each dr As DataRow In dtGuest.Select("PrimaryGuest=True")
                txtSumPhoneNo.Text = dr("MobileNumber")
                txtSumEmail.Text = dr("EmailId")
                txtSumGuestName.Text = dr("FirstName") & " " & dr("LastName")
            Next
            Dim Adult = dtGuest.Select("age>18")
            txtSumNumberOfAdult.Text = Adult.Count
            Dim Children = dtGuest.Select("age<18")
            txtChildren.Text = Children.Count
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
            ' gridSummaryDetails.Cols("RoomNumber").Format = "0"
            gridSummaryDetails.Cols("RoomNo").Name = "RoomNo"
            gridSummaryDetails.Cols("MstRoomId").Visible = False


            gridSummaryDetails.Cols("RoomType").Width = 100
            gridSummaryDetails.Cols("RoomType").DataType = Type.GetType("System.String")
            ' gridSummaryDetails.Cols("RoomType").Format = "0"
            gridSummaryDetails.Cols("RoomType").Name = "RoomType"
            ' gridSummaryDetails.Cols("RoomType").TextAlign = TextAlignEnum.LeftCenter

            gridSummaryDetails.Cols("Ameneties").Width = 455
            gridSummaryDetails.Cols("Ameneties").DataType = Type.GetType("System.String")
            ' gridSummaryDetails.Cols("Ameneties").Format = "0"
            gridSummaryDetails.Cols("Ameneties").Name = "Ameneties"
            '  gridSummaryDetails.Cols("Ameneties").TextAlign = TextAlignEnum.LeftCenter

            gridSummaryDetails.Cols("Promotions").Width = 100
            gridSummaryDetails.Cols("Promotions").DataType = Type.GetType("System.String")
            ' gridSummaryDetails.Cols("Promotions").Format = "0"
            gridSummaryDetails.Cols("Promotions").Name = "Promotions"
            ' gridSummaryDetails.Cols("Promotions").TextAlign = TextAlignEnum.LeftCenter

            gridSummaryDetails.Cols("Price").Width = 150 'Price
            gridSummaryDetails.Cols("Price").DataType = Type.GetType("System.Decimal")
            ' gridSummaryDetails.Cols("Price").Format = "0"
            gridSummaryDetails.Cols("Price").Name = "Price"
            gridSummaryDetails.Cols("Price").TextAlign = TextAlignEnum.LeftCenter

            gridSummaryDetails.Cols("Tax").Width = 50
            gridSummaryDetails.Cols("Tax").DataType = Type.GetType("System.Decimal")
            gridSummaryDetails.Cols("Tax").Format = "0"
            gridSummaryDetails.Cols("Tax").Name = "Tax"
            '  gridSummaryDetails.Cols("Tax").TextAlign = TextAlignEnum.LeftCenter

            gridSummaryDetails.Cols("Discount").Width = 100
            gridSummaryDetails.Cols("Discount").DataType = Type.GetType("System.Decimal")
            'gridSummaryDetails.Cols(Discount").Format = "0"
            gridSummaryDetails.Cols("Discount").Name = "Discount"
            gridSummaryDetails.Cols("Discount").TextAlign = TextAlignEnum.LeftCenter

            gridSummaryDetails.Cols("FinalCost").Width = 150
            gridSummaryDetails.Cols("FinalCost").DataType = Type.GetType("System.Decimal")
            gridSummaryDetails.Cols("FinalCost").Name = "FinalCost"
            gridSummaryDetails.Cols("FinalCost").TextAlign = TextAlignEnum.LeftCenter

            gridSummaryDetails.Cols("reservationRoomMapID").Visible = False
            gridSummaryDetails.Cols("reservationID").Visible = False
            gridSummaryDetails.Cols("Selected").Visible = False

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub


    Public Sub LoadSummaryInformationIntoGrid(ByVal dtRoomwisePromotion As DataTable) ''ByVal dtSummary As DataTable
        Try
            '  Dim tCost As String
            '  Dim tTax As String
            '     Dim TotalCost As String
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
                dtSummary.Rows.Add(rowReservation)

                'Dim st1 As String
                'Dim drResult As DataRow()
                '_selectedPromotion = ""

                'drResult = dtRoomwisePromotion.Select("RoomTypeId= '" + dr("RoomTypeId").ToString() + "'") ''dtPromotions.Select("mstRoomID= '" + dr("mstRoomID").ToString() + "' and mstRoomTypeId='" + dr("RoomTypeId").ToString() + "'")
                ' dtPromotions.ToString()
                'If drResult.Length > 0 Then
                '    For i = 0 To drResult.Length - 1
                '        _selectedPromotion = _selectedPromotion + "," + drResult(i)("PROMOTIONNAME").ToString()
                '    Next
                '    Dim _Promotion = _selectedPromotion.Substring(0, 1)
                '    If _Promotion = "," Then
                '        _selectedPromotion = (_selectedPromotion.Replace(",", ""))
                '    End If
                'End If
                'Dim dt As DataTable = dsMainHostEdit.Tables("Host_ReservationRoomMap")

                'rowReservation = dtSummary.NewRow
                ''   rowReservation("reservationRoomMapID") = dr("reservationRoomMapID")
                ''rowReservation("reservationRoomMapID") = dr("reservationRoomTypePromotionMapID")
                'rowReservation("reservationID") = dr("reservationID")
                'rowReservation("RoomNo") = dr("RoomNumber")
                'rowReservation("MstRoomId") = dr("MstRoomId")
                'rowReservation("RoomType") = dr("RoomType")
                'rowReservation("Ameneties") = dr("Amenities")
                'rowReservation("Price") = dr("Cost")

                'rowReservation("Tax") = If(dr("Tax") Is DBNull.Value, 0, dr("Tax"))
                'tCost = dr("Cost")
                ''                rowReservation("RecordStatus") = objHotelreservation.enumRecordStatus.Updated.ToString()

                'TotalCost = tCost + rowReservation("Tax").ToString

                'If drResult.Count > 0 Then
                '    rowReservation("Promotions") = Convert.ToString(drResult(0)("mstPromotionName"))
                '    rowReservation("Discount") = Convert.ToString(drResult(0)("discountInPercent"))
                '    rowReservation("FinalCost") = TotalCost - rowReservation("Discount")
                '    rowReservation("reservationRoomMapID") = drResult(0)("reservationRoomMapID")
                'End If
                'dtSummary.Rows.Add(rowReservation)
            Next
            gridSummaryDetailsSetting()
            SummaryInformation()
            ' ReadonlySummaryData()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

#End Region
#Region "-----------------------------Payment Details tab  "

    Private Sub UpdatePaymentPrevStru(ByRef dt As DataTable)
        Try
            dt.TableName = "MstRecieptType"
            'dt.Columns("CMRECPTLINENO").ColumnName = "SRNO"
            dt.Columns("receiptLineNo").ColumnName = "SRNO"
            dt.Columns("TENDERTYPECODE").ColumnName = "RECIEPTTYPECODE"
            'dt.Columns("RECIEPTTYPE").ColumnName = "RECIEPT"
            dt.Columns("TENDERHEADCODE").ColumnName = "RECIEPTTYPE"
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

    Private Sub UpdatePaymentDataSetStru(ByRef ds As DataSet, ByVal UpdateFlag As Boolean)
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


        Catch ex As Exception
            ShowMessage(getValueByKey("CM011"), "CM011 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in updating the payment data Structure", "Error")
        End Try
    End Sub

    Public Sub loadPaymentDetails()
        Try
            'txtAdvTotalDiscountAmount.Text = txtDiscountAmount.Text
            'txtAdvTotalTaxAmount.Text = txtTotalTaxAmount.Text
            'txtAdvFinalCost.Text = txtFinalCost.Text
            'txtAdvPaidAmount.Text = 0
            'txtAdvPaymentRemaining.Text = 0

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

            txtAdvTotalDiscountAmount.Text = txtSumDiscountAmount.Text
            txtAdvTotalTaxAmount.Text = txtSumTotalTaxAmount.Text
            txtAdvFinalCost.Text = txtSumFinalCost.Text
            txtAdvPaidAmount.Text = txtSumPaidAmt.Text
            txtAdvPaymentRemaining.Text = txtSumPaymentRemaining.Text


            txtAdvCheckin.Text = txtCheckedIn.Text
            txtAdvCheckout.Text = txtCheckedOut.Text
            txtAdvGuestName.Text = txtSumGuestName.Text
            txtAdvPhoneNo.Text = txtSumPhoneNo.Text
            txtAdvNoOfAdult.Text = txtSumNumberOfAdult.Text
            txtAdvEmail.Text = txtSumEmail.Text
            txtAdvNoOfNight.Text = txtNight.Text
            txtAdvNoOfChildren.Text = txtChildren.Text

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

            txtWPayDiscountAmot.Text = txtSumDiscountAmount.Text
            txtWPayTotalTaxAmount.Text = txtSumTotalTaxAmount.Text
            txtWPayFinalCost.Text = txtSumFinalCost.Text
            txtWPayPaidAmount.Text = txtSumPaidAmt.Text
            txtWPayRemainingPayment.Text = txtSumPaymentRemaining.Text

            txtWPayCheckin.Text = txtCheckedIn.Text
            txtWPayCheckout.Text = txtCheckedOut.Text
            txtWPayGuestName.Text = txtSumGuestName.Text
            txtWPayPhoneNo.Text = txtSumPhoneNo.Text
            txtWPayNoOfAdult.Text = txtSumNumberOfAdult.Text
            txtWPayEmail.Text = txtSumEmail.Text
            txtWPayNoOfNight.Text = txtNight.Text
            txtWPayNoOfChildren.Text = txtChildren.Text
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
            Dim _txtAdvPaidAmount As Decimal = Convert.ToDecimal(txtAdvPaidAmount.Text)
            If _txtAdvPaidAmount <> "0" Then
                ShowMessage("Amount already paid", "Information")
                Exit Sub
            End If
            ISCallFromPayment = True
            PrepareDataSave()
            Dim ds As DataSet
            Dim objPayment As frmNAcceptPayment
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
            Dim dt As DataTable = dsMainHostEdit.Tables("Host_ReservationReceipt").Copy()
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

                        If PrepareReceiptData(ds.Tables(0), dsMainHostEdit) = True Then
                            'error
                        End If
                        If SaveAndPrintReservation(False) = True Then
                            ShowMessage("Save Successfull for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
                        Else
                            ShowMessage("Save Failed for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
                        End If
                        Me.Close()
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
            End If
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
            Dim _txtAdvPaidAmount As Decimal = Convert.ToDecimal(txtAdvPaidAmount.Text)
            If _txtAdvPaidAmount <> "0" Then
                ShowMessage("Amount already paid", "Information")
                Exit Sub
            End If
            ISCallFromPayment = True
            PrepareDataSave()
            ' If IsTenderCash Then
            Dim objPaymentByCash As New frmNAcceptPaymentByCash
            objPaymentByCash.txtRemark.Text = _remarks
            objPaymentByCash._IsCashierPromoSelected = isCashierPromoSelected
            objPaymentByCash.TotalBillAmount = CDbl(txtAdvFinalCost.Text)
            objPaymentByCash.ShowDialog()
            If Not (objPaymentByCash.IsCancelAcceptPayment) Then
                If Not objPaymentByCash.ReciptTotalAmount Is Nothing And objPaymentByCash.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                    Dim ds As DataSet = objPaymentByCash.ReciptTotalAmount
                    _billAmt = objPaymentByCash.TotalBillAmount
                    _paidAmt = objPaymentByCash.TotalCustomerPadiAmount
                    txtAdvPaymentRemaining.Text = _paidAmt - _billAmt
                    objPaymentByCash.Close()
                    'If Not ds Is Nothing Then
                    UpdatePaymentDataSetStru(ds, UpdateFlag)
                    ' cmdHold.Enabled = False
                    If UpdateFlag = False Then
                        Dim dt As New DataTable
                        dt = ds.Tables("Host_ReservationReceipt").Copy()
                        dt.Columns("TenderHeadCode").ColumnName = "Reciepttypecode"
                        'cmdLoyalty_Click(sender, e, dt)
                        dt.Dispose()
                    End If
                    If objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeSave Then
                        If PrepareReceiptData(ds.Tables(0), dsMainHostEdit) = True Then
                            'error
                        End If
                        Dim ReservationId = dsMainHostEdit.Tables("Host_Reservation").Rows(0)("reservationID").ToString()
                        Dim ReservationRNumber = dsMainHostEdit.Tables("Host_Reservation").Rows(0)("reservationNumber").ToString()
                        If SaveAndPrintReservation(False) = True Then
                            cmdCash.Enabled = False 'vipin
                            cmdCard.Enabled = False
                            cmdCheque.Enabled = False
                            cmdPayment.Enabled = False
                            ShowMessage("Save Successfull for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
                        Else
                            ShowMessage("Save Failed for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
                        End If
                        '  Me.Close()
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
            Dim _txtAdvPaidAmount As Decimal = Convert.ToDecimal(txtAdvPaidAmount.Text)
            If _txtAdvPaidAmount <> "0" Then
                ShowMessage("Amount already paid", "Information")
                Exit Sub
            End If
            ISCallFromPayment = True
            PrepareDataSave()
            ' If IsTenderCreditCard Then
            If UpdateFlag = False Then
                'If PromotionCleared = False Then

                'End If
                'cmdLoyalty_Click(sender, e)
            End If
            Dim objPayment As New frmNAcceptPaymentByCard()
            objPayment.TotalBillAmount = CDbl(txtAdvFinalCost.Text)
            'objPayment.cboCurrency.SelectedIndex = 1
            objPayment.ShowDialog()
            Dim selectedTenderName As String = objPayment.SelectedTenderName
            Dim strCardTenderCode As String = objPayment.CardTenderCode

            objPayment.Close()
            If Not (objPayment.IsCancelAcceptPayment) Then
                If Not objPayment.ReciptTotalAmount Is Nothing And objPayment.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                    Dim ds As DataSet = objPayment.ReciptTotalAmount
                    'Dim ds As New DataSet()
                    'ds.Tables.Add(dt)
                    txtAdvPaymentRemaining.Text = _paidAmt - _billAmt
                    objPayment.Close()
                    'If Not ds Is Nothing Then
                    UpdatePaymentDataSetStru(ds, UpdateFlag)

                    If UpdateFlag = False Then

                        Dim dt As New DataTable
                        dt = ds.Tables("Host_ReservationReceipt").Copy()
                        dt.Columns("TenderHeadCode").ColumnName = "Reciepttypecode"
                        ' cmdLoyalty_Click(sender, e, dt)
                        dt.Dispose()
                    End If
                    If objPayment.Action = My.Resources.AcceptPaymentActionTypeSave Then

                        If PrepareReceiptData(ds.Tables(0), dsMainHostEdit) = True Then
                            'error
                        End If

                        Dim ReservationId = dsMainHostEdit.Tables("Host_Reservation").Rows(0)("reservationID").ToString()
                        Dim ReservationRNumber = dsMainHostEdit.Tables("Host_Reservation").Rows(0)("reservationNumber").ToString()
                        ReservationDate = System.DateTime.Now.ToString("dd/MM/yyyy")
                        If SaveAndPrintReservation(False) = True Then
                            cmdCash.Enabled = False 'vipin
                            cmdCard.Enabled = False
                            cmdCheque.Enabled = False
                            cmdPayment.Enabled = False
                            ShowMessage("Save Successfull for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
                        Else
                            ShowMessage("Save Failed for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
                        End If
                        '  Me.Close()
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
            Dim _txtAdvPaidAmount As Decimal = Convert.ToDecimal(txtAdvPaidAmount.Text)
            If _txtAdvPaidAmount <> "0" Then
                ShowMessage("Amount already paid", "Information")
                Exit Sub
            End If
            ISCallFromPayment = True
            PrepareDataSave()
            Dim objCheck As New frmNCheckPayment
            objCheck.BillAmount = CDbl(txtAdvFinalCost.Text)
            objCheck.ShowDialog()
            If objCheck.IsCancelAcceptPayment = False Then
                If Not objCheck.ReciptTotalAmount Is Nothing And objCheck.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                    Dim ds As DataSet = objCheck.ReciptTotalAmount
                    'Dim ds As New DataSet()
                    'ds.Tables.Add(dt)
                    _paidAmt = objCheck.CollectAmount
                    txtAdvPaymentRemaining.Text = txtAdvFinalCost.Text - _paidAmt
                    txtAdvPaidAmount.Text = _paidAmt
                    objCheck.Close()
                    'If Not ds Is Nothing Then
                    UpdatePaymentDataSetStru(ds, UpdateFlag)
                    If UpdateFlag = False Then

                        Dim dt As New DataTable
                        dt = ds.Tables("Host_ReservationReceipt").Copy()
                        dt.Columns("TenderHeadCode").ColumnName = "Reciepttypecode"
                        ' cmdLoyalty_Click(sender, e, dt)
                        dt.Dispose()
                    End If
                    If objCheck.Action = My.Resources.AcceptPaymentActionTypeSave Then

                        If PrepareReceiptData(ds.Tables(0), dsMainHostEdit) = True Then
                            'error
                            'ElseIf dsMainHost.Tables.Contains("CheckDtls") Then
                            '    PrepareCheckPaymentDetails(ds.Tables(1), dsMainHost)
                        End If
                        Dim ReservationId = dsMainHostEdit.Tables("Host_Reservation").Rows(0)("reservationID").ToString()
                        Dim ReservationRNumber = dsMainHostEdit.Tables("Host_Reservation").Rows(0)("reservationNumber").ToString()
                        If SaveAndPrintReservation(False) = True Then
                            cmdCash.Enabled = False 'vipin
                            cmdCard.Enabled = False
                            cmdCheque.Enabled = False
                            cmdPayment.Enabled = False
                            ShowMessage("Save Successfull for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
                        Else
                            ShowMessage("Check In Failed for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
                        End If
                        '   Me.Close()
                    ElseIf objCheck.Action = My.Resources.AcceptPaymentActionTypeGift Then
                        GiftMsg = objCheck.GiftReceiptMessage
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
            ' End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function cmdSavePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) As Boolean
        'Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
        Try
            If clsDefaultConfiguration.PrintFormatNo = "3" OrElse clsDefaultConfiguration.PrintFormatNo = "4" OrElse clsDefaultConfiguration.PrintFormatNo = "5" OrElse clsDefaultConfiguration.PrintFormatNo = "6" Then
                If clsDefaultConfiguration.IsArticleWiseKOT.Contains(clsAdmin.TerminalID) Then
                    IsArticleWiseKot = True
                Else
                    IsArticleWiseKot = False
                End If
                If clsDefaultConfiguration.IsCounterCopy.Contains(clsAdmin.TerminalID) Then
                    IsCounterCopy = True
                Else
                    IsCounterCopy = False
                End If
                If clsDefaultConfiguration.IsFinalReceipt.Contains(clsAdmin.TerminalID) Then
                    IsFinalReceipt = True
                Else
                    IsFinalReceipt = False
                End If
            End If
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
                        ''Rakesh:09-July-2013-->End: Template based cashmemo bill printing
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
                btnPreviousTab.Visible = True
                btnPreviousTab.Enabled = True
                _chkInDate = cmdChkInDate.Text
                _ChkoutDate = cmdCheckoutDate.Text
                txtCheckedIn.TextDetached = True
                txtCheckedIn.Text = cmdChkInDate.Text
                txtCheckedOut.TextDetached = True
                txtCheckedOut.Text = cmdCheckoutDate.Text
                _selectedRoomNumber = ""
                _selectedRoomTypeId = ""
                Dim numberOfRecords As Integer = dtReserv.Select("Selects = True").Length
                If numberOfRecords > 0 Then
                    For i = 0 To grdReservation.Rows.Count - 1
                        If i <> 0 Then
                            If grdReservation.Rows(i)("Selects") = True Then
                                _selectedRoomNumber = _selectedRoomNumber + "," + grdReservation.Rows(i)("RoomNumber").ToString()
                                _selectedRoomTypeId = _selectedRoomTypeId + "," + grdReservation.Rows(i)("RoomTypeId").ToString()
                                '+ IIf(grdReservation.Rows(i)("RoomTypeId").ToString(), String.Empty, grdReservation.Rows(i)("RoomTypeId").ToString())
                            End If
                        End If


                    Next
                    _selectedRoomNumber = (_selectedRoomNumber.Remove(0, 1))
                    _selectedRoomTypeId = (_selectedRoomTypeId.Remove(0, 1))
                Else
                    ShowMessage("Please select atleast one room", "Information")
                    Exit Sub
                End If
                ' If dtRoomTypeWisePromotions.Rows.Count = 0 Then
                dtRoomTypeWisePromotions = objHotelreservation.GetRoomTypeWisePromotions(clsAdmin.SiteCode, _chkInDate.Date, _chkInDate.Date.Year, _selectedRoomTypeId) '_selectedRoomTypeId
                HideTableLayoutPanle(False)
                BindPromotionsToCombo(dtRoomTypeWisePromotions)
                'End If
                If dtPromotionTotalDiscountValues.Rows.Count = 0 Then

                    Dim vl = dtPromotionTotalDiscountValues.Compute("Sum(TotalPromotionCost)", "")
                    If vl.ToString() <> "" Then
                        PromotionAmountDetails(SelectedRoomsCost, vl)
                    Else
                        PromotionAmountDetails(SelectedRoomsCost, "0")
                    End If
                End If
                'BindToChkBxList(dtRoomTypeWisePromotions)
                'PromotionAmountDetails(SelectedRoomsCost, "0")
                ', dtRoomTypeRoomNumberList

                '--------------------
                tpCreateDetails.Enabled = False
                tpPromotionsDetails.Enabled = True
                tpPromotionsDetails.Show()
                Exit Sub
            End If
            If tpPromotionsDetails.IsSelected Then

                'If dtPromotionSelected.Rows.Count > 0 Then
                '    dtPromotions.Clear()
                '    RoomTypeWiseSelectedPromotions(dtPromotionSelected, "", "", IsRemoved:=False)
                'End If
                'preparedtPromotion(roomtype,promtionId)
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

                'For Each dr As DataRow In dtPromotionSelected.Rows
                '    Dim RoomTypeId As String = dr("RoomTypeId").ToString()
                '    Dim mstSiteRoomTypeMap As String = dr("mstSiteRoomTypeMap").ToString()
                '    Dim mstPromotionID As String = dr("mstPromotionID").ToString()
                '    Dim mstPromotionName As String = dr("mstPromotionName").ToString()
                '    Dim discountInPercent As String = dr("discountInPercent").ToString()
                '    Dim Rate As Decimal = Convert.ToDecimal(dr("Rate").ToString())
                '    Dim discountAmount As Decimal = Convert.ToDecimal(dr("discountAmount").ToString())
                '    Dim RecordStatus As String = ""
                '    RoomTypeWiseSelectedPromotions(RoomTypeId, mstPromotionID, mstSiteRoomTypeMap, mstPromotionName, discountInPercent, Rate, discountAmount, RecordStatus:=True)
                'Next
                Dim dtSelectedGust As DataTable = dsMainHostEdit.Tables("Host_ReservationGuestDetail").Copy()
                dtGuest = objHotelreservation.GetDetailsForGuest()
                dtGuest.Clear()
                If dtSelectedGust.Rows.Count > 0 Then
                    For Each drSelectedGuest In dtSelectedGust.Rows
                        Dim drGust As DataRow
                        drGust = dtGuest.NewRow
                        If dtGuest.Rows.Count = 0 Then
                            drGust("srno") = 1
                        Else
                            drGust("srno") = dtGuest.Rows.Count + 1
                        End If
                        drGust("reservationGuestDetailID") = drSelectedGuest("reservationGuestDetailID")
                        drGust("FirstName") = drSelectedGuest("guestFirstName")
                        drGust("MiddleName") = drSelectedGuest("guestMiddleName")
                        drGust("LastName") = drSelectedGuest("guestLastName")
                        drGust("EmailId") = drSelectedGuest("guestEmailId")
                        drGust("MobileNumber") = drSelectedGuest("guestMobileNumber")
                        drGust("Age") = drSelectedGuest("guestAgeInYears")
                        drGust("Gender") = drSelectedGuest("guestGender")
                        drGust("DocumentType") = drSelectedGuest("supportedDocumentDetails_1")
                        drGust("DocumentTypeId") = drSelectedGuest("mstSupportedDocumentTypeID_1")
                        'Dim Image As Image = objHotelreservation.ImageToByteArray(drSelectedGuest("supportedDocumentImage_1"))
                        Dim BytesImage As Byte() = IIf(IsDBNull(drSelectedGuest("supportedDocumentImage_1")), Nothing, drSelectedGuest("supportedDocumentImage_1"))
                        '     Dim image As Image = drSelectedGuest("supportedDocumentImage_1")
                        drGust("GuestImage") = objHotelreservation.ByteArrayToImage(BytesImage)
                        drGust("PrimaryGuest") = drSelectedGuest("isPrimaryGuest")
                        drGust("CardNo") = drSelectedGuest("CardNo")
                        drGust("ClpProgramId") = drSelectedGuest("ClpProgramId")
                        drGust("supportedDocumentDetails") = drSelectedGuest("supportedDocumentDetails")
                        drGust("RecordStatus") = objHotelreservation.enumRecordStatus.Updated.ToString()
                        dtGuest.Rows.Add(drGust)
                    Next
                    gridGuestDetailsSetting()
                End If



                tpPromotionsDetails.Enabled = False
                tpGuestDetails.Enabled = True
                tpGuestDetails.Show()
                Exit Sub
            End If
            If tpGuestDetails.IsSelected Then
                If dtGuest.Rows.Count > 0 Then
                    If dtGuest.Select("PrimaryGuest").Count > 0 Then
                        tpGuestDetails.Enabled = False
                        tpSummaryDetails.Enabled = True
                        tpSummaryDetails.Show()
                        LoadSummaryInformationIntoGrid(dtPromotions)
                        ' LoadSummaryInformationIntoGrid(dtPromotions)
                        PromotionAmountDetails(SelectedRoomsCost, _selectedPromotionDiscount)
                    Else
                        ShowMessage("Please fill primary guest details", "Information")
                    End If
                Else
                    ShowMessage("Please fill guest details", "Information")
                End If

                'Dim dst As DataTable = dsMainHostEdit.Tables("Host_ReservationRoomTypePromotionMap")
                'Dim dtReservationCopy As DataTable = dtReservation.Copy
                'If dst.Rows.Count > 0 Then
                '    Dim drRow As DataRow
                '    For Each dr As DataRow In dst.Rows
                '        drRow = dtPromotionSelected.NewRow()
                '        drRow("reservationRoomTypePromotionMapID") = dr("reservationRoomTypePromotionMapID")
                '        drRow("reservationID") = dr("reservationID")
                '        drRow("mstSiteRoomTypeMap") = dr("mstSiteRoomTypeMap")
                '        drRow("mstPromotionID") = dr("mstPromotionID")
                '        drRow("rateDate") = dr("rateDate")
                '        drRow("yearInyyyy") = dr("yearInyyyy")
                '        drRow("mstpromotionName") = dr("promotionName")
                '        drRow("Weekday") = dr("Weekday")
                '        drRow("discountInPercent") = dr("discountInPercent")
                '        '  drRow("applicableWithOtherPromotions") = dr("applicableWithOtherPromotions")
                '        drRow("discountAmount") = dr("discountAmount")
                '        drRow("description") = dr("description")
                '        'drRow("mstStatusID") = dr("mstStatusID")
                '        dtPromotionSelected.Rows.Add(drRow)
                '    Next
                'End If

                Exit Sub
            End If
            If tpSummaryDetails.IsSelected Then
                loadPaymentDetails()
                btnNextTab.Enabled = False
                btnSaveAndFinish.Enabled = True
                Me.btnSaveAndFinish.BackColor = Color.FromArgb(0, 113, 188)
                Me.btnSaveAndFinish.Font = New Font("Callibri", 7, FontStyle.Bold)
                If dsMainHostEdit.Tables("Host_Reservation").Rows(0)("ReservationStatusId") = "8" Then
                    btnSaveAndCheckIn.Enabled = False
                    Me.btnSaveAndCheckIn.BackColor = Color.FromArgb(208, 208, 208)
                    ' Me.btnSaveAndCheckIn.Font = New Font("Callibri", 7, FontStyle.Bold)
                Else
                    btnSaveAndCheckIn.Enabled = True
                    Me.btnSaveAndCheckIn.BackColor = Color.FromArgb(0, 113, 188)
                    Me.btnSaveAndCheckIn.Font = New Font("Callibri", 7, FontStyle.Bold)
                End If
                
                tpSummaryDetails.Enabled = False
                tpPaymentsDetails.Enabled = True



                btnNextTab.BackColor = Color.FromArgb(208, 208, 208)
                tpPaymentsDetails.Show()
                If tpPaymentsDetails.IsSelected Then
                    tpAdvancePayment.Show()
                    Exit Sub
                End If
            Else
                btnNextTab.Enabled = True
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub



    Private Sub tabReservation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabReservation.SelectedIndexChanged
        If tpPromotionsDetails.IsSelected Then
            btnCancel.Visible = True
        End If
        If tpCreateDetails.IsSelected Then
            btnCancel.Visible = True
        End If


    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MsgBox(" If you cancel the reservation, you will lose unsaved data. Do you want to continue?", MsgBoxStyle.YesNo, "Information") = MsgBoxResult.Yes Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Else
            Me.DialogResult = Windows.Forms.DialogResult.None
        End If
    End Sub
    Private Sub btnPreviousTab_Click(sender As Object, e As EventArgs) Handles btnPreviousTab.Click
        btnNextTab.Enabled = True
        If tpCreateDetails.IsSelected Then
            btnPreviousTab.Enabled = False
            btnPreviousTab.Visible = False
        End If
        If tpPromotionsDetails.IsSelected Then
            tpCreateDetails.Enabled = True
            tpPromotionsDetails.Enabled = False
            tpCreateDetails.Show()
            btnPreviousTab.Visible = False
            gridReservationDetailsSetting()
            'btnPreviousTab.Enabled = False
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

    Private Sub btnSaveAndFinish_Click(sender As Object, e As EventArgs) Handles btnSaveAndFinish.Click
        Try
            PrepareDataSave()

            If dsMainHostEdit.Tables("Host_ReservationGuestDetail").Rows.Count = 0 Then
                ShowMessage("please go back and enter the guest details before check-in", "Information")
                Exit Sub
            End If
            Dim ReservationId = dsMainHostEdit.Tables("Host_Reservation").Rows(0)("reservationID").ToString()
            ReservationRNumber = dsMainHostEdit.Tables("Host_Reservation").Rows(0)("reservationNumber").ToString()
            'Dim disStatusMgs = "Reservation Saved Successfully for"  'for reservation
            '"Check-In Successfull for"  ' for check in
            'ReservationRNumber = "RN0010000000020"
            'ReservationGuestName = "Khusrao Khan"
            ReservationDate = System.DateTime.Now.ToString("dd/MM/yyyy")
            If SaveAndPrintReservation(True) = True Then
                ShowMessage("Save Successfully for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
            Else
                ShowMessage("Save Failed for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
            End If
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
#End Region
    '"--end Events"
#Region "--Functions"
    Public Sub getHOSTBinding()
        Try
            'dsMainHost = objHotelreservation.HOSTGetStruc()
        Catch ex As Exception

        End Try
    End Sub

    'Public Sub PrepareDataSave()
    '    Try
    '        getHOSTBinding()
    '        If PrepareReservationCreateData(dsMainHostEdit) Then
    '            Exit Sub
    '        ElseIf PrepareRoomMapData(dtSummary, dsMainHostEdit) Then
    '            Exit Sub
    '        ElseIf PreparePromotionData() Then
    '            Exit Sub
    '        ElseIf PrepareGuestDetailData(dtGuest, dsMainHostEdit) Then
    '            Exit Sub
    '        ElseIf PreparePaymentData() Then
    '            Exit Sub
    '        End If
    '        If objHotelreservation.SaveReservationAllData(dsMainHostEdit) Then
    '            ShowMessage("Reservation save successfully for" & vbCrLf & " Guest Name :" & " " & txtName.Text & vbCrLf & "Res Id :" & " " & _reservationNumber & vbCrLf & "Date :" & _chkInDate.Date & vbCrLf, "Reservation Details")
    '        Else
    '            ShowMessage("Reservation save failed ", "Reservation Details")
    '        End If
    '    Catch ex As Exception
    '        LogException(ex)
    '    End Try


    'End Sub
    Public Sub PrepareDataSave()
        Try
            '------------------------calucalte tax
            CalculateTax()
            '------------------------
            If PrepareReservationCreateData(dsMainHostEdit) Then   ' reservation  table
                Exit Sub
            ElseIf PrepareRoomMapData(dtSummary, dsMainHostEdit) Then       'room map table
                Exit Sub
            ElseIf PrepareTaxData(dtReservationTaxMap, dsMainHostEdit) Then  'room tax map table
                Exit Sub
            ElseIf PreparePromotionData(dtPromotions, dsMainHostEdit) Then  ' room map prmotions table
                Exit Sub
            ElseIf PrepareCustomersDetailsData(dtGuest, dsMainHostEdit) Then  'guest table
                Exit Sub
            ElseIf PrepareGuestDetailData(dtGuest, dsMainHostEdit) Then  'guest table
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
        dtReservationTaxMap = dsMainHostEdit.Tables("Host_ReservationTaxMap").Copy()
        Dim _totalCostAmount As Decimal
        Dim _totalTaxAmount As Decimal
        Dim _TaxAmount As Decimal
        For Each dr In dtHotelAllTaxes.Rows
            Dim drdtl = dtReservationTaxMap.Select("taxcode='" + dr("taxcode") + "' and siteCode='" + dr("SiteCode") + "'")
            If drdtl.Length > 0 Then
                drdtl(0)("SiteCode") = dr("SiteCode")
                drdtl(0)("TaxLineNo") = dr("TaxLineNo")
                drdtl(0)("DocumentType") = dr("DocumentType")
                drdtl(0)("TaxCode") = dr("TaxCode")
                drdtl(0)("TaxValueInPercent") = dr("Tax")
                _TaxAmount = dr("Tax")
                _totalCostAmount = Convert.ToDecimal(txtCTotalCostAmount.Text)
                _totalTaxAmount = (_totalCostAmount / 100) * _TaxAmount
                drdtl(0)("TaxValueInAmount") = _totalTaxAmount
                drdtl(0)("TaxValueInAmount") = _totalTaxAmount
            Else
                Dim drTax As DataRow
                drTax = dtReservationTaxMap.NewRow
                drTax("SiteCode") = dr("SiteCode")
                drTax("TaxLineNo") = dr("TaxLineNo")
                drTax("DocumentType") = dr("DocumentType")
                drTax("TaxCode") = dr("TaxCode")
                drTax("TaxValueInPercent") = dr("Tax")
                _TaxAmount = dr("Tax")
                _totalCostAmount = Convert.ToDecimal(txtCTotalCostAmount.Text)
                _totalTaxAmount = (_totalCostAmount / 100) * _TaxAmount
                drTax("TaxValueInAmount") = _totalTaxAmount
                drTax("TaxValueInAmount") = _totalTaxAmount
                dtReservationTaxMap.Rows.Add(drTax)
            End If
            dtReservationTaxMap.AcceptChanges()
        Next
    End Sub
    Public Function SaveAndPrintReservation(Optional ByVal SaveWithoutPayment As Boolean = False) As Boolean
        Try
            If objHotelreservation.SaveReservationAllData(dsMainHostEdit, SaveWithoutPayment) Then
                'print report 
                Return True
            Else
                Exit Function
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
#Region "----------------------------------Data prepare Edit"
    'added by khusrao adil on 02-02-2017
    ' updated by khusrao adil on on 17-02-2017
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
            Dim dtTEMP = dsMainHostEdit.Tables("Host_Reservation").Copy()
            For Each dr In dsMainHostEdit.Tables("Host_Reservation").Rows
                If Not String.IsNullOrEmpty(dr("reservationID").ToString()) Then
                    Dim _tempReservationId = Convert.ToDouble(dr("reservationID"))
                    Dim drReservationDtl = dsMmain.Tables("Host_Reservation").Select("reservationID=" + dr("reservationID").ToString() + " and reservationNumber='" + dr("reservationNumber") + "'")
                    If drReservationDtl.Length > 0 Then
                        drReservationDtl(0)("mstSupportedDocumentTypeID") = dr("mstSupportedDocumentTypeID")
                        drReservationDtl(0)("TerminalID") = dr("TerminalID")
                        'If isCheckInWithPayment = False Then
                        Dim _reservationStatusId = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RESERVATION_STATUS.ToString(), objHotelreservation.enumStatus.RESERVED.ToString())
                        If drReservationDtl(0)("reservationStatusID") = _reservationStatusId Then
                            drReservationDtl(0)("reservationStatusID") = _reservationStatusId
                        End If
                        'Else
                        '    drReservationDtl(0)("reservationStatusID") = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RESERVATION_STATUS.ToString(), objHotelreservation.enumStatus.CHECKED_IN.ToString())
                        'End If
                        drReservationDtl(0)("siteCode") = dr("siteCode")
                        drReservationDtl(0)("yearInyyyy") = dr("yearInyyyy")
                        drReservationDtl(0)("reservationNumber") = dr("reservationNumber")
                        drReservationDtl(0)("reservationDateTime") = dr("reservationDateTime")
                        drReservationDtl(0)("checkInDateTime") = dr("checkInDateTime")
                        drReservationDtl(0)("checkOutDateTime") = dr("checkOutDateTime")
                        drReservationDtl(0)("totalDaysOfStay") = dr("totalDaysOfStay")
                        drReservationDtl(0)("totalNightsOfStay") = dr("totalNightsOfStay")
                        drReservationDtl(0)("totalNoOfRooms") = dr("totalNoOfRooms")
                        drReservationDtl(0)("totalNetAmount") = dr("totalNetAmount")
                        drReservationDtl(0)("totalTaxAmount") = dr("totalTaxAmount")
                        drReservationDtl(0)("totalPromotionDiscountAmount") = dr("totalPromotionDiscountAmount")
                        drReservationDtl(0)("totalGrossAmount") = dr("totalGrossAmount")
                        'If isCheckIn = True Then
                        '    drReservationDtl(0)("totalAmountPaid") = txtSumPaidAmt.Text
                        '    drReservationDtl(0)("remainingAmountToPay") = txtSumPaymentRemaining.Text
                        'Else
                        '    Dim _txtAdvPaidAmount As Decimal = Convert.ToDecimal(txtAdvPaidAmount.Text)
                        '    'If _txtAdvPaidAmount = "0" Then
                        '    '    drReservationDtl(0)("totalAmountPaid") = txtSumPaymentRemaining.Text
                        '    '    drReservationDtl(0)("remainingAmountToPay") = txtSumPaidAmt.Text
                        '    'End If
                        '    'drReservationDtl(0)("totalAmountPaid") = txtSumPaymentRemaining.Text
                        '    'drReservationDtl(0)("remainingAmountToPay") = txtSumPaidAmt.Text

                        'End If
                        'drReservationDtl(0)("totalAmountPaid") = dr("totalAmountPaid")

                        If isCheckIn = True Then
                            drReservationDtl(0)("totalAmountPaid") = txtSumPaidAmt.Text
                        Else
                            If ISCallFromPayment = True Then
                                drReservationDtl(0)("totalAmountPaid") = txtSumPaymentRemaining.Text
                            Else
                                drReservationDtl(0)("totalAmountPaid") = txtSumPaidAmt.Text
                            End If
                        End If
                        drReservationDtl(0)("remainingAmountToPay") = _totalGrossAmount - CDbl(drReservationDtl(0)("totalAmountPaid"))
                        drReservationDtl(0)("totalServicesCharges") = dr("totalServicesCharges")
                        drReservationDtl(0)("totalFoodCharges") = dr("totalFoodCharges")
                        drReservationDtl(0)("primaryGuestDocumentNumber") = dr("primaryGuestDocumentNumber")
                        drReservationDtl(0)("primaryGuestDocumentDetails") = dr("primaryGuestDocumentDetails")
                        drReservationDtl(0)("primaryGuestDocumentImage") = dr("primaryGuestDocumentImage")
                        drReservationDtl(0)("totalNoOfAdults") = dr("totalNoOfAdults")
                        drReservationDtl(0)("totalNoOfChildren") = dr("totalNoOfChildren")
                        drReservationDtl(0)("remarks1") = dr("remarks1")
                        drReservationDtl(0)("remarks2") = dr("remarks2")
                        drReservationDtl(0)("UpdatedOn") = DateTime.Now
                        drReservationDtl(0)("UpdatedAt") = clsAdmin.SiteCode
                        drReservationDtl(0)("UpdatedBy") = clsAdmin.UserName
                        drReservationDtl(0)("mstStatusID") = dr("mstStatusID")
                        dsMmain.AcceptChanges()
                    End If
                    Else
                        Dim drReservation As DataRow
                        drReservation = dsMainHostEdit.Tables("Host_Reservation").NewRow
                        drReservation("reservationID") = MaxId
                        'If isCheckInWithPayment = False Then
                        drReservation("reservationStatusID") = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RESERVATION_STATUS.ToString(), objHotelreservation.enumStatus.RESERVED.ToString())
                        'Else
                        '    drReservation("reservationStatusID") = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RESERVATION_STATUS.ToString(), objHotelreservation.enumStatus.CHECKED_IN.ToString())
                        'End If
                        drReservation("mstSupportedDocumentTypeID") = cmdDocumentType.SelectedIndex
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
                        drReservation("totalAmountPaid") = txtSumPaidAmt.Text
                        drReservation("remainingAmountToPay") = txtSumPaymentRemaining.Text
                        drReservation("totalServicesCharges") = "0"
                        drReservation("totalFoodCharges") = "0"
                        drReservation("primaryGuestDocumentNumber") = "0"
                        drReservation("primaryGuestDocumentDetails") = "0"
                        drReservation("primaryGuestDocumentImage") = Nothing
                        drReservation("totalNoOfAdults") = txtSumNumberOfAdult.Text
                        drReservation("totalNoOfChildren") = "0" ' txtChildren.Text
                        drReservation("remarks1") = "0"
                        drReservation("remarks2") = "0"

                        drReservation("CreatedOn") = DateTime.Now
                        drReservation("CreatedAt") = clsAdmin.SiteCode
                        drReservation("CreatedBy") = clsAdmin.UserName
                        drReservation("UpdatedOn") = DateTime.Now
                        drReservation("UpdatedAt") = clsAdmin.SiteCode
                        drReservation("UpdatedBy") = clsAdmin.UserName
                        drReservation("mstStatusID") = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RECORD_STATUS.ToString(), objHotelreservation.enumStatus.ACTIVE.ToString())
                    End If
            Next
            '  dsMainHostEdit.Tables("Host_Reservation").AcceptChanges()
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    'added by khusrao adil on 03-02-2017 for room map details
    'updated by khusrao adil on 17-02-2017 for update room map details
    Public Function PrepareRoomMapData(ByVal dtRoomMap As DataTable, ByRef dsMainHost As DataSet) As Boolean
        Try
            Dim _rmDicountAmount As Decimal = 0
            Dim _rmNetAmount As Decimal = 0
            Dim _rmGrossAmount As Decimal = 0
            Dim _rmTaxAmount As Decimal = 0
            MaxId = objHotelreservation.GetMaxId("Host_ReservationRoomMap", "reservationRoomMapID")
            'dsMainHost.Tables("Host_ReservationRoomMap").Clear()
            Dim dtTEMP = dsMainHost.Tables("Host_ReservationRoomMap").Copy()
            dtRoomMap.TableName = "Host_ReservationRoomMap"
            For Each dr In dtRoomMap.Rows
                If Not String.IsNullOrEmpty(dr("reservationRoomMapID").ToString()) Then
                    Dim drRoomMapDtl = dsMainHost.Tables("Host_ReservationRoomMap").Select("reservationRoomMapID='" + dr("reservationRoomMapID") + "'")
                    If drRoomMapDtl.Length > 0 Then
                        _rmGrossAmount = Convert.ToDecimal(dr("Price"))
                        _rmDicountAmount = Convert.ToDecimal(dr("Discount"))
                        Dim _ttax = If(dr("tax") IsNot DBNull.Value, 0, dr("tax"))
                        _rmTaxAmount = Convert.ToDecimal(_ttax)
                        _rmNetAmount = (_rmGrossAmount + _rmTaxAmount) - _rmDicountAmount
                        drRoomMapDtl(0)("mstRoomID") = dr("mstRoomID")
                        drRoomMapDtl(0)("mstStandardRoomRateID") = dr("mstRoomID")
                        ' drRoomMapDtl(0)("rateDate") = dr("rateDate")
                        '  drRoomMapDtl(0)("yearInyyyy") = dr("yearInyyyy")
                        ' drRoomMapDtl(0)("weekDay") = dr("mstRoomID")
                        drRoomMapDtl(0)("rate") = dr("Price")
                        'drRoomMapDtl(0)("totalTaxInPercent") = dr("mstRoomID")
                        drRoomMapDtl(0)("totalTaxInAmount") = _rmTaxAmount
                        'drRoomMapDtl(0)("totalPromotionDiscountInPercent") = _rmDicountAmount
                        drRoomMapDtl(0)("totalPromotionDiscountInAmount") = _rmDicountAmount
                        drRoomMapDtl(0)("totalNetAmount") = _rmNetAmount
                        'drRoomMapDtl(0)("RecordStatus") = dr("RecordStatus")
                        drRoomMapDtl(0)("totalGrossAmount") = _rmGrossAmount
                        drRoomMapDtl(0)("UpdatedOn") = DateTime.Now
                        drRoomMapDtl(0)("UpdatedAt") = clsAdmin.SiteCode
                        drRoomMapDtl(0)("UpdatedBy") = clsAdmin.UserName
                        drRoomMapDtl(0)("mstStatusID") = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RECORD_STATUS.ToString(), objHotelreservation.enumStatus.ACTIVE.ToString())
                        dsMainHost.Tables("Host_ReservationRoomMap").AcceptChanges()
                    End If
                Else
                    _rmGrossAmount = Convert.ToDecimal(dr("Price"))
                    _rmDicountAmount = Convert.ToDecimal(dr("Discount"))
                    Dim _ttax = If(dr("tax") IsNot DBNull.Value, 0, dr("tax"))
                    _rmTaxAmount = Convert.ToDecimal(_ttax)
                    _rmNetAmount = (_rmGrossAmount + _rmTaxAmount) - _rmDicountAmount
                    Dim drRoomMap As DataRow
                    drRoomMap = dsMainHost.Tables("Host_ReservationRoomMap").NewRow
                    drRoomMap("reservationRoomMapID") = MaxId
                    drRoomMap("ReservationId") = dsMainHost.Tables("Host_Reservation").Rows(0)("ReservationId")
                    drRoomMap("mstRoomID") = dr("mstRoomID")
                    drRoomMap("mstStandardRoomRateID") = dr("mstRoomID")
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
                    '  drRoomMap("RecordStatus") = dr("RecordStatus")
                    drRoomMap("CreatedOn") = DateTime.Now
                    drRoomMap("CreatedAt") = clsAdmin.SiteCode
                    drRoomMap("CreatedBy") = clsAdmin.UserName
                    drRoomMap("UpdatedOn") = DateTime.Now
                    drRoomMap("UpdatedAt") = clsAdmin.SiteCode
                    drRoomMap("UpdatedBy") = clsAdmin.UserName
                    drRoomMap("mstStatusID") = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RECORD_STATUS.ToString(), objHotelreservation.enumStatus.ACTIVE.ToString())
                    dsMainHost.Tables("Host_ReservationRoomMap").Rows.Add(drRoomMap)
                    MaxId = MaxId + 1
                End If
            Next
            MaxId = 0
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function PreparePromotionData(ByVal dtRoomPromoMap As DataTable, ByRef dsMainHost As DataSet) As Boolean

        Try
            MaxId = objHotelreservation.GetMaxId("Host_ReservationRoomTypePromotionMap", "reservationRoomTypePromotionMapID")
            ' dsMainHost.Tables("Host_ReservationRoomTypePromotionMap").Clear()
            Dim dtTEMP = dsMainHost.Tables("Host_ReservationRoomTypePromotionMap").Copy()
            Dim dstemp = dsMainHost.Copy
            For Each dr In dtRoomPromoMap.Rows
                If Not String.IsNullOrEmpty(dr("reservationRoomTypePromotionMapID").ToString()) Then
                    Dim drRoomPromoMapDtl = dsMainHost.Tables("Host_ReservationRoomTypePromotionMap").Select("reservationRoomTypePromotionMapID='" + dr("reservationRoomTypePromotionMapID") + "'")
                    If drRoomPromoMapDtl.Length > 0 Then
                        drRoomPromoMapDtl(0)("reservationRoomTypePromotionMapID") = dr("reservationRoomTypePromotionMapID")
                        drRoomPromoMapDtl(0)("ReservationId") = dr("ReservationId")
                        drRoomPromoMapDtl(0)("mstPromotionID") = dr("mstPromotionID")
                        drRoomPromoMapDtl(0)("promotionName") = dr("mstPromotionName")
                        drRoomPromoMapDtl(0)("rateDate") = _chkInDate.Date
                        drRoomPromoMapDtl(0)("discountInPercent") = dr("discountInPercent")
                        drRoomPromoMapDtl(0)("discountAmount") = dr("discountAmount")
                        drRoomPromoMapDtl(0)("mstSiteRoomTypeMap") = dr("mstSiteRoomTypeMap")
                        drRoomPromoMapDtl(0)("yearInyyyy") = _chkInDate.Date.Year.ToString()
                        drRoomPromoMapDtl(0)("weekDay") = _chkInDate.Date.DayOfWeek
                        drRoomPromoMapDtl(0)("applicableWithOtherPromotions") = 0 'dr("applicableWithOtherPromotions")
                        drRoomPromoMapDtl(0)("description") = dr("description")
                        drRoomPromoMapDtl(0)("UpdatedOn") = DateTime.Now
                        drRoomPromoMapDtl(0)("UpdatedAt") = clsAdmin.SiteCode
                        drRoomPromoMapDtl(0)("UpdatedBy") = clsAdmin.UserName
                        drRoomPromoMapDtl(0)("mstStatusID") = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RECORD_STATUS.ToString(), objHotelreservation.enumStatus.ACTIVE.ToString())
                        dsMainHost.Tables("Host_ReservationRoomTypePromotionMap").AcceptChanges()
                    End If
                Else
                    Dim drRoomPromoMap As DataRow
                    drRoomPromoMap = dsMainHost.Tables("Host_ReservationRoomTypePromotionMap").NewRow
                    drRoomPromoMap("reservationRoomTypePromotionMapID") = MaxId
                    drRoomPromoMap("ReservationId") = dsMainHost.Tables("Host_Reservation").Rows(0)("ReservationId")
                    drRoomPromoMap("rateDate") = _chkInDate.Date
                    drRoomPromoMap("mstPromotionID") = dr("mstPromotionID")
                    drRoomPromoMap("promotionName") = dr("mstPromotionName")
                    drRoomPromoMap("discountInPercent") = dr("discountInPercent")
                    drRoomPromoMap("discountAmount") = dr("discountAmount")
                    drRoomPromoMap("mstSiteRoomTypeMap") = dr("mstSiteRoomTypeMap")
                    drRoomPromoMap("yearInyyyy") = _chkInDate.Date.Year.ToString()
                    drRoomPromoMap("weekDay") = _chkInDate.Date.DayOfWeek
                    drRoomPromoMap("applicableWithOtherPromotions") = 0 'dr("applicableWithOtherPromotions")
                    drRoomPromoMap("description") = dr("description")
                    drRoomPromoMap("CreatedOn") = DateTime.Now
                    drRoomPromoMap("CreatedAt") = clsAdmin.SiteCode
                    drRoomPromoMap("CreatedBy") = clsAdmin.UserName
                    drRoomPromoMap("UpdatedOn") = DateTime.Now
                    drRoomPromoMap("UpdatedAt") = clsAdmin.SiteCode
                    drRoomPromoMap("UpdatedBy") = clsAdmin.UserName
                    drRoomPromoMap("mstStatusID") = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RECORD_STATUS.ToString(), objHotelreservation.enumStatus.ACTIVE.ToString())
                    dsMainHost.Tables("Host_ReservationRoomTypePromotionMap").Rows.Add(drRoomPromoMap)
                    MaxId = MaxId + 1
                    dsMainHost.AcceptChanges()
                End If
            Next
            MaxId = 0
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    'code is added by irfan on 5/4/2018 for hotel reservation.
    Public Function PrepareCustomersDetailsData(ByVal dtCustDetail As DataTable, ByRef dsMainHost As DataSet)
        Try
            If dtGuest.Rows.Count > 0 Then
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
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 02-02-2017 for prepareing guest details
    Public Function PrepareGuestDetailData(ByVal dtGuestDatail As DataTable, ByRef dsMainHost As DataSet) As Boolean
        Try
            MaxId = objHotelreservation.GetMaxId("Host_ReservationGuestDetail", "reservationGuestDetailID")
            Dim tempReservationGuestDetail As DataTable = dsMainHostEdit.Tables("Host_ReservationGuestDetail").Copy()
            Dim tempReservation As DataTable = dsMainHostEdit.Tables("Host_ReservationGuestDetail").Copy()
            For Each dr In dtGuestDatail.Rows
                If Not String.IsNullOrEmpty(dr("reservationGuestDetailID").ToString()) Then
                    Dim drGuestDtl = dsMainHost.Tables("Host_ReservationGuestDetail").Select("reservationGuestDetailID='" + dr("reservationGuestDetailID") + "'")
                    If drGuestDtl.Length > 0 Then
                        drGuestDtl(0)("guestFirstName") = dr("FirstName")
                        drGuestDtl(0)("guestMiddleName") = dr("MiddleName")
                        drGuestDtl(0)("guestLastName") = dr("LastName")
                        drGuestDtl(0)("guestEmailID") = dr("EmailId")
                        drGuestDtl(0)("guestMobileNumber") = dr("MobileNumber")
                        drGuestDtl(0)("guestAgeInYears") = dr("Age")
                        drGuestDtl(0)("guestGender") = dr("Gender")
                        drGuestDtl(0)("mstSupportedDocumentTypeID_1") = dr("DocumentTypeId")
                        'drGuestDtl("supportedDocumentNumber_1") = dr("DocumentType")
                        drGuestDtl(0)("supportedDocumentDetails_1") = dr("DocumentType")
                        drGuestDtl(0)("mstSupportedDocumentTypeID_2") = dr("DocumentTypeId")
                        '  drGuestDtl("supportedDocumentNumber_2") = dr("DocumentType")
                        drGuestDtl(0)("supportedDocumentDetails_2") = dr("DocumentType")
                        drGuestDtl(0)("mstSupportedDocumentTypeID_3") = dr("DocumentTypeId")
                        '  drGuestDtl("supportedDocumentNumber_3") = dr("DocumentType")
                        drGuestDtl(0)("supportedDocumentDetails_3") = dr("DocumentType")
                        drGuestDtl(0)("supportedDocumentDetails") = dr("supportedDocumentDetails")
                        ' drGuestDtl("remarks") = dr("ReservationId")
                        drGuestDtl(0)("isPrimaryGuest") = dr("PrimaryGuest")
                        drGuestDtl(0)("CardNo") = dr("CardNo")
                        drGuestDtl(0)("ClpProgramId") = dr("ClpProgramId")
                        drGuestDtl(0)("UpdatedOn") = DateTime.Now
                        drGuestDtl(0)("UpdatedAt") = clsAdmin.SiteCode
                        drGuestDtl(0)("UpdatedBy") = clsAdmin.UserName
                        dsMainHost.Tables("Host_ReservationGuestDetail").AcceptChanges()
                    End If
                Else
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
                    drGuest("supportedDocumentDetails") = dr("supportedDocumentDetails")
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
                    drGuest("mstStatusID") = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RECORD_STATUS.ToString(), objHotelreservation.enumStatus.ACTIVE.ToString())
                    dsMainHost.Tables("Host_ReservationGuestDetail").Rows.Add(drGuest)
                    MaxId = MaxId + 1
                End If
            Next
            tempReservationGuestDetail = dsMainHost.Tables("Host_ReservationGuestDetail").Copy
            SaveGuestInCLPCustomer(dsMainHost.Tables("Host_ReservationGuestDetail"))
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    'added by khusrao adil on 17-02-2017 for update tax details
    Public Function PrepareTaxData(ByVal dtMapTaxDetails As DataTable, ByRef dsMainHost As DataSet) As Boolean
        Try
            MaxId = objHotelreservation.GetMaxId("Host_ReservationTaxMap", "reservationTaxMapID")
            Dim temp As DataTable = dsMainHost.Tables("Host_ReservationTaxMap").Copy()
            For Each dr In dtMapTaxDetails.Rows
                If Not String.IsNullOrEmpty(dr("reservationTaxMapID").ToString()) Then
                    Dim drTaxDtl = dsMainHost.Tables("Host_ReservationTaxMap").Select("reservationTaxMapID='" + dr("reservationTaxMapID").ToString() + "'")
                    If drTaxDtl.Length > 0 Then
                        drTaxDtl(0)("SiteCode") = dr("SiteCode")
                        drTaxDtl(0)("TaxLineNo") = dr("TaxLineNo")
                        drTaxDtl(0)("DocumentType") = dr("DocumentType")
                        drTaxDtl(0)("TaxCode") = dr("TaxCode")
                        drTaxDtl(0)("TaxValueInPercent") = dr("TaxValueInPercent")
                        drTaxDtl(0)("TaxValueInAmount") = dr("TaxValueInAmount")
                        drTaxDtl(0)("UpdatedOn") = DateTime.Now
                        drTaxDtl(0)("UpdatedAt") = clsAdmin.SiteCode
                        drTaxDtl(0)("UpdatedBy") = clsAdmin.UserName
                        dsMainHost.Tables("Host_ReservationTaxMap").AcceptChanges()
                    End If
                Else
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
                End If
            Next
            MaxId = 0
        Catch ex As Exception
            LogException(ex)
            Return False
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
    Public Function SaveGuestInCLPCustomer(ByRef dtGuestClp As DataTable) As DataTable
        Try
            Dim primmaryGustMobileNumber As String = ""
            For Each dr In dtGuestClp.Rows
                Dim docNo As String = objcomm.getDocumentNo("Customer Loyalty", clsAdmin.SiteCode)
                Dim otherCharacters = "CLS" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3, 3)
                Dim TiersList = CLP_Data.GetCLPTiers(clsAdmin.CLPProgram)
                Dim CardType As String
                If TiersList.Count > 0 Then
                    CardType = TiersList(0)
                End If

                If dr("RecordStatus").ToString() = "Update" Then
                    Dim CardNumber = dr("CardNo")
                    objReservation.UpdateCustomerDetails(dr("guestFirstName") + " " + dr("guestMiddleName") + " " + dr("guestLastName"), CardNumber, dr("guestGender"), dr("guestMobileNumber"), clsAdmin.SiteCode, clsAdmin.UserCode, clsAdmin.CLPProgram, CardType)
                Else
                    Dim CardNumber = GenDocNo(otherCharacters, 15, docNo)
                    objReservation.InsertCustomerDetails(dr("guestFirstName") + " " + dr("guestMiddleName") + " " + dr("guestLastName"), CardNumber, dr("guestMobileNumber"), clsAdmin.SiteCode, clsAdmin.UserCode, clsAdmin.CLPProgram, CardType)
                End If

                Dim dt = objHotelreservation.GetClpCustomer(dr("guestMobileNumber"))
                If dt.Rows.Count > 0 Then
                    dr("CardNo") = dt.Rows(0)("CardNo")
                    dr("ClpProgramId") = dt.Rows(0)("ClpProgramId")
                    dtGuestClp.AcceptChanges()
                End If
                If dr("isPrimaryGuest") = True Then
                    ' dsMainHost.Tables("Host_Reservation").Rows(0)("primaryGuestDocumentNumber") = dr("supportedDocumentNumber_1")
                    dsMainHostEdit.Tables("Host_Reservation").Rows(0)("primaryGuestDocumentDetails") = dr("supportedDocumentDetails_1")
                    dsMainHostEdit.Tables("Host_Reservation").Rows(0)("primaryGuestDocumentImage") = dr("supportedDocumentImage_1")
                    ReservationGuestName = dr("guestFirstName").ToString() + " " + dr("guestMiddleName").ToString() + " " + dr("guestLastName").ToString()
                    ReservationDate = System.DateTime.Now.ToString()
                End If

            Next

            dtGuestClp.AcceptChanges()

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

#End Region
#End Region
    ' "--end Functions"



#End Region
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
                'answer = tempRoom.Substring(tempRoom.Length - 1, 1)
                'If answer = "," ThenMO
                '    tempRoom = (tempRoom.Remove(tempRoom.Length - 1, 1))
                'End If
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
                            'If edit1 = False Then
                            Dim dtDefaultPromo = dtPromotions.Select("roomTypeId='" + d("mstRoomTypeId").ToString() + "'")
                            If dtDefaultPromo.Length <> 0 Then
                                Dim mstPromotionId As String = dtDefaultPromo(0)("mstPromotionId")
                                Dim strSel = DimComboFilter.Select("MstPromotionId='" + mstPromotionId + "'")
                                DisplayPromotionValues(CtrlComboPromotion1, CtrllblPromoAppliedValue1, strSel)
                            Else
                                CtrllblPromoAppliedValue1.Text = "0"
                                CtrllblPromoAppliedValue1.Tag = ""
                            End If
                            'End If
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

                            Dim dtDefaultPromo = dtPromotions.Select("roomTypeId='" + d("mstRoomTypeId").ToString() + "'")
                            If dtDefaultPromo.Length <> 0 Then
                                Dim mstPromotionId As String = dtDefaultPromo(0)("mstPromotionId")
                                Dim strSel = DimComboFilter.Select("MstPromotionId='" + mstPromotionId + "'")
                                DisplayPromotionValues(CtrlComboPromotion2, CtrllblPromoAppliedValue2, strSel)
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
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 21-02-2017 for promotion fiexed value
    Dim reservationroommapid As String
    Public Function BindToChkBxList(ByVal dt As DataTable)
        Try
            Dim DtFilter = dt.DefaultView.ToTable(True, "mstRoomTypeId")
            Dim drRowCount = DtFilter.Rows.Count  ' 2
            If DtFilter.Rows.Count > 4 Then
                FlowLayoutPanelHeaderHolder.AutoScroll = True
            Else
                FlowLayoutPanelHeaderHolder.AutoScroll = False
            End If

            'Dim xdtRoomTypeIdWise = dt.Select("mstRoomTypeId='" + Row("mstRoomTypeId").ToString() + "'").ToList()  '7
            For index = 0 To DtFilter.Rows.Count - 1
                Dim RoomNumberList = dtRoomTypeRoomsCount.Select("mstRoomTypeId='" + DtFilter.Rows(index)("mstRoomTypeId").ToString() + "'")
                Dim tempRoom As String = RoomNumberList(0)("RoomNumberList").ToString() '(_selectedRoomNumber.Remove(0, 1))
                Dim tempPromoAppliedCount As String = RoomNumberList(0)("PromotionCount").ToString()
                Dim answer As Char
                If tempRoom <> "" Then
                    answer = tempRoom.Substring(tempRoom.Length - 1, 1)
                    If answer = "," Then
                        tempRoom = (tempRoom.Remove(tempRoom.Length - 1, 1))
                    End If
                Else

                End If


                Dim dtAlreadyAppliedPromo As DataTable = dsMainHostEdit.Tables("Host_ReservationRoomTypePromotionMap").Copy()
                For Each drRow As DataRow In dtAlreadyAppliedPromo.Rows
                    Dim drAllPromo = dt.Select("mstSiteRoomTypeMap='" + drRow("mstSiteRoomTypeMap").ToString() + "' and mstPromotionID='" + drRow("mstPromotionID").ToString() + "'")
                    If drAllPromo.Length > 0 Then
                        ' drAllPromo(0)("DiscountInAmount") = drRow("discountAmount")
                        'drAllPromo(0)("")
                        'drAllPromo(0)("")
                        reservationroommapid = drRow("reservationRoomTypePromotionMapID")
                        '  CheckBoxCalculation(drRow("mstSiteRoomTypeMap"), drRow("mstPromotionID"), dt, True)
                    End If

                Next
                PromotionAmountDetails(SelectedRoomsCost, _selectedPromotionDiscount)
                Dim xdtRoomTypeIdWise = dt.Select("mstRoomTypeId='" + DtFilter.Rows(index)("mstRoomTypeId").ToString() + "'").ToList()  '7
                If index = 0 Then  'room type 1
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx1, True)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue1.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue1.Text = d("FinaliseCostAfterPromo").ToString + " INR"
                            CtrllblRoomTypeName1.Text = d("RoomTypeName")
                            CtrllblRoomTypeName1.Tag = d("mstRoomTypeId")

                        End If
                        counter = counter + 1
                    Next
                ElseIf index = 1 Then  'room type 2 
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx2, True)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue2.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue2.Text = d("FinaliseCostAfterPromo").ToString + " INR"
                            CtrllblRoomTypeName2.Text = d("RoomTypeName")
                            CtrllblRoomTypeName2.Tag = d("mstRoomTypeId")

                        End If
                        counter = counter + 1
                    Next
                ElseIf index = 2 Then   'room type 3
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx3, True)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue3.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue3.Text = d("rate").ToString + " INR"
                            CtrllblRoomTypeName3.Text = d("RoomTypeName")
                            CtrllblRoomTypeName3.Tag = d("mstRoomTypeId")

                        End If
                        counter = counter + 1
                    Next
                ElseIf index = 3 Then  'room type 4
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx4, True)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue4.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue4.Text = d("rate").ToString + " INR"
                            CtrllblRoomTypeName4.Text = d("RoomTypeName")
                            CtrllblRoomTypeName4.Tag = d("mstRoomTypeId")

                        End If
                        counter = counter + 1
                    Next
                ElseIf index = 4 Then  'room type 5
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx5, True)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue5.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue5.Text = d("rate").ToString + " INR"
                            CtrllblRoomTypeName5.Text = d("RoomTypeName")
                            CtrllblRoomTypeName5.Tag = d("mstRoomTypeId")

                        End If
                        counter = counter + 1
                    Next
                ElseIf index = 5 Then  'room type 6
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx6, True)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue6.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue6.Text = d("rate").ToString + " INR"
                            CtrllblRoomTypeName6.Text = d("RoomTypeName")
                            CtrllblRoomTypeName6.Tag = d("mstRoomTypeId")

                        End If
                        counter = counter + 1
                    Next
                ElseIf index = 6 Then  'room type 7
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx7, True)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue7.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue7.Text = d("rate").ToString + " INR"
                            CtrllblRoomTypeName7.Text = d("RoomTypeName")
                            CtrllblRoomTypeName7.Tag = d("mstRoomTypeId")

                        End If
                        counter = counter + 1
                    Next
                ElseIf index = 7 Then  'room type 8
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx8, True)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue8.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue8.Text = d("rate").ToString + " INR"
                            CtrllblRoomTypeName8.Text = d("RoomTypeName")
                            CtrllblRoomTypeName8.Tag = d("mstRoomTypeId")

                        End If
                        counter = counter + 1
                    Next
                ElseIf index = 8 Then  'room type 9
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx9, True)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue9.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue9.Text = d("rate").ToString + " INR"
                            CtrllblRoomTypeName9.Text = d("RoomTypeName")
                            CtrllblRoomTypeName9.Tag = d("mstRoomTypeId")
                            CtrllblRoomNumber9.Text = ""

                        End If
                        counter = counter + 1
                    Next
                ElseIf index = 9 Then  'room type 10
                    VisibleTableLayoutPanel(TblLayoutPnlChkBx10, True)
                    Dim counter As Integer = 1
                    For Each d In xdtRoomTypeIdWise
                        If counter = 1 Then
                            CtrllblCostBeforePromoValue10.Text = d("rate").ToString + " INR"
                            CtrllblCostAfterPromoValue10.Text = d("rate").ToString + " INR"
                            CtrllblRoomTypeName10.Text = d("RoomTypeName")
                            CtrllblRoomTypeName10.Tag = d("mstRoomTypeId")
                            CtrllblRoomNumber10.Text = ""

                        End If
                        counter = counter + 1
                    Next
                End If
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Sub CheckedChangeCheckBox(ByRef chk As CheckBox, ByVal condition As Boolean)
        chk.Checked = condition
    End Sub
    'added by khusrao adil on 21-02-2017 from promotion check boxex visiblity
    Public Sub VisibleCheckBox(ByRef chk As CheckBox, ByVal condition As Boolean)
        chk.Visible = condition
    End Sub
    Public Sub HideTableLayoutPanle(ByVal condition As Boolean)
        TblLayoutPnlChkBx1.Visible = condition
        TblLayoutPnlChkBx2.Visible = condition
        TblLayoutPnlChkBx3.Visible = condition
        TblLayoutPnlChkBx4.Visible = condition
        TblLayoutPnlChkBx5.Visible = condition
        TblLayoutPnlChkBx6.Visible = condition
        TblLayoutPnlChkBx7.Visible = condition
        TblLayoutPnlChkBx8.Visible = condition
        TblLayoutPnlChkBx9.Visible = condition
        TblLayoutPnlChkBx10.Visible = condition
    End Sub
    'added by khusrao adil on 22-02-2017 from promotion table layout pnl visiblity
    Public Sub VisibleTableLayoutPanel(ByRef TblPnl As TableLayoutPanel, ByVal condition As Boolean)
        TblPnl.Visible = condition
        TblPnl.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset
    End Sub

    Public Sub UpdatePromoAppliedCount(ByVal roomTypeId As String, PromotionId As String)
        'dtRoomTypeRoomsCount
        For Each drRow In dtRoomTypeRoomsCount.Rows
            If drRow("mstRoomTypeId") = roomTypeId Then
                Dim value As Integer = Integer.Parse(roomTypeId)
                Select Case value
                    Case 1
                        CtrllblPromoAppliedValue1.Text = drRow("PromotionCount").ToString()
                    Case 2
                        CtrllblPromoAppliedValue2.Text = drRow("PromotionCount").ToString()
                    Case 3
                        CtrllblPromoAppliedValue3.Text = drRow("PromotionCount").ToString()
                    Case 4
                        CtrllblPromoAppliedValue4.Text = drRow("PromotionCount").ToString()
                    Case 5
                        CtrllblPromoAppliedValue5.Text = drRow("PromotionCount").ToString()
                    Case 6
                        CtrllblPromoAppliedValue6.Text = drRow("PromotionCount").ToString()
                    Case 7
                        CtrllblPromoAppliedValue7.Text = drRow("PromotionCount").ToString()
                    Case 8
                        CtrllblPromoAppliedValue8.Text = drRow("PromotionCount").ToString()
                    Case 9
                        CtrllblPromoAppliedValue9.Text = drRow("PromotionCount").ToString()
                    Case 10
                        CtrllblPromoAppliedValue10.Text = drRow("PromotionCount").ToString()
                    Case Else
                        'CtrllblPromoAppliedValue1.Text = "0"
                        'CtrllblPromoAppliedValue2.Text = "0"
                        'CtrllblPromoAppliedValue3.Text = "0"
                        'CtrllblPromoAppliedValue4.Text = "0"
                        'CtrllblPromoAppliedValue5.Text = "0"
                        'CtrllblPromoAppliedValue6.Text = "0"
                        'CtrllblPromoAppliedValue7.Text = "0"
                        'CtrllblPromoAppliedValue8.Text = "0"
                        'CtrllblPromoAppliedValue9.Text = "0"
                        'CtrllblPromoAppliedValue10.Text = "0"
                End Select
            End If
        Next
    End Sub
    '=========================================== checkbox event ===========================================

    Private Sub btnSaveAndCheckIn_Click(sender As Object, e As EventArgs) Handles btnSaveAndCheckIn.Click
        PrepareDataSave()
        If dsMainHostEdit.Tables("Host_ReservationGuestDetail").Rows.Count = 0 Then
            ShowMessage("please go back and enter the guest details before check-in", "Information")
            Exit Sub
        End If

        VReservationId = dsMainHostEdit.Tables("Host_Reservation").Rows(0)("reservationID").ToString()
        ReservationRNumber = dsMainHostEdit.Tables("Host_Reservation").Rows(0)("reservationNumber").ToString()
        'Dim disStatusMgs = "Reservation Saved Successfully for"  'for reservation
        '"Check-In Successfull for"  ' for check in
        'ReservationRNumber = "RN0010000000020"
        'ReservationGuestName = "Khusrao Khan"
        ReservationDate = System.DateTime.Now.ToString("dd/MM/yyyy")
        If SaveAndPrintReservation(True) = True Then
            If ReservationCheckIn(VReservationId) = True Then
                ShowMessage("Check In Successfully for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
            Else
                ShowMessage("Check In Failed for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
            End If
        Else
            ShowMessage("Check In Failed for " & vbCrLf & vbCrLf & "Guest Name :" & " " & ReservationGuestName & vbCrLf & "Res Id :" & " " & ReservationRNumber & vbCrLf & "Date :" & ReservationDate & vbCrLf, "Reservation Details")
        End If
        Me.Close()
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
                dtCheck(0)("RateDate") = cmdChkInDate.Text

                dtCheck(0)("RecordStatus") = "Updated"
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
                drRow("reservationId") = dtPromotions.Rows(0)("reservationId")
                drRow("yearInyyyy") = Date.Now.Year
                drRow("weekDay") = Date.Now.DayOfWeek
                drRow("RateDate") = cmdChkInDate.Text
                drRow("RecordStatus") = ""
                dtPromotions.Rows.Add(drRow)
            End If
            dtPromotions.AcceptChanges()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    'added by khusrao adil on 23-03-2017
    'Public Function RoomTypeWiseSelectedPromotions(ByVal DdtpromotionSelected As DataTable, Optional ByVal _mstRoomTypeIdParam As String = "", Optional ByVal _mstPromotionIdParam As String = "", Optional ByVal IsRemoved As Boolean = False)
    Public Function RoomTypeWiseSelectedPromotions(ByVal DdtpromotionSelected As DataTable, Optional ByVal _mstRoomTypeIdParam As String = "", Optional ByVal _mstPromotionIdParam As String = "", Optional ByVal IsRemoved As Boolean = False)
        Try
            Dim _mstRoomTypeId As String = "0"
            Dim _mstPromotionId As String = "0"
            Dim _reservationRoomTypePromotionMapID As String = "0"
            Dim _reservationId As String = "0"
            Dim _reservationRoomMapID As String = "0"
            For Each drRow As DataRow In DdtpromotionSelected.Rows
                If IsRemoved = False Then
                    _mstRoomTypeId = drRow("RoomTypeId").ToString()
                    _mstPromotionId = drRow("mstPromotionID").ToString()
                    _reservationRoomTypePromotionMapID = drRow("reservationRoomTypePromotionMapID").ToString()
                    _reservationId = drRow("reservationId").ToString()
                    _reservationRoomMapID = drRow("reservationRoomMapID").ToString()
                Else
                    _mstRoomTypeId = _mstRoomTypeIdParam
                    _mstPromotionId = _mstPromotionIdParam
                End If
                Dim _mstSiteRoomTypeMap As String = drRow("mstSiteRoomTypeMap").ToString()
                Dim _promotionName As String = drRow("mstPromotionName").ToString()
                Dim _discountInPercent As String = drRow("discountInPercent").ToString()
                Dim _rate As Decimal = drRow("Rate").ToString()
                Dim _discountInAmount As Decimal = drRow("discountAmount").ToString()
                Dim RecordStatus As Boolean = False
                Dim rowMTDt = dtPromotions.Select("RoomTypeId='" + _mstRoomTypeId + "' and mstPromotionID='" + _mstPromotionId + "'")
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
                        drPromo("reservationId") = _reservationId
                        drPromo("reservationRoomTypePromotionMapID") = _reservationRoomTypePromotionMapID
                        drPromo("reservationRoomMapID") = _reservationRoomMapID
                        drPromo("Rate") = _rate
                        ' If RecordStatus = False Then
                        '  drPromo("RecordStatus") = objHotelreservation.enumRecordStatus.Inserted.ToString()
                        ' Else
                        drPromo("RecordStatus") = objHotelreservation.enumRecordStatus.Updated.ToString()
                        '  drPromo("ReservationId") = dsMainHostEdit.Tables("Host_Reservation").Rows(0)("ReservationId").ToString()
                        'End If
                        dtPromotions.Rows.Add(drPromo)
                        dtPromotions.AcceptChanges()
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
            Next
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    Public Function RoomTypeWiseSelectedPromotions(ByVal _mstRoomTypeId As String, ByVal _mstPromotionId As String, ByVal _mstSiteRoomTypeMap As String, ByVal _promotionName As String,
                                 ByVal _discountInPercent As String, ByVal _rate As Decimal, _discountInAmount As Decimal, Optional ByVal IsRemoved As Boolean = False, Optional ByVal RecordStatus As Boolean = False)
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
                    If RecordStatus = False Then
                        drPromo("RecordStatus") = objHotelreservation.enumRecordStatus.Inserted.ToString()
                    Else
                        drPromo("RecordStatus") = objHotelreservation.enumRecordStatus.Updated.ToString()
                        drPromo("ReservationId") = dsMainHostEdit.Tables("Host_Reservation").Rows(0)("ReservationId").ToString()
                    End If
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
                    Dim rowMTDtPRO = dtPromotionTotalDiscountValues.Select("RoomTypeId='" + _mstRoomTypeId + "'")
                    If rowMTDtPRO.Length > 0 Then
                        For Each row As DataRow In rowMTDtPRO
                            dtPromotionTotalDiscountValues.Rows.Remove(row)
                        Next
                    End If
                    If ctrlComboBox.SelectedValue = Nothing Then
                        _mstPromotionId = ctrlComboBox.Tag
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

                    Else
                        lblPromoAppliedName.Text = "No Promotion"
                        lblCostAfterPromoValue.Text = lblCostBeforPromoValue.Text
                    End If
                End If
            Else

                AllPromotionTotalDiscountAmount = AllPromotionTotalDiscountAmount - _roomTypeDiscountAmountRemove
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
                Dim vl = dtPromotionTotalDiscountValues.Compute("SUM(TotalPromotionCost)", "")
                If vl.ToString() <> "" Then
                    PromotionAmountDetails(SelectedRoomsCost, vl)
                Else
                    PromotionAmountDetails(SelectedRoomsCost, "0")
                End If
            End If

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

    'added by khusrao adil on 20-03-2017 for hostmanagement design change
    Public Sub DefaultTheme()
        Me.PictureBox1.Image = Global.Spectrum.My.Resources.Resources.promotion_image
        Me.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
        ' create reservation
        Me.grdReservation.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.grdReservation.Size = New Size(1375, 263)
        Me.grdReservation.Styles.Fixed.BackColor = Color.FromArgb(208, 208, 208)
        Me.grdReservation.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.grdReservation.Styles.Fixed.Font = New Font("Callibri", 10, FontStyle.Bold)
        Me.grdReservation.Styles.Focus.Font = New Font("Callibri", 10, FontStyle.Bold)
        Me.grdReservation.Styles.Highlight.BackColor = Color.FromArgb(252, 252, 252)
        Me.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black

        ' grdGuestDetails
        Me.grdGuestDetails.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.grdGuestDetails.Size = New Size(1375, 263)
        Me.grdGuestDetails.Styles.Fixed.BackColor = Color.FromArgb(208, 208, 208)
        Me.grdGuestDetails.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.grdGuestDetails.Styles.Fixed.Font = New Font("Callibri", 10, FontStyle.Bold)
        Me.grdGuestDetails.Styles.Focus.Font = New Font("Callibri", 10, FontStyle.Bold)
        Me.grdGuestDetails.Styles.Highlight.BackColor = Color.FromArgb(252, 252, 252)

        ' gridSummaryDetails
        Me.gridSummaryDetails.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.gridSummaryDetails.Size = New Size(1375, 263)
        Me.gridSummaryDetails.Styles.Fixed.BackColor = Color.FromArgb(208, 208, 208)
        Me.gridSummaryDetails.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.gridSummaryDetails.Styles.Fixed.Font = New Font("Callibri", 10, FontStyle.Bold)
        Me.gridSummaryDetails.Styles.Focus.Font = New Font("Callibri", 10, FontStyle.Bold)
        Me.gridSummaryDetails.Styles.Highlight.BackColor = Color.FromArgb(252, 252, 252)

        ' cmdSearch
        Me.cmdSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdSearch.FlatStyle = FlatStyle.Flat
        Me.cmdSearch.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdSearch.FlatAppearance.BorderSize = 0
        Me.cmdSearch.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmdSearch.TextAlign = ContentAlignment.MiddleCenter
        Me.cmdSearch.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdSearch.ForeColor = Color.White

        ' btnPreviousTab
        Me.btnPreviousTab.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnPreviousTab.FlatStyle = FlatStyle.Flat
        Me.btnPreviousTab.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.btnPreviousTab.FlatAppearance.BorderSize = 0
        Me.btnPreviousTab.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.btnPreviousTab.TextAlign = ContentAlignment.MiddleCenter
        Me.btnPreviousTab.BackColor = Color.FromArgb(0, 113, 188)
        Me.btnPreviousTab.ForeColor = Color.White

        ' btnNextTab
        Me.btnNextTab.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnNextTab.FlatStyle = FlatStyle.Flat
        Me.btnNextTab.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.btnNextTab.FlatAppearance.BorderSize = 0
        Me.btnNextTab.BackColor = Color.FromArgb(0, 113, 188)
        Me.btnNextTab.TextAlign = ContentAlignment.MiddleCenter
        Me.btnNextTab.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.btnNextTab.ForeColor = Color.White

        ' btnSaveAndFinish
        Me.btnSaveAndFinish.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnSaveAndFinish.FlatStyle = FlatStyle.Flat
        Me.btnSaveAndFinish.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.btnSaveAndFinish.FlatAppearance.BorderSize = 0
        Me.btnSaveAndFinish.BackColor = Color.FromArgb(208, 208, 208)
        Me.btnSaveAndFinish.TextAlign = ContentAlignment.MiddleCenter
        Me.btnSaveAndFinish.Font = New Font("Callibri", 7)
        Me.btnSaveAndFinish.ForeColor = Color.White

        ' btnSaveAndCheckIn
        Me.btnSaveAndCheckIn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnSaveAndCheckIn.FlatStyle = FlatStyle.Flat
        Me.btnSaveAndCheckIn.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.btnSaveAndCheckIn.FlatAppearance.BorderSize = 0
        Me.btnSaveAndCheckIn.BackColor = Color.FromArgb(208, 208, 208)
        Me.btnSaveAndCheckIn.TextAlign = ContentAlignment.MiddleCenter
        Me.btnSaveAndCheckIn.Font = New Font("Callibri", 7)
        Me.btnSaveAndCheckIn.ForeColor = Color.White

        'Me.cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'Me.cmdCancel.FlatStyle = FlatStyle.Flat
        'Me.cmdCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        'Me.cmdCancel.FlatAppearance.BorderSize = 0
        'Me.cmdCancel.BackColor = Color.FromArgb(0, 113, 188)
        'Me.cmdCancel.TextAlign = ContentAlignment.MiddleCenter
        'Me.cmdCancel.Font = New Font("Callibri", 7, FontStyle.Bold)
        'Me.cmdCancel.ForeColor = Color.White

        ' btnGuestAdd
        Me.btnGuestAdd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnGuestAdd.FlatStyle = FlatStyle.Flat
        Me.btnGuestAdd.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.btnGuestAdd.FlatAppearance.BorderSize = 0
        Me.btnGuestAdd.BackColor = Color.FromArgb(0, 113, 188)
        Me.btnGuestAdd.ForeColor = Color.White
        Me.btnGuestAdd.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.btnGuestAdd.TextAlign = ContentAlignment.MiddleCenter

        'btnGuestClear
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
        ' payment 1
        Me.cmdPayment.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdPayment.FlatStyle = FlatStyle.Flat
        Me.cmdPayment.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdPayment.FlatAppearance.BorderSize = 0
        Me.cmdPayment.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmdPayment.TextAlign = ContentAlignment.MiddleCenter
        Me.cmdPayment.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdPayment.ForeColor = Color.White

        ' cmdCash
        Me.cmdCash.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdCash.FlatStyle = FlatStyle.Flat
        Me.cmdCash.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdCash.FlatAppearance.BorderSize = 0
        Me.cmdCash.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdCash.ForeColor = Color.White
        Me.cmdCash.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmdCash.TextAlign = ContentAlignment.MiddleCenter

        ' cmdCard
        Me.cmdCard.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdCard.FlatStyle = FlatStyle.Flat
        Me.cmdCard.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdCard.FlatAppearance.BorderSize = 0
        Me.cmdCard.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdCard.ForeColor = Color.White
        Me.cmdCard.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmdCard.TextAlign = ContentAlignment.MiddleCenter
        ' cmdCheque
        Me.cmdCheque.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdCheque.FlatStyle = FlatStyle.Flat
        Me.cmdCheque.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdCheque.FlatAppearance.BorderSize = 0
        Me.cmdCheque.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdCheque.ForeColor = Color.White
        Me.cmdCheque.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmdCheque.TextAlign = ContentAlignment.MiddleCenter

        ' btnCancel
        Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnCancel.FlatStyle = FlatStyle.Flat
        Me.btnCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.BackColor = Color.FromArgb(0, 113, 188)
        Me.btnCancel.TextAlign = ContentAlignment.MiddleCenter
        Me.btnCancel.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.btnCancel.ForeColor = Color.White
    End Sub

 
End Class
