<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGenerateBarcode
    'Inherits System.Windows.Forms.Form
    Inherits Spectrum.CtrlPopupForm

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGenerateBarcode))
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.GrpShowData = New System.Windows.Forms.GroupBox()
        Me.GrdShowData = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioHierarchy = New System.Windows.Forms.RadioButton()
        Me.RadioArticle = New System.Windows.Forms.RadioButton()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.LblArt = New Spectrum.Controls.Label(Me.components)
        Me.TxtHierarchy = New System.Windows.Forms.TextBox()
        Me.LblHierarachyCode = New Spectrum.Controls.Label(Me.components)
        Me.TxtArticleFliter = New Spectrum.AndroidSearchTextBox(Me.components)
        Me.txtHierarachyArticle = New Spectrum.AndroidSearchTextBox(Me.components)
        Me.chkLabel = New System.Windows.Forms.CheckBox()
        Me.chkBarcode = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.DtPKDDate = New Spectrum.ctrlDate()
        Me.CboChangePKD = New System.Windows.Forms.CheckBox()
        Me.btnExcludeOrInclude = New Spectrum.CtrlBtn()
        Me.TxtQty = New System.Windows.Forms.NumericUpDown()
        Me.LblQuantity = New Spectrum.Controls.Label(Me.components)
        Me.btnApply = New Spectrum.CtrlBtn()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblHierarchy = New Spectrum.Controls.Label(Me.components)
        Me.lblHierarchyName = New Spectrum.CtrlLabel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCancel = New Spectrum.CtrlBtn()
        Me.btnPrint = New Spectrum.CtrlBtn()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.GrpShowData.SuspendLayout()
        CType(Me.GrdShowData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.LblArt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblHierarachyCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.DtPKDDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.lblHierarchy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblHierarchyName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.GrpShowData, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.GroupBox3, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.TableLayoutPanel1, 0, 2)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 3
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(992, 654)
        Me.TableLayoutPanel5.TabIndex = 132
        '
        'GrpShowData
        '
        Me.GrpShowData.Controls.Add(Me.GrdShowData)
        Me.GrpShowData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpShowData.Location = New System.Drawing.Point(3, 264)
        Me.GrpShowData.Name = "GrpShowData"
        Me.GrpShowData.Size = New System.Drawing.Size(986, 321)
        Me.GrpShowData.TabIndex = 124
        Me.GrpShowData.TabStop = False
        '
        'GrdShowData
        '
        Me.GrdShowData.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.GrdShowData.AllowUpdateOnBlur = False
        Me.GrdShowData.BackColor = System.Drawing.Color.White
        Me.GrdShowData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GrdShowData.CaptionHeight = 17
        Me.GrdShowData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrdShowData.ExtendRightColumn = True
        Me.GrdShowData.FilterBar = True
        Me.GrdShowData.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.Standard
        Me.GrdShowData.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrdShowData.GroupByAreaVisible = False
        Me.GrdShowData.GroupByCaption = "Drag a column header here to group by that column"
        Me.GrdShowData.Images.Add(CType(resources.GetObject("GrdShowData.Images"), System.Drawing.Image))
        Me.GrdShowData.Location = New System.Drawing.Point(3, 17)
        Me.GrdShowData.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.GrdShowData.Name = "GrdShowData"
        Me.GrdShowData.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.GrdShowData.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.GrdShowData.PreviewInfo.ZoomFactor = 75.0R
        Me.GrdShowData.PrintInfo.PageSettings = CType(resources.GetObject("GrdShowData.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.GrdShowData.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.GrdShowData.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None
        Me.GrdShowData.RowHeight = 15
        Me.GrdShowData.Size = New System.Drawing.Size(980, 301)
        Me.GrdShowData.TabIndex = 125
        Me.GrdShowData.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue
        Me.GrdShowData.PropBag = resources.GetString("GrdShowData.PropBag")
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TableLayoutPanel6)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(986, 255)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Hierarchy :"
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 1
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel7, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel4, 0, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel2, 0, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel3, 0, 3)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(3, 17)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 4
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(980, 235)
        Me.TableLayoutPanel6.TabIndex = 131
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 2
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.RadioHierarchy, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.RadioArticle, 1, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 1
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(974, 29)
        Me.TableLayoutPanel7.TabIndex = 0
        '
        'RadioHierarchy
        '
        Me.RadioHierarchy.AutoSize = True
        Me.RadioHierarchy.Checked = True
        Me.RadioHierarchy.Location = New System.Drawing.Point(3, 3)
        Me.RadioHierarchy.Name = "RadioHierarchy"
        Me.RadioHierarchy.Size = New System.Drawing.Size(130, 17)
        Me.RadioHierarchy.TabIndex = 127
        Me.RadioHierarchy.TabStop = True
        Me.RadioHierarchy.Text = "Item Hierarchy Search"
        Me.RadioHierarchy.UseVisualStyleBackColor = True
        '
        'RadioArticle
        '
        Me.RadioArticle.AutoSize = True
        Me.RadioArticle.Location = New System.Drawing.Point(139, 3)
        Me.RadioArticle.Name = "RadioArticle"
        Me.RadioArticle.Size = New System.Drawing.Size(109, 17)
        Me.RadioArticle.TabIndex = 126
        Me.RadioArticle.Text = "Article Search "
        Me.RadioArticle.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 4
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 168.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 227.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 227.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 352.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.LblArt, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.TxtHierarchy, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.LblHierarachyCode, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.TxtArticleFliter, 1, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.txtHierarachyArticle, 2, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.chkLabel, 3, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.chkBarcode, 3, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 38)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(974, 52)
        Me.TableLayoutPanel4.TabIndex = 3
        '
        'LblArt
        '
        Me.LblArt.AutoSize = True
        Me.LblArt.BackColor = System.Drawing.Color.Transparent
        Me.LblArt.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LblArt.ForeColor = System.Drawing.Color.Black
        Me.LblArt.Location = New System.Drawing.Point(3, 26)
        Me.LblArt.Name = "LblArt"
        Me.LblArt.Size = New System.Drawing.Size(93, 16)
        Me.LblArt.TabIndex = 141
        Me.LblArt.Tag = Nothing
        Me.LblArt.Text = "Article/EAN :"
        Me.LblArt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblArt.TextDetached = True
        Me.LblArt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'TxtHierarchy
        '
        Me.TxtHierarchy.Location = New System.Drawing.Point(171, 3)
        Me.TxtHierarchy.Name = "TxtHierarchy"
        Me.TxtHierarchy.Size = New System.Drawing.Size(216, 21)
        Me.TxtHierarchy.TabIndex = 38
        '
        'LblHierarachyCode
        '
        Me.LblHierarachyCode.AutoSize = True
        Me.LblHierarachyCode.BackColor = System.Drawing.Color.Transparent
        Me.LblHierarachyCode.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LblHierarachyCode.ForeColor = System.Drawing.Color.Black
        Me.LblHierarachyCode.Location = New System.Drawing.Point(3, 0)
        Me.LblHierarachyCode.Name = "LblHierarachyCode"
        Me.LblHierarachyCode.Size = New System.Drawing.Size(111, 16)
        Me.LblHierarachyCode.TabIndex = 135
        Me.LblHierarachyCode.Tag = Nothing
        Me.LblHierarachyCode.Text = "Item Hierarchy:"
        Me.LblHierarachyCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblHierarachyCode.TextDetached = True
        Me.LblHierarachyCode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'TxtArticleFliter
        '
        Me.TxtArticleFliter.AllowUpdateListBox = True
        Me.TxtArticleFliter.ArticleCodeDesclength = 0
        Me.TxtArticleFliter.DtSearchData = Nothing
        Me.TxtArticleFliter.isCalledFromlablePrint = False
        Me.TxtArticleFliter.IsCallFromPosTab = False
        Me.TxtArticleFliter.IsItemSelected = False
        Me.TxtArticleFliter.IsListBind = True
        Me.TxtArticleFliter.IsMouseOverList = False
        Me.TxtArticleFliter.IsMovingControl = False
        Me.TxtArticleFliter.IsSetLocation = False
        Me.TxtArticleFliter.ListBoxXCoordinate = 0
        Me.TxtArticleFliter.ListBoxYCoordinate = 0
        Me.TxtArticleFliter.Location = New System.Drawing.Point(171, 29)
        Me.TxtArticleFliter.lstNames = CType(resources.GetObject("TxtArticleFliter.lstNames"), System.Collections.Generic.List(Of String))
        Me.TxtArticleFliter.MaxLength = 35
        Me.TxtArticleFliter.Name = "TxtArticleFliter"
        Me.TxtArticleFliter.SearchBasedOnDB = Nothing
        Me.TxtArticleFliter.SearchQueryOnDB = Nothing
        Me.TxtArticleFliter.Size = New System.Drawing.Size(216, 21)
        Me.TxtArticleFliter.TabIndex = 140
        '
        'txtHierarachyArticle
        '
        Me.txtHierarachyArticle.AllowUpdateListBox = True
        Me.txtHierarachyArticle.ArticleCodeDesclength = 0
        Me.txtHierarachyArticle.DtSearchData = Nothing
        Me.txtHierarachyArticle.isCalledFromlablePrint = False
        Me.txtHierarachyArticle.IsCallFromPosTab = False
        Me.txtHierarachyArticle.IsItemSelected = False
        Me.txtHierarachyArticle.IsListBind = True
        Me.txtHierarachyArticle.IsMouseOverList = False
        Me.txtHierarachyArticle.IsMovingControl = False
        Me.txtHierarachyArticle.IsSetLocation = False
        Me.txtHierarachyArticle.ListBoxXCoordinate = 0
        Me.txtHierarachyArticle.ListBoxYCoordinate = 0
        Me.txtHierarachyArticle.Location = New System.Drawing.Point(398, 3)
        Me.txtHierarachyArticle.lstNames = CType(resources.GetObject("txtHierarachyArticle.lstNames"), System.Collections.Generic.List(Of String))
        Me.txtHierarachyArticle.MaxLength = 35
        Me.txtHierarachyArticle.Name = "txtHierarachyArticle"
        Me.txtHierarachyArticle.SearchBasedOnDB = Nothing
        Me.txtHierarachyArticle.SearchQueryOnDB = Nothing
        Me.txtHierarachyArticle.Size = New System.Drawing.Size(216, 21)
        Me.txtHierarachyArticle.TabIndex = 142
        '
        'chkLabel
        '
        Me.chkLabel.AutoSize = True
        Me.chkLabel.Location = New System.Drawing.Point(625, 3)
        Me.chkLabel.Name = "chkLabel"
        Me.chkLabel.Size = New System.Drawing.Size(56, 17)
        Me.chkLabel.TabIndex = 143
        Me.chkLabel.Text = "Label"
        Me.chkLabel.UseVisualStyleBackColor = True
        '
        'chkBarcode
        '
        Me.chkBarcode.AutoSize = True
        Me.chkBarcode.Location = New System.Drawing.Point(625, 29)
        Me.chkBarcode.Name = "chkBarcode"
        Me.chkBarcode.Size = New System.Drawing.Size(73, 17)
        Me.chkBarcode.TabIndex = 144
        Me.chkBarcode.Text = "Barcode"
        Me.chkBarcode.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 6
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 168.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 228.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 114.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 161.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 207.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 351.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.DtPKDDate, 5, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.CboChangePKD, 4, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnExcludeOrInclude, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TxtQty, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.LblQuantity, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnApply, 2, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 96)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(974, 52)
        Me.TableLayoutPanel2.TabIndex = 129
        '
        'DtPKDDate
        '
        Me.DtPKDDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.DtPKDDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DtPKDDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage"), System.Drawing.Image)
        '
        '
        '
        Me.DtPKDDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.DtPKDDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.DtPKDDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.DtPKDDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.DtPKDDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.DtPKDDate.Location = New System.Drawing.Point(881, 3)
        Me.DtPKDDate.Name = "DtPKDDate"
        Me.DtPKDDate.Size = New System.Drawing.Size(135, 19)
        Me.DtPKDDate.TabIndex = 1
        Me.DtPKDDate.Tag = Nothing
        Me.DtPKDDate.TrimStart = True
        Me.DtPKDDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.DtPKDDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'CboChangePKD
        '
        Me.CboChangePKD.AutoSize = True
        Me.CboChangePKD.Location = New System.Drawing.Point(674, 3)
        Me.CboChangePKD.Name = "CboChangePKD"
        Me.CboChangePKD.Size = New System.Drawing.Size(134, 17)
        Me.CboChangePKD.TabIndex = 0
        Me.CboChangePKD.Text = "Change PKD Date:"
        Me.CboChangePKD.UseVisualStyleBackColor = True
        '
        'btnExcludeOrInclude
        '
        Me.btnExcludeOrInclude.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnExcludeOrInclude.Location = New System.Drawing.Point(513, 3)
        Me.btnExcludeOrInclude.MoveToNxtCtrl = Nothing
        Me.btnExcludeOrInclude.Name = "btnExcludeOrInclude"
        Me.btnExcludeOrInclude.SetArticleCode = Nothing
        Me.btnExcludeOrInclude.SetRowIndex = 0
        Me.btnExcludeOrInclude.Size = New System.Drawing.Size(112, 31)
        Me.btnExcludeOrInclude.TabIndex = 142
        Me.btnExcludeOrInclude.Text = "Include All"
        Me.btnExcludeOrInclude.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnExcludeOrInclude.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnExcludeOrInclude.UseVisualStyleBackColor = True
        Me.btnExcludeOrInclude.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'TxtQty
        '
        Me.TxtQty.Location = New System.Drawing.Point(171, 3)
        Me.TxtQty.Name = "TxtQty"
        Me.TxtQty.Size = New System.Drawing.Size(203, 21)
        Me.TxtQty.TabIndex = 16
        '
        'LblQuantity
        '
        Me.LblQuantity.AutoSize = True
        Me.LblQuantity.BackColor = System.Drawing.Color.Transparent
        Me.LblQuantity.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LblQuantity.ForeColor = System.Drawing.Color.Black
        Me.LblQuantity.Location = New System.Drawing.Point(3, 0)
        Me.LblQuantity.Name = "LblQuantity"
        Me.LblQuantity.Size = New System.Drawing.Size(118, 16)
        Me.LblQuantity.TabIndex = 138
        Me.LblQuantity.Tag = Nothing
        Me.LblQuantity.Text = "Number Of Print:"
        Me.LblQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblQuantity.TextDetached = True
        Me.LblQuantity.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'btnApply
        '
        Me.btnApply.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnApply.Location = New System.Drawing.Point(399, 3)
        Me.btnApply.MoveToNxtCtrl = Nothing
        Me.btnApply.Name = "btnApply"
        Me.btnApply.SetArticleCode = Nothing
        Me.btnApply.SetRowIndex = 0
        Me.btnApply.Size = New System.Drawing.Size(108, 31)
        Me.btnApply.TabIndex = 141
        Me.btnApply.Text = "Apply All"
        Me.btnApply.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnApply.UseVisualStyleBackColor = True
        Me.btnApply.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.lblHierarchy, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.lblHierarchyName, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 154)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(974, 78)
        Me.TableLayoutPanel3.TabIndex = 130
        '
        'lblHierarchy
        '
        Me.lblHierarchy.AutoSize = True
        Me.lblHierarchy.BackColor = System.Drawing.Color.Transparent
        Me.lblHierarchy.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblHierarchy.ForeColor = System.Drawing.Color.Black
        Me.lblHierarchy.Location = New System.Drawing.Point(3, 0)
        Me.lblHierarchy.Name = "lblHierarchy"
        Me.lblHierarchy.Size = New System.Drawing.Size(124, 16)
        Me.lblHierarchy.TabIndex = 136
        Me.lblHierarchy.Tag = Nothing
        Me.lblHierarchy.Text = "Hierarchy Names:"
        Me.lblHierarchy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblHierarchy.TextDetached = True
        Me.lblHierarchy.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'lblHierarchyName
        '
        Me.lblHierarchyName.AttachedTextBoxName = Nothing
        Me.lblHierarchyName.BackColor = System.Drawing.Color.Transparent
        Me.lblHierarchyName.BorderColor = System.Drawing.Color.Transparent
        Me.lblHierarchyName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblHierarchyName.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblHierarchyName.ForeColor = System.Drawing.Color.Black
        Me.lblHierarchyName.Location = New System.Drawing.Point(139, 0)
        Me.lblHierarchyName.Name = "lblHierarchyName"
        Me.lblHierarchyName.Size = New System.Drawing.Size(832, 78)
        Me.lblHierarchyName.TabIndex = 1
        Me.lblHierarchyName.Tag = Nothing
        Me.lblHierarchyName.Text = "CtrlLabel1"
        Me.lblHierarchyName.TextDetached = True
        Me.lblHierarchyName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 2, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 591)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(986, 60)
        Me.TableLayoutPanel1.TabIndex = 125
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel8)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(397, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(191, 54)
        Me.GroupBox1.TabIndex = 125
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Print:"
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 3
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.btnCancel, 2, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.btnPrint, 0, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(3, 17)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(185, 34)
        Me.TableLayoutPanel8.TabIndex = 126
        '
        'btnCancel
        '
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancel.Location = New System.Drawing.Point(98, 3)
        Me.btnCancel.MoveToNxtCtrl = Nothing
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.SetArticleCode = Nothing
        Me.btnCancel.SetRowIndex = 0
        Me.btnCancel.Size = New System.Drawing.Size(84, 28)
        Me.btnCancel.TabIndex = 120
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnPrint
        '
        Me.btnPrint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPrint.Location = New System.Drawing.Point(3, 3)
        Me.btnPrint.MoveToNxtCtrl = Nothing
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.SetArticleCode = Nothing
        Me.btnPrint.SetRowIndex = 0
        Me.btnPrint.Size = New System.Drawing.Size(82, 28)
        Me.btnPrint.TabIndex = 121
        Me.btnPrint.Text = "Print"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnPrint.UseVisualStyleBackColor = True
        Me.btnPrint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmGenerateBarcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(992, 654)
        Me.Controls.Add(Me.TableLayoutPanel5)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGenerateBarcode"
        Me.Text = "Label Print"
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.GrpShowData.ResumeLayout(False)
        CType(Me.GrdShowData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.LblArt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblHierarachyCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.DtPKDDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.lblHierarchy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblHierarchyName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GrpShowData As System.Windows.Forms.GroupBox
    Friend WithEvents GrdShowData As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnPrint As Spectrum.CtrlBtn
    Friend WithEvents btnCancel As Spectrum.CtrlBtn
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents CboChangePKD As System.Windows.Forms.CheckBox
    Friend WithEvents DtPKDDate As Spectrum.ctrlDate
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnExcludeOrInclude As Spectrum.CtrlBtn
    Friend WithEvents TxtQty As System.Windows.Forms.NumericUpDown
    Friend WithEvents LblQuantity As Spectrum.Controls.Label
    Friend WithEvents btnApply As Spectrum.CtrlBtn
    Friend WithEvents lblHierarchy As Spectrum.Controls.Label
    Friend WithEvents lblHierarchyName As Spectrum.CtrlLabel
    Friend WithEvents RadioHierarchy As System.Windows.Forms.RadioButton
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LblArt As Spectrum.Controls.Label
    Friend WithEvents TxtHierarchy As System.Windows.Forms.TextBox
    Friend WithEvents LblHierarachyCode As Spectrum.Controls.Label
    Friend WithEvents TxtArticleFliter As Spectrum.AndroidSearchTextBox
    Friend WithEvents txtHierarachyArticle As Spectrum.AndroidSearchTextBox
    Friend WithEvents RadioArticle As System.Windows.Forms.RadioButton
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel6 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel8 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkLabel As System.Windows.Forms.CheckBox
    Friend WithEvents chkBarcode As System.Windows.Forms.CheckBox
End Class
