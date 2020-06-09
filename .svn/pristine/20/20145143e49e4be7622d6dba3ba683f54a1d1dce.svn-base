Imports SpectrumBL
Imports SpectrumPrint

Public Class frmNChangeTenderMode
    Private objDoc As New frmNSearchOrderSOBL
    Private clsInvc As New clsChangeTender
    Private clsCommon As New clsCommon

    Private SelectDocNo As String = String.Empty
    Private vSiteCode, vFinYear, vCustomerNo As String
    Private vCurrentDate As DateTime
    Private vDocType As String = "SO201"

    Private dtInvoiceInfo As New DataTable
    Private dsInvoiceAudit As New DataSet
    Private dsInvoice As New DataSet
    Private dsPayment As New DataSet
    Private dtPayment As New DataTable

    Private Sub frmNChangeTenderMode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            vSiteCode = clsAdmin.SiteCode
            vCurrentDate = clsAdmin.CurrentDate
            vFinYear = clsAdmin.Financialyear

            BtnChangeTenderMode.Enabled = False

            If CheckAuthorisation(clsAdmin.UserCode, "BLMain") Then
                RadioBirthList.Visible = True
            Else
                RadioBirthList.Visible = False
            End If

            SetCulture(Me, Me.Name)
            grdInvoiceInfo.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.None
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CTM003"), "CTM003 - " & getValueByKey("CLAE05"))
        End Try
    End Sub

    Private Sub BtnDocSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDocSearch.Click
        Try
            SelectDocNo = String.Empty
            txtDocNumber.Text = String.Empty

            If RadioSalesOrder.Checked = True Then
                objDoc.DocumentType = "SalesOrder"
                objDoc.Text = getValueByKey("CTM009")
                vDocType = "SalesOrder"
            ElseIf RadioBirthList.Checked = True Then
                objDoc.DocumentType = "BirthList"
                objDoc.Text = getValueByKey("CTM010")
                vDocType = "BLS"
            End If

            If objDoc.ShowDialog() = Windows.Forms.DialogResult.OK Then
                SelectDocNo = objDoc.DocumentNo
                txtDocNumber.Text = SelectDocNo.Trim

                dsInvoice = clsInvc.GetInvoiceInfo(vSiteCode, vFinYear, SelectDocNo, objDoc.DocumentType, clsAdmin.DayOpenDate.ToString("yyyyMMdd"))
                RefreshInvoiceGrid()
            Else
                Exit Sub
            End If

            BtnChangeTenderMode.Enabled = False
            BtnUpdateInvoice.Enabled = False

        Catch ex As Exception
            ShowMessage(getValueByKey("CTM004"), "CTM004 - " & getValueByKey("CLAE05"))
        End Try
    End Sub

    Private Sub txtDocNumber_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtDocNumber.PreviewKeyDown
        Try
            If e.KeyCode = Keys.Enter Then

                If String.IsNullOrEmpty(txtDocNumber.Text) = False Then
                    dsInvoice = clsInvc.GetInvoiceInfo(vSiteCode, vFinYear, txtDocNumber.Text.Trim, IIf(RadioSalesOrder.Checked = True, "SalesOrder", "BirthList"), clsAdmin.DayOpenDate.ToString("yyyyMMdd"))
                    RefreshInvoiceGrid()
                End If
            End If

            BtnChangeTenderMode.Enabled = False
            BtnUpdateInvoice.Enabled = False

        Catch ex As Exception
            ShowMessage(getValueByKey("CTM004"), "CTM004 - " & getValueByKey("CLAE04"))
        End Try
    End Sub

    Private Sub RefreshInvoiceGrid()
        Try
            grdInvoiceInfo.AutoResize = True
            grdInvoiceInfo.AutoGenerateColumns = True
            grdInvoiceInfo.DataSource = dsInvoice.Tables("SalesOrderInfo")
            assignGridHeaders()

            If (dsInvoice.Tables("SalesOrderInfo") IsNot Nothing AndAlso dsInvoice.Tables("SalesOrderInfo").Rows.Count > 0) Then
                vCustomerNo = dsInvoice.Tables("SalesOrderInfo").Rows(0)("CustomerNo").ToString()
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CTM004"), "CTM004 - " & getValueByKey("CLAE05"))
        End Try
    End Sub

    Private Sub RadioSalesOrder_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioSalesOrder.CheckedChanged
        ClearFieldValue()
    End Sub

    Private Sub RadioBirthList_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioBirthList.CheckedChanged
        ClearFieldValue()
    End Sub

    Private Sub ClearFieldValue()
        Try
            txtDocNumber.Text = String.Empty

            lblInvoiceNo.Text = String.Empty
            lblInvoiceDate.Text = String.Empty
            lblDocumentNo.Text = String.Empty
            lblInvoiceAmount.Text = String.Empty

            lblCreatedBy.Text = String.Empty
            lblCreatedOn.Text = String.Empty
            lblCreatedDate.Text = String.Empty
            lblUpdatedBy.Text = String.Empty
            lblUpdatedOn.Text = String.Empty
            lblUpdatedDate.Text = String.Empty

            lblNewInvoiceAmt.Text = String.Empty
            BtnChangeTenderMode.Enabled = False

            If dsInvoice.Tables.Count > 0 Then
                dsInvoice.Tables("SalesOrderInfo").Rows.Clear()
            End If

            dsPayment.Clear()
            RefreshInvoiceGrid()

        Catch ex As Exception
            ShowMessage(getValueByKey("CTM005"), "CTM005 - " & getValueByKey("CLAE05"))
        End Try
    End Sub

    Private Sub grdInvoiceInfo_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdInvoiceInfo.MouseClick
        Try
            If grdInvoiceInfo.Rows.Count > 1 Then
                lblInvoiceNo.Text = grdInvoiceInfo.Item(grdInvoiceInfo.Row, "InvoiceNo").ToString
                lblInvoiceDate.Text = grdInvoiceInfo.Item(grdInvoiceInfo.Row, "CreatedOn").ToString
                lblDocumentNo.Text = grdInvoiceInfo.Item(grdInvoiceInfo.Row, "DocNo").ToString
                lblInvoiceAmount.Text = FormatNumber(CDbl(grdInvoiceInfo.Item(grdInvoiceInfo.Row, "AmtReceivedToday").ToString), 2)

                lblCreatedBy.Text = grdInvoiceInfo.Item(grdInvoiceInfo.Row, "CreatedBy").ToString
                lblCreatedOn.Text = grdInvoiceInfo.Item(grdInvoiceInfo.Row, "CreatedOn").ToString
                lblCreatedDate.Text = grdInvoiceInfo.Item(grdInvoiceInfo.Row, "CreatedOn").ToString
                lblUpdatedBy.Text = grdInvoiceInfo.Item(grdInvoiceInfo.Row, "LastUpdatedBy").ToString
                lblUpdatedOn.Text = grdInvoiceInfo.Item(grdInvoiceInfo.Row, "UpdatedOn").ToString
                lblUpdatedDate.Text = grdInvoiceInfo.Item(grdInvoiceInfo.Row, "UpdatedOn").ToString

                BtnChangeTenderMode.Enabled = True
                BtnUpdateInvoice.Enabled = False
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CTM006"), "CTM006 - " & getValueByKey("CLAE05"))
        End Try
    End Sub

    Private Sub BtnChangeTenderMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnChangeTenderMode.Click
        Try
            Dim drNewInvc As DataRow

            dtInvoiceInfo = clsCommon.GetPaymentInfo
            dtInvoiceInfo.Rows.Clear()

            dsPayment = New DataSet
            dsPayment.Tables.Add(dtInvoiceInfo)
            dsPayment.Tables(0).TableName = "MSTRecieptType"

            Dim dvInvoice As New DataView(dsInvoice.Tables("SalesInvoice"), "SaleInvNumber='" & lblInvoiceNo.Text.Trim & "'", String.Empty, DataViewRowState.CurrentRows)
            dsInvoice.Tables("SalesInvoiceAudit").Merge(dvInvoice.ToTable)
            Dim docno = dsInvoice.Tables("SalesOrderInfo").Rows(0)("DocNo").ToString()
            For Each drOldInvc As DataRow In dsInvoice.Tables("SalesInvoice").Select("SaleInvNumber='" & lblInvoiceNo.Text.Trim & "'")
                drNewInvc = dsPayment.Tables("MSTRecieptType").NewRow()
                drNewInvc("SrNo") = drOldInvc("SaleInvLineNumber")
                'drNewInvc("Reciept") = drOldInvc("TenderHeadCode") commented by rama as it is wrongly assigned
                drNewInvc("Reciept") = drOldInvc("Receipt")
                drNewInvc("RecieptType") = drOldInvc("TenderHeadCode")
                drNewInvc("Amount") = drOldInvc("AmountTendered")
                drNewInvc("Number") = drOldInvc("RefNo_2")
                drNewInvc("Date") = drOldInvc("SOInvDate").date
                drNewInvc("RecieptTypeCode") = drOldInvc("TenderTypeCode")
                drNewInvc("ExchangeRate") = drOldInvc("ExchangeRate")
                drNewInvc("CurrencyCode") = drOldInvc("CurrencyCode")
                drNewInvc("AmountInCurrency") = DBNull.Value

                dsPayment.Tables("MSTRecieptType").Rows.Add(drNewInvc)
            Next
            '---------------------------------------------------------------
            Dim objCls As New clsChangeTender
            Dim dts As New DataTable
            dts = objCls.IsTotalPickUp(vSiteCode, vFinYear, txtDocNumber.Text)
            Dim TotalPickUpAmt As Integer = 0
            If dts.Rows.Count > 0 Then
                For index = 0 To dts.Rows.Count - 1
                    Dim netamt = dts.Rows(index)("NetAmount")
                    Dim deliveredQty = dts.Rows(index)("DeliveredQty")
                    TotalPickUpAmt = Decimal.Add(netamt * deliveredQty, TotalPickUpAmt)
                Next
            End If
            Dim objPayment As New frmNAcceptPayment()
            'objPayment.TotalBillAmount = CDbl(lblInvoiceAmount.Text.Trim)
            objPayment.TotalBillAmount = dsPayment.Tables("MSTRecieptType").Compute("Sum(Amount)", "")
            'objPayment.ParentRelation = "CashMemo"
            objPayment.AcceptEditBillDataSet = dsPayment
            objPayment.PaymentType = clsAcceptPayment.PaymentType.EditBill

            objPayment.TopMost = False
            objPayment.btnSave.Visible = False
            objPayment.btnGift.Visible = False
            objPayment.lblMinimumBalanceAmount.Visible = False
            objPayment.lblCalMinBalDue.Visible = False
            objPayment.lblCurrencyMinimumBalAmt.Visible = False
            objPayment.lblCalCurrencyMiniBalDue.Visible = False

            objPayment.btnOK.Visible = True
            objPayment.RoundAt = clsDefaultConfiguration.BillRoundOffAt

            objPayment.CLPCustomerCardNumber = vCustomerNo
            'objPayment.CLPCustomerName = CustInfo.CtrltxtCustomerName.Text
            ' --0012517: SO : User should not be able to select Tender mode 'Credit Sale' if not picking up the article 
            objPayment.AvoidCreditSalesTender = Not (clsInvc.IsSOPickupDone(dsInvoice.Tables("SalesInvoice").Rows(0)("SiteCode").ToString(), dsInvoice.Tables("SalesInvoice").Rows(0)("FinYear").ToString(), dsInvoice.Tables("SalesInvoice").Rows(0)("DocumentNumber").ToString()))
            objPayment.TotalPick = TotalPickUpAmt
            objPayment.IsChangeTender = True
            objPayment.isChangeTenderMode = True
            objPayment.ShowDialog()

            dsPayment = objPayment.ReciptTotalAmount()

            'If objPayment.DialogResult.ToString.ToUpper.Trim = "OK" Then
            If objPayment.blnChangeModeFormOk = True Then
                objPayment.blnChangeModeFormOk = False
                If dsPayment.Tables(0).Rows.Count > 0 Then
                    BtnUpdateInvoice.Enabled = True

                    lblNewInvoiceAmt.Text = FormatNumber(dsPayment.Tables(0).Compute("Sum(Amount)", String.Empty), 2)

                    BtnUpdateInvoice_Click(sender, New System.EventArgs)
                    clsInvc.UpdateCashier(clsAdmin.UserCode, clsAdmin.SiteCode, clsAdmin.TerminalID, docno)

                    AutoLogout()
                End If
            Else
                lblNewInvoiceAmt.Text = String.Empty
            End If

            objPayment.Close()
        Catch ex As Exception
            ShowMessage(getValueByKey("CTM007"), "CTM007 - " & getValueByKey("CLAE05"))
        End Try
    End Sub
   
    Private Sub BtnUpdateInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdateInvoice.Click
        Try
            '---Commented By Mahesh Mantis issue 0011826: Functionality of change tender type for creating Sales order with multi tender tender payment is not working
            'If Not (CDbl(lblNewInvoiceAmt.Text) = CDbl(lblInvoiceAmount.Text)) Then
            '    ShowMessage(getValueByKey("CTM008"), "CTM008 - " & getValueByKey("CLAE04"))
            '    Exit Sub
            'End If

            If dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables(0).Rows.Count > 0 Then
                dsInvoice.Tables("SalesInvoice").AcceptChanges()

                'vSalesInvcNo = "CM" & clsAdmin.TerminalID & objComn.getDocumentNo("CM")
                'dsInvoice.Tables("SalesInvoiceAudit")

                Dim ChangeOrderNo As Integer = clsInvc.GetChangeHistoryNo(vSiteCode, vFinYear, txtDocNumber.Text.Trim, lblInvoiceNo.Text.Trim)
                For Each drAudit As DataRow In dsInvoice.Tables("SalesInvoiceAudit").Rows
                    drAudit("InvoiceChangeNumber") = ChangeOrderNo
                Next

                If PrepareInvcDataforSave(dsInvoice) = False Then
                    Exit Sub
                End If

                'clsCommon.DeleteCheckDtls(dsInvoice, "SO", dsInvoice.Tables("CheckDtls"), lblInvoiceNo.Text.Trim, vSiteCode)


                Dim dsdata As DataSet = dsInvoice.Copy()
                If dsdata.Tables("SalesInvoice").Columns.Contains("Receipt") Then
                    dsdata.Tables("SalesInvoice").Columns.Remove("Receipt")
                End If
                If dsdata.Tables("SalesInvoiceAudit").Columns.Contains("Receipt") Then
                    dsdata.Tables("SalesInvoiceAudit").Columns.Remove("Receipt")
                End If
                If clsInvc.PrepareSaveInvoiceData(dsdata, vSiteCode, vFinYear, SelectDocNo, lblInvoiceNo.Text.Trim, clsAdmin.UserCode, clsAdmin.CVProgram, clsAdmin.DayOpenDate, vDocType, clsAdmin.CreditValidDays) = True Then
                    ShowMessage(getValueByKey("CTM001"), "CTM001 - " & getValueByKey("CLAE04"))
                    PrintVoucher(dsdata)
                    ClearFieldValue()
                End If
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CTM002"), "CTM002 - " & getValueByKey("CLAE05"))
        End Try

    End Sub

    


    Private Function PrintVoucher(ByVal ds As DataSet) As Boolean
        Try
            If Not ds Is Nothing AndAlso ds.Tables.Contains("SalesInvoice") Then
                Dim clsVoucher As New clsPrintVoucher
                For Each dr As DataRow In ds.Tables("SalesInvoice").Select("TenderTypeCode='CreditVouc(I)'", "", DataViewRowState.CurrentRows)
                    Dim TotalPay As Decimal = IIf(dr("AmountTendered") > 0, dr("AmountTendered"), dr("AmountTendered") * -1)
                    'objPrint.PrintVoucher("CMS", TotalPay, clsDefaultConfiguration.VoucherText, clsAdmin.SiteCode, Errormsg, clsAdmin.CurrencyCode)
                    clsVoucher.PrintGiftVoucherAndCreditNote(vDocType, clsAdmin.SiteCode, "CreditNote", "", TotalPay, String.Empty, clsAdmin.UserName, txtDocNumber.Text.Trim, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                Next
                For Each dr As DataRow In ds.Tables("SalesInvoice").Select("TenderTypeCode='GiftVoucher(R)'", "", DataViewRowState.CurrentRows)
                    Dim TotalPay As Decimal = IIf(dr("AmountTendered") > 0, dr("AmountTendered"), dr("AmountTendered") * -1)
                    'objPrint.PrintVoucher("CMS", TotalPay, clsDefaultConfiguration.VoucherText, clsAdmin.SiteCode, Errormsg, clsAdmin.CurrencyCode)
                    clsVoucher.PrintGiftVoucherAndCreditNote(vDocType, clsAdmin.SiteCode, "GiftVoucher", "", TotalPay, String.Empty, clsAdmin.UserName, txtDocNumber.Text.Trim, clsAdmin.CurrencyCode, clsAdmin.CurrentDate, BarCodeType)
                Next
            End If


        Catch ex As Exception

        End Try
    End Function
    Private Function PrepareInvcDataforSave(ByRef dsInvoiceTp As DataSet) As Boolean
        Try
            Dim drInvc As DataRow
            Dim drIndex As Integer = 1
            Dim objComn As New clsCommon
            If Not (dsPayment Is Nothing) AndAlso dsPayment.Tables.Count > 0 AndAlso dsPayment.Tables("MSTRecieptType").Rows.Count > 0 Then
                Dim SaleInvoicelineNo As Integer = Val(dsInvoiceTp.Tables("SalesInvoice").Compute("max(SaleInvLineNumber)", ""))
                For Each drPayment As DataRow In dsPayment.Tables("MSTRecieptType").Rows
                    drInvc = dsInvoiceTp.Tables("SalesInvoice").NewRow()

                    SaleInvoicelineNo += 1

                    drInvc("SiteCode") = vSiteCode
                    drInvc("FinYear") = clsAdmin.Financialyear
                    drInvc("TerminalID") = clsAdmin.TerminalID
                    drInvc("DocumentNumber") = txtDocNumber.Text.Trim
                    drInvc("SaleInvNumber") = lblInvoiceNo.Text.Trim
                    If drInvc.RowState <> DataRowState.Deleted Then
                        drInvc("SaleInvLineNumber") = SaleInvoicelineNo   'added +1 to change line no for the issue no 13798 for updating tendor mode  
                    Else
                        drInvc("SaleInvLineNumber") = drPayment("srno")
                    End If


                    drInvc("DocumentType") = vDocType
                    drInvc("TenderHeadCode") = drPayment("RecieptType")
                    drInvc("TenderTypeCode") = drPayment("RecieptTypeCode")
                    drInvc("AmountTendered") = drPayment("Amount")

                    drInvc("ExchangeRate") = drPayment("ExchangeRate")
                    drInvc("CurrencyCode") = drPayment("CurrencyCode")
                    drInvc("SOInvDate") = clsAdmin.DayOpenDate.Date
                    drInvc("SOInvTime") = Format(vCurrentDate, "hh:mm:ss tt")
                    drInvc("UserName") = clsAdmin.UserName

                    drInvc("ManagersKeytoUpdate") = DBNull.Value
                    drInvc("ChangeLine") = DBNull.Value
                    drInvc("RefNo_1") = clsCommon.ConvertToEnglish(IIf(drPayment("AMOUNTINCURRENCY") Is DBNull.Value, 0, drPayment("AMOUNTINCURRENCY"))) 'drPayment("Number")
                    drInvc("RefNo_2") = drPayment("Number")
                    'drInvc("RefNo_1") = drPayment("Number")
                    'drInvc("RefNo_2") = DBNull.Value
                    drInvc("RefNo_3") = DBNull.Value
                    drInvc("RefNo_4") = DBNull.Value
                    drInvc("RefDate") = vCurrentDate

                    drInvc("CREATEDAT") = vSiteCode
                    'drInvc("CREATEDBY") = clsAdmin.UserName
                    drInvc("CREATEDBY") = clsAdmin.UserCode
                    drInvc("CREATEDON") = vCurrentDate
                    drInvc("UPDATEDAT") = vSiteCode
                    drInvc("UPDATEDBY") = clsAdmin.UserName
                    drInvc("UPDATEDON") = objComn.GetCurrentDate
                    drInvc("STATUS") = True

                    dsInvoiceTp.Tables("SalesInvoice").Rows.Add(drInvc)
                    drIndex += 1
                Next

            End If

            If dsPayment.Tables.Contains("CheckDtls") Then
                Dim tempDtCheckDtls As New DataTable
                tempDtCheckDtls = dsPayment.Tables("CheckDtls").Copy
                tempDtCheckDtls.TableName = "CheckDtls"
                tempDtCheckDtls.AcceptChanges()
                If Not dsInvoiceTp.Tables.Contains("CheckDtls") Then
                    dsInvoiceTp.Tables.Add(tempDtCheckDtls)
                Else
                    dsInvoiceTp.Tables("CheckDtls").Merge(tempDtCheckDtls)
                End If
            End If

            clsCommon.PrepareCreditCheckData(dsInvoiceTp, vSiteCode, clsAdmin.UserName, clsAdmin.Financialyear, vDocType, lblInvoiceNo.Text.Trim, txtDocNumber.Text.Trim, vCurrentDate, Now.Date, String.Empty, "SO", vCurrentDate.Date)
            clsCommon.AddMode(dsInvoiceTp.Tables("CheckDtls"))

            Return True

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))

            Return False
        End Try
    End Function

    Private Sub assignGridHeaders()
        grdInvoiceInfo.Cols("amtreceivedtoday").Caption = getValueByKey("frmnchangetendermode.grdinvoiceinfo.amtreceivedtoday")
        grdInvoiceInfo.Cols("balanceamount").Caption = getValueByKey("frmnchangetendermode.grdinvoiceinfo.balanceamount")
        grdInvoiceInfo.Cols("createdby").Caption = getValueByKey("frmnchangetendermode.grdinvoiceinfo.createdby")
        grdInvoiceInfo.Cols("createdon").Caption = getValueByKey("frmnchangetendermode.grdinvoiceinfo.createdon")
        grdInvoiceInfo.Cols("docno").Caption = getValueByKey("frmnchangetendermode.grdinvoiceinfo.docno")
        grdInvoiceInfo.Cols("invoiceno").Caption = getValueByKey("frmnchangetendermode.grdinvoiceinfo.invoiceno")
        grdInvoiceInfo.Cols("lastupdatedby").Caption = getValueByKey("frmnchangetendermode.grdinvoiceinfo.lastupdatedby")
        grdInvoiceInfo.Cols("status").Caption = getValueByKey("frmnchangetendermode.grdinvoiceinfo.status")
        grdInvoiceInfo.Cols("totalamtreceived").Caption = getValueByKey("frmnchangetendermode.grdinvoiceinfo.totalamtreceived")
        grdInvoiceInfo.Cols("totalpayableamt").Caption = getValueByKey("frmnchangetendermode.grdinvoiceinfo.totalpayableamt")
        'grdInvoiceInfo.Cols("updatedon").Caption = getValueByKey("frmnchangetendermode.grdinvoiceinfo.updatedon")

    End Sub

    Private Sub frmNChangeTenderMode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "change-tender-mode.htm")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        BtnChangeTenderMode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnChangeTenderMode.BackColor = Color.Transparent
        BtnChangeTenderMode.BackColor = Color.FromArgb(0, 107, 163)
        BtnChangeTenderMode.ForeColor = Color.FromArgb(255, 255, 255)
        BtnChangeTenderMode.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnChangeTenderMode.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnChangeTenderMode.FlatStyle = FlatStyle.Flat
        BtnChangeTenderMode.FlatAppearance.BorderSize = 0
        BtnChangeTenderMode.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        BtnChangeTenderMode.Size = New Size(138, 30)

        BtnDocSearch.Size = New Size(40, 21)
        BtnDocSearch.Image = Nothing
        BtnDocSearch.VisualStyle = C1.Win.C1Input.VisualStyle.System
        BtnDocSearch.BackgroundImage = My.Resources.SearchItems1
        BtnDocSearch.FlatStyle = FlatStyle.Flat
        BtnDocSearch.BackgroundImageLayout = ImageLayout.Stretch
        BtnDocSearch.FlatAppearance.BorderSize = 0
        BtnDocSearch.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)

        BtnUpdateInvoice.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnUpdateInvoice.BackColor = Color.Transparent
        BtnUpdateInvoice.BackColor = Color.FromArgb(0, 107, 163)
        BtnUpdateInvoice.ForeColor = Color.FromArgb(255, 255, 255)
        BtnUpdateInvoice.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnUpdateInvoice.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnUpdateInvoice.FlatStyle = FlatStyle.Flat
        BtnUpdateInvoice.FlatAppearance.BorderSize = 0
        BtnUpdateInvoice.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        BtnUpdateInvoice.Size = New Size(138, 30)
        PanelUpdateButton.Size = New Size(786, 30)

        RadioSalesOrder.ForeColor = Color.White
        RadioBirthList.ForeColor = Color.White

        grdInvoiceInfo.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        grdInvoiceInfo.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        grdInvoiceInfo.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        grdInvoiceInfo.Rows.MinSize = 25
        grdInvoiceInfo.Styles.Normal.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        grdInvoiceInfo.Styles.Fixed.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        grdInvoiceInfo.Styles.Highlight.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        grdInvoiceInfo.Styles.Highlight.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        grdInvoiceInfo.Styles.Focus.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        grdInvoiceInfo.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)

        LbChangeTenderType.BackColor = Color.FromArgb(212, 212, 212)
        Me.LbNewInvoiceAmt.BackColor = Color.FromArgb(212, 212, 212)
        Me.LbInvoiceNo.BackColor = Color.FromArgb(212, 212, 212)

        Me.LbInvoiceDate.BackColor = Color.FromArgb(212, 212, 212)

        Me.LbCreatedBy.BackColor = Color.FromArgb(212, 212, 212)

        Me.LbCreatedOn.BackColor = Color.FromArgb(212, 212, 212)

        Me.LbCreatedDate.BackColor = Color.FromArgb(212, 212, 212)


        Me.LbUpdatedDate.BackColor = Color.FromArgb(212, 212, 212)

        Me.LbUpdatedOn.BackColor = Color.FromArgb(212, 212, 212)

        Me.LbUpdatedBy.BackColor = Color.FromArgb(212, 212, 212)

        Me.LbInvoiceAmount.BackColor = Color.FromArgb(212, 212, 212)

        Me.LbDocumentNo.BackColor = Color.FromArgb(212, 212, 212)
        Me.BtnDocSearch.BackColor = Color.FromArgb(212, 212, 212)
        '  Me.txtDocNumber.BackColor = Color.FromArgb(212, 212, 212)
        Me.LbDocNumber.BackColor = Color.FromArgb(212, 212, 212)


        Me.LbInvoiceNo.Size = New Size(107, 25)

        Me.LbInvoiceDate.Size = New Size(107, 25)
        Me.LbCreatedBy.Size = New Size(107, 25)

        Me.LbCreatedOn.Size = New Size(107, 25)
        Me.LbCreatedDate.Size = New Size(107, 25)
        Me.LbUpdatedDate.Size = New Size(107, 25)

        Me.LbUpdatedOn.Size = New Size(107, 25)
        Me.LbUpdatedBy.Size = New Size(107, 25)
        Me.LbInvoiceAmount.Size = New Size(107, 25)

        Me.LbDocumentNo.Size = New Size(107, 25)
        ' Me.BtnDocSearch.Size = New Size(107, 25)
        ' Me.txtDocNumber.Size = New Size(107, 25)
        ' Me.LbDocNumber.Size = New Size(107, 25)
    End Function

End Class