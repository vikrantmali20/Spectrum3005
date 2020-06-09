Imports SpectrumCommon
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Collections

Public Class DayCloseSubProductDetails
    Inherits clsDayCloseBase
    Implements IDayCloseScreens(Of SubProductDetails)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="request"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDayCloseData(ByRef request As DayCloseDataRequestModel(Of SubProductDetails), Optional ByVal AllowQtyZero As Boolean = False) As BindingList(Of SubProductDetails) _
        Implements IDayCloseScreens(Of SpectrumCommon.SubProductDetails).GetDayCloseData
        Try
            Dim subProductList As BindingList(Of SubProductDetails) = CheckIfDataExist(request)
            'If subProductList IsNot Nothing Then
            '    Return subProductList
            'Else
            Dim query As String = "select articlecode,ArticleShortName ,BaseUnitofMeasure from MstArticle where " & request.Query & " Order By ArticleShortName"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    If subProductList Is Nothing Then
                        subProductList = New BindingList(Of SubProductDetails)
                    End If
                    Do While dataReader.Read()
                        Dim subProduct As New SubProductDetails
                        subProduct.ArticleCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        subProduct.ArticleName = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        subProduct.ArticleBaseUOM = IIf(IsDBNull(dataReader.GetString(2)), String.Empty, dataReader.GetString(2))
                        'added by Khusrao Adil
                        ' set default quantity as zero
                        If AllowQtyZero Then
                            subProduct.EnteredQty = 0
                        End If
                        If Not subProductList.Any(Function(prd) prd.ArticleCode = subProduct.ArticleCode) Then
                            subProductList.Add(subProduct)
                        End If

                    Loop
                End If
            End Using
            If subProductList IsNot Nothing Then
                For Each subProduct In subProductList
                    subProduct.RecipeData = GetRecipeData(subProduct.ArticleCode)
                    subProduct.MAP = GetArticleMAP(request.SiteCode, subProduct.ArticleCode)
                    If subProduct.RecipeData IsNot Nothing AndAlso subProduct.RecipeData.Count > 0 Then
                        subProduct.RecipeCode = subProduct.RecipeData(0).RecipeCode
                    End If
                Next
            End If
            Return subProductList
            'End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="request"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckIfDataExist(ByRef request As DayCloseDataRequestModel(Of SubProductDetails)) As BindingList(Of SubProductDetails) _
         Implements IDayCloseScreens(Of SpectrumCommon.SubProductDetails).CheckIfDataExist
        Try
            Dim query As String = "select D.ArticleCode ,D.RecipeMasterCode ,D.ArticleBaseUOM ,D.MAP,D.BatchCount,D.EnteredQty ,D.CalculatedQty ," & _
                                    "D.SiteCode,d.DayCloseDate ,D.STATUS ,A.ArticleShortName,D.EnteredQtyBUOM,D.CalculatedQtyBUOM " & _
                                    " from DayCloseSubProductDetails  AS D Inner Join MstArticle As A On D.ArticleCode = A.ArticleCode " & _
                                   "where D.SiteCode = '" & request.SiteCode & "' and D.DayCloseDate = '" & request.GetDayCloseDateAsString() & "' and D.status=1 Order By A.ArticleShortName"
            Dim subProductList As BindingList(Of SubProductDetails)
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    subProductList = New BindingList(Of SubProductDetails)
                    Do While dataReader.Read()
                        Dim subProduct As New SubProductDetails
                        subProduct.ArticleCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        'subProduct.RecipeData = GetRecipeData(subProduct.ArticleCode)
                        subProduct.RecipeCode = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        subProduct.ArticleBaseUOM = IIf(IsDBNull(dataReader.GetString(2)), String.Empty, dataReader.GetString(2))
                        subProduct.MAP = IIf(IsDBNull(dataReader.GetDecimal(3)), 0, dataReader.GetDecimal(3))
                        subProduct.BatchCount = IIf(IsDBNull(dataReader.GetDouble(4)), Nothing, dataReader.GetDouble(4))
                        subProduct.EnteredQty = IIf(IsDBNull(dataReader.GetDecimal(5)), Nothing, dataReader.GetDecimal(5))
                        subProduct.CalculatedQty = IIf(IsDBNull(dataReader.GetDecimal(6)), 0, dataReader.GetDecimal(6))
                        subProduct.SiteCode = IIf(IsDBNull(dataReader.GetString(7)), String.Empty, dataReader.GetString(7))
                        subProduct.DayCloseDate = IIf(IsDBNull(dataReader.GetDateTime(8)), Nothing, dataReader.GetDateTime(8))
                        subProduct.Status = IIf(IsDBNull(dataReader.GetBoolean(9)), False, dataReader.GetBoolean(9))
                        subProduct.ArticleName = IIf(IsDBNull(dataReader.GetString(10)), String.Empty, dataReader.GetString(10))
                        subProduct.EnteredQtyBUOM = IIf(IsDBNull(dataReader.GetDecimal(11)), 0, dataReader.GetDecimal(11))
                        subProduct.CalculatedQtyBUOM = IIf(IsDBNull(dataReader.GetDecimal(12)), 0, dataReader.GetDecimal(12))
                        subProductList.Add(subProduct)
                    Loop
                End If
            End Using
            If subProductList IsNot Nothing Then
                For Each subProduct In subProductList
                    subProduct.RecipeData = GetRecipeData(subProduct.ArticleCode)
                Next
            End If
            Return subProductList
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function SaveDayCloseData(ByRef request As DayCloseDataRequestModel(Of SubProductDetails)) As Boolean _
        Implements IDayCloseScreens(Of SpectrumCommon.SubProductDetails).SaveDayCloseData
        Try
            Dim result As Boolean = False
            If request IsNot Nothing AndAlso request.DayCloseData.Count > 0 Then              
                For Each subProduct In request.DayCloseData
                    If CheckIfUpdateData(request, subProduct.ArticleCode) Then
                        Dim updateQuery As String = "Update  DayCloseSubProductDetails SET  RecipeMasterCode = '" & subProduct.RecipeCode & "'," & _
                        "BatchCount = " & subProduct.BatchCount & ", EnteredQty=" & subProduct.EnteredQty & ", CalculatedQty=" & subProduct.CalculatedQty & "," & _
                        "EnteredQtyBUOM = " & subProduct.EnteredQty & ",CalculatedQtyBUOM=" & subProduct.CalculatedQty & ",UPDATEDAT ='" & request.SiteCode & "', UPDATEDBY='" & request.UserId & "' , UPDATEDON= getdate() " & _
                        "where SiteCode = '" & request.SiteCode & "' And DayCloseDate = '" & request.GetDayCloseDateAsString() & "' and ArticleCode = '" & subProduct.ArticleCode & "' "
                        InsertOrUpdateRecord(updateQuery)
                    Else
                        Dim insertQuery As String = "Insert into DayCloseSubProductDetails (ArticleCode , RecipeMasterCode , ArticleBaseUOM , " & _
                        "MAP, BatchCount, EnteredQty, CalculatedQty,EnteredQtyBUOM,CalculatedQtyBUOM, SiteCode, DayCloseDate, CREATEDAT, CREATEDBY" & _
                        ",CREATEDON , UPDATEDAT , UPDATEDBY , UPDATEDON,Status ) VAlues ('" & subProduct.ArticleCode & "','" & subProduct.RecipeCode & "' " & _
                        ",'" & subProduct.ArticleBaseUOM & "'," & subProduct.MAP & "," & subProduct.BatchCount & "," & subProduct.EnteredQty & _
                        "," & subProduct.CalculatedQty & "," & subProduct.EnteredQty & "," & subProduct.CalculatedQty & ",'" & request.SiteCode & "','" & request.GetDayCloseDateAsString() & "','" & request.SiteCode & "' " & _
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
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Private Function CheckIfUpdateData(ByRef request As DayCloseDataRequestModel(Of SubProductDetails), ByVal articleCode As String) As Boolean
        Try
            Dim isUpdate As Boolean = False
            Dim query As String = "select D.ArticleCode " & _
                                  " from DayCloseSubProductDetails  AS D " & _
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
End Class
