Public Class BankDetailsSaveDataRequest
    Inherits BaseModel
    Private _SiteCode As String
    Public Property SiteCode As String
        Get
            Return _SiteCode
        End Get
        Set(ByVal value As String)
            _SiteCode = value
        End Set
    End Property

    Private _DayCloseDate As DateTime
    Public Property DayCloseDate As DateTime
        Get
            Return _DayCloseDate
        End Get
        Set(ByVal value As DateTime)
            _DayCloseDate = value
        End Set
    End Property

    Private _FinYear As String
    Public Property FinYear As String
        Get
            Return _FinYear
        End Get
        Set(ByVal value As String)
            _FinYear = value
        End Set
    End Property

    Private _CashDenominationList As List(Of CashDenominationDtl)
    Public Property CashDenominationList As List(Of CashDenominationDtl)
        Get
            If _CashDenominationList Is Nothing Then
                _CashDenominationList = New List(Of CashDenominationDtl)
            End If
            Return _CashDenominationList
        End Get
        Set(ByVal value As List(Of CashDenominationDtl))
            _CashDenominationList = value
        End Set
    End Property

    Private _ChequeDetailsList As IList(Of ChequeDetails)
    Public Property ChequeDetailsList As IList(Of ChequeDetails)
        Get
            If _ChequeDetailsList Is Nothing Then
                _ChequeDetailsList = New List(Of ChequeDetails)
            End If
            Return _ChequeDetailsList
        End Get
        Set(ByVal value As IList(Of ChequeDetails))
            _ChequeDetailsList = value
        End Set
    End Property

    Private _IsPettyCashApplicable As Boolean
    Public Property IsPettyCashApplicable As Boolean
        Get
            Return _IsPettyCashApplicable
        End Get
        Set(ByVal value As Boolean)
            _IsPettyCashApplicable = value
        End Set
    End Property

    Private _IsPettyCashOnSalesTerminal As Boolean
    Public Property IsPettyCashOnSalesTerminal As Boolean
        Get
            Return _IsPettyCashOnSalesTerminal
        End Get
        Set(ByVal value As Boolean)
            _IsPettyCashOnSalesTerminal = value
        End Set
    End Property

    Private _AddTotalSalesToPettyCash As Boolean
    Public Property AddTotalSalesToPettyCash As Boolean
        Get
            Return _AddTotalSalesToPettyCash
        End Get
        Set(ByVal value As Boolean)
            _AddTotalSalesToPettyCash = value
        End Set
    End Property

    Private _IsPDFGenerate As Boolean
    Public Property IsPDFGenerate As Boolean
        Get
            Return _IsPDFGenerate
        End Get
        Set(ByVal value As Boolean)
            _IsPDFGenerate = value
        End Set
    End Property
End Class
