<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNQuotationCancel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNQuotationCancel))
        Me.CtrlRbn1 = New Spectrum.CtrlRbn()
        Me.RibbonApplicationMenu1 = New C1.Win.C1Ribbon.RibbonApplicationMenu()
        Me.RibbonConfigToolBar1 = New C1.Win.C1Ribbon.RibbonConfigToolBar()
        Me.RibbonQat1 = New C1.Win.C1Ribbon.RibbonQat()
        Me.rbnTabSO = New C1.Win.C1Ribbon.RibbonTab()
        Me.RibbonGroup1 = New C1.Win.C1Ribbon.RibbonGroup()
        Me.cmdSONew = New C1.Win.C1Ribbon.RibbonButton()
        Me.CmdSOEdit = New C1.Win.C1Ribbon.RibbonButton()
        Me.BtnSOCancel = New C1.Win.C1Ribbon.RibbonButton()
        Me.RibbonGroup3 = New C1.Win.C1Ribbon.RibbonGroup()
        Me.rbbtnSave = New C1.Win.C1Ribbon.RibbonButton()
        Me.RibbonButton11 = New C1.Win.C1Ribbon.RibbonButton()
        Me.TabSalesOrder = New Spectrum.CtrlTab()
        Me.TabPageItemDetails = New C1.Win.C1Command.C1DockingTabPage()
        Me.C1Sizer3 = New C1.Win.C1Sizer.C1Sizer()
        Me.grdSOItems = New Spectrum.CtrlGrid()
        Me.CtrlSalesPerson = New Spectrum.CtrlSalesPerson()
        Me.TabPageInvoiceDetails = New C1.Win.C1Command.C1DockingTabPage()
        Me.grdSOInvoice = New Spectrum.CtrlGrid()
        Me.C1Sizer2 = New C1.Win.C1Sizer.C1Sizer()
        Me.CtrlBtnAddExtraCost = New Spectrum.CtrlBtn()
        Me.CtrlBtnStockCheck = New Spectrum.CtrlBtn()
        Me.CtrlCashSummary = New Spectrum.CtrlCashSummary()
        Me.CtrlSalesInfo = New Spectrum.CtrlSalesInfo()
        Me.CtrlCustDtls1 = New Spectrum.CtrlCustDtls()
        Me.CtrlProductImage = New Spectrum.CtrlProductImage()
        CType(Me.C1StatusBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlRbn1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabSalesOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabSalesOrder.SuspendLayout()
        Me.TabPageItemDetails.SuspendLayout()
        CType(Me.C1Sizer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer3.SuspendLayout()
        CType(Me.grdSOItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageInvoiceDetails.SuspendLayout()
        CType(Me.grdSOInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Sizer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer2.SuspendLayout()
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
        Me.rbnTabSO.Groups.Add(Me.RibbonGroup1)
        Me.rbnTabSO.Groups.Add(Me.RibbonGroup3)
        Me.rbnTabSO.Name = "rbnTabSO"
        Me.rbnTabSO.Text = "Sales Order"
        '
        'RibbonGroup1
        '
        Me.RibbonGroup1.Items.Add(Me.cmdSONew)
        Me.RibbonGroup1.Items.Add(Me.CmdSOEdit)
        Me.RibbonGroup1.Items.Add(Me.BtnSOCancel)
        Me.RibbonGroup1.Name = "RibbonGroup1"
        Me.RibbonGroup1.Text = "Sales Order Action"
        '
        'cmdSONew
        '
        Me.cmdSONew.Description = "New Sales Order"
        Me.cmdSONew.LargeImage = Global.Spectrum.My.Resources.Resources.quotation_create
        Me.cmdSONew.Name = "cmdSONew"
        Me.cmdSONew.ShortcutKeyDisplayString = "Ctrl+N"
        Me.cmdSONew.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.cmdSONew.SmallImage = CType(resources.GetObject("cmdSONew.SmallImage"), System.Drawing.Image)
        Me.cmdSONew.Text = "New   Sales Order(Ctrl+N)"
        '
        'CmdSOEdit
        '
        Me.CmdSOEdit.Description = "Update Open Sales Order"
        Me.CmdSOEdit.LargeImage = Global.Spectrum.My.Resources.Resources.quotation_update
        Me.CmdSOEdit.Name = "CmdSOEdit"
        Me.CmdSOEdit.ShortcutKeyDisplayString = "Ctrl+E"
        Me.CmdSOEdit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.CmdSOEdit.SmallImage = CType(resources.GetObject("CmdSOEdit.SmallImage"), System.Drawing.Image)
        Me.CmdSOEdit.Text = "Edit    Sales Order(Ctrl+E)"
        '
        'BtnSOCancel
        '
        Me.BtnSOCancel.Description = "Cancel Sales Order"
        Me.BtnSOCancel.LargeImage = Global.Spectrum.My.Resources.Resources.quotation_cancel
        Me.BtnSOCancel.Name = "BtnSOCancel"
        Me.BtnSOCancel.ShortcutKeyDisplayString = "Ctrl+X"
        Me.BtnSOCancel.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.BtnSOCancel.SmallImage = CType(resources.GetObject("BtnSOCancel.SmallImage"), System.Drawing.Image)
        Me.BtnSOCancel.Text = "Cancel Sales Order(Ctrl+X)"
        Me.BtnSOCancel.ToolTip = "Cancel Sales Order"
        '
        'RibbonGroup3
        '
        Me.RibbonGroup3.Items.Add(Me.rbbtnSave)
        Me.RibbonGroup3.Items.Add(Me.RibbonButton11)
        Me.RibbonGroup3.Name = "RibbonGroup3"
        Me.RibbonGroup3.Text = "Save And Print"
        '
        'rbbtnSave
        '
        Me.rbbtnSave.Description = "Save Sales Order"
        Me.rbbtnSave.LargeImage = CType(resources.GetObject("rbbtnSave.LargeImage"), System.Drawing.Image)
        Me.rbbtnSave.Name = "rbbtnSave"
        Me.rbbtnSave.SmallImage = CType(resources.GetObject("rbbtnSave.SmallImage"), System.Drawing.Image)
        Me.rbbtnSave.Text = "Save   Sales Order(Ctrl+S)"
        '
        'RibbonButton11
        '
        Me.RibbonButton11.Description = "Print Sales Order"
        Me.RibbonButton11.LargeImage = CType(resources.GetObject("RibbonButton11.LargeImage"), System.Drawing.Image)
        Me.RibbonButton11.Name = "RibbonButton11"
        Me.RibbonButton11.SmallImage = CType(resources.GetObject("RibbonButton11.SmallImage"), System.Drawing.Image)
        Me.RibbonButton11.Text = "Print   Sales Order(Ctrl+R)"
        '
        'TabSalesOrder
        '
        Me.TabSalesOrder.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabSalesOrder.Controls.Add(Me.TabPageItemDetails)
        Me.TabSalesOrder.Controls.Add(Me.TabPageInvoiceDetails)
        Me.TabSalesOrder.HotTrack = True
        Me.TabSalesOrder.Location = New System.Drawing.Point(3, 279)
        Me.TabSalesOrder.Name = "TabSalesOrder"
        Me.TabSalesOrder.SelectedIndex = 2
        Me.TabSalesOrder.SelectedTabBold = True
        Me.TabSalesOrder.ShowTabList = True
        Me.TabSalesOrder.ShowToolTips = True
        Me.TabSalesOrder.Size = New System.Drawing.Size(803, 215)
        Me.TabSalesOrder.TabIndex = 84
        Me.TabSalesOrder.TabsSpacing = 6
        Me.TabSalesOrder.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007
        Me.TabSalesOrder.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Blue
        '
        'TabPageItemDetails
        '
        Me.TabPageItemDetails.Controls.Add(Me.C1Sizer3)
        Me.TabPageItemDetails.Location = New System.Drawing.Point(1, 24)
        Me.TabPageItemDetails.Name = "TabPageItemDetails"
        Me.TabPageItemDetails.Size = New System.Drawing.Size(801, 190)
        Me.TabPageItemDetails.TabBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.TabPageItemDetails.TabBackColorSelected = System.Drawing.SystemColors.GradientActiveCaption
        Me.TabPageItemDetails.TabIndex = 0
        Me.TabPageItemDetails.Text = "Ordered Items"
        '
        'C1Sizer3
        '
        Me.C1Sizer3.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1Sizer3.Border.Corners = New C1.Win.C1Sizer.Corners(0, 0, 0, 4)
        Me.C1Sizer3.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1Sizer3.Controls.Add(Me.grdSOItems)
        Me.C1Sizer3.Controls.Add(Me.CtrlSalesPerson)
        Me.C1Sizer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Sizer3.GridDefinition = "11.5789473684211:False:True;81.0526315789474:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "14.9812734082397:False:" & _
    "True;31.585518102372:False:False;47.5655430711611:False:False;3.1210986267166:Fa" & _
    "lse:True;"
        Me.C1Sizer3.Location = New System.Drawing.Point(0, 0)
        Me.C1Sizer3.Name = "C1Sizer3"
        Me.C1Sizer3.Size = New System.Drawing.Size(801, 190)
        Me.C1Sizer3.TabIndex = 6
        Me.C1Sizer3.Text = "C1Sizer1"
        '
        'grdSOItems
        '
        Me.grdSOItems.CellButtonImage = CType(resources.GetObject("grdSOItems.CellButtonImage"), System.Drawing.Image)
        Me.grdSOItems.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:22;}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{Caption:""Item Code"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "2{Caption:""Item Descri" & _
    "ption"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "3{Caption:""Price"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "4{Caption:""Order Qty"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "5{Caption:""Net Amt"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "6{Cap" & _
    "tion:""Available Stock"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.grdSOItems.ExtendLastCol = True
        Me.grdSOItems.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.grdSOItems.Location = New System.Drawing.Point(5, 31)
        Me.grdSOItems.Name = "grdSOItems"
        Me.grdSOItems.Rows.DefaultSize = 17
        Me.grdSOItems.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.grdSOItems.Size = New System.Drawing.Size(791, 154)
        Me.grdSOItems.TabIndex = 70
        Me.grdSOItems.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'CtrlSalesPerson
        '
        Me.CtrlSalesPerson.Location = New System.Drawing.Point(5, 5)
        Me.CtrlSalesPerson.Name = "CtrlSalesPerson"
        Me.CtrlSalesPerson.Size = New System.Drawing.Size(791, 22)
        Me.CtrlSalesPerson.TabIndex = 0
        '
        'TabPageInvoiceDetails
        '
        Me.TabPageInvoiceDetails.Controls.Add(Me.grdSOInvoice)
        Me.TabPageInvoiceDetails.Location = New System.Drawing.Point(1, 24)
        Me.TabPageInvoiceDetails.Name = "TabPageInvoiceDetails"
        Me.TabPageInvoiceDetails.Size = New System.Drawing.Size(801, 190)
        Me.TabPageInvoiceDetails.TabBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.TabPageInvoiceDetails.TabBackColorSelected = System.Drawing.SystemColors.GradientActiveCaption
        Me.TabPageInvoiceDetails.TabIndex = 2
        Me.TabPageInvoiceDetails.Text = "Payment History"
        '
        'grdSOInvoice
        '
        Me.grdSOInvoice.CellButtonImage = CType(resources.GetObject("grdSOInvoice.CellButtonImage"), System.Drawing.Image)
        Me.grdSOInvoice.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:22;}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{Caption:""Item Code"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "2{Caption:""Item Descri" & _
    "ption"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "3{Caption:""Rate"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "4{Caption:""Order Qty"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "5{Caption:""Net Amt"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "6{Capt" & _
    "ion:""Available Stock"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.grdSOInvoice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSOInvoice.ExtendLastCol = True
        Me.grdSOInvoice.Location = New System.Drawing.Point(0, 0)
        Me.grdSOInvoice.Name = "grdSOInvoice"
        Me.grdSOInvoice.Rows.DefaultSize = 17
        Me.grdSOInvoice.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.grdSOInvoice.Size = New System.Drawing.Size(801, 190)
        Me.grdSOInvoice.TabIndex = 71
        Me.grdSOInvoice.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'C1Sizer2
        '
        Me.C1Sizer2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C1Sizer2.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1Sizer2.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1Sizer2.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1Sizer2.Controls.Add(Me.CtrlBtnAddExtraCost)
        Me.C1Sizer2.Controls.Add(Me.CtrlBtnStockCheck)
        Me.C1Sizer2.GridDefinition = resources.GetString("C1Sizer2.GridDefinition")
        Me.C1Sizer2.Location = New System.Drawing.Point(0, 497)
        Me.C1Sizer2.Name = "C1Sizer2"
        Me.C1Sizer2.Size = New System.Drawing.Size(803, 72)
        Me.C1Sizer2.TabIndex = 83
        '
        'CtrlBtnAddExtraCost
        '
        Me.CtrlBtnAddExtraCost.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlBtnAddExtraCost.Image = Global.Spectrum.My.Resources.Resources.addcost
        Me.CtrlBtnAddExtraCost.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CtrlBtnAddExtraCost.Location = New System.Drawing.Point(5, 5)
        Me.CtrlBtnAddExtraCost.Name = "CtrlBtnAddExtraCost"
        Me.CtrlBtnAddExtraCost.SetArticleCode = Nothing
        Me.CtrlBtnAddExtraCost.SetRowIndex = 0
        Me.CtrlBtnAddExtraCost.Size = New System.Drawing.Size(108, 62)
        Me.CtrlBtnAddExtraCost.TabIndex = 51
        Me.CtrlBtnAddExtraCost.Text = "&Add Cost"
        Me.CtrlBtnAddExtraCost.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CtrlBtnAddExtraCost.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CtrlBtnAddExtraCost.UseVisualStyleBackColor = True
        Me.CtrlBtnAddExtraCost.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlBtnStockCheck
        '
        Me.CtrlBtnStockCheck.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlBtnStockCheck.Image = Global.Spectrum.My.Resources.Resources.stock_check
        Me.CtrlBtnStockCheck.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CtrlBtnStockCheck.Location = New System.Drawing.Point(117, 5)
        Me.CtrlBtnStockCheck.Name = "CtrlBtnStockCheck"
        Me.CtrlBtnStockCheck.SetArticleCode = Nothing
        Me.CtrlBtnStockCheck.SetRowIndex = 0
        Me.CtrlBtnStockCheck.Size = New System.Drawing.Size(108, 62)
        Me.CtrlBtnStockCheck.TabIndex = 48
        Me.CtrlBtnStockCheck.Text = "Stock &Check"
        Me.CtrlBtnStockCheck.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CtrlBtnStockCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CtrlBtnStockCheck.UseVisualStyleBackColor = True
        Me.CtrlBtnStockCheck.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlCashSummary
        '
        Me.CtrlCashSummary.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlCashSummary.hdrText = "Total Summary"
        Me.CtrlCashSummary.lbl1 = "Gross Amt"
        Me.CtrlCashSummary.lbl10 = ""
        Me.CtrlCashSummary.lbl2 = "Disc Amt"
        Me.CtrlCashSummary.lbl3 = "Add Charges"
        Me.CtrlCashSummary.lbl4 = "Net Amt"
        Me.CtrlCashSummary.lbl5 = "Total Paid"
        Me.CtrlCashSummary.lbl6 = "Bal. Pay"
        Me.CtrlCashSummary.lbl7 = "Delivered Amt"
        Me.CtrlCashSummary.lbl8 = "Refund Amt"
        Me.CtrlCashSummary.lbl9 = "Bal. Refund Amt"
        Me.CtrlCashSummary.lbltxt1 = "0.00"
        Me.CtrlCashSummary.lbltxt10 = ""
        Me.CtrlCashSummary.lbltxt2 = "0.00"
        Me.CtrlCashSummary.lbltxt3 = "0.00"
        Me.CtrlCashSummary.lbltxt4 = "0.00"
        Me.CtrlCashSummary.lbltxt5 = "0.00"
        Me.CtrlCashSummary.lbltxt6 = "0.00"
        Me.CtrlCashSummary.lbltxt7 = "0.00"
        Me.CtrlCashSummary.lbltxt8 = "0.00"
        Me.CtrlCashSummary.lbltxt9 = "0.00"
        Me.CtrlCashSummary.lbltxtVisible1 = True
        Me.CtrlCashSummary.lbltxtVisible10 = False
        Me.CtrlCashSummary.lbltxtVisible2 = True
        Me.CtrlCashSummary.lbltxtVisible3 = True
        Me.CtrlCashSummary.lbltxtVisible4 = True
        Me.CtrlCashSummary.lbltxtVisible5 = True
        Me.CtrlCashSummary.lbltxtVisible6 = True
        Me.CtrlCashSummary.lbltxtVisible7 = True
        Me.CtrlCashSummary.lbltxtVisible8 = False
        Me.CtrlCashSummary.lbltxtVisible9 = False
        Me.CtrlCashSummary.lblVisible1 = True
        Me.CtrlCashSummary.lblVisible10 = False
        Me.CtrlCashSummary.lblVisible2 = True
        Me.CtrlCashSummary.lblVisible3 = True
        Me.CtrlCashSummary.lblVisible4 = True
        Me.CtrlCashSummary.lblVisible5 = True
        Me.CtrlCashSummary.lblVisible6 = True
        Me.CtrlCashSummary.lblVisible7 = True
        Me.CtrlCashSummary.lblVisible8 = False
        Me.CtrlCashSummary.lblVisible9 = False
        Me.CtrlCashSummary.Location = New System.Drawing.Point(808, 279)
        Me.CtrlCashSummary.Name = "CtrlCashSummary"
        Me.CtrlCashSummary.RowCount = 10
        Me.CtrlCashSummary.Size = New System.Drawing.Size(197, 225)
        Me.CtrlCashSummary.TabIndex = 82
        '
        'CtrlSalesInfo
        '
        Me.CtrlSalesInfo.Location = New System.Drawing.Point(4, 151)
        Me.CtrlSalesInfo.Name = "CtrlSalesInfo"
        Me.CtrlSalesInfo.Size = New System.Drawing.Size(335, 122)
        Me.CtrlSalesInfo.SOAction = Spectrum.CtrlSalesInfo.SOActionValue.SOCANCEL
        Me.CtrlSalesInfo.TabIndex = 0
        '
        'CtrlCustDtls1
        '
        Me.CtrlCustDtls1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlCustDtls1.DisplayCustType = False
        Me.CtrlCustDtls1.dtCustmInfo = Nothing
        Me.CtrlCustDtls1.Location = New System.Drawing.Point(345, 151)
        Me.CtrlCustDtls1.Name = "CtrlCustDtls1"
        Me.CtrlCustDtls1.Size = New System.Drawing.Size(461, 121)
        Me.CtrlCustDtls1.TabIndex = 80
        '
        'CtrlProductImage
        '
        Me.CtrlProductImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlProductImage.Location = New System.Drawing.Point(808, 153)
        Me.CtrlProductImage.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrlProductImage.Name = "CtrlProductImage"
        Me.CtrlProductImage.Size = New System.Drawing.Size(201, 120)
        Me.CtrlProductImage.TabIndex = 79
        '
        'frmNQuotationCancel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1009, 595)
        Me.Controls.Add(Me.TabSalesOrder)
        Me.Controls.Add(Me.C1Sizer2)
        Me.Controls.Add(Me.CtrlCashSummary)
        Me.Controls.Add(Me.CtrlSalesInfo)
        Me.Controls.Add(Me.CtrlCustDtls1)
        Me.Controls.Add(Me.CtrlProductImage)
        Me.Controls.Add(Me.CtrlRbn1)
        Me.IsDocToParent = True
        Me.Name = "frmNQuotationCancel"
        Me.Text = "Cancel Sales Order"
        Me.Controls.SetChildIndex(Me.C1StatusBar1, 0)
        Me.Controls.SetChildIndex(Me.CtrlRbn1, 0)
        Me.Controls.SetChildIndex(Me.CtrlProductImage, 0)
        Me.Controls.SetChildIndex(Me.CtrlCustDtls1, 0)
        Me.Controls.SetChildIndex(Me.CtrlSalesInfo, 0)
        Me.Controls.SetChildIndex(Me.CtrlCashSummary, 0)
        Me.Controls.SetChildIndex(Me.C1Sizer2, 0)
        Me.Controls.SetChildIndex(Me.TabSalesOrder, 0)
        CType(Me.C1StatusBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlRbn1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabSalesOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabSalesOrder.ResumeLayout(False)
        Me.TabPageItemDetails.ResumeLayout(False)
        CType(Me.C1Sizer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer3.ResumeLayout(False)
        CType(Me.grdSOItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageInvoiceDetails.ResumeLayout(False)
        CType(Me.grdSOInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Sizer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CtrlRbn1 As Spectrum.CtrlRbn
    Friend WithEvents RibbonApplicationMenu1 As C1.Win.C1Ribbon.RibbonApplicationMenu
    Friend WithEvents RibbonConfigToolBar1 As C1.Win.C1Ribbon.RibbonConfigToolBar
    Friend WithEvents RibbonQat1 As C1.Win.C1Ribbon.RibbonQat
    Friend WithEvents rbnTabSO As C1.Win.C1Ribbon.RibbonTab
    Friend WithEvents RibbonGroup1 As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents cmdSONew As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents CmdSOEdit As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents BtnSOCancel As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents RibbonGroup3 As C1.Win.C1Ribbon.RibbonGroup
    Friend WithEvents rbbtnSave As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents RibbonButton11 As C1.Win.C1Ribbon.RibbonButton
    Friend WithEvents TabSalesOrder As Spectrum.CtrlTab
    Friend WithEvents TabPageItemDetails As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents C1Sizer3 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents grdSOItems As Spectrum.CtrlGrid
    Friend WithEvents CtrlSalesPerson As Spectrum.CtrlSalesPerson
    Friend WithEvents TabPageInvoiceDetails As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents grdSOInvoice As Spectrum.CtrlGrid
    Friend WithEvents C1Sizer2 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents CtrlBtnAddExtraCost As Spectrum.CtrlBtn
    Friend WithEvents CtrlBtnStockCheck As Spectrum.CtrlBtn
    Friend WithEvents CtrlCashSummary As Spectrum.CtrlCashSummary
    Friend WithEvents CtrlSalesInfo As Spectrum.CtrlSalesInfo
    Friend WithEvents CtrlCustDtls1 As Spectrum.CtrlCustDtls
    Friend WithEvents CtrlProductImage As Spectrum.CtrlProductImage

End Class
