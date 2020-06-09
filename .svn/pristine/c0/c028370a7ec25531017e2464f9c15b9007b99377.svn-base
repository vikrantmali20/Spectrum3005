Public Class PartyDTO
    Inherits BaseModel

    Private _PartyCode As String
    Public Property PartyCode As String
        Get
            Return _PartyCode
        End Get
        Set(ByVal value As String)
            _PartyCode = value
        End Set
    End Property

    Private _PartyName As String
    Public Property PartyName As String
        Get
            Return _PartyName
        End Get
        Set(ByVal value As String)
            _PartyName = value
        End Set
    End Property

    Private _PartyType As String
    Public Property PartyType As String
        Get
            Return _PartyType
        End Get
        Set(ByVal value As String)
            _PartyType = value
        End Set
    End Property

    Private _PartyDisplayName As String
    Public Property PartyDisplayName As String
        Get
            If Not String.IsNullOrEmpty(PartyCode) AndAlso Not String.IsNullOrEmpty(PartyName) Then
                _PartyDisplayName = PartyName & " - " & PartyCode
            ElseIf Not String.IsNullOrEmpty(PartyName) Then
                _PartyDisplayName = PartyName
            End If
            Return _PartyDisplayName
        End Get
        Set(ByVal value As String)
            _PartyDisplayName = value
        End Set
    End Property
End Class
