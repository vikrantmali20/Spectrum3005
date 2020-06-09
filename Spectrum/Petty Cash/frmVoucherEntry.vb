Imports SpectrumCommon
Imports SpectrumBL
Imports System.Collections
Imports System.ComponentModel
Imports System.Linq
Imports SpectrumPrint

Public Class frmPCVoucherEntry
    Public Sub New()

        ' This call is required by the designer.
        BLInstance = New PettyCashVoucher()
        If clsAdmin.TerminalID.ToUpper() <> clsDefaultConfiguration.PettyCashTerminalId.ToUpper() Then
            ShowMessage(getValueByKey("frmPCVoucherEntry.invalidterminal"), getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If
        Dim request As New ValidationRequest
        request.SiteCode = clsAdmin.SiteCode
        request.TerminalID = clsAdmin.TerminalID
        If BLInstance.CheckIfPettyCashTerminalIsOpen(request) = False Then
            ShowMessage(getValueByKey("frmPCVoucherEntry.tillclosedmsg"), getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub
    Private _LastSelectedParty As String
    Private _VoucherId As String = String.Empty
    Public Property VoucherId As String
        Get
            Return _VoucherId
        End Get
        Set(ByVal value As String)
            _VoucherId = value
        End Set
    End Property

    Private _BLInstance As IPettyCashVoucher
    Public Property BLInstance As IPettyCashVoucher
        Get
            Return _BLInstance
        End Get
        Set(ByVal value As IPettyCashVoucher)
            _BLInstance = value
        End Set
    End Property

    Private _Response As GetVoucherEntryResponse
    Public Property Response As GetVoucherEntryResponse
        Get
            Return _Response
        End Get
        Set(ByVal value As GetVoucherEntryResponse)
            _Response = value
        End Set
    End Property

    Private _GridSource As BindingList(Of VoucherDtl)
    Public Property GridSource As BindingList(Of VoucherDtl)
        Get
            If _GridSource Is Nothing Then
                _GridSource = New BindingList(Of VoucherDtl)
            End If
            Return _GridSource
        End Get
        Set(ByVal value As BindingList(Of VoucherDtl))
            _GridSource = value
        End Set
    End Property

    Private Sub frmPCVoucherEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim objdefaultCM As New clsDefaultConfiguration("CMS")
            objdefaultCM.GetDefaultSettings()
            dgNarration.AutoGenerateColumns = False
            'SetComboDisplayMembers()
            Dim request As New GetVoucherEntryRequest
            request.SiteCode = clsAdmin.SiteCode
            request.FinYear = clsAdmin.Financialyear
            request.VoucherID = VoucherId
            Response = BLInstance.GetVoucherDetails(request)
            If Response.VoucherHeader IsNot Nothing Then
                If String.IsNullOrEmpty(Response.VoucherHeader.PreparedBY) Then
                    Response.VoucherHeader.PreparedBY = clsAdmin.UserCode
                End If
                If Response.VoucherHeader.ExpenseDate = DateTime.MinValue Then
                    Response.VoucherHeader.ExpenseDate = clsAdmin.DayOpenDate
                End If
                lblVcherDateValue.Text = Response.VoucherHeader.ExpenseDate.ToShortDateString()
                lblPrepByValue.Text = Response.VoucherHeader.PreparedBY
                lblVcherNoValue.Text = Response.VoucherHeader.VoucherID
                lblCurrency.Text = Response.VoucherHeader.CurrencyCode
                txtApprovedBy.Text = Response.VoucherHeader.Approvedby
                txtRecievedBy.Text = Response.VoucherHeader.ReceivedBY
                cmbVoucherType.DataSource = Response.VoucherTypes
                txtRefDocNo.Text = Response.VoucherHeader.RefDocNumber
                dtRefDocDate.Value = Response.VoucherHeader.RefDocDate
                If Not String.IsNullOrEmpty(Response.VoucherHeader.VoucherTypeCode) Then
                    cmbVoucherType.SelectedValue = Response.VoucherHeader.VoucherTypeCode
                End If
                If Not String.IsNullOrEmpty(Response.VoucherHeader.VoucherAccountID) Then
                    cmbVcherAccType.SelectedValue = Response.VoucherHeader.VoucherAccountID
                End If
                If Response.VoucherHeader.VoucherDetails.Count > 0 Then
                    GridSource = Response.VoucherHeader.VoucherDetails.ToBindingList()
                End If
                dgNarration.DataSource = GridSource
                CalculateTotalAmount()
                If Not String.IsNullOrEmpty(Response.VoucherHeader.PaidTo) Then
                    If String.IsNullOrEmpty(Response.VoucherHeader.EmployeeID) AndAlso String.IsNullOrEmpty(Response.VoucherHeader.SupplierID) Then
                        cmbPartyName.Visible = False
                        txtPartyName.Visible = True
                        rbnOther.Checked = True
                        txtPartyName.Text = Response.VoucherHeader.PaidTo
                    ElseIf Not String.IsNullOrEmpty(Response.VoucherHeader.EmployeeID) Then
                        rbnEmployee.Checked = True
                        cmbPartyName.SelectedValue = Response.VoucherHeader.EmployeeID
                    ElseIf Not String.IsNullOrEmpty(Response.VoucherHeader.SupplierID) Then
                        rbnSupplier.Checked = True
                        cmbPartyName.SelectedValue = Response.VoucherHeader.SupplierID
                    End If
                Else
                    rbnEmployee.Checked = True
                End If
                If Not String.IsNullOrEmpty(VoucherId) Then
                    EnableReadOnlyMode()
                End If
            Else
                ShowMessage("Falied to load data", getValueByKey("CLAE05"))
            End If
            SetCulture(Me)
            'code added for issue id 1263 by vipul 
            btnCancel.Text = "Clear"
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnAddNarration_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNarration.Click
        Try
            Dim vchrDtl As VoucherDtl = GetNewVoucherDtl()
            If vchrDtl IsNot Nothing Then
                GridSource.Add(vchrDtl)
                ReplaceNarration()
                dgNarration.DataSource = GridSource              
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnDeleteNarration_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteNarration.Click        
        Dim deletedItems = GridSource.Where(Function(x) x.IsSelected)
        If (deletedItems.Count() = 0) Then
            MessageBox.Show(getValueByKey("PettyCash.DeleteNarration"))
        End If
        For i As Integer = deletedItems.Count - 1 To 0 Step -1
            GridSource.Remove(deletedItems(i))
        Next        
        For i As Integer = 0 To GridSource.Count - 1 Step 1
            GridSource(i).LineNumber = i + 1
        Next
        dgNarration.DataSource = GridSource
        CalculateTotalAmount()      
    End Sub

    Private Sub cmbVoucherType_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVoucherType.SelectedValueChanged
        Try
            If Not String.IsNullOrEmpty(cmbVoucherType.SelectedValue) Then
                cmbVcherAccType.DataSource = Response.VoucherAccountTypes.Where(Function(x) x.VoucherTypeCode = cmbVoucherType.SelectedValue).ToList()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmbVcherAccType_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVcherAccType.SelectedValueChanged
        Try
            If Not String.IsNullOrEmpty(cmbVcherAccType.SelectedValue) Then
                GridSource = New BindingList(Of VoucherDtl)
                Dim vchrDtl As VoucherDtl = GetNewVoucherDtl()
                If vchrDtl IsNot Nothing Then
                    GridSource.Add(vchrDtl)
                    ReplaceNarration()
                    dgNarration.DataSource = GridSource                    
                End If
                CalculateTotalAmount()                
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmbPartyName_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPartyName.SelectedValueChanged
        Try
            If Not String.IsNullOrEmpty(cmbPartyName.SelectedValue) Then
                ReplaceNarration()
                '_LastSelectedParty = cmbPartyName.SelectedValue
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Sub txtPartyName_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPartyName.LostFocus
        Try
            If Not String.IsNullOrEmpty(txtPartyName.Text) Then
                ReplaceNarration()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function GetNewVoucherDtl() As VoucherDtl
        Try
            Dim lineNo As Integer = GridSource.Count + 1
            Dim vchrDtl As New VoucherDtl
            vchrDtl.LineNumber = lineNo
            vchrDtl.Narration = Response.VoucherAccountTypes.Where(Function(x) x.VoucherAccountID = cmbVcherAccType.SelectedValue).Select(Function(y) y.Narration).FirstOrDefault()
            Return vchrDtl
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Private Sub ReplaceNarration()
        Try
            Dim name As String = String.Empty
            If rbnOther.Checked Then
                name = txtPartyName.Text
            Else
                name = Response.VoucherParty.Where(Function(x) x.PartyCode = cmbPartyName.SelectedValue).Select(Function(Y) Y.PartyName).FirstOrDefault()
            End If
            For Each item In GridSource
                If item.Narration.Contains("(name)") AndAlso Not String.IsNullOrEmpty(name) Then
                    item.Narration = item.Narration.Replace("(name)", name)
                ElseIf item.Narration.Contains(_LastSelectedParty) AndAlso Not String.IsNullOrEmpty(name) Then
                    item.Narration = item.Narration.Replace(_LastSelectedParty, name)
                End If
            Next
            If Not String.IsNullOrEmpty(name) Then
                _LastSelectedParty = name
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub CalculateTotalAmount()
        Try
            lblTotalValue.Text = GridSource.Sum(Function(x) x.Amount)
            lblAmtInWordValue.Text = AmtInWord(Convert.ToDecimal(lblTotalValue.Text), clsAdmin.CurrencyCode, clsAdmin.CurrencyDescription)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub rbnEmployee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbnEmployee.CheckedChanged
        Try
            If rbnEmployee.Checked Then
                cmbPartyName.Visible = True
                txtPartyName.Visible = False
                cmbPartyName.DataSource = Response.VoucherParty.Where(Function(x) x.PartyType = VoucherPartyType.Employee.ToString()).ToList()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub rbnSupplier_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbnSupplier.CheckedChanged
        Try
            If rbnSupplier.Checked Then
                cmbPartyName.Visible = True
                txtPartyName.Visible = False
                cmbPartyName.DataSource = Response.VoucherParty.Where(Function(x) x.PartyType = VoucherPartyType.Supplier.ToString()).ToList()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub rbnOther_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbnOther.CheckedChanged
        Try
            If rbnOther.Checked Then
                cmbPartyName.Visible = False
                txtPartyName.Visible = True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Dim amtBeforeChange As Decimal
    Private Sub dgNarration_CellBeginEdit(ByVal sender As Object, ByVal e As DataGridViewCellCancelEventArgs) Handles dgNarration.CellBeginEdit
        Try
            amtBeforeChange = GridSource(e.RowIndex).Amount
            If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
                OnTouchKeyBoard()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub dgNarration_CellEndEdit(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgNarration.CellEndEdit
        Try
            Dim amtAfterChange As Decimal = 0
            If e.RowIndex >= 0 AndAlso e.RowIndex <= GridSource.Count - 1 Then
                amtAfterChange = GridSource(e.RowIndex).Amount
                If Val(amtAfterChange) > 9999999999999999 Then
                    ShowMessage(getValueByKey("frmPCVoucherEntry.amountmsg"), "frmPCVoucherEntry.amountmsg - " & getValueByKey("CLAE04"))
                    'ShowMessage("Amount can't be greater than 9999999999999999", "-" & getValueByKey("CLAE04"))
                    GridSource(e.RowIndex).Amount = amtBeforeChange
                    Exit Sub
                End If     
            If amtAfterChange = amtBeforeChange Then
                Exit Sub
            End If
            If amtAfterChange <= 0 Then
                ShowMessage(getValueByKey("frmPCVoucherEntry.invalidamount"), getValueByKey("CLAE04"))
                GridSource(e.RowIndex).Amount = amtBeforeChange
            Else
                amtBeforeChange = amtAfterChange
                CalculateTotalAmount()
            End If

                'code added for issue id 2282 by vipul
                If cmbVoucherType.Text.Equals("Expense") Then
                    If PettyCashReceiptOrExpense() = False Then
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            If cmbVoucherType.SelectedValue = Response.VoucherTypes(0).VoucherTypeCode Then
                cmbVoucherType_SelectionChanged(Nothing, New EventArgs())
            Else
                cmbVoucherType.SelectedValue = Response.VoucherTypes(0).VoucherTypeCode
            End If

            'If cmbVcherAccType.Items.Count > 0 Then cmbVcherAccType.SelectedIndex = 0
            rbnEmployee.Checked = True
            If cmbPartyName.Items.Count > 0 Then cmbPartyName.SelectedIndex = 0
            txtApprovedBy.Text = String.Empty
            txtRecievedBy.Text = String.Empty
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Not String.IsNullOrEmpty(VoucherId) Then
                Dim vobj As New clsCommon 'vipul for insering reprint entry
                vobj.UpdateReprintStatusOfPettyCashVoucher(VoucherId, clsAdmin.SiteCode)
                PrintVoucher()
                Me.Close()
                Exit Sub
            End If
            Dim key As String = String.Empty
            If Not IsValid(key) Then
                ShowMessage(getValueByKey(key), getValueByKey("CLAE04"))
                Exit Sub
            End If

            'code added for issue id 2282 by vipul

            If cmbVoucherType.Text.Equals("Expense") Then
                If PettyCashReceiptOrExpense() = False Then
                    Exit Sub
                End If
            End If


            Dim request As New SaveVoucherRequest
            request.VoucherHeader = Response.VoucherHeader
            request.VoucherHeader.CreatedAt = clsAdmin.SiteCode
            request.VoucherHeader.CreatedBy = clsAdmin.UserCode
            request.VoucherHeader.UpdatedAt = clsAdmin.SiteCode
            request.VoucherHeader.UpdatedBy = clsAdmin.UserCode
            request.VoucherHeader.SiteCode = clsAdmin.SiteCode
            request.VoucherHeader.FinYear = clsAdmin.Financialyear
            request.VoucherHeader.VoucherTypeCode = cmbVoucherType.SelectedValue
            request.VoucherHeader.VoucherAccountID = cmbVcherAccType.SelectedValue
            If rbnEmployee.Checked Then
                request.VoucherHeader.EmployeeID = cmbPartyName.SelectedValue
                request.VoucherHeader.PaidTo = cmbPartyName.SelectedValue
            ElseIf rbnSupplier.Checked Then
                request.VoucherHeader.SupplierID = cmbPartyName.SelectedValue
                request.VoucherHeader.PaidTo = cmbPartyName.SelectedValue
            ElseIf rbnOther.Checked Then
                request.VoucherHeader.PaidTo = txtPartyName.Text
            End If
            request.VoucherHeader.TotalAmt = Convert.ToDecimal(lblTotalValue.Text)
            request.VoucherHeader.Approvedby = txtApprovedBy.Text
            request.VoucherHeader.ReceivedBY = txtRecievedBy.Text
            request.VoucherHeader.RefDocNumber = txtRefDocNo.Text
            If Not IsDBNull(dtRefDocDate.Value) AndAlso dtRefDocDate.Value IsNot Nothing Then
                request.VoucherHeader.RefDocDate = dtRefDocDate.Value
            End If
            request.VoucherHeader.VoucherDetails = GridSource.ToList()
            If BLInstance.SaveVoucherData(request) Then
                ShowMessage(String.Format(getValueByKey("frmPCVoucherEntry.savesuccess"), request.VoucherHeader.VoucherID), getValueByKey("CLAE04"))
                Dim printRequest As New PrintVoucherRequest
                printRequest.VoucherHeader = request.VoucherHeader
                printRequest.VoucherAccountTypes = Response.VoucherAccountTypes
                printRequest.VoucherParty = Response.VoucherParty
                printRequest.dtPrinterInfo = dtPrinterInfo
                If PrintVoucher() Then
                    Me.Close()
                End If
            Else
                ShowMessage(getValueByKey("frmPCVoucherEntry.savefailed"), getValueByKey("CLAE05"))
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function PrintVoucher() As Boolean
        Try
            Dim clsVocuherPrint As New clsPettyCashVoucherPrint
            Dim printRequest As New PrintVoucherRequest
            printRequest.VoucherHeader = Response.VoucherHeader
            printRequest.VoucherAccountTypes = Response.VoucherAccountTypes
            printRequest.VoucherParty = Response.VoucherParty
            printRequest.dtPrinterInfo = dtPrinterInfo
            printRequest.IsPrviewRequiredForVchr = clsDefaultConfiguration.IsPrviewRequiredForVchr
            Return clsVocuherPrint.PrintVoucher(printRequest, dtPrinterInfo, clsAdmin.CurrencyCode, clsAdmin.CurrencyDescription, clsDefaultConfiguration.PrintFormatNo)
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Sub EnableReadOnlyMode()
        Try
            btnCancel.Visible = False
            btnSave.Text = "Print"
            cmbVoucherType.Enabled = False
            cmbVcherAccType.Enabled = False
            cmbPartyName.Enabled = False
            txtPartyName.Enabled = False
            txtApprovedBy.Enabled = False
            txtRecievedBy.Enabled = False
            btnAddNarration.Enabled = False
            btnDeleteNarration.Enabled = False
            rbnEmployee.Enabled = False
            rbnOther.Enabled = False
            rbnSupplier.Enabled = False
            dgNarration.ReadOnly = True
            txtRefDocNo.Enabled = False
            dtRefDocDate.Enabled = False
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function IsValid(ByRef key As String) As Boolean
        Try
            Dim result As Boolean = True
            If String.IsNullOrEmpty(txtApprovedBy.Text) OrElse String.IsNullOrEmpty(txtRecievedBy.Text) Then
                key = "frmPCVoucherEntry.invaliddata"
                result = False
            End If
            Dim isExist = GridSource.Where(Function(x) String.IsNullOrEmpty(x.Narration) OrElse x.Amount <= 0).FirstOrDefault()
            If isExist IsNot Nothing OrElse GridSource.Count = 0 Then
                key = "frmPCVoucherEntry.invalidnarration"
                result = False
            End If
            isExist = GridSource.Where(Function(x) x.Amount <= 0).FirstOrDefault()
            If isExist IsNot Nothing OrElse GridSource.Count = 0 Then
                key = "PettyCash.Narration.WrongAmount"
                result = False
            End If
            Return result
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Private Sub DataErrorEvent(ByVal sender As System.Object, ByVal e As DataGridViewDataErrorEventArgs) Handles dgNarration.DataError
        ShowMessage("Please enter valid data", getValueByKey(""))
    End Sub

    Public Function Themechange() As String
        Me.BackColor = Color.FromArgb(134, 134, 134)
        Me.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black
        Me.Size = New Size(828, 560)
        Me.lblVoucherType.BackColor = Color.Transparent 'Color.FromArgb(212, 212, 212)
        'Me.lblVoucherType.MaximumSize = New Size(185, 26)
        'Me.lblVoucherType.MinimumSize = New Size(185, 26)
        'Me.lblVoucherType.Size = New System.Drawing.Size(185, 26)
        Me.lblVoucherType.ForeColor = Color.White

        Me.lblVcherDate.BackColor = Color.Transparent ' Color.FromArgb(212, 212, 212)
        'Me.lblVcherDate.MaximumSize = New Size(185, 25)
        'Me.lblVcherDate.MinimumSize = New Size(185, 25)
        'Me.lblVcherDate.Size = New System.Drawing.Size(185, 25)
        Me.lblVcherDate.ForeColor = Color.White

        Me.lblVcherAccType.BackColor = Color.Transparent ' Color.FromArgb(212, 212, 212)
        'Me.lblVcherAccType.MaximumSize = New Size(185, 25)
        'Me.lblVcherAccType.MinimumSize = New Size(185, 25)
        'Me.lblVcherAccType.Size = New System.Drawing.Size(185, 25)
        Me.lblVcherAccType.ForeColor = Color.White

        Me.lblVcherNo.BackColor = Color.Transparent ' Color.FromArgb(212, 212, 212)
        'Me.lblVcherNo.MaximumSize = New Size(95, 25)
        'Me.lblVcherNo.MinimumSize = New Size(95, 25)
        'Me.lblVcherNo.Size = New System.Drawing.Size(95, 25)
        Me.lblVcherNo.ForeColor = Color.White

        Me.lblPartyOption.BackColor = Color.Transparent ' Color.FromArgb(212, 212, 212)
        'Me.lblPartyOption.MaximumSize = New Size(185, 28)
        'Me.lblPartyOption.MinimumSize = New Size(185, 28)
        'Me.lblPartyOption.Size = New System.Drawing.Size(185, 28)
        Me.lblPartyOption.ForeColor = Color.White

        Me.lblPartyName.BackColor = Color.Transparent ' Color.FromArgb(212, 212, 212)
        'Me.lblPartyName.MaximumSize = New Size(185, 28)
        'Me.lblPartyName.MinimumSize = New Size(185, 28)
        'Me.lblPartyName.Size = New System.Drawing.Size(185, 28)
        Me.lblPartyName.ForeColor = Color.White

        Me.lblVcherDateValue.BackColor = Color.Transparent ' Color.White 'Color.FromArgb(212, 212, 212)
        'Me.lblVcherDateValue.MaximumSize = New Size(205, 25)
        'Me.lblVcherDateValue.MinimumSize = New Size(205, 25)
        'Me.lblVcherDateValue.Size = New System.Drawing.Size(205, 25)
        Me.lblVcherDateValue.ForeColor = Color.White

        Me.lblVcherNoValue.BackColor = Color.Transparent ' Color.FromArgb(212, 212, 212)
        'Me.lblVcherNoValue.MaximumSize = New Size(185, 25)
        'Me.lblVcherNoValue.MinimumSize = New Size(185, 25)
        'Me.lblVcherNoValue.Size = New System.Drawing.Size(185, 25)
        Me.lblVcherNoValue.ForeColor = Color.White

        Me.rbnOther.BackColor = Color.Transparent
        Me.rbnOther.ForeColor = Color.White

        Me.rbnSupplier.BackColor = Color.Transparent
        Me.rbnSupplier.ForeColor = Color.White

        Me.rbnEmployee.BackColor = Color.Transparent
        Me.rbnEmployee.ForeColor = Color.White


        Me.lblAmountInWord.BackColor = Color.Transparent
        Me.lblAmountInWord.ForeColor = Color.White

        Me.lblPreparedBy.BackColor = Color.Transparent
        Me.lblPreparedBy.ForeColor = Color.White

        Me.lblPrepByValue.BackColor = Color.Transparent
        Me.lblPrepByValue.ForeColor = Color.White

        Me.lblApprovedBy.BackColor = Color.Transparent
        Me.lblApprovedBy.ForeColor = Color.White

        Me.lblRefDocNo.BackColor = Color.Transparent
        Me.lblRefDocNo.ForeColor = Color.White

        Me.lblRecievedBy.BackColor = Color.Transparent
        Me.lblRecievedBy.ForeColor = Color.White

        Me.lblRefDocDate.BackColor = Color.Transparent
        Me.lblRefDocDate.ForeColor = Color.White

        Me.lblTotalAmount.BackColor = Color.Transparent
        Me.lblTotalAmount.ForeColor = Color.White

        Me.FlowLayoutPanel1.BackColor = Color.Transparent
        Me.FlowLayoutPanel1.ForeColor = Color.White

        Me.lblAmtInWordValue.BackColor = Color.Transparent
        Me.lblAmtInWordValue.ForeColor = Color.White

        Me.lblTotalValue.BackColor = Color.Transparent
        Me.lblTotalValue.ForeColor = Color.White

        Me.lblCurrency.BackColor = Color.Transparent
        Me.lblTotalValue.ForeColor = Color.White

        Me.txtApprovedBy.Size = New System.Drawing.Size(183, 21)
        Me.txtRecievedBy.Size = New System.Drawing.Size(200, 21)

        Me.dgNarration.BackgroundColor = Color.FromArgb(134, 134, 134)

        Me.TableLayoutPanel1.Dock = DockStyle.None

        Me.TableLayoutPanel1.RowStyles(4).SizeType = SizeType.Absolute
        Me.TableLayoutPanel1.RowStyles(4).Height = 40

        Me.FlowLayoutPanel2.Width = 40

        Me.TableLayoutPanel1.RowStyles(7).SizeType = SizeType.Absolute
        Me.TableLayoutPanel1.RowStyles(7).Height = 30

        Me.TableLayoutPanel1.RowStyles(11).SizeType = SizeType.Absolute
        Me.TableLayoutPanel1.RowStyles(11).Height = 42

        'Me.btnAddNarration
        '
        Me.btnAddNarration.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnAddNarration.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnAddNarration.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnAddNarration.MaximumSize = New Size(124, 35)
        Me.btnAddNarration.MinimumSize = New Size(124, 35)
        Me.btnAddNarration.Size = New System.Drawing.Size(124, 35)
        Me.btnAddNarration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddNarration.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnAddNarration.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnAddNarration.FlatStyle = FlatStyle.Flat
        Me.btnAddNarration.FlatAppearance.BorderSize = 0
        Me.btnAddNarration.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'btnDeleteNarration
        '
        Me.btnDeleteNarration.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnDeleteNarration.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnDeleteNarration.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnDeleteNarration.MaximumSize = New Size(124, 35)
        Me.btnDeleteNarration.MinimumSize = New Size(124, 35)
        Me.btnDeleteNarration.Size = New System.Drawing.Size(124, 35)
        Me.btnDeleteNarration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDeleteNarration.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnDeleteNarration.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnDeleteNarration.FlatStyle = FlatStyle.Flat
        Me.btnDeleteNarration.FlatAppearance.BorderSize = 0
        Me.btnDeleteNarration.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'btnExit
        '
        Me.btnExit.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnExit.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnExit.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnExit.MaximumSize = New Size(75, 32)
        Me.btnExit.MinimumSize = New Size(75, 32)
        Me.btnExit.Size = New System.Drawing.Size(75, 32)
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnExit.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnExit.FlatStyle = FlatStyle.Flat
        Me.btnExit.FlatAppearance.BorderSize = 0
        Me.btnExit.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'btnCancel
        '
        Me.btnCancel.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnCancel.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnCancel.MaximumSize = New Size(75, 32)
        Me.btnCancel.MinimumSize = New Size(75, 32)
        Me.btnCancel.Size = New System.Drawing.Size(75, 32)
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnCancel.FlatStyle = FlatStyle.Flat
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'btnSave
        '
        Me.btnSave.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnSave.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnSave.MaximumSize = New Size(113, 32)
        Me.btnSave.MinimumSize = New Size(113, 32)
        Me.btnSave.Size = New System.Drawing.Size(113, 32)
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnSave.FlatStyle = FlatStyle.Flat
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        'dgNarration.EnableHeadersVisualStyles = True
        'dgNarration.styl()
        'dgNarration.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        dgNarration.BackgroundColor = Color.White
        dgNarration.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(177, 227, 253)
        dgNarration.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgNarration.DefaultCellStyle.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgNarration.DefaultCellStyle.SelectionBackColor = Color.LightBlue


        Return ""
    End Function
    'code added for issue id 2282 by vipul
    Private Function PettyCashReceiptOrExpense() As Boolean

        Dim PettyamtAfterChange As Decimal = 0
        PettyamtAfterChange = Convert.ToDecimal(lblTotalValue.Text)



        Dim prettyobj As New clsCommon
        Dim prettyTillobj As New clsTill()
        Dim pretttyrequest As New SpectrumCommon.GetVoucherBalanceRequest()
        Dim TillvalueAtOpening As Double
        Dim dtcashAmt As DataTable
        Dim pettyvoucher As IPettyCashVoucher = New PettyCashVoucher()
        Dim PettyCashRecAmt, PettyCashExpAmt As Double
        pretttyrequest.DayOpenDate = clsAdmin.DayOpenDate
        pretttyrequest.FinYear = clsAdmin.Financialyear
        pretttyrequest.SiteCode = clsAdmin.SiteCode

        PettyCashRecAmt = pettyvoucher.GetTotalPettyCashReceipt(pretttyrequest).ToString()
        PettyCashExpAmt = pettyvoucher.GetTotalPettyCashExpense(pretttyrequest).ToString()
        TillvalueAtOpening = prettyTillobj.GetTillOpenDetail(clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.DayOpenDate)
        dtcashAmt = prettyobj.GetTerminalCashDetail(clsAdmin.TerminalID, clsAdmin.SiteCode, clsAdmin.DayOpenDate)


        If (dtcashAmt IsNot Nothing AndAlso dtcashAmt.Rows.Count > 0 Or (dtcashAmt.Rows.Count = 0)) Then

            If dtcashAmt.Rows.Count = 0 Then
                dtcashAmt.Rows.Add("INR", "Rupees", "0.0", "1.00", "0.0")
            End If
            ' Dim sum As Integer = Convert.ToInt32(dtcashAmt.Compute("SUM(Total)", String.Empty))


            Dim totalcashindrower As Decimal = Convert.ToDecimal(dtcashAmt.Compute("SUM(Total)", String.Empty)) + TillvalueAtOpening + Val(PettyCashRecAmt) - Val(PettyCashExpAmt)

            If PettyamtAfterChange > totalcashindrower Then
                ShowMessage("YOU DON'T HAVE ENOUGH CASH TO SPEND", "INFORMATION")
                Return False

                Exit Function
            End If

        End If



        Return True

    End Function
End Class