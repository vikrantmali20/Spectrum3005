Imports SpectrumCommon
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Collections
Public Class DayCloseStockTakeDetails
    Inherits clsDayCloseBase
    Implements IStockTake(Of StockTakeDetails)

    Public Sub New(ByVal FlagHideControlsFromStockTake As Boolean)
        HideControlsFromStockTake = FlagHideControlsFromStockTake
    End Sub

    Public _HideControlsFromStockTake As Boolean = False
    Public Property HideControlsFromStockTake As Boolean
        Get
            Return _HideControlsFromStockTake
        End Get
        Set(ByVal value As Boolean)
            _HideControlsFromStockTake = value
        End Set
    End Property

    Public Function CheckIfDataExist(ByRef request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.StockTakeDetails)) As System.ComponentModel.BindingList(Of SpectrumCommon.StockTakeDetails) Implements IDayCloseScreens(Of SpectrumCommon.StockTakeDetails).CheckIfDataExist
        Try
            'Dim query As String = "select D.ArticleCode ,D.ArticleBaseUOM ,D.Quantity ,D.StockTakeUOM,D.MAP ," & _
            '                        "D.SiteCode,d.DayCloseDate ,D.STATUS ,A.ArticleShortName ,D.BUOMQuantity,D.CurrentStock,D.GroupCode " & _
            '                        " from DayCloseStockTakeDetails  AS D Inner Join MstArticle As A On D.ArticleCode = A.ArticleCode " & _
            '                       "where D.SiteCode = '" & request.SiteCode & "' and D.DayCloseDate = '" & request.GetDayCloseDateAsString() & "' Order By A.ArticleShortName"

            Dim query As String = "select  D.ArticleCode ,D.ArticleBaseUOM ,D.Quantity ,D.StockTakeUOM,D.MAP ," & _
                                   "D.SiteCode,d.DayCloseDate ,D.STATUS ,A.ArticleShortName ,D.BUOMQuantity,D.CurrentStock,D.GroupCode,Mag.SubGroup " & _
                                   " from DayCloseStockTakeDetails  AS D Inner Join MstArticle As A On D.ArticleCode = A.ArticleCode " & _
                                   " left outer join MSTArticleGroupDtl MAG on D.ArticleCode = MAG.ArticleCode and D.GroupCode=MAg.GroupID where D.SiteCode = '" & request.SiteCode & "' and D.DayCloseDate = '" & request.GetDayCloseDateAsString() & "' and D.status=1 and MAg.GroupID='" & request.Query & "' Order By MAG.SubGroup"

            Dim stockTakeList As BindingList(Of StockTakeDetails)
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    stockTakeList = New BindingList(Of StockTakeDetails)
                    Do While dataReader.Read()
                        Dim stockTakeDetails As New StockTakeDetails
                        stockTakeDetails.HideControlsFromStockTake = HideControlsFromStockTake
                        stockTakeDetails.ArticleCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        stockTakeDetails.ArticleBaseUOM = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        stockTakeDetails.StockTakeUOMCode = IIf(IsDBNull(dataReader.GetString(3)), Nothing, dataReader.GetString(3))
                        'stockTakeDetails.EnteredQty = IIf(IsDBNull(dataReader.GetDecimal(2)), Nothing, dataReader.GetDecimal(2))
                        stockTakeDetails.EnteredQty = IIf(IsDBNull(dataReader(2)), Nothing, dataReader(2))
                        stockTakeDetails.MAP = IIf(IsDBNull(dataReader.GetDecimal(4)), 0, dataReader.GetDecimal(4))
                        stockTakeDetails.SiteCode = IIf(IsDBNull(dataReader.GetString(5)), String.Empty, dataReader.GetString(5))
                        stockTakeDetails.DayCloseDate = IIf(IsDBNull(dataReader.GetDateTime(6)), Nothing, dataReader.GetDateTime(6))
                        stockTakeDetails.Status = IIf(IsDBNull(dataReader.GetBoolean(7)), False, dataReader.GetBoolean(7))
                        stockTakeDetails.ArticleName = IIf(IsDBNull(dataReader.GetString(8)), String.Empty, dataReader.GetString(8))
                        stockTakeDetails.BUOMQty = IIf(IsDBNull(dataReader.GetDecimal(9)), 0, dataReader.GetDecimal(9))
                        stockTakeDetails.CurrentStock = IIf(IsDBNull(dataReader.GetDecimal(10)), 0, dataReader.GetDecimal(10))
                        stockTakeDetails.GroupCode = IIf(IsDBNull(dataReader.GetString(11)), String.Empty, dataReader.GetString(11))
                        stockTakeDetails.SubGroupId = IIf(IsDBNull(dataReader(12)), 0, dataReader(12))
                        stockTakeList.Add(stockTakeDetails)
                    Loop
                End If
            End Using
            If stockTakeList IsNot Nothing Then
                For Each stockTake In stockTakeList
                    stockTake.CurrentStock = GetCurrentArticleStock(request.SiteCode, stockTake.ArticleCode)
                    stockTake.UOMData = GetArticleUOMList(New List(Of String) From {UOMTypes.BaseUnitofMeasure.ToString(), UOMTypes.DistributionUnitofMeasure.ToString()}, stockTake.ArticleCode)
                    Dim distributionUOM = stockTake.UOMData.Where(Function(a) a.UOMCode <> stockTake.ArticleBaseUOM).FirstOrDefault()
                    If distributionUOM IsNot Nothing Then
                        stockTake.Multiplier = GetUOMConversionFactor(stockTake.ArticleCode, distributionUOM.UOMCode)
                    Else
                        stockTake.Multiplier = 1
                    End If
                Next
            End If
            Return stockTakeList
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function GetDayCloseData(ByRef request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.StockTakeDetails), Optional ByVal AllowQtyZero As Boolean = False) As System.ComponentModel.BindingList(Of SpectrumCommon.StockTakeDetails) Implements IDayCloseScreens(Of SpectrumCommon.StockTakeDetails).GetDayCloseData
        Try
            'Dim stockTakeList As BindingList(Of StockTakeDetails) = CheckIfDataExist(request)
            Dim stockTakeList As BindingList(Of StockTakeDetails)
            'If stockTakeList IsNot Nothing Then
            '    Return stockTakeList
            'Else
            'Dim query As String = "select articlecode,ArticleShortName ,BaseUnitofMeasure from MstArticle where ArticleCode in ( select ArticleCode  from MSTArticleGroupDtl where GroupID = '" & request.Query & "')  and ArticleActive = 1 Order By ArticleShortName"
            'Added by Khusrao Adil
            'added SubGroup coloumn in query for color code requirement
            'Dim query As String = "select a.articlecode,ArticleShortName ,BaseUnitofMeasure,SubGroup from MstArticle  As a " & _
            '                        "Inner join MSTArticleGroupDtl As b on a.ArticleCode = b.ArticleCode " & _
            '                        "where b.GroupID = '" & request.Query & "' " & _
            '                        "and a.ArticleActive = 1 and b.Status=1 and a.ArticleCode not in (select MasterArticleCode  from MasterArticleMap) Order By a.ArticleShortName"
            'modifed by khusrao adil on 11-12-2017 for jk sprint 32
            'button article shoud not display when salesinforecord have entry with status as false for that article specific with site
            Dim query As String = " select a.articlecode,ArticleShortName ,BaseUnitofMeasure,SubGroup from MstArticle  As a " & _
                                  " Inner join MSTArticleGroupDtl As b on a.ArticleCode = b.ArticleCode " & _
                                  " Inner Join salesinforecord As SI on SI.ArticleCode = b.ArticleCode and SI.sitecode='" & request.SiteCode & "' and SI.status=1  " & _
                                  " where b.GroupID = '" & request.Query & "' " & _
                                  " and a.ArticleActive = 1 and b.Status=1 and a.ArticleCode not in (select MasterArticleCode  from MasterArticleMap) Order By a.ArticleShortName"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    stockTakeList = New BindingList(Of StockTakeDetails)
                    Do While dataReader.Read()
                        Dim stockTake As New StockTakeDetails
                        stockTake.HideControlsFromStockTake = HideControlsFromStockTake
                        stockTake.HideControlsFromStockTake = HideControlsFromStockTake
                        stockTake.ArticleCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        stockTake.ArticleName = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        stockTake.ArticleBaseUOM = IIf(IsDBNull(dataReader.GetString(2)), String.Empty, dataReader.GetString(2))
                        stockTake.SubGroupId = IIf(IsDBNull(dataReader(3)), 0, dataReader(3))
                        'Added by Khusrao Adil
                        'set default quantity as zero
                        If AllowQtyZero Then
                            stockTake.EnteredQty = 0
                        End If
                        stockTake.GroupCode = request.Query
                        stockTakeList.Add(stockTake)
                    Loop
                End If
            End Using
            If stockTakeList IsNot Nothing Then
                For Each stockTakeDetails In stockTakeList
                    stockTakeDetails.MAP = GetArticleMAP(request.SiteCode, stockTakeDetails.ArticleCode)
                    stockTakeDetails.CurrentStock = GetCurrentArticleStock(request.SiteCode, stockTakeDetails.ArticleCode)
                    stockTakeDetails.UOMData = GetArticleUOMList(New List(Of String) From {UOMTypes.BaseUnitofMeasure.ToString(), UOMTypes.DistributionUnitofMeasure.ToString()}, stockTakeDetails.ArticleCode)
                    If stockTakeDetails.UOMData IsNot Nothing AndAlso stockTakeDetails.UOMData.Count > 0 Then
                        stockTakeDetails.StockTakeUOMCode = stockTakeDetails.UOMData(0).UOMCode
                    End If
                    Dim distributionUOM = stockTakeDetails.UOMData.Where(Function(a) a.UOMCode <> stockTakeDetails.ArticleBaseUOM).FirstOrDefault()
                    If distributionUOM IsNot Nothing Then
                        stockTakeDetails.Multiplier = GetUOMConversionFactor(stockTakeDetails.ArticleCode, distributionUOM.UOMCode)
                    Else
                        stockTakeDetails.Multiplier = 1
                    End If
                Next
            End If
            Return stockTakeList
            'End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function SaveDayCloseData(ByRef request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.StockTakeDetails)) As Boolean Implements IDayCloseScreens(Of SpectrumCommon.StockTakeDetails).SaveDayCloseData
        Try
            Dim result As Boolean = False
            If request IsNot Nothing AndAlso request.DayCloseData.Count > 0 Then
                'Dim deleteQuery As String = "Delete From DayCloseStockTakeDetails where SiteCode = '" & request.SiteCode & "' and DayCloseDate = '" & request.DayCloseDate & "'"
                'InsertOrUpdateRecord(deleteQuery)
                For Each stockDetails In request.DayCloseData
                    If CheckIfUpdateData(request, stockDetails) Then
                        If CheckIfQuantityChanged(request, stockDetails) Then
                            Dim updateQuery As String = "Update  DayCloseStockTakeDetails SET status=1, groupcode= '" & stockDetails.GroupCode & "', StockTakeUOM = '" & stockDetails.StockTakeUOMCode & "'," & _
                        "Quantity=" & stockDetails.EnteredQty & ", BUOMQuantity=" & stockDetails.BUOMQty & ", CurrentStock=" & stockDetails.CurrentStock & "," & _
                        "UPDATEDAT ='" & request.SiteCode & "', UPDATEDBY='" & request.UserId & "' , UPDATEDON= getdate() " & _
                        "where SiteCode = '" & request.SiteCode & "' And DayCloseDate = '" & request.GetDayCloseDateAsString() & "' and ArticleCode = '" & stockDetails.ArticleCode & "' "
                            InsertOrUpdateRecord(updateQuery)
                        End If                        
                    Else
                        Dim insertQuery As String = "Insert into DayCloseStockTakeDetails (GroupCode,ArticleCode , ArticleBaseUOM ,Quantity,BaseUOM, " & _
                        " StockTakeUOM,BUOMQuantity,CurrentStock, MAP, SiteCode, DayCloseDate, CREATEDAT, CREATEDBY" & _
                        ",CREATEDON , UPDATEDAT , UPDATEDBY , UPDATEDON,Status ) VAlues ('" & stockDetails.GroupCode & "', '" & stockDetails.ArticleCode & "','" & stockDetails.ArticleBaseUOM & "' " & _
                        "," & If(stockDetails.EnteredQty Is Nothing, "NULL", stockDetails.EnteredQty) & ",'" & stockDetails.ArticleBaseUOM & "','" & stockDetails.StockTakeUOMCode & "', " & stockDetails.BUOMQty & "," & stockDetails.CurrentStock & "" & _
                        "," & stockDetails.MAP & ",'" & request.SiteCode & "','" & request.GetDayCloseDateAsString() & "','" & request.SiteCode & "' " & _
                        ",'" & request.UserId & "', getdate() ,'" & request.SiteCode & "','" & request.UserId & "', getdate() , 1 )"
                        InsertOrUpdateRecord(insertQuery)
                    End If
                Next
                result = True
            Else
                result = True
            End If
            Return result
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Private Function CheckIfUpdateData(ByRef request As DayCloseDataRequestModel(Of StockTakeDetails), ByVal stockDetails As StockTakeDetails) As Boolean
        Try
            Dim isUpdate As Boolean = False
            Dim query As String = "select D.ArticleCode, D.Quantity " & _
                                  " from DayCloseStockTakeDetails  AS D " & _
                                 "where D.SiteCode = '" & request.SiteCode & "' and D.DayCloseDate = '" & request.GetDayCloseDateAsString() & "' and D.ArticleCode = '" & stockDetails.ArticleCode & "'"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    isUpdate = True
                End If
            End Using
            Return isUpdate
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            'CloseConnection()
        End Try
    End Function

    Private Function CheckIfQuantityChanged(ByRef request As DayCloseDataRequestModel(Of StockTakeDetails), ByVal stockDetails As StockTakeDetails) As Boolean
        Try
            Dim isUpdate As Boolean = False
            Dim query As String = "select D.ArticleCode, D.Quantity " & _
                                  " from DayCloseStockTakeDetails  AS D " & _
                                 "where D.SiteCode = '" & request.SiteCode & "' and D.DayCloseDate = '" & request.GetDayCloseDateAsString() & "' and D.ArticleCode = '" & stockDetails.ArticleCode & "'"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    dataReader.Read()
                    Dim Qty As Decimal = IIf(IsDBNull(dataReader(1)), Nothing, dataReader(1))
                    If Qty <> stockDetails.EnteredQty Then
                        isUpdate = True
                    End If
                End If
            End Using
            Return isUpdate
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            'CloseConnection()
        End Try
    End Function

    Private Function GetCurrentArticleStock(ByVal siteCode As String, ByVal articleCode As String) As Decimal
        Try
            Dim currentStock As Decimal = 0
            Dim query As String = "select PhysicalQty  from ArticleStockBalances where SiteCode = '" & siteCode & "' and ArticleCode = '" & articleCode & "'"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    dataReader.Read()
                    currentStock = IIf(IsDBNull(dataReader.GetDecimal(0)), 0, dataReader.GetDecimal(0))
                End If
            End Using
            Return currentStock
        Catch ex As Exception
            LogException(ex)
            Return 0
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function GetStockGroups(ByVal groupType As SpectrumCommon.ArticleGroupType) As System.ComponentModel.BindingList(Of SpectrumCommon.ArticleGroupDetails) Implements IStockTake(Of SpectrumCommon.StockTakeDetails).GetStockGroups
        Try
            Dim stockGroupList As BindingList(Of ArticleGroupDetails)
            Dim query As String = "select GroupID ,GroupName , Type from MSTArticleGroup where Type = '" & groupType.ToString() & "' Order BY GroupID"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    stockGroupList = New BindingList(Of ArticleGroupDetails)
                    Do While dataReader.Read()
                        Dim stockGroup As New ArticleGroupDetails
                        stockGroup.GroupId = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        stockGroup.GroupName = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        stockGroup.GroupType = IIf(IsDBNull(dataReader.GetString(2)), String.Empty, dataReader.GetString(2))
                        stockGroupList.Add(stockGroup)
                    Loop
                End If
            End Using
            Return stockGroupList
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function DeleteStockTakeData(ByVal request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.StockTakeDetails)) As Boolean Implements IStockTake(Of SpectrumCommon.StockTakeDetails).DeleteStockTakeData
        Try
            Dim deleteQuery As String
            For Each item In request.DayCloseData
                'code added by vipul for issue id 2496
                '  deleteQuery = "Delete From DayCloseStockTakeDetails where SiteCode = '" & request.SiteCode & "' and DayCloseDate = '" & request.GetDayCloseDateAsString() & "' and ArticleCode = '" & item.ArticleCode & "'"
                deleteQuery = "update DayCloseStockTakeDetails set status=0,UPDATEDAT ='" & request.SiteCode & "', UPDATEDBY='" & request.UserId & "' , UPDATEDON= getdate() where SiteCode = '" & request.SiteCode & "' and DayCloseDate = '" & request.GetDayCloseDateAsString() & "' and ArticleCode = '" & item.ArticleCode & "'"
                InsertOrUpdateRecord(deleteQuery)
            Next
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function ClearStockTakeData(ByVal request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.StockTakeDetails)) As Boolean Implements IStockTake(Of SpectrumCommon.StockTakeDetails).ClearStockTakeData
        Try
            Dim deleteQuery As String
            For Each item In request.DayCloseData
                deleteQuery = "Delete From DayCloseStockTakeDetails where SiteCode = '" & request.SiteCode & "' and DayCloseDate = '" & request.GetDayCloseDateAsString() & "' "
                InsertOrUpdateRecord(deleteQuery)
            Next
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Sub GetNewItemMasterData(ByRef request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.StockTakeDetails), Optional ByVal AllowQtyZero As Boolean = False) Implements IStockTake(Of SpectrumCommon.StockTakeDetails).GetNewItemMasterData
        Try
            If request.DayCloseData IsNot Nothing AndAlso request.DayCloseData.Count > 0 Then
                For Each stockTakeDetails In request.DayCloseData
                    stockTakeDetails.MAP = GetArticleMAP(request.SiteCode, stockTakeDetails.ArticleCode)
                    stockTakeDetails.CurrentStock = GetCurrentArticleStock(request.SiteCode, stockTakeDetails.ArticleCode)
                    stockTakeDetails.UOMData = GetArticleUOMList(New List(Of String) From {UOMTypes.BaseUnitofMeasure.ToString(), UOMTypes.DistributionUnitofMeasure.ToString()}, stockTakeDetails.ArticleCode)
                    ' added by Khusrao Adil
                    'set default quantity as zero
                    If AllowQtyZero Then
                        stockTakeDetails.EnteredQty = 0
                    End If
                    If stockTakeDetails.UOMData IsNot Nothing AndAlso stockTakeDetails.UOMData.Count > 0 Then
                        stockTakeDetails.ArticleBaseUOM = stockTakeDetails.UOMData(0).UOMCode
                        stockTakeDetails.StockTakeUOMCode = stockTakeDetails.UOMData(0).UOMCode
                    End If
                    Dim distributionUOM = stockTakeDetails.UOMData.Where(Function(a) a.UOMCode <> stockTakeDetails.ArticleBaseUOM).FirstOrDefault()
                    If distributionUOM IsNot Nothing Then
                        stockTakeDetails.Multiplier = GetUOMConversionFactor(stockTakeDetails.ArticleCode, distributionUOM.UOMCode)
                    Else
                        stockTakeDetails.Multiplier = 1
                    End If
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        Finally
            CloseConnection()
        End Try
    End Sub

End Class
