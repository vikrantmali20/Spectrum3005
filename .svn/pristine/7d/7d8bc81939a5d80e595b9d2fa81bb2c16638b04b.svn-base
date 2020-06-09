Imports C1.Win.C1FlexGrid
Imports SpectrumBL

Public Class frmNSearchReservation

#Region "Global Variables"
    Dim objClp As New clsCLPCustomer
    Dim objCls As New clsReservation
    Dim dvCustmInfo As DataView
    Dim dtCustmData As DataTable
    Dim IsFilterVisible As Boolean = False
    Dim RaisedBy As String = ""
    Dim _ReturnResultSet As New DataTable
    Dim vCustmType, vSiteCode, vCustCode As String
    Dim vFilterCustmInfo As String = String.Empty
    Dim objGr As New frmNewReservation
    Dim IsDeleteAuth As Boolean = False
    Protected controlList As New ArrayList
    Dim SearchTableNo As String = "", SearchCustName As String = "", SearchNoOfPeople As String = "", SearchPhone As String = "", SearchDate As String = ""
    Dim SearchCustNo As String = "", SearchFromTime As String = "", SearchToTime As String = "", SearchReservationId As String = ""
    Dim SearchNoofReservationIds As New List(Of String)
    Dim SearchNoofTableNos As New List(Of String)
#End Region

