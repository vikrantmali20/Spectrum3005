Imports System.Data.SqlClient

Public Module AcceptPaymentTenderType
    '    Cash
    'Credit Card

    'Gift Voucher
    'MASTER-CARD
    'Parking Coupen
    'VISA-CARD
    'CreditVouc(R)
    ''' <summary>
    '''  Postive TenderType Names
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Public Class PositiveTenderType
        Private Shared _Cash As String = "Cash"
        Private Shared _CreditCard As String = "CreditCard"
        Private Shared _GiftVoucher As String = "GiftVoucher(R)"
        'Private Shared _MASTERCARD As String = "MASTER-CARD"
        'Private Shared _ParkingCoupen As String = "Parking Coupen"
        'Private Shared _VISACARD As String = "VISA-CARD"
        Private Shared _CreditVoucR As String = "CreditVouc(R)"
        Private Shared _CLPPoint As String = "CLPPoint"
        Private Shared _Cheque As String = "Cheque"
        Private Shared _PhonePe As String = "PhonePe"
        ''' <summary>
        '''  PositiveTenderType:Cash
        ''' </summary>
        ''' <value></value>
        ''' <returns>Cash</returns>
        ''' <remarks></remarks>
        Public Shared ReadOnly Property Cash() As String
            Get
                Return _Cash
            End Get
        End Property
        Public Shared ReadOnly Property PhonePe() As String
            Get
                Return _PhonePe
            End Get
        End Property
        Private Shared _Neft As String = "Neft"
        Public Shared ReadOnly Property Neft() As String
            Get
                Return _Neft
            End Get
        End Property
        'Code added by irfan on 10/9/2018
        Private Shared _FoodPandaCash As String = "FoodPandaCash"
        Public Shared ReadOnly Property FoodPandaCash() As String
            Get
                Return _FoodPandaCash
            End Get
        End Property
        Private Shared _FoodPandaOnline As String = "FoodPandaOnline"
        Public Shared ReadOnly Property FoodPandaOnline() As String
            Get
                Return _FoodPandaOnline
            End Get
        End Property

        Private Shared _Rtgs As String = "Rtgs"
        Public Shared ReadOnly Property Rtgs() As String
            Get
                Return _Rtgs
            End Get
        End Property

        Private Shared _MealPass As String = "MealPass"
        Public Shared ReadOnly Property MealPass() As String
            Get
                Return _MealPass
            End Get
        End Property

        Private Shared _Paytm As String = "Paytm"
        Public Shared ReadOnly Property Paytm() As String
            Get
                Return _Paytm
            End Get
        End Property
        Private Shared _QuickWallet As String = "QuickWallet"
        Public Shared ReadOnly Property QuickWallet() As String
            Get
                Return _QuickWallet
            End Get
        End Property
        Private Shared _JioMoney As String = "JioMoney"
        Public Shared ReadOnly Property JioMoney() As String
            Get
                Return _JioMoney
            End Get
        End Property
        'code added on 31-07-2017 by vipul
        Private Shared _Sodexo As String = "Sodexo"
        Public Shared ReadOnly Property Sodexo() As String
            Get
                Return _Sodexo
            End Get
        End Property
        Private Shared _Tr As String = "Tr"
        Public Shared ReadOnly Property Tr() As String
            Get
                Return _Tr
            End Get
        End Property

        Private Shared _TicketRestaurant As String = "TktRestaurant"
        Public Shared ReadOnly Property TicketRestaurant() As String
            Get
                Return _TicketRestaurant
            End Get
        End Property
        Private Shared _SwiggyOnline As String = "SwiggyOnline"
        Public Shared ReadOnly Property SwiggyOnline() As String
            Get
                Return _SwiggyOnline
            End Get
        End Property

        Private Shared _ZomatoOnline As String = "ZomatoOnline"
        Public Shared ReadOnly Property ZomatoOnline() As String
            Get
                Return _ZomatoOnline
            End Get
        End Property
        Private Shared _OnlinePayment As String = "OnlinePayment"
        Public Shared ReadOnly Property OnlinePayment() As String
            Get
                Return _OnlinePayment
            End Get
        End Property

        Private Shared _Others As String = "Others"
        Public Shared ReadOnly Property Others() As String
            Get
                Return _Others
            End Get
        End Property

        Private Shared _MobiKwik As String = "MobiKwik"
        Public Shared ReadOnly Property MobiKwik() As String
            Get
                Return _MobiKwik
            End Get
        End Property
        Private Shared _DynTenderType As String
        Public Shared Property DynTenderType() As String
            Get
                Return _DynTenderType
            End Get
            Set(ByVal value As String)
                _DynTenderType = value
            End Set
        End Property
        ''' <summary>
        '''  PositiveTenderType:Cash
        ''' </summary>
        ''' <value></value>
        ''' <returns>Cash</returns>
        ''' <remarks></remarks>
        Public Shared ReadOnly Property Cheque() As String
            Get
                Return _Cheque
            End Get
        End Property



        ''' <summary>
        '''  PositiveTenderType:Cash
        ''' </summary>
        ''' <value></value>
        ''' <returns>Cash</returns>
        ''' <remarks></remarks>
        Public Shared ReadOnly Property CLPPoint() As String
            Get
                Return _CLPPoint
            End Get
        End Property
        ''' <summary>
        '''  PositiveTenderType:GiftVoucher
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared ReadOnly Property GiftVoucher() As String
            Get
                Return _GiftVoucher
            End Get
        End Property
        ' ''' <summary>
        ' '''  PositiveTenderType:MASTERCARD
        ' ''' </summary>
        ' ''' <value></value>
        ' ''' <returns></returns>
        ' ''' <remarks></remarks>
        'Public Shared ReadOnly Property MASTERCARD() As String
        '    Get
        '        Return _MASTERCARD
        '    End Get
        'End Property
        ' ''' <summary>
        ' '''  PositiveTenderType:ParkingCoupen
        ' ''' </summary>
        ' ''' <value></value>
        ' ''' <returns></returns>
        ' ''' <remarks></remarks>
        'Public Shared ReadOnly Property ParkingCoupen() As String
        '    Get
        '        Return _ParkingCoupen
        '    End Get
        'End Property
        ''' <summary>
        ' '''  PositiveTenderType:VISACARD
        ' ''' </summary>
        ' ''' <value></value>
        ' ''' <returns></returns>
        ' ''' <remarks></remarks>
        'Public Shared ReadOnly Property VISACARD() As String
        '    Get
        '        Return _VISACARD
        '    End Get
        'End Property
        ''' <summary>
        '''  PositiveTenderType:CreditVouc(R)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared ReadOnly Property CreditVoucR() As String
            Get
                Return _CreditVoucR
            End Get
        End Property
        ''' <summary>
        '''  PositiveTenderType:CreditCard
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared ReadOnly Property CreditCard() As String
            Get
                Return _CreditCard
            End Get
        End Property

        Public Shared ReadOnly Property Credit() As String
            Get
                Return "Credit"
            End Get
        End Property
        'code added by vipul on 15-05-2018
        Private Shared _Payso As String = "Payso"
        Public Shared ReadOnly Property Payso() As String
            Get
                Return _Payso
            End Get
        End Property

        Private Shared _SodexoCards As String = "SodexoCards"
        Public Shared ReadOnly Property SodexoCards() As String
            Get
                Return _SodexoCards
            End Get
        End Property
        Private Shared _SodexoCpn As String = "SodexoCpn"
        Public Shared ReadOnly Property SodexoCpn() As String
            Get
                Return _SodexoCpn
            End Get
        End Property
        Private Shared _ZomatoCash As String = "ZomatoCash"
        Public Shared ReadOnly Property ZomatoCash() As String
            Get
                Return _ZomatoCash
            End Get
        End Property
        Private Shared _SwiggyCash As String = "SwiggyCash" 'vipul
        Public Shared ReadOnly Property SwiggyCash() As String
            Get
                Return _SwiggyCash
            End Get
        End Property
        '----------------
        Private Shared _ScootsyOnline As String = "ScootsyOnline" 'vipul 24-08-2018
        Public Shared ReadOnly Property ScootsyOnline() As String
            Get
                Return _ScootsyOnline
            End Get
        End Property
        Private Shared _ScootsyCash As String = "ScootsyCash"
        Public Shared ReadOnly Property ScootsyCash() As String
            Get
                Return _ScootsyCash
            End Get
        End Property
        Private Shared _UberEatsOnline As String = "UberEatsOnline"
        Public Shared ReadOnly Property UberEatsOnline() As String
            Get
                Return _UberEatsOnline
            End Get
        End Property
        Private Shared _UberEatsCash As String = "UberEatsCash"
        Public Shared ReadOnly Property UberEatsCash() As String
            Get
                Return _UberEatsCash
            End Get
        End Property

    End Class
    Public Class NegativeTenderType
        ''' <summary>
        ''' Cash(R)
        ''' </summary>
        ''' <remarks></remarks>
        Private Shared _Cash As String = "Cash(R)"
        ''' <summary>
        ''' CreditVouc(I)
        ''' </summary>
        ''' <remarks></remarks>
        Private Shared _CreditVoucherI As String = "CreditVouc(I)"
        Private Shared _GiftVoucherI As String = "GiftVoucher(I)"
        'Commented by rama on sept 15 as there is only cash tender type no cash(R)or cash(return) these are tender's

        'Public Shared ReadOnly Property Cash() As String
        '    Get
        '        Return _Cash
        '    End Get
        'End Property
        Public Shared ReadOnly Property CreditVoucherI() As String
            Get
                Return _CreditVoucherI
            End Get
        End Property
        Public Shared ReadOnly Property GiftVoucherI() As String
            Get
                Return _GiftVoucherI
            End Get
        End Property

        Public Shared ReadOnly Property CashR() As String
            Get
                Return _Cash
            End Get
        End Property
    End Class


