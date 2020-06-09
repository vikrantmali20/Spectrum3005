Public Class VoucherDtl
    Inherits BaseModel

    Private _IsSelected As Boolean
    Public Property IsSelected As Boolean
        Get
            Return _IsSelected
        End Get
        Set(ByVal value As Boolean)
            _IsSelected = value
        End Set
    End Property

    Private _VoucherID As String
    Public Property VoucherID As String
        Get
            Return _VoucherID
        End Get
        Set(ByVal value As String)
            _VoucherID = value
        End Set
    End Property

    Private _SiteCode As String
    Public Property SiteCode As String
        Get
            Return _SiteCode
        End Get
        Set(ByVal value As String)
            _SiteCode = value
        End Set
    End Property

    Private _FinYear As String
    Public Property FinYear As String
        Get
            Return _FinYear
        End Get
        Set(ByVal value As String)
            _FinYear = value
        End Set
    End Property

    Private _Amount As Decimal
    Public Property Amount As Decimal
        Get
            Return _Amount
        End Get
        Set(ByVal value As Decimal)
            _Amount = value
            NotifyPropertyChanged("Amount")
        End Set
    End Property

    Private _LineNumber As Integer
    Public Property LineNumber As Integer
        Get
            Return _LineNumber
        End Get
        Set(ByVal value As Integer)
            _LineNumber = value
        End Set
    End Property

    Private _Narration As String
    Public Property Narration As String
        Get
            Return _Narration
        End Get
        Set(ByVal value As String)
            _Narration = value
            NotifyPropertyChanged("Narration")
        End Set
    End Property
End Class
