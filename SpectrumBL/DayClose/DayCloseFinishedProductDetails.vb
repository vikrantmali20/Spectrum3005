Imports SpectrumCommon
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Collections
Public Class DayCloseFinishedProductDetails
    Inherits clsDayCloseBase
    Implements IDayCloseScreens(Of FinishedProductDetails)

    Public Function CheckIfDataExist(ByRef request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.FinishedProductDetails)) As System.ComponentModel.BindingList(Of SpectrumCommon.FinishedProductDetails) Implements IDayCloseScreens(Of SpectrumCommon.FinishedProductDetails).CheckIfDataExist
        Try
            Dim query As String = "select D.ArticleCode ,D.ArticleBaseUOM ,D.MAP, D.ProducedQty ," & _
                                    "D.SiteCode,d.DayCloseDate ,D.STATUS ,A.ArticleShortName " & _
                                    " from DayCloseFinishedProductDetails  AS D Inner Join MstArticle As A On D.ArticleCode = A.ArticleCode " & _
                                   "where D.SiteCode = '" & request.SiteCode & "' and D.DayCloseDate = '" & request.GetDayCloseDateAsString() & "' and D.status=1 Order By A.ArticleShortName"
            Dim finishedProductList As BindingList(Of FinishedProductDetails)
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    finishedProductList = New BindingList(Of FinishedProductDetails)
                    Do While dataReader.Read()
                        Dim fnishedProduct As New FinishedProductDetails
                        fnishedProduct.ArticleCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))                       
                        fnishedProduct.ArticleBaseUOM = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        fnishedProduct.MAP = IIf(IsDBNull(dataReader.GetDecimal(2)), 0, dataReader.GetDecimal(2))
                        fnishedProduct.EnteredQty = IIf(IsDBNull(dataReader.GetDecimal(3)), Nothing, dataReader.GetDecimal(3))
                        fnishedProduct.SiteCode = IIf(IsDBNull(dataReader.GetString(4)), String.Empty, dataReader.GetString(4))
                        fnishedProduct.DayCloseDate = IIf(IsDBNull(dataReader.GetDateTime(5)), Nothing, dataReader.GetDateTime(5))
                        fnishedProduct.Status = IIf(IsDBNull(dataReader.GetBoolean(6)), False, dataReader.GetBoolean(6))
                        fnishedProduct.ArticleName = IIf(IsDBNull(dataReader.GetString(7)), String.Empty, dataReader.GetString(7))
                        finishedProductList.Add(fnishedProduct)
                    Loop
                End If
            End Using            
            Return finishedProductList
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function GetDayCloseData(ByRef request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.FinishedProductDetails), Optional ByVal AllowQtyZero As Boolean = False) As System.ComponentModel.BindingList(Of SpectrumCommon.FinishedProductDetails) Implements IDayCloseScreens(Of SpectrumCommon.FinishedProductDetails).GetDayCloseData
        Try
            Dim finishedProductList As BindingList(Of FinishedProductDetails) = CheckIfDataExist(request)
            'If finishedProductList IsNot Nothing Then
            '    Return finishedProductList
            'Else
            Dim query As String = "select articlecode,ArticleShortName ,BaseUnitofMeasure  from MstArticle where " & request.Query & " Order By ArticleShortName"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    If finishedProductList Is Nothing Then
                        finishedProductList = New BindingList(Of FinishedProductDetails)
                    End If
                    Do While dataReader.Read()
                        Dim finishedProduct As New FinishedProductDetails
                        finishedProduct.ArticleCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        finishedProduct.ArticleName = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        finishedProduct.ArticleBaseUOM = IIf(IsDBNull(dataReader.GetString(2)), String.Empty, dataReader.GetString(2))
                        'added by Khusrao Adil
                        ' set default quantity as zero
                        If AllowQtyZero Then
                            finishedProduct.EnteredQty = 0
                        End If
                        If Not finishedProductList.Any(Function(prd) prd.ArticleCode = finishedProduct.ArticleCode) Then
                            finishedProductList.Add(finishedProduct)
                        End If
                    Loop
                End If
            End Using
            If finishedProductList IsNot Nothing Then
                For Each finishedProduct In finishedProductList
                    finishedProduct.MAP = GetArticleMAP(request.SiteCode, finishedProduct.ArticleCode)
                Next
            End If
            Return finishedProductList
            'End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function SaveDayCloseData(ByRef request As SpectrumCommon.DayCloseDataRequestModel(Of SpectrumCommon.FinishedProductDetails)) As Boolean Implements IDayCloseScreens(Of SpectrumCommon.FinishedProductDetails).SaveDayCloseData
        Try
            Dim result As Boolean = False
            If request IsNot Nothing AndAlso request.DayCloseData.Count > 0 Then
                'If CheckIfUpdateData(request) Then
                For Each finishedProduct In request.DayCloseData
                    If CheckIfUpdateData(request, finishedProduct.ArticleCode) Then
                        Dim updateQuery As String = "Update  DayCloseFinishedProductDetails SET  " & _
                        "ProducedQty=" & finishedProduct.EnteredQty & "," & _
                        "UPDATEDAT ='" & request.SiteCode & "', UPDATEDBY='" & request.UserId & "' , UPDATEDON= getdate() " & _
                        "where SiteCode = '" & request.SiteCode & "' And DayCloseDate = '" & request.GetDayCloseDateAsString() & "' and ArticleCode = '" & finishedProduct.ArticleCode & "' "
                        InsertOrUpdateRecord(updateQuery)
                    Else
                        Dim insertQuery As String = "Insert into DayCloseFinishedProductDetails (ArticleCode , ArticleBaseUOM , " & _
                        "MAP, ProducedQty, SiteCode, DayCloseDate, CREATEDAT, CREATEDBY" & _
                        ",CREATEDON , UPDATEDAT , UPDATEDBY , UPDATEDON,Status ) VAlues ('" & finishedProduct.ArticleCode & "' " & _
                        ",'" & finishedProduct.ArticleBaseUOM & "'," & finishedProduct.MAP & "," & finishedProduct.EnteredQty & _
                        ",'" & request.SiteCode & "','" & request.GetDayCloseDateAsString() & "','" & request.SiteCode & "' " & _
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

    Private Function CheckIfUpdateData(ByRef request As DayCloseDataRequestModel(Of FinishedProductDetails), ByVal articleCode As String) As Boolean
        Try
            Dim isUpdate As Boolean = False
            Dim query As String = "select D.ArticleCode " & _
                                  " from DayCloseFinishedProductDetails  AS D " & _
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

    Function ResetGLNoRangeObjects(obj As String ,SiteCode As String ) As Boolean
         Try
            Dim DocNo As String = ""
            Dim dt As New DataTable
            Dim daDoc As New SqlDataAdapter("SELECT OBJECTID,OBJECTTYPEID FROM GLNORANGEOBJECTSM WHERE OBJECTNAME='" & obj & "'", ConString)
            daDoc.Fill(dt)
            If dt.Rows.Count > 0 Then
                Dim objTypeId, objId As String
                objTypeId = dt.Rows(0)("OBJECTTYPEID")
                objId = dt.Rows(0)("OBJECTID")
                Dim UpdateQuery = " UPDATE GLNORANGEOBJECTS  SET CurrentNo = 1 WHERE OBJECTTYPEID='" & objTypeId & "' AND OBJECTID='" & objId & "' AND SiteCode='" & siteCode & "'"
                InsertOrUpdateRecord(UpdateQuery)
            End If
            Return DocNo
        Catch ex As Exception
            LogException(ex)
            Return ""
        End Try
    End Function

End Class
