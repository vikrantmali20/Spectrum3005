Imports SpectrumCommon
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Collections
Public Interface IStockTake(Of T)
    Inherits IDayCloseScreens(Of T)

    Function GetStockGroups(ByVal groupType As ArticleGroupType) As BindingList(Of ArticleGroupDetails)

    Function DeleteStockTakeData(ByVal request As DayCloseDataRequestModel(Of T)) As Boolean

    Sub GetNewItemMasterData(ByRef request As DayCloseDataRequestModel(Of T), Optional ByVal AllowQtyZero As Boolean = False)
    Function ClearStockTakeData(ByVal request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.StockTakeDetails)) As Boolean
End Interface
'added by khusrao adil on 17-04-2018 for jk sprint 35
Public Interface IRawMaterial(Of T)
    Inherits IDayCloseScreens(Of T)
    Function ClearStockTakeData(ByVal request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.RawMaterialDetails)) As Boolean
End Interface