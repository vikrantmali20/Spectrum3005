Imports System.Data.SqlClient
Imports System.Text

Public Class clsReservation
    Inherits clsCommon
#Region "Global Variables"
    Dim vStmtQry As New System.Text.StringBuilder
    Dim daScan As New SqlDataAdapter
    Dim dtScan As DataTable
    Dim dsScan As DataSet
#End Region

#Region "Functions"
    ''' <summary>
    ''' Get Reservation Tables List Which are available and Reserved at Particular Time and Date
    ''' </summary>
    ''' <param name="IsEdit"></param>
    ''' <param name="Dates"></param>
    ''' <param name="FromTime"></param>
    ''' <param name="ToTime"></param>
    ''' <param name="sitecode"></param>
    ''' <param name="ReservationBufferedTime"></param>
    ''' <param name="DayOpenDate"></param>
    ''' <param name="CardNo"></param>
    ''' <param name="ReservationTime"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetResevationTableNumber(ByVal IsEdit As Boolean, ByVal Dates As DateTime, ByVal FromTime As DateTime, ByVal ToTime As DateTime, ByVal sitecode As String, ByVal ReservationBufferedTime As String, ByVal DayOpenDate As DateTime, ByVal CustStayTime As Integer, Optional ByVal CardNo As String = Nothing, Optional ByVal ReservationTime As Integer = 0) As DataTable
        Try
            Dim StrSql As String

            StrSql = "EXEC GetDineInReservationTables '" & Dates.ToString("yyyy-MM-dd") & "', '" & FromTime.ToString("HH:mm:ss.fff") & "','" & DayOpenDate.ToString("yyyy-MM-dd") & "','" & sitecode & "','" & CardNo & "','" & ReservationTime & "','" & CustStayTime & "'"

            Dim cmdTrn As New SqlCommand(StrSql, SpectrumCon)
            Dim da As New SqlDataAdapter(cmdTrn)

            Dim dt As New DataTable
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Inserting Customer Data into CLPcustomers and CLPCustomeraddress
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <param name="CardNumber"></param>
    ''' <param name="MobileNo"></param>
    ''' <param name="Sitecode"></param>
    ''' <param name="userid"></param>
    ''' <param name="ClpProgId"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertCustomerEntry(ByVal Name As String, ByVal CardNumber As String, ByVal MobileNo As String, ByVal Sitecode As String, ByVal userid As String, ByVal ClpProgId As String, ByRef tran As SqlTransaction, ByVal CardType As String) As Boolean
        Try
            InsertCustomerEntry = False
            Dim Query As String
            Dim FullName = Name.Split(" ").ToArray
            Dim FirstName, MiddleName, LastName As String
            If FullName.Length = 3 Then
                FirstName = FullName(0)
                MiddleName = FullName(1)
                LastName = FullName(2)
            ElseIf FullName.Length = 2 Then
                FirstName = FullName(0)
                LastName = FullName(1)
            ElseIf FullName.Length = 1 Then
                FirstName = FullName(0)
            End If
            Query = " insert into dbo.CLPCustomers (CardNo, ClpProgramId, AccountNo, SiteCode, " & _
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
                     " OldCardType,CustomerGroup,resgistrationstatus)" & _
                    " Values('" & CardNumber & "','" & ClpProgId & "','" & CardNumber & "','" & Sitecode & "'," & _
                    " '','" & FirstName & "','" & MiddleName & "','" & LastName & "'," & _
                   "'" & String.Format("{0} {1}", FirstName, LastName) & "','" & CardType & "','',NULL," & _
                    "'','','',''," & _
                    "'" & MobileNo & "','','','" & DateTime.MaxValue.ToString("yyyy-MM-dd") & "'," & _
                    "'True',NULL,'',''," & _
                    " 0,0, 0,''," & _
                    "'','','',NULL," & _
                    " '',NULL, 0,0.0,0.0," & _
                     " '" & Sitecode & "','" & userid & "',getdate(),'" & Sitecode & "','" & userid & "',getdate(),1,'','','Enrolled') " & vbCrLf & _
                    " INSERT INTO CLPCustomerAddress (CardNo, Clpprogramid, AddressType, AddressLn1, AddressLn2, AddressLn3, AddressLn4, PinCode, CityCode, " & _
                    "   StateCode, CountryCode, Defaults, CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS)" & _
                    "    values('" & CardNumber & "','" & ClpProgId & "','1','','','','','','','','','1','" & Sitecode & "'," & _
                    "'" & userid & "',getdate(),'" & Sitecode & "','" & userid & "',getdate(),1) "

            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
            cmdTrn.Transaction = tran
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            UpdateDocumentNo("Customer Loyalty", SpectrumCon, tran)
            InsertCustomerEntry = True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    'added by khusrao adil on 17-02-2017 for hostmanagement guest details update
    Public Function UpdateCustomerEntry(ByVal Name As String, ByVal CardNumber As String, ByVal gender As String, ByVal MobileNo As String, ByVal Sitecode As String, ByVal userid As String, ByVal ClpProgId As String, ByRef tran As SqlTransaction, ByVal CardType As String) As Boolean
        Try
            UpdateCustomerEntry = False
            Dim Query As New StringBuilder
            vStmtQry.Length = 0
            Dim FullName = Name.Split(" ").ToArray
            Dim FirstName, MiddleName, LastName As String
            If FullName.Length = 3 Then
                FirstName = FullName(0)
                MiddleName = FullName(1)
                LastName = FullName(2)
            ElseIf FullName.Length = 2 Then
                FirstName = FullName(0)
                LastName = FullName(1)
            ElseIf FullName.Length = 1 Then
                FirstName = FullName(0)
            End If
            Query.Append("update clpcustomers set FirstName='" & FirstName & "',MiddleName='" & MiddleName & "',SurName='" & LastName & "', " & vbCrLf)
            Query.Append("NameOnCard='" & String.Format("{0} {1}", FirstName, LastName) & "',Gender='" & gender & "',Mobileno='" & MobileNo & "', " & vbCrLf)
            Query.Append("UPDATEDAT='" & Sitecode & "',UPDATEDBY='" & userid & "',UPDATEDON=GETDATE() " & vbCrLf)
            Query.Append("where CardNo='" & CardNumber & "' and ClpProgramId='" & ClpProgId & "' and status=1 " & vbCrLf)
            Dim cmdTrn As New SqlCommand(Query.ToString(), SpectrumCon)
            cmdTrn.Transaction = tran
            OpenConnection()
            cmdTrn.ExecuteNonQuery()

            UpdateCustomerEntry = True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Inserting Reservation Data Entry
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <param name="CardNumber"></param>
    ''' <param name="ReservationId"></param>
    ''' <param name="NoPeople"></param>
    ''' <param name="TableList"></param>
    ''' <param name="MobileNo"></param>
    ''' <param name="dDate"></param>
    ''' <param name="FromTime"></param>
    ''' <param name="ToTime"></param>
    ''' <param name="Sitecode"></param>
    ''' <param name="userid"></param>
    ''' <param name="FinYear"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertReservationDetails(ByVal Name As String, ByVal CardNumber As String, ByVal ReservationId As String, ByVal NoPeople As Integer, ByVal TableList As List(Of Short), ByVal MobileNo As String, ByVal dDate As DateTime, ByVal FromTime As DateTime, ByVal ToTime As DateTime, ByVal Sitecode As String, ByVal userid As String, ByVal FinYear As String, ByRef tran As SqlTransaction) As Boolean
        Try
            InsertReservationDetails = False
            Dim Query As String
            Dim FullName = Name.Split(" ").ToArray
            Dim FirstName, MiddleName, LastName As String
            If FullName.Length = 3 Then
                FirstName = FullName(0)
                MiddleName = FullName(1)
                LastName = FullName(2)
            ElseIf FullName.Length = 2 Then
                FirstName = FullName(0)
                LastName = FullName(1)
            ElseIf FullName.Length = 1 Then
                FirstName = FullName(0)
            End If
            For i As Integer = 0 To TableList.Count - 1
                Query = " INSERT INTO ReservationTbl(SiteCode,FinYear,ReservationId,CustomerNo,NoOfPeople,TableNo, MobileNo, Date, FromTime,ToTime,  " & _
                    " CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON,STATUS) " & _
                    " VALUES     ('" & Sitecode & "','" & FinYear & "','" & ReservationId & "','" & CardNumber & "','" & NoPeople & "'," & TableList(i) & ",'" & MobileNo & "','" & dDate.ToString("yyyy-MM-dd HH:mm:ss.fff") & "'," & _
                    "'" & FromTime.ToString("yyyy-MM-dd HH:mm:ss.fff") & "','" & ToTime.ToString("yyyy-MM-dd HH:mm:ss.fff") & "','" & Sitecode & "','" & userid & "',GETDATE(),'" & Sitecode & "','" & userid & "',GETDATE(),1)"
                Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
                cmdTrn.Transaction = tran
                OpenConnection()
                cmdTrn.ExecuteNonQuery()
            Next

            UpdateDocumentNo("RSDine", SpectrumCon, tran)
            InsertReservationDetails = True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Fetch Data of Customer 
    ''' </summary>
    ''' <param name="vSiteCode"></param>
    ''' <param name="vCustomerNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomerDataSet(ByVal vSiteCode As String, ByVal vCustomerNo As String) As DataSet
        Try
            Dim Sqlda As SqlDataAdapter
            Dim Sqlds As DataSet
            vStmtQry.Length = 0
            vStmtQry.Append(" Select * From CLPCUSTOMERS Where  SiteCode='" & vSiteCode & "' And CardNo ='" & vCustomerNo & "'; " & vbCrLf)
            vStmtQry.Append(" Select * From CLPCustomerAddress Where CardNo ='" & vCustomerNo & "'; " & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            'Sqlcmdb = New SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "CLPCustomers"
            Dim KeyCustmInfo(2) As DataColumn
            KeyCustmInfo(0) = Sqlds.Tables("ClpCustomers").Columns("CardNo")
            KeyCustmInfo(1) = Sqlds.Tables("ClpCustomers").Columns("ClpProgramId")
            KeyCustmInfo(2) = Sqlds.Tables("ClpCustomers").Columns("SiteCode")
            Sqlds.Tables("ClpCustomers").PrimaryKey = KeyCustmInfo
            Sqlds.Tables(1).TableName = "CLPCustomerAddress"
            Dim KeyCustmAdds(3) As DataColumn
            KeyCustmAdds(0) = Sqlds.Tables("CLPCustomerAddress").Columns("CardNo")
            KeyCustmAdds(1) = Sqlds.Tables("CLPCustomerAddress").Columns("ClpProgramId")
            Sqlds.Tables("CLPCustomerAddress").PrimaryKey = KeyCustmAdds

            Return Sqlds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Fetch Reservation Table Data
    ''' </summary>
    ''' <param name="CustomerNo"></param>
    ''' <param name="SiteCode"></param>
    ''' <param name="IsEdit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetReservationTableStructure(ByVal CustomerNo As String, ByVal SiteCode As String, Optional ByVal IsEdit As Boolean = False) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select * from ReservationTbl where CustomerNo='" & CustomerNo & "' and SiteCode='" & SiteCode & "'" & vbCrLf)
            If IsEdit = True Then
                vStmtQry.Append("And Status=1" & vbCrLf)
            End If
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            dtScan.TableName = "ReservationTbl"
            Dim Key(4) As DataColumn
            Key(0) = dtScan.Columns("SiteCode")
            Key(1) = dtScan.Columns("FinYear")
            Key(2) = dtScan.Columns("CustomerNo")
            Key(3) = dtScan.Columns("TableNo")
            Key(4) = dtScan.Columns("ReservationId")
            dtScan.PrimaryKey = Key
            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetReservedFullData(ByVal SiteCode As String, Optional ByVal IsEdit As Boolean = False) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select Date,CC.NameOnCard As Name,NoOfPeople,CC.MobileNo AS MobileNo,CC.CardNo as CustomerNo," & vbCrLf)
            vStmtQry.Append("RT.Date,tbl.TableNo as TableList,RT.ReservationId,Convert(Varchar,DATEPART(HOUR,FromTime))+':'+ " & vbCrLf)
            vStmtQry.Append("Convert(Varchar,DATEPART(MINUTE,FromTime))+':'+Convert(Varchar,DATEPART(Second,FromTime))+'.'+Convert(Varchar,DATEPART(MILLISECOND,FromTime)) as FromTime,Convert(Varchar,DATEPART(HOUR,RT.UPDATEDON))+':'+" & vbCrLf)
            vStmtQry.Append(" Convert(Varchar,DATEPART(MINUTE,RT.UPDATEDON)) as UpdatedOn  FROM ReservationTbl RT INNER JOIN (" & vbCrLf)
            vStmtQry.Append(" SELECT ReservationId, TableNo = STUFF((SELECT ', ' + TableNo FROM ReservationTbl b " & vbCrLf)
            vStmtQry.Append(" WHERE b.ReservationId = a.ReservationId AND B.STATUS=1 FOR XML PATH('')), 1, 2, '') " & vbCrLf)
            vStmtQry.Append("FROM ReservationTbl a Where STATUS=1 GROUP BY ReservationId " & vbCrLf)
            vStmtQry.Append(") AS tbl On tbl.ReservationId=RT.ReservationId Left Join CLPCustomers CC ON RT.CustomerNo=CC.CardNo " & vbCrLf)
            vStmtQry.Append("where RT.Status=1 and RT.IsOccupied=0 and RT.SiteCode='" & SiteCode & "'--and CAST(RT.Date as Date)=GETDATE()" & vbCrLf)
            vStmtQry.Append(" Group By TBL.TableNo,RT.ReservationId,CC.NameOnCard,RT.NoOfPeople,CC.Mobileno,CC.CardNo,RT.Date," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar,DATEPART(HOUR,FromTime))+':'+ Convert(Varchar,DATEPART(MINUTE,FromTime))+':'+Convert(Varchar,DATEPART(Second,FromTime))+'.'+Convert(Varchar,DATEPART(MILLISECOND,FromTime))," & vbCrLf)
            vStmtQry.Append("Convert(Varchar,DATEPART(HOUR,RT.UPDATEDON))+':'+ Convert(Varchar,DATEPART(MINUTE,RT.UPDATEDON)) Order By UPDATEDON desc" & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            dtScan.TableName = "ReservationTbl"
            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function UpdateReservDetails(ByVal dtResevr As DataTable) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            Dim clsCommon As New clsCommon()
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If clsCommon.SaveData(dtResevr, SpectrumCon, tran) Then
                If clsCommon.UpdateDocumentNo("RSDine", SpectrumCon, tran) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
            End If
            tran.Commit()
            CloseConnection()
            Return True

        Catch ex As Exception
            tran.Rollback()
            Return False
        End Try
    End Function

    Public Function UpdateCustDetails(ByVal dsCustData As DataSet, Optional ByVal isEdit As Boolean = False) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            Dim clsCommon As New clsCommon()
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If clsCommon.SaveData(dsCustData, SpectrumCon, tran) Then
                If Not isEdit Then
                    If clsCommon.UpdateDocumentNo("CLS", SpectrumCon, tran) = False Then
                        tran.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If
            End If
            tran.Commit()
            CloseConnection()
            Return True

        Catch ex As Exception
            tran.Rollback()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Getting Reserved Details of Different Customers
    ''' </summary>
    ''' <param name="tableNo"></param>
    ''' <param name="CustomerName"></param>
    ''' <param name="NoOfPeople"></param>
    ''' <param name="MobileNo"></param>
    ''' <param name="FromDate"></param>
    ''' <param name="Time"></param>
    ''' <param name="ReservationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetReservationDetails(Optional ByVal tableNo As String = Nothing, Optional ByVal CustomerName As String = Nothing, Optional ByVal NoOfPeople As String = Nothing, Optional ByVal MobileNo As String = Nothing, Optional ByVal FromDate As DateTime = Nothing, Optional ByVal Time As DateTime = Nothing, Optional ByVal ReservationId As String = Nothing) As DataTable
        Try
            Dim checkBoolean As Boolean = False
            'SqlQuery.Append("" & vbCrLf)
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0
            ' CONVERT(BIT,0)AS [SELECT],
            SqlQuery.Append("Select Convert(bit,'False') as Sel,tbl.TableNo as TableNumber," & vbCrLf)
            SqlQuery.Append("CC.NameOnCard As Name,tbl.NoOfPeople,CC.MobileNo AS Phone,CC.CardNo as CustomerNo,RT.Date,DATEADD(ms, -DATEPART(ms, Rt.FromTime),Rt. FromTime) AS FromTime,RT.ReservationId FROM ReservationTbl RT " & vbCrLf)
            SqlQuery.Append(" INNER JOIN (" & vbCrLf)
            SqlQuery.Append(" SELECT ReservationId,NoOfPeople,TableNo =  " & vbCrLf)
            SqlQuery.Append("STUFF((SELECT ', ' + TableNo FROM ReservationTbl b  " & vbCrLf)
            SqlQuery.Append(" WHERE b.ReservationId = a.ReservationId AND B.STATUS=1 AND ISNULL(b.IsOccupied,0)=0  FOR XML PATH('')), 1, 2, '') " & vbCrLf)
            SqlQuery.Append("  FROM ReservationTbl a Where STATUS=1 GROUP BY ReservationId,NoOfPeople " & vbCrLf)
            SqlQuery.Append(") AS tbl On tbl.ReservationId=RT.ReservationId  " & vbCrLf)
            SqlQuery.Append("Left Join CLPCustomers CC ON RT.CustomerNo=CC.CardNo " & vbCrLf)
            If tableNo <> Nothing AndAlso CustomerName <> Nothing AndAlso MobileNo <> Nothing AndAlso FromDate <> Nothing AndAlso Time <> Nothing Then
                SqlQuery.Append("where RT.TableNo IN ('" & tableNo & "') AND  CC.NameOnCard like '%" & CustomerName & "%' AND CC.MobileNo='" & MobileNo & "' AND RT.Date='" & FromDate.ToString("yyyy-MM-dd") & "' AND Cast(RT.FromTime as Time) like '%" & Time.ToString("HH:mm") & "%'" & vbCrLf)
            ElseIf tableNo <> Nothing AndAlso CustomerName <> Nothing AndAlso MobileNo <> Nothing AndAlso FromDate <> Nothing Then
                SqlQuery.Append("where RT.TableNo IN ('" & tableNo & "') AND  CC.NameOnCard like '%" & CustomerName & "%' AND CC.MobileNo='" & MobileNo & "' AND RT.Date='" & FromDate.ToString("yyyy-MM-dd") & "'" & vbCrLf)
            ElseIf tableNo <> Nothing AndAlso CustomerName <> Nothing AndAlso MobileNo <> Nothing AndAlso Time <> Nothing Then
                SqlQuery.Append("where RT.TableNo IN ('" & tableNo & "') AND  CC.NameOnCard like '%" & CustomerName & "%' AND CC.MobileNo='" & MobileNo & "' AND Cast(RT.FromTime as Time) like '%" & Time.ToString("HH:mm") & "%'" & vbCrLf)
            ElseIf tableNo <> Nothing AndAlso MobileNo <> Nothing AndAlso FromDate <> Nothing AndAlso Time <> Nothing Then
                SqlQuery.Append("where RT.TableNo IN ('" & tableNo & "') AND  CC.MobileNo='" & MobileNo & "' AND  RT.Date='" & FromDate.ToString("yyyy-MM-dd") & "' AND Cast(RT.FromTime as Time) like '%" & Time.ToString("HH:mm") & "%'" & vbCrLf)
            ElseIf CustomerName <> Nothing AndAlso MobileNo <> Nothing AndAlso FromDate <> Nothing AndAlso Time <> Nothing Then
                SqlQuery.Append("where CC.NameOnCard like '%" & CustomerName & "%' AND  CC.MobileNo='" & MobileNo & "' AND  RT.Date='" & FromDate.ToString("yyyy-MM-dd") & "' AND Cast(RT.FromTime as Time) like '%" & Time.ToString("HH:mm") & "%'" & vbCrLf)
            ElseIf tableNo <> Nothing AndAlso CustomerName <> Nothing AndAlso MobileNo <> Nothing Then
                SqlQuery.Append("where RT.TableNo IN ('" & tableNo & "') AND  CC.NameOnCard like '%" & CustomerName & "%' AND CC.MobileNo='" & MobileNo & "'" & vbCrLf)
            ElseIf tableNo <> Nothing AndAlso CustomerName <> Nothing AndAlso FromDate <> Nothing Then
                SqlQuery.Append("where RT.TableNo IN ('" & tableNo & "') AND  CC.NameOnCard like '%" & CustomerName & "%' AND RT.Date='" & FromDate.ToString("yyyy-MM-dd") & "'" & vbCrLf)
            ElseIf tableNo <> Nothing AndAlso CustomerName <> Nothing AndAlso Time <> Nothing Then
                SqlQuery.Append("where RT.TableNo IN ('" & tableNo & "') AND  CC.NameOnCard like '%" & CustomerName & "%' AND Cast(RT.FromTime as Time) like '%" & Time.ToString("HH:mm") & "%'" & vbCrLf)
            ElseIf tableNo <> Nothing AndAlso MobileNo <> Nothing AndAlso FromDate <> Nothing Then
                SqlQuery.Append("where RT.TableNo IN ('" & tableNo & "') AND CC.MobileNo='" & MobileNo & "' AND Cast(RT.FromTime as Time) like '%" & Time.ToString("HH:mm") & "%'" & vbCrLf)
            ElseIf tableNo <> Nothing AndAlso MobileNo <> Nothing AndAlso Time <> Nothing Then
                SqlQuery.Append("where RT.TableNo IN ('" & tableNo & "') AND CC.MobileNo='" & MobileNo & "' AND Cast(RT.FromTime as Time) like '%" & Time.ToString("HH:mm") & "%'" & vbCrLf)
            ElseIf tableNo <> Nothing AndAlso FromDate <> Nothing AndAlso Time <> Nothing Then
                SqlQuery.Append("where RT.TableNo IN ('" & tableNo & "') AND RT.Date='" & FromDate.ToString("yyyy-MM-dd") & "' AND Cast(RT.FromTime as Time) like '%" & Time.ToString("HH:mm") & "%'" & vbCrLf)
            ElseIf CustomerName <> Nothing AndAlso FromDate <> Nothing AndAlso Time <> Nothing Then
                SqlQuery.Append("where CC.NameOnCard like '%" & CustomerName & "%' AND RT.Date='" & FromDate.ToString("yyyy-MM-dd") & "' AND Cast(RT.FromTime as Time) like '%" & Time.ToString("HH:mm") & "%'" & vbCrLf)
            ElseIf CustomerName <> Nothing AndAlso MobileNo <> Nothing AndAlso FromDate <> Nothing Then
                SqlQuery.Append("where CC.NameOnCard like '%" & CustomerName & "%' AND CC.MobileNo='" & MobileNo & "' AND RT.Date='" & FromDate.ToString("yyyy-MM-dd") & "'" & vbCrLf)
            ElseIf CustomerName <> Nothing AndAlso MobileNo <> Nothing AndAlso Time <> Nothing Then
                SqlQuery.Append("where CC.NameOnCard like '%" & CustomerName & "%' AND CC.MobileNo='" & MobileNo & "' ANDCast(RT.FromTime as Time) like '%" & Time.ToString("HH:mm") & "%'" & vbCrLf)
            ElseIf tableNo <> Nothing AndAlso CustomerName <> Nothing Then
                SqlQuery.Append("where RT.TableNo IN ('" & tableNo & "') AND  CC.NameOnCard like '%" & CustomerName & "%' " & vbCrLf)
            ElseIf tableNo <> Nothing AndAlso MobileNo <> Nothing Then
                SqlQuery.Append("where RT.TableNo IN ('" & tableNo & "') AND CC.MobileNo='" & MobileNo & "' " & vbCrLf)
            ElseIf tableNo <> Nothing AndAlso FromDate <> Nothing Then
                SqlQuery.Append("where RT.TableNo IN ('" & tableNo & "') AND  RT.Date='" & FromDate.ToString("yyyy-MM-dd") & "' " & vbCrLf)
            ElseIf tableNo <> Nothing AndAlso Time <> Nothing Then
                SqlQuery.Append("where RT.TableNo IN ('" & tableNo & "') AND  Cast(RT.FromTime as Time) like '%" & Time.ToString("HH:mm") & "%' " & vbCrLf)
            ElseIf MobileNo <> Nothing AndAlso CustomerName <> Nothing Then
                SqlQuery.Append("where CC.MobileNo='" & MobileNo & "' AND  CC.NameOnCard like '%" & CustomerName & "%' " & vbCrLf)
            ElseIf FromDate <> Nothing AndAlso CustomerName <> Nothing Then
                SqlQuery.Append("where RT.Date='" & FromDate.ToString("yyyy-MM-dd") & "' AND  CC.NameOnCard like '%" & CustomerName & "%' " & vbCrLf)
            ElseIf Time <> Nothing AndAlso CustomerName <> Nothing Then
                SqlQuery.Append("where Cast(RT.FromTime as Time) like '%" & Time.ToString("HH:mm") & "%'  AND  CC.NameOnCard like '%" & CustomerName & "%' " & vbCrLf)
            ElseIf MobileNo <> Nothing AndAlso FromDate <> Nothing Then
                SqlQuery.Append("where CC.MobileNo='" & MobileNo & "' AND RT.Date='" & FromDate.ToString("yyyy-MM-dd") & "' " & vbCrLf)
            ElseIf MobileNo <> Nothing AndAlso Time <> Nothing Then
                SqlQuery.Append("where CC.MobileNo='" & MobileNo & "' AND Cast(RT.FromTime as Time) like '%" & Time.ToString("HH:mm") & "%' " & vbCrLf)
            ElseIf FromDate <> Nothing AndAlso Time <> Nothing Then
                SqlQuery.Append("where RT.Date='" & FromDate.ToString("yyyy-MM-dd") & "' AND  Cast(RT.FromTime as Time) like '%" & Time.ToString("HH:mm") & "%'" & vbCrLf)
            ElseIf tableNo <> Nothing Then
                SqlQuery.Append("where RT.TableNo IN ('" & tableNo & "')" & vbCrLf)
            ElseIf CustomerName <> Nothing Then
                SqlQuery.Append("where CC.NameOnCard like '%" & CustomerName & "%'" & vbCrLf)
            ElseIf MobileNo <> Nothing Then
                SqlQuery.Append("where CC.MobileNo='" & MobileNo & "'" & vbCrLf)
            ElseIf FromDate <> Nothing Then
                SqlQuery.Append("where RT.Date='" & FromDate.ToString("yyyy-MM-dd") & "'" & vbCrLf)
            ElseIf Time <> Nothing Then
                SqlQuery.Append("where Cast(RT.FromTime as Time) like '%" & Time.ToString("HH:mm") & "%'" & vbCrLf)
            Else
                SqlQuery.Append("where RT.Status=1 and RT.IsOccupied=0 and RT.Date='" & Now.ToString("yyyy-MM-dd") & "'" & vbCrLf)
                checkBoolean = True
            End If
            If checkBoolean = False Then
                SqlQuery.Append("AND RT.Status=1 and RT.IsOccupied=0 " & vbCrLf)
            End If
            SqlQuery.Append(" Group By TBL.TableNo,RT.ReservationId,CC.NameOnCard,tbl.NoOfPeople,CC.Mobileno,CC.CardNo,RT.Date,DATEADD(ms, -DATEPART(ms, Rt.FromTime),Rt. FromTime)" & vbCrLf)

            Dim dtData As New DataTable
            Dim cmd As New SqlCommand(SqlQuery.ToString, SpectrumCon())

            Dim daData As New SqlDataAdapter(cmd)
            daData.Fill(dtData)
            Return dtData

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Updating Reservation Date and Time
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <param name="NoOfPeople"></param>
    ''' <param name="TableNo"></param>
    ''' <param name="MobileNo"></param>
    ''' <param name="dDate"></param>
    ''' <param name="Time"></param>
    ''' <param name="OldTable"></param>
    ''' <param name="NewTable"></param>
    ''' <param name="UpdatedBy"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateReservationDetails(ByVal Name As String, ByVal NoOfPeople As String, ByVal TableNo As String, ByVal MobileNo As String, ByVal dDate As DateTime, ByVal Time As DateTime, ByVal OldTable As String, ByVal NewTable As String, ByVal UpdatedBy As String, ByRef tran As SqlTransaction) As Boolean
        Try
            UpdateReservationDetails = False
            Dim Query As String

            Query = "Update ReservationTbl set NoOfPeople= '" & NoOfPeople & "' ,Date='" & dDate.ToString("yyyy-MM-dd HH:mm:ss.fff") & "',Time= '" & Time.ToString("yyyy-MM-dd HH:mm:ss.fff") & "',TableNo='" & NewTable & "', "
            Query += "UpdatedOn='" & GetCurrentDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "'"
            Query += "where TableNo='" & OldTable & "'  "
            '' And SiteCode='" & Sitecode & "'
            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
            cmdTrn.Transaction = tran
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            UpdateReservationDetails = True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Updating Check In Status of Table
    ''' </summary>
    ''' <param name="ReservationId"></param>
    ''' <param name="Table"></param>
    ''' <param name="SiteCode"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateCheckIn(ByVal ReservationId As String, ByVal Table As List(Of Short), ByVal SiteCode As String, ByRef tran As SqlTransaction) As Boolean
        Try
            UpdateCheckIn = False
            Dim Query As String = ""

            For I = 0 To Table.Count - 1
                Query += "Update ReservationTbl set IsOccupied=1,UPDATEDON=getdate() " & _
                   "where TableNo='" & Table(I) & "' AND ReservationId='" & ReservationId & "' and Status=1" & _
                  "And SiteCode='" & SiteCode & "'; "
            Next
            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
            cmdTrn.Transaction = tran
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            UpdateCheckIn = True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function UpdateCheckInDetails(ByVal ReservationId As String, ByVal Table As List(Of Short), ByVal SiteCode As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If UpdateCheckIn(ReservationId, Table, SiteCode, tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            Return False
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function InsertCustomerDetails(ByVal Name As String, ByVal CardNumber As String, ByVal MobileNo As String, ByVal Sitecode As String, ByVal userid As String, ByVal ClpProgId As String, ByVal CardType As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If InsertCustomerEntry(Name, CardNumber, MobileNo, Sitecode, userid, ClpProgId, tran, CardType) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            Return False
            Throw New Exception(ex.Message)
        End Try
    End Function
    'added by khusrao adil on 1-02-2017 for host management guest details update
    Public Function UpdateCustomerDetails(ByVal Name As String, ByVal CardNumber As String, ByVal gender As String, ByVal MobileNo As String, ByVal Sitecode As String, ByVal userid As String, ByVal ClpProgId As String, ByVal CardType As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If UpdateCustomerEntry(Name, CardNumber, gender, MobileNo, Sitecode, userid, ClpProgId, tran, CardType) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If
            tran.Commit()
            CloseConnection()
            Return True
            CloseConnection()
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            Return False
            Throw New Exception(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Cancel the Reserved Tables
    ''' </summary>
    ''' <param name="ReservationId"></param>
    ''' <param name="SiteCode"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateCancelReservation(ByVal ReservationId As List(Of String), ByVal SiteCode As String, ByRef tran As SqlTransaction) As Boolean
        Try
            UpdateCancelReservation = False
            Dim Query As String = ""

            For I = 0 To ReservationId.Count - 1
                Query += "Update ReservationTbl set Status=0,IsOccupied=0,UPDATEDON=getdate() " & _
                   "where ReservationId='" & ReservationId(I) & "'" & _
                  "And SiteCode='" & SiteCode & "'; "
            Next
            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
            cmdTrn.Transaction = tran
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            UpdateCancelReservation = True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function UpdateCancelReservationDetails(ByVal ReservationId As List(Of String), ByVal SiteCode As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If UpdateCancelReservation(ReservationId, SiteCode, tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            Return False
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function UpdateReservationTime(ByVal ReservationId As String, ByVal FromTime As DateTime, ByVal SiteCode As String, ByRef tran As SqlTransaction) As Boolean
        Try
            UpdateReservationTime = False
            Dim Query As String = ""


            Query += "Update ReservationTbl set FromTime='" & FromTime.ToString("yyyy-MM-dd HH:mm:ss.fff") & "',UPDATEDON=getdate() " & _
               "where ReservationId='" & ReservationId & "' " & _
              "And SiteCode='" & SiteCode & "'; "

            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
            cmdTrn.Transaction = tran
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            UpdateReservationTime = True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function UpdateReservationTimeDetails(ByVal ReservationId As String, ByVal FromTime As DateTime, ByVal SiteCode As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If UpdateReservationTime(ReservationId, FromTime, SiteCode, tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            Return False
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class
