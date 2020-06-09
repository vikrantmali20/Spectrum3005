Imports System.Data
Imports System.Data.SqlClient
Imports System.Text

''' <summary>
''' This Class is Used For Record Production
''' </summary>
''' <Createdby></Createdby>
''' <UpdatedBy></UpdatedBy>
''' <usedin>Produce Items</usedin>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
Public Class clsRecordProduction
    Inherits clsCommon
#Region "Public Function's & Method's"
    ''' <summary>
    ''' Get the Schema for Saving all data of Grid
    ''' </summary>
    ''' <returns>Datable</returns>
    ''' <remarks></remarks>
    Public Function GetItemSchemaInfoForRecordProduction() As DataTable
        Try
            Dim StmtQry As New StringBuilder

            StmtQry.Length = 0
            StmtQry.Append(" Select Convert(Varchar(50),'') as ItemCode, " & vbCrLf)
            StmtQry.Append(" Convert(Varchar(100),'') as Description, " & vbCrLf)
            StmtQry.Append(" Convert(numeric(18,3),0) as Quantity, " & vbCrLf)
            StmtQry.Append(" Convert(Varchar(1000),'') as Remarks " & vbCrLf)
            Dim daScan As New SqlDataAdapter
            daScan = New SqlClient.SqlDataAdapter(StmtQry.ToString, SpectrumCon)
            Dim dtScan As New DataTable
            daScan.Fill(dtScan)

            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Generating Productionid and 
    ''' Inserting data of 'Produce Items' to RecordProductionHdr and RecordProductionDtl
    ''' </summary>
    ''' <param name="productionid"></param>
    ''' <param name="siteCode"></param>
    ''' <param name="finYear"></param>
    ''' <param name="Remarks"></param>
    ''' <param name="userId"></param>
    ''' <param name="DayOpenDate"></param>
    ''' <param name="TerminalId"></param>
    ''' <param name="dtRecordProductionItems"></param>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Public Function InsertRecordProductionData(ByVal productionid As String, ByVal Remarks As String, ByVal SlapperName As String, ByVal siteCode As String, ByVal finYear As String, ByVal userId As String, ByVal TerminalId As String, ByRef dtRecordProductionItems As DataTable) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            Dim SrNo As Integer = 1
            Dim DocNo As String = getDocumentNo("Record Production", siteCode)
            If String.IsNullOrEmpty(productionid) Then
                Try
                    productionid = GenDocNo("RCP" & TerminalId & finYear.Substring(finYear.Length - 2, 2), 15, DocNo)
                Catch ex As Exception
                    productionid = "RCP" & TerminalId & DocNo
                End Try
            End If

            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim cmd As New System.Text.StringBuilder()
            Dim objcomm As New clsCommon

            cmd.Append(" INSERT INTO RecordProductionHdr( ")
            cmd.Append(" ProductionId,SiteCode,FinYear,")
            cmd.Append(" Date,Remarks,SlapperName,")
            cmd.Append("CREATEDAT, CREATEDBY, CREATEDON,")
            cmd.Append(" UPDATEDAT,UPDATEDBY,  UPDATEDON,STATUS) ")
            cmd.Append(" Values(")
            cmd.Append("'" & productionid & "','" & siteCode & "','" & finYear & "',")
            cmd.Append(" GetDate(),'" & Remarks & "','" & SlapperName.Replace("'", "") & "',")
            cmd.Append("'" & siteCode & "','" & userId & "',GetDate(), ")
            cmd.Append("'" & siteCode & "','" & userId & "', GetDate(),1")
            cmd.Append(") ;")

            For Each dr As DataRow In dtRecordProductionItems.Rows

                cmd.Append(" INSERT INTO RecordProductionDtl ( ")
                cmd.Append(" ProductionId,SiteCode,SrNo,ArticleCode,Qty,")
                cmd.Append("Remarks,CREATEDAT,")
                cmd.Append(" CREATEDBY,CREATEDON,UPDATEDAT,")
                cmd.Append(" UPDATEDBY,UPDATEDON,STATUS)")
                cmd.Append(" Values(")
                cmd.Append("'" & productionid & "','" & siteCode & "','" & SrNo & "','" & dr("ItemCode") & "','" & dr("Quantity") & "',")
                cmd.Append("'" & dr("Remarks").ToString.Replace("'", "").Trim & "',")
                cmd.Append("'" & siteCode & "','" & userId & "',GetDate(), ")
                cmd.Append("'" & siteCode & "','" & userId & "',GetDate(),1")
                cmd.Append("); ")
                SrNo = SrNo + 1
            Next
            If objcomm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If
            cmd.Length = 0
            If UpdateDocumentNo("Record Production", SpectrumCon, tran) = False Then
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
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    ''' <summary>
    ''' Get Items Details with Description for adding in Grid
    ''' </summary>
    ''' <param name="SiteCode"></param>
    ''' <param name="strItem"></param>
    ''' <param name="lngCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
   Public Function GetItemDetails() As DataTable
        Try
            Dim dt As New DataTable
            Dim ArticleQuery As String
            ArticleQuery = " Select B.ArticleCode ,B.ArticleShortName ,B.ArticleName "
            ArticleQuery = ArticleQuery & " FROM MSTEAN A with (nolock) INNER JOIN MSTARTICLE B with (nolock) ON A.ARTICLECODE=B.ARTICLECODE WHERE B.ArticalTypeCode='KIT'   "
            ArticleQuery = ArticleQuery & " Union "
            ArticleQuery = ArticleQuery & " Select B.ArticleShortName as ArticleCode  , B.ArticleCode as ArticleShortName,B.ArticleName "
            ArticleQuery = ArticleQuery & " FROM MSTEAN A with (nolock) INNER JOIN MSTARTICLE B with (nolock) ON A.ARTICLECODE=B.ARTICLECODE WHERE B.ArticalTypeCode='KIT'   "
            dt = GetFilledTable(ArticleQuery)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Get all Items 
    ''' </summary>
    ''' <param name="SiteCode"></param>
    ''' <param name="strItem"></param>
    ''' <param name="lngCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllItems(ByVal SiteCode As String, ByVal strItem As String, ByRef lngCode As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim ArticleQuery As String
            ArticleQuery = " Select B.ArticleCode ,B.ArticleShortName ,B.ArticleName, 0 AS Qty  "
            ArticleQuery = ArticleQuery & " FROM MSTEAN A with (nolock) INNER JOIN MSTARTICLE B with (nolock) ON A.ARTICLECODE=B.ARTICLECODE "
            If Not String.IsNullOrEmpty(strItem) Then
                ArticleQuery = ArticleQuery & " Where (A.EAN='" & strItem & "' Or A.ARTICLECODE='" & strItem & "' or B.LegacyArticleCode = '" & strItem & "' or B.ArticleName = '" & strItem & "' AND  B.ArticalTypeCode='KIT')"
            End If
            dt = GetFilledTable(ArticleQuery)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
#End Region

End Class
