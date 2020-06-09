<Serializable()>
Public Class ArticleUOMDetails
    Public Sub New()

    End Sub

    Private _UOMCode As String
    Public Property UOMCode As String
        Get
            Return _UOMCode
        End Get
        Set(ByVal value As String)
            _UOMCode = value
        End Set
    End Property

    Private _UOMName As String
    Public Property UOMName As String
        Get
            Return _UOMName
        End Get
        Set(ByVal value As String)
            _UOMName = value
        End Set
    End Property
End Class
