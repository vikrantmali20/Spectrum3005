Public Class CLPCustomerDTO
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

    Private _SiteCode As String
    Public Property SiteCode As String
        Get
            Return _SiteCode
        End Get
        Set(value As String)
            _SiteCode = value
        End Set
    End Property

    Private _FirstName As String = String.Empty
    Public Property FirstName As String
        Get
            Return _FirstName
        End Get
        Set(value As String)
            _FirstName = value
        End Set
    End Property

    Private _MiddleName As String
    Public Property MiddleName As String
        Get
            Return _MiddleName
        End Get
        Set(value As String)
            _MiddleName = value
        End Set
    End Property

    Private _LastName As String
    Public Property LastName As String
        Get
            Return _LastName
        End Get
        Set(value As String)
            _LastName = value
        End Set
    End Property

    Private _EmailId As String
    Public Property EmailId As String
        Get
            Return _EmailId
        End Get
        Set(value As String)
            _EmailId = value
        End Set
    End Property

    Private _MobileNo As String
    Public Property MobileNo As String
        Get
            Return _MobileNo
        End Get
        Set(value As String)
            _MobileNo = value
        End Set
    End Property

    Private _BirthDate As DateTime
    Public Property BirthDate As DateTime
        Get         
            Return _BirthDate
        End Get
        Set(value As DateTime)
            _BirthDate = value
        End Set
    End Property

    Private _Gender As String = String.Empty
    Public Property Gender As String
        Get
            Return _Gender
        End Get
        Set(value As String)
            _Gender = value
        End Set
    End Property

    Private _CustomerGroup As String = String.Empty
    Public Property CustomerGroup As String
        Get
            Return _CustomerGroup
        End Get
        Set(value As String)
            _CustomerGroup = value
        End Set
    End Property

    Private _BalancePoints As Decimal
    Public Property BalancePoints As Decimal
        Get
            Return _BalancePoints
        End Get
        Set(ByVal value As Decimal)
            _BalancePoints = value
        End Set
    End Property

    Private _PointsAccumlated As Decimal
    Public Property PointsAccumlated As Decimal
        Get
            Return _PointsAccumlated
        End Get
        Set(ByVal value As Decimal)
            _PointsAccumlated = value
        End Set
    End Property

    Private _IsJoiningPointAccumlated As Boolean
    Public Property IsJoiningPointAccumlated As Boolean
        Get
            Return _IsJoiningPointAccumlated
        End Get
        Set(ByVal value As Boolean)
            _IsJoiningPointAccumlated = value
        End Set
    End Property

    Private _AddressList As List(Of CLPCustomerAddressDTO)
    Public Property AddressList As List(Of CLPCustomerAddressDTO)
        Get
            If _AddressList Is Nothing Then
                _AddressList = New List(Of CLPCustomerAddressDTO)
            End If
            Return _AddressList
        End Get
        Set(value As List(Of CLPCustomerAddressDTO))
            _AddressList = value
        End Set
    End Property

    Private _ContactList As List(Of ContactDTO)
    Public Property ContactList As List(Of ContactDTO)
        Get
            If _ContactList Is Nothing Then
                _ContactList = New List(Of ContactDTO)
            End If
            Return _ContactList
        End Get
        Set(value As List(Of ContactDTO))
            _ContactList = value
        End Set
    End Property
    'CODE ADDED BY IRFAN
    Private _GSTNo As String
    Public Property GSTNo() As String
        Get
            Return _GSTNo
        End Get
        Set(value As String)
            _GSTNo = value
        End Set
    End Property
    Private _RegistrationStatus As String
    Public Property RegistrationStatus() As String
        Get
            Return _RegistrationStatus
        End Get
        Set(ByVal value As String)
            _RegistrationStatus = value
        End Set
    End Property
    Private _Level As String
    Public Property Level() As String
        Get
            Return _Level
        End Get
        Set(ByVal value As String)
            _Level = value
        End Set
    End Property

    Private _CardType As String
    Public Property CardType() As String
        Get
            Return _CardType
        End Get
        Set(ByVal value As String)
            _CardType = value
        End Set
    End Property

    Private _MessageType As MessageType
    Public Property MessageType() As MessageType
        Get
            Return _MessageType
        End Get
        Set(ByVal value As MessageType)
            _MessageType = value
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

    Private _ResidenceNumber As String
    Public Property ResidenceNumber() As String
        Get
            Return _ResidenceNumber
        End Get
        Set(ByVal value As String)
            _ResidenceNumber = value
        End Set
    End Property


