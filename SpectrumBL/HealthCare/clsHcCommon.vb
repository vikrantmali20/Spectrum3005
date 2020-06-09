Imports System.Text
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Resources
Imports System.Windows.Forms

Public Class clsHcCommon
    Dim SqlQry As New StringBuilder     'SqlQry.Append("" & vbCrLf)
    Dim SqlTrans As SqlTransaction
    Dim Sqlda, Sqldau As SqlDataAdapter
    Dim SqlCmdb As SqlCommandBuilder
    Dim Sqlds As DataSet
    Dim Sqldr As DataRow
    Dim SqlDt As DataTable
    Dim PKey(1) As DataColumn

    Public Function GetDoctorInfo() As DataTable
        Try
            SqlQry.Length = 0
            SqlQry.Append("Select EmployeeCode, EmployeeName As DoctorName " & vbCrLf)
            SqlQry.Append("From HcMstEmployee " & vbCrLf)
            SqlQry.Append("Where DoctorType is not null And DoctorType=2 and Recstatus = 1 " & vbCrLf)
            SqlQry.Append("Order By EmployeeName" & vbCrLf)

            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            SqlDt = New DataTable
            Sqlda.Fill(SqlDt)

            Return SqlDt
        Catch ex As Exception
            MessageBox.Show("GetDoctorInfo :" & vbCrLf & ex.Message)
            Return Nothing
        End Try

    End Function
    Public Function GetPatientInfo(Optional ByVal PatientId As String = "") As DataTable
        Try
            SqlQry.Length = 0
            SqlQry.Append("select patientid, patientname, dateofbirth, ageyears, agemonths, gender, " & vbCrLf)
            SqlQry.Append("maritalstatus, nearestrelative, occupation, mothertongue, fathername, " & vbCrLf)
            SqlQry.Append("mothername, spousename, religon, primarytelphone, emailid, education " & vbCrLf)
            SqlQry.Append("from HcPatientDetail " & vbCrLf)

            If (PatientId <> String.Empty) Then
                SqlQry.Append("Where patientid ='" & PatientId & "'" & vbCrLf)
            End If

            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            SqlDt = New DataTable
            Sqlda.Fill(SqlDt)

            Return SqlDt
        Catch ex As Exception
            MessageBox.Show("GetPatientInfo :" & vbCrLf & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function GetNextDocNo(ByVal objName As String) As String
        Try
            Dim DocNo As String = String.Empty

            SqlQry.Length = 0
            SqlQry.Append("Select ObjectId,ObjectTypeId From GlNoRangeObjectsM " & vbCrLf)
            SqlQry.Append("Where ObjectName='" & objName & "'" & vbCrLf)

            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            SqlDt = New DataTable
            Sqlda.Fill(SqlDt)

            If SqlDt.Rows.Count > 0 Then
                Dim objTypeId As String = SqlDt.Rows(0)("ObjectTypeId")
                Dim objId As String = SqlDt.Rows(0)("ObjectId")

                SqlQry.Length = 0
                SqlQry.Append("Select CurrentNo From GlNoRangeObjects " & vbCrLf)
                SqlQry.Append("Where ObjectTypeId='" & objTypeId & "' " & vbCrLf)
                SqlQry.Append("And ObjectId='" & objId & "'" & vbCrLf)

                Sqlda.SelectCommand.CommandText = SqlQry.ToString
                SqlDt = New DataTable
                Sqlda.Fill(SqlDt)

                If SqlDt.Rows.Count > 0 Then
                    DocNo = SqlDt.Rows(0)("CurrentNo")
                End If
            End If

            Return DocNo
        Catch ex As Exception
            MessageBox.Show("GetNextDocNo :" & vbCrLf & ex.Message)
            Return String.Empty
        End Try

    End Function
    Public Function SetNextDocNo(ByVal objName As String, ByRef SqlCon As SqlConnection, ByRef SqlTran As SqlTransaction) As Boolean
        Try
            SqlQry.Length = 0
            SqlQry.Append("Select ObjectId,ObjectTypeId From GlNoRangeObjectsM " & vbCrLf)
            SqlQry.Append("Where ObjectName='" & objName & "'" & vbCrLf)

            Sqlda = New SqlDataAdapter(SqlQry.ToString, SqlCon)
            Sqlda.SelectCommand.Transaction = SqlTran
            SqlDt = New DataTable
            Sqlda.Fill(SqlDt)

            If SqlDt.Rows.Count > 0 Then
                Dim objTypeId As String = SqlDt.Rows(0)("ObjectTypeId")
                Dim objId As String = SqlDt.Rows(0)("ObjectId")

                SqlQry.Length = 0
                SqlQry.Append("Update GlNoRangeObjects Set " & vbCrLf)
                SqlQry.Append("UpdatedOn=GetDate(), CurrentNo = Convert(Numeric,CurrentNo) +1 " & vbCrLf)
                SqlQry.Append("Where ObjectTypeId='" & objTypeId & "' And ObjectId='" & objId & "'" & vbCrLf)

                Dim SqlCmd As New SqlCommand(SqlQry.ToString, SqlCon)
                SqlCmd.Transaction = SqlTran
                If SqlCmd.ExecuteNonQuery() > 0 Then
                    Return True
                End If
            End If

            Return False

        Catch ex As Exception
            MessageBox.Show("UpdateNextDocNo :" & vbCrLf & ex.Message)
        End Try
    End Function

    Public Function GetCodeDesc(ByVal vSiteCode As String, ByVal vCodeType As String) As DataTable
        Try
            SqlQry.Length = 0
            SqlQry.Append("Select Convert(bit,'False') As Checked, Code, ShortDesc from GeneralCodeMst " & vbCrLf)
            'SqlQry.Append("Where SiteCode='" & vSiteCode & "' And CodeType='" & vCodeType & "' " & vbCrLf)
            SqlQry.Append("Where CodeType='" & vCodeType & "' " & vbCrLf)
            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            SqlDt = New DataTable
            Sqlda.Fill(SqlDt)

            Return SqlDt

        Catch ex As Exception
            MessageBox.Show("GetClinicalHistory :" & vbCrLf & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function GetEmployeeInfo(ByVal vEmpCode As String) As String
        Try
            Dim OtherEmpDesignation As String = String.Empty
            SqlQry.Length = 0

            SqlQry.Append("select Gm.LongDesc from HcMstEmployee Em " & vbCrLf)
            SqlQry.Append("Inner Join GeneralCodeMst Gm On Em.DoctorType=Gm.Code " & vbCrLf)
            SqlQry.Append("Where Gm.CodeType='DoctorType' And Em.EmployeeCode = '" & vEmpCode & "'  " & vbCrLf)

            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            SqlDt = New DataTable
            Sqlda.Fill(SqlDt)

            If (SqlDt.Rows.Count > 0) Then
                OtherEmpDesignation = SqlDt.Rows(0)("LongDesc")
            End If

            Return OtherEmpDesignation

        Catch ex As Exception
            MessageBox.Show("GetClinicalHistory :" & vbCrLf & ex.Message)
            Return String.Empty
        End Try
    End Function


    Public Function ImageToByteArray(ByVal imageIn As System.Drawing.Image) As Byte()
        Try
            If (imageIn IsNot Nothing) Then
                Dim ms As New MemoryStream()
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Return ms.ToArray()
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show("RefreshPatientInfo :" & vbCrLf & ex.Message)
            Return Nothing
        End Try

    End Function
    Public Function ByteArrayToImage(ByVal byteArrayIn As Byte()) As Image
        Try
            If Not (byteArrayIn Is Nothing) Then
                Dim ms As New MemoryStream(byteArrayIn)
                Dim returnImage As Image = Image.FromStream(ms)

                Return returnImage
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("RefreshPatientInfo :" & vbCrLf & ex.Message)
            Return Nothing
        End Try

    End Function

    Public Function PrepareSaveData(ByRef dsMainTp As DataSet, ByVal TranDocType As String, ByVal IsNewDrApp As Boolean) As Boolean
        Try
            OpenConnection()
            SqlTrans = SpectrumCon.BeginTransaction()

            If SaveHMSInfo(dsMainTp, SpectrumCon, SqlTrans) = True Then

                If (IsNewDrApp = True) Then
                    SetNextDocNo(TranDocType, SpectrumCon, SqlTrans)
                End If

                SqlTrans.Commit()
                CloseConnection()

                SqlTrans.Dispose()
                Return True
            Else
                SqlTrans.Rollback()
                CloseConnection()
                Return False
            End If

        Catch ex As Exception
            SqlTrans.Rollback()
            CloseConnection()
            Console.WriteLine(ex.Message)
            MessageBox.Show("Error :" & vbCrLf & ex.Message)
            Return False
        End Try
    End Function
    Public Function SaveHMSInfo(ByVal dsDrApp As DataSet, ByRef SqlConn As SqlConnection, ByRef SqlTran As SqlTransaction) As Boolean
        Try
            For tabCount = 0 To dsDrApp.Tables.Count - 1
                Dim tableName As String = dsDrApp.Tables(tabCount).TableName
                SqlQry.Length = 0
                SqlQry.Append("Select * from " & tableName & vbCrLf)

                Dim daSaveInfo As New SqlClient.SqlDataAdapter(SqlQry.ToString, SqlConn)
                daSaveInfo.SelectCommand.CommandTimeout = 0
                daSaveInfo.SelectCommand.Transaction = SqlTrans

                Dim CmdbSaveInfo = New SqlCommandBuilder(daSaveInfo)
                daSaveInfo.TableMappings.Add(tableName, tableName)
                daSaveInfo = CmdbSaveInfo.DataAdapter

                daSaveInfo.Update(dsDrApp, tableName)
            Next

            Return True
        Catch ex As Exception

            Console.WriteLine(ex.Message)
            'MessageBox.Show("Save Clinical History :" & vbCrLf & ex.Message)
            Return False
        End Try
    End Function

    Public Function getDateDiff(ByVal strPatientId As String) As Integer
        OpenConnection()
        Dim strqry As String = "select datediff(dd,getDate(),(select max(a.billdate) from cashmemohdr a where clpno = '" & strPatientId & "'))"
        Dim scmd As New SqlCommand(strqry, SpectrumCon)

        Dim NoOfDay As Object = scmd.ExecuteScalar()

        Return If(NoOfDay Is DBNull.Value, 0, Convert.ToInt16(NoOfDay) * -1)

        'If Not scmd.ExecuteScalar() Is DBNull.Value Then
        '    Return scmd.ExecuteScalar()
        'Else
        '    Return 0
        'End If

    End Function

    Public Function GetSiteMasterInfo(ByVal vSiteCode As String) As DataSet
        Try
            Dim PKey(0) As DataColumn

            SqlQry.Length = 0
            SqlQry.Append("Select * from MstSite " & vbCrLf)
            If Not (String.IsNullOrEmpty(vSiteCode)) Then
                SqlQry.Append("Where SiteCode='" & vSiteCode & "';" & vbCrLf)
            End If

            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            PKey(0) = Sqlds.Tables(0).Columns("SiteCode")
            Sqlds.Tables(0).PrimaryKey = PKey

            Sqlds.Tables(0).TableName = "MstSite"
            Return Sqlds

        Catch ex As Exception
            MessageBox.Show("GetMstSiteInfo :" & vbCrLf & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function GetCountryStateInfo() As DataSet
        Try
            Dim PCKey(0) As DataColumn

            SqlQry.Length = 0
            SqlQry.Append("Select * from MstAreaCode Where AreaType In ('101','102','103') ; " & vbCrLf)

            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            PCKey(0) = Sqlds.Tables(0).Columns("AreaCode")
            Sqlds.Tables(0).PrimaryKey = PCKey

            Sqlds.Tables(0).TableName = "MstAreaCode"
            Return Sqlds

        Catch ex As Exception
            MessageBox.Show("GetCountryStateInfo :" & vbCrLf & ex.Message)
            Return Nothing
        End Try
    End Function
    Public Function CheckAreaCode(ByVal vAreaType As String, ByVal vAreaCode As String) As Boolean
        Try
            OpenConnection()
            SqlQry.Length = 0
            SqlQry.Append("Select Description from MstAreaCode Where AreaType = '" & vAreaType & "' And AreaCode='" & vAreaCode & "' ; " & vbCrLf)

            Dim cmd As New SqlCommand(SqlQry.ToString, SpectrumCon)
            Dim result As Object = cmd.ExecuteScalar
            CloseConnection()

            If (result IsNot Nothing) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            MessageBox.Show("GetCountryStateInfo :" & vbCrLf & ex.Message)
            Return Nothing
        End Try
    End Function
    Public Function GetAreaInformation(ByVal AreaType As String) As DataTable
        Try
            SqlQry.Length = 0

            If (AreaType = "Country") Then
                SqlQry.Append("Select AreaCode As CountryCode, Description, Status " & vbCrLf)
                SqlQry.Append("from MstAreaCode Where AreaType='101'; --Country " & vbCrLf)

            ElseIf (AreaType = "State") Then
                SqlQry.Append("Select AreaCode As StateCode, " & vbCrLf)
                SqlQry.Append("Description, dbo.FnGetDesc('CountryType',ParentCode,'') As Country, Status " & vbCrLf)
                SqlQry.Append("from MstAreaCode Where AreaType='102'; --State " & vbCrLf)

            ElseIf (AreaType = "City") Then
                SqlQry.Append("Select A.AreaCode As CityCode, A.Description, " & vbCrLf)
                SqlQry.Append("dbo.FnGetDesc('StateType',A.ParentCode,'') As State, " & vbCrLf)
                SqlQry.Append("dbo.FnGetDesc('CountryType',B.ParentCode,'') As Country, A.Status " & vbCrLf)
                SqlQry.Append("from MstAreaCode A, MstAreaCode B " & vbCrLf)
                SqlQry.Append("Where A.AreaType='103' And B.AreaType='102' " & vbCrLf)
                SqlQry.Append("And A.ParentCode=B.AreaCode; --City " & vbCrLf)
            End If

            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            SqlDt = New DataTable
            Sqlda.Fill(SqlDt)

            Return SqlDt

        Catch ex As Exception
            MessageBox.Show("GetAreaInformation :" & vbCrLf & ex.Message)
            Return Nothing
        End Try
    End Function
    Public Function DeleteAreaInfo(ByVal vAreaCode As String) As Boolean
        Try
            Dim cmdTrn As New SqlCommand("Delete from MstAreaCode Where AreaCode='" & vAreaCode & "' ", SpectrumCon)
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            CloseConnection()

            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function GetTerminalInfo() As DataSet
        Try
            Dim PTKey(0) As DataColumn
            SqlQry.Length = 0
            SqlQry.Append("Select * from MstTerminalID " & vbCrLf)

            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            PTKey(0) = Sqlds.Tables(0).Columns("TerminalID")
            Sqlds.Tables(0).PrimaryKey = PTKey

            Sqlds.Tables(0).TableName = "MstTerminalID"

            Return Sqlds

        Catch ex As Exception
            MessageBox.Show("GetTerminalInfo :" & vbCrLf & ex.Message)
            Return Nothing
        End Try
    End Function
    Public Function DeleteTerminalInfo(ByVal vTerminalID As String) As Boolean
        Try
            Dim cmdTrn As New SqlCommand("Delete from MstTerminalID Where TerminalID='" & vTerminalID & "' ", SpectrumCon)
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            CloseConnection()

            Return True
        Catch ex As Exception
            LogException(ex)    'Write Error Msg
            Return False
        End Try
    End Function

    Public Function PrepareToblankTheColumns(ByVal SiteCode As String, ByVal UserID As String, ByVal EmployeeCode As String) As Boolean
        Try
            Dim strUpdateQuery As String
            strUpdateQuery = "UPDATE AuthUsers set Password = NULL,PasswordUpdateDate = NULL " & vbCrLf
            strUpdateQuery += "where SiteCode = '" & SiteCode & "' and UserID = '" & UserID & "' and EmployeeCode = '" & EmployeeCode & "' "
            Dim cmdTrn As New SqlCommand(strUpdateQuery, SpectrumCon)
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            CloseConnection()

            Return True
        Catch ex As Exception
            LogException(ex)    'Write Error Msg
            Return False
        End Try
    End Function

    Public Function PrepareToResetDefaultPassword(ByVal SiteCode As String, ByVal UserID As String, ByVal Repass As String) As Boolean

        Try
            Dim strUpdateQuery As String
            strUpdateQuery = "UPDATE AuthUsers set Password ='" & Repass & "'," & vbCrLf
            strUpdateQuery += "where SiteCode = '" & SiteCode & "' and UserID = '" & UserID & "'"
            Dim cmdTrn As New SqlCommand(strUpdateQuery, SpectrumCon)
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            CloseConnection()

            Return True
        Catch ex As Exception
            LogException(ex)    'Write Error Msg
            Return False
        End Try
    End Function

    'created by Khusrao Adil
    Public Function GetPrescriptionDtlsStuc() As DataTable
        Try
            Dim strQuery As String
            '	[PrescriptionSrNo]
            strQuery = "select ArticleCode,ArticleDescription,EAN,Qty,ConsumptionRate,Duration,Remarks," & _
                       "ConsultantsNoteId,PatientId,SiteCode,NoteSrNo,CreatedAt,CreatedBy,CreatedOn,UpdatedAt," & _
                       "UpdatedBy,UpdatedOn,Status from HCPrescriptionDtls  Where 1=0"
            Dim dtTemp As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dtTemp)
            dtTemp.TableName = "HCPrescriptionDtls"

            ' pres.Columns.Add("SrNo", Type.GetType("System.String"))
            'pres.Columns.Add("ArticleCode", Type.GetType("System.String"))
            'pres.Columns.Add("ArticleDescription", Type.GetType("System.String"))
            'pres.Columns.Add("EAN", Type.GetType("System.String"))
            'pres.Columns.Add("Qty", Type.GetType("System.Decimal"))
            'pres.Columns.Add("ConsumptionRate", Type.GetType("System.String"))
            'pres.Columns.Add("Duration", Type.GetType("System.String"))
            'pres.Columns.Add("Remarks", Type.GetType("System.String"))
            'pres.Columns.Add("ConsultantsNoteId", Type.GetType("System.String"))
            'pres.Columns.Add("PatientId", Type.GetType("System.String"))
            'pres.Columns.Add("SiteCode", Type.GetType("System.String"))
            'pres.Columns.Add("NoteSrNo", Type.GetType("System.Int16"))
            'pres.Columns.Add("CREATEDAT", Type.GetType("System.String"))
            'pres.Columns.Add("CREATEDBY", Type.GetType("System.String"))
            'pres.Columns.Add("CREATEDON", Type.GetType("System.DateTime"))
            'pres.Columns.Add("UPDATEDAT", Type.GetType("System.String"))
            'pres.Columns.Add("UPDATEDBY", Type.GetType("System.String"))
            'pres.Columns.Add("UPDATEDON", Type.GetType("System.DateTime"))
            'pres.Columns.Add("STATUS", Type.GetType("System.Int16"))
            Return dtTemp
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
End Class
