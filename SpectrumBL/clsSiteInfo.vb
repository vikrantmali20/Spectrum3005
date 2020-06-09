Public Class clsSiteInfo
    Inherits clsCommon
    Public Sub New()

    End Sub

    Public Function GetAllSitesForDelivery() As DataTable
        Try
            Dim query As String = "select *  from MstSite where BusinessCode in ('Store','WH') and IsActive = 1 "
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
End Class