#Region "SocialIds" '---- Added By Er. Mahesh Nagar

    Private _FacebookId As String
    Public Property FacebookId() As String
        Get
            Return _FacebookId
        End Get
        Set(ByVal value As String)
            _FacebookId = value
        End Set
    End Property

    Private _TwitterId As String
    Public Property TwitterId() As String
        Get
            Return _TwitterId
        End Get
        Set(ByVal value As String)
            _TwitterId = value
        End Set
    End Property

    Private _LinkedInID As String
    Public Property LinkedInID() As String
        Get
            Return _LinkedInID
        End Get
        Set(ByVal value As String)
            _LinkedInID = value
        End Set
    End Property

    Private _GooglePlusId As String
    Public Property GooglePlusId() As String
        Get
            Return _GooglePlusId
        End Get
        Set(ByVal value As String)
            _GooglePlusId = value
        End Set
    End Property

    Private _Hi5Id As String
    Public Property Hi5Id() As String
        Get
            Return _Hi5Id
        End Get
        Set(ByVal value As String)
            _Hi5Id = value
        End Set
    End Property

    Private _MySpaceId As String
    Public Property MySpaceId() As String
        Get
            Return _MySpaceId
        End Get
        Set(ByVal value As String)
            _MySpaceId = value
        End Set
    End Property

    Private _IbiboId As String
    Public Property IbiboId() As String
        Get
            Return _IbiboId
        End Get
        Set(ByVal value As String)
            _IbiboId = value
        End Set
    End Property

    Private _FourSquareId As String
    Public Property FourSquareId() As String
        Get
            Return _FourSquareId
        End Get
        Set(ByVal value As String)
            _FourSquareId = value
        End Set
    End Property

    Private _OrkutId As String
    Public Property OrkutId() As String
        Get
            Return _OrkutId
        End Get
        Set(ByVal value As String)
            _OrkutId = value
        End Set
    End Property

    Private _SkypeId As String
    Public Property SkypeId() As String
        Get
            Return _SkypeId
        End Get
        Set(ByVal value As String)
            _SkypeId = value
        End Set
    End Property

#End Region

    Private _SpouseFirstName As String = String.Empty
    Public Property SpouseFirstName As String
        Get
            Return _SpouseFirstName
        End Get
        Set(value As String)
            _SpouseFirstName = value
        End Set
    End Property

    Private _SpouseMiddleName As String
    Public Property SpouseMiddleName As String
        Get
            Return _SpouseMiddleName
        End Get
        Set(value As String)
            _SpouseMiddleName = value
        End Set
    End Property

    Private _SpouseLastName As String
    Public Property SpouseLastName As String
        Get
            Return _SpouseLastName
        End Get
        Set(value As String)
            _SpouseLastName = value
        End Set
    End Property

    Private _SpouseBirthDate As DateTime
    Public Property SpouseBirthDate As DateTime
        Get
            Return _SpouseBirthDate
        End Get
        Set(value As DateTime)
            _SpouseBirthDate = value
        End Set
    End Property

    Private _Occupation As String
    Public Property Occupation As String
        Get
            Return _Occupation
        End Get
        Set(value As String)
            _Occupation = value
        End Set
    End Property

    Private _SpouseOccupation As String
    Public Property SpouseOccupation As String
        Get
            Return _SpouseOccupation
        End Get
        Set(value As String)
            _SpouseOccupation = value
        End Set
    End Property

    Private _Title As String
    Public Property Title As String
        Get
            Return _Title
        End Get
        Set(value As String)
            _Title = value
        End Set
    End Property

    Private _SpouseTitle As String
    Public Property SpouseTitle As String
        Get
            Return _SpouseTitle
        End Get
        Set(value As String)
            _SpouseTitle = value
        End Set
    End Property

    Private _MarriageAnivDate As DateTime
    Public Property MarriageAnivDate As DateTime
        Get
            Return _MarriageAnivDate
        End Get
        Set(value As DateTime)
            _MarriageAnivDate = value
        End Set
    End Property

    Private _OfficeNumber As String
    Public Property OfficeNumber() As String
        Get
            Return _OfficeNumber
        End Get
        Set(ByVal value As String)
            _OfficeNumber = value
        End Set
    End Property

    Private _Education As String
    Public Property Education() As String
        Get
            Return _Education
        End Get
        Set(ByVal value As String)
            _Education = value
        End Set
    End Property

    Private _MaritalStatus As String
    Public Property MaritalStatus() As String
        Get
            Return _MaritalStatus
        End Get
        Set(ByVal value As String)
            _MaritalStatus = value
        End Set
    End Property

    Private _PromotionInfobyEmail As Int16
    Public Property PromotionInfobyEmail() As String
        Get
            Return _PromotionInfobyEmail
        End Get
        Set(ByVal value As String)
            _PromotionInfobyEmail = value
        End Set
    End Property

    Private _PromotionInfobySMS As Int16
    Public Property PromotionInfobySMS() As Int16
        Get
            Return _PromotionInfobySMS
        End Get
        Set(ByVal value As Int16)
            _PromotionInfobySMS = value
        End Set
    End Property

    '----new customer
    Private _CompanyName As String
    Public Property CompanyName As String
        Get
            Return _CompanyName
        End Get
        Set(value As String)
            _CompanyName = value
        End Set
    End Property
    ' add by khusrao adil
    'for reminder for evazz pizza
    Private _ReminderComment As String
    Public Property ReminderComment() As String
        Get
            Return _ReminderComment
        End Get
        Set(ByVal value As String)
            _ReminderComment = value
        End Set
    End Property
End Class
