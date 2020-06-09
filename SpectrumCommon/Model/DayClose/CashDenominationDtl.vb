Public Class CashDenominationDtl
    Inherits NotificationBase

    Public QuantityChanged As Action
    Public Sub New()

    End Sub

    Private _CurrencyCode As String
    Public Property CurrencyCode As String
        Get
            Return _CurrencyCode
        End Get
        Set(ByVal value As String)
            _CurrencyCode = value
        End Set
    End Property

    Private _DenominationAmt As Decimal
    Public Property DenominationAmt As Decimal
        Get
            Return _DenominationAmt
        End Get
        Set(ByVal value As Decimal)
            _DenominationAmt = value
        End Set
    End Property

    Private _Quantity As Decimal
    Public Property Quantity As Decimal
        Get
            Return _Quantity
        End Get
        Set(ByVal value As Decimal)
            _Quantity = value
        End Set
    End Property

    Private _EnteredQuantity As Integer?
    Public Property EnteredQuantity As Integer?
        Get
            Return _EnteredQuantity
        End Get
        Set(ByVal value As Integer?)
            _EnteredQuantity = value
            If _EnteredQuantity IsNot Nothing Then
                TotalAmt = _EnteredQuantity * DenominationAmt
                If QuantityChanged IsNot Nothing Then
                    QuantityChanged()
                End If
            Else
                TotalAmt = 0
            End If
        End Set
    End Property

    Private _TotalAmt As Decimal
    Public Property TotalAmt As Decimal
        Get
            Return _TotalAmt
        End Get
        Set(ByVal value As Decimal)
            _TotalAmt = value
            NotifyPropertyChanged("TotalAmt")
        End Set
    End Property
End Class
