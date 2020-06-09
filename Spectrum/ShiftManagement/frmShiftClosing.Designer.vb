<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShiftClosing
    Inherits Spectrum.CtrlRbnBaseForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmShiftClosing))
        Me.sizTop = New C1.Win.C1Sizer.C1Sizer()
        Me.lblHeader = New Spectrum.CtrlLabel()
        Me.txtCheckAmount = New Spectrum.CtrlTextBox()
        Me.lblCheckRecevied = New Spectrum.CtrlLabel()
        Me.txtGVAmount = New Spectrum.CtrlTextBox()
        Me.lblGVreceived = New Spectrum.CtrlLabel()
        Me.txtCVAmount = New Spectrum.CtrlTextBox()
        Me.lblCVReceived = New Spectrum.CtrlLabel()
        Me.sizBottom = New C1.Win.C1Sizer.C1Sizer()
        Me.txtOperations = New Spectrum.CtrlTextBox()
        Me.sizFooter = New C1.Win.C1Sizer.C1Sizer()
        Me.cmdPrint = New Spectrum.CtrlBtn()
        Me.cancelBtn = New Spectrum.CtrlBtn()
        Me.cmdFinsh = New Spectrum.CtrlBtn()
        Me.cmdReset = New Spectrum.CtrlBtn()
        Me.cmdNext = New Spectrum.CtrlBtn()
        Me.lblOperation = New System.Windows.Forms.Label()
        Me.gbDenomination = New System.Windows.Forms.GroupBox()
        Me.dgMainGrid = New Spectrum.CtrlGrid()
        Me.cbCurrency = New Spectrum.ctrlCombo()
        Me.lblTotalAmount = New Spectrum.CtrlLabel()
        Me.lblAmount = New Spectrum.CtrlLabel()
        Me.CtrlLabel3 = New Spectrum.CtrlLabel()
        CType(Me.C1StatusBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sizTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sizTop.SuspendLayout()
        CType(Me.lblHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCheckAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCheckRecevied, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGVAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGVreceived, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCVAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCVReceived, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sizBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sizBottom.SuspendLayout()
        CType(Me.txtOperations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sizFooter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sizFooter.SuspendLayout()
        Me.gbDenomination.SuspendLayout()
        CType(Me.dgMainGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblLoggedIn
        '
        Me.lblLoggedIn.Text = Nothing
        '
        'sizTop
        '
        Me.sizTop.Border.Color = System.Drawing.Color.LightSlateGray
        Me.sizTop.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.sizTop.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.sizTop.Controls.Add(Me.lblHeader)
        Me.sizTop.Controls.Add(Me.txtCheckAmount)
        Me.sizTop.Controls.Add(Me.lblCheckRecevied)
        Me.sizTop.Controls.Add(Me.txtGVAmount)
        Me.sizTop.Controls.Add(Me.lblGVreceived)
        Me.sizTop.Controls.Add(Me.txtCVAmount)
        Me.sizTop.Controls.Add(Me.lblCVReceived)
        Me.sizTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.sizTop.GridDefinition = "92.7007299270073:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "97.9487179487179:False:False;"
        Me.sizTop.Location = New System.Drawing.Point(0, 0)
        Me.sizTop.Name = "sizTop"
        Me.sizTop.Padding = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.sizTop.Size = New System.Drawing.Size(585, 137)
        Me.sizTop.TabIndex = 4
        Me.sizTop.Text = "C1Sizer1"
        '
        'lblHeader
        '
        Me.lblHeader.AttachedTextBoxName = Nothing
        Me.lblHeader.AutoSize = True
        Me.lblHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblHeader.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(18, 9)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(371, 18)
        Me.lblHeader.TabIndex = 18
        Me.lblHeader.Tag = Nothing
        Me.lblHeader.Text = "Shift close is for Site {0}, Terminal {1}  and Date {2}"
        Me.lblHeader.TextDetached = True
        Me.lblHeader.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtCheckAmount
        '
        Me.txtCheckAmount.AutoSize = False
        Me.txtCheckAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtCheckAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCheckAmount.Location = New System.Drawing.Point(278, 99)
        Me.txtCheckAmount.MinimumSize = New System.Drawing.Size(12, 21)
        Me.txtCheckAmount.Name = "txtCheckAmount"
        Me.txtCheckAmount.Size = New System.Drawing.Size(202, 23)
        Me.txtCheckAmount.TabIndex = 2
        Me.txtCheckAmount.Tag = Nothing
        Me.txtCheckAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCheckAmount.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtCheckAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCheckRecevied
        '
        Me.lblCheckRecevied.AttachedTextBoxName = Nothing
        Me.lblCheckRecevied.AutoSize = True
        Me.lblCheckRecevied.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCheckRecevied.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCheckRecevied.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCheckRecevied.ForeColor = System.Drawing.Color.Black
        Me.lblCheckRecevied.Location = New System.Drawing.Point(18, 99)
        Me.lblCheckRecevied.Name = "lblCheckRecevied"
        Me.lblCheckRecevied.Size = New System.Drawing.Size(171, 15)
        Me.lblCheckRecevied.TabIndex = 4
        Me.lblCheckRecevied.Tag = Nothing
        Me.lblCheckRecevied.Text = "Enter amount of Cheque received:"
        Me.lblCheckRecevied.TextDetached = True
        Me.lblCheckRecevied.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtGVAmount
        '
        Me.txtGVAmount.AutoSize = False
        Me.txtGVAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtGVAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGVAmount.Location = New System.Drawing.Point(278, 46)
        Me.txtGVAmount.MinimumSize = New System.Drawing.Size(12, 21)
        Me.txtGVAmount.Name = "txtGVAmount"
        Me.txtGVAmount.Size = New System.Drawing.Size(202, 23)
        Me.txtGVAmount.TabIndex = 0
        Me.txtGVAmount.Tag = Nothing
        Me.txtGVAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtGVAmount.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtGVAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblGVreceived
        '
        Me.lblGVreceived.AttachedTextBoxName = Nothing
        Me.lblGVreceived.AutoSize = True
        Me.lblGVreceived.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGVreceived.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGVreceived.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGVreceived.ForeColor = System.Drawing.Color.Black
        Me.lblGVreceived.Location = New System.Drawing.Point(18, 47)
        Me.lblGVreceived.Name = "lblGVreceived"
        Me.lblGVreceived.Size = New System.Drawing.Size(149, 15)
        Me.lblGVreceived.TabIndex = 2
        Me.lblGVreceived.Tag = Nothing
        Me.lblGVreceived.Text = "Enter amount of GV received:"
        Me.lblGVreceived.TextDetached = True
        Me.lblGVreceived.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtCVAmount
        '
        Me.txtCVAmount.AutoSize = False
        Me.txtCVAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtCVAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCVAmount.Location = New System.Drawing.Point(278, 72)
        Me.txtCVAmount.MinimumSize = New System.Drawing.Size(12, 21)
        Me.txtCVAmount.Name = "txtCVAmount"
        Me.txtCVAmount.Size = New System.Drawing.Size(202, 23)
        Me.txtCVAmount.TabIndex = 1
        Me.txtCVAmount.Tag = Nothing
        Me.txtCVAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCVAmount.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtCVAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCVReceived
        '
        Me.lblCVReceived.AttachedTextBoxName = Nothing
        Me.lblCVReceived.AutoSize = True
        Me.lblCVReceived.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCVReceived.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCVReceived.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCVReceived.ForeColor = System.Drawing.Color.Black
        Me.lblCVReceived.Location = New System.Drawing.Point(18, 73)
        Me.lblCVReceived.Name = "lblCVReceived"
        Me.lblCVReceived.Size = New System.Drawing.Size(148, 15)
        Me.lblCVReceived.TabIndex = 0
        Me.lblCVReceived.Tag = Nothing
        Me.lblCVReceived.Text = "Enter amount of CV received:"
        Me.lblCVReceived.TextDetached = True
        Me.lblCVReceived.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'sizBottom
        '
        Me.sizBottom.Border.Color = System.Drawing.Color.LightSlateGray
        Me.sizBottom.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.sizBottom.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.sizBottom.Controls.Add(Me.txtOperations)
        Me.sizBottom.Controls.Add(Me.sizFooter)
        Me.sizBottom.Controls.Add(Me.lblOperation)
        Me.sizBottom.Controls.Add(Me.gbDenomination)
        Me.sizBottom.Controls.Add(Me.cbCurrency)
        Me.sizBottom.Controls.Add(Me.lblTotalAmount)
        Me.sizBottom.Controls.Add(Me.lblAmount)
        Me.sizBottom.Controls.Add(Me.CtrlLabel3)
        Me.sizBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sizBottom.GridDefinition = resources.GetString("sizBottom.GridDefinition")
        Me.sizBottom.Location = New System.Drawing.Point(0, 137)
        Me.sizBottom.Name = "sizBottom"
        Me.sizBottom.Size = New System.Drawing.Size(585, 490)
        Me.sizBottom.TabIndex = 5
        Me.sizBottom.Text = "C1Sizer2"
        '
        'txtOperations
        '
        Me.txtOperations.AutoSize = False
        Me.txtOperations.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtOperations.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOperations.Location = New System.Drawing.Point(293, 399)
        Me.txtOperations.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtOperations.Name = "txtOperations"
        Me.txtOperations.Size = New System.Drawing.Size(140, 27)
        Me.txtOperations.TabIndex = 6
        Me.txtOperations.Tag = Nothing
        Me.txtOperations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtOperations.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtOperations.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'sizFooter
        '
        Me.sizFooter.Border.Color = System.Drawing.Color.LightSlateGray
        Me.sizFooter.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.sizFooter.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.sizFooter.Controls.Add(Me.cmdPrint)
        Me.sizFooter.Controls.Add(Me.cancelBtn)
        Me.sizFooter.Controls.Add(Me.cmdFinsh)
        Me.sizFooter.Controls.Add(Me.cmdReset)
        Me.sizFooter.Controls.Add(Me.cmdNext)
        Me.sizFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.sizFooter.GridDefinition = "81.8181818181818:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "98.2608695652174:False:False;"
        Me.sizFooter.Location = New System.Drawing.Point(5, 430)
        Me.sizFooter.Name = "sizFooter"
        Me.sizFooter.Size = New System.Drawing.Size(575, 55)
        Me.sizFooter.TabIndex = 33
        Me.sizFooter.Text = "C1Sizer1"
        '
        'cmdPrint
        '
        Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(106, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.SetArticleCode = Nothing
        Me.cmdPrint.SetRowIndex = 0
        Me.cmdPrint.Size = New System.Drawing.Size(100, 32)
        Me.cmdPrint.TabIndex = 4
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdPrint.UseVisualStyleBackColor = True
        Me.cmdPrint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cancelBtn
        '
        Me.cancelBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cancelBtn.Location = New System.Drawing.Point(223, 10)
        Me.cancelBtn.Name = "cancelBtn"
        Me.cancelBtn.SetArticleCode = Nothing
        Me.cancelBtn.SetRowIndex = 0
        Me.cancelBtn.Size = New System.Drawing.Size(100, 32)
        Me.cancelBtn.TabIndex = 3
        Me.cancelBtn.Text = "&Cancel"
        Me.cancelBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cancelBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cancelBtn.UseVisualStyleBackColor = True
        Me.cancelBtn.Visible = False
        Me.cancelBtn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdFinsh
        '
        Me.cmdFinsh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdFinsh.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdFinsh.Location = New System.Drawing.Point(468, 10)
        Me.cmdFinsh.Name = "cmdFinsh"
        Me.cmdFinsh.SetArticleCode = Nothing
        Me.cmdFinsh.SetRowIndex = 0
        Me.cmdFinsh.Size = New System.Drawing.Size(100, 32)
        Me.cmdFinsh.TabIndex = 2
        Me.cmdFinsh.Text = "&Finsh"
        Me.cmdFinsh.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdFinsh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdFinsh.UseVisualStyleBackColor = True
        Me.cmdFinsh.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdReset
        '
        Me.cmdReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdReset.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdReset.Location = New System.Drawing.Point(353, 10)
        Me.cmdReset.Name = "cmdReset"
        Me.cmdReset.SetArticleCode = Nothing
        Me.cmdReset.SetRowIndex = 0
        Me.cmdReset.Size = New System.Drawing.Size(100, 32)
        Me.cmdReset.TabIndex = 1
        Me.cmdReset.Text = "&Reset"
        Me.cmdReset.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdReset.UseVisualStyleBackColor = True
        Me.cmdReset.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdNext
        '
        Me.cmdNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdNext.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdNext.Location = New System.Drawing.Point(237, 10)
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.SetArticleCode = Nothing
        Me.cmdNext.SetRowIndex = 0
        Me.cmdNext.Size = New System.Drawing.Size(100, 32)
        Me.cmdNext.TabIndex = 0
        Me.cmdNext.Text = "&Next"
        Me.cmdNext.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdNext.UseVisualStyleBackColor = True
        Me.cmdNext.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblOperation
        '
        Me.lblOperation.AutoSize = True
        Me.lblOperation.Location = New System.Drawing.Point(5, 399)
        Me.lblOperation.Name = "lblOperation"
        Me.lblOperation.Size = New System.Drawing.Size(149, 13)
        Me.lblOperation.TabIndex = 5
        Me.lblOperation.Text = "Enter amount paid to vendors:"
        '
        'gbDenomination
        '
        Me.gbDenomination.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbDenomination.Controls.Add(Me.dgMainGrid)
        Me.gbDenomination.Location = New System.Drawing.Point(5, 34)
        Me.gbDenomination.Name = "gbDenomination"
        Me.gbDenomination.Size = New System.Drawing.Size(575, 329)
        Me.gbDenomination.TabIndex = 33
        Me.gbDenomination.TabStop = False
        Me.gbDenomination.Text = "Tender Declaration:"
        '
        'dgMainGrid
        '
        Me.dgMainGrid.CellButtonImage = CType(resources.GetObject("dgMainGrid.CellButtonImage"), System.Drawing.Image)
        Me.dgMainGrid.ColumnInfo = resources.GetString("dgMainGrid.ColumnInfo")
        Me.dgMainGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgMainGrid.ExtendLastCol = True
        Me.dgMainGrid.Location = New System.Drawing.Point(3, 16)
        Me.dgMainGrid.Name = "dgMainGrid"
        Me.dgMainGrid.Rows.Count = 2
        Me.dgMainGrid.Rows.DefaultSize = 20
        Me.dgMainGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell
        Me.dgMainGrid.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.dgMainGrid.Size = New System.Drawing.Size(569, 310)
        Me.dgMainGrid.StyleInfo = resources.GetString("dgMainGrid.StyleInfo")
        Me.dgMainGrid.TabIndex = 0
        Me.dgMainGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Black
        '
        'cbCurrency
        '
        Me.cbCurrency.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cbCurrency.AutoCompletion = True
        Me.cbCurrency.AutoDropDown = True
        Me.cbCurrency.Caption = ""
        Me.cbCurrency.CaptionHeight = 17
        Me.cbCurrency.CaptionVisible = False
        Me.cbCurrency.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cbCurrency.ColumnCaptionHeight = 17
        Me.cbCurrency.ColumnFooterHeight = 17
        Me.cbCurrency.ColumnHeaders = False
        Me.cbCurrency.ContentHeight = 16
        Me.cbCurrency.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cbCurrency.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cbCurrency.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCurrency.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cbCurrency.EditorHeight = 16
        Me.cbCurrency.Images.Add(CType(resources.GetObject("cbCurrency.Images"), System.Drawing.Image))
        Me.cbCurrency.ItemHeight = 15
        Me.cbCurrency.Location = New System.Drawing.Point(149, 5)
        Me.cbCurrency.MatchEntryTimeout = CType(2000, Long)
        Me.cbCurrency.MaxDropDownItems = CType(5, Short)
        Me.cbCurrency.MaxLength = 32767
        Me.cbCurrency.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cbCurrency.Name = "cbCurrency"
        Me.cbCurrency.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cbCurrency.Size = New System.Drawing.Size(140, 22)
        Me.cbCurrency.TabIndex = 0
        Me.cbCurrency.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cbCurrency.PropBag = resources.GetString("cbCurrency.PropBag")
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.AttachedTextBoxName = Nothing
        Me.lblTotalAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTotalAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalAmount.ForeColor = System.Drawing.Color.Black
        Me.lblTotalAmount.Location = New System.Drawing.Point(293, 367)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(140, 28)
        Me.lblTotalAmount.TabIndex = 32
        Me.lblTotalAmount.Tag = Nothing
        Me.lblTotalAmount.Text = "0.00"
        Me.lblTotalAmount.TextDetached = True
        Me.lblTotalAmount.Value = "0.00"
        Me.lblTotalAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblAmount
        '
        Me.lblAmount.AttachedTextBoxName = Nothing
        Me.lblAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAmount.ForeColor = System.Drawing.Color.Black
        Me.lblAmount.Location = New System.Drawing.Point(5, 367)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(140, 28)
        Me.lblAmount.TabIndex = 30
        Me.lblAmount.Tag = Nothing
        Me.lblAmount.Text = "Total Amount"
        Me.lblAmount.TextDetached = True
        Me.lblAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel3
        '
        Me.CtrlLabel3.AttachedTextBoxName = Nothing
        Me.CtrlLabel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel3.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel3.Location = New System.Drawing.Point(5, 5)
        Me.CtrlLabel3.Name = "CtrlLabel3"
        Me.CtrlLabel3.Size = New System.Drawing.Size(140, 25)
        Me.CtrlLabel3.TabIndex = 1
        Me.CtrlLabel3.Tag = Nothing
        Me.CtrlLabel3.Text = "Select Currency"
        Me.CtrlLabel3.TextDetached = True
        Me.CtrlLabel3.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmShiftClosing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(585, 649)
        Me.Controls.Add(Me.sizBottom)
        Me.Controls.Add(Me.sizTop)
        Me.MaximumSize = New System.Drawing.Size(593, 680)
        Me.MinimumSize = New System.Drawing.Size(593, 602)
        Me.Name = "frmShiftClosing"
        Me.Text = "Shift Closing"
        Me.Controls.SetChildIndex(Me.C1StatusBar1, 0)
        Me.Controls.SetChildIndex(Me.sizTop, 0)
        Me.Controls.SetChildIndex(Me.sizBottom, 0)
        CType(Me.C1StatusBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sizTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sizTop.ResumeLayout(False)
        Me.sizTop.PerformLayout()
        CType(Me.lblHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCheckAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCheckRecevied, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGVAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGVreceived, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCVAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCVReceived, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sizBottom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sizBottom.ResumeLayout(False)
        Me.sizBottom.PerformLayout()
        CType(Me.txtOperations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sizFooter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sizFooter.ResumeLayout(False)
        Me.gbDenomination.ResumeLayout(False)
        CType(Me.dgMainGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents sizTop As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents lblHeader As Spectrum.CtrlLabel
    Friend WithEvents txtCheckAmount As Spectrum.CtrlTextBox
    Friend WithEvents lblCheckRecevied As Spectrum.CtrlLabel
    Friend WithEvents txtGVAmount As Spectrum.CtrlTextBox
    Friend WithEvents lblGVreceived As Spectrum.CtrlLabel
    Friend WithEvents txtCVAmount As Spectrum.CtrlTextBox
    Friend WithEvents lblCVReceived As Spectrum.CtrlLabel
    Friend WithEvents sizBottom As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents txtOperations As Spectrum.CtrlTextBox
    Friend WithEvents sizFooter As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents cmdPrint As Spectrum.CtrlBtn
    Friend WithEvents cancelBtn As Spectrum.CtrlBtn
    Friend WithEvents cmdFinsh As Spectrum.CtrlBtn
    Friend WithEvents cmdReset As Spectrum.CtrlBtn
    Friend WithEvents cmdNext As Spectrum.CtrlBtn
    Friend WithEvents lblOperation As System.Windows.Forms.Label
    Friend WithEvents gbDenomination As System.Windows.Forms.GroupBox
    Friend WithEvents dgMainGrid As Spectrum.CtrlGrid
    Friend WithEvents cbCurrency As Spectrum.ctrlCombo
    Friend WithEvents lblTotalAmount As Spectrum.CtrlLabel
    Friend WithEvents lblAmount As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel3 As Spectrum.CtrlLabel
End Class
