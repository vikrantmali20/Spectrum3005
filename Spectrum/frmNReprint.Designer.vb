<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNReprint
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNReprint))
        Me.grdInvoiceInfo = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.C1Sizer1 = New C1.Win.C1Sizer.C1Sizer()
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        Me.lblCustTypeValue = New Spectrum.CtrlLabel()
        Me.lblCustNo = New Spectrum.CtrlLabel()
        Me.lblCustNoValue = New Spectrum.CtrlLabel()
        Me.lblEmailIdValue = New Spectrum.CtrlLabel()
        Me.lblCustName = New Spectrum.CtrlLabel()
        Me.lblCustNameValue = New Spectrum.CtrlLabel()
        Me.lblEmailId = New Spectrum.CtrlLabel()
        Me.lblTelNo = New Spectrum.CtrlLabel()
        Me.lblAddress = New Spectrum.CtrlLabel()
        Me.lblTelNoValue = New Spectrum.CtrlLabel()
        Me.lblAddressValue = New Spectrum.CtrlLabel()
        Me.CtrlLabel3 = New Spectrum.CtrlLabel()
        Me.CtrlLabel4 = New Spectrum.CtrlLabel()
        Me.txtDocNumber = New Spectrum.CtrlTextBox()
        Me.BtnSearchInvoice = New Spectrum.CtrlBtn()
        Me.BtnReprintInvoice = New Spectrum.CtrlBtn()
        Me.CboInvoiceNo = New System.Windows.Forms.ComboBox()
        Me.CtrlLabel2 = New Spectrum.CtrlLabel()
        Me.txtReprintReason = New Spectrum.CtrlTextBox()
        CType(Me.grdInvoiceInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer1.SuspendLayout()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustTypeValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustNoValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmailIdValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCustNameValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmailId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTelNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTelNoValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAddressValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReprintReason, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.grdInvoiceInfo.Location = New System.Drawing.Point(9, 159)
        Me.grdInvoiceInfo.Name = "grdInvoiceInfo"
        Me.grdInvoiceInfo.NewRowWatermark = ""
        Me.grdInvoiceInfo.Rows.Count = 1
        Me.grdInvoiceInfo.Rows.DefaultSize = 20
        Me.grdInvoiceInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.grdInvoiceInfo.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.grdInvoiceInfo.Size = New System.Drawing.Size(773, 357)
        Me.grdInvoiceInfo.StyleInfo = resources.GetString("grdInvoiceInfo.StyleInfo")
        Me.grdInvoiceInfo.TabIndex = 5
        Me.grdInvoiceInfo.TabStop = False
        Me.grdInvoiceInfo.Tag = ""
        '
        'C1Sizer1
        '
        Me.C1Sizer1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C1Sizer1.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1Sizer1.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1Sizer1.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1Sizer1.Controls.Add(Me.CtrlLabel1)
        Me.C1Sizer1.Controls.Add(Me.lblCustTypeValue)
        Me.C1Sizer1.Controls.Add(Me.lblCustNo)
        Me.C1Sizer1.Controls.Add(Me.lblCustNoValue)
        Me.C1Sizer1.Controls.Add(Me.lblEmailIdValue)
        Me.C1Sizer1.Controls.Add(Me.lblCustName)
        Me.C1Sizer1.Controls.Add(Me.lblCustNameValue)
        Me.C1Sizer1.Controls.Add(Me.lblEmailId)
        Me.C1Sizer1.Controls.Add(Me.lblTelNo)
        Me.C1Sizer1.Controls.Add(Me.lblAddress)
        Me.C1Sizer1.Controls.Add(Me.lblTelNoValue)
        Me.C1Sizer1.Controls.Add(Me.lblAddressValue)
        Me.C1Sizer1.GridDefinition = resources.GetString("C1Sizer1.GridDefinition")
        Me.C1Sizer1.Location = New System.Drawing.Point(317, 9)
        Me.C1Sizer1.Margin = New System.Windows.Forms.Padding(0)
        Me.C1Sizer1.Name = "C1Sizer1"
        Me.C1Sizer1.Padding = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.C1Sizer1.Size = New System.Drawing.Size(460, 138)
        Me.C1Sizer1.SplitterWidth = 0
        Me.C1Sizer1.TabIndex = 9
        Me.C1Sizer1.TabStop = False
        Me.C1Sizer1.Tag = "v"
        Me.C1Sizer1.Text = "C1Sizer2"
        '
        'CtrlLabel1
        '
        Me.CtrlLabel1.AttachedTextBoxName = Nothing
        Me.CtrlLabel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel1.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel1.Location = New System.Drawing.Point(6, 5)
        Me.CtrlLabel1.Name = "CtrlLabel1"
        Me.CtrlLabel1.Size = New System.Drawing.Size(148, 22)
        Me.CtrlLabel1.TabIndex = 62
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.Text = "Cust Type"
        Me.CtrlLabel1.TextDetached = True
        '
        'lblCustTypeValue
        '
        Me.lblCustTypeValue.AttachedTextBoxName = Nothing
        Me.lblCustTypeValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblCustTypeValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustTypeValue.ForeColor = System.Drawing.Color.Black
        Me.lblCustTypeValue.Location = New System.Drawing.Point(154, 5)
        Me.lblCustTypeValue.Name = "lblCustTypeValue"
        Me.lblCustTypeValue.Size = New System.Drawing.Size(300, 22)
        Me.lblCustTypeValue.TabIndex = 60
        Me.lblCustTypeValue.Tag = Nothing
        Me.lblCustTypeValue.Text = "Customer Type"
        Me.lblCustTypeValue.TextDetached = True
        Me.lblCustTypeValue.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.lblCustTypeValue.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCustNo
        '
        Me.lblCustNo.AttachedTextBoxName = Nothing
        Me.lblCustNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblCustNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNo.ForeColor = System.Drawing.Color.Black
        Me.lblCustNo.Location = New System.Drawing.Point(6, 27)
        Me.lblCustNo.Name = "lblCustNo"
        Me.lblCustNo.Size = New System.Drawing.Size(148, 22)
        Me.lblCustNo.TabIndex = 58
        Me.lblCustNo.Tag = Nothing
        Me.lblCustNo.Text = "Cust No."
        Me.lblCustNo.TextDetached = True
        '
        'lblCustNoValue
        '
        Me.lblCustNoValue.AttachedTextBoxName = Nothing
        Me.lblCustNoValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblCustNoValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNoValue.ForeColor = System.Drawing.Color.Black
        Me.lblCustNoValue.Location = New System.Drawing.Point(154, 27)
        Me.lblCustNoValue.Name = "lblCustNoValue"
        Me.lblCustNoValue.Size = New System.Drawing.Size(300, 22)
        Me.lblCustNoValue.TabIndex = 57
        Me.lblCustNoValue.Tag = Nothing
        Me.lblCustNoValue.Text = "Cust No."
        Me.lblCustNoValue.TextDetached = True
        Me.lblCustNoValue.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.lblCustNoValue.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblEmailIdValue
        '
        Me.lblEmailIdValue.AttachedTextBoxName = Nothing
        Me.lblEmailIdValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblEmailIdValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEmailIdValue.ForeColor = System.Drawing.Color.Black
        Me.lblEmailIdValue.Location = New System.Drawing.Point(154, 93)
        Me.lblEmailIdValue.Name = "lblEmailIdValue"
        Me.lblEmailIdValue.Size = New System.Drawing.Size(300, 22)
        Me.lblEmailIdValue.TabIndex = 56
        Me.lblEmailIdValue.Tag = Nothing
        Me.lblEmailIdValue.Text = "Emails"
        Me.lblEmailIdValue.TextDetached = True
        Me.lblEmailIdValue.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.lblEmailIdValue.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCustName
        '
        Me.lblCustName.AttachedTextBoxName = Nothing
        Me.lblCustName.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblCustName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustName.ForeColor = System.Drawing.Color.Black
        Me.lblCustName.Location = New System.Drawing.Point(6, 49)
        Me.lblCustName.Name = "lblCustName"
        Me.lblCustName.Size = New System.Drawing.Size(148, 22)
        Me.lblCustName.TabIndex = 52
        Me.lblCustName.Tag = Nothing
        Me.lblCustName.Text = "Name"
        Me.lblCustName.TextDetached = True
        '
        'lblCustNameValue
        '
        Me.lblCustNameValue.AttachedTextBoxName = Nothing
        Me.lblCustNameValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblCustNameValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustNameValue.ForeColor = System.Drawing.Color.Black
        Me.lblCustNameValue.Location = New System.Drawing.Point(154, 49)
        Me.lblCustNameValue.Name = "lblCustNameValue"
        Me.lblCustNameValue.Size = New System.Drawing.Size(300, 22)
        Me.lblCustNameValue.TabIndex = 51
        Me.lblCustNameValue.Tag = Nothing
        Me.lblCustNameValue.Text = "Name"
        Me.lblCustNameValue.TextDetached = True
        Me.lblCustNameValue.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.lblCustNameValue.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblEmailId
        '
        Me.lblEmailId.AttachedTextBoxName = Nothing
        Me.lblEmailId.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblEmailId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEmailId.ForeColor = System.Drawing.Color.Black
        Me.lblEmailId.Location = New System.Drawing.Point(6, 93)
        Me.lblEmailId.Name = "lblEmailId"
        Me.lblEmailId.Size = New System.Drawing.Size(148, 22)
        Me.lblEmailId.TabIndex = 47
        Me.lblEmailId.Tag = Nothing
        Me.lblEmailId.Text = "Emailid"
        Me.lblEmailId.TextDetached = True
        '
        'lblTelNo
        '
        Me.lblTelNo.AttachedTextBoxName = Nothing
        Me.lblTelNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblTelNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTelNo.ForeColor = System.Drawing.Color.Black
        Me.lblTelNo.Location = New System.Drawing.Point(6, 115)
        Me.lblTelNo.Name = "lblTelNo"
        Me.lblTelNo.Size = New System.Drawing.Size(148, 22)
        Me.lblTelNo.TabIndex = 44
        Me.lblTelNo.Tag = Nothing
        Me.lblTelNo.Text = "Tel. No"
        Me.lblTelNo.TextDetached = True
        '
        'lblAddress
        '
        Me.lblAddress.AttachedTextBoxName = Nothing
        Me.lblAddress.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAddress.ForeColor = System.Drawing.Color.Black
        Me.lblAddress.Location = New System.Drawing.Point(6, 71)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(148, 22)
        Me.lblAddress.TabIndex = 42
        Me.lblAddress.Tag = Nothing
        Me.lblAddress.Text = "Address"
        Me.lblAddress.TextDetached = True
        '
        'lblTelNoValue
        '
        Me.lblTelNoValue.AttachedTextBoxName = Nothing
        Me.lblTelNoValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblTelNoValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTelNoValue.ForeColor = System.Drawing.Color.Black
        Me.lblTelNoValue.Location = New System.Drawing.Point(154, 115)
        Me.lblTelNoValue.Name = "lblTelNoValue"
        Me.lblTelNoValue.Size = New System.Drawing.Size(300, 22)
        Me.lblTelNoValue.TabIndex = 35
        Me.lblTelNoValue.Tag = Nothing
        Me.lblTelNoValue.Text = "Telephone No."
        Me.lblTelNoValue.TextDetached = True
        Me.lblTelNoValue.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.lblTelNoValue.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblAddressValue
        '
        Me.lblAddressValue.AttachedTextBoxName = Nothing
        Me.lblAddressValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblAddressValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAddressValue.ForeColor = System.Drawing.Color.Black
        Me.lblAddressValue.Location = New System.Drawing.Point(154, 71)
        Me.lblAddressValue.Name = "lblAddressValue"
        Me.lblAddressValue.Size = New System.Drawing.Size(300, 22)
        Me.lblAddressValue.TabIndex = 32
        Me.lblAddressValue.Tag = Nothing
        Me.lblAddressValue.Text = "Address"
        Me.lblAddressValue.TextDetached = True
        Me.lblAddressValue.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.lblAddressValue.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
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
        'CtrlLabel4
        '
        Me.CtrlLabel4.AttachedTextBoxName = Nothing
        Me.CtrlLabel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel4.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel4.Location = New System.Drawing.Point(15, 48)
        Me.CtrlLabel4.Name = "CtrlLabel4"
        Me.CtrlLabel4.Size = New System.Drawing.Size(100, 22)
        Me.CtrlLabel4.TabIndex = 7
        Me.CtrlLabel4.Tag = Nothing
        Me.CtrlLabel4.Text = "Invoice No."
        Me.CtrlLabel4.TextDetached = True
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
        Me.txtDocNumber.Name = "txtDocNumber"
        Me.txtDocNumber.Size = New System.Drawing.Size(146, 21)
        Me.txtDocNumber.TabIndex = 0
        Me.txtDocNumber.Tag = Nothing
        Me.txtDocNumber.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtDocNumber.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnSearchInvoice
        '
        Me.BtnSearchInvoice.Image = Global.Spectrum.My.Resources.Resources.search_2
        Me.BtnSearchInvoice.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnSearchInvoice.Location = New System.Drawing.Point(267, 14)
        Me.BtnSearchInvoice.Margin = New System.Windows.Forms.Padding(0)
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
        'BtnReprintInvoice
        '
        Me.BtnReprintInvoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnReprintInvoice.Image = Global.Spectrum.My.Resources.Resources.search_2
        Me.BtnReprintInvoice.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnReprintInvoice.Location = New System.Drawing.Point(382, 533)
        Me.BtnReprintInvoice.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnReprintInvoice.Name = "BtnReprintInvoice"
        Me.BtnReprintInvoice.SetArticleCode = Nothing
        Me.BtnReprintInvoice.SetRowIndex = 0
        Me.BtnReprintInvoice.Size = New System.Drawing.Size(128, 24)
        Me.BtnReprintInvoice.TabIndex = 4
        Me.BtnReprintInvoice.Text = "&Reprint"
        Me.BtnReprintInvoice.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnReprintInvoice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnReprintInvoice.UseVisualStyleBackColor = True
        Me.BtnReprintInvoice.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CboInvoiceNo
        '
        Me.CboInvoiceNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboInvoiceNo.FormattingEnabled = True
        Me.CboInvoiceNo.Location = New System.Drawing.Point(122, 49)
        Me.CboInvoiceNo.Name = "CboInvoiceNo"
        Me.CboInvoiceNo.Size = New System.Drawing.Size(192, 21)
        Me.CboInvoiceNo.TabIndex = 2
        '
        'CtrlLabel2
        '
        Me.CtrlLabel2.AttachedTextBoxName = Nothing
        Me.CtrlLabel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel2.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel2.Location = New System.Drawing.Point(15, 82)
        Me.CtrlLabel2.Name = "CtrlLabel2"
        Me.CtrlLabel2.Size = New System.Drawing.Size(100, 22)
        Me.CtrlLabel2.TabIndex = 8
        Me.CtrlLabel2.Tag = Nothing
        Me.CtrlLabel2.Text = "Re-print Reason"
        Me.CtrlLabel2.TextDetached = True
        '
        'txtReprintReason
        '
        Me.txtReprintReason.AutoSize = False
        Me.txtReprintReason.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtReprintReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReprintReason.DateTimeInput = False
        Me.txtReprintReason.EmptyAsNull = True
        Me.txtReprintReason.ErrorInfo.ErrorMessage = "only numeric data required"
        Me.txtReprintReason.Location = New System.Drawing.Point(122, 82)
        Me.txtReprintReason.MaximumSize = New System.Drawing.Size(233, 21)
        Me.txtReprintReason.MaxLength = 300
        Me.txtReprintReason.MinimumSize = New System.Drawing.Size(12, 50)
        Me.txtReprintReason.Multiline = True
        Me.txtReprintReason.Name = "txtReprintReason"
        Me.txtReprintReason.Size = New System.Drawing.Size(192, 50)
        Me.txtReprintReason.TabIndex = 3
        Me.txtReprintReason.Tag = Nothing
        Me.txtReprintReason.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtReprintReason.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmNReprint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.txtReprintReason)
        Me.Controls.Add(Me.CtrlLabel2)
        Me.Controls.Add(Me.CboInvoiceNo)
        Me.Controls.Add(Me.BtnReprintInvoice)
        Me.Controls.Add(Me.BtnSearchInvoice)
        Me.Controls.Add(Me.txtDocNumber)
        Me.Controls.Add(Me.CtrlLabel4)
        Me.Controls.Add(Me.CtrlLabel3)
        Me.Controls.Add(Me.C1Sizer1)
        Me.Controls.Add(Me.grdInvoiceInfo)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "frmNReprint"
        Me.Text = "Reprint Bill"
        CType(Me.grdInvoiceInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer1.ResumeLayout(False)
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustTypeValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustNoValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmailIdValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCustNameValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmailId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTelNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTelNoValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAddressValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReprintReason, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdInvoiceInfo As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents lblCustTypeValue As Spectrum.CtrlLabel
    Friend WithEvents lblCustNo As Spectrum.CtrlLabel
    Friend WithEvents lblCustNoValue As Spectrum.CtrlLabel
    Friend WithEvents lblEmailIdValue As Spectrum.CtrlLabel
    Friend WithEvents lblCustName As Spectrum.CtrlLabel
    Friend WithEvents lblCustNameValue As Spectrum.CtrlLabel
    Friend WithEvents lblEmailId As Spectrum.CtrlLabel
    Friend WithEvents lblTelNo As Spectrum.CtrlLabel
    Friend WithEvents lblAddress As Spectrum.CtrlLabel
    Friend WithEvents lblTelNoValue As Spectrum.CtrlLabel
    Friend WithEvents lblAddressValue As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel1 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel3 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel4 As Spectrum.CtrlLabel
    Friend WithEvents txtDocNumber As Spectrum.CtrlTextBox
    Friend WithEvents BtnSearchInvoice As Spectrum.CtrlBtn
    Friend WithEvents BtnReprintInvoice As Spectrum.CtrlBtn
    Friend WithEvents CboInvoiceNo As System.Windows.Forms.ComboBox
    Friend WithEvents CtrlLabel2 As Spectrum.CtrlLabel
    Friend WithEvents txtReprintReason As Spectrum.CtrlTextBox
End Class
