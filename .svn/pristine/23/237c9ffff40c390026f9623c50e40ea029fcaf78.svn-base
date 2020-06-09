Imports System.Data.SqlClient
Imports System.Text

Public Class clsSOApplyPromotion
    Dim MainDS As DataSet
    Dim MainPromoDS As New DataSet

    Public Sub CalculatedDs(ByRef ds As DataSet, ByVal SiteCode As String)
        Try
            MainDS = ds
            getActivePromo(SiteCode, "")
            StartApplyPromotion()
        Catch ex As Exception
        End Try
    End Sub

    Private Function getActivePromo(ByVal Sitecode As String, ByVal OfferNo As String) As DataSet
        Try
            Dim dtTemp As New DataTable
            Dim DaPromo As SqlDataAdapter
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0
            SqlQuery.Append("SELECT A.OFFERNO,A.OFFERNAME,A.OFFERPRIORITYNO,A.OFFERTYPE,A.N_QTY,A.M_QTY,")
            SqlQuery.Append("A.R_AMT,A.P_DISPER,A.Q_DISAMT,A.OFFERLEVEL,A.OFFERAPPLICABLEFOR,A.OFFERVALIDATIONREQUIRED,A.ISBATCHAPP,")
            SqlQuery.Append("ISNULL(C.LEVELON,'') + ISNULL(D.LEVELON,'') AS LEVELON,C.ISX,C.DISCOUNTTYPE,C.VALUE,")
            SqlQuery.Append("ISNULL(C.ARTICLECODE,'') + ISNULL(D.ARTICLECODE,'') AS ARTICLECODE,")
            SqlQuery.Append("ISNULL(C.BATCHNO,'')+ ISNULL(D.BATCHNO,'') AS BATCHNO,D.RANGEON,D.RANGEFROM,D.RANGETO,")
            SqlQuery.Append("D.SLABDISCOUNTPERCENTAGE, D.SLABDISCOUNTAMT, D.SLABDISCOUNTEDPRICE,D.PROMOSLIPTEXT ")
            SqlQuery.Append("FROM PROMOTIONS A INNER JOIN PROMOTIONSITEMAP B ON A.OFFERNO=B.OFFERNO ")
            SqlQuery.Append("LEFT OUTER JOIN PROMOTIONDETAILS C ON A.OFFERNO=C.OFFERNO ")
            SqlQuery.Append("LEFT OUTER JOIN PROMOTIONPOWERPRICINGDETAILS D ON A.OFFERNO=D.OFFERNO ")
            SqlQuery.Append("WHERE B.SITECODE='" & Sitecode & "' AND A.ISAPPROVED=1 AND A.OFFERACTIVE=1 AND GETDATE() BETWEEN A.STARTDATE AND A.ENDDATE ")
            If OfferNo = String.Empty Then
                SqlQuery.Append("AND A.OFFERTRIGGEREDBY='DEFAULT'")
            ElseIf OfferNo <> String.Empty Then
                SqlQuery.Append("AND A.OFFERTRIGGEREDBY='Cashier' AND A.OfferNo=" & OfferNo)
            End If

            DaPromo = New SqlDataAdapter(SqlQuery.ToString(), ConString)
            DaPromo.Fill(dtTemp)
            dtTemp.TableName = "PROMOTIONS"
            MainPromoDS.Tables.Add(dtTemp)
            ChangePromotionDs()

            SqlQuery.Length = 0
            dtTemp = New DataTable
            SqlQuery.Append("SELECT A.OFFERNO,A.LEVELON,A.ISX,A.DISCOUNTTYPE,A.VALUE,")
            SqlQuery.Append("A.BPSHAREPERCENTAGE, A.ARTICLECODE, A.TREECODE, A.LEVELCODE, A.NODECODE, A.BATCHNO ")
            SqlQuery.Append("FROM PROMOTIONDETAILS A INNER JOIN PROMOTIONSITEMAP B ON A.OFFERNO=B.OFFERNO INNER JOIN PROMOTIONS C ON C.OFFERNO=A.OFFERNO WHERE B.SITECODE='" & Sitecode & "'")
            SqlQuery.Append("AND C.ISAPPROVED=1 AND C.OFFERACTIVE=1 AND GETDATE() BETWEEN C.STARTDATE AND C.ENDDATE")
            If OfferNo <> String.Empty Then
                SqlQuery.Append(" AND A.OfferNo=" & OfferNo)
            End If
            DaPromo = New SqlDataAdapter(SqlQuery.ToString(), ConString)
            DaPromo.Fill(dtTemp)
            dtTemp.TableName = "PROMOTIONDETAILS"
            MainPromoDS.Tables.Add(dtTemp)

            SqlQuery.Length = 0
            dtTemp = New DataTable
            SqlQuery.Append("SELECT A.OFFERNO,A.LEVELON,A.ARTICLECODE,A.BATCHNO,A.RANGEON,A.RANGEFROM,A.RANGETO,")
            SqlQuery.Append("A.SLABDISCOUNTPERCENTAGE, A.SLABDISCOUNTAMT, A.SLABDISCOUNTEDPRICE, A.PROMOSLIPTEXT ")
            SqlQuery.Append("FROM PROMOTIONPOWERPRICINGDETAILS A INNER JOIN PROMOTIONSITEMAP B ON A.OFFERNO=B.OFFERNO INNER JOIN PROMOTIONS C ON C.OFFERNO=A.OFFERNO WHERE B.SITECODE='" & Sitecode & "'")
            SqlQuery.Append(" AND C.ISAPPROVED=1 AND C.OFFERACTIVE=1 AND GETDATE() BETWEEN C.STARTDATE AND C.ENDDATE")
            If OfferNo <> String.Empty Then
                SqlQuery.Append(" AND A.OfferNo=" & OfferNo)
            End If
            DaPromo = New SqlDataAdapter(SqlQuery.ToString(), ConString)
            DaPromo.Fill(dtTemp)
            dtTemp.TableName = "PROMOTIONPOWERPRICINGDETAILS"
            MainPromoDS.Tables.Add(dtTemp)

            Return MainPromoDS
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Private Sub ChangePromotionDs()
        Try
            Dim DvData As New DataView(MainPromoDS.Tables("PROMOTIONS"), "OFFERLEVEL=1 AND OfferApplicableFor='First Level Scheme' AND OFFERTYPE='PPS'", "", DataViewRowState.CurrentRows)
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

    Private Sub StartApplyPromotion()
        Try

            Dim StrFilter As String = ""
            Dim DvFilterData As New DataView(MainPromoDS.Tables("PROMOTIONS"), "", "", DataViewRowState.CurrentRows)

            Dim dvYSch As New DataView(MainPromoDS.Tables("PROMOTIONDETAILS"), "", "", DataViewRowState.CurrentRows)
            Dim dvPSch As New DataView(MainPromoDS.Tables("PROMOTIONPOWERPRICINGDETAILS"), "", "", DataViewRowState.CurrentRows)

            For Each dr As DataRow In MainDS.Tables("ItemScanDetails").Rows
                If dr("FIRSTLEVEL").ToString = String.Empty Then

                    Dim dv As New DataView(MainPromoDS.Tables("PROMOTIONS"), "ArticleCode='" & dr("ArticleCode").ToString() & "' AND ISX=1", "OFFERNO", DataViewRowState.CurrentRows)
                    If dv.Count > 0 Then

                        For Each drView As DataRowView In dv
                            If drView("OFFERLEVEL") = 1 And drView("OfferApplicableFor") = "First Level Scheme" Then

                                If drView("OFFERTYPE") = "BPXD" Then
                                    Dim TotalAmt As Double = dr("OrderQty") * dr("SellingPrice")

                                    If drView("DISCOUNTTYPE") = "DD" Then
                                        dr("LineDiscount") = (TotalAmt * drView("VALUE")) / 100
                                        dr("FIRSTLEVEL") = drView("OfferNo")
                                        dr("TOTALDISCPERCENTAGE") = CDbl(dr("TOTALDISCPERCENTAGE").ToString()) + drView("VALUE")
                                    ElseIf drView("DISCOUNTTYPE") = "PO" Then
                                        Dim Perc As Double = (drView("VALUE") * 100) / TotalAmt
                                        dr("LineDiscount") = drView("VALUE")
                                        dr("FIRSTLEVEL") = drView("OfferNo")
                                        dr("TOTALDISCPERCENTAGE") = CDbl(dr("TOTALDISCPERCENTAGE").ToString()) + Perc
                                    ElseIf drView("DISCOUNTTYPE") = "PS" Then
                                        Dim Perc As Double = ((TotalAmt - (dr("Quantity") * drView("VALUE"))) * 100) / TotalAmt
                                        dr("LineDiscount") = TotalAmt - (dr("Quantity") * drView("VALUE"))
                                        dr("FIRSTLEVEL") = drView("OfferNo")
                                        dr("TOTALDISCPERCENTAGE") = CDbl(dr("TOTALDISCPERCENTAGE").ToString()) + Perc
                                    End If
                                ElseIf drView("OFFERTYPE") = "BNQPXMQD" Then
                                    dvYSch.RowFilter = "Offerno='" & drView("Offerno") & "' AND ArticleCode='" & drView("ArticleCode") & "'"

                                    If dvYSch.Count > 0 Then
                                        Dim dtBatch As DataTable = dvYSch.ToTable(True, "BatchNO")

                                        For Each drBatch As DataRow In dtBatch.Rows
                                            Dim dvBatch As New DataView(dvYSch.Table, "OfferNo='" & drView("Offerno") & "' AND BatchNo='" & drBatch(0).ToString() & "'", "", DataViewRowState.CurrentRows)

                                            If dvBatch.Count > 0 Then
                                                Dim SchemeQty, TotalBatchQty, nApplicableQty As Int32
                                                Dim Item As String = ""
                                                Dim DtArtBatch As DataTable = dvBatch.ToTable(True, "ArticleCode")
                                                TotalBatchQty = 0
                                                For Each drArt As DataRow In DtArtBatch.Rows
                                                    TotalBatchQty = TotalBatchQty + CDbl(dr.Table.Compute("Sum(QUANTITY)", "ArticleCode='" & drArt(0) & "'").ToString())
                                                    Item = Item & "'" & drArt(0) & "',"
                                                Next
                                                SchemeQty = drView("N_Qty") + drView("M_Qty")
                                                nApplicableQty = Int(TotalBatchQty / SchemeQty)
                                                If nApplicableQty <= 0 Then Exit For
                                                Item = Item.Substring(0, Item.Length - 1)
                                                Dim dvApply As New DataView(MainDS.Tables("ItemScanDetails"), "ArticleCode In (" & Item & ")", "SellingPrice", DataViewRowState.CurrentRows)
                                                If dvApply.Count > 0 Then
                                                    dvApply.AllowEdit = True
                                                    For Each drFreeArt As DataRowView In dvApply
                                                        Dim ApplicableQty As Int32 = drFreeArt("Quantity")
                                                        Dim TotalAmt As Double = ApplicableQty * drFreeArt("SellingPrice")
                                                        If ApplicableQty >= nApplicableQty Then
                                                            If drView("DISCOUNTTYPE") = "DD" Then
                                                                drFreeArt("LineDiscount") = ((nApplicableQty * drFreeArt("SellingPrice")) * drView("Value")) / 100
                                                                drFreeArt("TOTALDISCPERCENTAGE") = CDbl(drFreeArt("TOTALDISCPERCENTAGE").ToString()) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                            ElseIf drView("DISCOUNTTYPE") = "PO" Then
                                                                drFreeArt("LineDiscount") = nApplicableQty * drView("Value")
                                                                drFreeArt("TOTALDISCPERCENTAGE") = CDbl(drFreeArt("TOTALDISCPERCENTAGE").ToString()) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                            ElseIf drView("DISCOUNTTYPE") = "PS" Then
                                                                drFreeArt("LineDiscount") = TotalAmt - (nApplicableQty * drView("Value"))
                                                                drFreeArt("TOTALDISCPERCENTAGE") = CDbl(drFreeArt("TOTALDISCPERCENTAGE").ToString()) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                            End If
                                                            Exit For
                                                        Else
                                                            If drView("DISCOUNTTYPE") = "DD" Then
                                                                drFreeArt("LineDiscount") = ((ApplicableQty * drFreeArt("SellingPrice")) * drView("Value")) / 100
                                                                drFreeArt("TOTALDISCPERCENTAGE") = CDbl(drFreeArt("TOTALDISCPERCENTAGE").ToString()) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                            ElseIf drView("DISCOUNTTYPE") = "PO" Then
                                                                drFreeArt("LineDiscount") = ApplicableQty * drView("Value")
                                                                drFreeArt("TOTALDISCPERCENTAGE") = CDbl(drFreeArt("TOTALDISCPERCENTAGE").ToString()) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                            ElseIf drView("DISCOUNTTYPE") = "PS" Then
                                                                drFreeArt("LineDiscount") = TotalAmt - (ApplicableQty * drView("Value"))
                                                                drFreeArt("TOTALDISCPERCENTAGE") = CDbl(drFreeArt("TOTALDISCPERCENTAGE").ToString()) + ((drFreeArt("LineDiscount") * 100) / TotalAmt)
                                                                drFreeArt("FIRSTLEVEL") = drView("OfferNo")
                                                            End If
                                                            nApplicableQty = nApplicableQty - ApplicableQty
                                                        End If
                                                    Next
                                                End If
                                            End If
                                        Next
                                    End If
                                ElseIf drView("OFFERTYPE") = "BNQPXPYD" Then
                                    dvYSch.RowFilter = "OfferNo='" & drView("OFFERNO") & "' And ArticleCode<>'" & drView("ArticleCode") & "'"
                                    If dvYSch.Count > 0 Then
                                        For Each drY As DataRowView In dvYSch
                                            Dim dvYMain As New DataView(MainDS.Tables("ItemScanDetails"), "ArticleCode='" & drY("ArticleCode").ToString() & "'", "", DataViewRowState.CurrentRows)
                                            If dvYMain.Count > 0 Then
                                                dvYMain.AllowEdit = True
                                                If dr("Quantity") >= drView("N_Qty") And dvYMain.Item(0)("Quantity") >= drView("M_Qty") Then
                                                    Dim SchemeQty, FreeQty, nApplicableQty As Int32
                                                    Dim totalAmt As Double = dvYMain.Item(0)("Quantity") * dvYMain.Item(0)("SellingPrice")
                                                    SchemeQty = drView("N_Qty")
                                                    FreeQty = drView("M_Qty")
                                                    nApplicableQty = Int(dr("Quantity") / SchemeQty)
                                                    nApplicableQty = nApplicableQty * FreeQty
                                                    If dvYMain.Item(0)("Quantity") < nApplicableQty Then
                                                        nApplicableQty = dvYMain.Item(0)("Quantity")
                                                    End If
                                                    If drView("DISCOUNTTYPE") = "DD" Then
                                                        dvYMain.Item(0)("LineDiscount") = ((nApplicableQty * dvYMain.Item(0)("SellingPrice")) * drY("Value")) / 100
                                                        dvYMain.Item(0)("TOTALDISCPERCENTAGE") = CDbl(dvYMain.Item(0)("TOTALDISCPERCENTAGE").ToString()) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                        dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                    ElseIf drView("DISCOUNTTYPE") = "PO" Then
                                                        dvYMain.Item(0)("LineDiscount") = nApplicableQty * drY("Value")
                                                        dvYMain.Item(0)("TOTALDISCPERCENTAGE") = CDbl(dvYMain.Item(0)("TOTALDISCPERCENTAGE").ToString()) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                        dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                    ElseIf drView("DISCOUNTTYPE") = "PS" Then
                                                        dvYMain.Item(0)("LineDiscount") = totalAmt - (nApplicableQty * drY("Value"))
                                                        dvYMain.Item(0)("TOTALDISCPERCENTAGE") = CDbl(dvYMain.Item(0)("TOTALDISCPERCENTAGE").ToString()) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                        dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                    End If
                                                End If

                                            End If
                                        Next
                                    End If
                                ElseIf drView("OFFERTYPE") = "BRPXD" Then
                                    Dim totalAmt As Double = dr("Quantity") * dr("SellingPrice")
                                    If totalAmt > drView("R_AMT") Then
                                        If drView("DISCOUNTTYPE") = "DD" Then
                                            dr("LineDiscount") = ((dr("Quantity") * dr("SellingPrice")) * drView("Value")) / 100
                                            dr("TOTALDISCPERCENTAGE") = CDbl(dr("TOTALDISCPERCENTAGE").ToString()) + ((dr("LineDiscount") * 100) / totalAmt)
                                            dr("FIRSTLEVEL") = drView("OfferNo")
                                        ElseIf drView("DISCOUNTTYPE") = "PO" Then
                                            dr("LineDiscount") = drView("Value")
                                            dr("TOTALDISCPERCENTAGE") = CDbl(dr("TOTALDISCPERCENTAGE").ToString()) + ((dr("LineDiscount") * 100) / totalAmt)
                                            dr("FIRSTLEVEL") = drView("OfferNo")
                                        ElseIf drView("DISCOUNTTYPE") = "PS" Then
                                            dr("LineDiscount") = totalAmt - (dr("Quantity") * drView("Value"))
                                            dr("TOTALDISCPERCENTAGE") = CDbl(dr("TOTALDISCPERCENTAGE").ToString()) + ((dr("LineDiscount") * 100) / totalAmt)
                                            dr("FIRSTLEVEL") = drView("OfferNo")
                                        End If
                                    End If
                                ElseIf drView("OFFERTYPE") = "BRPXPYD" Then
                                    dvYSch.RowFilter = "OfferNo='" & drView("OFFERNO") & "' And ArticleCode<>'" & drView("ArticleCode") & "'"
                                    For Each drY As DataRowView In dvYSch
                                        Dim dvYMain As New DataView(MainDS.Tables("ItemScanDetails"), "ArticleCode='" & drY("ArticleCode").ToString() & "'", "", DataViewRowState.CurrentRows)
                                        If dvYMain.Count > 0 Then
                                            dvYMain.AllowEdit = True
                                            Dim totalAmt As Double = dr("Quantity") * dr("SellingPrice")
                                            If totalAmt >= drView("R_AMT") Then
                                                totalAmt = dvYMain.Item(0)("Quantity") * dvYMain.Item(0)("SellingPrice")
                                                If drView("DISCOUNTTYPE") = "DD" Then
                                                    dvYMain.Item(0)("LineDiscount") = ((dvYMain.Item(0)("Quantity") * dvYMain.Item(0)("SellingPrice")) * drY("Value")) / 100
                                                    dvYMain.Item(0)("TOTALDISCPERCENTAGE") = CDbl(dvYMain.Item(0)("TOTALDISCPERCENTAGE").ToString()) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                    dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                ElseIf drView("DISCOUNTTYPE") = "PO" Then
                                                    dvYMain.Item(0)("LineDiscount") = dvYMain.Item(0)("Quantity") * drY("Value")
                                                    dvYMain.Item(0)("TOTALDISCPERCENTAGE") = CDbl(dvYMain.Item(0)("TOTALDISCPERCENTAGE").ToString()) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                    dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                ElseIf drView("DISCOUNTTYPE") = "PS" Then
                                                    dvYMain.Item(0)("LineDiscount") = totalAmt - (dvYMain.Item(0)("Quantity") * drY("Value"))
                                                    dvYMain.Item(0)("TOTALDISCPERCENTAGE") = CDbl(dvYMain.Item(0)("TOTALDISCPERCENTAGE").ToString()) + ((dvYMain.Item(0)("LineDiscount") * 100) / totalAmt)
                                                    dvYMain.Item(0)("FIRSTLEVEL") = drView("OfferNo")
                                                End If
                                            End If
                                        End If
                                    Next
                                ElseIf drView("OFFERTYPE") = "PPS" Then
                                    If drView("ISBATCHAPP") = True Then
                                        dvPSch.RowFilter = "Offerno='" & drView("Offerno") & "' AND ArticleCode='" & drView("ArticleCode") & "'"
                                        If dvPSch.Count > 0 Then
                                            Dim dtBatch As DataTable = dvPSch.ToTable(True, "BatchNO")
                                            For Each drBatch As DataRow In dtBatch.Rows
                                                Dim StrBatchFilter As String = "OfferNo='" & drView("Offerno") & "' AND BatchNo='" & drBatch(0).ToString() & "'"
                                                Dim dvBatch As New DataView(dvPSch.Table, StrBatchFilter, "RangeTo DESC", DataViewRowState.CurrentRows)
                                                If dvBatch.Count > 0 Then
                                                    Dim SchemeQty, TotalBatchQty As Int32
                                                    Dim Item As String = ""
                                                    Dim DtArtBatch As DataTable = dvBatch.ToTable(True, "ArticleCode")
                                                    TotalBatchQty = 0
                                                    For Each drArt As DataRow In DtArtBatch.Rows
                                                        TotalBatchQty = TotalBatchQty + CDbl(dr.Table.Compute("Sum(QUANTITY)", "ArticleCode='" & drArt(0) & "'").ToString())
                                                        Item = Item & "'" & drArt(0) & "',"
                                                    Next
                                                    Item = Item.Substring(0, Item.Length - 1)
