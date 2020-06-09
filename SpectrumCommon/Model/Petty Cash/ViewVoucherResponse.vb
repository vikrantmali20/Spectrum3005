Public Class ViewVoucherResponse

    Private _VoucherHeader As List(Of VoucherHdr)
    Public Property VoucherHeader As List(Of VoucherHdr)
        Get
            Return _VoucherHeader
        End Get
        Set(ByVal value As List(Of VoucherHdr))
            _VoucherHeader = value
        End Set
    End Property

    Private _VoucherTable As DataTable
    Public Property VoucherTable As DataTable
        Get
            Return _VoucherTable
        End Get
        Set(ByVal value As DataTable)
            _VoucherTable = value
        End Set
    End Property
End Class
