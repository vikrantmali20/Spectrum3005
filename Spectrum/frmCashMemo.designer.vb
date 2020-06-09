<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCashMemo
    Inherits CtrlRbnBaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCashMemo))
        Me.C1Ribbon1 = New Spectrum.CtrlRbn()
        Me.rbnAppMenu = New C1.Win.C1Ribbon.RibbonApplicationMenu()
        Me.rbnConfigToolbar = New C1.Win.C1Ribbon.RibbonConfigToolBar()
        Me.lblCM = New C1.Win.C1Ribbon.RibbonLabel()
        Me.lblCMNo = New C1.Win.C1Ribbon.RibbonLabel()
        Me.RibbonQat1 = New C1.Win.C1Ribbon.RibbonQat()
        Me.cmdNew = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbnGrpCMPromotion = New C1.Win.C1Ribbon.RibbonGroup()
        Me.cmdDefaultPromo = New C1.Win.C1Ribbon.RibbonButton()
        Me.cmdApplySelectPromo = New C1.Win.C1Ribbon.RibbonButton()
        Me.cmdClrAllPromo = New C1.Win.C1Ribbon.RibbonButton()
        Me.cmdClearSelectedPromo = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbnTabCM = New C1.Win.C1Ribbon.RibbonTab()
        Me.rbCMNew = New C1.Win.C1Ribbon.RibbonGroup()
        Me.cmdOldCashMemo = New C1.Win.C1Ribbon.RibbonButton()
        Me.cmdReprint = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbnGrpPayments = New C1.Win.C1Ribbon.RibbonGroup()
        Me.cmdPayments = New C1.Win.C1Ribbon.RibbonButton()
        Me.cmdCash = New C1.Win.C1Ribbon.RibbonButton()
        Me.cmdCard = New C1.Win.C1Ribbon.RibbonButton()
        Me.cmdCheque = New C1.Win.C1Ribbon.RibbonButton()
        Me.cmdCreditSale = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbnGrpHoldVoid = New C1.Win.C1Ribbon.RibbonGroup()
        Me.cmdHold = New C1.Win.C1Ribbon.RibbonButton()
        Me.cmdDelete = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbnGrpCustSearch = New C1.Win.C1Ribbon.RibbonGroup()
        Me.cmdCustomerinfo = New C1.Win.C1Ribbon.RibbonButton()
        Me.RibbonGroup1 = New C1.Win.C1Ribbon.RibbonGroup()
        Me.rbnbtnApplyCST = New C1.Win.C1Ribbon.RibbonButton()
        Me.RibbonGroup2 = New C1.Win.C1Ribbon.RibbonGroup()
        Me.rbnbtnRoundOff = New C1.Win.C1Ribbon.RibbonButton()
        Me.lblCustSaleType = New System.Windows.Forms.Label()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.CMbtnBottom = New Spectrum.CtrlCMbtnBottom()
        Me.Payment = New Spectrum.CtrlPayment()
        Me.CashSummary = New Spectrum.CtrlCashSummary()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.ProductImage = New Spectrum.CtrlProductImage()
        Me.CustInfo = New Spectrum.CtrlCustInfo()
        Me.c1SizerGrid = New C1.Win.C1Sizer.C1Sizer()
        Me.CtrlLblManualDisc = New Spectrum.CtrlLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblTotalItemQtyPaymentHistory = New System.Windows.Forms.Label()
        Me.lblCalTotalAmount = New System.Windows.Forms.Label()
        Me.lblCalTotalItemQty = New System.Windows.Forms.Label()
        Me.lblCalTotalItem = New System.Windows.Forms.Label()
        Me.lblTotalAmountPaymentHistory = New System.Windows.Forms.Label()
        Me.lblTotalItemPaymentHistory = New System.Windows.Forms.Label()
        Me.lblTotalPaymentHistory = New System.Windows.Forms.Label()
        Me.CtrlSalesPersons = New Spectrum.CtrlSalesPerson()
        Me.dgMainGrid = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.cmdEnable = New C1.Win.C1Input.C1Button()
        Me.cbManualDisc = New C1.Win.C1List.C1Combo()
        CType(Me.C1StatusBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Ribbon1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.c1SizerGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.c1SizerGrid.SuspendLayout()
        CType(Me.CtrlLblManualDisc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgMainGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbManualDisc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblLoggedIn
        '
        Me.lblLoggedIn.Text = Nothing
        '
        'C1Ribbon1
        '
        Me.C1Ribbon1.ApplicationMenuHolder = Me.rbnAppMenu
        Me.C1Ribbon1.ConfigToolBarHolder = Me.rbnConfigToolbar
        Me.C1Ribbon1.Name = "C1Ribbon1"
        Me.C1Ribbon1.QatHolder = Me.RibbonQat1
        Me.C1Ribbon1.Tabs.Add(Me.rbnTabCM)
        '
        'rbnAppMenu
        '
        Me.rbnAppMenu.LargeImage = Global.Spectrum.My.Resources.Resources.logo
        Me.rbnAppMenu.Name = "rbnAppMenu"
        '
        'rbnConfigToolbar
        '
        Me.rbnConfigToolbar.Items.Add(Me.lblCM)
        Me.rbnConfigToolbar.Items.Add(Me.lblCMNo)
        Me.rbnConfigToolbar.Name = "rbnConfigToolbar"
        '
        'lblCM
        '
        Me.lblCM.Name = "lblCM"
        Me.lblCM.Text = " Cash Memo No:"
        '
        'lblCMNo
        '
        Me.lblCMNo.Name = "lblCMNo"
        '
        'RibbonQat1
        '
        Me.RibbonQat1.HotItemLinks.Add(Me.cmdNew)
        Me.RibbonQat1.HotItemLinks.Add(Me.rbnGrpCMPromotion)
        Me.RibbonQat1.ItemLinks.Add(Me.cmdNew)
        Me.RibbonQat1.Name = "RibbonQat1"
        '
        'cmdNew
        '
        Me.cmdNew.Description = "Create New Cash Memo"
        Me.cmdNew.LargeImage = Global.Spectrum.My.Resources.Resources.cash_memo_new
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.ShortcutKeyDisplayString = "Ctrl+N"
        Me.cmdNew.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.cmdNew.SupportedGroupSizing = C1.Win.C1Ribbon.SupportedGroupSizing.LargeImageOnly
        Me.cmdNew.Text = "New Bill (Ctrl+N)"
        Me.cmdNew.ToolTip = "Ctrl + N as short key"
        '
        'rbnGrpCMPromotion
        '
        Me.rbnGrpCMPromotion.HasLauncherButton = True
        Me.rbnGrpCMPromotion.Image = CType(resources.GetObject("rbnGrpCMPromotion.Image"), System.Drawing.Image)
        Me.rbnGrpCMPromotion.Items.Add(Me.cmdDefaultPromo)
        Me.rbnGrpCMPromotion.Items.Add(Me.cmdApplySelectPromo)
        Me.rbnGrpCMPromotion.Items.Add(Me.cmdClrAllPromo)
        Me.rbnGrpCMPromotion.Items.Add(Me.cmdClearSelectedPromo)
        Me.rbnGrpCMPromotion.Name = "rbnGrpCMPromotion"
        Me.rbnGrpCMPromotion.Text = "Promotion"
        '
        'cmdDefaultPromo
        '
        Me.cmdDefaultPromo.LargeImage = Global.Spectrum.My.Resources.Resources.promo
        Me.cmdDefaultPromo.Name = "cmdDefaultPromo"
        Me.cmdDefaultPromo.ShortcutKeyDisplayString = "F11 as short key"
        Me.cmdDefaultPromo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.cmdDefaultPromo.SupportedGroupSizing = C1.Win.C1Ribbon.SupportedGroupSizing.LargeImageOnly
        Me.cmdDefaultPromo.Text = "Default Promo (Ctrl+D)"
        Me.cmdDefaultPromo.ToolTip = "Ctrl+D as short key"
        '
        'cmdApplySelectPromo
        '
        Me.cmdApplySelectPromo.Description = "Select Promotion to Apply"
        Me.cmdApplySelectPromo.LargeImage = Global.Spectrum.My.Resources.Resources.apply_promo
        Me.cmdApplySelectPromo.Name = "cmdApplySelectPromo"
        Me.cmdApplySelectPromo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.cmdApplySelectPromo.SupportedGroupSizing = C1.Win.C1Ribbon.SupportedGroupSizing.LargeImageOnly
        Me.cmdApplySelectPromo.Text = "Select Promo (Ctrl+P)"
        Me.cmdApplySelectPromo.ToolTip = "Ctrl+P as short key"
        '
        'cmdClrAllPromo
        '
        Me.cmdClrAllPromo.LargeImage = Global.Spectrum.My.Resources.Resources.clear_promo
        Me.cmdClrAllPromo.Name = "cmdClrAllPromo"
        Me.cmdClrAllPromo.ShortcutKeyDisplayString = "Ctrl + C as short key"
        Me.cmdClrAllPromo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.cmdClrAllPromo.SupportedGroupSizing = C1.Win.C1Ribbon.SupportedGroupSizing.LargeImageOnly
        Me.cmdClrAllPromo.Text = "Clear promo (Ctrl + C)"
        Me.cmdClrAllPromo.ToolTip = "Ctrl + C as short key"
        '
        'cmdClearSelectedPromo
        '
        Me.cmdClearSelectedPromo.Description = "Clear Promotion From Selected Item"
        Me.cmdClearSelectedPromo.LargeImage = Global.Spectrum.My.Resources.Resources.clear_sel_promo
        Me.cmdClearSelectedPromo.Name = "cmdClearSelectedPromo"
        Me.cmdClearSelectedPromo.SupportedGroupSizing = C1.Win.C1Ribbon.SupportedGroupSizing.LargeImageOnly
        Me.cmdClearSelectedPromo.Text = "Clear Selected Item (F3)"
        Me.cmdClearSelectedPromo.ToolTip = "F3 as short key"
        Me.cmdClearSelectedPromo.Visible = False
        '
        'rbnTabCM
        '
        Me.rbnTabCM.Groups.Add(Me.rbCMNew)
        Me.rbnTabCM.Groups.Add(Me.rbnGrpCMPromotion)
        Me.rbnTabCM.Groups.Add(Me.rbnGrpPayments)
        Me.rbnTabCM.Groups.Add(Me.rbnGrpHoldVoid)
        Me.rbnTabCM.Groups.Add(Me.rbnGrpCustSearch)
        Me.rbnTabCM.Groups.Add(Me.RibbonGroup1)
        Me.rbnTabCM.Groups.Add(Me.RibbonGroup2)
        Me.rbnTabCM.Name = "rbnTabCM"
        Me.rbnTabCM.Text = "Cash Memo"
        '
        'rbCMNew
        '
        Me.rbCMNew.Image = CType(resources.GetObject("rbCMNew.Image"), System.Drawing.Image)
        Me.rbCMNew.Items.Add(Me.cmdNew)
        Me.rbCMNew.Items.Add(Me.cmdOldCashMemo)
        Me.rbCMNew.Items.Add(Me.cmdReprint)
        Me.rbCMNew.Name = "rbCMNew"
        Me.rbCMNew.Text = "Billing"
        '
        'cmdOldCashMemo
        '
        Me.cmdOldCashMemo.LargeImage = Global.Spectrum.My.Resources.Resources.cash_memo_edit
        Me.cmdOldCashMemo.Name = "cmdOldCashMemo"
        Me.cmdOldCashMemo.ShortcutKeyDisplayString = "Ctrl+E"
        Me.cmdOldCashMemo.SmallImage = CType(resources.GetObject("cmdOldCashMemo.SmallImage"), System.Drawing.Image)
        Me.cmdOldCashMemo.SupportedGroupSizing = C1.Win.C1Ribbon.SupportedGroupSizing.LargeImageOnly
        Me.cmdOldCashMemo.Text = "Edit Old Bill (Ctrl + E)"
        Me.cmdOldCashMemo.ToolTip = "Ctrl +E as short key"
        '
        'cmdReprint
        '
        Me.cmdReprint.LargeImage = Global.Spectrum.My.Resources.Resources.print_bl
        Me.cmdReprint.Name = "cmdReprint"
        Me.cmdReprint.SmallImage = CType(resources.GetObject("cmdReprint.SmallImage"), System.Drawing.Image)
        Me.cmdReprint.SupportedGroupSizing = C1.Win.C1Ribbon.SupportedGroupSizing.LargeImageOnly
        Me.cmdReprint.Text = "Reprint Bill (Ctrl + R)"
        Me.cmdReprint.ToolTip = "Ctrl+R"
        '
        'rbnGrpPayments
        '
        Me.rbnGrpPayments.Image = CType(resources.GetObject("rbnGrpPayments.Image"), System.Drawing.Image)
        Me.rbnGrpPayments.Items.Add(Me.cmdPayments)
        Me.rbnGrpPayments.Items.Add(Me.cmdCash)
        Me.rbnGrpPayments.Items.Add(Me.cmdCard)
        Me.rbnGrpPayments.Items.Add(Me.cmdCheque)
        Me.rbnGrpPayments.Items.Add(Me.cmdCreditSale)
        Me.rbnGrpPayments.Name = "rbnGrpPayments"
        Me.rbnGrpPayments.Text = "Payments"
        '
        'cmdPayments
        '
        Me.cmdPayments.Description = "pay with multiple tender mode"
        Me.cmdPayments.LargeImage = Global.Spectrum.My.Resources.Resources.payment
        Me.cmdPayments.Name = "cmdPayments"
        Me.cmdPayments.ShortcutKeys = System.Windows.Forms.Keys.F4
        Me.cmdPayments.Text = "Pay (F4)"
        Me.cmdPayments.ToolTip = "F4 as short key"
        '
        'cmdCash
        '
        Me.cmdCash.LargeImage = Global.Spectrum.My.Resources.Resources.cash
        Me.cmdCash.Name = "cmdCash"
        Me.cmdCash.ShortcutKeyDisplayString = "F5. as short key"
        Me.cmdCash.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.cmdCash.Text = "Cash (F5)"
        Me.cmdCash.ToolTip = "F5 as short key"
        '
        'cmdCard
        '
        Me.cmdCard.LargeImage = Global.Spectrum.My.Resources.Resources.card2
        Me.cmdCard.Name = "cmdCard"
        Me.cmdCard.ShortcutKeyDisplayString = "F6 as short key"
        Me.cmdCard.ShortcutKeys = System.Windows.Forms.Keys.F6
        Me.cmdCard.Text = "Card (F6)"
        Me.cmdCard.ToolTip = "F6 as short key"
        '
        'cmdCheque
        '
        Me.cmdCheque.LargeImage = Global.Spectrum.My.Resources.Resources.chque
        Me.cmdCheque.Name = "cmdCheque"
        Me.cmdCheque.ShortcutKeys = System.Windows.Forms.Keys.F7
        Me.cmdCheque.Text = "Cheque (F7)"
        Me.cmdCheque.ToolTip = "F7 as short key"
        '
        'cmdCreditSale
        '
        Me.cmdCreditSale.LargeImage = CType(resources.GetObject("cmdCreditSale.LargeImage"), System.Drawing.Image)
        Me.cmdCreditSale.Name = "cmdCreditSale"
        Me.cmdCreditSale.Text = "Credit Sale(F8)"
        '
        'rbnGrpHoldVoid
        '
        Me.rbnGrpHoldVoid.Items.Add(Me.cmdHold)
        Me.rbnGrpHoldVoid.Items.Add(Me.cmdDelete)
        Me.rbnGrpHoldVoid.Name = "rbnGrpHoldVoid"
        Me.rbnGrpHoldVoid.Text = "Hold/Resume"
        '
        'cmdHold
        '
        Me.cmdHold.LargeImage = CType(resources.GetObject("cmdHold.LargeImage"), System.Drawing.Image)
        Me.cmdHold.Name = "cmdHold"
        Me.cmdHold.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
        Me.cmdHold.Tag = "RESUME"
        Me.cmdHold.Text = "Resume Bill (Ctrl+H)"
        Me.cmdHold.ToolTip = "Ctrl H  as short key"
        '
        'cmdDelete
        '
        Me.cmdDelete.LargeImage = Global.Spectrum.My.Resources.Resources.void
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Text = "Void Bill (Ctrl+Del)"
        Me.cmdDelete.ToolTip = "Ctrl + Del as short key"
        '
        'rbnGrpCustSearch
        '
        Me.rbnGrpCustSearch.Image = CType(resources.GetObject("rbnGrpCustSearch.Image"), System.Drawing.Image)
        Me.rbnGrpCustSearch.Items.Add(Me.cmdCustomerinfo)
        Me.rbnGrpCustSearch.Name = "rbnGrpCustSearch"
        Me.rbnGrpCustSearch.Text = "Customer"
        '
        'cmdCustomerinfo
        '
        Me.cmdCustomerinfo.AllowSelection = True
        Me.cmdCustomerinfo.LargeImage = Global.Spectrum.My.Resources.Resources.customer_search
        Me.cmdCustomerinfo.Name = "cmdCustomerinfo"
        Me.cmdCustomerinfo.ShortcutKeyDisplayString = "Ctrl+S as short key"
        Me.cmdCustomerinfo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.cmdCustomerinfo.Text = "Customer Search (Ctrl+S)"
        Me.cmdCustomerinfo.ToolTip = "Customer   Search (Ctrl+S)"
        '
        'RibbonGroup1
        '
        Me.RibbonGroup1.Items.Add(Me.rbnbtnApplyCST)
        Me.RibbonGroup1.Name = "RibbonGroup1"
        Me.RibbonGroup1.Text = "CST"
        '
        'rbnbtnApplyCST
        '
        Me.rbnbtnApplyCST.Name = "rbnbtnApplyCST"
        Me.rbnbtnApplyCST.Text = "Apply CST"
        '
        'RibbonGroup2
        '
        Me.RibbonGroup2.Image = CType(resources.GetObject("RibbonGroup2.Image"), System.Drawing.Image)
        Me.RibbonGroup2.Items.Add(Me.rbnbtnRoundOff)
        Me.RibbonGroup2.Name = "RibbonGroup2"
        Me.RibbonGroup2.Text = "RoundOff"
        '
        'rbnbtnRoundOff
        '
        Me.rbnbtnRoundOff.LargeImage = CType(resources.GetObject("rbnbtnRoundOff.LargeImage"), System.Drawing.Image)
        Me.rbnbtnRoundOff.Name = "rbnbtnRoundOff"
        Me.rbnbtnRoundOff.Text = "Round Off"
        '
        'lblCustSaleType
        '
        Me.lblCustSaleType.AutoSize = True
        Me.lblCustSaleType.BackColor = System.Drawing.Color.Transparent
        Me.lblCustSaleType.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustSaleType.ForeColor = System.Drawing.Color.Black
        Me.lblCustSaleType.Location = New System.Drawing.Point(840, 53)
        Me.lblCustSaleType.Margin = New System.Windows.Forms.Padding(1)
        Me.lblCustSaleType.Name = "lblCustSaleType"
        Me.lblCustSaleType.Size = New System.Drawing.Size(128, 19)
        Me.lblCustSaleType.TabIndex = 55
        Me.lblCustSaleType.Text = "Home Delivery"
        Me.lblCustSaleType.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel1, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 156)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(971, 403)
        Me.TableLayoutPanel4.TabIndex = 60
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.55304!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.99588!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.34809!))
        Me.TableLayoutPanel1.Controls.Add(Me.CMbtnBottom, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Payment, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CashSummary, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 285)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(965, 115)
        Me.TableLayoutPanel1.TabIndex = 60
        '
        'CMbtnBottom
        '
        Me.CMbtnBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CMbtnBottom.Location = New System.Drawing.Point(1, 1)
        Me.CMbtnBottom.Margin = New System.Windows.Forms.Padding(1)
        Me.CMbtnBottom.Name = "CMbtnBottom"
        Me.CMbtnBottom.Size = New System.Drawing.Size(515, 113)
        Me.CMbtnBottom.TabIndex = 1
        '
        'Payment
        '
        Me.Payment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Payment.Location = New System.Drawing.Point(749, 1)
        Me.Payment.Margin = New System.Windows.Forms.Padding(1)
        Me.Payment.Name = "Payment"
        Me.Payment.Size = New System.Drawing.Size(215, 113)
        Me.Payment.TabIndex = 41
        Me.Payment.TabStop = False
        Me.Payment.Visible = False
        '
        'CashSummary
        '
        Me.CashSummary.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CashSummary.hdrText = "Cash Memo Summary"
        Me.CashSummary.lbl1 = "Gross Amt:"
        Me.CashSummary.lbl10 = "10"
        Me.CashSummary.lbl2 = "Tax Amt:"
        Me.CashSummary.lbl3 = "Disc Amt:"
        Me.CashSummary.lbl4 = "Net Amt:"
        Me.CashSummary.lbl5 = "Credit Sales"
        Me.CashSummary.lbl6 = ""
        Me.CashSummary.lbl7 = ""
        Me.CashSummary.lbl8 = "10"
        Me.CashSummary.lbl9 = "10"
        Me.CashSummary.lbltxt1 = "0.00"
        Me.CashSummary.lbltxt10 = ""
        Me.CashSummary.lbltxt2 = "0.00"
        Me.CashSummary.lbltxt3 = "0.00"
        Me.CashSummary.lbltxt4 = "0.00"
        Me.CashSummary.lbltxt5 = "CtrlLabel7"
        Me.CashSummary.lbltxt6 = "CtrlLabel8"
        Me.CashSummary.lbltxt7 = ""
        Me.CashSummary.lbltxt8 = ""
        Me.CashSummary.lbltxt9 = ""
        Me.CashSummary.lbltxtVisible1 = True
        Me.CashSummary.lbltxtVisible10 = False
        Me.CashSummary.lbltxtVisible2 = True
        Me.CashSummary.lbltxtVisible3 = True
        Me.CashSummary.lbltxtVisible4 = True
        Me.CashSummary.lbltxtVisible5 = True
        Me.CashSummary.lbltxtVisible6 = False
        Me.CashSummary.lbltxtVisible7 = False
        Me.CashSummary.lbltxtVisible8 = False
        Me.CashSummary.lbltxtVisible9 = False
        Me.CashSummary.lblVisible1 = True
        Me.CashSummary.lblVisible10 = False
        Me.CashSummary.lblVisible2 = True
        Me.CashSummary.lblVisible3 = True
        Me.CashSummary.lblVisible4 = True
        Me.CashSummary.lblVisible5 = True
        Me.CashSummary.lblVisible6 = False
        Me.CashSummary.lblVisible7 = False
        Me.CashSummary.lblVisible8 = False
        Me.CashSummary.lblVisible9 = False
        Me.CashSummary.Location = New System.Drawing.Point(497, 1)
        Me.CashSummary.Margin = New System.Windows.Forms.Padding(1, 1, 2, 1)
        Me.CashSummary.Name = "CashSummary"
        Me.CashSummary.RowCount = 6
        Me.CashSummary.Size = New System.Drawing.Size(231, 134)
        Me.CashSummary.TabIndex = 2
        Me.CashSummary.TabStop = False
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.75489!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.24511!))
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel2, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.c1SizerGrid, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(965, 276)
        Me.TableLayoutPanel3.TabIndex = 59
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.ProductImage, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.CustInfo, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(753, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(209, 270)
        Me.TableLayoutPanel2.TabIndex = 45
        '
        'ProductImage
        '
        Me.ProductImage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ProductImage.Location = New System.Drawing.Point(0, 0)
        Me.ProductImage.Margin = New System.Windows.Forms.Padding(0)
        Me.ProductImage.Name = "ProductImage"
        Me.ProductImage.Size = New System.Drawing.Size(209, 135)
        Me.ProductImage.TabIndex = 50
        Me.ProductImage.TabStop = False
        '
        'CustInfo
        '
        Me.CustInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CustInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CustInfo.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CustInfo.Location = New System.Drawing.Point(0, 135)
        Me.CustInfo.Margin = New System.Windows.Forms.Padding(0)
        Me.CustInfo.Name = "CustInfo"
        Me.CustInfo.Size = New System.Drawing.Size(209, 135)
        Me.CustInfo.TabIndex = 51
        Me.CustInfo.TabStop = False
        '
        'c1SizerGrid
        '
        Me.c1SizerGrid.Border.Color = System.Drawing.Color.LightSlateGray
        Me.c1SizerGrid.Border.Corners = New C1.Win.C1Sizer.Corners(0, 0, 0, 4)
        Me.c1SizerGrid.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.c1SizerGrid.Controls.Add(Me.CtrlLblManualDisc)
        Me.c1SizerGrid.Controls.Add(Me.GroupBox1)
        Me.c1SizerGrid.Controls.Add(Me.CtrlSalesPersons)
        Me.c1SizerGrid.Controls.Add(Me.dgMainGrid)
        Me.c1SizerGrid.Controls.Add(Me.cmdEnable)
        Me.c1SizerGrid.Controls.Add(Me.cbManualDisc)
        Me.c1SizerGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1SizerGrid.GridDefinition = resources.GetString("c1SizerGrid.GridDefinition")
        Me.c1SizerGrid.Location = New System.Drawing.Point(3, 3)
        Me.c1SizerGrid.Name = "c1SizerGrid"
        Me.c1SizerGrid.Size = New System.Drawing.Size(744, 270)
        Me.c1SizerGrid.TabIndex = 2
        Me.c1SizerGrid.TabStop = False
        Me.c1SizerGrid.Text = "C1Sizer1"
        '
        'CtrlLblManualDisc
        '
        Me.CtrlLblManualDisc.AttachedTextBoxName = Nothing
        Me.CtrlLblManualDisc.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLblManualDisc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLblManualDisc.ForeColor = System.Drawing.Color.Black
        Me.CtrlLblManualDisc.Location = New System.Drawing.Point(5, 5)
        Me.CtrlLblManualDisc.Name = "CtrlLblManualDisc"
        Me.CtrlLblManualDisc.Size = New System.Drawing.Size(120, 21)
        Me.CtrlLblManualDisc.TabIndex = 44
        Me.CtrlLblManualDisc.Tag = Nothing
        Me.CtrlLblManualDisc.Text = "Manual Discount"
        Me.CtrlLblManualDisc.TextDetached = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblTotalItemQtyPaymentHistory)
        Me.GroupBox1.Controls.Add(Me.lblCalTotalAmount)
        Me.GroupBox1.Controls.Add(Me.lblCalTotalItemQty)
        Me.GroupBox1.Controls.Add(Me.lblCalTotalItem)
        Me.GroupBox1.Controls.Add(Me.lblTotalAmountPaymentHistory)
        Me.GroupBox1.Controls.Add(Me.lblTotalItemPaymentHistory)
        Me.GroupBox1.Controls.Add(Me.lblTotalPaymentHistory)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 235)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Size = New System.Drawing.Size(734, 30)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        '
        'lblTotalItemQtyPaymentHistory
        '
        Me.lblTotalItemQtyPaymentHistory.AutoSize = True
        Me.lblTotalItemQtyPaymentHistory.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalItemQtyPaymentHistory.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalItemQtyPaymentHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.lblTotalItemQtyPaymentHistory.Location = New System.Drawing.Point(293, 11)
        Me.lblTotalItemQtyPaymentHistory.Name = "lblTotalItemQtyPaymentHistory"
        Me.lblTotalItemQtyPaymentHistory.Size = New System.Drawing.Size(62, 13)
        Me.lblTotalItemQtyPaymentHistory.TabIndex = 129
        Me.lblTotalItemQtyPaymentHistory.Text = "Quantity"
        Me.lblTotalItemQtyPaymentHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCalTotalAmount
        '
        Me.lblCalTotalAmount.AutoSize = True
        Me.lblCalTotalAmount.BackColor = System.Drawing.Color.Transparent
        Me.lblCalTotalAmount.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalTotalAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.lblCalTotalAmount.Location = New System.Drawing.Point(480, 11)
        Me.lblCalTotalAmount.Name = "lblCalTotalAmount"
        Me.lblCalTotalAmount.Size = New System.Drawing.Size(15, 13)
        Me.lblCalTotalAmount.TabIndex = 133
        Me.lblCalTotalAmount.Text = "0"
        Me.lblCalTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCalTotalItemQty
        '
        Me.lblCalTotalItemQty.BackColor = System.Drawing.Color.Transparent
        Me.lblCalTotalItemQty.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalTotalItemQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.lblCalTotalItemQty.Location = New System.Drawing.Point(184, 11)
        Me.lblCalTotalItemQty.Name = "lblCalTotalItemQty"
        Me.lblCalTotalItemQty.Size = New System.Drawing.Size(103, 13)
        Me.lblCalTotalItemQty.TabIndex = 132
        Me.lblCalTotalItemQty.Text = "0"
        Me.lblCalTotalItemQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCalTotalItem
        '
        Me.lblCalTotalItem.BackColor = System.Drawing.Color.Transparent
        Me.lblCalTotalItem.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalTotalItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.lblCalTotalItem.Location = New System.Drawing.Point(74, 11)
        Me.lblCalTotalItem.Name = "lblCalTotalItem"
        Me.lblCalTotalItem.Size = New System.Drawing.Size(45, 13)
        Me.lblCalTotalItem.TabIndex = 131
        Me.lblCalTotalItem.Text = "0"
        Me.lblCalTotalItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalAmountPaymentHistory
        '
        Me.lblTotalAmountPaymentHistory.AutoSize = True
        Me.lblTotalAmountPaymentHistory.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalAmountPaymentHistory.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalAmountPaymentHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.lblTotalAmountPaymentHistory.Location = New System.Drawing.Point(380, 11)
        Me.lblTotalAmountPaymentHistory.Name = "lblTotalAmountPaymentHistory"
        Me.lblTotalAmountPaymentHistory.Size = New System.Drawing.Size(94, 13)
        Me.lblTotalAmountPaymentHistory.TabIndex = 130
        Me.lblTotalAmountPaymentHistory.Text = "TotalAmount:"
        Me.lblTotalAmountPaymentHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalItemPaymentHistory
        '
        Me.lblTotalItemPaymentHistory.AutoSize = True
        Me.lblTotalItemPaymentHistory.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalItemPaymentHistory.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalItemPaymentHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.lblTotalItemPaymentHistory.Location = New System.Drawing.Point(125, 11)
        Me.lblTotalItemPaymentHistory.Name = "lblTotalItemPaymentHistory"
        Me.lblTotalItemPaymentHistory.Size = New System.Drawing.Size(38, 13)
        Me.lblTotalItemPaymentHistory.TabIndex = 128
        Me.lblTotalItemPaymentHistory.Text = "Item"
        Me.lblTotalItemPaymentHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalPaymentHistory
        '
        Me.lblTotalPaymentHistory.AutoSize = True
        Me.lblTotalPaymentHistory.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalPaymentHistory.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalPaymentHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.lblTotalPaymentHistory.Location = New System.Drawing.Point(20, 11)
        Me.lblTotalPaymentHistory.Name = "lblTotalPaymentHistory"
        Me.lblTotalPaymentHistory.Size = New System.Drawing.Size(48, 13)
        Me.lblTotalPaymentHistory.TabIndex = 127
        Me.lblTotalPaymentHistory.Text = "Total :"
        Me.lblTotalPaymentHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CtrlSalesPersons
        '
        Me.CtrlSalesPersons.AlignChange = Nothing
        Me.CtrlSalesPersons.Location = New System.Drawing.Point(5, 30)
        Me.CtrlSalesPersons.Name = "CtrlSalesPersons"
        Me.CtrlSalesPersons.Size = New System.Drawing.Size(734, 24)
        Me.CtrlSalesPersons.TabIndex = 0
        '
        'dgMainGrid
        '
        Me.dgMainGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.dgMainGrid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.dgMainGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgMainGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D
        Me.dgMainGrid.CellButtonImage = Global.Spectrum.My.Resources.Resources.del_icon
        Me.dgMainGrid.ColumnInfo = resources.GetString("dgMainGrid.ColumnInfo")
        Me.dgMainGrid.ExtendLastCol = True
        Me.dgMainGrid.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.dgMainGrid.Location = New System.Drawing.Point(5, 58)
        Me.dgMainGrid.Name = "dgMainGrid"
        Me.dgMainGrid.Rows.Count = 12
        Me.dgMainGrid.Rows.DefaultSize = 21
        Me.dgMainGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgMainGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.dgMainGrid.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.dgMainGrid.Size = New System.Drawing.Size(734, 173)
        Me.dgMainGrid.StyleInfo = resources.GetString("dgMainGrid.StyleInfo")
        Me.dgMainGrid.SubtotalPosition = C1.Win.C1FlexGrid.SubtotalPositionEnum.BelowData
        Me.dgMainGrid.TabIndex = 1
        '
        'cmdEnable
        '
        Me.cmdEnable.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdEnable.AutoSize = True
        Me.cmdEnable.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdEnable.Location = New System.Drawing.Point(620, 5)
        Me.cmdEnable.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdEnable.Name = "cmdEnable"
        Me.cmdEnable.Size = New System.Drawing.Size(119, 23)
        Me.cmdEnable.TabIndex = 4
        Me.cmdEnable.Tag = "E"
        Me.cmdEnable.Text = "Enable (Ctrl+B)"
        Me.cmdEnable.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdEnable.UseMnemonic = False
        Me.cmdEnable.UseVisualStyleBackColor = True
        Me.cmdEnable.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cbManualDisc
        '
        Me.cbManualDisc.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cbManualDisc.AllowColMove = False
        Me.cbManualDisc.AutoCompletion = True
        Me.cbManualDisc.AutoDropDown = True
        Me.cbManualDisc.AutoSize = False
        Me.cbManualDisc.Caption = "Manual Promotion"
        Me.cbManualDisc.CaptionHeight = 17
        Me.cbManualDisc.CaptionVisible = False
        Me.cbManualDisc.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cbManualDisc.ColumnCaptionHeight = 17
        Me.cbManualDisc.ColumnFooterHeight = 17
        Me.cbManualDisc.ColumnHeaders = False
        Me.cbManualDisc.ContentHeight = 15
        Me.cbManualDisc.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cbManualDisc.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cbManualDisc.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbManualDisc.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cbManualDisc.EditorHeight = 15
        Me.cbManualDisc.FlatStyle = C1.Win.C1List.FlatModeEnum.Popup
        Me.cbManualDisc.Images.Add(CType(resources.GetObject("cbManualDisc.Images"), System.Drawing.Image))
        Me.cbManualDisc.ItemHeight = 15
        Me.cbManualDisc.Location = New System.Drawing.Point(129, 5)
        Me.cbManualDisc.MatchEntryTimeout = CType(2000, Long)
        Me.cbManualDisc.MaxDropDownItems = CType(5, Short)
        Me.cbManualDisc.MaxLength = 32767
        Me.cbManualDisc.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cbManualDisc.Name = "cbManualDisc"
        Me.cbManualDisc.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cbManualDisc.Size = New System.Drawing.Size(487, 21)
        Me.cbManualDisc.TabIndex = 4
        Me.cbManualDisc.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cbManualDisc.PropBag = resources.GetString("cbManualDisc.PropBag")
        '
        'frmCashMemo
        '
        Me._FrmTranCode = "Billing"
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(971, 581)
        Me.Controls.Add(Me.TableLayoutPanel4)
        Me.Controls.Add(Me.lblCustSaleType)
        Me.Controls.Add(Me.C1Ribbon1)
        Me.IsDocToParent = True
        Me.IsNetWorkConnected = True
        Me.MinimumSize = New System.Drawing.Size(798, 568)
        Me.Name = "frmCashMemo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Billing"
        Me.Controls.SetChildIndex(Me.C1StatusBar1, 0)
        Me.Controls.SetChildIndex(Me.C1Ribbon1, 0)
        Me.Controls.SetChildIndex(Me.lblCustSaleType, 0)
        Me.Controls.SetChildIndex(Me.TableLayoutPanel4, 0)
        CType(Me.C1StatusBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Ribbon1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.c1SizerGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.c1SizerGrid.ResumeLayout(False)
        Me.c1SizerGrid.PerformLayout()
        CType(Me.CtrlLblManualDisc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgMainGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbManualDisc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents C1Ribbon1 As CtrlRbn
    Friend WithEvents rbnAppMenu As C1.Win.C1Ribbon.RibbonApplicationMenu
    Friend WithEvents rbnConfigToolbar As C1.Win.C1Ribbon.RibbonConfigToolBar
    Friend WithEvents RibbonQat1 As C1.Win.C1Ribbon.RibbonQat
    Friend WithEvents cmdNew As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbnGrpCMPromotion As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents cmdDefaultPromo As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents cmdApplySelectPromo As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents cmdClrAllPromo As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents cmdClearSelectedPromo As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbnTabCM As C1.Win.C1Ribbon.RibbonTab
    Friend WithEvents rbCMNew As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents cmdOldCashMemo As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbnGrpPayments As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents cmdPayments As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents cmdCheque As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents cmdCash As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents cmdCard As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbnGrpHoldVoid As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents cmdHold As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents cmdDelete As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbnGrpCustSearch As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents cmdCustomerinfo As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents cmdReprint As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents lblCM As C1.Win.C1Ribbon.RibbonLabel
    Friend WithEvents lblCMNo As C1.Win.C1Ribbon.RibbonLabel
    Friend WithEvents RibbonGroup1 As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents rbnbtnApplyCST As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents lblCustSaleType As System.Windows.Forms.Label
    Friend WithEvents cmdCreditSale As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents RibbonGroup2 As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents rbnbtnRoundOff As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CMbtnBottom As Spectrum.CtrlCMbtnBottom
    Friend WithEvents Payment As Spectrum.CtrlPayment
    Friend WithEvents CashSummary As Spectrum.CtrlCashSummary
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ProductImage As Spectrum.CtrlProductImage
    Friend WithEvents CustInfo As Spectrum.CtrlCustInfo
    Friend WithEvents c1SizerGrid As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents CtrlLblManualDisc As Spectrum.CtrlLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalItemQtyPaymentHistory As System.Windows.Forms.Label
    Friend WithEvents lblCalTotalAmount As System.Windows.Forms.Label
    Friend WithEvents lblCalTotalItemQty As System.Windows.Forms.Label
    Friend WithEvents lblCalTotalItem As System.Windows.Forms.Label
    Friend WithEvents lblTotalAmountPaymentHistory As System.Windows.Forms.Label
    Friend WithEvents lblTotalItemPaymentHistory As System.Windows.Forms.Label
    Friend WithEvents lblTotalPaymentHistory As System.Windows.Forms.Label
    Friend WithEvents CtrlSalesPersons As Spectrum.CtrlSalesPerson
    Friend WithEvents dgMainGrid As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents cmdEnable As C1.Win.C1Input.C1Button
    Friend WithEvents cbManualDisc As C1.Win.C1List.C1Combo
End Class
