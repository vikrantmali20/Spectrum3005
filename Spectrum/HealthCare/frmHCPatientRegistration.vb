Imports SpectrumBL
Imports System.IO
Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class frmHCPatientRegistration

#Region "Declare Varables"
    Dim objClsCommon As New clsCommon
    Dim objCustm As New clsSOCustomer
    Dim objDrInfo As New clsHcDoctorInfo

    Private clsPatient As New clsHcPatientInfo
    Private clsComn As New clsHcCommon
    Private dsPatientRegn, dsRefDoc As DataSet
    Dim dtLCountry, dtLState, dtLCity, dtCustdata As New DataTable
    Dim dtPCountry, dtPState, dtPCity As New DataTable

    Private RefDoctorCode As String = String.Empty
    Private dtPatient, dtAuthor, dtTreeView As DataTable
    Private SqldtDistinct As DataTable

    Private AddNodeM, AddNodeP, AddNodeC As TreeNode

    Private vAuthorCode As String = String.Empty
    Public IsNewRegn As Boolean = False
    Public SearchedValue As String = String.Empty
    Private vDocNextId As Integer = 1001
    Private Const vRegnNextId As Integer = 2001

    Private fileExtention As String = String.Empty
    Private fileDocName As String = String.Empty
    Private vDocPathInServer As String = String.Empty

    Private OFileDialog As OpenFileDialog
    Private AddNodeMain As New TreeNode
    Private AddNodeDocs As New TreeNode
    Private ImagePatient As Image
    Private PfindKey(1) As Object
    'Private AfindKey(1) As Object
    Private AfindKey(2) As Object
    Private PopupClose As Boolean = False
    ' Public filterImage As String = "All files (*.*)|*.*"
    Public filterImage As String = "PNG file (*.png)|*.png|JPEG file (*.jpg)|*.jpg|Bitmap file (*.bmp)|*.bmp|TIFF file (*.tif)|*.tif|GIF file (*.gif)|*.gif"
#End Region

#Region "Properties"

    Dim _patientDOB As DateTime
    Public Property patientDOB As DateTime
        Get
            Return _patientDOB
        End Get
        Set(ByVal value As DateTime)
            _patientDOB = value
        End Set
    End Property
    Private PatientIdTp As String = String.Empty
    Private _patientId As String = String.Empty
    Public Property PatientId() As String
        Get
            Return _patientId
        End Get
        Set(ByVal value As String)
            _patientId = value
        End Set
    End Property
    Private _cardno As String = String.Empty
    Public Property cardno() As String
        Get
            Return _cardno
        End Get
        Set(ByVal value As String)
            _cardno = value
        End Set
    End Property

    Private _ClpProgId As String = String.Empty
    Public Property ClpProgId() As String
        Get
            Return _ClpProgId
        End Get
        Set(ByVal value As String)
            _ClpProgId = value
        End Set
    End Property

    Private vSiteCode As String = clsAdmin.SiteCode
    Private vUserName As String = clsAdmin.UserName
    Private dsCombo As DataSet
    Dim uniquemblno As String = ""
#End Region

#Region "Events"

    Private Sub frmPatientRegistration_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddNodeMain = New TreeNode
        AddNodeMain.Name = "MainNode"
        AddNodeMain.Text = "Patient documents"
        TreeViewDocs.Nodes.Add(AddNodeMain)

        'vDocPathInServer = GetPatientDocsPath()
        vDocPathInServer = objClsCommon.GetPatientDocsPath()
        FillComboData()
        Dim tempPateintId As String = PatientId

        dsPatientRegn = New DataSet
        dtCustdata = New DataTable
        dsPatientRegn = clsPatient.GetPatientInfo(PatientId, vSiteCode, False)
        dtCustdata = clsPatient.GetPatientInfoSchema(PatientId, vSiteCode)

        If Not (String.IsNullOrEmpty(PatientId)) Then
            RefreshPatientInfo(dsPatientRegn, PatientId, dtCustdata)
            EditableFields(Me, True)
        End If

        If (IsNewRegn) Then
            PatientId = tempPateintId
            GetNextPatientID()
        Else
            BtnNewRegn.Text = "Cancel"
        End If
        BtnNewRegn_Click(Nothing, Nothing)
        If (IsNewRegn) Then
            dtpDob.Value = Now.Date
        Else
            dtpDob.Value = patientDOB
        End If
        dtpDob.Value = Now.Date
        TabPageAddressInfo.Show()
        cmbRelation.SelectedIndex = 0
        dtpDocDate.Value = Now.Date
        cmbSalutation.TopIndex = 0
        cmbSalutation.Focus()
        GroupBoxPatientDetails.Select()
        'PPPP --------------
        'Khusrao added
        If SearchedValue <> Nothing Then
            If IsNumeric(SearchedValue.ToString()) Then
                If SearchedValue.Length > 10 Then
                    txtMobile.Value = SearchedValue.Substring(0, 10)
                Else
                    txtMobile.Value = SearchedValue
                End If
            End If
            TabPagePatientInfo.Show()
        End If
        BtnSearchPatient.Visible = False
        '#PPPP --------------
    End Sub

    Private Sub chkCopyAddress_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCopyAddress.CheckedChanged
        Try
            If chkCopyAddress.CheckState = CheckState.Checked Then
                txtPAddressLn1.Text = txtLAddressLn1.Text
                txtPAddressLn2.Text = txtLAddressLn2.Text
                cmbPCountry.SelectedValue = IIf(cmbLCountry.SelectedValue = Nothing, -1, cmbLCountry.SelectedValue)

                If (cmbPState.DataSource IsNot Nothing) Then
                    cmbPState.SelectedValue = IIf(cmbLState.SelectedValue = Nothing, -1, cmbLState.SelectedValue)
                End If
                If (cmbPCity.DataSource IsNot Nothing) Then
                    cmbPCity.SelectedValue = IIf(cmbLCity.SelectedValue = Nothing, -1, cmbLCity.SelectedValue)
                End If
                txtPPincode.Text = txtLPincode.Text

                cmbPStayYears.SelectedIndex = IIf(cmbLStayYears.SelectedIndex = 0, 0, cmbLStayYears.SelectedIndex)
                cmbPStayMonths.SelectedIndex = IIf(cmbLStayMonths.SelectedIndex = 0, 0, cmbLStayMonths.SelectedIndex)
            Else

                txtPAddressLn1.Text = String.Empty
                txtPAddressLn2.Text = String.Empty
                cmbPCountry.SelectedValue = String.Empty
                cmbPState.SelectedValue = String.Empty
                If (cmbPCity.DataSource IsNot Nothing) Then
                    cmbPCity.SelectedValue = IIf(cmbLCity.SelectedValue = Nothing, -1, String.Empty)
                End If
                ' cmbPCity.SelectedValue = String.Empty
                txtPPincode.Text = ""

                cmbPStayYears.SelectedIndex = 0
                cmbPStayMonths.SelectedIndex = 0

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cmbLCountry_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLCountry.SelectedValueChanged
        fnSetComboAfterSel(dtLState, cmbLState, "ParentCode = '" & cmbLCountry.SelectedValue & "'")
        cmbLCity.DataSource = Nothing
        cmbLCity.Text = String.Empty
        cmbLState.Text = String.Empty

    End Sub
    Private Sub cmbPCountry_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPCountry.SelectedValueChanged
        fnSetComboAfterSel(dtPState, cmbPState, "ParentCode = '" & cmbPCountry.SelectedValue & "'")
        cmbPCity.DataSource = Nothing
        cmbPCity.Text = String.Empty
        cmbPState.Text = String.Empty
    End Sub

    Private Sub cmbLState_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLState.SelectedValueChanged
        fnSetComboAfterSel(dtLCity, cmbLCity, "ParentCode = '" & cmbLState.SelectedValue & "'")
    End Sub
    Private Sub cmbpState_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPState.SelectedValueChanged
        fnSetComboAfterSel(dtPCity, cmbPCity, "ParentCode = '" & cmbPState.SelectedValue & "'")
    End Sub



    Private Sub dtpDob_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDob.ValueChanged

        If (dtpDob.Value IsNot DBNull.Value) Then
            Dim dt As Date
            dt = dtpDob.Value

            If dt.Date <= Now.Date Then
                Dim AgeCal As New clsAgeCalculator(dtpDob.Value, Now)
                cmbAgeYears.Text = AgeCal.Years
                cmbAgeMonths.Text = AgeCal.Months

            Else
                ShowMessage("Date of birth can not be future date", "Date of Birth")
                dtpDob.Value = Now.Date

                cmbAgeYears.Text = String.Empty
                cmbAgeMonths.Text = String.Empty
            End If
        End If
    End Sub

    Private Sub GetNextPatientID()

        Dim docNo As String = objClsCommon.GetNextDocumentNo("Customer Loyalty", clsAdmin.SiteCode)
        Dim otherCharacters = "CLS" & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Length - 3, 3)

        'String.Concat(otherCharacters,docNo.PadLeft(15-otherCharacters.Length,"0"))	"CLSHM1000000008"
        cardno = objClsCommon.GenDocNo(otherCharacters, 15, docNo)
        txtPatientID.Value = cardno

        ClpProgId = objClsCommon.GetCLPProgramId(clsAdmin.SiteCode)



        ' Dim NextPatientID As Integer = clsComn.GetNextDocNo("Patent Registration")
        'txtPatientID.Value = NextPatientID.ToString("000") & "-" & "KOC-" & Now.Year.ToString.Substring(2, 2)
    End Sub

