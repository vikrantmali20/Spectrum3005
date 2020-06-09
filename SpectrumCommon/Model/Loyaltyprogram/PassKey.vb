Public Class PassKey
    Inherits BaseModel
    Private _SiteCode As String
    Public Property SiteCode() As String
        Get
            Return _SiteCode
        End Get
        Set(ByVal value As String)
            _SiteCode = value
        End Set
    End Property

    'passkey field is a random number of n digit . n value will pick up from default Config .In case n value is greater than 15 consider its value 15 .
    Private _Passkey As String
    Public Property Passkey() As String
        Get
            Return _Passkey
        End Get
        Set(ByVal value As String)
            _Passkey = value
        End Set
    End Property

    Private _PasskeyValue As String
    Public Property PasskeyValue() As String
        Get
            Return _PasskeyValue
        End Get
        Set(ByVal value As String)
            _PasskeyValue = value
        End Set
    End Property

    Private _ExpiryDateTime As DateTime
    Public Property ExpiryDateTime() As DateTime
        Get
            Return _ExpiryDateTime
        End Get
        Set(ByVal value As DateTime)
            _ExpiryDateTime = value
        End Set
    End Property

    Private _DocumentNumber As String
    Public Property DocumentNumber() As String
        Get
            Return _DocumentNumber
        End Get
        Set(ByVal value As String)
            _DocumentNumber = value
        End Set
    End Property

    Private _DocumentType As String
    Public Property DocumentType() As String
        Get
            Return _DocumentType
        End Get
        Set(ByVal value As String)
            _DocumentType = value
        End Set
    End Property

    Private _DocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return _DocumentDate
        End Get
        Set(ByVal value As DateTime)
            _DocumentDate = value
        End Set
    End Property

    Private _IsRedeemed As Boolean
    Public Property IsRedeemed() As Boolean
        Get
            Return _IsRedeemed
        End Get
        Set(ByVal value As Boolean)
            _IsRedeemed = value
        End Set
    End Property


End Class
