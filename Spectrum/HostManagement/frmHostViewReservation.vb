Imports SpectrumBL
Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient

Public Class frmHostViewReservation

#Region "Globle variables for class"
    ' Dim mobileNo As String
    'Dim ReservationId As String
    Dim Name As String = ""
    Dim dtViewReservation As New DataTable
    Dim objHotelreservation As New clsHotelReservation
    Dim dtView As New DataTable
    Dim payment As New frmHostCheckoutPayment
    Private m_ChildFormNumber As Integer
#End Region


#Region "Properties"

    Dim _MobileNo As String
    Public Property MobileNo As String
        Get
            Return _MobileNo
        End Get
        Set(value As String)
            _MobileNo = value
        End Set
    End Property

    Dim _ReservationId As String
    Public Property ReservationId As String
        Get
            Return _ReservationId
        End Get
        Set(value As String)
            _ReservationId = value
        End Set
    End Property

    Dim _Status As String
    Public Property status As String
        Get
            Return _Status
        End Get
        Set(value As String)
            _Status = value
        End Set
    End Property

#End Region

    ''Events

    Private Sub frmHostViewReservation_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        cmdViewClosedRes.Enabled = False
      
        cmdPaymentCheckout.Enabled = False
        cmdEditCheckin.Enabled = True
        cmdCancelReservation.Enabled = True
        cmdCancel.Enabled = True
        BindStatus()
        LoadViewReservationDetails()
        gridViewReservationDetailsSetting()
        DefaultTheme()
        Me.cmdPaymentCheckout.BackColor = Color.FromArgb(208, 208, 208)
    End Sub

    Public Sub DefaultTheme()

        'grdViewReservationDetails
        Me.grdViewReservationDetails.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        '  Me.grdViewReservationDetails.Size = New Size(1375, 263)
        Me.grdViewReservationDetails.Styles.Fixed.BackColor = Color.FromArgb(208, 208, 208)
        Me.grdViewReservationDetails.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.grdViewReservationDetails.Styles.Fixed.Font = New Font("Callibri", 10, FontStyle.Bold)
        Me.grdViewReservationDetails.Styles.Focus.Font = New Font("Callibri", 10, FontStyle.Bold)
        Me.grdViewReservationDetails.Styles.Highlight.BackColor = Color.FromArgb(252, 252, 252)
        Me.grdViewReservationDetails.BorderStyle = Util.BaseControls.BorderStyleEnum.None

        'cmdSearch
        Me.cmdSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdSearch.FlatStyle = FlatStyle.Flat
        Me.cmdSearch.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdSearch.FlatAppearance.BorderSize = 0
        Me.cmdSearch.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmdSearch.TextAlign = ContentAlignment.MiddleCenter
        Me.cmdSearch.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdSearch.ForeColor = Color.White

        'cmdClear
        Me.cmdClear.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdClear.FlatStyle = FlatStyle.Flat
        Me.cmdClear.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdClear.FlatAppearance.BorderSize = 0
        Me.cmdClear.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmdClear.TextAlign = ContentAlignment.MiddleCenter
        Me.cmdClear.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdClear.ForeColor = Color.White

        'cmdViewClosedRes
        Me.cmdViewClosedRes.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdViewClosedRes.FlatStyle = FlatStyle.Flat
        Me.cmdViewClosedRes.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdViewClosedRes.FlatAppearance.BorderSize = 0
        Me.cmdViewClosedRes.Font = New Font("Callibri", 7, FontStyle.Bold)   ''153, 198, 228
        Me.cmdViewClosedRes.TextAlign = ContentAlignment.MiddleCenter
        Me.cmdViewClosedRes.BackColor = Color.FromArgb(208, 208, 208)
        Me.cmdViewClosedRes.ForeColor = Color.White  ''Color.FromArgb(255, 255, 255)

        'cmdPaymentCheckout
        Me.cmdPaymentCheckout.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdPaymentCheckout.FlatStyle = FlatStyle.Flat
        Me.cmdPaymentCheckout.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdPaymentCheckout.FlatAppearance.BorderSize = 0
        Me.cmdPaymentCheckout.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmdPaymentCheckout.TextAlign = ContentAlignment.MiddleCenter
        Me.cmdPaymentCheckout.BackColor = Color.FromArgb(208, 208, 208)
        Me.cmdPaymentCheckout.ForeColor = Color.White

        'cmdEditCheckin
        Me.cmdEditCheckin.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdEditCheckin.FlatStyle = FlatStyle.Flat
        Me.cmdEditCheckin.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdEditCheckin.FlatAppearance.BorderSize = 0
        Me.cmdEditCheckin.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmdEditCheckin.TextAlign = ContentAlignment.MiddleCenter
        Me.cmdEditCheckin.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdEditCheckin.ForeColor = Color.White

        'cmdCancelReservation
        Me.cmdCancelReservation.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdCancelReservation.FlatStyle = FlatStyle.Flat
        Me.cmdCancelReservation.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdCancelReservation.FlatAppearance.BorderSize = 0
        Me.cmdCancelReservation.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmdCancelReservation.TextAlign = ContentAlignment.MiddleCenter
        Me.cmdCancelReservation.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdCancelReservation.ForeColor = Color.White

        'cmdCancel
        Me.cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdCancel.FlatStyle = FlatStyle.Flat
        Me.cmdCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmdCancel.TextAlign = ContentAlignment.MiddleCenter
        Me.cmdCancel.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdCancel.ForeColor = Color.White

    End Sub

    Private Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        Try
            LoadViewReservationDetails()
            gridViewReservationDetailsSetting()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub LoadViewReservationDetails()
        Try
            Dim PhoneNumber As String = ""
            Dim ReservedID As String = ""
            Dim gstName As String = ""

            If txtMobileNo.Text.Trim <> "" Then
                PhoneNumber = txtMobileNo.Text.Trim
            End If
            If txtReservationId.Text.Trim <> "" Then
                ReservedID = txtReservationId.Text
            End If
            If txtName.Text.Trim <> "" Then
                gstName = txtName.Text
            End If

            status = CmbStatus.SelectedValue

            Call bindRoomReservation(status, ReservedID, PhoneNumber, gstName)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        Try
            txtMobileNo.Clear()
            txtName.Clear()
            txtReservationId.Clear()
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub cmdEditCheckin_Click(sender As Object, e As EventArgs) Handles cmdEditCheckin.Click
        Try
            Dim editForm As New frmHostEditCheckinReservation
            Dim VReservationId As String = ""
            Dim number = dtViewReservation.Select("Selects=true").Length
            If number > 0 Then

                If dtViewReservation.Rows.Count > 0 Then
                    For i = 0 To dtViewReservation.Rows.Count - 1
                        If dtViewReservation.Rows(i)("Selects") = True Then
                            VReservationId = dtViewReservation.Rows(i)("ReservationId")
                        End If
                    Next
                    If VReservationId <> "" Then
                        'editForm = New frmHostEditCheckinReservation
                        editForm.VReservationId = VReservationId
                        editForm.ShowDialog()
                        frmHostViewReservation_Load_1(Nothing, Nothing)
                    Else
                        editForm.Dispose()
                        ShowMessage("Please select reservation", "Information")
                        Exit Sub
                    End If
                    ' payment.bindCheckoutGuestDetails(ReservationId, MobileNo)
                End If
            Else
                ShowMessage("please select room first", "Information")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub grdViewReservationDetails_AfterEdit(sender As Object, e As RowColEventArgs) Handles grdViewReservationDetails.AfterEdit
        Try
            If grdViewReservationDetails.Cols(e.Col).Name = "Selects" Then
                For i = 1 To grdViewReservationDetails.Rows.Count - 1
                    If e.Row <> i Then
                        grdViewReservationDetails.Rows(i)("Selects") = False
                    End If
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub
   
    Private Sub cmdPaymentCheckout_Click(sender As Object, e As EventArgs) Handles cmdPaymentCheckout.Click
        Try
            Dim servicePayment As New frmHostCheckoutPayment
            Dim number As Integer = dtViewReservation.Select("Selects=true").Length
            If number > 0 Then
                For i = 0 To dtViewReservation.Rows.Count - 1
                    If dtViewReservation.Rows(i)("Selects") = True Then
                        servicePayment.ReservationId = dtViewReservation.Rows(i)("reservationId")
                        servicePayment.MobileNo = dtViewReservation.Rows(i)("guestMobileNumber")
                        servicePayment.CheckIn = dtViewReservation.Rows(i)("CheckIN")
                        servicePayment.CheckOut = dtViewReservation.Rows(i)("CheckOut")
                        servicePayment.Adult = dtViewReservation.Rows(i)("totalNoOfAdults")
                        servicePayment.Children = dtViewReservation.Rows(i)("totalNoOfChildren")
                        servicePayment.totDiscountAmt = dtViewReservation.Rows(i)("TotalDiscount")
                        servicePayment.totalTax = dtViewReservation.Rows(i)("TotalTax")
                        servicePayment.totalFinalCost = dtViewReservation.Rows(i)("TotalCost")
                        servicePayment.totalPaidAmt = dtViewReservation.Rows(i)("PaidAmount")
                        servicePayment.totRemainingAmt = dtViewReservation.Rows(i)("RemainingAmount")

                    End If
                Next
                servicePayment.tpGuesDetails.Show()
               
                servicePayment.ShowDialog()
                CmbStatus_TextChanged(sender, e)
            Else
                ShowMessage("please select room first", "Information")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub cmdCancelReservation_Click(sender As Object, e As EventArgs) Handles cmdCancelReservation.Click
        Try
            Dim tran As SqlTransaction = Nothing
            Dim scon = New SqlConnection
            Dim eventType As Int32
            Dim number = dtViewReservation.Select("Selects=true").Length
            If number > 0 Then


                If dtViewReservation.Rows.Count > 0 Then
                    For i = 0 To dtViewReservation.Rows.Count - 1

                        If dtViewReservation.Rows(i)("Selects") = True Then
                            Dim Id As String = dtViewReservation.Rows(i)("ReservationId")
                            Dim guestName As String = dtViewReservation.Rows(i)("guestName")
                            Dim ReservationDate As String = dtViewReservation.Rows(i)("ReservationDate")
                            Dim PaidAmt As String = dtViewReservation.Rows(i)("PaidAmount")
                            ShowMessage("Are You Sure you want to cancel selected Reservation " & vbCrLf & "Reservation ID :" & Id & vbCrLf & "Name :" & " " & guestName & vbCrLf & "Reservation Date :" & " " & ReservationDate, "Cancel Confirmation", eventType, "No", "Yes")
                            If eventType = 1 Then
                                Dim ReservationStatus As New DataTable
                                ReservationStatus = objHotelreservation.getStatusType(objHotelreservation.enumStatusTypes.RESERVATION_STATUS.ToString(), objHotelreservation.enumStatus.CANCELLED.ToString())
                                'If ReservationStatus Then

                                'End If

                                If objHotelreservation.UpdateReservationDetails(clsAdmin.SiteCode, Id, ReservationStatus(0)("mstStatusID"), clsAdmin.UserCode, tran) = False Then
                                    tran.Rollback()
                                    scon.Close()
                                End If
                                ShowMessage("Refund the amount of " & "" + PaidAmt + "" & " " & "to :" & vbCrLf & "Name :" & " " & guestName & vbCrLf & "" & "Reservation Id:" & "" & Id, "Information")
                                LoadViewReservationDetails()
                                ' this.close()

                                ' Me.Close()
                            End If

                        End If
                    Next
                End If
            Else
                ShowMessage("please select room first", "Information")
            End If

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub CmbStatus_TextChanged(sender As Object, e As EventArgs) Handles CmbStatus.TextChanged
        Try
            Dim status = CmbStatus.Text
            If status = "CANCELLED" Then  ''Or status = "CHECKED_IN" Then
                Me.cmdViewClosedRes.Enabled = False
                Me.cmdEditCheckin.Enabled = False
                Me.cmdCancel.Enabled = False
                Me.cmdCancelReservation.Enabled = False
                Me.cmdPaymentCheckout.Enabled = False
                Me.cmdViewClosedRes.BackColor = Color.FromArgb(208, 208, 208)
                Me.cmdEditCheckin.BackColor = Color.FromArgb(208, 208, 208)
                Me.cmdCancel.BackColor = Color.FromArgb(208, 208, 208)
                Me.cmdCancelReservation.BackColor = Color.FromArgb(208, 208, 208)
                Me.cmdPaymentCheckout.BackColor = Color.FromArgb(208, 208, 208)
                
                Me.cmdViewClosedRes.Enabled = True
                Me.cmdViewClosedRes.BackColor = Color.FromArgb(0, 113, 188)
                ' LoadViewReservationDetails()
            End If

            If status = "RESERVED" Then
                Me.cmdViewClosedRes.Enabled = False
                Me.cmdPaymentCheckout.Enabled = False
                Me.cmdViewClosedRes.BackColor = Color.FromArgb(208, 208, 208)
                Me.cmdPaymentCheckout.BackColor = Color.FromArgb(208, 208, 208)

                Me.cmdEditCheckin.Text = "Edit/Check-in"
                Me.cmdEditCheckin.Enabled = True
                Me.cmdCancel.Enabled = True
                Me.cmdCancelReservation.Enabled = True
                Me.cmdEditCheckin.BackColor = Color.FromArgb(0, 113, 188)
                Me.cmdCancel.BackColor = Color.FromArgb(0, 113, 188)
                Me.cmdCancelReservation.BackColor = Color.FromArgb(0, 113, 188)
                
            End If


            If status = "CHECKED_IN" Then

                Me.cmdEditCheckin.Text = "Manage"

                Me.cmdViewClosedRes.Enabled = False
                Me.cmdCancelReservation.Enabled = False
                Me.cmdViewClosedRes.BackColor = Color.FromArgb(208, 208, 208)
                Me.cmdCancelReservation.BackColor = Color.FromArgb(208, 208, 208)

                Me.cmdEditCheckin.Enabled = True
                Me.cmdPaymentCheckout.Enabled = True
                Me.cmdCancel.Enabled = True
                Me.cmdEditCheckin.BackColor = Color.FromArgb(0, 113, 188)
                Me.cmdPaymentCheckout.BackColor = Color.FromArgb(0, 113, 188)
                Me.cmdCancel.BackColor = Color.FromArgb(0, 113, 188)

            End If
            If status = "CHECKED_OUT" Then
                Me.cmdViewClosedRes.Enabled = False
                Me.cmdEditCheckin.Enabled = False
                Me.cmdCancel.Enabled = False
                Me.cmdCancelReservation.Enabled = False
                Me.cmdPaymentCheckout.Enabled = False
                Me.cmdViewClosedRes.BackColor = Color.FromArgb(208, 208, 208)
                Me.cmdEditCheckin.BackColor = Color.FromArgb(208, 208, 208)
                Me.cmdCancel.BackColor = Color.FromArgb(208, 208, 208)
                Me.cmdCancelReservation.BackColor = Color.FromArgb(208, 208, 208)
                Me.cmdPaymentCheckout.BackColor = Color.FromArgb(208, 208, 208)

                Me.cmdViewClosedRes.Enabled = True
                Me.cmdViewClosedRes.BackColor = Color.FromArgb(0, 113, 188)
            End If
            LoadViewReservationDetails()

        Catch ex As Exception
            LogException(ex)
        End Try


    End Sub

    ''End Events

    ''Functions
    Private Sub bindRoomReservation(ByVal Status As String, Optional ByVal ReservationId As String = Nothing, Optional ByVal MobileNo As String = "", Optional ByVal Name As String = "")
        Try
            dtViewReservation = objHotelreservation.GetViewReservationDetails(clsAdmin.SiteCode, Status, ReservationId, MobileNo, Name)
            grdViewReservationDetails.DataSource = dtViewReservation.DefaultView
            dtView = dtViewReservation
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub BindStatus()
        Try
            Dim dt As DataTable
            CmbStatus.Text = ""
            dt = objHotelreservation.getStatusType("RESERVATION_STATUS")
            If Not dt Is Nothing And dt.Rows.Count > 0 Then
                CmbStatus.DataSource = dt
                CmbStatus.DisplayMember = dt.Columns("name").ToString()
                CmbStatus.ValueMember = dt.Columns("mstStatusID").ToString()
                CmbStatus.ExtendRightColumn = True
                For Each r As C1.Win.C1List.Split In CmbStatus.Splits
                    Dim i As Integer
                    For i = 0 To r.DisplayColumns.Count - 1
                        If r.DisplayColumns(i).Name <> CmbStatus.DisplayMember Then
                            r.DisplayColumns(i).Visible = False
                        End If
                    Next
                Next
                CmbStatus.SelectedIndex = 3
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub gridViewReservationDetailsSetting()
        Try
            grdViewReservationDetails.DataSource = dtView
            grdViewReservationDetails.Cols("Selects").DataType = Type.GetType("System.Boolean")
            grdViewReservationDetails.Cols("Selects").AllowEditing = True
            grdViewReservationDetails.Cols("Selects").Caption = ""
            grdViewReservationDetails.Cols("Selects").Width = 50

            grdViewReservationDetails.Cols("ReservationDate").Width = 125
            grdViewReservationDetails.Cols("ReservationDate").DataType = Type.GetType("System.String")
            grdViewReservationDetails.Cols("ReservationDate").AllowEditing = False
            grdViewReservationDetails.Cols("ReservationDate").Name = "ReservationDate"
            grdViewReservationDetails.Cols("ReservationDate").TextAlign = TextAlignEnum.LeftCenter


            grdViewReservationDetails.Cols("CheckIN").Width = 100
            grdViewReservationDetails.Cols("CheckIN").DataType = Type.GetType("System.String")
            grdViewReservationDetails.Cols("CheckIN").AllowEditing = False
            grdViewReservationDetails.Cols("CheckIN").Name = "CheckIN"
            grdViewReservationDetails.Cols("CheckIN").TextAlign = TextAlignEnum.LeftCenter


            grdViewReservationDetails.Cols("CheckOut").Width = 100
            grdViewReservationDetails.Cols("CheckOut").DataType = Type.GetType("System.String")
            grdViewReservationDetails.Cols("CheckOut").AllowEditing = False
            grdViewReservationDetails.Cols("CheckOut").Name = "CheckOut"
            grdViewReservationDetails.Cols("CheckOut").TextAlign = TextAlignEnum.LeftCenter

            grdViewReservationDetails.Cols("Rooms").Width = 125
            grdViewReservationDetails.Cols("Rooms").AllowEditing = False
            grdViewReservationDetails.Cols("Rooms").DataType = Type.GetType("System.String")

            grdViewReservationDetails.Cols("Rooms").Name = "Rooms"
            grdViewReservationDetails.Cols("Rooms").TextAlign = TextAlignEnum.LeftCenter

            grdViewReservationDetails.Cols("RoomType").Width = 100
            grdViewReservationDetails.Cols("RoomType").AllowEditing = False
            grdViewReservationDetails.Cols("RoomType").DataType = Type.GetType("System.String")

            grdViewReservationDetails.Cols("RoomType").Name = "RoomType"
            grdViewReservationDetails.Cols("RoomType").TextAlign = TextAlignEnum.LeftCenter

            grdViewReservationDetails.Cols("STATUS").Width = 150
            grdViewReservationDetails.Cols("STATUS").DataType = Type.GetType("System.String")
            grdViewReservationDetails.Cols("STATUS").AllowEditing = False
            grdViewReservationDetails.Cols("STATUS").Name = "STATUS"
            grdViewReservationDetails.Cols("STATUS").TextAlign = TextAlignEnum.LeftCenter

            grdViewReservationDetails.Cols("PaidAmount").Width = 100
            grdViewReservationDetails.Cols("PaidAmount").AllowEditing = False
            grdViewReservationDetails.Cols("PaidAmount").DataType = Type.GetType("System.Decimal")
            grdViewReservationDetails.Cols("PaidAmount").Format = "0"
            grdViewReservationDetails.Cols("PaidAmount").Name = "PaidAmount"
            grdViewReservationDetails.Cols("PaidAmount").TextAlign = TextAlignEnum.LeftCenter

            grdViewReservationDetails.Cols("RemainingAmount").Width = 100
            grdViewReservationDetails.Cols("RemainingAmount").DataType = Type.GetType("System.String")
            grdViewReservationDetails.Cols("RemainingAmount").AllowEditing = False
            grdViewReservationDetails.Cols("RemainingAmount").Name = "RemainingAmount"
            grdViewReservationDetails.Cols("RemainingAmount").TextAlign = TextAlignEnum.LeftCenter

            If CmbStatus.Text = "CANCELLED" Then
                grdViewReservationDetails.Cols("CancelledOn").Visible = True
                grdViewReservationDetails.Cols("CancelledOn").Width = 140
                grdViewReservationDetails.Cols("CancelledOn").DataType = Type.GetType("System.String")
                grdViewReservationDetails.Cols("CancelledOn").AllowEditing = False
                grdViewReservationDetails.Cols("CancelledOn").Name = "CancelledOn"
                grdViewReservationDetails.Cols("CancelledOn").TextAlign = TextAlignEnum.LeftCenter

                grdViewReservationDetails.Cols("AmountRefunded").Visible = True
                grdViewReservationDetails.Cols("AmountRefunded").Width = 140
                grdViewReservationDetails.Cols("AmountRefunded").DataType = Type.GetType("System.String")
                grdViewReservationDetails.Cols("AmountRefunded").AllowEditing = False
                grdViewReservationDetails.Cols("AmountRefunded").Name = "AmountRefunded"
                grdViewReservationDetails.Cols("AmountRefunded").TextAlign = TextAlignEnum.LeftCenter
            Else
                grdViewReservationDetails.Cols("AmountRefunded").Visible = False
                grdViewReservationDetails.Cols("CancelledOn").Visible = False
            End If
            grdViewReservationDetails.Cols("ID").Width = 100
            grdViewReservationDetails.Cols("ID").DataType = Type.GetType("System.String")
            grdViewReservationDetails.Cols("ID").AllowEditing = False
            grdViewReservationDetails.Cols("ID").Visible = False
            grdViewReservationDetails.Cols("ID").Name = "ID"
            grdViewReservationDetails.Cols("ID").TextAlign = TextAlignEnum.LeftCenter

            'grdViewReservationDetails.Cols("guestFirstName").Width = 100
            'grdViewReservationDetails.Cols("guestFirstName").DataType = Type.GetType("System.String")
            'grdViewReservationDetails.Cols("guestFirstName").Visible = False
            'grdViewReservationDetails.Cols("guestFirstName").Name = "guestFirstName"
            'grdViewReservationDetails.Cols("guestFirstName").TextAlign = TextAlignEnum.LeftCenter

            'grdViewReservationDetails.Cols("guestMobileNumber").Width = 100
            'grdViewReservationDetails.Cols("guestMobileNumber").DataType = Type.GetType("System.String")
            'grdViewReservationDetails.Cols("guestMobileNumber").Visible = False
            'grdViewReservationDetails.Cols("guestMobileNumber").Name = "guestMobileNumber"
            'grdViewReservationDetails.Cols("guestMobileNumber").TextAlign = TextAlignEnum.LeftCenter

            'grdViewReservationDetails.Cols("mstStatusID").Width = 100
            'grdViewReservationDetails.Cols("mstStatusID").DataType = Type.GetType("System.String")
            'grdViewReservationDetails.Cols("mstStatusID").Visible = False
            'grdViewReservationDetails.Cols("mstStatusID").Name = "mstStatusID"
            'grdViewReservationDetails.Cols("mstStatusID").TextAlign = TextAlignEnum.LeftCenter

            'grdViewReservationDetails.Cols("name").Width = 100
            'grdViewReservationDetails.Cols("name").DataType = Type.GetType("System.String")
            'grdViewReservationDetails.Cols("name").Visible = False
            'grdViewReservationDetails.Cols("name").Name = "name"
            'grdViewReservationDetails.Cols("name").TextAlign = TextAlignEnum.LeftCenter

            'grdViewReservationDetails.Cols("reservationStatusID").Width = 100
            'grdViewReservationDetails.Cols("reservationStatusID").DataType = Type.GetType("System.String")
            'grdViewReservationDetails.Cols("reservationStatusID").Visible = False
            'grdViewReservationDetails.Cols("reservationStatusID").Name = "reservationStatusID"
            'grdViewReservationDetails.Cols("reservationStatusID").TextAlign = TextAlignEnum.LeftCenter

            'grdViewReservationDetails.Cols("name1").Width = 100
            'grdViewReservationDetails.Cols("name1").DataType = Type.GetType("System.String")
            'grdViewReservationDetails.Cols("name1").Visible = False
            'grdViewReservationDetails.Cols("name1").Name = "name1"
            'grdViewReservationDetails.Cols("name1").TextAlign = TextAlignEnum.LeftCenter




        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    ''End Functions




    Private Sub cmdViewClosedRes_Click(sender As Object, e As EventArgs) Handles cmdViewClosedRes.Click
        Try
            Dim ClosedReservation As New frmHostClosedViewReservation
            Dim number = dtViewReservation.Select("Selects=true").Length
            If number > 0 Then
                For i = 0 To dtViewReservation.Rows.Count - 1

                    If dtViewReservation.Rows(i)("Selects") = True Then
                        ClosedReservation.ReservationId = dtViewReservation.Rows(i)("ReservationId")
                        ClosedReservation.CheckIn = dtViewReservation.Rows(i)("CheckIN")
                        ClosedReservation.CheckOut = dtViewReservation.Rows(i)("CheckOut")
                        ClosedReservation.RoomType = dtViewReservation.Rows(i)("RoomType")
                        ClosedReservation.ShowDialog()
                    End If
                Next
            Else
                ShowMessage("please select room first", "Information")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


End Class