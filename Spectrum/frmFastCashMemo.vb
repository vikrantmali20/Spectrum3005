﻿Imports SpectrumBL
Imports System.IO
Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient
Imports SpectrumPrint
Imports System.Collections
Imports SpectrumCommon
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
'Imports System.Globalization
'Imports System.Threading
Imports Microsoft.Reporting.WinForms
Imports Spire.Pdf
Imports System.IO.Ports
Imports Spectrum.BL
Imports System.Threading
''' <summary>
''' This class is used for cash Memo Functionality 
''' </summary>
''' <CreatedBy>Rama Ranjan Jena </CreatedBy>
''' <Updatedby></Updatedby>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
Public Class frmFastCashMemo
    Inherits CtrlRbnBaseForm
#Region "Global Varibale for class"
    Dim objSearch As New frmNCommonSearch
    Dim dtPrescDtlFastCashMemoAddToBill As New DataTable
    Dim _prescriptionArticleAmount As String = "0"
    Dim RecalculateTax As Boolean = False
    Const ConstCounter As Integer = 25
    Dim saveCashCounter = 0
    Dim objCM As New clsCashMemo
    Dim objArticleCombo As New clsArticleCombo
    Dim ObjclsCommon As New clsCommon
    Dim dsMain As New DataSet
    Dim dsCashMemoComboDtl As New DataSet
    Dim dtMainTax As New DataTable
    Dim DtManualPromo, dtGV As DataTable
    Dim UpdateFlag As Boolean = False
    Dim HoldFlag As Boolean = False
    Dim DvItemDetail As DataView
    Dim TotalExclusiveTax, RoundedAmt, CustomerBalancePoint As Double
    'Dim ShowEan As Boolean = False
    Dim CLPCardType, CLPCustomerId, clpCustomerMobileNo As String
    Dim PromotionCleared As Boolean = False
    Dim updateCustomerpoint As Boolean = False
    Dim GiftMsg As String = ""
    Dim pkey As String = ""
    Dim exitNowFlag As Boolean = False
    Private _dDueDate As Date
    Private _strRemarks As String
    Dim ValueChangeRemark As String = ""
    Public _remarks As String
    Public _paidAmt As String
    Public _billAmt As String
    Private _iArticleQtyBeforeChange As Decimal = 0
    Dim CardNo As String
    Dim ArticleButtonName As Spectrum.CtrlBtn
    Dim newval As Decimal
    Dim flag As Int32 = 0
    Dim comm As New clsCommon
    Dim WM_Active As UInteger = &H21
    Dim compqty As Decimal
    'Private IsInclusiveTax As Boolean
    Private IsCSTApplicable As Boolean
    Private _PriceBeforeChange As Nullable(Of Decimal)
    Private IsCancelOldCashMemo As Boolean
    Dim WeghingScaleBarcode = False
    Dim Weight As String = ""
    Dim SP As String = ""
    Private customerType As String = String.Empty
    Private filterScanArticle As String = String.Empty
    Private IsChangeQuantityfromNumPad As Boolean = False
    Private IsChangeQuantityOrPrice As Boolean = False
    Const CustSaleTypeTimerBlinkFrequency As Int32 = 250
    Private CustSaleTypeTimer As New System.Windows.Forms.Timer()
    Dim dtHD As DataTable = Nothing
    Private HDPrintRequired As Boolean = False
    Dim IsDefaultPromotion As Boolean = False
    Private IsTenderCredit As Boolean = False
    Private IsTenderCash As Boolean = False
    Private IsTenderCheque As Boolean = False
    Private IsTenderCreditCard As Boolean = False
    Dim IsRoundOffMsg As Boolean = False
    Private HDMobileNo As String = String.Empty
    Private CustomerSaleType As Integer = 0
    Private IsHoldEnterKey As Boolean = False
    Public IsAddToOrderMAp As Boolean = False
    '-----Added for if Scan Bill is set true then follow some rules
    Private ScanBill As Boolean = False
    Dim dtScanedBillArticle As New DataTable
    Dim ScanItemBillSequence As Integer = 0
    ' Create List of Dictionary of holding bill and total qty .
    'Dim ScanBillsDictionary As New  Dictionary(Of String, Integer)
    Dim ScaleBillNo As String = ""
    Dim ScaleBillIntDate As Long
    Dim objItemSch As New clsIteamSearch
    Dim FloatAmt As Double
    Dim EditMode_IsupdateDeliveryPersonAllowed As Boolean
    Public FirstBillTimer As New System.Windows.Forms.Timer()
    Private FirstBillTimeInterval As Integer = 15
    Dim dtAllSubGroups As New DataTable
    Dim parentGroup As String
    Dim LastPushPop As String = ""
    Dim subGroupsDictionary As New Stack
    Dim dtArticles As New DataTable
    Dim dtMembData As New DataTable
    Dim Membershipid As String = ""
    Dim _20x2Line As SerialPort
    Dim _tempItemName As String = ""
    Dim _tempItemAmount As String = ""
    Dim _tempItemQty As String = "0"
    Dim _totalItems As String = ""
    Dim _tatalItemsQty As String = ""
    Dim _totalItemsAmount As String = ""
    Dim PrevComboCode As String = ""
    Dim CurrentComboCode As String = ""
    Dim CmbCount As Integer = 0
    'added for Innoviti
    Dim QRTrailerSeperator As String = "" 'vipin 24042018 QR scan changes
    Dim QRTrailerSegment As String = ""
    Dim QRSepearatorSegment As String = ""
    Dim QRItemsScanString As String = ""
    Public cashmemoInnovitiList As New Dictionary(Of String, String)  'for innovatii
    'added by khusrao adil on 16-02-2018 for innviti with card functionality point _ natural client
    Dim dtInnoviti As DataTable
    '-------  Dine In Process ------
#Region "Dine In Process Variables"
    Dim currentDineInTable As Integer = 0
    Dim currentDineInBillNo As String
    Public IsNewOrder As Boolean = False
    Dim dsMainDineInHold As New DataSet
    Dim vSiteCode As String = clsAdmin.SiteCode
    Dim vfinancialYear As String = clsAdmin.Financialyear
    Dim vTerminalID As String = clsAdmin.TerminalID
    Dim vcurrentDate As Date = clsAdmin.CurrentDate
    Dim vCurrencyDescription As String = clsAdmin.CurrencyDescription
    Dim vBillno As String
    Public BillGenerateStatus As Integer = 0
    Dim DineInAutoSave As Boolean = False
    Dim IsBillGenerateColor As Boolean = False
    Dim IsMerge As Boolean = False
    Dim dtDineInItemRemarks As DataTable = GetDineInItemRemarksStruct()
    Dim dtMergeOrderBillNo As New DataTable
    Dim IsReset As Boolean = False
    Dim IsMembership As Boolean = False
    Dim objclsMemb As New clsMembership
    Dim dtExtend As New DataTable
    Dim objextend As New Extened
    Dim GSTComboArticleCopy As New DataTable   'vipin
    Dim StrSelectedComboArticle As String = ""
#End Region


#End Region

#Region "Properties"

    Enum enumCustomerSaleType
        Dine_In = 1
        Home_Delivery = 2
        Take_Away = 3
    End Enum

    Private _IsHOInstance As Boolean
    Private Property IsHOInstance As Boolean
        Get
            Return _IsHOInstance
        End Get
        Set(ByVal value As Boolean)
            _IsHOInstance = value
        End Set
    End Property
 ''added by ketan Savoy Outstanding changes 
    Public Shared _paymentTermId As String
    Public Shared Property PaymentTermId() As String
        Get
            Return _paymentTermId
        End Get
        Set(value As String)
            _paymentTermId = value
        End Set
    End Property
    Public Shared _ReservationGuestId As String
    Public Shared Property ReservationGuestId() As String
        Get
            Return _ReservationGuestId
        End Get
        Set(value As String)
            _ReservationGuestId = value
        End Set
    End Property
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
    Private Sub ApplyManualPromotion(ByVal Ean As String, Optional ByVal dPreviousQty As Decimal = 0)
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

            Dim dvArticle As New DataView(dsMain.Tables("CASHMEMODTL"), "EAN='" & Ean & "' AND BTYPE='S'", "Ean", DataViewRowState.CurrentRows)
            'Dim drMain As DataRow = dvArticle.Item(0).Row   
            Dim drMain As DataRow = dvArticle.Item(dvArticle.Count - 1).Row  'Changed by Gaurav Danani for combo functionality
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
            Dim decTotalDiscPercentage As Decimal
            decTotalDiscPercentage = IIf(drMain("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, drMain("TOTALDISCPERCENTAGE"))
            If TotalQty > 0 Then
                If dPreviousQty > 0 Then
                    drMain("TOTALDISCPERCENTAGE") = ((decTotalDiscPercentage * dPreviousQty) + (DiscountPer * (TotalQty - dPreviousQty))) / TotalQty
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
    '    If clsDefaultConfiguration.InclusiveTaxDisplay.ToUpper() = "TRUE" Then
    '        If objCM.CheckIfInclusiveTax(clsAdmin.SiteCode, "CMS") Then
    '            IsInclusiveTax = True
    '        End If
    '    End If
    'End Sub

    ''' <summary>
    ''' Binding the Controls with the DataSet
    ''' </summary>
    ''' <param name="StrArticle">ArticleCode</param>
    ''' <remarks></remarks>
    Private Sub getBinding()
        Try
            Dim flp As New FlowLayoutPanel
            dsMain = objCM.GetStruc("0", "0")
            dsMain.Tables("CASHMEMODTL").Columns("IsPriceChanged").DefaultValue = False
            dsCashMemoComboDtl = objCM.GetCashMemoComboDtlStruc()
            DvItemDetail = New DataView(dsMain.Tables("CASHMEMODTL"), "", "BillLineNo Desc", DataViewRowState.CurrentRows)
            dgMainGrid.DataSource = DvItemDetail
            'dgMainGrid.DataSource = dsMain.Tables("CASHMEMODTL")
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
            dsMainDineInHold = dsMain.Copy()

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
                CMbtnBottom.CtrlBtnSaleGV.Enabled = True
                CMbtnBottom.CtrlBtnSaleCLPPoint.Enabled = True
            Else
                CMbtnBottom.CtrlBtnSaleGV.Enabled = False
                CMbtnBottom.CtrlBtnSaleCLPPoint.Enabled = False
            End If
            If clsDefaultConfiguration.CLPPointSaleAllowed = True Then
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
            'Rakesh-10.11.2013-7598 & 7595->Hide IFBNO column and resize gross amount coulm
            dgMainGrid.Cols("IFBNO").Caption = " "
            dgMainGrid.Cols("IFBNO").Visible = False
            dgMainGrid.Cols("Selects").Caption = " "
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                dgMainGrid.Cols("Selects").Width = 18
            Else
                dgMainGrid.Cols("Selects").Width = 30
            End If
            dgMainGrid.Cols("Selects").ComboList = "..."

            If (clsDefaultConfiguration.BarcodeDisplayAllowed) Then
                'added by Khusrao Adil
                ' for savoy
                If clsDefaultConfiguration.IsSavoy Then
                    dgMainGrid.Cols("EAN").Caption = "Model No."
                Else
                    dgMainGrid.Cols("EAN").Caption = IIf(resourceMgr Is Nothing, "Barcode", getValueByKey("frmcashmemo.dgmaingrid.ean"))
                End If
                dgMainGrid.Cols("EAN").AllowEditing = False
                dgMainGrid.Cols("EAN").Width = 175
                dgMainGrid.Cols("EAN").Visible = True
            Else
                dgMainGrid.Cols("EAN").Visible = False
            End If
            dgMainGrid.Cols("Btype").AllowEditing = False
            dgMainGrid.Cols("DISCRIPTION").Width = 140
            'dgMainGrid.Cols("DISCRIPTION").Caption = IIf(resourceMgr Is Nothing, "Item Description", getValueByKey  ("frmCashMemo_dgmainGrid_col_ItemDesc"))
            dgMainGrid.Cols("DISCRIPTION").Caption = IIf(resourceMgr Is Nothing, "Item Description", getValueByKey("frmcashmemo.dgmaingrid.discription"))
            dgMainGrid.Cols("DISCRIPTION").AllowEditing = False
            'dgMainGrid.Cols("STOCK").Width = 62
            'dgMainGrid.Cols("STOCK").Caption = IIf(resourceMgr Is Nothing, "Stock", getValueByKey  ("frmCashMemo_dgmainGrid_col_Stock"))
            'dgMainGrid.Cols("STOCK").AllowEditing = False
            dgMainGrid.Cols("ArticleCode").Width = 100
            'dgMainGrid.Cols("ArticleCode").Caption = IIf(resourceMgr Is Nothing, "Item Code", getValueByKey  ("frmCashMemo_dgmainGrid_col_Item"))
            dgMainGrid.Cols("ArticleCode").Caption = IIf(resourceMgr Is Nothing, "Item Code", getValueByKey("frmcashmemo.dgmaingrid.articlecode"))
            dgMainGrid.Cols("ArticleCode").AllowEditing = False
            dgMainGrid.Cols("ArticleCode").Visible = False
            dgMainGrid.Cols("Quantity").Width = 70
            dgMainGrid.Cols("TakeAwayQuantity").Width = 99
            'dgMainGrid.Cols("Quantity").Caption = IIf(resourceMgr Is Nothing, "Qty", getValueByKey  ("frmCashMemo_dgmainGrid_col_Quantity"))
            dgMainGrid.Cols("Quantity").Caption = IIf(resourceMgr Is Nothing, "Qty", getValueByKey("frmcashmemo.dgmaingrid.quantity"))

            If (clsDefaultConfiguration.AllowDecimalQty) Then
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
            dgMainGrid.Cols("SellingPrice").Width = 60
            'dgMainGrid.Cols("SellingPrice").Caption = IIf(resourceMgr Is Nothing, "Price", getValueByKey  ("frmCashMemo_dgmainGrid_col_Mrp"))
            'If Not IsInclusiveTax Then
            '    dgMainGrid.Cols("SellingPrice").Caption = "Price" 'IIf(resourceMgr Is Nothing, "Price", getValueByKey("frmcashmemo.dgmaingrid.sellingprice"))
            'Else
            dgMainGrid.Cols("SellingPrice").Caption = IIf(resourceMgr Is Nothing, "Price", getValueByKey("frmcashmemo.dgmaingrid.sellingprice"))
            'End If
            dgMainGrid.Cols("SellingPrice").AllowEditing = False
            dgMainGrid.Cols("TOTALDISCPERCENTAGE").Width = 62
            'dgMainGrid.Cols("TOTALDISCPERCENTAGE").Caption = IIf(resourceMgr Is Nothing, "Disc%", getValueByKey  ("frmCashMemo_dgmainGrid_col_Disc"))
            dgMainGrid.Cols("TOTALDISCPERCENTAGE").Caption = IIf(resourceMgr Is Nothing, "Disc%", getValueByKey("frmcashmemo.dgmaingrid.totaldiscpercentage"))
            If clsDefaultConfiguration.EvasPizzaChanges = True Then
                If CheckAuthorisation(clsAdmin.UserCode, "ORD") Then
                    dgMainGrid.Cols("TOTALDISCPERCENTAGE").AllowEditing = True
                Else
                    dgMainGrid.Cols("TOTALDISCPERCENTAGE").AllowEditing = False
                End If
            Else
                dgMainGrid.Cols("TOTALDISCPERCENTAGE").AllowEditing = False
            End If
            dgMainGrid.Cols("TOTALDISCPERCENTAGE").Format = "0.00"
            dgMainGrid.Cols("NETAMOUNT").Width = 72
            'dgMainGrid.Cols("NETAMOUNT").Caption = IIf(resourceMgr Is Nothing, "Net Amt", getValueByKey  ("frmCashMemo_dgmainGrid_col_NetAmount"))
            'If Not IsInclusiveTax Then
            '    dgMainGrid.Cols("NETAMOUNT").Caption = IIf(resourceMgr Is Nothing, "Net Amt", getValueByKey("frmcashmemo.dgmaingrid.netamount"))
            'Else
            dgMainGrid.Cols("NETAMOUNT").Caption = IIf(resourceMgr Is Nothing, "Net Amt", getValueByKey("frmcashmemo.dgmaingrid.netamountIncTax"))
            dgMainGrid.Cols("NETAMOUNT").TextAlign = TextAlignEnum.RightCenter
            dgMainGrid.Cols("NETAMOUNT").TextAlignFixed = TextAlignEnum.RightCenter
            'End If TOTALTAXAMOUNT
            dgMainGrid.Cols("NETAMOUNT").Format = "0.00"
            dgMainGrid.Cols("NETAMOUNT").AllowEditing = False
            dgMainGrid.Cols("TOTALDISCOUNT").Width = 70
            dgMainGrid.Cols("TOTALDISCOUNT").Caption = getValueByKey("frmcashmemo.dgmaingrid.discamt")
            dgMainGrid.Cols("TOTALDISCOUNT").Format = "0.00"
            If clsDefaultConfiguration.EvasPizzaChanges = True Then
                If CheckAuthorisation(clsAdmin.UserCode, "ORD") Then
                    dgMainGrid.Cols("TOTALDISCOUNT").AllowEditing = True
                Else
                    dgMainGrid.Cols("TOTALDISCOUNT").AllowEditing = False
                End If
            Else
                dgMainGrid.Cols("TOTALDISCOUNT").AllowEditing = False
            End If

            'If IsInclusiveTax Then
            dgMainGrid.Cols("TOTALTAXAMOUNT").Visible = True
            dgMainGrid.Cols("TOTALTAXAMOUNT").Width = 55
            dgMainGrid.Cols("TOTALTAXAMOUNT").Caption = getValueByKey("frmcashmemo.dgmaingrid.taxamt")
            dgMainGrid.Cols("TOTALTAXAMOUNT").Format = "0.00"
            dgMainGrid.Cols("TOTALTAXAMOUNT").AllowEditing = False
            'End If

            dgMainGrid.Cols("CLPRequire").Caption = getValueByKey("frmcashmemo.dgmaingrid.clprequire")
            dgMainGrid.Cols("CLPRequire").Width = 40
            dgMainGrid.Cols("CLPRequire").Visible = False
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

            If (Screen.PrimaryScreen.Bounds.Width >= 1025) Then
                dgMainGrid.Cols("Quantity").Width = 75
                dgMainGrid.Cols("SellingPrice").Width = 85
                dgMainGrid.Cols("DISCRIPTION").Width = 195
                dgMainGrid.Cols("TakeAwayQuantity").Width = 99
            End If

            If (Screen.PrimaryScreen.Bounds.Width <= 1025 AndAlso Not clsDefaultConfiguration.NumpadRequired) Then
                dgMainGrid.Cols("DISCRIPTION").Width = 180
            End If
            'If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            '    For i = 0 To dgMainGrid.Cols.Count - 1
            '        dgMainGrid.Cols(i).Caption = dgMainGrid.Cols(i).Caption.ToUpper
            '    Next
            'End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                dgMainGrid.Cols("Quantity").TextAlignFixed = TextAlignEnum.RightCenter
                dgMainGrid.Cols("SellingPrice").TextAlignFixed = TextAlignEnum.RightCenter
                ' dgMainGrid.Cols("DISCRIPTION").TextAlignFixed = TextAlignEnum.RightCenter
                dgMainGrid.Cols("TakeAwayQuantity").TextAlignFixed = TextAlignEnum.RightCenter
                dgMainGrid.Cols("NETAMOUNT").TextAlignFixed = TextAlignEnum.RightCenter
                dgMainGrid.Cols("TOTALDISCPERCENTAGE").TextAlignFixed = TextAlignEnum.RightCenter
                dgMainGrid.Cols("TOTALDISCOUNT").TextAlignFixed = TextAlignEnum.RightCenter
                dgMainGrid.Cols("TOTALTAXAMOUNT").TextAlignFixed = TextAlignEnum.RightCenter
            End If
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
                    'dr("LINEDISCOUNT") = dr("TOTALDISCOUNT")
                End If
                dr("GrossAmt") = dr("Quantity") * dr("SellingPrice")

                'code added  by vipul for Customer wise discount
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



                'code commeneted by vipul for customer wise discount
                '  Dim excTaxAmount = dtMainTax.Compute("SUM(TAXAMOUNT)", String.Format("Inclusive=0 AND BillLineNo={0}", dr("BillLineNo")))
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
                    'code commented by vipul for customer wise discount
                    ' dr("TOTALDISCPERCENTAGE") = IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, Math.Round((dr("TOTALDISCOUNT") / dr("GrossAmt")) * 100))

                    'code added  by vipul for Customer wise discount
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
    Private Function qtycalc(ByVal stritem As String, ByVal dt As DataTable) As DataTable
        Try
            Dim tmpdt As DataTable
            tmpdt = dt
            Dim filter As String = "BTYPE='S'"
            If stritem <> String.Empty Then
                filter = filter & " AND  EAN='" & stritem & "'"
            End If
            For Each dr As DataRow In tmpdt.Select(filter, "Ean", DataViewRowState.CurrentRows)
                dr("Quantity") = newval / dr("sellingprice")

            Next
            Return tmpdt
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    ''' <summary>
    ''' Validating the flow Or Form Validations 
    ''' </summary>
    ''' <returns>True and False</returns>
    ''' <remarks></remarks>
    Private Function ValidateAll() As Boolean
        Try
            Dim TotalNetValue As Double = CashSummary.CtrllblNetAmt.Text
            Dim TotalCollection As Double = Val(dsMain.Tables("CASHMEMORECEIPT").Compute("Sum(AMOUNTTENDERED)", "").ToString())
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

            If TotalNetValue < 0 AndAlso clsAdmin.CVProgram = String.Empty Then
                ShowMessage(getValueByKey("CM056"), "CM056 - " & getValueByKey("CLAE05"))
                Return False
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
            For Each dt As DataTable In dsMain.Tables
                dt.Clear()
            Next
            If dsMain.Tables.Contains("CheckDtls") Then
                dsMain.Tables.Remove("CheckDtls")
            End If
            If Not dtGV Is Nothing Then dtGV.Clear()
            If Not dtHD Is Nothing Then dtHD.Clear()
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
            If clsDefaultConfiguration.EvasPizzaChanges = True Then
                CustInfo.ctrlTxtAddress.Enabled = True
                CustInfo.ctrlTxtAddress.ReadOnly = False
                CustInfo.ctrlTxtAddress.Text = String.Empty
                CustInfo.ctrlTxtAddress.Enabled = False
                CustInfo.ctrlTxtAddress.ReadOnly = True
            End If
            HoldFlag = False
            lblCalTotalAmount.Text = 0.0
            lblCalTotalItem.Text = 0
            lblCalTotalItemQty.Text = 0
            PromotionCleared = False
            updateCustomerpoint = False
            EditMode_IsupdateDeliveryPersonAllowed = False
            IsDefaultPromotion = False
            CashSummary.CtrlLabel3.Text = "Disc Amt."
            If clsDefaultConfiguration.DineInProcess Then
                For Each dt As DataTable In dsMainDineInHold.Tables
                    dt.Clear()
                Next
                If Not dtDineInItemRemarks Is Nothing Then dtDineInItemRemarks.Clear()
                BillGenerateStatus = 0
            End If
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
            dr("COSTAMT") = Val(dsMain.Tables("CASHMEMODtl").Compute("sum(COSTPrice)", "").ToString())
            dr("TOTALITEMS") = IIf(dsMain.Tables("CASHMEMODtl").Compute("SUM(QUANTITY)", "") Is DBNull.Value, 0, dsMain.Tables("CASHMEMODtl").Compute("SUM(QUANTITY)", ""))
            dr("CREDITNOTENO") = 0
            dr("TRANSCURRENCY") = 0

            If CustInfo.CtrlTxtCustomerNo.Text <> String.Empty AndAlso customerType.Equals("CLP") Then
                dr("CLPSCHEME") = clsAdmin.CLPProgram
                dr("CLPPOINTS") = IIf(dsMain.Tables("CASHMEMODtl").Compute("SUM(CLPPOINTS)", "") Is DBNull.Value, 0, dsMain.Tables("CASHMEMODtl").Compute("SUM(CLPPOINTS)", ""))
                dr("CLPDISCOUNT") = IIf(dsMain.Tables("CASHMEMODtl").Compute("SUM(CLPDISCOUNT)", "") Is DBNull.Value, 0, dsMain.Tables("CASHMEMODtl").Compute("SUM(CLPDISCOUNT)", ""))
                dr("BALANCEPOINTS") = IIf(CustInfo.ctrlTxtPoints.Text <> String.Empty, CustInfo.ctrlTxtPoints.Text, 0) + IIf(dr("CLPPOINTS") Is DBNull.Value, 0, dr("CLPPOINTS"))

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
            Dim oldLineNo = 0, newLineNo As Integer = 0
            Dim dt As DataTable = ds.Tables(TableName)

            For i = 0 To dt.Rows.Count - 1
                oldLineNo = dt.Rows(i)("BillLineNo")
                newLineNo = i + 1

                dt.Rows(i)("BillLineNo") = newLineNo
                If ds.Tables.Contains("CASHMEMOTAXDTLS") = True Then
                    Dim j As Int16 = 1
                    For Each dr As DataRow In ds.Tables("CASHMEMOTAXDTLS").Select("BillLineNo = " & oldLineNo & " AND EAN='" & dt.Rows(i)("EAN").ToString() & "'", "", DataViewRowState.CurrentRows)
                        dr("BillLineNo") = newLineNo
                        dr("StepNo") = j
                        j = j + 1
                    Next
                End If

                If Not dtMainTax Is Nothing Then
                    Dim j As Int16 = 1
                    For Each dr As DataRow In dtMainTax.Select("BillLineNo = " & oldLineNo & " AND EAN='" & dt.Rows(i)("EAN").ToString() & "'", "", DataViewRowState.CurrentRows)
                        dr("BillLineNo") = newLineNo
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
    Private Sub calculateTotalbill()
        Try
            CashSummary.CtrllblNetAmt.Text = Format(Val(dsMain.Tables("CASHMEMODtl").Compute("SUM(NETAMOUNT)", "").ToString()), "0.00")
            'CashSummary.CtrllblNetAmt.Text = Math.Round(CDec(CashSummary.CtrllblNetAmt.Text))
            'lblTotalQty.Text = Format(Val(dsMain.Tables("CASHMEMODtl").Compute("SUM(QUANTITY)", "").ToString()), "0.00")
            'CashSummary.CtrllblDiscAmt.Text = Format(Val(dsMain.Tables("CASHMEMODtl").Compute("SUM(TOTALDISCOUNT)", "TOTALDISCOUNT >0").ToString()), "0.00")
            If IsRoundOffMsg Then
                CashSummary.CtrlLabel3.Text = "Roundoff Amt."
                CashSummary.CtrlLabel3.Tag = "Roundoff Amt."
                'IsRoundOffMsg = False
            Else
                CashSummary.CtrlLabel3.Text = "Disc. Amt."
                CashSummary.CtrlLabel3.Tag = "Disc. Amt."
            End If
            'CashSummary.CtrllblDiscAmt.Text = Format(Val(dsMain.Tables("CASHMEMODtl").Compute("SUM(LINEDISCOUNT)", "").ToString()), "0.00")
            If clsDefaultConfiguration.IsMembership Then '' MemberShip Discount Issue 
                CashSummary.CtrllblDiscAmt.Text = Format(Val(dsMain.Tables("CASHMEMODtl").Compute("SUM(TotalDiscount)", "").ToString()), "0.00")
            Else
                'code commented by vipul for customer wise discount
                ' CashSummary.CtrllblDiscAmt.Text = Format(Val(dsMain.Tables("CASHMEMODtl").Compute("SUM(LINEDISCOUNT)", "").ToString()), "0.00")
                'code added  by vipul for Customer wise discount
                If clsDefaultConfiguration.customerwisepricemanagement = True Then
                    CashSummary.CtrllblDiscAmt.Text = Format(Val(dsMain.Tables("CASHMEMODtl").Compute("SUM(TotalDiscount)", "").ToString()), "0.00")
                Else
                    CashSummary.CtrllblDiscAmt.Text = Format(Val(dsMain.Tables("CASHMEMODtl").Compute("SUM(LINEDISCOUNT)", "").ToString()), "0.00")
                End If
            End If
            CashSummary.CtrllblGrossAmt.Text = Format(Val(dsMain.Tables("CASHMEMODtl").Compute("SUM(GROSSAMT)", "").ToString()), "0.00")
            Dim PosTotal, NegTotal As Double
            PosTotal = Val(dsMain.Tables("CASHMEMODtl").Compute("SUM(NETAMOUNT)", "NETAMOUNT > 0").ToString())
            NegTotal = Val(dsMain.Tables("CASHMEMODtl").Compute("SUM(NETAMOUNT)", "NETAMOUNT < 0").ToString())
            NegTotal = NegTotal * -1
            Dim PosTax, NegTax As Double
            PosTax = IIf(PosTotal > 0, GetBillLabelTax(PosTotal), 0)
            NegTax = IIf(NegTotal > 0, GetBillLabelTax(NegTotal), 0)
            Dim tax As Double = PosTax - NegTax
            If tax <> 0.0 Then
                CashSummary.CtrllblNetAmt.Text = CashSummary.CtrllblNetAmt.Text + tax
            End If

            Dim ItemTax As Double = IIf(dsMain.Tables("CASHMEMODtl").Compute("SUM(EXCLUSIVETAX)", "") Is DBNull.Value, 0.0, dsMain.Tables("CASHMEMODtl").Compute("SUM(EXCLUSIVETAX)", ""))
            If ItemTax > 0.0 Then
                TotalExclusiveTax = ItemTax + tax
            Else
                TotalExclusiveTax = tax
            End If
            Dim TotalItemTax As Double = IIf(dsMain.Tables("CASHMEMODtl").Compute("SUM(TOTALTAXAMOUNT)", "") Is DBNull.Value, 0.0, dsMain.Tables("CASHMEMODtl").Compute("SUM(TOTALTAXAMOUNT)", ""))
            Dim TotalTax As Double = TotalItemTax + tax
            '--changed for bug no 533 totaltaxamount in exchanege of exclusive tax 
            'CashSummary.CtrllblTaxAmt.Text = Format(Math.Round(TotalExclusiveTax, 2), "0.00")
            'CashSummary.CtrllblTaxAmt.Text = Format(Math.Round(TotalTax, 2), "0.00")
            CashSummary.CtrllblTaxAmt.Text = Format(TotalTax, "0.00")
            Dim NetAmt As Double = CDbl(CashSummary.CtrllblNetAmt.Text)
            If clsDefaultConfiguration.RoundOffRequired = True Then

                CashSummary.CtrllblNetAmt.Text = Format(MyRound(CDec(CashSummary.CtrllblNetAmt.Text), clsDefaultConfiguration.BillRoundOffAt), "0.00")
                RoundedAmt = NetAmt - CDbl(CashSummary.CtrllblTaxAmt.Text)
            Else
                RoundedAmt = 0
            End If

            lblCalTotalItem.Text = dsMain.Tables("CASHMEMODtl").Rows.Count
            lblCalTotalItemQty.Text = Val(dsMain.Tables("CASHMEMODtl").Compute("SUM(QUANTITY)", "QUANTITY>0").ToString())
            lblCalTotalItemQty.Text = Val(lblCalTotalItemQty.Text.ToString()) + Val(dsMain.Tables("CASHMEMODtl").Compute("SUM(QUANTITY)", "QUANTITY <0").ToString()) * -1

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
            Dim totalComboItemQuantity As Integer = 0
            For Each dr As DataRow In dsMain.Tables("CASHMEMODtl").Rows
                If (dr.RowState <> DataRowState.Deleted AndAlso objArticleCombo.CheckIfComboArticle(dr("ArticleCode"))) Then
                    Dim comboItemQuantity As Integer = 0
                    comboItemQuantity = objArticleCombo.GetComboItemQuantity(dr("ArticleCode"))
                    If comboItemQuantity <> 0 Then
                        totalComboItemQuantity -= dr("QUANTITY")
                        totalComboItemQuantity += comboItemQuantity * dr("QUANTITY")
                    End If
                End If
            Next
            If totalComboItemQuantity > 0 Then
                lblCalTotalItemQty.Text = Format(Val(lblCalTotalItemQty.Text.ToString()) + totalComboItemQuantity, "0.00")
            End If
            If Val(CashSummary.CtrllblNetAmt.Text.ToString()) <= 0 Then
                SetButtons(2, False)
            Else
                SetButtons(2, True)
            End If
            'CtrlSalesPersons.CtrlTxtBox.Select()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                CashSummary.CtrlLabel3.Text = CashSummary.CtrlLabel3.Text.ToUpper
                CashSummary.CtrllblTaxAmt.Text = CashSummary.CtrllblTaxAmt.Text.ToUpper
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
    ''added By ketan Savoy Outstanding Changes 
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
            Return Format(DiscountAmt, "0.00")
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
            Return 0
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
                    '' added by ketan tax changes 
                    ''dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, "", "CMS", 0)
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
                        If Val(dtTaxCalc.Rows(iRowTax)("VALUE").ToString) <> 0 Then
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

    ''' <summary>
    ''' Assign Tax to the Row
    ''' </summary>
    ''' <param name="strMatcode">Article Code</param>
    ''' <param name="TaxableAmount">Amount on which Tax calulated </param>
    ''' <param name="iRow"> Row No</param>
    ''' <param name="EAN">Ean Code</param>
    ''' <returns>Tax amount</returns>
    ''' <remarks></remarks>

    Public Function CreateDataSetForTaxCalculation(ByRef originalTaxAmt As Double, ByVal LineNo As String, ByVal strMatcode As String, ByVal TaxableAmount As Double, ByVal iRow As Integer, ByVal ItemQty As Double, Optional ByVal EAN As String = "", Optional ByRef dt As DataTable = Nothing, Optional ByRef dtCashMemoDtl As DataTable = Nothing) As Object
        Try
            Dim StrTaxCode As Double = 0
            Dim dtTaxCalc As DataTable
            dtTaxCalc = New DataTable
            Dim dvTax As DataView
            If clsDefaultConfiguration.IsMembership Then
                If Not dt Is Nothing Then
                    dvTax = New DataView(dt, "BillLineNo='" & LineNo & "' AND ArticleCode='" & strMatcode & "'", "StepNo", DataViewRowState.CurrentRows)
                Else
                    dvTax = New DataView(dtMainTax, "BillLineNo='" & LineNo & "' AND ArticleCode='" & strMatcode & "'", "StepNo", DataViewRowState.CurrentRows)
                End If
            Else
                dvTax = New DataView(dtMainTax, "BillLineNo='" & LineNo & "' AND ArticleCode='" & strMatcode & "'", "StepNo", DataViewRowState.CurrentRows)
            End If
            If dvTax.Count > 0 Then
                dtTaxCalc = dvTax.ToTable()
                dvTax.AllowDelete = True
                Dim i As Integer
                For i = dvTax.Count - 1 To 0 Step -1
                    dvTax.Delete(i)
                Next
            Else
                If IsCSTApplicable Then
                    '    dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, "", "CMS", ItemQty, EAN, clsDefaultConfiguration.CSTTaxCode, True)
                    'Else
                    '    dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", ItemQty, EAN, clsDefaultConfiguration.CSTTaxCode, False)
                    '' ------ added by ketan article tax based on order type 
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
                If clsDefaultConfiguration.IsMembership Then
                    If Not dtCashMemoDtl Is Nothing AndAlso dtCashMemoDtl.Rows.Count > 0 Then
                        For Each dr As DataRow In dtCashMemoDtl.Select(filterScanArticle, "", DataViewRowState.CurrentRows)
                            dr("EXCLUSIVETAX") = dbExclTotalTax
                            'dr("TOTALTAXAMOUNT") = Format(StrTaxCode, "0.00")
                            dr("TOTALTAXAMOUNT") = StrTaxCode
                        Next
                    Else
                        For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Select(filterScanArticle, "", DataViewRowState.CurrentRows)
                            dr("EXCLUSIVETAX") = dbExclTotalTax
                            'dr("TOTALTAXAMOUNT") = Format(StrTaxCode, "0.00")
                            dr("TOTALTAXAMOUNT") = StrTaxCode
                        Next
                    End If

                Else
                    For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Select(filterScanArticle, "", DataViewRowState.CurrentRows)
                        dr("EXCLUSIVETAX") = dbExclTotalTax
                        'dr("TOTALTAXAMOUNT") = Format(StrTaxCode, "0.00")
                        dr("TOTALTAXAMOUNT") = StrTaxCode
                    Next
                End If


                'dsMain.Tables("CASHMEMODTL").Rows(iRow - 1)("EXCLUSIVETAX") = dbExclTotalTax
                If clsDefaultConfiguration.IsMembership Then
                    If Not dt Is Nothing Then
                        dt.Merge(dtTaxCalc, False, MissingSchemaAction.Ignore)
                        dt.AcceptChanges()
                    Else
                        dtMainTax.Merge(dtTaxCalc, False, MissingSchemaAction.Ignore)
                        dtMainTax.AcceptChanges()
                    End If
                Else
                    dtMainTax.Merge(dtTaxCalc, False, MissingSchemaAction.Ignore)
                    dtMainTax.AcceptChanges()
                End If

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
                    '    dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, True)
                    'Else
                    '    dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", dr("Quantity"), EAN, clsDefaultConfiguration.CSTTaxCode, False)
                    '' ------ added by ketan article tax based on order type 
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
                    Dim amt As Double = GetTaxableAmountForCst(strMatcode, EAN, dr("Quantity"), TaxableAmount)
                    originalTaxAmt = amt
                    dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount - amt
                    objCM.getCalculatedDataSet(dtTaxCalc)
                Else
                    dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
                    '----- Code Added By Mahesh Now I need to calculate for Qty
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

    Private Function GetTaxableAmountForCst(ByVal strMatcode As String, ByVal EAN As String, ByVal Quantity As Double, ByVal TaxableAmount As Double) As Double
        Dim dtTaxCalc As DataTable
        dtTaxCalc = New DataTable
        ''added  by ketan tax changes
        '' dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", Quantity, EAN, clsDefaultConfiguration.CSTTaxCode, False)
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
        dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
        objCM.getCalculatedDataSet(dtTaxCalc)
        Return dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")
    End Function

    Private Sub ClearDiscountAndExistingTax()
        dtMainTax.Rows.Clear()
        For Each dr As DataRow In dsMain.Tables("Cashmemodtl").Rows
            dr("TotalDiscount") = 0
            dr("LineDiscount") = 0
            dr("TOTALDISCPERCENTAGE") = 0
            dr("FIRSTLEVEL") = String.Empty
            dr("TOPLEVEL") = String.Empty
            dr("MANUALPROMO") = 0
            Dim taxAmt As Double
            CalculateTotalInclusiveTax(taxAmt, 0, dr("ARTICLECODE"), dr("GrossAmt"), 1, dr("EAN"))
            dr("TotalTaxAmount") = taxAmt
        Next
        isCashierPromoSelected = False
        ReCalculateCM("")
        calculateTotalbill()
    End Sub

    Private Function CalculateTotalInclusiveTax(ByRef originalTaxAmt As Double, ByVal LineNo As String, ByVal strMatcode As String, ByVal TaxableAmount As Double, ByVal quantity As Double, ByVal EAN As String) As Double
        Try
            Dim dtTaxCalc As DataTable
            dtTaxCalc = New DataTable
            Dim dvTax As New DataView(dtMainTax, "BillLineNo='" & LineNo & "' AND ArticleCode='" & strMatcode & "'", "StepNo", DataViewRowState.CurrentRows)
            If dvTax.Count > 0 Then
                dtTaxCalc = dvTax.ToTable()
                dvTax.AllowDelete = True
                Dim i As Integer
                For i = dvTax.Count - 1 To 0 Step -1
                    dvTax.Delete(i)
                    dtTaxCalc.Rows(i).AcceptChanges()
                Next
            Else
                If IsCSTApplicable Then
                    ' dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, True)
                    If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
                        If CustomerSaleType = 1 Then 'Dine_In = 1
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "DIN", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, True)
                        ElseIf CustomerSaleType = 2 Then  ' Home_Delivery = 2
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "HOD", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, True)
                        ElseIf CustomerSaleType = 3 Then 'Take_Away = 3
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "TAK", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, True)
                        Else
                            dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, True)
                        End If
                    Else
                        dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, True)
                    End If
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
            End If
            If objArticleCombo.CheckIfComboArticle(strMatcode) Then 'vipin
                dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, strMatcode, "CMS", quantity, EAN, clsDefaultConfiguration.CSTTaxCode, False)
            End If
            Dim dbIncTotalTax As Double = 0
            If dtTaxCalc.Rows.Count = 0 Then
                Return Nothing
            End If
            If dtTaxCalc.Rows.Count <> 0 Then
                If IsCSTApplicable Then
                    Dim amt As Double = GetTaxableAmountForCst(strMatcode, EAN, quantity, TaxableAmount)
                    originalTaxAmt = amt
                    dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = Math.Round(TaxableAmount - amt, clsDefaultConfiguration.DecimalPlaces)
                    objCM.getCalculatedDataSet(dtTaxCalc)
                Else
                    dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
                    objCM.getCalculatedDataSet(dtTaxCalc)
                    originalTaxAmt = dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")
                End If
                'dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
                'objCM.getCalculatedDataSet(dtTaxCalc, quantity, isInclusiveTax)
                Dim iRowTax As Int16
                With dtTaxCalc
                    For iRowTax = 0 To dtTaxCalc.Rows.Count - 1
                        dtTaxCalc.Rows(iRowTax)("BillLineNo") = LineNo
                        If Val(dtTaxCalc.Rows(iRowTax)("VALUE").ToString) <> 0 Then
                            dbIncTotalTax = dbIncTotalTax + Val(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT"))
                        End If
                    Next
                End With
                dtMainTax.Merge(dtTaxCalc, False, MissingSchemaAction.Ignore)
                dtMainTax.AcceptChanges()
                Return dbIncTotalTax
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM012"), "CM012 - " & getValueByKey("CLAE05"))
            LogException(ex)
            Return 0
        End Try
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
                dsMain.Tables("CASHMEMOHDR").Rows(0)("DiscountAmt") = Format(Val(dsMain.Tables("CASHMEMODtl").Compute("SUM(LINEDISCOUNT)", "").ToString()), "0.00")
                dsMain.Tables("CASHMEMOHDR").Rows(0)("TotalDiscount") = Format(Val(dsMain.Tables("CASHMEMODtl").Compute("SUM(TOTALDISCOUNT)", "").ToString()), "0.00")
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
                    If Not IsHOInstance Then
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
                        CMbtnBottom.CtrlBtnHomeDelivery.Enabled = Value
                        rbtnRoundOff.Enabled = Value
                    End If
                ElseIf Steps = 2 Then
                    If Not IsHOInstance Then
                        If IsTenderCreditCard Then cmdCard.Enabled = Value
                        If IsTenderCash Then cmdCash.Enabled = Value
                        If IsTenderCheque Then cmdCheque.Enabled = Value
                        If IsTenderCredit Then cmdCreditSale.Enabled = Value
                    End If
                ElseIf Steps = 3 Then
                    C1Ribbon1.DbtnSwitchUser.Enabled = Value
                    If Not IsHOInstance Then
                        cmdDelete.Enabled = Value
                        cmdReprint.Enabled = Value
                    End If
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
    Public Sub New()
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
                        ' ShowMessage(getValueByKey("CLIST03"), getValueByKey("CLIST03"))
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
            Me.FrmTranCode = "FastCashMemo"
            If CheckAuthorisation(clsAdmin.UserCode, "FastCashMemo") = False Then
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

            If (Screen.PrimaryScreen.Bounds.Width <= 1030) Then
                CtrlMODMenu1.Padding = New System.Windows.Forms.Padding(0, 0, 165, 0)
            Else
                CtrlMODMenu1.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
            End If

            CashSummary.CtrllblGrossAmt.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            CashSummary.CtrllblDiscAmt.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            CashSummary.CtrllblNetAmt.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

            ' Add any initialization after the InitializeComponent() call.
            'SetCulture(Me)    
            C1Ribbon1.ApplicationMenu.Visible = False
            C1Ribbon1.Qat.Visible = False

            'Dim highlightRowStyle As New GridRendererOffice2007Blue
            'highlightRowStyle.Highlight = dgMainGrid.Styles.Highlight.BackColor
            'dgMainGrid.Renderer = highlightRowStyle
            dgMainGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitButton.Click
        Try
            Dim eventType As Int32
            If UpdateFlag = False And dgMainGrid.Rows.Count - 1 > 0 Then
                ShowMessage(getValueByKey("CM014"), "CM014 - " & getValueByKey("CLAE04"), eventType, "Exit", "No")
                'ShowMessage("You have a bill on the Screen Hold first.", "Information", eventType)
                If eventType = 1 Then
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
                ElseIf eventType = 2 Then
                    exitNowFlag = True
                    comm.SaveKeylog()
                    If CtrlSalesPersons.AndroidSearchTextBox.IsListBoxVisible Then
                        CtrlSalesPersons.AndroidSearchTextBox.ResetListBox()
                    End If
                    Me.Close()
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
                comm.SaveKeylog()
                If CtrlSalesPersons.AndroidSearchTextBox.IsListBoxVisible Then
                    CtrlSalesPersons.AndroidSearchTextBox.ResetListBox()
                End If
                Me.Close()
            End If
            If clsDefaultConfiguration.IsPoleDisply Then
                Dim md As MDISpectrum
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

        If clsDefaultConfiguration.SalesPersonApplicable = True Then
            CtrlSalesPersons.CtrlSalesPersons.Focus()
            CtrlSalesPersons.CtrlSalesPersons.Select()
        Else
            'CtrlSalesPersons.CtrlTxtBox.Focus()
            'CtrlSalesPersons.CtrlTxtBox.Select()
            If clsDefaultConfiguration.IsMembership Then
                CustInfo.CtrlTxtSwape.Focus()
                CustInfo.CtrlTxtSwape.Select()
            Else
                CtrlSalesPersons.AndroidSearchTextBox.Select()
                CtrlSalesPersons.AndroidSearchTextBox.Focus()
            End If
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
                objClsCommon.DisplayHelpFile(ParentForm, "touch-cash-memo.htm")
            End If
            'If e.KeyCode = Keys.F1 Then
            '    cmdSearch_Click(sender, New System.EventArgs)
            If e.KeyCode = Keys.F2 Then
                If clsDefaultConfiguration.DineInProcess = True AndAlso mergeIdForGenerateBill > 0 Then
                    e.SuppressKeyPress = True
                Else
                    ChangeQty()
                    e.Handled = True
                End If
            ElseIf e.KeyCode = Keys.F4 And cmdPayments.Enabled Then
                '' added by nikhil
                If dgMainGrid.Selection.ContainsCol(dgMainGrid.Cols("Quantity").Index) Then
                    CtrlSalesPersons.CtrlTxtBox.Focus()
                End If
                cmdPayments_Click(cmdPayments, New System.EventArgs)

            ElseIf e.KeyCode = Keys.F5 And cmdCash.Enabled And IsTenderCash Then
                '' added by nikhil
                If dgMainGrid.Selection.ContainsCol(dgMainGrid.Cols("Quantity").Index) Then
                    CtrlSalesPersons.CtrlTxtBox.Focus()
                End If
                cmdCash_Click(cmdCash, New System.EventArgs)

            ElseIf e.KeyCode = Keys.F6 And cmdCard.Enabled And IsTenderCreditCard Then
                '' added by nikhil
                If dgMainGrid.Selection.ContainsCol(dgMainGrid.Cols("Quantity").Index) Then
                    CtrlSalesPersons.CtrlTxtBox.Focus()
                End If
                cmdCard_Click(cmdCard, New System.EventArgs)

            ElseIf e.KeyCode = Keys.F8 AndAlso IsTenderCredit AndAlso cmdCreditSale.Enabled Then
                '' added by nikhil
                If dgMainGrid.Selection.ContainsCol(dgMainGrid.Cols("Quantity").Index) Then
                    CtrlSalesPersons.CtrlTxtBox.Focus()
                End If
                cmdCreditSale_Click(cmdCreditSale, New System.EventArgs)

            ElseIf e.Control And e.KeyCode = Keys.E Then
                If CheckInterTransactionAuth("EditBill", dsMain.Tables("CASHMEMOHDR")) = True Then
                    'objSearch.RequestFromPage = enumOperationOnBill.EditBill
                    cmdOldCashMemo_Click(cmdOldCashMemo, New EventArgs)
                End If
                'ElseIf e.KeyCode = Keys.F8 Then
                'cmdHomeDelivery_Click(sender, e)

                'ElseIf e.KeyCode = Keys.F9 Then
                '    If (sellGiftVoucher.Enabled) Then
                '        cmdAdvanceSale_Click(CMbtnBottom.CtrlBtnSaleGV, New EventArgs())
                '    End If

                'ElseIf e.KeyCode = Keys.F10 Then
                '    If (sellGiftVoucher.Enabled) Then
                '        cmdAdvanceSale_Click(CMbtnBottom.CtrlBtnSaleCLPPoint, New EventArgs())
                '    End If
                'If e.KeyCode = Keys.F11 Then
                '    cmdReturns_Click(sender, New System.EventArgs)
            ElseIf e.KeyCode = Keys.F9 Then
                If clsDefaultConfiguration.IsNewCreditSale Then 'vipin 21.05.2018 
                    Dim objCreditSales As New frmNCreditSalesNew(False)
                    objCreditSales.ShowDialog()
                Else
                    Dim objCreditSales As New frmNCreditSales(False)
                    objCreditSales.ShowDialog()
                End If
                'code added by vipul for issue id 2783
            ElseIf e.KeyCode = Keys.F7 And cmdCheque.Enabled Then
                cmdCheque_Click(cmdCheque, New System.EventArgs)
            ElseIf e.KeyCode = Keys.F12 Then
                If (clsDefaultConfiguration.PriceChageAllowed) Then
                    PriceChange()
                    e.Handled = True
                End If
            ElseIf e.Control And e.KeyCode = Keys.N Then
                If clsDefaultConfiguration.DineInProcess Then
                    cmdNewOrder_Click(cmdNewOrder, New System.EventArgs)
                Else
                    cmdNew_Click(cmdNew, New System.EventArgs)
                End If
                e.Handled = True

            ElseIf e.KeyCode = Keys.Escape Then
                cmdExit_Click(sender, e)

            ElseIf e.Control And e.KeyCode = Keys.D AndAlso cmdDefaultPromo.Enabled Then
                cmdDefaultPromo_Click(cmdDefaultPromo, New System.EventArgs)

            ElseIf e.Control And e.KeyCode = Keys.C AndAlso cmdClrAllPromo.Enabled Then
                cmdClrAllPromo_Click(cmdClrAllPromo, New System.EventArgs)

            ElseIf e.Control And e.KeyCode = Keys.Delete Then
                cmdDelete_Click(cmdDelete, New System.EventArgs)
                e.Handled = True
            ElseIf e.Control And e.KeyCode = Keys.R AndAlso cmdReprint.Enabled Then
                cmdReprint_Click(cmdReprint, New System.EventArgs)

            ElseIf e.Control And e.KeyCode = Keys.H Then
                cmdHold_Click(cmdHold, New System.EventArgs)

            ElseIf e.Control And e.KeyCode = Keys.P AndAlso cmdApplySelectPromo.Enabled Then
                cmdDefaultPromo_Click(cmdApplySelectPromo, New System.EventArgs)

            ElseIf e.Control And e.KeyCode = Keys.S AndAlso cmdCustomerinfo.Enabled Then

                cmdCustomerinfo_Click(cmdCustomerinfo, New System.EventArgs)
                CtrlSalesPersons.CtrlTxtBox.Focus()
                CtrlSalesPersons.CtrlTxtBox.Select()
                e.Handled = True
            ElseIf e.Alt And e.KeyCode = Keys.R Then
                Dim ChildForm As New Spectrum.frmArticlesRemark
                Try
                    Dim lastRowIndex As Integer = dgMainGrid.Row
                    If lastRowIndex = -1 Then Exit Sub
                    Dim articlecode = dgMainGrid.Rows(lastRowIndex)("ArticleCode")
                    Dim billlineno = dgMainGrid.Rows(dgMainGrid.Row)("BillLineNo")
                    Dim EAN = dgMainGrid.Rows(lastRowIndex)("EAN")
                    Using objArticleRemark As New frmArticlesRemark
                        If dsMain.Tables.Contains("CASHMEMODTLITEMREMARK") Then
                            Dim Dr() = dsMain.Tables("CASHMEMODTLITEMREMARK").Select("ArticleCode ='" & articlecode & "' and EAN='" & EAN & "'and BillLineNo='" & billlineno & "'")
                            If Dr.Count > 0 Then
                                objArticleRemark.ResultRemark = IIf(IsDBNull(Dr(0)("itemRemarks")), "", Dr(0)("itemRemarks"))
                            End If
                            If objArticleRemark.ShowDialog() = Windows.Forms.DialogResult.OK Then
                                If Dr.Count > 0 Then
                                    Dr(0)("itemRemarks") = objArticleRemark.ResultRemark
                                Else
                                    Dim remarkRow As DataRow = dsMain.Tables("CASHMEMODTLITEMREMARK").NewRow()
                                    remarkRow("siteCode") = clsAdmin.SiteCode
                                    remarkRow("EAN") = EAN
                                    remarkRow("ArticleCode") = articlecode
                                    remarkRow("itemRemarks") = objArticleRemark.ResultRemark
                                    remarkRow("BillLineNo") = billlineno
                                    dsMain.Tables("CASHMEMODTLITEMREMARK").Rows.Add(remarkRow)

                                End If
                            End If
                        End If
                    End Using
                Catch ex As Exception
                    LogException(ex)
                End Try
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub
    Private Function ScreenResolution() As String
        Dim intX As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim intY As Integer = Screen.PrimaryScreen.Bounds.Height
        Return intX & " X " & intY
    End Function
    Private Sub CheckAuthForVoidandReprint()
        Me.C1Ribbon1.DbtnF2.Visible = False
        Me.C1Ribbon1.DgrpExtraKey.Visible = False
        If CheckAuthorisation(clsAdmin.UserCode, "DeleteBill") Then
            cmdDelete.Visible = True
            cmdDelete.Text = getValueByKey("frmfastcashmemo.cmdDelete")
            rbnGrpHoldVoid.Text = getValueByKey("frmfastcashmemo.rbnGrpHoldVoid")
            rbnGrpHoldVoid.Visible = True
        Else
            cmdHold.Visible = False
            rbnGrpHoldVoid.Visible = False
        End If
        If CheckAuthorisation(clsAdmin.UserCode, "ReprintCM") Then
            cmdReprint.Visible = True
            cmdReprint.Text = getValueByKey("frmfastcashmemo.cmdReprint")
            rbCMNew.Text = getValueByKey("frmfastcashmemo.rbCMNew")
            rbCMNew.Visible = True
        Else
            cmdReprint.Visible = False
            rbCMNew.Visible = False
        End If
    End Sub

    Private Sub AssignRibbonButtonLabel()
        sellGiftVoucher.Text = getValueByKey("salegv")
        rbnTabCM.Text = getValueByKey("frmfastcashmemo.rbnTabCM")
        rbnGrpCMPromotion.Text = getValueByKey("frmfastcashmemo.rbnGrpCMPromotion")
        cmdDefaultPromo.Text = getValueByKey("frmfastcashmemo.cmdDefaultPromo")
        cmdApplySelectPromo.Text = getValueByKey("frmfastcashmemo.cmdApplySelectPromo")
        cmdClrAllPromo.Text = getValueByKey("frmfastcashmemo.cmdClrAllPromo")
        rbnGrpPayments.Text = getValueByKey("frmfastcashmemo.rbnGrpPayments")
        cmdPayments.Text = getValueByKey("frmfastcashmemo.cmdPayments")
        cmdCash.Text = getValueByKey("frmfastcashmemo.cmdCash")
        rbnGrpCustSearch.Text = getValueByKey("frmfastcashmemo.rbnGrpCustSearch")
        cmdCustomerinfo.Text = getValueByKey("frmfastcashmemo.cmdCustomerinfo")
        homeDelivery.Text = getValueByKey("homedelivery")
        ExitButton.Text = getValueByKey("dbtntoprightexit")
    End Sub

    Private Sub DisableButtonsForHoInstance()
        If IsHOInstance Then
            cmdDefaultPromo.Enabled = False
            cmdApplySelectPromo.Enabled = False
            cmdClrAllPromo.Enabled = False
            cmdPayments.Enabled = False
            cmdCash.Enabled = False
            cmdCreditSale.Enabled = False
            cmdCustomerinfo.Enabled = False
            homeDelivery.Enabled = False
            sellGiftVoucher.Enabled = False
            cmdDelete.Enabled = True
            cmdReprint.Enabled = True
            'Else            
            '    cmdDelete.Enabled = False
        End If
    End Sub
    Private Sub SetHeaderValues()
        lblUserIdValue.Text = clsAdmin.UserName
        lblCMDateValue.Text = clsAdmin.DayOpenDate.ToShortDateString()
        ' lblSiteCodeValue.Text = clsAdmin.SiteCode
        ' lblSiteNameValue.Text = objCM.GetSiteName(clsAdmin.SiteCode)
    End Sub
    Private Sub FirstBillTimer_Tick(ByVal sender As Object, e As EventArgs)
        FirstBillTimer.Stop()
        comm.FirstBillQueryExecutes(clsAdmin.SiteCode)
        FirstBillTimer.Start()
    End Sub
    Private Sub frmCashMemo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If clsDefaultConfiguration.IsPoleDisply Then
                Dim md As MDISpectrum
                md.PoleDisplayTimer.Stop()

            End If
            '' added by ketan Wild search changes
            Dim objDefault As New clsDefaultConfiguration("CMS")
            objDefault.GetDefaultSettings()
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType = False AndAlso clsDefaultConfiguration.EnablewildSearch = True Then
                ''Work Wild Seach funcatonality 
                CtrlSalesPersons.AndroidSearchTextBox.IsListBind = True
            Else
                ''Work Old Seach funcatonality 
                CtrlSalesPersons.AndroidSearchTextBox.IsListBind = False
            End If
            dtExtend = ObjclsCommon.GetDetailsExtendScreen()
            dtExtend.Rows.Clear()
            comm.FirstBillQueryExecutes(clsAdmin.SiteCode)
            FirstBillTimer.Start()
            AddHandler FirstBillTimer.Tick, AddressOf FirstBillTimer_Tick
            FirstBillTimer.Interval = Convert.ToInt32(1000 * 60 * FirstBillTimeInterval)
            ctrlBack.Visible = False
            SetHeaderValues()
            PrintSetProperty()
            CheckIfHoInstance()
            'Me.Dock = DockStyle.Fill
            'lblTerminalId.Text = "Terminal Id"
            'lblTerminalId.Text = getValueByKey("ctrlrbnbaseform.lblterminalid") & " " & clsAdmin.TerminalID
            CheckAuthForVoidandReprint()
            AssignRibbonButtonLabel()
            C1Ribbon1.ExitButton.Tag = "U"
            C1Ribbon1.ExitButton.Visible = False
            C1Ribbon1.DbtnTopRightExit.Visible = False
            C1Ribbon1.DbtnOpenDrawer.Visible = False
            C1Ribbon1.DbtnGlobalCashMemo.Visible = False
            'AddHandler C1Ribbon1.ExitButton.Click, AddressOf cmdExit_Click
            'AddHandler C1Ribbon1.DbtnTopRightExit.Click, AddressOf cmdExit_Click
            'adeded By ketan for wild search 
            AddHandler CtrlSalesPersons.CtrlTxtBox.TextChanged, AddressOf txtSearch_textchange
            AddHandler CtrlSalesPersons.AndroidSearchTextBox.TextChanged, AddressOf AndroidSearchTextBox_Textchange
            ' If CtrlSalesPersons.AndroidSearchTextBox.IsListBind = False Then
            AddHandler CtrlSalesPersons.AndroidSearchTextBox.KeyDown, AddressOf AndroidSearchTextBox_KeyDown
            'End If
            CtrlNumberPad1.NumberEntered = AddressOf QuantityChanged
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
            AddHandler CustInfo.CtrlTxtCustomerNo.KeyDown, AddressOf CtrlTxtCustomerNo_KeyDown

            Dim colno As Integer
            'Dim objDefault As New clsDefaultConfiguration("CMS")
            'objDefault.GetDefaultSettings()
            SetDefaultSetting()
            'CheckIfInclusiveTax()
            Call EnableDiableTenderIcons()
            If clsDefaultConfiguration.ThemeSelect <> "Theme 1" Then
                AddCashMemoryComponents()
            End If
            getBinding()
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
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "NetAmount".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "TOTALTAXAMOUNT".ToUpper() Then

                    HideColumns(dgMainGrid, False, dgMainGrid.Cols(colno).Name)

                End If
                If Not dgMainGrid.Cols(colno).DataType Is Nothing AndAlso dgMainGrid.Cols(colno).DataType.ToString() = "System.Decimal" Then
                    dgMainGrid.Cols(colno).DataType = Type.GetType("System.Double")
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
            If clsDefaultConfiguration.EvasPizzaChanges = True Then
                CustInfo.ctrlTxtAddress.ReadOnly = True
            End If
            'CustInfo.CtrlTxtSwape.ReadOnly = True
            CustInfo.TabStop = False
            PSetDefaultCurrencyOfCashMemoSummary(CashSummary)
            'SetButtonsCaption()
            SetCulture(Me, Me.Name, C1Ribbon1)

            cmdHold.Text = getValueByKey("frmcashmemo.cmdresume")
            cmdHold.Tag = "RESUME"
            'dgMainGrid.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

            cbManualDisc.Enabled = False
            cmdEnable.Tag = "E"
            Me.Focus()
            'CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Select()
            Dim IsMultiTabAllowed = objCM.IsMultiTabAllowed()
            Dim btnGroup As DataTable
            Dim dt As New DataTable
            If IsMultiTabAllowed Then
                btnGroup = objCM.GetMultiButtonGroup(clsAdmin.SiteCode)
                dtArticles = objCM.GetMultiButtonArticle(clsAdmin.SiteCode)
                CtrlMODMenu1.Parent = Me
                CtrlMODMenu1.ButtonGroup = btnGroup
                CtrlMODMenu1.ImageInfo = dtArticles
                CtrlMODMenu1.LoadMultiArticle()
            Else
                btnGroup = objCM.GetButtonGroup(clsAdmin.SiteCode)
                dt = objCM.GetButtonArticle(clsAdmin.SiteCode)
                CtrlMODMenu1.Parent = Me
                CtrlMODMenu1.ButtonGroup = btnGroup
                CtrlMODMenu1.ImageInfo = dt
                CtrlMODMenu1.LoadArticle()
            End If
            'If dt IsNot Nothing Then
            '    For Each drArticle As DataRow In dt.Rows
            '        For Each ctrl As Spectrum.CtrlBtn In CtrlCMbtnArticle.C1Sizer1.Controls
            '            If (ctrl.Name = drArticle("ButtonName")) Then
            '                ctrl.Text = drArticle("ArticleName")
            '                ctrl.SetArticleCode = drArticle("ArticleCode")
            '                'ctrl.Image = Image.FromFile(ObjclsCommon.GetArticleImage(drArticle("ArticleCode"), clsAdmin.Articleimagepath))
            '                ShowArticleImage(drArticle("ArticleCode"), ctrl)
            '            End If

            '        Next
            '    Next
            'End If
            'CtrlMODMenu1.Parent = Me
            'CtrlMODMenu1.ButtonGroup = btnGroup
            'CtrlMODMenu1.ImageInfo = dt
            'CtrlMODMenu1.LoadArticle()
            AddHandler CtrlMODMenu1.ArticleContextMenuStrip.ItemClicked, AddressOf ArticleContextMenuStrip_ItemClicked
            AddHandler CtrlMODMenu1.FlowPanelMenuStrip.ItemClicked, AddressOf FlowPanelMenuStrip_ItemClicked
            For Each dm As TableLayoutPanel In CtrlMODMenu1.flpMenuButton.Controls
                'For Each klPanel As TableLayoutPanel In dm.Controls
                For Each kl As Control In dm.Controls
                    If (TypeOf kl Is CtrlBtn) Then

                        AddHandler kl.Click, AddressOf ModMenuCtrlBtn1_Click
                    End If

                Next

                'Next
            Next
            objCM.DecimalDigits = clsDefaultConfiguration.DecimalPlaces
            DisableButtonsForHoInstance()
        Catch ex As Exception
            ShowMessage(getValueByKey("CM015"), getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Transaction Not properly Set", "Error")
        Finally

        End Try
        Me.MaximumSize = New System.Drawing.Size(0, 0)
        Me.Dock = DockStyle.Fill

        If (Not clsDefaultConfiguration.NumpadRequired) Then
            CtrlNumberPad1.Visible = False
            Me.TableLayoutPanel1.SetColumnSpan(c1SizerGrid, 2)
        End If

        CustInfo.CtrlTxtSwape.ReadOnly = False
        CustInfo.ctrlTxtPoints.ReadOnly = True
        CustInfo.CtrlTxtCustomerNo.ReadOnly = True

        AddHandler CustSaleTypeTimer.Tick, AddressOf timer_Tick
        CustSaleTypeTimer.Interval = CustSaleTypeTimerBlinkFrequency
        lblCustSaleType.Visible = False

        If clsDefaultConfiguration.SalesPersonApplicable = True Then
            CtrlSalesPersons.CtrlSalesPersons.Focus()
            CtrlSalesPersons.CtrlSalesPersons.Select()
        Else
            'CtrlSalesPersons.CtrlTxtBox.Focus()
            'CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Focus()
            CtrlSalesPersons.AndroidSearchTextBox.Select()
        End If
        '---------- Added By Mahesh For Maintain Tab Sequence 
        Call SetTabSequence()
        '----------PcRoundoff
        rbtnRoundOff.Visible = clsDefaultConfiguration.PCRoundOff
        cmdGenerateBill.Enabled = False
        cmdGenerateKOT.Enabled = False
        objCM.SalesType = ""
        If clsDefaultConfiguration.DineInProcess = True Then
            RibbonTab1.Visible = True
            CMbtnBottom.CtrlBtnHomeDelivery.Visible = False
            homeDelivery.Enabled = False
            homeDeliveryGroup.Visible = False
            CtrlSalesPersons.CtrllabelSalesPerson.Text = "     Waiter "
            cmd_Remark.Enabled = False
            cmd_Track.Enabled = False
            cmd_Reprints.Enabled = False
            cmd_VoidKot.Enabled = False
        Else
            lblTable.Visible = False
            lblDineInTableNo.Visible = False
            RibbonTab1.Visible = False
            CMbtnBottom.CtrlBtnHomeDelivery.Visible = True
        End If
        If clsDefaultConfiguration.CUSTOMERSEARCHUSINGPHONENUMBER Then
            CustInfo.CtrlLabel2.Text = "Phone No."
        End If
        Dim condition As String
        Dim objItem As New clsIteamSearch
        condition = " AND A.ArticleCode <>'" & clsDefaultConfiguration.GVBaseArticle & "' AND A.ArticleCode <>'" & clsDefaultConfiguration.ClpBaseArticle & "' AND A.ArticleCode <>'" & clsAdmin.CVBaseArticle & "'"
        Dim dtBind = objItem.GetEANData(clsAdmin.SiteCode, "", clsAdmin.LangCode, condition, dtItemScanData)
        If dtBind.Rows.Count > 1 Then
            'Dim listSource As List(Of [String]) = (From row In dtBind Select Convert.ToString(row("ArticleCode")) + " " + Convert.ToString(row("Discription"))).Distinct().ToList()
            'CtrlSalesPersons.AndroidSearchTextBox.lstNames = listSource
            Call SetWildSearchTextBox(dtBind, CtrlSalesPersons.AndroidSearchTextBox, key:="ArticleCode", Value:="Discription", searchData:="ArticleCodeDesc")
        End If
        If clsDefaultConfiguration.IsMembership Then
            rbnGrpCMPromotion.Visible = False

        End If

        If clsDefaultConfiguration.ExtendScreen Then
            If clsDefaultConfiguration.HariOmExtendScreen = True Then
                TwoLineExtendScreen()
            Else
                Dim myScreen As System.Windows.Forms.Screen
                myScreen = Screen.FromControl(Me)
                'Form2 ff = new Form2();
                If Screen.AllScreens.Length > 1 Then
                    Dim otherScreen = Screen.AllScreens(1)
                    ' ff.StartPosition = FormStartPosition.Manual;
                    'objextend.Left = otherScreen.WorkingArea.Left + 200
                    'objextend.Top = otherScreen.WorkingArea.Top + 120
                    objextend.Left = otherScreen.WorkingArea.Left + 1
                    objextend.Top = otherScreen.WorkingArea.Top + 1
                    objextend.Size = New System.Drawing.Size((My.Computer.Screen.WorkingArea.Width + otherScreen.WorkingArea.Width - 10), (My.Computer.Screen.WorkingArea.Height + otherScreen.WorkingArea.Height - 50))
                End If
                objextend.Show()
            End If
        End If
        If clsDefaultConfiguration.EnableScanQRCode = True Then 'vipin 23042018 Mahavir changes
            QRTrailerSeperator = clsDefaultConfiguration.QRCodeTrailer
            Dim QrSplitString() As String = Split(QRTrailerSeperator, ",")
            QRTrailerSegment = QrSplitString(1)
            QRSepearatorSegment = QrSplitString(0)
        End If
        If clsDefaultConfiguration.IsPoleDisply Then
            TwoLineExtendScreen()
        End If
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            ThemeChange()
        End If
    End Sub

    ''' <summary>
    ''' extend screen for hariom
    ''' </summary>
    ''' <param name="line1String"></param>
    ''' <param name="line2String"></param>
    ''' <param name="portname"></param>
    ''' <remarks></remarks>
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

    Private Sub SetTabSequence()
        Try
            '---- Set Tab Index START
            SetFormTabStop(Me, tabStopValue:=False)

            Dim ctrTablIndex As New Dictionary(Of Object, Int16)

            ctrTablIndex.Add(Me, 0)
            ctrTablIndex.Add(Me.TableLayoutPanel1, 0)

            '----- First Parent 
            ctrTablIndex.Add(Me.c1SizerGrid, 0)
            '----- First Parent First CHILD 
            ctrTablIndex.Add(Me.CtrlSalesPersons, 0)
            ctrTablIndex.Add(Me.CtrlSalesPersons.CtrlSalesPersons, 1)
            ctrTablIndex.Add(Me.CtrlSalesPersons.CtrlTxtBox, 2)
            ctrTablIndex.Add(Me.CtrlSalesPersons.CtrlCmdSearch, 3)
            '----- First Parent SECOND CHILD 
            ctrTablIndex.Add(Me.dgMainGrid, 1)
            '----- SECOND Parent 
            'ctrTablIndex.Add(Me.Panel2, 3)
            'ctrTablIndex.Add(Me.C1Ribbon1, 0)
            'ctrTablIndex.Add(Me.rbnTabCM, 0)
            'ctrTablIndex.Add(Me.rbCMNew, 0)
            'ctrTablIndex.Add(Me.cmdReprint, 0)

            'ctrTablIndex.Add(Me.rbnGrpCMPromotion, 1)
            'ctrTablIndex.Add(Me.cmdDefaultPromo, 0)
            'ctrTablIndex.Add(Me.cmdApplySelectPromo, 1)
            'ctrTablIndex.Add(Me.cmdClrAllPromo, 2)

            'ctrTablIndex.Add(Me.rbnGrpPayments, 2)
            'ctrTablIndex.Add(Me.cmdPayments, 0)
            'ctrTablIndex.Add(Me.cmdCash, 1)
            'ctrTablIndex.Add(Me.cmdCard, 2)
            'ctrTablIndex.Add(Me.cmdCreditSale, 3)

            'ctrTablIndex.Add(Me.rbnGrpCustSearch, 3)
            'ctrTablIndex.Add(Me.cmdCustomerinfo, 0)

            'ctrTablIndex.Add(Me.extraGroup, 4)
            'ctrTablIndex.Add(Me.sellGiftVoucher, 0)

            'ctrTablIndex.Add(Me.homeDeliveryGroup, 5)
            'ctrTablIndex.Add(Me.homeDelivery, 0)
            '----- Third Parent 
            ctrTablIndex.Add(Me.Panel1, 3)
            ctrTablIndex.Add(Me.c1SizerSummary, 0)
            ctrTablIndex.Add(Me.CustInfo, 0)
            ctrTablIndex.Add(Me.CustInfo.BtnClearCustmInfo, 0)


            SetFormTabIndex(ctrTablIndex:=ctrTablIndex)
            Me.dgMainGrid.KeyActionTab = KeyActionEnum.None
            '---- Set Tab Index END 
            'CtrlSalesPersons.C1Sizer1.TabStop = False '' change by ketan For wild search  
            Me.c1SizerSummary.TabStop = False
            Me.c1SizerGrid.TabStop = False

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub


    Private Sub timer_Tick(ByVal sender As Object, e As EventArgs)
        lblCustSaleType.Visible = Not lblCustSaleType.Visible
    End Sub

    Private Sub AddCashMemoryComponents()
        'Me.CashSummary.Font = New System.Drawing.Font("Tahoma", 14.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
        'Me.CashSummary.CtrlHeader1.Font = New System.Drawing.Font("Tahoma", 14.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
        'Me.CashSummary.C1Sizer1.Grid.Clear()
        'Me.CashSummary.RowCount = 5
        'Me.CashSummary.C1Sizer1.Grid.Columns.Count = 2
        Me.CashSummary.C1Sizer1.Controls.Clear()
        Me.CashSummary.C1Sizer1.Grid.Rows(0).Size = 22
        Me.CashSummary.C1Sizer1.Grid.Rows(1).Size = 22
        'If IsInclusiveTax Then
        Me.CashSummary.C1Sizer1.Grid.Rows(2).Size = 22
        'Else
        '    Me.CashSummary.C1Sizer1.Grid.Rows(2).Size = 0
        'End If
        Me.CashSummary.C1Sizer1.Grid.Rows(3).Size = 22
        Me.CashSummary.C1Sizer1.Grid.Rows(4).Size = 22

        Me.CashSummary.C1Sizer1.AddControl(Me.CashSummary.CtrlHeader1, 0, 0, 1, 2)
        Me.CashSummary.C1Sizer1.AddControl(Me.CashSummary.CtrlLabel1, 1, 0, 1, 1)
        Me.CashSummary.C1Sizer1.AddControl(Me.CashSummary.CtrllblGrossAmt, 1, 1, 1, 1)
        'If IsInclusiveTax Then
        Me.CashSummary.C1Sizer1.AddControl(Me.CashSummary.CtrlLabel2, 2, 0, 1, 1)
        Me.CashSummary.C1Sizer1.AddControl(Me.CashSummary.CtrllblTaxAmt, 2, 1, 1, 1)
        'End If
        Me.CashSummary.C1Sizer1.AddControl(Me.CashSummary.CtrlLabel3, 3, 0, 1, 1)
        Me.CashSummary.C1Sizer1.AddControl(Me.CashSummary.CtrllblDiscAmt, 3, 1, 1, 1)
        Me.CashSummary.C1Sizer1.AddControl(Me.CashSummary.CtrlLabel4, 4, 0, 1, 1)
        Me.CashSummary.C1Sizer1.AddControl(Me.CashSummary.CtrllblNetAmt, 4, 1, 1, 1)
    End Sub

    Private Sub ScanItemBillWise(ByRef rowindex As Integer)
        Try
            If (IsNumeric(ScaleBillNo)) Then

                Dim ScaleNo As Integer = Val(ScaleBillNo.Substring(0, 2))
                Dim BillNo As Integer = Val(ScaleBillNo.Substring(2, 4))

                dtScanedBillArticle = objItemSch.GetScanedBillArticle(ScaleNo:=ScaleNo, BillNo:=BillNo, BillIntDate:=ScaleBillIntDate, MettlerConn:=clsDefaultConfiguration.MettlerConnString)
                If dtScanedBillArticle IsNot Nothing AndAlso dtScanedBillArticle.Rows.Count > 0 Then
                    ScanBill = True
                    dgMainGrid.Cols("Quantity").AllowEditing = True
                    Dim e As New System.Windows.Forms.KeyEventArgs(Keys.Enter)
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
                                If IsTenderCredit = True Then
                                    cmdCreditSale_Click(cmdCreditSale, New System.EventArgs)
                                Else
                                    ShowMessage("Credit facility is not available for this store", "Information")
                                    Exit Sub
                                End If
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
    Private Sub AndroidSearchTextBox_Textchange(sender As Object, e As EventArgs)
        If CtrlSalesPersons.AndroidSearchTextBox.IsItemSelected Then
            CtrlSalesPersons.CtrlTxtBox.Text = CtrlSalesPersons.AndroidSearchTextBox.Text
        End If
        If clsDefaultConfiguration.EnableScanQRCode = True Then
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
    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            
            Cursor.Current = Cursors.WaitCursor
            'Dim price As Double
            'Dim strArticle As String = ""
            'Dim Ean As String = ""
            'Dim tax As Object
            Dim openMrp As Boolean = False
            WeghingScaleBarcode = False
            Weight = ""
            SP = ""
            CtrlSalesPersons.CtrlTxtBox.Text = CtrlSalesPersons.CtrlTxtBox.Text.ToString().Split(" ")(0)
            Dim membershipmaparticle = CtrlSalesPersons.CtrlTxtBox.Text
            Dim objItemSch As New clsIteamSearch
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
                    If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
                        If dgMainGrid.Rows.Count > 1 Then
                            RecalculateTax = True
                            BillRecalculateTax()
                        End If
                    End If
                    '    End If
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
                        HideColumns(dgMainGrid, False, "TAKEAWAYQUANTITY")
                        lblCustSaleType.Text = "Home Delivery"
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
                    ' cmdCustomerSearchAndLoad("TAKEAWAY")
                    '  If (Not String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then
                    If CustomerSaleType = enumCustomerSaleType.Dine_In Then
                        For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Rows
                            dr("TakeAwayQuantity") = 0
                        Next
                    End If
                    CustomerSaleType = enumCustomerSaleType.Take_Away
                    Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
                    dsMain.Tables("CashMemoHdr").Rows(0)("ServiceTaxAmount") = CustomerSaleType
                    IsHoldEnterKey = False
                    lblCustSaleType.Text = "Take Away"
                    lblCustSaleType.Visible = True
                    HideColumns(dgMainGrid, False, "TAKEAWAYQUANTITY")
                    CustSaleTypeTimer.Start()
                    If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
                        If dgMainGrid.Rows.Count > 1 Then
                            RecalculateTax = True
                            BillRecalculateTax()
                        End If
                    End If
                    '  End If
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
                                If IsTenderCredit Then
                                    cmdCreditSale_Click(cmdCreditSale, New System.EventArgs)
                                Else
                                    'ShowMessage(getValueByKey("Credit facility is not available for this store", "CM059 - " & getValueByKey("CLAE05"))
                                    ShowMessage("Credit facility is not available for this store", "Information")
                                    Exit Sub
                                End If
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

                If Not dtMainTax Is Nothing AndAlso dtMainTax.Rows.Count > 0 Then
                    Dim taxFilter As String = String.Format("BillLineNo={0} AND ArticleCode='{1}' AND EAN='{2}'",
               dgMainGrid.Rows(dgMainGrid.Row)("BillLineNo"), dgMainGrid.Rows(dgMainGrid.Row)("ArticleCode"), dgMainGrid.Rows(dgMainGrid.Row)("EAN"))
                    Dim drTax = dtMainTax.Select(taxFilter)

                    If (drTax.Length > 0) Then
                        For Each dr As DataRow In drTax
                            dr.Delete()
                        Next
                        dtMainTax.AcceptChanges()
                    End If
                End If
                'code added by vipul for issue id 3053
                If Not dtGV Is Nothing Then
                    If dgMainGrid.Rows(dgMainGrid.Row)("Section") Is DBNull.Value Then
                    Else
                        Dim dv As New DataView(dtGV, "IssuedDocNumber=" & dgMainGrid.Rows(dgMainGrid.Row)("Section").ToString(), "", DataViewRowState.CurrentRows)
                        If dv.Count > 0 Then
                            dv.AllowDelete = True
                            For Each dr As DataRowView In dv
                                dr.Delete()
                            Next
                        End If
                        dtGV.AcceptChanges()
                    End If
                End If
                Dim number As Integer = dgMainGrid.Rows(dgMainGrid.Row)("BillLineNo")
                If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count > 0 Then
                    For i = dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count - 1 To 0 Step -1
                        If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).RowState = DataRowState.Deleted Then
                            dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                        End If
                    Next

                    For i = dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count - 1 To 0 Step -1
                        If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i)("BillLineNo") = ((dgMainGrid.Rows.Count - 1) - (dgMainGrid.Row - 1)) Then
                            dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).Delete()
                            'If Not dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i) Is Nothing Then
                            '    dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                            'End If
                        End If
                    Next

                    For i = dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count - 1 To 0 Step -1
                        If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).RowState = DataRowState.Deleted Then
                            dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                        End If
                    Next

                End If
                If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count > 0 Then
                    For Each row In dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows
                        If row("BillLineNo") > number Then
                            row("BillLineNo") = row("BillLineNo") - 1
                        End If
                    Next
                End If
                If comboArticleCopy.Rows.Count > 0 Then
                    For i = comboArticleCopy.Rows.Count - 1 To 0 Step -1
                        If comboArticleCopy.Rows(i)("BillLineNo") = ((dgMainGrid.Rows.Count - 1) - (dgMainGrid.Row - 1)) Then
                            comboArticleCopy.Rows(i).Delete()
                        End If
                    Next
                End If
                If comboArticleCopy.Rows.Count > 0 Then
                    For Each row In comboArticleCopy.Rows
                        If row("BillLineNo") > number Then
                            row("BillLineNo") = row("BillLineNo") - 1
                        End If
                    Next
                End If

                'remark
                For i = dsMain.Tables("CashMemoDtlItemRemark").Rows.Count - 1 To 0 Step -1
                    If dsMain.Tables("CashMemoDtlItemRemark").Rows(i)("BillLineNo") = ((dgMainGrid.Rows.Count - 1) - (dgMainGrid.Row - 1)) Then
                        dsMain.Tables("CashMemoDtlItemRemark").Rows(i).Delete()
                        'If Not dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i) Is Nothing Then
                        '    dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                        'End If
                    End If
                Next

                If dsMain.Tables("CashMemoDtlItemRemark").Rows.Count > 0 Then
                    For Each row In dsMain.Tables("CashMemoDtlItemRemark").Rows
                        If row("BillLineNo") > number Then
                            row("BillLineNo") = row("BillLineNo") - 1
                        End If
                    Next
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
                If clsDefaultConfiguration.IsMembership Then
                    CustInfo.CtrlTxtSwape.Focus()
                    CustInfo.CtrlTxtSwape.Select()
                Else
                    CtrlSalesPersons.AndroidSearchTextBox.Select()
                    CtrlSalesPersons.AndroidSearchTextBox.Focus()
                End If
                Exit Sub
            End If

            If e.KeyCode = Keys.Enter AndAlso CtrlSalesPersons.CtrlTxtBox.Text <> String.Empty Then
                IsHoldEnterKey = False
                If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType AndAlso CustomerSaleType = 0 Then Exit Sub

                '--- Added By Mahesh for Prashant Corner Bill Addition  6 char is combination for Start
                If clsDefaultConfiguration.IsBillScanApplicable AndAlso CtrlSalesPersons.CtrlTxtBox.Text.ToString().Length = 6 And clsDefaultConfiguration.EnableScanQRCode = False Then
                    If ScanBill = False Then
                        ScaleBillNo = CtrlSalesPersons.CtrlTxtBox.Text.ToString()
                        '---- Check Whether Bill Exist In database 
                        ScaleBillIntDate = Val(clsAdmin.DayOpenDate.Year.ToString.Substring(2, 2) & clsAdmin.DayOpenDate.Month.ToString.PadLeft(2, "0") & clsAdmin.DayOpenDate.Day.ToString.PadLeft(2, "0"))
                        '--------- Check Whether Bill Exist In database 
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

                '--- Prashant Corner Bill Addition  6 char is combination for END

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
                        Dim EAN As String = CtrlSalesPersons.CtrlTxtBox.Text
                        CtrlSalesPersons.CtrlTxtBox.Text = CInt(EAN)
                    End If
                End If

                Dim dt As New DataTable
                Dim cdr As New DataTable
                cdr = objCM.GetComponentArticleItems(CtrlSalesPersons.CtrlTxtBox.Text.Trim)

                If cdr IsNot Nothing AndAlso cdr.Rows.Count > 0 Then
                    flag = 0
                    For index = 0 To cdr.Rows.Count - 1

                        dt = objCM.GetItemDetails(clsAdmin.SiteCode, cdr(index)("ArticleCode"), openMrp, clsAdmin.LangCode)
                        dt(0)("Quantity") = cdr(index)("Quantity")
                        compqty = cdr(index)("Quantity")
                        flag = 3
                        setgridview(dt)
                        dt.Clear()

                    Next
                Else
                    flag = 0
                    ' dt = objCM.GetItemDetails(clsAdmin.SiteCode, CtrlSalesPersons.CtrlTxtBox.Text.Trim, openMrp, clsAdmin.LangCode)
QRCodeScan:         If clsDefaultConfiguration.EnableScanQRCode = True Then 'vipinQR code scan changes 24042018 
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
                            HandleComboArticles(dt)
                        Next

                    Else
                        GoTo NormalScanItem
                    End If
                    ' dt = objCM.GetItemDetails(clsAdmin.SiteCode, CtrlSalesPersons.CtrlTxtBox.Text.Trim, openMrp, clsAdmin.LangCode)
                    If clsDefaultConfiguration.EnableScanQRCode = False Then
NormalScanItem:         If clsDefaultConfiguration.customerwisepricemanagement = True Then
                            dt = objCM.GetItemDetailsCustomerWise(clsAdmin.SiteCode, CtrlSalesPersons.CtrlTxtBox.Text.Trim, openMrp, CustInfo.CtrlTxtCustomerNo.Text.Trim, clsAdmin.LangCode, If(WeghingScaleBarcode, False, clsDefaultConfiguration.IsBatchManagementReq))
                            If dt.Rows.Count = 0 Then
                                dt = objCM.GetItemDetails(clsAdmin.SiteCode, CtrlSalesPersons.CtrlTxtBox.Text.Trim, openMrp, clsAdmin.LangCode, If(WeghingScaleBarcode, False, clsDefaultConfiguration.IsBatchManagementReq))
                            End If
                        Else
                            dt = objCM.GetItemDetails(clsAdmin.SiteCode, CtrlSalesPersons.CtrlTxtBox.Text.Trim, openMrp, clsAdmin.LangCode, If(WeghingScaleBarcode, False, clsDefaultConfiguration.IsBatchManagementReq))

                        End If
                    End If

                    If Not dt Is Nothing And dt.Rows.Count = 0 Then
                        If WeghingScaleBarcode Then
                            MessageBox.Show(getValueByKey("weighingscale001"))
                            CtrlSalesPersons.CtrlTxtBox.Text = ""
                            CtrlSalesPersons.AndroidSearchTextBox.Text = ""
                            'CtrlSalesPersons.CtrlTxtBox.Focus()
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
                    Else
                        'code added by vipul for issue id 2736
                        If Not dt Is Nothing And dt.Rows.Count = 1 Then
                            If CheckKitArticleCodeInMstArticleKit(dt, CtrlSalesPersons.CtrlTxtBox.Text.Trim) = False Then
                                ShowMessage("Article not present in the Kit.", getValueByKey("CLAE04"))
                                CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                                CtrlSalesPersons.AndroidSearchTextBox.Text = String.Empty
                                CtrlSalesPersons.AndroidSearchTextBox.Focus()
                                Exit Sub
                            End If
                        End If
                       
                    End If

                    HandleComboArticles(dt)
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

            If clsDefaultConfiguration.ExtendScreen Then

                If clsDefaultConfiguration.HariOmExtendScreen = True Then
                    TwoLineExtendScreen()
                Else
                    ExtendFun()
                End If
            End If
            If clsDefaultConfiguration.IsPoleDisply = True Then
                Dim selectedrow As Integer = dgMainGrid.RowSel - 1
                TwoLineExtendScreen(selectedrow)
            End If


            CtrlSalesPersons.AndroidSearchTextBox.Text = String.Empty
            CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
            If clsDefaultConfiguration.IsMembership Then
                CustInfo.CtrlTxtSwape.Focus()
            Else
                CtrlSalesPersons.AndroidSearchTextBox.Focus()
            End If



        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            'MsgBox(ex.InnerException.Message)
            'MsgBox(ex.Message)
            LogException(ex)
            'ShowMessage("Error in Searcing of Item Details", "Error")
        Finally
            Cursor.Current = Cursors.Default
        End Try

    End Sub

    Private Sub setgridview(ByVal dt As DataTable, Optional ByVal isCombo As Boolean = False)
        Try
            If clsDefaultConfiguration.PrintItemFullName = True Then
                Dim objClsCommon As New clsCommon
                If dt.Rows.Count > 0 Then
                    Dim artilceDescription = objClsCommon.GetArticleFullName(dt.Rows(0)("ArticleCode").ToString())
                    If Not isCombo Then
                        dt(0)("DISCRIPTION") = artilceDescription
                    End If
                End If
            End If

            Cursor.Current = Cursors.WaitCursor
            Dim price As Double
            Dim strArticle As String = ""
            Dim Ean As String = ""
            Dim tax As Object
            Dim openMrp As Boolean = False
            newval = 0
            flag = 0

            If Not dt Is Nothing And dt.Rows.Count = 0 Then
                ShowMessage(getValueByKey("CM016"), "CM016 - " & getValueByKey("CLAE04"))
                'ShowMessage("No Such Item Found or Item is not Saleable.....", "Information")
                CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                CtrlSalesPersons.CtrlTxtBox.Focus()

                Exit Sub
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

            Ean = dt.Rows(0)("EAN").ToString()
            strArticle = dt.Rows(0)("ArticleCode").ToString()
            'Changed by Sameer for accepting decimal qty & calculating on tolerance true

            Dim tolerancevalue As Boolean
            tolerancevalue = Convert.ToBoolean(dt.Rows(0)("tolerancevalue"))

            If tolerancevalue Then
                If (String.IsNullOrEmpty(Weight)) Then
                    Dim frmprompt As New frmSpecialPrompt("Enter Quantity")
                    frmprompt.ShowTextBox = True
                    frmprompt.txtValue.NumericInput = True
                    frmprompt.AllowDecimal = True
                    frmprompt.IsNumeric = True
                    frmprompt.AcceptButton = frmprompt.cmdOk
                    frmprompt.ShowTextBox = True
                    frmprompt.ReadWeightFromCOM = clsDefaultConfiguration.WeightScaleEnabled
                    frmprompt.COMPortName = clsDefaultConfiguration.Comport
                    frmprompt.ShowDialog()
                    newval = 0

                    If (frmprompt.GetResult() Is Nothing) Then
                        Exit Sub
                    End If
                    newval = frmprompt.GetResult()

                Else
                    '' Change By Ketan In Case Of NOS And PIS Not Need To split Article Quantity
                    Dim result As String
                    Dim strUOM As String = dt.Rows(0)("UNITOFMEASURE").ToString()
                    If strUOM = "NOS" Then
                        result = Weight
                    Else
                        result = String.Format("{0}.{1}", Weight.Substring(0, clsDefaultConfiguration.WeightBarcodeWholeNOLength), Weight.Substring(clsDefaultConfiguration.WeightBarcodeWholeNOLength, clsDefaultConfiguration.WeightBarcodedecimalLength))
                    End If
                    ' Dim result As String = String.Format("{0}.{1}", Weight.Substring(0, clsDefaultConfiguration.WeightBarcodeWholeNOLength), Weight.Substring(clsDefaultConfiguration.WeightBarcodeWholeNOLength, clsDefaultConfiguration.WeightBarcodedecimalLength))
                    newval = Decimal.Parse(result)
                    If Not newval > 0 Then
                        ShowMessage(getValueByKey("CM021"), "CM021 - " & getValueByKey("CLAE04"))
                        CtrlSalesPersons.CtrlTxtBox.Text = ""
                        CtrlSalesPersons.CtrlTxtBox.Focus()
                        Exit Sub
                    End If
                End If
            Else
                If WeghingScaleBarcode Then
                    MessageBox.Show(getValueByKey("weighingscale001"))
                    CtrlSalesPersons.CtrlTxtBox.Text = ""
                    CtrlSalesPersons.CtrlTxtBox.Focus()
                    Exit Sub
                End If
            End If

            If newval > 0 Then
                dt.Rows(0)("Quantity") = newval
                flag = 1
            End If

            If clsDefaultConfiguration.WeightBarcodeRateApplicable Then
                If (Not String.IsNullOrEmpty(SP)) Then
                    Dim newsp As String = String.Format("{0}.{1}", SP.Substring(0, clsDefaultConfiguration.WeightBarcodeRateWholeNOLength), SP.Substring(clsDefaultConfiguration.WeightBarcodeRateWholeNOLength, clsDefaultConfiguration.WeightBarcodeRateDecimalLength))
                    Dim newPrice = Decimal.Parse(newsp)
                    dt.Rows(0)("SellingPrice") = newPrice
                End If
            Else
                Dim ismrpopen As Boolean
                ismrpopen = Convert.ToBoolean(dt(0)("Ismrpopen"))

                If ismrpopen Then
                    Dim frmprompt As New frmSpecialPrompt("Enter Price")
                    frmprompt.ShowTextBox = True
                    frmprompt.txtValue.NumericInput = True
                    frmprompt.AllowDecimal = True
                    frmprompt.IsNumeric = True
                    frmprompt.AcceptButton = frmprompt.cmdOk
                    frmprompt.ShowTextBox = True
                    frmprompt.ShowDialog()
                    newval = 0
                    flag = 2

                    newval = frmprompt.GetResult()
                    If CDbl(newval) = 0 Then
                        ShowMessage(getValueByKey("CM018"), "CM018 - " & getValueByKey("CLAE04"))
                        'ShowMessage("Article is Removing As no Price found on it.", "Information")
                        dt = Nothing
                    ElseIf CDbl(newval) > 0 Then
                        dt.Rows(0)("SellingPrice") = CDbl(newval)
                    End If
                End If
            End If

            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Dim dr As DataRow = dt.Rows(0)
                If flag <> 1 Then
                    dr("Quantity") = 1
                End If

                '-------adil code
                '------- adil code end


                'dr("GrossAmt") = newval
            End If
            'If flag = 2 Then
            '    dt = qtycalc(dt(0)("EAN"), dt)
            'End If

            If dt.Rows(0)("FreezeSB") = True Then
                dt = Nothing
                ShowMessage(getValueByKey("BL100"), "BL100 - " & getValueByKey("CLAE04"))
                CtrlSalesPersons.CtrlTxtBox.Focus()

                Exit Sub
            End If
            Dim Stock As Double = objCM.GetStocks(clsAdmin.SiteCode, Ean.Trim, strArticle.Trim, True)
            If OnlineConnect = True AndAlso clsDefaultConfiguration.NegativeInventoryAllowed = False Then
                If CDbl(Stock) <= 0 Then
                    ShowMessage(getValueByKey("CM017"), "CM017 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Article out of Stock.", "Information")
                    dt = Nothing
                    CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                    CtrlSalesPersons.CtrlTxtBox.Focus()

                    Exit Sub
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

                    'IsInclusiveTax = False
                    'Dim dtTaxCalc = objCM.getTax(clsAdmin.SiteCode, dt.Rows(0)("ArticleCode"), "CMS", 1, Ean.Trim(), clsDefaultConfiguration.CSTTaxCode, False)

                    'If (dtTaxCalc IsNot Nothing AndAlso dtTaxCalc.Rows.Count > 0) Then
                    '    IsInclusiveTax = IIf(dtTaxCalc.Rows(0)("INCLUSIVE") IsNot DBNull.Value, dtTaxCalc.Rows(0)("INCLUSIVE"), False)
                    'End If

                    ' If IsInclusiveTax Then
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
                        'code commented by vipul for customer wise discount
                        '  CreateDataSetForTaxCalculation(originalTaxAmt, view(0)("BillLineNo").ToString(), dt.Rows(0)("ArticleCode"), sprice * view(0)("Quantity"), view(0)("BillLineNo").ToString(), view(0)("Quantity"), view(0)("EAN").ToString())
                        'code added  by vipul for Customer wise discount
                        If clsDefaultConfiguration.customerwisepricemanagement = True Then
                            CreateDataSetForTaxCalculation(originalTaxAmt, view(0)("BillLineNo").ToString(), dt.Rows(0)("ArticleCode"), (sprice * view(0)("Quantity")) - (view(0)("TOTALDISCOUNT")), view(0)("BillLineNo").ToString(), view(0)("Quantity"), view(0)("EAN").ToString())

                        Else
                            CreateDataSetForTaxCalculation(originalTaxAmt, view(0)("BillLineNo").ToString(), dt.Rows(0)("ArticleCode"), (sprice * view(0)("Quantity")), view(0)("BillLineNo").ToString(), view(0)("Quantity"), view(0)("EAN").ToString())
                        End If

                        'sprice = sprice - (originalTaxAmt / view(0)("Quantity"))
                    End If

                    ' End If

                    'If objArticleCombo.CheckIfComboArticle(strArticle) Then
                    '    filterScanArticle = String.Format("Btype='S' AND EAN='{0}' AND SellingPrice = '{1}'", Ean.Trim(), sprice)
                    'Else
                    '    filterScanArticle = String.Format("Btype='S' AND EAN='{0}' AND MRP = '{1}'", Ean.Trim(), dt.Rows(0)("MRP"))
                    'End If
                    filterScanArticle = String.Format("Btype='S' AND EAN='{0}' AND SellingPrice = '{1}'", Ean.Trim(), sprice)
                    Dim dv As New DataView(dsMain.Tables("CASHMEMODTL"), filterScanArticle, "EAN", DataViewRowState.CurrentRows)
                    If dv.Count > 0 AndAlso isCombo = False Then
                        dv.AllowEdit = True

                        For Each drView As DataRowView In dv
                            Dim taxAmountToMultiply = drView("TOTALTAXAMOUNT") / drView("Quantity")
                            Dim prevQty = drView("Quantity")
                            If flag = 2 Then
                                drView("Quantity") = qtycalc(drView("EAN"), dsMain.Tables("cashmemodtl")).Rows(0)("Quantity")

                            ElseIf flag = 1 Then
                                drView("Quantity") = newval

                            ElseIf flag = 3 Then
                                drView("Quantity") = CDec(drView("Quantity")) + compqty
                            Else
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
                            End If


                            '--price = drView("GrossAmt")

                            price = drView("SELLINGPRICE") * drView("Quantity")
                            drView("GrossAmt") = price
                            'If cbManualDisc.Enabled = True AndAlso cbManualDisc.SelectedIndex > -1 Then
                            If cbManualDisc.Enabled = True Then
                                ApplyManualPromotion(drView("EAN").ToString)
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
                            'code commented by vipul for customer wise discount
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
                        CtrlSalesPersons.CtrlTxtBox.Focus()

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
                        'ShowMessage(getValueByKey("CM018"), "CM018 - " & getValueByKey("CLAE04"))
                        ''ShowMessage("Article is Removing As no Price found on it.", "Information")
                        'dt = Nothing
                    End If
                ElseIf openMrp = True Then
                    Dim objPrompt As New frmSpecialPrompt(getValueByKey("SP002"))
                    objPrompt.ShowMessage = False
                    objPrompt.ShowTextBox = True
                    objPrompt.AllowDecimal = True
                    objPrompt.txtValue.MaxLength = 14
                    objPrompt.ShowDialog()
                    objPrompt.IsNumeric = True
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

                    If flag = 0 Then
                        If _prescriptionArticleAmount <> "" And Convert.ToDouble(_prescriptionArticleAmount) <> 0 Then
                            dr("Quantity") = Convert.ToDouble(_prescriptionArticleAmount)
                        Else
                            dr("Quantity") = 1
                        End If
                    End If
                    dr("GrossAmt") = price
                    Dim taxAmtWOCst As Double
                    tax = CreateDataSetForTaxCalculation(taxAmtWOCst, dsMain.Tables("CASHMEMODTL").Rows.Count + 1, strArticle, price, dr, dr("EAN").ToString())
                    'If IsInclusiveTax Then
                    '    dt.Rows(0)("SellingPrice") = dt.Rows(0)("SellingPrice") - taxAmtWOCst
                    'End If
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
                Dim dv As New DataView(dsMain.Tables("CASHMEMODTL"), "Btype='S' AND EAN='" & Ean.Trim() & "' AND SellingPrice='" & sprice & "'", "EAN", DataViewRowState.CurrentRows)
                If dv.Count > 0 AndAlso isCombo = False Then
                    dv.AllowEdit = True
                    For Each drView As DataRowView In dv
                        drView("Quantity") = drView("Quantity") + 1
                        price = drView("GrossAmt")
                        'If cbManualDisc.Enabled = True AndAlso cbManualDisc.SelectedIndex > -1 Then
                        'CalculateManualPromo(drView("EAN").ToString)
                        If cbManualDisc.Enabled = True Then
                            ApplyManualPromotion(drView("EAN").ToString)
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

                    If (dgMainGrid.Rows.Count > 1) Then
                        For index = 1 To dgMainGrid.Rows.Count - 1

                            If (dgMainGrid.Rows.Item(index)("BillLineNo") = dv(0)("BillLineNo")) Then
                                dgMainGrid.Select(index, 2)
                            End If
                        Next
                    End If

                    CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                    CtrlSalesPersons.CtrlTxtBox.Focus()
                    Exit Sub
                Else
                    '''''''''''''''''''

                    If flag = 0 Then
                        If _prescriptionArticleAmount <> "" AndAlso Convert.ToDouble(_prescriptionArticleAmount) <> 0 Then
                            dt.Rows(0)("Quantity") = Convert.ToDouble(_prescriptionArticleAmount)
                        Else
                            dt.Rows(0)("Quantity") = 1

                        End If
                    End If

                    If clsDefaultConfiguration.IsBillScanApplicable AndAlso ScanBill Then
                        If dtScanedBillArticle IsNot Nothing AndAlso dtScanedBillArticle.Rows.Count > 0 Then
                            dt.Rows(0)("Quantity") = dtScanedBillArticle.Rows(ScanItemBillSequence)("Quantity")
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

                    'code added by irfan on 15/11/2017 ==============================================================
                    'If dsMain.Tables("CASHMEMODTL").Columns.Contains("TaxPer") = True Then
                    '    For i = 0 To dsMain.Tables("CASHMEMODTL").Rows.Count - 1
                    '        dsMain.Tables("CASHMEMODTL").Rows(i)("TaxPer") = dtMainTax.Compute("sum(Value)", "")
                    '    Next
                    'Else
                    '    dsMain.Tables("CASHMEMODTL").Columns.Add("TaxPer", System.Type.GetType("System.String"))
                    '    For i = 0 To dsMain.Tables("CASHMEMODTL").Rows.Count - 1
                    '        dsMain.Tables("CASHMEMODTL").Rows(i)("TaxPer") = dtMainTax.Compute("sum(Value)", "")
                    '    Next
                    'End If

                    If cbManualDisc.Enabled = True Then
                        ApplyManualPromotion(dsMain.Tables("CASHMEMODTL").Rows(dsMain.Tables("CASHMEMODTL").Rows.Count - 1)("EAN").ToString)
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
                    'ShowLastOper(dt.Rows(0)("EAN").ToString, dt.Rows(0)("Discription").ToString, 1)
                    ProductImage.ShowArticleImage(strArticle)
                    DisplayText("Name: " & dt.Rows(0)("Discription") & "  Price  " & dt.Rows(0)("SellingPrice"))
                    For Each rowTemp As C1.Win.C1FlexGrid.Row In dgMainGrid.Rows
                        rowTemp.AllowResizing = True
                    Next

                    If (dgMainGrid.Rows.Count > 1) Then
                        dgMainGrid.Select(1, 2)
                    End If
                End If
            End If
            CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
            CtrlSalesPersons.CtrlTxtBox.Focus()
            'code added by vipul for customer wise discount
            If clsDefaultConfiguration.customerwisepricemanagement = True Then
                If Not String.IsNullOrEmpty(strArticle) And Not String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim) Then
                    cmdDefaultPromo_Click(cmdDefaultPromo, New System.EventArgs, strArticle)
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub AddComboArticles(ByVal dataRow As DataRow, ByVal rowNo As Integer)
        Try
            dataRow("BillLineNo") = dsMain.Tables("CASHMEMODTL").Rows(rowNo)("BillLineNO")
            Dim cashMemoArticleTable As DataTable = dsCashMemoComboDtl.Tables("CashMemoComboDtl")
            Dim comboLineNo As Integer = ((From row In cashMemoArticleTable.Rows Where
                                         row("BillLineNo") = dsMain.Tables("CASHMEMODTL").Rows(rowNo)("BillLineNO")).Count()) + 1
            Dim isExist As DataRow = (From row In cashMemoArticleTable.Rows Where
                                        row("BillLineNo") = dsMain.Tables("CASHMEMODTL").Rows(rowNo)("BillLineNO") AndAlso row("ArticleCode") = dataRow("ArticleCode") Select row).FirstOrDefault()
            If isExist Is Nothing Then
                Dim newRow As DataRow = cashMemoArticleTable.NewRow()
                'Dim lastRow As Integer = cashMemoArticleTable.Rows.Count - 1
                newRow("SiteCode") = clsAdmin.SiteCode
                newRow("FinYear") = clsAdmin.Financialyear
                newRow("BillNo") = lblCMNo.Text
                newRow("ComboLineNumber") = comboLineNo
                newRow("BillLineNo") = dsMain.Tables("CASHMEMODTL").Rows(rowNo)("BillLineNO")
                newRow("EAN") = dataRow("EAN")
                newRow("ComboArticleCode") = dsMain.Tables("CASHMEMODTL").Rows(rowNo)("ARTICLECODE")
                newRow("ArticleCode") = dataRow("ARTICLECODE")
                newRow("SellingPrice") = dataRow("SELLINGPRICE")
                newRow("NetSellingPrice") = dataRow("SELLINGPRICE") - If(dataRow("Discount"), Nothing, dataRow("Discount"))
                newRow("Quantity") = dataRow("Quantity")
                newRow("CREATEDAT") = clsAdmin.SiteCode
                newRow("CREATEDBY") = clsAdmin.UserCode
                newRow("UPDATEDBY") = clsAdmin.UserCode
                newRow("UPDATEDAT") = clsAdmin.SiteCode
                newRow("STATUS") = 1


                'code added by vipul for issue id 1536
                Try
                    ComboBeforeAddedToMainGrid("combo code " + dsMain.Tables("CASHMEMODTL").Rows(rowNo)("ARTICLECODE") + " ArticleCode " + dataRow("ARTICLECODE") + " Adding to MainGrid")
                    If CmbCount = 0 Then
                        CmbCount = +1
                        PrevComboCode = dsMain.Tables("CASHMEMODTL").Rows(rowNo)("ARTICLECODE").ToString
                        CurrentComboCode = dsMain.Tables("CASHMEMODTL").Rows(rowNo)("ARTICLECODE").ToString
                    Else
                        CurrentComboCode = dsMain.Tables("CASHMEMODTL").Rows(rowNo)("ARTICLECODE").ToString
                    End If

                    If Not String.IsNullOrEmpty(PrevComboCode.ToString) Then
                        If Not PrevComboCode.ToString.Equals(CurrentComboCode.ToString) Then
                            ComboBeforeAddedToMainGrid("ComboCode is  not matching  " + PrevComboCode.ToString + "  Actual ComboCode " + CurrentComboCode.ToString + "  Current ComboCode ")

                        End If
                    End If
                Catch exx As Exception
                    LogException(exx)
                End Try

                cashMemoArticleTable.Rows.Add(newRow)
            Else
                isExist("Quantity") = isExist("Quantity") + dataRow("Quantity")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub UpdateComboArticleQuantity(ByVal quantity As Integer, ByVal lineNo As Integer)
        For Each row In dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows
            If row("BillLineNo") = lineNo Then
                row("Quantity") = row("Quantity") * quantity
            End If
        Next
    End Sub
    Private Sub CalculateGrossAmtForComboArticles()
        For Each row In dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows
            row("GrossAmt") = row("SellingPrice") * row("Quantity")
            row("NetAmount") = row("NetSellingPrice") * row("Quantity")
            row("TotalDiscount") = (row("SellingPrice") - row("NetSellingPrice")) * row("Quantity")
        Next
    End Sub

    Private Sub dgMainGrid_AfterDataRefresh(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles dgMainGrid.AfterDataRefresh
        Try
            If dgMainGrid.Rows.Count > 1 Then
                'cmdHold.Text = "Hold " & "Ctrl+H"
                cmdHold.Text = getValueByKey("frmcashmemo.cmdhold")
                cmdHold.Tag = "Hold"
                SetButtons(1, True)
                SetButtons(3, False)

                'If lblCustSaleType.Text = "Dine In" AndAlso clsDefaultConfiguration.DineInProcess Then
                '    cmdGenerateKOT.Enabled = True
                'End If
                If clsDefaultConfiguration.IsGVSellAllowedWithOtherArticle = False AndAlso CheckIfGVItemScanned(True) = False Then
                    sellGiftVoucher.Enabled = False
                Else
                    sellGiftVoucher.Enabled = True
                End If

            Else
                'cmdHold.Text = "Resume " & "Ctrl+H"
                cmdHold.Text = getValueByKey("frmcashmemo.cmdresume")
                cmdHold.Tag = "Resume"
                SetButtons(1, False)
                SetButtons(3, True)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub dgMainGrid_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgMainGrid.AfterEdit
        Try
            If exitNowFlag = True Then Exit Sub
            If dgMainGrid.Rows.Count <= 1 Then Exit Sub
            '---- Code Added by Mahesh after edit TakeAwayQty this event should not call. Just check take away qty not more than total qty 
            If dgMainGrid.Cols(e.Col).Name.ToUpper.Equals("TAKEAWAYQUANTITY") Then
                If dgMainGrid.Rows(e.Row)("BTYPE") = "S" AndAlso dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") < 0 Then
                    ShowMessage(getValueByKey("CM021"), "CM021 - " & getValueByKey("CLAE04"))
                    'ShowMessage("Please Delete the Item if not Required", "Information")
                    dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") = 0
                    Exit Sub
                End If
                If dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") <> Nothing AndAlso dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") > 1 Then
                    If dgMainGrid.Rows(e.Row)("Quantity") <> Nothing AndAlso dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") > dgMainGrid.Rows(e.Row)("Quantity") Then
                        ShowMessage("Take Away Quantity cannot be greater than Total Quantity", getValueByKey("CLAE04"))
                        dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") = 0
                    End If
                End If
                Exit Sub
            End If
            '---- Validation Add By Mahesh Now TAKEAWAYQUANTITY can not be greater than QUANTITY ...
            If dgMainGrid.Cols("TAKEAWAYQUANTITY").Visible Then
                If dgMainGrid.Cols(e.Col).Name.ToUpper <> "TAKEAWAYQUANTITY" AndAlso Not IsDBNull(dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY")) Then
                    If dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") AndAlso dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") > 1 Then
                        If Not IsNothing(dgMainGrid.Rows(e.Row)("Quantity")) AndAlso dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") > dgMainGrid.Rows(e.Row)("Quantity") Then
                            ShowMessage("Take Away Quantity cannot be greater than Total Quantity", getValueByKey("CLAE04"))
                            dgMainGrid.Rows(e.Row)("TAKEAWAYQUANTITY") = 0
                            If dgMainGrid.Rows(dgMainGrid.Row)("BTYPE") = "S" AndAlso dgMainGrid.Rows(e.Row)("Quantity") <= 0 Then
                                dgMainGrid.Rows(e.Row)("Quantity") = 1
                            End If
                        End If
                    End If
                End If
            End If

            If dgMainGrid.Rows(e.Row)("BTYPE") = "S" AndAlso dgMainGrid.Rows(e.Row)("Quantity") <= 0 Then
                ShowMessage(getValueByKey("CM021"), "CM021 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please Delete the Item if not Required", "Information")
                dgMainGrid.Rows(e.Row)("Quantity") = 1
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

            If dgMainGrid.Rows(e.Row)("BTYPE") = "S" Then

                'Rakesh:01.11.2013:8265-> Avoid quantity change of voucher
                If dgMainGrid.Cols(e.Col).Name.ToUpper.Equals("QUANTITY") Then
                    If (dgMainGrid.Rows(e.Row)("ArticleCode").ToString.ToUpper.Equals("GVBASEARTICLE")) Then
                        ShowMessage(getValueByKey("GVS05"), "GVS05 - " & getValueByKey("CLAE04"))
                        dgMainGrid.Rows(e.Row)("Quantity") = 1
                        Exit Sub
                    End If

                    Dim discountAmount As Decimal = Decimal.Zero
                    discountAmount = dsMain.Tables("CASHMEMODTL").Compute("Sum(TotalDiscount)", String.Empty)
                    If clsDefaultConfiguration.IsMembership Then discountAmount = Decimal.Zero
                    If (discountAmount > 0 AndAlso IsChangeQuantityOrPrice = False) Then
                        Dim newArticleQuantity = dgMainGrid.Rows(e.Row)("Quantity")
                        dgMainGrid.Rows(e.Row)("Quantity") = _iArticleQtyBeforeChange
                        'code uncommented for remove promotion if you change in quantity
                        If clsDefaultConfiguration.customerwisepricemanagement = False Then
                            If MsgBox(getValueByKey("CM064"), MsgBoxStyle.Information, "CM064") = MsgBoxResult.Ok Then
                                cmdClrAllPromo_Click("ClearPromWithoutMessage", EventArgs.Empty)
                            End If
                        End If

                        dgMainGrid.Rows(e.Row)("Quantity") = newArticleQuantity
                    End If
                End If

                If clsDefaultConfiguration.NegativeInventoryAllowed = False AndAlso dgMainGrid.Rows(e.Row)("Quantity") > dgMainGrid.Rows(e.Row)("STOCK") Then
                    ShowMessage(getValueByKey("CM055"), "CM055 - " & getValueByKey("CLAE04"))
                    dgMainGrid.Rows(e.Row)("Quantity") = 1
                End If
                Dim TotalAmt As Double = dgMainGrid.Rows(e.Row)("Quantity") * dgMainGrid.Rows(e.Row)("SellingPrice")
                dgMainGrid.Rows(e.Row)("GrossAmt") = TotalAmt
                If clsDefaultConfiguration.DineInProcess Then
                    If currentDineInTable > 0 Then
                        If (dgMainGrid.Rows(e.Row)("Quantity") > _iArticleQtyBeforeChange) Then
                            IsMergeOrderBillNo()
                            cmdGenerateKOT.Enabled = True
                        End If
                    End If
                End If
                If dgMainGrid.Rows(e.Row)("Articlecode") <> "GVBaseArticle" And dgMainGrid.Rows(e.Row)("Articlecode") <> "CLPBaseArticle" Then

                    'If _iArticleQtyBeforeChange < dgMainGrid.Rows(e.Row)("Quantity") Then

                    '    If cbManualDisc.Enabled = True Then
                    '        ApplyManualPromotion(dgMainGrid.Rows(e.Row)("EAN").ToString(), _iArticleQtyBeforeChange)
                    '    Else
                    '        If Not IsDBNull(dgMainGrid.Rows(e.Row)("FirstLevel")) AndAlso Not String.IsNullOrEmpty(dgMainGrid.Rows(e.Row)("FirstLevel")) Then
                    '            RemoveSelectedArticlePromotion()
                    '        End If
                    '    End If

                    '    'CalculateManualPromo(dgMainGrid.Rows(e.Row)("EAN").ToString())
                    'ElseIf _iArticleQtyBeforeChange > dgMainGrid.Rows(e.Row)("Quantity") Then
                    '    If MsgBox(getValueByKey("CM060"), MsgBoxStyle.YesNo, "CM060") = MsgBoxResult.Yes Then
                    '        dgMainGrid.Rows(e.Row)("TotalDiscount") = 0
                    '        dgMainGrid.Rows(e.Row)("LineDiscount") = 0
                    '        dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") = 0
                    '        dgMainGrid.Rows(e.Row)("FIRSTLEVEL") = String.Empty
                    '        dgMainGrid.Rows(e.Row)("TOPLEVEL") = String.Empty
                    '        dgMainGrid.Rows(e.Row)("MANUALPROMO") = 0

                    '        'Issue ID 7624
                    '        If IsInclusiveTax Then
                    '            CreateDataSetForTaxCalculation(0, dgMainGrid.Rows(e.Row)("BillLineNo").ToString(), dgMainGrid.Rows(e.Row)("Articlecode").ToString(), (_iArticleQtyBeforeChange * dgMainGrid.Rows(e.Row)("SellingPrice")), e.Row, _iArticleQtyBeforeChange, dgMainGrid.Rows(e.Row)("Ean").ToString(), True)
                    '        Else
                    '            CreateDataSetForTaxCalculation(0, dgMainGrid.Rows(e.Row)("BillLineNo").ToString(), dgMainGrid.Rows(e.Row)("Articlecode").ToString(), (_iArticleQtyBeforeChange * dgMainGrid.Rows(e.Row)("SellingPrice")), e.Row, _iArticleQtyBeforeChange, dgMainGrid.Rows(e.Row)("Ean").ToString())
                    '        End If
                    '        'End Issue 7624
                    '    Else
                    '        dgMainGrid.Rows(e.Row)("Quantity") = _iArticleQtyBeforeChange
                    '    End If
                    'Else
                    If _PriceBeforeChange IsNot Nothing AndAlso _PriceBeforeChange <> dgMainGrid.Rows(1)("SellingPrice") Then
                        'code commneted by vipul for customer wise discount
                        ' CreateDataSetForTaxCalculation(0, dgMainGrid.Rows(e.Row)("BillLineNo").ToString(), dgMainGrid.Rows(e.Row)("ArticleCode"), dgMainGrid.Rows(e.Row)("GrossAmt") - IIf(dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") Is DBNull.Value, 0, dgMainGrid.Rows(e.Row)("TOTALDISCOUNT")), e.Row, dgMainGrid.Rows(e.Row)("Quantity"), dgMainGrid.Rows(e.Row)("EAN"))
                        'code added  by vipul for Customer wise discount
                        If clsDefaultConfiguration.customerwisepricemanagement = True Then
                            CreateDataSetForTaxCalculation(0, dgMainGrid.Rows(e.Row)("BillLineNo").ToString(), dgMainGrid.Rows(e.Row)("ArticleCode"), dgMainGrid.Rows(e.Row)("GrossAmt") - IIf(dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") Is DBNull.Value, 0, (dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") * dgMainGrid.Rows(e.Row)("GrossAmt")) / 100), e.Row, dgMainGrid.Rows(e.Row)("Quantity"), dgMainGrid.Rows(e.Row)("EAN"))
                        Else
                            CreateDataSetForTaxCalculation(0, dgMainGrid.Rows(e.Row)("BillLineNo").ToString(), dgMainGrid.Rows(e.Row)("ArticleCode"), dgMainGrid.Rows(e.Row)("GrossAmt") - IIf(dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") Is DBNull.Value, 0, dgMainGrid.Rows(e.Row)("TOTALDISCOUNT")), e.Row, dgMainGrid.Rows(e.Row)("Quantity"), dgMainGrid.Rows(e.Row)("EAN"))
                        End If
                    End If

                End If

                If clsDefaultConfiguration.EvasPizzaChanges = True Then
                    Dim objcom As New clsCommon
                    Dim _strPromoID As String = ""
                    _strPromoID = objcom.GetPromotionIdByName("Store Manager Promo")
                    Dim _reason As New frmArticlesRemark

                    'If dgMainGrid.Cols(e.Col).Name = "TOTALDISCOUNT" Then

                    '    dgMainGrid.Rows(e.Row)("LineDiscount") = dgMainGrid.Rows(e.Row)("TotalDiscount")

                    'End If
                    'If dgMainGrid.Cols(e.Col).Name = "TOTALDISCPERCENTAGE" Then

                    '    If dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") > 100 Then
                    '        ShowMessage(getValueByKey("UA008"), "UA008 - " & getValueByKey("CLAE04"))
                    '        dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") = 0.0
                    '        'Exit Sub
                    '    End If

                    '    Dim disc = dgMainGrid.Rows(e.Row)("GROSSAMT") * (dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") / 100)

                    '    dgMainGrid.Rows(e.Row)("LineDiscount") = disc
                    'End If
                    '----------- evaz pizza changes on 25-11-2016
                    If dgMainGrid.Cols(e.Col).Name.ToUpper() = "TOTALDISCOUNT" Then
                        If dgMainGrid.Rows(e.Row)("TOTALDISCOUNT").ToString.Contains("-") Then
                            dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") = dgMainGrid.Rows(e.Row)("TOTALDISCOUNT").ToString().Replace("-", "")
                        End If
                        If dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") > dgMainGrid.Rows(e.Row)("GROSSAMT") Then
                            ShowMessage(getValueByKey("UA008"), "UA008 - " & getValueByKey("CLAE04"))
                            dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") = 0.0
                            'Exit Sub
                        Else
                            If dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") <> "0" Then
                                _reason.IsKOTReason = True
                                _reason.KOTReasonDetails = ValueChangeRemark
                                _reason.ShowDialog()
                                If _reason.DialogResult = Windows.Forms.DialogResult.OK Then
                                    ValueChangeRemark = _reason.KOTReasonDetails
                                End If
                            End If
                        End If
                        dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") = (dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") * 100) / dgMainGrid.Rows(e.Row)("GROSSAMT")
                    ElseIf dgMainGrid.Cols(e.Col).Name.ToUpper() = "TOTALDISCPERCENTAGE" Then
                        If dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE").ToString.Contains("-") Then
                            dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") = dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE").ToString().Replace("-", "")
                        End If
                        If dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") > 100 Then
                            ShowMessage(getValueByKey("UA008"), "UA008 - " & getValueByKey("CLAE04"))
                            dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") = 0.0
                            'Exit Sub
                        Else
                            If dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") <> "0" Then
                                _reason.IsKOTReason = True
                                _reason.KOTReasonDetails = ValueChangeRemark
                                _reason.ShowDialog()
                                If _reason.DialogResult = Windows.Forms.DialogResult.OK Then
                                    ValueChangeRemark = _reason.KOTReasonDetails
                                End If
                            End If


                        End If
                        dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") = (dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") * dgMainGrid.Rows(e.Row)("GROSSAMT")) / 100
                    End If
                    dgMainGrid.Rows(e.Row)("NETAMOUNT") = (dgMainGrid.Rows(e.Row)("GROSSAMT") + IIf(dgMainGrid.Rows(e.Row)("EXCLUSIVETAX") Is DBNull.Value, 0, dgMainGrid.Rows(e.Row)("EXCLUSIVETAX"))) - dgMainGrid.Rows(e.Row)("TOTALDISCOUNT")
                    dgMainGrid.Rows(e.Row)("LINEDISCOUNT") = dgMainGrid.Rows(e.Row)("TOTALDISCOUNT")
                    dgMainGrid.Rows(e.Row)("AuthUserId") = clsAdmin.UserName
                    dgMainGrid.Rows(e.Row)("AuthUserRemarks") = clsAdmin.UserName
                    dgMainGrid.Rows(e.Row)("PROMOTIONID") = _strPromoID + "," + _strPromoID
                    dgMainGrid.Rows(e.Row)("TOPLEVEL") = _strPromoID
                    dgMainGrid.Rows(e.Row)("FirstLEVEL") = _strPromoID
                    dgMainGrid.Rows(e.Row)("TOPLEVELDISC") = dgMainGrid.Rows(e.Row)("TOTALDISCOUNT")
                End If

                dgMainGrid.Rows(e.Row)("TotalDiscount") = IIf(dgMainGrid.Rows(e.Row)("LineDiscount") Is DBNull.Value, 0, dgMainGrid.Rows(e.Row)("LineDiscount")) + IIf(dgMainGrid.Rows(e.Row)("CLPDiscount") Is DBNull.Value, 0, dgMainGrid.Rows(e.Row)("CLPDiscount"))
                'code commented by vipul for customer wise discount
                'If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                '    TotalAmt = TotalAmt - IIf(dgMainGrid.Rows(e.Row)("TotalDiscount") Is DBNull.Value, 0, dgMainGrid.Rows(e.Row)("TotalDiscount"))
                'End If
                'code added by vipul for customer wise discount
                If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True And clsDefaultConfiguration.customerwisepricemanagement = False Then
                    TotalAmt = TotalAmt - IIf(dgMainGrid.Rows(e.Row)("TotalDiscount") Is DBNull.Value, 0, dgMainGrid.Rows(e.Row)("TotalDiscount"))
                End If
                If clsDefaultConfiguration.customerwisepricemanagement = True Then
                    TotalAmt = TotalAmt - IIf(dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") Is DBNull.Value, 0, (dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE") * dgMainGrid.Rows(e.Row)("GROSSAMT")) / 100)
                End If
                If dgMainGrid.Rows(e.Row)("TotalDiscount") > 0 Then

                    CreateDataSetForTaxCalculation(0, dgMainGrid.Rows(e.Row)("BillLineNo").ToString(), dgMainGrid.Rows(e.Row)("Articlecode").ToString(), TotalAmt, e.Row, dgMainGrid.Rows(e.Row)("Quantity"), dgMainGrid.Rows(e.Row)("Ean").ToString())

                Else
                    dgMainGrid.Rows(e.Row)("TOTALTAXAMOUNT") = (IIf(dgMainGrid.Rows(e.Row)("TOTALTAXAMOUNT") IsNot DBNull.Value, dgMainGrid.Rows(e.Row)("TOTALTAXAMOUNT"), 0) / IIf(_iArticleQtyBeforeChange = 0, 1, _iArticleQtyBeforeChange)) * dgMainGrid.Rows(e.Row)("Quantity")

                    Dim dvTax As New DataView(dtMainTax, "BillLineNo='" & dgMainGrid.Rows(e.Row)("BillLineNo").ToString() & "' AND ArticleCode='" & dgMainGrid.Rows(e.Row)("Articlecode").ToString() & "'", "StepNo", DataViewRowState.CurrentRows)
                    For Each tax In dvTax
                        tax("TaxAmount") = (tax("TaxAmount") / IIf(_iArticleQtyBeforeChange = 0, 1, _iArticleQtyBeforeChange)) * dgMainGrid.Rows(e.Row)("Quantity")
                    Next
                End If

                Dim stringArray As String() = dgMainGrid.Rows(e.Row)("Discription").ToString().Split(vbCrLf)
                If stringArray.Count > 1 Then
                    'by adil for sbbarro 11-jan-2017
                    ' comment remove for revert code purpose by khusrao adil on 18-jan-2018
                    dgMainGrid.Rows(e.Row)("Discription") = ChangeComboItemsQuantity(stringArray, _iArticleQtyBeforeChange, dgMainGrid.Rows(e.Row)("Quantity"))
                    Dim stringArray1 As String() = dgMainGrid.Rows(e.Row)("Discription").ToString().Split(vbCrLf)
                End If
                ReCalculateCM(dgMainGrid.Rows(e.Row)("EAN").ToString())
            End If
            calculateTotalbill()
            If clsDefaultConfiguration.ExtendScreen Then
                If clsDefaultConfiguration.HariOmExtendScreen = True Then
                    TwoLineExtendScreen(e.Row - 1)
                Else
                    ExtendFun()
                End If
            End If
            If clsDefaultConfiguration.IsPoleDisply = True Then
                TwoLineExtendScreen(e.Row - 1)
            End If

            If (Not IsChangeQuantityfromNumPad) Then
                If (dgMainGrid.Rows.Count > 1) Then
                    '' as per version 2013
                    ' dgMainGrid.Select(1, 2)
                End If

                CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
                'CtrlSalesPersons.CtrlTxtBox.Select()
                CtrlSalesPersons.AndroidSearchTextBox.Text = String.Empty
                CtrlSalesPersons.AndroidSearchTextBox.Focus()
                CtrlSalesPersons.AndroidSearchTextBox.Select()
            End If
            'If clsDefaultConfiguration.DineInProcess Then
            '    cmdGenerateKOT.Enabled = True
            '    IsMergeOrderBillNo()
            'End If
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
            If dsMain.Tables("CASHMEMODTL").Columns.Contains("TaxPer") = True Then
                dsMain.Tables("CASHMEMODTL").Columns.Remove("TaxPer")
            End If
            Cursor.Current = Cursors.WaitCursor
            Dim StrError As String = ""
            Dim billNo As String
            Dim dsTemp As DataSet
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
                    Dim strReason As String = String.Empty
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

                    For Each dr As DataRow In dsTemp.Tables("cashmemodtl").Rows
                        Dim qty As Decimal = Convert.ToDecimal(dr("Quantity"))
                        dr("Quantity") = Math.Round(qty, 3)
                    Next

                    For Each drh As DataRow In dsTemp.Tables("CASHMEMOHDR").Rows
                        Dim qty As Decimal
                        qty = Math.Round(Convert.ToDecimal(drh("TOTALITEMS")), 3)
                        drh("TOTALITEMS") = qty
                    Next
                    'added by khusrao adil on 16-02-2018 for innviti with card functionality point _ natural client
                    Dim InnvoitiPaymentEnable As Boolean = False
                    'added by khusrao adil for innovati on 18-05-2017
                    If clsDefaultConfiguration.PayFromInnoviti AndAlso clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) Then
                        objCM.clsInnovitiList = cashmemoInnovitiList
                        If dtInnoviti.Rows.Count > 0 Then
                            InnvoitiPaymentEnable = True
                            objCM.dtInnoviti = dtInnoviti
                        End If
                    End If
                    'If objCM.SaveCashMemo(clsAdmin.DayOpenDate, OnlineConnect, dsTemp, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.UserCode, UpdateFlag, billNo, StrError, clsAdmin.Financialyear, clsDefaultConfiguration.CashMemoStorageLocation, "CMS") Then
                    'Added by Rohit for Cr-5938
                    objCM.dDueDate = _dDueDate
                    objCM.strRemarks = _strRemarks
                    objCM.CashMemoResetonDayClose = clsDefaultConfiguration.CashMemoResetonDayClose
                    billNo = lblCMNo.Text 'For Innovitii
                    If objCM.SaveCashMemo(clsAdmin.DayOpenDate, OnlineConnect, dsTemp, Nothing, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.UserCode, UpdateFlag, _
                                          billNo, StrError, clsAdmin.Financialyear, clsDefaultConfiguration.StockStorageLocation, "CMS", dtTempGv, clsAdmin.ClpArticle, _
                                          CLPCustomerId, clsAdmin.CLPProgram, "", clsAdmin.CVProgram, clsAdmin.CreditValidDays, , , FloatAmt, _
                                          clsDefaultConfiguration.UpdateBillTime, clsDefaultConfiguration.IsMembership, _
                                          UpdateStockAtStoreLevel:=clsDefaultConfiguration.UpdateStockAtStoreLevel, _
                                          IsHostManagement:=clsDefaultConfiguration.IsHostManagementEnable, _
                                          innovatiiForTerminals:=clsDefaultConfiguration.InnovitiForTerminals, _
                                          InnovitiPaymentEnable:=InnvoitiPaymentEnable) Then

                        Dim objPrint As New clsCashMemoPrint(billNo, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving) '0000413

                        objPrint.DisplayArticleFullName = clsDefaultConfiguration.PrintItemFullName
                        objPrint.AllowDecimalQty = clsDefaultConfiguration.AllowDecimalQty
                        objPrint.WeightScaleEnabled = clsDefaultConfiguration.WeightScaleEnabled

                        objPrint.TokenNoRequiredInKOT = clsDefaultConfiguration.TokenNoRequiredInKOT
                        objPrint.CashMemoResetonDayClose = clsDefaultConfiguration.CashMemoResetonDayClose
                        objPrint.AllowBillingOnlyAfterSelectionOfSalesType = clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType
                        objPrint.PrintFormatNo = clsDefaultConfiguration.PrintFormatNo
                        objPrint.IsKotFontLarge = clsDefaultConfiguration.IsKotFontLarge
                        objPrint.IsKotFontBold = clsDefaultConfiguration.IsKotFontBold
                        objPrint.KOTPrintFormatNo = clsDefaultConfiguration.KOTPrintFormatNo
                        objPrint.MettlerConnString = clsDefaultConfiguration.MettlerConnString
                        objPrint.RoundOff = clsDefaultConfiguration.BillRoundOffAt
                        objPrint.IsRoundRequired = clsDefaultConfiguration.RoundOffRequired
                        objPrint.IsDintInEnabled = clsDefaultConfiguration.DineInProcess
                        objPrint.IsKotPrintRequired = clsDefaultConfiguration.KOTPrintRequired
                        objPrint.IsInvoicePrintFlag = clsDefaultConfiguration.WhetherBillPrintisRequiredornot
                        objPrint.IsInvoicePrintRequired = dtval
                        'added by vipin
                        objPrint.UserID = clsAdmin.UserCode
                        objPrint.EnableMailReSend = clsDefaultConfiguration.EnableMailReSend
                        objPrint.DocumentType = "CM"
                        objPrint.Terminalid = clsAdmin.TerminalID
                        'Select Case CustomerSaleType
                        '    Case enumCustomerSaleType.Dine_In
                        '        objPrint.CustomerSaleType = "Dine In"
                        '    Case enumCustomerSaleType.Home_Delivery
                        '        objPrint.CustomerSaleType = "Home Delivery"
                        '    Case enumCustomerSaleType.Take_Away
                        '        objPrint.CustomerSaleType = "Take Away"
                        '    Case Else
                        'End Select

                        'objPrint.CompositeTaxReqOnPrint = clsDefaultConfiguration.CompositeTaxReqOnPrint

                        Dim ErrorMsg As String = ""

                        ''--- Code Added By Mahesh for delivery Person Name in edit Mode
                        'Dim deliveryPersonName As String = String.Empty
                        'If (dsTemp.Tables("CashMemoHdr").Rows.Count > 0 AndAlso EditMode_IsupdateDeliveryPersonAllowed) Then
                        '    deliveryPersonName = dsTemp.Tables("CashMemoHdr").Rows(0)("DeliveryPersonID").ToString()
                        '    objPrint.DeliveryPersonName = deliveryPersonName
                        'End If
                        If clsDefaultConfiguration.IsMembership Then
                            If Not String.IsNullOrEmpty(Membershipid) Then
                                CashMemoPrintforMemberShip(billNo, clsDefaultConfiguration.ClientName, clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "", "", "", ErrorMsg, GiftMsg, _billAmt, _paidAmt, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy)
                            Else
                                objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Duplicate", "", "", ErrorMsg, "", _billAmt, _paidAmt, Nothing, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, TokenNoRequiredInKOT:=clsDefaultConfiguration.TokenNoRequiredInKOT) '0000413       code added by irfan on 9/9/2017 visiblity of hsn and tax
                            End If
                        Else
                            'objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Duplicate", "", "", ErrorMsg, "", _billAmt, _paidAmt, Nothing, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath) '0000413
                            objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Duplicate", "", "", ErrorMsg, "", _billAmt, _paidAmt, Nothing, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsCounterCopyKot:=True, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, TokenNoRequiredInKOT:=clsDefaultConfiguration.TokenNoRequiredInKOT) '0000413     code added by irfan on 9/9/2017 visiblity of hsn and tax
                        End If


                        Dim TotalPay As Double
                        Dim clsVoucher As New clsPrintVoucher
                        If Not dtTempGv Is Nothing AndAlso dtTempGv.Rows.Count > 0 Then
                            Dim dv As New DataView(dtTempGv, "", "VOURCHERSERIALNBR", DataViewRowState.CurrentRows)
                            If dv.Count > 0 Then
                                For Each dr As DataRowView In dv
                                    ' objPrint.PrintGiftVoucher(dr("VOURCHERSERIALNBR").ToString(), dr("ValueOfVoucher").ToString(), CDate(IIf(dr("ExpiryDate") Is DBNull.Value, Now, dr("ExpiryDate"))), DateDiff(DateInterval.Day, dr("ExpiryDate"), dr("issuedondate")))
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
                        cmdNew_Click(sender, e)
                        Return True
                    Else
                        ''added By ketan Sync Issue delete data from new table
                        'objCM.DeleteCashMemoTempTrans(billNo, clsAdmin.SiteCode)
                        ShowMessage(StrError, getValueByKey("CLAE05"))
                        Try
                            Throw New Exception(StrError & "line No 3070 cmd_savePrint_click for save")
                        Catch ex As Exception
                            LogException(ex)
                        End Try
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
                        For Each dr As DataRow In dsTemp.Tables("cashmemodtl").Rows

                            dr("Quantity") = Math.Round(Val(dr("Quantity")), 3)
                            'Math.Round(qty, 2)
                        Next
                        For Each drh As DataRow In dsTemp.Tables("CASHMEMOHDR").Rows

                            drh("TOTALITEMS") = Math.Round(Val(drh("TOTALITEMS")), 3)
                            'Math.Round(qty, 2)
                        Next
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
                        If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count > 0 Then
                            'Dim lineNoList As List(Of Object) = (From dr In dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows Select dr("BillLineNo")).Distinct().ToList()
                            'For Each lineNumber In lineNoList
                            '    Dim quantity As Integer = (From dr In dsTemp.Tables("CashMemoDtl").Rows Where dr("BillLineNo") = lineNumber Select dr("Quantity")).FirstOrDefault()
                            '    If quantity > 1 Then
                            '        UpdateComboArticleQuantity(quantity, lineNumber)
                            '    End If
                            'Next
                            CalculateGrossAmtForComboArticles()
                            SetCostPrice(clsDefaultConfiguration.isMAPbasedCost, dsCashMemoComboDtl.Tables("CashMemoComboDtl"), clsAdmin.SiteCode, "CostPrice")
                        End If
                        If Not IsDBNull(comboArticleCopy) Then
                            objCM.comboArticleCopy = comboArticleCopy
                        End If
                        'ashma 18 may
                        'If clsDefaultConfiguration.PayFromInnoviti AndAlso clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) Then
                        '    objCM.clsInnovitiList = cashmemoInnovitiList
                        'End If
                        Dim InnvoitiPaymentEnable As Boolean = False
                        'added by khusrao adil for innovati on 18-05-2017
                        If clsDefaultConfiguration.PayFromInnoviti AndAlso clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) Then
                            objCM.clsInnovitiList = cashmemoInnovitiList
                            objCM.dtInnoviti = dtInnoviti
                            If dtInnoviti.Rows.Count > 0 Then
                                InnvoitiPaymentEnable = True
                                objCM.dtInnoviti = dtInnoviti
                            End If
                        End If
                        billNo = lblCMNo.Text 'For Innovitii

                        If objCM.SaveCashMemo(clsAdmin.DayOpenDate, OnlineConnect, dsTemp, dsCashMemoComboDtl, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.UserCode, _
                                              UpdateFlag, billNo, StrError, clsAdmin.Financialyear, clsDefaultConfiguration.StockStorageLocation, "CMS", dtTempGv, _
                                              clsAdmin.ClpArticle, CLPCustomerId, clsAdmin.CLPProgram, "", clsAdmin.CVProgram, clsAdmin.CreditValidDays, _
                                              CLPRedemptionflag, RedemptionPoints, FloatAmt, clsDefaultConfiguration.UpdateBillTime, clsDefaultConfiguration.IsMembership, _
                                              UpdateStockAtStoreLevel:=clsDefaultConfiguration.UpdateStockAtStoreLevel, _
                                              innovatiiForTerminals:=clsDefaultConfiguration.InnovitiForTerminals, _
                                              IsHostManagement:=clsDefaultConfiguration.IsHostManagementEnable, _
                                              InnovitiPaymentEnable:=InnvoitiPaymentEnable) Then
                            'If CLPCustomerId <> String.Empty Then
                            'Dim TotalPoints As Double = IIf(dsTemp.Tables("CashMemoDtl").Compute("SUM(CLPPoints)", "") Is DBNull.Value, 0, dsTemp.Tables("CashMemoDtl").Compute("SUM(CLPPoints)", ""))
                            'If TotalPoints <= 0 Then
                            '    TotalPoints = IIf(dsTemp.Tables("CashMemoDtl").Compute("SUM(CLPDiscount)", "") Is DBNull.Value, 0, dsTemp.Tables("CashMemoDtl").Compute("SUM(CLPDiscount)", ""))
                            '    If TotalPoints > 0 Then
                            '        TotalPoints = Convert.ToDouble(CashSummary.CtrllblNetAmt.Text)
                            '    End If
                            'End If                            
                            'objCM.SaveCashMemoCombo(dsCashMemoComboDtl)
                            ' dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Clear()
                            ' comboArticleCopy.Rows.Clear()

                            isCashierPromoSelected = False
                            If TotalPoints <> 0 Then
                                If objCM.SaveClpData(dsTemp, clsAdmin.CLPProgram, CLPCustomerId, TotalPoints, RedemptionPoints, CustomerBalancePoint) = False Then

                                    ShowMessage(getValueByKey("CM049"), "CM049 - " & getValueByKey("CLAE04"))
                                Else
                                    '  SendSMS(clpCustomerMobileNo, "Dear Customer You have earn " & TotalPoints & " Points on your current shopping. Thank you for shopping with us.")
                                End If
                            End If
                            'End If
                            Dim TotalPay As Double = Val(CashSummary.CtrllblNetAmt.Text)

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

                            ClearData()
                            'Dim objPrint As New clsPrinting(billNo)

                            'Dim deliveryPersonName As String = String.Empty
                            'If (dsTemp.Tables("CashMemoHdr").Rows.Count > 0) Then
                            '    deliveryPersonName = dsTemp.Tables("CashMemoHdr").Rows(0)("DeliveryPersonID").ToString()
                            'End If

                            Dim objPrint As New clsCashMemoPrint(billNo, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
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
                            objPrint.IsKotFontLarge = clsDefaultConfiguration.IsKotFontLarge
                            objPrint.IsKotFontBold = clsDefaultConfiguration.IsKotFontBold
                            objPrint.KOTPrintFormatNo = clsDefaultConfiguration.KOTPrintFormatNo
                            objPrint.MettlerConnString = clsDefaultConfiguration.MettlerConnString
                            objPrint.RoundOff = clsDefaultConfiguration.BillRoundOffAt
                            objPrint.IsRoundRequired = clsDefaultConfiguration.RoundOffRequired
                            objPrint.IsDintInEnabled = clsDefaultConfiguration.DineInProcess
                            objPrint.IsKotPrintRequired = clsDefaultConfiguration.KOTPrintRequired
                            objPrint.IsInvoiceSendOnMailRequired = clsDefaultConfiguration.IsInvoiceSendOnMailRequired
                            objPrint.SendInvoiceSMS = clsDefaultConfiguration.SendInvoiceSMS
                            objPrint.IsInvoicePrintFlag = clsDefaultConfiguration.WhetherBillPrintisRequiredornot
                            objPrint.IsInvoicePrintRequired = dtval
                            'added by vipin
                            'added by vipin
                            objPrint.UserID = clsAdmin.UserCode
                            objPrint.EnableMailReSend = clsDefaultConfiguration.EnableMailReSend
                            objPrint.DocumentType = "CM"
                            objPrint.Terminalid = clsAdmin.TerminalID
                            'Select Case CustomerSaleType
                            '    Case enumCustomerSaleType.Dine_In
                            '        objPrint.CustomerSaleType = "Dine In"
                            '    Case enumCustomerSaleType.Home_Delivery
                            '        objPrint.CustomerSaleType = "Home Delivery"
                            '    Case enumCustomerSaleType.Take_Away
                            '        objPrint.CustomerSaleType = "Take Away"
                            '    Case Else

                            'End Select

                            'objPrint.CompositeTaxReqOnPrint = clsDefaultConfiguration.CompositeTaxReqOnPrint
                            'objPrint.TaxDetailsRequired = clsDefaultConfiguration.PrintingTaxInfo

                            '    objPrint.DeliveryPersonName = deliveryPersonName
                            'Dim Errormsg As String = ""
                            'code added for number of print copies
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

                            'Rakesh:09-July-2013-->Start: Template based cashmemo bill printing
                            If (clsDefaultConfiguration.TemplatePrintingAllowed) Then
                                objPrint.PrintTemplateCashMemoBillDetails(billNo, clsAdmin.SiteCode, clsAdmin.CurrencyCode, String.Empty)
                            Else
                                If clsDefaultConfiguration.IsMembership Then
                                    If Not String.IsNullOrEmpty(Membershipid) Then
                                        CashMemoPrintforMemberShip(billNo, clsDefaultConfiguration.ClientName, clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMS", "", "", "", Errormsg, GiftMsg, _billAmt, _paidAmt, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy)
                                    Else
                                        'objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMS", "", "", "", Errormsg, "", _billAmt, _paidAmt, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6, TokenNoRequiredInKOT:=clsDefaultConfiguration.TokenNoRequiredInKOT)    ' code added by irfan on 9/9/2017 visiblity of hsn and tax
                                        objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMS", "", "", "", Errormsg, "", _billAmt, _paidAmt, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6, JKPrintFormatEnable:=clsDefaultConfiguration.JKPrintFormatEnable)   ' code added by irfan against tender visblity  code added by irfan on 11/09/2017 visiblity of hsn and tax
                                    End If
                                Else
                                    'objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMS", "", "", "", Errormsg, "", _billAmt, _paidAmt, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, NoOFCopies:=NoOFCopies, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6, TokenNoRequiredInKOT:=clsDefaultConfiguration.TokenNoRequiredInKOT)  ' code added by irfan on 9/9/2017 visiblity of hsn and tax
                                    'modified by khusrao adil on 8-12-2017 for jk sprint 32
                                    'JKPrintFormatEnable flag added
                                    objPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMS", "", "", "", Errormsg, "", _billAmt, _paidAmt, clsDefaultConfiguration.PrintSeparateKotFoReachHierarchy, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot, IsCounterCopy, IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, EvasPizzaChanges:=clsDefaultConfiguration.EvasPizzaChanges, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6, JKPrintFormatEnable:=clsDefaultConfiguration.JKPrintFormatEnable)   'code added by irfan against tender visblity    code added by irfan on 11/09/2017 visiblity of hsn and tax

                                End If

                            End If
                            'code commented by vipul for call update stock in one transaction 
                            'Rakesh:09-July-2013-->End: Template based cashmemo bill printing
                            'If clsDefaultConfiguration.UpdateStockAtStoreLevel Then
                            '    If Not objCM.UpdateStockForArticle(clsAdmin.SiteCode, clsAdmin.UserCode, clsDefaultConfiguration.StockStorageLocation, billNo, clsAdmin.Financialyear, dsTemp, dsCashMemoComboDtl) Then
                            '        ActivityLogForShift(Nothing, "Update Stock Fail", "")
                            '    End If
                            'End If
                            dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Clear()
                            comboArticleCopy.Rows.Clear()
                            'Transfer data to kds tables


                            Dim obj As New clsAuthorization
                            Dim DtKdsTran As DataTable = obj.GetSitedAllowedTran(clsAdmin.SiteCode, "KDS")
                            If Not DtKdsTran Is Nothing AndAlso DtKdsTran.Rows.Count > 0 Then
                                objCM.TransferKdsData(billNo, clsAdmin.SiteCode)
                            End If

                            _remarks = String.Empty
                            If Errormsg <> String.Empty Then
                                ShowMessage(Errormsg, getValueByKey("CLAE05"))
                                Try
                                    Throw New Exception(Errormsg & "line No 3070 cmd_savePrint_click for print")
                                Catch ex As Exception
                                    LogException(ex)
                                End Try
                            End If





                            Dim clsVoucher As New clsPrintVoucher
                            If Not dtTempGv Is Nothing AndAlso dtTempGv.Rows.Count > 0 Then
                                Dim dv As New DataView(dtTempGv, "", "VOURCHERSERIALNBR", DataViewRowState.CurrentRows)
                                If dv.Count > 0 Then

                                    For Each dr As DataRowView In dv
                                        SendSMS(clpCustomerMobileNo, "E-Voucher of Couture Value Rs." & dr("ValueOfVoucher") & ". Voucher No : " & dr("VOURCHERSERIALNBR"))
                                        'objPrint.PrintGiftVoucher(dr("VOURCHERSERIALNBR").ToString(), dr("ValueOfVoucher").ToString(), CDate(IIf(dr("ExpiryDate") Is DBNull.Value, Now, dr("ExpiryDate"))), DateDiff(DateInterval.Day, dr("ExpiryDate"), dr("issuedondate")))
                                        clsVoucher.PrintGiftVoucherAndCreditNote("CMS", clsAdmin.SiteCode, "GiftVoucher", dr("VOURCHERSERIALNBR"), dr("ValueOfVoucher"), CDate(IIf(dr("ExpiryDate") Is DBNull.Value, Now, dr("ExpiryDate"))), clsAdmin.UserName, dr("IssuedDocNumber"), clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                                    Next
                                    SendSMS(clpCustomerMobileNo, "Your balance points are " & CustomerBalancePoint)
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
                            If clsDefaultConfiguration.CLPRegistration = True AndAlso TotalPay >= clsDefaultConfiguration.CLPRegistrationAmt Then
                                ShowMessage(getValueByKey("CM023"), "CM023 - " & getValueByKey("CLAE04"))
                                'ShowMessage("Customer is Eligible For CLP Registration", "Information")
                            End If

                            clsCashMemo.dsCashMemoPrinting = Nothing
                            cmdNew_Click(sender, e)

                            Return True
                        Else
                            ''added By ketan Sync Issue delete data from new table
                            'objCM.DeleteCashMemoTempTrans(billNo, clsAdmin.SiteCode)
                            saveCashCounter += 1
                            If saveCashCounter < ConstCounter Then
                                '  ShowMessage(saveCashCounter, getValueByKey("CLAE05"))
                                Thread.Sleep(2000)
                                cmdSavePrint_Click(sender, e)
                                ' ShowMessage(System.DateTime.Now, getValueByKey("CLAE05"))
                            Else
                                ShowMessage(StrError, getValueByKey("CLAE05"))
                            End If
                            Try
                                Throw New Exception(StrError & "line No 3070 cmd_savePrint_click")
                            Catch ex As Exception
                                LogException(ex)
                            End Try

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
                CtrlSalesPersons.CtrlTxtBox.Select()
                CtrlSalesPersons.AndroidSearchTextBox.Text = String.Empty
                'CtrlSalesPersons.CtrlTxtBox.Select()
                'CtrlSalesPersons.CtrlTxtBox.Focus()
                CtrlSalesPersons.AndroidSearchTextBox.Select()
                CtrlSalesPersons.AndroidSearchTextBox.Focus()
                Exit Sub
            End If
            Dim gridCOunt As Integer
            gridCOunt = dgMainGrid.Rows.Count
            Dim obj As New frmNItemSearch
            obj.ShowDialog()

            If Not obj.SearchResult Is Nothing Then
                'ShowEan = True
                Dim Quantity As String
                If dgMainGrid.Rows.Count > 1 Then
                    Quantity = dgMainGrid.Item(dgMainGrid.Row, "Quantity").ToString()
                   

                End If

                CtrlSalesPersons.CtrlTxtBox.Text = obj.ItemRow("ArticleCode").ToString()
                ' txtSearch_KeyDown(CtrlSalesPersons.CtrlTxtBox, New KeyEventArgs(Keys.Enter))
                If (Not String.IsNullOrEmpty(CtrlSalesPersons.CtrlTxtBox.Text.Trim()) AndAlso dgMainGrid.Rows.Count >= 1) Then
                    txtSearch_KeyDown(CtrlSalesPersons.CtrlTxtBox, New KeyEventArgs(Keys.Enter))
                End If

                If clsDefaultConfiguration.DineInProcess Then
                    If currentDineInTable > 0 Then
                        If dgMainGrid.Rows.Count > gridCOunt Or dgMainGrid.Item(dgMainGrid.Row, "Quantity") > Quantity Then
                            cmdGenerateKOT.Enabled = True
                        End If
                    End If
                End If
                'Dim frmuser As New frmNUserAuthorisation
                'frmuser.TaxPer = dtMainTax.Compute("sum(Value)", "")
                'If dsMain.Tables("CASHMEMODTL").Columns.Contains("TaxPer") = True Then
                '    For i = 0 To dsMain.Tables("CASHMEMODTL").Rows.Count - 1
                '        dsMain.Tables("CASHMEMODTL").Rows(i)("TaxPer") = dtMainTax.Compute("sum(Value)", "")
                '    Next
                'Else
                '    dsMain.Tables("CASHMEMODTL").Columns.Add("TaxPer", System.Type.GetType("System.String"))
                '    For i = 0 To dsMain.Tables("CASHMEMODTL").Rows.Count - 1
                '        dsMain.Tables("CASHMEMODTL").Rows(i)("TaxPer") = dtMainTax.Compute("sum(Value)", "")
                '    Next
                'End If

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub cmdCustomerinfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCustomerinfo.Click
        Try
            If OnlineConnect = False Then
                ShowMessage(getValueByKey("CMO50"), "CMO50 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If
            'Commented By Gaurav Danani
            'Dim objCust As New frmNSearchCustomer
            'objCust.ShowSO = False
            'objCust.BtnSearchCustomer.Enabled = True
            'objCust.txtCustomerCode.ReadOnly = False
            'objCust.ShowDialog()
            'Commented By Gaurav Danani

            'Commented By Sagar
            'Dim objCust As New frmNSearchCustomer
            'objCust.ShowSO = False
            'objCust.IsCustSearch = True
            'objCust.BtnSearchCustomer.Enabled = True
            'objCust.txtCustomerCode.ReadOnly = False
            'objCust.ShowDialog()
            'Dim dtCust As DataTable = objCust.dtCustmInfo()
            'dtHD = objCust.dtCustmInfo()
            'Commented By Sagar

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
                dtPrescDtlFastCashMemoAddToBill.Clear()
                dtPrescDtlFastCashMemoAddToBill = objCust.dtPrescDtlSrchAddToBill.Copy
                If dtPrescDtlFastCashMemoAddToBill.Rows.Count > 0 Then
                    For index = 0 To dtPrescDtlFastCashMemoAddToBill.Rows.Count - 1
                        _prescriptionArticleAmount = "0"
                        CtrlSalesPersons.CtrlTxtBox.Text = dtPrescDtlFastCashMemoAddToBill.Rows(index)("ArticleCode").ToString()
                        _prescriptionArticleAmount = dtPrescDtlFastCashMemoAddToBill.Rows(index)("Qty").ToString()
                        txtSearch_KeyDown(CtrlSalesPersons.CtrlTxtBox, New System.Windows.Forms.KeyEventArgs(Keys.Enter))
                        'CtrlSalesPersons.CtrlTxtBox.Text = clsDefaultConfiguration.DefaultSaleType
                        'txtSearch_KeyDown(sender, New System.Windows.Forms.KeyEventArgs(Keys.Enter))
                    Next
                End If
            End If
            type = objCust.AddressType
            custType = objCust.CustmType

            dtCust = objCust.dtCustmInfo()
            dtHD = objCust.dtCustmInfo()
            Dim CustomerNo = dtHD.Rows(0)("CUSTOMERNO").ToString()
            BindReservationGuestNo(CustomerNo)

            ''-- code added By Mahesh to change home delivery details ...
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
            'Commented By Sagar
            'Dim type As String = objCust.AddressType
            If Not dtCust Is Nothing AndAlso dtCust.Rows.Count > 0 Then
                If clsDefaultConfiguration.EvasPizzaChanges = True Then
                    Dim _RemComment As String = dtCust.Rows(0)("ReminderComments").ToString()
                    _RemComment = _RemComment.Replace("&", "&&").ToString()
                    If _RemComment <> String.Empty Or _RemComment <> Nothing Then
                        ShowMessage(_RemComment, "Information")
                    End If
                End If
                Dim dv As New DataView(dtCust, IIf(type Is Nothing, "", "isnull(AddressType,'')='" & type & "'"), "", DataViewRowState.CurrentRows)
                If dv.Count > 0 Then
                    If clsDefaultConfiguration.DineInProcess Then
                        ReCalculateCM(String.Empty)
                    Else
                        'lblCustSaleType.Visible = False
                        'CustSaleTypeTimer.Stop()
                    End If
                    calculateTotalbill()
                    customerType = dv.Item(0)("CustomerType").ToString()

                    If (custType = "CLP") Then
                        'CustInfo.CtrlTxtCustomerNo.Text = dv.Item(0)("CUSTOMERNO").ToString()
                        ' Dim da = ObjclsCommon.GetLastVisited(dv.Item(0)("CUSTOMERNO").ToString())
                        CustInfo.CtrlTxtCustomerNo.Text = dv.Item(0)("CUSTOMERNO").ToString()
                        If clsDefaultConfiguration.EvasPizzaChanges = True Then
                            CustInfo.CtrlTxtSwape.Text = dv.Item(0)("MobileNo").ToString()
                        Else
                            CustInfo.CtrlTxtSwape.Text = dv.Item(0)("CUSTOMERNO").ToString()
                        End If
                        ' CustInfo.CtrlLastVisit.Text = ObjclsCommon.GetLastVisited(dv.Item(0)("CUSTOMERNO")).Date
                        ' CustInfo.CtrlTxtSwape.Text = dv.Item(0)("CUSTOMERNO").ToString()
                        ' CustInfo.CtrltxtCustomerName.Text = dv.Item(0)("CustomerName").ToString()
                        'code added by vipul for showing customer name with level
                        If clsDefaultConfiguration.customerwisepricemanagement = True Then

                            If dv.Item(0)("Level").ToString().Equals("-") Then
                                CustInfo.CtrltxtCustomerName.Text = dv.Item(0)("CustomerName").ToString()
                            Else
                                CustInfo.CtrltxtCustomerName.Text = dv.Item(0)("CustomerName").ToString() + " - " + dv.Item(0)("Level").ToString()
                            End If


                        Else
                            CustInfo.CtrltxtCustomerName.Text = dv.Item(0)("CustomerName").ToString()
                        End If

                        If clsDefaultConfiguration.EvasPizzaChanges = True Then
                            CustInfo.ctrlTxtAddress.Enabled = True
                            CustInfo.ctrlTxtAddress.ReadOnly = False

                            CustInfo.ctrlTxtAddress.Value = dv.Item(0)("Address").ToString()
                            CustInfo.ctrlTxtAddress.Text = dv.Item(0)("Address").ToString()
                            Dim _AddressLine2City As String = ""
                            Dim _AddressLine2State As String = ""
                            Dim _AddressLine2Country As String = ""
                            Dim _PinCode As String = ""
                            If dv.Item(0)("City").ToString() <> String.Empty Then
                                _AddressLine2City = dv.Item(0)("City").ToString() + ","
                            End If
                            If dv.Item(0)("State").ToString() <> String.Empty Then
                                _AddressLine2State = dv.Item(0)("State").ToString() + ","
                            End If
                            If dv.Item(0)("Country").ToString() <> String.Empty Then
                                _AddressLine2Country = dv.Item(0)("Country").ToString()
                            End If
                            If dv.Item(0)("Pincode").ToString() <> String.Empty Then
                                _PinCode = ", Pincode-" + dv.Item(0)("Pincode").ToString()
                            End If
                            CustInfo.CtrlLastVisit.Text = _AddressLine2City + " " + _AddressLine2State + " " + _AddressLine2Country + _PinCode
                            ToolTip.SetToolTip(CustInfo.ctrlTxtAddress, CustInfo.ctrlTxtAddress.Text + " " + CustInfo.CtrlLastVisit.Text)
                            ToolTip.SetToolTip(CustInfo.CtrlLastVisit, CustInfo.ctrlTxtAddress.Text + " " + CustInfo.CtrlLastVisit.Text)
                            CustInfo.ctrlTxtAddress.ReadOnly = True
                        Else
                            CustInfo.CtrlLastVisit.Text = ObjclsCommon.GetLastVisited(dv.Item(0)("CUSTOMERNO")).Date
                            CustInfo.ctrlTxtAddress.Visible = False
                        End If
                        ' lblCity.Text = dv.Item(0)("CITY")
                        'lblBalPoint.Text = dv.Item(0)("BalancePoint")
                        CustomerBalancePoint = dv.Item(0)("BalancePoint").ToString()
                        CustInfo.ctrlTxtPoints.Text = dv.Item(0)("BalancePoint").ToString()
                        CLPCardType = IIf((dv.Item(0)("CARDTYPE") Is DBNull.Value), "", dv.Item(0)("CARDTYPE"))
                        CLPCustomerId = dv.Item(0)("CUSTOMERNO").ToString()
                        clpCustomerMobileNo = dv.Item(0)("Mobileno").ToString()
                        Dim objCustm As New clsCLPCustomer
                        'If clsDefaultConfiguration.IsCstTaxRequired AndAlso objCustm.CheckIfCstApplicable(clsAdmin.SiteCode, dv.Item(0)("STATE").ToString()) Then
                        '    DisplayCSTMessage()
                        'End If
                    Else
                        Dim da = ObjclsCommon.GetLastVisited(dv.Item(0)("CUSTOMERNO").ToString())
                        'CustInfo.CtrlTxtCustomerNo.Text = da.ToString("yyyy-MM-dd") ' dv.Item(0)("CUSTOMERNO").ToString()
                        CustInfo.CtrlTxtCustomerNo.Text = dv.Item(0)("CUSTOMERNO").ToString() 'da.ToString("yyyy-MM-dd") 
                        'CustInfo.CtrlTxtCustomerNo.Text = dv.Item(0)("CUSTOMERNO").ToString()
                        CustInfo.CtrltxtCustomerName.Text = dv.Item(0)("CustomerName").ToString()

                        CustInfo.ctrlTxtPoints.Text = String.Empty
                        CustInfo.CtrlTxtSwape.Text = String.Empty
                        CLPCardType = String.Empty
                    End If

                End If
            Else
                If clsDefaultConfiguration.EvasPizzaChanges = True Then
                    CustInfo.ctrlTxtAddress.Enabled = True
                    CustInfo.ctrlTxtAddress.ReadOnly = False
                    CustInfo.ctrlTxtAddress.Clear()
                    CustInfo.ctrlTxtAddress.Enabled = False
                End If
            End If
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType = False Then
                CtrlSalesPersons.CtrlTxtBox.Focus()
            Else
                CtrlSalesPersons.CtrlTxtBox.Select()
                CtrlSalesPersons.CtrlTxtBox.Focus()
            End If
            '


        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''Added by nikhil
    Dim dtMembDatapromo As DataTable
    Dim dtPrimaryGuest As New DataTable
    Private Sub BindReservationGuestNo(ByVal CardNo As String)
        Try
            Dim objHotelReservation As New clsHotelReservation

            dtPrimaryGuest = objHotelReservation.GetReservationGuest(CardNo)
            If dtPrimaryGuest IsNot Nothing Then
                objCM.ReservationGuestId = dtPrimaryGuest.Rows(0)("reservationID")
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ' Dim dtMembDatapromo As DataTable
    Private Sub CtrlTxtSwape_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'Created By Vaibhav
        Try
            If (e.KeyCode = Keys.Enter) Then
                If Not CustInfo.CtrlTxtSwape.Text = String.Empty Then
                    ' Dim CardNo As String = ReturnOnlyNumbersWhenCardSwipe(CustInfo.CtrlTxtSwape.Text)
                    Dim objCustm As New clsCLPCustomer
                    Dim dt As DataTable
                    Dim eventType As Int32
                    'CustInfo.CtrlTxtSwape.Text = String.Empty
                    CardNo = String.Empty
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
                        rbnGrpCMPromotion.Visible = False
                    Else
                        CardNo = CustInfo.CtrlTxtSwape.Text.Trim()
                    End If
                    '------Lee spa membership scan end

                    dt = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CardNo, CustomerWisePrice:=clsDefaultConfiguration.customerwisepricemanagement)
                    dtHD = dt.Copy()
                    If Not dt Is Nothing And dt.Rows.Count > 0 Then
                        AssignCustomerInfoData(dt, objCustm)
                        If clsDefaultConfiguration.EvasPizzaChanges = True Then
                            Dim _RemComment As String = dt.Rows(0)("ReminderComments").ToString()
                            _RemComment = _RemComment.Replace("&", "&&").ToString()

                            If CustomerSaleType = 2 Then
                                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                                    DisplayHomeDeliveryForm(dt)
                                End If
                            Else
                                If _RemComment <> String.Empty Or _RemComment <> Nothing Then
                                    ShowMessage(_RemComment, "Information")
                                End If
                            End If
                            'CustInfo.CtrlTxtSwape.Text = CardNo
                        End If
                    Else
                        CustInfo.CtrlTxtCustomerNo.Clear()
                        CustInfo.CtrltxtCustomerName.Clear()
                        CustInfo.ctrlTxtPoints.Clear()
                        CustInfo.CtrlTxtSwape.Clear()
                        CustInfo.CtrlTxtSwape.Focus()
                        CustInfo.CtrlLastVisit.Clear()
                        If clsDefaultConfiguration.EvasPizzaChanges = True Then
                            CustInfo.ctrlTxtAddress.Enabled = True
                            CustInfo.ctrlTxtAddress.ReadOnly = False
                            CustInfo.ctrlTxtAddress.Clear()
                            CustInfo.ctrlTxtAddress.Enabled = False
                        End If
                        ShowMessage("This customer is not exist. Do you want to create new customer?", "CM014 - " & getValueByKey("CLAE04"), eventType, "No", "Yes")
                        If eventType = 1 Then
                            If clsDefaultConfiguration.DetailedCustomerCreationformat = "0" Then
                                Dim objClpCustomer As New frmNSearchCustomer
                                objClpCustomer.CustomerNo = String.Empty
                                objClpCustomer.AccessCustomerOutside = True
                                objClpCustomer.ShowSO = False
                                objClpCustomer.ShowCLP = True
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

    Private Sub CtrlTxtCustomerNo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'Created By Vaibhav
        Try
            If (e.KeyCode = Keys.Enter) Then
                If Not String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text) Then
                    Dim objCustm As New clsCLPCustomer
                    Dim dt As DataTable
                    dt = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CustInfo.CtrlTxtCustomerNo.Text, CustomerWisePrice:=clsDefaultConfiguration.customerwisepricemanagement)
                    AssignCustomerInfoData(dt, objCustm)
                End If
                'CtrlSalesPersons.CtrlTxtBox.Focus()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub DisplayCSTMessage()
        Dim EventType As Int32
        ShowMessage("Click Yes to apply CST, all the discounts will be removed", getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
        If EventType = 1 Then
            IsCSTApplicable = True
            ClearDiscountAndExistingTax()
        End If
    End Sub

    Dim tooltip As New ToolTip
    Private Sub AssignCustomerInfoData(ByVal dt As DataTable, ByRef objCustm As clsCLPCustomer)
        Try
            If Not dt Is Nothing And dt.Rows.Count > 0 Then
                CustInfo.CtrlTxtCustomerNo.Value = dt.Rows(0)("CUSTOMERNO").ToString() 'da.ToString("yyyy-mm-dd")
                CustInfo.CtrlTxtCustomerNo.Text = dt.Rows(0)("CUSTOMERNO").ToString()

                ' CustInfo.CtrltxtCustomerName.Text = dt.Rows(0)("CUSTOMERNAME").ToString()
                'code added by vipul for showing customer name with level
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
                'CustInfo.CtrlLastVisit.Text = ObjclsCommon.GetLastVisited(dt.Rows(0)("CUSTOMERNO")).Date
                If (dt.Rows(0)("CustomerType") = "CLP") Then
                    If clsDefaultConfiguration.EvasPizzaChanges = False Then
                        CustInfo.CtrlTxtSwape.Text = dt.Rows(0)("AccountNo").ToString()
                    End If
                    CustInfo.ctrlTxtPoints.Text = dt.Rows(0)("BALANCEPOINT").ToString()
                    CLPCardType = IIf((dt.Rows(0)("CARDTYPE") Is DBNull.Value), "", dt.Rows(0)("CARDTYPE"))
                    CustomerBalancePoint = dt.Rows(0)("BalancePoint").ToString()
                    'code commented by vipul for issue id 2791
                    ' CustInfo.ctrlTxtAddress.Visible = False
                Else
                    CustInfo.ctrlTxtPoints.Clear()
                    CustInfo.CtrlTxtSwape.Clear()
                End If
                customerType = dt.Rows(0)("CustomerType")
                clpCustomerMobileNo = IIf((dt.Rows(0)("Mobileno") Is DBNull.Value), "", dt.Rows(0)("Mobileno"))
                CLPCustomerId = dt.Rows(0)("CUSTOMERNO").ToString()

                If clsDefaultConfiguration.EvasPizzaChanges = True Then
                    CustInfo.ctrlTxtAddress.Enabled = True
                    CustInfo.ctrlTxtAddress.ReadOnly = False

                    CustInfo.ctrlTxtAddress.Value = dt.Rows(0)("Address").ToString()
                    CustInfo.ctrlTxtAddress.Text = dt.Rows(0)("Address").ToString()
                    CustInfo.CtrlTxtSwape.Text = ""
                    CustInfo.CtrlTxtSwape.Text = dt.Rows(0)("MobileNo").ToString()
                    Dim _AddressLine2City As String = ""
                    Dim _AddressLine2State As String = ""
                    Dim _AddressLine2Country As String = ""
                    Dim _PinCode As String = ""
                    If dt.Rows(0)("City").ToString() <> String.Empty Then
                        _AddressLine2City = dt.Rows(0)("City").ToString() + ","
                    End If
                    If dt.Rows(0)("State").ToString() <> String.Empty Then
                        _AddressLine2State = dt.Rows(0)("State").ToString() + ","
                    End If
                    If dt.Rows(0)("Country").ToString() <> String.Empty Then
                        _AddressLine2Country = dt.Rows(0)("Country").ToString()
                    End If
                    If dt.Rows(0)("Pincode").ToString() <> String.Empty Then
                        _PinCode = ", Pincode-" + dt.Rows(0)("Pincode").ToString()
                    End If
                    CustInfo.CtrlLastVisit.Text = _AddressLine2City + " " + _AddressLine2State + " " + _AddressLine2Country + _PinCode
                    tooltip.SetToolTip(CustInfo.ctrlTxtAddress, CustInfo.ctrlTxtAddress.Text + " " + CustInfo.CtrlLastVisit.Text)
                    tooltip.SetToolTip(CustInfo.CtrlLastVisit, CustInfo.ctrlTxtAddress.Text + " " + CustInfo.CtrlLastVisit.Text)
                    CustInfo.ctrlTxtAddress.ReadOnly = True
                Else
                    CustInfo.CtrlLastVisit.Text = ObjclsCommon.GetLastVisited(dt.Rows(0)("CUSTOMERNO")).Date
                End If
                CtrlSalesPersons.CtrlTxtBox.Focus()
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
                'ShowMessage(getValueByKey("SC001"), "SC001 - " & getValueByKey("CLAE04"))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub clearCustomerInfo()
        CustInfo.CtrlTxtCustomerNo.Clear()
        CustInfo.CtrltxtCustomerName.Clear()
        CustInfo.ctrlTxtPoints.Clear()
        CustInfo.CtrlTxtSwape.Clear()
        CustInfo.CtrlTxtSwape.Select()
        CustInfo.CtrlLastVisit.Clear()
        CLPCardType = String.Empty
        CLPCustomerId = String.Empty
        IsCSTApplicable = False
        If clsDefaultConfiguration.IsMembership Then
            CustInfo.CtrlTxtCustomerNo.ReadOnly = False
            CustInfo.CtrlTxtSwape.ReadOnly = False
        End If
        If clsDefaultConfiguration.EvasPizzaChanges = True Then
            CustInfo.ctrlTxtAddress.Enabled = True
            CustInfo.ctrlTxtAddress.ReadOnly = False
            CustInfo.ctrlTxtAddress.Clear()
            CustInfo.ctrlTxtAddress.ReadOnly = True
        End If
        '''---- Clear Home Delivery too ----Added By Mahesh 
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

        End If
        If (dtHD IsNot Nothing AndAlso dtHD.Rows.Count > 0) Then dtHD.Clear()
        CtrlSalesPersons.CtrlTxtBox.Focus()
        If clsDefaultConfiguration.DineInProcess Then
            ReCalculateCM(String.Empty)
        Else
            'CustomerSaleType = 0
            'lblCustSaleType.Visible = False
            'CustSaleTypeTimer.Stop()
        End If
        calculateTotalbill()
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
                Dim dt As DataTable = objReturn.GetResultData()
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        Dim dv As New DataView(dsMain.Tables("CashMemoDtl"), "Btype='R' And Ean='" & dr("Ean").ToString() & "' AND ReturnCMNo='" & dr("ReturnCMNo").ToString() & "'", "", DataViewRowState.CurrentRows)
                        If dv.Count > 0 Then
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
                            dsMain.Tables("CASHMEMODTL").ImportRow(dr)
                        End If
                    Next
                    CreatingLineNO(dsMain, "CASHMEMODTL")
                    For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Select("Btype='R'", "Ean", DataViewRowState.CurrentRows)
                        If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then

                            CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt") - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT")), dr, dr("EAN"))


                        Else

                            CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt"), dr, dr("EAN"))


                        End If
                    Next
                End If
                GridSettings(UpdateFlag)
                calculateTotalbill()
            End If
            CtrlSalesPersons.CtrlTxtBox.Focus()

        Catch ex As Exception
            ShowMessage(getValueByKey("CM026"), "CM026 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in Updating return data into Main", "Error")
        End Try
    End Sub
    Private Sub cmdClrAllPromo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClrAllPromo.Click
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
                ' Dim taxableAmount = dr("MRP") - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT"))
                'passing taxableamount as gross amt- discount 
                Dim taxableAmount = dr("GrossAmt") - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT"))

                If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), taxableAmount, dr, dr("EAN"))
                    'If IsInclusiveTax Then
                    '    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt") - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT")), dr, dr("EAN"), isInclusiveCalcReq:=IsInclusiveTax)
                    'Else
                    '    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt") - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT")), dr, dr("EAN"))
                    'End If
                    'Dim perunittax = Math.Round((dr("TOTALTAXAMOUNT") / dr("Quantity")), clsDefaultConfiguration.DecimalPlaces)
                    'dr("TOTALTAXAMOUNT") = perunittax * dr("Quantity")
                Else
                    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), taxableAmount, dr, dr("EAN"))
                    'If IsInclusiveTax Then
                    '    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt"), dr, dr("EAN"), True)
                    'Else
                    '    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt"), dr, dr("EAN"))
                    'End If
                    'Dim perunittax = Math.Round((dr("TOTALTAXAMOUNT") / dr("Quantity")), clsDefaultConfiguration.DecimalPlaces)
                    'dr("TOTALTAXAMOUNT") = perunittax * dr("Quantity")
                End If
            Next
            IsRoundOffMsg = False
            ReCalculateCM("")
            calculateTotalbill()
            IsDefaultPromotion = False
            If clsDefaultConfiguration.ExtendScreen Then
                If clsDefaultConfiguration.HariOmExtendScreen = True Then
                    TwoLineExtendScreen()
                End If
            End If
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
                    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt") - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT")), dr, dr("EAN"))
                Else
                    CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt"), dr, dr("EAN"))

                End If
            Next
            ReCalculateCM("")
            calculateTotalbill()
            IsDefaultPromotion = False
            If clsDefaultConfiguration.ExtendScreen Then
                If clsDefaultConfiguration.HariOmExtendScreen = True Then
                    TwoLineExtendScreen()
                End If
            End If
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
            'obj.IsInclusiveTax = IsInclusiveTax
            obj.MainTable = "CASHMEMODTL"
            obj.ExclusiveTaxFieldName = "EXCLUSIVETAX"
            obj.TotalDiscountField = "TOTALDISCOUNT"
            obj.GrossAmtField = "GROSSAMT"
            obj.Condition = "BTYPE='S'"
            'If MsgBox(getValueByKey  ("CM048"), MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2, "CM048") = MsgBoxResult.Yes Then
            If UCase(sender.id) = UCase("cmdApplySelectPromo") Then

                'If IsDefaultPromotion Then
                '    If MsgBox(getValueByKey("SO094"), MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2, "Information") = MsgBoxResult.Yes Then
                'ClearAllPromo()
                '(sender.ToString() = "ClearPromWithoutMessage") 
                Dim p As Object = "ClearPromWithoutMessage"
                'code commented for issue id 1532
                ' cmdClrAllPromo_Click(p, Nothing)
                'Else
                '    Exit Sub
                'End If
                '    End If
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
                            cmdClrAllPromo_Click(p, Nothing)
                        End If
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

                                'ClearAllPromo() 'add for bug no 0001106
                                Dim dtReason As DataTable
                                Dim objReturn As New clsCashMemoReturn
                                dtReason = objReturn.GetReason("CMS")
                                If dsMain.Tables("CASHMEMOHDR").Rows.Count = 0 Then
                                    Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
                                End If
                                Dim authUserRemarks As String = ShowReason(dtReason)
                                'If String.IsNullOrEmpty(authUserRemarks) Then
                                '    Exit Sub
                                'Else
                                dsMain.Tables("CashMemoHdr").Rows(0)("AuthUserRemarks") = authUserRemarks
                                '  End If
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
                            ElseIf StrQues.Contains("Autho") = True Then
                                If CheckInterTransactionAuth("DAUTH", dsMain.Tables("CashMemodtl"), 0, 0, 0, offerno, False, "", "", 0, _remarks) = True Then
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
                '   ClearAllPromo()
                CustInfo.CtrlTxtCustomerNo.Value = custnumber
                CustInfo.CtrlTxtCustomerNo.Text = custnumber
                'added by Khusrao Adil 
                'for JK sprint 14 
                If clsDefaultConfiguration.PromotionBasedOn = "ApplicationDate" Then
                    obj.CalculatedDs(dsMain, clsAdmin.SiteCode, clsAdmin.DayOpenDate)
                Else
                    obj.CalculatedDs(dsMain, clsAdmin.SiteCode, CustWisePrice:=clsDefaultConfiguration.customerwisepricemanagement, CustNo:=custnumber, ArticelCode:=ArticleCode)
                End If
                IsDefaultPromotion = True
            End If

            'For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Select("Btype='S'", "Ean", DataViewRowState.CurrentRows)
            '    'If dr("TOTALDISCOUNT") <= 0 Then Continue For
            '    If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then

            '        If (Val(dr("TOTALDISCOUNT").ToString()) > 0) Then
            '            CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt") - IIf(dr("TOTALDISCOUNT") Is DBNull.Value, 0, dr("TOTALDISCOUNT")), dr, dr("EAN"), isInclusiveCalcReq:=IsInclusiveTax)
            '        Else
            '            'CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode"), dr("GrossAmt"), dr, dr("EAN"), isInclusiveCalcReq:=IsInclusiveTax)
            '        End If

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
            If clsDefaultConfiguration.ExtendScreen = True Then
                If clsDefaultConfiguration.HariOmExtendScreen = True Then
                    TwoLineExtendScreen()
                End If
            End If
            If clsDefaultConfiguration.IsPoleDisply = True Then
                TwoLineExtendScreen()
            End If

            CtrlSalesPersons.CtrlTxtBox.Focus()
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
            CtrlSalesPersons.CtrlTxtBox.Focus()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdAdvanceSale_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sellGiftVoucher.Click
        Try
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType AndAlso CustomerSaleType = 0 Then Exit Sub
            If clsDefaultConfiguration.SalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0 Then
                ShowMessage(getValueByKey("CM025"), "CM025 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please Select the Sales Person", "information")
                CtrlSalesPersons.CtrlSalesPersons.Select()
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

            If (dgMainGrid.Rows.Count > 1) Then
                dgMainGrid.Select(1, 1)
            End If

            'CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Select()

        Catch ex As Exception
            LogException(ex)
        End Try
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
            obj.Email = dtCust.Rows(0)("EMAILID").ToString()
            obj.MobileNo = dtCust.Rows(0)("MOBILENO").ToString()
            obj.TelNo = dtCust.Rows(0)("RESPHONE").ToString()
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
                If clsDefaultConfiguration.EvasPizzaChanges = True Then
                    Dim _RemComment As String = dtCust.Rows(0)("ReminderComments").ToString()
                    _RemComment = _RemComment.Replace("&", "&&").ToString()
                    If _RemComment <> String.Empty Or _RemComment <> Nothing Then
                        ShowMessage(_RemComment, "Information")
                    End If
                End If
                AssignCustomerInfoData(dtCust, objCustm)

                'If Not obj.txtRemark.Text = String.Empty Then
                '----Uncomment By Mahesh When I want Edit need to do EndCurrentEdit()
                Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()

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
                dsMain.Tables("CASHMEMOHDR").Rows(0)("DeliveryPartnerId") = obj.delieverypartnerid
                If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType Then
                    dsMain.Tables("CashMemoHdr").Rows(0)("ServiceTaxAmount") = 2
                    CustomerSaleType = enumCustomerSaleType.Home_Delivery
                End If
                HDMobileNo = obj.MobileNo

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



        Catch ex As Exception
            LogException(ex)
        End Try
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
    Private Sub cmdHomeDelivery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles homeDelivery.Click
        Try
            IsHoldEnterKey = False
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
                Dim dsFloatAmt As DataSet
                Dim Billno As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("BillNO").ToString()
                Dim BillDate As DateTime
                If (IsDBNull(dsMain.Tables("CASHMEMOHDR").Rows(0)("BillDate"))) Then
                    BillDate = clsAdmin.CurrentDate
                Else
                    BillDate = dsMain.Tables("CASHMEMOHDR").Rows(0)("BillDate")
                End If
                If UpdateFlag = True Then
                    dsFloatAmt = objCM.GetVoucherFloatData(Billno, BillDate)
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
                    Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("DeliveryPersonID") = obj.DeliveryPersonID
                    obj.Dispose()
                    SearchCustomerForHomeDelivery()

                ElseIf dialogResult = Windows.Forms.DialogResult.OK AndAlso Not obj.txtName.Text = String.Empty Then
                    Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("HDDeliveryDate") = obj.DeliveryDate
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("HDName") = obj.HdName.ToString()
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("HDAddress") = obj.Address1 + " ;" + obj.Address2 + " ;" + obj.Address3 + " ;" + obj.Address4
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("HDEmail") = obj.Email.ToString()
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("HDTelNo") = obj.TelNo.ToString()
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("HDRemark") = obj.Remark.ToString()
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("DeliveryPersonID") = obj.DeliveryPersonID.ToString()
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("SalesExecutiveCode") = obj.DeliveryPersonID.ToString()
                    Dim _deliverparterid As String = ""
                    If obj.delieverypartnerid <> Nothing Then
                        _deliverparterid = obj.delieverypartnerid.ToString()
                    End If
                    dsMain.Tables("CASHMEMOHDR").Rows(0)("DeliveryPartnerId") = _deliverparterid
                    If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType Then
                        dsMain.Tables("CashMemoHdr").Rows(0)("ServiceTaxAmount") = 2
                        CustomerSaleType = enumCustomerSaleType.Home_Delivery
                    End If
                    FloatAmt = obj.FloatAmt
                    If obj.IsUpdateCustomerAddress And Not String.IsNullOrEmpty(obj.CustomerNo) Then
                        Dim objCLPCustm As New clsCLPCustomer
                        objCLPCustm.UpdateCustomerAddress(obj.CustomerNo, clsAdmin.CLPProgram, obj.Address1.ToString(), obj.Address2.ToString(),
                                                          obj.Address3.ToString(), obj.Address4.ToString(),
                                                          obj.MobileNo.ToString(), obj.TelNo.ToString(),
                                                          obj.Email.ToString(), clsDefaultConfiguration.AllowMobileNoEditable)

                    End If

                    If Not String.IsNullOrEmpty(obj.HdName) Then
                        lblCustSaleType.Visible = True
                        lblCustSaleType.Text = "Home Delivery"
                        CustomerSaleType = enumCustomerSaleType.Home_Delivery
                        dsMain.Tables("CashMemoHdr").Rows(0)("ServiceTaxAmount") = 2
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

    Private Sub cmdCustomerSearchAndLoad(Optional ByVal saleType As String = "")
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


    Dim IsBillVoid As Boolean = False
    Public Sub cmdOldCashMemo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOldCashMemo.Click
        Try

            If sender IsNot Nothing Then
                If DirectCast(sender, C1.Win.C1Ribbon.RibbonButton).ID = "cmdOldCashMemo" Then
                    objSearch.RequestFromPage = enumOperationOnBill.EditBill
                End If
            End If

            If UpdateFlag = False AndAlso dsMain.Tables("cashMemodtl").Rows.Count > 0 Then
                If MsgBox(getValueByKey("CM029"), MsgBoxStyle.YesNo, "CM029 - " & getValueByKey("CLAE04")) = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If
            objCM.ShiftManagementForCM = clsDefaultConfiguration.ShiftManagement
            Dim dtshift As New DataTable
            dtshift = clsCommon.GetNextShiftID(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
            Dim dt As DataTable = objCM.GetOldCashMemo(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID, clsAdmin.UserCode, clsCommon._PrevShiftId, NoOfDaysForReprint:=clsDefaultConfiguration.NumberOfDaysApplicableForReprint, NewCustmor:=clsDefaultConfiguration.DetailedCustomerCreationformat)
            'Dim dt As DataTable = objCM.GetOldCashMemo(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                'Dim objSearch As New frmNCommonSearch
                objSearch.SetData = dt
                ' objSearch.ShowDialog()
                Dim DialogResult = objSearch.ShowDialog()

                If DialogResult = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                End If

                If Not objSearch.search Is Nothing AndAlso objSearch.search.Length > 0 Then
                    'commented for void bill
                    CtrlSalesPersons.CtrlTxtBox.Enabled = False
                    CtrlSalesPersons.AndroidSearchTextBox.Enabled = False
                    'CtrlSalesPersons.CtrlCmdSearch.Enabled = False
                    'CtrlSalesPersons.CtrlSalesPersons.Enabled = False
                    CtrlSalesPersons.CtrlCmdSearch.Enabled = True
                    CtrlSalesPersons.CtrlSalesPersons.Enabled = True
                    GetCashMemoDetails(objSearch.search(0), clsAdmin.SiteCode)
                    'ShowLastOper(DvItemDetail(0)("EAN").ToString(), DvItemDetail(0)("Discription").ToString(), DvItemDetail(0)("Quantity").ToString())
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

                    calculateTotalbill()
                    UpdateFlag = True
                    GridSettings(UpdateFlag)
                    PaymentGridSetting()
                    lblCMNo.Text = objSearch.search(0)
                    'lblCMDate.Text = objSearch.search(4)
                    Payment.Visible = True
                    cmdCard.Enabled = False
                    cmdCash.Enabled = False

                    cmdCheque.Enabled = False
                    CMbtnBottom.CtrlBtnSaleGV.Enabled = False
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

                    cmdCustomerinfo.Enabled = False
                    cmdDefaultPromo.Enabled = False
                    cmdClrAllPromo.Enabled = False
                    cmdClearSelectedPromo.Enabled = False
                    cmdApplySelectPromo.Enabled = False

                    cmdCreditSale.Enabled = False
                    sellGiftVoucher.Enabled = False
                    homeDelivery.Enabled = False
                    CtrlMODMenu1.Enabled = False
                    CtrlNumberPad1.Enabled = False
                    CustInfo.BtnClearCustmInfo.Enabled = False
                    CustInfo.CtrlTxtCustomerNo.ReadOnly = True
                    CustInfo.CtrlTxtSwape.ReadOnly = True

                    'Rakesh-19.09.2013> Mantis isuue -7483 
                    cmdHold.Enabled = False
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
                        homeDelivery.Enabled = True
                        EditMode_IsupdateDeliveryPersonAllowed = True
                    Else
                        homeDelivery.Enabled = False
                        EditMode_IsupdateDeliveryPersonAllowed = False
                    End If
                Else
                    IsBillVoid = True
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
            '' AS per Version 2013
            'Dim index As Int32 = dgMainGrid.Cols("Quantity").Index
            'dgMainGrid_AfterEdit(dgMainGrid, New C1.Win.C1FlexGrid.RowColEventArgs(dgMainGrid.Row, index))
            If UpdateFlag = True Then
                '---- Comments by Mahesh its asked while Edit now no need to ask again for authorization ...
                'If CheckInterTransactionAuth("UpdateBill", dsMain.Tables("CashMemoHdr"), 0, 0, 0, "") = False Then
                '    Exit Sub
                'End If
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
                'Issue ID 7625
                If dsMain.Tables("CASHMEMODTL").Select("BTYPE='S'  AND isnull(TotalDiscount,0)='0'", "", DataViewRowState.CurrentRows).Length = dsMain.Tables("CASHMEMODTL").Rows.Count Then
                    isCashierPromoSelected = False
                End If
                'End Issue ID 7625
                Dim objPayment As frmNAcceptPaymentPC

                CLP_Data.Sitecode = clsAdmin.SiteCode
                getclpsettings()
                If Not CLP_Data.CLPConfigdata.Tables("CLPHeader") Is Nothing AndAlso CLP_Data.CLPConfigdata.Tables("CLPHeader").Rows.Count > 0 AndAlso CLP_Data.CLPConfigdata.Tables("CLPHeader")(0)("RedemptionType").ToString().ToLower() = "rdt1" Then
                    objPayment = New frmNAcceptPaymentPC(dsMain.Tables("CASHMEMODTL"), True)
                Else
                    objPayment = New frmNAcceptPaymentPC(True)
                End If

                objPayment.IsFastCashMemo = True
                objPayment.TxtRemark.Text = _remarks
                objPayment.TotalBillAmount = CashSummary.CtrllblNetAmt.Text
                objPayment.ParentRelation = "CashMemo"
                objPayment._IsCashierPromoSelected = isCashierPromoSelected

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
                objPayment.TopMost = True
                objPayment.RoundAt = clsDefaultConfiguration.BillRoundOffAt
                C1Ribbon1.Visible = False
                If clsDefaultConfiguration.EvasPizzaChanges = True Then
                    objPayment.TxtRemark.Text = ValueChangeRemark
                End If

                Dim dtCustomer As New DataTable   '' Vipin PC SO Merge 16.05.2018
                dtCustomer = ObjclsCommon.GetCustomerDetails(CustInfo.CtrlTxtCustomerNo.Text.Trim(), clsAdmin.SiteCode)
                If Not dtCustomer Is Nothing AndAlso dtCustomer.Rows.Count > 0 Then
                    objPayment.CustName = dtCustomer.Rows(0)("NameOnCard").ToString
                    objPayment.CompName = dtCustomer.Rows(0)("CompanyName").ToString
                    objPayment.MobNumber = dtCustomer.Rows(0)("Mobileno").ToString
                End If
                If CustInfo.CtrlTxtCustomerNo.Text <> String.Empty Then
                    objPayment.CLPCustomerCardNumber = CLPCustomerId
                    objPayment.CLPCustomerName = CustInfo.CtrltxtCustomerName.Text
                End If

                objPayment.ShowDialog(Me)

                'added by vipin 15.11.2017------------------------------------------------------
                Dim Type As String = objPayment._Addresstype
                Dim custType As String = objPayment._custType
                Dim dtCust As DataTable = objPayment._dtCust
                If Not dtCust Is Nothing Then
                    If dtCust.Rows.Count > 0 Then
                        ''-- code added By Mahesh to change home delivery details ...
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
                        'Commented By Sagar
                        'Dim type As String = objCust.AddressType
                        If Not dtCust Is Nothing AndAlso dtCust.Rows.Count > 0 Then
                            Dim dv As New DataView(dtCust, IIf(Type Is Nothing, "", "isnull(AddressType,'')='" & Type & "'"), "", DataViewRowState.CurrentRows)
                            If dv.Count > 0 Then
                                If clsDefaultConfiguration.DineInProcess Then
                                    ReCalculateCM(String.Empty)
                                Else
                                    'lblCustSaleType.Visible = False
                                    'CustSaleTypeTimer.Stop()
                                End If
                                calculateTotalbill()
                                customerType = dv.Item(0)("CustomerType").ToString()

                                If (custType = "CLP") Then
                                    'CustInfo.CtrlTxtCustomerNo.Text = dv.Item(0)("CUSTOMERNO").ToString()
                                    ' Dim da = ObjclsCommon.GetLastVisited(dv.Item(0)("CUSTOMERNO").ToString())
                                    CustInfo.CtrlTxtCustomerNo.Text = dv.Item(0)("CUSTOMERNO").ToString()
                                    CustInfo.CtrlLastVisit.Text = ObjclsCommon.GetLastVisited(dv.Item(0)("CUSTOMERNO")).Date
                                    CustInfo.CtrlTxtSwape.Text = dv.Item(0)("CUSTOMERNO").ToString()
                                    CustInfo.CtrltxtCustomerName.Text = dv.Item(0)("CustomerName").ToString()
                                    ' lblCity.Text = dv.Item(0)("CITY")
                                    'lblBalPoint.Text = dv.Item(0)("BalancePoint")
                                    CustomerBalancePoint = dv.Item(0)("BalancePoint").ToString()
                                    CustInfo.ctrlTxtPoints.Text = dv.Item(0)("BalancePoint").ToString()
                                    CLPCardType = IIf((dv.Item(0)("CARDTYPE") Is DBNull.Value), "", dv.Item(0)("CARDTYPE"))
                                    CLPCustomerId = dv.Item(0)("CUSTOMERNO").ToString()
                                    clpCustomerMobileNo = dv.Item(0)("Mobileno").ToString()
                                    Dim objCustm As New clsCLPCustomer
                                    'If clsDefaultConfiguration.IsCstTaxRequired AndAlso objCustm.CheckIfCstApplicable(clsAdmin.SiteCode, dv.Item(0)("STATE").ToString()) Then
                                    '    DisplayCSTMessage()
                                    'End If
                                Else
                                    Dim da = ObjclsCommon.GetLastVisited(dv.Item(0)("CUSTOMERNO").ToString())
                                    CustInfo.CtrlTxtCustomerNo.Text = dv.Item(0)("CUSTOMERNO").ToString() 'da.ToString("yyyy-MM-dd") 
                                    'CustInfo.CtrlTxtCustomerNo.Text = dv.Item(0)("CUSTOMERNO").ToString()
                                    CustInfo.CtrltxtCustomerName.Text = dv.Item(0)("CustomerName").ToString()

                                    CustInfo.ctrlTxtPoints.Text = String.Empty
                                    CustInfo.CtrlTxtSwape.Text = String.Empty
                                    CLPCardType = String.Empty
                                End If

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

                        'End If
                        ' vipin Edi bill paymet error
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
                End If

                '------------------------------------------------------------------
                ''added By ketan Savoy Outstanding changes
                PaymentTermId = objPayment.PaymentTermNameId
                If True Then
                    _billAmt = objPayment.TotalBillAmount
                    _paidAmt = objPayment.TotalCustomerPadiAmount
                End If

                C1Ribbon1.Visible = True
                If objPayment.IsCancelAcceptPayment = False Then
                    If objPayment.DialogResult <> Windows.Forms.DialogResult.Cancel Then
                        'Added by Sagar
                        Dim TempCardNo As String = ""
                        If clsDefaultConfiguration.PayFromInnoviti AndAlso clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) Then
                            If objPayment.resonseInnoviti IsNot Nothing Then
                                cashmemoInnovitiList.Clear()
                                cashmemoInnovitiList = objPayment.resonseInnoviti
                                TempCardNo = cashmemoInnovitiList("CardNumber")
                            End If
                        End If
                        'Added by Rohit for CR5938
                        _dDueDate = objPayment.dDueDate
                        _strRemarks = objPayment.strRemarks
                        _remarks = objPayment.TxtRemark.Text

                        ds = objPayment.ReciptTotalAmount()
                        'added by khusrao adil for innovati on 18-05-2017
                        If clsDefaultConfiguration.PayFromInnoviti AndAlso clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) Then
                            If ds.Tables.Contains("MSTRecieptType") Then
                                If ds.Tables("MSTRecieptType").Rows.Count > 0 Then

                                    'For i = 0 To ds.Tables("MSTRecieptType").Rows.Count - 1
                                    '    If ds.Tables("MSTRecieptType").Rows(i)("RecieptType") <> "" Or ds.Tables("MSTRecieptType").Rows(i)("RecieptType") <> Nothing Or _
                                    '        ds.Tables("MSTRecieptType").Rows(i)("RecieptTypeCode") <> "" Or ds.Tables("MSTRecieptType").Rows(i)("RecieptTypeCode") <> Nothing Then
                                    '        ds.Tables("MSTRecieptType").Rows(i).Delete()
                                    '    End If
                                    'Next
                                    '   ds.Tables("MSTRecieptType").AcceptChanges()
                                    For Each dr As DataRow In ds.Tables("MSTRecieptType").Rows
                                        ' For Each Row As DataRow In ds.Tables("MSTRecieptType").Rows
                                        If dr.RowState <> DataRowState.Deleted Then
                                            '    StrFilter = StrFilter & Row("SRNO").ToString() & ","
                                            If dr("RecieptTypeCode") = "CreditCard" Then
                                                If TempCardNo <> "" Or TempCardNo <> Nothing Then
                                                    dr("Number") = TempCardNo
                                                End If
                                            End If
                                        End If
                                        'Next
                                    Next
                                End If
                            End If
                        End If


                        cmdLoyalty_Click(sender, e, ds.Tables("MSTRecieptType"))

                        If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
                            UpdatePaymentDataSetStru(ds, UpdateFlag)
                            ''added By ketan Savoy Outstanding changes 
                            If clsDefaultConfiguration.IsSavoy Then
                                UpdateSaleOrderTermNConditionsStru(ds, UpdateFlag)
                            End If
                            cmdHold.Enabled = False
                            'If Not String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text) Then
                            '    Dim objClpCustomer As New clsCLPCustomer
                            '    Dim clpPointInfo As DataTable = objClpCustomer.GetClpPointsForaCardNumber(CustInfo.CtrlTxtCustomerNo.Text)
                            '    If clpPointInfo Is Nothing AndAlso clpPointInfo.Rows.Count > 0 Then
                            '        objClpCustomer.UpdateClpPointsInfo(CustInfo.CtrlTxtCustomerNo.Text, clsAdmin.CLPProgram, CDbl(objPayment.CtrlCLPPoint.lblPointsValue.Text))
                            '    End If
                            'End If
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
            ShowMessage(getValueByKey("CM031"), "CM031 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in updating the payment data to main", "Error")
        End Try

    End Sub
    Private Sub cmdCheque_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCheque.Click
        Try
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType Then
                If CustomerSaleType = enumCustomerSaleType.Home_Delivery Then
                    If CustInfo.CtrlTxtSwape.Text.Trim = "" Then
                        ShowMessage(getValueByKey("Crs015"), "CM030 - " & getValueByKey("CLAE05"))
                        Exit Sub
                    End If
                End If
            End If
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType Then
                If CustomerSaleType = 0 Then Exit Sub
                Dim CustSaleType As String = IIf(CustomerSaleType = enumCustomerSaleType.Dine_In, "DINEIN", "TAKEAWAY")
                If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then
                    cmdCustomerSearchAndLoad(CustSaleType)
                End If
                If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then Exit Sub
            End If
            'Dim index As Int32 = dgMainGrid.Cols("Quantity").Index
            'dgMainGrid_AfterEdit(dgMainGrid, New C1.Win.C1FlexGrid.RowColEventArgs(dgMainGrid.Row, index))
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

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdCash_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCash.Click
        'Try
        '    Dim objCash As New frmSpecialPrompt("Enter Amount ")
        '    objCash.lblMessage.Text = " Bill Amount is " & CDbl(CashSummary.CtrllblNetAmt.Text)
        '    objCash.ShowTextBox = True
        '    objCash.lblMessage.Visible = True
        '    objCash.ShowDialog()
        '    Dim Value As Double = objCash.GetResult
        '    objCash.Dispose()
        '    If Value > 0 Then
        '        If Value > CDbl(CashSummary.CtrllblNetAmt.Text) Then
        '            Dim ReturnAmt As Double = Value - CDbl(CashSummary.CtrllblNetAmt.Text)
        '            Dim strshowmsg As String = ""
        '            strshowmsg = "Bill Amt to Pay " & CDbl(CashSummary.CtrllblNetAmt.Text) & vbLf
        '            strshowmsg = strshowmsg & "Customer Paid " & Value & vbLf
        '            strshowmsg = strshowmsg & "Return to Customer " & ReturnAmt & " Amount" & vbLf
        '            ShowMessage(strshowmsg, "Information")
        '        End If
        '        If Value < CDbl(CashSummary.CtrllblNetAmt.Text) Then
        '            ShowMessage(getValueByKey  ("CM032"), "CM032")
        '            'ShowMessage("Amount is not Settle ", "Information")
        '            Exit Sub
        '        End If
        '        Dim objPayment As New frmNAcceptPayment()
        '        objPayment.Show()
        '        objPayment.TotalBillAmount = CashSummary.CtrllblNetAmt.Text
        '        objPayment.Enabled = False
        '        objPayment.cboRecieptType.SelectedValue = "Cash"
        '        objPayment.TotalBillAmount = CashSummary.CtrllblNetAmt.Text
        '        'objPayment.cboCurrency.SelectedIndex = 1
        '        objPayment.InsertReceiptCashDetails()
        '        Dim ds As DataSet = objPayment.ReciptTotalAmount()
        '        objPayment.Close()
        '        If Not ds Is Nothing Then
        '            UpdatePaymentDataSetStru(ds, UpdateFlag)
        '            cmdHold.Enabled = False
        '            PaymentGridSetting()
        '            cmdSavePrint_Click(sender, e)
        '        End If
        '    End If
        'Catch ex As Exception
        '    ShowMessage(getValueByKey  ("CM033"), "Error:CM033")
        '    LogException(ex)
        '    'ShowMessage("Error in Updating cash payment data ", "Information")
        'End Try
        Try
           

            'ShowMessage(getValueByKey("CM067"), "CM067 - " & getValueByKey("CLAE04"))
            'Issue ID 7625
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType Then
                If CustomerSaleType = enumCustomerSaleType.Home_Delivery Then
                    If CustInfo.CtrlTxtSwape.Text.Trim = "" Then
                        ShowMessage(getValueByKey("Crs015"), "CM030 - " & getValueByKey("CLAE05"))
                        Exit Sub
                    End If
                End If
            End If
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType AndAlso clsDefaultConfiguration.IsAfterSelectSaleTypeCmSelectionRequired Then 'ketan
                If CustomerSaleType = 0 Then Exit Sub
                Dim CustSaleType As String = IIf(CustomerSaleType = enumCustomerSaleType.Dine_In, "DINEIN", "TAKEAWAY")
                If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then
                    cmdCustomerSearchAndLoad(CustSaleType)
                End If
                If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then Exit Sub
            End If
            'Dim index As Int32 = dgMainGrid.Cols("Quantity").Index
            'dgMainGrid_AfterEdit(dgMainGrid, New C1.Win.C1FlexGrid.RowColEventArgs(dgMainGrid.Row, index))
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType AndAlso CustomerSaleType = 0 Then Exit Sub
            If dsMain.Tables("CASHMEMODTL").Select("BTYPE='S'  AND isnull(TotalDiscount,0)='0'", "", DataViewRowState.CurrentRows).Length = dsMain.Tables("CASHMEMODTL").Rows.Count Then
                isCashierPromoSelected = False
            End If
            If clsDefaultConfiguration.EvasPizzaChanges = True Then
                '  dsMain.Tables("CASHMEMOHDR").Rows(0)("Remark") = ValueChangeRemark
                _remarks = ValueChangeRemark
            End If
            'End Issue ID 7625
            If UpdateFlag = False Then
                'If PromotionCleared = False Then
                cmdDefaultPromo_Click(sender, e)
                'End If
                'cmdLoyalty_Click(sender, e)
            End If

            If clsDefaultConfiguration.IsPoleDisply = True Then
                TwoLineExtendScreen(Ispayment:=True)
            End If

            If clsDefaultConfiguration.DirectCashPayment Then
                DirectCashPayment(sender, e)
            Else
                Using objPaymentByCash As New frmNAcceptPaymentByCash
                    If clsDefaultConfiguration.EvasPizzaChanges = True Then
                        _remarks = ValueChangeRemark
                    End If
                    objPaymentByCash.txtRemark.Text = _remarks
                    objPaymentByCash._IsCashierPromoSelected = isCashierPromoSelected
                    objPaymentByCash.TotalBillAmount = CDbl(CashSummary.CtrllblNetAmt.Text)
                    objPaymentByCash.ShowDialog()
                    If Not (objPaymentByCash.IsCancelAcceptPayment) Then
                        If Not objPaymentByCash.ReciptTotalAmount Is Nothing And objPaymentByCash.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                            _remarks = objPaymentByCash.txtRemark.Text
                            _billAmt = objPaymentByCash.TotalBillAmount
                            _paidAmt = objPaymentByCash.TotalCustomerPadiAmount
                            Dim ds As DataSet = objPaymentByCash.ReciptTotalAmount
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
                End Using
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

            Try
                If clsAdmin.IsCashDrawer Then
                    Dim cA4Print As New clsA4Print
                    cA4Print.OperateDevice("CashDrawer", CDflag:=clsDefaultConfiguration.CDBuildCode)
                End If
            Catch ex As Exception

            End Try
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
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType Then
                If CustomerSaleType = enumCustomerSaleType.Home_Delivery Then
                    If CustInfo.CtrlTxtSwape.Text.Trim = "" Then
                        ShowMessage(getValueByKey("Crs015"), "CM030 - " & getValueByKey("CLAE05"))
                        Exit Sub
                    End If
                End If
            End If
            'Dim index As Int32 = dgMainGrid.Cols("Quantity").Index
            'dgMainGrid_AfterEdit(dgMainGrid, New C1.Win.C1FlexGrid.RowColEventArgs(dgMainGrid.Row, index))
            'ShowMessage(getValueByKey("CM067"), "CM067 - " & getValueByKey("CLAE04"))
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType AndAlso clsDefaultConfiguration.IsAfterSelectSaleTypeCmSelectionRequired Then 'ketan
                If CustomerSaleType = 0 Then Exit Sub
                Dim CustSaleType As String = IIf(CustomerSaleType = enumCustomerSaleType.Dine_In, "DINEIN", "TAKEAWAY")
                If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then
                    cmdCustomerSearchAndLoad(CustSaleType)
                End If
                If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then Exit Sub
            End If
            If clsDefaultConfiguration.EvasPizzaChanges Then
                ' dsMain.Tables("CASHMEMOHDR").Rows(0)("Remark") = ValueChangeRemark
                _remarks = ValueChangeRemark
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
            Dim strSelectedTenderCode As String = objPayment.CardTenderCode
            objPayment.Close()
            If Not (objPayment.IsCancelAcceptPayment) Then
                If Not objPayment.ReciptTotalAmount Is Nothing And objPayment.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
                    Dim ds As DataSet = objPayment.ReciptTotalAmount
                    'added by khusrao adil on 27-04-2017
                    If clsDefaultConfiguration.PayFromInnoviti = True AndAlso clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) Then
                        cashmemoInnovitiList.Clear()
                        cashmemoInnovitiList = objPayment.resonseInnoviti
                    End If
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
                Else
                    ShowMessage(getValueByKey("BL095"), "BL095 - " & getValueByKey("CLAE05"))
                End If
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CM038"), "CM038 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdCreditSale_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCreditSale.Click
        Try
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType Then
                If CustomerSaleType = enumCustomerSaleType.Home_Delivery Then
                    If CustInfo.CtrlTxtSwape.Text.Trim = "" Then
                        ShowMessage(getValueByKey("Crs015"), "CM030 - " & getValueByKey("CLAE05"))
                        Exit Sub
                    End If
                End If
            End If

            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType AndAlso clsDefaultConfiguration.IsAfterSelectSaleTypeCmSelectionRequired Then 'ketan
                If CustomerSaleType = 0 Then Exit Sub
                Dim CustSaleType As String = IIf(CustomerSaleType = enumCustomerSaleType.Dine_In, "DINEIN", "TAKEAWAY")
                If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then
                    cmdCustomerSearchAndLoad(CustSaleType)
                End If
                If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then Exit Sub
            End If
            If clsDefaultConfiguration.EvasPizzaChanges = True Then
                'dsMain.Tables("CASHMEMOHDR").Rows(0)("Remark") = ValueChangeRemark
                _remarks = ValueChangeRemark
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
            'Dim index As Int32 = dgMainGrid.Cols("Quantity").Index
            'dgMainGrid_AfterEdit(dgMainGrid, New C1.Win.C1FlexGrid.RowColEventArgs(dgMainGrid.Row, index))
            'ShowMessage(getValueByKey("CM067"), "CM067 - " & getValueByKey("CLAE04"))

            If dsMain.Tables("CASHMEMODTL").Select("BTYPE='S'  AND isnull(TotalDiscount,0)='0'", "", DataViewRowState.CurrentRows).Length = dsMain.Tables("CASHMEMODTL").Rows.Count Then
                isCashierPromoSelected = False
            End If

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

    Private Sub cmdNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNew.Click
        Try
            saveCashCounter = 0
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
            'If Not IsHOInstance Then
            '    cmdDelete.Enabled = False
            'End If
            Dim docno As String = objCM.getDocumentNo("CM", clsAdmin.SiteCode)
            If dgMainGrid.Rows.Count > 1 Then
                If MsgBox(getValueByKey("CM036"), MsgBoxStyle.YesNo, "CM036 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                    CLPCustomerId = String.Empty
                    ClearData()
                    dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Clear()
                    comboArticleCopy.Rows.Clear()
                    CreateNewRecord(Me, dsMain, "CASHMEMOHDR")
                    UpdateFlag = False
                    SetButtons(1, False)
                    SetButtons(3, True)
                    ProductImage.CtrlProductImages.Image = Nothing
                    If OnlineConnect = True Then
                        'Changed by Rohit to generate Document No. for proper sorting
                        'Try
                        '    lblCMNo.Text = "CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2)
                        '    lblCMNo.Text = GenDocNo(lblCMNo.Text, 15, docno)
                        'Catch ex As Exception
                        '    lblCMNo.Text = "CM" & clsAdmin.TerminalID & docno
                        'End Try


                        Try
                            'If (clsDefaultConfiguration.CashMemoResetonDayClose = False) Then
                            '    docno = GenDocNo("CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                            'Else
                            '    docno = GenDocNo("CM" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, docno)
                            'End If
                            '' GST CHANGES By KETAN
                            If (clsDefaultConfiguration.CashMemoResetonDayClose = False) Then
                                ' docno = GenDocNo("CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                                ''GST changes by ketan add sitecode 3 digit in billno                               
                                docno = GenDocNo("C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 3, 3) & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                            Else
                                ' docno = GenDocNo("CM" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, docno)
                                ''GST changes by ketan add sitecode 2 digit in billno
                                docno = GenDocNo("C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, docno)
                            End If
                            lblCMNo.Text = docno
                        Catch ex As Exception
                            'lblCMNo.Text = "CM" & clsAdmin.TerminalID & docno
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
                        'Try
                        '    lblCMNo.Text = "OCM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2)
                        '    lblCMNo.Text = GenDocNo(lblCMNo.Text, 15, docno)
                        'Catch ex As Exception
                        '    lblCMNo.Text = "OCM" & clsAdmin.TerminalID & docno
                        'End Try

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
                Else
                    IsMerge = True
                    IsReset = True
                    IsMembership = True
                    Exit Sub
                End If
            Else
                CLPCustomerId = String.Empty
                ClearData()
                dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Clear()
                comboArticleCopy.Rows.Clear()
                CreateNewRecord(Me, dsMain, "CASHMEMOHDR")
                UpdateFlag = False
                SetButtons(1, False)
                SetButtons(3, True)
                ProductImage.CtrlProductImages.Image = Nothing
                If OnlineConnect = True Then
                    ''Changed by Rohit to generate Document No. for proper sorting
                    'Try
                    '    lblCMNo.Text = "CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2)
                    '    lblCMNo.Text = GenDocNo(lblCMNo.Text, 15, docno)
                    'Catch ex As Exception
                    '    lblCMNo.Text = "CM" & clsAdmin.TerminalID & docno
                    'End Try
                    '---Changed By Mahesh CM series new format CM '2 digit' terminalId DDMMYY (5 digit bill No)-----
                    Try
                        'If (clsDefaultConfiguration.CashMemoResetonDayClose = False) Then
                        '    docno = GenDocNo("CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                        'Else
                        '    docno = GenDocNo("CM" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, docno)
                        'End If
                        ''GST CHANGES BY KETAN
                        If (clsDefaultConfiguration.CashMemoResetonDayClose = False) Then
                            ''GST changes by ketan add sitecode 3 digit in billno
                            ' docno = GenDocNo("CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                            docno = GenDocNo("C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 3, 3) & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                        Else
                            ''GST changes by ketan add sitecode 2 digit in billno
                            '  docno = GenDocNo("CM" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, docno)
                            docno = GenDocNo("C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, docno)
                        End If
                        lblCMNo.Text = docno
                    Catch ex As Exception
                        lblCMNo.Text = "C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 2, 2) & docno
                    End Try
                    Try
                        rbnLStatus.SmallImage = Spectrum.My.Resources.connected1.ToBitmap
                        rbnLStatus.Text = getValueByKey("ctrlrbnbaseform.rbnStatus2")
                    Catch ex As Exception

                    End Try

                    'End Change by Rohit
                Else
                    'Changed by Rohit to generate Document No. for proper sorting
                    'Try
                    '    lblCMNo.Text = "OCM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2)
                    '    lblCMNo.Text = GenDocNo(lblCMNo.Text, 15, docno)
                    'Catch ex As Exception
                    '    lblCMNo.Text = "OCM" & clsAdmin.TerminalID & docno
                    'End Try

                    If (clsDefaultConfiguration.CashMemoResetonDayClose = False) Then
                        docno = GenDocNo("OCM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                    Else
                        docno = GenDocNo("OCM" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, docno)
                    End If
                    lblCMNo.Text = docno
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
            End If
            If Not IsHOInstance Then
                cmdCustomerinfo.Enabled = True
            End If
            CtrlSalesPersons.CtrlTxtBox.Enabled = True
            CtrlSalesPersons.AndroidSearchTextBox.Enabled = True

            IsCSTApplicable = False
            clsDefaultConfiguration.CSTTaxCode = ""
            If clsDefaultConfiguration.GVsaleAllowed = True Then
                CMbtnBottom.CtrlBtnSaleGV.Enabled = True
                sellGiftVoucher.Enabled = True
            Else
                CMbtnBottom.CtrlBtnSaleGV.Enabled = False
                sellGiftVoucher.Enabled = False
            End If
            CMbtnBottom.CtrlBtnSaleCLPPoint.Enabled = True
            CMbtnBottom.CtrlBtnStockCheck.Enabled = True
            CMbtnBottom.CtrlBtnReturn.Enabled = True
            CMbtnBottom.CtrlBtnAddExtraCost.Enabled = True
            CMbtnBottom.CtrlBtnHomeDelivery.Enabled = True
            customerType = String.Empty
            HDPrintRequired = False
            IsDefaultPromotion = False
            cmdCreditSale.Enabled = False
            homeDelivery.Enabled = True
            CtrlMODMenu1.Enabled = True
            CtrlNumberPad1.Enabled = True
            CustInfo.BtnClearCustmInfo.Enabled = True
            CustInfo.CtrlTxtCustomerNo.ReadOnly = False
            CustInfo.CtrlTxtSwape.ReadOnly = False
            If clsDefaultConfiguration.IsMembership Then
                rbnGrpCMPromotion.Visible = True
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
            IsMembership = False
            cbManualDisc.Enabled = False
            cmdEnable.Text = getValueByKey("frmcashmemo.cmdenable")

            Payment.Visible = False
            If clsDefaultConfiguration.SalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0 Then
                CtrlSalesPersons.CtrlSalesPersons.SelectedValue = clsAdmin.UserCode
            End If
            CustSaleTypeTimer.Stop()
            CustomerSaleType = 0
            IsHoldEnterKey = False
            lblCustSaleType.Visible = False
            IsBillVoid = False
            ' CtrlSalesPersons.CtrlTxtBox.Focus()
            If clsDefaultConfiguration.IsMembership Then
                CustInfo.CtrlTxtSwape.Focus()
            Else
                CtrlSalesPersons.AndroidSearchTextBox.Focus()
            End If
            _billAmt = 0
            _paidAmt = 0
            _remarks = ""
            If clsDefaultConfiguration.IsBillScanApplicable Then
                dgMainGrid.Cols("Quantity").AllowEditing = True
            End If
            HideColumns(dgMainGrid, False, "TAKEAWAYQUANTITY")
            If clsDefaultConfiguration.IsMembership Then
                CustInfo.CtrlTxtCustomerNo.ReadOnly = False
                CustInfo.CtrlTxtSwape.ReadOnly = False
            End If
            '--- Reset for Dine IN 

            If clsDefaultConfiguration.DineInProcess Then
                currentDineInBillNo = String.Empty
                currentDineInTable = 0
                mergeIdForGenerateBill = 0
                lblDineInTableNo.Visible = False
                lblTable.Visible = False
                cmdGenerateKOT.Enabled = False
                cmdGenerateBill.Enabled = False
                cmd_Remark.Enabled = False
                cmd_Track.Enabled = False
                cmd_VoidKot.Enabled = False
                cmd_Reprints.Enabled = False
                IsMerge = False
                If mergeIdForGenerateBill > 0 Then
                    mergeIdForGenerateBill = 0
                End If
            End If
            If clsDefaultConfiguration.ExtendScreen Then
                If clsDefaultConfiguration.HariOmExtendScreen = True Then
                    TwoLineExtendScreen()
                Else
                    ExtendFun()
                End If
            End If
            If clsDefaultConfiguration.IsPoleDisply = True Then
                TwoLineExtendScreen()
            End If

            If (clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType AndAlso clsDefaultConfiguration.DefaultSaleType <> "") Then
                If clsDefaultConfiguration.DefaultSaleType = "0" Then 'Or clsDefaultConfiguration.DefaultSaleType = "00" Or clsDefaultConfiguration.DefaultSaleType = "000" 
                    CtrlSalesPersons.CtrlTxtBox.Text = clsDefaultConfiguration.DefaultSaleType
                    txtSearch_KeyDown(sender, New System.Windows.Forms.KeyEventArgs(Keys.Enter))
                    ' cmdNew_Click(cmdNew, e)
                End If
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM038"), "CM038 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Transaction intialization is not Sucessfull", "Error")
        End Try

    End Sub
    Private Sub dgMainGrid_AfterSelChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles dgMainGrid.AfterSelChange
        If (dgMainGrid.Rows.Count > 1) Then
            calculateTotalbill()
            Me.CtrlNumberPad1.ClearNumber()

            If clsDefaultConfiguration.IsPoleDisply = True Then
                Dim selectedrow As Integer = dgMainGrid.RowSel - 1
                TwoLineExtendScreen(selectedrow)
            End If
        End If
    End Sub

    'Private Sub dgMainGrid_SelChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles dgMainGrid.SelChange        
    '    Me.CtrlNumberPad1.ClearNumber()
    'End Sub

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
            If clsDefaultConfiguration.DineInProcess = True Then
                IsMergeOrderBillNo()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub dgMainGrid_RowDoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles dgMainGrid.DoubleClick
        Try
            If dgMainGrid.Rows.Count < 2 Then Exit Sub
            Dim dtCombo As DataTable
            dtCombo = objArticleCombo.GetComboDetails(dgMainGrid.Rows(dgMainGrid.Row)("ARTICLECODE"))
            If Not dtCombo Is Nothing AndAlso dtCombo.Rows.Count > 0 Then
                If Not dgMainGrid.Cols(dgMainGrid.ColSel).Name.ToUpper() = "SELECTS".ToUpper() Then
                    If dgMainGrid.Row >= 1 Then
                        Dim dataRows As DataRow() = dsMain.Tables("Cashmemodtl").Select("TotalDiscount <> 0 And ArticleCode='" & dgMainGrid.Rows(dgMainGrid.Row)("ARTICLECODE") & "'")
                        If dataRows IsNot Nothing AndAlso dataRows.Count > 0 Then
                            If MsgBox(getValueByKey("frmfastcashmemo.combocleardiscount"), MsgBoxStyle.YesNo, getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                                For Each dr As DataRow In dsMain.Tables("Cashmemodtl").Select("TotalDiscount <> 0 And ArticleCode='" & dgMainGrid.Rows(dgMainGrid.Row)("ARTICLECODE") & "'")
                                    dr("TotalDiscount") = 0
                                    dr("LineDiscount") = 0
                                    dr("TOTALDISCPERCENTAGE") = 0
                                    dr("FIRSTLEVEL") = String.Empty
                                    dr("TOPLEVEL") = String.Empty
                                    dr("MANUALPROMO") = 0
                                    Dim taxAmt = CalculateTotalInclusiveTax(0, dgMainGrid.Rows(dgMainGrid.RowSel)("BillLineNo").ToString(), dr("ARTICLECODE"), dr("GrossAmt"), 1, dr("EAN"))
                                    dr("TotalTaxAmount") = taxAmt
                                Next
                            Else
                                Exit Sub
                            End If
                        End If
                        Dim articleTable As New DataTable
                        articleTable.Columns.Add("ARTICLECODE")
                        articleTable.Columns.Add("EAN")
                        articleTable.Columns.Add("DISCRIPTION")
                        articleTable.Columns.Add("SELLINGPRICE")
                        articleTable.Columns.Add("NETSELLINGPRICE")
                        articleTable.Columns.Add("Quantity")
                        articleTable.Columns.Add("IndividualQty")
                        articleTable.Columns.Add("ComboPartNumber")
                        articleTable.Columns.Add("BillLineNo")
                        articleTable.Columns.Add("IsUpgraded")
                        articleTable.Columns.Add("Discount")
                        Dim ArticleComboPopup As New ArticleComboPopup(True)

                        ArticleComboPopup.ComboDetails = dtCombo
                        For Each row In comboArticleCopy.Rows
                            If row("BillLineNo") = ((dgMainGrid.Rows.Count - 1) - (dgMainGrid.Row - 1)) Then
                                articleTable.Rows.Add(row("ARTICLECODE"), row("EAN"), row("DISCRIPTION"), row("SELLINGPRICE"), row("NETSELLINGPRICE"), row("Quantity"), row("IndividualQty"), row("ComboPartNumber"), row("BillLineNo"), row("IsUpgraded"), row("Discount"))
                            End If
                        Next
                        ArticleComboPopup.SelectedComboArticles = articleTable
                        ArticleComboPopup.ShowDialog()
                        'Issue ID 7627
                        StrSelectedComboArticle = ""
                        If ArticleComboPopup.SelectedComboArticles.Rows.Count > 0 Then
                            For Each dr In ArticleComboPopup.SelectedComboArticles.Rows
                                If StrSelectedComboArticle = "" Then
                                    StrSelectedComboArticle = dr("ArticleCode")
                                Else
                                    StrSelectedComboArticle = StrSelectedComboArticle + "," + dr("ArticleCode")
                                End If
                            Next
                        End If
                        If Not ArticleComboPopup.SelectedComboArticles Is Nothing AndAlso ArticleComboPopup.SelectedComboArticles.Rows.Count > 0 Then
                            Dim sellingPrice As DataTable = objArticleCombo.GetComboSellingPrice(dgMainGrid.Rows(dgMainGrid.Row)("ARTICLECODE"), clsAdmin.SiteCode)
                            Dim tax As Double = 0
                            Dim totalSellingPrice As Double = 0
                            Dim originalTax As Double
                            If ArticleComboPopup.AdditionalComboCost > 0 Then
                                tax = CalculateTotalInclusiveTax(originalTax, dgMainGrid.Rows(dgMainGrid.RowSel)("BillLineNo").ToString(), dgMainGrid.Rows(dgMainGrid.Row)("ARTICLECODE"), (sellingPrice.Rows(0)("SellingPrice") * ArticleComboPopup.ComboQuantity) + ArticleComboPopup.AdditionalComboCost, ArticleComboPopup.ComboQuantity, dgMainGrid.Rows(dgMainGrid.Row)("EAN"))
                                totalSellingPrice = (sellingPrice.Rows(0)("SellingPrice") + (ArticleComboPopup.AdditionalComboCost / ArticleComboPopup.ComboQuantity))
                            Else
                                tax = CalculateTotalInclusiveTax(originalTax, dgMainGrid.Rows(dgMainGrid.RowSel)("BillLineNo").ToString(), dgMainGrid.Rows(dgMainGrid.Row)("ARTICLECODE"), sellingPrice.Rows(0)("SellingPrice") * ArticleComboPopup.ComboQuantity, ArticleComboPopup.ComboQuantity, dgMainGrid.Rows(dgMainGrid.Row)("EAN"))
                                totalSellingPrice = sellingPrice.Rows(0)("SellingPrice")
                            End If
                            dgMainGrid.Rows(dgMainGrid.Row)("TotalTaxAmount") = tax

                            dgMainGrid.Rows(dgMainGrid.Row)("SELLINGPRICE") = totalSellingPrice
                            dgMainGrid.Rows(dgMainGrid.Row)("Quantity") = ArticleComboPopup.ComboQuantity

                            ReCalculateCM("")
                            'End Issue ID 7627
                            calculateTotalbill()
                            Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
                            articleDescriptionDictionary.Add(ObjclsCommon.GetArticleDescription(dgMainGrid.Rows(dgMainGrid.Row)("ARTICLECODE")), 0)
                            GetArticleDecriptionDictionary(articleDescriptionDictionary, ArticleComboPopup.SelectedComboArticles)
                            dgMainGrid.Rows(dgMainGrid.Row)("DISCRIPTION") = GetMultilinedString(articleDescriptionDictionary)
                            'setgridview(dt, True)
                            For i = dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count - 1 To 0 Step -1
                                If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).RowState = DataRowState.Deleted Then
                                    dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                                End If
                            Next

                            For i = dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count - 1 To 0 Step -1
                                If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i)("BillLineNo") = ((dgMainGrid.Rows.Count - 1) - (dgMainGrid.Row - 1)) Then
                                    dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).Delete()
                                    'If Not dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i) Is Nothing Then
                                    '    dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                                    'End If
                                End If
                            Next
                            'Just a Temp Solution, required Proper Analysis of issue
                            For i = dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count - 1 To 0 Step -1
                                If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).RowState = DataRowState.Deleted Then
                                    dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                                End If
                            Next

                            For i = comboArticleCopy.Rows.Count - 1 To 0 Step -1
                                If comboArticleCopy.Rows(i)("BillLineNo") = ((dgMainGrid.Rows.Count - 1) - (dgMainGrid.Row - 1)) Then
                                    comboArticleCopy.Rows(i).Delete()
                                End If
                            Next

                            CheckingComboCode()
                            For Each dataRow In ArticleComboPopup.SelectedComboArticles.Rows
                                AddComboArticles(dataRow, ((dgMainGrid.Rows.Count - 1) - (dgMainGrid.Row - 1)) - 1)
                            Next
                            comboArticleCopy.Merge(ArticleComboPopup.SelectedComboArticles)
                        End If
                    End If
                End If
            End If
            'code added by vipul for article remark on desc double click
            If dgMainGrid.Rows.Count > 0 And Not dgMainGrid.Rows.Count = 0 Then

                '   If dgMainGrid.IsCellSelected(dgMainGrid.Row, dgMainGrid.Col) Then

                If dgMainGrid.Cols(dgMainGrid.ColSel).Name.ToUpper() = "DISCRIPTION".ToUpper() Then
                    Dim ChildForm As New Spectrum.frmArticlesRemark
                    Try
                        Dim lastRowIndex As Integer = dgMainGrid.Row
                        If lastRowIndex = -1 Then Exit Sub
                        Dim articlecode = dgMainGrid.Rows(lastRowIndex)("ArticleCode")
                        Dim billlineno = dgMainGrid.Rows(dgMainGrid.Row)("BillLineNo")
                        Dim EAN = dgMainGrid.Rows(lastRowIndex)("EAN")
                        Using objArticleRemark As New frmArticlesRemark
                            If dsMain.Tables.Contains("CASHMEMODTLITEMREMARK") Then
                                Dim Dr() = dsMain.Tables("CASHMEMODTLITEMREMARK").Select("ArticleCode ='" & articlecode & "' and EAN='" & EAN & "'and BillLineNo='" & billlineno & "'")
                                If Dr.Count > 0 Then
                                    objArticleRemark.ResultRemark = IIf(IsDBNull(Dr(0)("itemRemarks")), "", Dr(0)("itemRemarks"))
                                End If
                                If objArticleRemark.ShowDialog() = Windows.Forms.DialogResult.OK Then
                                    If Dr.Count > 0 Then
                                        Dr(0)("itemRemarks") = objArticleRemark.ResultRemark
                                    Else
                                        Dim remarkRow As DataRow = dsMain.Tables("CASHMEMODTLITEMREMARK").NewRow()
                                        remarkRow("siteCode") = clsAdmin.SiteCode
                                        remarkRow("EAN") = EAN
                                        remarkRow("ArticleCode") = articlecode
                                        remarkRow("itemRemarks") = objArticleRemark.ResultRemark
                                        remarkRow("BillLineNo") = billlineno
                                        dsMain.Tables("CASHMEMODTLITEMREMARK").Rows.Add(remarkRow)

                                    End If
                                End If
                            End If
                        End Using
                    Catch ex As Exception
                        LogException(ex)
                    End Try
                End If

            End If
            dgMainGrid.Update()
            dgMainGrid.Refresh()
            If clsDefaultConfiguration.ExtendScreen = True Then
                If clsDefaultConfiguration.HariOmExtendScreen = True Then
                    TwoLineExtendScreen(dgMainGrid.Row - 1)
                End If
            End If
            If clsDefaultConfiguration.IsPoleDisply = True Then
                TwoLineExtendScreen(dgMainGrid.Row - 1)
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
            Dim articlecode = dgMainGrid.Rows(e.Row)("articlecode").ToString
            Dim discountAmount As Decimal = Decimal.Zero
            discountAmount = dsMain.Tables("CASHMEMODTL").Compute("Sum(TotalDiscount)", String.Empty)
            If clsDefaultConfiguration.IsMembership Then discountAmount = Decimal.Zero
            If (discountAmount > 0) Then
                If MsgBox(getValueByKey("CM060"), MsgBoxStyle.OkCancel, "CM060") = MsgBoxResult.Ok Then

                    'dsMain.Tables("CASHMEMODTL").Rows.RemoveAt(dgMainGrid.Row - 1)            
                    If Not dtMainTax Is Nothing AndAlso dtMainTax.Rows.Count > 0 Then

                        'Delete Tax details for selected row
                        Dim taxFilter As String = String.Format("BillLineNo={0} AND ArticleCode='{1}' AND EAN='{2}'", dgMainGrid.Rows(dgMainGrid.Row)("BillLineNo"), dgMainGrid.Rows(dgMainGrid.Row)("ArticleCode"), dgMainGrid.Rows(dgMainGrid.Row)("EAN"))
                        Dim drTax = dtMainTax.Select(taxFilter)

                        If (drTax.Length > 0) Then
                            For Each dr As DataRow In drTax
                                dr.Delete()
                            Next
                            dtMainTax.AcceptChanges()
                        End If

                        'Dim dv As New DataView(dtMainTax, "ArticleCode='" & dgMainGrid.Rows(e.Row)("ArticleCode") & "' AND EAN='" & dgMainGrid.Rows(e.Row)("EAN") & "'", "", DataViewRowState.CurrentRows)
                        'If dv.Count > 0 Then
                        '    dv.AllowDelete = True
                        '    For Each dr As DataRowView In dv
                        '        dr.Delete()
                        '    Next
                        'End If
                        'dtMainTax.AcceptChanges()
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
                    Dim number As Integer = dgMainGrid.Rows(e.Row)("BillLineNo")
                    If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count > 0 Then
                        For i = dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count - 1 To 0 Step -1
                            If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).RowState = DataRowState.Deleted Then
                                dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                            End If
                        Next

                        For i = dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count - 1 To 0 Step -1
                            If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i)("BillLineNo") = ((dgMainGrid.Rows.Count - 1) - (e.Row - 1)) Then
                                dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).Delete()
                                'If Not dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i) Is Nothing Then
                                '    dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                                'End If
                            End If
                        Next

                        For i = dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count - 1 To 0 Step -1
                            If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).RowState = DataRowState.Deleted Then
                                dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                            End If
                        Next

                    End If
                    If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count > 0 Then
                        For Each row In dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows
                            If row("BillLineNo") > number Then
                                row("BillLineNo") = row("BillLineNo") - 1
                            End If
                        Next
                    End If
                    If comboArticleCopy.Rows.Count > 0 Then
                        For i = comboArticleCopy.Rows.Count - 1 To 0 Step -1
                            If comboArticleCopy.Rows(i)("BillLineNo") = ((dgMainGrid.Rows.Count - 1) - (e.Row - 1)) Then
                                comboArticleCopy.Rows(i).Delete()
                            End If
                        Next
                    End If
                    If comboArticleCopy.Rows.Count > 0 Then
                        For Each row In comboArticleCopy.Rows
                            If row("BillLineNo") > number Then
                                row("BillLineNo") = row("BillLineNo") - 1
                            End If
                        Next
                    End If

                    'remark
                    For i = dsMain.Tables("CashMemoDtlItemRemark").Rows.Count - 1 To 0 Step -1
                        If dsMain.Tables("CashMemoDtlItemRemark").Rows(i)("BillLineNo") = ((dgMainGrid.Rows.Count - 1) - (dgMainGrid.Row - 1)) Then
                            dsMain.Tables("CashMemoDtlItemRemark").Rows(i).Delete()
                            'If Not dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i) Is Nothing Then
                            '    dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                            'End If
                        End If
                    Next

                    If dsMain.Tables("CashMemoDtlItemRemark").Rows.Count > 0 Then
                        For Each row In dsMain.Tables("CashMemoDtlItemRemark").Rows
                            If row("BillLineNo") > number Then
                                row("BillLineNo") = row("BillLineNo") - 1
                            End If
                        Next
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
                    'code added  by vipul for Customer wise price
                    If clsDefaultConfiguration.customerwisepricemanagement = True Then
                        calculateTotalbill()
                    End If
                    IsDefaultPromotion = False
                   
                End If
            Else

                'dsMain.Tables("CASHMEMODTL").Rows.RemoveAt(dgMainGrid.Row - 1)            
                If Not dtMainTax Is Nothing AndAlso dtMainTax.Rows.Count > 0 Then

                    'Delete Tax details for selected row
                    Dim taxFilter As String = String.Format("BillLineNo={0} AND ArticleCode='{1}' AND EAN='{2}'", dgMainGrid.Rows(dgMainGrid.Row)("BillLineNo"), dgMainGrid.Rows(dgMainGrid.Row)("ArticleCode"), dgMainGrid.Rows(dgMainGrid.Row)("EAN"))
                    Dim drTax = dtMainTax.Select(taxFilter)

                    If (drTax.Length > 0) Then
                        For Each dr As DataRow In drTax
                            dr.Delete()
                        Next
                        dtMainTax.AcceptChanges()
                    End If

                    'Dim dv As New DataView(dtMainTax, "ArticleCode='" & dgMainGrid.Rows(e.Row)("ArticleCode") & "' AND EAN='" & dgMainGrid.Rows(e.Row)("EAN") & "'", "", DataViewRowState.CurrentRows)
                    'If dv.Count > 0 Then
                    '    dv.AllowDelete = True
                    '    For Each dr As DataRowView In dv
                    '        dr.Delete()
                    '    Next
                    'End If
                    'dtMainTax.AcceptChanges()
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
                Dim number As Integer = dgMainGrid.Rows(e.Row)("BillLineNo")
                If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count > 0 Then
                    For i = dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count - 1 To 0 Step -1
                        If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).RowState = DataRowState.Deleted Then
                            dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                        End If
                    Next

                    For i = dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count - 1 To 0 Step -1
                        If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i)("BillLineNo") = ((dgMainGrid.Rows.Count - 1) - (e.Row - 1)) Then
                            dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).Delete()
                            'If Not dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i) Is Nothing Then
                            '    dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                            'End If
                        End If
                    Next

                    For i = dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count - 1 To 0 Step -1
                        If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).RowState = DataRowState.Deleted Then
                            dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                        End If
                    Next

                End If
                If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count > 0 Then
                    For Each row In dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows
                        If row("BillLineNo") > number Then
                            row("BillLineNo") = row("BillLineNo") - 1
                        End If
                    Next
                End If
                If comboArticleCopy.Rows.Count > 0 Then
                    For i = comboArticleCopy.Rows.Count - 1 To 0 Step -1
                        If comboArticleCopy.Rows(i)("BillLineNo") = ((dgMainGrid.Rows.Count - 1) - (e.Row - 1)) Then
                            comboArticleCopy.Rows(i).Delete()
                        End If
                    Next
                End If
                If comboArticleCopy.Rows.Count > 0 Then
                    For Each row In comboArticleCopy.Rows
                        If row("BillLineNo") > number Then
                            row("BillLineNo") = row("BillLineNo") - 1
                        End If
                    Next
                End If


                'remark
                For i = dsMain.Tables("CashMemoDtlItemRemark").Rows.Count - 1 To 0 Step -1
                    If dsMain.Tables("CashMemoDtlItemRemark").Rows(i)("BillLineNo") = ((dgMainGrid.Rows.Count - 1) - (dgMainGrid.Row - 1)) Then
                        dsMain.Tables("CashMemoDtlItemRemark").Rows(i).Delete()
                        'If Not dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i) Is Nothing Then
                        '    dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                        'End If
                    End If
                Next

                If dsMain.Tables("CashMemoDtlItemRemark").Rows.Count > 0 Then
                    For Each row In dsMain.Tables("CashMemoDtlItemRemark").Rows
                        If row("BillLineNo") > number Then
                            row("BillLineNo") = row("BillLineNo") - 1
                        End If
                    Next
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

            If Not clsDefaultConfiguration.IsGVSellAllowedWithOtherArticle Then
                Dim drGVItems() As DataRow = dsMain.Tables("CashMemoDtl").Select("ArticleCode='GVBaseArticle'")

                If (drGVItems.Count > 0) Then
                    sellGiftVoucher.Enabled = False
                Else
                    sellGiftVoucher.Enabled = True
                End If
            End If
            If clsDefaultConfiguration.ExtendScreen Then
                dtExtend.Rows.Clear()
                If clsDefaultConfiguration.HariOmExtendScreen = True Then
                    TwoLineExtendScreen()
                Else
                    ExtendFun()
                End If
            End If
            If clsDefaultConfiguration.IsPoleDisply = True Then
                TwoLineExtendScreen()
            End If


            'CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Select()

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub dgMainGrid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgMainGrid.KeyDown
        Try
            If UpdateFlag = False And e.KeyCode = Keys.Delete Then

                'If clsDefaultConfiguration.IsBillScanApplicable Then
                '    If (dsMain.Tables("CashMemoMettler") IsNot Nothing AndAlso dsMain.Tables("CashMemoMettler").Rows.Count > 0) Then
                '        Exit Sub
                '    End If
                'End If
                'Delete Tax details for selected row
                If Not dtMainTax Is Nothing AndAlso dtMainTax.Rows.Count > 0 Then
                    Dim taxFilter As String = String.Format("BillLineNo={0} AND ArticleCode='{1}' AND EAN='{2}'", dgMainGrid.Rows(dgMainGrid.Row)("BillLineNo"), dgMainGrid.Rows(dgMainGrid.Row)("ArticleCode"), dgMainGrid.Rows(dgMainGrid.Row)("EAN"))
                    Dim drTax = dtMainTax.Select(taxFilter)

                    If (drTax.Length > 0) Then
                        For Each dr As DataRow In drTax
                            dr.Delete()
                        Next
                        dtMainTax.AcceptChanges()
                    End If
                End If
                'code added by vipul for issue id 3053
                If Not dtGV Is Nothing Then
                    If dgMainGrid.Rows(dgMainGrid.Row)("Section") Is DBNull.Value Then
                    Else
                        Dim dv As New DataView(dtGV, "IssuedDocNumber=" & dgMainGrid.Rows(dgMainGrid.Row)("Section").ToString(), "", DataViewRowState.CurrentRows)
                        If dv.Count > 0 Then
                            dv.AllowDelete = True
                            For Each dr As DataRowView In dv
                                dr.Delete()
                            Next
                        End If
                        dtGV.AcceptChanges()
                    End If
                End If
                Dim number As Integer = dgMainGrid.Rows(dgMainGrid.Row)("BillLineNo")
                If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count > 0 Then
                    For i = dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count - 1 To 0 Step -1
                        If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).RowState = DataRowState.Deleted Then
                            dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                        End If
                    Next

                    For i = dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count - 1 To 0 Step -1
                        If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i)("BillLineNo") = ((dgMainGrid.Rows.Count - 1) - (dgMainGrid.Row - 1)) Then
                            dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).Delete()
                            'If Not dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i) Is Nothing Then
                            '    dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                            'End If
                        End If
                    Next

                    For i = dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count - 1 To 0 Step -1
                        If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).RowState = DataRowState.Deleted Then
                            dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                        End If
                    Next

                End If
                If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count > 0 Then
                    For Each row In dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows
                        If row("BillLineNo") > number Then
                            row("BillLineNo") = row("BillLineNo") - 1
                        End If
                    Next
                End If
                If comboArticleCopy.Rows.Count > 0 Then
                    For i = comboArticleCopy.Rows.Count - 1 To 0 Step -1
                        If comboArticleCopy.Rows(i)("BillLineNo") = ((dgMainGrid.Rows.Count - 1) - (dgMainGrid.Row - 1)) Then
                            comboArticleCopy.Rows(i).Delete()
                        End If
                    Next
                End If
                If comboArticleCopy.Rows.Count > 0 Then
                    For Each row In comboArticleCopy.Rows
                        If row("BillLineNo") > number Then
                            row("BillLineNo") = row("BillLineNo") - 1
                        End If
                    Next
                End If

                'remark
                For i = dsMain.Tables("CashMemoDtlItemRemark").Rows.Count - 1 To 0 Step -1
                    If dsMain.Tables("CashMemoDtlItemRemark").Rows(i)("BillLineNo") = ((dgMainGrid.Rows.Count - 1) - (dgMainGrid.Row - 1)) Then
                        dsMain.Tables("CashMemoDtlItemRemark").Rows(i).Delete()
                        'If Not dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i) Is Nothing Then
                        '    dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                        'End If
                    End If
                Next

                If dsMain.Tables("CashMemoDtlItemRemark").Rows.Count > 0 Then
                    For Each row In dsMain.Tables("CashMemoDtlItemRemark").Rows
                        If row("BillLineNo") > number Then
                            row("BillLineNo") = row("BillLineNo") - 1
                        End If
                    Next
                End If

                dgMainGrid.Rows.Remove(dgMainGrid.Row)
                'dsMain.Tables("CASHMEMODTL").AcceptChanges()
                RemoveDeletedRow(dsMain.Tables("CASHMEMODTL"))
                CreatingLineNO(dsMain, "CashMemoDtl")
                calculateTotalbill()

                If (dgMainGrid.Rows.Count > 1) Then
                    dgMainGrid.Select(1, 2)
                End If
                IsDefaultPromotion = False
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
                cbManualDisc.Enabled = True
                'cmdEnable.Text = "Disable(Ctrl+B)"
                cmdEnable.Text = getValueByKey("frmcashmemo.cmddisable")
                cbManualDisc.SelectedIndex = -1
                cbManualDisc.Select()
                cmdEnable.Tag = "D"
                cbManualDisc.Focus()
            Else
                cbManualDisc.Enabled = False
                'cmdEnable.Text = "Enable(Ctrl+B)"
                cmdEnable.Text = getValueByKey("frmcashmemo.cmdenable")
                cmdEnable.Tag = "E"
                'CtrlSalesPersons.CtrlTxtBox.Select()
                CtrlSalesPersons.AndroidSearchTextBox.Select()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function cmdGiftPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) As Boolean
        Try
            If dgMainGrid.Rows.Count > 1 Then
                Dim billNo As String = lblCMNo.Text
                If Not cmdSavePrint_Click(sender, e) Then
                    Return False
                End If

                Dim obj As New clsCashMemoPrint(billNo, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
                obj.DisplayArticleFullName = clsDefaultConfiguration.PrintItemFullName
                obj.AllowDecimalQty = clsDefaultConfiguration.AllowDecimalQty
                obj.WeightScaleEnabled = clsDefaultConfiguration.WeightScaleEnabled
                'obj.CompositeTaxReqOnPrint = clsDefaultConfiguration.CompositeTaxReqOnPrint

                Dim ErrorMsg As String = ""
                If Not clsDefaultConfiguration.IsMembership Then
                    'obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMWAmt", "", "", "", ErrorMsg, GiftMsg)
                    If clsDefaultConfiguration.PrintFormatNo = 1 Or clsDefaultConfiguration.PrintFormatNo = 2 Or clsDefaultConfiguration.PrintFormatNo = 3 Or clsDefaultConfiguration.PrintFormatNo = 0 Then
                        obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMWAmt", "", "", "", ErrorMsg, GiftMsg, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6) '0000413  code added by irfan on 18/9/2017 visiblity of hsn and tax
                    End If

                End If
                If ErrorMsg <> String.Empty Then
                    ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
                End If
                GiftMsg = ""
                Return True
            End If
            objCM.ShiftManagementForCM = False
            Dim dt As DataTable = objCM.GetOldCashMemo(clsAdmin.SiteCode, NoOfDaysForReprint:=clsDefaultConfiguration.NumberOfDaysApplicableForReprint)

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
                    'obj.CompositeTaxReqOnPrint = clsDefaultConfiguration.CompositeTaxReqOnPrint

                    Dim ErrorMsg As String = ""
                    ' obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMWAmt", "", "", "", ErrorMsg, "")
                    obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CMWAmt", "", "", "", ErrorMsg, "", IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6) '0000413  code added by irfan on 18/9/2017 visiblity of hsn and tax
                    If ErrorMsg <> String.Empty Then
                        ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
                    End If
                    ErrorMsg = ""
                    'obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "", "", "", ErrorMsg)
                    obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "", "", "", ErrorMsg, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6, JKPrintFormatEnable:=clsDefaultConfiguration.JKPrintFormatEnable) '0000413  code added by irfan on 18/9/2017 visiblity of hsn and tax

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
    'Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
    '    Try
    '        If OnlineConnect = False Then
    '            ShowMessage(getValueByKey("CMO50"), "CMO50 - " & getValueByKey("CLAE04"))
    '            Exit Sub
    '        End If
    '        If UpdateFlag = False Then
    '            ShowMessage(getValueByKey("CM039"), "CM039 - " & getValueByKey("CLAE04"))
    '            'ShowMessage("First Select a Cash Memo for Delete", "Information")
    '            Exit Sub
    '        End If
    '        If CheckInterTransactionAuth("DeleteBill", dsMain.Tables("CASHMEMOHDR")) = True Then
    '            Dim billNo, StrReason As String
    '            calculateTotalbill()
    '            Dim dt As DataTable = objCM.GetReasons("CMS")
    '            Dim objReason As New frmNCommonSearch
    '            objReason.SetData = dt
    '            objReason.ShowDialog()
    '            If objReason.search Is Nothing Then Exit Sub
    '            If Not objReason.search Is Nothing Then
    '                StrReason = objReason.search(0).ToString()
    '            End If
    '            Dim DeletTime As DateTime = objCM.GetCurrentDate()
    '            Dim AuthUser As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("AuthUserId").ToString()
    '            billNo = dsMain.Tables("CASHMEMOHDR").Rows(0)("Billno").ToString()
    '            dsMain.Tables("CASHMEMOHDR").Rows(0)("DeletionReason") = StrReason
    '            dsMain.Tables("CASHMEMOHDR").Rows(0)("BILLINTERMEDIATESTATUS") = "Deleted"
    '            dsMain.Tables("CASHMEMOHDR").Rows(0)("DeletionDate") = DeletTime
    '            dsMain.Tables("CASHMEMOHDR").Rows(0)("DeletionTime") = DeletTime
    '            dsMain.Tables("CASHMEMOHDR").Rows(0)("Updatedon") = DeletTime
    '            dsMain.Tables("CASHMEMOHDR").Rows(0)("Updatedby") = clsAdmin.UserCode
    '            dsMain.Tables("CASHMEMOHDR").Rows(0)("UpdatedAt") = clsAdmin.SiteCode
    '            If objCM.CheckVoucherRedemmed(clsAdmin.SiteCode, billNo, "CMS") = False Then
    '                ShowMessage(getValueByKey("CM058"), "CM058 - " & getValueByKey("CLAE04"))
    '                Exit Sub
    '            End If
    '            If objCM.DeleteBill(dsMain, clsAdmin.SiteCode, clsAdmin.UserCode, clsDefaultConfiguration.CashMemoStorageLocation) = True Then
    '                'dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Clear()
    '                'comboArticleCopy.Rows.Clear()
    '                '_remarks = String.Empty
    '                'isCashierPromoSelected = False
    '                If dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNo").ToString <> "" Then
    '                    Dim totalPoints As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPPoints").ToString()
    '                    If totalPoints = "" Or CDbl(totalPoints) <= 0 Then
    '                        totalPoints = dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPDiscount").ToString()
    '                    End If
    '                    Dim RedemptionPoints As Double = 0
    '                    For Each dr As DataRow In dsMain.Tables("CashMemoReceipt").Select("TenderTypeCode Like 'CLP%'", "", DataViewRowState.CurrentRows)
    '                        RedemptionPoints = IIf(dr("AmountTendered") Is DBNull.Value, 0, dr("AmountTendered"))
    '                    Next
    '                    If (totalPoints <> "" AndAlso CDbl(totalPoints) > 0) Or RedemptionPoints > 0 Then
    '                        If objCM.ReversedCLPPoints(clsAdmin.CLPProgram, dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNo").ToString(), totalPoints, RedemptionPoints, clsAdmin.SiteCode, clsAdmin.UserCode, dsMain.Tables("CASHMEMOHDR").Rows(0)("Billno").ToString(), dsMain.Tables("CASHMEMOHDR").Rows(0)("Billdate").ToString()) = False Then
    '                            ShowMessage(getValueByKey("CM053"), "CM053 - " & getValueByKey("CLAE04"))
    '                            Exit Sub
    '                        End If
    '                        'ElseIf RedemptionPoints > 0 Then
    '                        '    If objCM.UpdateClpPoints(False, clsAdmin.CLPProgram, dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNo").ToString(), RedemptionPoints, clsAdmin.SiteCode, clsAdmin.UserCode, dsMain.Tables("CASHMEMOHDR").Rows(0)("Billno").ToString(), dsMain.Tables("CASHMEMOHDR").Rows(0)("Billdate").ToString(), True) = False Then
    '                        '        ShowMessage("CLPData not reversed", "information")
    '                        '        Exit Sub
    '                        '    End If
    '                    End If
    '                End If
    '                Dim obj As New clsCashMemoPrint(billNo, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo)
    '                Dim ErrorMsg As String = ""
    '                obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "DCM", "", clsAdmin.UserCode, AuthUser, ErrorMsg, "", clsDefaultConfiguration.CompanyName, _remarks)


    '                If ErrorMsg <> String.Empty Then
    '                    ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
    '                End If
    '                ClearData()
    '                cmdNew_Click(cmdNew, e)
    '                AutoLogout(FrmTranCode, Me, lblLoggedIn)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowMessage(getValueByKey("CM040"), "CM040 - " & getValueByKey("CLAE05"))
    '        LogException(ex)
    '        'ShowMessage("Error Deleting Cash Memo", "Error")
    '    End Try
    'End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Try
            objSearch.RequestFromPage = enumOperationOnBill.VoidBill
            Dim authUserID = "", authRemarks As String = ""
            Dim dt As DataTable = objCM.GetOldCashMemo(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID, clsAdmin.UserCode, clsCommon._PrevShiftId, NoOfDaysForReprint:=clsDefaultConfiguration.NumberOfDaysApplicableForReprint, NewCustmor:=clsDefaultConfiguration.DetailedCustomerCreationformat)
            If Not dt Is Nothing Then
                If dt.Rows.Count = 0 Then
                    Exit Sub
                End If
            Else
                Exit Sub
            End If

            If CheckInterTransactionAuth("DeleteBill", dsMain.Tables("CASHMEMOHDR"), authUserID:=authUserID, authRemarks:=authRemarks) = True Then
                IsCancelOldCashMemo = False
                IsBillVoid = False
                objSearch.ArtRemark = ""
                cmdOldCashMemo_Click(Nothing, New EventArgs())
                authRemarks = objSearch.ArtRemark
                If authRemarks = "" Then
                    Exit Sub
                End If
                If IsBillVoid Then
                    IsBillVoid = False
                    Exit Sub
                End If
                If OnlineConnect = False Then
                    ShowMessage(getValueByKey("CMO50"), "CMO50 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
                If UpdateFlag = False Then
                    'Rakesh-22.08.2013:Issue-7597-->Avoid delete message
                    'If (Not IsCancelOldCashMemo) Then
                    '    ShowMessage(getValueByKey("CM039"), "CM039 - " & getValueByKey("CLAE04"))
                    'End If
                    'ShowMessage("First Select a Cash Memo for Delete", "Information")
                    Exit Sub
                End If

                Dim billNo, StrReason As String

                'Dim dt As DataTable = objCM.GetReasons("CMS")
                'Dim objReason As New frmNCommonSearch
                'objReason.SetData = dt
                'objReason.ShowDialog()
                'If objReason.search Is Nothing Then Exit Sub
                'If Not objReason.search Is Nothing Then
                '    StrReason = objReason.search(0).ToString()
                'End If

                billNo = dsMain.Tables("CASHMEMOHDR").Rows(0)("Billno").ToString()

                If objCM.CheckVoucherRedemmed(clsAdmin.SiteCode, billNo, "CMS") = False Then
                    ShowMessage(getValueByKey("CM058"), "CM058 - " & getValueByKey("CLAE04"))

                    Try
                        For index = 1 To dgMainGrid.Rows.Count - 1
                            dgMainGrid.Rows.Remove(1)
                        Next
                    Catch ex As Exception
                    End Try

                    cmdNew_Click(sender, e)
                    Exit Sub
                End If

                calculateTotalbill()

                Dim DeletTime As DateTime = objCM.GetCurrentDate()
                Dim AuthUser As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("AuthUserId").ToString()

                Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
                'dsMain.Tables("CASHMEMOHDR").Rows(0)("DeletionReason") = StrReason
                dsMain.Tables("CASHMEMOHDR").Rows(0)("BILLINTERMEDIATESTATUS") = "Deleted"
                dsMain.Tables("CASHMEMOHDR").Rows(0)("DeletionDate") = DeletTime
                dsMain.Tables("CASHMEMOHDR").Rows(0)("DeletionTime") = DeletTime
                dsMain.Tables("CASHMEMOHDR").Rows(0)("Updatedon") = DeletTime
                dsMain.Tables("CASHMEMOHDR").Rows(0)("Updatedby") = clsAdmin.UserCode
                dsMain.Tables("CASHMEMOHDR").Rows(0)("UpdatedAt") = clsAdmin.SiteCode

                dsMain.Tables("CASHMEMOHDR").Rows(0)("AuthUserId") = authUserID
                dsMain.Tables("CASHMEMOHDR").Rows(0)("AuthUserRemarks") = authRemarks
                Dim ds As DataSet = dsMain.Copy()
                If objCM.DeleteBill(ds, clsAdmin.SiteCode, clsAdmin.UserCode, clsDefaultConfiguration.StockStorageLocation) = True Then
                    'dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Clear()
                    'comboArticleCopy.Rows.Clear()
                    '_remarks = String.Empty
                    'isCashierPromoSelected = False
                    If dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNo").ToString <> "" Then
                        Dim totalPoints As String = dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPPoints").ToString()
                        If totalPoints = "" Or CDbl(totalPoints) <= 0 Then
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
                    'obj.CompositeTaxReqOnPrint = clsDefaultConfiguration.CompositeTaxReqOnPrint

                    Dim ErrorMsg As String = ""
                    ' obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "DCM", "", clsAdmin.UserCode, AuthUser, ErrorMsg, "")
                    obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "DCM", "", clsAdmin.UserCode, AuthUser, ErrorMsg, "", IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6, TokenNoRequiredInKOT:=clsDefaultConfiguration.TokenNoRequiredInKOT) '0000413  code added by irfan on 18/9/2017 visiblity of hsn and tax

                    If ErrorMsg <> String.Empty Then
                        ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
                    End If
                    ClearData()
                    cmdNew_Click(cmdNew, e)
                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
                End If
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
                ' ClearAllPromo()
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
                If objCM.HoldData(clsAdmin.DayOpenDate, dsTemp, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.UserCode, str, BillNo, salesPerson) Then
                    ClearData()
                    'cmdNew_Click(sender, e)
                    If clsDefaultConfiguration.HoldBillPrint = True Then

                        Dim obj As New clsCashMemoPrint(BillNo, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
                        obj.DisplayArticleFullName = clsDefaultConfiguration.PrintItemFullName
                        obj.AllowDecimalQty = clsDefaultConfiguration.AllowDecimalQty
                        obj.WeightScaleEnabled = clsDefaultConfiguration.WeightScaleEnabled
                        'obj.CompositeTaxReqOnPrint = clsDefaultConfiguration.CompositeTaxReqOnPrint

                        Dim ErrorMsg As String = ""
                        obj.HoldCMPrint(clsAdmin.SiteCode, "HoldCM", clsDefaultConfiguration.HoldBillPrintBarcode, ErrorMsg, BarCodeType)
                        If ErrorMsg <> String.Empty Then
                            ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
                        End If
                    End If
                    cmdNew_Click(sender, e)
                    AutoLogout(FrmTranCode, Me, lblLoggedIn)
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
                    objCM.GetHoldData(dsMain, TrnNo, clsAdmin.SiteCode, clsAdmin.UserCode, dtGV)
                    HoldFlag = True
                    'If clsDefaultConfiguration.TaxDetailsRequired = True Then
                    For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Rows
                        Dim TaxableAmt As Double = dr("GrossAmt")
                        If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                            TaxableAmt = dr("GrossAmt") - dr("TotalDiscount")
                        End If

                        CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode").ToString(), TaxableAmt, dr, dr("EAN").ToString())

                    Next
                    'End If

                    ''' calculateTotalbill()
                    CLPCustomerId = IIf(Not dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNO") Is DBNull.Value, dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNO"), String.Empty)

                    If dsMain.Tables("CASHMEMOHDR").Rows.Count > 0 Then
                        Try
                            CLPCustomerId = IIf(Not dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNO") Is DBNull.Value, dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNO"), String.Empty)
                            Dim objCustm As New clsCLPCustomer()
                            Dim _dtCustmInfo As DataTable

                            If (Not String.IsNullOrEmpty(CLPCustomerId)) Then
                                _dtCustmInfo = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CLPCustomerId)

                                If (_dtCustmInfo IsNot Nothing AndAlso _dtCustmInfo.Rows.Count > 0) Then
                                    CustInfo.CtrlTxtCustomerNo.Text = _dtCustmInfo.Rows(0)("CUSTOMERNO").ToString()
                                    CustInfo.CtrltxtCustomerName.Text = _dtCustmInfo.Rows(0)("CustomerName").ToString()
                                    CustInfo.ctrlTxtPoints.Text = _dtCustmInfo.Rows(0)("BalancePoint").ToString()
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


                        Catch ex As Exception
                            LogException(ex)
                        End Try
                    End If

                    ReCalculateCM(String.Empty)
                    calculateTotalbill()

                    If (dgMainGrid.Rows.Count > 1) Then
                        dgMainGrid.Select(dgMainGrid.Rows.Count - 1, 2)
                    End If
                    'Else
                    '    ShowMessage(getValueByKey("CM061"), getValueByKey("CLAE05"))
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
                    'obj.CompositeTaxReqOnPrint = clsDefaultConfiguration.CompositeTaxReqOnPrint
                    'Select Case CustomerSaleType
                    '    Case enumCustomerSaleType.Dine_In
                    '        obj.CustomerSaleType = "Dine In"
                    '    Case enumCustomerSaleType.Home_Delivery
                    '        obj.CustomerSaleType = "Home Delivery"
                    '    Case enumCustomerSaleType.Take_Away
                    '        obj.CustomerSaleType = "Take Away"
                    '    Case Else
                    'End Select

                    Dim ErrorMsg As String = ""
                    'obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Duplicate", "", "", ErrorMsg)
                    '  obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Duplicate", "", "", ErrorMsg, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=True, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsCounterCopyKot:=True, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6) '0000413  code added by irfan on 18/9/2017 visiblity of hsn and tax
                    'modified by khusrao adil on 8-12-2017 for jk sprint 32
                    'JKPrintFormatEnable flag added
                    obj.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Duplicate", "", "", ErrorMsg, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=True, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsCounterCopyKot:=True, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, IsTendersVisibleInPrintFormat7:=clsDefaultConfiguration.IsTendersVisibleInPrintFormat7, IsHsnAndTaxVisibleInPrintFormat6:=clsDefaultConfiguration.IsHsnAndTaxVisibleInPrintFormat6, JKPrintFormatEnable:=clsDefaultConfiguration.JKPrintFormatEnable) '0000413  code added by irfan against tender visblity    code added by irfan on 11/09/2017 visiblity of hsn and tax) '0000413
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
                'CalCulateCLP(CLPCardType, dsMain.Tables("CashMemoDtl"), "CLPRequire=TRUE AND BTYPE='S' And ArticleCode <>'GVBaseArticle' AND ArticleCode <>'CLPBaseArticle' ")
                CalCulateCLPSlabwise(CLPCardType, dsMain.Tables("CashMemoDtl"), "CLPRequire=TRUE AND BTYPE='S' And ArticleCode <>'GVBaseArticle' AND ArticleCode <>'CLPBaseArticle' ", CLPCustomerId, dtpayment)
                If UpdateFlag = False Then
                    ReCalculateCM("")
                End If
                calculateTotalbill()

                Dim totalpoints As Double
                totalpoints = IIf(dsMain.Tables("CashMemoDtl").Compute("SUM(CLPPoints)", "") Is DBNull.Value, 0, dsMain.Tables("CashMemoDtl").Compute("SUM(CLPPoints)", ""))
                totalpoints = totalpoints + CustomerBalancePoint
                'comment for demo by vaibhav 22/08/2011
                'GenerateGiftVoucherdata(totalpoints, clsAdmin.CLPProgram, clsDefaultConfiguration.CLPVoucherPercentage)
                CustomerBalancePoint = totalpoints
                'lblBalPoint.Text = dsMain.Tables("CashMemoDtl").Compute("SUM(CLPPoints)", "")
                'CustInfo.ctrlTxtPoints.Text = dsMain.Tables("CashMemoDtl").Compute("SUM(CLPPoints)", "")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub GenerateGiftVoucherdata(ByRef points As Double, ByVal clpprogramid As String, ByVal percentage As Int32)
        Try
            Dim _incrementNo As Int32 = 0
            Dim totalPoint As Double = points
            Dim dtDenomination As DataTable = objCM.GetCLPVoucherDenominationDetail(clsAdmin.CLPProgram, clsAdmin.SiteCode)
            If Not dtDenomination Is Nothing Then
                For Each dr As DataRow In dtDenomination.Rows
                    dr("Quantity") = 0
Recur:
                    Dim voucherAmount As Double = dr("DenominationAmt")
                    If (voucherAmount * percentage) <= totalPoint Then
                        dr("Quantity") = dr("Quantity") + 1
                        totalPoint = totalPoint - (voucherAmount * percentage)
                        GoTo Recur
                    End If
                Next
                Dim dv As New DataView(dtDenomination, "Quantity>0", "Quantity", DataViewRowState.CurrentRows)
                If dv.Count > 0 Then
                    For Each drv As DataRowView In dv
                        If dtGV Is Nothing Then
                            Dim obj As New clsAdvanceSale()
                            dtGV = obj.GetVoucherStru()
                            dtGV.TableName = "VOUCHERDTLS"
                        End If
                        Dim dr As DataRow = dtGV.NewRow()
                        dr("SITECODE") = clsAdmin.SiteCode
                        dr("VOUCHERCODE") = drv("VOUCHERCODE")
                        'dr("VOUCHERDESC") = cmbVoucherProgram.Text.Trim
                        dr("Quantity") = drv("Quantity")
                        Dim expiryDays As Object = drv("ExpiryAfterDays")
                        'For Each dvViewRow As DataRowView In dvView
                        '    If Not dvViewRow("ExpiryAfterDays") Is DBNull.Value Then
                        '        expiryDays = dvViewRow("ExpiryAfterDays")
                        '    End If
                        'Next
                        dr("VALUEOFVOUCHER") = drv("DenominationAmt")
                        dr("ISACTIVE") = 1
                        dr("ISISSUED") = 1
                        'dr("NetAmount") = dr("Quantity") * dr("VALUEOFVOUCHER")
                        dr("ISSUEDATSITE") = clsAdmin.SiteCode
                        dr("ISSUEDONDATE") = clsAdmin.DayOpenDate.Date
                        dr("IssuedDocNumber") = _incrementNo + 1
                        '_incrementNo = _incrementNo + 1
                        'If IsBirthListGV Then
                        If Not expiryDays Is Nothing Then
                            dr("ExpiryInDays") = CInt(expiryDays)
                            dr("ExpiryDate") = DateAdd(DateInterval.Day, expiryDays, Now)
                        Else
                            dr("ExpiryInDays") = 0
                            dr("ExpiryDate") = DateAdd(DateInterval.Day, 0, Now)
                        End If
                        'dr("ISSUEDATSITE") = clsAdmin.SiteCode
                        'dr("ISSUEDONDATE") = clsAdmin.CurrentDate
                        dr("ISSUEDINDOCTYPE") = "CMS"
                        'End If

                        dtGV.Rows.Add(dr)
                        updateCustomerpoint = True
                        points = totalPoint
                    Next
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub cmdShowMoreInfo() Handles CustInfo.Click
        Try
            If Not CustInfo.CtrlTxtSwape.Text = String.Empty Then
                Dim objClpCust As New clsCLPCustomer
                Dim dtCust As New DataTable
                Dim strSql As String = " "
                dtCust = objClpCust.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CustInfo.CtrlTxtSwape.Text)
                If Not dtCust Is Nothing AndAlso dtCust.Rows.Count > 0 Then
                    Dim objfrmCustomerDeatils As New frmCustomerDetails(dtCust)
                    objfrmCustomerDeatils.ShowDialog()
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub CtrlBtn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim articleCode As Spectrum.CtrlBtn = DirectCast(sender, Spectrum.CtrlBtn)
            If articleCode.Text <> String.Empty Or articleCode.Image IsNot Nothing Then
                Dim dtable As New DataTable

                If articleCode.SetArticleCode = String.Empty Then
                    dtable = objCM.GetButtonArticle(articleCode.Name)
                    articleCode.SetArticleCode = dtable.Rows(0)("ArticleCode")
                    dtable.Clear()
                End If
                Dim dt As New DataTable
                Dim cdr As New DataTable
                cdr = objCM.GetComponentArticleItems(articleCode.SetArticleCode)

                If cdr IsNot Nothing AndAlso cdr.Rows.Count > 0 Then
                    For index = 0 To cdr.Rows.Count - 1

                        'dt = objCM.GetItemDetails(clsAdmin.SiteCode, cdr(index)("ArticleCode"), False, clsAdmin.LangCode)
                        'setgridview(dt)
                        'dt.Clear()

                        dt = objCM.GetItemDetails(clsAdmin.SiteCode, cdr(index)("ArticleCode"), False, clsAdmin.LangCode)
                        dt(0)("Quantity") = cdr(index)("Quantity")
                        compqty = cdr(index)("Quantity")
                        flag = 3
                        setgridview(dt)
                        dt.Clear()
                    Next
                Else
                    dt = objCM.GetItemDetails(clsAdmin.SiteCode, articleCode.SetArticleCode, False, clsAdmin.LangCode)
                    setgridview(dt)
                End If

            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Dim comboArticleCopy As New DataTable
    Public Sub ModMenuCtrlBtn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'code added by vipul for customer wise discount
            If clsDefaultConfiguration.customerwisepricemanagement = True Then
                'If (String.IsNullOrEmpty(CustInfo.CtrlTxtCustomerNo.Text.Trim)) Then
                '    ShowMessage(getValueByKey("CHKP11"), getValueByKey("CLAE04"))
                '    CtrlSalesPersons.AndroidSearchTextBox.Clear()
                '    CtrlSalesPersons.AndroidSearchTextBox.Select()
                '    Exit Sub
                'End If
            End If


            If clsDefaultConfiguration.DineInProcess Then
                Using objfrmAuth As New frmNUserAuthorisation
                    IsMergeOrderBillNo()
                    If currentDineInTable > 0 Then
                        cmdGenerateKOT.Enabled = True
                        Dim status = objCM.GetOrderStatus(clsAdmin.SiteCode, clsAdmin.DayOpenDate, currentDineInBillNo)
                        If status = 1 AndAlso BillGenerateStatus = 0 Then

                            objfrmAuth.IsRemarkEnable = True
                            If CheckInterTransactionAuth("DeleteBill", Nothing) = True Then
                                BillGenerateStatus = 2
                            Else
                                Exit Sub
                            End If
                        End If
                        DineInAutoSave = True
                    End If
                    If mergeIdForGenerateBill > 0 Then
                        'CtrlSalesPersons.CtrlTxtBox.Select()
                        'CtrlSalesPersons.CtrlTxtBox.Focus()
                        CtrlSalesPersons.AndroidSearchTextBox.Select()
                        CtrlSalesPersons.AndroidSearchTextBox.Focus()
                        Exit Sub
                    End If
                End Using
            End If
            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType AndAlso CustomerSaleType = 0 Then
                'CtrlSalesPersons.CtrlTxtBox.Select()
                'CtrlSalesPersons.CtrlTxtBox.Focus()
                CtrlSalesPersons.AndroidSearchTextBox.Select()
                CtrlSalesPersons.AndroidSearchTextBox.Focus()
                Exit Sub
            End If

            If clsDefaultConfiguration.SalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0 Then
                ShowMessage(getValueByKey("CM025"), "CM025 - " & getValueByKey("CLAE04"))
                'ShowMessage("Please Select the Sales Person", "information")
                CtrlSalesPersons.CtrlSalesPersons.Select()
                Exit Sub
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

            Dim articleCode As Spectrum.CtrlBtn = DirectCast(sender, Spectrum.CtrlBtn)
            If articleCode.Tag IsNot Nothing Or articleCode.Image IsNot Nothing Then
                'Dim articleCodeNumber As String = articleCode.Tag              
                'articleCode.SetArticleCode = articleCodeNumber
                'Dim dtable As New DataTable

                'If articleCode.SetArticleCode = String.Empty Then
                '    dtable = objCM.GetButtonArticle(articleCode.Name)
                '    articleCode.SetArticleCode = dtable.Rows(0)("ArticleCode")
                '    dtable.Clear()
                'End If
                Dim dt As New DataTable
                Dim cdr As New DataTable
                cdr = objCM.GetComponentArticleItems(articleCode.SetArticleCode)

                If cdr IsNot Nothing AndAlso cdr.Rows.Count > 0 Then
                    For index = 0 To cdr.Rows.Count - 1

                        'dt = objCM.GetItemDetails(clsAdmin.SiteCode, cdr(index)("ArticleCode"), False, clsAdmin.LangCode)
                        'setgridview(dt)
                        'dt.Clear()

                        dt = objCM.GetItemDetails(clsAdmin.SiteCode, cdr(index)("ArticleCode"), False, clsAdmin.LangCode)
                        dt(0)("Quantity") = cdr(index)("Quantity")
                        compqty = cdr(index)("Quantity")
                        flag = 3
                        setgridview(dt)
                        dt.Clear()
                    Next
                Else
                    dt = objCM.GetItemDetails(clsAdmin.SiteCode, articleCode.SetArticleCode, False, clsAdmin.LangCode)
                    'code added by vipul for issue id 2736
                    If dt IsNot Nothing And dt.Rows.Count = 1 Then
                        If CheckKitArticleCodeInMstArticleKit(dt, articleCode.SetArticleCode) = False Then
                            ShowMessage("Article not present in the Kit.", getValueByKey("CLAE04"))
                            Exit Sub
                        End If
                    End If
                    HandleComboArticles(dt)
                End If
                'cmdGenerateBill.Enabled = True

            End If

            If SpectrumCommon.ComboItemPrintingAllowed Then
                dgMainGrid.AutoSizeRows()
                
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
                        If Not articleCode.SetArticleCode = "" Then
                            If articleCode.SetArticleCode = dtMembData.Rows(0)("ServiceCode").ToString() Then
                                Dim mainpromoId = dtMembDatapromo.Select("IsMainPromo=True")
                                obj.CalculatePromotionsByCustomer(dsMain, clsAdmin.SiteCode, mainpromoId(0)("PromotionId"))
                            Else
                                If servicearticle.Rows.Count > 0 Then
                                    Dim result = servicearticle.Select("articlecode='" + articleCode.SetArticleCode + "' or Ean='" + articleCode.SetArticleCode + "'")
                                    If result.Length = 0 Then
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

            ' dgMainGrid.GetData(selectedrow, 2)
            If clsDefaultConfiguration.ExtendScreen Then
                If clsDefaultConfiguration.HariOmExtendScreen = True Then
                    TwoLineExtendScreen()
                Else
                    ExtendFun()
                End If
            End If
            If clsDefaultConfiguration.IsPoleDisply = True Then
                Dim selectedrow As Integer = dgMainGrid.RowSel - 1
                TwoLineExtendScreen(selectedrow)
            End If

            If clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType Then
                CtrlSalesPersons.AndroidSearchTextBox.Select()
                CtrlSalesPersons.AndroidSearchTextBox.Focus()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub ctrlBack_Click(sender As Object, e As EventArgs) Handles ctrlBack.Click
        Try
            CtrlMODMenu1.ClearChildArticles(parentGroup)
            Dim dt As New DataTable


            Dim popElement = subGroupsDictionary.Pop()
            LastPushPop = popElement
            dt = dtAllSubGroups.Select("ParentGroupId='" + popElement.Trim + "'").CopyToDataTable()
            If popElement = parentGroup Then
                ctrlBack.Visible = False
            End If

            CtrlMODMenu1.CreateSubButtonsInTabPanelByGroup(dt, parentGroup)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub tabControlMain_Click(sender As Object, e As EventArgs)
        Try

            'sender.SelectedTab.TabBackColorSelected = ColorTranslator.FromOle(RGB(255, 192, 128))
            CtrlMODMenu1.Rowcount = 1
            CtrlMODMenu1.isFirstArticle = False
            'ctrTablIndex.Add(Me, 0)
            LastPushPop = ""
            subGroupsDictionary.Clear()
            parentGroup = ""
            ctrlBack.Visible = False

            Dim dt As New DataTable
            Dim dtParent As New DataTable
            Dim IsLastSubgroup = objCM.IsLastSubGroup(clsAdmin.SiteCode, sender.SelectedTab.Tag.ToString().Trim)
            parentGroup = sender.SelectedTab.Tag.ToString().Trim
            dtAllSubGroups = New DataTable

            If IsLastSubgroup = False Then

                '''' Here i am searching and hiding panels and show 

                dt = dtArticles.Select("GroupID='" + parentGroup.Trim + "'").CopyToDataTable() ' objCM.GetButtonArticleByGroup(clsAdmin.SiteCode, parentGroup.Trim)
                CtrlMODMenu1.isFirstArticle = True
                If dt.Rows.Count > 0 Then
                    CtrlMODMenu1.Rowcount = Math.Ceiling(dt.Rows.Count / 6)
                End If
                CtrlMODMenu1.LoadArticleByGroup(dt, parentGroup, parentGroup)

            Else
                LastPushPop = parentGroup
                'subGroupsDictionary.Push(parentGroup)
                dtAllSubGroups = objCM.GetSubGroupsByGroup(clsAdmin.SiteCode, sender.SelectedTab.Tag.ToString().Trim)
                If dtAllSubGroups IsNot Nothing Then
                    If dtAllSubGroups.Rows.Count > 0 Then
                        dtParent = dtAllSubGroups.Select("ParentGroupID='" + sender.SelectedTab.Tag.ToString().Trim + "'").CopyToDataTable()
                        If dtParent.Rows.Count > 0 Then
                            CtrlMODMenu1.Rowcount = Math.Ceiling(dtParent.Rows.Count / 6)
                        End If
                        CtrlMODMenu1.CreateSubButtonsInTabPanelByGroup(dtParent, parentGroup)
                    End If
                End If


            End If




        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub ModMenuCtrlBtn1_Leave(sender As Object, e As EventArgs)
        If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            sender.backcolor = Color.FromArgb(49, 49, 49)
        End If
    End Sub
    Public Sub ModMenuSubCtrlBtn1_Click(sender As Object, e As EventArgs)
        Try
            Dim subDt As New DataTable
            Dim dtArt As New DataTable
            CtrlMODMenu1.Rowcount = 1
            Dim articleCode As Spectrum.CtrlBtn = DirectCast(sender, Spectrum.CtrlBtn)
            If articleCode.Tag IsNot Nothing Or articleCode.Image IsNot Nothing Then
                Dim articleCodeNumber As String = articleCode.Tag


                Dim drHdr() = dtAllSubGroups.Select("ParentGroupID='" + articleCodeNumber + "' ")
                If drHdr.Count = 0 Then
                    dtArt = dtArticles.Select("GroupID='" + articleCodeNumber.Trim + "'").CopyToDataTable() ' objCM.GetButtonArticleByGroup(clsAdmin.SiteCode, articleCodeNumber.Trim)
                    'subGroupsDictionary.Push(LastPushPop)
                    LastPushPop = articleCodeNumber
                    CtrlMODMenu1.LoadArticleByGroup(dtArt, parentGroup, articleCodeNumber)
                    ctrlBack.Visible = True
                Else
                    subDt = dtAllSubGroups.Select("ParentGroupID='" + articleCodeNumber.Trim + "'").CopyToDataTable()
                    ctrlBack.Visible = True
                    If subDt.Rows.Count > 0 Then

                        CtrlMODMenu1.Rowcount = Math.Ceiling(subDt.Rows.Count / 6)

                        subGroupsDictionary.Push(LastPushPop)
                        LastPushPop = articleCodeNumber

                        CtrlMODMenu1.CreateSubButtonsInTabPanelByGroup(subDt, parentGroup)
                    End If

                End If



            End If
            'Dim dt As New DataTable


        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub HandleComboArticles(ByRef dt As DataTable)
        Try
            'Dim multipleMrpItem As DataTable = objCM.GetMultipleMrpItem(clsAdmin.SiteCode, dt.Rows(0)("ArticleCode"))
            'If multipleMrpItem IsNot Nothing AndAlso multipleMrpItem.Rows.Count > 1 Then
            '    Dim objFrmMultiplePrice As New frmMulipleSellingPrice
            '    objFrmMultiplePrice.MultipleMrpItems = multipleMrpItem
            '    objFrmMultiplePrice.ShowDialog()
            '    If objFrmMultiplePrice.SelectedPrice IsNot Nothing Then
            '        dt.Rows(0)("SellingPrice") = objFrmMultiplePrice.SelectedPrice("SellingPrice")
            '    Else
            '        Exit Sub
            '    End If
            'End If
            If dt.Rows(0)("Printable") = 0 AndAlso dt.Rows(0)("SELLINGPRICE") <> 0 Then
                ShowMessage(getValueByKey("frmfastcashmemo.articledatachkmsg"), getValueByKey("CLAE04"))
                Exit Sub
            End If
            Dim articleComboPopup As ArticleComboPopup
            If dt.Rows(0)("ARTICLETYPE") = "Combo" Then
                articleComboPopup = New ArticleComboPopup(False)
                Dim dtCombo As DataTable
                dtCombo = objArticleCombo.GetComboDetails(dt.Rows(0)("ArticleCode"))
                If Not dtCombo Is Nothing AndAlso dtCombo.Rows.Count > 0 Then
                    articleComboPopup.ComboDetails = dtCombo
                    articleComboPopup.ShowDialog()
                End If
            End If
            If articleComboPopup Is Nothing Then
                'dt.Rows(0)("Discription") = "Mod 12" & vbCrLf & "MyModOriginal" & vbCrLf & "Double Trouble"
                setgridview(dt)
            ElseIf Not articleComboPopup.SelectedComboArticles Is Nothing AndAlso articleComboPopup.SelectedComboArticles.Rows.Count > 0 Then
                'dt.Rows(0)("Quantity") = articleComboPopup.ComboQuantity
                'dt.Rows(0)("SellingPrice") = dt.Rows(0)("SellingPrice") * dt.Rows(0)("Quantity")
                Dim originalSellingPrice = dt.Rows(0)("SellingPrice")
                If articleComboPopup.AdditionalComboCost > 0 Then
                    dt.Rows(0)("SellingPrice") = dt.Rows(0)("SellingPrice") + (articleComboPopup.AdditionalComboCost / articleComboPopup.ComboQuantity)
                End If

                Dim scanArtilceRow = articleComboPopup.ComboDetails.Compute("Sum(Cost)", "ComboCode='" & dt.Rows(0)("ArticleCode") & "'")
                If (scanArtilceRow IsNot Nothing And scanArtilceRow IsNot DBNull.Value) Then
                    dt.Rows(0)("CostPrice") = scanArtilceRow
                End If
                If Not articleComboPopup.SelectedComboArticles Is Nothing AndAlso articleComboPopup.SelectedComboArticles.Rows.Count > 0 Then 'vipin
                    GSTComboArticleCopy = articleComboPopup.SelectedComboArticles
                End If
                Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
                articleDescriptionDictionary.Add(dt.Rows(0)("Discription"), 0)
                GetArticleDecriptionDictionary(articleDescriptionDictionary, articleComboPopup.SelectedComboArticles)
                dt.Rows(0)("Discription") = GetMultilinedString(articleDescriptionDictionary)
                setgridview(dt, True)
                'For i As Integer = 1 To articleComboPopup.ComboQuantity - 1 Step 1
                '    dt.Rows(0)("SellingPrice") = originalSellingPrice + (articleComboPopup.AdditionalComboCost / articleComboPopup.ComboQuantity)
                '    setgridview(dt)
                'Next
                If articleComboPopup.ComboQuantity > 1 Then
                    _iArticleQtyBeforeChange = dsMain.Tables("CASHMEMODTL").Rows(dsMain.Tables("CASHMEMODTL").Rows.Count - 1)("Quantity")
                    dsMain.Tables("CASHMEMODTL").Rows(dsMain.Tables("CASHMEMODTL").Rows.Count - 1)("Quantity") = articleComboPopup.ComboQuantity
                    Dim index As Int32 = dgMainGrid.Cols("Quantity").Index
                    dgMainGrid_AfterEdit(dgMainGrid, New C1.Win.C1FlexGrid.RowColEventArgs(dgMainGrid.Row, index))
                End If
                CheckingComboCode()
                For Each dataRow In articleComboPopup.SelectedComboArticles.Rows
                    AddComboArticles(dataRow, dsMain.Tables("CASHMEMODTL").Rows.Count - 1)
                Next
                comboArticleCopy.Merge(articleComboPopup.SelectedComboArticles)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub GetArticleDecriptionDictionary(ByRef articleDescriptionDictionary As Dictionary(Of String, Integer), ByRef dataTable As DataTable)
        Try
            For Each Row In dataTable.Rows
                If articleDescriptionDictionary.ContainsKey(Row("DISCRIPTION")) Then
                    articleDescriptionDictionary(Row("DISCRIPTION")) = articleDescriptionDictionary(Row("DISCRIPTION")) + Row("Quantity")
                Else
                    articleDescriptionDictionary.Add(Row("DISCRIPTION"), Row("Quantity"))
                End If
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub CtrlBtn1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If (e.Button = Windows.Forms.MouseButtons.Right) Then

                ArticleButtonName = New Spectrum.CtrlBtn
                ArticleButtonName = DirectCast(sender, Spectrum.CtrlBtn)
                If ArticleButtonName.Text <> String.Empty Or ArticleButtonName.Image IsNot Nothing Then
                    'CtrlCMbtnArticle.ArticleContextMenuStrip.Items(1).Visible = True
                    'CtrlCMbtnArticle.ArticleContextMenuStrip.Items(2).Visible = True
                    'CtrlCMbtnArticle.ArticleContextMenuStrip.Items(0).Visible = False
                Else
                    'CtrlCMbtnArticle.ArticleContextMenuStrip.Items(0).Visible = True
                    'CtrlCMbtnArticle.ArticleContextMenuStrip.Items(1).Visible = False
                    'CtrlCMbtnArticle.ArticleContextMenuStrip.Items(2).Visible = False
                End If

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub ArticleContextMenuStrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)
        Try
            Dim buttonId As String = DirectCast(sender, ContextMenuStrip).SourceControl.Tag
            ArticleButtonName = New Spectrum.CtrlBtn
            Dim openMrp As Boolean = False

            Cursor.Current = Cursors.WaitCursor
            Dim dt As New DataTable

            If e.ClickedItem.Text = "Add" Then
                If clsDefaultConfiguration.SalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0 Then
                    ShowMessage(getValueByKey("CM025"), "CM025 - " & getValueByKey("CLAE04"))
                    CtrlSalesPersons.CtrlSalesPersons.Select()
                    Exit Sub
                End If

                Dim obj As New frmNItemSearch
                obj.ShowDialog()

                If Not obj.SearchResult Is Nothing Then
                    dt = objCM.GetItemDetails(clsAdmin.SiteCode, obj.ItemRow("ArticleCode").ToString(), openMrp, clsAdmin.LangCode)
                    addArticle(ArticleButtonName, dt)
                    'objCM.SaveAndDeleteButtonArticleData(ArticleButtonName.Name, obj.ItemRow("ArticleCode").ToString(), obj.ItemRow("DISCRIPTION").ToString(), e.ClickedItem.Text)

                End If
            ElseIf e.ClickedItem.Text = "Edit" Then
                If clsDefaultConfiguration.SalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0 Then
                    ShowMessage(getValueByKey("CM025"), "CM025 - " & getValueByKey("CLAE04"))
                    CtrlSalesPersons.CtrlSalesPersons.Select()
                    Exit Sub
                End If
                If objCM.CheckIFGlNoRangeIsAvailable(clsAdmin.SiteCode) = False Then
                    ShowMessage("Please contact system administrator", getValueByKey("CLAE04"))
                    Exit Sub
                End If
                Dim obj As New frmNItemSearch
                obj.ShowDialog()

                If Not obj.SearchResult Is Nothing Then
                    If Not CtrlMODMenu1.CheckIfArticleExistInActibeTab(obj.ItemRow("ArticleCode").ToString()) Then
                        dt = objCM.GetItemDetails(clsAdmin.SiteCode, obj.ItemRow("ArticleCode").ToString(), openMrp, clsAdmin.LangCode)
                        addArticle(ArticleButtonName, dt)
                        objCM.SaveAndDeleteButtonArticleData(buttonId, CtrlMODMenu1.tabControlMain.SelectedTab.Tag, obj.ItemRow("ArticleCode").ToString(), obj.ItemRow("DISCRIPTION").ToString(), e.ClickedItem.Text, clsAdmin.UserCode, clsAdmin.SiteCode)
                        RefreshTabs()
                    Else
                        DisplayArticleWarningMessage()
                    End If
                End If
            ElseIf e.ClickedItem.Text = "Remove" Then
                If objCM.CheckIFGlNoRangeIsAvailable(clsAdmin.SiteCode) = False Then
                    ShowMessage("Please contact system administrator", getValueByKey("CLAE04"))
                    Exit Sub
                End If
                addArticle(ArticleButtonName, dt, e.ClickedItem.Text)
                objCM.SaveAndDeleteButtonArticleData(buttonId, CtrlMODMenu1.tabControlMain.SelectedTab.Tag, String.Empty, String.Empty, e.ClickedItem.Text, clsAdmin.UserCode, clsAdmin.SiteCode)
                RefreshTabs()
            End If

            Cursor.Current = Cursors.Default
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub FlowPanelMenuStrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)
        If objCM.CheckIFGlNoRangeIsAvailable(clsAdmin.SiteCode) = False Then
            ShowMessage("Please contact system administrator", getValueByKey("CLAE04"))
            Exit Sub
        End If
        ArticleButtonName = New Spectrum.CtrlBtn
        Dim openMrp As Boolean = False

        Cursor.Current = Cursors.WaitCursor
        Dim dt As New DataTable

        If e.ClickedItem.Text = "Add" Then
            If clsDefaultConfiguration.SalesPersonApplicable = True AndAlso CtrlSalesPersons.CtrlSalesPersons.SelectedIndex < 0 Then
                ShowMessage(getValueByKey("CM025"), "CM025 - " & getValueByKey("CLAE04"))
                CtrlSalesPersons.CtrlSalesPersons.Select()
                Exit Sub
            End If

            Dim obj As New frmNItemSearch
            obj.ShowDialog()

            If Not obj.SearchResult Is Nothing Then
                If Not CtrlMODMenu1.CheckIfArticleExistInActibeTab(obj.ItemRow("ArticleCode").ToString()) Then
                    dt = objCM.GetItemDetails(clsAdmin.SiteCode, obj.ItemRow("ArticleCode").ToString(), openMrp, clsAdmin.LangCode)
                    addArticle(ArticleButtonName, dt)
                    If objCM.SaveAndDeleteButtonArticleData(0, CtrlMODMenu1.tabControlMain.SelectedTab.Tag, obj.ItemRow("ArticleCode").ToString(), obj.ItemRow("DISCRIPTION").ToString(), e.ClickedItem.Text, clsAdmin.UserCode, clsAdmin.SiteCode) = False Then
                        ShowMessage("Please contact system administrator", getValueByKey("CLAE04"))
                    End If
                    RefreshTabs()
                Else
                    DisplayArticleWarningMessage()
                End If
                'CtrlMODMenu1.AddArticleButton(obj.ItemRow("ArticleCode").ToString(), obj.ItemRow("DISCRIPTION").ToString(),)
            End If
        End If
    End Sub
    Private Sub DisplayArticleWarningMessage()
        'MessageBox.Show("Article already exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        ShowMessage("Article already exist", "Warning")
    End Sub
    Private Sub RefreshTabs()
        CtrlMODMenu1.ActiveTabId = CtrlMODMenu1.tabControlMain.SelectedTab.Tag
        Dim btnGroup As DataTable
        Dim btnArticle As New DataTable
        btnGroup = objCM.GetButtonGroup(clsAdmin.SiteCode)
        btnArticle = objCM.GetButtonArticle(clsAdmin.SiteCode)
        CtrlMODMenu1.ButtonGroup = btnGroup
        CtrlMODMenu1.ImageInfo = btnArticle
        CtrlMODMenu1.LoadArticle()
    End Sub
    Private Sub addArticle(ByVal sender As Spectrum.CtrlBtn, ByRef dtTemp As DataTable, Optional ByVal remove As String = "")
        Try
            Dim btnName As Spectrum.CtrlBtn = DirectCast(sender, Spectrum.CtrlBtn)
            Dim df As String = ""
            If remove <> String.Empty Then
                btnName.Text = ""
                btnName.Image = Nothing
                btnName.SetArticleCode = ""
            Else
                btnName = DirectCast(sender, Spectrum.CtrlBtn)
                btnName.Text = dtTemp.Rows(0)("DISCRIPTION")
                'btnName.Image = Image.FromFile(objCM.GetArticleImage(dtTemp.Rows(0)("ArticleCode"), clsAdmin.Articleimagepath))


                ShowArticleImage(dtTemp.Rows(0)("ArticleCode"), btnName)
                btnName.SetArticleCode = dtTemp.Rows(0)("ArticleCode")

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
            If dgMainGrid.Cols(e.Col).Name = "QUANTITY" AndAlso dgMainGrid.Rows(e.Row)("BTYPE") <> "R" Then
                If objArticleCombo.CheckIfComboArticle(dgMainGrid.Rows(e.Row)("ArticleCode")) Then
                    e.Cancel = True
                    'ShowMessage("Quantity change is not allowed for combo articles", "Information")
                    Exit Sub
                End If
                _iArticleQtyBeforeChange = dgMainGrid.Rows(e.Row)("Quantity")
                'dgMainGrid.Rows(e.Row)(dgMainGrid.Col) = 0
                ReCalculateCM("")
                calculateTotalbill()
                If clsDefaultConfiguration.AllowOnScreenKeyBoard Then
                    OnTouchKeyBoard()
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Protected Overrides Function ProcessKeyPreview(ByRef m As System.Windows.Forms.Message) As Boolean
        Const WM_KEYDOWN As Integer = &H100
        If m.Msg = WM_KEYDOWN Then
            Select Case m.WParam.ToInt32
                Case Keys.F
                    If My.Computer.Keyboard.CtrlKeyDown AndAlso CtrlSalesPersons.CtrlCmdSearch.Enabled Then
                        'Debug.Print("Ctrl + F")
                        cmdSearch_Click(CtrlSalesPersons.CtrlTxtBox, New KeyEventArgs(Keys.Enter))
                    End If
                Case Keys.M
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        'Debug.Print("Ctrl + M")
                        cmdShowMoreInfo()
                    End If
                Case Keys.B
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        'Debug.Print("Ctrl + B") change enable disable manual promotion
                        Dim e1 As New System.EventArgs
                        cmdEnable_Click(cmdEnable, e1)
                    End If
            End Select
        End If
        Return MyBase.ProcessKeyPreview(m)
    End Function

    Private Sub PriceChange()
        Try
            If UpdateFlag = False Then
                Dim lastRowIndex As Integer = dgMainGrid.Row

                dgMainGrid.FinishEditing()
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
                            frm.IsNumeric = True
                            frm.AllowDecimal = True
                            Dim i As Decimal
                            frm.ShowDialog()

                            If frm.GetResult IsNot Nothing Then
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

                            dgMainGrid.Select(lastRowIndex, 2)
                        End If
                    End If
                End If
            End If

            CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.CtrlTxtBox.Focus()

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
                    'dgMainGrid_AfterEdit(dgMainGrid, New C1.Win.C1FlexGrid.RowColEventArgs(lastRowIndex, ColIndex)) 'comment by sagar for sabbaro issue after changing salestype tax amount is wrongly calculated.
                    _PriceBeforeChange = Nothing

                Next RowIndex
                IsChangeQuantityOrPrice = False
                dgMainGrid.FinishEditing()
                ReCalculateCM("")
                calculateTotalbill()
                dgMainGrid.Select(lastRowIndex, 2)
                CtrlSalesPersons.CtrlTxtBox.Select()
                CtrlSalesPersons.CtrlTxtBox.Focus()

            End If
            Exit Sub
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub QuantityChanged(ByVal enteredQuantity As String)
        Try
            If objArticleCombo.CheckIfComboArticle(dgMainGrid.Rows(dgMainGrid.Row)("ArticleCode")) = False Then

                If (dgMainGrid.Rows(dgMainGrid.Row)("ArticleCode").Equals("GVBaseArticle")) Then
                    ShowMessage(getValueByKey("GVS06"), "GVS06 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If

                Dim EventType As Int32 = 0
                IsChangeQuantityfromNumPad = False
                IsChangeQuantityOrPrice = False

                Dim discountAmount As Decimal = Decimal.Zero
                discountAmount = dsMain.Tables("CASHMEMODTL").Compute("Sum(TotalDiscount)", String.Empty)
                If clsDefaultConfiguration.IsMembership Then discountAmount = Decimal.Zero
                If (discountAmount > 0) Then
                    'code added  by vipul for Customer wise price
                    If clsDefaultConfiguration.customerwisepricemanagement = True Then
                        EventType = 1
                    Else
                        ShowMessage(getValueByKey("CM064"), getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
                    End If
                Else
                    EventType = 1
                End If

                If EventType = 1 Then
                    Dim quantity As Decimal

                    If Not String.IsNullOrEmpty(enteredQuantity) AndAlso Decimal.TryParse(enteredQuantity, quantity) AndAlso dgMainGrid.Rows.Count > 1 Then
                        IsChangeQuantityfromNumPad = True
                        IsChangeQuantityOrPrice = True
                        If (discountAmount > 0) Then
                            If clsDefaultConfiguration.customerwisepricemanagement = False Then
                                cmdClrAllPromo_Click("ClearPromWithoutMessage", EventArgs.Empty)
                            End If
                        End If

                        _iArticleQtyBeforeChange = dgMainGrid.Rows(dgMainGrid.Row)("Quantity")
                        dgMainGrid.Rows(dgMainGrid.Row)("Quantity") = quantity
                        'If clsDefaultConfiguration.DineInProcess Then
                        '    cmdGenerateKOT.Enabled = True
                        'End If
                        Dim index As Int32 = dgMainGrid.Cols("SellingPrice").Index
                        dgMainGrid_AfterEdit(dgMainGrid, New C1.Win.C1FlexGrid.RowColEventArgs(dgMainGrid.Row, index))
                    End If
                End If
            Else
                'commented by khusrao adil for revert purpose on 18-jan-2017
                Dim dtCombo As DataTable
                dtCombo = objArticleCombo.GetComboDetails(dgMainGrid.Rows(dgMainGrid.Row)("ARTICLECODE"))
                If Not dtCombo Is Nothing AndAlso dtCombo.Rows.Count > 0 Then
                    dgMainGrid_RowDoubleClick(Nothing, Nothing)
                Else

                    Dim quantity As Decimal
                    If Not String.IsNullOrEmpty(enteredQuantity) AndAlso Decimal.TryParse(enteredQuantity, quantity) AndAlso dgMainGrid.Rows.Count > 1 Then
                        IsChangeQuantityfromNumPad = True
                        IsChangeQuantityOrPrice = True
                        _iArticleQtyBeforeChange = dgMainGrid.Rows(dgMainGrid.Row)("Quantity")

                        If dgMainGrid.Cols("TAKEAWAYQUANTITY").Visible Then
                            If Not IsDBNull(dgMainGrid.Rows(dgMainGrid.Row)("TAKEAWAYQUANTITY")) Then
                                If dgMainGrid.Rows(dgMainGrid.Row)("TAKEAWAYQUANTITY") <> Nothing AndAlso dgMainGrid.Rows(dgMainGrid.Row)("TAKEAWAYQUANTITY") > 1 Then
                                    If dgMainGrid.Rows(dgMainGrid.Row)("TAKEAWAYQUANTITY") > quantity Then
                                        ShowMessage("Take Away Quantity cannot be greater than Total Quantity", getValueByKey("CLAE04"))
                                        dgMainGrid.Rows(dgMainGrid.Row)("TAKEAWAYQUANTITY") = 0
                                        If dgMainGrid.Rows(dgMainGrid.Row)("BTYPE") = "S" AndAlso quantity <= 0 Then
                                            quantity = 1
                                        End If
                                    End If
                                End If
                            End If
                        End If

                        If dgMainGrid.Rows(dgMainGrid.Row)("BTYPE") = "S" AndAlso quantity <= 0 Then
                            ShowMessage(getValueByKey("CM021"), "CM021 - " & getValueByKey("CLAE04"))
                            'ShowMessage("Please Delete the Item if not Required", "Information")
                            quantity = 1
                        End If

                        Call ChangeComboDirectQty(_iArticleQtyBeforeChange, quantity)
                        _iArticleQtyBeforeChange = 0
                        'Dim index As Int32 = dgMainGrid.Cols("SellingPrice").Index
                        'dgMainGrid_AfterEdit(dgMainGrid, New C1.Win.C1FlexGrid.RowColEventArgs(dgMainGrid.Row, index))

                        'End 
                    End If
                End If
            End If
            CtrlSalesPersons.CtrlTxtBox.Select()
            IsChangeQuantityfromNumPad = False
            IsChangeQuantityOrPrice = False
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
            dgMainGrid.FinishEditing()
            Dim lastRowIndex As Integer = dgMainGrid.Row

            If objArticleCombo.CheckIfComboArticle(dgMainGrid.Rows(lastRowIndex)("ArticleCode")) = False Then

                If UpdateFlag = False Then
                    Dim EventType As Int32 = 0

                    Dim discountAmount As Decimal = Decimal.Zero
                    discountAmount = dsMain.Tables("CASHMEMODTL").Compute("Sum(TotalDiscount)", String.Empty)
                    If clsDefaultConfiguration.IsMembership Then discountAmount = Decimal.Zero
                    If (discountAmount > 0) Then
                        ShowMessage(getValueByKey("CM064"), getValueByKey("CLAE04"), EventType, True, getValueByKey("mod009"))
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
                            frm.AllowDecimal = True
                            frm.ShowDialog()

                            Dim i As Integer
                            If frm.GetResult IsNot Nothing Then

                                If (discountAmount > 0) Then
                                    cmdClrAllPromo_Click("ClearPromWithoutMessage", EventArgs.Empty)
                                End If
                                IsChangeQuantityOrPrice = True

                                _iArticleQtyBeforeChange = dgMainGrid.Rows(lastRowIndex)("Quantity")
                                dgMainGrid.Rows(lastRowIndex)("Quantity") = IIf(frm.GetResult Is Nothing, 1, frm.GetResult)
                                dgMainGrid.Rows(lastRowIndex)("Quantity") = IIf(Int32.TryParse(frm.GetResult, i), frm.GetResult, dgMainGrid.Rows(lastRowIndex)("Quantity"))

                                Dim index As Int32 = dgMainGrid.Cols("SellingPrice").Index
                                dgMainGrid_AfterEdit(dgMainGrid, New C1.Win.C1FlexGrid.RowColEventArgs(lastRowIndex, index))
                                'If clsDefaultConfiguration.DineInProcess Then
                                '    cmdGenerateKOT.Enabled = True
                                'End If
                            End If
                        End If

                        dgMainGrid.Select(lastRowIndex, 2)
                        'CtrlSalesPersons.CtrlTxtBox.Select()
                        'CtrlSalesPersons.CtrlTxtBox.Focus()
                        CtrlSalesPersons.AndroidSearchTextBox.Select()
                        CtrlSalesPersons.AndroidSearchTextBox.Focus()

                    End If
                    IsChangeQuantityOrPrice = False
                End If
            Else
                ShowMessage(getValueByKey("frmfastcashmemo.comboqtychangemsg"), getValueByKey("CLAE04"))
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub CheckIfHoInstance()
        If UCase(ReadSpectrumParamFile("InstanceName")) = "HO" Then
            IsHOInstance = True
        End If
    End Sub

    Private Sub cbManualDisc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cbManualDisc.KeyDown
        If e.KeyCode = Keys.Enter Then
            CtrlSalesPersons.CtrlTxtBox.Select()
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

    Private Sub Me_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MDISpectrum.lblUserId.Visible = True
        MDISpectrum.lblLoggedIn.Visible = True
        MDISpectrum.lblTodayDate.Visible = True
        MDISpectrum.RibbonSeparator1.Visible = True
        MDISpectrum.RibbonSeparator2.Visible = True
        MDISpectrum.RibbonSeparator3.Visible = True
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        'If keyData <> Keys.Escape Then
        comm.keylogged(keyData.ToString(), Me.Name, Convert.ToString(System.DateTime.Now), clsAdmin.UserCode, "NULL", lblCMNo.Text)
        Return (MyBase.ProcessCmdKey(msg, keyData))
        'Else
        '    cmdExit_Click(Nothing, New EventArgs())
        '    Return True
        'End If
    End Function

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)

        If m.Msg = WM_Active Then

            If Not Me.GetChildAtPoint(Me.PointToClient(MousePosition)) Is Nothing Then

                Dim c As Control = Me.GetChildAtPoint(Me.PointToClient(MousePosition))
                Dim name As String = c.Name
                Dim type As String = c.GetType().ToString()

                If type = "C1Sizer" Then

                    Dim c1cont As Control = c.GetChildAtPoint(c.PointToClient(MousePosition))

                    comm.keylogged("NULL", Me.Name, Convert.ToString(System.DateTime.Now), clsAdmin.UserCode, c1cont.Name, lblCMNo.Text)

                Else
                    comm.keylogged("NULL", Me.Name, Convert.ToString(System.DateTime.Now), clsAdmin.UserCode, c.Name, lblCMNo.Text)

                End If


                ' Dim frmname As String = Me.GetChildAtPoint(Me.PointToClient(MousePosition)).Text
            End If

        End If

        MyBase.WndProc(m)
    End Sub

    Private Sub RemoveSelectedArticlePromotion()
        Try
            If (dgMainGrid.Rows.Count > 1) Then

                If MsgBox(getValueByKey("CM064"), MsgBoxStyle.Information, "CM064 -" & getValueByKey("CLAE04")) = MsgBoxResult.Ok Then
                    cmdClrAllPromo_Click("ClearPromWithoutMessage", EventArgs.Empty)
                End If
            End If

        Catch ex As Exception
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

#Region "Combo Qty Change Directly"

    Private Sub ChangeComboDirectQty(ByVal OldComboQuantity As Double, ByVal ComboQuantity As Double)
        Try
            Dim dtCombo As DataTable
            dtCombo = objArticleCombo.GetComboDetails(dgMainGrid.Rows(dgMainGrid.Row)("ARTICLECODE"))
            If Not dtCombo Is Nothing AndAlso dtCombo.Rows.Count > 0 Then
                If Not dgMainGrid.Cols(dgMainGrid.ColSel).Name.ToUpper() = "SELECTS".ToUpper() Then
                    If dgMainGrid.Row >= 1 Then
                        Dim dataRows As DataRow() = dsMain.Tables("Cashmemodtl").Select("TotalDiscount <> 0 And ArticleCode='" & dgMainGrid.Rows(dgMainGrid.Row)("ARTICLECODE") & "'")
                        If dataRows IsNot Nothing AndAlso dataRows.Count > 0 Then
                            If MsgBox(getValueByKey("frmfastcashmemo.combocleardiscount"), MsgBoxStyle.YesNo, getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                                For Each dr As DataRow In dsMain.Tables("Cashmemodtl").Select("TotalDiscount <> 0 And ArticleCode='" & dgMainGrid.Rows(dgMainGrid.Row)("ARTICLECODE") & "'")
                                    dr("TotalDiscount") = 0
                                    dr("LineDiscount") = 0
                                    dr("TOTALDISCPERCENTAGE") = 0
                                    dr("FIRSTLEVEL") = String.Empty
                                    dr("TOPLEVEL") = String.Empty
                                    dr("MANUALPROMO") = 0
                                    Dim taxAmt = CalculateTotalInclusiveTax(0, dgMainGrid.Rows(dgMainGrid.RowSel)("BillLineNo").ToString(), dr("ARTICLECODE"), dr("GrossAmt"), 1, dr("EAN"))
                                    dr("TotalTaxAmount") = taxAmt
                                Next
                            Else
                                Exit Sub
                            End If
                        End If
                        Dim articleTable As New DataTable
                        articleTable.Columns.Add("ARTICLECODE")
                        articleTable.Columns.Add("EAN")
                        articleTable.Columns.Add("DISCRIPTION")
                        articleTable.Columns.Add("SELLINGPRICE")
                        articleTable.Columns.Add("NETSELLINGPRICE")
                        articleTable.Columns.Add("Quantity")
                        articleTable.Columns.Add("IndividualQty")
                        articleTable.Columns.Add("ComboPartNumber")
                        articleTable.Columns.Add("BillLineNo")
                        articleTable.Columns.Add("IsUpgraded")
                        articleTable.Columns.Add("Discount")
                        'Dim ArticleComboPopup As New ArticleComboPopup(True)

                        'ArticleComboPopup.ComboDetails = dtCombo
                        For Each row In comboArticleCopy.Rows
                            If row("BillLineNo") = ((dgMainGrid.Rows.Count - 1) - (dgMainGrid.Row - 1)) Then
                                articleTable.Rows.Add(row("ARTICLECODE"), row("EAN"), row("DISCRIPTION"), row("SELLINGPRICE"), row("NETSELLINGPRICE"), row("Quantity") * ComboQuantity / _iArticleQtyBeforeChange, row("IndividualQty") * ComboQuantity / _iArticleQtyBeforeChange, row("ComboPartNumber"), row("BillLineNo"), row("IsUpgraded"), row("Discount"))
                            End If
                        Next
                        'ArticleComboPopup.SelectedComboArticles = articleTable
                        'ArticleComboPopup.ShowDialog()
                        'Issue ID 7627

                        If Not articleTable Is Nothing AndAlso articleTable.Rows.Count > 0 Then
                            Dim sellingPrice As DataTable = objArticleCombo.GetComboSellingPrice(dgMainGrid.Rows(dgMainGrid.Row)("ARTICLECODE"), clsAdmin.SiteCode)
                            Dim tax As Double = 0
                            Dim totalSellingPrice As Double = 0
                            Dim originalTax As Double

                            tax = CalculateTotalInclusiveTax(originalTax, dgMainGrid.Rows(dgMainGrid.RowSel)("BillLineNo").ToString(), dgMainGrid.Rows(dgMainGrid.Row)("ARTICLECODE"), sellingPrice.Rows(0)("SellingPrice") * ComboQuantity, ComboQuantity, dgMainGrid.Rows(dgMainGrid.Row)("EAN"))
                            totalSellingPrice = sellingPrice.Rows(0)("SellingPrice")

                            dgMainGrid.Rows(dgMainGrid.Row)("TotalTaxAmount") = tax

                            dgMainGrid.Rows(dgMainGrid.Row)("SELLINGPRICE") = totalSellingPrice
                            dgMainGrid.Rows(dgMainGrid.Row)("Quantity") = ComboQuantity

                            ReCalculateCM("")
                            'End Issue ID 7627
                            calculateTotalbill()
                            Dim articleDescriptionDictionary As New Dictionary(Of String, Integer)
                            articleDescriptionDictionary.Add(ObjclsCommon.GetArticleDescription(dgMainGrid.Rows(dgMainGrid.Row)("ARTICLECODE")), 0)
                            GetArticleDecriptionDictionary(articleDescriptionDictionary, articleTable)
                            dgMainGrid.Rows(dgMainGrid.Row)("DISCRIPTION") = GetMultilinedString(articleDescriptionDictionary)
                            'setgridview(dt, True)
                            For i = dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count - 1 To 0 Step -1
                                If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).RowState = DataRowState.Deleted Then
                                    dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                                End If
                            Next

                            For i = dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count - 1 To 0 Step -1
                                If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i)("BillLineNo") = ((dgMainGrid.Rows.Count - 1) - (dgMainGrid.Row - 1)) Then
                                    dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).Delete()
                                    'If Not dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i) Is Nothing Then
                                    '    dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                                    'End If
                                End If
                            Next
                            'Just a Temp Solution, required Proper Analysis of issue
                            For i = dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows.Count - 1 To 0 Step -1
                                If dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).RowState = DataRowState.Deleted Then
                                    dsCashMemoComboDtl.Tables("CashMemoComboDtl").Rows(i).AcceptChanges()
                                End If
                            Next

                            For i = comboArticleCopy.Rows.Count - 1 To 0 Step -1
                                If comboArticleCopy.Rows(i)("BillLineNo") = ((dgMainGrid.Rows.Count - 1) - (dgMainGrid.Row - 1)) Then
                                    comboArticleCopy.Rows(i).Delete()
                                End If
                            Next
                            CheckingComboCode()
                            For Each dataRow In articleTable.Rows
                                AddComboArticles(dataRow, ((dgMainGrid.Rows.Count - 1) - (dgMainGrid.Row - 1)) - 1)
                            Next
                            comboArticleCopy.Merge(articleTable)
                        End If
                    End If
                End If
            End If

            dgMainGrid.Update()
            dgMainGrid.Refresh()

        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
#End Region

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

    Private Sub rbtnRoundOff_Click(sender As System.Object, e As System.EventArgs) Handles rbtnRoundOff.Click
        Try
            Dim p As Object = "ClearPromWithoutMessage"
            cmdClrAllPromo_Click(p, Nothing)
            If CashSummary.CtrllblNetAmt.Text Mod 5 = 0 Then
                Exit Sub
            End If
            Dim FilterCondition As String = "BTYPE='S' AND (isnull(MANUALPROMO,'')='' OR isnull(MANUALPROMO,0)=0) "
            Dim totalGAmount As Double
            Dim percentage, totalDiscValue As Double
            totalDiscValue = CashSummary.CtrllblNetAmt.Text Mod 5
            Dim dtUserAuth As DataTable = dsMain.Tables("CashMemodtl").Copy
            Dim dtCashMemoDtls As DataTable = dsMain.Tables("CashMemodtl")
            totalGAmount = dtUserAuth.Compute("Sum(GROSSAMT)", FilterCondition)
            'totalDiscValue = txtValue.Text.Trim
            'If (totalGAmount - totalDiscValue) < 0 Then
            '    ShowMessage(getValueByKey("UA008"), "UA008 - " & getValueByKey("CLAE04"))
            '    'ShowMessage("Discount Percent Greater than 100 is not Possible", "Information")
            '    Exit Sub
            'End If
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
            CtrlSalesPersons.CtrlTxtBox.Select()

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
#Region "DineInProcess"
    Private Sub cmdNewOrder_Click(sender As System.Object, e As System.EventArgs) Handles cmdNewOrder.Click
        Try
            DineInAutoSave = False
            If currentDineInTable > 0 Then

                'If MsgBox(getValueByKey("CM036"), MsgBoxStyle.YesNo, "CM036 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                IsNewOrder = True
                Call SaveDineInTable(enumDineInProcess.NewHold)
                IsNewOrder = False
                'End If
            End If
            cmdNew_Click(sender, e)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdDinIn_Click(sender As System.Object, e As System.EventArgs) Handles cmdDinIn.Click
        Try
            '----DineBtn Click Autosave In case any item added in loded order
            If DineInAutoSave Then
                If currentDineInTable > 0 Then
                    IsNewOrder = True
                    Call SaveDineInTable(enumDineInProcess.NewHold)
                    IsNewOrder = False
                    DineInAutoSave = False
                    Call SaveDineInTable(enumDineInProcess.EditAndLoad)
                End If
            End If
            If mergeIdForGenerateBill > 0 Then
                ClearData()
                cmdNewOrder_Click(sender, e)
            End If
            '......new Dine-In form opens
            objCM.SalesType = "Dine In"
            objCM.DinInFlag = True
            If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
                If dgMainGrid.Rows.Count > 1 Then
                    RecalculateTax = True
                    CustomerSaleType = 1
                    BillRecalculateTax()
                End If
            End If
            Using objDineIn As New frmDineIn
                If (dgMainGrid.Rows.Count > 1) Then
                    objDineIn.AddToDineInTable = True
                Else
                    objDineIn.AddToDineInTable = False
                End If
                objDineIn.DineInNumber = currentDineInTable
                objDineIn.DinInBillNo = currentDineInBillNo
                objDineIn.ShowDialog()

                If objDineIn.DialogResult = Windows.Forms.DialogResult.OK Then
                    currentDineInTable = objDineIn.DineInNumber
                    currentDineInBillNo = objDineIn.DinInBillNo
                    objCM.CurrenDinInBillNo = objDineIn.DinInBillNo
                    '----FOr Color Coding
                    Dim status = objCM.GetOrderStatus(clsAdmin.SiteCode, clsAdmin.DayOpenDate, currentDineInBillNo)
                    If status = 1 Then
                        BillGenerateStatus = objDineIn.IsGenerateBillColor
                    End If
                    Call SaveDineInTable(objDineIn.DineInProcess)
                    ' lblTable.Text = "Table No." & objDineIn.DineInNumber
                    lblDineInTableNo.Visible = True
                    lblDineInTableNo.Text = objDineIn.DineInNumber
                    cmdGenerateBill.Enabled = True
                    cmd_Remark.Enabled = True
                    cmd_Track.Enabled = True
                    cmd_Reprints.Enabled = True
                    cmd_VoidKot.Enabled = True
                    vBillno = currentDineInBillNo
                    cmdGenerateKOT.Enabled = True
                    If ObjclsCommon.IsGenerateKOTEnable(currentDineInBillNo) Then
                        cmdGenerateKOT.Enabled = True
                        cmd_Reprints.Enabled = False
                        cmd_VoidKot.Enabled = False
                    Else
                        cmdGenerateKOT.Enabled = False
                    End If
                    If ObjclsCommon.IsPresentKot(currentDineInBillNo) Then
                        cmd_VoidKot.Enabled = True
                        cmd_Reprints.Enabled = True
                    End If
                    IsMergeOrderBillNo()
                    If mergeIdForGenerateBill = 0 Then
                        dgMainGrid.Enabled = True
                    End If
                    'Dim mergebill = ObjclsCommon.GetMergeBillNo()
                    'If mergebill.Rows.Count > 0 Then
                    '    Dim drHdr() = mergebill.Select("billno='" & currentDineInBillNo & "'")
                    '    If drHdr.Count > 0 Then
                    '        SetButtons(2, False)
                    '        cmdPayments.Enabled = False
                    '    Else
                    '        SetButtons(2, True)
                    '        cmdPayments.Enabled = True
                    '    End If
                    'End If
                Else
                    If clsDefaultConfiguration.AllowTaxMappingBasedOnOrderType = True Then
                        If dgMainGrid.Rows.Count > 1 Then
                            RecalculateTax = True
                            CustomerSaleType = 0
                            BillRecalculateTax()
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdTakeAway_Click(sender As System.Object, e As System.EventArgs) Handles cmdTakeAway.Click
        Try
            DineInAutoSave = False
            objCM.SalesType = "Take Away"
            lblTable.Visible = False
            lblDineInTableNo.Visible = False
            If currentDineInTable > 0 Then

                'If MsgBox(getValueByKey("CM036"), MsgBoxStyle.YesNo, "CM036 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                IsNewOrder = True
                Call SaveDineInTable(enumDineInProcess.NewHold)
                IsNewOrder = False
                cmdGenerateBill.Enabled = False
                cmdGenerateKOT.Enabled = False
                'End If
                currentDineInTable = 0
            End If

            cmdNewOrder_Click(sender, e)
            ' If MsgBox("Do yuou want to chnage order type?", MsgBoxStyle.YesNo, getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
            CtrlSalesPersons.CtrlTxtBox.Text = String.Empty
            'If CustomerSaleType = enumCustomerSaleType.Dine_In Then
            '    For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Rows
            '        dr("TakeAwayQuantity") = 0
            '    Next
            'End If
            CustomerSaleType = enumCustomerSaleType.Take_Away
            Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
            ' dsMain.Tables("CashMemoHdr").Rows(0)("ServiceTaxAmount") = CustomerSaleType
            IsHoldEnterKey = False
            If IsReset = True Then
                IsReset = False
                Exit Sub
            End If
            lblCustSaleType.Text = "Take Away"
            lblCustSaleType.Visible = True
            'HideColumns(dgMainGrid, False, "TAKEAWAYQUANTITY")
            CustSaleTypeTimer.Start()
            ' End If
            CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.CtrlTxtBox.Focus()


            ' cmdNew_Click(sender, e)
            'End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
        Exit Sub
    End Sub

    Private Sub cmdHomeDelivery_Click_1(sender As System.Object, e As System.EventArgs) Handles cmdHomeDelivery.Click
        Try
            DineInAutoSave = False
            lblTable.Visible = False
            lblDineInTableNo.Visible = False
            objCM.SalesType = "Home Delivery"
            If currentDineInTable > 0 Then

                'If MsgBox(getValueByKey("CM036"), MsgBoxStyle.YesNo, "CM036 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                IsNewOrder = True
                Call SaveDineInTable(enumDineInProcess.NewHold)
                IsNewOrder = False
                cmdGenerateBill.Enabled = False
                cmdGenerateKOT.Enabled = False
                'End If
                currentDineInTable = 0
            End If
            'cmdNew_Click(sender, e)
            'If MsgBox("Do yuou want to chnage order type?", MsgBoxStyle.YesNo, getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
            cmdNew_Click(sender, e)
            If IsReset = True Then
                IsReset = False
                Exit Sub
            End If
            CustSaleTypeTimer.Start()
            ' End If
            CtrlSalesPersons.CtrlTxtBox.Select()
            CtrlSalesPersons.CtrlTxtBox.Focus()
            lblCustSaleType.Visible = True
            cmdHomeDelivery_Click(sender, e)
            ' End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdGenerateBill_Click(sender As System.Object, e As System.EventArgs) Handles cmdGenerateBill.Click
        Try
            DineInAutoSave = False
            BillGenerateStatus = 1
            If currentDineInTable > 0 Then
                IsNewOrder = True
                Call SaveDineInTable(enumDineInProcess.NewHold)
                IsNewOrder = False
            End If

            Dim billNo As String = lblCMNo.Text
            Dim objPrint As New clsCashMemoPrint(billNo, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving) '0000413
            Dim ErrorMsg As String = ""

            'objPrint.CashMemoPrintFormatForDineIn(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "Duplicate", "", "", ErrorMsg, "") '0000413
            objPrint.HoldCMPrintDineIn(clsAdmin.SiteCode, "HoldCM", clsDefaultConfiguration.HoldBillPrintBarcode, ErrorMsg, BarCodeType, vBillno, lblTable.Text, mergeIdForGenerateBill)
            If ErrorMsg <> String.Empty Then
                ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
            End If
            If Not mergeIdForGenerateBill > 0 Then
                Call SaveDineInTable(enumDineInProcess.EditAndLoad)
                cmdGenerateBill.Enabled = True
                cmd_Reprints.Enabled = True
                cmd_VoidKot.Enabled = True
                cmd_Remark.Enabled = True
                cmd_Track.Enabled = True
                If ObjclsCommon.IsGenerateKOTEnable(currentDineInBillNo) Then
                    cmdGenerateKOT.Enabled = True
                    cmd_Reprints.Enabled = False
                    cmd_VoidKot.Enabled = False
                Else
                    cmdGenerateKOT.Enabled = False
                End If
                If ObjclsCommon.IsPresentKot(currentDineInBillNo) Then
                    cmd_VoidKot.Enabled = True
                    cmd_Reprints.Enabled = True
                End If
                IsMergeOrderBillNo()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Public Sub SaveDineInTable(ByVal DineInProcess As Int16, Optional ByVal dtGenerateKot As DataTable = Nothing)
        Try
            Dim dsTemp As DataSet

            If DineInProcess = enumDineInProcess.NewHold Then
                Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
                dsTemp = dsMain.Copy()
                Dim i As Int32 = 0
                For i = dsTemp.Tables.Count - 1 To 0 Step -1
                    If dsTemp.Tables(i).TableName = "CASHMEMOHDR" Or dsTemp.Tables(i).TableName = "CASHMEMODTL" Then
                        dsTemp.Tables(i).TableName = "DineIn" & dsTemp.Tables(i).TableName
                    ElseIf dsTemp.Tables(i).TableName = "ORDERDINEINMAP" Then
                        Continue For
                    Else
                        dsTemp.Tables.RemoveAt(i)
                    End If
                Next
                If dtGV IsNot Nothing Then
                    If dtGV.Rows.Count > 0 Then
                        Dim dtHGV As DataTable = dtGV.Copy()
                        dtHGV.TableName = "DineInVoucher"
                        dsTemp.Tables.Add(dtHGV)
                    End If
                End If
                'If enumDineInProcess.NewHold And IsNewOrder = False Then
                '    dsTemp.Tables("ORDERDINEINMAP").Rows.Add()
                '    dsTemp.Tables("ORDERDINEINMAP").Rows(0)("DineInNumber") = currentDineInTable
                'End If

                Dim str As String = String.Empty
                Dim salesPerson As String = IIf(CtrlSalesPersons.CtrlSalesPersons.SelectedValue IsNot Nothing, CtrlSalesPersons.CtrlSalesPersons.SelectedValue, String.Empty)
                CreatingLineNO(dsTemp, "DineInCashMemoDtl")

                'If objCM.HoldDineInTableData(clsAdmin.DayOpenDate, dsTemp, clsAdmin.SiteCode, clsAdmin.TerminalID, clsAdmin.UserCode, str, currentDineInBillNo, salesPerson, enumCustomerSaleType.Dine_In) Then
                ' ClearData()
                ' GoTo EditHold
                'End If
                If objCM.SaveDinInData(clsAdmin.DayOpenDate, dsTemp, dsMainDineInHold, dtDineInItemRemarks, clsAdmin.SiteCode, clsAdmin.Financialyear, currentDineInTable, clsAdmin.TerminalID, clsAdmin.UserCode, str, currentDineInBillNo, IsNewOrder, salesPerson, enumCustomerSaleType.Dine_In, dtGenerateKot, BillGenerateStatus) Then
                    ClearData()
                    If IsNewOrder = False Then
                        GoTo EditHold
                    End If
                Else
                    ShowMessage("Error", getValueByKey("CLAE05"))
                End If


            ElseIf DineInProcess = enumDineInProcess.EditHold Then

EditHold:       dsMain.Tables(0).Clear()
                Call objCM.GetDineInData(dsMain, currentDineInBillNo, clsAdmin.SiteCode, clsAdmin.UserCode, dtGV, False)
                dsMainDineInHold = dsMain.Copy()
                Call loadDineInTable()
                CtrlSalesPersons.CtrlSalesPersons.SelectedValue = dsMain.Tables("CASHMEMOHDR").Rows(0)("DeliveryPersonID")
                'lblTable.Text = "Table No." & currentDineInTable
                lblDineInTableNo.Visible = True
                lblDineInTableNo.Text = currentDineInTable
                'lblDineInTableNo.Visible = True
                lblTable.Visible = True
                lblCustSaleType.Text = "Dine In"
                lblCustSaleType.Visible = True
                CustSaleTypeTimer.Start()
                'merge case...
            ElseIf DineInProcess = enumDineInProcess.MergeHold Then
                Me.BindingContext(dsMain.Tables("CASHMEMOHDR")).EndCurrentEdit()
                dsTemp = dsMain.Copy()
                Call objCM.GetDineInData(dsMainDineInHold, currentDineInBillNo, clsAdmin.SiteCode, clsAdmin.UserCode, dtGV, False)
                Dim i As Int32 = 0
                For i = dsTemp.Tables.Count - 1 To 0 Step -1
                    If dsTemp.Tables(i).TableName = "CASHMEMOHDR" Or dsTemp.Tables(i).TableName = "CASHMEMODTL" Then
                        dsTemp.Tables(i).TableName = "DineIn" & dsTemp.Tables(i).TableName
                    ElseIf dsTemp.Tables(i).TableName = "ORDERDINEINMAP" Then
                        Continue For
                    Else
                        dsTemp.Tables.RemoveAt(i)
                    End If
                Next
                If dtGV IsNot Nothing Then
                    If dtGV.Rows.Count > 0 Then
                        Dim dtHGV As DataTable = dtGV.Copy()
                        dtHGV.TableName = "DineInVoucher"
                        dsTemp.Tables.Add(dtHGV)
                    End If
                End If
                Dim str As String = String.Empty
                Dim salesPerson As String = IIf(CtrlSalesPersons.CtrlSalesPersons.SelectedValue IsNot Nothing, CtrlSalesPersons.CtrlSalesPersons.SelectedValue, String.Empty)
                CreatingLineNO(dsTemp, "DineInCashMemoDtl")


                Dim finyear As String = clsAdmin.Financialyear
                If objCM.MergeDinInData(clsAdmin.DayOpenDate, dsTemp, dsMainDineInHold, clsAdmin.SiteCode, finyear, currentDineInTable, clsAdmin.TerminalID, clsAdmin.UserCode, str, currentDineInBillNo, salesPerson, enumCustomerSaleType.Dine_In, BillGenerateStatus) Then
                    ClearData()
                    GoTo EditHold
                End If

            ElseIf DineInProcess = enumDineInProcess.SwitchTable Then
                If objCM.SwtichDinInData(clsAdmin.DayOpenDate, dsTemp, dsMainDineInHold, clsAdmin.SiteCode, currentDineInTable, clsAdmin.TerminalID, clsAdmin.UserCode, currentDineInBillNo) Then

                End If

            ElseIf DineInProcess = enumDineInProcess.EditAndLoad Then
                'Dim objDinIn As New frmDineIn
                Dim tempDinInTableNo As Integer
                Dim tempDinBillNo As String
                tempDinInTableNo = currentDineInTable
                tempDinBillNo = currentDineInBillNo
                'currentDineInTable = objDinIn.OldDineInNumber
                'currentDineInBillNo = objDinIn.OldDinInBillNo
                ClearData()
                currentDineInTable = 0
                Call cmdNewOrder_Click(Nothing, Nothing)
                currentDineInTable = tempDinInTableNo
                currentDineInBillNo = tempDinBillNo
                'lblTable.Text = "Table No." & currentDineInTable
                lblDineInTableNo.Text = currentDineInTable
                lblDineInTableNo.Visible = True
                lblTable.Visible = True
                lblCustSaleType.Visible = True
                cmdGenerateBill.Visible = True
                cmdGenerateKOT.Visible = True
                CustSaleTypeTimer.Start()
                Call objCM.GetDineInData(dsMain, currentDineInBillNo, clsAdmin.SiteCode, clsAdmin.UserCode, dtGV, False)
                dsMainDineInHold = dsMain.Copy()
                CtrlSalesPersons.CtrlSalesPersons.SelectedValue = dsMain.Tables("CASHMEMOHDR").Rows(0)("DeliveryPersonID")
                dtDineInItemRemarks = ObjclsCommon.LoadRemarks(clsAdmin.SiteCode, currentDineInBillNo)
            End If
            GridSettings(UpdateFlag)
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub loadDineInTable()
        Try
            dtDineInItemRemarks = ObjclsCommon.LoadRemarks(clsAdmin.SiteCode, currentDineInBillNo)
            For Each dr As DataRow In dsMain.Tables("CASHMEMODTL").Rows
                Dim TaxableAmt As Double = dr("GrossAmt")
                If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                    TaxableAmt = dr("GrossAmt") - dr("TotalDiscount")
                End If
                CreateDataSetForTaxCalculation(0, dr("BillLineNo").ToString(), dr("ArticleCode").ToString(), TaxableAmt, dr, dr("EAN").ToString())
            Next
            CLPCustomerId = IIf(Not dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNO") Is DBNull.Value, dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNO"), String.Empty)

            If dsMain.Tables("CASHMEMOHDR").Rows.Count > 0 Then
                Try
                    CLPCustomerId = IIf(Not dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNO") Is DBNull.Value, dsMain.Tables("CASHMEMOHDR").Rows(0)("CLPNO"), String.Empty)
                    Dim objCustm As New clsCLPCustomer()
                    Dim _dtCustmInfo As DataTable

                    If (Not String.IsNullOrEmpty(CLPCustomerId)) Then
                        _dtCustmInfo = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CLPCustomerId)

                        If (_dtCustmInfo IsNot Nothing AndAlso _dtCustmInfo.Rows.Count > 0) Then
                            CustInfo.CtrlTxtCustomerNo.Text = _dtCustmInfo.Rows(0)("CUSTOMERNO").ToString()
                            CustInfo.CtrltxtCustomerName.Text = _dtCustmInfo.Rows(0)("CustomerName").ToString()
                            CustInfo.ctrlTxtPoints.Text = _dtCustmInfo.Rows(0)("BalancePoint").ToString()
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
                Catch ex As Exception
                    LogException(ex)
                End Try
            End If

            ReCalculateCM(String.Empty)
            calculateTotalbill()

            If (dgMainGrid.Rows.Count > 1) Then
                dgMainGrid.Select(dgMainGrid.Rows.Count - 1, 2)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Sub cmdGenerateKOT_Click(sender As System.Object, e As System.EventArgs) Handles cmdGenerateKOT.Click
        Try
            If currentDineInTable > 0 Then
                DineInAutoSave = False
                objCM.DinInFlagForKOT = True
                Dim oldQty As Decimal
                Dim newQty As Decimal
                Dim MaxKotNo As Integer = 0
                Dim ItemCode, desc, remark As String
                Dim dtNew As DataTable = dsMain.Tables(1).Copy()
                Dim dtOld As DataTable = ObjclsCommon.GetOldKotData(currentDineInBillNo)
                Dim dtFinal As New DataTable
                dtFinal = objCM.GetItemsInfoForGenerateBill()
                dtFinal.Rows.Clear()
                MaxKotNo = ObjclsCommon.GetMaxKotNo(currentDineInBillNo)
                MaxKotNo = MaxKotNo + 1
                Dim dvItemDetail As New DataView(dtNew, "", "", DataViewRowState.CurrentRows)
                Dim BillLineNO As String = String.Empty
                For Each dr As DataRow In dtNew.Rows
                    oldQty = 0
                    ItemCode = dr("ArticleCode").ToString()
                    desc = dr("Discription").ToString()
                    BillLineNO = dr("BillLineNO").ToString()
                    Dim result As DataRow() = dtOld.[Select]("ArticleCode='" + dr("ArticleCode") & "'")
                    If result.Count > 0 Then
                        oldQty = Convert.ToDecimal(result(0)("KotQty"))
                    End If
                    If oldQty > 0 Then
                        newQty = Convert.ToDecimal(dr("Quantity")) - oldQty
                    Else
                        newQty = Convert.ToDecimal(dr("Quantity"))
                    End If
                    newQty = FormatNumber(CDbl(clsCommon.CheckIfBlank(newQty)), 3)
                    '-----For Remark For item
                    ' remark = ObjclsCommon.GetRemarkItemWise(clsAdmin.SiteCode, currentDineInBillNo, ItemCode, dr("EAN").ToString())
                    Dim Drnew() = dtDineInItemRemarks.Select("ArticleCode ='" & ItemCode & "' and EAN='" & dr("EAN").ToString() & "'")
                    If Drnew.Count > 0 Then
                        remark = IIf(IsDBNull(Drnew(0)("Remark")), "", Drnew(0)("Remark"))
                    End If
                    '---adding to datatable
                    If Val(newQty) > 0 Then
                        Dim newRow As DataRow = dtFinal.NewRow()
                        newRow("BillNo") = currentDineInBillNo
                        newRow("EAN") = dr("EAN").ToString()
                        newRow("DISCRIPTION") = desc
                        newRow("ArticleCode") = ItemCode
                        newRow("KotQuantity") = newQty
                        newRow("KotNo") = MaxKotNo
                        newRow("Remark") = remark
                        dtFinal.Rows.Add(newRow)
                    End If
                Next
                IsNewOrder = True
                Call SaveDineInTable(enumDineInProcess.NewHold, dtFinal)
                IsNewOrder = False
                Dim dtLoad = dtFinal.Copy()
                Dim objPrint As New clsCashMemoPrint("", False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving) '0000413
                Dim ErrorMsg As String = ""
                objPrint.DineInKOTPrint("KOT", ErrorMsg, Nothing, dtFinal, vBillno, lblTable.Text, clsAdmin.SiteCode)
                If ErrorMsg <> String.Empty Then
                    ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
                End If
                objCM.DinInFlagForKOT = False
                If dtLoad.Rows.Count > 0 Then
                    Call SaveDineInTable(enumDineInProcess.EditAndLoad)
                    cmdGenerateKOT.Enabled = False
                End If
                cmdGenerateBill.Enabled = True
                cmd_Reprints.Enabled = True
                cmd_VoidKot.Enabled = True
                cmd_Track.Enabled = True
                cmd_Remark.Enabled = True
                IsMergeOrderBillNo()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmd_VoidKot_Click(sender As System.Object, e As System.EventArgs) Handles cmd_VoidKot.Click
        Try
            Using objvoidkot As New FrmVoidKot

                objvoidkot.currenttableNo = currentDineInTable
                objvoidkot.currentBillNo = currentDineInBillNo

                Dim dialogResult = objvoidkot.ShowDialog()
                If (dialogResult = Windows.Forms.DialogResult.Cancel) Then
                    Exit Sub
                ElseIf (dialogResult = Windows.Forms.DialogResult.OK) Then
                    '---- Void KOT 
                End If

            End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmd_Reprints_Click(sender As System.Object, e As System.EventArgs) Handles cmd_Reprints.Click
        Try
            Dim lastindex As Integer = dgMainGrid.Row
            If lastindex = -1 Then Exit Sub
            Using objreprint As New frmDinInReprint
                objreprint.currentReprintTableNo = currentDineInTable
                objreprint.currentReprintBillNo = currentDineInBillNo
                objreprint.ShowDialog()
                If objreprint.DialogResult = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                End If
            End Using
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Function GetDineInItemRemarksStruct() As DataTable
        GetDineInItemRemarksStruct = New DataTable
        GetDineInItemRemarksStruct.Columns.Add("ArticleCode", System.Type.GetType("System.String"))
        GetDineInItemRemarksStruct.Columns.Add("EAN", System.Type.GetType("System.String"))
        GetDineInItemRemarksStruct.Columns.Add("Remark", System.Type.GetType("System.String"))
    End Function


    Private Sub cmd_Remark_Click(sender As System.Object, e As System.EventArgs) Handles cmd_Remark.Click
        Try
            Dim lastRowIndex As Integer = dgMainGrid.Row
            If lastRowIndex = -1 Then Exit Sub
            Dim articlecode = dgMainGrid.Rows(lastRowIndex)("ArticleCode")
            Dim EAN = dgMainGrid.Rows(lastRowIndex)("EAN")

            Using objTrackCust As New frmTrackCustomers
                objTrackCust.currentBillNo = currentDineInBillNo
                objTrackCust.articlecode = articlecode
                objTrackCust.eanno = EAN
                objTrackCust.IsKOTRemark = True
                Dim Dr() = dtDineInItemRemarks.Select("ArticleCode ='" & articlecode & "' and EAN='" & EAN & "'")
                If Dr.Count > 0 Then
                    objTrackCust.ResultRemark = IIf(IsDBNull(Dr(0)("Remark")), "", Dr(0)("Remark"))
                End If
                objTrackCust.ShowDialog()
                If Dr.Count > 0 Then
                    DineInAutoSave = True
                    Dr(0)("Remark") = objTrackCust.ResultRemark
                Else
                    DineInAutoSave = True
                    Dim remarkRow As DataRow = dtDineInItemRemarks.NewRow()
                    remarkRow("ArticleCode") = articlecode
                    remarkRow("EAN") = EAN
                    remarkRow("Remark") = objTrackCust.ResultRemark
                    dtDineInItemRemarks.Rows.Add(remarkRow)
                End If
            End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmd_Track_Click(sender As System.Object, e As System.EventArgs) Handles cmd_Track.Click
        Try
            Using objtrackcust As New frmTrackCustomers

                objtrackcust.currenttableNo = currentDineInTable
                objtrackcust.currentBillNo = currentDineInBillNo

                Dim dialogResult = objtrackcust.ShowDialog()
                If (dialogResult = Windows.Forms.DialogResult.Cancel) Then
                    Exit Sub
                ElseIf (dialogResult = Windows.Forms.DialogResult.OK) Then
                    '----Track Customers
                End If

            End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub



    Private Sub cmd_MergeOrder_Click(sender As System.Object, e As System.EventArgs) Handles cmd_MergeOrder.Click
        Try
            If currentDineInTable > 0 Then
                IsNewOrder = True
                Call SaveDineInTable(enumDineInProcess.NewHold)
                IsNewOrder = False
                currentDineInTable = 0
            End If
            cmdNewOrder_Click(sender, e)
            If IsReset = True Then
                IsReset = False
                Exit Sub
            End If
            ClearData()
            If IsMerge = False Then
                Using objMerge As New frmMergeOrder(VMergeId:=0)
                    If (objMerge.ShowDialog() = Windows.Forms.DialogResult.Yes) Then
                        Call LoadMergeOrders(objMerge.MergeId)
                    End If
                End Using
            End If
            IsMerge = False
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmd_ViewMergedOrder_Click(sender As System.Object, e As System.EventArgs) Handles cmd_ViewMergedOrder.Click
        Try
            If currentDineInTable > 0 Then
                IsNewOrder = True
                Call SaveDineInTable(enumDineInProcess.NewHold)
                IsNewOrder = False
                currentDineInTable = 0
            End If
            cmdNewOrder_Click(sender, e)
            If IsReset = True Then
                IsReset = False
                Exit Sub
            End If
            ClearData()
            If IsMerge = False Then
                Using objVMerge As New frmViewMergeOrders
                    If (objVMerge.ShowDialog() = Windows.Forms.DialogResult.Yes) Then
                        Call LoadMergeOrders(objVMerge.MergeId)
                    End If
                End Using
            End If
            IsMerge = False
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Dim mergeIdForGenerateBill As Int64
    Sub LoadMergeOrders(MergeId As Integer)
        Try
            dsMain.Tables(0).Clear()
            Call objCM.GetMergeLoadData(dsMain, dtgv:=dtGV, MergeId:=MergeId, strSitecode:=clsAdmin.SiteCode, terminalid:=clsAdmin.TerminalID, isBatchManagementReq:=False)
            dsMainDineInHold = dsMain.Copy()
            Call loadDineInTable()
            objCM.CurrenDinInBillNo = ""
            objCM.MergeId = MergeId
            mergeIdForGenerateBill = MergeId
            cmdGenerateBill.Enabled = True
            objCM.SalesType = "Dine In"
            lblCustSaleType.Text = "Merge Order"
            lblCustSaleType.Visible = True
            CustSaleTypeTimer.Start()
            CtrlSalesPersons.CtrlCmdSearch.Enabled = False
            CtrlSalesPersons.CtrlTxtBox.Enabled = False
            CtrlNumberPad1.Enabled = False
            dgMainGrid.AllowEditing = False
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function IsMergeOrderBillNo() As Boolean
        dtMergeOrderBillNo = ObjclsCommon.GetMergeBillNo()
        Dim dr() As DataRow = dtMergeOrderBillNo.Select("BillNo='" & currentDineInBillNo & "'")
        If dr.Length > 0 Then
            SetButtons(2, False)
            cmdPayments.Enabled = False
        End If
    End Function
#End Region

    Private Sub frmFastCashMemo_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        FirstBillTimer.Stop()
        If clsDefaultConfiguration.ExtendScreen Then
            If clsDefaultConfiguration.HariOmExtendScreen = True Then
                ' clsTwoLineDisplay.ClearDisplay20x2Line(clsDefaultConfiguration.SerialPort)
                clsTwoLineDisplay.ClearDisplay20x2Line(clsDefaultConfiguration.SerialPort)
                Dim custStr1 = Strings.Mid("       ", 1, 7) + Strings.Mid(clsDefaultConfiguration.ClientName, 1, 8) + Strings.Space(20 - Strings.Len(Strings.Mid(clsDefaultConfiguration.ClientName, 1, 8)) - Strings.Len(Strings.Mid("       ", 1, 7))) + Strings.Mid("", 1, 3)
                Dim custStr2 = Strings.Mid("", 1, 7) + Strings.Space(20 - Strings.Len(Strings.Mid("", 1, 7)) - Strings.Len(Strings.Mid("", 1, 7))) + Strings.Mid("", 1, 7)
                clsTwoLineDisplay.ClientNameDisplay20x2Line(custStr1, custStr2, clsDefaultConfiguration.SerialPort)
            Else
                objextend.Close()
            End If
        End If
    End Sub

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
#End Region

    Dim btnCashMemo As New Button
    Dim btnOrderType As New Button
      Private Function ThemeChange()
        CtrlSalesPersons.AlignChange = "Fast Cash Memo"
        CashSummary.AlignChangeForCashSummary = "Fast Cash Memo"
        Me.TableLayoutPanel2.BackColor = Color.FromArgb(49, 49, 49)
        Me.lblCMNoText.BackColor = Color.FromArgb(49, 49, 49)
        Me.lblCMNoText.Text = Me.lblCMNoText.Text.ToUpper
        Me.lblCMNo.BackColor = Color.FromArgb(49, 49, 49)
        Me.lblCMNoText.ForeColor = Color.FromArgb(255, 255, 255)
        Me.lblCMNo.ForeColor = Color.FromArgb(255, 255, 255)
        Me.lblCMNoText.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        Me.lblCMNo.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        Me.lblCMNo.Text = Me.lblCMNo.Text.ToUpper

        Me.lblUserIdText.BackColor = Color.FromArgb(49, 49, 49)
        Me.lblUserIdValue.BackColor = Color.FromArgb(49, 49, 49)
        Me.lblUserIdText.ForeColor = Color.FromArgb(255, 255, 255)
        Me.lblUserIdValue.ForeColor = Color.FromArgb(255, 255, 255)
        Me.lblUserIdText.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        Me.lblUserIdValue.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        Me.lblUserIdText.Text = Me.lblUserIdText.Text.ToUpper
        Me.lblUserIdValue.Text = Me.lblUserIdValue.Text.ToUpper

        Me.lblCMDateText.BackColor = Color.FromArgb(49, 49, 49)
        Me.lblCMDateValue.BackColor = Color.FromArgb(49, 49, 49)
        Me.lblCMDateText.ForeColor = Color.FromArgb(255, 255, 255)
        Me.lblCMDateText.Text = Me.lblCMDateText.Text.ToUpper
        Me.lblCMDateValue.ForeColor = Color.FromArgb(255, 255, 255)
        Me.lblCMDateText.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        Me.lblCMDateValue.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        Me.lblCMDateValue.Text = Me.lblCMDateValue.Text.ToUpper

        Me.lblTable.BackColor = Color.FromArgb(49, 49, 49)
        Me.lblTable.Text = Me.lblTable.Text.ToUpper
        Me.lblDineInTableNo.BackColor = Color.FromArgb(49, 49, 49)
        Me.lblDineInTableNo.Text = Me.lblDineInTableNo.Text.ToUpper
        Me.lblTable.ForeColor = Color.FromArgb(255, 255, 255)
        Me.lblTable.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        Me.lblDineInTableNo.ForeColor = Color.FromArgb(255, 255, 255)
        Me.lblDineInTableNo.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        Me.lblDineInTableNo.Text = Me.lblDineInTableNo.Text.ToUpper

        Me.lblCustSaleType.BackColor = Color.FromArgb(49, 49, 49)
        Me.lblCustSaleType.ForeColor = Color.FromArgb(255, 255, 255)
        Me.lblCustSaleType.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblCustSaleType.Text = Me.lblCustSaleType.Text.ToUpper

        MDISpectrum.C1StatusBar1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black
        Me.C1StatusBar1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black

        Me.lblTotalItemPaymentHistory.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblTotalItemPaymentHistory.ForeColor = Color.FromArgb(0, 0, 0)
        Me.lblTotalItemPaymentHistory.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblTotalItemPaymentHistory.Location = New System.Drawing.Point(10, 6)
        '  Me.lblTotalItemPaymentHistory.Text = Me.lblTotalItemPaymentHistory.Text.ToUpper

        Me.lblCalTotalItem.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblCalTotalItem.ForeColor = Color.FromArgb(0, 0, 0)
        Me.lblCalTotalItem.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblCalTotalItem.Location = New System.Drawing.Point(55, 7)
        '  Me.lblCalTotalItem.Text = Me.lblCalTotalItem.Text.ToUpper


        Me.lblTotalItemQtyPaymentHistory.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblTotalItemQtyPaymentHistory.ForeColor = Color.FromArgb(0, 0, 0)
        Me.lblTotalItemQtyPaymentHistory.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblTotalItemQtyPaymentHistory.Location = New System.Drawing.Point(100, 6)
        '  Me.lblTotalItemQtyPaymentHistory.Text = Me.lblTotalItemQtyPaymentHistory.Text.ToUpper

        Me.lblCalTotalItemQty.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblCalTotalItemQty.ForeColor = Color.FromArgb(0, 0, 0)
        Me.lblCalTotalItemQty.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblCalTotalItemQty.Location = New System.Drawing.Point(175, 6)
        ' Me.lblCalTotalItemQty.Text = Me.lblCalTotalItemQty.Text.ToUpper

        Me.lblTotalAmountPaymentHistory.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblTotalAmountPaymentHistory.ForeColor = Color.FromArgb(0, 0, 0)
        Me.lblTotalAmountPaymentHistory.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblTotalAmountPaymentHistory.Location = New System.Drawing.Point(240, 6)
        ' Me.lblTotalAmountPaymentHistory.Text = Me.lblTotalAmountPaymentHistory.Text.ToUpper

        Me.lblCalTotalAmount.BackColor = Color.FromArgb(212, 212, 212)
        Me.lblCalTotalAmount.ForeColor = Color.FromArgb(0, 0, 0)
        Me.lblCalTotalAmount.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblCalTotalAmount.Location = New System.Drawing.Point(343, 6)
        ' Me.lblCalTotalAmount.Text = Me.lblCalTotalAmount.Text.ToUpper

        dgMainGrid.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgMainGrid.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        dgMainGrid.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.Highlight.BackColor = Color.FromArgb(212, 212, 212)

        dgMainGrid.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        dgMainGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgMainGrid.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        dgMainGrid.Rows.MinSize = 37
        Me.CtrlSalesPersons.BackColor = Color.FromArgb(255, 255, 255)

        Me.CtrlLblManualDisc.BackColor = Color.FromArgb(255, 255, 255)
        Me.CtrlLblManualDisc.ForeColor = Color.FromArgb(49, 49, 49)
        Me.CtrlLblManualDisc.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        Me.CtrlLblManualDisc.Text = Me.CtrlLblManualDisc.Text.ToUpper
        Me.GroupBox1.BackColor = Color.FromArgb(212, 212, 212)

        C1Ribbon1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2007Black

        'TableLayoutPanelNew
        Dim TableLayoutPanelNew As New TableLayoutPanel
        TableLayoutPanelNew.ColumnCount = 1
        TableLayoutPanelNew.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        If clsDefaultConfiguration.DineInProcess Then
            TableLayoutPanelNew.Controls.Add(btnOrderType, 0, 1)
        End If
        TableLayoutPanelNew.Controls.Add(btnCashMemo, 0, 0)
        TableLayoutPanelNew.Dock = System.Windows.Forms.DockStyle.Fill
        TableLayoutPanelNew.Location = New System.Drawing.Point(457, 553)
        TableLayoutPanelNew.Name = "TableLayoutPanelNew"
        TableLayoutPanelNew.RowCount = 2
        TableLayoutPanelNew.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        TableLayoutPanelNew.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        TableLayoutPanelNew.Size = New System.Drawing.Size(46, 145)
        TableLayoutPanelNew.TabIndex = 47

        ' Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.87755!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.102041!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.30612!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.71429!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.c1SizerGrid, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlNumberPad1, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(TableLayoutPanelNew, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 2, 2)

        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.Panel2, 2)
        Me.Panel2.Controls.Add(Me.ctrlBack)
        Me.Panel2.Controls.Add(Me.C1Ribbon1)



        Me.TableLayoutPanel1.SetColumnSpan(Me.Panel2, 2)
        Me.Panel2.Controls.Add(Me.ctrlBack)
        Me.Panel2.Controls.Add(Me.C1Ribbon1)


        btnCashMemo.Dock = DockStyle.Fill
        '   btnOrderTypeDine.Text = "ORDER" & vbCrLf & "TYPE"
        btnCashMemo.TextAlign = ContentAlignment.MiddleCenter
        '  btnPaymentDine.Text = "PAYMENT"
        btnCashMemo.Font = New Font("Neo Sans", 6, FontStyle.Bold)
        btnCashMemo.Dock = DockStyle.Fill
        btnCashMemo.Font = New Font("Neo Sans", 6, FontStyle.Bold)
        ' btnOrderTypeDine.Size = New System.Drawing.Size(60, 60)
        btnCashMemo.Image = Global.Spectrum.My.Resources.Btn_CashMemo_Active1
        ' btnOrderTypeDine.BackColor = Color.FromArgb(0, 107, 163)
        btnCashMemo.FlatStyle = FlatStyle.Flat
        btnCashMemo.FlatAppearance.BorderSize = 0
        If clsDefaultConfiguration.DineInProcess Then
            btnOrderType.Dock = DockStyle.Fill
            ' If clsDefaultConfiguration.DineInProcess = True Then
            ' btnPaymentDine.Location = New System.Drawing.Point(1, 83)
            ' btnPaymentDine.Size = New System.Drawing.Size(60, 60)
            btnOrderType.Image = Global.Spectrum.My.Resources.Btn_OrderType_Deactive1
            'btnPaymentDine.BackColor = Color.FromArgb(212, 212, 212)
            btnOrderType.FlatStyle = FlatStyle.Flat
            btnOrderType.FlatAppearance.BorderSize = 0
            RemoveHandler btnOrderType.Click, AddressOf btnOrderType_Click
            AddHandler btnOrderType.Click, AddressOf btnOrderType_Click
        End If
        ' panelnewButtons.Controls.Add(btnPaymentDine)
        RemoveHandler btnCashMemo.Click, AddressOf btnCashMemo_Click
        AddHandler btnCashMemo.Click, AddressOf btnCashMemo_Click
        ' End If
        ' panelnewButtons.Controls.Add(btnOrderTypeDine)
        ' Me.TableLayoutPanel1.Controls.Add(panelnewButtons)

        ' Me.rbnTabCM.ForeColorInner = Color.FromArgb(255, 255, 255)
        Me.rbnTabCM.Font = New Font("Neo Sans", 6, FontStyle.Bold)
        '  Me.rbnTabPayments.Font = New Font("Neo Sans", 6, FontStyle.Bold)
        ' Me.RibbonTab1.ForeColorInner = Color.FromArgb(255, 255, 255)
        Me.RibbonTab1.Font = New Font("Neo Sans", 6, FontStyle.Bold)

        Me.rbnGrpCMPromotion.Font = New Font("Neo Sans", 6, FontStyle.Bold)
        Me.rbnGrpCMPromotion.Image = Global.Spectrum.My.Resources.defaultPromo_Normal
        Me.rbnGrpCMPromotion.ForeColorInner = Color.FromArgb(37, 37, 37)
        Me.rbnGrpPayments.Font = New Font("Neo Sans", 6, FontStyle.Bold)
        Me.rbnGrpPayments.Image = Global.Spectrum.My.Resources.payment_Normal
        Me.rbnGrpPayments.ForeColorInner = Color.FromArgb(37, 37, 37)
        Me.rbnGrpCustSearch.Font = New Font("Neo Sans", 6, FontStyle.Bold)
        Me.rbnGrpCustSearch.ForeColorInner = Color.FromArgb(37, 37, 37)
        Me.extraGroup.Font = New Font("Neo Sans", 6, FontStyle.Bold)
        Me.extraGroup.ForeColorInner = Color.FromArgb(37, 37, 37)
        Me.homeDeliveryGroup.Font = New Font("Neo Sans", 6, FontStyle.Bold)
        Me.homeDeliveryGroup.ForeColorInner = Color.FromArgb(37, 37, 37)
        Me.rbnGrpDineIn.Font = New Font("Neo Sans", 6, FontStyle.Bold)
        Me.rbnGrpDineIn.ForeColorInner = Color.FromArgb(37, 37, 37)
        Me.cmdDefaultPromo.LargeImage = Global.Spectrum.My.Resources.Resources.defaultPromo_Normal
        Me.cmdApplySelectPromo.LargeImage = Global.Spectrum.My.Resources.Resources.SelectPromo_Normal
        Me.cmdClrAllPromo.LargeImage = Global.Spectrum.My.Resources.Resources.ClearPromo_Normal
        Me.cmdPayments.LargeImage = Global.Spectrum.My.Resources.Resources.payment_Normal
        Me.cmdCash.LargeImage = Global.Spectrum.My.Resources.Resources.Cash_Normal
        Me.cmdCard.LargeImage = Global.Spectrum.My.Resources.Resources.Card_Normal
        Me.cmdCreditSale.LargeImage = Global.Spectrum.My.Resources.Resources.credit_Normal
        Me.cmdCheque.LargeImage = Global.Spectrum.My.Resources.PayByCheque
        Me.cmdDelete.LargeImage = Global.Spectrum.My.Resources.Resources.VoidBills_Normal
        Me.cmdCustomerinfo.LargeImage = Global.Spectrum.My.Resources.Resources.CustomerSearch_Normal
        Me.sellGiftVoucher.LargeImage = Global.Spectrum.My.Resources.Resources.GV_Normal
        Me.cmdReprint.LargeImage = Global.Spectrum.My.Resources.Resources.reprint_Normal
        Me.cmdDinIn.LargeImage = Global.Spectrum.My.Resources.DineIn_NormalSmall
        Me.cmdNewOrder.LargeImage = Global.Spectrum.My.Resources.NewOrder_NormalSmall
        Me.cmdTakeAway.LargeImage = Global.Spectrum.My.Resources.TakeAway_NormalSmall
        Me.cmdHomeDelivery.LargeImage = Global.Spectrum.My.Resources.HomeDelivery_Normal
        Me.cmdGenerateBill.LargeImage = Global.Spectrum.My.Resources.GenerateBill_Normal
        Me.cmdGenerateKOT.LargeImage = Global.Spectrum.My.Resources.GenerateKOT_Normal
        Me.cmd_VoidKot.LargeImage = Global.Spectrum.My.Resources.VoidKOT_Normal
        Me.cmd_Reprints.LargeImage = Global.Spectrum.My.Resources.ReprintKOT_Normal
        Me.cmd_Remark.LargeImage = Global.Spectrum.My.Resources.Remark_Normal
        Me.cmd_Track.LargeImage = Global.Spectrum.My.Resources.NoOfCustomer_Normal
        Me.cmd_MergeOrder.LargeImage = Global.Spectrum.My.Resources.MergeOrder_Normal
        Me.cmd_ViewMergedOrder.LargeImage = Global.Spectrum.My.Resources.ViewMergeOrder_Normal
        Me.cmdOldCashMemo.LargeImage = Global.Spectrum.My.Resources.EditCash_Memo
        homeDelivery.LargeImage = Global.Spectrum.My.Resources.HomeDelivery_Normal
        '----ALL Caps
        Me.rbnTabCM.Text = rbnTabCM.Text.ToUpper
        '  Me.rbnTabPayments.Text = rbnTabPayments.Text.ToUpper
        ' Me.RibbonTab1.ForeColorInner = Color.FromArgb(255, 255, 255)
        Me.RibbonTab1.Text = RibbonTab1.Text.ToUpper
        Me.rbnGrpCMPromotion.Text = rbnGrpCMPromotion.Text.ToUpper
        Me.rbnGrpPayments.Text = rbnGrpPayments.Text.ToUpper
        Me.rbnGrpCustSearch.Text = rbnGrpCustSearch.Text.ToUpper
        Me.extraGroup.Text = extraGroup.Text.ToUpper
        Me.homeDeliveryGroup.Text = homeDeliveryGroup.Text.ToUpper
        Me.rbnGrpDineIn.Text = rbnGrpDineIn.Text.ToUpper
        Me.RibbonGroup1.Text = RibbonGroup1.Text.ToUpper
        Me.RibbonGroup2.Text = RibbonGroup2.Text.ToUpper
        Me.RibbonGroup1.Font = New Font("Neo Sans", 6, FontStyle.Bold)
        Me.RibbonGroup2.Font = New Font("Neo Sans", 6, FontStyle.Bold)
        rbnGrpCMPromotion.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbnGrpCMPromotion.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbnGrpPayments.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbnGrpPayments.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbnGrpCustSearch.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbnGrpCustSearch.ForeColorOuter = Color.FromArgb(0, 107, 163)
        extraGroup.ForeColorInner = Color.FromArgb(54, 54, 54)
        extraGroup.ForeColorOuter = Color.FromArgb(0, 107, 163)
        homeDeliveryGroup.ForeColorInner = Color.FromArgb(54, 54, 54)
        homeDeliveryGroup.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbnGrpDineIn.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbnGrpDineIn.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbCMNew.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbCMNew.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbnGrpHoldVoid.ForeColorInner = Color.FromArgb(54, 54, 54)
        rbnGrpHoldVoid.ForeColorOuter = Color.FromArgb(0, 107, 163)
        rbCMNew.Text = rbCMNew.Text.ToUpper
        Me.homeDeliveryGroup.Font = New Font("Neo Sans", 6, FontStyle.Bold)
        homeDelivery.Text = homeDelivery.Text.ToUpper
        cmdDefaultPromo.Text = cmdDefaultPromo.Text & "   "
        Me.cmdDefaultPromo.Text = cmdDefaultPromo.Text.ToUpper
        Me.cmdApplySelectPromo.Text = cmdApplySelectPromo.Text & "    "
        Me.cmdApplySelectPromo.Text = cmdApplySelectPromo.Text.ToUpper
        Me.cmdClrAllPromo.Text = cmdClrAllPromo.Text & ""
        Me.cmdClrAllPromo.Text = cmdClrAllPromo.Text.ToUpper
        Me.cmdPayments.Text = cmdPayments.Text.ToUpper
        Me.cmdCash.Text = cmdCash.Text & ""
        Me.cmdCash.Text = cmdCash.Text.ToUpper
        'code added by vipul for issue id 2783
        Me.cmdCheque.Visible = True
        Me.cmdCheque.Text = "CHEQUE (F7)"
        Me.cmdCard.Text = cmdCard.Text.ToUpper
        Me.cmdCreditSale.Text = cmdCreditSale.Text & "   "
        Me.cmdCreditSale.Text = cmdCreditSale.Text.ToUpper
        Me.cmdDelete.Text = cmdDelete.Text.ToUpper
        Me.cmdCustomerinfo.Text = cmdCustomerinfo.Text.ToUpper
        Me.sellGiftVoucher.Text = sellGiftVoucher.Text.ToUpper
        Me.cmdReprint.Text = cmdReprint.Text & "   "
        Me.cmdReprint.Text = cmdReprint.Text.ToUpper
        Me.cmdDinIn.Text = " " & cmdDinIn.Text & " "
        Me.cmdDinIn.Text = cmdDinIn.Text.ToUpper
        Me.cmdNewOrder.Text = cmdNewOrder.Text.ToUpper
        Me.cmdTakeAway.Text = cmdTakeAway.Text.ToUpper
        Me.cmdHomeDelivery.Text = "   " & cmdHomeDelivery.Text & "   "
        Me.cmdHomeDelivery.Text = cmdHomeDelivery.Text.ToUpper
        Me.cmdGenerateBill.Text = cmdGenerateBill.Text & "  "
        Me.cmdGenerateBill.Text = cmdGenerateBill.Text.ToUpper
        Me.cmdGenerateKOT.Text = cmdGenerateKOT.Text & "   "
        Me.cmdGenerateKOT.Text = cmdGenerateKOT.Text.ToUpper
        Me.cmd_VoidKot.Text = cmd_VoidKot.Text & "      "
        Me.cmd_VoidKot.Text = cmd_VoidKot.Text.ToUpper
        Me.cmd_Reprints.Text = cmd_Reprints.Text.ToUpper
        Me.cmd_Remark.Text = cmd_Remark.Text & "      "
        Me.cmd_Remark.Text = cmd_Remark.Text.ToUpper
        Me.cmd_Track.Text = cmd_Track.Text.ToUpper
        Me.cmd_MergeOrder.Text = cmd_MergeOrder.Text.ToUpper
        Me.cmd_ViewMergedOrder.Text = cmd_ViewMergedOrder.Text.ToUpper
        Me.cmdOldCashMemo.Text = cmdOldCashMemo.Text.ToUpper



        ExitButton.FlatStyle = FlatStyle.Flat
        ExitButton.FlatAppearance.BorderSize = 0
        Dim gp As New Drawing.Drawing2D.GraphicsPath
        Dim rect As New Rectangle
        rect.Location = New Point(3, 3)
        rect.Size = New Size(27, 27)
        rect.Inflate(-2, -2)
        gp.AddEllipse(rect)
        '  gp.AddEllipse(rect(New Point(3, 3), New Size(25, 26)).Inflate(-5, -5))
        ExitButton.Region = New Region(gp)
        Me.ExitButton.Text = ""
        Me.ExitButton.Dock = DockStyle.Right
        Me.ExitButton.Size = New System.Drawing.Size(32, 32)
        Me.ExitButton.TextImageRelation = TextImageRelation.Overlay
        Me.ExitButton.ImageAlign = ContentAlignment.MiddleCenter
        Me.ExitButton.Image = Global.Spectrum.My.Resources.Close_Hover
        dgMainGrid.CellButtonImage = Global.Spectrum.My.Resources.Delete
        '  Dim rect As Rectangle = Me.ClientRectangle
        ' rect.Inflate(-5, -5)
        Dim panel3, panel4, panel5 As New Panel

        panel3.BackColor = Color.FromArgb(212, 212, 212)
        panel3.Location = New System.Drawing.Point(0, 0)
        panel3.Size = New System.Drawing.Size(95, 26)

        panel5.BackColor = Color.FromArgb(212, 212, 212)
        panel5.Location = New System.Drawing.Point(80, 0)
        panel5.Size = New System.Drawing.Size(150, 26)

        panel4.BackColor = Color.FromArgb(212, 212, 212)
        panel4.Location = New System.Drawing.Point(228, 0)
        panel4.Size = New System.Drawing.Size(317, 26)

        GroupBox1.BackColor = Color.White
        Me.GroupBox1.Controls.Add(panel4)
        Me.GroupBox1.Controls.Add(panel3)
        Me.GroupBox1.Controls.Add(panel5)
        CtrlMODMenu1.TableLayoutPanel1.BackColor = Color.FromArgb(212, 212, 212)
        CtrlMODMenu1.TableLayoutPanel1.Controls.Add(ctrlBack)
        ctrlBack.Location = New Point(3, 3)
        ctrlBack.Size = New Size(90, 20)
        ctrlBack.TextAlign = ContentAlignment.MiddleCenter
        ctrlBack.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        ctrlBack.BackColor = Color.Transparent
        ctrlBack.BackColor = Color.FromArgb(0, 107, 163)
        'ctrlBack.ForeColor = Color.FromArgb(255, 255, 255)
        ctrlBack.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        ctrlBack.FlatAppearance.BorderSize = 0
        ctrlBack.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        ctrlBack.FlatStyle = FlatStyle.Flat
        ctrlBack.Text = ctrlBack.Text.ToUpper
        AddHandler rbnTabCM.Select, AddressOf rbnTabCM_Select
        AddHandler RibbonTab1.Select, AddressOf RibbonTab1_Select
        ctrlBack.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        ctrlBack.BackColor = Color.Transparent
        ctrlBack.BackColor = Color.FromArgb(0, 107, 163)
        ctrlBack.ForeColor = Color.FromArgb(255, 255, 255)
        ctrlBack.Font = New Font("Neo Sans", 15, FontStyle.Bold)
        ctrlBack.TextAlign = ContentAlignment.MiddleCenter
        ctrlBack.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        ctrlBack.FlatStyle = FlatStyle.Flat
        ctrlBack.FlatAppearance.BorderSize = 0

    End Function

    Private Sub rbnTabCM_Select(sender As Object, e As EventArgs) Handles rbnTabCM.Select
        btnCashMemo.Image = Global.Spectrum.My.Resources.Btn_CashMemo_Active1
        btnCashMemo.ImageAlign = ContentAlignment.TopCenter
        btnOrderType.Image = Global.Spectrum.My.Resources.Btn_OrderType_Deactive1
    End Sub

    Private Sub RibbonTab1_Select(sender As Object, e As EventArgs)
        btnCashMemo.Image = Global.Spectrum.My.Resources.Btn_CashMemo_Deactive1
        btnCashMemo.ImageAlign = ContentAlignment.TopCenter
        btnOrderType.Image = Global.Spectrum.My.Resources.Btn_OrderType_Active1
    End Sub
    Private Sub btnCashMemo_Click(sender As Object, e As EventArgs)
        btnCashMemo.Image = Global.Spectrum.My.Resources.Btn_CashMemo_Active1
        btnCashMemo.ImageAlign = ContentAlignment.TopCenter
        btnOrderType.Image = Global.Spectrum.My.Resources.Btn_OrderType_Deactive1
        rbnTabCM.Selected = True
    End Sub

    Private Sub btnOrderType_Click(sender As Object, e As EventArgs)
        btnCashMemo.Image = Global.Spectrum.My.Resources.Btn_CashMemo_Deactive1
        btnCashMemo.ImageAlign = ContentAlignment.TopCenter
        btnOrderType.Image = Global.Spectrum.My.Resources.Btn_OrderType_Active1
        RibbonTab1.Selected = True
    End Sub
    Function ExtendFun()
        Try
            If clsDefaultConfiguration.HariOmExtendScreen = False Then
                Dim dgMain As DataTable = dgMainGrid.DataSource.ToTable()
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
                Dim dataView As New DataView(dtExtend)
                dataView.Sort = "SrNo Desc"
                dtExtend = dataView.ToTable()
                objextend.dgMainGrid1.DataSource = dtExtend
                objextend.dgMainGrid1.Cols("SrNo").Visible = False
                objextend.dgMainGrid1.Cols("ArticleCode").Visible = False
                objextend.dgMainGrid1.Cols("ArticleDesc").Caption = "Article Desc"
                objextend.dgMainGrid1.Cols("DiscAmt").Caption = "Disc Amt"
                objextend.dgMainGrid1.Cols("DiscPer").Caption = "Disc Per"
            End If
            objextend.lblTotalItems.Text = lblCalTotalItem.Text
            objextend.lblTotalQty.Text = lblCalTotalItemQty.Text
            objextend.lblTotalAmount.Text = lblCalTotalAmount.Text
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    ''' <summary>
    ''' TWO LINE DISPLAY FOR HARI OM
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function TwoLineExtendScreen(Optional ByVal rowNumber As Integer = 0, Optional ByVal Ispayment As Boolean = False) As Integer
        _tempItemName = ""
        _tempItemQty = "0.00"
        _tempItemAmount = ""
        Try
            Dim dgMain As DataTable = dgMainGrid.DataSource.ToTable()
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
    
    ''' <summary>
    ''' function to specify space between values in 20x2Line extend screen
    ''' </summary>
    ''' <param name="itm"></param>
    ''' <param name="qty"></param>
    ''' <param name="amt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

    Private Sub dgMainGrid_Click(sender As Object, e As System.EventArgs) Handles dgMainGrid.Click
        If dgMainGrid.Rows.Count = 1 Then
            CtrlSalesPersons.AndroidSearchTextBox.Select()
            CtrlSalesPersons.AndroidSearchTextBox.Focus()
        End If
        
    End Sub
    Private Sub ComboBeforeAddedToMainGrid(ByVal mes As String)
        Dim ax As New ApplicationException(mes)
        LogException(ax)
    End Sub
    Private Sub CheckingComboCode()
        CmbCount = 0
        PrevComboCode = ""
        CurrentComboCode = ""
    End Sub

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
End Class