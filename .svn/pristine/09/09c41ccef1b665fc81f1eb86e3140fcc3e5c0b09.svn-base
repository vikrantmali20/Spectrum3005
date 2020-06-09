Imports System.Data.SqlClient
Imports System.Resources
Imports System.Data

'''<summary>
''' Added by Sameer for CLP Italy (CR-5875)
'''</summary>
Public Class CLP_Logic
    Public Function CLPRedCalc(ByVal customerID As String, ByRef ErrMsg As String, Optional ByRef ReedType As String = "Rdt2", Optional ByVal MstReceipttype As DataSet = Nothing, Optional ByVal dtitems As DataTable = Nothing, Optional ByVal Roundat As Int32 = 50, Optional ByRef IsPopOpen As Boolean = False, Optional ByRef CLPPoints As Double = 0) As DataView
        Try
            Dim tempds As New DataSet
            Dim custdata As New DataTable
            'Dim clscomm As New clsCommon
            Dim dv As New DataView
            Dim slabpoint As Decimal = 0.0
            getclpsettings()
            tempds = CLPConfigdata.Copy()

            custdata = getCLPcustomerdata(customerID, tempds.Tables("CLPHeader").Rows(0)("CLPPROGRAMID"))
            tempds.Tables.Add(custdata)
            'Checks whether customer is scanned
            If tempds.Tables("CLPCustomer").Rows.Count > 0 Then
                'Check's any Program Assigned
                If tempds.Tables("CLPHeader").Rows.Count > 0 Then
                    'Check's Customer does have any points
                    If tempds.Tables("CLPCustomer").Rows(0)("TotalBalancePoint") > 0 Then
                        'Check's Redeemption type is slab based or Point based
                        If tempds.Tables("CLPHeader").Rows(0)("RedemptionType") = "Rdt3" Then
                            'Check's MaxLimit on redeemption applicable or not
                            If tempds.Tables("CLPHeader").Rows(0)("MAXLIMITONRED") Then
                                'Check's Whether customer applicable for any slabe or not
                                If CDec(tempds.Tables("CLPCustomer").Rows(0)("TotalBalancePoint")) >= CDec(tempds.Tables("CLPRedDetails").Rows(tempds.Tables("CLPRedDetails").Rows.Count - 1)("slabpoints")) Then
                                    'For index = tempds.Tables("CLPRedDetails").Rows.Count To 0 Step -1
                                    '    dv = New DataView(tempds.Tables("CLPRedDetails"), "srno='" + index.ToString() + "'", "", DataViewRowState.CurrentRows)
                                    '    If dv.Count > 0 Then
                                    '        If CDec(dv(0)("slabpoints")) <= CDec(tempds.Tables("CLPCustomer")(0)("TotalBalancePoint")) Then
                                    '            CLP_Data._SlabPoints = CDec(dv(0)("slabpoints"))
                                    '            Exit For
                                    '            ' GoTo slabpoint
                                    '        End If
                                    '    End If
                                    'Next
                                    Dim int As Integer = 0

                                    For Each dr As DataRow In tempds.Tables("CLPRedDetails").Rows
                                        int += 1
                                        If CDec(dr("slabpoints")) <= CDec(tempds.Tables("CLPCustomer")(0)("TotalBalancePoint")) Then

                                            CLP_Data._SlabPoints = CDec(dr("slabpoints"))
                                            dv = New DataView(tempds.Tables("CLPRedDetails"), "slabpoints='" + dr("slabpoints").ToString() + "'", "", DataViewRowState.CurrentRows)
                                            Exit For
                                            ' GoTo slabpoint
                                        End If
                                    Next
                                    'slabpoints:
                                    'If slabpoint = 0.0 Then
                                    '    Return Nothing
                                    'Else

                                    Return dv
                                    'End If
                                Else
                                    ErrMsg = "LOY004"
                                    ''Error for unable to satisfy any slab
                                    Return Nothing
                                End If
                            Else
                                ' if flag maxredeemption is false
                                ReedType = "Rdt3"
                                IsPopOpen = True

                                Return Nothing
                            End If
                        ElseIf tempds.Tables("CLPHeader").Rows(0)("RedemptionType") = "Rdt2" Then

                            'Calculation for point based 
                            ReedType = "Rdt2"
                            If tempds.Tables("CLPRedDetails").Rows.Count > 0 Then
                                dv = New DataView(tempds.Tables("CLPRedDetails"), "", "", DataViewRowState.CurrentRows)
                                Return dv
                            End If

                            'If Not MstReceipttype Is Nothing Then
                            '    Dim dt As New DataTable
                            '    Dim dr As DataRow
                            '    dr = dt.NewRow()
                            '    dt.Columns.Add("slabpoints", System.Type.GetType("System.String"))
                            '    dt.Columns.Add("redemptionamount", System.Type.GetType("System.String"))
                            '    dr("slabpoints") = MstReceipttype.Tables("MSTRecieptType")(0)("Amount")
                            '    dr("redemptionamount") = MstReceipttype.Tables("MSTRecieptType")(0)("Amount")
                            '    dt.Rows.Add(dr)
                            '    dv = New DataView(dt, "", "", DataViewRowState.CurrentRows)
                            '    Return dv
                            'End If
                        ElseIf tempds.Tables("CLPHeader").Rows(0)("RedemptionType") = "Rdt1" Then

                            Dim totalpoints, totalamount As Decimal
                            Dim Items = ""
                            If (tempds.Tables("ClpRedemptionItemDetail") IsNot Nothing AndAlso tempds.Tables("ClpRedemptionItemDetail").Rows.Count > 0) OrElse (tempds.Tables("CLPRedDetails") IsNot Nothing AndAlso tempds.Tables("CLPRedDetails").Rows.Count > 0) Then

                                If dtitems IsNot Nothing AndAlso dtitems.Rows.Count > 0 Then

                                    For Each dritm As DataRow In dtitems.Rows
                                        Dim currAmt, CurrPoints As Double
                                        If tempds.Tables("ClpRedemptionItemDetail").Select("Articlecode ='" & dritm("Articlecode") & "'").Count() > 0 Then
                                            currAmt = (tempds.Tables("ClpRedemptionItemDetail").Select("Articlecode ='" & dritm("Articlecode") & "'").FirstOrDefault()("AmtValue"))
                                            CurrPoints = tempds.Tables("ClpRedemptionItemDetail").Select("Articlecode ='" & dritm("Articlecode") & "'").FirstOrDefault()("Points")

                                            If CurrPoints <> 0 Then
                                                currAmt = currAmt * dritm("Quantity")
                                                CurrPoints = CurrPoints * dritm("Quantity")
                                                totalamount += (dritm("NETAMOUNT") - currAmt)
                                                totalpoints += CurrPoints
                                                If Items = "" Then
                                                    Items = dritm("Articlecode")
                                                Else
                                                    Items = Items & "," & dritm("Articlecode")
                                                End If

                                            End If
                                        End If
                                    Next

                                    Dim TotalRemainderItemNet = If(dtitems.Compute("Sum(NetAmount)", "ArticleCode NOT IN ('" & Items & "')") Is DBNull.Value, 0, dtitems.Compute("Sum(NetAmount)", "ArticleCode NOT IN ('" & Items & "')"))
                                    If CLPPoints - totalpoints >= 0 Then
                                        Dim TotalNet, CVAmount As Decimal

                                        TotalNet = CLPPoints - totalpoints
                                        CVAmount = (TotalNet / tempds.Tables("CLPRedDetails")(0)("Points")) * tempds.Tables("CLPRedDetails")(0)("AmtValue")
                                        totalpoints += TotalNet

                                        If TotalRemainderItemNet >= CVAmount Then
                                            totalamount += CVAmount
                                            CVAmount = 0
                                        Else
                                            totalamount += CVAmount
                                            CVAmount = (CVAmount - TotalRemainderItemNet)
                                        End If


                                        Dim dtreturn As New DataTable()
                                        dtreturn.Columns.Add("Points")
                                        dtreturn.Columns.Add("Amount")
                                        dtreturn.Columns.Add("CVAmount")

                                        Dim drnew = dtreturn.NewRow()
                                        drnew("Points") = totalpoints
                                        drnew("Amount") = FormatNumber(MyRound(totalamount, Roundat), 2).ToString
                                        drnew("CVAmount") = CVAmount * -1

                                        dtreturn.Rows.Add(drnew)
                                        CLP_Data._SlabPoints = totalpoints
                                        Return New DataView(dtreturn)
                                    Else
                                        CLPPoints = totalpoints
                                        ErrMsg = "LOY020"
                                    End If

                                Else
                                    ErrMsg = "LOY011"
                                End If

                            Else
                                ErrMsg = "LOY010"
                            End If

                        End If
                    Else
                        'Rakesh 120912: Message changed as per Sameer K. suggestion
                        ErrMsg = "LOY004" 'No Balance with Customer
                    End If

                    ''''' 
                Else

                End If


            Else
                ErrMsg = "LOY011"  'No Loyalty program assigned!!
            End If

        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    Public Function NON_CLP_Tender_Calc(ByRef dtitem As DataTable, ByRef dtpayment As DataTable, ByVal Amtvalue As String, ByVal Points As String, ByVal bConvertinMultiples As Boolean, Optional ItemFilter As String = "")

        Dim Non_CLPAMT As Decimal = 0.0
        Dim CLPAMT As Decimal = 0.0
        Dim Non_CLPITM As Decimal = 0.0
        Dim CLPITM As Decimal = 0.0
        Dim Total_AMT As Decimal = 0.0




        Dim dv As New DataView(dtpayment, "NOCLP='TRUE'", "", DataViewRowState.CurrentRows)
        If dv.Count > 0 Then
            Non_CLPAMT = CDec(dv.ToTable().Compute("Sum(Amount)", ""))
            Total_AMT = CDec(dtpayment.Compute("Sum(Amount)", ""))
            CLPAMT = Total_AMT - Non_CLPAMT
            CLPITM = CDec(IIf(dtitem.Compute("Sum(NetAmount)", ItemFilter) Is DBNull.Value, 0, dtitem.Compute("Sum(NetAmount)", ItemFilter)))
            Non_CLPITM = CDec(IIf(dtitem.Compute("Sum(NetAmount)", "") Is DBNull.Value, 0, dtitem.Compute("Sum(NetAmount)", ""))) - CLPITM
            If CLPAMT > 0 AndAlso CLPITM > 0 Then
                'Dim clpAmount As Object = dtitem.Compute("Sum(NetAmount)", "CLPRequire=False AND BTYPE='S' or ArticleCode ='GVBaseArticle' or ArticleCode ='CLPBaseArticle' ")

                Dim result As Decimal = Non_CLPAMT - Non_CLPITM


                If result < 0.0 Then
                    CLPAMT += result


                    If CLPAMT > CLPITM Then
                        CLPDistribution(dtitem, CalculatePointsOnBill(CLPITM, Amtvalue, Points), bConvertinMultiples, ItemFilter)
                    Else
                        CLPDistribution(dtitem, CalculatePointsOnBill(CLPAMT, Amtvalue, Points), bConvertinMultiples, ItemFilter)
                    End If
                Else
                    If CLPAMT > CLPITM Then
                        CLPDistribution(dtitem, CalculatePointsOnBill(CLPITM, Amtvalue, Points), bConvertinMultiples, ItemFilter)
                        'Else
                        '    CLPDistribution(dtitem, CalculatePointsOnBill(CLPAMT, Amtvalue, Points), bConvertinMultiples, ItemFilter)
                    End If

                End If





                'If ItemFilter.Contains("CLPRequire") Then

                '    Non_CLPAMT = Non_CLPAMT - Convert.ToDecimal(IIf(dtitem.Compute("Sum(NetAmount)", "CLPRequire=False AND BTYPE='S' or ArticleCode ='GVBaseArticle' or ArticleCode ='CLPBaseArticle' ") Is DBNull.Value, 0, dtitem.Compute("Sum(NetAmount)", "CLPRequire=False AND BTYPE='S' or ArticleCode ='GVBaseArticle' or ArticleCode ='CLPBaseArticle' ")))
                'End If

                'If Not Non_CLPAMT <= 0 Then

                '    CLPDistribution(dtitem, CalculatePointsOnBill(CLPAMT, Amtvalue, Points), bConvertinMultiples)
                'Else
                '    CLPDistribution(dtitem, CalculatePointsOnBill(CLPAMT - Non_CLPAMT, Amtvalue, Points), bConvertinMultiples)
                'End If

            Else
                For Each dr In dtitem.Rows
                    dr("CLPPoints") = 0.0
                Next
            End If

        End If

    End Function
    Public Function CalculatePointsOnBill(ByVal NetBillAmt As Decimal, ByVal ConvAmt As Decimal, ByVal points As Decimal) As Decimal

        If Not NetBillAmt <= 0 AndAlso Not ConvAmt <= 0 Then
            Return (NetBillAmt / ConvAmt) * points
        Else
            Return 0.0
        End If


    End Function
    Public Function CLPDistribution(ByVal dtitem As DataTable, ByVal CLPPoints As Decimal, ByVal bConvertinMultiples As Boolean, Optional ItemFilter As String = "")
        Dim filter As String
        If ItemFilter <> "" Then
            filter = ItemFilter
        Else
            filter = "CLPRequire=TRUE And ArticleCode <>'GVBaseArticle' AND ArticleCode <>'CLPBaseArticle'"
        End If


        If Not dtitem.Columns("BTYPE") Is Nothing Then
            filter += " AND BTYPE='S'"


        End If


        Dim dv As New DataView(dtitem, filter, "", DataViewRowState.CurrentRows)

        If dv.Count > 0 Then

            For Each dr As DataRow In dtitem.Select(filter)
                If bConvertinMultiples Then

                    If Not dr("NetAmount") Is DBNull.Value Then
                        dr("CLPPoints") = Math.Floor(((CDec(dr("NetAmount")) / CDec(dv.ToTable().Compute("Sum(NetAmount)", ""))) * CLPPoints))
                    End If
                Else
                    If Not dr("NetAmount") Is DBNull.Value Then
                        dr("CLPPoints") = ((CDec(dr("NetAmount")) / CDec(dv.ToTable().Compute("Sum(NetAmount)", ""))) * CLPPoints).ToString()
                    End If

                End If
            Next
            'For Each dr As DataRow In dv.ToTable().Rows

            '    'checks for flag converts in multilples
            '    If bConvertinMultiples Then
            '        dr("CLPPoints") = Math.Round(((CDec(dr("NetAmount")) / CDec(dv.ToTable().Compute("Sum(NetAmount)", ""))) * CLPPoints))
            '    Else

            '        dr("CLPPoints") = ((CDec(dr("NetAmount")) / CDec(dv.ToTable().Compute("Sum(NetAmount)", ""))) * CLPPoints).ToString()
            '    End If


            'Next
        End If

    End Function

    Public Function getrelativesCLP(ByVal relpointsdata As DataSet, ByVal sqltra As SqlTransaction)

    End Function

    Public Function getclphierarchy(ByVal articlecode As String, ByVal Sitecode As String) As Boolean

        If Not getclphier(articlecode, Sitecode) Is Nothing Then
            Return True
        Else
            Return False
        End If

    End Function

End Class
