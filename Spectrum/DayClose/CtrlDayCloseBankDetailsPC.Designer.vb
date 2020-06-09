<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrlDayCloseBankDetailsPC
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GridCombine = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.dgOtherTenderDetail = New System.Windows.Forms.DataGridView()
        Me.SrNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tender = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReferenceNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Amt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WorkingOn = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.dgChequeDetails = New System.Windows.Forms.DataGridView()
        Me.SrNo1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BankName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ChequeNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblTotalChequeAmt = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblAmtGoingToBank = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblActualTotal = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgCashDetails = New System.Windows.Forms.DataGridView()
        Me.CurrencyCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DenominationAmt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Quantity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EnteredQuantity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalAmt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblTotalQuantity = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.btnDayCloseSalesReport = New System.Windows.Forms.Button()
        Me.btnBankReport = New System.Windows.Forms.Button()
        Me.btnDayClose = New System.Windows.Forms.Button()
        Me.LblImprestCashAmt = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GridCombine.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        CType(Me.dgOtherTenderDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.WorkingOn.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.dgChequeDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgCashDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GridCombine, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel4, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel5, 0, 3)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.22078!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.519481!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(969, 616)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'GridCombine
        '
        Me.GridCombine.ColumnCount = 2
        Me.GridCombine.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.97508!))
        Me.GridCombine.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.02492!))
        Me.GridCombine.Controls.Add(Me.GroupBox4, 1, 0)
        Me.GridCombine.Controls.Add(Me.WorkingOn, 0, 0)
        Me.GridCombine.Location = New System.Drawing.Point(3, 367)
        Me.GridCombine.Name = "GridCombine"
        Me.GridCombine.RowCount = 1
        Me.GridCombine.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.GridCombine.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 174.0!))
        Me.GridCombine.Size = New System.Drawing.Size(963, 174)
        Me.GridCombine.TabIndex = 4
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.TableLayoutPanel5)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox4.Location = New System.Drawing.Point(465, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(495, 168)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Other Tender Details"
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.Panel6, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.dgOtherTenderDetail, 0, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(3, 16)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 2
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(489, 149)
        Me.TableLayoutPanel5.TabIndex = 2
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.LightGray
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(0, 119)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(489, 30)
        Me.Panel6.TabIndex = 100
        '
        'dgOtherTenderDetail
        '
        Me.dgOtherTenderDetail.AllowUserToAddRows = False
        Me.dgOtherTenderDetail.AllowUserToDeleteRows = False
        Me.dgOtherTenderDetail.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(6, 0, 0, 0)
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgOtherTenderDetail.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgOtherTenderDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgOtherTenderDetail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SrNo, Me.Tender, Me.ReferenceNo, Me.Amt})
        Me.dgOtherTenderDetail.Location = New System.Drawing.Point(3, 3)
        Me.dgOtherTenderDetail.Name = "dgOtherTenderDetail"
        Me.dgOtherTenderDetail.RowHeadersVisible = False
        Me.dgOtherTenderDetail.Size = New System.Drawing.Size(483, 112)
        Me.dgOtherTenderDetail.TabIndex = 99
        '
        'SrNo
        '
        Me.SrNo.DataPropertyName = "SrNo"
        Me.SrNo.HeaderText = "  Sr No."
        Me.SrNo.Name = "SrNo"
        Me.SrNo.ReadOnly = True
        '
        'Tender
        '
        Me.Tender.DataPropertyName = "Tender"
        Me.Tender.HeaderText = "Tender"
        Me.Tender.Name = "Tender"
        Me.Tender.ReadOnly = True
        Me.Tender.Width = 129
        '
        'ReferenceNo
        '
        Me.ReferenceNo.DataPropertyName = "ReferenceNo"
        Me.ReferenceNo.HeaderText = "Reference No"
        Me.ReferenceNo.Name = "ReferenceNo"
        Me.ReferenceNo.ReadOnly = True
        Me.ReferenceNo.Width = 150
        '
        'Amt
        '
        Me.Amt.DataPropertyName = "Amt"
        Me.Amt.HeaderText = "Amt"
        Me.Amt.Name = "Amt"
        '
        'WorkingOn
        '
        Me.WorkingOn.Controls.Add(Me.TableLayoutPanel3)
        Me.WorkingOn.Location = New System.Drawing.Point(3, 3)
        Me.WorkingOn.Name = "WorkingOn"
        Me.WorkingOn.Size = New System.Drawing.Size(456, 166)
        Me.WorkingOn.TabIndex = 0
        Me.WorkingOn.TabStop = False
        Me.WorkingOn.Text = "Cheque Details"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.dgChequeDetails, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Panel3, 0, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 16)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(450, 147)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'dgChequeDetails
        '
        Me.dgChequeDetails.AllowUserToAddRows = False
        Me.dgChequeDetails.AllowUserToDeleteRows = False
        Me.dgChequeDetails.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.dgChequeDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgChequeDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SrNo1, Me.BankName, Me.ChequeNumber, Me.Amount})
        Me.dgChequeDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgChequeDetails.Location = New System.Drawing.Point(3, 3)
        Me.dgChequeDetails.Name = "dgChequeDetails"
        Me.dgChequeDetails.RowHeadersVisible = False
        Me.dgChequeDetails.Size = New System.Drawing.Size(444, 111)
        Me.dgChequeDetails.TabIndex = 0
        '
        'SrNo1
        '
        Me.SrNo1.DataPropertyName = "SrNo1"
        Me.SrNo1.HeaderText = "Sr No."
        Me.SrNo1.Name = "SrNo1"
        Me.SrNo1.ReadOnly = True
        Me.SrNo1.Width = 60
        '
        'BankName
        '
        Me.BankName.DataPropertyName = "BankName"
        Me.BankName.HeaderText = "Bank Name"
        Me.BankName.Name = "BankName"
        Me.BankName.ReadOnly = True
        Me.BankName.Width = 140
        '
        'ChequeNumber
        '
        Me.ChequeNumber.DataPropertyName = "ChequeNumber"
        Me.ChequeNumber.HeaderText = "Cheque No"
        Me.ChequeNumber.Name = "ChequeNumber"
        Me.ChequeNumber.ReadOnly = True
        Me.ChequeNumber.Width = 140
        '
        'Amount
        '
        Me.Amount.DataPropertyName = "Amount"
        Me.Amount.HeaderText = "Amount"
        Me.Amount.Name = "Amount"
        Me.Amount.ReadOnly = True
        Me.Amount.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.LightGray
        Me.Panel3.Controls.Add(Me.lblTotalChequeAmt)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 117)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(450, 30)
        Me.Panel3.TabIndex = 1
        '
        'lblTotalChequeAmt
        '
        Me.lblTotalChequeAmt.AutoSize = True
        Me.lblTotalChequeAmt.Location = New System.Drawing.Point(206, 7)
        Me.lblTotalChequeAmt.Name = "lblTotalChequeAmt"
        Me.lblTotalChequeAmt.Size = New System.Drawing.Size(13, 13)
        Me.lblTotalChequeAmt.TabIndex = 3
        Me.lblTotalChequeAmt.Text = "0"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(153, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Total : "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Panel4.Controls.Add(Me.lblAmtGoingToBank)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Panel4.Location = New System.Drawing.Point(3, 547)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(963, 28)
        Me.Panel4.TabIndex = 2
        '
        'lblAmtGoingToBank
        '
        Me.lblAmtGoingToBank.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblAmtGoingToBank.AutoSize = True
        Me.lblAmtGoingToBank.Location = New System.Drawing.Point(156, 6)
        Me.lblAmtGoingToBank.Name = "lblAmtGoingToBank"
        Me.lblAmtGoingToBank.Size = New System.Drawing.Size(15, 16)
        Me.lblAmtGoingToBank.TabIndex = 3
        Me.lblAmtGoingToBank.Text = "0"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(152, 16)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Amount Going To Bank :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel2)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(963, 358)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Cash Details"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.dgCashDetails, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Panel2, 0, 2)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 17)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.43195!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.579882!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(957, 338)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Panel1.AutoSize = True
        Me.Panel1.Controls.Add(Me.lblActualTotal)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(3, 2)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(106, 23)
        Me.Panel1.TabIndex = 0
        '
        'lblActualTotal
        '
        Me.lblActualTotal.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblActualTotal.AutoSize = True
        Me.lblActualTotal.Location = New System.Drawing.Point(88, 4)
        Me.lblActualTotal.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.lblActualTotal.Name = "lblActualTotal"
        Me.lblActualTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblActualTotal.Size = New System.Drawing.Size(15, 16)
        Me.lblActualTotal.TabIndex = 1
        Me.lblActualTotal.Text = "0"
        Me.lblActualTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 4)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Actual Total : "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgCashDetails
        '
        Me.dgCashDetails.AllowUserToAddRows = False
        Me.dgCashDetails.AllowUserToDeleteRows = False
        Me.dgCashDetails.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.dgCashDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgCashDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CurrencyCode, Me.DenominationAmt, Me.Quantity, Me.EnteredQuantity, Me.TotalAmt})
        Me.dgCashDetails.Location = New System.Drawing.Point(3, 30)
        Me.dgCashDetails.Name = "dgCashDetails"
        Me.dgCashDetails.RowHeadersVisible = False
        Me.dgCashDetails.Size = New System.Drawing.Size(951, 275)
        Me.dgCashDetails.TabIndex = 1
        '
        'CurrencyCode
        '
        Me.CurrencyCode.DataPropertyName = "CurrencyCode"
        Me.CurrencyCode.HeaderText = "Currency Code"
        Me.CurrencyCode.Name = "CurrencyCode"
        Me.CurrencyCode.ReadOnly = True
        Me.CurrencyCode.Width = 130
        '
        'DenominationAmt
        '
        Me.DenominationAmt.DataPropertyName = "DenominationAmt"
        Me.DenominationAmt.HeaderText = "Denomination Amt"
        Me.DenominationAmt.Name = "DenominationAmt"
        Me.DenominationAmt.ReadOnly = True
        Me.DenominationAmt.Width = 150
        '
        'Quantity
        '
        Me.Quantity.DataPropertyName = "Quantity"
        Me.Quantity.HeaderText = "Quantity"
        Me.Quantity.Name = "Quantity"
        Me.Quantity.ReadOnly = True
        Me.Quantity.Width = 120
        '
        'EnteredQuantity
        '
        Me.EnteredQuantity.DataPropertyName = "EnteredQuantity"
        Me.EnteredQuantity.HeaderText = "Entered Qty"
        Me.EnteredQuantity.Name = "EnteredQuantity"
        Me.EnteredQuantity.Width = 120
        '
        'TotalAmt
        '
        Me.TotalAmt.DataPropertyName = "TotalAmt"
        Me.TotalAmt.HeaderText = "TotalAmount"
        Me.TotalAmt.Name = "TotalAmt"
        Me.TotalAmt.ReadOnly = True
        Me.TotalAmt.Width = 130
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.LightGray
        Me.Panel2.Controls.Add(Me.lblTotalQuantity)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 308)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(957, 30)
        Me.Panel2.TabIndex = 2
        '
        'lblTotalQuantity
        '
        Me.lblTotalQuantity.AutoSize = True
        Me.lblTotalQuantity.Location = New System.Drawing.Point(405, 7)
        Me.lblTotalQuantity.Name = "lblTotalQuantity"
        Me.lblTotalQuantity.Size = New System.Drawing.Size(15, 16)
        Me.lblTotalQuantity.TabIndex = 1
        Me.lblTotalQuantity.Text = "0"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(226, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(181, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Total User Entered Quantity : "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel5
        '
        Me.Panel5.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Panel5.Controls.Add(Me.LblImprestCashAmt)
        Me.Panel5.Controls.Add(Me.btnDayCloseSalesReport)
        Me.Panel5.Controls.Add(Me.Label6)
        Me.Panel5.Controls.Add(Me.btnBankReport)
        Me.Panel5.Controls.Add(Me.btnDayClose)
        Me.Panel5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.Panel5.Location = New System.Drawing.Point(3, 578)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(963, 38)
        Me.Panel5.TabIndex = 3
        '
        'btnDayCloseSalesReport
        '
        Me.btnDayCloseSalesReport.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnDayCloseSalesReport.Enabled = False
        Me.btnDayCloseSalesReport.Location = New System.Drawing.Point(775, 7)
        Me.btnDayCloseSalesReport.Name = "btnDayCloseSalesReport"
        Me.btnDayCloseSalesReport.Size = New System.Drawing.Size(185, 27)
        Me.btnDayCloseSalesReport.TabIndex = 3
        Me.btnDayCloseSalesReport.Text = "Day Close Sales Report"
        Me.btnDayCloseSalesReport.UseVisualStyleBackColor = True
        '
        'btnBankReport
        '
        Me.btnBankReport.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnBankReport.Enabled = False
        Me.btnBankReport.Location = New System.Drawing.Point(667, 7)
        Me.btnBankReport.Name = "btnBankReport"
        Me.btnBankReport.Size = New System.Drawing.Size(102, 27)
        Me.btnBankReport.TabIndex = 2
        Me.btnBankReport.Text = "Bank Report"
        Me.btnBankReport.UseVisualStyleBackColor = True
        '
        'btnDayClose
        '
        Me.btnDayClose.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnDayClose.Location = New System.Drawing.Point(571, 7)
        Me.btnDayClose.Name = "btnDayClose"
        Me.btnDayClose.Size = New System.Drawing.Size(88, 27)
        Me.btnDayClose.TabIndex = 0
        Me.btnDayClose.Text = "Day Close"
        Me.btnDayClose.UseVisualStyleBackColor = True
        '
        'LblImprestCashAmt
        '
        Me.LblImprestCashAmt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblImprestCashAmt.AutoSize = True
        Me.LblImprestCashAmt.Location = New System.Drawing.Point(159, 11)
        Me.LblImprestCashAmt.Name = "LblImprestCashAmt"
        Me.LblImprestCashAmt.Size = New System.Drawing.Size(15, 16)
        Me.LblImprestCashAmt.TabIndex = 5
        Me.LblImprestCashAmt.Text = "0"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 16)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Imprest Cash:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CtrlDayCloseBankDetailsPC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "CtrlDayCloseBankDetailsPC"
        Me.Size = New System.Drawing.Size(969, 616)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GridCombine.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        CType(Me.dgOtherTenderDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.WorkingOn.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        CType(Me.dgChequeDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgCashDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblActualTotal As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgCashDetails As System.Windows.Forms.DataGridView
    Friend WithEvents CurrencyCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DenominationAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Quantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EnteredQuantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotalAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblTotalQuantity As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblAmtGoingToBank As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents btnDayCloseSalesReport As System.Windows.Forms.Button
    Friend WithEvents btnBankReport As System.Windows.Forms.Button
    Friend WithEvents btnDayClose As System.Windows.Forms.Button
    Friend WithEvents GridCombine As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents dgOtherTenderDetail As System.Windows.Forms.DataGridView
    Friend WithEvents SrNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tender As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReferenceNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Amt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WorkingOn As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblTotalChequeAmt As System.Windows.Forms.Label
    Friend WithEvents dgChequeDetails As System.Windows.Forms.DataGridView
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents SrNo1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BankName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChequeNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LblImprestCashAmt As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
