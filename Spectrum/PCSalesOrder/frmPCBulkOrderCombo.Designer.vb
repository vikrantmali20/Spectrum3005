<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPCBulkOrderCombo
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPCBulkOrderCombo))
        Me.C1Sizer4 = New C1.Win.C1Sizer.C1Sizer()
        Me.mainPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.CboFixed = New System.Windows.Forms.RadioButton()
        Me.CboVariable = New System.Windows.Forms.RadioButton()
        Me.rbnSingle = New System.Windows.Forms.RadioButton()
        Me.rbnCombo = New System.Windows.Forms.RadioButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.GrdFixedCombo = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.TxtFixedPriceEnter = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New Spectrum.Controls.Label(Me.components)
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.DgBulkComboGrid = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cboPrintName = New Spectrum.ctrlCombo()
        Me.lblPrintName = New Spectrum.CtrlLabel()
        Me.cboCopyFrom = New Spectrum.ctrlCombo()
        Me.lblCopyFrom = New Spectrum.CtrlLabel()
        Me.CtrlLabel2 = New Spectrum.CtrlLabel()
        Me.txtRemarks = New Spectrum.CtrlTextBox()
        Me.lblNetValue = New Spectrum.CtrlLabel()
        Me.lblDiscount = New Spectrum.CtrlLabel()
        Me.txtTaxAmount = New Spectrum.CtrlLabel()
        Me.lblGross = New Spectrum.CtrlLabel()
        Me.CtrlDiscount = New Spectrum.CtrlLabel()
        Me.CtrlGross = New Spectrum.CtrlLabel()
        Me.CtrlTaxAmt = New Spectrum.CtrlLabel()
        Me.ctrlNetValue = New Spectrum.CtrlLabel()
        Me.btnSave = New Spectrum.CtrlBtn()
        Me.btnCancel = New Spectrum.CtrlBtn()
        Me.btnClear = New Spectrum.CtrlBtn()
        Me.btnAddBulkCombo = New Spectrum.CtrlBtn()
        Me.txtAddArticle = New Spectrum.AndroidSearchTextBox(Me.components)
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        CType(Me.C1Sizer4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer4.SuspendLayout()
        Me.mainPanel.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.GrdFixedCombo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.DgBulkComboGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.cboPrintName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPrintName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCopyFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCopyFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNetValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTaxAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGross, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlDiscount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlGross, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlTaxAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ctrlNetValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'C1Sizer4
        '
        Me.C1Sizer4.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1Sizer4.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1Sizer4.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1Sizer4.Controls.Add(Me.mainPanel)
        Me.C1Sizer4.Dock = System.Windows.Forms.DockStyle.Top
        Me.C1Sizer4.GridDefinition = resources.GetString("C1Sizer4.GridDefinition")
        Me.C1Sizer4.Location = New System.Drawing.Point(0, 0)
        Me.C1Sizer4.Name = "C1Sizer4"
        Me.C1Sizer4.Size = New System.Drawing.Size(1354, 734)
        Me.C1Sizer4.TabIndex = 70
        Me.C1Sizer4.TabStop = False
        Me.C1Sizer4.Text = "C1Sizer1"
        '
        'mainPanel
        '
        Me.mainPanel.AutoScroll = True
        Me.mainPanel.Controls.Add(Me.Panel1)
        Me.mainPanel.Controls.Add(Me.Panel2)
        Me.mainPanel.Controls.Add(Me.Panel3)
        Me.mainPanel.Location = New System.Drawing.Point(5, 9)
        Me.mainPanel.Name = "mainPanel"
        Me.mainPanel.Size = New System.Drawing.Size(1334, 709)
        Me.mainPanel.TabIndex = 4
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.rbnSingle)
        Me.Panel1.Controls.Add(Me.rbnCombo)
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1184, 33)
        Me.Panel1.TabIndex = 103
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.CboFixed)
        Me.Panel4.Controls.Add(Me.CboVariable)
        Me.Panel4.Location = New System.Drawing.Point(564, 2)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(617, 29)
        Me.Panel4.TabIndex = 76
        '
        'CboFixed
        '
        Me.CboFixed.AutoSize = True
        Me.CboFixed.Font = New System.Drawing.Font("Calibri", 12.75!)
        Me.CboFixed.Location = New System.Drawing.Point(367, 4)
        Me.CboFixed.Name = "CboFixed"
        Me.CboFixed.Size = New System.Drawing.Size(229, 25)
        Me.CboFixed.TabIndex = 4
        Me.CboFixed.Text = "Assorted/ Fixed Price Combo"
        Me.CboFixed.UseVisualStyleBackColor = True
        '
        'CboVariable
        '
        Me.CboVariable.AutoSize = True
        Me.CboVariable.Checked = True
        Me.CboVariable.Font = New System.Drawing.Font("Calibri", 12.75!)
        Me.CboVariable.Location = New System.Drawing.Point(89, 4)
        Me.CboVariable.Name = "CboVariable"
        Me.CboVariable.Size = New System.Drawing.Size(238, 25)
        Me.CboVariable.TabIndex = 3
        Me.CboVariable.TabStop = True
        Me.CboVariable.Text = " Snacks/ Variable Price Combo"
        Me.CboVariable.UseVisualStyleBackColor = True
        '
        'rbnSingle
        '
        Me.rbnSingle.AutoSize = True
        Me.rbnSingle.Font = New System.Drawing.Font("Calibri", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbnSingle.Location = New System.Drawing.Point(212, 3)
        Me.rbnSingle.Name = "rbnSingle"
        Me.rbnSingle.Size = New System.Drawing.Size(117, 25)
        Me.rbnSingle.TabIndex = 1
        Me.rbnSingle.Text = "Single Article"
        Me.rbnSingle.UseVisualStyleBackColor = True
        '
        'rbnCombo
        '
        Me.rbnCombo.AutoSize = True
        Me.rbnCombo.Checked = True
        Me.rbnCombo.Font = New System.Drawing.Font("Calibri", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbnCombo.Location = New System.Drawing.Point(38, 2)
        Me.rbnCombo.Name = "rbnCombo"
        Me.rbnCombo.Size = New System.Drawing.Size(126, 25)
        Me.rbnCombo.TabIndex = 0
        Me.rbnCombo.TabStop = True
        Me.rbnCombo.Text = "Combo Article"
        Me.rbnCombo.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.GrdFixedCombo)
        Me.Panel3.Controls.Add(Me.TxtFixedPriceEnter)
        Me.Panel3.Controls.Add(Me.lblNetValue)
        Me.Panel3.Controls.Add(Me.lblDiscount)
        Me.Panel3.Controls.Add(Me.txtTaxAmount)
        Me.Panel3.Controls.Add(Me.lblGross)
        Me.Panel3.Controls.Add(Me.CtrlDiscount)
        Me.Panel3.Controls.Add(Me.CtrlGross)
        Me.Panel3.Controls.Add(Me.CtrlTaxAmt)
        Me.Panel3.Controls.Add(Me.ctrlNetValue)
        Me.Panel3.Controls.Add(Me.TableLayoutPanel3)
        Me.Panel3.Controls.Add(Me.TableLayoutPanel2)
        Me.Panel3.Controls.Add(Me.DgBulkComboGrid)
        Me.Panel3.Location = New System.Drawing.Point(3, 165)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1321, 531)
        Me.Panel3.TabIndex = 110
        '
        'GrdFixedCombo
        '
        Me.GrdFixedCombo.AutoGenerateColumns = False
        Me.GrdFixedCombo.CellButtonImage = Global.Spectrum.My.Resources.Resources.del_icon
        Me.GrdFixedCombo.ColumnInfo = resources.GetString("GrdFixedCombo.ColumnInfo")
        Me.GrdFixedCombo.ExtendLastCol = True
        Me.GrdFixedCombo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrdFixedCombo.Location = New System.Drawing.Point(0, 3)
        Me.GrdFixedCombo.Name = "GrdFixedCombo"
        Me.GrdFixedCombo.NewRowWatermark = ""
        Me.GrdFixedCombo.Rows.Count = 1
        Me.GrdFixedCombo.Rows.DefaultSize = 19
        Me.GrdFixedCombo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GrdFixedCombo.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.GrdFixedCombo.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.GrdFixedCombo.Size = New System.Drawing.Size(1314, 315)
        Me.GrdFixedCombo.StyleInfo = resources.GetString("GrdFixedCombo.StyleInfo")
        Me.GrdFixedCombo.TabIndex = 122
        Me.GrdFixedCombo.Tag = ""
        '
        'TxtFixedPriceEnter
        '
        Me.TxtFixedPriceEnter.Location = New System.Drawing.Point(199, 397)
        Me.TxtFixedPriceEnter.Name = "TxtFixedPriceEnter"
        Me.TxtFixedPriceEnter.Size = New System.Drawing.Size(100, 20)
        Me.TxtFixedPriceEnter.TabIndex = 121
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(28, 356)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(344, 30)
        Me.TableLayoutPanel3.TabIndex = 118
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(214, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Tag = Nothing
        Me.Label1.Text = "Single Combo Price Details"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.TextDetached = True
        Me.Label1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 114.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnCancel, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnClear, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnSave, 0, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(987, 356)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(334, 36)
        Me.TableLayoutPanel2.TabIndex = 117
        '
        'DgBulkComboGrid
        '
        Me.DgBulkComboGrid.AutoGenerateColumns = False
        Me.DgBulkComboGrid.CellButtonImage = Global.Spectrum.My.Resources.Resources.del_icon
        Me.DgBulkComboGrid.ColumnInfo = resources.GetString("DgBulkComboGrid.ColumnInfo")
        Me.DgBulkComboGrid.ExtendLastCol = True
        Me.DgBulkComboGrid.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgBulkComboGrid.Location = New System.Drawing.Point(0, 3)
        Me.DgBulkComboGrid.Name = "DgBulkComboGrid"
        Me.DgBulkComboGrid.NewRowWatermark = ""
        Me.DgBulkComboGrid.Rows.Count = 1
        Me.DgBulkComboGrid.Rows.DefaultSize = 19
        Me.DgBulkComboGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DgBulkComboGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.DgBulkComboGrid.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.DgBulkComboGrid.Size = New System.Drawing.Size(1314, 315)
        Me.DgBulkComboGrid.StyleInfo = resources.GetString("DgBulkComboGrid.StyleInfo")
        Me.DgBulkComboGrid.TabIndex = 113
        Me.DgBulkComboGrid.Tag = ""
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.cboPrintName)
        Me.Panel2.Controls.Add(Me.lblPrintName)
        Me.Panel2.Controls.Add(Me.cboCopyFrom)
        Me.Panel2.Controls.Add(Me.lblCopyFrom)
        Me.Panel2.Controls.Add(Me.CtrlLabel2)
        Me.Panel2.Controls.Add(Me.txtRemarks)
        Me.Panel2.Controls.Add(Me.txtAddArticle)
        Me.Panel2.Controls.Add(Me.btnAddBulkCombo)
        Me.Panel2.Controls.Add(Me.CtrlLabel1)
        Me.Panel2.Location = New System.Drawing.Point(3, 42)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1258, 117)
        Me.Panel2.TabIndex = 113
        '
        'cboPrintName
        '
        Me.cboPrintName.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboPrintName.AutoCompletion = True
        Me.cboPrintName.AutoDropDown = True
        Me.cboPrintName.Caption = ""
        Me.cboPrintName.CaptionHeight = 17
        Me.cboPrintName.CaptionVisible = False
        Me.cboPrintName.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboPrintName.ColumnCaptionHeight = 17
        Me.cboPrintName.ColumnFooterHeight = 17
        Me.cboPrintName.ColumnHeaders = False
        Me.cboPrintName.ContentHeight = 18
        Me.cboPrintName.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboPrintName.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboPrintName.EditorFont = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPrintName.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPrintName.EditorHeight = 18
        Me.cboPrintName.ExtendRightColumn = True
        Me.cboPrintName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPrintName.Images.Add(CType(resources.GetObject("cboPrintName.Images"), System.Drawing.Image))
        Me.cboPrintName.ItemHeight = 15
        Me.cboPrintName.Location = New System.Drawing.Point(116, 39)
        Me.cboPrintName.MatchCol = C1.Win.C1List.MatchColEnum.AllCols
        Me.cboPrintName.MatchEntry = C1.Win.C1List.MatchEntryEnum.Extended
        Me.cboPrintName.MatchEntryTimeout = CType(2000, Long)
        Me.cboPrintName.MaxDropDownItems = CType(5, Short)
        Me.cboPrintName.MaxLength = 32767
        Me.cboPrintName.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboPrintName.Name = "cboPrintName"
        Me.cboPrintName.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboPrintName.Size = New System.Drawing.Size(455, 24)
        Me.cboPrintName.TabIndex = 74
        Me.cboPrintName.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cboPrintName.PropBag = resources.GetString("cboPrintName.PropBag")
        '
        'lblPrintName
        '
        Me.lblPrintName.AttachedTextBoxName = Nothing
        Me.lblPrintName.AutoSize = True
        Me.lblPrintName.BackColor = System.Drawing.Color.Transparent
        Me.lblPrintName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblPrintName.Font = New System.Drawing.Font("Calibri", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrintName.ForeColor = System.Drawing.Color.Black
        Me.lblPrintName.Location = New System.Drawing.Point(4, 39)
        Me.lblPrintName.Name = "lblPrintName"
        Me.lblPrintName.Size = New System.Drawing.Size(106, 21)
        Me.lblPrintName.TabIndex = 72
        Me.lblPrintName.Tag = Nothing
        Me.lblPrintName.Text = "Print Name *:"
        Me.lblPrintName.TextDetached = True
        Me.lblPrintName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cboCopyFrom
        '
        Me.cboCopyFrom.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboCopyFrom.AutoCompletion = True
        Me.cboCopyFrom.AutoDropDown = True
        Me.cboCopyFrom.Caption = ""
        Me.cboCopyFrom.CaptionHeight = 17
        Me.cboCopyFrom.CaptionVisible = False
        Me.cboCopyFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboCopyFrom.ColumnCaptionHeight = 17
        Me.cboCopyFrom.ColumnFooterHeight = 17
        Me.cboCopyFrom.ColumnHeaders = False
        Me.cboCopyFrom.ContentHeight = 18
        Me.cboCopyFrom.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboCopyFrom.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboCopyFrom.EditorFont = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCopyFrom.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCopyFrom.EditorHeight = 18
        Me.cboCopyFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCopyFrom.Images.Add(CType(resources.GetObject("cboCopyFrom.Images"), System.Drawing.Image))
        Me.cboCopyFrom.ItemHeight = 15
        Me.cboCopyFrom.Location = New System.Drawing.Point(116, 3)
        Me.cboCopyFrom.MatchCol = C1.Win.C1List.MatchColEnum.AllCols
        Me.cboCopyFrom.MatchEntry = C1.Win.C1List.MatchEntryEnum.Extended
        Me.cboCopyFrom.MatchEntryTimeout = CType(2000, Long)
        Me.cboCopyFrom.MaxDropDownItems = CType(5, Short)
        Me.cboCopyFrom.MaxLength = 32767
        Me.cboCopyFrom.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboCopyFrom.Name = "cboCopyFrom"
        Me.cboCopyFrom.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboCopyFrom.Size = New System.Drawing.Size(455, 24)
        Me.cboCopyFrom.TabIndex = 73
        Me.cboCopyFrom.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cboCopyFrom.PropBag = resources.GetString("cboCopyFrom.PropBag")
        '
        'lblCopyFrom
        '
        Me.lblCopyFrom.AttachedTextBoxName = Nothing
        Me.lblCopyFrom.AutoSize = True
        Me.lblCopyFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblCopyFrom.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCopyFrom.Font = New System.Drawing.Font("Calibri", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyFrom.ForeColor = System.Drawing.Color.Black
        Me.lblCopyFrom.Location = New System.Drawing.Point(8, 6)
        Me.lblCopyFrom.Name = "lblCopyFrom"
        Me.lblCopyFrom.Size = New System.Drawing.Size(91, 21)
        Me.lblCopyFrom.TabIndex = 71
        Me.lblCopyFrom.Tag = Nothing
        Me.lblCopyFrom.Text = "Copy From:"
        Me.lblCopyFrom.TextDetached = True
        Me.lblCopyFrom.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel2
        '
        Me.CtrlLabel2.AttachedTextBoxName = Nothing
        Me.CtrlLabel2.AutoSize = True
        Me.CtrlLabel2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel2.Font = New System.Drawing.Font("Calibri", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlLabel2.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel2.Location = New System.Drawing.Point(687, 6)
        Me.CtrlLabel2.Name = "CtrlLabel2"
        Me.CtrlLabel2.Size = New System.Drawing.Size(75, 21)
        Me.CtrlLabel2.TabIndex = 88
        Me.CtrlLabel2.Tag = Nothing
        Me.CtrlLabel2.Text = "Remarks:"
        Me.CtrlLabel2.TextDetached = True
        Me.CtrlLabel2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtRemarks
        '
        Me.txtRemarks.AutoSize = False
        Me.txtRemarks.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(768, 3)
        Me.txtRemarks.MaxLength = 1000
        Me.txtRemarks.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(455, 99)
        Me.txtRemarks.TabIndex = 87
        Me.txtRemarks.Tag = Nothing
        Me.txtRemarks.TextDetached = True
        Me.txtRemarks.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtRemarks.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblNetValue
        '
        Me.lblNetValue.AttachedTextBoxName = Nothing
        Me.lblNetValue.AutoSize = True
        Me.lblNetValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblNetValue.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblNetValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetValue.Location = New System.Drawing.Point(198, 470)
        Me.lblNetValue.Name = "lblNetValue"
        Me.lblNetValue.Padding = New System.Windows.Forms.Padding(5)
        Me.lblNetValue.Size = New System.Drawing.Size(24, 23)
        Me.lblNetValue.TabIndex = 5
        Me.lblNetValue.Tag = Nothing
        Me.lblNetValue.Text = "0"
        Me.lblNetValue.TextDetached = True
        '
        'lblDiscount
        '
        Me.lblDiscount.AttachedTextBoxName = Nothing
        Me.lblDiscount.AutoSize = True
        Me.lblDiscount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDiscount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscount.Location = New System.Drawing.Point(198, 420)
        Me.lblDiscount.Name = "lblDiscount"
        Me.lblDiscount.Padding = New System.Windows.Forms.Padding(5)
        Me.lblDiscount.Size = New System.Drawing.Size(24, 23)
        Me.lblDiscount.TabIndex = 4
        Me.lblDiscount.Tag = Nothing
        Me.lblDiscount.Text = "0"
        Me.lblDiscount.TextDetached = True
        '
        'txtTaxAmount
        '
        Me.txtTaxAmount.AttachedTextBoxName = Nothing
        Me.txtTaxAmount.AutoSize = True
        Me.txtTaxAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtTaxAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtTaxAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxAmount.Location = New System.Drawing.Point(198, 447)
        Me.txtTaxAmount.Name = "txtTaxAmount"
        Me.txtTaxAmount.Padding = New System.Windows.Forms.Padding(5)
        Me.txtTaxAmount.Size = New System.Drawing.Size(24, 23)
        Me.txtTaxAmount.TabIndex = 8
        Me.txtTaxAmount.Tag = Nothing
        Me.txtTaxAmount.Text = "0"
        Me.txtTaxAmount.TextDetached = True
        '
        'lblGross
        '
        Me.lblGross.AttachedTextBoxName = Nothing
        Me.lblGross.AutoSize = True
        Me.lblGross.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGross.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGross.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGross.Location = New System.Drawing.Point(199, 394)
        Me.lblGross.Name = "lblGross"
        Me.lblGross.Padding = New System.Windows.Forms.Padding(5)
        Me.lblGross.Size = New System.Drawing.Size(24, 23)
        Me.lblGross.TabIndex = 3
        Me.lblGross.Tag = Nothing
        Me.lblGross.Text = "0"
        Me.lblGross.TextDetached = True
        '
        'CtrlDiscount
        '
        Me.CtrlDiscount.AttachedTextBoxName = Nothing
        Me.CtrlDiscount.AutoSize = True
        Me.CtrlDiscount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlDiscount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlDiscount.Location = New System.Drawing.Point(28, 420)
        Me.CtrlDiscount.Name = "CtrlDiscount"
        Me.CtrlDiscount.Padding = New System.Windows.Forms.Padding(5)
        Me.CtrlDiscount.Size = New System.Drawing.Size(75, 23)
        Me.CtrlDiscount.TabIndex = 1
        Me.CtrlDiscount.Tag = Nothing
        Me.CtrlDiscount.Text = "Discount :"
        Me.CtrlDiscount.TextDetached = True
        '
        'CtrlGross
        '
        Me.CtrlGross.AttachedTextBoxName = Nothing
        Me.CtrlGross.AutoSize = True
        Me.CtrlGross.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlGross.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlGross.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlGross.Location = New System.Drawing.Point(28, 394)
        Me.CtrlGross.Name = "CtrlGross"
        Me.CtrlGross.Padding = New System.Windows.Forms.Padding(5)
        Me.CtrlGross.Size = New System.Drawing.Size(127, 23)
        Me.CtrlGross.TabIndex = 0
        Me.CtrlGross.Tag = Nothing
        Me.CtrlGross.Text = "Single Combo Amt :"
        Me.CtrlGross.TextDetached = True
        '
        'CtrlTaxAmt
        '
        Me.CtrlTaxAmt.AttachedTextBoxName = Nothing
        Me.CtrlTaxAmt.AutoSize = True
        Me.CtrlTaxAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlTaxAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlTaxAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlTaxAmt.Location = New System.Drawing.Point(28, 447)
        Me.CtrlTaxAmt.Name = "CtrlTaxAmt"
        Me.CtrlTaxAmt.Padding = New System.Windows.Forms.Padding(5)
        Me.CtrlTaxAmt.Size = New System.Drawing.Size(92, 23)
        Me.CtrlTaxAmt.TabIndex = 7
        Me.CtrlTaxAmt.Tag = Nothing
        Me.CtrlTaxAmt.Text = "Tax Amount :"
        Me.CtrlTaxAmt.TextDetached = True
        '
        'ctrlNetValue
        '
        Me.ctrlNetValue.AttachedTextBoxName = Nothing
        Me.ctrlNetValue.AutoSize = True
        Me.ctrlNetValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ctrlNetValue.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ctrlNetValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ctrlNetValue.Location = New System.Drawing.Point(28, 470)
        Me.ctrlNetValue.Name = "ctrlNetValue"
        Me.ctrlNetValue.Padding = New System.Windows.Forms.Padding(5)
        Me.ctrlNetValue.Size = New System.Drawing.Size(174, 23)
        Me.ctrlNetValue.TabIndex = 9
        Me.ctrlNetValue.Tag = Nothing
        Me.ctrlNetValue.Text = "Single Combo Gross Value :"
        Me.ctrlNetValue.TextDetached = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSave.Location = New System.Drawing.Point(14, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.SetArticleCode = Nothing
        Me.btnSave.SetRowIndex = 0
        Me.btnSave.Size = New System.Drawing.Size(93, 30)
        Me.btnSave.TabIndex = 114
        Me.btnSave.Tag = ""
        Me.btnSave.Text = "&Save"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSave.UseVisualStyleBackColor = True
        Me.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancel.Location = New System.Drawing.Point(226, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.SetArticleCode = Nothing
        Me.btnCancel.SetRowIndex = 0
        Me.btnCancel.Size = New System.Drawing.Size(105, 30)
        Me.btnCancel.TabIndex = 116
        Me.btnCancel.Tag = ""
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnClear.Location = New System.Drawing.Point(113, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.SetArticleCode = Nothing
        Me.btnClear.SetRowIndex = 0
        Me.btnClear.Size = New System.Drawing.Size(104, 30)
        Me.btnClear.TabIndex = 115
        Me.btnClear.Tag = ""
        Me.btnClear.Text = "&Clear"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnClear.UseVisualStyleBackColor = True
        Me.btnClear.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnAddBulkCombo
        '
        Me.btnAddBulkCombo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddBulkCombo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnAddBulkCombo.Location = New System.Drawing.Point(597, 72)
        Me.btnAddBulkCombo.Name = "btnAddBulkCombo"
        Me.btnAddBulkCombo.SetArticleCode = Nothing
        Me.btnAddBulkCombo.SetRowIndex = 0
        Me.btnAddBulkCombo.Size = New System.Drawing.Size(79, 30)
        Me.btnAddBulkCombo.TabIndex = 89
        Me.btnAddBulkCombo.Tag = ""
        Me.btnAddBulkCombo.Text = "&Add"
        Me.btnAddBulkCombo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAddBulkCombo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAddBulkCombo.UseVisualStyleBackColor = True
        Me.btnAddBulkCombo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtAddArticle
        '
        Me.txtAddArticle.AllowUpdateListBox = True
        Me.txtAddArticle.DtSearchData = Nothing
        Me.txtAddArticle.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddArticle.IsItemSelected = False
        Me.txtAddArticle.IsListBind = True
        Me.txtAddArticle.IsMouseOverList = False
        Me.txtAddArticle.IsMovingControl = False
        Me.txtAddArticle.Location = New System.Drawing.Point(116, 79)
        Me.txtAddArticle.lstNames = CType(resources.GetObject("txtAddArticle.lstNames"), System.Collections.Generic.List(Of String))
        Me.txtAddArticle.MaxLength = 35
        Me.txtAddArticle.Name = "txtAddArticle"
        Me.txtAddArticle.SearchBasedOnDB = Nothing
        Me.txtAddArticle.SearchQueryOnDB = Nothing
        Me.txtAddArticle.Size = New System.Drawing.Size(455, 23)
        Me.txtAddArticle.TabIndex = 86
        '
        'CtrlLabel1
        '
        Me.CtrlLabel1.AttachedTextBoxName = Nothing
        Me.CtrlLabel1.AutoSize = True
        Me.CtrlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.Font = New System.Drawing.Font("Calibri", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlLabel1.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel1.Location = New System.Drawing.Point(8, 79)
        Me.CtrlLabel1.Name = "CtrlLabel1"
        Me.CtrlLabel1.Size = New System.Drawing.Size(103, 21)
        Me.CtrlLabel1.TabIndex = 74
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.Text = "Add Article *:"
        Me.CtrlLabel1.TextDetached = True
        Me.CtrlLabel1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmPCBulkOrderCombo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1354, 733)
        Me.Controls.Add(Me.C1Sizer4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPCBulkOrderCombo"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Combo"
        CType(Me.C1Sizer4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer4.ResumeLayout(False)
        Me.mainPanel.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.GrdFixedCombo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.DgBulkComboGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.cboPrintName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPrintName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCopyFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCopyFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNetValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTaxAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGross, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlDiscount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlGross, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlTaxAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ctrlNetValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents C1Sizer4 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents mainPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents CboFixed As System.Windows.Forms.RadioButton
    Friend WithEvents CboVariable As System.Windows.Forms.RadioButton
    Friend WithEvents rbnSingle As System.Windows.Forms.RadioButton
    Friend WithEvents rbnCombo As System.Windows.Forms.RadioButton
    Friend WithEvents cboPrintName As Spectrum.ctrlCombo
    Friend WithEvents cboCopyFrom As Spectrum.ctrlCombo
    Friend WithEvents lblPrintName As Spectrum.CtrlLabel
    Friend WithEvents lblCopyFrom As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel2 As Spectrum.CtrlLabel
    Friend WithEvents txtRemarks As Spectrum.CtrlTextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As Spectrum.Controls.Label
    Friend WithEvents CtrlDiscount As Spectrum.CtrlLabel
    Friend WithEvents CtrlGross As Spectrum.CtrlLabel
    Friend WithEvents lblGross As Spectrum.CtrlLabel
    Friend WithEvents lblDiscount As Spectrum.CtrlLabel
    Friend WithEvents lblNetValue As Spectrum.CtrlLabel
    Friend WithEvents CtrlTaxAmt As Spectrum.CtrlLabel
    Friend WithEvents txtTaxAmount As Spectrum.CtrlLabel
    Friend WithEvents ctrlNetValue As Spectrum.CtrlLabel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnSave As Spectrum.CtrlBtn
    Friend WithEvents btnCancel As Spectrum.CtrlBtn
    Friend WithEvents btnClear As Spectrum.CtrlBtn
    Friend WithEvents DgBulkComboGrid As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents btnAddBulkCombo As Spectrum.CtrlBtn
    Friend WithEvents txtAddArticle As Spectrum.AndroidSearchTextBox
    Friend WithEvents CtrlLabel1 As Spectrum.CtrlLabel
    Friend WithEvents TxtFixedPriceEnter As System.Windows.Forms.TextBox
    Friend WithEvents GrdFixedCombo As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel

End Class
