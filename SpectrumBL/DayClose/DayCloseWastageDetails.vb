Imports SpectrumCommon
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Collections
Public Class DayCloseWastageDetails
    Inherits clsDayCloseBase
    Implements IStockTake(Of WastageDetails)


    Public Function CheckIfDataExist(ByRef request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.WastageDetails)) As System.ComponentModel.BindingList(Of SpectrumCommon.WastageDetails) Implements IDayCloseScreens(Of SpectrumCommon.WastageDetails).CheckIfDataExist
        Try
            Dim query As String = "select D.ArticleCode ,D.ArticleBaseUOM ,D.WastageEntered ,D.WastageUOM,D.ReasonCode,D.MAP ," & _
                                    "D.SiteCode,d.DayCloseDate ,D.STATUS ,A.ArticleShortName ,D.WastageBUOMQty " & _
                                    " from DayCloseWastageDetails  AS D Inner Join MstArticle As A On D.ArticleCode = A.ArticleCode " & _
                                   "where D.SiteCode = '" & request.SiteCode & "' and D.DayCloseDate = '" & request.GetDayCloseDateAsString() & "' and D.status=1 Order By A.ArticleShortName"
            Dim wastageDetailsList As BindingList(Of WastageDetails)
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    wastageDetailsList = New BindingList(Of WastageDetails)
                    Do While dataReader.Read()
                        Dim wastageDetails As New WastageDetails
                        wastageDetails.ArticleCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        wastageDetails.ArticleBaseUOM = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        wastageDetails.WastageUOMCode = IIf(IsDBNull(dataReader.GetString(3)), Nothing, dataReader.GetString(3))
                        'wastageDetails.EnteredQty = IIf(IsDBNull(dataReader.GetDecimal(2)), Nothing, dataReader.GetDecimal(2))
                        wastageDetails.EnteredQty = IIf(IsDBNull(dataReader(2)), Nothing, dataReader(2))
                        wastageDetails.ReasonCode = IIf(IsDBNull(dataReader.GetString(4)), Nothing, dataReader.GetString(4))
                        wastageDetails.MAP = IIf(IsDBNull(dataReader.GetDecimal(5)), 0, dataReader.GetDecimal(5))
                        wastageDetails.SiteCode = IIf(IsDBNull(dataReader.GetString(6)), String.Empty, dataReader.GetString(6))
                        wastageDetails.DayCloseDate = IIf(IsDBNull(dataReader.GetDateTime(7)), Nothing, dataReader.GetDateTime(7))
                        wastageDetails.Status = IIf(IsDBNull(dataReader.GetBoolean(8)), False, dataReader.GetBoolean(8))
                        wastageDetails.ArticleName = IIf(IsDBNull(dataReader.GetString(9)), String.Empty, dataReader.GetString(9))
                        wastageDetails.WastageBUOMQty = IIf(IsDBNull(dataReader.GetDecimal(10)), 0, dataReader.GetDecimal(10))
                        wastageDetailsList.Add(wastageDetails)
                    Loop
                End If
            End Using
            If wastageDetailsList IsNot Nothing Then
                Dim reasonList = GetReasonDetails(DocumentType.wastage)
                For Each wasteDetails In wastageDetailsList
                    wasteDetails.UOMData = GetArticleUOMList(New List(Of String) From {UOMTypes.BaseUnitofMeasure.ToString(), UOMTypes.OrderUnitofMeasure.ToString()}, wasteDetails.ArticleCode)
                    wasteDetails.ReasonData = reasonList
                    Dim orderUOM = wasteDetails.UOMData.Where(Function(a) a.UOMCode <> wasteDetails.ArticleBaseUOM).FirstOrDefault()
                    If orderUOM IsNot Nothing Then
                        wasteDetails.Multiplier = GetUOMConversionFactor(wasteDetails.ArticleCode, orderUOM.UOMCode)
                    Else
                        wasteDetails.Multiplier = 1
                    End If
                Next
            End If
            Return wastageDetailsList
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function GetDayCloseData(ByRef request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.WastageDetails), Optional ByVal AllowQtyZero As Boolean = False) As System.ComponentModel.BindingList(Of SpectrumCommon.WastageDetails) Implements IDayCloseScreens(Of SpectrumCommon.WastageDetails).GetDayCloseData
        Try
            Dim wastageDetailsList As BindingList(Of WastageDetails) = CheckIfDataExist(request)
            If wastageDetailsList IsNot Nothing Then
                Return wastageDetailsList
            Else
                Dim query As String = "select articlecode,ArticleShortName ,BaseUnitofMeasure from MstArticle where " & request.Query & " Order By ArticleShortName"
                Using dataReader As SqlDataReader = GetReader(query)
                    If dataReader.HasRows Then
                        wastageDetailsList = New BindingList(Of WastageDetails)
                        Do While dataReader.Read()
                            Dim watageDetails As New WastageDetails
                            watageDetails.ArticleCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                            watageDetails.ArticleName = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                            watageDetails.ArticleBaseUOM = IIf(IsDBNull(dataReader.GetString(2)), String.Empty, dataReader.GetString(2))
                            'watageDetails.EnteredQty = Nothing
                            'added by Khusrao Adil
                            ' set default quantity as zero
                            If AllowQtyZero Then
                                watageDetails.EnteredQty = 0
                            End If
                            wastageDetailsList.Add(watageDetails)
                        Loop
                    End If
                End Using
                If wastageDetailsList IsNot Nothing Then
                    Dim reasonList = GetReasonDetails(DocumentType.wastage)
                    For Each wastageDetails In wastageDetailsList
                        wastageDetails.MAP = GetArticleMAP(request.SiteCode, wastageDetails.ArticleCode)
                        wastageDetails.UOMData = GetArticleUOMList(New List(Of String) From {UOMTypes.BaseUnitofMeasure.ToString(), UOMTypes.OrderUnitofMeasure.ToString()}, wastageDetails.ArticleCode)
                        wastageDetails.ReasonData = reasonList
                        If wastageDetails.UOMData IsNot Nothing AndAlso wastageDetails.UOMData.Count > 0 Then
                            wastageDetails.WastageUOMCode = wastageDetails.UOMData(0).UOMCode
                        End If
                        If reasonList IsNot Nothing AndAlso reasonList.Count > 0 Then
                            wastageDetails.ReasonCode = reasonList(0).ReasonCode
                        End If
                        Dim orderUOM = wastageDetails.UOMData.Where(Function(a) a.UOMCode <> wastageDetails.ArticleBaseUOM).FirstOrDefault()
                        If orderUOM IsNot Nothing Then
                            wastageDetails.Multiplier = GetUOMConversionFactor(wastageDetails.ArticleCode, orderUOM.UOMCode)
                        Else
                            wastageDetails.Multiplier = 1
                        End If
                    Next
                End If
                Return wastageDetailsList
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function SaveDayCloseData(ByRef request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.WastageDetails)) As Boolean Implements IDayCloseScreens(Of SpectrumCommon.WastageDetails).SaveDayCloseData
        Try
            Dim result As Boolean = False
            If request IsNot Nothing AndAlso request.DayCloseData.Count > 0 Then
                For Each wastageDetails In request.DayCloseData
                    If CheckIfUpdateData(request, wastageDetails.ArticleCode) Then
                        Dim updateQuery As String = "Update  DayCloseWastageDetails SET status=1, WastageUOM = '" & wastageDetails.WastageUOMCode & "'," & _
                        "WastageEntered=" & If(wastageDetails.EnteredQty Is Nothing, "NULL", wastageDetails.EnteredQty) & ", WastageBUOMQty=" & wastageDetails.WastageBUOMQty & ", ReasonCode='" & wastageDetails.ReasonCode & "'," & _
                        "UPDATEDAT ='" & request.SiteCode & "', UPDATEDBY='" & request.UserId & "' , UPDATEDON= getdate() " & _
                        "where SiteCode = '" & request.SiteCode & "' And DayCloseDate = '" & request.GetDayCloseDateAsString() & "' and ArticleCode = '" & wastageDetails.ArticleCode & "' "
                        InsertOrUpdateRecord(updateQuery)
                    Else
                        Dim insertQuery As String = "Insert into DayCloseWastageDetails (ArticleCode , ArticleBaseUOM ,WastageEntered, " & _
                        " WastageUOM,WastageBUOM,WastageBUOMQty, ReasonCode, MAP, SiteCode, DayCloseDate, CREATEDAT, CREATEDBY" & _
                        ",CREATEDON , UPDATEDAT , UPDATEDBY , UPDATEDON,Status ) VAlues ('" & wastageDetails.ArticleCode & "','" & wastageDetails.ArticleBaseUOM & "' " & _
                        "," & If(wastageDetails.EnteredQty Is Nothing, "NULL", wastageDetails.EnteredQty) & ",'" & wastageDetails.WastageUOMCode & "','" & wastageDetails.ArticleBaseUOM & "', " & wastageDetails.WastageBUOMQty & ",'" & wastageDetails.ReasonCode & "'" & _
                        "," & wastageDetails.MAP & ",'" & request.SiteCode & "','" & request.GetDayCloseDateAsString() & "','" & request.SiteCode & "' " & _
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

    Private Function CheckIfUpdateData(ByRef request As DayCloseDataRequestModel(Of WastageDetails), ByVal articleCode As String) As Boolean
        Try
            Dim isUpdate As Boolean = False
            Dim query As String = "select D.ArticleCode " & _
                                  " from DayCloseWastageDetails  AS D " & _
                                 "where D.SiteCode = '" & request.SiteCode & "' and D.DayCloseDate = '" & request.GetDayCloseDateAsString() & "' and D.ArticleCode = '" & articleCode & "'"
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

    Public Function DeleteStockTakeData(ByVal request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.WastageDetails)) As Boolean Implements IStockTake(Of SpectrumCommon.WastageDetails).DeleteStockTakeData
        Try
            Dim deleteQuery As String
            For Each item In request.DayCloseData
                'code added by vipul for issue id 2496
                ' deleteQuery = "Delete From DayCloseWastageDetails where SiteCode = '" & request.SiteCode & "' and DayCloseDate = '" & request.GetDayCloseDateAsString() & "' and ArticleCode = '" & item.ArticleCode & "'"
                deleteQuery = "update DayCloseWastageDetails set status=0,UPDATEDAT ='" & request.SiteCode & "',UPDATEDBY='" & request.UserId & "' , UPDATEDON= getdate() where SiteCode = '" & request.SiteCode & "' and DayCloseDate = '" & request.GetDayCloseDateAsString() & "' and ArticleCode = '" & item.ArticleCode & "'"
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

    Public Function ClearStockTakeData(ByVal request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.StockTakeDetails)) As Boolean Implements IStockTake(Of SpectrumCommon.WastageDetails).ClearStockTakeData
        Try
            Dim deleteQuery As String
            For Each item In request.DayCloseData
                deleteQuery = "Delete From DayCloseWastageDetails where SiteCode = '" & request.SiteCode & "' and DayCloseDate = '" & request.GetDayCloseDateAsString() & "' "
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

    Public Sub GetNewItemMasterData(ByRef request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.WastageDetails), Optional ByVal AllowQtyZero As Boolean = False) Implements IStockTake(Of SpectrumCommon.WastageDetails).GetNewItemMasterData
        Try
            If request.DayCloseData IsNot Nothing AndAlso request.DayCloseData.Count > 0 Then
                Dim reasonList = GetReasonDetails(DocumentType.wastage)
                For Each wastageDetails In request.DayCloseData
                    wastageDetails.MAP = GetArticleMAP(request.SiteCode, wastageDetails.ArticleCode)
                    wastageDetails.UOMData = GetArticleUOMList(New List(Of String) From {UOMTypes.BaseUnitofMeasure.ToString(), UOMTypes.OrderUnitofMeasure.ToString()}, wastageDetails.ArticleCode)
                    wastageDetails.ReasonData = reasonList
                    If wastageDetails.UOMData IsNot Nothing AndAlso wastageDetails.UOMData.Count > 0 Then
                        wastageDetails.ArticleBaseUOM = wastageDetails.UOMData(0).UOMCode
                        wastageDetails.WastageUOMCode = wastageDetails.UOMData(0).UOMCode
                    End If
                    If reasonList IsNot Nothing AndAlso reasonList.Count > 0 Then
                        wastageDetails.ReasonCode = reasonList(0).ReasonCode
                    End If
                    Dim orderUOM = wastageDetails.UOMData.Where(Function(a) a.UOMCode <> wastageDetails.ArticleBaseUOM).FirstOrDefault()
                    If orderUOM IsNot Nothing Then
                        wastageDetails.Multiplier = GetUOMConversionFactor(wastageDetails.ArticleCode, orderUOM.UOMCode)
                    Else
                        wastageDetails.Multiplier = 1
                    End If
                    ' ' added by Khusrao Adil
                    'set default quantity as zero
                    If AllowQtyZero Then
                        wastageDetails.EnteredQty = 0
                    End If
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        Finally
            CloseConnection()
        End Try
    End Sub

    Public Function GetStockGroups(ByVal groupType As SpectrumCommon.ArticleGroupType) As System.ComponentModel.BindingList(Of SpectrumCommon.ArticleGroupDetails) Implements IStockTake(Of SpectrumCommon.WastageDetails).GetStockGroups
        Try

        Catch ex As Exception

        End Try
    End Function
End Class
