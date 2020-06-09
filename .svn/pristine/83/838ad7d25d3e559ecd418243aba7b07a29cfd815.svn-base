Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Public Class clsBulkComboAdd
    Dim Sqlda As SqlDataAdapter
    Dim Sqlcmdb As SqlCommandBuilder
    Dim Sqlds As DataSet
    Dim Sqldt As DataTable

    Dim vStmtQry As New StringBuilder
    Dim objComn As New clsCommon

    Dim daBC As SqlDataAdapter
    Dim dtReturn As DataTable
    Dim ServerDate As DateTime
    Dim Month, day, Quarter As Int32
    Dim Months As String



    Public Function GetPackagingBoxDataSet(ByVal lastNodeCode As String) As DataTable
        Try
            vStmtQry.Length = 0

            vStmtQry.Append(" SELECT ArticleCode ,ArticalTypeCode ,ArticleShortName,ArticleName,BaseUnitofMeasure " & vbCrLf)
            vStmtQry.Append(" FROM  mstarticle AS MA inner join  " & vbCrLf)
            vStmtQry.Append(" 	  dbo.GetLastNodeCode('" & lastNodeCode & "') AS ln " & vbCrLf)
            vStmtQry.Append(" 	  ON MA.LastNodeCode = ln.lastnodeCode" & vbCrLf)

            'vStmtQry.Append(" SELECT ArticleCode ,ArticalTypeCode ,ArticleShortName,ArticleName,BaseUnitofMeasure " & vbCrLf)
            'vStmtQry.Append(" FROM  mstarticle AS MA   " & vbCrLf)
            'vStmtQry.Append(" Where	MA.LastNodeCode = '" & lastNodeCode & "'" & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlClient.SqlCommandBuilder(Sqlda)
            Sqldt = New DataTable
            Sqlda.Fill(Sqldt)

            Sqldt.TableName = "PackagingBox"

            Return Sqldt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

   
    ''' <summary>
    ''' Get the DB Structure for Binding
    ''' </summary>
    ''' <param name="CashMemo">BulkComboMstId</param>
    ''' <param name="SiteCode">SiteCode</param>
    ''' <UpdatedOn></UpdatedOn>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Function GetBulkComboStru(ByVal BulkComboMstId As String) As DataSet
        Try
            Dim ds As New DataSet
            daBC = New SqlDataAdapter(" EXEC GetBulkComboStru '" & BulkComboMstId & "'", SpectrumCon)
            daBC.Fill(ds)
            ds.Tables(0).TableName = "SoBulkComboHdr"
            ds.Tables(1).TableName = "SoBulkComboDtl"
            Return ds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

End Class


'Public Function GetPackagingCarryBagDataSet() As DataTable
'    Try
'        vStmtQry.Length = 0
'        vStmtQry.Append(" SELECT ArticleCode,ArticleShortName,ArticleName,BaseUnitofMeasure " & vbCrLf)
'        vStmtQry.Append(" FROM  mstarticle AS MA inner join  " & vbCrLf)
'        vStmtQry.Append(" 	  dbo.GetLastNodeCode('ANC000000000015') AS ln " & vbCrLf)
'        vStmtQry.Append(" 	  ON MA.LastNodeCode = ln.lastnodeCode" & vbCrLf)

'        Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
'        Sqlcmdb = New SqlClient.SqlCommandBuilder(Sqlda)
'        Sqldt = New DataTable
'        Sqlda.Fill(Sqldt)

'        Sqldt.TableName = "PackagingCarryBag"

'        Return Sqldt
'    Catch ex As Exception
'        LogException(ex)
'        Return Nothing
'    End Try
'End Function