ReCheck:
                                                    dvBatch.RowFilter = StrBatchFilter
                                                    If dvBatch.Count > 0 Then
                                                        SchemeQty = dvBatch(0)("RangeTo")
                                                        If TotalBatchQty >= SchemeQty Then
                                                            Dim dvApplicable As New DataView(MainDS.Tables("ItemScanDetails"), "ISNULL(FIRSTLEVEL,'')='' AND  ArticleCode in (" & Item & ")", "", DataViewRowState.CurrentRows)
                                                            For Each drApp As DataRowView In dvApplicable
                                                                If dvBatch(0)("SLABDISCOUNTPERCENTAGE").ToString() <> String.Empty Then
                                                                    drApp("LineDiscount") = (drApp("NetAmount") * dvBatch(0)("SLABDISCOUNTPERCENTAGE")) / 100
                                                                    drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("GrossAmt")
                                                                    drApp("FIRSTLEVEL") = dvBatch(0)("OfferNo")
                                                                ElseIf dvBatch(0)("SLABDISCOUNTAMT").ToString() <> String.Empty Then
                                                                    drApp("LineDiscount") = dvBatch(0)("SLABDISCOUNTAMT") * drApp("Quantity")
                                                                    drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("GrossAmt")
                                                                    drApp("FIRSTLEVEL") = dvBatch(0)("OfferNo")
                                                                ElseIf dvBatch(0)("SLABDISCOUNTEDPRICE").ToString() <> String.Empty Then
                                                                    drApp("LineDiscount") = dvBatch(0)("SLABDISCOUNTEDPRICE") * drApp("Quantity")
                                                                    drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("GrossAmt")
                                                                    drApp("FIRSTLEVEL") = dvBatch(0)("OfferNo")
                                                                End If
                                                            Next
                                                        Else
                                                            StrBatchFilter = StrBatchFilter & " AND RangeTo < " & SchemeQty
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
                                            ActualQty = CDbl(MainDS.Tables("ItemScanDetails").Compute("Sum(OrderQty)", "ArticleCode='" & drView("ArticleCode") & "'").ToString())
