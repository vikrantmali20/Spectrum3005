Imports SpectrumBL
Imports SpectrumBL.clsAcceptPayment
Imports SpectrumPrint
Imports SpectrumCommon

Public Class frmWriteOffCreditSales
#Region "Global Variables"
    Dim dtCreditWriteOff As DataTable
    Dim ObjclsCommon As New clsCommon
    Dim objCreditSales As New ClsCreditSale
    Private WriteOffNo As String = String.Empty
#End Region
#Region "Properties"
    Private _billAmount As Double
    Public Property BillAmount() As Double
        Get
            Return _billAmount
        End Get
        Set(ByVal value As Double)
            _billAmount = value
        End Set
    End Property
    Private _balAmount As Double
    Public Property BalAmount() As Double
        Get
            Return _balAmount
        End Get
        Set(ByVal value As Double)
            _balAmount = value
        End Set
    End Property
    Private _billNumber As String
    Public Property BillNumber() As String
        Get
            Return _billNumber
        End Get
        Set(ByVal value As String)
            _billNumber = value
        End Set
    End Property

    Private _invoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return _invoiceNumber
        End Get
        Set(ByVal value As String)
            _invoiceNumber = value
        End Set
    End Property
    Private _TranTypeCode As String
    Public Property TranTypeCode() As String
        Get
            Return _TranTypeCode
        End Get
        Set(ByVal value As String)
            _TranTypeCode = value
        End Set
    End Property
