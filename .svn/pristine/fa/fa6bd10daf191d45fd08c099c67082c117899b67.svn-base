Imports System.Text
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Resources
Imports System.Windows.Forms

Public Class clsHcPatientInfo

    Dim SqlQry As New StringBuilder     'SqlQry.Append("" & vbCrLf)
    Dim SqlTrans As SqlTransaction
    Dim Sqlda, Sqldau As SqlDataAdapter
    Dim SqlCmdb As SqlCommandBuilder
    Dim Sqlds As DataSet
    Dim Sqldr As DataRow
    Dim SqlDt As DataTable
    Dim PKey(1) As DataColumn
    Dim AKey(2) As DataColumn
    Dim HKey(1) As DataColumn

    Dim FilterQuery As String = String.Empty
    Public Function GetPatientInfoSchema(ByVal PatientId As String, ByVal SiteCode As String) As DataTable

        FilterQuery = String.Empty
        SqlQry.Length = 0
        SqlQry.Append(" SELECT clpcus.CardNo AS CardNo ,clpcus.ClpProgramId AS ClpProgramId,clpcus.Mobileno AS MobileNo,clpcus.Title," & vbCrLf)
        SqlQry.Append(" IsNull(dbo.FnGetDesc('Salutation', hcpod.Salutation,'" & SiteCode & "')+ ' ','')+ (clpcus.FirstName+' '+clpcus.surName)  AS PatientName," & vbCrLf)
        SqlQry.Append(" Convert(Varchar(10),clpcus.BirthDate,103) As BirthDate,clpcus.FirstName,clpcus.MiddleName,clpcus.SurName as SurName," & vbCrLf)
        'SqlQry.Append(" dbo.FnGetDesc('Gender',clpcus.Gender,'" & SiteCode & "') AS Gender," & vbCrLf)
        SqlQry.Append(" clpcus.Gender AS Gender," & vbCrLf)
        SqlQry.Append(" dbo.FnGetDesc('Doctor',hcpod.RefDoctorId,'" & SiteCode & "') AS RefDoctorId,hcpod.RefDoctorId AS RefDoctorCode, " & vbCrLf)
        SqlQry.Append(" Convert(Varchar(5),hcpod.AgeYears) + ' Years, ' + Convert(Varchar(5),hcpod.AgeMonths) + ' Months' AS Age," & vbCrLf)
        SqlQry.Append(" clpcus.Occupation AS Occupation, '' AS  FatherName,hcpod.Religon, " & vbCrLf)
        SqlQry.Append(" Convert(Varchar(10),hcpod.WeightKg) + ' Kg' As WeightKg, " & vbCrLf)
        SqlQry.Append(" clpcus.MaritalStatus AS MaritalStatus,hcpod.MotherTongue as MotherTongue," & vbCrLf)
        SqlQry.Append(" Convert(Varchar(10),hcpod.HeightCM) + ' cm' AS HeightCM, '' AS  PrimaryTelphone," & vbCrLf)
        SqlQry.Append(" clpcus.EmailId as EmailId, hcpod.NearestRelative,clpcus.Education As Education," & vbCrLf)
        SqlQry.Append(" hcpod.Nationality,hcpod.PatientImage,clpadd.AddressLn1,clpadd.AddressLn2," & vbCrLf)
        SqlQry.Append(" dbo.fnGetCodeDesc('103',clpAdd.CityCode) AS City,dbo.fnGetCodeDesc('102',clpAdd.StateCode) AS State," & vbCrLf)
        SqlQry.Append(" dbo.fnGetCodeDesc('101',clpAdd.CountryCode) AS Country,clpAdd.PinCode," & vbCrLf)
        SqlQry.Append(" clpcus.AccountNo AS AccountNo ,clpcus.SiteCode,clpcus.NameOnCard,clpcus.CardType,clpcus.Res_Phone,clpcus.OfficeNo," & vbCrLf)
        SqlQry.Append(" clpcus.SpouseFirstName,clpcus.ReferedBy,clpcus.UpdatedAt,clpcus.Status,clpcus.UpdatedBy,clpcus.UpdatedOn," & vbCrLf)
        SqlQry.Append(" clpcus.CreatedAt,clpcus.CreatedBy,clpcus.CreatedOn," & vbCrLf)
        SqlQry.Append(" clpCus.CardExpiryDT,clpCus.CardisActive,clpCus.CardIssueDT,clpCus.ReferedBy,clpCus.RelationWithPrimaryCard," & vbCrLf)
        SqlQry.Append(" clpCus.IsAddonCard,clpCus.PromotionInfobyEmail,clpCus.PromotionInfobySMS,clpCus.SpouseTitle,clpCus.SpouseFirstName," & vbCrLf)
        SqlQry.Append(" clpCus.SpouseMiddleName,clpCus.SpouseSurName,clpCus.SpouseDob,clpCus.SpouseOccupation,clpCus.MarriageAnivDate," & vbCrLf)
        SqlQry.Append(" clpCus.TotalBalancePoint,clpCus.PointsAccumlated,clpCus.PointsRedeemed,clpCus.OldCardType,clpCus.Password," & vbCrLf)
        SqlQry.Append(" clpCus.PasswordUpdateDate,clpCus.PasswordChangeNextDate,clpCus.PasswordExpiredon,clpCus.FacebookId," & vbCrLf)
        SqlQry.Append(" clpCus.TwitterId,clpCus.LinkedInID,clpCus.GooglePlusId,clpCus.Hi5Id,clpCus.MySpaceId,clpCus.IbiboId," & vbCrLf)
        SqlQry.Append(" clpCus.FourSquareId,clpCus.OrkutId,clpCus.SkypeId,clpCus.CustomerGroup,clpCus.resgistrationstatus,clpCus.CompanyName" & vbCrLf)


        SqlQry.Append("from  clpCustomers clpcus inner join HCPatientOtherDetails hcpod on clpcus.CardNo=hcpod.CardNo and clpcus.SiteCode=hcpod.SiteCode " & vbCrLf)
        SqlQry.Append("inner join CLPCustomerAddress clpAdd on clpcus.CardNo=clpAdd.CardNo and clpAdd.CardNo=hcpod.CardNo and clpcus.STATUS=1 " & vbCrLf)
        SqlQry.Append("and clpadd.STATUS=1 and clpAdd.AddressType=1 where " & vbCrLf)
        If PatientId <> String.Empty Then
            SqlQry.Append("clpcus.CardNo='" & PatientId & "' and hcpod.RecStatus='True' Order By clpcus.cardNo Desc " & vbCrLf)
        Else
            SqlQry.Append("clpcus.SiteCode='" & SiteCode & "' and hcpod.RecStatus='true'  Order By clpcus.cardNo Desc " & vbCrLf)
        End If
        Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
        SqlDt = New DataTable
        Sqlda.Fill(SqlDt)
        SqlDt.TableName = "CustomerDtl"
        PKey(0) = SqlDt.Columns("CardNo")
        PKey(1) = SqlDt.Columns("ClpProgramId")
        SqlDt.PrimaryKey = PKey
        Return SqlDt
    End Function
    Public Function GetPatientInfo(ByVal PatientId As String, ByVal SiteCode As String, ByVal IsSearchByID As Boolean) As DataSet
        Try
            FilterQuery = String.Empty

            'If (PatientId <> String.Empty) Then
            '    FilterQuery = "Where PatientId ='" & PatientId & "' And RecStatus='True'; " & vbCrLf
            'End If

            SqlQry.Length = 0
            'If (IsSearchByID = True) Then
            '    SqlQry.Append("Select PatientId, Mobile, IsNull(dbo.FnGetDesc('Salutation', Salutation,'" & SiteCode & "')+ ' ','') + PatientName As PatientName, " & vbCrLf)
            '    SqlQry.Append("Convert(Varchar(10),DateOfBirth,103) As DateOfBirth, FirstName, LastName, " & vbCrLf)
            '    SqlQry.Append("dbo.FnGetDesc('Gender',Gender,'" & SiteCode & "') As Gender, dbo.FnGetDesc('Doctor',RefDoctorId,'" & SiteCode & "') As RefDoctorId, " & vbCrLf)
            '    SqlQry.Append("RefDoctorId As RefDoctorCode, Convert(Varchar(5),AgeYears) + ' Years, ' + Convert(Varchar(5),AgeMonths) + ' Months' As Age, " & vbCrLf)
            '    SqlQry.Append("dbo.FnGetDesc('Occupation',Occupation,'" & SiteCode & "') As Occupation, FatherName, " & vbCrLf)
            '    SqlQry.Append("Religon, Convert(Varchar(10),WeightKg) + ' Kg' As WeightKg, " & vbCrLf)
            '    SqlQry.Append("dbo.FnGetDesc('MaritalStatus',MaritalStatus,'" & SiteCode & "') As MaritalStatus, " & vbCrLf)
            '    SqlQry.Append("Convert(Varchar(10),HeightCM) + ' cm' As HeightCM, PrimaryTelphone, " & vbCrLf)

            '    SqlQry.Append("EmailId, NearestRelative, dbo.FnGetDesc('Education',Education,'" & SiteCode & "') As Education, " & vbCrLf)
            '    SqlQry.Append("Nationality, PatientImage, AddressLine1, AddressLine2, " & vbCrLf)
            '    SqlQry.Append("dbo.fnGetCodeDesc('103',CityCode) As City, dbo.fnGetCodeDesc('102',StateCode) As State, " & vbCrLf)
            '    SqlQry.Append("dbo.fnGetCodeDesc('101',CountryCode) As Country, PinCode " & vbCrLf)
            '    SqlQry.Append("from HcPatientDetail Where RecStatus='True' Order By PatientId Desc " & vbCrLf)

            'Else

            'End If

            'If PatientId <> String.Empty Then
            '    FilterQuery = "clpcus.CardNo='" & PatientId & "' and hcpod.RecStatus='True' Order By clpcus.cardNo Desc "
            'ElseIf SiteCode <> String.Empty Then
            '    FilterQuery = "clpcus.SiteCode='" & SiteCode & "' and hcpod.RecStatus='True'  Order By clpcus.cardNo Desc "
            'Else
            '    FilterQuery = " Order By clpcus.cardNo Desc "
            'End If

            'SqlQry.Append("Select * from clpcustomers Where CardNo ='" & PatientId & "'" & vbCrLf)
            SqlQry.Append(" SELECT clpcus.CardNo AS CardNo ,clpcus.ClpProgramId AS ClpProgramId,clpcus.Mobileno AS MobileNo,clpcus.Title," & vbCrLf)
            SqlQry.Append(" IsNull(dbo.FnGetDesc('Salutation', hcpod.Salutation,'" & SiteCode & "')+ ' ','')+ (clpcus.FirstName+' '+clpcus.surName)  AS PatientName," & vbCrLf)
            SqlQry.Append(" Convert(Varchar(10),clpcus.BirthDate,103) As BirthDate,clpcus.FirstName,clpcus.MiddleName,clpcus.SurName as SurName," & vbCrLf)
            'SqlQry.Append(" dbo.FnGetDesc('Gender',clpcus.Gender,'" & SiteCode & "') AS Gender," & vbCrLf)
            SqlQry.Append(" clpcus.Gender AS Gender," & vbCrLf)
            SqlQry.Append(" dbo.FnGetDesc('Doctor',hcpod.RefDoctorId,'" & SiteCode & "') AS RefDoctorId,hcpod.RefDoctorId AS RefDoctorCode, " & vbCrLf)
            SqlQry.Append(" Convert(Varchar(5),hcpod.AgeYears) + ' Years, ' + Convert(Varchar(5),hcpod.AgeMonths) + ' Months' AS Age," & vbCrLf)
            SqlQry.Append(" dbo.FnGetDesc('Occupation',clpcus.Occupation,'" & SiteCode & "') AS Occupation, '' AS  FatherName,hcpod.Religon, " & vbCrLf)
            SqlQry.Append(" Convert(Varchar(10),hcpod.WeightKg) + ' Kg' As WeightKg, " & vbCrLf)
            SqlQry.Append(" dbo.FnGetDesc('MaritalStatus',clpcus.MaritalStatus,'" & SiteCode & "') AS MaritalStatus," & vbCrLf)
            SqlQry.Append(" Convert(Varchar(10),hcpod.HeightCM) + ' cm' AS HeightCM, '' AS  PrimaryTelphone," & vbCrLf)
            SqlQry.Append(" clpcus.EmailId as EmailId, hcpod.NearestRelative,dbo.FnGetDesc('Education',clpcus.Education,'" & SiteCode & "') As Education," & vbCrLf)
            SqlQry.Append(" hcpod.Nationality,hcpod.PatientImage,clpadd.AddressLn1,clpadd.AddressLn2," & vbCrLf)
            SqlQry.Append(" dbo.fnGetCodeDesc('103',clpAdd.CityCode) AS City,dbo.fnGetCodeDesc('102',clpAdd.StateCode) AS State," & vbCrLf)
            SqlQry.Append(" dbo.fnGetCodeDesc('101',clpAdd.CountryCode) AS Country,clpAdd.PinCode," & vbCrLf)
            SqlQry.Append(" clpcus.AccountNo AS AccountNo ,clpcus.SiteCode,clpcus.NameOnCard,clpcus.CardType,clpcus.Res_Phone,clpcus.OfficeNo," & vbCrLf)
            SqlQry.Append(" clpcus.SpouseFirstName,clpcus.ReferedBy,clpcus.UpdatedAt,clpcus.Status,clpcus.UpdatedBy,clpcus.UpdatedOn," & vbCrLf)
            SqlQry.Append(" clpcus.CreatedAt,clpcus.CreatedBy,clpcus.CreatedOn," & vbCrLf)
            SqlQry.Append(" clpCus.CardExpiryDT,clpCus.CardisActive,clpCus.CardIssueDT,clpCus.ReferedBy,clpCus.RelationWithPrimaryCard," & vbCrLf)
            SqlQry.Append(" clpCus.IsAddonCard,clpCus.PromotionInfobyEmail,clpCus.PromotionInfobySMS,clpCus.SpouseTitle,clpCus.SpouseFirstName," & vbCrLf)
            SqlQry.Append(" clpCus.SpouseMiddleName,clpCus.SpouseSurName,clpCus.SpouseDob,clpCus.SpouseOccupation,clpCus.MarriageAnivDate," & vbCrLf)
            SqlQry.Append(" clpCus.TotalBalancePoint,clpCus.PointsAccumlated,clpCus.PointsRedeemed,clpCus.OldCardType,clpCus.Password," & vbCrLf)
            SqlQry.Append(" clpCus.PasswordUpdateDate,clpCus.PasswordChangeNextDate,clpCus.PasswordExpiredon,clpCus.FacebookId," & vbCrLf)
            SqlQry.Append(" clpCus.TwitterId,clpCus.LinkedInID,clpCus.GooglePlusId,clpCus.Hi5Id,clpCus.MySpaceId,clpCus.IbiboId," & vbCrLf)
            SqlQry.Append(" clpCus.FourSquareId,clpCus.OrkutId,clpCus.SkypeId,clpCus.CustomerGroup,clpCus.resgistrationstatus,clpCus.CompanyName" & vbCrLf)


            SqlQry.Append("from  clpCustomers clpcus inner join HCPatientOtherDetails hcpod on clpcus.CardNo=hcpod.CardNo and clpcus.SiteCode=hcpod.SiteCode " & vbCrLf)
            SqlQry.Append("inner join CLPCustomerAddress clpAdd on clpcus.CardNo=clpAdd.CardNo and clpAdd.CardNo=hcpod.CardNo and clpcus.STATUS=1 " & vbCrLf)
            SqlQry.Append("and clpadd.STATUS=1 and clpAdd.AddressType=1 where " & vbCrLf)
            If PatientId <> String.Empty Then
                SqlQry.Append("clpcus.CardNo='" & PatientId & "' and hcpod.RecStatus='True' Order By clpcus.cardNo Desc " & vbCrLf)
            Else
                SqlQry.Append("clpcus.SiteCode='" & SiteCode & "' and hcpod.RecStatus='true'  Order By clpcus.cardNo Desc " & vbCrLf)
            End If
            'SqlQry.Append("Select *,'' as RefDoctorId,'' as ReferedBy,'' as PrimaryTelphone from clpcustomers Where " + FilterQuery & vbCrLf)
            SqlQry.Append("Select * from clpcustomeraddress Where CardNo ='" & PatientId & "' " & vbCrLf)
            SqlQry.Append("Select * from HcPatientDocs Where CardNo ='" & PatientId & "' " & vbCrLf)
            SqlQry.Append("Select * from HCPatientOtherDetails Where CardNo ='" & PatientId & "' and SiteCode='" & SiteCode & "' and RecStatus='True'  Order By cardNo Desc " & vbCrLf)

            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)
            'Sqlds.Tables(0).TableName = "HcPatientDetail"
            Sqlds.Tables(0).TableName = "clpcustomers"
            Sqlds.Tables(1).TableName = "clpcustomeraddress"
            Sqlds.Tables(2).TableName = "HcPatientDocs"
            Sqlds.Tables(3).TableName = "HCPatientOtherDetails"

            PKey(0) = Sqlds.Tables(0).Columns("CardNo")
            PKey(1) = Sqlds.Tables(0).Columns("ClpProgramId")
            Sqlds.Tables(0).PrimaryKey = PKey

            AKey(0) = Sqlds.Tables(1).Columns("CardNo")
            AKey(1) = Sqlds.Tables(1).Columns("ClpProgramId")
            AKey(2) = Sqlds.Tables(1).Columns("AddressType")
            Sqlds.Tables(1).PrimaryKey = AKey


            HKey(0) = Sqlds.Tables(3).Columns("CardNo")
            HKey(1) = Sqlds.Tables(3).Columns("ClpProgramId")
            Sqlds.Tables(3).PrimaryKey = HKey

            Return Sqlds
        Catch ex As Exception
            MessageBox.Show("GetPatientInfo :" & vbCrLf & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function IsPatientPrescriptionAvaliable(ByVal patientId) As Boolean
        Try
            IsPatientPrescriptionAvaliable = False
            FilterQuery = String.Empty
            SqlQry.Length = 0
            SqlQry.Append("select * from HCPrescriptionDtls " & vbCrLf)
            SqlQry.Append("Where status=1 and PatientId='" & patientId & "' " & vbCrLf)
            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            SqlDt = New DataTable
            Sqlda.Fill(SqlDt)
            If SqlDt.Rows.Count > 0 Then
                IsPatientPrescriptionAvaliable = True
            End If
            Return IsPatientPrescriptionAvaliable
        Catch ex As Exception
            MessageBox.Show("GetPatientInfo :" & vbCrLf & ex.Message)
            Return False
        End Try

    End Function
    Public Function GetLatestPrescriptionOfPatient(ByVal patientId, ByVal siteCode) As DataTable
        Try
            SqlQry.Length = 0
            'SqlQry.Append("select ROW_NUMBER() OVER (ORDER BY ArticleCode) AS SrNo,* from HCPrescriptionDtls where PatientId='" & patientId & "' and STATUS=1 and SiteCode='" & siteCode & "' and" & vbCrLf)
            'SqlQry.Append("NoteSrNo=(select max(NoteSrNo)-1 from HCPrescriptionDtls where PatientId='" & patientId & "' and status=1 and SiteCode='" & siteCode & "')" & vbCrLf)



            SqlQry.Append("select ROW_NUMBER() OVER (ORDER BY pd.ArticleCode) AS SrNo,pd.ArticleCode,pd.ArticleDescription,pd.EAN,pd.Qty," & vbCrLf)
            SqlQry.Append("pd.ConsumptionRate,pd.Duration,pd.Remarks,pd.ConsultantsNoteId,pd.PatientId,pd.SiteCode,pd.NoteSrNo,pd.CREATEDAT,pd.CREATEDBY," & vbCrLf)
            SqlQry.Append("pd.CREATEDON,pd.UPDATEDAT,pd.UPDATEDBY,pd.updatedOn,pd.STATUS,cn.ConsultantsNoteText from HCPrescriptionDtls pd" & vbCrLf)
            SqlQry.Append("inner join HcTrnConsultantsNote cn on pd.ConsultantsNoteId=cn.ConsultantsNoteId" & vbCrLf)
            SqlQry.Append(" where pd.PatientId='" & patientId & "' and pd.STATUS=1 and pd.SiteCode='" & siteCode & "' and" & vbCrLf)
            SqlQry.Append("pd.NoteSrNo=(select max(NoteSrNo) from HcTrnConsultantsNote where PatientId='" & patientId & "' and Recstatus=1 and SiteCode='" & siteCode & "')" & vbCrLf)
            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            SqlDt = New DataTable
            Sqlda.Fill(SqlDt)
            Return SqlDt
        Catch ex As Exception
            MessageBox.Show("Latest HCPrescriptionDtls info :" & vbCrLf & ex.Message)
            Return Nothing
        End Try
    End Function
    Public Function GetMobileNo(ByVal mobileNo As String) As Boolean
        Try
            GetMobileNo = False
            FilterQuery = String.Empty

            SqlQry.Length = 0
            SqlQry.Append("Select * from CLPCustomers " & vbCrLf)
            SqlQry.Append("Where Mobileno='" & mobileNo & "' " & vbCrLf)

            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            SqlDt = New DataTable
            Sqlda.Fill(SqlDt)
            Dim strMobile As String = SqlDt.Rows(0)("Mobileno").ToString()
            If strMobile <> "" Or Nothing Then
                GetMobileNo = True
            End If
            Return GetMobileNo
        Catch ex As Exception
            MessageBox.Show("GetPatientInfo :" & vbCrLf & ex.Message)
            Return Nothing
        End Try
    End Function
    Public Function GetPatientDocInfo(ByVal PatientId As String, ByVal SiteCode As String) As DataTable
        Try
            FilterQuery = String.Empty

            SqlQry.Length = 0
            SqlQry.Append("Select 'MainNode' As MainNode ,gmst.LongDesc As ParentNode, hcpd.DocDescription As ChildNode, hcpd.DocPath " & vbCrLf)
            SqlQry.Append("from HcPatientDocs hcpd " & vbCrLf)
            SqlQry.Append("Inner Join GeneralCodeMst gmst On hcpd.DocumentType=gmst.Code " & vbCrLf)
            ' SqlQry.Append("Where gmst.CodeType='DocumentType' And PatientID='" & PatientId & "' " & vbCrLf)
            SqlQry.Append("Where gmst.CodeType='DocumentType' And CardNo='" & PatientId & "' " & vbCrLf)
            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            SqlDt = New DataTable
            Sqlda.Fill(SqlDt)

            SqlDt.TableName = "HcPatientDocs"

            Return SqlDt
        Catch ex As Exception
            MessageBox.Show("GetPatientInfo :" & vbCrLf & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function GetDoctorInfo(ByVal DrCode As String) As DataTable
        Try
            FilterQuery = String.Empty

            SqlQry.Length = 0
            SqlQry.Append("Select * from HcMstEmployee " & vbCrLf)
            SqlQry.Append("Where EmployeeCode='" & DrCode & "' " & vbCrLf)

            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            SqlDt = New DataTable
            Sqlda.Fill(SqlDt)

            SqlDt.TableName = "HcMstEmployee"

            Return SqlDt
        Catch ex As Exception
            MessageBox.Show("GetDoctorInfo :" & vbCrLf & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function GetAuthorInfo(ByVal vAuthorCode As String) As DataTable
        Try
            FilterQuery = String.Empty
            If Not (vAuthorCode = String.Empty) Then
                FilterQuery = "Where EmployeeCode='" & vAuthorCode & "'; "
            End If

            SqlQry.Length = 0
            SqlQry.Append("Select EmployeeCode, dbo.FnGetDesc('DoctorType',DoctorType,'0000588') As DoctorType" & vbCrLf)
            SqlQry.Append(", FirstName, LastName, dbo.FnGetDesc('Gender',Gender,'0000588') As Gender" & vbCrLf)
            SqlQry.Append(", dbo.FnGetDesc('BloodGroup',BloodGroup,'0000588') As BloodGroup" & vbCrLf)
            SqlQry.Append(", DateOfBirth, AddressLine1, AddressLine2" & vbCrLf)
            SqlQry.Append(", dbo.fnGetCodeDesc('103',CityCode) As City" & vbCrLf)
            SqlQry.Append(", dbo.fnGetCodeDesc('102',StateCode) As State" & vbCrLf)
            SqlQry.Append(", dbo.fnGetCodeDesc('101',CountryCode) As Country, PinCode" & vbCrLf)
            SqlQry.Append(", dbo.FnGetDesc('Education',Education,'0000588') As Education" & vbCrLf)
            SqlQry.Append(", Nationality, EmailId, Mobile from HcMstEmployee" & FilterQuery & vbCrLf)

            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            SqlDt = New DataTable
            Sqlda.Fill(SqlDt)

            SqlDt.TableName = "HcMstEmployee"

            Return SqlDt
        Catch ex As Exception
            MessageBox.Show("GetDoctorInfo :" & vbCrLf & ex.Message)
            Return Nothing
        End Try
    End Function
    Public Function GetPatientInformation(ByVal SiteCode As String, ByVal CLpProgramId As String) As DataTable
        Try
            FilterQuery = String.Empty
            Dim obj As New clsHcClinicalHistory

            Dim dtPatientSymptoms As New DataTable
            'Dim dtPatientPreviousIntervention As New DataTable
            Dim dtPatientClinicalHistInvTreat As New DataTable
            'Dim dtPatientRiskFactor As New DataTable
            Dim dtPatientFamilyHistory As New DataTable
            Dim dtPatientMedicalHistory As New DataTable
            SqlQry.Length = 0
            SqlQry.Append("SELECT DISTINCT  ROW_NUMBER() OVER (ORDER BY CD.CARDNO) AS SrNo,CD.CARDNO AS PATIENTID, " & vbCrLf)
            SqlQry.Append("CD.MOBILENO,ISNULL(CD.FIRSTNAME,'') AS FIRSTNAME,ISNULL(CD.SURNAME,'') AS SURNAME," & vbCrLf)
            SqlQry.Append("AD.ADDRESSLN1 AS ADDRESSLINE1,AD.ADDRESSLN2 AS ADDRESSLINE2,ISNULL(DBO.FNGETLOCNDESC(AD.CITYCODE) ,'') AS CITY, " & vbCrLf)
            SqlQry.Append(" AD.PINCODE  FROM CLPCUSTOMERS CD" & vbCrLf)
            SqlQry.Append(" LEFT OUTER JOIN CLPCUSTOMERADDRESS AD ON CD.CARDNO=AD.CARDNO AND CD.CLPPROGRAMID=AD.CLPPROGRAMID " & vbCrLf)
            SqlQry.Append(" AND AD.Status=1 AND AD.DEFAULTS=1" & vbCrLf)
            SqlQry.Append("INNER JOIN CLPPROGRAMSITEMAP CLPID ON CD.CLPPROGRAMID=CLPID.CLPPROGRAMID  " & vbCrLf)
            SqlQry.Append(" AND CLPID.Status=1 " & vbCrLf)
            SqlQry.Append(" INNER JOIN MstCLPProgram CLP ON CLPID.CLPProgramID = CLP.CLPProgramID " & vbCrLf)
            SqlQry.Append(" WHERE CD.Status=1 and CD.CLPProgramID='" & CLpProgramId & "' and AD.Addresstype=1" & vbCrLf)


            'SqlQry.Append("Select ClinicalHistoryId,PatientId,SrNo,SiteCode,SymptomDesc,Duration,DMY,RecStatus from HcTrnSymptoms Where PatientId ='" & PatientId & "'  and SiteCode='" & SiteCode & "'" & vbCrLf)
            ''SqlQry.Append("Select * from HcTrnPreviousIntervention Where PatientId ='" & PatientId & "'  and SiteCode='" & SiteCode & "'" & vbCrLf)
            'SqlQry.Append("Select * from HcTrnClinicalHistInvTreat Where PatientId ='" & PatientId & "'  and SiteCode='" & SiteCode & "'" & vbCrLf)
            ''SqlQry.Append("Select * from HcTrnRiskFactor Where PatientId ='" & PatientId & "'  and SiteCode='" & SiteCode & "'" & vbCrLf)
            'SqlQry.Append("Select * from HcTrnFamilyHistory Where PatientId ='" & PatientId & "'  and SiteCode='" & SiteCode & "'" & vbCrLf)
            'SqlQry.Append("Select * from HcTrnMedicalHistory Where PatientId ='" & PatientId & "'  and SiteCode='" & SiteCode & "'" & vbCrLf)
            'SqlQry.Append("Select * from HcTrnClinicalHistory Where PatientId ='" & PatientId & "'  and SiteCode='" & SiteCode & "'" & vbCrLf)

            Sqlda = New SqlDataAdapter(SqlQry.ToString, ConString)
            SqlDt = New DataTable
            Dim ds As New DataSet
            Sqlda.Fill(SqlDt)
            SqlDt.TableName = "PatientInfo"
            Return SqlDt
        Catch ex As Exception
            MessageBox.Show("GetPatientInfo :" & vbCrLf & ex.Message)
            Return Nothing
        End Try
    End Function
End Class
