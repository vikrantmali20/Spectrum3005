Imports System.ComponentModel
<Serializable()>
Public Class WastageDetails
    Inherits DayCloseBaseModel
    Public Sub New()

    End Sub

    Private _IsSelected As Boolean
    Public Property IsSelected As Boolean
        Get
            Return _IsSelected
        End Get
        Set(ByVal value As Boolean)
            _IsSelected = value
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
                If ArticleBaseUOM <> WastageUOMCode Then
                    WastageBUOMQty = _EnteredQty * Multiplier
                Else
                    WastageBUOMQty = _EnteredQty
                End If
            End If
        End Set
    End Property

    Private _WastageBUOMQty As Decimal
    Public Property WastageBUOMQty As Decimal
        Get
            Return _WastageBUOMQty
        End Get
        Set(ByVal value As Decimal)
            _WastageBUOMQty = value
        End Set
    End Property

    Private _ReasonData As BindingList(Of ReasonDetails)
    Public Property ReasonData As BindingList(Of ReasonDetails)
        Get
            Return _ReasonData
        End Get
        Set(ByVal value As BindingList(Of ReasonDetails))
            _ReasonData = value
        End Set
    End Property

    Private _ReasonCode As String
    Public Property ReasonCode As String
        Get
            Return _ReasonCode
        End Get
        Set(ByVal value As String)
            _ReasonCode = value
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

    Private _WastageUOMCode As String
    Public Property WastageUOMCode As String
        Get
            Return _WastageUOMCode
        End Get
        Set(ByVal value As String)
            _WastageUOMCode = value
            If EnteredQty IsNot Nothing Then
                If ArticleBaseUOM <> _WastageUOMCode Then
                    WastageBUOMQty = EnteredQty * Multiplier
                Else
                    WastageBUOMQty = EnteredQty
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
