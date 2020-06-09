Imports SpectrumBL
Imports C1.Win.C1FlexGrid

Public Class frmHostCheckoutPayment
#Region "Global Variables for Class"
    Dim dtAccomodation As New DataTable
    Dim dtService As New DataTable
    Dim dtGuest As New DataTable
    Dim dtFood As New DataTable
    Dim Price As String = ""
    Dim serviceType As New DataTable
    Dim objHotelreservation As New clsHotelReservation
    Protected controlList As New ArrayList
    Dim index As Integer = 1
    Dim RowAccomodation As DataRow
    Dim rowService As DataRow
    Dim objCM As New clsCashMemo
    Dim objItemSch As New clsIteamSearch
    Dim dsMain As New DataSet
    Dim dtAddToGrid As New DataTable
    Dim dvCurrentQty As DataView
    Private IsChangeQuantityOrPrice As Boolean = False
    Dim dtPaymentGuest As New DataTable
    Dim dtBind As New DataTable
    Dim FinalCost As Double
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


#End Region


    ''' <summary>
    ''' Events
    Private Sub frmHostCheckoutPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmdCheckout.Enabled = False
        cmdPrintBill.Enabled = False

        cmdPayment.Enabled = False

        If tpServicesDetails.IsSelected = True Then
            cmdPayment.Enabled = True
        End If


        LoadReservationGuestDetails()
        gridGuestPaymentDetailsSetting()
        loadPrimaryGuestDetails()
        ' serviceType = objHotelreservation.GetServiceDetails()
        ' dtService = objHotelreservation.GetServicesSchema
        'dtService.Clear()
        Dim condition As String
        AddHandler txtAndroidArticleSearchTextBox.KeyDown, AddressOf txtSearch_KeyDown
        ' Dim dtBind = objCM.GetItemDetailsForBulkOrder(clsAdmin.SiteCode, "")
        condition = " AND A.ArticleCode <>'" & clsDefaultConfiguration.GVBaseArticle & "' AND A.ArticleCode <>'" & clsDefaultConfiguration.ClpBaseArticle & "' AND A.ArticleCode <>'" & clsAdmin.CVBaseArticle & "'"
        dtBind = objItemSch.GetEANData(clsAdmin.SiteCode, "", clsAdmin.LangCode, condition, dtItemScanData)
        If dtBind.Rows.Count > 1 Then
            Call SetWildSearchTextBox(dtBind, txtAndroidArticleSearchTextBox, key:="ArticleCode", Value:="Discription", searchData:="ArticleCodeDesc")
        End If

        bindAccomodationDetails(ReservationId)
        gridAccomodationPaymentDetailsSetting()
        dtService = objHotelreservation.GetServiceDetails
        dtService.Clear()
        LoadServiceDetails(ReservationId)
        PrimaryPaymentDetail()
        gridServicePaymentDetailsSetting()
        BindFoodPaymentDetails(ReservationId)
        gridFoodPaymentDetailsSetting()
        DefaultTheme()
        ' readOnlyData()
    End Sub

    Private Sub readOnlyData()
        'txtTotalTax.ReadOnly = True
        'txtTotalDiscountAmt.ReadOnly = True
        'txtTotalFinalCost.ReadOnly = True
        'txtTotalPaidAmount.ReadOnly = True
        'txtTotalRemainingAmt.ReadOnly = True
        'txtTotalTax.Enabled = False
        'txtTotalDiscountAmt.Enabled = False
        'txtTotalFinalCost.Enabled = False
        'txtTotalPaidAmount.Enabled = False
        'txtTotalRemainingAmt.Enabled = False
    End Sub
    Public Sub PrimaryPaymentDetail()
        Try
            FinalCost = txtTotalFinalCost.Text
            Dim servicesAmount As Decimal = "0"
            Dim TotCost As Integer = 0
            If dtService.Rows.Count > 0 Then
                For i As Integer = 0 To dtService.Rows.Count - 1
                    If dtService.Rows(i)("IsPaid") = False Then
                        Dim _TotCost As DataTable = dtService.Select("IsPaid=False").CopyToDataTable()
                        TotCost = CInt(_TotCost.Compute("SUM(Cost)", ""))
                        ' txtTotalRemainingAmt.Text = TotCost
                        ' txtTotalFinalCost.Text = txtTotalPaidAmount.Text + TotCost
                        servicesAmount = TotCost
                        ' Exit Sub
                    End If
                Next


                Dim txtCost As Integer
                txtCost = CDbl(txtTotalPaidAmount.Text)
                If txtCost <> 0 Then
                    txtTotalFinalCost.Text = txtTotalPaidAmount.Text + TotCost ''txtTotalPaidAmount.Text + TotCost
                    txtTotalRemainingAmt.Text = TotCost
                Else
                    txtTotalFinalCost.Text = _totRemainingAmt + TotCost
                    txtTotalRemainingAmt.Text = txtTotalFinalCost.Text
                End If
                'If txtTotalPaidAmount.Text = txtTotalFinalCost.Text Then
                '    txtTotalRemainingAmt.Text = "0.00"
                'Else
                '    'txtTotalRemainingAmt.Text = txtTotalFinalCost.Text
                '    txtTotalRemainingAmt.Text = servicesAmount
                'End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub DefaultTheme()

        Me.cmdPrintBill.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdPrintBill.FlatStyle = FlatStyle.Flat
        Me.cmdPrintBill.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdPrintBill.FlatAppearance.BorderSize = 0
        'Me.cmdPrintBill.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdPrintBill.Font = New Font("Callibri", 7)
        Me.cmdPrintBill.BackColor = Color.FromArgb(208, 208, 208)
        Me.cmdPrintBill.ForeColor = Color.White
        Me.cmdPrintBill.TextAlign = ContentAlignment.MiddleCenter

        Me.cmdCheckout.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdCheckout.FlatStyle = FlatStyle.Flat
        Me.cmdCheckout.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdCheckout.FlatAppearance.BorderSize = 0
        'Me.cmdCheckout.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdCheckout.BackColor = Color.FromArgb(208, 208, 208)
        Me.cmdCheckout.Font = New Font("Callibri", 7)
        Me.cmdCheckout.ForeColor = Color.White
        Me.cmdCheckout.TextAlign = ContentAlignment.MiddleCenter

        Me.cmdPayment.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdPayment.FlatStyle = FlatStyle.Flat
        Me.cmdPayment.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.cmdPayment.FlatAppearance.BorderSize = 0
        'Me.cmdPayment.BackColor = Color.FromArgb(0, 113, 188)
        Me.cmdPayment.BackColor = Color.FromArgb(208, 208, 208)
        Me.cmdPayment.Font = New Font("Callibri", 7)
        Me.cmdPayment.ForeColor = Color.White
        Me.cmdPayment.TextAlign = ContentAlignment.MiddleCenter

        Me.btnServieAdd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnServieAdd.FlatStyle = FlatStyle.Flat
        Me.btnServieAdd.FlatAppearance.BorderColor = Color.FromArgb(0, 113, 188)
        Me.btnServieAdd.FlatAppearance.BorderSize = 0
        Me.btnServieAdd.BackColor = Color.FromArgb(0, 113, 188)
        'Me.btnServieAdd.BackColor = Color.FromArgb(153, 198, 228)
        Me.btnServieAdd.Font = New Font("Callibri", 7, FontStyle.Bold)
        Me.btnServieAdd.ForeColor = Color.White
        Me.btnServieAdd.TextAlign = ContentAlignment.MiddleCenter

    End Sub

