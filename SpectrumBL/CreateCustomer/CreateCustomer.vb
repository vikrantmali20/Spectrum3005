Imports SpectrumCommon
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Collections
Public Class CreateCustomer
    Inherits CreateCustomerBase
    Implements ICreateCustomer
    Private objClsCommon As New clsCommon
    Public Function GetCustomerInfo(ByVal clpProgramId As String, ByVal PrimaryKeyNo As String, ByVal KeyIsMobileNo As Boolean) As GetCustomerResponse Implements ICreateCustomer.GetCustomerInfo
        Try
            Dim response As New GetCustomerResponse
            Dim query = String.Empty
            ' ---Changed By Mahesh :: Remove mobile no filter to card no as par Rama suggestion
            query = "select CardNo, ClpProgramId, AccountNo, SiteCode, FirstName, MiddleName, SurName, Gender, BirthDate, EmailId,  Mobileno, TotalBalancePoint,CustomerGroup,resgistrationstatus,CardType, Res_Phone AS ResidenceNumber,OrkutId,Level  " & _
                    " from CLPCustomers " & _
                    " where ClpProgramId='" & clpProgramId & "'" & _
                            " and STATUS = 1 "

            If KeyIsMobileNo Then
                query = query & " and MobileNo ='" & PrimaryKeyNo & "'"
            Else
                query = query & " and CardNo ='" & PrimaryKeyNo & "'"
            End If


            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    dataReader.Read()
                    response.CLPCustomer = New CLPCustomerDTO()
                    response.CLPCustomer.CardNumber = IIf(IsDBNull(dataReader.GetValue(0)), String.Empty, dataReader.GetValue(0))
                    response.CLPCustomer.ClpProgId = IIf(IsDBNull(dataReader.GetValue(1)), String.Empty, dataReader.GetValue(1))
                    response.CLPCustomer.SiteCode = IIf(IsDBNull(dataReader.GetValue(3)), String.Empty, dataReader.GetValue(3))
                    response.CLPCustomer.FirstName = IIf(IsDBNull(dataReader.GetValue(4)), String.Empty, dataReader.GetValue(4))
                    response.CLPCustomer.MiddleName = IIf(IsDBNull(dataReader.GetValue(5)), String.Empty, dataReader.GetValue(5))
                    response.CLPCustomer.LastName = IIf(IsDBNull(dataReader.GetValue(6)), String.Empty, dataReader.GetValue(6))
                    response.CLPCustomer.Gender = IIf(IsDBNull(dataReader.GetValue(7)), String.Empty, dataReader.GetValue(7))
                    response.CLPCustomer.BirthDate = IIf(IsDBNull(dataReader.GetValue(8)), DateTime.MinValue, dataReader.GetValue(8))
                    response.CLPCustomer.EmailId = IIf(IsDBNull(dataReader.GetValue(9)), String.Empty, dataReader.GetValue(9))
                    response.CLPCustomer.MobileNo = IIf(IsDBNull(dataReader.GetValue(10)), String.Empty, dataReader.GetValue(10))

                    If dataReader.GetValue(11) IsNot DBNull.Value Then
                        response.CLPCustomer.BalancePoints = dataReader.GetValue(11)
                    End If
                    response.CLPCustomer.CustomerGroup = IIf(IsDBNull(dataReader.GetValue(12)), String.Empty, dataReader.GetValue(12).ToString())
                    response.CLPCustomer.RegistrationStatus = IIf(IsDBNull(dataReader.GetValue(13)), String.Empty, dataReader.GetValue(13))
                    response.CLPCustomer.CardType = IIf(IsDBNull(dataReader.GetValue(14)), String.Empty, dataReader.GetValue(14))
                    response.CLPCustomer.ResidenceNumber = IIf(IsDBNull(dataReader.GetValue(15)), String.Empty, dataReader.GetValue(15))
                    response.CLPCustomer.GSTNo = IIf(IsDBNull(dataReader.GetValue(16)), String.Empty, dataReader.GetValue(16))
                    response.CLPCustomer.Level = IIf(IsDBNull(dataReader.GetValue(17)), String.Empty, dataReader.GetValue(17))
                End If
            End Using
            If response.CLPCustomer IsNot Nothing Then
                query = "select CardNo, Clpprogramid, AddressType, AddressLn1, AddressLn2, AddressLn3,  PinCode, CityCode, StateCode, CountryCode,AddressLn4 from dbo.CLPCustomerAddress where CardNo = '" & response.CLPCustomer.CardNumber & "' and Clpprogramid='" & response.CLPCustomer.ClpProgId & "' and STATUS=1"
                Using dataReader As SqlDataReader = GetReader(query)
                    If dataReader.HasRows Then
                        Do While dataReader.Read()
                            Dim customerAddress As New CLPCustomerAddressDTO()
                            customerAddress.CardNumber = IIf(IsDBNull(dataReader.GetValue(0)), String.Empty, dataReader.GetValue(0))
                            customerAddress.ClpProgId = IIf(IsDBNull(dataReader.GetValue(1)), String.Empty, dataReader.GetValue(1))
                            customerAddress.AddressType = IIf(IsDBNull(dataReader.GetValue(2)), String.Empty, dataReader.GetValue(2))
                            customerAddress.AddLine1 = IIf(IsDBNull(dataReader.GetValue(3)), String.Empty, dataReader.GetValue(3))
                            customerAddress.AddLine2 = IIf(IsDBNull(dataReader.GetValue(4)), String.Empty, dataReader.GetValue(4))
                            customerAddress.AddLine3 = IIf(IsDBNull(dataReader.GetValue(5)), String.Empty, dataReader.GetValue(5))
                            customerAddress.PinCode = IIf(IsDBNull(dataReader.GetValue(6)), String.Empty, dataReader.GetValue(6))
                            customerAddress.City = IIf(IsDBNull(dataReader.GetValue(7)), String.Empty, dataReader.GetValue(7))
                            customerAddress.State = IIf(IsDBNull(dataReader.GetValue(8)), String.Empty, dataReader.GetValue(8))
                            customerAddress.Country = IIf(IsDBNull(dataReader.GetValue(9)), String.Empty, dataReader.GetValue(9))
                            customerAddress.AddLine4 = IIf(IsDBNull(dataReader.GetValue(10)), String.Empty, dataReader.GetValue(10))
                            response.CLPCustomer.AddressList.Add(customerAddress)
                        Loop
                    End If
                End Using
            End If
            Return response
        Catch ex As Exception
            Throw
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function GetCustomerMaster() As GetCustomerMasterResponse Implements ICreateCustomer.GetCustomerMaster
        Try
            Dim response As New GetCustomerMasterResponse
            response.AddressTypeList = New List(Of AddressTypeDTO)()
            response.AddressTypeList.Add(New AddressTypeDTO With {.AddressTypeCode = "1", .AddressTypeName = "Residential"})
            Dim query = String.Empty
            'query = "select CustomerGroupCode, CustomerGroupName from MstCustomerGroup where Status = 1 "
            'Using dataReader As SqlDataReader = GetReader(query)
            '    If dataReader.HasRows Then
            '        Do While dataReader.Read()                      
            '            Dim customerGroup As New CustomerGroupDetails
            '            customerGroup.GroupCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
            '            customerGroup.GroupName = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
            '            response.CustomerGroupList.Add(customerGroup)
            '        Loop                    
            '    End If
            'End Using

            query = "select AreaCode, Description, AreaType, ParentCode from MstAreaCode where Status = 1"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim areaInfo As New AreaInfo
                        areaInfo.AreaCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        areaInfo.AreaName = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        areaInfo.AreaType = IIf(IsDBNull(dataReader.GetDecimal(2)), 0, dataReader.GetDecimal(2))                         
                        areaInfo.ParentAreaCode = IIf(IsDBNull(dataReader(3)), String.Empty, Convert.ToString(dataReader(3)))
                        response.AreaInfoList.Add(areaInfo)
                    Loop
                End If
            End Using
            Return response
        Catch ex As Exception            
            Throw
        Finally
            CloseConnection()
        End Try
    End Function
    'Code Added By irfan for Mantis issues===========================================================================
    Public Function GetCustomerGroup() As GetCustomerMasterResponse Implements ICreateCustomer.GetCustomerGroup
        Try
            Dim response As New GetCustomerMasterResponse
            response.CustomerGroupList = New List(Of CustomerGroupDetails)()
            response.CustomerGroupList.Add(New CustomerGroupDetails With {.GroupCode = "1", .GroupName = "Residential"})
            Dim query = String.Empty
            query = "select CustomerGroupCode, CustomerGroupName from MstCustomerGroup where Status = 1 "
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim customerGroup As New CustomerGroupDetails
                        customerGroup.GroupCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        customerGroup.GroupName = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        response.CustomerGroupList.Add(customerGroup)
                    Loop
                End If
            End Using

            Return response
        Catch ex As Exception
            Throw
        Finally
            CloseConnection()
        End Try
    End Function
    '==============================================================================================================

    'Public Function SaveCustomerInfo(ByRef request As SaveCustomerRequest) As Boolean Implements ICreateCustomer.SaveCustomerInfo      
    '    Dim tran As SqlTransaction
    '    Try
    '        Dim query As String = String.Empty
    '        If CheckIfInsertOrUpdate(request) = False Then
    '            Dim docNo As String = objClsCommon.getDocumentNo("Customer Loyalty", request.CLPCustomer.SiteCode)
    '            Dim otherCharacters = "CLS" & request.CLPCustomer.SiteCode.Substring(request.CLPCustomer.SiteCode.Length - 3, 3)

    '            'String.Concat(otherCharacters,docNo.PadLeft(15-otherCharacters.Length,"0"))	"CLSHM1000000008"
    '            request.CLPCustomer.CardNumber = objClsCommon.GenDocNo(otherCharacters, 15, docNo)

    '            query = "select top(1) ClpProgramId from CLPProgramSiteMap where Sitecode='" & request.CLPCustomer.SiteCode & "' AND Status=1 "
    '            Using dataReader As SqlDataReader = GetReader(query)
    '                If dataReader.HasRows Then
    '                    Do While dataReader.Read()
    '                        Dim customerGroup As New CustomerGroupDetails
    '                        request.CLPCustomer.ClpProgId = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
    '                    Loop
    '                Else
    '                    Throw New CLPProgramNotMapped("No Clp program is mapped to this site.")
    '                End If
    '            End Using
    '            tran = SpectrumCon.BeginTransaction()

    '            'query = "insert into dbo.CLPCustomers (CardNo, ClpProgramId, AccountNo, SiteCode, Title, FirstName, MiddleName, SurName, NameOnCard, CardType, Gender, BirthDate, Education, Occupation, EmailId, MaritalStatus, Mobileno, Res_Phone, OfficeNo, CardExpiryDT, CardisActive, CardIssueDT, ReferedBy, RelationWithPrimaryCard, IsAddonCard, PromotionInfobyEmail, PromotionInfobySMS, SpouseTitle, SpouseFirstName, SpouseMiddleName, SpouseSurName, SpouseDob, SpouseOccupation, MarriageAnivDate, TotalBalancePoint, PointsAccumlated, PointsRedeemed, CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS, OldCardType,CustomerGroup,resgistrationstatus)" & _
    '            '        "values('" & request.CLPCustomer.CardNumber & "','" & request.CLPCustomer.ClpProgId & "','" & request.CLPCustomer.CardNumber & "'," & _
    '            '        "'" & request.CLPCustomer.SiteCode & "','','" & request.CLPCustomer.FirstName & "','" & request.CLPCustomer.MiddleName & "','" & request.CLPCustomer.LastName & "'," & _
    '            '        "'" & String.Format("{0} {1}", request.CLPCustomer.FirstName, request.CLPCustomer.LastName) & "','" & request.CLPCustomer.CardType & "','" & request.CLPCustomer.Gender & "'," & IIf(request.CLPCustomer.BirthDate = DateTime.MinValue, "null", "'" & request.CLPCustomer.BirthDate.ToString("yyyy-MM-dd") & "'") & ",'','','" & request.CLPCustomer.EmailId & "','','" & request.CLPCustomer.MobileNo & "','" & request.CLPCustomer.ResidenceNumber & "','','" & DateTime.MaxValue.ToString("yyyy-MM-dd") & "','True',NULL,'','',0,1,1,'','','','',NULL,'',NULL," & request.CLPCustomer.BalancePoints & ",0.0,0.0," & _
    '            '        "'" & request.CLPCustomer.SiteCode & "','" & request.CreatedBy & "',getdate(),'" & request.CLPCustomer.SiteCode & "','" & request.CreatedBy & "',getdate(),1,'','" & request.CLPCustomer.CustomerGroup & "','" & request.CLPCustomer.RegistrationStatus & "') "
    '            With request.CLPCustomer


    '                query = "insert into dbo.CLPCustomers (CardNo, ClpProgramId, AccountNo, SiteCode, " & _
    '                                " Title, FirstName, MiddleName, SurName, " & _
    '                                " NameOnCard, CardType, Gender, BirthDate," & _
    '                                " Education, Occupation, EmailId, MaritalStatus," & _
    '                                " Mobileno, Res_Phone, OfficeNo, CardExpiryDT, " & _
    '                                " CardisActive, CardIssueDT, ReferedBy, RelationWithPrimaryCard," & _
    '                                " IsAddonCard, PromotionInfobyEmail, PromotionInfobySMS, SpouseTitle, " & _
    '                                " SpouseFirstName, SpouseMiddleName, SpouseSurName," & _
    '                                " SpouseDob, SpouseOccupation, MarriageAnivDate, " & _
    '                                " FacebookId, TwitterId, LinkedInID, " & _
    '                                " GooglePlusId, Hi5Id, MySpaceId, " & _
    '                                " IbiboId, FourSquareId, OrkutId, SkypeId," & _
    '                                " TotalBalancePoint, PointsAccumlated, PointsRedeemed, " & _
    '                                " CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS, " & _
    '                                " OldCardType,CustomerGroup,resgistrationstatus)" & _
    '                      " Values('" & .CardNumber & "','" & .ClpProgId & "','" & .CardNumber & "','" & .SiteCode & "'," & _
    '                                  "'" & .Title & "','" & .FirstName & "','" & .MiddleName & "','" & .LastName & "'," & _
    '                                  "'" & String.Format("{0} {1}", .FirstName, .LastName) & "','" & .CardType & "','" & .Gender & "'," & IIf(.BirthDate = DateTime.MinValue, "null", "'" & .BirthDate.ToString("yyyy-MM-dd") & "'") & "," & _
    '                                  "'" & .Education & "','" & .Occupation & "','" & .EmailId & "','" & .MaritalStatus & "'," & _
    '                                  "'" & .MobileNo & "','" & .ResidenceNumber & "','" & .OfficeNumber & "','" & DateTime.MaxValue.ToString("yyyy-MM-dd") & "'," & _
    '                                  "'True',NULL,'',''," & _
    '                                  "0," & .PromotionInfobyEmail & "," & .PromotionInfobySMS & ",'" & .SpouseTitle & "'," & _
    '                                  "'" & .SpouseFirstName & "','" & .SpouseMiddleName & "','" & .SpouseLastName & "'," & _
    '                                  IIf(.SpouseBirthDate = DateTime.MinValue, "NULL", "'" & .SpouseBirthDate.ToString("yyyy-MM-dd") & "'") & ",'" & .SpouseOccupation & "'," & IIf(.MarriageAnivDate = DateTime.MinValue, "NULL", "'" & .MarriageAnivDate.ToString("yyyy-MM-dd") & "'") & "," & _
    '                                  "'" & .FacebookId & "','" & .TwitterId & "','" & .LinkedInID & "'," & _
    '                                  "'" & .GooglePlusId & "','" & .Hi5Id & "','" & .MySpaceId & "'," & _
    '                                  "'" & .IbiboId & "','" & .FourSquareId & "','" & .OrkutId & "','" & .SkypeId & "'," & _
    '                                  .BalancePoints & ",0.0,0.0," & _
    '                                  "'" & .SiteCode & "','" & request.CreatedBy & "',getdate(),'" & .SiteCode & "','" & request.CreatedBy & "',getdate(),1,'', " & _
    '                                  "'" & .CustomerGroup & "','" & .RegistrationStatus & "') "

    '            End With

    '            InsertOrUpdateRecord(query, tran)

    '            For Each address In request.CLPCustomer.AddressList
    '                address.CardNumber = request.CLPCustomer.CardNumber
    '                address.ClpProgId = request.CLPCustomer.ClpProgId
    '                query = "insert into CLPCustomerAddress (CardNo, Clpprogramid, AddressType, AddressLn1, AddressLn2, AddressLn3, AddressLn4, PinCode, CityCode, " & _
    '                   "StateCode, CountryCode, Defaults, CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS)" & _
    '                   "values('" & address.CardNumber & "','" & address.ClpProgId & "','" & address.AddressType & "','" & address.AddLine1 & "','" & address.AddLine2 & "','" & address.AddLine3 & "','" & _
    '                    address.AddLine4 & "','" & address.PinCode & "','" & address.City & "','" & address.State & "','" & address.Country & "',1,'" & request.CLPCustomer.SiteCode & "'," & _
    '                    "'" & request.CreatedBy & "',getdate(),'" & request.CLPCustomer.SiteCode & "','" & request.CreatedBy & "',getdate(),1) "
    '                InsertOrUpdateRecord(query, tran)
    '            Next

    '            If (request.CLPCustomer.BalancePoints > 0) Then
    '                Dim clpTranNo As String = objClsCommon.getDocumentNo("CLPAccPoints", request.CLPCustomer.SiteCode)
    '                Dim clpTranID = "CRS" & request.CLPCustomer.SiteCode.Substring(request.CLPCustomer.SiteCode.Length - 3, 3)
    '                clpTranID = objClsCommon.GenDocNo(clpTranID, 15, clpTranNo)

    '                query = "INSERT INTO CLPTransaction (SiteCode,BillNo,BillDate,AccumLationPoints,RedemptionPoints,BalAccumlationPoints,ClpProgramId,ClpCustomerId,IsRedemptionProcess,"
    '                query += "CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS,Source) "
    '                query += "VALUES ('" & request.CLPCustomer.SiteCode & "','" & clpTranID & "', getdate()," & request.CLPCustomer.BalancePoints & ",0," & request.CLPCustomer.BalancePoints & ",'" & request.CLPCustomer.ClpProgId & "','" & request.CLPCustomer.CardNumber & "',0,"
    '                query += "'" & request.CLPCustomer.SiteCode & "','" & request.CreatedBy & "', getdate(),'" & request.CLPCustomer.SiteCode & "','" & request.CreatedBy & "',getdate(),1,NULL)"
    '                InsertOrUpdateRecord(query, tran)

    '                objClsCommon.UpdateDocumentNo("CLPAccPoints", SpectrumCon, tran)
    '            End If

    '            objClsCommon.UpdateDocumentNo("Customer Loyalty", SpectrumCon, tran)
    '        Else
    '            Dim prevoiusNumber As String = String.Empty
    '            If CheckIfUniqueMobileNo(request, prevoiusNumber) = False Then
    '                request.CLPCustomer.MobileNo = prevoiusNumber
    '            End If
    '            tran = SpectrumCon.BeginTransaction()

    '            query = "UPDATE CLPCustomers SET FirstName = '" & request.CLPCustomer.FirstName & "', MiddleName = '" & request.CLPCustomer.MiddleName & "', SurName = '" & request.CLPCustomer.LastName & "',Gender = '" & request.CLPCustomer.Gender & "',"
    '            query += "CustomerGroup='" & request.CLPCustomer.CustomerGroup & "',BirthDate=" & IIf(request.CLPCustomer.BirthDate = DateTime.MinValue, "null", "'" & request.CLPCustomer.BirthDate.ToString("yyyy-MM-dd") & "'") & ",EmailId ='" & request.CLPCustomer.EmailId & "',Mobileno ='" & request.CLPCustomer.MobileNo & "',UPDATEDAT = '" & request.CLPCustomer.SiteCode & "' , "
    '            query += "UPDATEDBY = '" & request.UpdatedBy & "' , UPDATEDON = getdate() , resgistrationstatus='" & request.CLPCustomer.RegistrationStatus & "', "

    '            If (request.CLPCustomer.IsJoiningPointAccumlated) Then
    '                query += "TotalBalancePoint += " & request.CLPCustomer.BalancePoints & ", PointsAccumlated += " & request.CLPCustomer.PointsAccumlated & ", "
    '            End If
    '            query += "Res_Phone = '" & request.CLPCustomer.ResidenceNumber & "' "

    '            query += "WHERE CardNo = '" & request.CLPCustomer.CardNumber & "' and ClpProgramId='" & request.CLPCustomer.ClpProgId & "'"

    '            InsertOrUpdateRecord(query, tran)
    '            For Each address In request.CLPCustomer.AddressList
    '                query = "update CLPCustomerAddress Set AddressLn1 = '" & address.AddLine1 & "',AddressLn2='" & address.AddLine2 & "', AddressLn3='" & address.AddLine3 & "', AddressLn4='" & address.AddLine4 & "',PinCode='" & address.PinCode & "'," & _
    '                        "CityCode='" & address.City & "',StateCode='" & address.State & "',CountryCode='" & address.Country & "', UPDATEDAT = '" & request.CLPCustomer.SiteCode & "', UPDATEDBY = '" & request.UpdatedBy & "' , UPDATEDON = getdate() " & _
    '                        " where CardNo = '" & address.CardNumber & "' and Clpprogramid = '" & address.ClpProgId & "' and AddressType ='" & address.AddressType & "'"
    '                InsertOrUpdateRecord(query, tran)
    '            Next

    '            If (request.CLPCustomer.IsJoiningPointAccumlated) Then
    '                Dim clpTranNo As String = objClsCommon.getDocumentNo("CLPAccPoints", request.CLPCustomer.SiteCode)
    '                Dim clpTranID = "CRS" & request.CLPCustomer.SiteCode.Substring(request.CLPCustomer.SiteCode.Length - 3, 3)
    '                clpTranID = objClsCommon.GenDocNo(clpTranID, 15, clpTranNo)

    '                query = "INSERT INTO CLPTransaction (SiteCode,BillNo,BillDate,AccumLationPoints,RedemptionPoints,BalAccumlationPoints,ClpProgramId,ClpCustomerId,IsRedemptionProcess,"
    '                query += "CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS,Source) "
    '                query += "VALUES ('" & request.CLPCustomer.SiteCode & "','" & clpTranID & "', getdate()," & request.CLPCustomer.BalancePoints & ",0," & request.CLPCustomer.PointsAccumlated & ",'" & request.CLPCustomer.ClpProgId & "','" & request.CLPCustomer.CardNumber & "',0,"
    '                query += "'" & request.CLPCustomer.SiteCode & "','" & request.CreatedBy & "', getdate(),'" & request.CLPCustomer.SiteCode & "','" & request.CreatedBy & "',getdate(),1,NULL)"
    '                InsertOrUpdateRecord(query, tran)

    '                objClsCommon.UpdateDocumentNo("CLPAccPoints", SpectrumCon, tran)
    '            End If

    '        End If
    '        tran.Commit()
    '        Return True
    '    Catch ex As CLPProgramNotMapped
    '        tran.Rollback()
    '        Throw
    '    Catch ex As Exception
    '        tran.Rollback()
    '        Throw
    '    Finally
    '        CloseConnection()
    '    End Try
    'End Function


    Public Function SaveCustomerInfo(ByRef request As SaveCustomerRequest) As Boolean Implements ICreateCustomer.SaveCustomerInfo
        Dim tran As SqlTransaction
        Try
            Dim query As String = String.Empty
            If CheckIfInsertOrUpdate(request) = False Then
                Dim docNo As String = objClsCommon.getDocumentNo("Customer Loyalty", request.CLPCustomer.SiteCode)
                Dim otherCharacters = "CLS" & request.CLPCustomer.SiteCode.Substring(request.CLPCustomer.SiteCode.Length - 3, 3)

                'String.Concat(otherCharacters,docNo.PadLeft(15-otherCharacters.Length,"0"))	"CLSHM1000000008"
                request.CLPCustomer.CardNumber = objClsCommon.GenDocNo(otherCharacters, 15, docNo)

                query = "select top(1) ClpProgramId from CLPProgramSiteMap where Sitecode='" & request.CLPCustomer.SiteCode & "' AND Status=1 "
                Using dataReader As SqlDataReader = GetReader(query)
                    If dataReader.HasRows Then
                        Do While dataReader.Read()
                            Dim customerGroup As New CustomerGroupDetails
                            request.CLPCustomer.ClpProgId = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        Loop
                    Else
                        Throw New CLPProgramNotMapped("No Clp program is mapped to this site.")
                    End If
                End Using
                tran = SpectrumCon.BeginTransaction()

                'query = "insert into dbo.CLPCustomers (CardNo, ClpProgramId, AccountNo, SiteCode, Title, FirstName, MiddleName, SurName, NameOnCard, CardType, Gender, BirthDate, Education, Occupation, EmailId, MaritalStatus, Mobileno, Res_Phone, OfficeNo, CardExpiryDT, CardisActive, CardIssueDT, ReferedBy, RelationWithPrimaryCard, IsAddonCard, PromotionInfobyEmail, PromotionInfobySMS, SpouseTitle, SpouseFirstName, SpouseMiddleName, SpouseSurName, SpouseDob, SpouseOccupation, MarriageAnivDate, TotalBalancePoint, PointsAccumlated, PointsRedeemed, CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS, OldCardType,CustomerGroup,resgistrationstatus)" & _
                '        "values('" & request.CLPCustomer.CardNumber & "','" & request.CLPCustomer.ClpProgId & "','" & request.CLPCustomer.CardNumber & "'," & _
                '        "'" & request.CLPCustomer.SiteCode & "','','" & request.CLPCustomer.FirstName & "','" & request.CLPCustomer.MiddleName & "','" & request.CLPCustomer.LastName & "'," & _
                '        "'" & String.Format("{0} {1}", request.CLPCustomer.FirstName, request.CLPCustomer.LastName) & "','" & request.CLPCustomer.CardType & "','" & request.CLPCustomer.Gender & "'," & IIf(request.CLPCustomer.BirthDate = DateTime.MinValue, "null", "'" & request.CLPCustomer.BirthDate.ToString("yyyy-MM-dd") & "'") & ",'','','" & request.CLPCustomer.EmailId & "','','" & request.CLPCustomer.MobileNo & "','" & request.CLPCustomer.ResidenceNumber & "','','" & DateTime.MaxValue.ToString("yyyy-MM-dd") & "','True',NULL,'','',0,1,1,'','','','',NULL,'',NULL," & request.CLPCustomer.BalancePoints & ",0.0,0.0," & _
                '        "'" & request.CLPCustomer.SiteCode & "','" & request.CreatedBy & "',getdate(),'" & request.CLPCustomer.SiteCode & "','" & request.CreatedBy & "',getdate(),1,'','" & request.CLPCustomer.CustomerGroup & "','" & request.CLPCustomer.RegistrationStatus & "') "
                With request.CLPCustomer

                    query = "insert into dbo.CLPCustomers (CardNo, ClpProgramId, AccountNo, SiteCode, " & _
                                    " Title, FirstName, MiddleName, SurName, " & _
                                     " NameOnCard, CardType, Gender, BirthDate," & _
                                     " Education, Occupation, EmailId, MaritalStatus," & _
                                     " Mobileno, Res_Phone, OfficeNo, CardExpiryDT, " & _
                                     " CardisActive, CardIssueDT, ReferedBy, RelationWithPrimaryCard," & _
                                     " IsAddonCard, PromotionInfobyEmail, PromotionInfobySMS, SpouseTitle, " & _
                                     " SpouseFirstName, SpouseMiddleName, SpouseSurName, SpouseDob," & _
                                     " SpouseOccupation, MarriageAnivDate, " & _
                                     " TotalBalancePoint, PointsAccumlated, PointsRedeemed, " & _
                                     " CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS, " & _
                                     " OldCardType,CustomerGroup,resgistrationstatus," & _
                                      " ReminderComments,OrkutId,Level,FacebookId,TwitterId,GooglePlusId,Hi5Id,MySpaceId,IbiboId,FourSquareId,SkypeId,LinkedInId)" & _
                            " Values('" & .CardNumber & "','" & .ClpProgId & "','" & .CardNumber & "','" & .SiteCode & "'," & _
                                        "'" & .Title & "','" & .FirstName & "','" & .MiddleName & "','" & .LastName & "'," & _
                                        "'" & String.Format("{0} {1}", .FirstName, .LastName) & "','" & .CardType & "','" & .Gender & "'," & IIf(.BirthDate = DateTime.MinValue, "null", "'" & .BirthDate.ToString("yyyy-MM-dd") & "'") & "," & _
                                        "'" & .Education & "','" & .Occupation & "','" & .EmailId & "','" & .MaritalStatus & "'," & _
                                        "'" & .MobileNo & "','" & .ResidenceNumber & "','" & .OfficeNumber & "','" & DateTime.MaxValue.ToString("yyyy-MM-dd") & "'," & _
                                        "'True',NULL,'',''," & _
                                        "0," & .PromotionInfobyEmail & "," & .PromotionInfobySMS & ",'" & .SpouseTitle & "'," & _
                                        "'" & .SpouseFirstName & "','" & .SpouseMiddleName & "','" & .SpouseLastName & "'," & IIf(.SpouseBirthDate = DateTime.MinValue, "null", "'" & .SpouseBirthDate.ToString("yyyy-MM-dd") & "'") & ",'" & _
                                        .SpouseOccupation & "'," & IIf(.MarriageAnivDate = DateTime.MinValue, "null", "'" & .MarriageAnivDate.ToString("yyyy-MM-dd") & "'") & "," _
                                        & .BalancePoints & ",0.0,0.0," & _
                                        "'" & .SiteCode & "','" & request.CreatedBy & "',getdate(),'" & .SiteCode & "','" & request.CreatedBy & "',getdate(),1,'','" & .CustomerGroup & "','" & .RegistrationStatus & "','" & .ReminderComment & "','" & .GSTNo & "','" & .Level & "','" & .FacebookId & "','" & .TwitterId & "','" & .GooglePlusId & "','" & .Hi5Id & "','" & .MySpaceId & "','" & .IbiboId & "','" & .FourSquareId & "','" & .SkypeId & "','" & .LinkedInID & "') "

                End With

                InsertOrUpdateRecord(query, tran)

                For Each address In request.CLPCustomer.AddressList
                    address.CardNumber = request.CLPCustomer.CardNumber
                    address.ClpProgId = request.CLPCustomer.ClpProgId
                    Dim DefaultValue As Integer = 0
                    If address.AddressType = 1 Then
                        DefaultValue = 1
                    Else
                        DefaultValue = 0
                    End If

                    query = " INSERT INTO CLPCustomerAddress (CardNo, Clpprogramid, AddressType, AddressLn1, AddressLn2, AddressLn3, AddressLn4, PinCode, CityCode, " & _
                       "   StateCode, CountryCode, Defaults, CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS)" & _
                       "    values('" & address.CardNumber & "','" & address.ClpProgId & "','" & address.AddressType & "','" & address.AddLine1 & "','" & address.AddLine2 & "','" & address.AddLine3 & "','" & _
                        address.AddLine4 & "','" & address.PinCode & "','" & address.City & "','" & address.State & "','" & address.Country & "'," & DefaultValue & ",'" & request.CLPCustomer.SiteCode & "'," & _
                        "'" & request.CreatedBy & "',getdate(),'" & request.CLPCustomer.SiteCode & "','" & request.CreatedBy & "',getdate(),1) "

                    InsertOrUpdateRecord(query, tran)

                Next

                If (request.CLPCustomer.BalancePoints > 0) Then
                    Dim clpTranNo As String = objClsCommon.getDocumentNo("CLPAccPoints", request.CLPCustomer.SiteCode)
                    Dim clpTranID = "CRS" & request.CLPCustomer.SiteCode.Substring(request.CLPCustomer.SiteCode.Length - 3, 3)
                    clpTranID = objClsCommon.GenDocNo(clpTranID, 15, clpTranNo)

                    query = "INSERT INTO CLPTransaction (SiteCode,BillNo,BillDate,AccumLationPoints,RedemptionPoints,BalAccumlationPoints,ClpProgramId,ClpCustomerId,IsRedemptionProcess,"
                    query += "CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS,Source) "
                    query += "VALUES ('" & request.CLPCustomer.SiteCode & "','" & clpTranID & "', getdate()," & request.CLPCustomer.BalancePoints & ",0," & request.CLPCustomer.BalancePoints & ",'" & request.CLPCustomer.ClpProgId & "','" & request.CLPCustomer.CardNumber & "',0,"
                    query += "'" & request.CLPCustomer.SiteCode & "','" & request.CreatedBy & "', getdate(),'" & request.CLPCustomer.SiteCode & "','" & request.CreatedBy & "',getdate(),1,NULL)"
                    InsertOrUpdateRecord(query, tran)

                    objClsCommon.UpdateDocumentNo("CLPAccPoints", SpectrumCon, tran)
                End If

                objClsCommon.UpdateDocumentNo("Customer Loyalty", SpectrumCon, tran)
            Else
                Dim prevoiusNumber As String = String.Empty
                If CheckIfUniqueMobileNo(request, prevoiusNumber) = False Then
                    request.CLPCustomer.MobileNo = prevoiusNumber
                End If
                tran = SpectrumCon.BeginTransaction()
                With request.CLPCustomer

                    query = "UPDATE CLPCustomers SET FirstName = '" & .FirstName & "', MiddleName = '" & .MiddleName & "', SurName = '" & .LastName & "',Gender = '" & .Gender & "',"
                    query += "CustomerGroup='" & .CustomerGroup & "',BirthDate=" & IIf(.BirthDate = DateTime.MinValue, "null", "'" & .BirthDate.ToString("yyyy-MM-dd") & "'") & ",EmailId ='" & .EmailId & "',Mobileno ='" & .MobileNo & "',UPDATEDAT = '" & .SiteCode & "' , "
                    query += "UPDATEDBY = '" & request.UpdatedBy & "' , UPDATEDON = getdate() , resgistrationstatus='" & .RegistrationStatus & "',Level='" & .Level & "', "

                    If (request.CLPCustomer.IsJoiningPointAccumlated) Then
                        query += "TotalBalancePoint += " & .BalancePoints & ", PointsAccumlated += " & .PointsAccumlated & ", "
                    End If
                    query += "ReminderComments = '" & .ReminderComment & "', "
                    query += "Res_Phone = '" & .ResidenceNumber & "', "
                    query += "Title = '" & .Title & "', "
                    query += "Education = '" & .Education & "', "
                    query += "Occupation = '" & .Occupation & "', "
                    query += "MaritalStatus = '" & .MaritalStatus & "', "
                    query += "OfficeNo = '" & .OfficeNumber & "', "
                    query += "PromotionInfobyEmail = '" & .PromotionInfobyEmail & "', "
                    query += "PromotionInfobySMS = '" & .PromotionInfobySMS & "', "
                    query += "SpouseTitle = '" & .SpouseTitle & "', "
                    query += "SpouseFirstName = '" & .SpouseFirstName & "', "
                    query += "SpouseMiddleName = '" & .SpouseMiddleName & "', "
                    query += "SpouseSurName = '" & .SpouseLastName & "', "
                    query += "FacebookId = '" & .FacebookId & "', "
                    query += "TwitterId = '" & .TwitterId & "', "
                    query += "LinkedInID = '" & .LinkedInID & "', "
                    query += "GooglePlusId = '" & .GooglePlusId & "', "
                    query += "Hi5Id = '" & .Hi5Id & "', "
                    query += "MySpaceId = '" & .MySpaceId & "', "
                    query += "IbiboId = '" & .IbiboId & "', "
                    query += "FourSquareId = '" & .FourSquareId & "', "
                    'query += "CustomerGroup='" & .CustomerGroup & "'"
                    ' query += "OrkutId = '" & .OrkutId & "', "
                    query += "OrkutId = '" & .GSTNo & "', "
                    query += "SkypeId = '" & .SkypeId & "', "
                    query += "SpouseOccupation = '" & .SpouseOccupation & "', "
                    query += "SpouseDob = " & IIf(.SpouseBirthDate = DateTime.MinValue, "null", "'" & .SpouseBirthDate.ToString("yyyy-MM-dd") & "'") & ","
                    query += "MarriageAnivDate = " & IIf(.MarriageAnivDate = DateTime.MinValue, "null", "'" & .MarriageAnivDate.ToString("yyyy-MM-dd") & "'") & ","
                    query += "NameOnCard = '" & String.Format("{0} {1}", .FirstName, .LastName) & "'"

                    query += "WHERE CardNo = '" & .CardNumber & "' and ClpProgramId='" & .ClpProgId & "'"

                End With

                InsertOrUpdateRecord(query, tran)

                '------ Code changes by mahesh mow Check Inserted or Updated Case ...

                For Each address In request.CLPCustomer.AddressList

                    Dim DefaultValue As Integer = 0
                    If address.AddressType = 1 Then
                        DefaultValue = 1
                    Else
                        DefaultValue = 0
                    End If

                    query = " SELECT 1 FROM  CLPCustomerAddress " & _
                            " WHERE CardNo = '" & request.CLPCustomer.CardNumber & "' and Clpprogramid = '" & request.CLPCustomer.ClpProgId & "' and AddressType ='" & address.AddressType & "'"

                    Dim dtTemp = GetScalerValue(query, tran)

                    If dtTemp <> Nothing AndAlso dtTemp.ToString() = "1" Then
                        query = "update CLPCustomerAddress Set AddressLn1 = '" & address.AddLine1 & "',AddressLn2='" & address.AddLine2 & "', AddressLn3='" & address.AddLine3 & "', AddressLn4='" & address.AddLine4 & "',PinCode='" & address.PinCode & "'," & _
                                "CityCode='" & address.City & "',StateCode='" & address.State & "',CountryCode='" & address.Country & "', UPDATEDAT = '" & request.CLPCustomer.SiteCode & "', UPDATEDBY = '" & request.UpdatedBy & "' , UPDATEDON = getdate() " & _
                                " where CardNo = '" & request.CLPCustomer.CardNumber & "' and Clpprogramid = '" & request.CLPCustomer.ClpProgId & "' and AddressType ='" & address.AddressType & "'"

                    Else
                        query = " insert into CLPCustomerAddress (CardNo, Clpprogramid, AddressType, AddressLn1, AddressLn2, AddressLn3, AddressLn4, PinCode, CityCode, " & _
                         " StateCode, CountryCode, Defaults, CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS)" & _
                         " values('" & request.CLPCustomer.CardNumber & "','" & request.CLPCustomer.ClpProgId & "','" & address.AddressType & "','" & address.AddLine1 & "','" & address.AddLine2 & "','" & address.AddLine3 & "','" & _
                          address.AddLine4 & "','" & address.PinCode & "','" & address.City & "','" & address.State & "','" & address.Country & "'," & DefaultValue & ",'" & request.CLPCustomer.SiteCode & "'," & _
                         "'" & request.CreatedBy & "',getdate(),'" & request.CLPCustomer.SiteCode & "','" & request.CreatedBy & "',getdate(),1) "

                    End If
                    InsertOrUpdateRecord(query, tran)
                Next

                If (request.CLPCustomer.IsJoiningPointAccumlated) Then
                    Dim clpTranNo As String = objClsCommon.getDocumentNo("CLPAccPoints", request.CLPCustomer.SiteCode)
                    Dim clpTranID = "CRS" & request.CLPCustomer.SiteCode.Substring(request.CLPCustomer.SiteCode.Length - 3, 3)
                    clpTranID = objClsCommon.GenDocNo(clpTranID, 15, clpTranNo)

                    query = "INSERT INTO CLPTransaction (SiteCode,BillNo,BillDate,AccumLationPoints,RedemptionPoints,BalAccumlationPoints,ClpProgramId,ClpCustomerId,IsRedemptionProcess,"
                    query += "CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS,Source) "
                    query += "VALUES ('" & request.CLPCustomer.SiteCode & "','" & clpTranID & "', getdate()," & request.CLPCustomer.BalancePoints & ",0," & request.CLPCustomer.PointsAccumlated & ",'" & request.CLPCustomer.ClpProgId & "','" & request.CLPCustomer.CardNumber & "',0,"
                    query += "'" & request.CLPCustomer.SiteCode & "','" & request.CreatedBy & "', getdate(),'" & request.CLPCustomer.SiteCode & "','" & request.CreatedBy & "',getdate(),1,NULL)"
                    InsertOrUpdateRecord(query, tran)

                    objClsCommon.UpdateDocumentNo("CLPAccPoints", SpectrumCon, tran)
                End If

            End If
            tran.Commit()
            Return True
        Catch ex As CLPProgramNotMapped
            tran.Rollback()
            Throw
        Catch ex As Exception
            tran.Rollback()
            Throw
        Finally
            CloseConnection()
        End Try
    End Function



    Private Function CheckIfInsertOrUpdate(request As SaveCustomerRequest) As Boolean
        Try         
            If String.IsNullOrEmpty(request.CLPCustomer.CardNumber) Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw
        Finally
            'CloseConnection()
        End Try
    End Function

    Private Function CheckIfUniqueMobileNo(request As SaveCustomerRequest, ByRef previousMobileNo As String) As Boolean
        Dim query As String = "select * from CLPCustomers where Mobileno = '" & request.CLPCustomer.MobileNo & "' and CardNo<> '" & request.CLPCustomer.CardNumber & "' and ClpProgramId<>'" & request.CLPCustomer.ClpProgId & "' "
        Dim dt As DataTable = GetFilledTable(query)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            Return True
        Else
            query = "Select Mobileno from CLPCustomers where CardNo='" & request.CLPCustomer.CardNumber & "' and ClpProgramId = '" & request.CLPCustomer.ClpProgId & "'"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        previousMobileNo = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                    Loop             
                End If
            End Using
            Return False
        End If
    End Function
End Class
