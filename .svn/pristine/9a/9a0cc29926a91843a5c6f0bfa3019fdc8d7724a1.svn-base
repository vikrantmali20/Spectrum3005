Imports System.Text
Imports System.Data.SqlClient

Public Class clsMembership
#Region "Global Variables"
    Dim vStmtQry As New System.Text.StringBuilder
    Dim daScan As New SqlDataAdapter
    Dim dtScan As DataTable
    Dim dsScan As DataSet

#End Region

#Region "Functions"

    Public Function GetMembership() As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("select MA.ArticleShortName,MA.ArticleCode,ME.EAN from MstArticle MA INNER JOIN MstArticleNode  MAN on MA.LastNodeCode= MAN.Nodecode " & vbCrLf)
            vStmtQry.Append("LEFT JOIN MstEAN ME ON MA.ArticleCode=ME.ArticleCode Where MAN.NodeName='Membership' and MAN.Status=1" & vbCrLf)
            Return GetFilledTable(vStmtQry.ToString)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetServices() As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("select MA.ArticleShortName,MA.ArticleCode,ME.EAN  from MstArticle MA INNER JOIN MstArticleNode  MAN on MA.LastNodeCode= MAN.Nodecode " & vbCrLf)
            vStmtQry.Append("LEFT JOIN MstEAN ME ON MA.ArticleCode=ME.ArticleCode Where MAN.NodeName='Service' and MAN.Status=1" & vbCrLf)
            Return GetFilledTable(vStmtQry.ToString)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetCarTypes() As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("select MA.ArticleShortName,MA.ArticleCode from MstArticle MA INNER JOIN MstArticleNode  MAN on MA.LastNodeCode= MAN.Nodecode " & vbCrLf)
            vStmtQry.Append("Where MAN.NodeName='Car Type' and MAN.Status=1" & vbCrLf)
            Return GetFilledTable(vStmtQry.ToString)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetMainPromotion(ByVal SiteCode As String) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select A.OfferName,A.OfferNo " & vbCrLf)
            vStmtQry.Append("FROM Promotions As A Inner join PROMOTIONSITEMAP As B " & vbCrLf)
            vStmtQry.Append("On A.OfferNo = B.OfferNo " & vbCrLf)
            vStmtQry.Append("Where  B.SITECODE='" & Sitecode & "' AND A.ISAPPROVED=1 AND A.OFFERACTIVE=1 AND A.OfferTriggeredBy='Customer'" & vbCrLf)

            Return GetFilledTable(vStmtQry.ToString)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetMainPromotionDiscount(ByVal ArticleCode As String, ByVal offerNo As String) As String
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select value " & vbCrLf)
            vStmtQry.Append("FROM PromotionDetails " & vbCrLf)
            vStmtQry.Append("Where  OfferNo='" & offerNo & "' AND ArticleCode='" & ArticleCode & "' and Status=1" & vbCrLf)
            If Not GetFilledTable(vStmtQry.ToString) Is Nothing AndAlso GetFilledTable(vStmtQry.ToString).Rows.Count > 0 Then
                Return IIf(GetFilledTable(vStmtQry.ToString).Rows(0)(0) Is DBNull.Value, 0, GetFilledTable(vStmtQry.ToString).Rows(0)(0))
            Else
                Return 0
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


    Public Function GetAdditionalPromotion(ByVal SiteCode As String) As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select A.OfferName,A.OfferNo " & vbCrLf)
            vStmtQry.Append("FROM Promotions As A Inner join PROMOTIONSITEMAP As B " & vbCrLf)
            vStmtQry.Append("On A.OfferNo = B.OfferNo " & vbCrLf)
            vStmtQry.Append("Where  B.SITECODE='" & SiteCode & "' AND A.ISAPPROVED=1 AND A.OFFERACTIVE=1 AND A.OfferTriggeredBy='Customer'" & vbCrLf)
            Return GetFilledTable(vStmtQry.ToString)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetFilledTable(ByVal strQuery As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Create Schema to display in grid
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMembershipSchema() As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(50),'')  as SrNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Del," & vbCrLf)
            vStmtQry.Append(" Convert(bit,'False')as Sel," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'')  as ArticleCode," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Period, " & vbCrLf)
            vStmtQry.Append(" Convert(DateTime,'') as StartDate, " & vbCrLf)
            vStmtQry.Append(" Convert(DateTime,'') as EndDate, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Price, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as DiscountPer, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Discount, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as NetAmt" & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dtScan = New DataTable
            daScan.Fill(dtScan)
            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


    Public Function GetAddDiscount(ByVal OfferNo As String) As String
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            vStmtQry.Append(" select OfferName from Promotions where OfferNo='" & OfferNo & "' and Status=1" & vbCrLf)
            Dim daAddPromo As New SqlDataAdapter
            daAddPromo = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtAddPromo As New DataTable
            daAddPromo.Fill(dtAddPromo)
            Return dtAddPromo.Rows(0)(0)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetMembershipTableStructure(ByVal Sitecode As String, ByVal CustomerNo As String, Optional ByVal BillNo As String = Nothing) As DataSet

        Try
            vStmtQry.Length = 0
            vStmtQry.Append("Select *from MemberShipDetails Where SiteCode='" & Sitecode & "' and CustomerNo='" & CustomerNo & "' ;" & vbCrLf)
            vStmtQry.Append("Select * from MembershipPromoMapping where membershipid in (Select membershipid from MemberShipDetails Where SiteCode='" & Sitecode & "' and CustomerNo='" & CustomerNo & "');" & vbCrLf)
            vStmtQry.Append("Select *from CashMemoHdr Where Sitecode= '" & Sitecode & "' and BillNo='" & BillNo & "' ;" & vbCrLf)
            vStmtQry.Append("Select * from CashMemoDtl Where Sitecode= '" & Sitecode & "' and BillNo='" & BillNo & "' ;" & vbCrLf)
            vStmtQry.Append("Select * from CashMemoReceipt Where Sitecode= '" & Sitecode & "' and BillNo='" & BillNo & "'; " & vbCrLf)
            vStmtQry.Append("Select * from CashMemoTaxDtls Where Sitecode= '" & Sitecode & "' and BillNo='" & BillNo & "'" & vbCrLf)
            vStmtQry.Append("Select * from ClpCustomerServiceArticlePeriodMap Where Sitecode= '" & Sitecode & "' and BillNo='" & BillNo & "'" & vbCrLf) 'vipin
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsScan = New DataSet
            daScan.Fill(dsScan)
            dsScan.Tables(0).TableName = "MemberShipDetails"
            dsScan.Tables(1).TableName = "MembershipPromoMapping"
            dsScan.Tables(2).TableName = "CashMemoHdr"
            dsScan.Tables(3).TableName = "CashMemoDtl"
            dsScan.Tables(4).TableName = "CashMemoReceipt"
            dsScan.Tables(5).TableName = "CashMemoTaxDtls"
            dsScan.Tables(6).TableName = "ClpCustomerServiceArticlePeriodMap" 'vipin
            Return dsScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function UpdateMemberShipDetails(ByVal dsMemb As DataSet) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            Dim clsCommon As New clsCommon()
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If clsCommon.SaveData(dsMemb, SpectrumCon, tran) Then

                If clsCommon.UpdateDocumentNo("Membershipid", SpectrumCon, tran) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If

                If clsCommon.UpdateDocumentNo("CM", SpectrumCon, tran) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
                tran.Commit()
                CloseConnection()
                Return True
            Else
                tran.Rollback()
                CloseConnection()
                Return False
            End If


        Catch ex As Exception
            tran.Rollback()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' schema for Membership to print in Card
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMembershipTableStruc() As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(100),'') as DetailsHeader," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as CustomerName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as CustContact," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as CustAddress," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as CarModel," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as CarNo," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as CarType," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as TotalService," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as CustomerId," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as ModeOfPayment," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as TotalAmount," & vbCrLf)
            vStmtQry.Append(" Convert(DateTime,'')as StartDate," & vbCrLf)
            vStmtQry.Append(" Convert(DateTime,'')as ExpiryDate," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as MembershipId" & vbCrLf)
            Dim daMembership As New SqlDataAdapter
            daMembership = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtMembership As New DataTable
            daMembership.Fill(dtMembership)

            Return dtMembership
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Get Membership Hdr Data
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMembershipHeaderTableStruc() As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            vStmtQry.Append("Select Convert(Varchar(100),'') as ClientName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as SiteName," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as SiteAddress" & vbCrLf)
            Dim daMembershiphdr As New SqlDataAdapter
            daMembershiphdr = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtMembershiphdr As New DataTable
            daMembershiphdr.Fill(dtMembershiphdr)

            Return dtMembershiphdr
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' schema for Membership to print in Card
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMemberPromotionTableStruc() As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Numeric(18,3),0) as CarWashPrice," & vbCrLf)
            vStmtQry.Append("  Convert(Varchar(100),'') as AdditionalServicedDiscount" & vbCrLf)
            Dim daPrintPromotionDetails As New SqlDataAdapter
            daPrintPromotionDetails = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtPromotionDetails As New DataTable
            daPrintPromotionDetails.Fill(dtPromotionDetails)

            Return dtPromotionDetails
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Schema for Printing Membership
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CashmemoHdrStruct() As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            vStmtQry.Append("Select Convert(Varchar(100),'')as ClientName," & vbCrLf)
            vStmtQry.Append("Convert(varchar(100),'')as StoreName," & vbCrLf)
            vStmtQry.Append("Convert(varchar(100),'')as Address," & vbCrLf)
            vStmtQry.Append("Convert(varchar(20),'')as PhoneNumber," & vbCrLf)
            vStmtQry.Append("Convert(varchar(100),'')as CashMemoNo," & vbCrLf)
            vStmtQry.Append("Convert(DateTime,'')as Date," & vbCrLf)
            vStmtQry.Append("Convert(DateTime,'')as Time," & vbCrLf)
            vStmtQry.Append("Convert(varchar(100),'')as Cashier," & vbCrLf)
            vStmtQry.Append("Convert(varchar(100),'')as GiftMsg," & vbCrLf)

            vStmtQry.Append("Convert(varchar(100),'')as DineIn," & vbCrLf)
            vStmtQry.Append("Convert(varchar(100),'')as TokenNo" & vbCrLf)
            Dim daMemberHdr As New SqlDataAdapter
            daMemberHdr = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtMemberHdr As New DataTable
            daMemberHdr.Fill(dtMemberHdr)
            Return dtMemberHdr
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' structure for KOT Print 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCashmemoKOTBodyStruct() As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            vStmtQry.Append("Select Convert(Varchar(100),'')as Description," & vbCrLf)
            vStmtQry.Append("Convert(varchar(100),'')as Qty," & vbCrLf)
            vStmtQry.Append("Convert(numeric(18,3),0) as Amt" & vbCrLf)
            Dim daMemberKOTBody As New SqlDataAdapter
            daMemberKOTBody = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtMemberKOTBody As New DataTable
            daMemberKOTBody.Fill(dtMemberKOTBody)
            Return dtMemberKOTBody
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetCashmemoKOTFooterStruct() As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            vStmtQry.Append("Select Convert(Varchar(100),'')as VatTinString," & vbCrLf)
            vStmtQry.Append("Convert(varchar(100),'')as LBTNo," & vbCrLf)
            vStmtQry.Append("Convert(varchar(100),'')as ThankYouMesg" & vbCrLf)
            Dim daCashmemoKOTFooter As New SqlDataAdapter
            daCashmemoKOTFooter = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtCashmemoKOTFooter As New DataTable
            daCashmemoKOTFooter.Fill(dtCashmemoKOTFooter)
            Return dtCashmemoKOTFooter
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetMemberDetails(ByVal MembershipId As String, ByVal sitecode As String) As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            vStmtQry.Append("select * from MembershipDetails where status=1 and Membershipid='" & MembershipId & "' and Sitecode='" & sitecode & "'" & vbCrLf)
            Dim daMemDet As New SqlDataAdapter
            daMemDet = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtMemDet As New DataTable
            daMemDet.Fill(dtMemDet)
            Return dtMemDet

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetMemberDiscount(ByVal MembershipId As String, ByVal sitecode As String) As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            vStmtQry.Append("select * from MembershipPromoMapping where status=1 and Membershipid='" & MembershipId & "' and Sitecode='" & sitecode & "'" & vbCrLf)
            Dim daMemDet As New SqlDataAdapter
            daMemDet = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtMemDet As New DataTable
            daMemDet.Fill(dtMemDet)
            Return dtMemDet

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Schema for Printing Membership
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CashmemoHdrCMStruct() As DataTable
        Try
            Dim vStmtQry As New StringBuilder
            vStmtQry.Length = 0
            vStmtQry.Append("Select Convert(Varchar(100),'')as ClientName," & vbCrLf)
            vStmtQry.Append("Convert(varchar(100),'')as StoreName," & vbCrLf)
            vStmtQry.Append("Convert(varchar(100),'')as Address," & vbCrLf)
            vStmtQry.Append("Convert(varchar(20),'')as PhoneNumber," & vbCrLf)
            vStmtQry.Append("Convert(varchar(100),'')as BillNo," & vbCrLf)
            vStmtQry.Append("Convert(varchar(100),'')as MembershipId," & vbCrLf)
            vStmtQry.Append("Convert(varchar(100),'') as CarNo," & vbCrLf)
            vStmtQry.Append("Convert(varchar(100),'')as GiftMsg," & vbCrLf)
            vStmtQry.Append("Convert(varchar(100),'')as DineIn," & vbCrLf)
            vStmtQry.Append("Convert(varchar(100),'')as TokenNo," & vbCrLf)
            vStmtQry.Append("Convert(numeric(18,3),0) as TaxAmt," & vbCrLf)
            vStmtQry.Append("Convert(numeric(18,3),0) as DiscAmt," & vbCrLf)
            vStmtQry.Append("Convert(numeric(18,3),0) as RateAfterDisc" & vbCrLf)
            Dim daMemberHdr As New SqlDataAdapter
            daMemberHdr = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtMemberHdr As New DataTable
            daMemberHdr.Fill(dtMemberHdr)
            Return dtMemberHdr
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

#End Region

End Class