#Region "Checkout Guest Details"
    Public Function LoadReservationGuestDetails()
        bindCheckoutGuestDetails(ReservationId, MobileNo)
    End Function



    Public Sub bindCheckoutGuestDetails(ByVal ReservationId As String, Optional ByVal MobileNo As String = "")
        Try
            dtGuest = objHotelreservation.GetCheckoutPaymentGuestDetails(ReservationId, MobileNo) 'clsAdmin.SiteCode,

            If dtGuest IsNot Nothing Then
                grdGuestDetails.DataSource = dtGuest.DefaultView
                dtPaymentGuest = dtGuest
            Else
                ShowMessage("Guest not Available", "Information")
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub loadPrimaryGuestDetails()
        Try
            If dtPaymentGuest.Rows.Count > 0 Then
                For i = 0 To dtPaymentGuest.Rows.Count - 1
                    If dtPaymentGuest.Rows(i)("isPrimaryGuest") = True Then
                        lblCustNameVal.Text = dtPaymentGuest.Rows(i)("Name")
                        lblEmailVal.Text = dtPaymentGuest.Rows(i)("guestEmailID")
                        lblPhonenoVal.Text = dtPaymentGuest.Rows(i)("MobileNo")
                        lblCheckInVal.Text = CheckIn
                        lblCheckOutVal.Text = CheckOut
                        lblAdultVal.Text = Adult
                        lblChildrenVal.Text = Children
                        Dim _totalTax As Decimal = Convert.ToDecimal(totalTax)
                        Dim _totDiscountAmt As Decimal = Convert.ToDecimal(totDiscountAmt)
                        Dim _totalPaidAmt As Decimal = Convert.ToDecimal(totalPaidAmt)
                        Dim _totalFinalCost As Decimal = IIf(Convert.ToDecimal(totalPaidAmt) = 0, Convert.ToDecimal(totRemainingAmt), Convert.ToDecimal(totalPaidAmt)) ''IIf(Convert.ToDecimal(totalFinalCost) = 0, 0, Convert.ToDecimal(totRemainingAmt))
                        Dim _totRemainingAmt As Decimal = Convert.ToDecimal(totRemainingAmt)
                       
                        txtTotalTax.Text = Math.Round(_totalTax, 2)
                        txtTotalDiscountAmt.Text = Math.Round(_totDiscountAmt, 2)
                        txtTotalPaidAmount.Text = Math.Round(_totalPaidAmt, 2)
                        txtTotalFinalCost.Text = Math.Round(_totalFinalCost, 2)

                        txtTotalRemainingAmt.Text = Math.Round(_totRemainingAmt, 2)

                    End If
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub



    Private Sub gridGuestPaymentDetailsSetting()
        Try
            grdGuestDetails.DataSource = dtPaymentGuest
            grdGuestDetails.Cols("Name").Width = 200
            grdGuestDetails.Cols("Name").DataType = Type.GetType("System.String")
            grdGuestDetails.Cols("Name").AllowEditing = False
            grdGuestDetails.Cols("Name").Name = "Name"
            grdGuestDetails.Cols("Name").TextAlign = TextAlignEnum.LeftCenter


            grdGuestDetails.Cols("MobileNo").Width = 125
            grdGuestDetails.Cols("MobileNo").DataType = Type.GetType("System.String")
            grdGuestDetails.Cols("MobileNo").AllowEditing = False
            grdGuestDetails.Cols("MobileNo").Name = "MobileNo"
            grdGuestDetails.Cols("MobileNo").TextAlign = TextAlignEnum.LeftCenter


            grdGuestDetails.Cols("Age").Width = 75
            grdGuestDetails.Cols("Age").DataType = Type.GetType("System.String")
            grdGuestDetails.Cols("Age").AllowEditing = False
            grdGuestDetails.Cols("Age").Name = "Age"
            grdGuestDetails.Cols("Age").TextAlign = TextAlignEnum.LeftCenter

            grdGuestDetails.Cols("Gender").Width = 100
            grdGuestDetails.Cols("Gender").AllowEditing = False
            grdGuestDetails.Cols("Gender").DataType = Type.GetType("System.String")
            grdGuestDetails.Cols("Gender").Name = "Gender"
            grdGuestDetails.Cols("Gender").TextAlign = TextAlignEnum.LeftCenter

            grdGuestDetails.Cols("DocumentType").Width = 150
            grdGuestDetails.Cols("DocumentType").AllowEditing = False
            grdGuestDetails.Cols("DocumentType").DataType = Type.GetType("System.String")
            grdGuestDetails.Cols("DocumentType").Name = "DocumentType"
            grdGuestDetails.Cols("DocumentType").TextAlign = TextAlignEnum.LeftCenter

            grdGuestDetails.Cols("Description").Width = 150
            grdGuestDetails.Cols("Description").DataType = Type.GetType("System.String")
            grdGuestDetails.Cols("Description").AllowEditing = False
            grdGuestDetails.Cols("Description").Name = "Description"
            grdGuestDetails.Cols("Description").TextAlign = TextAlignEnum.LeftCenter

            grdGuestDetails.Cols("DocumentNo").Width = 50
            grdGuestDetails.Cols("DocumentNo").AllowEditing = False
            grdGuestDetails.Cols("DocumentNo").DataType = Type.GetType("System.Decimal")
            grdGuestDetails.Cols("DocumentNo").Name = "DocumentNo"
            grdGuestDetails.Cols("DocumentNo").TextAlign = TextAlignEnum.LeftCenter

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

