Imports SpectrumBL
Imports System.Data.SqlClient

Public Class frmHostMakePayment
#Region "Global variable for Class"
    Public _remarks As String
    Private _strRemarks As String
    Public _paidAmt As String
    Public _billAmt As String
    Dim RemainingAmt As Integer
    Dim frmPayment As New frmHostCreateReservation
    Dim objHotelreservation As New clsHotelReservation
    Dim isCashierPromoSelected As Boolean
    Dim UpdateFlag As Boolean = False
    Dim dsMainHost As New DataSet
    Dim PaymentTermId As String = ""
    Dim GiftMsg As String = ""
    Private _dDueDate As Date
    Dim makePayment As Boolean = False
    Dim ds As DataSet
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
    Dim _CheckIn As DateTime
    Public Property CheckIn As DateTime
        Get
            Return _CheckIn
        End Get
        Set(value As DateTime)
            _CheckIn = value
        End Set
    End Property
    Dim _CheckOut As DateTime
    Public Property CheckOut As DateTime
        Get
            Return _CheckOut
        End Get
        Set(value As DateTime)
            _CheckOut = value
        End Set

    End Property
    Dim _Adult As String
    Public Property Adult As String
        Get
            Return _Adult
        End Get
        Set(value As String)
            _Adult = value
        End Set

    End Property
    Dim _Children As String
    Public Property Children As String
        Get
            Return _Children
        End Get
        Set(value As String)
            _Children = value
        End Set

    End Property
    Dim _totalTax As String
    Public Property totalTax As String
        Get
            Return _totalTax
        End Get
        Set(value As String)
            _totalTax = value
        End Set

    End Property
    Dim _totalPaidAmt As String
    Public Property totalPaidAmt As String
        Get
            Return _totalPaidAmt
        End Get
        Set(value As String)
            _totalPaidAmt = value
        End Set

    End Property
    Dim _totalFinalCost As String
    Public Property totalFinalCost As String
        Get
            Return _totalFinalCost
        End Get
        Set(value As String)
            _totalFinalCost = value
        End Set

    End Property
    Dim _totDiscountAmt As String
    Public Property totDiscountAmt As String
        Get
            Return _totDiscountAmt
        End Get
        Set(value As String)
            _totDiscountAmt = value
        End Set

    End Property
    Dim _totRemainingAmt As String
    Public Property totRemainingAmt As String
        Get
            Return _totRemainingAmt
        End Get
        Set(value As String)
            _totRemainingAmt = value
        End Set

    End Property
    Dim _guestName As String
    Public Property guestName As String
        Get
            Return _guestName
        End Get
        Set(value As String)
            _guestName = value
        End Set

    End Property
    Dim _PhoneNumber As String
    Public Property PhoneNumber As String
        Get
            Return _PhoneNumber
        End Get
        Set(value As String)
            _PhoneNumber = value
        End Set

    End Property
    Dim _email As String
    Public Property email As String
        Get
            Return _email
        End Get
        Set(value As String)
            _email = value
        End Set

    End Property

    Dim _serviceCharge As Decimal
    Public Property serviceCharge As Decimal
        Get
            Return _serviceCharge
        End Get
        Set(value As Decimal)
            _serviceCharge = value
        End Set
    End Property
    Dim _dtService As New DataTable
    Public Property dtService() As DataTable
        Get
            Return _dtService
        End Get

        Set(value As DataTable)
            _dtService = value
        End Set
    End Property


