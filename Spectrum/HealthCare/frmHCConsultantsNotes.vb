Imports System.Drawing
Imports SpectrumBL
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Stepi.UI
Imports Microsoft.Reporting.WebForms

Public Class frmHCConsultantsNotes
    Public dtPrescDtlAddToBill As New DataTable
    Private dtCustdata As New DataTable
    Private clsConsNotes As New clsHcConsultantsNotes
    Private clsComn As New clsHcCommon
    Private dsConsNote As DataSet
    Private drConsNote As DataRow()
    Private dt As DataTable

    Private intCol1Width As Integer = 0
    Private intCol2Width As Integer = 0

    Private vConsultantNoteID As String = String.Empty
    'Private vPatientID As String = String.Empty
    Public vPatientID As String = String.Empty
    Private vDrNoteID As String = String.Empty
    Private vNoteSrNo As Integer = 0
    Private vPrescriptionSrNo As Integer = 0 ' need to ask about it
    Private IsNewDrNotes As Boolean = False
    Private IsSavePrescription As Boolean = False
    Public vDoctorType As String = String.Empty
    Private vDoctorCode As String = String.Empty

    Private BindMb As BindingManagerBase
    Private CurrentSession As Integer = 0
    Private IsAppLoaded As Boolean = False
    Dim gsizeWith As Integer
    Dim gsizeHight As Integer
    Dim gLocationX As Integer
    Dim gLocationY As Integer
    Dim C_date As DateTime = Now
    Dim inc As Integer = 0
    Dim indxs As Integer


    Dim DtPrescHdr As New DataTable
    Dim DtPrescDtl As New DataTable
    Dim DtPrescriptionDtlNotes As New DataTable
    Dim DtPatientPrescPrevious As New DataTable
    Dim ChangedConsultantNote As String = ""
#Region "Functions"
    'CheckAuthorisationForTran
    Public Function GetLatestPescriptionDetailsOfPatient(ByVal patientId) As DataTable
        Dim clsHcPatientLatest As New clsHcPatientInfo
        Dim dt = clsHcPatientLatest.GetLatestPrescriptionOfPatient(patientId, clsAdmin.SiteCode)
        Return dt
    End Function

    Public Function BindConsultantNotesAndDetailsOfPatient(ByVal cPatientId As String) As Boolean
        Try
             Dim clsHcPatient As New clsHcPatientInfo
            CtrlPatientInfo.vPatientId = cPatientId
            CtrlPatientInfo.dsPatientDetails = clsHcPatient.GetPatientInfo(vPatientID, clsAdmin.SiteCode, True)
            dtCustdata = clsHcPatient.GetPatientInfoSchema(vPatientID, clsAdmin.SiteCode)
            CtrlPatientInfo.RefreshPatientInfo(CtrlPatientInfo.dsPatientDetails, vPatientID, dtCustdata)
            GetConsultantsNotesInfo()

        Catch ex As Exception
            ShowMessage("GetConsultantsNotesInfo :" & vbCrLf & ex.Message, "Error")
        End Try

    End Function

    Private Function PrepareConsultantNoteInfo() As Boolean
        Try
            If CtrlPatientInfo.txtPatientID.Text.Trim <> String.Empty Then
                If (String.IsNullOrEmpty(rtextTodSessionNotes.Text.Trim)) Then
                    'MessageBox.Show("Please enter some session notes", "Consultants Notes", MessageBoxButtons.OK)
                    ShowMessage("Please enter some session notes", "Consultants Notes")
                    Return False
                    Exit Function
                End If

                Dim drClinicalHistory As DataRow
                Dim filterRow As String = String.Empty
                filterRow = "CurrentDate='" & Now.Date.ToString("yyyyMMdd") & "'"
                drConsNote = dsConsNote.Tables("HcTrnConsultantsNote").Select(filterRow, Nothing)
                indxs = dsConsNote.Tables("HcTrnConsultantsNote").Rows().Count
                'If (drConsNote.Length = 0) Then

                vDrNoteID = clsComn.GetNextDocNo("Consultants Notes")

                drClinicalHistory = dsConsNote.Tables("HcTrnConsultantsNote").NewRow

                drClinicalHistory("ConsultantsNoteId") = vDrNoteID
                drClinicalHistory("PatientId") = vPatientID
                drClinicalHistory("SiteCode") = clsAdmin.SiteCode
                drClinicalHistory("NoteSrNo") = vNoteSrNo + 1
                drClinicalHistory("DoctorType") = vDoctorType
                drClinicalHistory("DoctorCode") = vDoctorCode
                drClinicalHistory("ConsultantsNoteText") = rtextTodSessionNotes.Text
                drClinicalHistory("RecStatus") = True
                drClinicalHistory("CreatedBy") = clsAdmin.UserName
                drClinicalHistory("CreatedOn") = DateTime.Now
                drClinicalHistory("UpdatedBy") = clsAdmin.UserName
                drClinicalHistory("UpdatedOn") = DateTime.Now
                dsConsNote.Tables("HcTrnConsultantsNote").Rows.Add(drClinicalHistory)
                Return True
            Else
                ShowMessage("Please select a patient", "Save Notes")
            End If

        Catch ex As Exception
            'MessageBox.Show("PrepareConsultantNoteInfo :" & vbCrLf & ex.Message)
            ShowMessage("PrepareConsultantNoteInfo :" & vbCrLf & ex.Message, "Save Notes")
            Return False
        End Try
    End Function