#End Region

#Region "Accomodation Details"

    Private Sub GettingDetailsofAccomodation()
        'Try
        '    If dtAccomodation.Rows.Count = 0 Then
        '        controlList.Clear()
        '    End If
        '    dtAccomodation = objHotelreservation.GetAccomodationSchema
        '    Dim dsMemb = objHotelreservation.GetAccomodationSchema
        '    If Not (dtAccomodation Is Nothing) AndAlso dtAccomodation.Rows.Count > 0 Then
        '        dtAccomodation.Rows.Clear()
        '    End If
        '    ' Dim index As Integer = 1

        '    If Not dsMemb Is Nothing AndAlso dsMemb.Rows.Count > 0 Then
        '        Dim index As Integer = 1
        '        For Each dr As DataRow In dsMemb.Rows

        '            ' dsMemb = objclsMemb.GetMembershipTableStructure(clsAdmin.SiteCode, CustomerNo, SearchRefBill)
        '            Dim rowDtls As DataRow = dtAccomodation.NewRow
        '            rowDtls("SrNo") = index
        '            rowDtls("Particular") = dr("Particular")
        '            rowDtls("Price") = dr("Price").ToString
        '            index = index + 1
        '            rowDtls("Sel") = False
        '            dtAccomodation.Rows.Add(rowDtls)

        '        Next

        '    End If

        '    '   BindAccomodationGridData(dtAccomodation)
        '    gridAccomodationPaymentDetailsSetting()
        'Catch ex As Exception
        '    LogException(ex)
        '    ShowMessage(ex.Message, getValueByKey("CLAE05"))
        'End Try
    End Sub

    Public Sub bindAccomodationDetails(ByVal ReservationId As String, Optional ByVal MobileNo As String = "")
        Try
            dtAccomodation = objHotelreservation.GetAccomodationDetails(ReservationId) 'clsAdmin.SiteCode,
            If dtAccomodation IsNot Nothing Then
                grdAccomodation.DataSource = dtAccomodation.DefaultView
                dtPaymentAccomodation = dtAccomodation
            Else
                ShowMessage(getValueByKey(" Accomodation not Available"), "Information - " & "Information")
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Dim dtPaymentAccomodation As New DataTable
    Public Sub gridAccomodationPaymentDetailsSetting()
        Try
            grdAccomodation.DataSource = dtPaymentAccomodation
            'grdAccomodation.Cols("Del").Caption = ""
            'grdAccomodation.Cols("Del").Width = 20
            'grdAccomodation.Cols("Del").ComboList = "..."
            'grdAccomodation.Cols("SrNo").Visible = True

            grdAccomodation.Cols("Particular").Width = 100
            grdAccomodation.Cols("Particular").DataType = Type.GetType("System.String")
            grdAccomodation.Cols("Particular").AllowEditing = True
            grdAccomodation.Cols("Particular").Visible = False
            grdAccomodation.Cols("Particular").Name = "Particular"
            grdAccomodation.Cols("Particular").TextAlign = TextAlignEnum.LeftCenter

            grdAccomodation.Cols("Price").Width = 100
            grdAccomodation.Cols("Price").DataType = Type.GetType("System.String")
            grdAccomodation.Cols("Price").AllowEditing = False
            grdAccomodation.Cols("Price").Name = "Price"
            grdAccomodation.Cols("Price").TextAlign = TextAlignEnum.LeftCenter

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function ValidatedAccomodationDetails() As Boolean
        Try
            'If txtAccParticular.Text.Trim() = "" Then
            '    txtAccParticular.Focus()
            '    Exit Function
            'ElseIf txtAccoPrice.Text.Trim() = "" Then
            '    txtAccoPrice.Focus()
            '    Exit Function

            'End If
            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)

            Return False
        End Try
    End Function

    Private Sub grdAccomodation_CellButtonClick(sender As Object, e As RowColEventArgs)
        'Try


        '    If grdAccomodation.Rows.Count > 1 Then
        '        grdAccomodation.Select(grdAccomodation.Rows.Count - 1, 2)
        '        grdAccomodation.Rows.Remove(grdAccomodation.Row)
        '    End If
        '    ' BindAccomodationGridData(dtAccomodation)
        '    gridAccomodationPaymentDetailsSetting()
        '    Dim AccomodationAmt As Integer = 0
        '    For index = 1 To grdAccomodation.Rows.Count - 1
        '        If grdAccomodation.Rows.Count > 0 Then
        '            AccomodationAmt = AccomodationAmt + grdAccomodation.Rows(index)("Price")
        '        End If
        '    Next
        '    'txtAccomodationFinalCost.Text = AccomodationAmt
        'Catch ex As Exception
        '    ShowMessage(ex.Message, getValueByKey("CLAE05"))
        '    LogException(ex)
        'End Try
    End Sub

    Private Sub btnAddAccomodation_Click(sender As Object, e As EventArgs)
        'Try
        '    Dim index As Integer = 1
        '    If ValidatedAccomodationDetails() Then
        '        RowAccomodation = dtAccomodation.NewRow()
        '        If grdAccomodation.Rows.Count > 1 Then
        '            RowAccomodation("SrNo") = dtAccomodation.Rows.Count + 1
        '        Else
        '            RowAccomodation("SrNo") = index
        '        End If
        '        'RowAccomodation("Particular") = txtAccParticular.Text
        '        'RowAccomodation("Price") = txtAccoPrice.Text
        '        dtAccomodation.Rows.Add(RowAccomodation)
        '    End If
        '    gridAccomodationPaymentDetailsSetting()
        '    Dim AccomodationAmt As Integer = 0
        '    For index = 1 To grdAccomodation.Rows.Count - 1
        '        If grdAccomodation.Rows.Count > 0 Then

        '            AccomodationAmt = AccomodationAmt + grdAccomodation.Rows(index)("Price")
        '        End If
        '    Next
        '    'txtAccomodationFinalCost.Text = AccomodationAmt
        '    'txtAccParticular.Clear()
        '    'txtAccoPrice.Clear()
        'Catch ex As Exception
        '    LogException(ex)
        'End Try

    End Sub



