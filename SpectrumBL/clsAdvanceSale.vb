Imports System.Data
Imports System.Data.SqlClient
Public Class clsAdvanceSale
    Inherits clsCommon
    Public Function GetVoucherProg(ByVal SiteCode As String, ByVal vourcherType As String) As DataTable
        Try

            Dim strQuery As String
            strQuery = "SELECT A.VOUCHERCODE,A.VOUCHERDESC,A.VOURCHERTYPE,A.ExpiryAfterDays,A.ArticleCode,A.ALLOWANYDENO,A.ISPREPRINTED FROM MSTVOUCHER A Inner join VoucherSiteMap B ON a.VoucherCode=B.VoucherCode  WHERE A.STATUS=1 AND B.Status=1 and  B.Sitecode='" & SiteCode & "' AND VOURCHERTYPE='" & vourcherType & "'"
            Dim dtTemp As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dtTemp)
            Return dtTemp
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function CheckActiveVoucherProg(ByVal SiteCode As String, ByVal vourcherType As String) As DataTable
        Try

            Dim strQuery As String
            strQuery = "SELECT A.VOUCHERCODE,A.VOUCHERDESC,A.VOURCHERTYPE,A.ExpiryAfterDays,A.ArticleCode,A.ALLOWANYDENO,A.ISPREPRINTED FROM MSTVOUCHER A Inner join VoucherSiteMap B ON a.VoucherCode=B.VoucherCode  WHERE (A.STATUS=0 OR B.Status=0)  AND   B.Sitecode='" & SiteCode & "' AND VOURCHERTYPE='" & vourcherType & "'"
            Dim dtTemp As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dtTemp)
            Return dtTemp
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetVoucherDenom() As DataTable
        Try
            Dim strQuery As String
            strQuery = "SELECT VOUCHERCODE,DENOMINATIONAMT FROM VOUCHERDENOMINATION WHERE STATUS =1"
            Dim dtTemp As New DataTable
            Dim da As New SqlDataAdapter(strQuery, ConString)
            da.Fill(dtTemp)
            Return dtTemp
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
End Class
