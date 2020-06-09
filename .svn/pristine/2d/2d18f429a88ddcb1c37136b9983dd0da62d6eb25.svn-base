Imports System.Data
Imports System.Data.SqlClient
Imports SpectrumBL
Imports System.Text
Public Class clsShift
    Inherits clsCommon
    Shared DTFloatingDetail As New DataTable

    Public Enum ShiftFinReport
        NetCashData
        SummaryData
        DiscountData
        NetSaleData
    End Enum

    Public Function GetShiftFinReportData(ByVal SiteCode As String, ByVal DayCloseDate As Date, ByVal TerminalId As String, ByVal ShiftId As Integer) As DataSet
        Dim dsDayClose As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("UDP_GetPCShiftFinReport", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@V_SiteCode", SiteCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@V_DayCloseDate", DayCloseDate.Date)
            sqlComm.Parameters.AddWithValue("@V_TerminalId", TerminalId)
            sqlComm.Parameters.AddWithValue("@V_ShiftId", ShiftId)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsDayClose = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsDayClose)
            Return dsDayClose
        Catch ex As Exception
            LogException(ex)
            Return dsDayClose
        End Try
    End Function

    Public Function SaveTill(ByVal DtTill As DataTable, ByVal Type As String, ByVal SiteCode As String, ByVal TerminalID As String, ByVal CurrencyCode As String, ByVal UserId As String) As Boolean
        Try
            Dim Tran As SqlTransaction

            DtTill.TableName = "FLOATINGDETAIL"
            AddColumnToDataTable(DtTill, "SiteCode", "System.String", SiteCode)
            AddColumnToDataTable(DtTill, "TerminalId", "System.String", TerminalID)
            AddColumnToDataTable(DtTill, "CurrencyCode", "System.String", CurrencyCode)
            AddColumnToDataTable(DtTill, "Action", "System.String", Type)
            AddColumnToDataTable(DtTill, "FlotDateTime", "System.DateTime", Now)
            AddColumnToDataTable(DtTill, "CREATEDON", "System.DateTime", Now)
            AddColumnToDataTable(DtTill, "CREATEDAT", "System.String", SiteCode)
            AddColumnToDataTable(DtTill, "CREATEDBY", "System.String", UserId)
            AddColumnToDataTable(DtTill, "Status", "System.Boolean", True)
            DeleteColumnFromDataTable(DtTill, "DENOMINATION")
            DtTill.Columns("Amount").ColumnName = "TotalAmount"
            Dim dv As New DataView(DtTill, "Qty>0", "", DataViewRowState.CurrentRows)
            Dim dtTemp As DataTable = dv.ToTable()
            AddMode(dtTemp)
            OpenConnection()
            Tran = SpectrumCon.BeginTransaction()
            If SaveData(dtTemp, SpectrumCon, Tran) = True Then
                Tran.Commit()
                Return True
            End If
            Tran.Rollback()
            Return False
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try

    End Function
    Public Function RemoveData(ByVal terminal As String, ByVal SiteCode As String, ByVal CurrentDate As DateTime, ByVal ShiftId As Integer) As Boolean
        Try
            ActivityLogForShift(Nothing, String.Format("Edit Case -RemoveData terminal : {0},SiteCode : {1},CurrentDate : {2},ShiftId : {3}", terminal, SiteCode, CurrentDate, ShiftId), ShiftId)
            Dim cmdTrn As New SqlCommand("Delete from FLOATINGDETAIL   WHERE Status=1 and SITECODE='" & SiteCode & "' and ShiftId=" & ShiftId & " AND TERMINALID='" & terminal & "' and (CONVERT(Datetime,CONVERT(VARCHAR(10), FlotDatetime, 101)) =Convert(dateTime,'" & CurrentDate.ToString("MM/dd/yyyy") & "'))", SpectrumCon)
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            CloseConnection()
            RemoveData = True
        Catch ex As Exception
            LogException(ex)
            RemoveData = False
        End Try
    End Function
    Public Function CheckOpening(ByVal Terminal As String, ByVal SiteCode As String, ByVal OpenClose As String) As Boolean
        Try
            Dim DATemp As New SqlDataAdapter("SELECT TERMINALID FROM MSTTERMINALID " & _
            "WHERE SITECODE='" & SiteCode & "' AND TERMINALID='" & Terminal & "' And OpenCloseSTATUS in ('" & OpenClose & "')", SpectrumCon)
            OpenConnection()
            DTFloatingDetail = New DataTable
            DATemp.Fill(DTFloatingDetail)
            If DTFloatingDetail.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function
    Public Function CheckIsCashDrawer(ByVal Terminal As String, ByVal SiteCode As String) As Boolean
        Try
            CheckIsCashDrawer = False
            Dim cmd As New SqlCommand("SELECT ISNULL(IsCashDrawer,0)  FROM MSTTERMINALID " & _
            "WHERE SITECODE='" & SiteCode & "' AND TERMINALID='" & Terminal & "'", SpectrumCon)
            OpenConnection()

            If cmd.ExecuteScalar() = True Then
                CheckIsCashDrawer = True
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function CheckDayOpening(ByVal SiteCode As String) As Boolean
        Try
            Dim DATemp As New SqlDataAdapter("Select OpenDate from dayopennclose where daycloseStatus=0 ANd Status=1  And sitecode='" & SiteCode & "' ", SpectrumCon)
            OpenConnection()
            DTFloatingDetail = New DataTable
            DATemp.Fill(DTFloatingDetail)
            If DTFloatingDetail.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function
    Public Function CheckClosed(ByVal Terminal As String, ByVal SiteCode As String, ByVal OpenClose As String, Optional ByVal FirstOne As Boolean = False) As Boolean
        Try
            Dim Query As String = " SELECT count(*) FROM TillCloseDtl " & _
            "WHERE SITECODE='" & SiteCode & "' AND TERMINALID='" & Terminal & "' "
            If FirstOne = False Then
                Query = Query & " And TillDate=(select Opendate from DayopenNclose where status=1 and dayclosestatus=0 and SiteCode = '" & SiteCode & "') "
            End If
            Dim DATemp As New SqlDataAdapter(Query, SpectrumCon)
            OpenConnection()
            DTFloatingDetail = New DataTable
            DATemp.Fill(DTFloatingDetail)
            If DTFloatingDetail.Rows.Count > 0 Then
                If CDbl(DTFloatingDetail.Rows(0)(0).ToString()) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If


        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function GetDatesToCloseForTillOpen(ByVal Terminal As String, ByVal SiteCode As String) As Date
        Try
            Dim Query As String = " SELECT Min(TillDate) FROM TillCloseDtl " & _
            "WHERE SITECODE='" & SiteCode & "' AND TERMINALID='" & Terminal & "' "
            Query = Query & " And TillDate=(select Opendate from DayopenNclose where status=1 and dayclosestatus=0) "
            Dim tillCloseDetails As DataTable
            tillCloseDetails = GetFilledTable(Query)
            If Not tillCloseDetails Is Nothing AndAlso tillCloseDetails.Rows.Count > 0 Then
                Return DirectCast(tillCloseDetails.Rows(0)(0), DateTime)
            Else
                Return DateTime.MinValue
            End If
        Catch ex As Exception
            LogException(ex)
            Return DateTime.MinValue
        End Try
    End Function

    Public Function CLoseDayForTillOpen(ByVal siteCode As String, ByVal finYear As String, ByVal dayOpenDate As DateTime, ByVal userId As String) As Boolean
        Try
            Dim query As String = " update DayOpenNClose set DayCloseStatus = 1, closedAt = 'FO', " & _
                                    "UPDATEDAT='" & siteCode & "', UPDATEDBY = '" & userId & "', UPDATEDON = '" & GetCurrentDate() & "' " & _
                                    " where SiteCode = '" & siteCode & "' and FinYear = '" & finYear & "' and OpenDate = '" & dayOpenDate & "' "
            UpdateDayOpenData(query)
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function UpdateDayOpenData(ByVal Query As String) As Boolean
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

    Public Function GetDataTable() As DataTable
        Return DTFloatingDetail
    End Function
    Public Function OpenCloseTerminal(ByVal Terminal As String, ByVal SiteCode As String, ByVal Open As Boolean, ByVal strUserName As String, Optional ByRef tran As SqlTransaction = Nothing) As Boolean
        ActivityLogForShift(Nothing, String.Format("OpenCloseTerminal() - Terminal : {0},SiteCode : {1},Open : {2},strUserName : {3}", Terminal, SiteCode, Open, strUserName), "")
        Dim status As String = "Open"
        If Open = False Then
            status = "Close"
        End If
        Try
            Dim cmdTrn As New SqlCommand("UPDATE MSTTERMINALID SET UPDATEDON = GetDate() ,UPDATEDAT = '" & SiteCode & "', UPDATEDBY = '" & strUserName & "', OpenCloseSTATUS='" & status & "'  WHERE SITECODE='" & SiteCode & "' AND TERMINALID='" & Terminal & "'", SpectrumCon)
            If Not tran Is Nothing Then
                cmdTrn.Transaction = tran
                If CInt(cmdTrn.ExecuteNonQuery()) > 0 Then
                    OpenCloseTerminal = True
                End If
            Else
                OpenConnection()
                If CInt(cmdTrn.ExecuteNonQuery()) > 0 Then
                    OpenCloseTerminal = True
                End If
                CloseConnection()
            End If
        Catch ex As Exception
            LogException(ex)
            ' Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function GetShiftOpenDetail(ByVal Sitecode As String, ByVal Terminal As String, ByVal Dayopendate As DateTime, ByVal shiftid As Integer) As Double
        Try
            Dim StrQuery As String
            StrQuery = "Select * from FloatingDetail where SiteCode='" & Sitecode & "' AND TerminalId='" & Terminal & "' AND ShiftId='" & shiftid & "' AND (Action='Open' or Action='ExtraOpen') AND (CONVERT(Datetime,CONVERT(VARCHAR(10), FlotDatetime, 101)) =Convert(datetime,'" & Dayopendate.ToString("MM/dd/yyyy") & "'))"
            Dim daTill As New SqlDataAdapter(StrQuery, ConString)
            Dim dtDetail As New DataTable
            daTill.Fill(dtDetail)
            If Not dtDetail Is Nothing AndAlso dtDetail.Rows.Count > 0 Then
                Dim dv As New DataView(dtDetail, "Action='Open' And Status=1", "", DataViewRowState.CurrentRows)
                If dv.Count > 0 Then
                    dv.AllowDelete = True
                    dv.RowFilter = "Action='Open' And Status=0"
                    For Each dr As DataRowView In dv
                        dr.Delete()
                    Next
                    dtDetail.AcceptChanges()
                End If
                Dim TotalAmount As Double
                TotalAmount = IIf(dtDetail.Compute("Sum(TotalAmount)", "") Is DBNull.Value, 0, dtDetail.Compute("Sum(TotalAmount)", ""))
                Return TotalAmount
            Else
                Return 0
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function UpdateIncosDetail(ByVal incos As Boolean, ByVal terminal As String, ByVal shiftid As Integer, ByVal SiteCode As String, ByVal CurrentDate As DateTime, ByVal TillDate As DateTime, ByVal Action As String, ByVal Amount As Double, ByVal userid As String, ByVal tender As DataTable, Optional ByRef tran As SqlTransaction = Nothing) As Boolean
        Try
            ActivityLogForShift(tender, String.Format("UpdateIncosDetail() - incos : {0},terminal : {1},shiftid : {2},SiteCode :{3},CurrentDate : {4},TillDate : {5},Action : {6},Amount :{7},userid : {8}", incos, terminal, shiftid, SiteCode, CurrentDate, TillDate, Action, Amount, userid), shiftid)
            ' Dim Query As String
            'Query = "Delete from FLOATINGINCOSDETAIL   WHERE Status=1 And action='" & Action & "'  and SITECODE='" & SiteCode & "' AND TERMINALID='" & terminal & "' AND ShiftId='" & shiftid & "' and (CONVERT(Datetime,CONVERT(VARCHAR(10), FlotDatetime, 101)) ='" & TillDate.ToString("yyyyMMdd") & "');"
            Dim Query As New StringBuilder
            Query.Length = 0
            If incos = True Then
                If tender.Rows.Count > 0 Then
                    For index = 0 To tender.Rows.Count - 1
                        If Not tender.Rows(index)("Amount").ToString() = 0 Then
                            '  Query = Query & "INSERT INTO FLOATINGINCOSDETAIL VALUES('" & SiteCode & "','" & terminal & "','" & Action & "','" & TillDate.ToString("yyyyMMdd") & "'," & tender.Rows(index)("Amount").ToString() & ",'" & SiteCode & "','" & userid & "',GetDate(),'" & SiteCode & "','" & userid & "',GetDate(),1," & shiftid & ",'" & tender.Rows(index)("TenderCode").ToString() & "');" '' commented by nikhil
                            Query.Append("If exists (Select * from FLOATINGINCOSDETAIL WHERE Status=1 And action='" & Action & "'  " & vbCrLf)
                            Query.Append("and SITECODE='" & SiteCode & "' AND TERMINALID='" & terminal & "' AND ShiftId='" & shiftid & "' and TenderTypeCode ='" & tender.Rows(index)("TenderCode").ToString() & "'" & vbCrLf)
                            Query.Append("AND (CONVERT(Datetime,CONVERT(VARCHAR(10), FlotDatetime, 101)) ='" & TillDate.ToString("yyyyMMdd") & "'))" & vbCrLf)
                            Query.Append("Begin Update  FLOATINGINCOSDETAIL  set Status= 1 , Action ='" & Action & "' , SiteCode='" & SiteCode & "' , TerminalId ='" & terminal & "' , ShiftId='" & shiftid & "'," & vbCrLf)
                            Query.Append("FlotDatetime  ='" & TillDate.ToString("yyyyMMdd") & "'" & vbCrLf)
                            Query.Append("where FlotDatetime ='" & TillDate.ToString("yyyyMMdd") & "'AND TERMINALID='" & terminal & "' and TenderTypeCode ='" & tender.Rows(index)("TenderCode").ToString() & "' End else Begin" & vbCrLf)
                            Query.Append("INSERT INTO FLOATINGINCOSDETAIL VALUES('" & SiteCode & "','" & terminal & "','" & Action & "','" & TillDate.ToString("yyyyMMdd") & "'," & tender.Rows(index)("Amount").ToString() & ",'" & SiteCode & "','" & userid & "',GetDate(),'" & SiteCode & "','" & userid & "',GetDate(),1,'" & shiftid & "','" & tender.Rows(index)("TenderCode").ToString() & "')" & vbCrLf)
                            Query.Append("End" & vbCrLf)
                            'Query = Query & "INSERT INTO FLOATINGINCOSDETAIL VALUES('" & SiteCode & "','" & terminal & "','" & Action & "','" & TillDate.ToString("yyyyMMdd") & "'," & tender.Rows(index)("Amount").ToString() & ",'" & SiteCode & "','" & userid & "',GetDate(),'" & SiteCode & "','" & userid & "',GetDate(),1,'" & shiftid & "','" & tender.Rows(index)("TenderCode").ToString() & "');"
                        End If
                    Next
                End If

                Dim cmdTrn As New SqlCommand(Query.ToString, SpectrumCon)
                If Not tran Is Nothing Then
                    cmdTrn.Transaction = tran
                    cmdTrn.ExecuteNonQuery()
                Else
                    OpenConnection()
                    cmdTrn.ExecuteNonQuery()
                    CloseConnection()
                End If
            End If
            UpdateIncosDetail = True
        Catch ex As Exception
            LogException(ex)
            UpdateIncosDetail = False
        End Try
    End Function

    ''added by nikhil
    Public Function InsertIncosDetail(ByVal incos As Boolean, ByVal SiteCode As String, ByVal terminal As String, ByVal Action As String, ByVal TillDate As DateTime, ByVal Amount As Double, ByVal shiftid As Integer, ByVal userid As String) As Boolean     '', ByRef tran As SqlTransaction
        Try
            ' ActivityLogForShift(String.Format("UpdateIncosDetail() - incos : {0},terminal : {1},shiftid : {2},SiteCode :{3},CurrentDate : {4},TillDate : {5},Action : {6},Amount :{7},userid : {8}", incos, terminal, shiftid, SiteCode, CurrentDate, TillDate, Action, Amount, userid), shiftid)

            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0
            ' Dim Query As String
            SqlQuery.Append("Delete from FLOATINGINCOSDETAIL   WHERE Status=1 And action='" & Action & "'  and SITECODE='" & SiteCode & "' AND TERMINALID='" & terminal & "' AND ShiftId='" & shiftid & "' and (CONVERT(Datetime,CONVERT(VARCHAR(10), FlotDatetime, 101)) ='" & TillDate.ToString("yyyyMMdd") & "');")

            If incos = True Then
                SqlQuery.Append("insert into FloatingIncosDetail(SiteCode,TerminalID,Action,FlotDatetime,TotalAmount,CREATEDAT," & vbCrLf)
                SqlQuery.Append("CREATEDBY,	CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS," & vbCrLf)
                SqlQuery.Append("ShiftId,TenderTypeCode )" & vbCrLf)
                SqlQuery.Append("values('" & SiteCode & "','" & terminal & "'," & vbCrLf)
                SqlQuery.Append("'" & Action & "'," & vbCrLf)
                SqlQuery.Append("'" & TillDate.ToString("yyyyMMdd") & "','" & Amount & "'," & vbCrLf)
                SqlQuery.Append("'" & SiteCode & "'," & vbCrLf)
                SqlQuery.Append("'" & userid & "',GETDATE()," & vbCrLf)
                SqlQuery.Append("'" & SiteCode & "'," & vbCrLf)
                SqlQuery.Append("'" & userid & "',GETDATE()," & vbCrLf)
                SqlQuery.Append("'1'," & vbCrLf)
                SqlQuery.Append("'" & shiftid & "', 'Card')" & vbCrLf)
            End If
            Dim cmdTrn As New SqlCommand(SqlQuery.ToString(), SpectrumCon)

            'cmdTrn.Transaction = tran
            OpenConnection()
            If cmdTrn.ExecuteNonQuery() > 0 Then
                InsertIncosDetail = True
            Else
                Return False
            End If

            Return True

        Catch ex As Exception
            LogException(ex)
            InsertIncosDetail = False
        End Try
    End Function

    Public Function SaveImprestClose(ByVal dt As DataTable, ByVal sitecode As String, ByVal shiftid As String, ByVal terminalid As String, ByVal TillDate As DateTime, ByVal UserId As String, ByVal incos As Boolean, ByVal Action As String, ByVal Amount As Double, ByVal ServerDate As DateTime, ByVal dtTender As DataTable) As Boolean

        Dim tran As SqlTransaction = Nothing
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If DeleteImprestDetail(terminalid, shiftid, sitecode, TillDate, "Imprest", tran:=tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            Else
                tran.Commit()
                CloseConnection()
            End If
            If SaveImprestCASH(dt) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If UpdateIncosDetailFOrImprest(incos, terminalid, shiftid, sitecode, ServerDate, TillDate, "Imprest", Amount, UserId, dtTender, tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            Else
                tran.Commit()
                CloseConnection()
            End If
            'tran.Commit()
            'CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            Return False
            Throw New Exception(ex.Message)
        End Try
    End Function

    '' added by ketan
    Public Function SaveImprestCASH(ByVal dtTemp As DataTable) As Boolean
        Try
            Dim Tran As SqlTransaction
            AddMode(dtTemp)
            OpenConnection()
            Tran = SpectrumCon.BeginTransaction()
            If SaveData(dtTemp, SpectrumCon, Tran) = True Then
                Tran.Commit()
                Return True
            End If
            Tran.Rollback()
            Return False
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try

    End Function
    Public Function UpdateIncosDetailFOrImprest(ByVal incos As Boolean, ByVal terminal As String, ByVal shiftid As Integer, ByVal SiteCode As String, ByVal CurrentDate As DateTime, ByVal TillDate As DateTime, ByVal Action As String, ByVal Amount As Double, ByVal userid As String, ByVal tender As DataTable, Optional ByRef tran As SqlTransaction = Nothing) As Boolean
        Try
            ActivityLogForShift(tender, String.Format("UpdateIncosDetail() - incos : {0},terminal : {1},shiftid : {2},SiteCode :{3},CurrentDate : {4},TillDate : {5},Action : {6},Amount :{7},userid : {8}", incos, terminal, shiftid, SiteCode, CurrentDate, TillDate, Action, Amount, userid), shiftid)
            Dim Query As String
            Query = "Delete from FLOATINGINCOSDETAIL   WHERE Status=1 And action='" & Action & "'  and SITECODE='" & SiteCode & "' AND TERMINALID='" & terminal & "' AND ShiftId='" & shiftid & "' and (CONVERT(Datetime,CONVERT(VARCHAR(10), FlotDatetime, 101)) ='" & TillDate.ToString("yyyyMMdd") & "');"
            If incos = True Then
                'If tender.Rows.Count > 0 Then
                'For index = 0 To tender.Rows.Count - 1
                'If Not tender.Rows(index)("Amount").ToString() = 0 Then
                Query = Query & "INSERT INTO FLOATINGINCOSDETAIL VALUES('" & SiteCode & "','" & terminal & "','" & Action & "','" & TillDate.ToString("yyyyMMdd") & "'," & Amount & ",'" & SiteCode & "','" & userid & "',GetDate(),'" & SiteCode & "','" & userid & "',GetDate(),1," & shiftid & ",'" & Action & "');"
                'Query = Query & "INSERT INTO FLOATINGINCOSDETAIL VALUES('" & SiteCode & "','" & terminal & "','" & Action & "','" & TillDate.ToString("yyyyMMdd") & "'," & tender.Rows(index)("Amount").ToString() & ",'" & SiteCode & "','" & userid & "',GetDate(),'" & SiteCode & "','" & userid & "',GetDate(),1,'" & shiftid & "','" & tender.Rows(index)("TenderCode").ToString() & "');"
                'End If
                'Next
                'End If
            End If
            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
            If Not tran Is Nothing Then
                cmdTrn.Transaction = tran
                cmdTrn.ExecuteNonQuery()
            Else
                OpenConnection()
                cmdTrn.ExecuteNonQuery()
                CloseConnection()
            End If
            UpdateIncosDetailFOrImprest = True
        Catch ex As Exception
            LogException(ex)
            UpdateIncosDetailFOrImprest = False
        End Try
    End Function

    Public Function DeleteImprestDetail(ByVal terminal As String, ByVal shiftid As Integer, ByVal SiteCode As String, ByVal TillDate As DateTime, ByVal Action As String, Optional ByRef tran As SqlTransaction = Nothing) As Boolean
        Try
            Dim Query As String
            Query = "Delete from FloatingDetail   WHERE Status=1 And action='" & Action & "'  and SITECODE='" & SiteCode & "' AND TERMINALID='" & terminal & "' AND ShiftId='" & shiftid & "' and (CONVERT(Datetime,CONVERT(VARCHAR(10), FlotDatetime, 101)) ='" & TillDate.ToString("yyyyMMdd") & "');"
            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
            If Not tran Is Nothing Then
                cmdTrn.Transaction = tran
                cmdTrn.ExecuteNonQuery()
            Else
                OpenConnection()
                cmdTrn.ExecuteNonQuery()
                CloseConnection()
            End If
            DeleteImprestDetail = True
        Catch ex As Exception
            LogException(ex)
            DeleteImprestDetail = False
        End Try
    End Function
    '--- This method is for put 
    Public Function AddExtraOpen(ByVal terminal As String, ByVal SiteCode As String, ByVal TillDate As DateTime, ByVal userid As String) As Boolean
        Try
            Dim Query As String
            Query = " INSERT INTO FloatingDetail( SiteCode, TerminalID, Action, FlotDatetime,  " & _
                    " CurrencyCode, DenominationAmt, Qty, TotalAmount, " & _
                    " CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS) " & _
                    " VALUES     (" & "'" & SiteCode & "','" & terminal & "','ExtraOpen','" & TillDate.ToString("yyyyMMdd") & "'," & _
                    " 'INR',1,0,0 " & _
                    ",'" & SiteCode & "','" & userid & "',GetDate(),'" & SiteCode & "','" & userid & "',GetDate(),1 )"

            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)

            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            CloseConnection()
            AddExtraOpen = True
        Catch ex As Exception
            LogException(ex)
            AddExtraOpen = False
        End Try
    End Function



    Public Function TillCloseStru(ByVal sitecode As String, ByVal terminalid As String, ByVal TillDate As DateTime, ByVal ServerDate As DateTime, ByVal shiftid As Integer) As DataTable
        Try
            Dim dt As New DataTable("TillCloseDtl")
            AddColumnToDataTable(dt, "SiteCode", "System.String", sitecode)
            AddColumnToDataTable(dt, "TerminalID", "System.String", terminalid)
            AddColumnToDataTable(dt, "TillDate", "System.DateTime", TillDate)
            AddColumnToDataTable(dt, "VendorAmt", "System.Double", 0)
            AddColumnToDataTable(dt, "GVAmount", "System.Double", 0)
            AddColumnToDataTable(dt, "CVAmount", "System.Double", 0)
            AddColumnToDataTable(dt, "CheckAmt", "System.Double", 0)
            AddColumnToDataTable(dt, "CREATEDAT", "System.String", sitecode)
            AddColumnToDataTable(dt, "CREATEDBY", "System.String", "0")
            AddColumnToDataTable(dt, "CREATEDON", "System.DateTime", ServerDate)
            AddColumnToDataTable(dt, "STATUS", "System.Boolean", 1)
            AddColumnToDataTable(dt, "UPDATEDAT", "System.String", sitecode)
            AddColumnToDataTable(dt, "UPDATEDBY", "System.String", "0")
            AddColumnToDataTable(dt, "UPDATEDON", "System.DateTime", ServerDate)
            AddColumnToDataTable(dt, "ShiftId", "System.Int32", shiftid)
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SaveShiftClose(ByVal ds As DataSet, ByVal sitecode As String, ByVal shiftid As String, ByVal terminalid As String, ByVal TillDate As DateTime, ByVal UserId As String, ByVal incos As Boolean, ByVal Action As String, ByVal Amount As Double, ByVal ServerDate As DateTime, ByVal dtTender As DataTable) As Boolean
        ActivityLogForShift(Nothing, String.Format("SaveShiftClose() - sitecode : {0},shiftid : {1},terminalid : {2},TillDate :{3},UserId : {4},incos : {5},Action : {6},Amount :{7},ServerDate : {8}", sitecode, shiftid, terminalid, TillDate, UserId, incos, Action, Amount, ServerDate), shiftid)
        Dim tran As SqlTransaction = Nothing
        Try
            AddColumnToDataTable(ds.Tables("TillCloseCashDtl"), "SiteCode", "System.String", sitecode)
            AddColumnToDataTable(ds.Tables("TillCloseCashDtl"), "TerminalID", "System.String", terminalid)
            AddColumnToDataTable(ds.Tables("TillCloseCashDtl"), "TillDate", "System.DateTime", TillDate)
            AddColumnToDataTable(ds.Tables("TillCloseCashDtl"), "CREATEDAT", "System.String", sitecode)
            AddColumnToDataTable(ds.Tables("TillCloseCashDtl"), "CREATEDBY", "System.String", UserId)
            AddColumnToDataTable(ds.Tables("TillCloseCashDtl"), "CREATEDON", "System.DateTime", ServerDate)
            AddColumnToDataTable(ds.Tables("TillCloseCashDtl"), "STATUS", "System.Boolean", 1)
            AddColumnToDataTable(ds.Tables("TillCloseCashDtl"), "UPDATEDAT", "System.String", sitecode)
            AddColumnToDataTable(ds.Tables("TillCloseCashDtl"), "UPDATEDBY", "System.String", UserId)
            AddColumnToDataTable(ds.Tables("TillCloseCashDtl"), "UPDATEDON", "System.DateTime", ServerDate)
            AddColumnToDataTable(ds.Tables("TillCloseCashDtl"), "ShiftId", "System.Int32", shiftid)
            ds.Tables("TillCloseCashDtl").Columns("Rate").ColumnName = "ExchangeRate"
            ds.Tables("TillCloseCashDtl").Columns("Total").ColumnName = "TotalAmount"
            DeleteColumnFromDataTable(ds.Tables("TillCloseCashDtl"), "Currency")

            Dim dtTemp As DataTable = ds.Tables("TillCloseFinanceDtl").Copy()
            ds.Tables("TillCloseFinanceDtl").Clear()
            Dim dv As New DataView(dtTemp, "", "", DataViewRowState.CurrentRows)
            Dim dtUnique As DataTable = dv.ToTable(True, "TENDERTYPE", "ISSUED")
            For Each drUnique As DataRow In dtUnique.Rows
                Dim drnew As DataRow = ds.Tables("TillCloseFinanceDtl").NewRow()
                drnew("AMOUNTTENDERED") = 0
                For Each dr As DataRow In dtTemp.Select("TenderType='" & drUnique("TENDERTYPE").ToString() & "' And Issued=" & drUnique("ISSUED").ToString(), "", DataViewRowState.CurrentRows)
                    drnew("TENDERTYPE") = dr("TENDERTYPE").ToString()
                    drnew("AMOUNTTENDERED") = drnew("AMOUNTTENDERED") + dr("AMOUNTTENDERED")
                    drnew("UserAmount") = Val(drnew("UserAmount").ToString()) + Val(dr("UserAmount").ToString())  ' Val(drnew("UserAmount")) +
                Next
                ds.Tables("TillCloseFinanceDtl").Rows.Add(drnew)
            Next



            AddColumnToDataTable(ds.Tables("TillCloseFinanceDtl"), "SiteCode", "System.String", sitecode)
            AddColumnToDataTable(ds.Tables("TillCloseFinanceDtl"), "TerminalID", "System.String", terminalid)
            AddColumnToDataTable(ds.Tables("TillCloseFinanceDtl"), "TillDate", "System.DateTime", TillDate)
            AddColumnToDataTable(ds.Tables("TillCloseFinanceDtl"), "CREATEDAT", "System.String", sitecode)
            AddColumnToDataTable(ds.Tables("TillCloseFinanceDtl"), "CREATEDBY", "System.String", UserId)
            AddColumnToDataTable(ds.Tables("TillCloseFinanceDtl"), "CREATEDON", "System.DateTime", ServerDate)
            AddColumnToDataTable(ds.Tables("TillCloseFinanceDtl"), "STATUS", "System.Boolean", 1)
            AddColumnToDataTable(ds.Tables("TillCloseFinanceDtl"), "UPDATEDAT", "System.String", sitecode)
            AddColumnToDataTable(ds.Tables("TillCloseFinanceDtl"), "UPDATEDBY", "System.String", UserId)
            AddColumnToDataTable(ds.Tables("TillCloseFinanceDtl"), "UPDATEDON", "System.DateTime", ServerDate)
            AddColumnToDataTable(ds.Tables("TillCloseFinanceDtl"), "ShiftId", "System.Int32", shiftid)
            ds.Tables("TillCloseFinanceDtl").Columns("AMOUNTTENDERED").ColumnName = "SystemAmount"
            DeleteColumnFromDataTable(ds.Tables("TillCloseFinanceDtl"), "ISSUED")
            DeleteColumnFromDataTable(ds.Tables("TillCloseFinanceDtl"), "DESCRIPTION")
            DeleteColumnFromDataTable(ds.Tables("TillCloseFinanceDtl"), "TenderHeadCode")

            AddMode(ds)

            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            '------log
            ActivityLogForShift(Nothing, "SaveData()", shiftid)
            For index = 0 To ds.Tables.Count - 1
                ActivityLogForShift(ds.Tables(index), ds.Tables(index).TableName, shiftid)
            Next
            If SaveData(ds, SpectrumCon, tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If
            If UpdateIncosDetail(incos, terminalid, shiftid, sitecode, ServerDate, TillDate, "TillClose", Amount, UserId, dtTender, tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If
            If OpenCloseTerminal(terminalid, sitecode, False, UserId, tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If
            If DeleteHoldBill(terminalid, sitecode, tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If
            If OpenCloseShift(terminalid, sitecode, shiftid, TillDate, UserId, tran) = False Then
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
    Public Function OpenCloseShift(ByVal Terminal As String, ByVal SiteCode As String, ByVal shiftid As String, ByVal dayopendate As Date, ByVal strUserName As String, Optional ByRef tran As SqlTransaction = Nothing) As Boolean
        ActivityLogForShift(Nothing, String.Format("OpenCloseShift() - Terminal : {0},SiteCode : {1},shiftid : {2},dayopendate :{3},strUserName : {4}", Terminal, SiteCode, shiftid, dayopendate, strUserName), shiftid)
        Dim status As String = "Close"
        Try
            Dim cmdTrn As New SqlCommand("UPDATE ShiftOpenClose SET UPDATEDON = GetDate() ,UPDATEDAT = '" & SiteCode & "', UPDATEDBY = '" & strUserName & "', OpenCloseSTATUS='" & status & "'  WHERE SITECODE='" & SiteCode & "' AND TERMINALID='" & Terminal & "' AND Shiftid='" & shiftid & "' And OpenDate=@DayOpenDate", SpectrumCon)
            cmdTrn.Parameters.AddWithValue("@DayOpenDate", SqlDbType.Date)
            cmdTrn.Parameters("@DayOpenDate").Value = dayopendate

            If Not tran Is Nothing Then
                cmdTrn.Transaction = tran
                If CInt(cmdTrn.ExecuteNonQuery()) > 0 Then
                    OpenCloseShift = True
                End If
            Else
                OpenConnection()
                If CInt(cmdTrn.ExecuteNonQuery()) > 0 Then
                    OpenCloseShift = True
                End If
                CloseConnection()
            End If
        Catch ex As Exception

            LogException(ex)
            Return False
        End Try
    End Function
    Public Function DeleteHoldBill(ByVal Terminalid As String, ByVal strSitecode As String, Optional ByRef tran As SqlTransaction = Nothing) As Boolean
        Try
            ActivityLogForShift(Nothing, String.Format("DeleteHoldBill() - Terminalid : {0},strSitecode : {1}", Terminalid, strSitecode), "")
            Dim query As New StringBuilder
            query.Append("Delete from HOLDCASHMEMODTL WHERE SITECODE='" & strSitecode & "' AND BILLNO  in (Select Billno from HOLDCASHMEMOHDR  WHERE SITECODE='" & strSitecode & "' AND TerminalID='" & Terminalid & "');")
            Dim cmdTrn As New SqlCommand(query.ToString(), SpectrumCon)
            cmdTrn.Transaction = tran
            cmdTrn.ExecuteNonQuery()
            cmdTrn.CommandText = "Delete from HoldCashMemoMettler WHERE BILLNO  in (Select Billno from HOLDCASHMEMOHDR  WHERE SITECODE='" & strSitecode & "' AND TerminalID='" & Terminalid & "');"
            cmdTrn.ExecuteNonQuery()
            cmdTrn.CommandText = "Delete from HOLDCASHMEMOHDR WHERE SITECODE='" & strSitecode & "' AND TerminalID='" & Terminalid & "' "
            cmdTrn.ExecuteNonQuery()

            DeleteHoldBill = True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function SaveRecords(ByVal ds As DataSet, ByVal incos As Boolean, ByVal Terminal As String, ByVal Sitecode As String, ByVal dayOpenDate As DateTime, ByVal currentshiftid As Integer, ByVal nextshiftid As Integer, ByVal usercode As String, ByVal ISEditeMode As Boolean) As Boolean
        Dim tran As SqlTransaction
        Try
            ActivityLogForShift(Nothing, String.Format("SaveRecords() : Incos : {0},Terminal : {1},Sitecode : {2},dayOpenDate : {3},currentshiftid : {4}, nextshiftid : {5}, usercode : {6}, ISEditeMode : {7}", incos, Terminal, Sitecode, dayOpenDate, currentshiftid, nextshiftid, usercode, ISEditeMode), nextshiftid)
            For index = 0 To ds.Tables.Count - 1
                ActivityLogForShift(ds.Tables(index), "", nextshiftid)
            Next
            OpenConnection()
            tran = SpectrumCon.BeginTransaction
            If ISEditeMode = False Then
                If InsertShiftOpenCloseDetails(Sitecode, dayOpenDate, Terminal, nextshiftid, usercode, tran) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
            End If
            If SaveData(ds, SpectrumCon, tran) Then
                If UpdateLastDayEntry(incos, Terminal, Sitecode, tran, dayOpenDate, currentshiftid, nextshiftid) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
                tran.Commit()
                CloseConnection()
                ''added by Ketan  '' FUn :- UpdateFloatingDetails Added By ketan issue in Opening Balance
                If ISEditeMode = False Then
                    If UpdateFloatingDetails(Sitecode, dayOpenDate, Terminal, nextshiftid, usercode, tran) = False Then
                        tran.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If
                Return True
            Else
                tran.Rollback()
                CloseConnection()
                Return False
            End If
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function InsertShiftOpenCloseDetails(ByVal SiteCode As String, ByVal DayOpenDate As Date, ByVal TerminalID As String, ByVal ShiftID As String, ByVal userid As String, ByRef tran As SqlTransaction) As Boolean
        Try
            ActivityLogForShift(Nothing, String.Format("InsertShiftOpenCloseDetails() : Sitecode : {0},DayOpenDate : {1},TerminalID : {2},ShiftID : {3}, userid : {4}", SiteCode, DayOpenDate, TerminalID, ShiftID, userid), ShiftID)
            InsertShiftOpenCloseDetails = False
            Dim Query As String

            Query = " INSERT INTO ShiftOpenClose( SiteCode, OpenDate ,TerminalID, ShiftId, OpenCloseStatus, " & _
                    " CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON) " & _
                    " VALUES     (" & "'" & SiteCode & "',@DayOpenDate ,'" & TerminalID & "','" & ShiftID & "','Open'" & _
                    ",'" & SiteCode & "','" & userid & "',GETDATE(),'" & SiteCode & "','" & userid & "',GETDATE())"

            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
            cmdTrn.Parameters.AddWithValue("@DayOpenDate", SqlDbType.Date)
            cmdTrn.Parameters("@DayOpenDate").Value = DayOpenDate
            cmdTrn.Transaction = tran
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            InsertShiftOpenCloseDetails = True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    '' FUn :- UpdateFloatingDetails Added By ketan issue in Opening Balance
    Public Function UpdateFloatingDetails(ByVal SiteCode As String, ByVal DayOpenDate As Date, ByVal TerminalID As String, ByVal ShiftID As String, ByVal userid As String, ByRef tran As SqlTransaction) As Boolean
        Try
            ' ActivityLogForShift(Nothing, String.Format("InsertShiftOpenCloseDetails() : Sitecode : {0},DayOpenDate : {1},TerminalID : {2},ShiftID : {3}, userid : {4}", SiteCode, DayOpenDate, TerminalID, ShiftID, userid), ShiftID)
            UpdateFloatingDetails = False
            Dim Query As String
            Query = "Update FloatingDetail Set CREATEDON = GetDate (),UPDATEDON = GETDATE() WHERE  " & _
                   " SiteCode = '" & SiteCode & "' AND  FlotDatetime= @DayOpenDate AND TerminalID = '" & TerminalID & "' AND " & _
             " ShiftID = '" & ShiftID & "' AND ACTION ='ExtraOpen'"
            Dim cmdTrn As New SqlCommand(Query, SpectrumCon)
            cmdTrn.Parameters.AddWithValue("@DayOpenDate", SqlDbType.Date)
            cmdTrn.Parameters("@DayOpenDate").Value = DayOpenDate
            cmdTrn.Transaction = tran
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            UpdateFloatingDetails = True
            'tran.Commit()
            'CloseConnection()
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Private Function UpdateLastDayEntry(ByVal incos As Boolean, ByVal Terminalid As String, ByVal strSitecode As String, ByRef tran As SqlTransaction, ByVal dayOpenDate As DateTime, ByVal shiftid As Integer, Optional ByVal currentshiftid As Integer = 0) As Boolean
        Try
            ActivityLogForShift(Nothing, String.Format("UpdateLastDayEntry() : Incos : {0},Terminalid : {1},strSitecode : {2},dayOpenDate : {3},shiftid : {4}", incos, Terminalid, strSitecode, dayOpenDate, shiftid), currentshiftid)
            Dim cmdTrn As New SqlCommand("Update FloatingDetail Set FlotDatetime=& Cast('" & dayOpenDate.ToString("yyyyMMdd") & "' AS DateTime) ' WHERE SITECODE='" & strSitecode & "'  TerminalID='" & Terminalid & "' and ShiftId='" & shiftid & "' ", SpectrumCon)
            'Dim cmdTrn As New SqlCommand("Update FloatingDetail Set FlotDatetime=& Cast('" & dayOpenDate.ToString("yyyyMMdd") & "' AS DateTime) ' WHERE SITECODE='" & strSitecode & "'  TerminalID='" & Terminalid & "' ", SpectrumCon)
            cmdTrn.Transaction = tran
            If incos = False Then
                cmdTrn.CommandText = "Delete from FloatingDetail where sitecode='" & strSitecode & "' and terminalID='" & Terminalid & "' and ShiftId='" & shiftid & "' and (convert(varchar(12), FlotDatetime  ,101) = '12/01/9999') And Status=0"
                'cmdTrn.CommandText = "Delete from FloatingDetail where sitecode='" & strSitecode & "' and terminalID='" & Terminalid & "' and (convert(varchar(12), FlotDatetime  ,101) = '12/01/9999') And Status=0"
            End If
            cmdTrn.ExecuteNonQuery()
            UpdateLastDayEntry = True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function GetTillOpenAmount(ByVal siteCode As String, ByVal terminalId As String, ByVal currencyCode As String) As Double
        Try
            GetTillOpenAmount = 0
            Dim dtFloatingDetail As DataTable
            Dim query As String = " Select sum(isnull(TotalAmount,0))  AS TotalAmount " & _
                                  " from FloatingDetail " & _
                                  " WHERE SiteCode='" & siteCode & "' AND TerminalID='" & terminalId & "' And CurrencyCode = '" & currencyCode & "' " & _
                                  " AND Action = 'Open' and FlotDatetime = '9999-12-01 00:00:00.000' "
            dtFloatingDetail = GetFilledTable(query)
            If Not dtFloatingDetail Is Nothing And dtFloatingDetail.Rows.Count > 0 Then
                GetTillOpenAmount = dtFloatingDetail.Rows(0)("TotalAmount")
            End If
        Catch ex As Exception
            LogException(ex)

        End Try
    End Function


    Public Function GetExistingDetail(ByVal ActionType As String, ByVal Sitecode As String, ByVal terminalid As String, ByVal dayopendate As DateTime) As DataTable
        Try
            Dim TAFloatingDetail As New POSDBDataSetTableAdapters.FloatingDetailTableAdapter
            TAFloatingDetail.Connection.ConnectionString = ConString
            'Return TAFloatingDetail.GetExistingFloatingDetail(ActionType, Sitecode, terminalid, dayopendate)
            Return TAFloatingDetail.GetExistingFloatingDetail(ActionType, Sitecode, terminalid)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function CheckIfValidShiftOpenAmount(ByVal siteCode As String, ByVal terminalId As String, ByVal currencyCode As String, ByVal tillOpenAmount As Double, ByVal shiftid As Integer) As Boolean
        Try
            ActivityLogForShift(Nothing, String.Format("CheckIfValidShiftOpenAmount() - Sitecode : {0},terminalid : {1},currencycode : {2},tillOpenAmount : {3},shitid : {4} ", siteCode, terminalId, currencyCode, tillOpenAmount, shiftid), shiftid)
            Dim dtFloatingDetail As DataTable
            Dim query As String = "Select TotalAmount from FloatingDetail " & _
                "WHERE SiteCode='" & siteCode & "' AND TerminalID='" & terminalId & "' And CurrencyCode = '" & currencyCode & "' AND Action = 'Open' and FlotDatetime = '9999-12-01 00:00:00.000' And ShiftId =  '" & shiftid & "'"
            dtFloatingDetail = GetFilledTable(query)
            If Not dtFloatingDetail Is Nothing And dtFloatingDetail.Rows.Count > 0 Then
                Dim totalNextdayAmount As Double = dtFloatingDetail.Compute("SUM(TotalAmount)", "")
                If tillOpenAmount = totalNextdayAmount Then
                    Return True
                Else
                    Return False
                End If
            Else
                If tillOpenAmount = 0 Then
                    Return True
                Else
                    Return False
                End If
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function GetNextDayFloatingDetailStruc() As DataTable
        Dim query As String = "Select * from NextDayFloatingDetail where 1=0"
        Return GetFilledTable(query)
    End Function

    Public Sub DeleteNextDayFloatIfExist(ByVal strSitecode As String, ByVal Terminalid As String, ByVal shiftid As Integer)
        Try
            ActivityLogForShift(Nothing, String.Format("DeleteNextDayFloatIfExist() - SiteCode : {0},Terminalid : {1},shiftid : {2} ", strSitecode, Terminalid, shiftid), shiftid)
            OpenConnection()
            Dim cmddelete As New SqlCommand("Delete from FloatingDetail where sitecode='" & strSitecode & "' and terminalID='" & Terminalid & "' and ShiftId='" & shiftid & "' and Action = 'Open'", SpectrumCon)
            cmddelete.ExecuteNonQuery()
            CloseConnection()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class
