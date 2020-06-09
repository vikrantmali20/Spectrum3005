Imports System.Data.SqlClient

Public Class clsKdsData
#Region "Global Variable For Class "
    Dim daCM As SqlDataAdapter
    Dim daScan As New SqlDataAdapter
    Dim tran As SqlTransaction

#End Region

#Region " Function's & Method's"

    Public Function GetArticleHeaderDetails() As DataTable
        Try
            Dim vStmtQry As New System.Text.StringBuilder
            Dim dtOrderHdr As New DataTable
            vStmtQry.Append("select top 10 BillNo,'Order No:'+ Convert(nvarchar(50),BillNo)+'-'+Convert(nvarchar(50),TerminalID) AS Result,")
            vStmtQry.Append("'(Elasped Time:'+convert(varchar(15),DateDiff(s, billdate, GETDATE())%3600/60)+':'+")
            vStmtQry.Append("convert(varchar(15),(DateDiff(s, billdate, GETDATE())%60))+')' as [ElaspedTime],  ")
            vStmtQry.Append("convert(varchar(15),DateDiff(s, billdate, GETDATE())%3600/60)+'.'+convert(varchar(15),(DateDiff(s, billdate, GETDATE())%60)) as [time] from CashMemoHdr")

            daCM = New SqlDataAdapter(vStmtQry.ToString(), SpectrumCon)

            daCM.Fill(dtOrderHdr)
            Return dtOrderHdr
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetArticleDetails(ByVal OrderNo As String) As DataTable
        Try
            Dim vStmtQry1 As New System.Text.StringBuilder
            Dim dtOrderDtl As New DataTable
            vStmtQry1.Append("select   art.ArticleCode, art.ArticleName as Item,CMDtl.Quantity, ")
            vStmtQry1.Append("convert(varchar(5),DateDiff(s, cmdtl.billdate, GETDATE())%3600/60)+':'+")
            vStmtQry1.Append("convert(varchar(5),(DateDiff(s, cmdtl.billdate, GETDATE())%60)) as [Time], ")
            vStmtQry1.Append("convert(varchar(5),DateDiff(s, cmdtl.billdate, GETDATE())%3600/60)+'.'+convert(varchar(5),(DateDiff(s, cmdtl.billdate, GETDATE())%60)) as [CheckTime] ")
            vStmtQry1.Append("from CashMemoDtl CMDtl join MSTArticle art on ")
            vStmtQry1.Append("CMDtl.ArticleCode = art.ArticleCode  ")
            vStmtQry1.Append("Where  CMDtl.BillNo='" & OrderNo & "'")

            daCM = New SqlDataAdapter(vStmtQry1.ToString(), SpectrumCon)

            daCM.Fill(dtOrderDtl)
            Return dtOrderDtl
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetNewBills_KotWise(ByVal siteCode As String, ByVal PrepStationID As String, DayOpenDate As Date, ByRef LastBillStampTime As Nullable(Of DateTime)) As DataTable
        Try
            Dim vStmtQry As New System.Text.StringBuilder
            Dim DsNewBills As New DataSet
            vStmtQry.Append(" SELECT GETDATE() As ServerDateTime ;")

            vStmtQry.Append("(  SELECT  CMH.BillNo, CMD.CREATEDON,Terminal.TerminalName,art.ArticleName ,art.ArticleShortName ,ISNULL(CMD.Remark,'') AS Remark , CMD.Quantity, ")
            vStmtQry.Append("  '' AS OrderTitle,CMD.articleCode ,dbo.ListOfKitArticle(CMD.articleCode)  'KitArticle' ,  #KOT.KotNO,#KOT.QTY,CMD.TableNo ")
            vStmtQry.Append("  FROM    PrepStationOrderHdr_KOTWise AS CMH Inner Join   ")
            vStmtQry.Append("  		MstTerminalID As Terminal On CMH.TerminalID = Terminal.TerminalID Inner Join   ")
            vStmtQry.Append("  		PrepStationOrderDtl_KOTWise AS CMD ON CMH.siteCode = CMD.SiteCode AND CMH.billNo = CMD.billNo  Inner Join    ")
            vStmtQry.Append("  	    MstArticle AS art on CMD.ArticleCode = art.ArticleCode Left Join   ")
            vStmtQry.Append("  	    CashMemoDtlItemRemark AS CMDIR On  CMD.SiteCode = CMDIR.SiteCode AND CMD.FinYear= CMDIR.FinYear   ")
            vStmtQry.Append("  					      	AND CMD.BillNo = CMDIR.BillNo AND CMD.BillLineNo = CMDIR.BillLineNo   ")
            vStmtQry.Append("  							AND CMD.EAN	= CMDIR.EAN AND CMD.ArticleCode = CMDIR.ArticleCode   ")
            vStmtQry.Append("INNER JOIN (select  BILLNO,EAN ,KOTNO,SUM(KOTQUANTITY) 'QTY' from DineInKotQtyMap  group by BILLNO,EAN,KOTNO ) #KOT on #KOT.Billno = CMD.Billno AND #KOT.EAN =  CMD.EAN  AND #KOT.KOTNO =  CMD.KOTNO")
            vStmtQry.Append("  WHERE  ISNULL(CMH.isOrderReady,0) = 0 AND ISNULL(CMD.isItemReady,0) = 0   ")
            vStmtQry.Append("  	     AND CMH.siteCode ='" & siteCode & "' ")
            vStmtQry.Append("  	     AND CMD.mstPrepStationID ='" & PrepStationID & "' ")
            vStmtQry.Append("  	     AND CMH.billDate = @DayOpenDate ")
            vStmtQry.Append("  	     AND  CMD.ArticleCode IS NOT NULL ")

            If LastBillStampTime IsNot Nothing Then
                vStmtQry.Append("AND   CMH.UpdatedOn >= @LastBillStampTime   and CMD.updatedOn>=@LastBillStampTime ")
            End If
            vStmtQry.Append(" UNION   ")
            vStmtQry.Append(" SELECT  CMH.BillNo, CMD.CREATEDON,Terminal.TerminalName,art.ArticleName ,art.ArticleShortName ,ISNULL(CMDIR.itemRemarks,'') AS Remark , CMD.Quantity, ")
            vStmtQry.Append("  '' AS OrderTitle,CMD.articleCode ,dbo.ListOfKitArticle(CMD.articleCode)  'KitArticle' ,  CMD.KotNO,CMD.quantity,CMD.TableNo ")
            vStmtQry.Append("  FROM    PrepStationOrderHdr_KOTWise AS CMH Inner Join   ")
            vStmtQry.Append("  		MstTerminalID As Terminal On CMH.TerminalID = Terminal.TerminalID Inner Join   ")
            vStmtQry.Append("  		PrepStationOrderDtl_KOTWise AS CMD ON CMH.siteCode = CMD.SiteCode AND CMH.billNo = CMD.billNo  Inner Join    ")
            vStmtQry.Append("  	    MstArticle AS art on CMD.ArticleCode = art.ArticleCode Left Join   ")
            vStmtQry.Append("  	    CashMemoDtlItemRemark AS CMDIR On  CMD.SiteCode = CMDIR.SiteCode AND CMD.FinYear= CMDIR.FinYear   ")
            vStmtQry.Append("  					      	AND CMD.BillNo = CMDIR.BillNo AND CMD.BillLineNo = CMDIR.BillLineNo   ")
            vStmtQry.Append("  							AND CMD.EAN	= CMDIR.EAN AND CMD.ArticleCode = CMDIR.ArticleCode   ")
            'vStmtQry.Append("INNER JOIN (select  BILLNO,EAN ,KOTNO,SUM(KOTQUANTITY) 'QTY' from DineInKotQtyMap  group by BILLNO,EAN,KOTNO ) #KOT on #KOT.Billno = CMD.Billno AND #KOT.EAN =  CMD.EAN  AND #KOT.KOTNO =  CMD.KOTNO")
            vStmtQry.Append("  WHERE  ISNULL(CMH.isOrderReady,0) = 0 AND ISNULL(CMD.isItemReady,0) = 0   ")
            vStmtQry.Append("  	     AND CMH.siteCode ='" & siteCode & "' ")
            vStmtQry.Append("  	     AND CMD.mstPrepStationID ='" & PrepStationID & "' AND CMD.TableNo in ('Home Delivery','Take Away') ")
            vStmtQry.Append("  	     AND CMH.billDate = @DayOpenDate ")
            vStmtQry.Append("  	     AND  CMD.ArticleCode IS NOT NULL ")
            If LastBillStampTime IsNot Nothing Then
                vStmtQry.Append("AND   CMH.UpdatedOn >= @LastBillStampTime   and CMD.updatedOn>=@LastBillStampTime ")
            End If
            vStmtQry.Append(" )order by CMD.CreatedOn   ")
            Dim cmd As New SqlCommand(vStmtQry.ToString, SpectrumCon)
            cmd.Parameters.AddWithValue("@DayOpenDate", DayOpenDate)
            If LastBillStampTime IsNot Nothing Then
                cmd.Parameters.AddWithValue("@LastBillStampTime", LastBillStampTime)
            End If

            daCM = New SqlDataAdapter(cmd)
            daCM.Fill(DsNewBills)

            LastBillStampTime = DsNewBills.Tables(0).Rows(0)(0)

            Return DsNewBills.Tables(1)

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetRetriveBillsKotWise(ByVal siteCode As String, ByVal PrepStationID As String, DayOpenDate As Date) As DataTable
        Try
            Dim vStmtQry As New System.Text.StringBuilder
            Dim DsNewBills As New DataTable

            vStmtQry.Append(" ( SELECT  CMH.BillNo, CMD.CREATEDON,Terminal.TerminalName,art.ArticleName ,art.ArticleShortName ,ISNULL(CMD.Remark,'') AS Remark , CMD.Quantity, ")
            vStmtQry.Append("  '' AS OrderTitle,CMD.articleCode ,dbo.ListOfKitArticle(CMD.articleCode)  'KitArticle',  #KOT.KotNO,#KOT.QTY,CMD.TableNo   ")
            vStmtQry.Append("  FROM    PrepStationOrderHdr_KOTWise AS CMH Inner Join   ")
            vStmtQry.Append("  		MstTerminalID As Terminal On CMH.TerminalID = Terminal.TerminalID Inner Join   ")
            vStmtQry.Append("  		PrepStationOrderDtl_KOTWise AS CMD ON CMH.siteCode = CMD.SiteCode AND CMH.billNo = CMD.billNo  Inner Join    ")
            vStmtQry.Append("  	    MstArticle AS art on CMD.ArticleCode = art.ArticleCode Left Join   ")
            vStmtQry.Append("  	    CashMemoDtlItemRemark AS CMDIR On  CMD.SiteCode = CMDIR.SiteCode AND CMD.FinYear= CMDIR.FinYear   ")
            vStmtQry.Append("  					      	AND CMD.BillNo = CMDIR.BillNo AND CMD.BillLineNo = CMDIR.BillLineNo   ")
            vStmtQry.Append("  							AND CMD.EAN	= CMDIR.EAN AND CMD.ArticleCode = CMDIR.ArticleCode   ")
            vStmtQry.Append("INNER JOIN (select  BILLNO,EAN ,KOTNO,SUM(KOTQUANTITY) 'QTY' from DineInKotQtyMap  group by BILLNO,EAN,KOTNO ) #KOT on #KOT.Billno = CMD.Billno AND #KOT.EAN =  CMD.EAN  AND #KOT.KOTNO =  CMD.KOTNO")
            vStmtQry.Append("  WHERE  ISNULL(CMD.isItemReady,0) = 1  ")
            vStmtQry.Append("  	     AND CMH.siteCode ='" & siteCode & "' ")
            vStmtQry.Append("  	     AND CMD.mstPrepStationID ='" & PrepStationID & "' ")
            vStmtQry.Append("  	     AND CMH.billDate = @DayOpenDate ")
            vStmtQry.Append("  	     AND  CMD.ArticleCode IS NOT NULL ")

            vStmtQry.Append("  	    UNION ")
            vStmtQry.Append("  SELECT  CMH.BillNo, CMD.CREATEDON,Terminal.TerminalName,art.ArticleName ,art.ArticleShortName ,ISNULL(CMDIR.itemRemarks,'') AS Remark , CMD.Quantity, ")
            vStmtQry.Append("  '' AS OrderTitle,CMD.articleCode ,dbo.ListOfKitArticle(CMD.articleCode)  'KitArticle',  CMD.KotNO,CMD.quantity,CMD.TableNo   ")
            vStmtQry.Append("  FROM    PrepStationOrderHdr_KOTWise AS CMH Inner Join   ")
            vStmtQry.Append("  		MstTerminalID As Terminal On CMH.TerminalID = Terminal.TerminalID Inner Join   ")
            vStmtQry.Append("  		PrepStationOrderDtl_KOTWise AS CMD ON CMH.siteCode = CMD.SiteCode AND CMH.billNo = CMD.billNo  Inner Join    ")
            vStmtQry.Append("  	    MstArticle AS art on CMD.ArticleCode = art.ArticleCode Left Join   ")
            vStmtQry.Append("  	    CashMemoDtlItemRemark AS CMDIR On  CMD.SiteCode = CMDIR.SiteCode AND CMD.FinYear= CMDIR.FinYear   ")
            vStmtQry.Append("  					      	AND CMD.BillNo = CMDIR.BillNo AND CMD.BillLineNo = CMDIR.BillLineNo   ")
            vStmtQry.Append("  							AND CMD.EAN	= CMDIR.EAN AND CMD.ArticleCode = CMDIR.ArticleCode   ")
            ' vStmtQry.Append("INNER JOIN (select  BILLNO,EAN ,KOTNO,SUM(KOTQUANTITY) 'QTY' from DineInKotQtyMap  group by BILLNO,EAN,KOTNO ) #KOT on #KOT.Billno = CMD.Billno AND #KOT.EAN =  CMD.EAN  AND #KOT.KOTNO =  CMD.KOTNO")
            vStmtQry.Append("  WHERE  ISNULL(CMD.isItemReady,0) = 1  ")
            vStmtQry.Append("  	     AND CMH.siteCode ='" & siteCode & "' ")
            vStmtQry.Append("  	     AND CMD.mstPrepStationID ='" & PrepStationID & "' AND CMD.TableNo in ('Home Delivery','Take Away')  ")
            vStmtQry.Append("  	     AND CMH.billDate = @DayOpenDate ")
            vStmtQry.Append("  	     AND  CMD.ArticleCode IS NOT NULL )")
            vStmtQry.Append(" order by CMD.CreatedOn")
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
    Public Function GetNewBills(ByVal siteCode As String, ByVal PrepStationID As String, DayOpenDate As Date, ByRef LastBillStampTime As Nullable(Of DateTime)) As DataTable
        Try
            Dim vStmtQry As New System.Text.StringBuilder
            Dim DsNewBills As New DataSet
            vStmtQry.Append(" SELECT GETDATE() As ServerDateTime ;")

            vStmtQry.Append("  SELECT  CMH.BillNo, CMH.CREATEDON,Terminal.TerminalName,art.ArticleName ,art.ArticleShortName ,ISNULL(CMDIR.itemRemarks,'') AS Remark , CMD.Quantity, ")
            vStmtQry.Append("  '' AS OrderTitle,CMD.articleCode ,dbo.ListOfKitArticle(CMD.articleCode)  'KitArticle'  ")
            vStmtQry.Append("  FROM    PrepStationOrderHdr AS CMH Inner Join   ")
            vStmtQry.Append("  		MstTerminalID As Terminal On CMH.TerminalID = Terminal.TerminalID Inner Join   ")
            vStmtQry.Append("  		PrepStationOrderDtl AS CMD ON CMH.siteCode = CMD.SiteCode AND CMH.billNo = CMD.billNo  Inner Join    ")
            vStmtQry.Append("  	    MstArticle AS art on CMD.ArticleCode = art.ArticleCode Left Join   ")
            vStmtQry.Append("  	    CashMemoDtlItemRemark AS CMDIR On  CMD.SiteCode = CMDIR.SiteCode AND CMD.FinYear= CMDIR.FinYear   ")
            vStmtQry.Append("  					      	AND CMD.BillNo = CMDIR.BillNo AND CMD.BillLineNo = CMDIR.BillLineNo   ")
            vStmtQry.Append("  							AND CMD.EAN	= CMDIR.EAN AND CMD.ArticleCode = CMDIR.ArticleCode   ")
            vStmtQry.Append("  WHERE  ISNULL(CMH.isOrderReady,0) = 0 AND ISNULL(CMD.isItemReady,0) = 0   ")
            vStmtQry.Append("  	     AND CMH.siteCode ='" & siteCode & "' ")
            vStmtQry.Append("  	     AND CMD.mstPrepStationID ='" & PrepStationID & "' ")
            vStmtQry.Append("  	     AND CMH.billDate = @DayOpenDate ")
            vStmtQry.Append("  	     AND  CMD.ArticleCode IS NOT NULL ")

            If LastBillStampTime IsNot Nothing Then
                vStmtQry.Append(" AND   CMH.CREATEDON >= @LastBillStampTime ")
            End If
            vStmtQry.Append(" order by CMH.createdOn ")
            Dim cmd As New SqlCommand(vStmtQry.ToString, SpectrumCon)
            cmd.Parameters.AddWithValue("@DayOpenDate", DayOpenDate)
            If LastBillStampTime IsNot Nothing Then
                cmd.Parameters.AddWithValue("@LastBillStampTime", LastBillStampTime)
            End If

            daCM = New SqlDataAdapter(cmd)
            daCM.Fill(DsNewBills)

            LastBillStampTime = DsNewBills.Tables(0).Rows(0)(0)

            Return DsNewBills.Tables(1)

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetRetriveBills(ByVal siteCode As String, ByVal PrepStationID As String, DayOpenDate As Date) As DataTable
        Try
            Dim vStmtQry As New System.Text.StringBuilder
            Dim DsNewBills As New DataTable

            vStmtQry.Append("  SELECT  CMH.BillNo, CMH.CREATEDON,Terminal.TerminalName,art.ArticleName ,art.ArticleShortName ,ISNULL(CMDIR.itemRemarks,'') AS Remark , CMD.Quantity, ")
            vStmtQry.Append("  '' AS OrderTitle,CMD.articleCode ,dbo.ListOfKitArticle(CMD.articleCode)  'KitArticle'    ")
            vStmtQry.Append("  FROM    PrepStationOrderHdr AS CMH Inner Join   ")
            vStmtQry.Append("  		MstTerminalID As Terminal On CMH.TerminalID = Terminal.TerminalID Inner Join   ")
            vStmtQry.Append("  		PrepStationOrderDtl AS CMD ON CMH.siteCode = CMD.SiteCode AND CMH.billNo = CMD.billNo  Inner Join    ")
            vStmtQry.Append("  	    MstArticle AS art on CMD.ArticleCode = art.ArticleCode Left Join   ")
            vStmtQry.Append("  	    CashMemoDtlItemRemark AS CMDIR On  CMD.SiteCode = CMDIR.SiteCode AND CMD.FinYear= CMDIR.FinYear   ")
            vStmtQry.Append("  					      	AND CMD.BillNo = CMDIR.BillNo AND CMD.BillLineNo = CMDIR.BillLineNo   ")
            vStmtQry.Append("  							AND CMD.EAN	= CMDIR.EAN AND CMD.ArticleCode = CMDIR.ArticleCode   ")
            vStmtQry.Append("  WHERE  ISNULL(CMH.isOrderReady,0) = 0 AND ISNULL(CMD.isItemReady,0) = 1  ")

            vStmtQry.Append("  	     AND CMH.siteCode ='" & siteCode & "' ")
            vStmtQry.Append("  	     AND CMD.mstPrepStationID ='" & PrepStationID & "' ")
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

    Public Function UpdateItemReadyKOTWise(BillNo As String, ItemCode As String, KotNo As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            Dim vStmtQry As New System.Text.StringBuilder
            vStmtQry.Length = 0

            vStmtQry.Append(" Update  PrepStationOrderDtl_KOTWise ")
            vStmtQry.Append(" SET     isItemReady = 1 ,UpdatedOn =GetDate() ")
            vStmtQry.Append(" Where   BillNo = '" & BillNo & "' AND ArticleCode = '" & ItemCode & "' AND KOTNO= '" & KotNo & "'; ")

            vStmtQry.Append("IF NOT EXISTS (select * from PrepStationOrderDtl_KOTWise where  Billno = '" & BillNo & "' And IsItemReady =0)")
            vStmtQry.Append(" BEGIN UPDATE PrepStationOrderhdr_KOTWise SET isOrderReady = 1,UpdatedOn =GetDate()  where  Billno = '" & BillNo & "' END ")
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
    Public Function UpdateItemRetriveKOtWise(BillNo As String, ItemCode As String, KotNo As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            Dim vStmtQry As New System.Text.StringBuilder
            vStmtQry.Length = 0

            vStmtQry.Append(" Update  PrepStationOrderDtl_KOTWise ")
            vStmtQry.Append(" SET     isItemReady = 0 ,UpdatedOn =GetDate() ")
            vStmtQry.Append(" Where   BillNo = '" & BillNo & "' AND ArticleCode = '" & ItemCode & "' AND KOTNO ='" & KotNo & "';")
            vStmtQry.Append(" UPDATE PrepStationOrderhdr_KOTWise SET  isOrderReady = 0,UpdatedOn =GetDate()  where  Billno = '" & BillNo & "';  ")
            ' vStmtQry.Append("IF  EXISTS (select * from PrepStationOrderDtl_KOTWise where  Billno = '" & BillNo & "' And IsItemReady =1)")
            'vStmtQry.Append(" BEGIN UPDATE PrepStationOrderhdr_KOTWise SET isOrderReady = 0,UpdatedOn =GetDate()  where  Billno = '" & BillNo & "' END ")

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
    Public Function UpdateItemReady(BillNo As String, ItemCode As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            Dim vStmtQry As New System.Text.StringBuilder
            vStmtQry.Length = 0

            vStmtQry.Append(" Update  PrepStationOrderDtl ")
            vStmtQry.Append(" SET     isItemReady = 1 ,UpdatedOn =GetDate() ")
            vStmtQry.Append(" Where   BillNo = '" & BillNo & "' AND ArticleCode = '" & ItemCode & "'")

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

    Public Function UpdateItemRetrive(BillNo As String, ItemCode As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            Dim vStmtQry As New System.Text.StringBuilder
            vStmtQry.Length = 0

            vStmtQry.Append(" Update  PrepStationOrderDtl ")
            vStmtQry.Append(" SET     isItemReady = 0 ,UpdatedOn =GetDate() ")
            vStmtQry.Append(" Where   BillNo = '" & BillNo & "' AND ArticleCode = '" & ItemCode & "'")

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
    Public Function CheckBillStatus(ByVal SiteCode As String, ByVal BillNo As String) As Boolean
        Dim strResult As Boolean = False
        Try
            Dim dt As New DataTable
            Dim sqlComm As New SqlCommand("CheckBillStatus", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@P_SITECODE", SiteCode)
            sqlComm.Parameters.AddWithValue("@P_BillNO", BillNo)
            sqlComm.CommandTimeout = 0
            sqlComm.CommandType = CommandType.StoredProcedure
            dt = New DataTable
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dt)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    strResult = dt.Rows(0)(0).ToString()
                    Return strResult
                End If
            End If
            Return strResult
        Catch ex As Exception
            LogException(ex)
            Return strResult
        End Try
    End Function

#End Region
End Class