#End Region

#Region "Service Details"

    Enum dgArticle
        ' Selects = 0
        Delete
        SrNo
        ArticleCode
        ArticleDescription
        Qty
        Cost

    End Enum

    Private Sub btnServieAdd_Click_1(sender As Object, e As EventArgs) Handles btnServieAdd.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim strArticle As String = ""
            Dim Ean As String = ""
            Dim flag As Integer = 0


            'If (txtAddArticle.Text.Trim() <> String.Empty) Then
            If (txtAndroidArticleSearchTextBox.Text.Trim() <> String.Empty) Then
                txtAndroidArticleSearchTextBox.Text = txtAndroidArticleSearchTextBox.Text.ToString().Split(" ")(0)
                ' dtAddToGrid = objCM.GetItemDetailsForBulkOrder(clsAdmin.SiteCode, txtAndroidArticleSearchTextBox.Text.Trim(), clsAdmin.LangCode)
                Dim openMrp As Boolean = False
                Dim dtAddToGrid As New DataTable

                dtAddToGrid = objCM.GetItemDetails(clsAdmin.SiteCode, txtAndroidArticleSearchTextBox.Text.Trim, openMrp, clsAdmin.LangCode, False)

                ' dtAddToGrid = objClsCommon.GetArticleDetailsById(txtAndroidArticleSearchTextBox.Text.Trim())
                'If dtAddToGrid.Rows.Count = 0 Then
                '    ShowMessage("Service does not exist", "BOC001 - " & getValueByKey("CLAE04"))
                '    Exit Sub
                'End If

               
                If Not dtAddToGrid Is Nothing AndAlso dtAddToGrid.Rows.Count > 0 Then
                    If grdService.Rows.Count > 1 Then
                        For index = 1 To grdService.Rows.Count - 1
                            If dtAddToGrid.Rows(0)("DISCRIPTION").ToString() = grdService.Rows(index)(dgArticle.ArticleDescription) Then
                                ShowMessage("Article alreay exist", "CLIST06 - " & getValueByKey("CLIST06"))
                                txtAndroidArticleSearchTextBox.Text = ""
                                Exit Sub
                            End If
                        Next
                    End If
                    ' If Not dtAddToGrid Is Nothing AndAlso dtAddToGrid.Rows.Count > 0 Then
                    AddArticleRemarkToGrid(dtAddToGrid, False)
                    ' gridServicePaymentDetailsSetting()
                End If
            End If

            If grdService.Rows.Count = 1 Then
                'MsgBox("Please add atleast one item ")
                ShowMessage(" please select Service first ", "Information")
                Exit Sub
            End If
            txtAndroidArticleSearchTextBox.Text = ""

        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub AddArticleRemarkToGrid(ByVal dt As DataTable, ByVal isEdit As Boolean, Optional qty As String = "")
        Try


            Dim rowService As DataRow
            rowService = dtService.NewRow()
            rowService("SrNo") = dtService.Rows.Count + 1
            rowService("ArticleCode") = dt.Rows(0)("ARTICLECODE").ToString
            rowService("ServiceName") = dt.Rows(0)("DISCRIPTION").ToString()
            rowService("Quantity") = 1
            ' rowService("MobileNumber") = MobileNo
            rowService("Cost") = dt.Rows(0)("SELLINGPRICE")
            rowService("IsPaid") = False
            'rowService("remark") = ""
            ' rowService("mstStatusID") = 1


            dtService.Rows.Add(rowService)
            PrimaryPaymentDetail()
            gridServicePaymentDetailsSetting()
            'Dim TotCost As Integer
            'If dtService.Rows.Count > 0 Then
            '    For i As Integer = 0 To dtService.Rows.Count - 1
            '        If dtService.Rows(i)("IsPaid") = False Then
            '            Dim _TotCost As DataTable = dtService.Select("IsPaid=False").CopyToDataTable
            '            TotCost = CInt(_TotCost.Compute("SUM(Cost)", ""))
            '        End If

            '    Next
            'End If

            'Dim txtCost As Integer
            'txtCost = CDbl(txtTotalFinalCost.Text)
            'txtTotalFinalCost.Text = txtTotalFinalCost.Text + TotCost
            'txtTotalRemainingAmt.Text = txtTotalFinalCost.Text



        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub grdService_CellButtonClick_1(sender As Object, e As RowColEventArgs) Handles grdService.CellButtonClick
        Try
            Dim ServiceAmt As Double = CDbl(txtTotalFinalCost.Text)
            Dim cost As Double = 0
            If grdService.Rows.Count > 0 Then
                For index = 0 To grdService.Rows.Count - 1
                    If index > 0 Then
                        If index = e.Row Then
                            cost = CDbl(grdService.Rows(index)("Cost"))
                            If grdService.Rows.Count > 0 Then
                                ServiceAmt = ServiceAmt - cost
                            End If
                        End If

                    End If

                Next
            End If
            txtTotalFinalCost.Text = ServiceAmt
            txtTotalRemainingAmt.Text = ServiceAmt
            If grdService.Rows.Count > 1 Then
                grdService.Select(grdService.Rows.Count - 1, 2)
                grdService.Rows.Remove(grdService.Row)
            End If

            If dtService.Compute("Sum(Cost)", "") Is Nothing Or Not IsDBNull(dtService.Compute("Sum(Cost)", "")) Then
                cost = dtService.Compute("Sum(Cost)", "")
            Else
                cost = ServiceAmt
            End If
            txtTotalFinalCost.Text = ServiceAmt
            txtTotalRemainingAmt.Text = cost
            'If dtService.Rows.Count > 1 Then
            '    cost = ServiceAmt
            'End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim strArticle As String = ""
            Dim Ean As String = ""
            Dim Weight As String = ""
            Dim WeghingScaleBarcode = False
            Dim flag As Integer = 0

            
            If (txtAndroidArticleSearchTextBox.Text.Trim() <> String.Empty) Then
                txtAndroidArticleSearchTextBox.Text = txtAndroidArticleSearchTextBox.Text.ToString().Split(" ")(0)
                If (e.KeyCode = Keys.Enter AndAlso sender.Text <> String.Empty) Then
                    Dim openMrp As Boolean = False
                    Dim dt As New DataTable
                    ' dt = objCM.GetItemDetails(clsAdmin.SiteCode, txtAndroidArticleSearchTextBox.Text.Trim, openMrp, clsAdmin.LangCode, False)
                    dt = objCM.GetItemDetails(clsAdmin.SiteCode, txtAndroidArticleSearchTextBox.Text.Trim, openMrp, clsAdmin.LangCode, If(WeghingScaleBarcode, False, clsDefaultConfiguration.IsBatchManagementReq))

                    If dt.Rows.Count = 0 Then
                        ShowMessage("Service does not exist", "BOC001 - " & getValueByKey("CLAE04"))
                        Exit Sub
                        txtAndroidArticleSearchTextBox.Text = ""
                    End If
                    If grdService.Rows.Count > 1 Then
                        For index = 1 To grdService.Rows.Count - 1
                            If dt.Rows(0)("DISCRIPTION").ToString() = grdService.Rows(index)(dgArticle.ArticleDescription) Then
                                ShowMessage("Article alreay exist", "CLIST06 - " & getValueByKey("CLIST06"))
                                txtAndroidArticleSearchTextBox.Text = ""
                                Exit Sub
                            End If
                        Next
                    End If
                    Dim ItemDesc As String = String.Empty
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        AddArticleRemarkToGrid(dt, False)
                    End If

                    txtAndroidArticleSearchTextBox.Text = ""
                End If
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - exc " & getValueByKey("CLAE05"))
            LogException(ex)

        End Try

    End Sub

    Public Sub gridServicePaymentDetailsSetting()
        Try
            grdService.DataSource = dtService
            grdService.Cols("SrNo").Visible = True

            grdService.Cols("ArticleCode").Visible = False

            grdService.Cols("ServiceName").Width = 300
            grdService.Cols("ServiceName").DataType = Type.GetType("System.String")
            grdService.Cols("ServiceName").AllowEditing = False
            grdService.Cols("ServiceName").Name = "ServiceName"
            grdService.Cols("ServiceName").TextAlign = TextAlignEnum.LeftCenter

            grdService.Cols("Quantity").Width = 100
            grdService.Cols("Quantity").DataType = Type.GetType("System.String")
            grdService.Cols("Quantity").AllowEditing = True
            grdService.Cols("Quantity").Name = "Quantity"
            grdService.Cols("Quantity").TextAlign = TextAlignEnum.LeftCenter

            grdService.Cols("Cost").Width = 100
            grdService.Cols("Cost").DataType = Type.GetType("System.string")
            grdService.Cols("Cost").AllowEditing = False
            grdService.Cols("Cost").Name = "Cost"
            grdService.Cols("Cost").TextAlign = TextAlignEnum.LeftCenter

            grdService.Cols("IsPaid").Visible = False


        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Dim DtLoadService As New DataTable
    Public Function LoadServiceDetails(ByVal reservationId As String)
        Try
            DtLoadService = objHotelreservation.GetAllServices(reservationId, clsAdmin.SiteCode)
            If DtLoadService IsNot Nothing Then
                grdService.DataSource = DtLoadService.DefaultView
                dtService = DtLoadService
                grdService.Cols("IsPaid").Visible = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function


    Private Sub grdService_AfterEdit(sender As Object, e As RowColEventArgs) Handles grdService.AfterEdit
        Try
            Dim x As New DataSet
            x = dsMain
            If grdService.Row = -1 Then Exit Sub
            Dim CurrentCell As Integer = e.Col
            Dim CurrentRow As Integer = grdService.Row '-- e.Row
            Dim addCondtionRow As String = String.Empty
            Dim strConsumption As String = "0"
            '    Dim strConsumption As String = dtAddToGrid.Rows(0)("CostPrice") ''grdService.Rows(grdService.Row)(dgArticle.Cost)
            If dtService.Rows.Count > 0 Then
                For i = 1 To grdService.Rows.Count - 1
                    Dim EditedSrNo = Val(grdService.Rows(grdService.Row)(dgArticle.SrNo).ToString())

                    Dim EditQnty = grdService.Rows(i)("Quantity") ''Val(grdService.Rows(grdService.Row)(dgArticle.Qty).ToString())
                    Dim EditArticleKey = grdService.Rows(i)("ArticleCode")
                    Dim dtFindBind = dtBind.Select("key='" + EditArticleKey + "'")
                    Dim ArticlePrice = dtFindBind(0)("SellingPrice").ToString()
                    Dim finalAmout = EditQnty * ArticlePrice
                    Dim drServiceRow = dtService.Select("ArticleCode='" + EditArticleKey + "'")
                    drServiceRow(0)("Cost") = finalAmout
                    strConsumption = finalAmout
                    ''  grdService.Rows(i)("Cost") = strConsumption * result
                    'grdService.Rows(grdService.Row)(dgArticle.Cost) = strConsumption * result
                    ' '      dtAddToGrid.AcceptChanges()
                Next
                gridServicePaymentDetailsSetting()
            End If
            'If dtAddToGrid.Rows.Count > 0 Then
            '    For i = 1 To grdService.Rows.Count - 1
            '        Dim EditedSrNo = Val(grdService.Rows(grdService.Row)(dgArticle.SrNo).ToString())

            '        Dim result = grdService.Rows(i)("Quantity") ''Val(grdService.Rows(grdService.Row)(dgArticle.Qty).ToString()) 
            '        grdService.Rows(i)("Cost") = strConsumption * result
            '        'grdService.Rows(grdService.Row)(dgArticle.Cost) = strConsumption * result
            '        dtAddToGrid.AcceptChanges()
            '    Next

            '    'AddArticleRemarkToGrid(dtAddToGrid, True, result)
            '    ' End If
            'End If
            Dim TotCost As Integer
            'For i As Integer = 0 To grdService.Rows.Count - 1
            '    TotCost = CInt(dtAddToGrid.Compute("SUM(CostPrice)", ""))
            'Next
            ' Dim txtCost As Integer
            'txtCost = CDbl(txtTotalFinalCost.Text)
            'txtTotalFinalCost.Text = txtCost + strConsumption ''grdService.Rows(grdService.Row)(dgArticle.Cost)
            txtTotalFinalCost.Text = FinalCost + strConsumption
            txtTotalRemainingAmt.Text = txtTotalFinalCost.Text ''grdService.Rows(grdService.Row)(dgArticle.Cost)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub





    ''Event End
