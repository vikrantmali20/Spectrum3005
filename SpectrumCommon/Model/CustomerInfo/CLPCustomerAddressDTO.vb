Public Class CLPCustomerAddressDTO
    Inherits BaseModel

    Private _CardNumber As String = String.Empty
    Public Property CardNumber As String
        Get
            Return _CardNumber
        End Get
        Set(value As String)
            _CardNumber = value
        End Set
    End Property

    Private _ClpProgId As String = String.Empty
    Public Property ClpProgId As String
        Get
            Return _ClpProgId
        End Get
        Set(value As String)
            _ClpProgId = value
        End Set
    End Property

    Private _AddressType As String = String.Empty
    Public Property AddressType As String
        Get
            Return _AddressType
        End Get
        Set(value As String)
            _AddressType = value
        End Set
    End Property

    Private _AddLine1 As String = String.Empty
    Public Property AddLine1 As String
        Get
            Return _AddLine1
        End Get
        Set(value As String)
            _AddLine1 = value
        End Set
    End Property

    Private _AddLine2 As String
    Public Property AddLine2 As String
        Get
            Return _AddLine2
        End Get
        Set(value As String)
            _AddLine2 = value
        End Set
    End Property

    Private _AddLine3 As String
    Public Property AddLine3 As String
        Get
            Return _AddLine3
        End Get
        Set(value As String)
            _AddLine3 = value
        End Set
    End Property

    Private _AddLine4 As String
    Public Property AddLine4 As String
        Get
            Return _AddLine4
        End Get
        Set(value As String)
            _AddLine4 = value
        End Set
    End Property

    Private _PinCode As String
    Public Property PinCode As String
        Get
            Return _PinCode
        End Get
        Set(value As String)
            _PinCode = value
        End Set
    End Property

    Private _City As String
    Public Property City As String
        Get
            Return _City
        End Get
        Set(value As String)
            _City = value
        End Set
    End Property

    Private _State As String
    Public Property State As String
        Get
            Return _State
        End Get
        Set(value As String)
            _State = value
        End Set
    End Property

    Private _Country As String = String.Empty
    Public Property Country As String
        Get
            Return _Country
        End Get
        Set(value As String)
            _Country = value
        End Set
    End Property
    Private _Department As String = String.Empty
    Public Property Department As String
        Get
            Return _Department
        End Get
        Set(value As String)
            _Department = value
        End Set
    End Property
    Private _DefaultAddress As Integer = 0
    Public Property DefaultAddress As Integer
        Get
            Return _DefaultAddress
        End Get
        Set(value As Integer)
            _DefaultAddress = value
        End Set
    End Property
    Private _SrNo As Integer = 0
    Public Property SrNo As Integer
        Get
            Return _SrNo
        End Get
        Set(value As Integer)
            _SrNo = value
        End Set
    End Property
End Class
