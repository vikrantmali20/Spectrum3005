﻿Imports System.Text
Imports System.Data.SqlClient
Imports SpectrumCommon

Public Class ClsCreditSale
    Implements ICreditSale

    Dim Conn As SqlConnection
    Dim Trans As SqlTransaction
    Public Sub New()
        Conn = SpectrumCon()

    End Sub
    Private TenderType As String = "CREDIT"

    Public Function GetCreditCashMemo() As System.Collections.Generic.List(Of SalesInvoice) Implements ICreditSale.GetCreditCashMemo
        'Dim SqlStr = "SELECT A.BILLNO,A.SITECODE,A.FINYEAR,A.TENDERHEADCODE,A.AMOUNTTENDERED,ISNULL( A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED), A.AMOUNTTENDERED) AS BALANCEAMOUNT, A.CMRCPTTIME , D.CARDNO, D.NAMEONCARD, 'CASH MEMO' AS DOCTYPE FROM CASHMEMORECEIPT A LEFT JOIN CREDITRECEIPT B ON A.BILLNO =B.REFBILLNO AND A.SITECODE =B.SITECODE AND B.TYPECODE ='CM'  LEFT JOIN CASHMEMOHDR C ON A.BILLNO= C.BILLNO  AND A.SITECODE= C.SITECODE LEFT JOIN CLPCUSTOMERS D ON C.CLPNo= D.CARDNO WHERE A.TENDERHEADCODE='CREDITSALE' GROUP BY  A.BILLNO,A.SITECODE,A.FINYEAR,A.AMOUNTTENDERED,A.CMRCPTTIME,A.TENDERHEADCODE, D.CARDNO, D.NAMEONCARD , B.TYPECODE HAVING A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED)>0 OR A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED) IS NULL  ORDER BY CONVERT(DATETIME, CMRCPTTIME,102) DESC "

        Dim sbCreditCashMemo As New StringBuilder()
        sbCreditCashMemo.Append("SELECT A.BILLNO,A.SITECODE,A.FINYEAR,A.TENDERHEADCODE,A.AMOUNTTENDERED, " & vbCrLf)
        sbCreditCashMemo.Append("ISNULL( A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED), A.AMOUNTTENDERED) AS BALANCEAMOUNT, " & vbCrLf)
        sbCreditCashMemo.Append("A.CMRCPTTIME , ISNULL(D.CARDNO, F.CustomerNo) CARDNO, " & vbCrLf)
        sbCreditCashMemo.Append("ISNULL((D.FirstName + ' '+ D.SurName), (F.FirstName + ' '+ F.LastName)) NAMEONCARD, " & vbCrLf)
        sbCreditCashMemo.Append("'CASH MEMO' AS DOCTYPE, E.SalesPersonName " & vbCrLf)
        sbCreditCashMemo.Append("FROM CASHMEMORECEIPT A " & vbCrLf)
        sbCreditCashMemo.Append("LEFT JOIN CREDITRECEIPT B ON A.BILLNO =B.REFBILLNO " & vbCrLf)
        sbCreditCashMemo.Append("AND A.SITECODE =B.SITECODE AND B.TYPECODE ='CM' " & vbCrLf)
        sbCreditCashMemo.Append("LEFT JOIN CASHMEMOHDR C ON A.BILLNO= C.BILLNO  AND A.SITECODE= C.SITECODE " & vbCrLf)
        sbCreditCashMemo.Append("LEFT JOIN CLPCUSTOMERS D ON C.CLPNo= D.CARDNO " & vbCrLf)
        sbCreditCashMemo.Append("LEFT JOIN MstSalesPerson E ON C.SalesExecutiveCode = E.EmpCode " & vbCrLf)
        sbCreditCashMemo.Append("LEFT JOIN CustomerSaleOrder F ON C.CustomerNo= F.CustomerNo " & vbCrLf)
        sbCreditCashMemo.Append("WHERE A.status=1 and A.TenderTypeCode='" & TenderType & "' " & vbCrLf)
        sbCreditCashMemo.Append("GROUP BY  A.BILLNO,A.SITECODE,A.FINYEAR,A.AMOUNTTENDERED,A.CMRCPTTIME, " & vbCrLf)
        sbCreditCashMemo.Append("A.TENDERHEADCODE, D.CARDNO, D.NAMEONCARD , B.TYPECODE, E.SalesPersonName, " & vbCrLf)
        sbCreditCashMemo.Append("D.FirstName, D.SurName, F.CustomerNo, F.FirstName, F.LastName " & vbCrLf)
        sbCreditCashMemo.Append("HAVING A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED)>0 " & vbCrLf)
        sbCreditCashMemo.Append("OR A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED) IS NULL " & vbCrLf)
        sbCreditCashMemo.Append("ORDER BY CONVERT(DATETIME, CMRCPTTIME,102) DESC " & vbCrLf)

        If Not Conn.State = ConnectionState.Open Then
            Conn.Open()
        End If

        Trans = Conn.BeginTransaction("Creditsale")
        Dim sqlcmd As New SqlCommand(sbCreditCashMemo.ToString(), Conn, Trans)
        Dim rdr = sqlcmd.ExecuteReader()
        Dim CashmemoList As New List(Of SalesInvoice)

        While rdr.Read()
            Dim SI As New SalesInvoice()
            SI.InvoiceNumber = rdr("BillNo")
            SI.SiteCode = rdr("SiteCode")
            SI.FinYear = rdr("FinYear")
            SI.AmountInCurrency = rdr("AmountTendered")
            SI.RecTime = IIf(IsDBNull(rdr("CmRcptTime")), String.Empty, rdr("CmRcptTime"))
            SI.TenderHeadCode = IIf(IsDBNull(rdr("TenderHeadCode")), String.Empty, rdr("TenderHeadCode"))
            SI.CustomerID = IIf(IsDBNull(rdr("CardNo")), String.Empty, rdr("CardNo"))
            SI.CustomerName = IIf(IsDBNull(rdr("NameOnCard")), String.Empty, rdr("NameOnCard"))
            SI.BalanceAmt = IIf(IsDBNull(rdr("BALANCEAMOUNT")), String.Empty, rdr("BALANCEAMOUNT"))
            SI.DocType = IIf(IsDBNull(rdr("DocType")), String.Empty, rdr("DocType"))
            SI.SalesPerson = IIf(IsDBNull(rdr("SalesPersonName")), String.Empty, rdr("SalesPersonName"))
            CashmemoList.Add(SI)
        End While
        rdr.Close()
        Conn.Close()
        Return CashmemoList
    End Function

    Public Function GetCreditSalesOrder() As System.Collections.Generic.List(Of SalesInvoice) Implements ICreditSale.GetCreditSalesOrder
        Try
            'Dim SqlStr = "SELECT A.SALEINVNUMBER,A.SITECODE,c.SaleOrderNumber as DocumentNumber ,A.FINYEAR,A.TENDERHEADCODE,A.AMOUNTTENDERED,ISNULL(A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED),A.AMOUNTTENDERED) AS BALANCEAMOUNT, A.SOINVTIME , D.CARDNO, D.NAMEONCARD, 'SALES ORDER' AS DOCTYPE FROM SALESINVOICE A LEFT JOIN CREDITRECEIPT B ON A.SALEINVNUMBER =B.REFBILLNO AND A.SITECODE =B.SITECODE  AND B.TYPECODE ='SO' LEFT JOIN SalesOrderHdr C ON a.DocumentNumber = C.SaleOrderNumber  AND A.SITECODE= c.SITECODE LEFT JOIN CLPCUSTOMERS D ON C.CUSTOMERNO= D.CARDNO WHERE A.TENDERHEADCODE='CREDITSALE' GROUP BY  A.SALEINVNUMBER,A.SITECODE,A.FINYEAR,A.AMOUNTTENDERED,A.SOINVTIME,A.TENDERHEADCODE, D.CARDNO, D.NAMEONCARD , B.TYPECODE, c.SaleOrderNumber HAVING A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED)>0 OR A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED) IS NULL  ORDER BY CONVERT(DATETIME, SOINVTIME,102) DESC"
            Dim sbCreditSalesOrder As New StringBuilder()
            sbCreditSalesOrder.Append("SELECT A.SALEINVNUMBER,A.SITECODE,c.SaleOrderNumber as DocumentNumber , " & vbCrLf)
            sbCreditSalesOrder.Append("A.FINYEAR, A.TENDERHEADCODE, A.AMOUNTTENDERED, " & vbCrLf)
            sbCreditSalesOrder.Append("ISNULL(A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED),A.AMOUNTTENDERED) AS BALANCEAMOUNT, " & vbCrLf)
            sbCreditSalesOrder.Append("A.SOINVTIME , 'SALES ORDER' AS DOCTYPE, E.SalesPersonName, " & vbCrLf)
            sbCreditSalesOrder.Append("ISNULL(D.CARDNO, F.CustomerNo) CARDNO, ISNULL((D.FirstName + ' '+ D.SurName), (F.FirstName + ' '+ F.LastName)) NAMEONCARD " & vbCrLf)
            sbCreditSalesOrder.Append("FROM SALESINVOICE A " & vbCrLf)
            sbCreditSalesOrder.Append("LEFT JOIN CREDITRECEIPT B ON A.SALEINVNUMBER =B.REFBILLNO " & vbCrLf)
            sbCreditSalesOrder.Append("AND A.SITECODE =B.SITECODE  AND B.TYPECODE ='SO' " & vbCrLf)
            sbCreditSalesOrder.Append("LEFT JOIN SalesOrderHdr C ON a.DocumentNumber = C.SaleOrderNumber  AND A.SITECODE= c.SITECODE " & vbCrLf)
            sbCreditSalesOrder.Append("LEFT JOIN CLPCUSTOMERS D ON C.CUSTOMERNO= D.CARDNO " & vbCrLf)
            sbCreditSalesOrder.Append("LEFT JOIN MstSalesPerson E ON C.SalesExecutiveCode = E.EmpCode " & vbCrLf)
            sbCreditSalesOrder.Append("LEFT JOIN CustomerSaleOrder F ON C.CustomerNo= F.CustomerNo " & vbCrLf)
            sbCreditSalesOrder.Append("WHERE A.status=1 and A.TenderTypeCode='" & TenderType & "' " & vbCrLf)
            sbCreditSalesOrder.Append("GROUP BY  A.SALEINVNUMBER,A.SITECODE,A.FINYEAR,A.AMOUNTTENDERED, " & vbCrLf)
            sbCreditSalesOrder.Append("A.SOINVTIME, A.TENDERHEADCODE, D.CARDNO, D.FirstName, D.SurName, " & vbCrLf)
            sbCreditSalesOrder.Append("B.TYPECODE, c.SaleOrderNumber, E.SalesPersonName, F.CustomerNo, F.FirstName, F.LastName " & vbCrLf)
            sbCreditSalesOrder.Append("HAVING A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED)>0 OR A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED) IS NULL " & vbCrLf)
            sbCreditSalesOrder.Append("ORDER BY CONVERT(DATETIME, SOINVTIME,102) DESC " & vbCrLf)

            If Not Conn.State = ConnectionState.Open Then
                Conn.Open()
            End If

            Trans = Conn.BeginTransaction("Creditsale")
            Dim sqlcmd As New SqlCommand(sbCreditSalesOrder.ToString(), Conn, Trans)
            Dim rdr = sqlcmd.ExecuteReader()
            Dim SalesInvoiceList As New List(Of SalesInvoice)
            Try
                While rdr.Read()
                    Dim SI As New SalesInvoice()
                    SI.InvoiceNumber = rdr("SaleInvNumber")
                    SI.DocNo = rdr("DocumentNumber").ToString()
                    SI.SiteCode = rdr("SiteCode")
                    SI.FinYear = rdr("FinYear")
                    SI.AmountInCurrency = rdr("AmountTendered")
                    SI.RecTime = IIf(IsDBNull(rdr("SOInvTime")), String.Empty, rdr("SOInvTime"))
                    SI.TenderHeadCode = IIf(IsDBNull(rdr("TenderHeadCode")), String.Empty, rdr("TenderHeadCode"))
                    SI.CustomerID = IIf(IsDBNull(rdr("CardNO")), String.Empty, rdr("CardNO"))
                    SI.CustomerName = IIf(IsDBNull(rdr("NameOnCard")), String.Empty, rdr("NameOnCard"))
                    SI.BalanceAmt = IIf(IsDBNull(rdr("balanceamount")), String.Empty, rdr("balanceamount"))
                    SI.DocType = IIf(IsDBNull(rdr("DOCTYPE")), String.Empty, rdr("DOCTYPE"))
                    SI.SalesPerson = IIf(IsDBNull(rdr("SalesPersonName")), String.Empty, rdr("SalesPersonName"))
                    SalesInvoiceList.Add(SI)
                End While
            Catch ex As Exception
                rdr.Close()
                Trans.Rollback()
            End Try
            rdr.Close()
            Conn.Close()
            Return SalesInvoiceList
        Catch ex As Exception
            Conn.Close()
            Trans.Rollback()
        End Try
    End Function

    Public Function GetCustomers() As System.Collections.Generic.List(Of CustomerInfo) Implements ICreditSale.GetCustomers
        Dim sbCreditCustomer As New StringBuilder()
        sbCreditCustomer.Append("SELECT CardNo, FirstName, MiddleName, SurName, Gender, " & vbCrLf)
        sbCreditCustomer.Append("BirthDate, EmailId, MobileNo " & vbCrLf)
        sbCreditCustomer.Append("FROM CLPCustomers WHERE STATUS = 1 " & vbCrLf)
        sbCreditCustomer.Append("UNION ALL " & vbCrLf)
        sbCreditCustomer.Append("SELECT CustomerNo CardNo, FirstName, MiddleName, LastName SurName, Gender, " & vbCrLf)
        sbCreditCustomer.Append("DateofBirth BirthDate, EmailId, MobilePhone MobileNo " & vbCrLf)
        sbCreditCustomer.Append("FROM CustomerSaleOrder WHERE STATUS = 1 " & vbCrLf)

        If Not Conn.State = ConnectionState.Open Then
            Conn.Open()
        End If
        Trans = Conn.BeginTransaction("Creditsale")
        Dim cmd As New SqlCommand(sbCreditCustomer.ToString(), Conn, Trans)
        Dim CustomerList As New List(Of CustomerInfo)
        Dim rdr = cmd.ExecuteReader()
        While rdr.Read()
            Try
                Dim customer As New CustomerInfo()
                customer.CardNumber = rdr("CardNo").ToString()
                customer.FirstName = rdr("FirstName").ToString()
                customer.MiddleName = rdr("MiddleName").ToString()
                customer.LastName = rdr("SurName").ToString()
                customer.Gender = rdr("Gender").ToString()
                customer.BirthDate = IIf(rdr("BirthDate") IsNot DBNull.Value, rdr("BirthDate"), DateTime.MinValue)
                customer.MobileNo = rdr("MobileNo").ToString()
                customer.EmailId = rdr("EmailId").ToString()
                CustomerList.Add(customer)
            Catch ex As Exception
                LogException(ex)
            End Try
        End While
        rdr.Close()
        Conn.Close()
        Return CustomerList
    End Function

    ' Public Function UpdateCredit(SalesInvoice As List(Of SalesInvoice), ByVal DayOpenDate As DateTime) As Boolean Implements ICreditSale.UpdateCredit
    Public Function UpdateCredit(SalesInvoice As List(Of SalesInvoice), ByVal DayOpenDate As DateTime, Optional ByVal Billno As String = "", _
                                 Optional ByVal SiteCode As String = "", Optional ByVal finyear As String = "", Optional ByVal UserId As String = "", _
                                 Optional ByVal DocumentType As String = "", Optional ByVal RetrievalReferenceNumber As String = "", _
                                 Optional ByVal TransactionTime As String = "", Optional PayByInnovitii As Boolean = False, Optional InnovatiiForTerminals As String = "", _
                                 Optional TerminalId As String = "", Optional dtInnoviti As DataTable = Nothing, _
                                  Optional InnovitiPaymentEnable As Boolean = False) As Boolean Implements ICreditSale.UpdateCredit
        Try
            If Not Conn.State = ConnectionState.Open Then
                Conn.Open()
            End If
            Dim Amount As String = String.Empty
            Dim clsCommonObj As New clsCommon()
            Trans = Conn.BeginTransaction("CreditSale")
            Dim sqlcmd As New SqlCommand("", Conn, Trans)
            sqlcmd.Parameters.Add("@RecTime", SqlDbType.Date)
            sqlcmd.Parameters("@RecTime").Value = DayOpenDate
            For Each inv As SalesInvoice In SalesInvoice

                Dim sqlstr = "INSERT INTO CreditReceipt(BillNo,SiteCode,FinYear,RefBillNo,TypeCode,CMRecptLineno,TerminalID,CardNo,BankAccNO,ExchangeRate,TenderTypeCode,TenderHeadCode,AmountTendered,CurrencyCode,AmountinCurrency,CmRcptDateTime,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS,RefBillInvNumber,Remark)" & _
                                                        "Values('" & inv.BillNO & "','" & inv.SiteCode & "','" & inv.FinYear & "','" & inv.InvoiceNumber & "','" & inv.DocType & "','" & inv.LineNo & "','" & inv.TerminalID & "','" & inv.CardNO & "','" & inv.BankNO & "'," & _
                                                        "'" & inv.ExchangeRate & "','" & inv.TenderTypeCode & "','" & inv.TenderHeadCode & "','" & inv.AmountTendered & "','" + inv.CurrencyCode + "','" & inv.AmountInCurrency & "',@RecTime,'" & inv.CreatedAt & "','" & inv.CreatedBy & "',GetDate(),'" & inv.UpdatedAt & "','" & inv.UpdatedBy & "',GetDate(),'" & inv.Status & "','" & inv.RefBillInvNumber & "','" & inv.Remark & "')"

                sqlcmd.CommandText = sqlstr
                If Not sqlcmd.ExecuteNonQuery() > 0 Then
                    Trans.Rollback()
                    Conn.Close()
                    Return False
                End If
                '---------------------------------------------section was comemented -------------------------------------
                'If PayByInnovitii AndAlso InnovatiiForTerminals.Contains(TerminalId) Then
                '    If inv.TenderTypeCode.ToUpper = "CreditCard".ToUpper Then
                '        ' terminalId = inv.TerminalID
                '        Amount = inv.AmountTendered
                '    End If
                'End If
                '-------------------------------------------------------------
            Next
            '----------------------------------------------section was comemented -----------------------------------------
            If PayByInnovitii AndAlso InnovatiiForTerminals.Contains(TerminalId) AndAlso InnovitiPaymentEnable = True Then
                If Amount <> String.Empty Then
                    If Not clsCommonObj.SaveInnovitiData(Billno, TerminalId, Amount, SiteCode, finyear, UserId, DocumentType, RetrievalReferenceNumber, TransactionTime, Trans) Then
                        Trans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                    If clsCommonObj.UpdateDocumentNo("TransInnoviti", SpectrumCon, Trans) = False Then
                        Trans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If
            End If
            '-----------------------------------------------
            Dim comm As New clsCommon()
            comm.UpdateDocumentNo("SalesInvoice", Conn, Trans, , "FO_DOC")
            Trans.Commit()
            Conn.Close()

            Return True
        Catch ex As Exception
            LogException(ex)
            Trans.Rollback()
            Conn.Close()
            Return False
        End Try
    End Function

    Public Function getBillbyCustomer(customerID As String) As System.Collections.Generic.List(Of SpectrumCommon.SalesInvoice) Implements ICreditSale.getBillbyCustomer
        'Dim sqlstr = "SELECT CONVERT(DATETIME, CMRCPTTIME,101) AS SORTER, A.BILLNO as BillNO,A.SITECODE,'' as DocumentNumber,A.FINYEAR,A.TENDERHEADCODE,A.AMOUNTTENDERED,ISNULL( A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED), A.AMOUNTTENDERED) AS BALANCEAMOUNT, A.CMRCPTTIME , D.CARDNO, D.NAMEONCARD, 'CASH MEMO' AS DOCTYPE FROM CASHMEMORECEIPT A LEFT JOIN CREDITRECEIPT B ON A.BILLNO =B.REFBILLNO AND A.SITECODE =B.SITECODE AND B.TYPECODE ='CM'  LEFT JOIN CASHMEMOHDR C ON A.BILLNO= C.BILLNO  AND A.SITECODE= C.SITECODE LEFT JOIN CLPCUSTOMERS D ON C.CLPNO= D.CARDNO WHERE A.TENDERHEADCODE='CREDITSALE' and d.CardNo='" + customerID + "' GROUP BY  A.BILLNO,A.SITECODE,A.FINYEAR,A.AMOUNTTENDERED,A.CMRCPTTIME,A.TENDERHEADCODE, D.CARDNO, D.NAMEONCARD , B.TYPECODE HAVING (A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED)>0 OR A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED) IS NULL) " & _
        '    "Union " & _
        '    "SELECT CONVERT(DATETIME, SOINVTIME,101) AS SORTER, A.SALEINVNUMBER,A.SITECODE,c.SaleOrderNumber as DocumentNumber ,A.FINYEAR,A.TENDERHEADCODE,A.AMOUNTTENDERED,ISNULL(A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED),A.AMOUNTTENDERED) AS BALANCEAMOUNT, A.SOINVTIME , D.CARDNO, D.NAMEONCARD, 'SALES ORDER' AS DOCTYPE FROM SALESINVOICE A LEFT JOIN CREDITRECEIPT B ON A.SALEINVNUMBER =B.REFBILLNO AND A.SITECODE =B.SITECODE  AND B.TYPECODE ='SO' LEFT JOIN SalesOrderHdr C ON a.DocumentNumber = C.SaleOrderNumber  AND A.SITECODE= c.SITECODE LEFT JOIN CLPCUSTOMERS D ON C.CUSTOMERNO= D.CARDNO WHERE A.TENDERHEADCODE='CREDITSALE' and d.CardNo='" + customerID + "'  GROUP BY  A.SALEINVNUMBER,A.SITECODE,A.FINYEAR,A.AMOUNTTENDERED,A.SOINVTIME,A.TENDERHEADCODE, D.CARDNO, D.NAMEONCARD , B.TYPECODE, c.SaleOrderNumber HAVING A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED)>0 OR A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED) IS NULL ORDER BY SORTER DESC"
        Dim sbCreditCustomer As New StringBuilder()
        sbCreditCustomer.Append("SELECT CONVERT(DATETIME, CMRCPTTIME,101) AS SORTER, A.BILLNO as BillNO, " & vbCrLf)
        sbCreditCustomer.Append("A.SITECODE,'' as DocumentNumber,A.FINYEAR,A.TENDERHEADCODE,A.AMOUNTTENDERED, " & vbCrLf)
        sbCreditCustomer.Append("ISNULL( A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED), A.AMOUNTTENDERED) AS BALANCEAMOUNT, " & vbCrLf)
        sbCreditCustomer.Append("A.CMRCPTTIME, 'CASH MEMO' AS DOCTYPE, F.SalesPersonName, " & vbCrLf)
        sbCreditCustomer.Append("ISNULL(D.CARDNO, E.CustomerNo) CARDNO, ISNULL((D.FirstName + ' '+ D.SurName), (E.FirstName + ' '+ E.LastName)) NAMEONCARD " & vbCrLf)
        sbCreditCustomer.Append("FROM CASHMEMORECEIPT A " & vbCrLf)
        sbCreditCustomer.Append("LEFT JOIN CREDITRECEIPT B ON A.BILLNO =B.REFBILLNO AND A.SITECODE =B.SITECODE AND B.TYPECODE ='CM' " & vbCrLf)
        sbCreditCustomer.Append("LEFT JOIN CASHMEMOHDR C ON A.BILLNO= C.BILLNO  AND A.SITECODE= C.SITECODE " & vbCrLf)
        sbCreditCustomer.Append("LEFT JOIN CLPCUSTOMERS D ON C.CLPNO= D.CARDNO " & vbCrLf)
        sbCreditCustomer.Append("LEFT JOIN CustomerSaleOrder E ON C.CustomerNo= E.CustomerNo " & vbCrLf)
        sbCreditCustomer.Append("LEFT JOIN MstSalesPerson F ON C.SalesExecutiveCode = F.EmpCode " & vbCrLf)
        sbCreditCustomer.Append("WHERE A.status=1 and A.TenderTypeCode='" & TenderType & "' AND (d.CardNo='" + customerID + "' OR E.CustomerNo ='" + customerID + "') " & vbCrLf)
        sbCreditCustomer.Append("GROUP BY A.BILLNO, A.SITECODE, A.FINYEAR, A.AMOUNTTENDERED, A.CMRCPTTIME, A.TENDERHEADCODE, F.SalesPersonName, " & vbCrLf)
        sbCreditCustomer.Append("D.CARDNO, D.FirstName, D.SurName, E.CustomerNo, E.FirstName, E.LastName, B.TYPECODE " & vbCrLf)
        sbCreditCustomer.Append("HAVING (A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED)>0 OR A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED) IS NULL) " & vbCrLf)
        sbCreditCustomer.Append("UNION ALL " & vbCrLf)
        sbCreditCustomer.Append("SELECT CONVERT(DATETIME, SOINVTIME,101) AS SORTER, A.SALEINVNUMBER,A.SITECODE, " & vbCrLf)
        sbCreditCustomer.Append("c.SaleOrderNumber as DocumentNumber ,A.FINYEAR,A.TENDERHEADCODE,A.AMOUNTTENDERED, " & vbCrLf)
        sbCreditCustomer.Append("ISNULL(A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED),A.AMOUNTTENDERED) AS BALANCEAMOUNT, " & vbCrLf)
        sbCreditCustomer.Append("A.SOINVTIME , 'SALES ORDER' AS DOCTYPE, F.SalesPersonName, " & vbCrLf)
        sbCreditCustomer.Append("ISNULL(D.CARDNO, E.CustomerNo) CARDNO, ISNULL((D.FirstName + ' '+ D.SurName), (E.FirstName + ' '+ E.LastName)) NAMEONCARD " & vbCrLf)
        sbCreditCustomer.Append("FROM SALESINVOICE A " & vbCrLf)
        sbCreditCustomer.Append("LEFT JOIN CREDITRECEIPT B ON A.SALEINVNUMBER =B.REFBILLNO AND A.SITECODE =B.SITECODE  AND B.TYPECODE ='SO' " & vbCrLf)
        sbCreditCustomer.Append("LEFT JOIN SalesOrderHdr C ON a.DocumentNumber = C.SaleOrderNumber  AND A.SITECODE= c.SITECODE " & vbCrLf)
        sbCreditCustomer.Append("LEFT JOIN CLPCUSTOMERS D ON C.CUSTOMERNO= D.CARDNO " & vbCrLf)
        sbCreditCustomer.Append("LEFT JOIN CustomerSaleOrder E ON C.CustomerNo= E.CustomerNo " & vbCrLf)
        sbCreditCustomer.Append("LEFT JOIN MstSalesPerson F ON C.SalesExecutiveCode = F.EmpCode " & vbCrLf)
        sbCreditCustomer.Append("WHERE A.status=1 and A.TenderTypeCode='" & TenderType & "' AND (D.CardNo='" + customerID + "' OR E.CustomerNo ='" + customerID + "') " & vbCrLf)
        sbCreditCustomer.Append("GROUP BY  A.SALEINVNUMBER,A.SITECODE,A.FINYEAR,A.AMOUNTTENDERED,A.SOINVTIME,A.TENDERHEADCODE, F.SalesPersonName, " & vbCrLf)
        sbCreditCustomer.Append("D.CARDNO, D.FirstName, D.SurName, E.CustomerNo, E.FirstName, E.LastName, B.TYPECODE, c.SaleOrderNumber " & vbCrLf)
        sbCreditCustomer.Append("HAVING A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED)>0 OR A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED) IS NULL " & vbCrLf)
        sbCreditCustomer.Append("ORDER BY SORTER DESC " & vbCrLf)

        If Not Conn.State = ConnectionState.Open Then
            Conn.Open()
        End If
        Trans = Conn.BeginTransaction("Creditsale")
        Dim SalesList As New List(Of SalesInvoice)
        Dim cmd As New SqlCommand(sbCreditCustomer.ToString(), Conn, Trans)
        Dim rdr = cmd.ExecuteReader()
        While rdr.Read()
            Try
                Dim Invoice As New SalesInvoice()
                Invoice.AmountInCurrency = rdr("AMOUNTTENDERED").ToString()
                Invoice.BalanceAmt = rdr("BALANCEAMOUNT").ToString()
                Invoice.DocNo = rdr("DocumentNumber").ToString()
                Invoice.FinYear = rdr("FinYear").ToString()
                Invoice.InvoiceNumber = rdr("BillNO").ToString()
                Invoice.SiteCode = rdr("SiteCode").ToString()
                Invoice.TenderHeadCode = rdr("TenderHeadCode").ToString()
                Invoice.DocType = rdr("doctype").ToString()
                Invoice.RecTime = rdr("CMRCPTTIME").ToString()
                Invoice.CustomerName = rdr("NameOnCard").ToString()
                Invoice.CustomerID = rdr("CardNo").ToString()
                Invoice.SalesPerson = rdr("SalesPersonName").ToString()
                SalesList.Add(Invoice)

            Catch ex As Exception
                LogException(ex)
            End Try
        End While
        rdr.Close()
        Conn.Close()
        Return SalesList
    End Function

    Public Function GetTotalCreditSalebyDate(salesdate As Date) As Decimal Implements ICreditSale.GetTotalCreditSalebyDate
        Try
            Dim SqlStr = "select SUM(AmountTendered) from CreditReceipt where CONVERT(datetime,CmRcptDateTime,101)='" + salesdate.ToString("yyyy-MM-dd") + "'"
            If Not Conn.State = ConnectionState.Open Then
                Conn.Open()
            End If
            Trans = Conn.BeginTransaction("Creditsale")
            Dim cmd As New SqlCommand(SqlStr, Conn, Trans)
            Dim amt As New Decimal
            amt = CDec(If(cmd.ExecuteScalar() Is DBNull.Value, 0, cmd.ExecuteScalar()))
            Trans.Commit()
            Conn.Close()
            Return amt
        Catch ex As Exception
            Trans.Rollback()
            Conn.Close()
            Return 0
        End Try
    End Function

    Public Function GetCreditBillByDeliveryPerson() As System.Collections.Generic.List(Of SalesInvoice) Implements ICreditSale.GetCreditBillByDeliveryPerson
        'Dim SqlStr = "SELECT A.BILLNO,A.SITECODE,A.FINYEAR,A.TENDERHEADCODE,A.AMOUNTTENDERED,ISNULL( A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED), A.AMOUNTTENDERED) AS BALANCEAMOUNT, A.CMRCPTTIME , D.CARDNO, D.NAMEONCARD, 'CASH MEMO' AS DOCTYPE FROM CASHMEMORECEIPT A LEFT JOIN CREDITRECEIPT B ON A.BILLNO =B.REFBILLNO AND A.SITECODE =B.SITECODE AND B.TYPECODE ='CM'  LEFT JOIN CASHMEMOHDR C ON A.BILLNO= C.BILLNO  AND A.SITECODE= C.SITECODE LEFT JOIN CLPCUSTOMERS D ON C.CLPNo= D.CARDNO WHERE A.TENDERHEADCODE='CREDITSALE' GROUP BY  A.BILLNO,A.SITECODE,A.FINYEAR,A.AMOUNTTENDERED,A.CMRCPTTIME,A.TENDERHEADCODE, D.CARDNO, D.NAMEONCARD , B.TYPECODE HAVING A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED)>0 OR A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED) IS NULL  ORDER BY CONVERT(DATETIME, CMRCPTTIME,102) DESC "

        Dim sbCreditByDeliveryPerson As New StringBuilder()
        sbCreditByDeliveryPerson.Append("SELECT A.BILLNO, A.BILLNO DocumentNumber, A.SITECODE,A.FINYEAR,A.TENDERHEADCODE,A.AMOUNTTENDERED, " & vbCrLf)
        sbCreditByDeliveryPerson.Append("ISNULL( A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED), A.AMOUNTTENDERED) AS BALANCEAMOUNT, " & vbCrLf)
        sbCreditByDeliveryPerson.Append("A.CMRCPTTIME BILLDATE , 'CASH MEMO' AS DOCTYPE, E.SalesPersonName, " & vbCrLf)
        sbCreditByDeliveryPerson.Append("ISNULL(D.CARDNO, F.CustomerNo) CARDNO, ISNULL((D.FirstName + ' '+ D.SurName), (F.FirstName + ' '+ F.LastName)) NAMEONCARD " & vbCrLf)
        sbCreditByDeliveryPerson.Append("FROM CASHMEMORECEIPT A " & vbCrLf)
        sbCreditByDeliveryPerson.Append("LEFT JOIN CREDITRECEIPT B ON A.BILLNO =B.REFBILLNO " & vbCrLf)
        sbCreditByDeliveryPerson.Append("AND A.SITECODE =B.SITECODE AND B.TYPECODE ='CM' " & vbCrLf)
        sbCreditByDeliveryPerson.Append("LEFT JOIN CASHMEMOHDR C ON A.BILLNO= C.BILLNO  AND A.SITECODE= C.SITECODE " & vbCrLf)
        sbCreditByDeliveryPerson.Append("LEFT JOIN CLPCUSTOMERS D ON C.CLPNo= D.CARDNO " & vbCrLf)
        sbCreditByDeliveryPerson.Append("LEFT JOIN MstSalesPerson E ON C.SalesExecutiveCode = E.EmpCode " & vbCrLf)
        sbCreditByDeliveryPerson.Append("LEFT JOIN CustomerSaleOrder F ON C.CustomerNo= F.CustomerNo " & vbCrLf)
        sbCreditByDeliveryPerson.Append("WHERE A.status=1 and A.TenderTypeCode='" & TenderType & "' AND E.SalesPersonName IS NOT NULL " & vbCrLf)
        sbCreditByDeliveryPerson.Append("GROUP BY  A.BILLNO,A.SITECODE,A.FINYEAR,A.AMOUNTTENDERED,A.CMRCPTTIME, " & vbCrLf)
        sbCreditByDeliveryPerson.Append("A.TENDERHEADCODE, D.CARDNO, D.FirstName, D.SurName, F.CustomerNo, F.FirstName, F.LastName, B.TYPECODE, E.SalesPersonName " & vbCrLf)
        sbCreditByDeliveryPerson.Append("HAVING A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED)>0 " & vbCrLf)
        sbCreditByDeliveryPerson.Append("OR A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED) IS NULL " & vbCrLf)
        sbCreditByDeliveryPerson.Append("UNION ALL " & vbCrLf)
        sbCreditByDeliveryPerson.Append("SELECT A.SALEINVNUMBER BILLNO, c.SaleOrderNumber as DocumentNumber , A.SITECODE, " & vbCrLf)
        sbCreditByDeliveryPerson.Append("A.FINYEAR, A.TENDERHEADCODE, A.AMOUNTTENDERED, " & vbCrLf)
        sbCreditByDeliveryPerson.Append("ISNULL(A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED),A.AMOUNTTENDERED) AS BALANCEAMOUNT, " & vbCrLf)
        sbCreditByDeliveryPerson.Append("A.SOINVTIME BILLDATE, 'SALES ORDER' AS DOCTYPE, E.SalesPersonName, " & vbCrLf)
        sbCreditByDeliveryPerson.Append("ISNULL(D.CARDNO, F.CustomerNo) CARDNO, ISNULL((D.FirstName + ' '+ D.SurName), (F.FirstName + ' '+ F.LastName)) NAMEONCARD " & vbCrLf)
        sbCreditByDeliveryPerson.Append("FROM SALESINVOICE A " & vbCrLf)
        sbCreditByDeliveryPerson.Append("LEFT JOIN CREDITRECEIPT B ON A.SALEINVNUMBER =B.REFBILLNO " & vbCrLf)
        sbCreditByDeliveryPerson.Append("AND A.SITECODE =B.SITECODE  AND B.TYPECODE ='SO' " & vbCrLf)
        sbCreditByDeliveryPerson.Append("LEFT JOIN SalesOrderHdr C ON a.DocumentNumber = C.SaleOrderNumber  AND A.SITECODE= c.SITECODE " & vbCrLf)
        sbCreditByDeliveryPerson.Append("LEFT JOIN CLPCUSTOMERS D ON C.CUSTOMERNO= D.CARDNO " & vbCrLf)
        sbCreditByDeliveryPerson.Append("LEFT JOIN MstSalesPerson E ON C.SalesExecutiveCode = E.EmpCode " & vbCrLf)
        sbCreditByDeliveryPerson.Append("LEFT JOIN CustomerSaleOrder F ON C.CustomerNo= F.CustomerNo " & vbCrLf)
        sbCreditByDeliveryPerson.Append("WHERE A.status=1 and A.TenderTypeCode='" & TenderType & "' AND E.SalesPersonName IS NOT NULL " & vbCrLf)
        sbCreditByDeliveryPerson.Append("GROUP BY  A.SALEINVNUMBER,A.SITECODE,A.FINYEAR,A.AMOUNTTENDERED, " & vbCrLf)
        sbCreditByDeliveryPerson.Append("A.SOINVTIME, A.TENDERHEADCODE, D.CARDNO, D.FirstName, D.SurName, F.CustomerNo, F.FirstName, F.LastName, " & vbCrLf)
        sbCreditByDeliveryPerson.Append("B.TYPECODE, c.SaleOrderNumber, E.SalesPersonName " & vbCrLf)
        sbCreditByDeliveryPerson.Append("HAVING A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED)>0 OR A.AMOUNTTENDERED-SUM(B.AMOUNTTENDERED) IS NULL " & vbCrLf)

        If Not Conn.State = ConnectionState.Open Then
            Conn.Open()
        End If

        Trans = Conn.BeginTransaction("Creditsale")
        Dim sqlcmd As New SqlCommand(sbCreditByDeliveryPerson.ToString(), Conn, Trans)
        Dim rdr = sqlcmd.ExecuteReader()
        Dim CashmemoList As New List(Of SalesInvoice)

        While rdr.Read()
            Dim SI As New SalesInvoice()
            SI.InvoiceNumber = rdr("BillNo")
            SI.SiteCode = rdr("SiteCode")
            SI.FinYear = rdr("FinYear")
            SI.AmountInCurrency = rdr("AmountTendered")
            SI.RecTime = IIf(IsDBNull(rdr("BillDate")), String.Empty, rdr("BillDate"))
            SI.TenderHeadCode = IIf(IsDBNull(rdr("TenderHeadCode")), String.Empty, rdr("TenderHeadCode"))
            SI.CustomerID = IIf(IsDBNull(rdr("CardNo")), String.Empty, rdr("CardNo"))
            SI.CustomerName = IIf(IsDBNull(rdr("NameOnCard")), String.Empty, rdr("NameOnCard"))
            SI.BalanceAmt = IIf(IsDBNull(rdr("BALANCEAMOUNT")), String.Empty, rdr("BALANCEAMOUNT"))
            SI.DocType = IIf(IsDBNull(rdr("DocType")), String.Empty, rdr("DocType"))
            SI.SalesPerson = IIf(IsDBNull(rdr("SalesPersonName")), String.Empty, rdr("SalesPersonName"))
            CashmemoList.Add(SI)
        End While
        rdr.Close()
        Conn.Close()
        Return CashmemoList
    End Function

    Public Function GetCreditSales(ByVal Sitecode As String) As DataTable Implements ICreditSale.GetCreditSales
        GetCreditSales = Nothing
        Dim daCM As SqlDataAdapter
        Dim dtTemp As New DataTable
        Dim objCmd = SpectrumCon.CreateCommand()
        objCmd.CommandTimeout = 0
        Try
            daCM = New SqlDataAdapter(String.Format("EXEC GetCreditSalesDetails '{0}'", Sitecode), SpectrumCon)
            daCM.Fill(dtTemp)
            dtTemp.TableName = "CreditSales"

            Return dtTemp
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function

    '--added for Home delivery load proc for filling grid with all values without filter ashma 21dec 2016
    Public Function GetHomeDelivery(ByVal Sitecode As String) As DataTable Implements ICreditSale.GetHomeDelivery
        GetHomeDelivery = Nothing
        Dim daCM As SqlDataAdapter
        Dim dtTemp As New DataTable
        Dim objCmd = SpectrumCon.CreateCommand()
        objCmd.CommandTimeout = 0
        Try
            daCM = New SqlDataAdapter(String.Format("EXEC getHomeDeliveryOrders '{0}'", Sitecode), SpectrumCon)
            daCM.Fill(dtTemp)
            dtTemp.TableName = "HomeDelivery"

            Return dtTemp
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetOnlineOrders(ByVal Sitecode As String) As DataTable
        Dim GetHomeDelivery = Nothing
        Dim daCM As SqlDataAdapter
        Dim dtTemp As New DataTable
        Dim objCmd = SpectrumCon.CreateCommand()
        objCmd.CommandTimeout = 0
        Try
            daCM = New SqlDataAdapter(String.Format("EXEC getOnlineOrders '{0}'", Sitecode), SpectrumCon)
            daCM.Fill(dtTemp)
            dtTemp.TableName = "HomeDelivery"

            Return dtTemp
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    Public Function GetBillInvoiceDtls() As DataSet Implements ICreditSale.GetBillInvoiceDtls

        Dim daInvoice As SqlDataAdapter
        Dim dsInvoice As New DataSet
        Try
            daInvoice = New SqlDataAdapter("SELECT * FROM CreditReceipt WHERE 1=2", SpectrumCon)
            daInvoice.Fill(dsInvoice)
            dsInvoice.Tables(0).TableName = "CreditReceipt"

            Return dsInvoice
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
    'added for Innoviti
    Public Function UpdateCreditInvoice(ByVal dsCreditInvoice As DataSet, Optional ByVal Billno As String = "", Optional ByVal SiteCode As String = "", _
                                        Optional ByVal finyear As String = "", Optional ByVal UserId As String = "", Optional ByVal DocumentType As String = "", _
                                        Optional ByVal RetrievalReferenceNumber As String = "", Optional ByVal TransactionTime As String = "", _
                                        Optional PayByInnovitii As Boolean = False, Optional InnovatiiForTerminals As String = "", Optional TerminalId As String = "", _
                                        Optional AllowInnovitiPayment As Boolean = False) As Boolean Implements ICreditSale.UpdateCreditInvoice 'modified by khusrao adil on 27-07-2018
        'Public Function UpdateCreditInvoice(ByVal dsCreditInvoice As DataSet) As Boolean Implements ICreditSale.UpdateCreditInvoice
        Try
            Dim clsCommon As New clsCommon()

            If Not Conn.State = ConnectionState.Open Then
                Conn.Open()
            End If
            Trans = Conn.BeginTransaction("CreditSale")
            Dim objType = "FO_DOC"
            If Not clsCommon.SaveData(dsCreditInvoice, SpectrumCon, Trans) Then
                Trans.Rollback()
                CloseConnection()
                Return False

            End If
            'Added by sagar for innovitii payment
            'added by khusrao adil on 05-07-2017 for innovati
            'modified by khusrao adil on 27-07-2018
            If PayByInnovitii AndAlso InnovatiiForTerminals.Contains(TerminalId) AndAlso AllowInnovitiPayment = True Then
                Dim row = dsCreditInvoice.Tables(0).Select("TenderTypeCode='CreditCard'")
                If row.Length > 0 Then
                    If Not clsCommon.SaveInnovitiData(Billno, dsCreditInvoice.Tables("creditreceipt")(0)("TerminalId").ToString, dsCreditInvoice.Tables("creditreceipt")(0)("AmountTendered").ToString, SiteCode, finyear, UserId, DocumentType, RetrievalReferenceNumber, TransactionTime, Trans) Then
                        Trans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                    If clsCommon.UpdateDocumentNo("TransInnoviti", SpectrumCon, Trans) = False Then
                        Trans.Rollback()
                        CloseConnection()
                        Return False
                    End If
                End If
            End If
            If Not clsCommon.UpdateDocumentNo("SalesInvoice", Conn, Trans, , objType) Then
                Trans.Rollback()
                CloseConnection()
                Return False
            End If
            Trans.Commit()
            Conn.Close()
            Return True
        Catch ex As Exception
            Trans.Rollback()
            Conn.Close()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Added By Mahesh for Calculate AmountTendered each Tender Type
    ''' </summary>
    ''' <param name="salesdate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function GetTotalCreditSaleAdjustmentbyDate(salesdate As Date, ByVal TerminalId As String) As DataTable Implements ICreditSale.GetTotalCreditSaleAdjustmentbyDate
        Dim dtCreditSaleAdjustment As New DataTable
        Try
            Dim SqlStr = "select TenderTypeCode as TENDERTYPE, TenderHeadCode + ' Adjustment' AS TenderHeadCode , 'Credit Adjustment'  AS DESCRIPTION, " & _
                              " SUM(AmountTendered) AS AMOUNTTENDERED, CONVERT(BIT, 0) AS ISSUED " & _
                              " from CreditReceipt where TerminalID = '" & TerminalId & "' AND CONVERT(datetime,CmRcptDateTime,101)='" + salesdate.ToString("yyyy-MM-dd") + "'" & _
                              " AND RefBillNo NOT IN( select BillNo from CashMemoHdr where billintermediatestatus='Deleted') " & _
                              " Group BY TenderTypeCode, TenderHeadCode   "
            'If Not Conn.State = ConnectionState.Open Then
            '    Conn.Open()
            'End If
            ''Trans = Conn.BeginTransaction("Creditsale")
            'Dim cmd As New SqlCommand(SqlStr, Conn, Trans)
            'Dim amt As New Decimal
            Dim daDenom As New SqlDataAdapter(SqlStr, ConString)
            Dim dtdenom As New DataTable
            daDenom.Fill(dtCreditSaleAdjustment)
            GetTotalCreditSaleAdjustmentbyDate = dtCreditSaleAdjustment
            'Return dtdenom
            'Trans.Commit()
            'Conn.Close()
            'Return amt
        Catch ex As Exception
            Return dtCreditSaleAdjustment
        End Try
    End Function

    '--------------------Function for shift
    Public Function GetTotalShiftCreditSaleAdjustmentbyDate(salesdate As Date, ByVal TerminalId As String, ByVal ShiftCreatedOn As DateTime) As DataTable Implements ICreditSale.GetTotalShiftCreditSaleAdjustmentbyDate
        Dim dtCreditSaleAdjustment As New DataTable
        Try
            Dim SqlStr = "select TenderTypeCode as TENDERTYPE, TenderHeadCode + ' Adjustment' AS TenderHeadCode , 'Credit Adjustment'  AS DESCRIPTION, " & _
                              " SUM(AmountTendered) AS AMOUNTTENDERED, CONVERT(BIT, 0) AS ISSUED " & _
                              " from CreditReceipt where TerminalID = '" & TerminalId & "' And CREATEDON > @ShiftCreatedOn AND CONVERT(datetime,CmRcptDateTime,101)='" + salesdate.ToString("yyyy-MM-dd") + "'" & _
                              " AND RefBillNo NOT IN( select BillNo from CashMemoHdr where billintermediatestatus='Deleted') " & _
                              " Group BY TenderTypeCode, TenderHeadCode   "
            'If Not Conn.State = ConnectionState.Open Then
            '    Conn.Open()
            'End If
            ''Trans = Conn.BeginTransaction("Creditsale")
            'Dim cmd As New SqlCommand(SqlStr, Conn, Trans)
            'Dim amt As New Decimal
            Dim cmd As New SqlCommand(SqlStr, SpectrumCon())
            cmd.Parameters.Add("@ShiftCreatedOn", SqlDbType.DateTime)
            cmd.Parameters("@ShiftCreatedOn").Value = ShiftCreatedOn
            Dim daDenom As New SqlDataAdapter(cmd)
            Dim dtdenom As New DataTable
            daDenom.Fill(dtCreditSaleAdjustment)
            GetTotalShiftCreditSaleAdjustmentbyDate = dtCreditSaleAdjustment
            'Return dtdenom
            'Trans.Commit()
            'Conn.Close()
            'Return amt
        Catch ex As Exception
            Return dtCreditSaleAdjustment
        End Try
    End Function

    ''' <summary>
    ''' Get CashMemo Data in DataTable
    ''' </summary>
    ''' <param name="Billno"></param>
    ''' <param name="Sitecode"></param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>

    Public Function GetHomeDeliveryDataZomato(ByVal Billno As String, ByVal Sitecode As String, Optional ByVal Partner As String = "0") As DataTable
        Dim dtCreditSaleAdjustment As New DataTable
        Try
            Dim SqlStr = "select * from DPACashMemoHdr where BillNo='" & Billno & "' AND Sitecode='" & Sitecode & "'"
            Dim cmd As New SqlCommand(SqlStr, SpectrumCon())
            Dim daHomeDelivery As New SqlDataAdapter(cmd)
            Dim dtHomeDelivery As New DataTable
            daHomeDelivery.Fill(dtHomeDelivery)
            Return dtHomeDelivery
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function GetHomeDeliveryData(ByVal Billno As String, ByVal Sitecode As String, Optional ByVal Partner As String = "0") As DataTable
        Dim dtCreditSaleAdjustment As New DataTable
        Try
            Dim SqlStr = "select * from CashMemoHdr where BillNo='" & Billno & "' AND Sitecode='" & Sitecode & "'"
            Dim cmd As New SqlCommand(SqlStr, SpectrumCon())
            Dim daHomeDelivery As New SqlDataAdapter(cmd)
            Dim dtHomeDelivery As New DataTable
            daHomeDelivery.Fill(dtHomeDelivery)
            Return dtHomeDelivery
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    ''' <summary>
    ''' Get Customer Mobile No.
    ''' </summary>
    ''' <param name="CLPNo"></param>
    ''' <param name="Sitecode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustData(ByVal CLPNo As String, ByVal Sitecode As String) As String
        Try
            Dim dtCust As New DataTable
            Dim query = "select ClpProgramId  from CLPProgramSiteMap  where Status=1 and SiteCode ='" & Sitecode & "'"
            Dim strString As String = "select Mobileno  from CLPCustomers where CardNo = '" & CLPNo & "'and Clpprogramid in (" & query & ");"
            Dim da As New SqlDataAdapter(strString, SpectrumCon())
            da.Fill(dtCust)
            If Not dtCust Is Nothing AndAlso dtCust.Rows.Count > 0 Then
                Return dtCust.Rows(0)(0)
            End If
            Return String.Empty
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    ''' <summary>
    ''' Updating Float Amt on Dispatch Click
    ''' </summary>
    ''' <param name="FloatAmt"></param>
    ''' <param name="DeliveryPerson"></param>
    ''' <param name="BillNo"></param>
    ''' <param name="BillDate"></param>
    ''' <param name="SiteCode"></param>
    ''' <param name="dsVoucher"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateFloatAmtDispatchData(ByVal FloatAmt As Double, ByVal DeliveryPerson As String, ByVal BillNo As String, ByVal BillDate As Date, ByVal SiteCode As String, ByRef dsVoucher As DataSet) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            If Not Conn.State = ConnectionState.Open Then
                Conn.Open()
            End If
            Trans = Conn.BeginTransaction("CreditSale")
            Dim sqlcmd As New SqlCommand("", Conn, Trans)

            Dim cmd = " update VoucherHDR set TotalAmt=" & FloatAmt & ",UPDATEDON=GetDate() where RefDocNumber='" & BillNo & "' AND RefDocDate=@BillDate; " & _
                     " update VoucherDTL set Amount=" & FloatAmt & ",Narration= '" & String.Format(dsVoucher.Tables("MSTVcherAccTypeFloat").Rows(0)("Narration"), DeliveryPerson, BillNo) & "',UPDATEDON=GetDate() " & _
                     " where VoucherID in (select VoucherID from VoucherHDR where RefDocNumber='" & BillNo & "' AND RefDocDate=@BillDate);"
            Dim objComm As New clsCommon
            sqlcmd.CommandText = cmd
            sqlcmd.Parameters.Add("@BillDate", SqlDbType.DateTime)
            sqlcmd.Parameters("@BillDate").Value = BillDate
            If Not sqlcmd.ExecuteNonQuery() > 0 Then
                Trans.Rollback()
                Conn.Close()
                Return False
            End If
            Trans.Commit()
            Conn.Close()
            Return True

        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function SaveFloatAmountData(ByVal FloatAmt As Double, ByVal DeliveryPerson As String, ByVal BillNo As String, ByVal BillDate As Date, ByVal sitecode As String, ByVal UserID As String, ByVal Fyear As String, ByRef dsVocuher As DataSet) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            Dim comm As New clsCommon()
            If Not Conn.State = ConnectionState.Open Then
                Conn.Open()
            End If
            Trans = Conn.BeginTransaction("CreditSale")
            Dim sqlcmd As New SqlCommand("", Conn, Trans)
            Dim cmd As String
            Dim VoucherID As String

            Try
                If String.IsNullOrEmpty(VoucherID) Then
                    Dim DocNo As String = comm.getDocumentNo("PettyCashVoucher", sitecode)
                    VoucherID = comm.GenDocNo("PCV" & sitecode.Substring((sitecode).Length - 3, 3) & (Fyear).Substring((Fyear).Length - 2, 2), 15, DocNo)
                End If

            Catch ex As Exception
                LogException(ex)
                Return False
            End Try


            cmd = "INSERT INTO VoucherHDR ([VoucherID],[VoucherTypeCode],[Sitecode],[FinYear],[ExpenseDate],[TotalAmt],[VoucherAccountID] " & _
           ",[PaidTo],[Currency],[Approvedby],[ReceivedBY],[PreparedBY],[Approvalstatus],[EmployeeID],[SupplierID],[RefDocNumber],[RefDocDate] " & _
           ",[CREATEDAT],[CREATEDBY],[CREATEDON],[UPDATEDAT],[UPDATEDBY],[UPDATEDON],[STATUS]) " & _
            "VALUES ('" & VoucherID & "','" & dsVocuher.Tables("MSTVcherAccTypeFloat").Rows(0)("VoucherTypeCode") & "','" & sitecode & "','" & Fyear & "',@BillDate,'" & FloatAmt & "','" & dsVocuher.Tables("MSTVcherAccTypeFloat").Rows(0)("VoucherAccountID") & "'," & _
            "'','INR','','','',0,'','', '" & BillNo & "',@BillDate, " & _
           "'" & sitecode & "','" & UserID & "', getdate() ,'" & sitecode & "','" & UserID & "',getdate(),1);" & _
           "Insert INTO VoucherDTL (VoucherID,Sitecode,FinYear,LineNumber,Amount," & _
            "Narration,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS) " & _
            "Values ('" & VoucherID & "','" & sitecode & "','" & Fyear & "',1,'" & FloatAmt & "'," & _
           "'" & String.Format(dsVocuher.Tables("MSTVcherAccTypeFloat").Rows(0)("Narration"), DeliveryPerson, BillNo) & "','" & sitecode & "','" & UserID & "',getDate(),'" & sitecode & "','" & UserID & "',getDate() ,1)"

            sqlcmd.CommandText = cmd
            sqlcmd.Parameters.Add("@BillDate", SqlDbType.DateTime)
            sqlcmd.Parameters("@BillDate").Value = BillDate
            If Not sqlcmd.ExecuteNonQuery() > 0 Then
                Trans.Rollback()
                Conn.Close()
                Return False
            End If
            If comm.UpdateDocumentNo("PettyCashVoucher", SpectrumCon, Trans, , "FO_DOC") = False Then
                Trans.Rollback()
                Conn.Close()
                Return False
            End If
            Trans.Commit()
            Conn.Close()
            Return True
        Catch ex As Exception
            LogException(ex)
            Return False
        End Try
    End Function


    Public Function UpdateDeliveryPartnerData(ByVal DeliveryPartner As String, ByVal BillNo As String, ByVal BillDate As Date, ByVal SiteCode As String) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            If Not Conn.State = ConnectionState.Open Then
                Conn.Open()
            End If
            Trans = Conn.BeginTransaction("CreditSale")
            Dim sqlcmd As New SqlCommand("", Conn, Trans)

            Dim cmd = " update CashMemoHdr set DeliveryPartnerId='" & DeliveryPartner & "',UPDATEDON=GetDate() where BillNo='" & BillNo & "' And Sitecode='" & SiteCode & "'; "
            Dim objComm As New clsCommon
            sqlcmd.CommandText = cmd
            sqlcmd.Parameters.Add("@BillDate", SqlDbType.DateTime)
            sqlcmd.Parameters("@BillDate").Value = BillDate
            If Not sqlcmd.ExecuteNonQuery() > 0 Then
                Trans.Rollback()
                Conn.Close()
                Return False
            End If
            Trans.Commit()
            Conn.Close()
            Return True

        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function UpdateDispatchTimeData(ByVal DeliveryPerson As String, ByVal BillNo As String, ByVal SiteCode As String, Optional ByVal UpdateDispatchPerson As Boolean = False) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            If Not Conn.State = ConnectionState.Open Then
                Conn.Open()
            End If
            Trans = Conn.BeginTransaction("CreditSale")
            Dim sqlcmd As New SqlCommand("", Conn, Trans)
            Dim cmd As String = ""
            If UpdateDispatchPerson = True Then
                cmd = "update CashMemoHdr set DeliveryPersonID='" & DeliveryPerson & "',SalesExecutiveCode='" & DeliveryPerson & "',UPDATEDON=GetDate() where BillNo='" & BillNo & "' and SiteCode='" & SiteCode & "'"
            Else
                cmd = "update CashMemoHdr set DeliveryPersonID='" & DeliveryPerson & "',SalesExecutiveCode='" & DeliveryPerson & "',DispatchTime=GETDATE(),UPDATEDON=GetDate() where BillNo='" & BillNo & "' and SiteCode='" & SiteCode & "'"
            End If
            Dim objComm As New clsCommon
            sqlcmd.CommandText = cmd
            If Not sqlcmd.ExecuteNonQuery() > 0 Then
                Trans.Rollback()
                Conn.Close()
                Return False
            End If
            Trans.Commit()
            Conn.Close()
            Return True

        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function InsertUpdateCashMemoHdrOrderStatus(ByVal objCashMemoHdrOrderStatusMap As clsCashMemoHdrOrderStatusMap, ByVal isDispatch As Boolean) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            If Not Conn.State = ConnectionState.Open Then
                Conn.Open()
            End If
            Trans = Conn.BeginTransaction("CreditSale")
            Dim sqlcmd As New SqlCommand("", Conn, Trans)
            Dim cmd As String = ""
            If Not isDispatch Then
                cmd = "insert into  CashMemoHdrOrderStatusMap (SiteCode,FinYear,BillNo,ExternalOrderReferenceId,remarks1,remarks2,remarks3,OrderStatusId,CreatedAt,CreatedOn,CreatedBy,UpdatedAt,UpdatedOn,UpdatedBy,Status)" & _
                      "Values('" & objCashMemoHdrOrderStatusMap.SiteCode & "','" & objCashMemoHdrOrderStatusMap.FinYear & "','" & objCashMemoHdrOrderStatusMap.BillNo & _
                       "','" & objCashMemoHdrOrderStatusMap.ExternalOrderReferenceId & _
                      "','" & objCashMemoHdrOrderStatusMap.Remark1 & "','" & objCashMemoHdrOrderStatusMap.Remark2 & "','" & objCashMemoHdrOrderStatusMap.Remark3 & _
                      "','" & objCashMemoHdrOrderStatusMap.OrderStatusId & "','" & objCashMemoHdrOrderStatusMap.CreatedAt & _
                      "',GETDATE(),'" & objCashMemoHdrOrderStatusMap.CreatedBy & _
                      "','" & objCashMemoHdrOrderStatusMap.UpdatedAt & "',GETDATE(),'" & objCashMemoHdrOrderStatusMap.UpdatedBy & "','" & objCashMemoHdrOrderStatusMap.Status & "')"
            Else
                cmd = "update  CashMemoHdrOrderStatusMap  set OrderStatusId='" & objCashMemoHdrOrderStatusMap.OrderStatusId & "' ,UpdatedAt='" & _
                       objCashMemoHdrOrderStatusMap.UpdatedAt & "',UpdatedOn=GETDATE()" & _
                     ",UpdatedBy='" & objCashMemoHdrOrderStatusMap.UpdatedBy & "' Where BillNo='" & objCashMemoHdrOrderStatusMap.BillNo & "' and  OrderStatusId='" & objCashMemoHdrOrderStatusMap.OldOrderStatusId & "' and SiteCode='" & objCashMemoHdrOrderStatusMap.SiteCode & "'"
            End If
            sqlcmd.CommandText = cmd
            If Not sqlcmd.ExecuteNonQuery() > 0 Then
                Trans.Rollback()
                Conn.Close()
                Return False
            End If
            Trans.Commit()
            Conn.Close()
            Return True

        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function
    '-- function for updating table on order cancellation of home delivery - ashma 21 dec 2016
    Public Function UpdateHDCancelOrder(ByVal BillNo As String, ByVal SiteCode As String, Optional ByVal CalllFromZomato As Boolean = False) As Boolean
        Dim tran As SqlTransaction = Nothing
        Try
            If Not Conn.State = ConnectionState.Open Then
                Conn.Open()
            End If
            Trans = Conn.BeginTransaction("HomeDeliveryCancelOrder")
            Dim sqlcmd As New SqlCommand("", Conn, Trans)

            'Dim cmd = "update CashMemoHdr set DeletionDate=GetDate(), DeletionTime=GetDate(),BillIntermediateStatus='" & BillIntermediateStatus & "' where BillNo='" & BillNo & "' and SiteCode='" & SiteCode & "'"
            Dim cmd As String
            If CalllFromZomato Then
                cmd = "update DPACashMemoHdr set Status='0' where BillNo='" & BillNo & "' and SiteCode='" & SiteCode & "'"
            Else
                cmd = "update CashMemoHdr set Status='0' where BillNo='" & BillNo & "' and SiteCode='" & SiteCode & "'"
            End If

            sqlcmd.CommandText = cmd
            If Not sqlcmd.ExecuteNonQuery() > 0 Then
                Trans.Rollback()
                Conn.Close()
                Return False
            End If
            Trans.Commit()
            Conn.Close()
            Return True

        Catch ex As Exception
            tran.Rollback()
            CloseConnection()
            LogException(ex)
            Throw New Exception(ex.Message)
        End Try
    End Function


    ''' <summary>
    ''' Saving Float Amt Return to Petty Cash Voucher Receipt Entry
    ''' </summary>
    ''' <param name="FloatAmtReturn"></param>
    ''' <param name="DeliveryPerson"></param>
    ''' <param name="BillNo"></param>
    ''' <param name="BillDate"></param>
    ''' <param name="sitecode"></param>
    ''' <param name="UserID"></param>
    ''' <param name="Fyear"></param>
    ''' <param name="dsVoucher"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFloatAmtReturnedData(ByVal FloatAmtReturn As Double, ByVal DeliveryPerson As String, ByVal BillNo As String, ByVal InvoiceBillNo As String, ByVal BillDate As Date, ByVal sitecode As String, ByVal UserID As String, ByVal Fyear As String, ByRef dsVoucher As DataSet) As Boolean
        Dim comm As New clsCommon()
        Try
            If Not Conn.State = ConnectionState.Open Then
                Conn.Open()
            End If
            Trans = Conn.BeginTransaction("CreditSale")
            Dim sqlcmd As New SqlCommand("", Conn, Trans)
            Dim VoucherID As String
            Try
                If String.IsNullOrEmpty(VoucherID) Then
                    Dim DocNo As String = comm.getDocumentNo("PettyCashVoucher", sitecode)
                    VoucherID = comm.GenDocNo("PCV" & sitecode.Substring((sitecode).Length - 3, 3) & (Fyear).Substring((Fyear).Length - 2, 2), 15, DocNo)
                End If

            Catch ex As Exception
                LogException(ex)
                Return False
            End Try
            Dim cmd = "INSERT INTO VoucherHDR ([VoucherID],[VoucherTypeCode],[Sitecode],[FinYear],[ExpenseDate],[TotalAmt],[VoucherAccountID] " & _
                      ",[PaidTo],[Currency],[Approvedby],[ReceivedBY],[PreparedBY],[Approvalstatus],[EmployeeID],[SupplierID],[RefDocNumber],[RefDocDate] " & _
                      ",[CREATEDAT],[CREATEDBY],[CREATEDON],[UPDATEDAT],[UPDATEDBY],[UPDATEDON],[STATUS]) " & _
                      " VALUES ('" & VoucherID & "','" & dsVoucher.Tables("MSTVcherAccTypeFloatReturn").Rows(0)("VoucherTypeCode") & "','" & sitecode & "','" & Fyear & "',@BillDate,'" & FloatAmtReturn & "','" & dsVoucher.Tables("MSTVcherAccTypeFloatReturn").Rows(0)("VoucherAccountID") & "'," & _
                      "'','INR','','','',0,'','', '" & InvoiceBillNo & "',@BillDate, " & _
                      "'" & sitecode & "','" & UserID & "', getdate() ,'" & sitecode & "','" & UserID & "',getdate(),1);" & _
                      "Insert INTO VoucherDTL (VoucherID,Sitecode,FinYear,LineNumber,Amount," & _
                      "Narration,CREATEDAT,CREATEDBY,CREATEDON,UPDATEDAT,UPDATEDBY,UPDATEDON,STATUS) " & _
                      "Values ('" & VoucherID & "','" & sitecode & "','" & Fyear & "',1,'" & FloatAmtReturn & "'," & _
                      "'" & String.Format(dsVoucher.Tables("MSTVcherAccTypeFloatReturn").Rows(0)("Narration"), DeliveryPerson, BillNo) & "','" & sitecode & "','" & UserID & "',getDate(),'" & sitecode & "','" & UserID & "',getDate() ,1)"

            sqlcmd.CommandText = cmd
            sqlcmd.Parameters.Add("@BillDate", SqlDbType.DateTime)
            sqlcmd.Parameters("@BillDate").Value = BillDate
            If Not sqlcmd.ExecuteNonQuery() > 0 Then
                Trans.Rollback()
                Conn.Close()
                Return False
            End If
            If comm.UpdateDocumentNo("PettyCashVoucher", SpectrumCon, Trans) = False Then
                Trans.Rollback()
                Conn.Close()
                Return False
            End If
            Trans.Commit()
            Conn.Close()
            Return True

        Catch ex As Exception
            Trans.Rollback()
            Conn.Close()
            LogException(ex)
            Return False
        End Try

    End Function
    'code added by vipul for calculating adjusted amount 
    Public Function BillAdjustAmount(ByVal Billno As String, ByVal Sitecode As String) As Double
        Try
            Dim SqlStr = " select isnull(sum(AmountTendered),0) as AmountTendered from CreditReceipt where RefBillNo='" & Billno & "' AND Sitecode='" & Sitecode & "' and Status=1"
            Dim cmd As New SqlCommand(SqlStr, SpectrumCon())
            Dim daAdjustamt As New SqlDataAdapter(cmd)
            Dim dtAdjustamt As New DataTable
            daAdjustamt.Fill(dtAdjustamt)
            Return dtAdjustamt.Rows(0)(0)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Public Function SattleCreditSaleUsingSingleTender(ByVal tender As String, ByVal SONumber As String, ByVal TendrAmt As String) As DataTable

        Try
            Dim query As String = "Exec CalculateSaleOrderInv '" & tender & "', '" & SONumber & "','" & TendrAmt & "'"    '' commented   CalculateSaleOrderInv
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function



    Public Function SattleCreditSaleUsingMultipleTender(ByVal SONumber As String, ByVal TendrAmt As String) As DataTable

        Try
            Dim query As String = "Exec CalculateSaleOrderInvMulti  '" & SONumber & "','" & TendrAmt & "'"
            Return GetFilledTable(query)
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
#Region "WriteOff Screen"

    ''' <summary>
    ''' Fetching the schema of creditsalewriteoff Table
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBillWriteOffDtls() As DataTable

        Dim daWriteOff As SqlDataAdapter
        Dim dtWriteOff As DataTable
        Try
            daWriteOff = New SqlDataAdapter("SELECT * FROM CreditSaleWriteOff", SpectrumCon)
            dtWriteOff = New DataTable
            daWriteOff.Fill(dtWriteOff)
            dtWriteOff.TableName = "CreditSaleWriteOff"
            Return dtWriteOff

        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Updating data into creditsalewriteoff table and updating document no. for Write off
    ''' </summary>
    ''' <param name="dtCreditSaleWriteOff"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateCreditSaleWriteOff(ByVal dtCreditSaleWriteOff As DataTable) As Boolean
        Try
            Dim clsCommon As New clsCommon()

            If Not Conn.State = ConnectionState.Open Then
                Conn.Open()
            End If
            Trans = Conn.BeginTransaction("CreditSale")

            If clsCommon.SaveData(dtCreditSaleWriteOff, SpectrumCon, Trans) Then

                Dim objType = "FO_DOC"
                clsCommon.UpdateDocumentNo("CreditSaleWriteOff", Conn, Trans, , objType)

                Trans.Commit()
                Conn.Close()

                Return True
            End If

            Return False
        Catch ex As Exception
            Trans.Rollback()
            Conn.Close()
            Return False
        End Try
    End Function
