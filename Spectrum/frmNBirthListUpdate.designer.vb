<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNBirthListUpdate
    Inherits CtrlRbnBaseForm

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNBirthListUpdate))
        Me.CtrlRbn1 = New Spectrum.CtrlRbn()
        Me.RibbonApplicationMenu1 = New C1.Win.C1Ribbon.RibbonApplicationMenu()
        Me.RibbonConfigToolBar1 = New C1.Win.C1Ribbon.RibbonConfigToolBar()
        Me.rbnLblDocNbrCaption = New C1.Win.C1Ribbon.RibbonLabel()
        Me.lblBirthListStatusCal = New C1.Win.C1Ribbon.RibbonLabel()
        Me.RibbonSeparator1 = New C1.Win.C1Ribbon.RibbonSeparator()
        Me.RibbonQat1 = New C1.Win.C1Ribbon.RibbonQat()
        Me.rbTabBL = New C1.Win.C1Ribbon.RibbonTab()
        Me.rbGrpBirthList = New C1.Win.C1Ribbon.RibbonGroup()
        Me.rbnbtnNewBL = New C1.Win.C1Ribbon.RibbonButton()
        Me.btnClose = New C1.Win.C1Ribbon.RibbonButton()
        Me.btnCancel = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbtnSaleBirthList = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbgrpBLSave = New C1.Win.C1Ribbon.RibbonGroup()
        Me.btnSaveAndPrint = New C1.Win.C1Ribbon.RibbonButton()
        Me.btnPrint = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbgrpCustSearch = New C1.Win.C1Ribbon.RibbonGroup()
        Me.rbnbtnSearchBL = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbnGrpStockCheck = New C1.Win.C1Ribbon.RibbonGroup()
        Me.rbnbtnstockCheck = New C1.Win.C1Ribbon.RibbonButton()
        Me.btnShowAllclmGrid = New C1.Win.C1Ribbon.RibbonButton()
        Me.C1Sizer1 = New C1.Win.C1Sizer.C1Sizer()
        Me.cboEvent = New C1.Win.C1List.C1Combo()
        Me.btnSearchBirthListID = New Spectrum.CtrlBtn()
        Me.txtBirthListid = New Spectrum.CtrlTextBox()
        Me.CtrlLabel5 = New Spectrum.CtrlLabel()
        Me.c1dateEditDeliverydate = New Spectrum.ctrlDate()
        Me.c1dateEditEventDate = New Spectrum.ctrlDate()
        Me.CtrlTextBox1 = New Spectrum.CtrlTextBox()
        Me.CtrlLabel4 = New Spectrum.CtrlLabel()
        Me.CtrlLabel3 = New Spectrum.CtrlLabel()
        Me.CtrlLabel2 = New Spectrum.CtrlLabel()
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        Me.CtrlHeader1 = New Spectrum.CtrlHeader()
        Me.CtrlCashSummary1 = New Spectrum.CtrlCashSummary()
        Me.CtrlCustDtls1 = New Spectrum.CtrlCustDtls()
        Me.C1Sizer2 = New C1.Win.C1Sizer.C1Sizer()
        Me.btnCovertToOpenAmount = New Spectrum.CtrlBtn()
        Me.btnPickUpQty = New Spectrum.CtrlBtn()
        Me.btnReturnCancel = New Spectrum.CtrlBtn()
        Me.BtnReturnSave = New Spectrum.CtrlBtn()
        Me.btnReturns = New Spectrum.CtrlBtn()
        Me.btnApplyDiscountAndClose1 = New Spectrum.CtrlBtn()
        Me.btnPurchaseQty = New Spectrum.CtrlBtn()
        Me.CtrlTab1 = New Spectrum.CtrlTab()
        Me.C1DockingTabPage1 = New C1.Win.C1Command.C1DockingTabPage()
        Me.c1SizerGrid = New C1.Win.C1Sizer.C1Sizer()
        Me.grdScanItem = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.CtrlSalesPerson1 = New Spectrum.CtrlSalesPerson()
        Me.C1DockingTabPage2 = New C1.Win.C1Command.C1DockingTabPage()
        Me.grpBoxGV = New System.Windows.Forms.GroupBox()
        Me.lblGVCalTotalAmout = New System.Windows.Forms.Label()
        Me.lblGVTotalAmount = New System.Windows.Forms.Label()
        Me.gridGV = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.C1DockingTabPage3 = New C1.Win.C1Command.C1DockingTabPage()
        Me.gridPaymentHistory = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblTotalItemQtyPaymentHistory = New System.Windows.Forms.Label()
        Me.lblCalTotalAmountPaymentHistory = New System.Windows.Forms.Label()
        Me.lblCalTotalItemQtyPaymentHistory = New System.Windows.Forms.Label()
        Me.lblCalTotalItemPaymentHistory = New System.Windows.Forms.Label()
        Me.lblTotalAmountPaymentHistory = New System.Windows.Forms.Label()
        Me.lblTotalItemPaymentHistory = New System.Windows.Forms.Label()
        Me.lblTotalPaymentHistory = New System.Windows.Forms.Label()
        Me.BirthListArticleDtlsTableAdapter1 = New SpectrumBL.POSDBDataSetTableAdapters.BirthListArticleDtlsTableAdapter()
        Me.POSDBDataSet = New SpectrumBL.POSDBDataSet()
        Me.BirthListTableAdapter1 = New SpectrumBL.POSDBDataSetTableAdapters.BirthListTableAdapter()
        Me.BirthListRequestedItemsTableAdapter1 = New SpectrumBL.POSDBDataSetTableAdapters.BirthListRequestedItemsTableAdapter()
        Me.ClpCustomersTableAdapter1 = New SpectrumBL.POSDBDataSetTableAdapters.CLPCustomersTableAdapter()
        Me.CustomerAddressTableAdapter1 = New SpectrumBL.POSDBDataSetTableAdapters.CustomerAddressTableAdapter()
        Me.CustomerSaleOrderTableAdapter1 = New SpectrumBL.POSDBDataSetTableAdapters.CustomerSaleOrderTableAdapter()
        Me.BirthListArticleDtlsTableAdapter2 = New SpectrumBL.POSDBDataSetTableAdapters.BirthListArticleDtlsTableAdapter()
        Me.lblBirthListStatusCal1 = New Spectrum.CtrlLabel()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.CtrlProductImage1 = New Spectrum.CtrlProductImage()
        CType(Me.C1StatusBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlRbn1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer1.SuspendLayout()
        CType(Me.cboEvent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBirthListid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.c1dateEditDeliverydate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.c1dateEditEventDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlTextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Sizer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer2.SuspendLayout()
        CType(Me.CtrlTab1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CtrlTab1.SuspendLayout()
        Me.C1DockingTabPage1.SuspendLayout()
        CType(Me.c1SizerGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.c1SizerGrid.SuspendLayout()
        CType(Me.grdScanItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1DockingTabPage2.SuspendLayout()
        Me.grpBoxGV.SuspendLayout()
        CType(Me.gridGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1DockingTabPage3.SuspendLayout()
        CType(Me.gridPaymentHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.POSDBDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBirthListStatusCal1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.CtrlRbn1.Tabs.Add(Me.rbTabBL)
        '
        'RibbonApplicationMenu1
        '
        Me.RibbonApplicationMenu1.Name = "RibbonApplicationMenu1"
        Me.RibbonApplicationMenu1.SmallImage = CType(resources.GetObject("RibbonApplicationMenu1.SmallImage"), System.Drawing.Image)
        '
        'RibbonConfigToolBar1
        '
        Me.RibbonConfigToolBar1.Items.Add(Me.rbnLblDocNbrCaption)
        Me.RibbonConfigToolBar1.Items.Add(Me.lblBirthListStatusCal)
        Me.RibbonConfigToolBar1.Items.Add(Me.RibbonSeparator1)
        Me.RibbonConfigToolBar1.Name = "RibbonConfigToolBar1"
        '
        'rbnLblDocNbrCaption
        '
        Me.rbnLblDocNbrCaption.Name = "rbnLblDocNbrCaption"
        Me.rbnLblDocNbrCaption.Text = "Document Status"
        '
        'lblBirthListStatusCal
        '
        Me.lblBirthListStatusCal.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBirthListStatusCal.Name = "lblBirthListStatusCal"
        '
        'RibbonSeparator1
        '
        Me.RibbonSeparator1.Name = "RibbonSeparator1"
        '
        'RibbonQat1
        '
        Me.RibbonQat1.Name = "RibbonQat1"
        '
        'rbTabBL
        '
        Me.rbTabBL.Groups.Add(Me.rbGrpBirthList)
        Me.rbTabBL.Groups.Add(Me.rbgrpBLSave)
        Me.rbTabBL.Groups.Add(Me.rbgrpCustSearch)
        Me.rbTabBL.Groups.Add(Me.rbnGrpStockCheck)
        Me.rbTabBL.Name = "rbTabBL"
        Me.rbTabBL.Text = "BirthList"
        '
        'rbGrpBirthList
        '
        Me.rbGrpBirthList.Items.Add(Me.rbnbtnNewBL)
        Me.rbGrpBirthList.Items.Add(Me.btnClose)
        Me.rbGrpBirthList.Items.Add(Me.btnCancel)
        Me.rbGrpBirthList.Items.Add(Me.rbtnSaleBirthList)
        Me.rbGrpBirthList.Name = "rbGrpBirthList"
        Me.rbGrpBirthList.Text = "BirthList"
        '
        'rbnbtnNewBL
        '
        Me.rbnbtnNewBL.LargeImage = Global.Spectrum.My.Resources.Resources.Birth_list_new
        Me.rbnbtnNewBL.Name = "rbnbtnNewBL"
        Me.rbnbtnNewBL.SmallImage = CType(resources.GetObject("rbnbtnNewBL.SmallImage"), System.Drawing.Image)
        Me.rbnbtnNewBL.Text = "&New BirthList"
        '
        'btnClose
        '
        Me.btnClose.Description = "Close Birth List"
        Me.btnClose.LargeImage = Global.Spectrum.My.Resources.Resources.Birth_list_close
        Me.btnClose.Name = "btnClose"
        Me.btnClose.SmallImage = CType(resources.GetObject("btnClose.SmallImage"), System.Drawing.Image)
        Me.btnClose.Text = "&Close BirthList"
        '
        'btnCancel
        '
        Me.btnCancel.Description = "Cancel BirthList"
        Me.btnCancel.LargeImage = Global.Spectrum.My.Resources.Resources.Birth_list_cancel
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.SmallImage = CType(resources.GetObject("btnCancel.SmallImage"), System.Drawing.Image)
        Me.btnCancel.Text = "Cance&L BirthList"
        '
        'rbtnSaleBirthList
        '
        Me.rbtnSaleBirthList.Description = "Sales Against BirthList"
        Me.rbtnSaleBirthList.LargeImage = Global.Spectrum.My.Resources.Resources.Birth_list_sale
        Me.rbtnSaleBirthList.Name = "rbtnSaleBirthList"
        Me.rbtnSaleBirthList.SmallImage = CType(resources.GetObject("rbtnSaleBirthList.SmallImage"), System.Drawing.Image)
        Me.rbtnSaleBirthList.Text = "Sale BirthList"
        Me.rbtnSaleBirthList.ToolTip = "Sales Against BirthList"
        '
        'rbgrpBLSave
        '
        Me.rbgrpBLSave.Items.Add(Me.btnSaveAndPrint)
        Me.rbgrpBLSave.Items.Add(Me.btnPrint)
        Me.rbgrpBLSave.Name = "rbgrpBLSave"
        Me.rbgrpBLSave.Text = "Save N Print"
        '
        'btnSaveAndPrint
        '
        Me.btnSaveAndPrint.Description = "Save BirthList"
        Me.btnSaveAndPrint.LargeImage = Global.Spectrum.My.Resources.Resources.save_bl
        Me.btnSaveAndPrint.Name = "btnSaveAndPrint"
        Me.btnSaveAndPrint.SmallImage = CType(resources.GetObject("btnSaveAndPrint.SmallImage"), System.Drawing.Image)
        Me.btnSaveAndPrint.Text = "Save Bir&ThList"
        '
        'btnPrint
        '
        Me.btnPrint.LargeImage = Global.Spectrum.My.Resources.Resources.print_bl
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.SmallImage = CType(resources.GetObject("btnPrint.SmallImage"), System.Drawing.Image)
        Me.btnPrint.Text = "&Print Birthlist List"
        '
        'rbgrpCustSearch
        '
        Me.rbgrpCustSearch.Items.Add(Me.rbnbtnSearchBL)
        Me.rbgrpCustSearch.Name = "rbgrpCustSearch"
        Me.rbgrpCustSearch.Text = "Birth List Search"
        '
        'rbnbtnSearchBL
        '
        Me.rbnbtnSearchBL.LargeImage = Global.Spectrum.My.Resources.Resources.customer_search
        Me.rbnbtnSearchBL.Name = "rbnbtnSearchBL"
        Me.rbnbtnSearchBL.SmallImage = CType(resources.GetObject("rbnbtnSearchBL.SmallImage"), System.Drawing.Image)
        Me.rbnbtnSearchBL.Text = "Search &Birth List"
        '
        'rbnGrpStockCheck
        '
        Me.rbnGrpStockCheck.Items.Add(Me.rbnbtnstockCheck)
        Me.rbnGrpStockCheck.Items.Add(Me.btnShowAllclmGrid)
        Me.rbnGrpStockCheck.Name = "rbnGrpStockCheck"
        Me.rbnGrpStockCheck.Text = "Stock Check"
        '
        'rbnbtnstockCheck
        '
        Me.rbnbtnstockCheck.LargeImage = Global.Spectrum.My.Resources.Resources.stock_check
        Me.rbnbtnstockCheck.Name = "rbnbtnstockCheck"
        Me.rbnbtnstockCheck.SmallImage = CType(resources.GetObject("rbnbtnstockCheck.SmallImage"), System.Drawing.Image)
        Me.rbnbtnstockCheck.Text = "Stock Chec&k"
        '
        'btnShowAllclmGrid
        '
        Me.btnShowAllclmGrid.Description = "To show all the column in grid at same time."
        Me.btnShowAllclmGrid.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowAllclmGrid.LargeImage = Global.Spectrum.My.Resources.Resources.show_all
        Me.btnShowAllclmGrid.Name = "btnShowAllclmGrid"
        Me.btnShowAllclmGrid.Text = "Show &All Columns"
        Me.btnShowAllclmGrid.ToolTip = "Display All Columns in Grid"
        '
        'C1Sizer1
        '
        Me.C1Sizer1.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1Sizer1.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1Sizer1.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1Sizer1.Controls.Add(Me.cboEvent)
        Me.C1Sizer1.Controls.Add(Me.btnSearchBirthListID)
        Me.C1Sizer1.Controls.Add(Me.txtBirthListid)
        Me.C1Sizer1.Controls.Add(Me.CtrlLabel5)
        Me.C1Sizer1.Controls.Add(Me.c1dateEditDeliverydate)
        Me.C1Sizer1.Controls.Add(Me.c1dateEditEventDate)
        Me.C1Sizer1.Controls.Add(Me.CtrlTextBox1)
        Me.C1Sizer1.Controls.Add(Me.CtrlLabel4)
        Me.C1Sizer1.Controls.Add(Me.CtrlLabel3)
        Me.C1Sizer1.Controls.Add(Me.CtrlLabel2)
        Me.C1Sizer1.Controls.Add(Me.CtrlLabel1)
        Me.C1Sizer1.GridDefinition = resources.GetString("C1Sizer1.GridDefinition")
        Me.C1Sizer1.Location = New System.Drawing.Point(0, 155)
        Me.C1Sizer1.Name = "C1Sizer1"
        Me.C1Sizer1.Padding = New System.Windows.Forms.Padding(1)
        Me.C1Sizer1.Size = New System.Drawing.Size(346, 122)
        Me.C1Sizer1.SplitterWidth = 1
        Me.C1Sizer1.TabIndex = 0
        Me.C1Sizer1.Text = "C1Sizer1"
        '
        'cboEvent
        '
        Me.cboEvent.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboEvent.AutoCompletion = True
        Me.cboEvent.AutoSelect = True
        Me.cboEvent.AutoSize = False
        Me.cboEvent.Caption = ""
        Me.cboEvent.CaptionHeight = 17
        Me.cboEvent.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboEvent.ColumnCaptionHeight = 17
        Me.cboEvent.ColumnFooterHeight = 17
        Me.cboEvent.ColumnHeaders = False
        Me.cboEvent.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList
        Me.cboEvent.ContentHeight = 16
        Me.cboEvent.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboEvent.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboEvent.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEvent.EditorForeColor = System.Drawing.Color.Black
        Me.cboEvent.EditorHeight = 16
        Me.cboEvent.Images.Add(CType(resources.GetObject("cboEvent.Images"), System.Drawing.Image))
        Me.cboEvent.ItemHeight = 15
        Me.cboEvent.Location = New System.Drawing.Point(72, 29)
        Me.cboEvent.MatchEntryTimeout = CType(2000, Long)
        Me.cboEvent.MaxDropDownItems = CType(5, Short)
        Me.cboEvent.MaxLength = 32767
        Me.cboEvent.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboEvent.Name = "cboEvent"
        Me.cboEvent.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboEvent.Size = New System.Drawing.Size(272, 22)
        Me.cboEvent.TabIndex = 2
        Me.cboEvent.Text = "C1Combo1"
        Me.cboEvent.PropBag = resources.GetString("cboEvent.PropBag")
        '
        'btnSearchBirthListID
        '
        Me.btnSearchBirthListID.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnSearchBirthListID.Image = Global.Spectrum.My.Resources.Resources.search_2
        Me.btnSearchBirthListID.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnSearchBirthListID.Location = New System.Drawing.Point(318, 2)
        Me.btnSearchBirthListID.Name = "btnSearchBirthListID"
        Me.btnSearchBirthListID.SetArticleCode = Nothing
        Me.btnSearchBirthListID.SetRowIndex = 0
        Me.btnSearchBirthListID.Size = New System.Drawing.Size(26, 26)
        Me.btnSearchBirthListID.TabIndex = 1
        Me.btnSearchBirthListID.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSearchBirthListID.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnSearchBirthListID.UseVisualStyleBackColor = True
        Me.btnSearchBirthListID.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtBirthListid
        '
        Me.txtBirthListid.AutoSize = False
        Me.txtBirthListid.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtBirthListid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBirthListid.Location = New System.Drawing.Point(72, 2)
        Me.txtBirthListid.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtBirthListid.Name = "txtBirthListid"
        Me.txtBirthListid.Size = New System.Drawing.Size(245, 26)
        Me.txtBirthListid.TabIndex = 0
        Me.txtBirthListid.Tag = Nothing
        Me.txtBirthListid.TextDetached = True
        Me.txtBirthListid.VerticalAlign = C1.Win.C1Input.VerticalAlignEnum.Middle
        Me.txtBirthListid.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtBirthListid.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel5
        '
        Me.CtrlLabel5.AttachedTextBoxName = Nothing
        Me.CtrlLabel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel5.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel5.Location = New System.Drawing.Point(2, 2)
        Me.CtrlLabel5.Name = "CtrlLabel5"
        Me.CtrlLabel5.Size = New System.Drawing.Size(69, 26)
        Me.CtrlLabel5.TabIndex = 0
        Me.CtrlLabel5.Tag = Nothing
        Me.CtrlLabel5.Text = "BirthList Id"
        Me.CtrlLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CtrlLabel5.TextDetached = True
        Me.CtrlLabel5.TrimStart = True
        '
        'c1dateEditDeliverydate
        '
        Me.c1dateEditDeliverydate.AutoSize = False
        Me.c1dateEditDeliverydate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.c1dateEditDeliverydate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.c1dateEditDeliverydate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage"), System.Drawing.Image)
        '
        '
        '
        Me.c1dateEditDeliverydate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.c1dateEditDeliverydate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.c1dateEditDeliverydate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.c1dateEditDeliverydate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.c1dateEditDeliverydate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.c1dateEditDeliverydate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.c1dateEditDeliverydate.Location = New System.Drawing.Point(252, 52)
        Me.c1dateEditDeliverydate.Margin = New System.Windows.Forms.Padding(0)
        Me.c1dateEditDeliverydate.Name = "c1dateEditDeliverydate"
        Me.c1dateEditDeliverydate.Size = New System.Drawing.Size(92, 23)
        Me.c1dateEditDeliverydate.TabIndex = 4
        Me.c1dateEditDeliverydate.Tag = Nothing
        Me.c1dateEditDeliverydate.TrimStart = True
        Me.c1dateEditDeliverydate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.c1dateEditDeliverydate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'c1dateEditEventDate
        '
        Me.c1dateEditEventDate.AutoSize = False
        Me.c1dateEditEventDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.c1dateEditEventDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.c1dateEditEventDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage1"), System.Drawing.Image)
        '
        '
        '
        Me.c1dateEditEventDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.c1dateEditEventDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.c1dateEditEventDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.c1dateEditEventDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.c1dateEditEventDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.c1dateEditEventDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.c1dateEditEventDate.Location = New System.Drawing.Point(72, 52)
        Me.c1dateEditEventDate.Name = "c1dateEditEventDate"
        Me.c1dateEditEventDate.Size = New System.Drawing.Size(102, 23)
        Me.c1dateEditEventDate.TabIndex = 3
        Me.c1dateEditEventDate.Tag = Nothing
        Me.c1dateEditEventDate.TrimStart = True
        Me.c1dateEditEventDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.c1dateEditEventDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlTextBox1
        '
        Me.CtrlTextBox1.AutoSize = False
        Me.CtrlTextBox1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.CtrlTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlTextBox1.Location = New System.Drawing.Point(72, 76)
        Me.CtrlTextBox1.MaxLength = 200
        Me.CtrlTextBox1.MinimumSize = New System.Drawing.Size(10, 21)
        Me.CtrlTextBox1.Multiline = True
        Me.CtrlTextBox1.Name = "CtrlTextBox1"
        Me.CtrlTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.CtrlTextBox1.ShowFocusRectangle = True
        Me.CtrlTextBox1.Size = New System.Drawing.Size(272, 44)
        Me.CtrlTextBox1.TabIndex = 5
        Me.CtrlTextBox1.Tag = Nothing
        Me.CtrlTextBox1.TextDetached = True
        Me.CtrlTextBox1.TrimStart = True
        Me.CtrlTextBox1.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlTextBox1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel4
        '
        Me.CtrlLabel4.AttachedTextBoxName = Nothing
        Me.CtrlLabel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel4.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel4.Location = New System.Drawing.Point(2, 76)
        Me.CtrlLabel4.Name = "CtrlLabel4"
        Me.CtrlLabel4.Size = New System.Drawing.Size(69, 21)
        Me.CtrlLabel4.TabIndex = 4
        Me.CtrlLabel4.Tag = Nothing
        Me.CtrlLabel4.Text = "Remarks"
        Me.CtrlLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CtrlLabel4.TextDetached = True
        Me.CtrlLabel4.TrimStart = True
        '
        'CtrlLabel3
        '
        Me.CtrlLabel3.AttachedTextBoxName = Nothing
        Me.CtrlLabel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel3.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel3.Location = New System.Drawing.Point(175, 52)
        Me.CtrlLabel3.Name = "CtrlLabel3"
        Me.CtrlLabel3.Size = New System.Drawing.Size(76, 23)
        Me.CtrlLabel3.TabIndex = 3
        Me.CtrlLabel3.Tag = Nothing
        Me.CtrlLabel3.Text = "Delivery Date"
        Me.CtrlLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CtrlLabel3.TextDetached = True
        Me.CtrlLabel3.TrimStart = True
        '
        'CtrlLabel2
        '
        Me.CtrlLabel2.AttachedTextBoxName = Nothing
        Me.CtrlLabel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel2.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel2.Location = New System.Drawing.Point(2, 52)
        Me.CtrlLabel2.Name = "CtrlLabel2"
        Me.CtrlLabel2.Size = New System.Drawing.Size(69, 23)
        Me.CtrlLabel2.TabIndex = 2
        Me.CtrlLabel2.Tag = Nothing
        Me.CtrlLabel2.Text = "Event Date"
        Me.CtrlLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CtrlLabel2.TextDetached = True
        Me.CtrlLabel2.TrimStart = True
        '
        'CtrlLabel1
        '
        Me.CtrlLabel1.AttachedTextBoxName = Nothing
        Me.CtrlLabel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel1.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel1.Location = New System.Drawing.Point(2, 29)
        Me.CtrlLabel1.Name = "CtrlLabel1"
        Me.CtrlLabel1.Size = New System.Drawing.Size(69, 22)
        Me.CtrlLabel1.TabIndex = 1
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.Text = "Event Name"
        Me.CtrlLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CtrlLabel1.TextDetached = True
        Me.CtrlLabel1.TrimStart = True
        '
        'CtrlHeader1
        '
        Me.CtrlHeader1.HdrText = "Search BirthList "
        Me.CtrlHeader1.Location = New System.Drawing.Point(2, 2)
        Me.CtrlHeader1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrlHeader1.Name = "CtrlHeader1"
        Me.CtrlHeader1.Padding = New System.Windows.Forms.Padding(1)
        Me.CtrlHeader1.Size = New System.Drawing.Size(242, 30)
        Me.CtrlHeader1.TabIndex = 2
        '
        'CtrlCashSummary1
        '
        Me.CtrlCashSummary1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlCashSummary1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlCashSummary1.hdrText = " Summary"
        Me.CtrlCashSummary1.lbl1 = "Purchase Amt"
        Me.CtrlCashSummary1.lbl10 = "Birthlist Value"
        Me.CtrlCashSummary1.lbl2 = "Purchase Items"
        Me.CtrlCashSummary1.lbl3 = "Purchase Qty"
        Me.CtrlCashSummary1.lbl4 = "Sold Amt"
        Me.CtrlCashSummary1.lbl5 = "Open Amount"
        Me.CtrlCashSummary1.lbl6 = "0"
        Me.CtrlCashSummary1.lbl7 = "0"
        Me.CtrlCashSummary1.lbl8 = "0"
        Me.CtrlCashSummary1.lbl9 = "0"
        Me.CtrlCashSummary1.lbltxt1 = "0.00"
        Me.CtrlCashSummary1.lbltxt10 = ""
        Me.CtrlCashSummary1.lbltxt2 = "0"
        Me.CtrlCashSummary1.lbltxt3 = "0"
        Me.CtrlCashSummary1.lbltxt4 = "0.00"
        Me.CtrlCashSummary1.lbltxt5 = "0.00"
        Me.CtrlCashSummary1.lbltxt6 = "0.00"
        Me.CtrlCashSummary1.lbltxt7 = ""
        Me.CtrlCashSummary1.lbltxt8 = ""
        Me.CtrlCashSummary1.lbltxt9 = ""
        Me.CtrlCashSummary1.lbltxtVisible1 = True
        Me.CtrlCashSummary1.lbltxtVisible10 = False
        Me.CtrlCashSummary1.lbltxtVisible2 = True
        Me.CtrlCashSummary1.lbltxtVisible3 = True
        Me.CtrlCashSummary1.lbltxtVisible4 = True
        Me.CtrlCashSummary1.lbltxtVisible5 = True
        Me.CtrlCashSummary1.lbltxtVisible6 = True
        Me.CtrlCashSummary1.lbltxtVisible7 = False
        Me.CtrlCashSummary1.lbltxtVisible8 = False
        Me.CtrlCashSummary1.lbltxtVisible9 = False
        Me.CtrlCashSummary1.lblVisible1 = True
        Me.CtrlCashSummary1.lblVisible10 = True
        Me.CtrlCashSummary1.lblVisible2 = True
        Me.CtrlCashSummary1.lblVisible3 = True
        Me.CtrlCashSummary1.lblVisible4 = True
        Me.CtrlCashSummary1.lblVisible5 = True
        Me.CtrlCashSummary1.lblVisible6 = True
        Me.CtrlCashSummary1.lblVisible7 = True
        Me.CtrlCashSummary1.lblVisible8 = True
        Me.CtrlCashSummary1.lblVisible9 = True
        Me.CtrlCashSummary1.Location = New System.Drawing.Point(594, 418)
        Me.CtrlCashSummary1.Name = "CtrlCashSummary1"
        Me.CtrlCashSummary1.RowCount = 7
        Me.CtrlCashSummary1.Size = New System.Drawing.Size(198, 152)
        Me.CtrlCashSummary1.TabIndex = 64
        Me.CtrlCashSummary1.TabStop = False
        '
        'CtrlCustDtls1
        '
        Me.CtrlCustDtls1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlCustDtls1.DisplayCustType = True
        Me.CtrlCustDtls1.dtCustmInfo = Nothing
        Me.CtrlCustDtls1.Location = New System.Drawing.Point(352, 152)
        Me.CtrlCustDtls1.Name = "CtrlCustDtls1"
        Me.CtrlCustDtls1.Size = New System.Drawing.Size(440, 127)
        Me.CtrlCustDtls1.TabIndex = 66
        Me.CtrlCustDtls1.TabStop = False
        '
        'C1Sizer2
        '
        Me.C1Sizer2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C1Sizer2.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1Sizer2.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1Sizer2.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1Sizer2.Controls.Add(Me.btnCovertToOpenAmount)
        Me.C1Sizer2.Controls.Add(Me.btnPickUpQty)
        Me.C1Sizer2.Controls.Add(Me.btnReturnCancel)
        Me.C1Sizer2.Controls.Add(Me.BtnReturnSave)
        Me.C1Sizer2.Controls.Add(Me.btnReturns)
        Me.C1Sizer2.Controls.Add(Me.btnApplyDiscountAndClose1)
        Me.C1Sizer2.Controls.Add(Me.btnPurchaseQty)
        Me.C1Sizer2.GridDefinition = resources.GetString("C1Sizer2.GridDefinition")
        Me.C1Sizer2.Location = New System.Drawing.Point(2, 498)
        Me.C1Sizer2.Name = "C1Sizer2"
        Me.C1Sizer2.Size = New System.Drawing.Size(591, 72)
        Me.C1Sizer2.TabIndex = 2
        '
        'btnCovertToOpenAmount
        '
        Me.btnCovertToOpenAmount.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCovertToOpenAmount.Image = Global.Spectrum.My.Resources.Resources.convert_to_open_amt
        Me.btnCovertToOpenAmount.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCovertToOpenAmount.Location = New System.Drawing.Point(170, 5)
        Me.btnCovertToOpenAmount.Name = "btnCovertToOpenAmount"
        Me.btnCovertToOpenAmount.SetArticleCode = Nothing
        Me.btnCovertToOpenAmount.SetRowIndex = 0
        Me.btnCovertToOpenAmount.Size = New System.Drawing.Size(80, 62)
        Me.btnCovertToOpenAmount.TabIndex = 2
        Me.btnCovertToOpenAmount.Text = "ConvertTo &OpenAmount"
        Me.btnCovertToOpenAmount.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCovertToOpenAmount.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCovertToOpenAmount.UseVisualStyleBackColor = True
        Me.btnCovertToOpenAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnPickUpQty
        '
        Me.btnPickUpQty.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPickUpQty.Image = Global.Spectrum.My.Resources.Resources.pick_up_item
        Me.btnPickUpQty.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPickUpQty.Location = New System.Drawing.Point(87, 5)
        Me.btnPickUpQty.Name = "btnPickUpQty"
        Me.btnPickUpQty.SetArticleCode = Nothing
        Me.btnPickUpQty.SetRowIndex = 0
        Me.btnPickUpQty.Size = New System.Drawing.Size(79, 62)
        Me.btnPickUpQty.TabIndex = 1
        Me.btnPickUpQty.Text = "Pick&Up Qty"
        Me.btnPickUpQty.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPickUpQty.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnPickUpQty.UseVisualStyleBackColor = True
        Me.btnPickUpQty.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnReturnCancel
        '
        Me.btnReturnCancel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReturnCancel.Image = Global.Spectrum.My.Resources.Resources.cancel_bl
        Me.btnReturnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnReturnCancel.Location = New System.Drawing.Point(508, 5)
        Me.btnReturnCancel.Name = "btnReturnCancel"
        Me.btnReturnCancel.SetArticleCode = Nothing
        Me.btnReturnCancel.SetRowIndex = 0
        Me.btnReturnCancel.Size = New System.Drawing.Size(78, 62)
        Me.btnReturnCancel.TabIndex = 6
        Me.btnReturnCancel.Text = "&Cancel"
        Me.btnReturnCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnReturnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnReturnCancel.UseVisualStyleBackColor = True
        Me.btnReturnCancel.Visible = False
        Me.btnReturnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnReturnSave
        '
        Me.BtnReturnSave.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnReturnSave.Image = Global.Spectrum.My.Resources.Resources.save_bl
        Me.BtnReturnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnReturnSave.Location = New System.Drawing.Point(422, 5)
        Me.BtnReturnSave.Name = "BtnReturnSave"
        Me.BtnReturnSave.SetArticleCode = Nothing
        Me.BtnReturnSave.SetRowIndex = 0
        Me.BtnReturnSave.Size = New System.Drawing.Size(82, 62)
        Me.BtnReturnSave.TabIndex = 5
        Me.BtnReturnSave.Text = "Sa&ve"
        Me.BtnReturnSave.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnReturnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnReturnSave.UseVisualStyleBackColor = True
        Me.BtnReturnSave.Visible = False
        Me.BtnReturnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnReturns
        '
        Me.btnReturns.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReturns.Image = Global.Spectrum.My.Resources.Resources.returns
        Me.btnReturns.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnReturns.Location = New System.Drawing.Point(338, 5)
        Me.btnReturns.Name = "btnReturns"
        Me.btnReturns.SetArticleCode = Nothing
        Me.btnReturns.SetRowIndex = 0
        Me.btnReturns.Size = New System.Drawing.Size(80, 62)
        Me.btnReturns.TabIndex = 4
        Me.btnReturns.Text = "&Return"
        Me.btnReturns.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnReturns.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnReturns.UseVisualStyleBackColor = True
        Me.btnReturns.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnApplyDiscountAndClose1
        '
        Me.btnApplyDiscountAndClose1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApplyDiscountAndClose1.Image = Global.Spectrum.My.Resources.Resources.addcost
        Me.btnApplyDiscountAndClose1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnApplyDiscountAndClose1.Location = New System.Drawing.Point(254, 5)
        Me.btnApplyDiscountAndClose1.Name = "btnApplyDiscountAndClose1"
        Me.btnApplyDiscountAndClose1.SetArticleCode = Nothing
        Me.btnApplyDiscountAndClose1.SetRowIndex = 0
        Me.btnApplyDiscountAndClose1.Size = New System.Drawing.Size(80, 62)
        Me.btnApplyDiscountAndClose1.TabIndex = 3
        Me.btnApplyDiscountAndClose1.Text = "Apply &Disc & Close"
        Me.btnApplyDiscountAndClose1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnApplyDiscountAndClose1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnApplyDiscountAndClose1.UseVisualStyleBackColor = True
        Me.btnApplyDiscountAndClose1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnPurchaseQty
        '
        Me.btnPurchaseQty.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPurchaseQty.Image = Global.Spectrum.My.Resources.Resources.purchase_item
        Me.btnPurchaseQty.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPurchaseQty.Location = New System.Drawing.Point(5, 5)
        Me.btnPurchaseQty.Name = "btnPurchaseQty"
        Me.btnPurchaseQty.SetArticleCode = Nothing
        Me.btnPurchaseQty.SetRowIndex = 0
        Me.btnPurchaseQty.Size = New System.Drawing.Size(78, 62)
        Me.btnPurchaseQty.TabIndex = 0
        Me.btnPurchaseQty.Text = "Purchase &Qty"
        Me.btnPurchaseQty.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPurchaseQty.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnPurchaseQty.UseVisualStyleBackColor = True
        Me.btnPurchaseQty.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlTab1
        '
        Me.CtrlTab1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlTab1.Controls.Add(Me.C1DockingTabPage1)
        Me.CtrlTab1.Controls.Add(Me.C1DockingTabPage2)
        Me.CtrlTab1.Controls.Add(Me.C1DockingTabPage3)
        Me.CtrlTab1.HotTrack = True
        Me.CtrlTab1.Location = New System.Drawing.Point(4, 282)
        Me.CtrlTab1.Name = "CtrlTab1"
        Me.CtrlTab1.SelectedIndex = 2
        Me.CtrlTab1.SelectedTabBold = True
        Me.CtrlTab1.ShowTabList = True
        Me.CtrlTab1.ShowToolTips = True
        Me.CtrlTab1.Size = New System.Drawing.Size(590, 215)
        Me.CtrlTab1.TabIndex = 1
        Me.CtrlTab1.TabsSpacing = 6
        Me.CtrlTab1.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007
        Me.CtrlTab1.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Blue
        '
        'C1DockingTabPage1
        '
        Me.C1DockingTabPage1.Controls.Add(Me.c1SizerGrid)
        Me.C1DockingTabPage1.Location = New System.Drawing.Point(1, 24)
        Me.C1DockingTabPage1.Name = "C1DockingTabPage1"
        Me.C1DockingTabPage1.Size = New System.Drawing.Size(588, 190)
        Me.C1DockingTabPage1.TabBackColor = System.Drawing.Color.Transparent
        Me.C1DockingTabPage1.TabBackColorSelected = System.Drawing.Color.Transparent
        Me.C1DockingTabPage1.TabIndex = 0
        Me.C1DockingTabPage1.Text = "BirthList Items"
        '
        'c1SizerGrid
        '
        Me.c1SizerGrid.Border.Color = System.Drawing.Color.LightSlateGray
        Me.c1SizerGrid.Border.Corners = New C1.Win.C1Sizer.Corners(0, 0, 0, 4)
        Me.c1SizerGrid.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.c1SizerGrid.Controls.Add(Me.grdScanItem)
        Me.c1SizerGrid.Controls.Add(Me.CtrlSalesPerson1)
        Me.c1SizerGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1SizerGrid.GridDefinition = "11.5789473684211:False:True;81.0526315789474:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "20.4081632653061:False:" & _
    "True;28.4013605442177:False:False;43.1972789115646:False:False;4.25170068027211:" & _
    "False:True;"
        Me.c1SizerGrid.Location = New System.Drawing.Point(0, 0)
        Me.c1SizerGrid.Name = "c1SizerGrid"
        Me.c1SizerGrid.Size = New System.Drawing.Size(588, 190)
        Me.c1SizerGrid.TabIndex = 0
        Me.c1SizerGrid.Text = "C1Sizer1"
        '
        'grdScanItem
        '
        Me.grdScanItem.AllowDelete = True
        Me.grdScanItem.AutoGenerateColumns = False
        Me.grdScanItem.BackColor = System.Drawing.Color.Transparent
        Me.grdScanItem.CellButtonImage = Global.Spectrum.My.Resources.Resources.del_icon
        Me.grdScanItem.ColumnInfo = resources.GetString("grdScanItem.ColumnInfo")
        Me.grdScanItem.ExtendLastCol = True
        Me.grdScanItem.Location = New System.Drawing.Point(5, 31)
        Me.grdScanItem.Name = "grdScanItem"
        Me.grdScanItem.Rows.Count = 1
        Me.grdScanItem.Rows.DefaultSize = 18
        Me.grdScanItem.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.grdScanItem.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.grdScanItem.ShowCellLabels = True
        Me.grdScanItem.Size = New System.Drawing.Size(578, 154)
        Me.grdScanItem.StyleInfo = resources.GetString("grdScanItem.StyleInfo")
        Me.grdScanItem.TabIndex = 1
        Me.grdScanItem.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'CtrlSalesPerson1
        '
        Me.CtrlSalesPerson1.Location = New System.Drawing.Point(5, 5)
        Me.CtrlSalesPerson1.Name = "CtrlSalesPerson1"
        Me.CtrlSalesPerson1.Size = New System.Drawing.Size(578, 22)
        Me.CtrlSalesPerson1.TabIndex = 0
        '
        'C1DockingTabPage2
        '
        Me.C1DockingTabPage2.Controls.Add(Me.grpBoxGV)
        Me.C1DockingTabPage2.Controls.Add(Me.gridGV)
        Me.C1DockingTabPage2.Location = New System.Drawing.Point(1, 24)
        Me.C1DockingTabPage2.Name = "C1DockingTabPage2"
        Me.C1DockingTabPage2.Size = New System.Drawing.Size(588, 190)
        Me.C1DockingTabPage2.TabBackColor = System.Drawing.Color.Transparent
        Me.C1DockingTabPage2.TabBackColorSelected = System.Drawing.Color.Transparent
        Me.C1DockingTabPage2.TabIndex = 8
        Me.C1DockingTabPage2.Text = "Gift Voucher"
        '
        'grpBoxGV
        '
        Me.grpBoxGV.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpBoxGV.Controls.Add(Me.lblGVCalTotalAmout)
        Me.grpBoxGV.Controls.Add(Me.lblGVTotalAmount)
        Me.grpBoxGV.Location = New System.Drawing.Point(0, 157)
        Me.grpBoxGV.Name = "grpBoxGV"
        Me.grpBoxGV.Size = New System.Drawing.Size(588, 33)
        Me.grpBoxGV.TabIndex = 130
        Me.grpBoxGV.TabStop = False
        '
        'lblGVCalTotalAmout
        '
        Me.lblGVCalTotalAmout.BackColor = System.Drawing.Color.Transparent
        Me.lblGVCalTotalAmout.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGVCalTotalAmout.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.lblGVCalTotalAmout.Location = New System.Drawing.Point(441, 8)
        Me.lblGVCalTotalAmout.Name = "lblGVCalTotalAmout"
        Me.lblGVCalTotalAmout.Size = New System.Drawing.Size(124, 21)
        Me.lblGVCalTotalAmout.TabIndex = 133
        Me.lblGVCalTotalAmout.Text = "0"
        Me.lblGVCalTotalAmout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblGVTotalAmount
        '
        Me.lblGVTotalAmount.BackColor = System.Drawing.Color.Transparent
        Me.lblGVTotalAmount.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGVTotalAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.lblGVTotalAmount.Location = New System.Drawing.Point(335, 5)
        Me.lblGVTotalAmount.Name = "lblGVTotalAmount"
        Me.lblGVTotalAmount.Size = New System.Drawing.Size(100, 26)
        Me.lblGVTotalAmount.TabIndex = 130
        Me.lblGVTotalAmount.Text = "Total Amount:"
        Me.lblGVTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gridGV
        '
        Me.gridGV.AllowDelete = True
        Me.gridGV.AllowEditing = False
        Me.gridGV.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gridGV.AutoGenerateColumns = False
        Me.gridGV.CellButtonImage = Global.Spectrum.My.Resources.Resources.del_icon
        Me.gridGV.ColumnInfo = resources.GetString("gridGV.ColumnInfo")
        Me.gridGV.ExtendLastCol = True
        Me.gridGV.Location = New System.Drawing.Point(0, 0)
        Me.gridGV.Name = "gridGV"
        Me.gridGV.Rows.Count = 1
        Me.gridGV.Rows.DefaultSize = 18
        Me.gridGV.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.gridGV.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.gridGV.Size = New System.Drawing.Size(592, 161)
        Me.gridGV.TabIndex = 127
        Me.gridGV.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'C1DockingTabPage3
        '
        Me.C1DockingTabPage3.Controls.Add(Me.gridPaymentHistory)
        Me.C1DockingTabPage3.Controls.Add(Me.GroupBox1)
        Me.C1DockingTabPage3.Location = New System.Drawing.Point(1, 24)
        Me.C1DockingTabPage3.Name = "C1DockingTabPage3"
        Me.C1DockingTabPage3.Size = New System.Drawing.Size(588, 190)
        Me.C1DockingTabPage3.TabBackColor = System.Drawing.Color.Transparent
        Me.C1DockingTabPage3.TabBackColorSelected = System.Drawing.Color.Transparent
        Me.C1DockingTabPage3.TabIndex = 9
        Me.C1DockingTabPage3.TabStop = False
        Me.C1DockingTabPage3.Text = "Payment History"
        '
        'gridPaymentHistory
        '
        Me.gridPaymentHistory.AllowDelete = True
        Me.gridPaymentHistory.AllowEditing = False
        Me.gridPaymentHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gridPaymentHistory.AutoGenerateColumns = False
        Me.gridPaymentHistory.CellButtonImage = Global.Spectrum.My.Resources.Resources.del_icon
        Me.gridPaymentHistory.ColumnInfo = resources.GetString("gridPaymentHistory.ColumnInfo")
        Me.gridPaymentHistory.ExtendLastCol = True
        Me.gridPaymentHistory.Location = New System.Drawing.Point(0, -1)
        Me.gridPaymentHistory.Name = "gridPaymentHistory"
        Me.gridPaymentHistory.Rows.Count = 1
        Me.gridPaymentHistory.Rows.DefaultSize = 18
        Me.gridPaymentHistory.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.gridPaymentHistory.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.gridPaymentHistory.Size = New System.Drawing.Size(588, 164)
        Me.gridPaymentHistory.TabIndex = 132
        Me.gridPaymentHistory.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblTotalItemQtyPaymentHistory)
        Me.GroupBox1.Controls.Add(Me.lblCalTotalAmountPaymentHistory)
        Me.GroupBox1.Controls.Add(Me.lblCalTotalItemQtyPaymentHistory)
        Me.GroupBox1.Controls.Add(Me.lblCalTotalItemPaymentHistory)
        Me.GroupBox1.Controls.Add(Me.lblTotalAmountPaymentHistory)
        Me.GroupBox1.Controls.Add(Me.lblTotalItemPaymentHistory)
        Me.GroupBox1.Controls.Add(Me.lblTotalPaymentHistory)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 159)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(590, 35)
        Me.GroupBox1.TabIndex = 133
        Me.GroupBox1.TabStop = False
        '
        'lblTotalItemQtyPaymentHistory
        '
        Me.lblTotalItemQtyPaymentHistory.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalItemQtyPaymentHistory.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalItemQtyPaymentHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.lblTotalItemQtyPaymentHistory.Location = New System.Drawing.Point(256, 4)
        Me.lblTotalItemQtyPaymentHistory.Name = "lblTotalItemQtyPaymentHistory"
        Me.lblTotalItemQtyPaymentHistory.Size = New System.Drawing.Size(63, 26)
        Me.lblTotalItemQtyPaymentHistory.TabIndex = 129
        Me.lblTotalItemQtyPaymentHistory.Text = "Quantity"
        Me.lblTotalItemQtyPaymentHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTotalItemQtyPaymentHistory.Visible = False
        '
        'lblCalTotalAmountPaymentHistory
        '
        Me.lblCalTotalAmountPaymentHistory.BackColor = System.Drawing.Color.Transparent
        Me.lblCalTotalAmountPaymentHistory.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalTotalAmountPaymentHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.lblCalTotalAmountPaymentHistory.Location = New System.Drawing.Point(470, 7)
        Me.lblCalTotalAmountPaymentHistory.Name = "lblCalTotalAmountPaymentHistory"
        Me.lblCalTotalAmountPaymentHistory.Size = New System.Drawing.Size(124, 21)
        Me.lblCalTotalAmountPaymentHistory.TabIndex = 133
        Me.lblCalTotalAmountPaymentHistory.Text = "0"
        Me.lblCalTotalAmountPaymentHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCalTotalItemQtyPaymentHistory
        '
        Me.lblCalTotalItemQtyPaymentHistory.BackColor = System.Drawing.Color.Transparent
        Me.lblCalTotalItemQtyPaymentHistory.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalTotalItemQtyPaymentHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.lblCalTotalItemQtyPaymentHistory.Location = New System.Drawing.Point(182, 7)
        Me.lblCalTotalItemQtyPaymentHistory.Name = "lblCalTotalItemQtyPaymentHistory"
        Me.lblCalTotalItemQtyPaymentHistory.Size = New System.Drawing.Size(68, 21)
        Me.lblCalTotalItemQtyPaymentHistory.TabIndex = 132
        Me.lblCalTotalItemQtyPaymentHistory.Text = "0"
        Me.lblCalTotalItemQtyPaymentHistory.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCalTotalItemQtyPaymentHistory.Visible = False
        '
        'lblCalTotalItemPaymentHistory
        '
        Me.lblCalTotalItemPaymentHistory.BackColor = System.Drawing.Color.Transparent
        Me.lblCalTotalItemPaymentHistory.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalTotalItemPaymentHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.lblCalTotalItemPaymentHistory.Location = New System.Drawing.Point(85, 4)
        Me.lblCalTotalItemPaymentHistory.Name = "lblCalTotalItemPaymentHistory"
        Me.lblCalTotalItemPaymentHistory.Size = New System.Drawing.Size(54, 26)
        Me.lblCalTotalItemPaymentHistory.TabIndex = 131
        Me.lblCalTotalItemPaymentHistory.Text = "0"
        Me.lblCalTotalItemPaymentHistory.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCalTotalItemPaymentHistory.Visible = False
        '
        'lblTotalAmountPaymentHistory
        '
        Me.lblTotalAmountPaymentHistory.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalAmountPaymentHistory.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalAmountPaymentHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.lblTotalAmountPaymentHistory.Location = New System.Drawing.Point(364, 3)
        Me.lblTotalAmountPaymentHistory.Name = "lblTotalAmountPaymentHistory"
        Me.lblTotalAmountPaymentHistory.Size = New System.Drawing.Size(100, 26)
        Me.lblTotalAmountPaymentHistory.TabIndex = 130
        Me.lblTotalAmountPaymentHistory.Text = "Total Amount:"
        Me.lblTotalAmountPaymentHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalItemPaymentHistory
        '
        Me.lblTotalItemPaymentHistory.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalItemPaymentHistory.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalItemPaymentHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.lblTotalItemPaymentHistory.Location = New System.Drawing.Point(145, 4)
        Me.lblTotalItemPaymentHistory.Name = "lblTotalItemPaymentHistory"
        Me.lblTotalItemPaymentHistory.Size = New System.Drawing.Size(44, 26)
        Me.lblTotalItemPaymentHistory.TabIndex = 128
        Me.lblTotalItemPaymentHistory.Text = "Item"
        Me.lblTotalItemPaymentHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTotalItemPaymentHistory.Visible = False
        '
        'lblTotalPaymentHistory
        '
        Me.lblTotalPaymentHistory.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalPaymentHistory.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalPaymentHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(103, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.lblTotalPaymentHistory.Location = New System.Drawing.Point(34, 4)
        Me.lblTotalPaymentHistory.Name = "lblTotalPaymentHistory"
        Me.lblTotalPaymentHistory.Size = New System.Drawing.Size(58, 26)
        Me.lblTotalPaymentHistory.TabIndex = 127
        Me.lblTotalPaymentHistory.Text = "Total :"
        Me.lblTotalPaymentHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTotalPaymentHistory.Visible = False
        '
        'BirthListArticleDtlsTableAdapter1
        '
        Me.BirthListArticleDtlsTableAdapter1.ClearBeforeFill = True
        '
        'POSDBDataSet
        '
        Me.POSDBDataSet.DataSetName = "POSDBDataSet"
        Me.POSDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'BirthListTableAdapter1
        '
        Me.BirthListTableAdapter1.ClearBeforeFill = True
        '
        'BirthListRequestedItemsTableAdapter1
        '
        Me.BirthListRequestedItemsTableAdapter1.ClearBeforeFill = True
        '
        'ClpCustomersTableAdapter1
        '
        Me.ClpCustomersTableAdapter1.ClearBeforeFill = True
        '
        'CustomerAddressTableAdapter1
        '
        Me.CustomerAddressTableAdapter1.ClearBeforeFill = True
        '
        'CustomerSaleOrderTableAdapter1
        '
        Me.CustomerSaleOrderTableAdapter1.ClearBeforeFill = True
        '
        'BirthListArticleDtlsTableAdapter2
        '
        Me.BirthListArticleDtlsTableAdapter2.ClearBeforeFill = True
        '
        'lblBirthListStatusCal1
        '
        Me.lblBirthListStatusCal1.AttachedTextBoxName = Nothing
        Me.lblBirthListStatusCal1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblBirthListStatusCal1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBirthListStatusCal1.ForeColor = System.Drawing.Color.Black
        Me.lblBirthListStatusCal1.Location = New System.Drawing.Point(550, 102)
        Me.lblBirthListStatusCal1.Name = "lblBirthListStatusCal1"
        Me.lblBirthListStatusCal1.Size = New System.Drawing.Size(162, 27)
        Me.lblBirthListStatusCal1.TabIndex = 75
        Me.lblBirthListStatusCal1.Tag = Nothing
        Me.lblBirthListStatusCal1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBirthListStatusCal1.TextDetached = True
        Me.lblBirthListStatusCal1.TrimStart = True
        Me.lblBirthListStatusCal1.Visible = False
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.C1SuperTooltip1.Opacity = 1.0R
        '
        'CtrlProductImage1
        '
        Me.CtrlProductImage1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlProductImage1.Location = New System.Drawing.Point(597, 282)
        Me.CtrlProductImage1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrlProductImage1.Name = "CtrlProductImage1"
        Me.CtrlProductImage1.Size = New System.Drawing.Size(195, 140)
        Me.CtrlProductImage1.TabIndex = 77
        Me.CtrlProductImage1.TabStop = False
        '
        'frmNBirthListUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 595)
        Me.Controls.Add(Me.CtrlProductImage1)
        Me.Controls.Add(Me.lblBirthListStatusCal1)
        Me.Controls.Add(Me.CtrlTab1)
        Me.Controls.Add(Me.C1Sizer2)
        Me.Controls.Add(Me.CtrlCustDtls1)
        Me.Controls.Add(Me.CtrlCashSummary1)
        Me.Controls.Add(Me.C1Sizer1)
        Me.Controls.Add(Me.CtrlRbn1)
        Me.Name = "frmNBirthListUpdate"
        Me.Text = "Edit BirthList"
        Me.Controls.SetChildIndex(Me.C1StatusBar1, 0)
        Me.Controls.SetChildIndex(Me.CtrlRbn1, 0)
        Me.Controls.SetChildIndex(Me.C1Sizer1, 0)
        Me.Controls.SetChildIndex(Me.CtrlCashSummary1, 0)
        Me.Controls.SetChildIndex(Me.CtrlCustDtls1, 0)
        Me.Controls.SetChildIndex(Me.C1Sizer2, 0)
        Me.Controls.SetChildIndex(Me.CtrlTab1, 0)
        Me.Controls.SetChildIndex(Me.lblBirthListStatusCal1, 0)
        Me.Controls.SetChildIndex(Me.CtrlProductImage1, 0)
        CType(Me.C1StatusBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlRbn1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer1.ResumeLayout(False)
        CType(Me.cboEvent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBirthListid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.c1dateEditDeliverydate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.c1dateEditEventDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlTextBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Sizer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer2.ResumeLayout(False)
        CType(Me.CtrlTab1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CtrlTab1.ResumeLayout(False)
        Me.C1DockingTabPage1.ResumeLayout(False)
        CType(Me.c1SizerGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.c1SizerGrid.ResumeLayout(False)
        CType(Me.grdScanItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1DockingTabPage2.ResumeLayout(False)
        Me.grpBoxGV.ResumeLayout(False)
        CType(Me.gridGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1DockingTabPage3.ResumeLayout(False)
        CType(Me.gridPaymentHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.POSDBDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBirthListStatusCal1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CtrlRbn1 As CtrlRbn
    Friend WithEvents RibbonApplicationMenu1 As C1.Win.C1Ribbon.RibbonApplicationMenu
    Friend WithEvents RibbonConfigToolBar1 As C1.Win.C1Ribbon.RibbonConfigToolBar
    Friend WithEvents RibbonQat1 As C1.Win.C1Ribbon.RibbonQat
    Friend WithEvents rbTabBL As C1.Win.C1Ribbon.RibbonTab
    Friend WithEvents rbGrpBirthList As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents rbnbtnNewBL As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents btnClose As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents btnCancel As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbgrpBLSave As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents btnSaveAndPrint As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents btnPrint As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbgrpCustSearch As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents rbnbtnSearchBL As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbnGrpStockCheck As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents rbnbtnstockCheck As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents C1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents c1dateEditDeliverydate As ctrlDate
    Friend WithEvents c1dateEditEventDate As ctrlDate
    Friend WithEvents CtrlTextBox1 As CtrlTextBox
    Friend WithEvents CtrlLabel4 As CtrlLabel
    Friend WithEvents CtrlLabel3 As CtrlLabel
    Friend WithEvents CtrlLabel2 As CtrlLabel
    Friend WithEvents CtrlLabel1 As CtrlLabel
    Friend WithEvents CtrlHeader1 As CtrlHeader
    Friend WithEvents CtrlCashSummary1 As Spectrum.CtrlCashSummary
    Friend WithEvents lblBirthListStatusCal As C1.Win.C1Ribbon.RibbonLabel
    Friend WithEvents rbnLblDocNbrCaption As C1.Win.C1Ribbon.RibbonLabel
    Friend WithEvents CtrlCustDtls1 As Spectrum.CtrlCustDtls
    Friend WithEvents CtrlLabel5 As Spectrum.CtrlLabel
    Friend WithEvents txtBirthListid As Spectrum.CtrlTextBox
    Friend WithEvents btnSearchBirthListID As Spectrum.CtrlBtn
    Friend WithEvents C1Sizer2 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents btnReturns As Spectrum.CtrlBtn
    Friend WithEvents btnPurchaseQty As Spectrum.CtrlBtn
    Friend WithEvents CtrlTab1 As Spectrum.CtrlTab
    Friend WithEvents C1DockingTabPage1 As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents c1SizerGrid As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents CtrlSalesPerson1 As Spectrum.CtrlSalesPerson
    Friend WithEvents C1DockingTabPage2 As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents C1DockingTabPage3 As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents btnApplyDiscountAndClose1 As Spectrum.CtrlBtn
    Friend WithEvents BirthListArticleDtlsTableAdapter1 As SpectrumBL.POSDBDataSetTableAdapters.BirthListArticleDtlsTableAdapter
    Friend WithEvents BirthListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents POSDBDataSet As SpectrumBL.POSDBDataSet
    Friend WithEvents BirthListRequestedItemsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents CustomerAddressBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents CustomerSaleOrderBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents BirthListArticleDtlsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents BirthListTableAdapter1 As SpectrumBL.POSDBDataSetTableAdapters.BirthListTableAdapter
    Friend WithEvents BirthListRequestedItemsTableAdapter1 As SpectrumBL.POSDBDataSetTableAdapters.BirthListRequestedItemsTableAdapter
    Friend WithEvents ClpCustomersTableAdapter1 As SpectrumBL.POSDBDataSetTableAdapters.CLPCustomersTableAdapter
    Friend WithEvents CustomerAddressTableAdapter1 As SpectrumBL.POSDBDataSetTableAdapters.CustomerAddressTableAdapter
    Friend WithEvents CustomerSaleOrderTableAdapter1 As SpectrumBL.POSDBDataSetTableAdapters.CustomerSaleOrderTableAdapter
    Friend WithEvents BirthListArticleDtlsTableAdapter2 As SpectrumBL.POSDBDataSetTableAdapters.BirthListArticleDtlsTableAdapter
    Friend WithEvents grdScanItem As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents gridGV As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents grpBoxGV As System.Windows.Forms.GroupBox
    Friend WithEvents lblGVCalTotalAmout As System.Windows.Forms.Label
    Friend WithEvents lblGVTotalAmount As System.Windows.Forms.Label
    Friend WithEvents btnReturnCancel As Spectrum.CtrlBtn
    Friend WithEvents BtnReturnSave As Spectrum.CtrlBtn
    Friend WithEvents cboEvent As C1.Win.C1List.C1Combo
    Friend WithEvents lblBirthListStatusCal1 As Spectrum.CtrlLabel
    Friend WithEvents btnCovertToOpenAmount As Spectrum.CtrlBtn
    Friend WithEvents btnPickUpQty As Spectrum.CtrlBtn
    Friend WithEvents gridPaymentHistory As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalItemQtyPaymentHistory As System.Windows.Forms.Label
    Friend WithEvents lblCalTotalAmountPaymentHistory As System.Windows.Forms.Label
    Friend WithEvents lblCalTotalItemQtyPaymentHistory As System.Windows.Forms.Label
    Friend WithEvents lblCalTotalItemPaymentHistory As System.Windows.Forms.Label
    Friend WithEvents lblTotalAmountPaymentHistory As System.Windows.Forms.Label
    Friend WithEvents lblTotalItemPaymentHistory As System.Windows.Forms.Label
    Friend WithEvents lblTotalPaymentHistory As System.Windows.Forms.Label
    Friend WithEvents btnShowAllclmGrid As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents RibbonSeparator1 As C1.Win.C1Ribbon.RibbonSeparator
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents rbtnSaleBirthList As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents CtrlProductImage1 As Spectrum.CtrlProductImage
End Class
