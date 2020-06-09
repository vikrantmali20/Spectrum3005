
Public Interface ICreditSale
    ' Function UpdateCredit(ByVal SalesInvoice As List(Of SpectrumCommon.SalesInvoice), ByVal DayOpenDate As DateTime) As Boolean
    Function UpdateCredit(ByVal SalesInvoice As List(Of SpectrumCommon.SalesInvoice), ByVal DayOpenDate As DateTime, Optional ByVal Billno As String = "", _
                          Optional ByVal SiteCode As String = "", Optional ByVal finyear As String = "", Optional ByVal UserId As String = "", _
                          Optional ByVal DocumentType As String = "", Optional ByVal RetrievalReferenceNumber As String = "", _
                          Optional ByVal TransactionTime As String = "", Optional PayByInnovitii As Boolean = False, _
                          Optional InnovatiiForTerminals As String = "", Optional TerminalId As String = "", _
                          Optional dtInnoviti As DataTable = Nothing, Optional InnovitiPaymentEnable As Boolean = False) As Boolean 'modified by khusrao adil on 25-07-2018
    Function GetCustomers() As List(Of SpectrumCommon.CustomerInfo)
    Function GetCreditSalesOrder() As List(Of SpectrumCommon.SalesInvoice)
    Function GetCreditCashMemo() As List(Of SpectrumCommon.SalesInvoice)
    Function getBillbyCustomer(ByVal customerID As String) As List(Of SpectrumCommon.SalesInvoice)
    Function GetTotalCreditSalebyDate(ByVal salesdate As Date) As Decimal
    Function GetCreditBillByDeliveryPerson() As System.Collections.Generic.List(Of SpectrumCommon.SalesInvoice)
    Function GetCreditSales(ByVal Sitecode As String) As DataTable
    Function GetBillInvoiceDtls() As DataSet
    'modified by khusrao adil on 19-05-2017 for innoviti
    Function UpdateCreditInvoice(ByVal dsCreditInvoice As DataSet, Optional ByVal Billno As String = "", Optional ByVal SiteCode As String = "", Optional ByVal finyear As String = "", _
                                 Optional ByVal UserId As String = "", Optional ByVal DocumentType As String = "", Optional ByVal RetrievalReferenceNumber As String = "", _
                                 Optional ByVal TransactionTime As String = "", Optional PayByInnovitii As Boolean = False, Optional InnovatiiForTerminals As String = "", _
                                 Optional TerminalId As String = "", Optional AllowInnovitiPayment As Boolean = False) As Boolean 'modified by khusrao adil on 25-07-2018
    'Function UpdateCreditInvoice(ByVal dsCreditInvoice As DataSet) As Boolean
    '--- Added By Mahesh for getting All Adjustment Records Tendertype Wise 
    Function GetTotalCreditSaleAdjustmentbyDate(salesdate As Date, ByVal TerminalId As String) As DataTable
    '---For Shift
    Function GetTotalShiftCreditSaleAdjustmentbyDate(salesdate As Date, ByVal TerminalId As String, ByVal ShiftCreatedOn As DateTime) As DataTable
    '-- added for updating Home delivery - ashma 21 dec 2016
    Function GetHomeDelivery(ByVal Sitecode As String) As DataTable
End Interface