#End Region
End Class
Public Class clsCashMemoHdrOrderStatusMap
    Private _SiteCode As String
    Public Property SiteCode() As String
        Get
            Return _SiteCode
        End Get
        Set(ByVal value As String)
            _SiteCode = value
        End Set
    End Property
    Private _FinYear As String
    Public Property FinYear() As String
        Get
            Return _FinYear
        End Get
        Set(ByVal value As String)
            _FinYear = value
        End Set
    End Property
    Private _BillNo As String
    Public Property BillNo() As String
        Get
            Return _BillNo
        End Get
        Set(ByVal value As String)
            _BillNo = value
        End Set
    End Property
    Private _ExternalOrderReferenceId As String
    Public Property ExternalOrderReferenceId() As String
        Get
            Return _ExternalOrderReferenceId
        End Get
        Set(ByVal value As String)
            _ExternalOrderReferenceId = value
        End Set
    End Property
    Private _Remark1 As String
    Public Property Remark1() As String
        Get
            Return _Remark1
        End Get
        Set(ByVal value As String)
            _Remark1 = value
        End Set
    End Property
    Private _Remark2 As String
    Public Property Remark2() As String
        Get
            Return _Remark2
        End Get
        Set(ByVal value As String)
            _Remark2 = value
        End Set
    End Property
    Private _Remark3 As String
    Public Property Remark3() As String
        Get
            Return _Remark3
        End Get
        Set(ByVal value As String)
            _Remark3 = value
        End Set
    End Property
    Private _OrderStatusId As String
    Public Property OrderStatusId() As String
        Get
            Return _OrderStatusId
        End Get
        Set(ByVal value As String)
            _OrderStatusId = value
        End Set
    End Property
    Private _OldOrderStatusId As String
    Public Property OldOrderStatusId() As String
        Get
            Return _OldOrderStatusId
        End Get
        Set(ByVal value As String)
            _OldOrderStatusId = value
        End Set
    End Property
    Private _CreatedAt As String
    Public Property CreatedAt() As String
        Get
            Return _CreatedAt
        End Get
        Set(ByVal value As String)
            _CreatedAt = value
        End Set
    End Property
    Private _CreatedBy As String
    Public Property CreatedBy() As String
        Get
            Return _CreatedBy
        End Get
        Set(ByVal value As String)
            _CreatedBy = value
        End Set
    End Property
    Private _CreatedOn As Date
    Public Property CreatedOn() As Date
        Get
            Return _CreatedOn
        End Get
        Set(ByVal value As Date)
            _CreatedOn = value
        End Set
    End Property
    Private _UpdatedAt As String
    Public Property UpdatedAt() As String
        Get
            Return _UpdatedAt
        End Get
        Set(ByVal value As String)
            _UpdatedAt = value
        End Set
    End Property
    Private _UpdatedBy As String
    Public Property UpdatedBy() As String
        Get
            Return _UpdatedBy
        End Get
        Set(ByVal value As String)
            _UpdatedBy = value
        End Set
    End Property
    Private _UpdatedOn As Date
    Public Property UpdatedOn() As Date
        Get
            Return _UpdatedOn
        End Get
        Set(ByVal value As Date)
            _UpdatedOn = value
        End Set
    End Property
    Private _Status As Boolean
    Public Property Status() As Boolean
        Get
            Return _Status
        End Get
        Set(ByVal value As Boolean)
            _Status = value
        End Set
    End Property
End Class