Public Class ArticleGroupDetails
    Public Sub New()

    End Sub

    Private _GroupId As String
    Public Property GroupId As String
        Get
            Return _GroupId
        End Get
        Set(ByVal value As String)
            _GroupId = value
        End Set
    End Property

    Private _GroupName As String
    Public Property GroupName As String
        Get
            Return _GroupName
        End Get
        Set(ByVal value As String)
            _GroupName = value
        End Set
    End Property

    Private _GroupType As String
    Public Property GroupType As String
        Get
            Return _GroupType
        End Get
        Set(ByVal value As String)
            _GroupType = value
        End Set
    End Property
End Class
