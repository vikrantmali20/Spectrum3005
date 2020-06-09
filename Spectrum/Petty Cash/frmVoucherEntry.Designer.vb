<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPCVoucherEntry
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPCVoucherEntry))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblVoucherType = New System.Windows.Forms.Label()
        Me.lblVcherDate = New System.Windows.Forms.Label()
        Me.lblVcherDateValue = New System.Windows.Forms.Label()
        Me.cmbVoucherType = New System.Windows.Forms.ComboBox()
        Me.lblVcherNoValue = New System.Windows.Forms.Label()
        Me.lblVcherNo = New System.Windows.Forms.Label()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmbVcherAccType = New System.Windows.Forms.ComboBox()
        Me.lblVcherAccType = New System.Windows.Forms.Label()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblPartyOption = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbnOther = New System.Windows.Forms.RadioButton()
        Me.rbnSupplier = New System.Windows.Forms.RadioButton()
        Me.rbnEmployee = New System.Windows.Forms.RadioButton()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblPartyName = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtPartyName = New Spectrum.CtrlTextBox()
        Me.cmbPartyName = New System.Windows.Forms.ComboBox()
        Me.dgNarration = New System.Windows.Forms.DataGridView()
        Me.IsSelected = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.LineNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Narration = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTotalAmount = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblTotalValue = New System.Windows.Forms.Label()
        Me.lblCurrency = New System.Windows.Forms.Label()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblAmtInWordValue = New System.Windows.Forms.Label()
        Me.lblAmountInWord = New System.Windows.Forms.Label()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblPreparedBy = New System.Windows.Forms.Label()
        Me.lblPrepByValue = New System.Windows.Forms.Label()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtRecievedBy = New Spectrum.CtrlTextBox()
        Me.lblApprovedBy = New System.Windows.Forms.Label()
        Me.txtApprovedBy = New Spectrum.CtrlTextBox()
        Me.lblRecievedBy = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnAddNarration = New Spectrum.CtrlBtn()
        Me.btnDeleteNarration = New Spectrum.CtrlBtn()
        Me.FlowLayoutPanel3 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnExit = New Spectrum.CtrlBtn()
        Me.btnCancel = New Spectrum.CtrlBtn()
        Me.btnSave = New Spectrum.CtrlBtn()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.dtRefDocDate = New Spectrum.ctrlDate()
        Me.lblRefDocDate = New System.Windows.Forms.Label()
        Me.lblRefDocNo = New System.Windows.Forms.Label()
        Me.txtRefDocNo = New Spectrum.CtrlTextBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.txtPartyName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgNarration, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        CType(Me.txtRecievedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtApprovedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel2.SuspendLayout()
        Me.FlowLayoutPanel3.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        CType(Me.dtRefDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRefDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel4, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel5, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.dgNarration, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel6, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel7, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel8, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel9, 0, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.FlowLayoutPanel2, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.FlowLayoutPanel3, 0, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel3, 0, 10)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 12
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.48063!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.906191!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.103064!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.4422!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.051764!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.15981!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.315572!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.646774!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.045008!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.133688!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.133688!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.581611!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(812, 512)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 4
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.78325!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.35468!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.43842!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.30049!))
        Me.TableLayoutPanel2.Controls.Add(Me.lblVoucherType, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblVcherDate, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.lblVcherDateValue, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.cmbVoucherType, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblVcherNoValue, 3, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.lblVcherNo, 2, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.44828!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.55172!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(812, 58)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'lblVoucherType
        '
        Me.lblVoucherType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblVoucherType.AutoSize = True
        Me.lblVoucherType.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblVoucherType.Location = New System.Drawing.Point(3, 7)
        Me.lblVoucherType.Name = "lblVoucherType"
        Me.lblVoucherType.Size = New System.Drawing.Size(105, 17)
        Me.lblVoucherType.TabIndex = 0
        Me.lblVoucherType.Text = "Voucher Type :"
        Me.lblVoucherType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblVcherDate
        '
        Me.lblVcherDate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblVcherDate.AutoSize = True
        Me.lblVcherDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblVcherDate.Location = New System.Drawing.Point(3, 36)
        Me.lblVcherDate.Name = "lblVcherDate"
        Me.lblVcherDate.Size = New System.Drawing.Size(46, 17)
        Me.lblVcherDate.TabIndex = 1
        Me.lblVcherDate.Text = "Date :"
        Me.lblVcherDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblVcherDateValue
        '
        Me.lblVcherDateValue.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblVcherDateValue.AutoSize = True
        Me.lblVcherDateValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblVcherDateValue.Location = New System.Drawing.Point(188, 36)
        Me.lblVcherDateValue.Name = "lblVcherDateValue"
        Me.lblVcherDateValue.Size = New System.Drawing.Size(0, 17)
        Me.lblVcherDateValue.TabIndex = 2
        Me.lblVcherDateValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbVoucherType
        '
        Me.cmbVoucherType.DisplayMember = "VoucherTypeName"
        Me.cmbVoucherType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbVoucherType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVoucherType.ItemHeight = 16
        Me.cmbVoucherType.Location = New System.Drawing.Point(188, 3)
        Me.cmbVoucherType.MaxDropDownItems = 5
        Me.cmbVoucherType.MaxLength = 32767
        Me.cmbVoucherType.Name = "cmbVoucherType"
        Me.cmbVoucherType.Size = New System.Drawing.Size(208, 24)
        Me.cmbVoucherType.TabIndex = 3
        Me.cmbVoucherType.ValueMember = "VoucherTypeCode"
        '
        'lblVcherNoValue
        '
        Me.lblVcherNoValue.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblVcherNoValue.AutoSize = True
        Me.lblVcherNoValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblVcherNoValue.Location = New System.Drawing.Point(503, 36)
        Me.lblVcherNoValue.Name = "lblVcherNoValue"
        Me.lblVcherNoValue.Size = New System.Drawing.Size(0, 17)
        Me.lblVcherNoValue.TabIndex = 5
        Me.lblVcherNoValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblVcherNo
        '
        Me.lblVcherNo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblVcherNo.AutoSize = True
        Me.lblVcherNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblVcherNo.Location = New System.Drawing.Point(402, 36)
        Me.lblVcherNo.Name = "lblVcherNo"
        Me.lblVcherNo.Size = New System.Drawing.Size(93, 17)
        Me.lblVcherNo.TabIndex = 4
        Me.lblVcherNo.Text = "Voucher no. :"
        Me.lblVcherNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.9064!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.0936!))
        Me.TableLayoutPanel3.Controls.Add(Me.cmbVcherAccType, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.lblVcherAccType, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 58)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(812, 30)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'cmbVcherAccType
        '
        Me.cmbVcherAccType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmbVcherAccType.DisplayMember = "AccountType"
        Me.cmbVcherAccType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVcherAccType.ItemHeight = 16
        Me.cmbVcherAccType.Location = New System.Drawing.Point(188, 3)
        Me.cmbVcherAccType.MaxDropDownItems = 5
        Me.cmbVcherAccType.MaxLength = 32767
        Me.cmbVcherAccType.Name = "cmbVcherAccType"
        Me.cmbVcherAccType.Size = New System.Drawing.Size(321, 24)
        Me.cmbVcherAccType.TabIndex = 1
        Me.cmbVcherAccType.ValueMember = "VoucherAccountID"
        '
        'lblVcherAccType
        '
        Me.lblVcherAccType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblVcherAccType.AutoSize = True
        Me.lblVcherAccType.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblVcherAccType.Location = New System.Drawing.Point(3, 6)
        Me.lblVcherAccType.Name = "lblVcherAccType"
        Me.lblVcherAccType.Size = New System.Drawing.Size(179, 17)
        Me.lblVcherAccType.TabIndex = 0
        Me.lblVcherAccType.Text = "Type of Expense / Income :"
        Me.lblVcherAccType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.9064!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.0936!))
        Me.TableLayoutPanel4.Controls.Add(Me.lblPartyOption, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Panel1, 1, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 88)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(812, 31)
        Me.TableLayoutPanel4.TabIndex = 2
        '
        'lblPartyOption
        '
        Me.lblPartyOption.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPartyOption.AutoSize = True
        Me.lblPartyOption.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblPartyOption.Location = New System.Drawing.Point(3, 7)
        Me.lblPartyOption.Name = "lblPartyOption"
        Me.lblPartyOption.Size = New System.Drawing.Size(143, 17)
        Me.lblPartyOption.TabIndex = 0
        Me.lblPartyOption.Text = "Paid / Receive From :"
        Me.lblPartyOption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbnOther)
        Me.Panel1.Controls.Add(Me.rbnSupplier)
        Me.Panel1.Controls.Add(Me.rbnEmployee)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(185, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(627, 31)
        Me.Panel1.TabIndex = 1
        '
        'rbnOther
        '
        Me.rbnOther.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbnOther.AutoSize = True
        Me.rbnOther.Location = New System.Drawing.Point(181, 5)
        Me.rbnOther.Name = "rbnOther"
        Me.rbnOther.Size = New System.Drawing.Size(62, 21)
        Me.rbnOther.TabIndex = 2
        Me.rbnOther.TabStop = True
        Me.rbnOther.Text = "Other"
        Me.rbnOther.UseVisualStyleBackColor = True
        '
        'rbnSupplier
        '
        Me.rbnSupplier.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbnSupplier.AutoSize = True
        Me.rbnSupplier.Location = New System.Drawing.Point(97, 5)
        Me.rbnSupplier.Name = "rbnSupplier"
        Me.rbnSupplier.Size = New System.Drawing.Size(78, 21)
        Me.rbnSupplier.TabIndex = 1
        Me.rbnSupplier.TabStop = True
        Me.rbnSupplier.Text = "Supplier"
        Me.rbnSupplier.UseVisualStyleBackColor = True
        '
        'rbnEmployee
        '
        Me.rbnEmployee.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbnEmployee.AutoSize = True
        Me.rbnEmployee.Location = New System.Drawing.Point(3, 5)
        Me.rbnEmployee.Name = "rbnEmployee"
        Me.rbnEmployee.Size = New System.Drawing.Size(88, 21)
        Me.rbnEmployee.TabIndex = 0
        Me.rbnEmployee.TabStop = True
        Me.rbnEmployee.Text = "Employee"
        Me.rbnEmployee.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.78325!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.21675!))
        Me.TableLayoutPanel5.Controls.Add(Me.lblPartyName, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.Panel2, 1, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 119)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(812, 32)
        Me.TableLayoutPanel5.TabIndex = 3
        '
        'lblPartyName
        '
        Me.lblPartyName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPartyName.AutoSize = True
        Me.lblPartyName.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblPartyName.Location = New System.Drawing.Point(3, 7)
        Me.lblPartyName.Name = "lblPartyName"
        Me.lblPartyName.Size = New System.Drawing.Size(90, 17)
        Me.lblPartyName.TabIndex = 0
        Me.lblPartyName.Text = "Party Name :"
        Me.lblPartyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtPartyName)
        Me.Panel2.Controls.Add(Me.cmbPartyName)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(184, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(628, 32)
        Me.Panel2.TabIndex = 1
        '
        'txtPartyName
        '
        Me.txtPartyName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtPartyName.AutoSize = False
        Me.txtPartyName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtPartyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartyName.Location = New System.Drawing.Point(3, 3)
        Me.txtPartyName.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtPartyName.Name = "txtPartyName"
        Me.txtPartyName.Size = New System.Drawing.Size(407, 24)
        Me.txtPartyName.TabIndex = 2
        Me.txtPartyName.Tag = Nothing
        Me.txtPartyName.VerticalAlign = C1.Win.C1Input.VerticalAlignEnum.Middle
        Me.txtPartyName.Visible = False
        Me.txtPartyName.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtPartyName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmbPartyName
        '
        Me.cmbPartyName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmbPartyName.DisplayMember = "PartyDisplayName"
        Me.cmbPartyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPartyName.ItemHeight = 16
        Me.cmbPartyName.Location = New System.Drawing.Point(3, 3)
        Me.cmbPartyName.MaxDropDownItems = 5
        Me.cmbPartyName.MaxLength = 32767
        Me.cmbPartyName.Name = "cmbPartyName"
        Me.cmbPartyName.Size = New System.Drawing.Size(319, 24)
        Me.cmbPartyName.TabIndex = 1
        Me.cmbPartyName.ValueMember = "PartyCode"
        Me.cmbPartyName.Visible = False
        '
        'dgNarration
        '
        Me.dgNarration.AllowUserToAddRows = False
        Me.dgNarration.AllowUserToDeleteRows = False
        Me.dgNarration.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.dgNarration.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgNarration.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IsSelected, Me.LineNumber, Me.Narration, Me.Amount})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.NullValue = Nothing
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgNarration.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgNarration.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgNarration.Location = New System.Drawing.Point(3, 184)
        Me.dgNarration.Name = "dgNarration"
        Me.dgNarration.RowHeadersVisible = False
        Me.dgNarration.Size = New System.Drawing.Size(806, 112)
        Me.dgNarration.TabIndex = 4
        '
        'IsSelected
        '
        Me.IsSelected.DataPropertyName = "IsSelected"
        Me.IsSelected.HeaderText = "Select"
        Me.IsSelected.Name = "IsSelected"
        Me.IsSelected.Width = 50
        '
        'LineNumber
        '
        Me.LineNumber.DataPropertyName = "LineNumber"
        Me.LineNumber.HeaderText = "Sr No"
        Me.LineNumber.Name = "LineNumber"
        Me.LineNumber.ReadOnly = True
        Me.LineNumber.Width = 70
        '
        'Narration
        '
        Me.Narration.DataPropertyName = "Narration"
        Me.Narration.HeaderText = "Narration"
        Me.Narration.Name = "Narration"
        Me.Narration.Width = 600
        '
        'Amount
        '
        Me.Amount.DataPropertyName = "Amount"
        DataGridViewCellStyle1.Format = "N2"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.Amount.DefaultCellStyle = DataGridViewCellStyle1
        Me.Amount.HeaderText = "Amount"
        Me.Amount.Name = "Amount"
        Me.Amount.Width = 70
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 2
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.lblTotalAmount, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.FlowLayoutPanel1, 1, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(0, 299)
        Me.TableLayoutPanel6.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(812, 27)
        Me.TableLayoutPanel6.TabIndex = 5
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblTotalAmount.AutoSize = True
        Me.lblTotalAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTotalAmount.Location = New System.Drawing.Point(674, 5)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(48, 17)
        Me.lblTotalAmount.TabIndex = 1
        Me.lblTotalAmount.Text = "Total :"
        Me.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.lblTotalValue)
        Me.FlowLayoutPanel1.Controls.Add(Me.lblCurrency)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(725, 0)
        Me.FlowLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(87, 27)
        Me.FlowLayoutPanel1.TabIndex = 2
        '
        'lblTotalValue
        '
        Me.lblTotalValue.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTotalValue.AutoSize = True
        Me.lblTotalValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTotalValue.Location = New System.Drawing.Point(3, 4)
        Me.lblTotalValue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 0)
        Me.lblTotalValue.Name = "lblTotalValue"
        Me.lblTotalValue.Size = New System.Drawing.Size(32, 17)
        Me.lblTotalValue.TabIndex = 0
        Me.lblTotalValue.Text = "100"
        Me.lblTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCurrency
        '
        Me.lblCurrency.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCurrency.AutoSize = True
        Me.lblCurrency.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCurrency.Location = New System.Drawing.Point(41, 4)
        Me.lblCurrency.Margin = New System.Windows.Forms.Padding(3, 4, 3, 0)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(31, 17)
        Me.lblCurrency.TabIndex = 1
        Me.lblCurrency.Text = "INR"
        Me.lblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 2
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.74877!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.25123!))
        Me.TableLayoutPanel7.Controls.Add(Me.lblAmtInWordValue, 1, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.lblAmountInWord, 0, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(0, 326)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 1
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(812, 49)
        Me.TableLayoutPanel7.TabIndex = 6
        '
        'lblAmtInWordValue
        '
        Me.lblAmtInWordValue.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblAmtInWordValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblAmtInWordValue.Location = New System.Drawing.Point(139, 5)
        Me.lblAmtInWordValue.Name = "lblAmtInWordValue"
        Me.lblAmtInWordValue.Size = New System.Drawing.Size(456, 38)
        Me.lblAmtInWordValue.TabIndex = 1
        Me.lblAmtInWordValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAmountInWord
        '
        Me.lblAmountInWord.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblAmountInWord.AutoSize = True
        Me.lblAmountInWord.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblAmountInWord.Location = New System.Drawing.Point(3, 16)
        Me.lblAmountInWord.Name = "lblAmountInWord"
        Me.lblAmountInWord.Size = New System.Drawing.Size(124, 17)
        Me.lblAmountInWord.TabIndex = 0
        Me.lblAmountInWord.Text = "Amount in Words :"
        Me.lblAmountInWord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 4
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.58477!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.85012!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.51351!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.0516!))
        Me.TableLayoutPanel8.Controls.Add(Me.lblPreparedBy, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.lblPrepByValue, 1, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(0, 375)
        Me.TableLayoutPanel8.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(812, 30)
        Me.TableLayoutPanel8.TabIndex = 7
        '
        'lblPreparedBy
        '
        Me.lblPreparedBy.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPreparedBy.AutoSize = True
        Me.lblPreparedBy.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblPreparedBy.Location = New System.Drawing.Point(3, 6)
        Me.lblPreparedBy.Name = "lblPreparedBy"
        Me.lblPreparedBy.Size = New System.Drawing.Size(95, 17)
        Me.lblPreparedBy.TabIndex = 0
        Me.lblPreparedBy.Text = "Prepared By :"
        Me.lblPreparedBy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPrepByValue
        '
        Me.lblPrepByValue.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPrepByValue.AutoSize = True
        Me.lblPrepByValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblPrepByValue.Location = New System.Drawing.Point(137, 6)
        Me.lblPrepByValue.Name = "lblPrepByValue"
        Me.lblPrepByValue.Size = New System.Drawing.Size(47, 17)
        Me.lblPrepByValue.TabIndex = 2
        Me.lblPrepByValue.Text = "Admin"
        Me.lblPrepByValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.ColumnCount = 5
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.50246!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.78325!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.42365!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.3202!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.84175!))
        Me.TableLayoutPanel9.Controls.Add(Me.txtRecievedBy, 3, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.lblApprovedBy, 0, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.txtApprovedBy, 1, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.lblRecievedBy, 2, 0)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(0, 405)
        Me.TableLayoutPanel9.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 1
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(812, 31)
        Me.TableLayoutPanel9.TabIndex = 8
        '
        'txtRecievedBy
        '
        Me.txtRecievedBy.AutoSize = False
        Me.txtRecievedBy.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtRecievedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRecievedBy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtRecievedBy.Location = New System.Drawing.Point(431, 3)
        Me.txtRecievedBy.MaxLength = 50
        Me.txtRecievedBy.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtRecievedBy.Name = "txtRecievedBy"
        Me.txtRecievedBy.Size = New System.Drawing.Size(159, 25)
        Me.txtRecievedBy.TabIndex = 3
        Me.txtRecievedBy.Tag = Nothing
        Me.txtRecievedBy.VerticalAlign = C1.Win.C1Input.VerticalAlignEnum.Middle
        Me.txtRecievedBy.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtRecievedBy.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblApprovedBy
        '
        Me.lblApprovedBy.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblApprovedBy.AutoSize = True
        Me.lblApprovedBy.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblApprovedBy.Location = New System.Drawing.Point(3, 7)
        Me.lblApprovedBy.Name = "lblApprovedBy"
        Me.lblApprovedBy.Size = New System.Drawing.Size(102, 17)
        Me.lblApprovedBy.TabIndex = 1
        Me.lblApprovedBy.Text = "*Approved By :"
        Me.lblApprovedBy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtApprovedBy
        '
        Me.txtApprovedBy.AutoSize = False
        Me.txtApprovedBy.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtApprovedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtApprovedBy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtApprovedBy.Location = New System.Drawing.Point(137, 3)
        Me.txtApprovedBy.MaxLength = 50
        Me.txtApprovedBy.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtApprovedBy.Name = "txtApprovedBy"
        Me.txtApprovedBy.Size = New System.Drawing.Size(179, 25)
        Me.txtApprovedBy.TabIndex = 4
        Me.txtApprovedBy.Tag = Nothing
        Me.txtApprovedBy.VerticalAlign = C1.Win.C1Input.VerticalAlignEnum.Middle
        Me.txtApprovedBy.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtApprovedBy.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblRecievedBy
        '
        Me.lblRecievedBy.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblRecievedBy.AutoSize = True
        Me.lblRecievedBy.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblRecievedBy.Location = New System.Drawing.Point(322, 7)
        Me.lblRecievedBy.Name = "lblRecievedBy"
        Me.lblRecievedBy.Size = New System.Drawing.Size(100, 17)
        Me.lblRecievedBy.TabIndex = 1
        Me.lblRecievedBy.Text = "*Received By :"
        Me.lblRecievedBy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.Controls.Add(Me.btnAddNarration)
        Me.FlowLayoutPanel2.Controls.Add(Me.btnDeleteNarration)
        Me.FlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(0, 151)
        Me.FlowLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(812, 30)
        Me.FlowLayoutPanel2.TabIndex = 9
        '
        'btnAddNarration
        '
        Me.btnAddNarration.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnAddNarration.Location = New System.Drawing.Point(3, 3)
        Me.btnAddNarration.Name = "btnAddNarration"
        Me.btnAddNarration.SetArticleCode = Nothing
        Me.btnAddNarration.SetRowIndex = 0
        Me.btnAddNarration.Size = New System.Drawing.Size(124, 23)
        Me.btnAddNarration.TabIndex = 0
        Me.btnAddNarration.Text = "Add Narration"
        Me.btnAddNarration.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAddNarration.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnAddNarration.UseVisualStyleBackColor = True
        Me.btnAddNarration.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnDeleteNarration
        '
        Me.btnDeleteNarration.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDeleteNarration.Location = New System.Drawing.Point(133, 3)
        Me.btnDeleteNarration.Name = "btnDeleteNarration"
        Me.btnDeleteNarration.SetArticleCode = Nothing
        Me.btnDeleteNarration.SetRowIndex = 0
        Me.btnDeleteNarration.Size = New System.Drawing.Size(133, 23)
        Me.btnDeleteNarration.TabIndex = 1
        Me.btnDeleteNarration.Text = "Delete Narration"
        Me.btnDeleteNarration.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDeleteNarration.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnDeleteNarration.UseVisualStyleBackColor = True
        Me.btnDeleteNarration.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'FlowLayoutPanel3
        '
        Me.FlowLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.FlowLayoutPanel3.Controls.Add(Me.btnExit)
        Me.FlowLayoutPanel3.Controls.Add(Me.btnCancel)
        Me.FlowLayoutPanel3.Controls.Add(Me.btnSave)
        Me.FlowLayoutPanel3.Location = New System.Drawing.Point(522, 471)
        Me.FlowLayoutPanel3.Name = "FlowLayoutPanel3"
        Me.FlowLayoutPanel3.Size = New System.Drawing.Size(287, 37)
        Me.FlowLayoutPanel3.TabIndex = 10
        '
        'btnExit
        '
        Me.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnExit.Location = New System.Drawing.Point(3, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.SetArticleCode = Nothing
        Me.btnExit.SetRowIndex = 0
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 0
        Me.btnExit.Text = "Exit"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnExit.UseVisualStyleBackColor = True
        Me.btnExit.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnCancel
        '
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancel.Location = New System.Drawing.Point(84, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.SetArticleCode = Nothing
        Me.btnCancel.SetRowIndex = 0
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnSave
        '
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSave.Location = New System.Drawing.Point(165, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.SetArticleCode = Nothing
        Me.btnSave.SetRowIndex = 0
        Me.btnSave.Size = New System.Drawing.Size(113, 23)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save and Print"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnSave.UseVisualStyleBackColor = True
        Me.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.TableLayoutPanel10)
        Me.Panel3.Location = New System.Drawing.Point(0, 436)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(812, 31)
        Me.Panel3.TabIndex = 11
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 5
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.52101!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.1973!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.38111!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.82462!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.dtRefDocDate, 3, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.lblRefDocDate, 2, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.lblRefDocNo, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.txtRefDocNo, 1, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 1
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(812, 31)
        Me.TableLayoutPanel10.TabIndex = 4
        '
        'dtRefDocDate
        '
        Me.dtRefDocDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.dtRefDocDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtRefDocDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage"), System.Drawing.Image)
        '
        '
        '
        Me.dtRefDocDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtRefDocDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtRefDocDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtRefDocDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtRefDocDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtRefDocDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtRefDocDate.Location = New System.Drawing.Point(428, 3)
        Me.dtRefDocDate.Name = "dtRefDocDate"
        Me.dtRefDocDate.Size = New System.Drawing.Size(158, 21)
        Me.dtRefDocDate.TabIndex = 3
        Me.dtRefDocDate.Tag = Nothing
        Me.dtRefDocDate.TrimStart = True
        Me.dtRefDocDate.VerticalAlign = C1.Win.C1Input.VerticalAlignEnum.Middle
        Me.dtRefDocDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtRefDocDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'lblRefDocDate
        '
        Me.lblRefDocDate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblRefDocDate.AutoSize = True
        Me.lblRefDocDate.Location = New System.Drawing.Point(320, 7)
        Me.lblRefDocDate.Name = "lblRefDocDate"
        Me.lblRefDocDate.Size = New System.Drawing.Size(101, 17)
        Me.lblRefDocDate.TabIndex = 1
        Me.lblRefDocDate.Text = "Ref Doc Date :"
        Me.lblRefDocDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRefDocNo
        '
        Me.lblRefDocNo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblRefDocNo.AutoSize = True
        Me.lblRefDocNo.Location = New System.Drawing.Point(3, 7)
        Me.lblRefDocNo.Name = "lblRefDocNo"
        Me.lblRefDocNo.Size = New System.Drawing.Size(93, 17)
        Me.lblRefDocNo.TabIndex = 0
        Me.lblRefDocNo.Text = "Ref Doc No. :"
        Me.lblRefDocNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRefDocNo
        '
        Me.txtRefDocNo.AutoSize = False
        Me.txtRefDocNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtRefDocNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefDocNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtRefDocNo.Location = New System.Drawing.Point(136, 3)
        Me.txtRefDocNo.MaxLength = 200
        Me.txtRefDocNo.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtRefDocNo.Name = "txtRefDocNo"
        Me.txtRefDocNo.Size = New System.Drawing.Size(178, 25)
        Me.txtRefDocNo.TabIndex = 2
        Me.txtRefDocNo.Tag = Nothing
        Me.txtRefDocNo.VerticalAlign = C1.Win.C1Input.VerticalAlignEnum.Middle
        Me.txtRefDocNo.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtRefDocNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmPCVoucherEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(812, 512)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPCVoucherEntry"
        Me.Text = "New Voucher Entry"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.txtPartyName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgNarration, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        Me.TableLayoutPanel9.ResumeLayout(False)
        Me.TableLayoutPanel9.PerformLayout()
        CType(Me.txtRecievedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtApprovedBy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel2.ResumeLayout(False)
        Me.FlowLayoutPanel3.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel10.PerformLayout()
        CType(Me.dtRefDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRefDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblVoucherType As System.Windows.Forms.Label
    Friend WithEvents cmbVoucherType As System.Windows.Forms.ComboBox
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblVcherAccType As System.Windows.Forms.Label
    Friend WithEvents cmbVcherAccType As System.Windows.Forms.ComboBox
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblPartyOption As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbnEmployee As System.Windows.Forms.RadioButton
    Friend WithEvents rbnOther As System.Windows.Forms.RadioButton
    Friend WithEvents rbnSupplier As System.Windows.Forms.RadioButton
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblPartyName As System.Windows.Forms.Label
    Friend WithEvents cmbPartyName As System.Windows.Forms.ComboBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtPartyName As Spectrum.CtrlTextBox
    Friend WithEvents dgNarration As System.Windows.Forms.DataGridView
    Friend WithEvents TableLayoutPanel6 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblTotalAmount As System.Windows.Forms.Label
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblTotalValue As System.Windows.Forms.Label
    Friend WithEvents lblCurrency As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblAmountInWord As System.Windows.Forms.Label
    Friend WithEvents lblAmtInWordValue As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel8 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtRecievedBy As Spectrum.CtrlTextBox
    Friend WithEvents lblPrepByValue As System.Windows.Forms.Label
    Friend WithEvents lblPreparedBy As System.Windows.Forms.Label
    Friend WithEvents lblRecievedBy As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel9 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtApprovedBy As Spectrum.CtrlTextBox
    Friend WithEvents lblApprovedBy As System.Windows.Forms.Label
    Friend WithEvents FlowLayoutPanel2 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnAddNarration As Spectrum.CtrlBtn
    Friend WithEvents btnDeleteNarration As Spectrum.CtrlBtn
    Friend WithEvents lblVcherDate As System.Windows.Forms.Label
    Friend WithEvents lblVcherDateValue As System.Windows.Forms.Label
    Friend WithEvents lblVcherNo As System.Windows.Forms.Label
    Friend WithEvents lblVcherNoValue As System.Windows.Forms.Label
    Friend WithEvents FlowLayoutPanel3 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnExit As Spectrum.CtrlBtn
    Friend WithEvents btnCancel As Spectrum.CtrlBtn
    Friend WithEvents btnSave As Spectrum.CtrlBtn
    Friend WithEvents IsSelected As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents LineNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Narration As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel10 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents dtRefDocDate As Spectrum.ctrlDate
    Friend WithEvents lblRefDocDate As System.Windows.Forms.Label
    Friend WithEvents txtRefDocNo As Spectrum.CtrlTextBox
    Friend WithEvents lblRefDocNo As System.Windows.Forms.Label
End Class
