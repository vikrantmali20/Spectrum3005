Imports Spectrum
Imports SpectrumBL
Imports C1.Win.C1FlexGrid

Public Class frmHostClosedViewReservation

#Region "Class"
    Dim dtViwCloseReservation As New DataTable
    Dim dtView As New DataTable
    Dim objHotelreservation As New clsHotelReservation

#End Region

#Region "Property"
    Dim _ReservationId As String
    Public Property ReservationId As String
        Get
            Return _ReservationId
        End Get
        Set(value As String)
            _ReservationId = value
        End Set
    End Property

    Dim _CheckIn As String
    Public Property CheckIn As String
        Get
            Return _CheckIn
        End Get
        Set(value As String)
            _CheckIn = value
        End Set
    End Property
    Dim _CheckOut As String
    Public Property CheckOut As String
        Get
            Return _CheckOut
        End Get
        Set(value As String)
            _CheckOut = value
        End Set
    End Property
    Dim _RoomType As String
    Public Property RoomType As String
        Get
            Return _RoomType
        End Get
        Set(value As String)
            _RoomType = value
        End Set
    End Property

#End Region

    Private Sub frmHostClosedViewReservation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
       
       
        BindViewReservation(ReservationId)
       
        gridSetting()
        loadTextValue()
        DefaultTheme()
       
    End Sub

    Public Sub DefaultTheme()

        'cmbRePrint
        Me.cmbRePrint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmbRePrint.FlatStyle = FlatStyle.Flat
        Me.cmbRePrint.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmbRePrint.FlatAppearance.BorderSize = 0
        Me.cmbRePrint.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmbRePrint.TextAlign = ContentAlignment.MiddleCenter
        Me.cmbRePrint.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmbRePrint.ForeColor = Color.White

        'cmbClose
        Me.cmbClose.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmbClose.FlatStyle = FlatStyle.Flat
        Me.cmbClose.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmbClose.FlatAppearance.BorderSize = 0
        Me.cmbClose.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmbClose.TextAlign = ContentAlignment.MiddleCenter
        Me.cmbClose.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmbClose.ForeColor = Color.White

    End Sub


    Public Function BindViewReservation(ByVal Id As String)
        Try
            dtViwCloseReservation = objHotelreservation.GetClosedGuestDetails(clsAdmin.SiteCode, ReservationId)
            If dtViwCloseReservation.Rows.Count > 0 Then
                grdCloseGuestDetails.DataSource = dtViwCloseReservation.DefaultView
                dtView = dtViwCloseReservation


            Else
                ShowMessage("Sorry, Something get wrong", "Information")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Sub loadTextValue()
        Try

            Dim guestName As String = ""
            Dim Email As String = ""
            Dim phone As String = ""
            Dim Adult As String = ""
            Dim NoOfNight As String = ""
            Dim NoOfChildren As String = ""
            Dim RoomNo As String = ""
            Dim totDiscount As String = ""
            Dim totTax As String = ""

            Dim totalCost As String = ""
            Dim FinalCost As String = ""
            Dim paidAmt As String = ""
            Dim RemainingAmt As String = ""
            If dtViwCloseReservation.Rows.Count > 0 Then
                For i = 0 To dtViwCloseReservation.Rows.Count - 1
                    If dtViwCloseReservation.Rows(i)("isPrimaryGuest") = True Then
                        guestName = dtViwCloseReservation.Rows(i)("GuestName")
                        Email = dtViwCloseReservation.Rows(i)("guestEmailID")
                        phone = dtViwCloseReservation.Rows(i)("PhoneNumber")

                        Adult = dtViwCloseReservation.Rows(i)("TotalNoOfAdults")
                        NoOfNight = dtViwCloseReservation.Rows(i)("totalNightsOfStay")
                        NoOfChildren = dtViwCloseReservation.Rows(i)("totalNoOfChildren")
                        RoomNo = dtViwCloseReservation.Rows(i)("RoomName")
                        totDiscount = dtViwCloseReservation.Rows(i)("totalPromotionDiscountAmount")
                        totTax = dtViwCloseReservation.Rows(i)("totalTaxAmount")
                        totalCost = dtViwCloseReservation.Rows(i)("totalNetAmount")
                        paidAmt = dtViwCloseReservation.Rows(i)("totalAmountPaid")
                        RemainingAmt = dtViwCloseReservation.Rows(i)("remainingAmountToPay")
                    End If

                Next
            End If
            txtguestName.Text = guestName
            txtEmail.Text = Email
            txtPhoneNo.Text = phone
            txtcheckin.Text = CheckIn
            txtcheckout.Text = CheckOut
            txtNumberOfAdult.Text = Adult
            txtNight.Text = NoOfNight
            txtChildren.Text = NoOfChildren
            txtRooms.Text = RoomNo
            txtDiscountAmount.Text = totDiscount
            txtTotalTaxAmount.Text = totTax
            txtFinalCost.Text = totalCost
            txtPaidAmt.Text = paidAmt
            txtPaymentRemaining.Text = RemainingAmt
            txtRoomType.Text = RoomType

            'txtguestName.ReadOnly = True
            'txtEmail.ReadOnly = True
            'txtPhoneNo.ReadOnly = True
            'txtcheckin.ReadOnly = True
            'txtcheckout.ReadOnly = True
            'txtNumberOfAdult.ReadOnly = True
            'txtNight.ReadOnly = True
            'txtChildren.ReadOnly = True
            'txtRooms.ReadOnly = True
            'txtDiscountAmount.ReadOnly = True
            'txtTotalTaxAmount.ReadOnly = True
            'txtFinalCost.ReadOnly = True
            'txtPaidAmt.ReadOnly = True
            'txtPaymentRemaining.ReadOnly = True
            'txtRoomType.ReadOnly = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub gridSetting()
        Try
            grdCloseGuestDetails.DataSource = dtView

            grdCloseGuestDetails.Cols("SrNo").Width = 50
            grdCloseGuestDetails.Cols("SrNo").Visible = False

            grdCloseGuestDetails.Cols("GuestName").Width = 125
            grdCloseGuestDetails.Cols("GuestName").DataType = Type.GetType("System.String")
            grdCloseGuestDetails.Cols("GuestName").AllowEditing = False
            grdCloseGuestDetails.Cols("GuestName").Name = "GuestName"
            grdCloseGuestDetails.Cols("GuestName").TextAlign = TextAlignEnum.LeftCenter

            grdCloseGuestDetails.Cols("PhoneNumber").Width = 100
            grdCloseGuestDetails.Cols("PhoneNumber").DataType = Type.GetType("System.String")
            grdCloseGuestDetails.Cols("PhoneNumber").AllowEditing = False
            grdCloseGuestDetails.Cols("PhoneNumber").Name = "PhoneNumber"
            grdCloseGuestDetails.Cols("PhoneNumber").TextAlign = TextAlignEnum.LeftCenter

            grdCloseGuestDetails.Cols("Age").Width = 100
            grdCloseGuestDetails.Cols("Age").DataType = Type.GetType("System.String")
            grdCloseGuestDetails.Cols("Age").AllowEditing = False
            grdCloseGuestDetails.Cols("Age").Name = "Age"
            grdCloseGuestDetails.Cols("Age").TextAlign = TextAlignEnum.LeftCenter

            grdCloseGuestDetails.Cols("Gender").Width = 125
            grdCloseGuestDetails.Cols("Gender").AllowEditing = False
            grdCloseGuestDetails.Cols("Gender").DataType = Type.GetType("System.String")

            grdCloseGuestDetails.Cols("Gender").Name = "Gender"
            grdCloseGuestDetails.Cols("Gender").TextAlign = TextAlignEnum.LeftCenter

            grdCloseGuestDetails.Cols("DocumentType").Width = 125
            grdCloseGuestDetails.Cols("DocumentType").AllowEditing = False
            grdCloseGuestDetails.Cols("DocumentType").DataType = Type.GetType("System.String")
            grdCloseGuestDetails.Cols("DocumentType").Name = "DocumentType"
            grdCloseGuestDetails.Cols("DocumentType").TextAlign = TextAlignEnum.LeftCenter

            grdCloseGuestDetails.Cols("Description").Width = 150
            grdCloseGuestDetails.Cols("Description").DataType = Type.GetType("System.String")
            grdCloseGuestDetails.Cols("Description").AllowEditing = False
            grdCloseGuestDetails.Cols("Description").Name = "Description"
            grdCloseGuestDetails.Cols("Description").TextAlign = TextAlignEnum.LeftCenter

            grdCloseGuestDetails.Cols("DocumentNo").Width = 100
            grdCloseGuestDetails.Cols("DocumentNo").AllowEditing = False
            grdCloseGuestDetails.Cols("DocumentNo").DataType = Type.GetType("System.Decimal")
            grdCloseGuestDetails.Cols("DocumentNo").Name = "DocumentNo"
            grdCloseGuestDetails.Cols("DocumentNo").TextAlign = TextAlignEnum.LeftCenter

            ' grdCloseGuestDetails.Cols("CardNo").Visible = False
            
        Catch ex As Exception
            LogException(ex)
        End Try


    End Sub

    Private Sub cmbRePrint_Click(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmbClose_Click(sender As Object, e As EventArgs)
        Try
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class