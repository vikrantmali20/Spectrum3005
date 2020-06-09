Imports System.Collections
Public Class DayCloseBankDetailsResponse
    Public Sub New()

    End Sub

    Private _ActualTotalCashAmt As Decimal
    Public Property ActualTotalCashAmt As Decimal
        Get
            Return _ActualTotalCashAmt
        End Get
        Set(ByVal value As Decimal)
            _ActualTotalCashAmt = value
        End Set
    End Property

    Private _ImprestAmount As Decimal    'vipin
    Public Property ImprestAmount As Decimal
        Get
            Return _ImprestAmount
        End Get
        Set(ByVal value As Decimal)
            _ImprestAmount = value
        End Set
    End Property

    Private _CashDenominationList As List(Of CashDenominationDtl)
    Public Property CashDenominationList As List(Of CashDenominationDtl)
        Get
            If _CashDenominationList Is Nothing Then
                _CashDenominationList = New List(Of CashDenominationDtl)
            End If
            Return _CashDenominationList
        End Get
        Set(ByVal value As List(Of CashDenominationDtl))
            _CashDenominationList = value
        End Set
    End Property

    Private _OtherTenderDetail As IList(Of OtherTenderDetail)     'vipin on 05-05-2017 PC
    Public Property OtherTenderDetail As IList(Of OtherTenderDetail)
        Get
            If _OtherTenderDetail Is Nothing Then
                _OtherTenderDetail = New List(Of OtherTenderDetail)
            End If
            Return _OtherTenderDetail
        End Get
        Set(ByVal value As IList(Of OtherTenderDetail))
            _OtherTenderDetail = value
        End Set
    End Property

    Private _ChequeDetailsList As IList(Of ChequeDetails)
    Public Property ChequeDetailsList As IList(Of ChequeDetails)
        Get
            If _ChequeDetailsList Is Nothing Then
                _ChequeDetailsList = New List(Of ChequeDetails)
            End If
            Return _ChequeDetailsList
        End Get
        Set(ByVal value As IList(Of ChequeDetails))
            _ChequeDetailsList = value
        End Set
    End Property

    Private _TotalChequeAmt As Decimal
    Public Property TotalChequeAmt As Decimal
        Get
            Return _TotalChequeAmt
        End Get
        Set(ByVal value As Decimal)
            _TotalChequeAmt = value
        End Set
    End Property

    Public ReadOnly Property AmountGoingToBank
        Get
            ' changed by mahesh 12944 Day Close : Amount going to bank displayed on Day close screen and amount going to bank displayed on final report is not matchin
            'Return TotalChequeAmt
            Return 0
        End Get
    End Property
End Class