#End Region

#Region "Food Payment Details"
    Dim dtFoodDetails As New DataTable
    Public Sub BindFoodPaymentDetails(ByVal ReservationId As String)
        Try

            dtFood = objHotelreservation.GetFoodPaymentCharges(ReservationId)
            If dtFood IsNot Nothing Then
                grdFoodCharges.DataSource = dtFood.DefaultView
                dtFoodDetails = dtFood

            Else
                ShowMessage(getValueByKey(" Food charges not Available"), "Information - " & "Information")

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub gridFoodPaymentDetailsSetting()
        Try
            grdFoodCharges.DataSource = dtFood
            grdFoodCharges.Cols("BillNo").Width = 170
            grdFoodCharges.Cols("BillNo").DataType = Type.GetType("System.String")
            grdFoodCharges.Cols("BillNo").AllowEditing = False
            grdFoodCharges.Cols("BillNo").Name = "BillNo"
            grdFoodCharges.Cols("BillNo").TextAlign = TextAlignEnum.LeftCenter


            grdFoodCharges.Cols("TillNo").Width = 100
            grdFoodCharges.Cols("TillNo").DataType = Type.GetType("System.String")
            grdFoodCharges.Cols("TillNo").AllowEditing = False
            grdFoodCharges.Cols("TillNo").Name = "TillNo"
            grdFoodCharges.Cols("TillNo").TextAlign = TextAlignEnum.LeftCenter


            grdFoodCharges.Cols("BillDate").Width = 100
            grdFoodCharges.Cols("BillDate").DataType = Type.GetType("System.String")
            grdFoodCharges.Cols("BillDate").AllowEditing = False
            grdFoodCharges.Cols("BillDate").Name = "BillDate"
            grdFoodCharges.Cols("BillDate").TextAlign = TextAlignEnum.LeftCenter

            grdFoodCharges.Cols("Item").Width = 300
            grdFoodCharges.Cols("Item").AllowEditing = False
            grdFoodCharges.Cols("Item").DataType = Type.GetType("System.String")

            grdFoodCharges.Cols("Item").Name = "Item"
            grdFoodCharges.Cols("Item").TextAlign = TextAlignEnum.LeftCenter

            grdFoodCharges.Cols("Quantity").Width = 100
            grdFoodCharges.Cols("Quantity").AllowEditing = False
            grdFoodCharges.Cols("Quantity").DataType = Type.GetType("System.String")

            grdFoodCharges.Cols("Quantity").Name = "Quantity"
            grdFoodCharges.Cols("Quantity").TextAlign = TextAlignEnum.LeftCenter

            grdFoodCharges.Cols("Amount").Width = 150
            grdFoodCharges.Cols("Amount").DataType = Type.GetType("System.String")
            grdFoodCharges.Cols("Amount").AllowEditing = False
            grdFoodCharges.Cols("Amount").Name = "Amount"
            grdFoodCharges.Cols("Amount").TextAlign = TextAlignEnum.LeftCenter

            grdFoodCharges.Cols("Status").Width = 100
            grdFoodCharges.Cols("Status").AllowEditing = False
            grdFoodCharges.Cols("Status").DataType = Type.GetType("System.Decimal")

            grdFoodCharges.Cols("Status").Name = "Status"
            grdFoodCharges.Cols("Status").TextAlign = TextAlignEnum.LeftCenter

            grdFoodCharges.Cols("Tender").Width = 100
            grdFoodCharges.Cols("Tender").DataType = Type.GetType("System.String")
            grdFoodCharges.Cols("Tender").AllowEditing = False
            grdFoodCharges.Cols("Tender").Name = "Tender"
            grdFoodCharges.Cols("Tender").TextAlign = TextAlignEnum.LeftCenter

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

