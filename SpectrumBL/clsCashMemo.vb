Imports System.Data
Imports System.Data.SqlClient
Imports System.Text

''' <summary>
''' This Class is Used For Cash Memo
''' </summary>
''' <Createdby>Rama Ranjan Jena</Createdby>
''' <UpdatedBy></UpdatedBy>
''' <usedin>CashMemo</usedin>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
Public Class clsCashMemo
    Inherits clsCommon
#Region "Global Variable For Class "
    Dim daCM As SqlDataAdapter
    Dim dtReturn As DataTable
    Dim ServerDate As DateTime
    Dim Month, day, year, Quarter As Int32
    Dim Months As String
    Dim vStmtQry As New System.Text.StringBuilder
    Dim daScan As New SqlDataAdapter
    Dim tran As SqlTransaction
    Private _strRemarks As String
    Dim DsMettlerKot As DataSet = Nothing
    Public Property strRemarks As String
        Get
            Return _strRemarks
        End Get
        Set(ByVal value As String)
            _strRemarks = value
        End Set
    End Property

    Private _CurrenDinInBillNo As String
    Public Property CurrenDinInBillNo As String
        Get
            Return _CurrenDinInBillNo
        End Get
        Set(ByVal value As String)
            _CurrenDinInBillNo = value
        End Set
    End Property

    Private _CurrentDineInNumber As Int16
    Public Property CurrentDineInNumber As Int16
        Get
            Return _CurrentDineInNumber
        End Get
        Set(ByVal value As Int16)
            _CurrentDineInNumber = value
        End Set
    End Property

    Private _ReservationId As String
    Public Property ReservationId As String
        Get
            Return _ReservationId
        End Get
        Set(ByVal value As String)
            _ReservationId = value
        End Set
    End Property
    Dim dtDineTax As New DataTable   ''' added by nikhil
    Private _DineTax As DataTable
    Public Property DineTax() As DataTable
        Get
            Return _DineTax
        End Get
        Set(ByVal value As DataTable)
            _DineTax = value
        End Set
    End Property
    Private _ReservationGuestId As String
    Public Property ReservationGuestId As String
        Get
            Return _ReservationGuestId
        End Get
        Set(value As String)
            _ReservationGuestId = value
        End Set
    End Property
    Private _DinInFlag As Boolean

    Public Property DinInFlag As Boolean
        Get
            Return _DinInFlag
        End Get
        Set(ByVal value As Boolean)
            _DinInFlag = value
        End Set
    End Property

    Private _DinInFlagForKOT As Boolean

    Public Property DinInFlagForKOT As Boolean
        Get
            Return _DinInFlagForKOT
        End Get
        Set(ByVal value As Boolean)
            _DinInFlagForKOT = value
        End Set
    End Property

    Private _dDueDate As Date
    Public Property dDueDate As Date
        Get
            Return _dDueDate
        End Get
        Set(ByVal value As Date)
            _dDueDate = value
        End Set
    End Property

    Private Shared _dsCashMemoPrinting As DataSet
    Public Shared Property dsCashMemoPrinting As DataSet
        Get
            Return _dsCashMemoPrinting
        End Get
        Set(ByVal value As DataSet)
            _dsCashMemoPrinting = value
        End Set
    End Property

    Private Shared _CashMemoResetonDayClose As Boolean
    Public Shared Property CashMemoResetonDayClose As Boolean
        Get
            Return _CashMemoResetonDayClose
        End Get
        Set(ByVal value As Boolean)
            _CashMemoResetonDayClose = value
        End Set
    End Property

    Private _comboArticleCopy As DataTable
    Public Property comboArticleCopy() As DataTable
        Get
            Return _comboArticleCopy
        End Get
        Set(ByVal value As DataTable)
            _comboArticleCopy = value
        End Set
    End Property
    Public Shared _GiftVoucherReturnAllowed As Boolean = False
    Public Property GiftVoucherReturnAllowed() As Boolean
        Get
            Return _GiftVoucherReturnAllowed
        End Get
        Set(ByVal value As Boolean)
            _GiftVoucherReturnAllowed = value
        End Set
    End Property

    Public _GVBasedAricleReturnList As Dictionary(Of String, String)
    Public Property GVBasedAricleReturnList() As Dictionary(Of String, String)
        Get
            Return _GVBasedAricleReturnList
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            _GVBasedAricleReturnList = value
        End Set
    End Property
    Public ListOfQueue As New Queue(Of Integer)

    Public Shared _IsBillLineNo As Integer
    Public Property IsBillLineNo() As Integer
        Get
            Return _IsBillLineNo
        End Get
        Set(ByVal value As Integer)
            _IsBillLineNo = value
        End Set
    End Property
    Public Shared _SalesType As String
    Public Property SalesType() As String
        Get
            Return _SalesType
        End Get
        Set(ByVal value As String)
            _SalesType = value
        End Set
    End Property
    Dim _ShiftmanagementForCM As Boolean = False
    Public Property ShiftManagementForCM() As Boolean
        Get
            Return _ShiftmanagementForCM
        End Get
        Set(ByVal value As Boolean)
            _ShiftmanagementForCM = value
        End Set
    End Property
    Public Shared _MergeId As Int64
    Public Property MergeId() As Int64
        Get
            Return _MergeId
        End Get
        Set(ByVal value As Int64)
            _MergeId = value
        End Set
    End Property


    'property for credit sales return update value

    Public Shared _SelectedCurrencyIndex As String
    Public Property SelectedCurrencyIndex() As String
        Get
            SelectedCurrencyIndex = _SelectedCurrencyIndex
        End Get
        Set(value As String)
            _SelectedCurrencyIndex = value
        End Set
    End Property


    Public Shared _iCurrencyCode As String
    Public Property CurrencyCode() As String
        Get
            CurrencyCode = _iCurrencyCode
        End Get
        Set(ByVal value As String)
            _iCurrencyCode = value
        End Set

    End Property
    'added on 18 may ashma
    Public _clsInnovitiList As Dictionary(Of String, String)
    Public Property clsInnovitiList() As Dictionary(Of String, String)
        Get
            Return _clsInnovitiList
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            _clsInnovitiList = value
        End Set
    End Property
    'added by khusrao adil on 16-02-2018 for innviti with card functionality point _ natural client
    Public _dtInnoviti As DataTable
    Public Property dtInnoviti() As DataTable
        Get
            Return _dtInnoviti
        End Get
        Set(ByVal value As DataTable)
            _dtInnoviti = value
        End Set
    End Property
    Public _IsnewSalesOrder As Boolean = False
    Public Property IsnewSalesOrder() As Boolean
        Get
            Return _IsnewSalesOrder
        End Get
        Set(ByVal value As Boolean)
            _IsnewSalesOrder = value
        End Set
    End Property
#End Region
#Region "Private Function's & Method's"
    ''' <summary>
    ''' Update audit Table for CashMemo Receipt Changes
    ''' </summary>
    ''' <param name="ds">Dataset</param>
    ''' <param name="BillNo">CashMemoNo</param>
    ''' <param name="TerminalId">TerminalId</param>
    ''' <param name="sitecode">SiteCode</param>
    ''' <param name="con">Connection</param>
    ''' <param name="tran">Transaction</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function updateCashMemoReceipt(ByRef ds As DataSet, ByVal BillNo As String, ByVal TerminalId As String, ByVal sitecode As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction, ByVal UserId As String, ByVal ClpProgramId As String, ByVal CLPCustomerId As String, ByRef CLPRed As Boolean) As Boolean
        Try
            Dim dsTemp As New DataSet
            Dim StrQuery As String
            Dim MaxAuditNo As Double
            StrQuery = "SELECT * FROM CASHMEMORECEIPT WHERE status=1 and SITECODE='" & sitecode & "' AND BILLNO='" & BillNo & "' ;"
            StrQuery = StrQuery & " SELECT ISNULL(MAX(AUDITNO),0) FROM CASHMEMORECEIPTAUDIT  WHERE SITECODE='" & sitecode & "' AND BILLNO='" & BillNo & "' ;"
            'StrQuery = StrQuery & "DELETE FROM CASHMEMORECEIPT WHERE SITECODE='" & sitecode & "' AND BILLNO='" & BillNo & "' ;"
            StrQuery = StrQuery & "UPDATE CASHMEMORECEIPT SET STATUS=0,UPDATEDON=getdate() WHERE SITECODE='" & sitecode & "' AND BILLNO='" & BillNo & "' ;"
            StrQuery = StrQuery & "UPDATE CheckDtls SET STATUS=0,UPDATEDON=getdate() WHERE SITECODE='" & sitecode & "' AND BILLNO='" & BillNo & "' ;"
            Dim daSqlUpdate As New SqlDataAdapter(StrQuery, con)
            daSqlUpdate.SelectCommand.Transaction = tran
            daSqlUpdate.Fill(dsTemp)
            dsTemp.Tables(0).TableName = "CASHMEMORECEIPTAUDIT"
            MaxAuditNo = CDbl(dsTemp.Tables(1).Rows(0)(0).ToString()) + 1
            dsTemp.Tables(0).Columns("CMRecptLineno").ColumnName = "CMReciptLineno"
            AddColumnToDataTable(dsTemp.Tables(0), "AuditNo", "System.Double", MaxAuditNo)
            For Each dr As DataRow In dsTemp.Tables(0).Rows
                dr.AcceptChanges()
                dr.SetAdded()
                If dr("TenderTypeCode").ToString().ToUpper() = "CreditVouc(R)".ToUpper() Or dr("TenderTypeCode").ToString().ToUpper() = "GiftVoucher(R)".ToUpper() Then
                    If ActiveVoucher(SpectrumCon, tran, sitecode, dr("CardNO").ToString(), UserId) = False Then
                        Return False
                    End If
                ElseIf dr("TenderTypeCode").ToString().ToUpper() = "GiftVoucher(I)".ToUpper() Or dr("TenderTypeCode").ToString().ToUpper() = "CreditVouc(I)".ToUpper() Then
                    If DeactivateVoucher(SpectrumCon, tran, sitecode, BillNo, UserId, "CMS", dr("CARDNO").ToString()) = False Then
                        Return False
                    End If
                ElseIf dr("TenderTypeCode").ToString().ToUpper() = "CLPPoints".ToUpper() Then
                    If UpdateClpPoints(False, ClpProgramId, CLPCustomerId, dr("AmountTendered"), SpectrumCon, tran, sitecode, UserId, BillNo, ServerDate, False) = False Then
                        tran.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If
            Next
            ds.Tables.Add(dsTemp.Tables(0).Copy())
            Return True
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function

    Dim dtCreditSaleData As New DataTable
    Public Sub SetBillWiseCreditSaleAmt(ByRef dtCreditSale As DataTable, ByRef dsMain As DataSet)
        Dim BillWiseCreditSaleAmt As Double
        Dim ItemReturnAmount As Double
        Dim BillNetCreditSale As Double
        Try
            Dim dataview As New DataView(dtCreditSale, "BillNo <>''", "", DataViewRowState.CurrentRows)
            Dim dtCreditSaleData As DataTable = dataview.ToTable(True, "BillNo")

            For Each row As DataRow In dtCreditSaleData.Rows
                Dim dr() = dtCreditSale.Select("Billno='" & row("BillNo") & "'")
                If dr.Length > 0 Then
                    ItemReturnAmount = Math.Abs(dsMain.Tables("CashMemoDtl").Compute("sum(NetAmount)", "ReturncmNo='" & row("BillNo") & "'"))
                    For Each RowCr As DataRow In dr
                        BillWiseCreditSaleAmt = 0
                        BillNetCreditSale = RowCr("creditsale") - RowCr("CreditSaleAdjustment")
                        If ItemReturnAmount > BillNetCreditSale Then
                            BillWiseCreditSaleAmt += BillNetCreditSale
                            ItemReturnAmount = ItemReturnAmount - BillNetCreditSale
                        Else
                            BillWiseCreditSaleAmt += ItemReturnAmount
                            ItemReturnAmount = 0
                        End If
                        RowCr("NetCreditSaleAmount") = RowCr("creditsale") - BillWiseCreditSaleAmt
                        RowCr("AdjustedCredit") = BillWiseCreditSaleAmt
                    Next

                End If
            Next
            dtCreditSale.AcceptChanges()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Public Sub SetCloseBillWiseCreditSaleAmt(ByRef dtCreditSale As DataTable, ByRef dsMain As DataSet)
        Dim BillWiseCreditSaleAmt As Double
        Try
            Dim dataview As New DataView(dtCreditSale, "BillNo <>''", "", DataViewRowState.CurrentRows)
            Dim dtCreditSaleData As DataTable = dataview.ToTable(True, "BillNo")

            For Each row As DataRow In dtCreditSaleData.Rows
                Dim dr() = dtCreditSale.Select("Billno='" & row("BillNo") & "'")
                If dr.Length > 0 Then
                    For Each RowCr As DataRow In dr
                        BillWiseCreditSaleAmt = RowCr("creditsale") - RowCr("CreditSaleAdjustment")
                        RowCr("NetCreditSaleAmount") = RowCr("creditsale") - BillWiseCreditSaleAmt
                        RowCr("AdjustedCredit") = BillWiseCreditSaleAmt
                    Next

                End If
            Next
            dtCreditSale.AcceptChanges()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Prepare Data For Save
    ''' </summary>
    ''' <param name="ds">Dataset</param>
    ''' <param name="sitecode">SiteCode</param>
    ''' <param name="terminalid">TerminalID</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrepareData(ByVal DayOpendate As DateTime, ByVal Online As Boolean, ByRef ds As DataSet, ByVal sitecode As String, ByVal terminalid As String, ByVal UserID As String, ByVal Fyear As String, Optional ByVal IsMemberShip As Boolean = False) As Boolean
        Try

            '----- Prepare data for Credit sales Update in case of return 
            Dim dataview As New DataView(ds.Tables("CashMemoDtl"), "", "", DataViewRowState.CurrentRows)
            Dim dtUnique As DataTable = dataview.ToTable(True, "RETURNCMNO")
            Dim ReturnBills As New StringBuilder
            For Each row As DataRow In dtUnique.Rows
                ReturnBills.Append("'" & row("RETURNCMNO") & "',")
            Next
            Dim objclsReturn As New clsCashMemoReturn
            dtCreditSaleData = objclsReturn.getCreditSaleBillData(ReturnBills.ToString().Substring(0, ReturnBills.ToString.Length - 1))
            If dtCreditSaleData IsNot Nothing AndAlso dtCreditSaleData.Rows.Count > 0 Then
                dtCreditSaleData.Columns.Add("NetCreditSaleAmount")
                dtCreditSaleData.Columns.Add("AdjustedCredit")
                SetBillWiseCreditSaleAmt(dtCreditSaleData, ds)
            End If
            Month = DayOpendate.Month
            day = DayOpendate.Day
            year = DayOpendate.Year
            Quarter = Math.Ceiling(Month / 3)
            'Months = Format(DayOpendate, "MMM")
            Months = GetEnglishMonthNames(DayOpendate.Month)
            Months = Left(Months, 3)
            Dim dr As DataRow = ds.Tables("CASHMEMOHDR").Rows(0)
            Dim DocNo As String = getDocumentNo("CM", sitecode)
            If Online = False Then
                'Changed by Rohit to generate Document No. for proper sorting
                Try
                    If (CashMemoResetonDayClose = False) Then
                        DocNo = GenDocNo("OCM" & terminalid & Fyear.Substring(Fyear.Length - 2, 2), 15, DocNo)
                    Else
                        DocNo = GenDocNo("OCM" & terminalid.Substring(terminalid.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, DocNo)
                    End If
                Catch ex As Exception
                    DocNo = "OCM" & terminalid & DocNo
                End Try
                'End Change by Rohit
            Else
                'Changed by Rohit to generate Document No. for proper sorting
                Try
                    '---Changed By Mahesh CM series new format CM '2 digit' terminalId DDMMYY (5 digit bill No)-----
                    If (CashMemoResetonDayClose = False) Then
                        ''''   DocNo = GenDocNo("CM" & terminalid & Fyear.Substring(Fyear.Length - 2, 2), 15, DocNo)
                        '   DocNo = GenDocNo("C" & terminalid.Substring(terminalid.Trim.Length - 2, 2) & Fyear.Substring(Fyear.Length - 2, 2), 15, DocNo)
                        ' GenDocNo("C" & terminalid.Substring(terminalid.Trim.Length - 2, 2) & sitecode.Substring(sitecode.Trim.Length - 3, 3) & strFinyear.Substring(strFinyear.Length - 2, 2), 15, DocNo)
                        DocNo = GenDocNo("C" & terminalid.Substring(terminalid.Trim.Length - 2, 2) & sitecode.Substring(sitecode.Trim.Length - 3, 3) & Fyear.ToString().Substring(Fyear.ToString().Length - 2, 2), 15, DocNo)
                        '  DocNo = GenDocNo("C" & terminalid.Substring(terminalid.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & Fyear.ToString().Substring(Fyear.ToString().Length - 2, 2), 15, DocNo)
                    Else
                        '---Changed By Sagar CM series new format CM '2 digit' terminalId DDMMYY (5 digit bill No)-----year from dayopendate instead of finyear
                        DocNo = GenDocNo("C" & terminalid.Substring(terminalid.Trim.Length - 2, 2) & sitecode.Substring(sitecode.Trim.Length - 2, 2) & day.ToString().PadLeft(2, "0") & Month.ToString().PadLeft(2, "0") & year.ToString().Substring(year.ToString().Length - 2, 2), 15, DocNo)
                        '   DocNo = GenDocNo("C" & terminalid.Substring(terminalid.Trim.Length - 2, 2) & sitecode.Substring(sitecode.Trim.Length - 3, 3) & Fyear.ToString().Substring(Fyear.ToString().Length - 2, 2), 15, DocNo)
                    End If
                Catch ex As Exception
                    DocNo = "CM" & terminalid & DocNo
                End Try
                'End Change by Rohit
            End If

            dr("Billno") = DocNo
            dr("BillDate") = DayOpendate.Date
            dr("BillTime") = ServerDate
            'dr("TAXAmount") = IIf(dr("TAXAmount").ToString() = "", 0, dr("TAXAmount")) + ds.Tables("CASHMEMODTL").Compute("SUM(TOTALTAXAMOUNT)", "")
            dr("PaymentAmt") = ds.Tables("CASHMEMORECEIPT").Compute("Sum(AmountTendered)", "TenderTypeCode='Cash'")
            'dr("DISCOUNTAMT") = ds.Tables("CASHMEMODTL").Compute("Sum(LINEDISCOUNT)", "")
            If IsMemberShip Then
                dr("DISCOUNTAMT") = ds.Tables("CASHMEMODTL").Compute("Sum(TotalDiscount)", "")
            Else
                dr("DISCOUNTAMT") = ds.Tables("CASHMEMODTL").Compute("Sum(LINEDISCOUNT)", "")
            End If
            ' If dr("DISCOUNTAMT").ToString <> "" AndAlso Math.Abs(dr("DISCOUNTAMT")) > 0 Then ''Changed by ketan DiscountPercentage not update in return case
            If dr("DISCOUNTAMT").ToString <> "" AndAlso Math.Abs(dr("DISCOUNTAMT")) > 0 Then
                dr("DiscountPercentage") = (dr("DISCOUNTAMT") * 100) / dr("GROSSAMT")
            End If
            dr("Quarter") = "Q" & Quarter
            dr("Month") = Months.ToUpper()
            dr("DAY") = day
            dr("CREATEDON") = ServerDate
            dr("UPDATEDON") = ServerDate
            dr("UPDATEDAT") = sitecode
            dr("UPDATEDBY") = UserID
            dr("BILLINTERMEDIATESTATUS") = "isclosed"
            dr("Status") = 1
            GetReturnData(ds.Tables("CASHMEMODTL"))
            Dim disc As Double
            disc = IIf(ds.Tables("CASHMEMODTL").Compute("Sum(TotalDiscount)", "Btype='S'") Is DBNull.Value, 0, ds.Tables("CASHMEMODTL").Compute("Sum(TotalDiscount)", "Btype='S'"))
            If disc > 0 Then
                Dim dtDtl As DataTable = ds.Tables("CASHMEMODTL").Copy()
                For Each row As DataRow In dtDtl.Select("PROMOTIONID='NO'", "", DataViewRowState.CurrentRows)
                    row("PROMOTIONID") = ""
                    row("MANUALPROMO") = ""
                    row("FIRSTLEVEL") = ""
                    row("TOPLEVEL") = ""
                Next
                Dim dtDisc As DataTable = CreateDiscSummary(DayOpendate, dtDtl, "", "CMS", sitecode, Fyear, DocNo, UserID, ServerDate, "FIRSTLEVEL", "TOPLEVEL", "MANUALPROMO")
                If Not dtDisc Is Nothing AndAlso dtDisc.Rows.Count > 0 Then
                    ds.Tables.Add(dtDisc)
                    ds.AcceptChanges()
                End If
            End If
            PromotionColUpdate(ds.Tables("CASHMEMODTL"))
            RemoveHolddata(sitecode, DocNo)
            SetDetailData(ds, sitecode, "CASHMEMODTL", DocNo, UserID)
            If ds.Tables.Contains("CASHMEMOTAXDTLS") = True Then
                DeleteColumnFromDataTable(ds.Tables("CASHMEMOTAXDTLS"), "ArticleCode")
                DeleteColumnFromDataTable(ds.Tables("CASHMEMOTAXDTLS"), "TAXTYPE")
                DeleteColumnFromDataTable(ds.Tables("CASHMEMOTAXDTLS"), "Inclusive")
                DeleteColumnFromDataTable(ds.Tables("CASHMEMOTAXDTLS"), "FromStepNO")
                DeleteColumnFromDataTable(ds.Tables("CASHMEMOTAXDTLS"), "ToStepNo")
                DeleteColumnFromDataTable(ds.Tables("CASHMEMOTAXDTLS"), "ISPERCENTAGEVALUE")
                DeleteColumnFromDataTable(ds.Tables("CASHMEMOTAXDTLS"), "Appliedon")
                DeleteColumnFromDataTable(ds.Tables("CASHMEMOTAXDTLS"), "Value")
                DeleteColumnFromDataTable(ds.Tables("CASHMEMOTAXDTLS"), "EAN")
                DeleteColumnFromDataTable(ds.Tables("CASHMEMOTAXDTLS"), "Taxable_amount")
                DeleteColumnFromDataTable(ds.Tables("CASHMEMOTAXDTLS"), "ItemQty")
                ds.Tables("CASHMEMOTAXDTLS").Columns("StepNo").ColumnName = "TaxLineNo"
                ds.Tables("CASHMEMOTAXDTLS").Columns("TaxCode").ColumnName = "TaxLabel"
                ds.Tables("CASHMEMOTAXDTLS").Columns("TaxAmount").ColumnName = "TaxValue"
                'DeleteColumnFromDataTable(ds.Tables("CASHMEMOTAXDTLS"), "CREATEDBY")
                'DeleteColumnFromDataTable(ds.Tables("CASHMEMOTAXDTLS"), "CREATEDAT")
                'DeleteColumnFromDataTable(ds.Tables("CASHMEMOTAXDTLS"), "CREATEDON")
                'DeleteColumnFromDataTable(ds.Tables("CASHMEMOTAXDTLS"), "UPDATEDAT")
                'DeleteColumnFromDataTable(ds.Tables("CASHMEMOTAXDTLS"), "UPDATEDBY")
                'DeleteColumnFromDataTable(ds.Tables("CASHMEMOTAXDTLS"), "UPDATEDON")
                'DeleteColumnFromDataTable(ds.Tables("CASHMEMOTAXDTLS"), "STATUS")

                AddColumnToDataTable(ds.Tables("CASHMEMOTAXDTLS"), "CREATEDAT", "System.String", sitecode)
                AddColumnToDataTable(ds.Tables("CASHMEMOTAXDTLS"), "CREATEDBY", "System.String", UserID)
                AddColumnToDataTable(ds.Tables("CASHMEMOTAXDTLS"), "CREATEDON", "System.DateTime", ServerDate)
                AddColumnToDataTable(ds.Tables("CASHMEMOTAXDTLS"), "STATUS", "System.Boolean", 1)
                AddColumnToDataTable(ds.Tables("CASHMEMOTAXDTLS"), "UPDATEDAT", "System.String", sitecode)
                AddColumnToDataTable(ds.Tables("CASHMEMOTAXDTLS"), "UPDATEDBY", "System.String", UserID)
                AddColumnToDataTable(ds.Tables("CASHMEMOTAXDTLS"), "UPDATEDON", "System.DateTime", ServerDate)
                AddColumnToDataTable(ds.Tables("CASHMEMOTAXDTLS"), "SITECODE", "System.String", sitecode)
                AddColumnToDataTable(ds.Tables("CASHMEMOTAXDTLS"), "BILLNO", "System.String", DocNo)
            End If
            If ds.Tables("CASHMEMOHDR").Columns.Contains("RETRIEVEDFROMCUSTNAME") Then
                DeleteColumnFromDataTable(ds.Tables("CASHMEMOHDR"), "RETRIEVEDFROMCUSTNAME")
            End If
            DeleteColumnFromDataTable(ds.Tables("CASHMEMOHDR"), "FinYear")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMODTL"), "BILLDATE")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMODTL"), "BILLTIME")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMODTL"), "FinYear")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMODTL"), "Quarter")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMODTL"), "Month")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMODTL"), "Day")
            If ds.Tables("CASHMEMODTL").Columns.Contains("MRP") Then
                DeleteColumnFromDataTable(ds.Tables("CASHMEMODTL"), "MRP")
            End If

            AddColumnToDataTable(ds.Tables("CASHMEMODTL"), "Quarter", "System.String", Quarter)
            AddColumnToDataTable(ds.Tables("CASHMEMODTL"), "Month", "System.String", Months.ToUpper())
            AddColumnToDataTable(ds.Tables("CASHMEMODTL"), "Day", "System.Int32", day)

            AddColumnToDataTable(ds.Tables("CASHMEMOHDR"), "FINYEAR", "System.String", Fyear)
            AddColumnToDataTable(ds.Tables("CASHMEMODTL"), "FINYEAR", "System.String", Fyear)
            AddColumnToDataTable(ds.Tables("CASHMEMODTL"), "BILLDATE", "System.DateTime", DayOpendate)
            AddColumnToDataTable(ds.Tables("CASHMEMODTL"), "BILLTIME", "System.DateTime", ServerDate)
            DeleteColumnFromDataTable(ds.Tables("CASHMEMORECEIPT"), "SITECODE")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMORECEIPT"), "BILLNO")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMORECEIPT"), "TERMINALID")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMORECEIPT"), "CMRCPTDATE")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMORECEIPT"), "CMRCPTTIME")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMORECEIPT"), "RecieptType")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMORECEIPT"), "CREATEDBY")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMORECEIPT"), "CREATEDAT")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMORECEIPT"), "CREATEDON")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMORECEIPT"), "UPDATEDAT")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMORECEIPT"), "UPDATEDBY")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMORECEIPT"), "UPDATEDON")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMORECEIPT"), "STATUS")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMORECEIPT"), "FinYear")
            DeleteColumnFromDataTable(ds.Tables("CASHMEMORECEIPT"), "AMOUNTRECEIVED")

            AddColumnToDataTable(ds.Tables("CASHMEMORECEIPT"), "FINYEAR", "System.String", Fyear)
            AddColumnToDataTable(ds.Tables("CASHMEMORECEIPT"), "CREATEDAT", "System.String", sitecode)
            AddColumnToDataTable(ds.Tables("CASHMEMORECEIPT"), "CREATEDBY", "System.String", UserID)
            AddColumnToDataTable(ds.Tables("CASHMEMORECEIPT"), "CREATEDON", "System.DateTime", ServerDate)
            AddColumnToDataTable(ds.Tables("CASHMEMORECEIPT"), "STATUS", "System.Boolean", 1)
            AddColumnToDataTable(ds.Tables("CASHMEMORECEIPT"), "UPDATEDAT", "System.String", sitecode)
            AddColumnToDataTable(ds.Tables("CASHMEMORECEIPT"), "UPDATEDBY", "System.String", UserID)
            AddColumnToDataTable(ds.Tables("CASHMEMORECEIPT"), "UPDATEDON", "System.DateTime", ServerDate)
            AddColumnToDataTable(ds.Tables("CASHMEMORECEIPT"), "SITECODE", "System.String", sitecode)
            AddColumnToDataTable(ds.Tables("CASHMEMORECEIPT"), "BILLNO", "System.String", DocNo)
            AddColumnToDataTable(ds.Tables("CASHMEMORECEIPT"), "TERMINALID", "System.String", terminalid)
            AddColumnToDataTable(ds.Tables("CASHMEMORECEIPT"), "CMRCPTDATE", "System.DateTime", DayOpendate)
            AddColumnToDataTable(ds.Tables("CASHMEMORECEIPT"), "CMRCPTTIME", "System.DateTime", ServerDate)

            'Added by Rohit for CR-5938
            PrepareCreditCheckData(ds, sitecode, UserID, Fyear, "CMS", DocNo, DocNo, ServerDate, dDueDate, strRemarks, "CashMemo", DayOpendate)

            If Not ds.Tables("CashMemoDtlItemRemark") Is Nothing AndAlso ds.Tables("CashMemoDtlItemRemark").Rows.Count > 0 Then
                Dim dtRemark As New DataView(ds.Tables("CashMemoDtlItemRemark"), "", "", DataViewRowState.CurrentRows)
                Dim dtUniqueRemark As DataTable = dtRemark.ToTable(True, "ArticleCode", "Ean", "BillLINENO")
                For Each articles In dtUniqueRemark.Rows
                    Dim drCashmemoDtls() = ds.Tables("CashMemoDtl").Select("ArticleCode='" & articles("ArticleCode") & "' AND Ean='" & articles("Ean") & "'AND BillLINENO='" & articles("BillLINENO") & "'")
                    If drCashmemoDtls.Length > 0 Then
                        Dim drRemarkItemDtls() = ds.Tables("CashMemoDtlItemRemark").Select("ArticleCode='" & articles("ArticleCode") & "' AND Ean='" & articles("Ean") & "'AND BillLINENO='" & articles("BillLINENO") & "'")
                        If drRemarkItemDtls.Length > 0 Then
                            drRemarkItemDtls(0)("BillNO") = drCashmemoDtls(0)("BillNO")
                            drRemarkItemDtls(0)("BillLINENO") = drCashmemoDtls(0)("BillLINENO")
                            drRemarkItemDtls(0)("FinYear") = drCashmemoDtls(0)("FinYear")
                            drRemarkItemDtls(0)("CreatedAt") = drCashmemoDtls(0)("CreatedAt")
                            drRemarkItemDtls(0)("CreatedBy") = drCashmemoDtls(0)("CreatedBy")
                            drRemarkItemDtls(0)("CreatedOn") = drCashmemoDtls(0)("CreatedOn")
                            drRemarkItemDtls(0)("UpdatedAt") = drCashmemoDtls(0)("UpdatedAt")
                            drRemarkItemDtls(0)("UpdatedBy") = drCashmemoDtls(0)("UpdatedBy")
                            drRemarkItemDtls(0)("UpdatedOn") = drCashmemoDtls(0)("UpdatedOn")
                            drRemarkItemDtls(0)("Status") = drCashmemoDtls(0)("Status")
                        End If

                    End If

                Next
                ds.Tables("CashMemoDtlItemRemark").AcceptChanges()
            End If

            AddMode(ds)
            Return True
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function GetArticleTypeCode(ByVal articleCode As String) As String
        Dim articleTypeCode As String
        Dim cmd1 As New SqlCommand
        cmd1.CommandType = CommandType.Text
        cmd1.CommandText = "select ArticalTypeCode from mstarticle where articlecode = @Articlecode"
        'cmd1.CommandText = "SELECT CustomerID, CompanyName FROM Customers WHERE CompanyName LIKE @companyName"
        OpenConnection()
        cmd1.Connection = SpectrumCon()

        ' Create a SqlParameter for each parameter in the stored procedure.      
        Dim strArticleCode As New SqlParameter("@Articlecode", SqlDbType.VarChar)
        cmd1.Parameters.Add(strArticleCode).Value = articleCode
        articleTypeCode = cmd1.ExecuteScalar()
        CloseConnection()
        Return articleTypeCode
    End Function

    ''' <summary>
    ''' Preparing Updated CashMemo Data
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function PrepareUpdateData(ByVal dayOpendate As DateTime, ByRef ds As DataSet, ByVal fYear As String, ByVal UserId As String, ByVal SiteCode As String, ByVal ServerDate As DateTime) As Boolean
        Try
            If ds.Tables("CASHMEMOHDR").Columns.Contains("RETRIEVEDFROMCUSTNAME") = True Then
                DeleteColumnFromDataTable(ds.Tables("CASHMEMOHDR"), "RETRIEVEDFROMCUSTNAME")
            End If
            If ds.Tables("CASHMEMORECEIPT").Columns.Contains("RECIEPTTYPE") = True Then
                DeleteColumnFromDataTable(ds.Tables("CASHMEMORECEIPT"), "RECIEPTTYPE")
            End If
            If ds.Tables("CASHMEMORECEIPT").Columns.Contains("AMOUNTRECEIVED") = True Then
                DeleteColumnFromDataTable(ds.Tables("CASHMEMORECEIPT"), "AMOUNTRECEIVED")
            End If
            ds.Tables.Remove("CASHMEMODTL")
            If ds.Tables.Contains("CASHMEMOTAXDTLS") = True Then
                ds.Tables.Remove("CASHMEMOTAXDTLS")
            End If
            '("", "", DataViewRowState.CurrentRows)(0)("CMRecptLineno"))
            Dim CMRecptLineno As Integer = Val(ds.Tables("CASHMEMORECEIPT").Compute("Max(CMRecptLineno)", ""))
            For Each row As DataRow In ds.Tables("CASHMEMORECEIPT").Rows
                If row.RowState <> DataRowState.Deleted Then
                    CMRecptLineno += 1
                    row("FINYEAR") = fYear
                    row("Createdby") = UserId
                    row("Createdon") = ServerDate
                    row("CreatedAt") = SiteCode
                    row("updatedby") = UserId
                    row("updatedOn") = ServerDate
                    row("updatedAt") = SiteCode
                    row("Status") = 1
                    row("CMRCPTDATE") = dayOpendate
                    row("CMRCPTTIME") = ServerDate
                    row("CMRecptLineno") = CMRecptLineno
                    row.AcceptChanges()
                    row.SetAdded()
                End If
            Next
            Dim i As Integer
            For i = (ds.Tables("CASHMEMORECEIPT").Rows.Count - 1) To 0 Step -1
                If ds.Tables("CASHMEMORECEIPT").Rows(i).RowState = DataRowState.Deleted Then
                    ds.Tables("CASHMEMORECEIPT").Rows(i).AcceptChanges()
                End If
            Next

            ''Added by Rohit for CR-5938


            DeleteCheckDtls(ds, "CMS", ds.Tables("CheckDtls"), ds.Tables("CASHMEMORECEIPT").Rows(0)("BillNo"), ds.Tables("CASHMEMORECEIPT").Rows(0)("SiteCode"))


            'If ds.Tables.Contains("CheckDtls") Then
            '    ds.Tables.Remove("CheckDtls")
            'End If

            'Dim dtCheckDtls As New DataTable
            'If ds.Tables("CASHMEMORECEIPT").Rows.Count > 0 Then
            '    dtCheckDtls = GetDtCheckDtls(ds.Tables("CASHMEMORECEIPT").Rows(0)("BillNo"), ds.Tables("CASHMEMORECEIPT").Rows(0)("SiteCode"))
            'End If

            'If dtCheckDtls.Rows.Count > 0 Then
            '    For Each drRow As DataRow In dtCheckDtls.Rows
            '        drRow.Delete()
            '    Next
            '    dtCheckDtls.TableName = "CheckDtls"
            '    ds.Tables.Add(dtCheckDtls)
            'End If

            PrepareCreditCheckData(ds, SiteCode, UserId, fYear, "CMS", ds.Tables("CASHMEMORECEIPT")(0)("BillNo").ToString, ds.Tables("CASHMEMORECEIPT")(0)("BillNo").ToString, ServerDate, dDueDate, strRemarks, "CashMemo", dayOpendate, True)
            If ds.Tables.Contains("CheckDtls") Then
                AddMode(ds.Tables("CheckDtls"))
            End If


            Return True
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    ''' <summary>
    ''' Update Promotion applied on lineItem
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub PromotionColUpdate(ByRef dt As DataTable)
        Try
            For Each row As DataRow In dt.Select("ISNULL(FIRSTLEVEL,'')<>'' OR ISNULL(TOPLEVEL,'')<>'' ", "EAN", DataViewRowState.CurrentRows)
                row("PROMOTIONID") = row("FIRSTLEVEL")
                If row("TopLEVEL").ToString() <> "" Then
                    row("PROMOTIONID") = row("PROMOTIONID") & "," & row("TOPLEVEL")
                End If
            Next
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Select the Return Data From CashMemo to a table 
    ''' </summary>
    ''' <param name="dt">DataTable</param>
    ''' <remarks></remarks>
    Private Sub GetReturnData(ByRef dt As DataTable)
        Try
            Dim dVReturn As New DataView(dt, "BTYPE='R'", "BillLineNo", DataViewRowState.CurrentRows)
            If dVReturn.Count > 0 Then
                dtReturn = dVReturn.ToTable()
                dVReturn.AllowEdit = True
                For Each dr As DataRowView In dVReturn
                    dr("FIRSTLEVEL") = Nothing
                    dr("TopLEVEL") = Nothing
                Next
            End If
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Update CashMemo Return details
    ''' </summary>
    ''' <param name="OldBillNo">CashMemo Number</param>
    ''' <param name="SiteCode">Sitecode</param>
    ''' <param name="Ean">Ean Code</param>
    ''' <param name="Qty">Return Qty</param>
    ''' <param name="RtReason">Return Reason</param>
    ''' <param name="NewBillNo">CashMemo No</param>
    ''' <param name="RtCMDate">Return Date</param>
    ''' <param name="Con">Connection</param>
    ''' <param name="Tran">Transaction</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Private Function UpdateOldCashMemo(ByVal Userid As String, ByVal OldBillNo As String, ByVal SiteCode As String, ByVal Ean As String, ByVal Qty As Int32, _
                         ByVal RtReason As String, ByVal NewBillNo As String, ByVal RtCMDate As DateTime, _
                         ByRef Con As SqlConnection, ByRef Tran As SqlTransaction, ByVal BillLineNo As Integer) As Boolean
        Try
            Qty = Qty * -1
            Dim qry As String = "UPDATE CASHMEMODTL SET SALESRETURNREASON='" & RtReason & "',RETURNCMNO='" & NewBillNo & "'," & _
                               "RETURNCMDATE= GETDATE(),RETURNQTY=ISNULL(RETURNQTY,0) + " & Qty & "  WHERE BILLNO='" & OldBillNo & "' AND SITECODE='" & SiteCode & "'" & _
                               " AND EAN='" & Ean & "'"

            If Ean = "GVBaseArticle" Then
                qry = qry + " And BillLineNo =" & BillLineNo
            End If
            Dim cmdTrn As New SqlCommand(qry, Con, Tran)
            If CInt(cmdTrn.ExecuteNonQuery()) > 0 Then
                UpdateOldCashMemo = True
            End If
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    ''' <summary>
    ''' Update Sales Order Return's
    ''' </summary>
    ''' <param name="SONumber"></param>
    ''' <param name="sitecode"></param>
    ''' <param name="EAN"></param>
    ''' <param name="Qty"></param>
    ''' <param name="RtReason"></param>
    ''' <param name="CashMemoNO"></param>
    ''' <param name="RtCMDate"></param>
    ''' <param name="Con"></param>
    ''' <param name="Tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateSO(ByVal Userid As String, ByVal SONumber As String, ByVal sitecode As String, ByVal EAN As String, ByVal Qty As Int32, _
                              ByVal RtReason As String, ByVal CashMemoNO As String, ByVal RtCMDate As DateTime, _
                              ByRef Con As SqlConnection, ByRef Tran As SqlTransaction) As Boolean
        Try

            Qty = Qty * -1
            Dim cmdTrn As New SqlCommand("UPDATE SALESORDERDTL SET RETURNSALEORDERNO='" & CashMemoNO & "',RETURNSALEORDERDT=Convert(datetime,'" & RtCMDate.ToString("yyyyMMdd") & "')," & _
                " RETURNQTY=ISNULL(RETURNQTY,0) + " & Qty & ",DELIVEREDQTY=ISNULL(DELIVEREDQTY,0) - " & Qty & ",SALESRETURNREASONCODE='" & RtReason & "',UPDATEDON=GETDATE(),UPDATEDBY='" & Userid & "',UPDATEDAT='" & sitecode & "'" & _
                "WHERE SITECODE='" & sitecode & "' AND SaleOrderNumber='" & SONumber & "' AND EAN='" & EAN & "'", Con, Tran)
            If CInt(cmdTrn.ExecuteNonQuery()) > 0 Then
                UpdateSO = True
            End If
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    ''' <summary>
    ''' Update Birth List Return's
    ''' </summary>
    ''' <param name="BLNumber"></param>
    ''' <param name="sitecode"></param>
    ''' <param name="EAN"></param>
    ''' <param name="Qty"></param>
    ''' <param name="RtReason"></param>
    ''' <param name="CashMemoNO"></param>
    ''' <param name="RtCMDate"></param>
    ''' <param name="Con"></param>
    ''' <param name="Tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateBL(ByVal Userid As String, ByVal BLNumber As String, ByVal sitecode As String, ByVal EAN As String, ByVal Qty As Int32, _
                             ByVal RtReason As String, ByVal CashMemoNO As String, ByVal RtCMDate As DateTime, _
                             ByRef Con As SqlConnection, ByRef Tran As SqlTransaction) As Boolean
        Try

            Qty = Qty * -1
            Dim cmdTrn As New SqlCommand("UPDATE BirthListRequestedItems SET RETURNQTY=ISNULL(RETURNQTY,0) + " & Qty & ",DELIVEREDQTY=isnull(DELIVEREDQTY,0)-" & Qty & ", RETURNREASON='" & RtReason & "'," & _
                "UPDATEDON=GETDATE(),UPDATEDBY='" & Userid & "',UPDATEDAT='" & sitecode & "' WHERE SITECODE='" & sitecode & "' AND BIRTHLISTID='" & BLNumber & "' AND EAN='" & EAN & "'", Con, Tran)
            If CInt(cmdTrn.ExecuteNonQuery()) > 0 Then
                UpdateBL = True
            End If
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    ''' <summary>
    ''' Remove the Hold Data from DataBase Tables
    ''' </summary>
    ''' <param name="SiteCode"></param>
    ''' <param name="StrTrn"></param>
    ''' <remarks></remarks>
    Private Sub RemoveHolddata(ByVal SiteCode As String, ByVal StrTrn As String)
        Try
            Dim cmdTrn As New SqlCommand("Delete From HOLDCASHMEMOHDR   WHERE SITECODE='" & SiteCode & "' AND BillNO='" & StrTrn & "' ", SpectrumCon)
            OpenConnection()
            cmdTrn.ExecuteNonQuery()
            cmdTrn.CommandText = "Delete From HOLDCASHMEMODTL   WHERE SITECODE='" & SiteCode & "' AND BillNO='" & StrTrn & "' ;"
            cmdTrn.CommandText = cmdTrn.CommandText & "Delete From HOLDVoucher   WHERE ISSUEDATSITE='" & SiteCode & "' AND ISSUEDDOCNUMBER='" & StrTrn & "' ;"
            cmdTrn.CommandText = cmdTrn.CommandText & "Delete From HoldCashMemoMettler WHERE BillNO='" & StrTrn & "' ;"
            cmdTrn.ExecuteNonQuery()
            CloseConnection()
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Set Details Table Configuration 
    ''' </summary>
    ''' <param name="ds">Dataset</param>
    ''' <param name="SiteCode">StieCode</param>
    ''' <param name="TableName">TableName</param>
    ''' <param name="BillNo">BillNo</param>
    ''' <remarks></remarks>
    Private Sub SetDetailData(ByRef ds As DataSet, ByVal SiteCode As String, ByVal TableName As String, ByVal BillNo As String, ByVal UserId As String)
        Try

            DeleteColumnFromDataTable(ds.Tables(TableName), "SITECODE")
            DeleteColumnFromDataTable(ds.Tables(TableName), "STOCK")
            DeleteColumnFromDataTable(ds.Tables(TableName), "DISCRIPTION")
            'DeleteColumnFromDataTable(ds.Tables(TableName), "FIRSTLEVEL")
            'DeleteColumnFromDataTable(ds.Tables(TableName), "TOPLEVEL")
            DeleteColumnFromDataTable(ds.Tables(TableName), "CREATEDBY")
            DeleteColumnFromDataTable(ds.Tables(TableName), "CREATEDAT")
            DeleteColumnFromDataTable(ds.Tables(TableName), "CREATEDON")
            DeleteColumnFromDataTable(ds.Tables(TableName), "UPDATEDAT")
            DeleteColumnFromDataTable(ds.Tables(TableName), "UPDATEDBY")
            DeleteColumnFromDataTable(ds.Tables(TableName), "UPDATEDON")
            DeleteColumnFromDataTable(ds.Tables(TableName), "STATUS")
            DeleteColumnFromDataTable(ds.Tables(TableName), "SELECTS")
            DeleteColumnFromDataTable(ds.Tables(TableName), "LASTNODECODE")
            DeleteColumnFromDataTable(ds.Tables(TableName), "IsPriceChanged")

            DeleteColumnFromDataTable(ds.Tables(TableName), "MANUALPROMO")
            DeleteColumnFromDataTable(ds.Tables(TableName), "BILLNO")
            AddColumnToDataTable(ds.Tables(TableName), "SITECODE", "System.String", SiteCode)
            AddColumnToDataTable(ds.Tables(TableName), "BILLNO", "System.String", BillNo)
            AddColumnToDataTable(ds.Tables(TableName), "CREATEDAT", "System.String", SiteCode)
            AddColumnToDataTable(ds.Tables(TableName), "CREATEDBY", "System.String", UserId)
            AddColumnToDataTable(ds.Tables(TableName), "CREATEDON", "System.DateTime", ServerDate)
            AddColumnToDataTable(ds.Tables(TableName), "STATUS", "System.Boolean", 1)
            AddColumnToDataTable(ds.Tables(TableName), "UPDATEDAT", "System.String", SiteCode)
            AddColumnToDataTable(ds.Tables(TableName), "UPDATEDBY", "System.String", UserId)
            AddColumnToDataTable(ds.Tables(TableName), "UPDATEDON", "System.DateTime", ServerDate)

        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Function UpdateVoidStatus(ByVal sitecode As String, ByVal Billno As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Boolean
        Try
            UpdateVoidStatus = False
            Dim StrQuery As String
            StrQuery = "UPDATE CheckDtls SET STATUS=0,UPDATEDON=getdate() WHERE SITECODE='" & sitecode & "' AND BILLNO='" & Billno & "' ;"
            Dim cmdTrn As New SqlCommand(StrQuery, con, tran)
            If CInt(cmdTrn.ExecuteNonQuery()) > 0 Then
                UpdateVoidStatus = True
            End If
        Catch ex As Exception
            UpdateVoidStatus = False
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function


    ''' <summary>
    ''' Update CashMemo status's 
    ''' </summary>
    ''' <param name="Status">Status</param>
    ''' <param name="sitecode">SiteCode</param>
    ''' <param name="Trminal">TerminalId</param>
    ''' <param name="Billno">CashMemoNo</param>
    ''' <param name="con">Connection</param>
    ''' <param name="tran">Transaction</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' 
    Private Function UpdateStatus(ByVal userid As String, ByVal TerminalID As String, ByVal Status As String, ByVal sitecode As String, ByVal Trminal As String, ByVal Billno As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Boolean
        Try
            Dim cmdTrn As New SqlCommand("UPDATE CASHMEMOHDR SET BILLINTERMEDIATESTATUS='" & Status & "',UPDATEDON=GETDATE(),UPDATEDBY='" & userid & "',CREATEDBY='" & userid & "',UPDATEDAT='" & sitecode & "',TERMINALID='" & TerminalID & "'  WHERE SITECODE='" & sitecode & "' AND TERMINALID='" & Trminal & "' AND   BILLNO='" & Billno & "'", con, tran)
            If CInt(cmdTrn.ExecuteNonQuery()) > 0 Then
                UpdateStatus = True
            End If
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    ''' <summary>
    ''' this function is to get the article price based the parameter given
    ''' </summary>
    ''' <param name="Ean"></param>
    ''' <param name="ArticleCode"></param>
    ''' <param name="sitecode"></param>
    ''' <returns> Price</returns>
    ''' <Usedby> frmCashMemo, </Usedby>
    ''' <remarks></remarks>
    Private Function GetarticlePrice(ByVal Ean As String, ByVal ArticleCode As String, ByVal sitecode As String) As Double
        Try
            Dim price As Double = 0.0
            Dim cmd As SqlCommand
            cmd = New SqlCommand("SELECT PRICE FROM SALESINFORECORD WHERE SITECODE='" & sitecode & "' AND EAN='" & Ean & "' AND ARTICLECODE='" & ArticleCode & "'", SpectrumCon)
            OpenConnection()
            price = cmd.ExecuteScalar()
            CloseConnection()
            Return price
        Catch ex As Exception
            LogException(ex)
            Return 0.0
        End Try
    End Function
#End Region
#Region "Public Function's & Method's"

    Public Function InsertHomeDeliveryData(ByVal siteCode As String, ByVal finYear As String, ByVal billNo As String, ByVal terminalId As String,
                                           ByVal hdDate As DateTime, ByVal hdName As String, ByVal hdAddress As String, ByVal hdTelephoneNo As String,
                                           ByVal hdEmail As String, ByVal hdRemarks As String) As Boolean
        Dim insertQuery As String
        If Not String.IsNullOrEmpty(siteCode) Or String.IsNullOrEmpty(finYear) Or String.IsNullOrEmpty(billNo) Or String.IsNullOrEmpty(terminalId) Then
            insertQuery = "insert into CashMemoHdr "
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Get the DB Structure for Binding
    ''' </summary>
    ''' <param name="CashMemo">CashMemo Number</param>
    ''' <param name="SiteCode">SiteCode</param>
    ''' <UpdatedBy></UpdatedBy>
    ''' <usedin>CashMemo</usedin>
    ''' <UpdatedOn></UpdatedOn>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Function GetStruc(ByVal CashMemo As String, ByVal SiteCode As String) As DataSet
        Try
            Dim ds As New DataSet
            daCM = New SqlDataAdapter(" EXEC GetBillStru '" & CashMemo & "','" & SiteCode & "'", SpectrumCon)
            daCM.Fill(ds)
            ds.Tables(0).TableName = "CASHMEMOHDR"
            ds.Tables(1).TableName = "CASHMEMODTL"
            ds.Tables(2).TableName = "CASHMEMORECEIPT"
            ds.Tables(3).TableName = "CASHMEMOMETTLER"
            ds.Tables(4).TableName = "CASHMEMODTLITEMREMARK"
            ''added by ketan Savoy Outstanding changes
            ds.Tables(5).TableName = "SaleOrderTermNConditions"
            Return ds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'code added by vipul for print format 9
    Public Function GetGuruKrupaTaxInvoiceDetails(ByVal SiteCode As String, ByVal BillNo As String) As DataSet
        Dim dsTaxInvoice As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("UDP_GSTCashMemoTaxInvoice", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@BillNo", BillNo)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@SiteCode", SiteCode)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsTaxInvoice = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsTaxInvoice)
            Return dsTaxInvoice
        Catch ex As Exception
            LogException(ex)
            Return dsTaxInvoice
        End Try
    End Function
    Public Function GetCashMemoComboDtlStruc() As DataSet
        Try
            Dim ds As New DataSet
            daCM = New SqlDataAdapter("select * from CashMemoComboDtl where SiteCode = '0'", SpectrumCon)
            daCM.Fill(ds)
            If Not ds.Tables Is Nothing AndAlso ds.Tables.Count > 0 Then
                ds.Tables(0).TableName = "CashMemoComboDtl"
            End If
            Return ds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Get Item Details 
    ''' </summary>
    ''' <param name="SiteCode">SiteCode</param>
    ''' <param name="strItem">Ean Code</param>
    ''' <param name="openMrp"></param>
    ''' <returns>Data Table</returns>
    ''' <remarks></remarks>
    Public Function GetItemDetails(ByVal SiteCode As String, ByVal strItem As String, ByRef openMrp As Boolean, ByRef lngCode As String, Optional isBarCodeScanned As Boolean = False, Optional ByVal SeatingAreaId As Integer = 0) As DataTable
        Try
            Dim dt As New DataTable
            Dim price As Double
            'redo:
            If Not SeatingAreaId = 0 Then
                daCM = New SqlDataAdapter("Exec getItemDetailsSeatingAreaWise '" & SiteCode & "','" & strItem & "','" & SeatingAreaId & "','" & lngCode & "'", SpectrumCon)
            Else
                If isBarCodeScanned Then
                    daCM = New SqlDataAdapter("Exec getItemDetails_BarCode '" & SiteCode & "','" & strItem & "','" & lngCode & "'", SpectrumCon)
                Else
                    daCM = New SqlDataAdapter("Exec getItemDetails '" & SiteCode & "','" & strItem & "','" & lngCode & "'", SpectrumCon)
                End If
            End If
            daCM.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows(0)("IsMrpOpen") = False Then
                    'price = GetarticlePrice(strItem, dt.Rows(0)("Articlecode").ToString(), SiteCode)
                    'dt.Rows(0)("SellingPrice") = price
                    openMrp = False
                ElseIf dt.Rows(0)("IsMrpOpen") = True Then
                    openMrp = True
                End If
            End If
            Return dt
        Catch ex As Exception
            'commented by rama as it goes on infinite loop.This was earlier written because of offline scan run at a single instance.
            'If DirectCast(ex, System.Data.SqlClient.SqlException).ErrorCode = "-2146232060" Then
            '    GoTo redo
            'End If
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetItemDetailsCustomerWise(ByVal SiteCode As String, ByVal strItem As String, ByRef openMrp As Boolean, ByVal CustomerID As String, ByRef lngCode As String, Optional isBarCodeScanned As Boolean = False) As DataTable
        Try
            Dim dt As New DataTable
            Dim price As Double
            'redo:

            '    If Not CustomerID = 0 Then
            daCM = New SqlDataAdapter("Exec getItemDetailsCustomerWise '" & SiteCode & "','" & strItem & "','" & CustomerID & "','" & lngCode & "'", SpectrumCon)
            '   Else
            ' If isBarCodeScanned Then
            'daCM = New SqlDataAdapter("Exec getItemDetails_BarCode '" & SiteCode & "','" & strItem & "','" & lngCode & "'", SpectrumCon)
            '  Else
            ' daCM = New SqlDataAdapter("Exec getItemDetails '" & SiteCode & "','" & strItem & "','" & lngCode & "'", SpectrumCon)
            ' End If
            ' End If
            daCM.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows(0)("IsMrpOpen") = False Then
                    'price = GetarticlePrice(strItem, dt.Rows(0)("Articlecode").ToString(), SiteCode)
                    'dt.Rows(0)("SellingPrice") = price
                    openMrp = False
                ElseIf dt.Rows(0)("IsMrpOpen") = True Then
                    openMrp = True
                End If
            End If
            Return dt
        Catch ex As Exception
            'commented by rama as it goes on infinite loop.This was earlier written because of offline scan run at a single instance.
            'If DirectCast(ex, System.Data.SqlClient.SqlException).ErrorCode = "-2146232060" Then
            '    GoTo redo
            'End If
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetItemDetailsForBulkOrderEdit(ByVal SiteCode As String, ByVal strItem As String, ByRef lngCode As String, Optional ByVal isSingle As Boolean = False, Optional ISCustomerWisePrice As Boolean = False, Optional CustomerWisePriceCardNo As String = "") As DataTable 'PC SO Merge vipin 02-05-2018
        Try
            Dim dt As New DataTable
            Dim coBulkArticleQuery As String
            strItem = strItem.ToString().Split(" ")(0)
            If isSingle = True Then
                coBulkArticleQuery = " Select B.ArticleCode,B.ArticleName,B.ArticleShortName,A.EAN, B.BaseUnitofMeasure,isnull(B.NetWeight,0)  AS Weight , 0 AS Qty ,   0 AS STR_QTY "
                coBulkArticleQuery = coBulkArticleQuery & " FROM MSTEAN A with (nolock) INNER JOIN MSTARTICLE B with (nolock) ON A.ARTICLECODE=B.ARTICLECODE "
            Else
                'vipin
                If ISCustomerWisePrice Then
                    coBulkArticleQuery = " Select B.ArticleCode,B.ArticleName,B.ArticleShortName,A.EAN, B.BaseUnitofMeasure,Case when b.BaseUnitofMeasure ='NOS' then 0 else CASE WHEN isnull (B.NetWeight,0)= 0 THEN 0 WHEN B.NetWeight =0  THEN 0 ELSE B.NetWeight END end AS Weight , ROUND ((SIR.SellingPrice- isnull(#E.ArticlePrize,0)) ,2) as 'Price' ,0 As Discount, 0 AS Qty ,   0 AS STR_QTY  FROM MSTEAN A with (nolock) " 'vipin
                    coBulkArticleQuery = coBulkArticleQuery & " LEFT JOIN (select ArticleCOde,isnull(ArticlePrize,0) 'ArticlePrize' from   Mst_CustomerWiseArticlePrize C with (nolock)  INNER JOIN  CLPCustomers D with (nolock) ON D.level= c.level and d.Status=1   AND d.cardno ='" & CustomerWisePriceCardNo & "' where c.Status=1  ) #E on #E.ARTICLECODE =a.ArticleCode"
                    coBulkArticleQuery = coBulkArticleQuery & " INNER JOIN MSTARTICLE B with (nolock) ON A.ARTICLECODE=B.ARTICLECODE "
                    coBulkArticleQuery = coBulkArticleQuery & " Inner Join  salesInfoRecord SIR on  SIR.ArticleCode=B.ArticleCode And SIR.SiteCode ='" & SiteCode & "'"
                Else
                    coBulkArticleQuery = " Select B.ArticleCode,B.ArticleName,B.ArticleShortName,A.EAN, B.BaseUnitofMeasure,isnull(B.NetWeight,0)  AS Weight ,SIR.SellingPrice as 'Price' ,0 As Discount, 0 AS Qty ,   0 AS STR_QTY  FROM MSTEAN A with (nolock) "
                    coBulkArticleQuery = coBulkArticleQuery & " INNER JOIN MSTARTICLE B with (nolock) ON A.ARTICLECODE=B.ARTICLECODE Inner Join  salesInfoRecord SIR on  SIR.ArticleCode=B.ArticleCode "
                End If
                '  coBulkArticleQuery = " Select B.ArticleCode,B.ArticleName,B.ArticleShortName,A.EAN, B.BaseUnitofMeasure,isnull(B.NetWeight,0)  AS Weight , Case when  b.BaseUnitofMeasure ='KGS' then  SIR.SellingPrice/(1/B.NetWeight) else  SIR.SellingPrice end as 'Price' ,0 As Discount, 0 AS Qty ,   0 AS STR_QTY  FROM MSTEAN A with (nolock) "
                'coBulkArticleQuery = " Select B.ArticleCode,B.ArticleName,B.ArticleShortName,A.EAN, B.BaseUnitofMeasure,isnull(B.NetWeight,0)  AS Weight ,SIR.SellingPrice as 'Price' ,0 As Discount, 0 AS Qty ,   0 AS STR_QTY  FROM MSTEAN A with (nolock) "
                'coBulkArticleQuery = coBulkArticleQuery & " INNER JOIN MSTARTICLE B with (nolock) ON A.ARTICLECODE=B.ARTICLECODE Inner Join  salesInfoRecord SIR on  SIR.ArticleCode=B.ArticleCode "
            End If
            If Not String.IsNullOrEmpty(strItem) Then
                coBulkArticleQuery = coBulkArticleQuery & " Where (A.DefaultEAN = 1) AND (A.EAN='" & strItem & "' Or A.ARTICLECODE='" & strItem & "' or B.LegacyArticleCode = '" & strItem & "' or B.ArticleShortName = '" & strItem & "')"
            End If
            'If ISCustomerWisePrice Then
            '    coBulkArticleQuery = coBulkArticleQuery & " AND d.cardno ='" & CustomerWisePriceCardNo & "'"
            'End If
            dt = GetFilledTable(coBulkArticleQuery)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetItemDetailsForBulkOrder(ByVal SiteCode As String, ByRef lngCode As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim coBulkArticleQuery As String

            coBulkArticleQuery = " Select B.ArticleCode,B.ArticleName,B.ArticleShortName,  B.ArticleCode + REPLICATE(' ', CASE WHEN LEN(B.ARTICLECODE)> 10 THEN 0 ELSE ( case when (10-LEN(B.ARTICLECODE))  > 0 THEN (10-LEN(B.ARTICLECODE)) ELSE 0 END)  END ) + + ' ' +  B.ArticleName  AS ArticleCodeDesc,  A.EAN, B.BaseUnitofMeasure,isnull(B.NetWeight,0)  AS Weight , 0 AS Qty ,   0 AS STR_QTY "
            coBulkArticleQuery = coBulkArticleQuery & " FROM MSTEAN A with (nolock) INNER JOIN MSTARTICLE B with (nolock) ON A.ARTICLECODE=B.ARTICLECODE "


            coBulkArticleQuery = coBulkArticleQuery & " Where (A.DefaultEAN = 1) and B.ArticalTypeCode<>'Combo' and B.status=1 and ArticleActive=1"

            dt = GetFilledTable(coBulkArticleQuery)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetItemDetailsForBulkOrder(ByVal SiteCode As String, ByVal strItem As String, ByRef lngCode As String, Optional prescriptionCheck As Boolean = False, Optional ISCustomerWisePrice As Boolean = False, Optional CustomerWisePriceCardNo As String = "", Optional CallFromPostab As Boolean = False) As DataTable
        Try
            If IsnewSalesOrder Then 'vipin PC SO Merge 03-05-2018
                Dim dt As New DataTable
                Dim coBulkArticleQuery As String
                strItem = strItem.ToString().Split(" ")(0)
                Dim isSingle As Boolean = False
                If isSingle = True Then
                    coBulkArticleQuery = " Select B.ArticleCode,B.ArticleName,B.ArticleShortName,A.EAN, B.BaseUnitofMeasure,isnull(B.NetWeight,0)  AS Weight , 0 AS Qty ,   0 AS STR_QTY "
                    coBulkArticleQuery = coBulkArticleQuery & " FROM MSTEAN A with (nolock) INNER JOIN MSTARTICLE B with (nolock) ON A.ARTICLECODE=B.ARTICLECODE "
                Else
                    If ISCustomerWisePrice Then
                        coBulkArticleQuery = " Select B.ArticleCode,B.ArticleName,B.ArticleShortName,A.EAN, B.BaseUnitofMeasure,Case when b.BaseUnitofMeasure ='NOS' then 0 else CASE WHEN isnull (B.NetWeight,0)= 0 THEN 0 WHEN B.NetWeight =0  THEN 0 ELSE B.NetWeight END end AS Weight , ROUND (Case when  b.BaseUnitofMeasure ='KGS' then  ( SIR.SellingPrice - isnull(#E.ArticlePrize,0)) /(1/ CASE WHEN isnull (B.NetWeight,1)= 1 THEN 1 WHEN B.NetWeight =0  THEN 1 ELSE B.NetWeight END) else  (SIR.SellingPrice- isnull(#E.ArticlePrize,0)) end ,2) as 'Price' ,0 As Discount, 0 AS Qty ,   0 AS STR_QTY  FROM MSTEAN A with (nolock) " 'vipin
                        coBulkArticleQuery = coBulkArticleQuery & " LEFT JOIN (select ArticleCOde,isnull(ArticlePrize,0) 'ArticlePrize' from   Mst_CustomerWiseArticlePrize C with (nolock)  INNER JOIN  CLPCustomers D with (nolock) ON D.level= c.level and d.Status=1   AND d.cardno ='" & CustomerWisePriceCardNo & "' where c.Status=1  ) #E on #E.ARTICLECODE =a.ArticleCode"
                        coBulkArticleQuery = coBulkArticleQuery & " INNER JOIN MSTARTICLE B with (nolock) ON A.ARTICLECODE=B.ARTICLECODE "
                        coBulkArticleQuery = coBulkArticleQuery & " Inner Join  salesInfoRecord SIR on  SIR.ArticleCode=B.ArticleCode And SIR.SiteCode ='" & SiteCode & "'"
                    Else
                        coBulkArticleQuery = " Select B.ArticleCode,B.ArticleName,B.ArticleShortName,A.EAN, B.BaseUnitofMeasure,Case when b.BaseUnitofMeasure ='NOS' then 0 else CASE WHEN isnull (B.NetWeight,0)= 0 THEN 0 WHEN B.NetWeight =0  THEN 0 ELSE B.NetWeight END end AS Weight , ROUND (Case when  b.BaseUnitofMeasure ='KGS' then  SIR.SellingPrice/(1/ CASE WHEN isnull (B.NetWeight,1)= 1 THEN 1 WHEN B.NetWeight =0  THEN 1 ELSE B.NetWeight END) else  SIR.SellingPrice end ,2) as 'Price' ,0 As Discount, 0 AS Qty ,   0 AS STR_QTY  FROM MSTEAN A with (nolock) " 'vipin
                        coBulkArticleQuery = coBulkArticleQuery & " INNER JOIN MSTARTICLE B with (nolock) ON A.ARTICLECODE=B.ARTICLECODE Inner Join  salesInfoRecord SIR on  SIR.ArticleCode=B.ArticleCode And SIR.SiteCode ='" & SiteCode & "'"
                    End If
                    'coBulkArticleQuery = " Select B.ArticleCode,B.ArticleName,B.ArticleShortName,A.EAN, B.BaseUnitofMeasure,Case when b.BaseUnitofMeasure ='NOS' then 0 else CASE WHEN isnull (B.NetWeight,0)= 0 THEN 0 WHEN B.NetWeight =0  THEN 0 ELSE B.NetWeight END end AS Weight , ROUND (Case when  b.BaseUnitofMeasure ='KGS' then  SIR.SellingPrice/(1/ CASE WHEN isnull (B.NetWeight,1)= 1 THEN 1 WHEN B.NetWeight =0  THEN 1 ELSE B.NetWeight END) else  SIR.SellingPrice end ,2) as 'Price' ,0 As Discount, 0 AS Qty ,   0 AS STR_QTY  FROM MSTEAN A with (nolock) " 'vipin
                    'coBulkArticleQuery = coBulkArticleQuery & " INNER JOIN MSTARTICLE B with (nolock) ON A.ARTICLECODE=B.ARTICLECODE Inner Join  salesInfoRecord SIR on  SIR.ArticleCode=B.ArticleCode And SIR.SiteCode ='" & SiteCode & "'"
                End If
                If Not String.IsNullOrEmpty(strItem) Then
                    coBulkArticleQuery = coBulkArticleQuery & " Where (A.DefaultEAN = 1) AND (A.EAN='" & strItem & "' Or A.ARTICLECODE='" & strItem & "' or B.LegacyArticleCode = '" & strItem & "' or B.ArticleShortName = '" & strItem & "')"
                End If
                'If ISCustomerWisePrice Then
                '    coBulkArticleQuery = coBulkArticleQuery & " AND d.cardno ='" & CustomerWisePriceCardNo & "'"
                'End If
                dt = GetFilledTable(coBulkArticleQuery)
                Return dt
            Else
                Dim dt As New DataTable
                Dim coBulkArticleQuery As String
                strItem = strItem.ToString().Split(" ")(0)
                If prescriptionCheck = True Then
                    If CallFromPostab Then
                        coBulkArticleQuery = " Select B.ArticleCode,B.ArticleName,B.ArticleShortName,'' as ConsumptionRate, '' as Duration,B.ArticleCode + REPLICATE(' ', CASE WHEN LEN(B.ARTICLECODE)> 10 THEN 0 ELSE ( case when (10-LEN(B.ARTICLECODE))  > 0 THEN (10-LEN(B.ARTICLECODE)) ELSE 0 END)  END ) + + ' ' +  B.ArticleName  AS ArticleCodeDesc,  A.EAN, B.BaseUnitofMeasure,(B.articalTypeCode +' '+B.baseUnitofMeasure +' '+B.materialTypeCode ) as AdditonalInfo ,isnull(B.NetWeight,0)  AS Weight , 0 AS Qty ,   0 AS STR_QTY "
                    Else
                        coBulkArticleQuery = " Select B.ArticleCode,B.ArticleName,B.ArticleShortName,'' as ConsumptionRate, '' as Duration ,B.ArticleCode + REPLICATE(' ', CASE WHEN LEN(B.ARTICLECODE)> 10 THEN 0 ELSE ( case when (10-LEN(B.ARTICLECODE))  > 0 THEN (10-LEN(B.ARTICLECODE)) ELSE 0 END)  END ) + + ' ' +  B.ArticleName  AS ArticleCodeDesc,  A.EAN, B.BaseUnitofMeasure,isnull(B.NetWeight,0)  AS Weight , 0 AS Qty ,   0 AS STR_QTY "
                    End If
                Else
                    If CallFromPostab Then
                        coBulkArticleQuery = " Select B.ArticleCode,B.ArticleName,B.ArticleShortName,A.EAN, B.BaseUnitofMeasure,(B.articalTypeCode +' '+B.baseUnitofMeasure +' '+B.materialTypeCode ) as AdditonalInfo,isnull(B.NetWeight,0)  AS Weight , 0 AS Qty ,   0 AS STR_QTY "
                    Else
                        coBulkArticleQuery = " Select B.ArticleCode,B.ArticleName,B.ArticleShortName,A.EAN, B.BaseUnitofMeasure,isnull(B.NetWeight,0)  AS Weight , 0 AS Qty ,   0 AS STR_QTY "
                    End If
                End If
                coBulkArticleQuery = coBulkArticleQuery & " FROM MSTEAN A with (nolock) INNER JOIN MSTARTICLE B with (nolock) ON A.ARTICLECODE=B.ARTICLECODE "

                If Not String.IsNullOrEmpty(strItem) Then
                    coBulkArticleQuery = coBulkArticleQuery & " Where (A.DefaultEAN = 1) AND (A.EAN='" & strItem & "' Or A.ARTICLECODE='" & strItem & "' or B.LegacyArticleCode = '" & strItem & "' or B.ArticleShortName = '" & strItem & "')"
                End If
                dt = GetFilledTable(coBulkArticleQuery)
                Return dt
            End If


        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


    Public Function GetItemDetails(ByVal SiteCode As String, ByVal strItem As String, ByRef lngCode As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim coBulkArticleQuery As String
            coBulkArticleQuery = " Select B.ArticleCode ,B.ArticleShortName ,B.ArticleName "
            coBulkArticleQuery = coBulkArticleQuery & " FROM MSTEAN A with (nolock) INNER JOIN MSTARTICLE B with (nolock) ON A.ARTICLECODE=B.ARTICLECODE "
            If Not String.IsNullOrEmpty(strItem) Then
                coBulkArticleQuery = coBulkArticleQuery & " Where (A.EAN='" & strItem & "' Or A.ARTICLECODE='" & strItem & "' or B.LegacyArticleCode = '" & strItem & "' or B.ArticleName = '" & strItem & "')"
            End If
            coBulkArticleQuery = coBulkArticleQuery & " Union "
            coBulkArticleQuery = coBulkArticleQuery & " Select B.ArticleShortName as ArticleCode  , B.ArticleCode as ArticleShortName,B.ArticleName "
            coBulkArticleQuery = coBulkArticleQuery & " FROM MSTEAN A with (nolock) INNER JOIN MSTARTICLE B with (nolock) ON A.ARTICLECODE=B.ARTICLECODE "
            If Not String.IsNullOrEmpty(strItem) Then
                coBulkArticleQuery = coBulkArticleQuery & " Where (A.EAN='" & strItem & "' Or A.ARTICLECODE='" & strItem & "' or B.LegacyArticleCode = '" & strItem & "' or B.ArticleName = '" & strItem & "')"
            End If
            dt = GetFilledTable(coBulkArticleQuery)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function



    Public Function CheckIFGlNoRangeIsAvailable(ByVal siteCode As String) As Boolean
        Try
            Dim result As Boolean
            Dim BAdocNo As String = getDocumentNo("BA", siteCode)
            Dim BGdocNo As String = getDocumentNo("BG", siteCode)
            If Not String.IsNullOrEmpty(BAdocNo) AndAlso Not String.IsNullOrEmpty(BGdocNo) Then
                result = True
            End If
            Return result
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    'modified by khusrao adil on 21-03-2018 for spectrum Lite master
    'added new optional parameter -AddWithUpdate
    Public Function SaveAndDeleteButtonArticleData(ByVal buttonID As String, ByVal groupId As String, ByVal articlecode As String, ByVal articlename As String, ByVal condition As String, ByVal userId As String, ByVal siteCode As String, Optional ByVal AddWithUpdate As Boolean = True) As Boolean
        Dim articleInsertQuery As String
        Dim atricleUpdateQuery As String
        Dim atricleDelete As String
        Try
            If condition <> String.Empty Then
                If condition = "Add" Then
                    If groupId <> String.Empty AndAlso articlecode <> String.Empty AndAlso articlename <> String.Empty Then
                        Dim query As String = "Select * from buttonarticle where ARTICLECODE = '" & articlecode & "' and Sitecode in ('" & siteCode & "','CCE') and GROUPID = '" & groupId & "' "
                        Dim dt As DataTable = GetFilledTable(query)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            If AddWithUpdate = False Then 'added and modified by khusrao adil on 21-03-2018 for spectrum Lite developement
                                atricleUpdateQuery = "update buttonarticle set Status=0,isActive=0, UpdatedBy = '" & userId & "', UpdatedOn='" & GetCurrentDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "' where ARTICLECODE = '" & articlecode & "' and Sitecode in ('" & siteCode & "','CCE') and GROUPID = '" & groupId & "' "
                            Else
                                atricleUpdateQuery = "update buttonarticle set Status=1,isActive=1, UpdatedBy = '" & userId & "', UpdatedOn='" & GetCurrentDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "' where ARTICLECODE = '" & articlecode & "' and Sitecode in ('" & siteCode & "','CCE') and GROUPID = '" & groupId & "' "
                            End If
                            UpdateButtonArticleData(atricleUpdateQuery)
                        Else
                            'Dim character As Char = siteCode.FirstOrDefault(Function(x) x <> "0")
                            Dim docNo As String = getDocumentNo("BA", siteCode)
                            'Dim otherCharacters As String = "BAS" & siteCode.Substring(siteCode.IndexOf(character))
                            'Dim newArticleId As String = GenDocNo(otherCharacters, 15, docNo)
                            Dim newArticleId As String = GenDocNo("BAS" & siteCode.Substring(siteCode.Length - 3, 3), 15, docNo)
                            ''articleInsertQuery = "insert into buttonarticle (ButtonName,ArticleCode,ArticleName) values ('" & btnName & "','" & articlecode & "','" & articlename & "')"
                            articleInsertQuery = "insert into buttonarticle (ID,GROUPID,ARTICLECODE,ARTICLENAME,ISACTIVE,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS,SITECODE) values " & _
                                "('" & newArticleId & "','" & groupId & "','" & articlecode & "','" & articlename & "'," & "1" & ",'" & siteCode & "','" & userId & "','" & GetCurrentDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & _
                                "','" & siteCode & "','" & userId & "','" & GetCurrentDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "',1,'" & siteCode & "')"
                            UpdateButtonArticleData(articleInsertQuery)
                            UpdateDocumentNo("BA", SpectrumCon, Nothing)
                        End If
                    End If
                ElseIf condition = "Edit" Then
                    If buttonID <> String.Empty AndAlso articlecode <> String.Empty AndAlso articlename <> String.Empty Then
                        atricleUpdateQuery = "update buttonarticle set Articlecode='" & articlecode & "',ArticleName='" & articlename & "', UpdatedBy = '" & userId & "', UpdatedOn='" & GetCurrentDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "' where ID='" & buttonID & "'"
                        UpdateButtonArticleData(atricleUpdateQuery)
                    End If
                ElseIf condition = "Remove" Then
                    If buttonID <> String.Empty Then
                        'atricleDelete = " delete from buttonarticle where ID ='" & buttonID & "'"                    
                        atricleDelete = "update buttonarticle set Status=0, UpdatedBy = '" & userId & "', UpdatedOn='" & GetCurrentDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "' where Articlecode in (Select ArticleCode from buttonarticle where ID ='" & buttonID & "')"
                        UpdateButtonArticleData(atricleDelete)
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    'modified by khusrao adil on 16-03-2018
    'added new optional parameter -ParentGroupID
    Public Function SaveAndEditButtonGroup(ByVal groupId As String, ByVal groupName As String, ByVal condition As String, ByVal userId As String, ByVal siteCode As String, Optional ParentGroupID As String = "") As Boolean
        Dim btnGroupInsertQuery As String
        Dim btnGroupUpdateQuery As String
        Try
            If Not String.IsNullOrEmpty(condition) Then
                If condition = "Add" Then
                    Dim objParentGroupID As Object
                    If ParentGroupID = "" Then
                        objParentGroupID = DBNull.Value
                    Else
                        objParentGroupID = ParentGroupID
                    End If
                    If groupName <> String.Empty Then
                        'Dim character As Char = siteCode.FirstOrDefault(Function(x) x <> "0")
                        Dim docNo As String = getDocumentNo("BG", siteCode)
                        'Dim otherCharacters As String = "BGS" & siteCode.Substring(siteCode.IndexOf(character))
                        'Dim newGroupId As String = GenDocNo(otherCharacters, 15, docNo)
                        Dim newGroupId As String = GenDocNo("BGS" & siteCode.Substring(siteCode.Length - 3, 3), 15, docNo)
                        btnGroupInsertQuery = "insert into ButtonGroup (GROUPID,GROUPNAME,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS,ISACTIVE,SITECODE,ParentGroupID) values " & _
                            "('" & newGroupId & "', '" & groupName & "','" & siteCode & "','" & userId & "','" & GetCurrentDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & _
                            "','" & siteCode & "','" & userId & "','" & GetCurrentDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "',1,1,'" & siteCode & "','" & objParentGroupID & "')"
                        UpdateButtonArticleData(btnGroupInsertQuery)
                        UpdateDocumentNo("BG", SpectrumCon, Nothing)
                    End If
                End If
                If condition = "Edit" Then
                    If groupName <> String.Empty AndAlso groupId <> String.Empty Then
                        btnGroupUpdateQuery = "update ButtonGroup set GroupName='" & groupName & "', UpdatedBy = '" & userId & "', UpdatedOn='" & GetCurrentDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "' where GroupID='" & groupId & "'"
                        UpdateButtonArticleData(btnGroupUpdateQuery)
                    End If
                End If
                If condition = "Remove" Then
                    If groupId <> String.Empty Then
                        btnGroupUpdateQuery = "update buttonarticle set Status=0, UpdatedBy = '" & userId & "', UpdatedOn='" & GetCurrentDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "' where GroupID in (Select GROUPID  from ButtonGroup where GROUPNAME  = (select GROUPNAME from ButtonGroup where GROUPID = '" & groupId & "')) "
                        UpdateButtonArticleData(btnGroupUpdateQuery)
                        btnGroupUpdateQuery = "update ButtonGroup set Status=0, UpdatedBy = '" & userId & "', UpdatedOn='" & GetCurrentDate().ToString("yyyy-MM-dd HH:mm:ss.fff") & "'  where GroupID in (Select GROUPID  from ButtonGroup where GROUPNAME  = (select GROUPNAME from ButtonGroup where GROUPID = '" & groupId & "')) "
                        UpdateButtonArticleData(btnGroupUpdateQuery)
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function


    'code added by vipul for checking kit article mapping in combo article
    Public Function CheckKitMapping(ByVal Articlecode As String) As Boolean
        Dim strResult As Boolean = False
        Try
            Dim dt As New DataTable

            Dim sqlComm As New SqlCommand("USP_CheckKitMapping", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@Articlecode", Articlecode)
            sqlComm.CommandTimeout = 0
            sqlComm.CommandType = CommandType.StoredProcedure
            dt = New DataTable
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dt)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    strResult = dt.Rows(0)(0).ToString()
                    Return strResult
                End If
            End If
            Return strResult
        Catch ex As Exception
            LogException(ex)
            Return strResult
        End Try
    End Function
    'code added by vipul for getting currrent date tiem of sql server
    Public Function GetSQLServerDateTime(ByRef con As SqlConnection, ByRef tran As SqlTransaction) As DateTime
        Try
            Dim strQuery As String = ""
            strQuery = "select getdate()"
            Dim cmdVoucher As SqlCommand = New SqlCommand(strQuery, con)
            cmdVoucher.Transaction = tran
            Dim Objdate = cmdVoucher.ExecuteScalar()
            Return Convert.ToDateTime(Objdate)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetComponentArticleItems(ByRef ArticleCode As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim componentArticleQuery As String
            ' componentArticleQuery = "select ArticleCode,EAN from ArticleComponent where  ArticleCode='" & ArticleCode & "' = (select componentArticle from ArticleComponent where ArticleCode='" & ArticleCode & "') and ArticleTypeCode='Kit' "
            componentArticleQuery = "select ArticleCode,EAN,quantity from ArticleComponent where  componentArticle='" & ArticleCode & "' and ArticleTypeCode='Kit'"
            dt = GetFilledTable(componentArticleQuery)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'code added by vipul for Checking KIT article
    Public Function CheckArticlecodePresentInKit(ByVal ArticleCode As String) As Boolean
        Try
            Dim result As Boolean = False
            Dim dt As New DataTable
            Dim query As String
            query = "select ArticleCode,EAN,quantity from MSTARTICLEKIT where  KitArticleCode='" & ArticleCode & "' and STATUS=1"
            dt = GetFilledTable(query)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                result = True
            End If
            Return result

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetMultiButtonArticle(ByVal siteCode As String, Optional ByVal btnName As String = "") As DataTable
        Try
            Dim dt As New DataTable
            'Dim dtUnique As New DataTable
            Dim btnArticaleQuery As String

            If btnName <> String.Empty Then
                btnArticaleQuery = "select * from ButtonArticle Where ButtonName = '" & btnName & "'"
            Else
                'btnArticaleQuery = "select * from ButtonArticle"
                'btnArticaleQuery = "Select BA.ID,Ba.GroupID ,Ba.Articlecode ,Ba.sitecode,  Mst.ArticleShortName AS ArticleName,BA.IsActive" & _
                '                                           " From ButtonArticle As BA Inner Join MstArticle AS Mst On Ba.Articlecode = Mst.ArticleCode Inner Join SalesInfoRecord SI on Mst.ArticleCode= SI.ArticleCode where Mst.ArticleActive = 1 and BA.SITECODE in ('" & siteCode & "','CCE') and BA.status=1 Order By Mst.ArticleShortName "
                'modifed by khusrao adil on 11-12-2017 for jk sprint 32
                'button article shoud not display when salesinforecord have entry with status as false for that article specific with site
                btnArticaleQuery = " Select BA.ID,Ba.GroupID ,Ba.Articlecode ,Ba.sitecode,  Mst.ArticleShortName AS ArticleName,BA.IsActive, " & _
                                   "Case when upper(Mst.ArticalTypeCode)='KIT' then '1' else '0' END AS IsKit," & _
                                    "Case when upper(Mst.ArticalTypeCode)='KIT' then dbo.ListOfKitArticle(Mst.ArticleCode)" & _
                                    "else Mst.ArticleShortName END AS KitArticleDesc " & _
                                  " From ButtonArticle As BA Inner Join MstArticle AS Mst On Ba.Articlecode = Mst.ArticleCode and BA.SITECODE in ('" & siteCode & "','CCE') " & _
                                  " Inner Join SalesInfoRecord SI on Mst.ArticleCode= SI.ArticleCode and SI.SiteCode='" & siteCode & "' and SI.status=1 " & _
                                  " where Mst.ArticleActive = 1 and   BA.status=1 Order By Mst.ArticleShortName "
            End If

            dt = GetFilledTable(btnArticaleQuery)
            Dim dv As New DataView(dt, "", "", DataViewRowState.CurrentRows)
            Dim dtUnique As DataTable = dv.ToTable(True, "GroupID")
            For j As Integer = 0 To dtUnique.Rows.Count - 1
                Dim articleCodeList As New Dictionary(Of String, Integer)()
                For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                    If dt.Rows(i).Item("GroupID") = dtUnique.Rows(j).Item("GroupID") Then
                        If articleCodeList.ContainsKey(dt.Rows(i)("Articlecode")) Then
                            If dt.Rows(i)("Sitecode") <> siteCode Then
                                dt.Rows(i).Delete()
                            Else
                                Dim index = articleCodeList(dt.Rows(i)("Articlecode"))
                                dt.Rows(index).Delete()
                            End If
                        Else
                            articleCodeList.Add(dt.Rows(i)("Articlecode"), i)
                        End If
                    End If
                Next
                dt.AcceptChanges()
            Next
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function IsLastSubGroup(ByVal siteCode As String, Optional ByVal btnName As String = "") As Boolean
        Try
            Dim result As Boolean = False
            Dim dataTable As DataTable
            Dim query As String = "select * from BUTTONGROUP where  SITECODE in ('" & siteCode & "','CCE') and PARENTGROUPID in('" & btnName & "')"
            dataTable = GetFilledTable(query)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                result = True
            End If
            Return result
        Catch ex As Exception
            LogException(ex)

        End Try
    End Function
    Public Function IsLastSubGroupById(ByVal siteCode As String, Optional ByVal btnName As String = "") As Boolean
        Try
            Dim result As Boolean = False
            Dim dataTable As DataTable
            Dim query As String = "select * from BUTTONGROUP where  SITECODE in ('" & siteCode & "','CCE') and GROUPID in('" & btnName & "') and STATUS=1 and IsSubGroup=1"
            dataTable = GetFilledTable(query)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                result = True
            End If
            Return result
        Catch ex As Exception
            LogException(ex)

        End Try
    End Function
    Public Function ParentGroupId(ByVal siteCode As String, Optional ByVal btnName As String = "") As String
        Try
            Dim result As String
            Dim dataTable As DataTable
            Dim query As String = "select GROUPID from BUTTONGROUP Where GROUPNAME='" & btnName & "' and STATUS=1   and SITECODE in ('" & siteCode & "')"
            dataTable = GetFilledTable(query)
            If Not dataTable Is Nothing AndAlso dataTable.Rows.Count > 0 Then
                result = dataTable.Rows(0)("GroupId").ToString()
            End If
            Return result
        Catch ex As Exception
            LogException(ex)

        End Try
    End Function
    Public Function GetButtonArticleByGroup(ByVal siteCode As String, Optional ByVal btnName As String = "") As DataTable
        Try
            Dim dt As New DataTable
            'Dim dtUnique As New DataTable
            Dim btnArticaleQuery As String

            If btnName <> String.Empty Then

                'btnArticaleQuery = "select * from ButtonArticle"
                btnArticaleQuery = "Select BA.ID,Ba.GroupID ,Ba.Articlecode ,Ba.sitecode,  Mst.ArticleShortName AS ArticleName,BA.IsActive" & _
                                      " From ButtonArticle As BA Inner Join MstArticle AS Mst On Ba.Articlecode = Mst.ArticleCode Inner Join SalesInfoRecord SI on Mst.ArticleCode= SI.ArticleCode where Mst.ArticleActive = 1 and BA.SITECODE in ('" & siteCode & "') and Ba.GROUPID in('" & btnName & "') and BA.status=1 Order By Mst.ArticleShortName "
            End If

            dt = GetFilledTable(btnArticaleQuery)
            Dim dv As New DataView(dt, "", "", DataViewRowState.CurrentRows)
            Dim dtUnique As DataTable = dv.ToTable(True, "GroupID")
            For j As Integer = 0 To dtUnique.Rows.Count - 1
                Dim articleCodeList As New Dictionary(Of String, Integer)()
                For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                    If dt.Rows(i).Item("GroupID") = dtUnique.Rows(j).Item("GroupID") Then
                        If articleCodeList.ContainsKey(dt.Rows(i)("Articlecode")) Then
                            If dt.Rows(i)("Sitecode") <> siteCode Then
                                dt.Rows(i).Delete()
                            Else
                                Dim index = articleCodeList(dt.Rows(i)("Articlecode"))
                                dt.Rows(index).Delete()
                            End If
                        Else
                            articleCodeList.Add(dt.Rows(i)("Articlecode"), i)
                        End If
                    End If
                Next
                dt.AcceptChanges()
            Next
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetSubGroupsByGroup(ByVal siteCode As String, Optional ByVal btnName As String = "") As DataTable
        Try
            Dim dt As New DataTable
            'Dim dtUnique As New DataTable
            Dim btnArticaleQuery As String

            If btnName <> String.Empty Then

                'btnArticaleQuery = "select * from ButtonArticle"
                btnArticaleQuery = "select * from dbo.GetButtonGroupsSubGroups('" & btnName & "','" & siteCode & "')  "
            End If

            dt = GetFilledTable(btnArticaleQuery)

            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetMultiButtonGroup(ByVal siteCode As String, Optional ByVal btnGroupId As Integer = 0) As DataTable
        Try
            Dim dt As New DataTable
            Dim btnGroupQuery As String

            If btnGroupId <> 0 Then
                btnGroupQuery = "select * from ButtonGroup where groupid = '" & btnGroupId & "'"
            Else
                btnGroupQuery = "select * from ButtonGroup where SITECODE in ('" & siteCode & "','CCE') and status=1 and (ParentGroupID is null or ParentGroupID='') "
            End If
            Dim tabList As New Dictionary(Of String, Integer)()
            dt = GetFilledTable(btnGroupQuery)
            For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                If tabList.ContainsKey(dt.Rows(i)("GroupName")) Then
                    If dt.Rows(i)("Sitecode") <> siteCode Then
                        dt.Rows(i).Delete()
                    Else
                        Dim index = tabList(dt.Rows(i)("GroupName"))
                        dt.Rows(index).Delete()
                    End If
                Else
                    tabList.Add(dt.Rows(i)("GroupName"), i)
                End If
            Next
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function IsMultiTabAllowed() As Boolean
        Try
            Dim strString As String = "select FldLabel ,FldValue  from DefaultConfig  where FldLabel in('IS_POSTAB_GROUP_HIERARCHY_ALLOWED') and Sitecode='BOCommon'"
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(strString, ConString)
            da.Fill(dt)
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    Dim res As String = dt.Rows(0)("FldValue").ToString()
                    If res.ToUpper() = "TRUE" Then
                        Return True
                    End If
                    Return False
                Else
                    Return False
                End If

            End If
            Return False
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    ''old start 
    Public Function GetButtonArticle(ByVal siteCode As String, Optional ByVal btnName As String = "") As DataTable
        Try
            Dim dt As New DataTable
            'Dim dtUnique As New DataTable
            Dim btnArticaleQuery As String

            If btnName <> String.Empty Then
                btnArticaleQuery = "select * from ButtonArticle Where ButtonName = '" & btnName & "'"
            Else
                'btnArticaleQuery = "select * from ButtonArticle"
                'btnArticaleQuery = "Select BA.ID,Ba.GroupID ,Ba.Articlecode ,Ba.sitecode,  Mst.ArticleShortName AS ArticleName,BA.IsActive" & _
                '                   " From ButtonArticle As BA Inner Join MstArticle AS Mst On Ba.Articlecode = Mst.ArticleCode Inner Join SalesInfoRecord SI on Mst.ArticleCode= SI.ArticleCode where Mst.ArticleActive = 1 and BA.SITECODE in ('" & siteCode & "','CCE') and BA.status=1 Order By Mst.ArticleShortName "  'Issue no. 0015232 
                'modifed by khusrao adil on 11-12-2017 for jk sprint 32
                'button article shoud not display when salesinforecord have entry with status as false for that article specific with site
                btnArticaleQuery = " Select BA.ID,Ba.GroupID ,Ba.Articlecode ,Ba.sitecode,  Mst.ArticleShortName AS ArticleName,BA.IsActive " & _
                                   " From ButtonArticle As BA Inner Join MstArticle AS Mst On Ba.Articlecode = Mst.ArticleCode and BA.SITECODE in  ('" & siteCode & "','CCE') " & _
                                   " Inner Join SalesInfoRecord SI on Mst.ArticleCode= SI.ArticleCode and SI.SiteCode='" & siteCode & "' and SI.status=1 " & _
                                   " where Mst.ArticleActive = 1 and   BA.status=1 Order By Mst.ArticleShortName "
            End If

            dt = GetFilledTable(btnArticaleQuery)
            Dim dv As New DataView(dt, "", "", DataViewRowState.CurrentRows)
            Dim dtUnique As DataTable = dv.ToTable(True, "GroupID")
            For j As Integer = 0 To dtUnique.Rows.Count - 1
                Dim articleCodeList As New Dictionary(Of String, Integer)()
                For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                    If dt.Rows(i).Item("GroupID") = dtUnique.Rows(j).Item("GroupID") Then
                        If articleCodeList.ContainsKey(dt.Rows(i)("Articlecode")) Then
                            If dt.Rows(i)("Sitecode") <> siteCode Then
                                dt.Rows(i).Delete()
                            Else
                                Dim index = articleCodeList(dt.Rows(i)("Articlecode"))
                                dt.Rows(index).Delete()
                            End If
                        Else
                            articleCodeList.Add(dt.Rows(i)("Articlecode"), i)
                        End If
                    End If
                Next
                dt.AcceptChanges()
            Next
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetButtonGroup(ByVal siteCode As String, Optional ByVal btnGroupId As Integer = 0) As DataTable
        Try
            Dim dt As New DataTable
            Dim btnGroupQuery As String

            If btnGroupId <> 0 Then
                btnGroupQuery = "select * from ButtonGroup where groupid = '" & btnGroupId & "'"
            Else
                btnGroupQuery = "select * from ButtonGroup where SITECODE in ('" & siteCode & "','CCE') and status=1  "
            End If
            Dim tabList As New Dictionary(Of String, Integer)()
            dt = GetFilledTable(btnGroupQuery)
            For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                If tabList.ContainsKey(dt.Rows(i)("GroupName")) Then
                    If dt.Rows(i)("Sitecode") <> siteCode Then
                        dt.Rows(i).Delete()
                    Else
                        Dim index = tabList(dt.Rows(i)("GroupName"))
                        dt.Rows(index).Delete()
                    End If
                Else
                    tabList.Add(dt.Rows(i)("GroupName"), i)
                End If
            Next
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''old end
    Public Function UpdateButtonArticleData(ByVal Query As String) As Boolean

        OpenConnection()
        Dim cmd As New SqlCommand
        cmd.CommandText = Query
        cmd.Connection = SpectrumCon()
        cmd.ExecuteNonQuery()
        Return True

    End Function
    ''' <summary>
    ''' Save Cash Memo Data To DB.
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="sitecode">SiteCode</param>
    ''' <param name="Terminalid">TerminalID</param>
    ''' <param name="userid">UserId</param>
    ''' <param name="UpdateFlag">True/False</param>
    ''' <param name="BillNo">BillNo</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Function SaveCashMemo(ByVal DayOpenDate As DateTime, ByVal Online As Boolean, ByRef ds As DataSet, ByRef dsCombo As DataSet, ByVal sitecode As String, ByVal terminalid As String, _
                                 ByVal userid As String, ByVal UpdateFlag As Boolean, ByRef BillNo As String, ByRef msgError As String, ByVal FYear As String, _
                                 ByVal Storage As String, ByVal DocType As String, Optional ByRef dtGV As DataTable = Nothing, Optional ByVal CLPArticle As String = "", Optional ByVal CLPCustomerId As String = "", Optional ByVal ClpProgramId As String = "", _
                                 Optional ByVal GVProgramId As String = "", Optional ByVal CVProgramId As String = "", Optional ByVal VoucherDays As Int32 = 0, _
                                 Optional ByVal CLPRed As Boolean = False, Optional ByRef CLPRedemptionPoints As Double = 0, Optional ByVal FloatAmt As Double = 0, _
                                 Optional ByVal UpdateBillTime As Boolean = False, Optional ByVal IsMemberShip As Boolean = False, _
                                 Optional ByVal UpdateStockAtStoreLevel As Boolean = False, Optional ByRef dsMergeOrderDtl As DataSet = Nothing, _
                                 Optional ByVal IsHostManagement As Boolean = False, _
                                  Optional ByVal innovatiiForTerminals As String = "", _
                                  Optional ByVal InnovitiPaymentEnable As Boolean = False) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            ServerDate = GetCurrentDate()

            Dim dsVoucher As DataSet
            Dim DeliveryPerson = ds.Tables("CashMemoHdr").Rows(0)("DeliveryPersonID").ToString()
            Dim DeliveryPartner = ds.Tables("CashMemoHdr").Rows(0)("DeliveryPartnerId").ToString()
            If UpdateFlag = True Then
                If ds.Tables("CashMemoReceipt").Rows.Count = 0 Then Return True
                If PrepareUpdateData(DayOpenDate, ds, FYear, userid, sitecode, ServerDate) = True Then
                    If dtGV Is Nothing Then
                        dtGV = GetVoucherStru()
                    End If
                    OpenConnection()
                    tran = SpectrumCon.BeginTransaction()
                    If updateCashMemoReceipt(ds, ds.Tables("CashMemoHdr").Rows(0)("billno").ToString(), ds.Tables("CashMemoHdr").Rows(0)("TerminalId").ToString(), ds.Tables("CashMemoHdr").Rows(0)("Sitecode").ToString(), SpectrumCon, tran, userid, ClpProgramId, CLPCustomerId, CLPRed) = True Then
                        If SaveTenders(ds, tran, DayOpenDate, sitecode, terminalid, userid, BillNo, DocType, dtGV, CLPArticle, CLPCustomerId, ClpProgramId, GVProgramId, CVProgramId, VoucherDays, CLPRed, CLPRedemptionPoints) = False Then
                            Return False
                        End If
                        If SaveData(ds, SpectrumCon, tran) Then
                            '-------------------------------------- innoviti start--------------------------------------
                            If InnovitiPaymentEnable = True AndAlso innovatiiForTerminals.Contains(terminalid) Then
                                Dim RetrievalReferenceNumber As String
                                Dim TransactionTime As String
                                If clsInnovitiList IsNot Nothing Then
                                    Dim row = ds.Tables("CASHMEMORECEIPT").Select("TenderTypeCode='CreditCard'")
                                    If row.Count = 1 Then
                                        For Each kvp As KeyValuePair(Of String, String) In clsInnovitiList
                                            If kvp.Key = "RetrievalReferenceNumber" Then
                                                RetrievalReferenceNumber = kvp.Value.ToUpper
                                            ElseIf kvp.Key = "TransactionTime" Then
                                                TransactionTime = kvp.Value.ToUpper
                                            End If
                                        Next
                                        If Not SaveInnovitiData(BillNo, terminalid, row(0)("AmountTendered"), sitecode, ds.Tables("CASHMEMOHdr").Rows(0)("Finyear").ToString(), userid, "CMS", RetrievalReferenceNumber, TransactionTime, tran) Then
                                            tran.Rollback()
                                            CloseConnection()
                                            Return False
                                        End If
                                    Else
                                        If dtInnoviti.Rows.Count > 0 Then
                                            For Each kvp As KeyValuePair(Of String, String) In clsInnovitiList
                                                If kvp.Key = "RetrievalReferenceNumber" Then
                                                    RetrievalReferenceNumber = kvp.Value.ToUpper
                                                ElseIf kvp.Key = "TransactionTime" Then
                                                    TransactionTime = kvp.Value.ToUpper
                                                End If
                                            Next
                                            For Each drInnvoiti As DataRow In dtInnoviti.Rows
                                                '  Dim strAmountTender As String = drInnvoiti("AmountTendered")
                                                If Not SaveInnovitiData(BillNo, terminalid, drInnvoiti("AmountTendered"), sitecode, ds.Tables("CASHMEMOHdr").Rows(0)("Finyear").ToString(), userid, "CMS", RetrievalReferenceNumber, TransactionTime, tran) Then
                                                    tran.Rollback()
                                                    CloseConnection()
                                                    Return False
                                                End If
                                            Next
                                        End If
                                    End If
                                End If
                            End If
                            '-------------------------------------- innoviti end--------------------------------------
                            If UpdateStatus(userid, terminalid, "Updated", ds.Tables("CashMemoHdr").Rows(0)("SiteCode").ToString(), ds.Tables("CashMemoHdr").Rows(0)("TerminalId").ToString(), ds.Tables("CashMemoHdr").Rows(0)("Billno").ToString(), SpectrumCon, tran) Then
                                If AssignVoucherToBill(SpectrumCon, tran, sitecode, BillNo, FYear, "CMS") = False Then
                                    tran.Rollback()
                                    CloseConnection()
                                    Return False
                                End If

                                '--------For Updating Float Amt in Voucher Entry
                                Dim BillDate = ds.Tables("CASHMEMOHdr").Rows(0)("BillDate")
                                dsVoucher = GetVoucherFloatData(BillNo, BillDate, tran)
                                If FloatAmt <> 0 Then
                                    If UpdateFloatAmtData(FloatAmt, DeliveryPerson, BillNo, BillDate, tran, dsVoucher) = False Then
                                        tran.Rollback()
                                        CloseConnection()
                                        Return False
                                    End If
                                End If
                                If UpdateBillTime = True Then
                                    If GetUpdateBillTime(BillNo, "CM", tran) Then
                                    End If
                                End If
                                tran.Commit()

                                'Rakesh:09-July-2013-->Start: Template based cashmemo bill printing parameter
                                '_dsCashMemoPrinting = ds
                                dsCashMemoPrinting = ds

                                If dsCombo IsNot Nothing Then
                                    For Each dtCombo As DataTable In dsCombo.Tables
                                        '_dsclscashmemoing.Tables.Add(dtCombo.Copy)
                                        dsCashMemoPrinting.Tables.Add(dtCombo.Copy)
                                    Next
                                End If

                                CloseConnection()
                                Return True
                            End If
                            tran.Rollback()
                            CloseConnection()
                            Return False
                        End If
                        tran.Rollback()
                        CloseConnection()
                        Return False
                    End If
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
            Else
                If dtGV Is Nothing Then
                    dtGV = GetVoucherStru()
                End If
                If PrepareData(DayOpenDate, Online, ds, sitecode, terminalid, userid, FYear, IsMemberShip:=IsMemberShip) Then
                    For i = 0 To ds.Tables("CASHMEMODTL").Rows.Count - 1
                        ds.Tables("CASHMEMODTL").Rows(i)("GrossAmt") = Math.Round(If(ds.Tables("CASHMEMODTL").Rows(i)("GrossAmt") Is DBNull.Value, 0, ds.Tables("CASHMEMODTL").Rows(i)("GrossAmt")), 2)
                        ''Line Commented By ketan remove round Off, Tax Round off Issue  date-19-July-2016
                        'ds.Tables("CASHMEMODTL").Rows(i)("TotalTaxAmount") = Math.Round(If(ds.Tables("CASHMEMODTL").Rows(i)("TotalTaxAmount") Is DBNull.Value, 0, ds.Tables("CASHMEMODTL").Rows(i)("TotalTaxAmount")), 2)
                        ds.Tables("CASHMEMODTL").Rows(i)("TotalTaxAmount") = If(ds.Tables("CASHMEMODTL").Rows(i)("TotalTaxAmount") Is DBNull.Value, 0, ds.Tables("CASHMEMODTL").Rows(i)("TotalTaxAmount"))
                        If ds.Tables.Contains("CASHMEMOTAXDTLS") = True Then
                            Dim j As Int16 = 1
                            For Each dr As DataRow In ds.Tables("CASHMEMOTAXDTLS").Select("BillLineNO='" & ds.Tables("CASHMEMODTL").Rows(i)("BillLineNO").ToString() & "'", "", DataViewRowState.CurrentRows)
                                ''Line Commented By ketan remove round Off, Tax Round off Issue  date-19-July-2016
                                ' dr("TaxValue") = Math.Round(dr("TaxValue"), 2)
                                dr("TaxValue") = dr("TaxValue")
                            Next
                        End If
                    Next

                    Dim SpectrumMettlerKotNo As String = ""
                    '---- Code Added By Mahesh In Case of Mettler Data is present 
                    If (ds.Tables.Contains("CashMemoMettler") AndAlso ds.Tables("CashMemoMettler").Rows.Count > 0) Then
                        Call prepareMettlerData(ds)
                    End If

                    If dtGV.Rows.Count > 0 Then
                        For i = 0 To ds.Tables("CASHMEMODTL").Rows.Count - 1
                            If ds.Tables("CASHMEMODTL").Rows(i)("ArticleCode").ToString() = "GVBaseArticle" Then
                                Dim number As Integer = ds.Tables("CASHMEMODTL").Rows(i)("BillLineNo").ToString()
                                ListOfQueue.Enqueue(number)
                            End If
                        Next
                        clsCommon._ListOfNumber = ListOfQueue
                    End If
                    '---added by sagar cash memo return with so
                    'If ds.Tables("CASHMEMODTL").Columns.Contains("LineNumber") Then
                    '    ds.Tables("CASHMEMODTL").Columns.Remove("LineNumber")
                    'End If

                    BillNo = ds.Tables("CASHMEMOHdr").Rows(0)("Billno").ToString()
                    OpenConnection()
                    tran = SpectrumCon.BeginTransaction()

                    'DeleteColumnFromDataTable(ds.Tables("CASHMEMODTL"), "BatchBarcode")
                    If SaveData(ds, SpectrumCon, tran) Then


                        '-------------
                        'added on 18 may ashma 
                        If InnovitiPaymentEnable = True AndAlso innovatiiForTerminals.Contains(terminalid) Then
                            Dim RetrievalReferenceNumber As String
                            Dim TransactionTime As String
                            If clsInnovitiList IsNot Nothing Then
                                Dim row = ds.Tables("CASHMEMORECEIPT").Select("TenderTypeCode='CreditCard'")
                                If row.Count = 1 Then
                                    For Each kvp As KeyValuePair(Of String, String) In clsInnovitiList
                                        If kvp.Key = "RetrievalReferenceNumber" Then
                                            RetrievalReferenceNumber = kvp.Value.ToUpper
                                        ElseIf kvp.Key = "TransactionTime" Then
                                            TransactionTime = kvp.Value.ToUpper
                                        End If
                                    Next
                                    If Not SaveInnovitiData(BillNo, terminalid, row(0)("AmountTendered"), sitecode, ds.Tables("CASHMEMOHdr").Rows(0)("Finyear").ToString(), userid, "CMS", RetrievalReferenceNumber, TransactionTime, tran) Then
                                        tran.Rollback()
                                        CloseConnection()
                                        Return False
                                    End If
                                Else
                                    If dtInnoviti.Rows.Count > 0 Then
                                        For Each kvp As KeyValuePair(Of String, String) In clsInnovitiList
                                            If kvp.Key = "RetrievalReferenceNumber" Then
                                                RetrievalReferenceNumber = kvp.Value.ToUpper
                                            ElseIf kvp.Key = "TransactionTime" Then
                                                TransactionTime = kvp.Value.ToUpper
                                            End If
                                        Next
                                        For Each drInnvoiti As DataRow In dtInnoviti.Rows
                                            '  Dim strAmountTender As String = drInnvoiti("AmountTendered")
                                            If Not SaveInnovitiData(BillNo, terminalid, drInnvoiti("AmountTendered"), sitecode, ds.Tables("CASHMEMOHdr").Rows(0)("Finyear").ToString(), userid, "CMS", RetrievalReferenceNumber, TransactionTime, tran) Then
                                                tran.Rollback()
                                                CloseConnection()
                                                Return False
                                            End If
                                        Next
                                    End If
                                End If
                            End If

                        End If
                        '--------------


                        If Not dtCreditSaleData Is Nothing AndAlso dtCreditSaleData.Rows.Count > 0 Then
                            If UpdateCreditSalesOnReturnItems(dtCreditSaleData, _SelectedCurrencyIndex, _iCurrencyCode, SpectrumCon, tran, terminalid, DayOpenDate, BillNo) = False Then
                                tran.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        End If
                        ''added by nikhil
                        ' ' 'Code Added by irfan on 17/1/2018 For Mantis issue 2756 CashMemoReturn
                        If Not dtCreditSaleData Is Nothing AndAlso dtCreditSaleData.Rows.Count > 0 Then
                            If updateCashMemoReceiptOnReturnItems(dtCreditSaleData, _SelectedCurrencyIndex, _iCurrencyCode, SpectrumCon, tran, terminalid, DayOpenDate, BillNo) = False Then
                                tran.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        End If
                        '==================================================================================================================================================
                        If IsHostManagement = True Then
                            If SaveReservationCashMemoHdr(ds.Tables("CASHMEMOHDR"), tran) = False Then
                                tran.Rollback()
                                CloseConnection()
                                Return False
                                '  Else
                                'tran.Commit()
                                'CloseConnection()
                                'Return True
                            End If
                        End If
                        If SalesType = "Dine In" Then
                            If DinInFlag = True AndAlso Not String.IsNullOrEmpty(CurrenDinInBillNo) Then
                                If UpdateDinInStatus(userid, ds.Tables("CashMemoHdr").Rows(0)("SiteCode").ToString(), CurrenDinInBillNo, SpectrumCon, tran) = False Then
                                    tran.Rollback()
                                    CloseConnection()
                                    Return False
                                End If
                                If Not String.IsNullOrEmpty(ReservationId) Then
                                    If UpdateCheckInStatus(userid, ds.Tables("CashMemoHdr").Rows(0)("SiteCode").ToString(), CurrentDineInNumber, ReservationId, SpectrumCon, tran) = False Then
                                        tran.Rollback()
                                        CloseConnection()
                                        Return False
                                    End If
                                End If
                            Else
                                If MergeId.ToString <> "0" Then
                                    If UpdateDinInAndMergeStatus(userid, ds.Tables("CashMemoHdr").Rows(0)("SiteCode").ToString(), MergeId, SpectrumCon, tran) = False Then
                                        tran.Rollback()
                                        CloseConnection()
                                        Return False
                                    End If
                                    If Not dsMergeOrderDtl Is Nothing AndAlso dsMergeOrderDtl.Tables.Count > 0 Then

                                        If dsMergeOrderDtl.Tables(1).Rows.Count > 0 AndAlso Not dsMergeOrderDtl.Tables(1) Is Nothing Then
                                            For i = 0 To dsMergeOrderDtl.Tables(1).Rows.Count - 1
                                                Dim ReservationId As String = dsMergeOrderDtl.Tables(1).Rows(i)("ReservationId")
                                                Dim CurrentDineInNo As String = dsMergeOrderDtl.Tables(1).Rows(i)("TableNumber")
                                                If Not String.IsNullOrEmpty(ReservationId) Then
                                                    If UpdateCheckInStatus(userid, ds.Tables("CashMemoHdr").Rows(0)("SiteCode").ToString(), CurrentDineInNo, ReservationId, SpectrumCon, tran) = False Then
                                                        ' tran.Rollback()
                                                        ' CloseConnection()
                                                        '  Return False
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If

                                End If
                            End If
                        End If

                        '----------For Saving Float Amt in VoucherEntry
                        Dim BillDate = DayOpenDate
                        dsVoucher = GetVoucherFloatData(BillNo, BillDate, tran)
                        'Dim DeliveryPerson As String = ds.Tables("CashMemoHdr").Rows(0)("DeliveryPersonID").ToString()
                        If FloatAmt <> 0 Then
                            If SaveFloatAmountData(FloatAmt, DeliveryPerson, BillNo, BillDate, sitecode, userid, FYear, tran, dsVoucher) = False Then
                                tran.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        End If
                        '-----------------
                        If Not dsCombo Is Nothing AndAlso dsCombo.Tables("CashMemoComboDtl").Rows.Count > 0 Then
                            If SaveCashMemoCombo(dsCombo, BillNo, SpectrumCon, tran) = False Then
                                tran.Rollback()
                                CloseConnection()
                                Return False
                            End If
                            If Not UpdateStockAtStoreLevel Then
                                For Each dr As DataRow In dsCombo.Tables("CashMemoComboDtl").Rows
                                    Dim QtyValue As Double = dr("QUANTITY")
                                    If Not IsDBNull(comboArticleCopy) Then
                                        Dim dr2() = comboArticleCopy.Select("ARTICLECODE='" & dr("ARTICLECODE") & "' AND BillLineNo='" & dr("BillLineNo") & "'")
                                        If dr2.Length > 0 Then
                                            Dim IndividualQty As Double = 0
                                            For index = 0 To dr2.Count - 1
                                                If dr2(index)("IndividualQty") > 0 Then
                                                    IndividualQty = IndividualQty + Val(dr2(index)("IndividualQty"))
                                                End If
                                            Next
                                            If Val(IndividualQty) > 0 Then
                                                QtyValue = IndividualQty
                                            End If
                                        End If
                                    End If
                                    If UpdateKitArticle(sitecode, dr("ARTICLECODE").ToString(), dr("EAN").ToString(), Nothing, QtyValue.ToString(), userid, SpectrumCon, tran, Storage, BillNo:=BillNo, updateArt:=True, SerVerDate:=ServerDate, Fyear:=FYear) = False Then
                                        tran.Rollback()
                                        CloseConnection()
                                        Return False
                                    End If
                                Next
                            End If
                        End If
                        If Not UpdateStockAtStoreLevel Then
                            For Each dr As DataRow In ds.Tables("CASHMEMODTL").Rows
                                If UpdateKitArticle(sitecode, dr("ARTICLECODE").ToString(), dr("EAN").ToString(), dr("UNITOFMEASURE").ToString(), dr("QUANTITY").ToString(), userid, SpectrumCon, tran, Storage, BillNo:=BillNo, updateArt:=True, SerVerDate:=ServerDate, Fyear:=FYear) = False Then
                                    tran.Rollback()
                                    CloseConnection()
                                    Return False
                                End If
                            Next
                        End If
                        'For Each dr As DataRow In ds.Tables("CASHMEMODTL").Rows
                        '    If UpdateStock(sitecode, dr("ARTICLECODE").ToString(), dr("EAN").ToString(), dr("UNITOFMEASURE").ToString(), dr("QUANTITY").ToString(), userid, SpectrumCon, tran, Storage, BillNo:=BillNo, updateArt:=True, SerVerDate:=ServerDate, Fyear:=FYear) = False Then
                        '        tran.Rollback()
                        '        CloseConnection()
                        '        Return False
                        '    End If
                        'Next
                        'For Each dr As DataRow In ds.Tables("CASHMEMODTL").Rows
                        '    If ds.Tables("CASHMEMODTL").Columns.Contains("BatchBarcode") Then
                        '        If IsDBNull(dr("BatchBarcode")) = False AndAlso String.IsNullOrEmpty(dr("BatchBarcode")) = False Then
                        '            If UpdateBatchDtlQtyAllocated(sitecode, dr("BatchBarcode").ToString(), dr("QUANTITY").ToString(), tran) = False Then
                        '                tran.Rollback()
                        '                CloseConnection()
                        '                Return False
                        '            End If
                        '        End If
                        '    End If

                        'Next

                        'code move by vipul from after print and added in save cash memo  transaction 
                        If UpdateStockAtStoreLevel Then
                            If Not UpdateStockForArticle(sitecode, userid, Storage, BillNo, FYear, ds, dsCombo, tran, SpectrumCon) Then
                                tran.Rollback()
                                CloseConnection()
                                Return False
                                ActivityLogForShift(Nothing, "Update Stock Fail", "")
                            End If
                        End If
                        If SaveTenders(ds, tran, DayOpenDate, sitecode, terminalid, userid, BillNo, DocType, dtGV, CLPArticle, CLPCustomerId, ClpProgramId, GVProgramId, CVProgramId, VoucherDays, CLPRed, CLPRedemptionPoints) = False Then
                            Return False
                        End If
                        For Each drCLP As DataRow In ds.Tables("CashMemoDtl").Select("Btype='S' And  ArticleCode='" & CLPArticle & "'", "", DataViewRowState.CurrentRows)
                            Dim TotalPoints As Integer = drCLP("Quantity")
                            If UpdateClpPoints(True, ClpProgramId, CLPCustomerId, TotalPoints, SpectrumCon, tran) = False Then
                                tran.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        Next
                        If UpdateDocumentNo("CM", SpectrumCon, tran) = False Then
                            tran.Rollback()
                            CloseConnection()
                            Return False
                        End If

                        'added by sagar for innovatii
                        'modified by khusrao adil on 27-04-2017
                        'innovatiiForTerminals
                        'modified by khusrao adil on 27-07-2018
                        If InnovitiPaymentEnable = True AndAlso innovatiiForTerminals.Contains(terminalid) Then
                            If dtInnoviti.Rows.Count > 0 Then
                                For Each dr In dtInnoviti.Rows
                                    If UpdateDocumentNo("TransInnoviti", SpectrumCon, tran) = False Then
                                        tran.Rollback()
                                        CloseConnection()
                                        Return False
                                    End If
                                Next
                            End If
                        End If

                        If Not dtReturn Is Nothing AndAlso dtReturn.Rows.Count > 0 Then
                            For Each dr As DataRow In dtReturn.Select("ReturnDocumentType='CM'", "", DataViewRowState.CurrentRows)
                                If UpdateOldCashMemo(userid, dr("RETURNCMNO").ToString(), sitecode, dr("EAN").ToString(), dr("Quantity").ToString(), dr("SALESRETURNREASON").ToString(), BillNo, ServerDate, SpectrumCon, tran, _IsBillLineNo) = False Then
                                    tran.Rollback()
                                    CloseConnection()
                                    Return False
                                End If
                            Next
                            For Each dr As DataRow In dtReturn.Select("ReturnDocumentType='SO'", "", DataViewRowState.CurrentRows)
                                If UpdateSO(userid, dr("RETURNCMNO").ToString(), sitecode, dr("EAN").ToString(), dr("Quantity").ToString(), dr("SALESRETURNREASON").ToString(), BillNo, ServerDate, SpectrumCon, tran) = False Then
                                    tran.Rollback()
                                    CloseConnection()
                                    Return False
                                End If
                            Next
                            For Each dr As DataRow In dtReturn.Select("ReturnDocumentType='BL'", "", DataViewRowState.CurrentRows)
                                If UpdateBL(userid, dr("RETURNCMNO").ToString(), sitecode, dr("EAN").ToString(), dr("Quantity").ToString(), dr("SALESRETURNREASON").ToString(), BillNo, ServerDate, SpectrumCon, tran) = False Then
                                    tran.Rollback()
                                    CloseConnection()
                                    Return False
                                End If
                            Next
                            If CLPCustomerId = String.Empty Then
                                For Each dr As DataRow In dtReturn.Select("isnull(ClpPoints,0)<>0", "", DataViewRowState.CurrentRows)
                                    Dim TotalPoints As Integer = dr("ClpPoints")
                                    If UpdateClpPoints(True, ClpProgramId, dr("Section").ToString(), TotalPoints, SpectrumCon, tran) = False Then
                                        tran.Rollback()
                                        CloseConnection()
                                        Return False
                                    End If
                                Next
                            End If
                            dtReturn.Clear()
                            _IsBillLineNo = 0
                        End If
                        If GVBasedAricleReturnList IsNot Nothing Then
                            If GiftVoucherReturnAllowed Then
                                For Each kvp As KeyValuePair(Of String, String) In GVBasedAricleReturnList
                                    If Not DeactivateVoucher(SpectrumCon, tran, sitecode, kvp.Key, userid, "CMS", kvp.Value) Then
                                        tran.Rollback()
                                        CloseConnection()
                                        Return False
                                    End If
                                Next
                            End If
                        End If
                        If AssignVoucherToBill(SpectrumCon, tran, sitecode, BillNo, FYear, "CMS") = False Then
                            tran.Rollback()
                            CloseConnection()
                            Return False
                        End If
                        If UpdateBillTime = True Then
                            If GetUpdateBillTime(BillNo, "CM", tran) Then
                            End If
                        End If
                        tran.Commit()

                        'Rakesh:09-July-2013-->Start: Template based cashmemo bill printing parameter
                        '_dsCashMemoPrinting = ds
                        dsCashMemoPrinting = ds

                        If dsCombo IsNot Nothing Then
                            For Each dtCombo As DataTable In dsCombo.Tables
                                '_dsCashMemoPrinting.Tables.Add(dtCombo.Copy)
                                dsCashMemoPrinting.Tables.Add(dtCombo.Copy)
                            Next
                        End If


                        CloseConnection()

                        Return True

                    Else
                        tran.Rollback()
                        CloseConnection()
                        Return False
                    End If
                Else
                    Return False
                End If

            End If
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            'If ex.Message.Contains("Concurrency") Then
            '    GoTo SaveCashMemodata
            'End If
            msgError = ex.Message
            LogException(ex)
            Return False
        End Try
    End Function
    '' added by nikhil
    Private Function SaveReservationCashMemoHdr(ByVal dt As DataTable, ByRef tran As SqlTransaction)
        Try
            Dim dtreservarionid As DataTable
            dtreservarionid = GetReserveId(dt, tran)
            SaveReservationCashMemoHdr = False
            If Not dtreservarionid Is Nothing Then
                Dim remark As String
                remark = ""
                vStmtQry.Length = 0
                vStmtQry.Append("insert into Host_ReservationCashMemoHdrMap (reservationCashMemoHdrMapID,reservationID ,SiteCode ,FinYear ,BillNo ,remarks ,createdAt ,createdBy ,createdOn ,updatedAt ,updatedBy ,updatedOn ,mstStatusID )" & vbCrLf)
                vStmtQry.Append("values('" & dtreservarionid.Rows(0)("reservationGuestDetailID") & "','" & dtreservarionid.Rows(0)("reservationID") & "','" & dt.Rows(0)("SITECODE") & "','" & dt.Rows(0)("FINYEAR") & "'," & vbCrLf)
                vStmtQry.Append("'" & dt.Rows(0)("BILLNO") & "','" & remark & "','" & dt.Rows(0)("CREATEDAT") & "','" & dt.Rows(0)("CREATEDBY") & "',GetDate(),'" & dt.Rows(0)("UPDATEDAT") & "','" & dt.Rows(0)("UPDATEDBY") & "',getDate(),1)" & vbCrLf)
                Dim cmdTrn As New SqlCommand(vStmtQry.ToString(), SpectrumCon)

                cmdTrn.Transaction = tran
                OpenConnection()
                If cmdTrn.ExecuteNonQuery() > 0 Then
                    SaveReservationCashMemoHdr = True
                Else
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    'Code is added by irfan on 6/4/2018 for hotel reservation
    Public Function GetReserveId(ByVal dt As DataTable, ByRef tran As SqlTransaction) As DataTable
        Try
            Dim strQuery As String
            strQuery = "select distinct hrs.reservationGuestDetailID,hrs.reservationID from Host_ReservationGuestDetail hrs inner join clpcustomers cls  on hrs.guestMobileNumber=cls.Mobileno where hrs.CardNo='" & dt.Rows(0)("CLPNo") & "'  and cls.STATUS=1"
            Dim dtTemp As New DataTable
            Dim da As New SqlDataAdapter(strQuery, SpectrumCon)
            da.SelectCommand.Transaction = tran
            da.Fill(dtTemp)
            If dtTemp.Rows.Count > 0 Then
                Return dtTemp
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function SaveMettlerKotDtl(ByVal DsTemp As DataSet, ByVal MaxNo As String) As Boolean
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If SaveData(DsTemp, SpectrumCon, tran) = False And UpdateDocumentNo("SpectrumMattler", SpectrumCon, tran, MaxNo, "FO_DOC") = False Then
                tran.Rollback()
                CloseConnection()
                Exit Try
            End If
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function UpdateStockForArticle(ByVal Sitecode As String, ByVal userid As String, ByVal Storage As String, ByRef BillNo As String, ByVal FYear As String, ByRef ds As DataSet, ByRef dsCombo As DataSet, ByRef tran As SqlTransaction, ByRef con As SqlConnection) As Boolean
        Try
            'ServerDate = GetCurrentDate()
            'code commented by vipul for keeping code save logic in single tarnsaction
            ' Dim tran As SqlTransaction = Nothing
            ServerDate = GetSQLServerDateTime(con, tran)
            BillNo = ds.Tables("CASHMEMOHdr").Rows(0)("Billno").ToString()
            '  OpenConnection()
            ' tran = SpectrumCon.BeginTransaction()

            If Not dsCombo Is Nothing AndAlso dsCombo.Tables("CashMemoComboDtl").Rows.Count > 0 Then
                For Each dr As DataRow In dsCombo.Tables("CashMemoComboDtl").Rows
                    Dim QtyValue As Double = dr("QUANTITY")
                    If Not IsDBNull(comboArticleCopy) Then
                        Dim dr2() = comboArticleCopy.Select("ARTICLECODE='" & dr("ARTICLECODE") & "' AND BillLineNo='" & dr("BillLineNo") & "'")
                        If dr2.Length > 0 Then
                            Dim IndividualQty As Double = 0
                            For index = 0 To dr2.Count - 1
                                If dr2(index)("IndividualQty") > 0 Then
                                    IndividualQty = IndividualQty + Val(dr2(index)("IndividualQty"))
                                End If
                            Next
                            If Val(IndividualQty) > 0 Then
                                QtyValue = IndividualQty
                            End If
                        End If
                    End If
                    If UpdateStock(Sitecode, dr("ARTICLECODE").ToString(), dr("EAN").ToString(), Nothing, QtyValue.ToString(), userid, SpectrumCon, tran, Storage, BillNo:=BillNo, updateArt:=True, SerVerDate:=ServerDate, Fyear:=FYear) = False Then
                        tran.Rollback()
                        CloseConnection()
                        Return False
                    End If
                Next
            End If
            For Each dr As DataRow In ds.Tables("CASHMEMODTL").Rows
                If UpdateStock(Sitecode, dr("ARTICLECODE").ToString(), dr("EAN").ToString(), dr("UNITOFMEASURE").ToString(), dr("QUANTITY").ToString(), userid, SpectrumCon, tran, Storage, BillNo:=BillNo, updateArt:=True, SerVerDate:=ServerDate, Fyear:=FYear) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
            Next
            For Each dr As DataRow In ds.Tables("CASHMEMODTL").Rows
                If ds.Tables("CASHMEMODTL").Columns.Contains("BatchBarcode") Then
                    If IsDBNull(dr("BatchBarcode")) = False AndAlso String.IsNullOrEmpty(dr("BatchBarcode")) = False Then
                        If UpdateBatchDtlQtyAllocated(Sitecode, dr("BatchBarcode").ToString(), dr("QUANTITY").ToString(), tran) = False Then
                            tran.Rollback()
                            CloseConnection()
                            Return False
                        End If
                    End If
                End If

            Next
            'code commented by vipul 
            ' tran.Commit()
            ' CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function
    Public Shared Function GetSpectrumMettlerDtlStru() As DataTable
        Try
            Dim strQuery As String
            strQuery = "select * from SpectrumMettlerDtl where 1=0"
            Dim dtTemp As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dtTemp)
            Return dtTemp
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Private Sub prepareMettlerData(ByRef dsTemp As DataSet)
        Try
            For index = 0 To dsTemp.Tables("CashMemoMettler").Rows.Count - 1
                dsTemp.Tables("CashMemoMettler").Rows(index)("BillNo") = dsTemp.Tables("CashMemoHdr").Rows(0)("BillNo")
                dsTemp.Tables("CashMemoMettler").Rows(index)("CREATEDAT") = dsTemp.Tables("CashMemoHdr").Rows(0)("CREATEDAT")
                dsTemp.Tables("CashMemoMettler").Rows(index)("CREATEDBY") = dsTemp.Tables("CashMemoHdr").Rows(0)("CREATEDBY")
                dsTemp.Tables("CashMemoMettler").Rows(index)("CREATEDON") = dsTemp.Tables("CashMemoHdr").Rows(0)("CREATEDON")
                dsTemp.Tables("CashMemoMettler").Rows(index)("UPDATEDBY") = dsTemp.Tables("CashMemoHdr").Rows(0)("UPDATEDBY")
                dsTemp.Tables("CashMemoMettler").Rows(index)("UPDATEDAT") = dsTemp.Tables("CashMemoHdr").Rows(0)("UPDATEDAT")
                dsTemp.Tables("CashMemoMettler").Rows(index)("UPDATEDON") = dsTemp.Tables("CashMemoHdr").Rows(0)("UPDATEDON")
                dsTemp.Tables("CashMemoMettler").Rows(index)("STATUS") = dsTemp.Tables("CashMemoHdr").Rows(0)("STATUS")
            Next
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    Private Sub prepareMettlerHoldData(ByRef dsTemp As DataSet)
        Try
            For index = 0 To dsTemp.Tables("HoldCashMemoMettler").Rows.Count - 1
                dsTemp.Tables("HoldCashMemoMettler").Rows(index)("BillNo") = dsTemp.Tables("HOLDCASHMEMOHDR").Rows(0)("BillNo")
                dsTemp.Tables("HoldCashMemoMettler").Rows(index)("CREATEDAT") = dsTemp.Tables("HOLDCASHMEMOHDR").Rows(0)("CREATEDAT")
                dsTemp.Tables("HoldCashMemoMettler").Rows(index)("CREATEDBY") = dsTemp.Tables("HOLDCASHMEMOHDR").Rows(0)("CREATEDBY")
                dsTemp.Tables("HoldCashMemoMettler").Rows(index)("CREATEDON") = dsTemp.Tables("HOLDCASHMEMOHDR").Rows(0)("CREATEDON")
                dsTemp.Tables("HoldCashMemoMettler").Rows(index)("UPDATEDBY") = dsTemp.Tables("HOLDCASHMEMOHDR").Rows(0)("UPDATEDBY")
                dsTemp.Tables("HoldCashMemoMettler").Rows(index)("UPDATEDAT") = dsTemp.Tables("HOLDCASHMEMOHDR").Rows(0)("UPDATEDAT")
                dsTemp.Tables("HoldCashMemoMettler").Rows(index)("UPDATEDON") = dsTemp.Tables("HOLDCASHMEMOHDR").Rows(0)("UPDATEDON")
                dsTemp.Tables("HoldCashMemoMettler").Rows(index)("STATUS") = dsTemp.Tables("HOLDCASHMEMODTL").Rows(0)("STATUS")
            Next
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub



    Public Function SaveCashMemoCombo(ByVal dataSet As DataSet, ByVal BillNo As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Boolean
        Try
            Dim strSelect As String
            For Each dataRow In dataSet.Tables("CashMemoComboDtl").Rows
                dataRow("CREATEDON") = ServerDate
                dataRow("UPDATEDON") = ServerDate
                dataRow("Billno") = BillNo
            Next
            strSelect = "select * from CashMemoComboDtl where 1=0"
            Dim dasave As New SqlDataAdapter(strSelect, con)
            dasave.SelectCommand.CommandTimeout = 0
            dasave.SelectCommand.Transaction = tran
            Dim cb As SqlCommandBuilder
            cb = New SqlCommandBuilder(dasave)
            dasave = cb.DataAdapter
            dasave.Update(dataSet, "CashMemoComboDtl")
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function SaveTenders(ByRef ds As DataSet, ByRef tran As SqlTransaction, ByVal DayOpenDate As DateTime, ByVal sitecode As String, ByVal terminalid As String, ByVal userid As String, ByRef BillNo As String, _
             ByVal DocType As String, Optional ByRef dtGV As DataTable = Nothing, Optional ByVal CLPArticle As String = "", Optional ByVal CLPCustomerId As String = "", Optional ByVal ClpProgramId As String = "", _
             Optional ByVal GVProgramId As String = "", Optional ByVal CVProgramId As String = "", Optional ByVal VoucherDays As Int32 = 0, Optional ByVal CLPRed As Boolean = False, Optional ByRef CLPRedemptionPoints As Double = 0) As Boolean
        Try


            Dim dvCreditVoucher As New DataView(ds.Tables("CashMemoReceipt"), "TenderTypeCode Like 'CreditVouc%'", "", DataViewRowState.CurrentRows)
            If dvCreditVoucher.Count > 0 Then
                For Each drView As DataRowView In dvCreditVoucher
                    If drView("TenderTypeCode") = "CreditVouc(I)" Then
                        ' '' Issue CV against partial redeemation should have expirydate same as orignal CV
                        ''If dvCreditVoucher.Count > 1 Then
                        ''    Dim dvRedimCV As New DataView(ds.Tables("CashMemoReceipt"), "TenderTypeCode = 'CreditVouc(R)'", "", DataViewRowState.CurrentRows)
                        ''    If dvRedimCV.Count > 0 Then
                        ''        If Not IsDBNull(dvRedimCV(0).Item("RefDate")) Then
                        ''            VoucherDays = DateDiff(DateInterval.Day, ServerDate, dvRedimCV(0).Item("RefDate"))
                        ''            VoucherDays = VoucherDays + 1
                        ''        End If
                        ''    End If
                        ''End If
                        ' '' Issue CV against partial redeemation should have expirydate same as orignal CV

                        If Not IsDBNull(drView("RefDate")) Then
                            VoucherDays = DateDiff(DateInterval.Day, ServerDate.Date, CDate(drView("RefDate")).Date)
                            VoucherDays = VoucherDays
                        End If


                        If UpdateCreditVoucher(CVProgramId, DocType, True, drView("BillNo").ToString(), sitecode, DayOpenDate, userid, tran, SpectrumCon, drView("AmountTendered"), "0", VoucherDays) = False Then
                            tran.Rollback()
                            CloseConnection()
                            Return False
                        End If
                    ElseIf drView("TenderTypeCode") = "CreditVouc(R)" Then
                        If UpdateCreditVoucher(CVProgramId, DocType, False, drView("BillNo").ToString(), sitecode, ServerDate, userid, tran, SpectrumCon, 0, drView("CARDNO").ToString()) = False Then
                            tran.Rollback()
                            CloseConnection()
                            Return False
                        End If
                    End If
                Next
            End If
            dvCreditVoucher.RowFilter = "TenderTypeCode Like 'GiftVouc%'"
            If dvCreditVoucher.Count > 0 Then
                For Each GVRow As DataRowView In dvCreditVoucher
                    If GVRow("TenderTypeCode") = "GiftVoucher(I)" Then
                        Dim drGV As DataRow = dtGV.NewRow()
                        drGV("SiteCode") = sitecode
                        'drGV("VoucherCode") = GVRow("CARDNO").ToString()
                        drGV("VoucherCode") = GVRow("RefNo_3").ToString()
                        drGV("ValueOfVoucher") = CDbl(IIf(GVRow("AmountTendered") < 0, GVRow("AmountTendered") * -1, GVRow("AmountTendered").ToString()))
                        drGV("IsActive") = 1
                        drGV("IsIssued") = 1
                        drGV("IssuedAtSite") = sitecode
                        'drGV("IssuedOnDate") = ServerDate
                        drGV("IssuedOnDate") = DayOpenDate
                        drGV("IssuedInDocType") = DocType
                        drGV("Quantity") = 1
                        drGV("NetAmount") = CDbl(IIf(GVRow("AmountTendered") < 0, GVRow("AmountTendered") * -1, GVRow("AmountTendered").ToString()))
                        drGV("ExpiryDate") = GVRow("RefDate")

                        ' '' Issue GV against partial redeemation should have expirydate same as orignal GV
                        ''If dvCreditVoucher.Count > 1 Then
                        ''    Dim dvRedimGV As New DataView(ds.Tables("CashMemoReceipt"), "TenderTypeCode = 'GiftVoucher(R)'", "", DataViewRowState.CurrentRows)
                        ''    If dvRedimGV.Count > 0 Then
                        ''        If Not IsDBNull(dvRedimGV(0).Item("RefDate")) Then
                        ''            drGV("ExpiryDate") = dvRedimGV(0).Item("RefDate")
                        ''        End If
                        ''    End If
                        ''End If
                        ' '' Issue GV against partial redeemation should have expirydate same as orignal GV

                        dtGV.Rows.Add(drGV)

                        'If UpdateCreditVoucher(GVRow("BillNo").ToString(), DocType, True, GVRow("BillNo").ToString(), sitecode, ServerDate, userid, tran, SpectrumCon, GVRow("AmountTendered"), "0", VoucherDays) = False Then
                        '    tran.Rollback()
                        '    CloseConnection()
                        'End If
                    ElseIf GVRow("TenderTypeCode") = "GiftVoucher(R)" Then
                        If UpdateCreditVoucher(GVProgramId, DocType, False, GVRow("BillNo").ToString(), sitecode, ServerDate, userid, tran, SpectrumCon, 0, GVRow("CARDNO").ToString()) = False Then
                            tran.Rollback()
                            CloseConnection()
                            Return False
                        End If
                    End If
                Next
            End If
            dvCreditVoucher.RowFilter = "TenderTypeCode Like 'CLP%'"
            If dvCreditVoucher.Count > 0 Then
                If Not ds.Tables.Contains("CASHMEMORECEIPTAUDIT") Then

                    If Not getclpsettings.Tables("CLPHeader") Is Nothing Then
                        If CLP_Data.CLPConfigdata.Tables("CLPHeader").Rows(0)("RedemptionType") = "Rdt3" OrElse CLP_Data.CLPConfigdata.Tables("CLPHeader").Rows(0)("RedemptionType") = "Rdt2" OrElse CLP_Data.CLPConfigdata.Tables("CLPHeader").Rows(0)("RedemptionType") = "Rdt1" Then

                            If CLP_Data.RedPoint = 0 Then
                                CLPRedemptionPoints = dvCreditVoucher.ToTable().Compute("Sum(AmountTendered)", "")
                            Else
                                CLPRedemptionPoints = CLP_Data.RedPoint
                            End If

                        End If
                        If IIf(ds.Tables("CashMemohdr")(0)("CLPPoints") Is DBNull.Value, 0, ds.Tables("CashMemohdr")(0)("CLPPoints")) > 0 Then
                            If UpdateClpPoints(False, ClpProgramId, CLPCustomerId, CLPRedemptionPoints, SpectrumCon, tran, sitecode, userid, BillNo, DayOpenDate, False, ds.Tables("Cashmemohdr")(0)("finyear")) = False Then
                                tran.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        Else
                            If UpdateClpPoints(False, ClpProgramId, CLPCustomerId, CLPRedemptionPoints, SpectrumCon, tran, sitecode, userid, BillNo, DayOpenDate, True, ds.Tables("Cashmemohdr")(0)("finyear")) = False Then
                                tran.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        End If

                        CLP_Data.RedeemPassKey(dvCreditVoucher(0)("RefNo_4").ToString(), dvCreditVoucher(0)("BILLNO"), dvCreditVoucher(0)("CMRCPTDATE"), CLPRedemptionPoints, tran, ClpProgramId, CLPCustomerId)
                    Else
                        For Each CLpRow As DataRowView In dvCreditVoucher
                            Dim TotalPoints As Integer = CLpRow("AmountTendered")
                            If CLPRedemptionPoints = 0 Then
                                CLPRedemptionPoints = TotalPoints
                            End If
                            'TotalPoints = TotalPoints * -1
                            If UpdateClpPoints(False, ClpProgramId, CLPCustomerId, CLPRedemptionPoints, SpectrumCon, tran, sitecode, userid, BillNo, ServerDate, True) = False Then
                                tran.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        Next
                    End If
                End If
            End If
            If Not dtGV Is Nothing AndAlso dtGV.Rows.Count > 0 Then
                If GenerateGiftVoucher(dtGV, tran, BillNo, sitecode, userid, ServerDate, DayOpenDate) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    'Public Function SaveClpData(ByVal ds As DataSet, ByVal CLPProgramId As String, ByVal CLpCustomerId As String, ByVal TotalPoints As Double, Optional ByVal RedemptionValue As Double = 0) As Boolean
    '    Try
    '        Dim dvCLP As New DataView(ds.Tables("CashMemoDtl"), "CLPRequire=TRUE", "", DataViewRowState.CurrentRows)
    '        If dvCLP.Count > 0 Then
    '            Dim dtDtl, dtHdr As DataTable
    '            dtDtl = dvCLP.ToTable(True, "SiteCode", "BillNo", "BillLineNo", "ArticleCode", "EAN", "SellingPrice", "Quantity", "CLPDiscount", "CLPPoints", "CREATEDAT", "CREATEDBY", "CREATEDON", "UPDATEDAT", "UPDATEDBY", "UPDATEDON", "STATUS")
    '            dvCLP = New DataView(ds.Tables("CashMemoHdr"), "", "", DataViewRowState.CurrentRows)
    '            dtHdr = dvCLP.ToTable(True, "SiteCode", "BillNo", "BillDate", "CREATEDAT", "CREATEDBY", "CREATEDON", "UPDATEDAT", "UPDATEDBY", "UPDATEDON", "STATUS")
    '            AddColumnToDataTable(dtHdr, "AccumLationPoints", "System.Double", TotalPoints)
    '            AddColumnToDataTable(dtHdr, "RedemptionPoints", "System.Double", RedemptionValue)
    '            AddColumnToDataTable(dtHdr, "BalAccumlationPoints", "System.Double", TotalPoints)
    '            AddColumnToDataTable(dtHdr, "ClpProgramId", "System.String", CLPProgramId)
    '            AddColumnToDataTable(dtHdr, "ClpCustomerId", "System.String", CLpCustomerId)
    '            AddColumnToDataTable(dtHdr, "IsRedemptionProcess", "System.Boolean", False)
    '            Dim dsClp As New DataSet
    '            dtDtl.TableName = "CLPTransactionsDetails"
    '            dtHdr.TableName = "CLPTransaction"
    '            dsClp.Tables.Add(dtDtl)
    '            dsClp.Tables.Add(dtHdr)
    '            AddMode(dsClp)
    '            Dim tran As SqlTransaction = Nothing
    '            OpenConnection()
    '            tran = SpectrumCon.BeginTransaction()
    '            If SaveData(dsClp, SpectrumCon, tran) Then
    '                If CLpCustomerId = String.Empty Then
    '                    tran.Commit()
    '                    CloseConnection()
    '                    Return True
    '                End If
    '                If UpdateClpPoints(True, CLPProgramId, CLpCustomerId, TotalPoints, SpectrumCon, tran) = True Then
    '                    tran.Commit()
    '                    CloseConnection()
    '                    Return True
    '                End If
    '                tran.Rollback()
    '                CloseConnection()
    '                Return False
    '            End If
    '            tran.Rollback()
    '            CloseConnection()
    '            Return False
    '        Else
    '            Return True
    '        End If
    '    Catch ex As Exception
    '        LogException(ex)
    '        Return False
    '    End Try
    'End Function
    ''' <summary>
    ''' Hold Cash Memo Data
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="sitecode">SiteCode</param>
    ''' <param name="TerminalId">TerminalId</param>
    ''' <param name="Userid">UserId</param>
    ''' <param name="HoldKey">Resume/Hold</param>
    ''' <param name="BillNO">Cash Memo No</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Function HoldData(ByVal DayOpenDate As DateTime, ByVal ds As DataSet, ByVal sitecode As String, ByVal TerminalId As String, ByVal Userid As String, ByVal HoldKey As String, ByRef BillNO As String, ByVal salesPerson As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            ServerDate = GetCurrentDate()
            Dim dr As DataRow
            'Dim strTrn As String = getTrnno(sitecode, TerminalId, Now)
            Dim DocNo As String = getDocumentNo("CMHold", sitecode)

            'Changed by Rohit to generate Document No. for proper sorting
            Try
                Dim strFinyear As String = GetFinancialYear(DayOpenDate, sitecode)
                'BillNO = GenDocNo("CM" & TerminalId & strFinyear.Substring(strFinyear.Length - 2, 2), 15, DocNo)
                ' BillNO = GenDocNo("CM" & TerminalId & strFinyear.Substring(strFinyear.Length - 2, 2), 15, DocNo)              
                ''GST changes by ketan add sitecode in billno 
                BillNO = GenDocNo("C" & TerminalId.Substring(TerminalId.Trim.Length - 2, 2) & sitecode.Substring(sitecode.Trim.Length - 3, 3) & strFinyear.Substring(strFinyear.Length - 2, 2), 15, DocNo)

            Catch ex As Exception
                BillNO = "CM" & TerminalId & DocNo
            End Try
            'End Change by Rohit


            DocNo = BillNO
            dr = ds.Tables("HOLDCASHMEMOHDR").Rows(0)
            dr.BeginEdit()
            dr("SITECODE") = sitecode
            dr("TerminalID") = TerminalId
            'dr("TRNTYPECODE") = 100
            dr("RETRIEVEDFROMCUSTNAME") = HoldKey
            dr("BILLNO") = DocNo
            dr("BillDate") = DayOpenDate.Date
            dr("BillTime") = ServerDate
            dr("Createdat") = sitecode
            dr("Createdby") = Userid
            dr("Createdon") = ServerDate.Date
            dr("updatedat") = sitecode
            dr("updatedby") = Userid
            dr("updatedon") = ServerDate.Date
            dr("BILLINTERMEDIATESTATUS") = "Hold"
            dr("SalesExecutiveCode") = salesPerson
            dr.EndEdit()
            'AddColumnToDataTable(ds.Tables("HOLDCASHMEMOHDR"), "BILLINTERMEDIATESTATUS", "System.String", "Hold")
            SetDetailData(ds, sitecode, "HOLDCASHMEMODTL", DocNo, Userid)
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMOHDR"), "DeletionReason")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMOHDR"), "HDDeliveryDate")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMOHDR"), "HDName")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMOHDR"), "HDAddress")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMOHDR"), "HDEmail")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMOHDR"), "HDTelNo")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMOHDR"), "HDRemark")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMOHDR"), "FINYEAR")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMOHDR"), "CLPPOINTS")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMOHDR"), "Quarter")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMOHDR"), "Month")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMOHDR"), "Day")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMOHDR"), "Remark")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMOHDR"), "DeliveryPartnerId")
            'DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMOHDR"), "CustomerNo")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMODTL"), "FIRSTLEVEL")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMODTL"), "TOPLEVEL")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMODTL"), "FIRSTLEVELDISC")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMODTL"), "TOPLEVELDISC")

            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMODTL"), "FINYEAR")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMODTL"), "PROMOTIONID")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMOHDR"), "AuthUserID")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMOHDR"), "AuthUserRemarks")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMODTL"), "AuthUserID")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMODTL"), "AuthUserRemarks")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMODTL"), "BILLDATE")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMODTL"), "BILLTIME")
            'changed by ram dt 24.05.2009 action: Add

            'Commented bby Rohit on 21/04/2011

            'DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMODTL"), "CLPRequire")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMODTL"), "CLPPoints")
            If ds.Tables("HOLDCASHMEMODTL").Columns.Contains("MRP") Then
                DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMODTL"), "MRP")
            End If

            If ds.Tables("HOLDCASHMEMODTL").Columns.Contains("TakeAwayQuantity") Then
                DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMODTL"), "TakeAwayQuantity")
            End If

            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMODTL"), "ReturnDocumentType")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMODTL"), "Quarter")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMODTL"), "Day")
            DeleteColumnFromDataTable(ds.Tables("HOLDCASHMEMODTL"), "Month")

            'changed by ram dt 24.05.2009 action: Add
            AddColumnToDataTable(ds.Tables("HOLDCASHMEMODTL"), "BILLDATE", "System.DateTime", DayOpenDate)
            AddColumnToDataTable(ds.Tables("HOLDCASHMEMODTL"), "BILLTIME", "System.DateTime", ServerDate)

            If ds.Tables.Contains("HOLDVOUCHER") Then
                AddColumnToDataTable(ds.Tables("HOLDVOUCHER"), "BILLLineNo", "System.String", "0")
                Dim i As Integer = 0
                For Each drVoucher As DataRow In ds.Tables("HOLDVOUCHER").Rows
                    drVoucher("BillLineNo") = i
                    drVoucher("ISSUEDDOCNUMBER") = BillNO
                    i = i + 1
                Next
            End If
            If (ds.Tables.Contains("HoldCashMemoMettler") AndAlso ds.Tables("HoldCashMemoMettler").Rows.Count > 0) Then
                Call prepareMettlerHoldData(ds)
            End If
            AddMode(ds)
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If SaveData(ds, SpectrumCon, tran) Then
                If UpdateDocumentNo("CMHold", SpectrumCon, tran) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
                tran.Commit()
                CloseConnection()
                Return True
            End If
            tran.Rollback()
            CloseConnection()
            Return False
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try
    End Function

    '--------

    Public Function SaveDinInData(ByVal DayOpenDate As Date, ByVal ds As DataSet, ByVal dsMainDineInHold As DataSet, DtDineInItemRemarks As DataTable, ByVal sitecode As String, ByVal finyear As String, ByVal currentDineInTable As String, ByVal TerminalId As String, ByVal Userid As String, ByVal HoldKey As String, ByRef BillNO As String, ByVal IsNewOrder As Boolean, ByVal salesPerson As String, CustomerSaleType As Int16, Optional dtGenerate As DataTable = Nothing, Optional ByVal BillGenerate As Integer = 0, Optional ByVal custno As String = "", Optional ByVal IsDineIn As Boolean = False) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            Dim MaxBilllineno As Integer = 0
            ServerDate = GetCurrentDate()
            Dim DocNo As String = getDocumentNo("DineIn", sitecode)
            If String.IsNullOrEmpty(BillNO) Then
                Try
                    Dim strFinyear As String = GetFinancialYear(DayOpenDate, sitecode)
                    BillNO = GenDocNo("DI" & TerminalId & strFinyear.Substring(strFinyear.Length - 2, 2), 15, DocNo)
                Catch ex As Exception
                    BillNO = "DI" & TerminalId & DocNo
                End Try
            End If

            CurrenDinInBillNo = BillNO
            Dim objComm As New clsCommon
            Dim cmd As New System.Text.StringBuilder()
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If Not dsMainDineInHold.Tables("CASHMEMODTL") Is Nothing Then

                Dim deletedEANList As String = String.Empty
                For Dtlindex = 0 To dsMainDineInHold.Tables("CASHMEMODTL").Rows.Count - 1
                    Dim EAN As String = dsMainDineInHold.Tables("CASHMEMODTL").Rows(Dtlindex)("EAN")
                    Dim dr() = ds.Tables("DineInCashMemoDtl").Select("EAN= '" & EAN & "' ")
                    If dr.Length = 0 Then
                        deletedEANList = deletedEANList & "'" & EAN & "'" & ","
                    End If
                Next
                If Not String.IsNullOrEmpty(deletedEANList) Then
                    deletedEANList = deletedEANList.Substring(0, deletedEANList.Length - 1)
                    cmd.Length = 0
                    cmd.Append(" Delete  From DineInCashMemoDtl  Where EAN  IN ( " & deletedEANList & " ) ")
                    cmd.Append(" AND BillNo = '" & BillNO & "'  ;")

                    If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                        tran.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If
            End If

            MaxBilllineno = GetMaxBillLineNo(BillNO, sitecode, SpectrumCon, tran)
            '---Whether Insert or Update 

            If Not dsMainDineInHold.Tables("CASHMEMOHDR") Is Nothing Then
                Dim drDinInHdr() = dsMainDineInHold.Tables("CASHMEMOHDR").Select("BillNo='" & BillNO & "' And sitecode = '" & sitecode & "' ")
                If drDinInHdr.Length > 0 Then
                    cmd.Length = 0
                    cmd.Append("  UPDATE   DinInCashMemoHdr ")
                    cmd.Append("  SET       ")
                    cmd.Append("  NetAmt = " & ds.Tables("DineInCashMemoHdr").Rows(0)("NetAmt") & ",  ")
                    cmd.Append("  CostAmt = " & IIf(IsDBNull(ds.Tables("DineInCashMemoHdr").Rows(0)("CostAmt")), 0, ds.Tables("DineInCashMemoHdr").Rows(0)("CostAmt")) & ",  ")
                    cmd.Append("  GrossAmt = " & ds.Tables("DineInCashMemoHdr").Rows(0)("GrossAmt") & ",  ")
                    cmd.Append("  UPDATEDAT = '" & sitecode & "',  ")
                    cmd.Append("  UPDATEDBY = '" & Userid & "',  ")
                    cmd.Append("  DeliveryPersonID = '" & salesPerson & "',  ")
                    cmd.Append("  CLPNO = '" & ds.Tables("DineInCashMemoHdr").Rows(0)("CLPNO") & "',  ")
                    cmd.Append("  UPDATEDON = getdate() , ")
                    cmd.Append("  Totaldiscount ='" & ds.Tables("DineInCashMemoHdr").Rows(0)("Totaldiscount") & "'")
                    cmd.Append("  Where billno = '" & ds.Tables("DineInCashMemoDtl").Rows(0)("billno") & "'  ")
                    cmd.Append("  and Sitecode = '" & ds.Tables("DineInCashMemoDtl").Rows(0)("Sitecode") & "'  ")
                    If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                        tran.Rollback()
                        CloseConnection()
                        Return False
                    End If
                Else : GoTo insert
                End If
            Else
                cmd.Length = 0
insert:         cmd.Append(" INSERT INTO DinInCashMemoHdr ")
                cmd.Append(" (sitecode,TerminalID,   ")
                cmd.Append("  BILLNO,BillDate,BillTime,ServiceTaxAmount,DeliveryPersonID, CREATEDAT,")
                cmd.Append("  CREATEDBY, CREATEDON,UPDATEDAT,  ")
                cmd.Append("  UPDATEDBY, UPDATEDON, STATUS,clpno) ")
                cmd.Append(" VALUES (")
                cmd.Append("'" & sitecode & "','" & TerminalId & "',")
                cmd.Append("'" & BillNO & "',@DayOpenDate,getdate(),'" & CustomerSaleType & "','" & salesPerson & "','" & sitecode & "',")
                cmd.Append("'" & Userid & "',getDate(),'" & sitecode & "',")
                cmd.Append("'" & Userid & "',getDate(), 'True','" & custno & "'")
                cmd.Append(")")
                If objComm.InsertOrUpdateRecordForDinein(cmd.ToString(), DayOpenDate, tran) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
            End If
            Dim articleDineInRemark As String
            If ds.Tables("DineInCashMemoDtl").Rows.Count > 0 Then
                For Dtlindex = 0 To ds.Tables("DineInCashMemoDtl").Rows.Count - 1
                    cmd.Length = 0
                    '--- Code Added By Mahesh to Update Remark in DineInCashMemoDtl Table ... 
                    Dim EAN As String = ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("EAN")
                    'Dim DrDineInRemark() = DtDineInItemRemarks.Select("ArticleCode ='" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("Articlecode") & "' and EAN='" & EAN & "'and BillLineNo='" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("BillLineNo") & "'")
                    Dim DrDineInRemark() = DtDineInItemRemarks.Select("ArticleCode ='" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("Articlecode") & "' and EAN='" & EAN & "'")
                    If DrDineInRemark.Count > 0 Then
                        articleDineInRemark = DrDineInRemark(0)("Remark").ToString()
                    Else
                        articleDineInRemark = "" '##Vipin
                    End If
                    If Not dsMainDineInHold.Tables("CASHMEMODTL") Is Nothing Then
                        ' Dim dr() = dsMainDineInHold.Tables("CASHMEMODTL").Select("BillNo='" & BillNO & "' And sitecode = '" & sitecode & "' and EAN = '" & EAN & "'and BillLineNo='" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("BillLineNo") & "'")
                        Dim dr() = dsMainDineInHold.Tables("CASHMEMODTL").Select("BillNo='" & BillNO & "' And sitecode = '" & sitecode & "' and EAN = '" & EAN & "'")
                        If dr.Length > 0 Then
                            cmd.Append("  UPDATE    DineInCashMemoDtl ")
                            cmd.Append("  SET               ")
                            cmd.Append("  btype	=	'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("btype") & "'	, ")
                            cmd.Append("  batchbarcode	=	'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("batchbarcode") & "'	, ")
                            '  cmd.Append("  Sitecode	=	'" & sitecode & "', ")
                            'cmd.Append("  billlineno	='" & dsMainDineInHold.Tables("CASHMEMODTL").Rows(Dtlindex)("billlineno") & "', ")
                            cmd.Append("  billno	=	'" & BillNO & "', ")
                            '   cmd.Append("  ean	=	'" & ds.Tables("HoldCASHMEMODTL").Rows(Dtlindex)("ean") & "', ")
                            cmd.Append("  Articlecode	=	'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("Articlecode") & "', ")


                            cmd.Append("  quantity	=	(SELECT  dbo.CompareQtyWithTable( '" & sitecode & "','" & BillNO & "','" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("ean") & "','" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("Articlecode") & "','" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("quantity") & "')), ") '	, ")
                            'cmd.Append("  quantity	=	'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("quantity") & "'	, ")

                            cmd.Append("  COSTPRICE	= '" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("COSTPRICE") & "', ")

                            cmd.Append("  GROSSAMT	= '" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("GROSSAMT") & "', ")
                            ' 'code added for issue id 1270-changes in generated bill print format by vipul
                            cmd.Append("  SellingPrice	= '" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("SellingPrice") & "', ")
                            cmd.Append("  TOTALDISCOUNT	= '" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("TOTALDISCOUNT") & "', ")

                            ' cmd.Append("  LINEDISCOUNT	= '" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("LINEDISCOUNT") & "', ")
                            'code added by vipul for issue id 3387
                            If IsDBNull(ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("LINEDISCOUNT")) Then
                                cmd.Append(" LINEDISCOUNT= '" & 0 & "' ,")
                            Else
                                cmd.Append("LINEDISCOUNT= '" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("LINEDISCOUNT") & "', ")
                            End If

                            cmd.Append("  PROMOTIONID	= '" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("PROMOTIONID") & "', ")

                            cmd.Append("  TotalTaxAmount	= '" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("TotalTaxAmount") & "', ")
                            cmd.Append("  NETAMOUNT	= '" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("NETAMOUNT") & "', ")
                            cmd.Append("  UPDATEDAT	=	'" & sitecode & "', ")
                            cmd.Append("  UPDATEDBY	= '" & Userid & "', ")
                            cmd.Append("  UPDATEDON	=	GETDATE(), ")
                            cmd.Append("  Remark ='" & articleDineInRemark & "'")
                            cmd.Append("  Where billno = '" & BillNO & "'  ")
                            cmd.Append("  and Sitecode = '" & sitecode & "'  ")
                            cmd.Append("  and ean = '" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("ean") & "'")

                            If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                                tran.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        Else : GoTo Update
                        End If
                    Else
                        cmd.Length = 0
Update:                 MaxBilllineno = MaxBilllineno + 1
                        cmd.Append(" INSERT INTO DineInCashMemoDtl( ")
                        cmd.Append(" Sitecode, billno,  ")
                        cmd.Append(" billlineno,EAN, BType, ArticleCode,  ")
                        cmd.Append(" SalesStaffID, SellingPrice, CostPrice, GrossAmt, TotalTaxAmount,NetAmount,  ")
                        cmd.Append(" Quantity, BillDate,BillTime, TotalDiscount,LINEDISCOUNT,PROMOTIONID, TotalDiscPercentage,  ")
                        cmd.Append(" CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT,  ")
                        cmd.Append(" UPDATEDBY, UPDATEDON, STATUS,CLPRequire,Remark) ")
                        cmd.Append(" Values(")
                        cmd.Append("'" & sitecode & "','" & BillNO & "',")
                        cmd.Append("'" & MaxBilllineno & "','" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("EAN") & "',")
                        cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("BType") & "','" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("ArticleCode") & "',")
                        If IsDBNull(ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("SalesStaffID")) Then
                            cmd.Append("'" & 0 & "' ,")
                        Else
                            cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("SalesStaffID") & "' ,")
                        End If
                        If IsDBNull(ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("SellingPrice")) Then
                            cmd.Append("'" & 0 & "' ,")
                        Else
                            cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("SellingPrice") & "' ,")
                        End If
                        cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("CostPrice") & "' ,")
                        cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("GrossAmt") & "' ,")
                        'code added for issue id 1270-changes in generated bill print format by vipul
                        cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("TotalTaxAmount") & "' ,")
                        cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("NetAmount") & "' ,")
                        cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("Quantity") & "' ,")

                        'cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("MRP") & "' ,")
                        'cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("SalesStaffID") & "','" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("SellingPrice") & "',")
                        'cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("CostPrice") & "','" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("GrossAmt") & "',")
                        'cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("Quantity") & "','" & DayOpenDate & "',")
                        cmd.Append("@DayOpenDate,")
                        cmd.Append("GetDate(),")
                        If IsDBNull(ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("TotalDiscount")) Then
                            cmd.Append("'" & 0 & "' ,")
                        Else
                            cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("TotalDiscount") & "' ,")
                        End If

                        If IsDBNull(ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("LINEDISCOUNT")) Then
                            cmd.Append("'" & 0 & "' ,")
                        Else
                            cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("LINEDISCOUNT") & "' ,")
                        End If

                        cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("PROMOTIONID") & "' ,")
                        If IsDBNull(ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("TotalDiscPercentage")) Then
                            cmd.Append("'" & 0 & "' ,")
                        Else
                            cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("TotalDiscPercentage") & "' ,")
                        End If
                        'cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("TotalDiscPercentage") & "','" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("CREATEDAT") & "',")
                        cmd.Append("'" & sitecode & "',")
                        cmd.Append("'" & Userid & "',GetDate(),'" & sitecode & "',")
                        cmd.Append("'" & Userid & "',GetDate(),'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("STATUS") & "', 'True', ")
                        cmd.Append("'" & articleDineInRemark & "' ")
                        cmd.Append(" );")

                        If objComm.InsertOrUpdateRecordForDinein(cmd.ToString(), DayOpenDate, tran) = False Then
                            tran.Rollback()
                            CloseConnection()
                            Return False
                        End If
                    End If
                Next
            End If
            ''' added by nikhi for OM Ganeshay to saving tax in DineCashMemoTaxdetails

            '' added by nikhil   for Om Ganeshya
            If IsDineIn = True Then
                If DineTax.Rows.Count > 0 Then
                    For i = 0 To DineTax.Rows.Count - 1
                        cmd.Length = 0

                        'Dim EAN As String = ds.Tables("DineInCashMemoTaxDtls").Rows(Dtlindex)("EAN")
                        'Dim DrDineInRemark() = DtDineInItemRemarks.Select("ArticleCode ='" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("Articlecode") & "' and EAN='" & EAN & "'")
                        'If DrDineInRemark.Count > 0 Then
                        '    articleDineInRemark = DrDineInRemark(0)("Remark")
                        'End If
                        ' If Not dsMainDineInHold.Tables("CASHMEMOTAXDTLS") Is Nothing Then
                        'Dim dr() = dsMainDineInHold.Tables("CASHMEMOTAXDTLS").Select("BillNo='" & BillNO & "' And sitecode = '" & sitecode & "'")
                        'If dr.Length > 0 Then
                        '        cmd.Length = 0
                        '        cmd.Append("  UPDATE   DineInCashMemoTaxDtls ")
                        '        cmd.Append("  SET       ")
                        '        cmd.Append("  SiteCode = " & ds.Tables("DineInCashMemoTaxDtls").Rows(0)("SiteCode") & ",  ")
                        '        cmd.Append("  BillNo =  " & ds.Tables("DineInCashMemoTaxDtls").Rows(0)("BillNo") & ",  ")
                        '        cmd.Append("  BillLineNo = " & ds.Tables("DineInCashMemoTaxDtls").Rows(0)("BillLineNo") & ",  ")
                        '        cmd.Append("  TaxLabel = " & ds.Tables("DineInCashMemoTaxDtls").Rows(0)("TaxLabel") & ",  ")
                        '        cmd.Append("  TaxValue = " & ds.Tables("DineInCashMemoTaxDtls").Rows(0)("TaxValue") & ",  ")
                        '        cmd.Append("  UPDATEDAT = '" & sitecode & "',  ")
                        '        cmd.Append("  UPDATEDBY = '" & Userid & "',  ")
                        '        cmd.Append("  UPDATEDON = getdate(),  ")
                        '        cmd.Append("  Where BillNo = '" & ds.Tables("DineInCashMemoTaxDtls").Rows(0)("BillNo") & "'  ")
                        '        cmd.Append("  and Sitecode = '" & ds.Tables("DineInCashMemoTaxDtls").Rows(0)("Sitecode") & "'  ")

                        '        If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                        '            tran.Rollback()
                        '            CloseConnection()
                        '            Return False
                        '        End If
                        '    Else : GoTo Update1
                        '    End If
                        'Else
                        cmd.Length = 0
Update1:                MaxBilllineno = MaxBilllineno + 1
                        cmd.Append(" if NOT exists (select  * from DineInCashMemoTaxDtls where SiteCode= '" & sitecode & "' and BillNo='" & BillNO & "'and BillLineNo='" & DineTax.Rows(i)("BILLLINENO") & "' and TaxLineNo='" & DineTax.Rows(i)("STEPNO") & "')") 'added by vipin 30.08.2018
                        cmd.Append(" INSERT INTO DineInCashMemoTaxDtls ")
                        cmd.Append(" (SiteCode,BillNo,   ")
                        cmd.Append("  BillLineNo,TaxLineNo,TaxLabel,TaxValue,")
                        cmd.Append("  CREATEDAT,CREATEDBY, CREATEDON,UPDATEDAT,  ")
                        cmd.Append("  UPDATEDBY, UPDATEDON, STATUS) ")
                        cmd.Append(" VALUES (")
                        cmd.Append("'" & sitecode & "','" & BillNO & "',")
                        cmd.Append("'" & DineTax.Rows(i)("BILLLINENO") & "','" & DineTax.Rows(i)("STEPNO") & "','" & DineTax.Rows(i)("TAXCODE") & "','" & IIf(IsDBNull(DineTax.Rows(i)("TAXAMOUNT")), 0, DineTax.Rows(i)("TAXAMOUNT")) & "',")
                        cmd.Append("'" & sitecode & "','" & Userid & "',getDate(),'" & sitecode & "',")
                        cmd.Append("'" & Userid & "',getDate(), 'True'")
                        cmd.Append(" )")
                        cmd.Append(" Else")
                        cmd.Append("   UPDATE   DineInCashMemoTaxDtls ")
                        cmd.Append("  SET       ")
                        cmd.Append("  TaxValue = " & DineTax.Rows(i)("TAXAMOUNT") & ",  ")
                        cmd.Append("  UPDATEDAT = '" & sitecode & "',  ")
                        cmd.Append("  UPDATEDBY = '" & Userid & "',  ")
                        cmd.Append("  UPDATEDON = getdate()  ")
                        cmd.Append("  Where Sitecode = '" & sitecode & "' ")
                        cmd.Append("  and BillNo = '" & BillNO & "'   ")
                        cmd.Append("  and TaxLineNo = '" & DineTax.Rows(i)("STEPNO") & "'   ")
                        cmd.Append("  and BillLineNo = '" & DineTax.Rows(i)("BILLLINENO") & "'  ")

                        If objComm.InsertOrUpdateRecordForDinein(cmd.ToString(), DayOpenDate, tran) = False Then
                            tran.Rollback()
                            CloseConnection()
                            Return False
                        End If
                        'End If
                    Next
                End If

            End If
            If IsNewOrder = True Then
                cmd.Length = 0
                cmd.Append("  UPDATE    OrderDineInMap ")
                cmd.Append("  SET               ")
                cmd.Append("  UPDATEDON	=	getDate()	")
                If BillGenerate > 0 Then
                    cmd.Append(" , OrderStatus ='" & BillGenerate & "' ")
                End If
                cmd.Append("  Where BillNo = '" & BillNO & "'  ")
                cmd.Append("  and DineInNumber = '" & currentDineInTable & "'  ")

                If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
            Else
                cmd.Length = 0
                cmd.Append(" INSERT INTO OrderDineInMap( ")
                cmd.Append(" Sitecode, DineInNumber,  ")
                cmd.Append(" BillNo,BillDate,  ")
                cmd.Append(" CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT,  ")
                cmd.Append(" UPDATEDBY, UPDATEDON, STATUS,OrderStatus) ")
                cmd.Append(" Values(")
                cmd.Append("'" & sitecode & "','" & currentDineInTable & "',")
                cmd.Append("'" & BillNO & "',@DayOpenDate,")
                cmd.Append("'" & sitecode & "',")
                cmd.Append("'" & Userid & "',GetDate(),'" & sitecode & "',")
                cmd.Append("'" & Userid & "',GetDate(), 'True',0")
                cmd.Append(" )")

                If objComm.InsertOrUpdateRecordForDinein(cmd.ToString(), DayOpenDate, tran) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
            End If
            '------------------------------------------------------------
            If DinInFlagForKOT = True Then
                cmd.Length = 0
                If Not dtGenerate Is Nothing Then
                    If dtGenerate.Rows.Count > 0 Then
                        For Dtlindex = 0 To dtGenerate.Rows.Count - 1
                            cmd.Append(" INSERT INTO DineInKotQtyMap( ")
                            cmd.Append("  billno, EAN, ")
                            cmd.Append("  ArticleCode, KotQuantity, status,KotNO,Remark,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON)")
                            cmd.Append(" Values(")
                            cmd.Append("'" & BillNO & "','" & dtGenerate.Rows(Dtlindex)("EAN") & "',")
                            cmd.Append("'" & dtGenerate.Rows(Dtlindex)("ArticleCode") & "','" & dtGenerate.Rows(Dtlindex)("KotQuantity") & "',")
                            cmd.Append(" 1,'" & dtGenerate.Rows(Dtlindex)("KotNo") & "','" & dtGenerate.Rows(Dtlindex)("Remark") & "','" & sitecode & "',")
                            cmd.Append("'" & Userid & "',getDate(),'" & sitecode & "',")
                            cmd.Append("'" & Userid & "',getDate() ")
                            cmd.Append(");")
                        Next
                        If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                            tran.Rollback()
                            CloseConnection()
                            Return False
                        End If
                    End If
                End If

            End If
            '-------------------------------------------------------------
            If IsNewOrder = False Then
                If UpdateDocumentNo("DineIn", SpectrumCon, tran) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
            End If

            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try

    End Function
    '' added by nikhil
    Public Function InsertOrUpDataSqlQuery(ByVal SreSQL As String) As Boolean
        Try
            Dim objComm As New clsCommon
            If objComm.InsertOrUpdateRecord(SreSQL, tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetDineCashMemoTaxStructure() As DataTable
        Try
            'Dim dt As New DataTable
            'Dim cmdTrn As New SqlCommand("select * from DineInCashMemoTaxDtls where 1=0 ")
            'Dim da As New SqlDataAdapter(cmdTrn.ToString, ConString)
            'da.Fill(dt)
            'Return dt

            Dim strQuery As New System.Text.StringBuilder
            Dim dt As New DataTable
            strQuery.Length = 0
            strQuery.Append(" select * from CASHMEMOTAXDTLS where 1=0")
            OpenConnection()
            Dim sqlCmd As New SqlCommand
            sqlCmd.CommandText = strQuery.ToString()
            sqlCmd.Connection = SpectrumCon()
            Dim sqlAdptor As New SqlDataAdapter
            sqlAdptor.SelectCommand = sqlCmd
            sqlAdptor.Fill(dt)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    '' added by nikhil for Om Ganeshya
    
    Public Function GetDineCashMemo(ByVal CashMemoNO As String, ByVal SiteCode As String, ByVal LangCode As String, Optional ByVal MergId As String = "0", Optional ByVal BillNoOfMergeId As String = "") As DataTable
        Try
            Dim StrSql As String
            'StrSql = "SELECT * FROM CMPRINT WHERE BILLNO='" & CashMemoNO.Trim & "' AND SiteCode='" & SiteCode & "' "
            ' StrSql = "SELECT * FROM CMPRINTDineIn ('" & CashMemoNO.Trim & "','" & SiteCode & "','" & LangCode & "' )"
            If MergId = 0 Then
                StrSql = "SELECT * FROM CMPRINTDineIn ('" & CashMemoNO.Trim & "','" & SiteCode & "','" & LangCode & "' )"
            Else
                StrSql = "SELECT * FROM CMPRINTMergeDineIn ('" & BillNoOfMergeId.Trim & "','" & SiteCode & "','" & LangCode & "' )"
            End If
            Dim dt As DataTable = GetFilledTable(StrSql)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetMergeOrderBillNo(ByVal siteCode As String, ByVal MergeId As String) As DataTable 'vipul
        Try
            Dim dt As New DataTable
            Dim daxread As SqlDataAdapter
            Dim stringComman As String = "GetBillNoByMergeId"
            Dim cmd As New SqlCommand(stringComman.ToString(), SpectrumCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@MergeId", MergeId)
            daxread = New SqlDataAdapter(cmd)
            daxread.Fill(dt)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetTaxNameByTaxCode(ByVal siteCode As String, ByVal TaxCode As String) As DataTable 'vipul
        Try
            Dim dt As New DataTable
            Dim daxread As SqlDataAdapter
            Dim stringComman As String = "GetTaxNameByTaxCode"
            Dim cmd As New SqlCommand(stringComman.ToString(), SpectrumCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@TaxCode", TaxCode)
            daxread = New SqlDataAdapter(cmd)
            daxread.Fill(dt)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetDineCustomers(ByVal DineCashMemoNO As String, ByVal SiteCode As String) As DataTable
        Try
            Dim StrSql As New StringBuilder
            StrSql.Length = 0
            'StrSql = "SELECT * FROM CMPRINT WHERE BILLNO='" & CashMemoNO.Trim & "' AND SiteCode='" & SiteCode & "' "
            StrSql = StrSql.Append("select ISNULL(TITLE,'')+ ' ' +ISNULL(I.FIRSTNAME,'')+ ' '+ISNULL(I.MIDDLENAME,'')+ ' '+ISNULL(I.SURNAME,'')  AS CLPNAME ")
            StrSql = StrSql.Append(" from CLPCUSTOMERS I  Inner Join  DinInCashMemoHdr DHdr on DHDR.CLPNo= I.CardNo where BillNo = '" & DineCashMemoNO & "'  and DHDR.SiteCode='" & SiteCode & "'")
            Dim dt As DataTable = GetFilledTable(StrSql.ToString)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetComboDetailsDataForDineInPrint(ByVal BillNo As String, ByVal sitecode As String, ByVal langcode As String) As DataTable
        Try
            Dim StrQuery As String = "SELECT  A.BillNO, A.BillLineNo ,a.ComboArticleCode , a.ArticleCode  ,b.ArticleName , a.Quantity FROM      DineInCashMemoDtl as a inner join mstarticle as b on a.ArticleCode = b.ArticleCode WHERE A.BillNO='" & BillNo & "' AND A.SiteCode='" & sitecode & "' "
            Dim da As New SqlDataAdapter(StrQuery, ConString)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetBillDetailsForDineDataForPrint(ByVal BillNo As String, ByVal sitecode As String, ByVal langcode As String, Optional sectionwisearticle As Boolean = False, Optional ByVal InvoiceType As String = "", Optional ByVal MergId As String = "0", Optional ByVal BillNoOfMergeId As String = "") As DataTable
        Try
            'Dim StrQuery As String = "SELECT A.BillLineNo , A.ARTICLECODE,A.UnitofMeasure,Case when C.ArticleShortName is null then B.ArticleShortName else C.ArticleShortName END AS DISCRIPTION, Case when C.ArticleName is null then B.ArticleName else C.ArticleShortName END AS ArticleFullName, A.QUANTITY, A.SELLINGPRICE, A.GROSSAMT, A.TOTALDISCOUNT, A.TOTALDISCPERCENTAGE, A.NETAMOUNT,A.PromotionId, A.TOTALTAXAMOUNT,case when  B.LastNodeCode=  P.LastNodeCode then 1 else 0 end as IsQuntityWiseRequired ,RE.itemRemarks AS Remark ,HSNCode,'' as GSTNo,(select SUM(TaxValue) as TotalTax from [dbo].[TaxSiteDocMap] where SiteCode='" & sitecode & "') as TotalTax FROM DineInCashMemoDtl A INNER JOIN MSTARTICLE B ON A.ARTICLECODE=B.ARTICLECODE left outer join ArticleDescInDiffLang C on A.ArticleCode=C.ArticleCode AND C.LanguageCode='" & langcode & "' left outer join  ( select * from dbo.GetLastNodeCode((select * from dbo.LastNodeCode( ))) )as  P on P.LastNodeCode=b.LastNodeCode LEFT OUTER JOIN CashMemoDtlItemRemark RE ON RE.articleCode =A.ArticleCode AND RE.billNo =A.BillNo   WHERE A.BillNO='" & BillNo & "' AND A.SiteCode='" & sitecode & "' AND B.Printable = 1"   ''A.TakeAwayQUANTITY,
            'If sectionwisearticle Then
            '    StrQuery += "order by b.LastNodeCode"
            'End If
            Dim StrQuery As String = ""

            If MergId = 0 Then
                StrQuery = "SELECT A.BillLineNo , A.ARTICLECODE,A.UnitofMeasure,Case when C.ArticleShortName is null then B.ArticleShortName else C.ArticleShortName END AS DISCRIPTION, Case when C.ArticleName is null then B.ArticleName else C.ArticleShortName END AS ArticleFullName, A.QUANTITY, A.SELLINGPRICE, A.GROSSAMT, A.TOTALDISCOUNT, A.TOTALDISCPERCENTAGE, A.NETAMOUNT,A.PromotionId, A.TOTALTAXAMOUNT,case when  B.LastNodeCode=  P.LastNodeCode then 1 else 0 end as IsQuntityWiseRequired ,RE.itemRemarks AS Remark ,HSNCode,'' as GSTNo,(select SUM(TaxValue) as TotalTax from [dbo].[TaxSiteDocMap] where SiteCode='" & sitecode & "') as TotalTax FROM DineInCashMemoDtl A INNER JOIN MSTARTICLE B ON A.ARTICLECODE=B.ARTICLECODE left outer join ArticleDescInDiffLang C on A.ArticleCode=C.ArticleCode AND C.LanguageCode='" & langcode & "' left outer join  ( select * from dbo.GetLastNodeCode((select * from dbo.LastNodeCode( ))) )as  P on P.LastNodeCode=b.LastNodeCode LEFT OUTER JOIN CashMemoDtlItemRemark RE ON RE.articleCode =A.ArticleCode AND RE.billNo =A.BillNo   WHERE A.BillNO ='" & BillNo & "' AND A.SiteCode='" & sitecode & "' AND B.Printable = 1"   ''A.TakeAwayQUANTITY,
                If sectionwisearticle Then
                    StrQuery += "order by b.LastNodeCode"
                End If
            Else

                ''StrQuery = "SELECT A.BillLineNo , A.ARTICLECODE,A.UnitofMeasure,Case when C.ArticleShortName is null then B.ArticleShortName else C.ArticleShortName END AS DISCRIPTION, Case when C.ArticleName is null then B.ArticleName else C.ArticleShortName END AS ArticleFullName, A.QUANTITY, A.SELLINGPRICE, A.GROSSAMT, A.TOTALDISCOUNT, A.TOTALDISCPERCENTAGE, A.NETAMOUNT,A.PromotionId, A.TOTALTAXAMOUNT,case when  B.LastNodeCode=  P.LastNodeCode then 1 else 0 end as IsQuntityWiseRequired ,RE.itemRemarks AS Remark ,HSNCode,'' as GSTNo,(select SUM(TaxValue) as TotalTax from [dbo].[TaxSiteDocMap] where SiteCode='" & sitecode & "') as TotalTax FROM DineInCashMemoDtl A INNER JOIN MSTARTICLE B ON A.ARTICLECODE=B.ARTICLECODE left outer join ArticleDescInDiffLang C on A.ArticleCode=C.ArticleCode AND C.LanguageCode='" & langcode & "' left outer join  ( select * from dbo.GetLastNodeCode((select * from dbo.LastNodeCode( ))) )as  P on P.LastNodeCode=b.LastNodeCode LEFT OUTER JOIN CashMemoDtlItemRemark RE ON RE.articleCode =A.ArticleCode AND RE.billNo =A.BillNo   WHERE A.BillNO  IN(SELECT * FROM fnSplitString('" & BillNoOfMergeId & "',',')) AND A.SiteCode='" & sitecode & "' AND B.Printable = 1"
                StrQuery = StrQuery + " SELECT  '0' BillLineNo,A.ARTICLECODE,"
                StrQuery = StrQuery + "Case when C.ArticleShortName is null then B.ArticleShortName else C.ArticleShortName END AS DISCRIPTION,"
                StrQuery = StrQuery + "Case when C.ArticleName is null then B.ArticleName else C.ArticleShortName END AS ArticleFullName, "
                StrQuery = StrQuery + "sum(A.QUANTITY) QUANTITY, A.SELLINGPRICE,sum(A.GROSSAMT) GROSSAMT,sum(A.TOTALDISCOUNT) TOTALDISCOUNT, "
                StrQuery = StrQuery + "sum(A.TOTALDISCPERCENTAGE) TOTALDISCPERCENTAGE,sum(A.NETAMOUNT) NETAMOUNT,"
                StrQuery = StrQuery + "sum(A.TOTALTAXAMOUNT) TOTALTAXAMOUNT FROM DineInCashMemoDtl A "
                StrQuery = StrQuery + "INNER JOIN MSTARTICLE B ON A.ARTICLECODE=B.ARTICLECODE "
                StrQuery = StrQuery + "left outer join ArticleDescInDiffLang C on A.ArticleCode=C.ArticleCode AND C.LanguageCode='" & langcode & "'"
                StrQuery = StrQuery + "left outer join  ( select * from dbo.GetLastNodeCode((select * from dbo.LastNodeCode( ))) )as  P on P.LastNodeCode=b.LastNodeCode "
                StrQuery = StrQuery + "LEFT OUTER JOIN CashMemoDtlItemRemark RE ON RE.articleCode =A.ArticleCode AND RE.billNo =A.BillNo "
                StrQuery = StrQuery + "WHERE A.BillNO  IN(SELECT * FROM fnSplitString('" & BillNoOfMergeId & "',',')) AND A.SiteCode='" & sitecode & "' AND B.Printable = 1"
                StrQuery = StrQuery + "group by A.ARTICLECODE,Case when C.ArticleShortName is null then B.ArticleShortName else C.ArticleShortName END, "
                StrQuery = StrQuery + "Case when C.ArticleName is null then B.ArticleName else C.ArticleShortName END ,A.SELLINGPRICE"

            End If


            Dim da As New SqlDataAdapter(StrQuery, ConString)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try


    End Function

    Public Function GetTaxDetailsForDineInBillNo(ByVal siteCode As String, ByVal billNo As String) As DataTable
        Try
            Dim query As String = "Exec GetTaxInfoForADineInBill '" & siteCode & "', '" & billNo & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    '' ended by nikhil

    Public Function MergeDinInData(ByVal DayOpenDate As Date, ByVal ds As DataSet, ByVal dsMainDineInHold As DataSet, ByVal sitecode As String, ByVal finyear As String, ByVal currentDineInTable As String, ByVal TerminalId As String, ByVal Userid As String, ByVal HoldKey As String, ByRef BillNO As String, ByVal salesPerson As String, CustomerSaleType As Int16, Optional ByVal BillGenerate As Integer = 0) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try

            Dim MaxBilllineno As Integer = 0
            Dim drDinIn As DataRow
            Dim IsUpdateBill As Boolean
            ServerDate = GetCurrentDate()
            Dim DocNo As String = getDocumentNo("DineIn", sitecode)
            If String.IsNullOrEmpty(BillNO) Then
                Try
                    Dim strFinyear As String = GetFinancialYear(DayOpenDate, sitecode)
                    BillNO = GenDocNo("DI" & TerminalId & strFinyear.Substring(strFinyear.Length - 2, 2), 15, DocNo)
                Catch ex As Exception
                    BillNO = "DI" & TerminalId & DocNo
                End Try
            Else
                IsUpdateBill = True
            End If

            Dim objComm As New clsCommon
            Dim cmd As New System.Text.StringBuilder()
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()


            MaxBilllineno = GetMaxBillLineNo(BillNO, sitecode, SpectrumCon, tran)


            'If Not ds.Tables("HoldCASHMEMODTL") Is Nothing AndAlso ds.Tables("HoldCASHMEMODTL").Rows.Count > 0 AndAlso Not dsMainDineInHold.Tables("CASHMEMODTL") Is Nothing Then

            '    Dim deletedEANList As String = String.Empty
            '    For Dtlindex = 0 To dsMainDineInHold.Tables("CASHMEMODTL").Rows.Count - 1
            '        Dim EAN As String = dsMainDineInHold.Tables("CASHMEMODTL").Rows(Dtlindex)("EAN")
            '        Dim dr() = ds.Tables("HoldCASHMEMODTL").Select("EAN= '" & EAN & "' ")
            '        If dr.Length = 0 Then
            '            deletedEANList = deletedEANList & EAN & ","
            '        End If
            '    Next
            '    If Not String.IsNullOrEmpty(deletedEANList) Then
            '        deletedEANList = deletedEANList.Substring(0, deletedEANList.Length - 1)

            '        cmd.Length = 0
            '        cmd.Append(" Delete  From HoldCASHMEMODTL  Where EAN  IN ( '" & deletedEANList & " ') ")
            '        cmd.Append(" AND BillNo = '" & BillNO & "'  ")
            '        If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
            '            tran.Rollback()
            '            CloseConnection()
            '            Return False
            '        End If
            '    End If
            'End If

            '---Whether Insert or Update 

            If IsUpdateBill Or Not dsMainDineInHold.Tables("CASHMEMOHDR") Is Nothing Then

                ' Dim drDinInHdr() = dsMainDineInHold.Tables("CASHMEMOHDR").Select("BillNo='" & BillNO & "' And sitecode = '" & sitecode & "' ")
                cmd.Length = 0
                cmd.Append("  UPDATE   DinInCashMemoHdr ")
                cmd.Append("  SET       ")
                '    cmd.Append("  BillNo = '" & BillNO & "', ")
                cmd.Append("  SiteCode = '" & sitecode & "',  ")
                cmd.Append("  NetAmt = NetAmt + " & ds.Tables("DineInCashMemoHdr").Rows(0)("NetAmt") & ",  ")
                If Not IsDBNull(ds.Tables("DineInCashMemoHdr").Rows(0)("CostAmt")) Then
                    cmd.Append("  CostAmt =CostAmt + '" & ds.Tables("DineInCashMemoHdr").Rows(0)("CostAmt") & "',  ")
                End If
                cmd.Append("  GrossAmt = GrossAmt + " & ds.Tables("DineInCashMemoHdr").Rows(0)("GrossAmt") & ", ")
                cmd.Append("  UPDATEDAT = '" & sitecode & "',  ")
                cmd.Append("  UPDATEDBY = '" & Userid & "',  ")
                cmd.Append("  UPDATEDON = getdate()  ")
                cmd.Append("  Where billno = '" & BillNO & "'  ")
                cmd.Append("  and Sitecode = '" & sitecode & "'  ")

                If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
            Else
                cmd.Append(" INSERT INTO DinInCashMemoHdr ")
                cmd.Append(" (sitecode,TerminalID,   ")
                cmd.Append("  BILLNO,BillDate,BillTime, CREATEDAT,")
                cmd.Append("  CREATEDBY, CREATEDON,UPDATEDAT,  ")
                cmd.Append("  UPDATEDBY, UPDATEDON, STATUS) ")
                cmd.Append(" VALUES (")
                cmd.Append("'" & sitecode & "','" & TerminalId & "',")
                cmd.Append("'" & BillNO & "',@DayOpenDate,GetDate(),'" & sitecode & "',")
                cmd.Append("'" & Userid & "',getDate(),'" & sitecode & "',")
                cmd.Append("'" & Userid & "',getDate(), 'True'  ")
                cmd.Append(")")

                If objComm.InsertOrUpdateRecordForDinein(cmd.ToString(), DayOpenDate, tran) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
            End If





            If ds.Tables("DineInCashMemoDtl").Rows.Count > 0 Then
                For Dtlindex = 0 To ds.Tables("DineInCashMemoDtl").Rows.Count - 1
                    cmd.Length = 0

                    If Not dsMainDineInHold.Tables("CASHMEMODTL") Is Nothing Then
                        Dim EAN As String = ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("EAN")
                        Dim dr() = dsMainDineInHold.Tables("CASHMEMODTL").Select("BillNo='" & BillNO & "' And sitecode = '" & sitecode & "' and EAN = '" & EAN & "'")
                        If dr.Length > 0 Then
                            cmd.Append("  UPDATE    DineInCashMemoDtl ")
                            cmd.Append("  SET               ")
                            cmd.Append("  btype	=	'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("btype") & "',")
                            cmd.Append("  batchbarcode	=	'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("batchbarcode") & "',")
                            'cmd.Append("  billlineno	='" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("billlineno") & "', ")
                            '  cmd.Append("  billno	=	'" & BillNO & "', ")
                            cmd.Append("  Articlecode	=	'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("Articlecode") & "',")
                            cmd.Append("  quantity	=	quantity + " & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("quantity") & ",")
                            cmd.Append("  COSTPRICE	= '" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("COSTPRICE") & "', ")
                            cmd.Append("  GROSSAMT	= GROSSAMT + '" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("GROSSAMT") & "',")
                            'code added changes in generated bill print  by vipul
                            cmd.Append("  TotalTaxAmount	= '" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("TotalTaxAmount") & "', ")
                            cmd.Append("  NETAMOUNT	= NETAMOUNT + " & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("NETAMOUNT") & ",")
                            cmd.Append("  UPDATEDAT	=	'" & sitecode & "',")
                            cmd.Append("  UPDATEDBY	= '" & Userid & "', ")
                            cmd.Append("  UPDATEDON	=	GETDATE() ")
                            cmd.Append("  Where billno = '" & BillNO & "'  ")
                            cmd.Append("  and Sitecode = '" & sitecode & "'  ")
                            cmd.Append("  and ean = '" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("ean") & "'  ")

                            If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                                tran.Rollback()
                                CloseConnection()
                                Return False
                            End If
                        Else : GoTo Update
                        End If
                    Else
Update:                 MaxBilllineno = MaxBilllineno + 1
                        cmd.Append(" INSERT INTO DineInCashMemoDtl( ")
                        cmd.Append(" Sitecode, billno,  ")
                        cmd.Append(" billlineno,EAN, BType, ArticleCode,  ")
                        cmd.Append(" SalesStaffID, SellingPrice, CostPrice, GrossAmt,  ")
                        cmd.Append(" Quantity, BillDate,BillTime, TotalDiscount, TotalDiscPercentage,  ")
                        cmd.Append(" CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT,  ")
                        cmd.Append(" UPDATEDBY, UPDATEDON, STATUS,CLPRequire,TotalTaxAmount) ")
                        cmd.Append(" Values(")
                        cmd.Append("'" & sitecode & "','" & BillNO & "',")
                        cmd.Append("'" & MaxBilllineno & "','" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("EAN") & "',")
                        cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("BType") & "','" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("ArticleCode") & "',")
                        If IsDBNull(ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("SalesStaffID")) Then
                            cmd.Append("'" & 0 & "' ,")
                        Else
                            cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("SalesStaffID") & "' ,")
                        End If
                        If IsDBNull(ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("SellingPrice")) Then
                            cmd.Append("'" & 0 & "' ,")
                        Else
                            cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("SellingPrice") & "' ,")
                        End If
                        cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("CostPrice") & "' ,")
                        cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("GrossAmt") & "' ,")
                        cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("Quantity") & "' ,")
                        'cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("MRP") & "' ,")
                        'cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("SalesStaffID") & "','" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("SellingPrice") & "',")
                        'cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("CostPrice") & "','" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("GrossAmt") & "',")
                        'cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("Quantity") & "','" & DayOpenDate & "',")
                        cmd.Append("@DayOpenDate,")
                        cmd.Append("GetDate(),")
                        If IsDBNull(ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("TotalDiscount")) Then
                            cmd.Append("'" & 0 & "' ,")
                        Else
                            cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("TotalDiscount") & "' ,")
                        End If
                        If IsDBNull(ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("TotalDiscPercentage")) Then
                            cmd.Append("'" & 0 & "' ,")
                        Else
                            cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("TotalDiscPercentage") & "' ,")
                        End If
                        'cmd.Append("'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("TotalDiscPercentage") & "','" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("CREATEDAT") & "',")
                        cmd.Append("'" & sitecode & "',")
                        cmd.Append("'" & Userid & "',GetDate(),'" & sitecode & "',")
                        cmd.Append("'" & Userid & "',GetDate(),'" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("STATUS") & "', 'True','" & ds.Tables("DineInCashMemoDtl").Rows(Dtlindex)("TotalTaxAmount") & "' ")
                        cmd.Append(" );")

                        If objComm.InsertOrUpdateRecordForDinein(cmd.ToString(), DayOpenDate, tran) = False Then
                            tran.Rollback()
                            CloseConnection()
                            Return False
                        End If
                    End If



                Next
            End If

            cmd.Length = 0
            cmd.Append("  UPDATE    OrderDineInMap ")
            cmd.Append("  SET               ")
            cmd.Append("  UPDATEDON	=	GetDate()	")
            If BillGenerate > 0 Then
                cmd.Append(" , OrderStatus ='" & BillGenerate & "' ")
            End If
            cmd.Append("  Where BillNo = '" & BillNO & " '  ")
            cmd.Append("  and DineInNumber = '" & currentDineInTable & " '  ")

            If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If

            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Return False
        End Try

    End Function
    Public Function SwtichDinInData(ByVal DayOpenDate As Date, ByVal ds As DataSet, ByVal dsMainDineInHold As DataSet, ByVal sitecode As String, ByVal currentDineInTable As String, ByVal TerminalId As String, ByVal Userid As String, ByRef BillNO As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try

            Dim objComm As New clsCommon
            Dim cmd As New System.Text.StringBuilder()
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            cmd.Length = 0

            cmd.Append("  UPDATE    OrderDineInMap ")
            cmd.Append("  SET               ")
            cmd.Append("  UPDATEDON	=GetDate(),")
            cmd.Append("  DineInNumber	=	'" & currentDineInTable & "' ")
            cmd.Append("  Where BillNo = '" & BillNO & " '  ")

            If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If

            tran.Commit()
            CloseConnection()
            Return True


        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
        End Try
    End Function

    Public Function GetMaxBillLineNo(ByVal billNo As String, ByVal StrSiteCode As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Integer
        Try
            Dim dt As New DataTable
            Dim cmdTrn As New SqlCommand("select Max(BillLineNo) as maxLineItem from DineInCashMemoDtl where BillNo ='" & billNo & "' AND SiteCode='" & StrSiteCode & "'", con, tran)
            Dim daBillLineNo As New SqlDataAdapter(cmdTrn)
            daBillLineNo.Fill(dt)

            If Not IsDBNull(dt.Rows(0)(0)) Then
                Return dt.Rows(0)("maxLineItem")
            Else
                Return 0
            End If

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetDineInMergeOrderDtls(ByVal MergeId As String) As DataSet
        Try
            Dim ds As New DataSet

            'Dim dt As New DataTable
            Dim da As New SqlDataAdapter("select MergeName  FROM DineInMergeOrdersHdr Where MergeId =" & MergeId & "; Select  ODM.DineInNumber AS TableNumber ,Dmod.BillNo AS OrderNumber,Dmod.ReservationId  FROM    DineInMergeOrdersDtl  AS DMOD Inner Join OrderDineInMap AS ODM ON DMOD.BillNo = ODM.BillNo  WHERE ODM.Status='1' and DMOD.MergeId =" & MergeId & "; ", ConString)
            'da.Fill(dt)
            'If Not dt Is Nothing Then
            '    Return dt
            'End If
            'Return Nothing

            'Dim StrQuery As String
            'StrQuery = "select MergeName  FROM DineInMergeOrdersHdr Where MergeId =" & MergeId & ";" & _
            '          " Select  ODM.DineInNumber AS TableNumber ,Dmod.BillNo AS OrderNumber,Dmod.ReservationId  FROM    DineInMergeOrdersDtl  AS DMOD Inner Join " & _
            '          "OrderDineInMap AS ODM ON DMOD.BillNo = ODM.BillNo  WHERE ODM.Status='1' and DMOD.MergeId =" & MergeId & "; "
            ' Dim daCM As New SqlDataAdapter(ConString)
            da.Fill(ds)
            ds.Tables(0).TableName = "DineInMergeOrdersHdr"
            ds.Tables(1).TableName = "DineInMergeOrdersDtl"
            Return ds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function UpdateDinInStatus(ByVal userid As String, ByVal sitecode As String, ByVal Billno As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Boolean
        Try
            Dim cmdTrn As New SqlCommand("UPDATE OrderDineInMap SET STATUS='False',UPDATEDON=GETDATE(),UPDATEDBY='" & userid & "',UPDATEDAT='" & sitecode & "'  WHERE SITECODE='" & sitecode & "' AND   BILLNO='" & Billno & "'", con, tran)
            If CInt(cmdTrn.ExecuteNonQuery()) > 0 Then
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function UpdateDinInAndMergeStatus(ByVal userid As String, ByVal sitecode As String, ByVal mergeid As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Boolean
        Try
            Dim strQuery As String

            strQuery = "UPDATE OrderDineInMap SET STATUS='False',UPDATEDON=GETDATE(),UPDATEDBY='" & userid & "',UPDATEDAT='" & sitecode & "'  WHERE SITECODE='" & sitecode & "' AND   BILLNO IN (SELECT BillNo FROM DineInMergeOrdersDtl Where MergeId =" & mergeid & ");" & _
                       "UPDATE DineInMergeOrdersHdr SET STATUS='False',UPDATEDON=GETDATE(),UPDATEDBY='" & userid & "' WHERE MergeId=" & mergeid & ";"
            Dim cmdTrn As New SqlCommand(strQuery, con, tran)
            If CInt(cmdTrn.ExecuteNonQuery()) > 0 Then
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function UpdateCheckInStatus(ByVal userid As String, ByVal sitecode As String, ByVal DineInNumber As String, ByVal ReservationId As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Boolean
        Try
            Dim cmdTrn As New SqlCommand("UPDATE ReservationTbl SET STATUS='False',IsOccupied='False',UPDATEDON=GETDATE(),UPDATEDBY='" & userid & "',UPDATEDAT='" & sitecode & "'  WHERE SITECODE='" & sitecode & "' and ReservationId='" & ReservationId & "' AND TableNo='" & DineInNumber & "' AND Status=1 AND IsOccupied=1 ", con, tran)
            If CInt(cmdTrn.ExecuteNonQuery()) > 0 Then
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    '----Function for KotNumber
    Public Function GetMaxKotNo(ByVal billNo As String, ByVal StrSiteCode As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Integer
        Try
            Dim dt As New DataTable
            Dim cmdTrn As New SqlCommand("select Isnull(MAX(KotNO ),0) as maxKotNo from DineInKotQtyMap where BillNo ='" & billNo & "'", con, tran)
            Dim daBillLineNo As New SqlDataAdapter(cmdTrn)
            daBillLineNo.Fill(dt)

            If Not IsDBNull(dt.Rows(0)(0)) Then
                Return dt.Rows(0)("maxKotNo")
            Else
                Return 0
            End If

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Get Hold Data Information
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="BillNO">BillNo</param>
    ''' <param name="strSitecode">SiteCode</param>
    ''' <remarks></remarks>
    ''' 
    Public Sub GetHoldData(ByRef ds As DataSet, ByVal BillNO As String, ByVal strSitecode As String, ByVal userid As String, ByRef dtgv As DataTable, Optional ByVal isBatchManagementReq As Boolean = False, Optional IsDineInProcess As Boolean = False)
        Try
            If IsDineInProcess Or UpdateHoldStatus(userid, BillNO, strSitecode, "Resume") Then
                Dim strQuery As New Text.StringBuilder()
                Dim daResume As SqlDataAdapter
                Dim dtData As New DataSet
                strQuery.Length = 0
                strQuery.Append("SELECT SITECODE,TERMINALID,TRANSACTIONCODE,BILLNO,BILLDATE,BILLTIME,")
                strQuery.Append("DISCOUNTPERCENTAGE, CLPNO, NETAMT, COSTAMT, GROSSAMT, PAYMENTAMT, DISCOUNTAMT, ")
                strQuery.Append("CLPDISCOUNT, TOTALDISCOUNT, TOTALITEMS, ROUNDEDAMT, LINEITEMS, CREDITNOTENO, ")
                strQuery.Append("TRANSCURRENCY, CLPSCHEME, RETRIEVEDFROMCUSTNAME, POSTINGSTATUS, TAXAMOUNT, ")
                strQuery.Append("SERVICETAXAMOUNT, SALESEXECUTIVECODE, NOOFREPRINTS, REPRINTREASON, ")
                strQuery.Append("REPRINTDATE, REPRINTTIME, DELETIONDATE, DELETIONTIME, BALANCEPOINTS ,DeliveryPersonID, CustomerNO ")
                strQuery.Append("FROM HOLDCASHMEMOHDR WHERE SITECODE='" & strSitecode & "' AND BILLNO='" & BillNO & "';" & vbCrLf)
                'daResume = New SqlDataAdapter(strQuery.ToString(), ConString)
                'daResume.Fill(dtData)


                'strQuery.Length = 0
                If isBatchManagementReq Then
                    strQuery.Append("SELECT A.SITECODE,BILLLINENO,BTYPE,BILLNO,A.EAN,ISNULL(F.TotalPhysicalSaleableQty ,0) AS STOCK,A.BatchBarcode,A.ARTICLECODE,B.ARTICLENAME AS DISCRIPTION,SALESSTAFFID,")
                Else
                    strQuery.Append("SELECT A.SITECODE,BILLLINENO,BTYPE,BILLNO,A.EAN,ISNULL(F.TotalPhysicalSaleableQty ,0) AS STOCK,A.BatchBarcode,A.ARTICLECODE,B.ARTICLENAME AS DISCRIPTION,SALESSTAFFID,")
                End If
                'strQuery.Append("SELECT A.SITECODE,BILLLINENO,BTYPE,BILLNO,A.EAN,ISNULL(F.QtyAllocated ,0) AS STOCK,A.BatchBarcode,A.ARTICLECODE,B.ARTICLENAME AS DISCRIPTION,SALESSTAFFID,")
                strQuery.Append("QUANTITY,A.SELLINGPRICE,A.SELLINGPRICE As MRP,TRANSACTIONSTATUS,A.COSTPRICE,BILLDATE,BILLTIME, ")
                strQuery.Append("TOTALDISCOUNT,TOTALDISCPERCENTAGE,GROSSAMT,NETAMOUNT,CLPRequire,PROMOTIONID,NETSELLINGPRICE,SECTION,SHELF,TRANSACTIONCODE, ")
                strQuery.Append("SCALEITEM,RETURNNOSALE,SERIALNBR,LINEDISCOUNT,POSTINGSTATUS,CLPDISCOUNT, ")
                strQuery.Append("UNITOFMEASURE,Isnull(TOTALTAXAMOUNT,0) As TOTALTAXAMOUNT,IFBNO,SALESRETURNREASON,RETURNCMNO, ")
                strQuery.Append("RETURNCMDATE,RETURNQTY ")
                If isBatchManagementReq Then
                    strQuery.Append("FROM HOLDCASHMEMODTL A INNER JOIN MSTARTICLE B ON A.ARTICLECODE=B.ARTICLECODE LEFT OUTER JOIN ArticleBatchBinStockBalances F  ON A.SITECODE=F.SITECODE And F.BatchBarcode = A.BatchBarcode where A.SITECODE='" & strSitecode & "' AND A.BILLNO='" & BillNO & "';" & vbCrLf)
                Else
                    strQuery.Append("FROM HOLDCASHMEMODTL A INNER JOIN MSTARTICLE B ON A.ARTICLECODE=B.ARTICLECODE LEFT OUTER JOIN ARTICLESTOCKBALANCES F  ON A.SITECODE=F.SITECODE And A.ARTICLECODE=F.ARTICLECODE AND A.EAN=F.EAN where A.SITECODE='" & strSitecode & "' AND A.BILLNO='" & BillNO & "';" & vbCrLf)
                End If
                'strQuery.Append("FROM HOLDCASHMEMODTL A INNER JOIN MSTARTICLE B ON A.ARTICLECODE=B.ARTICLECODE LEFT OUTER JOIN BatchDtl F  ON A.SITECODE=F.SITECODE And F.BatchBarcode = A.BatchBarcode where A.SITECODE='" & strSitecode & "' AND A.BILLNO='" & BillNO & "';" & vbCrLf)

                strQuery.Append(" Select * from HoldVoucher where Sitecode='" & strSitecode & "' AND ISSUEDDOCNUMBER='" & BillNO & "';" & vbCrLf)

                '-----In case holding mettler bill
                strQuery.Append(" Select * from HoldCashMemoMettler where BillNO='" & BillNO & "';" & vbCrLf)

                daResume = New SqlDataAdapter(strQuery.ToString(), ConString)
                'dtData = New DataTable
                daResume.Fill(dtData)
                ds.Tables("CASHMEMOHDR").Merge(dtData.Tables(0), False, MissingSchemaAction.Ignore)
                ds.Tables("CASHMEMODTL").Merge(dtData.Tables(1), False, MissingSchemaAction.Ignore)
                ds.Tables("CashMemoMettler").Merge(dtData.Tables(3), False, MissingSchemaAction.Ignore)
                If Not dtgv Is Nothing Then
                    dtgv.Merge(dtData.Tables(2), False, MissingSchemaAction.Ignore)
                Else
                    dtgv = dtData.Tables(2).Copy()
                End If

            End If
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub GetDineInData(ByRef ds As DataSet, ByVal BillNO As String, ByVal strSitecode As String, ByVal userid As String, ByRef dtgv As DataTable, Optional ByVal isBatchManagementReq As Boolean = False)
        Try
            Dim strQuery As New Text.StringBuilder()
            Dim daResume As SqlDataAdapter
            Dim dtData As New DataSet
            strQuery.Length = 0
            strQuery.Append("SELECT SITECODE,TERMINALID,TRANSACTIONCODE,BILLNO,BILLDATE,BILLTIME,")
            strQuery.Append("DISCOUNTPERCENTAGE, CLPNO, isNull(NETAMT,0) as NETAMT,isNull( COSTAMT,0) as COSTAMT, isNull(GROSSAMT,0) as GROSSAMT, PAYMENTAMT, isNull(DISCOUNTAMT,0) as DISCOUNTAMT, ")
            strQuery.Append("CLPDISCOUNT,isNull(TOTALDISCOUNT,0) as TOTALDISCOUNT, TOTALITEMS, ROUNDEDAMT, LINEITEMS, CREDITNOTENO, ")
            strQuery.Append("TRANSCURRENCY, CLPSCHEME, RETRIEVEDFROMCUSTNAME, POSTINGSTATUS, TAXAMOUNT, ")
            strQuery.Append("SERVICETAXAMOUNT, SALESEXECUTIVECODE, NOOFREPRINTS, REPRINTREASON, ")
            strQuery.Append("REPRINTDATE, REPRINTTIME, DELETIONDATE, DELETIONTIME, BALANCEPOINTS ,DeliveryPersonID, CustomerNO ")
            strQuery.Append("FROM DinInCashMemoHdr WHERE SITECODE='" & strSitecode & "' AND BILLNO='" & BillNO & "';" & vbCrLf)
            'daResume = New SqlDataAdapter(strQuery.ToString(), ConString)
            'daResume.Fill(dtData)


            'strQuery.Length = 0
            If isBatchManagementReq Then
                strQuery.Append("SELECT A.SITECODE,BILLLINENO,BTYPE,BILLNO,A.EAN,ISNULL(F.TotalPhysicalSaleableQty ,0) AS STOCK,A.BatchBarcode,A.ARTICLECODE,B.ARTICLENAME AS DISCRIPTION,SALESSTAFFID,")
            Else
                strQuery.Append("SELECT A.SITECODE,BILLLINENO,BTYPE,BILLNO,A.EAN,ISNULL(F.TotalPhysicalSaleableQty ,0) AS STOCK,A.BatchBarcode,A.ARTICLECODE,B.ARTICLENAME AS DISCRIPTION,SALESSTAFFID,")
            End If
            'strQuery.Append("SELECT A.SITECODE,BILLLINENO,BTYPE,BILLNO,A.EAN,ISNULL(F.QtyAllocated ,0) AS STOCK,A.BatchBarcode,A.ARTICLECODE,B.ARTICLENAME AS DISCRIPTION,SALESSTAFFID,")
            strQuery.Append("QUANTITY,A.SELLINGPRICE,A.SELLINGPRICE As MRP,TRANSACTIONSTATUS,A.COSTPRICE,BILLDATE,BILLTIME, ")
            strQuery.Append("TOTALDISCOUNT,TOTALDISCPERCENTAGE,GROSSAMT,NETAMOUNT,CLPRequire,PROMOTIONID,NETSELLINGPRICE,SECTION,SHELF,TRANSACTIONCODE, ")
            strQuery.Append("SCALEITEM,RETURNNOSALE,SERIALNBR,LINEDISCOUNT,POSTINGSTATUS,CLPDISCOUNT, ")
            strQuery.Append("UNITOFMEASURE,Isnull(TOTALTAXAMOUNT,0) As TOTALTAXAMOUNT,IFBNO,SALESRETURNREASON,RETURNCMNO, ")
            strQuery.Append("RETURNCMDATE,RETURNQTY ")
            If isBatchManagementReq Then
                strQuery.Append("FROM DineInCashMemoDtl A INNER JOIN MSTARTICLE B ON A.ARTICLECODE=B.ARTICLECODE LEFT OUTER JOIN ArticleBatchBinStockBalances F  ON A.SITECODE=F.SITECODE And F.BatchBarcode = A.BatchBarcode where A.SITECODE='" & strSitecode & "' AND A.BILLNO='" & BillNO & "';" & vbCrLf)
            Else
                strQuery.Append("FROM DineInCashMemoDtl A INNER JOIN MSTARTICLE B ON A.ARTICLECODE=B.ARTICLECODE LEFT OUTER JOIN ARTICLESTOCKBALANCES F  ON A.SITECODE=F.SITECODE And A.ARTICLECODE=F.ARTICLECODE AND A.EAN=F.EAN where A.SITECODE='" & strSitecode & "' AND A.BILLNO='" & BillNO & "';" & vbCrLf)
            End If
            'strQuery.Append("FROM HOLDCASHMEMODTL A INNER JOIN MSTARTICLE B ON A.ARTICLECODE=B.ARTICLECODE LEFT OUTER JOIN BatchDtl F  ON A.SITECODE=F.SITECODE And F.BatchBarcode = A.BatchBarcode where A.SITECODE='" & strSitecode & "' AND A.BILLNO='" & BillNO & "';" & vbCrLf)

            strQuery.Append(" Select * from DineInVoucher where Sitecode='" & strSitecode & "' AND ISSUEDDOCNUMBER='" & BillNO & "';" & vbCrLf)

            daResume = New SqlDataAdapter(strQuery.ToString(), ConString)
            'dtData = New DataTable
            daResume.Fill(dtData)
            ds.Tables("CASHMEMOHDR").Merge(dtData.Tables(0), False, MissingSchemaAction.Ignore)
            ds.Tables("CASHMEMODTL").Merge(dtData.Tables(1), False, MissingSchemaAction.Ignore)
            If Not dtgv Is Nothing Then
                dtgv.Merge(dtData.Tables(2), False, MissingSchemaAction.Ignore)
            Else
                dtgv = dtData.Tables(2).Copy()
            End If


        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Function UpdateReprintStatus(ByVal userid As String, ByVal Reason As String, ByVal sitecode As String, ByVal Trminal As String, ByVal Billno As String, ByVal AuthUserId As String, ByVal AuthUserRemarks As String) As Boolean
        Try
            OpenConnection()
            Dim cmdTrn As New SqlCommand("UPDATE CASHMEMOHDR SET NoofReprints=isnull(NoofReprints,0) + 1, ReprintReason='" & Reason.Replace("'", "") & "', ReprintDate=getdate(),ReprintTime=getdate(), AuthUserId='" & AuthUserId & "', AuthUserRemarks='" & AuthUserRemarks & "', UPDATEDON=GETDATE(),UPDATEDBY='" & userid & "',UPDATEDAT='" & sitecode & "'  WHERE SITECODE='" & sitecode & "' AND   BILLNO='" & Billno & "'", SpectrumCon)
            If CInt(cmdTrn.ExecuteNonQuery()) > 0 Then
                UpdateReprintStatus = True
            End If
            CloseConnection()
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function

    '------For MergeLoad
    Public Sub GetMergeLoadData(ByRef ds As DataSet, ByRef dtgv As DataTable, ByVal MergeId As Int64, ByVal strSitecode As String, terminalid As String, Optional ByVal isBatchManagementReq As Boolean = False)
        Try
            Dim strQuery As New Text.StringBuilder()
            Dim daResume As SqlDataAdapter
            Dim dtData As New DataSet

            strQuery.Length = 0
            strQuery.Append("SELECT  SITECODE," & MergeId & " As BillNo,'" & terminalid & "' As TerminalID, TRANSACTIONCODE,BILLDATE,Max(BILLTIME) As BILLTIME,Sum(Isnull(DISCOUNTPERCENTAGE,0)) As DISCOUNTPERCENTAGE, CLPNO,  ")
            strQuery.Append("Sum(Isnull(NETAMT,0)) As NETAMT,Sum(Isnull(COSTAMT,0)) As COSTAMT, Sum(Isnull(GROSSAMT,0)) As GROSSAMT, Sum(Isnull(PAYMENTAMT,0)) As PAYMENTAMT, Sum(Isnull(DISCOUNTAMT,0)) As DISCOUNTAMT, Sum(Isnull(CLPDISCOUNT,0)) As CLPDISCOUNT, Sum(Isnull(TOTALDISCOUNT,0)) As TOTALDISCOUNT, Sum(Isnull(TOTALITEMS,0)) As TOTALITEMS, Sum(Isnull(ROUNDEDAMT,0)) As ROUNDEDAMT,")
            strQuery.Append("LINEITEMS, CREDITNOTENO, TRANSCURRENCY, CLPSCHEME, RETRIEVEDFROMCUSTNAME, POSTINGSTATUS, TAXAMOUNT, SERVICETAXAMOUNT,  ")
            strQuery.Append("SALESEXECUTIVECODE, NOOFREPRINTS, REPRINTREASON, REPRINTDATE, REPRINTTIME, DELETIONDATE, DELETIONTIME, BALANCEPOINTS ,DeliveryPersonID, CustomerNO  ")
            strQuery.Append("FROM DinInCashMemoHdr  ")
            strQuery.Append(" WHERE SITECODE='" & strSitecode & "' AND BILLNO IN (SELECT  top 1  BillNo FROM DineInMergeOrdersDtl Where MergeId =" & MergeId & ")")
            strQuery.Append("Group by SITECODE,TRANSACTIONCODE,BILLDATE, CLPNO,LINEITEMS, CREDITNOTENO, TRANSCURRENCY, CLPSCHEME, RETRIEVEDFROMCUSTNAME, POSTINGSTATUS, TAXAMOUNT, SERVICETAXAMOUNT, ")
            strQuery.Append("SALESEXECUTIVECODE, NOOFREPRINTS, REPRINTREASON, REPRINTDATE, REPRINTTIME, DELETIONDATE, DELETIONTIME, BALANCEPOINTS ,DeliveryPersonID, CustomerNO ;" & vbCrLf)


            If isBatchManagementReq Then
                strQuery.Append("SELECT Distinct A.SITECODE, " & MergeId & " As BillNo,ROW_NUMBER() OVER(ORDER BY A.EAN DESC) AS BillLineNO  ,BTYPE,A.EAN,(ISNULL(F.TotalPhysicalSaleableQty ,0))AS STOCK,A.ARTICLECODE,B.ARTICLENAME AS DISCRIPTION,SALESSTAFFID,")
            Else
                strQuery.Append("SELECT Distinct A.SITECODE," & MergeId & " As BillNo,ROW_NUMBER() OVER(ORDER BY A.EAN DESC) AS BillLineNO  ,BTYPE,A.EAN,(ISNULL(F.TotalPhysicalSaleableQty ,0))AS STOCK,A.ARTICLECODE,B.ARTICLENAME AS DISCRIPTION,SALESSTAFFID,")
            End If
            strQuery.Append("Sum(Isnull(QUANTITY,0)) As QUANTITY,A.SELLINGPRICE,(A.SELLINGPRICE) As MRP,TRANSACTIONSTATUS,A.COSTPRICE,BILLDATE,Max(BILLTIME) As BILLTIME,")
            strQuery.Append("Sum(Isnull(TOTALDISCOUNT,0)) As TOTALDISCOUNT,Sum(Isnull(TOTALDISCPERCENTAGE,0)) as TOTALDISCPERCENTAGE,Sum(Isnull(GROSSAMT,0)) as GROSSAMT,Sum(Isnull(NETAMOUNT,0)) As NETAMOUNT, ")
            strQuery.Append("CLPRequire,PROMOTIONID,NETSELLINGPRICE,SECTION,SHELF,TRANSACTIONCODE,SCALEITEM,RETURNNOSALE,SERIALNBR,Sum(Isnull(LINEDISCOUNT,0)) AS LINEDISCOUNT,POSTINGSTATUS,Sum(Isnull(CLPDISCOUNT,0)) As CLPDISCOUNT, UNITOFMEASURE,Isnull(TOTALTAXAMOUNT,0) As TOTALTAXAMOUNT,IFBNO,SALESRETURNREASON,RETURNCMNO, RETURNCMDATE,Sum(Isnull(RETURNQTY ,0)) ")
            If isBatchManagementReq Then
                strQuery.Append("FROM DineInCashMemoDtl A INNER JOIN MSTARTICLE B ON A.ARTICLECODE=B.ARTICLECODE LEFT OUTER JOIN ArticleBatchBinStockBalances F  ON A.SITECODE=F.SITECODE And F.BatchBarcode = A.BatchBarcode where A.SITECODE='" & strSitecode & "' AND A.BILLNO IN (SELECT BillNo FROM DineInMergeOrdersDtl Where MergeId =" & MergeId & ")")
            Else
                strQuery.Append("FROM DineInCashMemoDtl A INNER JOIN MSTARTICLE B ON A.ARTICLECODE=B.ARTICLECODE LEFT OUTER JOIN ARTICLESTOCKBALANCES F  ON A.SITECODE=F.SITECODE And A.ARTICLECODE=F.ARTICLECODE AND A.EAN=F.EAN where A.SITECODE='" & strSitecode & "' AND A.BILLNO IN (SELECT BillNo FROM DineInMergeOrdersDtl Where MergeId =" & MergeId & ")")
            End If
            strQuery.Append("GROUP BY A.SITECODE,BTYPE,A.EAN,A.ARTICLECODE,B.ARTICLENAME,SALESSTAFFID,A.SELLINGPRICE,TRANSACTIONSTATUS,A.COSTPRICE,BILLDATE,TOTALTAXAMOUNT,F.TotalPhysicalSaleableQty,CLPRequire,PROMOTIONID,")
            strQuery.Append("NETSELLINGPRICE,SECTION,SHELF,TRANSACTIONCODE,SCALEITEM,RETURNNOSALE,SERIALNBR,LINEDISCOUNT,POSTINGSTATUS, CLPDISCOUNT, UNITOFMEASURE, TOTALTAXAMOUNT,IFBNO,SALESRETURNREASON,RETURNCMNO, RETURNCMDATE ;" & vbCrLf)

            strQuery.Append(" Select * from DineInVoucher where Sitecode='" & strSitecode & "' AND ISSUEDDOCNUMBER IN (SELECT BillNo FROM DineInMergeOrdersDtl Where MergeId =" & MergeId & ");" & vbCrLf)

            daResume = New SqlDataAdapter(strQuery.ToString(), ConString)
            daResume.Fill(dtData)
            ds.Tables("CASHMEMOHDR").Merge(dtData.Tables(0), False, MissingSchemaAction.Ignore)
            ds.Tables("CASHMEMODTL").Merge(dtData.Tables(1), False, MissingSchemaAction.Ignore)
            If Not dtgv Is Nothing Then
                dtgv.Merge(dtData.Tables(2), False, MissingSchemaAction.Ignore)
            Else
                dtgv = dtData.Tables(2).Copy()
            End If


        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' update Hold Data Status
    ''' </summary>
    ''' <param name="billno">CashMemo Number</param>
    ''' <param name="strSitecode">SiteCode</param>
    ''' <param name="Status">Status</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Function UpdateHoldStatus(ByVal userid As String, ByVal billno As String, ByVal strSitecode As String, ByVal Status As String) As Boolean
        Try
            Dim cmdTrn As New SqlCommand("Update HOLDCASHMEMOHDR set BILLINTERMEDIATESTATUS='" & Status & "',UPDATEDON=GETDATE(),UPDATEDBY='" & userid & "',UPDATEDAT='" & strSitecode & "'   WHERE SITECODE='" & strSitecode & "' AND BillNO='" & billno & "' ", SpectrumCon)
            OpenConnection()
            If CInt(cmdTrn.ExecuteNonQuery()) > 0 Then
                UpdateHoldStatus = True
            End If
            CloseConnection()
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Delete the HoldBill 
    ''' </summary>
    ''' <param name="billno"></param>
    ''' <param name="strSitecode"></param>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteHoldBill(ByVal billno As String, ByVal strSitecode As String, ByVal Status As String) As Boolean
        Try
            Dim cmdTrn As New SqlCommand("Delete from HOLDCASHMEMOHDR WHERE SITECODE='" & strSitecode & "' AND BillNO='" & billno & "' ", SpectrumCon)
            OpenConnection()
            If CInt(cmdTrn.ExecuteNonQuery()) = 0 Then
                DeleteHoldBill = False
            End If
            cmdTrn.CommandText = "Delete from HOLDCASHMEMODTL WHERE SITECODE='" & strSitecode & "' AND BillNO='" & billno & "' "
            If CInt(cmdTrn.ExecuteNonQuery()) = 0 Then
                DeleteHoldBill = False
            End If
            cmdTrn.CommandText = "Delete from HoldCashMemoMettler WHERE BillNO='" & billno & "' "
            If CInt(cmdTrn.ExecuteNonQuery()) = 0 Then
                DeleteHoldBill = False
            End If
            CloseConnection()
            DeleteHoldBill = True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Get the hold data List
    ''' </summary>
    ''' <param name="StrSiteCode">SiteCode</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetListofHoldData(ByVal StrSiteCode As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim daResume As New SqlDataAdapter("SELECT RETRIEVEDFROMCUSTNAME AS CUSTOMER, BILLNO,convert(varchar(12),BILLDATE,101) AS BILLDATE,NETAMT AS NETAMOUNT FROM HOLDCASHMEMOHDR  WHERE BILLINTERMEDIATESTATUS='HOLD' AND SiteCode='" & StrSiteCode & "'", ConString)
            daResume.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Check the hold data
    ''' </summary>
    ''' <param name="StrSiteCode">SiteCode</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function CheckHoldBillData(ByVal StrSiteCode As String) As Boolean
        Try
            Dim dt As New DataTable
            Dim daResume As New SqlDataAdapter("SELECT TOP 1 BILLNO FROM HOLDCASHMEMOHDR  WHERE BILLINTERMEDIATESTATUS='HOLD' AND SiteCode='" & StrSiteCode & "'", ConString)
            daResume.Fill(dt)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ' ''' <summary>
    ' ''' Get CashMemo Number
    ' ''' </summary>
    ' ''' <param name="SiteCode">SiteCode</param>
    ' ''' <returns>DataTable</returns>
    ' ''' <remarks></remarks>
    'Public Function GetOldCashMemo(ByVal SiteCode As String, Optional ByVal DayOpen As DateTime = Nothing, Optional ByVal TerminalId As String = Nothing, Optional ByVal cashiername As String = Nothing, Optional ByVal shiftid As String = Nothing, Optional ByVal IsReturnCashMemo As Boolean = False, Optional ByVal NoOfDaysForReprint As String = "1") As DataTable
    '    Try
    '        ' chnage to filter the mstender based on sitecode
    '        'Dim da As New SqlDataAdapter("SELECT A.CREATEDBY AS UserID,C.USERNAME as [User Name],A.TERMINALID as [Terminal Id] ,A.BILLNO as [Cash Memo No] ,A.BILLDATE AS Date,D.TenderHeadName AS [Receipt Type],Convert(numeric(18,2),B.AMOUNTTENDERED) as [Receipt Amt]" & _
    '        '        " FROM CASHMEMOHDR A LEFT OUTER JOIN CASHMEMORECEIPT B ON A.SITECODE=B.SITECODE AND A.BILLNO=B.BILLNO" & _
    '        '        " LEFT OUTER JOIN AUTHUSERS C ON A.SITECODE=C.SITECODE AND A.CREATEDBY=C.USERID  LEFT OUTER JOIN MSTTENDER D ON B.TENDERTYPECODE=D.TENDERHEADCODE   " & _
    '        '        " WHERE  A.BILLINTERMEDIATESTATUS<>'Deleted' AND A.SITECODE='" & SiteCode & "'", ConString)
    '        '   Dim str As String = "SELECT A.BILLNO as [Cash Memo No] ,A.CREATEDBY AS UserID,C.USERNAME as [User Name],A.TERMINALID as [Terminal Id] ,convert(varchar(12),A.BILLDATE,101) AS Date,D.TenderHeadName AS [Receipt Type],Convert(numeric(18,2),B.AMOUNTTENDERED) as [Receipt Amt],A.CLPNo " & _
    '        '  " FROM CASHMEMOHDR A LEFT OUTER JOIN CASHMEMORECEIPT B ON A.SITECODE=B.SITECODE AND A.BILLNO=B.BILLNO" & _
    '        '" LEFT OUTER JOIN AUTHUSERS C ON A.SITECODE=C.SITECODE AND A.CREATEDBY=C.USERID  LEFT OUTER JOIN MSTTENDER D ON B.TENDERHEADCODE=D.TENDERHEADCODE AND A.SITECODE=D.SITECODE  " & _
    '        '" WHERE  A.BILLINTERMEDIATESTATUS<>'Deleted' AND A.SITECODE='" & SiteCode & "'"

    '        'Dim str As String = "SELECT [Cash Memo No] ,[UserID],[USER Name],[Terminal Id],[Date],Substring([Reciept Type],1,case LEN([Reciept Type]) when  0 then 0 else LEN([Reciept Type]) -1 end ) as [Reciept Type],[Receipt Amt],[CLPNo],[UpdatedOn] 	" & _
    '        '                    " From ( SELECT A.BILLNO as [Cash Memo No] ,A.CREATEDBY AS UserID,C.USERNAME as [User Name],A.TERMINALID as [Terminal Id] ," & _
    '        '                    "             (SELECT Stuff(D.TENDERHEADCODE,1,0,'')+ ', '" & _
    '        '                    "              FROM CASHMEMOHDR AI LEFT OUTER JOIN CASHMEMORECEIPT B ON AI.SITECODE=B.SITECODE AND AI.BILLNO=B.BILLNO and B.status=1" & _
    '        '                    "                    LEFT OUTER JOIN MSTTENDER D ON B.TENDERHEADCODE =D.TENDERHEADCODE AND AI.SITECODE=D.SITECODE   " & _
    '        '                    "              WHERE AI.BILLINTERMEDIATESTATUS<>'Deleted' AND AI.SITECODE='" & SiteCode & "'  and a.BILLNO = AI.BillNo" & _
    '        '                    "             FOR XML PATH ('')) as [Reciept Type],convert(varchar(12),A.BILLDATE,101) AS Date," & _
    '        '                    "             sum(Convert(numeric(18,2),(B.AMOUNTTENDERED))) as [Receipt Amt],A.CLPNo,A.UpdatedOn  " & _
    '        '                    "  FROM CASHMEMOHDR A LEFT OUTER JOIN CASHMEMORECEIPT B ON A.SITECODE=B.SITECODE AND A.BILLNO=B.BILLNO and B.status=1" & _
    '        '                    "        LEFT OUTER JOIN AUTHUSERS C ON A.SITECODE=C.SITECODE AND A.CREATEDBY=C.USERID  " & _
    '        '                    "        LEFT OUTER JOIN MSTTENDER D ON B.TENDERHEADCODE =D.TENDERHEADCODE AND A.SITECODE=D.SITECODE   "
    '        Dim str As String = "SELECT [Cash Memo No] ,[UserID],[USER Name],[Terminal Id],[Date],Substring([Reciept Type],1,case LEN([Reciept Type]) when  0 then 0 else LEN([Reciept Type]) -1 end ) as [Reciept Type],[Receipt Amt],[CLPNo],[CustomerName],[ADDRESS],[UpdatedOn] 	" & _
    '                            " From ( SELECT A.BILLNO as [Cash Memo No] ,A.CREATEDBY AS UserID,C.USERNAME as [User Name],A.TERMINALID as [Terminal Id] ," & _
    '                            "             (SELECT Stuff(D.TENDERHEADCODE,1,0,'')+ ', '" & _
    '                            "              FROM CASHMEMOHDR AI LEFT OUTER JOIN CASHMEMORECEIPT B ON AI.SITECODE=B.SITECODE AND AI.BILLNO=B.BILLNO and B.status=1" & _
    '                            "                    LEFT OUTER JOIN MSTTENDER D ON B.TENDERHEADCODE =D.TENDERHEADCODE AND AI.SITECODE=D.SITECODE   " & _
    '                            "              WHERE AI.BILLINTERMEDIATESTATUS<>'Deleted' AND AI.SITECODE='" & SiteCode & "'  and a.BILLNO = AI.BillNo" & _
    '                            "             FOR XML PATH ('')) as [Reciept Type],convert(varchar(12),A.BILLDATE,101) AS Date," & _
    '                            "             sum(Convert(numeric(18,2),(B.AMOUNTTENDERED))) as [Receipt Amt],A.CLPNo," & _
    '                            "CLP.NameOnCard As [CustomerName]," & _
    '                            "ISNULL(CCAD.ADDRESSLN1,'') + ' ' + ISNULL(CCAD.ADDRESSLN2,'') + ' ' + ISNULL(CCAD.ADDRESSLN3,'') + ' ' + ISNULL(CCAD.ADDRESSLN4,'') AS [ADDRESS],A.UpdatedOn " & _
    '                            "  FROM CASHMEMOHDR A " & _
    '                            "        Left JOIN  CLPCustomers CLP On A.ClpNo= CLP.CardNo   " & _
    '                            "        Left JOIN  CLPCustomerAddress AS CCAD On CLP.CARDNO=CCAD.CARDNO AND CLP.CLPPROGRAMID=CCAD.CLPPROGRAMID AND CCAD.Defaults=1 and CCAD.DefaultAddress=1 and CCAD.STATUS=1 " & _
    '                            "        LEFT OUTER JOIN CASHMEMORECEIPT B ON A.SITECODE=B.SITECODE AND A.BILLNO=B.BILLNO and B.status=1" & _
    '                            "        LEFT OUTER JOIN AUTHUSERS C ON A.SITECODE=C.SITECODE AND A.CREATEDBY=C.USERID  " & _
    '                            "        LEFT OUTER JOIN MSTTENDER D ON B.TENDERHEADCODE =D.TENDERHEADCODE AND A.SITECODE=D.SITECODE   "

    '        If IsReturnCashMemo Then
    '            str += "  INNER JOIN (SELECT  Distinct CMD.SiteCode , CMD.BillNo FROM CASHMEMODTL AS CMD WHERE CMD.BTYPE <> 'R' AND CMD.QUANTITY-ISNULL(CMD.RETURNQTY,0)>0) "
    '            str += "  AS CMD ON A.SITECODE = CMD.SiteCode AND  A.BillNo = CMD.BillNo"
    '        End If
    '        '------issue no 0013521 shiftwise change tender
    '        If ShiftManagementForCM Then
    '            str = str & "left outer join ShiftOpenClose as  shift on shift.TerminalId =A.TerminalID "
    '        End If
    '        str = str & "  WHERE  A.BILLINTERMEDIATESTATUS<>'Deleted' AND A.SITECODE='" & SiteCode & "'"
    '        str = str & "  and A.BillDate between getDate() - " & NoOfDaysForReprint & " and getDate() "
    '        If TerminalId <> Nothing Then
    '            str = str & " AND A.TerminalID ='" & TerminalId & "'"
    '        End If
    '        '------issue no 0013521 shiftwise change tender
    '        If ShiftManagementForCM Then
    '            str = str & " AND shift.ShiftId='" & shiftid & "'"
    '            str = str & " And A.CREATEDON >= shift.CREATEDON "
    '            str = str & " And A.CreatedBy='" & cashiername & "'"
    '            str = str & " And shift.OpenDate= @DayOpen"
    '            str = str & " And Shift.OpenCloseStatus= 'Open'"
    '        End If
    '        If DayOpen <> Nothing Then
    '            ' this to ensure bill of only current day or bill can be modified only till before day close
    '            str = str & " AND convert(Varchar(10),A.BillDate,101) = ( select convert(Varchar(10),opendate,101) from dayopennclose where  SiteCode = '" & SiteCode & "' And  Status = 1 AND  DAYCLOSESTATUS=0)"
    '            'str = str & " AND A.BillDate > '" & Format(DayOpen, "yyyy-MM-dd hh:mm:ss.fff tt") & "'"
    '        End If
    '        str = str & "  Group By A.BILLNO,A.CREATEDBY,C.USERNAME ,A.TERMINALID , A.CLPNo ,A.BILLDATE,CLP.NameOnCard,ISNULL(CCAD.ADDRESSLN1,'') + ' ' + ISNULL(CCAD.ADDRESSLN2,'') + ' ' + ISNULL(CCAD.ADDRESSLN3,'') + ' ' + ISNULL(CCAD.ADDRESSLN4,''),A.UpdatedOn) As CashMemoTender "

    '        str = str & " ORDER BY UpdatedOn Desc"

    '        Dim dt As New DataTable
    '        Dim cmd As New SqlCommand(str.ToString, SpectrumCon())
    '        cmd.Parameters.Add("@DayOpen", SqlDbType.Date)
    '        cmd.Parameters("@DayOpen").Value = DayOpen
    '        Dim da As New SqlDataAdapter(cmd)
    '        da.Fill(dt)
    '        Return dt
    '        'Dim da As New SqlDataAdapter(str, ConString)

    '        'Dim dt As New DataTable
    '        'da.Fill(dt)
    '        'Return dt
    '    Catch ex As Exception
    '        LogException(ex)
    '        Return Nothing
    '    End Try
    'End Function

    Public Function GetOldCashMemo(ByVal SiteCode As String, Optional ByVal DayOpen As DateTime = Nothing, Optional ByVal TerminalId As String = Nothing, Optional ByVal cashiername As String = Nothing, Optional ByVal shiftid As String = Nothing, Optional ByVal IsReturnCashMemo As Boolean = False, Optional ByVal NoOfDaysForReprint As String = "1", Optional ByVal NewCustmor As String = "0") As DataTable
        Try
            ' chnage to filter the mstender based on sitecode
            'Dim da As New SqlDataAdapter("SELECT A.CREATEDBY AS UserID,C.USERNAME as [User Name],A.TERMINALID as [Terminal Id] ,A.BILLNO as [Cash Memo No] ,A.BILLDATE AS Date,D.TenderHeadName AS [Receipt Type],Convert(numeric(18,2),B.AMOUNTTENDERED) as [Receipt Amt]" & _
            '        " FROM CASHMEMOHDR A LEFT OUTER JOIN CASHMEMORECEIPT B ON A.SITECODE=B.SITECODE AND A.BILLNO=B.BILLNO" & _
            '        " LEFT OUTER JOIN AUTHUSERS C ON A.SITECODE=C.SITECODE AND A.CREATEDBY=C.USERID  LEFT OUTER JOIN MSTTENDER D ON B.TENDERTYPECODE=D.TENDERHEADCODE   " & _
            '        " WHERE  A.BILLINTERMEDIATESTATUS<>'Deleted' AND A.SITECODE='" & SiteCode & "'", ConString)
            '   Dim str As String = "SELECT A.BILLNO as [Cash Memo No] ,A.CREATEDBY AS UserID,C.USERNAME as [User Name],A.TERMINALID as [Terminal Id] ,convert(varchar(12),A.BILLDATE,101) AS Date,D.TenderHeadName AS [Receipt Type],Convert(numeric(18,2),B.AMOUNTTENDERED) as [Receipt Amt],A.CLPNo " & _
            '  " FROM CASHMEMOHDR A LEFT OUTER JOIN CASHMEMORECEIPT B ON A.SITECODE=B.SITECODE AND A.BILLNO=B.BILLNO" & _
            '" LEFT OUTER JOIN AUTHUSERS C ON A.SITECODE=C.SITECODE AND A.CREATEDBY=C.USERID  LEFT OUTER JOIN MSTTENDER D ON B.TENDERHEADCODE=D.TENDERHEADCODE AND A.SITECODE=D.SITECODE  " & _
            '" WHERE  A.BILLINTERMEDIATESTATUS<>'Deleted' AND A.SITECODE='" & SiteCode & "'"

            Dim str As String = "SELECT [Cash Memo No] ,[UserID],[USER Name],[Terminal Id],[Date],Substring([Reciept Type],1,case LEN([Reciept Type]) when  0 then 0 else LEN([Reciept Type]) -1 end ) as [Reciept Type],[Receipt Amt],[CLPNo],[CustomerName],[ADDRESS],[UpdatedOn] 	" & _
                                " From ( SELECT A.BILLNO as [Cash Memo No] ,A.CREATEDBY AS UserID,C.USERNAME as [User Name],A.TERMINALID as [Terminal Id] ," & _
                                "             (SELECT Stuff(D.TENDERHEADCODE,1,0,'')+ ', '" & _
                                "              FROM CASHMEMOHDR AI LEFT OUTER JOIN CASHMEMORECEIPT B ON AI.SITECODE=B.SITECODE AND AI.BILLNO=B.BILLNO and B.status=1" & _
                                "                    LEFT OUTER JOIN MSTTENDER D ON B.TENDERHEADCODE =D.TENDERHEADCODE AND AI.SITECODE=D.SITECODE   " & _
                                "              WHERE AI.BILLINTERMEDIATESTATUS<>'Deleted' AND AI.SITECODE='" & SiteCode & "'  and a.BILLNO = AI.BillNo" & _
                                "             FOR XML PATH ('')) as [Reciept Type],convert(varchar(12),A.BILLDATE,101) AS Date," & _
                                "             sum(Convert(numeric(18,2),(B.AMOUNTTENDERED))) as [Receipt Amt],A.CLPNo," & _
                                "CLP.NameOnCard As [CustomerName]," & _
                                "ISNULL(CCAD.ADDRESSLN1,'') + ' ' + ISNULL(CCAD.ADDRESSLN2,'') + ' ' + ISNULL(CCAD.ADDRESSLN3,'') + ' ' + ISNULL(CCAD.ADDRESSLN4,'') AS [ADDRESS],A.UpdatedOn " & _
                                "  FROM CASHMEMOHDR A " & _
                                "        Left JOIN  CLPCustomers CLP On A.ClpNo= CLP.CardNo   " & _
                                "        Left JOIN  CLPCustomerAddress AS CCAD On CLP.CARDNO=CCAD.CARDNO AND CLP.CLPPROGRAMID=CCAD.CLPPROGRAMID AND CCAD.Defaults=1"
            If NewCustmor = "1" Then
                str += "and CCAD.DefaultAddress=1 "
            End If

            str += "   and CCAD.STATUS=1   LEFT OUTER JOIN CASHMEMORECEIPT B ON A.SITECODE=B.SITECODE AND A.BILLNO=B.BILLNO and B.status=1"
            str += "   LEFT OUTER JOIN AUTHUSERS C ON A.SITECODE=C.SITECODE AND A.CREATEDBY=C.USERID  "
            str += "   LEFT OUTER JOIN MSTTENDER D ON B.TENDERHEADCODE =D.TENDERHEADCODE AND A.SITECODE=D.SITECODE   "

            If IsReturnCashMemo Then
                str += "  INNER JOIN (SELECT  Distinct CMD.SiteCode , CMD.BillNo FROM CASHMEMODTL AS CMD WHERE CMD.BTYPE <> 'R' AND CMD.QUANTITY-ISNULL(CMD.RETURNQTY,0)>0) "
                str += "  AS CMD ON A.SITECODE = CMD.SiteCode AND  A.BillNo = CMD.BillNo"
            End If
            '------issue no 0013521 shiftwise change tender
            If ShiftManagementForCM Then
                str = str & "left outer join ShiftOpenClose as  shift on shift.TerminalId =A.TerminalID "
            End If
            str = str & "  WHERE  A.BILLINTERMEDIATESTATUS<>'Deleted' AND A.SITECODE='" & SiteCode & "'"
            str = str & "  and A.BillDate between getDate() - " & NoOfDaysForReprint & " and getDate() "
            If TerminalId <> Nothing Then
                str = str & " AND A.TerminalID ='" & TerminalId & "'"
            End If
            '------issue no 0013521 shiftwise change tender
            If ShiftManagementForCM Then
                str = str & " AND shift.ShiftId='" & shiftid & "'"
                str = str & " And A.CREATEDON >= shift.CREATEDON "
                str = str & " And A.CreatedBy='" & cashiername & "'"
                str = str & " And shift.OpenDate= @DayOpen"
                str = str & " And Shift.OpenCloseStatus= 'Open'"
            End If
            If DayOpen <> Nothing Then
                ' this to ensure bill of only current day or bill can be modified only till before day close
                str = str & " AND convert(Varchar(10),A.BillDate,101) = ( select convert(Varchar(10),opendate,101) from dayopennclose where  SiteCode = '" & SiteCode & "' And  Status = 1 AND  DAYCLOSESTATUS=0)"
                'str = str & " AND A.BillDate > '" & Format(DayOpen, "yyyy-MM-dd hh:mm:ss.fff tt") & "'"
            End If
            str = str & "  Group By A.BILLNO,A.CREATEDBY,C.USERNAME ,A.TERMINALID , A.CLPNo ,A.BILLDATE,CLP.NameOnCard,ISNULL(CCAD.ADDRESSLN1,'') + ' ' + ISNULL(CCAD.ADDRESSLN2,'') + ' ' + ISNULL(CCAD.ADDRESSLN3,'') + ' ' + ISNULL(CCAD.ADDRESSLN4,''),A.UpdatedOn) As CashMemoTender "

            str = str & " ORDER BY UpdatedOn Desc"

            Dim dt As New DataTable
            Dim cmd As New SqlCommand(str.ToString, SpectrumCon())
            cmd.Parameters.Add("@DayOpen", SqlDbType.Date)
            cmd.Parameters("@DayOpen").Value = DayOpen
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Return dt
            'Dim da As New SqlDataAdapter(str, ConString)

            'Dim dt As New DataTable
            'da.Fill(dt)
            'Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Get List of Manual Promotions
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetManualPromotion(ByVal sitecode As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim daDefault As New SqlDataAdapter("SELECT A.PROMOTIONID,A.PROMOTIONNAME,A.PROMOTIONVALUE,A.DISCPER,A.FIXEDPRICEOFF,A.FIXEDSELLING FROM " & _
            " MANUALPROMOTION A INNER JOIN PROMOTIONSITEMAP B ON 	A.PROMOTIONID=B.OFFERNO AND B.SITECODE='" & sitecode & "' " & _
            " WHERE 	A.OFFERACTIVE=1 	AND A.ISAPPROVED=1 	AND (GETDATE() BETWEEN A.STARTDATE AND A.ENDDATE)  ", ConString)
            daDefault.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Get CashMemo for Print
    ''' </summary>
    ''' <param name="CashMemoNO">CashMemo Number</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetCashMemo(ByVal CashMemoNO As String, ByVal SiteCode As String, ByVal LangCode As String) As DataTable
        Try
            Dim StrSql As String
            'StrSql = "SELECT * FROM CMPRINT WHERE BILLNO='" & CashMemoNO.Trim & "' AND SiteCode='" & SiteCode & "' "
            StrSql = "SELECT * FROM CMPRINT ('" & CashMemoNO.Trim & "','" & SiteCode & "','" & LangCode & "' )"
            Dim dt As DataTable = GetFilledTable(StrSql)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    '''get all tender types by bill no
    ''' </summary>
    ''' <param name="_RefBillNo"></param>
    ''' <returns>datatable</returns>
    ''' <remarks>created by khusrao adil</remarks>
    Public Function GetTenderTypeByRefBillNo(ByVal _RefBillNo As String) As DataTable
        Try
            Dim StrSql As New StringBuilder
            'StrSql = "SELECT * FROM CMPRINT WHERE BILLNO='" & CashMemoNO.Trim & "' AND SiteCode='" & SiteCode & "' "
            StrSql = StrSql.Append("SELECT  CASE WHEN TenderheadCode = 'Credit' THEN 'Credit Sales' WHEN TenderheadCode = 'Cash' THEN 'Cash'")
            StrSql = StrSql.Append(" WHEN TenderheadCode = 'Cheque' THEN 'Cheque' WHEN TenderheadCode = 'Credit Card' THEN 'Credit Card'")
            StrSql = StrSql.Append(" WHEN TenderheadCode = 'Rtgs' THEN 'Rtgs' ELSE TenderheadCode END AS TENDERHEADNAME,")
            StrSql = StrSql.Append("TenderHeadCode,AmountTendered,AmountinCurrency,CurrencyCode,CMRecptLineno,CardNo,TenderTypeCode FROM creditreceipt where RefBillNo='" & _RefBillNo & "' and status=1")
            Dim dt As DataTable = GetFilledTable(StrSql.ToString())
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Check CashMemo in Hold
    ''' </summary>
    ''' <param name="billno">CashMemo Number</param>
    ''' <param name="strSitecode">Sitecode</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Function CheckBillDataInHold(ByVal billno As String, ByVal strSitecode As String) As Boolean
        Try
            CheckBillDataInHold = False
            Dim cmdTrn As New SqlCommand("Select count(Billno) from HOLDCASHMEMOHDR   WHERE SITECODE='" & strSitecode & "' AND BillNO='" & billno & "' ", SpectrumCon)
            OpenConnection()
            If CInt(cmdTrn.ExecuteScalar()) > 0 Then
                CheckBillDataInHold = True
            End If
            CloseConnection()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    ''' <summary>
    ''' Get Hold Data For printing
    ''' </summary>
    ''' <param name="BillNO">CashMemo Number</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetHoldDataForPrint(ByVal BillNO As String, ByVal sitecode As String) As DataTable
        Try
            Dim StrSql As String
            ' StrSql = "SELECT * FROM HOLDCASHMEMO WHERE BILLNO='" & BillNO.Trim & "' AND Sitecode='" & sitecode & "'"
            StrSql = "SELECT  *, Case when len(ISNULL(SITETELEPHONE1,'')) > 0 And len(ISNULL(SITETELEPHONE2,'')) > 0  then ISNULL(SITETELEPHONE1,'') + ',' +ISNULL(SITETELEPHONE2,'') else ISNULL(SITETELEPHONE1,'')+ISNULL(SITETELEPHONE2,'')END  AS TELNO, ISNULL(SITEADDRESSLN1,'') + ISNULL(SITEADDRESSLN2,'') + ISNULL(SITEADDRESSLN3,'') + ISNULL(CONVERT(VARCHAR,SITEPINCODE),'') AS ADDRESS FROM VW_HOLDCASHMEMO hdr  inner join mstsite sit on hdr.Sitecode = sit.Sitecode WHERE hdr.BILLNO='" & BillNO.Trim & "' AND hdr.Sitecode='" & sitecode & "' "
            Dim dt As DataTable = GetFilledTable(StrSql)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
#Region "Dine in"
    Public Function GetDineInHoldDataForPrint(ByVal BillNO As String, ByVal sitecode As String) As DataTable
        Try
            Dim StrSql As String
            ' StrSql = "SELECT * FROM HOLDCASHMEMO WHERE BILLNO='" & BillNO.Trim & "' AND Sitecode='" & sitecode & "'"
            StrSql = "SELECT  *, Case when len(ISNULL(SITETELEPHONE1,'')) > 0 And len(ISNULL(SITETELEPHONE2,'')) > 0  then ISNULL(SITETELEPHONE1,'') + ',' +ISNULL(SITETELEPHONE2,'') else ISNULL(SITETELEPHONE1,'')+ISNULL(SITETELEPHONE2,'')END  AS TELNO, ISNULL(SITEADDRESSLN1,'') + ISNULL(SITEADDRESSLN2,'') + ISNULL(SITEADDRESSLN3,'') + ISNULL(CONVERT(VARCHAR,SITEPINCODE),'') AS ADDRESS FROM VW_DINEINHOLDCASHMEMO hdr  inner join mstsite sit on hdr.Sitecode = sit.Sitecode WHERE hdr.BILLNO='" & BillNO.Trim & "' AND hdr.Sitecode='" & sitecode & "' "
            Dim dt As DataTable = GetFilledTable(StrSql)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetHoldDataForPrintForMerge(ByVal mergeid As Int64, ByVal sitecode As String) As DataTable
        Try
            Dim StrSql As String
            ' StrSql = "SELECT * FROM HOLDCASHMEMO WHERE BILLNO='" & BillNO.Trim & "' AND Sitecode='" & sitecode & "'"
            ' StrSql = "SELECT  *, Case when len(ISNULL(SITETELEPHONE1,'')) > 0 And len(ISNULL(SITETELEPHONE2,'')) > 0  then ISNULL(SITETELEPHONE1,'') + ',' +ISNULL(SITETELEPHONE2,'') else ISNULL(SITETELEPHONE1,'')+ISNULL(SITETELEPHONE2,'')END  AS TELNO, ISNULL(SITEADDRESSLN1,'') + ISNULL(SITEADDRESSLN2,'') + ISNULL(SITEADDRESSLN3,'') + ISNULL(CONVERT(VARCHAR,SITEPINCODE),'') AS ADDRESS FROM HOLDCASHMEMO  hdr  inner join mstsite sit on hdr.Sitecode = sit.Sitecode WHERE hdr.BILLNO IN (SELECT BillNo FROM DineInMergeOrdersDtl Where MergeId =" & mergeid & ") AND hdr.Sitecode='" & sitecode & "' "

            StrSql = "SELECT Distinct A.SITECODE, " & mergeid & " As BillNo,ROW_NUMBER() OVER(ORDER BY A.EAN DESC) AS BillLineNO  ,BTYPE,A.EAN,(ISNULL(F.TotalPhysicalSaleableQty ,0))AS STOCK,A.ARTICLECODE,B.ARTICLENAME AS ArticleName,SALESSTAFFID," & _
            "Sum(Isnull(QUANTITY,0)) As QUANTITY,A.SELLINGPRICE,(A.SELLINGPRICE) As MRP,TRANSACTIONSTATUS,A.COSTPRICE,BILLDATE,Max(BILLTIME) As BILLTIME," & _
            "Sum(Isnull(TOTALDISCOUNT,0)) As TOTALDISCOUNT,Sum(Isnull(TOTALDISCPERCENTAGE,0)) as TOTALDISCPERCENTAGE,Sum(Isnull(GROSSAMT,0)) as GROSSAMT,Sum(Isnull(NETAMOUNT,0)) As NETAMOUNT, " & _
            "CLPRequire,PROMOTIONID,NETSELLINGPRICE,SECTION,SHELF,TRANSACTIONCODE,SCALEITEM,RETURNNOSALE,SERIALNBR,Sum(Isnull(LINEDISCOUNT,0)) AS LINEDISCOUNT,POSTINGSTATUS,Sum(Isnull(CLPDISCOUNT,0)) As CLPDISCOUNT, UNITOFMEASURE,Isnull(TOTALTAXAMOUNT,0) As TOTALTAXAMOUNT,IFBNO,SALESRETURNREASON,RETURNCMNO, RETURNCMDATE,Sum(Isnull(RETURNQTY ,0)), " & _
            "Case when len(ISNULL(SITETELEPHONE1,'')) > 0 And len(ISNULL(SITETELEPHONE2,'')) > 0  then ISNULL(SITETELEPHONE1,'') + ',' +ISNULL(SITETELEPHONE2,'') else ISNULL(SITETELEPHONE1,'')+ISNULL(SITETELEPHONE2,'')END  AS TELNO," & _
            " ISNULL(SITEADDRESSLN1,'') + ISNULL(SITEADDRESSLN2,'') + ISNULL(SITEADDRESSLN3,'') + ISNULL(CONVERT(VARCHAR,SITEPINCODE),'') AS ADDRESS , SiteOfficialName " & _
            "FROM DineInCashMemoDtl A INNER JOIN MSTARTICLE B ON A.ARTICLECODE=B.ARTICLECODE LEFT OUTER JOIN ARTICLESTOCKBALANCES F  ON A.SITECODE=F.SITECODE And A.ARTICLECODE=F.ARTICLECODE AND A.EAN=F.EAN  inner join mstsite  sit on A.Sitecode = sit.Sitecode where A.SITECODE='" & sitecode & "' AND A.BILLNO IN (SELECT BillNo FROM DineInMergeOrdersDtl Where MergeId =" & mergeid & ")" & _
            "GROUP BY A.SITECODE,BTYPE,A.EAN,A.ARTICLECODE,B.ARTICLENAME,SALESSTAFFID,A.SELLINGPRICE,TRANSACTIONSTATUS,A.COSTPRICE,BILLDATE,TOTALTAXAMOUNT,F.TotalPhysicalSaleableQty,CLPRequire,PROMOTIONID," & _
            "NETSELLINGPRICE,SECTION,SHELF,TRANSACTIONCODE,SCALEITEM,RETURNNOSALE,SERIALNBR,LINEDISCOUNT,POSTINGSTATUS, CLPDISCOUNT, UNITOFMEASURE, TOTALTAXAMOUNT,IFBNO,SALESRETURNREASON,RETURNCMNO, RETURNCMDATE," & _
            "SiteOfficialName,SITETELEPHONE1,SITETELEPHONE2,SITEADDRESSLN1,SITEADDRESSLN2,SITEADDRESSLN3,SITEPINCODE"
            Dim dt As DataTable = GetFilledTable(StrSql)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetMergeBillNo(ByVal mergeid As Int64) As DataTable
        Try
            Dim StrSql As String
            StrSql = "Select OD.DineInNumber As TableNo,DM.BillNo As BillNo from DineInMergeOrdersDtl As DM Left Join OrderDineInMap As OD On Dm.BillNo  = OD.BillNo  Where MergeId =" & mergeid & " Order by OD.DineInNumber"
            Dim dt As DataTable = GetFilledTable(StrSql)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


    'Public Function GetTableNumber(ByVal sitecode As String, ByVal dayopendate As Date, Optional ByVal SeatingAreaId As String = Nothing) As DataTable
    '    Try
    '        Dim StrSql As String
    '        ',ISNULL(ODM.BillNo,'') as BillNo
    '        StrSql = " SELECT Distinct Dine.DineInNumber ,ISNULL(ODM.status,0) as Status " & _
    '                 " FROM	MstDineIn  Dine left outer join  " & _
    '                 " OrderDineInMap  ODM on Dine.DineInNumber = ODM.DineInNumber AND ODM.status = 1 AND BillDate = @dayopendate " & _
    '                 " WHERE Dine.Status=1 and Dine.sitecode='" & sitecode & "'  "
    '        If Not String.IsNullOrEmpty(SeatingAreaId) Then
    '            StrSql = StrSql & "  and Dine.SeatingAreaId='" & SeatingAreaId & "' "
    '        End If
    '        Dim cmdTrn As New SqlCommand(StrSql, SpectrumCon)
    '        cmdTrn.Parameters.Add("@dayopendate", SqlDbType.Date)
    '        cmdTrn.Parameters("@dayopendate").Value = dayopendate

    '        Dim da As New SqlDataAdapter(cmdTrn)

    '        Dim dt As New DataTable
    '        da.Fill(dt)
    '        Return dt

    '    Catch ex As Exception
    '        LogException(ex)
    '        Return Nothing
    '    End Try
    'End Function

    Public Function GetTableNumber(ByVal sitecode As String, ByVal dayopendate As Date, ByVal LockTime As Integer, ByVal CustStayTime As Integer, Optional ByVal SeatingAreaId As String = Nothing, Optional ByVal IsSwitchTable As Boolean = False) As DataTable
        Try
            Dim StrSql As String

            If IsSwitchTable Then
                StrSql = " SELECT Distinct Dine.DineInNumber ,ISNULL(ODM.status,0) as Status, " & _
                " Case When ISNULL(ODM.status,0)=1 then 'Reserved'   When ISNULL(RT.STATUS,0)=1 then 'Booked' Else 'Available' " & _
                " END as TableStatus  FROM	MstDineIn  Dine left outer join " & _
                " OrderDineInMap  ODM on Dine.DineInNumber = ODM.DineInNumber AND ODM.status = 1 AND BillDate = @dayopendate " & _
                "left outer join (SELECT ReservationId,CustomerNo, R.Date,R.IsOccupied, isnull(R.STATUS,0) AS STATUS ," & _
                "	Isnull( TableNo,0)AS TableNo,SiteCode from ReservationTbl R where STATUS=1 AND R.Date='" & Now.ToString("yyyy-MM-dd") & "')as RT" & _
                " on Dine.DineInNumber=RT.TableNo AND RT.STATUS=1 " & _
                " WHERE Dine.Status=1 and Dine.sitecode='" & sitecode & "'  "
                If Not String.IsNullOrEmpty(SeatingAreaId) Then
                    StrSql = StrSql & "  and Dine.SeatingAreaId='" & SeatingAreaId & "' "
                End If
            Else
                StrSql = "EXEC GetDineInReservationTables '" & Now.ToString("yyyy-MM-dd") & "', '" & Now.ToString("HH:mm:ss.ms") & "','" & dayopendate.ToString("yyyy-MM-dd") & "','" & sitecode & "','','" & LockTime & "','" & CustStayTime & "'"
            End If
            ',ISNULL(ODM.BillNo,'') as BillNo

            'StrSql = "   SELECt Distinct isnull ( RT.STATUS,0) AS RTStatus,Isnull (TableNo,0) AS TableNo, Case When ISNULL(ODM.Status,0)=1 Then 2 When Isnull (TableNo,0)=0 Then   " & _
            '         "    0 When Isnull (IsOccupied,0)=1 Then 3 else 1 END as TableStatus,MD.DineInNumber,isnull ( RT.IsOccupied,0) AS IsOccupied  " & _
            '         " from MstDineIn MD LEFT JOIN (SELECT isnull(STATUS,0) AS STATUS,Isnull( TableNo,0)AS TableNo,SiteCode," & _
            '         "Isnull( IsOccupied,0)AS IsOccupied from  ReservationTbl WHERE" & _
            '          " STATUS=1 AND Date='" & dayopendate.ToString("yyyy-MM-dd") & "'"

            'StrSql += " )AS RT ON MD.DineInNumber =RT.TableNo AND MD.SiteCode=RT.SiteCode "

            'StrSql += " Left Join (Select DineInNumber,Status,SiteCode,BillDate from OrderDineInMap wHERE Status=1 and BillDate='" & dayopendate.ToString("yyyy-MM-dd HH:mm:ss.fff") & "'  "
            'StrSql += " )ODM ON MD.DineInNumber=ODM.DineInNumber and MD.SiteCode=ODM.SiteCode Group By TableNo,MD.DineInNumber,RT.Status,ODM.Status,RT.IsOccupied Order By DineInNumber asc"


            Dim cmdTrn As New SqlCommand(StrSql, SpectrumCon)
            cmdTrn.Parameters.Add("@dayopendate", SqlDbType.Date)
            cmdTrn.Parameters("@dayopendate").Value = dayopendate

            Dim da As New SqlDataAdapter(cmdTrn)

            Dim dt As New DataTable
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetOrderTableWise(ByVal sitecode As String, ByVal dayopendate As Date, ByVal Tableno As String) As DataTable
        Try
            Dim StrSql As String
            ',ISNULL(ODM.BillNo,'') as BillNo
            StrSql = " SELECT ODM.DineInNumber ,ISNULL(ODM.status,0) as Status ,ISNULL(ODM.BillNo,'') as BillNo, Isnull(ODM.orderstatus,0) As orderstatus " & _
                     " FROM OrderDineInMap  ODM where  ODM.status = 1 AND BillDate = @dayopendate " & _
                     " And   ODM.sitecode='" & sitecode & "' And DineInNumber='" & Tableno & "' order by BillNo "

            Dim cmdTrn As New SqlCommand(StrSql, SpectrumCon)
            cmdTrn.Parameters.Add("@dayopendate", SqlDbType.Date)
            cmdTrn.Parameters("@dayopendate").Value = dayopendate

            Dim da As New SqlDataAdapter(cmdTrn)

            Dim dt As New DataTable
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetTotalAmountOrderWise(ByVal sitecode As String, ByVal dayopendate As Date, ByVal billno As String) As String
        Try
            Dim StrSql As String
            StrSql = " SELECT Isnull(SUM(NetAmount),0) As TotalPrise " & _
                     " FROM DineInCashMemoDtl  where  BillDate = @dayopendate " & _
                     " And  sitecode='" & sitecode & "'And  BillNo='" & billno & "' "

            Dim cmdTrn As New SqlCommand(StrSql, SpectrumCon)
            cmdTrn.Parameters.Add("@dayopendate", SqlDbType.Date)
            cmdTrn.Parameters("@dayopendate").Value = dayopendate

            Dim da As New SqlDataAdapter(cmdTrn)
            Dim dt As New DataTable
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt.Rows(0)(0)
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetOrderStatus(ByVal sitecode As String, ByVal dayopendate As Date, ByVal billno As String) As Integer
        Try
            Dim StrSql As String
            StrSql = " SELECT Isnull(orderstatus,0) As orderstatus " & _
                     " FROM OrderDineInMap  where  BillDate = @dayopendate " & _
                     " And  sitecode='" & sitecode & "'And  BillNo='" & billno & "' "

            Dim cmdTrn As New SqlCommand(StrSql, SpectrumCon)
            cmdTrn.Parameters.Add("@dayopendate", SqlDbType.Date)
            cmdTrn.Parameters("@dayopendate").Value = dayopendate

            Dim da As New SqlDataAdapter(cmdTrn)
            Dim dt As New DataTable
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt.Rows(0)(0)
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetItemsInfoForGenerateBill() As DataTable
        Try
            Dim vStmtQry As New StringBuilder

            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(50),'') as BillNo , " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as EAN, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as DISCRIPTION, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as ArticleCode, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as KotQuantity, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as KotNo ," & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Remark " & vbCrLf)
            Dim daScan As New SqlDataAdapter
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtScan As New DataTable
            daScan.Fill(dtScan)

            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function GetTableNum(ByVal sitecode As String, ByVal dayopendate As Date, ByVal billno As String) As String
        Try
            Dim StrSql As String
            StrSql = " SELECT IsNull(DineInNumber,0) " & _
                     " FROM OrderDineInMap  where  BillDate = @dayopendate " & _
                     " And  sitecode='" & sitecode & "'And  BillNo='" & billno & "' "

            Dim cmdTrn As New SqlCommand(StrSql, SpectrumCon)
            cmdTrn.Parameters.Add("@dayopendate", SqlDbType.Date)
            cmdTrn.Parameters("@dayopendate").Value = dayopendate

            Dim da As New SqlDataAdapter(cmdTrn)
            Dim dt As New DataTable
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Return dt.Rows(0)(0)
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'Void KOT Fetch the Data in Grid
    Public Function GetDineInKotQty(ByVal Billno As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim strQuery As New Text.StringBuilder()
            strQuery.Length = 0
            strQuery.Append(" SELECT distinct ma.ArticleCode,ma.ArticleName as DISCRIPTION,dk.EAN as EAN ,dk.KotNO ,")
            strQuery.Append(" dk.KotQuantity as Quantity,0.000 as VoidQuantity,'' as Reason,isnull(hc.Remark,'') as Remark")
            strQuery.Append(" FROM  DineInKotQtyMap dk Inner Join MstArticle ma ON dk.ArticleCode=ma.ArticleCode ")
            strQuery.Append(" inner join DineInCashMemoDtl hc on dk.ArticleCode=hc.ArticleCode ")
            'strQuery.Append(" where dk.BillNo='" & Billno & "'order by dk.KotNO")
            strQuery.Append(" where dk.BillNo='" & Billno & "' And hc.billno ='" & Billno & "' order by dk.KotNO")

            Using dafetch As New SqlDataAdapter(strQuery.ToString, ConString)
                dafetch.Fill(dt)
            End Using

            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

   
    Public Function updateVoidKotQty(ByVal ArticleCode As String, ByVal EAN As String, ByVal billno As String) As Boolean
        Try
            Dim objComm As New clsCommon
            Dim strQuery As New Text.StringBuilder()
            strQuery.Length = 0

            strQuery.Append(" delete from DineInKotQtyMap  ")
            strQuery.Append("  Where EAN='" & EAN & "'")
            strQuery.Append("  AND ArticleCode='" & ArticleCode & "'")
            strQuery.Append("  AND BillNo='" & billno & "';  ")

            strQuery.Append(" delete from DineInCashMemoDtl")
            strQuery.Append(" where BillNo='" & billno & "' ")
            strQuery.Append(" AND EAN='" & EAN & "' AND")
            strQuery.Append(" ArticleCode='" & ArticleCode & "'; ")
            'strQuery.Append("  and KotNO = '" & dr("KotNO") & "' ;")
            If objComm.InsertOrUpdateRecord(strQuery.ToString(), tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If
            Return True

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'Void KOT update Insert
    Public Function UpdateDineInKotQty(ByVal dt As DataTable, ByVal billno As String, ByVal UserName As String, ByVal sitecode As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            Dim objComm As New clsCommon
            Dim dtOld As New DataTable
            dtOld = clsCommon.GetOldKotData(billno)
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim cmd As New System.Text.StringBuilder()

            Dim KOTqty As Double
            Dim strreason As String

            Dim ReturnResult As DataTable = GetDineInKotQty(billno)
            For Each dr As DataRow In dt.Rows
                strreason = dr("Reason")


                'added by khusrao adil on 26-05-2017 for 1523 isseue id
                Dim dtDinInQty As DataTable = dt.Select("ArticleCode='" + dr("ArticleCode").ToString() + "'").CopyToDataTable
                Dim DinInQtyOld As String = dtDinInQty.Compute("sum(Quantity)-sum(VoidQuantity)", "")


                Dim article As String = dr("ArticleCode")
                Dim DinInQty As String
                For Each drr As DataRow In dtOld.Rows
                    Dim oldartcle As String = drr("ArticleCode")
                    If (article = oldartcle) Then
                        DinInQty = drr("KotQty")
                    End If
                Next

                KOTqty = DinInQty - dr("VoidQuantity")
                ' KOTqty = dr("Quantity") - dr("VoidQuantity")

                If (KOTqty = 0) Then

                    cmd.Append(" delete from DineInKotQtyMap  ")
                    cmd.Append("  Where BillNo='" & billno & "'")
                    cmd.Append("  AND EAN='" & dr("EAN") & "'")
                    cmd.Append("  AND ArticleCode='" & dr("ArticleCode") & "'  ")
                    cmd.Append("  and KotNO = '" & dr("KotNO") & "' ;")

                    cmd.Append(" delete from DineInCashMemoDtl")
                    cmd.Append(" where BillNo='" & billno & "' ")
                    cmd.Append(" AND EAN='" & dr("EAN") & "' AND")
                    cmd.Append(" ArticleCode='" & dr("ArticleCode") & "'; ")

                Else
                    If (DinInQtyOld = 0) Then
                        cmd.Append(" delete from DineInKotQtyMap  ")
                        cmd.Append("  Where BillNo='" & billno & "'")
                        cmd.Append("  AND EAN='" & dr("EAN") & "'")
                        cmd.Append("  AND ArticleCode='" & dr("ArticleCode") & "'  ")
                        cmd.Append("  and KotNO = '" & dr("KotNO") & "' ;")
                    End If
                    cmd.Append("  UPDATE   DineInKotQtyMap ")
                    cmd.Append("  SET               ")
                    cmd.Append("  KotQuantity=	'" & DinInQtyOld & "', ")
                    cmd.Append(" UPDATEDON=GETDATE(),UPDATEDAT='" & sitecode & "' ,UPDATEDBY='" & UserName & "'")
                    cmd.Append("  Where BillNo='" & billno & "'")
                    cmd.Append("  AND EAN='" & dr("EAN") & "'")
                    cmd.Append("  AND ArticleCode='" & dr("ArticleCode") & "'  ")
                    cmd.Append("  and KotNO = '" & dr("KotNO") & "' ;")



                    cmd.Append(" UPDATE DineInCashMemoDtl")
                    'code added for issue id 1523 
                    ' cmd.Append(" set Quantity='" & KOTqty & "', ")
                    cmd.Append(" set Quantity='" & KOTqty & "', ") 'code change KOTqty
                    cmd.Append(" UPDATEDON=GETDATE() ")
                    cmd.Append(" where BillNo='" & billno & "' ")
                    cmd.Append(" AND EAN='" & dr("EAN") & "' AND")
                    cmd.Append(" ArticleCode='" & dr("ArticleCode") & "' ;")

                End If

                cmd.Append(" INSERT INTO DineInProcessHistory( ")
                cmd.Append("ProcessType,BillNo,EAN,ArticleCode,Quantity,status,Reason,KotNO,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON) ")
                cmd.Append(" Values(")
                cmd.Append("'Void','" & billno & "',")
                cmd.Append("'" & dr("EAN") & "','" & dr("ArticleCode") & "',")
                cmd.Append("'" & dr("VoidQuantity") & "',1,'" & strreason.Replace("'", "") & "','" & dr("KotNO") & "', ")
                cmd.Append("'" & sitecode & "','" & UserName & "', GETDATE(),")
                cmd.Append("'" & sitecode & "','" & UserName & "', GETDATE()")
                cmd.Append(") ;")
                If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
                cmd.Length = 0

            Next
            tran.Commit()
            CloseConnection()
            Return True

        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function

    'update No of Customers in TrackCustomers
    Public Function UpdateTrackCustomers(ByVal billno As String, ByVal crntdineintable As String, ByVal strsitecode As String, ByVal noofcust As Integer)

        Dim tran As SqlTransaction = Nothing
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim cmd As New System.Text.StringBuilder()
            Dim objComm As New clsCommon

            cmd.Append(" UPDATE OrderDineInMap set")
            cmd.Append(" NoOfCustomer='" & noofcust & "'")
            cmd.Append(" where BillNo='" & billno & "' ")
            cmd.Append(" AND DineInNumber='" & crntdineintable & "' AND SiteCode='" & strsitecode & "' ")

            If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If
            cmd.Length = 0

            tran.Commit()
            CloseConnection()
            Return True

        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try

    End Function

    'Fetch No of Customers for Particular Billno 
    Public Function GetTrackCustomers(ByVal billno As String, ByVal currentdineintable As String, ByVal sitecode As String) As Integer
        Try
            Dim StrSql As String

            StrSql = "select NoOfCustomer from OrderDineInMap  where BillNo='" & billno & "'  AND DineInNumber='" & currentdineintable & "' AND SiteCode='" & sitecode & "' "

            Dim cmdTrn As New SqlCommand(StrSql, SpectrumCon)

            Using da As New SqlDataAdapter(cmdTrn)

                Dim dt As New DataTable
                da.Fill(dt)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    Return dt.Rows(0)(0)
                End If

            End Using
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    'Update KOT Remark in Void KOT Remark
    Public Function UpdateKOTRemark(ByVal remark As String, ByVal billno As String, ByVal articlecode As String, ByVal eanno As String)

        Dim tran As SqlTransaction = Nothing
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim cmd As New System.Text.StringBuilder()
            Dim objComm As New clsCommon

            cmd.Append(" UPDATE DineInCashMemoDtl set Remark='" & remark & "' ")
            cmd.Append(" where BillNo= '" & billno & "' AND ArticleCode='" & articlecode & "'")
            cmd.Append(" AND EAN='" & eanno & "'")





            If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If
            cmd.Length = 0

            tran.Commit()
            CloseConnection()
            Return True

        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try

    End Function

    'Get KOT Remark of Particular BillNo
    Public Function GetTrackKOTRemark(ByVal billno As String, ByVal articlecode As String, ByVal eanno As String) As String
        Try
            Dim StrSql As String

            StrSql = "select Remark from DineInCashMemoDtl where BillNo= '" & billno & "' AND ArticleCode='" & articlecode & "' AND EAN='" & eanno & "'"

            Dim cmdTrn As New SqlCommand(StrSql, SpectrumCon)

            Using da As New SqlDataAdapter(cmdTrn)

                Dim dt As New DataTable
                da.Fill(dt)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    Return dt.Rows(0)(0)
                End If

            End Using
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetOrderDetails(ByVal Billno As String) As DataTable
        Try
            Dim dt As New DataTable
            'Dim orderfetch As New SqlDataAdapter("select distinct CONVERT(BIT,0) AS [SELECT],dk.KotNO as KOTNumber,ma.ArticleCode as ItemCode,dk.EAN as EAN,ma.ArticleName as Description,dk.KotQuantity as Quantity,dk.Status as Status,isnull(dtl.Remark,'') as Remark from  DineInKotQtyMap dk INNER join MstArticle ma ON dk.ArticleCode=ma.ArticleCode inner join HoldCashMemoDtl dtl on dk.ArticleCode=dtl.ArticleCode  where dk.BillNo='" & Billno & "'  order by dk.KotNO ", ConString)
            Dim orderfetch As New SqlDataAdapter("select distinct CONVERT(BIT,0) AS [SELECT],dk.KotNO as KOTNumber,ma.ArticleCode as ItemCode,dk.EAN as EAN,ma.ArticleName as Description,dk.KotQuantity as Quantity,dk.Status as Status,isnull(dtl.Remark,'') as Remark from  DineInKotQtyMap dk INNER join MstArticle ma ON dk.ArticleCode=ma.ArticleCode inner join DineInCashMemoDtl dtl on dk.ArticleCode=dtl.ArticleCode  where dk.BillNo='" & Billno & "' And dtl.Billno='" & Billno & "' order by dk.KotNO ", ConString)
            orderfetch.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetReprintItemsInfo() As DataTable
        Try
            vStmtQry.Length = 0
            'vStmtQry.Append(" Select Convert(Varchar(50),'') as ArticleCode, " & vbCrLf)
            vStmtQry.Append(" Select Convert(bit,'True') as IsReprint, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as KOTNumber , " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(8000),'') as Description, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Reason " & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtScan As New DataTable
            daScan.Fill(dtScan)

            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function

    Public Function GetReprintStructure() As DataTable
        Try
            vStmtQry.Length = 0
            vStmtQry.Append(" Select Convert(Varchar(100),'') as ArticleCode, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as EAN, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(8000),'') as Discription, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as  KotQuantity, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(50),'') as Status, " & vbCrLf)
            vStmtQry.Append(" Convert(numeric(18,2),0) as KotNo, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Remark, " & vbCrLf)
            vStmtQry.Append(" Convert(Varchar(100),'') as Reason " & vbCrLf)
            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            Dim dtScan As New DataTable
            daScan.Fill(dtScan)

            Return dtScan
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function UpdateDineInReprintReason(ByVal dt As DataTable, ByVal billno As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim cmd As New System.Text.StringBuilder()
            Dim objComm As New clsCommon

            For Each dr As DataRow In dt.Rows
                cmd.Length = 0
                cmd.Append(" INSERT INTO DineInProcessHistory( ")
                cmd.Append("ProcessType,BillNo,EAN,ArticleCode,Quantity,status,Reason,KotNo) ")
                cmd.Append(" Values(")
                cmd.Append("'Reprint','" & billno & "',")
                cmd.Append("'null','null',")
                'cmd.Append("'0',1,'" & dr("Remark") & "','" & dr("KOTNumber") & "' ")
                cmd.Append("'0',1,")
                If Not IsDBNull(dr("Reason")) Then
                    cmd.Append("'" & dr("Reason") & "',")
                Else
                    cmd.Append("'null',")
                End If
                If Not IsDBNull(dr("KOTNumber")) Then
                    cmd.Append("'" & dr("KOTNumber") & "'")
                Else
                    cmd.Append("'null'")
                End If

                cmd.Append(") ;")


                If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If

            Next
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    '-----------Function for merge order
    Public Function GetTableForMerge(ByVal sitecode As String, Optional ByVal DayOpenDate As DateTime = Nothing) As DataTable
        Try
            Dim StrSql As String
            StrSql = " SELECT Distinct o.DineInNumber,m.DineInName,RT.ReservationId,o.BillNo from OrderDineInMap as o " & _
                     " Left Join MstDineIn as m on o.DineInNumber =m.DineInNumber " & _
                     "Left Join ReservationTbl RT on o.DineInNumber=RT.TableNo and RT.Status=1 and RT.IsOccupied=1 and RT.Date= '" & Now.ToString("yyyy-MM-dd") & "'" & _
                     " WHERE m.sitecode='" & sitecode & "' and o.Status=1 and BillDate='" & DayOpenDate.ToString("yyyy-MM-dd") & "' and BillNo Not In(Select BillNo  from DineInMergeOrdersDtl)  "

            Dim cmdTrn As New SqlCommand(StrSql, SpectrumCon)
            Dim da As New SqlDataAdapter(cmdTrn)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetOrderDetailForMerge(ByVal sitecode As String, ByVal tableno As Integer, Optional ByVal DayOpenDate As DateTime = Nothing) As DataTable
        Try
            Dim StrSql As String
            StrSql = " SELECT DineInNumber,BillNo " & _
                     " FROM	OrderDineInMap " & _
                     " WHERE sitecode='" & sitecode & "' and DineInNumber='" & tableno & "' and  BillDate='" & DayOpenDate.ToString("yyyy-MM-dd") & "' and Status=1 and BillNo Not In(Select BillNo  from DineInMergeOrdersDtl)"
            Dim cmdTrn As New SqlCommand(StrSql, SpectrumCon)
            Dim da As New SqlDataAdapter(cmdTrn)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetMappingMerge(Optional ByVal MergeId As Integer = 0, Optional ByVal DayOpenDate As DateTime = Nothing) As DataSet
        Try
            GetMappingMerge = New DataSet
            vStmtQry.Length = 0
            ' vStmtQry.Append("Select Convert(numeric(18,2),0) as TableNumber, " & vbCrLf)
            'vStmtQry.Append(" Convert(Varchar(100),'') as OrderNumber " & vbCrLf)
            ' vStmtQry.Append("Select  ODM.DineInNumber as TableNumber,Dmod.BillNo as OrderNumber from    DineInMergeOrdersDtl  as DMOD Inner Join OrderDineInMap AS ODM ON DMOD.BillNo = ODM.BillNo WHERE ODM.Status='1' ")
            vStmtQry.Append(" select MergeName  FROM DineInMergeOrdersHdr Where MergeId =" & MergeId & ";")

            vStmtQry.Append(" Select  ODM.DineInNumber AS TableNumber ,Dmod.BillNo AS OrderNumber,Dmod.ReservationId ")
            vStmtQry.Append(" FROM    DineInMergeOrdersDtl  AS DMOD Inner Join  ")
            vStmtQry.Append(" OrderDineInMap AS ODM ON DMOD.BillNo = ODM.BillNo ")
            vStmtQry.Append(" WHERE ODM.Status='1' and DMOD.MergeId =" & MergeId & " and ODM.BillDate='" & DayOpenDate.ToString("yyyy-MM-dd") & "';")

            daScan = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            daScan.Fill(GetMappingMerge)

            If GetMappingMerge.Tables.Count > 1 Then
                Dim primaryKey(1) As DataColumn
                primaryKey(0) = GetMappingMerge.Tables(1).Columns("OrderNumber")
                GetMappingMerge.Tables(1).PrimaryKey = primaryKey
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Public Function SaveForMergeOrder(ByVal dt As DataTable, ByVal mergename As String, ByVal usercode As String, ByVal sitecode As String, ByRef mergeid As Int64) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            Dim NewMergeId As Integer
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim cmd As New System.Text.StringBuilder()
            Dim objComm As New clsCommon

            If mergeid = 0 Then
                cmd.Length = 0
                cmd.Append(" INSERT INTO DineInMergeOrdersHdr( ")
                cmd.Append(" MergeName, CREATEDAT, CREATEDBY, CREATEDON, UPDATEDAT, UPDATEDBY, UPDATEDON, STATUS) ")
                cmd.Append(" Values(")
                cmd.Append("'" & mergename & "','" & sitecode & "',")
                cmd.Append("'" & usercode & "',GetDate(),")
                cmd.Append("'" & sitecode & "','" & usercode & "',")
                cmd.Append("GetDate(),1")
                cmd.Append(") ;")
                If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
                NewMergeId = getTableIdentityValueId("DineInMergeOrdersHdr", SpectrumCon, tran)
            Else
                '--- Update command ..
                cmd.Length = 0
                cmd.Append("  UPDATE    DineInMergeOrdersHdr ")
                cmd.Append("  SET ")
                cmd.Append("  UPDATEDON	=	GetDate() , mergename='" & mergename & "' ")
                cmd.Append("  Where MergeId = '" & mergeid & " '  ")
                If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
            End If

            If mergeid > 0 Then
                '--Delete details record
                cmd.Length = 0
                cmd.Append("  delete from    DineInMergeOrdersDtl ")
                cmd.Append("  Where MergeId = '" & mergeid & " '  ")
                If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
                NewMergeId = mergeid
            Else
                mergeid = NewMergeId
            End If
            For Each dr As DataRow In dt.Rows
                If Not dr.RowState = DataRowState.Deleted Then
                    cmd.Length = 0
                    cmd.Append(" INSERT INTO DineInMergeOrdersDtl( ")
                    cmd.Append(" MergeId, BillNo,ReservationId) ")
                    cmd.Append(" Values(")
                    cmd.Append("'" & NewMergeId & "','" & dr("OrderNumber") & "','" & dr("ReservationId") & "'")
                    cmd.Append(") ;")
                    If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                        tran.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If
            Next
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function LoadMergeOrders() As DataTable
        Try
            Dim StrSql As String
            StrSql = " SELECT DISTINCT CONVERT(BIT,0)AS [SELECT],  DhdrM.MergeName As Name " & _
                     " ,STUFF((SELECT ', ' + mst.DineInName   + '-' + Dhdtl.BillNo  As A" & _
                     " FROM DineInMergeOrdersHdr as Dhdr" & _
                     " LEFT JOIN DineInMergeOrdersDtl Dhdtl on Dhdr.MergeId = Dhdtl.MergeId  " & _
                     " LEFT JOIN OrderDineInMap As O on o.BillNo = Dhdtl.BillNo  " & _
                     " LEFT JOIN MstDineIn as mst on o.DineInNumber=mst.DineInNumber  " & _
                     "      where DhdrM.MergeId = Dhdr.MergeId" & _
                     " FOR XML PATH(''), TYPE)" & _
                     " .value('.','NVARCHAR(MAX)'),1,2,' ') Orders ,DhdrM.MergeId As MergeId " & _
                     " FROM DineInMergeOrdersHdr as DhdrM " & _
                     " INNER JOIN DineInMergeOrdersDTL DT ON DT.MergeId =DhdrM.MergeId " & _
                     " WHERE DT.BillNo IN (SELECT BILLNO FROM OrderDineInMap WHERE STATUS=1) AND " & _
                     " DhdrM.STATUS=1  "
            Dim cmdTrn As New SqlCommand(StrSql, SpectrumCon)
            Dim da As New SqlDataAdapter(cmdTrn)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    '---For Delete Merge Orders
    Public Function DeleteMergeOrder(ByRef mergeid As Int64) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim cmd As New System.Text.StringBuilder()
            Dim objComm As New clsCommon

            cmd.Length = 0
            cmd.Append("  delete from    DineInMergeOrdersHdr ")
            cmd.Append("  Where MergeId = '" & mergeid & "'; ")
            cmd.Append("  delete from    DineInMergeOrdersDtl ")
            cmd.Append("  Where MergeId = '" & mergeid & "' ; ")
            If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If

            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function LoadSeatingArea(ByVal SiteCode As String) As DataTable
        Try
            Dim StrSql As String
            StrSql = "select sa.SeatingAreaid,sa.SeatingAreaName from seatingArea sa Inner Join" & _
                    " Siteseatingareamapping sam on sa.Seatingareaid=sam.seatingareaid Where sam.Sitecode= '" & SiteCode & "' and sam.Status=1"
            Dim cmdTrn As New SqlCommand(StrSql, SpectrumCon)
            Dim da As New SqlDataAdapter(cmdTrn)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetSeatingAreId(ByVal DineInNumber As Integer, ByVal SiteCode As String) As Integer
        Try
            Dim StrSql As String
            StrSql = "select ISNULL(Seatingareaid,0) as SeatingAreaId from MstDineIn where DineInNumber='" & DineInNumber & "' and sitecode='" & SiteCode & "'"

            Dim cmdTrn As New SqlCommand(StrSql, SpectrumCon)
            Dim da As New SqlDataAdapter(cmdTrn)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt.Rows(0)(0)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

#End Region

    ''' <summary>
    ''' Delete the cash Memo
    ''' </summary>
    ''' <param name="Ds"></param>
    ''' <param name="SiteCode"></param>
    ''' <param name="UserId"></param>
    ''' <param name="Storage"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteBill(ByVal Ds As DataSet, ByVal SiteCode As String, ByVal UserId As String, ByVal Storage As String) As Boolean
        Try
            DeleteBill = True
            Dim tran As SqlTransaction = Nothing
            OpenConnection()
            tran = SpectrumCon.BeginTransaction


            Dim Qty As Double
            For Each row As DataRow In Ds.Tables("CASHMEMODTL").Rows
                Qty = CDbl(row("Quantity")) * -1
                If UpdateStock(SiteCode, row("ARTICLECODE").ToString(), row("EAN").ToString(), row("UNITOFMEASURE").ToString(), Qty, UserId, SpectrumCon, tran, Storage) = False Then
                    tran.Rollback()
                    DeleteBill = False
                    Exit Try
                End If
            Next
            For Each dr As DataRow In Ds.Tables("CASHMEMODTL").Rows
                If IsDBNull(dr("BatchBarcode")) = False AndAlso String.IsNullOrEmpty(dr("BatchBarcode")) = False Then
                    Qty = CDbl(dr("Quantity")) * -1
                    If UpdateBatchDtlQtyAllocated(SiteCode, dr("BatchBarcode").ToString(), Qty, tran) = False Then
                        tran.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If
            Next
            Dim billNo = Ds.Tables("CASHMEMOHDR").Rows(0)("BillNo")
            Dim query As String = "select ArticleCode , Quantity ,EAN from CashMemoComboDtl  where BillNo = '" & billNo & "' and SiteCode = '" & SiteCode & "' and FinYear = '" & DateTime.Now.Year.ToString() & "'"
            Dim comboItems As DataTable = GetFilledTable(query)
            If comboItems IsNot Nothing AndAlso comboItems.Rows.Count > 0 Then
                For Each dr As DataRow In comboItems.Rows
                    Qty = CDbl(dr("Quantity")) * -1
                    If UpdateStock(SiteCode, dr("ArticleCode").ToString(), dr("EAN").ToString(), Nothing, Qty, UserId, SpectrumCon, tran, Storage) = False Then
                        tran.Rollback()
                        DeleteBill = False
                        Exit Try
                    End If
                Next
            End If
            If Ds.Tables("CASHMEMOHDR").Columns.Contains("RETRIEVEDFROMCUSTNAME") Then
                DeleteColumnFromDataTable(Ds.Tables("CASHMEMOHDR"), "RETRIEVEDFROMCUSTNAME")
            End If
            If DeactivateVoucher(SpectrumCon, tran, SiteCode, Ds.Tables("CASHMEMOHDR").Rows(0)("Billno").ToString(), UserId, "CMS") = False Then
                tran.Rollback()
                DeleteBill = False
                Exit Try
            End If
            For Each dr As DataRow In Ds.Tables("CASHMEMORECEIPT").Select("TenderTypeCode='CreditVouc(R)' OR TenderTypeCode='GiftVoucher(R)'", "", DataViewRowState.CurrentRows)
                If ActiveVoucher(SpectrumCon, tran, SiteCode, dr("CardNO").ToString(), UserId) = False Then
                    Return False
                End If
            Next
            If Ds.Tables("CASHMEMORECEIPT").Rows(0)("TenderTypeCode") = "Cheque" Then
                If UpdateVoidStatus(SiteCode, billNo, SpectrumCon, tran) = False Then
                    tran.Rollback()
                    DeleteBill = False
                    Exit Try
                End If
            End If
            If SaveData(Ds.Tables("CashMemoHdr"), SpectrumCon, tran) = False Then
                tran.Rollback()
                DeleteBill = False
                Exit Try
            End If
            tran.Commit()
        Catch ex As Exception
            LogException(ex)
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function GetComboDetailsDataForPrint(ByVal BillNo As String, ByVal sitecode As String, ByVal langcode As String) As DataTable
        Try
            Dim StrQuery As String = "SELECT  A.BillNO, A.BillLineNo ,    a.ComboArticleCode , a.ArticleCode  ,b.ArticleName , a.Quantity FROM      CASHMEMOCOMBODTL as a inner join mstarticle as b on a.ArticleCode = b.ArticleCode WHERE A.BillNO='" & BillNo & "' AND A.SiteCode='" & sitecode & "' "
            Dim da As New SqlDataAdapter(StrQuery, ConString)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetBillDetailsDataForPrintForA4(ByVal BillNo As String, ByVal sitecode As String, ByVal Type As String) As DataSet
        Try
            Dim ds As New DataSet
            Dim query As String = "Exec UDP_INVOICEHEADER '" & Sitecode & "', '" & BillNo & "'"
            Dim da As New SqlDataAdapter(query, ConString)
            Dim dt As New DataTable
            da.Fill(dt)
            dt.TableName = "dsHeader"

            Dim query2 As String = "Exec UDP_INVOICEDETAILS '" & sitecode & "', '" & BillNo & "','" & Type & "'"
            Dim da2 As New SqlDataAdapter(query2, ConString)
            Dim dt2 As New DataTable
            da2.Fill(dt2)
            dt2.TableName = "dsUnique"
            ds.Tables.Add(dt)
            ds.Tables.Add(dt2)
            Return ds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ' Public Function GetBillDetailsDataForPrint(ByVal BillNo As String, ByVal sitecode As String, ByVal langcode As String, Optional sectionwisearticle As Boolean = False) As DataTable
    Public Function GetBillDetailsDataForPrint(ByVal BillNo As String, ByVal sitecode As String, ByVal langcode As String, Optional sectionwisearticle As Boolean = False, Optional ByVal InvoiceType As String = "") As DataTable   'Method is added by irfan on 15/09/2017 for IsHsnAndTaxVisibleInPrintFormat6
        Try
            'Dim CodeHierchy = "ANCCCE000000002,ANCCCE000000006"
            ' Dim StrQuery As String = "SELECT A.ARTICLECODE,Case when C.ArticleShortName is null then B.ArticleShortName else C.ArticleShortName END AS DISCRIPTION, Case when C.ArticleName is null then B.ArticleName else C.ArticleShortName END AS ArticleFullName, A.QUANTITY,A.TakeAwayQUANTITY, A.SELLINGPRICE, A.GROSSAMT, A.TOTALDISCOUNT, A.TOTALDISCPERCENTAGE, A.NETAMOUNT, A.TOTALTAXAMOUNT FROM CASHMEMODTL A INNER JOIN MSTARTICLE B ON A.ARTICLECODE=B.ARTICLECODE left outer join ArticleDescInDiffLang C on A.ArticleCode=C.ArticleCode AND C.LanguageCode='" & langcode & "' WHERE A.BillNO='" & BillNo & "' AND A.SiteCode='" & sitecode & "' AND B.Printable = 1 "
            'Dim StrQuery As String = "SELECT A.ARTICLECODE,Case when C.ArticleShortName is null then B.ArticleShortName else C.ArticleShortName END AS DISCRIPTION, Case when C.ArticleName is null then B.ArticleName else C.ArticleShortName END AS ArticleFullName, A.QUANTITY,A.TakeAwayQUANTITY, A.SELLINGPRICE, A.GROSSAMT, A.TOTALDISCOUNT, A.TOTALDISCPERCENTAGE, A.NETAMOUNT, A.TOTALTAXAMOUNT,case when  B.LastNodeCode=  P.LastNodeCode then 1 else 0 end as IsQuntityWiseRequired  FROM CASHMEMODTL A INNER JOIN MSTARTICLE B ON A.ARTICLECODE=B.ARTICLECODE left outer join ArticleDescInDiffLang C on A.ArticleCode=C.ArticleCode AND C.LanguageCode='" & langcode & "' left outer join  ( select * from dbo.GetLastNodeCode('" & CodeHierchy & "') )as  P on P.LastNodeCode=b.LastNodeCode   WHERE A.BillNO='" & BillNo & "' AND A.SiteCode='" & sitecode & "' AND B.Printable = 1 "
            'Dim StrQuery As String = "SELECT A.BillLineNo , A.ARTICLECODE,A.UnitofMeasure,Case when C.ArticleShortName is null then B.ArticleShortName else C.ArticleShortName END AS DISCRIPTION, Case when C.ArticleName is null then B.ArticleName else C.ArticleShortName END AS ArticleFullName, A.QUANTITY,A.TakeAwayQUANTITY, A.SELLINGPRICE, A.GROSSAMT, A.TOTALDISCOUNT, A.TOTALDISCPERCENTAGE, A.NETAMOUNT,A.PromotionId, A.TOTALTAXAMOUNT,case when  B.LastNodeCode=  P.LastNodeCode then 1 else 0 end as IsQuntityWiseRequired  FROM CASHMEMODTL A INNER JOIN MSTARTICLE B ON A.ARTICLECODE=B.ARTICLECODE left outer join ArticleDescInDiffLang C on A.ArticleCode=C.ArticleCode AND C.LanguageCode='" & langcode & "' left outer join  ( select * from dbo.GetLastNodeCode((select * from dbo.LastNodeCode( ))) )as  P on P.LastNodeCode=b.LastNodeCode   WHERE A.BillNO='" & BillNo & "' AND A.SiteCode='" & sitecode & "' AND B.Printable = 1 "
            '' Query changed by ketan add remark table 
            'modified by khusrao adil on 04-07-2017 added new cloumn GST No
            'Added by irfan on 13/09/2017 for hsn and tax
            Dim query2 As String = "Exec UDP_GETARTICLETAX  '" & sitecode & "','" & BillNo & "','" & InvoiceType & "', '" & sectionwisearticle & "'"
            Dim da As New SqlDataAdapter(query2, ConString)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' A4 size print format data
    ''' </summary>
    ''' <param name="BillNo"></param>
    ''' <param name="sitecode"></param>
    ''' <param name="langcode"></param>
    ''' <param name="sectionwisearticle"></param>
    ''' <returns>added by khurao on 22-08-2017</returns>
    ''' <remarks></remarks>
    Public Function GetBillDetailsDataForPrint(ByVal BillNo As String, ByVal sitecode As String) As DataSet
        Try
            Dim ds As New DataSet
            Dim query As String = "Exec UDP_INVOICEHEADER '" & sitecode & "', '" & BillNo & "'"
            Dim da As New SqlDataAdapter(query, ConString)
            Dim dt As New DataTable
            da.Fill(dt)
            dt.TableName = "dsHeader"

            Dim query2 As String = "Exec UDP_INVOICEDETAILS '" & sitecode & "', '" & BillNo & "', '" & "CMS" & "'"
            Dim da2 As New SqlDataAdapter(query2, ConString)
            Dim dt2 As New DataTable
            da2.Fill(dt2)
            dt2.TableName = "dsUnique"
            ds.Tables.Add(dt)
            ds.Tables.Add(dt2)
            Return ds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetMettlerBillDetailsDataForPrint(ByVal BillNo As String, ByVal sitecode As String) As DataTable
        Try
            Dim StrQuery As String = " Select MettlerScaleBillDate, SUBSTRING(MettlerScaleBillNO,1,2) AS SCALE_NO , CAST(SUBSTRING(MettlerScaleBillNO,3,4) as int) AS BILL_NO,TotalLineItems " & _
                                     " From CashMemoMettler AS A " & _
                                     " WHERE A.BillNO='" & BillNo & "' "
            Dim da As New SqlDataAdapter(StrQuery, ConString)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'code added by vipul for mettler printing details
    Public Function GetSpectrumMettlerBillDetailsDataForPrint(ByVal BillNo As String, ByVal sitecode As String) As DataTable
        Try
            Dim StrQuery As String = "  select a.MettlerScaleBillDate,b.ScaleNo AS SCALE_NO ,a.MettlerScaleBillNO AS BILL_NO,a.TotalLineItems from CashMemoMettler a" & _
                                      " inner Join   (select  billno,ScaleNo from SpectrumMettlerDtl group by BillNo,ScaleNo)  b on a.MettlerScaleBillNo = b.billno" & _
                                       " where a.BillNo = '" & BillNo & " '"



            Dim da As New SqlDataAdapter(StrQuery, ConString)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetVoucherData(ByVal BillNo As String, ByVal sitecode As String, ByRef VoucherNo As String, ByRef VoucherexpiryDate As DateTime) As DataTable
        Try
            Dim StrQuery As String = "SELECT A.BILLNO,A.BILLDATE,A.BILLTIME,B.BILLNO AS REFBILL,B.BILLDATE AS REFBILLDATE,A.CREATEDBY as USERNAME,G.TNCSRNO,G.DESCRIPTION " & _
                    "FROM CASHMEMOHDR A	LEFT OUTER JOIN	CASHMEMODTL B	ON A.BILLNO=B.RETURNCMNO LEFT OUTER JOIN MSTTERMSNCONDITON G ON A.TRANSACTIONCODE=G.DOCTYPE WHERE A.BILLNO='" & BillNo & "'"
            Dim da As New SqlDataAdapter(StrQuery, ConString)

            Dim dt As New DataTable
            da.Fill(dt)
            StrQuery = "SELECT VOUCHERCODE,VOURCHERSERIALNBR,EXPIRYDATE FROM VOUCHERDTLS WHERE ISSUEDDOCNUMBER='" & BillNo & "' AND ISSUEDATSITE='" & sitecode & "' AND ISSUEDINDOCTYPE='CMS'"
            Dim dtVouch As New DataTable
            da.SelectCommand.CommandText = StrQuery
            da.Fill(dtVouch)
            If dtVouch.Rows.Count > 0 Then
                VoucherNo = dtVouch.Rows(0)("VOURCHERSERIALNBR").ToString()
                VoucherexpiryDate = dtVouch.Rows(0)("EXPIRYDATE")
            End If
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' This same procedure is used for GV and CV data Creation and Updation
    ''' </summary>
    ''' <param name="CVProgram"></param>
    ''' <param name="DocType"></param>
    ''' <param name="NewVoucher"></param>
    ''' <param name="CMNo"></param>
    ''' <param name="SiteCode"></param>
    ''' <param name="ServerDateTime"></param>
    ''' <param name="UserId"></param>
    ''' <param name="tran"></param>
    ''' <param name="con"></param>
    ''' <param name="Amount"></param>
    ''' <param name="VoucherNo"></param>
    ''' <param name="VoucherDays"></param>
    ''' <param name="CVorGV"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateCreditVoucher(ByVal CVProgram As String, ByVal DocType As String, ByVal NewVoucher As Boolean, ByVal CMNo As String, ByVal SiteCode As String, _
              ByVal DayOpenDate As DateTime, ByVal UserId As String, ByRef tran As SqlTransaction, ByRef con As SqlConnection, _
              Optional ByVal Amount As Double = 0, Optional ByRef VoucherNo As String = "0", Optional ByRef VoucherDays As Int32 = 0, Optional ByVal CVorGV As String = "") As Boolean
        Try
            Dim cmdVoucher As SqlCommand
            Dim strQuery As String
            If Amount < 0 Then
                Amount = Amount * -1
            End If

            If NewVoucher Then
                Dim CVNo As String = getDocumentNo(IIf(CVorGV = "", "CV", CVorGV), SiteCode)
                CVNo = CInt(CVNo) + 1
                CVNo = CVNo

                'Changed by Rohit to generate Document No. for proper sorting
                Dim genVoucherNo As String = String.Empty
                Dim strcvorgv As String = "C"
                Try

                    If CVorGV <> "" Then
                        strcvorgv = CVorGV.Substring(0, 1)
                    Else
                        strcvorgv = CVorGV
                    End If
                    Dim strFinYear As String = GetFinancialYear(DayOpenDate, SiteCode)
                    Dim character As Char = SiteCode.FirstOrDefault(Function(x) x <> "0")
                    genVoucherNo = GenDocNo(IIf(CVorGV = "", "C", strcvorgv) & SiteCode.Substring(SiteCode.IndexOf(character)) & strFinYear.Substring(strFinYear.Length - 2, 2), 13, CVNo)
                Catch ex As Exception
                    genVoucherNo = IIf(CVorGV = "", "C", strcvorgv) + SiteCode.Substring(SiteCode.Length - 3, 3) + CVNo
                End Try
                'End Change by Rohit

                strQuery = "INSERT INTO VOUCHERDTLS(SITECODE,VOUCHERCODE,VOURCHERSERIALNBR,VALUEOFVOUCHER,ISACTIVE," & _
                "ISISSUED,ISSUEDATSITE,ISSUEDONDATE,ISSUEDINDOCTYPE,ISSUEDDOCNUMBER,EXPIRYDATE,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS)" & _
                "VALUES('" & SiteCode & "','" & CVProgram & "','" & genVoucherNo & "'," & ConvertToEnglish(Amount) & ",1,1,'" & SiteCode & "'," & _
                        "getdate() ,'" & DocType & "','" & CMNo & "',DateAdd(day, " & VoucherDays & ",getdate()) ,'" & SiteCode & "','" & UserId & "', getdate(),'" & SiteCode & "','" & UserId & "',getdate(),1)"
                cmdVoucher = New SqlCommand(strQuery, con)
                cmdVoucher.Transaction = tran
                If cmdVoucher.ExecuteNonQuery() > 0 Then
                    If UpdateDocumentNo(IIf(CVorGV = "", "CV", CVorGV), con, tran, CVNo) Then
                        VoucherNo = genVoucherNo
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                strQuery = "UPDATE VOUCHERDTLS SET RedeemedDocNumber='" & CMNo & "',RedeemedAtSite='" & SiteCode & "',RedeemedInDoctype='" & DocType & "',RedeemedOnDate=Cast('" & DayOpenDate.ToString("yyyyMMdd") & "' AS DateTime)" & _
                " ,IsRedeemed=1, UPDATEDAT='" & SiteCode & "',UPDATEDBY='" & UserId & "',UPDATEDON= getdate()   WHERE  VOURCHERSERIALNBR='" & VoucherNo & "'"
                cmdVoucher = New SqlCommand(strQuery, con)
                cmdVoucher.Transaction = tran
                If cmdVoucher.ExecuteNonQuery() > 0 Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception

            'ShowMessage(ex.Message, getValueByKey("CLAE05"))

            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function GetCLPAccumlationDetails(ByVal SiteCode As String, ByRef AccuType As String, ByRef RedType As String, Optional ByVal strCustomerID As String = "") As DataSet
        Try
            Dim dt, dt1, dtHeader, dtClpArticleHierarchyMap, dtCLPCustomers As DataTable
            Dim ds As New DataSet
            Dim da As New SqlDataAdapter("SELECT CLPPROGRAMID FROM CLPPROGRAMSITEMAP WHERE SITECODE='" & SiteCode & "' AND ACCUMLATIONAPPLICABLE=1 AND Status=1", ConString)
            dt = New DataTable
            da.Fill(dt)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                da.SelectCommand.CommandText = "SELECT A.CLPPROGRAMID,A.ACCUMLATIONTYPE,B.TYPEVALUE,A.RedemptionType,A.MAXLIMITONACC,A.MAXACCUMULATIONLIMIT,A.MAXLIMITONRED,A.ONLYATCREATEDSITE,A.ACCWHENRED,A.APPLICABLEONPROMOTION,A.ValueMinPointsForRedemption, A.IsMinPointsForRedemption FROM MSTCLPPROGRAM A INNER JOIN MSTCLPTYPE B ON A.ACCUMLATIONTYPE=B.TYPEKEY   WHERE A.CLPPROGRAMID='" & dt.Rows(0)("CLPPROGRAMID").ToString() & "' and A.Status=1 AND Getdate() between Startdate and enddate "
                dt = New DataTable
                da.Fill(dt)
                dt.TableName = "MSTCLPPROGRAM"
                ds.Tables.Add(dt)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    AccuType = dt.Rows(0)("TYPEVALUE").ToString()
                    RedType = dt.Rows(0)("REDEMPTIONTYPE").ToString()

                    da.SelectCommand.CommandText = "SELECT CLPPROGRAMID,ACCTYPE,ARTICLECODE,SRNO,CARDTYPE,FLAT,AMTVALUE,POINTS,DISCOUNTPER FROM CLPACCUMLATIONITEMDETAIL WHERE STATUS=1 AND CLPPROGRAMID='" & dt.Rows(0)("CLPPROGRAMID").ToString() & "' AND ACCTYPE='" & dt.Rows(0)("ACCUMLATIONTYPE").ToString() & "' And SiteCode='" & SiteCode & "'"
                    dt1 = New DataTable
                    da.Fill(dt1)
                    dt1.TableName = "Detail"

                    da.SelectCommand.CommandText = "SELECT ACCTYPE,SRNO,CARDTYPE,AMTVALUE,POINTS,DISCOUNTPER,ConvertMultiples FROM CLPACCUMLATIONDETAIL WHERE Status=1 and CLPPROGRAMID='" & dt.Rows(0)("CLPPROGRAMID").ToString() & "' AND ACCTYPE='" & dt.Rows(0)("ACCUMLATIONTYPE").ToString() & "'"
                    dtHeader = New DataTable
                    da.Fill(dtHeader)
                    dtHeader.TableName = "Header"

                    dtClpArticleHierarchyMap = New DataTable
                    da.SelectCommand.CommandText = "SELECT CLPPROGRAMID,SITECODE,NODECODE,ISTREE,ACTIVE,CREATEDON,CREATEDBY,CREATEDAT,UPDATEDON,UPDATEDBY,UPDATEDAT,STATUS FROM CLPARTICLEHIERARCHYMAP WHERE STATUS = 1 AND CLPPROGRAMID = '" & dt.Rows(0)("CLPPROGRAMID").ToString() & "' AND SITECODE = '" & SiteCode & "' AND ACTIVE = 1"
                    da.Fill(dtClpArticleHierarchyMap)
                    dtClpArticleHierarchyMap.TableName = "CLPARTICLEHIERARCHYMAP"

                    dtCLPCustomers = New DataTable
                    Dim bOnlyAtCreationSite As Boolean = IIf(ds.Tables("MSTCLPPROGRAM").Rows(0)("onlyAtCreatedSite") Is DBNull.Value, False, ds.Tables("MSTCLPPROGRAM").Rows(0)("onlyAtCreatedSite"))
                    If bOnlyAtCreationSite And strCustomerID <> String.Empty Then
                        da.SelectCommand.CommandText = "SELECT c.SiteCode,c.CardNo,c.ClpProgramId,c.AccountNo,c.CardType,c.CardExpiryDT,c.CardisActive,c.TotalBalancePoint,c.PointsAccumlated,c.PointsRedeemed, c.STATUS FROM CLPCUSTOMERS C join  CLPProgramSiteMap CM on c.ClpProgramId=cm.ClpProgramId and c.sitecode=cm.sitecode WHERE  C.CARDNO = '" & strCustomerID & "' AND C.STATUS = 1 AND C.CLPPROGRAMID = '" & dt.Rows(0)("CLPPROGRAMID").ToString() & "'  AND CardExpiryDT >= getdate() AND CardisActive = 'True'"
                        'da.SelectCommand.CommandText = "SELECT SiteCode,CardNo,ClpProgramId,AccountNo,CardType,CardExpiryDT,CardisActive,TotalBalancePoint,PointsAccumlated,PointsRedeemed, STATUS FROM CLPCUSTOMERS WHERE  CARDNO = '" & strCustomerID & "' AND STATUS = 1 AND CLPPROGRAMID = '" & dt.Rows(0)("CLPPROGRAMID").ToString() & "' AND SITECODE = '" & SiteCode & "' AND CardExpiryDT >= getdate() AND CardisActive = 'True'"
                    ElseIf bOnlyAtCreationSite = False And strCustomerID <> String.Empty Then
                        da.SelectCommand.CommandText = "SELECT SiteCode,CardNo,ClpProgramId,AccountNo,CardType,CardExpiryDT,CardisActive,TotalBalancePoint,PointsAccumlated,PointsRedeemed, STATUS FROM CLPCUSTOMERS WHERE  CARDNO = '" & strCustomerID & "' AND STATUS = 1 AND CLPPROGRAMID = '" & dt.Rows(0)("CLPPROGRAMID").ToString() & "'  AND CardExpiryDT >= getdate() AND CardisActive = 'True'"
                    Else
                        da.SelectCommand.CommandText = "SELECT SiteCode,CardNo,ClpProgramId,AccountNo,CardType,CardExpiryDT,CardisActive,TotalBalancePoint,PointsAccumlated,PointsRedeemed, STATUS FROM CLPCUSTOMERS WHERE  STATUS = 1 AND CLPPROGRAMID = '" & dt.Rows(0)("CLPPROGRAMID").ToString() & "' AND CardExpiryDT >= getdate() AND CardisActive = 'True'"
                    End If

                    da.Fill(dtCLPCustomers)
                    dtCLPCustomers.TableName = "CLPCUSTOMERS"

                    ds.Tables.Add(dtHeader)
                    ds.Tables.Add(dt1)
                    ds.Tables.Add(dtClpArticleHierarchyMap)
                    ds.Tables.Add(dtCLPCustomers)
                    Return ds
                End If
            Else
                'Throw New Exception("Accumlation is not active in Site")
                Throw New ApplicationException(getValueByKey("CLCM01"))
                Return Nothing
            End If
        Catch ax As ApplicationException
            Throw New ApplicationException(getValueByKey("CLCM01"))
            Return Nothing
        Catch ex As Exception
            LogException(ex)
            Throw New Exception(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function GetServiceArticle(ByVal sitecode As String) As DataTable
        Try
            Dim StrQuery As String = "SELECT A.ARTICLECODE,B.EAN,A.ARTICLESHORTNAME,C.SELLINGPRICE  "
            StrQuery = StrQuery & " FROM SalesInfoRecord AS C INNER JOIN MstSite ON C.SiteCode = MstSite.SiteCode RIGHT OUTER JOIN MstArticle AS A INNER JOIN MstEAN AS B ON A.ArticleCode = B.ArticleCode ON MstSite.DefaultEan = B.Discription AND C.EAN = B.EAN"
            StrQuery = StrQuery & " WHERE     (A.ArticalTypeCode = 'SERVICE') AND C.SiteCode = '" & sitecode & "'"

            Dim da As New SqlDataAdapter(StrQuery, ConString)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


    Public Function GetCLPVoucherDenominationDetail(ByVal clpprogram As String, ByVal strSitecode As String) As DataTable
        Try
            Dim dtVoucherDenom As New DataTable
            Dim strQuery As String = "Select A.VoucherCode,B.DenominationAmt, 0.0 as Quantity,c.ExpiryAfterDays   from CLPVoucherMap a inner join MstVoucher c on a.VoucherCode=c.VoucherCode  inner join VoucherDenomination b on a.VoucherCode=b.VoucherCode   WHERE A.SiteCode = '" & strSitecode & "' and A.CLPProgramid = '" & clpprogram & "' and A.STATUS = 1 and b.STATUS = 1 order by DenominationAmt Desc "
            OpenConnection()
            Dim daVoucherDenom As New SqlDataAdapter(strQuery, SpectrumCon)
            daVoucherDenom.Fill(dtVoucherDenom)
            CloseConnection()
            Return dtVoucherDenom

        Catch ex As Exception
            LogException(ex)
            Return Nothing

        End Try
    End Function
    Public Function SaveClpData(ByVal ds As DataSet, ByVal CLPProgramId As String, ByVal CLpCustomerId As String, ByVal TotalPoints As Double, Optional ByVal RedemptionValue As Double = 0, Optional ByVal balancepoint As Double = 0) As Boolean
        Try
            Dim dvCLP As New DataView(ds.Tables("CashMemoDtl"), "CLPRequire=TRUE", "", DataViewRowState.CurrentRows)
            If dvCLP.Count > 0 Then
                Dim dtDtl, dtHdr As DataTable
                dtDtl = dvCLP.ToTable(True, "SiteCode", "BillNo", "BillLineNo", "ArticleCode", "EAN", "SellingPrice", "Quantity", "CLPDiscount", "CLPPoints", "CREATEDAT", "CREATEDBY", "CREATEDON", "UPDATEDAT", "UPDATEDBY", "UPDATEDON", "STATUS")
                dvCLP = New DataView(ds.Tables("CashMemoHdr"), "", "", DataViewRowState.CurrentRows)
                dtHdr = dvCLP.ToTable(True, "SiteCode", "BillNo", "BillDate", "CREATEDAT", "CREATEDBY", "CREATEDON", "UPDATEDAT", "UPDATEDBY", "UPDATEDON", "STATUS")
                AddColumnToDataTable(dtHdr, "AccumLationPoints", "System.Double", TotalPoints)
                AddColumnToDataTable(dtHdr, "RedemptionPoints", "System.Double", RedemptionValue)
                AddColumnToDataTable(dtHdr, "BalAccumlationPoints", "System.Double", TotalPoints)
                AddColumnToDataTable(dtHdr, "ClpProgramId", "System.String", CLPProgramId)
                AddColumnToDataTable(dtHdr, "ClpCustomerId", "System.String", CLpCustomerId)
                AddColumnToDataTable(dtHdr, "IsRedemptionProcess", "System.Boolean", False)
                Dim dsClp As New DataSet
                dtDtl.TableName = "CLPTransactionsDetails"
                dtHdr.TableName = "CLPTransaction"
                dsClp.Tables.Add(dtDtl)
                dsClp.Tables.Add(dtHdr)
                AddMode(dsClp)
                Dim tran As SqlTransaction = Nothing
                OpenConnection()
                tran = SpectrumCon.BeginTransaction()
                If SaveData(dsClp, SpectrumCon, tran) Then
                    If CLpCustomerId = String.Empty Then
                        tran.Commit()
                        CloseConnection()
                        Return True
                    End If
                    'Changed by Rohit today
                    'If UpdateClpPoints(True, CLPProgramId, CLpCustomerId, TotalPoints, SpectrumCon, tran, "", "", "", ServerDate, False, balancepoint) = True Then
                    'For Each drclptender As DataRow In ds.Tables("CashMemoReceipt").Select("TENDERTYPECODE='CLPPoint'")
                    '    CLP_Data.RedeemPassKey(drclptender("RefNo_4").ToString(), tran)
                    'Next
                    If UpdateClpPoints(True, CLPProgramId, CLpCustomerId, TotalPoints, SpectrumCon, tran, "", "", "", ServerDate, False) = True Then
                        tran.Commit()
                        CloseConnection()
                        Return True
                    End If



                    tran.Rollback()
                    CloseConnection()
                    Return False
                End If
                tran.Rollback()
                CloseConnection()
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function GetTaxDetailsForBillNo(ByVal siteCode As String, ByVal billNo As String) As DataTable
        Try
            Dim query As String = "Exec GetTaxInfoForABill '" & siteCode & "', '" & billNo & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetMultipleMrpItem(ByVal siteCode As String, ByVal articleCode As String) As DataTable
        Try
            Dim query As String = "Exec SP_GetDualMrpItem '" & siteCode & "', '" & articleCode & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function CheckIfInclusiveTax(ByVal siteCode As String, ByVal docType As String) As Boolean
        Try
            Dim query As String = "Select TaxCode from TaxSiteDocMap where SiteCode = '" & siteCode & "' And DocumentType='" & docType & "'"
            Dim taxCodeTable As DataTable = GetFilledTable(query)
            If taxCodeTable IsNot Nothing AndAlso taxCodeTable.Rows.Count > 0 Then
                query = "Select TaxCode,Inclusive,TaxType from MstTax where TaxCode = '" & taxCodeTable.Rows(0)(0) & "'"
                Dim dt As DataTable = GetFilledTable(query)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If dt.Rows(0)("Inclusive") = True AndAlso IsDBNull(dt.Rows(0)("TaxType")) Then
                        Return True
                    End If
                End If
            End If
            Return False
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function GetTinNumberForASite(ByVal siteCode As String) As String
        Try
            Dim query As String = "select LocalSalesTaxNo  from MstSite where SiteCode = '" & siteCode & "'"
            Dim dt As DataTable = GetFilledTable(query)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If Not IsDBNull(dt.Rows(0)(0)) Then
                    Return dt.Rows(0)(0)
                End If
            End If
            Return String.Empty
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function

    Public Function GetBardCodesForArticle(ByVal siteCode As String, ByVal articleCode As String) As DataTable
        Try
            Dim query As String = "select BD.BatchBarcode,Ma.ArticleShortName,SI.SellingPrice,(ISNULL(ABBS.TOTALPHYSICALSALEABLEQTY,0) -(ISNULL(ABBS.RESERVEDQTY,0) + ISNULL(ABBS.TOTALPHYSICALNONSALEABLEQTY,0))) as QtyAllocated,BD.LandingCostPrice,ManufacturingDate ,ExpiryDate from BatchDtl BD inner join MstArticle MA on BD.ArticleCode = MA.ArticleCode " & _
                                    "inner Join SalesInfoRecord SI on Bd.SiteCode = Si.SiteCode and BD.ArticleCode = SI.ArticleCode and ISNULL (SI.SrNo ,0) =1   inner join ArticleBatchBinStockBalances ABBS on ABBS.SiteCode= BD.SiteCode  and ABBS.BatchBarcode= bd.BatchBarcode and ABBS.ArticleCode= BD.ArticleCode  " & _
                                    "where BD.SiteCode='" & siteCode & "' and BD.ArticleCode = '" & articleCode & "' "
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetPrinterHierarchyList() As DataTable
        Try
            Dim query As String = "select * from PrinterSubDocMap where status=1"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetHierarchyPrinters(ByVal hierStr As String) As DataTable
        Try
            Dim query As String = "SELECT ArticleCode FROM  mstarticle AS MA inner join dbo.GetLastNodeCodeByNodeName('" & hierStr & "') AS ln ON MA.LastNodeCode = ln.lastnodeCode"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetHierarchyPrintersLastNodeCode(ByVal hierStr As String) As DataTable
        Try
            Dim query As String = " SELECT ArticleCode,ln.lastnodeCode,ln.LastNodeCodeName " &
                                   " FROM  mstarticle AS MA inner join dbo.GetLastNodeCodeByNodeName('" & hierStr & "') AS ln ON MA.LastNodeCode = ln.lastnodeCode"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetHierarchyNodeName(ByVal ArticleCode As String) As DataTable
        Try
            Dim query As String = "SELECT ArticleCode,node.Nodecode,node.NodeName FROM  mstarticle AS MA inner join  MstArticleNode node on ma.LastNodeCode = node.Nodecode where node.ISThisLastNode =1 and node.STATUS =1 and ma.ArticleCode = '" & ArticleCode.ToString() & "' "
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Shared Function GetMRPforEAN(ByVal EanCode As String) As String
        'Dim query As String = " SELECT * FROM  PurchaseInfoRecord Where EAN '" & EanCode & "'"
        Try
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter

            da = New SqlDataAdapter("SELECT MRP FROM  PurchaseInfoRecord Where EAN = '" & EanCode & "'", SpectrumCon)
            da.Fill(dt)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If Not IsDBNull(dt.Rows(0)(0)) Then
                    Return dt.Rows(0)(0)
                End If
            End If
            Return String.Empty
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


    Public Shared Function GetCashMemoBillDetails(ByVal CashMemo As String, ByVal SiteCode As String, ByVal BillType As String, ByVal IsReprinting As Boolean, Optional ByVal ArticleCodeList As String = "", Optional ByVal TaxCodeList As String = "") As DataSet
        Try
            Dim dsCashMemoDetails As New DataSet
            Dim daCashMemoDetails As New SqlDataAdapter

            daCashMemoDetails = New SqlDataAdapter(String.Format("EXEC GetBillDetails '{0}', '{1}', '{2}', {3}, {4}, {5}", CashMemo, SiteCode, BillType, IsReprinting, ArticleCodeList, TaxCodeList), SpectrumCon)
            daCashMemoDetails.Fill(dsCashMemoDetails)

            dsCashMemoDetails.Tables(0).TableName = "PrintingDetails"
            dsCashMemoDetails.Tables(1).TableName = "SiteDetails"
            dsCashMemoDetails.Tables(2).TableName = "SalesPersonDetails"
            dsCashMemoDetails.Tables(3).TableName = "TerminalDetails"

            If (IsReprinting) Then
                dsCashMemoDetails.Tables(4).TableName = "CashMemoHeader"
                dsCashMemoDetails.Tables(5).TableName = "CashMemoDetails"
                dsCashMemoDetails.Tables(6).TableName = "CashMemoReceipts"
                dsCashMemoDetails.Tables(7).TableName = "CashMemoTaxDetails"
                dsCashMemoDetails.Tables(8).TableName = "CashMemoComboDetails"
                dsCashMemoDetails.Tables(9).TableName = "CustomerDetails"
            Else
                dsCashMemoDetails.Tables(4).TableName = "ArticleMaster"
                If dsCashMemoDetails.Tables.Count = 7 Then
                    dsCashMemoDetails.Tables(5).TableName = "TaxMaster"
                    dsCashMemoDetails.Tables(6).TableName = "CustomerDetails"
                Else
                    dsCashMemoDetails.Tables(dsCashMemoDetails.Tables.Count - 1).TableName = "CustomerDetails"
                End If
            End If

            Return dsCashMemoDetails

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Dim daInvoice As New SqlDataAdapter
    Dim dsInvoice As New DataSet
    Dim dtInvoice As DataTable
    Dim SqlQuery As New StringBuilder

    Public Function GetAllDocumentInfoCustmWise(ByVal pSiteCode As String, ByVal pFinYear As String, ByVal pDocumentType As String, ByVal SelectedCustomerCode As String) As DataTable
        Try
            SqlQuery.Length = 0
            If pDocumentType = "SalesOrder" Then
                SqlQuery.Append("Select SaleOrderNumber as DocumentNo, TerminalID,CustomerNo, CustomerType, NetAmt, BalanceAmount " & vbCrLf)
                SqlQuery.Append("from SalesOrderHdr Where SiteCode='" & pSiteCode & "' And FinYear='" & pFinYear & "' and SOStatus not in('Cancel','Return') Order By UpdatedOn Desc" & vbCrLf)

            ElseIf pDocumentType = "BirthList" Then
                SqlQuery.Append("Select BirthListId as DocumentNo, CustomerId as CustomerNo, CustomerType, PaidAmt as NetAmt, SaleInvNumber  " & vbCrLf)
                SqlQuery.Append("from BirthListSalesHdr Where SiteCode='" & pSiteCode & "' And FinYear='" & pFinYear & "' Order By UpdatedOn Desc" & vbCrLf)

            ElseIf pDocumentType = "CashMemo" Then
                SqlQuery.Append(" Select ch.BillDate,ch.BillNo as CashMemoNo, ch.DeliveryPersonID ,ch.TotalItems , ch.NetAmt" & vbCrLf)
                SqlQuery.Append(" From CashMemoHdr ch inner join (select distinct BillNo, BType from CashMemoDtl) cd on ch.BillNo=cd.BillNo " & vbCrLf)
                SqlQuery.Append(" Where ch.SiteCode='" & pSiteCode & "' And ch.FinYear='" & pFinYear & "' And cd.BType='S' " & vbCrLf)

                If Not String.IsNullOrEmpty(SelectedCustomerCode) Then
                    SqlQuery.Append(" AND ")
                    SqlQuery.Append("  CLPNo='" & SelectedCustomerCode & "'")
                End If
                SqlQuery.Append(" Order By  ch.UpdatedOn Desc " & vbCrLf)

            ElseIf pDocumentType = "ReturnCashMemo" Then
                SqlQuery.Append("Select ch.BillNo as DocumentNo, ch.TerminalID, ch.BillDate, IsNull(ch.CLPNo,0) as CustomerNo, " & vbCrLf)
                SqlQuery.Append("ch.NetAmt, ch.PaymentAmt, IsNull(ch.CLPPoints,0) as CLPPoints , ch.TotalDiscount, 'CLP' as CustomerType " & vbCrLf)
                SqlQuery.Append("from CashMemoHdr ch inner join (select distinct BillNo, BType from CashMemoDtl) cd on ch.BillNo=cd.BillNo " & vbCrLf)
                SqlQuery.Append("Where ch.SiteCode='" & pSiteCode & "' And ch.FinYear='" & pFinYear & "' And cd.BType='R' Order By ch.UpdatedOn Desc" & vbCrLf)
            End If

            daInvoice = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtInvoice = New DataTable
            daInvoice.Fill(dtInvoice)

            Return dtInvoice

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    'Code Added by irfan on 17/1/2018 For Mantis issue 2756 CashMemoReturn===========================================================================
    Public Function isApplicableforcashReturn(ByVal billno As String) As Boolean
        Try
            Dim cmd As SqlCommand
            If Not billno Is Nothing Then
                Dim query As String = "select * from CashMemoReceipt where BillNo='" & billno & "' and TenderTypeCode='Cash' and TenderHeadCode='Cash'"
                cmd = New SqlCommand(query.ToString, SpectrumCon)
                SpectrumCon.Open()
                Dim da As New SqlDataAdapter
                Dim dt As New DataTable
                da = New SqlDataAdapter(query.ToString, SpectrumBL.DataBaseConnection.ConString)
                da.Fill(dt)
                SpectrumCon.Close()
                If dt.Rows.Count > 0 Then
                    Return True
                End If
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function ReturnCreditSellAmount(ByVal billno As String) As DataTable
        Try
            If Not String.IsNullOrEmpty(billno) Then
                Dim com As SqlCommand
                Dim query As String = "Select AmountTendered from CashMemoReceipt where billno='" & billno & "' and TenderTypeCode='Credit'"
                com = New SqlCommand(query.ToString)
                SpectrumCon.Open()
                Dim da As New SqlDataAdapter
                Dim dt As New DataTable
                da = New SqlDataAdapter(query.ToString, SpectrumBL.DataBaseConnection.ConString)
                da.Fill(dt)
                SpectrumCon.Close()
                If dt.Rows.Count > 0 Then
                    Return dt
                End If
                Return Nothing
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function


    '==============================================================================================================
    Public Function ReturnAmount(ByVal dtCreditSaleData As DataTable) As DataTable
        Try
            '  Dim cmd As SqlCommand
            If dtCreditSaleData.Rows.Count > 0 Then
                Dim query As String = "select AmountTendered from CashMemoReceipt where BillNo='" & dtCreditSaleData.Rows(0)("BillNo") & "' and TenderTypeCode='Cash' and TenderHeadCode='Cash'"
                'SqlQuery.Append("select AmountTendered,")
                'cmd = New SqlCommand(query.ToString, SpectrumCon)
                SpectrumCon.Open()
                Dim da As New SqlDataAdapter
                Dim dt As New DataTable
                Dim amount As String
                da = New SqlDataAdapter(query.ToString, SpectrumBL.DataBaseConnection.ConString)
                da.Fill(dt)
                'ds.Tables(0).TableName = ""
                'ds.Tables(0).TableName = ""
                SpectrumCon.Close()
                If dt.Rows.Count > 0 Then
                    Return dt
                End If
                Return Nothing
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    '=================================================================================================================================================
    Public Function updateCashMemoReceiptOnReturnItems(ByVal dtBillDetails As DataTable, ByVal iselectedCurrencyIndex As String, ByVal ibaseCurrencyIndex As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction, ByVal terminalid As String, ByVal dayopendate As Date, ByVal NewBillNo As String, Optional ByVal C_Sitecode As String = "") As Boolean
        Try
            If Not dtBillDetails Is Nothing Then
                If dtBillDetails.Rows.Count > 0 Then
                    Dim Billno As String = dtBillDetails.Rows(0)("BillNo")
                    SqlQuery.Append(" UPDATE CashMemoReceipt SET STATUS='0',UpdatedON = getdate()  WHERE BillNo='" & Billno & "'" & vbCrLf)
                    SqlQuery.Append("UPDATE CashMemoDtl SET STATUS='0',UpdatedON = getdate()  WHERE BillNo='" & Billno & "'" & vbCrLf)
                    SqlQuery.Append("UPDATE CashMemoHdr SET STATUS='0',UpdatedON = getdate()  WHERE BillNo='" & Billno & "'")
                    Using cmd As New SqlCommand
                        If Billno.StartsWith("C") Then
                            cmd.CommandText = SqlQuery.ToString
                        End If
                        cmd.Connection = con
                        cmd.Transaction = tran
                        If cmd.ExecuteNonQuery > 0 Then
                            Return True
                        Else
                            Return False
                        End If
                    End Using
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    '===================================================================================================================================================

    Function UpdateCreditSalesOnReturnItems(ByVal dtBillDetails As DataTable, ByVal iselectedCurrencyIndex As String, ByVal ibaseCurrencyIndex As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction, ByVal terminalid As String, ByVal dayopendate As Date, ByVal NewBillNo As String, Optional ByVal C_Sitecode As String = "") As Boolean
        'code for credit sales return
        Try
            If dtBillDetails IsNot Nothing Then
                If dtBillDetails.Rows.Count > 0 Then
                    Dim dtv As New DataView(dtBillDetails, "", "", DataViewRowState.CurrentRows)
                    Dim dt As DataTable = dtv.ToTable(True, "BillNo", "creditsale", "NetCreditSaleAmount", "AdjustedCredit", "CreditSaleAdjustment", "BillInvNo")
                    Dim currencyFactor As Double = CalculateAmountTenderedExchangeRate(iselectedCurrencyIndex, ibaseCurrencyIndex)
                    For Each Bill In dt.Rows
                        Dim NetCreditSale As Double = Bill("creditsale") - Bill("CreditSaleAdjustment")

                        If NetCreditSale > 0 Then
                            If Val(Bill("NetCreditSaleAmount")) <> Val(Bill("creditsale")) Then
                                If Not UpdateCreditSaleAmount(Bill("BillNo"), Bill("NetCreditSaleAmount"), Bill("NetCreditSaleAmount") * currencyFactor, con, tran, terminalid, dayopendate, NewBillNo, Bill("AdjustedCredit"), C_Sitecode, Bill("BillInvNo")) Then
                                    UpdateCreditSalesOnReturnItems = False
                                    Exit Function
                                End If
                            End If
                        End If

                    Next
                End If
            End If
            UpdateCreditSalesOnReturnItems = True
        Catch ex As Exception
            LogException(ex)
            UpdateCreditSalesOnReturnItems = False
        End Try
    End Function



    Public Function UpdateCreditSaleAmount(ByVal Billno As String, ByVal creditsaleamt As Double, ByVal creditsaleamtInCurrency As Double, ByRef con As SqlConnection, ByRef tran As SqlTransaction, ByVal terminalid As String, ByVal dayopendate As Date, ByVal NewBillNo As String, ByVal CreditAdjusted As Double, Optional ByVal C_SiteCode As String = "", Optional ByVal SaleInvNumber As String = "") As Boolean
        Try
            Dim query As String = " UPDATE CashMemoReceipt SET AmountTendered = '" & creditsaleamt & "' , AmountinCurrency = '" & creditsaleamtInCurrency & "',UpdatedON = getdate()  WHERE BillNo='" & Billno & "' and STATUS='1' and TenderTypeCode ='Credit'"
            Dim query1 As String = " UPDATE SalesInvoice SET AmountTendered = '" & creditsaleamt & "', UpdatedON = getdate()  WHERE DocumentNumber='" & Billno & "' and STATUS='1' and TenderTypeCode ='Credit' and SaleInvNumber =  '" & SaleInvNumber & "'"
            Using cmd As New SqlCommand()
                If Billno.StartsWith("C") Then
                    cmd.CommandText = query
                Else
                    cmd.CommandText = query1
                End If
                cmd.Connection = con
                cmd.Transaction = tran
                If cmd.ExecuteNonQuery() > 0 Then
                    If InsertCashMemoCreditUsed(Billno, CreditAdjusted, con, tran, terminalid, dayopendate, NewBillNo, C_SiteCode) Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            End Using
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Public Function InsertCashMemoCreditUsed(ByVal billno As String, ByVal CreditSaleAdjustedAmt As Double, ByRef con As SqlConnection, ByRef tran As SqlTransaction, ByVal terminalid As String, ByVal dayopendate As Date, ByVal NewBillNo As String, Optional C_Sitecode As String = "") As Boolean
        Try
            If String.IsNullOrEmpty(Sitecode) Then
                Sitecode = C_Sitecode
            End If
            Dim CreditUsedNo As String = String.Empty
            Dim DocNo As String = getDocumentNo("CashMemoCreditSaleUsed", Sitecode, con, tran)
            Dim strFinyear As String = GetFinancialYear(dayopendate, Sitecode)
            CreditUsedNo = GenDocNo("CSU" & terminalid & strFinyear.Substring(strFinyear.Length - 2, 2), 15, DocNo)
            Dim strquery As New StringBuilder
            strquery.Append("INSERT INTO CashMemoReturnCreditUsedDtl")
            strquery.Append(" (CreditUsedNo, BillNo, ReturnBillNo, UsedCreditAmount, CREATEDAT, CREATEDBY, CREATEDON, UPDATEDBY, UPDATEDAT, UPDATEDON, Status) ")
            strquery.Append("VALUES   (@creditusedno,@newbillno,@returnbillno,@amount,@createdate,@createdby,getdate(),@updatedby,@updatedate,getdate(),@status)")

            Using cmd As New SqlCommand(strquery.ToString)
                cmd.Parameters.AddWithValue("@creditusedno", CreditUsedNo)
                cmd.Parameters.AddWithValue("@newbillno", NewBillNo)
                cmd.Parameters.AddWithValue("@returnbillno", billno)
                cmd.Parameters.AddWithValue("@amount", CreditSaleAdjustedAmt)
                cmd.Parameters.AddWithValue("@createdate", Sitecode)
                cmd.Parameters.AddWithValue("@createdby", "admin")
                cmd.Parameters.AddWithValue("@updatedby", "admin")
                cmd.Parameters.AddWithValue("@updatedate", Sitecode)
                cmd.Parameters.AddWithValue("@status", "1")
                cmd.Connection = con
                cmd.Transaction = tran
                If cmd.ExecuteNonQuery() > 0 Then
                    If UpdateDocumentNo("CashMemoCreditSaleUsed", con, tran) Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If


            End Using
        Catch ex As Exception
            Return False
            LogException(ex)
        End Try
    End Function

    Public Function GetCashMemoCreditAdjustedData(ByVal billno As String) As DataTable
        Dim query As String = "SELECT  ReturnBillNo AS BillNo ,ISNULL(SUM(UsedCreditAmount),0) AS CreditSale , 0 AS CreditSaleAdjustment FROM CashMemoReturnCreditUsedDtl  Where BillNo = '" & billno & "' GROUP BY ReturnBillNo"
        Using dt As New DataTable
            Using da As New SqlDataAdapter(query.ToString, SpectrumBL.DataBaseConnection.ConString)
                da.Fill(dt)
            End Using
            Return dt
        End Using
    End Function

    Public Function UpdateBalanceAndAdvanceAmt(ByVal Billno As String, ByVal BalanceAmt As Double, ByVal AdvacneAmt As Double, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Boolean
        Dim query As String = " update SalesOrderHdr set BalanceAmount ='" & BalanceAmt & "' ,AdvanceAmt='" & AdvacneAmt & "',UpdatedON = getdate()  where SaleOrderNumber ='" & Billno & "'"

        Using cmd As New SqlCommand()
            cmd.CommandText = query
            cmd.Connection = con
            cmd.Transaction = tran
            If cmd.ExecuteNonQuery() > 0 Then
                Return True
            Else
                Return False
            End If
        End Using
    End Function

    Public Function GetInvoiceWiseCreditAdjusted(ByVal Billno As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Double
        Try
            Dim query As String = "select sum(AmountTendered) from CreditReceipt  where refbillInvNumber ='" & Billno & "'"
            Dim CrdAdjusted As Double = 0

            Using cmd As New SqlCommand()
                cmd.CommandText = query
                cmd.Connection = con
                cmd.Transaction = tran
                CrdAdjusted = cmd.ExecuteScalar
                Return CrdAdjusted
            End Using

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Public Function UpdateCSThroughSalesInvoiceNo(ByVal SalesInvoiceNo As String, ByVal creditsaleamt As Double, ByRef con As SqlConnection, ByRef tran As SqlTransaction, ByVal terminalid As String, ByVal dayopendate As Date, ByVal NewBillNo As String, ByVal CreditAdjusted As Double, Optional ByVal C_SiteCode As String = "") As Boolean
        Try
            Dim query1 As String = " UPDATE SalesInvoice SET AmountTendered = '" & creditsaleamt & "', UpdatedON = getdate()  WHERE SaleInvNumber='" & SalesInvoiceNo & "' and STATUS='1' and TenderTypeCode ='Credit'"
            Using cmd As New SqlCommand()
                cmd.CommandText = query1
                cmd.Connection = con
                cmd.Transaction = tran
                If cmd.ExecuteNonQuery() > 0 Then
                    If InsertCashMemoCreditUsed(SalesInvoiceNo, CreditAdjusted, con, tran, terminalid, dayopendate, NewBillNo, C_SiteCode) Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            End Using
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Saving Float Amt Data to Voucher Expense Entry 
    ''' </summary>
    ''' <param name="FloatAmt"></param>
    ''' <param name="DeliveryPerson"></param>
    ''' <param name="BillNo"></param>
    ''' <param name="BillDate"></param>
    ''' <param name="sitecode"></param>
    ''' <param name="UserID"></param>
    ''' <param name="Fyear"></param>
    ''' <param name="tran"></param>
    ''' <param name="dsVocuher"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function SaveFloatAmountData(ByVal FloatAmt As Double, ByVal DeliveryPerson As String, ByVal BillNo As String, ByVal BillDate As Date, ByVal sitecode As String, ByVal UserID As String, ByVal Fyear As String, ByRef tran As SqlTransaction, ByRef dsVocuher As DataSet) As Boolean
        Try

            Dim cmd As String
            Dim VoucherID As String

            Try
                If String.IsNullOrEmpty(VoucherID) Then
                    Dim DocNo As String = getDocumentNo("PettyCashVoucher", sitecode)
                    VoucherID = GenDocNo("PCV" & sitecode.Substring((sitecode).Length - 3, 3) & (Fyear).Substring((Fyear).Length - 2, 2), 15, DocNo)
                End If

            Catch ex As Exception
                LogException(ex)
                Return False
            End Try


            cmd = "INSERT INTO VoucherHDR ([VoucherID],[VoucherTypeCode],[Sitecode],[FinYear],[ExpenseDate],[TotalAmt],[VoucherAccountID] " & _
           ",[PaidTo],[Currency],[Approvedby],[ReceivedBY],[PreparedBY],[Approvalstatus],[EmployeeID],[SupplierID],[RefDocNumber],[RefDocDate] " & _
           ",[CREATEDAT],[CREATEDBY],[CREATEDON],[UPDATEDAT],[UPDATEDBY],[UPDATEDON],[STATUS]) " & _
            "VALUES ('" & VoucherID & "','" & dsVocuher.Tables("MSTVcherAccTypeFloat").Rows(0)("VoucherTypeCode") & "','" & sitecode & "','" & Fyear & "',@BillDate,'" & FloatAmt & "','" & dsVocuher.Tables("MSTVcherAccTypeFloat").Rows(0)("VoucherAccountID") & "'," & _
            "'','INR','','','',0,'','', '" & BillNo & "',@BillDate, " & _
           "'" & sitecode & "','" & UserID & "', getdate() ,'" & sitecode & "','" & UserID & "',getdate(),1);" & _
           "Insert INTO VoucherDTL (VoucherID,Sitecode,FinYear,LineNumber,Amount," & _
            "Narration,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS) " & _
            "Values ('" & VoucherID & "','" & sitecode & "','" & Fyear & "',1,'" & FloatAmt & "'," & _
           "'" & String.Format(dsVocuher.Tables("MSTVcherAccTypeFloat").Rows(0)("Narration"), DeliveryPerson, BillNo) & "','" & sitecode & "','" & UserID & "',getDate(),'" & sitecode & "','" & UserID & "',getDate() ,1)"

            Dim sqlcmd As New SqlCommand("", SpectrumCon(), tran)
            sqlcmd.CommandText = cmd
            sqlcmd.Parameters.Add("@BillDate", SqlDbType.DateTime)
            sqlcmd.Parameters("@BillDate").Value = BillDate
            If Not sqlcmd.ExecuteNonQuery() > 0 Then
                Return False
            End If

            If UpdateDocumentNo("PettyCashVoucher", SpectrumCon, tran) = False Then
                Return False
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Updating Float Amt Data in Voucher Expense Entry
    ''' </summary>
    ''' <param name="FloatAmt"></param>
    ''' <param name="DeliveryPerson"></param>
    ''' <param name="BillNo"></param>
    ''' <param name="BillDate"></param>
    ''' <param name="tran"></param>
    ''' <param name="dsVocuher"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateFloatAmtData(ByVal FloatAmt As Double, ByVal DeliveryPerson As String, ByVal BillNo As String, ByVal BillDate As Date, ByRef tran As SqlTransaction, ByRef dsVocuher As DataSet) As Boolean

        Try

            Dim sqlquery As String
            sqlquery = "update VoucherHDR set TotalAmt=" & FloatAmt & ",UPDATEDON=GetDate() where RefDocNumber='" & BillNo & "' AND RefDocDate= @BillDate;" & _
                " update VoucherDTL set Amount=" & FloatAmt & ",Narration= '" & String.Format(dsVocuher.Tables("MSTVcherAccTypeFloat").Rows(0)("Narration"), DeliveryPerson, BillNo) & "',UPDATEDON=GetDate() where VoucherID in (select VoucherID from VoucherHDR where RefDocNumber='" & BillNo & "' AND RefDocDate=@BillDate )"

            Dim sqlcmd As New SqlCommand(sqlquery, SpectrumCon(), tran)
            sqlcmd.Parameters.Add("@BillDate", SqlDbType.DateTime)
            sqlcmd.Parameters("@BillDate").Value = BillDate
            If Not sqlcmd.ExecuteNonQuery() > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Fetching Data of Voucher in Dataset
    ''' </summary>
    ''' <param name="RefDocNo"></param>
    ''' <param name="RefDocDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function GetVoucherFloatData(ByVal RefDocNo As String, ByVal RefDocDate As Date, Optional ByRef tran As SqlTransaction = Nothing) As DataSet
        Try
            Dim ds As New DataSet
            Dim StrQuery As String
            StrQuery = "select TotalAmt from VoucherHDR where RefDocNumber='" & RefDocNo & "' AND RefDocDate=@RefDocDate;" & _
                      "select * from MSTVcherAccType where AccountType='Delivery Float';" & _
                      "select * from MSTVcherAccType where AccountType='Delivery Float Return';"

            Dim cmd As New SqlCommand(StrQuery, SpectrumCon(), tran)
            cmd.Parameters.Add("@RefDocDate", SqlDbType.DateTime)
            cmd.Parameters("@RefDocDate").Value = RefDocDate
            Dim daCM As New SqlDataAdapter(cmd)
            daCM.Fill(ds)
            ds.Tables(0).TableName = "VOUCHERHDR"
            ds.Tables(1).TableName = "MSTVcherAccTypeFloat"
            ds.Tables(2).TableName = "MSTVcherAccTypeFloatReturn"
            Return ds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

#End Region

    'Private Function saveKdsData(ByVal billno As String, ByRef con As SqlConnection, ByRef tran As SqlTransaction) As Boolean
    '    Try
    '        Using cmd As New SqlCommand
    '            Dim strInsertQuery As New StringBuilder
    '            cmd.Connection = con
    '            cmd.Transaction = tran
    '            strInsertQuery.Append("insert into PrepStationOrderHdr (SiteCode,FinYear,BillNo,TerminalID,isOrderReady,billdate,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS)  ")
    '            strInsertQuery.Append("select SiteCode,FinYear,BillNo,TerminalID,0,billdate,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS from CashMemoHdr  where BillNo='" & billno & "'")
    '            cmd.CommandText = strInsertQuery.ToString()
    '            If cmd.ExecuteNonQuery() > 0 Then
    '                strInsertQuery.Length = 0
    '                strInsertQuery.Append("insert into PrepStationOrderDtl ")
    '                strInsertQuery.Append("select Null AS MstPrepStationId,SiteCode,FinYear,BillNo,BillLineNo,EAN,ArticleCode,Quantity,0,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS from CashMemoDtl where BillNo='" & billno & "'")
    '                cmd.CommandText = strInsertQuery.ToString()
    '                If cmd.ExecuteNonQuery() > 0 Then
    '                    Return True
    '                End If
    '            Else
    '                Return False
    '            End If
    '        End Using
    '    Catch ex As Exception
    '        LogException(ex)
    '        tran.Rollback()
    '    End Try

    'End Function
    '' added By ketan PC New Tax Invoice After GST Changes 
    Public Function GetPCTaxInvoiceDetails(ByVal SiteCode As String, ByVal BillNo As String) As DataSet
        Dim dsTaxInvoice As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("UDP_GSTCMTaxInvoice", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@BillNo", BillNo)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@SiteCode", SiteCode)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsTaxInvoice = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsTaxInvoice)
            Return dsTaxInvoice
        Catch ex As Exception
            LogException(ex)
            Return dsTaxInvoice
        End Try
    End Function

    Public Function GetPCTaxInvoiceDetailsforFormat11(ByVal SiteCode As String, ByVal BillNo As String) As DataSet
        Dim dsTaxInvoice As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("UDP_GSTCMTaxInvoiceForPrintFormat11", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@BillNo", BillNo)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@SiteCode", SiteCode)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsTaxInvoice = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsTaxInvoice)
            Return dsTaxInvoice
        Catch ex As Exception
            LogException(ex)
            Return dsTaxInvoice
        End Try
    End Function

    Public Function GetPCTaxInvoiceDetailsSplitBill(ByVal SiteCode As String, ByVal BillNo As String, ByVal ISLiqueurPrint As Boolean) As DataSet
        Dim dsTaxInvoice As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("UDP_GSTCMTaxInvoiceSplitBill", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@BillNo", BillNo)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@SiteCode", SiteCode)
            sqlComm.Parameters.AddWithValue("@ISLiqueurPrint", ISLiqueurPrint)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsTaxInvoice = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsTaxInvoice)
            Return dsTaxInvoice
        Catch ex As Exception
            LogException(ex)
            Return dsTaxInvoice
        End Try
    End Function
    Public Function GetArticleLabelDetailsForPrint(ByVal ArticleCode As String, ByVal SiteCode As String, ByVal IsPKDRequired As Boolean, PKDDate As Date) As DataSet
        Dim dsTaxInvoice As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("UDP_LblArticlePrint", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@ArticleCode", ArticleCode)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@SiteCode", SiteCode)
            sqlComm.Parameters.AddWithValue("@PKDDate", PKDDate)
            sqlComm.Parameters.AddWithValue("@IsPKDRequired", IsPKDRequired)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsTaxInvoice = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsTaxInvoice)
            Return dsTaxInvoice
        Catch ex As Exception
            LogException(ex)
            Return dsTaxInvoice
        End Try
    End Function

    Public Function TransferKdsData(ByVal billno As String, ByVal sitecode As String, Optional ByVal terminal As String = "") As Boolean
        Try
            Using cmd As New SqlCommand
                Dim strInsertQuery As New StringBuilder
                OpenConnection()
                cmd.Connection = SpectrumCon()
                cmd.CommandText = "exec TransferKdsData '" & billno & "','" & sitecode & "','" & terminal & "'"
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function TRANSFERDINEINDATATOKDS(ByVal billno As String, ByVal sitecode As String) As Boolean
        Try
            Using cmd As New SqlCommand
                Dim strInsertQuery As New StringBuilder
                OpenConnection()
                cmd.Connection = SpectrumCon()
                cmd.CommandText = "exec TRANSFERDINEINDATATOKDS_DineIn '" & billno & "','" & sitecode & "'"
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function GetUpdateBillTime(ByVal BillNo As String, ByVal TransType As String, ByRef tran As SqlTransaction) As Boolean

        Try
            Dim sqlquery As String
            sqlquery = "exec Udp_UpdateBillsTime '" & BillNo & "','" & TransType & "'"
            Dim sqlcmd As New SqlCommand(sqlquery, SpectrumCon(), tran)
            sqlcmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function GetArticleBarcodeDetailsForPrinting(ByVal ArticleCode As String, ByVal SiteCode As String, ByVal IsPKDRequired As Boolean, PKDDate As Date) As DataTable
        Dim dsBatchBarcode As DataTable = Nothing
        Try
            Dim sqlComm As New SqlCommand("UDP_BarcodePrintFormat4", SpectrumCon)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@SiteCode", SiteCode)
            sqlComm.Parameters.AddWithValue("@BatchBarcode", ArticleCode)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsBatchBarcode = New DataTable
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsBatchBarcode)
            Return dsBatchBarcode
        Catch ex As Exception
            LogException(ex)
            Return dsBatchBarcode
        End Try
    End Function
    Public Function GetArticleLabelDetailsForPrinting(ByVal ArticleCode As String, ByVal SiteCode As String, ByVal IsPKDRequired As Boolean, PKDDate As Date) As DataTable
        Dim dsLabel As DataTable = Nothing
        Try
            Dim sqlComm As New SqlCommand("UDP_LabelPrintFormat4", SpectrumCon)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@SiteCode", SiteCode)
            sqlComm.Parameters.AddWithValue("@BatchBarcode", ArticleCode)
            sqlComm.Parameters.AddWithValue("@PKDDate", PKDDate)
            sqlComm.Parameters.AddWithValue("@IsPKDRequired", IsPKDRequired)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsLabel = New DataTable
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsLabel)
            Return dsLabel
        Catch ex As Exception
            LogException(ex)
            Return dsLabel
        End Try
    End Function
    Public Function AutoCompleteKOT(ByVal SiteCode As String, ByVal billno As String, ByVal TableNo As String)
        Dim tran As SqlTransaction = Nothing
        Try
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            Dim cmd As New System.Text.StringBuilder()
            Dim objComm As New clsCommon
            cmd.Append(" UPDATE PrepStationOrderDtl_KOTWise set AutoComplete=1,isItemReady=1,updatedon=getdate() ")
            cmd.Append(" where BillNo= '" & billno & "' AND sitecode='" & SiteCode & "' and isItemReady=0; ")
            cmd.Append(" UPDATE PrepStationOrderHdr_KOTWise set isOrderReady=1,updatedon=getdate() ")
            cmd.Append(" where BillNo= '" & billno & "' AND sitecode='" & SiteCode & "'")
            If objComm.InsertOrUpdateRecord(cmd.ToString(), tran) = False Then
                tran.Rollback()
                CloseConnection()
                Return False
            End If
            cmd.Length = 0
            tran.Commit()
            CloseConnection()
            Return True
        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
        End Try

    End Function
    'transfer home delivery and take away order from dine-in module to kds screen
    Public Function TRANSFERORDERTOKDS(ByVal billno As String, ByVal sitecode As String, ByVal OrderType As String) As Boolean
        Try
            Using cmd As New SqlCommand
                Dim strInsertQuery As New StringBuilder
                OpenConnection()
                cmd.Connection = SpectrumCon()
                cmd.CommandText = "exec TRANSFERORDERTOKDS '" & billno & "','" & sitecode & "','" & OrderType & "'"
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
   
    Public Function GetTableNameFromTableNumber(ByVal Sitecode As String, ByVal TableNo As String) As String
        Try
            Dim dt As DataTable
            Dim strString As String = "select DineInName from MstDineIn where DineInNumber='" & TableNo & "' and SiteCode='" & Sitecode & "' and status=1"
            Dim cmd As New SqlCommand(strString, SpectrumCon())
            Dim da As New SqlDataAdapter(cmd)
            dt = New DataTable
            da.Fill(dt)
            Dim name As String
            If dt.Rows.Count > 0 Then
                name = dt.Rows(0)("DineInName")
                Return name
            Else
                Return ""
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function SaveHashTagInfo(ByVal ds As DataSet) As Boolean
        Try

            Dim tran As SqlTransaction = Nothing
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If SaveData(ds, SpectrumCon, tran) Then
                tran.Commit()
                CloseConnection()
                Return True
            Else
                tran.Rollback()
                CloseConnection()
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Public Function GetCustomerBillInfoForHashTag(ByVal SiteCode As String, ByVal finYear As String, ByVal billNo As String) As DataSet
        Try
            Dim ds As New DataSet
            daCM = New SqlDataAdapter("select * from CustomerBillInfoForHashTag where SiteCode ='" + SiteCode + "' AND FinYear= '" + finYear + "' AND BILLNO ='" + billNo + "'", SpectrumCon)
            daCM.Fill(ds)
            If Not ds Is Nothing Then
                ds.Tables(0).TableName = "CustomerBillInfoForHashTag"
                Dim KeyHashTag(2) As DataColumn
                KeyHashTag(0) = ds.Tables("CustomerBillInfoForHashTag").Columns("SiteCode")
                KeyHashTag(1) = ds.Tables("CustomerBillInfoForHashTag").Columns("FinYear")
                KeyHashTag(2) = ds.Tables("CustomerBillInfoForHashTag").Columns("Billno")
                ds.Tables("CustomerBillInfoForHashTag").PrimaryKey = KeyHashTag
            End If
            Return ds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function CreateJsonStringForSendingDataToHashtag(ByVal ds As DataSet, ByVal SiteCodeValue As String) As String
        Dim JSONString = New System.Text.StringBuilder()
        Dim strtoken As String = "token"
        If ds.Tables.Count > 0 Then
            JSONString.Append("{")
            JSONString.Append("""" & strtoken & """:" & """" + SiteCodeValue & """,")

            JSONString.Append("""" & "data" & """:{")


            For index = 0 To ds.Tables.Count - 1
                If ds.Tables(index).Rows.Count > 0 Then

                    If index = ds.Tables.Count - 1 Then
                        JSONString.Append("""" & ds.Tables(index).TableName & """:[")
                    Else
                        JSONString.Append("""" & ds.Tables(index).TableName & """:")
                    End If

                    For i As Integer = 0 To ds.Tables(index).Rows.Count - 1
                        JSONString.Append("{")
                        For j = 0 To ds.Tables(index).Columns.Count - 1
                            Dim ColumnDataType = ds.Tables(index).Columns(j).DataType
                            JSONString.Append("""" & ds.Tables(index).Columns(j).ColumnName.ToString().Trim & """:")
                            If j <> ds.Tables(index).Columns.Count - 1 Then
                                If ColumnDataType.Name.ToString.Trim = "Decimal" Then
                                    JSONString.Append(ds.Tables(index).Rows(i)(j).ToString().Trim)
                                Else
                                    JSONString.Append("""" & ds.Tables(index).Rows(i)(j).ToString().Trim & """")
                                End If
                                'If ds.Tables(index).TableName <> "order_items" Then
                                '    JSONString.Append(",")
                                'End If
                                'If ds.Tables(index).TableName = "order_items" AndAlso ds.Tables("order_items").Rows.Count > 1 Then
                                '    JSONString.Append(",")
                                'End If
                                JSONString.Append(",")
                            Else
                                If ColumnDataType.Name.ToString.Trim = "Decimal" Then
                                    JSONString.Append(ds.Tables(index).Rows(i)(j).ToString().Trim)
                                Else
                                    JSONString.Append("""" & ds.Tables(index).Rows(i)(j).ToString().Trim & """")
                                End If
                            End If
                        Next
                        If index <> ds.Tables.Count - 1 Then
                            JSONString.Append("},")
                        Else
                            If i <> ds.Tables(index).Rows.Count - 1 Then
                                JSONString.Append("},")
                            Else
                                JSONString.Append("}")
                            End If

                        End If
                        'If ds.Tables(index).TableName = "order_items" AndAlso ds.Tables("order_items").Rows.Count > 1 Then
                        '    JSONString.Append("}")
                        'End If
                    Next
                    If index <> ds.Tables.Count - 1 Then
                    Else
                        JSONString.Append("]")
                    End If
                End If
            Next
            JSONString.Append("}}")
        End If

        ' Dim myDataSet = JsonConvert.DeserializeObject(DataSet)(JSONString)


        Return JSONString.ToString()
    End Function
    Public Function GetDataForSendingHashtag(ByVal SiteCode As String, ByVal BillNo As String) As DataSet
        Try
            Dim dtData As New DataSet
            Dim daData As New SqlDataAdapter("EXEC UDP_GetDataForSendingHashtag '" & SiteCode & "','" & BillNo & "'", ConString)
            daData.Fill(dtData)
            If Not dtData Is Nothing Then
                dtData.Tables(0).TableName = "customer"
                dtData.Tables(1).TableName = "order"
                dtData.Tables(2).TableName = "order_items"
            End If
            Return dtData
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function UDP_GSTCMTaxInvoiceBrandWise(ByVal SiteCode As String, ByVal BillNo As String, ByVal TreeId As String) As DataSet
        Dim dsTaxInvoice As DataSet = Nothing
        Try
            Dim sqlComm As New SqlCommand("UDP_GSTCMTaxInvoiceBrandWise", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@BillNo", BillNo)
            sqlComm.CommandTimeout = 0
            sqlComm.Parameters.AddWithValue("@SiteCode", SiteCode)
            sqlComm.Parameters.AddWithValue("@Treeid", TreeId)
            sqlComm.CommandType = CommandType.StoredProcedure
            dsTaxInvoice = New DataSet
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dsTaxInvoice)
            Return dsTaxInvoice
        Catch ex As Exception
            LogException(ex)
            Return dsTaxInvoice
        End Try
    End Function
    Public Function GetAllStoreBrand(ByVal SiteCode As String, ByVal BillNo As String) As DataTable

        Try
            Dim dsTaxInvoice As DataTable = Nothing
            Dim sqlquery As String
            sqlquery = "Select distinct B.BrandID,A.NodeName 'BrandName' from MSTARTICLENODE A  "
            sqlquery = sqlquery + " inner join (select B.Articlecode,B.ArticleName,COALESCE(Level7,Level6,Level5,Level4,Level3,Level2) as BrandId "
            sqlquery = sqlquery + " From VW_HIREARCYLEVEL A  inner join mstarticle B on A.level1=B.lastnodecode "
            sqlquery = sqlquery + " INNER JOIN CashMemoDtl CH on B.ArticleCode = CH.ArticleCode and CH.BillNo ='" & BillNo & "' AND CH.SiteCode  ='" & SiteCode & "' "
            sqlquery = sqlquery + " ) B On A.Nodecode=B.BrandId "
            Dim sqlcmd As New SqlCommand(sqlquery, SpectrumCon(), tran)
            dsTaxInvoice = New DataTable
            Dim da As New SqlDataAdapter(sqlcmd)
            da.Fill(dsTaxInvoice)
            Return dsTaxInvoice
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function CheckIsItemAllowToDelete(ByVal Articlecode As String, ByVal EAN As String, ByVal BillNo As String, ByVal BillLineNo As String) As Boolean
        Dim strResult As Boolean = False
        Try
            Dim dt As New DataTable
            Dim sqlComm As New SqlCommand("USP_CheckIsItemAllowToDelete", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@Articlecode", Articlecode)
            sqlComm.Parameters.AddWithValue("@EAN", EAN)
            sqlComm.Parameters.AddWithValue("@BillNo", BillNo)
            sqlComm.Parameters.AddWithValue("@BillLineNo", BillLineNo)
            sqlComm.CommandTimeout = 0
            sqlComm.CommandType = CommandType.StoredProcedure
            dt = New DataTable
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dt)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    strResult = dt.Rows(0)(0).ToString()
                    Return strResult
                End If
            End If
            Return strResult
        Catch ex As Exception
            LogException(ex)
            Return strResult
        End Try
    End Function
    Public Function CheckIsAllowToChangeQty(ByVal Articlecode As String, ByVal EAN As String, ByVal BillNo As String, ByVal Quantity As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim sqlComm As New SqlCommand("USP_CheckIsItemAllowToDecreaseQty", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@Articlecode", Articlecode)
            sqlComm.Parameters.AddWithValue("@EAN", EAN)
            sqlComm.Parameters.AddWithValue("@BillNo", BillNo)
            sqlComm.Parameters.AddWithValue("@Qty", Quantity)
            sqlComm.CommandTimeout = 0
            sqlComm.CommandType = CommandType.StoredProcedure
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return dt
        End Try
    End Function
    Public Function CheckIsKOTGenerate(ByVal Articlecode As String, ByVal EAN As String, ByVal BillNo As String) As Boolean
        Dim strResult As Boolean = False
        Try
            Dim dt As New DataTable
            Dim sqlComm As New SqlCommand("USP_CheckIsKOTGenerate", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@Articlecode", Articlecode)
            sqlComm.Parameters.AddWithValue("@EAN", EAN)
            sqlComm.Parameters.AddWithValue("@BillNo", BillNo)
            sqlComm.CommandTimeout = 0
            sqlComm.CommandType = CommandType.StoredProcedure
            dt = New DataTable
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dt)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    strResult = dt.Rows(0)(0).ToString()
                    Return strResult
                End If
            End If
            Return strResult
        Catch ex As Exception
            LogException(ex)
            Return strResult
        End Try
    End Function
    Public Function IsTableExistInMergeOrder(ByVal siteCode As String, ByVal tableNo As String) As Boolean
        Dim strResult As Boolean = False
        Try
            Dim dt As New DataTable

            Dim sqlComm As New SqlCommand("USP_CheckTableExistInMergeOrder", SpectrumCon)
            sqlComm.Parameters.AddWithValue("@SiteCode", siteCode)
            sqlComm.Parameters.AddWithValue("@TableNo", tableNo)
            sqlComm.CommandTimeout = 0
            sqlComm.CommandType = CommandType.StoredProcedure
            dt = New DataTable
            Dim da As New SqlDataAdapter(sqlComm)
            da.Fill(dt)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    strResult = dt.Rows(0)(0).ToString()
                    Return strResult
                End If
            End If
            Return strResult
        Catch ex As Exception
            LogException(ex)
            Return strResult
        End Try
    End Function

End Class


''' <summary>
''' This Class is Used For Cash Memo Return
''' </summary>
''' <Createdby>Rama Ranjan Jena</Createdby>
''' <UpdatedBy></UpdatedBy>
''' <usedin>CashMemo</usedin>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
Public Class clsCashMemoReturn
    Inherits clsCommon
#Region "Public Function's & Method's"
    ''' <summary>
    ''' Get the Return Reason 
    ''' </summary>
    ''' <param name="DocType">DocType</param>
    ''' <returns>Data Table</returns>
    ''' <remarks></remarks>
    Public Function GetReason(ByVal DocType As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim daReturn As New SqlDataAdapter("SELECT CONVERT(BIT,0) AS [SELECT], REASONCODE,REASONNAME,TRNSEQUENCENAME FROM REASONS where Doctype='" & DocType & "'", ConString)
            daReturn.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Check CashMemo No valid or Not
    ''' </summary>
    ''' <param name="MemoNo">CashMemo</param>
    ''' <param name="MemoDate">Memo Date</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Public Function ValidateCashMemo(ByVal MemoNo As String, ByVal MemoDate As Date, ByRef TrnNo As String) As Boolean
        Try
            Dim cmd1 As New SqlCommand
            cmd1.CommandType = CommandType.Text
            cmd1.CommandText = "SELECT BILLNO FROM CASHMEMOHDR WHERE  CONVERT(VARCHAR(10),BILLDATE,112)= CONVERT(VARCHAR(10),@Pdate,112) and  BILLNO = @MemoNo "
            'cmd1.CommandText = "SELECT CustomerID, CompanyName FROM Customers WHERE CompanyName LIKE @companyName"
            OpenConnection()
            cmd1.Connection = SpectrumCon()

            ' Create a SqlParameter for each parameter in the stored procedure.
            Dim billDate As New SqlParameter("@Pdate", SqlDbType.DateTime)
            Dim strBillno As New SqlParameter("@MemoNo", SqlDbType.VarChar)
            cmd1.Parameters.Add(billDate).Value = MemoDate
            cmd1.Parameters.Add(strBillno).Value = MemoNo
            TrnNo = cmd1.ExecuteScalar()
            CloseConnection()



            'Dim cmd As New SqlCommand("SELECT BILLNO FROM CASHMEMOHDR WHERE  CONVERT(VARCHAR(10),BILLDATE,112)=CONVERT(VARCHAR(10),CONVERT(DATETIME,'" & Format(MemoDate, My.Settings.SqlDateFormat) & "'),112) AND BILLNO='" & MemoNo & "'", SpectrumCon)
            'OpenConnection()
            'cmd.CommandType = CommandType.Text
            'TrnNo = cmd.ExecuteScalar()
            'CloseConnection()
            If TrnNo Is Nothing Then
                Return False
            End If
            Return True
        Catch ex As Exception
            LogException(ex)

            Return False
        End Try
    End Function




    ''' <summary>
    ''' Get CashMemo details 
    ''' </summary>
    ''' <param name="MemoNo">CashMemoNo</param>
    ''' <param name="SiteCode">SiteCode</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetDetails(ByVal MemoNo As String, ByVal SiteCode As String) As DataTable
        Try
            Dim dtData As New DataTable
            Dim daData As New SqlDataAdapter("EXEC GETRETURNDATA '" & SiteCode & "','" & MemoNo & "'", ConString)
            daData.Fill(dtData)
            Return dtData
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function ValidateSO(ByVal SONumber As String, ByVal SiteCode As String, ByRef msg As String) As DataTable
        Try
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            da = New SqlDataAdapter("EXEC GETCLOSEDSODATA  '" & SiteCode & "','" & SONumber & "'", ConString)
            da.Fill(ds)
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)("SOStatus").trim() <> "Closed" Then
                    'msg = "Sales Order is not Closed..... "
                    msg = getValueByKey("CLCM04")
                    Return Nothing
                End If
                'If ds.Tables(1).Rows.Count <= 0 Then
                '    msg = "No Item there in Sales Order to Return....."
                '    Return Nothing
                'End If
                Return ds.Tables(1)
            Else
                'msg = "Sales Order is not valid..... "
                msg = getValueByKey("CLCM05")
                Return Nothing
            End If
        Catch ex As Exception
            LogException(ex)
            msg = ex.Message
        End Try
    End Function
    Public Function ValidateBL(ByVal BLNumber As String, ByVal SiteCode As String, ByRef msg As String) As DataTable
        Try
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            da = New SqlDataAdapter("EXEC GETCLOSEDBLDATA  '" & SiteCode & "','" & BLNumber & "'", ConString)
            da.Fill(ds)
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0)("BirthListStatus").trim().ToString().Contains("Close") = False Then 'Bug no-0000392
                    ' msg = "Birth List is not Closed..... "
                    msg = getValueByKey("CLCM03")
                    Return Nothing
                End If
                For Each dr As DataRow In ds.Tables(1).Rows
                    For Each PriceRow As DataRow In ds.Tables(2).Select("EAN='" & dr("EAN").ToString() & "'", "SaleInvNumber desc", DataViewRowState.CurrentRows)
                        dr("SellingPrice") = PriceRow("Rate")
                        dr("GrossAmt") = dr("Quantity") * dr("SellingPrice")
                        dr("NetAmount") = dr("GrossAmt")
                        dr("OLDNETAMT") = dr("NetAmount")
                        dr("OldETax") = PriceRow("ExclusiveTax")
                    Next
                Next
                Return ds.Tables(1)
            Else
                'msg = "Birth List is not valid..... "
                msg = getValueByKey("CLCM02")
                Return Nothing
            End If
        Catch ex As Exception
            LogException(ex)
            msg = ex.Message
        End Try
    End Function
    Public Function GetClosedBirtHListDetails(ByVal SiteCode As String) As DataTable
        Try
            Dim da As SqlDataAdapter
            Dim dt As New DataTable
            da = New SqlDataAdapter("SELECT BIRTHLISTID,EVENTDATE,CUSTOMERID,Case When CustomerType='CLP' Then CUSTOMERID ELSE '' END AS CUSTOMER FROM BIRTHLIST WHERE BIRTHLISTSTATUS like 'CLOSE%' And SiteCode='" & SiteCode & "' ORDER BY UpdatedOn Desc ", ConString)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function GetClosedSODetails(ByVal SiteCode As String, Optional ByVal IsReturnSales As Boolean = False) As DataTable
        Try
            Dim da As SqlDataAdapter
            Dim dt As New DataTable
            Dim strQry As New StringBuilder
            If IsReturnSales Then
                strQry.Append("SELECT   Sohdr.SaleOrderNumber, CustomerNo, SOHdr.CREATEDON AS Date, NetAmt, GrossAmt, InvoiceCustName, CASE WHEN CustomerType = 'CLP' THEN CUSTOMERNO ELSE '' END AS CUSTOMER ")
                strQry.Append("FROM  SalesOrderHdr AS SOHdr ")
                strQry.Append("INNER Join	(Select Distinct  SiteCode,SaleOrderNumber From SALESORDERDTL SoDtl WITH (NOLOCK) INNER JOIN MSTEAN B  WITH (NOLOCK) ON SoDtl.EAN=B.EAN INNER JOIN  MSTARTICLE C  WITH (NOLOCK) ON B.ARTICLECODE=C.ARTICLECODE ")
                strQry.Append("Where sodtl.SiteCode = '" & SiteCode & "' AND SoDtl.TRANSACTIONCODE<>'SORETURN'  AND ISNULL(C.NONRETUNABLE,0)<>1 AND SoDtl.DeliveredQty > 0 ) AS SODtl ")
                strQry.Append("ON  SOHdr.SiteCode = SoDtl.SiteCode AND SOHdr.SaleOrderNumber = SoDtl.SaleOrderNumber WHERE  (SOStatus = 'CLOSED') AND (Sohdr.SiteCode = '" & SiteCode & "')ORDER BY UPDATEDON DESC ")
            Else
                strQry.Append("SELECT SALEORDERNUMBER,CUSTOMERNO,CREATEDON As Date,NETAMT,GROSSAMT,INVOICECUSTNAME,Case When CustomerType='CLP' Then CUSTOMERNO ELSE '' END AS CUSTOMER FROM SALESORDERHDR WHERE SOSTATUS='CLOSED' And SiteCode='" & SiteCode & "' ORDER BY UpdatedOn Desc")
            End If
            da = New SqlDataAdapter(strQry.ToString, ConString)

            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function getCreditSaleBillData(ByVal returnBills As String) As DataTable
        Try
            Dim query As New StringBuilder
            query.Length = 0
            query.Append(" SELECT CMR.BillNo,BillInvNo,CMR.CreditSale ,ISNULL(CR.CreditSaleAdjustment,0) AS CreditSaleAdjustment")
            query.Append(" FROM ((")
            query.Append(" SELECT CMR.BillNo,BillNo as BillInvNo ,sum(CMR.Amounttendered) AS  CreditSale ")
            query.Append(" FROM   CashMemoReceipt as CMR ")
            query.Append(" Where  CMR.status =1 AND TenderTypeCode ='Credit' ")
            query.Append(" 	   AND CMR.BillNo IN (" & returnBills & ")	")
            query.Append(" GROUP BY CMR.BillNo ) AS CMR Left Outer join ( ")
            query.Append(" 				SELECT CR.RefBillNo ,SUM(CR.AmountTendered) AS CreditSaleAdjustment ")
            query.Append(" 				FROM  CreditReceipt AS CR ")
            query.Append(" 				WHERE CR.status =1 AND CR.RefBillNo IN(" & returnBills & ")")
            query.Append(" 				GROUP BY CR.RefBillNo ) AS CR ON CMR.billno = CR.RefBillNo ")
            query.Append(" ) ")
            query.Append(" Union ALL ")
            query.Append("SELECT    SI.DocumentNumber,SI.SaleInvNumber AS BillInvNo, SUM(SI.AmountTendered) AS CreditSale, ")
            query.Append("(SELECT   ISNULL(SUM(ISNULL(B.AMOUNTTENDERED,0)),0)")
            query.Append("FROM     CreditReceipt AS B  ")
            query.Append("WHere    B.RefBillNo =  SI.DocumentNumber AND  B.RefBillInvNumber = SI.SaleInvNumber AND B.TYPECODE ='SO') AS CreditSaleAdjustment ")
            query.Append("FROM      SalesInvoice AS SI ")
            query.Append("WHERE      (STATUS = 1) AND (TenderTypeCode = 'Credit') AND (DocumentNumber IN (" & returnBills & "))")
            query.Append("GROUP BY DocumentNumber,SaleInvNumber")

            getCreditSaleBillData = New DataTable
            Using da As New SqlDataAdapter(query.ToString(), ConString)
                da.Fill(getCreditSaleBillData)
                Return getCreditSaleBillData
            End Using


        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SpectrumMettlerKotDtl(ByVal SMbillno As String) As DataTable
        Try
            Dim strrquery = "SELECT Billno,Scaleno  AS TerminalId,'' AS DeliveryPersonID,legacyarticlecode as ArticleCode,c_article_name as DISCRIPTION,Quantity FROM SpectrumMettlerDtl WHERE STATUS =1 AND BillNo ='" & SMbillno & "' "
            Dim Sqlda As SqlDataAdapter = New SqlClient.SqlDataAdapter(strrquery.ToString, ConString)
            Dim Sqldt As New DataTable
            Sqlda.Fill(Sqldt)

            Return Sqldt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
        End Try

    End Function
#End Region
End Class