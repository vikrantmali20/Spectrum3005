Imports System.Data
Imports System.Data.SqlClient


Public Class clsBirthListSales
    Private dsBirthListSales As New DataSet
    Private objClsSalesOrder As New clsSalesOrder
    Private objClsComman As New clsCommon
    Private objclsBirthListGobal As New clsBirthListGobal
    ''' <summary>
    ''' Get item price
    ''' </summary>
    ''' <param name="strSiteCode"></param>
    ''' <param name="strArticle"></param>
    ''' <param name="strEAN"></param>
    ''' <returns></returns>
    ''' <usedby>frmBirthListSales.vb</usedby>
    ''' <remarks></remarks>

    Public Function GetItemPrice(Optional ByVal strSiteCode As String = "", Optional ByVal strArticle As String = "", Optional ByVal strEAN As String = "") As DataTable
        Dim sqlCommand As New SqlCommand

        Dim dtItemPrice As New DataTable
        Try
            OpenConnection()
            sqlCommand.CommandText = "select  Price from PricingCondition " & _
                                        " inner join BirthListRequestedItems" & _
                                     " on BirthListRequestedItems.EAN=PricingCondition.EAN" & _
                                     " and BirthListRequestedItems.ArticleCode=PricingCondition.ArticleCode " & _
                                     " and BirthListRequestedItems.SiteCode=PricingCondition.SiteCode"
            sqlCommand.Connection = SpectrumCon
            Dim sqlAdptor As SqlDataAdapter
            sqlAdptor.SelectCommand = sqlCommand
            sqlAdptor.Fill(dtItemPrice)
            Return dtItemPrice
        Catch ex As Exception
            LogException(ex)
        Finally
            CloseConnection() 
        End Try
    End Function
    ''' <summary>
    ''' Get BirthList customer address 
    ''' </summary>
    ''' <param name="strCustomerId"></param>
    ''' <param name="strSiteCode"></param>
    ''' <param name="iAddressType">default is "1"</param>
    ''' <usedby>frmBirthListSales.vb</usedby>
    ''' <returns>String (Address)</returns>
    ''' <remarks></remarks>
    Public Function GetBLCustomerAddress(ByVal strCustomerId As String, ByVal strSiteCode As String, Optional ByVal iAddressType As Integer = 1, Optional ByVal strCustomerType As String = "SO") As String
        Dim sqlCommand As New SqlCommand
        Dim dtCustomerInfo As New DataTable
        Dim strCutomerAddress As String = ""
        Try
            OpenConnection()
                      If strCustomerType = "CLP" Then
                sqlCommand.CommandText = "select  AddressLn1,AddressLn2,AddressLn3,AddressLn4,PinCode,CityCode," & _
"a.Description as City ,StateCode,CountryCode,b.Description as State," & _
"c.Description as Country from CLPCustomerAddress  inner join MstAreaCode as" & _
" a on CLPCustomerAddress.CityCode=a.AreaCode inner join MstAreaCode as b " & _
"on CLPCustomerAddress.StateCode=b.AreaCode inner Join MstAreaCode as c " & _
"on CLPCustomerAddress.CountryCode=c.AreaCode" & _
" where cardno='10009' and addresstype='1' "

            Else
                sqlCommand.CommandText = "select  AddressLn1,AddressLn2,AddressLn3,AddressLn4,PinCode,CityCode,a.Description as City ,StateCode,CountryCode,b.Description as State,c.Description as Country from CustomerAddress inner join MstAreaCode as" & _
