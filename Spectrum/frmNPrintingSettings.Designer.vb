<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNPrintingSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNPrintingSettings))
        Me.sizBottom = New C1.Win.C1Sizer.C1Sizer
        Me.btnCancel = New Spectrum.CtrlBtn
        Me.btnSave = New Spectrum.CtrlBtn
        Me.btnAdd = New Spectrum.CtrlBtn
        Me.CtrlTab1 = New Spectrum.CtrlTab
        Me.tpGeneral = New C1.Win.C1Command.C1DockingTabPage
        Me.lblDocumentType = New Spectrum.CtrlLabel
        Me.lblItem = New Spectrum.CtrlLabel
        Me.cboDocumentType = New Spectrum.ctrlCombo
        Me.cboItemNoReceipt = New Spectrum.ctrlCombo
        Me.tpTop = New C1.Win.C1Command.C1DockingTabPage
        Me.gridTop = New Spectrum.CtrlGrid
        Me.tpBottom = New C1.Win.C1Command.C1DockingTabPage
        Me.gridBottom = New Spectrum.CtrlGrid
        Me.tpWelcome = New C1.Win.C1Command.C1DockingTabPage
        Me.gridWelcome = New Spectrum.CtrlGrid
        Me.tpPromo = New C1.Win.C1Command.C1DockingTabPage
        Me.gridPromo = New Spectrum.CtrlGrid
        Me.tpPrinting = New C1.Win.C1Command.C1DockingTabPage
        Me.gridTax = New Spectrum.CtrlGrid
        CType(Me.sizBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sizBottom.SuspendLayout()
        CType(Me.CtrlTab1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CtrlTab1.SuspendLayout()
        Me.tpGeneral.SuspendLayout()
        CType(Me.lblDocumentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDocumentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboItemNoReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpTop.SuspendLayout()
        CType(Me.gridTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpBottom.SuspendLayout()
        CType(Me.gridBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpWelcome.SuspendLayout()
        CType(Me.gridWelcome, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpPromo.SuspendLayout()
        CType(Me.gridPromo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpPrinting.SuspendLayout()
        CType(Me.gridTax, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sizBottom
        '
        Me.sizBottom.Border.Color = System.Drawing.Color.LightSlateGray
        Me.sizBottom.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.sizBottom.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.sizBottom.Controls.Add(Me.btnCancel)
        Me.sizBottom.Controls.Add(Me.btnSave)
        Me.sizBottom.Controls.Add(Me.btnAdd)
        Me.sizBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.sizBottom.GridDefinition = "81.1320754716981:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "98.6893840104849:False:False;"
        Me.sizBottom.Location = New System.Drawing.Point(0, 301)
        Me.sizBottom.Name = "sizBottom"
        Me.sizBottom.Size = New System.Drawing.Size(763, 53)
        Me.sizBottom.TabIndex = 1
        Me.sizBottom.Text = "C1Sizer1"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancel.Location = New System.Drawing.Point(453, 15)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(96, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSave.Location = New System.Drawing.Point(355, 15)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(96, 23)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnSave.UseVisualStyleBackColor = True
        Me.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnAdd.Location = New System.Drawing.Point(257, 15)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(96, 23)
        Me.btnAdd.TabIndex = 0
        Me.btnAdd.Text = "Add "
        Me.btnAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnAdd.UseVisualStyleBackColor = True
        Me.btnAdd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlTab1
        '
        Me.CtrlTab1.Controls.Add(Me.tpGeneral)
        Me.CtrlTab1.Controls.Add(Me.tpTop)
        Me.CtrlTab1.Controls.Add(Me.tpBottom)
        Me.CtrlTab1.Controls.Add(Me.tpWelcome)
        Me.CtrlTab1.Controls.Add(Me.tpPromo)
        Me.CtrlTab1.Controls.Add(Me.tpPrinting)
        Me.CtrlTab1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlTab1.Location = New System.Drawing.Point(0, 0)
        Me.CtrlTab1.Name = "CtrlTab1"
        Me.CtrlTab1.SelectedIndex = 5
        Me.CtrlTab1.Size = New System.Drawing.Size(763, 301)
        Me.CtrlTab1.TabIndex = 2
        Me.CtrlTab1.TabsSpacing = 5
        Me.CtrlTab1.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007
        Me.CtrlTab1.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Blue
        '
        'tpGeneral
        '
        Me.tpGeneral.Controls.Add(Me.lblDocumentType)
        Me.tpGeneral.Controls.Add(Me.lblItem)
        Me.tpGeneral.Controls.Add(Me.cboDocumentType)
        Me.tpGeneral.Controls.Add(Me.cboItemNoReceipt)
        Me.tpGeneral.Location = New System.Drawing.Point(1, 24)
        Me.tpGeneral.Name = "tpGeneral"
        Me.tpGeneral.Size = New System.Drawing.Size(761, 276)
        Me.tpGeneral.TabIndex = 0
        Me.tpGeneral.Text = "General"
        '
        'lblDocumentType
        '
        Me.lblDocumentType.AutoSize = True
        Me.lblDocumentType.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblDocumentType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDocumentType.ForeColor = System.Drawing.Color.Black
        Me.lblDocumentType.Location = New System.Drawing.Point(25, 45)
        Me.lblDocumentType.Name = "lblDocumentType"
        Me.lblDocumentType.Size = New System.Drawing.Size(90, 15)
        Me.lblDocumentType.TabIndex = 3
        Me.lblDocumentType.Tag = Nothing
        Me.lblDocumentType.Text = "Document Types"
        Me.lblDocumentType.TextDetached = True
        '
        'lblItem
        '
        Me.lblItem.AutoSize = True
        Me.lblItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblItem.ForeColor = System.Drawing.Color.Black
        Me.lblItem.Location = New System.Drawing.Point(25, 15)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(104, 15)
        Me.lblItem.TabIndex = 2
        Me.lblItem.Tag = Nothing
        Me.lblItem.Text = "Item No. on Receipt"
        Me.lblItem.TextDetached = True
        Me.lblItem.Visible = False
        '
        'cboDocumentType
        '
        Me.cboDocumentType.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboDocumentType.AutoCompletion = True
        Me.cboDocumentType.AutoDropDown = True
        Me.cboDocumentType.Caption = ""
        Me.cboDocumentType.CaptionHeight = 17
        Me.cboDocumentType.CaptionVisible = False
        Me.cboDocumentType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboDocumentType.ColumnCaptionHeight = 17
        Me.cboDocumentType.ColumnFooterHeight = 17
        Me.cboDocumentType.ColumnHeaders = False
        Me.cboDocumentType.ContentHeight = 15
        Me.cboDocumentType.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboDocumentType.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboDocumentType.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDocumentType.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDocumentType.EditorHeight = 15
        Me.cboDocumentType.Images.Add(CType(resources.GetObject("cboDocumentType.Images"), System.Drawing.Image))
        Me.cboDocumentType.ItemHeight = 15
        Me.cboDocumentType.Location = New System.Drawing.Point(173, 42)
        Me.cboDocumentType.MatchEntryTimeout = CType(2000, Long)
        Me.cboDocumentType.MaxDropDownItems = CType(5, Short)
        Me.cboDocumentType.MaxLength = 32767
        Me.cboDocumentType.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboDocumentType.Name = "cboDocumentType"
        Me.cboDocumentType.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboDocumentType.Size = New System.Drawing.Size(197, 21)
        Me.cboDocumentType.TabIndex = 1
        Me.cboDocumentType.PropBag = resources.GetString("cboDocumentType.PropBag")
        '
        'cboItemNoReceipt
        '
        Me.cboItemNoReceipt.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboItemNoReceipt.AutoCompletion = True
        Me.cboItemNoReceipt.AutoDropDown = True
        Me.cboItemNoReceipt.Caption = ""
        Me.cboItemNoReceipt.CaptionHeight = 17
        Me.cboItemNoReceipt.CaptionVisible = False
        Me.cboItemNoReceipt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboItemNoReceipt.ColumnCaptionHeight = 17
        Me.cboItemNoReceipt.ColumnFooterHeight = 17
        Me.cboItemNoReceipt.ColumnHeaders = False
        Me.cboItemNoReceipt.ContentHeight = 15
        Me.cboItemNoReceipt.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboItemNoReceipt.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboItemNoReceipt.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboItemNoReceipt.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboItemNoReceipt.EditorHeight = 15
        Me.cboItemNoReceipt.Images.Add(CType(resources.GetObject("cboItemNoReceipt.Images"), System.Drawing.Image))
        Me.cboItemNoReceipt.ItemHeight = 15
        Me.cboItemNoReceipt.Location = New System.Drawing.Point(173, 15)
        Me.cboItemNoReceipt.MatchEntryTimeout = CType(2000, Long)
        Me.cboItemNoReceipt.MaxDropDownItems = CType(5, Short)
        Me.cboItemNoReceipt.MaxLength = 32767
        Me.cboItemNoReceipt.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboItemNoReceipt.Name = "cboItemNoReceipt"
        Me.cboItemNoReceipt.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboItemNoReceipt.Size = New System.Drawing.Size(197, 21)
        Me.cboItemNoReceipt.TabIndex = 0
        Me.cboItemNoReceipt.Visible = False
        Me.cboItemNoReceipt.PropBag = resources.GetString("cboItemNoReceipt.PropBag")
        '
        'tpTop
        '
        Me.tpTop.Controls.Add(Me.gridTop)
        Me.tpTop.Location = New System.Drawing.Point(1, 24)
        Me.tpTop.Name = "tpTop"
        Me.tpTop.Size = New System.Drawing.Size(761, 276)
        Me.tpTop.TabIndex = 1
        Me.tpTop.Text = "Top"
        '
        'gridTop
        '
        Me.gridTop.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.gridTop.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.gridTop.AutoGenerateColumns = False
        Me.gridTop.CellButtonImage = CType(resources.GetObject("gridTop.CellButtonImage"), System.Drawing.Image)
        Me.gridTop.ColumnInfo = resources.GetString("gridTop.ColumnInfo")
        Me.gridTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridTop.ExtendLastCol = True
        Me.gridTop.Location = New System.Drawing.Point(0, 0)
        Me.gridTop.Name = "gridTop"
        Me.gridTop.Rows.Count = 2
        Me.gridTop.Rows.DefaultSize = 17
        Me.gridTop.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.gridTop.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.gridTop.Size = New System.Drawing.Size(761, 276)
        Me.gridTop.StyleInfo = resources.GetString("gridTop.StyleInfo")
        Me.gridTop.TabIndex = 0
        Me.gridTop.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'tpBottom
        '
        Me.tpBottom.Controls.Add(Me.gridBottom)
        Me.tpBottom.Location = New System.Drawing.Point(1, 24)
        Me.tpBottom.Name = "tpBottom"
        Me.tpBottom.Size = New System.Drawing.Size(761, 276)
        Me.tpBottom.TabIndex = 2
        Me.tpBottom.Text = "Bottom"
        '
        'gridBottom
        '
        Me.gridBottom.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.gridBottom.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.gridBottom.AutoGenerateColumns = False
        Me.gridBottom.CellButtonImage = CType(resources.GetObject("gridBottom.CellButtonImage"), System.Drawing.Image)
        Me.gridBottom.ColumnInfo = resources.GetString("gridBottom.ColumnInfo")
        Me.gridBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridBottom.ExtendLastCol = True
        Me.gridBottom.Location = New System.Drawing.Point(0, 0)
        Me.gridBottom.Name = "gridBottom"
        Me.gridBottom.Rows.Count = 2
        Me.gridBottom.Rows.DefaultSize = 17
        Me.gridBottom.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.gridBottom.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.gridBottom.Size = New System.Drawing.Size(761, 276)
        Me.gridBottom.StyleInfo = resources.GetString("gridBottom.StyleInfo")
        Me.gridBottom.TabIndex = 1
        Me.gridBottom.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'tpWelcome
        '
        Me.tpWelcome.Controls.Add(Me.gridWelcome)
        Me.tpWelcome.Location = New System.Drawing.Point(1, 24)
        Me.tpWelcome.Name = "tpWelcome"
        Me.tpWelcome.Size = New System.Drawing.Size(761, 276)
        Me.tpWelcome.TabIndex = 3
        Me.tpWelcome.Text = "Welcome Message"
        '
        'gridWelcome
        '
        Me.gridWelcome.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.gridWelcome.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.gridWelcome.AutoGenerateColumns = False
        Me.gridWelcome.CellButtonImage = CType(resources.GetObject("gridWelcome.CellButtonImage"), System.Drawing.Image)
        Me.gridWelcome.ColumnInfo = resources.GetString("gridWelcome.ColumnInfo")
        Me.gridWelcome.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridWelcome.ExtendLastCol = True
        Me.gridWelcome.Location = New System.Drawing.Point(0, 0)
        Me.gridWelcome.Name = "gridWelcome"
        Me.gridWelcome.Rows.Count = 2
        Me.gridWelcome.Rows.DefaultSize = 17
        Me.gridWelcome.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.gridWelcome.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.gridWelcome.Size = New System.Drawing.Size(761, 276)
        Me.gridWelcome.StyleInfo = resources.GetString("gridWelcome.StyleInfo")
        Me.gridWelcome.TabIndex = 1
        Me.gridWelcome.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'tpPromo
        '
        Me.tpPromo.Controls.Add(Me.gridPromo)
        Me.tpPromo.Location = New System.Drawing.Point(1, 24)
        Me.tpPromo.Name = "tpPromo"
        Me.tpPromo.Size = New System.Drawing.Size(761, 276)
        Me.tpPromo.TabIndex = 4
        Me.tpPromo.Text = "Promotional Message"
        '
        'gridPromo
        '
        Me.gridPromo.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.gridPromo.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.gridPromo.AutoGenerateColumns = False
        Me.gridPromo.CellButtonImage = CType(resources.GetObject("gridPromo.CellButtonImage"), System.Drawing.Image)
        Me.gridPromo.ColumnInfo = resources.GetString("gridPromo.ColumnInfo")
        Me.gridPromo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridPromo.ExtendLastCol = True
        Me.gridPromo.Location = New System.Drawing.Point(0, 0)
        Me.gridPromo.Name = "gridPromo"
        Me.gridPromo.Rows.Count = 2
        Me.gridPromo.Rows.DefaultSize = 17
        Me.gridPromo.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.gridPromo.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.gridPromo.Size = New System.Drawing.Size(761, 276)
        Me.gridPromo.StyleInfo = resources.GetString("gridPromo.StyleInfo")
        Me.gridPromo.TabIndex = 1
        Me.gridPromo.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'tpPrinting
        '
        Me.tpPrinting.Controls.Add(Me.gridTax)
        Me.tpPrinting.Location = New System.Drawing.Point(1, 24)
        Me.tpPrinting.Name = "tpPrinting"
        Me.tpPrinting.Size = New System.Drawing.Size(761, 276)
        Me.tpPrinting.TabIndex = 5
        Me.tpPrinting.Text = "Tax Information"
        '
        'gridTax
        '
        Me.gridTax.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.gridTax.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.gridTax.AutoGenerateColumns = False
        Me.gridTax.CellButtonImage = CType(resources.GetObject("gridTax.CellButtonImage"), System.Drawing.Image)
        Me.gridTax.ColumnInfo = resources.GetString("gridTax.ColumnInfo")
        Me.gridTax.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridTax.ExtendLastCol = True
        Me.gridTax.Location = New System.Drawing.Point(0, 0)
        Me.gridTax.Name = "gridTax"
        Me.gridTax.Rows.Count = 2
        Me.gridTax.Rows.DefaultSize = 17
        Me.gridTax.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.gridTax.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.gridTax.Size = New System.Drawing.Size(761, 276)
        Me.gridTax.StyleInfo = resources.GetString("gridTax.StyleInfo")
        Me.gridTax.TabIndex = 1
        Me.gridTax.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'frmNPrintingSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(763, 376)
        Me.Controls.Add(Me.CtrlTab1)
        Me.Controls.Add(Me.sizBottom)
        Me.IsNetWorkConnected = True
        Me.Name = "frmNPrintingSettings"
        Me.Text = "Printer Settings"
        Me.Controls.SetChildIndex(Me.sizBottom, 0)
        Me.Controls.SetChildIndex(Me.CtrlTab1, 0)
        CType(Me.sizBottom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sizBottom.ResumeLayout(False)
        CType(Me.CtrlTab1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CtrlTab1.ResumeLayout(False)
        Me.tpGeneral.ResumeLayout(False)
        Me.tpGeneral.PerformLayout()
        CType(Me.lblDocumentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDocumentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboItemNoReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpTop.ResumeLayout(False)
        CType(Me.gridTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpBottom.ResumeLayout(False)
        CType(Me.gridBottom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpWelcome.ResumeLayout(False)
        CType(Me.gridWelcome, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpPromo.ResumeLayout(False)
        CType(Me.gridPromo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpPrinting.ResumeLayout(False)
        CType(Me.gridTax, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents sizBottom As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents btnCancel As Spectrum.CtrlBtn
    Friend WithEvents btnSave As Spectrum.CtrlBtn
    Friend WithEvents btnAdd As Spectrum.CtrlBtn
    Friend WithEvents CtrlTab1 As Spectrum.CtrlTab
    Friend WithEvents tpGeneral As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents tpTop As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents tpBottom As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents tpWelcome As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents tpPromo As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents tpPrinting As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents gridTop As Spectrum.CtrlGrid
    Friend WithEvents gridBottom As Spectrum.CtrlGrid
    Friend WithEvents gridWelcome As Spectrum.CtrlGrid
    Friend WithEvents gridPromo As Spectrum.CtrlGrid
    Friend WithEvents gridTax As Spectrum.CtrlGrid
    Friend WithEvents lblDocumentType As Spectrum.CtrlLabel
    Friend WithEvents lblItem As Spectrum.CtrlLabel
    Friend WithEvents cboDocumentType As Spectrum.ctrlCombo
    Friend WithEvents cboItemNoReceipt As Spectrum.ctrlCombo

End Class
