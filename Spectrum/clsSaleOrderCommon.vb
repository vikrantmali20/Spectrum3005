Imports SpectrumBL
Public Class clsSaleOrderCommon
    Dim ObjComm As New clsCommon
    Public IsCSTApplicable As Boolean
    Dim _strCustNo As String
    Private _CustomerNo As String
    Public Property CustomerNo As String
        Get
            Return _CustomerNo
        End Get
        Set(ByVal value As String)
            _CustomerNo = value
        End Set
    End Property

    Public Function RecalculateLine(ByRef dr As DataRow, ByVal SoNumber As String, ByRef dsMain As DataSet, Optional ByVal RemovePromo As Boolean = True, Optional ByVal CalcFromDb As Boolean = True, Optional preQty As Integer = 0)

        Try
            'dtMainTax = dsMain.Tables("SalesOrderTaxDtls").Copy()
            dr("Discount") = 0
            dr("PromotionId") = 0
            dr("LineDiscount") = 0
            dr("TotalDiscPercentage") = 0
            dr("FirstLevel") = String.Empty
            dr("TopLevel") = String.Empty

            If RemovePromo = True Then
                dr("GrossAmt") = dr("Quantity") * dr("SellingPrice")
                'If CalcFromDb Then
                CreateDataSetForTaxCalculation(dr("ArticleCode").ToString(), dr("GrossAmt"), dr, dsMain, SoNumber, dr("EAN").ToString(), True)
                'Else
                ' dr("IncTaxAmt") = (dr("IncTaxAmt") / (preQty)) * dr("Quantity")
                ' dr("TotalTaxAmt") = FormatNumber(dr("ExclTaxAmt") + dr("IncTaxAmt"), 2)
                If preQty = 0 Then
                    preQty = 1
                End If
                'dr("TotalTaxAmt") = FormatNumber((If(dr("TotalTaxAmt") Is DBNull.Value, 0, dr("TotalTaxAmt")) / (preQty)) * dr("Quantity"), 2)
                'End If
                Dim discountAmount As Decimal = If(dr("Discount") Is DBNull.Value, 0, dr("Discount"))
                'Dim taxAmount As Decimal = If(dr("TotalTaxAmt") Is DBNull.Value, 0, dr("TotalTaxAmt"))

                dr("NetAmount") = FormatNumber((dr("GrossAmt") + IIf(dr("ExclTaxAmt") Is DBNull.Value, 0, dr("ExclTaxAmt"))) - discountAmount, 2)
            Else
                'CreateDataSetForTaxCalculation(dr("ArticleCode").ToString(), dr("GrossAmt"), dr, dsMain, SoNumber, dr("EAN").ToString(), True)
                'dr("GrossAmt") = dr("Quantity") * dr("SellingPrice")
                'dr("NetAmount") = FormatNumber((dr("GrossAmt") + dr("ExclTaxAmt") + dr("IncTaxAmt")) - dr("Discount"), 2)
                '------ Changed By Mahesh 
                dr("GrossAmt") = dr("Quantity") * dr("SellingPrice")
                CreateDataSetForTaxCalculation(dr("ArticleCode").ToString(), dr("GrossAmt"), dr, dsMain, SoNumber, dr("EAN").ToString(), True)
                dr("NetAmount") = FormatNumber((dr("GrossAmt") + dr("ExclTaxAmt")) - dr("Discount"), 2)
            End If
            'If Not dr("PickUpQty") Is DBNull.Value AndAlso dr("PickUpQty") >= 0 Then
            '    dr("Stock") = dr("Stock") - dr("PickUpQty")
            'End If
            'dsMain.Tables("SalesOrderTaxDtls").Clear()
            'dsMain.Tables("SalesOrderTaxDtls").Merge(dtMainTax, False, MissingSchemaAction.Ignore)
        Catch ex As Exception
        End Try
    End Function
    Public Function CreateDataSetForTaxCalculation(ByVal strMatcode As String, ByVal TaxableAmount As Double, ByRef dr As DataRow, ByRef dsMain As DataSet, ByVal SONumber As String, Optional ByVal EAN As String = "", Optional ByVal isInclusiveCalc As Boolean = False) As Object
        Try
            Dim dtMainTax As DataTable
            Dim StrTaxCode As Double = 0
            Dim dtTaxCalc As DataTable
            dtTaxCalc = New DataTable
            Dim ExlusiveFound As Boolean = False
            ObjComm.DecimalDigits = clsDefaultConfiguration.DecimalPlaces
            'Dim dvTax As New DataView(dtMainTax, "EAN='" & EAN & "'", "", DataViewRowState.CurrentRows)
            'If dvTax.Count > 0 Then
            '    dtTaxCalc = dvTax.ToTable()
            '    dvTax.AllowDelete = True
            '    Dim i As Integer
            '    For i = dvTax.Count - 1 To 0 Step -1
            '        dvTax.Delete(i)
            '    Next
            'Else
            If IsCSTApplicable Then
                dtTaxCalc = ObjComm.getTax(clsAdmin.SiteCode, strMatcode, "SO201", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, True)
            Else
                dtTaxCalc = ObjComm.getTax(clsAdmin.SiteCode, strMatcode, "SO201", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, False)
            End If
            ' dtTaxCalc = ObjComm.getTax(clsAdmin.SiteCode, strMatcode, "SO201", dr("Quantity"), EAN)
            'End If

            'Code added by irfan on 23-10-2017 for salesorder CAKEKRAFT
            Dim objComn As New clsCommon
            If dsMain.Tables("SalesOrderTaxDtls").Columns.Contains("CustomerNo") = True Then
                '  If IsDBNull(dsMain.Tables("SalesOrderTaxDtls").Rows(0)("CustomerNo")) Then
                _strCustNo = dsMain.Tables("SalesOrderTaxDtls").Rows(0)("CustomerNo")
                'Else
                'End If
            Else
                ' _strCustNo = dsMain.Tables("SalesOrderHDR").Rows(0)("CustomerNo")
                If Not dsMain.Tables("SalesOrderHDR") Is Nothing AndAlso dsMain.Tables("SalesOrderHDR").Rows.Count > 0 Then                                   'code added by irfan on 21/12/2017 for error in updating tax in so
                    _strCustNo = dsMain.Tables("SalesOrderHDR").Rows(0)("CustomerNo")
                Else
                    _strCustNo = CustomerNo
                End If
            End If

            ''Dim state As DataTable = objCM.getSiteStateCode(vSiteCode)
            Dim IsIGSTApplicableForOutsideCustomer = objComn.checkIGSTAplicableForOutSideStateCustomer(clsAdmin.SiteCode, _strCustNo)
            Dim IGSTtaxCode As String = objComn.ReturnIGSTTaxID(dtTaxCalc)

            If Not dtTaxCalc Is Nothing AndAlso dtTaxCalc.Rows.Count > 0 Then
                If IsIGSTApplicableForOutsideCustomer = True Then
                    If IGSTtaxCode <> "" Then
                        'code by irfan on 12/13/2017
                        'Dim index As Integer
                        'For index = 0 To dtTaxCalc.Rows.Count - 1
                        '    index = 0
                        '    If dtTaxCalc.Rows(0)("TAXCODE").ToString <> IGSTtaxCode Then
                        '        dtTaxCalc.Rows.RemoveAt(index)
                        '        dtTaxCalc.AcceptChanges()
                        '    Else
                        '        Exit For
                        '    End If
                        'Next
                        Dim dv As New DataView(dtTaxCalc, "TAXCODE='" & IGSTtaxCode & "'", "", DataViewRowState.CurrentRows)
                        dtTaxCalc = dv.ToTable
                        'commented by irfan 
                        'Else
                        '    Dim index As Integer
                        '    For index = 0 To dtTaxCalc.Rows.Count - 1
                        '        If dtTaxCalc.Rows.Count > 0 Then
                        '            index = 0
                        '            dtTaxCalc.Rows.RemoveAt(index)
                        '            dtTaxCalc.AcceptChanges()
                        '        Else
                        '            Exit For
                        '        End If
                        '    Next
                    End If
                Else
                    If dtTaxCalc.Rows.Count > 0 Then
                        Dim index As Integer
                        For index = 0 To dtTaxCalc.Rows.Count - 1
                            If dtTaxCalc.Rows(index)("TAXCODE").ToString.Trim = IGSTtaxCode Then
                                dtTaxCalc.Rows.RemoveAt(index)
                                dtTaxCalc.AcceptChanges()
                                Exit For
                            End If
                        Next
                    End If
                End If
            End If


            '=================================================================================================================================================
            Dim dbIncTotalTax As Double = 0
            Dim dbExclTotalTax As Double = 0
            If dtTaxCalc.Rows.Count = 0 Then
                Return Nothing
            End If
            If dtTaxCalc.Rows.Count <> 0 Then
                'dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
                'ObjComm.getCalculatedDataSet(dtTaxCalc, dr("Quantity"))
                If IsCSTApplicable And isInclusiveCalc = False Then
                    Dim amt As Double = GetTaxableAmountForCst(strMatcode, EAN, dr("Quantity"), TaxableAmount)
                    'originalTaxAmt = amt
                    dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount - amt
                    ObjComm.getCalculatedDataSet(dtTaxCalc)
                Else
                    dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
                    ObjComm.getCalculatedDataSet(dtTaxCalc)
                    'originalTaxAmt = dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")
                End If
                Dim iRowTax As Int16
                With dtTaxCalc
                    For iRowTax = 0 To dtTaxCalc.Rows.Count - 1
                        If CDbl(dtTaxCalc.Rows(iRowTax)("VALUE").ToString) <> 0 Then
                            If dtTaxCalc.Rows(iRowTax)("INCLUSIVE") = "0" Then
                                dbExclTotalTax = dbExclTotalTax + CDbl(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT"))
                            Else
                                dbIncTotalTax = dbIncTotalTax + CDbl(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT"))
                            End If
                        End If
                    Next
                End With
                dr("ExclTaxAmt") = FormatNumber(dbExclTotalTax, 2)
                dr("IncTaxAmt") = FormatNumber(dbIncTotalTax, 2)
                dr("TotalTaxAmt") = FormatNumber(dbExclTotalTax + dbIncTotalTax, 2)
                StrTaxCode = dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")
                'dr("TOTALTAXAMOUNT") = StrTaxCode
                'dtMainTax.Merge(dtTaxCalc, False, MissingSchemaAction.Ignore)
                'dtMainTax.AcceptChanges()
                Dim taxLineNo As Int32 = 0
                Dim findkeyTax(4) As Object
                For Each drRowTax In dtTaxCalc.Rows
                    taxLineNo += 1
                    findkeyTax(0) = clsAdmin.SiteCode
                    findkeyTax(1) = clsAdmin.Financialyear
                    findkeyTax(2) = SONumber
                    findkeyTax(3) = dr("EAN")
                    findkeyTax(4) = taxLineNo
                    Dim drTax As DataRow
                    drTax = dsMain.Tables("SalesOrderTaxDtls").Rows.Find(findkeyTax)
                    If drTax Is Nothing Then
                        drTax = dsMain.Tables("SalesOrderTaxDtls").NewRow

                        drTax("SiteCode") = clsAdmin.SiteCode
                        drTax("FinYear") = clsAdmin.Financialyear
                        drTax("SaleOrderNumber") = SONumber
                        drTax("EAN") = dr("EAN")
                        drTax("TaxLineNo") = taxLineNo
                        drTax("TaxLabel") = drRowTax("TaxCode")
                        drTax("TaxValue") = drRowTax("TaxAmount")

                        dsMain.Tables("SalesOrderTaxDtls").Rows.Add(drTax)
                    Else
                        drTax("TaxLineNo") = taxLineNo
                        drTax("TaxLabel") = drRowTax("TaxCode")
                        drTax("TaxValue") = Math.Round(drRowTax("TaxAmount"), 2)
                    End If
                    'drTax("SiteCode") = clsAdmin.SiteCode
                    'drTax("SaleOrderNumber") = SONumber
                    'drTax("EAN") = dr("EAN").ToString()

                Next

                Return StrTaxCode
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM012"), "CM012 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in updating Tax Calculation", "Error")
            Return Nothing
        End Try
    End Function
    Public Function RecalculateLineP(ByRef dr As DataRow, ByVal SoNumber As String, ByRef dsMain As DataSet, Optional ByVal RemovePromo As Boolean = True, Optional ByVal CalcFromDb As Boolean = True, Optional preQty As Integer = 0, Optional packIndex As Integer = 0, Optional ByVal hdrQty As Integer = 0, Optional ByVal excAmt As Integer = 0, Optional ByVal TotalPickUpDisc As DataTable = Nothing)

        Try
            'dtMainTax = dsMain.Tables("SalesOrderTaxDtls").Copy()
            dr("Discount") = 0
            dr("PromotionId") = 0
            dr("LineDiscount") = 0
            dr("TotalDiscPercentage") = 0
            dr("FirstLevel") = String.Empty
            dr("TopLevel") = String.Empty

            If RemovePromo = True Then
                dr("GrossAmt") = dr("Quantity") * dr("SellingPrice")
                Dim taxGrossAmt As Decimal = 0
                taxGrossAmt = (dr("Quantity") + hdrQty) * dr("SellingPrice")
                'If CalcFromDb Then
                CreateSODataSetForTaxCalculation(dr("ArticleCode").ToString(), taxGrossAmt, dr, dsMain, SoNumber, dr("EAN").ToString(), True, packIndex, hdrQty, excAmt)
                'Else
                ' dr("IncTaxAmt") = (dr("IncTaxAmt") / (preQty)) * dr("Quantity")
                ' dr("TotalTaxAmt") = FormatNumber(dr("ExclTaxAmt") + dr("IncTaxAmt"), 2)
                If preQty = 0 Then
                    preQty = 1
                End If
                'dr("TotalTaxAmt") = FormatNumber((If(dr("TotalTaxAmt") Is DBNull.Value, 0, dr("TotalTaxAmt")) / (preQty)) * dr("Quantity"), 2)
                'End If
                Dim discountAmount As Decimal = If(dr("Discount") Is DBNull.Value, 0, dr("Discount"))
                'Dim taxAmount As Decimal = If(dr("TotalTaxAmt") Is DBNull.Value, 0, dr("TotalTaxAmt"))
                If TotalPickUpDisc IsNot Nothing Then
                    Dim result As DataRow() = TotalPickUpDisc.Select("BillLineNo='" + dr("BillLineNo").ToString() + "' and pkgLineNo='" + packIndex.ToString() + "'")
                    If result.Length > 0 Then
                        dr("NetAmount") = FormatNumber((dr("GrossAmt") + IIf(dr("ExclTaxAmt") Is DBNull.Value, 0, dr("ExclTaxAmt"))) - discountAmount, 2) - result(0)("DiscountAmount")
                    Else
                        dr("NetAmount") = FormatNumber((dr("GrossAmt") + IIf(dr("ExclTaxAmt") Is DBNull.Value, 0, dr("ExclTaxAmt"))) - discountAmount, 2)
                    End If
                Else
                    dr("NetAmount") = FormatNumber((dr("GrossAmt") + IIf(dr("ExclTaxAmt") Is DBNull.Value, 0, dr("ExclTaxAmt"))) - discountAmount, 2)
                End If


            Else
                'CreateDataSetForTaxCalculation(dr("ArticleCode").ToString(), dr("GrossAmt"), dr, dsMain, SoNumber, dr("EAN").ToString(), True)
                'dr("GrossAmt") = dr("Quantity") * dr("SellingPrice")
                'dr("NetAmount") = FormatNumber((dr("GrossAmt") + dr("ExclTaxAmt") + dr("IncTaxAmt")) - dr("Discount"), 2)
                '------ Changed By Mahesh 
                dr("GrossAmt") = dr("Quantity") * dr("SellingPrice")
                CreateDataSetForTaxCalculation(dr("ArticleCode").ToString(), dr("GrossAmt"), dr, dsMain, SoNumber, dr("EAN").ToString(), True)
                If TotalPickUpDisc IsNot Nothing Then
                    Dim result As DataRow() = TotalPickUpDisc.Select("BillLineNo='" + dr("BillLineNo").ToString() + "' and pkgLineNo='" + packIndex.ToString() + "'")
                    If result.Length > 0 Then
                        dr("NetAmount") = FormatNumber((dr("GrossAmt") + dr("ExclTaxAmt")) - dr("Discount"), 2) - result(0)("DiscountAmount")
                    Else
                        dr("NetAmount") = FormatNumber((dr("GrossAmt") + dr("ExclTaxAmt")) - dr("Discount"), 2)
                    End If
                Else
                    dr("NetAmount") = FormatNumber((dr("GrossAmt") + dr("ExclTaxAmt")) - dr("Discount"), 2)
                End If

            End If
            'If Not dr("PickUpQty") Is DBNull.Value AndAlso dr("PickUpQty") >= 0 Then
            '    dr("Stock") = dr("Stock") - dr("PickUpQty")
            'End If
            'dsMain.Tables("SalesOrderTaxDtls").Clear()
            'dsMain.Tables("SalesOrderTaxDtls").Merge(dtMainTax, False, MissingSchemaAction.Ignore)
        Catch ex As Exception
        End Try
    End Function
    Public Function RecalculateLinePack(ByRef dr As DataRow, ByVal SoNumber As String, ByRef dsMain As DataSet, Optional ByVal RemovePromo As Boolean = True, Optional ByVal CalcFromDb As Boolean = True, Optional preQty As Integer = 0, Optional packIndex As Integer = 0, Optional ByVal hdrQty As Integer = 0, Optional ByVal excAmt As Integer = 0, Optional ByVal TotalPickUpDisc As DataTable = Nothing)

        Try
            'dtMainTax = dsMain.Tables("SalesOrderTaxDtls").Copy()
            dr("Discount") = 0
            dr("PromotionId") = 0
            dr("LineDiscount") = 0
            dr("TotalDiscPercentage") = 0
            dr("FirstLevel") = String.Empty
            dr("TopLevel") = String.Empty

            If RemovePromo = True Then
                'dr("GrossAmt") = dr("Quantity") * dr("SellingPrice")
                Dim taxGrossAmt As Decimal = 0
                taxGrossAmt = (dr("Quantity") + hdrQty) * dr("SellingPrice")
                'If CalcFromDb Then
                CreateSODataSetForTaxCalculationPack(dr("ArticleCode").ToString(), taxGrossAmt, dr, dsMain, SoNumber, dr("EAN").ToString(), True, packIndex, hdrQty, excAmt)
                'Else
                ' dr("IncTaxAmt") = (dr("IncTaxAmt") / (preQty)) * dr("Quantity")
                ' dr("TotalTaxAmt") = FormatNumber(dr("ExclTaxAmt") + dr("IncTaxAmt"), 2)
                If preQty = 0 Then
                    preQty = 1
                End If
                'dr("TotalTaxAmt") = FormatNumber((If(dr("TotalTaxAmt") Is DBNull.Value, 0, dr("TotalTaxAmt")) / (preQty)) * dr("Quantity"), 2)
                'End If
                Dim discountAmount As Decimal = If(dr("Discount") Is DBNull.Value, 0, dr("Discount"))
                'Dim taxAmount As Decimal = If(dr("TotalTaxAmt") Is DBNull.Value, 0, dr("TotalTaxAmt"))
                dr("GrossAmt") = dr("Quantity") * dr("SellingPrice")
                dr("GrossAmount") = dr("Quantity") * dr("SellingPrice")
                If TotalPickUpDisc IsNot Nothing Then
                    Dim result As DataRow() = TotalPickUpDisc.Select("BillLineNo='" + dr("BillLineNo").ToString() + "' and pkgLineNo='" + packIndex.ToString() + "'")
                    If result.Length > 0 Then
                        dr("NetAmount") = FormatNumber((dr("GrossAmt") + IIf(dr("ExclTaxAmt") Is DBNull.Value, 0, dr("ExclTaxAmt"))) - discountAmount, 2) - result(0)("DiscountAmount")
                    Else
                        dr("NetAmount") = FormatNumber((dr("GrossAmt") + IIf(dr("ExclTaxAmt") Is DBNull.Value, 0, dr("ExclTaxAmt"))) - discountAmount, 2)
                    End If
                Else
                    dr("NetAmount") = FormatNumber((dr("GrossAmt") + IIf(dr("ExclTaxAmt") Is DBNull.Value, 0, dr("ExclTaxAmt"))) - discountAmount, 2)
                End If


            Else
                'CreateDataSetForTaxCalculation(dr("ArticleCode").ToString(), dr("GrossAmt"), dr, dsMain, SoNumber, dr("EAN").ToString(), True)
                'dr("GrossAmt") = dr("Quantity") * dr("SellingPrice")
                'dr("NetAmount") = FormatNumber((dr("GrossAmt") + dr("ExclTaxAmt") + dr("IncTaxAmt")) - dr("Discount"), 2)
                '------ Changed By Mahesh 
                dr("GrossAmt") = dr("Quantity") * dr("SellingPrice")
                dr("GrossAmount") = dr("Quantity") * dr("SellingPrice")
                CreateDataSetForTaxCalculation(dr("ArticleCode").ToString(), dr("GrossAmt"), dr, dsMain, SoNumber, dr("EAN").ToString(), True)
                If TotalPickUpDisc IsNot Nothing Then
                    Dim result As DataRow() = TotalPickUpDisc.Select("BillLineNo='" + dr("BillLineNo").ToString() + "' and pkgLineNo='" + packIndex.ToString() + "'")
                    If result.Length > 0 Then
                        dr("NetAmount") = FormatNumber((dr("GrossAmt") + dr("ExclTaxAmt")) - dr("Discount"), 2) - result(0)("DiscountAmount")
                    Else
                        dr("NetAmount") = FormatNumber((dr("GrossAmt") + dr("ExclTaxAmt")) - dr("Discount"), 2)
                    End If
                Else
                    dr("NetAmount") = FormatNumber((dr("GrossAmt") + dr("ExclTaxAmt")) - dr("Discount"), 2)
                End If


            End If
            'If Not dr("PickUpQty") Is DBNull.Value AndAlso dr("PickUpQty") >= 0 Then
            '    dr("Stock") = dr("Stock") - dr("PickUpQty")
            'End If
            'dsMain.Tables("SalesOrderTaxDtls").Clear()
            'dsMain.Tables("SalesOrderTaxDtls").Merge(dtMainTax, False, MissingSchemaAction.Ignore)
        Catch ex As Exception
        End Try
    End Function

    Public Function CreateSODataSetForTaxCalculation(ByVal strMatcode As String, ByVal TaxableAmount As Double, ByRef dr As DataRow, ByRef dsMain As DataSet, ByVal SONumber As String, Optional ByVal EAN As String = "", Optional ByVal isInclusiveCalc As Boolean = False, Optional ByVal PackIndex As Integer = 0, Optional ByVal hdrQty As Integer = 0, Optional ByVal excAmt As Integer = 0) As Object
        Try
            Dim dtMainTax As DataTable
            Dim StrTaxCode As Double = 0
            Dim dtTaxCalc As DataTable
            dtTaxCalc = New DataTable
            Dim ExlusiveFound As Boolean = False
            ObjComm.DecimalDigits = clsDefaultConfiguration.DecimalPlaces
            'Dim dvTax As New DataView(dtMainTax, "EAN='" & EAN & "'", "", DataViewRowState.CurrentRows)
            'If dvTax.Count > 0 Then
            '    dtTaxCalc = dvTax.ToTable()
            '    dvTax.AllowDelete = True
            '    Dim i As Integer
            '    For i = dvTax.Count - 1 To 0 Step -1
            '        dvTax.Delete(i)
            '    Next
            'Else
            If IsCSTApplicable Then
                dtTaxCalc = ObjComm.getTax(clsAdmin.SiteCode, strMatcode, "SO201", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, True)
            Else
                dtTaxCalc = ObjComm.getTax(clsAdmin.SiteCode, strMatcode, "SO201", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, False)
            End If
            ' dtTaxCalc = ObjComm.getTax(clsAdmin.SiteCode, strMatcode, "SO201", dr("Quantity"), EAN)
            'End If
            Dim dbIncTotalTax As Double = 0
            Dim dbExclTotalTax As Double = 0
            If dtTaxCalc.Rows.Count = 0 Then
                Return Nothing
            End If
            If dtTaxCalc.Rows.Count <> 0 Then
                'dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
                'ObjComm.getCalculatedDataSet(dtTaxCalc, dr("Quantity"))
                If IsCSTApplicable And isInclusiveCalc = False Then

                    'hdrQty = hdrQty + dr("Quantity")

                    'Dim amt As Double = GetTaxableAmountForCst(strMatcode, EAN, hdrQty, TaxableAmount)
                    Dim amt As Double = GetTaxableAmountForCst(strMatcode, EAN, dr("Quantity"), TaxableAmount)
                    'originalTaxAmt = amt
                    dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount - amt
                    ObjComm.getCalculatedDataSet(dtTaxCalc)
                Else
                    dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
                    ObjComm.getCalculatedDataSet(dtTaxCalc)
                    'originalTaxAmt = dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")
                End If
                Dim iRowTax As Int16
                With dtTaxCalc
                    For iRowTax = 0 To dtTaxCalc.Rows.Count - 1
                        If CDbl(dtTaxCalc.Rows(iRowTax)("VALUE").ToString) <> 0 Then
                            If dtTaxCalc.Rows(iRowTax)("INCLUSIVE") = "0" Then
                                dbExclTotalTax = dbExclTotalTax + CDbl(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT"))
                            Else
                                dbIncTotalTax = dbIncTotalTax + CDbl(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT"))
                            End If
                        End If
                    Next
                End With
                'If excAmt > 0 Then
                '    If dbExclTotalTax > 0 Then
                '        dr("ExclTaxAmt") = FormatNumber(dbExclTotalTax - excAmt, 2)
                '    Else
                '        dr("ExclTaxAmt") = FormatNumber(dbExclTotalTax, 2)
                '    End If

                '    If dbIncTotalTax > 0 Then
                '        dr("IncTaxAmt") = FormatNumber(dbIncTotalTax - excAmt, 2)
                '    Else
                '        dr("IncTaxAmt") = FormatNumber(dbIncTotalTax, 2)
                '    End If

                '    dr("TotalTaxAmt") = FormatNumber(dbExclTotalTax - excAmt + dbIncTotalTax, 2)
                'Else
                dr("ExclTaxAmt") = FormatNumber(dbExclTotalTax, 2)
                dr("IncTaxAmt") = FormatNumber(dbIncTotalTax, 2)
                dr("TotalTaxAmt") = FormatNumber(dbExclTotalTax + dbIncTotalTax, 2)
                'End If

                StrTaxCode = dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")
                'dr("TOTALTAXAMOUNT") = StrTaxCode
                'dtMainTax.Merge(dtTaxCalc, False, MissingSchemaAction.Ignore)
                'dtMainTax.AcceptChanges()

                Dim findkeyTax(4) As Object
                For Each drRowTax In dtTaxCalc.Rows
                    Dim results As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("EAN='" + EAN + "'   and TaxLabel='" + drRowTax("TaxCode") + "'")

                    findkeyTax(0) = clsAdmin.SiteCode
                    findkeyTax(1) = clsAdmin.Financialyear
                    findkeyTax(2) = SONumber
                    findkeyTax(3) = dr("EAN")
                    'findkeyTax(4) = drRowTax("TaxCode")
                    If results.Length > 0 Then

                        findkeyTax(4) = results(0)("TaxLineNo")
                    End If
                    'findkeyTax(5) = PackIndex
                    Dim drTax As DataRow
                    drTax = dsMain.Tables("SalesOrderTaxDtls").Rows.Find(findkeyTax)
                    If drTax Is Nothing Then
                        drTax = dsMain.Tables("SalesOrderTaxDtls").NewRow

                        drTax("SiteCode") = clsAdmin.SiteCode
                        drTax("FinYear") = clsAdmin.Financialyear
                        drTax("SaleOrderNumber") = SONumber
                        drTax("EAN") = dr("EAN")
                        drTax("TaxLineNo") = dsMain.Tables("SalesOrderTaxDtls").Rows.Count + 1
                        drTax("TaxLabel") = drRowTax("TaxCode")
                        drTax("TaxValue") = drRowTax("TaxAmount")

                        dsMain.Tables("SalesOrderTaxDtls").Rows.Add(drTax)
                    Else
                        'drTax("TaxLineNo") = PackIndex
                        'drTax("TaxLabel") = drRowTax("TaxCode")
                        drTax("TaxValue") = Math.Round(drRowTax("TaxAmount"), 2) + excAmt
                    End If
                    'drTax("SiteCode") = clsAdmin.SiteCode
                    'drTax("SaleOrderNumber") = SONumber
                    'drTax("EAN") = dr("EAN").ToString()

                Next

                Return StrTaxCode
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM012"), "CM012 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in updating Tax Calculation", "Error")
            Return Nothing
        End Try
    End Function
    Public Function CreateSODataSetForTaxCalculationPack(ByVal strMatcode As String, ByVal TaxableAmount As Double, ByRef dr As DataRow, ByRef dsMain As DataSet, ByVal SONumber As String, Optional ByVal EAN As String = "", Optional ByVal isInclusiveCalc As Boolean = False, Optional ByVal PackIndex As Integer = 0, Optional ByVal hdrQty As Integer = 0, Optional ByVal excAmt As Integer = 0) As Object
        Try
            Dim dtMainTax As DataTable
            Dim StrTaxCode As Double = 0
            Dim dtTaxCalc As DataTable
            dtTaxCalc = New DataTable
            Dim ExlusiveFound As Boolean = False
            ObjComm.DecimalDigits = clsDefaultConfiguration.DecimalPlaces
            'Dim dvTax As New DataView(dtMainTax, "EAN='" & EAN & "'", "", DataViewRowState.CurrentRows)
            'If dvTax.Count > 0 Then
            '    dtTaxCalc = dvTax.ToTable()
            '    dvTax.AllowDelete = True
            '    Dim i As Integer
            '    For i = dvTax.Count - 1 To 0 Step -1
            '        dvTax.Delete(i)
            '    Next
            'Else
            If IsCSTApplicable Then
                dtTaxCalc = ObjComm.getTax(clsAdmin.SiteCode, strMatcode, "SO201", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, True)
            Else
                dtTaxCalc = ObjComm.getTax(clsAdmin.SiteCode, strMatcode, "SO201", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, False)
            End If
            ' dtTaxCalc = ObjComm.getTax(clsAdmin.SiteCode, strMatcode, "SO201", dr("Quantity"), EAN)
            'End If
            Dim dbIncTotalTax As Double = 0
            Dim dbExclTotalTax As Double = 0
            If dtTaxCalc.Rows.Count = 0 Then
                Return Nothing
            End If
            If dtTaxCalc.Rows.Count <> 0 Then
                'dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
                'ObjComm.getCalculatedDataSet(dtTaxCalc, dr("Quantity"))
                If IsCSTApplicable And isInclusiveCalc = False Then

                    'hdrQty = hdrQty + dr("Quantity")

                    'Dim amt As Double = GetTaxableAmountForCst(strMatcode, EAN, hdrQty, TaxableAmount)
                    Dim amt As Double = GetTaxableAmountForCst(strMatcode, EAN, dr("Quantity"), TaxableAmount)
                    'originalTaxAmt = amt
                    dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount - amt
                    ObjComm.getCalculatedDataSet(dtTaxCalc)
                Else
                    dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
                    ObjComm.getCalculatedDataSet(dtTaxCalc)
                    'originalTaxAmt = dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")
                End If
                Dim iRowTax As Int16
                With dtTaxCalc
                    For iRowTax = 0 To dtTaxCalc.Rows.Count - 1
                        If CDbl(dtTaxCalc.Rows(iRowTax)("VALUE").ToString) <> 0 Then
                            If dtTaxCalc.Rows(iRowTax)("INCLUSIVE") = "0" Then
                                dbExclTotalTax = dbExclTotalTax + CDbl(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT"))
                            Else
                                dbIncTotalTax = dbIncTotalTax + CDbl(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT"))
                            End If
                        End If
                    Next
                End With
                'If excAmt > 0 Then
                '    If dbExclTotalTax > 0 Then
                '        dr("ExclTaxAmt") = FormatNumber(dbExclTotalTax - excAmt, 2)
                '    Else
                '        dr("ExclTaxAmt") = FormatNumber(dbExclTotalTax, 2)
                '    End If

                '    If dbIncTotalTax > 0 Then
                '        dr("IncTaxAmt") = FormatNumber(dbIncTotalTax - excAmt, 2)
                '    Else
                '        dr("IncTaxAmt") = FormatNumber(dbIncTotalTax, 2)
                '    End If

                '    dr("TotalTaxAmount") = FormatNumber(dbExclTotalTax - excAmt + dbIncTotalTax, 2)
                'Else
                dr("ExclTaxAmt") = FormatNumber(dbExclTotalTax, 2)
                dr("IncTaxAmt") = FormatNumber(dbIncTotalTax, 2)
                dr("TotalTaxAmount") = FormatNumber(dbExclTotalTax + dbIncTotalTax, 2)
                'End If

                StrTaxCode = dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")
                'dr("TOTALTAXAMOUNT") = StrTaxCode
                'dtMainTax.Merge(dtTaxCalc, False, MissingSchemaAction.Ignore)
                'dtMainTax.AcceptChanges()

                Dim findkeyTax(4) As Object
                For Each drRowTax In dtTaxCalc.Rows
                    Dim results As DataRow() = dsMain.Tables("SalesOrderTaxDtls").Select("EAN='" + EAN + "'   and TaxLabel='" + drRowTax("TaxCode") + "'")

                    findkeyTax(0) = clsAdmin.SiteCode
                    findkeyTax(1) = clsAdmin.Financialyear
                    findkeyTax(2) = SONumber
                    findkeyTax(3) = dr("EAN")
                    'findkeyTax(4) = drRowTax("TaxCode")
                    If results.Length > 0 Then

                        findkeyTax(4) = results(0)("TaxLineNo")
                    End If
                    'findkeyTax(5) = PackIndex
                    Dim drTax As DataRow
                    drTax = dsMain.Tables("SalesOrderTaxDtls").Rows.Find(findkeyTax)
                    If drTax Is Nothing Then
                        drTax = dsMain.Tables("SalesOrderTaxDtls").NewRow

                        drTax("SiteCode") = clsAdmin.SiteCode
                        drTax("FinYear") = clsAdmin.Financialyear
                        drTax("SaleOrderNumber") = SONumber
                        drTax("EAN") = dr("EAN")
                        drTax("TaxLineNo") = dsMain.Tables("SalesOrderTaxDtls").Rows.Count + 1
                        drTax("TaxLabel") = drRowTax("TaxCode")
                        drTax("TaxValue") = drRowTax("TaxAmount")

                        dsMain.Tables("SalesOrderTaxDtls").Rows.Add(drTax)
                    Else
                        'drTax("TaxLineNo") = PackIndex
                        'drTax("TaxLabel") = drRowTax("TaxCode")
                        drTax("TaxValue") = Math.Round(drRowTax("TaxAmount"), 2) + excAmt
                    End If
                    'drTax("SiteCode") = clsAdmin.SiteCode
                    'drTax("SaleOrderNumber") = SONumber
                    'drTax("EAN") = dr("EAN").ToString()

                Next

                Return StrTaxCode
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM012"), "CM012 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in updating Tax Calculation", "Error")
            Return Nothing
        End Try
    End Function
    Private Function GetTaxableAmountForCst(ByVal strMatcode As String, ByVal EAN As String, ByVal Quantity As Double, ByVal TaxableAmount As Double) As Double
        Dim dtTaxCalc As DataTable
        dtTaxCalc = New DataTable
        dtTaxCalc = ObjComm.getTax(clsAdmin.SiteCode, strMatcode, "CMS", Quantity, EAN, clsDefaultConfiguration.CSTTaxCode, False)
        dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
        ObjComm.getCalculatedDataSet(dtTaxCalc)
        Return dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")
    End Function
End Class
