Imports SpectrumPrint
Imports SpectrumBL
Imports SpectrumCommon
Imports Microsoft.Reporting.WinForms
Imports Spire.Pdf
Imports System.IO
Imports C1.Win.C1BarCode

Public Class frmNCreditSales

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
    Dim ScannedDeliveryPersonHold As String = ""
    Dim ScannedDeliveryPersonOldHold As String = ""
    'added by khusrao adil on 21-06-2017 for natural juhu CR
    Dim CSAAuthUserName As String = ""
    'added by khusroa adil on 25-07-2018
    Private AllowInnovitiPayment As Boolean = False
    Private AllowInnovitiWithOtherTender As Boolean = False
    Dim _innovitiCardNo As String = ""
    'added by khusrao adil on 25-07-2018 for innviti with card functionality point _ natural client
    Dim dtInnvoiti As DataTable
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
            Call PaymentCreditSales(TenderMode:="Cash")
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub grdCreditSales_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdCreditSales.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Call PaymentCreditSales(TenderMode:="Cash")
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
                If Not clsDefaultConfiguration.EvasPizzaChanges Then
                    Call PaymentCreditSales(TenderMode:="Cash")
                End If
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
                'added by khusrao adil on 16-06-2017 for natural Juhu CR
                'authorisation req for payment
                If clsDefaultConfiguration.AuthReqForCreditSalesAdjustmentOnCSA = True Then
                    If AuthorizationRequiredAndValid() = True Then
                    Else
                        Exit Sub
                    End If
                End If
                Dim objPaymentCreditSales As New frmPaymentCreditSales()
                objPaymentCreditSales.InvoiceNumber = grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo")
                objPaymentCreditSales.BillNumber = grdCreditSales.Rows(grdCreditSales.Row)("BillNo")
                objPaymentCreditSales.BillAmount = grdCreditSales.Rows(grdCreditSales.Row)("BillAmount")
                objPaymentCreditSales.BalAmount = grdCreditSales.Rows(grdCreditSales.Row)("BalanceAmount")
                objPaymentCreditSales.DeliveryPerson = IIf(grdCreditSales.Rows(grdCreditSales.Row)("DeliveryPerson") Is DBNull.Value, "", grdCreditSales.Rows(grdCreditSales.Row)("DeliveryPerson"))
                objPaymentCreditSales.FloatAmtReturned = IIf(grdCreditSales.Rows(grdCreditSales.Row)("FloatAmtReturned") Is String.Empty, 0, grdCreditSales.Rows(grdCreditSales.Row)("FloatAmtReturned"))
                objPaymentCreditSales.TenderTypeCode = TenderMode


                'code added by vipul for double bill adjustment issue  2683
                Dim dbAdjustedAmt As Double
                dbAdjustedAmt = 0
                dbAdjustedAmt = objCreditSales.BillAdjustAmount(grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo").ToString, clsAdmin.SiteCode)

                If CDbl(grdCreditSales.Rows(grdCreditSales.Row)("BillAmount")) = dbAdjustedAmt Then
                    ShowMessage("Selected Bill has been already adjusted", getValueByKey("CLAE04"))
                    cmdRefreshGrid_Click(Nothing, Nothing)
                    Exit Sub
                End If

                If CDbl(grdCreditSales.Rows(grdCreditSales.Row)("BillAmount")) - dbAdjustedAmt = CDbl(grdCreditSales.Rows(grdCreditSales.Row)("BalanceAmount")) Then

                Else
                    objPaymentCreditSales.BalAmount = CDbl(grdCreditSales.Rows(grdCreditSales.Row)("BillAmount")) - dbAdjustedAmt
                End If



                If clsDefaultConfiguration.AuthReqForCreditSalesAdjustmentOnCSA = True Then
                    objPaymentCreditSales.CSAAuthUserName = CSAAuthUserName
                End If

                If (grdCreditSales.Rows(grdCreditSales.RowSel)("DocType").ToString().ToLower() = "Cash memo".ToLower()) Then
                    objPaymentCreditSales.TranTypeCode = "CM"
                ElseIf (grdCreditSales.Rows(grdCreditSales.RowSel)("DocType").ToString().ToLower() = "sales order".ToLower()) Then
                    objPaymentCreditSales.TranTypeCode = "SO"
                Else
                    objPaymentCreditSales.TranTypeCode = "CM"
                End If

                If (objPaymentCreditSales.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                    'added by khusrao adil for niq-naq
                    Dim code As String = objPaymentCreditSales.TranTypeCode
                    Dim bill As String = objPaymentCreditSales.BillNumber
                    If ObjclsCommon.UpdateDate(code, bill) Then
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

    Private Sub FilterCreditSales(Optional Evasfilter As Boolean = False)

        Try
            Dim rowfilterString As String = String.Empty
            Dim filterString As New System.Text.StringBuilder()
            Dim filterText() As String
            If Evasfilter = True Then
                filterText = txtDeliveryId.Text.ToString().Trim().Split(Space(1))
                For index = 0 To filterText.Count - 1
                    'filterString.AppendFormat("Convert( {0}, 'System.String') LIKE '%{1}%' AND ", "FILTER", Replace(filterText(index).ToString(), "'", "''"))
                    filterString.AppendFormat("Convert( {0}, 'System.String') LIKE '%{1}%' OR ", "DeliveryPerson", Replace(filterText(index).ToString(), "'", "''"))
                    'filterString.Append(" Or")
                    filterString.AppendFormat("Convert( {0}, 'System.String') LIKE '%{1}%' OR ", "InvoiceNo", Replace(filterText(index).ToString(), "'", "''"))
                Next
            Else
                filterText = txtFilterCreditSales.Text.ToString().Trim().Split(Space(1))
                For index = 0 To filterText.Count - 1
                    filterString.AppendFormat("Convert( {0}, 'System.String') LIKE '%{1}%' AND ", "FILTER", Replace(filterText(index).ToString(), "'", "''"))
                    ' filterString.AppendFormat("Convert( {0}, 'System.String') LIKE '%{1}%' AND ", "DeliveryPerson", Replace(filterText(index).ToString(), "'", "''"))
                Next
            End If
            rowfilterString = filterString.ToString().Substring(0, filterString.ToString().Length - 4)
            dtCreditSales.DefaultView.RowFilter = rowfilterString
            grdCreditSales.DataSource = dtCreditSales.DefaultView
            GridColumnSettings()
            If (grdCreditSales.Rows.Count > 1) Then
                grdCreditSales.Select(1, 3)
            End If
            If Evasfilter = True Then
                txtDeliveryId.Select()
            Else
                txtFilterCreditSales.Select()
            End If
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
            'added by khsrao adil on 16-06-2017 for natural Juhu CR -DoneOrderIdColumn
            Dim DoneOrderIdColumn As String = String.Empty
            If clsDefaultConfiguration.IsTablet = True Then
                CouponColumn = "CouponNo,"
            End If
            If clsDefaultConfiguration.DoneSystemApplicable = True Then
                DoneOrderIdColumn = "DoneOrderId,"
            End If
            Dim displayColumns As String = CouponColumn & "InvoiceNo," & DoneOrderIdColumn & " CustomerName,DeliveryPerson,SalesPerson,BillAmount,BalanceAmount,Address,BillCreatedTime,ElapsedTime,TotalAmt,FloatAmtReturned,Dispatch,DispatchTime,DeliveryPartner"
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
            If clsDefaultConfiguration.DoneSystemApplicable = True Then
                grdCreditSales.Cols("DoneOrderId").Caption = "Done Order Id"
            End If
            grdCreditSales.Cols("InvoiceNo").Caption = getValueByKey("frmNCreditSales.grdCreditSales.InvoiceNo")
            grdCreditSales.Cols("CustomerName").Caption = getValueByKey("frmNCreditSales.grdCreditSales.CustomerName")
            grdCreditSales.Cols("DeliveryPerson").Caption = getValueByKey("frmNCreditSales.grdCreditSales.DeliveryPerson")
            grdCreditSales.Cols("SalesPerson").Caption = getValueByKey("frmNCreditSales.grdCreditSales.SalesPerson")
            grdCreditSales.Cols("BillAmount").Caption = getValueByKey("frmNCreditSales.grdCreditSales.BillAmount")
            grdCreditSales.Cols("BalanceAmount").Caption = getValueByKey("frmNCreditSales.grdCreditSales.BalanceAmount")
            grdCreditSales.Cols("Address").Caption = getValueByKey("frmNCreditSales.grdCreditSales.Address")
            grdCreditSales.Cols("BillCreatedTime").Caption = getValueByKey("frmNCreditSales.grdCreditSales.BillCreatedTime")
            grdCreditSales.Cols("BillCreatedTime").Format = "g"
            grdCreditSales.Cols("ElapsedTime").Caption = getValueByKey("frmNCreditSales.grdCreditSales.ElapsedTime")
            grdCreditSales.Cols("TotalAmt").Caption = "Float Amount"
            grdCreditSales.Cols("FloatAmtReturned").Caption = "Float Amount Returned"
            grdCreditSales.Cols("FloatAmtReturned").AllowEditing = True
            grdCreditSales.Cols("Dispatch").DataType = Type.GetType("System.Boolean")
            grdCreditSales.Cols("Dispatch").Caption = "Dispatch"
            grdCreditSales.Cols("DispatchTime").Caption = "Dispatch Time"
            grdCreditSales.Cols("DispatchTime").Format = "hh:mm tt"
            grdCreditSales.Cols("DeliveryPartner").Caption = "Delivery Partner"
            grdCreditSales.Cols("DeliveryPartner").Visible = True
            grdCreditSales.Cols("DeliveryPartner").AllowEditing = False
            grdCreditSales.AutoSizeCols()
            grdCreditSales.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns
            '     End If
            '-------------To Enable Checkbox if Dispatch Time is There and Make Enabling False
            For row = 1 To grdCreditSales.Rows.Count - 1
                If Not String.IsNullOrEmpty(grdCreditSales.Rows(row)("DispatchTime").ToString) Then
                    grdCreditSales.Rows(row)("Dispatch") = True
                End If
            Next
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub frmNCreditSales_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            'added by khusrao adil on 25-07-2018
            AllowInnovitiPayment = False
            AllowInnovitiWithOtherTender = False
            dtInnvoiti = ObjclsCommon.GetInnovitiStruc()
            dtInnvoiti.Clear()
            'code added by vipul for issue id 2716
            cmdDispatch.Visible = True
            Call SetResourcesText()
            Dim objdefaultSO As New clsDefaultConfiguration("SalesOrder")
            objdefaultSO.GetDefaultSettings()

            Dim objdefaultCM As New clsDefaultConfiguration("CMS")
            objdefaultCM.GetDefaultSettings()

            Call BindCreditSales()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            If clsDefaultConfiguration.EvasPizzaChanges Then
                txtDeliveryId.Focus()
            Else
                txtDeliveryId.Visible = False
                lblDeliveryId.Visible = False
            End If
            'Checking Authorization for Write Off Transaction
            If CheckAuthorisation(clsAdmin.UserCode, "CRDSale") = False Then
                cmdWriteOff.Visible = False
                Exit Sub
            End If
            cmdWriteOff.Visible = clsDefaultConfiguration.AllowCreditSaleWriteOff.Contains(clsAdmin.TerminalID)
            'If clsDefaultConfiguration.KOTAndBillGeneration = True Then
            '    cmdKOTReprint.Visible = True
            '    If cmdWriteOff.Visible = False Then
            '        cmdKOTReprint.Location = New System.Drawing.Point(0, 0)
            '        Me.cmdKOTReprint.Size = New System.Drawing.Size(109, 43)
            '        Me.cmdKOTReprint.BringToFront()
            '    End If
            'Else
            '    cmdKOTReprint.Visible = False
            'End If
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
            txtFilterCreditSales.Text = String.Empty
            txtDeliveryId.Text = String.Empty
            dtCreditSales = objCreditSales.GetCreditSales(clsAdmin.SiteCode)
            If dtCreditSales IsNot Nothing Then
                dtCreditSales.DefaultView.Sort = "BillTime DESC"
                If clsDefaultConfiguration.DoneSystemApplicable Then
                    If clsDefaultConfiguration.ExternalOrdersTillNo = clsAdmin.TerminalID Then
                        Dim dr() = dtCreditSales.Select("TerminalId='" & clsDefaultConfiguration.ExternalOrdersTillNo & "'")
                        If dr.Count > 0 Then
                            dtCreditSales = dtCreditSales.Select("TerminalId='" & clsDefaultConfiguration.ExternalOrdersTillNo & "'").CopyToDataTable()
                            dtCreditSales.DefaultView.Sort = "BillTime DESC"
                        Else
                            dtCreditSales.Rows.Clear()
                        End If
                    Else
                        Dim drt() = dtCreditSales.Select("TerminalId<>'" & clsDefaultConfiguration.ExternalOrdersTillNo & "'")
                        If drt.Count > 0 Then
                            dtCreditSales = dtCreditSales.Select("TerminalId<>'" & clsDefaultConfiguration.ExternalOrdersTillNo & "'").CopyToDataTable()
                            dtCreditSales.DefaultView.Sort = "BillTime DESC"
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
                        row("FILTER") = row("FILTER") & row("CouponNo")
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

    'added by khusrao adil on 06-06-2017 for natural juhu CR
    ' authorisation req for payment 
    Public Function AuthorizationRequiredAndValid() As Boolean
        Try
            AuthorizationRequiredAndValid = False
            Dim objAuth As New frmNUserAuthorisation("CSA_Settle", Nothing)
            objAuth.SendFromCSAScreen = True
            Dim result = objAuth.ShowDialog()
            If result = DialogResult.Yes Then
                If objAuth.Authorized = True Then
                    CSAAuthUserName = objAuth.CSAAuthUserName
                    AuthorizationRequiredAndValid = True
                End If
            ElseIf result = Windows.Forms.DialogResult.No Then
                AuthorizationRequiredAndValid = False
            ElseIf result = Windows.Forms.DialogResult.Cancel Then
                AuthorizationRequiredAndValid = False
            End If
            Return AuthorizationRequiredAndValid
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function

    Private Sub cmdPayments_Click(sender As System.Object, e As System.EventArgs) Handles cmdPayments.Click
        Try
            If grdCreditSales.Rows.Count > 1 Then
                'added by khusrao adil on 16-06-2017 for natural Juhu CR
                'authorisation req for payment
                If clsDefaultConfiguration.AuthReqForCreditSalesAdjustmentOnCSA = True Then
                    If AuthorizationRequiredAndValid() = True Then
                    Else
                        Exit Sub
                    End If
                End If

                Dim payment As New frmNAcceptPaymentPC()
                payment.CreditSettlement = True
                ' payment.TotalBillAmount =
                payment.CustomerWantPay = grdCreditSales.Rows(grdCreditSales.Row)("BalanceAmount")
                If Not IsDBNull(grdCreditSales.Rows(grdCreditSales.Row)("CardNo")) Then
                    payment.CLPCustomerCardNumber = grdCreditSales.Rows(grdCreditSales.Row)("CardNo")
                End If
                Dim DeliveryPerson As String = IIf(grdCreditSales.Rows(grdCreditSales.Row)("DeliveryPerson") Is DBNull.Value, "", grdCreditSales.Rows(grdCreditSales.Row)("DeliveryPerson"))
                Dim FloatAmtReturned As Double = IIf(grdCreditSales.Rows(grdCreditSales.Row)("FloatAmtReturned") Is String.Empty, 0, grdCreditSales.Rows(grdCreditSales.Row)("FloatAmtReturned"))
                Dim BillNumber As String = grdCreditSales.Rows(grdCreditSales.Row)("BillNo")
                Dim InvoicBillNumber As String = grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo")

                'code added by vipul for double bill adjustment issue  2683
                Dim dbAdjustedAmt As Double
                dbAdjustedAmt = 0
                dbAdjustedAmt = objCreditSales.BillAdjustAmount(grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo").ToString, clsAdmin.SiteCode)

                If CDbl(grdCreditSales.Rows(grdCreditSales.Row)("BillAmount")) = dbAdjustedAmt Then
                    ShowMessage("Selected Bill has been already adjusted", getValueByKey("CLAE04"))
                    cmdRefreshGrid_Click(Nothing, Nothing)
                    Exit Sub
                End If

                If CDbl(grdCreditSales.Rows(grdCreditSales.Row)("BillAmount")) - dbAdjustedAmt = CDbl(grdCreditSales.Rows(grdCreditSales.Row)("BalanceAmount")) Then
                Else
                    payment.CustomerWantPay = CDbl(grdCreditSales.Rows(grdCreditSales.Row)("BillAmount")) - dbAdjustedAmt
                End If
                payment._IsCreditSale = True


                Dim dtCustomer As New DataTable   '' added by vipin PC merge 15.05.2018
                dtCustomer = ObjclsCommon.GetCustomerDetails(ObjclsCommon.GetBillDetail(BillNumber, clsAdmin.SiteCode), clsAdmin.SiteCode)
                If Not dtCustomer Is Nothing AndAlso dtCustomer.Rows.Count > 0 Then
                    payment.CustName = dtCustomer.Rows(0)("NameOnCard").ToString
                    payment.CompName = dtCustomer.Rows(0)("CompanyName").ToString
                    payment.MobNumber = dtCustomer.Rows(0)("Mobileno").ToString
                End If
                payment.ShowDialog()

                'added by khusrao adil on 25-07-2018
                AllowInnovitiPayment = payment.AllowInnovitiPayment
                AllowInnovitiWithOtherTender = payment.AllowInnovitiWithOtherTender
                dtInnvoiti.Clear()
                dtInnvoiti = payment.dtInnoviti
                If payment.IsCancelAcceptPayment = False Then
                    'If BillNo = "" Then
                    If payment.DialogResult <> Windows.Forms.DialogResult.Cancel Then
                        'added by khusrao adil on 25-07-2018
                        Dim aplicableWithInnvo As Boolean = False
                        If AllowInnovitiWithOtherTender = True And AllowInnovitiPayment = True Then
                            aplicableWithInnvo = True
                        ElseIf AllowInnovitiWithOtherTender = False And AllowInnovitiPayment = True Then
                            aplicableWithInnvo = True
                        ElseIf AllowInnovitiWithOtherTender = False And AllowInnovitiPayment = False Then
                            aplicableWithInnvo = False
                        End If
                        'added by khusrao adil on 19-05-2017 for innoviti
                        Dim RetrievalReferenceNumber As String
                        Dim TransactionTime As String

                        If clsDefaultConfiguration.PayFromInnoviti AndAlso clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) AndAlso aplicableWithInnvo = True Then
                            Dim clsInnovitiList = payment.resonseInnoviti

                            If clsInnovitiList IsNot Nothing Then
                                Dim row = payment.ReciptTotalAmount.Tables(0).Select("RecieptTypeCode='CreditCard'")
                                If row.Count > 0 Then
                                    For Each kvp As KeyValuePair(Of String, String) In clsInnovitiList
                                        If kvp.Key = "RetrievalReferenceNumber" Then
                                            RetrievalReferenceNumber = kvp.Value.ToUpper
                                        ElseIf kvp.Key = "TransactionTime" Then
                                            TransactionTime = kvp.Value.ToUpper
                                        ElseIf kvp.Key = "CardNumber" Then
                                            _innovitiCardNo = kvp.Value.ToUpper
                                        End If
                                    Next
                                End If
                            End If
                        End If
                        'added by khusrao adil on 26-07-2018 for innviti with card functionality point _ natural client
                        Dim InnovitiPaymentEnable As Boolean = False
                        If dtInnvoiti.Rows.Count > 0 Then
                            InnovitiPaymentEnable = True
                        End If
                        Dim objCM As New clsCashMemo()
                        Dim docno As String = objCM.getDocumentNo("SalesInvoice", clsAdmin.SiteCode, "FO_DOC")
                        'BillNo = GenDocNo("SI" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                        ''GST changes by ketan add sitecode 3 digit in billno 
                        ' BillNo = GenDocNo("SI" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                        BillNo = GenDocNo("S" & clsAdmin.TerminalID.Substring(clsAdmin.TerminalID.Trim.Length - 2, 2) & clsAdmin.SiteCode.Substring(clsAdmin.SiteCode.Trim.Length - 3, 3) & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)
                        'End If
                        Dim trans = PrepareData(payment.ReciptTotalAmount)
                        ' If objCreditSales.UpdateCredit(trans, clsAdmin.DayOpenDate, ) Then
                        If objCreditSales.UpdateCredit(trans, clsAdmin.DayOpenDate, Billno:=BillNo, SiteCode:=clsAdmin.SiteCode, finyear:=clsAdmin.Financialyear, _
                                                       DocumentType:="CSA", UserId:=clsAdmin.UserCode, RetrievalReferenceNumber:=RetrievalReferenceNumber, _
                                                       TransactionTime:=TransactionTime, PayByInnovitii:=clsDefaultConfiguration.PayFromInnoviti, _
                                                       InnovatiiForTerminals:=clsDefaultConfiguration.InnovitiForTerminals, TerminalId:=clsAdmin.TerminalID,
                                                        dtInnoviti:=dtInnvoiti, _
                                                        InnovitiPaymentEnable:=InnovitiPaymentEnable) Then
                            Dim BillDate = clsAdmin.DayOpenDate
                            Dim dsVoucher As DataSet
                            dsVoucher = objCM.GetVoucherFloatData(BillNumber, BillDate)
                            '-----------Saving Data of Float Return to Petty cash Entry
                            'Code is added by irfan for Csa screen on 13/03/2018
                            Dim objcls As New clsCommon
                            Dim code As String
                            Dim bill As String = BillNumber
                            code = objcls.GetTranTypeCode(bill)
                            If FloatAmtReturned <> 0 Then
                                objCreditSales.SaveFloatAmtReturnedData(FloatAmtReturned, DeliveryPerson, BillNumber, InvoicBillNumber, BillDate, clsAdmin.SiteCode, clsAdmin.UserCode, clsAdmin.Financialyear, dsVoucher)
                            End If
                            If ObjclsCommon.UpdateDate(code, bill) Then
                                If clsDefaultConfiguration.KOTAndBillGeneration = True Then
                                    PrintKOT(True)
                                    CmdPrint_Click(sender, EventArgs.Empty)
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

            Dim ServerDate = ObjclsCommon.GetCurrentDate()
            Dim ReceiptList As New List(Of SalesInvoice)
            For Each dr As DataRow In ds.Tables(0).Rows
                Dim salesinvoice As New SalesInvoice()
                salesinvoice.BillNO = BillNo
                salesinvoice.AmountTendered = dr("Amount")
                salesinvoice.BankNO = dr("BankAccNO").ToString()
                Dim aplicableWithInnvo As Boolean = False
                'added by khusrao adil on 25-07-2018
                If AllowInnovitiWithOtherTender = True And AllowInnovitiPayment = True Then
                    aplicableWithInnvo = True
                ElseIf AllowInnovitiWithOtherTender = False And AllowInnovitiPayment = True Then
                    aplicableWithInnvo = True
                ElseIf AllowInnovitiWithOtherTender = False And AllowInnovitiPayment = False Then
                    aplicableWithInnvo = False
                End If
                If clsDefaultConfiguration.PayFromInnoviti = True AndAlso clsDefaultConfiguration.InnovitiForTerminals.Contains(clsAdmin.TerminalID) AndAlso aplicableWithInnvo = True Then
                    If dtInnvoiti.Rows.Count > 0 Then
                        Dim _InnoCardNumber = dtInnvoiti.Select("TenderSequenceLineNo='" & dr("SRNo").ToString() & "'")
                        If _InnoCardNumber.Length > 0 Then
                            salesinvoice.CardNO = _innovitiCardNo
                        Else
                            salesinvoice.CardNO = dr("Number").ToString()
                        End If
                    Else
                        salesinvoice.CardNO = dr("Number").ToString()
                    End If
                Else
                    salesinvoice.CardNO = dr("Number").ToString()
                    _innovitiCardNo = ""
                End If
                'salesinvoice.CardNO = dr("Number").ToString()
                salesinvoice.TerminalID = clsAdmin.TerminalID
                salesinvoice.AmountInCurrency = If(dr("AmountInCurrency").ToString() = "", 0.0, CDbl(dr("AmountInCurrency").ToString()))
                salesinvoice.CurrencyCode = dr("CurrencyCode").ToString()
                salesinvoice.DocType = If(grdCreditSales.Rows(grdCreditSales.RowSel)("DocType").ToString().ToLower() = "Cash memo".ToLower(), "CM", "SO")
                salesinvoice.ExchangeRate = If(dr("ExchangeRate").ToString() = "", 0.0, CDec(dr("ExchangeRate").ToString()))
                salesinvoice.FinYear = clsAdmin.Financialyear
                salesinvoice.InvoiceNumber = grdCreditSales.Rows(grdCreditSales.RowSel)("BillNo").ToString()
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
                salesinvoice.RefBillInvNumber = grdCreditSales.Rows(grdCreditSales.RowSel)("InvoiceNo").ToString() 'new added
                ReceiptList.Add(salesinvoice)
            Next
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
            If clsDefaultConfiguration.PrintFormatNo = "3" OrElse clsDefaultConfiguration.PrintFormatNo = "4" OrElse clsDefaultConfiguration.PrintFormatNo = "5" OrElse clsDefaultConfiguration.PrintFormatNo = "6" OrElse clsDefaultConfiguration.PrintFormatNo = "7" OrElse clsDefaultConfiguration.PrintFormatNo = "8" OrElse clsDefaultConfiguration.PrintFormatNo = "9" OrElse clsDefaultConfiguration.PrintFormatNo = "11" Then
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
                If (grdCreditSales.Rows(grdCreditSales.Row)("DocType").ToString().ToLower() = "sales order".ToLower()) Then
                    Dim SalesPersonName As String = String.Empty
                    dtSaleItems = New DataTable
                    dtSaleItems = objPrint.GetSaleItems(clsAdmin.SiteCode, clsAdmin.Financialyear, grdCreditSales.Rows(grdCreditSales.Row)("BillNo").ToString(), "SalesOrder", grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo").ToString())
                    If clsDefaultConfiguration.IsNewSalesOrder = True Then
                        Dim DounmentNo As String = dtSaleItems.Rows(0)("SaleOrderNumber")
                        SOprint(DounmentNo)
                    Else
                        'dtSaleItems.Columns("rowindex").Caption = "BillLineNo"
                        If Not IsDBNull(dtSaleItems.Rows(0)("SalesPersonFullName")) Then SalesPersonName = dtSaleItems.Rows(0)("SalesPersonFullName")
                        dtPayment = New DataTable
                        dtPayment = objPrint.GetPaymentDetails(clsAdmin.SiteCode, clsAdmin.Financialyear, grdCreditSales.Rows(grdCreditSales.Row)("BillNo").ToString(), grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo").ToString())
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
                        Dim Dstemp = objSO.GetSOBulkComboTableStruct(clsAdmin.SiteCode, IIf(grdCreditSales.Rows(grdCreditSales.Row)("BillNo").ToString() = String.Empty, 0, grdCreditSales.Rows(grdCreditSales.Row)("BillNo").ToString()))
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
                                                                                clsAdmin.UserName, grdCreditSales.Rows(grdCreditSales.Row)("BillNo").ToString(), dtCustInfo, dtSaleItems, dtPayment, vCustRef, grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo").ToString(),
                                                                                String.Empty, Nothing, Nothing, dtPrinterInfo, statusValue, dsOtherCharges, strReturnReason:=vSalesOrderRemark,
                                                                                ShowFullName:=clsDefaultConfiguration.PrintItemFullName, SalesOrderCreationDate:=vSalesOrderCreationDate,
                                                                                dtSOBulkComboHDRPrint:=DtSoBulkComboHdr, dtSOBulkComboDtlPrint:=DtSoBulkComboDtl, SalesOrderDeliveryDate:=vSalesOrderDeliveryDate, strSalesPerson:=SalesPersonName, strInvoiceTo:=vInvoiceTo)
                        'objPrint.UpdateReprintStatus(clsAdmin.SiteCode, clsAdmin.Financialyear, grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo"), PrintTransType, ReprintReason )
                    End If
                ElseIf (grdCreditSales.Rows(grdCreditSales.Row)("DocType").ToString().ToLower() = "Cash memo".ToLower()) Then
                    Dim clsDefault As New clsDefaultConfiguration("CMS")
                    clsDefault.GetDefaultSettings()

                    Dim objCMPrint As New clsCashMemoPrint(grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo").ToString(), False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving) '0000413
                    objCMPrint.DisplayArticleFullName = clsDefaultConfiguration.PrintItemFullName

                    objCMPrint.AllowDecimalQty = clsDefaultConfiguration.AllowDecimalQty
                    objCMPrint.WeightScaleEnabled = clsDefaultConfiguration.WeightScaleEnabled
                    objCMPrint.KOTBillPrintingRequired = False
                    objCMPrint.TokenNoRequiredInKOT = clsDefaultConfiguration.TokenNoRequiredInKOT
                    objCMPrint.CashMemoResetonDayClose = clsDefaultConfiguration.CashMemoResetonDayClose
                    objCMPrint.AllowBillingOnlyAfterSelectionOfSalesType = clsDefaultConfiguration.AllowBillingOnlyAfterSelectionOfSalesType
                    objCMPrint.IsRoundRequired = clsDefaultConfiguration.RoundOffRequired
                    objCMPrint.PrintFormatNo = clsDefaultConfiguration.PrintFormatNo
                    objCMPrint.IsDeliveryCopyRequired = clsDefaultConfiguration.IsDeliveryCopyRequired
                    If clsDefaultConfiguration.KOTAndBillGeneration = True Then
                        objCMPrint.PrintCouponNumberForKOT = True
                    Else
                        objCMPrint.PrintCouponNumberForKOT = False
                    End If
                    Dim ErrorMsg As String = ""
                    '--- Changes by mahesh before reprint updated reprint count (same as Cash Memo)
                    'objPrint.UpdateReprintStatus(clsAdmin.SiteCode, clsAdmin.Financialyear,grdCreditSales.Rows(row)("InvoiceNo").ToString(), PrintTransType, ReprintReason)

                    If (clsDefaultConfiguration.TemplatePrintingAllowed) Then
                        objCMPrint.PrintTemplateCashMemoBillDetails(grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo").ToString(), clsAdmin.SiteCode, clsAdmin.CurrencyCode, String.Empty, Nothing, True, ReprintReason)
                    Else
                        If clsDefaultConfiguration.KOTAndBillGeneration = True Then
                            Dim _printKOT As Boolean = True
                            'Dim strText As String = (sender as TextBox).Text
                            If sender Is Nothing Then
                                _printKOT = True
                            ElseIf UCase(sender.name) = UCase("cmdPrint") = True Then
                                _printKOT = False
                            End If
                            'objCMPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "", "", "", ErrorMsg, "", "0", "0", False, clsDefaultConfiguration.IsSavoy, False, False, False, _printKOT) '0000413
                            ' objCMPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "", "", "", ErrorMsg, "", " 0", "0", False, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=IsFinalReceipt, KOTAndBillGeneration:=_printKOT, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath) '0000413
                            'objCMPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "", "", "", ErrorMsg, "", " 0", "0", False, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=IsFinalReceipt, KOTAndBillGeneration:=_printKOT, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsKOTBillGenerationFromPRintButton:=clsDefaultConfiguration.KOTAndBillGeneration, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6) '0000413
                            objCMPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "", "", "", ErrorMsg, "", " 0", "0", False, clsDefaultConfiguration.IsSavoy, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=IsFinalReceipt, KOTAndBillGeneration:=_printKOT, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsKOTBillGenerationFromPRintButton:=clsDefaultConfiguration.KOTAndBillGeneration, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, JKPrintFormatEnable:=clsDefaultConfiguration.JKPrintFormatEnable) '0000413
                        Else
                            ' objCMPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "", "", "", ErrorMsg) '0000413
                            '  objCMPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "", "", "", ErrorMsg, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsCounterCopyKot:=True, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6) '0000413
                            'modified by khusrao adil on 8-12-2017 for jk sprint 32
                            'JKPrintFormatEnable flag added
                            objCMPrint.CashMemoPrint(clsAdmin.DayOpenDate, clsAdmin.LangCode, clsAdmin.CurrencyCode, clsAdmin.SiteCode, "CM", "", "", "", ErrorMsg, IsArticleWiseKot:=IsArticleWiseKot, IsCounterCopy:=IsCounterCopy, IsFinalReceipt:=IsFinalReceipt, ClientName:=clsDefaultConfiguration.ClientName, DayCloseReportPath:=clsDefaultConfiguration.DayCloseReportPath, IsCounterCopyKot:=True, IsRateVisible:=clsDefaultConfiguration.IsRatevisibleInPrintFormat6, JKPrintFormatEnable:=clsDefaultConfiguration.JKPrintFormatEnable) '0000413
                        End If
                    End If
                    If ErrorMsg <> String.Empty Then
                        ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
                    End If
                End If
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
                Dim Billno As String = grdCreditSales.Rows(grdCreditSales.Row)("BillNo")
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
                            Dim objcls As New clsCashMemo
                            Dim dsVoucher = objcls.GetVoucherFloatData(Billno, BillDate)
                            If dsVoucher.Tables.Count > 0 Then
                                If dsVoucher.Tables(0) IsNot Nothing AndAlso dsVoucher.Tables(0).Rows.Count = 0 Then
                                    If objCreditSales.SaveFloatAmountData(FloatAmt, DeliveryPerson, Billno, BillDate, clsAdmin.SiteCode, clsAdmin.UserCode, clsAdmin.Financialyear, dsVoucher) = False Then
                                    End If
                                Else
                                    objCreditSales.UpdateFloatAmtDispatchData(FloatAmt, DeliveryPerson, Billno, BillDate, clsAdmin.SiteCode, dsFloatAmt)
                                End If
                            End If
                        End If
                        If DeliveryPartner <> String.Empty Then
                            objCreditSales.UpdateDeliveryPartnerData(DeliveryPartner, Billno, BillDate, clsAdmin.SiteCode)
                        End If
                        objCreditSales.UpdateDispatchTimeData(DeliveryPerson, Billno, clsAdmin.SiteCode)
                        grdCreditSales.Rows(grdCreditSales.Row)("DeliveryPerson") = DeliveryPerson
                        grdCreditSales.Rows(grdCreditSales.Row)("DeliveryPartner") = DeliveryPartner
                        grdCreditSales.Rows(grdCreditSales.Row)("SalesPerson") = DeliveryPerson
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
            ''added by khusrao adil on 16-06-2017 for natural Juhu CR
            ''authorisation req for payment
            'If clsDefaultConfiguration.AuthReqForCreditSalesAdjustmentOnCSA = True Then
            '    If AuthorizationRequiredAndValid() = True Then
            '    Else
            '        Exit Sub
            '    End If
            'End If
            Dim objWriteOffCreditSales As New frmWriteOffCreditSales()
            If grdCreditSales.Row > 0 Then
                Dim objPaymentCreditSales As New frmPaymentCreditSales()
                objWriteOffCreditSales.BillAmount = grdCreditSales.Rows(grdCreditSales.Row)("BillAmount")
                objWriteOffCreditSales.BalAmount = grdCreditSales.Rows(grdCreditSales.Row)("BalanceAmount")
                objWriteOffCreditSales.BillNumber = grdCreditSales.Rows(grdCreditSales.Row)("BillNo")
                objWriteOffCreditSales.InvoiceNumber = grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo")
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
        Me.TableLayoutPanel1.RowStyles(0).SizeType = SizeType.Absolute
        Me.TableLayoutPanel1.RowStyles(0).Height = 35
        Me.TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
        Me.TableLayoutPanel1.RowStyles(1).Height = 550
        ' Me.TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Absolute
        Me.TableLayoutPanel1.RowStyles(2).Height = 8
        ' Me.TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Absolute
        Me.TableLayoutPanel1.RowStyles(3).Height = 30

        ' Me.TableLayoutPanel1.SetRow(Me.grdCreditSales, 2)

        Me.TableLayoutPanel1.SetRowSpan(Me.TableLayoutPanel2, 2)
        'lblsearch
        '
        Me.lblsearch.BackColor = Color.Transparent
        Me.lblsearch.BorderStyle = BorderStyle.None
        Me.lblsearch.ForeColor = Color.White
        Me.lblsearch.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblsearch.Text = Me.lblsearch.Text.ToUpper()

        'lblDelivery
        '
        Me.lblDeliveryId.BackColor = Color.Transparent
        Me.lblDeliveryId.BorderStyle = BorderStyle.None
        Me.lblDeliveryId.ForeColor = Color.White
        Me.lblDeliveryId.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblDeliveryId.Text = Me.lblDeliveryId.Text.ToUpper()

        'dgGridReciept
        '
        'Me.grdCreditSales.Dock = DockStyle.Fill
        Me.grdCreditSales.MaximumSize = New Size(1364, 600)
        Me.grdCreditSales.Size = New System.Drawing.Size(1364, 600)
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
        Me.TableLayoutPanel2.Dock = DockStyle.None
        Me.TableLayoutPanel2.ColumnCount = 11
        Me.TableLayoutPanel2.ColumnStyles().Add(New ColumnStyle(SizeType.Absolute, 50))
        Me.TableLayoutPanel2.ColumnStyles().Add(New ColumnStyle(SizeType.Absolute, 50))
        Me.TableLayoutPanel2.ColumnStyles().Add(New ColumnStyle(SizeType.Absolute, 50))
        Me.TableLayoutPanel2.ColumnStyles().Add(New ColumnStyle(SizeType.Absolute, 50))
        'Me.TableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        Me.TableLayoutPanel2.RowStyles(0).SizeType = SizeType.Absolute

        Me.TableLayoutPanel2.BackColor = Color.FromArgb(76, 76, 76)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(5, 457)
        Me.TableLayoutPanel2.MaximumSize = New Size(2000, 85)
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(2000, 85)

        'Me.TableLayoutPanel2.ColumnStyles(0).Width = 500
        'Me.TableLayoutPanel2.ColumnStyles(1).Width = 400
        'TableLayoutPanel1.Controls.Add(pnlButton)
        'pnlButton.BringToFront()
        'cmdDispatch
        '
        Me.cmdDispatch.Dock = DockStyle.Fill
        Me.cmdDispatch.Location = New System.Drawing.Point(5, 5)
        Me.cmdDispatch.MaximumSize = New Size(0, 50)
        Me.cmdDispatch.Size = New System.Drawing.Size(100, 80)
        Me.cmdDispatch.BringToFront()
        Me.cmdDispatch.Font = New System.Drawing.Font("Verdana", 9.0!)
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
        Me.cmdPrint.Font = New System.Drawing.Font("Verdana", 9.0!)
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
        Me.cmdPayments.Font = New System.Drawing.Font("Verdana", 9.0!)
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
        Me.cmdCash.Font = New System.Drawing.Font("Verdana", 9.0!)
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
        Me.cmdCard.Font = New System.Drawing.Font("Verdana", 9.0!)
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
        Me.cmdCheque.Font = New System.Drawing.Font("Verdana", 9.0!)
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
        Me.cmdKOTReprint.Font = New System.Drawing.Font("Verdana", 9.0!)
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
        Me.cmdWriteOff.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.cmdWriteOff.Image = Global.Spectrum.My.Resources.Resources.WriteOffnew
        Me.cmdWriteOff.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdWriteOff.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdWriteOff.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.cmdWriteOff.TextImageRelation = TextImageRelation.ImageAboveText

        'cmdRefreshGrid
        '
        '  Me.cmdRefreshGrid.Dock = DockStyle.Right
        ' Me.cmdRefreshGrid.Location = New System.Drawing.Point(500, 4)
        Me.cmdRefreshGrid.MaximumSize = New Size(25, 25)
        Me.cmdRefreshGrid.Size = New System.Drawing.Size(25, 25)
        Me.cmdRefreshGrid.BringToFront()
        Me.cmdRefreshGrid.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.cmdRefreshGrid.Image = Global.Spectrum.My.Resources.Resources.Refresh_CSA2
        Me.cmdRefreshGrid.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        'Me.cmdRefreshGrid.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdRefreshGrid.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        '   Me.cmdRefreshGrid.TextImageRelation = TextImageRelation.ImageAboveText
        Me.cmdRefreshGrid.Text = ""
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
        dtCustmInfo = objCustm.GetCustomerInformation("CLP", clsAdmin.SiteCode, clsAdmin.CLPProgram, CUSTNo, CustFormat:=clsDefaultConfiguration.DetailedCustomerCreationformat, IsNewSalesOrder:=clsDefaultConfiguration.IsNewSalesOrder)
        dsPackagingDelivery = objPCSO.SetSalesOrderDeliveryInSO(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
        dtPackagingPrintBox = ObjclsCommon.GetPackagingBox(clsAdmin.SiteCode, 2)
        dsInv = objPCSO.SetInvoiceInSOCancel(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
        ProgramID = objPCSO.GetCLPProgramID(clsAdmin.SiteCode)
        dtOrderAddresses = objcomm.GetSOAddresses(CUSTNo, ProgramID, True)
        dtStrPrint = objPCSO.GetSalesOrderSTRPrint(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))
        _pickupHistory = objPCSO.GetSalesOrderPickupHistory(clsAdmin.SiteCode, IIf(DocumentNo = String.Empty, 0, DocumentNo))

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

        BarCodestring = ImageToBase64(DocumentNo)
        'GenerateSoDeliveryPrint()
        GenerateSOPrint()
        GenerateOrderPreparationPrint()
    End Sub
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
    Private Function GenerateOrderPreparationPrint() As Boolean
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
            Dim DataSource1 As New ReportDataSource("dsSalesPreparationDeliveryDetails", dtDeliveryDetails)
            Dim DataSource2 As New ReportDataSource("dsSalesPreparationRemarks", dtRemark)
            Dim DataSource3 As New ReportDataSource("dsSalesPreparationOrderDetails", dtOrderComboDetails)
            Dim DataSource4 As New ReportDataSource("dsSalesPreparationSTRDetails", dtStrDetails)
            Dim DataSource5 As New ReportDataSource("SdBalToPay", dtPaymentDetails1)
            Dim DataSource6 As New ReportDataSource("Ds_salesOrderPrintAddress", dtAddress)

            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            reportViewer2.LocalReport.DataSources.Add(DataSource3)
            reportViewer2.LocalReport.DataSources.Add(DataSource4)
            reportViewer2.LocalReport.DataSources.Add(DataSource5)
            reportViewer2.LocalReport.DataSources.Add(DataSource6)
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            path = clsDefaultConfiguration.DayCloseReportPath & "\SOPreparationPrint_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
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
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\SalesOrderPrint.rdl")
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

            reportViewer2.LocalReport.DataSources.Add(DataSource)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            reportViewer2.LocalReport.DataSources.Add(DataSource3)
            reportViewer2.LocalReport.DataSources.Add(DataSource4)
            reportViewer2.LocalReport.DataSources.Add(DataSource5)
            reportViewer2.LocalReport.DataSources.Add(DataSource6)
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            path = clsDefaultConfiguration.DayCloseReportPath & "\SOInvoice_" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
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
            If clsDefaultConfiguration.PrintFormatNo = "1" OrElse clsDefaultConfiguration.PrintFormatNo = "3" OrElse clsDefaultConfiguration.PrintFormatNo = "4" OrElse clsDefaultConfiguration.PrintFormatNo = "5" OrElse clsDefaultConfiguration.PrintFormatNo = "11" Then
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

                Dim _KOTReprintReason As String = ""
                Dim _billNo As String = grdCreditSales.Rows(grdCreditSales.Row)("InvoiceNo").ToString()
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
                    'objPrint.CashMemoKOTPrintBasedOnPrintFormatNo("CMS", ErrorMsg, dtView, DtUnique, Dtcombo, "", clsAdmin.DayOpenDate)
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
        If clsDefaultConfiguration.EvasPizzaChanges Then
            txtDeliveryId.Focus()
        End If
    End Sub

    Private Sub txtDeliveryId_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDeliveryId.KeyDown
        If e.KeyCode = Keys.Enter AndAlso clsDefaultConfiguration.EvasPizzaChanges Then
            Dim strBillNo As String = ""
            Dim strNewValue As String = ""
            Dim dtSearchData As New DataTable
            If txtDeliveryId.Text = "" Then
                Exit Sub
            Else
                strNewValue = txtDeliveryId.Text.Trim
                strBillNo = strNewValue
            End If
            'Dim drData() = dtCreditSales.Select("BillNo='" & txtFilterCreditSales.Text & "'")
            Dim drData() = dtCreditSales.Select("BillNo='" & strBillNo & "'")
            If drData.Count > 0 Then
                Dim CrnDeliveryPersonId As String
                Dim ScannedDeliveryPerson As String
                'ScannedDeliveryPerson = txtDeliveryId.Text
                ScannedDeliveryPerson = ScannedDeliveryPersonHold
                Dim obj As New frmNHomeDelivery
                'obj.IsDispatch = True
                If grdCreditSales.Row > 0 Then
                    Dim Billno As String = strBillNo
                    Dim dtHD As New DataTable
                    dtHD = objCreditSales.GetHomeDeliveryData(strBillNo, clsAdmin.SiteCode)
                    If Not dtHD Is Nothing AndAlso dtHD.Rows.Count > 0 Then
                        CrnDeliveryPersonId = IIf(dtHD.Rows(0)("DeliveryPersonId") Is DBNull.Value, "", dtHD.Rows(0)("DeliveryPersonId"))
                    End If
                    'If ScannedDeliveryPerson.ToUpper <> CrnDeliveryPersonId.ToUpper Then
                    If drData(0)("Dispatch") = True Then
                        objCreditSales.UpdateDispatchTimeData(ScannedDeliveryPerson, strBillNo, clsAdmin.SiteCode, True)
                    Else
                        objCreditSales.UpdateDispatchTimeData(ScannedDeliveryPerson, strBillNo, clsAdmin.SiteCode)
                    End If
                    'End If
                    frmNCreditSales_Load(Nothing, Nothing)
                    'ScannedDeliveryPersonHold = ""
                End If
            Else
                If ScannedDeliveryPersonHold <> txtDeliveryId.Text.Trim Then
                    ScannedDeliveryPersonHold = txtDeliveryId.Text.Trim
                Else
                    ScannedDeliveryPersonOldHold = ScannedDeliveryPersonHold
                End If
                '  ScannedDeliveryPersonHold = txtDeliveryId.Text
                txtDeliveryId.Text = String.Empty
                Exit Sub
            End If
            'txtFilterCreditSales.Focus()
        End If
    End Sub

    Private Sub txtDeliveryId_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtDeliveryId.TextChanged
        'Try
        '    Call FilterCreditSales(clsDefaultConfiguration.EvasPizzaChanges)
        '    'txtFilterCreditSales.Select()
        '    txtDeliveryId.Select()
        'Catch ex As Exception
        '    ShowMessage(ex.Message, getValueByKey("CLAE05"))
        '    LogException(ex)
        'End Try
    End Sub
End Class