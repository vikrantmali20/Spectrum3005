Imports System.IO
Imports SpectrumBL

Public Class CtrlPatientDtls
    'Private vPatientId As String
    Private clsPatient As New clsHcPatientInfo
    Private clsComn As New clsHcCommon
    Public dsPatientDetails As DataSet
    Private dsPatienttrnsDetails As DataSet
    Private SqldtDistinct As DataTable
    Private dtTreeView As DataTable
    Private dtCustdata As New DataTable
    Private clsClinical As New clsHcClinicalHistory
    Private AddNodeM As TreeNode
    Private AddNodeP As TreeNode
    Private AddNodeC As TreeNode

    Private _HideFileList As Boolean
    Public Property HideFileList() As Boolean
        Get
            Return _HideFileList
        End Get
        Set(ByVal value As Boolean)
            _HideFileList = value
            If value = False Then
                Me.CtrlFileList.Visible = _HideFileList
                Me.TreeViewPatient.Visible = _HideFileList
                Me.TableLayoutPanel1.ColumnStyles(7).SizeType = SizeType.Percent
                Me.TableLayoutPanel1.ColumnStyles(7).Width = 2
                Me.TableLayoutPanel1.SetColumnSpan(CtrlPatientImage1, 2)
            Else
                Me.TableLayoutPanel1.SetColumnSpan(CtrlPatientImage1, 1)
                Me.CtrlFileList.Visible = _HideFileList
                Me.TreeViewPatient.Visible = _HideFileList
                Me.TableLayoutPanel1.SetColumn(CtrlFileList, 7)
                Me.TableLayoutPanel1.SetColumnSpan(CtrlFileList, 1)
                Me.TableLayoutPanel1.ColumnStyles(7).SizeType = SizeType.Percent
                Me.TableLayoutPanel1.ColumnStyles(7).Width = Me.TableLayoutPanel1.ColumnStyles(6).Width

            End If
        End Set
    End Property

    Private _DoctorCode As String = String.Empty
    Public Property DoctorCode() As String
        Get
            Return _DoctorCode
        End Get
        Set(ByVal value As String)
            _DoctorCode = value
        End Set
    End Property
    Private _vPatientId As String = String.Empty
    Public Property vPatientId() As String
        Get
            Return _vPatientId
        End Get
        Set(ByVal value As String)
            _vPatientId = value
        End Set
    End Property

    Private Sub BtnSearchPatient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchPatient.Click
        Try
            dsPatientDetails = New DataSet
            Dim dtPatientDetails As New DataTable
            Dim dtPatientSearch As New DataTable
            'dsPatientDetails = clsPatient.GetPatientInfo(String.Empty, clsAdmin.SiteCode, True)
            dtPatientSearch = clsPatient.GetPatientInfoSchema(String.Empty, clsAdmin.SiteCode)

            Dim objPatient As New frmHcCommonView
            'objPatient.SetDataTable = dsPatientDetails.Tables("HcPatientDetail")
            'objPatient.SetCaptionText = "Patient Information"
            'objPatient.DisplayColumns = "PatientId, FirstName, LastName, AddressLine1, AddressLine2, City, Pincode"
            'objPatient.SetDataTable = dsPatientDetails.Tables("clpCustomers")
            objPatient.SetDataTable = dtPatientSearch
            objPatient.SetCaptionText = "Patient Information"
            objPatient.DisplayColumns = "CardNo, MobileNo,FirstName, SurName, AddressLn1, AddressLn2, City, Pincode"
            If (objPatient.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                Dim drPatient As DataRow = objPatient.GetResultRow
                'RefreshPatientInfo(dsPatientDetails, drPatient("PatientId"))
                ' RefreshPatientInfo(dsPatientDetails, drPatient("CardNo"))
                dtCustdata = clsPatient.GetPatientInfoSchema(drPatient("CardNo"), clsAdmin.SiteCode)
                RefreshPatientInfo(dsPatientDetails, drPatient("CardNo"), dtCustdata)


            End If

        Catch ex As Exception
            MessageBox.Show("GetPatientDetails:" & vbCrLf & ex.Message)
        End Try
    End Sub
    Private Sub BtnMoreInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnMoreInfo.Click
        If (String.IsNullOrEmpty(txtPatientName.Text.Trim) = False) Then

            Dim ChildForm As New Spectrum.frmHCPatientRegistration
            ChildForm.PatientId = txtPatientID.Text.Trim
            ChildForm.Text = "Patient Details"
            ChildForm.C1Label44.Visible = False
            ChildForm.HideAllButtons()

            'ChildForm.BtnSearchPatient.Visible = False
            'ChildForm.BtnNewRegn.Visible = False
            'ChildForm.BtnEditRegn.Visible = False
            'ChildForm.BtnDeleteRegn.Visible = False

            'ChildForm.BtnUploadImage.Visible = False
            'ChildForm.BtnClearImage.Visible = False

            'ChildForm.BtnNewRefDr.Visible = False
            'ChildForm.BtnSearchRefDr.Visible = False

            'ChildForm.BtnDocNew.Visible = False
            'ChildForm.BtnDeleteDoc.Visible = False
            'ChildForm.BtnAddDoc.Visible = False
            'ChildForm.BtnUploadDoc.Visible = False

            ChildForm.ShowDialog()
        Else
            ShowMessage("Please select a patient", "More Info")
        End If

    End Sub

    Public Sub txtPatientID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPatientID.KeyDown
        If (e.KeyCode = Keys.Enter AndAlso String.IsNullOrEmpty(txtPatientID.Text.Trim) = False) Then
            GetPatientDetails(txtPatientID.Text.Trim)
        End If
    End Sub

    Public Sub RefreshPatientInfo(ByVal dsInfo As DataSet, ByVal PatientId As String, Optional ByRef dtCust As DataTable = Nothing)
        Try
            If Not (dsPatientDetails Is Nothing) Then
                If (Not dtCust Is Nothing AndAlso dtCust.Rows.Count > 0) Then ' khusrao added


                    'If (dsPatientDetails.Tables("HcPatientDetail").Rows.Count > 0) Then
                    'If (dsPatientDetails.Tables("clpCustomers").Rows.Count > 0) Then
                    If (dtCust.Rows.Count > 0) Then
                        ' Dim drPatientInfoTp As DataRow() = dsPatientDetails.Tables("clpCustomers").Select("CardNo='" & PatientId & "'")
                        Dim drPatientInfo As DataRow = dtCust.Select("CardNo='" & PatientId & "'")(0) ' khusrao added
                        'Dim drPatientInfo As DataRow
                        'If (drPatientInfoTp Is Nothing Or drPatientInfoTp Is DBNull.Value) Then
                        '    Exit Sub
                        'Else
                        '    drPatientInfo = drPatientInfoTp(0)
                        'End If
                        '_DoctorCode = IIf(IsDBNull(drPatientInfo("RefDoctorCode")), String.Empty, drPatientInfo("RefDoctorCode"))
                        txtPatientID.Value = drPatientInfo("CardNo")
                        txtPatientName.Value = IIf(IsDBNull(drPatientInfo("FirstName") + " " + drPatientInfo("SurName")), String.Empty, (drPatientInfo("FirstName") + " " + drPatientInfo("SurName")))
                        txtReferedByDr.Value = IIf(IsDBNull(drPatientInfo("RefDoctorId")), String.Empty, drPatientInfo("RefDoctorId"))
                        txtPrimaryPhone.Value = IIf(IsDBNull(drPatientInfo("PrimaryTelphone")), String.Empty, drPatientInfo("PrimaryTelphone"))
                        txtCity.Value = IIf(IsDBNull(drPatientInfo("City")), String.Empty, drPatientInfo("City"))

                        txtGender.Value = IIf(IsDBNull(drPatientInfo("Gender")), String.Empty, drPatientInfo("Gender"))
                        txtWeight.Value = IIf(IsDBNull(drPatientInfo("WeightKg")), String.Empty, drPatientInfo("WeightKg"))
                        txtHeight.Value = IIf(IsDBNull(drPatientInfo("HeightCM")), String.Empty, drPatientInfo("HeightCM"))
                        txtAge.Value = IIf(IsDBNull(drPatientInfo("Age")), String.Empty, drPatientInfo("Age"))
                        txtMobileNo.Value = IIf(IsDBNull(drPatientInfo("MobileNo")), String.Empty, drPatientInfo("MobileNo"))

                        Dim BytesImage As Byte() = IIf(IsDBNull(drPatientInfo("PatientImage")), Nothing, drPatientInfo("PatientImage"))
                        If BytesImage IsNot Nothing Then
                            If BytesImage.Length <> 0 AndAlso BytesImage.Count <> 0 Then
                                CtrlPatientImage1.PicBoxImages.Image = clsComn.ByteArrayToImage(BytesImage)
                                CtrlPatientImage1.PicBoxImages.SizeMode = PictureBoxSizeMode.StretchImage
                            End If
                        End If
                        PopulateTreeView()

                        Me.Update()
                        Me.Refresh()
                    Else
                        ClearAllInputFields()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("RefreshPatientInfo :" & vbCrLf & ex.Message)
        End Try
    End Sub

    Public Sub ClearAllInputFields()

        txtPatientName.Value = Nothing
        txtReferedByDr.Value = Nothing
        txtPrimaryPhone.Value = Nothing
        txtCity.Value = Nothing

        txtGender.Value = Nothing
        txtWeight.Value = Nothing
        txtHeight.Value = Nothing
        txtAge.Value = Nothing
        txtMobileNo.Value = Nothing

        CtrlPatientImage1.PicBoxImages.Image = Nothing
        TreeViewPatient.Refresh()
        _DoctorCode = String.Empty

        txtPatientID.Select()
    End Sub
    Public Sub GetPatientDetails(ByVal PatientID As String)
        Try
            dsPatientDetails = New DataSet
            dsPatientDetails = clsPatient.GetPatientInfo(PatientID, clsAdmin.SiteCode, True)
            'dsPatientDetails = clsPatient.GetPatientInfo(txtPatientID.Text.Trim, clsAdmin.SiteCode, True)
            dtCustdata = New DataTable
            dtCustdata = clsPatient.GetPatientInfoSchema(PatientID, clsAdmin.SiteCode)
            RefreshPatientInfo(dsPatientDetails, PatientID, dtCustdata)

        Catch ex As Exception
            MessageBox.Show("GetPatientDetails:" & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub PopulateTreeView()
        Try
            dtTreeView = New DataTable
            dtTreeView = clsPatient.GetPatientDocInfo(txtPatientID.Text.Trim, clsAdmin.SiteCode)

            TreeViewPatient.Nodes.Clear()

            If (dtTreeView.Rows.Count > 0) Then
                AddNodeM = New TreeNode
                AddNodeM.Name = "MainNode"
                AddNodeM.Text = "Patient Docs"
                TreeViewPatient.Nodes.Add(AddNodeM)

                SqldtDistinct = dtTreeView.DefaultView.ToTable(True, "ParentNode")

                For Each drNodeParent As DataRow In SqldtDistinct.Rows
                    AddNodeP = New TreeNode
                    AddNodeP.Name = drNodeParent("ParentNode")
                    AddNodeP.Text = drNodeParent("ParentNode")

                    TreeViewPatient.SelectedNode = AddNodeM
                    TreeViewPatient.SelectedNode.Nodes.Add(AddNodeP)

                    For Each drNodeChild As DataRow In dtTreeView.Select("ParentNode= '" & drNodeParent("ParentNode") & "'")
                        AddNodeC = New TreeNode
                        AddNodeC.Name = drNodeChild("ParentNode")
                        AddNodeC.Text = drNodeChild("ChildNode")
                        AddNodeC.Tag = drNodeChild("DocPath")

                        TreeViewPatient.SelectedNode = AddNodeP
                        TreeViewPatient.SelectedNode.Nodes.Add(AddNodeC)
                    Next
                Next

                TreeViewPatient.ExpandAll()
            End If

        Catch ex As Exception
            MessageBox.Show("PopulateTreeViewInfo :" & vbCrLf & ex.Message)
        End Try
    End Sub
    Private Sub TreeViewPatient_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeViewPatient.DoubleClick
        Try
            If Not (TreeViewPatient.SelectedNode.Tag = Nothing) Then
                Dim Proc As New System.Diagnostics.Process
                Proc.StartInfo.FileName = TreeViewPatient.SelectedNode.Tag
                Proc.Start()
            End If
        Catch ex As Exception
            MessageBox.Show("" & vbCrLf & ex.Message)
        End Try

    End Sub

    Private Sub BtnClinicalHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClinicalHistory.Click
        If Not (txtPatientID.Text = String.Empty) Then

            Dim objClinicalHistory As New frmHCClinicalHistory()
            objClinicalHistory.SetPatientID = txtPatientID.Text.Trim
            'dsPatientHcTrnDetails = clsPatient.GetPatientHcTrnDetails(txtPatientID.Text.Trim, clsAdmin.SiteCode)
            'dsPatientHcTrnDetails=clsClinical.GetClinicalHistory(

            dsPatientHcTrnDetails = clsClinical.GetClinicalHistory(clsClinical.GetClinicalHistId(txtPatientID.Text.Trim, clsAdmin.SiteCode), txtPatientID.Text.Trim, clsAdmin.SiteCode)
            Dim HID = objClinicalHistory.ShowDialog()
        Else
            ShowMessage("Please select a patient", "More Info")
        End If
    End Sub



    Public Sub CtrlPatientDtls_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If vPatientId <> String.Empty AndAlso vPatientId <> Nothing Then
        '    dsPatientDetails = clsPatient.GetPatientInfo(vPatientId, clsAdmin.SiteCode, True)
        '    RefreshPatientInfo(dsPatientDetails, vPatientId)
        'End If

        

    End Sub
End Class
