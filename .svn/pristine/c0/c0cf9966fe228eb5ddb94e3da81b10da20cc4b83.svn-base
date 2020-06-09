Imports System.Data
Imports System.Data.SqlClient
''' <summary>
''' This Class is Used For authorisation of User's
''' </summary>
''' <Createdby>Rama Ranjan Jena</Createdby>
''' <UpdatedBy></UpdatedBy>
''' <usedin>Login</usedin>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
Public Class clsAuthorization
#Region "Global Variables For Class"
    Dim daAuth As SqlDataAdapter
#End Region
#Region "Public Functions & Method's"
    ''' <summary>
    ''' Get the transaction based on User Roles
    ''' </summary>
    ''' <UpdatedBy></UpdatedBy>
    ''' <usedin>Login</usedin>
    ''' <UpdatedOn></UpdatedOn>
    ''' <param name="Sitecode">SiteCode</param>
    ''' <param name="UserId">UserId</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetuserRoles(ByVal Sitecode As String, ByVal UserId As String) As DataTable
        Try
            Dim strQuery As String
            Dim dtRoles As New DataTable
            strQuery = "SELECT GROLEID  FROM AUTHUSERSITEROLEMAP WHERE STATUS=1 AND SITECODE='" & Sitecode & "'" & _
                       " AND USERID='" & UserId & "'"
            daAuth = New SqlDataAdapter(strQuery, ConString)
            daAuth.Fill(dtRoles)
            Return dtRoles
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function


    Public Function GetSitedAllowedTran(ByVal Sitecode As String, ByVal TransactonCode As String) As DataTable
        Try
            Dim strQuery As String
            Dim dtAuthForKds As New DataTable
            strQuery = "SELECT * from SiteAllowedTransactions siteallowed join MstTransaction msttran on siteallowed.TransactionCode = msttran.TransactionCode "
            strQuery += "WHERE siteallowed.STATUS=1 and msttran.STATUS=1 and  siteallowed.SiteCode='" & Sitecode & "' and siteallowed.TransactionCode ='" & TransactonCode & "'"
            daAuth = New SqlDataAdapter(strQuery, ConString)
            daAuth.Fill(dtAuthForKds)
            Return dtAuthForKds
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Get the transaction applicable for Site And User Roles
    ''' </summary>
    ''' <UpdatedBy></UpdatedBy>
    ''' <usedin>Login</usedin>
    ''' <UpdatedOn></UpdatedOn>
    ''' <param name="SiteCode">siteCode</param>
    ''' <param name="StrRoles">Roles</param>
    ''' <returns>dataTable</returns>
    ''' <remarks></remarks>
    Public Function GetSiteAllowedTransaction(ByVal SiteCode As String, ByVal StrRoles As String, ByVal UserId As String) As DataTable
        Try
            Dim sbQuery As New System.Text.StringBuilder()
            Dim dtTransactions As New DataTable
            'strQuery = "SELECT DISTINCT A.AUTHTRANSACTIONCODE FROM AUTHROLETRANSACTIONMAP A " & _
            '            "INNER JOIN SITEALLOWEDTRANSACTIONS B " & _
            '            "ON A.AUTHTRANSACTIONCODE=B.TRANSACTIONCODE AND B.ACTIVE=1" & _
            '            "WHERE A.Status=1 AND A.ROLEID IN (" & StrRoles & ")"


            'Rakesh-21.10.2013-8276- Set rights based on transaction
            sbQuery.Append("SELECT DISTINCT A.TransactionCode AuthTransactionCode, ISNULL(ISNULL(C.Rights, B.STATUS), 0) AS Rights " & vbCrLf)
            sbQuery.Append("FROM SiteAllowedTransactions A " & vbCrLf)
            sbQuery.Append("LEFT JOIN AuthRoleTransactionMap B ON A.TransactionCode = B.AuthTransactionCode AND B.RoleID IN (" & StrRoles & ") " & vbCrLf)
            sbQuery.Append("LEFT JOIN AuthUserSiteTransactionMap C ON A.TransactionCode=C.AuthTransactionCode AND C.UserID = '" & UserId & "'" & vbCrLf)
            sbQuery.Append("AND A.Active=1 " & vbCrLf)
            sbQuery.Append("WHERE A.Status=1 " & vbCrLf)
            sbQuery.Append("and A.SiteCode='" & SiteCode & "'")
            daAuth = New SqlDataAdapter(sbQuery.ToString(), ConString)
            daAuth.Fill(dtTransactions)
            Return dtTransactions
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Get transaction for which User have special Authorisation
    ''' </summary>
    ''' <UpdatedBy></UpdatedBy>
    ''' <usedin>Login</usedin>
    ''' <UpdatedOn></UpdatedOn>
    ''' <param name="SiteCode">siteCode </param>
    ''' <param name="Userid">Userid </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSpecialAuthorization(ByVal SiteCode As String, ByVal Userid As String) As DataTable
        Try
            Dim strQuery As String
            Dim dtTransactions As New DataTable
            strQuery = "SELECT AUTHTRANSACTIONCODE, ISNULL(Rights,0) AS Rights FROM AUTHUSERSITETRANSACTIONMAP WHERE RIGHTS=1 AND SITECODE='" & SiteCode & "'" & _
                       " AND USERID='" & Userid & "'"
            daAuth = New SqlDataAdapter(strQuery, ConString)
            daAuth.Fill(dtTransactions)
            Return dtTransactions
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
#End Region
End Class
