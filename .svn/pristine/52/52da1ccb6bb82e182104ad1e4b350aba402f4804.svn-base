Imports SpectrumBL
Imports C1.Win.C1FlexGrid
Imports System.IO
Imports System.Text
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net

Public Class frmCreateReservation
    Dim objForm As New clsHotelReservation
    Dim _chkInDate As DateTime
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
    Private OFileDialog As OpenFileDialog
    Dim roomtype As String
    Dim bedNo As Integer
    Dim NoOfPeole As Integer
    Dim currentDetal As Integer
    Dim guest As String
    Dim Age As String
    Dim GstGender As String
    Dim Documentype As String
    Dim currenDetails As Integer
    Dim getColumnType As String = ""
    Protected controlList As New ArrayList
    Public filterImage As String = "All files (*.*)|*.*"
    Dim fileLocation As String
    Dim FullNameWithExtension As String
    Dim IsFileUpload As Boolean = False

    Private Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        grdReservation.Clear()
        If String.IsNullOrEmpty(cmdChkInDate.Value) Then

        End If
        checkOutdate = cmbCheckoutDate.Value
        roomtype = cmbRoomType.SelectedIndex
        bedNo = CmbBed.SelectedIndex
        NoOfPeole = txtNo.Text
        Call bindRegistration(checkIndate, checkOutdate, roomtype, bedNo, NoOfPeole)


    End Sub
    Private Sub bindRegistration(Checkin As DateTime, checkout As DateTime, Optional ByVal Roomtype As String = Nothing, Optional ByVal bedNo As String = "", Optional ByVal noOfPeople As String = "")
        Try
            Dim dtform = objForm.GetReservationListForms(Checkin, checkout, Roomtype, bedNo, noOfPeople)
            If dtform IsNot Nothing Then
                grdReservation.DataSource = dtform.DefaultView
                gridCoumnSetting()
            Else
                ShowMessage(getValueByKey(" Room not Available"), "Information - " & "Information")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub gridCoumnSetting()
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

    Public Function gridGuestDetailsSetting() As DataTable
        Try
            grdGuestDetails.Cols("Select").DataType = Type.GetType("System.Boolean")
            grdGuestDetails.Cols("Select").AllowEditing = True
            grdGuestDetails.Cols("Select").Caption = "Select"
            grdGuestDetails.Cols("Select").Width = 50
            grdGuestDetails.Cols("SrNo").Visible = True

            grdGuestDetails.Cols("Name").Width = 300
            grdGuestDetails.Cols("Name").DataType = Type.GetType("System.String")
            ' grdGuestDetails.Cols("Name").Format = "0"
            grdGuestDetails.Cols("Name").Name = "Name"
            grdGuestDetails.Cols("Name").TextAlign = TextAlignEnum.LeftCenter

            grdGuestDetails.Cols("Age").Width = 300
            grdGuestDetails.Cols("Age").DataType = Type.GetType("System.Numeric")
            grdGuestDetails.Cols("Age").Format = "0"
            grdGuestDetails.Cols("Age").Name = "Age"
            grdGuestDetails.Cols("Age").TextAlign = TextAlignEnum.LeftCenter

            grdGuestDetails.Cols("Gender").Width = 300
            grdGuestDetails.Cols("Gender").DataType = Type.GetType("System.String")
            'grdGuestDetails.Cols("Gender").Format = "0"
            grdGuestDetails.Cols("Gender").Name = "Gender"
            grdGuestDetails.Cols("Gender").TextAlign = TextAlignEnum.LeftCenter

            grdGuestDetails.Cols("DocumentType").Width = 300
            grdGuestDetails.Cols("DocumentType").DataType = Type.GetType("System.String")
            grdGuestDetails.Cols("DocumentType").Name = "DocumentType"
            grdGuestDetails.Cols("DocumentType").TextAlign = TextAlignEnum.LeftCenter

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function

    Enum DgGuestDetails


        Selects = 0
        SrNo
        Name
        Age
        'EAN
        'Packageduom
        Gender
        DocumentType

    End Enum
    Dim srno As Integer = 1
    Dim dtGuest As New DataTable
    Dim dsMemb As New DataSet

    Private Function ReservationDetailsData() As DataTable
        Try
            dtGuest = objForm.GetReservationTableStruc()
            dtGuest.Rows.Clear()
            Dim SelCount As Integer = 1
            For i = 1 To grdGuestDetails.Rows.Count - 1
                If grdGuestDetails.Rows(i)("SrNo") = True Then
                    SelCount = SelCount + 1
                End If
            Next
            'If Not dsMemb.Tables("Reservation") Is Nothing AndAlso dsMemb.Tables("Reservation").Rows.Count > 0 Then
            'For Each drMembDetails As DataRow In dsMemb.Tables("Reservation").Select()
            Dim drMemdetnew = dtGuest.NewRow()
            drMemdetnew("SrNo") = SelCount
            drMemdetnew("Name") = txtGuest.Text
            drMemdetnew("Age") = txtAge.Text
            drMemdetnew("Gender") = txtGender.Text
            drMemdetnew("DocumentType") = txtDocumentType.Text
            dtGuest.Rows.Add(drMemdetnew)
            '  Next
            ' End If
            Return dtGuest
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Function
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If ValidatedGuestDetails() Then
            'Dim SelCount As Integer = 1
            'For i = 1 To grdGuestDetails.Rows.Count - 1
            '    If grdGuestDetails.Rows(i)("SrNo") = True Then
            '        SelCount = SelCount + 1
            '    End If
            'Next
            dtGuest = ReservationDetailsData()
            ' dtGuest = gridGuestDetailsSetting()
            'grdGuestDetails.Rows(grdGuestDetails.Rows.Count - 1)(DgGuestDetails.Name) = dtGuest(0)("Name").ToString()
            ' dtGuest(0)("ArticleCode").ToString()
            grdGuestDetails.Rows.Add()
            For Each dr As DataRow In dtGuest.Rows
                grdGuestDetails.Rows(grdGuestDetails.Rows.Count - 1)(DgGuestDetails.Selects) = ""
                grdGuestDetails.Rows(grdGuestDetails.Rows.Count - 1)(DgGuestDetails.SrNo) = dr("SrNo")
                grdGuestDetails.Rows(grdGuestDetails.Rows.Count - 1)(DgGuestDetails.Name) = dr("Name")
                grdGuestDetails.Rows(grdGuestDetails.Rows.Count - 1)(DgGuestDetails.Age) = dr("Age")
                grdGuestDetails.Rows(grdGuestDetails.Rows.Count - 1)(DgGuestDetails.Gender) = dr("Gender")
                grdGuestDetails.Rows(grdGuestDetails.Rows.Count - 1)(DgGuestDetails.DocumentType) = dr("DocumentType")

                ' srno += 1
            Next

            btnClear_Click("", Nothing)
        End If
    End Sub

    Private Function ValidatedGuestDetails() As Boolean
        Try

            If txtGuest.Text.Trim() = "" Then
                'ShowMessage(getValueByKey("SOC008"), "SOC008 - " & getValueByKey("CLAE04"))
                txtGuest.Focus()
                Exit Function
            ElseIf txtAge.Text.Trim() = "" Then
                ' ShowMessage(getValueByKey("SOC009"), "SOC009 - " & getValueByKey("CLAE04"))
                txtAge.Focus()
                Exit Function
            ElseIf String.IsNullOrEmpty(txtGender.Text.Trim()) Then
                ' ShowMessage("Mobile Number is Mandatory", getValueByKey("CLAE04"))
                txtGender.Focus()
                Exit Function
            ElseIf String.IsNullOrEmpty(txtDocumentType.Text.Trim()) Then
                txtDocumentType.Focus()
                Exit Function
            End If
            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            SendMail(ex)
            Return False
        End Try
    End Function

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtGuest.Text = String.Empty
        txtAge.Text = ""
        txtGender.Text = String.Empty
        txtDocumentType.Text = String.Empty
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        If MsgBox(" If you cancel the reservation, you will lose unsaved data. Do you want to continue?", MsgBoxStyle.YesNo, "Information") = MsgBoxResult.Yes Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Else
            Me.DialogResult = Windows.Forms.DialogResult.None
        End If
    End Sub

    Public Function SendMail(ByVal e As Exception) As Boolean

        Dim smtpClient As New SmtpClient("smtp.myserver.com", 587) 'I tried using different hosts and ports
        smtpClient.UseDefaultCredentials = False
        smtpClient.Credentials = New NetworkCredential("Nikhil.Uplanchiwar@criti.in", "pass$123")
        smtpClient.EnableSsl = True 'Also tried setting this to false
        Dim result As Boolean = LogException(e)
        Dim mm As New MailMessage
        mm.From = New MailAddress("Nikhil.Uplanchiwar@criti.in")
        mm.Subject = "Test Mail"
        mm.IsBodyHtml = True
        mm.Body = result
        mm.To.Add("Prasad.Nikumbh@criti.in")

        Try
            smtpClient.Send(mm)
            MsgBox("SUCCESS!")
        Catch ex As Exception
            MsgBox(ex.InnerException.ToString)
        End Try

        mm.Dispose()
        smtpClient.Dispose()

        Return True

    End Function


    Private Sub btnUpload_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles btnUpload.LinkClicked
        OFileDialog = New OpenFileDialog
        OFileDialog.Filter = filterImage
        If (OFileDialog.ShowDialog() = DialogResult.OK) Then

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
            File.Copy(fileLocation, Path.Combine(Path.GetFileName(FullNameWithExtension)), True)
            'File.Copy(OFileDialog.FileName, dest)
            ' File.Copy(OFileDialog.FileName, filePath)
            IsFileUpload = True
        End If


    End Sub

    Private Sub NextBtn_Click(sender As Object, e As EventArgs) Handles NextBtn.Click
        Try
            If ValidatedGuestDetails() = False Then
                'ShowMessage("Please enter all mandatory fields to continue.", getValueByKey("CLAE04"))
                ShowMessage(getValueByKey("NC0001"), getValueByKey("CLAE04"))

                Exit Sub
            End If
            '
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdGuestDetails_CellButtonClick(sender As Object, e As RowColEventArgs) Handles grdGuestDetails.CellButtonClick
        Try
            If grdGuestDetails.Rows.Count > 1 Then
                grdGuestDetails.Select(grdGuestDetails.Rows.Count - 1, 2)
                grdGuestDetails.Rows.Remove(grdGuestDetails.Row)
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class