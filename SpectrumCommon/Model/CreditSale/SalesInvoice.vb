Imports System.ComponentModel
Public Class SalesInvoice
    Inherits BaseModel

    Private _BillNO As String
    Public Property BillNO() As String
        Get
            Return _BillNO
        End Get
        Set(ByVal value As String)
            _BillNO = value
        End Set
    End Property

    Private _SiteCode As String
    <DisplayName("Site Code")>
     Public Property SiteCode() As String
        Get
            Return _SiteCode
        End Get
        Set(ByVal value As String)
            _SiteCode = value
        End Set
    End Property

    Private _FinYear As String
    <DisplayName("Financial Year")>
    Public Property FinYear() As String
        Get
            Return _FinYear
        End Get
        Set(ByVal value As String)
            _FinYear = value
        End Set
    End Property

    Private _DocNo As String
    <DisplayName("Document Number")>
    Public Property DocNo() As String
        Get
            Return _DocNo
        End Get
        Set(ByVal value As String)
            _DocNo = value
        End Set
    End Property

    Private _DocType As String
    <DisplayName("Document Type")>
    Public Property DocType() As String
        Get
            Return _DocType
        End Get
        Set(ByVal value As String)
            _DocType = value
        End Set
    End Property


    Private _InvNo As String
    <DisplayName("Invoice Number")>
     Public Property InvoiceNumber() As String
        Get
            Return _InvNo
        End Get
        Set(ByVal value As String)
            _InvNo = value
        End Set
    End Property

    Private _CustomerID As String
    Public Property CustomerID() As String
        Get
            Return _CustomerID
        End Get
        Set(ByVal value As String)
            _CustomerID = value
        End Set
    End Property

    Private _CustomerName As String
    <DisplayName("Customer Name")>
     Public Property CustomerName() As String
        Get
            Return _CustomerName
        End Get
        Set(ByVal value As String)
            _CustomerName = value
        End Set
    End Property

    Private _LineNo As Integer
    Public Property LineNo() As Integer
        Get
            Return _LineNo
        End Get
        Set(ByVal value As Integer)
            _LineNo = value
        End Set
    End Property

    Private _TerminalID As String
    Public Property TerminalID() As String
        Get
            Return _TerminalID
        End Get
        Set(ByVal value As String)
            _TerminalID = value
        End Set
    End Property

    Private _TenderTypeCode As String
    Public Property TenderTypeCode() As String
        Get
            Return _TenderTypeCode
        End Get
        Set(ByVal value As String)
            _TenderTypeCode = value
        End Set
    End Property

    Private _TenderHeadCode As String
    <DisplayName("Tender")>
    Public Property TenderHeadCode() As String
        Get
            Return _TenderHeadCode
        End Get
        Set(ByVal value As String)
            _TenderHeadCode = value
        End Set
    End Property

    Private _ExchangeRate As Decimal
    Public Property ExchangeRate() As Decimal
        Get
            Return _ExchangeRate
        End Get
        Set(ByVal value As Decimal)
            _ExchangeRate = value
        End Set
    End Property

    Private _AmountTendered As Decimal
    <DisplayName("Total Amount")>
     Public Property AmountTendered() As Decimal
        Get
            Return _AmountTendered
        End Get
        Set(ByVal value As Decimal)
            _AmountTendered = value
        End Set
    End Property

    Private _CurrencyCode As String
    Public Property CurrencyCode() As String
        Get
            Return _CurrencyCode
        End Get
        Set(ByVal value As String)
            _CurrencyCode = value
        End Set
    End Property

    Private _AmountInCurrency As Decimal?
    <DisplayName("Amount")>
    Public Property AmountInCurrency() As Decimal?
        Get
            Return _AmountInCurrency
        End Get
        Set(ByVal value As Decimal?)
            _AmountInCurrency = value
        End Set
    End Property

    Private _CardNO As String
    Public Property CardNO() As String
        Get
            Return _CardNO
        End Get
        Set(ByVal value As String)
            _CardNO = value
        End Set
    End Property

    Private _BankNO As String
    Public Property BankNO() As String
        Get
            Return _BankNO
        End Get
        Set(ByVal value As String)
            _BankNO = value
        End Set
    End Property


    Private _RecTime As DateTime
    <DisplayName("Bill Date")>
    Public Property RecTime() As DateTime
        Get
            Return _RecTime
        End Get
        Set(ByVal value As DateTime)
            _RecTime = value
        End Set
    End Property

    Private _BalanceAmt As String
    <DisplayName("Balance Amount")>
     Public Property BalanceAmt() As String
        Get
            Return _BalanceAmt
        End Get
        Set(ByVal value As String)
            _BalanceAmt = value
        End Set
    End Property

    Private _SalesPerson As String
    <DisplayName("Sales Person")>
    Public Property SalesPerson() As String
        Get
            Return _SalesPerson
        End Get
        Set(ByVal value As String)
            _SalesPerson = value
        End Set
    End Property

    Private _RefBillInvNumber As String
    Public Property RefBillInvNumber() As String
        Get
            Return _RefBillInvNumber
        End Get
        Set(ByVal value As String)
            _RefBillInvNumber = value
        End Set
    End Property
    Private _Remark As String 'vipin 15.11.2017
    Public Property Remark() As String
        Get
            Return _Remark
        End Get
        Set(ByVal value As String)
            _Remark = value
        End Set
    End Property
End Class
