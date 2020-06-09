Public Class GetVoucherEntryResponse

    Private _VoucherHeader As VoucherHdr
    Private _VoucherTypes As List(Of VoucherType)
    Private _VoucherAccountTypes As List(Of VoucherAccountType)
    Private _VoucherParty As List(Of PartyDTO)

    Public Property VoucherHeader As VoucherHdr
        Get
            Return _VoucherHeader
        End Get
        Set(ByVal value As VoucherHdr)
            _VoucherHeader = value
        End Set
    End Property

    Public Property VoucherTypes As List(Of VoucherType)
        Get
            If _VoucherTypes Is Nothing Then
                _VoucherTypes = New List(Of VoucherType)
            End If
            Return _VoucherTypes
        End Get
        Set(ByVal value As List(Of VoucherType))
            _VoucherTypes = value
        End Set
    End Property

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
End Class
