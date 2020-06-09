<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSplitBill
    Inherits CtrlPopupForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSplitBill))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.grdInvoiceInfo = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.ChkOrignalBillRequired = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BtnAddCustomer = New Spectrum.CtrlBtn()
        Me.GrdAddCustomer = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.CashSummary = New Spectrum.CtrlCashSummary()
        Me.BtnSplitBill = New Spectrum.CtrlBtn()
        Me.BtnSearchInvoice = New Spectrum.CtrlBtn()
        Me.txtDocNumber = New Spectrum.CtrlTextBox()
        Me.CtrlLabel3 = New Spectrum.CtrlLabel()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdInvoiceInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GrdAddCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.grdInvoiceInfo)
        Me.GroupBox2.Location = New System.Drawing.Point(0, 58)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(880, 270)
        Me.GroupBox2.TabIndex = 17
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Bill Detail:"
        '
        'grdInvoiceInfo
        '
        Me.grdInvoiceInfo.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.grdInvoiceInfo.AllowEditing = False
        Me.grdInvoiceInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdInvoiceInfo.AutoGenerateColumns = False
        Me.grdInvoiceInfo.ColumnInfo = resources.GetString("grdInvoiceInfo.ColumnInfo")
        Me.grdInvoiceInfo.ExtendLastCol = True
        Me.grdInvoiceInfo.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.grdInvoiceInfo.Location = New System.Drawing.Point(6, 20)
        Me.grdInvoiceInfo.Name = "grdInvoiceInfo"
        Me.grdInvoiceInfo.NewRowWatermark = ""
        Me.grdInvoiceInfo.Rows.Count = 1
        Me.grdInvoiceInfo.Rows.DefaultSize = 20
        Me.grdInvoiceInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.grdInvoiceInfo.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.grdInvoiceInfo.Size = New System.Drawing.Size(868, 244)
        Me.grdInvoiceInfo.StyleInfo = resources.GetString("grdInvoiceInfo.StyleInfo")
        Me.grdInvoiceInfo.TabIndex = 5
        Me.grdInvoiceInfo.TabStop = False
        Me.grdInvoiceInfo.Tag = ""
        '
        'ChkOrignalBillRequired
        '
        Me.ChkOrignalBillRequired.AutoSize = True
        Me.ChkOrignalBillRequired.Location = New System.Drawing.Point(122, 58)
        Me.ChkOrignalBillRequired.Name = "ChkOrignalBillRequired"
        Me.ChkOrignalBillRequired.Size = New System.Drawing.Size(173, 17)
        Me.ChkOrignalBillRequired.TabIndex = 14
        Me.ChkOrignalBillRequired.Text = "Orignal Bill Print Required"
        Me.ChkOrignalBillRequired.UseVisualStyleBackColor = True
        Me.ChkOrignalBillRequired.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BtnAddCustomer)
        Me.GroupBox1.Controls.Add(Me.GrdAddCustomer)
        Me.GroupBox1.Location = New System.Drawing.Point(342, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(538, 165)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Add Customer:"
        Me.GroupBox1.Visible = False
        '
        'BtnAddCustomer
        '
        Me.BtnAddCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnAddCustomer.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnAddCustomer.Location = New System.Drawing.Point(3, 17)
        Me.BtnAddCustomer.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnAddCustomer.MoveToNxtCtrl = Nothing
        Me.BtnAddCustomer.Name = "BtnAddCustomer"
        Me.BtnAddCustomer.SetArticleCode = Nothing
        Me.BtnAddCustomer.SetRowIndex = 0
        Me.BtnAddCustomer.Size = New System.Drawing.Size(128, 24)
        Me.BtnAddCustomer.TabIndex = 118
        Me.BtnAddCustomer.Text = "&Add Customer"
        Me.BtnAddCustomer.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnAddCustomer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnAddCustomer.UseVisualStyleBackColor = True
        Me.BtnAddCustomer.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'GrdAddCustomer
        '
        Me.GrdAddCustomer.AutoGenerateColumns = False
        Me.GrdAddCustomer.CellButtonImage = Global.Spectrum.My.Resources.Resources.del_icon
        Me.GrdAddCustomer.ColumnInfo = resources.GetString("GrdAddCustomer.ColumnInfo")
        Me.GrdAddCustomer.ExtendLastCol = True
        Me.GrdAddCustomer.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrdAddCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GrdAddCustomer.Location = New System.Drawing.Point(-2, 46)
        Me.GrdAddCustomer.Name = "GrdAddCustomer"
        Me.GrdAddCustomer.NewRowWatermark = ""
        Me.GrdAddCustomer.Rows.Count = 1
        Me.GrdAddCustomer.Rows.DefaultSize = 19
        Me.GrdAddCustomer.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.GrdAddCustomer.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.GrdAddCustomer.Size = New System.Drawing.Size(540, 127)
        Me.GrdAddCustomer.StyleInfo = resources.GetString("GrdAddCustomer.StyleInfo")
        Me.GrdAddCustomer.TabIndex = 117
        Me.GrdAddCustomer.Tag = ""
        '
        'CashSummary
        '
        Me.CashSummary.AlignChangeForCashSummary = Nothing
        Me.CashSummary.hdrText = "Cash Memo Summary"
        Me.CashSummary.lbl1 = "Gross Amt"
        Me.CashSummary.lbl10 = "CtrlLabel10"
        Me.CashSummary.lbl10Location = New System.Drawing.Point(6, 219)
        Me.CashSummary.lbl1location = New System.Drawing.Point(6, 32)
        Me.CashSummary.lbl2 = "Tax Amt"
        Me.CashSummary.lbl2Location = New System.Drawing.Point(6, 54)
        Me.CashSummary.lbl3 = "Disc Amt"
        Me.CashSummary.lbl3Location = New System.Drawing.Point(6, 76)
        Me.CashSummary.lbl4 = "Net Amt"
        Me.CashSummary.lbl4Location = New System.Drawing.Point(6, 98)
        Me.CashSummary.lbl5 = "CtrlLabel5"
        Me.CashSummary.lbl5Location = New System.Drawing.Point(6, 120)
        Me.CashSummary.lbl6 = "CtrlLabel6"
        Me.CashSummary.lbl6Location = New System.Drawing.Point(6, 140)
        Me.CashSummary.lbl7 = "CtrlLabel7"
        Me.CashSummary.lbl7Location = New System.Drawing.Point(6, 160)
        Me.CashSummary.lbl8 = "CtrlLabel8"
        Me.CashSummary.lbl8Location = New System.Drawing.Point(6, 180)
        Me.CashSummary.lbl9 = "CtrlLabel9"
        Me.CashSummary.lbl9Location = New System.Drawing.Point(0, 0)
        Me.CashSummary.lbltxt1 = ""
        Me.CashSummary.lbltxt10 = ""
        Me.CashSummary.lbltxt10location = New System.Drawing.Point(120, 219)
        Me.CashSummary.lbltxt1location = New System.Drawing.Point(120, 32)
        Me.CashSummary.lbltxt2 = ""
        Me.CashSummary.lbltxt2location = New System.Drawing.Point(120, 54)
        Me.CashSummary.lbltxt3 = ""
        Me.CashSummary.lbltxt3location = New System.Drawing.Point(120, 76)
        Me.CashSummary.lbltxt4 = ""
        Me.CashSummary.lbltxt4location = New System.Drawing.Point(120, 98)
        Me.CashSummary.lbltxt5 = ""
        Me.CashSummary.lbltxt5location = New System.Drawing.Point(120, 120)
        Me.CashSummary.lbltxt6 = ""
        Me.CashSummary.lbltxt6location = New System.Drawing.Point(120, 140)
        Me.CashSummary.lbltxt7 = ""
        Me.CashSummary.lbltxt7location = New System.Drawing.Point(120, 160)
        Me.CashSummary.lbltxt8 = ""
        Me.CashSummary.lbltxt8location = New System.Drawing.Point(120, 180)
        Me.CashSummary.lbltxt9 = ""
        Me.CashSummary.lbltxt9location = New System.Drawing.Point(120, 199)
        Me.CashSummary.lbltxtVisible1 = True
        Me.CashSummary.lbltxtVisible10 = True
        Me.CashSummary.lbltxtVisible2 = True
        Me.CashSummary.lbltxtVisible3 = True
        Me.CashSummary.lbltxtVisible4 = True
        Me.CashSummary.lbltxtVisible5 = True
        Me.CashSummary.lbltxtVisible6 = True
        Me.CashSummary.lbltxtVisible7 = True
        Me.CashSummary.lbltxtVisible8 = True
        Me.CashSummary.lbltxtVisible9 = True
        Me.CashSummary.lblVisible1 = True
        Me.CashSummary.lblVisible10 = True
        Me.CashSummary.lblVisible2 = True
        Me.CashSummary.lblVisible3 = True
        Me.CashSummary.lblVisible4 = True
        Me.CashSummary.lblVisible5 = True
        Me.CashSummary.lblVisible6 = True
        Me.CashSummary.lblVisible7 = True
        Me.CashSummary.lblVisible8 = True
        Me.CashSummary.lblVisible9 = True
        Me.CashSummary.Location = New System.Drawing.Point(677, 328)
        Me.CashSummary.Name = "CashSummary"
        Me.CashSummary.RowCount = 11
        Me.CashSummary.Size = New System.Drawing.Size(208, 126)
        Me.CashSummary.TabIndex = 16
        '
        'BtnSplitBill
        '
        Me.BtnSplitBill.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSplitBill.Image = Global.Spectrum.My.Resources.Resources.search_2
        Me.BtnSplitBill.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnSplitBill.Location = New System.Drawing.Point(362, 469)
        Me.BtnSplitBill.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnSplitBill.MoveToNxtCtrl = Nothing
        Me.BtnSplitBill.Name = "BtnSplitBill"
        Me.BtnSplitBill.SetArticleCode = Nothing
        Me.BtnSplitBill.SetRowIndex = 0
        Me.BtnSplitBill.Size = New System.Drawing.Size(128, 24)
        Me.BtnSplitBill.TabIndex = 4
        Me.BtnSplitBill.Text = "&Split Bill"
        Me.BtnSplitBill.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnSplitBill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnSplitBill.UseVisualStyleBackColor = True
        Me.BtnSplitBill.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnSearchInvoice
        '
        Me.BtnSearchInvoice.Image = Global.Spectrum.My.Resources.Resources.search_2
        Me.BtnSearchInvoice.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnSearchInvoice.Location = New System.Drawing.Point(267, 14)
        Me.BtnSearchInvoice.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnSearchInvoice.MoveToNxtCtrl = Nothing
        Me.BtnSearchInvoice.Name = "BtnSearchInvoice"
        Me.BtnSearchInvoice.SetArticleCode = Nothing
        Me.BtnSearchInvoice.SetRowIndex = 0
        Me.BtnSearchInvoice.Size = New System.Drawing.Size(47, 21)
        Me.BtnSearchInvoice.TabIndex = 1
        Me.BtnSearchInvoice.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnSearchInvoice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnSearchInvoice.UseVisualStyleBackColor = True
        Me.BtnSearchInvoice.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtDocNumber
        '
        Me.txtDocNumber.AutoSize = False
        Me.txtDocNumber.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtDocNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDocNumber.DateTimeInput = False
        Me.txtDocNumber.EmptyAsNull = True
        Me.txtDocNumber.ErrorInfo.ErrorMessage = "only numeric data required"
        Me.txtDocNumber.Location = New System.Drawing.Point(122, 14)
        Me.txtDocNumber.MaximumSize = New System.Drawing.Size(233, 21)
        Me.txtDocNumber.MinimumSize = New System.Drawing.Size(12, 21)
        Me.txtDocNumber.MoveToNxtCtrl = Nothing
        Me.txtDocNumber.Name = "txtDocNumber"
        Me.txtDocNumber.Size = New System.Drawing.Size(146, 21)
        Me.txtDocNumber.TabIndex = 0
        Me.txtDocNumber.Tag = Nothing
        Me.txtDocNumber.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtDocNumber.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel3
        '
        Me.CtrlLabel3.AttachedTextBoxName = Nothing
        Me.CtrlLabel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel3.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel3.Location = New System.Drawing.Point(15, 13)
        Me.CtrlLabel3.Name = "CtrlLabel3"
        Me.CtrlLabel3.Size = New System.Drawing.Size(100, 22)
        Me.CtrlLabel3.TabIndex = 6
        Me.CtrlLabel3.Tag = Nothing
        Me.CtrlLabel3.Text = "Doc Number"
        Me.CtrlLabel3.TextDetached = True
        '
        'FrmSplitBill
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(892, 502)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.CashSummary)
        Me.Controls.Add(Me.ChkOrignalBillRequired)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BtnSplitBill)
        Me.Controls.Add(Me.BtnSearchInvoice)
        Me.Controls.Add(Me.txtDocNumber)
        Me.Controls.Add(Me.CtrlLabel3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(400, 200)
        Me.Name = "FrmSplitBill"
        Me.Text = "Split BIll"
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdInvoiceInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.GrdAddCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdInvoiceInfo As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents CtrlLabel3 As Spectrum.CtrlLabel
    Friend WithEvents txtDocNumber As Spectrum.CtrlTextBox
    Friend WithEvents BtnSearchInvoice As Spectrum.CtrlBtn
    Friend WithEvents BtnSplitBill As Spectrum.CtrlBtn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnAddCustomer As Spectrum.CtrlBtn
    Friend WithEvents GrdAddCustomer As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents ChkOrignalBillRequired As System.Windows.Forms.CheckBox
    Friend WithEvents CashSummary As Spectrum.CtrlCashSummary
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
End Class
