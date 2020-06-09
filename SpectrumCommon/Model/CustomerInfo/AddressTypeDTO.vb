Public Class AddressTypeDTO
    Public Sub New()
    End Sub

    Private _AddressTypeName As String
    Public Property AddressTypeName As String
        Get
            Return _AddressTypeName
        End Get
        Set(value As String)
            _AddressTypeName = value
        End Set
    End Property

    Private _AddressTypeCode As String
    Public Property AddressTypeCode As String
        Get
            Return _AddressTypeCode
        End Get
        Set(value As String)
            _AddressTypeCode = value
        End Set
    End Property
End Class
