Public Class CustomerDetails
    Private _CustomerName As String
    Public Property CustomerName() As String
        Get
            Return _CustomerName
        End Get
        Set(ByVal value As String)
            _CustomerName = value
        End Set
    End Property

    Private _PhoneNo As String
    Public Property PhoneNo() As String
        Get
            Return _PhoneNo
        End Get
        Set(ByVal value As String)
            _PhoneNo = value
        End Set
    End Property

    Private _TotalBalancePoint As String
    Public Property TotalBalancePoint() As String
        Get
            Return _TotalBalancePoint
        End Get
        Set(ByVal value As String)
            _TotalBalancePoint = value
        End Set
    End Property

End Class
