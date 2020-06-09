Imports System.Data.SqlClient

Public Class clsExpeditor
#Region "Global Variable For Class "
    Dim daCM As SqlDataAdapter
    Dim daScan As New SqlDataAdapter
    Dim tran As SqlTransaction

#End Region

    Public Function GetNewBills(ByVal siteCode As String, ByVal PrepStationID As String, DayOpenDate As Date, ByRef LastBillStampTime As Nullable(Of DateTime)) As DataTable
        Try
            Dim vStmtQry As New System.Text.StringBuilder
            Dim DsNewBills As New DataSet
            vStmtQry.Append(" SELECT GETDATE() As ServerDateTime ;")

            vStmtQry.Append("  SELECT cmd.mstPrepStationID, CMH.BillNo, CMH.CREATEDON,Terminal.TerminalName,art.ArticleName ,art.ArticleShortName ,ISNULL(CMDIR.itemRemarks,'') AS Remark , CMD.Quantity, ")
            vStmtQry.Append("  '' AS OrderTitle,CMD.articleCode,cmd.isItemReady,CMH.isOrderReady, CMH.createdOn as [Time]    ")
            vStmtQry.Append("  FROM    PrepStationOrderHdr AS CMH Inner Join   ")
            vStmtQry.Append("  		MstTerminalID As Terminal On CMH.TerminalID = Terminal.TerminalID Inner Join   ")
            vStmtQry.Append("  		PrepStationOrderDtl AS CMD ON CMH.siteCode = CMD.SiteCode AND CMH.billNo = CMD.billNo  Inner Join    ")
            vStmtQry.Append("  	    MstArticle AS art on CMD.ArticleCode = art.ArticleCode Left Join   ")
            vStmtQry.Append("  	    CashMemoDtlItemRemark AS CMDIR On  CMD.SiteCode = CMDIR.SiteCode AND CMD.FinYear= CMDIR.FinYear   ")
            vStmtQry.Append("  					      	AND CMD.BillNo = CMDIR.BillNo AND CMD.BillLineNo = CMDIR.BillLineNo   ")
            vStmtQry.Append("  							AND CMD.EAN	= CMDIR.EAN AND CMD.ArticleCode = CMDIR.ArticleCode   ")
            vStmtQry.Append("  WHERE  ")
            vStmtQry.Append("  	      ISNULL(CMH.isOrderReady,0) = 0 AND CMH.siteCode ='" & siteCode & "' ")
            'vStmtQry.Append("  	     AND (CMD.mstPrepStationID ='" & PrepStationID & "' or CMD.mstPrepStationID is null  ")
            vStmtQry.Append("  	     AND CMH.billDate = @DayOpenDate ")
            vStmtQry.Append("  	     AND  CMD.ArticleCode IS NOT NULL ")

            If LastBillStampTime IsNot Nothing Then
                vStmtQry.Append(" AND   CMH.CREATEDON >= @LastBillStampTime ")
            End If
            vStmtQry.Append(" order by CMH.createdOn asc ")
            Dim cmd As New SqlCommand(vStmtQry.ToString, SpectrumCon)
            cmd.Parameters.AddWithValue("@DayOpenDate", DayOpenDate)
            If LastBillStampTime IsNot Nothing Then
                cmd.Parameters.AddWithValue("@LastBillStampTime", LastBillStampTime)
            End If
            ' -- ISNULL(CMH.isOrderReady,0) = 0 AND ISNULL(CMD.isItemReady,0) = 0    --and CMH.isOrderReady = 0 
            daCM = New SqlDataAdapter(cmd)
            daCM.Fill(DsNewBills)

            LastBillStampTime = DsNewBills.Tables(0).Rows(0)(0)

            Return DsNewBills.Tables(1)

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function



    Public Function UpdateOrderReady(BillNo As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            Dim vStmtQry As New System.Text.StringBuilder
            vStmtQry.Length = 0

            vStmtQry.Append(" Update  PrepStationOrderHdr ")
            vStmtQry.Append(" SET     isOrderReady = 1 ,UpdatedOn =GetDate() ")
            vStmtQry.Append(" Where   BillNo = '" & BillNo & "'")

            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim cmd As New SqlCommand(vStmtQry.ToString(), SpectrumCon, tran)

            cmd.ExecuteNonQuery()
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            LogException(ex)
            CloseConnection()
            Return False
        End Try
    End Function

    Public Function UpdateOrderRetrive(BillNo As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            Dim vStmtQry As New System.Text.StringBuilder
            vStmtQry.Length = 0

            vStmtQry.Append(" Update  PrepStationOrderHdr ")
            vStmtQry.Append(" SET     isOrderReady = 0 ,UpdatedOn =GetDate() ")
            vStmtQry.Append(" Where   BillNo = '" & BillNo & "'")

            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim cmd As New SqlCommand(vStmtQry.ToString(), SpectrumCon, tran)

            cmd.ExecuteNonQuery()
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            LogException(ex)
            CloseConnection()
            Return False
        End Try
    End Function

    Public Function RetriveBills(ByVal siteCode As String, ByVal PrepStationID As String, DayOpenDate As Date) As DataTable
        Try
            Dim vStmtQry As New System.Text.StringBuilder
            Dim DsNewBills As New DataTable

            vStmtQry.Append("  SELECT   cmd.mstPrepStationID, CMH.BillNo, CMH.CREATEDON,Terminal.TerminalName,art.ArticleName ,art.ArticleShortName ,ISNULL(CMDIR.itemRemarks,'') AS Remark , CMD.Quantity, ")
            vStmtQry.Append("  '' AS OrderTitle,CMD.articleCode,cmd.isItemReady,CMH.isOrderReady, CMH.createdOn as [Time]     ")
            vStmtQry.Append("  FROM    PrepStationOrderHdr AS CMH Inner Join   ")
            vStmtQry.Append("  		MstTerminalID As Terminal On CMH.TerminalID = Terminal.TerminalID Inner Join   ")
            vStmtQry.Append("  		PrepStationOrderDtl AS CMD ON CMH.siteCode = CMD.SiteCode AND CMH.billNo = CMD.billNo  Inner Join    ")
            vStmtQry.Append("  	    MstArticle AS art on CMD.ArticleCode = art.ArticleCode Left Join   ")
            vStmtQry.Append("  	    CashMemoDtlItemRemark AS CMDIR On  CMD.SiteCode = CMDIR.SiteCode AND CMD.FinYear= CMDIR.FinYear   ")
            vStmtQry.Append("  					      	AND CMD.BillNo = CMDIR.BillNo AND CMD.BillLineNo = CMDIR.BillLineNo   ")
            vStmtQry.Append("  							AND CMD.EAN	= CMDIR.EAN AND CMD.ArticleCode = CMDIR.ArticleCode   ")
            vStmtQry.Append("  WHERE  ISNULL(CMH.isOrderReady,0) = 1 AND ISNULL(CMD.isItemReady,0) = 1  ")

            vStmtQry.Append("  	     AND CMH.siteCode ='" & siteCode & "' ")
            'vStmtQry.Append("  	     AND CMD.mstPrepStationID ='" & PrepStationID & "' ")
            vStmtQry.Append("  	     AND CMH.billDate = @DayOpenDate ")
            vStmtQry.Append("  	     AND  CMD.ArticleCode IS NOT NULL ")

            vStmtQry.Append(" order by CMD.UpdatedOn DESC")
            Dim cmd As New SqlCommand(vStmtQry.ToString, SpectrumCon)
            cmd.Parameters.AddWithValue("@DayOpenDate", DayOpenDate)

            daCM = New SqlDataAdapter(cmd)
            daCM.Fill(DsNewBills)
            Return DsNewBills

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


   
End Class
