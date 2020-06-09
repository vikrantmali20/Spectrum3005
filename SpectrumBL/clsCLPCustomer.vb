Imports System.Data
Imports System.Data.SqlClient

Public Class clsCLPCustomer
    Inherits clsCommon
    Dim Sqlda_CLP As SqlDataAdapter
    Dim Sqlcmdb_CLP As SqlCommandBuilder
    Dim Sqlcmd_CLP As SqlCommand
    Dim Sqlds_CLP As DataSet
    Dim Sqldt_CLP As DataTable

    Dim vStmtQry As New System.Text.StringBuilder
    Dim objComn As New clsCommon

    Private _CustomerSearchParameters As String
    Public Property CustomerSearchParameters() As String
        Get
            Return _CustomerSearchParameters
        End Get
        Set(ByVal value As String)
            _CustomerSearchParameters = value
        End Set
    End Property

    Public Function GetComboDataSet() As DataSet
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select AreaCode, Description,ParentCode from MSTAreaCode Where AreaType='103'; " & vbCrLf)               '1 City
            vStmtQry.Append("Select AreaCode, Description,ParentCode from MSTAreaCode Where AreaType='102'; " & vbCrLf)               '2 State
            vStmtQry.Append("Select AreaCode, Description,ParentCode from MSTAreaCode Where AreaType='101'; " & vbCrLf)               '3 Country
            vStmtQry.Append("Select Code, ShortDesc from GeneralCodeMst Where Status='1' and CodeType='TITLE'; " & vbCrLf) '4 Title
            vStmtQry.Append("Select Code, ShortDesc from GeneralCodeMst Where CodeType='Gender'; " & vbCrLf)               '5 Gender
            vStmtQry.Append("Select Code, ShortDesc from GeneralCodeMst Where CodeType='MARITALSTATUS'" & vbCrLf)          '6 Marital
            vStmtQry.Append("Select Code, ShortDesc from GeneralCodeMst Where CodeType='EDUCATION'" & vbCrLf)              '7 Education
            vStmtQry.Append("Select Code, ShortDesc from GeneralCodeMst Where CodeType='OCCUPATION'" & vbCrLf)             '8 Occupation
            vStmtQry.Append("Select Code, ShortDesc from GeneralCodeMst Where CodeType='CustomerType'" & vbCrLf)            '9 AddressType

            Sqlda_CLP = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb_CLP = New SqlClient.SqlCommandBuilder(Sqlda_CLP)
            Sqlds_CLP = New DataSet
            Sqlda_CLP.Fill(Sqlds_CLP)

            Sqlds_CLP.Tables(0).TableName = "CityTab"
            Sqlds_CLP.Tables(1).TableName = "StateTab"
            Sqlds_CLP.Tables(2).TableName = "CountryTab"
            Sqlds_CLP.Tables(3).TableName = "TitleTab"
            Sqlds_CLP.Tables(4).TableName = "GenderTab"
            Sqlds_CLP.Tables(5).TableName = "MaritalTab"
            Sqlds_CLP.Tables(6).TableName = "EducationTab"
            Sqlds_CLP.Tables(7).TableName = "OccupationTab"
            Sqlds_CLP.Tables(8).TableName = "CustomerTypeTab"

            Return Sqlds_CLP
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Save Customer Information
    ''' </summary>
    ''' <param name="dsSaveData"></param>
    ''' <param name="NextDocDesc"></param>
    ''' <param name="IsNextDocNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function SaveData(ByRef dsSaveData As DataSet, ByVal NextDocDesc As String, ByVal IsNextDocNo As Boolean) As Boolean
        Dim SqlTrans As SqlTransaction
        Try
            OpenConnection()
            SqlTrans = SpectrumCon.BeginTransaction()

            For TbCnt = 0 To dsSaveData.Tables.Count - 1
                Dim tableName As String = dsSaveData.Tables(TbCnt).TableName
                Dim tableColumns As String = ""

                For ColIndex = 0 To dsSaveData.Tables(TbCnt).Columns.Count - 1
                    tableColumns = tableColumns & ", " & dsSaveData.Tables(TbCnt).Columns(ColIndex).ColumnName.ToString()
                Next
                tableColumns = tableColumns.Substring(1)

                vStmtQry.Length = 0
                vStmtQry.Append("SELECT " + tableColumns & " FROM " & tableName)

                Sqlda_CLP = New SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
                Sqlda_CLP.SelectCommand.Transaction = SqlTrans

                Sqlcmdb_CLP = New SqlCommandBuilder(Sqlda_CLP)
                Sqlda_CLP.TableMappings.Add(tableName, tableName)
                Sqlda_CLP = Sqlcmdb_CLP.DataAdapter
                Sqlda_CLP.Update(dsSaveData, tableName)
            Next
            If IsNextDocNo = True Then
                If objComn.UpdateDocumentNo(NextDocDesc, SpectrumCon, SqlTrans) = False Then
                    SqlTrans.Rollback()
                    CloseConnection()
                    Return False
                End If
            End If

            SqlTrans.Commit()
            CloseConnection()
            Return True

        Catch ex As Exception
            SqlTrans.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function


    'Public Function GetClpCustomerInfom() As DataSet
    '    Try
    '        vStmtQry.Length = 0
    '        vStmtQry.Append(" SELECT CD.ACCOUNTNO as CUSTOMERNO, CD.CARDNO, CD.CARDTYPE, TITLE + ' ' +  CD.FIRSTNAME + ' ' + CD.SURNAME AS CUSTOMERNAME, " & vbCrLf)
    '        vStmtQry.Append(" DBO.FNGETMSTDESC('GENDER',CD.GENDER) AS GENDER, CD.TOTALBALANCEPOINT AS BALANCEPOINT, " & vbCrLf)
    '        vStmtQry.Append(" CONVERT(VARCHAR(12), CD.BIRTHDATE,105) AS BIRTHDATE, CD.EMAILID, CD.MOBILENO, CD.Res_Phone as ResPhone, " & vbCrLf)
    '        vStmtQry.Append(" AD.ADDRESSLN1, AD.ADDRESSLN2, AD.ADDRESSLN3, AD.ADDRESSLN4, DBO.FNGETLOCNDESC(AD.CITYCODE) AS CITY, " & vbCrLf)
    '        vStmtQry.Append(" DBO.FNGETLOCNDESC(AD.STATECODE) AS STATE,DBO.FNGETLOCNDESC(AD.COUNTRYCODE) AS COUNTRY, " & vbCrLf)
    '        vStmtQry.Append(" AD.PINCODE, CD.SITECODE, AD.ADDRESSTYPE FROM CLPCUSTOMERS CD, CLPCUSTOMERADDRESS AD " & vbCrLf)
    '        vStmtQry.Append(" WHERE CD.CARDNO=AD.CARDNO AND CD.STATUS=1" & vbCrLf)

    '        Sqlda_CLP = New SqlClient.SqlDataAdapter(vStmtQry.ToString, ConString)
    '        Sqlds_CLP = New DataSet
    '        Sqlda_CLP.Fill(Sqlds_CLP)
    '        Return Sqlds_CLP
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    Public Function ValidateClpNo(ByVal vCLPCardNo As String, ByVal CLpProgramId As String, ByVal vSiteCode As String) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" SELECT CD.CARDNO, CD.ACCOUNTNO, CD.TITLE + ' ' +  CD.FIRSTNAME + ' ' + CD.SURNAME AS CUSTOMERNAME, " & vbCrLf)
            vStmtQry.Append(" DBO.FNGETMSTDESC('GENDER',CD.GENDER) AS GENDER, isnull(Cd.TotalBalancePoint,0) AS BALANCEPOINT, " & vbCrLf)
            vStmtQry.Append(" CONVERT(VARCHAR(12), CD.BIRTHDATE,105) AS BIRTHDATE, CD.EMAILID, CD.MOBILENO, CD.Res_Phone as ResPhone, " & vbCrLf)
            vStmtQry.Append(" AD.ADDRESSLN1, AD.ADDRESSLN2, AD.ADDRESSLN3, AD.ADDRESSLN4, DBO.FNGETLOCNDESC(AD.CITYCODE) AS CITY, " & vbCrLf)
            vStmtQry.Append(" DBO.FNGETLOCNDESC(AD.STATECODE) AS STATE,DBO.FNGETLOCNDESC(AD.COUNTRYCODE) AS COUNTRY, " & vbCrLf)
            vStmtQry.Append(" AD.PINCODE, CD.SITECODE, AD.ADDRESSTYPE " & vbCrLf)
            vStmtQry.Append(" FROM CLPCUSTOMERS CD Inner Join CLPCustomerAddress AD on CD.CARDNO=AD.CARDNO " & vbCrLf)
            vStmtQry.Append(" and CD.ClpProgramId=AD.ClpProgramId  Left join CLPProgramSiteMap CLPID on CD.ClpProgramId=CLPID.ClpProgramId and " & vbCrLf)
            vStmtQry.Append(" CD.SiteCode=CLPID.SiteCode AND CLP.status=1 WHERE  CD.STATUS='1' " & vbCrLf)
            vStmtQry.Append(" AND CD.ClpProgramId='" & CLpProgramId & "' AND CD.CARDNO='" & vCLPCardNo & "' and CD.SiteCode='" & vSiteCode & "' AND Isnull(CD.CARDISACTIVE,'false')='True' AND CARDEXPIRYDT>GETDATE()" & vbCrLf)

            Sqlda_CLP = New SqlClient.SqlDataAdapter(vStmtQry.ToString, ConString)
            Sqldt_CLP = New DataTable
            Sqlda_CLP.Fill(Sqldt_CLP)

            Return Sqldt_CLP
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetCustomerAddressInformation(ByVal vCustmType As String, ByVal vCustCode As String) As DataTable
        Try
            If vCustmType = "SO" Then
                vStmtQry.Length = 0
                vStmtQry.Append("SELECT AD.ADDRESSLN1,AD.ADDRESSLN2, AD.ADDRESSLN3,AD.ADDRESSLN4 " & vbCrLf)
                vStmtQry.Append(" FROM CUSTOMERADDRESS AD " & vbCrLf)
                If Not (vCustCode = String.Empty) Then
                    vStmtQry.Append(" AND CD.CUSTOMERNO='" & vCustCode & "' " & vbCrLf)
                End If
            End If
            If vCustmType = "CLP" Then
                vStmtQry.Length = 0
                vStmtQry.Append(" SELECT  AD.ADDRESSLN1,AD.ADDRESSLN2, AD.ADDRESSLN3,AD.ADDRESSLN4 ,' ' as KeyData ,' ' as Value" & vbCrLf)
                vStmtQry.Append(" FROM CLPCUSTOMERADDRESS AD " & vbCrLf)
                If Not (vCustCode = String.Empty) AndAlso vCustCode <> "0" Then
                    vStmtQry.Append(" AND CD.CARDNO='" & vCustCode & "' " & vbCrLf)
                End If
            End If
            Sqlda_CLP = New SqlClient.SqlDataAdapter(vStmtQry.ToString, ConString)
            Sqldt_CLP = New DataTable
            Sqlda_CLP.Fill(Sqldt_CLP)
            'Console.WriteLine(vStmtQry.ToString)
            Return Sqldt_CLP

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function



    Public Function GetCustomerInformation(ByVal vCustmType As String, ByVal vSiteCode As String, ByVal CLpProgramId As String, ByVal vCustCode As String, _
                                           Optional ByVal isAddressCombined As Boolean = False, Optional ByVal OnlyAtCreatedSite As Boolean = False, _
                                            Optional vFilterVal As String = "", Optional CustFormat As String = "0", Optional IsNewSalesOrder As Boolean = False, Optional SearchByPhone As Boolean = False, Optional CustomerWisePrice As Boolean = False) As DataTable
        Try
            If vCustmType = "SO" Then
                vStmtQry.Length = 0
                vStmtQry.Append("SELECT  " & vbCrLf)
                If isAddressCombined Then
                    vStmtQry.Append(" isnull(DBO.FNGETDESC('TITLE',CD.TITLECODE,'000'),'') As TITLE, CD.FIRSTNAME + ' ' + CD.LASTNAME AS CUSTOMERNAME, " & vbCrLf)
                Else
                    vStmtQry.Append(" LTRIM(isnull(DBO.FNGETDESC('TITLE',CD.TITLECODE,'000'),'') + ' ' +  CD.FIRSTNAME + ' ' + CD.LASTNAME) AS CUSTOMERNAME, " & vbCrLf)
                End If
                vStmtQry.Append(" CD.MOBILEPHONE AS MOBILENO, " & vbCrLf)
                vStmtQry.Append(" CD.RESIDENCEPHONE AS RESPHONE, " & vbCrLf)
                'If isAddressCombined Then
                '    ' --- Address 2,3,4 field added by Mahesh Needed to show Address 1, 2 ,3 4 on form .
                '    vStmtQry.Append("AD.ADDRESSLN1 + ' ' + AD.ADDRESSLN2 + ' ' + AD.ADDRESSLN3 + ' ' + AD.ADDRESSLN4 AS ADDRESS, AD.ADDRESSLN1,AD.ADDRESSLN2, AD.ADDRESSLN3,AD.ADDRESSLN4, " & vbCrLf)
                'Else
                vStmtQry.Append(" AD.ADDRESSLN1 + ' ' + AD.ADDRESSLN2 + ' ' + AD.ADDRESSLN3 + ' ' + AD.ADDRESSLN4 AS ADDRESS,AD.ADDRESSLN1,AD.ADDRESSLN2, AD.ADDRESSLN3,AD.ADDRESSLN4, " & vbCrLf)
                'End If
                vStmtQry.Append("  CD.EMAILID EMAILID, DateofBirth AS BIRTHDATE, CONVERT(VARCHAR(12), CD.DateofBirth,105) AS BIRTHDATE,   " & vbCrLf)
                vStmtQry.Append(" CD.LASTNAME SURNAME, cd.MiddleName, '' RegistrationStatus, CD.FIRSTNAME, " & vbCrLf)

                vStmtQry.Append(" DBO.FNGETLOCNDESC(AD.CITYCODE) AS CITY, DBO.FNGETLOCNDESC(AD.STATECODE) AS STATE,DBO.FNGETLOCNDESC(AD.COUNTRYCODE) AS COUNTRY, CITYCODE, STATECODE, COUNTRYCODE, " & vbCrLf)
                vStmtQry.Append(" CD.CUSTOMERNO," & vbCrLf)
                ' commented for bug no 1552
                'vStmtQry.Append(" SELECT CD.CUSTOMERNO, DBO.FNGETDESC('TITLE',CD.TITLECODE,'000') + ' ' +  CD.FIRSTNAME + ' ' + CD.LASTNAME AS CUSTOMERNAME, " & vbCrLf)

                vStmtQry.Append(" DBO.FNGETMSTDESC('GENDER',CD.GENDER) AS GENDER," & vbCrLf)
                vStmtQry.Append("  CD.OFFICEPHONE AS OFFICENO, " & vbCrLf)
                'vStmtQry.Append(" AD.ADDRESSLN1 + ', ' + AD.ADDRESSLN2 + ', ' + AD.ADDRESSLN3 + ', ' + AD.ADDRESSLN4 AS ADDRESS ,AD.ADDRESSLN1, AD.ADDRESSLN2, AD.ADDRESSLN3, AD.ADDRESSLN4, " & vbCrLf)
                'vStmtQry.Append(" DBO.FNGETLOCNDESC(AD.CITYCODE) AS CITY , DBO.FNGETLOCNDESC(AD.STATECODE) AS STATE, " & vbCrLf)
                vStmtQry.Append(" AD.PINCODE, AD.ADDRESSTYPE, CD.CUSTOMERTYPE, " & vbCrLf)
                vStmtQry.Append(" DBO.FNGETDESC('ADDRESS',AD.ADDRESSTYPE,'000') AS ADDRESSTYPENAME, " & vbCrLf)

                '----- Addedd Column for wildsearch  Named - Filter ---
                vStmtQry.Append("  ISNULL(CD.LASTNAME ,'') + ' ' + ISNULL(CD.FIRSTNAME,'') +" & vbCrLf)
                vStmtQry.Append("  ISNULL(CD.MOBILEPHONE,'') + ISNULL(CD.RESIDENCEPHONE,'') +  " & vbCrLf)
                vStmtQry.Append("  ISNULL(AD.ADDRESSLN1,'') + ' ' + ISNULL(AD.ADDRESSLN2,'') + ' ' + ISNULL(AD.ADDRESSLN3,'') + ' ' + ISNULL(AD.ADDRESSLN4,'') + " & vbCrLf)
                vStmtQry.Append("  ISNULL(CD.EMAILID,'') + ISNULL(CONVERT(VARCHAR(12), CD.DateofBirth,105) ,'') + ISNULL(DBO.FNGETLOCNDESC(AD.CITYCODE) ,'')  + ISNULL(CD.CUSTOMERNO,'') AS FILTER" & vbCrLf)


                vStmtQry.Append(" FROM CUSTOMERSALEORDER CD, CUSTOMERADDRESS AD WHERE CD.CUSTOMERNO=AD.CUSTOMERNO AND " & vbCrLf)
                vStmtQry.Append("  AD.STATUS='1' --AND AD.AddressType = '1'" & vbCrLf)
                If Not (vCustCode = String.Empty) Then
                    vStmtQry.Append(" AND CD.CUSTOMERNO='" & vCustCode & "' " & vbCrLf)

                End If
            End If

            If vCustmType = "CLP" Then
                vStmtQry.Length = 0
                vStmtQry.Append("SELECT  DISTINCT  ROW_NUMBER() OVER (ORDER BY CD.CARDNO) AS SrNo, " & vbCrLf)

                If isAddressCombined Then
                    If IsNewSalesOrder Then
                        vStmtQry.Append("  ISNULL(TITLE,'') As TITLE, ISNULL(CD.FIRSTNAME,'') + ' ' + ISNULL(CD.SURNAME,'') AS CUSTOMERNAME, " & vbCrLf)
                    Else
                        vStmtQry.Append("  ISNULL(TITLE,'') As TITLE, ISNULL(CD.SURNAME,'') + ' ' + ISNULL(CD.FIRSTNAME,'') AS CUSTOMERNAME, " & vbCrLf)
                    End If
                Else
                    If IsNewSalesOrder Then
                        vStmtQry.Append("  LTRIM(ISNULL(TITLE,'') + ' ' +  ISNULL(CD.FIRSTNAME,'') + ' ' + ISNULL(CD.SURNAME,'')) AS CUSTOMERNAME, " & vbCrLf)
                    Else
                        vStmtQry.Append("  LTRIM(ISNULL(TITLE,'') + ' ' +  ISNULL(CD.SURNAME,'') + ' ' + ISNULL(CD.FIRSTNAME,'')) AS CUSTOMERNAME, " & vbCrLf)
                    End If

                End If
                If CustomerWisePrice = True Then
                    vStmtQry.Append(" ISNULL(CD.Level ,'-') As Level," & vbCrLf)
                End If
                vStmtQry.Append(" ISNULL(CD.CompanyName ,'-') As CompanyName," & vbCrLf)
                vStmtQry.Append(" ISNULL(AD.Department ,'-') As Department," & vbCrLf)
                vStmtQry.Append(" CD.MOBILENO, " & vbCrLf)
                vStmtQry.Append(" ISNULL(STUFF((SELECT distinct ', ' + t1.ContactValue  from CLPCustomerContacts t1 ")
                vStmtQry.Append("   Inner JOIN CLPCustomers  CC ON t1.CARDNO=CC.CARDNO AND t1.CLPPROGRAMID=CC.CLPPROGRAMID ")
                vStmtQry.Append("Inner JOIN CLPCUSTOMERADDRESS AD ON t1.CARDNO=AD.CARDNO AND t1.CLPPROGRAMID=AD.CLPPROGRAMID ")
                vStmtQry.Append("  where  t1.[CardNo] = CD.CARDNO and t1.[ContactType] ='Residence Number' and t1.STATUS =1")
                vStmtQry.Append("   FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,2,''),'-') ")
                vStmtQry.Append(" As 'Residence Number'," & vbCrLf)
                vStmtQry.Append("  CD.RES_PHONE AS RESPHONE," & vbCrLf)
                'If isAddressCombined Then
                '    ' --- Address 2,3,4 field added by Mahesh Needed to show Address 1, 2 ,3 4 on form .
                '    vStmtQry.Append("AD.ADDRESSLN1 + ' ' + AD.ADDRESSLN2 + ' ' + AD.ADDRESSLN3 + ' ' + AD.ADDRESSLN4 AS ADDRESS, AD.ADDRESSLN1,AD.ADDRESSLN2, AD.ADDRESSLN3,AD.ADDRESSLN4, " & vbCrLf)
                'Else
                vStmtQry.Append(" AD.ADDRESSLN1 + ' ' + AD.ADDRESSLN2 + ' ' + AD.ADDRESSLN3 + ' ' + AD.ADDRESSLN4 AS ADDRESS,AD.ADDRESSLN1,AD.ADDRESSLN2, AD.ADDRESSLN3,AD.ADDRESSLN4, " & vbCrLf)
                'End If

                vStmtQry.Append("  ISNULL( STUFF((SELECT distinct ', ' + t1.ContactValue from CLPCustomerContacts t1" & vbCrLf)
                vStmtQry.Append("  Inner JOIN CLPCustomers  CC ON t1.CARDNO=CC.CARDNO AND t1.CLPPROGRAMID=CC.CLPPROGRAMID " & vbCrLf)
                vStmtQry.Append("  Inner JOIN CLPCUSTOMERADDRESS AD ON t1.CARDNO=AD.CARDNO AND t1.CLPPROGRAMID=AD.CLPPROGRAMID " & vbCrLf)
                vStmtQry.Append("  where  t1.[CardNo] = CD.CARDNO and t1.[ContactType] ='Email Address' and t1.STATUS =1" & vbCrLf)
                vStmtQry.Append("  FOR XML PATH(''), TYPE" & vbCrLf)
                vStmtQry.Append(" ).value('.', 'NVARCHAR(MAX)') " & vbCrLf)
                vStmtQry.Append(",1,2,''),'-') " & vbCrLf)
                vStmtQry.Append(" 'Email Address'," & vbCrLf)

                vStmtQry.Append(" CD.EMAILID, " & vbCrLf)
                vStmtQry.Append("ISNULL(CONVERT(VARCHAR(12) , CD.BIRTHDATE,105),'-') AS BIRTHDATE, " & vbCrLf)
                vStmtQry.Append(" isnull(Cd.TotalBalancePoint,0) AS BALANCEPOINT," & vbCrLf)
                vStmtQry.Append(" CD.CARDNO AS CUSTOMERNO, " & vbCrLf)
                If IsNewSalesOrder = True Then
                    vStmtQry.Append(" CD.OrkutId AS CHSN, " & vbCrLf)
                Else
                    vStmtQry.Append(" CD.OrkutId AS GSTNO, " & vbCrLf)
                End If
                'vStmtQry.Append(" CD.OrkutId AS GSTNO, " & vbCrLf)
                vStmtQry.Append(" CD.GENDER AS GENDER ,  " & vbCrLf)
                vStmtQry.Append(" CD.FIRSTNAME,CD.MiddleName, CD.SURNAME, BirthDate,Gender,resgistrationstatus AS RegistrationStatus, " & vbCrLf)


                vStmtQry.Append(" DBO.FNGETLOCNDESC(AD.CITYCODE) AS CITY, DBO.FNGETLOCNDESC(AD.STATECODE) AS STATE,DBO.FNGETLOCNDESC(AD.COUNTRYCODE) AS COUNTRY, CITYCODE, STATECODE, COUNTRYCODE, " & vbCrLf)

                vStmtQry.Append("  CD.CARDTYPE, CD.ACCOUNTNO," & vbCrLf)

                vStmtQry.Append("  CD.OFFICENO, " & vbCrLf)
                'vStmtQry.Append(" AD.ADDRESSLN1 + ' ' + AD.ADDRESSLN2 + ' ' + AD.ADDRESSLN3 + ' ' + AD.ADDRESSLN4 AS ADDRESS,AD.ADDRESSLN1,AD.ADDRESSLN2, AD.ADDRESSLN3,AD.ADDRESSLN4, " & vbCrLf)
                vStmtQry.Append(" AD.PINCODE, " & vbCrLf)
                vStmtQry.Append(" AD.ADDRESSTYPE, 'CLP' AS CUSTOMERTYPE, DBO.FNGETDESC('ADDRESS',AD.ADDRESSTYPE,'000') AS ADDRESSTYPENAME, CD.SITECODE, " & vbCrLf)
                vStmtQry.Append(" CD.POINTSACCUMLATED, CD.CLPPROGRAMID, CLP.PhysicalCard , " & vbCrLf)
                vStmtQry.Append(" ISNULL(CD.ReminderComments,'') as ReminderComments , " & vbCrLf)
                If IsNewSalesOrder = True Then    'PC SO Merge vipin 02-05-2018
                    vStmtQry.Append(" ISNULL(CD.OrkutId,'') as CHSN ,CD.Level,CD.CustomerGroup, " & vbCrLf)
                Else
                    vStmtQry.Append(" ISNULL(CD.OrkutId,'') as GSTNo ,CD.Level,CD.CustomerGroup, " & vbCrLf)
                End If
                ' vStmtQry.Append(" ISNULL(CD.OrkutId,'') as GSTNo ,CD.Level,CD.CustomerGroup, " & vbCrLf)
                '----- Addedd Column for wildsearch  Named - Filter ---
                'vStmtQry.Append(" LTRIM(ISNULL(TITLE,'') + ' ' +  ISNULL(CD.SURNAME,'') + ' ' + ISNULL(CD.FIRSTNAME,'')) +" & vbCrLf)
                'vStmtQry.Append(" ISNULL(CD.MOBILENO,'') + ISNULL(CD.RES_PHONE,'') +  " & vbCrLf)
                'vStmtQry.Append(" ISNULL(AD.ADDRESSLN1,'') + ' ' + ISNULL(AD.ADDRESSLN2,'') + ' ' + ISNULL(AD.ADDRESSLN3,'') + ' ' + ISNULL(AD.ADDRESSLN4,'') + " & vbCrLf)
                'vStmtQry.Append(" Cast(ISNULL(Cd.TotalBalancePoint,0) as varchar) + " & vbCrLf)
                'vStmtQry.Append(" ISNULL(CD.EMAILID,'') +  ISNULL(CONVERT(VARCHAR(12) , CD.BIRTHDATE,105),'') + ISNULL(DBO.FNGETLOCNDESC(AD.CITYCODE) ,'')  + ISNULL(CD.CARDNO ,'') AS FILTER" & vbCrLf)

                If CustomerSearchParameters = "Mobile No." Then
                    vStmtQry.Append(" ISNULL(CD.MOBILENO,'')  AS FILTER" & vbCrLf)
                Else
                    vStmtQry.Append(" LTRIM(ISNULL(TITLE,'') + ' ' +  ISNULL(CD.SURNAME,'') + ' ' + ISNULL(CD.FIRSTNAME,'')) +" & vbCrLf)
                    vStmtQry.Append(" ISNULL(CD.MOBILENO,'') + ISNULL(CD.RES_PHONE,'') +  " & vbCrLf)
                    vStmtQry.Append(" ISNULL(AD.ADDRESSLN1,'') + ' ' + ISNULL(AD.ADDRESSLN2,'') + ' ' + ISNULL(AD.ADDRESSLN3,'') + ' ' + ISNULL(AD.ADDRESSLN4,'') + " & vbCrLf)
                    vStmtQry.Append(" Cast(ISNULL(Cd.TotalBalancePoint,0) as varchar) + " & vbCrLf)
                    vStmtQry.Append(" ISNULL(CD.EMAILID,'') +  ISNULL(CONVERT(VARCHAR(12) , CD.BIRTHDATE,105),'') + ISNULL(DBO.FNGETLOCNDESC(AD.CITYCODE) ,'')  + ISNULL(CD.CARDNO ,'') + ISNULL(CD.CompanyName ,'') +  ISNULL(AD.Department ,'') AS FILTER" & vbCrLf)
                End If

                'vStmtQry.Append(" ISNULL(CD.MOBILENO,'')  AS FILTER" & vbCrLf)
                vStmtQry.Append(" FROM CLPCUSTOMERS CD Left Outer JOIN CLPCUSTOMERADDRESS AD ON CD.CARDNO=AD.CARDNO AND CD.CLPPROGRAMID=AD.CLPPROGRAMID " & vbCrLf) 'AND AD.Defaults=1
                ''changed by ketan in case CLPCUSTOMERADDRESS have not entry then problem occure (when online customer created by MOD)
                If CustFormat = "1" Then
                    vStmtQry.Append(" AND AD.DefaultAddress=1 " & vbCrLf)
                    vStmtQry.Append(" AND AD.Status=1 " & vbCrLf)
                Else
                    vStmtQry.Append(" AND AD.Defaults=1 " & vbCrLf)
                End If
                vStmtQry.Append(" Inner JOIN CLPPROGRAMSITEMAP CLPID ON CD.CLPPROGRAMID=CLPID.CLPPROGRAMID  " & vbCrLf)
                'If CustFormat = "1" Then
                '    vStmtQry.Append(" AND AD.DefaultAddress=1 " & vbCrLf)
                '    vStmtQry.Append(" AND AD.Status=1 " & vbCrLf)
                'Else
                '    vStmtQry.Append(" AND AD.Defaults=1 " & vbCrLf)
                'End If
                If OnlyAtCreatedSite Then
                    vStmtQry.Append(" AND CD.SITECODE=CLPID.SITECODE " & vbCrLf)
                End If
                vStmtQry.Append(" AND CLPID.Status=1 " & vbCrLf)
                vStmtQry.Append(" INNER JOIN MstCLPProgram CLP ON CLPID.CLPProgramID = CLP.CLPProgramID " & vbCrLf)
                vStmtQry.Append(" WHERE CD.Status=1 and CD.CLPProgramID='" & CLpProgramId & "' " & vbCrLf)
                vStmtQry.Append(" AND Isnull(CD.CARDISACTIVE,'false')='True' AND (CARDEXPIRYDT IS NULL OR CARDEXPIRYDT>GETDATE()) " & vbCrLf)
                '"' AND  CD.SiteCode='" & vSiteCode &

                If Not (vCustCode = String.Empty) AndAlso vCustCode <> "0" Then
                    'vStmtQry.Append(" AND CD.CARDNO='" & vCustCode & "' " & vbCrLf)
                    If IsNumeric(vCustCode) Then
                        vStmtQry.Append(" AND CD.MobileNo='" & vCustCode & "' " & vbCrLf)
                    Else
                        vStmtQry.Append(" AND CD.CARDNO='" & vCustCode.Trim & "' " & vbCrLf)

                    End If
                End If
                '------MOD Search by Mobile number
                If SearchByPhone Then
                    'vStmtQry.Append(" AND CD.MobileNo='" & vFilterVal & "' " & vbCrLf)
                    If IsNumeric(vFilterVal) Then
                        vStmtQry.Append(" AND CD.MobileNo='" & vFilterVal & "' " & vbCrLf)
                    Else
                        vStmtQry.Append(" AND CD.CARDNO='" & vFilterVal & "' " & vbCrLf)
                    End If
                Else
                    If Not (vFilterVal = String.Empty) AndAlso vFilterVal <> "0" Then
                        vStmtQry.Append(" AND ")
                        'vStmtQry.Append(" LTRIM(ISNULL(TITLE,'') + ' ' +  ISNULL(CD.SURNAME,'') + ' ' + ISNULL(CD.FIRSTNAME,'')) +" & vbCrLf)
                        'vStmtQry.Append(" ISNULL(CD.MOBILENO,'') + ISNULL(CD.RES_PHONE,'') +  " & vbCrLf)
                        'vStmtQry.Append(" ISNULL(AD.ADDRESSLN1,'') + ' ' + ISNULL(AD.ADDRESSLN2,'') + ' ' + ISNULL(AD.ADDRESSLN3,'') + ' ' + ISNULL(AD.ADDRESSLN4,'') + " & vbCrLf)
                        'vStmtQry.Append(" Cast(ISNULL(Cd.TotalBalancePoint,0) as varchar) + " & vbCrLf)
                        'vStmtQry.Append(" ISNULL(CD.EMAILID,'') +  ISNULL(CONVERT(VARCHAR(12) , CD.BIRTHDATE,105),'') + ISNULL(DBO.FNGETLOCNDESC(AD.CITYCODE) ,'')  + ISNULL(CD.CARDNO ,'') ")
                        If _CustomerSearchParameters = "Mobile No." Then
                            vStmtQry.Append(" ISNULL(CD.MOBILENO,'') " & vbCrLf)
                        Else
                            vStmtQry.Append(" LTRIM(ISNULL(TITLE,'') + ' ' +  ISNULL(CD.SURNAME,'') + ' ' + ISNULL(CD.FIRSTNAME,'')) +" & vbCrLf)
                            vStmtQry.Append(" ISNULL(CD.MOBILENO,'') + ISNULL(CD.RES_PHONE,'') +  " & vbCrLf)
                            vStmtQry.Append(" ISNULL(AD.ADDRESSLN1,'') + ' ' + ISNULL(AD.ADDRESSLN2,'') + ' ' + ISNULL(AD.ADDRESSLN3,'') + ' ' + ISNULL(AD.ADDRESSLN4,'') + " & vbCrLf)
                            vStmtQry.Append(" Cast(ISNULL(Cd.TotalBalancePoint,0) as varchar) + " & vbCrLf)
                            vStmtQry.Append(" ISNULL(CD.EMAILID,'') +  ISNULL(CONVERT(VARCHAR(12) , CD.BIRTHDATE,105),'') + ISNULL(DBO.FNGETLOCNDESC(AD.CITYCODE) ,'')  + ISNULL(CD.CARDNO ,'') ")
                            If CustFormat = "1" Then
                                vStmtQry.Append("+ ISNULL(CD.CompanyName ,'') +  ISNULL(AD.Department ,'') ")
                            End If
                        End If
                        vStmtQry.Append(" LIKE '%" & vFilterVal & "%' " & vbCrLf)
                    End If
                End If

            End If
            Sqlda_CLP = New SqlClient.SqlDataAdapter(vStmtQry.ToString, ConString)
            Sqldt_CLP = New DataTable
            Sqlda_CLP.Fill(Sqldt_CLP)
            'Console.WriteLine(vStmtQry.ToString)
            Return Sqldt_CLP

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    'added by khusrao adil on 12-09-2017 for Customer GST No unique value check
    Public Function CheckGStNoExist(ByVal GSTNo As String) As Boolean
        Try
            Dim query As String = "select OrkutId from CLPCustomers  where OrkutId='" + GSTNo + "'"
            Dim Sqlda As SqlDataAdapter = New SqlClient.SqlDataAdapter(query, ConString)
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(query, ConString)
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
        End Try

    End Function

    ''' <summary>
    ''' Get loyalty Customer Table Structure Information
    ''' </summary>
    ''' <param name="vSiteCode"></param>
    ''' <param name="vCustomerNo"></param>
    ''' <UsedBy>frm.vb</UsedBy>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function GetCustomerDataSet(ByVal vSiteCode As String, ByVal vCustomerNo As String) As DataSet
        Try
            Dim Sqlda As SqlDataAdapter
            Dim Sqlds As DataSet
            vStmtQry.Length = 0
            '' vStmtQry.Append(" Select * From CLPCUSTOMERS Where  SiteCode='" & vSiteCode & "' And CardNo ='" & vCustomerNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From CLPCUSTOMERS Where   CardNo ='" & vCustomerNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From CLPCustomerAddress Where CardNo ='" & vCustomerNo & "'; " & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            'Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "CLPCustomer"
            'Dim KeyCustmInfo(1) As DataColumn
            'KeyCustmInfo(0) = Sqlds.Tables("CustomerSaleOrder").Columns("SiteCode")
            'KeyCustmInfo(1) = Sqlds.Tables("CustomerSaleOrder").Columns("CustomerNo")
            'Sqlds.Tables("CustomerSaleOrder").PrimaryKey = KeyCustmInfo

            Sqlds.Tables(1).TableName = "CLPCustomerAddress"
            'Dim KeyCustmAdds(3) As DataColumn
            'KeyCustmAdds(0) = Sqlds.Tables("CustomerAddress").Columns("SiteCode")
            'KeyCustmAdds(1) = Sqlds.Tables("CustomerAddress").Columns("CustomerNo")
            'KeyCustmAdds(2) = Sqlds.Tables("CustomerAddress").Columns("CustomerType")
            'KeyCustmAdds(3) = Sqlds.Tables("CustomerAddress").Columns("AddressType")
            'Sqlds.Tables("CustomerAddress").PrimaryKey = KeyCustmAdds

            Return Sqlds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetCLPCustomerDataSet(ByVal vCardNumber As String, ByVal vClpprogramid As String) As DataSet
        Try
            Dim Sqlda As SqlDataAdapter
            'Dim Sqlcmdb As SqlCommandBuilder
            Dim Sqlds As DataSet
            vStmtQry.Length = 0
            vStmtQry.Append(" Select * From CLPCustomers Where CardNo='" & vCardNumber & "' And Clpprogramid ='" & vClpprogramid & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From CLPCustomerAddress Where CardNo='" & vCardNumber & "' And Clpprogramid ='" & vClpprogramid & "'; " & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            'Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "CLPCustomers"
            'Dim KeyCustmInfo(1) As DataColumn
            'KeyCustmInfo(0) = Sqlds.Tables("CustomerSaleOrder").Columns("SiteCode")
            'KeyCustmInfo(1) = Sqlds.Tables("CustomerSaleOrder").Columns("CustomerNo")
            'Sqlds.Tables("CustomerSaleOrder").PrimaryKey = KeyCustmInfo

            Sqlds.Tables(1).TableName = "CLPCustomerAddress"
            'Dim KeyCustmAdds(3) As DataColumn
            'KeyCustmAdds(0) = Sqlds.Tables("CustomerAddress").Columns("SiteCode")
            'KeyCustmAdds(1) = Sqlds.Tables("CustomerAddress").Columns("CustomerNo")
            'KeyCustmAdds(2) = Sqlds.Tables("CustomerAddress").Columns("CustomerType")
            'KeyCustmAdds(3) = Sqlds.Tables("CustomerAddress").Columns("AddressType")
            'Sqlds.Tables("CustomerAddress").PrimaryKey = KeyCustmAdds

            Return Sqlds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function CheckIfDeliveryAddressExist(ByVal customerNo As String, ByVal siteCode As String, ByVal clpProgramId As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim query As String
            '---Address type condition change to default address  
            query = " select ADDRESSLN1 + ' ' + ADDRESSLN2 + ' ' + ADDRESSLN3 + ' ' + ADDRESSLN4 AS ADDRESS , ADDRESSLN1 , ADDRESSLN2 , ADDRESSLN3 ,ADDRESSLN4+' '+CityCode+' '+PinCode As ADDRESSLN4 from dbo.CLPCustomerAddress Where CardNo='" & customerNo & "' And Clpprogramid ='" & clpProgramId & "' And defaults = '1' And Status=1" ''& "' And AddressType = '2' "
            dt = GetFilledTable(query)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt
            Else
                query = " Select ADDRESSLN1 + ' ' + ADDRESSLN2 + ' ' + ADDRESSLN3 + ' ' + ADDRESSLN4 AS ADDRESS , ADDRESSLN1 , ADDRESSLN2 , ADDRESSLN3 , ADDRESSLN4 From CustomerAddress Where CustomerNo='" & customerNo & "' And SiteCode ='" & siteCode & "' And CustomerType = 'SO' "   'and AddressType = '2' "
                dt = GetFilledTable(query)
                Return dt
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetClpPointsForaCardNumber(ByVal cardNumber As String) As DataTable
        Try
            Dim query As String
            query = " exec getClpPointsForCustomer '" & cardNumber & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetClpPoints(ByVal cardNumber As String, ByVal clpProgramId As String) As Double
        Try
            Dim dt As New DataTable
            Dim query As String
            query = " select TotalBalancePoint from dbo.CLPCustomers Where CardNo='" & cardNumber & "' And Clpprogramid ='" & clpProgramId & "'"
            dt = GetFilledTable(query)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt.Rows(0)("TotalBalancePoint")
            Else
                Return 0
            End If
        Catch ex As Exception
            LogException(ex)
            Return 0
        End Try
    End Function

    Public Function UpdateClpPointsInfo(ByVal cardNumber As String, ByVal clpProgramId As String, ByVal points As Double) As Boolean
        Try
            Dim dt As New DataTable
            Dim query As String
            query = " Update dbo.CLPCustomers set TotalBalancePoint = " & points & " Where CardNo='" & cardNumber & "' And Clpprogramid ='" & clpProgramId & "'"
            UpdateClpData(query)
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function UpdateCustomerAddress(ByVal cardNumber As String, ByVal clpProgramId As String, ByVal AddressLn1 As String, ByVal AddressLn2 As String, _
                                          ByVal AddressLn3 As String, ByVal AddressLn4 As String,
                                          ByVal MobileNo As String, ByVal TelNo As String,
                                          ByVal EmailAddress As String, ByVal AllowMobileNoEditable As Boolean) As Boolean
        Try
            Dim dt As New DataTable
            Dim query As String
            query = " Update CLPCustomerAddress set AddressLn1 = '" & AddressLn1.Replace("'", "''") & _
                       "', AddressLn2 = '" & AddressLn2.Replace("'", "''") & _
                       "', AddressLn3 = '" & AddressLn3.Replace("'", "''") & _
                       "', AddressLn4 = '" & AddressLn4.Replace("'", "''") & _
                       "', UPDATEDON = getDate() " & _
                       "  Where CardNo='" & cardNumber & "' And Clpprogramid ='" & clpProgramId & "'" & " And defaults = '1' "

            If AllowMobileNoEditable Then

                query = query & "   UPDATE CLPCustomers " & _
                        " SET Mobileno ='" & MobileNo & "'," & _
                        " Res_Phone ='" & TelNo & "'," & _
                        " EmailId ='" & EmailAddress & "'," & _
                        " UPDATEDON = getDate() " & _
                        " Where CardNo='" & cardNumber & "' And Clpprogramid ='" & clpProgramId & "'"
            End If

            UpdateClpData(query)
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function


    Public Function UpdateClpData(ByVal Query As String) As Boolean
        Try
            OpenConnection()
            Dim cmd As New SqlCommand
            cmd.CommandText = Query
            cmd.Connection = SpectrumCon()
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CheckIfCstApplicable(ByVal siteCode As String, ByVal customerStateCode As String)
        Try
            Dim custmrStateCode As String = String.Empty
            Dim result As Boolean
            Dim dt As New DataTable
            Dim query As String
            query = "select top 1 AreaCode  from MstAreaCode where Description = '" & customerStateCode & "' and status='1' "
            dt = GetFilledTable(query)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso IsDBNull(dt.Rows(0)(0)) = False Then
                custmrStateCode = dt.Rows(0)(0)
            End If
            query = "select StateCode  from MstSite where SiteCode = '" & siteCode & "' and status='1' "
            dt = GetFilledTable(query)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso IsDBNull(dt.Rows(0)(0)) = False Then
                Dim siteStateCode As String = dt.Rows(0)(0)
                If siteStateCode.ToUpper().Equals(custmrStateCode.ToUpper()) = False Then
                    result = True
                End If
            End If
            Return result
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
End Class