Rechk:
                                            dvPSch.RowFilter = StrFilterPromo
                                            If dvPSch.Count > 0 Then
                                                SchemeQty = dvPSch(0)("RangeTo")
                                                If ActualQty >= SchemeQty Then
                                                    Dim dvApplicable As New DataView(MainDS.Tables("ItemScanDetails"), "ISNULL(FIRSTLEVEL,'')='' AND  ArticleCode='" & drView("ArticleCode") & "'", "", DataViewRowState.CurrentRows)
                                                    For Each drApp As DataRowView In dvApplicable
                                                        If dvPSch(0)("SLABDISCOUNTPERCENTAGE").ToString() <> String.Empty Then
                                                            drApp("LineDiscount") = (drApp("NetAmount") * dvPSch(0)("SLABDISCOUNTPERCENTAGE")) / 100
                                                            drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("GrossAmt")
                                                            drApp("FIRSTLEVEL") = dvPSch(0)("OfferNo")
                                                        ElseIf dvPSch(0)("SLABDISCOUNTAMT").ToString() <> String.Empty Then
                                                            drApp("LineDiscount") = dvPSch(0)("SLABDISCOUNTAMT") * drApp("Quantity")
                                                            drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("GrossAmt")
                                                            drApp("FIRSTLEVEL") = dvPSch(0)("OfferNo")
                                                        ElseIf dvPSch(0)("SLABDISCOUNTEDPRICE").ToString() <> String.Empty Then
                                                            drApp("LineDiscount") = dvPSch(0)("SLABDISCOUNTEDPRICE") * drApp("Quantity")
                                                            drApp("TOTALDISCPERCENTAGE") = drApp("TotalDiscount") * 100 / drApp("GrossAmt")
                                                            drApp("FIRSTLEVEL") = dvPSch(0)("OfferNo")
                                                        End If
                                                    Next
                                                Else
                                                    StrFilterPromo = StrFilterPromo & " AND RangeTo < " & SchemeQty
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
            Next
            'Offer aplicable only for that Item
            StrFilter = "OFFERLEVEL=1 AND OfferApplicableFor='Top Up Scheme'"
            DvFilterData.RowFilter = StrFilter
            If DvFilterData.Count > 0 Then
                For Each drFilterArt As DataRowView In DvFilterData
                    Try
                        Dim dvData As New DataView(MainDS.Tables("ItemScanDetails"), "ArticleCode='" & drFilterArt("ArticleCode") & "' AND ISNULL(TOPLEVEL,'')=''", "", DataViewRowState.CurrentRows)
                        If dvData.Count > 0 Then
                            Dim TotalAmt As Double = CDbl(MainDS.Tables("ItemScanDetails").Compute("SUM(NetAmount)", "ArticleCode='" & drFilterArt("ArticleCode") & "' AND ISNULL(TOPLEVEL,'')=''").ToString())
                            If TotalAmt > CDbl(drFilterArt("R_AMT").ToString()) Then

                                Dim TotalFreePer, ApplyAmt As Double
                                If drFilterArt("Q_DisAmt").ToString() <> String.Empty Then
                                    ApplyAmt = drFilterArt("Q_DisAmt")
                                    TotalFreePer = (ApplyAmt * 100) / TotalAmt
                                ElseIf drFilterArt("P_DisPer").ToString() <> String.Empty Then
                                    TotalFreePer = drFilterArt("P_DisPer")
                                End If
                                For Each dr As DataRowView In dvData
                                    ApplyAmt = (dr("NetAmount") * TotalFreePer) / 100
                                    dr("LineDiscount") = CDbl(dr("LineDiscount").ToString()) + ApplyAmt
                                    dr("TOTALDISCPERCENTAGE") = CDbl(dr("TOTALDISCPERCENTAGE").ToString()) + TotalFreePer
                                    dr("TOPLEVEL") = drFilterArt("OfferNo")
                                Next
                                'End If
                            End If
                        End If

                    Catch ex As Exception
                    End Try
                Next
            End If
            'Offer Applicable on Remaning Items on which Item level Not applied
            StrFilter = "OFFERLEVEL=2 AND OfferApplicableFor='First Level Scheme'"
            DvFilterData.RowFilter = StrFilter
            DvFilterData.Sort = "R_Amt Desc"
            If DvFilterData.Count > 0 Then
                For Each drFilterScheme As DataRowView In DvFilterData
                    Dim TotalAmt As Double = CDbl(MainDS.Tables("ItemScanDetails").Compute("SUM(NetAmount)", "ISNULL(FIRSTLEVEL,'')='' AND ISNULL(TOPLEVEL,'')=''").ToString())

                    If TotalAmt > drFilterScheme("R_Amt") Then
                        Try
                            Dim dvApplicableData As New DataView(MainDS.Tables("ItemScanDetails"), "ISNULL(FIRSTLEVEL,'')=''", "ArticleCode", DataViewRowState.CurrentRows)
                            If dvApplicableData.Count > 0 Then
                                Dim TotalFreePer, ApplyAmt As Double
                                If drFilterScheme("Q_DisAmt").ToString() <> String.Empty Then
                                    ApplyAmt = drFilterScheme("Q_DisAmt")
                                    TotalFreePer = (ApplyAmt * 100) / TotalAmt
                                ElseIf drFilterScheme("P_DisPer").ToString() <> String.Empty Then
                                    TotalFreePer = drFilterScheme("P_DisPer")
                                End If
                                For Each dr As DataRowView In dvApplicableData
                                    ApplyAmt = (dr("NetAmount") * TotalFreePer) / 100
                                    dr("LineDiscount") = CDbl(dr("LineDiscount").ToString()) + ApplyAmt
                                    dr("TOTALDISCPERCENTAGE") = CDbl(dr("TOTALDISCPERCENTAGE").ToString()) + TotalFreePer
                                    dr("FIRSTLEVEL") = drFilterScheme("OfferNo")
                                Next

                            End If
                        Catch ex As Exception
                        End Try

                    End If
                Next
            End If
            'Offer applicable only Item which have no topup scheme applied earlier
            StrFilter = "OFFERLEVEL=2 AND OfferApplicableFor='Top Up Scheme'"
            DvFilterData.RowFilter = StrFilter
            DvFilterData.Sort = "R_Amt Desc"

            If DvFilterData.Count > 0 Then
                For Each drFilterScheme As DataRowView In DvFilterData

                    Dim TotalAmt As Double = CDbl(MainDS.Tables("ItemScanDetails").Compute("SUM(NetAmount)", "ISNULL(TOPLEVEL,'')=''").ToString())
                    Dim vR_Amt As Double = IIf(drFilterScheme("R_Amt") Is DBNull.Value, 0.0, drFilterScheme("R_Amt"))

                    If TotalAmt > vR_Amt Then
                        Try
                            Dim dvApplicableData As New DataView(MainDS.Tables("ItemScanDetails"), "ISNULL(TOPLEVEL,"")=""", "ArticleCode", DataViewRowState.CurrentRows)
                            If dvApplicableData.Count > 0 Then
                                Dim TotalFreePer, ApplyAmt As Double
                                If drFilterScheme("Q_DisAmt").ToString() <> String.Empty Then
                                    ApplyAmt = drFilterScheme("Q_DisAmt")
                                    TotalFreePer = (ApplyAmt * 100) / TotalAmt
                                ElseIf drFilterScheme("P_DisPer").ToString() <> String.Empty Then
                                    TotalFreePer = drFilterScheme("P_DisPer")
                                End If
                                For Each dr As DataRowView In dvApplicableData
                                    ApplyAmt = (dr("NetAmount") * TotalFreePer) / 100
                                    dr("LineDiscount") = CDbl(dr("LineDiscount").ToString()) + ApplyAmt
                                    dr("TOTALDISCPERCENTAGE") = CDbl(dr("TOTALDISCPERCENTAGE").ToString()) + TotalFreePer
                                    dr("TOPLEVEL") = drFilterScheme("OfferNo")
                                    If dr("FIRSTLEVEL") = "" Then
                                        dr("FIRSTLEVEL") = drFilterScheme("OfferNo")
                                    End If
                                Next
                            End If
                        Catch ex As Exception
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function GetListofActivePromotions(ByVal SiteCode As String) As DataTable
        Try
            Dim dt As DataTable
            getActivePromo(SiteCode, "")
            Dim dv As New DataView(MainPromoDS.Tables("Promotions"), "", "OfferNo", DataViewRowState.CurrentRows)
            dt = dv.ToTable(True, "Offerno", "OfferName")
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Sub ApplySelectedPromotion(ByVal promotionNo As String, ByRef dsMain As DataSet, ByVal SiteCode As String)
        Try
            MainDS = dsMain

            If Not MainPromoDS Is Nothing Then
                Dim ds As DataSet = MainPromoDS.Copy()
                'MainPromoDS.Clear()
                MainPromoDS = New DataSet
                For Each dt As DataTable In ds.Tables
                    Dim dv As New DataView(dt, "OfferNo=" & promotionNo, "", DataViewRowState.CurrentRows)
                    MainPromoDS.Tables.Add(dv.ToTable())
                Next
            Else
                getActivePromo(SiteCode, promotionNo)
            End If
            StartApplyPromotion()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub CheckForPromotionList(ByVal strSiteCode As String, ByVal NetBillAmt As Double, ByRef PromotionSlipText As String, ByRef PromoId As String)
        Try
            getActivePromo(strSiteCode, "")
            PromotionSlipText = GetPromotionSlip(NetBillAmt, PromoId)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function GetPromotionSlip(ByVal NetBillAmt As Double, ByRef PromotionID As String) As String
        Try
            Dim PromoSlipText As String = ""
            Dim dvPSlip As New DataView(MainPromoDS.Tables("Promotions"), "OfferType='PPS' AND OFFERLEVEL=2 AND OfferApplicableFor='Top Up Scheme'   AND  " & NetBillAmt & " Between RANGEFROM AND RANGETO", "RANGETO", DataViewRowState.CurrentRows)
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
End Class

