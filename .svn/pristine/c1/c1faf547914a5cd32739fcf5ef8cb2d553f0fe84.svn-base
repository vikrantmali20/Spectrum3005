Imports System.Text
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Resources
Imports System.Windows.Forms

Public Class clsHcConsultantsNotes
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

    Public Function GetDrNotesInfo(ByVal vPatientId As String, ByVal vDoctorType As String) As DataSet
        Try
            FilterQuery = String.Empty
            If Not (vPatientId = String.Empty) Then
                FilterQuery = "Where PatientID='" & vPatientId & "' and DoctorType = '" & vDoctorType & "' Order By Convert(varchar,CreatedOn,113) DESC; " & vbCrLf
            End If
            SqlQry.Length = 0
            SqlQry.Append("Select *, CONVERT(CHAR(10),CreatedOn,3) + SUBSTRING(CONVERT(varchar,CreatedOn,0),12,8) As CurrentDate from HcTrnConsultantsNote " & FilterQuery & vbCrLf)

            'SqlQry.Append("Select *,Convert(varchar(8),CreatedOn,112) As CurrentDate from HcTrnConsultantsNote " & FilterQuery & vbCrLf)
            'SqlQry.Append(" select hcnn.NoteSrNo As NoteSrNo,hcnn.ConsultantsNoteId AS ConsultantsNoteId,ROW_NUMBER() OVER (ORDER BY hcpd.CreatedOn) As SrNo,hcpd.ArticleCode AS ArticleCode,hcpd.ArticleDescription AS ArticleDescription, hcpd.Qty as Quantity,hcpd.ConsumptionRate AS ConsumptionRate,hcpd.Duration AS Duration,hcpd.Remarks AS Remarks from HcTrnConsultantsNote hcnn left join HCPrescriptionDtls  hcpd  on  hcnn.ConsultantsNoteId=hcpd.ConsultantsNoteId and hcnn.NoteSrNo=hcpd.NoteSrNo where hcnn.PatientId ='000-KOC-16' and hcnn.ConsultantsNoteId=hcpd.ConsultantsNoteId")
            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            SqlCmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "HcTrnConsultantsNote"
            'Sqlds.Tables(1).TableName = "HCPrescriptionDtlsPrevious"
            Return Sqlds


        Catch ex As Exception
            MessageBox.Show("GetDrNotesInfo :" & vbCrLf & ex.Message)
            Return Nothing
        End Try

    End Function
    Public Function GetTo_dayDrAppointmentInfo(ByVal DrId As String, ByVal selectedDate As String) As DataTable
        Try
            SqlQry.Length = 0

            SqlQry.Append("Select PatientId, CONVERT(varchar, StartDate,8) As AppTime,PatientName " & vbCrLf)
            SqlQry.Append("from HcTrnAppointment " & vbCrLf)
            SqlQry.Append("Where DoctorCode= '" & DrId & "' And Convert(varchar(8),StartDate,112) = '" & selectedDate & "' " & vbCrLf)
            SqlQry.Append("Order By AppTime" & vbCrLf)

            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            SqlDt = New DataTable
            Sqlda.Fill(SqlDt)

            SqlDt.TableName = "TodayDrApp"
            Return SqlDt

        Catch ex As Exception
            MessageBox.Show("Error:" & vbAbort & ex.Message)
            Return Nothing
        End Try
    End Function
    Public Function GetPatientPrescriptionInfo(ByVal patientId As String, ByVal NoteSrNo As String)
        Try
            SqlQry.Length = 0
            

            SqlQry.Append("select ROW_NUMBER() OVER (ORDER BY CreatedOn) As SrNo,NoteSrNo,ArticleCode,ArticleDescription," & vbCrLf)
            SqlQry.Append("Qty,ConsumptionRate,Duration,Remarks from HCPrescriptionDtls " & vbCrLf)
            SqlQry.Append("Where PatientId= '" & patientId & "' and Status=1" & vbCrLf)
            If NoteSrNo <> "" Then
                SqlQry.Append("and NoteSrNo='" & NoteSrNo & "'" & vbCrLf)
            End If
            ' SqlQry.Append("ORder by Updatedon desc")
            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            SqlDt = New DataTable
            Sqlda.Fill(SqlDt)

            SqlDt.TableName = "HCPrescriptionDtlsPrevious"
            Return SqlDt

        Catch ex As Exception
            MessageBox.Show("Error:" & vbAbort & ex.Message)
            Return Nothing
        End Try
    End Function
End Class
