Imports System.Data.SqlClient
Imports System.Text
''' <summary>
''' This Class is Used to Apply Promotion's
''' </summary>
''' <CreatedBy>Rama Ranjan Jena</CreatedBy>
''' <UpdatedBy></UpdatedBy>
''' <usedin>CashMemo</usedin>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
Public Class clsApplyPromotion
    Inherits clsCommon
#Region "Global Variable For class"
    Dim MainDS As DataSet
    Dim MainPromoDS As New DataSet
    Dim _TableName, _ExlusiveTax, _TotalDisc, _GrossAmt, _Condition As String
#End Region
#Region "Public Property's"
    Public WriteOnly Property Condition() As String
        Set(ByVal value As String)
            _Condition = value
        End Set
    End Property
    Public WriteOnly Property MainTable() As String
        Set(ByVal value As String)
            _TableName = value
        End Set
    End Property
    Public WriteOnly Property ExclusiveTaxFieldName() As String
        Set(ByVal value As String)
            _ExlusiveTax = value
        End Set
    End Property
    Public WriteOnly Property TotalDiscountField() As String
        Set(ByVal value As String)
            _TotalDisc = value
        End Set
    End Property
    Public WriteOnly Property GrossAmtField() As String
        Set(ByVal value As String)
            _GrossAmt = value
        End Set
    End Property
    Private _IsInclusiveTax As Boolean
    Public Property IsInclusiveTax() As Boolean
        Get
            Return _IsInclusiveTax
        End Get
        Set(ByVal value As Boolean)
            _IsInclusiveTax = value
        End Set
    End Property
