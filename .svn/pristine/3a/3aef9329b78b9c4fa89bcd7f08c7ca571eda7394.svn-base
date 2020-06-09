Imports SpectrumPrint
Imports SpectrumBL
Imports SpectrumCommon
Imports Microsoft.Reporting.WinForms
Imports Spire.Pdf
Imports System.IO
Imports C1.Win.C1BarCode

Public Class frmNCreditSalesNew

    Public Sub New(ByVal IsMdiformCall As Boolean)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        If clsDefaultConfiguration.TillOperationRequired = True And clsDefaultConfiguration.TillOpenDone = False Then
            ShowMessage(getValueByKey("CM057"), "CM057 - " & getValueByKey("CLAE04"))
            Me.Dispose()
            Me.Close()
            Exit Sub
        End If
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False

        If (Not IsMdiformCall) Then
            Me.WindowState = FormWindowState.Maximized
            Me.AutoSize = False
            Me.MaximumSize = New Point(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
            Me.MinimumSize = New Point(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
        Else
            Me.WindowState = FormWindowState.Normal
        End If

    End Sub

    Private objCreditSales As New ClsCreditSale
    Private _dtCreditSales As DataTable
    Dim BillNo As String
    '------ For Print 
    Dim objPrint As New clsReprintBill
    Dim dtSaleItems As DataTable
    Dim dtPayment As DataTable
    Dim dtCustInfo As New DataTable
    Dim objCharge As New clsSalesOrder
    Private IsTenderCash As Boolean = False
    Private IsTenderCheque As Boolean = False
    Private IsTenderCreditCard As Boolean = False
    Dim IsArticleWiseKot As Boolean = False
    Dim IsCounterCopy As Boolean = False
    Dim IsFinalReceipt As Boolean = False
    Dim FloatAmt As Double
    Public Property dtCreditSales() As DataTable
        Get
            Return _dtCreditSales
        End Get
        Set(ByVal value As DataTable)
            _dtCreditSales = value
        End Set
    End Property

    Private Sub grdCreditSales_DoubleClick(sender As System.Object, e As System.EventArgs) Handles grdCreditSales.DoubleClick
        Try
            ' Call PaymentCreditSales(TenderMode:="Cash")  vipin Feature should be remove
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub grdCreditSales_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdCreditSales.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                ' Call PaymentCreditSales(TenderMode:="Cash")
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub SetResourcesText()
        Try
            Me.Text = getValueByKey("frmNCreditSales")
            Me.lblsearch.Text = getValueByKey("frmNCreditSales.lblsearch")
            Me.cmdPayments.Text = getValueByKey("frmNCreditSales.cmdPayments")
            Me.cmdCash.Text = getValueByKey("frmNCreditSales.cmdCash")
            Me.cmdCard.Text = getValueByKey("frmNCreditSales.cmdCard")
            Me.cmdCheque.Text = getValueByKey("frmNCreditSales.cmdCheque")
            Me.cmdPrint.Text = getValueByKey("frmNCreditSales.cmdPrint")
            Me.cmdWriteOff.Text = getValueByKey("frmNCreditSales.cmdWriteOff")
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub txtFilterCreditSales_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtFilterCreditSales.KeyDown
        Try
            If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
                e.Handled = True
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub txtFilterCreditSales_PreviewKeyDown(sender As System.Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtFilterCreditSales.PreviewKeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Call PaymentCreditSales(TenderMode:="Cash")
            ElseIf e.KeyCode = Keys.Up Then
                If (grdCreditSales.Rows.Count > 1) AndAlso grdCreditSales.Row > 1 Then
                    grdCreditSales.Select(grdCreditSales.Row - 1, 3)
                End If
            ElseIf e.KeyCode = Keys.Down Then
                If (grdCreditSales.Rows.Count > 1) AndAlso grdCreditSales.Row < grdCreditSales.Rows.Count - 1 Then
                    grdCreditSales.Select(grdCreditSales.Row + 1, 3)
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub PaymentCreditSales(ByVal TenderMode As String)
        Try
            If grdCreditSales.Row > 0 Then
                If TenderMode = "Neft" Or TenderMode = "Rtgs" Then   '' added by nikhil
                    Dim objPayment As New frmNAcceptPaymentByNEFTRTGS()
                    objPayment.InvoiceNumber = grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo")
                    objPayment.BillNumber = grdCreditSales.Rows(grdCreditSales.Row)("DocumentNo")
                    objPayment.BillAmount = grdCreditSales.Rows(grdCreditSales.Row)("TotInvAmt")
                    objPayment.BalAmount = grdCreditSales.Rows(grdCreditSales.Row)("BalanceAmount")   ''' 
                    objPayment.DeliveryPerson = IIf(grdCreditSales.Rows(grdCreditSales.Row)("DeliveryPerson") Is DBNull.Value, "", grdCreditSales.Rows(grdCreditSales.Row)("DeliveryPerson"))
                    objPayment.FloatAmtReturned = IIf(grdCreditSales.Rows(grdCreditSales.Row)("FloatAmtReturned") Is String.Empty, 0, grdCreditSales.Rows(grdCreditSales.Row)("FloatAmtReturned"))
                    objPayment.TenderTypeCode = TenderMode

                    If (grdCreditSales.Rows(grdCreditSales.RowSel)("DocType").ToString().ToLower() = "Cash memo".ToLower()) Then
                        objPayment.TranTypeCode = "CM"
                    ElseIf (grdCreditSales.Rows(grdCreditSales.RowSel)("DocType").ToString().ToLower() = "sales order".ToLower()) Then
                        objPayment.TranTypeCode = "SO"
                    Else
                        objPayment.TranTypeCode = "CM"
                    End If
                    objPayment.IsCreditSale = True 'Vipin
                    If (objPayment.ShowDialog() = Windows.Forms.DialogResult.OK) Then

                        If clsDefaultConfiguration.KOTAndBillGeneration = True Then
                            PrintKOT(True)
                            CmdPrint_Click(Nothing, EventArgs.Empty)
                            MessageBox.Show(getValueByKey("Crs008"))
                        End If
                        MessageBox.Show(getValueByKey("Crs008")) 'vipin 28.11.2017
                        frmNCreditSales_Load(Nothing, EventArgs.Empty)
                    End If
                Else
                    'Dim objPayment As New frmNAcceptPaymentByNEFTRTGS()
                    'objPayment.InvoiceNumber = grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo")
                    'objPayment.BillNumber = grdCreditSales.Rows(grdCreditSales.Row)("DocumentNo")
                    'objPayment.BillAmount = grdCreditSales.Rows(grdCreditSales.Row)("TotInvAmt")
                    'objPayment.BalAmount = grdCreditSales.Rows(grdCreditSales.Row)("BalanceAmount")   ''' 
                    'objPayment.DeliveryPerson = IIf(grdCreditSales.Rows(grdCreditSales.Row)("DeliveryPerson") Is DBNull.Value, "", grdCreditSales.Rows(grdCreditSales.Row)("DeliveryPerson"))
                    'objPayment.FloatAmtReturned = IIf(grdCreditSales.Rows(grdCreditSales.Row)("FloatAmtReturned") Is String.Empty, 0, grdCreditSales.Rows(grdCreditSales.Row)("FloatAmtReturned"))
                    'objPayment.TenderTypeCode = TenderMode

                    'If (grdCreditSales.Rows(grdCreditSales.RowSel)("DocType").ToString().ToLower() = "Cash memo".ToLower()) Then
                    '    objPayment.TranTypeCode = "CM"
                    'ElseIf (grdCreditSales.Rows(grdCreditSales.RowSel)("DocType").ToString().ToLower() = "sales order".ToLower()) Then
                    '    objPayment.TranTypeCode = "SO"
                    'Else
                    '    objPayment.TranTypeCode = "CM"
                    'End If

                    'If (objPayment.ShowDialog() = Windows.Forms.DialogResult.OK) Then

                    '    If clsDefaultConfiguration.KOTAndBillGeneration = True Then
                    '        PrintKOT(True)
                    '        CmdPrint_Click(Nothing, EventArgs.Empty)
                    '        MessageBox.Show(getValueByKey("Crs008"))
                    '    End If
                    '    frmNCreditSales_Load(Nothing, EventArgs.Empty)
                    'End If
                    Dim objPaymentCreditSales As New frmPaymentCreditSales()
                    objPaymentCreditSales.InvoiceNumber = grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo")
                    objPaymentCreditSales.BillNumber = grdCreditSales.Rows(grdCreditSales.Row)("DocumentNo")  '' commented by nik BillNo
                    objPaymentCreditSales.BillAmount = grdCreditSales.Rows(grdCreditSales.Row)("TotInvAmt")   '' commented by nik  BillAmount
                    objPaymentCreditSales.BalAmount = grdCreditSales.Rows(grdCreditSales.Row)("BalanceAmount")
                    objPaymentCreditSales.DeliveryPerson = IIf(grdCreditSales.Rows(grdCreditSales.Row)("DeliveryPerson") Is DBNull.Value, "", grdCreditSales.Rows(grdCreditSales.Row)("DeliveryPerson"))
                    objPaymentCreditSales.FloatAmtReturned = IIf(grdCreditSales.Rows(grdCreditSales.Row)("FloatAmtReturned") Is String.Empty, 0, grdCreditSales.Rows(grdCreditSales.Row)("FloatAmtReturned"))
                    objPaymentCreditSales.TenderTypeCode = TenderMode

                    If (grdCreditSales.Rows(grdCreditSales.RowSel)("DocType").ToString().ToLower() = "Cash memo".ToLower()) Then
                        objPaymentCreditSales.TranTypeCode = "CM"
                    ElseIf (grdCreditSales.Rows(grdCreditSales.RowSel)("DocType").ToString().ToLower() = "sales order".ToLower()) Then
                        objPaymentCreditSales.TranTypeCode = "SO"
                    Else
                        objPaymentCreditSales.TranTypeCode = "CM"
                    End If
                    'objPaymentCreditSales.TxtBalAmt.Enabled = False 'vipin
                    If (objPaymentCreditSales.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                        'added by khusrao adil for niq-naq
                        If clsDefaultConfiguration.KOTAndBillGeneration = True Then
                            PrintKOT(True)
                            CmdPrint_Click(Nothing, EventArgs.Empty)
                            MessageBox.Show(getValueByKey("Crs008"))
                        End If
                        frmNCreditSales_Load(Nothing, EventArgs.Empty)
                    End If
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub FilterCreditSales()

        Try
            Dim rowfilterString As String = String.Empty
            Dim filterString As New System.Text.StringBuilder()
            Dim filterText() As String = txtFilterCreditSales.Text.ToString().Trim().Split(Space(1))
            For index = 0 To filterText.Count - 1
                filterString.AppendFormat("Convert( {0}, 'System.String') LIKE '%{1}%' AND ", "FILTER", Replace(filterText(index).ToString(), "'", "''"))
            Next
            rowfilterString = filterString.ToString().Substring(0, filterString.ToString().Length - 4)
            dtCreditSales.DefaultView.RowFilter = rowfilterString
            grdCreditSales.DataSource = dtCreditSales.DefaultView
            GridColumnSettings()
            If (grdCreditSales.Rows.Count > 1) Then
                grdCreditSales.Select(1, 3)
            End If
            txtFilterCreditSales.Select()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub GridColumnSettings()
        Try
            grdCreditSales.AllowEditing = True
            '      If grdCreditSales.Cols.Count > 0 Then
            Dim CouponColumn As String = String.Empty
            If clsDefaultConfiguration.IsTablet = True Then
                CouponColumn = "CouponNo,"
            End If
            '   Dim displayColumns As String = CouponColumn & "InvoiceNo,CustomerName,DeliveryPerson,SalesPerson,BillAmount,BalanceAmount,Address,BillCreatedTime,ElapsedTime,TotalAmt,FloatAmtReturned,Dispatch,DispatchTime,DeliveryPartner"
            Dim displayColumns As String = CouponColumn & "SrNo.,DocumentNo,InvoiceNo,CustomerName,CompanyNAme,TotInvAmt,PaidAmount,BalanceAmount,ContactNo,ADDRESS,Till,Cashier,InvoiceDate,PastDueDates,ElapsedTime,TotalAmt,FloatAmtReturned,Dispatch,DispatchTime,DeliveryPartner,DeliveryPerson,SalesPerson,CardNo,DocType" 'vipin
            Dim columnsList = displayColumns.ToUpper().Split(",")

            For colIndex = 0 To grdCreditSales.Cols.Count - 1 Step 1
                If columnsList.Contains(grdCreditSales.Cols(colIndex).Name.ToUpper()) Then
                    grdCreditSales.Cols(colIndex).Visible = True
                Else
                    grdCreditSales.Cols(colIndex).Visible = False
                End If
                grdCreditSales.Cols(colIndex).AllowEditing = False
            Next
            If clsDefaultConfiguration.KOTAndBillGeneration = True Then
                grdCreditSales.Cols("CouponNo").Caption = "Coupon No"
            End If
            'grdCreditSales.Cols("InvoiceNo").Caption = getValueByKey("frmNCreditSales.grdCreditSales.InvoiceNo")
            'grdCreditSales.Cols("CustomerName").Caption = getValueByKey("frmNCreditSales.grdCreditSales.CustomerName")
            'grdCreditSales.Cols("DeliveryPerson").Caption = getValueByKey("frmNCreditSales.grdCreditSales.DeliveryPerson")
            'grdCreditSales.Cols("SalesPerson").Caption = getValueByKey("frmNCreditSales.grdCreditSales.SalesPerson")
            'grdCreditSales.Cols("BillAmount").Caption = getValueByKey("frmNCreditSales.grdCreditSales.BillAmount")
            'grdCreditSales.Cols("BalanceAmount").Caption = getValueByKey("frmNCreditSales.grdCreditSales.BalanceAmount")
            'grdCreditSales.Cols("Address").Caption = getValueByKey("frmNCreditSales.grdCreditSales.Address")
            'grdCreditSales.Cols("BillCreatedTime").Caption = getValueByKey("frmNCreditSales.grdCreditSales.BillCreatedTime")
            'grdCreditSales.Cols("BillCreatedTime").Format = "g"
            'grdCreditSales.Cols("ElapsedTime").Caption = getValueByKey("frmNCreditSales.grdCreditSales.ElapsedTime")
            'grdCreditSales.Cols("TotalAmt").Caption = "Float Amount"
            'grdCreditSales.Cols("FloatAmtReturned").Caption = "Float Amount Returned"
            'grdCreditSales.Cols("FloatAmtReturned").AllowEditing = True
            'grdCreditSales.Cols("Dispatch").DataType = Type.GetType("System.Boolean")
            'grdCreditSales.Cols("Dispatch").Caption = "Dispatch"
            'grdCreditSales.Cols("DispatchTime").Caption = "Dispatch Time"
            'grdCreditSales.Cols("DispatchTime").Format = "hh:mm tt"
            'grdCreditSales.Cols("DeliveryPartner").Caption = "Delivery Partner"
            'grdCreditSales.Cols("DeliveryPartner").Visible = True
            'grdCreditSales.Cols("DeliveryPartner").AllowEditing = False
            grdCreditSales.Cols("SrNo.").Caption = "Sr No."
            grdCreditSales.Cols("DocumentNo").Caption = "Tax Invoice No."   '' "Tax.Inv.No" ''added by vipin
            grdCreditSales.Cols("InvoiceNo").Caption = "Receipt No."            ''"Rcpt. No."  ''added by vipin
            grdCreditSales.Cols("CustomerName").Caption = "Customer Name"
            grdCreditSales.Cols("CompanyNAme").Caption = "Company Name"
            grdCreditSales.Cols("TotInvAmt").Caption = "Total Credit" & "" & vbCrLf & "" & "Sales Amount "
            grdCreditSales.Cols("PaidAmount").Caption = "Paid Amount"

            grdCreditSales.Cols("BalanceAmount").Caption = "Balance Amt"
            grdCreditSales.Cols("ContactNo").Caption = "Contact No."
            grdCreditSales.Cols("ADDRESS").Caption = "Address"
            grdCreditSales.Cols("Till").Caption = "Till No."
            grdCreditSales.Cols("Cashier").Caption = "Cashier Name"
            grdCreditSales.Cols("InvoiceDate").Caption = "Invoice Date"            ''    "Invoice Date"
            grdCreditSales.Cols("PastDueDates").Caption = "Past Due Days"
            grdCreditSales.Cols("ElapsedTime").Caption = "Elapsed Time"
            grdCreditSales.Cols("TotalAmt").Caption = "Float Amount"
            grdCreditSales.Cols("FloatAmtReturned").Caption = "Float Amount returned"
            grdCreditSales.Cols("FloatAmtReturned").AllowEditing = True
            grdCreditSales.Cols("Dispatch").Caption = "Dispatch"
            grdCreditSales.Cols("Dispatch").DataType = Type.GetType("System.Boolean")
            grdCreditSales.Cols("DispatchTime").Caption = "Dispatch Time"
            grdCreditSales.Cols("DispatchTime").Format = "hh:mm tt"
            grdCreditSales.Cols("DeliveryPartner").Caption = "DeliveryPartner"

            grdCreditSales.Cols("DeliveryPerson").Visible = False
            grdCreditSales.Cols("SalesPerson").Visible = False
            grdCreditSales.Cols("CardNo").Visible = False
            grdCreditSales.Cols("DocType").Visible = False

            grdCreditSales.Cols("InvoiceNo").Width = 150
            grdCreditSales.AutoSizeCols()
            grdCreditSales.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns
            '     End If
            '-------------To Enable Checkbox if Dispatch Time is There and Make Enabling False
            For row = 1 To grdCreditSales.Rows.Count - 1
                If Not String.IsNullOrEmpty(grdCreditSales.Rows(row)("DispatchTime").ToString) Then
                    grdCreditSales.Rows(row)("Dispatch") = True
                End If
                '---------------- added by ketan -------------------------------
                '' Wrap Grid row  Functionality
                Dim InvoiceNo = grdCreditSales.Rows(row)("InvoiceNo").ToString
                If InvoiceNo.Contains(",") Then
                    WrapGridColumnByComma(row, "InvoiceNo")
                    WrapGridColumnByComma(row, "Till")
                    WrapGridColumnByComma(row, "Cashier")
                End If
                WrapGridColumnByLength(row, "CompanyNAme")
                WrapGridColumnByLength(row, "ADDRESS")

            Next
            Me.grdCreditSales.Cols("CompanyNAme").Width = 250
            Me.grdCreditSales.Cols("ADDRESS").Width = 255
            Me.grdCreditSales.Cols("Cashier").Width = 150
            Me.grdCreditSales.Cols("Till").Width = 50
            Me.grdCreditSales.Rows(0).Height = 50
            Me.grdCreditSales.Cols("TotInvAmt").Width = 100
            Me.grdCreditSales.Cols("TotInvAmt").Format = "#,###"
            grdCreditSales.Cols("PaidAmount").Format = "#,###"
            grdCreditSales.Cols("BalanceAmount").Format = "#,###"
            Me.grdCreditSales.Styles.SelectedColumnHeader.Font = New Font(grdCreditSales.Font.Name.ToString, grdCreditSales.Font.Size, FontStyle.Bold)
            '---------------------------------------------------------------

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub WrapGridColumnByComma(ByVal rowid As Integer, ByVal ColumnName As String)
        Try
            Dim InvoiceNo = grdCreditSales.Rows(rowid)(ColumnName).ToString
            If InvoiceNo.Contains(",") Then
                Dim vlue As String = ""
                Dim length As Integer = 0
                Dim arr As String() = Split(InvoiceNo, ",")
                For Each element As String In arr
                    If element <> "" & vbCrLf & "" Then
                        vlue = vlue + element.Trim() & "," & vbCrLf
                        length = length + 1
                    End If
                Next
                grdCreditSales.Rows(rowid)(ColumnName) = vlue.Substring(0, vlue.Length - 3)

                Me.grdCreditSales.Rows(rowid).Height = 20 * length
                Me.grdCreditSales.Cols(ColumnName).Width = 140

            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub WrapGridColumnByLength(ByVal rowid As Integer, ByVal ColumnName As String)
        Try
            Dim ColumnValue = grdCreditSales.Rows(rowid)(ColumnName).ToString
            ColumnValue = ColumnValue.Replace("" & vbCrLf & "", "")
            If ColumnValue.Length > 37 Then
                Dim str1 = System.Text.RegularExpressions.Regex.Replace(ColumnValue.ToString(), ".{35}", "$0" & vbCrLf)
                Dim arr As String() = Split(str1, "" & vbCrLf & "")
                grdCreditSales.Rows(rowid)(ColumnName) = str1
                Me.grdCreditSales.Rows(rowid).Height = 20 * arr.Length
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Sub frmNCreditSales_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Call SetResourcesText()
            Dim objdefaultSO As New clsDefaultConfiguration("SalesOrder")
            objdefaultSO.GetDefaultSettings()

            Dim objdefaultCM As New clsDefaultConfiguration("CMS")
            objdefaultCM.GetDefaultSettings()

            Call BindCreditSales()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            'Checking Authorization for Write Off Transaction
            If CheckAuthorisation(clsAdmin.UserCode, "CRDSale") = False Then
                cmdWriteOff.Enabled = False
                Exit Sub
            End If
            cmdWriteOff.Enabled = clsDefaultConfiguration.AllowCreditSaleWriteOff.Contains(clsAdmin.TerminalID)
            If clsDefaultConfiguration.KOTAndBillGeneration = True Then
                cmdKOTReprint.Visible = True
                If cmdWriteOff.Enabled = False Then
                    cmdKOTReprint.Location = New System.Drawing.Point(0, 0)
                    Me.cmdKOTReprint.Size = New System.Drawing.Size(109, 43)
                    Me.cmdKOTReprint.BringToFront()
                End If
            Else
                cmdKOTReprint.Visible = False
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub EnableDisableCmd(ByVal flagEnableDisable As Boolean)
        If IsTenderCreditCard Then cmdCard.Enabled = flagEnableDisable
        If IsTenderCash Then cmdCash.Enabled = flagEnableDisable
        If IsTenderCheque Then cmdCheque.Enabled = flagEnableDisable
        cmdPayments.Enabled = flagEnableDisable
        cmdPrint.Enabled = flagEnableDisable
        cmdDispatch.Enabled = flagEnableDisable
        cmdWriteOff.Enabled = flagEnableDisable
        cmdKOTReprint.Enabled = flagEnableDisable
    End Sub

    Private Sub BindCreditSales()
        Try
            txtFilterCreditSales.Value = String.Empty
            dtCreditSales = objCreditSales.GetCreditSales(clsAdmin.SiteCode)
            If dtCreditSales IsNot Nothing Then
                'dtCreditSales.DefaultView.Sort = "BillTime DESC"
                dtCreditSales.DefaultView.Sort = "CustomerName"
                If clsDefaultConfiguration.DoneSystemApplicable Then
                    If clsDefaultConfiguration.ExternalOrdersTillNo = clsAdmin.TerminalID Then
                        Dim dr() = dtCreditSales.Select("TerminalId='" & clsDefaultConfiguration.ExternalOrdersTillNo & "'")
                        If dr.Count > 0 Then
                            dtCreditSales = dtCreditSales.Select("TerminalId='" & clsDefaultConfiguration.ExternalOrdersTillNo & "'").CopyToDataTable()
                            '-   dtCreditSales.DefaultView.Sort = "BillTime DESC"
                            dtCreditSales.DefaultView.Sort = "CustomerName"
                        Else
                            dtCreditSales.Rows.Clear()
                        End If
                    Else
                        Dim drt() = dtCreditSales.Select("TerminalId<>'" & clsDefaultConfiguration.ExternalOrdersTillNo & "'")
                        If drt.count > 0 Then
                            dtCreditSales = dtCreditSales.Select("TerminalId<>'" & clsDefaultConfiguration.ExternalOrdersTillNo & "'").CopyToDataTable()
                            ' dtCreditSales.DefaultView.Sort = "BillTime DESC"
                            dtCreditSales.DefaultView.Sort = "CustomerName"
                        Else
                            dtCreditSales.Rows.Clear()
                        End If
                    End If
                End If
                If clsDefaultConfiguration.IsTablet = True Then
                    Dim row As DataRow
                    For Each row In dtCreditSales.Rows
                        'need to set value to NewColumn column
                        'Dim strBillno As String = row("BillNo")
                        'Dim strTillno As String = row("TerminalID")
                        'Dim CouponNo As String = strTillno.Substring(0, 1) & strTillno.Substring(strTillno.Length - 2, 2) & strBillno.Substring(strBillno.Length - 2, 2)
                        ' row("CouponNo") = CouponNo   ' or set it to some other value
                        'row("FILTER") = row("FILTER") & CouponNo
                        ' row("FILTER") = row("FILTER") & row("CouponNo")
                    Next
                End If
                grdCreditSales.DataSource = dtCreditSales.DefaultView
                Call EnableDiableTenderIcons()
                GridColumnSettings()
                If dtCreditSales.Rows.Count > 0 Then
                    grdCreditSales.Select()
                    EnableDisableCmd(True)
                Else
                    EnableDisableCmd(False)
                End If

            End If
            txtFilterCreditSales.Focus()
            txtFilterCreditSales.Select()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub txtFilterCreditSales_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtFilterCreditSales.TextChanged
        Try
            Call FilterCreditSales()
            txtFilterCreditSales.Select()
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub frmNCreditSales_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                dtCreditSales = Nothing
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
            ElseIf e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "credit-sales-adjustment.htm")
            End If

            If e.KeyCode = Keys.F3 AndAlso cmdPrint.Enabled Then CmdPrint_Click(cmdPrint, New System.EventArgs)

            If e.KeyCode = Keys.F4 AndAlso cmdPayments.Enabled Then cmdPayments_Click(cmdPayments, New System.EventArgs)

            If e.KeyCode = Keys.F5 AndAlso cmdCash.Enabled Then cmdCash_Click(cmdCash, New System.EventArgs)

            If e.KeyCode = Keys.F6 AndAlso cmdCard.Enabled Then cmdCard_Click(cmdCard, New System.EventArgs)

            If e.KeyCode = Keys.F7 AndAlso cmdCheque.Enabled Then cmdCheque_Click(cmdCheque, New System.EventArgs)
            'Shortcut Key for WriteOff Screen
            If e.KeyCode = Keys.F8 AndAlso cmdWriteOff.Visible Then cmdWriteOff_Click(cmdWriteOff, New System.EventArgs)

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try


    End Sub

    Dim dsNewTable As New DataSet
    Dim BillNumber As String
    Private Sub cmdPayments_Click(sender As System.Object, e As System.EventArgs) Handles cmdPayments.Click
        Try
            If grdCreditSales.Rows.Count > 1 Then

                Dim payment As New frmNAcceptPaymentPC()
                payment.CreditSettlement = True
                ' payment.TotalBillAmount =
                payment.CustomerWantPay = grdCreditSales.Rows(grdCreditSales.Row)("BalanceAmount")
                If Not IsDBNull(grdCreditSales.Rows(grdCreditSales.Row)("CardNo")) Then
                    payment.CLPCustomerCardNumber = grdCreditSales.Rows(grdCreditSales.Row)("CardNo")
                End If
                Dim DeliveryPerson As String = IIf(grdCreditSales.Rows(grdCreditSales.Row)("DeliveryPerson") Is DBNull.Value, "", grdCreditSales.Rows(grdCreditSales.Row)("DeliveryPerson"))
                Dim FloatAmtReturned As Double = IIf(grdCreditSales.Rows(grdCreditSales.Row)("FloatAmtReturned") Is String.Empty, 0, grdCreditSales.Rows(grdCreditSales.Row)("FloatAmtReturned"))
                BillNumber = grdCreditSales.Rows(grdCreditSales.Row)("DocumentNo")  ''("InvoiceNo") '' commnetd by nik on BillNo
                Dim InvoicBillNumber As String = grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo")

                payment.CustName = grdCreditSales.Rows(grdCreditSales.Row)("CustomerName").ToString
                payment.CompName = grdCreditSales.Rows(grdCreditSales.Row)("CompanyName").ToString
                payment.MobNumber = grdCreditSales.Rows(grdCreditSales.Row)("ContactNo").ToString
                payment.IsCreditSale = True 'vipin
                'payment.lblMinimumBalanceAmount.Visible = False 'vipin 20.12.2017
                payment.lblCurrencyMinimumBalAmt.Visible = False
                payment.lblCalMinBalDue.Visible = False
                payment.lblCalCurrencyMiniBalDue.Visible = False
                payment.ShowDialog()

                If payment.IsCancelAcceptPayment = False Then
                    'If BillNo = "" Then
                    Dim objCM As New clsCashMemo()
                    Dim dtTender = payment.ReciptTotalAmount
                    If Not dtTender Is Nothing Then
                        If dtTender.Tables.Count > 0 Then
                            If dtTender.Tables(0).Rows.Count > 0 Then
                                Dim multTender As String = ""
                                Dim AllRemark As String = ""
                                Dim tendrAmount As String = ""
                                Dim dtnewtbl As New DataTable

                                Dim docType As String = ""   '' added by nikhil

                                If BillNumber <> "" Then
                                    docType = BillNumber.Substring(0, 2)
                                End If
                                If dtTender.Tables.Count > 0 Then
                                    If dtTender.Tables(0).Rows.Count > 0 Then

                                        dtnewtbl = dtTender.Tables(0)

                                        Dim tempdr = dtnewtbl.Select("RecieptTypeCode='Cash'")
                                        If tempdr.Length > 0 Then
                                            '   tendrAmount = tempdr(0)("Amount").ToString
                                            tendrAmount = dtnewtbl.Compute("SUM(Amount)", "RecieptTypeCode='Cash'").ToString
                                        Else
                                            tendrAmount = "0"
                                        End If

                                        Dim tempdr1 = dtnewtbl.Select("RecieptTypeCode='CreditCard'")
                                        If tempdr1.Length > 0 Then
                                            tendrAmount = tendrAmount + "," + dtnewtbl.Compute("SUM(Amount)", "RecieptTypeCode='CreditCard'").ToString
                                        Else
                                            tendrAmount = tendrAmount + "," + "0"
                                        End If

                                        Dim tempdr2 = dtnewtbl.Select("RecieptTypeCode='Cheque'")
                                        If tempdr2.Length > 0 Then
                                            tendrAmount = tendrAmount + "," + dtnewtbl.Compute("SUM(Amount)", "RecieptTypeCode='Cheque'").ToString
                                        Else
                                            tendrAmount = tendrAmount + "," + "0"
                                        End If

                                        Dim tempdr3 = dtnewtbl.Select("RecieptTypeCode='NEFT'")
                                        If tempdr3.Length > 0 Then
                                            tendrAmount = tendrAmount + "," + dtnewtbl.Compute("SUM(Amount)", "RecieptTypeCode='NEFT'").ToString
                                        Else
                                            tendrAmount = tendrAmount + "," + "0"
                                        End If



                                        Dim tempdr4 = dtnewtbl.Select("RecieptTypeCode='RTGS'")
                                        If tempdr4.Length > 0 Then
                                            tendrAmount = tendrAmount + "," + dtnewtbl.Compute("SUM(Amount)", "RecieptTypeCode='RTGS'").ToString

                                        End If





                                        For i = 0 To dtnewtbl.Rows.Count - 1
                                            If multTender <> "" Then
                                                If dtnewtbl.Rows(i)("RecieptType").ToString.Trim = "Neft" Or dtnewtbl.Rows(i)("RecieptType").ToString.Trim = "Rtgs" Then
                                                    multTender = multTender + ", " + "Rs." + objcomm.NumberInIndainFormat(dtnewtbl.Rows(i)("Amount").ToString) + " " + dtnewtbl.Rows(i)("RecieptType").ToString.ToUpper()
                                                Else
                                                    multTender = multTender + ", " + "Rs." + objcomm.NumberInIndainFormat(dtnewtbl.Rows(i)("Amount").ToString) + " " + dtnewtbl.Rows(i)("RecieptType").ToString()
                                                End If
                                            Else
                                                If dtnewtbl.Rows(i)("RecieptType").ToString.Trim = "Neft" Or dtnewtbl.Rows(i)("RecieptType").ToString.Trim = "Rtgs" Then
                                                    multTender = "Rs." + objcomm.NumberInIndainFormat(dtnewtbl.Rows(i)("Amount").ToString) + " " + dtnewtbl.Rows(i)("RecieptType").ToString.ToUpper()
                                                Else
                                                    multTender = "Rs." + objcomm.NumberInIndainFormat(dtnewtbl.Rows(i)("Amount").ToString) + " " + dtnewtbl.Rows(i)("RecieptType").ToString
                                                End If
                                            End If
                                        Next


                                        For i = 0 To dtnewtbl.Rows.Count - 1
                                            If AllRemark <> "" Then
                                                If dtnewtbl.Rows(i)("Remarks").ToString.Trim() <> "-" Then
                                                    If dtnewtbl.Rows(i)("RecieptType").ToString.Trim = "Neft" Or dtnewtbl.Rows(i)("RecieptType").ToString.Trim = "Rtgs" Then
                                                        AllRemark = AllRemark + "##" + dtnewtbl.Rows(i)("RecieptType").ToString.ToUpper() + " - " + dtnewtbl.Rows(i)("Remarks").ToString()
                                                    Else
                                                        AllRemark = AllRemark + "##" + dtnewtbl.Rows(i)("RecieptType").ToString() + " - " + dtnewtbl.Rows(i)("Remarks").ToString()
                                                    End If
                                                End If
                                            Else
                                                If dtnewtbl.Rows(i)("Remarks").ToString.Trim() <> "-" Then
                                                    If dtnewtbl.Rows(i)("RecieptType").ToString.Trim = "Neft" Or dtnewtbl.Rows(i)("RecieptType").ToString.Trim = "Rtgs" Then
                                                        AllRemark = dtnewtbl.Rows(i)("RecieptType").ToString.ToUpper() + " - " + dtnewtbl.Rows(i)("Remarks").ToString()
                                                    Else
                                                        AllRemark = dtnewtbl.Rows(i)("RecieptType").ToString() + " - " + dtnewtbl.Rows(i)("Remarks").ToString()
                                                    End If
                                                End If
                                            End If
                                        Next

                                    End If
                                End If

                                Dim objBLLAcceptPayment As New clsAcceptPayment   ''' added by nikhil for PC to divide the payment for SO with multiple invoice
                                Dim dtInvoice As New DataTable
                                Dim dtmutpleTender As New DataTable
                                '      dtInvoice = objCreditSales.GetBillInvoiceDtlsAgainstSO(BillNumber, docType)
                                'If Not dtInvoice Is Nothing AndAlso dtInvoice.Rows.Count > 1 Then

                                '    dtmutpleTender = objCreditSales.GetMultipleInvoicesAgainstSO(multTender, BillNumber, tendrAmount)
                                'Else
                                '    dtmutpleTender = objCreditSales.GetsingleInvoiceDtlsAgainstSO(multTender, BillNumber, tendrAmount)
                                'End If
                                dtmutpleTender = objCreditSales.SattleCreditSaleUsingMultipleTender(BillNumber, tendrAmount)
                                'dsNewTable.Tables(0).Clear()
                                dsNewTable = objBLLAcceptPayment.GetDataset()
                                If dsNewTable.Tables(0).Columns.Contains("SoInvNumber") = True Then
                                Else
                                    dsNewTable.Tables(0).Columns.Add("SoInvNumber", GetType(String))
                                End If

                                If dtTender.Tables.Count > 0 Then
                                    If dtTender.Tables(0).Rows.Count > 0 Then
                                        For Each dr1 In dtnewtbl.Rows  'vipin 15.11.2017
                                            If dr1("NEFTReferenceNo") <> "-" Then
                                                dr1("CardNo") = dr1("NEFTReferenceNo")
                                                dr1("Number") = dr1("NEFTReferenceNo")
                                            End If

                                            If dr1("RTGSReferenceNo") <> "-" Then
                                                dr1("Number") = dr1("RTGSReferenceNo")
                                                dr1("CardNo") = dr1("RTGSReferenceNo")
                                            End If
                                        Next
                                    End If
                                End If


                                If Not dtmutpleTender Is Nothing AndAlso dtmutpleTender.Rows.Count > 0 Then
                                    ' For j = 0 To dtmutpleTender.Rows.Count - 1
                                    For i = 0 To dtmutpleTender.Rows.Count - 1
                                        Dim Currentender = dtmutpleTender.Rows(i)("Tender").ToString()
                                        Dim dataRow1 = dtnewtbl.Select("RecieptTypeCode='" & Currentender & "'")
                                        Dim dataRow As DataRow
                                        dataRow = dsNewTable.Tables(0).NewRow()
                                        dataRow("SrNo") = i + 1
                                        dataRow("Reciept") = dtmutpleTender.Rows(i)("Tender")
                                        dataRow("RecieptType") = dataRow1(0)("Reciept")
                                        dataRow("RecieptTypeCode") = dtmutpleTender.Rows(i)("Tender")
                                        dataRow("Amount") = dtmutpleTender.Rows(i)("AmountPaid")
                                        dataRow("AmountInCurrency") = dtmutpleTender.Rows(i)("AmountPaid")
                                        ' dataRow("Number") = dtmutpleTender.Rows(i)("AmountPaid")
                                        dataRow("Number") = dataRow1(0)("Number")  'vipin
                                        dataRow("Date") = dataRow1(0)("Date")
                                        dataRow("ExchangeRate") = dataRow1(0)("ExchangeRate")
                                        dataRow("CurrencyCode") = dataRow1(0)("CurrencyCode")
                                        '  dataRow("Number") = dtnewtbl.Rows(i)("Number")
                                        dataRow("RefNo_3") = dataRow1(0)("RefNo_3")
                                        dataRow("RefNo_4") = dataRow1(0)("RefNo_4")
                                        dataRow("BankAccNo") = dataRow1(0)("BankAccNo")
                                        dataRow("NOClP") = dataRow1(0)("NOClP")
                                        dataRow("IssuedForCLP") = dataRow1(0)("IssuedForCLP")
                                        dataRow("PaymentTermName") = dataRow1(0)("PaymentTermName")
                                        dataRow("TenderType") = dtmutpleTender.Rows(i)("Tender")
                                        dataRow("BankName") = dataRow1(0)("BankName")
                                        dataRow("CardNo") = dataRow1(0)("CardNo")
                                        dataRow("ChequeNo") = dataRow1(0)("ChequeNo")
                                        dataRow("MICRNO") = dataRow1(0)("MICRNO")
                                        dataRow("ChequeDate") = dataRow1(0)("ChequeDate")
                                        dataRow("NEFTReferenceNo") = dataRow1(0)("NEFTReferenceNo")
                                        dataRow("RTGSReferenceNo") = dataRow1(0)("RTGSReferenceNo")
                                        dataRow("CreditVoucherNo") = dataRow1(0)("CreditVoucherNo")
                                        dataRow("Remarks") = dataRow1(0)("Remarks")
                                        dataRow("SoInvNumber") = dtmutpleTender.Rows(i)("saleInvNumber")
                                        dsNewTable.Tables(0).Rows.Add(dataRow)
                                    Next
                                    ' Next
                                End If
                                ''''''''''''''''''''''''''''''''''''''''''''''''

                                Dim docno As String = objCM.getDocumentNo("SalesInvoice", clsAdmin.SiteCode, "FO_DOC")
                                ' BillNo = GenDocNo("SI" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                                ''GST changes by ketan add sitecode 3 digit in billno                     
                                BillNo = GenDocNo("S" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 3, 3) & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)

                                'End If
                                Dim trans As Object

                                If dsNewTable.Tables(0).Rows.Count > 0 Then
                                    trans = PrepareData(dsNewTable)
                                Else
                                    trans = PrepareData(payment.ReciptTotalAmount)
                                End If
                                If payment.DtCheckDetail IsNot Nothing Then

                                    Dim Counter1 As Integer = 0
                                    For Each DR In payment.DtCheckDetail.Tables(0).Rows
                                        payment.DtCheckDetail.Tables(0).Rows(Counter1)("SiteCode") = trans.Item(0).SiteCode
                                        payment.DtCheckDetail.Tables(0).Rows(Counter1)("FinYear") = trans.Item(0).FinYear
                                        payment.DtCheckDetail.Tables(0).Rows(Counter1)("BillNo") = trans.Item(0).RefBillInvNumber
                                        payment.DtCheckDetail.Tables(0).Rows(Counter1)("DocumentNo") = BillNumber
                                        payment.DtCheckDetail.Tables(0).Rows(Counter1)("DocumentType") = trans.Item(0).DocType
                                        payment.DtCheckDetail.Tables(0).Rows(Counter1)("BillDate") = clsAdmin.DayOpenDate
                                        payment.DtCheckDetail.Tables(0).Rows(Counter1)("CREATEDAT") = trans.Item(0).CreatedAt
                                        payment.DtCheckDetail.Tables(0).Rows(Counter1)("CreatedBy") = trans.Item(0).CreatedBy
                                        payment.DtCheckDetail.Tables(0).Rows(Counter1)("CreatedOn") = trans.Item(0).CreatedOn
                                        payment.DtCheckDetail.Tables(0).Rows(Counter1)("UpdatedAt") = trans.Item(0).UpdatedAt
                                        payment.DtCheckDetail.Tables(0).Rows(Counter1)("UpdatedBy") = trans.Item(0).UpdatedBy
                                        payment.DtCheckDetail.Tables(0).Rows(Counter1)("UpdatedOn") = trans.Item(0).UpdatedOn
                                        Counter1 = Counter1 + 1
                                    Next

                                    '' added by nikhil

                                    ' Dim dtview As New DataView(dsTender)
                                    Dim dtmultender As New DataTable
                                    Dim articleCode As String = ""

                                    If objCreditSales.UpdateCredit(trans, clsAdmin.DayOpenDate) And objCreditSales.UpdateCreditInvoice(payment.DtCheckDetail) Then '' dsRecieptType() , trans

                                        'If objCreditSales.UpdateCredit(trans, clsAdmin.DayOpenDate) Then

                                        Dim BillDate = clsAdmin.DayOpenDate
                                        Dim dsVoucher As DataSet
                                        dsVoucher = objCM.GetVoucherFloatData(BillNumber, BillDate)
                                        '-----------Saving Data of Float Return to Petty cash Entry
                                        If FloatAmtReturned <> 0 Then
                                            objCreditSales.SaveFloatAmtReturnedData(FloatAmtReturned, DeliveryPerson, BillNumber, InvoicBillNumber, BillDate, clsAdmin.SiteCode, clsAdmin.UserCode, clsAdmin.Financialyear, dsVoucher)
                                        End If
                                        If clsDefaultConfiguration.KOTAndBillGeneration = True Then
                                            PrintKOT(True)
                                            CmdPrint_Click(sender, EventArgs.Empty)
                                        Else
                                            If clsDefaultConfiguration.IsNewCreditSale Then
                                                'Dim totAmt As String = dsNewTable.Tables(0).Compute("Sum(Amount)", "").ToString
                                                Dim totAmt As String = ""
                                                Dim PaidtotAmt As String = dsNewTable.Tables(0).Compute("Sum(Amount)", "").ToString
                                                totAmt = dsNewTable.Tables(0).Compute("Sum(Amount)", "").ToString
                                                If Not dtmutpleTender Is Nothing AndAlso dtmutpleTender.Rows.Count > 0 Then
                                                    totAmt = dtmutpleTender.Rows(0)("amountTendered").ToString
                                                End If
                                                Dim objPrint As New frmNReprint
                                                Dim dtHeader As New DataTable
                                                dtHeader = ObjclsCommon.GetCreditSaleSettledDataToPrint(clsAdmin.SiteCode)
                                                Dim AmtInRs As String = ""
                                                Dim TenderTypeCode As String = ""
                                                Dim invNumber As String = ""
                                                Dim dtview As New DataView(dsNewTable.Tables(0))
                                                dtview.Sort = "RecieptType"
                                                Dim dtTenderOnly = dtview.ToTable(True, "RecieptType", "SoInvNumber")
                                                'Dim names = From row In dtTenderOnly.AsEnumerable()
                                                '   Select row.Field(Of String)("RecieptType") Distinct


                                                'Dim Addressarray1 As String() = (From row In dtTenderOnly
                                                '                Select Convert.ToString(row("RecieptType"))).Distinct().ToArray()

                                                ' dtTenderOnly = dsNewTable.Tables(0).AsEnumerable().GroupBy(Function(r) New With {Key .cole1 = r("RecieptType")}).[Select](Function(g) g.OrderBy(Function(r) r("SoInvNumber.")).First()).Distinct.CopyToDataTable()
                                                For i = 0 To dtTenderOnly.Rows.Count - 1
                                                    If dtInvoice.Rows.Count > 1 Then
                                                        If TenderTypeCode <> "" Then
                                                            TenderTypeCode = TenderTypeCode + " / " + dtTenderOnly(i)("RecieptType").ToString
                                                            invNumber = invNumber + "," + dtTenderOnly(i)("SoInvNumber").ToString
                                                        Else
                                                            TenderTypeCode = dtTenderOnly(i)("RecieptType").ToString
                                                            invNumber = dsNewTable.Tables(0)(i)("SoInvNumber").ToString
                                                        End If
                                                    ElseIf TenderTypeCode <> "" Then
                                                        TenderTypeCode = TenderTypeCode + " / " + dtTenderOnly(i)("RecieptType").ToString
                                                        'invNumber = invNumber + "," + dtTenderOnly(i)("SoInvNumber").ToString
                                                    Else
                                                        TenderTypeCode = dtTenderOnly(i)("RecieptType").ToString
                                                        invNumber = dsNewTable.Tables(0)(i)("SoInvNumber").ToString
                                                    End If

                                                    'End If

                                                Next
                                                If PaidtotAmt <> "" Then
                                                    AmtInRs = AmtInWord(PaidtotAmt, Nothing, Nothing)
                                                Else
                                                    totAmt = grdCreditSales.Rows(grdCreditSales.Row)("BalanceAmount").ToString
                                                    AmtInRs = AmtInWord(PaidtotAmt, Nothing, Nothing)
                                                End If
                                                If invNumber = "" Then
                                                    invNumber = trans.Item(0).RefBillInvNumber().ToString
                                                End If


                                                ' AmtInRs = objPrint.NumberToText(txtCollectAmount.Text)
                                                If Not dtHeader Is Nothing AndAlso dtHeader.Rows.Count > 0 Then
                                                    objPrint.GenerateCreditSaleSettledReportPrint(clsAdmin.SiteCode, grdCreditSales.Rows(grdCreditSales.Row)("BalanceAmount").ToString, PaidtotAmt, multTender, AllRemark, AmtInRs, clsAdmin.TerminalID, BillNumber, clsAdmin.UserName, dtHeader, BillNumber)
                                                End If
                                            Else
                                                Dim Printer As New clsPrintCreditSettlementNote()
                                                Printer.PrintNote(trans, dtPrinterInfo)
                                            End If
                                        End If
                                        MessageBox.Show(getValueByKey("Crs008"))
                                        ' If payment.DialogResult = Windows.Forms.DialogResult.OK Then
                                        BindCreditSales()
                                        'End If

                                    Else
                                        MessageBox.Show(getValueByKey("Crs009"))
                                    End If
                                Else
                                    If objCreditSales.UpdateCredit(trans, clsAdmin.DayOpenDate) Then

                                        Dim BillDate = clsAdmin.DayOpenDate
                                        Dim dsVoucher As DataSet
                                        dsVoucher = objCM.GetVoucherFloatData(BillNumber, BillDate)
                                        '-----------Saving Data of Float Return to Petty cash Entry
                                        If FloatAmtReturned <> 0 Then
                                            objCreditSales.SaveFloatAmtReturnedData(FloatAmtReturned, DeliveryPerson, BillNumber, InvoicBillNumber, BillDate, clsAdmin.SiteCode, clsAdmin.UserCode, clsAdmin.Financialyear, dsVoucher)
                                        End If
                                        If clsDefaultConfiguration.KOTAndBillGeneration = True Then
                                            PrintKOT(True)
                                            CmdPrint_Click(sender, EventArgs.Empty)
                                        Else
                                            If clsDefaultConfiguration.IsNewCreditSale Then
                                                If Not dsNewTable Is Nothing AndAlso dsNewTable.Tables(0).Rows.Count > 0 Then
                                                    'Dim totAmt As String = dsNewTable.Tables(0).Compute("Sum(Amount)", "").ToString
                                                    Dim totAmt As String = ""
                                                    Dim PaidtotAmt As String = dsNewTable.Tables(0).Compute("Sum(Amount)", "").ToString
                                                    totAmt = dsNewTable.Tables(0).Compute("Sum(Amount)", "").ToString
                                                    If Not dtmutpleTender Is Nothing AndAlso dtmutpleTender.Rows.Count > 0 Then
                                                        totAmt = dtmutpleTender.Rows(0)("amountTendered").ToString
                                                    End If
                                                    Dim objPrint As New frmNReprint
                                                    Dim dtHeader As New DataTable
                                                    dtHeader = ObjclsCommon.GetCreditSaleSettledDataToPrint(clsAdmin.SiteCode)
                                                    Dim AmtInRs As String = ""
                                                    Dim TenderTypeCode As String = ""
                                                    Dim invNumber As String = ""

                                                    Dim dtview As New DataView(dsNewTable.Tables(0))
                                                    Dim dtTenderOnly = dtview.ToTable(True, "RecieptType", "SoInvNumber")
                                                    dtTenderOnly.DefaultView.ToTable(True, "RecieptType", "SoInvNumber")
                                                    ' dtTenderOnly = dsNewTable.Tables(0).AsEnumerable().GroupBy(Function(r) New With {Key .cole1 = r("RecieptType")}).[Select](Function(g) g.OrderBy(Function(r) r("SoInvNumber.")).First()).Distinct.CopyToDataTable()
                                                    'For i = 0 To dtTenderOnly.Rows.Count - 1
                                                    '    If dtInvoice.Rows.Count > 1 Then
                                                    '        If TenderTypeCode <> "" Then
                                                    '            TenderTypeCode = TenderTypeCode + " / " + dtTenderOnly(i)("RecieptType").ToString
                                                    '            invNumber = invNumber + "," + dtTenderOnly(i)("SoInvNumber").ToString
                                                    '        Else
                                                    '            TenderTypeCode = dtTenderOnly(i)("RecieptType").ToString
                                                    '            invNumber = dsNewTable.Tables(0)(i)("SoInvNumber").ToString
                                                    '        End If
                                                    '    ElseIf TenderTypeCode <> "" Then
                                                    '        TenderTypeCode = TenderTypeCode + " / " + dtTenderOnly(i)("RecieptType").ToString
                                                    '        'invNumber = invNumber + "," + dtTenderOnly(i)("SoInvNumber").ToString
                                                    '    Else
                                                    '        TenderTypeCode = dtTenderOnly(i)("RecieptType").ToString
                                                    '        invNumber = dsNewTable.Tables(0)(i)("SoInvNumber").ToString
                                                    '    End If
                                                    'Next
                                                    AmtInRs = AmtInWord(PaidtotAmt, Nothing, Nothing)
                                                    ' AmtInRs = objPrint.NumberToText(txtCollectAmount.Text)
                                                    If Not dtHeader Is Nothing AndAlso dtHeader.Rows.Count > 0 Then
                                                        objPrint.GenerateCreditSaleSettledReportPrint(clsAdmin.SiteCode, grdCreditSales.Rows(grdCreditSales.Row)("BalanceAmount").ToString, PaidtotAmt, multTender, AllRemark, AmtInRs, clsAdmin.TerminalID, BillNumber, clsAdmin.UserName, dtHeader, BillNumber)
                                                    End If
                                                Else
                                                    Dim totAmt As String = payment.ReciptTotalAmount.Tables(0).Compute("Sum(Amount)", "").ToString
                                                    Dim objPrint As New frmNReprint
                                                    Dim dtHeader As New DataTable
                                                    dtHeader = ObjclsCommon.GetCreditSaleSettledDataToPrint(clsAdmin.SiteCode)
                                                    Dim AmtInRs As String = ""
                                                    Dim TenderTypeCode As String = ""
                                                    Dim invNumber As String = ""

                                                    Dim dtview As New DataView(dsNewTable.Tables(0))
                                                    Dim dtTenderOnly = dtview.ToTable(True, "RecieptType", "SoInvNumber")

                                                    dtTenderOnly = (From row In dtTenderOnly
                                                            Select Convert.ToString(row("RecieptType"))).Distinct()
                                                    ' dtTenderOnly = dsNewTable.Tables(0).AsEnumerable().GroupBy(Function(r) New With {Key .cole1 = r("RecieptType")}).[Select](Function(g) g.OrderBy(Function(r) r("SoInvNumber.")).First()).Distinct.CopyToDataTable()
                                                    For i = 0 To dtTenderOnly.Rows.Count - 1
                                                        If TenderTypeCode <> "" Then
                                                            TenderTypeCode = TenderTypeCode + " / " + dtTenderOnly(i)("RecieptType").ToString
                                                            invNumber = invNumber + " / " + dtTenderOnly(i)("SoInvNumber").ToString
                                                        Else
                                                            TenderTypeCode = dtTenderOnly(i)("RecieptType").ToString
                                                            invNumber = dtTenderOnly(i)("SoInvNumber").ToString
                                                        End If
                                                    Next
                                                    AmtInRs = AmtInWord(totAmt, Nothing, Nothing)
                                                    ' AmtInRs = objPrint.NumberToText(txtCollectAmount.Text)
                                                    If Not dtHeader Is Nothing AndAlso dtHeader.Rows.Count > 0 Then
                                                        objPrint.GenerateCreditSaleSettledReportPrint(clsAdmin.SiteCode, grdCreditSales.Rows(grdCreditSales.Row)("BalanceAmount").ToString, totAmt, multTender, AllRemark, AmtInRs, clsAdmin.TerminalID, BillNumber, clsAdmin.UserName, dtHeader, BillNumber)
                                                    End If
                                                End If


                                            Else
                                                Dim Printer As New clsPrintCreditSettlementNote()
                                                Printer.PrintNote(trans, dtPrinterInfo)
                                            End If


                                        End If
                                        MessageBox.Show(getValueByKey("Crs008"))
                                        '' added by nikhil for Multiple tender payment settlement
                                        'Dim dtCollect = ObjclsCommon.GetMultipleTenderPaymentsToPrint(BillNumber, clsAdmin.SiteCode)

                                        ' Dim dtAmt = dtCollect.Compute("Sum(AmountTendered)", "").ToString
                                        'Dim AmtInRs As String = ""
                                        'AmtInRs = NumberToText(txtCollectAmount.Text)
                                        'If Not dtCollect Is Nothing AndAlso dtCollect.Rows.Count > 0 Then
                                        '    ' objPrint.GenerateCreditSaleSettledReportPrint(clsAdmin.SiteCode, dtAmt, txtCollectAmount.Text, TenderTypeCode, txtRemark.Text, AmtInRs, clsAdmin.TerminalID, InvoiceNumber, clsAdmin.UserName, dtHeader)
                                        'End If
                                        ' If payment.DialogResult = Windows.Forms.DialogResult.OK Then
                                        BindCreditSales()
                                        'End If

                                    Else
                                        MessageBox.Show(getValueByKey("Crs009"))
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

            Else
                MessageBox.Show(getValueByKey("Crs010"))

            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub


    Dim ObjclsCommon As New clsCommon
    Private Function PrepareData(ByVal ds As DataSet) As List(Of SalesInvoice)
        Try


            'Dim dtInvoice As New DataTable
            'Dim SoNumber As String = ""
            'SoNumber = grdCreditSales.Rows(grdCreditSales.RowSel)("DocumentNo").ToString()
            'dtInvoice = objCreditSales.GetBillInvoiceDtlsAgainstSO(SoNumber)

            Dim ServerDate = ObjclsCommon.GetCurrentDate()
            Dim ReceiptList As New List(Of SalesInvoice)
            '  If Not dtInvoice Is Nothing AndAlso dtInvoice.Rows.Count > 0 Then
            '    For j = 0 To dtInvoice.Rows.Count - 1

            For Each dr As DataRow In ds.Tables(0).Rows


                Dim salesinvoice As New SalesInvoice()
                salesinvoice.BillNO = BillNo
                salesinvoice.AmountTendered = dr("Amount")
                salesinvoice.BankNO = dr("BankAccNO").ToString()
                salesinvoice.CardNO = dr("Number").ToString()
                salesinvoice.TerminalID = clsAdmin.TerminalID
                salesinvoice.AmountInCurrency = If(dr("AmountInCurrency").ToString() = "", 0.0, CDbl(dr("AmountInCurrency").ToString()))
                salesinvoice.CurrencyCode = dr("CurrencyCode").ToString()
                salesinvoice.DocType = If(grdCreditSales.Rows(grdCreditSales.RowSel)("DocType").ToString().ToLower() = "Cash memo".ToLower(), "CM", "SO")
                salesinvoice.ExchangeRate = If(dr("ExchangeRate").ToString() = "", 0.0, CDec(dr("ExchangeRate").ToString()))
                salesinvoice.FinYear = clsAdmin.Financialyear
                salesinvoice.InvoiceNumber = grdCreditSales.Rows(grdCreditSales.RowSel)("DocumentNo").ToString()   ''  commented by nik billNo
                salesinvoice.IsNew = True
                salesinvoice.LineNo = dr("SrNO")
                salesinvoice.RecTime = clsAdmin.DayOpenDate
                salesinvoice.SiteCode = clsAdmin.SiteCode
                salesinvoice.Status = True
                salesinvoice.TenderHeadCode = dr("RECIEPTTYPE")
                salesinvoice.TenderTypeCode = dr("RECIEPTTYPECODE")
                salesinvoice.CreatedAt = clsAdmin.SiteCode
                salesinvoice.CreatedBy = clsAdmin.UserCode
                salesinvoice.CreatedOn = ServerDate

                salesinvoice.UpdatedAt = clsAdmin.SiteCode
                salesinvoice.UpdatedBy = clsAdmin.UserCode
                salesinvoice.UpdatedOn = ServerDate
                If ds.Tables(0).Columns.Contains("SoInvNumber") = True Then
                    salesinvoice.RefBillInvNumber = dr("SoInvNumber")
                Else
                    salesinvoice.RefBillInvNumber = grdCreditSales.Rows(grdCreditSales.RowSel)("InvoiceNo").ToString() 'new added
                End If
                ''
                salesinvoice.Remark = dr("Remarks").ToString() 'vipin
                ReceiptList.Add(salesinvoice)
            Next


            '  Next j

            ' End If
            Return ReceiptList
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function


    Private Sub cmdCash_Click(sender As System.Object, e As System.EventArgs) Handles cmdCash.Click
        Try
            PaymentCreditSales(TenderMode:="Cash")
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdCard_Click(sender As System.Object, e As System.EventArgs) Handles cmdCard.Click
        Try
            PaymentCreditSales(TenderMode:="CreditCard")
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdCheque_Click(sender As System.Object, e As System.EventArgs) Handles cmdCheque.Click
        Try
            PaymentCreditSales(TenderMode:="Cheque")
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Dim DtSoBulkComboHdr As New DataTable
    Dim DtSoBulkComboDtl As New DataTable
    Private Sub CmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        Try
            If clsCommon.checkDiscSpce(clsDefaultConfiguration.DayCloseReportPath.Substring(0, 3)) = False Then
                ShowMessage("Insufficient disk space for saving files", "")
            End If
            Dim PrintFormatNo As Integer
            PrintFormatNo = clsDefaultConfiguration.PrintFormatNo
            '' Commit By ketan 
            'If clsDefaultConfiguration.ClientForMail = "PC" Then
            '    PrintFormatNo = 3
            'End If
            If PrintFormatNo = "3" OrElse PrintFormatNo = "4" OrElse PrintFormatNo = "5" Then
                If clsDefaultConfiguration.IsArticleWiseKOT.Contains(clsAdmin.TerminalID) Then
                    IsArticleWiseKot = True
                Else
                    IsArticleWiseKot = False
                End If
                If clsDefaultConfiguration.IsCounterCopy.Contains(clsAdmin.TerminalID) Then
                    IsCounterCopy = True
                Else
                    IsCounterCopy = False
                End If
                If clsDefaultConfiguration.IsFinalReceipt.Contains(clsAdmin.TerminalID) Then
                    IsFinalReceipt = True
                Else
                    IsFinalReceipt = False
                End If
            End If
            Dim ReprintReason = String.Empty
            If grdCreditSales.Rows.Count > 1 Then
                'Step 1 ---- Get loop All SELECTED ROW
                For Each row In grdCreditSales.Rows.Selected
                    If (row("DocType").ToString().ToLower() = "sales order".ToLower()) Then
                        Dim SalesPersonName As String = String.Empty
                        dtSaleItems = New DataTable
                        dtSaleItems = objPrint.GetSaleItems(clsAdmin.SiteCode, clsAdmin.Financialyear, row("DocumentNo"), "SalesOrder", row("InvoiceNo"))   '' commneted by nik  BillNo
                        If clsDefaultConfiguration.IsNewSalesOrder = True Then
                            Dim DounmentNo As String = dtSaleItems.Rows(0)("SaleOrderNumber")
                            SOprint(DounmentNo)
                        Else
                            'dtSaleItems.Columns("rowindex").Caption = "BillLineNo"
                            If Not IsDBNull(dtSaleItems.Rows(0)("SalesPersonFullName")) Then SalesPersonName = dtSaleItems.Rows(0)("SalesPersonFullName")
                            dtPayment = New DataTable
                            dtPayment = objPrint.GetPaymentDetails(clsAdmin.SiteCode, clsAdmin.Financialyear, row("DocumentNo"), row("InvoiceNo"))  '' '' commneted by nik  BillNo
                            'SOInvc
                            Dim vSalesOrderCreationDate As DateTime = DateTime.Now
                            vSalesOrderCreationDate = dtSaleItems.Rows(0)("CreatedOn")
                            Dim vSalesOrderDeliveryDate As DateTime = DateTime.Now
                            vSalesOrderDeliveryDate = dtSaleItems.Rows(0)("ActualDeliveryDate")
                            Dim vSalesOrderRemark As String = ""
                            vSalesOrderRemark = (IIf(dtSaleItems.Rows(0)("Remarks") Is DBNull.Value, "", dtSaleItems.Rows(0)("Remarks")))
                            Dim vInvoiceTo As String = ""
                            vInvoiceTo = (IIf(dtSaleItems.Rows(0)("InvoiceTo") Is DBNull.Value, "", dtSaleItems.Rows(0)("InvoiceTo")))
                            Dim objCustm As New clsCLPCustomer()
                            Dim vCustRef As String = ""
                            vCustRef = (IIf(dtSaleItems.Rows(0)("CustomerOrderRef") Is DBNull.Value, "", dtSaleItems.Rows(0)("CustomerOrderRef")))
                            dtCustInfo = objCustm.GetCustomerInformation(dtSaleItems.Rows(0)("CustomerType"), clsAdmin.SiteCode, clsAdmin.CLPProgram, dtSaleItems.Rows(0)("CustomerNo"))

                            Dim statusValue = IIf(dtSaleItems.Compute("Sum(Quantity)", "") - (dtSaleItems.Compute("Sum(PickUpQty)", "") + dtSaleItems.Compute("Sum(DeliveredQty)", "")) > 0, "Open", "Close")
                            Dim Dstemp = objSO.GetSOBulkComboTableStruct(clsAdmin.SiteCode, IIf(row("BillNo") = String.Empty, 0, row("BillNo")))
                            DtSoBulkComboHdr = Dstemp.Tables("SoBulkComboHdr")
                            DtSoBulkComboDtl = Dstemp.Tables("SoBulkComboDtl")

                            Dim SalesOrderNumber = dtSaleItems.Rows(0)("SaleOrderNumber")
                            Dim dsOtherCharges As New DataSet
                            dsOtherCharges.Clear()
                            Dim dt = objCharge.GetDtOtherCharge(clsAdmin.SiteCode, IIf(SalesOrderNumber = String.Empty, 0, SalesOrderNumber))
                            dsOtherCharges.Tables.Add(dt.Copy())
                            dsOtherCharges.Tables(0).TableName = "NewOtherCharges"
                            Dim objPrintDll As New SpectrumPrint.clsPrintSalesOrder(clsDefaultConfiguration.SOPrintPreviewAllowed, clsDefaultConfiguration.RoundOffRequired,
                                                                                    SpectrumPrint.clsPrintSalesOrder.PrintSOTransactionSet.Status, clsAdmin.SiteCode, clsAdmin.CurrencyCode,
                                                                                    clsAdmin.UserName, row("BillNo"), dtCustInfo, dtSaleItems, dtPayment, vCustRef, row("InvoiceNo"),
                                                                                    String.Empty, Nothing, Nothing, dtPrinterInfo, statusValue, dsOtherCharges, strReturnReason:=vSalesOrderRemark,
                                                                                    ShowFullName:=clsDefaultConfiguration.PrintItemFullName, SalesOrderCreationDate:=vSalesOrderCreationDate,
                                                                                    dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderDeliveryDate, strSalesPerson:=SalesPersonName, strInvoiceTo:=vInvoiceTo)
                            'objPrint.UpdateReprintStatus(clsAdmin.SiteCode, clsAdmin.Financialyear, grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo"), PrintTransType, ReprintReason )
                        End If
                    ElseIf (row("DocType").ToString().ToLower() = "Cash memo".ToLower()) Then
                        Dim clsDefault As New clsDefaultConfiguration("CMS")
                        clsDefault.GetDefaultSettings()

                        Dim objCMPrint As New clsCashMemoPrint(row("InvoiceNo"), False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving) '0000413
                        objCMPrint.DisplayArticleFullName = clsDefaultConfiguration.PrintItemFullName

                        objCMPrint.AllowDecimalQty = clsDefaultConfiguration.AllowDecimalQty
                        objCMPrint.WeightScaleEnabled = clsDefaultConfiguration.WeightScaleEnabled
                        objCMPrint.KOTBillPrintingRequired = False
                        objCMPrint.TokenNoRequiredInKOT = clsDefaultConfiguration.TokenNoRequiredInKOT
                        objCMPrint.CashMemoResetonDayClose = clsDefaultConfiguration.CashMemoResetonDayClose
                        objCMPrint.AllowBillingOnlyAfterSelectionOfSalesType = clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType
                        objCMPrint.PrintFormatNo = PrintFormatNo
                        If clsDefaultConfiguration.KOTAndBillGeneration = True Then
                            objCMPrint.PrintCouponNumberForKOT = True
                        Else
                            objCMPrint.PrintCouponNumberForKOT = False
                        End If
                        Dim ErrorMsg As String = ""
                        '--- Changes by mahesh before reprint updated reprint count (same as Cash Memo)
                        'objPrint.UpdateReprintStatus(clsAdmin.SiteCode, clsAdmin.Financialyear, row("InvoiceNo"), PrintTransType, ReprintReason)

                        If (clsDefaultConfiguration.TemplatePrintingAllowed) Then
                            objCMPrint.PrintTemplateCashMemoBillDetails(row("InvoiceNo"), clsAdmin.SiteCode, clsAdmin.CurrencyCode, String.Empty, Nothing, True, ReprintReason)
                        Else
                            If clsDefaultConfiguration.KOTAndBillGeneration = True Then
                                Dim _printKOT As Boolean = True
                                'Dim strText As String = (sender as TextBox).Text
                                If sender Is Nothing Then
                                    _printKOT = True
                                ElseIf UCase(sender.name) = UCase("cmdPrint") = True Then
                                    _printKOT = False
                                End If
                                ' objCMPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "", "", "", ErrorMsg, "", "0", "0", False, clsDefaultConfiguration.IsSavoy, False, False, False, _printKOT) '0000413
                                objCMPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "", "", "", ErrorMsg, "", " 0", "0", False, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=IsFinalReceipt, KOTAndBillGeneration:=_printKOT, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsKOTBillGenerationFromPRintButton:=clsDefaultConfiguration.KOTAndBillGeneration) '0000413
                            Else
                                'objCMPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "", "", "", ErrorMsg) '0000413
                                objCMPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "", "", "", ErrorMsg, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsCounterCopyKot:=True) '0000413
                            End If
                        End If
                        If ErrorMsg <> String.Empty Then
                            ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
                        End If
                    End If
                Next row
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub EnableDiableTenderIcons()
        '--- Added by Mahesh for disable credit sale if tender not assign
        Dim DtTender As DataTable = GetTenderInfo(clsAdmin.SiteCode)

        '----Cash
        Dim dt() = DtTender.Select("TenderType='" & "Cash" & "'")
        If dt IsNot Nothing AndAlso dt.Count > 0 Then
            IsTenderCash = True
        Else
            cmdCash.Enabled = False
        End If
        '----Cheque
        Dim dq() = DtTender.Select("TenderType='" & "Cheque" & "'")
        If dq IsNot Nothing AndAlso dq.Count > 0 Then
            IsTenderCheque = True
        Else
            cmdCheque.Enabled = False
        End If
        '----CreditCard
        Dim dw() = DtTender.Select("TenderType='" & "CreditCard" & "'")
        If dw IsNot Nothing AndAlso dw.Count > 0 Then
            IsTenderCreditCard = True
        Else
            cmdCard.Enabled = False
        End If
    End Sub
    ''' <summary>
    ''' Dispatch Click :Getting Data of Home Delivery and Displaying HomeDelivery Form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdDispatch_Click(sender As Object, e As EventArgs) Handles cmdDispatch.Click
        Try
            Dim obj As New frmNHomeDelivery
            obj.IsDispatch = True
            If grdCreditSales.Row > 0 Then
                Dim Billno As String = grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo")   '' commented by nik BillNo

                Dim dtHD As New DataTable
                dtHD = objCreditSales.GetHomeDeliveryData(Billno, clsAdmin.SiteCode)
                If Not dtHD Is Nothing And dtHD.Rows.Count > 0 Then

                    If (IsDBNull(dtHD.Rows(0)("HDDeliveryDate"))) Then
                        obj.DeliveryDate = clsAdmin.CurrentDate
                    Else
                        obj.DeliveryDate = dtHD.Rows(0)("HDDeliveryDate")
                    End If
                    obj.IsupdateDeliveryPersonAllowed = True
                    obj.HdName = dtHD.Rows(0)("HDName").ToString()
                    Dim Address() As String = dtHD.Rows(0)("HDAddress").ToString().Split(";")
                    Select Case Address.Count
                        Case 1
                            obj.Address1 = Address(0).ToString()
                        Case 2
                            obj.Address1 = Address(0).ToString()
                            obj.Address2 = Address(1).ToString()
                        Case 3
                            obj.Address1 = Address(0).ToString()
                            obj.Address2 = Address(1).ToString()
                            obj.Address3 = Address(2).ToString()
                        Case 4
                            obj.Address1 = Address(0).ToString()
                            obj.Address2 = Address(1).ToString()
                            obj.Address3 = Address(2).ToString()
                            obj.Address4 = Address(3).ToString()
                        Case Else
                    End Select

                    obj.Email = dtHD.Rows(0)("HDEmail").ToString()
                    obj.TelNo = dtHD.Rows(0)("HDTelNo").ToString()
                    obj.Remark = dtHD.Rows(0)("HDRemark").ToString()
                    Dim dsFloatAmt As DataSet
                    Dim BillDate As DateTime = dtHD.Rows(0)("BillDate")
                    Dim objcm As New clsCashMemo
                    dsFloatAmt = objcm.GetVoucherFloatData(Billno, BillDate)
                    If Not dsFloatAmt.Tables("VoucherHdr").Rows.Count = 0 Then
                        obj.FloatAmt = IIf(dsFloatAmt.Tables("VoucherHdr").Rows(0)("TotalAmt") Is DBNull.Value, 0, dsFloatAmt.Tables("VoucherHdr").Rows(0)("TotalAmt"))
                    End If
                    obj.DeliveryPersonID = dtHD.Rows(0)("DeliveryPersonID").ToString()
                    Dim DeliveryPartner = dtHD.Rows(0)("DeliveryPartnerId").ToString()
                    obj.delieverypartnerid = DeliveryPartner
                    If (Not String.IsNullOrEmpty(dtHD.Rows(0)("ClpNo").ToString())) Then
                        obj.CustomerNo = dtHD.Rows(0)("ClpNo").ToString()
                    ElseIf (Not String.IsNullOrEmpty(dtHD.Rows(0)("CustomerNo").ToString())) Then
                        obj.CustomerNo = dtHD.Rows(0)("CustomerNo").ToString()
                    End If
                    obj.MobileNo = objCreditSales.GetCustData(obj.CustomerNo, clsAdmin.SiteCode)
                    obj.btnClear.Visible = True
                    obj.cmdOk.Text = "Dispatch"
                    Dim dialogResult = obj.ShowDialog()
                    If (dialogResult = Windows.Forms.DialogResult.Cancel) Then
                        Exit Sub

                    ElseIf dialogResult = Windows.Forms.DialogResult.OK Then
                        FloatAmt = obj.FloatAmt
                        Dim DeliveryPerson As String = obj.DeliveryPersonID
                        DeliveryPartner = obj.delieverypartnerid
                        If FloatAmt <> 0 Then
                            objCreditSales.UpdateFloatAmtDispatchData(FloatAmt, DeliveryPerson, Billno, BillDate, clsAdmin.SiteCode, dsFloatAmt)
                        End If
                        If DeliveryPartner <> String.Empty Then
                            objCreditSales.UpdateDeliveryPartnerData(DeliveryPartner, Billno, BillDate, clsAdmin.SiteCode)
                        End If
                        objCreditSales.UpdateDispatchTimeData(DeliveryPerson, Billno, clsAdmin.SiteCode)
                        grdCreditSales.Rows(grdCreditSales.Row)("DeliveryPerson") = DeliveryPerson
                        grdCreditSales.Rows(grdCreditSales.Row)("SalesPerson") = DeliveryPerson
                        grdCreditSales.Rows(grdCreditSales.Row)("DeliveryPartner") = DeliveryPartner
                        grdCreditSales.Rows(grdCreditSales.Row)("TotalAmt") = FloatAmt
                        grdCreditSales.Rows(grdCreditSales.Row)("Dispatch") = True
                        grdCreditSales.Rows(grdCreditSales.Row)("DispatchTime") = DateTime.Now
                    End If
                    obj.IsDispatch = False
                End If
            End If
        Catch ex As Exception
            ShowMessage(False, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub grdCreditSales_AfterEdit(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdCreditSales.AfterEdit
        If (grdCreditSales.Cols(e.Col).Name.ToUpper() = "DISPATCH") Then
            If Not String.IsNullOrEmpty(grdCreditSales.Rows(grdCreditSales.Row)("DispatchTime").ToString) Then
                grdCreditSales.Rows(grdCreditSales.Row)("Dispatch") = True
                grdCreditSales.Rows(grdCreditSales.Row).AllowEditing = False
            End If

        ElseIf (grdCreditSales.Cols(e.Col).Name.ToUpper() = "FLOATAMTRETURNED") Then
            Dim currentqty As Double = IIf(grdCreditSales.Item(grdCreditSales.Row, "FLOATAMTRETURNED") Is DBNull.Value, 0, grdCreditSales.Item(grdCreditSales.Row, "FLOATAMTRETURNED"))
            If currentqty < 0 Then
                ShowMessage(getValueByKey("ACP039"), "ACP039 - " & getValueByKey("CLAE04"))
                'Float Amount Returned cannot be less than 0
                grdCreditSales.Item(grdCreditSales.Row, "FLOATAMTRETURNED") = 0
                Exit Sub
            End If
            Dim FloatReturned As Double = IIf(grdCreditSales.Item(grdCreditSales.Row, "FLOATAMTRETURNED") Is DBNull.Value, 0, grdCreditSales.Item(grdCreditSales.Row, "FLOATAMTRETURNED"))
            If FloatReturned = 0 Then
                grdCreditSales.Item(grdCreditSales.Row, "FLOATAMTRETURNED") = 0
            End If
            If FloatReturned > 9999 Then
                ShowMessage(getValueByKey("ACP040"), "ACP040 - " & getValueByKey("CLAE04"))
                ' Float Amount Returned cannot be greater then 9999
                grdCreditSales.Item(grdCreditSales.Row, "FLOATAMTRETURNED") = 0
                e.Cancel = True
            End If
            grdCreditSales.Rows(grdCreditSales.Row).AllowEditing = True
        End If
    End Sub

    ''' <summary>
    ''' New Button for Write off Screen,Fetching data of grid into Properties
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdWriteOff_Click(sender As Object, e As EventArgs) Handles cmdWriteOff.Click
        Try
            Dim objPaymentCreditSales As New frmPaymentCreditSales()
            If grdCreditSales.Row > 0 Then 'grdCreditSales.Rows(grdCreditSales.Row)

                Dim objWriteOffCreditSales As New frmWriteOffCreditSales()
                'objWriteOffCreditSales.BillAmount = grdCreditSales.Rows(grdCreditSales.Row)("BillAmount")
                'objWriteOffCreditSales.BalAmount = grdCreditSales.Rows(grdCreditSales.Row)("BalanceAmount")
                'objWriteOffCreditSales.BillNumber = grdCreditSales.Rows(grdCreditSales.Row)("BillNo")
                'objWriteOffCreditSales.InvoiceNumber = grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo")


                '''''''


                objWriteOffCreditSales.BillAmount = grdCreditSales.Rows(grdCreditSales.Row)("TotInvAmt")
                objWriteOffCreditSales.BalAmount = grdCreditSales.Rows(grdCreditSales.Row)("BalanceAmount")
                objWriteOffCreditSales.BillNumber = grdCreditSales.Rows(grdCreditSales.Row)("DocumentNo")
                objWriteOffCreditSales.InvoiceNumber = grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo")






                ''''''''''''

                If (grdCreditSales.Rows(grdCreditSales.RowSel)("DocType").ToString().ToLower() = "Cash memo".ToLower()) Then
                    objWriteOffCreditSales.TranTypeCode = "CM"
                ElseIf (grdCreditSales.Rows(grdCreditSales.RowSel)("DocType").ToString().ToLower() = "sales order".ToLower()) Then
                    objWriteOffCreditSales.TranTypeCode = "SO"
                Else
                    objWriteOffCreditSales.TranTypeCode = "CM"
                End If
                If (objWriteOffCreditSales.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                    frmNCreditSales_Load(Nothing, EventArgs.Empty)
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' theme changer function
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Themechange() As String

        Me.BackColor = Color.FromArgb(76, 76, 76)

        Me.topButtonPanel.BackColor = Color.FromArgb(76, 76, 76)

        'TableLayoutPanel1
        '
        'Me.TableLayoutPanel1.RowStyles(0).SizeType = SizeType.Absolute
        'Me.TableLayoutPanel1.RowStyles(0).Height = 35
        'Me.TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
        'Me.TableLayoutPanel1.RowStyles(1).Height = 550
        '' Me.TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Absolute
        'Me.TableLayoutPanel1.RowStyles(2).Height = 8
        '' Me.TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Absolute
        'Me.TableLayoutPanel1.RowStyles(3).Height = 30

        ' Me.TableLayoutPanel1.SetRow(Me.grdCreditSales, 2)

        Me.TableLayoutPanel1.SetRowSpan(Me.TableLayoutPanel2, 2)
        'lblsearch
        '
        Me.lblsearch.BackColor = Color.Transparent
        Me.lblsearch.BorderStyle = BorderStyle.None
        Me.lblsearch.ForeColor = Color.White
        Me.lblsearch.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblsearch.Text = Me.lblsearch.Text.ToUpper()

        'dgGridReciept
        '

        Me.grdCreditSales.MaximumSize = New Size(1564, 600)
        Me.grdCreditSales.Size = New System.Drawing.Size(1564, 600)
        Me.grdCreditSales.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.grdCreditSales.Styles.Highlight.BackColor = Color.FromArgb(153, 255, 255)
        Me.grdCreditSales.Styles.Highlight.ForeColor = Color.Black
        Me.grdCreditSales.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdCreditSales.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        Me.grdCreditSales.Rows.MinSize = 26
        Me.grdCreditSales.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        Me.grdCreditSales.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdCreditSales.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdCreditSales.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdCreditSales.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdCreditSales.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdCreditSales.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.grdCreditSales.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdCreditSales.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdCreditSales.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdCreditSales.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)


        'Me.TableLayoutPanel1.SetColumnSpan(TableLayoutPanel2, 1)


        'Me.TableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        Me.TableLayoutPanel1.ColumnStyles(0).Width = 100
        'Me.TableLayoutPanel2.Dock = DockStyle.None
        'Me.TableLayoutPanel2.ColumnCount = 11
        'Me.TableLayoutPanel2.ColumnStyles().Add(New ColumnStyle(SizeType.Absolute, 50))
        'Me.TableLayoutPanel2.ColumnStyles().Add(New ColumnStyle(SizeType.Absolute, 50))
        'Me.TableLayoutPanel2.ColumnStyles().Add(New ColumnStyle(SizeType.Absolute, 50))
        'Me.TableLayoutPanel2.ColumnStyles().Add(New ColumnStyle(SizeType.Absolute, 50))
        ''Me.TableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        'Me.TableLayoutPanel2.RowStyles(0).SizeType = SizeType.Absolute

        'Me.TableLayoutPanel2.BackColor = Color.FromArgb(76, 76, 76)
        'Me.TableLayoutPanel2.Location = New System.Drawing.Point(5, 457)
        'Me.TableLayoutPanel2.MaximumSize = New Size(2000, 85)
        'Me.TableLayoutPanel2.Size = New System.Drawing.Size(2000, 85)

        ''Me.TableLayoutPanel2.ColumnStyles(0).Width = 500
        ''Me.TableLayoutPanel2.ColumnStyles(1).Width = 400
        ''TableLayoutPanel1.Controls.Add(pnlButton)
        ''pnlButton.BringToFront()
        ''cmdDispatch

        Me.cmdDispatch.Dock = DockStyle.Fill
        Me.cmdDispatch.Location = New System.Drawing.Point(5, 5)
        Me.cmdDispatch.MaximumSize = New Size(0, 50)
        Me.cmdDispatch.Size = New System.Drawing.Size(100, 80)
        Me.cmdDispatch.BringToFront()
        Me.cmdDispatch.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmdDispatch.Image = Global.Spectrum.My.Resources.Resources.Dispatchnew
        Me.cmdDispatch.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdDispatch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdDispatch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdPrint.TextImageRelation = TextImageRelation.ImageAboveText

        'cmdPrint
        '
        Me.cmdPrint.Dock = DockStyle.Fill
        Me.cmdPrint.Location = New System.Drawing.Point(5, 5)
        Me.cmdPrint.MaximumSize = New Size(0, 50)
        Me.cmdPrint.Size = New System.Drawing.Size(100, 80)
        Me.cmdPrint.BringToFront()
        Me.cmdPrint.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmdPrint.Image = Global.Spectrum.My.Resources.Resources.PrintSO1
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPrint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdPrint.TextImageRelation = TextImageRelation.ImageAboveText

        'cmdPayments
        '
        Me.cmdPayments.Dock = DockStyle.Fill
        Me.cmdPayments.Location = New System.Drawing.Point(5, 5)
        Me.cmdPayments.MaximumSize = New Size(0, 50)
        Me.cmdPayments.Size = New System.Drawing.Size(100, 80)
        Me.cmdPayments.BringToFront()
        Me.cmdPayments.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmdPayments.Image = Global.Spectrum.My.Resources.Resources.payment_Normal
        Me.cmdPayments.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPayments.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPayments.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdPayments.TextImageRelation = TextImageRelation.ImageAboveText


        'cmdCash
        '
        Me.cmdCash.Dock = DockStyle.Fill
        Me.cmdCash.Location = New System.Drawing.Point(5, 5)
        Me.cmdCash.MaximumSize = New Size(0, 50)
        Me.cmdCash.Size = New System.Drawing.Size(100, 80)
        Me.cmdCash.BringToFront()
        Me.cmdCash.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmdCash.Image = Global.Spectrum.My.Resources.Resources.Cash_Normal
        Me.cmdCash.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdCash.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdCash.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdCash.TextImageRelation = TextImageRelation.ImageAboveText

        'cmdCard
        '
        Me.cmdCard.Dock = DockStyle.Fill
        Me.cmdCard.Location = New System.Drawing.Point(5, 5)
        Me.cmdCard.MaximumSize = New Size(0, 50)
        Me.cmdCard.Size = New System.Drawing.Size(100, 80)
        Me.cmdCard.BringToFront()
        Me.cmdCard.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmdCard.Image = Global.Spectrum.My.Resources.Resources.Card_Normal
        Me.cmdCard.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdCard.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdCard.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdCard.TextImageRelation = TextImageRelation.ImageAboveText

        'cmdCheque
        '
        Me.cmdCheque.Dock = DockStyle.Fill
        Me.cmdCheque.Location = New System.Drawing.Point(5, 5)
        Me.cmdCheque.MaximumSize = New Size(0, 50)
        Me.cmdCheque.Size = New System.Drawing.Size(100, 80)
        Me.cmdCheque.BringToFront()
        Me.cmdCheque.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmdCheque.Image = Global.Spectrum.My.Resources.Resources.Cheque_Normal
        Me.cmdCheque.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdCheque.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdCheque.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdCheque.TextImageRelation = TextImageRelation.ImageAboveText

        'cmdKOTReprint
        '
        Me.cmdKOTReprint.Dock = DockStyle.None
        Me.cmdKOTReprint.Location = New System.Drawing.Point(0, 0)
        Me.cmdKOTReprint.MaximumSize = New Size(109, 50)
        Me.cmdKOTReprint.Size = New System.Drawing.Size(109, 50)
        Me.cmdKOTReprint.BringToFront()
        Me.cmdKOTReprint.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmdKOTReprint.Image = Global.Spectrum.My.Resources.Resources.PrintKOT_CSA
        Me.cmdKOTReprint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdKOTReprint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdKOTReprint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdKOTReprint.TextImageRelation = TextImageRelation.ImageAboveText


        'cmdWriteOff
        '
        Me.cmdWriteOff.Dock = DockStyle.None
        Me.cmdWriteOff.Location = New System.Drawing.Point(112, 0)
        Me.cmdWriteOff.MaximumSize = New Size(0, 50)
        Me.cmdWriteOff.Size = New System.Drawing.Size(100, 80)
        Me.cmdWriteOff.BringToFront()
        Me.cmdWriteOff.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmdWriteOff.Image = Global.Spectrum.My.Resources.Resources.WriteOffnew
        Me.cmdWriteOff.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdWriteOff.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdWriteOff.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdWriteOff.TextImageRelation = TextImageRelation.ImageAboveText

        '
        Me.cmdPrintCreditSaleReport.Dock = DockStyle.None
        Me.cmdPrintCreditSaleReport.Location = New System.Drawing.Point(112, 0)
        Me.cmdPrintCreditSaleReport.MaximumSize = New Size(0, 50)
        Me.cmdPrintCreditSaleReport.Size = New System.Drawing.Size(100, 80)
        Me.cmdPrintCreditSaleReport.BringToFront()
        Me.cmdPrintCreditSaleReport.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmdPrintCreditSaleReport.Image = Global.Spectrum.My.Resources.Resources.WriteOffnew
        Me.cmdPrintCreditSaleReport.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrintCreditSaleReport.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPrintCreditSaleReport.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdPrintCreditSaleReport.TextImageRelation = TextImageRelation.ImageAboveText


        Me.cmdNEFT.Dock = DockStyle.None
        Me.cmdNEFT.Location = New System.Drawing.Point(112, 0)
        Me.cmdNEFT.MaximumSize = New Size(0, 50)
        Me.cmdNEFT.Size = New System.Drawing.Size(100, 80)
        Me.cmdNEFT.BringToFront()
        Me.cmdNEFT.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmdNEFT.Image = Global.Spectrum.My.Resources.Resources.RTGS_Blue
        Me.cmdNEFT.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdNEFT.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdNEFT.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdNEFT.TextImageRelation = TextImageRelation.ImageAboveText


        Me.cmdRTGS.Dock = DockStyle.None
        Me.cmdRTGS.Location = New System.Drawing.Point(112, 0)
        Me.cmdRTGS.MaximumSize = New Size(0, 50)
        Me.cmdRTGS.Size = New System.Drawing.Size(100, 80)
        Me.cmdRTGS.BringToFront()
        Me.cmdRTGS.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmdRTGS.Image = Global.Spectrum.My.Resources.Resources.RTGS_Blue
        Me.cmdRTGS.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdRTGS.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdRTGS.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdRTGS.TextImageRelation = TextImageRelation.ImageAboveText

        Me.CmdDiscount.Dock = DockStyle.None
        Me.CmdDiscount.Location = New System.Drawing.Point(112, 0)
        Me.CmdDiscount.MaximumSize = New Size(0, 50)
        Me.CmdDiscount.Size = New System.Drawing.Size(100, 80)
        Me.CmdDiscount.BringToFront()
        Me.CmdDiscount.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmdDiscount.Image = Global.Spectrum.My.Resources.Resources.defaultPromo_Normal
        Me.CmdDiscount.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdDiscount.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdDiscount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.CmdDiscount.TextImageRelation = TextImageRelation.ImageAboveText

        'cmdRefreshGrid
        '
        'Me.cmdRefreshGrid.Dock = DockStyle.Right
        'Me.cmdRefreshGrid.Location = New System.Drawing.Point(500, 4)
        'Me.cmdRefreshGrid.MaximumSize = New Size(25, 25)
        'Me.cmdRefreshGrid.Size = New System.Drawing.Size(25, 25)
        'Me.cmdRefreshGrid.BringToFront()
        'Me.cmdRefreshGrid.Font = New System.Drawing.Font("Verdana", 9.0!)
        'Me.cmdRefreshGrid.Image = Global.Spectrum.My.Resources.Resources.Refresh_CSA2
        'Me.cmdRefreshGrid.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        ''Me.cmdRefreshGrid.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdRefreshGrid.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'Me.cmdRefreshGrid.TextImageRelation = TextImageRelation.ImageAboveText
        'Me.cmdRefreshGrid.Text = ""
        Return ""
    End Function
#Region "SO PRINT"

    Dim drSOPrintHeader As DataRow
    Dim dtHeaderDetails As DataTable
    Dim DocumentNo As String
    Dim DtSoBulkRemarks As New DataTable
    Dim dtOrderDetails As DataTable
    Dim dtOrderComboDetails As DataTable
    Dim dtPaymentDetails1 As DataTable
    Dim dtPaymentDetails As DataTable
    Dim dtDeliveryDetails As DataTable
    Dim dtRemark As DataTable
    Dim dtAddress As DataTable
    Dim dtStrDetails As DataTable
    Dim dtPickupHistory As DataTable
    Dim dtStrPrint As DataTable
    Dim dtOrderAddresses As DataTable
    Dim dsPackagingDelivery As DataSet
    Dim dtCustmInfo As New DataTable
    Dim _pickupHistory As New DataSet
    Dim dtPackagingPrintBox As New DataTable
    Dim objcomm As New clsCommon
    Dim BarCodestring As String
    Dim path As String = ""
    Dim dsInv As New DataSet
    Dim ProgramID As String
    Dim dsSOInvoice As New DataSet
    Dim objCustm As New clsCLPCustomer
    Dim _dsPackagingVar As New DataSet
    Dim dtArticleWisePaymentDetails As DataTable
    Dim dtReturnOrderComboDtl As DataTable
    Dim DtCustDtlForSOPrint As DataTable ' vipin
    Dim DtComboGridData As DataTable 'vipin

    Public Sub SOprint(DocumentNumber As String)
        Dim clsPCCommon As New clsSalesOrderPC
        Dim ObjclsCommon As New clsCommon
        Dim ClientName As String = clsDefaultConfiguration.ClientName
        Dim TerminalID As String = clsAdmin.TerminalID
        Dim UserName As String = clsAdmin.UserName
        Dim dtSiteInfo As DataTable = objcomm.GetSiteInfo(clsAdmin.SiteCode)
        DocumentNo = DocumentNumber
        dsSOInvoice = objPCSO.GetSOTableStruct(clsAdmin.SiteCode, DocumentNo)
        Dim CUSTNo As String
        If dsSOInvoice.Tables("SalesOrderHDR").Rows.Count > 0 Then
            CUSTNo = dsSOInvoice.Tables("SalesOrderHDR").Rows(0)("CustomerNo")
        End If
        _dsPackagingVar = objPCSO.SetSalesOrderPackVariationInSO(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
        dtCustmInfo = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CUSTNo, CustFormat:=clsDefaultConfiguration.DetailedCustomerCreationformat, IsNewSalesOrder:=clsDefaultConfiguration.IsNewSalesOrder)
        dsPackagingDelivery = objPCSO.SetSalesOrderDeliveryInSO(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
        dtPackagingPrintBox = ObjclsCommon.GetPackagingBox(clsAdmin.SiteCode, 2)
        dsInv = objPCSO.SetInvoiceInSOCancel(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
        ProgramID = objPCSO.GetCLPProgramID(clsAdmin.SiteCode)
        dtOrderAddresses = objcomm.GetSOAddresses(CUSTNo, ProgramID, True)
        dtStrPrint = objPCSO.GetSalesOrderSTRPrint(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
        _pickupHistory = objPCSO.GetSalesOrderPickupHistory(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
        dtReturnOrderComboDtl = objPCSO.SOPrintReturnOrderComboDetails() '' added by ketan SO Return chnages 
        dtHeaderDetails = objPCSO.SOPrintHeader(dtSiteInfo, dtCustmInfo, DocumentNo, dsSOInvoice, ClientName, TerminalID, UserName)
        dtOrderDetails = objPCSO.SOPrintOrderDetails(dsSOInvoice, dsPackagingDelivery, clsDefaultConfiguration.PackageFiedlsAllowed, dtPackagingPrintBox)
        dtOrderComboDetails = objPCSO.SOPrintOrderComboDetails(dsSOInvoice)
        dtPaymentDetails1 = objPCSO.SOPrintPaymentDetails1(dsSOInvoice, DocumentNo, clsAdmin.SiteCode, dsInv)
        dtPaymentDetails = objPCSO.SOPrintPaymentDetails(dsInv, clsAdmin.SiteCode, DocumentNo)
        dtDeliveryDetails = objPCSO.SOPrintDeliveryDetails(dsSOInvoice, clsDefaultConfiguration.PackageFiedlsAllowed, dtPackagingPrintBox, dtOrderAddresses)
        dtStrDetails = objPCSO.SOStrDetails(dtStrPrint)
        dtAddress = objPCSO.GetSOPrintAddress(dsSOInvoice, dtOrderAddresses)
        dtRemark = objPCSO.SOPrintRemarks(dsSOInvoice)
        dtPickupHistory = objPCSO.SOPrintPickupHistory(dsSOInvoice, _pickupHistory, dtPackagingPrintBox, clsDefaultConfiguration.PackageFiedlsAllowed)
        soprintarticlepaymentwisedetails()
        DtCustDtlForSOPrint = ObjclsCommon.GetCustDetailForSoPrint(dtCustmInfo.Rows(0)("CustomerNo").ToString())
        DtComboGridData = ObjclsCommon.GetComboDtlForSoPrint(DocumentNo)

        BarCodestring = ImageToBase64(DocumentNo)
        'GenerateSoDeliveryPrint()
        GenerateSOPrint()
        GenerateOrderPreparationAsPerDeliveryDetails(dtDeliveryDetails)
        'GenerateOrderPreparationPrint()
    End Sub
    Dim NEWdtDeliveryDetails As DataTable
    Dim NEWdtOrderComboDetails As DataTable
    Dim NewdtRemark As DataTable
    Private Function GenerateOrderPreparationAsPerDeliveryDetails(ByVal dtDeliveryDetails As DataTable)
        Try
            ' Dim TEMPdtDeliveryDetails = dtDeliveryDetails
            Dim DVdtOrderComboDetails As New DataView(dtOrderComboDetails)
            Dim NewdvRemark As New DataView(dtRemark)
            NewdtRemark = dtRemark.Copy
            Dim dvDeliveryDate As New DataView(dtDeliveryDetails)
            Dim dvDeliveryDateNEW As New DataView(dtDeliveryDetails)
            NEWdtOrderComboDetails = dtOrderComboDetails.Copy
            NEWdtDeliveryDetails = dtDeliveryDetails
            dvDeliveryDate = dvDeliveryDate.ToTable(True, "DeliveryTime", "DeliveryAddress").DefaultView
            dvDeliveryDate.Sort = "DeliveryTime ASC"
            '' SO Preparation Print call from here for Consolidated purpose 
            GenerateOrderPreparationPrint(True)

            If dvDeliveryDate.Count > 1 Then
                Dim i As Integer = 1
                For Each dr As DataRowView In dvDeliveryDate
                    dvDeliveryDateNEW.RowFilter = "DeliveryAddress='" & dr("DeliveryAddress").ToString & "' AND DeliveryTime='" & dr("DeliveryTime").ToString & "'"

                    ' dvDeliveryDateNEW.RowFilter = "DeliveryTime='" & dr("DeliveryTime").ToString & "'"
                    NEWdtOrderComboDetails.Rows.Clear()
                    NEWdtDeliveryDetails = dvDeliveryDateNEW.ToTable()
                    Dim datatable As DataTable
                    For Each drCombo As DataRowView In dvDeliveryDateNEW
                        DVdtOrderComboDetails.RowFilter = "SrNo='" & drCombo("SrNo").ToString & "'"
                        datatable = DVdtOrderComboDetails.ToTable
                        For Each drPrintCombo As DataRow In datatable.Rows
                            NEWdtOrderComboDetails.ImportRow(drPrintCombo)
                        Next
                    Next
                    If Not dtRemark Is Nothing AndAlso dtRemark.Rows.Count > 0 Then
                        NewdtRemark.Rows.Clear()
                        For Each drRemark As DataRowView In dvDeliveryDateNEW
                            NewdvRemark.RowFilter = "BillLineNo='" & drRemark("BillLineNo").ToString & "'"
                            datatable = NewdvRemark.ToTable
                            For Each drPrintRemark As DataRow In datatable.Rows
                                NewdtRemark.ImportRow(drPrintRemark)
                            Next
                        Next

                    End If

                    '' added by ketan Combo variation detail artical add in print 
                    objPCSO.SOPrintComboVariationDetails(dtOrderComboDetails, NEWdtOrderComboDetails)
                    ''SO Preparation Print call from here for Individual bifurcations  
                    GenerateOrderPreparationPrint(False, i)
                    i = i + 1
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Function soprintarticlepaymentwisedetails() As Boolean 'by ketan 
        Try
            Dim dtPickUpDiscGrid As New DataTable
            If dsSOInvoice.Tables("Salesorderhdr").Rows.Count > 0 Then
                dtPickUpDiscGrid = objPCSO.GetSalesOrderPickUpDisc(clsAdmin.SiteCode, IIf(dsSOInvoice.Tables("Salesorderhdr").Rows(0)("Saleordernumber") = String.Empty, 0, dsSOInvoice.Tables("Salesorderhdr").Rows(0)("Saleordernumber")))
            End If
            Dim a = dsPackagingDelivery
            dtArticleWisePaymentDetails = objPCSO.GetSOPrintArticleWisePaymentTableStruc()
            dtArticleWisePaymentDetails.Rows.Clear()
            Dim otherCharges As Double
            If dsSOInvoice.Tables("SalesOrderOtherCharges").Rows.Count() > 0 Then
                otherCharges = dsSOInvoice.Tables("SalesOrderOtherCharges").Compute("Sum(ChargeAmount)", "")
            End If
            Dim i As Integer = 1
            If dsSOInvoice.Tables("sopackagingboxdeliverydtl").Rows.Count > 0 Then
                Dim TotalDiscountAmt As Decimal = 0
                Dim RegularetaxPResent As Boolean = True
                If CDbl(dsSOInvoice.Tables("soitempackagingboxdtl").Compute("SUM(discountamount)", "")) = 0 Then
                    RegularetaxPResent = False
                End If
                If CDbl(dsSOInvoice.Tables("soitempackagingboxdtl").Compute("SUM(discountamount)", "")) = 0 AndAlso dtPickUpDiscGrid.Rows.Count > 0 Then
                    TotalDiscountAmt = dtPickUpDiscGrid.Compute("SUM(DiscountAmount)", "")
                Else
                    TotalDiscountAmt = dsSOInvoice.Tables("soitempackagingboxdtl").Compute("SUM(DiscountAmount)", "")
                End If
                For Each dr As DataRow In dsSOInvoice.Tables("sopackagingboxdeliverydtl").Select("status=true", "billlineno")
                    Dim drresult As DataRow() = dsSOInvoice.Tables("soitempackagingboxdtl").Select("pkglineno='" + dr("pkglineno").ToString + "'")
                    Dim drpickupqty As DataRow() = (dsPackagingDelivery.Tables(0)).Select("deliverylineno='" + dr("deliverylineno").ToString + "'")
                    Dim drdisc As DataRow() = dsSOInvoice.Tables("sopackagingdiscdtl").Select("pkglineno='" + dr("pkglineno").ToString + "' and status=true")
                    Dim drtax As DataRow() = dsSOInvoice.Tables("salesordertaxdtls").Select("packagingindex='" + dr("pkglineno").ToString + "' and status=true")
                    Dim DsPackVar As DataRow() = _dsPackagingVar.Tables(0).Select("pkglineno='" + dr("pkglineno").ToString + "'")
                    Dim DrDiscDtl As DataRow() = dtPickUpDiscGrid.Select("pkglineno='" + dr("pkglineno").ToString + "'")
                    Dim dttx As DataTable
                    If drtax.Length > 0 Then
                        Dim taxtlabel As String = String.Empty
                        For index = 0 To drtax.Length - 1
                            taxtlabel = taxtlabel & ",'" & drtax(index)("taxlabel").ToString & "'"

                        Next
                        taxtlabel = taxtlabel.Substring(1, taxtlabel.Length - 1)
                        dttx = clsCommon.GetArticleGSTTax(taxtlabel)
                    End If

                    drSOPrintHeader = dtArticleWisePaymentDetails.NewRow()
                    drSOPrintHeader("billlineno") = dr("billlineno")
                    Dim drsrno As DataRow() = dtArticleWisePaymentDetails.Select("billlineno='" + dr("billlineno").ToString + "'")
                    If drsrno.Length = 0 Then
                        drSOPrintHeader("srno") = i
                    Else
                        i = i - 1
                        drSOPrintHeader("srno") = i & "." & drsrno.Length
                    End If
                    Dim discription = dsSOInvoice.Tables("salesorderbulkcombohdr").Select("combosrno='" & dr("billlineno") & "'")
                    If discription.Length > 0 Then
                        drSOPrintHeader("articledescription") = discription(0)("packagingboxname")
                    Else
                        drSOPrintHeader("articledescription") = clsCommon.GetArticleFullName(dr("articlecode")).ToString()
                    End If
                    drSOPrintHeader("quantity") = dr("quantity")
                    drSOPrintHeader("price") = drresult(0)("sellingprice")
                    drSOPrintHeader("netamt") = drresult(0)("sellingprice") * dr("quantity")
                    drSOPrintHeader("othercharges") = otherCharges
                    '       If drdisc.Length > 0 Then

                    If RegularetaxPResent = False Then
                        If DrDiscDtl.Length > 0 Then
                            drresult(0)("discountamount") = DrDiscDtl(0)("DiscountAmount")
                        Else
                            drresult(0)("discountamount") = 0
                        End If
                    End If
                    'If drresult.Length > 0 Then
                    '    'drsoprintheader("discount") = drdisc(0)("discountamount")
                    '    'drsoprintheader("discountper") = drdisc(0)("discountper")
                    '    'drsoprintheader("taxableamount") = (drresult(0)("sellingprice") * dr("quantity")) - (drdisc(0)("discountamount"))
                    '    drSOPrintHeader("discount") = drresult(0)("discountamount")  'vipin
                    '    drSOPrintHeader("discountper") = drresult(0)("discountpercentage")
                    '    drSOPrintHeader("taxableamount") = (drresult(0)("sellingprice") * dr("quantity")) - (drresult(0)("discountamount"))
                    'Else
                    '    drSOPrintHeader("discount") = 0
                    '    drSOPrintHeader("discountper") = 0
                    '    drSOPrintHeader("taxableamount") = (drresult(0)("sellingprice") * dr("quantity"))
                    'End If

                    If drresult.Length > 0 Then
                        drSOPrintHeader("discount") = ((drresult(0)("discountamount") / DsPackVar(0)("Quantity")) * dr("quantity")) 'vipin 
                        If drresult(0)("discountamount") = 0 Then 'vipin
                            drSOPrintHeader("discountper") = Math.Round(0, 2)
                        Else
                            drSOPrintHeader("discountper") = (drSOPrintHeader("discount") * 100) / (drresult(0)("sellingprice") * dr("quantity")) ' Math.Round((drresult(0)("discountamount") / TotalDiscountAmt) * 100)  'vipin
                        End If
                        drSOPrintHeader("taxableamount") = (drresult(0)("sellingprice") * dr("quantity")) - (drSOPrintHeader("discount"))
                    Else
                        drSOPrintHeader("discount") = 0
                        drSOPrintHeader("discountper") = 0
                        drSOPrintHeader("taxableamount") = (drresult(0)("sellingprice") * dr("quantity"))
                    End If

                    ''added by vipin
                    'drSOPrintHeader("cgstvalue") = DsPackVar(0)("TotalTaxAmount") / 2
                    'drSOPrintHeader("sgstvalue") = DsPackVar(0)("TotalTaxAmount") / 2

                    drSOPrintHeader("cgstvalue") = ((DsPackVar(0)("TotalTaxAmount") / DsPackVar(0)("Quantity")) * dr("quantity")) / 2 'DsPackVar(0)("TotalTaxAmount") / 2
                    drSOPrintHeader("sgstvalue") = ((DsPackVar(0)("TotalTaxAmount") / DsPackVar(0)("Quantity")) * dr("quantity")) / 2 ' DsPackVar(0)("TotalTaxAmount") / 2

                    drSOPrintHeader("cgst") = (drSOPrintHeader("cgstvalue") / drSOPrintHeader("taxableamount")) * 100
                    drSOPrintHeader("sgst") = (drSOPrintHeader("sgstvalue") / drSOPrintHeader("taxableamount")) * 100


                    drSOPrintHeader("grossamt") = drSOPrintHeader("taxableamount") + drSOPrintHeader("cgstvalue") + drSOPrintHeader("sgstvalue")
                    ' drSOPrintHeader("hsncode") = objComn.GetHSNbyArticle(dr("articlecode"))
                    If dr("IsCombo") = True Then  'vipin to hide HSN in case of combo
                        drSOPrintHeader("hsncode") = ""
                        drSOPrintHeader("IsCombo") = "Y"
                    Else
                        drSOPrintHeader("hsncode") = objcomm.GetHSNbyArticle(dr("articlecode"))
                        drSOPrintHeader("IsCombo") = "N"
                    End If
                    'drSOPrintHeader("hsncode") = objComn.GetHSNbyArticle(dr("articlecode"))
                    dtArticleWisePaymentDetails.Rows.Add(drSOPrintHeader)
                    i = i + 1
                Next
            End If
        Catch ex As Exception

        End Try

    End Function
    Public Function ImageToBase64(ByVal CodeString As String) As String
        Try
            Dim VarBarcode As C1BarCode
            Dim s As C1BarCode = GetBarcode(CodeString)
            VarBarcode = s
            Dim mImage = VarBarcode.Image
            Dim uPix As GraphicsUnit = GraphicsUnit.Pixel
            Using ms As New MemoryStream()
                ' Convert Image to byte[]
                mImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                Dim imageBytes As Byte() = ms.ToArray()
                ' Convert byte[] to Base64 String
                Dim base64String As String = Convert.ToBase64String(imageBytes)
                Return base64String
            End Using
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Dim clsSoCommon As New clsSalesOrderPC
    Private Function GenerateOrderPreparationPrint(Optional ByVal consolidated As Boolean = False, Optional ByVal PrintID As Integer = 0) As Boolean
        Try

            If Not dtHeaderDetails Is Nothing AndAlso dtHeaderDetails.Rows.Count > 0 Then
                dtHeaderDetails.Rows(0)("FooterMassage") = "This document is printed "
            End If
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\SalesOrderPreparation.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim DataSource As New ReportDataSource("dsSalesPreparationHeader", dtHeaderDetails)
            Dim DataSource1 As New ReportDataSource("dsSalesPreparationDeliveryDetails", NEWdtDeliveryDetails)
            Dim DataSource2 As New ReportDataSource("dsSalesPreparationRemarks", NewdtRemark)
            Dim DataSource3 As New ReportDataSource("dsSalesPreparationOrderDetails", NEWdtOrderComboDetails)
            Dim DataSource4 As New ReportDataSource("dsSalesPreparationSTRDetails", dtStrDetails)
            Dim DataSource5 As New ReportDataSource("SdBalToPay", dtPaymentDetails1)
            Dim DataSource6 As New ReportDataSource("Ds_salesOrderPrintAddress", dtAddress)
            Dim DataSource7 As New ReportDataSource("DS_salesReturnOrderPrintOrderDetails", dtReturnOrderComboDtl) '' added by ketan So Return Chnages 
            Dim DataSource8 As New ReportDataSource("CustDetail", DtCustDtlForSOPrint)

            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            reportViewer2.LocalReport.DataSources.Add(DataSource3)
            reportViewer2.LocalReport.DataSources.Add(DataSource4)
            reportViewer2.LocalReport.DataSources.Add(DataSource5)
            reportViewer2.LocalReport.DataSources.Add(DataSource6)
            reportViewer2.LocalReport.DataSources.Add(DataSource7)
            reportViewer2.LocalReport.DataSources.Add(DataSource8)

            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            ' path = clsDefaultConfiguration.DayCloseReportPath & "\SOPreparationPrint_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
            Dim SOPath As String = clsDefaultConfiguration.DayCloseReportPath 'NEWdtDeliveryDetails, 
            Dim Newpath As String = ""
            If consolidated = True Then
                Newpath = clsSoCommon.CreatePathForPrint(dtHeaderDetails, NEWdtDeliveryDetails, SOPath, "Consolidated-Order_prep", consolidated, True)
            Else
                Newpath = clsSoCommon.CreatePathForPrint(dtHeaderDetails, NEWdtDeliveryDetails, SOPath, "Order_prep" & PrintID & "", consolidated, True)
            End If
            path = Newpath
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            If clsDefaultConfiguration.SOPrintPreviewAllowed = True Then
                Process.Start(path)
            Else
                'Code For Print SO
                PrinterName = SetPrinterName(dtPrinterInfo, "SalesOrder", "SOInvc")
                Dim pdfdocument As New PdfDocument()
                pdfdocument.LoadFromFile(path)
                pdfdocument.PrinterName = PrinterName
                pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                pdfdocument.PrintDocument.Print()
                pdfdocument.Dispose()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Function GenerateSOPrint() As Boolean
        Try
            If Not dtHeaderDetails Is Nothing AndAlso dtHeaderDetails.Rows.Count > 0 Then
                dtHeaderDetails.Rows(0)("FooterMassage") = "This is computer generated invoice"
            End If
            Dim reportViewer2 As New ReportViewer()
            '  Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\SalesOrderPrint.rdl")
            Dim appPath As String
            If clsDefaultConfiguration.ColabaSOPrint Then
                appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\ColabaSOPrint.rdl")
            Else
                appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\SalesOrderPrint.rdl")
            End If
            reportViewer2.LocalReport.ReportPath = appPath

            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            reportViewer2.LocalReport.SetParameters(New ReportParameter("BarCode", BarCodestring))
            Dim DataSource As New ReportDataSource("DS_SalesOrderPrintHeader", dtHeaderDetails)
            Dim DataSource1 As New ReportDataSource("DS_SalesOrderPrintPaymentDetails", dtPaymentDetails)
            Dim DataSource2 As New ReportDataSource("DS_salesOrderPrintPaymentsDetails1", dtPaymentDetails1)
            Dim DataSource3 As New ReportDataSource("DS_SalesOrderPrintDeliveryDetails", dtDeliveryDetails)
            Dim DataSource4 As New ReportDataSource("DS_SalesOrderPrintRemarks", dtRemark)
            Dim DataSource5 As New ReportDataSource("DS_salesOrderPrintOrderDetails", dtOrderComboDetails)
            Dim DataSource6 As New ReportDataSource("Ds_salesOrderPrintAddress", dtAddress)
            Dim DataSource7 As New ReportDataSource("DS_ArticleWiseGST", dtArticleWisePaymentDetails)
            Dim DataSource8 As New ReportDataSource("DS_salesReturnOrderPrintOrderDetails", dtReturnOrderComboDtl) '' added by ketan So Return Chnages 
            Dim DataSource9 As New ReportDataSource("CustDetail", DtCustDtlForSOPrint)

            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            reportViewer2.LocalReport.DataSources.Add(DataSource3)
            reportViewer2.LocalReport.DataSources.Add(DataSource4)
            reportViewer2.LocalReport.DataSources.Add(DataSource5)
            reportViewer2.LocalReport.DataSources.Add(DataSource6)
            reportViewer2.LocalReport.DataSources.Add(DataSource7)
            reportViewer2.LocalReport.DataSources.Add(DataSource8)
            reportViewer2.LocalReport.DataSources.Add(DataSource9)
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            ' path = clsDefaultConfiguration.DayCloseReportPath & "\SOInvoice_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
            Dim Newpath As String = ""
            Dim SOPath As String = clsDefaultConfiguration.DayCloseReportPath 'NEWdtDeliveryDetails, 
            Newpath = clsSoCommon.CreatePathForPrint(dtHeaderDetails, NEWdtDeliveryDetails, SOPath, "SOInvoice", False, False)
            path = Newpath
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using
            If clsDefaultConfiguration.SOPrintPreviewAllowed = True Then
                Process.Start(path)
            Else
                'Code For Print SO
                PrinterName = SetPrinterName(dtPrinterInfo, "SalesOrder", "SOInvc")
                Dim pdfdocument As New PdfDocument()
                pdfdocument.LoadFromFile(path)
                pdfdocument.PrinterName = PrinterName
                pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                pdfdocument.PrintDocument.Print()
                pdfdocument.Dispose()
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Function


#End Region
    Sub PrintKOT(ByVal isKOTGenerateWithBill As Boolean)
        Try
            If clsDefaultConfiguration.PrintFormatNo = "1" OrElse clsDefaultConfiguration.PrintFormatNo = "3" OrElse clsDefaultConfiguration.PrintFormatNo = "4" OrElse clsDefaultConfiguration.PrintFormatNo = "5" Then
                If clsDefaultConfiguration.IsArticleWiseKOT.Contains(clsAdmin.TerminalID) Then
                    IsArticleWiseKot = True
                Else
                    IsArticleWiseKot = False
                End If
                If clsDefaultConfiguration.IsCounterCopy.Contains(clsAdmin.TerminalID) Then
                    IsCounterCopy = True
                Else
                    IsCounterCopy = False
                End If
                If clsDefaultConfiguration.IsFinalReceipt.Contains(clsAdmin.TerminalID) Then
                    IsFinalReceipt = True
                Else
                    IsFinalReceipt = False
                End If
            End If
            If grdCreditSales.Rows.Count > 1 Then
                'Step 1 ---- Get loop All SELECTED ROW
                For Each row In grdCreditSales.Rows.Selected
                    Dim _KOTReprintReason As String = ""
                    Dim _billNo As String = row("InvoiceNo")
                    Dim isPrintDetailUpdate As Boolean = False
                    If CheckKOTPrintDetailsExist(clsAdmin.SiteCode, _billNo, clsAdmin.TerminalID) = True Then
                        If isKOTGenerateWithBill = True Then
                            Exit Sub
                        End If
                        Dim dialogResult As DialogResult = MessageBox.Show("KOT has already been generated. Do you again want to Reprint KOT", "", MessageBoxButtons.YesNo)
                        If dialogResult = Windows.Forms.DialogResult.Yes Then
                            Dim objKOTReason As New frmArticlesRemark
                            objKOTReason.IsKOTReason = True
                            _KOTReprintReason = objKOTReason.KOTReasonDetails
                            If objKOTReason.ShowDialog() = Windows.Forms.DialogResult.OK Then
                                _KOTReprintReason = objKOTReason.KOTReasonDetails
                                objKOTReason.Close()
                                isPrintDetailUpdate = True
                                ' KOTPrintDetailsSaveAndUpdate(clsAdmin.SiteCode, _billNo, clsAdmin.TerminalID, clsAdmin.UserCode, isPrintDetailUpdate, _KOTReprintReason)
                            ElseIf objKOTReason.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                                objKOTReason.Close()
                                Exit Sub
                            End If
                        Else
                            Exit Sub
                        End If
                    Else
                        If isKOTGenerateWithBill = True Then
                            isPrintDetailUpdate = False
                        End If
                        'If KOTPrintDetailsSaveAndUpdate(clsAdmin.SiteCode, _billNo, clsAdmin.TerminalID, clsAdmin.UserCode, isPrintDetailUpdate) = False Then
                        '    ShowMessage("KOT Print details failed", "GRV001 - " & getValueByKey("GRV010"))
                        '    Exit Sub
                        'End If
                    End If
                    Dim objclsCashmemo As New clsCashMemo
                    Dim objPrint As New clsCashMemoPrint(_billNo, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
                    objPrint.DisplayArticleFullName = clsDefaultConfiguration.PrintItemFullName
                    objPrint.AllowDecimalQty = clsDefaultConfiguration.AllowDecimalQty
                    objPrint.WeightScaleEnabled = clsDefaultConfiguration.WeightScaleEnabled
                    objPrint.KOTBillPrintingRequired = clsDefaultConfiguration.KOTPrintRequired
                    objPrint.CustomerNameRequiredInKOT = clsDefaultConfiguration.CustomerNameRequiredInKOT
                    objPrint.TokenNoRequiredInKOT = clsDefaultConfiguration.TokenNoRequiredInKOT
                    objPrint.CashMemoResetonDayClose = clsDefaultConfiguration.CashMemoResetonDayClose
                    objPrint.AllowBillingOnlyAfterSelectionOfSalesType = clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType
                    objPrint.PrintFormatNo = clsDefaultConfiguration.PrintFormatNo
                    objPrint.KOTPrintEachlineItem = clsDefaultConfiguration.IsKOTPrintEachlineItem
                    objPrint.KOTPrintForEachQuantity = clsDefaultConfiguration.IsKOTPrintQuantityWise
                    objPrint.KOTPrintFormatNo = clsDefaultConfiguration.KOTPrintFormatNo
                    objPrint.IsKotFontBold = clsDefaultConfiguration.IsKotFontBold
                    objPrint.IsKotFontLarge = clsDefaultConfiguration.IsKotFontLarge
                    objPrint.MettlerConnString = clsDefaultConfiguration.MettlerConnString
                    objPrint.RoundOff = clsDefaultConfiguration.BillRoundOffAt
                    If clsDefaultConfiguration.KOTAndBillGeneration = True Then
                        objPrint.PrintCouponNumberForKOT = True
                    Else
                        objPrint.PrintCouponNumberForKOT = False
                    End If
                    Dim ErrorMsg As String = ""
                    Dim dtView As New DataTable
                    Dim BoldLineLength As Int32 = 27
                    Dim obj As New SpectrumBL.clsCashMemo()
                    dtView = obj.GetCashMemo(_billNo, clsAdmin.SiteCode, clsAdmin.LangCode)

                    Dim dvItemDetail As New DataView(dtView, "", "", DataViewRowState.CurrentRows)
                    Dim DtUnique As DataTable = objclsCashmemo.GetBillDetailsDataForPrint(_billNo, clsAdmin.SiteCode, clsAdmin.LangCode)
                    Dim Dtcombo As DataTable = objclsCashmemo.GetComboDetailsDataForPrint(_billNo, clsAdmin.SiteCode, clsAdmin.LangCode)
                    Dim printSuccess As Boolean = False
                    For index = 1 To 2
                        objPrint.CashMemoKOTPrintBasedOnPrintFormatNo("CMS", ErrorMsg, dtView, DtUnique, Dtcombo, "", clsAdmin.DayOpenDate, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy)
                        printSuccess = True
                    Next
                    If printSuccess = True Then
                        If isPrintDetailUpdate = True Then
                            If KOTPrintDetailsSaveAndUpdate(clsAdmin.SiteCode, _billNo, clsAdmin.TerminalID, clsAdmin.UserCode, isPrintDetailUpdate, _KOTReprintReason) = False Then
                                ShowMessage("KOT Print details failed", "GRV001 - " & getValueByKey("GRV010"))
                                'Exit Sub
                            End If
                        Else
                            If KOTPrintDetailsSaveAndUpdate(clsAdmin.SiteCode, _billNo, clsAdmin.TerminalID, clsAdmin.UserCode, isPrintDetailUpdate) = False Then
                                ShowMessage("KOT Print details failed", "GRV001 - " & getValueByKey("GRV010"))
                                'Exit Sub
                            End If
                        End If
                    End If
                Next
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Function KOTPrintDetailsSaveAndUpdate(ByVal _siteCode As String, ByVal _billNo As String, ByVal _terminalId As String, ByVal _userId As String, ByVal isUpdate As Boolean, Optional KOTReprintReason As String = "") As Boolean
        Dim obj As New clsCommon
        Return obj.SaveAndUpdateKOTPrintDetails(_siteCode, _billNo, _terminalId, _userId, isUpdate, KOTReprintReason)
    End Function
    Public Function CheckKOTPrintDetailsExist(ByVal _siteCode As String, ByVal _billNo As String, ByVal _terminalId As String) As Boolean
        Dim obj As New clsCommon
        Return obj.CheckKOTPrintExist(_siteCode, _billNo, _terminalId)
    End Function
    Private Sub cmdKOTReprint_Click(sender As System.Object, e As System.EventArgs) Handles cmdKOTReprint.Click
        PrintKOT(False)
    End Sub

    Private Sub cmdRefreshGrid_Click(sender As System.Object, e As System.EventArgs) Handles cmdRefreshGrid.Click
        BindCreditSales()
    End Sub
    '' added by nikhil For PC

    Private Sub CmdDiscount_Click(sender As Object, e As EventArgs) Handles CmdDiscount.Click
        Try

            If grdCreditSales.Row > 0 Then
                Dim objPaymentDiscount As New frmNAcceptPaymentByDiscount()
                objPaymentDiscount.BillAmount = grdCreditSales.Rows(grdCreditSales.Row)("TotInvAmt")
                objPaymentDiscount.BalAmount = grdCreditSales.Rows(grdCreditSales.Row)("BalanceAmount")
                objPaymentDiscount.BillNumber = grdCreditSales.Rows(grdCreditSales.Row)("DocumentNo")
                objPaymentDiscount.InvoiceNumber = grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo")

                If (grdCreditSales.Rows(grdCreditSales.RowSel)("DocType").ToString().ToLower() = "Cash memo".ToLower()) Then
                    objPaymentDiscount.TranTypeCode = "CM"
                ElseIf (grdCreditSales.Rows(grdCreditSales.RowSel)("DocType").ToString().ToLower() = "sales order".ToLower()) Then
                    objPaymentDiscount.TranTypeCode = "SO"
                Else
                    objPaymentDiscount.TranTypeCode = "CM"
                End If
                If (objPaymentDiscount.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                    frmNCreditSales_Load(Nothing, EventArgs.Empty)
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)

        End Try
    End Sub

    Private Sub cmdPrintCreditSaleReport_Click(sender As Object, e As EventArgs) Handles cmdPrintCreditSaleReport.Click   '' added by nikhil
        Try
            If clsCommon.checkDiscSpce(clsDefaultConfiguration.DayCloseReportPath.Substring(0, 3)) = False Then
                ShowMessage("Insufficient disk space for saving files", "")
            End If

            Dim dt As New DataTable
            Dim objPop As New frmCreditSaleRportPopUp
            Dim datView As New DataView
            datView = grdCreditSales.DataSource
            objPop.DtSale = datView.ToTable
            ' objPop.DtSale = dtCreditSales.Copy
            objPop.ShowDialog()

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdNEFT_Click(sender As Object, e As EventArgs) Handles cmdNEFT.Click
        Try
            PaymentCreditSales(TenderMode:="Neft")
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)

        End Try
    End Sub

    Private Sub cmdRTGS_Click(sender As Object, e As EventArgs) Handles cmdRTGS.Click
        Try
            PaymentCreditSales(TenderMode:="Rtgs")
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)

        End Try
    End Sub

    '' ended by nikhil For PC
End Class