Imports System.ComponentModel
<Serializable()>
Public Class SubProductDetails
    Inherits DayCloseBaseModel

    Public Sub New()

    End Sub

    Private _RecipeCode As String
    Public Property RecipeCode As String
        Get
            Return _RecipeCode
        End Get
        Set(ByVal value As String)
            _RecipeCode = value
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

    Private _BatchCount As Double?
    Public Property BatchCount As Double?
        Get
            Return _BatchCount
        End Get
        Set(ByVal value As Double?)
            _BatchCount = value
            If Not String.IsNullOrEmpty(RecipeCode) AndAlso _BatchCount IsNot Nothing AndAlso Me.RecipeData IsNot Nothing Then
                Dim recipe = Me.RecipeData.Where(Function(rcp) rcp.RecipeCode = Me.RecipeCode).FirstOrDefault()
                If recipe IsNot Nothing Then
                    CalculatedQty = BatchCount * recipe.Quantity
                End If
            End If
        End Set
    End Property

    Private _EnteredQty As Decimal?
    Public Property EnteredQty As Decimal?
        Get
            Return _EnteredQty
        End Get
        Set(ByVal value As Decimal?)
            _EnteredQty = value
        End Set
    End Property

    Private _CalculatedQty As Decimal
    Public Property CalculatedQty As Decimal
        Get
            Return _CalculatedQty
        End Get
        Set(ByVal value As Decimal)
            _CalculatedQty = value
            NotifyPropertyChanged("CalculatedQty")
        End Set
    End Property

    Private _EnteredQtyBUOM As Decimal
    Public Property EnteredQtyBUOM As Decimal
        Get
            Return _EnteredQtyBUOM
        End Get
        Set(ByVal value As Decimal)
            _EnteredQtyBUOM = value
        End Set
    End Property

    Private _CalculatedQtyBUOM As Decimal
    Public Property CalculatedQtyBUOM As Decimal
        Get
            Return _CalculatedQtyBUOM
        End Get
        Set(ByVal value As Decimal)
            _CalculatedQtyBUOM = value
        End Set
    End Property

    Private _RecipeData As BindingList(Of RecipeDetails)
    Public Property RecipeData As BindingList(Of RecipeDetails)
        Get
            Return _RecipeData
        End Get
        Set(ByVal value As BindingList(Of RecipeDetails))
            _RecipeData = value
        End Set
    End Property
End Class
