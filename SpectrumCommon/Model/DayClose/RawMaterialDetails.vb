Imports System.ComponentModel
<Serializable()>
Public Class RawMaterialDetails
    Inherits DayCloseBaseModel
    Public Sub New()

    End Sub

    Private _GroupCode As String
    Public Property GroupCode As String
        Get
            Return _GroupCode
        End Get
        Set(ByVal value As String)
            _GroupCode = value
        End Set
    End Property

    Private _IsSelected As Boolean
    Public Property IsSelected As Boolean
        Get
            Return _IsSelected
        End Get
        Set(ByVal value As Boolean)
            _IsSelected = value
        End Set
    End Property
    Private _SubGroupId As String
    Public Property SubGroupId As String
        Get
            Return _SubGroupId
        End Get
        Set(ByVal value As String)
            _SubGroupId = value
        End Set
    End Property

    Private _ArticleBaseUOM As String
    Public Property ArticleBaseUOM As String
        Get
            Return _ArticleBaseUOM
        End Get
        Set(ByVal value As String)
            _ArticleBaseUOM = value
        End Set
    End Property

    Private _MAP As Decimal
    Public Property MAP As Decimal
        Get
            Return _MAP
        End Get
        Set(ByVal value As Decimal)
            _MAP = value
        End Set
    End Property

    Private _EnteredQty As Decimal?
    Public Property EnteredQty As Decimal?
        Get
            Return _EnteredQty
        End Get
        Set(ByVal value As Decimal?)
            _EnteredQty = value
            If _EnteredQty IsNot Nothing Then
                If ArticleBaseUOM <> StockTakeUOMCode Then
                    BUOMQty = _EnteredQty * Multiplier
                Else
                    BUOMQty = _EnteredQty
                End If
            End If
            NotifyPropertyChanged("EnteredQty")
        End Set
    End Property

    Private _BUOMQty As Decimal
    Public Property BUOMQty As Decimal
        Get
            Return _BUOMQty
        End Get
        Set(ByVal value As Decimal)
            _BUOMQty = value
        End Set
    End Property

    Private _CurrentStock As Decimal
    Public Property CurrentStock As Decimal
        Get
            Return _CurrentStock
        End Get
        Set(ByVal value As Decimal)
            _CurrentStock = value
        End Set
    End Property

    Private _UOMData As BindingList(Of ArticleUOMDetails)
    Public Property UOMData As BindingList(Of ArticleUOMDetails)
        Get
            Return _UOMData
        End Get
        Set(ByVal value As BindingList(Of ArticleUOMDetails))
            _UOMData = value
        End Set
    End Property

    Private _StockTakeUOMCode As String
    Public Property StockTakeUOMCode As String
        Get
            Return _StockTakeUOMCode
        End Get
        Set(ByVal value As String)
            _StockTakeUOMCode = value
            If EnteredQty IsNot Nothing Then
                If ArticleBaseUOM <> _StockTakeUOMCode Then
                    BUOMQty = EnteredQty * Multiplier
                Else
                    BUOMQty = EnteredQty
                End If
            End If
        End Set
    End Property

    Private _Multiplier As Decimal
    Public Property Multiplier As Decimal
        Get
            Return _Multiplier
        End Get
        Set(ByVal value As Decimal)
            _Multiplier = value
        End Set
    End Property
End Class
