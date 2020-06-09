Imports System.Text
Imports System.Data
Imports System.Data.SqlClient

Public Class clsDefault

    Dim SqlTrans As SqlTransaction
    Dim Sqlda As SqlDataAdapter
    Dim SqlReader As SqlDataReader
    Dim Sqlcmdb As SqlCommandBuilder
    Dim Sqlcmd As SqlCommand
    Dim Sqlds As DataSet
    Dim Sqldr As DataRow
    Dim SqlColumn As DataColumn

    Dim objComn As New clsCommon
    Dim vStmtQry As New StringBuilder

    Public Function GetComboDataSet(ByVal ConfigFunction As String) As DataSet
        Try
            vStmtQry.Length = 0
            If ConfigFunction = "DefaultProcess" Then
                vStmtQry.Append("Select SiteCode, SiteShortName from MstSite Where Status=1; " & vbCrLf)               '1 Site Information

            ElseIf ConfigFunction = "DefaultConfiguration" Then
                vStmtQry.Append("Select SiteCode, SiteShortName from MstSite Where Status=1; " & vbCrLf)               '1 Site Information
                vStmtQry.Append("Select ProcessId, ProcessName from SysDefaultProcess Where Status=1; " & vbCrLf)               '1 Site Information

            ElseIf ConfigFunction = "ConfigurationList" Then
                vStmtQry.Append("Select SiteCode, SiteShortName from MstSite Where Status=1; " & vbCrLf)               '1 Site Information
                vStmtQry.Append("Select ConfigurationId, ConfigName from SysDefaultConfiguration Where Status=1; " & vbCrLf)               '1 Site Information
            End If

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlClient.SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            If ConfigFunction = "DefaultProcess" Then
                Sqlds.Tables(0).TableName = "MstSite"

            ElseIf ConfigFunction = "DefaultConfiguration" Then
                Sqlds.Tables(0).TableName = "MstSite"
                Sqlds.Tables(1).TableName = "SysDefaultProcess"

            ElseIf ConfigFunction = "ConfigurationList" Then
                Sqlds.Tables(0).TableName = "MstSite"
                Sqlds.Tables(1).TableName = "SysDefaultConfiguration"
            End If

            Return Sqlds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    Public Function GetDefaultDataSet(ByVal ConfigFunction As String) As DataSet
        Try
            vStmtQry.Length = 0
            If ConfigFunction = "DefaultProcess" Then
                vStmtQry.Append("Select * From SysDefaultProcess ; " & vbCrLf)               '1 Site Information

            ElseIf ConfigFunction = "DefaultConfiguration" Then
                vStmtQry.Append("Select * From SysDefaultConfiguration ; " & vbCrLf)               '1 Site Information

            ElseIf ConfigFunction = "ConfigurationList" Then
                vStmtQry.Append("Select * From sysDefaultConfigurationList ; " & vbCrLf)               '1 Site Information
            End If

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlClient.SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            If ConfigFunction = "DefaultProcess" Then
                Sqlds.Tables(0).TableName = "SysDefaultProcess"
                Dim KeyProcess(1) As DataColumn
                KeyProcess(0) = Sqlds.Tables("SysDefaultProcess").Columns("SiteCode")
                KeyProcess(1) = Sqlds.Tables("SysDefaultProcess").Columns("ProcessId")
                Sqlds.Tables("SysDefaultProcess").PrimaryKey = KeyProcess

            ElseIf ConfigFunction = "DefaultConfiguration" Then
                Sqlds.Tables(0).TableName = "SysDefaultConfiguration"
                Dim KeyConfig(1) As DataColumn
                KeyConfig(0) = Sqlds.Tables("SysDefaultConfiguration").Columns("SiteCode")
                KeyConfig(1) = Sqlds.Tables("SysDefaultConfiguration").Columns("ConfigurationId")
                Sqlds.Tables("SysDefaultConfiguration").PrimaryKey = KeyConfig

            ElseIf ConfigFunction = "ConfigurationList" Then
                Sqlds.Tables(0).TableName = "sysDefaultConfigurationList"
                Dim KeyConfig(1) As DataColumn
                KeyConfig(0) = Sqlds.Tables("sysDefaultConfigurationList").Columns("SiteCode")
                KeyConfig(1) = Sqlds.Tables("sysDefaultConfigurationList").Columns("ConfigurationlistId")
                Sqlds.Tables("sysDefaultConfigurationList").PrimaryKey = KeyConfig
            End If

            Return Sqlds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    Public Function SaveData(ByRef dsSaveData As DataSet, ByVal NextDocDesc As String, ByVal IsNextDocNo As Boolean) As Boolean
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


End Class
