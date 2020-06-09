
Public Class PrintVoucherRequest
    Private _VoucherHeader As VoucherHdr
    Public Property VoucherHeader As VoucherHdr
        Get
            Return _VoucherHeader
        End Get
        Set(ByVal value As VoucherHdr)
            _VoucherHeader = value
        End Set
    End Property

    Private _dtPrinterInfo As DataTable
    Public Property dtPrinterInfo As DataTable
        Get
            Return _dtPrinterInfo
        End Get
        Set(ByVal value As DataTable)
            _dtPrinterInfo = value
        End Set
    End Property

    Private _VoucherAccountTypes As List(Of VoucherAccountType)
    Private _VoucherParty As List(Of PartyDTO)
    Public Property VoucherAccountTypes As List(Of VoucherAccountType)
        Get
            If _VoucherAccountTypes Is Nothing Then
                _VoucherAccountTypes = New List(Of VoucherAccountType)
            End If
            Return _VoucherAccountTypes
        End Get
        Set(ByVal value As List(Of VoucherAccountType))
            _VoucherAccountTypes = value
        End Set
    End Property

    Public Property VoucherParty As List(Of PartyDTO)
        Get
            If _VoucherParty Is Nothing Then
                _VoucherParty = New List(Of PartyDTO)
            End If
            Return _VoucherParty
        End Get
        Set(ByVal value As List(Of PartyDTO))
            _VoucherParty = value
        End Set
    End Property

    Private _IsPrviewRequiredForVchr As Boolean
    Public Property IsPrviewRequiredForVchr As Boolean
        Get
            Return _IsPrviewRequiredForVchr
        End Get
        Set(value As Boolean)
            _IsPrviewRequiredForVchr = value
        End Set
    End Property
End Class
