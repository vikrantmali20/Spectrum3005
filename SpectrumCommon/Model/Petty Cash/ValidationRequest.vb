Public Class ValidationRequest
    Private _TerminalID As String
    Public Property TerminalID As String
        Get
            Return _TerminalID
        End Get
        Set(ByVal value As String)
            _TerminalID = value
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
End Class
