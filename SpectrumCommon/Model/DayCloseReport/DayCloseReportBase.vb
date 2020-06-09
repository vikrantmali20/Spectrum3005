Public Class DayCloseReportBase
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

    Private _ToDate As DateTime
    Public Property ToDate As DateTime
        Get
            Return _ToDate
        End Get
        Set(ByVal value As DateTime)
            _ToDate = value
        End Set
    End Property

    Private _FromDate As DateTime
    Public Property FromDate As DateTime
        Get
            Return _FromDate
        End Get
        Set(ByVal value As DateTime)
            _FromDate = value
        End Set
    End Property
End Class
