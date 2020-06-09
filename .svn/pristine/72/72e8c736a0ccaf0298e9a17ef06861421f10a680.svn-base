Imports System.Data
Imports System.Data.SqlClient
Imports System.Text

Public Class clsHealthCare

#Region "Customer Classify"
    ''' <summary>
    ''' Get Customer classification Drop Down Data
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomerClassData() As DataTable
        Try
            Dim StmtQry As New StringBuilder

            StmtQry.Length = 0
            StmtQry.Append(" select Code,ShortDesc from GeneralCodemst where CodeType='CustomerClassType' and Status=1" & vbCrLf)
            Dim daCustClass As New SqlDataAdapter
            daCustClass = New SqlClient.SqlDataAdapter(StmtQry.ToString, SpectrumCon)
            Dim dtCustClass As New DataTable
            daCustClass.Fill(dtCustClass)

            Return dtCustClass
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    ''' <summary>
    ''' Insert Customer Classify Data 
    ''' </summary>
    ''' <param name="CardNo"></param>
    ''' <param name="ClpProgramId"></param>
    ''' <param name="siteCode"></param>
    ''' <param name="userId"></param>
    ''' <param name="Classify"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertCustomerClassData(ByVal CardNo As String, ByVal ClpProgramId As String, ByVal siteCode As String, ByVal userId As String, ByVal Classify As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim cmd As New System.Text.StringBuilder()
            Dim objcomm As New clsCommon

            cmd.Append(" INSERT INTO HCPatientOtherDetails( ")
            cmd.Append(" CardNo, ClpProgramId, siteCode, Salutation, RefDoctorId, AgeYears, AgeMonths, NearestRelative,")
            cmd.Append("  MontlyIncome, MotherTongue, FatherName, MotherName, Religon, HeightCM, WeightKg, BloodGroup, NatureOfBody,")
            cmd.Append("  Nationality, StayDurYearsL, StayDurMonthsL, StayDurYearsP, StayDurMonthsP, PatientImage, RecStatus, Classify,")
            cmd.Append("  CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS )")
            cmd.Append(" Values(")
            cmd.Append("'" & CardNo & "','" & ClpProgramId & "','" & siteCode & "',")
            cmd.Append("'','','0','0','',0,'','','','','0','0','','','','0','0','0','0','',1,'" & Classify & "',")
            cmd.Append("'" & siteCode & "','" & userId & "',GetDate(), ")
            cmd.Append("'" & siteCode & "','" & userId & "', GetDate(),1")
            cmd.Append(") ;")

            If objcomm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If
            cmd.Length = 0
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    ''' <summary>
    ''' Updating Customer Classify Data 
    ''' </summary>
    ''' <param name="CardNo"></param>
    ''' <param name="ClpProgramId"></param>
    ''' <param name="siteCode"></param>
    ''' <param name="userId"></param>
    ''' <param name="Classify"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateCustomerClassData(ByVal CardNo As String, ByVal ClpProgramId As String, ByVal siteCode As String, ByVal userId As String, ByVal Classify As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim cmd As New System.Text.StringBuilder()
            Dim objcomm As New clsCommon

            cmd.Append("update HCPatientOtherDetails set Classify='" & Classify & "' ")
            cmd.Append(" ,UpdatedAt='" & siteCode & "',UpdatedBy='" & userId & "',")
            cmd.Append("  UpdatedOn=GetDate() where CardNo='" & CardNo & "' and CLPProgramId='" & ClpProgramId & "'")

            If objcomm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If
            cmd.Length = 0
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function



    ''' <summary>
    ''' Get Customer Classify Data By Customer Details
    ''' </summary>
    ''' <param name="CardNo"></param>
    ''' <param name="ClpProgramId"></param>
    ''' <param name="siteCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomerClassDataByCustomerNo(ByVal CardNo As String, ByVal ClpProgramId As String, ByVal siteCode As String) As DataTable
        Try
            Dim StmtQry As New StringBuilder

            StmtQry.Length = 0
            StmtQry.Append(" select * from HCPatientOtherDetails where CardNo='" & CardNo & "' AND ClpProgramId='" & ClpProgramId & "' and SiteCode='" & siteCode & "' " & vbCrLf)
            Dim daCustClass As New SqlDataAdapter
            daCustClass = New SqlClient.SqlDataAdapter(StmtQry.ToString, SpectrumCon)
            Dim dtCustClass As New DataTable
            daCustClass.Fill(dtCustClass)

            Return dtCustClass
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
#End Region

End Class
