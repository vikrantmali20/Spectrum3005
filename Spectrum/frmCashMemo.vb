﻿Imports SpectrumBL
Imports System.IO
Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient
Imports SpectrumPrint
Imports SpectrumCommon
'Imports System.Globalization
'Imports System.Threading
Imports Microsoft.Reporting.WinForms
Imports System.Text
Imports Spire.Pdf
Imports System.IO.Ports
Imports Spectrum.BL
''' <summary>
''' This class is used for cash Memo Functionality 
''' </summary>
''' <CreatedBy>Rama Ranjan Jena </CreatedBy>
''' <Updatedby></Updatedby>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
Public Class frmCashMemo
    Inherits CtrlRbnBaseForm

#Region "Global Varibale for class"
    Dim objSearch As New frmNCommonSearch
    Dim dtPrescDtlCashMemoAddToBill As New DataTable
    Dim dt As New DataTable
    Dim _prescriptionArticleAmount As String = ""
    Dim RecalculateTax As Boolean = False
    Dim objCM As New clsCashMemo
    Dim objArticleCombo As New clsArticleCombo
    Dim dsMain As New DataSet
    Dim dtMainTax As New DataTable
    Dim DtManualPromo, dtGV As DataTable
    Dim UpdateFlag As Boolean = False
    Dim HoldFlag As Boolean = False
    Dim DvItemDetail As DataView
    Dim TotalExclusiveTax, RoundedAmt, CustomerBalancePoint As Double
    'Dim ShowEan As Boolean = False
    Dim CLPCardType, CLPCustomerId, clpCustomerMobileNo As String
    Dim PromotionCleared As Boolean = False
    Dim GiftMsg As String = ""
    Private filterScanArticle As String = String.Empty
    Dim pkey As String = ""
    Dim exitNowFlag As Boolean = False
    Private _dDueDate As Date
    Private _strRemarks As String
    Private _iArticleQtyBeforeChange As Decimal
    Private IsInclusiveTax As Boolean
    Private IsCSTApplicable As Boolean
    Dim objItemSch As New clsIteamSearch
    Private customerType As String = String.Empty
    Private IsChangeQuantityOrPrice As Boolean = False
    Private _PriceBeforeChange As Nullable(Of Decimal)
    '---- Added By Mahesh for Hd fns same as Fast Cash Memo ...
    Const CustSaleTypeTimerBlinkFrequency As Int32 = 250
    Private CustSaleTypeTimer As New Timer()
    Dim dtHD As DataTable = Nothing
    Private HDPrintRequired As Boolean = False
    Private HDMobileNo As String = String.Empty
    Dim FloatAmt As Double
    Private CustomerSaleType As Integer = 0
    Private IsHoldEnterKey As Boolean = False
    Public _paidAmt As String
    Public _billAmt As String
    Dim EditMode_IsupdateDeliveryPersonAllowed As Boolean
    Private IsTenderCredit As Boolean = False
    Private IsTenderCash As Boolean = False
    Private IsTenderCheque As Boolean = False
    Private IsTenderCreditCard As Boolean = False
    Dim IsRoundOffMsg As Boolean = False
    Public GVBasedAricleReturnList As New Dictionary(Of String, String)
    Public _remarks As String
    Dim IsDefaultPromotion As Boolean = False
    Dim CardNo As String
    Dim IsMembership As Boolean = False
    Dim objclsMemb As New clsMembership
    Dim dtMembData As New DataTable
    Dim Membershipid As String = ""
    Dim isApplicableforcashReturn As Boolean
    Dim retuntAmt As New DataTable
    Dim QRTrailerSeperator As String = "" 'vipin
    Dim QRTrailerSegment As String = ""
    Dim QRSepearatorSegment As String = ""
    Dim QRItemsScanString As String = ""
    Dim iscalledFromReturnClick As Boolean = False
    Dim objcom As New clsCommon
    Enum enumCustomerSaleType
        Dine_In = 1
        Home_Delivery = 2
        Take_Away = 3
    End Enum
    Public Shared _paymentTermId As String
    Public Shared Property PaymentTermId() As String
        Get
            Return _paymentTermId
        End Get
        Set(value As String)
            _paymentTermId = value
        End Set
    End Property
    '-----Added for if Scan Bill is set true then follow some rules
    Private ScanBill As Boolean = False
    Dim dtScanedBillArticle As New DataTable
    Dim ScanItemBillSequence As Integer = 0
    ' Create List of Dictionary of holding bill and total qty .
    'Dim ScanBillsDictionary As New  Dictionary(Of String, Integer)
    Dim ScaleBillNo As String = ""
    Dim ScaleBillIntDate As Long

    '---- Credit sale DataTable for return Cases 
    Dim dtCreditSaleData As New DataTable

#End Region
#Region "Private Methods And Functions"
    'Private Sub ShowLastOper(ByVal Ean As String, ByVal Desc As String, ByVal Qty As String)
    '    Try
    '        lblItemCode.Text = Ean
    '        lblItemDis.Text = Desc
    '        lblQty.Text = Qty
    '    Catch ex As Exception

    '    End Try
    'End Sub
    ''' <summary>
    ''' Getting the List of all promotions Active in Site
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetListofManualPromotion()
        Try
            DtManualPromo = objCM.GetManualPromotion(clsAdmin.SiteCode)
            If Not DtManualPromo Is Nothing AndAlso DtManualPromo.Rows.Count > 0 Then
                cbManualDisc.DataSource = DtManualPromo
                cbManualDisc.DisplayMember = "PROMOTIONNAME"
                cbManualDisc.ValueMember = "PROMOTIONID"
                cbManualDisc.ExtendRightColumn = True
                For Each r As C1.Win.C1List.Split In cbManualDisc.Splits
                    Dim i As Integer
                    For i = 0 To r.DisplayColumns.Count - 1
                        If r.DisplayColumns(i).Name <> cbManualDisc.DisplayMember Then
                            r.DisplayColumns(i).Visible = False
                        End If
                    Next
                Next
                cbManualDisc.SelectedIndex = -1
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Set the Resource 
    ''' </summary>
    ''' <param name="ctrls"></param>
    ''' <remarks></remarks>
    'Private Sub SetCulture(ByRef ctrls As Control)
    '    Try
    '        If Not resourceMgr Is Nothing Then
    '            For Each ctrl As Control In ctrls.Controls
    '                If TypeOf ctrl Is Button Or TypeOf ctrl Is Label Then
    '                    If getValueByKey  (Me.Name & "_" & ctrl.Name & ".Text") <> String.Empty Then
    '                        ctrl.Text = getValueByKey  (Me.Name & "_" & ctrl.Name & ".Text")
    '                    End If
    '                End If
    '                If ctrl.Controls.Count > 0 Then
    '                    SetCulture(ctrl)
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        ShowMessage(ex.Message, "Error")
    '        LogException(ex)
    '    End Try
    'End Sub
    ''' <summary>
    ''' Applying the Manual Promotion in Current Row
    ''' </summary>
    ''' <remarks>Manual Promotion may be(%,fixed price off,Fixed price sale) </remarks>
    Private Sub ApplyManualPromotion(ByVal Ean As String, Optional ByVal dPreviousQty As Decimal = 0, Optional ByVal batchBarCode As String = "")
        Try
            'If cbManualDisc.Enabled = True AndAlso cbManualDisc.SelectedIndex > -1 Then
            Dim PromoId As String = String.Empty
            Dim PromoValue, DiscountPer As Double
            Dim PromoVal As String = ""
            DiscountPer = 0
            PromoVal = 0
            If cbManualDisc.Enabled = True AndAlso cbManualDisc.SelectedIndex > -1 Then
                PromoId = cbManualDisc.SelectedValue
            End If
            Dim dvArticle As DataView
            If clsDefaultConfiguration.IsBatchManagementReq Then
                dvArticle = New DataView(dsMain.Tables("CASHMEMODTL"), "EAN='" & Ean & "' AND BTYPE='S' And BatchBarcode='" & batchBarCode & "'", "Ean", DataViewRowState.CurrentRows)
            Else
                dvArticle = New DataView(dsMain.Tables("CASHMEMODTL"), "EAN='" & Ean & "' AND BTYPE='S'", "Ean", DataViewRowState.CurrentRows)
            End If


            Dim drMain As DataRow = dvArticle.Item(0).Row
            Dim TotalQty, TotalAmt As Double
            TotalAmt = CDbl(drMain("GROSSAMT"))
            TotalQty = CDbl(drMain("QUANTITY"))

            For Each dr As DataRow In DtManualPromo.Select("PROMOTIONID='" & PromoId & "'", "PROMOTIONID", DataViewRowState.CurrentRows)
                PromoValue = dr("PROMOTIONVALUE")
                If dr("DISCPER").ToString().ToUpper = "TRUE" Then
                    DiscountPer = PromoValue
                    PromoVal = PromoValue
                ElseIf dr("FIXEDPRICEOFF").ToString().ToUpper = "TRUE" Then
                    DiscountPer = ((PromoValue * TotalQty) * 100) / TotalAmt
                    PromoVal = PromoValue & "R"
                ElseIf dr("FIXEDSELLING").ToString().ToUpper = "TRUE" Then
                    DiscountPer = (TotalAmt - (PromoValue * TotalQty)) * 100 / TotalAmt
                    PromoVal = PromoValue & "S"
                End If
            Next
            If DiscountPer > 100 Then
                ShowMessage(getValueByKey("CM001"), "CM001 - " & getValueByKey("CLAE04"))
                'ShowMessage("Discount Cannot be greater than 100%", "Information")
                Exit Sub
            ElseIf DiscountPer < 0 Then
                ShowMessage(getValueByKey("CM002"), "CM002 - " & getValueByKey("CLAE04"))
                'ShowMessage("Discount Cannot be Less than 0%", "Information")
                Exit Sub
            End If

            'Changed  by Rohit for issue no. 6061
            ''---Updated Code By Mahesh for Mettler Qty logic is simple qty not occour in one time ...
            Dim decTotalDiscPercentage As Decimal = 0
            decTotalDiscPercentage = IIf(drMain("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drMain("TOTALDISCPERCENTAGE"))
            If TotalQty > 0 Then
                If dPreviousQty > 0 Then
                    drMain("TOTALDISCPERCENTAGE") = ((decTotalDiscPercentage * dPreviousQty) + (DiscountPer * (TotalQty - dPreviousQty))) / TotalQty
                    'ElseIf decTotalDiscPercentage = 0 AndAlso TotalQty > 1 Then
                    '    drMain("TOTALDISCPERCENTAGE") = DiscountPer
                Else
                    drMain("TOTALDISCPERCENTAGE") = ((decTotalDiscPercentage * (TotalQty - 1)) + DiscountPer) / TotalQty
                End If

            Else
                drMain("TOTALDISCPERCENTAGE") = DiscountPer
            End If

            decTotalDiscPercentage = IIf(drMain("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drMain("TOTALDISCPERCENTAGE"))
            drMain("LINEDISCOUNT") = (decTotalDiscPercentage * TotalAmt) / 100

            'Change End

            drMain("FIRSTLEVEL") = PromoId
            drMain("TOPLEVEL") = PromoId
            drMain("MANUALPROMO") = PromoVal
            ReCalculateCM("")
            'End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM003"), "CM003 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in applying Manual Promotion", "Error")
        End Try
    End Sub
    ''' <summary>
    ''' Calulating Manual promotion on the pass Data Row
    ''' </summary>
    ''' <param name="dr">Detail Table row</param>
    ''' <remarks></remarks>
    Private Overloads Sub CalculateManualPromo(ByRef dr As DataRow)
        Try
            'MANUALPROMO
            Dim PromoValue, DiscountAmt, DiscPer As Double
            If dr("MANUALPROMO").ToString = String.Empty Then Exit Sub
            Dim type As String = dr("MANUALPROMO").ToString().Substring(dr("MANUALPROMO").ToString.Length - 1)
            If dr("MANUALPROMO").ToString() <> "" AndAlso type = "S" Then
                PromoValue = dr("MANUALPROMO").ToString().Substring(0, dr("MANUALPROMO").ToString.Length - 1)
                dr("LINEDISCOUNT") = dr("GROSSAMT") - (PromoValue * dr("Quantity"))
            ElseIf dr("MANUALPROMO").ToString() <> "" AndAlso type = "R" Then
                PromoValue = dr("MANUALPROMO").ToString().Substring(0, dr("MANUALPROMO").ToString.Length - 1)
                dr("LINEDISCOUNT") = PromoValue * dr("Quantity")
            ElseIf dr("MANUALPROMO").ToString() <> "" Then
                PromoValue = dr("MANUALPROMO")
                DiscountAmt = (dr("GROSSAMT") * PromoValue) / 100
                DiscPer = PromoValue
                dr("TOTALDISCPERCENTAGE") = DiscPer
                dr("LINEDISCOUNT") = DiscountAmt
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM004"), "CM004 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in calculating Manual Promotion", "Error")
        End Try
    End Sub
    ''' <summary>
    ''' Calulating Manual promotion on the pass Data Row
    ''' </summary>
    ''' <param name="rowno">Row Number</param>
    ''' <remarks></remarks>
    Private Overloads Sub CalculateManualPromo(ByVal Ean As String)
        Try
            For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Select("EAN='" & Ean & "' AND BTYPE='S'", "", DataViewRowState.CurrentRows)
                CalculateManualPromo(dr)
                dr.AcceptChanges()
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    'Private Sub CheckIfInclusiveTax()
    '    If clsDefaultConfiguration.InclusiveTaxDisplay.ToString().ToUpper() = "TRUE" Then
    '        'If objCM.CheckIfInclusiveTax(clsAdmin.SiteCode, "CMS") Then
    '        IsInclusiveTax = True
    '        'End If
    '    End If
    'End Sub

    ''' <summary>
    ''' Binding the Controls with the DataSet
    ''' </summary>
    ''' <param name="StrArticle">ArticleCode</param>
    ''' <remarks></remarks>
    Private Sub getBinding()
        Try
            dsMain = objCM.GetStruc("0", "0")
            dsMain.Tables("CASHMEMODTL").Columns("IsPriceChanged").DefaultValue = False
            'DvItemDetail = New DataView(dsMain.Tables("CASHMEMODTL"), "", "BillLineNo Desc", DataViewRowState.CurrentRows)
            'dsMain.Tables("CASHMEMODTL").DefaultView.Sort = "BillLineNo Desc"
            dgMainGrid.DataSource = dsMain.Tables("CASHMEMODTL")

            Payment.CtrlListPayment.DataSource = dsMain.Tables("CASHMEMORECEIPT")
            CustInfo.CtrlTxtCustomerNo.DataBindings.Add("Text", dsMain.Tables("CASHMEMOHDR"), "CLPNO")
            CashSummary.CtrllblDiscAmt.DataBindings.Add("Text", dsMain.Tables("CASHMEMOHDR"), "TOTALDISCOUNT")
            CashSummary.CtrllblGrossAmt.DataBindings.Add("Text", dsMain.Tables("CASHMEMOHDR"), "GROSSAMT")
            CashSummary.CtrllblNetAmt.DataBindings.Add("Text", dsMain.Tables("CASHMEMOHDR"), "NETAMT")
            'CustInfo.ctrlTxtPoints.DataBindings.Add("Text", dsMain.Tables("CASHMEMOHDR"), "CLPPoints")

            dtMainTax = objCM.getTax("", "", "", "0", "")
            dtMainTax.TableName = "CASHMEMOTAXDTLS"
            'lblUID.Text = clsAdmin.UserCode
            GridSettings(UpdateFlag)
            'lblCMDate.Text = Now.Date


        Catch ex As Exception
            ShowMessage(getValueByKey("CM005"), "CM005 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Transaction Not properly Binded", "Error")
        End Try
    End Sub
    ''' <summary>
    ''' Applying Default Setting on Screen Set 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDefaultSetting()
        Try
            If clsDefaultConfiguration.ManualPromotionAllowed = True Then
                cbManualDisc.Enabled = True
                cmdEnable.Enabled = True
            Else
                cbManualDisc.Enabled = False
                cmdEnable.Enabled = False
            End If
            'If clsDefaultConfiguration.SalesPersonApplicable = True Then
            '    CtrlSalesPersons.CtrlSalesPersons.Enabled = True
            'Else
            '    CtrlSalesPersons.CtrlSalesPersons.Enabled = False
            'End If
            If clsDefaultConfiguration.AdvanceSalesAllowed = True Then
                CMbtnBottom.CtrlBtnSaleCLPPoint.Enabled = True
            Else
                CMbtnBottom.CtrlBtnSaleCLPPoint.Enabled = False
            End If

            If clsDefaultConfiguration.GVsaleAllowed = True Then
                CMbtnBottom.CtrlBtnSaleGV.Enabled = True
            Else
                CMbtnBottom.CtrlBtnSaleGV.Enabled = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Set the Grid Based on Flag
    ''' </summary>
    ''' <param name="Update">Update flag</param>
    ''' <remarks></remarks>
    Private Sub GridSettings(ByVal Update As Boolean)
        Try
            'IFBNO
            dgMainGrid.Cols("IFBNO").Caption = " "
            dgMainGrid.Cols("Selects").Caption = " "
            dgMainGrid.Cols("Selects").Width = 40
            dgMainGrid.Cols("Selects").ComboList = "..."

            If (clsDefaultConfiguration.BarcodeDisplayAllowed) Then
                ' added by Khusrao Adil
                ' for savoy
                If clsDefaultConfiguration.IsSavoy Then
                    dgMainGrid.Cols("EAN").Caption = "Model No."
                Else
                    dgMainGrid.Cols("EAN").Caption = IIf(resourceMgr Is Nothing, "Barcode", getValueByKey("frmcashmemo.dgmaingrid.ean"))
                End If
                dgMainGrid.Cols("EAN").Width = 150
                dgMainGrid.Cols("EAN").AllowEditing = False
                dgMainGrid.Cols("EAN").Visible = True
            Else
                dgMainGrid.Cols("EAN").Visible = False
            End If
            dgMainGrid.Cols("Btype").AllowEditing = False
            dgMainGrid.Cols("DISCRIPTION").Width = 220
            'dgMainGrid.Cols("DISCRIPTION").Caption = IIf(resourceMgr Is Nothing, "Item Description", getValueByKey  ("frmCashMemo_dgmainGrid_col_ItemDesc"))
            dgMainGrid.Cols("DISCRIPTION").Caption = IIf(resourceMgr Is Nothing, "Item Description", getValueByKey("frmcashmemo.dgmaingrid.discription"))
            dgMainGrid.Cols("DISCRIPTION").AllowEditing = False
            'dgMainGrid.Cols("STOCK").Width = 62
            'dgMainGrid.Cols("STOCK").Caption = IIf(resourceMgr Is Nothing, "Stock", getValueByKey  ("frmCashMemo_dgmainGrid_col_Stock"))
            'dgMainGrid.Cols("STOCK").AllowEditing = False
            dgMainGrid.Cols("ArticleCode").Width = 140
            'dgMainGrid.Cols("ArticleCode").Caption = IIf(resourceMgr Is Nothing, "Item Code", getValueByKey  ("frmCashMemo_dgmainGrid_col_Item"))
            dgMainGrid.Cols("ArticleCode").Caption = IIf(resourceMgr Is Nothing, "Item Code", getValueByKey("frmcashmemo.dgmaingrid.articlecode"))
            dgMainGrid.Cols("ArticleCode").AllowEditing = False

            dgMainGrid.Cols("TakeAwayQuantity").Width = 99

            dgMainGrid.Cols("Quantity").Width = 75
            'dgMainGrid.Cols("Quantity").Caption = IIf(resourceMgr Is Nothing, "Qty", getValueByKey  ("frmCashMemo_dgmainGrid_col_Quantity"))
            dgMainGrid.Cols("Quantity").Caption = IIf(resourceMgr Is Nothing, "Qty", getValueByKey("frmcashmemo.dgmaingrid.quantity"))

            If clsDefaultConfiguration.AllowDecimalQty Then
                dgMainGrid.Cols("Quantity").DataType = Type.GetType("System.Decimal")

                If clsDefaultConfiguration.WeightScaleEnabled Then
                    dgMainGrid.Cols("Quantity").Format = "0.000"
                Else
                    dgMainGrid.Cols("Quantity").Format = "0.00"
                End If
            Else
                dgMainGrid.Cols("Quantity").DataType = Type.GetType("System.Int32")
                dgMainGrid.Cols("Quantity").Format = "0"
            End If

            dgMainGrid.Cols("TakeAwayQuantity").Caption = IIf(resourceMgr Is Nothing, "TakeAway Qty", getValueByKey("frmcashmemo.dgmaingrid.TakeAwayQuantity"))
            If (clsDefaultConfiguration.AllowDecimalQty) Then
                dgMainGrid.Cols("TakeAwayQuantity").DataType = Type.GetType("System.Decimal")

                If clsDefaultConfiguration.WeightScaleEnabled Then
                    dgMainGrid.Cols("TakeAwayQuantity").Format = "0.000"
                Else
                    dgMainGrid.Cols("TakeAwayQuantity").Format = "0.00"
                End If
            Else
                dgMainGrid.Cols("TakeAwayQuantity").DataType = Type.GetType("System.Int32")
                dgMainGrid.Cols("TakeAwayQuantity").Format = "0"
            End If

            'dgMainGrid.Cols("Quantity").EditMask = "999999999"
            dgMainGrid.Cols("SellingPrice").Width = 80
            'dgMainGrid.Cols("SellingPrice").Caption = IIf(resourceMgr Is Nothing, "Price", getValueByKey  ("frmCashMemo_dgmainGrid_col_Mrp"))
            dgMainGrid.Cols("SellingPrice").Caption = "Price" 'IIf(resourceMgr Is Nothing, "Price", getValueByKey("frmcashmemo.dgmaingrid.sellingprice"))
            dgMainGrid.Cols("SellingPrice").AllowEditing = False
            dgMainGrid.Cols("TOTALDISCPERCENTAGE").Width = 56
            'dgMainGrid.Cols("TOTALDISCPERCENTAGE").Caption = IIf(resourceMgr Is Nothing, "Disc%", getValueByKey  ("frmCashMemo_dgmainGrid_col_Disc"))
            dgMainGrid.Cols("TOTALDISCPERCENTAGE").Caption = IIf(resourceMgr Is Nothing, "Disc%", getValueByKey("frmcashmemo.dgmaingrid.totaldiscpercentage"))
            dgMainGrid.Cols("TOTALDISCPERCENTAGE").AllowEditing = False

            dgMainGrid.Cols("TOTALDISCPERCENTAGE").Format = "0"

            dgMainGrid.Cols("NETAMOUNT").Width = 100
            'dgMainGrid.Cols("NETAMOUNT").Caption = IIf(resourceMgr Is Nothing, "Net Amt", getValueByKey  ("frmCashMemo_dgmainGrid_col_NetAmount"))
            dgMainGrid.Cols("NETAMOUNT").Caption = IIf(resourceMgr Is Nothing, "Net Amt", getValueByKey("frmcashmemo.dgmaingrid.netamount"))

            dgMainGrid.Cols("NETAMOUNT").Format = "0.00"

            dgMainGrid.Cols("NETAMOUNT").AllowEditing = False
            dgMainGrid.Cols("TOTALDISCOUNT").Width = 100
            dgMainGrid.Cols("TOTALDISCOUNT").Caption = getValueByKey("frmcashmemo.dgmaingrid.discamt")

            dgMainGrid.Cols("TOTALDISCOUNT").Format = "0.00"
            dgMainGrid.Cols("TOTALDISCOUNT").AllowEditing = False

            If IsInclusiveTax Then
                dgMainGrid.Cols("TOTALTAXAMOUNT").Visible = True
                dgMainGrid.Cols("TOTALTAXAMOUNT").Width = 55
                dgMainGrid.Cols("TOTALTAXAMOUNT").Format = "0.00"
                dgMainGrid.Cols("TOTALTAXAMOUNT").AllowEditing = False
            End If
            dgMainGrid.Cols("TOTALTAXAMOUNT").Caption = getValueByKey("frmcashmemo.dgmaingrid.taxamt")

            dgMainGrid.Cols("CLPRequire").Caption = getValueByKey("frmcashmemo.dgmaingrid.clprequire")
            dgMainGrid.Cols("CLPRequire").Width = 125
            If clsDefaultConfiguration.ShowDiscountAmount = False Then
                dgMainGrid.Cols("TOTALDISCOUNT").Visible = False
            Else
                dgMainGrid.Cols("TOTALDISCOUNT").Visible = True
            End If
            If Update = True Then
                dgMainGrid.AllowEditing = False
            Else
                dgMainGrid.AllowEditing = True
            End If
            'dgMainGrid.AutoSizeCols()
            'If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            '    For i = 0 To dgMainGrid.Cols.Count - 1
            '        dgMainGrid.Cols(i).Caption = dgMainGrid.Cols(i).Caption.ToUpper
            '    Next
            'End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM006"), "CM006 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Iteam Detail Screen not intiallized properly ", "Error")
        End Try
    End Sub
    ''' <summary>
    ''' CalCulating Line Item, Whole
    ''' </summary>
    ''' <param name="strItem">Ean code</param>
    ''' <remarks>If Ean code not sends Calculated All line items</remarks>
    Private Sub ReCalculateCM(ByVal strItem As String)
        Try
            Dim filter As String = "BTYPE='S'"
            If strItem <> String.Empty Then
                filter = filter & " AND  EAN='" & strItem & "'"
            End If
            For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Select(filter, "Ean", DataViewRowState.CurrentRows)
                dr("TOTALDISCOUNT") = Val(dr("LINEDISCOUNT").ToString()) + Val(dr("CLPDISCOUNT").ToString())
                'Added By Gaurav Danani 
                dr("TOTALDISCOUNT") = Math.Round(dr("TOTALDISCOUNT"), clsDefaultConfiguration.DecimalPlaces)
                'Add End
                If clsDefaultConfiguration.IsMembership Then '' MemberShip Discount Issue 
                    dr("TOTALDISCOUNT") = dr("TOTALDISCOUNT") * dr("Quantity")
                End If
                dr("GrossAmt") = dr("Quantity") * dr("SellingPrice")



                'code added  by vipul for Customer wise price
                If clsDefaultConfiguration.IsMembership = False And clsDefaultConfiguration.customerwisepricemanagement = True Then '' MemberShip Discount Issue 
                    '  dr("TOTALDISCOUNT") = ((dr("SellingPrice") * (dr("TOTALDISCPERCENTAGE")) / 100)) * dr("Quantity")
                    '  dr("LINEDISCOUNT") = dr("TOTALDISCOUNT")
                    ' dr("TOTALDISCOUNT") = dr("TOTALDISCOUNT") * dr("Quantity")

                    If dr("PROMOTIONID") Is DBNull.Value Then
                        If dr("TOTALDISCPERCENTAGE") Is DBNull.Value Or dr("TOTALDISCPERCENTAGE") = 0 Then
                            dr("TOTALDISCOUNT") = dr("TOTALDISCOUNT") * dr("Quantity")
                        Else

                            dr("TOTALDISCOUNT") = ((dr("SellingPrice") * (dr("TOTALDISCPERCENTAGE")) / 100)) * dr("Quantity")
                        End If


                    Else
                        dr("TOTALDISCOUNT") = ((dr("SellingPrice") * (dr("TOTALDISCPERCENTAGE")) / 100)) * dr("Quantity")

                    End If
                End If

                ' Chenged by  Rahul katkar: used MYRound function to take only required decimals   

                'If flag <> 2 Then

                'Commented By Gaurav Danani
                'dr("NETAMOUNT") = dr("GrossAmt") - MyRound(CDbl(dr("TOTALDISCOUNT")), clsDefaultConfiguration.BillRoundOffAt, clsDefaultConfiguration.RoundOffRequired)
                dr("NETAMOUNT") = dr("GrossAmt") - dr("TOTALDISCOUNT")
                'Comment End

                'IsInclusiveTax = True
                'Dim dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, dr("ArticleCode"), "CMS", 1, dr("EAN"), clsDefaultConfiguration.CSTTaxCode, False)

                'If (dtTaxCalc IsNot Nothing AndAlso dtTaxCalc.Rows.Count > 0) Then
                '    IsInclusiveTax = IIf(dtTaxCalc.Rows(0)("INCLUSIVE") IsNot DBNull.Value, dtTaxCalc.Rows(0)("INCLUSIVE"), False)
                'End If




                '   Dim excTaxAmount = dtMainTax.Compute("SUM(TAXAMOUNT)", String.Format("Inclusive=0 AND BillLineNo={0}", dr("BillLineNo")))
                Dim excTaxAmount = Nothing
                If clsDefaultConfiguration.customerwisepricemanagement = True Then
                    ' If dr("PROMOTIONID") Is DBNull.Value Then

                    If Not IsDBNull(dtMainTax.Compute("SUM(VALUE)", String.Format("Inclusive=0 AND BillLineNo={0}", dr("BillLineNo")))) Then
                        excTaxAmount = (dtMainTax.Compute("SUM(VALUE)", String.Format("Inclusive=0 AND BillLineNo={0}", dr("BillLineNo"))) * dr("NETAMOUNT")) / 100
                    End If

                    'End If
                Else

                    If Not IsDBNull(dtMainTax.Compute("SUM(TAXAMOUNT)", String.Format("Inclusive=0 AND BillLineNo={0}", dr("BillLineNo")))) Then
                        excTaxAmount = dtMainTax.Compute("SUM(TAXAMOUNT)", String.Format("Inclusive=0 AND BillLineNo={0}", dr("BillLineNo")))
                    End If
                End If

                If Not IsDBNull(excTaxAmount) AndAlso Val(excTaxAmount) > 0 Then

                    dr("NETAMOUNT") = dr("NETAMOUNT") + Math.Round(Val(excTaxAmount.ToString()), clsDefaultConfiguration.DecimalPlaces)
                    ''Line Commented By ketan remove round Off, Tax Round off Issue  date-19-July-2016
                    'dr("EXCLUSIVETAX") = Math.Round(Val(excTaxAmount.ToString()), clsDefaultConfiguration.DecimalPlaces)
                    dr("EXCLUSIVETAX") = Val(excTaxAmount.ToString())

                    If clsDefaultConfiguration.customerwisepricemanagement = True Then
                        dr("TOTALTAXAMOUNT") = dr("EXCLUSIVETAX")
                    End If
                End If
                'If clsDefaultConfiguration.InclusiveTaxDisplay AndAlso Not IsDBNull(dr("TOTALTAXAMOUNT")) Then
                '    'dr("NETAMOUNT") = Math.Round(dr("NETAMOUNT") + dr("TOTALTAXAMOUNT"), clsDefaultConfiguration.DecimalPlaces)

                '    dr("NETAMOUNT") = dr("NETAMOUNT") - (dr("TOTALTAXAMOUNT") - Math.Round(Val(excTaxAmount.ToString()), clsDefaultConfiguration.DecimalPlaces))
                'End If
                If dr("NETAMOUNT") < 0 Then
                    dr("NETAMOUNT") = 0
                End If
                'Else
                'dr("NETAMOUNT") = newval

                'End If
                If Not IsDBNull(dr("TOTALDISCPERCENTAGE")) Then
                    dr("TOTALDISCPERCENTAGE") = Math.Round(dr("TOTALDISCPERCENTAGE"), clsDefaultConfiguration.DecimalPlaces)
                End If

                If Not dr("TOTALDISCOUNT") Is DBNull.Value AndAlso dr("TOTALDISCOUNT") <> 0 AndAlso dr("GrossAmt") <> 0 Then
                    'dr("TOTALDISCPERCENTAGE") = (IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT")) * 100) / dr("GrossAmt")
                    '  dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, Math.Round((dr("TOTALDISCOUNT") / dr("GrossAmt")) * 100))
                    'code added  by vipul for Customer wise price
                    If clsDefaultConfiguration.customerwisepricemanagement = True Then
                        dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, (dr("TOTALDISCOUNT") / dr("GrossAmt")) * 100)
                    Else
                        dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, Math.Round((dr("TOTALDISCOUNT") / dr("GrossAmt")) * 100))
                    End If
                End If

            Next
        Catch ex As Exception
            ShowMessage(getValueByKey("CM007"), "CM007 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Iteam Details calculation not done Properly", "Error")
        End Try
    End Sub
    ''' <summary>
    ''' Validating the flow Or Form Validations 
    ''' </summary>
    ''' <returns>True and False</returns>
    ''' <remarks></remarks>
    Private Function ValidateAll() As Boolean
        Try
            Dim TotalNetValue As Double = CashSummary.CtrllblNetAmt.Text
            Dim TotalCollection As Double = CDbl(If(dsMain.Tables("CASHMEMORECEIPT").Compute("Sum(AMOUNTTENDERED)", "").ToString() = "", 0.0, dsMain.Tables("CASHMEMORECEIPT").Compute("Sum(AMOUNTTENDERED)", "").ToString()))
            'changed by ram dt : 24.05.2009 action: changed
            'If TotalNetValue <> TotalCollection Then
            '    ShowMessage("Payment is not Settle", "Information")
            '    Return False
            'End If

            If TotalNetValue < 0 AndAlso clsDefaultConfiguration.IssueCreditVoucher = False Then

                ShowMessage(getValueByKey("CM042"), "CM042 - " & getValueByKey("CLAE04"))
                Return False
            End If

            If Math.Round(Math.Abs(TotalNetValue), 2) <> Math.Round(Math.Abs(TotalCollection), 2) Then
                ShowMessage(getValueByKey("CM043"), "CM043 - " & getValueByKey("CLAE04"))
                'ShowMessage("Payment is not Settle", "CM042")
                Return False
            End If
            'changed by ram dt : 24.05.2009 action: changed

            If Not clsAdmin.CVProgram = String.Empty Then
                If TotalNetValue < 0 AndAlso clsAdmin.CVProgram = String.Empty Then
                    ShowMessage(getValueByKey("CM056"), "CM056 - " & getValueByKey("CLAE05"))
                    Return False
                End If
            End If



            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    ''' <summary>
    ''' Clear the Data from DataSet
    ''' </summary>
    ''' <remarks>Clears the data from Screen also</remarks>
    ''' 
    Private Sub ClearData()
        Try
            IsRoundOffMsg = False
            For Each dt As DataTable In dsMain.Tables
                dt.Clear()
            Next
            If dsMain.Tables.Contains("CheckDtls") Then
                dsMain.Tables.Remove("CheckDtls")
            End If
            If Not dtGV Is Nothing Then dtGV.Clear()
            If Not dtMembData Is Nothing Then dtMembData.Clear()
            If Not dtMembDatapromo Is Nothing Then dtMembDatapromo.Clear()
            dtMainTax.Clear()
            'lblBalPoint.Text = String.Empty
            'lblCity.Text = String.Empty
            'cmdHold.Text = "Resume " & "Ctrl+H"
            cmdHold.Text = getValueByKey("frmcashmemo.cmdresume")
            cmdHold.Tag = "RESUME"
            CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
            CashSummary.CtrllblTaxAmt.Text = String.Empty
            CustInfo.CtrltxtCustomerName.Text = String.Empty
            CustInfo.CtrlTxtCustomerNo.Text = String.Empty
            CustInfo.CtrlTxtSwape.Text = String.Empty
            CustInfo.ctrlTxtPoints.Text = String.Empty
            CustInfo.CtrlLastVisit.Text = String.Empty
            CtrlSalesPersons.CtrlSalesPersons.SelectedIndex = -1
            HoldFlag = False
            lblCalTotalAmount.Text = 0.0
            lblCalTotalItem.Text = 0
            lblCalTotalItemQty.Text = 0
            PromotionCleared = False
            EditMode_IsupdateDeliveryPersonAllowed = False
            iscalledFromReturnClick = False
        Catch ex As Exception
            ShowMessage(getValueByKey("CM008"), "CM008 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in Clearing Data", "Error")
        End Try
    End Sub
    ''' <summary>
    ''' Preapring the data for save
    ''' </summary>
    ''' <param name="dsTemp">Copied dataSet</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrepareDataforSave(ByRef dsTemp As DataSet) As Boolean
        Try
            Dim dr As DataRow = dsTemp.Tables("CASHMEMOHdr").Rows(0)
            prepareHeader(dr)
            'If clsDefaultConfiguration.TaxDetailsRequired = True Then
            Dim dt As DataTable = dtMainTax.Copy()
            dsTemp.Tables.Add(dt)
            'End If
            Return True
        Catch ex As Exception
            ShowMessage(getValueByKey("CM009"), "CM009 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in Prepaering Data for save", "DataPrepare Error")
        End Try
    End Function
    ''' <summary>
    ''' Prepare the Header details for save
    ''' </summary>
    ''' <param name="dr">datarow</param>
    ''' <remarks></remarks>
    Private Sub prepareHeader(ByRef dr As DataRow)
        Try
            dr("SiteCode") = clsAdmin.SiteCode
            dr("TerminalID") = clsAdmin.TerminalID
            dr("TRANSACTIONCODE") = "CMS"
            dr("LINEITEMS") = dsMain.Tables("CASHMEMODtl").Rows.Count
            Dim strCostPrice As String = dsMain.Tables("CASHMEMODtl").Compute("sum(COSTPrice)", "").ToString()
            dr("COSTAMT") = CDbl(IIf(strCostPrice <> String.Empty, strCostPrice, 0))
            dr("TOTALITEMS") = IIf(dsMain.Tables("CASHMEMODtl").Compute("SUM(QUANTITY)", "") Is DBNull.Value, 0, dsMain.Tables("CASHMEMODtl").Compute("SUM(QUANTITY)", ""))
            dr("CREDITNOTENO") = 0
            dr("TRANSCURRENCY") = 0

            If CustInfo.CtrlTxtCustomerNo.Text <> String.Empty AndAlso customerType.Equals("CLP") Then
                dr("CLPSCHEME") = clsAdmin.CLPProgram
                dr("CLPPOINTS") = IIf(dsMain.Tables("CASHMEMODtl").Compute("SUM(CLPPOINTS)", "") Is DBNull.Value, 0, dsMain.Tables("CASHMEMODtl").Compute("SUM(CLPPOINTS)", ""))
                dr("CLPDISCOUNT") = IIf(dsMain.Tables("CASHMEMODtl").Compute("SUM(CLPDISCOUNT)", "") Is DBNull.Value, 0, dsMain.Tables("CASHMEMODtl").Compute("SUM(CLPDISCOUNT)", ""))
                dr("BALANCEPOINTS") = CheckIfBlank(CustInfo.ctrlTxtPoints.Text) + IIf(dr("CLPPOINTS") Is DBNull.Value, 0, dr("CLPPOINTS"))

            ElseIf customerType.Equals("SO") Then
                dr("CLPNo") = DBNull.Value
                dr("CustomerNo") = CustInfo.CtrlTxtCustomerNo.Text.Trim
            End If

            dr("EXCLUSIVETAX") = TotalExclusiveTax
            dr("TaxAmount") = TotalExclusiveTax

            dr("ROUNDEDAMT") = RoundedAmt
            If CtrlSalesPersons.CtrlSalesPersons.SelectedIndex > -1 Then
                dr("SALESEXECUTIVECODE") = CtrlSalesPersons.CtrlSalesPersons.SelectedValue
            End If
            dr("CREATEDAT") = clsAdmin.SiteCode
            dr("CREATEDBY") = clsAdmin.UserCode
            dr("Remark") = _remarks
        Catch ex As Exception
            ShowMessage(getValueByKey("CM009"), "CM009 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in Prepaering Data for save", "HeaderData Error")
        End Try
    End Sub
    ''' <summary>
    ''' Creating Line No in the Main and Subordinate table in Dataset
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="TableName">Name of the Table</param>
    ''' <remarks></remarks>
    Private Sub CreatingLineNO(ByRef ds As DataSet, ByVal TableName As String)
        Try
            Dim i As Integer
            Dim dt As DataTable = ds.Tables(TableName)
            For i = 0 To dt.Rows.Count - 1
                dt.Rows(i)("BillLineNo") = i + 1
                If ds.Tables.Contains("CASHMEMOTAXDTLS") = True Then
                    Dim j As Int16 = 1
                    For Each dr As DataRow In ds.Tables("CASHMEMOTAXDTLS").Select("EAN='" & dt.Rows(i)("EAN").ToString() & "'", "", DataViewRowState.CurrentRows)
                        dr("BillLineNo") = i + 1
                        dr("StepNo") = j
                        j = j + 1
                    Next
                End If

                If Not dtMainTax Is Nothing Then
                    Dim j As Int16 = 1
                    For Each dr As DataRow In dtMainTax.Select("EAN='" & dt.Rows(i)("EAN").ToString() & "'", "", DataViewRowState.CurrentRows)
                        dr("BillLineNo") = i + 1
                        dr("StepNo") = j
                        j = j + 1
                    Next
                End If
            Next

            'Set the Costprice of each line Item Except Return Item
            SetCostPrice(clsDefaultConfiguration.isMAPbasedCost, ds.Tables(TableName), clsAdmin.SiteCode, "costprice")
            'set the costprice of each line item except Return item

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Calculate Total bill and Show in Screen
    ''' </summary>
    ''' <remarks></remarks>
    Dim Creditsell As DataSet
    Private Sub calculateTotalbill(Optional ByVal IsReturnClick As Boolean = True)
        Try

            '--- Code Added By mahesh for Calculate Net Amount for return bill In case of Items are sale on Credit 
            'provison for showing credit sales of bill starts here
            Dim returnBillCreditSale As Double = 0
            If dtCreditSaleData IsNot Nothing Or dtCreditSaleData.Rows.Count > 0 Then
                returnBillCreditSale = SetBillWiseCreditSaleAmt()
                CashSummary.CtrlLabeltxt5.Text = FormatNumber(returnBillCreditSale, 2)
                If returnBillCreditSale = 0.0 Then
                    CashSummary.lbltxtVisible5 = False
                    CashSummary.lblVisible5 = False
                ElseIf returnBillCreditSale <> 0.0 Then
                    CashSummary.lbltxtVisible5 = True
                    CashSummary.lblVisible5 = True
                End If
            End If

            RemoveDeletedRow(dsMain.Tables("CASHMEMODtl"))           ' 'Code Added by irfan on 17/1/2018 For Mantis issue 2756 CashMemoReturn

            'provison for showing credit sales of bill ends here
            Dim strSumNetAmount As String = dsMain.Tables("CASHMEMODtl").Compute("SUM(NETAMOUNT)", "").ToString()
            CashSummary.CtrllblNetAmt.Text = FormatNumber(CDbl(IIf(strSumNetAmount <> "", strSumNetAmount, 0)), 2)

            'Code Added by irfan on 17/1/2018 For Mantis issue 2756 CashMemoReturn===========================================================================
            If dt.Rows.Count > 0 Then
                Dim OLDQTY As String = dt.Rows(0)("QUANTITY")
                Dim ORIGINALQTY As String = IIf(dt.Rows(0)("ORIGINALQTY") Is DBNull.Value, 0, dt.Rows(0)("ORIGINALQTY"))
                If OLDQTY.Contains("-") Then
                    OLDQTY = OLDQTY.Remove(0, 1)
                End If
                If OLDQTY <> ORIGINALQTY AndAlso ORIGINALQTY <> 0 Then
                    CashSummary.lbltxtVisible5 = False
                    CashSummary.lblVisible5 = False
                End If
            End If



            
            If dtCreditSaleData.Rows.Count > 0 Then
                isApplicableforcashReturn = objCM.isApplicableforcashReturn(dtCreditSaleData.Rows(0)("BILLNO"))
                retuntAmt = objCM.ReturnAmount(dtCreditSaleData)
            End If

            Dim No As Integer
            Dim netmat As Double = CashSummary.CtrllblNetAmt.Text
            Dim ind As Integer
            For ind = 0 To dsMain.Tables("CASHMEMODtl").Rows.Count - 1
                If dsMain.Tables("CASHMEMODtl").Rows(ind)("BType") = "S" Then
                    If CashSummary.lbltxtVisible5 = True Then
                        If Not String.IsNullOrEmpty(strSumNetAmount) Then

                            Dim i As Integer
                            Creditsell = dsMain.Copy
                            For i = 0 To Creditsell.Tables("CASHMEMODtl").Rows.Count - 1
                                If Not Creditsell.Tables("CASHMEMODtl").Rows(0)("BillNo") Is DBNull.Value Then
                                    If dtCreditSaleData.Rows(0)("BillNo") = Creditsell.Tables("CASHMEMODtl").Rows(0)("BillNo") Then
                                        Creditsell.Tables("CASHMEMODtl").Rows.RemoveAt(0)
                                    End If
                                End If
                            Next
                            If isApplicableforcashReturn = True Then
                                Dim strSumNet As String = Creditsell.Tables("CASHMEMODtl").Compute("SUM(NETAMOUNT)", "").ToString()
                                CashSummary.CtrllblNetAmt.Text = FormatNumber(CDbl(IIf(strSumNet <> "", strSumNet, 0)), 2)
                                Dim paidAmount As Double = CashSummary.CtrllblNetAmt.Text
                                Dim retAmt As Double = retuntAmt.Rows(0)("AmountTendered")
                                CashSummary.CtrllblNetAmt.Text = paidAmount - retAmt
                                'For No = 0 To dsMain.Tables("CASHMEMODtl").Rows.Count - 1
                                '    If dsMain.Tables("CASHMEMODtl").Rows(No)("BTYPE") <> "R" Then
                                '        dsMain.Tables("CASHMEMODtl").Rows(No)("NETAMOUNT") = Creditsell.Tables("CASHMEMODtl").Compute("SUM(NETAMOUNT)", "BTYPE<> 'R'") - retAmt

                                '    End If
                                'Next
                            Else
                                Dim strSumNet As String = Creditsell.Tables("CASHMEMODtl").Compute("SUM(NETAMOUNT)", "").ToString()
                                CashSummary.CtrllblNetAmt.Text = FormatNumber(CDbl(IIf(strSumNet <> "", strSumNet, 0)), 2)
                            End If

                        End If

                    End If
                End If

            Next
            '=========================================================================================================================================================

            'CashSummary.CtrllblNetAmt.Text = Math.Round(CDbl(CashSummary.CtrllblNetAmt.Text))
            'lblTotalQty.Text = Format(CDbl(dsMain.Tables("CASHMEMODtl").Compute("SUM(QUANTITY)", "").ToString()), "0.00")
            'CashSummary.CtrllblDiscAmt.Text = Format(CDbl(dsMain.Tables("CASHMEMODtl").Compute("SUM(TOTALDISCOUNT)", "TOTALDISCOUNT >0").ToString()), "0.00")
            'Dim ObjclsCommon As New clsCommon
            'Dim offerno As String = ObjclsCommon.GetOffernoForRoundOff()
            'If dsMain.Tables("CASHMEMODtl").Rows.Count > 0 And dsMain.Tables("CASHMEMODtl").Select("", "", DataViewRowState.CurrentRows).Count = 1 Then
            '    If IsRoundOffMsg Or (dsMain.Tables("CASHMEMODtl").Rows(0)("PromotionId").ToString().Contains(offerno)) Then
            If IsRoundOffMsg Then
                CashSummary.CtrlLabel3.Text = "Roundoff Amt."
                CashSummary.CtrlLabel3.Tag = "Roundoff Amt."
                'IsRoundOffMsg = False
            Else
                CashSummary.CtrlLabel3.Text = "Discount Amt."
                CashSummary.CtrlLabel3.Tag = "Discount Amt."
            End If

            'Dim strSumTotalDiscount As String = dsMain.Tables("CASHMEMODtl").Compute("SUM(LINEDISCOUNT)", "").ToString()
            Dim strSumTotalDiscount As String
            If clsDefaultConfiguration.IsMembership Then '' MemberShip Discount Issue 
                strSumTotalDiscount = dsMain.Tables("CASHMEMODtl").Compute("SUM(TotalDiscount)", "").ToString()
            Else
                '  strSumTotalDiscount = dsMain.Tables("CASHMEMODtl").Compute("SUM(LINEDISCOUNT)", "").ToString()
                'code added  by vipul for Customer wise price
                If clsDefaultConfiguration.customerwisepricemanagement = True Then
                    CashSummary.CtrllblDiscAmt.Text = Format(Val(dsMain.Tables("CASHMEMODtl").Compute("SUM(TotalDiscount)", "").ToString()), "0.00")
                Else
                    CashSummary.CtrllblDiscAmt.Text = Format(Val(dsMain.Tables("CASHMEMODtl").Compute("SUM(LINEDISCOUNT)", "").ToString()), "0.00")
                End If
                ''Code Added by irfan on 17/1/2018 For Mantis issue 2756 CashMemoReturn===========================================================================
                For ind = 0 To dsMain.Tables("CASHMEMODtl").Rows.Count - 1
                    If dsMain.Tables("CASHMEMODtl").Rows(ind)("BType") = "S" Then
                        If CashSummary.lbltxtVisible5 = True Then
                            If Not String.IsNullOrEmpty(strSumNetAmount) Then
                                strSumTotalDiscount = Creditsell.Tables("CASHMEMODtl").Compute("SUM(LINEDISCOUNT)", "").ToString()
                                CashSummary.CtrllblDiscAmt.Text = FormatNumber(CDbl(IIf(strSumTotalDiscount <> "", strSumTotalDiscount, 0)), 2)
                            End If
                        End If
                    End If
                Next
                '========================================================================================================================================
            End If
            ' CashSummary.CtrllblDiscAmt.Text = FormatNumber(CDbl(IIf(strSumTotalDiscount <> "", strSumTotalDiscount, 0)), 2)

            Dim strSumGrossAmt As String = dsMain.Tables("CASHMEMODtl").Compute("SUM(GROSSAMT)", "").ToString()
            CashSummary.CtrllblGrossAmt.Text = FormatNumber(CDbl(IIf(strSumGrossAmt <> "", strSumGrossAmt, 0)), 2)
            ''Code Added by irfan on 17/1/2018 For Mantis issue 2756 CashMemoReturn===========================================================================
            Dim GrsTax As Double = CashSummary.CtrllblGrossAmt.Text
            For ind = 0 To dsMain.Tables("CASHMEMODtl").Rows.Count - 1
                If dsMain.Tables("CASHMEMODtl").Rows(ind)("BType") = "S" Then
                    If CashSummary.lbltxtVisible5 = True Then
                        If Not String.IsNullOrEmpty(strSumNetAmount) Then
                            Dim strSumNet As String = Creditsell.Tables("CASHMEMODtl").Compute("SUM(GROSSAMT)", "").ToString()
                            CashSummary.CtrllblGrossAmt.Text = FormatNumber(CDbl(IIf(strSumNet <> "", strSumNet, 0)), 2)
                        End If
                    End If
                End If
            Next
            '==============================================================================================================================================
            Dim PosTotal, NegTotal As Double
            Dim strPosTotal, strNegTotal As String
            strPosTotal = dsMain.Tables("CASHMEMODtl").Compute("SUM(NETAMOUNT)", "NETAMOUNT > 0").ToString()
            strNegTotal = dsMain.Tables("CASHMEMODtl").Compute("SUM(NETAMOUNT)", "NETAMOUNT < 0").ToString()
            PosTotal = CDbl(IIf(strPosTotal <> "", strPosTotal, 0))
            NegTotal = CDbl(IIf(strNegTotal <> "", strNegTotal, 0))

            NegTotal = NegTotal * -1
            Dim PosTax, NegTax As Double
            PosTax = IIf(PosTotal > 0, GetBillLabelTax(PosTotal), 0)
            NegTax = IIf(NegTotal > 0, GetBillLabelTax(NegTotal), 0)
            Dim tax As Double = PosTax - NegTax
            If tax <> 0 Then
                CashSummary.CtrllblNetAmt.Text = FormatNumber(CDbl(CashSummary.CtrllblNetAmt.Text) + tax, 2).ToString
            End If

            '--- Calculate net Amount with returnBillCreditSale
            'If returnBillCreditSale > 0 Then
            '    CashSummary.CtrllblNetAmt.Text = FormatNumber(CDbl(CashSummary.CtrllblNetAmt.Text) + returnBillCreditSale, 2).ToString
            'End If

            Dim ItemTax As Double = IIf(dsMain.Tables("CASHMEMODtl").Compute("SUM(EXCLUSIVETAX)", "") Is DBNull.Value, 0.0, dsMain.Tables("CASHMEMODtl").Compute("SUM(EXCLUSIVETAX)", ""))
            If ItemTax > 0 Then
                TotalExclusiveTax = ItemTax + tax
            Else
                TotalExclusiveTax = tax
            End If

            Dim TotalItemTax As Double = IIf(dsMain.Tables("CASHMEMODtl").Compute("SUM(TOTALTAXAMOUNT)", "") Is DBNull.Value, 0.0, dsMain.Tables("CASHMEMODtl").Compute("SUM(TOTALTAXAMOUNT)", ""))
            ''Code Added by irfan on 17/1/2018 For Mantis issue 2756 CashMemoReturn===========================================================================
            Dim ITotalTax As Double = TotalItemTax
            For ind = 0 To dsMain.Tables("CASHMEMODtl").Rows.Count - 1
                If dsMain.Tables("CASHMEMODtl").Rows(ind)("BType") = "S" Then
                    If CashSummary.lbltxtVisible5 = True Then
                        If Not String.IsNullOrEmpty(strSumNetAmount) Then
                            TotalItemTax = IIf(Creditsell.Tables("CASHMEMODtl").Compute("SUM(EXCLUSIVETAX)", "") Is DBNull.Value, 0.0, Creditsell.Tables("CASHMEMODtl").Compute("SUM(EXCLUSIVETAX)", ""))
                            ITotalTax = ItemTax
                        End If

                    End If
                End If
            Next
            '================================================================================================================================
            Dim TotalTax As Double = TotalItemTax + tax
            '--changed for bug no 533 totaltaxamount in exchanege of exclusive tax 
            'CashSummary.CtrllblTaxAmt.Text = Format(Math.Round(TotalExclusiveTax, 2), "0.00")

            CashSummary.CtrllblTaxAmt.Text = FormatNumber(TotalTax, 2).ToString
            Dim NetAmt As Double = CDbl(IIf(CashSummary.CtrllblNetAmt.Text <> "", CashSummary.CtrllblNetAmt.Text, 0))
            If clsDefaultConfiguration.RoundOffRequired = True Then

                CashSummary.CtrllblNetAmt.Text = FormatNumber(MyRound(CDbl(CashSummary.CtrllblNetAmt.Text), clsDefaultConfiguration.BillRoundOffAt), 2).ToString
                RoundedAmt = NetAmt - CDbl(CashSummary.CtrllblTaxAmt.Text)
            Else
                RoundedAmt = 0
            End If

            lblCalTotalItem.Text = dsMain.Tables("CASHMEMODtl").Rows.Count

            Dim strSumPositiveQuantity, strSumNegativeQuantity As String
            strSumPositiveQuantity = dsMain.Tables("CASHMEMODtl").Compute("SUM(QUANTITY)", "QUANTITY>0").ToString()
            strSumNegativeQuantity = dsMain.Tables("CASHMEMODtl").Compute("SUM(QUANTITY)", "QUANTITY <0").ToString()

            lblCalTotalItemQty.Text = CDbl(IIf(strSumPositiveQuantity <> "", strSumPositiveQuantity, 0))
            lblCalTotalItemQty.Text = CDbl(IIf(lblCalTotalItemQty.Text <> "", lblCalTotalItemQty.Text, 0)) + CDbl(IIf(strSumNegativeQuantity <> "", strSumNegativeQuantity, 0)) * -1
            'lblCalTotalItemQty.Text = FormatNumber(CDbl(lblCalTotalItemQty.Text.ToString()), 2)

            If (clsDefaultConfiguration.AllowDecimalQty) Then
                If clsDefaultConfiguration.WeightScaleEnabled Then
                    lblCalTotalItemQty.Text = Format(Val(lblCalTotalItemQty.Text.ToString()), "0.000")
                Else
                    lblCalTotalItemQty.Text = Format(Val(lblCalTotalItemQty.Text.ToString()), "0.00")
                End If
            Else
                lblCalTotalItemQty.Text = Format(Val(lblCalTotalItemQty.Text.ToString()), "0")
            End If

            lblCalTotalAmount.Text = CashSummary.CtrllblNetAmt.Text

            If CDbl(IIf(CashSummary.CtrllblNetAmt.Text.ToString() <> String.Empty, CashSummary.CtrllblNetAmt.Text.ToString(), 0)) <= 0 Then
                SetButtons(2, False)
            Else
                SetButtons(2, True)
            End If

            'CtrlSalesPersons.CtrlTxtBox.Select()
            CashSummary.CtrllblNetAmt.TextAlign = ContentAlignment.MiddleRight
            CashSummary.CtrllblTaxAmt.TextAlign = ContentAlignment.MiddleRight
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                CashSummary.CtrlLabel3.Text = CashSummary.CtrlLabel3.Text.ToUpper
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM010"), "CM010 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in Calculating total bill", "Error")
        End Try
    End Sub
    ''' <summary>
    ''' Change the Table Columns name For payment module 
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub UpdatePaymentPrevStru(ByRef dt As DataTable)
        Try
            dt.TableName = "MstRecieptType"
            dt.Columns("CMRECPTLINENO").ColumnName = "SRNO"
            dt.Columns("TENDERTYPECODE").ColumnName = "RECIEPTTYPECODE"
            dt.Columns("RECIEPTTYPE").ColumnName = "RECIEPT"
            dt.Columns("TENDERHEADCODE").ColumnName = "RECIEPTTYPE"
            dt.Columns("AMOUNTTENDERED").ColumnName = "AMOUNT"
            dt.Columns("CARDNO").ColumnName = "NUMBER"
            dt.Columns("REFDATE").ColumnName = "DATE"
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Updating Payment Structure's 
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="UpdateFlag"></param>
    ''' <remarks></remarks>
    Private Sub UpdatePaymentDataSetStru(ByRef ds As DataSet, ByVal UpdateFlag As Boolean)
        Try
            ds.Tables(0).TableName = "CASHMEMORECEIPT"
            ds.Tables(0).Columns("SRNO").ColumnName = "CMRECPTLINENO"
            ds.Tables(0).Columns("RECIEPTTYPE").ColumnName = "TENDERHEADCODE"
            ds.Tables(0).Columns("RECIEPTTYPECODE").ColumnName = "TENDERTYPECODE"
            ds.Tables(0).Columns("RECIEPT").ColumnName = "RECIEPTTYPE"
            ds.Tables(0).Columns("AMOUNT").ColumnName = "AMOUNTTENDERED"
            ds.Tables(0).Columns("CURRENCYCODE").ColumnName = "CURRENCYCODE"
            ds.Tables(0).Columns("AMOUNTINCURRENCY").ColumnName = "AMOUNTINCURRENCY"
            ds.Tables(0).Columns("EXCHANGERATE").ColumnName = "EXCHANGERATE"
            ds.Tables(0).Columns("NUMBER").ColumnName = "CARDNO"
            ds.Tables(0).Columns("DATE").ColumnName = "REFDATE"
            If UpdateFlag = True Then
                For Each dr As DataRow In ds.Tables(0).Rows
                    If dr.RowState = DataRowState.Added Then
                        dr("Sitecode") = dsMain.Tables("CashMemoHdr").Rows(0)("Sitecode")
                        dr("Billno") = dsMain.Tables("CashMemoHdr").Rows(0)("billno")
                        dr("TerminalId") = clsAdmin.TerminalID   'dsMain.Tables("CashMemoHdr").Rows(0)("Sitecode")
                        dr("CMRCPTDATE") = Now
                        dr("CMRCPTTIME") = Now
                    End If
                Next
            Else
                Dim dv As New DataView(ds.Tables(0), "TenderTypeCode='CASH'", "", DataViewRowState.CurrentRows)
                If dv.Count > 0 Then
                    dv.AllowEdit = True
                    For Each dr As DataRowView In dv
                        dr("CARDNO") = Nothing
                    Next
                End If
                ds.Tables(0).AcceptChanges()
            End If
            dsMain.Tables("CASHMEMORECEIPT").Clear()
            dsMain.Tables("CASHMEMORECEIPT").Merge(ds.Tables("CASHMEMORECEIPT"), False, MissingSchemaAction.Ignore)
            For Each dr As DataRow In dsMain.Tables("CASHMEMORECEIPT").Rows
                If dr.RowState <> DataRowState.Deleted Then
                    If clsAdmin.CurrencyCode.ToUpper() = dr("CURRENCYCODE").ToString().ToUpper() Then
                        dr("AMOUNTRECEIVED") = dr("CURRENCYCODE").ToString() & " " & dr("AMOUNTTENDERED")
                    ElseIf dr("CURRENCYCODE").ToString() <> "" AndAlso clsAdmin.CurrencyCode.ToUpper() <> dr("CURRENCYCODE").ToString().ToUpper() Then
                        dr("AMOUNTRECEIVED") = dr("CURRENCYCODE").ToString() & " " & dr("AMOUNTINCURRENCY")
                    Else
                        dr("AMOUNTRECEIVED") = dr("AMOUNTTENDERED")
                    End If
                End If
            Next

            'Added by Rohit for CR-5938

            'If dsMain.Tables.Contains("CheckDtls") Then
            '    dsMain.Tables.Remove("CheckDtls")
            'End If
            If ds.Tables.Contains("CheckDtls") Then
                Dim dtCheckDtls As New DataTable
                dtCheckDtls = ds.Tables("CheckDtls").Copy
                dtCheckDtls.TableName = "CheckDtls"
                dtCheckDtls.AcceptChanges()
                If Not dsMain.Tables.Contains("CheckDtls") Then
                    dsMain.Tables.Add(dtCheckDtls)
                Else
                    dsMain.Tables("CheckDtls").Merge(dtCheckDtls)
                End If

            End If

            PaymentGridSetting()
        Catch ex As Exception
            ShowMessage(getValueByKey("CM011"), "CM011 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in updating the payment data Structure", "Error")
        End Try
    End Sub
    Private Sub UpdateSaleOrderTermNConditionsStru(ByRef ds As DataSet, ByVal UpdateFlag As Boolean)
        Try
            Dim objcomm As New clsCommon
            Dim vcrntdate = objcomm.GetCurrentDate()
            For Each drMStRec As DataRow In ds.Tables(0).Select("TENDERTYPECODE = 'Credit'")
                Dim drTermscod As DataRow = dsMain.Tables("SaleOrderTermNConditions").NewRow

                drTermscod("SiteCode") = clsAdmin.SiteCode
                drTermscod("FinYear") = clsAdmin.Financialyear
                drTermscod("SaleOrderNumber") = lblCMNo.Text
                drTermscod("TnCcode") = _paymentTermId
                drTermscod("SrNo") = 1
                drTermscod("CREATEDAT") = clsAdmin.SiteCode
                drTermscod("CREATEDBY") = clsAdmin.UserCode
                drTermscod("CREATEDON") = vcrntdate
                drTermscod("UPDATEDAT") = clsAdmin.SiteCode
                drTermscod("UPDATEDBY") = clsAdmin.UserCode
                drTermscod("UPDATEDON") = vcrntdate
                drTermscod("STATUS") = True
                drTermscod("RefInvNumber") = lblCMNo.Text

                dsMain.Tables("SaleOrderTermNConditions").Rows.Add(drTermscod)

            Next


        Catch ex As Exception
            ShowMessage(getValueByKey("CM011"), "CM011 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in updating the payment data Structure", "Error")
        End Try
    End Sub
    ''' <summary>
    ''' Set the Payment Grid Layout
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PaymentGridSetting()
        Try
            Payment.CtrlListPayment.Columns("RECIEPTTYPE").Caption = getValueByKey("frmnbirthlistsales.ctrlpayment1.ctrllistpayment.payment mode")
            Payment.CtrlListPayment.Columns("AMOUNTRECEIVED").Caption = getValueByKey("frmnbirthlistsales.ctrlpayment1.ctrllistpayment.amount")
            For Each r As C1.Win.C1List.Split In Payment.CtrlListPayment.Splits
                Dim i As Integer
                For i = 0 To r.DisplayColumns.Count - 1
                    If r.DisplayColumns(i).DataColumn.DataField.ToUpper() <> "AMOUNTRECEIVED".ToUpper() And r.DisplayColumns(i).DataColumn.DataField.ToUpper() <> "RECIEPTTYPE".ToUpper() Then
                        r.DisplayColumns(i).Visible = False
                    End If
                Next
            Next
            Payment.CtrlListPayment.ExtendRightColumn = True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Get previous Cash Memo Details
    ''' </summary>
    ''' <param name="strCashMemo">Cash Memo No</param>
    ''' <param name="strSiteCode">Site Code</param>
    ''' <remarks></remarks>
    Private Sub GetCashMemoDetails(ByVal strCashMemo As String, ByVal strSiteCode As String)
        Try
            Dim dsTemp As DataSet
            dsTemp = objCM.GetStruc(strCashMemo, strSiteCode)
            dsMain.Clear()
            If Not dsTemp Is Nothing AndAlso dsTemp.Tables.Count > 0 Then
                dsMain.Tables("CASHMEMOHDR").Merge(dsTemp.Tables(0), False, MissingSchemaAction.Ignore)
                dsMain.Tables("CASHMEMODTL").Merge(dsTemp.Tables(1), False, MissingSchemaAction.Ignore)
                dsMain.Tables("CASHMEMORECEIPT").Merge(dsTemp.Tables(2), False, MissingSchemaAction.Ignore)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Show the Discount Amount on Mouse Move
    ''' </summary>
    ''' <param name="row"></param>
    ''' <param name="col"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CalculateDiscountAmount(ByVal row As Int32, ByVal col As Int32) As Double
        Try
            Dim DiscountAmt As Double
            DiscountAmt = dgMainGrid.Rows(row)("TotalDiscount")
            Return FormatNumber(DiscountAmt, 2)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Return 0
        End Try
    End Function
    ''' <summary>
    ''' Assign Tax to the Row
    ''' </summary>
    ''' <param name="strMatcode">Article Code</param>
    ''' <param name="TaxableAmount">Amount on which Tax calulated </param>
    ''' <param name="iRow"> Row No</param>
    ''' <param name="EAN">Ean Code</param>
    ''' <returns>Tax amount</returns>
    ''' <remarks></remarks>

    Private Function CreateDataSetForTaxCalculation(ByRef originalTaxAmt As Double, ByVal LineNo As String, ByVal strMatcode As String, ByVal TaxableAmount As Double, ByVal iRow As Integer, ByVal ItemQty As Double, Optional ByVal EAN As String = "") As Object
        Try
            Dim StrTaxCode As Double = 0
            Dim dtTaxCalc As DataTable
            dtTaxCalc = New DataTable
            Dim dvTax As New DataView(dtMainTax, "BillLineNo='" & LineNo & "' AND ArticleCode='" & strMatcode & "'", "StepNo", DataViewRowState.CurrentRows)

            If dvTax.Count > 0 Then
                dtTaxCalc = dvTax.ToTable()
                dvTax.AllowDelete = True
                Dim i As Integer
                For i = dvTax.Count - 1 To 0 Step -1
                    dvTax.Delete(i)
                Next
            Else
                If IsCSTApplicable Then
                    ''---added by ketan artical tax mapped based on Order type
                    If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
                        If CustomerSaleType = 1 Then 'Dine_In = 1
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "DIN", ItemQty, EAN, clsDefaultConfiguration.CSTTaxCode, True)
                        ElseIf CustomerSaleType = 2 Then  ' Home_Delivery = 2
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "HOD", ItemQty, EAN, clsDefaultConfiguration.CSTTaxCode, True)
                        ElseIf CustomerSaleType = 3 Then 'Take_Away = 3
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "TAK", ItemQty, EAN, clsDefaultConfiguration.CSTTaxCode, True)
                        Else
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", ItemQty, EAN, clsDefaultConfiguration.CSTTaxCode, True)
                        End If
                    Else
                        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", ItemQty, EAN, clsDefaultConfiguration.CSTTaxCode, True)
                    End If
                Else
                    If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
                        If CustomerSaleType = 1 Then 'Dine_In = 1
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "DIN", ItemQty, EAN, clsDefaultConfiguration.CSTTaxCode, False)
                        ElseIf CustomerSaleType = 2 Then  ' Home_Delivery = 2
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "HOD", ItemQty, EAN, clsDefaultConfiguration.CSTTaxCode, False)
                        ElseIf CustomerSaleType = 3 Then 'Take_Away = 3
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "TAK", ItemQty, EAN, clsDefaultConfiguration.CSTTaxCode, False)
                        Else
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", ItemQty, EAN, clsDefaultConfiguration.CSTTaxCode, False)
                        End If
                    Else
                        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", ItemQty, EAN, clsDefaultConfiguration.CSTTaxCode, False)
                    End If
                End If
            End If

            Dim dbIncTotalTax As Decimal = 0
            Dim dbExclTotalTax As Decimal = 0
            If dtTaxCalc.Rows.Count = 0 Then
                Return Nothing
            End If
            If dtTaxCalc.Rows.Count <> 0 Then
                If IsCSTApplicable Then
                    Dim amt As Double = GetTaxableAmountForCst(strMatcode, EAN, ItemQty, TaxableAmount)
                    originalTaxAmt = amt
                    dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount - amt
                    objCM.getCalculatedDataSet(dtTaxCalc)
                Else
                    dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount

                    objCM.getCalculatedDataSet(dtTaxCalc)

                    Dim taxAmount = dtTaxCalc.Compute("SUM(TAXAMOUNT)", "Inclusive=1")
                    originalTaxAmt = Val(taxAmount.ToString())
                End If
                Dim iRowTax As Int16
                With dtTaxCalc
                    For iRowTax = 0 To dtTaxCalc.Rows.Count - 1
                        dtTaxCalc.Rows(iRowTax)("BillLineNo") = LineNo
                        If Val(dtTaxCalc.Rows(iRowTax)("VALUE").ToString) <> 0 Then
                            If dtTaxCalc.Rows(iRowTax)("INCLUSIVE") = "0" Then
                                dbExclTotalTax = dbExclTotalTax + Format(Val(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT")), "0.00")
                            Else
                                dbIncTotalTax = dbIncTotalTax + Format(Val(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT")), "0.00")
                            End If
                        End If
                    Next
                End With
                StrTaxCode = dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")

                If objArticleCombo.CheckIfComboArticle(strMatcode) Then
                    filterScanArticle = String.Format("Btype='S' AND EAN='{0}' AND SellingPrice = '{1}'", EAN.Trim(), TaxableAmount)
                Else
                    filterScanArticle = String.Format("Btype='S' AND EAN='{0}'", EAN.Trim())
                End If

                For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Select(filterScanArticle, "", DataViewRowState.CurrentRows)
                    dr("EXCLUSIVETAX") = dbExclTotalTax
                    'dr("TOTALTAXAMOUNT") = Format(StrTaxCode, "0.00")
                    dr("TOTALTAXAMOUNT") = StrTaxCode
                Next

                'dsMain.Tables("CASHMEMODTL").Rows(iRow - 1)("EXCLUSIVETAX") = dbExclTotalTax

                dtMainTax.Merge(dtTaxCalc, False, MissingSchemaAction.Ignore)
                dtMainTax.AcceptChanges()
                Return StrTaxCode
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM012"), "CM012 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in updating Tax Calculation", "Error")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Assign Tax to the Row
    ''' </summary>
    ''' <param name="strMatcode">Article Code</param>
    ''' <param name="TaxableAmount">Amount on which Tax calulated </param>
    ''' <param name="dr"> Data Row</param>
    ''' <param name="EAN">Ean Code</param>
    ''' <returns>Tax amount</returns>
    ''' <remarks></remarks>
    Private Function CreateDataSetForTaxCalculation(ByRef originalTaxAmt As Double, ByVal LineNo As String, ByVal strMatcode As String, ByVal TaxableAmount As Double, ByRef dr As DataRow, Optional ByVal EAN As String = "") As Object
        Try
            Dim StrTaxCode As Double = 0
            Dim dtTaxCalc As DataTable
            dtTaxCalc = New DataTable
            Dim ExlusiveFound As Boolean = False
            Dim dvTax As New DataView(dtMainTax, "BillLineNo='" & LineNo & "' AND ArticleCode='" & strMatcode & "'", "StepNo", DataViewRowState.CurrentRows)
            If dvTax.Count > 0 Then
                dtTaxCalc = dvTax.ToTable()
                dvTax.AllowDelete = True
                Dim i As Integer
                For i = dvTax.Count - 1 To 0 Step -1
                    dvTax.Delete(i)
                Next
            Else
                If IsCSTApplicable Then
                    '    'dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, True)
                    '    dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, "", "CMS", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, True)
                    'Else
                    '    dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, False)
                    ''---added by ketan artical tax mapped based on Order type
                    If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
                        If CustomerSaleType = 1 Then 'Dine_In = 1
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "DIN", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, True)
                        ElseIf CustomerSaleType = 2 Then  ' Home_Delivery = 2
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "HOD", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, True)
                        ElseIf CustomerSaleType = 3 Then 'Take_Away = 3
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "TAK", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, True)
                        Else
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, True)
                        End If
                    Else
                        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, True)
                    End If
                Else

                    If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
                        If CustomerSaleType = 1 Then 'Dine_In = 1
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "DIN", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, False)
                        ElseIf CustomerSaleType = 2 Then  ' Home_Delivery = 2
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "HOD", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, False)
                        ElseIf CustomerSaleType = 3 Then 'Take_Away = 3
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "TAK", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, False)
                        Else
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, False)
                        End If
                    Else
                        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, False)
                    End If
                End If
            End If
            Dim dbIncTotalTax As Double = 0
            Dim dbExclTotalTax As Double = 0
            If dtTaxCalc.Rows.Count = 0 Then
                Return Nothing
            End If
            If dtTaxCalc.Rows.Count <> 0 Then
                If IsCSTApplicable Then
                    '------- Commented and changed by Mahesh I am sending Taxable Amt in Advance not need to calculate again n again 
                    'Dim amt As Double = GetTaxableAmountForCst(strMatcode, EAN, dr("Quantity"), TaxableAmount)
                    'originalTaxAmt = amt
                    'dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount - amt
                    dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
                    objCM.getCalculatedDataSet(dtTaxCalc)
                Else
                    dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
                    '----- Code Added By Mahesh Now in case of tax value type  need to calculate for Qty
                    dtTaxCalc.Rows(0)("ItemQty") = dr("Quantity")
                    objCM.getCalculatedDataSet(dtTaxCalc)

                    Dim taxAmount = dtTaxCalc.Compute("SUM(TAXAMOUNT)", "Inclusive=1")
                    originalTaxAmt = Val(taxAmount.ToString())
                End If

                Dim iRowTax As Int16
                With dtTaxCalc
                    For iRowTax = 0 To dtTaxCalc.Rows.Count - 1
                        dtTaxCalc.Rows(iRowTax)("BillLineNo") = LineNo
                        If Val(dtTaxCalc.Rows(iRowTax)("VALUE").ToString) <> 0 Then
                            If dtTaxCalc.Rows(iRowTax)("INCLUSIVE") = "0" Then
                                dbExclTotalTax = dbExclTotalTax + Val(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT"))
                            Else
                                dbIncTotalTax = dbIncTotalTax + Val(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT"))
                            End If
                        End If
                    Next
                End With
                dr("EXCLUSIVETAX") = dbExclTotalTax
                StrTaxCode = dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")
                dr("TOTALTAXAMOUNT") = StrTaxCode
                dtMainTax.Merge(dtTaxCalc, False, MissingSchemaAction.Ignore)
                dtMainTax.AcceptChanges()
                Return StrTaxCode
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM012"), "CM012 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in updating Tax Calculation", "Error")
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Get Bill Lavel Tax
    ''' </summary>
    ''' <param name="TaxableAmount">Taxable Amount</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetBillLabelTax(ByVal TaxableAmount As Double) As Double
        Try
            Dim StrTaxCode As Double = 0
            Dim dtTaxCalc As DataTable
            dtTaxCalc = New DataTable
            If dtMainTax.Rows.Count > 0 Then
                Dim dvTax As New DataView(dtMainTax, "ArticleCode =''", "StepNo", DataViewRowState.CurrentRows)
                If dvTax.Count > 0 Then
                    dtTaxCalc = dvTax.ToTable()
                    dvTax.AllowDelete = True
                    Dim i As Integer
                    For i = dvTax.Count - 1 To 0 Step -1
                        dvTax.Delete(i)
                    Next
                Else
                    ''added by ketan Tax Changes
                    ' dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, "", "CMS", 0)
                    If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
                        If CustomerSaleType = 1 Then 'Dine_In = 1
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, "", "DIN", 0)
                        ElseIf CustomerSaleType = 2 Then  ' Home_Delivery = 2
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, "", "HOD", 0)
                        ElseIf CustomerSaleType = 3 Then 'Take_Away = 3
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, "", "TAK", 0)
                        Else
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, "", "CMS", 0)
                        End If
                    Else
                        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, "", "CMS", 0)
                    End If
                End If
            End If

            Dim dbIncTotalTax As Decimal = 0
            Dim dbExclTotalTax As Decimal = 0
            If dtTaxCalc.Rows.Count = 0 Then
                Return Nothing
            End If
            If dtTaxCalc.Rows.Count <> 0 Then
                dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
                objCM.getCalculatedDataSet(dtTaxCalc)
                Dim iRowTax As Int16
                With dtTaxCalc
                    For iRowTax = 0 To dtTaxCalc.Rows.Count - 1
                        If CDbl(dtTaxCalc.Rows(iRowTax)("VALUE").ToString) <> 0 Then
                            If dtTaxCalc.Rows(iRowTax)("INCLUSIVE") = False Then
                                dbExclTotalTax = dbExclTotalTax + IIf(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT") Is DBNull.Value, 0, dtTaxCalc.Rows(iRowTax)("TAXAMOUNT"))
                            Else
                                dbIncTotalTax = dbIncTotalTax + IIf(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT") Is DBNull.Value, 0, dtTaxCalc.Rows(iRowTax)("TAXAMOUNT"))
                            End If
                        End If
                    Next
                End With
                'dsMain.Tables("CASHMEMODTL").Rows(iRow - 1)("EXCLUSIVETAX") = dbExclTotalTax
                StrTaxCode = IIf(dtTaxCalc.Compute("SUM(TAXAMOUNT)", "") Is DBNull.Value, 0, dtTaxCalc.Compute("SUM(TAXAMOUNT)", ""))
                'dsMain.Tables("CASHMEMODTL").Rows(iRow - 1)("TOTALTAXAMOUNT") = StrTaxCode
                dtMainTax.Merge(dtTaxCalc, False, MissingSchemaAction.Ignore)
                dtMainTax.AcceptChanges()
                Return dbExclTotalTax
            End If
        Catch ex As Exception
            LogException(ex)

        End Try
    End Function


    Private Function CalculateTotalInclusiveTax(ByRef originalTaxAmt As Double, ByVal strMatcode As String, ByVal TaxableAmount As Double, ByVal quantity As Double, ByVal EAN As String, Optional ByVal isInclusiveTax As Boolean = False) As Double
        Try
            Dim dtTaxCalc As DataTable
            dtTaxCalc = New DataTable
            If IsCSTApplicable Then
                ''added by ketan tax changes
                'dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, "", "CMS", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, True)
                If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
                    If CustomerSaleType = 1 Then 'Dine_In = 1
                        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, "", "DIN", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, True)
                    ElseIf CustomerSaleType = 2 Then  ' Home_Delivery = 2
                        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, "", "HOD", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, True)
                    ElseIf CustomerSaleType = 3 Then 'Take_Away = 3
                        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, "", "TAK", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, True)
                    Else
                        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, "", "CMS", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, True)
                    End If
                Else
                    dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, "", "CMS", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, True)
                End If
                For Each Row In dtTaxCalc.Rows
                    Row("ArticleCode") = strMatcode
                    Row("EAN") = EAN
                Next
            Else
                'dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, False)
                If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
                    If CustomerSaleType = 1 Then 'Dine_In = 1
                        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "DIN", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, False)
                    ElseIf CustomerSaleType = 2 Then  ' Home_Delivery = 2
                        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "HOD", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, False)
                    ElseIf CustomerSaleType = 3 Then 'Take_Away = 3
                        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "TAK", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, False)
                    Else
                        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, False)
                    End If
                Else
                    dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, False)
                End If
            End If
            Dim dbIncTotalTax As Double = 0
            If dtTaxCalc.Rows.Count = 0 Then
                Return Nothing
            End If
            If dtTaxCalc.Rows.Count <> 0 Then
                If IsCSTApplicable Then
                    Dim amt As Double = GetTaxableAmountForCst(strMatcode, EAN, quantity, TaxableAmount)
                    originalTaxAmt = amt
                    '------- Changes By Mahesh In Case of CST OTHER Tax ALREADY NULL , now Tax is applicable to Tax Amt 
                    'dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount - amt
                    dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount

                    objCM.getCalculatedDataSet(dtTaxCalc)
                Else
                    dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
                    objCM.getCalculatedDataSet(dtTaxCalc)

                    Dim taxAmount = dtTaxCalc.Compute("SUM(TAXAMOUNT)", "Inclusive=1")
                    originalTaxAmt = Val(taxAmount.ToString())
                End If

                Dim iRowTax As Int16
                With dtTaxCalc
                    For iRowTax = 0 To dtTaxCalc.Rows.Count - 1
                        If Val(dtTaxCalc.Rows(iRowTax)("VALUE").ToString) <> 0 Then
                            dbIncTotalTax = dbIncTotalTax + Val(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT"))
                        End If
                    Next
                End With
                Return dbIncTotalTax
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM012"), "CM012 - " & getValueByKey("CLAE05"))
            LogException(ex)
            Return 0
        End Try
    End Function

    Private Sub DisplayCSTMessage()
        Dim EventType As Int32
        ShowMessage(getValueByKey("CST001"), getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
        If EventType = 1 Then
            Dim dtList = objCM.GetAllTaxesAppliedToSite(clsAdmin.SiteCode, "CMS")
            If dtList Is Nothing OrElse dtList.Rows.Count <= 0 Then
                ShowMessage("No Taxes available for this document type", getValueByKey("CLAE04"))
                Exit Sub
            End If
            Dim objView As New frmNCommonSearch
            objView.SetData = dtList
            objView.ShowDialog()
            If Not objView.search Is Nothing Then
                clsDefaultConfiguration.CSTTaxCode = objView.search(1)
            Else
                Exit Sub
            End If
            IsCSTApplicable = True
            rbnbtnApplyCST.Enabled = False
            ClearDiscountAndExistingTax()
        End If
    End Sub

    Private Sub ClearDiscountAndExistingTax()
        dtMainTax.Rows.Clear()
        For Each dr As DataRow In dsMain.Tables("Cashmemodtl").Rows
            dr("TotalDiscount") = 0
            dr("LineDiscount") = 0
            dr("TOTALDISCPERCENTAGE") = 0
            dr("FIRSTLEVEL") = String.Empty
            dr("TOPLEVEL") = String.Empty
            dr("MANUALPROMO") = 0
            Dim taxAmt = CalculateTotalInclusiveTax(0, dr("ARTICLECODE"), dr("GrossAmt"), 1, dr("EAN"), IsInclusiveTax)
            dr("TotalTaxAmount") = taxAmt

            'Rakesh-22.10.2013-8238: Refresh tax amount in summary
            If (dr("EXCLUSIVETAX") Is DBNull.Value OrElse dr("EXCLUSIVETAX") = 0) Then
                dr("EXCLUSIVETAX") = taxAmt
            End If

        Next
        isCashierPromoSelected = False
        ReCalculateCM("")
        calculateTotalbill()
    End Sub

    Private Function GetTaxableAmountForCst(ByVal strMatcode As String, ByVal EAN As String, ByVal Quantity As Double, ByVal TaxableAmount As Double) As Double
        Dim dtTaxCalc As DataTable
        dtTaxCalc = New DataTable

        'dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", Quantity, EAN, clsDefaultConfiguration.CSTTaxCode, False)
        If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
            If CustomerSaleType = 1 Then 'Dine_In = 1
                dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "DIN", Quantity, EAN, clsDefaultConfiguration.CSTTaxCode, False)
            ElseIf CustomerSaleType = 2 Then  ' Home_Delivery = 2
                dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "HOD", Quantity, EAN, clsDefaultConfiguration.CSTTaxCode, False)
            ElseIf CustomerSaleType = 3 Then 'Take_Away = 3
                dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "TAK", Quantity, EAN, clsDefaultConfiguration.CSTTaxCode, False)
            Else
                dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", Quantity, EAN, clsDefaultConfiguration.CSTTaxCode, False)
            End If
        Else
            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", Quantity, EAN, clsDefaultConfiguration.CSTTaxCode, False)
        End If
        If dtTaxCalc.Rows.Count > 0 Then
            dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
            objCM.getCalculatedDataSet(dtTaxCalc)
            Return dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")
        Else
            Return 0
        End If

    End Function

    ''' <summary>
    ''' Clear all Promotion's
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearAllPromo()
        Try
            For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Select("BTYPE='S' AND isnull(PROMOTIONID,'')<>'NO'", "", DataViewRowState.CurrentRows)
                If (dr("PROMOTIONID").ToString() <> String.Empty Or dr("FIRSTLEVEL").ToString() <> String.Empty Or dr("TOPLEVEL").ToString() <> String.Empty) Then
                    Try
                        If (dr("MANUALPROMO").ToString() = "") Then
                            dr("LineDiscount") = 0
                            dr("TOTALDISCPERCENTAGE") = 0
                            'dr("MANUALPROMO") = String.Empty
                            dr("PROMOTIONID") = String.Empty

                            dr("FirstLevelDisc") = 0
                            dr("TopLevelDisc") = 0
                            dr("TotalDiscount") = 0
                            dr("FIRSTLEVEL") = String.Empty
                            dr("TOPLEVEL") = String.Empty
                            PromotionCleared = True
                            isCashierPromoSelected = False
                        ElseIf dr("MANUALPROMO").ToString() = 0 Then
                            dr("LineDiscount") = 0
                            dr("TOTALDISCPERCENTAGE") = 0
                            'dr("MANUALPROMO") = String.Empty
                            dr("PROMOTIONID") = String.Empty

                            dr("FirstLevelDisc") = 0
                            dr("TopLevelDisc") = 0
                            dr("TotalDiscount") = 0
                            dr("FIRSTLEVEL") = String.Empty
                            dr("TOPLEVEL") = String.Empty
                            PromotionCleared = True
                            isCashierPromoSelected = False
                        End If
                    Catch ex As Exception
                        LogException(ex)
                    End Try
                End If
            Next
            If dsMain.Tables("CASHMEMOHDR").Rows.Count > 0 Then
                dsMain.Tables("CASHMEMOHDR").Rows(0)("DiscountAmt") = FormatNumber(CDbl(CheckIfBlank(dsMain.Tables("CASHMEMODtl").Compute("SUM(LINEDISCOUNT)", "").ToString())), 2)
                dsMain.Tables("CASHMEMOHDR").Rows(0)("TotalDiscount") = FormatNumber(CDbl(CheckIfBlank(dsMain.Tables("CASHMEMODtl").Compute("SUM(TOTALDISCOUNT)", "").ToString())), 2)
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub SetButtons(ByVal Steps As Int32, ByVal Value As Boolean)
        Try
            If UpdateFlag = False Then
                If Steps = 1 Then
                    'cmdDelete.Enabled = Value
                    cmdPayments.Enabled = Value
                    If IsTenderCreditCard Then cmdCard.Enabled = Value
                    If IsTenderCash Then cmdCash.Enabled = Value
                    If IsTenderCredit Then cmdCreditSale.Enabled = Value
                    If IsTenderCheque Then cmdCheque.Enabled = Value
                    cmdDefaultPromo.Enabled = Value
                    cmdClrAllPromo.Enabled = Value
                    cmdClearSelectedPromo.Enabled = Value
                    cmdApplySelectPromo.Enabled = Value
                    rbnbtnRoundOff.Enabled = Value
                ElseIf Steps = 2 Then
                    If IsTenderCreditCard Then cmdCard.Enabled = Value
                    If IsTenderCash Then cmdCash.Enabled = Value
                    If IsTenderCredit Then cmdCreditSale.Enabled = Value
                    If IsTenderCheque Then cmdCheque.Enabled = Value
                ElseIf Steps = 3 Then
                    CMbtnBottom.CtrlBtnSaleGV.Enabled = Value
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ClearCLP()
        Try
            For Each dr As DataRow In dsMain.Tables("CashMemoDtl").Select("Btype='S'", "", DataViewRowState.CurrentRows)
                dr("CLPPoints") = DBNull.Value
                dr("CLPDiscount") = DBNull.Value
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
#End Region
#Region "Class Events"
    Public Sub New(Optional ByVal isCashMemoSearch As Boolean = False)
        Try
            'ci = New CultureInfo(clsAdmin.CultureInfo)
            ''add by ram this take care of calendar to specific culture.
            'Thread.CurrentThread.CurrentCulture = ci
            ''end for add by ram 
            'Thread.CurrentThread.CurrentUICulture = ci
            'Rohit

            Me.CancelButton = Nothing
            If clsDefaultConfiguration.TillOperationRequired = True And clsDefaultConfiguration.TillOpenDone = False Then
                ShowMessage(getValueByKey("CM057"), "CM057 - " & getValueByKey("CLAE04"))
                Me.Dispose()
                Me.Close()
                Exit Sub
            End If
            If clsDefaultConfiguration.ChecklistOnTillOpen = True And clsDefaultConfiguration.TillOpenDone = True Then
                Dim ObjclsCommon As New clsCommon
                Dim IsCheckListFilled As Boolean = ObjclsCommon.IsCheckListFilled(clsAdmin.TerminalID, clsAdmin.SiteCode)

                If IsCheckListFilled = False Then
                    Dim IsCheckListExist As Boolean = ObjclsCommon.IsCheckListAvailabl()
                    If IsCheckListExist = True Then
                        Dim chkListObj As New frmchecklist()
                        chkListObj.ShowDialog()
                        chkListObj.Dispose()
                    Else
                        'ShowMessage(getValueByKey("CLIST03"), getValueByKey("CLIST03"))
                    End If

                End If
            End If
            If clsAdmin.TerminalID = String.Empty Then
                ShowMessage(getValueByKey("CM013"), "CM013 - " & getValueByKey("CLAE04"))
                'ShowMessage("Terminal has not provided any Id", "Information")
                Me.Dispose()
                Me.Close()
                Exit Sub
            End If

            Me.FrmTranCode = "Billing"
            Dim isAuthorised As Boolean
            If isCashMemoSearch Then
                isAuthorised = CheckAuthorisation(clsAdmin.UserCode, "CashmemoSearch")
            Else
                isAuthorised = CheckAuthorisation(clsAdmin.UserCode, "Billing")
            End If
            If isAuthorised = False Then
                ShowMessage(getValueByKey("SPCM001"), "SPCM001 - " & getValueByKey("CLAE04"))
                'ShowMessage("You have not Sufficent Rights", "Information")
                Me.Dispose()
                Me.Close()
                Exit Sub
            End If
            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            C1Ribbon1.pInitRbn()
            Me.ClientSize = New System.Drawing.Size(gmdiclientwidth, gmdiclientheight)
            InitializeDevices()
            ' Add any initialization after the InitializeComponent() call.
            'SetCulture(Me)

            C1Ribbon1.DbtnF12.LargeImage = Global.Spectrum.My.Resources.Resources.price_change
            C1Ribbon1.DbtnF2.LargeImage = Global.Spectrum.My.Resources.Resources.change_qty

            'Dim highlightRowStyle As New GridRendererOffice2007Blue
            'highlightRowStyle.Highlight = dgMainGrid.Styles.Highlight.BackColor
            'dgMainGrid.Renderer = highlightRowStyle
            dgMainGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
            dgMainGrid.ScrollBars = ScrollBars.Both

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try


            Dim eventType As Int32
            If UpdateFlag = False And dgMainGrid.Rows.Count - 1 > 0 Then
                ShowMessage(getValueByKey("CM014"), "CM014 - " & getValueByKey("CLAE04"), eventType)
                'ShowMessage("You have a bill on the Screen Hold first.", "Information", eventType)
                If eventType = 1 Then
                    exitNowFlag = True
                    Me.Close()
                    If CtrlSalesPersons.AndroidSearchTextBox.IsListBoxVisible Then
                        CtrlSalesPersons.AndroidSearchTextBox.ResetListBox()
                    End If
                ElseIf eventType = 2 Then
                    Try
                        If dgMainGrid.Rows(dgMainGrid.Row)(dgMainGrid.Col) = 0 Then
                            'ShowMessage(getValueByKey("CM021"), "CM021 - " & getValueByKey("CLAE04"))
                            dgMainGrid.Rows(dgMainGrid.Row)("Quantity") = 1
                            ReCalculateCM("")
                            calculateTotalbill()
                        End If
                    Catch ex As Exception

                    End Try
                    Exit Sub
                ElseIf eventType = 3 Then
                    Try
                        If dgMainGrid.Rows(dgMainGrid.Row)(dgMainGrid.Col) = 0 Then
                            'ShowMessage(getValueByKey("CM021"), "CM021 - " & getValueByKey("CLAE04"))
                            dgMainGrid.Rows(dgMainGrid.Row)("Quantity") = 1
                            ReCalculateCM("")
                            calculateTotalbill()
                        End If
                    Catch ex As Exception

                    End Try
                    cmdHold_Click(cmdHold, New EventArgs)
                    'Exit Sub
                End If
            Else
                Me.Close()
                If CtrlSalesPersons.AndroidSearchTextBox.IsListBoxVisible Then
                    CtrlSalesPersons.AndroidSearchTextBox.ResetListBox()
                End If
            End If
            If clsDefaultConfiguration.IsPoleDisply Then
                Dim md As MDISpectrum
                Dim objClsCommon As New clsCommon
                Dim ComPortName As String = clsDefaultConfiguration.SerialPort
                ObjclsCommon.Display20x2Line("", "", ComPortName, True)
                md.PoleDisplayTimer.Start()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub frmCashMemo_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'rbnLStatus.Text = IIf(OnlineConnect = True, "Online", "Offline")
        rbnLStatus.Text = IIf(OnlineConnect = True, getValueByKey("frmcashmemo.rbnlstatusonline"), getValueByKey("frmcashmemo.rbnlstatusoffline"))
        'CtrlSalesPersons.CtrlTxtBox.Focus()
        'CtrlSalesPersons.CtrlTxtBox.Select()

        If clsDefaultConfiguration.IsMembership Then
            CustInfo.CtrlTxtSwape.Focus()
            CustInfo.CtrlTxtSwape.Select()
        Else
            CtrlSalesPersons.AndroidSearchTextBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Focus()
        End If
    End Sub
    'Private Sub frmCashMemo_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    '    Try
    '        GetSubString(strTitle, Me.Text, False)
    '    Catch ex As Exception
    '        LogException(ex)
    '    End Try
    'End Sub


    Private Sub frmCashMemo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "new-cash-memo.htm")
            End If
            'MsgBox("key Up evetn of form " & e.KeyValue & " = " & e.KeyData.ToString)
            If e.KeyCode = Keys.F11 Then
                cmdReturns_Click(sender, New System.EventArgs)
            ElseIf e.KeyCode = Keys.F12 Then
                If (clsDefaultConfiguration.PriceChageAllowed) Then
                    PriceChange()
                    e.Handled = True
                End If
            ElseIf e.Control And e.KeyCode = Keys.N Then
                cmdNew_Click(sender, New System.EventArgs)

            ElseIf e.Control And e.KeyCode = Keys.R Then
                cmdReprint_Click(cmdReprint, New System.EventArgs)

            ElseIf e.Control And e.KeyCode = Keys.E Then
                If CheckInterTransactionAuth("EditBill", dsMain.Tables("CASHMEMOHDR")) = True Then
                    cmdOldCashMemo_Click(cmdOldCashMemo, New EventArgs)
                End If
            ElseIf e.Control And e.KeyCode = Keys.Delete And UpdateFlag Then
                cmdDelete_Click(cmdDelete, New EventArgs)

            ElseIf e.KeyCode = Keys.F8 AndAlso IsTenderCredit AndAlso cmdCreditSale.Enabled Then
                ''added by nikhil
                If dgMainGrid.Selection.ContainsCol(dgMainGrid.Cols("Quantity").Index) Then
                    CtrlSalesPersons.CtrlTxtBox.Focus()
                End If
                cmdCreditSale_Click(cmdCreditSale, New System.EventArgs)
                
            ElseIf e.KeyCode = Keys.F9 Then 'vipin
                If clsDefaultConfiguration.IsNewCreditSale Then
                    Dim objCreditSales As New frmNCreditSalesNew(False)
                    objCreditSales.ShowDialog()
                Else
                    Dim objCreditSales As New frmNCreditSales(False)
                    objCreditSales.ShowDialog()
                End If

            ElseIf e.KeyCode = Keys.F10 Then
                cmdAdvanceSale_Click(CMbtnBottom.CtrlBtnSaleCLPPoint, New EventArgs())

            ElseIf e.KeyCode = Keys.Escape Then
                cmdExit_Click(sender, e)

            ElseIf e.KeyCode = Keys.F2 Then
                ChangeQty()
                e.Handled = True
            ElseIf e.Control AndAlso e.KeyCode = Keys.F Then
                If (UpdateFlag = False) Then
                    cmdSearch_Click(CtrlSalesPersons.CtrlTxtBox, New KeyEventArgs(Keys.Enter))
                End If

            ElseIf e.Control AndAlso e.KeyCode = Keys.M Then
                cmdShowMoreInfo()

            ElseIf e.Control AndAlso e.KeyCode = Keys.B Then
                If clsDefaultConfiguration.ManualPromotionAllowed Then
                    Dim e1 As New System.EventArgs
                    cmdEnable_Click(cmdEnable, e1)
                End If
            ElseIf e.KeyCode = Keys.R Then
                'Dim ChildForm As New Spectrum.frmArticlesRemark
                'Try
                '    Dim lastRowIndex As Integer = dgMainGrid.Row
                '    If lastRowIndex = -1 Then Exit Sub
                '    Dim articlecode = dgMainGrid.Rows(lastRowIndex)("ArticleCode")
                '    Dim EAN = dgMainGrid.Rows(lastRowIndex)("EAN")
                '    Using objArticleRemark As New frmArticlesRemark
                '        If dsMain.Tables.Contains("CASHMEMODTLITEMREMARK") Then
                '            Dim Dr() = dsMain.Tables("CASHMEMODTLITEMREMARK").Select("ArticleCode ='" & articlecode & "' and EAN='" & EAN & "'")
                '            If Dr.Count > 0 Then
                '                objArticleRemark.ResultRemark = IIf(IsDBNull(Dr(0)("itemRemarks")), "", Dr(0)("itemRemarks"))
                '            End If
                '            If objArticleRemark.ShowDialog() = Windows.Forms.DialogResult.OK Then
                '                If Dr.Count > 0 Then
                '                    Dr(0)("Remark") = objArticleRemark.ResultRemark
                '                Else
                '                    Dim remarkRow As DataRow = dsMain.Tables("CASHMEMODTLITEMREMARK").NewRow()
                '                    remarkRow("siteCode") = clsAdmin.SiteCode
                '                    remarkRow("EAN") = EAN
                '                    remarkRow("ArticleCode") = articlecode
                '                    remarkRow("itemRemarks") = objArticleRemark.ResultRemark
                '                    dsMain.Tables("CASHMEMODTLITEMREMARK").Rows.Add(remarkRow)
                '                End If
                '            End If
                '        End If
                '    End Using
                'Catch ex As Exception
                '    LogException(ex)
                'End Try

            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub SetHeaderValues()
        'lblUserId.Text = clsAdmin.UserName
        'lblDayOpenDate.Text = clsAdmin.DayOpenDate.ToShortDateString()
        ''lblSiteCodeValue.Text = clsAdmin.SiteCode
        'lblSiteName.Text = objCM.GetSiteName(clsAdmin.SiteCode)
    End Sub

    Private Sub frmCashMemo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress

    End Sub



    Private Sub frmCashMemo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If clsDefaultConfiguration.IsPoleDisply Then
                Dim md As MDISpectrum
                md.PoleDisplayTimer.Stop()

            End If
            '' added by ketan Wild Search changes 
            Dim objDefault As New clsDefaultConfiguration("CMS")
            objDefault.GetDefaultSettings()
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType = False AndAlso clsDefaultConfiguration.EnablewildSearch = True Then
                ''Work Wild Seach funcatonality 
                CtrlSalesPersons.AndroidSearchTextBox.IsListBind = True
            Else
                ''Work Old Seach funcatonality 
                CtrlSalesPersons.AndroidSearchTextBox.IsListBind = False
            End If
            '---- Set Tab Index START
            SetFormTabStop(Me, tabStopValue:=False)

            Dim ctrTablIndex As New Dictionary(Of Object, Int16)
            ctrTablIndex.Add(Me.CtrlSalesPersons, 0)
            ctrTablIndex.Add(Me.CtrlSalesPersons.CtrlSalesPersons, 1)
            ctrTablIndex.Add(Me.CtrlSalesPersons.CtrlTxtBox, 2)

            ctrTablIndex.Add(Me.dgMainGrid, 3)
            ctrTablIndex.Add(Me.CMbtnBottom, 4)
            ctrTablIndex.Add(Me.CMbtnBottom.CtrlBtnSaleGV, 5)
            ctrTablIndex.Add(Me.CMbtnBottom.CtrlBtnSaleCLPPoint, 6)
            ctrTablIndex.Add(Me.CMbtnBottom.CtrlBtnHomeDelivery, 7)
            ctrTablIndex.Add(Me.CMbtnBottom.CtrlBtnStockCheck, 8)
            ctrTablIndex.Add(Me.CMbtnBottom.CtrlBtnAddExtraCost, 9)
            ctrTablIndex.Add(Me.CMbtnBottom.CtrlBtnReturn, 10)

            SetFormTabIndex(ctrTablIndex:=ctrTablIndex)
            Me.dgMainGrid.KeyActionTab = KeyActionEnum.None
            '---- Set Tab Index END 

            Me.CtrlSalesPersons.CtrlSalesPersons.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList
            Me.CMbtnBottom.CtrlBtnStockCheck.Visible = IIf(clsDefaultConfiguration.SpectrumLiteAllowed, False, True)

            PrintSetProperty()
            SetHeaderValues()
            'Me.Dock = DockStyle.Fill
            C1Ribbon1.ExitButton.Tag = "U"
            RibbonGroup1.Text = getValueByKey("CST003")
            rbnbtnApplyCST.Text = getValueByKey("CST004")
            rbnbtnApplyCST.LargeImage = My.Resources.ApplyCSTTax
            'added by ketan For Wild search 
            AddHandler CtrlSalesPersons.CtrlTxtBox.TextChanged, AddressOf txtSearch_textchange
            AddHandler CtrlSalesPersons.AndroidSearchTextBox.TextChanged, AddressOf AndroidSearchTextBox_Textchange
            'If CtrlSalesPersons.AndroidSearchTextBox.IsListBind = False Then
            AddHandler CtrlSalesPersons.AndroidSearchTextBox.KeyDown, AddressOf AndroidSearchTextBox_KeyDown
            'End If

            AddHandler C1Ribbon1.ExitButton.Click, AddressOf cmdExit_Click
            AddHandler C1Ribbon1.DbtnTopRightExit.Click, AddressOf cmdExit_Click
            AddHandler CtrlSalesPersons.CtrlTxtBox.KeyDown, AddressOf txtSearch_KeyDown
            AddHandler CtrlSalesPersons.CtrlCmdSearch.Click, AddressOf cmdSearch_Click
            AddHandler CMbtnBottom.CtrlBtnReturn.Click, AddressOf cmdReturns_Click
            AddHandler CMbtnBottom.CtrlBtnStockCheck.Click, AddressOf cmdStockCheck_Click
            AddHandler CMbtnBottom.CtrlBtnSaleGV.Click, AddressOf cmdAdvanceSale_Click
            AddHandler CMbtnBottom.CtrlBtnSaleCLPPoint.Click, AddressOf cmdAdvanceSale_Click
            AddHandler CMbtnBottom.CtrlBtnHomeDelivery.Click, AddressOf cmdHomeDelivery_Click
            AddHandler CMbtnBottom.CtrlBtnAddExtraCost.Click, AddressOf cmdAdjust_Click
            'AddHandler CMbtnBottom.CtrlBtnLoyaltyPrg.Click, AddressOf cmdLoyalty_Click
            AddHandler cmdApplySelectPromo.Click, AddressOf cmdDefaultPromo_Click
            'AddHandler CMbtnBottom.CtrlBtnClearAllItem.Click, AddressOf cmdNew_Click
            AddHandler CustInfo.CtrlLabel4.Click, AddressOf cmdShowMoreInfo
            AddHandler CustInfo.BtnClearCustmInfo.Click, AddressOf clearCustomerInfo
            AddHandler C1Ribbon1.DbtnF12.Click, AddressOf PriceChange
            AddHandler C1Ribbon1.DbtnF2.Click, AddressOf ChangeQty
            AddHandler CustInfo.CtrlTxtSwape.KeyDown, AddressOf CtrlTxtSwape_KeyDown

            Dim colno As Integer
            getBinding()
            'Dim objDefault As New clsDefaultConfiguration("CMS")
            'objDefault.GetDefaultSettings()
            SetDefaultSetting()

            Call EnableDiableTenderIcons()
            'CheckIfInclusiveTax()
            For colno = 1 To dgMainGrid.Cols.Count - 1
                If dgMainGrid.Cols(colno).Name.ToUpper() <> "DISCRIPTION".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "EAN".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "ArticleCode".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "SellingPrice".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "Quantity".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "TOTALDISCPERCENTAGE".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "Selects".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "CLPRequire".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "TotalDiscount".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "IFBNO".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "NetAmount".ToUpper() Then
                    If IsInclusiveTax Then
                        If dgMainGrid.Cols(colno).Name.ToUpper() <> "TOTALTAXAMOUNT".ToUpper() Then
                            HideColumns(dgMainGrid, False, dgMainGrid.Cols(colno).Name)
                        End If
                    Else
                        HideColumns(dgMainGrid, False, dgMainGrid.Cols(colno).Name)
                    End If
                    'HideColumns(dgMainGrid, False, dgMainGrid.Cols(colno).Name)
                End If
                If Not dgMainGrid.Cols(colno).DataType Is Nothing AndAlso dgMainGrid.Cols(colno).DataType.ToString() = "System.Decimal" Then
                    'dgMainGrid.Cols(colno).DataType = Type.GetType("System.Double")
                    dgMainGrid.Cols(colno).Format = "0.00"

                End If
            Next
            'LoadsalesData()
            If clsDefaultConfiguration.PriceChageAllowed = False Then
                Me.C1Ribbon1.DbtnF12.Visible = False
            End If
            GetListofManualPromotion()
            cmdNew_Click(sender, e)
            PaymentGridSetting()
            CustInfo.CtrlTxtCustomerNo.ReadOnly = True
            CustInfo.CtrltxtCustomerName.ReadOnly = True
            CustInfo.ctrlTxtPoints.ReadOnly = True
            CustInfo.CtrlLastVisit.ReadOnly = True
            'CustInfo.CtrlTxtSwape.ReadOnly = True
            CustInfo.TabStop = False
            PSetDefaultCurrencyOfCashMemoSummary(CashSummary)
            'SetButtonsCaption()
            SetCulture(Me, Me.Name, C1Ribbon1)
            ' added by Khusrao Adil
            ' for savoy 
            If clsDefaultConfiguration.BarcodeDisplayAllowed Then
                If clsDefaultConfiguration.IsSavoy Then
                    GridSettings(UpdateFlag)
                End If
            End If
            cmdHold.Text = getValueByKey("frmcashmemo.cmdresume")
            cmdHold.Tag = "RESUME"

            cbManualDisc.Enabled = False
            cmdEnable.Tag = "E"

            objCM.DecimalDigits = clsDefaultConfiguration.DecimalPlaces
            If clsDefaultConfiguration.IsCstTaxRequired Then
                RibbonGroup1.Visible = True
            Else
                RibbonGroup1.Visible = False
            End If

            '---- Added By Mahesh for HD flash
            AddHandler CustSaleTypeTimer.Tick, AddressOf timer_Tick
            CustSaleTypeTimer.Interval = CustSaleTypeTimerBlinkFrequency
            lblCustSaleType.Visible = False
            lblCustSaleType.Location = New Point(Me.Right - lblCustSaleType.Width - 15, lblCustSaleType.Location.Y + 20)
            '----------PcRoundoff
            RibbonGroup2.Visible = clsDefaultConfiguration.PCRoundOff
            '--- End of Load 
            'Me.Focus()
            CustInfo.CtrlLabel1.Text = "Cust No."
            If clsDefaultConfiguration.CUSTOMERSEARCHUSINGPHONENUMBER Then
                CustInfo.CtrlLabel2.Text = "Phone No."
            End If
            'CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Select()
            Dim condition As String
            Dim objItem As New clsIteamSearch
            condition = " AND A.ArticleCode <>'" & clsDefaultConfiguration.GVBaseArticle & "' AND A.ArticleCode <>'" & clsDefaultConfiguration.ClpBaseArticle & "' AND A.ArticleCode <>'" & clsAdmin.CVBaseArticle & "'"
            Dim dtBind = objItem.GetEANData(clsAdmin.SiteCode, "", clsAdmin.LangCode, condition, dtItemScanData)
            If dtBind.Rows.Count > 1 Then
                'Dim listSource As List(Of [String]) = (From row In dtBind Select Convert.ToString(row("ArticleCode")) + " " + Convert.ToString(row("Discription"))).Distinct().ToList()
                'CtrlSalesPersons.AndroidSearchTextBox.lstNames = listSource
                Call SetWildSearchTextBox(dtBind, CtrlSalesPersons.AndroidSearchTextBox, key:="ArticleCode", Value:="Discription", searchData:="ArticleCodeDesc")
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM015"), getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Transaction Not properly Set", "Error")
        Finally

        End Try
        'CtrlSalesPersons.CtrlTxtBox.Select()
        CtrlSalesPersons.AndroidSearchTextBox.Select()
        If clsDefaultConfiguration.IsMembership Then
            rbnGrpCMPromotion.Visible = False
        End If
        If clsDefaultConfiguration.IsPoleDisply Then
            Dim comm As New clsCommon
            dtExtend = comm.GetDetailsExtendScreen()
            TwoLineExtendScreen()
        End If
        If clsDefaultConfiguration.EnableScanQRCode = True Then 'vipin 23042018 Mahavir changes
            QRTrailerSeperator = clsDefaultConfiguration.QRCodeTrailer
            Dim QrSplitString() As String = Split(QRTrailerSeperator, ",")
            QRTrailerSegment = QrSplitString(1)
            QRSepearatorSegment = QrSplitString(0)
        End If
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            ThemeChange()
        End If

    End Sub

    Private Sub timer_Tick(ByVal sender As Object, e As EventArgs)
        lblCustSaleType.Visible = Not lblCustSaleType.Visible
    End Sub

    Private Sub ScanItemBillWise(ByRef rowindex As Integer)
        Try
            If (IsNumeric(ScaleBillNo)) Then

                Dim ScaleNo As Integer = Val(ScaleBillNo.Substring(0, 2))
                Dim BillNo As Integer = Val(ScaleBillNo.Substring(2, 4))

                dtScanedBillArticle = objItemSch.GetScanedBillArticle(ScaleNo:=ScaleNo, BillNo:=BillNo, BillIntDate:=ScaleBillIntDate, MettlerConn:=clsDefaultConfiguration.MettlerConnString)
                If dtScanedBillArticle IsNot Nothing AndAlso dtScanedBillArticle.Rows.Count > 0 Then
                    ScanBill = True
                    Dim e As New System.Windows.Forms.KeyEventArgs(Keys.Enter)
                    dgMainGrid.Cols("Quantity").AllowEditing = True
                    For rowindex = 0 To dtScanedBillArticle.Rows.Count - 1 Step 1
                        CtrlSalesPersons.CtrlTxtBox.Text = dtScanedBillArticle.Rows(rowindex)("LegacyArticleCode")
                        txtSearch_KeyDown(CtrlSalesPersons.CtrlTxtBox, e)
                    Next rowindex
                    ScanBill = False
                    'ScanBillsDictionary.Add(ScaleBillNo, dtScanedBillArticle.Rows.Count)
                    '---- Enter Bill in Table 
                    dsMain.Tables("CashMemoMettler").Rows.Add()
                    dsMain.Tables("CashMemoMettler").Rows(dsMain.Tables("CashMemoMettler").Rows.Count - 1)("MettlerScaleBillNo") = ScaleBillNo
                    dsMain.Tables("CashMemoMettler").Rows(dsMain.Tables("CashMemoMettler").Rows.Count - 1)("MettlerScaleBillDate") = ScaleBillIntDate
                    dsMain.Tables("CashMemoMettler").Rows(dsMain.Tables("CashMemoMettler").Rows.Count - 1)("TotalLineItems") = dtScanedBillArticle.Rows.Count

                    dgMainGrid.Cols("Quantity").AllowEditing = False
                Else
                    ShowMessage(getValueByKey("CM016"), "CM016 - " & getValueByKey("CLAE04"))
                    CtrlSalesPersons.CtrlTxtBox.Text = ""
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            ScanBill = False
        End Try

    End Sub
    'added by ketan For Wild search 
    Private Sub AndroidSearchTextBox_Textchange(sender As Object, e As EventArgs)
        If CtrlSalesPersons.AndroidSearchTextBox.IsItemSelected Then
            CtrlSalesPersons.CtrlTxtBox.Text = CtrlSalesPersons.AndroidSearchTextBox.Text
        End If
        If clsDefaultConfiguration.EnableScanQRCode = True Then 'vipin 23042018
            CtrlSalesPersons.CtrlTxtBox.Text = CtrlSalesPersons.AndroidSearchTextBox.Text
            If CtrlSalesPersons.CtrlTxtBox.Text.LastOrDefault = QRTrailerSegment Then
                Call txtSearch_KeyDown(sender, New KeyEventArgs(Keys.Enter))
            End If
        End If
    End Sub
    Private Sub txtSearch_textchange(sender As Object, e As EventArgs)
        If Not String.IsNullOrEmpty(CtrlSalesPersons.CtrlTxtBox.Text) AndAlso CtrlSalesPersons.AndroidSearchTextBox.IsItemSelected Then
            CtrlSalesPersons.AndroidSearchTextBox.IsItemSelected = False
            'SendKeys.Send("{Enter}")
            Dim eKeyDown = New System.Windows.Forms.KeyEventArgs(Keys.Enter)
            Call txtSearch_KeyDown(sender, eKeyDown)
        End If
    End Sub
    Private Sub AndroidSearchTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
            CtrlSalesPersons.CtrlTxtBox.Text = CtrlSalesPersons.AndroidSearchTextBox.Text
            If CtrlSalesPersons.AndroidSearchTextBox.IsItemSelected Then
                If (String.IsNullOrEmpty(CtrlSalesPersons.AndroidSearchTextBox.Text.Trim()) AndAlso dgMainGrid.Rows.Count > 1) Then
                    If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType Then
                        Select Case CustomerSaleType
                            Case enumCustomerSaleType.Dine_In
                                cmdCash_Click(cmdDefaultPromo, e)
                            Case enumCustomerSaleType.Home_Delivery
                                '----On Enter Key Hold First time Key -----
                                cmdCreditSale_Click(cmdCreditSale, New System.EventArgs)
                            Case enumCustomerSaleType.Take_Away
                                cmdCash_Click(cmdDefaultPromo, e)
                            Case Else
                        End Select
                    Else
                        cmdCash_Click(cmdDefaultPromo, e)
                    End If

                End If
            End If
        End If
        If e.KeyCode = Keys.Delete Then
            txtSearch_KeyDown(sender, New System.Windows.Forms.KeyEventArgs(Keys.Delete))
        End If
    End Sub
    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
           
            Cursor.Current = Cursors.WaitCursor

            Dim price As Double
            Dim strArticle As String = ""
            Dim Ean As String = ""
            Dim tax As Object
            Dim SP As String = ""
            Dim Weight As String = ""
            Dim WeghingScaleBarcode = False
            Dim flag As Integer = 0
            CtrlSalesPersons.CtrlTxtBox.Text = CtrlSalesPersons.CtrlTxtBox.Text.ToString().Split(" ")(0)
            Dim membershipmaparticle = CtrlSalesPersons.CtrlTxtBox.Text
            '''--------  Code Added By Mahesh for Implementaing HD fns like Fast Cash Memo ....
            If e.KeyCode = Keys.Enter Then
                If clsDefaultConfiguration.customerwisepricemanagement = True Then
                    'If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then
                    '    ShowMessage(getValueByKey("CHKP11"), getValueByKey("CLAE04"))
                    '    CtrlSalesPersons.AndroidSearchTextBox.Clear()
                    '    CtrlSalesPersons.AndroidSearchTextBox.Select()
                    '    Exit Sub
                    'End If
                End If
                'Open Cash payment screen when enter 00 value
                If (clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType AndAlso CtrlSalesPersons.CtrlTxtBox.Text.Equals("0")) Then
                    CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                    CtrlSalesPersons.AndroidSearchTextBox.Text = String.Empty
                    'cmdCustomerSearchAndLoad("DINEIN")
                    'If (Not String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then
                    CustomerSaleType = enumCustomerSaleType.Dine_In
                    Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
                    dsMain.Tables("CashMemoHdr").Rows(0)("ServiceTaxAmount") = CustomerSaleType
                    IsHoldEnterKey = False
                    lblCustSaleType.Text = "Dine-In"
                    HideColumns(dgMainGrid, True, "TAKEAWAYQUANTITY")
                    lblCustSaleType.Visible = True
                    CustSaleTypeTimer.Start()
                    ''---added by ketan add tax based on order type
                    If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
                        If dgMainGrid.Rows.Count > 1 Then
                            RecalculateTax = True
                            BillRecalculateTax()
                        End If
                    End If
                    '   End If
                    'CtrlSalesPersons.CtrlTxtBox.Select()
                    'CtrlSalesPersons.CtrlTxtBox.Focus()
                    CtrlSalesPersons.AndroidSearchTextBox.Select()
                    CtrlSalesPersons.AndroidSearchTextBox.Focus()
                    Exit Sub
                ElseIf CtrlSalesPersons.CtrlTxtBox.Text.Equals("00") Then
                    CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                    CtrlSalesPersons.AndroidSearchTextBox.Text = String.Empty
                    cmdHomeDelivery_Click(sender, e)
                    'CtrlSalesPersons.CtrlTxtBox.Select()
                    'CtrlSalesPersons.CtrlTxtBox.Focus()
                    CtrlSalesPersons.AndroidSearchTextBox.Select()
                    CtrlSalesPersons.AndroidSearchTextBox.Focus()
                    If (Not String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then
                        If CustomerSaleType = enumCustomerSaleType.Dine_In Then
                            For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Rows
                                dr("TakeAwayQuantity") = 0
                            Next
                        End If
                        CustomerSaleType = enumCustomerSaleType.Home_Delivery
                        Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
                        dsMain.Tables("CashMemoHdr").Rows(0)("ServiceTaxAmount") = CustomerSaleType
                        'As Per discussed with kamal and santoshOn home delivery the bill should be saved on click of enter.
                        'IsHoldEnterKey = False
                        IsHoldEnterKey = True
                        lblCustSaleType.Text = "Home Delivery"
                        HideColumns(dgMainGrid, False, "TAKEAWAYQUANTITY")
                        ''---added by ketan add tax based on order type
                        If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
                            If dgMainGrid.Rows.Count > 1 Then
                                RecalculateTax = True
                                BillRecalculateTax()
                            End If
                        End If
                    End If
                    Exit Sub
                ElseIf (clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType AndAlso CtrlSalesPersons.CtrlTxtBox.Text.Equals("000")) Then
                    CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                    CtrlSalesPersons.AndroidSearchTextBox.Text = String.Empty
                    'cmdCustomerinfo_Click(sender, e)
                    'cmdCustomerSearchAndLoad("TAKEAWAY")
                    'If (Not String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then
                    If CustomerSaleType = enumCustomerSaleType.Dine_In Then
                        For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Rows
                            dr("TakeAwayQuantity") = 0
                        Next
                    End If

                    CustomerSaleType = enumCustomerSaleType.Take_Away
                    Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
                    dsMain.Tables("CashMemoHdr").Rows(0)("ServiceTaxAmount") = CustomerSaleType
                    IsHoldEnterKey = False
                    lblCustSaleType.Text = "Take-Away"
                    lblCustSaleType.Visible = True
                    HideColumns(dgMainGrid, False, "TAKEAWAYQUANTITY")
                    CustSaleTypeTimer.Start()
                    ''---added by ketan add tax based on order type
                    If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
                        If dgMainGrid.Rows.Count > 1 Then
                            RecalculateTax = True
                            BillRecalculateTax()
                        End If
                    End If
                    'End If
                    'CtrlSalesPersons.CtrlTxtBox.Select()
                    'CtrlSalesPersons.CtrlTxtBox.Focus()
                    CtrlSalesPersons.AndroidSearchTextBox.Select()
                    CtrlSalesPersons.AndroidSearchTextBox.Focus()

                    Exit Sub
                ElseIf (String.IsNullOrEmpty(CtrlSalesPersons.CtrlTxtBox.Text.Trim()) AndAlso dgMainGrid.Rows.Count > 1) Then
                    'cmdCash_Click(cmdDefaultPromo, e)
                    If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType Then
                        Select Case CustomerSaleType
                            Case enumCustomerSaleType.Dine_In
                                cmdCash_Click(cmdDefaultPromo, e)
                            Case enumCustomerSaleType.Home_Delivery
                                '----On Enter Key Hold First time Key -----
                                'If IsHoldEnterKey Then
                                '    cmdCreditSale_Click(cmdCreditSale, New System.EventArgs)
                                'Else
                                '    IsHoldEnterKey = True
                                'End If
                                cmdCreditSale_Click(cmdCreditSale, New System.EventArgs)
                            Case enumCustomerSaleType.Take_Away
                                cmdCash_Click(cmdDefaultPromo, e)
                            Case Else
                        End Select
                    Else
                        cmdCash_Click(cmdDefaultPromo, e)
                    End If
                    'CtrlSalesPersons.CtrlTxtBox.Select()
                    'CtrlSalesPersons.CtrlTxtBox.Focus()
                    If clsDefaultConfiguration.IsMembership Then
                        CustInfo.CtrlTxtSwape.Focus()
                        CustInfo.CtrlTxtSwape.Select()
                    Else
                        CtrlSalesPersons.AndroidSearchTextBox.Select()
                        CtrlSalesPersons.AndroidSearchTextBox.Focus()
                    End If
                    Exit Sub
                End If
            ElseIf e.KeyCode = Keys.Delete AndAlso dgMainGrid.Rows.Count > 1 Then
                'If clsDefaultConfiguration.IsBillScanApplicable Then
                '    If (dsMain.Tables("CashMemoMettler") IsNot Nothing AndAlso dsMain.Tables("CashMemoMettler").Rows.Count > 0) Then
                '        Exit Sub
                '    End If
                'End If
                IsHoldEnterKey = False
                'Dim taxCount = dtMainTax.Rows.Count
                'Dim taxIndex As Integer = -1
                'For index = 0 To taxCount - 1
                '    If (dtMainTax.Rows(index)("ArticleCode").Equals(dgMainGrid.Rows(dgMainGrid.Row)("ArticleCode")) AndAlso _
                '        dtMainTax.Rows(index)("EAN").Equals(dgMainGrid.Rows(dgMainGrid.Row)("EAN"))) Then
                '        taxIndex = index
                '    End If
                'Next

                'If (taxIndex <> -1) Then
                '    dtMainTax.Rows.RemoveAt(taxIndex)
                'End If


                Dim taxFilter As String = String.Format("BillLineNo={0} AND ArticleCode='{1}' AND EAN='{2}'",
    dgMainGrid.Rows(dgMainGrid.Row)("BillLineNo"), dgMainGrid.Rows(dgMainGrid.Row)("ArticleCode"), dgMainGrid.Rows(dgMainGrid.Row)("EAN"))
                Dim drTax = dtMainTax.Select(taxFilter)

                If (drTax.Length > 0) Then
                    For Each dr As DataRow In drTax
                        dr.Delete()
                    Next
                    dtMainTax.AcceptChanges()
                End If

                dgMainGrid.Rows.Remove(dgMainGrid.Row)
                'dsMain.Tables("CASHMEMODTL").AcceptChanges()
                RemoveDeletedRow(dsMain.Tables("CASHMEMODTL"))
                CreatingLineNO(dsMain, "CashMemoDtl")
                calculateTotalbill()
                If (dgMainGrid.Rows.Count > 1) Then
                    dgMainGrid.Select(1, 2)
                End If
                'CtrlSalesPersons.CtrlTxtBox.Select()
                'CtrlSalesPersons.CtrlTxtBox.Focus()
                CtrlSalesPersons.AndroidSearchTextBox.Select()
                CtrlSalesPersons.AndroidSearchTextBox.Focus()
                Exit Sub
            End If
            '''--------  Code Ended By Mahesh 

            If (e.KeyCode = Keys.Enter AndAlso CtrlSalesPersons.CtrlTxtBox.Text <> String.Empty) Then
                IsHoldEnterKey = False
                If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType AndAlso CustomerSaleType = 0 Then Exit Sub

                '--- Added By Mahesh for Prashant Corner Bill Addition  6 char is combination for 
                If clsDefaultConfiguration.IsBillScanApplicable AndAlso CtrlSalesPersons.CtrlTxtBox.Text.ToString().Length = 6 And clsDefaultConfiguration.EnableScanQRCode = False Then
                    If ScanBill = False Then
                        ScaleBillNo = CtrlSalesPersons.CtrlTxtBox.Text.ToString()
                        '---- Check Whether Bill Exist In database 
                        ScaleBillIntDate = Val(clsAdmin.DayOpenDate.Year.ToString.Substring(2, 2) & clsAdmin.DayOpenDate.Month.ToString.PadLeft(2, "0") & clsAdmin.DayOpenDate.Day.ToString.PadLeft(2, "0"))
                        '-----------
                        If (Not objItemSch.IsValidScanedHoldBillArticle(ScaleBillNo:=ScaleBillNo, ScaleBillIntDate:=ScaleBillIntDate)) Then
                            CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                            ShowMessage(getValueByKey("CM073"), "CM0_73 - " & getValueByKey("CLAE04"))
                            Exit Sub
                        End If
                        If (Not objItemSch.IsValidScanedBillArticle(ScaleBillNo:=ScaleBillNo, ScaleBillIntDate:=ScaleBillIntDate)) Then
                            '----Check Whether Bill Exist in Current Cash Memo if Yes Then Delete First
                            If (dsMain.Tables("CashMemoMettler") IsNot Nothing AndAlso dsMain.Tables("CashMemoMettler").Rows.Count > 0) Then
                                Dim dr() = dsMain.Tables("CashMemoMettler").Select("MettlerScaleBillNo='" & ScaleBillNo & "' AND MettlerScaleBillDate=" & ScaleBillIntDate)
                                If (dr.Length > 0) Then
                                    ''--Delete Bill No from tables
                                    'For Each Row As DataRow In dr
                                    '    dsMain.Tables("CashMemoMettler").Rows.Remove(Row)
                                    'Next

                                    'Dim drdtl() = dsMain.Tables("CashMemoDtl").Select("BillNo='" & ScaleBillNo & "'")
                                    'If (drdtl.Length > 0) Then
                                    '    '--Delete Bill No from  tables
                                    '    For Each Row As DataRow In drdtl
                                    '        dsMain.Tables("CashMemoDtl").Rows.Remove(Row)
                                    '    Next
                                    'End If
                                    'dsMain.AcceptChanges()

                                    'Delete All Bill then Reload all Bills 
                                    dsMain.Tables("CashMemoDtl").Rows.Clear()
                                    dsMain.Tables("CashMemoDtl").AcceptChanges()
                                    Dim dtTemp = dsMain.Tables("CashMemoMettler").Copy()
                                    For rowIndex = 0 To dtTemp.Rows.Count - 1
                                        ScaleBillNo = dtTemp.Rows(rowIndex)("MettlerScaleBillNo")
                                        Dim drDel() = dsMain.Tables("CashMemoMettler").Select("MettlerScaleBillNo='" & ScaleBillNo & "' AND MettlerScaleBillDate=" & ScaleBillIntDate)
                                        If (drDel.Length > 0) Then
                                            For Each Row As DataRow In drDel
                                                dsMain.Tables("CashMemoMettler").Rows.Remove(Row)
                                                ScanItemBillSequence = 0
                                                Call ScanItemBillWise(ScanItemBillSequence)
                                            Next
                                        End If
                                    Next rowIndex
                                    Exit Sub
                                End If
                            End If
                            '--- Scan all bill items
                            ScanItemBillSequence = 0
                            Call ScanItemBillWise(ScanItemBillSequence)
                            Exit Sub
                        Else
                            CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                            ShowMessage(getValueByKey("CM071"), "CM0_71 - " & getValueByKey("CLAE04"))
                            Exit Sub
                        End If
                    End If
                End If
                If Not clsDefaultConfiguration.IsGVSellAllowedWithOtherArticle AndAlso CheckIfGVItemScanned() Then
                    ShowMessage("Can not sell article with GV", getValueByKey("CLAE04"))
                    CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                    'CtrlSalesPersons.CtrlTxtBox.Select()
                    'CtrlSalesPersons.CtrlTxtBox.Focus()
                    CtrlSalesPersons.AndroidSearchTextBox.Select()
                    CtrlSalesPersons.AndroidSearchTextBox.Focus()
                    Exit Sub
                End If

                If clsDefaultConfiguration.SalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0 Then
                    ShowMessage(getValueByKey("CM025"), "CM025 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Please Select the Sales Person", "information")
                    CtrlSalesPersons.AndroidSearchTextBox.Text = ""
                    CtrlSalesPersons.CtrlSalesPersons.Focus()

                    Exit Sub
                End If
                If clsDefaultConfiguration.WeightScaleEnabled Then
                    'Weight = CtrlSalesPersons.CtrlTxtBox.Text.Substring(CtrlSalesPersons.CtrlTxtBox.Text.Length - clsDefaultConfiguration.WeightBarcodeLength, clsDefaultConfiguration.WeightBarcodeLength)
                    'CtrlSalesPersons.CtrlTxtBox.Text = CtrlSalesPersons.CtrlTxtBox.Text.Substring(clsDefaultConfiguration.WeightBarcodePrefixDigits, CtrlSalesPersons.CtrlTxtBox.Text.Length - 7)
                    'If clsDefaultConfiguration.WeightBarcodeRateApplicable Then

                    'End If
                    If CtrlSalesPersons.CtrlTxtBox.TextLength >= clsDefaultConfiguration.WeightBarcodePrefixDigits AndAlso CtrlSalesPersons.CtrlTxtBox.Text.Substring(0, clsDefaultConfiguration.WeightBarcodePrefixDigits) = clsDefaultConfiguration.WeightBarcodePrefix Then
                        Dim lst As List(Of SpectrumCommon.Sequence) = clsDefaultConfiguration.WeightBarcodeSequence
                        lst.SetWeighingScaleBarcodeElementValues(CtrlSalesPersons.CtrlTxtBox.Text.Trim())
                        For Each comp As Sequence In lst
                            Select Case comp.Element
                                Case WeighingScaleBarcodeSections.EAN.ToString()
                                    CtrlSalesPersons.CtrlTxtBox.Text = comp.SeqValue
                                Case WeighingScaleBarcodeSections.Prefix.ToString()

                                Case WeighingScaleBarcodeSections.Qty.ToString()
                                    Weight = comp.SeqValue
                                Case WeighingScaleBarcodeSections.Rate.ToString()
                                    SP = comp.SeqValue
                            End Select
                        Next
                        WeghingScaleBarcode = True
                        '' added By Ketan Remove all Zeros From Left side of articlecOde
                        Dim EANCode As String = CtrlSalesPersons.CtrlTxtBox.Text
                        CtrlSalesPersons.CtrlTxtBox.Text = CInt(EANCode)
                    End If


                End If

                Dim tolerance As Boolean
                Dim dt As New DataTable
                Dim openMrp As Boolean = False
                If clsDefaultConfiguration.IsBatchManagementReq Then
                    Dim articleCode As String = objItemSch.GetArticleCodeFromBarcode(clsAdmin.SiteCode, CtrlSalesPersons.CtrlTxtBox.Text.Trim)
                    If String.IsNullOrEmpty(articleCode) Then
                        articleCode = objItemSch.GetArticleCodeFromEAN(CtrlSalesPersons.CtrlTxtBox.Text.Trim)
                        If String.IsNullOrEmpty(articleCode) Then
                            articleCode = CtrlSalesPersons.CtrlTxtBox.Text.Trim
                        End If
                        Dim barCode As String
                        Dim _IsBarcodeNotAvailable, _IsBarcodeAvlbleQtyZero As Boolean
                        tolerance = objItemSch.GetTorelanceValFromArticleCode(articleCode)
                        'If Not tolerance AndAlso clsDefaultConfiguration.WeightScaleEnabled = False AndAlso WeghingScaleBarcode = False Then
                        If clsDefaultConfiguration.IsBatchManagementReq AndAlso WeghingScaleBarcode = False Then
                            barCode = SearchAvailableBarcodes(articleCode, _IsBarcodeNotAvailable, _IsBarcodeAvlbleQtyZero)
                            If String.IsNullOrEmpty(barCode) Then
                                If _IsBarcodeAvlbleQtyZero Then
                                    ShowMessage(getValueByKey("BL046"), getValueByKey("CLAE04"))
                                End If

                                If _IsBarcodeNotAvailable Then
                                    ShowMessage(getValueByKey("barcodeerror"), getValueByKey("CLAE04"))
                                End If
                                Exit Sub
                            Else
                                CtrlSalesPersons.CtrlTxtBox.Text = barCode
                            End If
                        End If
                    Else
                        Dim BarCodeTable = objCM.GetBardCodesForArticle(clsAdmin.SiteCode, articleCode)
                        Dim row = BarCodeTable.Select("Batchbarcode='" & CtrlSalesPersons.CtrlTxtBox.Text.Trim & "'")
                        If row.Length > 0 Then
                            Dim expiry As Date = row(0)("ExpiryDate")
                            If Now.Date > expiry And Now.Date <> expiry Then
                                ShowMessage("Selected Barcode is expired", "Information")
                                CtrlSalesPersons.CtrlTxtBox.Text = ""
                                CtrlSalesPersons.AndroidSearchTextBox.Text = ""
                                CtrlSalesPersons.AndroidSearchTextBox.Focus()
                                Exit Sub
                            End If
                        End If
                        'Else
                        '    dt = objCM.GetItemDetails(clsAdmin.SiteCode, CtrlSalesPersons.CtrlTxtBox.Text.Trim, openMrp, clsAdmin.LangCode, clsDefaultConfiguration.IsBatchManagementReq)
                    End If
                End If

                'dt = objCM.GetItemDetails(clsAdmin.SiteCode, CtrlSalesPersons.CtrlTxtBox.Text.Trim, openMrp, clsAdmin.LangCode, If(WeghingScaleBarcode, False, clsDefaultConfiguration.IsBatchManagementReq))
                If clsDefaultConfiguration.EnableScanQRCode = True Then 'vipin QR code scan changes
                    Dim QRArticleSplit() As String
                    Dim QrArticleCode As String = ""
                    Dim QRQty As String = ""

                    CtrlSalesPersons.CtrlTxtBox.Text = CtrlSalesPersons.CtrlTxtBox.Text.ToString.Trim()
                    If CtrlSalesPersons.CtrlTxtBox.Text.LastOrDefault = QRTrailerSegment Then
                        CtrlSalesPersons.CtrlTxtBox.Text = CtrlSalesPersons.CtrlTxtBox.Text.Substring(0, CtrlSalesPersons.CtrlTxtBox.Text.Length - 1)
                    End If
                    Dim DtTempDetectScan = objCM.GetItemDetails(clsAdmin.SiteCode, CtrlSalesPersons.CtrlTxtBox.Text, openMrp, clsAdmin.LangCode)
                    If Not DtTempDetectScan Is Nothing AndAlso DtTempDetectScan.Rows.Count > 0 Then
                        GoTo NormalScanItem
                    End If
                    QRArticleSplit = Split(CtrlSalesPersons.CtrlTxtBox.Text.Trim, QRSepearatorSegment)

                    For Index = 0 To QRArticleSplit.Length - 1
                        QrArticleCode = QRArticleSplit(Index).ToString.Substring(0, 6)
                        QRQty = QRArticleSplit(Index).ToString.Substring(6, 6)

                        If QrArticleCode.ToUpper() = QrArticleCode.ToLower() Then
                            CtrlSalesPersons.CtrlTxtBox.Text = CInt(QrArticleCode).ToString()
                        Else

                        End If

                        dt = objCM.GetItemDetails(clsAdmin.SiteCode, CtrlSalesPersons.CtrlTxtBox.Text.Trim, openMrp, clsAdmin.LangCode)

                        For Each dtdt In dt.Rows
                            If dtdt("UNITOFMEASURE") = "KGS" Then
                                QRQty = CDbl(QRQty) / 1000
                            ElseIf dtdt("UNITOFMEASURE") = "NOS" Then
                                QRQty = CDbl(QRQty)
                            End If

                            dtdt("Quantity") = dtdt("Quantity") + CDbl(QRQty)
                        Next
                        ' HandleComboArticles(dt)
                    Next

                Else
                    ' dt = objCM.GetItemDetails(clsAdmin.SiteCode, CtrlSalesPersons.CtrlTxtBox.Text.Trim, openMrp, clsAdmin.LangCode, If(WeghingScaleBarcode, False, clsDefaultConfiguration.IsBatchManagementReq))
NormalScanItem:     If clsDefaultConfiguration.customerwisepricemanagement = True Then
                        dt = objCM.GetItemDetailsCustomerWise(clsAdmin.SiteCode, CtrlSalesPersons.CtrlTxtBox.Text.Trim, openMrp, CustInfo.CtrlTxtCustomerNo.Text.Trim, clsAdmin.LangCode, If(WeghingScaleBarcode, False, clsDefaultConfiguration.IsBatchManagementReq))
                        If dt.Rows.Count = 0 Then
                            dt = objCM.GetItemDetails(clsAdmin.SiteCode, CtrlSalesPersons.CtrlTxtBox.Text.Trim, openMrp, clsAdmin.LangCode, If(WeghingScaleBarcode, False, clsDefaultConfiguration.IsBatchManagementReq))
                        End If
                    Else
                        dt = objCM.GetItemDetails(clsAdmin.SiteCode, CtrlSalesPersons.CtrlTxtBox.Text.Trim, openMrp, clsAdmin.LangCode, If(WeghingScaleBarcode, False, clsDefaultConfiguration.IsBatchManagementReq))

                    End If
                    If clsDefaultConfiguration.EnableScanQRCode = True Then
                        For Each dtdt In dt.Rows
                            dtdt("Quantity") = 1
                        Next
                    End If
                End If
                If clsDefaultConfiguration.PrintItemFullName Then
                    Dim objClsCommon As New clsCommon
                    If dt.Rows.Count > 0 Then
                        dt(0)("DISCRIPTION") = objClsCommon.GetArticleFullName(dt.Rows(0)("ArticleCode").ToString())
                    End If
                End If
                If Not dt Is Nothing AndAlso dt.Rows.Count = 0 Then
                    If WeghingScaleBarcode Then
                        MessageBox.Show(getValueByKey("weighingscale001"))
                        CtrlSalesPersons.CtrlTxtBox.Text = ""
                        'CtrlSalesPersons.CtrlTxtBox.Focus()
                        CtrlSalesPersons.AndroidSearchTextBox.Text = ""
                        CtrlSalesPersons.AndroidSearchTextBox.Focus()
                        Exit Sub
                    End If

                    ShowMessage(getValueByKey("CM016"), "CM016 - " & getValueByKey("CLAE04"))
                    'ShowMessage("No Such Item Found or Item is not Saleable.....", "Information")
                    CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                    CtrlSalesPersons.AndroidSearchTextBox.Text = String.Empty
                    'CtrlSalesPersons.CtrlTxtBox.Focus()
                    CtrlSalesPersons.AndroidSearchTextBox.Focus()

                    Exit Sub
                    'code added by vipul for issue id 2736
                ElseIf Not dt Is Nothing AndAlso dt.Rows.Count = 1 Then
                    If CheckKitArticleCodeInMstArticleKit(dt, CtrlSalesPersons.CtrlTxtBox.Text.Trim) = False Then
                        ShowMessage("Article not present in the Kit.", getValueByKey("CLAE04"))
                        CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                        CtrlSalesPersons.AndroidSearchTextBox.Text = String.Empty
                        CtrlSalesPersons.AndroidSearchTextBox.Focus()
                        Exit Sub
                    End If
                ElseIf Not dt Is Nothing AndAlso dt.Rows.Count > 1 Then
                    Dim dvEan As New DataView(dt, "Ean='" & CtrlSalesPersons.CtrlTxtBox.Text & "'", "", DataViewRowState.CurrentRows)
                    If dvEan.Count > 0 Then
                        dvEan.RowFilter = "EAN<>'" & CtrlSalesPersons.CtrlTxtBox.Text & "'"
                        If dvEan.Count > 0 Then
                            dvEan.AllowDelete = True
                            For Each dr As DataRowView In dvEan
                                dr.Delete()
                            Next
                            dt.AcceptChanges()
                        End If
                    Else
                        Dim dv As New DataView(dt, "DefaultEAN <> 1", "", DataViewRowState.CurrentRows)
                        'Dim dv As New DataView(dt, " EanType <> '" & EanType & "'", "", DataViewRowState.CurrentRows)
                        If dv.Count > 0 Then
                            dv.AllowDelete = True
                            For Each dr As DataRowView In dv
                                dr.Delete()
                            Next
                            dt.AcceptChanges()
                            If dt.Rows.Count <= 0 Then
                                ShowMessage(getValueByKey("CMR12"), "CMR12 - " & getValueByKey("CLAE04"))
                            End If
                            If dt.Rows.Count > 1 Then
                                Dim objEan As New frmNCommonView
                                objEan.SetData = dt
                                Array.Resize(objEan.ColumnName, dt.Columns.Count)
                                Dim i As Integer = 0
                                For Each col As DataColumn In dt.Columns
                                    If col.ColumnName <> "EAN" And col.ColumnName <> "ARTICLECODE" And col.ColumnName <> "SELLINGPRICE" Then
                                        objEan.ColumnName(i) = col.ColumnName
                                    End If
                                    i = i + 1
                                Next
                                objEan.ShowDialog()
                                Dim dtTemp As DataTable = dt.Clone()
                                dtTemp.ImportRow(objEan.GetResultRow)
                                dt.Clear()
                                dt = dtTemp
                            End If
                        End If
                    End If

                    'End If
                    'ShowEan = False
                End If
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    Ean = dt.Rows(0)("EAN").ToString()
                    strArticle = dt.Rows(0)("ArticleCode").ToString()
                End If
                'From Here Decimal Qty
                If clsDefaultConfiguration.AllowDecimalQty Then

                    Dim SrNo As Boolean = Convert.ToBoolean(dt.Rows(0)("ToleranceValue"))
                    Dim SrnoValue As Decimal = 0
                    If SrNo = True Then

                        If Weight Is Nothing OrElse Weight = String.Empty OrElse Weight = "" Then

                            Dim objHold As New frmSpecialPrompt("Enter Quantity")
                            objHold.ShowTextBox = True
                            objHold.AcceptButton = objHold.cmdOk
                            objHold.txtValue.NumericInput = True
                            objHold.AllowDecimal = True
                            objHold.IsNumeric = True
                            objHold.ReadWeightFromCOM = clsDefaultConfiguration.WeightScaleEnabled
                            objHold.COMPortName = clsDefaultConfiguration.Comport
                            objHold.ShowDialog()

                            If (objHold.GetResult() Is Nothing) Then
                                Exit Sub
                            End If
                            SrnoValue = objHold.GetResult()

                        Else
                            '' Change By Ketan In Case Of NOS And PIS Not Need To split Article Quantity
                            Dim result As String
                            Dim strUOM As String = dt.Rows(0)("UNITOFMEASURE").ToString()
                            If strUOM = "NOS" Then
                                result = Weight
                            Else
                                result = String.Format("{0}.{1}", Weight.Substring(0, clsDefaultConfiguration.WeightBarcodeWholeNOLength), Weight.Substring(clsDefaultConfiguration.WeightBarcodeWholeNOLength, clsDefaultConfiguration.WeightBarcodedecimalLength))
                            End If
                            'Dim result As String = String.Format("{0}.{1}", Weight.Substring(0, clsDefaultConfiguration.WeightBarcodeWholeNOLength), Weight.Substring(clsDefaultConfiguration.WeightBarcodeWholeNOLength, clsDefaultConfiguration.WeightBarcodedecimalLength))
                            SrnoValue = Decimal.Parse(result)
                            If Not SrnoValue > 0 Then
                                ShowMessage(getValueByKey("CM021"), "CM021 - " & getValueByKey("CLAE04"))
                                CtrlSalesPersons.CtrlTxtBox.Text = ""
                                'CtrlSalesPersons.CtrlTxtBox.Focus()
                                CtrlSalesPersons.AndroidSearchTextBox.Text = ""
                                CtrlSalesPersons.AndroidSearchTextBox.Focus()
                                Exit Sub
                            End If

                        End If
                    Else
                        If WeghingScaleBarcode Then
                            MessageBox.Show(getValueByKey("weighingscale001"))
                            CtrlSalesPersons.CtrlTxtBox.Text = ""
                            'CtrlSalesPersons.CtrlTxtBox.Focus()
                            CtrlSalesPersons.AndroidSearchTextBox.Text = ""
                            CtrlSalesPersons.AndroidSearchTextBox.Focus()
                            Exit Sub
                        End If


                    End If
                    If SrnoValue > 0 Then
                        'dt.Columns("Quantity").DataType = Type.GetType("SYSTEM.DECIMAL")
                        flag = 1
                        dt.Rows(0)("Quantity") = SrnoValue
                    Else
                        If _prescriptionArticleAmount <> "" AndAlso _prescriptionArticleAmount <> 0 Then
                            dt.Rows(0)("Quantity") = _prescriptionArticleAmount
                        End If

                    End If
                End If
                If clsDefaultConfiguration.WeightBarcodeRateApplicable Then
                    If SP <> String.Empty Then
                        Dim newsp As String = String.Format("{0}.{1}", SP.Substring(0, clsDefaultConfiguration.WeightBarcodeRateWholeNOLength), SP.Substring(clsDefaultConfiguration.WeightBarcodeRateWholeNOLength, clsDefaultConfiguration.WeightBarcodeRateDecimalLength))
                        Dim newPrice = Decimal.Parse(newsp)
                        dt.Rows(0)("SellingPrice") = newPrice
                    End If

                End If
                'End Decimal Qty               
                If dt.Rows(0)("FreezeSB") = True Then
                    dt = Nothing
                    ShowMessage(getValueByKey("BL100"), "BL100 - " & getValueByKey("CLAE04"))
                    'CtrlSalesPersons.CtrlTxtBox.Focus()
                    CtrlSalesPersons.AndroidSearchTextBox.Focus()
                    Exit Sub
                End If
                Dim Stock As Double = objCM.GetStocks(clsAdmin.SiteCode, Ean.Trim, strArticle.Trim, True, clsDefaultConfiguration.IsBatchManagementReq, dt.Rows(0)("BatchBarcode").ToString())
                If OnlineConnect = True AndAlso clsDefaultConfiguration.NegativeInventoryAllowed = False Then
                    If CDbl(Stock) <= 0 Then
                        ShowMessage(getValueByKey("CM017"), "CM017 - " & getValueByKey("CLAE04"))
                        'ShowMessage("Article out of Stock.", "Information")
                        dt = Nothing
                        CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                        'CtrlSalesPersons.CtrlTxtBox.Focus()
                        CtrlSalesPersons.AndroidSearchTextBox.Text = String.Empty
                        CtrlSalesPersons.AndroidSearchTextBox.Focus()
                        Exit Sub
                    ElseIf Stock <= dt.Rows(0)("Quantity") Then
                        ShowMessage(getValueByKey("CM017"), "CM017 - " & getValueByKey("CLAE04"))
                        'ShowMessage("Article out of Stock.", "Information")
                        dt = Nothing
                        CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                        'CtrlSalesPersons.CtrlTxtBox.Focus()
                        CtrlSalesPersons.AndroidSearchTextBox.Text = String.Empty
                        CtrlSalesPersons.AndroidSearchTextBox.Focus()
                    End If
                    Try
                        rbnLStatus.SmallImage = Spectrum.My.Resources.connected1.ToBitmap
                        rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus2")
                    Catch ex As Exception

                    End Try

                ElseIf OnlineConnect = False Then
                    Try
                        rbnLStatus.SmallImage = Spectrum.My.Resources.disconnected1.ToBitmap
                        rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
                    Catch ex As Exception

                    End Try
                End If

                'Dim multipleMrpItem As DataTable = objCM.GetMultipleMrpItem(clsAdmin.SiteCode, dt.Rows(0)("ArticleCode"))
                'If multipleMrpItem IsNot Nothing AndAlso multipleMrpItem.Rows.Count > 1 Then
                '    Dim objFrmMultiplePrice As New frmMulipleSellingPrice
                '    objFrmMultiplePrice.MultipleMrpItems = multipleMrpItem
                '    objFrmMultiplePrice.ShowDialog()
                '    If objFrmMultiplePrice.SelectedPrice IsNot Nothing Then
                '        dt.Rows(0)("SellingPrice") = objFrmMultiplePrice.SelectedPrice("SellingPrice")
                '    Else
                '        CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                '        CtrlSalesPersons.CtrlTxtBox.Select()
                '        CtrlSalesPersons.CtrlTxtBox.Focus()
                '        Exit Sub
                '    End If
                'End If

                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    strArticle = dt.Rows(0)("ArticleCode").ToString()
                    price = dt.Rows(0)("SellingPrice")
                    If openMrp = False Then
                        If dt.Rows.Count > 1 Then
                            Dim objPrice As New frmNCommonView
                            objPrice.SetData = dt
                            Array.Resize(objPrice.ColumnName, dt.Columns.Count)
                            Dim i As Integer = 0
                            For Each col As DataColumn In dt.Columns
                                If col.ColumnName <> "EAN" And col.ColumnName <> "SELLINGPRICE" Then
                                    objPrice.ColumnName(i) = col.ColumnName
                                End If
                                i = i + 1
                            Next
                            objPrice.ShowDialog()

                            For i = dt.Rows.Count - 1 To 1 Step -1
                                dt.Rows.RemoveAt(i)
                            Next

                            If Not objPrice.search Is Nothing Then
                                dt.Rows(0)("SellingPrice") = objPrice.search(8)
                            Else
                                dt.Rows(0)("SellingPrice") = 0
                            End If
                        End If

                        Dim sprice As Double = 0.0
                        sprice = dt.Rows(0)("SellingPrice")
                        price = dt.Rows(0)("SellingPrice")

                        'IsInclusiveTax = False
                        'Dim dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, dt.Rows(0)("ArticleCode"), "CMS", 1, Ean.Trim(), clsDefaultConfiguration.CSTTaxCode, False)

                        'If (dtTaxCalc IsNot Nothing AndAlso dtTaxCalc.Rows.Count > 0) Then
                        '    IsInclusiveTax = IIf(dtTaxCalc.Rows(0)("INCLUSIVE") IsNot DBNull.Value, dtTaxCalc.Rows(0)("INCLUSIVE"), False)
                        'End If

                        '  If IsInclusiveTax Then
                        Dim originalTaxAmt As Double
                        If objArticleCombo.CheckIfComboArticle(strArticle) Then
                            filterScanArticle = String.Format("Btype='S' AND EAN='{0}' AND SellingPrice = '{1}'", Ean.Trim(), sprice)
                        Else
                            filterScanArticle = String.Format("Btype='S' AND EAN='{0}'", Ean.Trim())
                        End If
                        Dim view As New DataView(dsMain.Tables("CASHMEMODTL"), filterScanArticle, "EAN", DataViewRowState.CurrentRows)
                        If view.Count = 0 Then
                            'CalculateTotalInclusiveTax(originalTaxAmt, dsMain.Tables("CASHMEMODTL").Rows.Count + 1, dt.Rows(0)("ArticleCode"), sprice, 1, dt.Rows(0)("EAN"))
                            CreateDataSetForTaxCalculation(originalTaxAmt, dsMain.Tables("CASHMEMODTL").Rows.Count + 1, dt.Rows(0)("ArticleCode"), sprice, 1, 1, dt.Rows(0)("EAN"))
                            'sprice = Math.Round(sprice - originalTaxAmt, clsDefaultConfiguration.DecimalPlaces)
                            'sprice = sprice - originalTaxAmt
                        Else
                            'CalculateTotalInclusiveTax(originalTaxAmt, dgMainGrid.Rows(dgMainGrid.RowSel)("BillLineNo").ToString(), dt.Rows(0)("ArticleCode"), sprice * view(0)("Quantity"), view(0)("Quantity"), dt.Rows(0)("EAN"))
                            '  CreateDataSetForTaxCalculation(originalTaxAmt, view(0)("BillLineNo").ToString(), dt.Rows(0)("ArticleCode"), sprice * view(0)("Quantity"), view(0)("BillLineNo").ToString(), view(0)("Quantity"), view(0)("EAN").ToString())
                            'sprice = sprice - (originalTaxAmt / view(0)("Quantity"))
                            'code added  by vipul for Customer wise discount
                            If clsDefaultConfiguration.customerwisepricemanagement = True Then
                                CreateDataSetForTaxCalculation(originalTaxAmt, view(0)("BillLineNo").ToString(), dt.Rows(0)("ArticleCode"), (sprice * view(0)("Quantity")) - (view(0)("TOTALDISCOUNT")), view(0)("BillLineNo").ToString(), view(0)("Quantity"), view(0)("EAN").ToString())

                            Else
                                CreateDataSetForTaxCalculation(originalTaxAmt, view(0)("BillLineNo").ToString(), dt.Rows(0)("ArticleCode"), (sprice * view(0)("Quantity")), view(0)("BillLineNo").ToString(), view(0)("Quantity"), view(0)("EAN").ToString())
                            End If
                        End If

                        '   End If
                        'Dim dv As New DataView(dsMain.Tables("CASHMEMODTL"), "EAN='" & Ean.Trim() & "' AND SellingPrice=" & dt.Rows(0)("SellingPrice"), "EAN", DataViewRowState.CurrentRows)
                        Dim dv As DataView
                        If clsDefaultConfiguration.IsBatchManagementReq Then
                            dv = New DataView(dsMain.Tables("CASHMEMODTL"), "Btype='S' AND EAN='" & Ean.Trim() & "' AND SellingPrice='" & sprice & "' AND BatchBarcode='" & dt.Rows(0)("BatchBarcode") & "'", "EAN", DataViewRowState.CurrentRows)
                        Else
                            dv = New DataView(dsMain.Tables("CASHMEMODTL"), "Btype='S' AND EAN='" & Ean.Trim() & "' AND SellingPrice='" & sprice & "'", "EAN", DataViewRowState.CurrentRows)
                        End If
                        'Dim dv As New DataView(dsMain.Tables("CASHMEMODTL"), "Btype='S' AND EAN='" & Ean.Trim() & "' AND SellingPrice='" & sprice & "'", "EAN", DataViewRowState.CurrentRows)
                        If dv.Count > 0 Then
                            dv.AllowEdit = True
                            For Each drView As DataRowView In dv
                                If clsDefaultConfiguration.NegativeInventoryAllowed = False AndAlso Not Stock >= drView("Quantity") + 1 Then
                                    ShowMessage(getValueByKey("CM055"), "CM055 - " & getValueByKey("CLAE04"))
                                    CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                                    Exit Sub
                                End If


                                Dim taxAmountToMultiply = drView("TOTALTAXAMOUNT") / drView("Quantity")
                                Dim prevQty = drView("Quantity")
                                If dt.Rows(0)("ToleranceValue") Is Nothing OrElse CBool(dt.Rows(0)("ToleranceValue")) = False Then
                                    '---Changed Code By Mahesh while Implementing Mettler Integration Qty is pick from Bill ...
                                    If clsDefaultConfiguration.IsBillScanApplicable AndAlso ScanBill Then
                                        If dtScanedBillArticle IsNot Nothing AndAlso dtScanedBillArticle.Rows.Count > 0 Then
                                            drView("Quantity") = drView("Quantity") + dtScanedBillArticle.Rows(ScanItemBillSequence)("Quantity")
                                        Else
                                            drView("Quantity") = drView("Quantity") + 1
                                        End If
                                    Else
                                        drView("Quantity") = drView("Quantity") + 1
                                    End If
                                Else
                                    drView("Quantity") += dt.Rows(0)("Quantity")
                                End If


                                '--price = drView("GrossAmt")
                                price = drView("SELLINGPRICE") * drView("Quantity")
                                drView("GrossAmt") = price
                                'If cbManualDisc.Enabled = True AndAlso cbManualDisc.SelectedIndex > -1 Then
                                If cbManualDisc.Enabled = True Then
                                    ApplyManualPromotion(drView("EAN").ToString, 0, drView("BatchBarcode"))
                                Else
                                    If clsDefaultConfiguration.IsMembership = False Then
                                        Dim discountAmount As Decimal = Decimal.Zero
                                        discountAmount = dsMain.Tables("CASHMEMODTL").Compute("Sum(TotalDiscount)", String.Empty)
                                        If clsDefaultConfiguration.customerwisepricemanagement = False Then
                                            If (discountAmount > 0) Then
                                                RemoveSelectedArticlePromotion()
                                            End If
                                        End If
                                       
                                    End If
                                End If

                                'CalculateManualPromo(drView("EAN").ToString)
                                'End If
                                drView("TOTALDISCOUNT") = IIf(drView("LineDiscount") Is DBNull.Value, 0.0, drView("LineDiscount")) + IIf(drView("CLPDIscount") Is DBNull.Value, 0.0, drView("CLPDIscount"))
                                drView("TOTALDISCOUNT") = Math.Round(drView("TOTALDISCOUNT"), clsDefaultConfiguration.DecimalPlaces)
                                'If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                                '    price = price - IIf(drView("TOTALDISCOUNT") Is DBNull.Value, 0, drView("TOTALDISCOUNT"))
                                'End If
                                'code added by vipul for customer wise discount
                                If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True And clsDefaultConfiguration.customerwisepricemanagement = False Then
                                    price = price - IIf(drView("TOTALDISCOUNT") Is DBNull.Value, 0, drView("TOTALDISCOUNT"))
                                End If
                                If clsDefaultConfiguration.ExclusiveTaxAfterDisc = False And clsDefaultConfiguration.customerwisepricemanagement = True Then
                                    price = price - IIf(drView("TOTALDISCOUNT") Is DBNull.Value, 0, drView("TOTALDISCOUNT") * drView("Quantity"))
                                End If
                                'byram CreateDataSetForTaxCalculation(txtSearch.Text.Trim(), price, drView.Row, drView("EAN").ToString())
                                If drView("TOTALDISCOUNT") > 0 Then

                                    CreateDataSetForTaxCalculation(0, drView("BillLineNo").ToString(), strArticle, price, drView.Row, drView("EAN").ToString())

                                Else
                                    drView("TOTALTAXAMOUNT") = taxAmountToMultiply * drView("Quantity")
                                    Dim dvTax As New DataView(dtMainTax, "BillLineNo='" & drView("BillLineNo").ToString() & "' AND ArticleCode='" & drView("ArticleCode").ToString() & "'", "StepNo", DataViewRowState.CurrentRows)
                                    For Each tax In dvTax
                                        tax("TaxAmount") = (tax("TaxAmount") / prevQty) * drView("Quantity")
                                    Next
                                End If
                                ReCalculateCM(Ean.Trim())
                                calculateTotalbill()
                                'ShowLastOper(drView("EAN").ToString, drView("Discription").ToString, drView("Quantity"))
                                ProductImage.ShowArticleImage(strArticle)
                            Next
                            CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                            CtrlSalesPersons.AndroidSearchTextBox.Text = String.Empty
                            'CtrlSalesPersons.CtrlTxtBox.Focus()
                            CtrlSalesPersons.AndroidSearchTextBox.Focus()

                            If (dgMainGrid.Rows.Count > 1) Then
                                For index = 1 To dgMainGrid.Rows.Count - 1

                                    If (dgMainGrid.Rows.Item(index)("BillLineNo") = dv(0)("BillLineNo")) Then
                                        dgMainGrid.Select(index, 2)
                                    End If
                                Next
                            End If

                            Exit Sub
                        End If
                        If CDbl(dt.Rows(0)("SellingPrice")) = 0 Then
                            ShowMessage(getValueByKey("CM018"), "CM018 - " & getValueByKey("CLAE04"))
                            'ShowMessage("Article is Removing As no Price found on it.", "Information")
                            dt = Nothing
                        End If
                    ElseIf openMrp = True Then
                        Dim objPrompt As New frmSpecialPrompt(getValueByKey("SP002"))
                        objPrompt.ShowMessage = False
                        objPrompt.ShowTextBox = True
                        objPrompt.AllowDecimal = True
                        objPrompt.IsNumeric = True
                        objPrompt.txtValue.MaxLength = 14
                        objPrompt.ShowDialog()
                        price = objPrompt.GetResult()
                        objPrompt.Dispose()
                        If CDbl(price) = 0 Then
                            ShowMessage(getValueByKey("CM018"), "CM018 - " & getValueByKey("CLAE04"))
                            'ShowMessage("Article is Removing As no Price found on it.", "Information")
                            dt = Nothing
                        ElseIf CDbl(price) > 0 Then
                            dt.Rows(0)("SellingPrice") = CDbl(price)
                        End If
                    End If
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim dr As DataRow = dt.Rows(0)
                        If flag <> 1 Then
                            If clsDefaultConfiguration.EnableScanQRCode = False Then
                                dr("Quantity") = 1
                            End If
                        Else
                            If _prescriptionArticleAmount <> "" AndAlso _prescriptionArticleAmount <> 0 Then
                                dr("Quantity") = _prescriptionArticleAmount
                            End If

                        End If
                        Dim taxAmtWOCst As Double
                        dr("GrossAmt") = price
                        If clsDefaultConfiguration.EnableScanQRCode = True Then
                            price = price * dt.Rows(0)("Quantity")
                        End If
                        If dt.Rows(0)("ToleranceValue") Is Nothing OrElse CBool(dt.Rows(0)("ToleranceValue")) = False Then
                            tax = CreateDataSetForTaxCalculation(taxAmtWOCst, dsMain.Tables("CASHMEMODTL").Rows.Count + 1, strArticle, price, dr, dr("EAN").ToString())
                        Else
                            tax = CreateDataSetForTaxCalculation(taxAmtWOCst, dsMain.Tables("CASHMEMODTL").Rows.Count + 1, strArticle, price * dr("Quantity"), dr, dr("EAN").ToString())
                        End If

                        If IsInclusiveTax Then
                            If CBool(dt.Rows(0)("ToleranceValue")) Then
                                dt.Rows(0)("SellingPrice") = dt.Rows(0)("SellingPrice") - GetTaxableAmountForCst(strArticle, dr("EAN").ToString(), 1, dt.Rows(0)("SellingPrice"))
                            Else
                                dt.Rows(0)("SellingPrice") = dt.Rows(0)("SellingPrice") - taxAmtWOCst
                            End If


                        End If
                        If clsDefaultConfiguration.ArticleTaxAllowed = False AndAlso tax Is Nothing Then
                            ShowMessage(getValueByKey("CM019"), "CM019 - " & getValueByKey("CLAE04"))
                            'ShowMessage("Article is Removing As no Tax found on it.", "Information")
                            dt = Nothing
                        Else
                            If tax Is Nothing Then tax = 0
                        End If
                    End If
                End If
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim sprice As Double = 0
                    sprice = dt.Rows(0)("SellingPrice")
                    'Dim dv As New DataView(dsMain.Tables("CASHMEMODTL"), "EAN='" & Ean.Trim() & "' AND SellingPrice=" & dt.Rows(0)("SellingPrice"), "EAN", DataViewRowState.CurrentRows)
                    Dim dv As DataView
                    If clsDefaultConfiguration.IsBatchManagementReq Then
                        dv = New DataView(dsMain.Tables("CASHMEMODTL"), "Btype='S' AND EAN='" & Ean.Trim() & "' AND SellingPrice=" & ConvertToEnglish(sprice) & " AND BatchBarcode='" & dt.Rows(0)("BatchBarcode") & "'", "EAN", DataViewRowState.CurrentRows)
                    Else
                        dv = New DataView(dsMain.Tables("CASHMEMODTL"), "Btype='S' AND EAN='" & Ean.Trim() & "' AND SellingPrice=" & ConvertToEnglish(sprice), "EAN", DataViewRowState.CurrentRows)
                    End If

                    If dv.Count > 0 Then
                        dv.AllowEdit = True
                        For Each drView As DataRowView In dv
                            drView("Quantity") = drView("Quantity") + 1
                            price = drView("GrossAmt")
                            'If cbManualDisc.Enabled = True AndAlso cbManualDisc.SelectedIndex > -1 Then
                            'CalculateManualPromo(drView("EAN").ToString)
                            If cbManualDisc.Enabled = True Then
                                ApplyManualPromotion(drView("EAN").ToString, 0, drView("BatchBarcode"))
                            Else
                                If clsDefaultConfiguration.IsMembership = False Then
                                    Dim discountAmount As Decimal = Decimal.Zero
                                    discountAmount = dsMain.Tables("CASHMEMODTL").Compute("Sum(TotalDiscount)", String.Empty)

                                    If (discountAmount > 0) Then
                                        RemoveSelectedArticlePromotion()
                                    End If
                                End If
                            End If

                            'End If
                            If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                                price = price - drView("TOTALDISCOUNT")
                            End If
                            CreateDataSetForTaxCalculation(0, drView("BilllineNo").ToString(), strArticle, price, drView.Row, drView("EAN").ToString())

                            ReCalculateCM(Ean.Trim())
                            calculateTotalbill()
                            'ShowLastOper(drView("EAN").ToString, drView("Discription").ToString, drView("Quantity"))
                            ProductImage.ShowArticleImage(strArticle)
                            DisplayText("Name: " & drView("Discription") & "  Price  " & drView("SellingPrice"))
                        Next
                        CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                        'CtrlSalesPersons.CtrlTxtBox.Focus()
                        'CtrlSalesPersons.CtrlTxtBox.Select()
                        CtrlSalesPersons.AndroidSearchTextBox.Text = String.Empty
                        CtrlSalesPersons.AndroidSearchTextBox.Focus()
                        CtrlSalesPersons.AndroidSearchTextBox.Select()
                        Exit Sub
                    Else
                        If flag <> 1 Then
                            If _prescriptionArticleAmount <> "" AndAlso _prescriptionArticleAmount <> 0 Then
                                ' dt.Rows(0)("Quantity") = _prescriptionArticleAmount
                                If clsDefaultConfiguration.EnableScanQRCode = False Then
                                    dt.Rows(0)("Quantity") = _prescriptionArticleAmount
                                End If
                            Else
                                If clsDefaultConfiguration.EnableScanQRCode = False Then
                                    dt.Rows(0)("Quantity") = 1
                                End If
                            End If
                        End If

                        dsMain.Tables("CASHMEMODTL").Merge(dt, False, MissingSchemaAction.Ignore)
                        dsMain.Tables("CASHMEMODTL").Rows(dsMain.Tables("CASHMEMODTL").Rows.Count - 1)("BillLineNO") = dsMain.Tables("CASHMEMODTL").Rows.Count
                        dsMain.Tables("CASHMEMODTL").Rows(dsMain.Tables("CASHMEMODTL").Rows.Count - 1)("TOTALDISCPERCENTAGE") = Decimal.Zero

                        If clsDefaultConfiguration.IsBillScanApplicable AndAlso ScanBill Then
                            If dtScanedBillArticle IsNot Nothing AndAlso dtScanedBillArticle.Rows.Count > 0 Then
                                dsMain.Tables("CASHMEMODTL").Rows(dsMain.Tables("CASHMEMODTL").Rows.Count - 1)("BILLNO") = ScaleBillNo
                            End If
                        End If

                        GridSettings(UpdateFlag)
                        If cbManualDisc.Enabled = True Then
                            ApplyManualPromotion(dsMain.Tables("CASHMEMODTL").Rows(dsMain.Tables("CASHMEMODTL").Rows.Count - 1)("EAN").ToString, 0, dsMain.Tables("CASHMEMODTL").Rows(dsMain.Tables("CASHMEMODTL").Rows.Count - 1)("BatchBarcode").ToString)
                        Else
                            If clsDefaultConfiguration.IsMembership = False Then
                                Dim discountAmount As Decimal = Decimal.Zero
                                discountAmount = dsMain.Tables("CASHMEMODTL").Compute("Sum(TotalDiscount)", String.Empty)
                                If clsDefaultConfiguration.customerwisepricemanagement = False Then
                                    If (discountAmount > 0) Then
                                        RemoveSelectedArticlePromotion()
                                    End If
                                End If
                               
                            End If
                        End If

                        If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                            Dim dr As DataRow = dsMain.Tables("CASHMEMODTL").Rows(dsMain.Tables("CASHMEMODTL").Rows.Count - 1)
                            Dim Linedisc = IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT"))
                            price = dr("GrossAmt") - IIf(dr("LINEDISCOUNT") Is DBNull.Value, 0, dr("LINEDISCOUNT"))
                            If Linedisc > 0 Then
                                CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode").ToString(), price, dr, dr("EAN").ToString())
                            End If
                        End If

                        ReCalculateCM(Ean.Trim())
                        calculateTotalbill()
                        If clsDefaultConfiguration.IsPoleDisply = True Then
                            Dim selectedrow As Integer = dgMainGrid.RowSel - 1
                            TwoLineExtendScreen(selectedrow)
                        End If
                        'ShowLastOper(dt.Rows(0)("EAN").ToString, dt.Rows(0)("Discription").ToString, 1)
                        ProductImage.ShowArticleImage(strArticle)
                        DisplayText("Name: " & dt.Rows(0)("Discription") & "  Price  " & dt.Rows(0)("SellingPrice"))
                        If clsDefaultConfiguration.IsBillScanApplicable AndAlso ScanBill Then
                            If dtScanedBillArticle IsNot Nothing AndAlso dtScanedBillArticle.Rows.Count > 0 Then
                                If Val(dtScanedBillArticle.Rows(ScanItemBillSequence)("Quantity")) > 0 Then
                                    '----Code Added By Mahesh : Change Qty apply after calculate manual promotion n others things then change Qty
                                    Dim dvArticle As DataView
                                    If clsDefaultConfiguration.IsBatchManagementReq Then
                                        dvArticle = New DataView(dsMain.Tables("CASHMEMODTL"), "EAN='" & dsMain.Tables("CASHMEMODTL").Rows(dsMain.Tables("CASHMEMODTL").Rows.Count - 1)("EAN").ToString & "' AND BTYPE='S' And BatchBarcode='" & dsMain.Tables("CASHMEMODTL").Rows(dsMain.Tables("CASHMEMODTL").Rows.Count - 1)("BatchBarcode").ToString & "'", "Ean", DataViewRowState.CurrentRows)
                                    Else
                                        dvArticle = New DataView(dsMain.Tables("CASHMEMODTL"), "EAN='" & dsMain.Tables("CASHMEMODTL").Rows(dsMain.Tables("CASHMEMODTL").Rows.Count - 1)("EAN").ToString & "' AND BTYPE='S'", "Ean", DataViewRowState.CurrentRows)
                                    End If
                                    Dim drMain As DataRow = dvArticle.Item(0).Row
                                    drMain("Quantity") = dtScanedBillArticle.Rows(ScanItemBillSequence)("Quantity")
                                    Dim index As Int32 = dgMainGrid.Cols("Quantity").Index
                                    Dim lastRowIndex As Int32 = dgMainGrid.Rows.Count - 1

                                    Dim TotalAmt As Double = dgMainGrid.Rows(lastRowIndex)("Quantity") * dgMainGrid.Rows(lastRowIndex)("SellingPrice")
                                    dgMainGrid.Rows(lastRowIndex)("GrossAmt") = TotalAmt
                                    dgMainGrid.Rows(lastRowIndex)("LineDiscount") = dgMainGrid.Rows(lastRowIndex)("Quantity") * IIf(dgMainGrid.Rows(lastRowIndex)("LineDiscount") Is DBNull.Value, 0, dgMainGrid.Rows(lastRowIndex)("LineDiscount"))
                                    dgMainGrid.Rows(lastRowIndex)("TotalDiscount") = IIf(dgMainGrid.Rows(lastRowIndex)("LineDiscount") Is DBNull.Value, 0, dgMainGrid.Rows(lastRowIndex)("LineDiscount")) + IIf(dgMainGrid.Rows(lastRowIndex)("CLPDiscount") Is DBNull.Value, 0, dgMainGrid.Rows(lastRowIndex)("CLPDiscount"))
                                    If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                                        TotalAmt = TotalAmt - IIf(dgMainGrid.Rows(lastRowIndex)("TotalDiscount") Is DBNull.Value, 0, dgMainGrid.Rows(lastRowIndex)("TotalDiscount"))
                                    End If

                                    If dgMainGrid.Rows(lastRowIndex)("TotalDiscount") > 0 Then
                                        CreateDataSetForTaxCalculation(0, dgMainGrid.Rows(lastRowIndex)("BillLineNo").ToString(), dgMainGrid.Rows(lastRowIndex)("Articlecode").ToString(), TotalAmt, lastRowIndex, dgMainGrid.Rows(lastRowIndex)("Quantity"), dgMainGrid.Rows(lastRowIndex)("Ean").ToString())
                                    Else
                                        If _iArticleQtyBeforeChange > 0 Then
                                            dgMainGrid.Rows(lastRowIndex)("TOTALTAXAMOUNT") = (IIf(dgMainGrid.Rows(lastRowIndex)("TOTALTAXAMOUNT") IsNot DBNull.Value, dgMainGrid.Rows(lastRowIndex)("TOTALTAXAMOUNT"), 0) / _iArticleQtyBeforeChange) * dgMainGrid.Rows(lastRowIndex)("Quantity")
                                            Dim dvTax As New DataView(dtMainTax, "BillLineNo='" & dgMainGrid.Rows(lastRowIndex)("BillLineNo").ToString() & "' AND ArticleCode='" & dgMainGrid.Rows(lastRowIndex)("Articlecode").ToString() & "'", "StepNo", DataViewRowState.CurrentRows)
                                            For Each tax In dvTax
                                                tax("TaxAmount") = (tax("TaxAmount") / _iArticleQtyBeforeChange) * dgMainGrid.Rows(lastRowIndex)("Quantity")
                                            Next
                                        End If
                                    End If
                                    ReCalculateCM(dgMainGrid.Rows(lastRowIndex)("EAN").ToString())
                                    calculateTotalbill()
                                End If
                            End If
                        End If
                    End If
                End If
                If clsDefaultConfiguration.IsMembership Then
                    '-----lee spa
                    Dim obj As New clsApplyPromotion
                    obj.DecimalDigits = clsDefaultConfiguration.DecimalPlaces
                    'obj.IsInclusiveTax = IsInclusiveTax
                    obj.MainTable = "CASHMEMODTL"
                    obj.ExclusiveTaxFieldName = "EXCLUSIVETAX"
                    obj.TotalDiscountField = "TOTALDISCOUNT"
                    obj.GrossAmtField = "GROSSAMT"
                    obj.Condition = "BTYPE='S'"
                    Dim servicearticle = objclsMemb.GetServices()
                    If dtMembData IsNot Nothing Then
                        If dtMembData.Rows.Count > 0 Then

                            If Not membershipmaparticle = "" Then
                                If membershipmaparticle = dtMembData.Rows(0)("ServiceCode").ToString() Then
                                    Dim mainpromoId = dtMembDatapromo.Select("IsMainPromo=True")
                                    obj.CalculatePromotionsByCustomer(dsMain, clsAdmin.SiteCode, mainpromoId(0)("PromotionId"))
                                Else
                                    If servicearticle.Rows.Count > 0 Then
                                        Dim result = servicearticle.Select("articlecode='" + membershipmaparticle + "' or Ean='" + membershipmaparticle + "'")
                                        If result.Length > 0 Then
                                            Dim mainpromoId = dtMembDatapromo.Select("IsMainPromo=False")
                                            obj.CalculatePromotionsByCustomer(dsMain, clsAdmin.SiteCode, mainpromoId(0)("PromotionId"))
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If


                    ReCalculateCM("")
                    calculateTotalbill()
                End If
                CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                CtrlSalesPersons.AndroidSearchTextBox.Text = String.Empty
                'CtrlSalesPersons.CtrlTxtBox.Focus()
                'CtrlSalesPersons.CtrlTxtBox.Select()   
                If clsDefaultConfiguration.IsMembership Then
                    CustInfo.CtrlTxtSwape.Focus()
                    CustInfo.CtrlTxtSwape.Select()
                Else
                    CtrlSalesPersons.AndroidSearchTextBox.Focus()
                    CtrlSalesPersons.AndroidSearchTextBox.Select()
                End If
                If (dgMainGrid.Rows.Count > 1) Then
                    dgMainGrid.Select(dgMainGrid.Rows.Count - 1, 2)
                End If
            End If
            If clsDefaultConfiguration.customerwisepricemanagement = True Then
                If Not String.IsNullOrEmpty(strArticle) And Not String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim) Then
                    cmdDefaultPromo_Click(cmdDefaultPromo, New System.EventArgs, strArticle)
                End If
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            'MsgBox(ex.InnerException.Message)
            'MsgBox(ex.Message)
            LogException(ex)
            'ShowMessage("Error in Searcing of Item Details", "Error") 
        Finally
            Cursor.Current = Cursors.Default
            ' CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
            'CtrlSalesPersons.CtrlTxtBox.Focus()
        End Try

    End Sub

    Private Sub dgMainGrid_AfterDataRefresh(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles dgMainGrid.AfterDataRefresh
        Try
            If dgMainGrid.Rows.Count > 1 Then
                'cmdHold.Text = "Hold " & "Ctrl+H"
                cmdHold.Text = getValueByKey("frmcashmemo.cmdhold")
                cmdHold.Tag = "Hold"
                SetButtons(1, True)
                If clsDefaultConfiguration.IsGVSellAllowedWithOtherArticle = False AndAlso CheckIfGVItemScanned(True) = False Then
                    SetButtons(3, False)
                End If

                If cmdHold.Tag.ToString().ToUpper.Equals("RESUME") Then
                    cmdHold.Enabled = False
                Else
                    cmdHold.Enabled = True
                End If
            Else
                'cmdHold.Text = "Resume " & "Ctrl+H"
                cmdHold.Text = getValueByKey("frmcashmemo.cmdresume")
                cmdHold.Tag = "Resume"
                SetButtons(1, False)
                If clsDefaultConfiguration.IsGVSellAllowedWithOtherArticle = False Then
                    SetButtons(3, True)
                End If

                If (objCM.CheckHoldBillData(clsAdmin.SiteCode)) Then
                    cmdHold.Enabled = True
                Else
                    cmdHold.Enabled = False
                End If

            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                cmdHold.LargeImage = My.Resources.HoldNew
                cmdHold.Text = cmdHold.Text.ToUpper
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdCustomerSearchAndLoad(ByVal saleType As String)
        Try
            Dim objCust As New frmSearchCustomer
            objCust.FormName = saleType
            objCust.ShowDialog()

            Dim dt As DataTable
            Dim objCustm As New clsCLPCustomer
            Dim dtCust As DataTable = objCust.dtCustmInfo()

            'Dim type As String = objCust.AddressType
            If Not dtCust Is Nothing AndAlso dtCust.Rows.Count > 0 Then
                AssignCustomerInfoData(dtCust, objCustm)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub


    Private Function CheckIfGVItemScanned(Optional ByVal AvoidGiftVoucherMessage As Boolean = False) As Boolean
        Dim result As Boolean
        For Each dr In dsMain.Tables("CashMemoDtl").Rows
            If dr("ArticleCode") = "GVBaseArticle" Then
                result = True
                Exit For
            End If
        Next


        Return result

    End Function
    Private Sub dgMainGrid_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgMainGrid.AfterEdit
        Try
            If exitNowFlag = True Then Exit Sub
            If dgMainGrid.Rows.Count <= 1 Then Exit Sub
            If dgMainGrid.Rows.Count = e.Row Then Exit Sub

            Dim lastRowIndex As Integer = 0

            If (IsChangeQuantityOrPrice) Then
                lastRowIndex = e.Row 'dgMainGrid.Rows.Count - 1
            Else
                lastRowIndex = e.Row
            End If

            '---- Code Added by Mahesh after edit TakeAwayQty this event should not call ..just check take away qty not more than total qty 
            If dgMainGrid.Cols(e.Col).Name.ToUpper.Equals("TAKEAWAYQUANTITY") Then
                If dgMainGrid.Rows(e.Row)("BTYPE") = "S" AndAlso dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") <= 0 Then
                    ShowMessage(getValueByKey("CM021"), "CM021 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Please Delete the Item if not Required", "Information")
                    dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") = 0
                    Exit Sub
                End If
                If dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") <> Nothing AndAlso dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") > 1 Then
                    If dgMainGrid.Rows(e.Row)("Quantity") <> Nothing AndAlso dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") > dgMainGrid.Rows(e.Row)("Quantity") Then
                        ShowMessage("Take Away Quantity must be less than Total Quantity", getValueByKey("CLAE04"))
                        dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") = 0
                    End If
                End If
                Exit Sub
            End If

            If dgMainGrid.Rows(lastRowIndex)("BTYPE") = "S" AndAlso dgMainGrid.Rows(lastRowIndex)("Quantity") <= 0 Then
                ShowMessage(getValueByKey("CM021"), "CM021 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please Delete the Item if not Required", "Information")
                dgMainGrid.Rows(lastRowIndex)("Quantity") = 1
            End If

            '---- Validation Add By Mahesh Now TAKEAWAYQUANTITY can not be greater than QUANTITY ...
            If dgMainGrid.Cols("TAKEAWAYQUANTITY").Visible Then
                If dgMainGrid.Cols(e.Col).Name.ToUpper <> "TAKEAWAYQUANTITY" AndAlso Not IsDBNull(dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY")) Then
                    If dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") <> Nothing AndAlso dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") > 1 Then
                        If dgMainGrid.Rows(e.Row)("Quantity") <> Nothing AndAlso dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") > dgMainGrid.Rows(e.Row)("Quantity") Then
                            ShowMessage("Take Away Quantity cannot be greater than Total Quantity", getValueByKey("CLAE04"))
                            dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") = 0
                        End If
                    End If
                End If
            End If

            If dgMainGrid.Rows(lastRowIndex)("BTYPE") = "S" Then

                'Rakesh:01.11.2013:8265-> Avoid quantity change of voucher
                If dgMainGrid.Cols(e.Col).Name.ToUpper.Equals("QUANTITY") Then
                    If (dgMainGrid.Rows(lastRowIndex)("ArticleCode").ToString.ToUpper.Equals("GVBASEARTICLE")) Then
                        ShowMessage(getValueByKey("GVS05"), "GVS05 - " & getValueByKey("CLAE04"))
                        dgMainGrid.Rows(lastRowIndex)("Quantity") = 1
                        Exit Sub
                    End If

                    Dim discountAmount As Decimal = Decimal.Zero
                    discountAmount = dsMain.Tables("CASHMEMODTL").Compute("Sum(TotalDiscount)", String.Empty)
                    If clsDefaultConfiguration.IsMembership Then discountAmount = Decimal.Zero
                    If (discountAmount > 0 AndAlso IsChangeQuantityOrPrice = False) Then
                        Dim newArticleQuantity = dgMainGrid.Rows(lastRowIndex)("Quantity")
                        dgMainGrid.Rows(lastRowIndex)("Quantity") = _iArticleQtyBeforeChange
                        If clsDefaultConfiguration.customerwisepricemanagement = False Then
                            If MsgBox(getValueByKey("CM064"), MsgBoxStyle.Information, "CM064") = MsgBoxResult.Ok Then
                                cmdClrAllPromo_Click("ClearPromWithoutMessage", EventArgs.Empty)
                            End If
                        End If
                      

                        dgMainGrid.Rows(lastRowIndex)("Quantity") = newArticleQuantity
                    End If

                End If

                Dim articleStockQty As Decimal = 0.0
                If (dgMainGrid.Rows(lastRowIndex)("ArticleCode").Equals("GVBaseArticle") AndAlso dgMainGrid.Cols(e.Col).Name.Equals("CLPRequire")) Then
                    articleStockQty = 1
                Else
                    articleStockQty = IIf(dgMainGrid.Rows(lastRowIndex)("STOCK") Is DBNull.Value, 0, dgMainGrid.Rows(lastRowIndex)("STOCK"))
                End If

                If clsDefaultConfiguration.NegativeInventoryAllowed = False AndAlso dgMainGrid.Rows(lastRowIndex)("Quantity") > articleStockQty Then
                    ShowMessage(getValueByKey("CM055"), "CM055 - " & getValueByKey("CLAE04"))
                    dgMainGrid.Rows(lastRowIndex)("Quantity") = 1
                End If

                Dim TotalAmt As Double = dgMainGrid.Rows(lastRowIndex)("Quantity") * dgMainGrid.Rows(lastRowIndex)("SellingPrice")
                dgMainGrid.Rows(lastRowIndex)("GrossAmt") = TotalAmt

                If dgMainGrid.Rows(lastRowIndex)("Articlecode") <> "GVBaseArticle" And dgMainGrid.Rows(lastRowIndex)("Articlecode") <> "CLPBaseArticle" Then

                    'If _iArticleQtyBeforeChange < dgMainGrid.Rows(lastRowIndex)("Quantity") Then
                    '    If cbManualDisc.Enabled = True Then
                    '        ApplyManualPromotion(dgMainGrid.Rows(lastRowIndex)("EAN").ToString(), _iArticleQtyBeforeChange, dgMainGrid.Rows(lastRowIndex)("BatchBarcode").ToString())
                    '    Else
                    '        If Not IsDBNull(dgMainGrid.Rows(lastRowIndex)("FirstLevel")) AndAlso Not String.IsNullOrEmpty(dgMainGrid.Rows(lastRowIndex)("FirstLevel")) Then
                    '            RemoveSelectedArticlePromotion()
                    '        End If
                    '    End If
                    '    'CalculateManualPromo(dgMainGrid.Rows(lastRowIndex)("EAN").ToString())
                    'ElseIf _iArticleQtyBeforeChange > dgMainGrid.Rows(lastRowIndex)("Quantity") Then

                    '    Dim discountAmount As Decimal = Decimal.Zero
                    '    discountAmount = dsMain.Tables("CASHMEMODTL").Compute("Sum(TotalDiscount)", String.Empty)

                    '    If (discountAmount > 0) Then
                    '        If MsgBox(getValueByKey("CM060"), MsgBoxStyle.YesNo, "CM060") = MsgBoxResult.Yes Then

                    '            cmdClrAllPromo_Click("ClearPromWithoutMessage", EventArgs.Empty)

                    '            'dgMainGrid.Rows(lastRowIndex)("TotalDiscount") = 0
                    '            'dgMainGrid.Rows(lastRowIndex)("LineDiscount") = 0
                    '            'dgMainGrid.Rows(lastRowIndex)("TOTALDISCPERCENTAGE") = 0
                    '            'dgMainGrid.Rows(lastRowIndex)("FIRSTLEVEL") = String.Empty
                    '            'dgMainGrid.Rows(lastRowIndex)("TOPLEVEL") = String.Empty
                    '            'dgMainGrid.Rows(lastRowIndex)("MANUALPROMO") = 0
                    '            'dgMainGrid.Rows(lastRowIndex)("FirstLevelDisc") = 0
                    '            'dgMainGrid.Rows(lastRowIndex)("TotalDiscount") = 0
                    '        Else
                    '            dgMainGrid.Rows(lastRowIndex)("Quantity") = _iArticleQtyBeforeChange
                    '        End If
                    '    End If

                    'End If
                    If _PriceBeforeChange IsNot Nothing AndAlso _PriceBeforeChange <> dgMainGrid.Rows(lastRowIndex)("SellingPrice") Then
                        'CreateDataSetForTaxCalculation(0, dgMainGrid.Rows(lastRowIndex)("BillLineNo").ToString(), dgMainGrid.Rows(lastRowIndex)("ArticleCode"), dgMainGrid.Rows(lastRowIndex)("GrossAmt") - IIf(dgMainGrid.Rows(lastRowIndex)("TOTALDISCOUNT") Is DBNull.Value, 0, dgMainGrid.Rows(lastRowIndex)("TOTALDISCOUNT")), lastRowIndex, dgMainGrid.Rows(lastRowIndex)("Quantity"), dgMainGrid.Rows(lastRowIndex)("EAN"))
                        'code added  by vipul for Customer wise price
                        If clsDefaultConfiguration.customerwisepricemanagement = True Then
                            CreateDataSetForTaxCalculation(0, dgMainGrid.Rows(e.Row)("BillLineNo").ToString(), dgMainGrid.Rows(e.Row)("ArticleCode"), dgMainGrid.Rows(e.Row)("GrossAmt") - IIf(dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") Is DBNull.Value, 0, (dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") * dgMainGrid.Rows(e.Row)("GrossAmt")) / 100), e.Row, dgMainGrid.Rows(e.Row)("Quantity"), dgMainGrid.Rows(e.Row)("EAN"))
                        Else
                            CreateDataSetForTaxCalculation(0, dgMainGrid.Rows(e.Row)("BillLineNo").ToString(), dgMainGrid.Rows(e.Row)("ArticleCode"), dgMainGrid.Rows(e.Row)("GrossAmt") - IIf(dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") Is DBNull.Value, 0, dgMainGrid.Rows(e.Row)("TOTALDISCOUNT")), e.Row, dgMainGrid.Rows(e.Row)("Quantity"), dgMainGrid.Rows(e.Row)("EAN"))
                        End If
                    End If
                End If
                dgMainGrid.Rows(lastRowIndex)("TotalDiscount") = IIf(dgMainGrid.Rows(lastRowIndex)("LineDiscount") Is DBNull.Value, 0, dgMainGrid.Rows(lastRowIndex)("LineDiscount")) + IIf(dgMainGrid.Rows(lastRowIndex)("CLPDiscount") Is DBNull.Value, 0, dgMainGrid.Rows(lastRowIndex)("CLPDiscount"))
                'If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                '    TotalAmt = TotalAmt - IIf(dgMainGrid.Rows(lastRowIndex)("TotalDiscount") Is DBNull.Value, 0, dgMainGrid.Rows(lastRowIndex)("TotalDiscount"))
                'End If
                'code added by vipul for customer wise discount
                If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True And clsDefaultConfiguration.customerwisepricemanagement = False Then
                    TotalAmt = TotalAmt - IIf(dgMainGrid.Rows(e.Row)("TotalDiscount") Is DBNull.Value, 0, dgMainGrid.Rows(e.Row)("TotalDiscount"))
                End If
                If clsDefaultConfiguration.customerwisepricemanagement = True Then
                    TotalAmt = TotalAmt - IIf(dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, (dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") * dgMainGrid.Rows(e.Row)("GROSSAMT")) / 100)
                End If

                If dgMainGrid.Rows(lastRowIndex)("TotalDiscount") > 0 Then
                    CreateDataSetForTaxCalculation(0, dgMainGrid.Rows(lastRowIndex)("BillLineNo").ToString(), dgMainGrid.Rows(lastRowIndex)("Articlecode").ToString(), TotalAmt, lastRowIndex, dgMainGrid.Rows(lastRowIndex)("Quantity"), dgMainGrid.Rows(lastRowIndex)("Ean").ToString())
                Else
                    dgMainGrid.Rows(lastRowIndex)("TOTALTAXAMOUNT") = (IIf(dgMainGrid.Rows(lastRowIndex)("TOTALTAXAMOUNT") IsNot DBNull.Value, dgMainGrid.Rows(lastRowIndex)("TOTALTAXAMOUNT"), 0) / _iArticleQtyBeforeChange) * dgMainGrid.Rows(lastRowIndex)("Quantity")
                    Dim dvTax As New DataView(dtMainTax, "BillLineNo='" & dgMainGrid.Rows(lastRowIndex)("BillLineNo").ToString() & "' AND ArticleCode='" & dgMainGrid.Rows(lastRowIndex)("Articlecode").ToString() & "'", "StepNo", DataViewRowState.CurrentRows)
                    For Each tax In dvTax
                        tax("TaxAmount") = (tax("TaxAmount") / _iArticleQtyBeforeChange) * dgMainGrid.Rows(lastRowIndex)("Quantity")
                    Next
                End If
                'CreateDataSetForTaxCalculation(dgMainGrid.Rows(lastRowIndex)("BillLineNo").ToString(), dgMainGrid.Rows(lastRowIndex)("Articlecode").ToString(), TotalAmt, lastRowIndex, dgMainGrid.Rows(lastRowIndex)("Quantity"), dgMainGrid.Rows(lastRowIndex)("Ean").ToString())
                ReCalculateCM(dgMainGrid.Rows(lastRowIndex)("EAN").ToString())
            End If
            calculateTotalbill()
            If clsDefaultConfiguration.IsPoleDisply = True Then
                TwoLineExtendScreen(e.Row - 1)
            End If
            If (dgMainGrid.Rows.Count > 1) Then
                dgMainGrid.Select(dgMainGrid.Rows.Count - 1, 2)
            End If

            CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
            'CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Text = String.Empty
            CtrlSalesPersons.AndroidSearchTextBox.Select()

        Catch ex As Exception
            ShowMessage(getValueByKey("CM022"), "CM022 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Calulation not properly happen", "Error")
        End Try
    End Sub
    Dim IsArticleWiseKot As Boolean = False
    Dim IsCounterCopy As Boolean = False
    Dim IsFinalReceipt As Boolean = False
    Public Function cmdSavePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) As Boolean
        Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()

        Try
            If clsDefaultConfiguration.PrintFormatNo = "3" OrElse clsDefaultConfiguration.PrintFormatNo = "4" OrElse clsDefaultConfiguration.PrintFormatNo = "5" OrElse clsDefaultConfiguration.PrintFormatNo = "6" OrElse clsDefaultConfiguration.PrintFormatNo = "7" OrElse clsDefaultConfiguration.PrintFormatNo = "8" OrElse clsDefaultConfiguration.PrintFormatNo = "9" OrElse clsDefaultConfiguration.PrintFormatNo = "11" Then
                If clsDefaultConfiguration.IsArticleWiseKOT.Contains(clsAdmin.TerminalID) Then
                    IsArticleWiseKot = True
                Else
                    IsArticleWiseKot = False
                End If
                If clsDefaultConfiguration.IsCounterCopy.Contains(clsAdmin.TerminalID) Then
                    IsCounterCopy = True
                Else
                    IsCounterCopy = False
                End If
                If clsDefaultConfiguration.IsFinalReceipt.Contains(clsAdmin.TerminalID) Then
                    IsFinalReceipt = True
                Else
                    IsFinalReceipt = False
                End If
            End If
            Cursor.Current = Cursors.WaitCursor
            Dim StrError As String = ""
            Dim billNo As String
            Dim dsTemp As New DataSet
            Dim dtval As Boolean = False
            If clsDefaultConfiguration.WhetherBillPrintisRequiredornot AndAlso clsDefaultConfiguration.PrintPreivewReq = False AndAlso clsDefaultConfiguration.PrintFormatNo <> "3" Then
                Dim dlg As DialogResult = MessageBox.Show("Dou you want to Print the Invoice on Printer?", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
                If dlg = Windows.Forms.DialogResult.Yes Then
                    dtval = True
                End If
                If dlg = Windows.Forms.DialogResult.No Then
                    dtval = False
                End If
                If dlg = Windows.Forms.DialogResult.Cancel Then
                    Exit Function
                End If
            Else
                dtval = True
            End If
            objCM.CashMemoResetonDayClose = clsDefaultConfiguration.CashMemoResetonDayClose
            If ValidateAll() = True Then
                dsTemp = dsMain.Copy()
                If UpdateFlag = True Then


                    'If CheckInterTransactionAuth("UpdateBill", dsMain.Tables("CashMemoHdr")) = False Then Exit Sub
                    Dim strReason As String
                    Dim dt As DataTable = objCM.GetReasons("CMS")
                    Dim objReason As New frmNCommonView

                    objReason.SetData = dt
                    Array.Resize(objReason.ColumnName, dt.Columns.Count)
                    objReason.ColumnName(0) = "TRNSEQUENCENAME"
                    objReason.ShowDialog()
                    If objReason.search Is Nothing Then Exit Function
                    If Not objReason.search Is Nothing Then
                        strReason = objReason.search(0).ToString()
                    End If
                    dsTemp.Tables("CASHMEMOHDR").Rows(0)("AuthUserRemarks") = strReason
                    billNo = lblCMNo.Text
                    Dim dtTempGv As DataTable
                    If Not dtGV Is Nothing Then
                        dtTempGv = dtGV.Copy()
                    End If

                    'If objCM.SaveCashMemo(clsAdmin.DayOpenDate, OnlineConnect, dsTemp, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.UserCode, UpdateFlag, billNo, StrError, clsAdmin.Financialyear, clsDefaultConfiguration.CashMemoStorageLocation, "CMS") Then
                    'Added by Rohit for Cr-5938
                    objCM.dDueDate = _dDueDate
                    objCM.strRemarks = _strRemarks
                    objCM.CashMemoResetonDayClose = clsDefaultConfiguration.CashMemoResetonDayClose
                    If objCM.SaveCashMemo(clsAdmin.DayOpenDate, OnlineConnect, dsTemp, Nothing, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.UserCode, UpdateFlag, billNo, StrError, clsAdmin.Financialyear, clsDefaultConfiguration.StockStorageLocation, "CMS", dtTempGv, clsAdmin.ClpArticle, CLPCustomerId, clsAdmin.CLPProgram, "", clsAdmin.CVProgram, clsAdmin.CreditValidDays, , , FloatAmt, clsDefaultConfiguration.UpdateBillTime, clsDefaultConfiguration.IsMembership, UpdateStockAtStoreLevel:=clsDefaultConfiguration.UpdateStockAtStoreLevel) Then
                        Dim objPrint As New clsCashMemoPrint(billNo, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving) '0000413
                        objPrint.DisplayArticleFullName = clsDefaultConfiguration.PrintItemFullName
                        objPrint.AllowDecimalQty = clsDefaultConfiguration.AllowDecimalQty
                        objPrint.WeightScaleEnabled = clsDefaultConfiguration.WeightScaleEnabled

                        objPrint.TokenNoRequiredInKOT = clsDefaultConfiguration.TokenNoRequiredInKOT
                        objPrint.CashMemoResetonDayClose = clsDefaultConfiguration.CashMemoResetonDayClose
                        objPrint.AllowBillingOnlyAfterSelectionOfSalesType = clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType
                        objPrint.PrintFormatNo = clsDefaultConfiguration.PrintFormatNo
                        objPrint.KOTPrintFormatNo = clsDefaultConfiguration.KOTPrintFormatNo
                        objPrint.IsKotFontBold = clsDefaultConfiguration.IsKotFontBold
                        objPrint.IsKotFontLarge = clsDefaultConfiguration.IsKotFontLarge
                        objPrint.IsRoundRequired = clsDefaultConfiguration.RoundOffRequired
                        objPrint.MettlerConnString = clsDefaultConfiguration.MettlerConnString
                        objPrint.IsKotPrintRequired = clsDefaultConfiguration.KOTPrintRequired
                        objPrint.IsInvoicePrintRequired = dtval
                        objPrint.IsInvoicePrintFlag = clsDefaultConfiguration.WhetherBillPrintisRequiredornot
                        'added by vipin
                        objPrint.UserID = clsAdmin.UserCode
                        objPrint.EnableMailReSend = clsDefaultConfiguration.EnableMailReSend
                        objPrint.DocumentType = "CM"
                        objPrint.Terminalid = clsAdmin.TerminalID
                        objPrint.DisplayBrandWiseSale = clsDefaultConfiguration.DisplayBrandWiseSale 'vipin 10.09.2018 
                        'Select Case CustomerSaleType
                        '    Case enumCustomerSaleType.Dine_In
                        '        objPrint.CustomerSaleType = "Dine In"
                        '    Case enumCustomerSaleType.Home_Delivery
                        '        objPrint.CustomerSaleType = "Home Delivery"
                        '    Case enumCustomerSaleType.Take_Away
                        '        objPrint.CustomerSaleType = "Take Away"
                        '    Case Else
                        'End Select

                        Dim ErrorMsg As String = ""

                        ' ''--- Code Added By Mahesh for delivery Person Name in edit Mode
                        'Dim deliveryPersonName As String = String.Empty
                        'If (dsTemp.Tables("CashMemoHdr").Rows.Count > 0 AndAlso EditMode_IsupdateDeliveryPersonAllowed) Then
                        '    deliveryPersonName = dsTemp.Tables("CashMemoHdr").Rows(0)("DeliveryPersonID").ToString()
                        '    objPrint.DeliveryPersonName = deliveryPersonName
                        'End If
                        If (clsDefaultConfiguration.TemplatePrintingAllowed) Then
                            clsCashMemo.dsCashMemoPrinting = Nothing
                            objPrint.PrintTemplateCashMemoBillDetails(billNo, clsAdmin.SiteCode, clsAdmin.CurrencyCode, String.Empty, Nothing, IsBillReprint:=True, ReprintReason:="")
                        Else
                            If clsDefaultConfiguration.IsMembership Then
                                If Not String.IsNullOrEmpty(Membershipid) Then
                                    CashMemoPrintforMemberShip(billNo, clsDefaultConfiguration.ClientName, clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "", "", "", ErrorMsg, GiftMsg, _billAmt, _paidAmt, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy)
                                Else
                                    ' objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Modified", "", "", ErrorMsg, _billAmt, _paidAmt, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt) '0000413
                                    ' objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Modified", "", "", ErrorMsg, "", _billAmt, _paidAmt, False, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6) '0000413
                                    'objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Modified", "", "", ErrorMsg, "", _billAmt, _paidAmt, False, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7) '0000413   code added by irfan on 9/8/2017 aginast tender visiblity
                                    objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Modified", "", "", ErrorMsg, "", _billAmt, _paidAmt, False, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6, JKPrintFormatEnable:=clsDefaultConfiguration.JKPrintFormatEnable) '0000413  code added by irfan against tender visblity    code added by irfan on 11/09/2017 visiblity of hsn and tax) '0000413

                                End If
                            Else
                                ' objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Modified", "", "", ErrorMsg, _billAmt, _paidAmt, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt) '0000413
                                '  objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Modified", "", "", ErrorMsg, "", _billAmt, _paidAmt, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7) '0000413    code added by irfan on 9/8/2017 aginast tender visiblity
                                'modified by khusrao adil on 8-12-2017 for jk sprint 32
                                ' JKPrintFormatEnable flag added
                                objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Modified", "", "", ErrorMsg, "", _billAmt, _paidAmt, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6, JKPrintFormatEnable:=clsDefaultConfiguration.JKPrintFormatEnable) '0000413  code added by irfan against tender visblity    code added by irfan on 11/09/2017 visiblity of hsn and tax) '0000413
                            End If
                        End If

                        Dim TotalPay As Double
                        Dim clsVoucher As New clsPrintVoucher
                        If Not dtTempGv Is Nothing AndAlso dtTempGv.Rows.Count > 0 Then
                            Dim dv As New DataView(dtTempGv, "", "VOURCHERSERIALNBR", DataViewRowState.CurrentRows)
                            If dv.Count > 0 Then

                                For Each dr As DataRowView In dv
                                    'objPrint.PrintGiftVoucher(dr("VOURCHERSERIALNBR").ToString(), dr("ValueOfVoucher").ToString(), CDate(IIf(dr("ExpiryDate") Is DBNull.Value, Now, dr("ExpiryDate"))), DateDiff(DateInterval.Day, dr("ExpiryDate"), dr("issuedondate")))
                                    clsVoucher.PrintGiftVoucherAndCreditNote("CMS", clsAdmin.SiteCode, "GiftVoucher", dr("VOURCHERSERIALNBR"), dr("ValueOfVoucher"), CDate(IIf(dr("ExpiryDate") Is DBNull.Value, Now, dr("ExpiryDate"))), clsAdmin.UserName, dr("IssuedDocNumber"), clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                                Next
                            End If
                        End If
                        For Each dr As DataRow In dsTemp.Tables("CashMemoReceipt").Select("TenderTypeCode='CreditVouc(I)'", "", DataViewRowState.CurrentRows)
                            TotalPay = IIf(dr("AmountTendered") > 0, dr("AmountTendered"), dr("AmountTendered") * -1)
                            'objPrint.PrintVoucher("CMS", TotalPay, clsDefaultConfiguration.VoucherText, clsAdmin.SiteCode, Errormsg, clsAdmin.CurrencyCode)
                            clsVoucher.PrintGiftVoucherAndCreditNote("CMS", clsAdmin.SiteCode, "CreditNote", String.Empty, TotalPay, String.Empty, clsAdmin.UserName, billNo, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                        Next
                        If ErrorMsg <> String.Empty Then
                            ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
                        End If
                        ClearData()
                        OpenCashDrawer(dsTemp)
                        cmdNew_Click(sender, e)
                        Return True
                    Else
                        ''added By ketan Sync Issue delete data from new table
                        'objCM.DeleteCashMemoTempTrans(billNo, clsAdmin.SiteCode)
                        ShowMessage(StrError, getValueByKey("CLAE05"))
                        Return False
                    End If
                Else
                    objCM.dDueDate = _dDueDate
                    objCM.strRemarks = _strRemarks
                    If PrepareDataforSave(dsTemp) Then
                        RemoveDeletedRow(dsTemp.Tables("CASHMEMODTL"))
                        CreatingLineNO(dsTemp, "CASHMEMODTL")
                        Dim dtTempGv As DataTable
                        If Not dtGV Is Nothing Then
                            dtTempGv = dtGV.Copy()
                        End If
                        Dim TotalPoints, RedemptionPoints As Double
                        Dim CLPRedemptionflag As Boolean = True
                        'If CLPCustomerId <> String.Empty Then
                        TotalPoints = IIf(dsTemp.Tables("CashMemoDtl").Compute("SUM(CLPPoints)", "") Is DBNull.Value, 0, dsTemp.Tables("CashMemoDtl").Compute("SUM(CLPPoints)", ""))
                        If TotalPoints = 0 Then
                            TotalPoints = IIf(dsTemp.Tables("CashMemoDtl").Compute("SUM(CLPDiscount)", "") Is DBNull.Value, 0, dsTemp.Tables("CashMemoDtl").Compute("SUM(CLPDiscount)", ""))
                            If TotalPoints > 0 Then
                                TotalPoints = Convert.ToDouble(CashSummary.CtrllblNetAmt.Text)
                            End If
                        End If
                        If TotalPoints <> 0 Then
                            CLPRedemptionflag = False

                        End If

                        'End If
                        If clsDefaultConfiguration.GiftVoucherReturnAllowed Then

                            objCM.GVBasedAricleReturnList = GVBasedAricleReturnList
                            objCM.GiftVoucherReturnAllowed = clsDefaultConfiguration.GiftVoucherReturnAllowed

                        End If

                        'Code Added By Irfan on 17/1/2017 For Mantis issue==============================================================
                        If CashSummary.lbltxtVisible5 = True Then
                            If dtCreditSaleData.Rows.Count > 0 Then
                                Dim i, j As Integer
                                For i = 0 To dtCreditSaleData.Rows.Count - 1
                                    For j = 0 To dsTemp.Tables("CASHMEMODtl").Rows.Count - 1
                                        If Not dsTemp.Tables("CASHMEMODtl").Rows(j)("BILLNO") Is DBNull.Value Then
                                            If dtCreditSaleData.Rows(i)("BillNo") = dsTemp.Tables("CASHMEMODtl").Rows(j)("BILLNO") Then
                                                dsTemp.Tables("CASHMEMODtl").Rows(j)("NETAMOUNT") = "0.0"
                                                dsTemp.Tables("CASHMEMODtl").Rows(j)("GROSSAMT") = "0.0"
                                                '  dsTemp.Tables("CASHMEMODtl").Rows(j)("STATUS") = False
                                            End If
                                        End If
                                    Next
                                Next
                            End If
                        End If

                        '=================================================================================================================
                        If objCM.SaveCashMemo(clsAdmin.DayOpenDate, OnlineConnect, dsTemp, Nothing, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.UserCode, UpdateFlag, billNo, StrError, clsAdmin.Financialyear, clsDefaultConfiguration.StockStorageLocation, "CMS", dtTempGv, clsAdmin.ClpArticle, CLPCustomerId, clsAdmin.CLPProgram, "", clsAdmin.CVProgram, clsAdmin.CreditValidDays, CLPRedemptionflag, RedemptionPoints, FloatAmt, clsDefaultConfiguration.UpdateBillTime, clsDefaultConfiguration.IsMembership, UpdateStockAtStoreLevel:=clsDefaultConfiguration.UpdateStockAtStoreLevel) Then
                            'If CLPCustomerId <> String.Empty Then
                            'Dim TotalPoints As Double = IIf(dsTemp.Tables("CashMemoDtl").Compute("SUM(CLPPoints)", "") Is DBNull.Value, 0, dsTemp.Tables("CashMemoDtl").Compute("SUM(CLPPoints)", ""))
                            'If TotalPoints <= 0 Then
                            '    TotalPoints = IIf(dsTemp.Tables("CashMemoDtl").Compute("SUM(CLPDiscount)", "") Is DBNull.Value, 0, dsTemp.Tables("CashMemoDtl").Compute("SUM(CLPDiscount)", ""))
                            '    If TotalPoints > 0 Then
                            '        TotalPoints = Convert.ToDouble(CashSummary.CtrllblNetAmt.Text)
                            '    End If
                            'End If
                            'If clsDefaultConfiguration.UpdateBillTime = True Then
                            '    objCM.UpdateBillTime(billNo, "")
                            'End If

                            If TotalPoints <> 0 Then
                                If objCM.SaveClpData(dsTemp, clsAdmin.CLPProgram, CLPCustomerId, TotalPoints, RedemptionPoints) = False Then
                                    ShowMessage(getValueByKey("CM049"), "CM049 - " & getValueByKey("CLAE04"))
                                End If
                            End If
                            'End If
                            Dim TotalPay As Double = CDbl(CashSummary.CtrllblNetAmt.Text)
                            '''Code ln 3330 Start Added BY Mahesh In case of AllowBillPrintForCreditSales false(Naturals) & Payment by Credit Sale for Home Delivery bill Print not required , instead a pop up Message “Cash Memo Created successfully ” will shown .
                            If Not clsDefaultConfiguration.AllowBillPrintForCreditSales _
                                    AndAlso dsTemp.Tables("CashMemoReceipt").Rows.Count = 1 _
                                    AndAlso CustomerSaleType = enumCustomerSaleType.Home_Delivery Then
                                '------Check payment only from Credit Sale ---
                                Dim dr() = dsTemp.Tables("CashMemoReceipt").Select("TenderTypeCode='Credit'")
                                If dr.Count > 0 Then
                                    ShowMessage(getValueByKey("CM072"), "CM072 - " & getValueByKey("CLAE04"))
                                    ClearData()
                                    clsCashMemo.dsCashMemoPrinting = Nothing
                                    cmdNew_Click(sender, e)
                                    Return True
                                End If
                            End If
                            '--- Code ln 3330 End
                            OpenCashDrawer(dsTemp)
                            ClearData()
                            'Dim objPrint As New clsPrinting(billNo)

                            Dim deliveryPersonName As String = String.Empty
                            If (dsTemp.Tables("CashMemoHdr").Rows.Count > 0) Then
                                deliveryPersonName = dsTemp.Tables("CashMemoHdr").Rows(0)("DeliveryPersonID").ToString()
                            End If

                            Dim objPrint As New clsCashMemoPrint(billNo, False, False, clsDefaultConfiguration.SalesPersonApplicable, (clsDefaultConfiguration.TaxDetailsRequired Or IsInclusiveTax), clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
                            objPrint.DisplayArticleFullName = clsDefaultConfiguration.PrintItemFullName
                            objPrint.AllowDecimalQty = clsDefaultConfiguration.AllowDecimalQty
                            objPrint.WeightScaleEnabled = clsDefaultConfiguration.WeightScaleEnabled

                            objPrint.KOTBillPrintingRequired = clsDefaultConfiguration.KOTPrintRequired
                            objPrint.CustomerNameRequiredInKOT = clsDefaultConfiguration.CustomerNameRequiredInKOT
                            objPrint.TokenNoRequiredInKOT = clsDefaultConfiguration.TokenNoRequiredInKOT
                            objPrint.CashMemoResetonDayClose = clsDefaultConfiguration.CashMemoResetonDayClose
                            objPrint.AllowBillingOnlyAfterSelectionOfSalesType = clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType
                            objPrint.PrintFormatNo = clsDefaultConfiguration.PrintFormatNo
                            objPrint.KOTPrintEachlineItem = clsDefaultConfiguration.IsKOTPrintEachlineItem
                            objPrint.KOTPrintForEachQuantity = clsDefaultConfiguration.IsKOTPrintQuantityWise
                            objPrint.KOTPrintFormatNo = clsDefaultConfiguration.KOTPrintFormatNo
                            objPrint.IsKotFontBold = clsDefaultConfiguration.IsKotFontBold
                            objPrint.IsKotFontLarge = clsDefaultConfiguration.IsKotFontLarge
                            objPrint.MettlerConnString = clsDefaultConfiguration.MettlerConnString
                            objPrint.IsRoundRequired = clsDefaultConfiguration.RoundOffRequired
                            objPrint.RoundOff = clsDefaultConfiguration.BillRoundOffAt
                            objPrint.IsKotPrintRequired = clsDefaultConfiguration.KOTPrintRequired
                            objPrint.IsInvoiceSendOnMailRequired = clsDefaultConfiguration.IsInvoiceSendOnMailRequired
                            objPrint.SendInvoiceSMS = clsDefaultConfiguration.SendInvoiceSMS
                            objPrint.IsInvoicePrintRequired = dtval
                            objPrint.IsInvoicePrintFlag = clsDefaultConfiguration.WhetherBillPrintisRequiredornot
                            'added by vipin
                            objPrint.UserID = clsAdmin.UserCode
                            objPrint.EnableMailReSend = clsDefaultConfiguration.EnableMailReSend
                            objPrint.DocumentType = "CM"
                            objPrint.Terminalid = clsAdmin.TerminalID
                            objPrint.DisplayBrandWiseSale = clsDefaultConfiguration.DisplayBrandWiseSale 'vipin 10.09.2018 
                            'objPrint.DeliveryPersonName = deliveryPersonName

                            'Select CustomerSaleType
                            '    Case enumCustomerSaleType.Dine_In
                            '        objPrint.CustomerSaleType = "Dine In"
                            '    Case enumCustomerSaleType.Home_Delivery
                            '        objPrint.CustomerSaleType = "Home Delivery"
                            '    Case enumCustomerSaleType.Take_Away
                            '        objPrint.CustomerSaleType = "Take Away"
                            '    Case Else
                            'End Select


                            Dim Errormsg As String = ""
                            Dim NoOFCopies As Integer = 1
                            If CustomerSaleType = 1 Then
                                NoOFCopies = clsDefaultConfiguration.NoOfCopiesforDineIn
                            ElseIf CustomerSaleType = 2 Then
                                NoOFCopies = clsDefaultConfiguration.NoOfCopiesforHomeDelivery
                            ElseIf CustomerSaleType = 3 Then
                                NoOFCopies = clsDefaultConfiguration.NoOfCopiesforTakeAway
                            Else
                                NoOFCopies = 1
                            End If
                            ''' objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMS", "", "", "", Errormsg, "", _billAmt, _paidAmt)

                            If (clsDefaultConfiguration.TemplatePrintingAllowed) Then
                                objPrint.PrintTemplateCashMemoBillDetails(billNo, clsAdmin.SiteCode, clsAdmin.CurrencyCode, String.Empty)
                            Else
                                If clsDefaultConfiguration.IsMembership Then
                                    If Not String.IsNullOrEmpty(Membershipid) Then
                                        CashMemoPrintforMemberShip(billNo, clsDefaultConfiguration.ClientName, clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMS", "", "", "", Errormsg, GiftMsg, _billAmt, _paidAmt, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy)
                                    Else
                                        '    objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMS", "", "", "", Errormsg, "", _billAmt, _paidAmt, Nothing, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt)
                                        'objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMS", "", "", "", Errormsg, "", _billAmt, _paidAmt, False, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsCounterCopyKot:=False, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6) '0000413   code added by irfan on 18/9/2017 visiblity of hsn and tax
                                        objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMS", "", "", "", Errormsg, "", _billAmt, _paidAmt, False, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsCounterCopyKot:=False, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6, JKPrintFormatEnable:=clsDefaultConfiguration.JKPrintFormatEnable) '0000413  code added by irfan against tender visblity    code added by irfan on 11/09/2017 visiblity of hsn and tax) '0000413
                                    End If
                                Else
                                    'objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMS", "", "", "", Errormsg, "", _billAmt, _paidAmt, Nothing, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt)
                                    'objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMS", "", "", "", Errormsg, "", _billAmt, _paidAmt, False, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsCounterCopyKot:=False, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, NoOFCopies:=NoOFCopies, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6) '0000413   code added by irfan on 18/9/2017 visiblity of hsn and tax
                                    objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMS", "", "", "", Errormsg, "", _billAmt, _paidAmt, False, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsCounterCopyKot:=False, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6, JKPrintFormatEnable:=clsDefaultConfiguration.JKPrintFormatEnable) '0000413  code added by irfan against tender visblity    code added by irfan on 11/09/2017 visiblity of hsn and tax) '0000413
                                End If

                            End If
                            ''code commented by vipul for call update stock in one transaction 
                            'If clsDefaultConfiguration.UpdateStockAtStoreLevel Then
                            '    If Not objCM.UpdateStockForArticle(clsAdmin.SiteCode, clsAdmin.UserCode, clsDefaultConfiguration.StockStorageLocation, billNo, clsAdmin.Financialyear, dsTemp, Nothing) Then
                            '        ActivityLogForShift(Nothing, "Update Stock Fail", "")
                            '    End If
                            'End If
                            'Transfer data to kds tables
                            Dim obj As New clsAuthorization
                            Dim DtKdsTran As DataTable = obj.GetSitedAllowedTran(clsAdmin.SiteCode, "KDS")
                            If Not DtKdsTran Is Nothing AndAlso DtKdsTran.Rows.Count > 0 Then
                                objCM.TransferKdsData(billNo, clsAdmin.SiteCode)
                            End If
                            _remarks = String.Empty
                            isCashierPromoSelected = False
                            If Errormsg <> String.Empty Then
                                ShowMessage(Errormsg, getValueByKey("CLAE05"))
                            End If
                            Dim clsVoucher As New clsPrintVoucher
                            If Not dtTempGv Is Nothing AndAlso dtTempGv.Rows.Count > 0 Then
                                Dim dv As New DataView(dtTempGv, "", "VOURCHERSERIALNBR", DataViewRowState.CurrentRows)
                                If dv.Count > 0 Then

                                    For Each dr As DataRowView In dv
                                        'objPrint.PrintGiftVoucher(dr("VOURCHERSERIALNBR").ToString(), dr("ValueOfVoucher").ToString(), CDate(IIf(dr("ExpiryDate") Is DBNull.Value, Now, dr("ExpiryDate"))), DateDiff(DateInterval.Day, dr("ExpiryDate"), dr("issuedondate")))
                                        clsVoucher.PrintGiftVoucherAndCreditNote("CMS", clsAdmin.SiteCode, "GiftVoucher", dr("VOURCHERSERIALNBR"), dr("ValueOfVoucher"), CDate(IIf(dr("ExpiryDate") Is DBNull.Value, Now, dr("ExpiryDate"))), clsAdmin.UserName, dr("IssuedDocNumber"), clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                                    Next
                                End If
                            End If
                            If (dsTemp.Tables("CashMemoReceipt") IsNot Nothing) Then
                                For Each dr As DataRow In dsTemp.Tables("CashMemoReceipt").Select("TenderTypeCode='CreditVouc(I)'", "", DataViewRowState.CurrentRows)
                                    TotalPay = IIf(dr("AmountTendered") > 0, dr("AmountTendered"), dr("AmountTendered") * -1)
                                    'objPrint.PrintVoucher("CMS", TotalPay, clsDefaultConfiguration.VoucherText, clsAdmin.SiteCode, Errormsg, clsAdmin.CurrencyCode)
                                    clsVoucher.PrintGiftVoucherAndCreditNote("CMS", clsAdmin.SiteCode, "CreditNote", String.Empty, TotalPay, String.Empty, clsAdmin.UserName, billNo, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                                Next
                            End If

                            Dim objPromo As New clsApplyPromotion
                            Dim PromoText As String = ""
                            objPromo.CheckForPromotionList(clsAdmin.SiteCode, TotalPay, PromoText, 1)
                            If PromoText <> String.Empty Then
                                fnPrint(PromoText, "PRN")
                            End If

                            If clsDefaultConfiguration.AskForMoreCustomerInfo Then
                                cmdCustomerinfo_Click(New Object(), New EventArgs())
                            End If

                            If clsDefaultConfiguration.CLPRegistration = True AndAlso TotalPay >= clsDefaultConfiguration.CLPRegistrationAmt Then
                                ShowMessage(getValueByKey("CM023"), "CM023 - " & getValueByKey("CLAE04"))
                                'ShowMessage("Customer is Eligible For CLP Registration", "Information")
                            End If

                            cmdNew_Click(sender, e)

                            'code added by irfan on 21/12/2017 for resume and hold icon========
                            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                                Me.cmdHold.LargeImage = Global.Spectrum.My.Resources._Resume
                                cmdHold.Text = cmdHold.Text.ToUpper
                            End If
                            '===================================================================
                            Return True
                        Else
                            ''added By ketan Sync Issue delete data from new table
                            'objCM.DeleteCashMemoTempTrans(billNo, clsAdmin.SiteCode)
                            ShowMessage(StrError, getValueByKey("CLAE05"))
                            Return False
                        End If
                    Else
                        Return False
                    End If
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CM024"), "CM024 - " & getValueByKey("CLAE05"))
            LogException(ex)
            Return False
            'ShowMessage("Error in Saving the Details", "Save Error")
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Function

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType AndAlso CustomerSaleType = 0 Then Exit Sub
            Cursor.Current = Cursors.WaitCursor

            If clsDefaultConfiguration.SalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0 Then
                ShowMessage(getValueByKey("CM025"), "CM025 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please Select the Sales Person", "information")

                Exit Sub
            End If

            If Not clsDefaultConfiguration.IsGVSellAllowedWithOtherArticle AndAlso CheckIfGVItemScanned() Then
                ShowMessage("Can not sell article with GV", getValueByKey("CLAE04"))
                CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                CtrlSalesPersons.AndroidSearchTextBox.Text = String.Empty
                'CtrlSalesPersons.CtrlTxtBox.Select()
                'CtrlSalesPersons.CtrlTxtBox.Focus()
                CtrlSalesPersons.AndroidSearchTextBox.Select()
                CtrlSalesPersons.AndroidSearchTextBox.Focus()
                Exit Sub
            End If

            Dim obj As New frmNItemSearch
            obj.ShowDialog()

            If Not obj.SearchResult Is Nothing Then
                'ShowEan = True
                Dim ean As String = String.Empty
                If clsDefaultConfiguration.IsBatchManagementReq Then
                    Dim _IsBarcodeNotAvailable, _IsBarcodeAvlbleQtyZero As Boolean
                    ean = SearchAvailableBarcodes(obj.ItemRow("ArticleCode").ToString(), _IsBarcodeNotAvailable, _IsBarcodeAvlbleQtyZero)
                    If String.IsNullOrEmpty(ean) Then
                        If _IsBarcodeAvlbleQtyZero Then
                            ShowMessage(getValueByKey("BL046"), getValueByKey("CLAE04"))
                        End If

                        If _IsBarcodeNotAvailable Then
                            ShowMessage(getValueByKey("barcodeerror"), getValueByKey("CLAE04"))
                        End If
                        Exit Sub
                    End If

                End If
                If String.IsNullOrEmpty(ean) Then
                    ean = obj.ItemRow("EAN").ToString()
                End If
                CtrlSalesPersons.CtrlTxtBox.Text = ean
                CtrlSalesPersons.CtrlTxtBox.Text = obj.ItemRow("ArticleCode").ToString()
                If (Not String.IsNullOrEmpty(CtrlSalesPersons.CtrlTxtBox.Text.Trim()) AndAlso dgMainGrid.Rows.Count >= 1) Then
                    txtSearch_KeyDown(ean, New KeyEventArgs(Keys.Enter))
                End If
                'txtSearch_KeyDown(ean, New KeyEventArgs(Keys.Enter))
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
        Cursor.Current = Cursors.Default
    End Sub

    Private Function SearchAvailableBarcodes(ByRef articleCode As String, ByRef IsBarcodeNotAvailable As Boolean, ByRef IsBarcodeAvlbleQtyZero As Boolean) As String
        Dim barCode As String = String.Empty
        Dim objFrmBarcode As New frmSelectBarcode
        objFrmBarcode.ArticleCode = articleCode
        objFrmBarcode.ShowDialog()
        If objFrmBarcode.SelectedRow IsNot Nothing Then
            barCode = objFrmBarcode.SelectedRow.Cells("BatchBarcode").Value
        Else
            If objFrmBarcode.IsBarcodeNotAvailable Then
                IsBarcodeNotAvailable = True
            End If
            If objFrmBarcode.IsBarcodeAvlbleQtyZero Then
                IsBarcodeAvlbleQtyZero = True
            End If
        End If
        Return barCode
    End Function

    Private Sub cmdCustomerinfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCustomerinfo.Click
        Try
            If OnlineConnect = False Then
                ShowMessage(getValueByKey("CMO50"), "CMO50 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If

            'Dim objCust As New frmNSearchCustomer
            'objCust.ShowSO = False
            'objCust.IsCustSearch = True
            'objCust.BtnSearchCustomer.Enabled = True
            'objCust.txtCustomerCode.ReadOnly = False
            'objCust.ShowDialog()

            'Dim dtCust As DataTable = objCust.dtCustmInfo()
            ''---Added dtHD By Mahesh For Home Delivery ...
            'dtHD = objCust.dtCustmInfo()

            Dim dtCust As New DataTable
            Dim type As String
            Dim custType As String
            'If clsDefaultConfiguration.DetailedCustomerCreationformat = "0" Then
            '    Dim objCust As New frmNSearchCustomer
            '    objCust.ShowSO = False
            '    objCust.IsCustSearch = True
            '    objCust.BtnSearchCustomer.Enabled = True
            '    objCust.txtCustomerCode.ReadOnly = False
            '    objCust.ShowDialog()
            '    type = objCust.AddressType
            '    custType = objCust.CustmType

            '    dtCust = objCust.dtCustmInfo()
            '    dtHD = objCust.dtCustmInfo()
            'Else
            '    Dim objCust As New frmNewCustomer
            '    objCust.IsCustSearch = True
            '    objCust.ShowDialog()

            '    type = objCust.AddressType
            '    custType = objCust.CustmType

            '    dtCust = objCust.dtCustmInfo()
            '    dtHD = objCust.dtCustmInfo()
            'End If
            Dim objCust As New frmSearchCustomer
            Dim result As DialogResult = objCust.ShowDialog()
            If result = Windows.Forms.DialogResult.Yes Then
                dtPrescDtlCashMemoAddToBill.Clear()
                dtPrescDtlCashMemoAddToBill = objCust.dtPrescDtlSrchAddToBill.Copy
                ' BindPatientPrescriptionToMainGrid()
                If dtPrescDtlCashMemoAddToBill.Rows.Count > 0 Then

                    For index = 0 To dtPrescDtlCashMemoAddToBill.Rows.Count - 1
                        _prescriptionArticleAmount = "0"
                        CtrlSalesPersons.CtrlTxtBox.Text = dtPrescDtlCashMemoAddToBill.Rows(index)("ArticleCode").ToString()
                        _prescriptionArticleAmount = dtPrescDtlCashMemoAddToBill.Rows(index)("Qty").ToString()
                        txtSearch_KeyDown(CtrlSalesPersons.CtrlTxtBox, New System.Windows.Forms.KeyEventArgs(Keys.Enter))
                    Next

                End If


            End If
            type = objCust.AddressType
            custType = objCust.CustmType

            dtCust = objCust.dtCustmInfo()
            dtHD = objCust.dtCustmInfo()

            If dsMain.Tables("CASHMEMOHDR").Rows.Count > 0 Then
                If (dtCust.Rows(0)("CustomerType") = "CLP") Then
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("ClpNo") = dtCust.Rows(0)("CUSTOMERNO").ToString()
                Else
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("ClpNo") = dtCust.Rows(0)("CUSTOMERNO").ToString()
                End If
                'dsMain.Tables("CASHMEMOHDR").Rows(0)("HDDeliveryDate") = obj.DeliveryDate
                dsMain.Tables("CASHMEMOHDR").Rows(0)("HDName") = dtCust.Rows(0)("CustomerName").ToString()
                dsMain.Tables("CASHMEMOHDR").Rows(0)("HDAddress") = dtCust.Rows(0)("ADDRESSLN1").ToString() + " ;" + dtCust.Rows(0)("ADDRESSLN2").ToString() + " ;" + dtCust.Rows(0)("ADDRESSLN3").ToString() + " ;" + dtCust.Rows(0)("ADDRESSLN4").ToString()
                dsMain.Tables("CASHMEMOHDR").Rows(0)("HDEmail") = dtCust.Rows(0)("EMAILID").ToString()
                dsMain.Tables("CASHMEMOHDR").Rows(0)("HDTelNo") = dtCust.Rows(0)("MOBILENO").ToString()
            End If

            'Dim type As String = objCust.AddressType
            If Not dtCust Is Nothing AndAlso dtCust.Rows.Count > 0 Then
                Dim dv As New DataView(dtCust, IIf(type Is Nothing, "", "isnull(AddressType,'')='" & type & "'"), "", DataViewRowState.CurrentRows)
                If dv.Count > 0 Then

                    'CustSaleTypeTimer.Stop()
                    'lblCustSaleType.Visible = False
                    calculateTotalbill() 'aaded by prasad clear btn issue(clear payment detail also)
                   
                    Dim ObjclsCommon As New clsCommon
                    Dim da = ObjclsCommon.GetLastVisited(dv.Item(0)("CUSTOMERNO").ToString())
                    CustInfo.CtrlTxtCustomerNo.Text = dv.Item(0)("CUSTOMERNO").ToString()
                    CustInfo.CtrlLastVisit.Text = ObjclsCommon.GetLastVisited(dv.Item(0)("CUSTOMERNO")).Date
                    ' CustInfo.CtrltxtCustomerName.Text = dv.Item(0)("CustomerName").ToString()
                    If clsDefaultConfiguration.customerwisepricemanagement = True Then
                        If dv.Item(0)("Level").ToString().Equals("-") Then
                            CustInfo.CtrltxtCustomerName.Text = dv.Item(0)("CustomerName").ToString()
                        Else
                            CustInfo.CtrltxtCustomerName.Text = dv.Item(0)("CustomerName").ToString() + " - " + dv.Item(0)("Level").ToString()
                        End If
                    Else
                        CustInfo.CtrltxtCustomerName.Text = dv.Item(0)("CustomerName").ToString()
                    End If
                    ' lblCity.Text = dv.Item(0)("CITY")
                    'lblBalPoint.Text = dv.Item(0)("BalancePoint")
                    customerType = IIf((dtCust.Rows(0)("CustomerType") Is DBNull.Value), "", dtCust.Rows(0)("CustomerType"))
                    CustInfo.CtrlTxtSwape.Text = dv.Item(0)("CUSTOMERNO").ToString()
                    If (dtCust.Columns.Contains("BalancePoint")) Then
                        CustInfo.ctrlTxtPoints.Text = dv.Item(0)("BalancePoint").ToString()
                        CLPCardType = IIf((dv.Item(0)("CARDTYPE") Is DBNull.Value), "", dv.Item(0)("CARDTYPE"))
                    Else
                        CustInfo.ctrlTxtPoints.Text = String.Empty
                    End If

                    CLPCustomerId = dv.Item(0)("CUSTOMERNO").ToString()
                    'Dim objCustm As New clsCLPCustomer
                    'If clsDefaultConfiguration.IsCstTaxRequired AndAlso objCustm.CheckIfCstApplicable(clsAdmin.SiteCode, dv.Item(0)("STATE").ToString()) Then
                    '    DisplayCSTMessage()
                    'End If
                End If
            End If

            'CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Select()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub BindPatientPrescriptionToMainGrid()
        Try
            dgMainGrid.Rows.Count = 1
            For row = 0 To dtPrescDtlCashMemoAddToBill.Rows.Count - 1
                dgMainGrid.Rows.Add()
                Dim dtRow As Int32 = dtPrescDtlCashMemoAddToBill.Rows.Count - 1

                'dgMainGrid.Rows(dgMainGrid.Rows.Count - 1)("SrNo") = dtPrescDtlCashMemoAddToBill.Rows(row)("SrNo")
                dgMainGrid.Rows(dgMainGrid.Rows.Count - 1)("ArticleCode") = dtPrescDtlCashMemoAddToBill.Rows(row)("ArticleCode").ToString
                dgMainGrid.Rows(dgMainGrid.Rows.Count - 1)("DISCRIPTION") = dtPrescDtlCashMemoAddToBill.Rows(row)("ArticleDescription").ToString
                dgMainGrid.Rows(dgMainGrid.Rows.Count - 1)("Quantity") = dtPrescDtlCashMemoAddToBill.Rows(row)("Qty").ToString
                dgMainGrid.Rows(dgMainGrid.Rows.Count - 1)("BType") = "S"
            Next


            'dgMainGrid.Rows.Count = 1
            'For index = 0 To dtPrescDtlCashMemoAddToBill.Rows.Count - 1
            '    dgMainGrid.Rows.Add()
            '    '  dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.Selects) = ""
            '    ''''dgGridArticleToday.Rows(dgGridArticleToday.Rows.Count - 1)("SrNo") = dtPrescToday.Rows(index)("SrNo").ToString()
            '    dgMainGrid.Rows(dgMainGrid.Rows.Count - 1)("SrNo") = dtPrescDtlCashMemoAddToBill.Rows(index)("SrNo").ToString
            '    dgMainGrid.Rows(dgMainGrid.Rows.Count - 1)("ArticleCode") = dtPrescDtlCashMemoAddToBill.Rows(index)("ArticleCode").ToString()
            '    dgMainGrid.Rows(dgMainGrid.Rows.Count - 1)("ArticleDescription") = dtPrescDtlCashMemoAddToBill.Rows(index)("ArticleDescription")

            '    dgMainGrid.Rows(dgMainGrid.Rows.Count - 1)("Quantity") = dtPrescDtlCashMemoAddToBill.Rows(index)("Qty")


            '    ' dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.EAN) = dt.Rows(0)("Ean").ToString()
            '    If (dgMainGrid.Rows.Count > 1) Then
            '        dgMainGrid.Select(dgMainGrid.Rows.Count - 1, 2)
            '    End If
            'Next

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub clearCustomerInfo()
        CustInfo.CtrlTxtCustomerNo.Text = String.Empty
        CustInfo.CtrltxtCustomerName.Text = String.Empty
        CustInfo.ctrlTxtPoints.Text = String.Empty
        CustInfo.CtrlTxtSwape.Text = String.Empty
        CustInfo.CtrlLastVisit.Text = String.Empty
        CLPCardType = String.Empty
        CLPCustomerId = String.Empty
        If clsDefaultConfiguration.IsMembership Then
            CustInfo.CtrlTxtCustomerNo.ReadOnly = False
            CustInfo.CtrlTxtSwape.ReadOnly = False
        End If
        'CustomerSaleType = 0
        'IsCSTApplicable = False
        ' '---- Clear Home Delivery too ----Added By Mahesh 
        If dsMain.Tables("CASHMEMOHDR").Rows.Count > 0 Then
            dsMain.Tables("CASHMEMOHDR").Rows(0)("ClpNo") = DBNull.Value
            dsMain.Tables("CASHMEMOHDR").Rows(0)("HDDeliveryDate") = DBNull.Value
            dsMain.Tables("CASHMEMOHDR").Rows(0)("HDName") = DBNull.Value
            dsMain.Tables("CASHMEMOHDR").Rows(0)("HDAddress") = DBNull.Value
            dsMain.Tables("CASHMEMOHDR").Rows(0)("HDEmail") = DBNull.Value
            dsMain.Tables("CASHMEMOHDR").Rows(0)("HDTelNo") = DBNull.Value
            dsMain.Tables("CASHMEMOHDR").Rows(0)("HDRemark") = DBNull.Value
            dsMain.Tables("CASHMEMOHDR").Rows(0)("DeliveryPersonID") = DBNull.Value
            dsMain.Tables("CASHMEMOHDR").Rows(0)("SalesExecutiveCode") = DBNull.Value
            dsMain.Tables("CASHMEMOHDR").Rows(0)("HDName") = DBNull.Value
            'dsMain.Tables("CashMemoHdr").Rows(0)("ServiceTaxAmount") = 0

            'CustSaleTypeTimer.Stop()
            'lblCustSaleType.Visible = False
        End If
        If (dtHD IsNot Nothing AndAlso dtHD.Rows.Count > 0) Then dtHD.Clear()
        ReCalculateCM("")
        calculateTotalbill()
    End Sub

    Private Sub DisplayHomeDeliveryForm(ByRef dtCust As DataTable, Optional ByVal IsUsingCustSearch As Boolean = False)
        Try
            Dim obj As New frmNHomeDelivery
            Dim objCustm As New clsCLPCustomer

            If IsUsingCustSearch Then
                obj.btnClear.Visible = True
            End If

            customerType = dtCust.Rows(0)("CustomerType").ToString()

            obj.HdName = dtCust.Rows(0)("CustomerName").ToString()
            Dim deliveryAddress As DataTable = objCustm.CheckIfDeliveryAddressExist(dtCust.Rows(0)("CUSTOMERNO").ToString(), clsAdmin.SiteCode, clsAdmin.CLPProgram)
            If Not deliveryAddress Is Nothing AndAlso deliveryAddress.Rows.Count > 0 Then
                obj.Address1 = deliveryAddress.Rows(0)("ADDRESSLN1").ToString()
                obj.Address2 = deliveryAddress.Rows(0)("ADDRESSLN2").ToString()
                obj.Address3 = deliveryAddress.Rows(0)("ADDRESSLN3").ToString()
                obj.Address4 = deliveryAddress.Rows(0)("ADDRESSLN4").ToString()
            Else
                obj.Address1 = dtCust.Rows(0)("ADDRESSLN1").ToString()
                obj.Address2 = dtCust.Rows(0)("ADDRESSLN2").ToString()
                obj.Address3 = dtCust.Rows(0)("ADDRESSLN3").ToString()
                obj.Address4 = dtCust.Rows(0)("ADDRESSLN4").ToString()
            End If
            obj.MobileNo = dtCust.Rows(0)("MOBILENO").ToString()
            obj.Email = dtCust.Rows(0)("EMAILID").ToString()
            obj.TelNo = dtCust.Rows(0)("MOBILENO").ToString()
            obj.CustomerNo = dtCust.Rows(0)("CUSTOMERNO").ToString()
            obj.DeliveryPersonID = dsMain.Tables("CASHMEMOHDR").Rows(0)("DeliveryPersonID").ToString()
            HDMobileNo = obj.MobileNo
            Dim Billno As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("BillNO").ToString
            Dim BillDate As DateTime
            If (IsDBNull(dsMain.Tables("CASHMEMOHDR").Rows(0)("BillDate"))) Then
                BillDate = clsAdmin.DayOpenDate
            Else
                BillDate = dsMain.Tables("CASHMEMOHDR").Rows(0)("BillDate")
            End If
            If UpdateFlag = True Then
                Dim dsFloatAmt = objCM.GetVoucherFloatData(Billno, BillDate)
                obj.FloatAmt = IIf(dsFloatAmt.Tables("VoucherHdr").Rows(0)("TotalAmt") Is DBNull.Value, 0, dsFloatAmt.Tables("VoucherHdr").Rows(0)("TotalAmt"))
            Else
                obj.FloatAmt = FloatAmt
            End If

            If (obj.ShowDialog() = Windows.Forms.DialogResult.OK) Then

                AssignCustomerInfoData(dtCust, objCustm)

                'If Not obj.txtRemark.Text = String.Empty Then
                'Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
                If (dtCust.Rows(0)("CustomerType") = "CLP") Then
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("ClpNo") = dtCust.Rows(0)("CUSTOMERNO").ToString()
                Else
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("ClpNo") = dtCust.Rows(0)("CUSTOMERNO").ToString()
                End If

                dsMain.Tables("CASHMEMOHDR").Rows(0)("HDDeliveryDate") = obj.DeliveryDate
                dsMain.Tables("CASHMEMOHDR").Rows(0)("HDName") = obj.HdName
                dsMain.Tables("CASHMEMOHDR").Rows(0)("HDAddress") = obj.Address1 + " ;" + obj.Address2 + " ;" + obj.Address3 + " ;" + obj.Address4
                dsMain.Tables("CASHMEMOHDR").Rows(0)("HDEmail") = obj.Email
                dsMain.Tables("CASHMEMOHDR").Rows(0)("HDTelNo") = obj.TelNo
                dsMain.Tables("CASHMEMOHDR").Rows(0)("HDRemark") = obj.Remark
                FloatAmt = obj.FloatAmt
                dsMain.Tables("CASHMEMOHDR").Rows(0)("DeliveryPersonID") = obj.DeliveryPersonID
                dsMain.Tables("CASHMEMOHDR").Rows(0)("SalesExecutiveCode") = obj.DeliveryPersonID
                dsMain.Tables("CASHMEMOHDR").Rows(0)("DeliveryPartnerID") = obj.delieverypartnerid 'jayesh 06-09-2018 not saving deliverypartner
                If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType Then
                    dsMain.Tables("CashMemoHdr").Rows(0)("ServiceTaxAmount") = 2
                    CustomerSaleType = enumCustomerSaleType.Home_Delivery
                End If
                If obj.IsUpdateCustomerAddress And Not String.IsNullOrEmpty(obj.CustomerNo) Then
                    Dim objCLPCustm As New clsCLPCustomer
                    objCLPCustm.UpdateCustomerAddress(obj.CustomerNo, clsAdmin.CLPProgram, obj.Address1.ToString(), obj.Address2.ToString(),
                                                      obj.Address3.ToString(), obj.Address4.ToString(),
                                                      obj.MobileNo.ToString(), obj.TelNo.ToString(),
                                                      obj.Email.ToString(), clsDefaultConfiguration.AllowMobileNoEditable)
                End If
                dtCust = Nothing
                If Not String.IsNullOrEmpty(obj.HdName) Then
                    lblCustSaleType.Visible = True
                    CustSaleTypeTimer.Start()
                    HDPrintRequired = True
                    lblCustSaleType.Text = "Home Delivery"
                    CustomerSaleType = enumCustomerSaleType.Home_Delivery
                    Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
                    dsMain.Tables("CashMemoHdr").Rows(0)("ServiceTaxAmount") = CustomerSaleType
                    HDPrintRequired = True
                    If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
                        If dgMainGrid.Rows.Count > 1 Then
                            RecalculateTax = True
                            BillRecalculateTax()
                        End If
                    End If
                End If
            End If

            If IsUsingCustSearch And obj._IsCleared Then
                obj.Dispose()
                SearchCustomerForHomeDelivery()
            End If
            CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
            CtrlSalesPersons.CtrlTxtBox.Focus()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Sub AssignCustomerInfoData(ByVal dt As DataTable, ByRef objCustm As clsCLPCustomer)
        If Not dt Is Nothing And dt.Rows.Count > 0 Then
            Dim ObjclsCommon As New clsCommon
            Dim da = ObjclsCommon.GetLastVisited(dt.Rows(0)("CUSTOMERNO").ToString())
            CustInfo.CtrlTxtCustomerNo.Value = dt.Rows(0)("CUSTOMERNO").ToString() 'da.ToString("yyyy-mm-dd")
            CustInfo.CtrlTxtCustomerNo.Text = dt.Rows(0)("CUSTOMERNO").ToString()

            '  CustInfo.CtrltxtCustomerName.Text = dt.Rows(0)("CUSTOMERNAME").ToString()

            If clsDefaultConfiguration.customerwisepricemanagement = True Then
                If dt.Columns.Contains("Level") = True Then
                    If dt.Rows(0)("Level").ToString().Equals("-") Then
                        CustInfo.CtrltxtCustomerName.Text = dt.Rows(0)("CustomerName").ToString()
                    Else
                        CustInfo.CtrltxtCustomerName.Text = dt.Rows(0)("CustomerName").ToString() + " - " + dt.Rows(0)("Level").ToString()
                    End If
                Else
                    CustInfo.CtrltxtCustomerName.Text = dt.Rows(0)("CustomerName").ToString()
                End If
            Else
                CustInfo.CtrltxtCustomerName.Text = dt.Rows(0)("CustomerName").ToString()
            End If
            CustInfo.CtrlLastVisit.Text = ObjclsCommon.GetLastVisited(dt.Rows(0)("CUSTOMERNO")).Date

            If (dt.Rows(0)("CustomerType") = "CLP") Then
                CustInfo.CtrlTxtSwape.Text = dt.Rows(0)("AccountNo").ToString()
                CustInfo.ctrlTxtPoints.Text = dt.Rows(0)("BALANCEPOINT").ToString()
                CLPCardType = IIf((dt.Rows(0)("CARDTYPE") Is DBNull.Value), "", dt.Rows(0)("CARDTYPE"))
                CustomerBalancePoint = dt.Rows(0)("BalancePoint").ToString()

            Else
                CustInfo.ctrlTxtPoints.Clear()
                CustInfo.CtrlTxtSwape.Clear()
            End If
            customerType = dt.Rows(0)("CustomerType")
            clpCustomerMobileNo = IIf((dt.Rows(0)("Mobileno") Is DBNull.Value), "", dt.Rows(0)("Mobileno"))
            CLPCustomerId = dt.Rows(0)("CUSTOMERNO").ToString()
            ' CtrlSalesPersons.CtrlTxtBox.Focus()
            CtrlSalesPersons.AndroidSearchTextBox.Focus()
            If clsDefaultConfiguration.IsCstTaxRequired AndAlso dt.Rows(0)("STATE") IsNot DBNull.Value AndAlso objCustm.CheckIfCstApplicable(clsAdmin.SiteCode, dt.Rows(0)("STATE").ToString()) Then
                DisplayCSTMessage()
            End If
        Else
            CustInfo.CtrlTxtCustomerNo.Clear()
            CustInfo.CtrltxtCustomerName.Clear()
            CustInfo.ctrlTxtPoints.Clear()
            CustInfo.CtrlTxtSwape.Clear()
            CustInfo.CtrlTxtSwape.Focus()
            CustInfo.CtrlLastVisit.Clear()
            ' ShowMessage(getValueByKey("SC001"), "SC001 - " & getValueByKey("CLAE04"))
        End If
    End Sub


    Private Sub SearchCustomerForHomeDelivery()
        Try
            Dim objCust As New frmSearchCustomer
            objCust.FormName = "HOMEDELIVERY"
            objCust.ShowDialog()

            Dim dt As DataTable
            Dim objCustm As New clsCLPCustomer
            Dim dtCust As DataTable = objCust.dtCustmInfo()

            'Dim type As String = objCust.AddressType
            If Not dtCust Is Nothing AndAlso dtCust.Rows.Count > 0 Then
                DisplayHomeDeliveryForm(dtCust)

            ElseIf objCust.vCustomerNo <> String.Empty Then
                dt = objCustm.GetCustomerInformation("SO", clsAdmin.SiteCode, clsAdmin.CLPProgram, objCust.vCustomerNo)
                DisplayHomeDeliveryForm(dt)
            End If
            'CtrlSalesPersons.CtrlTxtBox.Select()
            'CtrlSalesPersons.CtrlTxtBox.Focus()
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType Then
                CtrlSalesPersons.AndroidSearchTextBox.Select()
                CtrlSalesPersons.AndroidSearchTextBox.Focus()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub



    Private Sub cmdReturns_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If OnlineConnect = False Then
                ShowMessage(getValueByKey("CMO50"), "CMO50 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            If CheckInterTransactionAuth("ExchangeBill", dsMain.Tables("CashMemoDtl")) = True Then
                Dim objReturn As New frmNCashMemoReturn
                objReturn.ShowDialog()
                Dim CustomerNo = objReturn.Customer
                Dim OriginalQty As Double
                If CustInfo.CtrlTxtCustomerNo.Text = Nothing Then
                    CustInfo.CtrlTxtCustomerNo.Text = CustomerNo
                End If
                If (Not String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim())) Then
                    Dim objCustm As New clsCLPCustomer()
                    Dim dtCustmInfo As DataTable
                    dtCustmInfo = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CustInfo.CtrlTxtCustomerNo.Text)
                    customerType = "CLP"

                    AssignCustomerInfoData(dtCustmInfo, objCustm)
                End If
                dt = objReturn.GetResultData()
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If dr("ArticleCode") = "GVBaseArticle" Then
                            Dim vouchercode As String = dt.Rows(0)("VourcherSerialNbr").ToString()
                            Dim billno As String = dt.Rows(0)("BILLNO").ToString()
                            Dim BillLineNo As Integer = dt.Rows(0)("BillLineNo").ToString()
                            clsCashMemo._IsBillLineNo = BillLineNo
                            If GVBasedAricleReturnList.Count = 0 Then
                                GVBasedAricleReturnList.Add(billno, vouchercode)
                            Else
                                ShowMessage(getValueByKey("CM080"), "CM080 - " & getValueByKey("CLAE04"))
                                Exit Sub
                            End If
                        End If

                        Dim dv As DataView
                        If clsDefaultConfiguration.IsBatchManagementReq Then
                            dv = New DataView(dsMain.Tables("CashMemoDtl"), "Btype='R' And Ean='" & dr("Ean").ToString() & "' AND ReturnCMNo='" & dr("ReturnCMNo").ToString() & "' AND BatchBarcode='" & dr("BatchBarcode").ToString() & "'", "", DataViewRowState.CurrentRows)
                        Else
                            dv = New DataView(dsMain.Tables("CashMemoDtl"), "Btype='R' And Ean='" & dr("Ean").ToString() & "' AND ReturnCMNo='" & dr("ReturnCMNo").ToString() & "'", "", DataViewRowState.CurrentRows)
                            '---added by sagar cash memo return with so
                            'If clsDefaultConfiguration.IsNewSalesOrder And dr("ReturnDocumenttype") = "SO" Then
                            '    dv = New DataView(dsMain.Tables("CashMemoDtl"), "Btype='R' And Ean='" & dr("Ean").ToString() & "' And LineNumber='" & dr("BillLineNo").ToString() & "' AND ReturnCMNo='" & dr("ReturnCMNo").ToString() & "'", "", DataViewRowState.CurrentRows)
                            'Else
                            '    dv = New DataView(dsMain.Tables("CashMemoDtl"), "Btype='R' And Ean='" & dr("Ean").ToString() & "' AND ReturnCMNo='" & dr("ReturnCMNo").ToString() & "'", "", DataViewRowState.CurrentRows)
                            'End If

                        End If
                        If IsDBNull(dr("OriginalQTY")) Then
                            OriginalQty = 0.0
                        Else
                            OriginalQty = Val(dr("OriginalQTY"))
                        End If
                        If dv.Count > 0 Then
                            If (OriginalQty = 0 Or OriginalQty >= Math.Abs(Val(dv.Item(0)("QUANTITY").ToString()) + Val(dr("QUANTITY").ToString()))) Then
                                dv.Item(0)("QUANTITY") = Val(dv.Item(0)("QUANTITY").ToString()) + Val(dr("QUANTITY").ToString())
                                dv.Item(0)("GROSSAMT") = Val(dv.Item(0)("GROSSAMT").ToString()) + Val(dr("GROSSAMT").ToString())
                                dv.Item(0)("TOTALDISCOUNT") = Val(dv.Item(0)("TOTALDISCOUNT").ToString()) + Val(dr("TOTALDISCOUNT").ToString())
                                dv.Item(0)("LINEDISCOUNT") = Val(dv.Item(0)("LINEDISCOUNT").ToString()) + Val(dr("LINEDISCOUNT").ToString())
                                'dv.Item(0)("TOTALDISCPERCENTAGE") = dr("TOTALDISCPERCENTAGE")
                                'dv.Item(0)("SELLINGPRICE") = dr("SELLINGPRICE")
                                dv.Item(0)("EXCLUSIVETAX") = Val(dv.Item(0)("EXCLUSIVETAX").ToString()) + Val(dr("EXCLUSIVETAX").ToString)
                                dv.Item(0)("NETAMOUNT") = Val(dv.Item(0)("NETAMOUNT").ToString()) + Val(dr("NETAMOUNT").ToString())
                                dv.Item(0)("SALESRETURNREASON") = dv.Item(0)("SALESRETURNREASON").ToString() & "," & dr("SALESRETURNREASON").ToString()
                                dv.Item(0)("CLPPOINTS") = Val(dv.Item(0)("CLPPOINTS").ToString()) + Val(dr("CLPPOINTS").ToString())
                                dv.Item(0)("CLPDISCOUNT") = Val(dv.Item(0)("CLPDISCOUNT").ToString()) + Val(dr("CLPDISCOUNT").ToString())
                                dv.Item(0)("SECTION") = dr("SECTION")
                            Else
                                ShowMessage(getValueByKey("CM080"), "CM080 - " & getValueByKey("CLAE04"))
                            End If
                        Else
                            dsMain.Tables("CASHMEMODTL").ImportRow(dr)
                            '---added by sagar cash memo return with so
                            'If clsDefaultConfiguration.IsNewSalesOrder And dr("ReturnDocumenttype") = "SO" Then
                            '    Dim res = dsMain.Tables("CASHMEMODTL").Select("BillLineNo='" & dr("BillLineNo").ToString() & "' and articlecode='" & dr("articlecode").ToString() & "' and Discription='" & dr("Discription").ToString() & "'")
                            '    If res.Length > 0 Then
                            '        res(0)("LineNumber") = dr("BillLineNo")
                            '    End If
                            'End If
                        End If
                    Next
                    CreatingLineNO(dsMain, "CASHMEMODTL")

                    ' Provison for showing credit sales of bill starts here
                    Dim dataview As New DataView(dsMain.Tables("CashMemoDtl"), "", "", DataViewRowState.CurrentRows)
                    Dim dtUnique As DataTable = dataview.ToTable(True, "RETURNCMNO")
                    Dim ReturnBills As New StringBuilder
                    For Each row As DataRow In dtUnique.Rows
                        ReturnBills.Append("'" & row("RETURNCMNO") & "',")
                    Next
                    Dim objclsReturn As New clsCashMemoReturn
                    dtCreditSaleData = objclsReturn.getCreditSaleBillData(ReturnBills.ToString().Substring(0, ReturnBills.ToString.Length - 1))
                    ' Provison for showing credit sales of bill End here
                    iscalledFromReturnClick = True

                End If
                GridSettings(UpdateFlag)
                calculateTotalbill()

                'For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Select("Btype='R'", "Ean", DataViewRowState.CurrentRows)
                '    If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                '        'CreateDataSetForTaxCalculation(dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt") - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT")), dr, dr("EAN"))
                '        If IsInclusiveTax Then
                '            CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt") - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT")), dr, dr("EAN"), True)
                '        Else
                '            CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt") - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT")), dr, dr("EAN"))
                '        End If
                '    Else
                '        If IsInclusiveTax Then
                '            CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt"), dr, dr("EAN"), True)
                '        Else
                '            CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt"), dr, dr("EAN"))
                '        End If
                '        'CreateDataSetForTaxCalculation(dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt"), dr, dr("EAN"))
                '    End If
                'Next
            End If


            If (dgMainGrid.Rows.Count > 1) Then
                dgMainGrid.Select(dgMainGrid.Rows.Count - 1, 2)
            End If
            'CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Select()
        Catch ex As Exception
            ShowMessage(getValueByKey("CM026"), "CM026 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in Updating return data into Main", "Error")
        End Try
    End Sub
    Private Sub cmdClrAllPromo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClrAllPromo.Click
        Dim dtTaxCalc As DataTable
        dtTaxCalc = New DataTable
        Dim EventType As Int32

        If (sender.ToString() = "ClearPromWithoutMessage") Then
            EventType = 1
        Else
            ShowMessage(getValueByKey("frmfastcashmemo.clearpromomsg"), getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
        End If

        If EventType = 1 Then
            ClearAllPromo()
            ReCalculateCM("")

            For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Select("Btype='S'", "Ean", DataViewRowState.CurrentRows)

                IsInclusiveTax = False
                ''added by ketan tax changes
                'dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, dr("ArticleCode"), "CMS", 1, dr("Ean"), clsDefaultConfiguration.CSTTaxCode, False)
                If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
                    If CustomerSaleType = 1 Then 'Dine_In = 1
                        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, dr("ArticleCode"), "DIN", 1, dr("Ean"), clsDefaultConfiguration.CSTTaxCode, False)
                    ElseIf CustomerSaleType = 2 Then  ' Home_Delivery = 2
                        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, dr("ArticleCode"), "HOD", 1, dr("Ean"), clsDefaultConfiguration.CSTTaxCode, False)
                    ElseIf CustomerSaleType = 3 Then 'Take_Away = 3
                        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, dr("ArticleCode"), "TAK", 1, dr("Ean"), clsDefaultConfiguration.CSTTaxCode, False)
                    Else
                        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, dr("ArticleCode"), "CMS", 1, dr("Ean"), clsDefaultConfiguration.CSTTaxCode, False)
                    End If
                Else
                    dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, dr("ArticleCode"), "CMS", 1, dr("Ean"), clsDefaultConfiguration.CSTTaxCode, False)
                End If
                If (dtTaxCalc IsNot Nothing AndAlso dtTaxCalc.Rows.Count > 0) Then
                    IsInclusiveTax = IIf(dtTaxCalc.Rows(0)("INCLUSIVE") IsNot DBNull.Value, dtTaxCalc.Rows(0)("INCLUSIVE"), False)
                End If

                Dim taxableAmount = Val(dr("MRP").ToString()) - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT"))
                If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), taxableAmount * dr("Quantity"), dr, dr("EAN"))
                    'If IsInclusiveTax Then
                    '    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt") - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT")), dr, dr("EAN"), True)
                    'Else
                    '    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt") - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT")), dr, dr("EAN"))
                    'End If
                Else
                    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), taxableAmount * dr("Quantity"), dr, dr("EAN"))
                    'If IsInclusiveTax Then
                    '    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt"), dr, dr("EAN"), True)
                    'Else
                    '    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt"), dr, dr("EAN"))
                    'End If
                End If
            Next
            IsRoundOffMsg = False
            ReCalculateCM("")
            calculateTotalbill()
            'CtrlSalesPersons.CtrlTxtBox.Focus()
            'CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Focus()
            CtrlSalesPersons.AndroidSearchTextBox.Select()
            If clsDefaultConfiguration.IsPoleDisply = True Then
                TwoLineExtendScreen()
            End If
        ElseIf EventType = 2 Then
            Exit Sub
        End If
    End Sub
    Private Sub cmdClearSelectedPromo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClearSelectedPromo.Click
        Try
            For Each dr As C1.Win.C1FlexGrid.Row In dgMainGrid.Rows.Selected
                If dr("Btype") = "S" AndAlso dr("PROMOTIONID").ToString() <> "NO" Then
                    If (dr("PROMOTIONID").ToString() <> String.Empty Or dr("FIRSTLEVEL").ToString() <> String.Empty Or dr("TOPLEVEL").ToString() <> String.Empty) And dr("MANUALPROMO").ToString() = "" Then
                        dr("LineDiscount") = 0
                        dr("TOTALDISCPERCENTAGE") = 0
                        dr("PROMOTIONID") = ""
                        'dr("MANUALPROMO") = ""
                        dr("FIRSTLEVEL") = String.Empty
                        dr("TOPLEVEL") = String.Empty
                        PromotionCleared = True
                    End If
                End If
            Next
            ReCalculateCM("")
            For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Select("Btype='S'", "Ean", DataViewRowState.CurrentRows)
                If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                    'CreateDataSetForTaxCalculation(dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt") - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT")), dr, dr("EAN"))
                    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt") - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT")), dr, dr("EAN"))
                Else
                    'CreateDataSetForTaxCalculation(dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt"), dr, dr("EAN"))
                    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt"), dr, dr("EAN"))
                End If
            Next
            ReCalculateCM("")
            calculateTotalbill()
            If clsDefaultConfiguration.IsPoleDisply = True Then
                TwoLineExtendScreen()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    Dim isCashierPromoSelected As Boolean
    Private Sub cmdDefaultPromo_Click(ByVal sender As Object, ByVal e As System.EventArgs, Optional ByVal ArticleCode As String = "") Handles cmdDefaultPromo.Click
        Try
            Dim custnumber As String = ""
            custnumber = CustInfo.CtrlTxtCustomerNo.Text
            Dim obj As New clsApplyPromotion
            obj.DecimalDigits = clsDefaultConfiguration.DecimalPlaces
            obj.IsInclusiveTax = IsInclusiveTax
            obj.MainTable = "CASHMEMODTL"
            obj.ExclusiveTaxFieldName = "EXCLUSIVETAX"
            obj.TotalDiscountField = "TOTALDISCOUNT"
            obj.GrossAmtField = "GROSSAMT"
            obj.Condition = "BTYPE='S'"
            'If MsgBox(getValueByKey  ("CM048"), MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2, "CM048") = MsgBoxResult.Yes Then
            If UCase(sender.id) = UCase("cmdApplySelectPromo") Then
                Dim dtList As DataTable
                dtList = obj.GetListofActivePromotions(clsAdmin.SiteCode)
                If Not dtList Is Nothing Then
                    Dim objView As New frmNCommonSearch
                    objView.RequestFromPage = enumOperationOnBill.Promotion
                    objView.SetData = dtList
                    objView.ShowDialog()
                    If Not objView.search Is Nothing Then
                        'code added for issue id 1532 by vipul
                        If objView.search(1).ToString <> "Store Manager Promo" Then
                            Dim pp As Object = "ClearPromWithoutMessage"
                            cmdClrAllPromo_Click(pp, Nothing)
                        End If
                        CustInfo.CtrlTxtCustomerNo.Value = custnumber
                        CustInfo.CtrlTxtCustomerNo.Text = custnumber
                        Dim offerno As String = objView.search(0)
                        If obj.CheckValidations(offerno) = True Then
                            Dim dtValidation As DataTable = obj.GetAllQuestions(offerno)
                            Dim StrQues As String = ""
                            For Each dr As DataRow In dtValidation.Rows
                                StrQues = StrQues & dr("QuestionName").ToString() & ","
                            Next
                            If StrQues.Contains("Autho") = True AndAlso StrQues.Contains("Voucher") = True Then
                                ' Dim dv As New DataView(dsMain.Tables("CashMemodtl"), "isnull(MANUALPROMO,'')<>''", "", DataViewRowState.CurrentRows)
                                'If dv.Count > 0 Then

                                'End If
                                Dim p As Object = "ClearPromWithoutMessage"
                                'code commented for issue id 1532
                                ' cmdClrAllPromo_Click(p, Nothing)
                                'ClearAllPromo() 'add for bug no 0001106
                                Dim dtReason As DataTable
                                Dim objReturn As New clsCashMemoReturn
                                dtReason = objReturn.GetReason("CMS")
                                If dsMain.Tables("CASHMEMOHDR").Rows.Count = 0 Then
                                    Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
                                End If
                                dsMain.Tables("CashMemoHdr").Rows(0)("AuthUserRemarks") = ShowReason(dtReason)
                                Dim isPromoSelected As Boolean

                                'code added for issue id 1532 by vipul
                                If dsMain.Tables("CashMemodtl").Rows(0)("TopLevel").ToString() <> "" OrElse dsMain.Tables("CashMemodtl").Rows(0)("PromotionId").ToString() <> "" Then
                                    modCommonFunc.isPromotionsDetailClearRequired = True
                                Else
                                    modCommonFunc.isPromotionsDetailClearRequired = False
                                End If

                                CheckInterTransactionAuth("ORD", dsMain.Tables("CashMemodtl"), 0, 0, 0, offerno, False, "", "BTYPE='S' AND (isnull(MANUALPROMO,'')='' OR isnull(MANUALPROMO,0)=0) AND", 0, _remarks, isPromoSelected)
                                If isPromoSelected Then
                                    isCashierPromoSelected = True
                                    IsDefaultPromotion = True
                                End If
                                dsMain.Tables("CashMemodtl").DefaultView.Sort = "BillLineNo Desc"
                                dsMain.Tables("CashMemodtl").DefaultView.Sort = "BillLineNo ASC"
                                dsMain.Tables("CashMemodtl").AcceptChanges()

                            ElseIf StrQues.Contains("Autho") = True Then
                                If CheckInterTransactionAuth("DAUTH", dsMain.Tables("CashMemodtl"), 0, 0, 0, offerno) = True Then
                                    obj.ApplySelectedPromotion(offerno, dsMain, clsAdmin.SiteCode)
                                    isCashierPromoSelected = True
                                End If
                            End If
                        Else
                            obj.ApplySelectedPromotion(offerno, dsMain, clsAdmin.SiteCode)
                            isCashierPromoSelected = True
                            IsDefaultPromotion = True
                        End If
                    End If
                End If
            Else
                'ShowMessage(getValueByKey  ("CM027"), "CM027")
                'ShowMessage("Default Schemes is applied Now", "Message")
                'ClearAllPromo()
                'code added for issue id 1532
                '  ClearAllPromo()
                CustInfo.CtrlTxtCustomerNo.Value = custnumber
                CustInfo.CtrlTxtCustomerNo.Text = custnumber
                'added by Khusrao Adil
                'for JK sprint 14 - Promotion based on date
                If clsDefaultConfiguration.PromotionBasedOn = "ApplicationDate" Then
                    obj.CalculatedDs(dsMain, clsAdmin.SiteCode, clsAdmin.DayOpenDate)
                Else
                    obj.CalculatedDs(dsMain, clsAdmin.SiteCode, CustWisePrice:=clsDefaultConfiguration.customerwisepricemanagement, CustNo:=custnumber, ArticelCode:=ArticleCode)
                End If

                'clsDefaultConfiguration.PromotionBasedOn
            End If

            'For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Select("Btype='S'", "Ean", DataViewRowState.CurrentRows)
            '    If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
            '        If (Val(dr("TOTALDISCOUNT").ToString()) > 0) Then
            '            CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt") - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT")), dr, dr("EAN"), isInclusiveCalcReq:=IsInclusiveTax)
            '        Else
            '            If (Val(dr("ExclusiveTax").ToString()) > 0) Then
            '                CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt"), dr, dr("EAN"), isInclusiveCalcReq:=IsInclusiveTax)
            '            Else
            '                CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("NetAmount"), dr, dr("EAN"), isInclusiveCalcReq:=IsInclusiveTax)
            '            End If
            '        End If

            '        'CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt") - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT")), dr, dr("EAN"), isInclusiveCalcReq:=IsInclusiveTax)
            '        'If IsInclusiveTax Then
            '        '    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt") - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT")), dr, dr("EAN"), True)
            '        'Else
            '        '    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt") - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT")), dr, dr("EAN"))
            '        'End If
            '    Else
            '        CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt"), dr, dr("EAN"), isInclusiveCalcReq:=IsInclusiveTax)
            '        'If IsInclusiveTax Then
            '        '    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt"), dr, dr("EAN"), True)
            '        'Else
            '        '    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt"), dr, dr("EAN"))
            '        'End If
            '    End If
            'Next
            Dim orgTax As Double = 0
            For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Rows
                Dim TaxableAmt = dr("GrossAmt") - dr("TotalDiscount")
                CreateDataSetForTaxCalculation(orgTax, dr("BillLineNo").ToString(), dr("ArticleCode").ToString(), TaxableAmt, dr, dr("EAN").ToString())
            Next
            ReCalculateCM("")
            calculateTotalbill()
            'CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Select()
        Catch ex As Exception
            ShowMessage(getValueByKey("CM028"), "CM028 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Promotion is not applied properly", "Error")
        End Try
    End Sub
    Private Sub cmdStockCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If OnlineConnect = False Then
                ShowMessage(getValueByKey("CMO50"), "CMO50 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            Dim objfrmStockCheck As New frmNStockCheck
            objfrmStockCheck.ShowDialog()
            ' CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Select()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdAdvanceSale_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType AndAlso CustomerSaleType = 0 Then Exit Sub

            'Rakesh-21.10.2013-8262: Select the Sales Person First! 
            If clsDefaultConfiguration.SalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0 Then
                ShowMessage(getValueByKey("CM025"), "CM025 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please Select the Sales Person", "information")
                Exit Sub
            End If

            Dim obj As New frmNAdvanceSale
            obj.dgMainGrid.AllowEditing = False

            If dtGV Is Nothing Then
                obj._incrementNo = 0
            ElseIf dtGV.Rows.Count = 0 Then
                obj._incrementNo = 0
            Else
                obj._incrementNo = dtGV.Rows(dtGV.Rows.Count - 1)("IssuedDocNumber")
            End If

            obj.DataSource = dsMain.Tables("CashMemoDtl")
            'obj.GVDetail = dtGV
            If sender.Tag = "GV" Then
                obj.SaleType = "GV"
            ElseIf sender.Tag = "CLP" Then
                obj.SaleType = "CLP"
            End If
            If obj.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                obj.GVDetail = Nothing
            End If
            If Not dtGV Is Nothing Then
                If Not obj.GVDetail Is Nothing Then
                    dtGV.Merge(obj.GVDetail, False, MissingSchemaAction.Ignore)
                End If
            Else
                If Not obj.GVDetail Is Nothing Then
                    dtGV = obj.GVDetail
                End If
            End If

            If Not dtGV Is Nothing Then
                For Each dr As DataRow In dtGV.Rows
                    dr("ISSUEDATSITE") = clsAdmin.SiteCode
                    dr("ISSUEDONDATE") = clsAdmin.CurrentDate
                    dr("ISSUEDINDOCTYPE") = "CMS"
                Next
            End If
            CreatingLineNO(dsMain, "CashMemoDtl")
            ReCalculateCM("")
            calculateTotalbill()
            'lblBalPoint.Text = obj.txtBalPoint.Text
            If sender.Tag = "CLP" Then
                CustInfo.CtrlTxtCustomerNo.Text = obj.txtClpNumber.Text
                CustInfo.CtrltxtCustomerName.Text = obj.txtName.Text
                CLPCustomerId = obj.txtClpNumber.Text
                CustInfo.ctrlTxtPoints.Text = obj.txtBalPoint.Text
                'lblCity.Text = obj.CLPCity
                CLPCardType = obj.ClpCard
            End If

            'CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Select()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdHomeDelivery_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Try
        '    Dim obj As New frmNHomeDelivery
        '    Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
        '    If UpdateFlag = True Then
        '        obj.CMdate = Convert.ToDateTime(dsMain.Tables("CASHMEMOHDR").Rows(0)("BillDate"))
        '    End If
        '    If dsMain.Tables("CASHMEMOHDR").Rows(0)("HDName").ToString() <> String.Empty Then
        '        'obj.CMdate = dsMain.Tables("CASHMEMOHDR").Rows(0)("BillDate")
        '        obj.DeliveryDate = dsMain.Tables("CASHMEMOHDR").Rows(0)("HDDeliveryDate")
        '        obj.HdName = dsMain.Tables("CASHMEMOHDR").Rows(0)("HDName")
        '        obj.Address1 = dsMain.Tables("CASHMEMOHDR").Rows(0)("HDAddress")
        '        obj.Email = dsMain.Tables("CASHMEMOHDR").Rows(0)("HDEmail")
        '        obj.TelNo = dsMain.Tables("CASHMEMOHDR").Rows(0)("HDTelNo")
        '        obj.Remark = dsMain.Tables("CASHMEMOHDR").Rows(0)("HDRemark")
        '    End If
        '    obj.ShowDialog()
        '    dsMain.Tables("CASHMEMOHDR").Rows(0)("HDDeliveryDate") = obj.DeliveryDate
        '    dsMain.Tables("CASHMEMOHDR").Rows(0)("HDName") = obj.HdName
        '    dsMain.Tables("CASHMEMOHDR").Rows(0)("HDAddress") = obj.Address1
        '    dsMain.Tables("CASHMEMOHDR").Rows(0)("HDEmail") = obj.Email
        '    dsMain.Tables("CASHMEMOHDR").Rows(0)("HDTelNo") = obj.TelNo
        '    dsMain.Tables("CASHMEMOHDR").Rows(0)("HDRemark") = obj.Remark

        '    CtrlSalesPersons.CtrlTxtBox.Select()
        '    CtrlSalesPersons.CtrlTxtBox.Focus()

        'Catch ex As Exception
        '    LogException(ex)
        'End Try
        '--- Copy Fast cash Memo Code :.Added By Mahesh 
        Try
            Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
            If Not String.IsNullOrEmpty(dsMain.Tables("CASHMEMOHDR").Rows(0)("HDName").ToString()) Then
                Dim obj As New frmNHomeDelivery
                If (IsDBNull(dsMain.Tables("CASHMEMOHDR").Rows(0)("HDDeliveryDate"))) Then
                    obj.DeliveryDate = clsAdmin.CurrentDate
                Else
                    obj.DeliveryDate = dsMain.Tables("CASHMEMOHDR").Rows(0)("HDDeliveryDate")
                End If

                obj.IsupdateDeliveryPersonAllowed = EditMode_IsupdateDeliveryPersonAllowed

                obj.HdName = dsMain.Tables("CASHMEMOHDR").Rows(0)("HDName").ToString()
                Dim Address() As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("HDAddress").ToString().Split(";")
                Select Case Address.Count
                    Case 1
                        obj.Address1 = Address(0).ToString()
                    Case 2
                        obj.Address1 = Address(0).ToString()
                        obj.Address2 = Address(1).ToString()
                    Case 3
                        obj.Address1 = Address(0).ToString()
                        obj.Address2 = Address(1).ToString()
                        obj.Address3 = Address(2).ToString()
                    Case 4
                        obj.Address1 = Address(0).ToString()
                        obj.Address2 = Address(1).ToString()
                        obj.Address3 = Address(2).ToString()
                        obj.Address4 = Address(3).ToString()
                    Case Else
                End Select
                obj.MobileNo = HDMobileNo
                obj.Email = dsMain.Tables("CASHMEMOHDR").Rows(0)("HDEmail").ToString()
                obj.TelNo = dsMain.Tables("CASHMEMOHDR").Rows(0)("HDTelNo").ToString()
                obj.Remark = dsMain.Tables("CASHMEMOHDR").Rows(0)("HDRemark").ToString()
                Dim Billno As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("BillNO").ToString
                Dim BillDate As DateTime
                If (IsDBNull(dsMain.Tables("CASHMEMOHDR").Rows(0)("BillDate"))) Then
                    BillDate = clsAdmin.DayOpenDate
                Else
                    BillDate = dsMain.Tables("CASHMEMOHDR").Rows(0)("BillDate")
                End If
                If UpdateFlag = True Then
                    Dim dsFloatAmt As DataSet = objCM.GetVoucherFloatData(Billno, BillDate)
                    obj.FloatAmt = IIf(dsFloatAmt.Tables("VoucherHdr").Rows(0)("TotalAmt") Is DBNull.Value, 0, dsFloatAmt.Tables("VoucherHdr").Rows(0)("TotalAmt"))
                Else
                    obj.FloatAmt = FloatAmt
                End If
                obj.DeliveryPersonID = dsMain.Tables("CASHMEMOHDR").Rows(0)("DeliveryPersonID").ToString()

                If (Not String.IsNullOrEmpty(dsMain.Tables("CASHMEMOHDR").Rows(0)("ClpNo").ToString())) Then
                    obj.CustomerNo = dsMain.Tables("CASHMEMOHDR").Rows(0)("ClpNo").ToString()
                ElseIf (Not String.IsNullOrEmpty(dsMain.Tables("CASHMEMOHDR").Rows(0)("CustomerNo").ToString())) Then
                    obj.CustomerNo = dsMain.Tables("CASHMEMOHDR").Rows(0)("CustomerNo").ToString()
                End If

                obj.btnClear.Visible = True

                Dim dialogResult = obj.ShowDialog()
                If (dialogResult = Windows.Forms.DialogResult.Cancel) Then
                    Exit Sub

                ElseIf dialogResult = Windows.Forms.DialogResult.Retry Then
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("DeliveryPersonID") = obj.DeliveryPersonID
                    obj.Dispose()
                    SearchCustomerForHomeDelivery()

                ElseIf dialogResult = Windows.Forms.DialogResult.OK AndAlso Not obj.txtName.Text = String.Empty Then
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("HDDeliveryDate") = obj.DeliveryDate
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("HDName") = obj.HdName.ToString()
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("HDAddress") = obj.Address1 + " ;" + obj.Address2 + " ;" + obj.Address3 + " ;" + obj.Address4
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("HDEmail") = obj.Email.ToString()
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("HDTelNo") = obj.TelNo.ToString()
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("HDRemark") = obj.Remark.ToString()
                    FloatAmt = obj.FloatAmt
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("DeliveryPersonID") = obj.DeliveryPersonID.ToString()
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("SalesExecutiveCode") = obj.DeliveryPersonID.ToString()
                    If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType Then
                        dsMain.Tables("CashMemoHdr").Rows(0)("ServiceTaxAmount") = 2
                        CustomerSaleType = enumCustomerSaleType.Home_Delivery
                    End If
                    If obj.IsUpdateCustomerAddress And Not String.IsNullOrEmpty(obj.CustomerNo) Then
                        Dim objCLPCustm As New clsCLPCustomer
                        objCLPCustm.UpdateCustomerAddress(obj.CustomerNo, clsAdmin.CLPProgram, obj.Address1.ToString(), obj.Address2.ToString(),
                                                          obj.Address3.ToString(), obj.Address4.ToString(),
                                                          obj.MobileNo.ToString(), obj.TelNo.ToString(),
                                                          obj.Email.ToString(), clsDefaultConfiguration.AllowMobileNoEditable)
                    End If

                    If Not String.IsNullOrEmpty(obj.HdName) Then
                        lblCustSaleType.Visible = True
                        CustSaleTypeTimer.Start()
                        HDPrintRequired = True
                        If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
                            If dgMainGrid.Rows.Count > 1 Then
                                RecalculateTax = True
                                BillRecalculateTax()
                            End If
                        End If
                    End If
                End If

            Else
                If dtHD Is Nothing OrElse dtHD.Rows.Count = 0 Then
                    SearchCustomerForHomeDelivery()
                Else
                    DisplayHomeDeliveryForm(dtHD, IsUsingCustSearch:=True)
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    Public Sub cmdOldCashMemo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOldCashMemo.Click
        Try

            If sender IsNot Nothing Then
                If DirectCast(sender, C1.Win.C1Ribbon.RibbonButton).ID = "cmdOldCashMemo" Then
                    objSearch.RequestFromPage = enumOperationOnBill.EditBill
                End If
            End If
            'If OnlineConnect = False Then
            '    ShowMessage("Sorry this is not active in Offline", "Information")
            '    Exit Sub
            'End If
            If UpdateFlag = False AndAlso dsMain.Tables("cashMemodtl").Rows.Count > 0 Then

                If MsgBox(getValueByKey("CM029"), MsgBoxStyle.YesNo, "CM029 - " & getValueByKey("CLAE04")) = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If
            IsRoundOffMsg = False
            objCM.ShiftManagementForCM = clsDefaultConfiguration.ShiftManagement
            Dim dtshift As New DataTable
            dtshift = clsCommon.GetNextShiftID(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
            Dim dt As DataTable = objCM.GetOldCashMemo(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID, clsAdmin.UserCode, clsCommon._PrevShiftId, NoOfDaysForReprint:=clsDefaultConfiguration.NumberOfDaysApplicableForReprint, NewCustmor:=clsDefaultConfiguration.DetailedCustomerCreationformat)
            'Dim dt As DataTable = objCM.GetOldCashMemo(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                'Dim objSearch As New frmNCommonSearch
                'objSearch.SetData = dt
                'objSearch.ShowDialog()

                objSearch.SetData = dt

                Dim DialogResult = objSearch.ShowDialog()

                If DialogResult = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                End If

                If Not objSearch.search Is Nothing AndAlso objSearch.search.Length > 0 Then
                    CtrlSalesPersons.CtrlTxtBox.Enabled = False
                    CtrlSalesPersons.AndroidSearchTextBox.Enabled = False
                    CtrlSalesPersons.CtrlCmdSearch.Enabled = False
                    CtrlSalesPersons.CtrlSalesPersons.Enabled = False
                    GetCashMemoDetails(objSearch.search(0), clsAdmin.SiteCode)
                    '--- Fill Credit sale return used amount ;;;;
                    dtCreditSaleData = objCM.GetCashMemoCreditAdjustedData(objSearch.search(0))

                    'ShowLastOper(DvItemDetail(0)("EAN").ToString(), DvItemDetail(0)("Discription").ToString(), DvItemDetail(0)("Quantity").ToString())
                    Dim ObjclsCommon As New clsCommon
                    Dim offerno As String = ObjclsCommon.GetOffernoForRoundOff()
                    If dsMain.Tables("CashMemoDtl").Rows(0)("PromotionId").ToString().Contains(offerno) And offerno <> "" Then
                        IsRoundOffMsg = True
                    End If
                    If Not dsMain.Tables("CashMemoHdr").Rows(0)("SalesExecutiveCode") Is Nothing Then
                        CtrlSalesPersons.CtrlSalesPersons.SelectedValue = dsMain.Tables("CashMemoHdr").Rows(0)("SalesExecutiveCode")
                    End If

                    If (Not String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim())) Then
                        Dim objCustm As New clsCLPCustomer()
                        Dim dtCustmInfo As DataTable
                        dtCustmInfo = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CustInfo.CtrlTxtCustomerNo.Text, CustomerWisePrice:=clsDefaultConfiguration.customerwisepricemanagement)
                        customerType = "CLP"

                        AssignCustomerInfoData(dtCustmInfo, objCustm)
                    End If
                    calculateTotalbill()

                    '--- Added By Mahesh Assign Customer Sales Sales Type for print Customer sales type
                    If Not IsDBNull(dsMain.Tables("CashMemoHdr").Rows(0)("ServiceTaxAmount")) AndAlso Val(dsMain.Tables("CashMemoHdr").Rows(0)("ServiceTaxAmount")) > 0 Then
                        CustomerSaleType = Convert.ToInt16(dsMain.Tables("CashMemoHdr").Rows(0)("ServiceTaxAmount"))
                        Select Case CustomerSaleType
                            Case enumCustomerSaleType.Dine_In
                                lblCustSaleType.Text = "Dine In"
                            Case enumCustomerSaleType.Home_Delivery
                                lblCustSaleType.Text = "Home Delivery"
                            Case enumCustomerSaleType.Take_Away
                                lblCustSaleType.Text = "Take Away"
                            Case Else
                        End Select
                        lblCustSaleType.Visible = True
                        CustSaleTypeTimer.Start()
                    End If

                    UpdateFlag = True
                    GridSettings(UpdateFlag)
                    PaymentGridSetting()
                    lblCMNo.Text = objSearch.search(0)
                    'lblCMDate.Text = objSearch.search(4)
                    Payment.Visible = True
                    cmdCard.Enabled = False
                    cmdCash.Enabled = False
                    cmdCreditSale.Enabled = False

                    cmdCheque.Enabled = False
                    CMbtnBottom.CtrlBtnSaleGV.Enabled = False
                    CMbtnBottom.CtrlBtnHomeDelivery.Enabled = False
                    CMbtnBottom.CtrlBtnSaleCLPPoint.Enabled = False
                    CMbtnBottom.CtrlBtnStockCheck.Enabled = False
                    CMbtnBottom.CtrlBtnReturn.Enabled = False
                    CMbtnBottom.CtrlBtnAddExtraCost.Enabled = False
                    CMbtnBottom.CtrlBtnHomeDelivery.Enabled = False



                    If CheckAuthorisation(clsAdmin.UserCode, "DeleteBill") Then
                        cmdDelete.Enabled = True
                    Else
                        cmdDelete.Enabled = False
                    End If

                    cmdCustomerinfo.Enabled = True
                    cmdDefaultPromo.Enabled = False
                    cmdClrAllPromo.Enabled = False
                    cmdClearSelectedPromo.Enabled = False
                    cmdApplySelectPromo.Enabled = False
                    rbnbtnRoundOff.Enabled = False
                    'Rakesh-19.09.2013> Mantis isuue -7483 
                    cmdHold.Enabled = False
                    rbnbtnApplyCST.Enabled = False
                    C1Ribbon1.DbtnF12.Enabled = False
                    C1Ribbon1.DbtnF2.Enabled = False

                    If clsAdmin.LangCode <> "EN" Then
                        Dim strNum, strCur, strnum1, strnum2 As String
                        Dim iSpaceIndex, iDotIndex As Integer
                        For Each drRow In dsMain.Tables("CASHMEMORECEIPT").Rows
                            For i As Integer = 0 To drRow("AmountReceived").ToString.Length
                                If drRow("AmountReceived").ToString(i) = " " Then
                                    iSpaceIndex = i
                                    Exit For
                                End If
                            Next
                            strNum = drRow("AmountReceived").ToString.Substring(0, iSpaceIndex)
                            If iSpaceIndex + 1 <= drRow("AmountReceived").ToString.Length Then
                                strCur = drRow("AmountReceived").ToString.Substring(iSpaceIndex)
                            End If
                            iDotIndex = strNum.LastIndexOf(".")
                            strnum1 = strNum.Substring(0, iDotIndex)
                            If iDotIndex + 1 <= strNum.Length Then
                                strnum2 = strNum.Substring(iDotIndex + 1)
                            End If

                            strNum = strnum1 & "," & strnum2
                            drRow("AmountReceived") = CDbl(strNum).ToString & strCur
                        Next
                    End If
                    '------ Code Added By Mahesh if home delivery is there user can modify delivery person only 
                    If Not IsDBNull(dsMain.Tables("CashMemoHdr").Rows(0)("DeliveryPersonID")) AndAlso Not String.IsNullOrEmpty(dsMain.Tables("CashMemoHdr").Rows(0)("DeliveryPersonID")) Then
                        CMbtnBottom.CtrlBtnHomeDelivery.Enabled = True
                        EditMode_IsupdateDeliveryPersonAllowed = True
                    Else
                        CMbtnBottom.CtrlBtnHomeDelivery.Enabled = False
                        EditMode_IsupdateDeliveryPersonAllowed = False
                    End If
                End If
                If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                    If UpdateFlag = True Then
                        CustInfo.SendToBack()
                    Else
                        CustInfo.BringToFront()
                    End If

                End If
            End If
            dgMainGrid.Select(dgMainGrid.Rows.Count - 1, 2)
            dgMainGrid.Focus()
        Catch ex As Exception
            ShowMessage(getValueByKey("CM030"), "CM030 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in retriving the cash memo data", "Error")
        End Try
    End Sub

    Private Sub cmdPayments_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPayments.Click
        Try
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType Then
                If CustomerSaleType = enumCustomerSaleType.Home_Delivery Then
                    If CustInfo.CtrlTxtSwape.Text.Trim = "" Then
                        ShowMessage(getValueByKey("Crs015"), "CM030 - " & getValueByKey("CLAE05"))
                        Exit Sub
                    End If
                End If
            End If
            '--- Here we see In payment Screen not allow to select currency as of now i.e . we are suppose to selected currency also base currency 
            '-- Once it allow to select currency need to change SelectedCurrencyIndex too.
            objCM._iCurrencyCode = clsAdmin.CurrencyCode
            objCM.SelectedCurrencyIndex = clsAdmin.CurrencyCode
            '0,000 should remain, then after selection of items, on payment, Customer selection to be mandatory as it was earlier.
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType AndAlso clsDefaultConfiguration.IsAfterSelectSaleTypeCmSelectionRequired Then 'ketan
                If CustomerSaleType = 0 Then
                    If dsMain.Tables("CASHMEMODTL").Rows(0)("BTYPE").ToString() = "R" Then
                    Else
                        Exit Sub
                    End If
                End If
                Dim CustSaleType As String = IIf(CustomerSaleType = enumCustomerSaleType.Dine_In, "DINEIN", "TAKEAWAY")
                If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then
                    cmdCustomerSearchAndLoad(CustSaleType)
                End If
                If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then Exit Sub
            End If

            If (Not UpdateFlag) Then
                'ShowMessage(getValueByKey("CM067"), "CM067 - " & getValueByKey("CLAE04"))
            End If

            If UpdateFlag = True Then
                If CheckInterTransactionAuth("UpdateBill", dsMain.Tables("CashMemoHdr"), 0, 0, 0, "") = False Then
                    Exit Sub
                End If
                If objCM.CheckVoucherRedemmed(clsAdmin.SiteCode, dsMain.Tables("CashMemoHdr").Rows(0)("BILLNO").ToString(), "CMS") = False Then
                    ShowMessage(getValueByKey("CM058"), "CM058 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
            Else
                If UpdateFlag = False AndAlso clsDefaultConfiguration.SalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0 Then
                    ShowMessage(getValueByKey("CM025"), "CM005 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Please Select the Sales Person", "information")
                    Exit Sub
                End If
                'If PromotionCleared = False Then
                cmdDefaultPromo_Click(sender, e)
                'End If
                cmdLoyalty_Click(sender, e)
            End If
            If clsDefaultConfiguration.IsPoleDisply = True Then
                TwoLineExtendScreen(Ispayment:=True)
            End If
            If Not (CashSummary.CtrllblNetAmt.Text = String.Empty) Then
                Dim ds As DataSet
                Dim objPayment As frmNAcceptPaymentPC
                CLP_Data.Sitecode = clsAdmin.SiteCode
                getclpsettings()

                If Not CLP_Data.CLPConfigdata.Tables("CLPHeader") Is Nothing AndAlso CLP_Data.CLPConfigdata.Tables("CLPHeader").Rows.Count > 0 AndAlso CLP_Data.CLPConfigdata.Tables("CLPHeader")(0)("RedemptionType").ToString().ToLower() = "rdt1" Then
                    objPayment = New frmNAcceptPaymentPC(dsMain.Tables("CASHMEMODTL"), True)
                Else
                    objPayment = New frmNAcceptPaymentPC(True)
                End If

                objPayment.IsTenderChange = UpdateFlag
                objPayment.TxtRemark.Text = _remarks
                Dim isApplicableforReturn As Boolean = False
                Dim returnfund As New DataTable
                Dim dtCreditData As New DataTable
                'objPayment.TotalBillAmount = Nothing

                If iscalledFromReturnClick = True Then
                    If dsMain.Tables("CASHMEMODTL").Rows.Count > 0 Then
                        If Not dsMain.Tables("CASHMEMODTL").Rows(0)("BILLNO") Is DBNull.Value Then
                            isApplicableforReturn = objCM.isApplicableforcashReturn(dsMain.Tables("CASHMEMODTL").Rows(0)("BILLNO"))
                        End If
                    End If
                End If

               
                If isApplicableforReturn = True Then
                    returnfund.Clear()
                    dtCreditData = dsMain.Tables("CASHMEMODTL")
                    returnfund = objCM.ReturnAmount(dtCreditData)
                    ' objPayment.TotalBillAmount = "-" & FormatNumber(CDbl(returnfund.Rows(0)("AmountTendered").ToString), 2)
                    objPayment.TotalBillAmount = CashSummary.CtrllblNetAmt.Text
                Else
                    objPayment.TotalBillAmount = CashSummary.CtrllblNetAmt.Text
                End If
                ' objPayment.TotalBillAmount = CashSummary.CtrllblNetAmt.Text
                objPayment.ParentRelation = "CashMemo"
                objPayment.IsGiftVoucherIssued = GVBasedAricleReturnList
                If CustInfo.CtrlTxtCustomerNo.Text <> String.Empty Then
                    objPayment.CLPCustomerCardNumber = CLPCustomerId
                    objPayment.CLPCustomerName = CustInfo.CtrltxtCustomerName.Text
                End If
                If dsMain.Tables("CASHMEMORECEIPT").Rows.Count > 0 Then
                    Dim dt As DataTable = dsMain.Tables("CASHMEMORECEIPT").Copy()
                    ds = New DataSet()
                    ds.Tables.Add(dt)
                    UpdatePaymentPrevStru(ds.Tables("CASHMEMORECEIPT"))

                    If Not ds.Tables("MstRecieptType").Columns.Contains("NOCLP") Then
                        ds.Tables("MstRecieptType").Columns.Add("NOCLP", System.Type.GetType("System.Boolean"))
                    End If

                    objPayment.AcceptEditBillDataSet = ds
                    objPayment.PaymentType = clsAcceptPayment.PaymentType.EditBill
                End If
                objPayment.TopMost = False
                objPayment.RoundAt = clsDefaultConfiguration.BillRoundOffAt

                Dim dtCustomer As New DataTable   '' Vipin PC SO Merge 16.05.2018
                dtCustomer = objcom.GetCustomerDetails(CustInfo.CtrlTxtCustomerNo.Text.Trim(), clsAdmin.SiteCode)
                If Not dtCustomer Is Nothing AndAlso dtCustomer.Rows.Count > 0 Then
                    objPayment.CustName = dtCustomer.Rows(0)("NameOnCard").ToString
                    objPayment.CompName = dtCustomer.Rows(0)("CompanyName").ToString
                    objPayment.MobNumber = dtCustomer.Rows(0)("Mobileno").ToString
                End If

                objPayment.ShowDialog(Me)
                PaymentTermId = objPayment.PaymentTermNameId
                If True Then
                    _billAmt = objPayment.TotalBillAmount
                    _paidAmt = objPayment.TotalCustomerPadiAmount
                End If

                If objPayment.IsCancelAcceptPayment = False Then

                    '------------------- added by vipin-----------------
                    Dim dtCust As DataTable = objPayment._dtCust
                    Dim Type As String = objPayment._Addresstype
                    Dim custType As String = objPayment._custType

                    If Not dtCust Is Nothing Then
                        If dtCust.Rows.Count > 0 Then
                            If dsMain.Tables("CASHMEMOHDR").Rows.Count > 0 Then
                                If (dtCust.Rows(0)("CustomerType") = "CLP") Then
                                    dsMain.Tables("CASHMEMOHDR").Rows(0)("ClpNo") = dtCust.Rows(0)("CUSTOMERNO").ToString()
                                Else
                                    dsMain.Tables("CASHMEMOHDR").Rows(0)("ClpNo") = dtCust.Rows(0)("CUSTOMERNO").ToString()
                                End If
                                'dsMain.Tables("CASHMEMOHDR").Rows(0)("HDDeliveryDate") = obj.DeliveryDate
                                dsMain.Tables("CASHMEMOHDR").Rows(0)("HDName") = dtCust.Rows(0)("CustomerName").ToString()
                                dsMain.Tables("CASHMEMOHDR").Rows(0)("HDAddress") = dtCust.Rows(0)("ADDRESSLN1").ToString() + " ;" + dtCust.Rows(0)("ADDRESSLN2").ToString() + " ;" + dtCust.Rows(0)("ADDRESSLN3").ToString() + " ;" + dtCust.Rows(0)("ADDRESSLN4").ToString()
                                dsMain.Tables("CASHMEMOHDR").Rows(0)("HDEmail") = dtCust.Rows(0)("EMAILID").ToString()
                                dsMain.Tables("CASHMEMOHDR").Rows(0)("HDTelNo") = dtCust.Rows(0)("MOBILENO").ToString()
                            End If

                            'Dim type As String = objCust.AddressType
                            If Not dtCust Is Nothing AndAlso dtCust.Rows.Count > 0 Then
                                Dim dv As New DataView(dtCust, IIf(Type Is Nothing, "", "isnull(AddressType,'')='" & Type & "'"), "", DataViewRowState.CurrentRows)
                                If dv.Count > 0 Then

                                    'CustSaleTypeTimer.Stop()
                                    'lblCustSaleType.Visible = False
                                    calculateTotalbill() 'aaded by prasad clear btn issue(clear payment detail also)
                                    Dim ObjclsCommon As New clsCommon
                                    Dim da = ObjclsCommon.GetLastVisited(dv.Item(0)("CUSTOMERNO").ToString())
                                    CustInfo.CtrlTxtCustomerNo.Text = dv.Item(0)("CUSTOMERNO").ToString()
                                    CustInfo.CtrlLastVisit.Text = ObjclsCommon.GetLastVisited(dv.Item(0)("CUSTOMERNO")).Date
                                    CustInfo.CtrltxtCustomerName.Text = dv.Item(0)("CustomerName").ToString()
                                    ' lblCity.Text = dv.Item(0)("CITY")
                                    'lblBalPoint.Text = dv.Item(0)("BalancePoint")
                                    customerType = IIf((dtCust.Rows(0)("CustomerType") Is DBNull.Value), "", dtCust.Rows(0)("CustomerType"))
                                    CustInfo.CtrlTxtSwape.Text = dv.Item(0)("CUSTOMERNO").ToString()
                                    If (dtCust.Columns.Contains("BalancePoint")) Then
                                        CustInfo.ctrlTxtPoints.Text = dv.Item(0)("BalancePoint").ToString()
                                        CLPCardType = IIf((dv.Item(0)("CARDTYPE") Is DBNull.Value), "", dv.Item(0)("CARDTYPE"))
                                    Else
                                        CustInfo.ctrlTxtPoints.Text = String.Empty
                                    End If

                                    CLPCustomerId = dv.Item(0)("CUSTOMERNO").ToString()
                                    'Dim objCustm As New clsCLPCustomer
                                    'If clsDefaultConfiguration.IsCstTaxRequired AndAlso objCustm.CheckIfCstApplicable(clsAdmin.SiteCode, dv.Item(0)("STATE").ToString()) Then
                                    '    DisplayCSTMessage()
                                    'End If
                                End If
                            End If
                        End If
                    End If 'vipin

                    ds = objPayment.ReciptTotalAmount()

                    If ds.Tables.Count > 0 Then
                        If ds.Tables(0).Rows.Count > 0 Then
                            'If Not ds.Tables(0).Columns("RefNo_4") Is Nothing Then

                            '    For Each dr In ds.Tables(0).Rows   'vipin
                            '        dr("RefNo_4") = dr("Remarks")
                            '    Next

                            'End If 'vipin Edit bill Error
                            If Not ds.Tables(0).Columns("NEFTReferenceNo") Is Nothing AndAlso Not ds.Tables(0).Columns("RTGSReferenceNo") Is Nothing AndAlso Not ds.Tables(0).Columns("RefNo_4") Is Nothing Then

                                For Each dr In ds.Tables(0).Rows   'vipin
                                    If dr("NEFTReferenceNo") <> "-" Then
                                        dr("Number") = dr("NEFTReferenceNo")
                                    End If

                                    If dr("RTGSReferenceNo") <> "-" Then
                                        dr("Number") = dr("NEFTReferenceNo")
                                    End If
                                    dr("RefNo_4") = dr("Remarks")
                                Next

                            End If
                        End If

                        'Added by Rohit for CR5938
                        _dDueDate = objPayment.dDueDate
                        _strRemarks = objPayment.strRemarks

                        ds = objPayment.ReciptTotalAmount()
                        cmdLoyalty_Click(sender, e, ds.Tables("MSTRecieptType"))
                        If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
                            UpdatePaymentDataSetStru(ds, UpdateFlag)
                            '' added by ketan savoy Outstanding changes 
                            If clsDefaultConfiguration.IsSavoy Then
                                UpdateSaleOrderTermNConditionsStru(ds, UpdateFlag)
                            End If
                            cmdHold.Enabled = False

                            If objPayment.Action = "Save" Then
                                If cmdSavePrint_Click(sender, e) Then
                                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                                End If
                            ElseIf objPayment.Action = "Gift" Then

                                '*********************Rahul Changes Start 02 Nov*********************.
                                'Dim obj As New frmSpecialPrompt("Gift Message ")
                                'obj.ShowTextBox = True
                                'obj.ShowDialog()
                                'If Not obj.GetResult Is Nothing Then
                                '    GiftMsg = obj.GetResult
                                'End If
                                GiftMsg = objPayment.GiftReceiptMessage
                                '*********************Rahul Changes End 02 Nov*********************.

                                If cmdGiftPrint_Click(sender, e) Then
                                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                                End If

                            End If
                        ElseIf ds.Tables.Count = 0 AndAlso CDbl(CashSummary.CtrllblNetAmt.Text) = 0 Then
                            cmdHold.Enabled = False
                            If objPayment.Action = "Save" Then
                                If cmdSavePrint_Click(sender, e) Then
                                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                                End If

                            ElseIf objPayment.Action = "Gift" Then
                                GiftMsg = objPayment.GiftReceiptMessage
                                If cmdGiftPrint_Click(sender, e) Then
                                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                                End If


                            End If
                        End If
                    End If
                    If clsDefaultConfiguration.IsTablet Then
                        If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
                            Dim drp() = Process.GetProcessesByName("osk")
                            If drp.Length > 0 Then
                                Dim proc As New Process
                                For Each pr As Process In Process.GetProcesses()
                                    If pr.ProcessName = "osk" Then
                                        pr.Kill()
                                    End If

                                Next
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CM031"), "CM031 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in updating the payment data to main", "Error")
        End Try

    End Sub
    Private Sub cmdCheque_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCheque.Click
        Try
            If IsTenderCheque Then
                If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType Then
                    If CustomerSaleType = enumCustomerSaleType.Home_Delivery Then
                        If CustInfo.CtrlTxtSwape.Text.Trim = "" Then
                            ShowMessage(getValueByKey("Crs015"), "CM030 - " & getValueByKey("CLAE05"))
                            Exit Sub
                        End If
                    End If
                End If
                '0,000 should remain, then after selection of items, on payment, Customer selection to be mandatory as it was earlier.
                If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType AndAlso clsDefaultConfiguration.IsAfterSelectSaleTypeCmSelectionRequired Then 'ketan
                    If CustomerSaleType = 0 Then Exit Sub
                    Dim CustSaleType As String = IIf(CustomerSaleType = enumCustomerSaleType.Dine_In, "DINEIN", "TAKEAWAY")
                    If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then
                        cmdCustomerSearchAndLoad(CustSaleType)
                    End If
                    If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then Exit Sub
                End If

                'ShowMessage(getValueByKey("CM067"), "CM067 - " & getValueByKey("CLAE04"))
                If UpdateFlag = False AndAlso clsDefaultConfiguration.SalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0 Then
                    ShowMessage(getValueByKey("CM025"), "CM005 - " & getValueByKey("CLAE04"))
                    Exit Sub

                ElseIf UpdateFlag = False AndAlso String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text) Then
                    ShowMessage(getValueByKey("CHKP11"), "CHKP11 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If

                If UpdateFlag = False Then
                    'If PromotionCleared = False Then
                    cmdDefaultPromo_Click(sender, e)
                    'End If
                    'cmdLoyalty_Click(sender, e)
                End If
                Dim objCheck As New frmNCheckPayment
                objCheck.BillAmount = CDbl(CashSummary.CtrllblNetAmt.Text)
                objCheck.ShowDialog()
                'If objCheck.CheckAmount > 0 Then
                '    objCheck.Close()
                '    Dim objPayment As New frmNAcceptPayment()
                '    objPayment.Show()
                '    objPayment.TotalBillAmount = CashSummary.CtrllblNetAmt.Text
                '    objPayment.Enabled = False
                '    objPayment.cboRecieptType.SelectedValue = "Cheque"
                '    objPayment.TotalBillAmount = CashSummary.CtrllblNetAmt.Text
                '    'objPayment.cboCurrency.SelectedIndex = 1
                '    objPayment.InsertCheque(objCheck.CheckAmount, objCheck.CheckNo, objCheck.CheckDate, objCheck.MicrNo, objCheck.BankName)
                '    Dim ds As DataSet = objPayment.ReciptTotalAmount()
                '    objPayment.Close()
                '    If Not ds Is Nothing Then
                '        UpdatePaymentDataSetStru(ds, UpdateFlag)
                '        cmdHold.Enabled = False
                '        PaymentGridSetting()
                '        If objCheck.Action = "Save" Then
                '            cmdSavePrint_Click(sender, e)
                '        ElseIf objCheck.Action = "Gift" Then
                '            cmdGiftPrint_Click(sender, e)
                '        End If
                '    End If
                'End If
                If objCheck.IsCancelAcceptPayment = False Then
                    If Not objCheck.ReciptTotalAmount Is Nothing And objCheck.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                        Dim ds As DataSet = objCheck.ReciptTotalAmount
                        'Dim ds As New DataSet()
                        'ds.Tables.Add(dt)
                        objCheck.Close()
                        'If Not ds Is Nothing Then
                        UpdatePaymentDataSetStru(ds, UpdateFlag)
                        cmdHold.Enabled = False
                        PaymentGridSetting()
                        If UpdateFlag = False Then

                            Dim dt As New DataTable
                            dt = ds.Tables("Cashmemoreceipt").Copy()
                            dt.Columns("TenderHeadCode").ColumnName = "Reciepttypecode"
                            cmdLoyalty_Click(sender, e, dt)
                            dt.Dispose()
                        End If
                        If objCheck.Action = My.Resources.AcceptPaymentActionTypeSave Then


                            If cmdSavePrint_Click(sender, e) Then
                                AutoLogout(FrmTranCode, Me, lblLoggedIn)
                            End If

                        ElseIf objCheck.Action = My.Resources.AcceptPaymentActionTypeGift Then

                            '*********************Rahul Changes Start 02 Nov*********************.
                            'Dim obj As New frmSpecialPrompt("Gift Message ")
                            'obj.ShowTextBox = True
                            'obj.ShowDialog()
                            'If Not obj.GetResult Is Nothing Then
                            '    GiftMsg = obj.GetResult
                            'End If
                            GiftMsg = objCheck.GiftReceiptMessage

                            '****************************Rahul Changes End 02 Nov********************.
                            If cmdGiftPrint_Click(sender, e) Then
                                AutoLogout(FrmTranCode, Me, lblLoggedIn)
                            End If

                        End If

                        'End If
                    Else
                        ShowMessage(getValueByKey("BL095"), "BL095 - " & getValueByKey("CLAE05"))
                    End If
                End If
                If clsDefaultConfiguration.IsTablet Then
                    If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
                        Dim drp() = Process.GetProcessesByName("osk")
                        If drp.Length > 0 Then
                            Dim proc As New Process
                            For Each pr As Process In Process.GetProcesses()
                                If pr.ProcessName = "osk" Then
                                    pr.Kill()
                                End If

                            Next
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdCash_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCash.Click
        Try
            If IsTenderCash Then
                If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType Then
                    If CustomerSaleType = enumCustomerSaleType.Home_Delivery Then
                        If CustInfo.CtrlTxtSwape.Text.Trim = "" Then
                            ShowMessage(getValueByKey("Crs015"), "CM030 - " & getValueByKey("CLAE05"))
                            Exit Sub
                        End If
                    End If
                End If
                'ShowMessage(getValueByKey("CM067"), "CM067 - " & getValueByKey("CLAE04"))
                '0,000 should remain, then after selection of items, on payment, Customer selection to be mandatory as it was earlier.
                If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType AndAlso clsDefaultConfiguration.IsAfterSelectSaleTypeCmSelectionRequired Then 'ketan
                    If CustomerSaleType = 0 Then Exit Sub
                    Dim CustSaleType As String = IIf(CustomerSaleType = enumCustomerSaleType.Dine_In, "DINEIN", "TAKEAWAY")
                    If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then
                        cmdCustomerSearchAndLoad(CustSaleType)
                    End If
                    If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then Exit Sub
                End If

                If UpdateFlag = False Then
                    'If PromotionCleared = False Then
                    cmdDefaultPromo_Click(sender, e)
                    'End If
                    'cmdLoyalty_Click(sender, e)
                End If
                If clsDefaultConfiguration.IsPoleDisply = True Then
                    TwoLineExtendScreen(Ispayment:=True)
                End If
                If UpdateFlag = False AndAlso clsDefaultConfiguration.SalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0 Then
                    ShowMessage(getValueByKey("CM025"), "CM005 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
                If clsDefaultConfiguration.DirectCashPayment Then
                    DirectCashPayment(sender, e)
                Else
                    Dim objPaymentByCash As New frmNAcceptPaymentByCash
                    objPaymentByCash.txtRemark.Text = _remarks
                    objPaymentByCash._IsCashierPromoSelected = isCashierPromoSelected
                    objPaymentByCash.TotalBillAmount = CDbl(CashSummary.CtrllblNetAmt.Text)
                    objPaymentByCash.ShowDialog()
                    If Not (objPaymentByCash.IsCancelAcceptPayment) Then
                        If Not objPaymentByCash.ReciptTotalAmount Is Nothing And objPaymentByCash.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                            Dim ds As DataSet = objPaymentByCash.ReciptTotalAmount
                            _billAmt = objPaymentByCash.TotalBillAmount
                            _paidAmt = objPaymentByCash.TotalCustomerPadiAmount
                            'Dim ds As New DataSet()
                            'ds.Tables.Add(dt)
                            objPaymentByCash.Close()
                            'If Not ds Is Nothing Then
                            UpdatePaymentDataSetStru(ds, UpdateFlag)
                            cmdHold.Enabled = False
                            PaymentGridSetting()
                            If UpdateFlag = False Then

                                Dim dt As New DataTable
                                dt = ds.Tables("Cashmemoreceipt").Copy()
                                dt.Columns("TenderHeadCode").ColumnName = "Reciepttypecode"
                                cmdLoyalty_Click(sender, e, dt)
                                dt.Dispose()
                            End If
                            If objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeSave Then


                                If cmdSavePrint_Click(sender, e) Then

                                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                                End If

                            ElseIf objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeGift Then
                                'Rahul Changes Start 02 Nov. 

                                'Dim obj As New frmSpecialPrompt("Gift Message ")
                                'obj.ShowTextBox = True
                                'obj.ShowDialog()
                                'If Not obj.GetResult Is Nothing Then
                                '    GiftMsg = obj.GetResult
                                'End If
                                GiftMsg = objPaymentByCash.GiftReceiptMessage
                                'Rahul Changes End 02 Nov. 

                                If cmdGiftPrint_Click(sender, e) Then
                                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                                End If

                            End If

                            'End If
                        Else
                            ShowMessage(getValueByKey("BL095"), "BL095 - " & getValueByKey("CLAE05"))
                        End If
                    End If
                End If
                If clsDefaultConfiguration.IsTablet Then
                    If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
                        Dim drp() = Process.GetProcessesByName("osk")
                        If drp.Length > 0 Then
                            Dim proc As New Process
                            For Each pr As Process In Process.GetProcesses()
                                If pr.ProcessName = "osk" Then
                                    pr.Kill()
                                End If

                            Next
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM033"), "CM033 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Public Function DirectCashPayment(ByVal sender As Object, ByVal e As System.EventArgs) As Boolean
        Try
            DirectCashPayment = False
            Dim ConfigPath As String = clsDefaultConfiguration.DirectCashPayment
            Dim dsprint As DataSet = DirectCashPaymentPrint(sender, e)
            Dim ds As DataSet = DirectCashPaymentPrint(sender, e)
            UpdatePaymentDataSetStru(ds, UpdateFlag)
            ' cmdHold.Enabled = False
            PaymentGridSetting()
            If UpdateFlag = False Then
                Dim dt As New DataTable
                dt = dsprint.Tables("MstRecieptType").Copy()
                dt.Columns("Reciepttypecode").ColumnName = "Reciepttypecode"
                cmdLoyalty_Click(sender, e, dt)
                dt.Dispose()
            End If
            If cmdSavePrint_Click(sender, e) Then
                AutoLogout(FrmTranCode, Me, lblLoggedIn)
            End If
            Return DirectCashPayment
        Catch ex As Exception
            LogException(ex)
            Return DirectCashPayment
        End Try
    End Function
    Private Function DirectCashPaymentPrint(sender As Object, e As EventArgs) As DataSet
        Try

            'Try
            '    If clsAdmin.IsCashDrawer Then
            '        Dim cA4Print As New clsA4Print
            '        cA4Print.OperateDevice("CashDrawer", CDflag:=clsDefaultConfiguration.CDBuildCode)
            '    End If
            'Catch ex As Exception

            'End Try
            Dim TotalCustomerPaidAmount As Decimal = CDbl(CashSummary.CtrllblNetAmt.Text)

            Dim _Actiontype As String = My.Resources.AcceptPaymentActionTypeSave.ToString()

            Dim dsRecieptType As New DataSet
            PaymentTransactionByShortCutForms(TotalCustomerPaidAmount, SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cash.ToString(), SpectrumBL.AcceptPaymentTenderType.PositiveTenderType.Cash, dsRecieptType)
            Return dsRecieptType

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Sub cmdCard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCard.Click
        'Try
        '    Dim objPayment As New frmNAcceptPayment()
        '    objPayment.TotalBillAmount = CashSummary.CtrllblNetAmt.Text
        '    objPayment.PaymentBy = "Card"
        '    objPayment.SwipeCard()
        '    'objPayment.cboCurrency.SelectedIndex = 1
        '    objPayment.ShowDialog()
        '    Dim ds As DataSet = objPayment.ReciptTotalAmount()
        '    objPayment.Close()
        '    If Not ds Is Nothing And ds.Tables.Count > 0 Then
        '        UpdatePaymentDataSetStru(ds, UpdateFlag)
        '        cmdHold.Enabled = False
        '        PaymentGridSetting()
        '        cmdSavePrint_Click(sender, e)
        '    End If
        'Catch ex As Exception
        '    ShowMessage(getValueByKey  ("CM034"), "Error:CM034")
        '    LogException(ex)
        '    'ShowMessage("Error in Updating card payment data ", "Information")
        'End Try
        Try
            If IsTenderCreditCard Then
                If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType Then
                    If CustomerSaleType = enumCustomerSaleType.Home_Delivery Then
                        If CustInfo.CtrlTxtSwape.Text.Trim = "" Then
                            ShowMessage(getValueByKey("Crs015"), "CM030 - " & getValueByKey("CLAE05"))
                            Exit Sub
                        End If
                    End If
                End If
                '0,000 should remain, then after selection of items, on payment, Customer selection to be mandatory as it was earlier.
                If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType AndAlso clsDefaultConfiguration.IsAfterSelectSaleTypeCmSelectionRequired Then 'ketan
                    If CustomerSaleType = 0 Then Exit Sub
                    Dim CustSaleType As String = IIf(CustomerSaleType = enumCustomerSaleType.Dine_In, "DINEIN", "TAKEAWAY")
                    If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then
                        cmdCustomerSearchAndLoad(CustSaleType)
                    End If
                    If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then Exit Sub
                End If

                'ShowMessage(getValueByKey("CM067"), "CM067 - " & getValueByKey("CLAE04"))
                If UpdateFlag = False AndAlso clsDefaultConfiguration.SalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0 Then
                    ShowMessage(getValueByKey("CM025"), "CM005 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If

                If UpdateFlag = False Then
                    'If PromotionCleared = False Then
                    cmdDefaultPromo_Click(sender, e)
                    'End If
                    'cmdLoyalty_Click(sender, e)
                End If
                If clsDefaultConfiguration.IsPoleDisply = True Then
                    TwoLineExtendScreen(Ispayment:=True)
                End If
                Dim objPayment As New frmNAcceptPaymentByCard()
                objPayment.TotalBillAmount = CDbl(CashSummary.CtrllblNetAmt.Text)
                'objPayment.cboCurrency.SelectedIndex = 1
                objPayment.ShowDialog()
                Dim selectedTenderName As String = objPayment.SelectedTenderName
                Dim strCardTenderCode As String = objPayment.CardTenderCode
                objPayment.Close()
                If Not (objPayment.IsCancelAcceptPayment) Then
                    If Not objPayment.ReciptTotalAmount Is Nothing And objPayment.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                        Dim ds As DataSet = objPayment.ReciptTotalAmount
                        'Dim ds As New DataSet()
                        'ds.Tables.Add(dt)
                        objPayment.Close()
                        'If Not ds Is Nothing Then
                        UpdatePaymentDataSetStru(ds, UpdateFlag)
                        cmdHold.Enabled = False
                        PaymentGridSetting()
                        If UpdateFlag = False Then

                            Dim dt As New DataTable
                            dt = ds.Tables("Cashmemoreceipt").Copy()
                            dt.Columns("TenderHeadCode").ColumnName = "Reciepttypecode"
                            cmdLoyalty_Click(sender, e, dt)
                            dt.Dispose()
                        End If
                        If objPayment.Action = My.Resources.AcceptPaymentActionTypeSave Then


                            If cmdSavePrint_Click(sender, e) Then
                                AutoLogout(FrmTranCode, Me, lblLoggedIn)
                            End If

                        ElseIf objPayment.Action = My.Resources.AcceptPaymentActionTypeGift Then
                            'Rahul Changes Start 02 Nov. 

                            'Dim obj As New frmSpecialPrompt("Gift Message ")
                            'obj.ShowTextBox = True
                            'obj.ShowDialog()
                            'If Not obj.GetResult Is Nothing Then
                            '    GiftMsg = obj.GetResult
                            'End If 
                            GiftMsg = objPayment.GiftReceiptMessage
                            'Rahul Changes End 02 Nov. 

                            If cmdGiftPrint_Click(sender, e) Then
                                AutoLogout(FrmTranCode, Me, lblLoggedIn)
                            End If

                        End If
                    Else
                        ShowMessage(getValueByKey("BL095"), "BL095 - " & getValueByKey("CLAE05"))
                    End If
                End If
                If clsDefaultConfiguration.IsTablet Then
                    If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
                        Dim drp() = Process.GetProcessesByName("osk")
                        If drp.Length > 0 Then
                            Dim proc As New Process
                            For Each pr As Process In Process.GetProcesses()
                                If pr.ProcessName = "osk" Then
                                    pr.Kill()
                                End If

                            Next
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM038"), "CM038 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        Try
            Dim Month, day, year, Quarter As Int32
            Month = clsAdmin.DayOpenDate.Month
            day = clsAdmin.DayOpenDate.Day
            year = clsAdmin.DayOpenDate.Year
            If HoldFlag = True AndAlso dgMainGrid.Rows.Count > 1 Then
                ShowMessage(getValueByKey("CM035"), "CM035 - " & getValueByKey("CLAE04"))
                'ShowMessage("Hold Your bill first", "information")

                Exit Sub
            End If
            IsRoundOffMsg = False
            IsCSTApplicable = False
            clsDefaultConfiguration.CSTTaxCode = ""

            CMbtnBottom.CtrlBtnStockCheck.Enabled = True
            CMbtnBottom.CtrlBtnReturn.Enabled = True
            CMbtnBottom.CtrlBtnAddExtraCost.Enabled = True
            CMbtnBottom.CtrlBtnHomeDelivery.Enabled = True
            cmdCreditSale.Enabled = False
            cmdDelete.Enabled = False
            HDPrintRequired = False
            CustomerSaleType = 0

            C1Ribbon1.DbtnF12.Enabled = True
            C1Ribbon1.DbtnF2.Enabled = True
            customerType = String.Empty

            Dim docno As String = objCM.getDocumentNo("CM", clsAdmin.SiteCode)
            If dgMainGrid.Rows.Count > 1 Then
                If MsgBox(getValueByKey("CM036"), MsgBoxStyle.YesNo, "CM036 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                    CLPCustomerId = String.Empty
                    ClearData()
                    SetButtons(1, False)
                    CreateNewRecord(Me, dsMain, "CASHMEMOHDR")
                    UpdateFlag = False
                    ProductImage.CtrlProductImages.Image = Nothing
                    cmdPayments.Enabled = False

                    If OnlineConnect = True Then
                        'Changed by Rohit to generate Document No. for proper sorting
                        Try
                            'If (clsDefaultConfiguration.CashMemoResetonDayClose = False) Then
                            '    docno = GenDocNo("CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                            'Else
                            '    docno = GenDocNo("CM" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, docno)
                            'End If
                            If (clsDefaultConfiguration.CashMemoResetonDayClose = False) Then
                                ''GST changes by ketan add sitecode 3 digit in billno
                                ' docno = GenDocNo("CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                                docno = GenDocNo("C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 3, 3) & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                            Else
                                ''GST changes by ketan add sitecode 2 digit in billno
                                ' docno = GenDocNo("CM" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, docno)
                                docno = GenDocNo("C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, docno)

                            End If
                            lblCMNo.Text = docno
                        Catch ex As Exception
                            lblCMNo.Text = "C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 3, 3) & docno
                        End Try

                        Try
                            rbnLStatus.SmallImage = Spectrum.My.Resources.connected1.ToBitmap
                            rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus2")
                        Catch ex As Exception

                        End Try

                        'End Change by Rohit
                    Else
                        'Changed by Rohit to generate Document No. for proper sorting
                        Try
                            If (clsDefaultConfiguration.CashMemoResetonDayClose = False) Then
                                docno = GenDocNo("OCM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                            Else
                                docno = GenDocNo("OCM" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, docno)
                            End If
                            lblCMNo.Text = docno
                        Catch ex As Exception
                            lblCMNo.Text = "OCM" & clsAdmin.TerminalID & docno
                        End Try

                        Try
                            rbnLStatus.SmallImage = Spectrum.My.Resources.disconnected1.ToBitmap
                            rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
                        Catch ex As Exception

                        End Try
                        'End Change by Rohit
                    End If

                    'lblCMDate.Text = Now.Date
                    CtrlSalesPersons.CtrlCmdSearch.Enabled = True
                    cmdHold.Enabled = True
                    ProductImage.CtrlProductImages.BackgroundImage = Nothing
                    ''' added shifted by Mahesh only for new record yes then should enabled ...(Rakesh sir instruction)
                    cmdCustomerinfo.Enabled = True
                    CtrlSalesPersons.CtrlTxtBox.Enabled = True
                    CtrlSalesPersons.CtrlSalesPersons.Enabled = True
                Else
                    Payment.Visible = True
                    cmdCard.Enabled = False
                    cmdCash.Enabled = False
                    cmdCreditSale.Enabled = False

                    cmdCheque.Enabled = False
                    CMbtnBottom.CtrlBtnSaleGV.Enabled = False
                    CMbtnBottom.CtrlBtnHomeDelivery.Enabled = False
                    CMbtnBottom.CtrlBtnSaleCLPPoint.Enabled = False
                    CMbtnBottom.CtrlBtnStockCheck.Enabled = False
                    CMbtnBottom.CtrlBtnReturn.Enabled = False
                    CMbtnBottom.CtrlBtnAddExtraCost.Enabled = False
                    CMbtnBottom.CtrlBtnHomeDelivery.Enabled = False
                    C1Ribbon1.DbtnF12.Enabled = False
                    C1Ribbon1.DbtnF2.Enabled = False
                    cmdDelete.Enabled = True
                    IsMembership = True
                    Exit Sub
                End If
            Else
                CLPCustomerId = String.Empty
                ClearData()
                UpdateFlag = False
                SetButtons(1, False)
                CreateNewRecord(Me, dsMain, "CASHMEMOHDR")

                ProductImage.CtrlProductImages.Image = Nothing
                If OnlineConnect = True Then
                    'Changed by Rohit to generate Document No. for proper sorting
                    Try
                        'If (clsDefaultConfiguration.CashMemoResetonDayClose = False) Then
                        '    docno = GenDocNo("CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                        'Else
                        '    docno = GenDocNo("CM" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, docno)
                        'End If
                        If (clsDefaultConfiguration.CashMemoResetonDayClose = False) Then
                            ''GST changes by ketan add sitecode 2 digit in billno
                            ' docno = GenDocNo("CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                            docno = GenDocNo("C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 3, 3) & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                        Else
                            ''GST changes by ketan  add sitecode 2 digit in billno
                            'docno = GenDocNo("C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, docno)
                            ' docno = GenDocNo("C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, docno)
                            '  docno = GenDocNo("C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, docno)
                            '  docno = GenDocNo("C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 3, 3) & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                            docno = GenDocNo("C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, docno)
                        End If

                        lblCMNo.Text = docno
                    Catch ex As Exception
                        lblCMNo.Text = "C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 3, 3) & docno
                    End Try

                    Try
                        rbnLStatus.SmallImage = Spectrum.My.Resources.connected1.ToBitmap
                        rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus2")
                    Catch ex As Exception

                    End Try

                    'End Change by Rohit
                Else
                    'Changed by Rohit to generate Document No. for proper sorting
                    Try
                        If (clsDefaultConfiguration.CashMemoResetonDayClose = False) Then
                            docno = GenDocNo("OCM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                        Else
                            docno = GenDocNo("OCM" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, docno)
                        End If
                        lblCMNo.Text = docno
                    Catch ex As Exception
                        lblCMNo.Text = "OCM" & clsAdmin.TerminalID & docno
                    End Try

                    Try
                        rbnLStatus.SmallImage = Spectrum.My.Resources.disconnected1.ToBitmap
                        rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus1")
                    Catch ex As Exception

                    End Try

                    'End Change by Rohit
                End If
                'lblCMDate.Text = Now.Date
                CtrlSalesPersons.CtrlCmdSearch.Enabled = True
                CtrlSalesPersons.AndroidSearchTextBox.Enabled = True
                CtrlSalesPersons.CtrlTxtBox.Enabled = True
                CtrlSalesPersons.CtrlSalesPersons.Enabled = True
                cmdHold.Enabled = True
                ProductImage.CtrlProductImages.BackgroundImage = Nothing
            End If

            If clsDefaultConfiguration.CLPIntimation = True Then
                ShowMessage(getValueByKey("CM037"), "CM037 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please ask For CLP Card....", "Information")
            End If
            If clsDefaultConfiguration.ManualPromotionAllowed = True Then
                'cmdEnable.Text = "Enable(Ctrl+B)"
                cmdEnable.Text = getValueByKey("frmcashmemo.cmdenable")
                cmdEnable.Tag = "E"
                cmdEnable_Click(sender, e)
            Else
                'cmdEnable.Text = "Disable(Ctrl+B)"
                cmdEnable.Text = getValueByKey("frmcashmemo.cmddisable")
                cmdEnable.Tag = "D"
                cmdEnable_Click(sender, e)
            End If

            cbManualDisc.Enabled = False
            cmdEnable.Text = getValueByKey("frmcashmemo.cmdenable")
            cmdPayments.Enabled = False
            If (objCM.CheckHoldBillData(clsAdmin.SiteCode)) Then
                cmdHold.Enabled = True
            Else
                cmdHold.Enabled = False
            End If

            CMbtnBottom.CtrlBtnSaleCLPPoint.Enabled = clsDefaultConfiguration.AdvanceSalesAllowed
            CMbtnBottom.CtrlBtnSaleGV.Enabled = clsDefaultConfiguration.GVsaleAllowed
            rbnbtnApplyCST.Enabled = clsDefaultConfiguration.IsCstTaxRequired

            Payment.Visible = False
            If clsDefaultConfiguration.SalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0 Then
                CtrlSalesPersons.CtrlSalesPersons.SelectedValue = clsAdmin.UserCode
            End If
            If clsDefaultConfiguration.IsBillScanApplicable Then
                dgMainGrid.Cols("Quantity").AllowEditing = True
            End If
            CustSaleTypeTimer.Stop()
            lblCustSaleType.Visible = False
            HideColumns(dgMainGrid, False, "TAKEAWAYQUANTITY")
            If clsDefaultConfiguration.IsMembership Then
                CustInfo.CtrlTxtCustomerNo.ReadOnly = False
                CustInfo.CtrlTxtSwape.ReadOnly = False
            End If
            ' CtrlSalesPersons.CtrlTxtBox.Select()
            If clsDefaultConfiguration.IsMembership Then
                CustInfo.CtrlTxtSwape.Select()
            Else
                CtrlSalesPersons.AndroidSearchTextBox.Select()
            End If
            _billAmt = 0
            _paidAmt = 0
            _remarks = ""
            GVBasedAricleReturnList.Clear()
            IsRoundOffMsg = False
            IsMembership = False
            '--- Credit sales return labels hide by default
            CashSummary.lbltxtVisible5 = False
            CashSummary.lblVisible5 = False
            If clsDefaultConfiguration.IsPoleDisply = True Then
                TwoLineExtendScreen()
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                CustInfo.BringToFront()
            End If
            'added for clear button
            If dtHD IsNot Nothing Then
                If dtHD.Rows.Count > 0 Then
                    dtHD.Clear()
                End If
            End If



            CustInfo.BtnClearCustmInfo.Enabled = True
        Catch ex As Exception
            ShowMessage(getValueByKey("CM038"), "CM038 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Transaction intialization is not Sucessfull", "Error")
        End Try

    End Sub

    Private Sub dgMainGrid_AfterSelChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles dgMainGrid.AfterSelChange
        calculateTotalbill()
        If clsDefaultConfiguration.IsPoleDisply = True Then
            Dim selectedrow As Integer = dgMainGrid.RowSel - 1
            TwoLineExtendScreen(selectedrow)
        End If
    End Sub

    Private Sub dgMainGrid_BeforeEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgMainGrid.BeforeEdit
        Try
            If dgMainGrid.Rows.Count > 1 AndAlso dgMainGrid.Rows(e.Row)("BTYPE") = "R" AndAlso e.Row = dgMainGrid.RowSel Then
                dgMainGrid.Cols("QUANTITY").AllowEditing = False
                dgMainGrid.Cols("CLPREQUIRE").AllowEditing = False
            ElseIf dgMainGrid.Rows.Count > 1 AndAlso dgMainGrid.Rows(e.Row)("BTYPE") = "S" AndAlso e.Row = dgMainGrid.RowSel Then
                dgMainGrid.Cols("CLPREQUIRE").AllowEditing = True

                If clsDefaultConfiguration.IsBillScanApplicable Then
                    If (dsMain.Tables("CashMemoMettler") IsNot Nothing AndAlso dsMain.Tables("CashMemoMettler").Rows.Count > 0) Then
                        Exit Sub
                    End If
                End If
                dgMainGrid.Cols("QUANTITY").AllowEditing = True

            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub dgMainGrid_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgMainGrid.CellButtonClick
        Try

            '---- This is called only for delete button --Code Added By Mahesh for block Item delete if some rows is added by Using scanbill ...
            'If clsDefaultConfiguration.IsBillScanApplicable Then
            '    If (dsMain.Tables("CashMemoMettler") IsNot Nothing AndAlso dsMain.Tables("CashMemoMettler").Rows.Count > 0) Then
            '        Exit Sub
            '    End If
            'End If

            Dim discountAmount As Decimal = Decimal.Zero
            discountAmount = dsMain.Tables("CASHMEMODTL").Compute("Sum(TotalDiscount)", String.Empty)
            If clsDefaultConfiguration.IsMembership Then discountAmount = Decimal.Zero
            If (discountAmount > 0) Then
                If MsgBox(getValueByKey("CM060"), MsgBoxStyle.OkCancel, "CM060") = MsgBoxResult.Ok Then

                    'dsMain.Tables("CASHMEMODTL").Rows.RemoveAt(dgMainGrid.Row - 1)
                    If Not dtMainTax Is Nothing AndAlso dtMainTax.Rows.Count > 0 Then
                        Dim dv As New DataView(dtMainTax, "ArticleCode='" & dgMainGrid.Rows(e.Row)("ArticleCode") & "' AND EAN='" & dgMainGrid.Rows(e.Row)("EAN") & "'", "", DataViewRowState.CurrentRows)
                        If dv.Count > 0 Then
                            dv.AllowDelete = True
                            For Each dr As DataRowView In dv
                                dr.Delete()
                            Next
                        End If
                        dtMainTax.AcceptChanges()
                    End If
                    If Not dtGV Is Nothing Then
                        If dgMainGrid.Rows(e.Row)("Section") Is DBNull.Value Then
                        Else
                            Dim dv As New DataView(dtGV, "IssuedDocNumber=" & dgMainGrid.Rows(e.Row)("Section").ToString(), "", DataViewRowState.CurrentRows)
                            If dv.Count > 0 Then
                                dv.AllowDelete = True
                                For Each dr As DataRowView In dv
                                    dr.Delete()
                                Next
                            End If
                            dtGV.AcceptChanges()
                        End If
                    End If
                    dgMainGrid.Rows.Remove(dgMainGrid.Row)
                    RemoveDeletedRow(dsMain.Tables("CASHMEMODTL"))
                    If clsDefaultConfiguration.customerwisepricemanagement = False Then
                        cmdClrAllPromo_Click("ClearPromWithoutMessage", EventArgs.Empty)
                    End If


                    ProductImage.CtrlProductImages.BackgroundImage = Nothing
                    If dgMainGrid.Rows.Count > 1 Then
                        Dim strArticle As String = dgMainGrid.Rows(dgMainGrid.RowSel)("ArticleCode").ToString()
                        ProductImage.ShowArticleImage(strArticle)
                    End If
                    CreatingLineNO(dsMain, "CashMemoDtl")
                    'code added  by vipul for Customer wise discount
                    If clsDefaultConfiguration.customerwisepricemanagement = True Then
                        calculateTotalbill()
                    End If
                End If
            Else
                'dsMain.Tables("CASHMEMODTL").Rows.RemoveAt(dgMainGrid.Row - 1)
                If Not dtMainTax Is Nothing AndAlso dtMainTax.Rows.Count > 0 Then
                    Dim dv As New DataView(dtMainTax, "ArticleCode='" & dgMainGrid.Rows(e.Row)("ArticleCode") & "' AND EAN='" & dgMainGrid.Rows(e.Row)("EAN") & "'", "", DataViewRowState.CurrentRows)
                    If dv.Count > 0 Then
                        dv.AllowDelete = True
                        For Each dr As DataRowView In dv
                            dr.Delete()
                        Next
                    End If
                    dtMainTax.AcceptChanges()
                End If
                If Not dtGV Is Nothing Then
                    If dgMainGrid.Rows(e.Row)("Section") Is DBNull.Value Then
                    Else
                        Dim dv As New DataView(dtGV, "IssuedDocNumber=" & dgMainGrid.Rows(e.Row)("Section").ToString(), "", DataViewRowState.CurrentRows)
                        If dv.Count > 0 Then
                            dv.AllowDelete = True
                            For Each dr As DataRowView In dv
                                dr.Delete()
                            Next
                        End If
                        dtGV.AcceptChanges()
                    End If
                End If
                If dgMainGrid.Rows(dgMainGrid.RowSel)("ArticleCode").ToString() = "GVBaseArticle" AndAlso GVBasedAricleReturnList.Count > 0 Then
                    GVBasedAricleReturnList.Clear()
                End If

                dgMainGrid.Rows.Remove(dgMainGrid.Row)
                RemoveDeletedRow(dsMain.Tables("CASHMEMODTL"))

                ProductImage.CtrlProductImages.BackgroundImage = Nothing
                If dgMainGrid.Rows.Count > 1 Then
                    Dim strArticle As String = dgMainGrid.Rows(dgMainGrid.RowSel)("ArticleCode").ToString()
                    ProductImage.ShowArticleImage(strArticle)
                End If
                CreatingLineNO(dsMain, "CashMemoDtl")

                calculateTotalbill()
            End If
            If clsDefaultConfiguration.IsPoleDisply = True Then
                TwoLineExtendScreen()
            End If
            If (dgMainGrid.Rows.Count > 1) Then
                dgMainGrid.Select(dgMainGrid.Rows.Count - 1, 2)
            End If

            'CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Select()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Dim _20x2Line As SerialPort
    Dim _tempItemName As String = ""
    Dim _tempItemAmount As String = ""
    Dim _tempItemQty As String = "0"
    Dim _totalItems As String = ""
    Dim _tatalItemsQty As String = ""
    Dim _totalItemsAmount As String = ""
    Dim dtExtend As New DataTable
    Function TwoLineExtendScreen(Optional ByVal rowNumber As Integer = 0, Optional ByVal Ispayment As Boolean = False) As Integer
        _tempItemName = ""
        _tempItemQty = "0.00"
        _tempItemAmount = ""
        Try
            Dim dgMain As DataTable = dgMainGrid.DataSource
            '  Dim dgMain As DataTable
            If dgMain.Rows.Count = 0 Then
                dtExtend.Rows.Clear()
            End If
            For Each dr As DataRow In dgMain.Rows
                Dim result As DataRow() = dtExtend.Select("ArticleCode='" + dr("ArticleCode").ToString() & "'")
                If result.Count = 0 Then
                    Dim row = dtExtend.NewRow()
                    row("SrNo") = dr("BillLineNo")
                    row("ArticleCode") = dr("ArticleCode")
                    row("ArticleDesc") = dr("Discription")
                    row("Qty") = FormatNumber(dr("Quantity"), 3)
                    row("Price") = dr("SellingPrice")
                    row("DiscAmt") = FormatNumber(dr("TotalDiscount"), 2)
                    row("DiscPer") = FormatNumber(dr("TotalDiscPercentage"), 2)
                    row("Tax") = FormatNumber(dr("TotalTaxAmount"), 2)
                    row("Gross") = FormatNumber(dr("GrossAmt"), 2)
                    dtExtend.Rows.Add(row)
                Else
                    result(0)("Qty") = FormatNumber(dr("Quantity"), 3)
                    result(0)("DiscAmt") = FormatNumber(dr("TotalDiscount"), 2)
                    result(0)("DiscPer") = FormatNumber(dr("TotalDiscPercentage"), 2)
                    result(0)("Tax") = FormatNumber(dr("TotalTaxAmount"), 2)
                    result(0)("Gross") = FormatNumber(dr("GrossAmt"), 2)
                End If
            Next
            'Dim _totalItems As String = ""
            'Dim _tatalItemsQty As String = ""
            'Dim _totalItemsAmount As String = ""
            Dim _ItemPrice As String = ""
            If dgMain.Rows.Count >= 1 Then
                _tempItemName = dgMain.Rows(rowNumber)("Discription")
                _tempItemQty = dgMain.Rows(rowNumber)("Quantity")
                _tempItemAmount = dgMain.Rows(rowNumber)("NetAmount")
                _ItemPrice = dgMain.Rows(rowNumber)("SELLINGPRICE")

                _totalItems = lblCalTotalItem.Text
                _tatalItemsQty = lblCalTotalItemQty.Text
                _totalItemsAmount = lblCalTotalAmount.Text
            Else
                _totalItems = ""
                _tatalItemsQty = ""
                _totalItemsAmount = ""
            End If
            Dim ComPortName As String = clsDefaultConfiguration.SerialPort
            If _tempItemAmount <> "" Then
                Dim CheckTempAmt As Integer = _tempItemAmount.LastIndexOf(".") + 1
                Dim strCheckTempAmtValue As String = _tempItemAmount.Substring(CheckTempAmt, _tempItemAmount.Length - CheckTempAmt)
                If strCheckTempAmtValue = "000" Or strCheckTempAmtValue = "00" Or strCheckTempAmtValue = "0" Then
                    _tempItemAmount = _tempItemAmount.Remove(CheckTempAmt - 1)
                End If
            End If
            Dim custStr1 As String = ""
            Dim custStr2 As String = ""
            If Ispayment = False Then
                If _ItemPrice <> "" Then
                    Dim CheckTempAmt As Integer = _ItemPrice.LastIndexOf(".") + 1
                    Dim strCheckTempAmtValue As String = _ItemPrice.Substring(CheckTempAmt, _ItemPrice.Length - CheckTempAmt)
                    If strCheckTempAmtValue = "000" Or strCheckTempAmtValue = "00" Or strCheckTempAmtValue = "0" Then
                        _ItemPrice = _ItemPrice.Remove(CheckTempAmt - 1)
                    End If
                End If
                If clsDefaultConfiguration.IsPoleDisply = True Then
                    custStr1 = Strings.Mid(_tempItemName, 1, 20) + Strings.Space(20 - Strings.Len(Strings.Mid(_tempItemName, 1, 20)))
                Else
                    custStr1 = Strings.Mid(_tempItemName, 1, 11) + Strings.Space(20 - Strings.Len(Strings.Mid(_tempItemName, 1, 11)) - Strings.Len(Strings.Mid(_tempItemAmount, 1, 7))) + Strings.Mid(_tempItemAmount, 1, 7)
                End If



                Dim _spaceChar As Integer = 0

                If _totalItems = "" AndAlso _tatalItemsQty = "" AndAlso _totalItemsAmount = "" Then
                    ' _spaceChar = SpaceChar(_totalItems, _tatalItemsQty, _totalItemsAmount)
                    ' Dim custStr2 = Strings.Mid(_totalItems, 1, 3) + Strings.Mid("          ", 1, _spaceChar) + Strings.Mid(_tatalItemsQty, 1, 7) + Strings.Space(20 - Strings.Len(Strings.Mid(_totalItems, 1, 3)) - Strings.Len(Strings.Mid("          ", 1, _spaceChar)) - Strings.Len(Strings.Mid(_tatalItemsQty, 1, 7)) - Strings.Len(Strings.Mid(_totalItemsAmount, 1, 4))) + Strings.Mid(_totalItemsAmount, 1, 4)
                    custStr2 = Strings.Mid(_totalItems, 1, 7) + Strings.Space(20 - Strings.Len(Strings.Mid(_totalItems, 1, 7)) - Strings.Len(Strings.Mid(_totalItemsAmount, 1, 7))) + Strings.Mid(_totalItemsAmount, 1, 7)
                    clsTwoLineDisplay.ClearDisplay20x2Line(ComPortName)
                    Display20x2Line(custStr1, custStr2, ComPortName)
                Else
                    '  _spaceChar = SpaceChar(_totalItems, _tatalItemsQty, _totalItemsAmount)
                    _spaceChar = SpaceChar(_tempItemQty, _ItemPrice, _tempItemAmount)
                    If clsDefaultConfiguration.IsPoleDisply = True Then
                        Dim f = Strings.Space(6 - Strings.Len(Strings.Mid(_tempItemAmount, 1, 6))) + Strings.Mid(_totalItemsAmount, 1, 5)
                        custStr2 = Strings.Mid(_tempItemQty, 1, 7) + Strings.Space(8 - Strings.Len(Strings.Mid(_tempItemQty, 1, 7))) + Strings.Mid(_ItemPrice, 1, 5) + Strings.Space(6 - Strings.Len(Strings.Mid(_ItemPrice, 1, 5))) + f
                    Else
                        custStr2 = Strings.Mid(_totalItems, 1, 3) + Strings.Mid("          ", 1, _spaceChar) + Strings.Mid(_tatalItemsQty, 1, 7) + Strings.Space(20 - Strings.Len(Strings.Mid(_totalItems, 1, 3)) - Strings.Len(Strings.Mid("          ", 1, _spaceChar)) - Strings.Len(Strings.Mid(_tatalItemsQty, 1, 7)) - Strings.Len(Strings.Mid(_totalItemsAmount, 1, 5))) + Strings.Mid(_totalItemsAmount, 1, 5)
                    End If

                End If
            Else
                If clsDefaultConfiguration.IsPoleDisply = True Then
                    custStr1 = Strings.Space(1) + "Total Bill Amount" + Strings.Space(1)
                    custStr2 = Strings.Space(5) + Strings.Mid(_totalItemsAmount, 1, 10) + Strings.Space(5)
                End If

            End If
            ' Dim custStr2 = Strings.Mid(_totalItems, 1, 3) + Strings.Mid("          ", 1, _spaceChar) + Strings.Mid(_tatalItemsQty, 1, 7) + Strings.Space(20 - Strings.Len(Strings.Mid(_totalItems, 1, 3)) - Strings.Len(Strings.Mid("          ", 1, _spaceChar)) - Strings.Len(Strings.Mid(_tatalItemsQty, 1, 7)) - Strings.Len(Strings.Mid(_totalItemsAmount, 1, 5))) + Strings.Mid(_totalItemsAmount, 1, 5)
            ' Dim custStr2 = Strings.Mid(_totalItems, 1, 2) + Strings.Mid("    ", 1, 2) + Strings.Mid(_tatalItemsQty, 1, 7) + Strings.Mid("    ", 1, 2) + Strings.Mid(_totalItemsAmount, 1, 7)
            clsTwoLineDisplay.ClearDisplay20x2Line(ComPortName)

            Display20x2Line(custStr1, custStr2, ComPortName)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Sub Display20x2Line(ByVal line1String As String, ByVal line2String As String, ByVal portname As String, Optional ByVal ClearScreen As Boolean = False)

        Try
            _20x2Line = New SerialPort()
            _20x2Line.PortName = portname
            _20x2Line.BaudRate = 9600
            _20x2Line.DataBits = 8
            _20x2Line.StopBits = StopBits.One
            _20x2Line.Parity = Parity.None
            _20x2Line.ReadTimeout = 5000
            _20x2Line.Handshake = Handshake.None
            _20x2Line.WriteTimeout = 500

            If _20x2Line.IsOpen = False Then ' Checks whether port is already OPEN or Close 
                _20x2Line.Open()             'If COM port is not opened then OPEN for communication

                '_20x2Line.Write("\x0c")                   ' 0x0c hex value to clear the display before sending any message to Display

                _20x2Line.Write(line1String + line2String)

                If _20x2Line.IsOpen = True Then           ' Checks whether port is already OPEN, If OPEN then Close it before exiting the function
                    _20x2Line.Close()
                    _20x2Line.Dispose()
                    Console.ReadLine()
                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub dgMainGrid_Click(sender As Object, e As System.EventArgs) Handles dgMainGrid.Click
        If dgMainGrid.Rows.Count = 1 Then
            CtrlSalesPersons.AndroidSearchTextBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Focus()
        End If
    End Sub

    Private Sub dgMainGrid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgMainGrid.KeyDown
        Try
            If UpdateFlag = False And e.KeyCode = Keys.Delete Then

                'If clsDefaultConfiguration.IsBillScanApplicable Then
                '    If (dsMain.Tables("CashMemoMettler") IsNot Nothing AndAlso dsMain.Tables("CashMemoMettler").Rows.Count > 0) Then
                '        Exit Sub
                '    End If
                'End If
                If dgMainGrid.Rows(dgMainGrid.RowSel)("ArticleCode").ToString() = "GVBaseArticle" AndAlso GVBasedAricleReturnList.Count > 0 Then
                    GVBasedAricleReturnList.Clear()
                End If
                If (dgMainGrid.Rows.Count > 1) Then

                    dgMainGrid.Rows.Remove(dgMainGrid.Row)
                    'dsMain.Tables("CASHMEMODTL").AcceptChanges()
                    RemoveDeletedRow(dsMain.Tables("CASHMEMODTL"))
                    CreatingLineNO(dsMain, "CashMemoDtl")
                    calculateTotalbill()

                    If (dgMainGrid.Rows.Count > 1) Then
                        dgMainGrid.Select(1, 2)
                    End If
                End If
                'CtrlSalesPersons.CtrlTxtBox.Select()
                'CtrlSalesPersons.CtrlTxtBox.Focus()
                CtrlSalesPersons.AndroidSearchTextBox.Select()
                CtrlSalesPersons.AndroidSearchTextBox.Focus()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    'Private Sub dgMainGrid_MouseEnterCell(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgMainGrid.MouseEnterCell
    '    Try
    '        If e.Col = dgMainGrid.Cols("TOTALDISCPERCENTAGE").Index And e.Row <> 0 And dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE").ToString() <> String.Empty Then
    '            'Dim x As Integer = pnlDiscountAmt.Location.X + e.Row
    '            'Dim Y As Integer = pnlDiscountAmt.Location.Y + e.Row
    '            'pnlDiscountAmt.Location = New Point(x, Y)
    '            Dim value As Double = CalculateDiscountAmount(e.Row, e.Col)
    '            Dim rg As Rectangle = dgMainGrid.GetCellRect(e.Row, e.Col)
    '            pnlDiscountAmt.Left = rg.Right + dgMainGrid.Cols("TOTALDISCPERCENTAGE").Width
    '            pnlDiscountAmt.Top = rg.Y + 150

    '            lblDiscAmount.Text = value
    '            pnlDiscountAmt.Visible = True
    '            'lblDiscAmount.Text
    '        Else
    '            pnlDiscountAmt.Visible = False
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub dgMainGrid_MouseLeaveCell(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgMainGrid.MouseLeaveCell
    '    pnlDiscountAmt.Visible = False
    'End Sub
    Private Sub cmdEnable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEnable.Click
        Try
            If cmdEnable.Tag = "E" Then
                If clsDefaultConfiguration.ManualPromotionAllowed = True Then
                    cbManualDisc.Enabled = True
                    'cmdEnable.Text = "Disable(Ctrl+B)"
                    cmdEnable.Text = getValueByKey("frmcashmemo.cmddisable")
                    cbManualDisc.SelectedIndex = -1
                    cbManualDisc.Select()
                    cmdEnable.Tag = "D"
                    'cbManualDisc.Focus()
                End If
            Else
                cbManualDisc.Enabled = False
                'cmdEnable.Text = "Enable(Ctrl+B)"
                cmdEnable.Text = getValueByKey("frmcashmemo.cmdenable")
                cmdEnable.Tag = "E"
            End If

            'CtrlSalesPersons.CtrlTxtBox.Select()
            'CtrlSalesPersons.CtrlTxtBox.Focus()
            CtrlSalesPersons.AndroidSearchTextBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Focus()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function cmdGiftPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) As Boolean
        Try
            If dgMainGrid.Rows.Count > 1 Then
                Dim billNo As String = lblCMNo.Text
                '  Dim numbr As String = billNo
                If Not cmdSavePrint_Click(sender, e) Then
                    Return False
                End If
                ' billNo = numbr
                Dim obj As New clsCashMemoPrint(billNo, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
                obj.DisplayArticleFullName = clsDefaultConfiguration.PrintItemFullName
                obj.AllowDecimalQty = clsDefaultConfiguration.AllowDecimalQty
                obj.WeightScaleEnabled = clsDefaultConfiguration.WeightScaleEnabled

                Dim ErrorMsg As String = ""
                If Not clsDefaultConfiguration.IsMembership Then
                    'obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMWAmt", "", "", "", ErrorMsg, GiftMsg)
                    ' obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMWAmt", "", "", "", ErrorMsg, GiftMsg, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6) '0000413   code added by irfan on 18/9/2017 visiblity of hsn and tax
                    If clsDefaultConfiguration.PrintFormatNo = 1 Or clsDefaultConfiguration.PrintFormatNo = 2 Or clsDefaultConfiguration.PrintFormatNo = 3 Or clsDefaultConfiguration.PrintFormatNo = 0 Then
                        obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMWAmt", "", "", "", ErrorMsg, GiftMsg, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6) '0000413    code added by irfan on 9/8/2017 against tender visiblity   code added by irfan on 11/09/2017 visiblity of hsn and tax
                    End If

                End If
                If ErrorMsg <> String.Empty Then
                    ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
                End If
                GiftMsg = ""
                Return True
            End If
            objCM.ShiftManagementForCM = False
            Dim dt As DataTable = objCM.GetOldCashMemo(clsAdmin.SiteCode, NoOfDaysForReprint:=clsDefaultConfiguration.NumberOfDaysApplicableForReprint, NewCustmor:=clsDefaultConfiguration.DetailedCustomerCreationformat)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Dim objSearch As New frmNCommonSearch
                objSearch.RequestFromPage = enumOperationOnBill.GiftPrint
                objSearch.SetData = dt
                objSearch.ShowDialog()
                If Not objSearch.search Is Nothing AndAlso objSearch.search.Length > 0 Then
                    Dim obj As New clsCashMemoPrint(objSearch.search(0).Trim, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
                    obj.DisplayArticleFullName = clsDefaultConfiguration.PrintItemFullName
                    obj.AllowDecimalQty = clsDefaultConfiguration.AllowDecimalQty
                    obj.WeightScaleEnabled = clsDefaultConfiguration.WeightScaleEnabled

                    Dim ErrorMsg As String = ""
                    'obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMWAmt", "", "", "", ErrorMsg)
                    obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMWAmt", "", "", "", ErrorMsg, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6) '0000413   code added by irfan on 9/9/2017 visiblity of hsn and tax
                    If ErrorMsg <> String.Empty Then
                        ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
                    End If
                    ErrorMsg = ""
                    ' obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "", "", "", ErrorMsg)
                    ' obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "", "", "", ErrorMsg, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6) '0000413   code added by irfan on 18/9/2017 visiblity of hsn and tax
                    'modified by khusrao adil on 8-12-2017 for jk sprint 32
                    'JKPrintFormatEnable flag added
                    obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "", "", "", ErrorMsg, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6, JKPrintFormatEnable:=clsDefaultConfiguration.JKPrintFormatEnable) '0000413  code added by irfan against tender visblity    code added by irfan on 11/09/2017 visiblity of hsn and tax) '0000413
                    If ErrorMsg <> String.Empty Then
                        ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
                    End If
                    Return True
                End If
            End If

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Try
            Dim authUserID = "", authRemarks As String = ""
            Dim dts As DataTable = objCM.GetOldCashMemo(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID, clsAdmin.UserCode, clsCommon._PrevShiftId, NoOfDaysForReprint:=clsDefaultConfiguration.NumberOfDaysApplicableForReprint, NewCustmor:=clsDefaultConfiguration.DetailedCustomerCreationformat)
            If Not dts Is Nothing Then
                If dts.Rows.Count = 0 Then
                    Exit Sub
                End If
            Else
                Exit Sub
            End If

            If CheckInterTransactionAuth("DeleteBill", dsMain.Tables("CASHMEMOHDR"), authUserID:=authUserID, authRemarks:=authRemarks, IsCalledFromCashMemo:=True) = True Then
                If OnlineConnect = False Then
                    ShowMessage(getValueByKey("CMO50"), "CMO50 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
                If UpdateFlag = False Then
                    ShowMessage(getValueByKey("CM039"), "CM039 - " & getValueByKey("CLAE04"))
                    'ShowMessage("First Select a Cash Memo for Delete", "Information")
                    Exit Sub
                End If

                Dim billNo, StrReason As String
                calculateTotalbill()
                'Dim dt As DataTable = objCM.GetReasons("CMS")
                'Dim objReason As New frmNCommonSearch
                'objReason.RequestFromPage = enumOperationOnBill.VoidBill
                'objReason.SetData = dt
                'objReason.ArtRemark = ""
                'objReason.ShowDialog()
                'authRemarks = objReason.ArtRemark
                ''---------------
                'If authRemarks = "" Then
                '    Exit Sub
                'End If

                'If objReason.search Is Nothing Then Exit Sub
                'If Not objReason.search Is Nothing Then
                '    StrReason = objReason.search(0).ToString()
                'End If
                Dim DeletTime As DateTime = objCM.GetCurrentDate()
                Dim AuthUser As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("AuthUserId").ToString()
                billNo = dsMain.Tables("CASHMEMOHDR").Rows(0)("Billno").ToString()
                dsMain.Tables("CASHMEMOHDR").Rows(0)("DeletionReason") = StrReason
                dsMain.Tables("CASHMEMOHDR").Rows(0)("BILLINTERMEDIATESTATUS") = "Deleted"
                dsMain.Tables("CASHMEMOHDR").Rows(0)("DeletionDate") = DeletTime
                dsMain.Tables("CASHMEMOHDR").Rows(0)("DeletionTime") = DeletTime
                dsMain.Tables("CASHMEMOHDR").Rows(0)("Updatedon") = DeletTime
                dsMain.Tables("CASHMEMOHDR").Rows(0)("Updatedby") = clsAdmin.UserCode
                dsMain.Tables("CASHMEMOHDR").Rows(0)("UpdatedAt") = clsAdmin.SiteCode
                dsMain.Tables("CASHMEMOHDR").Rows(0)("AuthUserId") = authUserID
                dsMain.Tables("CASHMEMOHDR").Rows(0)("AuthUserRemarks") = authRemarks
                If objCM.CheckVoucherRedemmed(clsAdmin.SiteCode, billNo, "CMS") = False Then
                    ShowMessage(getValueByKey("CM058"), "CM058 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
                Dim ds As DataSet = dsMain.Copy()
                If objCM.DeleteBill(ds, clsAdmin.SiteCode, clsAdmin.UserCode, clsDefaultConfiguration.StockStorageLocation) = True Then
                    If dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNo").ToString <> "" Then
                        Dim totalPoints As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPPoints").ToString()
                        If Val(totalPoints) <= 0 Then
                            totalPoints = dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPDiscount").ToString()
                        End If
                        Dim RedemptionPoints As Double = 0
                        For Each dr As DataRow In dsMain.Tables("CashMemoReceipt").Select("TenderTypeCode Like 'CLP%'", "", DataViewRowState.CurrentRows)
                            RedemptionPoints = IIf(dr("AmountTendered") Is DBNull.Value, 0, dr("AmountTendered"))
                        Next
                        If (totalPoints <> "" AndAlso CDbl(totalPoints) > 0) Or RedemptionPoints > 0 Then
                            If objCM.ReversedCLPPoints(clsAdmin.CLPProgram, dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNo").ToString(), totalPoints, RedemptionPoints, clsAdmin.SiteCode, clsAdmin.UserCode, dsMain.Tables("CASHMEMOHDR").Rows(0)("Billno").ToString(), dsMain.Tables("CASHMEMOHDR").Rows(0)("Billdate").ToString()) = False Then
                                ShowMessage(getValueByKey("CM053"), "CM053 - " & getValueByKey("CLAE04"))
                                Exit Sub
                            End If
                            'ElseIf RedemptionPoints > 0 Then
                            '    If objCM.UpdateClpPoints(False, clsAdmin.CLPProgram, dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNo").ToString(), RedemptionPoints, clsAdmin.SiteCode, clsAdmin.UserCode, dsMain.Tables("CASHMEMOHDR").Rows(0)("Billno").ToString(), dsMain.Tables("CASHMEMOHDR").Rows(0)("Billdate").ToString(), True) = False Then
                            '        ShowMessage("CLPData not reversed", "information")
                            '        Exit Sub
                            '    End If
                        End If
                    End If
                    Dim obj As New clsCashMemoPrint(billNo, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
                    obj.DisplayArticleFullName = clsDefaultConfiguration.PrintItemFullName
                    obj.AllowDecimalQty = clsDefaultConfiguration.AllowDecimalQty
                    obj.WeightScaleEnabled = clsDefaultConfiguration.WeightScaleEnabled
                    obj.PrintFormatNo = 1
                    Dim ErrorMsg As String = ""
                    '  obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "DCM", "", clsAdmin.UserCode, AuthUser, ErrorMsg)
                    obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "DCM", "", clsAdmin.UserCode, AuthUser, ErrorMsg, "", IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6) '0000413   code added by irfan on 18/9/2017 visiblity of hsn and tax

                    If ErrorMsg <> String.Empty Then
                        ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
                    End If
                    ClearData()
                    cmdNew_Click(cmdNew, e)
                    cmdCustomerinfo.Enabled = True
                    cmdCustomerinfo.Visible = True
                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                End If
                cmdPayments.Enabled = False
            Else
                cmdPayments.Enabled = True
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CM040"), "CM040 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error Deleting Cash Memo", "Error")
        End Try
    End Sub
    Private Sub cmdHold_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHold.Click
        Try

            'If OnlineConnect = False Then
            '    ShowMessage("Sorry this is not active in Offline", "Information")
            '    Exit Sub
            'End If
            If sender.Tag.ToString.ToUpper() = "HOLD" Then
                If UpdateFlag = True Then
                    ShowMessage(getValueByKey("CM047"), "CM047 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Old bill can't be Hold", "Information")
                    Exit Sub
                End If
                Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
                Dim BillNo As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("BillNo").ToString()
                If BillNo <> String.Empty Then
                    If objCM.CheckBillDataInHold(BillNo, clsAdmin.SiteCode) = True Then
                        objCM.DeleteHoldBill(BillNo, clsAdmin.SiteCode, "Hold")
                    End If
                End If
                Dim objHold As New frmSpecialPrompt(getValueByKey("SP001"))
                objHold.ShowTextBox = True
                objHold.txtValue.Text = lblCMNo.Text & "-" & CustInfo.CtrltxtCustomerName.Text
                objHold.AcceptButton = objHold.cmdOk
                objHold.AllowText = True
                objHold.ShowDialog()
                Dim str As String = objHold.GetResult()
                If str = String.Empty Then
                    Exit Sub
                End If
                '  If clsDefaultConfiguration.customerwisepricemanagement = False Then
                '  ClearAllPromo()
                'End If

                ReCalculateCM("")
                calculateTotalbill()
                Dim dsTemp As DataSet
                dsTemp = dsMain.Copy()
                Dim i As Int32 = 0
                For i = dsTemp.Tables.Count - 1 To 0 Step -1
                    If dsTemp.Tables(i).TableName = "CASHMEMOHDR" Or dsTemp.Tables(i).TableName = "CASHMEMODTL" Then
                        dsTemp.Tables(i).TableName = "Hold" & dsTemp.Tables(i).TableName
                    Else
                        'dsTemp.Tables.RemoveAt(i)
                        If dsTemp.Tables(i).Rows.Count > 0 Then
                            dsTemp.Tables(i).TableName = "Hold" & dsTemp.Tables(i).TableName
                        Else
                            dsTemp.Tables.RemoveAt(i)
                        End If
                    End If
                Next
                If dtGV IsNot Nothing Then
                    If dtGV.Rows.Count > 0 Then
                        Dim dtHGV As DataTable = dtGV.Copy()
                        dtHGV.TableName = "HOLDVOUCHER"
                        dsTemp.Tables.Add(dtHGV)
                    End If
                End If

                Dim salesPerson As String = IIf(CtrlSalesPersons.CtrlSalesPersons.SelectedValue IsNot Nothing, CtrlSalesPersons.CtrlSalesPersons.SelectedValue, String.Empty)
                CreatingLineNO(dsTemp, "HoldCASHMEMODTL")

                If CustInfo.CtrlTxtCustomerNo.Text <> String.Empty AndAlso customerType.Equals("SO") Then
                    dsTemp.Tables("HoldCashMemoHdr").Rows(0)("CLPNo") = DBNull.Value
                    dsTemp.Tables("HoldCashMemoHdr").Rows(0)("CustomerNo") = CustInfo.CtrlTxtCustomerNo.Text.Trim
                End If

                If objCM.HoldData(clsAdmin.DayOpenDate, dsTemp, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.UserCode, str, BillNo, salesPerson) Then
                    ClearData()
                    'cmdNew_Click(sender, e)
                    If clsDefaultConfiguration.HoldBillPrint = True Then
                        Dim obj As New clsCashMemoPrint(BillNo, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
                        obj.AllowDecimalQty = clsDefaultConfiguration.AllowDecimalQty
                        obj.WeightScaleEnabled = clsDefaultConfiguration.WeightScaleEnabled

                        Dim ErrorMsg As String = ""
                        obj.HoldCMPrint(clsAdmin.SiteCode, "HoldCM", clsDefaultConfiguration.HoldBillPrintBarcode, ErrorMsg, BarCodeType)
                        If ErrorMsg <> String.Empty Then
                            ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
                        End If
                    End If
                    cmdNew_Click(sender, e)
                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                    If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                        Me.cmdHold.LargeImage = Global.Spectrum.My.Resources._Resume
                        cmdHold.Text = cmdHold.Text.ToUpper
                    End If
                End If

            ElseIf sender.Tag.ToString.ToUpper() = "RESUME" Then
                Dim objResume As New frmNCommonSearch
                objResume.RequestFromPage = enumOperationOnBill.HoldBill
                objResume.SetData = objCM.GetListofHoldData(clsAdmin.SiteCode)
                objResume.ShowDialog()
                If Not objResume.search Is Nothing Then
                    Dim TrnNo As String
                    TrnNo = objResume.search(1).ToString()
                    ClearData()
                    objCM.GetHoldData(dsMain, TrnNo, clsAdmin.SiteCode, clsAdmin.UserCode, dtGV, clsDefaultConfiguration.IsBatchManagementReq)
                    HoldFlag = True
                    'If clsDefaultConfiguration.TaxDetailsRequired = True Then
                    For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Rows
                        Dim TaxableAmt As Double = dr("GrossAmt")
                        If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                            TaxableAmt = dr("GrossAmt") - dr("TotalDiscount")
                        End If
                        CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode").ToString(), TaxableAmt, dr, dr("EAN").ToString())
                        'CreateDataSetForTaxCalculation(dr("BillLineNo").ToString(), dr("ArticleCode").ToString(), TaxableAmt, dr, dr("EAN").ToString())
                    Next
                    'End If

                    If dsMain.Tables("CASHMEMOHDR").Rows.Count > 0 Then

                        Try
                            CLPCustomerId = IIf(Not dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNO") Is DBNull.Value, dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNO"), String.Empty)
                            Dim objCustm As New clsCLPCustomer()
                            Dim _dtCustmInfo As DataTable

                            If (Not String.IsNullOrEmpty(CLPCustomerId)) Then
                                _dtCustmInfo = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CLPCustomerId, CustomerWisePrice:=clsDefaultConfiguration.customerwisepricemanagement)

                                If (_dtCustmInfo IsNot Nothing AndAlso _dtCustmInfo.Rows.Count > 0) Then
                                    CustInfo.CtrlTxtCustomerNo.Text = _dtCustmInfo.Rows(0)("CUSTOMERNO").ToString()
                                    CustInfo.CtrltxtCustomerName.Text = _dtCustmInfo.Rows(0)("CustomerName").ToString()
                                    CustInfo.ctrlTxtPoints.Text = _dtCustmInfo.Rows(0)("BalancePoint").ToString()
                                    AssignCustomerInfoData(_dtCustmInfo, objCustm)
                                    CLPCardType = IIf((_dtCustmInfo.Rows(0)("CARDTYPE") Is DBNull.Value), "", _dtCustmInfo.Rows(0)("CARDTYPE"))
                                    customerType = IIf((_dtCustmInfo.Rows(0)("CustomerType") Is DBNull.Value), "", _dtCustmInfo.Rows(0)("CustomerType"))
                                End If

                            Else
                                CLPCustomerId = IIf(Not dsMain.Tables("CASHMEMOHDR").Rows(0)("CustomerNo") Is DBNull.Value, dsMain.Tables("CASHMEMOHDR").Rows(0)("CustomerNo"), String.Empty)

                                If (Not String.IsNullOrEmpty(CLPCustomerId)) Then
                                    _dtCustmInfo = objCustm.GetCustomerInformation("SO", clsAdmin.SiteCode, clsAdmin.CLPProgram, CLPCustomerId)

                                    If (_dtCustmInfo IsNot Nothing AndAlso _dtCustmInfo.Rows.Count > 0) Then
                                        CustInfo.CtrlTxtCustomerNo.Text = _dtCustmInfo.Rows(0)("CUSTOMERNO").ToString()
                                        CustInfo.CtrltxtCustomerName.Text = _dtCustmInfo.Rows(0)("CustomerName").ToString()
                                        customerType = IIf((_dtCustmInfo.Rows(0)("CustomerType") Is DBNull.Value), "", _dtCustmInfo.Rows(0)("CustomerType"))
                                    End If
                                End If
                            End If

                            Dim salesPersonCode = IIf(Not dsMain.Tables("CASHMEMOHDR").Rows(0)("SalesExecutiveCode") Is DBNull.Value, dsMain.Tables("CASHMEMOHDR").Rows(0)("SalesExecutiveCode"), String.Empty)
                            CtrlSalesPersons.CtrlSalesPersons.SelectedValue = salesPersonCode

                            '--- Added By Mahesh Assign Customer Sales Sales Type for print Customer sales type
                            If Not IsDBNull(dsMain.Tables("CashMemoHdr").Rows(0)("ServiceTaxAmount")) AndAlso Val(dsMain.Tables("CashMemoHdr").Rows(0)("ServiceTaxAmount")) > 0 Then
                                CustomerSaleType = Convert.ToInt16(dsMain.Tables("CashMemoHdr").Rows(0)("ServiceTaxAmount"))
                                Select Case CustomerSaleType
                                    Case enumCustomerSaleType.Dine_In
                                        lblCustSaleType.Text = "Dine In"
                                    Case enumCustomerSaleType.Home_Delivery
                                        lblCustSaleType.Text = "Home Delivery"
                                    Case enumCustomerSaleType.Take_Away
                                        lblCustSaleType.Text = "Take Away"
                                    Case Else
                                End Select
                                lblCustSaleType.Visible = True
                                CustSaleTypeTimer.Start()
                            End If
                            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                                If HoldFlag = True Then
                                    Me.cmdHold.LargeImage = Global.Spectrum.My.Resources.HoldNew
                                    cmdHold.Text = cmdHold.Text.ToUpper
                                End If
                            End If

                        Catch ex As Exception
                            LogException(ex)
                        End Try
                    End If
                    'Else
                    '    ShowMessage(getValueByKey("CM061"), getValueByKey("CLAE05"))
                    ReCalculateCM(String.Empty)
                    calculateTotalbill()

                    If (dgMainGrid.Rows.Count > 1) Then
                        dgMainGrid.Select(dgMainGrid.Rows.Count - 1, 2)
                    End If
                End If
            End If
            GridSettings(UpdateFlag)
        Catch ex As Exception
            ShowMessage(getValueByKey("CM041"), getValueByKey("CLAE05"))
            HoldFlag = False
            LogException(ex)
            'ShowMessage("Error in Holding or Resuming the data", "Error")
        End Try
    End Sub
    Private Sub cmdSalesOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim frmSo As New frmNSalesOrderCreation()
            frmSo.Show()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdBirthList_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim frmBL As New frmNBirthListCreate()
            frmBL.Show()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Dim lastKeystroke As New DateTime(0)
    Dim barcode As New List(Of Char)(30)

    Private Sub dgMainGrid_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles dgMainGrid.KeyPress

        If (Not UpdateFlag) Then
            'check timing (keystrokes within 100 ms)
            Dim elapsed As TimeSpan = (DateTime.Now - lastKeystroke)
            If (elapsed.TotalMilliseconds > 100) Then
                CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                barcode.Clear()
            End If

            'record keystroke & timestamp
            lastKeystroke = DateTime.Now
            barcode.Add(e.KeyChar)

            If (barcode.Count <= 0) Then
                ShowMessage(getValueByKey("CM066"), "CM066-" & getValueByKey("CLAE04"))
                'Invalid article scan. Please scan again!
            End If

            'process barcode
            If (Convert.ToInt32(e.KeyChar) = 13 AndAlso barcode.Count > 0) Then
                Dim barcodeValue As New String(barcode.ToArray())
                Dim articleCode = barcodeValue.Trim()

                CtrlSalesPersons.CtrlTxtBox.Value = barcodeValue.Trim()
                CtrlSalesPersons.CtrlTxtBox.Text = barcodeValue.Trim()

                lastKeystroke = New DateTime(0)
                barcode = New List(Of Char)(30)

                If (Not String.IsNullOrEmpty(CtrlSalesPersons.CtrlTxtBox.Text)) Then
                    txtSearch_KeyDown(sender, New System.Windows.Forms.KeyEventArgs(Keys.Enter))
                    e.Handled = True
                End If
            End If
        End If

    End Sub
    Private Sub dgMainGrid_RowColChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgMainGrid.RowColChange
        Try
            If dgMainGrid.Rows.Count > 1 Then
                Dim strArticle As String = dgMainGrid.Rows(dgMainGrid.Row)("ArticleCode").ToString()
                ProductImage.ShowArticleImage(strArticle)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdAdjust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim objAdj As New frmNAdjustment
            objAdj.ShowDialog()
            Dim dtOthercharge As DataTable = objAdj.GetCharges
            If Not dtOthercharge Is Nothing AndAlso dtOthercharge.Rows.Count > 0 Then
                For Each dr As DataRow In dtOthercharge.Rows
                    Dim dritem As DataRow = dsMain.Tables("CashMemodtl").NewRow()
                    dritem("Btype") = "S"
                    dritem("Articlecode") = dr("article").ToString()
                    dritem("EAN") = dr("EAN").ToString()
                    dritem("Quantity") = 1
                    dritem("Discription") = dr("AdjType").ToString()
                    dritem("SellingPrice") = dr("AdjAmount").ToString()
                    dritem("GrossAmt") = dritem("SellingPrice")
                    dritem("NetAmount") = dritem("SellingPrice")
                    dsMain.Tables("CashMemodtl").Rows.Add(dritem)
                Next
            End If
            CreatingLineNO(dsMain, "CashMemoDtl")
            ReCalculateCM("")
            calculateTotalbill()

            'CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Select()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdReprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReprint.Click
        Try
            Dim authUserID = "", authRemarks As String = ""
            If Not CheckInterTransactionAuth("ReprintCM", dsMain.Tables("CASHMEMOHDR"), authUserID:=authUserID, authRemarks:=authRemarks) = True Then
                Exit Sub
            End If

            If OnlineConnect = False Then
                ShowMessage(getValueByKey("CMO50"), "CMO50 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            If UpdateFlag = False AndAlso dsMain.Tables("cashMemodtl").Rows.Count > 0 Then
                'ShowMessage(getValueByKey  ("CM041"), "Error:CM041")
                If MsgBox(getValueByKey("CM029"), MsgBoxStyle.YesNo, "CM029 - " & getValueByKey("CLAE04")) = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If
            objCM.ShiftManagementForCM = False
            Dim dt As DataTable = objCM.GetOldCashMemo(clsAdmin.SiteCode, NoOfDaysForReprint:=clsDefaultConfiguration.NumberOfDaysApplicableForReprint, NewCustmor:=clsDefaultConfiguration.DetailedCustomerCreationformat)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Dim objSearch As New frmNCommonSearch
                objSearch.RequestFromPage = enumOperationOnBill.Reprint
                objSearch.SetData = dt
                objSearch._IsRemarkVisible = True
                objSearch.ShowDialog()
                If Not objSearch.search Is Nothing AndAlso objSearch.search.Length > 0 Then
                    objCM.UpdateReprintStatus(clsAdmin.UserCode, objSearch.txtReason.Text.Trim(), clsAdmin.SiteCode, clsAdmin.TerminalID, objSearch.search(0).Trim, AuthUserId:=authUserID, AuthUserRemarks:=authRemarks)
                    Dim obj As New clsCashMemoPrint(objSearch.search(0).Trim, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
                    obj.DisplayArticleFullName = clsDefaultConfiguration.PrintItemFullName
                    obj.AllowDecimalQty = clsDefaultConfiguration.AllowDecimalQty
                    obj.WeightScaleEnabled = clsDefaultConfiguration.WeightScaleEnabled

                    obj.TokenNoRequiredInKOT = clsDefaultConfiguration.TokenNoRequiredInKOT
                    obj.CashMemoResetonDayClose = clsDefaultConfiguration.CashMemoResetonDayClose
                    obj.AllowBillingOnlyAfterSelectionOfSalesType = clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType
                    obj.PrintFormatNo = clsDefaultConfiguration.PrintFormatNo
                    obj.DisplayBrandWiseSale = clsDefaultConfiguration.DisplayBrandWiseSale 'vipin 10.09.2018 

                    Dim ErrorMsg As String = ""
                    If (clsDefaultConfiguration.TemplatePrintingAllowed) Then
                        clsCashMemo.dsCashMemoPrinting = Nothing
                        obj.PrintTemplateCashMemoBillDetails(objSearch.search(0).Trim, clsAdmin.SiteCode, clsAdmin.CurrencyCode, String.Empty, Nothing, IsBillReprint:=True, ReprintReason:=objSearch.txtReason.Text.Trim())
                    Else
                        'obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Duplicate", "", "", ErrorMsg)
                        ' obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Duplicate", "", "", ErrorMsg, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=True, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsCounterCopyKot:=True, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6) '0000413   code added by irfan on 18/9/2017 visiblity of hsn and tax
                        'modified by khusrao adil on 8-12-2017 for jk sprint 32
                        ',JKPrintFormatEnable falg added
                        obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Duplicate", "", "", ErrorMsg, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=True, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsCounterCopyKot:=True, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6, JKPrintFormatEnable:=clsDefaultConfiguration.JKPrintFormatEnable) '0000413  code added by irfan against tender visblity    code added by irfan on 11/09/2017 visiblity of hsn and tax) '0000413
                    End If



                    If ErrorMsg <> String.Empty Then
                        ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
                    End If
                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                End If
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM030"), "CM030 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in retriving the cash memo data", "Error")
        End Try
    End Sub
    Private Sub cmdLoyalty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal dtpayment As DataTable = Nothing)
        Try
            If CustInfo.CtrlTxtCustomerNo.Text <> String.Empty AndAlso customerType.Equals("CLP") Then
                ClearCLP()
                CalCulateCLPSlabwise(CLPCardType, dsMain.Tables("CashMemoDtl"), "CLPRequire=TRUE AND BTYPE='S' And ArticleCode <>'GVBaseArticle' AND ArticleCode <>'CLPBaseArticle' ", CLPCustomerId, dtpayment)
                If UpdateFlag = False Then
                    ReCalculateCM("")
                End If
                calculateTotalbill()
            End If

            ' CtrlSalesPersons.CtrlTxtBox.Select()
            ' CtrlSalesPersons.AndroidSearchTextBox.Select()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdShowMoreInfo() Handles CustInfo.Click
        Try
            If Not CustInfo.CtrlTxtSwape.Text = String.Empty Then
                Dim objClpCust As New clsCLPCustomer
                Dim dtCust As New DataTable
                Dim strSql As String = " "

                If customerType.Equals("SO") Then
                    dtCust = objClpCust.GetCustomerInformation("SO", clsAdmin.SiteCode, String.Empty, CustInfo.CtrlTxtSwape.Text)
                Else
                    dtCust = objClpCust.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CustInfo.CtrlTxtSwape.Text)
                End If

                If Not dtCust Is Nothing AndAlso dtCust.Rows.Count > 0 Then
                    Dim objfrmCustomerDeatils As New frmCustomerDetails(dtCust)
                    objfrmCustomerDeatils.ShowDialog()
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
#End Region
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Private Sub dgMainGrid_StartEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgMainGrid.StartEdit
        Try
            If Not dgMainGrid.Rows(e.Row) Is Nothing AndAlso dgMainGrid.Rows(e.Row)("Quantity") > 0 Then
                _iArticleQtyBeforeChange = dgMainGrid.Rows(e.Row)("Quantity")
            End If


            If dgMainGrid.Cols(e.Col).Name = "QUANTITY" AndAlso dgMainGrid.Rows(e.Row)("BTYPE") <> "R" Then
                _iArticleQtyBeforeChange = dgMainGrid.Rows(e.Row)("Quantity")
                'dgMainGrid.Rows(e.Row)(dgMainGrid.Col) = 0
                ReCalculateCM("")
                calculateTotalbill()
            End If
        Catch ex As Exception

        End Try

    End Sub
    'Protected Overrides Function ProcessKeyPreview(ByRef m As System.Windows.Forms.Message) As Boolean
    '    Const WM_KEYDOWN As Integer = &H100
    '    If m.Msg = WM_KEYDOWN Then
    '        Select Case m.WParam.ToInt32
    '            Case Keys.F
    '                If My.Computer.Keyboard.CtrlKeyDown Then
    '                    'Debug.Print("Ctrl + F")
    '                    If (UpdateFlag = False) Then
    '                        cmdSearch_Click(CtrlSalesPersons.CtrlTxtBox, New KeyEventArgs(Keys.Enter))
    '                    End If
    '                End If
    '            Case Keys.M
    '                If My.Computer.Keyboard.CtrlKeyDown Then
    '                    'Debug.Print("Ctrl + M")
    '                    cmdShowMoreInfo()
    '                End If
    '            Case Keys.B
    '                If My.Computer.Keyboard.CtrlKeyDown AndAlso clsDefaultConfiguration.ManualPromotionAllowed Then
    '                    'Debug.Print("Ctrl + B") change enable disable manual promotion
    '                    Dim e1 As New System.EventArgs
    '                    cmdEnable_Click(cmdEnable, e1)
    '                End If
    '        End Select
    '    End If
    '    Return MyBase.ProcessKeyPreview(m)
    'End Function
    Private Sub PriceChange()
        Try
            If UpdateFlag = False Then
                dgMainGrid.FinishEditing()
                Dim lastRowIndex As Integer = dgMainGrid.Row 'dgMainGrid.Rows.Count - 1
                Dim EventType As Int32 = 0

                Dim discountAmount As Decimal = Decimal.Zero
                discountAmount = dsMain.Tables("CASHMEMODTL").Compute("Sum(TotalDiscount)", String.Empty)
                If clsDefaultConfiguration.IsMembership Then discountAmount = Decimal.Zero
                If (discountAmount > 0) Then
                    ' ShowMessage(getValueByKey("CM065"), getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
                    'code added  by vipul for Customer wise price
                    If clsDefaultConfiguration.customerwisepricemanagement = True Then
                        EventType = 1
                    Else
                        ShowMessage(getValueByKey("CM064"), getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
                    End If
                Else
                    EventType = 1
                End If
                IsChangeQuantityOrPrice = False

                If EventType = 1 Then
                    If dgMainGrid.Rows(lastRowIndex)("Btype") = "S" Then

                        If clsDefaultConfiguration.PriceChageAllowed = True Then
                            If CheckInterTransactionAuth("PriceChange", dsMain.Tables("CashMemoDtl")) = True Then

                                If (dgMainGrid.Rows(lastRowIndex)("ArticleCode").Equals("GVBaseArticle")) Then
                                    ShowMessage(getValueByKey("GVS06"), "GVS06 - " & getValueByKey("CLAE04"))
                                    Exit Sub
                                End If

                                Dim frm As New frmSpecialPrompt(getValueByKey("SP002"))
                                frm.ShowTextBox = True
                                frm.txtValue.MaxLength = 14
                                frm.AcceptButton = frm.cmdOk
                                frm.AllowDecimal = True
                                frm.IsNumeric = True
                                Dim i As Decimal
                                frm.ShowDialog()

                                If frm.GetResult IsNot Nothing Then

                                    If (discountAmount > 0) Then
                                        If clsDefaultConfiguration.customerwisepricemanagement = False Then
                                            cmdClrAllPromo_Click("ClearPromWithoutMessage", EventArgs.Empty)
                                        End If

                                    End If
                                    IsChangeQuantityOrPrice = True

                                    _PriceBeforeChange = dgMainGrid.Rows(lastRowIndex)("SellingPrice")
                                    _iArticleQtyBeforeChange = dgMainGrid.Rows(lastRowIndex)("Quantity")
                                    If dgMainGrid.Rows(lastRowIndex)("SellingPrice") <> IIf(Decimal.TryParse(frm.GetResult, i), frm.GetResult, dgMainGrid.Rows(lastRowIndex)("SellingPrice")) Then
                                        dgMainGrid.Rows(lastRowIndex)("IsPricechanged") = True
                                    End If

                                    dgMainGrid.Rows(lastRowIndex)("SellingPrice") = IIf(Decimal.TryParse(frm.GetResult, i), frm.GetResult, dgMainGrid.Rows(lastRowIndex)("SellingPrice"))
                                    Dim index As Int32 = dgMainGrid.Cols("SellingPrice").Index
                                    dgMainGrid_AfterEdit(dgMainGrid, New C1.Win.C1FlexGrid.RowColEventArgs(lastRowIndex, index))
                                    _PriceBeforeChange = Nothing
                                End If

                            End If
                        End If

                        IsChangeQuantityOrPrice = False
                        dgMainGrid.FinishEditing()
                    End If

                    dgMainGrid.Select(lastRowIndex, 2)
                    'CtrlSalesPersons.CtrlTxtBox.Select()
                    'CtrlSalesPersons.CtrlTxtBox.Focus()
                    CtrlSalesPersons.AndroidSearchTextBox.Select()
                    CtrlSalesPersons.AndroidSearchTextBox.Focus()

                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub BillRecalculateTax()
        Try
            If UpdateFlag = False Then
                dgMainGrid.FinishEditing()
                '---- Clear existing tax details 
                dtMainTax.Rows.Clear()
                For Each dr As DataRow In dsMain.Tables("Cashmemodtl").Rows
                    Dim taxAmt = 0 'CalculateTotalInclusiveTax(0, dr("ARTICLECODE"), dr("GrossAmt"), 1, dr("EAN"), IsInclusiveTax)
                    dr("TotalTaxAmount") = taxAmt
                    ' If (dr("EXCLUSIVETAX") Is DBNull.Value OrElse dr("EXCLUSIVETAX") = 0) Then
                    dr("EXCLUSIVETAX") = taxAmt
                    '   End If
                Next
                isCashierPromoSelected = False
                '------ Clear existing tax details 
                Dim lastRowIndex As Integer
                For RowIndex As Integer = 1 To dgMainGrid.Rows.Count - 1
                    lastRowIndex = RowIndex
                    Dim EventType As Int32 = 0
                    '_PriceBeforeChange = 0
                    _iArticleQtyBeforeChange = dgMainGrid.Rows(lastRowIndex)("Quantity")
                    Dim ColIndex As Int32 = dgMainGrid.Cols("SellingPrice").Index
                    'Dim discountAmount As Decimal = Decimal.Zero
                    'discountAmount = dsMain.Tables("CASHMEMODTL").Compute("Sum(TotalDiscount)", String.Empty)

                    'If (discountAmount > 0) Then
                    '    ShowMessage(getValueByKey("CM065"), getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
                    'Else
                    '    EventType = 1
                    'End If
                    IsChangeQuantityOrPrice = True
                    dgMainGrid.Rows(lastRowIndex)("IsPricechanged") = True
                    If RecalculateTax = True Then
                        CreateDataSetForTaxCalculation(0, dgMainGrid.Rows(lastRowIndex)("BillLineNo").ToString(), dgMainGrid.Rows(lastRowIndex)("ArticleCode"), dgMainGrid.Rows(lastRowIndex)("GrossAmt") - IIf(dgMainGrid.Rows(lastRowIndex)("TOTALDISCOUNT") Is DBNull.Value, 0, dgMainGrid.Rows(lastRowIndex)("TOTALDISCOUNT")), lastRowIndex, dgMainGrid.Rows(lastRowIndex)("Quantity"), dgMainGrid.Rows(lastRowIndex)("EAN"))
                    End If

                    'dgMainGrid.Rows(lastRowIndex)("SellingPrice") = IIf(Decimal.TryParse(frm.GetResult, i), frm.GetResult, dgMainGrid.Rows(lastRowIndex)("SellingPrice"))
                    ' dgMainGrid_AfterEdit(dgMainGrid, New C1.Win.C1FlexGrid.RowColEventArgs(lastRowIndex, ColIndex)) 'comment by sagar for sabbaro issue after changing salestype tax amount is wrongly calculated.
                    _PriceBeforeChange = Nothing

                Next RowIndex
                IsChangeQuantityOrPrice = False
                dgMainGrid.FinishEditing()
                ReCalculateCM("")
                calculateTotalbill()
                dgMainGrid.Select(lastRowIndex, 2)
                'CtrlSalesPersons.CtrlTxtBox.Select()
                'CtrlSalesPersons.CtrlTxtBox.Focus()
                CtrlSalesPersons.AndroidSearchTextBox.Select()
                CtrlSalesPersons.AndroidSearchTextBox.Focus()

            End If
            Exit Sub
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub ChangeQty()
        Try
            If clsDefaultConfiguration.IsBillScanApplicable Then
                If (dsMain.Tables("CashMemoMettler") IsNot Nothing AndAlso dsMain.Tables("CashMemoMettler").Rows.Count > 0) Then
                    Exit Sub
                End If
            End If
            If UpdateFlag = False Then
                dgMainGrid.FinishEditing()
                Dim lastRowIndex As Integer = dgMainGrid.Row 'dgMainGrid.Rows.Count - 1
                Dim EventType As Int32 = 0

                Dim discountAmount As Decimal = Decimal.Zero
                discountAmount = dsMain.Tables("CASHMEMODTL").Compute("Sum(TotalDiscount)", String.Empty)
                If clsDefaultConfiguration.IsMembership Then discountAmount = Decimal.Zero
                If (discountAmount > 0) Then
                    'ShowMessage(getValueByKey("CM064"), getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
                    If clsDefaultConfiguration.customerwisepricemanagement = True Then
                        EventType = 1
                    Else
                        ShowMessage(getValueByKey("CM064"), getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
                    End If
                Else
                    EventType = 1
                End If
                IsChangeQuantityOrPrice = False

                If EventType = 1 Then
                    If dgMainGrid.Rows(lastRowIndex)("Btype") = "S" Then

                        'Rakesh:01.11.2013:8265-> Avoid quantity change of voucher
                        If (dgMainGrid.Rows(lastRowIndex)("ArticleCode").ToString.ToUpper.Equals("GVBASEARTICLE")) Then
                            ShowMessage(getValueByKey("GVS05"), "GVS05 - " & getValueByKey("CLAE04"))
                            Exit Sub
                        End If

                        Dim frm As New frmSpecialPrompt(getValueByKey("SP003"))
                        frm.ShowTextBox = True
                        frm.AcceptButton = frm.cmdOk
                        frm.txtValue.MaxLength = 9
                        frm.IsNumeric = True
                        If clsDefaultConfiguration.AllowDecimalQty Then
                            frm.AllowDecimal = True
                        End If
                        frm.ShowDialog()
                        Dim i As Integer
                        If frm.GetResult IsNot Nothing Then

                            If (discountAmount > 0) Then
                                If clsDefaultConfiguration.customerwisepricemanagement = False Then
                                    cmdClrAllPromo_Click("ClearPromWithoutMessage", EventArgs.Empty)
                                End If

                            End If
                            IsChangeQuantityOrPrice = True

                            _iArticleQtyBeforeChange = dgMainGrid.Rows(dgMainGrid.Row)("Quantity")
                            dgMainGrid.Rows(lastRowIndex)("Quantity") = IIf(frm.GetResult Is Nothing, 1, frm.GetResult)
                            dgMainGrid.Rows(lastRowIndex)("Quantity") = IIf(Int32.TryParse(frm.GetResult, i), frm.GetResult, dgMainGrid.Rows(lastRowIndex)("Quantity"))

                            Dim index As Int32 = dgMainGrid.Cols("SellingPrice").Index
                            dgMainGrid_AfterEdit(dgMainGrid, New C1.Win.C1FlexGrid.RowColEventArgs(lastRowIndex, index))
                        End If

                    End If
                    IsChangeQuantityOrPrice = False
                    dgMainGrid.FinishEditing()
                End If

                dgMainGrid.Select(lastRowIndex, 2)
                'CtrlSalesPersons.CtrlTxtBox.Select()
                'CtrlSalesPersons.CtrlTxtBox.Focus()
                CtrlSalesPersons.AndroidSearchTextBox.Select()
                CtrlSalesPersons.AndroidSearchTextBox.Focus()

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cbManualDisc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cbManualDisc.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Select()
        End If
    End Sub
    Private Sub dgMainGrid_ValidateEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.ValidateEditEventArgs) Handles dgMainGrid.ValidateEdit

        If dgMainGrid.Cols(e.Col).Name = "QUANTITY" Then
            If dgMainGrid.Editor.Text.Length > 9 Then
                'CM059() " Qty cannot be greater then 999999999
                If Val(dgMainGrid.Editor.Text) > 999999999 Then
                    ShowMessage(getValueByKey("CM059"), "CM059 - " & getValueByKey("CLAE04"))
                    e.Cancel = True
                End If
            End If
        End If

    End Sub

    Private Sub rbnbtnApplyCST_Click(sender As System.Object, e As System.EventArgs) Handles rbnbtnApplyCST.Click
        If clsDefaultConfiguration.IsCstTaxRequired Then
            DisplayCSTMessage()
        End If
    End Sub

    Private Sub RemoveSelectedArticlePromotion()
        Try
            If (dgMainGrid.Rows.Count > 1) Then
                If (dgMainGrid.Rows.Count > 1) Then
                    dgMainGrid.Select(dgMainGrid.Rows.Count - 1, 2)
                End If

                If MsgBox(getValueByKey("CM064"), MsgBoxStyle.Information, "CM064-" & getValueByKey("CLAE04")) = MsgBoxResult.Ok Then
                    cmdClrAllPromo_Click("ClearPromWithoutMessage", EventArgs.Empty)
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub


    Private Sub CustInfo_Load(sender As Object, e As EventArgs) Handles CustInfo.Load

    End Sub

    Private Sub cmdCreditSale_Click(sender As Object, e As EventArgs) Handles cmdCreditSale.Click
        'Private Sub cmdCreditSale_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCreditSale.Click
        Try
            '0,000 should remain, then after selection of items, on payment, Customer selection to be mandatory as it was earlier.
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType AndAlso clsDefaultConfiguration.IsAfterSelectSaleTypeCmSelectionRequired Then 'ketan
                If CustomerSaleType = 0 Then Exit Sub
                Dim CustSaleType As String = IIf(CustomerSaleType = enumCustomerSaleType.Dine_In, "DINEIN", "TAKEAWAY")
                If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then
                    cmdCustomerSearchAndLoad(CustSaleType)
                End If
                If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then Exit Sub
            End If
            If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then
                ' added by khusrao adil for niq-naq requirment for waiter take order without customer selection
                'If clsDefaultConfiguration.IsTablet = True Then
                If clsDefaultConfiguration.IsCustomerMandatoryForCreditSale = True Then
                    ShowMessage(getValueByKey("CHKP11"), getValueByKey("CLAE04"))
                    'CtrlSalesPersons.CtrlTxtBox.Select()
                    CtrlSalesPersons.AndroidSearchTextBox.Select()
                    Exit Sub
                End If
            End If

            'ShowMessage(getValueByKey("CM067"), "CM067 - " & getValueByKey("CLAE04"))

            'If dsMain.Tables("CASHMEMODTL").Select("BTYPE='S'  AND isnull(TotalDiscount,0)='0'", "", DataViewRowState.CurrentRows).Length = dsMain.Tables("CASHMEMODTL").Rows.Count Then
            '    'isCashierPromoSelected = False
            'End If

            If UpdateFlag = False Then
                cmdDefaultPromo_Click(sender, e)
                'cmdLoyalty_Click(sender, e)
            End If
            If clsDefaultConfiguration.IsPoleDisply = True Then
                TwoLineExtendScreen(Ispayment:=True)
            End If
            Dim totalAmount As Decimal = Decimal.Zero
            totalAmount = Val(dsMain.Tables("CASHMEMODtl").Compute("SUM(NETAMOUNT)", "NETAMOUNT > 0").ToString())
            totalAmount = MyRound(totalAmount, clsDefaultConfiguration.BillRoundOffAt)

            If (totalAmount > 0) Then
                Dim dtCurrency = New clsAcceptPayment().LoadCurrency(clsAdmin.SiteCode)
                Dim currencySymbol As String = IIf((dtCurrency IsNot Nothing AndAlso dtCurrency.Rows.Count > 0), dtCurrency.Rows(0)("CurrencySymbol").ToString(), "")

                dsMain.Tables("CASHMEMORECEIPT").Rows.Clear()

                If Not dsMain.Tables("CASHMEMORECEIPT").Columns.Contains("NOCLP") Then
                    dsMain.Tables("CASHMEMORECEIPT").Columns.Add("NOCLP", System.Type.GetType("System.Boolean"))
                End If

                Dim drReceipt As DataRow = dsMain.Tables("CASHMEMORECEIPT").NewRow
                drReceipt("SiteCode") = clsAdmin.SiteCode
                drReceipt("FinYear") = clsAdmin.Financialyear
                drReceipt("CardNo") = String.Format("{0} {1}", currencySymbol, FormatNumber(totalAmount, 2))
                drReceipt("CMRecptLineno") = 1
                drReceipt("TerminalID") = clsAdmin.TerminalID
                drReceipt("ExchangeRate") = 1
                drReceipt("TenderTypeCode") = "Credit"
                drReceipt("AmountTendered") = totalAmount
                drReceipt("CurrencyCode") = clsAdmin.CurrencyCode
                drReceipt("AmountinCurrency") = totalAmount
                drReceipt("CmRcptDate") = clsAdmin.CurrentDate.Date
                drReceipt("CmRcptTime") = clsAdmin.CurrentDate
                drReceipt("RefDate") = clsAdmin.CurrentDate.Date
                drReceipt("STATUS") = True
                '--- Changed By Mahesh
                'drReceipt("TenderHeadCode") = "CreditSale"
                drReceipt("TenderHeadCode") = "Credit Sales"
                dsMain.Tables("CASHMEMORECEIPT").Rows.Add(drReceipt)

                If UpdateFlag = False Then
                    Dim dt As New DataTable
                    dt = dsMain.Tables("CashMemoReceipt").Copy()
                    dt.Columns("TenderHeadCode").ColumnName = "Reciepttypecode"
                    cmdLoyalty_Click(sender, e, dt)
                    dt.Dispose()

                    If dsMain.Tables("CASHMEMORECEIPT").Columns.Contains("NOCLP") Then
                        dsMain.Tables("CASHMEMORECEIPT").Columns.Remove("NOCLP")
                    End If
                End If

                If cmdSavePrint_Click(sender, e) Then
                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                End If
            End If

            'CtrlSalesPersons.CtrlTxtBox.Select()
            'CtrlSalesPersons.CtrlTxtBox.Focus()
            CtrlSalesPersons.AndroidSearchTextBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Focus()

        Catch ex As Exception
            ShowMessage(getValueByKey("CM033"), "CM033 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub EnableDiableTenderIcons()
        '--- Added by Mahesh for disable credit sale if tender not assign
        Dim DtTender As DataTable = GetTenderInfo(clsAdmin.SiteCode)
        '--- Credit sale 
        Dim dr() = DtTender.Select("TenderType='" & "Credit" & "'")
        If dr IsNot Nothing AndAlso dr.Count > 0 Then
            IsTenderCredit = True
        Else
            cmdCreditSale.Enabled = False
        End If
        '----Cash
        Dim dt() = DtTender.Select("TenderType='" & "Cash" & "'")
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            IsTenderCash = True
        Else
            cmdCash.Enabled = False
        End If
        '----Cheque
        Dim dq() = DtTender.Select("TenderType='" & "Cheque" & "'")
        If dq IsNot Nothing AndAlso dq.Count > 0 Then
            IsTenderCheque = True
        Else
            cmdCheque.Enabled = False
        End If
        '----CreditCard
        Dim dw() = DtTender.Select("TenderType='" & "CreditCard" & "'")
        If dw IsNot Nothing AndAlso dw.Count > 0 Then
            IsTenderCreditCard = True
        Else
            cmdCard.Enabled = False
        End If
    End Sub

    Private Sub rbnbtnRoundOff_Click(sender As Object, e As EventArgs) Handles rbnbtnRoundOff.Click
        Try
            Dim p As Object = "ClearPromWithoutMessage"
            cmdClrAllPromo_Click(p, Nothing)
            If CashSummary.CtrllblNetAmt.Text Mod 5 = 0 Then
                Exit Sub
            End If
            Dim FilterCondition As String = "BTYPE='S' AND (isnull(MANUALPROMO,'')='' OR isnull(MANUALPROMO,0)=0)  "
            Dim totalGAmount As Double
            Dim percentage, totalDiscValue As Double
            totalDiscValue = CashSummary.CtrllblNetAmt.Text Mod 5
            Dim dtUserAuth As DataTable = dsMain.Tables("CashMemodtl").Copy
            Dim dtCashMemoDtls As DataTable = dsMain.Tables("CashMemodtl")
            totalGAmount = dtUserAuth.Compute("Sum(GROSSAMT)", FilterCondition)
            Dim ObjclsCommon As New clsCommon
            Dim offerno As String = ObjclsCommon.GetOffernoForRoundOff()
            For Each dr As DataRow In dtUserAuth.Select(FilterCondition, "Ean", DataViewRowState.CurrentRows)
                percentage = (dr("GROSSAMT") / totalGAmount) * 100
                dr("LINEDISCOUNT") = (percentage * totalDiscValue) / 100
                dr("TOTALDISCPERCENTAGE") = (dr("LINEDISCOUNT") / dr("GrossAmt")) * 100
                dr("TOTALDISCOUNT") = dr("LINEDISCOUNT")
                dr("NETAMOUNT") = (dr("GROSSAMT") + IIf(dr("EXCLUSIVETAX") Is DBNull.Value, 0, dr("EXCLUSIVETAX"))) - dr("TOTALDISCOUNT")
                dr("AuthUserId") = clsAdmin.UserCode
                dr("AuthUserRemarks") = clsAdmin.UserCode
                dr("PROMOTIONID") = offerno
                dr("TOPLEVEL") = offerno
                dr("FirstLEVEL") = offerno

            Next


            Dim dvDtls = dtUserAuth.Select(String.Empty, String.Empty, DataViewRowState.ModifiedCurrent)
            If (dvDtls.Count > 0) Then
                For rowIndex = 0 To dtUserAuth.Rows.Count - 1
                    For colIndex = 0 To dtUserAuth.Columns.Count - 1
                        dtCashMemoDtls(rowIndex)(colIndex) = dtUserAuth(rowIndex)(colIndex)
                    Next
                Next
            End If
            Dim orgTax As Double = 0
            For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Rows
                Dim TaxableAmt = dr("GrossAmt") - dr("TotalDiscount")
                CreateDataSetForTaxCalculation(orgTax, dr("BillLineNo").ToString(), dr("ArticleCode").ToString(), TaxableAmt, dr, dr("EAN").ToString())
            Next
            IsRoundOffMsg = True
            ReCalculateCM("")
            calculateTotalbill()
            ' CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Select()

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
#Region "Credit Sales Return cases"

    Public Function SetBillWiseCreditSaleAmt() As Double
        Try
            Dim dataview As New DataView(dsMain.Tables("CashMemoDtl"), "RETURNCMNO<>''", "", DataViewRowState.CurrentRows)
            Dim dtUnique As DataTable = dataview.ToTable(True, "RETURNCMNO")
            Dim ReturnBills As New StringBuilder
            Dim ItemReturnAmount As Double
            Dim BillNetCreditSale As Double
            For Each row As DataRow In dtUnique.Rows
                Dim dr() = dtCreditSaleData.Select("Billno='" & row("RETURNCMNO") & "'")
                If dr.Length > 0 Then
                    BillNetCreditSale = dtCreditSaleData.Compute("sum(creditsale)", "Billno='" & row("RETURNCMNO") & "'") - dtCreditSaleData.Compute("sum(CreditSaleAdjustment)", "Billno='" & row("RETURNCMNO") & "'")
                    ItemReturnAmount = Math.Abs(dsMain.Tables("CashMemoDtl").Compute("sum(NetAmount)", "RETURNCMNO='" & row("RETURNCMNO") & "'"))
                    If ItemReturnAmount > BillNetCreditSale Then
                        SetBillWiseCreditSaleAmt += BillNetCreditSale
                    Else
                        SetBillWiseCreditSaleAmt += ItemReturnAmount
                    End If
                End If
            Next
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

#End Region
    Dim dtMembDatapromo As DataTable
    Private Sub CtrlTxtSwape_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'Created By Vaibhav
        Try
            If (e.KeyCode = Keys.Enter) Then
                'code added for issue id 1674  by vipul
                CustInfo.CtrlTxtSwape.Text = CustInfo.CtrlTxtSwape.Text.Trim
                If Not CustInfo.CtrlTxtSwape.Text = String.Empty Then
                    ' Dim CardNo As String = ReturnOnlyNumbersWhenCardSwipe(CustInfo.CtrlTxtSwape.Text)
                    Dim objCustm As New clsCLPCustomer
                    Dim dt As DataTable
                    Dim eventType As Int32
                    'CustInfo.CtrlTxtSwape.Text = String.Empty
                    '------Lee spa membership scan start
                    If clsDefaultConfiguration.IsMembership Then
                        Membershipid = CustInfo.CtrlTxtSwape.Text
                        cmdNew_Click(sender, e)
                        If IsMembership Then Exit Sub
                        Dim objmemb As New clsMembership
                        dtMembData = objmemb.GetMemberDetails(Membershipid, clsAdmin.SiteCode)
                        dtMembDatapromo = objmemb.GetMemberDiscount(Membershipid, clsAdmin.SiteCode)
                        If Not dtMembData Is Nothing AndAlso dtMembData.Rows.Count > 0 Then
                            If dtMembData.Rows(0)("EndDate").Date <= DateTime.Now.Date Then
                                ShowMessage("The membership has been expired. Kindly renew. ", "Information")
                                CustInfo.CtrlTxtSwape.Text = String.Empty
                                Exit Sub
                            End If
                            CardNo = dtMembData.Rows(0)("CustomerNo").ToString()
                            CtrlSalesPersons.CtrlTxtBox.Text = dtMembData.Rows(0)("ServiceCode").ToString()
                            txtSearch_KeyDown(CtrlSalesPersons.CtrlTxtBox, e)
                        End If
                        CustInfo.CtrlTxtCustomerNo.ReadOnly = True
                        CustInfo.CtrlTxtSwape.ReadOnly = True
                    Else
                        CardNo = CustInfo.CtrlTxtSwape.Text.Trim()
                    End If
                    '------Lee spa membership scan end

                    dt = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CardNo, CustomerWisePrice:=clsDefaultConfiguration.customerwisepricemanagement)
                    dtHD = dt.Copy()
                    If Not dt Is Nothing And dt.Rows.Count > 0 Then
                        AssignCustomerInfoData(dt, objCustm)
                    Else
                        CustInfo.CtrlTxtCustomerNo.Clear()
                        CustInfo.CtrltxtCustomerName.Clear()
                        CustInfo.ctrlTxtPoints.Clear()
                        CustInfo.CtrlTxtSwape.Clear()
                        CustInfo.CtrlTxtSwape.Focus()
                        ShowMessage("This customer is not exist. Do you want to create new customer?", "CM014 - " & getValueByKey("CLAE04"), eventType, "No", "Yes")
                        If eventType = 1 Then
                            If clsDefaultConfiguration.DetailedCustomerCreationformat = "0" Then
                                Dim objClpCustomer As New frmNSearchCustomer
                                objClpCustomer.CustomerNo = String.Empty
                                objClpCustomer.AccessCustomerOutside = True
                                'objClpCustomer.ShowSO = False
                                'objClpCustomer.ShowCLP = True
                                If IsNumeric(CardNo) Then
                                    objClpCustomer.SearchedValue = CardNo
                                End If

                                If (objClpCustomer.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                                    If (objClpCustomer.dtCustmInfo IsNot Nothing AndAlso objClpCustomer.dtCustmInfo.Rows.Count > 0) Then
                                        dt = objClpCustomer.dtCustmInfo()
                                        dtHD = objClpCustomer.dtCustmInfo()
                                        Me.DialogResult = Windows.Forms.DialogResult.OK
                                    End If
                                End If
                                objClpCustomer.Dispose()
                            Else
                                Dim objClpCustomer As New frmNewCustomer
                                objClpCustomer.CustomerNo = String.Empty
                                If IsNumeric(CardNo) Then
                                    objClpCustomer.SearchedValue = CardNo
                                End If
                                If (objClpCustomer.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                                    If (objClpCustomer.dtCustmInfo IsNot Nothing AndAlso objClpCustomer.dtCustmInfo.Rows.Count > 0) Then
                                        dt = objClpCustomer.dtCustmInfo()
                                        dtHD = objClpCustomer.dtCustmInfo()
                                        Me.DialogResult = Windows.Forms.DialogResult.OK
                                    End If
                                End If
                                objClpCustomer.Dispose()
                            End If
                            AssignCustomerInfoData(dt, objCustm)
                        End If
                    End If
                End If
                ' code added by khusrao adil for home delivery
                If CustomerSaleType = 2 Then
                    Dim dt = dtHD.Copy()
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        DisplayHomeDeliveryForm(dt)
                    End If
                End If
                'CtrlSalesPersons.CtrlTxtBox.Focus()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try


    End Sub
    Function SpaceChar(ByVal itm As String, ByVal qty As String, ByVal amt As String) As Integer
        Dim _spaceChar As Integer = 0
        Dim Checkqty As Integer = qty.LastIndexOf(".") + 1
        Dim strCheckValue As String = qty.Substring(Checkqty, qty.Length - Checkqty)
        Dim Checkamt As Integer = amt.LastIndexOf(".") + 1
        Dim strCheckamtValue As String = amt.Substring(Checkamt, amt.Length - Checkamt)
        If strCheckValue = "000" Then
            qty = qty.Remove(Checkqty - 1)
        End If
        If strCheckamtValue = "000" Or strCheckamtValue = "00" Then
            amt = amt.Remove(Checkamt - 1)
        End If
        _tatalItemsQty = qty
        _totalItemsAmount = amt
        If (itm.Length = 2 Or itm.Length = 1) AndAlso qty.Length = 7 Then
            _spaceChar = 3
            Return _spaceChar
        ElseIf (itm.Length = 2 Or itm.Length = 1) AndAlso (qty.Length = 6 Or qty.Length = 5) Then
            _spaceChar = 3
            Return _spaceChar
        ElseIf (itm.Length = 2 Or itm.Length = 1) AndAlso (qty.Length = 4 Or qty.Length = 3) Then
            _spaceChar = 5
            Return _spaceChar
        ElseIf (itm.Length = 2 Or itm.Length = 1) AndAlso qty.Length = 2 Then
            _spaceChar = 6
            Return _spaceChar
        ElseIf (itm.Length = 2 Or itm.Length = 1) AndAlso qty.Length = 1 Then
            _spaceChar = 6
            Return _spaceChar
        Else
            _spaceChar = 3
        End If
        Return SpaceChar
    End Function
    Private Function ThemeChange()

        CtrlSalesPersons.AlignChange = "Cash Memo"
        CashSummary.AlignChangeForCashSummary = "Cash Memo"
        rbnTabCM.Text = rbnTabCM.Text.ToUpper
        rbnTabCM.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        C1Ribbon1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black
        'c1SizerGrid.Size = New Size((My.Computer.Screen.WorkingArea.Width * 1094) / 1366, 416)
        'c1SizerGrid.Grid.Rows(0).Size = 23
        ' c1SizerGrid.Grid.Rows(1).Size = 23
        ' c1SizerGrid.Grid.Rows(2).Size = 165
        ' c1SizerGrid.Grid.Rows(3).Size = 30
        cmdEnable.VisualStyle = C1.Win.C1Input.VisualStyle.System
        cmdEnable.BackColor = Color.Transparent
        cmdEnable.BackColor = Color.FromArgb(0, 107, 163)
        cmdEnable.ForeColor = Color.FromArgb(255, 255, 255)
        cmdEnable.Font = New Font("Neo Sans", 7, FontStyle.Bold)
        cmdEnable.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdEnable.FlatStyle = FlatStyle.Flat
        cmdEnable.TextAlign = ContentAlignment.TopCenter
        cmdEnable.FlatAppearance.BorderSize = 0
        cmdEnable.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        lblCM.Font = New Font("Neo Sans", 7, FontStyle.Bold)
        lblCMNo.Font = New Font("Neo Sans", 7, FontStyle.Bold)
        lblCustSaleType.BackColor = Color.FromArgb(212, 212, 212)
        Me.cmdDefaultPromo.LargeImage = Global.Spectrum.My.Resources.Resources.defaultPromo_Normal
        Me.cmdApplySelectPromo.LargeImage = Global.Spectrum.My.Resources.Resources.SelectPromo_Normal
        Me.cmdClrAllPromo.LargeImage = Global.Spectrum.My.Resources.Resources.ClearPromo_Normal
        Me.cmdPayments.LargeImage = Global.Spectrum.My.Resources.Resources.payment_Normal
        Me.cmdCash.LargeImage = Global.Spectrum.My.Resources.Resources.Cash_Normal
        Me.cmdCard.LargeImage = Global.Spectrum.My.Resources.Resources.Card_Normal
        Me.cmdCreditSale.LargeImage = Global.Spectrum.My.Resources.Resources.credit_Normal
        Me.cmdDelete.LargeImage = Global.Spectrum.My.Resources.Resources.VoidBills_Normal
        Me.cmdCustomerinfo.LargeImage = Global.Spectrum.My.Resources.Resources.CustomerSearch_Normal
        Me.cmdReprint.LargeImage = Global.Spectrum.My.Resources.Resources.reprint_Normal
        Me.cmdOldCashMemo.LargeImage = Global.Spectrum.My.Resources.EditCash_Memo
        Me.cmdCheque.LargeImage = Global.Spectrum.My.Resources.PayByCheque
        Me.cmdNew.LargeImage = Global.Spectrum.My.Resources.NewCashMemo

        Me.cmdHold.LargeImage = Global.Spectrum.My.Resources._Resume
        Me.cmdDelete.LargeImage = Global.Spectrum.My.Resources.VoidBills_Normal
        Me.rbnbtnApplyCST.LargeImage = Global.Spectrum.My.Resources.ApplyTax
        Me.rbnbtnRoundOff.LargeImage = Global.Spectrum.My.Resources.RoundOff
        C1Ribbon1.DbtnF12.LargeImage = Global.Spectrum.My.Resources.Resources.PriceChange
        C1Ribbon1.DbtnF2.LargeImage = Global.Spectrum.My.Resources.Resources.ChangeQuantity
        Me.rbnGrpCMPromotion.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        Me.rbnGrpCMPromotion.Image = Global.Spectrum.My.Resources.defaultPromo_Normal
        Me.rbnGrpCMPromotion.ForeColorInner = Color.FromArgb(37, 37, 37)
        Me.rbnGrpPayments.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        Me.rbnGrpPayments.Image = Global.Spectrum.My.Resources.payment_Normal
        Me.rbnGrpPayments.ForeColorInner = Color.FromArgb(37, 37, 37)
        Me.rbnGrpCustSearch.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        Me.rbnGrpCustSearch.ForeColorInner = Color.FromArgb(37, 37, 37)
        Me.rbnGrpHoldVoid.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        Me.rbnGrpHoldVoid.ForeColorInner = Color.FromArgb(37, 37, 37)
        Me.RibbonGroup1.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        Me.RibbonGroup1.ForeColorInner = Color.FromArgb(37, 37, 37)
        Me.RibbonGroup2.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        Me.RibbonGroup2.ForeColorInner = Color.FromArgb(37, 37, 37)
        C1Ribbon1.DbtnF12.Font = New Font("Droid Sans Bold", 8, FontStyle.Bold)
        C1Ribbon1.DbtnF12.ForeColorInner = Color.FromArgb(37, 37, 37)
        C1Ribbon1.DbtnF2.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        C1Ribbon1.DbtnF2.ForeColorInner = Color.FromArgb(37, 37, 37)
        Me.rbCMNew.Font = New Font("Droid Sans Bold", 7, FontStyle.Bold)
        Me.rbCMNew.ForeColorInner = Color.FromArgb(37, 37, 37)

        rbnGrpCMPromotion.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbnGrpCMPromotion.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbnGrpCMPromotion.Text = rbnGrpCMPromotion.Text.ToUpper
        rbnGrpPayments.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbnGrpPayments.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbnGrpPayments.Text = rbnGrpPayments.Text.ToUpper
        rbnGrpCustSearch.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbnGrpCustSearch.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbnGrpCustSearch.Text = rbnGrpCustSearch.Text.ToUpper
        rbnGrpHoldVoid.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbnGrpHoldVoid.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbnGrpHoldVoid.Text = rbnGrpHoldVoid.Text.ToUpper
        RibbonGroup1.ForeColorOuter = Color.FromArgb(0, 107, 163)
        RibbonGroup1.ForeColorInner = Color.FromArgb(54, 54, 54)
        RibbonGroup1.Text = RibbonGroup1.Text.ToUpper
        RibbonGroup2.ForeColorOuter = Color.FromArgb(0, 107, 163)
        RibbonGroup2.ForeColorInner = Color.FromArgb(54, 54, 54)
        RibbonGroup2.Text = RibbonGroup2.Text.ToUpper
        rbCMNew.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbCMNew.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbCMNew.Text = rbCMNew.Text.ToUpper

        Me.cmdDefaultPromo.Text = cmdDefaultPromo.Text.ToUpper
        Me.cmdApplySelectPromo.Text = cmdApplySelectPromo.Text.ToUpper
        Me.cmdClrAllPromo.Text = cmdClrAllPromo.Text.ToUpper
        Me.cmdPayments.Text = cmdPayments.Text.ToUpper
        Me.cmdCash.Text = cmdCash.Text.ToUpper
        Me.cmdCard.Text = cmdCard.Text.ToUpper
        Me.cmdCreditSale.Text = cmdCreditSale.Text.ToUpper
        Me.cmdDelete.Text = cmdDelete.Text.ToUpper
        Me.cmdCustomerinfo.Text = cmdCustomerinfo.Text.ToUpper
        Me.cmdReprint.Text = cmdReprint.Text.ToUpper
        Me.cmdOldCashMemo.Text = cmdOldCashMemo.Text.ToUpper
        Me.cmdCheque.Text = cmdCheque.Text.ToUpper
        Me.cmdNew.Text = cmdNew.Text.ToUpper
        Me.cmdHold.Text = cmdHold.Text.ToUpper
        Me.cmdDelete.Text = cmdDelete.Text.ToUpper
        Me.rbnbtnApplyCST.Text = rbnbtnApplyCST.Text.ToUpper
        Me.rbnbtnRoundOff.Text = rbnbtnRoundOff.Text.ToUpper
        C1Ribbon1.DbtnF12.Text = C1Ribbon1.DbtnF12.Text.ToUpper
        C1Ribbon1.DbtnF2.Text = C1Ribbon1.DbtnF2.Text.ToUpper
        '  dgMainGrid.Size = New Size(1050, 219)
        '  dgMainGrid.MinimumSize = New Size(1050, 0)
        dgMainGrid.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)
        dgMainGrid.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgMainGrid.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgMainGrid.Rows.MinSize = 26
        dgMainGrid.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        dgMainGrid.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgMainGrid.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        dgMainGrid.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgMainGrid.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgMainGrid.CellButtonImage = Global.Spectrum.My.Resources.Delete
        ' ProductImage.Size = New Size(260, 128)
        'ProductImage.Location = New Size(1097, 159)
        ' CustInfo.Size = New Size(260, 517)

        CustInfo.BringToFront()

        'CustInfo.Location = New Size(1097, 291)
        ' CustInfo.BtnClearCustmInfo.Location = New Point(200, 4)
        ' TableLayoutPanel1.Size = New Size((My.Computer.Screen.WorkingArea.Width * 1345) / 1366, 165)
        ' TableLayoutPanel2.Location = New Point(900, 200)
        ' TableLayoutPanel2.Size = New Size((My.Computer.Screen.WorkingArea.Width * 260) / 1366, 420)
        ' TableLayoutPanel1.Location = New Point(0, 550)
        ' Me.TableLayoutPanel1.ColumnStyles.Insert(0, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        ' Me.TableLayoutPanel1.ColumnStyles.Insert(1, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.0!))
        ' Me.TableLayoutPanel1.ColumnStyles.Insert(2, New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.0!))
        'Payment.Size = New Size(260, 134)
        ' Payment.Location = New Point(731, 1)
        CtrlLblManualDisc.BackColor = Color.FromArgb(212, 212, 212)
        CtrlLblManualDisc.BorderStyle = BorderStyle.None
        CtrlLblManualDisc.TextAlign = ContentAlignment.MiddleLeft
        CtrlLblManualDisc.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        lblTotalPaymentHistory.ForeColor = Color.FromArgb(255, 255, 255)
        lblCalTotalItem.ForeColor = Color.FromArgb(255, 255, 255)
        lblTotalItemPaymentHistory.ForeColor = Color.FromArgb(255, 255, 255)
        lblCalTotalItemQty.ForeColor = Color.FromArgb(255, 255, 255)
        lblTotalItemQtyPaymentHistory.ForeColor = Color.FromArgb(255, 255, 255)
        lblTotalAmountPaymentHistory.ForeColor = Color.FromArgb(255, 255, 255)
        lblCalTotalAmount.ForeColor = Color.FromArgb(255, 255, 255)
        'For i = 0 To dgMainGrid.Cols.Count - 1
        '    dgMainGrid.Cols(i).Caption = dgMainGrid.Cols(i).Caption.ToUpper
        'Next
    End Function

#Region "Membership"
    Dim dtView As New DataTable
    Dim Dtcombo As New DataTable
    Dim DtUnique As New DataTable
    Public Sub CashMemoPrintforMemberShip(ByVal CashMemoNo As String, ByVal ClientName As String, ByVal dayopenDate As Date, ByVal LangCode As String, ByVal Currency As String, ByVal Sitecode As String, ByVal Type As String, ByVal DuplicatePrinting As String, ByVal DeletedUserid As String, ByVal AuthorisedUser As String, ByRef errorMsg As String, Optional ByVal GiftMsg As String = "", Optional ByVal BillAmt As String = "0", Optional ByVal PaidAmt As String = "0", Optional ByVal HierarachyWisePrintFlag As Boolean = False)
        Try
            Dim obj As New clsCashMemo
            dtView = obj.GetCashMemo(CashMemoNo, Sitecode, LangCode)
            DtUnique = obj.GetBillDetailsDataForPrint(CashMemoNo, Sitecode, LangCode)
            Dtcombo = obj.GetComboDetailsDataForPrint(CashMemoNo, Sitecode, LangCode)

            CashmemoBodyDetailsData()
            CashmemoHdrDetailsData(ClientName, LangCode, GiftMsg)
            CashmemoFooterDetailsData()
            GenerateKOTDandBillDetailsPrint()
        Catch ex As Exception
        End Try

    End Sub

    '  Dim objclsMemb As New clsMembership
    Dim dtCashMemohdrData As New DataTable
    Dim dtCashMemoBodyData As New DataTable
    Dim dtcashmemofooterData As New DataTable
    Private Function CashmemoHdrDetailsData(ByVal ClientName As String, ByVal LangCode As String, Optional ByVal GiftMsg As String = "") As Boolean
        Try

            Dim obj As New SpectrumBL.clsCashMemo()
            dtCashMemohdrData = objclsMemb.CashmemoHdrCMStruct()
            Dim dtMemb As New DataTable
            dtCashMemohdrData.Rows.Clear()
            If Not dtView Is Nothing AndAlso dtView.Rows.Count > 0 Then
                For Each drHdr As DataRow In dtView.Rows
                    Dim drData = dtCashMemohdrData.NewRow()
                    drData("ClientName") = ClientName
                    drData("StoreName") = drHdr("SITEOFFICIALNAME")
                    drData("Address") = drHdr("ADDRESSLINE1") + drHdr("ADDRESSLINE2") + drHdr("ADDRESSLINE3") + drHdr("ADDRESSPINCODE")
                    drData("PhoneNumber") = drHdr("TELNO")
                    drData("BillNo") = drHdr("BILLNO")
                    Dim dtMembData As DataTable = objclsMemb.GetMemberDetails(Membershipid, clsAdmin.SiteCode)
                    If Not dtMembData Is Nothing AndAlso dtMembData.Rows.Count > 0 Then
                        drData("MembershipId") = dtMembData.Rows(0)("MembershipId")
                        drData("CarNo") = dtMembData.Rows(0)("CarNo")
                    End If
                    drData("GiftMsg") = GiftMsg
                    drData("DineIn") = ""
                    drData("TokenNo") = drHdr("BILLNO").ToString().Trim.Substring(drHdr("BILLNO").ToString().Trim.Length - 2, 2)
                    drData("TaxAmt") = TTaxAmt
                    drData("DiscAmt") = FormatNumber(drHdr("Discount"), 2)

                    drData("RateAfterDisc") = TNetAmt
                    dtCashMemohdrData.Rows.Add(drData)

                    Membershipid = String.Empty
                    Exit For
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Dim TRateAfterDisc As Double = 0
    Dim TTaxAmt As Double = 0
    Dim TNetAmt As Double = 0
    Dim TGrossAmt As Double = 0
    Private Function CashmemoBodyDetailsData() As Boolean
        Try
            Dim TaxAmt As Double = 0
            Dim GrossAmt As Double = 0
            Dim NetAmt As Double = 0
            Dim RateAfterDisc As Double = 0

            TRateAfterDisc = 0
            TTaxAmt = 0
            TNetAmt = 0
            TGrossAmt = 0
            dtCashMemoBodyData = objclsMemb.GetCashmemoKOTBodyStruct()
            dtCashMemoBodyData.Rows.Clear()
            If Not DtUnique Is Nothing AndAlso DtUnique.Rows.Count > 0 Then
                For Each drBody As DataRow In DtUnique.Rows
                    Dim drBodyData = dtCashMemoBodyData.NewRow()
                    drBodyData("Description") = drBody("DISCRIPTION")
                    drBodyData("Qty") = drBody("QUANTITY")
                    drBodyData("Amt") = drBody("GROSSAMT")
                    RateAfterDisc = (drBody("SellingPrice") - drBody("TOTALDISCOUNT")).ToString()
                    TaxAmt = (drBody("TotalTaxAmount")).ToString
                    NetAmt = (drBody("NetAmount")).ToString
                    GrossAmt = (drBody("GrossAmt")).ToString
                    TRateAfterDisc = TRateAfterDisc + RateAfterDisc
                    TTaxAmt = TTaxAmt + TaxAmt
                    TNetAmt = TNetAmt + NetAmt
                    TGrossAmt = TGrossAmt + GrossAmt
                    dtCashMemoBodyData.Rows.Add(drBodyData)
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function CashmemoFooterDetailsData() As Boolean
        Try
            dtcashmemofooterData = objclsMemb.GetCashmemoKOTFooterStruct()
            dtcashmemofooterData.Rows.Clear()
            Dim VatTanString As String = "VAT/TIN NO: 27120029370 U/V"
            Dim LbtString As String = "LBT NO: TMC-LBT0005578-13"
            Dim msgString As String = "THANK YOU ... VISIT AGAIN"
            Dim drFooterData = dtcashmemofooterData.NewRow()

            drFooterData("VatTinString") = VatTanString
            drFooterData("LBTNo") = LbtString
            drFooterData("ThankYouMesg") = msgString
            dtcashmemofooterData.Rows.Add(drFooterData)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function GenerateKOTDandBillDetailsPrint() As Boolean
        Try

            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\KOTANDBILLCM.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()

            Dim DataSource1 As New ReportDataSource("dsCashMemoHdr", dtCashMemohdrData)
            Dim DataSource2 As New ReportDataSource("dsCashmemodtls", dtCashMemoBodyData)
            Dim DataSource3 As New ReportDataSource("DsFooterDetails", dtcashmemofooterData)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            reportViewer2.LocalReport.DataSources.Add(DataSource3)
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            Dim path As String = ""
            path = clsDefaultConfiguration.DayCloseReportPath & "\KOTANDBILLCM" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") & ".pdf"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            If clsDefaultConfiguration.PrintPreivewReq = True Then
                Process.Start(path)
            Else
                'Code For Print SO
                PrinterName = SetPrinterName(dtPrinterInfo, "CashMemo", "Billing")
                Dim pdfdocument As New PdfDocument()
                pdfdocument.LoadFromFile(path)
                pdfdocument.PrinterName = PrinterName
                pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                pdfdocument.PrintDocument.Print()
                pdfdocument.Dispose()
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function

    'code added by vipul for issue id 2736
    Private Function CheckKitArticleCodeInMstArticleKit(ByVal dt As DataTable, ByVal ArticleCode As String) As Boolean
        Try
            Dim dtextra As New DataTable
            dtextra = dt.Copy
            If dtextra.Rows(0)("ARTICLETYPE").ToString.ToUpper.Equals("KIT") Then
                If objCM.CheckArticlecodePresentInKit(ArticleCode) = False Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
#End Region
    Private Sub OpenCashDrawer(ByVal dsTemp As DataSet)
        Try
            Dim IsCashDrawerOpen As Boolean = False
            If clsAdmin.IsCashDrawer Then
                If Not dsTemp Is Nothing Then
                    If dsTemp.Tables.Contains("CashMemoReceipt") Then
                        Dim dt = dsTemp.Tables("CashMemoReceipt").Copy
                        For Each dr In dt.Rows
                            Dim strTender As String = ""
                            strTender = dr("TenderTypeCode").ToString.ToUpper
                            If clsDefaultConfiguration.TenderForCashDrawer.ToString.ToUpper.Contains(strTender) AndAlso dr("Status") = "True" Then
                                IsCashDrawerOpen = True
                                Exit For
                            End If
                        Next
                    End If
                End If
            End If
            If IsCashDrawerOpen = True Then
                If CheckAuthorisation(clsAdmin.UserCode, "OPENDRAWER") = True Then
                    Dim cA4Print As New clsA4Print
                    cA4Print.CashDrawerWithoutDriver = clsAdmin.CashDrawerWithoutDriver
                    cA4Print.OperateDevice("CashDrawer", CDflag:=clsDefaultConfiguration.CDBuildCode)
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class