#End Region
    ''Page Events



    Private Sub frmHostMakePayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtTotalDiscountAmt.Text = totDiscountAmt
        txtTotalTaxAmount.Text = totalTax
        txtTotalFinalCost.Text = totalFinalCost
        txtTotalPaidAmount.Text = totalPaidAmt
        txtTotPaymentRemaining.Text = totRemainingAmt
        'txtTotalDiscountAmt.ReadOnly = True
        'txtTotalTaxAmount.ReadOnly = True
        'txtTotalFinalCost.ReadOnly = True
        'txtTotalPaidAmount.ReadOnly = True
        'txtTotPaymentRemaining.ReadOnly = True

        lblCheckInVal.Text = CheckIn
        lblCheckOutVal.Text = CheckOut
        lblCustNameVal.Text = guestName
        lblPhonenoVal.Text = PhoneNumber
        lblEmailVal.Text = email
        lblAdultVal.Text = Adult
        lblChildrenVal.Text = Children
        cmdPrintBill.Enabled = False

        DefaultTheme()
    End Sub

    Public Sub DefaultTheme()
        Me.cmdCard.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdCard.FlatStyle = FlatStyle.Flat
        Me.cmdCard.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdCard.FlatAppearance.BorderSize = 0
        Me.cmdCard.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdCard.ForeColor = Color.White
        Me.cmdCard.TextAlign = ContentAlignment.MiddleCenter

        Me.cmdCash.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdCash.FlatStyle = FlatStyle.Flat
        Me.cmdCash.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdCash.FlatAppearance.BorderSize = 0
        Me.cmdCash.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdCash.ForeColor = Color.White
        Me.cmdCash.TextAlign = ContentAlignment.MiddleCenter



        Me.cmdCheckout.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdCheckout.FlatStyle = FlatStyle.Flat
        Me.cmdCheckout.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdCheckout.FlatAppearance.BorderSize = 0
        Me.cmdCheckout.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdCheckout.ForeColor = Color.FromArgb(255, 255, 255)
        Me.cmdCheckout.TextAlign = ContentAlignment.MiddleCenter

        Me.cmdPayment.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdPayment.FlatStyle = FlatStyle.Flat
        Me.cmdPayment.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdPayment.FlatAppearance.BorderSize = 0
        Me.cmdPayment.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdPayment.ForeColor = Color.FromArgb(255, 255, 255)
        Me.cmdPayment.TextAlign = ContentAlignment.MiddleCenter

        Me.cmdSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdSave.FlatStyle = FlatStyle.Flat
        Me.cmdSave.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdSave.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.cmdSave.FlatAppearance.BorderSize = 0
        Me.cmdSave.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdSave.ForeColor = Color.White
        Me.cmdSave.TextAlign = ContentAlignment.MiddleCenter

        Me.cmdCheque.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdCheque.FlatStyle = FlatStyle.Flat
        Me.cmdCheque.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdCheque.FlatAppearance.BorderSize = 0
        Me.cmdCheque.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdCheque.ForeColor = Color.White
        Me.cmdCheque.TextAlign = ContentAlignment.MiddleCenter

        Me.cmdPrintBill.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdPrintBill.FlatStyle = FlatStyle.Flat
        Me.cmdPrintBill.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdPrintBill.Font = New Font("Callibri", 7)
        Me.cmdPrintBill.FlatAppearance.BorderSize = 0
        Me.cmdPrintBill.BackColor = Color.FromArgb(208, 208, 208)
        Me.cmdPrintBill.ForeColor = Color.White
        Me.cmdPrintBill.TextAlign = ContentAlignment.MiddleCenter
    End Sub

    Private Sub frmHostMakePayment_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
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
        Catch ex As Exception
            LogException(ex)

        End Try
    End Sub

    ''end 


    Private Sub cmdPrintBill_Click(sender As Object, e As EventArgs) Handles cmdPrintBill.Click
        Try

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Try
            Dim remianingamt As Double = txtTotPaymentRemaining.Text
            Dim paidAmt As Double
            If _paidAmt <> "0.0" Then
                paidAmt = txtTotalPaidAmount.Text
            Else
                paidAmt = _paidAmt
            End If
            PrepareDataSave(True)
            If SaveServices(True) = True Then
                If objHotelreservation.updatebalamt(ReservationId, clsAdmin.SiteCode, remianingamt, paidAmt) Then
                    ShowMessage("Service saved successfully", "Information")
                End If
            Else
                ShowMessage("Service saved failed", "Information")
            End If
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdCheckout_Click(sender As Object, e As EventArgs) Handles cmdCheckout.Click
        Try
            Dim tran As SqlTransaction = Nothing
            Dim scon = New SqlConnection
            Dim remianingamt As Double = txtTotPaymentRemaining.Text
            Dim paidAmt As Double
            If _paidAmt <> "0.0" Then
                paidAmt = txtTotalPaidAmount.Text
            Else
                paidAmt = _paidAmt
            End If

            If CInt(txtTotPaymentRemaining.Text) = 0 Then
                makePayment = True
            End If
            If makePayment = True Then
                '  Dim status As String = "9"
                Dim ReservationStatus As DataTable = objHotelreservation.getStatusType(objHotelreservation.enumStatusTypes.RESERVATION_STATUS.ToString(), objHotelreservation.enumStatus.CHECKED_OUT.ToString())
                Dim status As Integer = ReservationStatus.Rows(0)("mstStatusID")
                If objHotelreservation.UpdateReservationDetails(clsAdmin.SiteCode, ReservationId, status, clsAdmin.UserName, tran) = False Then
                    tran.Rollback()
                    scon.Close()
                Else
                    If objHotelreservation.updatebalamt(ReservationId, clsAdmin.SiteCode, remianingamt, paidAmt) Then
                        ShowMessage("Check-Out Successfully for " & vbCrLf & "Guest Name :" & " " & guestName & vbCrLf & "Res Id :" & " " & ReservationId & vbCrLf & "Date :" & CheckOut & vbCrLf, "Reservation Details")
                        Me.Close()
                    End If
                    'Dim viewReservation As New Spectrum.frmHostViewReservation
                    'viewReservation.Show()
                    End If
            Else
                    ShowMessage("please pay  remaining payment before Checkout", "Information")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdCash_Click(sender As Object, e As EventArgs) Handles cmdCash.Click
        Try
            If CInt(txtTotPaymentRemaining.Text) = 0 Then
                ShowMessage("Amount is already paid", "CM031 - " & getValueByKey("CLAE05"))
                Exit Sub
            End If
            PrepareDataSave()
            ' If IsTenderCash Then
            Dim objPaymentByCash As New frmNAcceptPaymentByCash
            objPaymentByCash.txtRemark.Text = _remarks
            objPaymentByCash._IsCashierPromoSelected = isCashierPromoSelected
            objPaymentByCash.TotalBillAmount = CDbl(txtTotalFinalCost.Text)
            objPaymentByCash.ShowDialog()
            If Not (objPaymentByCash.IsCancelAcceptPayment) Then
                If Not objPaymentByCash.ReciptTotalAmount Is Nothing And objPaymentByCash.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                    ds = objPaymentByCash.ReciptTotalAmount
                    _billAmt = objPaymentByCash.TotalBillAmount
                    _paidAmt = objPaymentByCash.TotalCustomerPadiAmount
                    _remarks = objPaymentByCash.txtRemark.Text
                    txtTotPaymentRemaining.Text = txtTotalFinalCost.Text - _paidAmt
                    RemainingAmt = txtTotPaymentRemaining.Text
                    txtTotalPaidAmount.Text = _paidAmt
                    objPaymentByCash.Close()
                    frmPayment.UpdatePaymentDataSetStru(ds, UpdateFlag)
                    ' frmPayment.PaymentGridSetting()
                    If UpdateFlag = False Then

                        Dim dt As New DataTable
                        dt = ds.Tables("Host_ReservationReceipt").Copy()
                        dt.Columns("TenderHeadCode").ColumnName = "Reciepttypecode"
                        'cmdLoyalty_Click(sender, e, dt)
                        dt.Dispose()
                    End If
                    If objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeSave Then
                        'cmdSavePrint_Click(sender, e)
                        'If frmPayment.PrepareReceiptData(ds.Tables(0), dsMainHost) = True Then
                        '    'error
                        'End If

                        SaveServices(True)
                        cmdCash.Enabled = False
                        cmdCard.Enabled = False
                        cmdCheque.Enabled = False
                        cmdPayment.Enabled = False
                        ShowMessage("Payment Done Successfully", "Information")

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
            '  Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function SaveServices(Optional ByVal SaveWithoutPayment As Boolean = False) As Boolean
        Try
            Dim tran As SqlTransaction = Nothing

            Dim scon = New SqlConnection
            Dim remainginAmt As String = "0"
            If SaveWithoutPayment = False Then
                remainginAmt = _totRemainingAmt
            End If

            If objHotelreservation.UpdateReservationForServices(clsAdmin.SiteCode, serviceCharge, ReservationId, clsAdmin.UserCode, tran, TotalAmtPaid:=_billAmt, _remainingAmountToPay:=RemainingAmt) = False Then
                tran.Rollback()
                scon.Close()
            End If

            If objHotelreservation.SaveServiceAllData(dsMainHost, SaveWithoutPayment) Then
                'print report 
                makePayment = True
                ' ShowMessage("Service Save Successfully", "Information")
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Sub PrepareDataSave(Optional ServiceSaveWithoutPayment As Boolean = False)
        Try
            getHOSTBinding()
            Dim tran As SqlTransaction = Nothing
            Dim scon = New SqlConnection
            '------------------------calucalte tax
            frmPayment.CalculateTax()
            '------------------------
            'If objHotelreservation.UpdateReservationForServices(clsAdmin.SiteCode, serviceCharge, ReservationId, clsAdmin.UserCode, tran) = False Then
            '    tran.Rollback()
            '    scon.Close()
            'End If

            If PrepareServiceDetailData(dtService, dsMainHost, ServiceSaveWithoutPayment:=ServiceSaveWithoutPayment) Then       'Service table
                Exit Sub
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Public Sub getHOSTBinding()
        Try
            dsMainHost = objHotelreservation.HOSTGetStruc()
        Catch ex As Exception

        End Try
    End Sub

    Public Function PrepareServiceDetailData(ByVal dtGuestDatail As DataTable, ByRef dsMainHost As DataSet, Optional ServiceSaveWithoutPayment As Boolean = False) As Boolean
        Try
            Dim MaxId = objHotelreservation.GetMaxId("Host_Services", "ServicesID")
            dsMainHost.Tables("Host_Services").Clear()
            Dim tempServiceDetail As DataTable = dsMainHost.Tables("Host_Services").Copy()
            'Dim tempReservation As DataTable = dsMainHost.Tables("Host_Reservation").Copy()
            For Each dr In dtGuestDatail.Rows
                Dim drService As DataRow
                drService = dsMainHost.Tables("Host_Services").NewRow
                drService("ServicesID") = MaxId
                drService("ReservationId") = ReservationId
                drService("ArticleCode") = dr("ArticleCode")
                drService("ArticleName") = dr("ServiceName")
                drService("cost") = dr("Cost")
                If ServiceSaveWithoutPayment = False Then
                    drService("IsPaid") = False
                Else
                    drService("IsPaid") = True
                End If
                drService("remarks") = ""
                drService("CreatedOn") = DateTime.Now
                drService("CreatedAt") = clsAdmin.SiteCode
                drService("CreatedBy") = clsAdmin.UserName
                drService("UpdatedOn") = DateTime.Now
                drService("UpdatedAt") = clsAdmin.SiteCode
                drService("UpdatedBy") = clsAdmin.UserName
                drService("mstStatusID") = objHotelreservation.GetStatusTypeId(objHotelreservation.enumStatusTypes.RECORD_STATUS.ToString(), objHotelreservation.enumStatus.ACTIVE.ToString())
                dsMainHost.Tables("Host_Services").Rows.Add(drService)
                MaxId = MaxId + 1
            Next
            MaxId = 0
            ' tempReservationGuestDetail = dsMainHost.Tables("Host_ReservationGuestDetail").Copy
            'SaveGuestInCLPCustomer(dsMainHost.Tables("Host_ReservationGuestDetail"))

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Sub cmdPayment_Click(sender As Object, e As EventArgs) Handles cmdPayment.Click
        Try
            If CInt(txtTotPaymentRemaining.Text) = 0 Then
                ShowMessage("Amount is already paid", "CM031 - " & getValueByKey("CLAE05"))
                Exit Sub
            End If
            PrepareDataSave()
            Dim ds As DataSet
            Dim objPayment As New frmNAcceptPayment

            'objPayment = New frmNAcceptPayment()
            objPayment.IsTenderChange = UpdateFlag
            objPayment.remarksTextbox.Text = _remarks
            objPayment.IsFastCashMemo = True
            objPayment.remarksTextbox.Text = _remarks
            objPayment.TotalBillAmount = CDbl(txtTotalFinalCost.Text)
            objPayment.ParentRelation = "CashMemo"
            objPayment._IsCashierPromoSelected = isCashierPromoSelected
            ' If dsMainHost.Tables("Host_ReservationReceipt").Rows.Count > 0 Then
            ds = objPayment.ReciptTotalAmount
            Dim dt As DataTable = dsMainHost.Tables("Host_ReservationReceipt").Copy()
            ' ds = New DataSet()
            ds.Tables.Add(dt)
            frmPayment.UpdatePaymentPrevStru(ds.Tables("Host_ReservationReceipt"))

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

                        If frmPayment.PrepareReceiptData(ds.Tables(0), dsMainHost) = True Then
                            'error
                        End If

                        SaveServices(True)


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
                ElseIf ds.Tables.Count = 0 AndAlso CDbl(txtTotalFinalCost.Text) = 0 Then
                    '  cmdHold.Enabled = False
                    If objPayment.Action = "Save" Then
                        frmPayment.cmdSavePrint_Click(sender, e)
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
            Me.Close()
            ' End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM031"), "CM031 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in updating the payment data to main", "Error")
        End Try
    End Sub

    Private Sub cmdCard_Click(sender As Object, e As EventArgs) Handles cmdCard.Click

        Try
            If CInt(txtTotPaymentRemaining.Text) = 0 Then
                ShowMessage("Amount is already paid", "CM031 - " & getValueByKey("CLAE05"))
                Exit Sub
            End If
            PrepareDataSave()
            ' If IsTenderCreditCard Then
            If UpdateFlag = False Then
                'If PromotionCleared = False Then

                'End If
                'cmdLoyalty_Click(sender, e)
            End If
            Dim objPayment As New frmNAcceptPaymentByCard()
            objPayment.TotalBillAmount = CDbl(txtTotalFinalCost.Text)
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
                    _paidAmt = objPayment.TotalBillAmount
                    txtTotPaymentRemaining.Text = txtTotalFinalCost.Text - _paidAmt
                    txtTotalPaidAmount.Text = _paidAmt
                    objPayment.Close()
                    'If Not ds Is Nothing Then
                    frmPayment.UpdatePaymentDataSetStru(ds, UpdateFlag)

                    'frmPayment.PaymentGridSetting()
                    If UpdateFlag = False Then

                        Dim dt As New DataTable
                        dt = ds.Tables("Host_ReservationReceipt").Copy()
                        dt.Columns("TenderHeadCode").ColumnName = "Reciepttypecode"
                        ' cmdLoyalty_Click(sender, e, dt)
                        dt.Dispose()
                    End If
                    If objPayment.Action = My.Resources.AcceptPaymentActionTypeSave Then
                        If frmPayment.PrepareReceiptData(ds.Tables(0), dsMainHost) = True Then
                            'error
                        End If

                        '  SaveServices(True)
                        If SaveServices(True) = True Then
                            cmdCash.Enabled = False
                            cmdCard.Enabled = False
                            cmdCheque.Enabled = False
                            cmdPayment.Enabled = False
                            ShowMessage("Payment Done Successfully", "Information")
                        End If
                        'If cmdSavePrint_Click(sender, e) Then
                        '    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                        'End If

                    ElseIf objPayment.Action = My.Resources.AcceptPaymentActionTypeGift Then

                        GiftMsg = objPayment.GiftReceiptMessage


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
            '    Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdCheque_Click(sender As Object, e As EventArgs) Handles cmdCheque.Click
        Try
            If CInt(txtTotPaymentRemaining.Text) = 0 Then
                ShowMessage("Amount is already paid", "CM031 - " & getValueByKey("CLAE05"))
                Exit Sub
            End If
            PrepareDataSave()
            Dim objCheck As New frmNCheckPayment
            objCheck.BillAmount = CDbl(txtTotalFinalCost.Text)
            objCheck.ShowDialog()
            If objCheck.IsCancelAcceptPayment = False Then
                If Not objCheck.ReciptTotalAmount Is Nothing And objCheck.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                    Dim ds As DataSet = objCheck.ReciptTotalAmount
                    'Dim ds As New DataSet(
                    'ds.Tables.Add(dt)
                    _paidAmt = objCheck.CollectAmount
                    txtTotPaymentRemaining.Text = txtTotalFinalCost.Text - _paidAmt
                    txtTotalPaidAmount.Text = _paidAmt
                    objCheck.Close()
                    'If Not ds Is Nothing Then
                    frmPayment.UpdatePaymentDataSetStru(ds, UpdateFlag)

                    ' frmPayment.PaymentGridSetting()
                    If UpdateFlag = False Then

                        Dim dt As New DataTable
                        dt = ds.Tables("Host_ReservationReceipt").Copy()
                        dt.Columns("TenderHeadCode").ColumnName = "Reciepttypecode"
                        ' cmdLoyalty_Click(sender, e, dt)
                        dt.Dispose()
                    End If
                    If objCheck.Action = My.Resources.AcceptPaymentActionTypeSave Then

                        If frmPayment.PrepareReceiptData(ds.Tables(0), dsMainHost) = True Then
                            'error
                            'ElseIf dsMainHost.Tables.Contains("CheckDtls") Then
                            '    PrepareCheckPaymentDetails(ds.Tables(1), dsMainHost)
                        End If

                        SaveServices(True)
                        cmdCash.Enabled = False
                        cmdCard.Enabled = False
                        cmdCheque.Enabled = False
                        cmdPayment.Enabled = False
                        'If cmdSavePrint_Click(sender, e) Then
                        '    ' AutoLogout(FrmTranCode, Me, lblLoggedIn)
                        'End If

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
            '  Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class