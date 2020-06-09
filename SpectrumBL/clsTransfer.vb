Imports System.Data
Imports System.Data.SqlClient
Public Class clsTransfer
    Public Function GetArticlesBySite(ByVal SiteCode As String) As DataTable
        Try

            Dim strQuery As String
            strQuery = "select a.ArticleShortName as ArticleName ,a.ArticleCode from MstArticle a inner join SalesInfoRecord s on a.ArticleCode =s.ArticleCode and s.SiteCode ='" & SiteCode & "'"
            Dim dtTemp As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dtTemp)
            Return dtTemp
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetArticlesByArticleName(ByVal SiteCode As String, ByVal articleName As String) As DataTable
        Try

            Dim strQuery As String
            strQuery = "select a.ArticleShortName as ArticleName ,a.ArticleCode from MstArticle a inner join SalesInfoRecord s on a.ArticleCode =s.ArticleCode and s.SiteCode ='" & SiteCode & "' and a.ArticleShortName like '" & articleName & "%'"
            Dim dtTemp As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dtTemp)
            Return dtTemp
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetArticlesByArticleCode(ByVal SiteCode As String, ByVal articleCode As String) As DataTable
        Try

            Dim strQuery As String
            strQuery = "select a.ArticleShortName as ArticleName ,a.ArticleCode from MstArticle a inner join SalesInfoRecord s on a.ArticleCode =s.ArticleCode and s.SiteCode ='" & SiteCode & "' and a.ArticleCode like '" & articleCode & "%'"
            Dim dtTemp As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dtTemp)
            Return dtTemp
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetStores() As DataTable
        Try

            Dim strQuery As String
            strQuery = "select * from MstSite where BusinessCode='Store'"
            Dim dtTemp As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dtTemp)
            Return dtTemp
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSTRbyArticles(ByVal SiteCode As String, ByVal articleCode As String) As DataTable
        Try

            Dim strQuery As String
            strQuery = "   select   hdr.DocumentNumber,   replace(convert(NVARCHAR, hdr.ExpectedDeliveryDt, 106), ' ', '-') as DeliveryDt  from OrderHdr hdr inner join OrderDtl dtl on hdr.DocumentNumber =dtl.DocumentNumber   where hdr.DocumentType='SR' and hdr.IsClosed=0 and hdr.SiteCode='" & SiteCode & "'"

            Dim dtTemp As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dtTemp)
            Return dtTemp
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetSTRDetails(ByVal SiteCode As String, ByVal articleCode As String, ByVal StrDocNumber As String) As DataTable
        Try

            Dim strQuery As String
            strQuery = "select dtl.BaseUom,dtl.Qty,dtl.ArticleCode , hdr.DocumentNumber from OrderHdr hdr inner join OrderDtl dtl on hdr.DocumentNumber =dtl.DocumentNumber  where hdr.DocumentType='SR' and hdr.IsClosed=0 and hdr.SiteCode='" & SiteCode & "' and hdr.DocumentNumber='" & StrDocNumber & "' and dtl.ArticleCode='" & articleCode & "'"

            Dim dtTemp As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dtTemp)
            Return dtTemp
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetArticleNameByCode(ByVal SiteCode As String, ByVal articleCode As String) As String
        Try
            Dim articleName As String
            Dim strQuery As String
            strQuery = "select a.ArticleName  from MstArticle a inner join SalesInfoRecord s on a.ArticleCode =s.ArticleCode and s.SiteCode ='" & SiteCode & "' and a.ArticleCode = '" & articleCode & "'"
            If SpectrumCon().State <> ConnectionState.Open Then
                SpectrumCon().Open()
            End If
            Dim cmd As New SqlCommand(strQuery, SpectrumCon())
            articleName = cmd.ExecuteScalar()
            Return articleName
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetArticleCodeByName(ByVal SiteCode As String, ByVal articleName As String) As String
        Try
            Dim articleCode As String
            Dim strQuery As String
            strQuery = "select a.ArticleCode from MstArticle a inner join SalesInfoRecord s on a.ArticleCode =s.ArticleCode and s.SiteCode ='" & SiteCode & "' and a.ArticleShortName = '" & articleName & "'"
            If SpectrumCon().State <> ConnectionState.Open Then
                SpectrumCon().Open()
            End If
            Dim cmd As New SqlCommand(strQuery, SpectrumCon())
            articleCode = cmd.ExecuteScalar()
            Return articleCode
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function AddTransferDetails(ByVal objTransferDetails As TransferDetails) As Boolean
        Try
            Dim Query = "INSERT INTO TransferDetails ([ArticleCode],[SiteCode],[RefDocumentNo],[Quantity],[UOM],[TransferStatus],[CREATEDAT],[CREATEDBY] ,[CREATEDON] ,[UPDATEDAT],[UPDATEDBY],[UPDATEDON],[STATUS])" & _
                    "VALUES('" & objTransferDetails.ArticleCode & "','" & objTransferDetails.SiteCode & "', '" & objTransferDetails.RefDocumentNo & "','" & objTransferDetails.Quantity & "', '" & objTransferDetails.UOM & "', '" & objTransferDetails.TransferStatus & "','" & objTransferDetails.CREATEDAT & "','" & objTransferDetails.CREATEDBY & "','" & objTransferDetails.CREATEDON & "'," & _
                    "'" & objTransferDetails.UPDATEDAT & "','" & objTransferDetails.UPDATEDBY & "', '" & objTransferDetails.UPDATEDON & "' , '" & objTransferDetails.Status & "' )"
            If SpectrumCon().State <> ConnectionState.Open Then
                SpectrumCon().Open()
            End If

            Dim cmd As New SqlCommand(Query, SpectrumCon())
            If cmd.ExecuteNonQuery() > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    

    Public Function GetTransferDetails(ByVal fromDate As String, ByVal toDate As String, ByVal articleCode As String, ByVal articleName As String, ByVal SiteCode As String) As DataTable
        Try
            Dim Query As String = ""
            Query &= "select art.ArticleShortName,art.ArticleCode ,site.SiteOfficialName ,tdtls.RefDocumentNo, od.Qty,tdtls.Quantity,tdtls.UOM,tdtls.TransferStatus, hdr.DeliveryDate ,tdtls.SiteCode from TransferDetails  tdtls inner join MSTArticle a on art.ArticleCode=tdls.ArticleCode inner join MSTSite site on site.SiteCode =tdls.SiteCode " & _
                        "join OrderDtl od on od.DocumentNumber=tdtls.RefDocumentNo and od.ArticleCode=tdtls.ArticleCode and od.SiteCode=tdtls.SiteCode " & _
                        "join OrderHdr hdr on hdr.DocumentNumber=od.DocumentNumber where 1=1 "

            If articleName.Trim() <> "" Then
                Query &= " and art.ArticleShortName like '%" & articleName & "%'"
            End If

            If articleCode.Trim() <> "" Then
                Query &= " and tdtls.ArticleCode like '%" & articleCode & "%'"
            End If

            If SiteCode.Trim() <> "" Then
                Query &= " and tdtls.SiteCode like '%" & SiteCode & "%'"
            End If

            If fromDate.Trim() <> "" AndAlso toDate.Trim() <> "" Then
                Query &= " and tdtls.CREATEDON between CONVERT(datetime,'" & fromDate & "') and CONVERT(datetime,'" & toDate & "') "
            ElseIf fromDate.Trim() <> "" Then
                Query &= " and tdtls.CREATEDON >= CONVERT(datetime,'" & fromDate & "')"
            End If

            If SpectrumCon().State <> ConnectionState.Open Then
                SpectrumCon().Open()
            End If

            Dim cmd As New SqlCommand(Query, SpectrumCon())
            Dim dtTemp As New DataTable
            Dim da As New SqlDataAdapter(Query, ConString)
            da.Fill(dtTemp)
            Return dtTemp
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