#End Region

#Region "Sub"
    Private Sub GetConsultantsNotesInfo()
        Try
            rtextTodSessionNotes.Clear()
            rtxtPrevSessionNotes.Clear()
            BtnPreviousSession.Enabled = True
            BtnNextSession.Enabled = True
            If (String.IsNullOrEmpty(CtrlPatientInfo.txtPatientName.Text) Or String.IsNullOrEmpty(CtrlPatientInfo.txtPatientName.Value)) Then Exit Sub

            vPatientID = CtrlPatientInfo.txtPatientID.Text.Trim
            vDoctorCode = CtrlPatientInfo.DoctorCode

            dsConsNote = New DataSet
            dsConsNote = clsConsNotes.GetDrNotesInfo(vPatientID, vDoctorType)
            If dgArticleGridPrevious.Rows.Count > 1 Then
                dgArticleGridPrevious.Rows.RemoveRange(1, dgArticleGridPrevious.Rows.Count - 1)
            End If
            If dgGridArticleToday.Rows.Count > 1 Then
                dgGridArticleToday.Rows.RemoveRange(1, dgGridArticleToday.Rows.Count - 1)
            End If
            vNoteSrNo = 0
            rtextTodSessionNotes.Clear()
            rtxtPrevSessionNotes.Clear()

            If (dsConsNote.Tables("HcTrnConsultantsNote").Rows.Count > 0) Then

                'C1SizerNotes.Grid.Columns(0).Size = 0
                Dim filterRow As String = String.Empty
                filterRow = "CurrentDate='" & Now.Date.ToString("ddMMyyyyhhmm") & "'"
                drConsNote = dsConsNote.Tables("HcTrnConsultantsNote").Select(filterRow, Nothing)

                If (drConsNote.Length > 0) Then
                    vNoteSrNo = drConsNote(0)("NoteSrNo")
                    vDrNoteID = drConsNote(0)("ConsultantsNoteId")
                    rtextTodSessionNotes.Text = drConsNote(0)("ConsultantsNoteText")
                    IsNewDrNotes = False
                Else
                    vNoteSrNo = dsConsNote.Tables("HcTrnConsultantsNote").Compute("Max(NoteSrNo)", String.Empty)
                    IsNewDrNotes = True
                    vDrNoteID = String.Empty
                    rtextTodSessionNotes.Text = String.Empty
                End If
                BindMb = Nothing
                If (IsAppLoaded = True) Then
                    lblPrevSessionDate.DataBindings.RemoveAt(0)
                    rtxtPrevSessionNotes.DataBindings.RemoveAt(0)
                End If
                IsAppLoaded = True
                lblPrevSessionDate.DataBindings.Add("Text", dsConsNote, "HcTrnConsultantsNote.CurrentDate")
                rtxtPrevSessionNotes.DataBindings.Add("Text", dsConsNote, "HcTrnConsultantsNote.ConsultantsNoteText")
                If rtextTodSessionNotes.Text <> String.Empty Then
                    rtextTodSessionNotes.Clear()
                End If
                BindMb = Me.BindingContext(dsConsNote, "HcTrnConsultantsNote")
                BindMb.Position = 0
                txtTotalSession.Value = BindMb.Count & "/" & BindMb.Count
            Else
                IsNewDrNotes = True
                txtTotalSession.Value = "0/0"
                lblPrevSessionDate.Text = String.Empty
                rtxtPrevSessionNotes.Text = String.Empty
            End If
            If vNoteSrNo > 0 Then
                DtPatientPrescPrevious.Clear()
                DtPatientPrescPrevious = clsConsNotes.GetPatientPrescriptionInfo(vPatientID, vNoteSrNo)
                BindArticelGridPByNoteId(DtPatientPrescPrevious)

            End If
        Catch ex As Exception

            ShowMessage("GetConsultantsNotesInfo :" & vbCrLf & ex.Message, "Error")
        End Try
    End Sub

    Public Sub BindArticelGridPByNoteId(ByVal dt As DataTable)
        Try
            If dgArticleGridPrevious.Rows.Count > 1 Then
                dgArticleGridPrevious.Rows.RemoveRange(1, dgArticleGridPrevious.Rows.Count - 1)
            End If
            If dt.Rows.Count > 0 Then
                'btnCopyPrescriptionToCurrent.Enabled = True
                btnCopyPrescriptionToCurrent.Enabled = False
            Else
                btnCopyPrescriptionToCurrent.Enabled = False
            End If
            For i = 0 To dt.Rows.Count - 1
                dgArticleGridPrevious.Rows.Add()
                dgArticleGridPrevious.Rows(dgArticleGridPrevious.Rows.Count - 1)("SrNo") = dgArticleGridPrevious.Rows.Count - 1
                dgArticleGridPrevious.Rows(dgArticleGridPrevious.Rows.Count - 1)("ArticleCode") = dt.Rows(i)("ArticleCode").ToString()
                dgArticleGridPrevious.Rows(dgArticleGridPrevious.Rows.Count - 1)("ArticleDescription") = dt.Rows(i)("ArticleDescription").ToString()
                dgArticleGridPrevious.Rows(dgArticleGridPrevious.Rows.Count - 1)("Quantity") = dt.Rows(i)("Qty")
                dgArticleGridPrevious.Rows(dgArticleGridPrevious.Rows.Count - 1)("ConsumptionRate") = dt.Rows(i)("ConsumptionRate").ToString()
                dgArticleGridPrevious.Rows(dgArticleGridPrevious.Rows.Count - 1)("Duration") = dt.Rows(i)("Duration").ToString()
                dgArticleGridPrevious.Rows(dgArticleGridPrevious.Rows.Count - 1)("Remarks") = dt.Rows(i)("Remarks").ToString()
                If (dgArticleGridPrevious.Rows.Count > 1) Then
                    dgArticleGridPrevious.Select(dgArticleGridPrevious.Rows.Count - 1, 2)
                End If
                dgArticleGridPrevious.AllowEditing = False
            Next
        Catch ex As Exception
            ShowMessage("GetConsultantsNotesInfo :" & vbCrLf & ex.Message, "Error")
        End Try
    End Sub
