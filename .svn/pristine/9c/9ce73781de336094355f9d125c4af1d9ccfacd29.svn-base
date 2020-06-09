Imports SpectrumCommon
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Collections
Public Class DayCloseRawMaterialDetails
    Inherits clsDayCloseBase
    Implements IRawMaterial(Of RawMaterialDetails)

    Public Function GetDayCloseData(ByRef request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.RawMaterialDetails), Optional ByVal AllowQtyZero As Boolean = False) As System.ComponentModel.BindingList(Of SpectrumCommon.RawMaterialDetails) Implements IDayCloseScreens(Of SpectrumCommon.RawMaterialDetails).GetDayCloseData
        Try
            Dim rawMaterialList As BindingList(Of RawMaterialDetails)
            Dim query As String = "select articlecode,ArticleShortName ,BaseUnitofMeasure from MstArticle where " & request.Query & " Order By ArticleShortName"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    If rawMaterialList Is Nothing Then
                        rawMaterialList = New BindingList(Of RawMaterialDetails)
                    End If
                End If
                Do While dataReader.Read()
                    Dim rawMaterial As New RawMaterialDetails
                    rawMaterial.ArticleCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                    rawMaterial.ArticleName = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                    rawMaterial.ArticleBaseUOM = IIf(IsDBNull(dataReader.GetString(2)), String.Empty, dataReader.GetString(2))
                    'rawMaterial.GroupCode = IIf(IsDBNull(dataReader.GetString(2)), String.Empty, dataReader.GetString(2))
                    If AllowQtyZero Then
                        rawMaterial.EnteredQty = 0
                    End If
                    Dim groupId = request.Query.ToString().Trim
                    groupId = groupId.Substring(groupId.Length - 1)
                    rawMaterial.GroupCode = groupId
                    If Not rawMaterialList.Any(Function(prd) prd.ArticleCode = rawMaterial.ArticleCode) Then
                        rawMaterialList.Add(rawMaterial)
                    End If
                Loop
            End Using
            If rawMaterialList IsNot Nothing Then
                For Each rawMaterialDetails In rawMaterialList
                    rawMaterialDetails.MAP = GetArticleMAP(request.SiteCode, rawMaterialDetails.ArticleCode)
                    rawMaterialDetails.CurrentStock = GetCurrentArticleStock(request.SiteCode, rawMaterialDetails.ArticleCode)
                    rawMaterialDetails.UOMData = GetArticleUOMList(New List(Of String) From {UOMTypes.BaseUnitofMeasure.ToString(), UOMTypes.DistributionUnitofMeasure.ToString()}, rawMaterialDetails.ArticleCode)
                    If rawMaterialDetails.UOMData IsNot Nothing AndAlso rawMaterialDetails.UOMData.Count > 0 Then
                        rawMaterialDetails.StockTakeUOMCode = rawMaterialDetails.UOMData(0).UOMCode
                    End If
                    Dim distributionUOM = rawMaterialDetails.UOMData.Where(Function(a) a.UOMCode <> rawMaterialDetails.ArticleBaseUOM).FirstOrDefault()
                    If distributionUOM IsNot Nothing Then
                        rawMaterialDetails.Multiplier = GetUOMConversionFactor(rawMaterialDetails.ArticleCode, distributionUOM.UOMCode)
                    Else
                        rawMaterialDetails.Multiplier = 1
                    End If
                Next
            End If
            Return rawMaterialList
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try

    End Function

    Public Function CheckIfDataExist(ByRef request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.RawMaterialDetails)) As System.ComponentModel.BindingList(Of SpectrumCommon.RawMaterialDetails) Implements IDayCloseScreens(Of SpectrumCommon.RawMaterialDetails).CheckIfDataExist
        Try
            Dim groupId = request.Query.ToString().Trim
            groupId = groupId.Substring(groupId.Length - 1)
            Dim query As String = "select  D.ArticleCode ,D.ArticleBaseUOM ,D.Quantity ,D.StockTakeUOM,D.MAP ," & _
                                   "D.SiteCode,d.DayCloseDate ,D.STATUS ,A.ArticleShortName ,D.BUOMQuantity,D.CurrentStock,D.GroupCode,Mag.SubGroup " & _
                                   " from DayCloseStockTakeDetails  AS D Inner Join MstArticle As A On D.ArticleCode = A.ArticleCode " & _
                                   " left outer join MSTArticleGroupDtl MAG on D.ArticleCode = MAG.ArticleCode and D.GroupCode=MAg.GroupID where D.SiteCode = '" & request.SiteCode & "' and D.DayCloseDate = '" & request.GetDayCloseDateAsString() & "' and D.status=1 and MAg.GroupID='" & groupId & "' Order By MAG.SubGroup "

            Dim rawMaterialDetailsList As BindingList(Of RawMaterialDetails)
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    rawMaterialDetailsList = New BindingList(Of RawMaterialDetails)
                    Do While dataReader.Read()
                        Dim rawMaterialDetails As New RawMaterialDetails
                        rawMaterialDetails.ArticleCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        rawMaterialDetails.ArticleBaseUOM = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        rawMaterialDetails.StockTakeUOMCode = IIf(IsDBNull(dataReader.GetString(3)), Nothing, dataReader.GetString(3))
                        'stockTakeDetails.EnteredQty = IIf(IsDBNull(dataReader.GetDecimal(2)), Nothing, dataReader.GetDecimal(2))
                        rawMaterialDetails.EnteredQty = IIf(IsDBNull(dataReader(2)), Nothing, dataReader(2))
                        rawMaterialDetails.MAP = IIf(IsDBNull(dataReader.GetDecimal(4)), 0, dataReader.GetDecimal(4))
                        rawMaterialDetails.SiteCode = IIf(IsDBNull(dataReader.GetString(5)), String.Empty, dataReader.GetString(5))
                        rawMaterialDetails.DayCloseDate = IIf(IsDBNull(dataReader.GetDateTime(6)), Nothing, dataReader.GetDateTime(6))
                        rawMaterialDetails.Status = IIf(IsDBNull(dataReader.GetBoolean(7)), False, dataReader.GetBoolean(7))
                        rawMaterialDetails.ArticleName = IIf(IsDBNull(dataReader.GetString(8)), String.Empty, dataReader.GetString(8))
                        rawMaterialDetails.BUOMQty = IIf(IsDBNull(dataReader.GetDecimal(9)), 0, dataReader.GetDecimal(9))
                        rawMaterialDetails.CurrentStock = IIf(IsDBNull(dataReader.GetDecimal(10)), 0, dataReader.GetDecimal(10))
                        rawMaterialDetails.GroupCode = IIf(IsDBNull(dataReader.GetString(11)), String.Empty, dataReader.GetString(11))
                        rawMaterialDetails.SubGroupId = IIf(IsDBNull(dataReader(12)), 0, dataReader(12))
                        rawMaterialDetailsList.Add(rawMaterialDetails)
                    Loop
                End If
            End Using

            If rawMaterialDetailsList IsNot Nothing Then
                For Each rawMaterialDetails In rawMaterialDetailsList
                    rawMaterialDetails.MAP = GetArticleMAP(request.SiteCode, rawMaterialDetails.ArticleCode)
                    rawMaterialDetails.CurrentStock = GetCurrentArticleStock(request.SiteCode, rawMaterialDetails.ArticleCode)
                    rawMaterialDetails.UOMData = GetArticleUOMList(New List(Of String) From {UOMTypes.BaseUnitofMeasure.ToString(), UOMTypes.DistributionUnitofMeasure.ToString()}, rawMaterialDetails.ArticleCode)
                    If rawMaterialDetails.UOMData IsNot Nothing AndAlso rawMaterialDetails.UOMData.Count > 0 Then
                        rawMaterialDetails.StockTakeUOMCode = rawMaterialDetails.UOMData(0).UOMCode
                    End If
                    Dim distributionUOM = rawMaterialDetails.UOMData.Where(Function(a) a.UOMCode <> rawMaterialDetails.ArticleBaseUOM).FirstOrDefault()
                    If distributionUOM IsNot Nothing Then
                        rawMaterialDetails.Multiplier = GetUOMConversionFactor(rawMaterialDetails.ArticleCode, distributionUOM.UOMCode)
                    Else
                        rawMaterialDetails.Multiplier = 1
                    End If
                Next
            End If

            Return rawMaterialDetailsList
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function
    Public Function SaveDayCloseData(ByRef request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.RawMaterialDetails)) As Boolean Implements IDayCloseScreens(Of SpectrumCommon.RawMaterialDetails).SaveDayCloseData

    End Function

    Public Function ClearStockTakeData(request As DayCloseDataRequestModel(Of RawMaterialDetails)) As Boolean Implements IRawMaterial(Of RawMaterialDetails).ClearStockTakeData

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
End Class
