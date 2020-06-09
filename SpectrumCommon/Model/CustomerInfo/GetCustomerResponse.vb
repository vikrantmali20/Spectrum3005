Public Class GetCustomerResponse
    Inherits BaseModel

    Private _CLPCustomer As CLPCustomerDTO
    Public Property CLPCustomer As CLPCustomerDTO
        Get
            Return _CLPCustomer
        End Get
        Set(value As CLPCustomerDTO)
            _CLPCustomer = value
        End Set
    End Property
End Class
