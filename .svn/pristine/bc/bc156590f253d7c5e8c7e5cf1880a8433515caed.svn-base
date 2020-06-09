Imports SpectrumCommon
Imports SpectrumBL
Imports SpectrumPrint

Public Class CreditSaleAdjustment
    Dim CreditSale As ICreditSale = New ClsCreditSale()
    Dim BillNo As String = ""
    Private Sub CreditSaleAdjustment_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            'cmbType.DataSource = [Enum].GetNames(GetType(CreditSaleSearchType))
            cmbType.DataMode = C1.Win.C1List.DataModeEnum.AddItem
            cmbType.AddItem(GetEnumDescription(CreditSaleSearchType.CashMemo))
            cmbType.AddItem(GetEnumDescription(CreditSaleSearchType.SalesOrder))
            cmbType.AddItem(GetEnumDescription(CreditSaleSearchType.Customer))
            cmbType.AddItem(GetEnumDescription(CreditSaleSearchType.SalesPerson))

            grdMain.Rows.RemoveRange(1, grdMain.Rows.Count - 1)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.ShowIcon = False
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim displayGridColumns As String = "InvoiceNumber, CustomerID, CustomerName, BalanceAmt, SalesPerson"
            grdMain.DataSource = Nothing

            If cmbType.Text.Replace(Space(1), String.Empty) = CreditSaleSearchType.CashMemo.ToString() Then
                Dim CashmemoList = CreditSale.GetCreditCashMemo()

                Dim popup As New frmPropmt(CashmemoList, "InvoiceNumber", getValueByKey("Crs012"), displayGridColumns)
                popup.ShowDialog()

                If Not popup.SelectedVal = "" Then
                    grdMain.DataSource = CashmemoList.Where(Function(c) c.InvoiceNumber = popup.SelectedVal).ToList()
                    grdMain.Visible = True
                    HideColumns(displayGridColumns)
                End If
            ElseIf cmbType.Text.Replace(Space(1), String.Empty) = CreditSaleSearchType.SalesOrder.ToString() Then
                Dim SalesorderList = CreditSale.GetCreditSalesOrder()

                Dim popup As New frmPropmt(SalesorderList, "InvoiceNumber", getValueByKey("Crs013"), displayGridColumns)
                popup.ShowDialog()

                If Not popup.SelectedVal = "" Then
                    grdMain.DataSource = SalesorderList.Where(Function(x) x.InvoiceNumber = popup.SelectedVal).ToList()
                    grdMain.Visible = True
                    HideColumns(displayGridColumns)
                End If

            ElseIf cmbType.Text.Replace(Space(1), String.Empty) = CreditSaleSearchType.Customer.ToString() Then
                Dim CustomerList = CreditSale.GetCustomers()
                Dim displayCustomerColumns As String = "CardNumber, FirstName, LastName, Gender, MobileNo, EmailId, BirthDate"

                Dim popup As New frmPropmt(CustomerList, "CardNumber", getValueByKey("Crs014"), displayCustomerColumns)
                popup.ShowDialog()
                If Not popup.SelectedVal = "" Then
                    Dim SalesByCustomer = CreditSale.getBillbyCustomer(popup.SelectedVal)

                    popup = New frmPropmt(SalesByCustomer, "InvoiceNumber", getValueByKey("Crs013"), displayGridColumns)
                    popup.ShowDialog()
                    If Not popup.SelectedVal = "" Then
                        grdMain.DataSource = SalesByCustomer.Where(Function(x) x.InvoiceNumber = popup.SelectedVal).ToList()
                        grdMain.Visible = True
                        HideColumns(displayGridColumns)
                    End If

                    'Else
                    '    MessageBox.Show(getValueByKey("Crs015"))
                End If
            ElseIf cmbType.Text.Replace(Space(1), String.Empty) = CreditSaleSearchType.SalesPerson.ToString() Then
                Dim SalesorderList = CreditSale.GetCreditBillByDeliveryPerson()

                Dim popup As New frmPropmt(SalesorderList, "InvoiceNumber", getValueByKey("Crs023"), displayGridColumns)
                popup.ShowDialog()

                If Not popup.SelectedVal = "" Then
                    grdMain.DataSource = SalesorderList.Where(Function(x) x.InvoiceNumber = popup.SelectedVal).ToList()
                    grdMain.Visible = True
                    HideColumns(displayGridColumns)
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub HideColumns(ByVal hidcollist As String)
        If Not hidcollist Is Nothing Then
            For index = 0 To grdMain.Cols.Count - 1
                If Not (hidcollist.Contains(grdMain.Cols(index).Name)) Then
                    grdMain.Cols(index).Visible = False
                End If
            Next

            grdMain.ExtendLastCol = True
            grdMain.AutoSizeCols()
        End If
    End Sub

    Private Sub btnPay_Click(sender As System.Object, e As System.EventArgs) Handles btnPay.Click
        Try
            If grdMain.Visible AndAlso grdMain.Rows.Count > 1 Then

                Dim howmuchtopay As New frmNHowMuchtoPay()
                howmuchtopay.CtrlTxtMinAmt.Text = 1
                howmuchtopay.TotalBalAmt = Convert.ToDouble(grdMain.Rows(grdMain.RowSel)("balanceamt").ToString())
                howmuchtopay.ctrlTxtHowMuchPay.Text = 0
                howmuchtopay.CtrlTxtPickAmt.Text = 0
                howmuchtopay.PickupAmountVisiable = False
                howmuchtopay.ShowDialog()

                If howmuchtopay.blnAllowtoGoPaymentScreen Then
                    If CDbl(howmuchtopay.ctrlTxtHowMuchPay.Text) > 0.0 Then
                        Dim payment As New frmNAcceptPayment()
                        payment.CreditSettlement = True
                        ' payment.TotalBillAmount =
                        payment.CustomerWantPay = howmuchtopay.ctrlTxtHowMuchPay.Text

                        payment.ShowDialog()

                        If payment.IsCancelAcceptPayment = False Then
                            'If BillNo = "" Then
                            Dim objCM As New clsCashMemo()
                            Dim docno As String = objCM.getDocumentNo("CM", clsAdmin.SiteCode)
                            'BillNo = GenDocNo("CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno
                            ''GST changes by ketan add sitecode in billno
                            ' BillNo = GenDocNo("CM" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                            BillNo = GenDocNo("C" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 3, 3) & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)

                            'End If
                            Dim trans = PrepareData(payment.ReciptTotalAmount)

                            If CreditSale.UpdateCredit(trans, clsAdmin.DayOpenDate) Then
                                Dim Printer As New clsPrintCreditSettlementNote()
                                Printer.PrintNote(trans, dtPrinterInfo)
                                MessageBox.Show(getValueByKey("Crs008"))
                            Else
                                MessageBox.Show(getValueByKey("Crs009"))
                            End If
                            grdMain.Visible = False
                        End If
                    Else
                        MessageBox.Show(getValueByKey("Crs010"))

                    End If

                End If
            Else
                MessageBox.Show(getValueByKey("Crs011"))

            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Function PrepareData(ByVal ds As DataSet) As List(Of SalesInvoice)
        Dim ReceiptList As New List(Of SalesInvoice)
        For Each dr As DataRow In ds.Tables(0).Rows
            Dim salesinvoice As New SalesInvoice()
            salesinvoice.BillNO = BillNo
            salesinvoice.AmountTendered = dr("Amount")
            salesinvoice.BankNO = dr("BankAccNO").ToString()
            salesinvoice.CardNO = dr("Number").ToString()
            salesinvoice.TerminalID = clsAdmin.TerminalID
            salesinvoice.AmountInCurrency = If(dr("AmountInCurrency").ToString() = "", 0.0, CDbl(dr("AmountInCurrency").ToString()))
            salesinvoice.CurrencyCode = dr("CurrencyCode").ToString()
            salesinvoice.DocType = If(grdMain.Rows(grdMain.RowSel)("DocType").ToString().ToLower() = "Cash memo".ToLower(), "CM", "SO")
            salesinvoice.ExchangeRate = If(dr("ExchangeRate").ToString() = "", 0.0, CDec(dr("ExchangeRate").ToString()))
            salesinvoice.FinYear = clsAdmin.Financialyear
            salesinvoice.InvoiceNumber = grdMain.Rows(grdMain.RowSel)("InvoiceNumber").ToString()
            salesinvoice.IsNew = True
            salesinvoice.LineNo = dr("SrNO")
            salesinvoice.RecTime = clsAdmin.DayOpenDate
            salesinvoice.SiteCode = clsAdmin.SiteCode
            salesinvoice.Status = True
            salesinvoice.TenderHeadCode = dr("RECIEPTTYPE")
            salesinvoice.TenderTypeCode = dr("RECIEPTTYPECODE")
            salesinvoice.CreatedAt = clsAdmin.SiteCode
            salesinvoice.CreatedBy = clsAdmin.UserCode
            salesinvoice.CreatedOn = DateTime.Now

            salesinvoice.UpdatedAt = clsAdmin.SiteCode
            salesinvoice.UpdatedBy = clsAdmin.UserCode
            salesinvoice.UpdatedOn = DateTime.Now
            ReceiptList.Add(salesinvoice)
        Next
        Return ReceiptList
    End Function

    Private Sub TableLayoutPanel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub
End Class