End Module
Public Class clsAcceptPayment
    Inherits clsCommon


    'Rakesh: 28-05-09 - Add same receipt type with diffrent currency 
    Dim vCurrencyCode As String = String.Empty



    'Dim objclsLogin As New clsLogin()
    Protected dsRecieptType As New DataSet()
    Private dtRecieptType As New DataTable()
    ''' <summary>
    ''' Payment type structure
    ''' </summary>
    '''<usedby>frmAcceptPayment.vb</usedby>
    ''' <remarks></remarks>
    Public Enum PaymentType
        Accept = 0 'Dont Change
        Advance = 1 'Dont change
        EditBill = 2 'Dont Change
        CreditBill = 3 'Dont Change
    End Enum


    Private _strRemarks As String

    Public Property strRemarks As String
        Get
            Return _strRemarks
        End Get
        Set(ByVal value As String)
            _strRemarks = value
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
    '--------PC SO----------------
    Private _ReferenceNo As String

    Public Property ReferenceNo As String
        Get
            Return _ReferenceNo
        End Get
        Set(ByVal value As String)
            _ReferenceNo = value
        End Set
    End Property

    Private _TenderRemarks As String

    Public Property TenderRemarks As String
        Get
            Return _TenderRemarks
        End Get
        Set(ByVal value As String)
            _TenderRemarks = value
        End Set
    End Property

    Private _MainRemarks As String

    Public Property MainRemarks As String
        Get
            Return _MainRemarks
        End Get
        Set(ByVal value As String)
            _MainRemarks = value
        End Set
    End Property
    Private _BankName As String
    Public Property BankName As String
        Get
            Return _BankName
        End Get
        Set(ByVal value As String)
            _BankName = value
        End Set
    End Property
    Private _ChequeNo As String

    Public Property ChequeNo As String
        Get
            Return _ChequeNo
        End Get
        Set(ByVal value As String)
            _ChequeNo = value
        End Set
    End Property
    Private _ChequeDate As String

    Public Property ChequeDate As String
        Get
            Return _ChequeDate
        End Get
        Set(ByVal value As String)
            _ChequeDate = value
        End Set
    End Property
    Private _MICRNo As String

    Public Property MICRNo As String
        Get
            Return _MICRNo
        End Get
        Set(ByVal value As String)
            _MICRNo = value
        End Set
    End Property

    Private _IsNewSalesOrder As String 'PC SO Merge vipin 02-05-2018

    Public Property IsNewSalesOrder As String
        Get
            Return _IsNewSalesOrder
        End Get
        Set(ByVal value As String)
            _IsNewSalesOrder = value
        End Set
    End Property
    '-------------------------
    ''' <summary>
    '''  Get receipt type's according to PymentType
    ''' </summary>
    ''' <param name="paymentType"></param>
    ''' <returns>DataTable</returns>
    '''<usedby>frmAcceptPayment.vb</usedby>
    ''' <remarks>on 16.04.2009 changed by ram . table is changed from mstTenderType to MstTender</remarks>
    Public Function LoadRecieptType(ByVal paymentType As PaymentType, ByVal PLocalSiteCode As String, Optional innovitiEnable As Boolean = False) As DataTable
        'Dim strSign As String = "+"
        'If paymentType = clsAcceptPayment.PaymentType.CreditBill Then
        '    strSign = String.Empty
        '    strSign = "-"
        'End If
        If (paymentType = paymentType.Accept Or paymentType = clsAcceptPayment.PaymentType.Advance Or paymentType = clsAcceptPayment.PaymentType.CreditBill) Then
            Dim datasetRecieptType As New DataSet
            Try
                OpenConnection()
                'Dim sqlSelectCommand As New SqlCommand("select tendertype,Description from MstTenderType", SpectrumCon)
                'Dim sqlSelectCommand As New SqlCommand("select TenderHeadCode,TenderHeadName,TenderType,Positive_Negative,MaxNo,MaxValue,MinBillValue,DefaultValue from MstTender where  Status = 'True' ", SpectrumCon)
                ' local site is add to take care of site
                Dim sqlSelectCommand As New SqlCommand("select TenderHeadCode,TenderHeadName,TenderType,Positive_Negative,MaxNo,MaxValue,MinBillValue,DefaultValue from MstTender where sitecode = '" & PLocalSiteCode & "' and  Status = 'True' ", SpectrumCon)
                Dim sqlAdapter As New SqlDataAdapter()
                sqlAdapter.SelectCommand = sqlSelectCommand
                sqlAdapter.Fill(datasetRecieptType, "tblRecieptType")
                'added by khusrao adil on 25-07-2018 for innviti with card functionality point _ natural client
                If innovitiEnable Then
                    Dim drInnoTender As DataRow = datasetRecieptType.Tables("tblRecieptType").NewRow
                    Dim drResult = datasetRecieptType.Tables("tblRecieptType").Select("TenderType='CreditCard'")
                    drInnoTender("TenderHeadCode") = "Card Inv"
                    drInnoTender("TenderHeadName") = "Card Inv"
                    drInnoTender("TenderType") = drResult(0)("TenderType")
                    drInnoTender("Positive_Negative") = drResult(0)("Positive_Negative")
                    drInnoTender("MaxNo") = drResult(0)("MaxNo")
                    drInnoTender("MaxValue") = drResult(0)("MaxValue")
                    drInnoTender("MinBillValue") = drResult(0)("MinBillValue")
                    drInnoTender("DefaultValue") = drResult(0)("DefaultValue")
                    datasetRecieptType.Tables("tblRecieptType").Rows.Add(drInnoTender)
                    Dim dvPayment As DataView
                    dvPayment = New DataView(datasetRecieptType.Tables("tblRecieptType"), "", "TenderType asc", DataViewRowState.CurrentRows)
                    Dim dt As DataTable = dvPayment.ToTable()
                    Return dt
                End If
                Return datasetRecieptType.Tables("tblRecieptType")
            Catch ex As Exception
                LogException(ex)
                Return Nothing
            Finally
                CloseConnection()
            End Try
        Else
            Return Nothing
        End If
    End Function
    ''' <summary>
    ''' Get Currencies applicable for site
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''<usedby>frmAcceptPayment.vb</usedby>
    Public Function LoadCurrency(ByVal SiteCode As String) As DataTable
        Dim datasetRecieptType As New DataSet

        Try
            OpenConnection()
            Dim str As String = "SELECT CURRENCYCODE,CURRENCYSYMBOL,CURRENCYDESCRIPTION FROM MSTCURRENCY A INNER JOIN MSTSITE B ON A.CURRENCYCODE=B.LOCALCURRANCYCODE WHERE B.SITECODE='" & SiteCode & "' " & _
            " UNION " & _
            " SELECT A.CURRENCYCODE,A.CURRENCYSYMBOL,A.CURRENCYDESCRIPTION FROM MSTCURRENCY A INNER JOIN	MSTSITECURRANCYMAP B ON A.CURRENCYCODE=B.LOCALCURRANCYCODE AND B.STATUS=1 WHERE A.STATUS=1 AND B.SITECODE='" & SiteCode & "' "
            'Dim sqlSelectCommand As New SqlCommand("select currencyCode,CurrencySymbol,CurrencyDescription from MSTCurrency", SpectrumCon)
            Dim sqlSelectCommand As New SqlCommand(str, SpectrumCon)
            Dim sqlAdapter As New SqlDataAdapter()
            sqlAdapter.SelectCommand = sqlSelectCommand
            sqlAdapter.Fill(datasetRecieptType, "tblCurrency")
            Return datasetRecieptType.Tables("tblCurrency")
        Catch ex As Exception
            LogException(ex)
            Return datasetRecieptType.Tables(0)
        Finally
            CloseConnection()
        End Try
    End Function
    ''' <summary>
    ''' Table structure for  storing payment breakups
    ''' 
    ''' </summary>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    '''<usedby>frmAcceptPayment.vb</usedby>
    Public Function GetDataset() As DataSet
        Try
            dtRecieptType.TableName = "MSTRecieptType"
            dtRecieptType.AcceptChanges()
            If dtRecieptType.Columns.Count > 0 Then
                dsRecieptType.Tables.Clear()
                dsRecieptType.Tables.Add(dtRecieptType)
                dtRecieptType.AcceptChanges()
                dsRecieptType.AcceptChanges()
                Return dsRecieptType
            End If
            Dim dcSrno As New DataColumn("SrNo")
            dcSrno.ReadOnly = True
            dcSrno.DataType = GetType(Integer)
            dcSrno.AutoIncrement = True
            dcSrno.AutoIncrementStep = 1
            dcSrno.AutoIncrementSeed = 1
            dtRecieptType.Columns.Add(dcSrno)
            Dim primaryKey(0) As DataColumn
            primaryKey(0) = dtRecieptType.Columns("SrNo")
            dtRecieptType.PrimaryKey = primaryKey
            dtRecieptType.Columns.Add(New DataColumn("Reciept"))
            dtRecieptType.Columns.Add(New DataColumn("RecieptType"))
            dtRecieptType.Columns.Add(New DataColumn("Amount", System.Type.GetType("System.Decimal")))
            dtRecieptType.Columns.Add(New DataColumn("AmountInCurrency", System.Type.GetType("System.Decimal")))
            dtRecieptType.Columns.Add(New DataColumn("Number")) 'CreditCard, Gift Voucher number 
            dtRecieptType.Columns.Add(New DataColumn("Date", System.Type.GetType("System.DateTime"))) ' Expiry Date 
            dtRecieptType.Columns.Add(New DataColumn("RecieptTypeCode"))
            dtRecieptType.Columns.Add(New DataColumn("ExchangeRate"))
            dtRecieptType.Columns.Add(New DataColumn("CurrencyCode"))
            dtRecieptType.Columns.Add(New DataColumn("RefNo_3"))
            dtRecieptType.Columns.Add(New DataColumn("RefNo_4"))
            dtRecieptType.Columns.Add(New DataColumn("BankAccNo"))
            dtRecieptType.Columns.Add(New DataColumn("NOCLP", System.Type.GetType("System.Boolean")))
            dtRecieptType.Columns.Add(New DataColumn("IssuedForCLP", System.Type.GetType("System.Boolean")))
            dtRecieptType.Columns("IssuedForCLP").DefaultValue = False
            ''added by ketan
            dtRecieptType.Columns.Add(New DataColumn("PaymentTermName"))

            '' added by nikhil
            'If IsNewSalesOrderInternal Then
            dtRecieptType.Columns.Add(New DataColumn("TenderType"))
            dtRecieptType.Columns.Add(New DataColumn("BankName"))
            dtRecieptType.Columns.Add(New DataColumn("CardNo")) 'CreditCard, Gift Voucher number 
            dtRecieptType.Columns.Add(New DataColumn("ChequeNo")) '' , System.Type.GetType("System.s"))) ' Expiry Date 
            dtRecieptType.Columns.Add(New DataColumn("MICRNo"))
            dtRecieptType.Columns.Add(New DataColumn("ChequeDate"))
            dtRecieptType.Columns.Add(New DataColumn("NEFTReferenceNo"))
            dtRecieptType.Columns.Add(New DataColumn("RTGSReferenceNo"))
            dtRecieptType.Columns.Add(New DataColumn("CreditVoucherNo"))
            dtRecieptType.Columns.Add(New DataColumn("Remarks"))
            '  End If



            dsRecieptType.Tables.Clear()
            dsRecieptType.Tables.Add(dtRecieptType)
            dtRecieptType.AcceptChanges()
            dsRecieptType.AcceptChanges()
            Return dsRecieptType
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    ''' <summary>
    ''' For CreditCard
    '''  adding row into grid row when receipt type is only creditcard
    ''' </summary>
    ''' <param name="strRecieptType"></param>
    ''' <param name="strAmount"></param>
    ''' <param name="strNumber"></param>
    ''' <param name="dtDate"></param>
    ''' <param name="iRecieptTypeCode"></param>
    ''' <returns>DataSet</returns>
    '''<usedby>frmAcceptPayment.vb</usedby>
    ''' <remarks> parameter irecieptype is changed to string on 20.04.2008</remarks>
    Public Function AddRowRecipetAmountInGrid(ByVal Reciept As String, ByVal BaseCurrency As String, ByVal strRecieptType As String, ByVal strAmount As String, ByVal strNumber As String, ByVal dtDate As Object, ByVal iRecieptTypeCode As String, Optional ByVal dDueDate As Date = Nothing, Optional ByVal strRemarks As String = "", Optional ByVal bankAccNo As String = "") As DataSet
        Try
            '    If IsNewSalesOrder Then
            ' If (dsRecieptType.Tables.Count > 0) Then
            ' Else
            GetDataset()
            ' End If

            If Not dsRecieptType.Tables.Contains("CheckDtls") And strRecieptType = "CreditCheque" Then
                dsRecieptType.Tables.Add(CreateCheckDtls())
            End If

            Dim drNewCashDetail As DataRow
            Dim drNewCheckdtl As DataRow

            drNewCashDetail = dtRecieptType.NewRow()

            If String.IsNullOrEmpty(bankAccNo) = False Then
                drNewCashDetail.Item("BankAccNo") = bankAccNo
            End If

            If Reciept = "Check" Then 'vipin 05.12.2017
                drNewCashDetail.Item("Reciept") = strRecieptType
            Else
                drNewCashDetail.Item("Reciept") = Reciept
            End If

            drNewCashDetail.Item("RecieptType") = strRecieptType
            drNewCashDetail.Item("Amount") = strAmount


            drNewCashDetail.Item("Number") = strNumber
            If Not dtDate Is Nothing Then
                drNewCashDetail.Item("Date") = dtDate
            End If

            'Added by Rohit for CR-5938
            If Not dDueDate = Nothing Then
                drNewCashDetail.Item("Date") = dDueDate
            End If
            'Added by Rohit for CR-5938

            drNewCashDetail.Item("RecieptTypeCode") = iRecieptTypeCode
            drNewCashDetail.Item("CurrencyCode") = BaseCurrency

            If Not strRemarks Is Nothing AndAlso strRemarks <> String.Empty Then
                drNewCashDetail.Item("RefNo_3") = strRemarks
                'Else
                '    drNewCashDetail.Item("RefNo_3") = strNumber
            End If

            ''''''''  added by nikhil '''''''
            If String.IsNullOrEmpty(BankName) = False Then
                drNewCashDetail.Item("BankName") = BankName
            Else
                drNewCashDetail.Item("BankName") = "-"
            End If
            drNewCashDetail.Item("TenderType") = strRecieptType
            drNewCashDetail.Item("Amount") = strAmount
            If strNumber <> "" Then
                drNewCashDetail.Item("CardNo") = strNumber
            Else
                drNewCashDetail.Item("CardNo") = "-"
            End If


            If MainRemarks <> "" Then
                drNewCashDetail.Item("Remarks") = MainRemarks
            Else
                MainRemarks = "-"
                drNewCashDetail.Item("Remarks") = MainRemarks
            End If
            If strRecieptType = "Cheque" Then
                If ChequeNo <> "" Then
                    drNewCashDetail.Item("ChequeNo") = ChequeNo
                Else
                    drNewCashDetail.Item("ChequeNo") = "-"
                End If
                If MICRNo <> "" Then
                    drNewCashDetail.Item("MICRNo") = MICRNo
                Else
                    drNewCashDetail.Item("MICRNo") = "-"
                End If
                If ChequeDate <> "" Then
                    drNewCashDetail.Item("ChequeDate") = ChequeDate
                Else
                    drNewCashDetail.Item("ChequeDate") = "-"
                End If

            Else
                drNewCashDetail.Item("ChequeNo") = "-"
                drNewCashDetail.Item("MICRNo") = "-"
                drNewCashDetail.Item("ChequeDate") = "-"
            End If

            drNewCashDetail.Item("NEFTReferenceNo") = "-"
            drNewCashDetail.Item("RTGSReferenceNo") = "-"
            drNewCashDetail.Item("CreditVoucherNo") = "-"



            'to handle the expiry date of issue gv
            If strAmount < 0 And strRecieptType.Contains("GiftVoucher(I)") Then
                drNewCashDetail.Item("Date") = dtRecieptType.Compute("MAX(date)", " RecieptTypeCode = 'GiftVoucher(R)' Or  RecieptTypeCode ='CreditVouc(R)'")
                If IsDBNull(drNewCashDetail.Item("Date")) Then
                    If Not dtDate Is Nothing Then
                        drNewCashDetail.Item("Date") = dtDate
                    End If
                End If
            End If
            'to handle the expiry date of issue gv

            'Added by Rohit for CR-5938
            If dsRecieptType.Tables.Contains("CheckDtls") And strRecieptType = "CreditCheque" Then
                drNewCheckdtl = dsRecieptType.Tables("CheckDtls").NewRow()
                drNewCheckdtl("PayLineNo") = drNewCashDetail("SrNo")
                drNewCheckdtl("CheckNo") = strNumber
                drNewCheckdtl("Amount") = strAmount
                drNewCheckdtl("DueDate") = dDueDate
                drNewCheckdtl("Remarks") = strRemarks
                drNewCheckdtl("STATUS") = 1


                dsRecieptType.Tables("CheckDtls").Rows.Add(drNewCheckdtl)
                dsRecieptType.Tables("CheckDtls").AcceptChanges()
            End If
            'Added by Rohit for CR-5938

            dtRecieptType.Rows.Add(drNewCashDetail)

            dtRecieptType.AcceptChanges()
            dsRecieptType.AcceptChanges()
            Return dsRecieptType
            'Else
            'If (dsRecieptType.Tables.Count > 0) Then
            'Else
            '    GetDataset()
            'End If

            'If Not dsRecieptType.Tables.Contains("CheckDtls") And strRecieptType = "CreditCheque" Then
            '    dsRecieptType.Tables.Add(CreateCheckDtls())
            'End If

            'Dim drNewCashDetail As DataRow
            'Dim drNewCheckdtl As DataRow

            'drNewCashDetail = dtRecieptType.NewRow()

            'If String.IsNullOrEmpty(bankAccNo) = False Then
            '    drNewCashDetail.Item("BankAccNo") = bankAccNo
            'End If

            'drNewCashDetail.Item("Reciept") = Reciept
            'drNewCashDetail.Item("RecieptType") = strRecieptType
            'drNewCashDetail.Item("Amount") = strAmount


            'drNewCashDetail.Item("Number") = strNumber
            'If Not dtDate Is Nothing Then
            '    drNewCashDetail.Item("Date") = dtDate
            'End If

            ''Added by Rohit for CR-5938
            'If Not dDueDate = Nothing Then
            '    drNewCashDetail.Item("Date") = dDueDate
            'End If
            ''Added by Rohit for CR-5938

            'drNewCashDetail.Item("RecieptTypeCode") = iRecieptTypeCode
            'drNewCashDetail.Item("CurrencyCode") = BaseCurrency

            'If Not strRemarks Is Nothing AndAlso strRemarks <> String.Empty Then
            '    drNewCashDetail.Item("RefNo_3") = strRemarks
            '    'Else
            '    '    drNewCashDetail.Item("RefNo_3") = strNumber
            'End If

            ''to handle the expiry date of issue gv
            'If strAmount < 0 And strRecieptType.Contains("GiftVoucher(I)") Then
            '    drNewCashDetail.Item("Date") = dtRecieptType.Compute("MAX(date)", " RecieptTypeCode = 'GiftVoucher(R)' Or  RecieptTypeCode ='CreditVouc(R)'")
            '    If IsDBNull(drNewCashDetail.Item("Date")) Then
            '        If Not dtDate Is Nothing Then
            '            drNewCashDetail.Item("Date") = dtDate
            '        End If
            '    End If
            'End If
            ''to handle the expiry date of issue gv

            ''Added by Rohit for CR-5938
            'If dsRecieptType.Tables.Contains("CheckDtls") And strRecieptType = "CreditCheque" Then
            '    drNewCheckdtl = dsRecieptType.Tables("CheckDtls").NewRow()
            '    drNewCheckdtl("PayLineNo") = drNewCashDetail("SrNo")
            '    drNewCheckdtl("CheckNo") = strNumber
            '    drNewCheckdtl("Amount") = strAmount
            '    drNewCheckdtl("DueDate") = dDueDate
            '    drNewCheckdtl("Remarks") = strRemarks
            '    drNewCheckdtl("STATUS") = 1


            '    dsRecieptType.Tables("CheckDtls").Rows.Add(drNewCheckdtl)
            '    dsRecieptType.Tables("CheckDtls").AcceptChanges()
            'End If
            ''Added by Rohit for CR-5938

            'dtRecieptType.Rows.Add(drNewCashDetail)

            'dtRecieptType.AcceptChanges()
            'dsRecieptType.AcceptChanges()
            'Return dsRecieptType
            'End If

        Catch ex As Exception
            LogException(ex)
            Return dsRecieptType
        End Try
    End Function

    Public Function GetCheckDetailsTableStruture() As DataTable
        Try
            Dim query As String = "Select * from CheckDtls where 1 = 0"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    '''  For CLPPoint adding row into grid row when receipt type is only CLPPoint
    ''' </summary>
    ''' <param name="strRecieptType"></param>
    ''' <param name="strAmount"></param>
    ''' <param name="iRecieptTypeCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''<usedby>frmAcceptPayment.vb</usedby>
    Public Function AddRowRecipetAmountInGrid(ByVal Receipt As String, ByVal strRecieptType As String, ByVal strAmount As String, ByVal iRecieptTypeCode As String, ByVal clpPoints As Double, Optional ByVal NOCLP As Boolean = False) As DataSet
        Try
            If (dsRecieptType.Tables.Count > 0) Then
            Else
                GetDataset()
            End If
            Dim drNewCashDetail As DataRow
            drNewCashDetail = dtRecieptType.NewRow()
            drNewCashDetail.Item("Reciept") = Receipt
            drNewCashDetail.Item("RecieptType") = strRecieptType
            drNewCashDetail.Item("Amount") = strAmount
            drNewCashDetail.Item("RecieptTypeCode") = iRecieptTypeCode
            If iRecieptTypeCode = "CLPPoint" Then
                drNewCashDetail.Item("RefNo_4") = strRemarks
                If clpPoints > 0 Then
                    drNewCashDetail.Item("RefNo_3") = clpPoints
                End If
            Else
                drNewCashDetail.Item("RefNo_3") = strRemarks
            End If
            drNewCashDetail.Item("NOCLP") = NOCLP
            dtRecieptType.Rows.Add(drNewCashDetail)
            dtRecieptType.AcceptChanges()
            dsRecieptType.AcceptChanges()
            Return dsRecieptType
        Catch ex As Exception
            LogException(ex)
            Return dsRecieptType
        End Try
    End Function

    'For only Cash'
    ''' <summary>
    ''' For only Cash, adding row into grid row when receipt type is only Cash
    ''' </summary>
    ''' <param name="strRecieptType"></param>
    ''' <param name="decAmount"></param>
    ''' <param name="iselectedCurrencyIndex"></param>
    ''' <param name="ibaseCurrencyIndex"></param>
    ''' <param name="strSelectedCurrencyText"></param>
    ''' <param name="iRecieptTypeCode"></param>
    ''' <returns></returns>
    ''' <usedby>frmAcceptPayment.vb</usedby>
    ''' <remarks></remarks>
    Public Function AddRowRecipetAmountInGrid_Cash(ByVal Receipt As String, ByVal strRecieptType As String, ByVal decAmount As Decimal, ByVal iselectedCurrencyIndex As String, ByVal ibaseCurrencyIndex As String, ByVal strSelectedCurrencyText As String, ByVal iRecieptTypeCode As String, ByVal vCurrencyCode1 As String, Optional ByVal vCreditPayTerm As String = "") As DataSet
        'Rakesh: 28-05-09 - Add same receipt type with diffrent currency
        vCurrencyCode = vCurrencyCode1
        Try
            If (dsRecieptType.Tables.Count > 0) Then
            Else
                GetDataset()
            End If

            Dim dsCurrencyRate As New DataSet
            Dim decCurrentCurrencyRateAgainstBaseCurrency As Decimal = 1.0

            Dim isExchangeRateFound As Boolean = True
            If ibaseCurrencyIndex <> iselectedCurrencyIndex Then
                Try
                    OpenConnection()
                    '"select MSTCurrency.CurrencyDescription,MSTCurrencyRate.ExchangeQty,MSTCurrencyRate.ExchangeRate from MSTCurrency inner join MSTCurrencyRate on (MSTCurrency.CurrencyCode = MSTCurrencyRate.CurrencyCode ) where MSTCurrencyRate.CurrencyCode='" & ibaseCurrencyIndex & "' and MSTCurrencyRate.RelationalCurrency='" & iselectedCurrencyIndex & "' "
                    Dim sqlQuery As String = "SELECT A.CurrencyDescription,B.ExchangeQty,B.ExchangeRate " + vbCrLf
                    sqlQuery += "FROM MSTCurrency A" + vbCrLf
                    sqlQuery += "INNER JOIN MSTCurrencyRate B ON A.CurrencyCode = B.CurrencyCode " + vbCrLf
                    sqlQuery += "WHERE B.CurrencyCode='" & ibaseCurrencyIndex & "' AND B.RelationalCurrency='" & iselectedCurrencyIndex & "' " + vbCrLf
                    sqlQuery += "AND EndDate IS NULL"

                    Dim sqlSelectCommand As New SqlCommand(sqlQuery, SpectrumCon)
                    Dim sqlAdapter As New SqlDataAdapter()
                    sqlAdapter.SelectCommand = sqlSelectCommand
                    sqlAdapter.Fill(dsCurrencyRate, "tblCurrencyRate")
                    If dsCurrencyRate.Tables("tblCurrencyRate").Rows.Count > 0 Then
                        Dim decCurrentCurrencyQtyAgainstBaseCurrency As Double
                        decCurrentCurrencyQtyAgainstBaseCurrency = CDbl(dsCurrencyRate.Tables("tblCurrencyRate").Rows(0)("ExchangeQty"))
                        'decCurrentCurrencyRateAgainstBaseCurrency = CDbl(dsCurrencyRate.Tables("tblCurrencyRate").Rows(0)("ExchangeQty"))
                        decCurrentCurrencyRateAgainstBaseCurrency = CDbl(dsCurrencyRate.Tables("tblCurrencyRate").Rows(0)("ExchangeRate"))
                        decCurrentCurrencyRateAgainstBaseCurrency = decCurrentCurrencyRateAgainstBaseCurrency / decCurrentCurrencyQtyAgainstBaseCurrency

                    Else
                        isExchangeRateFound = False
                    End If
                Catch ex As Exception

                    MsgBox(getValueByKey("CLAP01"), MsgBoxStyle.Critical, "CLAP01 - " & getValueByKey("CLAE04"))


                    decCurrentCurrencyRateAgainstBaseCurrency = 0
                    isExchangeRateFound = False
                Finally
                    CloseConnection()
                End Try
            End If
            If (isExchangeRateFound) Then
                Dim strAmountCurrency As String = strSelectedCurrencyText + " " + decAmount.ToString()
                Dim decCalculateTotalAmount As Decimal = decAmount * decCurrentCurrencyRateAgainstBaseCurrency

                InsertDataIntoDataTable(Receipt, ibaseCurrencyIndex, strRecieptType, iRecieptTypeCode, FormatCurrency(decCalculateTotalAmount, 2), GetCurrentDate(), strAmountCurrency, decCurrentCurrencyRateAgainstBaseCurrency, iselectedCurrencyIndex, decAmount, Nothing, vCreditPayTerm)
                Dim StrFilter As String = ""
                For Each Row As DataRow In dsRecieptType.Tables(0).Rows
                    If Row.RowState <> DataRowState.Deleted Then
                        StrFilter = StrFilter & Row("SRNO").ToString() & ","
                    End If
                Next
                dtRecieptType.AcceptChanges()
                dsRecieptType.AcceptChanges()
            Else
                'MsgBox("Exchange rate is not found ", MsgBoxStyle.Critical, "Exchange Rate ")
                MsgBox(getValueByKey("CLAP01"), MsgBoxStyle.Critical, "CLAP01" & getValueByKey("CLAE05"))

            End If

            Return dsRecieptType
        Catch ex As Exception
            LogException(ex)
            Return dsRecieptType
        End Try
    End Function
    ''' <summary>
    ''' Function check whether the Cash or CreditVoucher(I) is inserted before into grid.
    ''' </summary>
    ''' <param name="strReceiptType"></param>
    ''' <param name="dtGridData"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsCashOrCreditVoucherIInsert(ByVal strReceiptType As String, ByVal ReciptType As String, ByVal dtGridData As DataTable, ByRef LastAmt As Double, ByRef LastAmtincurr As Double, ByVal iRecieptTypeCode As String) As Boolean
        Try
            If (strReceiptType = AcceptPaymentTenderType.PositiveTenderType.Cash) Or (strReceiptType = AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI) Or (iRecieptTypeCode = AcceptPaymentTenderType.PositiveTenderType.Credit) Or (iRecieptTypeCode = AcceptPaymentTenderType.PositiveTenderType.MealPass) Then
                'If (strReceiptType = AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI) Then
                If Not dtGridData Is Nothing Then
                    If (dtGridData.Rows.Count > 0) Then
                        Dim dvCashRows As DataView

                        'Rakesh: 28-05-09 - Add same receipt type with diffrent currency
                        dvCashRows = New DataView(dtGridData, "RecieptType='" + ReciptType + "' And CurrencyCode='" & vCurrencyCode & "'  ", "", DataViewRowState.CurrentRows)
                        If (dvCashRows.Count > 0) Then
                            For Each drDataView As DataRowView In dvCashRows
                                LastAmt = drDataView("Amount")
                                LastAmtincurr = IIf(drDataView("AmountInCurrency") Is DBNull.Value, 0, drDataView("AmountInCurrency"))
                                drDataView.Delete()

                            Next
                            Return False
                        Else
                            Return False
                        End If
                    Else
                        Return False

                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    'For only Cash and CreditNote
    ''' <summary>
    ''' Insert all reciept data into DataTable("MSTRecieptType")
    ''' </summary>
    ''' <param name="strRecieptType"></param>
    ''' <param name="iRecieptTypeCode"></param>
    ''' <param name="decCalculateTotalAmount"></param>
    ''' <param name="dateExpiry"></param>
    ''' <param name="strAmountCurrency"></param>
    ''' <param name="decCurrentCurrencyRateAgainstBaseCurrency"></param>
    ''' <param name="iselectedCurrencyIndex"></param>
    ''' <usedby>frmAcceptPayment.vb</usedby>
    ''' <param name="decAmount"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function InsertDataIntoDataTable(ByVal Receipt As String, ByVal BaseCurrency As String, ByVal strRecieptType As String, ByVal iRecieptTypeCode As String, ByVal decCalculateTotalAmount As Decimal, Optional ByVal dateExpiry As DateTime = Nothing, Optional ByVal strAmountCurrency As String = "", Optional ByVal decCurrentCurrencyRateAgainstBaseCurrency As Decimal = 1.0, Optional ByVal iselectedCurrencyIndex As String = "", Optional ByVal decAmount As Decimal = Decimal.Zero, Optional ByVal NOCLP As Boolean = False, Optional ByVal CreditPayTerm As String = "") As Boolean

        Try
            If (strRecieptType = AcceptPaymentTenderType.NegativeTenderType.CreditVoucherI Or strRecieptType = AcceptPaymentTenderType.PositiveTenderType.Cash Or iRecieptTypeCode = AcceptPaymentTenderType.PositiveTenderType.Credit Or iRecieptTypeCode = AcceptPaymentTenderType.PositiveTenderType.MealPass) Then
                Dim lastAmt, Lastamtincurrency As Double
                lastAmt = 0
                Lastamtincurrency = 0
                If Not (IsCashOrCreditVoucherIInsert(strRecieptType, Receipt, dtRecieptType, lastAmt, Lastamtincurrency, iRecieptTypeCode)) Then
                    If lastAmt <> 0 Then
                        decCalculateTotalAmount = decCalculateTotalAmount + lastAmt
                        decAmount = decAmount + Lastamtincurrency
                    End If
                    InsertDataIntoGridAftervalidate(Receipt, BaseCurrency, strRecieptType, iRecieptTypeCode, decCalculateTotalAmount, dateExpiry, strAmountCurrency, decCurrentCurrencyRateAgainstBaseCurrency, iselectedCurrencyIndex, decAmount, NOCLP, CreditPayTerm)
                End If
            Else
                InsertDataIntoGridAftervalidate(Receipt, BaseCurrency, strRecieptType, iRecieptTypeCode, decCalculateTotalAmount, dateExpiry, strAmountCurrency, decCurrentCurrencyRateAgainstBaseCurrency, iselectedCurrencyIndex, decAmount, NOCLP)

            End If
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function

    Private Function InsertDataIntoGridAftervalidate(ByVal Reciept As String, ByVal ibaseCurrencyIndex As String, ByVal strRecieptType As String, ByVal iRecieptTypeCode As String, ByVal decCalculateTotalAmount As Decimal, Optional ByVal dateExpiry As DateTime = Nothing, Optional ByVal strAmountCurrency As String = "", Optional ByVal decCurrentCurrencyRateAgainstBaseCurrency As Decimal = 1.0, Optional ByVal iselectedCurrencyIndex As String = "", Optional ByVal decAmount As Decimal = Decimal.Zero, Optional ByVal NOCLP As Boolean = False, Optional ByVal CreditPayTerm As String = "") As Boolean
        Try
            'If IsNewSalesOrder = True Then
            Dim drNewCashDetail As DataRow
            drNewCashDetail = dtRecieptType.NewRow()
            drNewCashDetail.Item("Reciept") = Reciept
            drNewCashDetail.Item("RecieptType") = strRecieptType

            drNewCashDetail.Item("TenderType") = strRecieptType  '' added by nikhil for Cash Payment
            'If TenderRemarks <> "" Then
            '    drNewCashDetail.Item("Remarks") = TenderRemarks
            'Else
            '    TenderRemarks = "-"
            '    drNewCashDetail.Item("Remarks") = TenderRemarks
            'End If
            If strRecieptType = "Neft" Then
                drNewCashDetail.Item("NEFTReferenceNo") = ReferenceNo
            Else
                drNewCashDetail.Item("NEFTReferenceNo") = "-"
            End If

            If strRecieptType = "Rtgs" Then
                drNewCashDetail.Item("RTGSReferenceNo") = ReferenceNo
            Else
                drNewCashDetail.Item("RTGSReferenceNo") = "-"
            End If
            ' drNewCashDetail.Item("RTGSReferenceNo") = "-"
            drNewCashDetail.Item("BankName") = "-"
            drNewCashDetail.Item("CardNo") = "-"
            drNewCashDetail.Item("ChequeNo") = "-"
            drNewCashDetail.Item("MICRNo") = "-"
            drNewCashDetail.Item("ChequeDate") = "-"
            drNewCashDetail.Item("CreditVoucherNo") = "-"
            If MainRemarks <> "" Then
                drNewCashDetail.Item("Remarks") = MainRemarks
            Else
                MainRemarks = "-"
                drNewCashDetail.Item("Remarks") = MainRemarks
            End If
            '' ended by nikhil
            drNewCashDetail.Item("Amount") = decCalculateTotalAmount
            drNewCashDetail.Item("Number") = strAmountCurrency
            drNewCashDetail.Item("Date") = dateExpiry
            drNewCashDetail.Item("RecieptTypeCode") = iRecieptTypeCode
            drNewCashDetail.Item("ExchangeRate") = decCurrentCurrencyRateAgainstBaseCurrency
            drNewCashDetail.Item("CurrencyCode") = iselectedCurrencyIndex
            drNewCashDetail.Item("NOCLP") = NOCLP
            drNewCashDetail.Item("PaymentTermName") = CreditPayTerm
            If iRecieptTypeCode = "CreditVouc(I)" Then
                drNewCashDetail.Item("RefNo_4") = strRemarks
            End If
            'to handle the expiry date of issue cv
            If decCalculateTotalAmount < 0 And iRecieptTypeCode.Contains("CreditVouc(I)") Then
                drNewCashDetail.Item("Date") = dtRecieptType.Compute("MAX(date)", " RecieptTypeCode = 'GiftVoucher(R)' Or  RecieptTypeCode ='CreditVouc(R)'")
                If IsDBNull(drNewCashDetail.Item("Date")) Then
                    drNewCashDetail.Item("Date") = dateExpiry
                End If
            End If

            'to handle the expiry date of issue cv

            'If ibaseCurrencyIndex <> iselectedCurrencyIndex Then
            drNewCashDetail.Item("AmountInCurrency") = decAmount
            'End If

            dtRecieptType.Rows.Add(drNewCashDetail)
            Return True
            'Else
            'Dim drNewCashDetail As DataRow
            'drNewCashDetail = dtRecieptType.NewRow()
            'drNewCashDetail.Item("Reciept") = Reciept
            'drNewCashDetail.Item("RecieptType") = strRecieptType
            'drNewCashDetail.Item("Amount") = decCalculateTotalAmount
            'drNewCashDetail.Item("Number") = strAmountCurrency
            'drNewCashDetail.Item("Date") = dateExpiry
            'drNewCashDetail.Item("RecieptTypeCode") = iRecieptTypeCode
            'drNewCashDetail.Item("ExchangeRate") = decCurrentCurrencyRateAgainstBaseCurrency
            'drNewCashDetail.Item("CurrencyCode") = iselectedCurrencyIndex
            'drNewCashDetail.Item("NOCLP") = NOCLP
            'drNewCashDetail.Item("PaymentTermName") = CreditPayTerm
            'If iRecieptTypeCode = "CreditVouc(I)" Then
            '    drNewCashDetail.Item("RefNo_4") = strRemarks
            'End If
            ''to handle the expiry date of issue cv
            'If decCalculateTotalAmount < 0 And iRecieptTypeCode.Contains("CreditVouc(I)") Then
            '    drNewCashDetail.Item("Date") = dtRecieptType.Compute("MAX(date)", " RecieptTypeCode = 'GiftVoucher(R)' Or  RecieptTypeCode ='CreditVouc(R)'")
            '    If IsDBNull(drNewCashDetail.Item("Date")) Then
            '        drNewCashDetail.Item("Date") = dateExpiry
            '    End If
            'End If

            ''to handle the expiry date of issue cv

            ''If ibaseCurrencyIndex <> iselectedCurrencyIndex Then
            'drNewCashDetail.Item("AmountInCurrency") = decAmount
            ''End If

            'dtRecieptType.Rows.Add(drNewCashDetail)
            'Return True
            'End If

        Catch ex As Exception
            LogException(ex)
            Return False
        End Try

    End Function


    ''' <summary>
    ''' For only CreditNote , adding row into grid row when receipt type is only CreditNote 
    ''' </summary>
    ''' <param name="strRecieptType"></param>
    ''' <param name="strAmount"></param>
    ''' <param name="strCreditNoteNumber"></param>
    ''' <param name="errorMsg"></param>
    ''' <param name="iRecieptTypeCode"></param>
    ''' <usedby>frmAcceptPayment.vb</usedby>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddRowRecieptAmountInGrid_CreditNote(ByVal Voucherdays As Int32, ByVal Receipt As String, ByVal BaseCurrency As String, ByVal strRecieptType As String, ByVal strAmount As Decimal, ByVal strCreditNoteNumber As String, ByRef errorMsg As String, ByVal iRecieptTypeCode As String, ByVal strSiteCode As String, ByVal strVoucherCode As String, Optional ByVal NOCLP As Boolean = False) As DataSet
        Try
            If (dsRecieptType.Tables.Count > 0) Then
            Else
                GetDataset()
            End If
            If (iRecieptTypeCode = AcceptPaymentTenderType.PositiveTenderType.CreditVoucR) Then

                If Not (IsCreditNoteUsed(strCreditNoteNumber, strAmount)) Then
                    Dim dsCreditNote As New DataSet
                    Dim currentDate As Date = GetCurrentDate()
                    Dim sqlSelectCommand_CreditNote As New SqlCommand("select VourcherSerialNbr,IssuedDocNumber,ValueofVoucher,IsActive,ExpiryDate from Voucherdtls where VourcherSerialNbr='" & strCreditNoteNumber & "' ", SpectrumCon)
                    Dim sqlAdapter As New SqlDataAdapter()
                    sqlAdapter.SelectCommand = sqlSelectCommand_CreditNote
                    sqlAdapter.Fill(dsCreditNote, "tblCreditNote")
                    Dim strCreditNoteNumberDB As String
                    Dim strStatus As Boolean
                    Dim decAmount As Decimal
                    Dim dtExpiryDate As DateTime
                    If (dsCreditNote.Tables("tblCreditNote").Rows.Count > 0) Then
                        strCreditNoteNumberDB = dsCreditNote.Tables("tblCreditNote").Rows(0)("VourcherSerialNbr").ToString()
                        strStatus = dsCreditNote.Tables("tblCreditNote").Rows(0)("IsActive")
                        decAmount = dsCreditNote.Tables("tblCreditNote").Rows(0)("ValueofVoucher").ToString()
                        If (strStatus = True) Then
                            If (decAmount = strAmount) Then
                                dtExpiryDate = (dsCreditNote.Tables("tblCreditNote").Rows(0)("ExpiryDate"))
                                'changes by rama on date comp.
                                If (DateDiff(DateInterval.Day, currentDate, dtExpiryDate) >= 0) Then
                                    InsertDataIntoDataTable(Receipt, BaseCurrency, strRecieptType, iRecieptTypeCode, strAmount, dtExpiryDate, strCreditNoteNumber, , , , NOCLP)
                                    dtRecieptType.AcceptChanges()
                                    dsRecieptType.AcceptChanges()
                                Else
                                    'errorMsg = "CreditNote date is Expired"
                                    errorMsg = errorMsg = getValueByKey("EM003")
                                End If
                            Else
                                'errorMsg = "Entered CreditNote amount is not match"
                                errorMsg = errorMsg = getValueByKey("EM006")
                            End If
                        Else
                            'errorMsg = "Already used."
                            errorMsg = errorMsg = getValueByKey("EM001")
                        End If
                    Else
                        'errorMsg = "CreditNote number is not Found"
                        errorMsg = errorMsg = getValueByKey("EM004")
                    End If
                Else
                    'errorMsg = "Already used."
                    errorMsg = errorMsg = getValueByKey("EM001")
                End If
            Else
                'Rakesh-10.10.2013-7738-> Avoid zero amount cv issue
                If (strAmount < 0) Then
                    InsertDataIntoDataTable(Receipt, BaseCurrency, strRecieptType, iRecieptTypeCode, strAmount, DateAdd(DateInterval.Day, Voucherdays, GetCurrentDate()), "", 1, BaseCurrency, NOCLP)
                End If
            End If

        Catch ex As Exception
            LogException(ex)
        Finally
            CloseConnection()
        End Try
        Return dsRecieptType
    End Function
    ''' <summary>
    ''' Get current date of server 
    ''' </summary>
    ''' <returns>Date</returns>
    ''' <usedby>frmAcceptPayment.vb</usedby>
    ''' <remarks></remarks>
    Private Function GetCurrentDate() As Date
        Dim currentDate As Date
        Dim drCurrentDate As SqlDataReader
        Try
            OpenConnection()
            Dim cmdCurrentDate As New SqlCommand("SELECT getDate()", SpectrumCon)
            drCurrentDate = cmdCurrentDate.ExecuteReader()
            If (drCurrentDate.Read()) Then
                If Not (drCurrentDate.IsDBNull(0)) Then
                    currentDate = drCurrentDate.GetDateTime(0).Date
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        Finally
            drCurrentDate.Close()
            CloseConnection()
        End Try
        Return currentDate
    End Function
    ''' <summary>
    ''' Checking CreidtNote is already used or not 
    ''' </summary>
    ''' <param name="strCreditNoteNumber"></param>
    ''' <param name="strAmount"></param>
    ''' <returns>Boolean</returns>
    ''' <usedby>frmAcceptPayment.vb</usedby>
    ''' <remarks></remarks>
    Public Function IsCreditNoteUsed(ByVal strCreditNoteNumber As String, ByVal strAmount As Decimal) As Boolean
        Try
            If (dtRecieptType.Rows.Count > 0) Then
                For index As Integer = 0 To dtRecieptType.Rows.Count - 1
                    If (dtRecieptType.Rows(index)("RecieptTypeCode").ToString = AcceptPaymentTenderType.PositiveTenderType.CreditVoucR Or dtRecieptType.Rows(index)("RecieptTypeCode").ToString = AcceptPaymentTenderType.PositiveTenderType.GiftVoucher) Then
                        If (dtRecieptType.Rows(index)("Number").ToString() = strCreditNoteNumber) Then
                            If (dtRecieptType.Rows(index)("Amount") = strAmount) Then
                                Return True
                            Else
                                Return False
                            End If
                        End If
                    End If
                Next
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)

        End Try
    End Function

    ''' <summary>
    ''' Calculate total bill amount 
    ''' </summary>
    ''' <param name="decBillAmount"></param>
    ''' <param name="iselectedCurrency"></param>
    ''' <param name="decCurrentCurrencyRateAgainstBaseCurrency"></param>
    ''' <param name="iBaseCurrency"></param>
    ''' <usedby>frmAcceptPayment.vb</usedby>
    ''' <returns>Decimal</returns>
    ''' <remarks></remarks>
    Public Function CalculateTotalBillAmount_InCurrency(ByVal decBillAmount As Decimal, ByVal iselectedCurrency As String, ByRef decCurrentCurrencyRateAgainstBaseCurrency As Decimal, Optional ByVal iBaseCurrency As String = "") As Decimal
        Try
            If iselectedCurrency <> iBaseCurrency Then
                Dim dsCurrencyRate As New DataSet
                Try
                    OpenConnection()
                    Dim sqlSelectCommand As New SqlCommand("select MSTCurrency.CurrencyDescription,MSTCurrencyRate.ExchangeQty,MSTCurrencyRate.ExchangeRate from MSTCurrency inner join MSTCurrencyRate on (MSTCurrency.CurrencyCode = MSTCurrencyRate.CurrencyCode ) where MSTCurrencyRate.CurrencyCode='" & iBaseCurrency & "' and MSTCurrencyRate.RelationalCurrency='" & iselectedCurrency & "' ", SpectrumCon)
                    Dim sqlAdapter As New SqlDataAdapter()
                    sqlAdapter.SelectCommand = sqlSelectCommand
                    sqlAdapter.Fill(dsCurrencyRate, "tblCurrencyRate")
                    If (dsCurrencyRate.Tables("tblCurrencyRate").Rows.Count > 0) Then
                        Dim decCurrentCurrencyQtyAgainstBaseCurrency As Double
                        decCurrentCurrencyQtyAgainstBaseCurrency = CDbl(dsCurrencyRate.Tables("tblCurrencyRate").Rows(0)("ExchangeQty"))
                        decCurrentCurrencyRateAgainstBaseCurrency = CDbl(dsCurrencyRate.Tables("tblCurrencyRate").Rows(0)("ExchangeRate"))
                        'Add as per the discussion with rashid
                        'INR = BillAmt*exchangeQty/exchangeqty

                        'Return Format(decBillAmount / decCurrentCurrencyRateAgainstBaseCurrency, "0.00")
                        Dim lNewVariable As Decimal = decCurrentCurrencyQtyAgainstBaseCurrency / decCurrentCurrencyRateAgainstBaseCurrency
                        decCurrentCurrencyRateAgainstBaseCurrency = lNewVariable
                        Return decBillAmount * (lNewVariable)
                    Else
                        Return Decimal.Zero

                        MsgBox(getValueByKey("CLAP02"), , "CLAP02 - " & getValueByKey("CLAE04"))



                    End If

                Catch ex As Exception
                    LogException(ex)
                Finally
                    CloseConnection()
                End Try
            Else
                decCurrentCurrencyRateAgainstBaseCurrency = 1.0
                Return decBillAmount 'Decimal.Zero
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function
    ''' <summary>
    ''' Calculate total amount receipt amount from user
    ''' </summary>
    ''' <param name="decCurrencyRate"></param>
    ''' <returns>Decimal</returns>
    ''' <usedby>frmAcceptPayment.vb</usedby>
    ''' <remarks></remarks>
    Public Function CalculateTotalRecieptAmount_InCurrency(ByVal decCurrencyRate As Decimal, ByVal reverse As Boolean) As Decimal
        Try
            Dim decSum As Decimal
            If (dtRecieptType.Rows.Count > 0) Then
                'If dtRecieptType.Columns.Contains("CVAmountAgainstPoint") Then
                '    decSum = dtRecieptType.Compute("Sum(Amount)", "CVAmountAgainstPoint Is Null Or CVAmountAgainstPoint = 0")
                'Else
                '    decSum = dtRecieptType.Compute("Sum(Amount)", " ")
                'End If
                decSum = dtRecieptType.Compute("Sum(Amount)", " ")
            End If
            If decCurrencyRate > 0 Then
                If reverse = False Then
                    Return decSum / decCurrencyRate
                Else
                    Return decSum * decCurrencyRate
                End If

            Else
                Return decSum
            End If
        Catch ex As Exception
            LogException(ex)

        End Try
    End Function
    ''' <summary>
    ''' Calculate total balance amount need to accept from customer 
    ''' </summary>
    ''' <param name="decBillAmount"></param>
    ''' <param name="decTotalRecieptAmount"></param>
    ''' <returns></returns>
    ''' <usedby>frmAcceptPayment.vb</usedby>
    ''' <remarks></remarks>
    Public Function CalculateBalanceDue(ByVal decBillAmount As Decimal, ByVal decTotalRecieptAmount As Decimal) As Decimal
        Return decBillAmount - decTotalRecieptAmount
    End Function
    ''' <summary>
    '''  Set local currency according to site 
    ''' </summary>
    ''' <param name="SiteCode"></param>
    ''' <returns></returns>
    ''' <usedby>frmAcceptPayment.vb</usedby>
    ''' <remarks></remarks>
    Public Function GetLocalCurrency(ByVal SiteCode As String, ByRef EanType As String) As String
        Dim iLocalCurencyCode As String
        Dim drLC As SqlDataReader = Nothing
        Try
            OpenConnection()
            'change by rama on 16 th sep 2009 as currency code should pick up from Site master.
            'Dim sqlCmdLocalCurrency As New SqlCommand("select fldvalue from defaultconfig where sitecode='" & SiteCode & "' and Fldlabel='LocalCurrency'", SpectrumCon)
            Dim sqlCmdLocalCurrency As New SqlCommand("select LocalCurrancyCode,DefaultEan from MstSite where Status=1 AND sitecode='" & SiteCode & "'", SpectrumCon)
            drLC = sqlCmdLocalCurrency.ExecuteReader()
            If (drLC.Read()) Then
                If Not (drLC.IsDBNull(0)) Then
                    iLocalCurencyCode = drLC.GetString(0)
                    EanType = drLC.GetString(1)
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        Finally
            drLC.Close()
            CloseConnection()
        End Try
        Return iLocalCurencyCode
    End Function
    Public Function ValidateCLP(ByVal CLPCardNo As String, ByVal CLPProgramId As String, ByVal Points As Integer) As Boolean
        Dim con As New SqlConnection(ConString)
        Try
            Dim Str As String
            Str = "Select count(*) from CLPCustomers where CardNo='" & CLPCardNo & "' and  ClpProgramId='" & CLPProgramId & "' And TotalBalancePoint >= " & Points
            Dim cmd As New SqlCommand(Str, con)
            con.Open()
            Dim rows As Int32 = cmd.ExecuteScalar()
            If rows > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            con.Close()
        End Try
    End Function

End Class

