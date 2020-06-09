Imports SpectrumCommon
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Collections
Public Class PettyCashVoucher
    Inherits clsPettyCashBase
    Implements IPettyCashVoucher
    Private objClsCommon As New clsCommon
    Public Function GetVoucherDetails(ByVal request As SpectrumCommon.GetVoucherEntryRequest) As SpectrumCommon.GetVoucherEntryResponse Implements IPettyCashVoucher.GetVoucherDetails
        Try
            Dim response As New GetVoucherEntryResponse()
            response.VoucherHeader = GetVoucherData(request)
            response.VoucherAccountTypes = GetVoucherAccountType()
            response.VoucherTypes = GetVoucherType()
            response.VoucherParty = GetAllVoucherParty(request)
            Return response
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Private Function GetVoucherData(ByVal request As GetVoucherEntryRequest) As VoucherHdr
        Try
            Dim vchrHdr As New VoucherHdr()
            Dim query As String = "select VoucherID,VoucherTypeCode,Sitecode,FinYear,ExpenseDate,TotalAmt,VoucherAccountID,PaidTo,Currency, " & _
                                  "Approvedby,ReceivedBY,PreparedBY,Approvalstatus,EmployeeID,SupplierID,RefDocNumber,RefDocDate from  VoucherHDR where " & _
                                  "VoucherID='" & request.VoucherID & "' and Sitecode = '" & request.SiteCode & "' and FinYear = '" & request.FinYear & "' "
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    dataReader.Read()
                    vchrHdr.VoucherID = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                    vchrHdr.VoucherTypeCode = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                    vchrHdr.SiteCode = IIf(IsDBNull(dataReader.GetString(2)), String.Empty, dataReader.GetString(2))
                    vchrHdr.FinYear = IIf(IsDBNull(dataReader.GetString(3)), String.Empty, dataReader.GetString(3))
                    vchrHdr.ExpenseDate = IIf(IsDBNull(dataReader.GetDateTime(4)), DateTime.MinValue, dataReader.GetDateTime(4))
                    vchrHdr.TotalAmt = IIf(IsDBNull(dataReader.GetDecimal(5)), 0, dataReader.GetDecimal(5))
                    vchrHdr.VoucherAccountID = IIf(IsDBNull(dataReader.GetString(6)), String.Empty, dataReader.GetString(6))
                    vchrHdr.PaidTo = IIf(IsDBNull(dataReader.GetString(7)), String.Empty, dataReader.GetString(7))
                    vchrHdr.CurrencyCode = IIf(IsDBNull(dataReader.GetString(8)), String.Empty, dataReader.GetString(8))
                    vchrHdr.Approvedby = IIf(IsDBNull(dataReader.GetString(9)), String.Empty, dataReader.GetString(9))
                    vchrHdr.ReceivedBY = IIf(IsDBNull(dataReader.GetString(10)), String.Empty, dataReader.GetString(10))
                    vchrHdr.PreparedBY = IIf(IsDBNull(dataReader.GetString(11)), String.Empty, dataReader.GetString(11))
                    vchrHdr.Approvalstatus = IIf(IsDBNull(dataReader.GetBoolean(12)), False, dataReader.GetBoolean(12))
                    vchrHdr.EmployeeID = IIf(IsDBNull(dataReader.GetString(13)), String.Empty, dataReader.GetString(13))
                    vchrHdr.SupplierID = IIf(IsDBNull(dataReader.GetString(14)), String.Empty, dataReader.GetString(14))
                    vchrHdr.RefDocNumber = IIf(IsDBNull(dataReader.GetValue(15)), String.Empty, dataReader.GetValue(15))
                    Dim refDate As Object = IIf(IsDBNull(dataReader.GetValue(16)), Nothing, dataReader.GetValue(16))
                    If refDate IsNot Nothing Then
                        vchrHdr.RefDocDate = IIf(IsDBNull(dataReader.GetDateTime(16)), Nothing, dataReader.GetDateTime(16))
                    End If
                End If
            End Using
            If String.IsNullOrEmpty(vchrHdr.VoucherID) Then               
                Dim docNo As String = objClsCommon.getDocumentNo("PettyCashVoucher", request.SiteCode)
                'Dim character As Char = request.SiteCode.FirstOrDefault(Function(x) x <> "0")
                'Dim otherCharacters = "PCV" & request.SiteCode.Substring(request.SiteCode.IndexOf(character)) & "S"
                'vchrHdr.VoucherID = GenDocNo(otherCharacters, 15, docNo)
                vchrHdr.VoucherID = GenDocNo("PCV" & (request.SiteCode).Substring((request.SiteCode).Length - 3, 3) & (request.FinYear).Substring((request.FinYear).Length - 2, 2), 15, docNo)
            End If
            If String.IsNullOrEmpty(vchrHdr.CurrencyCode) Then
                vchrHdr.CurrencyCode = GetLocalSiteCurrency(request.SiteCode)
            End If
            vchrHdr.VoucherDetails = GetVoucherDtls(request)
            Return vchrHdr
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Private Function GetLocalSiteCurrency(ByVal siteCode As String) As String
        Try
            Dim localCurrencyCode As String = String.Empty
            Dim query As String = "select LocalCurrancyCode from MstSite where SiteCode = '" & siteCode & "'"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    dataReader.Read()
                    localCurrencyCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                End If
            End Using
            Return localCurrencyCode
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Private Function GetVoucherDtls(ByVal request As GetVoucherEntryRequest) As List(Of VoucherDtl)
        Try
            Dim voucherdetailsList As New List(Of VoucherDtl)
            Dim query As String = "select VoucherID,Sitecode,FinYear,LineNumber,Amount,Narration from VoucherDTL where VoucherID = '" & request.VoucherID & "' " & _
                                   "and Sitecode = '" & request.SiteCode & "' and FinYear = '" & request.FinYear & "' and STATUS = 1"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim vchrDtl As New VoucherDtl
                        vchrDtl.VoucherID = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        vchrDtl.SiteCode = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        vchrDtl.FinYear = IIf(IsDBNull(dataReader.GetString(2)), String.Empty, dataReader.GetString(2))
                        vchrDtl.LineNumber = IIf(IsDBNull(dataReader.GetInt32(3)), 0, dataReader.GetInt32(3))
                        vchrDtl.Amount = IIf(IsDBNull(dataReader.GetDecimal(4)), 0, dataReader.GetDecimal(4))
                        vchrDtl.Narration = IIf(IsDBNull(dataReader.GetString(5)), 0, dataReader.GetString(5))
                        voucherdetailsList.Add(vchrDtl)
                    Loop
                End If
            End Using
            Return voucherdetailsList
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Private Function GetVoucherAccountType() As List(Of VoucherAccountType)
        Try
            Dim voucherAccTypeList As List(Of VoucherAccountType)
            Dim query As String = "select VoucherAccountID ,VoucherTypeCode , AccountType , AccountHead , Narration  from MSTVcherAccType where STATUS = 1"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    voucherAccTypeList = New List(Of VoucherAccountType)
                    Do While dataReader.Read()
                        Dim vchrAccType As New VoucherAccountType
                        vchrAccType.VoucherAccountID = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        vchrAccType.VoucherTypeCode = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        vchrAccType.AccountType = IIf(IsDBNull(dataReader.GetString(2)), String.Empty, dataReader.GetString(2))
                        vchrAccType.AccountHead = IIf(IsDBNull(dataReader.GetString(3)), String.Empty, dataReader.GetString(3))
                        vchrAccType.Narration = IIf(IsDBNull(dataReader.GetString(4)), String.Empty, dataReader.GetString(4))
                        voucherAccTypeList.Add(vchrAccType)
                    Loop
                End If
            End Using
            Return voucherAccTypeList
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Private Function GetVoucherType() As List(Of VoucherType)
        Try
            Dim voucherAccTypeList As List(Of VoucherType)
            Dim query As String = "select VoucherTypeCode , VoucherTypeName  from MSTVoucherType where STATUS = 1"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    voucherAccTypeList = New List(Of VoucherType)
                    Do While dataReader.Read()
                        Dim vchrType As New VoucherType
                        vchrType.VoucherTypeCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        vchrType.VoucherTypeName = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        voucherAccTypeList.Add(vchrType)
                    Loop
                End If
            End Using
            Return voucherAccTypeList
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Private Function GetAllVoucherParty(ByVal request As GetVoucherEntryRequest) As List(Of PartyDTO)
        Try
            Dim voucherPartyList As New List(Of PartyDTO)
            voucherPartyList.AddRange(GetSuppliersForSite(request.SiteCode))
            voucherPartyList.AddRange(GetAllUsers(request.SiteCode))
            Return voucherPartyList
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Private Function GetSuppliersForSite(ByVal siteCode As String) As List(Of PartyDTO)
        Try
            Dim supplierList As New List(Of PartyDTO)
            Dim query As String = "select supp.SupplierCode ,supp.SupplierName,'Supplier' As PartyType   from MstSupplier AS supp Inner Join " & _
                                  "SiteSupplierMap AS sitesupp  on supp.SupplierCode = sitesupp.SupplierCode " & _
                                  "where sitesupp.SiteCode = '" & siteCode & "' order by SupplierName "
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim supplier As New PartyDTO
                        supplier.PartyCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        supplier.PartyName = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        supplier.PartyType = IIf(IsDBNull(dataReader.GetString(2)), String.Empty, dataReader.GetString(2))
                        supplierList.Add(supplier)
                    Loop
                End If
            End Using
            Return supplierList
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Private Function GetAllUsers(ByVal siteCode As String) As List(Of PartyDTO)
        Try
            Dim userList As New List(Of PartyDTO)
            Dim query As String = "select UserID, UserName , 'Employee' As PartyType   from AuthUsers  where status=1 and SiteCode = '" & siteCode & "' order by UserName "
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim user As New PartyDTO
                        user.PartyCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        user.PartyName = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        user.PartyType = IIf(IsDBNull(dataReader.GetString(2)), String.Empty, dataReader.GetString(2))
                        userList.Add(user)
                    Loop
                End If
            End Using
            query = "select  UserID, UserName,'Employee' As PartyType  from AuthUsers where status=1 order by UserName  "
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim user As New PartyDTO
                        user.PartyCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        user.PartyName = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        user.PartyType = IIf(IsDBNull(dataReader.GetString(2)), String.Empty, dataReader.GetString(2))
                        Dim isExist = userList.Where(Function(x) x.PartyCode = user.PartyCode).FirstOrDefault()
                        If isExist Is Nothing Then
                            userList.Add(user)
                        End If
                    Loop
                End If
            End Using
            Return userList
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Private Function GenDocNo(ByVal strDocNo As String, ByVal iMaxLength As Integer, ByVal strSuffix As String) As String
        Try
            If strDocNo.Length < iMaxLength Then
                Dim iTempLength As Integer = iMaxLength - strDocNo.Length
                For i As Integer = 0 To (iTempLength - strSuffix.Length) - 1
                    strDocNo = strDocNo & "0"
                Next
                strDocNo = strDocNo & strSuffix
                Return strDocNo
            End If
            Return strDocNo
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function

    Public Function SaveVoucherData(ByVal request As SaveVoucherRequest) As Boolean Implements IPettyCashVoucher.SaveVoucherData
        Try
            OpenConnection()
            Dim query As String
            Dim tran As SqlTransaction = SpectrumCon.BeginTransaction()
            If request.VoucherHeader.RefDocDate Is Nothing Then
                query = "INSERT INTO VoucherHDR ([VoucherID],[VoucherTypeCode],[Sitecode],[FinYear],[ExpenseDate],[TotalAmt],[VoucherAccountID] " & _
                                  ",[PaidTo],[Currency],[Approvedby],[ReceivedBY],[PreparedBY],[Approvalstatus],[EmployeeID],[SupplierID],[RefDocNumber] " & _
                                  ",[CREATEDAT],[CREATEDBY],[CREATEDON],[UPDATEDAT],[UPDATEDBY],[UPDATEDON],[STATUS]) " & _
                                  "VALUES ('" & request.VoucherHeader.VoucherID & "','" & request.VoucherHeader.VoucherTypeCode & "','" & request.VoucherHeader.SiteCode & "', " & _
                                  "'" & request.VoucherHeader.FinYear & "','" & request.VoucherHeader.ExpenseDate.ToString("yyyy-MM-dd") & "'," & request.VoucherHeader.TotalAmt & ", " & _
                                  "'" & request.VoucherHeader.VoucherAccountID & "','" & request.VoucherHeader.PaidTo & "','" & request.VoucherHeader.CurrencyCode & "','" & request.VoucherHeader.Approvedby & "','" & request.VoucherHeader.ReceivedBY & "' " & _
                                  ",'" & request.VoucherHeader.PreparedBY & "',0,'" & request.VoucherHeader.EmployeeID & "','" & request.VoucherHeader.SupplierID & "', '" & request.VoucherHeader.RefDocNumber & "', " & _
                                  "'" & request.VoucherHeader.CreatedAt & "','" & request.VoucherHeader.CreatedBy & "', getdate() ,'" & request.VoucherHeader.UpdatedAt & "','" & request.VoucherHeader.UpdatedBy & "',getdate(),1)"
            Else
                query = "INSERT INTO VoucherHDR ([VoucherID],[VoucherTypeCode],[Sitecode],[FinYear],[ExpenseDate],[TotalAmt],[VoucherAccountID] " & _
                                  ",[PaidTo],[Currency],[Approvedby],[ReceivedBY],[PreparedBY],[Approvalstatus],[EmployeeID],[SupplierID],[RefDocNumber],[RefDocDate] " & _
                                  ",[CREATEDAT],[CREATEDBY],[CREATEDON],[UPDATEDAT],[UPDATEDBY],[UPDATEDON],[STATUS]) " & _
                                  "VALUES ('" & request.VoucherHeader.VoucherID & "','" & request.VoucherHeader.VoucherTypeCode & "','" & request.VoucherHeader.SiteCode & "', " & _
                                  "'" & request.VoucherHeader.FinYear & "','" & request.VoucherHeader.ExpenseDate.ToString("yyyy-MM-dd") & "'," & request.VoucherHeader.TotalAmt & ", " & _
                                  "'" & request.VoucherHeader.VoucherAccountID & "','" & request.VoucherHeader.PaidTo & "','" & request.VoucherHeader.CurrencyCode & "','" & request.VoucherHeader.Approvedby & "','" & request.VoucherHeader.ReceivedBY & "' " & _
                                  ",'" & request.VoucherHeader.PreparedBY & "',0,'" & request.VoucherHeader.EmployeeID & "','" & request.VoucherHeader.SupplierID & "', '" & request.VoucherHeader.RefDocNumber & "','" & request.VoucherHeader.RefDocDate.Value.ToString("yyyy-MM-dd HH:mm:ss.fff") & "', " & _
                                  "'" & request.VoucherHeader.CreatedAt & "','" & request.VoucherHeader.CreatedBy & "', getdate() ,'" & request.VoucherHeader.UpdatedAt & "','" & request.VoucherHeader.UpdatedBy & "',getdate(),1)"
            End If
            If InsertOrUpdateRecord(query, tran) = False Then
                tran.Rollback()
                Return False
            End If
            For Each item In request.VoucherHeader.VoucherDetails
                query = "Insert INTO VoucherDTL (VoucherID,Sitecode,FinYear,LineNumber,Amount," & _
                    "Narration,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS) " & _
                    "Values ('" & request.VoucherHeader.VoucherID & "','" & request.VoucherHeader.SiteCode & "','" & request.VoucherHeader.FinYear & "'," & item.LineNumber & "," & item.Amount & ",'" & item.Narration & "','" & request.VoucherHeader.CreatedAt & "', " & _
                    "'" & request.VoucherHeader.CreatedBy & "', getdate() ,'" & request.VoucherHeader.UpdatedAt & "','" & request.VoucherHeader.UpdatedBy & "',getdate(),1)"
                If InsertOrUpdateRecord(query, tran) = False Then
                    tran.Rollback()
                    Return False
                End If
            Next
            tran.Commit()
            objClsCommon.UpdateDocumentNo("PettyCashVoucher", SpectrumCon, Nothing)
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function GetAllVoucher(Optional ByVal noOfRecords As String = "") As ViewVoucherResponse Implements IPettyCashVoucher.GetAllVoucher
        Try
            Dim response As New ViewVoucherResponse
            Dim vcherList As New List(Of VoucherHdr)
            Dim query As String
            If noOfRecords = "" Then
                query = "select  VoucherID,hdr.VoucherTypeCode,hdr.Sitecode,FinYear,Convert(Varchar(10),ExpenseDate,105) As ExpenseDate,TotalAmt,VAt.AccountType, " & _
                       "Case  When LEN ( sup.SupplierName  ) > 0 Then sup.SupplierName Else (Case When LEN ( AU.UserName  ) > 0 Then AU.UserName Else hdr.PaidTo End) End AS  PaidTo,Currency, " & _
                       "Approvedby,ReceivedBY,PreparedBY,Approvalstatus,EmployeeID,SupplierID,ExpenseDate as ExpenseDate4Order,cast (0 as bit ) [Delete] from VoucherHDR Hdr " & _
                       "Left Outer Join dbo.MSTVcherAccType VAT on hdr.VoucherAccountID = VAt.VoucherAccountID " & _
                       "Left Outer Join MstSupplier Sup On hdr.PaidTo = sup.SupplierCode " & _
                       "Left Outer Join AuthUsers AU On hdr.PaidTo = AU.UserID " & _
                       "where Hdr.STATUS =1 order by ExpenseDate4Order desc "
            Else
                query = "select Top(" & noOfRecords & ")  VoucherID,hdr.VoucherTypeCode,hdr.Sitecode,FinYear,Convert(Varchar(10),ExpenseDate,105) As ExpenseDate,TotalAmt,VAt.AccountType, " & _
                           "Case  When LEN ( sup.SupplierName  ) > 0 Then sup.SupplierName Else (Case When LEN ( AU.UserName  ) > 0 Then AU.UserName Else hdr.PaidTo End) End AS  PaidTo,Currency, " & _
                           "Approvedby,ReceivedBY,PreparedBY,Approvalstatus,EmployeeID,SupplierID , ExpenseDate as ExpenseDate4Order,cast (0 as bit ) [Delete] from VoucherHDR Hdr " & _
                           "Left Outer Join dbo.MSTVcherAccType VAT on hdr.VoucherAccountID = VAt.VoucherAccountID " & _
                           "Left Outer Join MstSupplier Sup On hdr.PaidTo = sup.SupplierCode " & _
                           "Left Outer Join AuthUsers AU On hdr.PaidTo = AU.UserID " & _
                           "where Hdr.STATUS =1 order by ExpenseDate4Order desc "
            End If
            Dim dt As DataTable = GetFilledTable(query)
            response.VoucherTable = dt
            Return response
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function GetSiteInfo(ByVal siteCode As String) As System.Data.DataTable Implements IPettyCashVoucher.GetSiteInfo
        Try
            Dim query As String = "SELECT SITEOFFICIALNAME, Case when len(ISNULL(SITETELEPHONE1,'')) > 0 And len(ISNULL(SITETELEPHONE2,'')) > 0 then ISNULL(SITETELEPHONE1,'') + ',' +ISNULL(SITETELEPHONE2,'') else ISNULL(SITETELEPHONE1,'')+ISNULL(SITETELEPHONE2,'')END AS TELNO," & _
                                  "ISNULL(SITEADDRESSLN1,'') + ISNULL(SITEADDRESSLN2,'') + ISNULL(SITEADDRESSLN3,'') + ISNULL(CONVERT(VARCHAR,SITEPINCODE),'') AS ADDRESS " & _
                                  "FROM MstSite where SiteCode = '" & siteCode & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function GetPettyCashBalanceAmount(ByVal request As GetVoucherBalanceRequest) As Decimal Implements IPettyCashVoucher.GetPettyCashBalanceAmount
        Try
            Dim openingBalance As Decimal
            Dim totalReceipt As Decimal
            Dim totalExpense As Decimal
            openingBalance = GetPettyCashOpeningBalance(request)
            totalReceipt = GetTotalPettyCashReceipt(request)
            totalExpense = GetTotalPettyCashExpense(request)
            Return openingBalance + totalReceipt - totalExpense
        Catch ex As Exception
            LogException(ex)
            Return 0
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function GetTotalPettyCashExpense(ByVal request As SpectrumCommon.GetVoucherBalanceRequest) As Decimal Implements IPettyCashVoucher.GetTotalPettyCashExpense
        Try
            Dim totalExpense As Decimal
            Dim query As String = "select  isnull (SUM (TotalAmt),0)  from VoucherHDR where Sitecode = '" & request.SiteCode & "' and FinYear = '" & request.FinYear & "' and ExpenseDate = '" & request.DayOpenDate.ToString("yyyy-MM-dd") & "' and VoucherTypeCode = '" & PettyCashVoucherType.Expense.ToString() & "' and Status=1"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        totalExpense = IIf(IsDBNull(dataReader.GetDecimal(0)), 0, dataReader.GetDecimal(0))
                    Loop
                End If
            End Using
            Return totalExpense
        Catch ex As Exception
            LogException(ex)
            Return 0
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function GetTotalPettyCashReceipt(ByVal request As SpectrumCommon.GetVoucherBalanceRequest) As Decimal Implements IPettyCashVoucher.GetTotalPettyCashReceipt
        Try
            Dim totalReceipt As Decimal
            Dim query As String = "select  isnull(SUM (TotalAmt),0) from VoucherHDR where Sitecode = '" & request.SiteCode & "' and FinYear = '" & request.FinYear & "' and ExpenseDate = '" & request.DayOpenDate.ToString("yyyy-MM-dd") & "' and VoucherTypeCode = '" & PettyCashVoucherType.Receipt.ToString() & "' and Status=1"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        totalReceipt = IIf(IsDBNull(dataReader.GetDecimal(0)), 0, dataReader.GetDecimal(0))
                    Loop
                End If
            End Using
            Return totalReceipt
        Catch ex As Exception
            LogException(ex)
            Return 0
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function GetPettyCashOpeningBalance(ByVal request As SpectrumCommon.GetVoucherBalanceRequest) As Decimal Implements IPettyCashVoucher.GetPettyCashOpeningBalance
        Try
            Dim openingBalance As Decimal
            Dim query As String = "select OpeningBal from ExpenseSummary where Sitecode = '" & request.SiteCode & "' and FinYear = '" & request.FinYear & "' and Date = '" & request.DayOpenDate.ToString("yyyy-MM-dd") & "' "
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        openingBalance = IIf(IsDBNull(dataReader.GetDecimal(0)), 0, dataReader.GetDecimal(0))
                    Loop
                End If
            End Using
            Return openingBalance
        Catch ex As Exception
            LogException(ex)
            Return 0
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function CheckIfPettyCashTerminalIsOpen(ByVal request As ValidationRequest) As Boolean Implements IPettyCashVoucher.CheckIfPettyCashTerminalIsOpen
        Try
            Dim result As Boolean
            Dim query As String = "Select OpenCloseStatus from dbo.MstTerminalID where SiteCode = '" & request.SiteCode & "' and TerminalID ='" & request.TerminalID & "' "
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim status As String = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        If status.ToUpper() = "OPEN" Then
                            result = True
                        End If
                    Loop
                End If
            End Using
            Return result
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function
    '----------------------PettyCash Function For ShiftManagement
    Public Function GetTotalPettyCashExpenseShiftWise(ByVal request As SpectrumCommon.GetVoucherBalanceRequest) As Decimal Implements IPettyCashVoucher.GetTotalPettyCashExpenseShiftWise
        Try
            Dim totalExpense As Decimal
            Dim query As String = "select ISNULL(SUM(isnull(TotalAmt,0)),0) from VoucherHDR where Sitecode = '" & request.SiteCode & "' and FinYear = '" & request.FinYear & "' and CreatedOn > @CreatedOn and ExpenseDate = @DayOpenDate and VoucherTypeCode = '" & PettyCashVoucherType.Expense.ToString() & "'"
            Dim cmd As New SqlCommand(query, SpectrumCon())
            cmd.Parameters.Add("@CreatedOn", SqlDbType.DateTime)
            cmd.Parameters("@CreatedOn").Value = request.CreatedOn
            cmd.Parameters.Add("@DayOpenDate", SqlDbType.Date)
            cmd.Parameters("@DayOpenDate").Value = request.DayOpenDate


            '  Dim datareader As New SqlDataReader(cmd,c
            'Dim datareader = cmd.ExecuteReader()
            'If datareader.HasRows Then
            '    Do While datareader.Read()
            OpenConnection()
            totalExpense = Convert.ToDecimal(cmd.ExecuteScalar())
            CloseConnection()
            'totalReceipt = IIf(IsDBNull(datareader.GetDecimal(0)), 0, datareader.GetDecimal(0))
            '    Loop
            'End If
            '  datareader.Close()
            Return totalExpense
            'Using dataReader As SqlDataReader = GetReader(query)
            '    If dataReader.HasRows Then
            '        Do While dataReader.Read()
            '            totalExpense = IIf(IsDBNull(dataReader.GetDecimal(0)), 0, dataReader.GetDecimal(0))
            '        Loop
            '    End If
            'End Using
            'Return totalExpense
        Catch ex As Exception
            LogException(ex)
            Return 0
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function GetTotalPettyCashReceiptShiftWise(ByVal request As SpectrumCommon.GetVoucherBalanceRequest) As Decimal Implements IPettyCashVoucher.GetTotalPettyCashReceiptShiftWise
        Try
            Dim totalReceipt As Decimal

            Dim query As String = "select ISNULL(SUM (isnull(TotalAmt,0)),0) from VoucherHDR where Sitecode = '" & request.SiteCode & "' and FinYear = '" & request.FinYear & "' and CreatedOn > @CreatedOn  and ExpenseDate = @DayOpenDate and VoucherTypeCode = '" & PettyCashVoucherType.Receipt.ToString() & "'"
            Dim cmd As New SqlCommand(query, SpectrumCon())
            cmd.Parameters.Add("@CreatedOn", SqlDbType.DateTime)
            cmd.Parameters("@CreatedOn").Value = request.CreatedOn
            cmd.Parameters.Add("@DayOpenDate", SqlDbType.Date)
            cmd.Parameters("@DayOpenDate").Value = request.DayOpenDate


            '  Dim datareader As New SqlDataReader(cmd,c
            'Dim datareader = cmd.ExecuteReader()
            'If datareader.HasRows Then
            '    Do While datareader.Read()
            OpenConnection()
            totalReceipt = Convert.ToDecimal(cmd.ExecuteScalar())
            CloseConnection()
            'totalReceipt = IIf(IsDBNull(datareader.GetDecimal(0)), 0, datareader.GetDecimal(0))
            '    Loop
            'End If
            '  datareader.Close()
            Return totalReceipt
        Catch ex As Exception
            LogException(ex)
            Return 0
        Finally
            CloseConnection()
        End Try
    End Function
End Class
