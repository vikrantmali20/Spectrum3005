Imports System.ComponentModel
Imports SpectrumCommon

Public Interface IDayCloseScreens(Of T)

    Function GetDayCloseData(ByRef request As DayCloseDataRequestModel(Of T), Optional ByVal AllowQtyZero As Boolean = False) As BindingList(Of T)

    Function CheckIfDataExist(ByRef request As DayCloseDataRequestModel(Of T)) As BindingList(Of T)

    Function SaveDayCloseData(ByRef request As DayCloseDataRequestModel(Of T)) As Boolean
End Interface