#End Region
#Region "Public Function's & Method's"
    ''' <summary>
    ''' intiallizies the class 
    ''' </summary>
    ''' <param name="ds">Dataset</param>
    ''' <param name="SiteCode">Sitecode</param>
    ''' <remarks></remarks>
    Public Sub CalculatedDs(ByRef ds As DataSet, ByVal SiteCode As String, Optional DayOpenDate As String = "", Optional ByVal CustWisePrice As Boolean = False, Optional ByVal ArticelCode As String = "", Optional ByVal CustNo As String = "", Optional ByVal IsHashTagApplicable As Boolean = False, Optional ByVal UserSelectedPromoHastTag As DataTable = Nothing, Optional ByVal dtBNQPXPYDHashTagPromo As DataTable = Nothing)
        Try
            'added by khusrao Adil
            'new parameter DayOpendate for sprint 14 promotion
            MainDS = ds
            getActivePromo(SiteCode, "", DayOpenDate:=DayOpenDate, CustWisePrice:=CustWisePrice, ArticelCode:=ArticelCode, CustNo:=CustNo, IsHashTagApplicable:=IsHashTagApplicable, UserSelectedPromoHastTag:=UserSelectedPromoHastTag, dtBNQPXPYDHashTagPromo:=dtBNQPXPYDHashTagPromo)
            Dim query As String = "Select A.OfferNo " & _
                                  "FROM Promotions As A Inner join PROMOTIONSITEMAP As B " & _
                                  "On A.OfferNo = B.OfferNo " & _
                                  "Where  B.SITECODE='" & SiteCode & "' AND A.ISAPPROVED=1 AND A.OFFERACTIVE=1 AND A.OfferTriggeredBy='Default'"
            Dim defaultPromotionNumbers As DataTable = GetFilledTable(query)
            If defaultPromotionNumbers IsNot Nothing AndAlso defaultPromotionNumbers.Rows.Count > 0 Then
                For Each row As DataRow In defaultPromotionNumbers.Rows
                    If CheckIfValidPromotion(row(0), SiteCode, DayOpenDate) = False Then
                        For Each table As DataTable In MainPromoDS.Tables
                            If table.Columns.Contains("OFFERNO") Then
                                For Each dataRow As DataRow In table.Select("OFFERNO='" & row(0) & "'")
                                    table.Rows.Remove(dataRow)
                                Next
                            End If
                        Next
                    End If
                Next
            End If
            StartApplyPromotion(custwisedtl:=CustWisePrice, Articlecode:=ArticelCode, IsHashTagApplicable:=IsHashTagApplicable, UserSelectedPromoHastTag:=UserSelectedPromoHastTag)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Lee spa
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="SiteCode"></param>
    ''' <remarks></remarks>
    Public Sub CalculatePromotionsByCustomer(ByRef ds As DataSet, ByVal SiteCode As String, Optional ByVal Promoid As String = "")
        Try
            MainDS = ds
            getActivePromo(SiteCode, "", False, True, Promoid)
            Dim query As String = "Select A.OfferNo " & _
                                  "FROM Promotions As A Inner join PROMOTIONSITEMAP As B " & _
                                  "On A.OfferNo = B.OfferNo " & _
                                  "Where  B.SITECODE='" & SiteCode & "' AND A.ISAPPROVED=1 AND A.OFFERACTIVE=1 AND A.OfferTriggeredBy='Customer'"
            Dim defaultPromotionNumbers As DataTable = GetFilledTable(query)
            If defaultPromotionNumbers IsNot Nothing AndAlso defaultPromotionNumbers.Rows.Count > 0 Then
                For Each row As DataRow In defaultPromotionNumbers.Rows
                    If CheckIfValidPromotion(row(0), SiteCode) = False Then
                        For Each table As DataTable In MainPromoDS.Tables
                            If table.Columns.Contains("OFFERNO") Then
                                For Each dataRow As DataRow In table.Select("OFFERNO='" & row(0) & "'")
                                    table.Rows.Remove(dataRow)
                                Next
                            End If
                        Next
                    End If
                Next
            End If
            StartApplyPromotion()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Get the List of activate Promotion's
    ''' </summary>
    ''' <UpdatedBy></UpdatedBy>
    ''' <usedin>CashMemo</usedin>
    ''' <UpdatedOn></UpdatedOn>
    ''' <param name="Sitecode">SiteCode</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetListofActivePromotions(ByVal SiteCode As String, Optional ByVal IsHashTagApplicable As Boolean = False) As DataTable
        Try
            Dim dt As DataTable
            getActivePromo(SiteCode, "", True, IsHashTagApplicable:=IsHashTagApplicable)
            Dim dv As New DataView(MainPromoDS.Tables("Promotions"), "", "OfferNo", DataViewRowState.CurrentRows)
            dt = dv.ToTable(True, "Offerno", "OfferName")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                    If CheckIfValidPromotion(dt.Rows(i)("Offerno"), SiteCode) = False Then
                        dt.Rows.RemoveAt(i)
                    End If
                Next
            End If
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Apply only the Selected Promotion's
    ''' </summary>
    ''' <UpdatedBy></UpdatedBy>
    ''' <usedin>CashMemo</usedin>
    ''' <UpdatedOn></UpdatedOn>
    ''' <param name="promotionNo">Promotion id</param>
    ''' <param name="dsMain">DataSet</param>
    ''' <param name="SiteCode">SiteCode</param>
    ''' <remarks></remarks>
    Public Sub ApplySelectedPromotion(ByVal promotionNo As String, ByRef dsMain As DataSet, ByVal SiteCode As String)
        Try
            If Not CheckIfValidPromotion(promotionNo, SiteCode) Then
                Exit Sub
            End If

            MainDS = dsMain
            If Not MainPromoDS Is Nothing Then
                Dim ds As DataSet = MainPromoDS.Copy()
                'MainPromoDS.Clear()
                MainPromoDS = New DataSet
                For Each dt As DataTable In ds.Tables
                    Dim dv As New DataView(dt, "OfferNo='" & promotionNo & "'", "", DataViewRowState.CurrentRows)
                    MainPromoDS.Tables.Add(dv.ToTable())
                Next
            Else
                getActivePromo(SiteCode, promotionNo, True)
            End If
            StartApplyPromotion()
        Catch ex As Exception

        End Try
    End Sub
    ''' <summary>
    ''' Check Promotion List and Apply
    ''' </summary>
    ''' <UpdatedBy></UpdatedBy>
    ''' <usedin>CashMemo</usedin>
    ''' <UpdatedOn></UpdatedOn>
    ''' <param name="strSiteCode">SiteCode</param>
    ''' <param name="NetBillAmt">BillAmount</param>
    ''' <param name="PromotionSlipText">Return- PromotionSlipText</param>
    ''' <param name="PromoId">PromotionId</param>
    ''' <remarks></remarks>
    Public Sub CheckForPromotionList(ByVal strSiteCode As String, ByVal NetBillAmt As Double, ByRef PromotionSlipText As String, ByRef PromoId As String)
        Try
            getActivePromo(strSiteCode, "")
            PromotionSlipText = GetPromotionSlip(NetBillAmt, PromoId)
        Catch ex As Exception
        End Try
    End Sub
    Public Function CheckValidations(ByVal Promotionid As String) As Boolean
        Try
            Dim dv As New DataView(MainPromoDS.Tables("PROMOTIONS"), "OfferNo='" & Promotionid & "'", "", DataViewRowState.CurrentRows)
            For Each dr As DataRowView In dv
                If Not dr("OFFERVALIDATIONREQUIRED") Is DBNull.Value Then
                    If dr("OFFERVALIDATIONREQUIRED") = True Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Next
        Catch ex As Exception
        End Try
    End Function
    Public Function GetAllQuestions(ByVal PromotionNo As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = MainPromoDS.Tables("PROMOTIONQUESTIONS").Clone()
            Dim dv As New DataView(MainPromoDS.Tables("PROMOTIONQUESTIONS"), "OfferNo='" & PromotionNo & "'", "", DataViewRowState.CurrentRows)
            For Each dr As DataRowView In dv
                dt.ImportRow(dr.Row)
            Next
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function GetManualPromotions(ByVal sitecode As String, Optional ByVal PromoId As String = "") As DataTable
        Try
            Dim StrQuery As String
            StrQuery = "SELECT A.PROMOTIONID,A.PROMOTIONNAME,A.PROMOTIONVALUE,ISNULL(A.DISCPER,0)AS DISCPER,ISNULL(A.FIXEDPRICEOFF,0)as FIXEDPRICEOFF,"
            StrQuery = StrQuery & "ISNULL(A.FIXEDSELLING,0)as FIXEDSELLING,A.CREATEDAT,A.CREATEDBY,A.CREATEDON,A.UPDATEDAT,A.UPDATEDBY,A.UPDATEDON,"
            StrQuery = StrQuery & "ISNULL(A.STATUS,0) as STATUS FROM MANUALPROMOTION A INNER JOIN PROMOTIONSITEMAP B ON 	A.PROMOTIONID=B.OFFERNO AND B.SITECODE='" & sitecode & "' WHERE A.STATUS=1 "
            'StrQuery = "SELECT * FROM MANUALPROMOTION WHERE STATUS=1 "
            If PromoId <> String.Empty Then
                StrQuery = StrQuery & " And A.PROMOTIONID='" & PromoId & "'"
            End If
            Dim dtPromo As New DataTable
            Dim daPromo As New SqlDataAdapter(StrQuery, ConString)
            daPromo.Fill(dtPromo)
            dtPromo.TableName = "MANUALPROMOTION"
            Return dtPromo
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SaveManualPromotions(ByRef dtTemp As DataTable) As Boolean
        Try
            Dim tran As SqlTransaction
            Dim ServerDate As DateTime = GetCurrentDate()
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If SaveData(dtTemp, SpectrumCon, tran) = True Then
                tran.Commit()
                CloseConnection()
                Return True
            End If
            tran.Rollback()
            CloseConnection()
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region
#Region "Private Function's & Method's"
    ''' <summary>
    ''' Get the List of activate Promotion's
    ''' </summary>
    ''' <UpdatedBy></UpdatedBy>
    ''' <usedin>CashMemo</usedin>
    ''' <UpdatedOn></UpdatedOn>
    ''' <param name="Sitecode">SiteCode</param>
    ''' <param name="OfferNo">OfferId</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Private Function getActivePromo(ByVal Sitecode As String, ByVal OfferNo As String, Optional ByVal cashierTrigg As Boolean = False, Optional ByVal CustomerTrigg As Boolean = False, Optional ByVal promoid As String = "", Optional DayOpenDate As String = "", Optional ByVal CustWisePrice As Boolean = False, Optional ByVal ArticelCode As String = "", Optional ByVal CustNo As String = "", Optional ByVal IsHashTagApplicable As Boolean = False, Optional ByVal UserSelectedPromoHastTag As DataTable = Nothing, Optional ByVal dtBNQPXPYDHashTagPromo As DataTable = Nothing) As DataSet
        Try
            Dim dtTemp As New DataTable
            Dim DaPromo As SqlDataAdapter
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0
            SqlQuery.Append("SELECT A.OFFERNO,A.OFFERNAME,isnull(A.OFFERPRIORITYNO,0)as OFFERPRIORITYNO,A.OFFERTYPE,A.N_QTY,A.M_QTY,")
            SqlQuery.Append("A.R_AMT,A.P_DISPER,A.Q_DISAMT,A.ItemLevel,A.TopUpLevel,A.OFFERVALIDATIONREQUIRED,A.ISBATCHAPP,")
            SqlQuery.Append("ISNULL(C.LEVELON,'') + ISNULL(D.LEVELON,'') AS LEVELON,C.ISX,C.DISCOUNTTYPE,C.VALUE,")
            SqlQuery.Append("ISNULL(C.ARTICLECODE,'') + ISNULL(D.ARTICLECODE,'') AS ARTICLECODE,")
            SqlQuery.Append("ISNULL(C.BATCHNO,'')+ ISNULL(D.BATCHNO,'') AS BATCHNO,D.RANGEON,D.RANGEFROM,D.RANGETO,")
            SqlQuery.Append("D.SLABDISCOUNTPERCENTAGE, D.SLABDISCOUNTAMT, D.SLABDISCOUNTEDPRICE,D.PROMOSLIPTEXT,C.NODECODE ")
            SqlQuery.Append("FROM PROMOTIONS A INNER JOIN PROMOTIONSITEMAP B ON A.OFFERNO=B.OFFERNO ")
            SqlQuery.Append("LEFT OUTER JOIN PROMOTIONDETAILS C ON A.OFFERNO=C.OFFERNO ")
            SqlQuery.Append("LEFT OUTER JOIN PROMOTIONPOWERPRICINGDETAILS D ON A.OFFERNO=D.OFFERNO ")
            SqlQuery.Append("WHERE B.SITECODE='" & Sitecode & "' AND A.ISAPPROVED=1 AND A.OFFERACTIVE=1 AND isnull(C.Status,1)=1 AND ISNULL(B.STATUS,1) = 1  AND A.STATUS ='1' ")
            If OfferNo <> String.Empty Then
                SqlQuery.Append(" AND A.OfferNo=" & OfferNo)
            End If
            If cashierTrigg = True Then
                SqlQuery.Append(" AND A.OfferTriggeredBy='Cashier'")
            ElseIf CustomerTrigg = True Then
                SqlQuery.Append(" AND A.OfferTriggeredBy='Customer' and A.OfferNo='" + promoid + "'")
            Else
                SqlQuery.Append(" AND A.OfferTriggeredBy='Default'")
            End If
            If DayOpenDate <> String.Empty Then
                SqlQuery.Append(" And a.StartDate <='" & Convert.ToDateTime(DayOpenDate).ToString("yyyy-MM-dd") & "'")
                SqlQuery.Append(" And a.Enddate >='" & Convert.ToDateTime(DayOpenDate).ToString("yyyy-MM-dd") & "'")
            Else
                SqlQuery.Append(" AND CAST(GETDATE()as DATE) between A.STARTDATE AND A.ENDDATE ")
            End If
            If IsHashTagApplicable = True Then
                SqlQuery.Append(" AND A.OFFERNAME  NOT LIKE 'HASHTAG%' ")
            End If

            DaPromo = New SqlDataAdapter(SqlQuery.ToString(), ConString)
            DaPromo.Fill(dtTemp)
            If IsHashTagApplicable = True Then
                If Not UserSelectedPromoHastTag Is Nothing AndAlso UserSelectedPromoHastTag.Rows.Count > 0 AndAlso cashierTrigg = False Then
                    dtTemp.Merge(UserSelectedPromoHastTag)
                    dtTemp.AcceptChanges()
                End If

            End If

            dtTemp.TableName = "PROMOTIONS"
            MainPromoDS.Tables.Add(dtTemp)
            ChangePromotionDs()

            SqlQuery.Length = 0
            dtTemp = New DataTable
            SqlQuery.Append("SELECT A.OFFERNO,A.LEVELON,A.ISX,A.DISCOUNTTYPE,A.VALUE,")
            SqlQuery.Append("A.BPSHAREPERCENTAGE, A.ARTICLECODE, A.TREECODE, A.LEVELCODE, A.NODECODE, A.BATCHNO ")
            SqlQuery.Append("FROM PROMOTIONDETAILS A INNER JOIN PROMOTIONSITEMAP B ON A.OFFERNO=B.OFFERNO INNER JOIN PROMOTIONS C ON C.OFFERNO=A.OFFERNO WHERE B.SITECODE='" & Sitecode & "'")
            'SqlQuery.Append(" AND C.ISAPPROVED=1 AND A.Status=1 AND C.OFFERACTIVE=1 AND GETDATE() BETWEEN C.STARTDATE AND C.ENDDATE")
            SqlQuery.Append(" AND C.ISAPPROVED=1 AND A.Status=1 AND C.OFFERACTIVE=1  ")
            If OfferNo <> String.Empty Then
                SqlQuery.Append(" AND A.OfferNo=" & OfferNo)
            End If
            If cashierTrigg = True Then
                SqlQuery.Append(" AND C.OfferTriggeredBy='Cashier'")
            ElseIf CustomerTrigg = True Then
                SqlQuery.Append(" AND C.OfferTriggeredBy='Customer' and A.OfferNo='" + promoid + "'")
            Else
                SqlQuery.Append(" AND C.OfferTriggeredBy='Default'")
            End If
            If DayOpenDate <> String.Empty Then
                SqlQuery.Append(" And C.StartDate <='" & Convert.ToDateTime(DayOpenDate).ToString("yyyy-MM-dd") & "'")
                SqlQuery.Append(" And C.Enddate >='" & Convert.ToDateTime(DayOpenDate).ToString("yyyy-MM-dd") & "'")
            Else
                SqlQuery.Append(" AND CAST(GETDATE()as DATE) between C.STARTDATE AND C.ENDDATE ")
            End If


            DaPromo = New SqlDataAdapter(SqlQuery.ToString(), ConString)
            DaPromo.Fill(dtTemp)
            If IsHashTagApplicable = True Then
                If Not UserSelectedPromoHastTag Is Nothing AndAlso UserSelectedPromoHastTag.Rows.Count > 0 AndAlso cashierTrigg = False Then
                    If Not dtBNQPXPYDHashTagPromo Is Nothing AndAlso dtBNQPXPYDHashTagPromo.Rows.Count > 0 Then
                        dtTemp.Merge(dtBNQPXPYDHashTagPromo)
                        dtTemp.AcceptChanges()
                    End If
                End If
            End If

            dtTemp.TableName = "PROMOTIONDETAILS"
            MainPromoDS.Tables.Add(dtTemp)

            SqlQuery.Length = 0
            dtTemp = New DataTable
            SqlQuery.Append("SELECT A.OFFERNO,A.LEVELON,ISNULL(A.ARTICLECODE,D.ARTICLECODE) AS ARTICLECODE,A.BATCHNO,A.RANGEON,A.RANGEFROM,A.RANGETO, " & vbCrLf)
            SqlQuery.Append("A.SLABDISCOUNTPERCENTAGE, A.SLABDISCOUNTAMT, A.SLABDISCOUNTEDPRICE, A.PROMOSLIPTEXT " & vbCrLf)
            SqlQuery.Append("FROM PROMOTIONPOWERPRICINGDETAILS A " & vbCrLf)
            SqlQuery.Append("INNER JOIN PROMOTIONSITEMAP B ON A.OFFERNO=B.OFFERNO " & vbCrLf)
            SqlQuery.Append("INNER JOIN PROMOTIONS C ON C.OFFERNO=A.OFFERNO " & vbCrLf)
            SqlQuery.Append("LEFT JOIN PromotionDetails D ON A.OFFERNO=D.OFFERNO " & vbCrLf)
            SqlQuery.Append("WHERE B.SITECODE='" & Sitecode & "' AND C.ISAPPROVED=1 " & vbCrLf)
            'SqlQuery.Append("AND C.OFFERACTIVE=1 AND GETDATE() BETWEEN C.STARTDATE AND C.ENDDATE ")
            SqlQuery.Append("AND C.OFFERACTIVE=1  ") 'AND CAST(GETDATE()as DATE) between C.STARTDATE AND C.ENDDATE
            If OfferNo <> String.Empty Then
                SqlQuery.Append(" AND A.OfferNo=" & OfferNo)
            End If
            If cashierTrigg = True Then
                SqlQuery.Append(" AND C.OfferTriggeredBy='Cashier'")
            ElseIf CustomerTrigg = True Then
                SqlQuery.Append(" AND C.OfferTriggeredBy='Customer' and A.OfferNo='" + promoid + "'")
            Else
                SqlQuery.Append(" AND C.OfferTriggeredBy='Default'")
            End If
            If DayOpenDate <> String.Empty Then
                SqlQuery.Append(" And C.StartDate <='" & Convert.ToDateTime(DayOpenDate).ToString("yyyy-MM-dd") & "'")
                SqlQuery.Append(" And C.Enddate >='" & Convert.ToDateTime(DayOpenDate).ToString("yyyy-MM-dd") & "'")
            Else
                SqlQuery.Append(" AND CAST(GETDATE()as DATE) between C.STARTDATE AND C.ENDDATE ")
            End If
            DaPromo = New SqlDataAdapter(SqlQuery.ToString(), ConString)
            DaPromo.Fill(dtTemp)
            dtTemp.TableName = "PROMOTIONPOWERPRICINGDETAILS"
            MainPromoDS.Tables.Add(dtTemp)

            SqlQuery.Length = 0
            dtTemp = New DataTable
            SqlQuery.Append("SELECT B.OFFERNO,A.QUESTIONNO,A.QUESTIONNAME ")
            SqlQuery.Append("FROM PROMOTIONVALIDATIONQUESTIONS A INNER JOIN PROMOTIONVALIDATIONDETAILS B ")
            SqlQuery.Append("ON A.QUESTIONNO=B.QUESTIONNO WHERE B.ISAPPLICABLE=1")
            If OfferNo <> String.Empty Then
                SqlQuery.Append(" AND B.OfferNo=" & OfferNo)
            End If
            DaPromo = New SqlDataAdapter(SqlQuery.ToString(), ConString)
            DaPromo.Fill(dtTemp)
            dtTemp.TableName = "PROMOTIONQUESTIONS"
            MainPromoDS.Tables.Add(dtTemp)

            'code added  by vipul for Customer wise discount

            Dim dtCustWisePrice As New DataTable
            SqlQuery.Length = 0
            ' SqlQuery.Append("select '0'as OFFERNO,'0'as LEVELON,'0' as ISX ,'0',ArticlePrize ,ArticleCode  from Mst_CustomerWiseArticlePrize p where CardNo ='" & CustNo & "'  and SiteCode='" & Sitecode & "' and status=1 and ArticleCode='" & ArticelCode & "'")
            SqlQuery.Append("select '0'as OFFERNO,'0'as LEVELON,'0' as ISX ,'0',ArticlePrize ,ArticleCode  from Mst_CustomerWiseArticlePrize p where  SiteCode='" & Sitecode & "' and status=1 and ArticleCode='" & ArticelCode & "' and level in (select level from CLPCustomers where cardno = '" & CustNo & "' )")
            DaPromo = New SqlDataAdapter(SqlQuery.ToString(), ConString)
            DaPromo.Fill(dtCustWisePrice)
            dtCustWisePrice.TableName = "ArticleWiseDiscount"
            MainPromoDS.Tables.Add(dtCustWisePrice)
            Return MainPromoDS
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Changed Row's of Specific Promotion's
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChangePromotionDs()
        Try
            Dim DvData As New DataView(MainPromoDS.Tables("PROMOTIONS"), "ITEMLEVEL=True AND TopUpLevel=False AND OFFERTYPE='PPS'", "", DataViewRowState.CurrentRows)
            If DvData.Count > 0 Then
                DvData.AllowEdit = True
                For Each dr As DataRowView In DvData
                    dr("ISX") = 1
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Applying The Promotion on the Main Item Table
    ''' </summary>
    ''' <UpdatedBy></UpdatedBy>
    ''' <usedin>CashMemo</usedin>
    ''' <UpdatedOn></UpdatedOn>
    ''' <remarks></remarks>
    Private Sub StartApplyPromotion(Optional ByVal custwisedtl As Boolean = False, Optional ByVal Articlecode As String = "", Optional ByVal IsHashTagApplicable As Boolean = False, Optional ByVal UserSelectedPromoHastTag As DataTable = Nothing)
        Try
            Dim CustWisePromoApply As Boolean = False
            Dim StrFilter As String = ""
            Dim DvFilterData As New DataView(MainPromoDS.Tables("PROMOTIONS"), "", "", DataViewRowState.CurrentRows)

            Dim dvYSch As New DataView(MainPromoDS.Tables("PROMOTIONDETAILS"), "", "", DataViewRowState.CurrentRows)
            Dim dvPSch As New DataView(MainPromoDS.Tables("PROMOTIONPOWERPRICINGDETAILS"), "", "", DataViewRowState.CurrentRows)
            For Each dr As DataRow In MainDS.Tables(_TableName).Select(_Condition, "", DataViewRowState.CurrentRows)
                'code added by vipul for customer wise discount

                'code added  by vipul for Customer wise price
                Dim dv1 As New DataView(MainPromoDS.Tables("PROMOTIONS"), "(ArticleCode='" & dr("ArticleCode").ToString() & "' OR ArticleCode ='" & dr("LastNodeCode").ToString() & "') AND ISX=1", "OFFERPRIORITYNO,OFFERNO", DataViewRowState.CurrentRows)
                Dim dv3 As New DataView(MainPromoDS.Tables("PROMOTIONS"), "(ArticleCode='" & Articlecode.ToString() & "' OR ArticleCode ='" & dr("LastNodeCode").ToString() & "') AND ISX=1", "OFFERPRIORITYNO,OFFERNO", DataViewRowState.CurrentRows)
                Dim dv2 As New DataView(MainPromoDS.Tables("ArticleWiseDiscount")) '', "(ArticleCode='" & dr("ArticleCode").ToString() & "' OR ArticleCode ='" & dr("LastNodeCode").ToString() & "') AND ISX=1", "OFFERPRIORITYNO,OFFERNO", DataViewRowState.CurrentRows)

                If custwisedtl = True And dv3.Count = 0 And dv2.Count > 0 And CustWisePromoApply = False Then

                    '  If dv1.Count = 0 Then
                    For Each drView As DataRowView In dv2

                        '  Dim dr2 As DataView = MainDS.Tables(_TableName).Select("Articlecode" = Articlecode.ToString, "", "")

                        Dim dr2 As DataRow() = MainDS.Tables("Cashmemodtl").Select(" ArticleCode='" & Articlecode.ToString & "'")


                        Dim TotalAmt As Double = dr2(0)("Quantity") * dr2(0)("SellingPrice")

                        dr2(0)("LineDiscount") = drView("ArticlePrize")   '(TotalAmt * IIf(drView("ArticlePrize") Is DBNull.Value, 0, drView("ArticlePrize"))) / 100
                        dr2(0)("FIRSTLEVEL") = drView("OfferNo")
                        ' dr("TOTALDISCPERCENTAGE") = (dr("Quantity") * dr("SellingPrice"))'IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + drView("VALUE")
                        dr2(0)(_TotalDisc) = IIf(dr2(0)("LINEDISCOUNT") Is DBNull.Value, 0, dr2(0)("LINEDISCOUNT")) + IIf(dr2(0)("CLPDISCOUNT") Is DBNull.Value, 0, dr2(0)("CLPDISCOUNT"))
                        'Added By Gaurav Danani 
                        dr2(0)(_TotalDisc) = Math.Round(dr2(0)(_TotalDisc), DecimalDigits)
                        'Change End
                        dr2(0)("NETAMOUNT") = (dr2(0)(_GrossAmt) - dr2(0)(_TotalDisc)) + IIf(dr2(0)(_ExlusiveTax) Is DBNull.Value, 0, dr2(0)(_ExlusiveTax))
                        dr2(0)("FIRSTLEVELDISC") = dr("LineDiscount")
                        CustWisePromoApply = True
                    Next
                    'End If
                Else
                    If IsHashTagApplicable = True Then
                        If dr("FIRSTLEVEL").ToString = String.Empty Then
                            Dim dv As New DataView(MainPromoDS.Tables("PROMOTIONS"), "(ArticleCode='" & dr("ArticleCode").ToString() & "' OR ArticleCode ='" & dr("LastNodeCode").ToString() & "') AND ISX=1", "OFFERPRIORITYNO,OFFERNO", DataViewRowState.CurrentRows)
                            If dv.Count > 0 Then
                                For Each drView As DataRowView In dv
                                    If drView("ITEMLEVEL") = True And drView("TOPUPLEVEL") = False And dr("FIRSTLEVEL").ToString = String.Empty Or (IsHashTagApplicable = True) Then
                                        If drView("OFFERTYPE") = "BPXD" Then
                                            Dim TotalAmt As Double = dr("Quantity") * dr("SellingPrice")
                                            If drView("DISCOUNTTYPE") = "DD" Then
                                                'dr("LineDiscount") = (TotalAmt * IIf(drView("VALUE") Is DBNull.Value, 0, drView("VALUE"))) / 100
                                                'dr("FIRSTLEVEL") = drView("OfferNo").ToString()
                                                'dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + drView("VALUE")
                                                'dr(_TotalDisc) = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT")) + IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                                                ''Added By Gaurav Danani 
                                                'dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                ''Change End
                                                'dr("NETAMOUNT") = (dr(_GrossAmt) - dr(_TotalDisc)) + IIf(dr(_ExlusiveTax) Is DBNull.Value, 0, dr(_ExlusiveTax))
                                                'dr("FIRSTLEVELDISC") = dr("LineDiscount")


                                                ' ----------------------------------------------------------

                                                Dim TotalFreePer, ApplyAmt As Double

                                                If drView("VALUE").ToString() <> String.Empty Then
                                                    'ApplyAmt = drView("VALUE") * dr("Quantity")
                                                    ' TotalFreePer = (ApplyAmt * 100) / dr("NETAMOUNT") 'for rs
                                                    ' ElseIf drView("VALUE").ToString() <> String.Empty Then
                                                    TotalFreePer = drView("VALUE")
                                                End If
                                                If TotalFreePer > 100 Then
                                                    TotalFreePer = 100
                                                End If
                                                If TotalFreePer > 0 Then
                                                    '  For Each dr As DataRowView In dvApplicableData
                                                    'If IsInclusiveTax Then
                                                    ApplyAmt = ((dr(_GrossAmt) - dr(_TotalDisc)) * TotalFreePer) / 100
                                                    'ApplyAmt = ((dr(_GrossAmt)) * TotalFreePer) / 100
                                                    'Else
                                                    '    ApplyAmt = (dr("NetAmount") * TotalFreePer) / 100
                                                    'End If
                                                    dr("LineDiscount") = IIf(dr("LineDiscount") Is DBNull.Value, 0, dr("LineDiscount")) + ApplyAmt
                                                    If dr("TOTALDISCPERCENTAGE").ToString() <> String.Empty AndAlso ((dr("TOTALDISCPERCENTAGE")) + TotalFreePer) <= 100 Then
                                                        dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + TotalFreePer
                                                    ElseIf dr("TOTALDISCPERCENTAGE").ToString() = "" Then
                                                        dr("TOTALDISCPERCENTAGE") = TotalFreePer
                                                    End If
                                                    'dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + TotalFreePer
                                                    Dim OfferNAME = drView("OFFERNAME").ToString.ToUpper
                                                    If OfferNAME.StartsWith("HASHTAG") Then
                                                        dr("TOPLEVEL") = drView("OfferNo")
                                                        dr("TOPLEVELDISC") = ApplyAmt
                                                    Else
                                                        dr("FIRSTLEVEL") = drView("OfferNo")
                                                        dr("FIRSTLEVELDISC") = IIf(dr("FIRSTLEVELDISC") Is DBNull.Value, 0, dr("FIRSTLEVELDISC")) + ApplyAmt
                                                    End If
                                                    dr(_TotalDisc) = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT")) + IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                                                    'Added By Gaurav Danani 
                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                    'Change End
                                                    dr("NETAMOUNT") = (dr(_GrossAmt) - dr(_TotalDisc)) + IIf(dr(_ExlusiveTax) Is DBNull.Value, 0, dr(_ExlusiveTax))
                                                    '  dr("FIRSTLEVELDISC") = ApplyAmt
                                                    ' Next
                                                End If

                                            ElseIf drView("DISCOUNTTYPE") = "PO" Then
                                                'Dim Perc As Double = (drView("VALUE") * 100) / dr("SellingPrice") 'TotalAmt
                                                'dr("LineDiscount") = drView("VALUE") * dr("Quantity")
                                                'dr("FIRSTLEVEL") = drView("OfferNo")
                                                'dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + Perc
                                                'dr(_TotalDisc) = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT")) + IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                                                ''Added By Gaurav Danani 
                                                'dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                ''Change End
                                                'dr("NETAMOUNT") = (dr(_GrossAmt) - dr(_TotalDisc)) + IIf(dr(_ExlusiveTax) Is DBNull.Value, 0, dr(_ExlusiveTax))
                                                'dr("FIRSTLEVELDISC") = dr("LineDiscount")


                                                ' ----------------------------------------------------------

                                                Dim TotalFreePer, ApplyAmt As Double

                                                If drView("VALUE").ToString() <> String.Empty Then
                                                    ApplyAmt = drView("VALUE") * dr("Quantity")
                                                    TotalFreePer = (ApplyAmt * 100) / dr("GrossAmt") 'for rs
                                                    '  ElseIf drFilterScheme("P_DisPer").ToString() <> String.Empty Then
                                                    ' TotalFreePer = drFilterScheme("P_DisPer")
                                                End If
                                                If TotalFreePer > 100 Then
                                                    TotalFreePer = 100
                                                End If

                                                If TotalFreePer > 0 Then
                                                    '  For Each dr As DataRowView In dvApplicableData
                                                    'If IsInclusiveTax Then
                                                    ApplyAmt = ((dr(_GrossAmt) - dr(_TotalDisc)) * TotalFreePer) / 100
                                                    'ApplyAmt = ((dr(_GrossAmt)) * TotalFreePer) / 100
                                                    'Else
                                                    '    ApplyAmt = (dr("NetAmount") * TotalFreePer) / 100
                                                    'End If
                                                    dr("LineDiscount") = IIf(dr("LineDiscount") Is DBNull.Value, 0, dr("LineDiscount")) + ApplyAmt
                                                    If dr("TOTALDISCPERCENTAGE").ToString() <> String.Empty AndAlso ((dr("TOTALDISCPERCENTAGE")) + TotalFreePer) <= 100 Then
                                                        dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + TotalFreePer
                                                    ElseIf dr("TOTALDISCPERCENTAGE").ToString() = "" Then
                                                        dr("TOTALDISCPERCENTAGE") = TotalFreePer
                                                    End If
                                                    'dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + TotalFreePer

                                                    Dim OfferNAME = drView("OFFERNAME").ToString.ToUpper
                                                    If OfferNAME.StartsWith("HASHTAG") Then
                                                        dr("TOPLEVEL") = drView("OfferNo")
                                                        dr("TOPLEVELDISC") = ApplyAmt
                                                    Else
                                                        dr("FIRSTLEVEL") = drView("OfferNo")
                                                        dr("FIRSTLEVELDISC") = IIf(dr("FIRSTLEVELDISC") Is DBNull.Value, 0, dr("FIRSTLEVELDISC")) + ApplyAmt
                                                    End If
                                                    dr(_TotalDisc) = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT")) + IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                                                    'Added By Gaurav Danani 
                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                    'Change End
                                                    dr("NETAMOUNT") = (dr(_GrossAmt) - dr(_TotalDisc)) + IIf(dr(_ExlusiveTax) Is DBNull.Value, 0, dr(_ExlusiveTax))
                                                    ' dr("FIRSTLEVELDISC") = ApplyAmt
                                                    ' Next
                                                End If
                                                '   ------------------------------------------------------------------
                                            ElseIf drView("DISCOUNTTYPE") = "PS" Then
                                                Dim Perc As Double = ((TotalAmt - (dr("Quantity") * drView("VALUE"))) * 100) / TotalAmt
                                                dr("LineDiscount") = TotalAmt - (dr("Quantity") * drView("VALUE"))
                                                dr("FIRSTLEVEL") = drView("OfferNo")
                                                dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + Perc 'CDbl(dr("TOTALDISCPERCENTAGE").ToString()) + Perc
                                                dr(_TotalDisc) = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT")) + IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                                                'Added By Gaurav Danani 
                                                dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                'Change End
                                                dr("NETAMOUNT") = (dr(_GrossAmt) - dr(_TotalDisc)) + IIf(dr(_ExlusiveTax) Is DBNull.Value, 0, dr(_ExlusiveTax))
                                                dr("FIRSTLEVELDISC") = dr("LineDiscount")
                                            End If
                                        ElseIf drView("OFFERTYPE") = "BNQPXMQD" Then
                                            dvYSch.RowFilter = "Offerno='" & drView("Offerno") & "' AND ArticleCode='" & drView("ArticleCode") & "'"
                                            If dvYSch.Count > 0 Then
                                                If IsDBNull(drView("ISBATCHAPP")) = False AndAlso drView("ISBATCHAPP") = True Then
                                                    Dim dtBatch As DataTable = dvYSch.ToTable(True, "BatchNO")
                                                    For Each drBatch As DataRow In dtBatch.Rows
                                                        Dim dvBatch As New DataView(dvYSch.Table, "OfferNo='" & drView("Offerno") & "' AND BatchNo='" & drBatch(0).ToString() & "'", "", DataViewRowState.CurrentRows)
                                                        If dvBatch.Count > 0 Then
                                                            Dim SchemeQty, TotalBatchQty, nApplicableQty As Int32
                                                            Dim Item As String = ""
                                                            Dim DtArtBatch As DataTable = dvBatch.ToTable(True, "ArticleCode")
                                                            TotalBatchQty = 0
                                                            For Each drArt As DataRow In DtArtBatch.Rows
                                                                TotalBatchQty = TotalBatchQty + IIf(dr.Table.Compute("Sum(QUANTITY)", "ArticleCode='" & drArt(0) & "'") Is DBNull.Value, 0, dr.Table.Compute("Sum(QUANTITY)", "ArticleCode='" & drArt(0) & "'"))
                                                                Item = Item & "'" & drArt(0) & "',"
                                                            Next
                                                            SchemeQty = drView("N_Qty") + drView("M_Qty")
                                                            nApplicableQty = Int(TotalBatchQty / SchemeQty)
                                                            nApplicableQty = nApplicableQty * drView("M_Qty")
                                                            If nApplicableQty <= 0 Then Exit For
                                                            Item = Item.Substring(0, Item.Length - 1)
                                                            Dim dvApply As New DataView(MainDS.Tables(_TableName), "ArticleCode In (" & Item & ") " & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "SellingPrice", DataViewRowState.CurrentRows)
                                                            If dvApply.Count > 0 Then
                                                                dvApply.AllowEdit = True
                                                                For Each drFreeArt As DataRowView In dvApply
                                                                    Dim ApplicableQty As Int32 = drFreeArt("Quantity")
                                                                    Dim TotalAmt As Double = ApplicableQty * drFreeArt("SellingPrice")
                                                                    If ApplicableQty >= nApplicableQty Then
                                                                        If drView("DISCOUNTTYPE") = "DD" Then
                                                                            drFreeArt("LineDiscount") = ((nApplicableQty * drFreeArt("SellingPrice")) * drView("Value")) / 100
                                                                            drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                            drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                            drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                            'Added By Gaurav Danani 
                                                                            dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                            'Change End
                                                                            drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                            drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                        ElseIf drView("DISCOUNTTYPE") = "PO" Then
                                                                            drFreeArt("LineDiscount") = nApplicableQty * drView("Value")
                                                                            drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                            drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                            drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                            'Added By Gaurav Danani 
                                                                            dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                            'Change End
                                                                            drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                            drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                        ElseIf drView("DISCOUNTTYPE") = "PS" Then
                                                                            drFreeArt("LineDiscount") = (nApplicableQty * drFreeArt("SellingPrice")) - (nApplicableQty * drView("Value"))
                                                                            drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                            drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                            drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                            'Added By Gaurav Danani 
                                                                            dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                            'Change End
                                                                            drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                            drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                        End If
                                                                        Exit For
                                                                    Else
                                                                        If drView("DISCOUNTTYPE") = "DD" Then
                                                                            drFreeArt("LineDiscount") = ((ApplicableQty * drFreeArt("SellingPrice")) * drView("Value")) / 100
                                                                            drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                            drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                            drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                            'Added By Gaurav Danani 
                                                                            dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                            'Change End
                                                                            drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                            drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                        ElseIf drView("DISCOUNTTYPE") = "PO" Then
                                                                            drFreeArt("LineDiscount") = ApplicableQty * drView("Value")
                                                                            drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                            drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                            drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                            'Added By Gaurav Danani 
                                                                            dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                            'Change End
                                                                            drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                            drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                        ElseIf drView("DISCOUNTTYPE") = "PS" Then
                                                                            drFreeArt("LineDiscount") = TotalAmt - (ApplicableQty * drView("Value"))
                                                                            drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                            drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                            drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                            'Added By Gaurav Danani 
                                                                            dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                            'Change End
                                                                            drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                            drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                        End If
                                                                        nApplicableQty = nApplicableQty - ApplicableQty
                                                                    End If
                                                                Next
                                                            End If
                                                        End If
                                                    Next
                                                Else
                                                    Dim SchemeQty, TotalBatchQty, nApplicableQty As Int32
                                                    Dim Item As String = ""
                                                    Dim DtArtBatch As DataTable = dvYSch.ToTable(True, "ArticleCode")
                                                    TotalBatchQty = 0
                                                    For Each drArt As DataRow In DtArtBatch.Rows
                                                        TotalBatchQty = TotalBatchQty + IIf(dr.Table.Compute("Sum(QUANTITY)", "ArticleCode='" & drArt(0) & "'") Is DBNull.Value, 0, dr.Table.Compute("Sum(QUANTITY)", "ArticleCode='" & drArt(0) & "'"))
                                                        Item = Item & "'" & drArt(0) & "',"
                                                    Next
                                                    SchemeQty = drView("N_Qty") + drView("M_Qty")
                                                    nApplicableQty = Int(TotalBatchQty / SchemeQty)
                                                    nApplicableQty = nApplicableQty * drView("M_Qty")
                                                    If nApplicableQty <= 0 Then Exit For
                                                    Item = Item.Substring(0, Item.Length - 1)
                                                    Dim dvApply As New DataView(MainDS.Tables(_TableName), "ArticleCode In (" & Item & ")" & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "SellingPrice", DataViewRowState.CurrentRows)
                                                    If dvApply.Count > 0 Then
                                                        dvApply.AllowEdit = True
                                                        For Each drFreeArt As DataRowView In dvApply
                                                            Dim ApplicableQty As Int32 = drFreeArt("Quantity")
                                                            Dim TotalAmt As Double = ApplicableQty * drFreeArt("SellingPrice")
                                                            If ApplicableQty >= nApplicableQty Then
                                                                If drView("DISCOUNTTYPE") = "DD" Then
                                                                    drFreeArt("LineDiscount") = ((nApplicableQty * drFreeArt("SellingPrice")) * drView("Value")) / 100
                                                                    drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                    drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                    drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End
                                                                    drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                    drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                ElseIf drView("DISCOUNTTYPE") = "PO" Then
                                                                    drFreeArt("LineDiscount") = nApplicableQty * drView("Value")
                                                                    drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                    drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                    drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End
                                                                    drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                    drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                ElseIf drView("DISCOUNTTYPE") = "PS" Then
                                                                    drFreeArt("LineDiscount") = TotalAmt - (nApplicableQty * drView("Value"))
                                                                    drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                    drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                    drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End
                                                                    drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                    drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                End If
                                                                Exit For
                                                            Else
                                                                If drView("DISCOUNTTYPE") = "DD" Then
                                                                    drFreeArt("LineDiscount") = ((ApplicableQty * drFreeArt("SellingPrice")) * drView("Value")) / 100
                                                                    drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                    drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                    drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End
                                                                    drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                    drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                ElseIf drView("DISCOUNTTYPE") = "PO" Then
                                                                    drFreeArt("LineDiscount") = ApplicableQty * drView("Value")
                                                                    drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                    drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                    drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End
                                                                    drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                    drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                ElseIf drView("DISCOUNTTYPE") = "PS" Then
                                                                    drFreeArt("LineDiscount") = TotalAmt - (ApplicableQty * drView("Value"))
                                                                    drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                    drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                    drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End
                                                                    drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                    drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                End If
                                                                nApplicableQty = nApplicableQty - ApplicableQty
                                                            End If
                                                        Next
                                                    End If
                                                End If
                                            End If
                                        ElseIf drView("OFFERTYPE") = "BNQPXPYD" Then
                                            If IsDBNull(drView("ISBATCHAPP")) = False AndAlso drView("ISBATCHAPP") = True Then
                                                dvYSch.RowFilter = "OfferNo='" & drView("OFFERNO") & "' AND ArticleCode='" & dr("ArticleCode").ToString() & "'"
                                                Dim dtUniqueBatch As DataTable = dvYSch.ToTable("Batch", True, "BatchNO")
                                                'Dim applicableQtyCounter As Integer = 0
                                                For Each drbatch As DataRow In dtUniqueBatch.Rows
                                                    dvYSch.RowFilter = "OfferNo='" & drView("OFFERNO") & "' And ISX=0 AND BatchNO='" & drbatch(0).ToString() & "'"
                                                    If dvYSch.Count > 0 Then
                                                        For Each drY As DataRowView In dvYSch
                                                            Dim dvBatch As New DataView(dvYSch.Table, "OfferNo='" & drView("Offerno") & "' AND BatchNo='" & drbatch(0).ToString() & "'", "", DataViewRowState.CurrentRows)
                                                            Dim Item As String = ""
                                                            For Each drv As DataRowView In dvBatch
                                                                Item = Item & "'" & drv("ArticleCode") & "',"
                                                            Next
                                                            Item = Item.Substring(0, Item.Length - 1)
                                                            ' Dim TotalbatchQty As Int32 = MainDS.Tables(_TableName).Compute("Sum(Quantity)", " isnull(TOPLEVEL,'')='' And articlecode in (" & Item & ")")
                                                            Dim TotalbatchQty As Int32 = MainDS.Tables(_TableName).Compute("Sum(Quantity)", " articlecode in (" & Item & ")")
                                                            '  Dim dvYMain As New DataView(MainDS.Tables(_TableName), " isnull(TOPLEVEL,'')='' And ArticleCode='" & drY("ArticleCode").ToString() & "'" & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "SellingPrice", DataViewRowState.CurrentRows)
                                                            Dim dvYMain As New DataView(MainDS.Tables(_TableName), "ArticleCode='" & drY("ArticleCode").ToString() & "'" & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "SellingPrice", DataViewRowState.CurrentRows)
                                                            If dvYMain.Count > 0 Then
                                                                dvYMain.AllowEdit = True
                                                                'If IIf(IsDBNull(drView("M_Qty")), 0, drView("M_Qty")) = applicableQtyCounter Then
                                                                '    Exit Sub
                                                                'End If
                                                                If TotalbatchQty >= drView("N_Qty") And dvYMain.Item(0)("Quantity") >= IIf(IsDBNull(drView("M_Qty")), 0, drView("M_Qty")) Then
                                                                    Dim SchemeQty, FreeQty, nApplicableQty As Int32
                                                                    Dim totalAmt As Double = dvYMain.Item(0)("Quantity") * dvYMain.Item(0)("SellingPrice")
                                                                    SchemeQty = drView("N_Qty") + IIf(drView("M_Qty") IsNot DBNull.Value, drView("M_Qty"), 0)
                                                                    'FreeQty = drView("M_Qty")
                                                                    FreeQty = IIf(IsDBNull(drView("M_Qty")), 0, drView("M_Qty"))
                                                                    nApplicableQty = Int(TotalbatchQty / SchemeQty)
                                                                    nApplicableQty = nApplicableQty * FreeQty
                                                                    If dvYMain.Item(0)("Quantity") < nApplicableQty Then
                                                                        nApplicableQty = dvYMain.Item(0)("Quantity")
                                                                    End If
                                                                    'If nApplicableQty > FreeQty Then
                                                                    '    nApplicableQty = FreeQty
                                                                    'End If
                                                                    'applicableQtyCounter = applicableQtyCounter + 1
                                                                    If drY("DISCOUNTTYPE") = "DD" Then
                                                                        'dvYMain.Item(0)("LineDiscount") = ((nApplicableQty * dvYMain.Item(0)("SellingPrice")) * drY("Value")) / 100
                                                                        'dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                        'dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                        ''Added By Gaurav Danani 
                                                                        'dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                        ''Change End
                                                                        'dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                        'dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                                        'dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")

                                                                        ' ----------------------------------------------------------
                                                                        Dim TotalFreePer, ApplyAmt As Double
                                                                        If drY("Value").ToString() <> String.Empty Then
                                                                            ' ApplyAmt = drY("Value") * 1
                                                                            ' TotalFreePer = (ApplyAmt * 100) / dvYMain.Item(0)("NETAMOUNT")
                                                                            TotalFreePer = drY("Value")
                                                                        End If
                                                                        If TotalFreePer > 100 Then
                                                                            TotalFreePer = 100
                                                                        End If
                                                                        If TotalFreePer > 0 Then
                                                                            If String.IsNullOrEmpty(IIf(dvYMain.Item(0)("TOPLEVEL") Is DBNull.Value, String.Empty, dvYMain.Item(0)("TOPLEVEL"))) Then


                                                                                ApplyAmt = (((dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) * TotalFreePer) / 100) / dvYMain.Item(0)("QUANTITY")
                                                                                dvYMain.Item(0)("LineDiscount") = IIf(dvYMain.Item(0)("LineDiscount") Is DBNull.Value, 0, dvYMain.Item(0)("LineDiscount")) + ApplyAmt
                                                                                If dvYMain.Item(0)("TOTALDISCPERCENTAGE").ToString() <> String.Empty AndAlso ((dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + TotalFreePer) <= 100 Then
                                                                                    dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + TotalFreePer
                                                                                ElseIf dvYMain.Item(0)("TOTALDISCPERCENTAGE").ToString() = "" Then
                                                                                    dvYMain.Item(0)("TOTALDISCPERCENTAGE") = TotalFreePer
                                                                                End If
                                                                                Dim OfferNAME = drView("OFFERNAME").ToString.ToUpper
                                                                                If OfferNAME.StartsWith("HASHTAG") Then
                                                                                    dvYMain.Item(0)("TOPLEVEL") = drView("OfferNo")
                                                                                    dvYMain.Item(0)("TOPLEVELDISC") = ApplyAmt
                                                                                Else
                                                                                    dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                                    dvYMain.Item(0)("FIRSTLEVELDISC") = ApplyAmt
                                                                                End If
                                                                                dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                                dvYMain.Item(0)(_TotalDisc) = Math.Round(dvYMain.Item(0)(_TotalDisc), DecimalDigits)
                                                                                dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                            End If
                                                                        End If



                                                                    ElseIf drY("DISCOUNTTYPE") = "PO" Then
                                                                        'dvYMain.Item(0)("LineDiscount") = nApplicableQty * drY("Value")
                                                                        'dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                        'dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                        ''Added By Gaurav Danani 
                                                                        'dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                        ''Change End
                                                                        'dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                        'dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                                        'dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                        ' ----------------------------------------------------------
                                                                        Dim TotalFreePer, ApplyAmt As Double
                                                                        If drY("Value").ToString() <> String.Empty Then
                                                                            ApplyAmt = drY("Value") * 1
                                                                            TotalFreePer = (ApplyAmt * 100) / dvYMain.Item(0)("NETAMOUNT")
                                                                        End If
                                                                        If TotalFreePer > 100 Then
                                                                            TotalFreePer = 100
                                                                        End If
                                                                        If TotalFreePer > 0 Then


                                                                            If String.IsNullOrEmpty(IIf(dvYMain.Item(0)("TOPLEVEL") Is DBNull.Value, String.Empty, dvYMain.Item(0)("TOPLEVEL"))) Then


                                                                                ApplyAmt = ((dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) * TotalFreePer) / 100
                                                                                dvYMain.Item(0)("LineDiscount") = IIf(dvYMain.Item(0)("LineDiscount") Is DBNull.Value, 0, dvYMain.Item(0)("LineDiscount")) + ApplyAmt
                                                                                If dvYMain.Item(0)("TOTALDISCPERCENTAGE").ToString() <> String.Empty AndAlso ((dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + TotalFreePer) <= 100 Then
                                                                                    dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + TotalFreePer
                                                                                ElseIf dvYMain.Item(0)("TOTALDISCPERCENTAGE").ToString() = "" Then
                                                                                    dvYMain.Item(0)("TOTALDISCPERCENTAGE") = TotalFreePer
                                                                                End If
                                                                                Dim OfferNAME = drView("OFFERNAME").ToString.ToUpper
                                                                                If OfferNAME.StartsWith("HASHTAG") Then
                                                                                    dvYMain.Item(0)("TOPLEVEL") = drView("OfferNo")
                                                                                    dvYMain.Item(0)("TOPLEVELDISC") = ApplyAmt
                                                                                Else
                                                                                    dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                                    dvYMain.Item(0)("FIRSTLEVELDISC") = ApplyAmt
                                                                                End If
                                                                                dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                                dvYMain.Item(0)(_TotalDisc) = Math.Round(dvYMain.Item(0)(_TotalDisc), DecimalDigits)
                                                                                dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                            End If
                                                                        End If

                                                                    ElseIf drY("DISCOUNTTYPE") = "PS" Then
                                                                        dvYMain.Item(0)("LineDiscount") = totalAmt - (nApplicableQty * drY("Value"))
                                                                        'dvYMain.Item(0)("LineDiscount") = (nApplicableQty * dvYMain.Item(0)("SellingPrice")) - (nApplicableQty * drY("Value"))
                                                                        dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                        dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                        'Added By Gaurav Danani 
                                                                        dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                        'Change End
                                                                        dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                        dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                                        dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                    End If
                                                                End If

                                                            End If
                                                        Next

                                                    End If

                                                Next
                                            Else
                                                dvYSch.RowFilter = "OfferNo='" & drView("OFFERNO") & "' And ArticleCode<>'" & drView("ArticleCode") & "'"
                                                If dvYSch.Count > 0 Then
                                                    For Each drY As DataRowView In dvYSch
                                                        Dim dvYMain As New DataView(MainDS.Tables(_TableName), " isnull(FIRSTLEVEL,'')='' And ArticleCode='" & drY("ArticleCode").ToString() & "'" & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "", DataViewRowState.CurrentRows)
                                                        If dvYMain.Count > 0 Then
                                                            dvYMain.AllowEdit = True
                                                            If dr("Quantity") >= drView("N_Qty") And dvYMain.Item(0)("Quantity") >= IIf(IsDBNull(drView("M_Qty")), 0, drView("M_Qty")) Then
                                                                Dim SchemeQty, FreeQty, nApplicableQty As Int32
                                                                Dim totalAmt As Double = dvYMain.Item(0)("Quantity") * dvYMain.Item(0)("SellingPrice")
                                                                SchemeQty = drView("N_Qty")
                                                                'FreeQty = drView("M_Qty")
                                                                FreeQty = IIf(IsDBNull(drView("M_Qty")), 0, drView("M_Qty"))
                                                                nApplicableQty = Int(dr("Quantity") / SchemeQty)
                                                                nApplicableQty = nApplicableQty * FreeQty
                                                                If dvYMain.Item(0)("Quantity") < nApplicableQty Then
                                                                    nApplicableQty = dvYMain.Item(0)("Quantity")
                                                                End If
                                                                If drY("DISCOUNTTYPE") = "DD" Then
                                                                    dvYMain.Item(0)("LineDiscount") = ((nApplicableQty * dvYMain.Item(0)("SellingPrice")) * drY("Value")) / 100
                                                                    dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                    dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End
                                                                    dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                    dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                                    dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                ElseIf drY("DISCOUNTTYPE") = "PO" Then
                                                                    dvYMain.Item(0)("LineDiscount") = nApplicableQty * drY("Value")
                                                                    dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                    dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End
                                                                    dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                    dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                                    dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                ElseIf drY("DISCOUNTTYPE") = "PS" Then
                                                                    dvYMain.Item(0)("LineDiscount") = totalAmt - (nApplicableQty * drY("Value"))
                                                                    dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                    dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End
                                                                    dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                    dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                                    dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                End If
                                                            End If

                                                        End If
                                                    Next

                                                End If
                                            End If

                                        ElseIf drView("OFFERTYPE") = "BRPXD" Then
                                            Dim totalAmt As Double = dr("Quantity") * dr("SellingPrice")
                                            If totalAmt >= drView("R_AMT") Then
                                                If drView("DISCOUNTTYPE") = "DD" Then
                                                    dr("LineDiscount") = ((dr("Quantity") * dr("SellingPrice")) * drView("Value")) / 100
                                                    dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + ((dr("LineDiscount") * 100) / totalAmt)
                                                    dr("FIRSTLEVEL") = drView("OfferNo")
                                                    dr(_TotalDisc) = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT")) + IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                                                    'Added By Gaurav Danani 
                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                    'Change End
                                                    dr("NETAMOUNT") = (dr(_GrossAmt) - dr(_TotalDisc)) + IIf(dr(_ExlusiveTax) Is DBNull.Value, 0, dr(_ExlusiveTax))
                                                    dr("FIRSTLEVELDISC") = dr("LineDiscount")
                                                ElseIf drView("DISCOUNTTYPE") = "PO" Then
                                                    dr("LineDiscount") = drView("Value")
                                                    dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + ((dr("LineDiscount") * 100) / totalAmt)
                                                    dr("FIRSTLEVEL") = drView("OfferNo")
                                                    dr(_TotalDisc) = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT")) + IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                                                    'Added By Gaurav Danani 
                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                    'Change End
                                                    dr("NETAMOUNT") = (dr(_GrossAmt) - dr(_TotalDisc)) + IIf(dr(_ExlusiveTax) Is DBNull.Value, 0, dr(_ExlusiveTax))
                                                    dr("FIRSTLEVELDISC") = dr("LineDiscount")
                                                ElseIf drView("DISCOUNTTYPE") = "PS" Then
                                                    dr("LineDiscount") = totalAmt - (dr("Quantity") * drView("Value"))
                                                    dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + ((dr("LineDiscount") * 100) / totalAmt)
                                                    dr("FIRSTLEVEL") = drView("OfferNo")
                                                    dr(_TotalDisc) = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT")) + IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                                                    'Added By Gaurav Danani 
                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                    'Change End
                                                    dr("NETAMOUNT") = (dr(_GrossAmt) - dr(_TotalDisc)) + IIf(dr(_ExlusiveTax) Is DBNull.Value, 0, dr(_ExlusiveTax))
                                                    dr("FIRSTLEVELDISC") = dr("LineDiscount")
                                                End If
                                            End If
                                        ElseIf drView("OFFERTYPE") = "BRPXPYD" Then
                                            If IsDBNull(drView("ISBATCHAPP")) = False AndAlso drView("ISBATCHAPP") = True Then
                                                dvYSch.RowFilter = "OfferNo='" & drView("OFFERNO") & "'"
                                                Dim dtUniqueBatch As DataTable = dvYSch.ToTable("Batch", True, "BatchNO")
                                                For Each drbatch As DataRow In dtUniqueBatch.Rows
                                                    Dim Items As String = ""
                                                    Dim dvBatchItem As New DataView(dvYSch.Table, "OFFERNO='" & drView("OFFERNO").ToString() & "' And BatchNo='" & drbatch(0).ToString() & "' AND IsX=1", "", DataViewRowState.CurrentRows)
                                                    For Each drVbatchitem As DataRowView In dvBatchItem
                                                        Items = Items & "'" & drVbatchitem("ArticleCode").ToString() & "',"
                                                    Next
                                                    Items = Items.Substring(0, Items.Length - 1)
                                                    Dim dvXMain As New DataView(MainDS.Tables(_TableName), "ArticleCode in (" & Items & ") " & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "", DataViewRowState.CurrentRows)

                                                    Dim totalAmt As Double = IIf(dvXMain.ToTable.Compute("Sum(GrossAmt)", "") IsNot DBNull.Value, dvXMain.ToTable.Compute("Sum(GrossAmt)", ""), 0)

                                                    dvBatchItem.RowFilter = "OFFERNO='" & drView("OFFERNO").ToString() & "' And BatchNo='" & drbatch(0).ToString() & "' AND IsX=0"
                                                    Items = ""
                                                    For Each drVbatchitem As DataRowView In dvBatchItem
                                                        Items = Items & "'" & drVbatchitem("ArticleCode").ToString() & "',"
                                                    Next
                                                    Items = Items.Substring(0, Items.Length - 1)
                                                    Dim dvYMain As New DataView(MainDS.Tables(_TableName), "ArticleCode in (" & Items & ") " & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "", DataViewRowState.CurrentRows)
                                                    If totalAmt >= drView("R_AMT") Then
                                                        If dvYMain.Count > 0 Then
                                                            dvYMain.AllowEdit = True
                                                            totalAmt = dvYMain.Item(0)("Quantity") * dvYMain.Item(0)("SellingPrice")
                                                            If dvBatchItem.Item(0)("DISCOUNTTYPE") = "DD" Then
                                                                dvYMain.Item(0)("LineDiscount") = ((dvYMain.Item(0)("Quantity") * dvYMain.Item(0)("SellingPrice")) * dvBatchItem.Item(0)("Value")) / 100
                                                                dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                'Added By Gaurav Danani 
                                                                dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                'Change End
                                                                dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                            ElseIf dvBatchItem.Item(0)("DISCOUNTTYPE") = "PO" Then
                                                                dvYMain.Item(0)("LineDiscount") = dvYMain.Item(0)("Quantity") * dvBatchItem.Item(0)("Value")
                                                                dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                'Added By Gaurav Danani 
                                                                dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                'Change End
                                                                dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                            ElseIf dvBatchItem.Item(0)("DISCOUNTTYPE") = "PS" Then
                                                                dvYMain.Item(0)("LineDiscount") = totalAmt - (dvYMain.Item(0)("Quantity") * dvBatchItem.Item(0)("Value"))
                                                                dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                'Added By Gaurav Danani 
                                                                dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                'Change End
                                                                dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                            End If
                                                        End If
                                                    End If
                                                Next
                                            Else
                                                dvYSch.RowFilter = "OfferNo='" & drView("OFFERNO") & "' And ArticleCode<>'" & drView("ArticleCode") & "'"
                                                For Each drY As DataRowView In dvYSch
                                                    Dim dvYMain As New DataView(MainDS.Tables(_TableName), "ArticleCode='" & drY("ArticleCode").ToString() & "'" & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "", DataViewRowState.CurrentRows)
                                                    If dvYMain.Count > 0 Then
                                                        dvYMain.AllowEdit = True
                                                        Dim totalAmt As Double = dr("Quantity") * dr("SellingPrice")
                                                        If totalAmt >= drView("R_AMT") Then
                                                            totalAmt = dvYMain.Item(0)("Quantity") * dvYMain.Item(0)("SellingPrice")
                                                            If drY("DISCOUNTTYPE") = "DD" Then
                                                                dvYMain.Item(0)("LineDiscount") = ((dvYMain.Item(0)("Quantity") * dvYMain.Item(0)("SellingPrice")) * drY("Value")) / 100
                                                                dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                'Added By Gaurav Danani 
                                                                dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                'Change End
                                                                dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                            ElseIf drY("DISCOUNTTYPE") = "PO" Then
                                                                dvYMain.Item(0)("LineDiscount") = dvYMain.Item(0)("Quantity") * drY("Value")
                                                                dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                'Added By Gaurav Danani 
                                                                dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                'Change End
                                                                dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                            ElseIf drY("DISCOUNTTYPE") = "PS" Then
                                                                dvYMain.Item(0)("LineDiscount") = totalAmt - (dvYMain.Item(0)("Quantity") * drY("Value"))
                                                                dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                'Added By Gaurav Danani 
                                                                dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                'Change End
                                                                dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                            End If
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        ElseIf drView("OFFERTYPE") = "PPS" Then
                                            If IsDBNull(drView("ISBATCHAPP")) = False AndAlso drView("ISBATCHAPP") = True Then
                                                dvPSch.RowFilter = "Offerno='" & drView("Offerno") & "' AND ArticleCode='" & drView("ArticleCode") & "'"
                                                If dvPSch.Count > 0 Then
                                                    Dim dtBatch As DataTable = dvPSch.ToTable(True, "BatchNO")
                                                    For Each drBatch As DataRow In dtBatch.Rows
                                                        Dim StrBatchFilter As String = "OfferNo='" & drView("Offerno") & "' AND BatchNo='" & drBatch(0).ToString() & "'"
                                                        Dim dvBatch As New DataView(dvPSch.Table, StrBatchFilter, "RangeTo DESC", DataViewRowState.CurrentRows)
                                                        If dvBatch.Count > 0 Then
                                                            Dim SchemeQty, TotalBatchQty, nApplicableQty As Int32
                                                            Dim Item As String = ""
                                                            Dim DtArtBatch As DataTable = dvBatch.ToTable(True, "ArticleCode")
                                                            TotalBatchQty = 0
                                                            For Each drArt As DataRow In DtArtBatch.Rows
                                                                TotalBatchQty = TotalBatchQty + IIf(dr.Table.Compute("Sum(QUANTITY)", "ArticleCode='" & drArt(0) & "'") Is DBNull.Value, 0, dr.Table.Compute("Sum(QUANTITY)", "ArticleCode='" & drArt(0) & "'"))
                                                                Item = Item & "'" & drArt(0) & "',"
                                                            Next
                                                            Item = Item.Substring(0, Item.Length - 1)
ReCheck1:
                                                            dvBatch.RowFilter = StrBatchFilter
                                                            If dvBatch.Count > 0 Then
                                                                SchemeQty = dvBatch(0)("RangeFrom")
                                                                If TotalBatchQty >= SchemeQty Then
                                                                    Dim dvApplicable As New DataView(MainDS.Tables(_TableName), "ISNULL(FIRSTLEVEL,'')='' AND  ArticleCode in (" & Item & ")" & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "", DataViewRowState.CurrentRows)
                                                                    For Each drApp As DataRowView In dvApplicable
                                                                        Dim totalAmount As Double = drApp("Quantity") * drApp("SellingPrice")
                                                                        If dvBatch(0)("SLABDISCOUNTPERCENTAGE").ToString() <> String.Empty Then
                                                                            'If IsInclusiveTax Then
                                                                            drApp("LineDiscount") = (drApp(_GrossAmt) * dvBatch(0)("SLABDISCOUNTPERCENTAGE")) / 100
                                                                            'Else
                                                                            '    drApp("LineDiscount") = (drApp("NetAmount") * dvBatch(0)("SLABDISCOUNTPERCENTAGE")) / 100
                                                                            'End If
                                                                            drApp("FIRSTLEVEL") = dvBatch(0)("OfferNo")
                                                                            drApp(_TotalDisc) = IIf(drApp("LINEDISCOUNT") Is DBNull.Value, 0, drApp("LINEDISCOUNT")) + IIf(drApp("CLPDISCOUNT") Is DBNull.Value, 0, drApp("CLPDISCOUNT"))
                                                                            'Added By Gaurav Danani 
                                                                            dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                            'Change End

                                                                            'If (IsInclusiveTax) Then

                                                                            If (MainDS.Tables(_TableName).Columns.Contains("TotalDiscount")) Then
                                                                                drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp(_GrossAmt)
                                                                            Else
                                                                                drApp("TOTALDISCPERCENTAGE") = drApp("Discount") * 100 / drApp(_GrossAmt)
                                                                            End If

                                                                            drApp("NETAMOUNT") = (drApp(_GrossAmt) - drApp(_TotalDisc)) + IIf(drApp(_ExlusiveTax) Is DBNull.Value, 0, drApp(_ExlusiveTax))
                                                                            'Else
                                                                            '    If (MainDS.Tables(_TableName).Columns.Contains("TotalDiscount")) Then
                                                                            '        drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("NetAmount")
                                                                            '    Else
                                                                            '        drApp("TOTALDISCPERCENTAGE") = drApp("Discount") * 100 / drApp("NetAmount")
                                                                            '    End If

                                                                            '    drApp("NETAMOUNT") = (drApp("NetAmount") - drApp(_TotalDisc)) + IIf(drApp(_ExlusiveTax) Is DBNull.Value, 0, drApp(_ExlusiveTax))
                                                                            'End If
                                                                            'drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("GrossAmt")
                                                                            'drApp("NETAMOUNT") = (drApp(_GrossAmt) - drApp(_TotalDisc)) + IIf(drApp(_ExlusiveTax) Is DBNull.Value, 0, drApp(_ExlusiveTax))

                                                                            drApp("FIRSTLEVELDISC") = drApp("LineDiscount")
                                                                        ElseIf dvBatch(0)("SLABDISCOUNTAMT").ToString() <> String.Empty Then
                                                                            drApp("LineDiscount") = dvBatch(0)("SLABDISCOUNTAMT") * drApp("Quantity")
                                                                            drApp("FIRSTLEVEL") = dvBatch(0)("OfferNo")
                                                                            drApp(_TotalDisc) = IIf(drApp("LINEDISCOUNT") Is DBNull.Value, 0, drApp("LINEDISCOUNT")) + IIf(drApp("CLPDISCOUNT") Is DBNull.Value, 0, drApp("CLPDISCOUNT"))
                                                                            'Added By Gaurav Danani 
                                                                            dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                            'Change End

                                                                            If (MainDS.Tables(_TableName).Columns.Contains("TotalDiscount")) Then
                                                                                drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("GrossAmt")
                                                                            Else
                                                                                drApp("TOTALDISCPERCENTAGE") = drApp("Discount") * 100 / drApp("GrossAmt")
                                                                            End If

                                                                            drApp("NETAMOUNT") = (drApp(_GrossAmt) - drApp(_TotalDisc)) + IIf(drApp(_ExlusiveTax) Is DBNull.Value, 0, drApp(_ExlusiveTax))
                                                                            drApp("FIRSTLEVELDISC") = drApp("LineDiscount")
                                                                        ElseIf dvBatch(0)("SLABDISCOUNTEDPRICE").ToString() <> String.Empty Then
                                                                            drApp("LineDiscount") = totalAmount - (dvBatch(0)("SLABDISCOUNTEDPRICE") * drApp("Quantity"))
                                                                            drApp("FIRSTLEVEL") = dvBatch(0)("OfferNo")
                                                                            drApp(_TotalDisc) = IIf(drApp("LINEDISCOUNT") Is DBNull.Value, 0, drApp("LINEDISCOUNT")) + IIf(drApp("CLPDISCOUNT") Is DBNull.Value, 0, drApp("CLPDISCOUNT"))
                                                                            'Added By Gaurav Danani 
                                                                            dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                            'Change End

                                                                            If (MainDS.Tables(_TableName).Columns.Contains("TotalDiscount")) Then
                                                                                drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("GrossAmt")
                                                                            Else
                                                                                drApp("TOTALDISCPERCENTAGE") = drApp("Discount") * 100 / drApp("GrossAmt")
                                                                            End If

                                                                            drApp("NETAMOUNT") = (drApp(_GrossAmt) - drApp(_TotalDisc)) + IIf(drApp(_ExlusiveTax) Is DBNull.Value, 0, drApp(_ExlusiveTax))
                                                                            drApp("FIRSTLEVELDISC") = drApp("LineDiscount")
                                                                        End If
                                                                    Next
                                                                Else
                                                                    StrBatchFilter = StrBatchFilter & " AND RangeFrom < " & SchemeQty
                                                                    GoTo ReCheck1
                                                                End If
                                                            End If
                                                        End If
                                                    Next
                                                End If
                                            Else
                                                Dim StrFilterPromo As String = "Offerno='" & drView("Offerno") & "' AND ArticleCode='" & drView("ArticleCode") & "'"
                                                dvPSch.RowFilter = StrFilterPromo
                                                dvPSch.Sort = "RangeTo desc"
                                                If dvPSch.Count > 0 Then
                                                    Dim SchemeQty, ActualQty As Int32
                                                    ActualQty = IIf(MainDS.Tables(_TableName).Compute("Sum(QUANTITY)", "ArticleCode='" & drView("ArticleCode") & "'" & IIf(_Condition <> String.Empty, " AND " & _Condition, "")) Is DBNull.Value, 0, MainDS.Tables(_TableName).Compute("Sum(QUANTITY)", "ArticleCode='" & drView("ArticleCode") & "'" & IIf(_Condition <> String.Empty, " AND " & _Condition, "")))
Rechk1:
                                                    dvPSch.RowFilter = StrFilterPromo
                                                    If dvPSch.Count > 0 Then
                                                        SchemeQty = dvPSch(0)("RangeFrom")
                                                        If ActualQty >= SchemeQty Then
                                                            Dim dvApplicable As New DataView(MainDS.Tables(_TableName), "ISNULL(FIRSTLEVEL,'')='' AND  ArticleCode='" & drView("ArticleCode") & "'" & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "", DataViewRowState.CurrentRows)
                                                            For Each drApp As DataRowView In dvApplicable
                                                                Dim totalAmount As Double = drApp("Quantity") * drApp("SellingPrice")
                                                                If dvPSch(0)("SLABDISCOUNTPERCENTAGE").ToString() <> String.Empty Then
                                                                    'If IsInclusiveTax Then
                                                                    drApp("LineDiscount") = (drApp(_GrossAmt) * dvPSch(0)("SLABDISCOUNTPERCENTAGE")) / 100
                                                                    'Else
                                                                    'drApp("LineDiscount") = (drApp("NetAmount") * dvPSch(0)("SLABDISCOUNTPERCENTAGE")) / 100
                                                                    'End If
                                                                    drApp("FIRSTLEVEL") = dvPSch(0)("OfferNo")
                                                                    drApp(_TotalDisc) = IIf(drApp("LINEDISCOUNT") Is DBNull.Value, 0, drApp("LINEDISCOUNT")) + IIf(drApp("CLPDISCOUNT") Is DBNull.Value, 0, drApp("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End

                                                                    If (MainDS.Tables(_TableName).Columns.Contains("TotalDiscount")) Then
                                                                        drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("GrossAmt")
                                                                    Else
                                                                        drApp("TOTALDISCPERCENTAGE") = drApp("Discount") * 100 / drApp("GrossAmt")
                                                                    End If

                                                                    drApp("NETAMOUNT") = (drApp(_GrossAmt) - drApp(_TotalDisc)) + IIf(drApp(_ExlusiveTax) Is DBNull.Value, 0, drApp(_ExlusiveTax))
                                                                    drApp("FIRSTLEVELDISC") = drApp("LineDiscount")
                                                                ElseIf dvPSch(0)("SLABDISCOUNTAMT").ToString() <> String.Empty Then
                                                                    drApp("LineDiscount") = dvPSch(0)("SLABDISCOUNTAMT") * drApp("Quantity")
                                                                    drApp("FIRSTLEVEL") = dvPSch(0)("OfferNo")
                                                                    drApp(_TotalDisc) = IIf(drApp("LINEDISCOUNT") Is DBNull.Value, 0, drApp("LINEDISCOUNT")) + IIf(drApp("CLPDISCOUNT") Is DBNull.Value, 0, drApp("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End

                                                                    If (MainDS.Tables(_TableName).Columns.Contains("TotalDiscount")) Then
                                                                        drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("GrossAmt")
                                                                    Else
                                                                        drApp("TOTALDISCPERCENTAGE") = drApp("Discount") * 100 / drApp("GrossAmt")
                                                                    End If

                                                                    drApp("NETAMOUNT") = (drApp(_GrossAmt) - drApp(_TotalDisc)) + IIf(drApp(_ExlusiveTax) Is DBNull.Value, 0, drApp(_ExlusiveTax))
                                                                    drApp("FIRSTLEVELDISC") = drApp("LineDiscount")
                                                                ElseIf dvPSch(0)("SLABDISCOUNTEDPRICE").ToString() <> String.Empty Then
                                                                    drApp("LineDiscount") = totalAmount - (dvPSch(0)("SLABDISCOUNTEDPRICE") * drApp("Quantity"))
                                                                    drApp("FIRSTLEVEL") = dvPSch(0)("OfferNo")
                                                                    drApp(_TotalDisc) = IIf(drApp("LINEDISCOUNT") Is DBNull.Value, 0, drApp("LINEDISCOUNT")) + IIf(drApp("CLPDISCOUNT") Is DBNull.Value, 0, drApp("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End

                                                                    If (MainDS.Tables(_TableName).Columns.Contains("TotalDiscount")) Then
                                                                        drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("GrossAmt")
                                                                    Else
                                                                        drApp("TOTALDISCPERCENTAGE") = drApp("Discount") * 100 / drApp("GrossAmt")
                                                                    End If

                                                                    drApp("NETAMOUNT") = (drApp(_GrossAmt) - drApp(_TotalDisc)) + IIf(drApp(_ExlusiveTax) Is DBNull.Value, 0, drApp(_ExlusiveTax))
                                                                    drApp("FIRSTLEVELDISC") = drApp("LineDiscount")
                                                                End If
                                                            Next
                                                        Else
                                                            StrFilterPromo = StrFilterPromo & " AND RangeFrom < " & SchemeQty
                                                            GoTo Rechk1
                                                        End If
                                                    End If

                                                End If
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        End If
                        '''''''''''
                    Else
                        If dr("FIRSTLEVEL").ToString = String.Empty Then
                            Dim dv As New DataView(MainPromoDS.Tables("PROMOTIONS"), "(ArticleCode='" & dr("ArticleCode").ToString() & "' OR ArticleCode ='" & dr("LastNodeCode").ToString() & "') AND ISX=1", "OFFERPRIORITYNO,OFFERNO", DataViewRowState.CurrentRows)
                            If dv.Count > 0 Then
                                For Each drView As DataRowView In dv
                                    If drView("ITEMLEVEL") = True And drView("TOPUPLEVEL") = False And dr("FIRSTLEVEL").ToString = String.Empty Then
                                        If drView("OFFERTYPE") = "BPXD" Then
                                            Dim TotalAmt As Double = dr("Quantity") * dr("SellingPrice")
                                            If drView("DISCOUNTTYPE") = "DD" Then
                                                dr("LineDiscount") = (TotalAmt * IIf(drView("VALUE") Is DBNull.Value, 0, drView("VALUE"))) / 100
                                                dr("FIRSTLEVEL") = drView("OfferNo").ToString()
                                                dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + drView("VALUE")
                                                dr(_TotalDisc) = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT")) + IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                                                'Added By Gaurav Danani 
                                                dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                'Change End
                                                dr("NETAMOUNT") = (dr(_GrossAmt) - dr(_TotalDisc)) + IIf(dr(_ExlusiveTax) Is DBNull.Value, 0, dr(_ExlusiveTax))
                                                dr("FIRSTLEVELDISC") = dr("LineDiscount")
                                            ElseIf drView("DISCOUNTTYPE") = "PO" Then
                                                Dim Perc As Double = (drView("VALUE") * 100) / dr("SellingPrice") 'TotalAmt
                                                dr("LineDiscount") = drView("VALUE") * dr("Quantity")
                                                dr("FIRSTLEVEL") = drView("OfferNo")
                                                dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + Perc
                                                dr(_TotalDisc) = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT")) + IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                                                'Added By Gaurav Danani 
                                                dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                'Change End
                                                dr("NETAMOUNT") = (dr(_GrossAmt) - dr(_TotalDisc)) + IIf(dr(_ExlusiveTax) Is DBNull.Value, 0, dr(_ExlusiveTax))
                                                dr("FIRSTLEVELDISC") = dr("LineDiscount")
                                            ElseIf drView("DISCOUNTTYPE") = "PS" Then
                                                Dim Perc As Double = ((TotalAmt - (dr("Quantity") * drView("VALUE"))) * 100) / TotalAmt
                                                dr("LineDiscount") = TotalAmt - (dr("Quantity") * drView("VALUE"))
                                                dr("FIRSTLEVEL") = drView("OfferNo")
                                                dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + Perc 'CDbl(dr("TOTALDISCPERCENTAGE").ToString()) + Perc
                                                dr(_TotalDisc) = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT")) + IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                                                'Added By Gaurav Danani 
                                                dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                'Change End
                                                dr("NETAMOUNT") = (dr(_GrossAmt) - dr(_TotalDisc)) + IIf(dr(_ExlusiveTax) Is DBNull.Value, 0, dr(_ExlusiveTax))
                                                dr("FIRSTLEVELDISC") = dr("LineDiscount")
                                            End If
                                        ElseIf drView("OFFERTYPE") = "BNQPXMQD" Then
                                            dvYSch.RowFilter = "Offerno='" & drView("Offerno") & "' AND ArticleCode='" & drView("ArticleCode") & "'"
                                            If dvYSch.Count > 0 Then
                                                If IsDBNull(drView("ISBATCHAPP")) = False AndAlso drView("ISBATCHAPP") = True Then
                                                    Dim dtBatch As DataTable = dvYSch.ToTable(True, "BatchNO")
                                                    For Each drBatch As DataRow In dtBatch.Rows
                                                        Dim dvBatch As New DataView(dvYSch.Table, "OfferNo='" & drView("Offerno") & "' AND BatchNo='" & drBatch(0).ToString() & "'", "", DataViewRowState.CurrentRows)
                                                        If dvBatch.Count > 0 Then
                                                            Dim SchemeQty, TotalBatchQty, nApplicableQty As Int32
                                                            Dim Item As String = ""
                                                            Dim DtArtBatch As DataTable = dvBatch.ToTable(True, "ArticleCode")
                                                            TotalBatchQty = 0
                                                            For Each drArt As DataRow In DtArtBatch.Rows
                                                                TotalBatchQty = TotalBatchQty + IIf(dr.Table.Compute("Sum(QUANTITY)", "ArticleCode='" & drArt(0) & "'") Is DBNull.Value, 0, dr.Table.Compute("Sum(QUANTITY)", "ArticleCode='" & drArt(0) & "'"))
                                                                Item = Item & "'" & drArt(0) & "',"
                                                            Next
                                                            SchemeQty = drView("N_Qty") + drView("M_Qty")
                                                            nApplicableQty = Int(TotalBatchQty / SchemeQty)
                                                            nApplicableQty = nApplicableQty * drView("M_Qty")
                                                            If nApplicableQty <= 0 Then Exit For
                                                            Item = Item.Substring(0, Item.Length - 1)
                                                            Dim dvApply As New DataView(MainDS.Tables(_TableName), "ArticleCode In (" & Item & ") " & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "SellingPrice", DataViewRowState.CurrentRows)
                                                            If dvApply.Count > 0 Then
                                                                dvApply.AllowEdit = True
                                                                For Each drFreeArt As DataRowView In dvApply
                                                                    Dim ApplicableQty As Int32 = drFreeArt("Quantity")
                                                                    Dim TotalAmt As Double = ApplicableQty * drFreeArt("SellingPrice")
                                                                    If ApplicableQty >= nApplicableQty Then
                                                                        If drView("DISCOUNTTYPE") = "DD" Then
                                                                            drFreeArt("LineDiscount") = ((nApplicableQty * drFreeArt("SellingPrice")) * drView("Value")) / 100
                                                                            drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                            drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                            drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                            'Added By Gaurav Danani 
                                                                            dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                            'Change End
                                                                            drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                            drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                        ElseIf drView("DISCOUNTTYPE") = "PO" Then
                                                                            drFreeArt("LineDiscount") = nApplicableQty * drView("Value")
                                                                            drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                            drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                            drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                            'Added By Gaurav Danani 
                                                                            dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                            'Change End
                                                                            drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                            drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                        ElseIf drView("DISCOUNTTYPE") = "PS" Then
                                                                            drFreeArt("LineDiscount") = (nApplicableQty * drFreeArt("SellingPrice")) - (nApplicableQty * drView("Value"))
                                                                            drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                            drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                            drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                            'Added By Gaurav Danani 
                                                                            dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                            'Change End
                                                                            drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                            drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                        End If
                                                                        Exit For
                                                                    Else
                                                                        If drView("DISCOUNTTYPE") = "DD" Then
                                                                            drFreeArt("LineDiscount") = ((ApplicableQty * drFreeArt("SellingPrice")) * drView("Value")) / 100
                                                                            drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                            drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                            drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                            'Added By Gaurav Danani 
                                                                            dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                            'Change End
                                                                            drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                            drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                        ElseIf drView("DISCOUNTTYPE") = "PO" Then
                                                                            drFreeArt("LineDiscount") = ApplicableQty * drView("Value")
                                                                            drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                            drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                            drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                            'Added By Gaurav Danani 
                                                                            dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                            'Change End
                                                                            drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                            drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                        ElseIf drView("DISCOUNTTYPE") = "PS" Then
                                                                            drFreeArt("LineDiscount") = TotalAmt - (ApplicableQty * drView("Value"))
                                                                            drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                            drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                            drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                            'Added By Gaurav Danani 
                                                                            dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                            'Change End
                                                                            drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                            drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                        End If
                                                                        nApplicableQty = nApplicableQty - ApplicableQty
                                                                    End If
                                                                Next
                                                            End If
                                                        End If
                                                    Next
                                                Else
                                                    Dim SchemeQty, TotalBatchQty, nApplicableQty As Int32
                                                    Dim Item As String = ""
                                                    Dim DtArtBatch As DataTable = dvYSch.ToTable(True, "ArticleCode")
                                                    TotalBatchQty = 0
                                                    For Each drArt As DataRow In DtArtBatch.Rows
                                                        TotalBatchQty = TotalBatchQty + IIf(dr.Table.Compute("Sum(QUANTITY)", "ArticleCode='" & drArt(0) & "'") Is DBNull.Value, 0, dr.Table.Compute("Sum(QUANTITY)", "ArticleCode='" & drArt(0) & "'"))
                                                        Item = Item & "'" & drArt(0) & "',"
                                                    Next
                                                    SchemeQty = drView("N_Qty") + drView("M_Qty")
                                                    nApplicableQty = Int(TotalBatchQty / SchemeQty)
                                                    nApplicableQty = nApplicableQty * drView("M_Qty")
                                                    If nApplicableQty <= 0 Then Exit For
                                                    Item = Item.Substring(0, Item.Length - 1)
                                                    Dim dvApply As New DataView(MainDS.Tables(_TableName), "ArticleCode In (" & Item & ")" & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "SellingPrice", DataViewRowState.CurrentRows)
                                                    If dvApply.Count > 0 Then
                                                        dvApply.AllowEdit = True
                                                        For Each drFreeArt As DataRowView In dvApply
                                                            Dim ApplicableQty As Int32 = drFreeArt("Quantity")
                                                            Dim TotalAmt As Double = ApplicableQty * drFreeArt("SellingPrice")
                                                            If ApplicableQty >= nApplicableQty Then
                                                                If drView("DISCOUNTTYPE") = "DD" Then
                                                                    drFreeArt("LineDiscount") = ((nApplicableQty * drFreeArt("SellingPrice")) * drView("Value")) / 100
                                                                    drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                    drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                    drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End
                                                                    drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                    drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                ElseIf drView("DISCOUNTTYPE") = "PO" Then
                                                                    drFreeArt("LineDiscount") = nApplicableQty * drView("Value")
                                                                    drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                    drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                    drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End
                                                                    drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                    drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                ElseIf drView("DISCOUNTTYPE") = "PS" Then
                                                                    drFreeArt("LineDiscount") = TotalAmt - (nApplicableQty * drView("Value"))
                                                                    drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                    drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                    drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End
                                                                    drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                    drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                End If
                                                                Exit For
                                                            Else
                                                                If drView("DISCOUNTTYPE") = "DD" Then
                                                                    drFreeArt("LineDiscount") = ((ApplicableQty * drFreeArt("SellingPrice")) * drView("Value")) / 100
                                                                    drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                    drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                    drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End
                                                                    drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                    drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                ElseIf drView("DISCOUNTTYPE") = "PO" Then
                                                                    drFreeArt("LineDiscount") = ApplicableQty * drView("Value")
                                                                    drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                    drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                    drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End
                                                                    drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                    drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                ElseIf drView("DISCOUNTTYPE") = "PS" Then
                                                                    drFreeArt("LineDiscount") = TotalAmt - (ApplicableQty * drView("Value"))
                                                                    drFreeArt("TOTALDISCPERCENTAGE") = IIf(drFreeArt("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drFreeArt("TOTALDISCPERCENTAGE")) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                    drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                                    drFreeArt(_TotalDisc) = IIf(drFreeArt("LINEDISCOUNT") Is DBNull.Value, 0, drFreeArt("LINEDISCOUNT")) + IIf(drFreeArt("CLPDISCOUNT") Is DBNull.Value, 0, drFreeArt("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End
                                                                    drFreeArt("NETAMOUNT") = (drFreeArt(_GrossAmt) - drFreeArt(_TotalDisc)) + IIf(drFreeArt(_ExlusiveTax) Is DBNull.Value, 0, drFreeArt(_ExlusiveTax))
                                                                    drFreeArt("FIRSTLEVELDISC") = drFreeArt("LineDiscount")
                                                                End If
                                                                nApplicableQty = nApplicableQty - ApplicableQty
                                                            End If
                                                        Next
                                                    End If
                                                End If
                                            End If
                                        ElseIf drView("OFFERTYPE") = "BNQPXPYD" Then
                                            If IsDBNull(drView("ISBATCHAPP")) = False AndAlso drView("ISBATCHAPP") = True Then
                                                dvYSch.RowFilter = "OfferNo='" & drView("OFFERNO") & "' AND ArticleCode='" & dr("ArticleCode").ToString() & "'"
                                                Dim dtUniqueBatch As DataTable = dvYSch.ToTable("Batch", True, "BatchNO")
                                                'Dim applicableQtyCounter As Integer = 0
                                                For Each drbatch As DataRow In dtUniqueBatch.Rows
                                                    dvYSch.RowFilter = "OfferNo='" & drView("OFFERNO") & "' And ISX=0 AND BatchNO='" & drbatch(0).ToString() & "'"
                                                    If dvYSch.Count > 0 Then
                                                        For Each drY As DataRowView In dvYSch
                                                            Dim dvBatch As New DataView(dvYSch.Table, "OfferNo='" & drView("Offerno") & "' AND BatchNo='" & drbatch(0).ToString() & "'", "", DataViewRowState.CurrentRows)
                                                            Dim Item As String = ""
                                                            For Each drv As DataRowView In dvBatch
                                                                Item = Item & "'" & drv("ArticleCode") & "',"
                                                            Next
                                                            Item = Item.Substring(0, Item.Length - 1)
                                                            Dim TotalbatchQty As Int32 = MainDS.Tables(_TableName).Compute("Sum(Quantity)", " isnull(FIRSTLEVEL,'')='' And articlecode in (" & Item & ")")
                                                            Dim dvYMain As New DataView(MainDS.Tables(_TableName), " isnull(FIRSTLEVEL,'')='' And ArticleCode='" & drY("ArticleCode").ToString() & "'" & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "SellingPrice", DataViewRowState.CurrentRows)
                                                            If dvYMain.Count > 0 Then
                                                                dvYMain.AllowEdit = True
                                                                'If IIf(IsDBNull(drView("M_Qty")), 0, drView("M_Qty")) = applicableQtyCounter Then
                                                                '    Exit Sub
                                                                'End If
                                                                If TotalbatchQty >= drView("N_Qty") And dvYMain.Item(0)("Quantity") >= IIf(IsDBNull(drView("M_Qty")), 0, drView("M_Qty")) Then
                                                                    Dim SchemeQty, FreeQty, nApplicableQty As Int32
                                                                    Dim totalAmt As Double = dvYMain.Item(0)("Quantity") * dvYMain.Item(0)("SellingPrice")
                                                                    SchemeQty = drView("N_Qty") + IIf(drView("M_Qty") IsNot DBNull.Value, drView("M_Qty"), 0)
                                                                    'FreeQty = drView("M_Qty")
                                                                    FreeQty = IIf(IsDBNull(drView("M_Qty")), 0, drView("M_Qty"))
                                                                    nApplicableQty = Int(TotalbatchQty / SchemeQty)
                                                                    nApplicableQty = nApplicableQty * FreeQty
                                                                    If dvYMain.Item(0)("Quantity") < nApplicableQty Then
                                                                        nApplicableQty = dvYMain.Item(0)("Quantity")
                                                                    End If
                                                                    'If nApplicableQty > FreeQty Then
                                                                    '    nApplicableQty = FreeQty
                                                                    'End If
                                                                    'applicableQtyCounter = applicableQtyCounter + 1
                                                                    If drY("DISCOUNTTYPE") = "DD" Then
                                                                        dvYMain.Item(0)("LineDiscount") = ((nApplicableQty * dvYMain.Item(0)("SellingPrice")) * drY("Value")) / 100
                                                                        dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                        dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                        'Added By Gaurav Danani 
                                                                        dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                        'Change End
                                                                        dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                        dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                                        dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                    ElseIf drY("DISCOUNTTYPE") = "PO" Then
                                                                        dvYMain.Item(0)("LineDiscount") = nApplicableQty * drY("Value")
                                                                        dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                        dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                        'Added By Gaurav Danani 
                                                                        dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                        'Change End
                                                                        dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                        dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                                        dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                    ElseIf drY("DISCOUNTTYPE") = "PS" Then
                                                                        dvYMain.Item(0)("LineDiscount") = totalAmt - (nApplicableQty * drY("Value"))
                                                                        'dvYMain.Item(0)("LineDiscount") = (nApplicableQty * dvYMain.Item(0)("SellingPrice")) - (nApplicableQty * drY("Value"))
                                                                        dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                        dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                        'Added By Gaurav Danani 
                                                                        dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                        'Change End
                                                                        dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                        dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                                        dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                    End If
                                                                End If

                                                            End If
                                                        Next

                                                    End If

                                                Next
                                            Else
                                                dvYSch.RowFilter = "OfferNo='" & drView("OFFERNO") & "' And ArticleCode<>'" & drView("ArticleCode") & "'"
                                                If dvYSch.Count > 0 Then
                                                    For Each drY As DataRowView In dvYSch
                                                        Dim dvYMain As New DataView(MainDS.Tables(_TableName), " isnull(FIRSTLEVEL,'')='' And ArticleCode='" & drY("ArticleCode").ToString() & "'" & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "", DataViewRowState.CurrentRows)
                                                        If dvYMain.Count > 0 Then
                                                            dvYMain.AllowEdit = True
                                                            If dr("Quantity") >= drView("N_Qty") And dvYMain.Item(0)("Quantity") >= IIf(IsDBNull(drView("M_Qty")), 0, drView("M_Qty")) Then
                                                                Dim SchemeQty, FreeQty, nApplicableQty As Int32
                                                                Dim totalAmt As Double = dvYMain.Item(0)("Quantity") * dvYMain.Item(0)("SellingPrice")
                                                                SchemeQty = drView("N_Qty")
                                                                'FreeQty = drView("M_Qty")
                                                                FreeQty = IIf(IsDBNull(drView("M_Qty")), 0, drView("M_Qty"))
                                                                nApplicableQty = Int(dr("Quantity") / SchemeQty)
                                                                nApplicableQty = nApplicableQty * FreeQty
                                                                If dvYMain.Item(0)("Quantity") < nApplicableQty Then
                                                                    nApplicableQty = dvYMain.Item(0)("Quantity")
                                                                End If
                                                                If drY("DISCOUNTTYPE") = "DD" Then
                                                                    dvYMain.Item(0)("LineDiscount") = ((nApplicableQty * dvYMain.Item(0)("SellingPrice")) * drY("Value")) / 100
                                                                    dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                    dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End
                                                                    dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                    dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                                    dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                ElseIf drY("DISCOUNTTYPE") = "PO" Then
                                                                    dvYMain.Item(0)("LineDiscount") = nApplicableQty * drY("Value")
                                                                    dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                    dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End
                                                                    dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                    dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                                    dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                ElseIf drY("DISCOUNTTYPE") = "PS" Then
                                                                    dvYMain.Item(0)("LineDiscount") = totalAmt - (nApplicableQty * drY("Value"))
                                                                    dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                    dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End
                                                                    dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                    dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                                    dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                End If
                                                            End If

                                                        End If
                                                    Next

                                                End If
                                            End If

                                        ElseIf drView("OFFERTYPE") = "BRPXD" Then
                                            Dim totalAmt As Double = dr("Quantity") * dr("SellingPrice")
                                            If totalAmt >= drView("R_AMT") Then
                                                If drView("DISCOUNTTYPE") = "DD" Then
                                                    dr("LineDiscount") = ((dr("Quantity") * dr("SellingPrice")) * drView("Value")) / 100
                                                    dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + ((dr("LineDiscount") * 100) / totalAmt)
                                                    dr("FIRSTLEVEL") = drView("OfferNo")
                                                    dr(_TotalDisc) = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT")) + IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                                                    'Added By Gaurav Danani 
                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                    'Change End
                                                    dr("NETAMOUNT") = (dr(_GrossAmt) - dr(_TotalDisc)) + IIf(dr(_ExlusiveTax) Is DBNull.Value, 0, dr(_ExlusiveTax))
                                                    dr("FIRSTLEVELDISC") = dr("LineDiscount")
                                                ElseIf drView("DISCOUNTTYPE") = "PO" Then
                                                    dr("LineDiscount") = drView("Value")
                                                    dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + ((dr("LineDiscount") * 100) / totalAmt)
                                                    dr("FIRSTLEVEL") = drView("OfferNo")
                                                    dr(_TotalDisc) = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT")) + IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                                                    'Added By Gaurav Danani 
                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                    'Change End
                                                    dr("NETAMOUNT") = (dr(_GrossAmt) - dr(_TotalDisc)) + IIf(dr(_ExlusiveTax) Is DBNull.Value, 0, dr(_ExlusiveTax))
                                                    dr("FIRSTLEVELDISC") = dr("LineDiscount")
                                                ElseIf drView("DISCOUNTTYPE") = "PS" Then
                                                    dr("LineDiscount") = totalAmt - (dr("Quantity") * drView("Value"))
                                                    dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + ((dr("LineDiscount") * 100) / totalAmt)
                                                    dr("FIRSTLEVEL") = drView("OfferNo")
                                                    dr(_TotalDisc) = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT")) + IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                                                    'Added By Gaurav Danani 
                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                    'Change End
                                                    dr("NETAMOUNT") = (dr(_GrossAmt) - dr(_TotalDisc)) + IIf(dr(_ExlusiveTax) Is DBNull.Value, 0, dr(_ExlusiveTax))
                                                    dr("FIRSTLEVELDISC") = dr("LineDiscount")
                                                End If
                                            End If
                                        ElseIf drView("OFFERTYPE") = "BRPXPYD" Then
                                            If IsDBNull(drView("ISBATCHAPP")) = False AndAlso drView("ISBATCHAPP") = True Then
                                                dvYSch.RowFilter = "OfferNo='" & drView("OFFERNO") & "'"
                                                Dim dtUniqueBatch As DataTable = dvYSch.ToTable("Batch", True, "BatchNO")
                                                For Each drbatch As DataRow In dtUniqueBatch.Rows
                                                    Dim Items As String = ""
                                                    Dim dvBatchItem As New DataView(dvYSch.Table, "OFFERNO='" & drView("OFFERNO").ToString() & "' And BatchNo='" & drbatch(0).ToString() & "' AND IsX=1", "", DataViewRowState.CurrentRows)
                                                    For Each drVbatchitem As DataRowView In dvBatchItem
                                                        Items = Items & "'" & drVbatchitem("ArticleCode").ToString() & "',"
                                                    Next
                                                    Items = Items.Substring(0, Items.Length - 1)
                                                    Dim dvXMain As New DataView(MainDS.Tables(_TableName), "ArticleCode in (" & Items & ") " & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "", DataViewRowState.CurrentRows)

                                                    Dim totalAmt As Double = IIf(dvXMain.ToTable.Compute("Sum(GrossAmt)", "") IsNot DBNull.Value, dvXMain.ToTable.Compute("Sum(GrossAmt)", ""), 0)

                                                    dvBatchItem.RowFilter = "OFFERNO='" & drView("OFFERNO").ToString() & "' And BatchNo='" & drbatch(0).ToString() & "' AND IsX=0"
                                                    Items = ""
                                                    For Each drVbatchitem As DataRowView In dvBatchItem
                                                        Items = Items & "'" & drVbatchitem("ArticleCode").ToString() & "',"
                                                    Next
                                                    Items = Items.Substring(0, Items.Length - 1)
                                                    Dim dvYMain As New DataView(MainDS.Tables(_TableName), "ArticleCode in (" & Items & ") " & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "", DataViewRowState.CurrentRows)
                                                    If totalAmt >= drView("R_AMT") Then
                                                        If dvYMain.Count > 0 Then
                                                            dvYMain.AllowEdit = True
                                                            totalAmt = dvYMain.Item(0)("Quantity") * dvYMain.Item(0)("SellingPrice")
                                                            If dvBatchItem.Item(0)("DISCOUNTTYPE") = "DD" Then
                                                                dvYMain.Item(0)("LineDiscount") = ((dvYMain.Item(0)("Quantity") * dvYMain.Item(0)("SellingPrice")) * dvBatchItem.Item(0)("Value")) / 100
                                                                dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                'Added By Gaurav Danani 
                                                                dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                'Change End
                                                                dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                            ElseIf dvBatchItem.Item(0)("DISCOUNTTYPE") = "PO" Then
                                                                dvYMain.Item(0)("LineDiscount") = dvYMain.Item(0)("Quantity") * dvBatchItem.Item(0)("Value")
                                                                dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                'Added By Gaurav Danani 
                                                                dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                'Change End
                                                                dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                            ElseIf dvBatchItem.Item(0)("DISCOUNTTYPE") = "PS" Then
                                                                dvYMain.Item(0)("LineDiscount") = totalAmt - (dvYMain.Item(0)("Quantity") * dvBatchItem.Item(0)("Value"))
                                                                dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                'Added By Gaurav Danani 
                                                                dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                'Change End
                                                                dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                            End If
                                                        End If
                                                    End If
                                                Next
                                            Else
                                                dvYSch.RowFilter = "OfferNo='" & drView("OFFERNO") & "' And ArticleCode<>'" & drView("ArticleCode") & "'"
                                                For Each drY As DataRowView In dvYSch
                                                    Dim dvYMain As New DataView(MainDS.Tables(_TableName), "ArticleCode='" & drY("ArticleCode").ToString() & "'" & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "", DataViewRowState.CurrentRows)
                                                    If dvYMain.Count > 0 Then
                                                        dvYMain.AllowEdit = True
                                                        Dim totalAmt As Double = dr("Quantity") * dr("SellingPrice")
                                                        If totalAmt >= drView("R_AMT") Then
                                                            totalAmt = dvYMain.Item(0)("Quantity") * dvYMain.Item(0)("SellingPrice")
                                                            If drY("DISCOUNTTYPE") = "DD" Then
                                                                dvYMain.Item(0)("LineDiscount") = ((dvYMain.Item(0)("Quantity") * dvYMain.Item(0)("SellingPrice")) * drY("Value")) / 100
                                                                dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                'Added By Gaurav Danani 
                                                                dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                'Change End
                                                                dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                            ElseIf drY("DISCOUNTTYPE") = "PO" Then
                                                                dvYMain.Item(0)("LineDiscount") = dvYMain.Item(0)("Quantity") * drY("Value")
                                                                dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                'Added By Gaurav Danani 
                                                                dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                'Change End
                                                                dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                            ElseIf drY("DISCOUNTTYPE") = "PS" Then
                                                                dvYMain.Item(0)("LineDiscount") = totalAmt - (dvYMain.Item(0)("Quantity") * drY("Value"))
                                                                dvYMain.Item(0)("TOTALDISCPERCENTAGE") = IIf(dvYMain.Item(0)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dvYMain.Item(0)("TOTALDISCPERCENTAGE")) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                                dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                                dvYMain.Item(0)(_TotalDisc) = IIf(dvYMain.Item(0)("LINEDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("LINEDISCOUNT")) + IIf(dvYMain.Item(0)("CLPDISCOUNT") Is DBNull.Value, 0, dvYMain.Item(0)("CLPDISCOUNT"))
                                                                'Added By Gaurav Danani 
                                                                dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                'Change End
                                                                dvYMain.Item(0)("NETAMOUNT") = (dvYMain.Item(0)(_GrossAmt) - dvYMain.Item(0)(_TotalDisc)) + IIf(dvYMain.Item(0)(_ExlusiveTax) Is DBNull.Value, 0, dvYMain.Item(0)(_ExlusiveTax))
                                                                dvYMain.Item(0)("FIRSTLEVELDISC") = dvYMain.Item(0)("LineDiscount")
                                                            End If
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        ElseIf drView("OFFERTYPE") = "PPS" Then
                                            If IsDBNull(drView("ISBATCHAPP")) = False AndAlso drView("ISBATCHAPP") = True Then
                                                dvPSch.RowFilter = "Offerno='" & drView("Offerno") & "' AND ArticleCode='" & drView("ArticleCode") & "'"
                                                If dvPSch.Count > 0 Then
                                                    Dim dtBatch As DataTable = dvPSch.ToTable(True, "BatchNO")
                                                    For Each drBatch As DataRow In dtBatch.Rows
                                                        Dim StrBatchFilter As String = "OfferNo='" & drView("Offerno") & "' AND BatchNo='" & drBatch(0).ToString() & "'"
                                                        Dim dvBatch As New DataView(dvPSch.Table, StrBatchFilter, "RangeTo DESC", DataViewRowState.CurrentRows)
                                                        If dvBatch.Count > 0 Then
                                                            Dim SchemeQty, TotalBatchQty, nApplicableQty As Int32
                                                            Dim Item As String = ""
                                                            Dim DtArtBatch As DataTable = dvBatch.ToTable(True, "ArticleCode")
                                                            TotalBatchQty = 0
                                                            For Each drArt As DataRow In DtArtBatch.Rows
                                                                TotalBatchQty = TotalBatchQty + IIf(dr.Table.Compute("Sum(QUANTITY)", "ArticleCode='" & drArt(0) & "'") Is DBNull.Value, 0, dr.Table.Compute("Sum(QUANTITY)", "ArticleCode='" & drArt(0) & "'"))
                                                                Item = Item & "'" & drArt(0) & "',"
                                                            Next
                                                            Item = Item.Substring(0, Item.Length - 1)
ReCheck:
                                                            dvBatch.RowFilter = StrBatchFilter
                                                            If dvBatch.Count > 0 Then
                                                                SchemeQty = dvBatch(0)("RangeFrom")
                                                                If TotalBatchQty >= SchemeQty Then
                                                                    Dim dvApplicable As New DataView(MainDS.Tables(_TableName), "ISNULL(FIRSTLEVEL,'')='' AND  ArticleCode in (" & Item & ")" & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "", DataViewRowState.CurrentRows)
                                                                    For Each drApp As DataRowView In dvApplicable
                                                                        Dim totalAmount As Double = drApp("Quantity") * drApp("SellingPrice")
                                                                        If dvBatch(0)("SLABDISCOUNTPERCENTAGE").ToString() <> String.Empty Then
                                                                            'If IsInclusiveTax Then
                                                                            drApp("LineDiscount") = (drApp(_GrossAmt) * dvBatch(0)("SLABDISCOUNTPERCENTAGE")) / 100
                                                                            'Else
                                                                            '    drApp("LineDiscount") = (drApp("NetAmount") * dvBatch(0)("SLABDISCOUNTPERCENTAGE")) / 100
                                                                            'End If
                                                                            drApp("FIRSTLEVEL") = dvBatch(0)("OfferNo")
                                                                            drApp(_TotalDisc) = IIf(drApp("LINEDISCOUNT") Is DBNull.Value, 0, drApp("LINEDISCOUNT")) + IIf(drApp("CLPDISCOUNT") Is DBNull.Value, 0, drApp("CLPDISCOUNT"))
                                                                            'Added By Gaurav Danani 
                                                                            dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                            'Change End

                                                                            'If (IsInclusiveTax) Then

                                                                            If (MainDS.Tables(_TableName).Columns.Contains("TotalDiscount")) Then
                                                                                drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp(_GrossAmt)
                                                                            Else
                                                                                drApp("TOTALDISCPERCENTAGE") = drApp("Discount") * 100 / drApp(_GrossAmt)
                                                                            End If

                                                                            drApp("NETAMOUNT") = (drApp(_GrossAmt) - drApp(_TotalDisc)) + IIf(drApp(_ExlusiveTax) Is DBNull.Value, 0, drApp(_ExlusiveTax))
                                                                            'Else
                                                                            '    If (MainDS.Tables(_TableName).Columns.Contains("TotalDiscount")) Then
                                                                            '        drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("NetAmount")
                                                                            '    Else
                                                                            '        drApp("TOTALDISCPERCENTAGE") = drApp("Discount") * 100 / drApp("NetAmount")
                                                                            '    End If

                                                                            '    drApp("NETAMOUNT") = (drApp("NetAmount") - drApp(_TotalDisc)) + IIf(drApp(_ExlusiveTax) Is DBNull.Value, 0, drApp(_ExlusiveTax))
                                                                            'End If
                                                                            'drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("GrossAmt")
                                                                            'drApp("NETAMOUNT") = (drApp(_GrossAmt) - drApp(_TotalDisc)) + IIf(drApp(_ExlusiveTax) Is DBNull.Value, 0, drApp(_ExlusiveTax))

                                                                            drApp("FIRSTLEVELDISC") = drApp("LineDiscount")
                                                                        ElseIf dvBatch(0)("SLABDISCOUNTAMT").ToString() <> String.Empty Then
                                                                            drApp("LineDiscount") = dvBatch(0)("SLABDISCOUNTAMT") * drApp("Quantity")
                                                                            drApp("FIRSTLEVEL") = dvBatch(0)("OfferNo")
                                                                            drApp(_TotalDisc) = IIf(drApp("LINEDISCOUNT") Is DBNull.Value, 0, drApp("LINEDISCOUNT")) + IIf(drApp("CLPDISCOUNT") Is DBNull.Value, 0, drApp("CLPDISCOUNT"))
                                                                            'Added By Gaurav Danani 
                                                                            dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                            'Change End

                                                                            If (MainDS.Tables(_TableName).Columns.Contains("TotalDiscount")) Then
                                                                                drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("GrossAmt")
                                                                            Else
                                                                                drApp("TOTALDISCPERCENTAGE") = drApp("Discount") * 100 / drApp("GrossAmt")
                                                                            End If

                                                                            drApp("NETAMOUNT") = (drApp(_GrossAmt) - drApp(_TotalDisc)) + IIf(drApp(_ExlusiveTax) Is DBNull.Value, 0, drApp(_ExlusiveTax))
                                                                            drApp("FIRSTLEVELDISC") = drApp("LineDiscount")
                                                                        ElseIf dvBatch(0)("SLABDISCOUNTEDPRICE").ToString() <> String.Empty Then
                                                                            drApp("LineDiscount") = totalAmount - (dvBatch(0)("SLABDISCOUNTEDPRICE") * drApp("Quantity"))
                                                                            drApp("FIRSTLEVEL") = dvBatch(0)("OfferNo")
                                                                            drApp(_TotalDisc) = IIf(drApp("LINEDISCOUNT") Is DBNull.Value, 0, drApp("LINEDISCOUNT")) + IIf(drApp("CLPDISCOUNT") Is DBNull.Value, 0, drApp("CLPDISCOUNT"))
                                                                            'Added By Gaurav Danani 
                                                                            dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                            'Change End

                                                                            If (MainDS.Tables(_TableName).Columns.Contains("TotalDiscount")) Then
                                                                                drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("GrossAmt")
                                                                            Else
                                                                                drApp("TOTALDISCPERCENTAGE") = drApp("Discount") * 100 / drApp("GrossAmt")
                                                                            End If

                                                                            drApp("NETAMOUNT") = (drApp(_GrossAmt) - drApp(_TotalDisc)) + IIf(drApp(_ExlusiveTax) Is DBNull.Value, 0, drApp(_ExlusiveTax))
                                                                            drApp("FIRSTLEVELDISC") = drApp("LineDiscount")
                                                                        End If
                                                                    Next
                                                                Else
                                                                    StrBatchFilter = StrBatchFilter & " AND RangeFrom < " & SchemeQty
                                                                    GoTo ReCheck
                                                                End If
                                                            End If
                                                        End If
                                                    Next
                                                End If
                                            Else
                                                Dim StrFilterPromo As String = "Offerno='" & drView("Offerno") & "' AND ArticleCode='" & drView("ArticleCode") & "'"
                                                dvPSch.RowFilter = StrFilterPromo
                                                dvPSch.Sort = "RangeTo desc"
                                                If dvPSch.Count > 0 Then
                                                    Dim SchemeQty, ActualQty As Int32
                                                    ActualQty = IIf(MainDS.Tables(_TableName).Compute("Sum(QUANTITY)", "ArticleCode='" & drView("ArticleCode") & "'" & IIf(_Condition <> String.Empty, " AND " & _Condition, "")) Is DBNull.Value, 0, MainDS.Tables(_TableName).Compute("Sum(QUANTITY)", "ArticleCode='" & drView("ArticleCode") & "'" & IIf(_Condition <> String.Empty, " AND " & _Condition, "")))
Rechk:
                                                    dvPSch.RowFilter = StrFilterPromo
                                                    If dvPSch.Count > 0 Then
                                                        SchemeQty = dvPSch(0)("RangeFrom")
                                                        If ActualQty >= SchemeQty Then
                                                            Dim dvApplicable As New DataView(MainDS.Tables(_TableName), "ISNULL(FIRSTLEVEL,'')='' AND  ArticleCode='" & drView("ArticleCode") & "'" & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "", DataViewRowState.CurrentRows)
                                                            For Each drApp As DataRowView In dvApplicable
                                                                Dim totalAmount As Double = drApp("Quantity") * drApp("SellingPrice")
                                                                If dvPSch(0)("SLABDISCOUNTPERCENTAGE").ToString() <> String.Empty Then
                                                                    'If IsInclusiveTax Then
                                                                    drApp("LineDiscount") = (drApp(_GrossAmt) * dvPSch(0)("SLABDISCOUNTPERCENTAGE")) / 100
                                                                    'Else
                                                                    'drApp("LineDiscount") = (drApp("NetAmount") * dvPSch(0)("SLABDISCOUNTPERCENTAGE")) / 100
                                                                    'End If
                                                                    drApp("FIRSTLEVEL") = dvPSch(0)("OfferNo")
                                                                    drApp(_TotalDisc) = IIf(drApp("LINEDISCOUNT") Is DBNull.Value, 0, drApp("LINEDISCOUNT")) + IIf(drApp("CLPDISCOUNT") Is DBNull.Value, 0, drApp("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End

                                                                    If (MainDS.Tables(_TableName).Columns.Contains("TotalDiscount")) Then
                                                                        drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("GrossAmt")
                                                                    Else
                                                                        drApp("TOTALDISCPERCENTAGE") = drApp("Discount") * 100 / drApp("GrossAmt")
                                                                    End If

                                                                    drApp("NETAMOUNT") = (drApp(_GrossAmt) - drApp(_TotalDisc)) + IIf(drApp(_ExlusiveTax) Is DBNull.Value, 0, drApp(_ExlusiveTax))
                                                                    drApp("FIRSTLEVELDISC") = drApp("LineDiscount")
                                                                ElseIf dvPSch(0)("SLABDISCOUNTAMT").ToString() <> String.Empty Then
                                                                    drApp("LineDiscount") = dvPSch(0)("SLABDISCOUNTAMT") * drApp("Quantity")
                                                                    drApp("FIRSTLEVEL") = dvPSch(0)("OfferNo")
                                                                    drApp(_TotalDisc) = IIf(drApp("LINEDISCOUNT") Is DBNull.Value, 0, drApp("LINEDISCOUNT")) + IIf(drApp("CLPDISCOUNT") Is DBNull.Value, 0, drApp("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End

                                                                    If (MainDS.Tables(_TableName).Columns.Contains("TotalDiscount")) Then
                                                                        drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("GrossAmt")
                                                                    Else
                                                                        drApp("TOTALDISCPERCENTAGE") = drApp("Discount") * 100 / drApp("GrossAmt")
                                                                    End If

                                                                    drApp("NETAMOUNT") = (drApp(_GrossAmt) - drApp(_TotalDisc)) + IIf(drApp(_ExlusiveTax) Is DBNull.Value, 0, drApp(_ExlusiveTax))
                                                                    drApp("FIRSTLEVELDISC") = drApp("LineDiscount")
                                                                ElseIf dvPSch(0)("SLABDISCOUNTEDPRICE").ToString() <> String.Empty Then
                                                                    drApp("LineDiscount") = totalAmount - (dvPSch(0)("SLABDISCOUNTEDPRICE") * drApp("Quantity"))
                                                                    drApp("FIRSTLEVEL") = dvPSch(0)("OfferNo")
                                                                    drApp(_TotalDisc) = IIf(drApp("LINEDISCOUNT") Is DBNull.Value, 0, drApp("LINEDISCOUNT")) + IIf(drApp("CLPDISCOUNT") Is DBNull.Value, 0, drApp("CLPDISCOUNT"))
                                                                    'Added By Gaurav Danani 
                                                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                                                    'Change End

                                                                    If (MainDS.Tables(_TableName).Columns.Contains("TotalDiscount")) Then
                                                                        drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("GrossAmt")
                                                                    Else
                                                                        drApp("TOTALDISCPERCENTAGE") = drApp("Discount") * 100 / drApp("GrossAmt")
                                                                    End If

                                                                    drApp("NETAMOUNT") = (drApp(_GrossAmt) - drApp(_TotalDisc)) + IIf(drApp(_ExlusiveTax) Is DBNull.Value, 0, drApp(_ExlusiveTax))
                                                                    drApp("FIRSTLEVELDISC") = drApp("LineDiscount")
                                                                End If
                                                            Next
                                                        Else
                                                            StrFilterPromo = StrFilterPromo & " AND RangeFrom < " & SchemeQty
                                                            GoTo Rechk
                                                        End If
                                                    End If

                                                End If
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    End If
                End If
            Next
            'Offer aplicable only for that Item
            StrFilter = "ITEMLEVEL=True AND TOPUPLEVEL=True"
            DvFilterData.RowFilter = StrFilter
            If DvFilterData.Count > 0 Then
                For Each drFilterArt As DataRowView In DvFilterData
                    Dim dvData As New DataView(MainDS.Tables(_TableName), "ArticleCode='" & drFilterArt("ArticleCode") & "' AND ISNULL(TOPLEVEL,'')=''" & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "", DataViewRowState.CurrentRows)
                    If dvData.Count > 0 Then
                        Dim TotalAmt As Double = IIf(MainDS.Tables(_TableName).Compute("SUM(NETAmount)", "ArticleCode='" & drFilterArt("ArticleCode") & "' AND ISNULL(TOPLEVEL,'')=''" & IIf(_Condition <> String.Empty, " AND " & _Condition, "")) Is DBNull.Value, 0, MainDS.Tables(_TableName).Compute("SUM(NETAmount)", "ArticleCode='" & drFilterArt("ArticleCode") & "' AND ISNULL(TOPLEVEL,'')=''" & IIf(_Condition <> String.Empty, " AND " & _Condition, "")))
                        If TotalAmt >= IIf(drFilterArt("R_AMT") Is DBNull.Value, 0, drFilterArt("R_AMT")) Then
                            'Dim dvApplicableData As New DataView(MainDS.Tables(_TableName ), "ArticleCode='" & drFilterArt("ArticleCode") & "'AND ISNULL(TOPLEVEL,"")=""", "ArticleCode", DataViewRowState.CurrentRows)
                            'If dvApplicableData.Count > 0 Then
                            Dim TotalFreePer, ApplyAmt As Double
                            If drFilterArt("Q_DisAmt").ToString() <> String.Empty Then
                                ApplyAmt = drFilterArt("Q_DisAmt")
                                TotalFreePer = (ApplyAmt * 100) / TotalAmt
                            ElseIf drFilterArt("P_DisPer").ToString() <> String.Empty Then
                                TotalFreePer = drFilterArt("P_DisPer")
                            End If
                            If TotalFreePer > 0 Then
                                For Each dr As DataRowView In dvData
                                    'If IsInclusiveTax Then
                                    ApplyAmt = ((dr(_GrossAmt) - dr(_TotalDisc)) * TotalFreePer) / 100
                                    'Else
                                    'ApplyAmt = (dr("NetAmount") * TotalFreePer) / 100
                                    'End If
                                    dr("LineDiscount") = IIf(dr("LineDiscount") Is DBNull.Value, 0, dr("LineDiscount")) + ApplyAmt
                                    If dr("TOTALDISCPERCENTAGE").ToString() <> String.Empty AndAlso ((dr("TOTALDISCPERCENTAGE")) + TotalFreePer) <= 100 Then
                                        dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + TotalFreePer
                                    ElseIf dr("TOTALDISCPERCENTAGE").ToString() = "" Then
                                        dr("TOTALDISCPERCENTAGE") = TotalFreePer
                                    End If
                                    'dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + TotalFreePer
                                    dr("TOPLEVEL") = drFilterArt("OfferNo")
                                    dr(_TotalDisc) = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT")) + IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                                    'Added By Gaurav Danani 
                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                    'Change End
                                    dr("NETAMOUNT") = (dr(_GrossAmt) - dr(_TotalDisc)) + IIf(dr(_ExlusiveTax) Is DBNull.Value, 0, dr(_ExlusiveTax))
                                    dr("TOPLEVELDISC") = ApplyAmt
                                Next
                            End If
                            'End If
                        End If
                    End If

                Next
            End If
            'Offer Applicable on Remaning Items on which Item level Not applied

            If IsHashTagApplicable = True Then
                'document
                StrFilter = "ITEMLEVEL=False AND TOPUPLEVEL=False" 'OFFERLEVEL=2 AND OfferApplicableFor='First Level Scheme'"
                DvFilterData.RowFilter = StrFilter
                DvFilterData.Sort = "R_Amt Desc"
                If DvFilterData.Count > 0 Then
                    For Each drFilterScheme As DataRowView In DvFilterData
                        '  Dim Total As String = MainDS.Tables(_TableName).Compute("SUM(NETAmount)", "ISNULL(FIRSTLEVEL,'')='' AND ISNULL(TOPLEVEL,'')=''" & IIf(_Condition <> String.Empty, " AND " & _Condition, "")).ToString()
                        Dim Total As String = MainDS.Tables(_TableName).Compute("SUM(GrossAmt)", IIf(_Condition <> String.Empty, _Condition, "")).ToString()
                        Dim TotalAmt As Double = IIf(Total = String.Empty, 0, Total)
                        If TotalAmt >= IIf(drFilterScheme("R_Amt") Is DBNull.Value, 0, drFilterScheme("R_Amt")) Or IsHashTagApplicable = True Then
                            Dim dvApplicableData As New DataView(MainDS.Tables(_TableName), "ISNULL(TOPLEVEL,'')=''" & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "ArticleCode", DataViewRowState.CurrentRows)
                            '  Dim dvApplicableData As New DataView(MainDS.Tables(_TableName), IIf(_Condition <> String.Empty, _Condition, ""), "ArticleCode", DataViewRowState.CurrentRows)
                            If dvApplicableData.Count > 0 Then
                                Dim TotalFreePer, ApplyAmt As Double

                                If drFilterScheme("Q_DisAmt").ToString() <> String.Empty Then
                                    ApplyAmt = drFilterScheme("Q_DisAmt")
                                    TotalFreePer = (ApplyAmt * 100) / TotalAmt 'for rs
                                ElseIf drFilterScheme("P_DisPer").ToString() <> String.Empty Then
                                    TotalFreePer = drFilterScheme("P_DisPer")
                                End If
                                If TotalFreePer > 100 Then
                                    TotalFreePer = 100
                                End If

                                If TotalFreePer > 0 Then
                                    For Each dr As DataRowView In dvApplicableData
                                        'If IsInclusiveTax Then
                                        ApplyAmt = ((dr(_GrossAmt) - dr(_TotalDisc)) * TotalFreePer) / 100
                                        'Else
                                        '    ApplyAmt = (dr("NetAmount") * TotalFreePer) / 100
                                        'End If
                                        dr("LineDiscount") = IIf(dr("LineDiscount") Is DBNull.Value, 0, dr("LineDiscount")) + ApplyAmt
                                        If dr("TOTALDISCPERCENTAGE").ToString() <> String.Empty AndAlso ((dr("TOTALDISCPERCENTAGE")) + TotalFreePer) <= 100 Then
                                            dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + TotalFreePer
                                        ElseIf dr("TOTALDISCPERCENTAGE").ToString() = "" Then
                                            dr("TOTALDISCPERCENTAGE") = TotalFreePer
                                        End If
                                        'dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + TotalFreePer
                                        ' dr("TOPLEVEL") = drFilterScheme("OfferNo")
                                        Dim OfferNAME = drFilterScheme("OFFERNAME").ToString.ToUpper
                                        If OfferNAME.StartsWith("HASHTAG") Then
                                            dr("TOPLEVEL") = drFilterScheme("OfferNo")
                                            dr("TOPLEVELDISC") = ApplyAmt
                                        Else
                                            dr("FIRSTLEVEL") = drFilterScheme("OfferNo")
                                            dr("FIRSTLEVELDISC") = IIf(dr("FIRSTLEVELDISC") Is DBNull.Value, 0, dr("FIRSTLEVELDISC")) + ApplyAmt
                                        End If
                                        dr(_TotalDisc) = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT")) + IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                                        'Added By Gaurav Danani 
                                        dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                        'Change End
                                        dr("NETAMOUNT") = (dr(_GrossAmt) - dr(_TotalDisc)) + IIf(dr(_ExlusiveTax) Is DBNull.Value, 0, dr(_ExlusiveTax))
                                        ' dr("FIRSTLEVELDISC") = ApplyAmt
                                    Next
                                End If
                            End If
                        End If
                    Next
                End If

            Else
                StrFilter = "ITEMLEVEL=False AND TOPUPLEVEL=False" 'OFFERLEVEL=2 AND OfferApplicableFor='First Level Scheme'"
                DvFilterData.RowFilter = StrFilter
                DvFilterData.Sort = "R_Amt Desc"
                If DvFilterData.Count > 0 Then
                    For Each drFilterScheme As DataRowView In DvFilterData
                        Dim Total As String = MainDS.Tables(_TableName).Compute("SUM(NETAmount)", "ISNULL(FIRSTLEVEL,'')='' AND ISNULL(TOPLEVEL,'')=''" & IIf(_Condition <> String.Empty, " AND " & _Condition, "")).ToString()
                        Dim TotalAmt As Double = IIf(Total = String.Empty, 0, Total)
                        If TotalAmt >= IIf(drFilterScheme("R_Amt") Is DBNull.Value, 0, drFilterScheme("R_Amt")) Then
                            Dim dvApplicableData As New DataView(MainDS.Tables(_TableName), "ISNULL(FIRSTLEVEL,'')=''" & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "ArticleCode", DataViewRowState.CurrentRows)
                            If dvApplicableData.Count > 0 Then
                                Dim TotalFreePer, ApplyAmt As Double
                                If drFilterScheme("Q_DisAmt").ToString() <> String.Empty Then
                                    ApplyAmt = drFilterScheme("Q_DisAmt")
                                    TotalFreePer = (ApplyAmt * 100) / TotalAmt
                                ElseIf drFilterScheme("P_DisPer").ToString() <> String.Empty Then
                                    TotalFreePer = drFilterScheme("P_DisPer")
                                End If
                                If TotalFreePer > 0 Then
                                    For Each dr As DataRowView In dvApplicableData
                                        'If IsInclusiveTax Then
                                        ApplyAmt = ((dr(_GrossAmt) - dr(_TotalDisc)) * TotalFreePer) / 100
                                        'Else
                                        '    ApplyAmt = (dr("NetAmount") * TotalFreePer) / 100
                                        'End If
                                        dr("LineDiscount") = IIf(dr("LineDiscount") Is DBNull.Value, 0, dr("LineDiscount")) + ApplyAmt
                                        If dr("TOTALDISCPERCENTAGE").ToString() <> String.Empty AndAlso ((dr("TOTALDISCPERCENTAGE")) + TotalFreePer) <= 100 Then
                                            dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + TotalFreePer
                                        ElseIf dr("TOTALDISCPERCENTAGE").ToString() = "" Then
                                            dr("TOTALDISCPERCENTAGE") = TotalFreePer
                                        End If
                                        'dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + TotalFreePer
                                        dr("FIRSTLEVEL") = drFilterScheme("OfferNo")
                                        dr(_TotalDisc) = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT")) + IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                                        'Added By Gaurav Danani 
                                        dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                        'Change End
                                        dr("NETAMOUNT") = (dr(_GrossAmt) - dr(_TotalDisc)) + IIf(dr(_ExlusiveTax) Is DBNull.Value, 0, dr(_ExlusiveTax))
                                        dr("FIRSTLEVELDISC") = ApplyAmt
                                    Next
                                End If
                            End If
                        End If
                    Next
                End If
            End If

            'Offer applicable only Item which have no topup scheme applied earlier
            StrFilter = "ITEMLEVEL=False AND TOPUPLEVEL=True" '"OFFERLEVEL=2 AND OfferApplicableFor='Top Up Scheme'"
            DvFilterData.RowFilter = StrFilter
            DvFilterData.Sort = "R_Amt Desc"
            If DvFilterData.Count > 0 Then
                For Each drFilterScheme As DataRowView In DvFilterData
                    Dim Total As String = MainDS.Tables(_TableName).Compute("SUM(NETAmount)", "ISNULL(TOPLEVEL,'')=''" & IIf(_Condition <> String.Empty, " AND " & _Condition, "")).ToString()
                    Dim TotalAmt As Double = IIf(Total = String.Empty, 0, Total)
                    If TotalAmt >= IIf(drFilterScheme("R_Amt") Is DBNull.Value, 0, drFilterScheme("R_Amt")) Then
                        Dim dvApplicableData As New DataView(MainDS.Tables(_TableName), "ISNULL(TOPLEVEL,'')='' " & IIf(_Condition <> String.Empty, " AND " & _Condition, ""), "ArticleCode", DataViewRowState.CurrentRows)
                        If dvApplicableData.Count > 0 Then
                            Dim TotalFreePer, ApplyAmt As Double
                            If drFilterScheme("Q_DisAmt").ToString() <> String.Empty Then
                                ApplyAmt = drFilterScheme("Q_DisAmt")
                                TotalFreePer = (ApplyAmt * 100) / TotalAmt
                            ElseIf drFilterScheme("P_DisPer").ToString() <> String.Empty Then
                                TotalFreePer = drFilterScheme("P_DisPer")
                            End If
                            If TotalFreePer > 0 Then
                                For Each dr As DataRowView In dvApplicableData
                                    'If IsInclusiveTax Then                                       
                                    ApplyAmt = ((dr(_GrossAmt) - dr(_TotalDisc)) * TotalFreePer) / 100
                                    'Else
                                    '    ApplyAmt = (dr("NetAmount") * TotalFreePer) / 100
                                    'End If
                                    dr("LineDiscount") = IIf(dr("LineDiscount") Is DBNull.Value, 0, dr("LineDiscount")) + ApplyAmt
                                    If dr("TOTALDISCPERCENTAGE").ToString() <> String.Empty AndAlso ((dr("TOTALDISCPERCENTAGE")) + TotalFreePer) <= 100 Then
                                        dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, dr("TOTALDISCPERCENTAGE")) + TotalFreePer
                                    ElseIf dr("TOTALDISCPERCENTAGE").ToString() = "" Then
                                        dr("TOTALDISCPERCENTAGE") = TotalFreePer
                                        'Rakesh-24.09.2013:Issue-7722=>Wrong display discount percentage
                                    ElseIf Math.Round(dr("GrossAmt"), 2) = Math.Round(dr("LineDiscount"), 2) Then
                                        dr("TOTALDISCPERCENTAGE") = TotalFreePer
                                    End If
                                    dr("TOPLEVEL") = drFilterScheme("OfferNo")
                                    dr(_TotalDisc) = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT")) + IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                                    'Added By Gaurav Danani 
                                    dr(_TotalDisc) = Math.Round(dr(_TotalDisc), DecimalDigits)
                                    'Change End
                                    dr("NETAMOUNT") = (dr(_GrossAmt) - dr(_TotalDisc)) + IIf(dr(_ExlusiveTax) Is DBNull.Value, 0, dr(_ExlusiveTax))
                                    If dr("FIRSTLEVEL").ToString() = String.Empty Then
                                        dr("FIRSTLEVEL") = drFilterScheme("OfferNo")
                                    End If
                                    dr("TOPLEVELDISC") = ApplyAmt
                                Next
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Get Promotion Slips if any
    ''' </summary>
    ''' <UpdatedBy></UpdatedBy>
    ''' <usedin>CashMemo</usedin>
    ''' <UpdatedOn></UpdatedOn>
    ''' <param name="NetBillAmt"></param>
    ''' <param name="PromotionID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetPromotionSlip(ByVal NetBillAmt As Double, ByRef PromotionID As String) As String
        Try
            Dim PromoSlipText As String = ""
            Dim strCondition As String = "OfferType='PPS' AND ITEMLEVEL=False AND TOPUPLEVEL=True   AND  RANGEFROM < " & ConvertToEnglish(NetBillAmt) & " AND RANGETO > " & ConvertToEnglish(NetBillAmt)
            Dim dvPSlip As New DataView(MainPromoDS.Tables("Promotions"), strCondition, "RANGETO", DataViewRowState.CurrentRows)
            If dvPSlip.Count > 0 Then
                PromoSlipText = dvPSlip(0)("PROMOSLIPTEXT").ToString()
                PromotionID = dvPSlip(0)("OFFERNO").ToString()
            End If
            Return PromoSlipText
        Catch ex As Exception
            LogException(ex)
            Return ""
        End Try
    End Function

    Private Function CheckIfValidPromotion(ByVal offerNo As String, ByVal siteCode As String, Optional DayOpenDate As String = "") As Boolean
        Try
            'added by khusrao adil
            'new parameter DayOpendate for sprint 14 promotion
            Dim isValid As Boolean
            Dim _dayOfWeek As String
            If Not DayOpenDate = Nothing AndAlso Not DayOpenDate = "" Then
                Dim Dweek As New DateTime
                Dweek = Convert.ToDateTime(DayOpenDate)
                _dayOfWeek = Dweek.DayOfWeek.ToString()
            Else
                _dayOfWeek = DateTime.Now.DayOfWeek.ToString()
            End If
            Dim query As String
            query = "select * from PromotionSiteMap where OfferNo = '" & offerNo & "' And SiteCode = '" & siteCode & "'"
            Dim promotionDays As DataTable = GetFilledTable(query)
            If promotionDays IsNot Nothing AndAlso promotionDays.Rows.Count > 0 Then
                If promotionDays.Rows(0)(_dayOfWeek) Then
                    isValid = CheckPromotionStartEndTime(offerNo)
                Else
                    isValid = False
                End If
            Else
                isValid = CheckPromotionStartEndTime(offerNo)
            End If
            Return isValid
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function CheckPromotionStartEndTime(ByVal offerNo As String) As Boolean
        Dim query As String
        Dim startEndTime As DataTable
        query = "Select StartTime,EndTime from Promotions where OfferNo = '" & offerNo & "' "
        startEndTime = GetFilledTable(query)
        If startEndTime IsNot Nothing AndAlso startEndTime.Rows.Count > 0 Then
            Dim startTime = DirectCast(startEndTime.Rows(0)("StartTime"), DateTime).TimeOfDay
            Dim endTime = DirectCast(startEndTime.Rows(0)("EndTime"), DateTime).TimeOfDay
            Dim systemTime = DateTime.Now.TimeOfDay
            If systemTime >= startTime AndAlso systemTime <= endTime Then
                Return True
            End If
        End If
        Return False
    End Function
    Public Function GetAllCashierPromo(ByVal SiteCode As String, Optional ByVal HashTagPromotionId As String = "", Optional ByVal ArticleCode As String = "", Optional ByVal CategaryCode As String = "") As DataTable
        Try
            Dim _dayOfWeek As String
            _dayOfWeek = DateTime.Now.DayOfWeek.ToString()
            Dim dt As New DataTable
            Dim DP As SqlDataAdapter
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0
            SqlQuery.Append("SELECT A.OFFERNO,A.OFFERNAME,isnull(A.OFFERPRIORITYNO,0)as OFFERPRIORITYNO,A.OFFERTYPE,A.N_QTY,A.M_QTY,")
            SqlQuery.Append("A.R_AMT,A.P_DISPER,A.Q_DISAMT,A.ItemLevel,A.TopUpLevel,A.OFFERVALIDATIONREQUIRED,A.ISBATCHAPP,")
            SqlQuery.Append("ISNULL(C.LEVELON,'') + ISNULL(D.LEVELON,'') AS LEVELON,C.ISX,C.DISCOUNTTYPE,C.VALUE,")
            SqlQuery.Append("ISNULL(C.ARTICLECODE,'') + ISNULL(D.ARTICLECODE,'') AS ARTICLECODE,")
            SqlQuery.Append("ISNULL(C.BATCHNO,'')+ ISNULL(D.BATCHNO,'') AS BATCHNO,D.RANGEON,D.RANGEFROM,D.RANGETO,")
            SqlQuery.Append("D.SLABDISCOUNTPERCENTAGE, D.SLABDISCOUNTAMT, D.SLABDISCOUNTEDPRICE,D.PROMOSLIPTEXT,C.NODECODE ")
            SqlQuery.Append("FROM PROMOTIONS A INNER JOIN PROMOTIONSITEMAP B ON A.OFFERNO=B.OFFERNO ")
            SqlQuery.Append("LEFT OUTER JOIN PROMOTIONDETAILS C ON A.OFFERNO=C.OFFERNO ")
            SqlQuery.Append("LEFT OUTER JOIN PROMOTIONPOWERPRICINGDETAILS D ON A.OFFERNO=D.OFFERNO ")
            SqlQuery.Append("WHERE B.SITECODE='" & SiteCode & "' AND A.ISAPPROVED=1 AND A.OFFERACTIVE=1 AND isnull(C.Status,1)=1 AND ISNULL(B.STATUS,1) = 1  AND A.STATUS ='1' ")
            If HashTagPromotionId <> String.Empty Then
                SqlQuery.Append(" AND A.OfferNo= '" & HashTagPromotionId & "'")
            End If
            If ArticleCode <> String.Empty Then
                SqlQuery.Append(" AND C.ARTICLECODE='" & ArticleCode & "'")
            End If
            If CategaryCode <> String.Empty Then
                SqlQuery.Append(" AND C.NODECODE='" & CategaryCode & "'")
            End If
            SqlQuery.Append(" AND A.OfferTriggeredBy='Cashier'")
            If _dayOfWeek <> String.Empty Then
                SqlQuery.Append(" AND B." & _dayOfWeek & "='1'")
            End If


            SqlQuery.Append(" AND CAST(GETDATE()as DATE) between A.STARTDATE AND A.ENDDATE ")
            SqlQuery.Append(" AND A.OFFERNAME LIKE 'HASHTAG%' ")
            DP = New SqlDataAdapter(SqlQuery.ToString(), ConString)
            DP.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Public Function GetAllPromotionDtlforBNQPXPYDType(ByVal SiteCode As String, ByVal OfferNo As String, ByVal ArticleCode As String) As DataTable
        Try

            Dim _dayOfWeek As String
            _dayOfWeek = DateTime.Now.DayOfWeek.ToString()
            Dim dtTemp As New DataTable
            Dim DP As SqlDataAdapter
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0
            dtTemp = New DataTable
            SqlQuery.Append("SELECT A.OFFERNO,A.LEVELON,A.ISX,A.DISCOUNTTYPE,A.VALUE,")
            SqlQuery.Append("A.BPSHAREPERCENTAGE, A.ARTICLECODE, A.TREECODE, A.LEVELCODE, A.NODECODE, A.BATCHNO ")
            SqlQuery.Append("FROM PROMOTIONDETAILS A INNER JOIN PROMOTIONSITEMAP B ON A.OFFERNO=B.OFFERNO INNER JOIN PROMOTIONS C ON C.OFFERNO=A.OFFERNO WHERE B.SITECODE='" & SiteCode & "'")
            'SqlQuery.Append(" AND C.ISAPPROVED=1 AND A.Status=1 AND C.OFFERACTIVE=1 AND GETDATE() BETWEEN C.STARTDATE AND C.ENDDATE")
            SqlQuery.Append(" AND C.ISAPPROVED=1 AND A.Status=1 AND C.OFFERACTIVE=1  ")
            SqlQuery.Append(" AND A.OfferNo='" & OfferNo & "'")
            SqlQuery.Append(" AND C.OfferTriggeredBy='Cashier'")
            SqlQuery.Append(" AND CAST(GETDATE()as DATE) between C.STARTDATE AND C.ENDDATE ")
            SqlQuery.Append(" AND (IsX=1 or(IsX=0 and A.ARTICLECODE='" & ArticleCode & "'))  ")

            DP = New SqlDataAdapter(SqlQuery.ToString(), ConString)
            DP.Fill(dtTemp)
            Return dtTemp
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

#End Region
End Class