#Region "Functions"

    Private Function Themechange()
        dgMainGrid.VisualStyle = VisualStyle.Custom
        dgMainGrid.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgMainGrid.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgMainGrid.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
        dgMainGrid.Rows.MinSize = 25
        dgMainGrid.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        dgMainGrid.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgMainGrid.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)

        cmdDelete.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdDelete.BackColor = Color.Transparent
        cmdDelete.BackColor = Color.FromArgb(0, 107, 163)
        cmdDelete.ForeColor = Color.FromArgb(255, 255, 255)
        cmdDelete.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdDelete.FlatStyle = FlatStyle.Flat
        cmdDelete.FlatAppearance.BorderSize = 0
        cmdDelete.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdDelete.Size = New Size(92, 38)
        cmdDelete.TextAlign = ContentAlignment.MiddleCenter

        cmdSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdSearch.BackColor = Color.Transparent
        cmdSearch.BackColor = Color.FromArgb(0, 107, 163)
        cmdSearch.ForeColor = Color.FromArgb(255, 255, 255)
        cmdSearch.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdSearch.FlatStyle = FlatStyle.Flat
        cmdSearch.FlatAppearance.BorderSize = 0
        cmdSearch.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdSearch.Size = New Size(66, 27)

        CmdShowFilter.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CmdShowFilter.BackColor = Color.Transparent
        CmdShowFilter.BackColor = Color.FromArgb(0, 107, 163)
        CmdShowFilter.ForeColor = Color.FromArgb(255, 255, 255)
        CmdShowFilter.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CmdShowFilter.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CmdShowFilter.FlatStyle = FlatStyle.Flat
        CmdShowFilter.FlatAppearance.BorderSize = 0
        CmdShowFilter.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        CmdShowFilter.Size = New Size(85, 27)

        btnChangeTable.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnChangeTable.BackColor = Color.Transparent
        btnChangeTable.BackColor = Color.FromArgb(0, 107, 163)
        btnChangeTable.ForeColor = Color.FromArgb(255, 255, 255)
        btnChangeTable.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnChangeTable.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnChangeTable.FlatStyle = FlatStyle.Flat
        btnChangeTable.FlatAppearance.BorderSize = 0
        btnChangeTable.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnChangeTable.Size = New Size(92, 38)

        btnChangeTime.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnChangeTime.BackColor = Color.Transparent
        btnChangeTime.BackColor = Color.FromArgb(0, 107, 163)
        btnChangeTime.ForeColor = Color.FromArgb(255, 255, 255)
        btnChangeTime.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnChangeTime.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnChangeTime.FlatStyle = FlatStyle.Flat
        btnChangeTime.FlatAppearance.BorderSize = 0
        btnChangeTime.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnChangeTime.Size = New Size(92, 38)

        cmdSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdSearch.BackColor = Color.Transparent
        cmdSearch.BackColor = Color.FromArgb(0, 107, 163)
        cmdSearch.ForeColor = Color.FromArgb(255, 255, 255)
        cmdSearch.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdSearch.FlatStyle = FlatStyle.Flat
        cmdSearch.FlatAppearance.BorderSize = 0
        cmdSearch.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdSearch.Size = New Size(70, 25)

        CtrlBtnClear.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnClear.BackColor = Color.Transparent
        CtrlBtnClear.BackColor = Color.FromArgb(0, 107, 163)
        CtrlBtnClear.ForeColor = Color.FromArgb(255, 255, 255)
        CtrlBtnClear.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlBtnClear.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlBtnClear.FlatStyle = FlatStyle.Flat
        CtrlBtnClear.FlatAppearance.BorderSize = 0
        CtrlBtnClear.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        CtrlBtnClear.Size = New Size(70, 25)

        btnCheckIn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        btnCheckIn.BackColor = Color.Transparent
        btnCheckIn.BackColor = Color.FromArgb(0, 107, 163)
        btnCheckIn.ForeColor = Color.FromArgb(255, 255, 255)
        btnCheckIn.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        btnCheckIn.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnCheckIn.FlatStyle = FlatStyle.Flat
        btnCheckIn.FlatAppearance.BorderSize = 0
        btnCheckIn.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        btnCheckIn.Size = New Size(92, 38)
        btnCheckIn.TextAlign = ContentAlignment.MiddleCenter

        lblPhoneNo.BackColor = Color.FromArgb(212, 212, 212)
        lblPhoneNo.AutoSize = False
        '  lblPhoneNo.Size = New Size(150, 20)
        ' lblPhoneNo.Margin = New Padding(3, 0, 0, 0)
        lblPhoneNo.BorderStyle = BorderStyle.None

        lblName.BackColor = Color.FromArgb(212, 212, 212)
        lblName.AutoSize = False
        '  lblName.Size = New Size(150, 20)
        ' lblName.Margin = New Padding(3, 0, 0, 0)
        lblName.BorderStyle = BorderStyle.None

        CtrlLabel5.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel5.AutoSize = False
        'CtrlLabel5.Size = New Size(150, 20)
        'CtrlLabel5.Margin = New Padding(3, 0, 0, 0)
        CtrlLabel5.BorderStyle = BorderStyle.None

        CtrlLabel2.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLabel2.AutoSize = False
        ' CtrlLabel2.Size = New Size(150, 20)
        ' CtrlLabel2.Margin = New Padding(3, 0, 0, 0)
        CtrlLabel2.BorderStyle = BorderStyle.None

        lblTabelNo.BackColor = Color.FromArgb(212, 212, 212)
        'lblTabelNo.AutoSize = False
        'lblTabelNo.Size = New Size(150, 20)
        lblTabelNo.Margin = New Padding(3, 0, 0, 0)
        lblTabelNo.BorderStyle = BorderStyle.None

        lblGrievanceId.BackColor = Color.FromArgb(212, 212, 212)
        lblGrievanceId.AutoSize = False
        ' lblGrievanceId.Size = New Size(150, 20)
        '  lblGrievanceId.Margin = New Padding(3, 0, 0, 0)

        lblTicketTitle.BackColor = Color.FromArgb(212, 212, 212)
        lblTicketTitle.AutoSize = False
        ' lblTicketTitle.Size = New Size(150, 20)
        'lblTicketTitle.Margin = New Padding(3, 0, 0, 0)


        ' tblpanel.ColumnStyles(0).SizeType = SizeType.Absolute
        'tblpanel.ColumnStyles(0).Width = 100

        ' tblpanel.ColumnStyles(1).SizeType = SizeType.Absolute
        'tblpanel.ColumnStyles(1).Width = 180

        'tblpanel.ColumnStyles(2).SizeType = SizeType.Absolute
        ' tblpanel.ColumnStyles(2).Width = 100

        ' tblpanel.ColumnStyles(3).SizeType = SizeType.Absolute
        ' tblpanel.ColumnStyles(3).Width = 150

        ' tblpanel.RowStyles(0).SizeType = SizeType.Absolute
        ' tblpanel.RowStyles(0).Height = 25

        ' tblpanel.RowStyles(1).SizeType = SizeType.Absolute
        '  tblpanel.RowStyles(1).Height = 25

        ' tblpanel.RowStyles(4).SizeType = SizeType.Absolute
        ' tblpanel.RowStyles(4).Height = 25

        ' tblpanel.RowStyles(5).SizeType = SizeType.Absolute
        ' tblpanel.RowStyles(5).Height = 25
        '
        'tblpanel.RowStyles(6).SizeType = SizeType.Absolute
        ' tblpanel.RowStyles(6).Height = 25
    End Function

    Private Function clearFilter()
        txtTableNo.Text = ""
        txtName.Text = ""
        txtPhone.Text = ""
        txtDate.SelectedText = ""
        txtTime.SelectedText = ""
    End Function

    Private Sub ShowDetail()
        Try
            dgMainGrid.DataSource = _ReturnResultSet
            GridSettings()
            SetCulture(Me, Me.Name)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub GridSettings()
        Try
            dgMainGrid.Cols("Sel").Width = 30
            dgMainGrid.Cols("Sel").AllowEditing = True
            dgMainGrid.Cols("Sel").Caption = ""

            dgMainGrid.Cols("TableNumber").Width = 100
            dgMainGrid.Cols("TableNumber").AllowEditing = False
            dgMainGrid.Cols("TableNumber").Caption = "Table No."

            dgMainGrid.Cols("Name").Width = 200
            dgMainGrid.Cols("Name").AllowEditing = False
            dgMainGrid.Cols("Name").Caption = "Name"

            dgMainGrid.Cols("NoOfPeople").Width = 100
            dgMainGrid.Cols("NoOfPeople").AllowEditing = False
            dgMainGrid.Cols("NoOfPeople").Caption = "No.of People"

            dgMainGrid.Cols("Phone").Width = 100
            dgMainGrid.Cols("Phone").AllowEditing = False
            dgMainGrid.Cols("Phone").Caption = "Phone No."

            dgMainGrid.Cols("Date").Width = 100
            dgMainGrid.Cols("Date").AllowEditing = False
            dgMainGrid.Cols("Date").Caption = "Date"
            dgMainGrid.Cols("Date").Format = "d"

            dgMainGrid.Cols("CustomerNo").Width = 120
            dgMainGrid.Cols("CustomerNo").AllowEditing = False
            dgMainGrid.Cols("CustomerNo").Caption = "Customer No."

            dgMainGrid.Cols("FromTime").Width = 80
            dgMainGrid.Cols("FromTime").AllowEditing = False
            dgMainGrid.Cols("FromTime").Caption = "From Time"
            dgMainGrid.Cols("FromTime").Format = "t"

            dgMainGrid.Cols("ReservationId").Visible = False
            '   dgMainGrid.Cols("ReservationId").AllowEditing = False
            '  dgMainGrid.Cols("ReservationId").Caption = "Table No."
            dgMainGrid.AutoSizeCols()
            ' dgMainGrid.AutoResize = True
            dgMainGrid.AllowResizing = False
            dgMainGrid.Size = New Point(cmdDelete.Right - tblpanel.Left, dgMainGrid.Size.Height)

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' validation if checkbox selected in grid 
    ''' </summary>
    ''' <remarks></remarks>
    Sub SelectionValidation()
        Dim SelCount As Integer = 0
        For i = 1 To dgMainGrid.Rows.Count - 1
            If dgMainGrid.Rows(i)("Sel") = True Then
                SelCount = SelCount + 1
            End If
        Next
        If SelCount = 0 Then
            ShowMessage("Please Select one Customer", "Information")
            Exit Sub
        ElseIf SelCount = 2 Then
            ShowMessage("Please Select only one Customer", "Information")
            Exit Sub
        End If
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' calling search button click event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmNSearchReservation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            btnChangeTable.Enabled = False
            btnChangeTime.Enabled = False
            btnCheckIn.Enabled = False
            cmdDelete.Enabled = False
            Call cmdSearch_Click(Nothing, Nothing)
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' loading customer data from table and and displaying in grid 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        Try
            Dim tableNo As String
            If Not IsDBNull(txtTableNo.Text) Then
                tableNo = txtTableNo.Text.Replace(",", "','")
            End If
            Dim NoOfPeople = txtPeople.Text
            Dim CustomerName As String = txtName.Text
            Dim MobileNo As String = txtPhone.Text
            Dim FromDate As DateTime
            If Not IsDBNull(txtDate.Value) Then
                FromDate = txtDate.Value
            End If
            Dim Time As DateTime
            If Not IsDBNull(txtTime.Value) Then
                Time = txtTime.Value
            End If
            _ReturnResultSet = objCls.GetReservationDetails(tableNo, CustomerName, NoOfPeople, MobileNo, FromDate, Time)
            ShowDetail()

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' showing and hiding controls and changing location of controls
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CmdShowFilter_Click(sender As Object, e As EventArgs) Handles CmdShowFilter.Click
        If IsFilterVisible = False Then
            tblpanel.Visible = True
            dgMainGrid.Top = tblpanel.Bottom + 5
            btnChangeTable.Top = dgMainGrid.Bottom + 5
            btnChangeTime.Top = dgMainGrid.Bottom + 5
            btnCheckIn.Top = dgMainGrid.Bottom + 5
            cmdDelete.Top = dgMainGrid.Bottom + 5
            CmdShowFilter.Text = "- Filter"
            ' chkselectall.Location = New Point(dgMainGrid.Left + 10, dgMainGrid.Top + 5)
        Else
            tblpanel.Visible = False
            dgMainGrid.Top = CmdShowFilter.Bottom + 5
            btnChangeTable.Top = dgMainGrid.Bottom + 5
            btnChangeTime.Top = dgMainGrid.Bottom + 5
            btnCheckIn.Top = dgMainGrid.Bottom + 5
            cmdDelete.Top = dgMainGrid.Bottom + 5
            CmdShowFilter.Text = "+ Filter"
            ' TableLayoutPanel2.Location = New Point(dgMainGrid.Size.Width, cmdNew.Top - CmdShowFilter.Bottom - 10)
            '  chkselectall.Location = New Point(dgMainGrid.Left + 10, dgMainGrid.Top + 5)
        End If
        IsFilterVisible = Not IsFilterVisible
    End Sub

    ''' <summary>
    ''' clearing all data 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrlBtnClear_Click(sender As Object, e As EventArgs) Handles CtrlBtnClear.Click
        clearFilter()
        ShowDetail()
    End Sub

    ''' <summary>
    ''' on double click event, the new reservation form should be open and displaying data in that form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgMainGrid_DoubleClick(sender As Object, e As EventArgs) Handles dgMainGrid.DoubleClick
        Try

            If dgMainGrid.Rows.Count = 0 Or 1 Then
                Exit Sub
            End If
            Using objGr As New frmNewReservation
                objGr.StartPosition = FormStartPosition.CenterParent
                objGr.IsEdit = True
                objGr.DineInNumber = dgMainGrid.Rows(dgMainGrid.RowSel)("TableNumber").ToString()
                objGr.CustomerName = dgMainGrid.Rows(dgMainGrid.RowSel)("Name")
                objGr.NoOfPeople = dgMainGrid.Rows(dgMainGrid.RowSel)("NoOfPeople").ToString()
                objGr.Phone = dgMainGrid.Rows(dgMainGrid.RowSel)("Phone")
                objGr.custDate = dgMainGrid.Rows(dgMainGrid.RowSel)("Date")
                objGr.FromTime = dgMainGrid.Rows(dgMainGrid.RowSel)("FromTime")
                objGr.ToTime = dgMainGrid.Rows(dgMainGrid.RowSel)("ToTime")
                If objGr.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    cmdSearch_Click(sender, e)
                Else
                    frmNSearchReservation_Load(sender, e)
                    objGr.Close()

                End If

            End Using
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs)
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

    Private Sub txtTableNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTableNo.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' the button for cancelling the reservation made 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Try
            Dim eventType As Int32
            Dim TableList As String = " "
            Dim SelCount As Integer = 0
            For i = 1 To dgMainGrid.Rows.Count - 1
                If dgMainGrid.Rows(i)("Sel") = True Then
                    SelCount = SelCount + 1
                End If
            Next
            If SelCount = 0 Then
                ShowMessage("Please Select one Customer", "Information")
                Exit Sub
            End If
            For i = 0 To SearchNoofTableNos.Count - 1
                If i = 0 Then
                    TableList &= SearchNoofTableNos(i)
                Else
                    TableList &= ", " & SearchNoofTableNos(i)
                End If
            Next
            ShowMessage("Are You Sure you want to cancel Reservation For Table No :& " & TableList & " " & vbCrLf & "Name :" & SearchCustName & vbCrLf & "Phone Number :" & " " & SearchPhone, "Cancel Confirmation", eventType, "No", "Yes")
            If eventType = 1 Then
                If objCls.UpdateCancelReservationDetails(SearchNoofReservationIds, clsAdmin.SiteCode) Then
                    ShowMessage("Reservation Cancelled Successful" & vbCrLf & "for" & " " & "Table No :" & " " & TableList & vbCrLf & "Name :" & SearchCustName & vbCrLf & "Phone Number :" & " " & SearchPhone, "Cancel Confirmation")
                    frmNSearchReservation_Load(sender, e)
                    '   cmdSearch_Click(sender, e)
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' loading the new reservation form and displaying loaded tables 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnChangeTable_Click(sender As Object, e As EventArgs) Handles btnChangeTable.Click
        Try
            Using objGr As New frmNewReservation
                SelectionValidation()
                objGr.StartPosition = FormStartPosition.CenterParent
                objGr.IsEdit = True
                objGr.IsChangeTime = False
                objGr.DineInNumber = SearchTableNo
                objGr.CustomerName = SearchCustName
                objGr.NoOfPeople = SearchNoOfPeople
                objGr.Phone = SearchPhone
                objGr.custDate = SearchDate
                objGr.FromTime = SearchFromTime
                objGr.PreviousFromTime = objGr.FromTime
                objGr.PreviousToTime = objGr.ToTime
                objGr.CustomerNo = SearchCustNo
                objGr.ReservationIdEdit = SearchReservationId
                objGr.IsCheckIn = False
                If objGr.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    cmdSearch_Click(sender, e)
                Else
                    frmNSearchReservation_Load(sender, e)
                    objGr.Close()

                End If
            End Using
        Catch Ex As Exception
            ShowMessage(Ex.Message, getValueByKey("CLAE05"))
            LogException(Ex)
        End Try
    End Sub

    ''' <summary>
    ''' loading the new reservation form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnChangeTime_Click(sender As Object, e As EventArgs) Handles btnChangeTime.Click
        Try
            Using objGr As New frmNewReservation
                SelectionValidation()
                objGr.StartPosition = FormStartPosition.CenterParent
                objGr.IsChangeTime = True
                objGr.IsEdit = False
                objGr.DineInNumber = SearchTableNo
                objGr.CustomerName = SearchCustName
                objGr.NoOfPeople = SearchNoOfPeople
                objGr.Phone = SearchPhone
                objGr.custDate = SearchDate
                objGr.FromTime = SearchFromTime
                objGr.PreviousFromTime = objGr.FromTime
                objGr.PreviousToTime = objGr.ToTime
                objGr.CustomerNo = SearchCustNo
                objGr.ReservationIdEdit = SearchReservationId
                objGr.IsCheckIn = False
                objGr.CustomerNo = dgMainGrid.Rows(dgMainGrid.RowSel)("CustomerNo").ToString()
                If objGr.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    cmdSearch_Click(sender, e)
                Else
                    frmNSearchReservation_Load(sender, e)
                    objGr.Close()

                End If
            End Using
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub


    ''' <summary>
    ''' loading the new reservation form
    ''' </summary>
    ''' <param name="sender"></param>\
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCheckIn_Click(sender As Object, e As EventArgs) Handles btnCheckIn.Click
        Try
            Using objGr As New frmNewReservation
                SelectionValidation()
                Dim CheckFromTimeLockTime As DateTime = Convert.ToDateTime(SearchFromTime).AddMinutes(-clsDefaultConfiguration.LockTime)
                If Convert.ToDateTime(SearchDate).ToShortDateString = DateTime.Now.ToShortDateString Then
                    If DateTime.Compare(CheckFromTimeLockTime.ToShortTimeString, DateTime.Now.ToShortTimeString) >= 0 Then
                        ShowMessage("You can't CheckIn before ReservationTime:" + CheckFromTimeLockTime.ToShortTimeString, "Information")
                        Exit Sub
                    End If
                ElseIf Convert.ToDateTime(SearchDate).ToShortDateString <> DateTime.Now.ToShortDateString Then
                    ShowMessage("You can't CheckIn Today", "Information")
                    Exit Sub
                End If
                objGr.StartPosition = FormStartPosition.CenterParent
                objGr.IsEdit = True
                objGr.IsChangeTime = False
                objGr.IsCheckIn = True
                objGr.DineInNumber = SearchTableNo
                objGr.CustomerName = SearchCustName
                objGr.NoOfPeople = SearchNoOfPeople
                objGr.Phone = SearchPhone
                objGr.custDate = SearchDate
                objGr.FromTime = SearchFromTime
                objGr.PreviousFromTime = objGr.FromTime
                objGr.PreviousToTime = objGr.ToTime
                objGr.CustomerNo = SearchCustNo
                objGr.ReservationIdEdit = SearchReservationId
                If objGr.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    cmdSearch_Click(sender, e)
                Else
                    frmNSearchReservation_Load(sender, e)
                    objGr.Close()

                End If
            End Using
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' taking data of selected checkbox in some variables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgMainGrid_AfterEdit(sender As Object, e As RowColEventArgs) Handles dgMainGrid.AfterEdit
        Try
            Dim SelCount As Integer = 0
            For i = 1 To dgMainGrid.Rows.Count - 1
                If dgMainGrid.Rows(i)("Sel") = True Then
                    SearchTableNo = IIf(IsDBNull(dgMainGrid.Rows(i)("TableNumber")), String.Empty, dgMainGrid.Rows(i)("TableNumber"))
                    SearchNoOfPeople = IIf(IsDBNull(dgMainGrid.Rows(i)("NoOfPeople")), String.Empty, dgMainGrid.Rows(i)("NoOfPeople"))
                    SearchCustNo = IIf(IsDBNull(dgMainGrid.Rows(i)("CustomerNo")), String.Empty, dgMainGrid.Rows(i)("CustomerNo"))
                    SearchDate = IIf(IsDBNull(dgMainGrid.Rows(i)("Date")), String.Empty, dgMainGrid.Rows(i)("Date"))
                    SearchFromTime = IIf(IsDBNull(dgMainGrid.Rows(i)("FromTime")), String.Empty, dgMainGrid.Rows(i)("FromTime"))
                    SearchCustName = IIf(IsDBNull(dgMainGrid.Rows(i)("Name")), String.Empty, dgMainGrid.Rows(i)("Name"))
                    SearchPhone = IIf(IsDBNull(dgMainGrid.Rows(i)("Phone")), String.Empty, dgMainGrid.Rows(i)("Phone"))
                    SearchReservationId = IIf(IsDBNull(dgMainGrid.Rows(i)("Reservationid")), String.Empty, dgMainGrid.Rows(i)("Reservationid"))
                End If
            Next
            SearchNoofReservationIds.Clear()
            SearchNoofTableNos.Clear()
            For j = 1 To dgMainGrid.Rows.Count - 1
                Dim SearchReservid As String = ""
                Dim SearchTblNo As String = ""
                If dgMainGrid.Rows(j)("Sel") = True Then
                    SearchReservid = IIf(IsDBNull(dgMainGrid.Rows(j)("Reservationid")), String.Empty, dgMainGrid.Rows(j)("Reservationid"))
                    SearchTblNo = IIf(IsDBNull(dgMainGrid.Rows(j)("TableNumber")), String.Empty, dgMainGrid.Rows(j)("TableNumber"))
                    SearchNoofReservationIds.Add(SearchReservid)
                    SearchNoofTableNos.Add(SearchTblNo)
                    ' SearchReservationId = ""
                    SelCount = SelCount + 1
                End If
                If dgMainGrid.Rows(j)("Sel") = False Then
                    SearchReservid = IIf(IsDBNull(dgMainGrid.Rows(j)("Reservationid")), String.Empty, dgMainGrid.Rows(j)("Reservationid"))
                    SearchTblNo = IIf(IsDBNull(dgMainGrid.Rows(j)("TableNumber")), String.Empty, dgMainGrid.Rows(j)("TableNumber"))
                    SearchNoofReservationIds.Remove(SearchReservid)
                    SearchNoofTableNos.Remove(SearchTblNo)
                    ' SearchReservationId = ""
                End If
            Next
            If SelCount = 0 Then
                btnChangeTable.Enabled = False
                btnChangeTime.Enabled = False
                btnCheckIn.Enabled = False
                cmdDelete.Enabled = False
            ElseIf SelCount = 1 Then
                btnChangeTable.Enabled = True
                btnChangeTime.Enabled = True
                btnCheckIn.Enabled = True
                cmdDelete.Enabled = True
            ElseIf SelCount > 1 Then
                btnChangeTable.Enabled = False
                btnChangeTime.Enabled = False
                btnCheckIn.Enabled = False
                cmdDelete.Enabled = True
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub frmNSearchReservation_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

End Class