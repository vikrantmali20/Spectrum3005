Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Xml
Imports SpectrumBL


Public Class clsPrintBirthList
    Public Sub New()

    End Sub
End Class
''' <summary>
'''  Print BirthList sales ,Created , or return item 
''' </summary>
''' <remarks></remarks>
Class PrintBirthList

    Private _CustomerDetail As DataTable
    Private _dtBirthListItemDetail As DataTable
    Private _OrderNumber As String
    Private _InvoiceNumber As String
    ''' <summary>
    '''  Birthlist transaction for printing 
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum PrintTransactionSet
        CreateBirthList
        SaleBirthListItem
        EditBirthListItem
        ReturnBirthListItem
    End Enum
    Private _IsReprint As Boolean
    Private Property Reprint() As String
        Get
            Return _IsReprint
        End Get
        Set(ByVal value As String)
            _IsReprint = value
        End Set
    End Property
    Private Property InvoiceNumber() As String
        Get
            Return _InvoiceNumber
        End Get
        Set(ByVal value As String)
            _InvoiceNumber = value
        End Set
    End Property
    ''' <summary>
    '''  Order Number for Created new item information
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property OrderNumber() As String
        Get
            Return _OrderNumber
        End Get
        Set(ByVal value As String)
            _OrderNumber = value
        End Set
    End Property
    Private _PrintTransaction As PrintTransactionSet
    ''' <summary>
    ''' Customer Print for sales,retrun or create BL items 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property PrintTransaction() As PrintTransactionSet
        Get
            Return _PrintTransaction
        End Get
        Set(ByVal value As PrintTransactionSet)
            _PrintTransaction = value
        End Set
    End Property
    ''' <summary>
    ''' Customer details who ordered or sold or comes to return items 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property CustomerDetail() As DataTable
        Get
            Return _CustomerDetail
        End Get
        Set(ByVal value As DataTable)
            _CustomerDetail = value
        End Set
    End Property
    ''' <summary>
    '''  Customer ordered,sold ,return item 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property BirthListItemDetail() As DataTable
        Get
            Return _dtBirthListItemDetail
        End Get
        Set(ByVal value As DataTable)
            _dtBirthListItemDetail = value
        End Set
    End Property
    ''' <summary>
    ''' Voucher Information 
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtVoucherSales As DataTable
    Private Property VoucherDetail() As DataTable
        Get
            Return _dtVoucherSales
        End Get
        Set(ByVal value As DataTable)
            _dtVoucherSales = value
        End Set
    End Property
    ''' <summary>
    '''  Payment History  
    ''' </summary>
    ''' <remarks></remarks>
    Private _dtPaymentHistory As DataTable
    Private Property PaymentHistory() As DataTable
        Get
            Return _dtPaymentHistory
        End Get
        Set(ByVal value As DataTable)
            _dtPaymentHistory = value
        End Set
    End Property

    Private _dtBirthListCustomerInfo As DataRow
    ''' <summary>
    '''  Sales and Edit BirthList : Birthlist owner information 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property BirthListCustomerInfo() As DataRow
        Get
            Return _dtBirthListCustomerInfo
        End Get
        Set(ByVal value As DataRow)
            _dtBirthListCustomerInfo = value
        End Set
    End Property
    ''' <summary>
    '''  Print setting for birthlist for all birthlist printing 
    ''' </summary>
    ''' <UsedBy>frmBirthListCreate.vb,frmBirthListSale.vb,frmBirthListUpdate.vb</UsedBy>
    ''' <param name="StrSubHeader"></param>
    ''' <param name="StrFooter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintBirthListHeaderAndFooter(ByRef StrSubHeader As StringBuilder, ByRef StrFooter As StringBuilder) As Boolean
        Dim dtPrint As DataTable
        Dim objClsComman As New clsCommon
        dtPrint = objClsComman.GetPrintingDetails("BLS")
        Dim StrPrintHeaderLine As String

        Dim StrPrintFooterLine As String

        If Not dtPrint Is Nothing Then
            Dim filter As String = ""
            Dim dv As New DataView(dtPrint, filter, "Sequenceno", DataViewRowState.CurrentRows)
            'If clsDefaultConfiguration.HeaderPrinting = True Then
            filter = "TOPBOTTOM='Top'"
            dv.RowFilter = filter
            Dim fon As New Font("Verdana", 3)
            For Each drview As DataRowView In dv
                StrPrintHeaderLine = drview("ReceiptText").ToString()

                If Not drview("Align") Is Nothing AndAlso drview("Align").ToString() <> String.Empty Then
                    'StrPrintHeaderLine
                End If
                StrSubHeader = StrSubHeader.AppendLine(StrPrintHeaderLine & vbNewLine)
            Next
            'End If
            'If clsDefaultConfiguration.FooterPrinting = True Then
            filter = "TOPBOTTOM='Bottom'"
            dv.RowFilter = filter
            For Each drview As DataRowView In dv
                StrPrintFooterLine = drview("ReceiptText").ToString()
                If Not drview("Align") Is Nothing AndAlso drview("Align").ToString() <> String.Empty Then
                    'StrPrintHeaderLine
                End If
                StrFooter = StrFooter.AppendLine(StrPrintFooterLine & vbNewLine)
            Next
            'End If
        End If
    End Function
    ''' <summary>
    '''  Call print function to print delvery note .
    ''' </summary>
    ''' <param name="errorMsg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Public Function Print(ByRef errorMsg As String) As Boolean
        Try
            Dim printMsg As New System.Text.StringBuilder
            If PrintTransaction = PrintTransactionSet.CreateBirthList Then
                Dim strBirthListID As String = InvoiceNumber
                Return NewBirthList(CustomerDetail, BirthListItemDetail, strBirthListID)
            Else
                Dim IsPickupItem As Boolean
                Dim iTotalBirthListItem, decNetAmt As Decimal 
                Dim strTotalQty, strNetAmt, strTermsCond, strLineEnd As String
                Dim strHdrItemInfo As New System.Text.StringBuilder 
                If (Reprint) Then
                    ReprintBirthListDeatils(strHdrItemInfo, strTotalQty, strNetAmt)
                Else
                    'Purchased item information  
                    If (SalesBirthlist(strHdrItemInfo, iTotalBirthListItem, decNetAmt, strTotalQty, strNetAmt, IsPickupItem)) Then
                        Dim strHeaderTitle As String = String.Format("Sales against BirthList :               InvoiceNumber::{0}", InvoiceNumber)
                        If (PrintFormat(strHeaderTitle, strHdrItemInfo, strTotalQty, strNetAmt, errorMsg)) Then
                            If (IsPickupItem) Then
                                strTotalQty = String.Empty
                                strNetAmt = String.Empty
                                strHdrItemInfo.Length = 0
                                If (DeliveryNotePickUpItem(strHdrItemInfo, strTotalQty, strNetAmt)) Then
                                    Dim strHeaderTitle2 As String = String.Format("Deliver Note  :               InvoiceNumber::{0}", OrderNumber)
                                    PrintFormat(strHeaderTitle2, strHdrItemInfo, strTotalQty, strNetAmt, errorMsg)
                                End If
                                errorMsg = String.Empty
                                Return True
                            Else
                                errorMsg = String.Empty
                                Return False
                            End If
                        End If
                    Else
                        errorMsg = "Not any item purchased "
                        Return False
                    End If
                    'Voucher Information 
                End If 
               
            End If

        Catch ex As Exception
            errorMsg = "Print Exception"
            Return False
        End Try

    End Function
    Private Function PrintFormat(ByVal strhdrTitle As String, ByVal strHdrItemInfo As StringBuilder, ByVal strTotalQty As String, ByVal strNetAmt As String, ByRef errorMsg As String) As Boolean
        Try
            Try
                Dim printMsg As New System.Text.StringBuilder
                If PrintTransaction = PrintTransactionSet.CreateBirthList Then
                    Dim strBirthListID As String = InvoiceNumber
                    Return NewBirthList(CustomerDetail, BirthListItemDetail, strBirthListID)
                Else
                    Dim IsPickupItem As Boolean 
                    Dim strTermsCond, strLineEnd As String 
                    Dim SelectedCustomerInfo As DataRow = CustomerDetail.Rows(0)
                    Dim SelectedBirthListInfo As DataRow = BirthListCustomerInfo
                    Dim BirthLisID As String = BirthListItemDetail.Rows(0)("BirthListID")
                    Dim CustomerID As String = CustomerDetail.Rows(0)("CustomerNO") 
                    Try 
                        Dim strhdrLine, strhdrEndLing As String
                        Dim sbCompdetails As New System.Text.StringBuilder
                        Dim strSiteName As String = ""
                        Dim strSiteAddress As String = ""
                        strhdrLine = "----------------------------------------------------------------------------------------------------" & vbCrLf
                         strhdrEndLing = "----------------------------------------------------------------------------------------------------" & vbCrLf

                        Dim strHeader As New StringBuilder
                        Dim strFooter As New StringBuilder
                        PrintBirthListHeaderAndFooter(strHeader, strFooter)
                        Dim strLineSiteEnd As String
                        Dim strOrderNo As String
                        Dim strDate As String
                        Dim strCashierNm As String
                        Dim strCustomerName As String
                        Dim strCustomerAddress As New System.Text.StringBuilder
                        strLineSiteEnd = "----------------------------------------------------------------------------------------------------"
                        strOrderNo = "BirthList ID        : " & BirthLisID & vbTab & vbTab & vbTab & " " & vbTab & " Date    : " & Format(Now.Date, "dd-MM-yyyy")
                        strDate = "Expected Delivery Date : " & SelectedBirthListInfo("DeliveryDate")
                        strCashierNm = "Cashier Name           : " & clsAdmin.UserName
                        strCustomerName = "Customer Name    : " & SelectedCustomerInfo("CustomerName") & vbTab & vbTab & "  Customer Code : " & CustomerID
                        strCustomerAddress.Append("Home Address     : " & SelectedCustomerInfo("Address"))

                        Dim strCustomerphonNo As String
                        strCustomerphonNo = "Tel. No.  	: " & vbTab & SelectedCustomerInfo("OfficeNO")
                        Dim strCustomerLinEnd As String
                        Dim strHdrItem As String

                        strCustomerLinEnd = "----------------------------------------------------------------------------------------------------"

                        strHdrItem = "Item Code                  Item Desc                     Qty    Rate       NetAmt"

                        If (strHdrItemInfo.ToString() = String.Empty) Then

                            Return False
                        End If

                        Dim strItemDetailLineEnd As String
                        strItemDetailLineEnd = "----------------------------------------------------------------------------------------------------" & vbCrLf


                        strTermsCond = "<Terms & Condition>" & vbCrLf & vbCrLf
                        strLineEnd = "Authorized Sign:............................." & vbTab & "Customer Sign:..............................."

                        Dim strPaymentBrakup As New StringBuilder

                        PrintPaymentBreakup(strPaymentBrakup)

                        printMsg.Append(strhdrLine + vbCrLf)
                        printMsg.Append(strhdrTitle + vbCrLf)

                        printMsg.Append(strhdrEndLing + vbCrLf)
                        If (strHeader IsNot Nothing AndAlso Not String.IsNullOrEmpty(strHeader.ToString())) Then
                            printMsg.Append(strHeader.Append(vbCrLf))
                        End If
                        printMsg.Append(sbCompdetails.Append(vbCrLf))
                        printMsg.Append(strSiteName + vbCrLf)
                        printMsg.Append(strSiteAddress + vbCrLf)
                        printMsg.Append(strLineSiteEnd + vbCrLf)
                        printMsg.Append(strOrderNo + vbCrLf)
                        printMsg.Append(strDate + vbCrLf)
                        printMsg.Append(strCashierNm + vbCrLf)
                        printMsg.Append(strCustomerName + vbCrLf)
                        printMsg.Append(strCustomerAddress.Append(vbCrLf))
                        printMsg.Append(strCustomerphonNo + vbCrLf)
                        printMsg.Append(strCustomerLinEnd + vbCrLf)
                        printMsg.Append(strHdrItem + vbCrLf)
                        printMsg.Append(strItemDetailLineEnd + vbCrLf)
                        printMsg.Append(strHdrItemInfo.Append(vbCrLf))
                        printMsg.Append(strItemDetailLineEnd + vbCrLf)
                        printMsg.Append(strTotalQty + vbCrLf)
                        printMsg.Append(strNetAmt + vbCrLf)
                        printMsg.Append(strTermsCond + vbCrLf)
                        printMsg.Append(strLineEnd + vbCrLf)

                        If strFooter IsNot Nothing AndAlso Not String.IsNullOrEmpty(strFooter.ToString()) Then
                            printMsg.Append(strFooter.Append(vbCrLf))
                        End If
                        printMsg.Append(strPaymentBrakup.Append(vbCrLf)) 
                    Catch ex As Exception 
                    End Try
                    printMsg.ToString()
                    If Not printMsg.ToString() = String.Empty Then
                        fnPrint(printMsg.ToString(), "PRN")
                        errorMsg = String.Empty
                        Return True
                    Else
                        errorMsg = "Problem printing "
                        Return False
                    End If

                End If

            Catch ex As Exception
                errorMsg = "Print Exception"
                Return False
            End Try

        Catch ex As Exception

        End Try
    End Function
    Private Function ReprintBirthListDeatils(ByRef strHdrItemInfo As StringBuilder, ByRef strTotalQty As String, ByRef strNetAmt As String) As Boolean

        Try
            Dim strArticleCode As String
            Dim strArticleDesc As String
            Dim iPurchasedQty As Integer
            Dim iPrice As String
            Dim iNetAmount As String
            Dim iCalNetAmount As Decimal
            Dim iCalTotolNetAmt As Decimal = Decimal.Zero

            If Reprint = True Then
                For Each drDtl As DataRow In _dtBirthListItemDetail.Rows
                    If Not drDtl.RowState = DataRowState.Deleted Then
                        strArticleCode = drDtl("EAN")
                        strArticleDesc = drDtl("Discription") & Space(30 - drDtl("Discription").ToString.Length)

                        If (PrintTransaction = PrintTransactionSet.SaleBirthListItem) Then
                            iPurchasedQty = drDtl("BookedQty") & Space(7 - 4)
                        Else
                            iPurchasedQty = drDtl("RequstedQty") & Space(7 - 4)
                        End If
                        If Not drDtl("SellingPrice") Is DBNull.Value Then
                            iPrice = drDtl("SellingPrice")
                        Else
                            If (PrintTransaction = PrintTransactionSet.SaleBirthListItem) Then
                                iPrice = drDtl("NetAmt")
                            End If
                        End If
                        If (PrintTransaction = PrintTransactionSet.SaleBirthListItem) Then
                            iCalNetAmount = drDtl("BookedQty") * iPrice
                        Else
                            iCalNetAmount = drDtl("RequstedQty") * iPrice
                        End If
                        iNetAmount = String.Format(iCalNetAmount, "O.OO")
                        iCalTotolNetAmt = iCalTotolNetAmt + iNetAmount
                        strHdrItemInfo.Append(strArticleCode & Space(7) & strArticleDesc & Space(10) & iPurchasedQty.ToString() & Space(10) & iPrice & Space(10) & iNetAmount + vbCrLf)
                    End If
                Next
            End If
            If Reprint = True Then
                If (PrintTransaction = PrintTransactionSet.SaleBirthListItem) Then
                    strTotalQty = "Total Qty : " & _dtBirthListItemDetail.Compute("Sum(BookedQty)", " ")
                    strNetAmt = "Net   Amt : " & FormatNumber(_dtBirthListItemDetail.Compute("Sum(NetAmt)", " "), 2)
                Else
                    strTotalQty = "Total Qty : " & _dtBirthListItemDetail.Compute("Sum(RequstedQty)", " ")
                    strNetAmt = "Net   Amt : " & FormatNumber(_dtBirthListItemDetail.Compute("Sum(NetAmount)", " "), 2)
                End If
            End If


            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Private Function DeliveryNotePickUpItem(ByRef strHdrItemInfo As StringBuilder, ByRef strTotalQty As String, ByRef strNetAmt As String) As Boolean
        Try
            Dim strArticleCode As String
            Dim strArticleDesc As String
            Dim iPurchasedQty As Integer
            Dim iPrice As String
            Dim iNetAmount As String
            Dim iCalNetAmount As Decimal
            Dim iCalTotolNetAmt As Decimal = Decimal.Zero
            If Not PrintTransaction = PrintTransactionSet.EditBirthListItem Then
                For Each drDtl As DataRow In _dtBirthListItemDetail.Rows
                    If Not drDtl.RowState = DataRowState.Deleted Then
                        If (drDtl("PickUpQty") > 0) Then
                            strArticleCode = drDtl("EAN")
                            strArticleDesc = drDtl("Discription") & Space(30 - drDtl("Discription").ToString.Length)
                            iPurchasedQty = drDtl("PickUpQty") & Space(7 - 4)
                            iPrice = drDtl("SellingPrice")
                            iCalNetAmount = drDtl("PickUpQty") * drDtl("SellingPrice")
                            iNetAmount = String.Format(iCalNetAmount, "O.OO")
                            iCalTotolNetAmt = iCalTotolNetAmt + iNetAmount
                            strHdrItemInfo.Append(strArticleCode & Space(7) & strArticleDesc + iPurchasedQty.ToString() & Space(10) & iPrice & Space(10) & iNetAmount + vbCrLf)
                        End If
                    End If
                Next
            ElseIf PrintTransaction = PrintTransactionSet.EditBirthListItem Then
                For Each drDtl As DataRow In _dtBirthListItemDetail.Rows
                    If Not drDtl.RowState = DataRowState.Deleted Then
                        If (drDtl("PickUpQty") > 0) Then
                            strArticleCode = drDtl("EAN")
                            strArticleDesc = drDtl("Discription") & Space(30 - drDtl("Discription").ToString.Length)
                            iPurchasedQty = drDtl("PickUpQty") & Space(7 - 4)
                            iPrice = drDtl("SellingPrice")
                            iCalNetAmount = drDtl("PickUpQty") * drDtl("SellingPrice")
                            iNetAmount = String.Format(iCalNetAmount, "O.OO")
                            iCalTotolNetAmt = iCalTotolNetAmt + iNetAmount
                            strHdrItemInfo.Append(strArticleCode & Space(7) & strArticleDesc + iPurchasedQty.ToString() & Space(10) & iPrice & Space(10) & iNetAmount + vbCrLf)
                        End If
                        If (drDtl("CurrentReturnQty") > 0) Then
                            strArticleCode = drDtl("EAN")
                            strArticleDesc = drDtl("Discription") & Space(30 - drDtl("Discription").ToString.Length)
                            iPurchasedQty = drDtl("CurrentReturnQty") & Space(7 - 4)
                            iPrice = drDtl("SellingPrice")
                            iCalNetAmount = drDtl("CurrentReturnQty") * drDtl("SellingPrice")
                            iNetAmount = String.Format(iCalNetAmount, "O.OO")
                            iCalTotolNetAmt = iCalTotolNetAmt + iNetAmount
                            strHdrItemInfo.Append(strArticleCode & Space(7) & strArticleDesc + iPurchasedQty.ToString() & Space(10) & iPrice & Space(10) & iNetAmount + vbCrLf)

                        End If
                    End If
                Next
            End If

            strTotalQty = "Total Qty : " & _dtBirthListItemDetail.Compute("Sum(PickUpQty)", " ")
            strNetAmt = "Net   Amt : " & FormatNumber(iCalTotolNetAmt, 2)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SalesBirthlist(ByRef strHdrItemInfo As StringBuilder, ByRef iTotalBirthListItem As Decimal, ByRef decNetAmt As Decimal, ByRef strTotalQty As String, ByRef strNetAmt As String, Optional ByRef isPickupQty As Boolean = False) As Boolean
        Try

            Dim strArticleCode As String
            Dim strArticleDesc As String
            Dim iPurchasedQty As Integer
            Dim iPrice As String
            Dim iNetAmount As String
            For Each drDtl As DataRow In BirthListItemDetail.Rows
                If Not (drDtl.RowState = DataRowState.Deleted) Then
                    If (drDtl("purchasedqty") > 0) Then
                        strArticleCode = drDtl("EAN")
                        strArticleDesc = drDtl("Discription") & Space(30 - drDtl("Discription").ToString.Length)
                        iPurchasedQty = drDtl("purchasedqty") & Space(7 - 4)
                        iPrice = drDtl("SellingPrice")
                        If Not (PrintTransaction = PrintTransactionSet.EditBirthListItem) Then
                            iNetAmount = FormatNumber(drDtl("NetAmount").ToString(), 2)
                        Else
                            iNetAmount = FormatNumber(drDtl("CurrentPurchasedAmount").ToString(), 2)
                        End If
                        strHdrItemInfo.Append(strArticleCode & Space(7) & strArticleDesc + iPurchasedQty.ToString() & Space(10) & iPrice & Space(10) & iNetAmount + vbCrLf)
                    End If
                End If
            Next 

            If Not BirthListItemDetail Is Nothing Then
                If BirthListItemDetail.Rows.Count > 0 Then
                    Dim iPickUpQty As Integer = BirthListItemDetail.Compute("sum(PickUpQty)", " ")
                    If iPickUpQty > 0 Then
                        isPickupQty = True
                    End If
                End If
            End If



            If Not _dtBirthListItemDetail Is Nothing Then
                If (_dtBirthListItemDetail.Rows.Count > 0) Then

                    iTotalBirthListItem = _dtBirthListItemDetail.Compute("Sum(purchasedqty)", "")
                    strTotalQty = "Total Qty : " & _dtBirthListItemDetail.Compute("Sum(purchasedqty)", " ")
                    Try
                        If Not (PrintTransaction = PrintTransactionSet.EditBirthListItem) Then
                            decNetAmt = _dtBirthListItemDetail.Compute("Sum(NetAmount)", "")
                            strNetAmt = "Net   Amt : " & FormatNumber(_dtBirthListItemDetail.Compute("Sum(NetAmount)", ""), 2)
                        Else
                            decNetAmt = _dtBirthListItemDetail.Compute("Sum(NetAmount)", "")
                            strNetAmt = "Net   Amt : " & FormatNumber(_dtBirthListItemDetail.Compute("Sum(CurrentPurchasedAmount)", ""), 2)
                        End If
                    Catch ex As Exception

                    End Try
                End If
            End If
            VocuherSales(strHdrItemInfo, iTotalBirthListItem, decNetAmt, strTotalQty, strNetAmt, "", "")


            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function
    Private Function VocuherSales(ByRef strHdrItemInfo As StringBuilder, ByRef iTotalBirthListItem As Decimal, ByRef decNetAmt As Decimal, ByRef strTotalQty As String, ByRef strNetAmt As String, ByRef strTermsCond As String, ByRef strLineEnd As String) As Boolean
        Try

            Dim strArticleCode As String
            Dim strArticleDesc As String
            Dim iPurchasedQty As Integer
            Dim iPrice As String
            Dim iNetAmount As String
            If Not _dtVoucherSales Is Nothing Then
                For Each drGV As DataRow In _dtVoucherSales.Rows
                    strArticleCode = drGV("EAN")
                    strArticleDesc = drGV("Discription") & Space(30 - drGV("Discription").ToString.Length)
                    iPurchasedQty = drGV("BookedQty") & Space(7 - 4) 
                    iNetAmount = FormatNumber(drGV("NetAmount").ToString(), 2)
                    iPrice = drGV("SellingPrice")
                    strHdrItemInfo.Append(strArticleCode & Space(7) & strArticleDesc + iPurchasedQty.ToString() & Space(10) & iPrice & Space(10) & iNetAmount + vbCrLf)
                Next
            End If

            If Not _dtVoucherSales Is Nothing Then
                If _dtVoucherSales.Rows.Count > 0 Then
                    iTotalBirthListItem = Decimal.Add(iTotalBirthListItem, _dtVoucherSales.Compute("Sum(BookedQty)", ""))
                    strTotalQty = "Total Qty : " & iTotalBirthListItem
                    decNetAmt = decNetAmt + _dtVoucherSales.Compute("Sum(NetAmount)", " ")
                    strNetAmt = "Net   Amt : " & decNetAmt

                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function NewBirthList(ByVal dataTable As DataTable, ByVal dtGridData As DataTable, Optional ByVal strBirthListID As String = "") As Boolean
        Try
           
            Dim strBirthListIDR As String = "BirthList No: " + strBirthListID
            Dim strHeader As New StringBuilder
            Dim strFooter As New StringBuilder
           
            PrintBirthListHeaderAndFooter(strHeader, strFooter)

            Dim strCustomerName As String = "Customer Name : " + dataTable.Rows(0)("CustomerName")
            Dim strAddressMain As String = "Address:  " + dataTable.Rows(0)("Address")
            Dim strEmailID As String = "Email ID:" + dataTable.Rows(0)("EmailID")
            Dim strPinCode As String = "PinCode: " + dataTable.Rows(0)("PinCode")
            Dim strDeliveryDate As String = "DeliveryDate: " + dataTable.Rows(0)("DeliveryDate")
            Dim strAddressLn1 As String = ""
            Dim strAddressLn2 As String = ""
            If (strAddressMain.Length > 24) Then
                strAddressLn1 = strAddressMain.Substring(0, 24)
                Dim ilastIndex As Integer = strAddressMain.Length - 24
                strAddressLn2 = strAddressMain.Substring(24, ilastIndex)
            End If

            Dim strDotLineUAnticepatedDOB As String = "-----------------------------------"
            Dim strAnticepatedDateOfBirth As String = String.Format("Anticipated date of birth: " + dataTable.Rows(0)("EventDate"))

            Dim strDotLineLAnticepatedDOB As String = "-----------------------------------"

            Dim strCustomerLinEnd As String
            Dim strHdrItem As String
            Dim strHdrItemInfo As New System.Text.StringBuilder
            strCustomerLinEnd = "--------------------------------------------------------------------------------------------------"
            strHdrItem = "Item Code                  Item Desc                     Qty    Rate       NetAmt"
            Dim strArticleCode As String
            Dim strArticleDesc As String
            Dim iPurchasedQty As Integer
            Dim iPrice As String
            Dim iNetAmount As String

            Dim itemCount As Integer = 1
            For Each drGrid As DataRow In dtGridData.Rows
                If Not (drGrid.RowState = DataRowState.Deleted) Then
                    strArticleCode = drGrid("EAN")
                    strArticleDesc = drGrid("ArticleDescription") & Space(30 - drGrid("ArticleDescription").ToString.Length)
                    iPurchasedQty = drGrid("RequstedQty") & Space(7 - 4)
                    iPrice = drGrid("Rate")
                    iNetAmount = FormatNumber(drGrid("Amount").ToString(), 2)
                    strHdrItemInfo.Append(strArticleCode & Space(7) & strArticleDesc + iPurchasedQty.ToString() & Space(10) & iPrice & Space(10) & iNetAmount + vbCrLf)
                    itemCount += 1
                End If
            Next

            Dim strTotalItemCal As String = dataTable.Rows(0)("TotalItem")
            Dim iOrderedItemCAl As String = dataTable.Rows(0)("TotalQty")
            Dim decTotalAmountCal As String = dataTable.Rows(0)("TotalAmount")

            Dim strTotalItem As String = String.Format("TotalQty :  " & strTotalItemCal)
            Dim strOrderedItem As String = String.Format("OrderedQty :  " & iOrderedItemCAl)
            Dim strTotalAmount As String = String.Format("TotalAmount :  " & decTotalAmountCal)



            Dim strPrint As New StringBuilder
            strPrint.Length = 0

            strPrint.Append(strBirthListIDR + vbCrLf)

            If strHeader IsNot Nothing AndAlso Not String.IsNullOrEmpty(strHeader.ToString()) Then
                strPrint.Append(strHeader.Append(vbCrLf))
            End If


            strPrint.Append(strCustomerName + vbCrLf)

            If Not (strAddressLn1 = String.Empty) Then
                strPrint.Append(strAddressLn1 + vbCrLf)
            End If
            If Not strAddressLn2 = String.Empty Then
                strPrint.Append(strAddressLn2 + vbCrLf)
            End If
            If (strAddressLn1 = String.Empty And strAddressLn2 = String.Empty) Then
                strPrint.Append(strAddressMain + vbCrLf)
            End If 
            strPrint.Append(strDeliveryDate + vbCrLf)
            strPrint.Append(strPinCode + vbCrLf)
            strPrint.Append(strEmailID + vbCrLf)
            strPrint.Append(strDotLineLAnticepatedDOB + vbCrLf)
            strPrint.Append(strAnticepatedDateOfBirth + vbCrLf)
            strPrint.Append(strDotLineLAnticepatedDOB + vbCrLf)
            strPrint.Append(strCustomerLinEnd + vbCrLf)
            strPrint.Append(strHdrItem + vbCrLf)
            strPrint.Append(strHdrItemInfo.ToString() + vbCrLf)
            strPrint.Append(strDotLineLAnticepatedDOB + vbCrLf)
            strPrint.Append(strTotalItem + vbCrLf)
            strPrint.Append(strOrderedItem + vbCrLf)
            strPrint.Append(strTotalAmount + vbCrLf)

            If strFooter IsNot Nothing AndAlso Not String.IsNullOrEmpty(strFooter.ToString()) Then
                strPrint.Append(strFooter.Append(vbCrLf))
            End If
 
            fnPrint(strPrint.ToString(), "PRN")
            Return True
        Catch ex As Exception
            Return False
            ShowMessage(getValueByKey("CLPBL01"), "CLPBL01 - " & getValueByKey("CLAE05"))
        End Try
    End Function


    Private Function PrintPaymentBreakup(ByRef strPaymentBrakup As StringBuilder) As Boolean
        Try
            If Not PaymentHistory Is Nothing Then
                If _dtPaymentHistory.Rows.Count > 0 Then
                    Dim strPayment As String = "Cash Details :- "
                    Dim strline As String = "---------------------"
                    Dim strHeaderPayment As String = "|Tender Type|Amount|"
                    Dim strContent As New StringBuilder
                    Dim strTenderType As String

                    Dim strAmount As String
                    For Each drCash As DataRow In _dtPaymentHistory.Rows
                        strTenderType = drCash("RecieptType")
                        strAmount = drCash("Amount").ToString()
                        strContent.Append(strTenderType & "   " & strAmount + vbCrLf)
                    Next
                    strPaymentBrakup.Length = 0

                    strPaymentBrakup.Append(strPayment + vbCrLf)
                    strPaymentBrakup.Append(strline + vbCrLf)
                    strPaymentBrakup.Append(strHeaderPayment + vbCrLf)
                    strPaymentBrakup.Append(strline + vbCrLf)
                    strPaymentBrakup.Append(strContent.Append(vbCrLf))
                    strPaymentBrakup.Append(strline + vbCrLf)
                Else
                    strPaymentBrakup.Append("Payment Breakup is not found ")
                End If
            Else
                strPaymentBrakup.Append("Payment Breakup is not found ")
            End If
            Return True
        Catch ex As Exception
            strPaymentBrakup.Append("Payment Breakup is not found ")
            Return False
        End Try
    End Function


    ''' <summary>
    ''' For only CreditVoucher Printing 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

    End Sub

    ''' <summary>
    '''  Print BirthList 
    ''' </summary>
    ''' <param name="printTran">Transaction:New,Sales,Edit,Retrun </param>
    ''' <param name="dtcustomerDetails">Customer information </param>
    ''' <param name="dtbirthlistitem">Birthlist information</param>
    ''' <param name="dtVoucherDetail">Gift Voucher information </param>
    ''' <param name="strInvoiceNumber"> Invoice Number for current document only for purchased item</param>
    ''' <param name="strOrderNumber"> Ordered Number for current item  delivery i.e for pickup item </param>
    ''' <param name="isReprint">Reprinting of the document  </param>
    ''' <remarks></remarks>
    Public Sub New(ByVal printTran As PrintTransactionSet, ByVal dtcustomerDetails As DataTable, ByVal dtbirthlistitem As DataTable, Optional ByVal dtVoucherDetail As DataTable = Nothing, Optional ByVal dtBirthListCustomerInfo As DataRow = Nothing, Optional ByVal dtPaymentReciept As DataTable = Nothing, Optional ByVal strInvoiceNumber As String = "", Optional ByVal strOrderNumber As String = "", Optional ByVal isReprint As Boolean = True)
        Try
            PrintTransaction = printTran
            CustomerDetail = dtcustomerDetails
            BirthListItemDetail = dtbirthlistitem
            VoucherDetail = dtVoucherDetail
            InvoiceNumber = strInvoiceNumber
            OrderNumber = strOrderNumber
            Reprint = isReprint
            BirthListCustomerInfo = dtBirthListCustomerInfo
            PaymentHistory = dtPaymentReciept
        Catch ex As Exception

        End Try
    End Sub
       
    ''' <summary>
    ''' Printing Credit Voucher
    ''' </summary>
    ''' <param name="type">document Type</param>
    ''' <param name="Amt"></param>
    ''' <remarks></remarks>
    Public Function PrintVoucher(ByVal currentDate As Date, ByVal Documnettype As String, ByVal strBirthListId As String, ByVal userName As String, ByVal Amt As Double, Optional ByVal dtCustomerInfo As DataTable = Nothing, Optional ByVal strVoucherNo As String = "") As Boolean
        Try
            Dim strPrintMsg As StringBuilder
            Dim strLineDetail As New StringBuilder
            Dim StrHeader, StrSubHeader, StrBody, StrSubFooter, StrFooter, StrCashMemoLine, StrCashierLine As String
            Dim StrBirthListNo, StrBirthListDate, StrBirthListTime, StrCashier As String
            Dim TermsNcond, StrPrintHeaderLine, StrPrintFooterLine As String
            Dim strLine As String
            Dim strDblLine As String
            Dim objclscomman As New clsCommon
            
            'Dim dtView, dtPrint As DataTable
            'Dim obj As New SpectrumBL.clsCashMemo()
            'dtView = obj.GetVoucherData(CashMemoNo)

            Dim strHeaderDB As New StringBuilder
            Dim strFooterDB As New StringBuilder
            PrintBirthListHeaderAndFooter(strHeaderDB, strFooterDB)

            Dim strCustomerFullName As String = ""
            If Not dtCustomerInfo Is Nothing Then
                If dtCustomerInfo.Rows.Count > 0 Then
                    strCustomerFullName = dtCustomerInfo.Rows(0)("CustomerName")
                End If
            End If

            strLine = "----------------------------------------" & vbCrLf
            strDblLine = "========================================" & vbCrLf
            StrBirthListNo = strCustomerFullName
            StrBirthListDate = FormatDateTime(currentDate, DateFormat.ShortDate)
            StrCashMemoLine = "Credit Note/Voucher No:" & strVoucherNo & Space(15 - StrBirthListNo.Length) & "Date:" & StrBirthListDate & vbCrLf
            StrCashier = userName
            StrBirthListTime = currentDate.ToString("hh:mm:ss tt")
            StrCashierLine = "Cashier:" & StrCashier & Space(5) & StrBirthListTime & vbNewLine

            StrHeader = StrHeader & StrCashMemoLine
            StrHeader = StrHeader & StrCashierLine


            StrBody = "Total Discount Amount" & Space(2) & Amt & vbNewLine

            StrBody = StrBody & "Issued against " & vbCrLf

            StrBody = StrBody & "BirthList No:" & strBirthListId & vbNewLine
            StrBody = StrBody & "Customer Name:" & StrBirthListNo & vbNewLine

            StrBody = StrBody & "Valid For " & clsAdmin.CreditValidDays.ToString() & " Days" & vbCrLf
            StrBody = StrBody & "From issue date if stamped and signed." & vbCrLf & vbCrLf
            StrBody = StrBody & Space(10) & "-------------" & vbCrLf

            'Dim dtTerms As DataTable = dvItemDetail.ToTable(True, "TNCSRNO", "DESCRIPTION")
            'For Each drTerms As DataRow In dtTerms.Select("", "TNCSRNO", DataViewRowState.CurrentRows)
            '    TermsNcond = TermsNcond & drTerms("DESCRIPTION").ToString() & vbCrLf
            'Next

            'StrFooter = StrFooter & TermsNcond & vbCrLf

            strPrintMsg = New StringBuilder()
            strPrintMsg.Append(StrHeader)
            strPrintMsg.Append(strLine)
            strPrintMsg.Append(strHeaderDB)

            strPrintMsg.Append(StrBody)

            'strPrintMsg.Append(StrSubFooter)
            strPrintMsg.Append(strFooterDB)
            fnPrint(strPrintMsg.ToString(), "")

            Return True
        Catch ex As Exception

            Return False
        End Try
    End Function

    'Public Sub PrintGiftVoucher(ByVal VoucherNo As String, ByVal Amt As Double, ByVal ValidUpto As DateTime, Optional ByVal validDays As Int32 = 0)
    '    Try
    '        Dim strPrintMsg As StringBuilder
    '        Dim strLineDetail As New StringBuilder
    '        Dim StrHeader, StrBody, StrFooter As String
    '        Dim TermsNcond, StrPrintHeaderLine, StrPrintFooterLine As String
    '        Dim strLine As String
    '        Dim strDblLine As String

    '        StrHeader = "Gift Voucher No:- " & VoucherNo & vbCrLf
    '        strLine = "----------------------------------------" & vbCrLf
    '        strDblLine = "========================================" & vbCrLf
    '        StrBody = "Amount :-" & Amt & vbCrLf
    '        StrFooter = "Valid Upto:-" & ValidUpto & Space(5) & "Valid days:-" & validDays & vbCrLf
    '        StrFooter = StrFooter & "Valid on all Store's"

    '        strPrintMsg = New StringBuilder()
    '        strPrintMsg.Append(StrHeader)
    '        strPrintMsg.Append(strLine)
    '        strPrintMsg.Append(StrBody)
    '        strPrintMsg.Append(strLine)
    '        strPrintMsg.Append(StrFooter)
    '        fnPrint(strPrintMsg.ToString(), "PRN")

    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class
