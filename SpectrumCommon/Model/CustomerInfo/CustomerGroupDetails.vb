Public Class CustomerGroupDetails
    Inherits BaseModel

    Private _GroupCode As String
    Public Property GroupCode As String
        Get
            Return _GroupCode
        End Get
        Set(ByVal value As String)
            _GroupCode = value
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
End Class
