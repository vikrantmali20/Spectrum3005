Imports SpectrumPrint
Imports SpectrumBL
Imports SpectrumCommon
Imports Microsoft.Reporting.WinForms
Imports Spire.Pdf
Imports System.IO
Imports C1.Win.C1BarCode
Imports System.Data
Imports System.Data.SqlClient

Public Class frmViewMembership
    Public Property ConString() As String
        Get
            Return DataBaseConnection.ConString
        End Get
        Set(ByVal value As String)
            DataBaseConnection.ConString = value
        End Set
    End Property
    Private _IsEnquiry As Integer
    Public Property IsEnquiry As Integer
        Get
            Return _IsEnquiry
        End Get
        Set(value As Integer)
            _IsEnquiry = value
        End Set
    End Property
    Dim objclsMemb As New clsMembership
    Dim dtView As New DataTable
    Dim DtUnique As New DataTable
    Dim Dtcombo As New DataTable
    Dim dtCashMemohdrData As New DataTable
    Dim dtCashMemoBodyData As New DataTable
    Dim dtcashmemofooterData As New DataTable
    Dim dsMemb As New DataSet
    Dim ds As DataSet
    Dim EnquiryBillNo As String = ""
    Dim CustomerNo As String = ""
    Dim vIsPrintPreviewAllowed As Boolean = False
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
    Dim CheckForAcceptRejectEntery As Boolean = False
    Dim selectedOrderStatusText As String = "OPEN"
    Dim selectedChannelPartnerText As String = ""
    Dim selectedFromDateText As Date
    Dim selectedToDateText As Date
    Dim IsDeliveryDate As Boolean = False
    Dim ObjclsCommon As New clsCommon
    Dim dtMembershipdata As New DataTable
    Public Property dtCreditSales() As DataTable
        Get
            Return _dtCreditSales
        End Get
        Set(ByVal value As DataTable)
            _dtCreditSales = value
        End Set
    End Property












    Private Sub GridColumnSettings()
        Try
            grdCreditSales.AllowEditing = True
            '      If grdCreditSales.Cols.Count > 0 Then
            Dim CouponColumn As String = String.Empty
            Dim EcomerceColumns As String = String.Empty
            If clsDefaultConfiguration.IsTablet = True Then
                CouponColumn = "CouponNo,"
            End If

            If EcomerceColumns = "" Then
                EcomerceColumns = ",OrderStatus"
            End If
            ' CreatedOn
            Dim displayColumns As String = "BillNo,CardNo,PeriodID,SrNo,EAN,Amount,StartDate,EndDate,DiscountAmount,discountInPercentage,CreatedOn,CreatedBy"
            Dim columnsList = displayColumns.ToUpper().Split(",")

            For colIndex = 0 To grdCreditSales.Cols.Count - 1 Step 1
                If columnsList.Contains(grdCreditSales.Cols(colIndex).Name.ToUpper()) Then
                    grdCreditSales.Cols(colIndex).Visible = True
                Else
                    grdCreditSales.Cols(colIndex).Visible = False
                End If
                grdCreditSales.Cols(colIndex).AllowEditing = False
            Next
            'If clsDefaultConfiguration.KOTAndBillGeneration = True Then
            '    grdCreditSales.Cols("CouponNo").Caption = "Coupon No"
            ''End If
            'grdCreditSales.Cols("InvoiceNo").Caption = getValueByKey("frmNCreditSales.grdCreditSales.InvoiceNo")
            'grdCreditSales.Cols("CustomerName").Caption = getValueByKey("frmNCreditSales.grdCreditSales.CustomerName")
            'grdCreditSales.Cols("DeliveryPerson").Caption = getValueByKey("frmNCreditSales.grdCreditSales.DeliveryPerson")
            ''grdCreditSales.Cols("SalesPerson").Caption = getValueByKey("frmNCreditSales.grdCreditSales.SalesPerson")
            'grdCreditSales.Cols("OrderStatus").Caption = "Order Status"
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
            'grdCreditSales.Cols("DeliveryPartner").Caption = "Channel Partner"
            'grdCreditSales.Cols("DeliveryPartner").Visible = True
            ' grdCreditSales.Cols("discuontAmount").Caption = "Discount Amount"
            grdCreditSales.Cols("discountInPercentage").Caption = "Discount Percentage"

            grdCreditSales.AutoSizeCols()
            grdCreditSales.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns
            '     End If
            '-------------To Enable Checkbox if Dispatch Time is There and Make Enabling False
            'For row = 1 To grdCreditSales.Rows.Count - 1
            '    If Not String.IsNullOrEmpty(grdCreditSales.Rows(row)("DispatchTime").ToString) Then
            '        grdCreditSales.Rows(row)("Dispatch") = True
            '    End If
            'Next
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub frmNCreditSales_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            If IsEnquiry = 1 Then
                Me.Text = "Membership Enquiry"
                btnPayment.Visible = True
            Else
                Me.Text = "View Membership"
                btnPayment.Visible = False
            End If

            '  txtDeliveryId.ReadOnly = True

            'DtFromDate.CustomFormat = DateTimePickerFormat.Custom
            'DtFromDate.SelectedText = ""
            'DtToDate.SelectedText = ""
            Call BindMembershipData()
            GridColumnSettings()
            'Dim dChannelPartner As DataTable = FillChannelPartnerComboBox()
            'PopulateComboBox(dChannelPartner, cmbChannelPartner)
            'pC1ComboSetDisplayMember(cmbChannelPartner)
            'Dim dtOrderStatus As DataTable = FillOrderStatusCombo()
            'PopulateComboBox(dtOrderStatus, cmbOrderStatus)
            'pC1ComboSetDisplayMember(cmbOrderStatus)
            'cmbOrderStatus.SelectedIndex = 3

            'Call FillOrderStatusCombo()
            'Call SetResourcesText()
            'Dim objdefaultSO As New clsDefaultConfiguration("SalesOrder")
            'objdefaultSO.GetDefaultSettings()

            'Dim objdefaultCM As New clsDefaultConfiguration("CMS")
            'objdefaultCM.GetDefaultSettings()


            '   EnableDisabaledBottomButtons()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            BtnFilterSearch.Visible = False
            'If clsDefaultConfiguration.EvasPizzaChanges Then
            '    txtDeliveryId.Focus()
            'Else
            '    txtDeliveryId.Visible = False
            '    lblDeliveryPerson.Visible = False
            '    lblsearch.Visible = False
            '    txtFilterCreditSales.Visible = False
            'End If
            'Checking Authorization for Write Off Transaction
            'If CheckAuthorisation(clsAdmin.UserCode, "CRDSale") = False Then
            '    'cmdWriteOff.Visible = False
            '    Exit Sub
            'End If
            vIsPrintPreviewAllowed = clsDefaultConfiguration.PrintPreivewReq
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub






    'Private Sub BindCreditSales()
    '    Try
    '        ' txtFilterCreditSales.Text = String.Empty
    '        'txtDeliveryId.Text = String.Empty
    '        'dtCreditSales = objCreditSales.GetCreditSales(clsAdmin.SiteCode)
    '        '--added new proc for fetching all data without any filter ahma 21 dec 2016
    '        dtCreditSales = objCreditSales.GetHomeDelivery(clsAdmin.SiteCode)
    '        If dtCreditSales IsNot Nothing Then
    '            dtCreditSales.DefaultView.Sort = "BillTime DESC"
    '            If clsDefaultConfiguration.DoneSystemApplicable Then
    '                If clsDefaultConfiguration.ExternalOrdersTillNo = clsAdmin.TerminalID Then
    '                    Dim dr() = dtCreditSales.Select("TerminalId='" & clsDefaultConfiguration.ExternalOrdersTillNo & "'")
    '                    If dr.Count > 0 Then
    '                        dtCreditSales = dtCreditSales.Select("TerminalId='" & clsDefaultConfiguration.ExternalOrdersTillNo & "'").CopyToDataTable()
    '                        dtCreditSales.DefaultView.Sort = "BillTime DESC"
    '                    Else
    '                        dtCreditSales.Rows.Clear()
    '                    End If
    '                Else
    '                    Dim drt() = dtCreditSales.Select("TerminalId<>'" & clsDefaultConfiguration.ExternalOrdersTillNo & "'")
    '                    If drt.Count > 0 Then
    '                        dtCreditSales = dtCreditSales.Select("TerminalId<>'" & clsDefaultConfiguration.ExternalOrdersTillNo & "'").CopyToDataTable()
    '                        dtCreditSales.DefaultView.Sort = "BillTime DESC"
    '                    Else
    '                        dtCreditSales.Rows.Clear()
    '                    End If
    '                End If
    '            End If
    '            If clsDefaultConfiguration.IsTablet = True Then
    '                Dim row As DataRow
    '                For Each row In dtCreditSales.Rows
    '                    'need to set value to NewColumn column
    '                    'Dim strBillno As String = row("BillNo")
    '                    'Dim strTillno As String = row("TerminalID")
    '                    'Dim CouponNo As String = strTillno.Substring(0, 1) & strTillno.Substring(strTillno.Length - 2, 2) & strBillno.Substring(strBillno.Length - 2, 2)
    '                    ' row("CouponNo") = CouponNo   ' or set it to some other value
    '                    'row("FILTER") = row("FILTER") & CouponNo
    '                    row("FILTER") = row("FILTER") & row("CouponNo")
    '                Next
    '            End If
    '            ' added by khusrao adil for e-commerce
    '            Dim dt As DataTable = dtCreditSales
    '            If dtCreditSales.Select("orderstatus='" & selectedOrderStatusText & "'").Length <> 0 Then
    '                dt = dtCreditSales.Select("orderstatus='" & selectedOrderStatusText & "'").CopyToDataTable
    '            Else
    '                dt = dtCreditSales
    '            End If
    '            dt.DefaultView.Sort = "BillTime DESC"
    '            grdCreditSales.DataSource = dt


    '            GridColumnSettings()
    '            If dtCreditSales.Rows.Count > 0 Then
    '                grdCreditSales.Select()
    '                EnableDisableCmd(True)
    '            Else
    '                EnableDisableCmd(False)
    '            End If

    '        End If
    '        ' txtFilterCreditSales.Focus()
    '        'txtFilterCreditSales.Select()
    '    Catch ex As Exception
    '        ShowMessage(ex.Message, getValueByKey("CLAE05"))
    '        LogException(ex)
    '    End Try
    'End Sub

    Private Sub txtFilterCreditSales_TextChanged(sender As System.Object, e As System.EventArgs)
        'Try
        '    Call FilterCreditSales()
        '    txtFilterCreditSales.Select()
        'Catch ex As Exception
        '    ShowMessage(ex.Message, getValueByKey("CLAE05"))
        '    LogException(ex)
        'End Try
    End Sub

    Private Sub frmNCreditSales_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                dtCreditSales = Nothing
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
            ElseIf e.KeyCode = Keys.F1 Then

                objClsCommon.DisplayHelpFile(ParentForm, "credit-sales-adjustment.htm")
            End If

            '  If e.KeyCode = Keys.F3 AndAlso  Then CmdPrint_Click(cmdPrint, New System.EventArgs)

            'If e.KeyCode = Keys.F4 AndAlso cmdPayments.Enabled Then cmdPayments_Click(cmdPayments, New System.EventArgs)

            'If e.KeyCode = Keys.F5 AndAlso cmdCash.Enabled Then cmdCash_Click(cmdCash, New System.EventArgs)

            'If e.KeyCode = Keys.F6 AndAlso cmdCard.Enabled Then cmdCard_Click(cmdCard, New System.EventArgs)

            'If e.KeyCode = Keys.F7 AndAlso cmdCheque.Enabled Then cmdCheque_Click(cmdCheque, New System.EventArgs)
            ''Shortcut Key for WriteOff Screen
            'If e.KeyCode = Keys.F8 AndAlso cmdWriteOff.Visible Then cmdWriteOff_Click(cmdWriteOff, New System.EventArgs)

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try


    End Sub














    ''' <summary>
    ''' Dispatch Click :Getting Data of Home Delivery and Displaying HomeDelivery Form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>


    'Private Sub grdCreditSales_AfterEdit(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs)
    '    If (grdCreditSales.Cols(e.Col).Name.ToUpper() = "DISPATCH") Then
    '        If Not String.IsNullOrEmpty(grdCreditSales.Rows(grdCreditSales.Row)("DispatchTime").ToString) Then
    '            grdCreditSales.Rows(grdCreditSales.Row)("Dispatch") = True
    '            grdCreditSales.Rows(grdCreditSales.Row).AllowEditing = False
    '        End If

    '    ElseIf (grdCreditSales.Cols(e.Col).Name.ToUpper() = "FLOATAMTRETURNED") Then
    '        Dim currentqty As Double = IIf(grdCreditSales.Item(grdCreditSales.Row, "FLOATAMTRETURNED") Is DBNull.Value, 0, grdCreditSales.Item(grdCreditSales.Row, "FLOATAMTRETURNED"))
    '        If currentqty < 0 Then
    '            ShowMessage(getValueByKey("ACP039"), "ACP039 - " & getValueByKey("CLAE04"))
    '            'Float Amount Returned cannot be less than 0
    '            grdCreditSales.Item(grdCreditSales.Row, "FLOATAMTRETURNED") = 0
    '            Exit Sub
    '        End If
    '        Dim FloatReturned As Double = IIf(grdCreditSales.Item(grdCreditSales.Row, "FLOATAMTRETURNED") Is DBNull.Value, 0, grdCreditSales.Item(grdCreditSales.Row, "FLOATAMTRETURNED"))
    '        If FloatReturned = 0 Then
    '            grdCreditSales.Item(grdCreditSales.Row, "FLOATAMTRETURNED") = 0
    '        End If
    '        If FloatReturned > 9999 Then
    '            ShowMessage(getValueByKey("ACP040"), "ACP040 - " & getValueByKey("CLAE04"))
    '            ' Float Amount Returned cannot be greater then 9999
    '            grdCreditSales.Item(grdCreditSales.Row, "FLOATAMTRETURNED") = 0
    '            e.Cancel = True
    '        End If
    '        grdCreditSales.Rows(grdCreditSales.Row).AllowEditing = True
    '    End If
    'End Sub

    ''' <summary>
    ''' New Button for Write off Screen,Fetching data of grid into Properties
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

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
        Me.TableLayoutPanel1.RowStyles(0).Height = 65
        Me.TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
        ' Me.TableLayoutPanel1.RowStyles(1).Height = 550
        ' Me.TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Absolute
        Me.TableLayoutPanel1.RowStyles(2).Height = 8
        ' Me.TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Absolute
        Me.TableLayoutPanel1.RowStyles(3).Height = 30

        ' Me.TableLayoutPanel1.SetRow(Me.grdCreditSales, 2)

        '  Me.TableLayoutPanel1.SetRowSpan(Me.TableLayoutPanel2, 2)
        'lblsearch
        ''
        'Me.lblsearch.BackColor = Color.Transparent
        'Me.lblsearch.BorderStyle = BorderStyle.None
        'Me.lblsearch.ForeColor = Color.White
        'Me.lblsearch.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        'Me.lblsearch.Text = Me.lblsearch.Text.ToUpper()

        'lblDelivery
        '
        'Me.lblChannelPartner.BackColor = Color.Transparent
        'Me.lblChannelPartner.BorderStyle = BorderStyle.None
        'Me.lblChannelPartner.ForeColor = Color.White
        'Me.lblChannelPartner.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        'Me.lblChannelPartner.Text = Me.lblChannelPartner.Text.ToUpper()

        'lblDeliveryPerson
        '
        Me.lblDeliveryPerson.BackColor = Color.Transparent
        Me.lblDeliveryPerson.BorderStyle = BorderStyle.None
        Me.lblDeliveryPerson.ForeColor = Color.White
        Me.lblDeliveryPerson.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.lblDeliveryPerson.Text = Me.lblDeliveryPerson.Text.ToUpper()

        ''lblOrderStatus
        'Me.lblOrderStatus.BackColor = Color.Transparent
        'Me.lblOrderStatus.BorderStyle = BorderStyle.None
        'Me.lblOrderStatus.ForeColor = Color.White
        'Me.lblOrderStatus.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        'Me.lblOrderStatus.Text = Me.lblOrderStatus.Text.ToUpper()

        'lblFromDeliveryDate
        Me.lblFromDeliveryDate.BackColor = Color.Transparent
        Me.lblFromDeliveryDate.BorderStyle = BorderStyle.None
        Me.lblFromDeliveryDate.ForeColor = Color.White
        Me.lblFromDeliveryDate.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.lblFromDeliveryDate.Text = Me.lblFromDeliveryDate.Text.ToUpper()

        'lblToDeliveryDate
        Me.lblToDeliveryDate.BackColor = Color.Transparent
        Me.lblToDeliveryDate.BorderStyle = BorderStyle.None
        Me.lblToDeliveryDate.ForeColor = Color.White
        Me.lblToDeliveryDate.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.lblToDeliveryDate.Text = Me.lblToDeliveryDate.Text.ToUpper()

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
        'Me.TableLayoutPanel1.ColumnStyles(0).Width = 100
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
        'Me.TableLayoutPanel2.ColumnStyles(1).Width = 400
        'TableLayoutPanel1.Controls.Add(pnlButton)
        'pnlButton.BringToFront()
        'cmdDispatch
        '
        'Me.cmdDispatch.Dock = DockStyle.Fill
        'Me.cmdDispatch.Location = New System.Drawing.Point(5, 5)
        'Me.cmdDispatch.MaximumSize = New Size(0, 50)
        'Me.cmdDispatch.Size = New System.Drawing.Size(100, 80)
        'Me.cmdDispatch.BringToFront()
        'Me.cmdDispatch.Font = New System.Drawing.Font("Verdana", 9.0!)
        'Me.cmdDispatch.Image = Global.Spectrum.My.Resources.Resources.Dispatchnew
        'Me.cmdDispatch.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        'Me.cmdDispatch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        'Me.cmdDispatch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'Me.cmdPrint.TextImageRelation = TextImageRelation.ImageAboveText

        ''cmdAccept
        ''
        'Me.cmdAccept.Dock = DockStyle.Fill
        'Me.cmdAccept.Location = New System.Drawing.Point(5, 5)
        'Me.cmdAccept.MaximumSize = New Size(0, 50)
        'Me.cmdAccept.Size = New System.Drawing.Size(100, 80)
        'Me.cmdAccept.BringToFront()
        'Me.cmdAccept.Font = New System.Drawing.Font("Verdana", 9.0!)
        'Me.cmdAccept.Image = Global.Spectrum.My.Resources.Resources.PrintSO1
        'Me.cmdAccept.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        'Me.cmdAccept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        'Me.cmdAccept.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'Me.cmdAccept.TextImageRelation = TextImageRelation.ImageAboveText

        ''btnRejectOrder

        'Me.btnRejectOrder.Dock = DockStyle.Fill
        'Me.btnRejectOrder.Location = New System.Drawing.Point(5, 5)
        'Me.btnRejectOrder.MaximumSize = New Size(0, 50)
        'Me.btnRejectOrder.Size = New System.Drawing.Size(100, 80)
        'Me.btnRejectOrder.BringToFront()
        'Me.btnRejectOrder.Font = New System.Drawing.Font("Verdana", 9.0!)
        'Me.btnRejectOrder.Image = Global.Spectrum.My.Resources.Resources.Cancel_Normal
        'Me.btnRejectOrder.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        'Me.btnRejectOrder.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        'Me.btnRejectOrder.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'Me.cmdPrint.TextImageRelation = TextImageRelation.ImageAboveText


        'btnRejectOrder
        '
        'Me.CmdViewOrder.Dock = DockStyle.Fill
        'Me.CmdViewOrder.Location = New System.Drawing.Point(5, 5)
        'Me.CmdViewOrder.MaximumSize = New Size(0, 50)
        'Me.CmdViewOrder.Size = New System.Drawing.Size(100, 80)
        'Me.CmdViewOrder.BringToFront()
        'Me.CmdViewOrder.Font = New System.Drawing.Font("Verdana", 9.0!)
        'Me.CmdViewOrder.Image = Global.Spectrum.My.Resources.Resources.PrintSO1
        'Me.CmdViewOrder.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        'Me.CmdViewOrder.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        'Me.CmdViewOrder.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'Me.CmdViewOrder.TextImageRelation = TextImageRelation.ImageAboveText

        'cmdPrint
        '
        'Me.cmdPrint.Dock = DockStyle.Fill
        'Me.cmdPrint.Location = New System.Drawing.Point(5, 5)
        'Me.cmdPrint.MaximumSize = New Size(0, 50)
        'Me.cmdPrint.Size = New System.Drawing.Size(100, 80)
        'Me.cmdPrint.BringToFront()
        'Me.cmdPrint.Font = New System.Drawing.Font("Verdana", 9.0!)
        'Me.cmdPrint.Image = Global.Spectrum.My.Resources.Resources.PrintSO1
        'Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        'Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        'Me.cmdPrint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'Me.cmdPrint.TextImageRelation = TextImageRelation.ImageAboveText


        'cmdPayments
        '
        'Me.cmdPayments.Dock = DockStyle.Fill
        'Me.cmdPayments.Location = New System.Drawing.Point(5, 5)
        'Me.cmdPayments.MaximumSize = New Size(0, 50)
        'Me.cmdPayments.Size = New System.Drawing.Size(100, 80)
        'Me.cmdPayments.BringToFront()
        'Me.cmdPayments.Font = New System.Drawing.Font("Verdana", 9.0!)
        'Me.cmdPayments.Image = Global.Spectrum.My.Resources.Resources.payment_Normal
        'Me.cmdPayments.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        'Me.cmdPayments.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        'Me.cmdPayments.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'Me.cmdPayments.TextImageRelation = TextImageRelation.ImageAboveText


        ''cmdCash
        ''
        'Me.cmdCash.Dock = DockStyle.Fill
        'Me.cmdCash.Location = New System.Drawing.Point(5, 5)
        'Me.cmdCash.MaximumSize = New Size(0, 50)
        'Me.cmdCash.Size = New System.Drawing.Size(100, 80)
        'Me.cmdCash.BringToFront()
        'Me.cmdCash.Font = New System.Drawing.Font("Verdana", 9.0!)
        'Me.cmdCash.Image = Global.Spectrum.My.Resources.Resources.Cash_Normal
        'Me.cmdCash.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        'Me.cmdCash.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        'Me.cmdCash.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'Me.cmdCash.TextImageRelation = TextImageRelation.ImageAboveText

        ''cmdCard
        ''
        'Me.cmdCard.Dock = DockStyle.Fill
        'Me.cmdCard.Location = New System.Drawing.Point(5, 5)
        'Me.cmdCard.MaximumSize = New Size(0, 50)
        'Me.cmdCard.Size = New System.Drawing.Size(100, 80)
        'Me.cmdCard.BringToFront()
        'Me.cmdCard.Font = New System.Drawing.Font("Verdana", 9.0!)
        'Me.cmdCard.Image = Global.Spectrum.My.Resources.Resources.Card_Normal
        'Me.cmdCard.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        'Me.cmdCard.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        'Me.cmdCard.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'Me.cmdCard.TextImageRelation = TextImageRelation.ImageAboveText

        ''cmdCheque
        ''
        'Me.cmdCheque.Dock = DockStyle.Fill
        'Me.cmdCheque.Location = New System.Drawing.Point(5, 5)
        'Me.cmdCheque.MaximumSize = New Size(0, 50)
        'Me.cmdCheque.Size = New System.Drawing.Size(100, 80)
        'Me.cmdCheque.BringToFront()
        'Me.cmdCheque.Font = New System.Drawing.Font("Verdana", 9.0!)
        'Me.cmdCheque.Image = Global.Spectrum.My.Resources.Resources.Cheque_Normal
        'Me.cmdCheque.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        'Me.cmdCheque.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        'Me.cmdCheque.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'Me.cmdCheque.TextImageRelation = TextImageRelation.ImageAboveText

        ''cmdKOTReprint
        ''
        'Me.cmdKOTReprint.Dock = DockStyle.None
        'Me.cmdKOTReprint.Location = New System.Drawing.Point(0, 0)
        'Me.cmdKOTReprint.MaximumSize = New Size(109, 50)
        'Me.cmdKOTReprint.Size = New System.Drawing.Size(109, 50)
        'Me.cmdKOTReprint.BringToFront()
        'Me.cmdKOTReprint.Font = New System.Drawing.Font("Verdana", 9.0!)
        'Me.cmdKOTReprint.Image = Global.Spectrum.My.Resources.Resources.PrintKOT_CSA
        'Me.cmdKOTReprint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        'Me.cmdKOTReprint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        'Me.cmdKOTReprint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'Me.cmdKOTReprint.TextImageRelation = TextImageRelation.ImageAboveText


        ''cmdWriteOff
        ''
        'Me.cmdWriteOff.Dock = DockStyle.None
        'Me.cmdWriteOff.Location = New System.Drawing.Point(112, 0)
        'Me.cmdWriteOff.MaximumSize = New Size(0, 50)
        'Me.cmdWriteOff.Size = New System.Drawing.Size(100, 80)
        'Me.cmdWriteOff.BringToFront()
        'Me.cmdWriteOff.Font = New System.Drawing.Font("Verdana", 9.0!)
        'Me.cmdWriteOff.Image = Global.Spectrum.My.Resources.Resources.WriteOffnew
        'Me.cmdWriteOff.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        'Me.cmdWriteOff.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        'Me.cmdWriteOff.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'Me.cmdWriteOff.TextImageRelation = TextImageRelation.ImageAboveText

        'cmdRefreshGrid
        '
        '  Me.cmdRefreshGrid.Dock = DockStyle.Right
        ' Me.cmdRefreshGrid.Location = New System.Drawing.Point(500, 4)
        ' Me.cmdRefreshGrid.MaximumSize = New Size(25, 25)
        ' Me.cmdRefreshGrid.Size = New System.Drawing.Size(25, 25)
        ' Me.cmdRefreshGrid.BringToFront()
        '  Me.cmdRefreshGrid.Font = New System.Drawing.Font("Verdana", 9.0!)
        ' Me.cmdRefreshGrid.Image = Global.Spectrum.My.Resources.Resources.Refresh_CSA2
        ' Me.cmdRefreshGrid.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        'Me.cmdRefreshGrid.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        ' Me.cmdRefreshGrid.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        '   Me.cmdRefreshGrid.TextImageRelation = TextImageRelation.ImageAboveText
        '   Me.cmdRefreshGrid.Text = ""
        Dim a As New ToolTip
        a.SetToolTip(cmdRefreshGrid, "This will redirects to default search")
        a.SetToolTip(BtnFilterSearch, "Search records")
        'BtnFilterSearch
        ''Me.BtnFilterSearch.MaximumSize = New Size(25, 25)
        '  Me.BtnFilterSearch.Size = New System.Drawing.Size(35, 30)
        'Me.BtnFilterSearch.BringToFront()
        'Me.BtnFilterSearch.Font = New System.Drawing.Font("Verdana", 9.0!)
        'Me.BtnFilterSearch.Image = Global.Spectrum.My.Resources.Resources.search
        'Me.BtnFilterSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        ''Me.cmdRefreshGrid.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        'Me.BtnFilterSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        ''   Me.cmdRefreshGrid.TextImageRelation = TextImageRelation.ImageAboveText
        'Me.BtnFilterSearch.Text = ""

        '  Me.BtnFilterSearch.Text = ""
        ' Me.BtnFilterSearch.VisualStyle = C1.Win.C1Input.VisualStyle.System
        ' Me.BtnFilterSearch.Image = Nothing
        ' Me.BtnFilterSearch.BackgroundImage = Global.Spectrum.My.Resources.Resources.SearchItems
        ' Me.BtnFilterSearch.FlatStyle = FlatStyle.Flat
        '    Me.BtnFilterSearch.BackgroundImageLayout = ImageLayout.Stretch
        Return ""
    End Function




    Private Sub cmdRefreshGrid_Click(sender As System.Object, e As System.EventArgs)
        'BindCreditSales()
        ' cmbOrderStatus.SelectedIndex = 3
        ' cmbChannelPartner.Text = ""
        DtFromDate.Value = DBNull.Value
        DtToDate.Value = DBNull.Value
        EnableDisabaledBottomButtons()
        If grdCreditSales.Rows.Count > 1 Then
            grdCreditSales.Select(1, 3)
        End If
    End Sub

    'Private Sub txtDeliveryId_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDeliveryId.KeyDown
    '    If e.KeyCode = Keys.Enter AndAlso clsDefaultConfiguration.EvasPizzaChanges Then
    '        Dim strBillNo As String = ""
    '        Dim dtSearchData As New DataTable
    '        If ScannedDeliveryPersonHold = "" AndAlso ScannedDeliveryPersonHold <> txtDeliveryId.Text Then
    '            ScannedDeliveryPersonHold = txtDeliveryId.Text
    '            txtDeliveryId.Text = String.Empty
    '            Exit Sub
    '        Else
    '            strBillNo = txtDeliveryId.Text
    '        End If

    '        'Dim drData() = dtCreditSales.Select("BillNo='" & txtFilterCreditSales.Text & "'")
    '        Dim drData() = dtCreditSales.Select("BillNo='" & strBillNo & "'")
    '        If drData.Count > 0 Then
    '            Dim DeliveryDate As DateTime
    '            Dim Address1, Address2, Address3, Address4 As String
    '            Dim CrnDeliveryPersonId As String
    '            Dim MobileNo As String
    '            Dim CustomerNo As String
    '            Dim ScannedDeliveryPerson As String
    '            'ScannedDeliveryPerson = txtDeliveryId.Text
    '            ScannedDeliveryPerson = ScannedDeliveryPersonHold
    '            Dim obj As New frmNHomeDelivery
    '            'obj.IsDispatch = True
    '            If grdCreditSales.Row > 0 Then
    '                Dim Billno As String = grdCreditSales.Rows(grdCreditSales.Row)("BillNo")
    '                Dim dtHD As New DataTable
    '                dtHD = objCreditSales.GetHomeDeliveryData(Billno, clsAdmin.SiteCode)
    '                If Not dtHD Is Nothing AndAlso dtHD.Rows.Count > 0 Then
    '                    CrnDeliveryPersonId = IIf(dtHD.Rows(0)("DeliveryPersonId") Is DBNull.Value, "", dtHD.Rows(0)("DeliveryPersonId"))
    '                End If
    '                'If ScannedDeliveryPerson.ToUpper <> CrnDeliveryPersonId.ToUpper Then
    '                objCreditSales.UpdateDispatchTimeData(ScannedDeliveryPerson, Billno, clsAdmin.SiteCode)
    '                'End If
    '                frmNCreditSales_Load(Nothing, Nothing)
    '                ScannedDeliveryPersonHold = ""
    '            End If
    '        Else
    '            ScannedDeliveryPersonHold = txtDeliveryId.Text
    '            txtDeliveryId.Text = String.Empty
    '            Exit Sub
    '        End If
    '        'txtFilterCreditSales.Focus()
    '    End If
    'End Sub

    'Private Sub txtDeliveryId_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtDeliveryId.TextChanged
    '    Try
    '        Call FilterCreditSales(clsDefaultConfiguration.EvasPizzaChanges)
    '        'txtFilterCreditSales.Select()
    '        txtDeliveryId.Select()
    '    Catch ex As Exception
    '        ShowMessage(ex.Message, getValueByKey("CLAE05"))
    '        LogException(ex)
    '    End Try
    'End Sub



    Private Sub cmbChannelPartner_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Try

        '    Dim filterText As String
        '    dtCreditSales.DefaultView.RowFilter = ""
        '    filterText = cmbChannelPartner.Text
        '    If Not filterText = "All" Then
        '        Dim rowFilterString = "DeliveryPartner='" & filterText & "'"
        '        dtCreditSales.DefaultView.RowFilter = rowFilterString
        '    End If

        '    grdCreditSales.DataSource = dtCreditSales.DefaultView
        '    GridColumnSettings()
        '    If (grdCreditSales.Rows.Count > 1) Then
        '        grdCreditSales.Select(1, 3)
        '    End If

        'Catch ex As Exception
        '    ShowMessage(ex.Message, getValueByKey("CLAE05"))
        '    LogException(ex)
        'End Try
    End Sub

    Private Sub txtDeliveryId_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter AndAlso clsDefaultConfiguration.EvasPizzaChanges Then
            Dim strBillNo As String = ""
            Dim strNewValue As String = ""
            Dim dtSearchData As New DataTable
            If txtDeliveryId.Text = "" Then
                Exit Sub
            Else
                strNewValue = txtDeliveryId.Text
                strBillNo = strNewValue
            End If
            ''ScannedDeliveryPersonOldHold = ScannedDeliveryPersonHold
            'If ScannedDeliveryPersonHold <> txtDeliveryId.Text Then
            '    ScannedDeliveryPersonOldHold = ScannedDeliveryPersonHold
            '    If True Then
            '        ScannedDeliveryPersonHold = txtDeliveryId.Text
            '        txtDeliveryId.Text = String.Empty
            '        Exit Sub
            '    End If
            'ElseIf ScannedDeliveryPersonHold <> ScannedDeliveryPersonOldHold Then
            '    ScannedDeliveryPersonHold = txtDeliveryId.Text
            '    txtDeliveryId.Text = String.Empty
            '    Exit Sub
            'ElseIf ScannedDeliveryPersonHold = ScannedDeliveryPersonOldHold Then
            '    txtDeliveryId.Text = String.Empty
            '    Exit Sub
            'Else
            '    strBillNo = txtDeliveryId.Text
            'End If
            ''''Dim drData() = dtCreditSales.Select("BillNo='" & txtFilterCreditSales.Text & "'")
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
                    dtHD = objCreditSales.GetHomeDeliveryData(Billno, clsAdmin.SiteCode)
                    If Not dtHD Is Nothing AndAlso dtHD.Rows.Count > 0 Then
                        CrnDeliveryPersonId = IIf(dtHD.Rows(0)("DeliveryPersonId") Is DBNull.Value, "", dtHD.Rows(0)("DeliveryPersonId"))
                    End If
                    'If ScannedDeliveryPerson.ToUpper <> CrnDeliveryPersonId.ToUpper Then
                    If drData(0)("Dispatch") = True Then
                        objCreditSales.UpdateDispatchTimeData(ScannedDeliveryPerson, Billno, clsAdmin.SiteCode, True)
                    Else
                        objCreditSales.UpdateDispatchTimeData(ScannedDeliveryPerson, Billno, clsAdmin.SiteCode)
                    End If
                    'End If
                    frmNCreditSales_Load(Nothing, Nothing)
                    'ScannedDeliveryPersonHold = ""
                End If
            Else
                If ScannedDeliveryPersonHold <> txtDeliveryId.Text Then
                    ScannedDeliveryPersonHold = txtDeliveryId.Text
                Else
                    ScannedDeliveryPersonOldHold = ScannedDeliveryPersonHold
                End If
                txtDeliveryId.Text = String.Empty
                Exit Sub
            End If
            'txtFilterCreditSales.Focus()
        End If
    End Sub



    Private Sub CmdViewOrder_Click(sender As Object, e As EventArgs)
        Try
            Dim objfrmViewOrderDetails As New ViewOrderDetails()
            If grdCreditSales.Row > 0 Then
                objfrmViewOrderDetails.BillNo = grdCreditSales.Rows(grdCreditSales.Row)("BillNo")
                objfrmViewOrderDetails.SiteCode = clsAdmin.SiteCode
                objfrmViewOrderDetails.ShowDialog()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Sub cmbOrderStatus_SelectedIndexChanged(sender As Object, e As EventArgs)
    End Sub
    Private Sub cmbOrderStatus_SelectedValueChanged(sender As Object, e As System.EventArgs)
        Dim rowFilterString As New System.Text.StringBuilder() '"orderStatus='" & filterText & "'"
        If DtFromDate.Text <> "" AndAlso DtToDate.Text <> "" Then
            selectedFromDateText = DtFromDate.Value
            selectedToDateText = DtToDate.Value
            IsDeliveryDate = True
        Else
            IsDeliveryDate = False
        End If

        ' If cmbOrderStatus.Text <> "ALL" Then
        'rowFilterString.Append("orderStatus='" & cmbOrderStatus.Text & "' and ")
        ' End If
        'If cmbChannelPartner.Text <> "" Then
        '    If cmbChannelPartner.Text <> "All" Then
        '        'rowFilterString.Append(" and ")f
        '        rowFilterString.Append("DeliveryPartner='" & cmbChannelPartner.Text & "'")
        '        If IsDeliveryDate = True Then
        '            rowFilterString.Append(" and ")
        '        End If
        '    ElseIf IsDeliveryDate = False Then
        '        rowFilterString.Replace("and", "").ToString()
        '    End If
        'ElseIf IsDeliveryDate = False Then
        '    rowFilterString.Replace("and", "").ToString()
        'End If
        'If cmbChannelPartner.Text <> "" Then
        '    If cmbChannelPartner.Text = "All" Then
        '        ' rowFilterString.Replace("and", "").ToString()
        '    Else
        '        rowFilterString.Append(" DeliveryPartner='" & cmbChannelPartner.Text & "' and")
        '    End If
        'Else
        '    'rowFilterString.Replace("and", "").ToString()
        'End If
        If IsDeliveryDate = True Then
            'If cmbOrderStatus.Text = "ALL" AndAlso cmbChannelPartner.Text = "All" Then
            '    rowFilterString.Replace("and", "").ToString()
            'ElseIf cmbChannelPartner.Text = "" Then
            'End If
            rowFilterString.Append(" HDDeliveryDate >='" & selectedFromDateText.ToString("yyyy-MM-dd") & "' and HDDeliveryDate <='" & selectedToDateText.ToString("yyyy-MM-dd") & "'")
        End If
        dtCreditSales.DefaultView.RowFilter = rowFilterString.ToString()
        grdCreditSales.DataSource = dtCreditSales.DefaultView
        GridColumnSettings()
        If (grdCreditSales.Rows.Count > 1) Then
            grdCreditSales.Select(1, 3)
        End If
        EnableDisabaledBottomButtons()
    End Sub
    Sub EnableDisabaledBottomButtons()
        'If cmbOrderStatus.SelectedIndex = 0 Then
        '    cmdDispatch.Enabled = False
        '    cmdAccept.Enabled = False
        '    btnRejectOrder.Enabled = False
        'ElseIf cmbOrderStatus.SelectedIndex = 1 Or cmbOrderStatus.SelectedIndex = 2 Then
        '    cmdDispatch.Enabled = True
        '    cmdAccept.Enabled = False
        '    btnRejectOrder.Enabled = False
        'ElseIf cmbOrderStatus.SelectedIndex = 3 Then
        '    cmdDispatch.Enabled = False
        '    cmdAccept.Enabled = True
        '    btnRejectOrder.Enabled = True
        'ElseIf cmbOrderStatus.SelectedIndex = 4 Then
        '    cmdDispatch.Enabled = True
        '    cmdAccept.Enabled = False
        '    btnRejectOrder.Enabled = False
        'End If
        ' If cmbOrderStatus.Text = "ALL" Or cmbOrderStatus.Text = "Rejected" Or cmbOrderStatus.Text = "Dispatched" Then
        '    cmdDispatch.Enabled = False
        '    cmdAccept.Enabled = False
        '    btnRejectOrder.Enabled = False
        'ElseIf cmbOrderStatus.Text = "OPEN" Then
        '    cmdDispatch.Enabled = False
        '    cmdAccept.Enabled = True
        '    btnRejectOrder.Enabled = True
        'ElseIf cmbOrderStatus.Text = "Accepted" Then
        '    cmdDispatch.Enabled = True
        '    cmdAccept.Enabled = False
        '    btnRejectOrder.Enabled = False
        ' End If

    End Sub

    Private Sub grdCreditSales_MouseClick(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub grdCreditSales_Click(sender As Object, e As EventArgs)
        'Dim _orderStatus As String = grdCreditSales.Rows(grdCreditSales.Row)("OrderStatus")
        'If _orderStatus = "All" Then
        'ElseIf _orderStatus = "OPEN" Then
        '    cmdAccept.Enabled = True
        '    btnRejectOrder.Enabled = True
        '    cmdDispatch.Enabled = False
        'ElseIf _orderStatus = "ORDER_APPROVED_AT_STORE" Then
        '    cmdAccept.Enabled = False
        '    btnRejectOrder.Enabled = False
        '    cmdDispatch.Enabled = True
        'ElseIf _orderStatus = "" Then

        'End If

        'EnableDisabaledBottomButtons()
    End Sub

    Private Sub cmbOrderStatus_TextChanged(sender As Object, e As EventArgs)
        ' EnableDisabaledBottomButtons()
        '    selectedOrderStatusText = cmbOrderStatus.Text
        'Dim filterText As String
        'If dtCreditSales Is Nothing Then
        '    dtCreditSales = objCreditSales.GetHomeDelivery(clsAdmin.SiteCode)
        'End If
        'dtCreditSales.DefaultView.RowFilter = ""
        'filterText = cmbOrderStatus.Text

        'If Not filterText = "ALL" Then
        '    Dim rowFilterString = "orderStatus='" & filterText & "'"
        '    dtCreditSales.DefaultView.RowFilter = rowFilterString
        '    grdCreditSales.DataSource = dtCreditSales.DefaultView
        'Else
        '    grdCreditSales.DataSource = dtCreditSales
        'End If

        'GridColumnSettings()
        'If (grdCreditSales.Rows.Count > 1) Then
        '    grdCreditSales.Select(1, 3)
        'End If
        'EnableDisabaledBottomButtons()
    End Sub
    Private Sub cmbChannelPartner_TextChanged(sender As Object, e As EventArgs)
        Try
            '    selectedChannelPartnerText = cmbChannelPartner.Text
            'If dtCreditSales Is Nothing Then
            '    dtCreditSales = objCreditSales.GetHomeDelivery(clsAdmin.SiteCode)
            'End If
            'Dim filterText As String
            'dtCreditSales.DefaultView.RowFilter = ""
            'filterText = cmbChannelPartner.Text
            'If Not filterText = "All" Then
            '    Dim rowFilterString = "DeliveryPartner='" & filterText & "'"
            '    dtCreditSales.DefaultView.RowFilter = rowFilterString
            'End If

            'grdCreditSales.DataSource = dtCreditSales.DefaultView
            'GridColumnSettings()
            'If (grdCreditSales.Rows.Count > 1) Then
            '    grdCreditSales.Select(1, 3)
            'End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnFilterSearch_Click(sender As Object, e As EventArgs)

        Dim objCust As New frmSearchCustomer
        objCust.CheckTransactionRights = True
        objCust.FormName = Me.Name()
        Dim dialog = objCust.ShowDialog()
        If dialog = Windows.Forms.DialogResult.Cancel Then
            'If CustomerNo = String.Empty Then
            '    btnAdd.Enabled = False
            'Else
            '    btnAdd.Enabled = True
            'End If
            Me.Close()
            Exit Sub
        End If


        Dim dt As DataTable
        Dim objCustm As New clsCLPCustomer
        Dim dtCust As DataTable = objCust.dtCustmInfo()
        txtDeliveryId.Text = Convert.ToString(dtCust.Rows(0)("CustomerNo"))


    End Sub



    Private Sub BindMembershipData()

        dtMembershipdata = ObjclsCommon.GetMembershipDetails(IsEnquiry, "")

        'If Not dtMembershipdata Is Nothing AndAlso dtMembershipdata.Rows.Count > 0 Then
        grdCreditSales.DataSource = dtMembershipdata
        ' Else
        'grdCreditSales.DataSource = Nothing
        'End If

    End Sub


    Private Sub BtnFilterSearch_Click_1(sender As Object, e As EventArgs) Handles BtnFilterSearch.Click
        Dim objCust As New frmSearchCustomer
        objCust.CheckTransactionRights = True
        objCust.FormName = Me.Name()
        Dim dialog = objCust.ShowDialog()
        If dialog = Windows.Forms.DialogResult.Cancel Then
            'If CustomerNo = String.Empty Then
            '    btnAdd.Enabled = False
            'Else
            '    btnAdd.Enabled = True
            'End If

            Exit Sub
        End If


        Dim dt As DataTable
        Dim objCustm As New clsCLPCustomer
        Dim dtCust As DataTable = objCust.dtCustmInfo()
        txtDeliveryId.ReadOnly = False
        txtDeliveryId.Text = Convert.ToString(dtCust.Rows(0)("CustomerNo"))
        txtDeliveryId.ReadOnly = True
    End Sub

    Private Sub cmdRefreshGrid_Click_1(sender As Object, e As EventArgs) Handles cmdRefreshGrid.Click
        If DtFromDate.Value Is DBNull.Value And DtToDate.Value Is DBNull.Value And String.IsNullOrEmpty(txtDeliveryId.Text) Then
            dtMembershipdata = ObjclsCommon.GetMembershipDetails(IsEnquiry, "")
            grdCreditSales.DataSource = dtMembershipdata
            GridColumnSettings()
            Exit Sub
        End If

        Dim rowFilterString As New System.Text.StringBuilder() '"orderStatus='" & filterText & "'"
        If DtFromDate.Text <> "" AndAlso DtToDate.Text <> "" Then
            selectedFromDateText = DtFromDate.Value
            selectedToDateText = DtToDate.Value
            IsDeliveryDate = True
        Else
            IsDeliveryDate = False
        End If



        If Not DtFromDate.Value Is DBNull.Value And DtToDate.Value Is DBNull.Value Then
            ShowMessage("Please Select To date", getValueByKey("CLAE04"))
            Exit Sub
        End If
        If DtFromDate.Value Is DBNull.Value And Not DtToDate.Value Is DBNull.Value Then
            ShowMessage("Please Select From Date", getValueByKey("CLAE04"))
            Exit Sub
        End If
        Dim cardno As String = ObjclsCommon.GetCardNo(txtDeliveryId.Text)
        If String.IsNullOrEmpty(cardno) Then
            ShowMessage("No Record Exists.", "Information")
            txtDeliveryId.Text = ""
            Exit Sub
        End If

        ' If cmbOrderStatus.Text <> "ALL" Then
        'rowFilterString.Append("orderStatus='" & cmbOrderStatus.Text & "' and ")
        ' End If
        'If cmbChannelPartner.Text <> "" Then
        '    If cmbChannelPartner.Text <> "All" Then
        '        'rowFilterString.Append(" and ")f
        '        rowFilterString.Append("DeliveryPartner='" & cmbChannelPartner.Text & "'")
        '        If IsDeliveryDate = True Then
        '            rowFilterString.Append(" and ")
        '        End If
        '    ElseIf IsDeliveryDate = False Then
        '        rowFilterString.Replace("and", "").ToString()
        '    End If
        'ElseIf IsDeliveryDate = False Then
        '    rowFilterString.Replace("and", "").ToString()
        'End If
        'If cmbChannelPartner.Text <> "" Then
        '    If cmbChannelPartner.Text = "All" Then
        '        ' rowFilterString.Replace("and", "").ToString()
        '    Else
        '        rowFilterString.Append(" DeliveryPartner='" & cmbChannelPartner.Text & "' and")
        '    End If
        'Else
        '    'rowFilterString.Replace("and", "").ToString()
        'End If
        '  If IsDeliveryDate = True Then
        'If cmbOrderStatus.Text = "ALL" AndAlso cmbChannelPartner.Text = "All" Then
        '    rowFilterString.Replace("and", "").ToString()
        'ElseIf cmbChannelPartner.Text = "" Then
        'End If

        ' cast(CreatedOn as date) between '2018-04-04'  and '2018-04-04'

        'If String.IsNullOrEmpty(txtDeliveryId.Text) Then
        '    If Not DtFromDate.Value Is DBNull.Value And Not DtToDate.Value Is DBNull.Value Then
        '        If DtFromDate.Value = DtToDate.Value Then
        '            rowFilterString.Append(" CreatedOn ='" & selectedFromDateText.Date & "'")
        '        Else
        '            rowFilterString.Append(" CreatedOn >='" & selectedFromDateText.ToString("yyyy-MM-dd") & "' and CreatedOn <='" & selectedToDateText.ToString("yyyy-MM-dd") & "'")
        '        End If
        '    End If

        'Else
        '    If Not DtFromDate.Value Is DBNull.Value And Not DtToDate.Value Is DBNull.Value Then

        '        If DtFromDate.Value = DtToDate.Value Then
        '            rowFilterString.Append("CreatedOn ='" & selectedFromDateText.ToString("yyyy-MM-dd") & "' and CardNo ='" & txtDeliveryId.Text & "'")
        '        Else
        '            rowFilterString.Append(" CreatedOn >='" & selectedFromDateText.ToString("yyyy-MM-dd") & "' and CreatedOn <='" & selectedToDateText.ToString("yyyy-MM-dd") & "' and CardNo ='" & txtDeliveryId.Text & "' ")
        '        End If
        '    Else
        '        rowFilterString.Append("CardNo ='" & txtDeliveryId.Text & "' ")
        '    End If

        'End If
        Dim condition As String = " and "
        If String.IsNullOrEmpty(cardno) Then
            If Not DtFromDate.Value Is DBNull.Value And Not DtToDate.Value Is DBNull.Value Then
                condition = condition + "(" + "cast(CreatedOn as date) between " + "'" + selectedFromDateText.ToString("yyyy-MM-dd") + "'" + " And " + "'" + selectedToDateText.ToString("yyyy-MM-dd") + "'" + ")"

            End If

        Else
            If Not DtFromDate.Value Is DBNull.Value And Not DtToDate.Value Is DBNull.Value Then
                condition = condition + "(" + "  cast(CreatedOn as date) between " + "'" + selectedFromDateText.ToString("yyyy-MM-dd") + "'" + " And " + "'" + selectedToDateText.ToString("yyyy-MM-dd") + "'" + ")" + " and CardNo ='" & cardno & "'"
            Else

                condition = condition + " CardNo ='" & cardno & "' "
            End If
        End If


        dtMembershipdata = ObjclsCommon.GetMembershipDetails(IsEnquiry, condition)
        txtDeliveryId.ReadOnly = False
        txtDeliveryId.Text = ""
        txtDeliveryId.ReadOnly = True
        'dtMembershipdata.DefaultView.RowFilter = rowFilterString.ToString()
        'grdCreditSales.DataSource = dtMembershipdata.DefaultView
        grdCreditSales.DataSource = dtMembershipdata
        GridColumnSettings()
    End Sub

    Private Sub DtFromDate_ValueChanged(sender As Object, e As EventArgs) Handles DtFromDate.ValueChanged
        If DtFromDate.Value Is DBNull.Value OrElse DtToDate.Value Is DBNull.Value Then
            Exit Sub
        End If
        If DateTime.Compare(DtFromDate.Value, DtToDate.Value) > 0 Then
            ShowMessage("From date can't be greater than To Date", getValueByKey("CLAE04"))
            DtFromDate.Value = DateTime.Now
        End If
    End Sub

    Private Sub DtToDate_ValueChanged(sender As Object, e As EventArgs) Handles DtToDate.ValueChanged
        If DtFromDate.Value Is DBNull.Value OrElse DtToDate.Value Is DBNull.Value Then
            Exit Sub
        End If
        If DateTime.Compare(DtFromDate.Value, DtToDate.Value) > 0 Then
            ShowMessage("From date can't be greater than To Date", getValueByKey("CLAE04"))
            DtToDate.Value = DateTime.Now
        End If
    End Sub

    Private Sub grdCreditSales_DoubleClick(sender As System.Object, e As System.EventArgs) Handles grdCreditSales.DoubleClick

        'If grdCreditSales.Rows.Count < 2 Then Exit Sub
        '' Amount = grdCreditSales.Rows(grdCreditSales.Row)("Amount")
        'If IsEnquiry = 1 Then
        '    CustomerNo = grdCreditSales.Rows(grdCreditSales.Row)("CardNo")

        '    EnquiryBillNo = grdCreditSales.Rows(grdCreditSales.Row)("BillNo")
        '    Dim Amount As Decimal


        '    For index As Integer = 1 To grdCreditSales.Rows.Count - 1

        '        If EnquiryBillNo = grdCreditSales.Rows(index)("BillNo") Then
        '            Amount += Convert.ToDecimal(grdCreditSales.Rows(index)("Amount"))
        '        End If
        '    Next
        '    ' Dim objMem As New frmMembershipEnrollment
        '    'objMem._IsEnquiryfromEnquiryScreen = CustomerNo
        '    'objMem.ShowDialog()


        '    '  If SearchTenderType = "Cash" Then

        '    Dim objPaymentByCash As New frmNAcceptPaymentByCash
        '    objPaymentByCash.TotalBillAmount = Amount
        '    objPaymentByCash.ShowDialog()

        '    If Not (objPaymentByCash.IsCancelAcceptPayment) Then
        '        If Not objPaymentByCash.ReciptTotalAmount Is Nothing And objPaymentByCash.ReciptTotalAmount.Tables.Contains("MSTRecieptType") Then
        '            ds = objPaymentByCash.ReciptTotalAmount
        '            ' _billAmt = objPaymentByCash.TotalBillAmount
        '            ' _paidAmt = objPaymentByCash.TotalCustomerPadiAmount
        '            objPaymentByCash.Close()
        '            If objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeSave Then
        '                SaveAllData()
        '            ElseIf objPaymentByCash.Action = My.Resources.AcceptPaymentActionTypeGift Then
        '                ' GiftMsg = objPaymentByCash.GiftReceiptMessage
        '                SaveAllData()
        '            End If

        '            dtMembershipdata = ObjclsCommon.GetMembershipDetails(IsEnquiry, "")
        '            grdCreditSales.DataSource = dtMembershipdata
        '            GridColumnSettings()


        '        Else
        '            ' ShowMessage(getValueByKey("BL095"), "BL095 - " & getValueByKey("CLAE05"))
        '        End If

        '    End If
        'End If
    End Sub
    Private Function CashmemoHdrDetailsData() As Boolean
        Try
            dtCashMemohdrData = objclsMemb.CashmemoHdrStruct()
            dtCashMemohdrData.Rows.Clear()
            If Not dtView Is Nothing AndAlso dtView.Rows.Count > 0 Then
                For Each drHdr As DataRow In dtView.Rows
                    Dim drData = dtCashMemohdrData.NewRow()
                    drData("ClientName") = clsDefaultConfiguration.ClientName
                    drData("StoreName") = drHdr("SITEOFFICIALNAME")
                    drData("Address") = drHdr("ADDRESSLINE1") + drHdr("ADDRESSLINE2") + drHdr("ADDRESSLINE3") + drHdr("ADDRESSPINCODE")
                    drData("PhoneNumber") = drHdr("TELNO")
                    drData("CashMemoNo") = drHdr("BILLNO")
                    drData("Date") = drHdr("BILLDATE")
                    drData("Time") = drHdr("BILLTIME")
                    drData("Cashier") = drHdr("CREATEDBY")
                    drData("GiftMsg") = ""
                    drData("DineIn") = ""
                    drData("TokenNo") = drHdr("BILLNO").ToString().Trim.Substring(drHdr("BILLNO").ToString().Trim.Length - 2, 2)
                    dtCashMemohdrData.Rows.Add(drData)
                Next
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Function

    Private Function CashmemoBodyDetailsData() As Boolean
        Try
            dtCashMemoBodyData = objclsMemb.GetCashmemoKOTBodyStruct()
            dtCashMemoBodyData.Rows.Clear()
            If Not DtUnique Is Nothing AndAlso DtUnique.Rows.Count > 0 Then
                For Each drBody As DataRow In DtUnique.Rows
                    Dim drBodyData = dtCashMemoBodyData.NewRow()
                    drBodyData("Description") = drBody("DISCRIPTION")
                    drBodyData("Qty") = drBody("QUANTITY")
                    drBodyData("Amt") = drBody("GROSSAMT")
                    dtCashMemoBodyData.Rows.Add(drBodyData)
                    Exit For
                Next
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Function

    ''' <summary>
    ''' Print Payment static information in the footer
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CashmemoFooterDetailsData() As Boolean
        Try
            dtcashmemofooterData = objclsMemb.GetCashmemoKOTFooterStruct()
            dtcashmemofooterData.Rows.Clear()
            Dim VatTanString As String = "VAT/TIN NO: 27120029370 U/V"
            Dim LbtString As String = "LBT NO: TMC-LBT0005578-13"
            Dim msgString As String = "THANK YOU ... VISIT AGAIN"
            Dim drFooterData = dtcashmemofooterData.NewRow()

            drFooterData("VatTinString") = VatTanString
            drFooterData("LBTNo") = LbtString
            drFooterData("ThankYouMesg") = msgString
            dtcashmemofooterData.Rows.Add(drFooterData)

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Function

    Private Function GenerateKOTDetailsPrint() As Boolean
        Try

            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String = System.IO.Path.Combine(Application.StartupPath, "Reports\KOTANDBILL.rdl")
            reportViewer2.LocalReport.ReportPath = appPath

            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()

            Dim DataSource1 As New ReportDataSource("dsCashMemoHdr", dtCashMemohdrData)
            Dim DataSource2 As New ReportDataSource("dsCashmemodtls", dtCashMemoBodyData)
            Dim DataSource3 As New ReportDataSource("DsFooterDetails", dtcashmemofooterData)
            reportViewer2.LocalReport.DataSources.Add(DataSource1)
            reportViewer2.LocalReport.DataSources.Add(DataSource2)
            reportViewer2.LocalReport.DataSources.Add(DataSource3)
            Dim mybytes As [Byte]() = reportViewer2.LocalReport.Render("Pdf")
            'Byte[] mybytes = report.Render("PDF"); for exporting to PDF
            Dim obj As New clsCommon
            'path = clsDefaultConfiguration.DayCloseReportPath & "\DayClose_" & request.ToDate & ".pdf"
            Dim path = clsDefaultConfiguration.DayCloseReportPath & "\KOTANDBILL" & DateTime.Now.ToString("dd-MM-yyyy_HH-mm") & ".pdf"
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            If vIsPrintPreviewAllowed = True Then
                Process.Start(path)
            Else
                'Code For Print SO
                PrinterName = SetPrinterName(dtPrinterInfo, "CashMemo", "Billing")
                Dim pdfdocument As New PdfDocument()
                pdfdocument.LoadFromFile(path)
                pdfdocument.PrinterName = PrinterName
                pdfdocument.PrintDocument.PrinterSettings.Copies = 1
                pdfdocument.PrintDocument.Print()
                pdfdocument.Dispose()
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function


    Private Function PrepareCashMemoReceiptforSave(ByRef dsReceipt As DataSet)



        dsMemb = objclsMemb.GetMembershipTableStructure(clsAdmin.SiteCode, CustomerNo)
        Try
            For Each drReceipt As DataRow In dsReceipt.Tables(0).Rows
                Dim drRecData As DataRow = dsMemb.Tables("CashMemoReceipt").NewRow()
                Dim objclscomm As New clsCommon
                Dim ServerDate = objclscomm.GetCurrentDate()
                drRecData("SiteCode") = clsAdmin.SiteCode
                drRecData("FinYear") = clsAdmin.Financialyear
                drRecData("BillNo") = EnquiryBillNo
                drRecData("CMRECPTLINENO") = drReceipt("SrNo")
                drRecData("TerminalId") = clsAdmin.TerminalID
                drRecData("CARDNO") = drReceipt("Number")
                drRecData("EXCHANGERATE") = drReceipt("ExchangeRate")
                drRecData("TENDERTYPECODE") = drReceipt("RecieptTypeCode")
                drRecData("AMOUNTTENDERED") = drReceipt("Amount")
                drRecData("CURRENCYCODE") = drReceipt("CurrencyCode")
                drRecData("AMOUNTINCURRENCY") = drReceipt("AmountInCurrency")
                drRecData("CmRcptDate") = clsAdmin.DayOpenDate.Date
                drRecData("CmRcptTime") = ServerDate
                drRecData("REFDATE") = drReceipt("Date")
                drRecData("CREATEDAT") = clsAdmin.SiteCode
                drRecData("CREATEDBY") = clsAdmin.UserCode
                drRecData("CREATEDON") = ServerDate
                drRecData("UPDATEDAT") = clsAdmin.SiteCode
                drRecData("UPDATEDBY") = clsAdmin.UserCode
                drRecData("UPDATEDON") = ServerDate
                drRecData("STATUS") = True
                drRecData("TENDERHEADCODE") = drReceipt("RecieptType")
                drRecData("BankAccNo") = drReceipt("BankAccNo")
                dsMemb.Tables("CashMemoReceipt").Rows.Add(drRecData)
            Next
            Return True
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Function



    Private Sub SaveAllData()
        If Not ds Is Nothing Then
            If PrepareCashMemoReceiptforSave(ds) = False Then
                Exit Sub
            End If
        End If

        Dim i As Int32 = 0
        For i = dsMemb.Tables.Count - 1 To 0 Step -1
            If Not dsMemb.Tables(i).TableName = "CashMemoReceipt" Then
                dsMemb.Tables.RemoveAt(i)
            End If
        Next

        Dim objItem As New clsIteamSearch
        If objItem.SaveCashMemoReciptFromEnquiry(dsMemb, EnquiryBillNo, clsAdmin.SiteCode, clsAdmin.UserCode) Then
            ShowMessage("Payment Done Successfully", "Information")

            Dim obj As New SpectrumBL.clsCashMemo()
            dtView = obj.GetCashMemo(EnquiryBillNo, clsAdmin.SiteCode, clsAdmin.LangCode)
            DtUnique = obj.GetBillDetailsDataForPrint(EnquiryBillNo, clsAdmin.SiteCode, clsAdmin.LangCode)
            Dtcombo = obj.GetComboDetailsDataForPrint(EnquiryBillNo, clsAdmin.SiteCode, clsAdmin.LangCode)
            CashmemoHdrDetailsData()
            CashmemoBodyDetailsData()
            CashmemoFooterDetailsData()

            GenerateKOTDetailsPrint()

        End If

        ' End If
    End Sub

    Private Sub btnPayment_Click(sender As Object, e As EventArgs) Handles btnPayment.Click
        If IsEnquiry = 1 Then
            CustomerNo = grdCreditSales.Rows(grdCreditSales.Row)("CardNo")

            EnquiryBillNo = grdCreditSales.Rows(grdCreditSales.Row)("BillNo")
            Dim Amount As Decimal


            For index As Integer = 1 To grdCreditSales.Rows.Count - 1

                If EnquiryBillNo = grdCreditSales.Rows(index)("BillNo") Then
                    Amount += Convert.ToDecimal(grdCreditSales.Rows(index)("Amount"))
                End If
            Next

            Dim objmembership As New frmMembershipEnrollment
            objmembership.IscalledFromIsEnquiry = 1
            objmembership.IsEnquiryBillNo = EnquiryBillNo
            objmembership.IsEnquiryAmount = Amount
            objmembership.IsSameForm = False
            objmembership.CardNo = grdCreditSales.Rows(grdCreditSales.Row)("CardNo")
            objmembership.ShowDialog()
            dtMembershipdata = ObjclsCommon.GetMembershipDetails(IsEnquiry, "")
            grdCreditSales.DataSource = dtMembershipdata
            GridColumnSettings()
        End If



    End Sub

    Private Sub txtDeliveryId_Enter(sender As Object, e As System.EventArgs) Handles txtDeliveryId.Enter
        Try
            cmdRefreshGrid_Click_1(sender, e)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class