#Region "Search Patient Information"
    Private Sub BtnSearchPatient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchPatient.Click
        Try
            ClearFields(Me)
            dsPatientRegn = New DataSet
            dsPatientRegn = clsPatient.GetPatientInfo(String.Empty, clsAdmin.SiteCode, True)

            'PPPP -----------------------
            ' Khusrao added
            Dim dtPatientSearch As New DataTable
            dtPatientSearch = New DataTable
            dtPatientSearch = clsPatient.GetPatientInformation(clsAdmin.SiteCode, clsAdmin.CLPProgram)
            '#PPPP -----------------------

            ' commented by khusrao adil
            Dim objPatient As New frmHcCommonView
            ''objPatient.SetDataTable = dsPatientRegn.Tables("HcPatientDetail")
            objPatient.SetDataTable = dtPatientSearch
            objPatient.SetCaptionText = "Patient Information"
            objPatient.DisplayColumns = "PatientId,MobileNo, FirstName, SurName, AddressLine1, AddressLine2, City, Pincode" 'MobileNo, added

            If (objPatient.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                Dim drPatient As DataRow = objPatient.GetResultRow
                PatientId = drPatient("PatientId")

                EditableFields(Me, True)

                txtPatientID.Value = PatientId
                GetPatientDetails(PatientId)

                IsNewRegn = False
                BtnNewRegn.Text = "New"
                BtnEditRegn.Text = "Edit"
            End If

        Catch ex As Exception
            MessageBox.Show("GetPatientDetails:" & vbCrLf & ex.Message)
        End Try
    End Sub
    Private Sub txtPatientID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPatientID.KeyDown
        If (e.KeyCode = Keys.Enter AndAlso txtPatientID.Text.Trim.Length > 0) Then
            GetPatientDetails(txtPatientID.Text.Trim)
        End If
    End Sub
    Public Sub GetPatientDetails(ByVal vPatientID As String)
        Try
            dsPatientRegn = New DataSet
            dsPatientRegn = clsPatient.GetPatientInfo(vPatientID, clsAdmin.SiteCode, False)

            RefreshPatientInfo(dsPatientRegn, vPatientID)

        Catch ex As Exception
            MessageBox.Show("GetPatientDetails:" & vbCrLf & ex.Message)
        End Try
    End Sub
#End Region
#Region "Patient Registration Button Event"
    Private Sub BtnNewRegn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNewRegn.Click
        If (IsNewRegn = False) Then
            ReadOnlyDocsInfo(True)
            'End If
        ElseIf (IsNewRegn = False) Then
                ReadOnlyDocsInfo(True)
                BtnUploadImage.Enabled = False
                BtnClearImage.Enabled = False
            ElseIf (BtnNewRegn.Text = "New") Then
                IsNewRegn = True
                EditableFields(Me, False)
                ClearFields(Me)
                ReadOnlyDocsInfo(False)
                ClearDocsInfo()

                IsNewRegn = True
                txtRefDoctorName.ReadOnly = True
                BtnEditRegn.Text = "Save"

                BtnNewRegn.Text = "Cancel"

                BtnSearchPatient.Enabled = False
                BtnDeleteRegn.Enabled = False
                BtnEditRegn.Visible = True

                TreeViewDocs.Nodes.Clear()
                AddNodeMain = New TreeNode
                AddNodeMain.Name = "MainNode"
                AddNodeMain.Text = "Patient documents"
                TreeViewDocs.Nodes.Add(AddNodeMain)

                For Each dtPatient As DataTable In dsPatientRegn.Tables
                    If (dtPatient.Rows.Count > 0) Then
                        dtPatient.Rows.Clear()
                    End If
                Next
            If dtCustdata IsNot Nothing AndAlso dtCustdata.Rows.Count > 0 Then
                dtCustdata.Rows.Clear()
            End If
                BtnNewDoc_Click(sender, New EventArgs())
                BtnAddDoc.Visible = True
                BtnDeleteDoc.Visible = True
                BtnNewDoc.Visible = True

                BtnUploadImage.Enabled = True
                BtnClearImage.Enabled = True
                BtnSearchRefDr.Enabled = True
                BtnNewRefDr.Enabled = True
                BtnSearchAuthor.Enabled = True
                BtnUploadDoc.Enabled = True

            ElseIf (BtnNewRegn.Text = "Cancel") Then
                EditableFields(Me, True)
            RefreshPatientInfo(dsPatientRegn, PatientId, dtCustdata)

                BtnDeleteRegn.Enabled = True
                'BtnSearchPatient.Enabled = True ' added bu khusrao 
                BtnEditRegn.Text = "Edit"
                BtnNewRegn.Text = "New"

                BtnUploadImage.Enabled = False
                BtnClearImage.Enabled = False
                BtnSearchRefDr.Enabled = False
                BtnNewRefDr.Enabled = False
                BtnSearchAuthor.Enabled = False
                BtnUploadDoc.Enabled = False

                BtnNewDoc_Click(sender, New EventArgs())
                BtnAddDoc.Visible = False
                BtnDeleteDoc.Visible = False
                BtnNewDoc.Visible = False
            End If
    End Sub
    Private Sub BtnEditRegn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEditRegn.Click
        Try
            If (BtnEditRegn.Text = "Edit") Then
                EditableFields(Me, False)

                BtnNewRegn.Text = "Cancel"
                BtnEditRegn.Text = "Save"
                IsNewRegn = False
                PatientId = txtPatientID.Text.Trim

                BtnAddDoc.Visible = True
                BtnDeleteDoc.Visible = True
                BtnNewDoc.Visible = True

                BtnUploadImage.Enabled = True
                BtnClearImage.Enabled = True
                BtnSearchRefDr.Enabled = True
                BtnNewRefDr.Enabled = True
                BtnSearchAuthor.Enabled = True
                BtnUploadDoc.Enabled = True

                BtnNewDoc_Click(sender, e)

            ElseIf (BtnEditRegn.Text = "Save") Then
                If (ValidatePatientRegistration() = False) Then
                    Exit Sub
                End If
                'PPPP -------------------
                ' khusrao added
                If txtMobile.Text.Trim().Length < 10 Then

                    ShowMessage("Please enter Valid Mobile Number", getValueByKey("CLAE04"))
                    Exit Sub
                End If
                PopupClose = True
                'If CheckUniqueMobileNumber(txtMobile.Text.Trim(), PatientId) Then
                '    'ShowMessage("Mobile No Is not unique", getValueByKey("CLAE05"))			
                '    ShowMessage("Mobile No Is not unique", getValueByKey("CLAE04"))
                '    Exit Sub
                'End If
                '#PPPP ---------------------
                SavePatientRegistrationInfo()
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub BtnDeleteRegn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDeleteRegn.Click
        If MsgBox("Are you sure you want to delete the Patient details?", MsgBoxStyle.YesNo, "Confirm CLose") = MsgBoxResult.Yes Then

            If dsPatientRegn.Tables.Count > 1 Then
                If (dsPatientRegn.Tables("HcPatientDetail").Rows.Count > 0) Then

                    dsPatientRegn.Tables("HcPatientDetail").Rows(0)("RecStatus") = False

                    For Each drAddress As DataRow In dsPatientRegn.Tables("HcPatientAddress").Rows
                        drAddress("RecStatus") = False
                    Next
                    For Each drAddress As DataRow In dsPatientRegn.Tables("HcPatientDocs").Rows
                        drAddress("RecStatus") = False
                    Next

                End If

                If (clsComn.PrepareSaveData(dsPatientRegn, "Patent Registration", False)) Then
                    ShowMessage("Patient ID =" & PatientId & "  : Patient Details Deleted Successfully", "Patient Registration Info")
                    'ReInitializeControls(False)
                    ClearFields(Me)
                Else
                    MessageBox.Show("Patient Deletion : Fail", "Patient Registration Info", MessageBoxButtons.OK)
                End If

            End If

        End If
    End Sub
    Private Sub BtnUploadImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUploadImage.Click

        OFileDialog = New OpenFileDialog()
        OFileDialog.Filter = filterImage '"All Files(*.*)|*.*"
        OFileDialog.FilterIndex = 1

        If (OFileDialog.ShowDialog() = DialogResult.OK) Then

            ImagePatient = Image.FromFile(OFileDialog.FileName)

            pbPhoto.Image = ImagePatient
            pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage

            OFileDialog = Nothing
        End If

    End Sub
    Private Sub BtnClearImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClearImage.Click
        pbPhoto.Image = Nothing
    End Sub

    Private Sub BtnCloseRegn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCloseRegn.Click
        If (PopupClose = True) Then
            Me.Close()

        ElseIf (IsNewRegn = True) Then
            If MsgBox("Do You really want to close this ? If yes, the data will be vanished...", MsgBoxStyle.YesNo, "Information") = MsgBoxResult.Yes Then
                Me.Close()
            End If
        ElseIf (BtnEditRegn.Text = "Save") Then
            If MsgBox("Do You want to close the current window? If Yes all entered data wil be lost.", MsgBoxStyle.YesNo, "Information") = MsgBoxResult.Yes Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub
#End Region

#Region "Search Ref Dr. for Patient"
    Private Sub BtnSearchRefDr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchRefDr.Click
        Try
            dsRefDoc = New DataSet
            dsRefDoc = objDrInfo.GetDoctorsInfo(String.Empty)

            ' comencted by khusrao adil
            Dim objRefDoctor As New frmHcCommonView
            objRefDoctor.SetDataTable = dsRefDoc.Tables("HcMstEmployee")
            objRefDoctor.InfoType = "RefDoctor"
            objRefDoctor.SetCaptionText = "Doctor Information"
            objRefDoctor.DisplayColumns = "EmployeeCode, DoctorType, FirstName, SurName, AddressLine1, AddressLine2, City, Pincode"

            If (objRefDoctor.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                RefDoctorCode = objRefDoctor.GetResultRow("EmployeeCode")
                txtRefDoctorName.Value = objRefDoctor.GetResultRow("EmployeeName")
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, "Get Ref Doctor Info Error", True)
        End Try
    End Sub
    Private Sub BtnNewRefDr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNewRefDr.Click
        ' commencted by khusrao adil
        'Dim ChildForm As New frmHCDoctorRegistration
        'ChildForm.IsNewRefDoctor = True

        'If (ChildForm.ShowDialog() = DialogResult.OK) Then
        '    txtRefDoctorName.Value = RefDoctorName
        '    RefDoctorCode = RefDoctorID
        'End If
        'RefDoctorName = String.Empty
        'RefDoctorID = String.Empty

    End Sub
#End Region
#Region "Upload Patient Document Files"
    Private Sub BtnNewDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNewDoc.Click
        Try
            If (BtnNewDoc.Text = "New") Then

                BtnAddDoc.Enabled = True
                BtnDeleteDoc.Enabled = False
                BtnNewDoc.Text = "Cancel"
                ReadOnlyDocsInfo(False)
            ElseIf (BtnNewDoc.Text = "Cancel") Then

                BtnAddDoc.Enabled = False
                BtnDeleteDoc.Enabled = True
                BtnNewDoc.Text = "New"
                ReadOnlyDocsInfo(True)

            End If

            ClearDocsInfo()

            If BtnNewDoc.Text = "Cancel" Then
                GroupBoxDocuments.Select()
                cmbDocumentType.Select()
            End If

        Catch ex As Exception
            MessageBox.Show("BtnDocNew Error : " & vbCrLf & ex.Message)
        End Try

    End Sub
    Private Sub BtnAddDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddDoc.Click
        Dim drDocs As DataRow

        If (ValidatePatientDocs() = True) Then
            drDocs = dsPatientRegn.Tables("HcPatientDocs").NewRow

            drDocs("DocumentId") = vDocNextId
            drDocs("CardNo") = PatientId
            drDocs("SiteCode") = clsAdmin.SiteCode
            drDocs("Author") = txtAuthor.Text.Trim
            drDocs("DocName") = IIf(String.IsNullOrEmpty(DocName), DBNull.Value, DocName)
            drDocs("DocumentType") = cmbDocumentType.SelectedValue
            drDocs("DocDescription") = IIf(String.IsNullOrEmpty(txtDocDescription.Text), DBNull.Value, txtDocDescription.Text)
            drDocs("DocDate") = IIf(String.IsNullOrEmpty(dtpDocDate.Text), DBNull.Value, dtpDocDate.Value)
            drDocs("DocPath") = txtFilePath.Text.Trim
            drDocs("RecStatus") = True

            drDocs("CreatedBy") = clsAdmin.UserName
            drDocs("CreatedOn") = Now
            drDocs("UpdatedBy") = clsAdmin.UserName
            drDocs("UpdatedOn") = Now

            dsPatientRegn.Tables("HcPatientDocs").Rows.Add(drDocs)

            Dim AddNodeP As New TreeNode
            AddNodeP = New TreeNode
            AddNodeP.Name = vDocNextId
            AddNodeP.Text = cmbDocumentType.SelectedText

            Dim ExistNode As New TreeNode
            For Each nextNode As TreeNode In TreeViewDocs.Nodes(0).Nodes
                If (nextNode.Text = cmbDocumentType.SelectedText) Then
                    ExistNode = nextNode
                End If
            Next

            AddNodeDocs = New TreeNode
            AddNodeDocs.Name = txtFilePath.Text
            AddNodeDocs.Tag = vDocNextId
            AddNodeDocs.Text = txtDocDescription.Text.Trim

            If (ExistNode.Name = String.Empty) Then
                TreeViewDocs.SelectedNode = AddNodeMain
                TreeViewDocs.SelectedNode.Nodes.Add(AddNodeP)
                ExistNode = AddNodeP
            End If

            TreeViewDocs.SelectedNode = ExistNode
            TreeViewDocs.SelectedNode.Nodes.Add(AddNodeDocs)
            TreeViewDocs.ExpandAll()

            vDocNextId += 1
            ClearDocsInfo()
            ReadOnlyDocsInfo(True)

            BtnNewDoc.Text = "New"
            BtnNewDoc.Select()
            BtnAddDoc.Enabled = False
            BtnDeleteDoc.Enabled = True
            OFileDialog = Nothing
        End If

    End Sub
    Private Sub BtnDeleteDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDeleteDoc.Click
        Try
            If (TreeViewDocs.SelectedNode.Tag IsNot Nothing) Then
                If MsgBox("Are you sure you want to delete the Document?", MsgBoxStyle.YesNo, "Confirm Delete") = MsgBoxResult.Yes Then

                    For Each drRow As DataRow In dsPatientRegn.Tables("HcPatientDocs").Select("DocumentId = '" & TreeViewDocs.SelectedNode.Tag & "'")
                        'drRow.Delete()
                        drRow("RecStatus") = False
                        TreeViewDocs.SelectedNode.Remove()
                        ClearDocsInfo()
                        ReadOnlyDocsInfo(False)
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("BtnDeleteDoc Error : " & vbCrLf & ex.Message)
        End Try
    End Sub
    Dim DocName As String
    Private Sub BtnUploadDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUploadDoc.Click
        OFileDialog = New OpenFileDialog()
        OFileDialog.Filter = filterImage '"All files (*.*)|*.*"
        OFileDialog.FilterIndex = 1

        If Not (OFileDialog.ShowDialog() = DialogResult.Cancel) Then
            txtFilePath.Value = OFileDialog.FileName
            DocName = OFileDialog.FileName
        End If
    End Sub

    Private Sub TreeViewDocs_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeViewDocs.DoubleClick
        Try
            If Not (TreeViewDocs.SelectedNode.Name = "MainNode") AndAlso Not (TreeViewDocs.SelectedNode.Name = Nothing) AndAlso (TreeViewDocs.SelectedNode.Name.Contains("\")) Then
                Dim Proc As New System.Diagnostics.Process
                Proc.StartInfo.FileName = TreeViewDocs.SelectedNode.Name
                Proc.Start()
            End If
        Catch ex As Exception
            MessageBox.Show("" & vbCrLf & ex.Message)
        End Try
    End Sub
    Private Sub TreeViewDocs_AfterSelect(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeViewDocs.AfterSelect

        If (TreeViewDocs.SelectedNode.Tag IsNot Nothing) Then
            ShowTreeViewInfo(TreeViewDocs.SelectedNode.Tag)
        End If

    End Sub
    Private Sub ShowTreeViewInfo(ByVal vDocumentID As String)
        Try
            Dim drDocInfo() As DataRow = dsPatientRegn.Tables("HcPatientDocs").Select("DocumentId='" & vDocumentID & "' And RecStatus='True'")

            If (drDocInfo.Length = 1) Then
                cmbDocumentType.SelectedValue = drDocInfo(0)("DocumentType")
                txtDocDescription.Value = drDocInfo(0)("DocDescription")
                txtAuthor.Value = drDocInfo(0)("Author")
                dtpDocDate.Value = drDocInfo(0)("DocDate")

                txtFilePath.Value = drDocInfo(0)("DocName")
            End If

        Catch ex As Exception
            MessageBox.Show("ShowTreeViewInfo Error :" & vbCrLf & ex.Message)
        End Try
    End Sub
    Private Sub ReadOnlyDocsInfo(ByVal IsEnableFields As Boolean)
        Try
            cmbDocumentType.ReadOnly = IsEnableFields
            txtDocDescription.ReadOnly = IsEnableFields
            txtAuthor.ReadOnly = IsEnableFields
            dtpDocDate.ReadOnly = IsEnableFields
            txtFilePath.ReadOnly = True

        Catch ex As Exception
            MessageBox.Show("ReadOnlyDocsInfo Error : " & vbCrLf & ex.Message)
        End Try
    End Sub
    Private Sub ClearDocsInfo()
        Try
            cmbDocumentType.SelectedIndex = -1
            txtDocDescription.Value = Nothing
            txtAuthor.Value = Nothing
            dtpDocDate.Value = Date.Now
            txtFilePath.Value = Nothing
            txtAuthor.Value = Nothing
            vAuthorCode = String.Empty

            Me.Update()
            Me.Refresh()

        Catch ex As Exception
            'MessageBox.Show("ClearDocsInfo Error : " & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Function ValidatePatientDocs() As Boolean
        Try

            If (String.IsNullOrEmpty(cmbDocumentType.Text)) Then
                ShowMessage("Please Select Document", "Document Info")
                cmbDocumentType.Focus()
                Return False

            ElseIf (String.IsNullOrEmpty(txtDocDescription.Text)) Then
                ShowMessage("Please Enter Document Desc", "Document Info")
                txtDocDescription.Focus()
                Return False

            ElseIf (String.IsNullOrEmpty(txtAuthor.Text)) Then
                ShowMessage("Please Enter Author", "Document Info")
                cmbDocumentType.Focus()
                Return False

            ElseIf (String.IsNullOrEmpty(txtFilePath.Text)) Then
                ShowMessage("Please Upload Document Image", "Document Info")
                cmbDocumentType.Focus()
                Return False
            End If

            Return True
        Catch ex As Exception
            Return False
            MessageBox.Show("ValidatePatientDocs Error :" & vbCrLf & ex.Message)
        End Try
    End Function

    Private Sub BtnSearchAuthor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearchAuthor.Click
        Try
            dtAuthor = New DataTable
            dtAuthor = clsPatient.GetAuthorInfo(String.Empty)

            ' comencted by khusrao adil
            'Dim objPatient As New frmHcCommonView
            'objPatient.SetDataTable = dtAuthor
            'objPatient.SetCaptionText = "Author Information"
            'objPatient.DisplayColumns = "EmployeeCode, DoctorType, FirstName, SurName, Gender, AddressLine1, AddressLine2, City, Pincode"

            'If (objPatient.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            '    Dim drPatient As DataRow = objPatient.GetResultRow
            '    vAuthorCode = drPatient("FirstName") & " " & drPatient("SurName")

            '    txtAuthor.Value = drPatient("FirstName") & " " & drPatient("SurName")
            'End If

        Catch ex As Exception
            MessageBox.Show("GetPatientDetails:" & vbCrLf & ex.Message)
        End Try
    End Sub
#End Region

#End Region

#Region "Private Functions"

    Private Sub EditableFields(ByRef ctrlMyControl As Control, ByVal blEditable As Boolean)
        For Each ctrl As Control In ctrlMyControl.Controls
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).ReadOnly = blEditable
                CType(ctrl, TextBox).BackColor = Color.White

            ElseIf TypeOf ctrl Is ctrlCombo Then
                CType(ctrl, ctrlCombo).ReadOnly = blEditable
                CType(ctrl, ctrlCombo).BackColor = Color.White

            ElseIf TypeOf ctrl Is CheckBox Then
                CType(ctrl, CheckBox).Enabled = IIf(blEditable, False, True)

            ElseIf ctrl.Controls.Count > 0 Then
                EditableFields(ctrl, blEditable)
            End If
        Next
    End Sub

    Private Sub ClearFields(ByRef ctrlMyControl As Control)
        Try
            For Each ctrl As Control In ctrlMyControl.Controls
                If TypeOf ctrl Is CtrlTextBox Then
                    DirectCast(ctrl, CtrlTextBox).Value = Nothing

                ElseIf TypeOf ctrl Is ctrlDate Then
                    DirectCast(ctrl, ctrlDate).Value = String.Empty

                ElseIf TypeOf ctrl Is ctrlCombo Then
                    DirectCast(ctrl, ctrlCombo).SelectedIndex = -1

                ElseIf ctrl.Controls.Count > 0 Then
                    ClearFields(ctrl)
                End If
            Next

            'txtPatientID.Value = clsComn.GetNextDocNo("Patent Registration") & "-" & "KOC-" & Now.Year.ToString.Substring(2, 2)
            GetNextPatientID()

            TabPageAddressInfo.Show()
            RefDoctorCode = String.Empty
            vDocNextId = 1001

            dtpDob.Value = Now.Date
            PatientId = String.Empty

            dtpDocDate.Value = Date.Now

            cmbLStayYears.SelectedIndex = 0
            cmbLStayMonths.SelectedIndex = 0
            cmbPStayYears.SelectedIndex = 0
            cmbPStayMonths.SelectedIndex = 0

            pbPhoto.Image = Nothing
            pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage
            chkCopyAddress.Checked = False
            'PPPP -------------------
            ' khusrao added
            chckKaff.Checked = False

            chckPitta.Checked = False
            chckVaat.Checked = False
            '#PPPP ------------------

            Me.Update()
            Me.Refresh()
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    Dim IsNewAddress As Boolean = False
    Private Sub SavePatientRegistrationInfo()
        Try
        
            If (PreparePatientInfo() = False) Then
                Exit Sub
            End If
            If (PreparePatientAddressInfo() = False) Then
                Exit Sub
            End If
            If (PreparePatientDocsInfo() = False) Then
                Exit Sub
            End If
            If (PreparePatientOtherInfo() = False) Then
                Exit Sub
            End If
            'If (clsComn.PrepareSaveData(dsPatientRegn, "Patent Registration", IsNewRegn)) Then
            If (clsComn.PrepareSaveData(dsPatientRegn, "Customer Loyalty", IsNewRegn)) Then ' khusrao added
                ShowMessage("Patient ID =" & PatientId & "  : Patient Details Saved Successfully", "Patient Registration Info")

                BtnNewRegn.Text = "New"
                BtnNewDoc.Text = "New"
                BtnNewRegn_Click(Nothing, Nothing)
            Else
                MessageBox.Show("Patient Registration : Fail", "Patient Registration Info", MessageBoxButtons.OK)
            End If

        Catch ex As Exception
            MessageBox.Show("SavePatientRegistrationInfo :" & vbCrLf & ex.Message)
        End Try
    End Sub
    Private Function PreparePatientInfo() As Boolean
        Try
            Dim drPatientInfo As DataRow
            Dim findKey(1) As Object
            If IsNewRegn Then
                GetNextPatientID()
                PatientId = txtPatientID.Text.Trim
            End If

            findKey(0) = PatientId
            findKey(1) = ClpProgId
            drPatientInfo = dsPatientRegn.Tables("clpcustomers").Rows.Find(findKey)

            If drPatientInfo Is Nothing Then
                drPatientInfo = dsPatientRegn.Tables("clpcustomers").NewRow
            End If

            drPatientInfo("CardNo") = PatientId
            drPatientInfo("ClpProgramId") = ClpProgId
            drPatientInfo("AccountNo") = PatientId

            drPatientInfo("SiteCode") = clsAdmin.SiteCode
            drPatientInfo("Title") = ""
            drPatientInfo("FirstName") = txtFirstName.Text.Trim
            drPatientInfo("MiddleName") = IIf(String.IsNullOrEmpty(txtMiddleName.Text), DBNull.Value, txtMiddleName.Text.Trim)
            drPatientInfo("SurName") = txtLastName.Text.Trim
            drPatientInfo("NameOnCard") = txtFirstName.Text.Trim & " " & txtLastName.Text.Trim
            drPatientInfo("CardType") = ""

            'drPatientInfo("Gender") = cboGender.SelectedValue
            drPatientInfo("Gender") = cboGender.Text  '  khusrao added

            drPatientInfo("BirthDate") = IIf(String.IsNullOrEmpty(dtpDob.Text), DBNull.Value, dtpDob.Value)
            drPatientInfo("MaritalStatus") = IIf(String.IsNullOrEmpty(cmbMaritalStatus.Text), DBNull.Value, cmbMaritalStatus.SelectedValue)

            'drPatientInfo("Education") = IIf(String.IsNullOrEmpty(cmbQualification.Text), DBNull.Value, cmbQualification.SelectedValue)
            drPatientInfo("Education") = IIf(String.IsNullOrEmpty(cmbQualification.Text), DBNull.Value, cmbQualification.Text) ' khusrao added

            drPatientInfo("Occupation") = cmbOccupation.SelectedValue
            drPatientInfo("EmailId") = IIf(String.IsNullOrEmpty(txtEmail.Text), DBNull.Value, txtEmail.Text)

            'drPatientInfo("MaritalStatus") = IIf(String.IsNullOrEmpty(cmbMaritalStatus.Text), DBNull.Value, cmbMaritalStatus.SelectedValue)
            drPatientInfo("MaritalStatus") = IIf(String.IsNullOrEmpty(cmbMaritalStatus.Text), DBNull.Value, cmbMaritalStatus.Text) ' khusrao added

            drPatientInfo("MobileNo") = IIf(String.IsNullOrEmpty(txtMobile.Text), DBNull.Value, txtMobile.Text)
            drPatientInfo("Res_Phone") = IIf(String.IsNullOrEmpty(txtResPhone.Text), "", txtResPhone.Text)
            drPatientInfo("OfficeNo") = IIf(String.IsNullOrEmpty(txtOffPhone.Text), "", txtOffPhone.Text)
            ' khusrao commnented
            'If cmbRelation.SelectedValue = 1 Then
            '    drPatientInfo("FatherName") = IIf(String.IsNullOrEmpty(txtRelation.Text), DBNull.Value, txtRelation.Text)
            'ElseIf cmbRelation.SelectedValue = 2 Then
            '    drPatientInfo("MotherName") = IIf(String.IsNullOrEmpty(txtRelation.Text), DBNull.Value, txtRelation.Text)
            'ElseIf cmbRelation.SelectedValue = 3 Then
            '    drPatientInfo("SpouseFirstName") = IIf(String.IsNullOrEmpty(txtRelation.Text), DBNull.Value, txtRelation.Text)
            'End If
            'PPPP -----------------------
            ' khusrao added
            'drPatientInfo("cardisactive") = clsAdmin.SiteCode
            drPatientInfo("cardisactive") = "True"
            If cmbRelation.SelectedValue = 3 Then
                drPatientInfo("SpouseFirstName") = IIf(String.IsNullOrEmpty(txtRelation.Text), DBNull.Value, txtRelation.Text)
            End If
            drPatientInfo("UpdatedAt") = clsAdmin.SiteCode
            drPatientInfo("Status") = True

            '#PPPP ----------------------
            drPatientInfo("UpdatedBy") = clsAdmin.UserName
            drPatientInfo("UpdatedOn") = Now
            If IsNewRegn Then
                drPatientInfo("CreatedAt") = clsAdmin.SiteCode ' khusrao added
                drPatientInfo("CreatedBy") = clsAdmin.UserName
                drPatientInfo("CreatedOn") = Now

                dsPatientRegn.Tables("clpcustomers").Rows.Add(drPatientInfo)
            End If

            Return True
        Catch ex As Exception
            MessageBox.Show("PreparePatientInfo :" & vbCrLf & ex.Message)
            Return False
        End Try

    End Function
    Private Function PreparePatientAddressInfo() As Boolean
        Try
            'Save Residential Address Data
            Dim drLAddress As DataRow
            Dim IsNewAddress As Boolean = False

            If (String.IsNullOrEmpty(txtLAddressLn1.Text) AndAlso _
                String.IsNullOrEmpty(txtLAddressLn2.Text) AndAlso _
                String.IsNullOrEmpty(cmbLCity.Text)) Then

                Return True
                Exit Function
            End If
            AfindKey(0) = PatientId
            AfindKey(1) = ClpProgId
            AfindKey(2) = 2
            drLAddress = dsPatientRegn.Tables("CLPCustomerAddress").Rows.Find(AfindKey)

            If drLAddress Is Nothing Then
                drLAddress = dsPatientRegn.Tables("CLPCustomerAddress").NewRow
                'drLAddress("PatientId") = PatientId
                drLAddress("CardNo") = PatientId ' khusrao added
                IsNewAddress = True
            End If
            drLAddress("Clpprogramid") = ClpProgId
            drLAddress("AddressType") = 2
            'drLAddress("AddressLine1") = txtLAddressLn1.Text
            'drLAddress("AddressLine2") = IIf(txtLAddressLn2.Text = String.Empty, DBNull.Value, txtLAddressLn2.Text)

            drLAddress("AddressLn1") = txtLAddressLn1.Text  ' khusrao added
            drLAddress("AddressLn2") = IIf(txtLAddressLn2.Text = String.Empty, DBNull.Value, txtLAddressLn2.Text)  ' khusrao added

            drLAddress("CityCode") = cmbLCity.SelectedValue
            drLAddress("StateCode") = cmbLState.SelectedValue
            drLAddress("CountryCode") = cmbLCountry.SelectedValue
            drLAddress("Pincode") = IIf(txtLPincode.Text = String.Empty, DBNull.Value, txtLPincode.Text)
            drLAddress("Defaults") = 0
            drLAddress("UpdatedAt") = clsAdmin.SiteCode ' khusrao added
            drLAddress("UpdatedBy") = clsAdmin.UserName
            drLAddress("UpdatedOn") = Now
            drLAddress("Status") = True ' khusrao added

            If IsNewRegn Or IsNewAddress Then
                drLAddress("CreatedAt") = clsAdmin.SiteCode ' khusrao added
                drLAddress("CreatedBy") = clsAdmin.UserName
                drLAddress("CreatedOn") = Now
                ' dsPatientRegn.Tables("HcPatientAddress").Rows.Add(drLAddress)
                dsPatientRegn.Tables("CLPCustomerAddress").Rows.Add(drLAddress)  ' khusrao added
            End If

            'save alternate address
            'start
            Dim drPAddress As DataRow
            'Dim IsNewAddressL As Boolean = False
            If (String.IsNullOrEmpty(txtPAddressLn1.Text) AndAlso _
                String.IsNullOrEmpty(txtPAddressLn2.Text) AndAlso _
                String.IsNullOrEmpty(cmbPCity.Text)) Then

                Return True
                Exit Function
            End If
            AfindKey(0) = PatientId
            AfindKey(1) = ClpProgId
            AfindKey(2) = 1
            drPAddress = dsPatientRegn.Tables("CLPCustomerAddress").Rows.Find(AfindKey)

            If drPAddress Is Nothing Then
                drPAddress = dsPatientRegn.Tables("CLPCustomerAddress").NewRow
                ' drPAddress("PatientId") = PatientId
                drPAddress("CardNo") = PatientId ' khusrao added
                IsNewAddress = True
            End If

            drPAddress("Clpprogramid") = ClpProgId  ' khusrao added
            drPAddress("AddressType") = 1
            'drPAddress("AddressLine1") = txtPAddressLn1.Text
            'drPAddress("AddressLine2") = IIf(txtPAddressLn2.Text = String.Empty, DBNull.Value, txtPAddressLn2.Text)
            drPAddress("AddressLn1") = txtPAddressLn1.Text  ' khusrao added
            drPAddress("AddressLn2") = IIf(txtPAddressLn2.Text = String.Empty, DBNull.Value, txtPAddressLn2.Text)  ' khusrao added
            drPAddress("CityCode") = cmbPCity.SelectedValue
            drPAddress("StateCode") = cmbPState.SelectedValue
            drPAddress("CountryCode") = cmbPCountry.SelectedValue
            drPAddress("Pincode") = IIf(txtPPincode.Text = String.Empty, DBNull.Value, txtPPincode.Text)
            drPAddress("Defaults") = 1
            drPAddress("UpdatedBy") = clsAdmin.UserName
            drPAddress("UpdatedOn") = Now

            If IsNewRegn Or IsNewAddress Then
                drPAddress("CreatedBy") = clsAdmin.UserName
                drPAddress("CreatedOn") = Now
                'dsPatientRegn.Tables("HcPatientAddress").Rows.Add(drPAddress)
                dsPatientRegn.Tables("CLPCustomerAddress").Rows.Add(drPAddress)  ' khusrao added
            End If

            'end
            Return True

        Catch ex As Exception
            MessageBox.Show("PreparePatientAddressInfo :" & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    Private Function PreparePatientDocsInfo() As Boolean
        Try
            If (Not System.IO.Directory.Exists(vDocPathInServer + "\" + PatientId)) Then
                System.IO.Directory.CreateDirectory(vDocPathInServer + "\" + PatientId)
            End If

            If (IsNewRegn) Then
                For Each drDocs As DataRow In dsPatientRegn.Tables("HcPatientDocs").Rows
                    fileExtention = drDocs("DocName").Split(".")(1)
                    fileDocName = vDocPathInServer & "\" & PatientId & "\" & PatientId & Now.ToString("ddhhmmssfff") & "." & fileExtention

                    Try
                        File.Copy(drDocs("DocPath"), fileDocName)
                    Catch ex As Exception
                        ShowMessage("Document(s) Not Save to Common Folder", "Document Info", False)
                    End Try

                    'drDocs("PatientId") = PatientId
                    drDocs("CardNo") = PatientId   ' khusrao added
                    drDocs("DocPath") = fileDocName
                Next
            Else
                For Each drDocs As DataRow In dsPatientRegn.Tables("HcPatientDocs").Select(Nothing, Nothing, DataViewRowState.ModifiedOriginal)
                    drDocs("RecStatus") = False
                Next

                For Each drDocs As DataRow In dsPatientRegn.Tables("HcPatientDocs").Select(Nothing, Nothing, DataViewRowState.Added)
                    fileExtention = drDocs("DocName").Split(".")(1)
                    fileDocName = vDocPathInServer & "\" & PatientId & "\" & PatientId & Now.ToString("ddhhmmssfff") & "." & fileExtention

                    Try
                        File.Copy(drDocs("DocPath"), fileDocName)
                    Catch ex As Exception

                        ShowMessage("Document(s) Not Save to Common Folder", "Document Info", False)
                    End Try

                    'drDocs("PatientId") = PatientId
                    drDocs("CardNo") = PatientId   ' khusrao added
                    drDocs("DocPath") = fileDocName
                Next
            End If

            Return True
        Catch ex As Exception
            MessageBox.Show("PreparePatientDocsInfo Error : " & vbCrLf & ex.Message)
            Return False
        End Try

    End Function

    Private Function PreparePatientOtherInfo() As Boolean
        Try
            Dim drPatientOtherInfo As DataRow

            If IsNewRegn Then
                GetNextPatientID()
                PatientId = txtPatientID.Text.Trim
            End If

            PfindKey(0) = PatientId
            PfindKey(1) = ClpProgId
            drPatientOtherInfo = dsPatientRegn.Tables("HCPatientOtherDetails").Rows.Find(PfindKey)

            If drPatientOtherInfo Is Nothing Then
                drPatientOtherInfo = dsPatientRegn.Tables("HCPatientOtherDetails").NewRow
            End If

            drPatientOtherInfo("CardNo") = PatientId
            drPatientOtherInfo("ClpProgramId") = ClpProgId
            drPatientOtherInfo("SiteCode") = clsAdmin.SiteCode
            drPatientOtherInfo("Salutation") = cmbSalutation.SelectedValue

            drPatientOtherInfo("AgeYears") = IIf((cmbAgeYears.Text = String.Empty), DBNull.Value, cmbAgeYears.Text)
            drPatientOtherInfo("AgeMonths") = IIf((cmbAgeMonths.Text = String.Empty), DBNull.Value, cmbAgeMonths.Text)
            drPatientOtherInfo("RefDoctorId") = IIf(String.IsNullOrEmpty(RefDoctorCode), DBNull.Value, RefDoctorCode)
            drPatientOtherInfo("NearestRelative") = IIf(String.IsNullOrEmpty(txtNearestRelative.Text), DBNull.Value, txtNearestRelative.Text.Trim)
            drPatientOtherInfo("MontlyIncome") = IIf(String.IsNullOrEmpty(txtMonthlyIncome.Text), DBNull.Value, txtMonthlyIncome.Text)
            drPatientOtherInfo("MotherTongue") = IIf(String.IsNullOrEmpty(cmbMotherTounge.Text), DBNull.Value, cmbMotherTounge.SelectedValue)

            If cmbRelation.SelectedValue = 1 Then
                drPatientOtherInfo("FatherName") = IIf(String.IsNullOrEmpty(txtRelation.Text), DBNull.Value, txtRelation.Text)
            ElseIf cmbRelation.SelectedValue = 2 Then
                drPatientOtherInfo("MotherName") = IIf(String.IsNullOrEmpty(txtRelation.Text), DBNull.Value, txtRelation.Text)
            End If

            drPatientOtherInfo("Religon") = IIf(String.IsNullOrEmpty(txtReligon.Text), DBNull.Value, txtReligon.Text)
            drPatientOtherInfo("HeightCm") = IIf(String.IsNullOrEmpty(txtHeightCm.Text), DBNull.Value, txtHeightCm.Text)
            drPatientOtherInfo("WeightKg") = IIf(String.IsNullOrEmpty(txtWeightKg.Text), DBNull.Value, txtWeightKg.Text)
 
            drPatientOtherInfo("BloodGroup") = cmbPatientBloodGroup.SelectedValue
            drPatientOtherInfo("Nationality") = IIf(String.IsNullOrEmpty(txtNationality.Text), DBNull.Value, txtNationality.Text)

            drPatientOtherInfo("StayDurYearsL") = IIf(String.IsNullOrEmpty(cmbLStayYears.Text), DBNull.Value, cmbLStayYears.Text)
            drPatientOtherInfo("StayDurMonthsL") = IIf(String.IsNullOrEmpty(cmbLStayMonths.Text), DBNull.Value, cmbLStayMonths.Text)

            drPatientOtherInfo("StayDurYearsP") = IIf(String.IsNullOrEmpty(cmbPStayYears.Text), DBNull.Value, cmbPStayYears.Text)
            drPatientOtherInfo("StayDurMonthsP") = IIf(String.IsNullOrEmpty(cmbPStayMonths.Text), DBNull.Value, cmbPStayMonths.Text)
            
            drPatientOtherInfo("PatientImage") = clsComn.ImageToByteArray(ImagePatient)

            'PPPP ---------------------------------------------------
            ' khusrao added
            Dim StrNatureOfBody As String = ""
            If chckKaff.Checked Then
                StrNatureOfBody = chckKaff.Text
            End If
            If chckPitta.Checked Then
                StrNatureOfBody = StrNatureOfBody & "," & chckPitta.Text
            End If
            If chckVaat.Checked Then
                StrNatureOfBody = StrNatureOfBody & "," & chckVaat.Text
            End If
            drPatientOtherInfo("NatureOfBody") = StrNatureOfBody
            '#PPPP ---------------------------------------------------

            drPatientOtherInfo("Classify") = "Patient"
            drPatientOtherInfo("UpdatedAt") = clsAdmin.SiteCode  ' khusrao added
            drPatientOtherInfo("UpdatedBy") = clsAdmin.UserName
            drPatientOtherInfo("UpdatedOn") = Now
            drPatientOtherInfo("RecStatus") = True
            drPatientOtherInfo("Status") = True ' khusrao added
            If IsNewRegn Then
                drPatientOtherInfo("CreatedAt") = clsAdmin.SiteCode ' khusrao added
                drPatientOtherInfo("CreatedBy") = clsAdmin.UserName
                drPatientOtherInfo("CreatedOn") = Now

                dsPatientRegn.Tables("HCPatientOtherDetails").Rows.Add(drPatientOtherInfo)
            End If

            Return True
        Catch ex As Exception
            MessageBox.Show("PreparePatientInfo :" & vbCrLf & ex.Message)
            Return False
        End Try

    End Function

    Private Function ValidatePatientRegistration() As Boolean
        Try
            Dim ObMobile As New clsHcPatientInfo
            If txtFirstName.Text = "" Then
                ShowMessage(getValueByKey("SOC008"), "SOC008 - " & getValueByKey("CLAE04"))
                txtFirstName.Focus()
                Return IsValidated
                Exit Function
            ElseIf txtLastName.Text = "" Then
                ShowMessage(getValueByKey("SOC009"), "SOC009 - " & getValueByKey("CLAE04"))
                txtLastName.Focus()
                Return IsValidated
                Exit Function
            ElseIf cboGender.Text = "" Then
                ShowMessage(getValueByKey("SOC010"), "SOC010 - " & getValueByKey("CLAE04"))
                cboGender.Focus()
                Return IsValidated
                Exit Function
            ElseIf Not IsDBNull(dtpDob.Value) AndAlso Format(dtpDob.Value, "yyyy") > Format(Now.Date, "yyyy") Then
                'If Format(dtpDob.Value, "yyyy") > Format(Now.Date, "yyyy") Then
                ShowMessage(getValueByKey("SOC014"), "SOC014 - " & getValueByKey("CLAE04"))
                dtpDob.Focus()
                Return IsValidated
                Exit Function
                'End If
            ElseIf txtLAddressLn1.Text = "" Then
                ShowMessage(getValueByKey("SOC011"), "SOC011 - " & getValueByKey("CLAE04"))
                TabPageAddressInfo.Show()
                txtLAddressLn1.Focus()
                Return IsValidated
                Exit Function
            ElseIf txtLAddressLn2.Text = "" Then
                ShowMessage(getValueByKey("SOC011"), "SOC011 - " & getValueByKey("CLAE04"))
                TabPageAddressInfo.Show()
                txtLAddressLn2.Focus()
                Return IsValidated
                Exit Function
            ElseIf cmbLCountry.Text = "" Then
                ShowMessage("Please Select Country Of Residence ", "Patient Info")
                TabPageAddressInfo.Show()
                cmbLCountry.Focus()
                Return IsValidated
                Exit Function
            ElseIf (cmbLState.Text = "") Then
                ShowMessage("Please Select State Of Residence ", "Patient Info")
                TabPageAddressInfo.Show()
                cmbLState.Focus()
                Return IsValidated
                Exit Function
            ElseIf (cmbLCity.Text = "") Then
                ShowMessage("Please Select City Of Residence ", "Patient Info")
                TabPageAddressInfo.Show()
                cmbLCity.Focus()
                Return IsValidated
                Exit Function
            ElseIf (txtLPincode.Text = "") Then
                ShowMessage(getValueByKey("SOC013"), "SOC013 - " & getValueByKey("CLAE04"))
                TabPageAddressInfo.Show()
                txtLPincode.Focus()
                Return IsValidated
                Exit Function
                'ElseIf cmbLStayYears.Text = String.Empty Then
                '    ShowMessage("Please Select Years ", "Patient Info")
                '    TabPageAddressInfo.Show()
                '    cmbLStayYears.Focus()
                '    Return IsValidated
                '    Exit Function
                'ElseIf cmbLStayMonths.Text = String.Empty Then
                '    ShowMessage("Please Select Months", "Patient Info")
                '    TabPageAddressInfo.Show()
                '    cmbLStayMonths.Focus()
                '    Return IsValidated
                '    Exit Function
            ElseIf (String.IsNullOrEmpty(txtMobile.Text) And String.IsNullOrEmpty(txtResPhone.Text)) Then
                ShowMessage("Atleast one telephone no. is required", "Patient Info")
                TabPagePatientInfo.Show()
                txtMobile.Focus()
                Return IsValidated
                Exit Function

            ElseIf txtEmail.Text <> String.Empty AndAlso validateEmailId(txtEmail.Text) = False Then
                ShowMessage(getValueByKey("HMD003"), "HMD003 - " & getValueByKey("CLAE04"))
                TabPagePatientInfo.Show()
                txtEmail.Focus()
                Return IsValidated
                Exit Function

            ElseIf (cmbQualification.Text = "") Then
                ShowMessage("Qualification is Required", "Patient Info")
                TabPagePatientInfo.Show()
                cmbQualification.Focus()
                Return IsValidated
                Exit Function
            ElseIf (cmbOccupation.Text = "") Then
                ShowMessage("Occuption is Required", "Patient Info")
                TabPagePatientInfo.Show()
                cmbOccupation.Focus()
                Return IsValidated
                Exit Function
            ElseIf (Not (String.IsNullOrEmpty(txtWeightKg.Text)) AndAlso CDec(txtWeightKg.Text) > 250) Then
                ShowMessage("Weight Cannot greater than 250 Kg", "Patient Info")
                txtWeightKg.Clear()
                txtWeightKg.Focus()
                Return IsValidated
                Exit Function
            ElseIf ObMobile.GetMobileNo(txtMobile.Text) = True Then
                ShowMessage("Mobile No. already exist", "Patient Info")
                'txtWeightKg.Clear()
                txtMobile.Focus()
                Return IsValidated
            End If

            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Return False
        End Try
    End Function

    Private Sub FillComboData()


        Dim dsCombo = objCustm.GetComboDataSet

        dtLCountry = dsCombo.Tables("CountryTab").Copy()
        dtLState = dsCombo.Tables("StateTab").Copy()
        dtLCity = dsCombo.Tables("CityTab").Copy()
        dtPCountry = dsCombo.Tables("CountryTab").Copy()
        dtPState = dsCombo.Tables("StateTab").Copy()
        dtPCity = dsCombo.Tables("CityTab").Copy()

        PopulateComboBox(dtLCity, cmbLCity)
        PopulateComboBox(dtLState, cmbLState)
        PopulateComboBox(dtLCountry, cmbLCountry)
        PopulateComboBox(dtPCity, cmbPCity)
        PopulateComboBox(dtPState, cmbPState)
        PopulateComboBox(dtPCountry, cmbPCountry)

        pC1ComboSetDisplayMember(cmbLCity)
        pC1ComboSetDisplayMember(cmbLCountry)
        pC1ComboSetDisplayMember(cmbPCity)
        pC1ComboSetDisplayMember(cmbPState)
        pC1ComboSetDisplayMember(cmbLState)
        pC1ComboSetDisplayMember(cmbPCountry)

        PopulateComboBox(dsCombo.Tables("GenderTab"), cboGender)

        PopulateComboBox(dsCombo.Tables("MaritalTab"), cmbMaritalStatus)
        PopulateComboBox(dsCombo.Tables("EducationTab"), cmbQualification)
        PopulateComboBox(dsCombo.Tables("OccupationTab"), cmbOccupation)
        PopulateComboBox(dsCombo.Tables("TitleTab"), cmbSalutation)
        PopulateComboBox(dsCombo.Tables("DocumentTypeTab"), cmbDocumentType)
        'PopulateComboBox(dsCombo.Tables("AuthorTab"), cmbAuthor)
        PopulateComboBox(dsCombo.Tables("SalutationTab"), cmbSalutation)
        PopulateComboBox(dsCombo.Tables("RelationTab"), cmbRelation)
        PopulateComboBox(dsCombo.Tables("MotherToungeTab"), cmbMotherTounge)

        PopulateComboBox(dsCombo.Tables("BloodGroupTab"), cmbPatientBloodGroup)
        pC1ComboSetDisplayMember(cmbPatientBloodGroup)

        pC1ComboSetDisplayMember(cboGender)
        pC1ComboSetDisplayMember(cmbMaritalStatus)
        pC1ComboSetDisplayMember(cmbQualification)
        pC1ComboSetDisplayMember(cmbOccupation)
        pC1ComboSetDisplayMember(cmbSalutation)

        pC1ComboSetDisplayMember(cmbDocumentType)
        pC1ComboSetDisplayMember(cmbSalutation)
        pC1ComboSetDisplayMember(cmbRelation)
        pC1ComboSetDisplayMember(cmbMotherTounge)

        For i As Integer = 0 To 120
            cmbAgeYears.AddItem(i.ToString)
            cmbLStayYears.AddItem(i.ToString)
            cmbPStayYears.AddItem(i.ToString)
        Next

        For i As Integer = 0 To 12
            cmbAgeMonths.AddItem(i.ToString)
            cmbPStayMonths.AddItem(i.ToString)
            cmbLStayMonths.AddItem(i.ToString)
        Next

    End Sub

    Public Sub RefreshPatientInfo(ByVal dsInfo As DataSet, ByVal PatientId As String, Optional ByRef dtCust As DataTable = Nothing)
        Try
            If Not (dsInfo Is Nothing) Then
                'If (dsInfo.Tables("CLPCustomers").Rows.Count > 0) Then ' khusrao added
                '    Dim drPatient As DataRow = dsInfo.Tables("clpCustomers").Select("CardNo='" & PatientId & "'")(0) ' khusrao added
                If (Not dtCust Is Nothing AndAlso dtCust.Rows.Count > 0) Then ' khusrao added
                    ' Dim drPatient As DataRow = dsInfo.Tables("clpCustomers").Select("CardNo='" & PatientId & "'")(0) ' khusrao added
                    Dim drPatient As DataRow = dtCust.Select("CardNo='" & PatientId & "'")(0) ' khusrao added

                    ClearFields(Me)
                    TabPageAddressInfo.Show()


                    PatientId = drPatient("CardNo")
                    txtPatientID.Value = PatientId
                    'cmbSalutation.SelectedValue = IIf(Not drPatient("Salutation") Is DBNull.Value, drPatient("Salutation"), -1)
                    txtFirstName.Value = IIf(Not drPatient("FirstName") Is DBNull.Value, drPatient("FirstName"), String.Empty)
                    txtMiddleName.Value = IIf(Not drPatient("MiddleName") Is DBNull.Value, drPatient("MiddleName"), String.Empty)

                    txtLastName.Value = IIf(Not drPatient("SurName") Is DBNull.Value, drPatient("SurName"), String.Empty)  ' khusrao added

                    If (drPatient("BirthDate") Is DBNull.Value) Then
                        dtpDob.Value = Nothing
                    Else
                        dtpDob.Value = drPatient("BirthDate")
                    End If
                    'dtpDob.Value = IIf(Not drPatient("DateofBirth") Is DBNull.Value, drPatient("DateofBirth"), String.Empty)
                    ' cboGender.SelectedValue = IIf(Not drPatient("Gender") Is DBNull.Value, drPatient("Gender"), -1)
                    cboGender.Text = IIf(Not drPatient("Gender") Is DBNull.Value, drPatient("Gender"), -1)
                    txtRefDoctorName.Value = IIf(Not drPatient("ReferedBy") Is DBNull.Value, drPatient("ReferedBy"), String.Empty)

                    txtResPhone.Value = IIf(Not drPatient("Res_Phone") Is DBNull.Value, drPatient("Res_Phone"), String.Empty)
                    txtMobile.Value = IIf(Not drPatient("MobileNo") Is DBNull.Value, drPatient("MobileNo"), String.Empty)
                    uniquemblno = txtMobile.Value
                    txtOffPhone.Value = IIf(Not drPatient("OfficeNo") Is DBNull.Value, drPatient("OfficeNo"), String.Empty)
                    'cmbAgeYears.Text = IIf(Not drPatient("AGEYEARS") Is DBNull.Value, drPatient("AGEYEARS"), String.Empty)
                    'cmbAgeMonths.Text = IIf(Not drPatient("AGEMONTHS") Is DBNull.Value, drPatient("AGEMONTHS"), String.Empty)
                    txtHeightCm.Value = IIf(Not drPatient("HEIGHTCM") Is DBNull.Value, drPatient("HEIGHTCM"), String.Empty)
                    txtWeightKg.Value = IIf(Not drPatient("WEIGHTKG") Is DBNull.Value, drPatient("WEIGHTKG"), String.Empty)
                    'cmbOccupation.SelectedText = IIf(Not drPatient("Occupation") Is DBNull.Value, drPatient("Occupation"), -1)
                    cmbOccupation.SelectedValue = IIf(Not drPatient("Occupation") Is DBNull.Value, drPatient("Occupation"), -1)
                    'cmbQualification.SelectedText = IIf(Not drPatient("Education") Is DBNull.Value, drPatient("Education"), -1)
                    cmbQualification.Text = IIf(Not drPatient("Education") Is DBNull.Value, drPatient("Education"), -1)
                    txtEmail.Value = IIf(Not drPatient("EmailId") Is DBNull.Value, drPatient("EmailId"), String.Empty)
                    ' cboGender.SelectedText = IIf(Not drPatient("Gender") Is DBNull.Value, drPatient("Gender"), String.Empty)
                    'cmbMaritalStatus.SelectedText = IIf(Not drPatient("MaritalStatus") Is DBNull.Value, drPatient("MaritalStatus"), -1)
                    cmbMaritalStatus.Text = IIf(Not drPatient("MaritalStatus") Is DBNull.Value, drPatient("MaritalStatus"), -1)
                    txtReligon.Value = IIf(Not drPatient("RELIGON") Is DBNull.Value, drPatient("RELIGON"), String.Empty)
                    cmbMotherTounge.SelectedValue = IIf(Not drPatient("MOTHERTONGUE") Is DBNull.Value, drPatient("MOTHERTONGUE"), -1)
                    txtNationality.Value = IIf(Not drPatient("NATIONALITY") Is DBNull.Value, drPatient("NATIONALITY"), String.Empty)
                    txtNearestRelative.Value = IIf(Not drPatient("NEARESTRELATIVE") Is DBNull.Value, drPatient("NEARESTRELATIVE"), String.Empty)
                    'txtMonthlyIncome.Value = IIf(Not drPatient("MONTLYINCOME") Is DBNull.Value, drPatient("MONTLYINCOME"), String.Empty)

                    If Not drPatient("SpouseFirstName") Is DBNull.Value Then
                        '    cmbRelation.SelectedValue = 1
                        '    txtRelation.Value = drPatient("FATHERNAME")
                        'ElseIf Not drPatient("MOTHERNAME") Is DBNull.Value Then
                        '    cmbRelation.SelectedValue = 2
                        '    txtRelation.Value = drPatient("MOTHERNAME")
                        'ElseIf Not drPatient("SPOUSENAME") Is DBNull.Value Then
                        cmbRelation.SelectedValue = 3
                        txtRelation.Value = drPatient("SpouseFirstName")
                    Else
                        cmbRelation.SelectedValue = -1
                        txtRelation.Value = String.Empty
                    End If

                    '   RefDoctorCode = IIf(Not drPatient("RefDoctorId") Is DBNull.Value, drPatient("RefDoctorId"), String.Empty)

                    If (String.IsNullOrEmpty(RefDoctorCode) = False) Then
                        dtPatient = New DataTable
                        dtPatient = clsPatient.GetDoctorInfo(RefDoctorCode)

                        If (dtPatient.Rows.Count > 0) Then
                            txtRefDoctorName.Value = dtPatient.Rows(0)("EmployeeName")
                        End If

                    End If

                    'Display Patient Image
                    'Dim BytesImage As Byte() = IIf(IsDBNull(drPatient("PatientImage")), Nothing, drPatient("PatientImage"))
                    'ImagePatient = clsComn.ByteArrayToImage(BytesImage)

                    'pbPhoto.Image = Nothing
                    'pbPhoto.Image = ImagePatient
                    'pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage

                    ''Display Address Details : Local

                    'txtLAddressLn1.Value = IIf(Not drPatient("AddressLine1") Is DBNull.Value, drPatient("AddressLine1"), String.Empty)
                    'txtLAddressLn2.Value = IIf(Not drPatient("AddressLine2") Is DBNull.Value, drPatient("AddressLine2"), String.Empty)
                    'txtLPincode.Value = IIf(Not drPatient("PinCode") Is DBNull.Value, drPatient("PinCode"), String.Empty)
                    'cmbLCountry.SelectedValue = IIf(Not drPatient("CountryCode") Is DBNull.Value, drPatient("CountryCode"), -1)
                    'cmbLState.SelectedValue = IIf(Not drPatient("StateCode") Is DBNull.Value, drPatient("StateCode"), -1)
                    'cmbLCity.SelectedValue = IIf(Not drPatient("CityCode") Is DBNull.Value, drPatient("CityCode"), -1)
                    'cmbLStayYears.Text = IIf(Not drPatient("StayDurationYears") Is DBNull.Value, drPatient("STAYDURATIONYEARS"), String.Empty)
                    'cmbLStayMonths.Text = IIf(Not drPatient("StayDurationMonths") Is DBNull.Value, drPatient("STAYDURATIONMONTHS"), String.Empty)

                    'Display Address Details : Permanent
                    If dsInfo.Tables("CLPCustomerAddress").Rows.Count > 0 Then
                        'Dim drPAddress As DataRow = dsInfo.Tables("CLPCustomerAddress").Rows(0)

                        For Each drPatient In dsInfo.Tables("CLPCustomerAddress").Select("AddressType=1")
                            'Display Address Details : Local			

                            txtLAddressLn1.Value = IIf(Not drPatient("AddressLn1") Is DBNull.Value, drPatient("AddressLn1"), String.Empty)
                            txtLAddressLn2.Value = IIf(Not drPatient("AddressLn2") Is DBNull.Value, drPatient("AddressLn2"), String.Empty)
                            txtLPincode.Value = IIf(Not drPatient("PinCode") Is DBNull.Value, drPatient("PinCode"), String.Empty)
                            cmbLCountry.SelectedValue = IIf(Not drPatient("CountryCode") Is DBNull.Value, drPatient("CountryCode"), -1)
                            cmbLState.SelectedValue = IIf(Not drPatient("StateCode") Is DBNull.Value, drPatient("StateCode"), -1)
                            cmbLCity.SelectedValue = IIf(Not drPatient("CityCode") Is DBNull.Value, drPatient("CityCode"), -1)
                        Next
                        For Each drPatient In dsInfo.Tables("CLPCustomerAddress").Select("AddressType=2")
                            txtPAddressLn1.Value = IIf(Not drPatient("AddressLn1") Is DBNull.Value, drPatient("AddressLn1"), String.Empty)
                            txtPAddressLn2.Value = IIf(Not drPatient("AddressLn2") Is DBNull.Value, drPatient("AddressLn2"), String.Empty)
                            txtPPincode.Value = IIf(Not drPatient("PinCode") Is DBNull.Value, drPatient("PinCode"), String.Empty)
                            cmbPCountry.SelectedValue = IIf(Not drPatient("CountryCode") Is DBNull.Value, drPatient("CountryCode"), -1)
                            cmbPState.SelectedValue = IIf(Not drPatient("StateCode") Is DBNull.Value, drPatient("StateCode"), -1)
                            cmbPCity.SelectedValue = IIf(Not drPatient("CityCode") Is DBNull.Value, drPatient("CityCode"), -1)
                        Next
                    End If

                    'Document Details
                    If dsInfo.Tables("HcPatientDocs").Rows.Count > 0 Then
                        TreeViewDocs.Nodes.Clear()
                        AddNodeMain = New TreeNode
                        AddNodeMain.Name = "MainNode"
                        AddNodeMain.Text = "Patient documents"
                        TreeViewDocs.Nodes.Add(AddNodeMain)

                        For Each drFiles As DataRow In dsInfo.Tables("HcPatientDocs").Select("RecStatus='True'")

                            'dsCombo.Tables("DocumentTypeTab") 
                            'DirectCast(cmbDocumentType.DataSource,DataTable)   'DocumentType

                            'AddNodeDocs = New TreeNode
                            'AddNodeDocs.Name = drFiles("DocPath")
                            'AddNodeDocs.Tag = drFiles("DocumentId")
                            'AddNodeDocs.Text = drFiles("DocDescription")

                            'TreeViewDocs.SelectedNode = AddNodeMain
                            'TreeViewDocs.SelectedNode.Nodes.Add(AddNodeDocs)

                            Dim drDocInfo As DataRow() = DirectCast(cmbDocumentType.DataSource, DataTable).Select("Code='" & drFiles("DocumentType") & "'")

                            Dim AddNodeP As New TreeNode
                            AddNodeP = New TreeNode
                            AddNodeP.Name = drDocInfo(0)("Code")
                            AddNodeP.Text = drDocInfo(0)("LongDesc")

                            Dim ExistNode As New TreeNode
                            For Each nextNode As TreeNode In TreeViewDocs.Nodes(0).Nodes
                                If (nextNode.Text = AddNodeP.Text) Then
                                    ExistNode = nextNode
                                End If
                            Next

                            AddNodeDocs = New TreeNode
                            AddNodeDocs.Name = drFiles("DocPath")
                            AddNodeDocs.Tag = drFiles("DocumentId")
                            AddNodeDocs.Text = drFiles("DocDescription")

                            If (ExistNode.Name = String.Empty) Then
                                TreeViewDocs.SelectedNode = AddNodeMain
                                TreeViewDocs.SelectedNode.Nodes.Add(AddNodeP)
                                ExistNode = AddNodeP
                            End If

                            TreeViewDocs.SelectedNode = ExistNode
                            TreeViewDocs.SelectedNode.Nodes.Add(AddNodeDocs)
                        Next

                        vDocNextId = CInt(dsInfo.Tables("HcPatientDocs").Compute("Max(DocumentId)", Nothing))
                        vDocNextId += 1

                        TreeViewDocs.ExpandAll()
                    End If

                    If dsInfo.Tables("HCPatientOtherDetails").Rows.Count > 0 Then
                        For Each drOtherDocs As DataRow In dsInfo.Tables("HCPatientOtherDetails").Select("Status='True'")
                            cmbSalutation.SelectedValue = IIf(Not drOtherDocs("Salutation") Is DBNull.Value, drOtherDocs("Salutation"), -1)
                            txtReligon.Value = IIf(Not drOtherDocs("RELIGON") Is DBNull.Value, drOtherDocs("RELIGON"), String.Empty)
                            'cmbMotherTounge.SelectedValue = IIf(Not drOtherDocs("MOTHERTONGUE") Is DBNull.Value, drOtherDocs("MOTHERTONGUE"), -1)
                            txtNationality.Value = IIf(Not drOtherDocs("NATIONALITY") Is DBNull.Value, drOtherDocs("NATIONALITY"), String.Empty)
                            txtNearestRelative.Value = IIf(Not drOtherDocs("NEARESTRELATIVE") Is DBNull.Value, drOtherDocs("NEARESTRELATIVE"), String.Empty)
                            txtMonthlyIncome.Value = IIf(Not drOtherDocs("MONTLYINCOME") Is DBNull.Value, drOtherDocs("MONTLYINCOME"), String.Empty)

                            cmbAgeYears.Text = IIf(Not drOtherDocs("AGEYEARS") Is DBNull.Value, drOtherDocs("AGEYEARS"), String.Empty)
                            cmbAgeMonths.Text = IIf(Not drOtherDocs("AGEMONTHS") Is DBNull.Value, drOtherDocs("AGEMONTHS"), String.Empty)
                            txtHeightCm.Value = IIf(Not drOtherDocs("HEIGHTCM") Is DBNull.Value, drOtherDocs("HEIGHTCM"), String.Empty)
                            txtWeightKg.Value = IIf(Not drOtherDocs("WEIGHTKG") Is DBNull.Value, drOtherDocs("WEIGHTKG"), String.Empty)
                            If Not drOtherDocs("FATHERNAME") Is DBNull.Value Then
                                cmbRelation.SelectedValue = 1
                                txtRelation.Value = drOtherDocs("FATHERNAME")
                            ElseIf Not drOtherDocs("MOTHERNAME") Is DBNull.Value Then
                                cmbRelation.SelectedValue = 2
                                txtRelation.Value = drOtherDocs("MOTHERNAME")
                            End If
                            cmbPStayYears.Text = IIf(Not drOtherDocs("STAYDURYEARSP") Is DBNull.Value, drOtherDocs("STAYDURYEARSP"), String.Empty)
                            cmbPStayMonths.Text = IIf(Not drOtherDocs("STAYDURMONTHSP") Is DBNull.Value, drOtherDocs("STAYDURMONTHSP"), String.Empty)
                            cmbLStayYears.Text = IIf(Not drOtherDocs("STAYDURYEARSL") Is DBNull.Value, drOtherDocs("STAYDURYEARSL"), String.Empty)
                            cmbLStayMonths.Text = IIf(Not drOtherDocs("STAYDURMONTHSL") Is DBNull.Value, drOtherDocs("STAYDURMONTHSL"), String.Empty)

                            'Display Patient Image			
                            Dim BytesImage As Byte() = IIf(IsDBNull(drOtherDocs("PatientImage")), Nothing, drOtherDocs("PatientImage"))
                            ImagePatient = clsComn.ByteArrayToImage(BytesImage)

                            pbPhoto.Image = Nothing
                            pbPhoto.Image = ImagePatient
                            pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage

                            cmbPatientBloodGroup.SelectedValue = IIf(Not drOtherDocs("BLOODGROUP") Is DBNull.Value, drOtherDocs("BLOODGROUP"), -1)
                            If Not drOtherDocs("NatureofBody") Is DBNull.Value Then
                                Dim dr() = drOtherDocs("NatureOfBody").ToString.Split(",")
                                For i = 0 To dr.Length - 1
                                    If dr(i).ToString = "Kaff" Then
                                        chckKaff.Checked = True
                                    ElseIf dr(i).ToString = "Pitta" Then
                                        chckPitta.Checked = True
                                    ElseIf dr(i).ToString = "Vaat" Then
                                        chckVaat.Checked = True
                                    End If
                                Next
                            End If



                        Next
                    End If
                    Me.Update()
                    Me.Refresh()

                Else
                    ClearFields(Me)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Function CheckUniqueMobileNumber(ByVal mobilenumber As String, Optional ByVal cardno As String = "") As Boolean
        If uniquemblno <> txtMobile.Text.Trim Then
            PatientId = ""
        End If
        If objCustm.CheckMobileNoUnique(mobilenumber, clsAdmin.SiteCode, cardno) = False Then
            Return True
        End If
    End Function
    Public Sub HideAllButtons()
        BtnSearchPatient.Visible = False
        BtnNewRegn.Visible = False
        BtnEditRegn.Visible = False
        BtnDeleteRegn.Visible = False

        BtnUploadImage.Visible = False
        BtnClearImage.Visible = False

        BtnNewRefDr.Visible = False
        BtnSearchRefDr.Visible = False

        BtnNewDoc.Visible = False
        BtnDeleteDoc.Visible = False
        BtnAddDoc.Visible = False
        BtnUploadDoc.Visible = False
        BtnSearchAuthor.Visible = False

        chkCopyAddress.Visible = False
    End Sub

#End Region

    Private Sub cmbPStayMonths_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles cmbPStayMonths.PreviewKeyDown
        If e.KeyCode = Keys.Tab Then
            TabPagePatientInfo.Show()
            groupboxContactDetails.Select()
            'txtMobile.Select()
            'txtMobile.Focus()
        End If
    End Sub

    Private Sub BtnClearImage_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles BtnClearImage.PreviewKeyDown

        If e.KeyCode = Keys.Tab Then
            TabPageAddressInfo.Show()
            GroupBoxLocalAddress.Select()
        End If

    End Sub
    Private Sub txtEmail_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtEmail.Validating
        If txtEmail.Value <> "" Then
            If emailaddresscheck(txtEmail.Text) Then
                txtEmail.BackColor = Color.White
            Else
                e.Cancel = True
                txtEmail.ErrorInfo.ErrorMessage = "Please provide correct email ID"
                txtEmail.ErrorInfo.ErrorMessageCaption = "Error"
                txtEmail.BackColor = Color.Gray
            End If
        Else
            txtEmail.BackColor = Color.White
        End If
    End Sub
    Private Sub dtpDocDate_TextChanged(sender As Object, e As EventArgs) Handles dtpDocDate.TextChanged

        Dim Docdate As New DateTime
        Docdate = dtpDocDate.Text
        If (Docdate > Now) Then
            ShowMessage("Document date can not be future date", "Information")
        End If
    End Sub

    Private Sub dtpDob_Leave(sender As System.Object, e As System.EventArgs) Handles dtpDob.Leave
        If (dtpDob.Value IsNot DBNull.Value) Then
            Dim dt As Date
            dt = dtpDob.Value
            If dt.Date <= Now.Date Then
                Dim AgeCal As New clsAgeCalculator(dtpDob.Value, Now)
                cmbAgeYears.Text = AgeCal.Years
                cmbAgeMonths.Text = AgeCal.Months
            Else
                ShowMessage("Date of birth cannot be future date", "Date of Birth")
                dtpDob.Value = Now.Date
                cmbAgeYears.Text = String.Empty
                cmbAgeMonths.Text = String.Empty
            End If
        End If
    End Sub
End Class

