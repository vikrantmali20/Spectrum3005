Imports System.Data
Imports System.Data.SqlClient

Public Class clsBirthListUpdate
    ''' <summary>
    ''' Get selected BirthListInformation
    ''' </summary>
    ''' <param name="strBirthListId"></param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>

    Public Function GetBirthListInfo(ByVal strBirthListId As String) As DataSet
        Dim dsQueryData As New DataSet()
        Try
            OpenConnection()
            Dim sqlCommand As New SqlCommand
            Dim sqlAdp As New SqlDataAdapter()
            sqlCommand.CommandText = "select BirthList.SiteCode,BirthList.Customerid,BirthList.BirthListId,BirthList.EventDate,BirthList.EventId,BirthList.DeliveryDate,BirthList.CustomerId, CustomerSaleOrder.TitleCode,CustomerSaleOrder.FirstName,CustomerSaleOrder.MiddleName,CustomerSaleOrder.LastName,CustomerSaleOrder.CustomerName,CustomerSaleOrder.DateofBirth,CustomerSaleOrder.ResidencePhone,CustomerSaleOrder.MobilePhone,CustomerSaleOrder.OfficePhone,CustomerSaleOrder.Occupation,CustomerSaleOrder.Education,CustomerSaleOrder.EmailId,CustomerSaleOrder.Gender,CustomerSaleOrder.MaritalStatus,T.ShortDesc as NameTitle ,CustomerAddress.CustomerType,GMAdd.ShortDesc as AddressTypeName,CustomerAddress.AddressType,CustomerAddress.AddressLn1,CustomerAddress.AddressLn2,CustomerAddress.AddressLn3,CustomerAddress.AddressLn4,CustomerAddress.PinCode,CustomerAddress.CityCode,CustomerAddress.StateCode,CustomerAddress.CountryCode, a.Description as City,b.Description as State,c.Description as Country from BirthList inner join CustomerSaleOrder on BirthList.Customerid=CustomerSaleOrder.CustomerNo inner join GeneralCodeMst as T on CustomerSaleOrder.TitleCode=T.Code and T.CodeType='Title' inner join CustomerAddress  on BirthList.Customerid=CustomerAddress.CustomerNo  inner join GeneralCodeMst as GMAdd on  CustomerAddress.AddressType = GMAdd.Code and GMAdd.CodeType='AddressType' inner join MstAreaCode as a on CustomerAddress.CityCode= a.areacode inner join MstAreaCode as b on CustomerAddress.StateCode= b.areacode inner join MstAreaCode as c on CustomerAddress.CountryCode= c.areacode where BirthListId ='" & strBirthListId & "' select BirthListRequestedItems.sitecode,BirthListRequestedItems.EAN,BirthListRequestedItems.BirthListId,BirthListRequestedItems.articlecode,BirthListRequestedItems.requstedqty,BirthListRequestedItems.bookedqty,BirthListRequestedItems.DeliveredQty ,Price,MstEAN.Discription,BALANCEQTY from BirthListRequestedItems inner join  MstEAN on BirthListRequestedItems.EAN =MstEAN.EAN inner join PricingCondition on BirthListRequestedItems.EAN =PricingCondition.EAN and BirthListRequestedItems.ArticleCode=PricingCondition.ArticleCode and BirthListRequestedItems.SiteCode = PricingCondition.SiteCode inner join ARTICLESTOCKBALANCES on BirthListRequestedItems.SiteCode=ARTICLESTOCKBALANCES.SiteCode and BirthListRequestedItems.EAN =ARTICLESTOCKBALANCES.EAN  where BirthListRequestedItems.BirthListId='" & strBirthListId & "'"
            sqlCommand.Connection = SpectrumCon
            sqlAdp.SelectCommand = sqlCommand
            sqlAdp.Fill(dsQueryData)
            dsQueryData.Tables(0).TableName = "BirthListCustomerInfo"
            dsQueryData.Tables(1).TableName = "BirthListCustomerItemInfo"
            Return dsQueryData
        Catch ex As Exception
            LogException(ex)
            Return dsQueryData
        End Try

    End Function

    Public Function SaveBirthListClose(ByVal strSiteCode As String, ByVal strCREATEDBY As String, ByVal strFinYear As String, ByVal strBirthListID As String, ByVal strPromotionID As String, ByVal strPromotionValue As Decimal, ByVal strStatus As String, ByVal decBLDisount As Decimal, ByVal decCLPDiscount As Decimal, ByVal decCLPPoints As Decimal, ByVal dateCreated As Date, ByVal dtBirthListClose As POSDBDataSet.BirthListCancelDataTable, ByVal AdpBirthListClose As POSDBDataSetTableAdapters.BirthListCancelTableAdapter, Optional ByVal dtPosBirthListItems As POSDBDataSet.BirthListRequestedItemsDataTable = Nothing, Optional ByVal adpPosBirthListItemsTableAdaptor As POSDBDataSetTableAdapters.BirthListRequestedItemsTableAdapter = Nothing, Optional ByVal dateDayOpen As DateTime = Nothing) As Boolean
        Try
            If Not dtBirthListClose Is Nothing AndAlso Not AdpBirthListClose Is Nothing Then
                OpenConnection()
                Dim tran As SqlTransaction = SpectrumCon.BeginTransaction()
                'dtBirthListClose = AdpBirthListClose.GetData(strSiteCode, strBirthListID)
                AdpBirthListClose.UpdateStatusClose(decBLDisount, decCLPDiscount, decCLPPoints, Decimal.Add(decBLDisount, decCLPDiscount), strStatus, strSiteCode, strBirthListID)
                Dim objclsBirthListGlobal As New clsBirthListGobal

                If (objclsBirthListGlobal.InsertQuery("Insert into  SalesDiscDtl(SiteCode,FinYear,BillNo,documentType,PromotionID,PromotionValue,CREATEDBY,CREATEDAT,CREATEDON,UPDATEDBY,UPDATEDAT,UPDATEDON,STATUS,BillDate ) values ('" & strSiteCode & "','" & strFinYear & "','" & strBirthListID & "','BLS','" & strPromotionID & "'," & clsCommon.ConvertToEnglish(CDbl(strPromotionValue)) & ",'" & strCREATEDBY & "','" & strSiteCode & "','" & Format(dateCreated, "yyyyMMdd") & "','" & strCREATEDBY & "','" & strSiteCode & "','" & Format(dateCreated, "yyyyMMdd") & "','1','" & Format(dateDayOpen, "yyyyMMdd") & "')", tran)) Then

                    'For Each dr As POSDBDataSet.BirthListRequestedItemsRow In dtPosBirthListItems.Rows
                    '    Console.WriteLine(dr.RowState.ToString())
                    'Next

                    If Not dtPosBirthListItems Is Nothing AndAlso dtPosBirthListItems.Rows.Count > 0 Then
                        If Not adpPosBirthListItemsTableAdaptor Is Nothing Then
                            'adpPosBirthListItemsTableAdaptor.Transaction = tran
                            Console.WriteLine(adpPosBirthListItemsTableAdaptor.Connection.ConnectionString.ToString())
                            adpPosBirthListItemsTableAdaptor.Update(dtPosBirthListItems)
                        End If
                    End If
                    tran.Commit()

                    Try
                        Dim objclscomman As New clsCommon()
                        OpenConnection()
                        Dim tran1 As SqlTransaction = SpectrumCon.BeginTransaction()
                        objclscomman.AssignVoucherToBill(SpectrumCon, tran1, strSiteCode, strBirthListID, strFinYear, "BLS")

                    Catch ex1 As Exception

                    End Try

                Else
                    tran.Rollback()
                End If

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        Finally
            CloseConnection()
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

End Class