#End Region
#Region "Class Events"

    ''' <summary>
    ''' Fetching the BillAmount and Balance Amt into Textbox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmWriteOffCreditSales_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim objPaymentCreditSales As New frmPaymentCreditSales()
            Dim AmtPaidValue As Double
            lblBalAmtValue.Text = FormatNumber(BalAmount, 0, True, True, True)
            lblBillAmtValue.Text = FormatNumber(BillAmount, 0, True, True, True)
            AmtPaidValue = CDbl(BillAmount) - CDbl(BalAmount)
            lblAmtPaidValue.Text = FormatNumber(AmtPaidValue, 0, True, True, True)
            txtWriteOffAmt.Text = BalAmount
            txtWriteOffAmt.Select()
            txtWriteOffAmt.Focus()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    '''Saving Data into Database and checking validation for Textbox.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CmdOk_Click(sender As Object, e As EventArgs) Handles CmdOk.Click
        Try
            If Not (CheckInteger(txtWriteOffAmt.Text)) Then
                ' ShowMessage("Enter Valid Amount")
                ShowMessage(getValueByKey("ACP002"), "ACP002 - " & getValueByKey("CLAE04"))
                Exit Sub
            ElseIf CDbl(txtWriteOffAmt.Text) <> CDbl(lblBalAmtValue.Text) Then
                ShowMessage(getValueByKey("Crs026"), "Crs026-" & getValueByKey("CLAE04"))
                ' ShowMessage("Write Off amount must match the amount due", "" & getValueByKey("CLAE04"))
                Exit Sub
            End If
            If MsgBox(getValueByKey("Crs027"), MsgBoxStyle.YesNo, "CM036 - " & getValueByKey("CLAE04")) = MsgBoxResult.No Then
                ' ShowMessage("Are you sure you want to write off the due amount?", "" & getValueByKey("CLAE04"))
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
                Exit Sub
            End If
            dtCreditWriteOff = AddWriteOffDataIntoDataTable(txtWriteOffAmt.Text, txtRemark.Text)
            If objCreditSales.UpdateCreditSaleWriteOff(dtCreditWriteOff) Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show(getValueByKey("Crs009"))
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Close the Writeoff Screen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CmdCancel_Click(sender As Object, e As EventArgs) Handles CmdCancel.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        Catch ex As Exception
            ShowMessage(False, getValueByKey("CLAE05"))
            'Error
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' writeoff TexBox by pressing enter, saves the data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtWriteOffAmt_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtWriteOffAmt.PreviewKeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                CmdOk_Click(CmdOk, New System.EventArgs)
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
#End Region
#Region "Methods and Functions"
    ''' <summary>
    ''' Function for Adding creditsalewriteoff data into datatable 
    ''' </summary>
    ''' <param name="writeoffamt"></param>
    ''' <param name="remark"></param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>

    Private Function AddWriteOffDataIntoDataTable(ByVal writeoffamt As Double, ByVal remark As String) As DataTable
        Try
            If clsDefaultConfiguration.IsNewCreditSale Then
                Dim objCM As New clsCashMemo()
                Dim ServerDate = ObjclsCommon.GetCurrentDate()
                Dim objType = "FO_DOC"
                Dim docno As String = objCM.getDocumentNo("CreditSaleWriteOff", clsAdmin.SiteCode, objType)
                WriteOffNo = GenDocNo("WO" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)

                Dim dtCurrency = New clsAcceptPayment().LoadCurrency(clsAdmin.SiteCode)
                Dim currencySymbol As String = IIf((dtCurrency IsNot Nothing AndAlso dtCurrency.Rows.Count > 0), dtCurrency.Rows(0)("CurrencySymbol").ToString(), "")
                dtCreditWriteOff = objCreditSales.GetBillWriteOffDtls()
                Dim DtCreditDtl As DataTable = objCreditSales.SattleCreditSaleUsingSingleTender("WriteOff", BillNumber, writeoffamt)     'vipin 11.10.2017
                If Not DtCreditDtl Is Nothing Then
                    If DtCreditDtl.Rows.Count > 0 Then
                        For Each DrCredtDtl In DtCreditDtl.Rows
                            Dim drWriteOff As DataRow = dtCreditWriteOff.NewRow()
                            drWriteOff("WriteOffNumber") = WriteOffNo
                            drWriteOff("SiteCode") = clsAdmin.SiteCode
                            drWriteOff("FinYear") = clsAdmin.Financialyear
                            drWriteOff("WriteOffDateTime") = clsAdmin.DayOpenDate
                            drWriteOff("WriteOffLineNo") = 1
                            drWriteOff("TypeCode") = TranTypeCode
                            drWriteOff("TerminalID") = clsAdmin.TerminalID
                            drWriteOff("RefBillNo") = BillNumber
                            '    drWriteOff("RefBillInvNumber") = InvoiceNumber
                            drWriteOff("RefBillInvNumber") = DrCredtDtl("SaleInvNumber").ToString  'vipin
                            drWriteOff("ExchangeRate") = 1
                            '   drWriteOff("AmountTendered") = writeoffamt
                            drWriteOff("AmountTendered") = DrCredtDtl("AmountPaid").ToString 'vipin 
                            drWriteOff("CurrencyCode") = clsAdmin.CurrencyCode
                            '  drWriteOff("AmountInCurrency") = writeoffamt
                            drWriteOff("AmountInCurrency") = DrCredtDtl("AmountPaid").ToString  'vipin
                            drWriteOff("Remark") = remark
                            drWriteOff("TenderType") = "WriteOff"  'vipin
                            drWriteOff("CreatedAt") = clsAdmin.SiteCode
                            drWriteOff("CreatedBy") = clsAdmin.UserCode
                            drWriteOff("CreatedOn") = ServerDate
                            drWriteOff("UpdatedAt") = clsAdmin.SiteCode
                            drWriteOff("UpdatedBy") = clsAdmin.UserCode
                            drWriteOff("UpdatedOn") = ServerDate
                            drWriteOff("Status") = True
                            dtCreditWriteOff.Rows.Add(drWriteOff)
                        Next
                    End If        'vipin
                End If
                Return dtCreditWriteOff
            Else
                Dim objCM As New clsCashMemo()
                Dim ServerDate = ObjclsCommon.GetCurrentDate()
                Dim objType = "FO_DOC"
                Dim docno As String = objCM.getDocumentNo("CreditSaleWriteOff", clsAdmin.SiteCode, objType)
                WriteOffNo = GenDocNo("WO" & clsAdmin.TerminalID & clsAdmin.Financialyear.Substring(clsAdmin.Financialyear.Length - 2, 2), 15, docno)

                Dim dtCurrency = New clsAcceptPayment().LoadCurrency(clsAdmin.SiteCode)
                Dim currencySymbol As String = IIf((dtCurrency IsNot Nothing AndAlso dtCurrency.Rows.Count > 0), dtCurrency.Rows(0)("CurrencySymbol").ToString(), "")
                dtCreditWriteOff = objCreditSales.GetBillWriteOffDtls()

                Dim drWriteOff As DataRow = dtCreditWriteOff.NewRow()
                drWriteOff("WriteOffNumber") = WriteOffNo
                drWriteOff("SiteCode") = clsAdmin.SiteCode
                drWriteOff("FinYear") = clsAdmin.Financialyear
                drWriteOff("WriteOffDateTime") = clsAdmin.DayOpenDate
                drWriteOff("WriteOffLineNo") = 1
                drWriteOff("TypeCode") = TranTypeCode
                drWriteOff("TerminalID") = clsAdmin.TerminalID
                drWriteOff("RefBillNo") = BillNumber
                drWriteOff("RefBillInvNumber") = InvoiceNumber
                drWriteOff("ExchangeRate") = 1
                drWriteOff("AmountTendered") = writeoffamt
                drWriteOff("CurrencyCode") = clsAdmin.CurrencyCode
                drWriteOff("AmountInCurrency") = writeoffamt
                drWriteOff("Remark") = remark
                drWriteOff("CreatedAt") = clsAdmin.SiteCode
                drWriteOff("CreatedBy") = clsAdmin.UserCode
                drWriteOff("CreatedOn") = ServerDate
                drWriteOff("UpdatedAt") = clsAdmin.SiteCode
                drWriteOff("UpdatedBy") = clsAdmin.UserCode
                drWriteOff("UpdatedOn") = ServerDate
                drWriteOff("Status") = True
                dtCreditWriteOff.Rows.Add(drWriteOff)
                Return dtCreditWriteOff
            End If
        Catch ex As Exception
            LogException(ex)
            Return Nothing
        End Try

    End Function
    Private Function Themechange()
        Me.BackColor = Color.FromArgb(134, 134, 134)

        CmdOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CmdOk.BackColor = Color.Transparent
        CmdOk.BackColor = Color.FromArgb(0, 107, 163)
        CmdOk.ForeColor = Color.FromArgb(255, 255, 255)
        CmdOk.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CmdOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CmdOk.FlatStyle = FlatStyle.Flat
        CmdOk.FlatAppearance.BorderSize = 0
        CmdOk.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        CmdOk.Size = New Size(85, 30)

        CmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CmdCancel.BackColor = Color.Transparent
        CmdCancel.BackColor = Color.FromArgb(0, 107, 163)
        CmdCancel.ForeColor = Color.FromArgb(255, 255, 255)
        CmdCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CmdCancel.FlatStyle = FlatStyle.Flat
        CmdCancel.FlatAppearance.BorderSize = 0
        CmdCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        CmdCancel.Size = New Size(85, 30)

        lblAmtPaid.BackColor = Color.Transparent
        lblAmtPaid.AutoSize = False
        lblAmtPaid.ForeColor = Color.White
        '  lblAmtPaid.Size = New Size(88, 8)
        lblAmtPaid.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblAmtPaid.BorderStyle = BorderStyle.None

        lblAmtPaidValue.BackColor = Color.Transparent
        lblAmtPaidValue.ForeColor = Color.White

        lblBalAmt.BackColor = (Color.Transparent)
        lblBalAmt.AutoSize = False
        lblBalAmt.ForeColor = Color.White
        '  lblBalAmt.Size = New Size(88, 8)
        lblBalAmt.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblBalAmt.BorderStyle = BorderStyle.None

        lblBalAmtValue.ForeColor = Color.Transparent
        lblBalAmtValue.BackColor = Color.Transparent

        lblBillAmt.BackColor = Color.Transparent
        lblBillAmt.AutoSize = False
        'lblBillAmt.Size = New Size(88, 8)
        lblBillAmt.ForeColor = Color.White
        lblBillAmt.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblBillAmt.BorderStyle = BorderStyle.None

        lblBillAmtValue.BackColor = Color.Transparent
        lblBillAmtValue.ForeColor = Color.White

        lblRemarks.BackColor = (Color.Transparent)
        lblRemarks.AutoSize = False
        lblRemarks.ForeColor = Color.White
        lblRemarks.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblRemarks.BorderStyle = BorderStyle.None

        lblWriteOffAmt.BackColor = Color.Transparent
        lblWriteOffAmt.AutoSize = False
        '  lblWriteOffAmt.Size = New Size(88, 8)
        lblWriteOffAmt.ForeColor = Color.White
        lblWriteOffAmt.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblWriteOffAmt.BorderStyle = BorderStyle.None
    End Function
#End Region

   
 
End Class