<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPCSOBookingScreen
    Inherits Spectrum.CtrlRbnBaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPCSalesOrderCreation))
        Me.CtrlRbn1 = New Spectrum.CtrlRbn()
        Me.RibbonApplicationMenu1 = New C1.Win.C1Ribbon.RibbonApplicationMenu()
        Me.RibbonConfigToolBar1 = New C1.Win.C1Ribbon.RibbonConfigToolBar()
        Me.RibbonQat1 = New C1.Win.C1Ribbon.RibbonQat()
        Me.rbnTabSO = New C1.Win.C1Ribbon.RibbonTab()
        Me.rbgrpSO = New C1.Win.C1Ribbon.RibbonGroup()
        Me.rbbtnSONew = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbbtnSOEdit = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbbtnSOCancel = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbgrpSaveAndPrint = New C1.Win.C1Ribbon.RibbonGroup()
        Me.rbbtnSave = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbbtnPrint = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbnGrpCMPromotion = New C1.Win.C1Ribbon.RibbonGroup()
        Me.rbbtnDefaultPromo = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbbtnSelectPromo = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbbtnClrAllPromo = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbbtnClearSelectedPromo = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbnGrpCST = New C1.Win.C1Ribbon.RibbonGroup()
        Me.rbnCST = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbnGrpAddCombo = New C1.Win.C1Ribbon.RibbonGroup()
        Me.rbBtnAddCombo = New C1.Win.C1Ribbon.RibbonButton()
        Me.RibbonGroup2 = New C1.Win.C1Ribbon.RibbonGroup()
        Me.rbBtnRoundOff = New C1.Win.C1Ribbon.RibbonButton()
        Me.c1SizerGrid = New C1.Win.C1Sizer.C1Sizer()
        Me.grdScanItem = New Spectrum.CtrlGrid()
        Me.CtrlSalesPersons = New Spectrum.CtrlSalesPerson()
        Me.C1Sizer2 = New C1.Win.C1Sizer.C1Sizer()
        Me.cmdGenerateSTR = New Spectrum.CtrlBtn()
        Me.BtnSelectDeliveryLoc = New Spectrum.CtrlBtn()
        Me.CtrlBtnSearchCLP = New Spectrum.CtrlBtn()
        Me.CtrlbtnSOOtherCharges = New Spectrum.CtrlBtn()
        Me.CtrlBtnStockCheck = New Spectrum.CtrlBtn()
        Me.tabSalesOrder = New Spectrum.CtrlTab()
        Me.TabPageAddItems = New C1.Win.C1Command.C1DockingTabPage()
        Me.TabPageDeliveryLocation = New C1.Win.C1Command.C1DockingTabPage()
        Me.dgDeliveryLocation = New Spectrum.CtrlGrid()
        Me.CtrlLblDelivery = New Spectrum.CtrlLabel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.rdDelNo = New System.Windows.Forms.RadioButton()
        Me.lblStrRaised = New Spectrum.CtrlLabel()
        Me.lblCompName = New Spectrum.CtrlLabel()
        Me.rdDelYes = New System.Windows.Forms.RadioButton()
        Me.lblRemarks = New Spectrum.CtrlLabel()
        Me.lblDispStrRaised = New Spectrum.CtrlLabel()
        Me.lblSelCust = New Spectrum.CtrlLabel()
        Me.lblSalesOrderNo = New Spectrum.CtrlLabel()
        Me.lblDelReq = New Spectrum.CtrlLabel()
        Me.lblDispCompany = New Spectrum.CtrlLabel()
        Me.CtrltxrCust = New Spectrum.CtrlTextBox()
        Me.CtrlTxtOrderNo = New Spectrum.CtrlTextBox()
        Me.BtnSearchCust = New Spectrum.CtrlBtn()
        Me.lblDispDepart = New Spectrum.CtrlLabel()
        Me.lblOrderDate = New Spectrum.CtrlLabel()
        Me.CtrlLabel18 = New Spectrum.CtrlLabel()
        Me.CtrldtOrderDt = New Spectrum.ctrlDate()
        Me.lnkCustDetails = New System.Windows.Forms.LinkLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CtrlLabel5 = New Spectrum.CtrlLabel()
        Me.remarkPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.CtrllblNetAmt = New System.Windows.Forms.Label()
        Me.CtrllblAmtPaid = New System.Windows.Forms.Label()
        Me.CtrllblCreditSale = New System.Windows.Forms.Label()
        Me.CtrllblBaltoPay = New System.Windows.Forms.Label()
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        Me.CtrlLabel2 = New Spectrum.CtrlLabel()
        Me.CtrlLabel3 = New Spectrum.CtrlLabel()
        Me.CtrlLabel17 = New Spectrum.CtrlLabel()
        Me.CtrllblGrossAmt = New System.Windows.Forms.Label()
        Me.CtrllblDiscAmt = New System.Windows.Forms.Label()
        Me.CtrllblTaxAmt = New System.Windows.Forms.Label()
        Me.CtrllblOtherCharges = New System.Windows.Forms.Label()
        Me.CtrllblMinAdv = New System.Windows.Forms.Label()
        Me.CtrlLabel15 = New Spectrum.CtrlLabel()
        Me.ctlDispDisc = New Spectrum.CtrlLabel()
        Me.CtrlLabel10 = New Spectrum.CtrlLabel()
        Me.CtrlLabel4 = New Spectrum.CtrlLabel()
        Me.CtrlLabel6 = New Spectrum.CtrlLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.CtrlLabel7 = New Spectrum.CtrlLabel()
        Me.CtrlLabel8 = New Spectrum.CtrlLabel()
        Me.CtrlLabel9 = New Spectrum.CtrlLabel()
        Me.CtrlLabel12 = New Spectrum.CtrlLabel()
        Me.CtrlLabel11 = New Spectrum.CtrlLabel()
        CType(Me.C1StatusBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlRbn1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.c1SizerGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.c1SizerGrid.SuspendLayout()
        CType(Me.grdScanItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Sizer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer2.SuspendLayout()
        CType(Me.tabSalesOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabSalesOrder.SuspendLayout()
        Me.TabPageAddItems.SuspendLayout()
        Me.TabPageDeliveryLocation.SuspendLayout()
        CType(Me.dgDeliveryLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLblDelivery, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.lblStrRaised, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCompName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDispStrRaised, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSelCust, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDelReq, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDispCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrltxrCust, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlTxtOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDispDepart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOrderDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrldtOrderDt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.CtrlLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ctlDispDisc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.CtrlLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblLoggedIn
        '
        Me.lblLoggedIn.Text = Nothing
        '
        'CtrlRbn1
        '
        Me.CtrlRbn1.ApplicationMenuHolder = Me.RibbonApplicationMenu1
        Me.CtrlRbn1.ConfigToolBarHolder = Me.RibbonConfigToolBar1
        Me.CtrlRbn1.Name = "CtrlRbn1"
        Me.CtrlRbn1.QatHolder = Me.RibbonQat1
        Me.CtrlRbn1.Tabs.Add(Me.rbnTabSO)
        '
        'RibbonApplicationMenu1
        '
        Me.RibbonApplicationMenu1.LargeImage = Global.Spectrum.My.Resources.Resources.logo
        Me.RibbonApplicationMenu1.Name = "RibbonApplicationMenu1"
        '
        'RibbonConfigToolBar1
        '
        Me.RibbonConfigToolBar1.Name = "RibbonConfigToolBar1"
        '
        'RibbonQat1
        '
        Me.RibbonQat1.Name = "RibbonQat1"
        '
        'rbnTabSO
        '
        Me.rbnTabSO.Groups.Add(Me.rbgrpSO)
        Me.rbnTabSO.Groups.Add(Me.rbgrpSaveAndPrint)
        Me.rbnTabSO.Groups.Add(Me.rbnGrpCMPromotion)
        Me.rbnTabSO.Groups.Add(Me.rbnGrpCST)
        Me.rbnTabSO.Groups.Add(Me.rbnGrpAddCombo)
        Me.rbnTabSO.Groups.Add(Me.RibbonGroup2)
        Me.rbnTabSO.Name = "rbnTabSO"
        Me.rbnTabSO.Text = "Sales Order"
        '
        'rbgrpSO
        '
        Me.rbgrpSO.Items.Add(Me.rbbtnSONew)
        Me.rbgrpSO.Items.Add(Me.rbbtnSOEdit)
        Me.rbgrpSO.Items.Add(Me.rbbtnSOCancel)
        Me.rbgrpSO.Name = "rbgrpSO"
        Me.rbgrpSO.Text = "Sales Order Action"
        '
        'rbbtnSONew
        '
        Me.rbbtnSONew.Description = "New Sales Order"
        Me.rbbtnSONew.LargeImage = Global.Spectrum.My.Resources.Resources.sales_order_new
        Me.rbbtnSONew.Name = "rbbtnSONew"
        Me.rbbtnSONew.ShortcutKeyDisplayString = "Ctrl+N"
        Me.rbbtnSONew.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.rbbtnSONew.SmallImage = CType(resources.GetObject("rbbtnSONew.SmallImage"), System.Drawing.Image)
        Me.rbbtnSONew.Text = "New   Sales Order (Ctrl+N)"
        '
        'rbbtnSOEdit
        '
        Me.rbbtnSOEdit.Description = "Update Open Sales Order"
        Me.rbbtnSOEdit.LargeImage = Global.Spectrum.My.Resources.Resources.sales_order_edit
        Me.rbbtnSOEdit.Name = "rbbtnSOEdit"
        Me.rbbtnSOEdit.ShortcutKeyDisplayString = "Ctrl+E"
        Me.rbbtnSOEdit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.rbbtnSOEdit.SmallImage = CType(resources.GetObject("rbbtnSOEdit.SmallImage"), System.Drawing.Image)
        Me.rbbtnSOEdit.Text = "Edit    Sales Order (Ctrl + E)"
        '
        'rbbtnSOCancel
        '
        Me.rbbtnSOCancel.Description = "Cancel Sales Order"
        Me.rbbtnSOCancel.LargeImage = Global.Spectrum.My.Resources.Resources.sales_order_cancel
        Me.rbbtnSOCancel.Name = "rbbtnSOCancel"
        Me.rbbtnSOCancel.ShortcutKeyDisplayString = "Ctrl+X"
        Me.rbbtnSOCancel.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.rbbtnSOCancel.SmallImage = CType(resources.GetObject("rbbtnSOCancel.SmallImage"), System.Drawing.Image)
        Me.rbbtnSOCancel.Text = "Cancel Sales Order(Ctrl+X)"
        '
        'rbgrpSaveAndPrint
        '
        Me.rbgrpSaveAndPrint.Items.Add(Me.rbbtnSave)
        Me.rbgrpSaveAndPrint.Items.Add(Me.rbbtnPrint)
        Me.rbgrpSaveAndPrint.Name = "rbgrpSaveAndPrint"
        Me.rbgrpSaveAndPrint.Text = "Save And Print"
        '
        'rbbtnSave
        '
        Me.rbbtnSave.Description = "Save Sales Order"
        Me.rbbtnSave.LargeImage = CType(resources.GetObject("rbbtnSave.LargeImage"), System.Drawing.Image)
        Me.rbbtnSave.Name = "rbbtnSave"
        Me.rbbtnSave.SmallImage = CType(resources.GetObject("rbbtnSave.SmallImage"), System.Drawing.Image)
        Me.rbbtnSave.Text = "Save   Sales Order(Ctrl+S)"
        '
        'rbbtnPrint
        '
        Me.rbbtnPrint.Description = "Print Sales Order"
        Me.rbbtnPrint.Enabled = False
        Me.rbbtnPrint.LargeImage = CType(resources.GetObject("rbbtnPrint.LargeImage"), System.Drawing.Image)
        Me.rbbtnPrint.Name = "rbbtnPrint"
        Me.rbbtnPrint.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.rbbtnPrint.SmallImage = CType(resources.GetObject("rbbtnPrint.SmallImage"), System.Drawing.Image)
        Me.rbbtnPrint.Text = "Print   Sales Order(Ctrl+R)"
        '
        'rbnGrpCMPromotion
        '
        Me.rbnGrpCMPromotion.Image = CType(resources.GetObject("rbnGrpCMPromotion.Image"), System.Drawing.Image)
        Me.rbnGrpCMPromotion.Items.Add(Me.rbbtnDefaultPromo)
        Me.rbnGrpCMPromotion.Items.Add(Me.rbbtnSelectPromo)
        Me.rbnGrpCMPromotion.Items.Add(Me.rbbtnClrAllPromo)
        Me.rbnGrpCMPromotion.Items.Add(Me.rbbtnClearSelectedPromo)
        Me.rbnGrpCMPromotion.Name = "rbnGrpCMPromotion"
        Me.rbnGrpCMPromotion.Text = "Promotion"
        '
        'rbbtnDefaultPromo
        '
        Me.rbbtnDefaultPromo.LargeImage = CType(resources.GetObject("rbbtnDefaultPromo.LargeImage"), System.Drawing.Image)
        Me.rbbtnDefaultPromo.Name = "rbbtnDefaultPromo"
        Me.rbbtnDefaultPromo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.rbbtnDefaultPromo.Text = "Default Promo (Ctrl+D)"
        '
        'rbbtnSelectPromo
        '
        Me.rbbtnSelectPromo.LargeImage = CType(resources.GetObject("rbbtnSelectPromo.LargeImage"), System.Drawing.Image)
        Me.rbbtnSelectPromo.Name = "rbbtnSelectPromo"
        Me.rbbtnSelectPromo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.rbbtnSelectPromo.Text = "Selected Promo(Ctrl+P)"
        '
        'rbbtnClrAllPromo
        '
        Me.rbbtnClrAllPromo.LargeImage = CType(resources.GetObject("rbbtnClrAllPromo.LargeImage"), System.Drawing.Image)
        Me.rbbtnClrAllPromo.Name = "rbbtnClrAllPromo"
        Me.rbbtnClrAllPromo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.rbbtnClrAllPromo.Text = "Clear Promo (Ctrl+C)"
        '
        'rbbtnClearSelectedPromo
        '
        Me.rbbtnClearSelectedPromo.Description = "Clear Promotion From Selected Item"
        Me.rbbtnClearSelectedPromo.LargeImage = CType(resources.GetObject("rbbtnClearSelectedPromo.LargeImage"), System.Drawing.Image)
        Me.rbbtnClearSelectedPromo.Name = "rbbtnClearSelectedPromo"
        Me.rbbtnClearSelectedPromo.ShortcutKeys = System.Windows.Forms.Keys.F3
        Me.rbbtnClearSelectedPromo.Text = "Clear Selected Item (F3)"
        Me.rbbtnClearSelectedPromo.Visible = False
        '
        'rbnGrpCST
        '
        Me.rbnGrpCST.Items.Add(Me.rbnCST)
        Me.rbnGrpCST.Name = "rbnGrpCST"
        Me.rbnGrpCST.Text = "CST"
        '
        'rbnCST
        '
        Me.rbnCST.Name = "rbnCST"
        Me.rbnCST.Text = "Apply CST"
        '
        'rbnGrpAddCombo
        '
        Me.rbnGrpAddCombo.Items.Add(Me.rbBtnAddCombo)
        Me.rbnGrpAddCombo.Name = "rbnGrpAddCombo"
        Me.rbnGrpAddCombo.Text = "Add Combo"
        '
        'rbBtnAddCombo
        '
        Me.rbBtnAddCombo.LargeImage = Global.Spectrum.My.Resources.Resources.AddCombo
        Me.rbBtnAddCombo.Name = "rbBtnAddCombo"
        Me.rbBtnAddCombo.Text = "Add Combo"
        '
        'RibbonGroup2
        '
        Me.RibbonGroup2.Items.Add(Me.rbBtnRoundOff)
        Me.RibbonGroup2.Name = "RibbonGroup2"
        Me.RibbonGroup2.Text = "Round Off"
        '
        'rbBtnRoundOff
        '
        Me.rbBtnRoundOff.LargeImage = CType(resources.GetObject("rbBtnRoundOff.LargeImage"), System.Drawing.Image)
        Me.rbBtnRoundOff.Name = "rbBtnRoundOff"
        Me.rbBtnRoundOff.Text = "Round Off"
        '
        'c1SizerGrid
        '
        Me.c1SizerGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.c1SizerGrid.Border.Color = System.Drawing.Color.LightSlateGray
        Me.c1SizerGrid.Border.Corners = New C1.Win.C1Sizer.Corners(0, 0, 0, 4)
        Me.c1SizerGrid.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.c1SizerGrid.Controls.Add(Me.grdScanItem)
        Me.c1SizerGrid.Controls.Add(Me.CtrlSalesPersons)
        Me.c1SizerGrid.GridDefinition = "15.2777777777778:False:True;75:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "13.3333333333333:False:True;33.111111" & _
    "1111111:False:False;45.3333333333333:False:False;5.77777777777778:False:True;"
        Me.c1SizerGrid.Location = New System.Drawing.Point(0, 0)
        Me.c1SizerGrid.Name = "c1SizerGrid"
        Me.c1SizerGrid.Size = New System.Drawing.Size(900, 144)
        Me.c1SizerGrid.TabIndex = 0
        Me.c1SizerGrid.Text = "C1Sizer1"
        '
        'grdScanItem
        '
        Me.grdScanItem.CellButtonImage = CType(resources.GetObject("grdScanItem.CellButtonImage"), System.Drawing.Image)
        Me.grdScanItem.ColumnInfo = resources.GetString("grdScanItem.ColumnInfo")
        Me.grdScanItem.ExtendLastCol = True
        Me.grdScanItem.Location = New System.Drawing.Point(5, 31)
        Me.grdScanItem.Name = "grdScanItem"
        Me.grdScanItem.Rows.DefaultSize = 17
        Me.grdScanItem.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox
        Me.grdScanItem.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.grdScanItem.Size = New System.Drawing.Size(890, 108)
        Me.grdScanItem.TabIndex = 1
        Me.grdScanItem.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'CtrlSalesPersons
        '
        Me.CtrlSalesPersons.Location = New System.Drawing.Point(5, 5)
        Me.CtrlSalesPersons.Name = "CtrlSalesPersons"
        Me.CtrlSalesPersons.Size = New System.Drawing.Size(890, 22)
        Me.CtrlSalesPersons.TabIndex = 0
        '
        'C1Sizer2
        '
        Me.C1Sizer2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C1Sizer2.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1Sizer2.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1Sizer2.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1Sizer2.Controls.Add(Me.cmdGenerateSTR)
        Me.C1Sizer2.Controls.Add(Me.BtnSelectDeliveryLoc)
        Me.C1Sizer2.Controls.Add(Me.CtrlBtnSearchCLP)
        Me.C1Sizer2.Controls.Add(Me.CtrlbtnSOOtherCharges)
        Me.C1Sizer2.Controls.Add(Me.CtrlBtnStockCheck)
        Me.C1Sizer2.GridDefinition = resources.GetString("C1Sizer2.GridDefinition")
        Me.C1Sizer2.Location = New System.Drawing.Point(1, 546)
        Me.C1Sizer2.Name = "C1Sizer2"
        Me.C1Sizer2.Size = New System.Drawing.Size(901, 72)
        Me.C1Sizer2.TabIndex = 3
        '
        'cmdGenerateSTR
        '
        Me.cmdGenerateSTR.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdGenerateSTR.Image = Global.Spectrum.My.Resources.Resources.GenerateSTR
        Me.cmdGenerateSTR.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdGenerateSTR.Location = New System.Drawing.Point(515, 5)
        Me.cmdGenerateSTR.Name = "cmdGenerateSTR"
        Me.cmdGenerateSTR.SetArticleCode = Nothing
        Me.cmdGenerateSTR.SetRowIndex = 0
        Me.cmdGenerateSTR.Size = New System.Drawing.Size(129, 62)
        Me.cmdGenerateSTR.TabIndex = 6
        Me.cmdGenerateSTR.Text = "Generate STR"
        Me.cmdGenerateSTR.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdGenerateSTR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdGenerateSTR.UseVisualStyleBackColor = True
        Me.cmdGenerateSTR.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnSelectDeliveryLoc
        '
        Me.BtnSelectDeliveryLoc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSelectDeliveryLoc.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnSelectDeliveryLoc.Location = New System.Drawing.Point(387, 5)
        Me.BtnSelectDeliveryLoc.Name = "BtnSelectDeliveryLoc"
        Me.BtnSelectDeliveryLoc.SetArticleCode = Nothing
        Me.BtnSelectDeliveryLoc.SetRowIndex = 0
        Me.BtnSelectDeliveryLoc.Size = New System.Drawing.Size(124, 62)
        Me.BtnSelectDeliveryLoc.TabIndex = 3
        Me.BtnSelectDeliveryLoc.Text = "Select Delivery Location"
        Me.BtnSelectDeliveryLoc.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnSelectDeliveryLoc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnSelectDeliveryLoc.UseVisualStyleBackColor = True
        Me.BtnSelectDeliveryLoc.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlBtnSearchCLP
        '
        Me.CtrlBtnSearchCLP.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlBtnSearchCLP.Image = Global.Spectrum.My.Resources.Resources.clp
        Me.CtrlBtnSearchCLP.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CtrlBtnSearchCLP.Location = New System.Drawing.Point(256, 5)
        Me.CtrlBtnSearchCLP.Name = "CtrlBtnSearchCLP"
        Me.CtrlBtnSearchCLP.SetArticleCode = Nothing
        Me.CtrlBtnSearchCLP.SetRowIndex = 0
        Me.CtrlBtnSearchCLP.Size = New System.Drawing.Size(126, 62)
        Me.CtrlBtnSearchCLP.TabIndex = 2
        Me.CtrlBtnSearchCLP.Text = "&Loaylty Program"
        Me.CtrlBtnSearchCLP.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CtrlBtnSearchCLP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CtrlBtnSearchCLP.UseVisualStyleBackColor = True
        Me.CtrlBtnSearchCLP.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlbtnSOOtherCharges
        '
        Me.CtrlbtnSOOtherCharges.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlbtnSOOtherCharges.Image = Global.Spectrum.My.Resources.Resources.addcost
        Me.CtrlbtnSOOtherCharges.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CtrlbtnSOOtherCharges.Location = New System.Drawing.Point(132, 5)
        Me.CtrlbtnSOOtherCharges.Name = "CtrlbtnSOOtherCharges"
        Me.CtrlbtnSOOtherCharges.SetArticleCode = Nothing
        Me.CtrlbtnSOOtherCharges.SetRowIndex = 0
        Me.CtrlbtnSOOtherCharges.Size = New System.Drawing.Size(120, 62)
        Me.CtrlbtnSOOtherCharges.TabIndex = 1
        Me.CtrlbtnSOOtherCharges.Text = "&Add Cost"
        Me.CtrlbtnSOOtherCharges.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CtrlbtnSOOtherCharges.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CtrlbtnSOOtherCharges.UseVisualStyleBackColor = True
        Me.CtrlbtnSOOtherCharges.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlBtnStockCheck
        '
        Me.CtrlBtnStockCheck.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlBtnStockCheck.Image = Global.Spectrum.My.Resources.Resources.stock_check
        Me.CtrlBtnStockCheck.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CtrlBtnStockCheck.Location = New System.Drawing.Point(5, 5)
        Me.CtrlBtnStockCheck.Name = "CtrlBtnStockCheck"
        Me.CtrlBtnStockCheck.SetArticleCode = Nothing
        Me.CtrlBtnStockCheck.SetRowIndex = 0
        Me.CtrlBtnStockCheck.Size = New System.Drawing.Size(122, 62)
        Me.CtrlBtnStockCheck.TabIndex = 0
        Me.CtrlBtnStockCheck.Text = "Stock &Check "
        Me.CtrlBtnStockCheck.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CtrlBtnStockCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CtrlBtnStockCheck.UseVisualStyleBackColor = True
        Me.CtrlBtnStockCheck.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'tabSalesOrder
        '
        Me.tabSalesOrder.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabSalesOrder.Controls.Add(Me.TabPageAddItems)
        Me.tabSalesOrder.Controls.Add(Me.TabPageDeliveryLocation)
        Me.tabSalesOrder.Location = New System.Drawing.Point(2, 381)
        Me.tabSalesOrder.Name = "tabSalesOrder"
        Me.tabSalesOrder.SelectedIndex = 4
        Me.tabSalesOrder.Size = New System.Drawing.Size(900, 164)
        Me.tabSalesOrder.TabIndex = 1
        Me.tabSalesOrder.TabsSpacing = 5
        Me.tabSalesOrder.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007
        Me.tabSalesOrder.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Blue
        '
        'TabPageAddItems
        '
        Me.TabPageAddItems.Controls.Add(Me.c1SizerGrid)
        Me.TabPageAddItems.Location = New System.Drawing.Point(1, 24)
        Me.TabPageAddItems.Name = "TabPageAddItems"
        Me.TabPageAddItems.Size = New System.Drawing.Size(898, 139)
        Me.TabPageAddItems.TabIndex = 0
        Me.TabPageAddItems.Text = "Add Items"
        '
        'TabPageDeliveryLocation
        '
        Me.TabPageDeliveryLocation.Controls.Add(Me.dgDeliveryLocation)
        Me.TabPageDeliveryLocation.Location = New System.Drawing.Point(1, 24)
        Me.TabPageDeliveryLocation.Name = "TabPageDeliveryLocation"
        Me.TabPageDeliveryLocation.Size = New System.Drawing.Size(898, 139)
        Me.TabPageDeliveryLocation.TabIndex = 1
        Me.TabPageDeliveryLocation.Text = "Order Details"
        Me.TabPageDeliveryLocation.Visible = False
        '
        'dgDeliveryLocation
        '
        Me.dgDeliveryLocation.CellButtonImage = CType(resources.GetObject("dgDeliveryLocation.CellButtonImage"), System.Drawing.Image)
        Me.dgDeliveryLocation.ColumnInfo = resources.GetString("dgDeliveryLocation.ColumnInfo")
        Me.dgDeliveryLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgDeliveryLocation.ExtendLastCol = True
        Me.dgDeliveryLocation.Location = New System.Drawing.Point(0, 0)
        Me.dgDeliveryLocation.Name = "dgDeliveryLocation"
        Me.dgDeliveryLocation.Rows.DefaultSize = 17
        Me.dgDeliveryLocation.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox
        Me.dgDeliveryLocation.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.dgDeliveryLocation.Size = New System.Drawing.Size(898, 139)
        Me.dgDeliveryLocation.TabIndex = 2
        Me.dgDeliveryLocation.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'CtrlLblDelivery
        '
        Me.CtrlLblDelivery.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlLblDelivery.AttachedTextBoxName = Nothing
        Me.CtrlLblDelivery.AutoSize = True
        Me.CtrlLblDelivery.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLblDelivery.BorderColor = System.Drawing.Color.Transparent
        Me.CtrlLblDelivery.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlLblDelivery.ForeColor = System.Drawing.Color.Black
        Me.CtrlLblDelivery.Location = New System.Drawing.Point(830, 3)
        Me.CtrlLblDelivery.Name = "CtrlLblDelivery"
        Me.CtrlLblDelivery.Size = New System.Drawing.Size(68, 16)
        Me.CtrlLblDelivery.TabIndex = 67
        Me.CtrlLblDelivery.Tag = Nothing
        Me.CtrlLblDelivery.Text = "Delivery"
        Me.CtrlLblDelivery.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.CtrlLblDelivery.TextDetached = True
        Me.CtrlLblDelivery.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 17
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.33088!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.335046!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.335046!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.799836!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.983545!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.953602!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28865!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.142052!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.4319129!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.142052!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.465698!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.55683!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.5433987!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.549277!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.07014!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.067796!))
        Me.TableLayoutPanel1.Controls.Add(Me.rdDelNo, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.lblStrRaised, 2, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.lblCompName, 2, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.rdDelYes, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.lblRemarks, 1, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.lblDispStrRaised, 1, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.lblSelCust, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.lblSalesOrderNo, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.lblDelReq, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.lblDispCompany, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrltxrCust, 2, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlTxtOrderNo, 2, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnSearchCust, 5, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.lblDispDepart, 5, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.lblOrderDate, 5, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel18, 7, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrldtOrderDt, 7, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.lnkCustDetails, 6, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.remarkPanel, 2, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrllblNetAmt, 15, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrllblAmtPaid, 15, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrllblCreditSale, 15, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrllblBaltoPay, 15, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel1, 14, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel2, 14, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel3, 14, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel17, 14, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrllblGrossAmt, 12, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrllblDiscAmt, 12, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrllblTaxAmt, 12, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrllblOtherCharges, 12, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrllblMinAdv, 12, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel15, 11, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.ctlDispDisc, 11, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel10, 11, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel4, 11, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel6, 11, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 11, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel8, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel9, 9, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel12, 10, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel11, 1, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(4, 160)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 10
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.74637!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.479346!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.295984!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.99711!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.99711!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.99711!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.99711!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.99711!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.49276!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(895, 215)
        Me.TableLayoutPanel1.TabIndex = 71
        '
        'rdDelNo
        '
        Me.rdDelNo.AutoSize = True
        Me.rdDelNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdDelNo.Location = New System.Drawing.Point(175, 38)
        Me.rdDelNo.Name = "rdDelNo"
        Me.rdDelNo.Size = New System.Drawing.Size(67, 16)
        Me.rdDelNo.TabIndex = 79
        Me.rdDelNo.Text = "No"
        Me.rdDelNo.UseVisualStyleBackColor = True
        '
        'lblStrRaised
        '
        Me.lblStrRaised.AttachedTextBoxName = Nothing
        Me.lblStrRaised.AutoSize = True
        Me.lblStrRaised.BackColor = System.Drawing.Color.Transparent
        Me.lblStrRaised.BorderColor = System.Drawing.Color.Transparent
        Me.lblStrRaised.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblStrRaised.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStrRaised.ForeColor = System.Drawing.Color.Black
        Me.lblStrRaised.Location = New System.Drawing.Point(102, 123)
        Me.lblStrRaised.Name = "lblStrRaised"
        Me.lblStrRaised.Size = New System.Drawing.Size(67, 22)
        Me.lblStrRaised.TabIndex = 85
        Me.lblStrRaised.Tag = Nothing
        Me.lblStrRaised.Text = "Yes"
        Me.lblStrRaised.TextDetached = True
        Me.lblStrRaised.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCompName
        '
        Me.lblCompName.AttachedTextBoxName = Nothing
        Me.lblCompName.AutoSize = True
        Me.lblCompName.BackColor = System.Drawing.Color.Transparent
        Me.lblCompName.BorderColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.SetColumnSpan(Me.lblCompName, 2)
        Me.lblCompName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCompName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompName.ForeColor = System.Drawing.Color.Black
        Me.lblCompName.Location = New System.Drawing.Point(102, 79)
        Me.lblCompName.Name = "lblCompName"
        Me.lblCompName.Size = New System.Drawing.Size(140, 22)
        Me.lblCompName.TabIndex = 89
        Me.lblCompName.Tag = Nothing
        Me.lblCompName.Text = "Prashant Corner"
        Me.lblCompName.TextDetached = True
        Me.lblCompName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'rdDelYes
        '
        Me.rdDelYes.AutoSize = True
        Me.rdDelYes.Checked = True
        Me.rdDelYes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdDelYes.Location = New System.Drawing.Point(102, 38)
        Me.rdDelYes.Name = "rdDelYes"
        Me.rdDelYes.Size = New System.Drawing.Size(67, 16)
        Me.rdDelYes.TabIndex = 78
        Me.rdDelYes.TabStop = True
        Me.rdDelYes.Text = "Yes"
        Me.rdDelYes.UseVisualStyleBackColor = True
        '
        'lblRemarks
        '
        Me.lblRemarks.AttachedTextBoxName = Nothing
        Me.lblRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRemarks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRemarks.ForeColor = System.Drawing.Color.Black
        Me.lblRemarks.Location = New System.Drawing.Point(9, 146)
        Me.lblRemarks.Margin = New System.Windows.Forms.Padding(1)
        Me.lblRemarks.MaximumSize = New System.Drawing.Size(200, 21)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(89, 21)
        Me.lblRemarks.TabIndex = 76
        Me.lblRemarks.Tag = Nothing
        Me.lblRemarks.Text = "Remarks"
        Me.lblRemarks.TextDetached = True
        '
        'lblDispStrRaised
        '
        Me.lblDispStrRaised.AttachedTextBoxName = Nothing
        Me.lblDispStrRaised.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblDispStrRaised.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDispStrRaised.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDispStrRaised.ForeColor = System.Drawing.Color.Black
        Me.lblDispStrRaised.Location = New System.Drawing.Point(9, 124)
        Me.lblDispStrRaised.Margin = New System.Windows.Forms.Padding(1)
        Me.lblDispStrRaised.Name = "lblDispStrRaised"
        Me.lblDispStrRaised.Size = New System.Drawing.Size(89, 20)
        Me.lblDispStrRaised.TabIndex = 85
        Me.lblDispStrRaised.Tag = Nothing
        Me.lblDispStrRaised.Text = "STR Raised:"
        Me.lblDispStrRaised.TextDetached = True
        '
        'lblSelCust
        '
        Me.lblSelCust.AttachedTextBoxName = Nothing
        Me.lblSelCust.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblSelCust.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSelCust.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSelCust.ForeColor = System.Drawing.Color.Black
        Me.lblSelCust.Location = New System.Drawing.Point(9, 58)
        Me.lblSelCust.Margin = New System.Windows.Forms.Padding(1)
        Me.lblSelCust.Name = "lblSelCust"
        Me.lblSelCust.Size = New System.Drawing.Size(89, 20)
        Me.lblSelCust.TabIndex = 88
        Me.lblSelCust.Tag = Nothing
        Me.lblSelCust.Text = "Select customer:"
        Me.lblSelCust.TextDetached = True
        '
        'lblSalesOrderNo
        '
        Me.lblSalesOrderNo.AttachedTextBoxName = Nothing
        Me.lblSalesOrderNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblSalesOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSalesOrderNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSalesOrderNo.ForeColor = System.Drawing.Color.Black
        Me.lblSalesOrderNo.Location = New System.Drawing.Point(9, 102)
        Me.lblSalesOrderNo.Margin = New System.Windows.Forms.Padding(1)
        Me.lblSalesOrderNo.Name = "lblSalesOrderNo"
        Me.lblSalesOrderNo.Size = New System.Drawing.Size(89, 20)
        Me.lblSalesOrderNo.TabIndex = 84
        Me.lblSalesOrderNo.Tag = Nothing
        Me.lblSalesOrderNo.Text = "Order No."
        Me.lblSalesOrderNo.TextDetached = True
        '
        'lblDelReq
        '
        Me.lblDelReq.AttachedTextBoxName = Nothing
        Me.lblDelReq.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblDelReq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDelReq.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDelReq.ForeColor = System.Drawing.Color.Black
        Me.lblDelReq.Location = New System.Drawing.Point(9, 36)
        Me.lblDelReq.Margin = New System.Windows.Forms.Padding(1)
        Me.lblDelReq.Name = "lblDelReq"
        Me.lblDelReq.Size = New System.Drawing.Size(89, 20)
        Me.lblDelReq.TabIndex = 77
        Me.lblDelReq.Tag = Nothing
        Me.lblDelReq.Text = "Delivery required:"
        Me.lblDelReq.TextDetached = True
        '
        'lblDispCompany
        '
        Me.lblDispCompany.AttachedTextBoxName = Nothing
        Me.lblDispCompany.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblDispCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDispCompany.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDispCompany.ForeColor = System.Drawing.Color.Black
        Me.lblDispCompany.Location = New System.Drawing.Point(9, 80)
        Me.lblDispCompany.Margin = New System.Windows.Forms.Padding(1)
        Me.lblDispCompany.Name = "lblDispCompany"
        Me.lblDispCompany.Size = New System.Drawing.Size(89, 20)
        Me.lblDispCompany.TabIndex = 81
        Me.lblDispCompany.Tag = Nothing
        Me.lblDispCompany.Text = "Company:"
        Me.lblDispCompany.TextDetached = True
        '
        'CtrltxrCust
        '
        Me.CtrltxrCust.AutoSize = False
        Me.CtrltxrCust.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.CtrltxrCust.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel1.SetColumnSpan(Me.CtrltxrCust, 2)
        Me.CtrltxrCust.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrltxrCust.Location = New System.Drawing.Point(100, 58)
        Me.CtrltxrCust.Margin = New System.Windows.Forms.Padding(1)
        Me.CtrltxrCust.MinimumSize = New System.Drawing.Size(10, 21)
        Me.CtrltxrCust.Name = "CtrltxrCust"
        Me.CtrltxrCust.Size = New System.Drawing.Size(144, 21)
        Me.CtrltxrCust.TabIndex = 86
        Me.CtrltxrCust.Tag = "NO"
        Me.CtrltxrCust.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrltxrCust.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlTxtOrderNo
        '
        Me.CtrlTxtOrderNo.AutoSize = False
        Me.CtrlTxtOrderNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.CtrlTxtOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel1.SetColumnSpan(Me.CtrlTxtOrderNo, 2)
        Me.CtrlTxtOrderNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlTxtOrderNo.Location = New System.Drawing.Point(100, 102)
        Me.CtrlTxtOrderNo.Margin = New System.Windows.Forms.Padding(1)
        Me.CtrlTxtOrderNo.MinimumSize = New System.Drawing.Size(10, 21)
        Me.CtrlTxtOrderNo.Name = "CtrlTxtOrderNo"
        Me.CtrlTxtOrderNo.Size = New System.Drawing.Size(144, 21)
        Me.CtrlTxtOrderNo.TabIndex = 71
        Me.CtrlTxtOrderNo.Tag = "NO"
        Me.CtrlTxtOrderNo.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlTxtOrderNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnSearchCust
        '
        Me.BtnSearchCust.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnSearchCust.Image = Global.Spectrum.My.Resources.Resources.search_2
        Me.BtnSearchCust.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnSearchCust.Location = New System.Drawing.Point(255, 60)
        Me.BtnSearchCust.Name = "BtnSearchCust"
        Me.BtnSearchCust.SetArticleCode = Nothing
        Me.BtnSearchCust.SetRowIndex = 0
        Me.BtnSearchCust.Size = New System.Drawing.Size(29, 16)
        Me.BtnSearchCust.TabIndex = 87
        Me.BtnSearchCust.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnSearchCust.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnSearchCust.UseVisualStyleBackColor = True
        Me.BtnSearchCust.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblDispDepart
        '
        Me.lblDispDepart.AttachedTextBoxName = Nothing
        Me.lblDispDepart.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblDispDepart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel1.SetColumnSpan(Me.lblDispDepart, 2)
        Me.lblDispDepart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDispDepart.ForeColor = System.Drawing.Color.Black
        Me.lblDispDepart.Location = New System.Drawing.Point(253, 80)
        Me.lblDispDepart.Margin = New System.Windows.Forms.Padding(1)
        Me.lblDispDepart.Name = "lblDispDepart"
        Me.lblDispDepart.Size = New System.Drawing.Size(85, 20)
        Me.lblDispDepart.TabIndex = 84
        Me.lblDispDepart.Tag = Nothing
        Me.lblDispDepart.Text = "Department:"
        Me.lblDispDepart.TextDetached = True
        '
        'lblOrderDate
        '
        Me.lblOrderDate.AttachedTextBoxName = Nothing
        Me.lblOrderDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblOrderDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel1.SetColumnSpan(Me.lblOrderDate, 2)
        Me.lblOrderDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOrderDate.ForeColor = System.Drawing.Color.Black
        Me.lblOrderDate.Location = New System.Drawing.Point(253, 102)
        Me.lblOrderDate.Margin = New System.Windows.Forms.Padding(1)
        Me.lblOrderDate.Name = "lblOrderDate"
        Me.lblOrderDate.Size = New System.Drawing.Size(85, 20)
        Me.lblOrderDate.TabIndex = 83
        Me.lblOrderDate.Tag = Nothing
        Me.lblOrderDate.Text = "Order Date"
        Me.lblOrderDate.TextDetached = True
        '
        'CtrlLabel18
        '
        Me.CtrlLabel18.AttachedTextBoxName = Nothing
        Me.CtrlLabel18.AutoSize = True
        Me.CtrlLabel18.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel18.BorderColor = System.Drawing.Color.Transparent
        Me.CtrlLabel18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlLabel18.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlLabel18.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel18.Location = New System.Drawing.Point(342, 79)
        Me.CtrlLabel18.Name = "CtrlLabel18"
        Me.CtrlLabel18.Size = New System.Drawing.Size(120, 22)
        Me.CtrlLabel18.TabIndex = 83
        Me.CtrlLabel18.Tag = Nothing
        Me.CtrlLabel18.Text = "-"
        Me.CtrlLabel18.TextDetached = True
        Me.CtrlLabel18.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrldtOrderDt
        '
        Me.CtrldtOrderDt.AutoSize = False
        Me.CtrldtOrderDt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.CtrldtOrderDt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        '
        '
        '
        Me.CtrldtOrderDt.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.CtrldtOrderDt.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrldtOrderDt.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrldtOrderDt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrldtOrderDt.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.CtrldtOrderDt.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.CtrldtOrderDt.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.CtrldtOrderDt.Location = New System.Drawing.Point(340, 102)
        Me.CtrldtOrderDt.Margin = New System.Windows.Forms.Padding(1)
        Me.CtrldtOrderDt.Name = "CtrldtOrderDt"
        Me.CtrldtOrderDt.Size = New System.Drawing.Size(124, 20)
        Me.CtrldtOrderDt.TabIndex = 82
        Me.CtrldtOrderDt.Tag = Nothing
        Me.CtrldtOrderDt.TrimStart = True
        Me.CtrldtOrderDt.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        Me.CtrldtOrderDt.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrldtOrderDt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lnkCustDetails
        '
        Me.lnkCustDetails.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.lnkCustDetails, 2)
        Me.lnkCustDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lnkCustDetails.Location = New System.Drawing.Point(290, 57)
        Me.lnkCustDetails.Name = "lnkCustDetails"
        Me.lnkCustDetails.Size = New System.Drawing.Size(172, 22)
        Me.lnkCustDetails.TabIndex = 80
        Me.lnkCustDetails.TabStop = True
        Me.lnkCustDetails.Text = "View Details"
        '
        'Panel1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.Panel1, 7)
        Me.Panel1.Controls.Add(Me.CtrlLabel5)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(11, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(451, 22)
        Me.Panel1.TabIndex = 78
        '
        'CtrlLabel5
        '
        Me.CtrlLabel5.AttachedTextBoxName = Nothing
        Me.CtrlLabel5.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel5.BorderColor = System.Drawing.Color.White
        Me.CtrlLabel5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlLabel5.ForeColor = System.Drawing.Color.Red
        Me.CtrlLabel5.Location = New System.Drawing.Point(212, 3)
        Me.CtrlLabel5.Name = "CtrlLabel5"
        Me.CtrlLabel5.Size = New System.Drawing.Size(115, 19)
        Me.CtrlLabel5.TabIndex = 102
        Me.CtrlLabel5.Tag = Nothing
        Me.CtrlLabel5.Text = "Order Details"
        Me.CtrlLabel5.TextDetached = True
        Me.CtrlLabel5.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'remarkPanel
        '
        Me.remarkPanel.AutoScroll = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.remarkPanel, 6)
        Me.remarkPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.remarkPanel.Location = New System.Drawing.Point(102, 148)
        Me.remarkPanel.Name = "remarkPanel"
        Me.remarkPanel.Size = New System.Drawing.Size(360, 50)
        Me.remarkPanel.TabIndex = 111
        '
        'CtrllblNetAmt
        '
        Me.CtrllblNetAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrllblNetAmt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrllblNetAmt.Location = New System.Drawing.Point(748, 37)
        Me.CtrllblNetAmt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CtrllblNetAmt.Name = "CtrllblNetAmt"
        Me.CtrllblNetAmt.Size = New System.Drawing.Size(127, 18)
        Me.CtrllblNetAmt.TabIndex = 103
        Me.CtrllblNetAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CtrllblAmtPaid
        '
        Me.CtrllblAmtPaid.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrllblAmtPaid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrllblAmtPaid.Location = New System.Drawing.Point(748, 59)
        Me.CtrllblAmtPaid.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CtrllblAmtPaid.Name = "CtrllblAmtPaid"
        Me.CtrllblAmtPaid.Size = New System.Drawing.Size(127, 18)
        Me.CtrllblAmtPaid.TabIndex = 104
        Me.CtrllblAmtPaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CtrllblCreditSale
        '
        Me.CtrllblCreditSale.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrllblCreditSale.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrllblCreditSale.Location = New System.Drawing.Point(748, 81)
        Me.CtrllblCreditSale.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CtrllblCreditSale.Name = "CtrllblCreditSale"
        Me.CtrllblCreditSale.Size = New System.Drawing.Size(127, 18)
        Me.CtrllblCreditSale.TabIndex = 107
        Me.CtrllblCreditSale.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CtrllblBaltoPay
        '
        Me.CtrllblBaltoPay.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrllblBaltoPay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrllblBaltoPay.Location = New System.Drawing.Point(748, 103)
        Me.CtrllblBaltoPay.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CtrllblBaltoPay.Name = "CtrllblBaltoPay"
        Me.CtrllblBaltoPay.Size = New System.Drawing.Size(127, 18)
        Me.CtrllblBaltoPay.TabIndex = 108
        Me.CtrllblBaltoPay.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CtrlLabel1
        '
        Me.CtrlLabel1.AttachedTextBoxName = Nothing
        Me.CtrlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlLabel1.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel1.Location = New System.Drawing.Point(699, 101)
        Me.CtrlLabel1.Name = "CtrlLabel1"
        Me.CtrlLabel1.Size = New System.Drawing.Size(43, 22)
        Me.CtrlLabel1.TabIndex = 107
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.Text = "Bal to Pay:"
        Me.CtrlLabel1.TextDetached = True
        Me.CtrlLabel1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel2
        '
        Me.CtrlLabel2.AttachedTextBoxName = Nothing
        Me.CtrlLabel2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlLabel2.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel2.Location = New System.Drawing.Point(699, 79)
        Me.CtrlLabel2.Name = "CtrlLabel2"
        Me.CtrlLabel2.Size = New System.Drawing.Size(43, 22)
        Me.CtrlLabel2.TabIndex = 106
        Me.CtrlLabel2.Tag = Nothing
        Me.CtrlLabel2.Text = "Credit Sale:"
        Me.CtrlLabel2.TextDetached = True
        Me.CtrlLabel2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel3
        '
        Me.CtrlLabel3.AttachedTextBoxName = Nothing
        Me.CtrlLabel3.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlLabel3.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel3.Location = New System.Drawing.Point(699, 57)
        Me.CtrlLabel3.Name = "CtrlLabel3"
        Me.CtrlLabel3.Size = New System.Drawing.Size(43, 22)
        Me.CtrlLabel3.TabIndex = 103
        Me.CtrlLabel3.Tag = Nothing
        Me.CtrlLabel3.Text = "Amt Paid:"
        Me.CtrlLabel3.TextDetached = True
        Me.CtrlLabel3.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel17
        '
        Me.CtrlLabel17.AttachedTextBoxName = Nothing
        Me.CtrlLabel17.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel17.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlLabel17.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel17.Location = New System.Drawing.Point(699, 35)
        Me.CtrlLabel17.Name = "CtrlLabel17"
        Me.CtrlLabel17.Size = New System.Drawing.Size(43, 22)
        Me.CtrlLabel17.TabIndex = 102
        Me.CtrlLabel17.Tag = Nothing
        Me.CtrlLabel17.Text = "Net Amt:"
        Me.CtrlLabel17.TextDetached = True
        Me.CtrlLabel17.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrllblGrossAmt
        '
        Me.CtrllblGrossAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrllblGrossAmt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrllblGrossAmt.Location = New System.Drawing.Point(566, 37)
        Me.CtrllblGrossAmt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CtrllblGrossAmt.Name = "CtrllblGrossAmt"
        Me.CtrllblGrossAmt.Size = New System.Drawing.Size(123, 18)
        Me.CtrllblGrossAmt.TabIndex = 95
        Me.CtrllblGrossAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CtrllblDiscAmt
        '
        Me.CtrllblDiscAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrllblDiscAmt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrllblDiscAmt.Location = New System.Drawing.Point(566, 59)
        Me.CtrllblDiscAmt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CtrllblDiscAmt.Name = "CtrllblDiscAmt"
        Me.CtrllblDiscAmt.Size = New System.Drawing.Size(123, 18)
        Me.CtrllblDiscAmt.TabIndex = 97
        Me.CtrllblDiscAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CtrllblTaxAmt
        '
        Me.CtrllblTaxAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrllblTaxAmt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrllblTaxAmt.Location = New System.Drawing.Point(566, 81)
        Me.CtrllblTaxAmt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CtrllblTaxAmt.Name = "CtrllblTaxAmt"
        Me.CtrllblTaxAmt.Size = New System.Drawing.Size(123, 18)
        Me.CtrllblTaxAmt.TabIndex = 98
        Me.CtrllblTaxAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CtrllblOtherCharges
        '
        Me.CtrllblOtherCharges.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrllblOtherCharges.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrllblOtherCharges.Location = New System.Drawing.Point(566, 103)
        Me.CtrllblOtherCharges.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CtrllblOtherCharges.Name = "CtrllblOtherCharges"
        Me.CtrllblOtherCharges.Size = New System.Drawing.Size(123, 18)
        Me.CtrllblOtherCharges.TabIndex = 102
        Me.CtrllblOtherCharges.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CtrllblMinAdv
        '
        Me.CtrllblMinAdv.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrllblMinAdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrllblMinAdv.Location = New System.Drawing.Point(566, 125)
        Me.CtrllblMinAdv.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CtrllblMinAdv.Name = "CtrllblMinAdv"
        Me.CtrllblMinAdv.Size = New System.Drawing.Size(123, 18)
        Me.CtrllblMinAdv.TabIndex = 101
        Me.CtrllblMinAdv.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CtrlLabel15
        '
        Me.CtrlLabel15.AttachedTextBoxName = Nothing
        Me.CtrlLabel15.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel15.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlLabel15.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel15.Location = New System.Drawing.Point(509, 35)
        Me.CtrlLabel15.Name = "CtrlLabel15"
        Me.CtrlLabel15.Size = New System.Drawing.Size(51, 22)
        Me.CtrlLabel15.TabIndex = 100
        Me.CtrlLabel15.Tag = Nothing
        Me.CtrlLabel15.Text = "Gross Amt:"
        Me.CtrlLabel15.TextDetached = True
        Me.CtrlLabel15.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'ctlDispDisc
        '
        Me.ctlDispDisc.AttachedTextBoxName = Nothing
        Me.ctlDispDisc.BackColor = System.Drawing.Color.Transparent
        Me.ctlDispDisc.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ctlDispDisc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ctlDispDisc.ForeColor = System.Drawing.Color.Black
        Me.ctlDispDisc.Location = New System.Drawing.Point(509, 57)
        Me.ctlDispDisc.Name = "ctlDispDisc"
        Me.ctlDispDisc.Size = New System.Drawing.Size(51, 22)
        Me.ctlDispDisc.TabIndex = 101
        Me.ctlDispDisc.Tag = Nothing
        Me.ctlDispDisc.Text = "Discount Amt:"
        Me.ctlDispDisc.TextDetached = True
        Me.ctlDispDisc.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel10
        '
        Me.CtrlLabel10.AttachedTextBoxName = Nothing
        Me.CtrlLabel10.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel10.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlLabel10.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel10.Location = New System.Drawing.Point(509, 79)
        Me.CtrlLabel10.Name = "CtrlLabel10"
        Me.CtrlLabel10.Size = New System.Drawing.Size(51, 22)
        Me.CtrlLabel10.TabIndex = 102
        Me.CtrlLabel10.Tag = Nothing
        Me.CtrlLabel10.Text = "Tax Amt:"
        Me.CtrlLabel10.TextDetached = True
        Me.CtrlLabel10.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel4
        '
        Me.CtrlLabel4.AttachedTextBoxName = Nothing
        Me.CtrlLabel4.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlLabel4.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel4.Location = New System.Drawing.Point(509, 101)
        Me.CtrlLabel4.Name = "CtrlLabel4"
        Me.CtrlLabel4.Size = New System.Drawing.Size(51, 22)
        Me.CtrlLabel4.TabIndex = 103
        Me.CtrlLabel4.Tag = Nothing
        Me.CtrlLabel4.Text = "Other Charges:"
        Me.CtrlLabel4.TextDetached = True
        Me.CtrlLabel4.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel6
        '
        Me.CtrlLabel6.AttachedTextBoxName = Nothing
        Me.CtrlLabel6.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel6.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlLabel6.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel6.Location = New System.Drawing.Point(509, 123)
        Me.CtrlLabel6.Name = "CtrlLabel6"
        Me.CtrlLabel6.Size = New System.Drawing.Size(51, 22)
        Me.CtrlLabel6.TabIndex = 98
        Me.CtrlLabel6.Tag = Nothing
        Me.CtrlLabel6.Text = "Min Adv Amt:"
        Me.CtrlLabel6.TextDetached = True
        Me.CtrlLabel6.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'Panel2
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.Panel2, 5)
        Me.Panel2.Controls.Add(Me.CtrlLabel7)
        Me.Panel2.Location = New System.Drawing.Point(509, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(354, 22)
        Me.Panel2.TabIndex = 110
        '
        'CtrlLabel7
        '
        Me.CtrlLabel7.AttachedTextBoxName = Nothing
        Me.CtrlLabel7.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel7.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlLabel7.ForeColor = System.Drawing.Color.Red
        Me.CtrlLabel7.Location = New System.Drawing.Point(222, 2)
        Me.CtrlLabel7.Name = "CtrlLabel7"
        Me.CtrlLabel7.Size = New System.Drawing.Size(126, 20)
        Me.CtrlLabel7.TabIndex = 103
        Me.CtrlLabel7.Tag = Nothing
        Me.CtrlLabel7.Text = "Payment Summary"
        Me.CtrlLabel7.TextDetached = True
        Me.CtrlLabel7.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel8
        '
        Me.CtrlLabel8.AttachedTextBoxName = Nothing
        Me.CtrlLabel8.BackColor = System.Drawing.Color.Black
        Me.CtrlLabel8.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel8.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel8.Location = New System.Drawing.Point(1, 1)
        Me.CtrlLabel8.Margin = New System.Windows.Forms.Padding(1)
        Me.CtrlLabel8.Name = "CtrlLabel8"
        Me.TableLayoutPanel1.SetRowSpan(Me.CtrlLabel8, 9)
        Me.CtrlLabel8.Size = New System.Drawing.Size(2, 199)
        Me.CtrlLabel8.TabIndex = 104
        Me.CtrlLabel8.Tag = Nothing
        Me.CtrlLabel8.TextDetached = True
        Me.CtrlLabel8.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel9
        '
        Me.CtrlLabel9.AttachedTextBoxName = Nothing
        Me.CtrlLabel9.BackColor = System.Drawing.Color.Black
        Me.CtrlLabel9.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlLabel9.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel9.Location = New System.Drawing.Point(485, 1)
        Me.CtrlLabel9.Margin = New System.Windows.Forms.Padding(1)
        Me.CtrlLabel9.Name = "CtrlLabel9"
        Me.TableLayoutPanel1.SetRowSpan(Me.CtrlLabel9, 9)
        Me.CtrlLabel9.Size = New System.Drawing.Size(1, 199)
        Me.CtrlLabel9.TabIndex = 114
        Me.CtrlLabel9.Tag = Nothing
        Me.CtrlLabel9.TextDetached = True
        Me.CtrlLabel9.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel12
        '
        Me.CtrlLabel12.AttachedTextBoxName = Nothing
        Me.CtrlLabel12.BackColor = System.Drawing.Color.Black
        Me.CtrlLabel12.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel1.SetColumnSpan(Me.CtrlLabel12, 7)
        Me.CtrlLabel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlLabel12.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel12.Location = New System.Drawing.Point(488, 29)
        Me.CtrlLabel12.Margin = New System.Windows.Forms.Padding(1)
        Me.CtrlLabel12.Name = "CtrlLabel12"
        Me.CtrlLabel12.Size = New System.Drawing.Size(406, 3)
        Me.CtrlLabel12.TabIndex = 116
        Me.CtrlLabel12.Tag = Nothing
        Me.CtrlLabel12.TextDetached = True
        Me.CtrlLabel12.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel11
        '
        Me.CtrlLabel11.AttachedTextBoxName = Nothing
        Me.CtrlLabel11.BackColor = System.Drawing.Color.Black
        Me.CtrlLabel11.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel1.SetColumnSpan(Me.CtrlLabel11, 8)
        Me.CtrlLabel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlLabel11.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel11.Location = New System.Drawing.Point(9, 29)
        Me.CtrlLabel11.Margin = New System.Windows.Forms.Padding(1)
        Me.CtrlLabel11.Name = "CtrlLabel11"
        Me.CtrlLabel11.Size = New System.Drawing.Size(474, 3)
        Me.CtrlLabel11.TabIndex = 115
        Me.CtrlLabel11.Tag = Nothing
        Me.CtrlLabel11.TextDetached = True
        Me.CtrlLabel11.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmPCSalesOrderCreation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(902, 643)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.CtrlLblDelivery)
        Me.Controls.Add(Me.C1Sizer2)
        Me.Controls.Add(Me.tabSalesOrder)
        Me.Controls.Add(Me.CtrlRbn1)
        Me.IsDocToParent = True
        Me.MinimumSize = New System.Drawing.Size(798, 568)
        Me.Name = "frmPCSalesOrderCreation"
        Me.Text = "Create Sales Order"
        Me.Controls.SetChildIndex(Me.CtrlRbn1, 0)
        Me.Controls.SetChildIndex(Me.tabSalesOrder, 0)
        Me.Controls.SetChildIndex(Me.C1Sizer2, 0)
        Me.Controls.SetChildIndex(Me.CtrlLblDelivery, 0)
        Me.Controls.SetChildIndex(Me.C1StatusBar1, 0)
        Me.Controls.SetChildIndex(Me.TableLayoutPanel1, 0)
        CType(Me.C1StatusBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlRbn1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.c1SizerGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.c1SizerGrid.ResumeLayout(False)
        CType(Me.grdScanItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Sizer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer2.ResumeLayout(False)
        CType(Me.tabSalesOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabSalesOrder.ResumeLayout(False)
        Me.TabPageAddItems.ResumeLayout(False)
        Me.TabPageDeliveryLocation.ResumeLayout(False)
        CType(Me.dgDeliveryLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLblDelivery, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.lblStrRaised, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCompName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDispStrRaised, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSelCust, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDelReq, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDispCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrltxrCust, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlTxtOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDispDepart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOrderDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrldtOrderDt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.CtrlLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ctlDispDisc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.CtrlLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CtrlRbn1 As Spectrum.CtrlRbn
    Friend WithEvents RibbonApplicationMenu1 As C1.Win.C1Ribbon.RibbonApplicationMenu
    Friend WithEvents RibbonConfigToolBar1 As C1.Win.C1Ribbon.RibbonConfigToolBar
    Friend WithEvents RibbonQat1 As C1.Win.C1Ribbon.RibbonQat
    Friend WithEvents rbnTabSO As C1.Win.C1Ribbon.RibbonTab
    Friend WithEvents c1SizerGrid As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents grdScanItem As Spectrum.CtrlGrid
    Friend WithEvents CtrlSalesPersons As Spectrum.CtrlSalesPerson
    Friend WithEvents rbgrpSO As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents rbbtnSONew As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbbtnSOEdit As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbbtnSOCancel As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbgrpSaveAndPrint As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents rbbtnSave As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbbtnPrint As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbnGrpCMPromotion As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents rbbtnSelectPromo As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbbtnClrAllPromo As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbbtnClearSelectedPromo As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbbtnDefaultPromo As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents C1Sizer2 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents CtrlbtnSOOtherCharges As Spectrum.CtrlBtn
    Friend WithEvents CtrlBtnStockCheck As Spectrum.CtrlBtn
    Friend WithEvents CtrlBtnSearchCLP As Spectrum.CtrlBtn
    Friend WithEvents tabSalesOrder As Spectrum.CtrlTab
    Friend WithEvents TabPageAddItems As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents TabPageDeliveryLocation As C1.Win.C1Command.C1DockingTabPage

    Friend WithEvents BtnSelectDeliveryLoc As Spectrum.CtrlBtn

    Friend WithEvents rbnGrpCST As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents rbnCST As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbnGrpAddCombo As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents rbBtnAddCombo As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents cmdGenerateSTR As Spectrum.CtrlBtn
    Friend WithEvents RibbonGroup2 As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents rbBtnRoundOff As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents CtrlLblDelivery As Spectrum.CtrlLabel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents rdDelNo As System.Windows.Forms.RadioButton
    Friend WithEvents lblStrRaised As Spectrum.CtrlLabel
    Friend WithEvents lblCompName As Spectrum.CtrlLabel
    Friend WithEvents rdDelYes As System.Windows.Forms.RadioButton
    Friend WithEvents lblRemarks As Spectrum.CtrlLabel
    Friend WithEvents lblDispStrRaised As Spectrum.CtrlLabel
    Friend WithEvents lblSelCust As Spectrum.CtrlLabel
    Friend WithEvents lblSalesOrderNo As Spectrum.CtrlLabel
    Friend WithEvents lblDelReq As Spectrum.CtrlLabel
    Friend WithEvents lblDispCompany As Spectrum.CtrlLabel
    Friend WithEvents CtrltxrCust As Spectrum.CtrlTextBox
    Friend WithEvents CtrlTxtOrderNo As Spectrum.CtrlTextBox
    Friend WithEvents BtnSearchCust As Spectrum.CtrlBtn
    Friend WithEvents lblDispDepart As Spectrum.CtrlLabel
    Friend WithEvents lblOrderDate As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel18 As Spectrum.CtrlLabel
    Friend WithEvents CtrldtOrderDt As Spectrum.ctrlDate
    Friend WithEvents lnkCustDetails As System.Windows.Forms.LinkLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CtrlLabel5 As Spectrum.CtrlLabel
    Friend WithEvents remarkPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents CtrllblNetAmt As System.Windows.Forms.Label
    Friend WithEvents CtrllblAmtPaid As System.Windows.Forms.Label
    Friend WithEvents CtrllblCreditSale As System.Windows.Forms.Label
    Friend WithEvents CtrllblBaltoPay As System.Windows.Forms.Label
    Friend WithEvents CtrlLabel1 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel2 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel3 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel17 As Spectrum.CtrlLabel
    Friend WithEvents CtrllblGrossAmt As System.Windows.Forms.Label
    Friend WithEvents CtrllblDiscAmt As System.Windows.Forms.Label
    Friend WithEvents CtrllblTaxAmt As System.Windows.Forms.Label
    Friend WithEvents CtrllblOtherCharges As System.Windows.Forms.Label
    Friend WithEvents CtrllblMinAdv As System.Windows.Forms.Label
    Friend WithEvents CtrlLabel15 As Spectrum.CtrlLabel
    Friend WithEvents ctlDispDisc As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel10 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel4 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel6 As Spectrum.CtrlLabel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents CtrlLabel7 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel8 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel9 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel12 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel11 As Spectrum.CtrlLabel
    Friend WithEvents dgDeliveryLocation As Spectrum.CtrlGrid

End Class
