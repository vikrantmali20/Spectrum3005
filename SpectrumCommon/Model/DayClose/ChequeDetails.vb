Public Class ChequeDetails
    Public Sub New()

    End Sub

    Private _ChequeNumber As String
    Public Property ChequeNumber As String
        Get
            Return _ChequeNumber
        End Get
        Set(ByVal value As String)
            _ChequeNumber = value
        End Set
    End Property

    Private _Amount As Decimal
    Public Property Amount As Decimal
        Get
            Return _Amount
        End Get
        Set(ByVal value As Decimal)
            _Amount = value
        End Set
    End Property
    Private _SrNo1 As String         'vipin on 05-05-2017 PC
    Public Property SrNo1 As String
        Get
            Return _SrNo1
        End Get
        Set(ByVal value As String)
            _SrNo1 = value
        End Set
    End Property
    Private _BankName As String      'vipin on 05-05-2017 PC
    Public Property BankName As String
        Get
            Return _BankName
        End Get
        Set(ByVal value As String)
            _BankName = value
        End Set
    End Property
End Class
