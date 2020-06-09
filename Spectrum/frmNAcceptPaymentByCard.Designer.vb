<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNAcceptPaymentByCard
    Inherits Spectrum.CtrlPopupForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNAcceptPaymentByCard))
        Me.CardLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTotalAmount = New Spectrum.CtrlLabel()
        Me.txtMinAmount = New System.Windows.Forms.Label()
        Me.lblMinAmount = New System.Windows.Forms.Label()
        Me.lblExpiryDate = New Spectrum.CtrlLabel()
        Me.dtpExpiryDate = New Spectrum.ctrlDate()
        Me.txtCardNo = New Spectrum.CtrlTextBox()
        Me.lblCardNo = New Spectrum.CtrlLabel()
        Me.lblBankName = New Spectrum.CtrlLabel()
        Me.txtCalCollectAmount = New Spectrum.CtrlTextBox()
        Me.cmbBankName = New Spectrum.ctrlCombo()
        Me.lblCollectAmount = New Spectrum.CtrlLabel()
        Me.actionPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSave = New Spectrum.CtrlBtn()
        Me.btnGift = New Spectrum.CtrlBtn()
        Me.btnCancle = New Spectrum.CtrlBtn()
        Me.lblBillAmount = New System.Windows.Forms.Label()
        Me.CardLayout.SuspendLayout()
        CType(Me.lblTotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExpiryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpExpiryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCardNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCardNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCalCollectAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCollectAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.actionPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'CardLayout
        '
        Me.CardLayout.ColumnCount = 5
        Me.CardLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104.0!))
        Me.CardLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.29927!))
        Me.CardLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.03406!))
        Me.CardLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.28224!))
        Me.CardLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.87105!))
        Me.CardLayout.Controls.Add(Me.lblTotalAmount, 0, 0)
        Me.CardLayout.Controls.Add(Me.txtMinAmount, 4, 0)
        Me.CardLayout.Controls.Add(Me.lblMinAmount, 3, 0)
        Me.CardLayout.Controls.Add(Me.lblExpiryDate, 0, 4)
        Me.CardLayout.Controls.Add(Me.dtpExpiryDate, 2, 4)
        Me.CardLayout.Controls.Add(Me.txtCardNo, 2, 3)
        Me.CardLayout.Controls.Add(Me.lblCardNo, 0, 3)
        Me.CardLayout.Controls.Add(Me.lblBankName, 0, 2)
        Me.CardLayout.Controls.Add(Me.txtCalCollectAmount, 2, 1)
        Me.CardLayout.Controls.Add(Me.cmbBankName, 2, 2)
        Me.CardLayout.Controls.Add(Me.lblCollectAmount, 0, 1)
        Me.CardLayout.Controls.Add(Me.actionPanel, 0, 5)
        Me.CardLayout.Controls.Add(Me.lblBillAmount, 2, 0)
        Me.CardLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CardLayout.Location = New System.Drawing.Point(0, 0)
        Me.CardLayout.Name = "CardLayout"
        Me.CardLayout.RowCount = 7
        Me.CardLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.CardLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.CardLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.CardLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.CardLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.CardLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.CardLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.CardLayout.Size = New System.Drawing.Size(515, 249)
        Me.CardLayout.TabIndex = 52
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.AttachedTextBoxName = Nothing
        Me.lblTotalAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTotalAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CardLayout.SetColumnSpan(Me.lblTotalAmount, 2)
        Me.lblTotalAmount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTotalAmount.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblTotalAmount.ForeColor = System.Drawing.Color.Black
        Me.lblTotalAmount.Location = New System.Drawing.Point(3, 0)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(127, 50)
        Me.lblTotalAmount.TabIndex = 34
        Me.lblTotalAmount.Tag = Nothing
        Me.lblTotalAmount.Text = "Bill Amount       :"
        Me.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTotalAmount.TextDetached = True
        Me.lblTotalAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtMinAmount
        '
        Me.txtMinAmount.AutoSize = True
        Me.txtMinAmount.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtMinAmount.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold)
        Me.txtMinAmount.ForeColor = System.Drawing.Color.Red
        Me.txtMinAmount.Location = New System.Drawing.Point(423, 0)
        Me.txtMinAmount.Name = "txtMinAmount"
        Me.txtMinAmount.Size = New System.Drawing.Size(46, 50)
        Me.txtMinAmount.TabIndex = 16
        Me.txtMinAmount.Text = "0.00"
        Me.txtMinAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMinAmount
        '
        Me.lblMinAmount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMinAmount.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMinAmount.Location = New System.Drawing.Point(242, 0)
        Me.lblMinAmount.Name = "lblMinAmount"
        Me.lblMinAmount.Size = New System.Drawing.Size(175, 50)
        Me.lblMinAmount.TabIndex = 49
        Me.lblMinAmount.Text = "Min. Advance Amount :"
        Me.lblMinAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblExpiryDate
        '
        Me.lblExpiryDate.AttachedTextBoxName = Nothing
        Me.lblExpiryDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblExpiryDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblExpiryDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CardLayout.SetColumnSpan(Me.lblExpiryDate, 2)
        Me.lblExpiryDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblExpiryDate.ForeColor = System.Drawing.Color.Black
        Me.lblExpiryDate.Location = New System.Drawing.Point(4, 138)
        Me.lblExpiryDate.Margin = New System.Windows.Forms.Padding(4)
        Me.lblExpiryDate.Name = "lblExpiryDate"
        Me.lblExpiryDate.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.lblExpiryDate.Size = New System.Drawing.Size(125, 20)
        Me.lblExpiryDate.TabIndex = 8
        Me.lblExpiryDate.Tag = Nothing
        Me.lblExpiryDate.Text = "Expiry Date"
        Me.lblExpiryDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblExpiryDate.TextDetached = True
        Me.lblExpiryDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'dtpExpiryDate
        '
        Me.dtpExpiryDate.AutoSize = False
        Me.dtpExpiryDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.dtpExpiryDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtpExpiryDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage"), System.Drawing.Image)
        '
        '
        '
        Me.dtpExpiryDate.Calendar.AccessibleRole = System.Windows.Forms.AccessibleRole.Table
        Me.dtpExpiryDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtpExpiryDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.CardLayout.SetColumnSpan(Me.dtpExpiryDate, 2)
        Me.dtpExpiryDate.DisplayFormat.CustomFormat = "MM-yyyy"
        Me.dtpExpiryDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.dtpExpiryDate.DisplayFormat.Inherit = CType((((C1.Win.C1Input.FormatInfoInheritFlags.NullText Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpExpiryDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpExpiryDate.EditFormat.CustomFormat = "MM-yyyy"
        Me.dtpExpiryDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.dtpExpiryDate.EditFormat.Inherit = CType((((C1.Win.C1Input.FormatInfoInheritFlags.NullText Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpExpiryDate.EmptyAsNull = True
        Me.dtpExpiryDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.dtpExpiryDate.Location = New System.Drawing.Point(134, 135)
        Me.dtpExpiryDate.Margin = New System.Windows.Forms.Padding(1)
        Me.dtpExpiryDate.MaxLength = 35
        Me.dtpExpiryDate.Name = "dtpExpiryDate"
        Me.dtpExpiryDate.Size = New System.Drawing.Size(285, 26)
        Me.dtpExpiryDate.TabIndex = 3
        Me.dtpExpiryDate.Tag = Nothing
        Me.dtpExpiryDate.TrimStart = True
        Me.dtpExpiryDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        Me.dtpExpiryDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtpExpiryDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'txtCardNo
        '
        Me.txtCardNo.AutoSize = False
        Me.txtCardNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtCardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CardLayout.SetColumnSpan(Me.txtCardNo, 2)
        Me.txtCardNo.CustomFormat = "0"
        Me.txtCardNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCardNo.Location = New System.Drawing.Point(134, 107)
        Me.txtCardNo.Margin = New System.Windows.Forms.Padding(1)
        Me.txtCardNo.MaxLength = 18
        Me.txtCardNo.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtCardNo.Name = "txtCardNo"
        Me.txtCardNo.Size = New System.Drawing.Size(285, 26)
        Me.txtCardNo.TabIndex = 2
        Me.txtCardNo.Tag = "NO"
        Me.txtCardNo.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtCardNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCardNo
        '
        Me.lblCardNo.AttachedTextBoxName = Nothing
        Me.lblCardNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCardNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CardLayout.SetColumnSpan(Me.lblCardNo, 2)
        Me.lblCardNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCardNo.ForeColor = System.Drawing.Color.Black
        Me.lblCardNo.Location = New System.Drawing.Point(4, 110)
        Me.lblCardNo.Margin = New System.Windows.Forms.Padding(4)
        Me.lblCardNo.Name = "lblCardNo"
        Me.lblCardNo.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.lblCardNo.Size = New System.Drawing.Size(125, 20)
        Me.lblCardNo.TabIndex = 7
        Me.lblCardNo.Tag = Nothing
        Me.lblCardNo.Text = "*Card No."
        Me.lblCardNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCardNo.TextDetached = True
        Me.lblCardNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblBankName
        '
        Me.lblBankName.AttachedTextBoxName = Nothing
        Me.lblBankName.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblBankName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblBankName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CardLayout.SetColumnSpan(Me.lblBankName, 2)
        Me.lblBankName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblBankName.ForeColor = System.Drawing.Color.Black
        Me.lblBankName.Location = New System.Drawing.Point(4, 82)
        Me.lblBankName.Margin = New System.Windows.Forms.Padding(4)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.lblBankName.Size = New System.Drawing.Size(125, 20)
        Me.lblBankName.TabIndex = 6
        Me.lblBankName.Tag = Nothing
        Me.lblBankName.Text = "*Bank Name"
        Me.lblBankName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBankName.TextDetached = True
        Me.lblBankName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtCalCollectAmount
        '
        Me.txtCalCollectAmount.AcceptsEscape = False
        Me.txtCalCollectAmount.AutoSize = False
        Me.txtCalCollectAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtCalCollectAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CardLayout.SetColumnSpan(Me.txtCalCollectAmount, 2)
        Me.txtCalCollectAmount.DataType = GetType(Long)
        Me.txtCalCollectAmount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCalCollectAmount.Location = New System.Drawing.Point(134, 51)
        Me.txtCalCollectAmount.Margin = New System.Windows.Forms.Padding(1)
        Me.txtCalCollectAmount.MaxLength = 15
        Me.txtCalCollectAmount.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtCalCollectAmount.Name = "txtCalCollectAmount"
        Me.txtCalCollectAmount.Size = New System.Drawing.Size(285, 26)
        Me.txtCalCollectAmount.TabIndex = 0
        Me.txtCalCollectAmount.Tag = Nothing
        Me.txtCalCollectAmount.TextDetached = True
        Me.txtCalCollectAmount.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtCalCollectAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmbBankName
        '
        Me.cmbBankName.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbBankName.AutoCompletion = True
        Me.cmbBankName.AutoDropDown = True
        Me.cmbBankName.Caption = ""
        Me.cmbBankName.CaptionHeight = 17
        Me.cmbBankName.CaptionVisible = False
        Me.cmbBankName.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbBankName.ColumnCaptionHeight = 17
        Me.cmbBankName.ColumnFooterHeight = 17
        Me.cmbBankName.ColumnHeaders = False
        Me.CardLayout.SetColumnSpan(Me.cmbBankName, 2)
        Me.cmbBankName.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList
        Me.cmbBankName.ContentHeight = 18
        Me.cmbBankName.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbBankName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbBankName.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cmbBankName.EditorFont = New System.Drawing.Font("Verdana", 9.75!)
        Me.cmbBankName.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbBankName.EditorHeight = 18
        Me.cmbBankName.ExtendRightColumn = True
        Me.cmbBankName.Images.Add(CType(resources.GetObject("cmbBankName.Images"), System.Drawing.Image))
        Me.cmbBankName.ItemHeight = 15
        Me.cmbBankName.Location = New System.Drawing.Point(134, 79)
        Me.cmbBankName.Margin = New System.Windows.Forms.Padding(1)
        Me.cmbBankName.MatchEntryTimeout = CType(2000, Long)
        Me.cmbBankName.MaxDropDownItems = CType(5, Short)
        Me.cmbBankName.MaxLength = 75
        Me.cmbBankName.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbBankName.Name = "cmbBankName"
        Me.cmbBankName.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbBankName.Size = New System.Drawing.Size(285, 24)
        Me.cmbBankName.TabIndex = 1
        Me.cmbBankName.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbBankName.PropBag = resources.GetString("cmbBankName.PropBag")
        '
        'lblCollectAmount
        '
        Me.lblCollectAmount.AttachedTextBoxName = Nothing
        Me.lblCollectAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCollectAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCollectAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CardLayout.SetColumnSpan(Me.lblCollectAmount, 2)
        Me.lblCollectAmount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCollectAmount.ForeColor = System.Drawing.Color.Black
        Me.lblCollectAmount.Location = New System.Drawing.Point(4, 54)
        Me.lblCollectAmount.Margin = New System.Windows.Forms.Padding(4)
        Me.lblCollectAmount.Name = "lblCollectAmount"
        Me.lblCollectAmount.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.lblCollectAmount.Size = New System.Drawing.Size(125, 20)
        Me.lblCollectAmount.TabIndex = 36
        Me.lblCollectAmount.Tag = Nothing
        Me.lblCollectAmount.Text = "Collect Amount"
        Me.lblCollectAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCollectAmount.TextDetached = True
        Me.lblCollectAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'actionPanel
        '
        Me.actionPanel.ColumnCount = 5
        Me.CardLayout.SetColumnSpan(Me.actionPanel, 5)
        Me.actionPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.684318!))
        Me.actionPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.77189!))
        Me.actionPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.77189!))
        Me.actionPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.77189!))
        Me.actionPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.actionPanel.Controls.Add(Me.btnSave, 1, 0)
        Me.actionPanel.Controls.Add(Me.btnGift, 2, 0)
        Me.actionPanel.Controls.Add(Me.btnCancle, 3, 0)
        Me.actionPanel.Location = New System.Drawing.Point(0, 162)
        Me.actionPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.actionPanel.Name = "actionPanel"
        Me.actionPanel.RowCount = 1
        Me.actionPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.actionPanel.Size = New System.Drawing.Size(515, 55)
        Me.actionPanel.TabIndex = 51
        '
        'btnSave
        '
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSave.Image = Global.Spectrum.My.Resources.Resources.save_print_btn
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(26, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.SetArticleCode = Nothing
        Me.btnSave.SetRowIndex = 0
        Me.btnSave.Size = New System.Drawing.Size(150, 49)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "F10" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Save Print"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnSave.UseVisualStyleBackColor = True
        Me.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnGift
        '
        Me.btnGift.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnGift.Image = Global.Spectrum.My.Resources.Resources.save_gift_print
        Me.btnGift.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGift.Location = New System.Drawing.Point(182, 3)
        Me.btnGift.Name = "btnGift"
        Me.btnGift.SetArticleCode = Nothing
        Me.btnGift.SetRowIndex = 0
        Me.btnGift.Size = New System.Drawing.Size(150, 49)
        Me.btnGift.TabIndex = 1
        Me.btnGift.Text = "F11" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Save/Gift Print"
        Me.btnGift.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnGift.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnGift.UseVisualStyleBackColor = True
        Me.btnGift.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnCancle
        '
        Me.btnCancle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancle.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancle.Location = New System.Drawing.Point(338, 3)
        Me.btnCancle.Name = "btnCancle"
        Me.btnCancle.SetArticleCode = Nothing
        Me.btnCancle.SetRowIndex = 0
        Me.btnCancle.Size = New System.Drawing.Size(150, 49)
        Me.btnCancle.TabIndex = 2
        Me.btnCancle.Text = "Cancel"
        Me.btnCancle.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCancle.UseVisualStyleBackColor = True
        Me.btnCancle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblBillAmount
        '
        Me.lblBillAmount.AutoSize = True
        Me.lblBillAmount.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblBillAmount.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblBillAmount.ForeColor = System.Drawing.Color.Red
        Me.lblBillAmount.Location = New System.Drawing.Point(136, 0)
        Me.lblBillAmount.Name = "lblBillAmount"
        Me.lblBillAmount.Size = New System.Drawing.Size(46, 50)
        Me.lblBillAmount.TabIndex = 1
        Me.lblBillAmount.Text = "0.00"
        Me.lblBillAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmNAcceptPaymentByCard
        '
        Me.ClientSize = New System.Drawing.Size(515, 249)
        Me.Controls.Add(Me.CardLayout)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNAcceptPaymentByCard"
        Me.Text = "Accept Payment By Card"
        Me.CardLayout.ResumeLayout(False)
        Me.CardLayout.PerformLayout()
        CType(Me.lblTotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExpiryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpExpiryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCardNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCardNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCalCollectAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCollectAmount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.actionPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblBillAmount As System.Windows.Forms.Label
    Friend WithEvents txtMinAmount As System.Windows.Forms.Label
    Friend WithEvents CardLayout As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblTotalAmount As Spectrum.CtrlLabel
    Friend WithEvents lblMinAmount As System.Windows.Forms.Label
    Friend WithEvents lblExpiryDate As Spectrum.CtrlLabel
    Friend WithEvents dtpExpiryDate As Spectrum.ctrlDate
    Friend WithEvents txtCardNo As Spectrum.CtrlTextBox
    Friend WithEvents lblCardNo As Spectrum.CtrlLabel
    Friend WithEvents lblBankName As Spectrum.CtrlLabel
    Friend WithEvents txtCalCollectAmount As Spectrum.CtrlTextBox
    Friend WithEvents cmbBankName As Spectrum.ctrlCombo
    Friend WithEvents lblCollectAmount As Spectrum.CtrlLabel
    Friend WithEvents actionPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnSave As Spectrum.CtrlBtn
    Friend WithEvents btnGift As Spectrum.CtrlBtn
    Friend WithEvents btnCancle As Spectrum.CtrlBtn

End Class
