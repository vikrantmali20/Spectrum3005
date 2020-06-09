Public Class DayCloseScreenConfig
    Inherits DayCloseBaseModel

    Public Sub New()

    End Sub

    Private _ModuleName As String
    Public Property ModuleName As String
        Get
            Return _ModuleName
        End Get
        Set(ByVal value As String)
            _ModuleName = value
        End Set
    End Property

    Private _ScreenName As String
    Public Property ScreenName As String
        Get
            Return _ScreenName
        End Get
        Set(ByVal value As String)
            _ScreenName = value
        End Set
    End Property

    Private _Sequence As Integer
    Public Property Sequence As Integer
        Get
            Return _Sequence
        End Get
        Set(ByVal value As Integer)
            _Sequence = value
        End Set
    End Property

    Private _Script As String
    Public Property Script As String
        Get
            Return _Script
        End Get
        Set(ByVal value As String)
            _Script = value
        End Set
    End Property
End Class
