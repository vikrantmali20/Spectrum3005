Public Class AreaInfo
    Inherits BaseModel

    Private _AreaCode As String
    Public Property AreaCode As String
        Get
            Return _AreaCode
        End Get
        Set(ByVal value As String)
            _AreaCode = value
        End Set
    End Property

    Private _AreaName As String
    Public Property AreaName As String
        Get
            Return _AreaName
        End Get
        Set(ByVal value As String)
            _AreaName = value
        End Set
    End Property

    Private _ParentAreaCode As String
    Public Property ParentAreaCode As String
        Get
            Return _ParentAreaCode
        End Get
        Set(ByVal value As String)
            _ParentAreaCode = value
        End Set
    End Property

    Private _AreaType As Decimal
    Public Property AreaType As Decimal
        Get
            Return _AreaType
        End Get
        Set(ByVal value As Decimal)
            _AreaType = value
        End Set
    End Property
End Class
