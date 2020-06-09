Imports SpectrumCommon
Imports System.Collections
Imports System.Data.SqlClient

Public Class clsDayClose
    Inherits clsCommon
    Public Sub New()

    End Sub

    Public Function GetDayCloseScreenConfig(ByVal moduleName As Modules, ByVal screenName As DayCloseScreens) As IList(Of DayCloseScreenConfig)
        Try
            Dim query As String
            If screenName = DayCloseScreens.None Then
                query = "select Module , ScreenName , Sequence , STATUS , Script from DayCloseScreenConfig where Module = '" & moduleName.ToString() & "' and STATUS = 1"
            ElseIf screenName = DayCloseScreens.StockTakedetails Then
                query = "select Module , ScreenName , Sequence , STATUS , Script from DayCloseScreenConfig where Module = '" & moduleName.ToString() & "'and ScreenName in ('WastageDetails','StockTakedetails') "
            Else
                query = "select Module , ScreenName , Sequence , STATUS , Script from DayCloseScreenConfig where Module = '" & moduleName.ToString() & "' And ScreenName = '" & screenName.ToString() & "' and STATUS = 1"
            End If
            Dim screenList As New List(Of DayCloseScreenConfig)
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim screenConfig As New DayCloseScreenConfig
                        screenConfig.ModuleName = IIf(IsDBNull(dataReader.GetString(0)), String.Empty, dataReader.GetString(0))
                        screenConfig.ScreenName = IIf(IsDBNull(dataReader.GetString(1)), String.Empty, dataReader.GetString(1))
                        screenConfig.Sequence = IIf(IsDBNull(dataReader.GetInt32(2)), String.Empty, dataReader.GetInt32(2))
                        screenConfig.Status = IIf(IsDBNull(dataReader.GetBoolean(3)), String.Empty, dataReader.GetBoolean(3))
                        screenConfig.Script = IIf(IsDBNull(dataReader.GetString(4)), String.Empty, dataReader.GetString(4))
                        screenList.Add(screenConfig)
                    Loop
                End If
            End Using
            Return screenList.OrderBy(Function(screen) screen.Sequence).ToList()
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        Finally
            CloseConnection()
        End Try
    End Function
    Public Function IsBillSettleBoforeTillClose() As Boolean
        Try
            Dim ds As New DataTable
            Dim daCM As SqlDataAdapter
            daCM = New SqlDataAdapter(" EXEC sp_IsTableReserverdOnLastTillCLose", SpectrumCon)
            daCM.Fill(ds)
            If ds.Rows(0)("IsTableReserved") = 1 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function CheckIfValidDayClose(ByVal siteCode As String, ByVal finYear As String, ByVal dayOpenDate As Date) As Boolean
        Try
            Dim result As Boolean
            Dim query As String = "select DayCloseStatus  from DayOpenNClose where SiteCode = '" & siteCode & "' and FinYear = '" & finYear & "' and OpenDate = '" & dayOpenDate.ToString("yyyy-MM-dd") & "'"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows Then
                    dataReader.Read()
                    Dim dayclosetatus As Boolean = IIf(IsDBNull(dataReader.GetBoolean(0)), False, dataReader.GetBoolean(0))
                    If dayclosetatus = False Then
                        result = True
                    End If
                End If
            End Using
            Return result
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function
    '' added  By ketan Check BillCount For Day Open Date
    Public Function CheckBillCount(ByVal siteCode As String, ByVal finYear As String, ByVal dayOpenDate As Date) As Boolean
        Try
            Dim result As Boolean
            Dim query As String = "select isnull(SUM( isnull (netAmt,0)),0) AS NetAmt  from CashMemoHdr where  BillIntermediateStatus <>'Deleted' AND STATUS =1 AND SiteCode = '" & siteCode & "' and FinYear = '" & finYear & "' and BillDate = '" & dayOpenDate.ToString("yyyy-MM-dd") & "'"
            Using dataReader As SqlDataReader = GetReader(query)
                If Not dataReader.HasRows Then
                    result = True
                Else
                    dataReader.Read()
                    Dim Netsale As Decimal = dataReader("NetAmt")
                    If Netsale = 0 Then
                        result = True
                    Else
                        result = False
                    End If
                End If
            End Using
            Return result
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function
    Public Function CheckIfAllTerminalAreClosed(ByVal siteCode As String) As Boolean
        Try
            Dim result As Boolean
            Dim query As String = "Select * from dbo.MstTerminalID where SiteCode = '" & siteCode & "' and OpenCloseStatus = 'Open'"
            Using dataReader As SqlDataReader = GetReader(query)
                If dataReader.HasRows = False Then
                    result = True
                End If
            End Using
            Return result
        Catch ex As Exception
            LogException(ex)
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function GetSiteMailId(ByVal SiteCode As String) As String
        Try
            Dim query As String = "select EmailId  from MstSite where SiteCode = '" & SiteCode & "'"
            Return GetFilledTable(query).Rows(0)(0)
        Catch ex As Exception
            LogException(ex)
            Return String.Empty
        End Try
    End Function

    Public Function GetUsernamePassword() As DataTable
        Try
            Dim query As String = "select FldValue , FldLabel  from DefaultConfig where FldLabel IN ('SMTP.UserName','SMTP.Password','SMTP.IP','SMTP.HOST') and Sitecode ='BOCommon'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

End Class
