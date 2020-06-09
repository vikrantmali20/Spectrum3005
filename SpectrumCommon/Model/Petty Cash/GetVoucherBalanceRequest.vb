Public Class GetVoucherBalanceRequest
    Private _DayOpenDate As DateTime
    Public Property DayOpenDate As DateTime
        Get
            Return _DayOpenDate
        End Get
        Set(ByVal value As DateTime)
            _DayOpenDate = value
        End Set
    End Property

    Private _SiteCode As String
    Public Property SiteCode As String
        Get
            Return _SiteCode
        End Get
        Set(ByVal value As String)
            _SiteCode = value
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
    Private _CreatedOn As String
    Public Property CreatedOn As String
        Get
            Return _CreatedOn
        End Get
        Set(ByVal value As String)
            _CreatedOn = value
        End Set
    End Property
End Class
