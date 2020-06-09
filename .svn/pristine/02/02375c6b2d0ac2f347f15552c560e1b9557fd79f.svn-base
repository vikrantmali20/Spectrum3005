Imports SpectrumBL
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Text
Imports System.Math

Public Class clsArticleCombo
    Inherits clsCommon

    Public Function GetComboDetails(ByVal comboCode As String) As DataTable
        Try
            'Dim result As DataTable
            Dim query As String
            query = "select * from MstArticleCombo where status=1 and combocode ='" & comboCode & "' order by sequence"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetArticlesOfANode(ByVal nodeCode As String, ByVal siteCode As String) As DataTable
        Try
            Dim dataAdapter As SqlDataAdapter
            Dim articles As New DataTable
            dataAdapter = New SqlDataAdapter("Exec getAllArticlesForANode '" & siteCode & "','" & nodeCode & "'", SpectrumCon)
            dataAdapter.Fill(articles)
            Return articles
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetArticlesOfAGroup(ByVal groupID As String, ByVal siteCode As String) As DataTable
        Try
            Dim dataAdapter As SqlDataAdapter
            Dim articles As New DataTable
            dataAdapter = New SqlDataAdapter("Exec getAllArticlesOfAGroup '" & groupID & "','" & siteCode & "'", SpectrumCon)
            dataAdapter.Fill(articles)
            Return articles
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetComboSellingPrice(ByVal articleCode As String, ByVal siteCode As String) As DataTable
        Dim query As String
        query = "select SellingPrice from SalesInfoRecord where ArticleCode ='" & articleCode & "' AND SiteCode='" & siteCode & "'"
        Return GetFilledTable(query)
    End Function

    Public Function GetComboItemQuantity(ByVal articleCode As String) As Integer
        Try
            Dim query As String = "select SUM(Quantity) from MstArticleCombo where ComboCode = '" & articleCode & "'"
            Dim dt As DataTable = GetFilledTable(query)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso IsDBNull(dt.Rows(0)(0)) = False Then
                Return dt.Rows(0)(0)
            Else
                Return 0
            End If
        Catch ex As Exception
            LogException(ex)
            Return 0
        End Try
    End Function

    Public Function CheckIfComboArticle(ByVal articleCode As String) As Boolean
        Try
            Dim query As String = "select ArticalTypeCode  from mstarticle where ArticleCode = '" & articleCode & "'"
            Dim dt As DataTable = GetFilledTable(query)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If UCase(dt.Rows(0)(0)) = "COMBO" Then
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function getTaxAmount(ByVal articleCode As String, ByVal sitecode As String, ByVal DocumentType As String) As DataTable 'PC SO Merge vipin 02-05-2018
        Try
            Dim vStmQry As String = ""
            vStmQry = ("select TaxValue  from TaxSiteDocMap  TM inner join SiteArticleTaxMapping SM  on TM.SITECODE=SM.SITECODE AND  SM.taxCode=TM.TaxCode where  Suppliercode='Internal' and SM.STATUS = 1 AND TM.IsDocumentLevelTax=0 and ArticleCode = '" & articleCode & "' and SM.SiteCode ='" & sitecode & "' and DocumentType ='" & DocumentType & "' and TM.STATUS = 1")
            Dim da = New SqlDataAdapter(vStmQry.ToString, SpectrumCon)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function FillArticles(ByVal nodeCode As String, ByVal siteCode As String) As DataTable
        Try
            Dim dataAdapter As SqlDataAdapter
            Dim articles As New DataTable
            dataAdapter = New SqlDataAdapter("Exec getAllArticlesForANode '" & siteCode & "','" & nodeCode & "'", SpectrumCon)
            dataAdapter.Fill(articles)
            Return articles
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function IsCodeLastNode(ByVal NodeCode As String) As Boolean
        Try
            Dim query As String = "select * from MstArticleNode  where Nodecode='" & NodeCode & "' and ISThisLastNode=1 and status=1"
            Dim dt As DataTable = GetFilledTable(query)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function GetBaseArticlePrice(ByVal siteCode As String, ByVal articleCode As String) As Double
        Try
            Dim query As String = "select SellingPrice from SalesInfoRecord where SiteCode = '" & siteCode & "' and ArticleCode = '" & articleCode & "' and ISNULL (srno,0) = 1"
            Dim dt As DataTable = GetFilledTable(query)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt.Rows(0)(0)
            End If
            Return 0
        Catch ex As Exception
            LogException(ex)
            Return 0
        End Try
    End Function
    Public Function GetArticleDetails(ByVal Article As String) As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            vStmtQry.Append("Select A.ARTICLECODE,ISNULL( Z.ARTICLESHORTNAME,A.ARTICLESHORTNAME) AS ArticleName, (CASE WHEN EL.TRANSLATEDTEXT IS NULL  THEN D.NodeName " & vbCrLf)
            vStmtQry.Append("WHEN EL.TRANSLATEDTEXT IS NOT NULL AND EL.LANGUAGECODE<>' EN' THEN D.NodeName ELSE EL.TRANSLATEDTEXT END )as NodeName FROM MSTARTICLE A with (nolock)" & vbCrLf)
            vStmtQry.Append("INNER JOIN MSTEAN H with (nolock) ON A.ARTICLECODE=H.ARTICLECODE AND A.ArticleActive=1 " & vbCrLf)
            vStmtQry.Append("INNER JOIN MSTARTICLENODE D with (nolock) ON A.LASTNODECODE=D.NODECODE " & vbCrLf)
            vStmtQry.Append("LEFT OUTER JOIN ARTICLEDESCINDIFFLANG Z with (nolock) ON A.ARTICLECODE = Z.ARTICLECODE AND Z.LANGUAGECODE = 'EN'" & vbCrLf)
            vStmtQry.Append("LEFT JOIN ElementTranslation EL ON D.NODECODE=EL.ELEMENTCODE AND EL.LANGUAGECODE = 'EN' " & vbCrLf)
            vStmtQry.Append("where A.ARTICLECODE ='" & Article & "' or A.ARTICLESHORTNAME = '" & Article & "' or D.NodeName='" & Article & "'" & vbCrLf)

            'If Article.Length = 0 Then
            '    vStmtQry.Append(" Select ArticleCode ,ArticleName ,LastNodeCode   from MstArticle  where 1 = 0" & vbCrLf)
            'Else  ''If'' Article.Length >= 3 Then
            '    vStmtQry.Append(" Select ArticleCode ,ArticleName ,LastNodeCode   from MstArticle  where ArticleName = '" & Article & "' or LastNodeCode ='" & Article & "' or ArticleCode = '" & Article & "' and STATUS=1" & vbCrLf)

            '    'Else
            '    '    vStmtQry.Append(" Select ArticleCode ,ArticleName ,LastNodeCode   from MstArticle  where 1 = 0" & vbCrLf)
            'End If
            Dim da = New SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Public Function GetDetailsForGuest() As DataTable 'added by vipin  26-03-2018
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            vStmtQry.Append("Select convert(varchar(50),'') as SrNo," & vbCrLf)
            ' vStmtQry.Append(" Convert(bit,'') as Del," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ArticleCode," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ArticleName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as LastNodeCode" & vbCrLf)

            Dim daReservation As New SqlDataAdapter
            daReservation = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtReservation As New DataTable
            daReservation.Fill(dtReservation)

            Return dtReservation
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetDetailsForKitArticle() As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            vStmtQry.Append("Select convert(varchar(50),'') as SrNo," & vbCrLf)
            ' vStmtQry.Append(" Convert(bit,'') as Del," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Ean," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ArticleCode," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ArticleShortName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as SellingPrice," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as SaleUnitofMeasure," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Quantity" & vbCrLf)

            Dim daReservation As New SqlDataAdapter
            daReservation = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtReservation As New DataTable
            daReservation.Fill(dtReservation)

            Return dtReservation
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
End Class