#End Region
#Region "Page Event"


    Private Sub frmHCConsultantsNotes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim vDate As String = Now.ToString("MMM/dd/yyyy hh:mm tt")
        GetGroupBoxDetails()
        Try
            AddHandler CtrlPatientInfo.txtPatientName.TextChanged, AddressOf GetConsultantsNotesInfo
            CtrlPatientInfo.TreeViewPatient.Visible = True
            'BtnNextSession.Enabled = False
          
        Catch ex As Exception

            ShowMessage("frmHCConsultantsNotes :" & vbCrLf & ex.Message, "Error")
        End Try
        SplitContainer.Size = New System.Drawing.Size(gsizeWith - 20, gsizeHight - 68)
        C1HandlingPreviousNextSizer.Size = New System.Drawing.Size(SplitContainer.Size.Width, 35)
        C1HandlingPreviousNextSizer.Grid.Columns(0).Size = 0
        dtAppDate.Value = C_date
        'dtpDocApp.Value = C_date
        'If dtAppDate.Text = String.Empty AndAlso clsAdmin.EmployeeID <> String.Empty Then

        '     dt = clsConsNotes.GetTo_dayDrAppointmentInfo(clsAdmin.EmployeeID, Now.Date.ToString("yyyyMMdd"))
        '     grdDrAppInfo.DataSource = dt

        ' End If
        'dtAppDate_TextChanged(String.Empty, EventArgs.Empty)
        'ExtendedPPatient.State = ExtendedPPatient.State.Collapsed
        'Dim gSine As Integer = Convert.ToInt32(GroupBox2.Size)
        'Me.StartPosition = StartPosition.CenterScreen
        Dim objpc As New clsSalesOrder
        Call objpc.GetSOBulkComboTablestructure(DtPrescHdr, DtPrescDtl)
        'Call objpc.GetPatientPrescriptionTableStructure(DtPrescriptionDtlNotes)
        Call objpc.GetPatientPrescriptionTableStructure(dtPrescDtlAddToBill)
        If vPatientID <> String.Empty AndAlso vPatientID <> Nothing Then
            vDoctorType = "Doctor" '' for temprary purpos
            Call BindConsultantNotesAndDetailsOfPatient(vPatientID)
            Dim dt As New DataTable
            dt = GetLatestPescriptionDetailsOfPatient(vPatientID)

            BindPrescriptionTodayGrid(dt)
        End If
        CtrlPatientInfo.BtnSearchPatient.Enabled = False
        CtrlPatientInfo.BtnMoreInfo.Enabled = False
        If CheckAuthorisationForTran(clsAdmin.SiteCode, "ConsultantNotes") = True Then
            btnCopyPrescriptionToCurrent.Visible = True
            btnAddCurrentPrescription.Visible = True
            BtnSaveConsultantNote.Visible = True

        Else
            btnCopyPrescriptionToCurrent.Visible = False
            btnAddCurrentPrescription.Visible = False
            BtnSaveConsultantNote.Visible = False
            'rtextTodSessionNotes.Visible = False
            rtextTodSessionNotes.ReadOnly = True
        End If
    End Sub
