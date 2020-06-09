Imports SpectrumBL
Imports System.Data

Public Class frmHCClinicalHistory
    Private clsClinical As New clsHcClinicalHistory
    Private clsComn As New clsHcCommon
    Dim objClsCommon As New clsCommon
    Private dsClinical As DataSet
    Private dtPreIntv As DataTable
    Private dtRelation As DataTable

    Private drCHistory, drSymptoms, drPreInt As DataRow
    Private drInvTreat, drRiskFactors, drFamily, drMedical As DataRow

    Private fkCHistory(1), fkSymptoms(2), fkPreInt(2) As Object
    Private fkInvTreat(2), fkRiskFactors(2), fkFamily(3), fkMedical(1) As Object

    Private IsNewCHistory, IsNewSymptoms, IsNewPreInt As Boolean
    Private IsNewInvTreat, IsNewRiskFactors, IsNewFamily, IsNewMedical As Boolean

    'Private IsUpdatedClinicalHistory As Boolean = False
    Private IsNewClinicalHist As Boolean = False

    Private ClinicalHistoryId As String = String.Empty
    Private PatientID As String = String.Empty
    Private filterQuery As String = String.Empty
    Private cRiskFactors As CtrlHcRiskFactors

    Private SymptomsSrNo, InvTreatSrNo, PreIntvIndex, FamilySrNo, MedicalSrNo, ClinicalHistorySrNo As Integer
    Private gIndex As Integer = 1
    Public SetPatientID As String = String.Empty

    Private Sub frmHCClinicalHistory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If BtnNewClinicalHistory.Text = "Cancel" Then
                BtnNewClinicalHistory.Visible = False
            End If
            AddHandler CtrlPatientInfo.txtPatientName.ValueChanged, AddressOf GetClinicalHistoryInfo
            'Comented by Khusrao Adil
            ' AddHandler RiskFactorsNil.CheckBoxRiskFactors.Click, AddressOf SetAllRiskFactorClearAndDisable
            CtrlPatientInfo.BtnMoreInfo.Enabled = False
            dtPreIntv = New DataTable
            'Comented by Khusrao Adil
            'dtPreIntv = clsComn.GetCodeDesc(clsAdmin.SiteCode, "PreIntervention")

            'grdPreIntervention.DataSource = Nothing
            'grdPreIntervention.DataSource = dtPreIntv
            'grdPreIntervention.DataMember = "PreIntervention"

            dtRelation = New DataTable
            dtRelation = clsComn.GetCodeDesc(clsAdmin.SiteCode, "Relation")
            dtRelation.TableName = "Relation"

            Dim cboRelation As New ComboBox
            cboRelation.DropDownStyle = ComboBoxStyle.DropDownList

            cboRelation.DisplayMember = "ShortDesc"
            cboRelation.ValueMember = "Code"
            cboRelation.DataSource = dtRelation
            grdFamilyHistory.Cols("Relationship").Editor = cboRelation

            If (SetPatientID <> String.Empty) Then
                CtrlPatientInfo.GetPatientDetails(SetPatientID)
                CtrlPatientInfo.BtnSearchPatient.Enabled = False
                'SendKeys.Send(vbCr)
                'DisableAllControlsInClinicalHistoryForm()
                ' RefreshPatientTrnsDetails(dsPatientHcTrnDetails, SetPatientID, True)
                dsClinical = dsPatientHcTrnDetails.Copy
            Else

                dsClinical = clsClinical.GetClinicalHistory(String.Empty, String.Empty, clsAdmin.SiteCode)
                ReInitializeControls(False)
                CtrlPatientInfo.txtPatientID.Select()
            End If
            If CheckAuthorisationForTran(clsAdmin.SiteCode, "ConsultantNotes") = True Then
            Else
                BtnNewClinicalHistory.Visible = False
                BtnEditClinicalHistory.Visible = False
                grdSymptoms.AllowEditing = False
                grdInvestigations.AllowEditing = False
                grdFamilyHistory.AllowEditing = False
                grdMedicalHistory.AllowEditing = False
                '   ReInitializeControls(True)
            End If
        Catch ex As Exception
            MessageBox.Show("frmHCClinicalHistory :" & vbCrLf & ex.Message)
        End Try

    End Sub
    Public Function RefreshPatientTrnsDetails(ByVal dsTrnsInfo As DataSet, ByVal PatientId As String, ByVal isNewEntry As Boolean)
        Try
            If isNewEntry = True Then
                'If dsTrnsInfo.Tables("HcTrnSymptoms").Rows.Count > 0 Then
                '    For Each drSymptoms In dsTrnsInfo.Tables("HcTrnSymptoms").Rows
                '        If (drSymptoms("SymptomDesc") IsNot DBNull.Value OrElse _
                '            drSymptoms("Duration") IsNot DBNull.Value) Then
                '            grdSymptoms.Item(gIndex, "SymptomDesc") = drSymptoms("SymptomDesc")
                '            grdSymptoms.Item(gIndex, "Duration") = drSymptoms("Duration")
                '            grdSymptoms.Item(gIndex, "DMY") = drSymptoms("DMY")

                '            grdSymptoms.Rows(gIndex).AllowEditing = True
                '        Else
                '            grdSymptoms.Rows(gIndex).AllowEditing = True
                '        End If
                '        gIndex += 1
                '    Next
                'End If
            Else
                ' dsClinical = dsTrnsInfo.Copy
                ' PopulateClinicalHistoryInfo(isNewEntry)
            End If
            If dsTrnsInfo.Tables("HcTrnSymptoms").Rows.Count > 0 Then
                For Each drSymptoms In dsTrnsInfo.Tables("HcTrnSymptoms").Rows
                    If (drSymptoms("SymptomDesc") IsNot DBNull.Value OrElse _
                        drSymptoms("Duration") IsNot DBNull.Value) Then
                        grdSymptoms.Item(gIndex, "SymptomDesc") = drSymptoms("SymptomDesc")
                        grdSymptoms.Item(gIndex, "Duration") = drSymptoms("Duration")
                        grdSymptoms.Item(gIndex, "DMY") = drSymptoms("DMY")

                        grdSymptoms.Rows(gIndex).AllowEditing = True
                    Else
                        grdSymptoms.Rows(gIndex).AllowEditing = True
                    End If
                    gIndex += 1
                Next
            End If
            
            
        Catch ex As Exception
            MessageBox.Show("RefreshPatientTrnsDetails :" & vbCrLf & ex.Message)
        End Try
    End Function
    Private Sub GetClinicalHistoryInfo()
        GetClinicalHistoryInfo(CtrlPatientInfo.txtPatientID.Text.Trim)
    End Sub
    Public Sub GetClinicalHistoryInfo(ByVal GetPatientID As String)
        Try
            If (String.IsNullOrEmpty(GetPatientID)) Then Exit Sub

            If (dsClinical IsNot Nothing) Then
                For Each dtInfo As DataTable In dsClinical.Tables
                    dtInfo.Rows.Clear()
                Next
            End If

            ClinicalHistoryId = clsClinical.GetClinicalHistId(GetPatientID, clsAdmin.SiteCode)

            SymptomsSrNo = FamilySrNo = MedicalSrNo = InvTreatSrNo = PreIntvIndex = 0
            ReInitializeControls(True)

            If Not (String.IsNullOrEmpty(ClinicalHistoryId)) Then
                dsClinical = New DataSet
                dsClinical = clsClinical.GetClinicalHistory(ClinicalHistoryId, GetPatientID, clsAdmin.SiteCode)

                PopulateClinicalHistoryInfo(True)
            Else
                ReInitializeControls(True)
            End If

        Catch ex As Exception
            MessageBox.Show("GetClinicalHistoryInfo :" & vbCrLf & ex.Message)
        End Try
    End Sub
    Private Sub PopulateClinicalHistoryInfo(Optional ByVal isNewEntry As Boolean = False)
        Try
            'Comented By Khusrao Adil

            'If (dsClinical.Tables("HcTrnClinicalHistory").Rows(0)("IsGenConsentFrmSigned") = True) Then
            '    CheckBoxConsent.Checked = True
            '    CheckBoxConsent.Enabled = False
            'Else
            '    CheckBoxConsent.Checked = False
            '    CheckBoxConsent.Enabled = True
            'End If

            gIndex = 1
            For Each drSymptoms In dsClinical.Tables("HcTrnSymptoms").Rows
                If (drSymptoms("SymptomDesc") IsNot DBNull.Value OrElse _
                    drSymptoms("Duration") IsNot DBNull.Value) Then
                    grdSymptoms.Item(gIndex, "SymptomDesc") = drSymptoms("SymptomDesc")
                    grdSymptoms.Item(gIndex, "Duration") = drSymptoms("Duration")
                    grdSymptoms.Item(gIndex, "DMY") = drSymptoms("DMY")
                    If isNewEntry = True Then
                        grdSymptoms.Rows(gIndex).AllowEditing = True
                    Else
                        grdSymptoms.Rows(gIndex).AllowEditing = False
                    End If
                Else
                    grdSymptoms.Rows(gIndex).AllowEditing = True
                End If
                gIndex += 1
            Next
            'Comented by Khusrao Adil
            '
            'grdPreIntervention.Cols("Checked").AllowEditing = True
            'For GridIndex = 0 To grdPreIntervention.Rows.Count - 1
            '    grdPreIntervention.Item(GridIndex, "Checked") = False
            '    grdPreIntervention.Rows(GridIndex).Style = grdPreIntervention.Styles("DelTransStyle")

            '    For Each drItem As DataRow In dsClinical.Tables("HcTrnPreviousIntervention").Rows

            '        If (drItem("InterventionCode") = grdPreIntervention.Item(GridIndex, "Code")) Then
            '            grdPreIntervention.Item(GridIndex, "Checked") = True
            '            grdPreIntervention.Rows(GridIndex).AllowEditing = False
            '            grdPreIntervention.Rows(GridIndex).Style = grdPreIntervention.Styles("AddTransStyle")
            '        End If
            '    Next
            'Next
            'If (dsClinical.Tables("HcTrnPreviousIntervention").Rows.Count = 1) Then
            '    If (dsClinical.Tables("HcTrnPreviousIntervention").Rows(0)("InterventionCode") = 1) Then
            '        For GridIndex = 1 To grdPreIntervention.Rows.Count - 1
            '            grdPreIntervention.Item(GridIndex, "Checked") = False
            '            grdPreIntervention.Rows(GridIndex).AllowEditing = False
            '            grdPreIntervention.Rows(GridIndex).Style = grdPreIntervention.Styles("AddTransStyle")
            '        Next
            '        grdPreIntervention.Item(0, "Checked") = True
            '        grdPreIntervention.Rows(0).AllowEditing = True
            '        grdPreIntervention.Rows(0).Style = grdPreIntervention.Styles("DelTransStyle")

            '    Else
            '        grdPreIntervention.Rows(0).Style = grdPreIntervention.Styles("AddTransStyle")
            '        grdPreIntervention.Item(0, "Checked") = False
            '        grdPreIntervention.Rows(0).AllowEditing = False
            '    End If
            'ElseIf (dsClinical.Tables("HcTrnPreviousIntervention").Rows.Count > 1) Then
            '    grdPreIntervention.Item(0, "Checked") = False
            '    grdPreIntervention.Rows(0).AllowEditing = False
            'End If

            gIndex = 1
            For Each drInvTreat In dsClinical.Tables("HcTrnClinicalHistInvTreat").Rows
                grdInvestigations.Item(gIndex, "Investigations") = drInvTreat("Investigations")
                grdInvestigations.Item(gIndex, "HistoryOfTreatment") = drInvTreat("HistoryOfTreatment")
                If isNewEntry = True Then
                    grdInvestigations.Rows(gIndex).AllowEditing = True
                Else
                    grdInvestigations.Rows(gIndex).AllowEditing = False
                End If

                gIndex += 1
            Next

            For Each drRiskFactor As DataRow In dsClinical.Tables("HcTrnRiskFactor").Rows
                'comented By Khusrao Adil

                'For Each cRiskFactorstp As Control In C1SizerRiskFactors.Controls
                '    cRiskFactors = DirectCast(cRiskFactorstp, CtrlHcRiskFactors)

                '    If (cRiskFactors.CheckBoxRiskFactors.Text = drRiskFactor("RiskFactorCode")) Then
                '        cRiskFactors.CheckBoxRiskFactors.Checked = True
                '        cRiskFactors.txtDurationIn.Value = drRiskFactor("Duration")

                '        If (drRiskFactor("DMY") = "Day(s)") Then
                '            cRiskFactors.cboDurationIn.SelectedIndex = 0
                '        ElseIf (drRiskFactor("DMY") = "Month(s)") Then
                '            cRiskFactors.cboDurationIn.SelectedIndex = 1
                '        Else
                '            cRiskFactors.cboDurationIn.SelectedIndex = 2
                '        End If
                '        cRiskFactors.Enabled = False

                '    End If
                'Next
            Next
            'comented By Khusrao Adil

            'If (dsClinical.Tables("HcTrnRiskFactor").Rows.Count = 1) Then
            '    If (dsClinical.Tables("HcTrnRiskFactor").Rows(0)("RiskFactorCode") = "Nil") Then
            '        ClearAndEnableDisableRiskFactor(False, False)
            '        RiskFactorsNil.Enabled = True
            '        RiskFactorsNil.CheckBoxRiskFactors.Checked = True
            '    Else
            '        RiskFactorsNil.Enabled = False
            '        RiskFactorsNil.CheckBoxRiskFactors.Checked = False

            '    End If
            'ElseIf (dsClinical.Tables("HcTrnRiskFactor").Rows.Count > 1) Then
            '    RiskFactorsNil.Enabled = False
            '    RiskFactorsNil.CheckBoxRiskFactors.Checked = False
            'End If

            gIndex = 1
            For Each drFamily In dsClinical.Tables("HcTrnFamilyHistory").Rows
                grdFamilyHistory.Item(gIndex, "RelationShip") = drFamily("RelationShip")
                grdFamilyHistory.Item(gIndex, "DiseaseCode") = drFamily("DiseaseCode")
                grdFamilyHistory.Item(gIndex, "Duration") = drFamily("Duration")
                grdFamilyHistory.Item(gIndex, "DMY") = drFamily("DMY")
                grdFamilyHistory.Item(gIndex, "Treatment") = drFamily("Treatment")
                If isNewEntry = True Then
                    grdFamilyHistory.Rows(gIndex).AllowEditing = True
                Else
                    grdFamilyHistory.Rows(gIndex).AllowEditing = False
                End If
                gIndex += 1
            Next

            gIndex = 1
            For Each drMedical In dsClinical.Tables("HcTrnMedicalHistory").Rows
                grdMedicalHistory.Item(gIndex, "DiseaseCode") = drMedical("DiseaseCode")
                grdMedicalHistory.Item(gIndex, "Duration") = drMedical("Duration")
                grdMedicalHistory.Item(gIndex, "DMY") = drMedical("DMY")
                grdMedicalHistory.Item(gIndex, "Treatment") = drMedical("Treatment")
                If isNewEntry = True Then
                    grdMedicalHistory.Rows(gIndex).AllowEditing = True
                Else
                    grdMedicalHistory.Rows(gIndex).AllowEditing = False
                End If

                gIndex += 1
            Next

        Catch ex As Exception
            MessageBox.Show("PopulateClinicalHistoryInfo :" & vbCrLf & ex.Message)
        End Try
    End Sub
    Private Sub SetAllRiskFactorClearAndDisable()
        'comented By Khusrao Adil

        'If (RiskFactorsNil.CheckBoxRiskFactors.CheckState = CheckState.Checked) Then
        '    ClearAndEnableDisableRiskFactor(False, False)
        '    RiskFactorsNil.Enabled = True
        '    RiskFactorsNil.CheckBoxRiskFactors.Checked = True
        'Else
        '    ClearAndEnableDisableRiskFactor(False, True)
        'End If

    End Sub

    Private Sub BtnNewClinicalHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNewClinicalHistory.Click

        ReInitializeControls(True)
        BtnNewClinicalHistory.Visible = False
        BtnEditClinicalHistory.Visible = False
        'Comented by Khusrao Adil

        ' grdPreIntervention.AllowEditing = False
        grdSymptoms.AllowEditing = False
        grdInvestigations.AllowEditing = False
        grdFamilyHistory.AllowEditing = False
        grdMedicalHistory.AllowEditing = False
        'Comented By Khusrao Adil
        'CheckBoxConsent.Checked = False

    End Sub
    Private Sub BtnEditClinicalHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEditClinicalHistory.Click
        Try
            If (ValidateClinicalHistory() = False) Then
                Exit Sub
            End If
            'Comented By Khusrao Adil

            'If CheckBoxConsent.Checked = False Then
            'Dim dlgans As DialogResult
            'dlgans = MsgBox("Consent Form is Not Checked/Signed." & vbCrLf & " Do you still want to Save ?", MsgBoxStyle.YesNoCancel, "Confirmation")
            'If dlgans = MsgBoxResult.Yes Then
            '    If MsgBox(" The patient will not reflect in the Consultants list. Do you want continue?", MsgBoxStyle.YesNo, "Confirmation") = MsgBoxResult.Yes Then
            '        SaveClinicalHistoryInfo()
            '    End If
            'ElseIf dlgans = MsgBoxResult.No Then
            '    If MsgBox(" Change will be lost. Do you want continue?", MsgBoxStyle.YesNo, "Confirmation") = MsgBoxResult.Yes Then
            '        BtnNewClinicalHistory_Click(Nothing, Nothing)
            '    End If
            'ElseIf dlgans = MsgBoxResult.Cancel Then

            'End If
            'Else
            SaveClinicalHistoryInfo()
            ' End If

        Catch ex As Exception
            MessageBox.Show("BtnEditClinicalHistory :" & vbCrLf & ex.Message)
        End Try

    End Sub
    Private Sub BtnExitClinicalHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExitClinicalHistory.Click
        Me.Close()
    End Sub

    Private Sub SaveClinicalHistoryInfo()
        Try
            PatientID = CtrlPatientInfo.txtPatientID.Text.Trim

            If (PrepareClinicalHistoryInfo() = False) Then
                Exit Sub
            End If
            If (PrepareSymptomsInfo() = False) Then
                Exit Sub
            End If
            ' ''If (PreparePreviousInterventionInfo() = False) Then
            ' ''    Exit Sub
            ' ''End If
            If (PrepareClinicalHistInvTreatInfo() = False) Then
                Exit Sub
            End If
            ' ''If (PrepareRiskFactorsInfo() = False) Then
            ' ''    Exit Sub
            ' ''End If
            If (PrepareFamilyHistoryInfo() = False) Then
                Exit Sub
            End If
            If (PrepareMedicalHistoryInfo() = False) Then
                Exit Sub
            End If

            If (clsComn.PrepareSaveData(dsClinical, "Clinical History", IsNewClinicalHist)) Then
                MessageBox.Show("Clinical History Record : Saved Successfully", "ClinicalHistory", MessageBoxButtons.OK)

                If (dsClinical IsNot Nothing) Then
                    For Each dtInfo As DataTable In dsClinical.Tables
                        dtInfo.Rows.Clear()
                    Next
                End If
                Me.Close()
                ' ReInitializeControls(False)

            Else
                MessageBox.Show("Clinical History Record : Fail", "ClinicalHistory", MessageBoxButtons.OK)
            End If
        Catch ex As Exception
            MessageBox.Show("SaveClinicalHistoryInfo :" & vbCrLf & ex.Message)
        End Try
    End Sub
    Private Function PrepareClinicalHistoryInfo() As Boolean
        Try
            Dim HStatus As Boolean = True
            drCHistory = Nothing
            IsNewCHistory = False
            ' dsClinical = dsPatientHcTrnDetails.Copy
            If (String.IsNullOrEmpty(ClinicalHistoryId)) Then
                'ClinicalHistoryId = clsComn.GetNextDocNo("Clinical History")
                ClinicalHistoryId = objClsCommon.GetNextDocumentNo("Clinical History", clsAdmin.SiteCode)
                ClinicalHistorySrNo = objClsCommon.GetClinicalHistorySrNo(ClinicalHistoryId, PatientID, clsAdmin.SiteCode)
            Else
                ClinicalHistorySrNo = objClsCommon.GetClinicalHistorySrNo(ClinicalHistoryId, PatientID, clsAdmin.SiteCode, False)
            End If

            fkCHistory(0) = ClinicalHistoryId
            fkCHistory(1) = PatientID
            'fkCHistory(2) = ClinicalHistorySrNo
            If dsClinical.Tables("HcTrnClinicalHistory").Rows.Count > 0 Then
                drCHistory = dsClinical.Tables("HcTrnClinicalHistory").Rows.Find(fkCHistory)
            End If

            If (drCHistory Is Nothing) Then

                drCHistory = dsClinical.Tables("HcTrnClinicalHistory").NewRow

                drCHistory("ClinicalHistoryId") = ClinicalHistoryId
                drCHistory("PatientId") = PatientID
                drCHistory("ClinicalHistorySrNo") = ClinicalHistorySrNo
                drCHistory("SiteCode") = clsAdmin.SiteCode
                drCHistory("ReportedByDr") = DBNull.Value
                drCHistory("Investigations") = DBNull.Value
                drCHistory("HistoryOfTreatment") = DBNull.Value
                'Comented By Khusrao Adil
                'drCHistory("IsGenConsentFrmSigned") = IIf(CheckBoxConsent.CheckState = CheckState.Checked, True, False)
                drCHistory("IsGenConsentFrmSigned") = False
                drCHistory("CreatedAt") = clsAdmin.SiteCode
                drCHistory("CreatedBy") = clsAdmin.UserName
                drCHistory("CreatedOn") = DateTime.Now
                drCHistory("Status") = True

                IsNewCHistory = True
            End If

            drCHistory("RecStatus") = True
            drCHistory("UpdatedAt") = clsAdmin.SiteCode
            drCHistory("UpdatedBy") = clsAdmin.UserName
            drCHistory("UpdatedOn") = DateTime.Now

            If (IsNewCHistory) Then
                dsClinical.Tables("HcTrnClinicalHistory").Rows.Add(drCHistory)
            End If

            Return True
        Catch ex As Exception
            MessageBox.Show("PrepareClinicalHistoryInfo :" & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    Private Function PrepareSymptomsInfo() As Boolean
        Try
            Dim drCheckSymptoms As DataRow
            Dim vSymptoms, vDuration, vDurationIn As Object
            SymptomsSrNo = 1

            For gridIndex = 1 To grdSymptoms.Rows.Count - 1

                Dim findKey(2) As Object
                findKey(0) = ClinicalHistoryId
                findKey(1) = PatientID
                findKey(2) = SymptomsSrNo

                vSymptoms = grdSymptoms.Item(gridIndex, "SymptomDesc")
                vDuration = grdSymptoms.Item(gridIndex, "Duration")
                vDurationIn = grdSymptoms.Item(gridIndex, "DMY")

                If (String.IsNullOrEmpty(Convert.ToString(vSymptoms)) AndAlso String.IsNullOrEmpty(Convert.ToString(vDuration))) Then
                    Return True
                    Exit Try
                Else
                    drCheckSymptoms = dsClinical.Tables("HcTrnSymptoms").Rows.Find(findKey)
                    '  drCheckSymptoms = dsClinical.Tables("HcTrnSymptoms").Rows.Find(findKey)
                    ' drCheckSymptoms = dsClinical.Tables("HcTrnSymptoms").Rows.Find(findKey)
                    ' filterQuery = String.Empty
                    ' filterQuery = "SymptomDesc='" & vSymptoms & "' And Duration=" & vDuration & ""
                    '   filterQuery = "ClinicalHistoryId='" & ClinicalHistoryId & "' And PatientId='" & PatientID & "' And SrNo='" & SymptomsSrNo & "'"
                    'drCheckSymptoms = dsClinical.Tables("HcTrnSymptoms").Select(filterQuery)

                End If

                'If (drCheckSymptoms.Length = 0) Then
                If (drCheckSymptoms Is Nothing) Then
                    drSymptoms = dsClinical.Tables("HcTrnSymptoms").NewRow

                    drSymptoms("ClinicalHistoryId") = ClinicalHistoryId
                    drSymptoms("PatientId") = PatientID
                    drSymptoms("SrNo") = SymptomsSrNo

                    drSymptoms("SiteCode") = clsAdmin.SiteCode
                    drSymptoms("SymptomDesc") = vSymptoms
                    drSymptoms("Duration") = IIf(String.IsNullOrEmpty(vDuration), 0, vDuration)
                    drSymptoms("DMY") = vDurationIn

                    drSymptoms("RecStatus") = True
                    drSymptoms("CreatedBy") = clsAdmin.UserName
                    drSymptoms("CreatedAt") = clsAdmin.SiteCode
                    drSymptoms("CreatedOn") = DateTime.Now
                    drSymptoms("UpdatedBy") = clsAdmin.UserName
                    drSymptoms("UpdatedAt") = clsAdmin.SiteCode
                    drSymptoms("UpdatedOn") = DateTime.Now
                    drSymptoms("Status") = True
                    dsClinical.Tables("HcTrnSymptoms").Rows.Add(drSymptoms)

                Else
                    drCheckSymptoms("SiteCode") = clsAdmin.SiteCode
                    drCheckSymptoms("SymptomDesc") = vSymptoms
                    drCheckSymptoms("Duration") = IIf(String.IsNullOrEmpty(vDuration), 0, vDuration)
                    drCheckSymptoms("DMY") = vDurationIn
                    drCheckSymptoms("RecStatus") = True
                    drCheckSymptoms("UpdatedBy") = clsAdmin.UserName
                    drCheckSymptoms("UpdatedAt") = clsAdmin.SiteCode
                    drCheckSymptoms("UpdatedOn") = DateTime.Now
                    ' dsClinical.Tables("HcTrnSymptoms").Rows.Add(drSymptoms)
                End If

                SymptomsSrNo += 1
            Next

            Return True
        Catch ex As Exception
            MessageBox.Show("PrepareSymptomsInfo :" & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    Private Function PreparePreviousInterventionInfo() As Boolean
        Try
            Dim drPreIntervention As DataRow
            Dim drCheckPreIntv As DataRow()

            For Each drDelete As DataRow In dsClinical.Tables("HcTrnPreviousIntervention").Rows
                drDelete.Delete()
            Next
            'comented by Khusrao Adil

            'For GridIndex = 0 To grdPreIntervention.Rows.Count - 1
            '    If (grdPreIntervention.Item(GridIndex, "Checked")) Then

            '        filterQuery = String.Empty
            '        filterQuery = "InterventionCode='" & grdPreIntervention.Item(GridIndex, "Code") & "'"
            '        drCheckPreIntv = dsClinical.Tables("HcTrnPreviousIntervention").Select(filterQuery)

            '        If (drCheckPreIntv.Length = 0) Then
            '            drPreIntervention = dsClinical.Tables("HcTrnPreviousIntervention").NewRow

            '            drPreIntervention("ClinicalHistoryId") = ClinicalHistoryId
            '            drPreIntervention("InterventionCode") = grdPreIntervention.Item(GridIndex, "Code")
            '            drPreIntervention("PatientId") = PatientID

            '            drPreIntervention("SiteCode") = clsAdmin.SiteCode
            '            drPreIntervention("ExtraText") = DBNull.Value

            '            drPreIntervention("RecStatus") = True
            '            drPreIntervention("CreatedBy") = clsAdmin.UserName
            '            drPreIntervention("CreatedOn") = DateTime.Now
            '            drPreIntervention("UpdatedBy") = clsAdmin.UserName
            '            drPreIntervention("UpdatedOn") = DateTime.Now

            '            dsClinical.Tables("HcTrnPreviousIntervention").Rows.Add(drPreIntervention)
            '        End If
            '    End If
            'Next

            'If (dsClinical.Tables("HcTrnPreviousIntervention").Rows.Count > 1) Then
            '    filterQuery = "InterventionCode='1'"
            '    drCheckPreIntv = dsClinical.Tables("HcTrnPreviousIntervention").Select(filterQuery)
            '    If (drCheckPreIntv IsNot Nothing) Then
            '        drCheckPreIntv(0)("RecStatus") = False
            '    End If
            'End If


            Return True
        Catch ex As Exception
            MessageBox.Show("PreparePreviousInterventionInfo :" & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    Private Function PrepareClinicalHistInvTreatInfo() As Boolean
        Try
            Dim drHistInvTreat As DataRow
            Dim drCheckInvTreat As DataRow
            Dim vInvestigations, vTreatment As Object
            InvTreatSrNo = 1

            For gridIndex = 1 To grdInvestigations.Rows.Count - 1
                Dim findKey(2) As Object
                findKey(0) = ClinicalHistoryId
                findKey(1) = PatientID
                findKey(2) = InvTreatSrNo
                vInvestigations = grdInvestigations.Item(gridIndex, "Investigations")
                vTreatment = grdInvestigations.Item(gridIndex, "HistoryOfTreatment")

                If (String.IsNullOrEmpty(Convert.ToString(vInvestigations)) AndAlso String.IsNullOrEmpty(Convert.ToString(vTreatment))) Then
                    Return True
                    Exit Try
                Else
                    'filterQuery = String.Empty

                    'If (String.IsNullOrEmpty(Convert.ToString(vInvestigations)) = False) Then
                    '    filterQuery = "Investigations='" & vInvestigations & "' "
                    'Else
                    '    filterQuery = "Investigations Is Null "
                    'End If

                    'If (String.IsNullOrEmpty(Convert.ToString(vTreatment)) = False) Then
                    '    filterQuery = filterQuery & "And HistoryOfTreatment='" & vTreatment & "'"
                    'Else
                    '    filterQuery = filterQuery & "And HistoryOfTreatment Is Null"
                    'End If
                    'drCheckInvTreat = dsClinical.Tables("HcTrnClinicalHistInvTreat").Select(filterQuery)
                    drCheckInvTreat = dsClinical.Tables("HcTrnClinicalHistInvTreat").Rows.Find(findKey)
                End If

                'If (drCheckInvTreat.Length = 0) Then
                If drCheckInvTreat Is Nothing Then
                    drHistInvTreat = dsClinical.Tables("HcTrnClinicalHistInvTreat").NewRow

                    drHistInvTreat("ClinicalHistoryId") = ClinicalHistoryId
                    drHistInvTreat("InvestTreatSrNo") = InvTreatSrNo
                    drHistInvTreat("PatientId") = PatientID

                    drHistInvTreat("SiteCode") = clsAdmin.SiteCode
                    drHistInvTreat("Investigations") = IIf(String.IsNullOrEmpty(vInvestigations), DBNull.Value, vInvestigations)
                    drHistInvTreat("HistoryOfTreatment") = IIf(String.IsNullOrEmpty(vTreatment), DBNull.Value, vTreatment)

                    drHistInvTreat("RecStatus") = True
                    drHistInvTreat("CreatedBy") = clsAdmin.UserName
                    drHistInvTreat("CreatedAt") = clsAdmin.SiteCode
                    drHistInvTreat("CreatedOn") = DateTime.Now
                    drHistInvTreat("UpdatedBy") = clsAdmin.UserName
                    drHistInvTreat("UpdatedAt") = clsAdmin.SiteCode
                    drHistInvTreat("UpdatedOn") = DateTime.Now
                    drHistInvTreat("Status") = True

                    dsClinical.Tables("HcTrnClinicalHistInvTreat").Rows.Add(drHistInvTreat)
                Else
                    drCheckInvTreat("SiteCode") = clsAdmin.SiteCode
                    drCheckInvTreat("Investigations") = IIf(String.IsNullOrEmpty(vInvestigations), DBNull.Value, vInvestigations)
                    drCheckInvTreat("HistoryOfTreatment") = IIf(String.IsNullOrEmpty(vTreatment), DBNull.Value, vTreatment)

                    drCheckInvTreat("RecStatus") = True
                    drCheckInvTreat("UpdatedBy") = clsAdmin.UserName
                    drCheckInvTreat("UpdatedAt") = clsAdmin.SiteCode
                    drCheckInvTreat("UpdatedOn") = DateTime.Now
                End If
                InvTreatSrNo += 1
            Next

            Return True

        Catch ex As Exception
            MessageBox.Show("PrepareClinicalHistInvTreatInfo :" & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    Private Function PrepareRiskFactorsInfo() As Boolean
        Try
            Dim drRiskFactors As DataRow
            Dim drCheckRiskFactors As DataRow()
            Dim cRiskFactors As CtrlHcRiskFactors

            'comented By Khusrao Adil

            'For Each cRiskFactorstp As Control In C1SizerRiskFactors.Controls
            '    cRiskFactors = DirectCast(cRiskFactorstp, CtrlHcRiskFactors)

            '    If cRiskFactors.CheckBoxRiskFactors.Checked = True Then

            '        filterQuery = String.Empty
            '        filterQuery = "RiskFactorCode='" & cRiskFactors.CheckBoxRiskFactors.Text & "'"
            '        drCheckRiskFactors = dsClinical.Tables("HcTrnRiskFactor").Select(filterQuery)

            '        If (drCheckRiskFactors.Length = 0) Then
            '            drRiskFactors = dsClinical.Tables("HcTrnRiskFactor").NewRow

            '            drRiskFactors("ClinicalHistoryId") = ClinicalHistoryId
            '            drRiskFactors("RiskFactorCode") = cRiskFactors.CheckBoxRiskFactors.Text
            '            drRiskFactors("PatientId") = PatientID

            '            drRiskFactors("SiteCode") = clsAdmin.SiteCode
            '            drRiskFactors("Duration") = IIf(cRiskFactors.txtDurationIn.Text = String.Empty, 0, cRiskFactors.txtDurationIn.Text)
            '            drRiskFactors("DMY") = cRiskFactors.cboDurationIn.Text

            '            drRiskFactors("RecStatus") = True
            '            drRiskFactors("CreatedBy") = clsAdmin.UserName
            '            drRiskFactors("CreatedOn") = DateTime.Now
            '            drRiskFactors("UpdatedBy") = clsAdmin.UserName
            '            drRiskFactors("UpdatedOn") = DateTime.Now

            '            dsClinical.Tables("HcTrnRiskFactor").Rows.Add(drRiskFactors)
            '        End If

            '    End If
            'Next

            If (dsClinical.Tables("HcTrnRiskFactor").Rows.Count > 1) Then
                filterQuery = "RiskFactorCode='Nil'"
                Dim drRiskFactorsTp As DataRow()
                drRiskFactorsTp = dsClinical.Tables("HcTrnRiskFactor").Select(filterQuery)
                If (drRiskFactorsTp.Length > 0) Then
                    drRiskFactorsTp(0).Delete()
                End If
            End If

            Return True
        Catch ex As Exception
            MessageBox.Show("PrepareRiskFactorsInfo :" & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    Private Function PrepareFamilyHistoryInfo() As Boolean
        Try
            Dim drFamilyHistory As DataRow
            'Dim drCheckFamily As DataRow()
            Dim drCheckFamily As DataRow
            FamilySrNo = 1
            Dim vRelationShip, vDiseaseCode, vDuration, vDMY, vTreatment As Object

            For gridIndex = 1 To grdFamilyHistory.Rows.Count - 1
                vRelationship = grdFamilyHistory.Item(gridIndex, "Relationship")
                vDiseaseCode = grdFamilyHistory.Item(gridIndex, "DiseaseCode")
                vDuration = IIf(grdFamilyHistory.Item(gridIndex, "Duration") Is DBNull.Value, Nothing, grdFamilyHistory.Item(gridIndex, "Duration"))
                vDMY = IIf(grdFamilyHistory.Item(gridIndex, "DMY") Is DBNull.Value, Nothing, grdFamilyHistory.Item(gridIndex, "DMY"))
                vTreatment = grdFamilyHistory.Item(gridIndex, "Treatment")

                Dim findKey(2) As Object
                findKey(0) = ClinicalHistoryId
                findKey(1) = PatientID
                findKey(2) = FamilySrNo
                If (String.IsNullOrEmpty(Convert.ToString(vRelationShip)) AndAlso String.IsNullOrEmpty(Convert.ToString(vDiseaseCode))) Then
                    Return True
                    Exit Try
                Else
                    ' filterQuery = String.Empty
                    ' filterQuery = "RelationShip='" & vRelationShip & "' And DiseaseCode='" & vDiseaseCode & "'"

                    'drCheckFamily = dsClinical.Tables("HcTrnFamilyHistory").Select(filterQuery)
                    drCheckFamily = dsClinical.Tables("HcTrnFamilyHistory").Rows.Find(findKey)
                End If

                'If (drCheckFamily.Length = 0) Then
                If drCheckFamily Is Nothing Then
                    drFamilyHistory = dsClinical.Tables("HcTrnFamilyHistory").NewRow

                    drFamilyHistory("ClinicalHistoryId") = ClinicalHistoryId
                    drFamilyHistory("PatientId") = PatientID
                    ' drFamilyHistory("FamilySrNo") = gridIndex
                    drFamilyHistory("FamilySrNo") = FamilySrNo
                    drFamilyHistory("RelationShip") = vRelationShip
                    drFamilyHistory("DiseaseCode") = vDiseaseCode
                    drFamilyHistory("Duration") = IIf(String.IsNullOrEmpty(vDuration), DBNull.Value, vDuration)
                    drFamilyHistory("DMY") = IIf(String.IsNullOrEmpty(vDMY), DBNull.Value, vDMY)
                    drFamilyHistory("Treatment") = vTreatment

                    drFamilyHistory("SiteCode") = clsAdmin.SiteCode
                    drFamilyHistory("RecStatus") = True
                    drFamilyHistory("CreatedBy") = clsAdmin.UserName
                    drFamilyHistory("CreatedAt") = clsAdmin.SiteCode
                    drFamilyHistory("CreatedOn") = DateTime.Now
                    drFamilyHistory("UpdatedBy") = clsAdmin.UserName
                    drFamilyHistory("UpdatedAt") = clsAdmin.SiteCode
                    drFamilyHistory("UpdatedOn") = DateTime.Now

                    dsClinical.Tables("HcTrnFamilyHistory").Rows.Add(drFamilyHistory)
                Else

                    drCheckFamily("RelationShip") = vRelationShip
                    drCheckFamily("DiseaseCode") = vDiseaseCode
                    drCheckFamily("Duration") = IIf(String.IsNullOrEmpty(vDuration), DBNull.Value, vDuration)
                    drCheckFamily("DMY") = IIf(String.IsNullOrEmpty(vDMY), DBNull.Value, vDMY)
                    drCheckFamily("Treatment") = vTreatment
                    drCheckFamily("SiteCode") = clsAdmin.SiteCode
                    drCheckFamily("RecStatus") = True
                    drCheckFamily("UpdatedBy") = clsAdmin.UserName

                    drCheckFamily("UpdatedOn") = DateTime.Now
                End If
                FamilySrNo += 1
            Next

            Return True
        Catch ex As Exception
            MessageBox.Show("PrepareFamilyHistoryInfo :" & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    Private Function PrepareMedicalHistoryInfo() As Boolean
        Try
            Dim drMedicalHistory As DataRow
            Dim drCheckMedical As DataRow
            Dim vDiseaseCode, vDuration, vDMY, vTreatment As Object
            MedicalSrNo = 1
            For gridIndex = 1 To grdMedicalHistory.Rows.Count - 1
                Dim findKey(2) As Object
                findKey(0) = ClinicalHistoryId
                findKey(1) = PatientID
                findKey(2) = MedicalSrNo

                vDiseaseCode = grdMedicalHistory.Item(gridIndex, "DiseaseCode")
                vDuration = IIf(grdMedicalHistory.Item(gridIndex, "Duration") Is DBNull.Value, Nothing, grdMedicalHistory.Item(gridIndex, "Duration"))
                vDMY = IIf(grdMedicalHistory.Item(gridIndex, "DMY") Is DBNull.Value, Nothing, grdMedicalHistory.Item(gridIndex, "DMY"))
                vTreatment = grdMedicalHistory.Item(gridIndex, "Treatment")

                If (String.IsNullOrEmpty(Convert.ToString(vDiseaseCode)) AndAlso String.IsNullOrEmpty(Convert.ToString(vTreatment))) Then
                    Return True
                    Exit Try
                Else
                    'filterQuery = String.Empty
                    'filterQuery = "DiseaseCode='" & vDiseaseCode & "' "
                    'If (String.IsNullOrEmpty(Convert.ToString(vDuration)) = False) Then
                    '    filterQuery = filterQuery & "And Duration='" & vDuration & "' "
                    'Else
                    '    filterQuery = filterQuery & "And Duration Is Null"
                    'End If
                    drCheckMedical = dsClinical.Tables("HcTrnMedicalHistory").Rows.Find(findKey)
                    'drCheckMedical = dsClinical.Tables("HcTrnMedicalHistory").Select(filterQuery)
                End If

                ' If (drCheckMedical.Length = 0) Then
                If drCheckMedical Is Nothing Then
                    drMedicalHistory = dsClinical.Tables("HcTrnMedicalHistory").NewRow

                    drMedicalHistory("ClinicalHistoryId") = ClinicalHistoryId
                    drMedicalHistory("PatientId") = PatientID
                    drMedicalHistory("MedicalSrNo") = gridIndex

                    drMedicalHistory("DiseaseCode") = vDiseaseCode
                    drMedicalHistory("Duration") = IIf(String.IsNullOrEmpty(vDuration), DBNull.Value, vDuration)
                    drMedicalHistory("DMY") = IIf(String.IsNullOrEmpty(vDMY), DBNull.Value, vDMY)
                    drMedicalHistory("Treatment") = vTreatment

                    drMedicalHistory("SiteCode") = clsAdmin.SiteCode
                    drMedicalHistory("RecStatus") = True
                    drMedicalHistory("CreatedBy") = clsAdmin.UserName
                    drMedicalHistory("CreatedAt") = clsAdmin.SiteCode
                    drMedicalHistory("CreatedOn") = DateTime.Now
                    drMedicalHistory("UpdatedBy") = clsAdmin.UserName
                    drMedicalHistory("UpdatedAt") = clsAdmin.SiteCode
                    drMedicalHistory("UpdatedOn") = DateTime.Now
                    drMedicalHistory("Status") = True

                    dsClinical.Tables("HcTrnMedicalHistory").Rows.Add(drMedicalHistory)
                Else
                    drCheckMedical("DiseaseCode") = vDiseaseCode
                    drCheckMedical("Duration") = IIf(String.IsNullOrEmpty(vDuration), DBNull.Value, vDuration)
                    drCheckMedical("DMY") = IIf(String.IsNullOrEmpty(vDMY), DBNull.Value, vDMY)
                    drCheckMedical("Treatment") = vTreatment
                    drCheckMedical("SiteCode") = clsAdmin.SiteCode
                    drCheckMedical("RecStatus") = True
                    drCheckMedical("CreatedBy") = clsAdmin.UserName
                    drCheckMedical("CreatedAt") = clsAdmin.SiteCode
                    drCheckMedical("CreatedOn") = DateTime.Now
                    drCheckMedical("UpdatedBy") = clsAdmin.UserName
                    drCheckMedical("UpdatedAt") = clsAdmin.SiteCode
                    drCheckMedical("UpdatedOn") = DateTime.Now
                End If
                MedicalSrNo += 1
            Next

            Return True
        Catch ex As Exception
            MessageBox.Show("PrepareMedicalHistoryInfo :" & vbCrLf & ex.Message)
            Return False
        End Try
    End Function

    Private Function ValidateClinicalHistory() As Boolean
        Try
            'Patient : Select Patient first
            If (String.IsNullOrEmpty(CtrlPatientInfo.txtPatientName.Text)) Then
                MessageBox.Show("Please Select Patient For Clinical History", "Clinical History", MessageBoxButtons.OK)
                CtrlPatientInfo.txtPatientID.Select()
                Return False
            End If

            'Symptoms : Fill Inoformation in datagrid columns
            Dim vSymptoms, vDuration, vDurationIn As Object
            For gridIndex = 1 To grdSymptoms.Rows.Count - 1
                vSymptoms = grdSymptoms.Item(gridIndex, "SymptomDesc")
                vDuration = grdSymptoms.Item(gridIndex, "Duration")
                vDurationIn = grdSymptoms.Item(gridIndex, "DMY")

                If (String.IsNullOrEmpty(Convert.ToString(vSymptoms)) AndAlso String.IsNullOrEmpty(Convert.ToString(vDuration)) AndAlso String.IsNullOrEmpty(Convert.ToString(vDurationIn))) Then
                    Exit For
                End If

                If (Nothing = vSymptoms AndAlso
                    Nothing = vDuration AndAlso
                    Convert.ToString(vDurationIn) = " ") Then
                    grdSymptoms.Item(gridIndex, "DMY") = Nothing
                    Exit For
                End If

                If (String.IsNullOrEmpty(Convert.ToString(vSymptoms)) OrElse _
                     String.IsNullOrEmpty(Convert.ToString(vDuration)) OrElse _
                     (String.IsNullOrEmpty(Convert.ToString(vDurationIn)) Or Convert.ToString(vDurationIn) = " ")) Then
                    MessageBox.Show("Please Complete Row " & gridIndex & " For Symptoms Information.", "Clinical History", MessageBoxButtons.OK)
                    grdSymptoms.Select()
                    Return False
                End If

            Next

            'Previous Intervention : Select atleast one Previous Intervention
            Dim IsPreInterv As Boolean = False
            'Comented by Khusrao Adil

            'For GridIndex = 0 To grdPreIntervention.Rows.Count - 1
            '    If (grdPreIntervention.Item(GridIndex, "Checked") = True) Then
            '        IsPreInterv = True
            '    End If
            'Next

            'If (IsPreInterv = False) Then
            '    MessageBox.Show("Please select atleast one Previous Intervention.", "Clinical History", MessageBoxButtons.OK)

            '    '  grdPreIntervention.Select()
            '    Return False
            'End If

            'Risk Factors : Select atleast one Risk Factors
            Dim cRiskFactors As CtrlHcRiskFactors
            Dim iRiskFactors As Integer = 0
            'comented By Khusrao Adil

            'For Each cRiskFactorstp As Control In C1SizerRiskFactors.Controls
            '    cRiskFactors = DirectCast(cRiskFactorstp, CtrlHcRiskFactors)

            '    If cRiskFactors.CheckBoxRiskFactors.Checked = True Then
            '        iRiskFactors += 1
            '        If (cRiskFactors.RiskFactorName <> "Nil" AndAlso String.IsNullOrEmpty(cRiskFactors.txtDurationIn.Text)) Then

            '            MessageBox.Show("Please Enter Duration of the Risk Factors.", "Clinical History", MessageBoxButtons.OK)
            '            cRiskFactors.txtDurationIn.Select()
            '            Return False
            '        End If

            '    End If
            'Next
            'If (iRiskFactors = 0) Then
            '    MessageBox.Show("Please select atleast one Risk Factors.", "Clinical History", MessageBoxButtons.OK)


            '    'C1SizerRiskFactors.Select()
            '    Return False
            'End If

            'FamilyHistory : 
            Dim vfRelationship, vfDiseaseCode, vfTreatment, vfDuration, vfDurationIn As Object
            For GridIndex = 1 To grdFamilyHistory.Rows.Count - 1
                vfRelationship = grdFamilyHistory.Item(GridIndex, "Relationship")
                vfDiseaseCode = grdFamilyHistory.Item(GridIndex, "DiseaseCode")
                vfDuration = grdFamilyHistory.Item(GridIndex, "Duration")
                vfDurationIn = grdFamilyHistory.Item(GridIndex, "DMY")
                vfTreatment = grdFamilyHistory.Item(GridIndex, "Treatment")
                If (String.IsNullOrEmpty(Convert.ToString(vfRelationship)) AndAlso String.IsNullOrEmpty(Convert.ToString(vfDiseaseCode)) AndAlso String.IsNullOrEmpty(Convert.ToString(vfDuration))) Then
                    Exit For
                End If
                If Nothing = vfDiseaseCode AndAlso
                    Nothing = vfTreatment AndAlso
                    Nothing = vfDuration AndAlso
                    Nothing = vfDurationIn AndAlso
                    (Convert.ToString(vfRelationship) = " " Or Convert.ToString(vfDurationIn) = " ") Then
                    grdFamilyHistory.Item(GridIndex, "Relationship") = Nothing
                    grdFamilyHistory.Item(GridIndex, "DMY") = Nothing
                    Exit For
                End If


                If Convert.ToString(vfDuration) <> String.Empty And Convert.ToString(vfDurationIn) = String.Empty Or Convert.ToString(vfDurationIn) = " " Then
                    MessageBox.Show("Please Complete Row " & GridIndex & " For Family History Information.", "Clinical History", MessageBoxButtons.OK)
                    grdFamilyHistory.Select()
                    Return False
                End If

                If String.IsNullOrEmpty(Convert.ToString(vfDiseaseCode)) OrElse String.IsNullOrEmpty(Convert.ToString(vfRelationship)) Then
                    MessageBox.Show("Please Complete Row " & GridIndex & " For Family History Information.", "Clinical History", MessageBoxButtons.OK)
                    grdFamilyHistory.Select()
                    Return False
                End If
            Next

            'MedicalHistory : 
            Dim vmDiseaseCode, vmDuration, vmDurationIn As Object
            For GridIndex = 1 To grdMedicalHistory.Rows.Count - 1
                vmDiseaseCode = grdMedicalHistory.Item(GridIndex, "DiseaseCode")
                vmDuration = grdMedicalHistory.Item(GridIndex, "Duration")
                vmDurationIn = grdMedicalHistory.Item(GridIndex, "DMY")
                If (String.IsNullOrEmpty(Convert.ToString(vmDiseaseCode)) AndAlso String.IsNullOrEmpty(Convert.ToString(vmDuration))) Then
                    Exit For
                End If

                If (Nothing = vmDiseaseCode AndAlso
                Nothing = vmDuration AndAlso
                Convert.ToString(vmDurationIn) = " ") Then
                    grdMedicalHistory.Item(GridIndex, "DMY") = Nothing
                    Exit For
                End If

                If (String.IsNullOrEmpty(Convert.ToString(vmDiseaseCode)) OrElse _
                   String.IsNullOrEmpty(Convert.ToString(vmDuration)) OrElse _
                   (String.IsNullOrEmpty(Convert.ToString(vmDurationIn)) Or Convert.ToString(vmDurationIn) = " ")) Then
                    MessageBox.Show("Please Complete Row " & GridIndex & "  For Medical History Information.", "Medical History", MessageBoxButtons.OK)
                    grdMedicalHistory.Select()
                    Return False
                End If
            Next

            Return True
        Catch ex As Exception
            MessageBox.Show("ValidateClinicalHistory" & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    Private Function ReInitializeControls(ByVal IsReadOnly As Boolean) As Boolean
        Try
            Dim gIndex As Integer = 1

            If (SetPatientID = String.Empty) Then
                If (IsReadOnly = False) Then
                    For Each txtInfo As Control In CtrlPatientInfo.TableLayoutPanel1.Controls
                        If (TypeOf txtInfo Is CtrlTextBox) Then
                            DirectCast(txtInfo, CtrlTextBox).Value = Nothing
                        End If
                    Next
                End If

                CtrlPatientInfo.BtnMoreInfo.Enabled = IsReadOnly
                CtrlPatientInfo.CtrlPatientImage1.PicBoxImages.Image = Nothing
                CtrlPatientInfo.TreeViewPatient.Nodes.Clear()
            End If
            CtrlPatientInfo.BtnClinicalHistory.Visible = False

            For gIndex = 1 To grdSymptoms.Rows.Count - 1
                grdSymptoms.Item(gIndex, "SymptomDesc") = Nothing
                grdSymptoms.Item(gIndex, "Duration") = Nothing
                grdSymptoms.Item(gIndex, "DMY") = Nothing
                grdSymptoms.Rows(gIndex).AllowEditing = IsReadOnly
            Next
            'Comented by Khusrao Adil
            '
            'grdPreIntervention.Cols("Checked").AllowEditing = IsReadOnly
            'For gIndex = 0 To grdPreIntervention.Rows.Count - 1
            '    grdPreIntervention.Item(gIndex, "Checked") = False
            '    'grdPreIntervention.Rows(gIndex).AllowEditing = IsReadOnly
            '    grdPreIntervention.Rows(gIndex).Style = grdPreIntervention.Styles("DelTransStyle")
            'Next

            For gIndex = 1 To grdInvestigations.Rows.Count - 1
                grdInvestigations.Item(gIndex, "Investigations") = Nothing
                grdInvestigations.Item(gIndex, "HistoryOfTreatment") = Nothing
                grdInvestigations.Rows(gIndex).AllowEditing = IsReadOnly
            Next

            For gIndex = 1 To grdFamilyHistory.Rows.Count - 1
                grdFamilyHistory.Item(gIndex, "RelationShip") = Nothing
                grdFamilyHistory.Item(gIndex, "DiseaseCode") = Nothing
                grdFamilyHistory.Item(gIndex, "Duration") = Nothing
                grdFamilyHistory.Item(gIndex, "DMY") = Nothing
                grdFamilyHistory.Item(gIndex, "Treatment") = Nothing
                grdFamilyHistory.Rows(gIndex).AllowEditing = IsReadOnly
            Next

            For gIndex = 1 To grdMedicalHistory.Rows.Count - 1
                grdMedicalHistory.Item(gIndex, "DiseaseCode") = Nothing
                grdMedicalHistory.Item(gIndex, "Duration") = Nothing
                grdMedicalHistory.Item(gIndex, "DMY") = Nothing
                grdMedicalHistory.Item(gIndex, "Treatment") = Nothing
                grdMedicalHistory.Rows(gIndex).AllowEditing = IsReadOnly
            Next

            'Clear And Enable All Risk Factor Control 
            ClearAndEnableDisableRiskFactor(True, IsReadOnly)
            'comented By Khusrao Adil

            'RiskFactorsNil.txtDurationIn.Visible = False
            'RiskFactorsNil.cboDurationIn.Visible = False

            'comented By Khusrao Adil
            'CheckBoxConsent.Checked = False
            'CheckBoxConsent.Enabled = IsReadOnly

            BtnNewClinicalHistory.Enabled = IsReadOnly
            BtnEditClinicalHistory.Enabled = IsReadOnly

        Catch ex As Exception
            MessageBox.Show("ReInitializeControls : " & vbCrLf & ex.Message)
        End Try
    End Function

    Private Sub ClearAndEnableDisableRiskFactor(ByVal IsClearValue As Boolean, ByVal IsEnable As Boolean)
        Try
            'comented By Khusrao Adil

            'For Each cRiskFactorsTp As Control In C1SizerRiskFactors.Controls

            '    cRiskFactors = DirectCast(cRiskFactorsTp, CtrlHcRiskFactors)
            '    cRiskFactors.Enabled = IsEnable

            '    If (IsClearValue) Then
            '        cRiskFactors.CheckBoxRiskFactors.Checked = False
            '        cRiskFactors.txtDurationIn.Value = Nothing
            '        cRiskFactors.cboDurationIn.SelectedIndex = 0
            '    End If
            'Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub DisableAllControlsInClinicalHistoryForm()

        CtrlPatientInfo.txtPatientID.ReadOnly = True
        CtrlPatientInfo.BtnSearchPatient.Visible = False
        CtrlPatientInfo.BtnMoreInfo.Visible = False
        CtrlPatientInfo.BtnClinicalHistory.Visible = False

        For GridIndex = 0 To grdFamilyHistory.Rows.Count - 1
            grdFamilyHistory.Rows(GridIndex).AllowEditing = False
        Next
        For GridIndex = 0 To grdInvestigations.Rows.Count - 1
            grdInvestigations.Rows(GridIndex).AllowEditing = False
        Next
        For GridIndex = 0 To grdMedicalHistory.Rows.Count - 1
            grdMedicalHistory.Rows(GridIndex).AllowEditing = False
        Next
        For GridIndex = 0 To grdSymptoms.Rows.Count - 1
            grdSymptoms.Rows(GridIndex).AllowEditing = False
        Next
        'Comented by Khusrao Adil

        'For GridIndex = 0 To grdPreIntervention.Rows.Count - 1
        '    grdPreIntervention.Rows(GridIndex).AllowEditing = False
        'Next

        ClearAndEnableDisableRiskFactor(False, False)
        'comented By Khusrao Adil
        'CheckBoxConsent.Visible = False

        BtnNewClinicalHistory.Visible = False
        BtnEditClinicalHistory.Visible = False

    End Sub
    'Public Sub New()

    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    ' Add any initialization after the InitializeComponent() call.

    'End Sub

    'Public Sub New(ByVal OpMode As String, ByVal strPatientId As String)

    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()
    '    _Mode = OpMode
    '    PatientID = strPatientId
    '    ' Add any initialization after the InitializeComponent() call.

    'End Sub
    'Private Sub gridViewStatus(ByVal flag As Boolean)
    '    grdFamilyHistory.Enabled = flag
    '    grdInvestigations.Enabled = flag
    '    grdMedicalHistory.Enabled = flag
    '    grdPreIntervention.Enabled = flag
    '    grdSymptoms.Enabled = flag
    '    cRiskFactors.Enabled = flag
    '    CheckBoxConsent.Enabled = flag

    '    RiskFactorsAlcohol.fnEnable(flag)
    '    RiskFactorsBeedi.fnEnable(flag)
    '    RiskFactorsBetelLeaves.fnEnable(flag)
    '    RiskFactorsBetelNut.fnEnable(flag)
    '    RiskFactorsCigarette.fnEnable(flag)
    '    RiskFactorsGutka.fnEnable(flag)
    '    RiskFactorsHormoneTherapy.fnEnable(flag)
    '    RiskFactorsMasheri.fnEnable(flag)
    '    RiskFactorsNil.fnEnable(flag)
    '    RiskFactorsPanMasala.fnEnable(flag)
    '    RiskFactorsSnuff.fnEnable(flag)
    '    RiskFactorsTabaccoChewing.fnEnable(flag)

    '    For Each txtInfo As Control In CtrlPatientInfo.TableLayoutPanel1.Controls
    '        If (TypeOf txtInfo Is CtrlTextBox) AndAlso Not (String.IsNullOrEmpty(DirectCast(txtInfo, CtrlTextBox).Text)) Then
    '            DirectCast(txtInfo, CtrlTextBox).Enabled = flag
    '        End If
    '    Next
    'End Sub

    Private Sub grdPreIntervention_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
        Try
            'Comented by Khusrao Adil

            'If (grdPreIntervention.Item(grdPreIntervention.Row, 2) = "Nil") Then
            '    If (grdPreIntervention.Item(0, "Checked") = True) Then
            '        For GridIndex = 1 To grdPreIntervention.Rows.Count - 1
            '            grdPreIntervention.Item(GridIndex, "Checked") = False
            '            grdPreIntervention.Rows(GridIndex).AllowEditing = False
            '            grdPreIntervention.Rows(GridIndex).Style = grdPreIntervention.Styles("AddTransStyle")
            '        Next
            '    ElseIf (grdPreIntervention.Item(0, "Checked") = False) Then
            '        For GridIndex = 1 To grdPreIntervention.Rows.Count - 1
            '            grdPreIntervention.Item(GridIndex, "Checked") = False
            '            grdPreIntervention.Rows(GridIndex).AllowEditing = True
            '            grdPreIntervention.Rows(GridIndex).Style = grdPreIntervention.Styles("DelTransStyle")
            '        Next
            '    End If
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    
    Private Sub grdSymptoms_KeyDownEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyEditEventArgs) Handles grdSymptoms.KeyDownEdit
        If e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
            Dim rowIndex As Integer = grdSymptoms.RowSel.ToString()
            grdSymptoms.Item(rowIndex, "Duration") = Nothing
        End If
      
    End Sub

    Private Sub grdMedicalHistory_KeyDownEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyEditEventArgs) Handles grdMedicalHistory.KeyDownEdit
        If e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
            Dim rowIndex As Integer = grdMedicalHistory.RowSel.ToString()
            grdMedicalHistory.Item(rowIndex, "Duration") = Nothing
        End If
    End Sub

    Private Sub grdFamilyHistory_KeyDownEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyEditEventArgs) Handles grdFamilyHistory.KeyDownEdit
        If e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
            Dim rowIndex As Integer = grdFamilyHistory.RowSel.ToString()
            grdFamilyHistory.Item(rowIndex, "Duration") = Nothing
        End If
    End Sub

End Class