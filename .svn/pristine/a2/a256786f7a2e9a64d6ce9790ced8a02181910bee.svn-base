Public Class DayCloseDataRequestModel(Of T)
    Public Sub New()

    End Sub

    Private _Query As String
    Public Property Query As String
        Get
            Return _Query
        End Get
        Set(ByVal value As String)
            _Query = value
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

    Private _DayCloseDate As DateTime
    Public Property DayCloseDate As DateTime
        Get
            Return _DayCloseDate
        End Get
        Set(ByVal value As DateTime)
            _DayCloseDate = value
        End Set
    End Property

    Public Function GetDayCloseDateAsString() As String
        Try
            Return DayCloseDate.ToString("yyyy-MM-dd")
        Catch ex As Exception
            Return DayCloseDate.ToString()
        End Try
    End Function

    Private _DayCloseData As IList(Of T)
    Public Property DayCloseData As IList(Of T)
        Get
            Return _DayCloseData
        End Get
        Set(ByVal value As IList(Of T))
            _DayCloseData = value
        End Set
    End Property

    Private _UserId As String
    Public Property UserId As String
        Get
            Return _UserId
        End Get
        Set(ByVal value As String)
            _UserId = value
        End Set
    End Property
End Class
