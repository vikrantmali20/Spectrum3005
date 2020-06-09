Imports System.Data
Imports SpectrumBL
Imports Spectrum.BL
Imports System.IO
Imports System.Data.SqlClient
Imports System.Collections


Public Class frmTableManagment
    Dim cls As New clsCommon
    Dim dtcol As New DataTable
    Dim dtPackagingBox As New DataTable
    Dim dtComboStatus As New DataTable
    Dim dtvalue As New DataTable
    Dim dtsearch As DataTable
    Dim dtlist As New DataTable
    Dim dtSiteSeatingAreaMapping As New DataTable

    Private Sub frmTableManagment_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            fillsites()
            dtcol = cls.GetTableStruc()
            dtSiteSeatingAreaMapping = cls.GetSiteSeatingAreaMappingTableStruc()
            dtSiteSeatingAreaMapping.Clear()
            grdTables.Visible = False

            btnSave.Visible = False
            btnClose.Visible = False
            combox2bind()
            fillArea()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ThemeChange()
            End If

            dtlist = cls.GetDineIN()
            showsitecode()
            Me.Size = New System.Drawing.Size((My.Computer.Screen.WorkingArea.Width - 10), (My.Computer.Screen.WorkingArea.Height - 10))
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Public Sub fillsites()
        Try
            Dim dt As DataTable = cls.GetSitedetail()
            cboComboBox2.ValueMember = "Id"
            cboComboBox2.DisplayMember = "SiteShortName"
            cboComboBox2.DataSource = dt
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub fillArea()
        Try
            Dim dt As DataTable = cls.GetSeatingArea()
            cboArea.ValueMember = "Id"
            cboArea.DisplayMember = "Name"
            dt.Rows.Add("-1", "All")
            cboArea.DataSource = dt
            'Dim row As DataRow
            'row = dt.NewRow()
            'dt.Rows.InsertAt(row, 0)
            cboArea.SelectedIndex = 4

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub bindComboStatus()
        Try
            dtComboStatus.Columns.Add("Id", System.Type.GetType("System.Int32"))
            dtComboStatus.Columns.Add("Value", System.Type.GetType("System.String"))
            dtComboStatus.Rows.Add("1", "Active")
            dtComboStatus.Rows.Add("0", "Deactive")
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub combox2bind()
        Try
            dtvalue.Columns.Add("Id", System.Type.GetType("System.Int32"))
            dtvalue.Columns.Add("Value", System.Type.GetType("System.String"))
            dtvalue.Rows.Add("-1", "All")
            dtvalue.Rows.Add("0", "Deactive")
            dtvalue.Rows.Add("1", "Active")
            cboStatus.ValueMember = "Id"
            cboStatus.DisplayMember = "Value"
            cboStatus.DataSource = dtvalue
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub gridsetting()
        Try

            dtPackagingBox = cls.GetSeatingArea()
            Dim PackagingMaterialList As String
            For index = 0 To dtPackagingBox.Rows.Count - 1
                PackagingMaterialList = PackagingMaterialList & dtPackagingBox(index)(1) & "|"
            Next index
            If PackagingMaterialList.Length > 0 Then
                PackagingMaterialList = PackagingMaterialList.Substring(0, PackagingMaterialList.Length - 1)
            End If
            grdTables.Cols("Seating Area").Width = 300
            grdTables.Cols("Seating Area").Caption = "SEATING AREA"
            grdTables.Cols("Seating Area").ComboList = PackagingMaterialList
            grdTables.Cols(1).Width = 20
            grdTables.Cols(1).ComboList = " "
            grdTables.Cols(1).ComboList = "..."
            grdTables.Cols(1).AllowEditing = True
            grdTables.Cols(1).Caption = ""
            grdTables.Cols(7).Width = 200

            bindComboStatus()
            Dim StatusList As String
            For index = 0 To dtComboStatus.Rows.Count - 1
                StatusList = StatusList & dtComboStatus(index)(1) & "|"
            Next
            If StatusList.Length > 0 Then
                StatusList = StatusList.Substring(0, StatusList.Length - 1)
            End If
            grdTables.Cols("STATUS").Width = 123
            ' grdTables.Cols("STATUS").Caption = "STATUS"
            '  grdTables.Cols("STATUS").AllowEditing = True


            grdTables.Cols("STATUS").ComboList = StatusList

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnADD_Click(sender As System.Object, e As System.EventArgs) Handles btnADD.Click
        Try
            grdTables.Visible = True
            Dim i As Int32
            Dim id As String = cls.GetTopDineInNumber()
            If grdTables.Rows.Count > 1 Then
                For i = grdTables.Rows.Count - 1 To 1 Step -1
                    If grdTables.Rows(i)("Name") <> "" Then
                        grdTables.Rows.Remove(i)
                        If grdTables.Rows.Count = 1 Then
                            Exit For
                        End If
                    End If
                Next
            End If

            '     grdTables.Cols("Id").AllowEditing = True
            showsitecode()
            '  gridsetting()

            ' Me.grdTables.CellButtonImage = CType(Resources.GetObject("dgDeliveryLocation.CellButtonImage"), System.Drawing.Image)
            'grdTables.Cols("DEL").Width = 20
            'grdTables.Cols("DEL").Caption = "DEL"
            'grdTables.Cols("DEL").ComboList = "..."
            grdTables.Cols(3).AllowEditing = True
            grdTables.Cols(4).AllowEditing = True
            grdTables.Cols("Seating Area").AllowEditing = True
            grdTables.Cols(6).AllowEditing = True
            grdTables.Cols("EDIT").Visible = False
            grdTables.Cols(1).Visible = True

            grdTables.Cols("STATUS").Caption = "STATUS"
            grdTables.Cols("STATUS").AllowEditing = True

            dtcol.Rows.Clear()
            Dim dr As DataRow
            dr = dtcol.NewRow()

            ' dr("Id") = id + 1
            dtcol.Rows.Add(dr)
            Dim a As Integer = dtcol.Rows.Count
            grdTables.Rows.Add(a)

            Dim z As Integer
            For z = 1 To grdTables.Rows.Count - 1
                If grdTables.Rows(z)("STATUS") = "" Then
                    grdTables.Item(z, "STATUS") = dtComboStatus.Rows(0)("Value")
                End If
            Next


            If grdTables.Rows.Count > 1 Then
                For i = 1 To grdTables.Rows.Count - 1
                    If grdTables.Rows.Count <> "2" Then
                        If (String.IsNullOrEmpty(grdTables.Rows(i)(3))) Then
                            If (String.IsNullOrEmpty(grdTables.Rows(i - 1)(3))) Then
                                ShowMessage("Table Id is Compulsory", "Information")
                                grdTables.Rows.Remove(i)
                            End If
                        End If
                    End If
                Next
            End If

            btnClose.Visible = True
            btnSave.Visible = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        If MessageBox.Show("Your will loose unsaved data, Do you wish to continue?", "Information", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Public Function showsitecode()
        Try
            'existing code========
            ''grdTables.Visible = True
            ''Dim dr = dtcol.NewRow
            ''dtcol.Rows.Add(dr)
            '====================
            If dtlist.Rows.Count > 0 Then
                grdTables.Visible = True
                '============================================
                Dim i As Int32
                '  Dim id As String = cls.GetTopDineInNumber()
                If grdTables.Rows.Count > 1 Then
                    For i = grdTables.Rows.Count - 1 To 1 Step -1
                        If grdTables.Rows(i)("Name") = "" Then
                            grdTables.Rows.Remove(i)
                            If grdTables.Rows.Count = 1 Then
                                Exit For
                            End If
                        End If
                    Next
                End If
                '============================================
                '  Dim dtlist As New DataTable
                '  dtlist = cls.GetDineIN()
                gridsetting()
                ' grdTables.Cols("EDIT").Visible = False
                grdTables.Cols(3).AllowEditing = False
                grdTables.Cols(4).AllowEditing = False
                grdTables.Cols("Seating Area").AllowEditing = False
                grdTables.Cols(6).AllowEditing = False

                grdTables.Cols(2).Visible = False
                grdTables.Cols(1).Visible = False


                grdTables.Cols("STATUS").Caption = "STATUS"
                grdTables.Cols("STATUS").AllowEditing = False

                dtlist = cls.GetDineIN()

                Dim dr As DataRow
                dr = dtlist.NewRow
                dtlist.Rows.Add(dr)

                Dim a As Integer = dtlist.Rows.Count
                grdTables.Rows.Add(a)

                Dim j As Integer = 1
                For Each dr In dtlist.Rows
                    grdTables.Rows(j)(3) = dr("Id")
                    grdTables.Rows(j)(4) = dr("Name")
                    grdTables.Rows(j)(5) = dr("Seating Area")
                    grdTables.Rows(j)(6) = dr("Capacity")
                    grdTables.Rows(j)(7) = dr("Status")
                    j = j + 1
                Next

                Dim xx As Int32
                If grdTables.Rows.Count > 0 Then
                    For xx = grdTables.Rows.Count - 1 To 0 Step -1
                        If grdTables.Rows(xx)("Id") = "" Then
                            grdTables.Rows.Remove(xx)
                        End If
                    Next
                End If

                grdTables.Visible = True
            End If
            ' grdTables.DataSource = dtlist
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Sub txtSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            If TextBoxValidated() Then
                Dim iid As Int32
                If grdTables.Rows.Count > 0 Then
                    For iid = grdTables.Rows.Count - 1 To 1 Step -1
                        If Not (String.IsNullOrEmpty(grdTables.Rows(iid)("Id"))) Then
                            grdTables.Rows.Remove(iid)
                        End If
                    Next
                End If
                grdTables.Cols("Id").AllowEditing = False
                ' grdTables.Rows(0)(1) = False

                Dim status As String
                Dim status1 As String
                Dim area As String
                Dim id As String = txtId.Text
                Dim name As String = txtName.Text
                If cboArea.Text <> "" Then
                    area = cboArea.SelectedValue
                Else
                    area = ""
                End If
                Dim capacity As String = txtCapacity.Text
                status = cboStatus.SelectedValue

                dtsearch = cls.GetSearchDineIn(id, name, area, capacity, status)

                gridsetting()

                grdTables.Cols(2).Visible = True
                grdTables.Cols(1).Visible = False
                '======================================== grdTables.Cols("ID").AllowEditing = False
                disableCheckbox()
                Dim x As Integer
                For x = 1 To grdTables.Rows.Count - 1
                    grdTables.Rows(x)(2) = False
                    '  grdTables.Rows(x).AllowEditing = True
                Next
                'For x = 1 To grdTables.Rows.Count - 1
                '    grdTables.Rows(x).AllowEditing = True
                'Next
                '  grdTables.Cols(1).Clear(1)
                '==================================================================================

                Dim dr As DataRow
                dr = dtsearch.NewRow
                dtsearch.Rows.Add(dr)

                Dim a As Integer = dtsearch.Rows.Count
                grdTables.Rows.Add(a)
                ' grdTables.DataSource = dtsearch

                Dim i As Integer = 1
                For Each dr In dtsearch.Rows
                    grdTables.Rows(i)(3) = dr("Id")
                    grdTables.Rows(i)(4) = dr("Name")
                    grdTables.Rows(i)(5) = dr("Seating Area")
                    grdTables.Rows(i)(6) = dr("Capacity")
                    grdTables.Rows(i)(7) = dr("Status")
                    i = i + 1
                Next

                Dim ii As Int32
                If grdTables.Rows.Count > 0 Then
                    For ii = grdTables.Rows.Count - 1 To 0 Step -1
                        If (String.IsNullOrEmpty(grdTables.Rows(ii)("Id"))) Then
                            grdTables.Rows.Remove(ii)
                        End If
                    Next
                End If

                grdTables.Visible = True
                clear()
                btnClose.Visible = True
                btnSave.Visible = True
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub clear()
        txtId.Text = String.Empty
        txtName.Text = String.Empty
        cboArea.SelectedItem = -1
        txtCapacity.Text = String.Empty
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Try
            Dim seatingarea As String
            Dim index As Integer
            Dim idd As Int64
            Dim action As Boolean
            Dim area As String
            Dim param As String
            Dim Status As String
            Dim id As String
            dtSiteSeatingAreaMapping.Clear()
            For index = 1 To grdTables.Rows.Count - 1
                If (String.IsNullOrEmpty(grdTables.Rows(index)("Id"))) Then
                    ShowMessage("Please Enter Table Id", "Information")
                    grdTables.Cols(3).AllowEditing = True
                    Exit Sub
                End If
                If grdTables.Rows(index)("Name") = "" Then
                    ShowMessage("Please Enter  Table Name", "Information")
                    grdTables.Cols(4).AllowEditing = True
                    Exit Sub
                End If
                'If grdTables.Rows(index)("Seating Area") = "" Then
                '    ShowMessage("Please Enter  Seating Area", "Information")
                '    grdTables.Cols(5).AllowEditing = True
                '    Exit Sub
                'End If
                If grdTables.Rows(index)("Seating Area") <> "" Then
                    If (String.IsNullOrEmpty(grdTables.Rows(index)("Capacity"))) Then
                        ShowMessage("Please Enter  Capacity", "Information")
                        grdTables.Cols(6).AllowEditing = True
                        Exit Sub
                    End If
                End If
                If Not (String.IsNullOrEmpty(grdTables.Rows(index)("Capacity"))) Then
                    If grdTables.Rows.Count - 1 = index Then
                        If grdTables.Rows(index)("Seating Area") = "" Then
                            Dim ik As Integer = grdTables.Rows(index)("Capacity")
                            If grdTables.Cols(2).Visible = False Then
                                If Not ik = "0" Then
                                    ShowMessage("Please Enter  Seating Area", "Information")
                                    grdTables.Cols(5).AllowEditing = True
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                End If

                If grdTables.Rows(index)("Status") = "" Then
                    ShowMessage("Please Enter  Status", "Information")
                    grdTables.Cols(7).AllowEditing = True
                    Exit Sub
                End If
                Dim sitecode As String = clsAdmin.SiteCode
                id = grdTables.Rows(index)("Id").ToString
                If id = 0 Then
                    ShowMessage("Tabel No. " + id + " Can't be Created.", "Information")
                    Exit Sub
                End If
                Dim name As String = grdTables.Rows(index)("Name").ToString
                If grdTables.Rows(index)("Seating Area") <> "" Then
                    area = ""
                    area = grdTables.Rows(index)("Seating Area").ToString()
                Else
                    area = ""
                    area = "0"
                    idd = "0"
                End If
                Dim createdby As String = clsAdmin.UserCode
                Dim i As Integer
                For i = 0 To dtPackagingBox.Rows.Count - 1
                    If area = dtPackagingBox.Rows(i)("Name") Then
                        idd = dtPackagingBox.Rows(i)("id")
                    End If
                Next
                seatingarea = idd

                Dim capacity As String
                If Not (String.IsNullOrEmpty(grdTables.Rows(index)("Capacity"))) Then
                    capacity = grdTables.Rows(index)("Capacity").ToString
                Else
                    capacity = ""
                End If
                param = grdTables.Rows(index)("Status").ToString
                If cls.DineInIdExist(id) Then
                    action = True
                Else
                    action = False
                End If
                If param = "Active" Then
                    Status = 1
                End If
                If param = "Deactive" Then
                    Status = 0
                End If
                If action = True Then
                    If grdTables.Rows(index)(2) = True Then
                        If Not (String.IsNullOrEmpty(grdTables.Rows(index)("Capacity"))) Then
                            If grdTables.Rows(index)("Seating Area") = "" Then
                                Dim ik As Integer = grdTables.Rows(index)("Capacity")
                                If Not ik = "0" Then
                                    ShowMessage("Please Enter  Seating Area", "Information")
                                    grdTables.Cols(5).AllowEditing = True
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                End If
                If action = True Then
                    If grdTables.Rows(index)(2) = True Then
                        If grdTables.Rows(index)("Seating Area") <> "" Then
                            If Not (String.IsNullOrEmpty(grdTables.Rows(index)("Capacity"))) Then
                                Dim jk As Integer = grdTables.Rows(index)("Capacity")
                                If jk = "0" Then
                                    ShowMessage("Please Enter  Capacity", "Information")
                                    grdTables.Cols(6).AllowEditing = True
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                End If

                If Not String.IsNullOrEmpty(id) Then
                    If action Then
                        Dim indx As Integer
                        For indx = 1 To grdTables.Rows.Count - 1
                            If grdTables.Rows(indx)(2) = True Then
                                cls.updateDineIn(sitecode, id, name, seatingarea, capacity, Status, createdby, action)

                            End If
                        Next
                    Else
                        cls.updateDineIn(sitecode, id, name, seatingarea, capacity, Status, createdby, action)
                    End If

                End If

                'added by khusrao adil on 21-08-2018 for Seating area mapping
                If seatingarea <> "" AndAlso seatingarea <> 0 Then
                    Dim _result As DataRow() = dtSiteSeatingAreaMapping.Select("SiteCode='" + sitecode + "' and  SeatingAreaId='" + seatingarea + "' ")
                    If _result.Length > 0 Then
                        _result(0)("SiteCode") = sitecode
                        _result(0)("SeatingAreaId") = seatingarea
                        Dim _status = Convert.ToInt64(Status)
                        _result(0)("Status") = Convert.ToBoolean(_status)
                        _result(0)("UPDATEDAT") = clsAdmin.SiteCode
                        _result(0)("UPDATEDBY") = clsAdmin.UserCode
                    Else
                        Dim drRowSiteSeatingAreaMapping As DataRow
                        drRowSiteSeatingAreaMapping = dtSiteSeatingAreaMapping.NewRow()
                        drRowSiteSeatingAreaMapping("SiteCode") = sitecode
                        drRowSiteSeatingAreaMapping("SeatingAreaId") = seatingarea
                        drRowSiteSeatingAreaMapping("CREATEDAT") = clsAdmin.SiteCode
                        drRowSiteSeatingAreaMapping("CREATEDBY") = clsAdmin.UserCode
                        drRowSiteSeatingAreaMapping("UPDATEDAT") = clsAdmin.SiteCode
                        drRowSiteSeatingAreaMapping("UPDATEDBY") = clsAdmin.UserCode
                        Dim _status = Convert.ToInt64(Status)
                        drRowSiteSeatingAreaMapping("Status") = Convert.ToBoolean(_status)
                        dtSiteSeatingAreaMapping.Rows.Add(drRowSiteSeatingAreaMapping)
                    End If
                    dtSiteSeatingAreaMapping.AcceptChanges()
                End If
            Next
            Dim ii As Int32
            If grdTables.Rows.Count > 0 Then
                For ii = grdTables.Rows.Count - 1 To 1 Step -1
                    If (String.IsNullOrEmpty(grdTables.Rows(ii)("Id"))) Then
                        grdTables.Rows.Remove(ii)
                    End If
                Next
            End If
            'added by khusrao adil on 21-08-2018 for Seating area mapping
            If dtSiteSeatingAreaMapping.Rows.Count > 0 Then
                If cls.SaveSiteSeatingAreaMapping(dtSiteSeatingAreaMapping) Then
                Else
                    Dim ax As New ApplicationException("Site Seating Area not map")
                    LogException(ax)
                End If

            End If
            grdTables.Cols(2).Visible = False
            If action Then
                ShowMessage("Record Updated Successfully", "Information")
            Else
                ShowMessage("Tabel No. " + id + " Created Successfully.", "Information")
            End If
            frmTableManagment_Load(sender, e)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub disableCheckbox()
        grdTables.Cols(3).AllowEditing = False
        grdTables.Cols(4).AllowEditing = False
        grdTables.Cols(5).AllowEditing = False
        grdTables.Cols(6).AllowEditing = False
        grdTables.Cols(7).AllowEditing = False
    End Sub

    Private Sub grdTables_CellChecked(sender As System.Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdTables.CellChecked
        Try
            If grdTables.Rows.Count > 0 Then
                Dim i As Integer
                For i = 1 To grdTables.Rows.Count - 1
                    If grdTables.Rows(i)(2) = True Then

                        grdTables.Cols(3).AllowEditing = True
                        grdTables.Cols(4).AllowEditing = True
                        grdTables.Cols(5).AllowEditing = True
                        grdTables.Cols(6).AllowEditing = True
                        grdTables.Cols(7).AllowEditing = True
                        ' ''If grdTables.Rows.Count > 0 Then
                        ' ''    If grdTables.Rows(e.Row)(1) = True Then
                        ' ''        grdTables.Rows(e.Row).AllowEditing = True

                        ' ''        grdTables.Focus()
                        ' ''        gridsetting()
                        ' ''    End If
                        ' ''End If
                        Exit Sub
                    Else
                        disableCheckbox()
                    End If
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub ThemeChange()
        Me.BackColor = Color.FromArgb(134, 134, 134)

        'Label
        'Label5.BackColor = Color.FromArgb(212, 212, 212)
        'Label1.BackColor = Color.FromArgb(212, 212, 212)
        'Label2.BackColor = Color.FromArgb(212, 212, 212)
        'Label3.BackColor = Color.FromArgb(212, 212, 212)
        'Label4.BackColor = Color.FromArgb(212, 212, 212)


        'Button1
        ' btnADD.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnADD.BackColor = Color.Transparent
        btnADD.BackColor = Color.FromArgb(0, 107, 163)
        btnADD.ForeColor = Color.FromArgb(255, 255, 255)
        btnADD.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        '  btnADDn1.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnADD.FlatAppearance.BorderSize = 0
        btnADD.FlatStyle = System.Windows.Forms.FlatStyle.Flat

        'Button2
        '  btnSearch.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnSearch.BackColor = Color.Transparent
        btnSearch.BackColor = Color.FromArgb(0, 107, 163)
        btnSearch.ForeColor = Color.FromArgb(255, 255, 255)
        btnSearch.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        '  btnSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSearch.FlatAppearance.BorderSize = 0
        btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat

        'Button4
        ' btnClose.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnClose.BackColor = Color.Transparent
        btnClose.BackColor = Color.FromArgb(0, 107, 163)
        btnClose.ForeColor = Color.FromArgb(255, 255, 255)
        btnClose.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        '   btnClose.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat

        'Button3
        '  btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.System
        btnSave.BackColor = Color.Transparent
        btnSave.BackColor = Color.FromArgb(0, 107, 163)
        btnSave.ForeColor = Color.FromArgb(255, 255, 255)
        btnSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        '  btnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat

        'grdTables
        grdTables.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        grdTables.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        grdTables.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        grdTables.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        grdTables.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdTables.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdTables.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdTables.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdTables.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdTables.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
        grdTables.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        grdTables.BackColor = Color.White




        'Me.grdTables.MaximumSize = New Size(1364, 600)
        'Me.grdTables.Size = New System.Drawing.Size(1364, 600)
        'Me.grdTables.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        'Me.grdTables.Styles.Highlight.BackColor = Color.FromArgb(153, 255, 255)
        'Me.grdTables.Styles.Highlight.ForeColor = Color.Black
        'Me.grdTables.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        'Me.grdTables.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        'Me.grdTables.Rows.MinSize = 26
        'Me.grdTables.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        'Me.grdTables.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        'Me.grdTables.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        'Me.grdTables.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        'Me.grdTables.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        'Me.grdTables.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        'Me.grdTables.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        'Me.grdTables.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        'Me.grdTables.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        'Me.grdTables.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        'Me.grdTables.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)


    End Sub

    Public Function TextBoxValidated() As Boolean
        Try
            If txtId.Text <> "" Then
                Return True
            ElseIf txtName.Text <> "" Then
                Return True
            ElseIf cboArea.Text <> "" Then
                Return True
            ElseIf txtCapacity.Text <> "" Then
                Return True
            ElseIf cboStatus.Text <> "" Then
                Return True
            Else
                ShowMessage("Please Input Value To Search", "Information")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Sub txtId_Leave(sender As System.Object, e As System.EventArgs)
        If Not IsNumeric(txtId.Text) Then
            ShowMessage("Id Must be Number.", "Information")
            txtId.Text = ""
            txtId.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub txtCapacity_Leave(sender As System.Object, e As System.EventArgs)
        If Not IsNumeric(txtCapacity.Text) Then
            ShowMessage("Capacity Must be Number.", "Information")
            txtCapacity.Text = ""
            txtCapacity.Focus()
            Exit Sub
        End If
    End Sub

    'Private Sub txtArea_Leave(sender As System.Object, e As System.EventArgs) Handles txtArea.Leave
    '    If Not IsNumeric(txtArea.Text) Then
    '        ShowMessage("Capacity  Must be Number.", "Information")
    '        txtArea.Text = ""
    '        txtArea.Focus()
    '        Exit Sub
    '    End If
    'End Sub

    Private Sub grdTables_AfterEdit(sender As System.Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdTables.AfterEdit
        Try
            Dim id As Integer = e.Col
            Dim CurrentRow As Integer = e.Row
            Dim CurrentId As Integer = grdTables.Rows(CurrentRow).Item(3)
            If CurrentRow = grdTables.Rows.Count - 1 Then
                If cls.DineInIdExist(CurrentId) Then
                    If grdTables.Cols("EDIT").Visible = False Then
                        ShowMessage("This Table Id is already exist.", "Information")
                        btnADD_Click(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub grdTables_CellButtonClick(sender As System.Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdTables.CellButtonClick
        Try
            Dim id As Integer = e.Col
            Dim CurrentRow As Integer = e.Row
            Dim CurrentId As Integer = grdTables.Item(grdTables.Row, "Id")
            Dim status As String = grdTables.Item(grdTables.Row, "status")
            If cls.DineInIdExist(CurrentId) Then
                If cls.updateDineInStatus(CurrentId, 0) Then
                    If status <> "Deactive" Then
                        ShowMessage("Table No." + CurrentId.ToString + " is De-activated Successfully.", "Information")
                        showsitecode()
                        Exit Sub
                    Else
                        ShowMessage("Table No." + CurrentId.ToString + " is already De-active.", "Information")
                        Exit Sub
                    End If
                End If
            Else
                grdTables.Rows.Remove(CurrentRow)
                btnSave.Visible = False
                btnClose.Visible = False
                grdTables.Cols(1).Visible = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Sub txtId_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtId.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If txtId.Text <> "" Then
                    txtSearch_Click(sender, e)
                    txtId.Text = ""
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtCapacity_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtCapacity.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If txtCapacity.Text <> "" Then
                    txtSearch_Click(sender, e)
                    txtCapacity.Text = ""
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtName_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If txtName.Text <> "" Then
                    txtSearch_Click(sender, e)
                    txtName.Text = ""
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtId_Click(sender As System.Object, e As System.EventArgs)
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub

    Private Sub txtName_Click(sender As System.Object, e As System.EventArgs)
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub

    Private Sub txtCapacity_Click(sender As System.Object, e As System.EventArgs)
        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
            OnTouchKeyBoard()
        End If
    End Sub

    Public Function OnTouchKeyBoard()
        Try
            Dim procc = Process.GetProcessesByName("osk")
            If procc.Length = 0 Then
                Process.Start("osk")
            Else
                For Each proccShow As Process In Process.GetProcessesByName("osk")
                    If proccShow.ProcessName = "osk" Then
                        proccShow.Kill()
                        Process.Start("osk")
                    End If
                Next

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function


End Class