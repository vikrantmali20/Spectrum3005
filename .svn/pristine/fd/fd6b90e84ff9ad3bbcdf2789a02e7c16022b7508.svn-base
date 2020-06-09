Imports SpectrumCommon
Imports System.Data.SqlClient
Imports System.ComponentModel

Public Class clsDayCloseBase

    Private _ServerDate As DateTime?
    Private Property ServerDate As DateTime?
        Get
            Return _ServerDate
        End Get
        Set(ByVal value As DateTime?)
            _ServerDate = value
        End Set
    End Property


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="articleCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetRecipeData(ByVal articleCode) As BindingList(Of RecipeDetails)
        Try
            Dim query As String = "select RecipeCode , RecipeName,Qty   from Recipe where ArticleCode = '" & articleCode & "'"
            Dim recipeList As New BindingList(Of RecipeDetails)
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim recipe As New RecipeDetails
                        recipe.RecipeCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        recipe.RecipeName = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        recipe.Quantity = IIf(IsDBNull(dataReader.GetDecimal(2)), String.Empty, dataReader.GetDecimal(2))
                        recipeList.Add(recipe)
                    Loop
                End If
            End Using
            Return recipeList
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
    ''' <param name="docType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetReasonDetails(ByVal docType As DocumentType) As BindingList(Of ReasonDetails)
        Try
            Dim query As String = "Select ReasonCode , ReasonName from Reasons where DocType ='" & docType.ToString() & "' and Status=1 order by TrnSequenceName"
            Dim reasonList As New BindingList(Of ReasonDetails)
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim reason As New ReasonDetails
                        reason.ReasonCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        reason.ReasonName = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        reasonList.Add(reason)
                    Loop
                End If
            End Using
            Return reasonList
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Protected Function GetArticleUOMList(ByRef UomColumnNames As List(Of String), ByVal articleCode As String) As BindingList(Of ArticleUOMDetails)
        Try
            Dim query As String = "select " & UomColumnNames(0) & ", " & UomColumnNames(1) & " from MstArticle where ArticleCode ='" & articleCode & "'"
            Dim uomList As New BindingList(Of ArticleUOMDetails)
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim articleBaseUOM As New ArticleUOMDetails

                        articleBaseUOM.UOMCode = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        articleBaseUOM.UOMName = articleBaseUOM.UOMCode
                        uomList.Add(articleBaseUOM)
                        Dim uom As String = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        If uom.ToUpper() <> articleBaseUOM.UOMCode.ToUpper() Then
                            Dim articleOrderUOM As New ArticleUOMDetails
                            articleOrderUOM.UOMCode = uom
                            articleOrderUOM.UOMName = articleOrderUOM.UOMCode
                            uomList.Add(articleOrderUOM)
                        End If
                    Loop
                End If
            End Using
            Return uomList
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function

    Protected Function GetArticleMAP(ByVal siteCode As String, ByVal articleCode As String) As Double
        Try
            Dim map As Decimal = 0
            Dim query As String = "select MAP from PurchaseInfoRecord where  SiteCode ='" & siteCode & "' and ArticleCode = '" & articleCode & "' and IsDefaultSupplier = 1 "
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    dataReader.Read()
                    map = IIf(IsDBNull(dataReader.GetDecimal(0)), 0, dataReader.GetDecimal(0))
                End If
            End Using
            Return map
        Catch ex As Exception
            LogException(ex)
            Return 0
        Finally
            CloseConnection()
        End Try
    End Function

    Protected Function GetUOMConversionFactor(ByVal articleCode As String, ByVal uom As String) As Decimal
        Try
            Dim divisor As Decimal = 0
            Dim query As String = "select Divisor  from ArticleUOM where ArticleCode = '" & articleCode & "' and UOMCode = '" & uom & "' "
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    dataReader.Read()
                    divisor = IIf(IsDBNull(dataReader.GetDecimal(0)), 0, dataReader.GetDecimal(0))
                End If
            End Using
            Return divisor
        Catch ex As Exception
            LogException(ex)
            Return 0
        Finally
            CloseConnection()
        End Try
    End Function

    Protected Function GetReader(ByRef query As String) As SqlDataReader
        Try
            OpenConnection()
            Dim sqlCmd As New SqlCommand(query, SpectrumCon)
            Return sqlCmd.ExecuteReader()
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Public Function GetFilledTable(ByVal strQuery As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    Protected Function GetServerDate() As DateTime
        Try
            Dim query As String = "select getdate() As ServerDate"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        ServerDate = IIf(IsDBNull(dataReader.GetDateTime(0)), Nothing, dataReader.GetDateTime(0))
                    Loop
                End If
            End Using
            Return ServerDate
        Catch ex As Exception
            LogException(ex)
        Finally
            CloseConnection()
        End Try
    End Function

    Protected Function InsertOrUpdateRecord(ByVal Query As String) As Boolean
        Try
            OpenConnection()
            Dim cmd As New SqlCommand
            cmd.CommandText = Query
            cmd.Connection = SpectrumCon()
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
    Protected Function InsertOrUpdateRecord(ByVal cmd As SqlCommand) As Boolean
        Try
            OpenConnection()
            cmd.Connection = SpectrumCon()
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function
End Class
