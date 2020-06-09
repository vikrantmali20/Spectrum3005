Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
''' <summary>
''' This Class is Used for Iteam Search
''' </summary>
''' <CreatedBy>Rama Ranjan Jena</CreatedBy>
''' <UpdatedBy></UpdatedBy>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
Public Class clsIteamSearch
    Inherits clsCommon
#Region "Global Variable for Class"
    Dim daSql As SqlDataAdapter
    Dim dtSearch As New DataTable
    Dim vstmtqry As String = ""
    Dim vSqlQry As New StringBuilder
#End Region
#Region "Public Function & Method's"
    Public Function GetItemData(Optional ByVal Item As String = "", Optional ByVal gLngCode As String = "") As DataTable
        Try
            vstmtqry = ""

            'Commented by Rohit for CR-5773

            'vstmtqry = " SELECT H.EAN,ISNULL( Z.ARTICLESHORTNAME,A.ARTICLESHORTNAME) AS DISCRIPTION,A.ARTICLECODE,A.SUPPLIERREF,A.BASEUNITOFMEASURE AS UNITOFMEASURE,D.NODENAME,B.SELLINGPRICE,DBO.GETCHAROFARTICLE(A.ARTICLECODE) AS CHARS, IsNull(A.IsMrpOpen ,0) as IsMrpOpen "
            'vstmtqry = vstmtqry & " FROM MSTEAN H INNER JOIN  MSTARTICLE A ON H.ARTICLECODE=A.ARTICLECODE LEFT Outer JOIN SalesInfoRecord B ON H.EAN=B.EAN "
            ''vstmtqry = vstmtqry & " INNER JOIN ARTICLENODEMAP C ON A.ARTICLECODE=C.ARTICLECODE INNER JOIN MSTARTICLENODE D ON C.LASTNODECODE=D.NODECODE "
            'vstmtqry = vstmtqry & " INNER JOIN ARTICLENODEMAP C ON A.ARTICLECODE=C.ARTICLECODE INNER JOIN MSTARTICLENODE D ON C.LASTNODECODE=D.NODECODE "
            'vstmtqry = vstmtqry & " LEFT OUTER JOIN ARTICLEDESCINDIFFLANG Z ON A.ARTICLECODE = Z.ARTICLECODE AND Z.LANGUAGECODE = '" & gLngCode & "' "

            'Added by Rohit for CR-5773

            vstmtqry = " SELECT H.EAN,ISNULL( Z.ARTICLESHORTNAME,A.ARTICLESHORTNAME) AS DISCRIPTION,A.ARTICLECODE,A.SUPPLIERREF,A.BASEUNITOFMEASURE AS UNITOFMEASURE,(CASE WHEN EL.TRANSLATEDTEXT IS NULL  THEN D.NODENAME WHEN EL.TRANSLATEDTEXT IS NOT NULL AND EL.LANGUAGECODE<>' " & strLangCode & "' THEN D.NODENAME ELSE EL.TRANSLATEDTEXT END )as NODENAME,B.SELLINGPRICE,DBO.GETCHAROFARTICLE(H.EAN) AS CHARS, IsNull(A.IsMrpOpen ,0) as IsMrpOpen "
            vstmtqry = vstmtqry & " FROM MSTEAN H INNER JOIN  MSTARTICLE A ON H.ARTICLECODE=A.ARTICLECODE LEFT Outer JOIN SalesInfoRecord B ON H.EAN=B.EAN "
            'vstmtqry = vstmtqry & " INNER JOIN ARTICLENODEMAP C ON A.ARTICLECODE=C.ARTICLECODE INNER JOIN MSTARTICLENODE D ON C.LASTNODECODE=D.NODECODE "
            vstmtqry = vstmtqry & " INNER JOIN ARTICLENODEMAP C ON A.ARTICLECODE=C.ARTICLECODE INNER JOIN MSTARTICLENODE D ON C.LASTNODECODE=D.NODECODE "
            vstmtqry = vstmtqry & " LEFT OUTER JOIN ARTICLEDESCINDIFFLANG Z ON A.ARTICLECODE = Z.ARTICLECODE AND Z.LANGUAGECODE = '" & gLngCode & "' "
            vstmtqry = vstmtqry & " LEFT JOIN ElementTranslation EL ON D.NODECODE=EL.ELEMENTCODE AND EL.LANGUAGECODE = '" & strLangCode & "' "
            vstmtqry = vstmtqry & " WHERE isnull(A.SALABLE,0)=1"
            If Not (Item = String.Empty) Then
                vstmtqry = vstmtqry & " AND A.ArticleCode = '" & Item & "'"
            End If
            'daSql = New SqlDataAdapter("SELECT A.ARTICLECODE,A.ARTICLESHORTNAME AS DISCRIPTION,A.SUPPLIERREF,D.NODENAME,B.PRICE,DBO.GETCHAROFARTICLE(A.ARTICLECODE) AS CHARS " & _
            '                          " FROM MSTARTICLE A INNER JOIN PricingCondition B ON A.ARTICLECODE=B.ARTICLECODE " & _
            '                           " INNER JOIN ARTICLENODEMAP C ON A.ARTICLECODE=C.ARTICLECODE INNER JOIN MSTARTICLENODE D " & _
            '                           " ON C.LASTNODECODE=D.NODECODE ", SpectrumCon)
            daSql = New SqlDataAdapter(vstmtqry, SpectrumCon)
            dtSearch.Clear()
            daSql.Fill(dtSearch)
            Return dtSearch
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetEANDataforLablePrint(ByVal pCurrentSiteCode As String, Optional ByVal vEAN As String = "", Optional ByVal gLngCode As String = "", Optional ByVal ExtraCondition As String = "", Optional ByRef DtTemp As DataTable = Nothing, Optional ByVal isDayClose As Boolean = False, Optional isBatchManagementReq As Boolean = False, Optional batchBarcode As String = "", Optional ByVal ArticleMatrixApplicable As Boolean = False) As DataTable
        Try
            'If Not DtTemp Is Nothing Then
            '    If vEAN <> String.Empty Then
            '        Dim dv As New DataView(DtTemp, "EAN = '" & vEAN & "' OR ArticleCode='" & vEAN & "' OR LegacyArticleCode ='" & vEAN & "'", "", DataViewRowState.CurrentRows)
            '        dtSearch = dv.ToTable()
            '        Return dtSearch
            '    End If
            'End If
            vstmtqry = ""
            Dim checkBoxColumn As String = String.Empty
            Dim barCodeColumn As String = String.Empty
            If isDayClose Then
                checkBoxColumn = "CONVERT(BIT,0) AS [SELECT],"
            End If
            If isBatchManagementReq Then
                barCodeColumn = "'" & batchBarcode & "' As BatchBarcode,"
            Else
                barCodeColumn = " CONVERT(varchar(50),NULL)  As BatchBarcode,"
            End If
            'vstmtqry = " SELECT A.ARTICLECODE,ISNULL( Z.ARTICLESHORTNAME,A.ARTICLESHORTNAME) AS DISCRIPTION,H.EAN,A.Season,A.Theme,A.SUPPLIERREF, A.ArticleName AS FullName,Convert(varchar,B.SELLINGPRICE) as SELLINGPRICE,Convert(varchar,E.TotalPhysicalSaleableQty-(e.ReservedQty+e.TotalPhysicalNonSaleableQty)) AS  AvailableQty,Convert(Varchar,E.OnOrderQty) AS OrderQty,Convert(Varchar,E.TotalPhysicalSaleableQty) as PhysicalQty, IsNull(A.IsMrpOpen ,0) as IsMrpOpen, A.SaleUnitOfMeasure as UOM,D.NodeName ,isNull(H.DefaultEAN,0) as DefaultEAN,isnull(B.FreezeSB,0) as FreezeSB,isnull(B.FreezeOB,0) as FreezeOB,isnull(B.FreezeSR,0) as FreezeSR ,H.Discription AS EanType, A.LastNodeCode AS Nodes "
            'vstmtqry = vstmtqry & " FROM MSTEAN H with (nolock) INNER JOIN MSTARTICLE A with (nolock) ON H.ARTICLECODE=A.ARTICLECODE INNER JOIN SalesInfoRecord B with (nolock) ON H.EAN=B.EAN "
            ''vstmtqry = vstmtqry & " INNER JOIN ARTICLENODEMAP C ON A.ARTICLECODE=C.ARTICLECODE INNER JOIN MSTARTICLENODE D ON C.LASTNODECODE=D.NODECODE LEFT OUTER JOIN ARTICLESTOCKBALANCES E ON A.ARTICLECODE=E.ARTICLECODE AND H.EAN=E.EAN WHERE isnull(A.SALABLE,0)=1  "
            'vstmtqry = vstmtqry & " INNER JOIN ARTICLENODEMAP  C with (nolock) ON A.ARTICLECODE=C.ARTICLECODE INNER JOIN MSTARTICLENODE D with (nolock) ON C.LASTNODECODE=D.NODECODE LEFT OUTER JOIN ARTICLESTOCKBALANCES E with (nolock) ON A.ARTICLECODE=E.ARTICLECODE AND H.EAN=E.EAN "



            'Commented by Rohit for CR-5773

            ' vstmtqry = "SELECT A.ARTICLECODE,ISNULL( Z.ARTICLESHORTNAME,A.ARTICLESHORTNAME) AS DISCRIPTION,H.EAN,A.Season,A.Theme,A.SUPPLIERREF, " & _
            '" A.ArticleName AS FullName,Convert(varchar,B.SELLINGPRICE) as SELLINGPRICE,Convert(varchar,E.TotalPhysicalSaleableQty-(e.ReservedQty+e.TotalPhysicalNonSaleableQty)) AS  AvailableQty," & _
            '" Convert(Varchar,E.OnOrderQty) AS OrderQty,Convert(Varchar,E.TotalPhysicalSaleableQty) as PhysicalQty, IsNull(A.IsMrpOpen ,0) as IsMrpOpen, A.SaleUnitOfMeasure as UOM, " & _
            '" D.NodeName ,isNull(H.DefaultEAN,0) as DefaultEAN,isnull(B.FreezeSB,0) as FreezeSB,isnull(B.FreezeOB,0) as FreezeOB,isnull(B.FreezeSR,0) as FreezeSR ,H.Discription AS EanType, A.LastNodeCode AS Nodes  " & _
            '" FROM MSTARTICLE A with (nolock)  INNER JOIN MSTEAN H with (nolock) ON A.ARTICLECODE=H.ARTICLECODE AND A.ArticleActive=1 " & _
            '" INNER JOIN 	SalesInfoRecord B with (nolock) ON B.SiteCode='" & pCurrentSiteCode & "' AND B.SrNo=1 AND H.EAN=B.EAN  INNER JOIN MSTARTICLENODE D with (nolock)" & _
            '" ON A.LASTNODECODE=D.NODECODE LEFT OUTER JOIN ARTICLESTOCKBALANCES E with (nolock) ON E.Sitecode='" & pCurrentSiteCode & "' AND A.ARTICLECODE=E.ARTICLECODE AND H.EAN=E.EAN  "

            'Added by Rohit for CR-5773

            'Changed by Rohit for Issue No. 00006101 - Removed varchar conversion of all numeric values

            vstmtqry = "SELECT distinct " & checkBoxColumn & " " & barCodeColumn & "A.ARTICLECODE,A.ArticalTypeCode ,A.LegacyArticleCode, ISNULL( Z.ARTICLESHORTNAME,A.ARTICLESHORTNAME) AS DISCRIPTION, "
            vstmtqry = vstmtqry + "H.EAN+'  '+  A.ARTICLECODE + REPLICATE(' ', CASE WHEN LEN(A.ARTICLECODE)> 10 THEN 0 ELSE ( case when (10-LEN(A.ARTICLECODE))  > 0 THEN (10-LEN(A.ARTICLECODE)) ELSE 0 END)  END )  +' '+"
            '  If ArticleMatrixApplicable Then

            ' End If
            vstmtqry = vstmtqry & "  ISNULL( Z.ARTICLESHORTNAME,A.ARTICLESHORTNAME)  AS ArticleCodeDesc,  H.EAN,A.Season,A.Theme,A.SUPPLIERREF, " & _
                       " A.ArticleName AS FullName,B.SELLINGPRICE as SELLINGPRICE,(E.TotalPhysicalSaleableQty-e.ReservedQty) AS  AvailableQty," & _
                       " E.OnOrderQty AS OrderQty,E.TotalPhysicalSaleableQty as PhysicalQty, IsNull(A.IsMrpOpen ,0) as IsMrpOpen, A.SaleUnitOfMeasure as UOM, " & _
                       " (CASE WHEN EL.TRANSLATEDTEXT IS NULL  THEN D.NodeName WHEN EL.TRANSLATEDTEXT IS NOT NULL AND EL.LANGUAGECODE<>' " & strLangCode & "' THEN D.NodeName ELSE EL.TRANSLATEDTEXT END )as NodeName ,isNull(H.DefaultEAN,0) as DefaultEAN,isnull(B.FreezeSB,0) as FreezeSB,isnull(B.FreezeOB,0) as FreezeOB,isnull(B.FreezeSR,0) as FreezeSR ,H.Discription AS EanType, A.LastNodeCode AS Nodes ,AM.CPBaseCurr as CostAmt "
            If ArticleMatrixApplicable Then
                vstmtqry = vstmtqry & ", #CharTbl.[Action] as Charactrestic "
            End If
            vstmtqry = vstmtqry & " FROM MSTARTICLE A with (nolock)  INNER JOIN MSTEAN H with (nolock) ON A.ARTICLECODE=H.ARTICLECODE AND A.ArticleActive=1 " & _
                       " INNER JOIN 	SalesInfoRecord B with (nolock) ON B.SiteCode='" & pCurrentSiteCode & "' AND B.SrNo=1 AND H.EAN=B.EAN  INNER JOIN MSTARTICLENODE D with (nolock)" & _
                       " ON A.LASTNODECODE=D.NODECODE LEFT OUTER JOIN (SELECT SiteCode ,ArticleCode,SUM(TotalPhysicalSaleableQty)TotalPhysicalSaleableQty,SUM(ReservedQty)ReservedQty,SUM(TotalPhysicalNonSaleableQty)TotalPhysicalNonSaleableQty,SUM(OnOrderQty)OnOrderQty FROM ARTICLESTOCKBALANCES GROUP BY Sitecode ,ARTICLECODE) E ON E.Sitecode='" & pCurrentSiteCode & "' AND A.ARTICLECODE=E.ARTICLECODE " & _
                       " LEFT JOIN ElementTranslation EL ON D.NODECODE=EL.ELEMENTCODE AND EL.LANGUAGECODE = '" & strLangCode & "' "

            vstmtqry = vstmtqry & " LEFT OUTER JOIN ARTICLEDESCINDIFFLANG Z with (nolock) ON A.ARTICLECODE = Z.ARTICLECODE AND Z.LANGUAGECODE = '" & gLngCode & "' "
            'vstmtqry = vstmtqry & " LEFT OUTER JOIN ArticleMAP AM on am.EAN=H.EAN "
            vstmtqry = vstmtqry & " LEFT OUTER JOIN PurchaseInfoRecord  AM on am.EAN=H.EAN and am.IsDefaultSupplier ='1' and am.ArticleCode =H.ArticleCode  AND AM.sitecode =b.sitecode"
            If ArticleMatrixApplicable Then
                vstmtqry = vstmtqry & " LEFT JOIN ( SELECT   A2.eancode , action = replace  ((SELECT charactrestic AS [data()]  FROM (select AM.eancode, (MC.CharName+':'+AM.CharValue+',') as charactrestic  "
                vstmtqry = vstmtqry & "  from ArticleMatrix AM INNER JOIN MstCharacteristics MC ON AM.CharCode=MC.CharCode) A1   WHERE  A1.eancode =  a2.EanCode "
                vstmtqry = vstmtqry & "  ORDER BY  A1.eancode  FOR xml path('')), ',',CHAR(10)+''+CHAR(13)) FROM (select distinct EanCode  from ArticleMatrix ) A2 ) #CharTbl on #CharTbl.EanCode = h.EAN "
            End If
            If isDayClose Then
                vstmtqry = vstmtqry & " WHERE A.ArticleCode not in (select MasterArticleCode  from MasterArticleMap) "
                vstmtqry = vstmtqry & " And E.Sitecode = '" & pCurrentSiteCode & "' AND B.SITECODE='" & pCurrentSiteCode & "' And isnull(B.SRNO,0)=1"
            Else
                vstmtqry = vstmtqry & " WHERE isnull(A.SALABLE,0)=1  "
                vstmtqry = vstmtqry & " AND E.Sitecode = '" & pCurrentSiteCode & "' AND B.SITECODE='" & pCurrentSiteCode & "' And isnull(B.SRNO,0)=1"
            End If
            If ArticleMatrixApplicable = False Then
                If (vEAN = String.Empty) Then
                    ' vstmtqry = vstmtqry & " AND ( H.DEFAULTEAN = 1 ) "
                End If
            End If
            If Not (vEAN = String.Empty) Then
                'vstmtqry = vstmtqry & " AND ( H.EAN = '" & vEAN & "' OR A.ArticleCode='" & vEAN & "')"
                ' for optimization OR A.ArticleCode='"
                vstmtqry = vstmtqry & " AND ( H.EAN = '" & vEAN & "' OR H.ArticleCode='" & vEAN & "' OR LegacyArticleCode ='" & vEAN & "')"
            End If
            If ExtraCondition <> String.Empty Then
                vstmtqry = vstmtqry & ExtraCondition
            End If
Redo:
            daSql = New SqlDataAdapter(vstmtqry, ConString)
            dtSearch.Clear()
            daSql.Fill(dtSearch)
            'If vEAN = String.Empty And (DtTemp) Is Nothing Then
            '    DtTemp = dtSearch.Copy()
            'End If
            Return dtSearch
        Catch ex As Exception
            'If DirectCast(ex, System.Data.SqlClient.SqlException).ErrorCode = "-2146232060" Then
            '    GoTo redo
            'End If
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetArticleCodeFromBarcode(ByVal pCurrentSiteCode As String, ByVal vBarcode As String) As String
        Try
            Dim articleCode As String = String.Empty
            Dim query As String = "select ArticleCode from BatchDtl where SiteCode='" & pCurrentSiteCode & "' and BatchBarcode = '" & vBarcode & "'"
            Dim dt As DataTable = GetFilledTable(query)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                articleCode = dt.Rows(0)(0)
            End If
            Return articleCode
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function

    Public Function GetExistingOutBoundBarcode(ByVal pCurrentSiteCode As String, ByVal vBarcode As String) As String
        Try
            Dim articleCode As String = String.Empty
            Dim query As String = "select ArticleCode from BatchDtl where SiteCode='" & pCurrentSiteCode & "' and BatchBarcode = '" & vBarcode & "'"
            Dim dt As DataTable = GetFilledTable(query)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                articleCode = dt.Rows(0)(0)
            End If
            Return articleCode
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function

    Public Function GetArticleCodeFromEAN(ByRef ean As String) As String
        Try
            Dim articleCode As String = String.Empty
            Dim query As String = "select ArticleCode  from MstEAN where EAN='" & ean & "'"
            Dim dt As DataTable = GetFilledTable(query)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                articleCode = dt.Rows(0)(0)
            End If
            Return articleCode
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function

    Public Function GetTorelanceValFromArticleCode(ByVal ArticleCode As String) As Boolean
        Try
            Dim tolerance As Boolean = False
            Dim query As String = "select ToleranceValue from mstarticle where ArticleCode='" & ArticleCode & "'"
            Dim dt As DataTable = GetFilledTable(query)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                tolerance = If(dt.Rows(0)(0) Is DBNull.Value, False, dt.Rows(0)(0))
            End If
            Return tolerance
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function

    Public Function GetEANData(ByVal pCurrentSiteCode As String, Optional ByVal vEAN As String = "", Optional ByVal gLngCode As String = "", Optional ByVal ExtraCondition As String = "", Optional ByRef DtTemp As DataTable = Nothing, Optional ByVal isDayClose As Boolean = False, Optional isBatchManagementReq As Boolean = False, Optional batchBarcode As String = "") As DataTable
        Try
            'If Not DtTemp Is Nothing Then
            '    If vEAN <> String.Empty Then
            '        Dim dv As New DataView(DtTemp, "EAN = '" & vEAN & "' OR ArticleCode='" & vEAN & "' OR LegacyArticleCode ='" & vEAN & "'", "", DataViewRowState.CurrentRows)
            '        dtSearch = dv.ToTable()
            '        Return dtSearch
            '    End If
            'End If
            vstmtqry = ""
            Dim checkBoxColumn As String = String.Empty
            Dim barCodeColumn As String = String.Empty
            If isDayClose Then
                checkBoxColumn = "CONVERT(BIT,0) AS [SELECT],"
            End If
            If isBatchManagementReq Then
                barCodeColumn = "'" & batchBarcode & "' As BatchBarcode,"
            Else
                barCodeColumn = " CONVERT(varchar(50),NULL)  As BatchBarcode,"
            End If
            'vstmtqry = " SELECT A.ARTICLECODE,ISNULL( Z.ARTICLESHORTNAME,A.ARTICLESHORTNAME) AS DISCRIPTION,H.EAN,A.Season,A.Theme,A.SUPPLIERREF, A.ArticleName AS FullName,Convert(varchar,B.SELLINGPRICE) as SELLINGPRICE,Convert(varchar,E.TotalPhysicalSaleableQty-(e.ReservedQty+e.TotalPhysicalNonSaleableQty)) AS  AvailableQty,Convert(Varchar,E.OnOrderQty) AS OrderQty,Convert(Varchar,E.TotalPhysicalSaleableQty) as PhysicalQty, IsNull(A.IsMrpOpen ,0) as IsMrpOpen, A.SaleUnitOfMeasure as UOM,D.NodeName ,isNull(H.DefaultEAN,0) as DefaultEAN,isnull(B.FreezeSB,0) as FreezeSB,isnull(B.FreezeOB,0) as FreezeOB,isnull(B.FreezeSR,0) as FreezeSR ,H.Discription AS EanType, A.LastNodeCode AS Nodes "
            'vstmtqry = vstmtqry & " FROM MSTEAN H with (nolock) INNER JOIN MSTARTICLE A with (nolock) ON H.ARTICLECODE=A.ARTICLECODE INNER JOIN SalesInfoRecord B with (nolock) ON H.EAN=B.EAN "
            ''vstmtqry = vstmtqry & " INNER JOIN ARTICLENODEMAP C ON A.ARTICLECODE=C.ARTICLECODE INNER JOIN MSTARTICLENODE D ON C.LASTNODECODE=D.NODECODE LEFT OUTER JOIN ARTICLESTOCKBALANCES E ON A.ARTICLECODE=E.ARTICLECODE AND H.EAN=E.EAN WHERE isnull(A.SALABLE,0)=1  "
            'vstmtqry = vstmtqry & " INNER JOIN ARTICLENODEMAP  C with (nolock) ON A.ARTICLECODE=C.ARTICLECODE INNER JOIN MSTARTICLENODE D with (nolock) ON C.LASTNODECODE=D.NODECODE LEFT OUTER JOIN ARTICLESTOCKBALANCES E with (nolock) ON A.ARTICLECODE=E.ARTICLECODE AND H.EAN=E.EAN "



            'Commented by Rohit for CR-5773

            ' vstmtqry = "SELECT A.ARTICLECODE,ISNULL( Z.ARTICLESHORTNAME,A.ARTICLESHORTNAME) AS DISCRIPTION,H.EAN,A.Season,A.Theme,A.SUPPLIERREF, " & _
            '" A.ArticleName AS FullName,Convert(varchar,B.SELLINGPRICE) as SELLINGPRICE,Convert(varchar,E.TotalPhysicalSaleableQty-(e.ReservedQty+e.TotalPhysicalNonSaleableQty)) AS  AvailableQty," & _
            '" Convert(Varchar,E.OnOrderQty) AS OrderQty,Convert(Varchar,E.TotalPhysicalSaleableQty) as PhysicalQty, IsNull(A.IsMrpOpen ,0) as IsMrpOpen, A.SaleUnitOfMeasure as UOM, " & _
            '" D.NodeName ,isNull(H.DefaultEAN,0) as DefaultEAN,isnull(B.FreezeSB,0) as FreezeSB,isnull(B.FreezeOB,0) as FreezeOB,isnull(B.FreezeSR,0) as FreezeSR ,H.Discription AS EanType, A.LastNodeCode AS Nodes  " & _
            '" FROM MSTARTICLE A with (nolock)  INNER JOIN MSTEAN H with (nolock) ON A.ARTICLECODE=H.ARTICLECODE AND A.ArticleActive=1 " & _
            '" INNER JOIN 	SalesInfoRecord B with (nolock) ON B.SiteCode='" & pCurrentSiteCode & "' AND B.SrNo=1 AND H.EAN=B.EAN  INNER JOIN MSTARTICLENODE D with (nolock)" & _
            '" ON A.LASTNODECODE=D.NODECODE LEFT OUTER JOIN ARTICLESTOCKBALANCES E with (nolock) ON E.Sitecode='" & pCurrentSiteCode & "' AND A.ARTICLECODE=E.ARTICLECODE AND H.EAN=E.EAN  "

            'Added by Rohit for CR-5773

            'Changed by Rohit for Issue No. 00006101 - Removed varchar conversion of all numeric values

            vstmtqry = "SELECT distinct " & checkBoxColumn & " " & barCodeColumn & "A.ARTICLECODE,A.ArticalTypeCode ,A.LegacyArticleCode, ISNULL( Z.ARTICLESHORTNAME,A.ARTICLESHORTNAME) AS DISCRIPTION,  A.ARTICLECODE + REPLICATE(' ', CASE WHEN LEN(A.ARTICLECODE)> 10 THEN 0 ELSE ( case when (10-LEN(A.ARTICLECODE))  > 0 THEN (10-LEN(A.ARTICLECODE)) ELSE 0 END)  END ) + + ' ' + ISNULL( Z.ARTICLESHORTNAME,A.ARTICLESHORTNAME)  AS ArticleCodeDesc,  H.EAN,A.Season,A.Theme,A.SUPPLIERREF, " & _
            " A.ArticleName AS FullName,B.SELLINGPRICE as SELLINGPRICE,(E.TotalPhysicalSaleableQty-e.ReservedQty) AS  AvailableQty," & _
            " E.OnOrderQty AS OrderQty,E.TotalPhysicalSaleableQty as PhysicalQty, IsNull(A.IsMrpOpen ,0) as IsMrpOpen, A.SaleUnitOfMeasure as UOM, " & _
            " (CASE WHEN EL.TRANSLATEDTEXT IS NULL  THEN D.NodeName WHEN EL.TRANSLATEDTEXT IS NOT NULL AND EL.LANGUAGECODE<>' " & strLangCode & "' THEN D.NodeName ELSE EL.TRANSLATEDTEXT END )as NodeName ,isNull(H.DefaultEAN,0) as DefaultEAN,isnull(B.FreezeSB,0) as FreezeSB,isnull(B.FreezeOB,0) as FreezeOB,isnull(B.FreezeSR,0) as FreezeSR ,H.Discription AS EanType, A.LastNodeCode AS Nodes ,AM.CPBaseCurr as CostAmt " & _
            " FROM MSTARTICLE A with (nolock)  INNER JOIN MSTEAN H with (nolock) ON A.ARTICLECODE=H.ARTICLECODE AND A.ArticleActive=1 " & _
            " INNER JOIN 	SalesInfoRecord B with (nolock) ON B.SiteCode='" & pCurrentSiteCode & "' AND B.SrNo=1 AND H.EAN=B.EAN  INNER JOIN MSTARTICLENODE D with (nolock)" & _
            " ON A.LASTNODECODE=D.NODECODE LEFT OUTER JOIN (SELECT SiteCode ,ArticleCode,SUM(TotalPhysicalSaleableQty)TotalPhysicalSaleableQty,SUM(ReservedQty)ReservedQty,SUM(TotalPhysicalNonSaleableQty)TotalPhysicalNonSaleableQty,SUM(OnOrderQty)OnOrderQty FROM ARTICLESTOCKBALANCES GROUP BY Sitecode ,ARTICLECODE) E ON E.Sitecode='" & pCurrentSiteCode & "' AND A.ARTICLECODE=E.ARTICLECODE " & _
            " LEFT JOIN ElementTranslation EL ON D.NODECODE=EL.ELEMENTCODE AND EL.LANGUAGECODE = '" & strLangCode & "' "

            vstmtqry = vstmtqry & " LEFT OUTER JOIN ARTICLEDESCINDIFFLANG Z with (nolock) ON A.ARTICLECODE = Z.ARTICLECODE AND Z.LANGUAGECODE = '" & gLngCode & "' "
            'vstmtqry = vstmtqry & " LEFT OUTER JOIN ArticleMAP AM on am.EAN=H.EAN "
            vstmtqry = vstmtqry & " LEFT OUTER JOIN PurchaseInfoRecord  AM on am.EAN=H.EAN and am.IsDefaultSupplier ='1' and am.ArticleCode =H.ArticleCode  AND AM.sitecode =b.sitecode"
            If isDayClose Then
                vstmtqry = vstmtqry & " WHERE A.ArticleCode not in (select MasterArticleCode  from MasterArticleMap) "
                vstmtqry = vstmtqry & " And E.Sitecode = '" & pCurrentSiteCode & "' AND B.SITECODE='" & pCurrentSiteCode & "' And isnull(B.SRNO,0)=1"
            Else
                vstmtqry = vstmtqry & " WHERE isnull(A.SALABLE,0)=1  "
                vstmtqry = vstmtqry & " AND E.Sitecode = '" & pCurrentSiteCode & "' AND B.SITECODE='" & pCurrentSiteCode & "' And isnull(B.SRNO,0)=1"
            End If
            If (vEAN = String.Empty) Then
                vstmtqry = vstmtqry & " AND ( H.DEFAULTEAN = 1 ) "
            End If
            If Not (vEAN = String.Empty) Then
                'vstmtqry = vstmtqry & " AND ( H.EAN = '" & vEAN & "' OR A.ArticleCode='" & vEAN & "')"
                ' for optimization OR A.ArticleCode='"
                vstmtqry = vstmtqry & " AND ( H.EAN = '" & vEAN & "' OR H.ArticleCode='" & vEAN & "' OR LegacyArticleCode ='" & vEAN & "')"
            End If
            If ExtraCondition <> String.Empty Then
                vstmtqry = vstmtqry & ExtraCondition
            End If
Redo:
            daSql = New SqlDataAdapter(vstmtqry, ConString)
            dtSearch.Clear()
            daSql.Fill(dtSearch)
            'If vEAN = String.Empty And (DtTemp) Is Nothing Then
            '    DtTemp = dtSearch.Copy()
            'End If
            Return dtSearch
        Catch ex As Exception
            'If DirectCast(ex, System.Data.SqlClient.SqlException).ErrorCode = "-2146232060" Then
            '    GoTo redo
            'End If
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetPromotionData(Optional ByVal vEAN As String = "") As DataTable
        Try
            vSqlQry.Length = 0

            'Commented by Rohit for CR-5773

            'vSqlQry.Append("SELECT H.EAN,A.ARTICLESHORTNAME AS DISCRIPTION,A.ARTICLECODE,A.SUPPLIERREF, " & vbCrLf)
            'vSqlQry.Append("D.NODENAME,B.SELLINGPRICE,DBO.GETCHAROFARTICLE(A.ARTICLECODE) AS CHARS, " & vbCrLf)
            'vSqlQry.Append("IsNull(A.IsMrpOpen ,0) as IsMrpOpen " & vbCrLf)
            'vSqlQry.Append("FROM MSTEAN H INNER JOIN  MSTARTICLE A ON H.ARTICLECODE=A.ARTICLECODE " & vbCrLf)
            'vSqlQry.Append("INNER JOIN SalesInfoRecord B ON H.EAN=B.EAN " & vbCrLf)
            'vSqlQry.Append("INNER JOIN ARTICLENODEMAP C ON A.ARTICLECODE=C.ARTICLECODE " & vbCrLf)
            'vSqlQry.Append("INNER JOIN MSTARTICLENODE D ON C.LASTNODECODE=D.NODECODE " & vbCrLf)

            'Added by Rohit for CR-5773

            vSqlQry.Append("SELECT H.EAN,A.ARTICLESHORTNAME AS DISCRIPTION,A.ARTICLECODE,A.SUPPLIERREF, " & vbCrLf)
            vSqlQry.Append("(CASE WHEN EL.TRANSLATEDTEXT IS NULL  THEN D.NODENAME WHEN EL.TRANSLATEDTEXT IS NOT NULL AND EL.LANGUAGECODE<>' " & strLangCode & "' THEN D.NODENAME ELSE EL.TRANSLATEDTEXT END )as NODENAME,B.SELLINGPRICE,DBO.GETCHAROFARTICLE(A.ARTICLECODE) AS CHARS, " & vbCrLf)
            vSqlQry.Append("IsNull(A.IsMrpOpen ,0) as IsMrpOpen " & vbCrLf)
            vSqlQry.Append("FROM MSTEAN H INNER JOIN  MSTARTICLE A ON H.ARTICLECODE=A.ARTICLECODE " & vbCrLf)
            vSqlQry.Append("INNER JOIN SalesInfoRecord B ON H.EAN=B.EAN " & vbCrLf)
            vSqlQry.Append("INNER JOIN ARTICLENODEMAP C ON A.ARTICLECODE=C.ARTICLECODE " & vbCrLf)
            vSqlQry.Append("INNER JOIN MSTARTICLENODE D ON C.LASTNODECODE=D.NODECODE " & vbCrLf)
            vSqlQry.Append("LEFT JOIN ElementTranslation EL ON D.NODECODE=EL.ELEMENTCODE AND EL.LANGUAGECODE = '" & strLangCode & "' ")

            daSql = New SqlDataAdapter(vSqlQry.ToString, SpectrumCon)
            dtSearch.Clear()
            daSql.Fill(dtSearch)
            Return dtSearch

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetPromItemsData(Optional ByVal vEAN As String = "") As DataTable
        Try
            vSqlQry.Length = 0
            vSqlQry.Append(" Select Convert(Varchar(50),'') as EAN, " & vbCrLf)
            vSqlQry.Append(" Convert(Varchar(50),'') as Discription, " & vbCrLf)
            vSqlQry.Append(" Convert(Varchar(50),'') as Product, " & vbCrLf)
            vSqlQry.Append(" Convert(numeric(18,2),0) as SellingPrice, " & vbCrLf)
            vSqlQry.Append(" Convert(numeric(18,2),0) as Discount, " & vbCrLf)
            vSqlQry.Append(" Convert(numeric(18,2),0) as FlatePrice, " & vbCrLf)
            vSqlQry.Append(" Convert(numeric(18,2),0) as RupeesOff, " & vbCrLf)
            vSqlQry.Append(" Convert(Varchar(50),'') as OfferType " & vbCrLf)

            daSql = New SqlDataAdapter(vSqlQry.ToString, SpectrumCon)
            dtSearch.Clear()
            daSql.Fill(dtSearch)
            Return dtSearch

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetArticleTree() As DataTable
        Try
            Dim str As String = "SELECT A.NODECODE,A.PARENTNODECODE,(CASE WHEN EL.TRANSLATEDTEXT IS NULL  THEN B.NODENAME WHEN EL.TRANSLATEDTEXT IS NOT NULL AND EL.LANGUAGECODE<>'" & strLangCode & "' THEN B.NODENAME ELSE EL.TRANSLATEDTEXT END )as NODENAME,B.ISTHISLASTNODE ,A.TREECODE FROM ARTICLETREENODEMAP A " & _
            " INNER JOIN MSTARTICLENODE B ON A.NODECODE=B.NODECODE INNER JOIN MSTARTICLETREE C ON A.TREECODE=C.TREECODE AND C.STATUS=1 " & _
            " LEFT JOIN ElementTranslation EL ON B.NODECODE=EL.ELEMENTCODE AND EL.LANGUAGECODE = '" & strLangCode & "' " & _
            " WHERE B.STATUS=1 ORDER BY A.TREECODE,A.PARENTNODECODE  "
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(str, ConString)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    'added by khusrao adil on 01-08-2018 for pos tab tree issue
    Public Function GetArticleTreeForPosTab() As DataTable
        Try
            Dim StrQuery As New StringBuilder
            StrQuery.Length = 0
            StrQuery.Append("SELECT  [TreeCode] AS NODECODE,[C2] AS [ParentNodecode], [TreeName] AS [NodeName], [C4] AS IsThisLastNode,[C7] AS TreeCode ")
            StrQuery.Append("FROM  (SELECT 1 AS [C1], [Extent1].[TreeCode] AS [TreeCode], [Extent1].[TreeName] AS [TreeName], ")
            StrQuery.Append("CAST(NULL AS varchar(1)) AS [C2], CAST(NULL AS varchar(1)) AS [C3], [Extent1].[TreeCode] AS [TreeCode1], [Extent1].[TreeName] AS [TreeName1], ")
            StrQuery.Append("cast(0 as bit) AS [C4], N'0' AS [C5], N'' AS [C6], 1 AS [C7]")
            StrQuery.Append("FROM [dbo].[MstArticleTree] AS [Extent1] WHERE (1 = [Extent1].[STATUS]) AND ([Extent1].[STATUS] IS NOT NULL)")
            StrQuery.Append("UNION ALL SELECT 2 AS [C1], [Filter2].[Nodecode1] AS [Nodecode], [Filter2].[NodeName] AS [NodeName], ")
            StrQuery.Append("CASE WHEN ([Filter2].[ParentNodecode] IS NULL) THEN [Filter2].[Treecode1] ELSE [Filter2].[ParentNodecode] END AS NODECODE, ")
            StrQuery.Append("CASE WHEN ([Extent6].[NodeName] IS NULL) THEN [Filter2].[TreeName] ELSE [Extent6].[NodeName] END AS [C3], ")
            StrQuery.Append("[Filter2].[Treecode1] AS [Treecode], [Filter2].[TreeName] AS [TreeName], [Filter2].[ISThisLastNode] AS [ISThisLastNode], ")
            StrQuery.Append("[Filter2].[LevelCode] AS [LevelCode], [Extent5].[LevelName] AS [LevelName], 2 AS [C4]")
            StrQuery.Append("FROM    (SELECT [Extent2].[Nodecode] AS [Nodecode1], [Extent2].[ParentNodecode] AS [ParentNodecode], [Extent2].[Treecode] AS [Treecode1], [Extent3].[NodeName] AS [NodeName], [Extent3].[ISThisLastNode] AS [ISThisLastNode], [Extent3].[LevelCode] AS [LevelCode], [Extent4].[TreeCode] AS [TreeCode2], [Extent4].[TreeName] AS [TreeName]")
            StrQuery.Append("FROM   [dbo].[ArticleTreeNodeMap] AS [Extent2]")
            StrQuery.Append("INNER JOIN [dbo].[MstArticleNode] AS [Extent3] ON [Extent2].[Nodecode] = [Extent3].[Nodecode]")
            StrQuery.Append("INNER JOIN [dbo].[MstArticleTree] AS [Extent4] ON [Extent2].[Treecode] = [Extent4].[TreeCode]")
            StrQuery.Append("WHERE (1 = [Extent3].[STATUS]) AND ([Extent3].[STATUS] IS NOT NULL) AND (1 = [Extent4].[STATUS]) AND ([Extent4].[STATUS] IS NOT NULL) ) AS [Filter2]")
            StrQuery.Append("INNER JOIN [dbo].[MstArticleTreeLevel] AS [Extent5] ON ([Filter2].[TreeCode2] = [Extent5].[TreeCode]) AND ([Filter2].[LevelCode] = [Extent5].[LevelCode])")
            StrQuery.Append("LEFT OUTER JOIN [dbo].[MstArticleNode] AS [Extent6] ON [Filter2].[ParentNodecode] = [Extent6].[Nodecode]) AS [UnionAll1]")
            StrQuery.Append("order by [UnionAll1].[TreeCode] asc")
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(StrQuery.ToString(), ConString)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function getarticleTrees(ByVal SiteCode As String) As DataTable
        Try
            Dim dt As New DataTable
            'Commented by Rohit for CR-5773

            'Dim da As New SqlDataAdapter("SELECT A.* FROM MSTARTICLETREE A INNER JOIN SITEARTICLEHIERARCHYMAPPING B ON A.TREECODE=B.TREEID WHERE A.STATUS=1 AND B.Sitecode='" & SiteCode & "'", ConString)

            'Added by Rohit for CR-5773

            Dim da As New SqlDataAdapter("SELECT A.TreeCode,(CASE WHEN EL.TRANSLATEDTEXT IS NULL  THEN A.TreeName WHEN EL.TRANSLATEDTEXT IS NOT NULL AND EL.LANGUAGECODE<>'" & strLangCode & "' THEN A.TreeName ELSE EL.TRANSLATEDTEXT END )as TreeName,A.CREATEDAT,A.CREATEDBY,A.CREATEDON,A.UPDATEDAT,A.UPDATEDBY,A.UPDATEDON,A.STATUS FROM MSTARTICLETREE A INNER JOIN SITEARTICLEHIERARCHYMAPPING B ON A.TREECODE=B.TREEID LEFT JOIN ElementTranslation EL ON A.TreeCode=EL.ELEMENTCODE AND EL.LANGUAGECODE = '" & strLangCode & "' WHERE A.STATUS=1 AND B.Sitecode in ('" & SiteCode & "','CCE')", ConString)
            da.Fill(dt)
            Return dt
        Catch ex As Exception

        End Try
    End Function

    Public Function GetArticleNames(ByVal articleCodes As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter("SELECT ArticleCode, ArticleName FROM MstArticle WHERE ArticleCode IN (" & articleCodes & ") ORDER BY ArticleName", ConString)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

#End Region

#Region "Mettler Implementaion"

    Public Function IsValidScanedBillArticle(ByVal ScaleBillNo As String, ByVal ScaleBillIntDate As Long) As Boolean
        Try
            IsValidScanedBillArticle = False
            Dim query As String = "	IF EXISTS( SELECT 1  " & _
                                "	FROM dbo.CashMemoMettler  " & _
                                "	WHERE MettlerScaleBillNo = '" & ScaleBillNo & "'" & _
                                "	AND MettlerScaleBillDate = '" & ScaleBillIntDate & "')" & _
                                "   SELECT 1 ELSE SELECT 0"

            Dim dt As DataTable = GetFilledTable(query)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If (dt.Rows(0)(0) = 1) Then
                    IsValidScanedBillArticle = True
                End If
            End If
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function
    Public Function IsValidScanedHoldBillArticle(ByVal ScaleBillNo As String, ByVal ScaleBillIntDate As Long) As Boolean
        Try
            IsValidScanedHoldBillArticle = False
            Dim query As String = "	IF EXISTS( SELECT 1  " & _
                                "	FROM dbo.HoldCashMemoMettler  " & _
                                "	WHERE MettlerScaleBillNo = '" & ScaleBillNo & "'" & _
                                "	AND MettlerScaleBillDate = '" & ScaleBillIntDate & "')" & _
                                "   SELECT 0 ELSE SELECT 1"

            Dim dt As DataTable = GetFilledTable(query)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If (dt.Rows(0)(0) = 1) Then
                    IsValidScanedHoldBillArticle = True
                End If
            End If
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function
    Public Function IsValidScanedSpectrumMettler(ByVal ScaleBillNo As String, ByVal ScaleBillIntDate As Date) As Boolean
        Try
            IsValidScanedSpectrumMettler = False
            Dim StrQuery As String
            Dim dtdenom As New DataTable
            Dim ConectionString As New SqlConnection(ConString)
            'StrQuery = "SELECT  A.DenominationAmt, B.CurrencySymbol, A.DenominationDesc AS DENOMINATION, ISNULL(f.Qty,0) AS QTY,isnull(TotalAmount,0) AS AMOUNT, B.CurrencyCode  FROM    DenominationDetail AS A LEFT JOIN MstCurrency AS B ON A.CurrencyCode = B.CurrencyCode LEFT JOIN   FloatingDetail AS f ON A.CurrencyCode = f.CurrencyCode AND A.DENOMINATIONAMT = f.DENOMINATIONAMT   AND (f.Action = 'extraopen')   AND f.FlotDatetime =  @flotdate WHERE (B.CurrencyCode = '" & CurrencyCode & "') AND (A.STATUS = 1)  "
            StrQuery = "	IF EXISTS( SELECT 1  " & _
                               "	FROM dbo.SpectrumMettlerDtl  " & _
                               "	WHERE Billno = '" & ScaleBillNo & "'" & _
                               "	AND cast(MettlerScaleBillDate as date)= @ScaleBillIntDate" & _
                               "	AND Status = 1)" & _
                               "   SELECT 1 ELSE SELECT 0"
            Dim cmd As New SqlCommand(StrQuery, ConectionString)
            cmd.CommandText = StrQuery.ToString
            cmd.Parameters.AddWithValue("@ScaleBillIntDate", ScaleBillIntDate)
            Using daDenom As New SqlDataAdapter()

                daDenom.SelectCommand = cmd
                daDenom.Fill(dtdenom)

                If dtdenom IsNot Nothing AndAlso dtdenom.Rows.Count > 0 Then
                    If (dtdenom.Rows(0)(0) = 1) Then
                        IsValidScanedSpectrumMettler = True
                    End If
                End If
            End Using
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetScanedBillArticle(ByVal ScaleNo As Integer, ByVal BillNo As Integer, ByVal BillIntDate As Long, ByVal MettlerConn As String) As DataTable
        Try
            Dim dttemp As New DataTable
            'Dim DbMettler As String = SpectrumBL.clsLogin.ReadLocalParaFile("DbMettler")
            Dim DbMettlerConString As String = MettlerConn 'DataBaseConnection.MettlerConnectionString()
            DataBaseConnection.MettlerSpectrumCon(DbMettlerConString)

            '--- Bill No - CAST(SUBSTRING(c_flow_no,8,6) as int) 
            '---  No - CAST(SUBSTRING(c_flow_no,29,6) as int) 
            If ScaleNo > 0 And BillNo > 0 Then

                'Dim query As String = " SELECT SUBSTRING(c_flow_no,8,6) as ScaleNo ,SUBSTRING(c_flow_no,29,6) as BillNo ,SUBSTRING(c_flow_no,35,6) AS SequenceNo " & _
                '                 "		,c_plu_no  as LegacyArticleCode,c_article_name  ,CASE ISNULL(c_weight,0) when 0 THEN c_quantity else c_weight end  AS Quantity " & _
                '                 " FROM   " & DbMettler & ".dbo.TradeInfo_bCom " & _
                '                 " Where  CAST(SUBSTRING(c_flow_no,8,6) as int) = " & ScaleNo & _
                '                 "	   AND CAST(SUBSTRING(c_flow_no,29,6) as int) = " & BillNo & _
                '                 "    AND CAST(SUBSTRING(c_flow_no,15,6) as int) = " & BillIntDate & _
                '                 " ORDER BY SUBSTRING(c_flow_no,35,6)"
                Dim query As String = " SELECT SUBSTRING(c_flow_no,8,6) as ScaleNo ,SUBSTRING(c_flow_no,29,6) as BillNo ,SUBSTRING(c_flow_no,35,6) AS SequenceNo " & _
                                 "		,c_plu_no  as LegacyArticleCode,c_article_name,CASE ISNULL(c_weight,0) when 0 THEN c_quantity else (cast(c_weight as float)/1000) end  AS Quantity " & _
                                 " FROM    TradeInfo_bCom with(nolock)" & _
                                 " Where  CAST(SUBSTRING(c_flow_no,8,6) as int) = " & ScaleNo & _
                                 "	   AND CAST(SUBSTRING(c_flow_no,29,6) as int) = " & BillNo & _
                                 "    AND CAST(SUBSTRING(c_flow_no,15,6) as int) = " & BillIntDate & _
                                 " ORDER BY SUBSTRING(c_flow_no,35,6)"
                'dttemp = GetFilledTable(query)

                Dim da As New SqlDataAdapter(query, DbMettlerConString)
                da.Fill(dttemp)
            End If
            Return dttemp
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function GetScanedBillArticleForSpectrumMettler(ByVal BillNo As String) As DataTable
        Try
            Dim dttemp As New DataTable
            Dim query As String = " select * from  SpectrumMettlerDtl where billno like  '%" & BillNo & "%'"
            Dim da As New SqlDataAdapter(query, SpectrumCon)
            da.Fill(dttemp)
            Return dttemp
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Function GetScanedCashMemoBillsArticle(ByVal BillNoList As String, ByVal BillIntDate As Long, ByVal MettlerConn As String) As DataTable
        Try
            Dim dttemp As New DataTable
            Dim DbMettlerConString As String = MettlerConn
            DataBaseConnection.MettlerSpectrumCon(DbMettlerConString)
            If Not String.IsNullOrEmpty(BillNoList) Then
                Dim query As String = " SELECT c_plu_no  as LegacyArticleCode,SUM(isnull(CASE ISNULL(c_weight,0)  when 0 THEN c_quantity else (cast(c_weight as float)/1000) end ,0)) AS Quantity " & _
                                 " FROM    TradeInfo_bCom " & _
                                 " WHERE  CAST(SUBSTRING(c_flow_no,29,6) as int) in( " & BillNoList & ")" & _
                                 "        AND CAST(SUBSTRING(c_flow_no,15,6) as int) = " & BillIntDate & _
                                 " GROUP BY c_plu_no "

                Dim da As New SqlDataAdapter(query, DbMettlerConString)
                da.Fill(dttemp)

            End If
            Return dttemp
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function


#End Region

End Class

