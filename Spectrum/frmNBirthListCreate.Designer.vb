<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNBirthListCreate
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
        Dim RibbonApplicationMenu1 As C1.Win.C1Ribbon.RibbonApplicationMenu
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNBirthListCreate))
        Me.CtrlRbn1 = New Spectrum.CtrlRbn()
        Me.RibbonConfigToolBar1 = New C1.Win.C1Ribbon.RibbonConfigToolBar()
        Me.lblBirthListNo = New C1.Win.C1Ribbon.RibbonLabel()
        Me.lblCalBirthListNo = New C1.Win.C1Ribbon.RibbonLabel()
        Me.RibbonSeparator1 = New C1.Win.C1Ribbon.RibbonSeparator()
        Me.RibbonQat1 = New C1.Win.C1Ribbon.RibbonQat()
        Me.rbnTabBL = New C1.Win.C1Ribbon.RibbonTab()
        Me.rbngrpBL = New C1.Win.C1Ribbon.RibbonGroup()
        Me.btnCreateNew = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbbtnEdit = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbbtnCancelBL = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbbtnBLSales = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbnGrpSave = New C1.Win.C1Ribbon.RibbonGroup()
        Me.rbbtnSaveBL = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbbtnPrintBL = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbnGrpCustSearch = New C1.Win.C1Ribbon.RibbonGroup()
        Me.rbbtnclpSearch = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbbtnsoSearch = New C1.Win.C1Ribbon.RibbonButton()
        Me.rbnGrpStock = New C1.Win.C1Ribbon.RibbonGroup()
        Me.rbbtnstockcheck = New C1.Win.C1Ribbon.RibbonButton()
        Me.CtrlProductImage1 = New Spectrum.CtrlProductImage()
        Me.c1SizerGrid = New C1.Win.C1Sizer.C1Sizer()
        Me.grdItemDetails = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.CtrlSalesPerson1 = New Spectrum.CtrlSalesPerson()
        Me.CtrlCashSummary1 = New Spectrum.CtrlCashSummary()
        Me.C1Sizer1 = New C1.Win.C1Sizer.C1Sizer()
        Me.dateCDelivery = New Spectrum.ctrlDate()
        Me.dateCEvent = New Spectrum.ctrlDate()
        Me.cboEvent = New C1.Win.C1List.C1Combo()
        Me.rtxtRemark = New Spectrum.CtrlTextBox()
        Me.CtrlLabel4 = New Spectrum.CtrlLabel()
        Me.CtrlLabel3 = New Spectrum.CtrlLabel()
        Me.CtrlLabel2 = New Spectrum.CtrlLabel()
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        Me.CtrlHeader1 = New Spectrum.CtrlHeader()
        Me.CtrlCustSearch1 = New Spectrum.CtrlCustSearch()
        Me.CtrlCustDtls1 = New Spectrum.CtrlCustDtls()
        RibbonApplicationMenu1 = New C1.Win.C1Ribbon.RibbonApplicationMenu()
        CType(Me.C1StatusBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlRbn1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.c1SizerGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.c1SizerGrid.SuspendLayout()
        CType(Me.grdItemDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer1.SuspendLayout()
        CType(Me.dateCDelivery, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dateCEvent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboEvent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblLoggedIn
        '
        Me.lblLoggedIn.Text = Nothing
        '
        'RibbonApplicationMenu1
        '
        RibbonApplicationMenu1.LargeImage = Global.Spectrum.My.Resources.Resources.logo
        RibbonApplicationMenu1.Name = "RibbonApplicationMenu1"
        '
        'CtrlRbn1
        '
        Me.CtrlRbn1.ApplicationMenuHolder = RibbonApplicationMenu1
        Me.CtrlRbn1.ConfigToolBarHolder = Me.RibbonConfigToolBar1
        Me.CtrlRbn1.Name = "CtrlRbn1"
        Me.CtrlRbn1.QatHolder = Me.RibbonQat1
        Me.CtrlRbn1.Tabs.Add(Me.rbnTabBL)
        Me.CtrlRbn1.ToolTipSettings.IsBalloon = True
        '
        'RibbonConfigToolBar1
        '
        Me.RibbonConfigToolBar1.Items.Add(Me.lblBirthListNo)
        Me.RibbonConfigToolBar1.Items.Add(Me.lblCalBirthListNo)
        Me.RibbonConfigToolBar1.Items.Add(Me.RibbonSeparator1)
        Me.RibbonConfigToolBar1.Name = "RibbonConfigToolBar1"
        '
        'lblBirthListNo
        '
        Me.lblBirthListNo.Name = "lblBirthListNo"
        Me.lblBirthListNo.Text = "BirthList No:"
        '
        'lblCalBirthListNo
        '
        Me.lblCalBirthListNo.Name = "lblCalBirthListNo"
        '
        'RibbonSeparator1
        '
        Me.RibbonSeparator1.Name = "RibbonSeparator1"
        '
        'RibbonQat1
        '
        Me.RibbonQat1.Name = "RibbonQat1"
        '
        'rbnTabBL
        '
        Me.rbnTabBL.Groups.Add(Me.rbngrpBL)
        Me.rbnTabBL.Groups.Add(Me.rbnGrpSave)
        Me.rbnTabBL.Groups.Add(Me.rbnGrpCustSearch)
        Me.rbnTabBL.Groups.Add(Me.rbnGrpStock)
        Me.rbnTabBL.Name = "rbnTabBL"
        Me.rbnTabBL.Text = "BirthList"
        '
        'rbngrpBL
        '
        Me.rbngrpBL.Items.Add(Me.btnCreateNew)
        Me.rbngrpBL.Items.Add(Me.rbbtnEdit)
        Me.rbngrpBL.Items.Add(Me.rbbtnCancelBL)
        Me.rbngrpBL.Items.Add(Me.rbbtnBLSales)
        Me.rbngrpBL.Name = "rbngrpBL"
        Me.rbngrpBL.Text = "BirthList"
        '
        'btnCreateNew
        '
        Me.btnCreateNew.Description = "Create New BirthList"
        Me.btnCreateNew.LargeImage = Global.Spectrum.My.Resources.Resources.Birth_list_new
        Me.btnCreateNew.Name = "btnCreateNew"
        Me.btnCreateNew.ShortcutKeyDisplayString = "Ctrl+N"
        Me.btnCreateNew.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.btnCreateNew.SmallImage = CType(resources.GetObject("btnCreateNew.SmallImage"), System.Drawing.Image)
        Me.btnCreateNew.Text = "&New BirthList"
        Me.btnCreateNew.ToolTip = "Create New Birth List"
        '
        'rbbtnEdit
        '
        Me.rbbtnEdit.Description = "Update Open Brithlist"
        Me.rbbtnEdit.LargeImage = Global.Spectrum.My.Resources.Resources.Birth_list_edit
        Me.rbbtnEdit.Name = "rbbtnEdit"
        Me.rbbtnEdit.SmallImage = CType(resources.GetObject("rbbtnEdit.SmallImage"), System.Drawing.Image)
        Me.rbbtnEdit.Text = "&Edit BirthList"
        '
        'rbbtnCancelBL
        '
        Me.rbbtnCancelBL.AllowSelection = True
        Me.rbbtnCancelBL.Description = "Cancel BirthList"
        Me.rbbtnCancelBL.LargeImage = Global.Spectrum.My.Resources.Resources.Birth_list_cancel
        Me.rbbtnCancelBL.Name = "rbbtnCancelBL"
        Me.rbbtnCancelBL.SmallImage = CType(resources.GetObject("rbbtnCancelBL.SmallImage"), System.Drawing.Image)
        Me.rbbtnCancelBL.Text = "Cance&L BirthList"
        '
        'rbbtnBLSales
        '
        Me.rbbtnBLSales.Description = "Sales Against BirthList"
        Me.rbbtnBLSales.LargeImage = Global.Spectrum.My.Resources.Resources.Birth_list_sale
        Me.rbbtnBLSales.Name = "rbbtnBLSales"
        Me.rbbtnBLSales.Text = "Sale  Birthlist"
        Me.rbbtnBLSales.ToolTip = "Sales Against BirthList"
        '
        'rbnGrpSave
        '
        Me.rbnGrpSave.Items.Add(Me.rbbtnSaveBL)
        Me.rbnGrpSave.Items.Add(Me.rbbtnPrintBL)
        Me.rbnGrpSave.Name = "rbnGrpSave"
        Me.rbnGrpSave.Text = "Save And Print"
        '
        'rbbtnSaveBL
        '
        Me.rbbtnSaveBL.Description = "Save BirthList"
        Me.rbbtnSaveBL.LargeImage = Global.Spectrum.My.Resources.Resources.save_bl
        Me.rbbtnSaveBL.Name = "rbbtnSaveBL"
        Me.rbbtnSaveBL.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.rbbtnSaveBL.SmallImage = CType(resources.GetObject("rbbtnSaveBL.SmallImage"), System.Drawing.Image)
        Me.rbbtnSaveBL.Text = "&Save BirthList"
        Me.rbbtnSaveBL.ToolTip = "Save New BirthList"
        '
        'rbbtnPrintBL
        '
        Me.rbbtnPrintBL.Description = "Print Brith List Items"
        Me.rbbtnPrintBL.LargeImage = Global.Spectrum.My.Resources.Resources.print_bl
        Me.rbbtnPrintBL.Name = "rbbtnPrintBL"
        Me.rbbtnPrintBL.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.rbbtnPrintBL.SmallImage = CType(resources.GetObject("rbbtnPrintBL.SmallImage"), System.Drawing.Image)
        Me.rbbtnPrintBL.Text = "&Print Birthlist List"
        Me.rbbtnPrintBL.ToolTip = "Print Item List of Current BirthList"
        '
        'rbnGrpCustSearch
        '
        Me.rbnGrpCustSearch.Items.Add(Me.rbbtnclpSearch)
        Me.rbnGrpCustSearch.Items.Add(Me.rbbtnsoSearch)
        Me.rbnGrpCustSearch.Name = "rbnGrpCustSearch"
        Me.rbnGrpCustSearch.Text = "Customer Searh"
        Me.rbnGrpCustSearch.Visible = False
        '
        'rbbtnclpSearch
        '
        Me.rbbtnclpSearch.Description = "Search Customer whose Member of Customer Loyalty Programme"
        Me.rbbtnclpSearch.LargeImage = Global.Spectrum.My.Resources.Resources.customer_search
        Me.rbbtnclpSearch.Name = "rbbtnclpSearch"
        Me.rbbtnclpSearch.SmallImage = CType(resources.GetObject("rbbtnclpSearch.SmallImage"), System.Drawing.Image)
        Me.rbbtnclpSearch.Text = "Search CL&P Customer"
        Me.rbbtnclpSearch.ToolTip = "Search Customer Member of CLP"
        '
        'rbbtnsoSearch
        '
        Me.rbbtnsoSearch.Description = "Search Other Customer"
        Me.rbbtnsoSearch.LargeImage = Global.Spectrum.My.Resources.Resources.customer_search
        Me.rbbtnsoSearch.Name = "rbbtnsoSearch"
        Me.rbbtnsoSearch.SmallImage = CType(resources.GetObject("rbbtnsoSearch.SmallImage"), System.Drawing.Image)
        Me.rbbtnsoSearch.Text = "Search &Other Customer"
        Me.rbbtnsoSearch.ToolTip = "Search Other Customer"
        Me.rbbtnsoSearch.Visible = False
        '
        'rbnGrpStock
        '
        Me.rbnGrpStock.Items.Add(Me.rbbtnstockcheck)
        Me.rbnGrpStock.Name = "rbnGrpStock"
        Me.rbnGrpStock.Text = "Stock Check"
        '
        'rbbtnstockcheck
        '
        Me.rbbtnstockcheck.Description = "Stock Check From Other Sites"
        Me.rbbtnstockcheck.LargeImage = Global.Spectrum.My.Resources.Resources.stock_check
        Me.rbbtnstockcheck.Name = "rbbtnstockcheck"
        Me.rbbtnstockcheck.SmallImage = CType(resources.GetObject("rbbtnstockcheck.SmallImage"), System.Drawing.Image)
        Me.rbbtnstockcheck.Text = "Stock &Check"
        Me.rbbtnstockcheck.ToolTip = "Stock Check from All Sites in Network"
        '
        'CtrlProductImage1
        '
        Me.CtrlProductImage1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlProductImage1.Location = New System.Drawing.Point(611, 306)
        Me.CtrlProductImage1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrlProductImage1.Name = "CtrlProductImage1"
        Me.CtrlProductImage1.Size = New System.Drawing.Size(178, 134)
        Me.CtrlProductImage1.TabIndex = 2
        Me.CtrlProductImage1.TabStop = False
        '
        'c1SizerGrid
        '
        Me.c1SizerGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.c1SizerGrid.Border.Color = System.Drawing.Color.LightSlateGray
        Me.c1SizerGrid.Border.Corners = New C1.Win.C1Sizer.Corners(0, 0, 0, 4)
        Me.c1SizerGrid.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.c1SizerGrid.Controls.Add(Me.grdItemDetails)
        Me.c1SizerGrid.Controls.Add(Me.CtrlSalesPerson1)
        Me.c1SizerGrid.GridDefinition = "8.94308943089431:False:True;85.3658536585366:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "19.8675496688742:False:" & _
    "True;28.6423841059603:False:False;39.2384105960265:False:False;8.60927152317881:" & _
    "False:True;"
        Me.c1SizerGrid.Location = New System.Drawing.Point(1, 307)
        Me.c1SizerGrid.Name = "c1SizerGrid"
        Me.c1SizerGrid.Size = New System.Drawing.Size(604, 246)
        Me.c1SizerGrid.TabIndex = 2
        Me.c1SizerGrid.TabStop = False
        Me.c1SizerGrid.Text = "C1Sizer1"
        '
        'grdItemDetails
        '
        Me.grdItemDetails.AllowDelete = True
        Me.grdItemDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItemDetails.AutoGenerateColumns = False
        Me.grdItemDetails.CellButtonImage = Global.Spectrum.My.Resources.Resources.del_icon
        Me.grdItemDetails.ColumnInfo = resources.GetString("grdItemDetails.ColumnInfo")
        Me.grdItemDetails.ExtendLastCol = True
        Me.grdItemDetails.Location = New System.Drawing.Point(5, 31)
        Me.grdItemDetails.Name = "grdItemDetails"
        Me.grdItemDetails.Rows.Count = 1
        Me.grdItemDetails.Rows.DefaultSize = 18
        Me.grdItemDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.grdItemDetails.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.grdItemDetails.Size = New System.Drawing.Size(594, 210)
        Me.grdItemDetails.TabIndex = 1
        Me.grdItemDetails.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'CtrlSalesPerson1
        '
        Me.CtrlSalesPerson1.Location = New System.Drawing.Point(5, 5)
        Me.CtrlSalesPerson1.Name = "CtrlSalesPerson1"
        Me.CtrlSalesPerson1.Size = New System.Drawing.Size(594, 22)
        Me.CtrlSalesPerson1.TabIndex = 0
        '
        'CtrlCashSummary1
        '
        Me.CtrlCashSummary1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlCashSummary1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlCashSummary1.hdrText = "Summary"
        Me.CtrlCashSummary1.lbl1 = "Total Item"
        Me.CtrlCashSummary1.lbl10 = "Total Amt"
        Me.CtrlCashSummary1.lbl2 = "Total Qty"
        Me.CtrlCashSummary1.lbl3 = "Reserved Qty"
        Me.CtrlCashSummary1.lbl4 = ""
        Me.CtrlCashSummary1.lbl5 = ""
        Me.CtrlCashSummary1.lbl6 = ""
        Me.CtrlCashSummary1.lbl7 = ""
        Me.CtrlCashSummary1.lbl8 = ""
        Me.CtrlCashSummary1.lbl9 = "0"
        Me.CtrlCashSummary1.lbltxt1 = "0"
        Me.CtrlCashSummary1.lbltxt10 = "0"
        Me.CtrlCashSummary1.lbltxt2 = "0"
        Me.CtrlCashSummary1.lbltxt3 = "0"
        Me.CtrlCashSummary1.lbltxt4 = "0"
        Me.CtrlCashSummary1.lbltxt5 = ""
        Me.CtrlCashSummary1.lbltxt6 = ""
        Me.CtrlCashSummary1.lbltxt7 = ""
        Me.CtrlCashSummary1.lbltxt8 = ""
        Me.CtrlCashSummary1.lbltxt9 = ""
        Me.CtrlCashSummary1.lbltxtVisible1 = True
        Me.CtrlCashSummary1.lbltxtVisible10 = True
        Me.CtrlCashSummary1.lbltxtVisible2 = True
        Me.CtrlCashSummary1.lbltxtVisible3 = True
        Me.CtrlCashSummary1.lbltxtVisible4 = True
        Me.CtrlCashSummary1.lbltxtVisible5 = True
        Me.CtrlCashSummary1.lbltxtVisible6 = True
        Me.CtrlCashSummary1.lbltxtVisible7 = True
        Me.CtrlCashSummary1.lbltxtVisible8 = True
        Me.CtrlCashSummary1.lbltxtVisible9 = True
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
        Me.CtrlCashSummary1.Location = New System.Drawing.Point(611, 443)
        Me.CtrlCashSummary1.Name = "CtrlCashSummary1"
        Me.CtrlCashSummary1.RowCount = 5
        Me.CtrlCashSummary1.Size = New System.Drawing.Size(178, 110)
        Me.CtrlCashSummary1.TabIndex = 57
        Me.CtrlCashSummary1.TabStop = False
        '
        'C1Sizer1
        '
        Me.C1Sizer1.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1Sizer1.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1Sizer1.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1Sizer1.Controls.Add(Me.dateCDelivery)
        Me.C1Sizer1.Controls.Add(Me.dateCEvent)
        Me.C1Sizer1.Controls.Add(Me.cboEvent)
        Me.C1Sizer1.Controls.Add(Me.rtxtRemark)
        Me.C1Sizer1.Controls.Add(Me.CtrlLabel4)
        Me.C1Sizer1.Controls.Add(Me.CtrlLabel3)
        Me.C1Sizer1.Controls.Add(Me.CtrlLabel2)
        Me.C1Sizer1.Controls.Add(Me.CtrlLabel1)
        Me.C1Sizer1.Controls.Add(Me.CtrlHeader1)
        Me.C1Sizer1.GridDefinition = resources.GetString("C1Sizer1.GridDefinition")
        Me.C1Sizer1.Location = New System.Drawing.Point(4, 155)
        Me.C1Sizer1.Name = "C1Sizer1"
        Me.C1Sizer1.Padding = New System.Windows.Forms.Padding(1)
        Me.C1Sizer1.Size = New System.Drawing.Size(243, 149)
        Me.C1Sizer1.SplitterWidth = 0
        Me.C1Sizer1.TabIndex = 0
        Me.C1Sizer1.Text = "C1Sizer1"
        '
        'dateCDelivery
        '
        Me.dateCDelivery.AutoSize = False
        Me.dateCDelivery.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.dateCDelivery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dateCDelivery.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage"), System.Drawing.Image)
        '
        '
        '
        Me.dateCDelivery.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dateCDelivery.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.dateCDelivery.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.dateCDelivery.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dateCDelivery.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dateCDelivery.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dateCDelivery.Location = New System.Drawing.Point(65, 79)
        Me.dateCDelivery.Margin = New System.Windows.Forms.Padding(0)
        Me.dateCDelivery.Name = "dateCDelivery"
        Me.dateCDelivery.Size = New System.Drawing.Size(176, 21)
        Me.dateCDelivery.TabIndex = 4
        Me.dateCDelivery.Tag = Nothing
        Me.dateCDelivery.TrimStart = True
        Me.dateCDelivery.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.dateCDelivery.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'dateCEvent
        '
        Me.dateCEvent.AutoSize = False
        Me.dateCEvent.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.dateCEvent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dateCEvent.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage1"), System.Drawing.Image)
        '
        '
        '
        Me.dateCEvent.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dateCEvent.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.dateCEvent.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.dateCEvent.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dateCEvent.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dateCEvent.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dateCEvent.Location = New System.Drawing.Point(65, 56)
        Me.dateCEvent.Name = "dateCEvent"
        Me.dateCEvent.Size = New System.Drawing.Size(176, 23)
        Me.dateCEvent.TabIndex = 3
        Me.dateCEvent.Tag = Nothing
        Me.dateCEvent.TrimStart = True
        Me.dateCEvent.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.dateCEvent.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
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
        Me.cboEvent.ContentHeight = 18
        Me.cboEvent.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboEvent.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboEvent.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEvent.EditorForeColor = System.Drawing.Color.Black
        Me.cboEvent.EditorHeight = 18
        Me.cboEvent.Images.Add(CType(resources.GetObject("cboEvent.Images"), System.Drawing.Image))
        Me.cboEvent.ItemHeight = 15
        Me.cboEvent.Location = New System.Drawing.Point(65, 32)
        Me.cboEvent.MatchEntryTimeout = CType(2000, Long)
        Me.cboEvent.MaxDropDownItems = CType(5, Short)
        Me.cboEvent.MaxLength = 32767
        Me.cboEvent.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboEvent.Name = "cboEvent"
        Me.cboEvent.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboEvent.Size = New System.Drawing.Size(176, 24)
        Me.cboEvent.TabIndex = 2
        Me.cboEvent.Text = "C1Combo1"
        Me.cboEvent.PropBag = resources.GetString("cboEvent.PropBag")
        '
        'rtxtRemark
        '
        Me.rtxtRemark.AutoSize = False
        Me.rtxtRemark.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.rtxtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtxtRemark.Location = New System.Drawing.Point(65, 100)
        Me.rtxtRemark.MinimumSize = New System.Drawing.Size(10, 21)
        Me.rtxtRemark.Name = "rtxtRemark"
        Me.rtxtRemark.Size = New System.Drawing.Size(176, 47)
        Me.rtxtRemark.TabIndex = 5
        Me.rtxtRemark.Tag = Nothing
        Me.rtxtRemark.TextDetached = True
        Me.rtxtRemark.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.rtxtRemark.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel4
        '
        Me.CtrlLabel4.AttachedTextBoxName = Nothing
        Me.CtrlLabel4.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel4.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel4.Location = New System.Drawing.Point(2, 100)
        Me.CtrlLabel4.Name = "CtrlLabel4"
        Me.CtrlLabel4.Size = New System.Drawing.Size(63, 23)
        Me.CtrlLabel4.TabIndex = 6
        Me.CtrlLabel4.Tag = Nothing
        Me.CtrlLabel4.Text = "Remarks"
        Me.CtrlLabel4.TextDetached = True
        Me.CtrlLabel4.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel3
        '
        Me.CtrlLabel3.AttachedTextBoxName = Nothing
        Me.CtrlLabel3.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel3.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel3.Location = New System.Drawing.Point(2, 79)
        Me.CtrlLabel3.Name = "CtrlLabel3"
        Me.CtrlLabel3.Size = New System.Drawing.Size(63, 21)
        Me.CtrlLabel3.TabIndex = 7
        Me.CtrlLabel3.Tag = Nothing
        Me.CtrlLabel3.Text = "Delivery Date"
        Me.CtrlLabel3.TextDetached = True
        Me.CtrlLabel3.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel2
        '
        Me.CtrlLabel2.AttachedTextBoxName = Nothing
        Me.CtrlLabel2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel2.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel2.Location = New System.Drawing.Point(2, 56)
        Me.CtrlLabel2.Name = "CtrlLabel2"
        Me.CtrlLabel2.Size = New System.Drawing.Size(63, 23)
        Me.CtrlLabel2.TabIndex = 8
        Me.CtrlLabel2.Tag = Nothing
        Me.CtrlLabel2.Text = "Event Date"
        Me.CtrlLabel2.TextDetached = True
        Me.CtrlLabel2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel1
        '
        Me.CtrlLabel1.AttachedTextBoxName = Nothing
        Me.CtrlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel1.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel1.Location = New System.Drawing.Point(2, 32)
        Me.CtrlLabel1.Name = "CtrlLabel1"
        Me.CtrlLabel1.Size = New System.Drawing.Size(63, 24)
        Me.CtrlLabel1.TabIndex = 1
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.Text = "Event Name"
        Me.CtrlLabel1.TextDetached = True
        Me.CtrlLabel1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlHeader1
        '
        Me.CtrlHeader1.HdrText = "Event Details"
        Me.CtrlHeader1.Location = New System.Drawing.Point(2, 2)
        Me.CtrlHeader1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrlHeader1.Name = "CtrlHeader1"
        Me.CtrlHeader1.Padding = New System.Windows.Forms.Padding(1)
        Me.CtrlHeader1.Size = New System.Drawing.Size(239, 30)
        Me.CtrlHeader1.TabIndex = 0
        Me.CtrlHeader1.TabStop = False
        '
        'CtrlCustSearch1
        '
        Me.CtrlCustSearch1.AddressType = Nothing
        Me.CtrlCustSearch1.BorderColorVisible = True
        Me.CtrlCustSearch1.CardType = Nothing
        Me.CtrlCustSearch1.CustmType = "CLP"
        Me.CtrlCustSearch1.dtCustmInfo = Nothing
        Me.CtrlCustSearch1.Location = New System.Drawing.Point(253, 155)
        Me.CtrlCustSearch1.Name = "CtrlCustSearch1"
        Me.CtrlCustSearch1.pshowSwapeCard = True
        Me.CtrlCustSearch1.Size = New System.Drawing.Size(253, 117)
        Me.CtrlCustSearch1.TabIndex = 1
        '
        'CtrlCustDtls1
        '
        Me.CtrlCustDtls1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlCustDtls1.DisplayCustType = False
        Me.CtrlCustDtls1.dtCustmInfo = Nothing
        Me.CtrlCustDtls1.Location = New System.Drawing.Point(502, 157)
        Me.CtrlCustDtls1.Name = "CtrlCustDtls1"
        Me.CtrlCustDtls1.Size = New System.Drawing.Size(278, 149)
        Me.CtrlCustDtls1.TabIndex = 61
        Me.CtrlCustDtls1.TabStop = False
        '
        'frmNBirthListCreate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(792, 581)
        Me.Controls.Add(Me.CtrlCustDtls1)
        Me.Controls.Add(Me.C1Sizer1)
        Me.Controls.Add(Me.CtrlCashSummary1)
        Me.Controls.Add(Me.CtrlRbn1)
        Me.Controls.Add(Me.c1SizerGrid)
        Me.Controls.Add(Me.CtrlProductImage1)
        Me.Controls.Add(Me.CtrlCustSearch1)
        Me.IsDocToParent = True
        Me.MinimumSize = New System.Drawing.Size(798, 568)
        Me.Name = "frmNBirthListCreate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Create BirthList"
        Me.TopMost = True
        Me.Controls.SetChildIndex(Me.C1StatusBar1, 0)
        Me.Controls.SetChildIndex(Me.CtrlCustSearch1, 0)
        Me.Controls.SetChildIndex(Me.CtrlProductImage1, 0)
        Me.Controls.SetChildIndex(Me.c1SizerGrid, 0)
        Me.Controls.SetChildIndex(Me.CtrlRbn1, 0)
        Me.Controls.SetChildIndex(Me.CtrlCashSummary1, 0)
        Me.Controls.SetChildIndex(Me.C1Sizer1, 0)
        Me.Controls.SetChildIndex(Me.CtrlCustDtls1, 0)
        CType(Me.C1StatusBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlRbn1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.c1SizerGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.c1SizerGrid.ResumeLayout(False)
        CType(Me.grdItemDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer1.ResumeLayout(False)
        CType(Me.dateCDelivery, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dateCEvent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboEvent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CtrlRbn1 As CtrlRbn
    Friend WithEvents RibbonConfigToolBar1 As C1.Win.C1Ribbon.RibbonConfigToolBar
    Friend WithEvents RibbonQat1 As C1.Win.C1Ribbon.RibbonQat
    Friend WithEvents rbnTabBL As C1.Win.C1Ribbon.RibbonTab
    Friend WithEvents rbngrpBL As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents CtrlProductImage1 As CtrlProductImage
    Friend WithEvents rbnGrpSave As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents btnCreateNew As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbbtnEdit As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbbtnCancelBL As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbbtnSaveBL As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbbtnPrintBL As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents c1SizerGrid As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents CtrlSalesPerson1 As CtrlSalesPerson
    Friend WithEvents CtrlCashSummary1 As CtrlCashSummary
    Friend WithEvents C1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents CtrlLabel1 As CtrlLabel
    Friend WithEvents CtrlHeader1 As CtrlHeader
    Friend WithEvents cboEvent As C1.Win.C1List.C1Combo
    Friend WithEvents rtxtRemark As CtrlTextBox
    Friend WithEvents CtrlLabel4 As CtrlLabel
    Friend WithEvents CtrlLabel3 As CtrlLabel
    Friend WithEvents CtrlLabel2 As CtrlLabel
    Friend WithEvents rbnGrpCustSearch As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents rbbtnclpSearch As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents rbbtnsoSearch As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents dateCDelivery As ctrlDate
    Friend WithEvents dateCEvent As ctrlDate
    Friend WithEvents rbnGrpStock As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents rbbtnstockcheck As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents CtrlCustSearch1 As Spectrum.CtrlCustSearch
    Friend WithEvents grdItemDetails As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents lblBirthListNo As C1.Win.C1Ribbon.RibbonLabel
    Friend WithEvents lblCalBirthListNo As C1.Win.C1Ribbon.RibbonLabel
    Friend WithEvents RibbonSeparator1 As C1.Win.C1Ribbon.RibbonSeparator
    Friend WithEvents rbbtnBLSales As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents CtrlCustDtls1 As Spectrum.CtrlCustDtls

End Class