#End Region




   


    Private Sub BtnSaveConsultantNote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveConsultantNote.Click
        Try
            If (PrepareConsultantNoteInfo() = False) Then
                Exit Sub
            ElseIf PrepareCustomerPrescriptionInfo() = False Then
                Exit Sub
            End If
            If DtPrescDtl.Rows.Count > 0 Then

            End If
            If (clsComn.PrepareSaveData(dsConsNote, "Consultants Notes", IsNewDrNotes)) Then
                ShowMessage("Consultants Notes Record : Saved Successfully", "Consultants Notes")
                IsSavePrescription = True
                ' ReInitializeControls()
                '  vPatientID = ""

            Else
                ShowMessage("Save failed", "Consultants Notes")
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            MessageBox.Show("BtnSaveConsultantNoteInfo :" & vbCrLf & ex.Message)
        End Try
    End Sub

  
    ' created by Khusrao Adil
    Public Function PrepareCustomerPrescriptionInfo() As Boolean
        Try
            PrepareCustomerPrescriptionInfo = False
            Dim IsGridBlank As Boolean = False
            Dim drCutomerPrescription As DataRow
            If dgGridArticleToday.Rows.Count = 500 Then
                dgGridArticleToday.Rows.Count = 1
            Else

            End If
            ' For gridIndex = 1 To grdSymptoms.Rows.Count - 1
            For gridIndex = 1 To dgGridArticleToday.Rows.Count - 1
                If dsConsNote.Tables.Contains("HCPrescriptionDtls") = True Then

                    drCutomerPrescription = dsConsNote.Tables("HCPrescriptionDtls").NewRow
                    vPrescriptionSrNo = vPrescriptionSrNo + 1

                Else
                    Dim _dt As New DataTable
                    _dt = clsComn.GetPrescriptionDtlsStuc()
                    dsConsNote.Tables.Add(_dt)
                    drCutomerPrescription = dsConsNote.Tables("HCPrescriptionDtls").NewRow
                End If




                '  Dim x As Integer
                ' x = dgGridArticleToday.Rows(gridIndex)("ArticleCode").Value

                'vDrNoteID = clsComn.GetNextDocNo("Consultants Notes")
                drCutomerPrescription("ArticleCode") = dgGridArticleToday.Rows(gridIndex)("ArticleCode").ToString()
                drCutomerPrescription("ArticleDescription") = dgGridArticleToday.Rows(gridIndex)("ArticleDescription").ToString()
                'drCutomerPrescription("EAN") = vDrNoteID
                drCutomerPrescription("Qty") = dgGridArticleToday.Rows(gridIndex)("Quantity").ToString()
                drCutomerPrescription("ConsumptionRate") = dgGridArticleToday.Rows(gridIndex)("ConsumptionRate").ToString()
                drCutomerPrescription("Duration") = dgGridArticleToday.Rows(gridIndex)("Duration").ToString()
                drCutomerPrescription("Remarks") = dgGridArticleToday.Rows(gridIndex)("Remarks").ToString()
                drCutomerPrescription("ConsultantsNoteId") = vDrNoteID
                drCutomerPrescription("PatientId") = vPatientID
                drCutomerPrescription("SiteCode") = clsAdmin.SiteCode
                drCutomerPrescription("NoteSrNo") = vNoteSrNo + 1
                drCutomerPrescription("CreatedBy") = clsAdmin.UserName
                drCutomerPrescription("CreatedOn") = DateTime.Now
                drCutomerPrescription("UpdatedBy") = clsAdmin.UserName
                drCutomerPrescription("UpdatedOn") = DateTime.Now
                drCutomerPrescription("Status") = True
                dsConsNote.Tables("HCPrescriptionDtls").Rows.Add(drCutomerPrescription)


            Next

            Return True
        Catch ex As Exception
            ShowMessage("PrepareCustomerPrescriptionInfo :" & vbCrLf & ex.Message, "Save Prescription")
            Return False
        End Try

    End Function

    Private Function ReInitializeControls() As Boolean
        Try
            vNoteSrNo = 0
            vDrNoteID = String.Empty

            rtextTodSessionNotes.Clear()
            rtxtPrevSessionNotes.Clear()

            CtrlPatientInfo.CtrlPatientImage1.PicBoxImages.Image = Nothing
            CtrlPatientInfo.TreeViewPatient.Nodes.Clear()

            For Each txtInfo As Control In CtrlPatientInfo.TableLayoutPanel1.Controls
                If (TypeOf txtInfo Is CtrlTextBox) AndAlso Not (String.IsNullOrEmpty(DirectCast(txtInfo, CtrlTextBox).Text)) Then
                    DirectCast(txtInfo, CtrlTextBox).Value = String.Empty
                End If
            Next

            lblPrevSessionDate.Text = String.Empty
            LblTodaySessionNotes.Text = String.Empty
            txtTotalSession.Value = String.Empty
            DtPatientPrescPrevious.Clear()
            dgArticleGridPrevious.Clear()
            DtPatientPrescPrevious.AcceptChanges()
            dgGridArticleToday.Rows.RemoveRange(1, dgGridArticleToday.Rows.Count - 1)
            dgArticleGridPrevious.Rows.RemoveRange(1, dgGridArticleToday.Rows.Count - 1)
            'dgArticleGridPrevious.DataSource = DtPatientPrescPrevious
            'For Each row As DataRow In dgArticleGridPrevious.Rows
            '    dgArticleGridPrevious.Rows.Remove(row)
            'Next
            'If dgArticleGridPrevious.Rows.Count > 0 Then


            '    If dgArticleGridPrevious.Rows.Count = 1 Then
            '        dgArticleGridPrevious.Rows.Remove(dgArticleGridPrevious.Rows.Count)
            '    Else
            '        dgArticleGridPrevious.Rows.RemoveRange(1, dgGridArticleToday.Rows.Count - 1)
            '    End If
            'End If


            btnCopyPrescriptionToCurrent.Enabled = False

        Catch ex As Exception

            ShowMessage("ReInitializeControls : " & vbCrLf & ex.Message, "Error")
        End Try
    End Function

    Private Sub BtnCloseConsultantNote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCloseConsultantNote.Click
        Me.Close()
    End Sub
    Private Sub BtnCollapse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCollapse.Click
        C1HandlingPreviousNextSizer.Size = New System.Drawing.Size(SplitContainer.Size.Width, 35)
        If BtnCollapse.Tag = "<" Then

            intCol1Width = C1HandlingPreviousNextSizer.Grid.Columns(0).Size
            intCol2Width = C1HandlingPreviousNextSizer.Grid.Columns(1).Size

            C1HandlingPreviousNextSizer.Grid.Columns(1).Size = intCol1Width + intCol2Width
            C1HandlingPreviousNextSizer.Grid.Columns(0).Size = 0

            SplitContainer.Panel1Collapsed = True
            BtnCollapse.Tag = ">"
            BtnCollapse.Text = ">"

        Else
            C1HandlingPreviousNextSizer.Grid.Columns(1).Size = C1HandlingPreviousNextSizer.Size.Width / 2
            C1HandlingPreviousNextSizer.Grid.Columns(0).Size = C1HandlingPreviousNextSizer.Size.Width / 2

            SplitContainer.Panel1Collapsed = False
            SplitContainer.SplitterDistance = SplitContainer.Width / 2
            BtnCollapse.Tag = "<"
            BtnCollapse.Text = "<"

            BtnPreviousSession.Enabled = True
            BtnNextSession.Enabled = True

        End If
    End Sub

    Private Sub BtnPreviousSession_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPreviousSession.Click

        Dim CS As Integer
        If (BindMb IsNot Nothing AndAlso BindMb.Count > 0) Then
            CurrentSession = BindMb.Position
            If BindMb.Count <> 1 Then
                CS = BindMb.Count - (CurrentSession + 1)
            Else
                CS = BindMb.Count
            End If
            If CS Then

                txtTotalSession.Value = CS & "/" & BindMb.Count
                BindMb.Position += 1

                If CS = 1 Then
                    BtnPreviousSession.Enabled = False
                    BtnNextSession.Enabled = True
                Else
                    BtnNextSession.Enabled = True
                End If
            End If
        End If
        If CS <> 0 Then
            Dim _dt As DataTable = clsConsNotes.GetPatientPrescriptionInfo(vPatientID, CS)
            If _dt.Rows.Count > 0 Then
                If dgArticleGridPrevious.Rows.Count > 1 Then
                    dgArticleGridPrevious.Rows.RemoveRange(1, dgArticleGridPrevious.Rows.Count - 1)
                End If
                BindArticelGridPByNoteId(_dt)
            Else
                dgArticleGridPrevious.Rows.RemoveRange(1, dgArticleGridPrevious.Rows.Count - 1)
                btnCopyPrescriptionToCurrent.Enabled = False
            End If
        End If
        If DtPrescDtl.Rows.Count > 0 Then
            'btnCopyPrescriptionToCurrent.Enabled = True
        End If


    End Sub
    Private Sub BtnNextSession_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNextSession.Click

        If (BindMb IsNot Nothing AndAlso BindMb.Count > 0) Then
            CurrentSession = BindMb.Position
            Dim CS As Integer
            If BindMb.Count <> 1 Then
                CS = BindMb.Count - (CurrentSession - 1)
            Else
                CS = BindMb.Count
            End If
            If CS <= BindMb.Count Then
                txtTotalSession.Value = CS & "/" & BindMb.Count
                BindMb.Position -= 1

                If CS = BindMb.Count Then
                    BtnNextSession.Enabled = False
                    BtnPreviousSession.Enabled = True
                Else
                    BtnPreviousSession.Enabled = True
                End If
            End If
            If CS <> 0 Then
                Dim _dt As DataTable = clsConsNotes.GetPatientPrescriptionInfo(vPatientID, CS)
                If _dt.Rows.Count > 0 Then
                    If dgArticleGridPrevious.Rows.Count > 1 Then
                        dgArticleGridPrevious.Rows.RemoveRange(1, dgArticleGridPrevious.Rows.Count - 1)
                    End If
                    BindArticelGridPByNoteId(_dt)
                Else
                    dgArticleGridPrevious.Rows.RemoveRange(1, dgArticleGridPrevious.Rows.Count - 1)
                    btnCopyPrescriptionToCurrent.Enabled = False
                End If
                'If _dt.Rows.Count > 0 Then

                'End If
            End If
            If DtPrescDtl.Rows.Count > 0 Then
                ' btnCopyPrescriptionToCurrent.Enabled = True
            End If


        End If

    End Sub

    Private Sub ExtendedPPatient_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExtendedPPatient.SizeChanged

        If ExtendedPPatient.State = Stepi.UI.ExtendedPanelState.Expanding Then
            GetGroupBoxDetails()
            If ExtendedPPatient.Size.Width = 40 Then

                'MessageBox.Show("Expanding" + extendedPanelTest.Size.Width)
                SplitContainer.Size = New System.Drawing.Size(gsizeWith - 275, gsizeHight - 65) ' Size(749, 455) MIN =277, 364
            End If
        Else
            If ExtendedPPatient.Size.Width = 257 Then
                GetGroupBoxDetails()
                'MessageBox.Show("Collapsing" + extendedPanelTest.Size.Width)
                SplitContainer.Size = New System.Drawing.Size(gsizeWith - 20, gsizeHight - 68) 'Size(1005, 455)

            End If
        End If
    End Sub

    'Private Sub GetDoctorId()
    '    Try
    '        Dim clsComn As New clsHcCommon
    '        Dim dtDrInfo As DataTable = New DataTable()
    '        dtDrInfo = clsComn.GetDoctorInfo()
    '        dtDrInfo.TableName = "DoctorInfo"

    '        dtDrInfo = objComn.GetSalesPerson(clsAdmin.SiteCode)

    '        CtrlCmbDrName.ColumnHeaders = False
    '        CtrlCmbDrName.ExtendRightColumn = True
    '        CtrlCmbDrName.DisplayMember = "DoctorName"
    '        CtrlCmbDrName.ValueMember = "EmployeeCode"

    '        CtrlCmbDrName.DataSource = dtDrInfo
    '        CtrlCmbDrName.Splits(0, 0).DisplayColumns("EmployeeCode").Visible = False
    '        CtrlCmbDrName.SelectedIndex = 0

    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub dtAppDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtAppDate.ValueChanged
        If clsAdmin.EmployeeID <> String.Empty AndAlso dtAppDate.Text <> String.Empty Then

            ' Dim setdate As Date = Convert.ToDateTime(dtAppDate.Text)

            dt = clsConsNotes.GetTo_dayDrAppointmentInfo(clsAdmin.EmployeeID, DirectCast(dtAppDate.Value, Date).ToString("yyyyMMdd"))
            grdDrAppInfo.DataSource = Nothing
            dt.TableName = "HcTrnAppointment"
            grdDrAppInfo.DataSource = dt
            grdDrAppInfo.DataMember = "HcTrnAppointment"
            'GridDisplayInfo.Splits(0).DisplayColumns("SiteCode").Width = 75
            'GridDisplayInfo.Splits(0).DisplayColumns("SiteCode").Visible = False
            grdDrAppInfo.Cols("PatientId").Visible = False

        End If
    End Sub
    Private Sub GetGroupBoxDetails()
        gsizeWith = GroupBox2.Size.Width
        gsizeHight = GroupBox2.Size.Height
        gLocationX = GroupBox2.Location.X
        gLocationY = GroupBox2.Location.Y
        If ExtendedPPatient.State = ExtendedPanelState.Collapsed Then
            ExtendedPPatient.Location = New System.Drawing.Point(gsizeWith - 20, 15)
        End If
    End Sub

    Private Sub grdDrAppInfo_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDrAppInfo.DoubleClick

        If grdDrAppInfo.Rows.Count - 1 > 0 Then
            Dim PatientCode As String = grdDrAppInfo.Item(grdDrAppInfo.Row, "PatientId")
            CtrlPatientInfo.GetPatientDetails(PatientCode)
        End If


    End Sub
    Private Sub btnAddCurrentPrescription_Click(sender As Object, e As EventArgs) Handles btnAddCurrentPrescription.Click
        Try
            If vPatientID = "" Then
                ShowMessage("Please select patient", "Information")
                Exit Sub
            End If
            Using objaddPresc As New frmAddPrescription
                objaddPresc.DtPrescDtl = DtPrescDtl
                objaddPresc.PatientId = vPatientID
                objaddPresc.NoteSrNo = vNoteSrNo
                objaddPresc.ConsultantsNoteId = vConsultantNoteID
                objaddPresc.DtPrescriptionCopy = DtPrescDtl.Copy
                Dim dialogResult = objaddPresc.ShowDialog()
                If (dialogResult = Windows.Forms.DialogResult.Cancel) Then
                    'If objaddPresc.DtPrescDtl.Rows.Count > 0 Then
                    'Else
                    '    dgGridArticleToday.Rows.RemoveRange(1, dgGridArticleToday.Rows.Count - 1)
                    '    btnCopyPrescriptionToCurrent.Enabled = True
                    'End If
                    Exit Sub
                ElseIf (dialogResult = Windows.Forms.DialogResult.OK) Then

                    DtPrescDtl = objaddPresc.DtPrescDtl
                    dgGridArticleToday.Rows.Count = 1
                    If DtPrescDtl.Rows.Count > 0 Then
                        btnCopyPrescriptionToCurrent.Enabled = False
                    Else
                        'btnCopyPrescriptionToCurrent.Enabled = True
                        btnCopyPrescriptionToCurrent.Enabled = False
                    End If
                    BindPrescriptionTodayGrid(DtPrescDtl)
                End If
            End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnCopyPrescriptionToCurrent_Click(sender As Object, e As EventArgs) Handles btnCopyPrescriptionToCurrent.Click
        CopyPrescriptionLastToCurrent()
    End Sub

    Public Function CopyPrescriptionLastToCurrent()
        Try
            Dim _CsrNo As Integer = 0
            For index = 1 To dgArticleGridPrevious.Rows.Count - 1
                Dim result As DataRow() = DtPrescDtl.Select("Articlecode='" + dgArticleGridPrevious.Rows(index)("Articlecode").ToString() + "'")
                If result.Count > 0 Then
                Else
                    DtPrescDtl.Rows.Add()
                    _CsrNo = DtPrescDtl.Rows.Count - 1
                    DtPrescDtl.Rows(DtPrescDtl.Rows.Count - 1)("SrNo") = _CsrNo
                    DtPrescDtl.Rows(DtPrescDtl.Rows.Count - 1)("ArticleCode") = dgArticleGridPrevious.Rows(index)("ArticleCode").ToString()
                    DtPrescDtl.Rows(DtPrescDtl.Rows.Count - 1)("ArticleDescription") = dgArticleGridPrevious.Rows(index)("ArticleDescription").ToString()
                    DtPrescDtl.Rows(DtPrescDtl.Rows.Count - 1)("Qty") = dgArticleGridPrevious.Rows(index)("Quantity").ToString()
                    DtPrescDtl.Rows(DtPrescDtl.Rows.Count - 1)("ConsumptionRate") = dgArticleGridPrevious.Rows(index)("ConsumptionRate").ToString()
                    DtPrescDtl.Rows(DtPrescDtl.Rows.Count - 1)("Duration") = dgArticleGridPrevious.Rows(index)("Duration").ToString()
                    DtPrescDtl.Rows(DtPrescDtl.Rows.Count - 1)("Remarks") = dgArticleGridPrevious.Rows(index)("Remarks").ToString()
                    DtPrescDtl.Rows(DtPrescDtl.Rows.Count - 1)("ConsultantsNoteId") = vDrNoteID
                    DtPrescDtl.Rows(DtPrescDtl.Rows.Count - 1)("PatientId") = vPatientID
                    DtPrescDtl.Rows(DtPrescDtl.Rows.Count - 1)("SiteCode") = clsAdmin.SiteCode
                End If
                'Dim dtRow As Int32 = -1
             
            Next
            dgGridArticleToday.Rows.Count = 1
            For row = 0 To DtPrescDtl.Rows.Count - 1

                dgGridArticleToday.Rows.Add()
                Dim dtRow As Int32 = -1
                dgGridArticleToday.Rows(dgGridArticleToday.Rows.Count - 1)("SrNo") = DtPrescDtl.Rows(row)("SrNo").ToString
                dgGridArticleToday.Rows(dgGridArticleToday.Rows.Count - 1)("ArticleCode") = DtPrescDtl.Rows(row)("ArticleCode").ToString
                dgGridArticleToday.Rows(dgGridArticleToday.Rows.Count - 1)("ArticleDescription") = DtPrescDtl.Rows(row)("ArticleDescription").ToString
                dgGridArticleToday.Rows(dgGridArticleToday.Rows.Count - 1)("Quantity") = DtPrescDtl.Rows(row)("Qty").ToString
                dgGridArticleToday.Rows(dgGridArticleToday.Rows.Count - 1)("ConsumptionRate") = DtPrescDtl.Rows(row)("ConsumptionRate").ToString
                dgGridArticleToday.Rows(dgGridArticleToday.Rows.Count - 1)("Duration") = DtPrescDtl.Rows(row)("Duration").ToString
                dgGridArticleToday.Rows(dgGridArticleToday.Rows.Count - 1)("Remarks") = DtPrescDtl.Rows(row)("Remarks").ToString
            Next

            btnCopyPrescriptionToCurrent.Enabled = False
        Catch ex As Exception
            ShowMessage("CopyPrescriptionLastToCurrent : " & vbCrLf & ex.Message, "Error")
        End Try
    End Function
    Public Function BindPrescriptionTodayGrid(ByVal dtPrescToday As DataTable)
        Try

            dgGridArticleToday.Rows.Count = 1
            For index = 0 To dtPrescToday.Rows.Count - 1
                dgGridArticleToday.Rows.Add()
                '  dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.Selects) = ""
                dgGridArticleToday.Rows(dgGridArticleToday.Rows.Count - 1)("SrNo") = dtPrescToday.Rows(index)("SrNo").ToString()
                'dgGridArticleToday.Rows(dgGridArticleToday.Rows.Count - 1)("SrNo") = dgGridArticleToday.Rows.Count - 1
                dgGridArticleToday.Rows(dgGridArticleToday.Rows.Count - 1)("ArticleCode") = dtPrescToday.Rows(index)("ArticleCode").ToString()
                dgGridArticleToday.Rows(dgGridArticleToday.Rows.Count - 1)("ArticleDescription") = dtPrescToday.Rows(index)("ArticleDescription")

                dgGridArticleToday.Rows(dgGridArticleToday.Rows.Count - 1)("Quantity") = dtPrescToday.Rows(index)("Qty")
                dgGridArticleToday.Rows(dgGridArticleToday.Rows.Count - 1)("ConsumptionRate") = dtPrescToday.Rows(index)("ConsumptionRate")
                dgGridArticleToday.Rows(dgGridArticleToday.Rows.Count - 1)("Duration") = (dtPrescToday.Rows(index)("Duration"))
                dgGridArticleToday.Rows(dgGridArticleToday.Rows.Count - 1)("Remarks") = dtPrescToday.Rows(index)("Remarks")
                vConsultantNoteID = dtPrescToday.Rows(index)("ConsultantsNoteId")
                If ChangedConsultantNote <> String.Empty Then
                    rtextTodSessionNotes.Text = ChangedConsultantNote
                Else
                    ChangedConsultantNote = dtPrescToday.Rows(index)("ConsultantsNoteText").ToString()
                    rtextTodSessionNotes.Text = ChangedConsultantNote
                End If




                ' dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.EAN) = dt.Rows(0)("Ean").ToString()
                If (dgGridArticleToday.Rows.Count > 1) Then
                    dgGridArticleToday.Select(dgGridArticleToday.Rows.Count - 1, 2)
                End If
            Next
            DtPrescDtl = dtPrescToday.Copy()
            dgGridArticleToday.AllowEditing = False
        Catch ex As Exception
            ShowMessage("BindPrescriptionTodayGrid : " & vbCrLf & ex.Message, "Error")
            LogException(ex)
        End Try

    End Function

    Private Sub btnAddToBill_Click(sender As Object, e As EventArgs) Handles btnAddToBill.Click
        Try
            dtPrescDtlAddToBill.Clear()
            For index = 1 To dgGridArticleToday.Rows.Count - 1
                'Dim dtRow As Int32 = -1
                dtPrescDtlAddToBill.Rows.Add()
                dtPrescDtlAddToBill.Rows(dtPrescDtlAddToBill.Rows.Count - 1)("SrNo") = dgGridArticleToday.Rows(index)("SrNo").ToString()
                dtPrescDtlAddToBill.Rows(dtPrescDtlAddToBill.Rows.Count - 1)("ArticleCode") = dgGridArticleToday.Rows(index)("ArticleCode").ToString()
                dtPrescDtlAddToBill.Rows(dtPrescDtlAddToBill.Rows.Count - 1)("ArticleDescription") = dgGridArticleToday.Rows(index)("ArticleDescription").ToString()
                dtPrescDtlAddToBill.Rows(dtPrescDtlAddToBill.Rows.Count - 1)("Qty") = dgGridArticleToday.Rows(index)("Quantity").ToString()
                dtPrescDtlAddToBill.Rows(dtPrescDtlAddToBill.Rows.Count - 1)("ConsumptionRate") = dgGridArticleToday.Rows(index)("ConsumptionRate").ToString()
                dtPrescDtlAddToBill.Rows(dtPrescDtlAddToBill.Rows.Count - 1)("Duration") = dgGridArticleToday.Rows(index)("Duration").ToString()
                dtPrescDtlAddToBill.Rows(dtPrescDtlAddToBill.Rows.Count - 1)("Remarks") = dgGridArticleToday.Rows(index)("Remarks").ToString()
                dtPrescDtlAddToBill.Rows(dtPrescDtlAddToBill.Rows.Count - 1)("ConsultantsNoteId") = vConsultantNoteID
                dtPrescDtlAddToBill.Rows(dtPrescDtlAddToBill.Rows.Count - 1)("PatientId") = vPatientID
                dtPrescDtlAddToBill.Rows(dtPrescDtlAddToBill.Rows.Count - 1)("SiteCode") = clsAdmin.SiteCode
            Next
            Me.DialogResult = Windows.Forms.DialogResult.Yes
            Exit Sub
        Catch ex As Exception

        End Try



    End Sub
    
End Class

'ArticleCode
'Quantity