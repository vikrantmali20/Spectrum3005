<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNOutboundDelivery
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNOutboundDelivery))
        Me.cboDocType = New C1.Win.C1List.C1Combo()
        Me.LbDeliveredAmt = New Spectrum.CtrlLabel()
        Me.LbPickupAmt = New Spectrum.CtrlLabel()
        Me.lblNetAmount = New Spectrum.CtrlLabel()
        Me.LbNetAmount = New Spectrum.CtrlLabel()
        Me.lblReceivedAmount = New Spectrum.CtrlLabel()
        Me.LbReceivedAmount = New Spectrum.CtrlLabel()
        Me.BtnCancelOD = New Spectrum.CtrlBtn()
        Me.BtnOKOD = New Spectrum.CtrlBtn()
        Me.grdOrderInfo = New Spectrum.CtrlGrid()
        Me.CtrlCustDtls1 = New Spectrum.CtrlCustDtls()
        Me.BtnSearchOrder = New Spectrum.CtrlBtn()
        Me.txtOrderNo = New Spectrum.CtrlTextBox()
        Me.lblDocNo = New Spectrum.CtrlLabel()
        Me.lblDocType = New Spectrum.CtrlLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblPickupAmt = New Spectrum.CtrlLabel()
        Me.lblDeliveredAmt = New Spectrum.CtrlLabel()
        Me.lblAmountToPay = New Spectrum.CtrlLabel()
        Me.btnPay = New Spectrum.CtrlBtn()
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        Me.txtBatchBarcode = New Spectrum.CtrlTextBox()
        Me.lblBatchBarcode = New Spectrum.CtrlLabel()
        Me.CtrlTab1 = New Spectrum.CtrlTab()
        Me.C1DockingTabPage1 = New C1.Win.C1Command.C1DockingTabPage()
        CType(Me.cboDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LbDeliveredAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LbPickupAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNetAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LbNetAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReceivedAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LbReceivedAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdOrderInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.lblPickupAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDeliveredAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmountToPay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBatchBarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBatchBarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlTab1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CtrlTab1.SuspendLayout()
        Me.C1DockingTabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cboDocType
        '
        Me.cboDocType.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboDocType.AllowSort = False
        Me.cboDocType.AutoSize = False
        Me.cboDocType.Caption = ""
        Me.cboDocType.CaptionHeight = 17
        Me.cboDocType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboDocType.ColumnCaptionHeight = 17
        Me.cboDocType.ColumnFooterHeight = 17
        Me.cboDocType.ColumnHeaders = False
        Me.cboDocType.ContentHeight = 15
        Me.cboDocType.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboDocType.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboDocType.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDocType.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDocType.EditorHeight = 15
        Me.cboDocType.ExtendRightColumn = True
        Me.cboDocType.Images.Add(CType(resources.GetObject("cboDocType.Images"), System.Drawing.Image))
        Me.cboDocType.ItemHeight = 15
        Me.cboDocType.Location = New System.Drawing.Point(106, 40)
        Me.cboDocType.MatchEntryTimeout = CType(2000, Long)
        Me.cboDocType.MaxDropDownItems = CType(5, Short)
        Me.cboDocType.MaxLength = 32767
        Me.cboDocType.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboDocType.ScrollTrack = False
        Me.cboDocType.Size = New System.Drawing.Size(152, 21)
        Me.cboDocType.TabIndex = 14
        Me.cboDocType.PropBag = resources.GetString("cboDocType.PropBag")
        '
        'LbDeliveredAmt
        '
        Me.LbDeliveredAmt.AttachedTextBoxName = Nothing
        Me.LbDeliveredAmt.AutoSize = True
        Me.LbDeliveredAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.LbDeliveredAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.LbDeliveredAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LbDeliveredAmt.ForeColor = System.Drawing.Color.Black
        Me.LbDeliveredAmt.Location = New System.Drawing.Point(1, 496)
        Me.LbDeliveredAmt.Name = "LbDeliveredAmt"
        Me.LbDeliveredAmt.Size = New System.Drawing.Size(121, 15)
        Me.LbDeliveredAmt.TabIndex = 21
        Me.LbDeliveredAmt.Tag = Nothing
        Me.LbDeliveredAmt.Text = "Delivered Amount :"
        Me.LbDeliveredAmt.TextDetached = True
        Me.LbDeliveredAmt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'LbPickupAmt
        '
        Me.LbPickupAmt.AttachedTextBoxName = Nothing
        Me.LbPickupAmt.AutoSize = True
        Me.LbPickupAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.LbPickupAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.LbPickupAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LbPickupAmt.ForeColor = System.Drawing.Color.Black
        Me.LbPickupAmt.Location = New System.Drawing.Point(174, 496)
        Me.LbPickupAmt.Name = "LbPickupAmt"
        Me.LbPickupAmt.Size = New System.Drawing.Size(103, 15)
        Me.LbPickupAmt.TabIndex = 19
        Me.LbPickupAmt.Tag = Nothing
        Me.LbPickupAmt.Text = "Pickup Amount :"
        Me.LbPickupAmt.TextDetached = True
        Me.LbPickupAmt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblNetAmount
        '
        Me.lblNetAmount.AttachedTextBoxName = Nothing
        Me.lblNetAmount.AutoSize = True
        Me.lblNetAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblNetAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNetAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmount.ForeColor = System.Drawing.Color.Black
        Me.lblNetAmount.Location = New System.Drawing.Point(604, 498)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.Size = New System.Drawing.Size(27, 15)
        Me.lblNetAmount.TabIndex = 18
        Me.lblNetAmount.Tag = Nothing
        Me.lblNetAmount.Text = "0.0"
        Me.lblNetAmount.TextDetached = True
        Me.lblNetAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'LbNetAmount
        '
        Me.LbNetAmount.AttachedTextBoxName = Nothing
        Me.LbNetAmount.AutoSize = True
        Me.LbNetAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.LbNetAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.LbNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LbNetAmount.ForeColor = System.Drawing.Color.Black
        Me.LbNetAmount.Location = New System.Drawing.Point(522, 497)
        Me.LbNetAmount.Name = "LbNetAmount"
        Me.LbNetAmount.Size = New System.Drawing.Size(85, 15)
        Me.LbNetAmount.TabIndex = 17
        Me.LbNetAmount.Tag = Nothing
        Me.LbNetAmount.Text = "Net Amount :"
        Me.LbNetAmount.TextDetached = True
        Me.LbNetAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblReceivedAmount
        '
        Me.lblReceivedAmount.AttachedTextBoxName = Nothing
        Me.lblReceivedAmount.AutoSize = True
        Me.lblReceivedAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblReceivedAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblReceivedAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReceivedAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReceivedAmount.ForeColor = System.Drawing.Color.Black
        Me.lblReceivedAmount.Location = New System.Drawing.Point(445, 498)
        Me.lblReceivedAmount.Name = "lblReceivedAmount"
        Me.lblReceivedAmount.Size = New System.Drawing.Size(27, 15)
        Me.lblReceivedAmount.TabIndex = 16
        Me.lblReceivedAmount.Tag = Nothing
        Me.lblReceivedAmount.Text = "0.0"
        Me.lblReceivedAmount.TextDetached = True
        Me.lblReceivedAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'LbReceivedAmount
        '
        Me.LbReceivedAmount.AttachedTextBoxName = Nothing
        Me.LbReceivedAmount.AutoSize = True
        Me.LbReceivedAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.LbReceivedAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.LbReceivedAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LbReceivedAmount.ForeColor = System.Drawing.Color.Black
        Me.LbReceivedAmount.Location = New System.Drawing.Point(330, 497)
        Me.LbReceivedAmount.Name = "LbReceivedAmount"
        Me.LbReceivedAmount.Size = New System.Drawing.Size(118, 15)
        Me.LbReceivedAmount.TabIndex = 15
        Me.LbReceivedAmount.Tag = Nothing
        Me.LbReceivedAmount.Text = "Received Amount :"
        Me.LbReceivedAmount.TextDetached = True
        Me.LbReceivedAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnCancelOD
        '
        Me.BtnCancelOD.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCancelOD.Location = New System.Drawing.Point(465, 538)
        Me.BtnCancelOD.Name = "BtnCancelOD"
        Me.BtnCancelOD.SetArticleCode = Nothing
        Me.BtnCancelOD.SetRowIndex = 0
        Me.BtnCancelOD.Size = New System.Drawing.Size(76, 19)
        Me.BtnCancelOD.TabIndex = 12
        Me.BtnCancelOD.Text = "&Cancel"
        Me.BtnCancelOD.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnCancelOD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnCancelOD.UseVisualStyleBackColor = True
        Me.BtnCancelOD.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnOKOD
        '
        Me.BtnOKOD.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnOKOD.Location = New System.Drawing.Point(380, 538)
        Me.BtnOKOD.Name = "BtnOKOD"
        Me.BtnOKOD.SetArticleCode = Nothing
        Me.BtnOKOD.SetRowIndex = 0
        Me.BtnOKOD.Size = New System.Drawing.Size(76, 19)
        Me.BtnOKOD.TabIndex = 11
        Me.BtnOKOD.Text = "&OK"
        Me.BtnOKOD.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnOKOD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnOKOD.UseVisualStyleBackColor = True
        Me.BtnOKOD.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'grdOrderInfo
        '
        Me.grdOrderInfo.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.grdOrderInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdOrderInfo.AutoGenerateColumns = False
        Me.grdOrderInfo.CellButtonImage = CType(resources.GetObject("grdOrderInfo.CellButtonImage"), System.Drawing.Image)
        Me.grdOrderInfo.ColumnInfo = resources.GetString("grdOrderInfo.ColumnInfo")
        Me.grdOrderInfo.ExtendLastCol = True
        Me.grdOrderInfo.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.grdOrderInfo.Location = New System.Drawing.Point(0, 154)
        Me.grdOrderInfo.Name = "grdOrderInfo"
        Me.grdOrderInfo.Rows.Count = 1
        Me.grdOrderInfo.Rows.DefaultSize = 20
        Me.grdOrderInfo.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.grdOrderInfo.Size = New System.Drawing.Size(842, 334)
        Me.grdOrderInfo.StyleInfo = resources.GetString("grdOrderInfo.StyleInfo")
        Me.grdOrderInfo.TabIndex = 10
        Me.grdOrderInfo.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'CtrlCustDtls1
        '
        Me.CtrlCustDtls1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlCustDtls1.DisplayCustType = False
        Me.CtrlCustDtls1.dtCustmInfo = Nothing
        Me.CtrlCustDtls1.Location = New System.Drawing.Point(379, 6)
        Me.CtrlCustDtls1.Name = "CtrlCustDtls1"
        Me.CtrlCustDtls1.Size = New System.Drawing.Size(462, 141)
        Me.CtrlCustDtls1.TabIndex = 9
        '
        'BtnSearchOrder
        '
        Me.BtnSearchOrder.Image = Global.Spectrum.My.Resources.Resources.search_2
        Me.BtnSearchOrder.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnSearchOrder.Location = New System.Drawing.Point(259, 62)
        Me.BtnSearchOrder.Name = "BtnSearchOrder"
        Me.BtnSearchOrder.SetArticleCode = Nothing
        Me.BtnSearchOrder.SetRowIndex = 0
        Me.BtnSearchOrder.Size = New System.Drawing.Size(37, 21)
        Me.BtnSearchOrder.TabIndex = 5
        Me.BtnSearchOrder.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnSearchOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnSearchOrder.UseVisualStyleBackColor = True
        Me.BtnSearchOrder.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtOrderNo
        '
        Me.txtOrderNo.AutoSize = False
        Me.txtOrderNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrderNo.Location = New System.Drawing.Point(106, 62)
        Me.txtOrderNo.MaximumSize = New System.Drawing.Size(233, 21)
        Me.txtOrderNo.MinimumSize = New System.Drawing.Size(12, 21)
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.Size = New System.Drawing.Size(152, 21)
        Me.txtOrderNo.TabIndex = 8
        Me.txtOrderNo.Tag = Nothing
        Me.txtOrderNo.TextDetached = True
        Me.txtOrderNo.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtOrderNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblDocNo
        '
        Me.lblDocNo.AttachedTextBoxName = Nothing
        Me.lblDocNo.AutoSize = True
        Me.lblDocNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDocNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDocNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDocNo.ForeColor = System.Drawing.Color.Black
        Me.lblDocNo.Location = New System.Drawing.Point(5, 62)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(86, 15)
        Me.lblDocNo.TabIndex = 6
        Me.lblDocNo.Tag = Nothing
        Me.lblDocNo.Text = "Document No"
        Me.lblDocNo.TextDetached = True
        Me.lblDocNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblDocType
        '
        Me.lblDocType.AttachedTextBoxName = Nothing
        Me.lblDocType.AutoSize = True
        Me.lblDocType.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDocType.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDocType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDocType.ForeColor = System.Drawing.Color.Black
        Me.lblDocType.Location = New System.Drawing.Point(5, 40)
        Me.lblDocType.Name = "lblDocType"
        Me.lblDocType.Size = New System.Drawing.Size(98, 15)
        Me.lblDocType.TabIndex = 4
        Me.lblDocType.Tag = Nothing
        Me.lblDocType.Text = "Document Type"
        Me.lblDocType.TextDetached = True
        Me.lblDocType.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblPickupAmt)
        Me.Panel1.Controls.Add(Me.lblDeliveredAmt)
        Me.Panel1.Controls.Add(Me.lblAmountToPay)
        Me.Panel1.Controls.Add(Me.btnPay)
        Me.Panel1.Controls.Add(Me.CtrlLabel1)
        Me.Panel1.Controls.Add(Me.txtBatchBarcode)
        Me.Panel1.Controls.Add(Me.lblBatchBarcode)
        Me.Panel1.Controls.Add(Me.CtrlCustDtls1)
        Me.Panel1.Controls.Add(Me.BtnOKOD)
        Me.Panel1.Controls.Add(Me.lblDocType)
        Me.Panel1.Controls.Add(Me.lblDocNo)
        Me.Panel1.Controls.Add(Me.LbDeliveredAmt)
        Me.Panel1.Controls.Add(Me.txtOrderNo)
        Me.Panel1.Controls.Add(Me.BtnSearchOrder)
        Me.Panel1.Controls.Add(Me.LbPickupAmt)
        Me.Panel1.Controls.Add(Me.lblNetAmount)
        Me.Panel1.Controls.Add(Me.cboDocType)
        Me.Panel1.Controls.Add(Me.LbNetAmount)
        Me.Panel1.Controls.Add(Me.grdOrderInfo)
        Me.Panel1.Controls.Add(Me.lblReceivedAmount)
        Me.Panel1.Controls.Add(Me.BtnCancelOD)
        Me.Panel1.Controls.Add(Me.LbReceivedAmount)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(851, 576)
        Me.Panel1.TabIndex = 23
        '
        'lblPickupAmt
        '
        Me.lblPickupAmt.AttachedTextBoxName = Nothing
        Me.lblPickupAmt.AutoSize = True
        Me.lblPickupAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblPickupAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblPickupAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPickupAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPickupAmt.ForeColor = System.Drawing.Color.Black
        Me.lblPickupAmt.Location = New System.Drawing.Point(273, 498)
        Me.lblPickupAmt.Name = "lblPickupAmt"
        Me.lblPickupAmt.Size = New System.Drawing.Size(27, 15)
        Me.lblPickupAmt.TabIndex = 30
        Me.lblPickupAmt.Tag = Nothing
        Me.lblPickupAmt.Text = "0.0"
        Me.lblPickupAmt.TextDetached = True
        Me.lblPickupAmt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblDeliveredAmt
        '
        Me.lblDeliveredAmt.AttachedTextBoxName = Nothing
        Me.lblDeliveredAmt.AutoSize = True
        Me.lblDeliveredAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDeliveredAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDeliveredAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDeliveredAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeliveredAmt.ForeColor = System.Drawing.Color.Black
        Me.lblDeliveredAmt.Location = New System.Drawing.Point(117, 497)
        Me.lblDeliveredAmt.Name = "lblDeliveredAmt"
        Me.lblDeliveredAmt.Size = New System.Drawing.Size(27, 15)
        Me.lblDeliveredAmt.TabIndex = 29
        Me.lblDeliveredAmt.Tag = Nothing
        Me.lblDeliveredAmt.Text = "0.0"
        Me.lblDeliveredAmt.TextDetached = True
        Me.lblDeliveredAmt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblAmountToPay
        '
        Me.lblAmountToPay.AttachedTextBoxName = Nothing
        Me.lblAmountToPay.AutoSize = True
        Me.lblAmountToPay.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblAmountToPay.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblAmountToPay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAmountToPay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmountToPay.ForeColor = System.Drawing.Color.Black
        Me.lblAmountToPay.Location = New System.Drawing.Point(791, 498)
        Me.lblAmountToPay.Name = "lblAmountToPay"
        Me.lblAmountToPay.Size = New System.Drawing.Size(27, 15)
        Me.lblAmountToPay.TabIndex = 28
        Me.lblAmountToPay.Tag = Nothing
        Me.lblAmountToPay.Text = "0.0"
        Me.lblAmountToPay.TextDetached = True
        Me.lblAmountToPay.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnPay
        '
        Me.btnPay.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPay.Location = New System.Drawing.Point(296, 538)
        Me.btnPay.Name = "btnPay"
        Me.btnPay.SetArticleCode = Nothing
        Me.btnPay.SetRowIndex = 0
        Me.btnPay.Size = New System.Drawing.Size(75, 19)
        Me.btnPay.TabIndex = 27
        Me.btnPay.Text = "Payment"
        Me.btnPay.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnPay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnPay.UseVisualStyleBackColor = True
        Me.btnPay.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel1
        '
        Me.CtrlLabel1.AttachedTextBoxName = Nothing
        Me.CtrlLabel1.AutoSize = True
        Me.CtrlLabel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel1.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel1.Location = New System.Drawing.Point(676, 497)
        Me.CtrlLabel1.Name = "CtrlLabel1"
        Me.CtrlLabel1.Size = New System.Drawing.Size(118, 15)
        Me.CtrlLabel1.TabIndex = 25
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.Text = "Expected Amount :"
        Me.CtrlLabel1.TextDetached = True
        Me.CtrlLabel1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtBatchBarcode
        '
        Me.txtBatchBarcode.AutoSize = False
        Me.txtBatchBarcode.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtBatchBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBatchBarcode.Location = New System.Drawing.Point(106, 132)
        Me.txtBatchBarcode.MaximumSize = New System.Drawing.Size(233, 21)
        Me.txtBatchBarcode.MinimumSize = New System.Drawing.Size(12, 21)
        Me.txtBatchBarcode.Name = "txtBatchBarcode"
        Me.txtBatchBarcode.Size = New System.Drawing.Size(152, 21)
        Me.txtBatchBarcode.TabIndex = 24
        Me.txtBatchBarcode.Tag = Nothing
        Me.txtBatchBarcode.TextDetached = True
        Me.txtBatchBarcode.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtBatchBarcode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblBatchBarcode
        '
        Me.lblBatchBarcode.AttachedTextBoxName = Nothing
        Me.lblBatchBarcode.AutoSize = True
        Me.lblBatchBarcode.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblBatchBarcode.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblBatchBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBatchBarcode.ForeColor = System.Drawing.Color.Black
        Me.lblBatchBarcode.Location = New System.Drawing.Point(12, 136)
        Me.lblBatchBarcode.Name = "lblBatchBarcode"
        Me.lblBatchBarcode.Size = New System.Drawing.Size(56, 15)
        Me.lblBatchBarcode.TabIndex = 23
        Me.lblBatchBarcode.Tag = Nothing
        Me.lblBatchBarcode.Text = "Barcode"
        Me.lblBatchBarcode.TextDetached = True
        Me.lblBatchBarcode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlTab1
        '
        Me.CtrlTab1.Controls.Add(Me.C1DockingTabPage1)
        Me.CtrlTab1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlTab1.Location = New System.Drawing.Point(0, 0)
        Me.CtrlTab1.Name = "CtrlTab1"
        Me.CtrlTab1.SelectedIndex = 1
        Me.CtrlTab1.Size = New System.Drawing.Size(853, 602)
        Me.CtrlTab1.TabIndex = 24
        Me.CtrlTab1.TabsSpacing = 5
        Me.CtrlTab1.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007
        Me.CtrlTab1.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Blue
        '
        'C1DockingTabPage1
        '
        Me.C1DockingTabPage1.Controls.Add(Me.Panel1)
        Me.C1DockingTabPage1.Location = New System.Drawing.Point(1, 25)
        Me.C1DockingTabPage1.Name = "C1DockingTabPage1"
        Me.C1DockingTabPage1.Size = New System.Drawing.Size(851, 576)
        Me.C1DockingTabPage1.TabIndex = 0
        Me.C1DockingTabPage1.TabStop = False
        Me.C1DockingTabPage1.Text = "Delivery"
        '
        'frmNOutboundDelivery
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(853, 602)
        Me.Controls.Add(Me.CtrlTab1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "frmNOutboundDelivery"
        Me.Text = "Outbound Delivery"
        CType(Me.cboDocType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LbDeliveredAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LbPickupAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNetAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LbNetAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReceivedAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LbReceivedAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdOrderInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDocType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.lblPickupAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDeliveredAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmountToPay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBatchBarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBatchBarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlTab1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CtrlTab1.ResumeLayout(False)
        Me.C1DockingTabPage1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnSearchOrder As Spectrum.CtrlBtn
    Friend WithEvents txtOrderNo As Spectrum.CtrlTextBox
    Friend WithEvents lblDocNo As Spectrum.CtrlLabel
    Friend WithEvents lblDocType As Spectrum.CtrlLabel
    Friend WithEvents CtrlCustDtls1 As Spectrum.CtrlCustDtls
    Friend WithEvents grdOrderInfo As Spectrum.CtrlGrid
    Friend WithEvents BtnOKOD As Spectrum.CtrlBtn
    Friend WithEvents BtnCancelOD As Spectrum.CtrlBtn
    Friend WithEvents cboDocType As C1.Win.C1List.C1Combo
    Friend WithEvents LbReceivedAmount As Spectrum.CtrlLabel
    Friend WithEvents lblReceivedAmount As Spectrum.CtrlLabel
    Friend WithEvents lblNetAmount As Spectrum.CtrlLabel
    Friend WithEvents LbNetAmount As Spectrum.CtrlLabel
    Friend WithEvents LbPickupAmt As Spectrum.CtrlLabel
    Friend WithEvents LbDeliveredAmt As Spectrum.CtrlLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CtrlTab1 As Spectrum.CtrlTab
    Friend WithEvents C1DockingTabPage1 As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents txtBatchBarcode As Spectrum.CtrlTextBox
    Friend WithEvents lblBatchBarcode As Spectrum.CtrlLabel
    Friend WithEvents btnPay As Spectrum.CtrlBtn
    Friend WithEvents CtrlLabel1 As Spectrum.CtrlLabel
    Friend WithEvents lblAmountToPay As Spectrum.CtrlLabel
    Friend WithEvents lblPickupAmt As Spectrum.CtrlLabel
    Friend WithEvents lblDeliveredAmt As Spectrum.CtrlLabel
End Class
