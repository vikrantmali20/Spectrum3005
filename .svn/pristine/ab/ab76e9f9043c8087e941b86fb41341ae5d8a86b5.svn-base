Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Public Class clsDataArcive
    Inherits clsCommon
    Dim newConString As String = ConString
    Public Function CopyDataBase(ByVal OldDatabase As String, ByVal NewDataBaseName As String, ByVal Path As String, ByVal TakeonlyBackup As Boolean) As Boolean
        Try
            Dim Con As New SqlConnection(ConString)
            Dim strQuery As String = "EXEC COPYDATABASE  '" & OldDatabase & "','" & NewDataBaseName & "','" & Path & "\" & "'," & TakeonlyBackup
            Dim cmdDB As SqlCommand = New SqlCommand(strQuery, Con)
            cmdDB.CommandTimeout = 0
            If Con.State <> ConnectionState.Open Then
                Con.Open()
            End If
            If cmdDB.ExecuteNonQuery() > 0 Then
                newConString = newConString.Replace(OldDatabase, NewDataBaseName)
                Con.Close()
                Return True
            Else
                Con.Close()
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function DeleteExtraFromArciveData(ByVal fromdate As DateTime, ByVal todate As DateTime, ByVal Sitecode As String) As Boolean
        Try
            Dim SqlQuerys As New StringBuilder
            Dim daDataArcSetting As New SqlDataAdapter("SELECT * FROM DATAARCHIVESETTINGS WHERE STATUS=1 AND PROCESSNAME='TRANSACTION' ORDER BY SEQ DESC", newConString)
            Dim dtDataArcSetting As New DataTable
            daDataArcSetting.Fill(dtDataArcSetting)
            If Not dtDataArcSetting Is Nothing AndAlso dtDataArcSetting.Rows.Count > 0 Then
                SqlQuerys.Length = 0
                For Each drow As DataRow In dtDataArcSetting.Rows
                    SqlQuerys.Append("Delete from " & drow("TableName").ToString().Trim & " ")
                    If drow("ParentTable").ToString() <> String.Empty Then
                        Dim ParentDateField As String = ""
                        Dim ParentWherecond As String = ""
                        Dim dv As New DataView(dtDataArcSetting, "TableName='" & drow("ParentTable").ToString().Trim & "'", "", DataViewRowState.CurrentRows)
                        If dv.Count > 0 Then
                            ParentDateField = dv.Item(0)("FilterField").ToString().Trim
                            If dv.Item(0)("WhereCondition").ToString().Trim <> String.Empty Then
                                ParentWherecond = dv.Item(0)("WhereCondition").ToString().Trim
                            End If
                        End If
                        If ParentDateField = String.Empty Then
                            ParentDateField = drow("FilterField").ToString().Trim
                        End If
                        SqlQuerys.Append(" Where " & IIf(drow("ChildField").ToString.Trim <> String.Empty, drow("ChildField").ToString.Trim, drow("ParentField").ToString().Trim) & " IN (Select  " & drow("ParentField").ToString().Trim & " from " & drow("ParentTable").ToString().Trim & " Where SiteCode='" & Sitecode & "' AND NOT Convert(DateTime,Convert(Varchar(10)," & ParentDateField & ",101)) between  convert(datetime,'" & fromdate.ToString("MM/dd/yyyy") & "',101) AND convert(datetime,'" & todate.ToString("MM/dd/yyyy") & "',101)" & IIf(ParentWherecond <> String.Empty, " AND " & ParentWherecond, "") & " )")
                        If drow("WhereCondition").ToString.Trim <> String.Empty Then
                            SqlQuerys.Append(" AND " & drow("WhereCondition").ToString().Trim)
                        End If
                    Else
                        SqlQuerys.Append(" Where NOT (Convert(DateTime,Convert(Varchar(10)," & drow("FilterField").ToString().Trim & ",101)) between  convert(datetime,'" & fromdate.ToString("MM/dd/yyyy") & "',101) AND convert(datetime,'" & todate.ToString("MM/dd/yyyy") & "',101))")
                        If drow("WhereCondition").ToString.Trim <> String.Empty Then
                            SqlQuerys.Append(" AND " & drow("WhereCondition").ToString().Trim)
                        End If
                    End If
                    SqlQuerys.Append(" AND SiteCode='" & Sitecode & "' ;" & vbCrLf)
                Next
                Dim sqlCon As New SqlConnection(newConString)
                Dim SqlComm As New SqlCommand(SqlQuerys.ToString(), sqlCon)
                SqlComm.CommandTimeout = 0
                If sqlCon.State <> ConnectionState.Open Then
                    sqlCon.Open()
                End If
                SqlComm.ExecuteNonQuery()
                sqlCon.Close()
                Return True
            Else
                Throw New Exception(getvaluebykey("DA029"))
            End If
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(getValueByKey("DA029"))
        End Try
    End Function
    Public Function DeleteMaster(ByVal fromdate As DateTime, ByVal todate As DateTime) As Boolean
        Try
            Dim daDataArcSetting As New SqlDataAdapter("SELECT * FROM DATAARCHIVESETTINGS WHERE STATUS=1 AND PROCESSNAME<>'TRANSACTION' AND SelectReq=1 order by seq desc;SELECT * FROM DATAARCHIVESETTINGS WHERE STATUS=1 AND PROCESSNAME<>'TRANSACTION' ORDER BY SEQ DESC", newConString)
            Dim dsDataArcSetting As New DataSet
            daDataArcSetting.Fill(dsDataArcSetting)
            If Not dsDataArcSetting Is Nothing Then
                If dsDataArcSetting.Tables(0).Rows.Count > 0 Then
                    For Each drow As DataRow In dsDataArcSetting.Tables(0).Rows
                        Dim dtdata As New DataTable
                        Dim strQuery As String = "Select * from " & drow("TableName").ToString & " Where 1=1 "
                        If drow("SelectDateCheckOn").ToString <> String.Empty AndAlso drow("SelectCondition").ToString.Trim <> String.Empty Then
                            strQuery = strQuery & " AND Convert(Datetime,Convert(varchar(10)," & drow("SelectCondition").ToString & ",101)) <Convert(datetime, " & IIf(drow("SelectDateCheckOn").ToString.ToUpper = "ToDate".ToUpper, todate.ToString("MM/dd/yyyy"), fromdate.ToString("MM/dd/yyyy")) & ",101)"
                        ElseIf drow("SelectDateCheckOn").ToString = String.Empty AndAlso drow("SelectCondition").ToString.Trim <> String.Empty Then
                            strQuery = strQuery & " AND " & drow("SelectCondition").ToString
                        End If
                        Dim daData As New SqlDataAdapter(strQuery, newConString)
                        daData.Fill(dtdata)
                        If dtdata.Rows.Count > 0 Then
                            For Each row As DataRow In dtdata.Rows
                                Try
                                    Dim SqlQuerys As New StringBuilder
                                    SqlQuerys.Length = 0
                                    For Each drSettins As DataRow In dsDataArcSetting.Tables(1).Select("PROCESSNAME='" & drow("PROCESSNAME").ToString & "'", "SEQ DESC", DataViewRowState.CurrentRows)
                                        SqlQuerys.Append("Delete from " & drSettins("TableName").ToString().Trim & " Where " & drSettins("FilterField").ToString() & "='" & row(drSettins("FilterField").ToString()) & "' ;" & vbCrLf)
                                    Next
                                    Dim sqlCon As New SqlConnection(newConString)
                                    Dim SqlComm As New SqlCommand(SqlQuerys.ToString(), sqlCon)
                                    'SqlComm.CommandTimeout = 0

                                    If sqlCon.State <> ConnectionState.Open Then
                                        sqlCon.Open()
                                    End If
                                    SqlComm.ExecuteNonQuery()
                                    sqlCon.Close()
                                Catch ex As Exception

                                End Try
                            Next
                        End If
                    Next
                End If
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(getValueByKey("DA030"))
        End Try
    End Function
    Public Function deletefromOriginal(ByVal fromdate As DateTime, ByVal todate As DateTime, ByVal SiteCode As String) As Boolean
        Dim tran As SqlTransaction
        Try
            Dim SqlQuerys As New StringBuilder
            Dim daDataArcSetting As New SqlDataAdapter("SELECT * FROM DATAARCHIVESETTINGS WHERE STATUS=1 AND PROCESSNAME='TRANSACTION' ORDER BY SEQ DESC", ConString)
            Dim dtDataArcSetting As New DataTable
            daDataArcSetting.Fill(dtDataArcSetting)
            If Not dtDataArcSetting Is Nothing AndAlso dtDataArcSetting.Rows.Count > 0 Then
                SqlQuerys.Length = 0
                For Each drow As DataRow In dtDataArcSetting.Rows
                    SqlQuerys.Append("Delete from " & drow("TableName").ToString().Trim & " ")
                    If drow("ParentTable").ToString() <> String.Empty Then
                        Dim ParentDateField As String = ""
                        Dim ParentWherecond As String = ""
                        Dim dv As New DataView(dtDataArcSetting, "TableName='" & drow("ParentTable").ToString().Trim & "'", "", DataViewRowState.CurrentRows)
                        If dv.Count > 0 Then
                            ParentDateField = dv.Item(0)("FilterField").ToString().Trim
                            If dv.Item(0)("WhereCondition").ToString().Trim <> String.Empty Then
                                ParentWherecond = dv.Item(0)("WhereCondition").ToString().Trim
                            End If
                        End If
                        If ParentDateField = String.Empty Then
                            ParentDateField = drow("FilterField").ToString().Trim
                        End If
                        SqlQuerys.Append(" Where " & IIf(drow("ChildField").ToString.Trim <> String.Empty, drow("ChildField").ToString.Trim, drow("ParentField").ToString().Trim) & " IN (Select  " & drow("ParentField").ToString().Trim & " from " & drow("ParentTable").ToString().Trim & " Where SiteCode='" & SiteCode & "' AND Convert(DateTime,Convert(Varchar(10)," & ParentDateField & ",101)) between  convert(datetime,'" & fromdate.ToString("MM/dd/yyyy") & "',101) AND convert(datetime,'" & todate.ToString("MM/dd/yyyy") & "',101)" & IIf(ParentWherecond <> String.Empty, " AND " & ParentWherecond, "") & " )")
                        If drow("WhereCondition").ToString.Trim <> String.Empty Then
                            SqlQuerys.Append(" AND " & drow("WhereCondition").ToString().Trim)
                        End If
                    Else
                        SqlQuerys.Append(" Where (Convert(DateTime,Convert(Varchar(10)," & drow("FilterField").ToString().Trim & ",101)) between  convert(datetime,'" & fromdate.ToString("MM/dd/yyyy") & "',101) AND convert(datetime,'" & todate.ToString("MM/dd/yyyy") & "',101))")
                        If drow("WhereCondition").ToString.Trim <> String.Empty Then
                            SqlQuerys.Append(" AND " & drow("WhereCondition").ToString().Trim)
                        End If
                    End If
                    SqlQuerys.Append(" AND SiteCode='" & SiteCode & "' ;" & vbCrLf)
                Next

                Dim sqlCon As New SqlConnection(ConString)
                Dim SqlComm As New SqlCommand(SqlQuerys.ToString(), sqlCon)
                If sqlCon.State <> ConnectionState.Open Then
                    sqlCon.Open()
                End If
                tran = sqlCon.BeginTransaction
                SqlComm.Transaction = tran
                SqlComm.CommandTimeout = 0
                SqlComm.ExecuteNonQuery()
                tran.Commit()
                sqlCon.Close()
                Return True
            Else
                Throw New Exception(getValueByKey("DA029"))
            End If
        Catch ex As Exception
            If (Not tran Is Nothing) Then
                tran.Rollback()
            End If
            LogException(ex)
            Throw New Exception(getValueByKey("DA029"))
        End Try

    End Function
    Public Function ReindexingArciveTable(ByVal NewDB As Boolean) As Boolean
        Try

            Dim sqlCon As SqlConnection
            If NewDB = True Then
                sqlCon = New SqlConnection(newConString)
            Else
                sqlCon = New SqlConnection(ConString)
            End If
            Dim SqlComm As New SqlCommand("EXEC REINDEXINGTABLE", sqlCon)
            SqlComm.CommandTimeout = 0
            If sqlCon.State <> ConnectionState.Open Then
                sqlCon.Open()
            End If
            SqlComm.ExecuteNonQuery()
            sqlCon.Close()
            Return True
        Catch ex As Exception
            LogException(ex)
            If NewDB = True Then
                Throw New Exception(getValueByKey("DA031"))
            Else
                Throw New Exception(getValueByKey("DA031"))
            End If
        End Try
    End Function

    Public Function DataArchiveLog(ByVal strSiteCode As String, ByVal strDbName As String, ByVal dtFromDate As Date, ByVal dtToDate As Date, ByVal dtExecutionTime As Date, ByVal strCashierName As String) As Boolean
        Try
            Dim objclscomman As New clsCommon
            dtExecutionTime = objclscomman.GetCurrentDate()

            Dim sqlCon As SqlConnection
            sqlCon = New SqlConnection(ConString)
            Dim SqlComm As New SqlCommand("select max(id)   from  DataArciveDtls", sqlCon)
            If sqlCon.State <> ConnectionState.Open Then
                sqlCon.Open()
            End If
            Dim sqlReader As SqlDataReader = SqlComm.ExecuteReader()


            Dim objMaxId As Object
            If Not sqlReader Is Nothing Then
                If sqlReader.Read() Then
                    objMaxId = sqlReader.GetValue(0)
                    objMaxId = IIf(objMaxId Is DBNull.Value, 0, objMaxId) + 1
                Else
                    objMaxId = 1
                End If
            Else
                objMaxId = 1
            End If

            sqlCon.Close()

            Dim SqlComm2 As New SqlCommand("insert  into DataArciveDtls(ID,SiteCode,ArciveDBName,FromDate,Todate,Executedon,Executedby) values(" & objMaxId & ",'" & strSiteCode & "','" & strDbName & "',convert(datetime,'" & dtFromDate.ToString("MM/dd/yyyy") & "',101),convert(datetime,'" & dtToDate.ToString("MM/dd/yyyy") & "',101) ,convert(datetime,'" & dtExecutionTime.ToString("MM/dd/yyyy") & "',101) ,'" & strCashierName & "')", sqlCon)
            If sqlCon.State <> ConnectionState.Open Then
                sqlCon.Open()
            End If
            SqlComm2.ExecuteNonQuery()
            sqlCon.Close()
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Function getLastDataArchiveDate(ByVal strSiteCode As String) As Object
        Try
            Dim sqlCon As SqlConnection
            sqlCon = New SqlConnection(ConString)
            Dim SqlComm As New SqlCommand("select  sitecode,todate  from  DataArciveDtls where sitecode='" & strSiteCode & "' order by id desc ", sqlCon)
            If sqlCon.State <> ConnectionState.Open Then
                sqlCon.Open()
            End If
            Dim sqlReader As SqlDataReader = SqlComm.ExecuteReader()
            Dim objMaxId As Object
            If Not sqlReader Is Nothing Then
                If sqlReader.Read() Then
                    objMaxId = sqlReader.GetValue(1)
                Else
                    objMaxId = getSiteCreationDate(strSiteCode)
                End If
            Else
                objMaxId = getSiteCreationDate(strSiteCode)
            End If
            If sqlCon.State <> ConnectionState.Closed Then
                sqlCon.Close()
            End If
            Return objMaxId
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Function getSiteCreationDate(ByVal strSiteCode As String) As Object
        Try
            Dim sqlCon As SqlConnection
            sqlCon = New SqlConnection(ConString)
            Dim SqlComm As New SqlCommand("select  sitecode,CREATEDON  from  MstSite where sitecode='" & strSiteCode & "'   ", sqlCon)
            If sqlCon.State <> ConnectionState.Open Then
                sqlCon.Open()
            End If
            Dim sqlReader As SqlDataReader = SqlComm.ExecuteReader()
            Dim objMaxId As Object
            If Not sqlReader Is Nothing Then
                If sqlReader.Read() Then
                    objMaxId = sqlReader.GetValue(1)
                Else
                    objMaxId = Nothing
                End If
            Else
                objMaxId = Nothing
            End If
            sqlCon.Close()
            Return objMaxId
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

End Class
