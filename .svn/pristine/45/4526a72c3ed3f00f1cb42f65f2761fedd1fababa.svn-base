Imports System.Text
Imports System.Data
Imports System.Data.SqlClient

Public Class clsPromotionMaster

    Dim SqlTrans As SqlTransaction
    Dim Sqlda As SqlDataAdapter
    Dim Sqlcmdb As SqlCommandBuilder
    Dim Sqlds As DataSet

    Dim vStmtQry As New StringBuilder
    Dim objComn As New clsCommon

    Public Function GetComboDataSet() As DataSet
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select OfferTypeCode, OfferTypeDesc, OfferLevel, OfferApplicableFor from PromoType; " & vbCrLf)
            vStmtQry.Append("Select SiteCode, SiteShortName from MstSite Where Status=1; " & vbCrLf)
            vStmtQry.Append("Select QuestionNo, QuestionName, OfferApplicableFor, 'false' as IsApplicable " & vbCrLf)
            vStmtQry.Append("from PromotionValidationQuestions ; " & vbCrLf)

            Sqlda = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Sqlcmdb = New SqlClient.SqlCommandBuilder(Sqlda)
            Sqlds = New DataSet
            Sqlda.Fill(Sqlds)

            Sqlds.Tables(0).TableName = "PromoType"
            Sqlds.Tables(1).TableName = "MstSite"
            Sqlds.Tables(2).TableName = "PromValQuestions"
            
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
