Imports System.Text
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Resources
Imports System.Windows.Forms

Public Class clsHcClinicalHistory

    Dim SqlQry As New StringBuilder     'SqlQry.Append("" & vbCrLf)
    Dim SqlTrans As SqlTransaction
    Dim Sqlda, Sqldau As SqlDataAdapter
    Dim SqlCmdb As SqlCommandBuilder
    Dim Sqlds As DataSet
    Dim Sqldr As DataRow
    Dim SqlDt As DataTable
    Dim PKey(1) As DataColumn

    Dim FilterQuery As String = String.Empty.ToLower
    Dim clsComn As New clsHcCommon

    Public Function GetClinicalHistory(ByVal ClinicalHistID As String, ByVal PatientID As String, ByVal SiteCode As String) As DataSet
        Try
            FilterQuery = String.Empty

            If Not (String.IsNullOrEmpty(ClinicalHistID)) Then
                FilterQuery = "Where ClinicalHistoryID ='" & ClinicalHistID & "' And PatientID ='" & PatientID & "' " & vbCrLf
                FilterQuery = FilterQuery & "And SiteCode ='" & SiteCode & "'; " & vbCrLf
            Else
                FilterQuery = "Where 1=0 ; " & vbCrLf
            End If

            SqlQry.Length = 0
            SqlQry.Append("Select * from HcTrnClinicalHistory " & FilterQuery & vbCrLf)
            SqlQry.Append("Select * from HcTrnSymptoms " & FilterQuery & vbCrLf)
            SqlQry.Append("Select * from HcTrnPreviousIntervention " & FilterQuery & vbCrLf)
            SqlQry.Append("Select * from HcTrnClinicalHistInvTreat " & FilterQuery & vbCrLf)
            SqlQry.Append("Select * from HcTrnRiskFactor " & FilterQuery & vbCrLf)
            SqlQry.Append("Select * from HcTrnFamilyHistory " & FilterQuery & vbCrLf)
            SqlQry.Append("Select * from HcTrnMedicalHistory " & FilterQuery & vbCrLf)

            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "HcTrnClinicalHistory"
            Sqlds.Tables(1).TableName = "HcTrnSymptoms"
            Sqlds.Tables(2).TableName = "HcTrnPreviousIntervention"
            Sqlds.Tables(3).TableName = "HcTrnClinicalHistInvTreat"
            Sqlds.Tables(4).TableName = "HcTrnRiskFactor"
            Sqlds.Tables(5).TableName = "HcTrnFamilyHistory"
            Sqlds.Tables(6).TableName = "HcTrnMedicalHistory"


            Dim PhKey(1) As DataColumn
            PhKey(0) = Sqlds.Tables(0).Columns("ClinicalHistoryId")
            PhKey(1) = Sqlds.Tables(0).Columns("PatientId")
            'PhKey(2) = Sqlds.Tables(0).Columns("ClinicalHistorySrNo")
            Sqlds.Tables(0).PrimaryKey = PhKey

            Dim PsKey(2) As DataColumn
            PsKey(0) = Sqlds.Tables(1).Columns("ClinicalHistoryId")
            PsKey(1) = Sqlds.Tables(1).Columns("PatientId")
            PsKey(2) = Sqlds.Tables(1).Columns("SrNo")
            Sqlds.Tables(1).PrimaryKey = PsKey

            Dim PpKey(2) As DataColumn
            PpKey(0) = Sqlds.Tables(2).Columns("ClinicalHistoryId")
            PpKey(1) = Sqlds.Tables(2).Columns("PatientId")
            PpKey(2) = Sqlds.Tables(2).Columns("InterventionCode")
            Sqlds.Tables(2).PrimaryKey = PpKey

            Dim PiKey(2) As DataColumn
            PiKey(0) = Sqlds.Tables(3).Columns("ClinicalHistoryId")
            PiKey(1) = Sqlds.Tables(3).Columns("PatientId")
            PiKey(2) = Sqlds.Tables(3).Columns("InvestTreatSrNo")
            Sqlds.Tables(3).PrimaryKey = PiKey

            Dim PrKey(2) As DataColumn
            PrKey(0) = Sqlds.Tables(4).Columns("ClinicalHistoryId")
            PrKey(1) = Sqlds.Tables(4).Columns("PatientId")
            PrKey(2) = Sqlds.Tables(4).Columns("RiskFactorCode")
            Sqlds.Tables(4).PrimaryKey = PrKey

            Dim PfKey(2) As DataColumn
            PfKey(0) = Sqlds.Tables(5).Columns("ClinicalHistoryId")
            PfKey(1) = Sqlds.Tables(5).Columns("PatientId")
            PfKey(2) = Sqlds.Tables(5).Columns("FamilySrNo")
            'PfKey(2) = Sqlds.Tables(5).Columns("RelationShip")
            'PfKey(3) = Sqlds.Tables(5).Columns("DiseaseCode")
            Sqlds.Tables(5).PrimaryKey = PfKey

            Dim PmKey(2) As DataColumn
            PmKey(0) = Sqlds.Tables(6).Columns("ClinicalHistoryId")
            PmKey(1) = Sqlds.Tables(6).Columns("PatientId")
            PmKey(2) = Sqlds.Tables(6).Columns("MedicalSrNo")
            Sqlds.Tables(6).PrimaryKey = PmKey

            Return Sqlds
        Catch ex As Exception
            MessageBox.Show("GetClinicalHistory :" & vbCrLf & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function GetClinicalHistId(ByVal PatientID As String, ByVal SiteCode As String) As String
        Try
            SqlQry.Length = 0
            'SqlQry.Append("Select MAX(ClinicalHistoryID) as ClinicalHistoryID from HcTrnClinicalHistory " & vbCrLf)
            SqlQry.Append("Select ClinicalHistoryID as ClinicalHistoryID from HcTrnClinicalHistory " & vbCrLf)
            SqlQry.Append("Where PatientID='" & PatientID & "' And SiteCode='" & SiteCode & "'; " & vbCrLf)

            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            SqlDt = New DataTable
            Sqlda.Fill(SqlDt)

            Dim ClinicalHistoryID As String = String.Empty
            If (SqlDt.Rows.Count = 1) Then
                If SqlDt.Rows(0)("ClinicalHistoryID") IsNot DBNull.Value Then
                    ClinicalHistoryID = SqlDt.Rows(0)("ClinicalHistoryID")
                Else
                    ClinicalHistoryID = ""
                End If
            End If

            Return ClinicalHistoryID
        Catch ex As Exception
            MessageBox.Show("GetClinicalHistory :" & vbCrLf & ex.Message)
            Return String.Empty
        End Try
    End Function

End Class
