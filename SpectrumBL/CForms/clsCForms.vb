Imports SpectrumCommon
Imports System.Text
Imports System.Data.SqlClient

Public Class clsCForms
    Inherits clsCommon
    ''' <summary>
    ''' Get All Customers Name
    ''' </summary>
    ''' <param name="CLPProgram"></param>
    ''' <param name="SiteCode"></param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetAllCustomers(ByVal SiteCode As String, Optional ByVal IsEdit As Boolean = False) As DataTable
        Try
            Dim daCust As New SqlDataAdapter
            Dim dtCust As DataTable
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0
            If Not IsEdit Then
                SqlQuery.Append("Select CUST.CardNo AS CustomerCode,CUST.NameOnCard AS CustomerName from SalesInvoice SI " & vbCrLf)
                SqlQuery.Append("INNER JOIN SalesOrderHdr SOH ON SI.DocumentNumber = SOH.SaleOrderNumber  AND SI.SITECODE= SOH.SITECODE " & vbCrLf)
                SqlQuery.Append("LEFT JOIN CLPCustomers CUST ON SOH.CustomerNo=CUST.CardNo" & vbCrLf)
                SqlQuery.Append("LEFT OUTER JOIN (SELECT MT.TaxCode,MT.TaxName,SOT.SaleOrderNumber,SOT.SiteCode,SOT.TaxValue,MT.STATUS FROM MSTTAX MT INNER JOIN " & vbCrLf)
                SqlQuery.Append("SalesOrderTaxDtls SOT ON MT.TaxCode=SOT.TaxLabel AND MT.STATUS=1 and SOT.STATUS=1 " & vbCrLf)
                SqlQuery.Append("GROUP BY MT.TaxCode,MT.TaxName,SOT.SaleOrderNumber,SOT.SiteCode,SOT.TaxValue,MT.STATUS)" & vbCrLf)
                SqlQuery.Append("TAXDTLS " & vbCrLf)
                SqlQuery.Append("ON SOH.SaleOrderNumber=TAXDTLS.SaleOrderNumber AND SOH.SiteCode=TAXDTLS.SiteCode " & vbCrLf)
                SqlQuery.Append("LEFT OUTER JOIN (Select CD.InvoiceNo,CD.InvoiceDate from CustomerCform CF inner join CustomerCformdtls CD ON CF.CustomerCformid=CD.CustomerCformId  " & vbCrLf)
                SqlQuery.Append(" Where CD.status=1)CFORM ON SI.SaleInvNumber=CFORM.InvoiceNo AND SI.SOInvDate=CFORM.InvoiceDate" & vbCrLf)
                SqlQuery.Append("  Where TAXDTLS.TaxName like '%CST%' AND SOH.SiteCode='" & SiteCode & "' AND CFORM.InvoiceDate IS NULL" & vbCrLf)
                SqlQuery.Append("  group By CUST.CardNo,CUST.NameOnCard " & vbCrLf)
            Else
                SqlQuery.Append("SELECT CUST.CardNo AS CustomerCode,CUST.NameOnCard AS CustomerName FROM CustomerCForm CF  " & vbCrLf)
                SqlQuery.Append("inner join CLPCustomers CUST ON CF.CustomerNo=CUST.CardNo and cf.STATUS=1  " & vbCrLf)
                SqlQuery.Append("group by cust.CardNo,cust.NameOnCard " & vbCrLf)
            End If
            daCust = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtCust = New DataTable
            daCust.Fill(dtCust)
            Return dtCust
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Get List of Invoice for which CST Tax of 2% have been added 
    ''' </summary>
    ''' <param name="SiteCode"></param>
    ''' <param name="IsEdit"></param>
    ''' <param name="CustomerName"></param>
    ''' <param name="FromDate"></param>
    ''' <param name="ToDate"></param>
    ''' <param name="State"></param>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Public Function GetSOListCForms(ByVal SiteCode As String, Optional ByVal IsEdit As Boolean = False, Optional ByVal CustomerName As String = Nothing, Optional ByVal FromDate As DateTime = Nothing, Optional ByVal ToDate As DateTime = Nothing, Optional ByVal State As String = Nothing) As DataTable
        Try
            Dim daCustCForms As New SqlDataAdapter
            Dim dtCustCForms As DataTable
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0
            SqlQuery.Append(" Select Convert(Bit,'') as Sel,SI.SaleInvNumber as InvoiceNo,SI.SOInvDate AS InvoiceDate,SI.UPDATEDON  AS UpdatedOn," & vbCrLf)
            SqlQuery.Append(" CUST.NameOnCard AS CustomerName,SUM(SI.AmountTendered) AS Amount,CUSTDET.Description" & vbCrLf)
            If IsEdit = True Then
                SqlQuery.Append(" ,CFORM.CFormNo,CFORM.CFormDate,CFORM.Remarks " & vbCrLf)
            End If

            SqlQuery.Append(",SOH.CustomerNo from SalesInvoice SI  " & vbCrLf)
            SqlQuery.Append(" INNER JOIN SalesOrderHdr SOH ON SI.DocumentNumber = SOH.SaleOrderNumber  AND SI.SITECODE= SOH.SITECODE" & vbCrLf)
            SqlQuery.Append(" INNER JOIN (select SaleOrderNumber,SiteCode,SUM(DeliveredQty) AS TotDeliveredQty from SOPackagingBoxDeliveryDtl  GROUP BY SaleOrderNumber,SiteCode )SOPD ON " & vbCrLf)
            SqlQuery.Append(" SOH.SaleOrderNumber = SOPD.SaleOrderNumber And SOH.SiteCode = SOPD.SiteCode And SOPD.TotDeliveredQty > 0 " & vbCrLf)
            If IsEdit = True Then
                SqlQuery.Append(" INNER JOIN  (Select CF.CustomerCformId,CF.CFormNo,CF.CFormDate,CF.CustomerNo,CD.InvoiceNo,CD.InvoiceDate,CD.Remarks,CF.Status " & vbCrLf)
                SqlQuery.Append(" AS HdrStatus,CD.Status as DtlStatus,CF.SiteCode from CustomerCForm  CF INNER JOIN CustomerCFormDtls CD on CF.CustomerCformId=CD.CustomerCformId)" & vbCrLf)
                SqlQuery.Append("CFORM ON SOH.CustomerNo=CFORM.CustomerNo AND SI.SaleInvNumber=CFORM.InvoiceNo AND SI.SOInvDate=CFORM.InvoiceDate" & vbCrLf)
                SqlQuery.Append("AND SI.SiteCode=CFORM.SiteCode AND CFORM.HdrStatus=1 AND CFORM.DtlStatus=1 " & vbCrLf)
            End If
            SqlQuery.Append(" LEFT OUTER JOIN CLPCustomers CUST ON SOH.CustomerNo=CUST.CardNo " & vbCrLf) ''AND SOH.SiteCode=CUST.SiteCode Commend By ketan Customer Name Not DIsplay 
            SqlQuery.Append(" LEFT OUTER JOIN(select MAC.Description,CUSTADD.CardNo,CUSTADD.StateCode From CLPCustomerAddress CUSTADD " & vbCrLf)
            SqlQuery.Append(" LEFT OUTER JOIN MstAreaCode MAC ON CUSTADD.StateCode=MAC.AreaCode WHERE " & vbCrLf)
            SqlQuery.Append(" CUSTADD.DefaultAddress=1 AND CUSTADD.STATUS=1)CUSTDET ON SOH.CUSTOMERNo=CUSTDET.CardNo" & vbCrLf)
            If IsEdit = True Then
                SqlQuery.Append("WHERE SI.SiteCode='" & SiteCode & "' " & vbCrLf)
            Else
                SqlQuery.Append(" LEFT OUTER JOIN (SELECT MT.TaxCode,MT.TaxName,SOT.SaleOrderNumber,SOT.SiteCode,SOT.TaxValue,MT.STATUS FROM MSTTAX MT INNER JOIN " & vbCrLf)
                SqlQuery.Append(" SalesOrderTaxDtls SOT ON MT.TaxCode=SOT.TaxLabel AND MT.STATUS=1 and SOT.STATUS=1" & vbCrLf)
                SqlQuery.Append(" GROUP BY MT.TaxCode,MT.TaxName,SOT.SaleOrderNumber,SOT.SiteCode,SOT.TaxValue,MT.STATUS)" & vbCrLf)
                SqlQuery.Append(" TAXDTLS" & vbCrLf)
                SqlQuery.Append(" ON SOH.SaleOrderNumber=TAXDTLS.SaleOrderNumber AND SOH.SiteCode=TAXDTLS.SiteCode" & vbCrLf)
                SqlQuery.Append(" LEFT OUTER JOIN (Select CD.InvoiceNo,CD.InvoiceDate from CustomerCform CF inner join CustomerCformdtls CD ON CF.CustomerCformid=CD.CustomerCformId " & vbCrLf)
                SqlQuery.Append(" Where CD.status=1)CFORM ON SI.SaleInvNumber=CFORM.InvoiceNo AND SI.SOInvDate=CFORM.InvoiceDate" & vbCrLf)
                SqlQuery.Append(" Where TAXDTLS.TaxName like '%CST%' AND SOH.SiteCode='" & SiteCode & "' AND CFORM.InvoiceDate IS NULL" & vbCrLf)
            End If
            If CustomerName <> String.Empty AndAlso FromDate <> Nothing AndAlso ToDate <> Nothing AndAlso State <> String.Empty Then
                SqlQuery.Append("AND CAST(SI.SOInvDate as Date) Between Convert(DateTime,'" & FromDate.ToString("yyyyMMdd") & "') AND Convert(DateTime,'" & ToDate.ToString("yyyyMMdd") & "') AND " & vbCrLf)
                SqlQuery.Append("SOH.CustomerNo IN (Select CardNo from CLPCustomers Where NameOnCard='" & CustomerName & "') " & vbCrLf)
                SqlQuery.Append("AND CUSTDET.StateCode IN (Select AreaCode from MstAreaCode Where Description='" & State & "' )" & vbCrLf)
            ElseIf CustomerName <> String.Empty AndAlso FromDate <> Nothing AndAlso ToDate <> Nothing Then
                SqlQuery.Append("AND CAST(SI.SOInvDate as Date) Between Convert(DateTime,'" & FromDate.ToString("yyyyMMdd") & "') AND Convert(DateTime,'" & ToDate.ToString("yyyyMMdd") & "') AND " & vbCrLf)
                SqlQuery.Append("SOH.CustomerNo IN (Select CardNo from CLPCustomers Where NameOnCard='" & CustomerName & "') " & vbCrLf)
            ElseIf FromDate <> Nothing AndAlso ToDate <> Nothing AndAlso State <> String.Empty Then
                SqlQuery.Append("AND CAST(SI.SOInvDate as Date) Between  Convert(DateTime,'" & FromDate.ToString("yyyyMMdd") & "') AND Convert(DateTime,'" & ToDate.ToString("yyyyMMdd") & "') " & vbCrLf)
                SqlQuery.Append(" AND CUSTDET.StateCode IN (Select AreaCode from MstAreaCode Where Description='" & State & "')" & vbCrLf)
            ElseIf State <> String.Empty AndAlso CustomerName <> String.Empty Then
                SqlQuery.Append(" AND CUSTDET.StateCode IN (Select AreaCode from MstAreaCode Where Description='" & State & "')" & vbCrLf)
                SqlQuery.Append("AND SOH.CustomerNo IN (Select CardNo from CLPCustomers Where NameOnCard='" & CustomerName & "') " & vbCrLf)
            ElseIf FromDate <> Nothing AndAlso ToDate <> Nothing Then
                SqlQuery.Append("AND CAST(SI.SOInvDate as Date) Between Convert(DateTime,'" & FromDate.ToString("yyyyMMdd") & "') AND Convert(DateTime,'" & ToDate.ToString("yyyyMMdd") & "') " & vbCrLf)
            ElseIf CustomerName <> String.Empty Then
                SqlQuery.Append("AND SOH.CustomerNo IN (Select CardNo from CLPCustomers Where NameOnCard ='" & CustomerName & "') " & vbCrLf)
            ElseIf State <> String.Empty Then
                SqlQuery.Append(" AND CUSTDET.StateCode IN (Select AreaCode from MstAreaCode Where Description='" & State & "')" & vbCrLf)
            End If
            If Not IsEdit Then
                SqlQuery.Append(" Group By SI.SaleInvNumber,SI.SOInvDate,cust.NameOnCard,CUSTDET.Description,SOH.CustomerNo,SI.UPDATEDON" & vbCrLf)
                SqlQuery.Append("Order By  SI.UPDATEDON DESC" & vbCrLf)
            Else
                SqlQuery.Append(" Group By SI.SaleInvNumber,SI.UPDATEDON,SI.SOInvDate,cust.NameOnCard,CFORM.CFormNo,CFORM.CFormDate,CUSTDET.Description,CFORM.Remarks,SOH.CustomerNo" & vbCrLf)
                SqlQuery.Append("Order By  SI.UPDATEDON DESC" & vbCrLf)
            End If
            daCustCForms = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtCustCForms = New DataTable
            daCustCForms.Fill(dtCustCForms)
            Return dtCustCForms
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Get All States
    ''' </summary>
    ''' <param name="CLPProgram"></param>
    ''' <param name="SiteCode"></param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetAllStates() As DataTable
        Try
            Dim daState As New SqlDataAdapter
            Dim dtState As DataTable
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0
            SqlQuery.Append("select Areacode,Description from MstAreaCode Where AreaType=102" & vbCrLf)
            daState = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtState = New DataTable
            daState.Fill(dtState)
            Return dtState
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Get CustomerCForms Data 
    ''' </summary>
    ''' <returns>Dataset</returns>
    ''' <remarks></remarks>
    Public Function GetCustCForms() As DataSet

        Dim daCustCforms As New SqlDataAdapter
        Dim vStmtQry As New StringBuilder
        Dim dsCustCforms As DataSet
        Try
            vStmtQry.Append("Select * from CustomerCform ;" & vbCrLf)
            vStmtQry.Append("Select * from CustomerCformdtls" & vbCrLf)
            daCustCforms = New SqlClient.SqlDataAdapter(vStmtQry.ToString, SpectrumCon)
            dsCustCforms = New DataSet
            daCustCforms.Fill(dsCustCforms)
            dsCustCforms.Tables(0).TableName = "CustomerCForm"
            dsCustCforms.Tables(1).TableName = "CustomerCformdtls"
            Dim KeySOCform(1) As DataColumn
            KeySOCform(0) = dsCustCforms.Tables("CustomerCForm").Columns("CustomerCformId")
            KeySOCform(1) = dsCustCforms.Tables("CustomerCForm").Columns("CustomerNo")
            Dim KeySOCformDtls(2) As DataColumn
            KeySOCformDtls(0) = dsCustCforms.Tables("CustomerCFormDtls").Columns("CustomerCformId")
            KeySOCformDtls(1) = dsCustCforms.Tables("CustomerCFormDtls").Columns("InvoiceNo")
            KeySOCformDtls(2) = dsCustCforms.Tables("CustomerCFormDtls").Columns("InvoiceDate")
            dsCustCforms.Tables("CustomerCForm").PrimaryKey = KeySOCform
            dsCustCforms.Tables("CustomerCFormDtls").PrimaryKey = KeySOCformDtls
            Return dsCustCforms
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Get CustomerNo from Sales Invoice No.
    ''' </summary>
    ''' <param name="SiteCode"></param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function GetCustomerNo(ByVal SalesInvNo As String, ByVal SalesInvDate As Date, ByVal SiteCode As String) As String
        Try
            Dim daCustNo As New SqlDataAdapter
            Dim dtCustNo As DataTable
            Dim SqlQuery As New StringBuilder
            SqlQuery.Length = 0
            SqlQuery.Append(" select soh.CustomerNo from salesorderhdr SOH INNER JOIN SalesInvoice SI ON SOH.SaleOrderNumber=si.DocumentNumber and SOH.SITECODE=SI.SITECODE" & vbCrLf)
            SqlQuery.Append("where SI.SaleInvNumber='" & SalesInvNo & "' AND SI.SOInvDate=  Convert(DateTime,'" & SalesInvDate.ToString("yyyyMMdd") & "')  AND SI.SiteCode ='" & SiteCode & "'   " & vbCrLf)
            daCustNo = New SqlDataAdapter(SqlQuery.ToString, SpectrumBL.DataBaseConnection.ConString)
            dtCustNo = New DataTable
            daCustNo.Fill(dtCustNo)
            Return dtCustNo.Rows(0)(0)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Updating Dataset and GLNrange number
    ''' </summary>
    ''' <param name="dsCustCForm"></param>
    ''' <param name="IsDelete"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function UpdateCFormDetails(ByVal dsCustCForm As DataSet, Optional ByVal IsDelete As Boolean = False) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            Dim clsCommon As New clsCommon()
            OpenConnection()
            tran = SpectrumCon.BeginTransaction()
            If clsCommon.SaveData(dsCustCForm, SpectrumCon, tran) Then
                If Not IsDelete Then
                    If UpdateDocumentNo("CustCform", SpectrumCon, tran) = False Then
                        tran.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If

                tran.Commit()
                CloseConnection()
                Return True
            End If
        Catch ex As Exception
            tran.Rollback()
            Return False
        End Try
    End Function

End Class
