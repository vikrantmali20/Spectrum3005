Public Class GetCustomerMasterResponse
    Inherits BaseModel

    Private _AddressTypeList As List(Of AddressTypeDTO)
    Public Property AddressTypeList As List(Of AddressTypeDTO)
        Get
            Return _AddressTypeList
        End Get
        Set(value As List(Of AddressTypeDTO))
            _AddressTypeList = value
        End Set
    End Property

    Private _CustomerGroupList As List(Of CustomerGroupDetails)
    Public Property CustomerGroupList As List(Of CustomerGroupDetails)
        Get
            If _CustomerGroupList Is Nothing Then
                _CustomerGroupList = New List(Of CustomerGroupDetails)()
            End If
            Return _CustomerGroupList
        End Get
        Set(value As List(Of CustomerGroupDetails))
            _CustomerGroupList = value
        End Set
    End Property

    Private _AreaInfoList As List(Of AreaInfo)
    Public Property AreaInfoList As List(Of AreaInfo)
        Get
            If _AreaInfoList Is Nothing Then
                _AreaInfoList = New List(Of AreaInfo)()
            End If
            Return _AreaInfoList
        End Get
        Set(value As List(Of AreaInfo))
            _AreaInfoList = value
        End Set
    End Property
End Class