#End Region


    Private Sub cmdPayment_Click(sender As Object, e As EventArgs) Handles cmdPayment.Click
        Try
            Dim Payment As New frmHostMakePayment
            Payment.ReservationId = ReservationId
            Payment.totDiscountAmt = txtTotalDiscountAmt.Text
            Payment.totalTax = txtTotalTax.Text
            Payment.totalFinalCost = txtTotalFinalCost.Text
            Payment.totalPaidAmt = txtTotalPaidAmount.Text
            Payment.totRemainingAmt = txtTotalRemainingAmt.Text
            Payment.CheckIn = lblCheckInVal.Text
            Payment.CheckOut = lblCheckOutVal.Text
            Payment.guestName = lblCustNameVal.Text
            Payment.email = lblEmailVal.Text
            Payment.PhoneNumber = lblPhonenoVal.Text
            Payment.Adult = lblAdultVal.Text
            Payment.Children = lblChildrenVal.Text
            Payment.dtService = dtService
            If dtService.Rows.Count > 0 Then
                ' For i = 0 To grdService.Rows.Count - 1
                'Dim DtFilter = dtService.DefaultView.ToTable(True, "isPaid", "ArticleCode")
                'Dim d As DataTable = dtService.Select("IsPaid=True").CopyToDataTable


                'If d.Rows.Count > 0 Then
                '    Dim _servieCharge As DataTable = d.Select("IsPaid=true").CopyToDataTable
                '    'Payment.serviceCharge = Convert.ToDecimal(_servieCharge.Compute("Sum(Cost)", "")) '.Rows(0)("Cost")
                'Else
                '    Dim _servieCharge As DataTable = dtService.Select("IsPaid=false").CopyToDataTable
                '    Payment.serviceCharge = Convert.ToDecimal(_servieCharge.Compute("Sum(Cost)", ""))
                'End If
                '   Dim _servieCharge As DataTable = dtService.Select("IsPaid=false").CopyToDataTable
                Payment.serviceCharge = Convert.ToDecimal(dtService.Compute("Sum(Cost)", ""))
               
                ' Next
            End If

            Payment.ShowDialog()
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub tpServicesDetails_VisibleChanged(sender As Object, e As EventArgs) Handles tpServicesDetails.VisibleChanged
        If tpServicesDetails.IsSelected = True Then
            cmdPayment.Enabled = True
            Me.cmdPayment.ForeColor = Color.White
            Me.cmdPayment.BackColor = Color.FromArgb(0, 113, 188)
            Me.cmdPayment.Font = New Font("Callibri", 7, FontStyle.Bold)
        Else
            cmdPayment.Enabled = False
            Me.cmdPayment.BackColor = Color.FromArgb(208, 208, 208)
            Me.cmdPayment.Font = New Font("Callibri", 7)
            Me.cmdCheckout.Font = New Font("Callibri", 7)

        End If

    End Sub
End Class