End Class
Public Class TransferDetails

    Dim articleCodeStr, siteCodeStr, refDocumentNoStr, UOMStr, CREATEDATStr, CREATEDBYStr, UPDATEDATStr, UPDATEDBYStr, CREATEDONStr, UPDATEDONStr As String
    Dim quantityInt, transferStatusInt, statusInt As Integer
    Public Property ArticleCode As String
        Get
            Return articleCodeStr
        End Get
        Set(ByVal value As String)
            articleCodeStr = value
        End Set
    End Property
    Public Property SiteCode As String
        Get
            Return siteCodeStr
        End Get
        Set(ByVal value As String)
            siteCodeStr = value
        End Set
    End Property
    Public Property RefDocumentNo As String
        Get
            Return refDocumentNoStr
        End Get
        Set(ByVal value As String)
            refDocumentNoStr = value
        End Set
    End Property
    Public Property Quantity As Integer
        Get
            Return quantityInt
        End Get
        Set(ByVal value As Integer)
            quantityInt = value
        End Set
    End Property
    Public Property UOM As String
        Get
            Return UOMStr
        End Get
        Set(ByVal value As String)
            UOMStr = value
        End Set
    End Property
    Public Property TransferStatus As Integer
        Get
            Return transferStatusInt
        End Get
        Set(ByVal value As Integer)
            transferStatusInt = value
        End Set
    End Property

    Public Property CREATEDON As String
        Get
            Return CREATEDONStr
        End Get
        Set(ByVal value As String)
            CREATEDONStr = value
        End Set
    End Property
    Public Property CREATEDAT As String
        Get
            Return CREATEDATStr
        End Get
        Set(ByVal value As String)
            CREATEDATStr = value
        End Set
    End Property
    Public Property CREATEDBY As String
        Get
            Return CREATEDBYStr
        End Get
        Set(ByVal value As String)
            CREATEDBYStr = value
        End Set
    End Property
    Public Property UPDATEDON As String
        Get
            Return UPDATEDONStr
        End Get
        Set(ByVal value As String)
            UPDATEDONStr = value
        End Set
    End Property
    Public Property UPDATEDAT As String
        Get
            Return UPDATEDATStr
        End Get
        Set(ByVal value As String)
            UPDATEDATStr = value
        End Set
    End Property
    Public Property UPDATEDBY As String
        Get
            Return UPDATEDBYStr
        End Get
        Set(ByVal value As String)
            UPDATEDBYStr = value
        End Set
    End Property
    Public Property Status As Integer
        Get
            Return statusInt
        End Get
        Set(ByVal value As Integer)
            statusInt = value
        End Set
    End Property
End Class