" a on CustomerAddress.CityCode=a.AreaCode inner join MstAreaCode as b on CustomerAddress.StateCode=b.AreaCode inner Join MstAreaCode as c on CustomerAddress.CountryCode=c.AreaCode where customerno='" & strCustomerId & "' and addresstype=" & iAddressType & " and sitecode='" & strSiteCode & "'"

            End If


            Dim sqlAdptor As New SqlDataAdapter
            sqlCommand.Connection = SpectrumCon
            sqlAdptor.SelectCommand = sqlCommand
            sqlAdptor.Fill(dtCustomerInfo)
            If (dtCustomerInfo.Rows.Count > 0) Then
                strCutomerAddress = dtCustomerInfo.Rows(0)("AddressLn1") + "," + dtCustomerInfo.Rows(0)("AddressLn2") + "," + dtCustomerInfo.Rows(0)("AddressLn3") + "," + dtCustomerInfo.Rows(0)("AddressLn4") + ". " + dtCustomerInfo.Rows(0)("City") + " " + dtCustomerInfo.Rows(0)("PinCode") + "." + dtCustomerInfo.Rows(0)("State") + " , " + dtCustomerInfo.Rows(0)("Country") + "."
            End If
            Return strCutomerAddress
        Catch ex As Exception
            LogException(ex)
            Return strCutomerAddress
        End Try
    End Function
    ''' <summary>
    ''' Get entered birthlist information
    ''' </summary>
    ''' <param name="strQuery"></param>
    ''' <param name="strErrorMsg"></param>
    ''' <returns></returns>
    ''' <usedby>frmBirthListSales.vb</usedby>
    ''' <remarks></remarks>
    Public Function BirthListInfo(ByVal strQuery As String, ByRef strErrorMsg As String) As DataTable
        Dim dtQueryData As New DataTable
        Dim cmdRetrieveQuery As New SqlCommand
        Dim adt As New SqlDataAdapter
        Try
            OpenConnection()
            cmdRetrieveQuery.CommandText = strQuery
            cmdRetrieveQuery.Connection = SpectrumCon
            adt.SelectCommand = cmdRetrieveQuery
            adt.Fill(dtQueryData)
            Return objclsBirthListGobal.CreateBirthListItemTable(dtQueryData)
        Catch ex As Exception
            LogException(ex)
            strErrorMsg = "RetrieveQuery Failed."
            Return Nothing

        Finally
            strErrorMsg = String.Empty
        End Try
    End Function


    ''' <summary>
    ''' Get BL price change config from Default Config
    ''' </summary>
    ''' <param name="strQuery"></param>
    ''' <param name="strErrorMsg"></param>
    ''' <returns></returns>
    ''' <usedby>frmBirthListSales.vb</usedby>
    ''' <remarks></remarks>
    Public Function GetBLPriceConfig(ByVal strQuery As String, ByRef strErrorMsg As String) As DataTable
        Dim dtQueryData As New DataTable
        Dim cmdRetrieveQuery As New SqlCommand
        Dim adt As New SqlDataAdapter
        Try
            OpenConnection()
            cmdRetrieveQuery.CommandText = strQuery
            cmdRetrieveQuery.Connection = SpectrumCon()
            adt.SelectCommand = cmdRetrieveQuery
            adt.Fill(dtQueryData)
            Return dtQueryData
        Catch ex As Exception
            LogException(ex)
            strErrorMsg = "RetrieveQuery Failed."
            Return Nothing

        Finally
            strErrorMsg = String.Empty
        End Try
    End Function

    Dim dtVoucherTable As New DataTable
    Public Function TableStructure() As DataTable
        Try
            dtVoucherTable.Columns.Add("SiteCode")
            dtVoucherTable.Columns.Add("EAN")
            dtVoucherTable.Columns.Add("ArticleCode")
            dtVoucherTable.Columns.Add("NetAmount", System.Type.GetType("System.Decimal"))
            dtVoucherTable.Columns.Add("RequstedQty")
            dtVoucherTable.Columns.Add("BookedQty", System.Type.GetType("System.Int32"))
            dtVoucherTable.Columns.Add("PurchasedQty", System.Type.GetType("System.Decimal"))
            dtVoucherTable.Columns.Add("SellingPrice", System.Type.GetType("System.Decimal"))
            dtVoucherTable.Columns.Add("Discription")
            Return dtVoucherTable
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
End Class
