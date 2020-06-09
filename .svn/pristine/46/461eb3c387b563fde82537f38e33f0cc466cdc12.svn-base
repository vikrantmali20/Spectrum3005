Imports System.Text
Imports System.Data
Imports System.Data.SqlClient

Public Class clsSOCustomer
    Dim Sqlda As SqlDataAdapter
    Dim Sqlcmdb As SqlCommandBuilder
    Dim Sqlds As DataSet

    Dim vStmtQry As New StringBuilder
    Dim objComn As New clsCommon

    ''' <summary>
    ''' Set Display Data in ComboBox
    ''' </summary>
    ''' <returns>DataSet</returns>
    ''' <UsedBy>frmAddCustSO.vb</UsedBy>
    ''' <remarks></remarks>
    
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

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlClient.SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "CityTab"
            Sqlds.Tables(1).TableName = "StateTab"
            Sqlds.Tables(2).TableName = "CountryTab"
            Sqlds.Tables(3).TableName = "TitleTab"
            Sqlds.Tables(4).TableName = "GenderTab"
            Sqlds.Tables(5).TableName = "MaritalTab"
            Sqlds.Tables(6).TableName = "EducationTab"
            Sqlds.Tables(7).TableName = "OccupationTab"
            Sqlds.Tables(8).TableName = "CustomerTypeTab"

            Return Sqlds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Get Customer Table Structure Information
    ''' </summary>
    ''' <param name="vSiteCode"></param>
    ''' <param name="vCustomerNo"></param>
    ''' <UsedBy>frmAddCustSO.vb</UsedBy>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function GetCustomerDataSet(ByVal vSiteCode As String, ByVal vCustomerNo As String) As DataSet
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select * From CustomerSaleOrder Where SiteCode='" & vSiteCode & "' And CustomerNo ='" & vCustomerNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From CustomerAddress Where SiteCode='" & vSiteCode & "' And CustomerNo ='" & vCustomerNo & "'; " & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "CustomerSaleOrder"
            Dim KeyCustmInfo(1) As DataColumn
            KeyCustmInfo(0) = Sqlds.Tables("CustomerSaleOrder").Columns("SiteCode")
            KeyCustmInfo(1) = Sqlds.Tables("CustomerSaleOrder").Columns("CustomerNo")
            Sqlds.Tables("CustomerSaleOrder").PrimaryKey = KeyCustmInfo

            Sqlds.Tables(1).TableName = "CustomerAddress"
            Dim KeyCustmAdds(3) As DataColumn
            KeyCustmAdds(0) = Sqlds.Tables("CustomerAddress").Columns("SiteCode")
            KeyCustmAdds(1) = Sqlds.Tables("CustomerAddress").Columns("CustomerNo")
            KeyCustmAdds(2) = Sqlds.Tables("CustomerAddress").Columns("CustomerType")
            KeyCustmAdds(3) = Sqlds.Tables("CustomerAddress").Columns("AddressType")
            Sqlds.Tables("CustomerAddress").PrimaryKey = KeyCustmAdds

            Return Sqlds
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

                Sqlda = New SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
                Sqlda.SelectCommand.Transaction = SqlTrans

                Sqlcmdb = New SqlCommandBuilder(Sqlda)
                Sqlda.TableMappings.Add(tableName, tableName)
                Sqlda = Sqlcmdb.DataAdapter
                Sqlda.Update(dsSaveData, tableName)
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

    ''' <summary>
    ''' Display Customer Information in DataGrid
    ''' </summary>
    ''' <returns>DataSet</returns>
    ''' <UsedBy>frmAddCustSO.vb</UsedBy>
    ''' <remarks></remarks>
    Public Function GetCustmGridDataStruct() As DataSet
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select cp.CustomerNo, dbo.FnGetMstDesc('TITLE',cp.TitleCode) as Title, cp.FirstName, " & vbCrLf)
            vStmtQry.Append(" cp.MiddleName, cp.LastName, dbo.FnGetMstDesc('Gender',cp.Gender) as Gender, cp.CustomerName, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(10),cp.DateofBirth,105) as DateofBirth, cp.ResidencePhone, cp.MobilePhone, " & vbCrLf)
            vStmtQry.Append(" cp.OfficePhone, cp.EmailId, dbo.FnGetMstDesc('OCCUPATION', cp.Occupation) as Occupation, " & vbCrLf)
            vStmtQry.Append(" dbo.FnGetMstDesc('EDUCATION', cp.Education) as Education, ca.AddressLn1, ca.AddressLn2, " & vbCrLf)
            vStmtQry.Append(" dbo.FnGetLocnDesc(ca.CityCode) as CityCode, dbo.FnGetLocnDesc(ca.StateCode) as StateCode, " & vbCrLf)
            vStmtQry.Append(" dbo.FnGetLocnDesc(ca.CountryCode) as CountryCode, ca.PinCode " & vbCrLf)
            vStmtQry.Append(" From CustomerSaleOrder cp, CustomerAddress ca Where cp.SiteCode=ca.SiteCode " & vbCrLf)
            vStmtQry.Append(" And cp.CustomerNo=ca.CustomerNo And AddressType='1' " & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Return Sqlds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetDataForAddressTab() As DataTable
        Try
            Dim vStmtQry As New StringBuilder

            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(50),'') as SrNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as 'Address Type'," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(1000),'') as Address, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as 'PrimaryAddress'" & vbCrLf)
            Dim daScan As New SqlDataAdapter
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtScan As New DataTable
            daScan.Fill(dtScan)

            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetDetailsForAddress() As DataTable
        Try
            Dim vStmtQry As New StringBuilder

            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(50),'') as rowindex," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as FlatNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as BuildingName, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Area," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Landmark," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as City," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as ZipCode," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as State," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Country," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Department," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as PrimaryAddress," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as AddressType," & vbCrLf)
            vStmtQry.Append(" Convert(int,0) as SrNo " & vbCrLf)

            Dim daScan As New SqlDataAdapter
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtScan As New DataTable
            daScan.Fill(dtScan)

            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetDataForCommunicationTab() As DataTable
        Try
            Dim vStmtQry As New StringBuilder

            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(50),'') as SrNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ContactType," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as ContactDetails," & vbCrLf)
            vStmtQry.Append(" Convert(int,0) as SrNumber," & vbCrLf)
            vStmtQry.Append(" Convert(int,0) as SortOrder" & vbCrLf)
            Dim daScan As New SqlDataAdapter
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtScan As New DataTable
            daScan.Fill(dtScan)

            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetCityList(ByVal code As String) As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select Description  , ' 'as KeyData ,' 'as Value   from MstAreaCode  where AreaType=103 and ParentCode ='" & code & "' order by Description asc "
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetAddressType() As DataTable

        Dim table As New DataTable
        table.Columns.Add("Status", GetType(Int16))
        table.Columns.Add("Value", GetType(String))
        table.Rows.Add(1, "Residential")
        table.Rows.Add(2, "Office")

        Return table
    End Function

    Public Function GetCLPCustDetails(ByVal vSiteCode As String, ByVal vCustomerNo As String) As DataSet
        Try
            Dim Sqlda As SqlDataAdapter
            Dim Sqlds As DataSet
            Dim query = "select ClpProgramId  from CLPProgramSiteMap  where Status=1 and SiteCode ='" & vSiteCode & "'"
            vStmtQry.Length = 0
            'vStmtQry.Append(" Select CardNo,ClpProgramId,Mobileno,FirstName,MiddleName,SurName,CompanyName,Gender,BirthDate,CardType,resgistrationstatus,TotalBalancePoint From CLPCUSTOMERS Where  SiteCode='" & vSiteCode & "' And CardNo ='" & vCustomerNo & "'and Clpprogramid in (" & query & "); " & vbCrLf)
            'vStmtQry.Append(" Select AddressType,AddressLn1,AddressLn2,AddressLn3,AddressLn4 ,PinCode,CityCode,StateCode,CountryCode,DefaultAddress,Department,Srno From CLPCustomerAddress Where Status=1 and CardNo ='" & vCustomerNo & "' and Clpprogramid in (" & query & "); " & vbCrLf)
            'vStmtQry.Append(" Select ContactType,ContactValue,SrNo From CLPCustomerContacts Where Status=1 and SiteCode='" & vSiteCode & "' And CardNo ='" & vCustomerNo & "' and Clpprogramid in (" & query & ") order by SrNo asc; " & vbCrLf)
            vStmtQry.Append(" Select CardNo,ClpProgramId,Mobileno,FirstName,MiddleName,SurName,CompanyName,Gender,BirthDate,CardType,resgistrationstatus,TotalBalancePoint From CLPCUSTOMERS Where  CardNo ='" & vCustomerNo & "'and Clpprogramid in (" & query & "); " & vbCrLf)
            vStmtQry.Append(" Select AddressType,AddressLn1,AddressLn2,AddressLn3,AddressLn4 ,PinCode,CityCode,StateCode,CountryCode,DefaultAddress,Department,Srno From CLPCustomerAddress Where Status=1 and CardNo ='" & vCustomerNo & "' and Clpprogramid in (" & query & "); " & vbCrLf)
            vStmtQry.Append(" Select ContactType,ContactValue,SrNo From CLPCustomerContacts Where Status=1 And CardNo ='" & vCustomerNo & "' and Clpprogramid in (" & query & ") order by SrNo asc; " & vbCrLf)
            vStmtQry.Append(" Select * From CLPTransaction Where clpprogramid ='" & vCustomerNo & "'  and Clpprogramid in (" & query & "); " & vbCrLf)


            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "CLPCustomer"
            Sqlds.Tables(1).TableName = "CLPCustomerAddress"
            Sqlds.Tables(2).TableName = "CLPCustomerContact"
            Sqlds.Tables(3).TableName = "CLPTransaction"

            Return Sqlds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function CheckMobileNoUnique(ByVal MobileNo As String, ByVal vSiteCode As String, Optional ByVal cardno As String = "") As Boolean
        Try
            Dim dt As DataTable
            Dim query = "select ClpProgramId  from CLPProgramSiteMap  where Status=1 and SiteCode ='" & vSiteCode & "'"
            Dim strString As String
            If cardno = "" Then
                strString = "select 1 from CLPCustomers where Mobileno = '" & MobileNo & "'and Clpprogramid in (" & query & ");"
            Else
                strString = "select 1 from CLPCustomers where Mobileno = '" & MobileNo & "'and Clpprogramid in (" & query & ") and cardno='" & cardno & "';"
            End If
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            If cardno = "" Then
                If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                    Return True
                End If
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
End Class
