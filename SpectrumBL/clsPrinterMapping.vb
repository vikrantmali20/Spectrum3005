Imports System.Text
Imports System.Data
Imports System.Data.SqlClient

Public Class clsPrinterMapping

    Dim daPrn As New SqlDataAdapter
    Dim cmdbPrn As New SqlCommandBuilder
    Dim cmdPrn As New SqlCommand
    Dim dsPrn As New DataSet
    Dim dtPrn As DataTable
    Dim SqlQuery As New StringBuilder
    Dim SqlTrans As SqlTransaction = Nothing
    Dim clsComn As New clsCommon

    Public Function GetPrnMappingStruc(ByVal pSiteCode As String) As DataSet
        Try
            'SqlQuery.Append("" & vbCrLf)
            SqlQuery.Length = 0
            SqlQuery.Append("Select * From PrinterTillMap; " & vbCrLf)

            daPrn = New SqlDataAdapter(SqlQuery.ToString, ConString)
            cmdbPrn = New SqlCommandBuilder(daPrn)
            dsPrn = New DataSet
            daPrn.Fill(dsPrn)

            dsPrn.Tables(0).TableName = "PrinterTillMap"

            Dim KeyPrinter(1) As DataColumn
            KeyPrinter(0) = dsPrn.Tables("PrinterTillMap").Columns("TerminalID")
            KeyPrinter(1) = dsPrn.Tables("PrinterTillMap").Columns("PrinterDocument")
            dsPrn.Tables("PrinterTillMap").PrimaryKey = KeyPrinter

            Return dsPrn

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function GetPrinterComboInfo(ByVal pSiteCode As String) As DataSet
        Try
            'SqlQuery.Append("" & vbCrLf)
            SqlQuery.Length = 0
            SqlQuery.Append("Select TerminalID, TerminalName from MstTerminalID Where SiteCode='" & pSiteCode & "'; " & vbCrLf)
            SqlQuery.Append("Select PrinterDocument, DocumentDesc from MstPrinterDoc; " & vbCrLf)
            SqlQuery.Append("Select TerminalID, DriverType, LogicalName from PosDeviceProfile Where SiteCode='" & pSiteCode & "' And DeviceType='Printer'; " & vbCrLf)

            daPrn = New SqlDataAdapter(SqlQuery.ToString, ConString)
            cmdbPrn = New SqlCommandBuilder(daPrn)
            dsPrn = New DataSet
            daPrn.Fill(dsPrn)

            dsPrn.Tables(0).TableName = "TerminalInfo"
            dsPrn.Tables(1).TableName = "PrinterDoc"
            dsPrn.Tables(2).TableName = "DeviceProfile"

            Return dsPrn

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function SavePrinterTillMapping(ByRef dsMain As DataSet, ByVal vSiteCode As String) As Boolean
        Try
            OpenConnection()
            If dsMain.Tables("PrinterTillMap").Columns.Contains("DocDesc") = True Then
                dsMain.Tables("PrinterTillMap").Columns.Remove("DocDesc")
            End If

            For TbCnt = 0 To dsMain.Tables.Count - 1
                Dim tableName As String = dsMain.Tables(TbCnt).TableName
                SqlQuery.Length = 0
                SqlQuery.Append("SELECT * FROM " & tableName)

                daPrn = New SqlDataAdapter(SqlQuery.ToString, SpectrumCon)
                cmdbPrn = New SqlCommandBuilder(daPrn)
                daPrn.TableMappings.Add(tableName, tableName)
                daPrn = cmdbPrn.DataAdapter

                daPrn.Update(dsMain, tableName)
            Next

            CloseConnection()
            Return True

        Catch ex As Exception
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function SavePrinterHierarchyMapping(ByRef dsMain As DataSet, ByVal vSiteCode As String) As Boolean
        Try
            OpenConnection()
            If dsMain.Tables("PrinterSubDocMap").Columns.Contains("DocDesc") = True Then
                dsMain.Tables("PrinterSubDocMap").Columns.Remove("DocDesc")
            End If

            For TbCnt = 0 To dsMain.Tables.Count - 1
                Dim tableName As String = dsMain.Tables(TbCnt).TableName
                SqlQuery.Length = 0
                SqlQuery.Append("SELECT * FROM " & tableName)

                daPrn = New SqlDataAdapter(SqlQuery.ToString, SpectrumCon)
                cmdbPrn = New SqlCommandBuilder(daPrn)
                daPrn.TableMappings.Add(tableName, tableName)
                daPrn = cmdbPrn.DataAdapter

                daPrn.Update(dsMain, tableName)
            Next

            CloseConnection()
            Return True

        Catch ex As Exception
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function GetHirearchyList() As DataTable
        Try
            ' Dim strString As String = "select NodeCode,NodeName from MstArticleNode  where LevelCode=1 and status =1   "
            Dim strString As String = "select NodeCode,NodeName from MstArticleNode where isthisLastNode=1 and STATUS = 1"
            Dim dt As New DataTable
            Dim daDefault As New SqlDataAdapter(strString, ConString)
            daDefault.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetPrnSubDocMappingStruc() As DataSet
        Try
            'SqlQuery.Append("" & vbCrLf)
            SqlQuery.Length = 0
            SqlQuery.Append("Select * From PrinterSubDocMap; " & vbCrLf)

            daPrn = New SqlDataAdapter(SqlQuery.ToString, ConString)
            cmdbPrn = New SqlCommandBuilder(daPrn)
            dsPrn = New DataSet
            daPrn.Fill(dsPrn)

            dsPrn.Tables(0).TableName = "PrinterSubDocMap"

            Dim KeyPrinter(1) As DataColumn
            KeyPrinter(0) = dsPrn.Tables("PrinterSubDocMap").Columns("DocumentType")
            KeyPrinter(1) = dsPrn.Tables("PrinterSubDocMap").Columns("SubDocumentType")
            dsPrn.Tables("PrinterSubDocMap").PrimaryKey = KeyPrinter

            Return dsPrn

        Catch ex As Exception
            Return Nothing
        End Try

    End Function
End Class
