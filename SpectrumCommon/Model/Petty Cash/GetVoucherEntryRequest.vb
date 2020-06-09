Public Class GetVoucherEntryRequest
    Private _VoucherID As String
    Public Property VoucherID As String
        Get
            Return _VoucherID
        End Get
        Set(ByVal value As String)
            _VoucherID = value
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
End Class
