Public Class clsSiteBank
    Inherits clsCommon
    Public Sub New()

    End Sub

    Public Function GetBankNames(ByVal siteCode As String, ByVal forEdc As Boolean) As DataTable
        Try            
            Dim query As String = "select * from MSTSiteBankMap where Sitecode = '" & siteCode & "' and ForEdc='" & forEdc & "'"
            Return GetFilledTable(query)        
        Catch ex As Exception
            Return Nothing
            LogException(ex)
        End Try
    End Function
End Class
