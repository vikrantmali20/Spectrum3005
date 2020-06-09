Imports SpectrumCommon
Imports System.Collections
Imports SpectrumBL

Public Class StockTakeController
    Inherits NotificationBase

    Private _DayCloseScreenList As IList(Of DayCloseScreenConfig)
    Private Property DayCloseScreenList As IList(Of DayCloseScreenConfig)
        Get
            Return _DayCloseScreenList
        End Get
        Set(ByVal value As IList(Of DayCloseScreenConfig))
            _DayCloseScreenList = value
        End Set
    End Property

    Private _CurrentScreen As DayCloseScreenConfig
    Public Property CurrentScreen As DayCloseScreenConfig
        Get
            Return _CurrentScreen
        End Get
        Set(ByVal value As DayCloseScreenConfig)
            _CurrentScreen = value
        End Set
    End Property

    Private _IsLastScreen As Boolean
    Public Property IsLastScreen As Boolean
        Get
            Return _IsLastScreen
        End Get
        Set(ByVal value As Boolean)
            _IsLastScreen = value
        End Set
    End Property

    Private _IsFirstScreen As Boolean
    Public Property IsFirstScreen As Boolean
        Get
            Return _IsFirstScreen
        End Get
        Set(ByVal value As Boolean)
            _IsFirstScreen = value
        End Set
    End Property

    Private _objClsDayClose As New clsDayClose

    Public Sub New()
        Try

            DayCloseScreenList = _objClsDayClose.GetDayCloseScreenConfig(Modules.Day_Close, DayCloseScreens.StockTakedetails)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function GetNextScreen() As DayCloseScreenConfig
        Try
            Dim screenInfo As DayCloseScreenConfig
            If Not IsLastScreen Then
                If CurrentScreen Is Nothing Then
                    If DayCloseScreenList IsNot Nothing AndAlso DayCloseScreenList.Count > 0 Then
                        screenInfo = DayCloseScreenList(0)
                        IsFirstScreen = True
                    End If
                Else
                    If DayCloseScreenList.IndexOf(CurrentScreen) + 1 <= DayCloseScreenList.Count - 1 Then
                        screenInfo = DayCloseScreenList(DayCloseScreenList.IndexOf(CurrentScreen) + 1)
                    End If
                    'screenInfo = DayCloseScreenList.Where(Function(config) config.Sequence = CurrentScreen.Sequence + 1).FirstOrDefault()
                    IsFirstScreen = False
                End If
                If screenInfo IsNot Nothing Then
                    IsLastScreen = DayCloseScreenList.IndexOf(screenInfo) + 1 > DayCloseScreenList.Count - 1
                    'IsLastScreen = IIf((DayCloseScreenList.Where(Function(config) config.Sequence = screenInfo.Sequence + 1).FirstOrDefault()) Is Nothing, True, False)
                End If
                CurrentScreen = screenInfo
            End If
            Return screenInfo
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetPreviousScreen() As DayCloseScreenConfig
        Try
            Dim screenInfo As DayCloseScreenConfig
            If Not IsFirstScreen Then
                If CurrentScreen IsNot Nothing Then
                    If DayCloseScreenList.IndexOf(CurrentScreen) - 1 <= DayCloseScreenList.Count - 1 Then
                        screenInfo = DayCloseScreenList(DayCloseScreenList.IndexOf(CurrentScreen) - 1)
                    End If
                    'screenInfo = DayCloseScreenList.Where(Function(config) config.Sequence = CurrentScreen.Sequence - 1).FirstOrDefault()
                    IsLastScreen = False
                End If
                If screenInfo IsNot Nothing Then
                    IsFirstScreen = DayCloseScreenList.IndexOf(screenInfo) = 0
                    'IsFirstScreen = IIf((DayCloseScreenList.Where(Function(config) config.Sequence = screenInfo.Sequence - 1).FirstOrDefault()) Is Nothing, True, False)
                End If
                CurrentScreen = screenInfo
            End If
            Return screenInfo
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
End Class
