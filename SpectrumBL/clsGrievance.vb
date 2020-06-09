Imports System.Data
Imports System.Data.SqlClient
Imports SpectrumBL
Imports System.Text

Public Class clsGrievance
    Inherits clsCommon

    Public _GrievanceRemarkIdForAttachement As String
    Public Property GrievanceRemarkIdForAttachement() As String
        Get
            Return _GrievanceRemarkIdForAttachement
        End Get
        Set(ByVal value As String)
            _GrievanceRemarkIdForAttachement = value
        End Set
    End Property

    ' Dim GrievanceRemarkIdForAttachement As String = ""
    Public Function SaveGrivanceDetail(ByVal Sitecode As String, ByVal SiteStdCode As String, ByVal Grievanceid As String, ByVal status As String, ByVal grievanceTypeid As Nullable(Of Integer), _
                                       ByVal departmentid As Integer, ByVal title As String, ByVal details As String,
                                       ByVal userid As String, ByVal EditMode As Boolean, ByVal Remarks As String, ByVal strFinyear As String, ByVal isGrievanceHistoryChange As Boolean,
                                       ByVal GrievanceHistoryText As String,
                                       ByVal RaisedFromSite As String, ByVal IsRaisedFromSite As Boolean, ByVal RaisedToDepartment As Integer,
                                       ByVal IsRaisedToSite As Boolean, ByVal CCSite As String, ByVal CCDepartment As String, ByVal IsCCSite As Boolean, ByVal AssignedSiteCode As String,
                                        Optional ByVal dayopendate As Date = Nothing, Optional ByVal RaisedBy As String = "", Optional ByVal doEntryForSms As Boolean = False, Optional ByVal fileName As String = "", Optional ByVal MoblileList As String = "", Optional ByVal IsReopenHistoryEntery As Boolean = False,
                                        Optional ByVal IsBoldApplicable As Boolean = False,
                                        Optional ByVal dtMobileNumbers As DataTable = Nothing) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            'Dim strFinyear As String = GetFinancialYear(dayopendate, Sitecode)
            'Dim docno As String = getDocumentNo("GRIEVANCE Remark", Sitecode)
            'GrievanceRemarkId = GenDocNo("GCR" & Sitecode.Substring(Sitecode.Length - 3, 3) & strFinyear.Substring(strFinyear.Length - 2, 2), 15, docno)
            Dim grievanceSideCode As String = String.Empty
            Dim SiteName As String = String.Empty
            ' Dim dtMobileNumbers As DataTable

            'Dim strFinyear As String = GetFinancialYear(dayopendate, Sitecode)
            If doEntryForSms = True Then
                If RaisedBy = "Bo" Then
                    grievanceSideCode = "CCE"
                Else
                    grievanceSideCode = Sitecode
                End If
                SiteName = GetSiteName(Sitecode)
                'If status = "Resolved" Then
                '    dtMobileNumbers = GetMobileNumberBySiteCode(Sitecode)
                'Else
                '    dtMobileNumbers = GetDepartmentMobileNumberByDepartmentId(departmentid)
                'End If
                'dtMobileNumbers = GetDepartmentMobileNumberByDepartmentId(departmentid)
            End If
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If Not EditMode Then

                Dim docno As String = getDocumentNo("Grievance", Sitecode)
                ' docno = GenDocNo("GCF" & Sitecode.Substring(Sitecode.Length - 3, 3), 14, docno)
                'Grievanceid = docno
                ' Grievanceid = GenDocNo("GCF" & Sitecode.Substring(Sitecode.Length - 3, 3) & strFinyear.Substring(strFinyear.Length - 2, 2), 14, docno)
                docno = GenDocNo("GC" & SiteStdCode & strFinyear.Substring(strFinyear.Length - 2, 2), 13, docno)
                Grievanceid = docno
            End If

            If EditMode Then
                If UpdateGrievanceDetails(Sitecode, Grievanceid, status, grievanceTypeid, departmentid, tran, Remarks, RaisedBy, userid, RaisedToDepartment, CCSite, CCDepartment, AssignedSiteCode, IsRaisedToSite:=IsRaisedToSite, IsCCSite:=IsCCSite) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                ElseIf doEntryForSms = True AndAlso SaveGrievanceSMSDetails(Sitecode, SiteName, grievanceSideCode, Grievanceid, userid, tran, status, dtMobileNumbers, strFinyear) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                    'ByVal Sitecode As String, ByVal id As String, ByVal GrievanceSiteCode As String, ByVal GrievanceId As String, 
                    'ByVal HistoryText As String, ByVal userid As String, ByRef tran As SqlTransaction, ByVal Status As String
                    'HistorySiteCode,HistoryId,GrievanceSiteCode,GrievanceId,HistoryText,CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON,Status

                ElseIf isGrievanceHistoryChange Then
                    Dim docnoForGrievanceHistory = getDocumentNo("GrievanceHistory", Sitecode)
                    Dim GrievanceHistoryId = GenDocNo("GCH" & Sitecode.Substring(Sitecode.Length - 3, 3) & strFinyear.Substring(strFinyear.Length - 2, 2), 15, docnoForGrievanceHistory)
                    If InsertGrievanceHistoryDetails(Sitecode, GrievanceHistoryId, Sitecode, Grievanceid, GrievanceHistoryText, userid, tran, RaisedBy) = False Then
                        tran.Rollback()
                        CloseConnection()
                        Return False
                    End If
                    If UpdateDocumentNo("GrievanceHistory", SpectrumCon, tran) = False Then
                        tran.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If
                If IsReopenHistoryEntery = True Then
                    Dim docnoForGrievanceReopenHistory = getDocumentNo("GrievanceReopenHistory", Sitecode)
                    Dim GrievanceReopenHistoryId = GenDocNo("GRH" & Sitecode.Substring(Sitecode.Length - 3, 3) & strFinyear.Substring(strFinyear.Length - 2, 2), 15, docnoForGrievanceReopenHistory)
                    If InsertGrievanceReopenHistoryDetails(Sitecode, GrievanceReopenHistoryId, Grievanceid, userid, tran) = False Then
                        tran.Rollback()
                        CloseConnection()
                        Return False
                    End If
                    If UpdateDocumentNo("GrievanceReopenHistory", SpectrumCon, tran) = False Then
                        tran.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If
            Else
                If InsertGrievanceDetails(Sitecode, Grievanceid, grievanceTypeid, departmentid, title, details, userid, tran, Remarks:=String.Empty, MobileList:=MoblileList,
                                          RaisedFromSite:=RaisedFromSite, IsRaisedFromSite:=IsRaisedFromSite, RaisedToDepartment:=RaisedToDepartment,
                                          IsRaisedToSite:=IsRaisedToSite, CCSite:=CCSite, CCDepartment:=CCDepartment, IsCCSite:=IsCCSite, AssignedSiteCode:=AssignedSiteCode) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
                If UpdateDocumentNo("Grievance", SpectrumCon, tran) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                ElseIf doEntryForSms = True AndAlso SaveGrievanceSMSDetails(Sitecode, SiteName, grievanceSideCode, Grievanceid, userid, tran, status, dtMobileNumbers, strFinyear) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
            End If

            If Not String.IsNullOrEmpty(Remarks) Then
                If Not SaveTicketRemark(Sitecode, Grievanceid, userid, Remarks, strFinyear, dayopendate, tran) Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
                If fileName <> String.Empty Then
                    If Not SaveTicketRemarkAttachment(Sitecode, userid, fileName, Grievanceid, tran) Then
                        tran.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If
            End If
            'Added by sagar to update Isread Status (Not String.IsNullOrEmpty(Remarks)) Or status = "New"
            If IsBoldApplicable Then
                If Not UpdateReadStatus(Sitecode, userid, Grievanceid, True, tran) Then
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
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function SaveTicketRemark(ByVal Sitecode As String, ByVal grievanceId As String, _
                                     ByVal userid As String, ByVal Remarks As String, ByVal strFinyear As String, Optional ByVal dayopendate As Date = Nothing, Optional ByRef tran As SqlTransaction = Nothing) As Boolean

        Try
            OpenConnection()
            Dim GrievanceRemarkId As String = String.Empty
            ' Dim strFinyear As String = GetFinancialYear(dayopendate, Sitecode)
            Dim docno As String = getDocumentNo("GRIEVANCE Remark", Sitecode)
            GrievanceRemarkId = GenDocNo("GCR" & Sitecode.Substring(Sitecode.Length - 3, 3) & strFinyear.Substring(strFinyear.Length - 2, 2), 15, docno)


            Dim Query As String
            Query = " INSERT INTO GrievanceRemarkDetails(RemarksSiteCode,RemarkId,GrievanceSiteCode,GrievanceId, Remark, " & _
                    " CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON,status) " & _
                    " VALUES     ('" & Sitecode & "','" & GrievanceRemarkId & "','" & Sitecode & "','" & grievanceId & "','" & Replace(Remarks, "'", "''") & _
                    "','" & Sitecode & "','" & userid & "',GETDATE(),'" & Sitecode & "','" & userid & "',GETDATE(),1)"

            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
            cmdTrn.Transaction = tran
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            GrievanceRemarkIdForAttachement = GrievanceRemarkId
            If UpdateDocumentNo("GRIEVANCE Remark", SpectrumCon, tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If

            Return True

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Dim attachmentFileName As String
    Public Function SaveTicketRemarkAttachment(ByVal Sitecode As String, ByVal userid As String, ByVal FileName As String, ByVal GrievanceId As String, ByRef tran As SqlTransaction)
        Try
            OpenConnection()
            ' attachmentFileName = FileName
            ' GrievanceRemarkId = GenDocNo("GCR" & Sitecode.Substring(Sitecode.Length - 3, 3))
            Dim Query = "Insert into GrievanceAttachments(GrievanceAttachmentsSiteCode,RemarkId,GrievanceSiteCode,GrievanceId,FileName,CreatedOn,CreatedAt,CreatedBy,UpdatedOn,UpdatedAt,UpdatedBy,Status)" & _
                   " VALUES('" & Sitecode & "','" & GrievanceRemarkIdForAttachement & "', '" & Sitecode & " ','" & GrievanceId & "','" & FileName & "'," & _
                   "GETDATE(),'" & Sitecode & "','" & userid & "',GETDATE(),'" & Sitecode & "','" & userid & "',1)"

            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
            cmdTrn.Transaction = tran
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            Return True

        Catch ex As Exception
            LogException(ex)
            CloseConnection()
            Return False
        End Try
    End Function

    Public Function getGrievanceFileName(ByVal RemarkId As String) As String

        Try
            Dim dataTable As DataTable
            Dim query = "select FileName from GrievanceAttachments where RemarkId = '" & RemarkId & "'"
            dataTable = GetFilledTable(query)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                Return dataTable.Rows(0)(0)
            End If
            Return String.Empty
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try

    End Function
    Public Function InsertGrievanceReopenHistoryDetails(ByVal Sitecode As String, ByVal ReOpenHistoryId As String, ByVal id As String, ByVal userid As String, ByRef tran As SqlTransaction) As Boolean
        Try
            InsertGrievanceReopenHistoryDetails = False
            Dim Query As String
            Query = "Insert into GrievanceReopenHistoryDetails(SiteCode,ReopenHistoryId,GrievanceSiteCode,GrievanceId,GrievanceStatus," & _
                      "CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON,status)" & _
                      "Values('" & Sitecode & "','" & ReOpenHistoryId & "','" & Sitecode & "','" & id & "','Re-opened','" & Sitecode & "','" & userid & "',GETDATE(),'" & Sitecode & "','" & userid & "',GETDATE(),1)"
            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
            cmdTrn.Transaction = tran
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            InsertGrievanceReopenHistoryDetails = True


        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function InsertGrievanceDetails(ByVal Sitecode As String, ByVal id As String, ByVal grievanceTypeid As Nullable(Of Integer), ByVal departmentid As Integer, ByVal title As String, ByVal details As String, ByVal userid As String, ByRef tran As SqlTransaction, ByVal Remarks As String, ByVal MobileList As String,
                                          ByVal RaisedFromSite As String, ByVal IsRaisedFromSite As Boolean, ByVal RaisedToDepartment As Integer,
                                       ByVal IsRaisedToSite As Boolean, ByVal CCSite As String, ByVal CCDepartment As String, ByVal IsCCSite As Boolean, ByVal AssignedSiteCode As String) As Boolean
        Try
            InsertGrievanceDetails = False
            Dim Query As String

            Query = " INSERT INTO GrievanceDetails(SiteCode,GrievanceId,GrievanceTypeId, DeptId, Title, GrievanceDesc, GrievanceStatus, " & _
                    " CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON,status,Remark,AssignedSiteCode,MobileList,IsViewed," & _
                     "RaisedFromSite, IsRaisedFromSite, RaisedToDepartment, IsRaisedToSite, CCSite, CCDepartment, IsCCSite )" & _
                    " VALUES     ('" & Sitecode & "','" & id & "'," & IIf(grievanceTypeid Is Nothing, "NULL", grievanceTypeid) & ", Case When " & departmentid & " =0 Then  NULL Else " & departmentid & " END,'" & title & "','" & details & "','New'" & _
                    ",'" & Sitecode & "','" & userid & "',GETDATE(),'" & Sitecode & "','" & userid & "',GETDATE(),1,'" & Remarks & "', Case When '" & AssignedSiteCode & "' = '' Then NULL ELSE '" & AssignedSiteCode & "' END  ,'" & MobileList & "',1 " & _
                    " ,'" & RaisedFromSite & "', '" & IsRaisedFromSite & "', Case When'" & RaisedToDepartment & "' =0 Then NULL Else '" & RaisedToDepartment & "' END , '" & IsRaisedToSite & "', Case When '" & CCSite & "' = '' Then NULL ELSE '" & CCSite & "' END , Case WHEN '" & CCDepartment & "' = '' Then NULL ELSE '" & CCDepartment & "' END , '" & IsCCSite & "')"

            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
            cmdTrn.Transaction = tran
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            InsertGrievanceDetails = True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function UpdateGrievanceDetails(ByVal Sitecode As String, ByVal id As String, ByVal status As String, ByVal grievanceTypeid As Nullable(Of Integer), ByVal departmentid As Integer, ByRef tran As SqlTransaction, ByVal Remarks As String, Optional ByVal RaisedBy As String = "", Optional ByVal UpdatedBy As String = "", Optional ByVal RaisedToDepartment As Integer = 0, Optional ByVal CCSite As String = "", Optional ByVal CCDepartment As String = "", Optional ByVal AssignedSiteCode As String = "", Optional ByVal IsRaisedToSite As Boolean = False, Optional ByVal IsCCSite As Boolean = False) As Boolean
        Try
            UpdateGrievanceDetails = False
            Dim Query As String

            Query = "Update GrievanceDetails set GrievanceStatus= '" & status & "',GrievanceTypeId= " & IIf(grievanceTypeid Is Nothing, "NULL", grievanceTypeid) & ",UPDATEDON=getdate(),UPDATEDAT='" & Sitecode & "' , UPDATEDBY='" & UpdatedBy & "',RaisedToDepartment= CASE WHEN '" & RaisedToDepartment & "'='0' Then NULL ELSE '" & RaisedToDepartment & "' END , CCSite = CASE WHEN '" & CCSite & "'='' Then NULL ELSE '" & CCSite & "' END  ,  CCDepartment = CASE WHEN '" & CCDepartment & "'='' THEN NULL ELSE '" & CCDepartment & "' END , AssignedSiteCode = CASE WHEN '" & AssignedSiteCode & "' = '' THEN NULL ELSE '" & AssignedSiteCode & "' END ,IsRaisedToSite = '" & IsRaisedToSite & "',IsCCSite = '" & IsCCSite & "'"


            ''Remark= '" & Remarks & "' " & _

            'If status = "Resolved" Then
            '    Query += ",IsRated=0 "
            'Else
            If RaisedBy = "Bo" Then
                Sitecode = "CCE"
            End If
            ''AssignedSiteCode='CCE'

            If status <> "Resolved" Then
                Query += ",IsRated=0 "
            End If
            Query += "where GrievanceId='" & id & "' And SiteCode='" & Sitecode & "' "

            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
            cmdTrn.Transaction = tran
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            UpdateGrievanceDetails = True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    'code added for jk sprint 24 by vipul
    Public Function UpdateGrievanceDetailsONFeedBack(ByVal siteCode As String, ByVal Id As String, ByVal Status As String, ByVal grievanceTypeid As Nullable(Of Integer), _
                                                    ByVal DepartmentId As Integer, ByVal remark As String, ByVal RaisedToDepartment As Integer, ByVal UpdatedBy As String,
                                                    ByVal userId As String, ByVal dtMobileNumbers As DataTable, ByVal FinYear As String, Optional RaisedBy As String = "", Optional doEntryForSms As Boolean = False) As Boolean
        Try
            Dim grievanceSideCode As String = siteCode
            If RaisedBy = "Bo" Then
                grievanceSideCode = "CCE"
            Else
                grievanceSideCode = siteCode
            End If
            Dim SiteName = GetSiteName(siteCode)
            Dim tran As SqlTransaction = Nothing
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If UpdateGrievanceDetails(siteCode, Id, Status, grievanceTypeid, DepartmentId, tran, remark, , UpdatedBy:=UpdatedBy, RaisedToDepartment:=RaisedToDepartment) = False Then
                tran.Rollback()
                CloseConnection()
            ElseIf doEntryForSms = True AndAlso SaveGrievanceSMSDetails(siteCode, SiteName, grievanceSideCode, Id, userId, tran, Status, dtMobileNumbers, FinYear) = False Then
                tran.Rollback()
                CloseConnection()
            Else
                tran.Commit()
                CloseConnection()
                Return False
            End If
            Return True
        Catch ex As Exception
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' saving Grievance history when deparmtent get changed
    ''' </summary>
    ''' <param name="Sitecode"></param>
    ''' <param name="id"></param>
    ''' <param name="GrievanceSiteCode"></param>
    ''' <param name="GrievanceId"></param>
    ''' <param name="HistoryText"></param>
    ''' <param name="userid"></param>
    ''' <param name="tran"></param>
    ''' <param name="RaisedBy"></param>
    ''' <returns></returns>
    Public Function InsertGrievanceHistoryDetails(ByVal Sitecode As String, ByVal id As String, ByVal GrievanceSiteCode As String, ByVal GrievanceId As String, ByVal HistoryText As String, ByVal userid As String, ByRef tran As SqlTransaction, ByVal RaisedBy As String) As Boolean
        Try
            InsertGrievanceHistoryDetails = False
            If RaisedBy = "Bo" Then
                GrievanceSiteCode = "CCE"
            End If

            Dim Query As String

            Query = "insert into GrievanceHistoryDetails(HistorySiteCode,HistoryId,GrievanceSiteCode,GrievanceId,HistoryText," & _
                                                            "CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON,Status)" & _
                                                            "Values ('" & Sitecode & "','" & id & "','" & GrievanceSiteCode & "','" & GrievanceId & "','" & HistoryText & "'," & _
                                                            " '" & Sitecode & "','" & userid & "',GETDATE(),'" & Sitecode & "','" & userid & "',GETDATE(),1)"
            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
            cmdTrn.Transaction = tran
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            InsertGrievanceHistoryDetails = True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function DeleteGrivanceDetail(ByVal Sitecode As String, ByVal id As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If DeleteDetails(Sitecode, id, tran) = False Then
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

    ''' <summary>
    ''' get grievenace history of department
    ''' </summary>
    ''' <param name="GrievanceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGrievanceHistory(ByVal GrievanceId As String) As DataTable

        Try
            Dim strQuery = "Select HistoryText as History FROM GrievanceHistoryDetails  where GrievanceId='" & GrievanceId & "' and status=1 order by CreatedOn asc"
            Dim da As New SqlDataAdapter(strQuery, ConString)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function DeleteDetails(ByVal Sitecode As String, ByVal id As String, ByRef tran As SqlTransaction) As Boolean
        Try
            DeleteDetails = False
            Dim Query As String

            Query = "Update GrievanceDetails set Status=0,UPDATEDON=getdate() " & _
                    "where GrievanceId='" & id & "' And SiteCode='" & Sitecode & "' "

            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
            cmdTrn.Transaction = tran
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            DeleteDetails = True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function


    Public Function GetGrievanceRemarks(ByVal id As String, Optional ByVal OrderBy As String = "Asc") As DataTable
        Try
            Dim StrQuery As String = "select RemarkId, Remark,RemarksSiteCode,Createdon AS CreateTime,CREATEDBY as UserName from  GrievanceRemarkDetails where GrievanceId ='" & id & "' and Status= '1' " & _
                                    "order by CREATEDON " & OrderBy & ""

            Dim da As New SqlDataAdapter(StrQuery, ConString)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' sms content saving while ticket raise
    ''' </summary>
    ''' <param name="smsSiteCode"></param>
    ''' <param name="smsId"></param>
    ''' <param name="grievanceSiteCode"></param>
    ''' <param name="grievanceId"></param>
    ''' <param name="smsText"></param>
    ''' <param name="mobileNo"></param>
    ''' <param name="userId"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertGrievanceSMSDetails(ByVal smsSiteCode As String, ByVal smsId As String, ByVal grievanceSiteCode As String, ByVal grievanceId As String, ByVal GrievanceStatus As String, ByVal smsText As String, ByVal mobileNo As String, ByVal userId As String, ByRef tran As SqlTransaction) As Integer
        Try
            InsertGrievanceSMSDetails = False
            Dim Query As String
            Dim SmsgrievanceSiteCode As String = grievanceSiteCode
            Query = "Insert into GrievanceSmsDetails(SmsSiteCode,smsId,GrievanceSiteCode,GrievanceId,GrievanceStatus,SmsText,Send,status,MobileNo,RetryCount,CreatedOn,CreatedAt,CreatedBy,UpdatedOn,UpdatedAt,UpdatedBy)" & _
                    " VALUES('" & smsSiteCode & "','" & smsId & "','" & SmsgrievanceSiteCode & "','" & grievanceId & "','" & GrievanceStatus & "','" & smsText & "',0,1,'" & mobileNo & "','0'" & _
                    ",GETDATE(),'" & smsSiteCode & "','" & userId & "',GETDATE(),'" & smsSiteCode & "','" & userId & "')"

            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
            cmdTrn.Transaction = tran
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            InsertGrievanceSMSDetails = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    ''' <summary>
    ''' save grievance sms detail base on status either new or resolved
    ''' 
    ''' </summary>
    ''' <param name="Sitecode"></param>
    ''' <param name="SiteName"></param>
    ''' <param name="grievanceSideCode"></param>
    ''' <param name="grievanceId"></param>
    ''' <param name="userId"></param>
    ''' <param name="tran"></param>
    ''' <param name="Status"></param>
    ''' <param name="dtMobileNumbers"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveGrievanceSMSDetails(ByVal Sitecode As String, ByVal SiteName As String, ByVal grievanceSideCode As String, ByVal grievanceId As String, ByVal userId As String, ByRef tran As SqlTransaction, ByVal Status As String, ByRef dtMobileNumbers As DataTable, ByVal strFinyear As String) As Boolean
        Try
            SaveGrievanceSMSDetails = False
            Dim mobileNo As String = ""
            Dim smstext As String = ""
            If Status = "New" Then
                smstext = "New Ticket has been raised for you with Ticket id " & grievanceId & " from " & SiteName & ""
            ElseIf Status = "Resolved" Then
                smstext = "Ticket id " & grievanceId & " has been resolved by concern authority.Kindly rate resolved ticket with rating 1 to 5 with 5 being highest to 1 being poor."
            ElseIf Status = "Re-opened" Then
                smstext = "Ticket id " & grievanceId & " has been reopened. kindly go to Ticketing system to resolve ticket."
                ''Ticket Id [tktid] has been reopened. kindly go to Ticketing system to resolve ticket
            End If
            If SiteName = "" Then
                SiteName = Sitecode
            End If
            If dtMobileNumbers.Rows.Count >= 0 Then
                For MobileRow = 0 To dtMobileNumbers.Rows.Count - 1
                    Dim docno As String = getDocumentNo("GrievanceSms", Sitecode, SpectrumCon, tran, "FO_DOC")
                    Dim smsId As String = GenDocNo("GCS" & Sitecode.Substring(Sitecode.Length - 3, 3) & strFinyear.Substring(strFinyear.Length - 2, 2), 15, docno)
                    'Dim smsId As String = GenDocNo("GCS" & Sitecode.Substring(Sitecode.Length - 3, 3), 15, docno)
                    mobileNo = dtMobileNumbers.Rows(MobileRow)("MobileNo")
                    If InsertGrievanceSMSDetails(Sitecode, smsId, grievanceSideCode, grievanceId, Status, smstext, mobileNo, userId, tran) = False Then
                        SaveGrievanceSMSDetails = False
                    Else
                        If UpdateDocumentNo("GrievanceSms", SpectrumCon, tran, "", "FO_DOC") = False Then
                            SaveGrievanceSMSDetails = False
                        End If
                    End If
                Next
                SaveGrievanceSMSDetails = True
            End If
        Catch ex As Exception
            LogException(ex)
            SaveGrievanceSMSDetails = False
        End Try
    End Function
    Public Function SaveFeedBack(ByVal Sitecode As String, ByVal dt As DataTable, ByVal userid As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Dim sqlQuery As New StringBuilder
        Try
            SaveFeedBack = False
            If SpectrumCon.State = ConnectionState.Closed Then
                OpenConnection()
            End If
            tran = SpectrumCon.BeginTransaction()
            For Each rows In dt.Rows
                sqlQuery.Length = 0
                Dim _coment As String = rows("Comment")
                _coment = _coment.Trim()
                _coment = _coment.Replace("'", "")
                sqlQuery.Append("INSERT INTO GrievanceFeedbackdtl" & _
                        " (SiteCode, GrievanceId, Comment, Rating, CreatedAt, CreatedBy, CreatedOn, UpdatedAt, UpdatedBy, UpdatedOn, Status)" & _
                       " VALUES('" & Sitecode & "','" & rows("TicketId") & "' , '" & _coment & "','" & rows("Rating") & "' ,'" & Sitecode & "' ,'" & userid & "' ,GETDATE() ,'" & Sitecode & "' , '" & userid & "' ,GETDATE() ,1 );")

                sqlQuery.Append(" UPDATE GrievanceDetails set IsRated = 1 , UpdatedOn=GetDate() where GrievanceId='" & rows("TicketId") & "'and Status=1")

                Dim cmd As New SqlCommand(sqlQuery.ToString(), SpectrumCon, tran)
                cmd.ExecuteNonQuery()

            Next
            tran.Commit()
            SpectrumCon.Close()
            SaveFeedBack = True
            Return SaveFeedBack
        Catch ex As Exception
            tran.Rollback()
            SpectrumCon.Close()
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function UpdateReadStatusForTicket(ByVal Sitecode As String, ByVal UserId As String, ByVal id As String, ByVal IsRead As Boolean) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If UpdateReadStatus(Sitecode, UserId, id, IsRead, tran) = False Then
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
        End Try
    End Function
    Public Function UpdateReadStatus(ByVal Sitecode As String, ByVal UserId As String, ByVal id As String, ByVal IsRead As Boolean, ByRef tran As SqlTransaction) As Boolean
        Try
            UpdateReadStatus = False
            Dim Query As String

            Query = "Update GrievanceDetails set BoldBy=(select Isnull(BoldBy,'') from GrievanceDetails where GrievanceId='" & id & "') + '," & UserId & "',IsBold='" & IsRead & "',UPdatedBy='" & UserId & "'"
            If IsRead Then
                Query += ",UPDATEDON=getdate()"
            End If
            Query += "where GrievanceId='" & id & "'" ' And SiteCode='" & Sitecode & "'"

            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
            cmdTrn.Transaction = tran
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            UpdateReadStatus = True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    'code added for jk sprint 24 by vipul
    Public Function Togetsite_cmfid(ByVal selectedCCDeparmentId As String) As DataTable
        Try
            Dim dt As DataTable
            Dim strString As String = "select ISNULL(SQFTArea,0) as SQFTArea from mstsite  WHERE   SiteCode IN ('" + selectedCCDeparmentId + "')"
            Dim cmdTrn As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmdTrn)
            dt = New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


End Class
