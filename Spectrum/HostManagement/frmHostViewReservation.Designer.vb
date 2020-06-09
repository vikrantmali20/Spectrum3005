<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHostViewReservation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHostViewReservation))
        Me.TextBox1 = New Spectrum.Controls.TextBox(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmdCancel = New Spectrum.CtrlBtn()
        Me.cmdViewClosedRes = New Spectrum.CtrlBtn()
        Me.cmdEditCheckin = New Spectrum.CtrlBtn()
        Me.cmdPaymentCheckout = New Spectrum.CtrlBtn()
        Me.cmdCancelReservation = New Spectrum.CtrlBtn()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblSearchResult = New Spectrum.CtrlLabel()
        Me.grdViewReservationDetails = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblStatus = New Spectrum.CtrlLabel()
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        Me.lblRoomtype = New Spectrum.CtrlLabel()
        Me.cmdClear = New Spectrum.CtrlBtn()
        Me.lblResId = New Spectrum.CtrlLabel()
        Me.txtMobileNo = New Spectrum.CtrlTextBox()
        Me.CmbStatus = New Spectrum.ctrlCombo()
        Me.cmdSearch = New Spectrum.CtrlBtn()
        Me.txtReservationId = New Spectrum.CtrlTextBox()
        Me.txtName = New Spectrum.CtrlTextBox()
        CType(Me.TextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.lblSearchResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdViewReservationDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRoomtype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblResId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMobileNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmbStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReservationId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.TextBox1.Location = New System.Drawing.Point(3, 3)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(118, 64)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.Tag = Nothing
        Me.TextBox1.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        Me.TextBox1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.cmdCancel)
        Me.Panel2.Controls.Add(Me.cmdViewClosedRes)
        Me.Panel2.Controls.Add(Me.cmdEditCheckin)
        Me.Panel2.Controls.Add(Me.cmdPaymentCheckout)
        Me.Panel2.Controls.Add(Me.cmdCancelReservation)
        Me.Panel2.Location = New System.Drawing.Point(24, 517)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1204, 77)
        Me.Panel2.TabIndex = 57
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdCancel.Font = New System.Drawing.Font("Calibri", 9.25!)
        Me.cmdCancel.ForeColor = System.Drawing.Color.White
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdCancel.Location = New System.Drawing.Point(1073, 14)
        Me.cmdCancel.MoveToNxtCtrl = Nothing
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.SetArticleCode = Nothing
        Me.cmdCancel.SetRowIndex = 0
        Me.cmdCancel.Size = New System.Drawing.Size(117, 43)
        Me.cmdCancel.TabIndex = 4
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdCancel.UseVisualStyleBackColor = False
        Me.cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdViewClosedRes
        '
        Me.cmdViewClosedRes.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdViewClosedRes.Font = New System.Drawing.Font("Calibri", 9.25!)
        Me.cmdViewClosedRes.ForeColor = System.Drawing.Color.White
        Me.cmdViewClosedRes.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdViewClosedRes.Location = New System.Drawing.Point(807, 14)
        Me.cmdViewClosedRes.MoveToNxtCtrl = Nothing
        Me.cmdViewClosedRes.Name = "cmdViewClosedRes"
        Me.cmdViewClosedRes.SetArticleCode = Nothing
        Me.cmdViewClosedRes.SetRowIndex = 0
        Me.cmdViewClosedRes.Size = New System.Drawing.Size(117, 43)
        Me.cmdViewClosedRes.TabIndex = 3
        Me.cmdViewClosedRes.Text = "View Closed Reservation"
        Me.cmdViewClosedRes.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdViewClosedRes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdViewClosedRes.UseVisualStyleBackColor = False
        Me.cmdViewClosedRes.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdEditCheckin
        '
        Me.cmdEditCheckin.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdEditCheckin.Font = New System.Drawing.Font("Calibri", 9.25!)
        Me.cmdEditCheckin.ForeColor = System.Drawing.Color.White
        Me.cmdEditCheckin.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdEditCheckin.Location = New System.Drawing.Point(536, 14)
        Me.cmdEditCheckin.MoveToNxtCtrl = Nothing
        Me.cmdEditCheckin.Name = "cmdEditCheckin"
        Me.cmdEditCheckin.SetArticleCode = Nothing
        Me.cmdEditCheckin.SetRowIndex = 0
        Me.cmdEditCheckin.Size = New System.Drawing.Size(117, 43)
        Me.cmdEditCheckin.TabIndex = 0
        Me.cmdEditCheckin.Text = "Edit/Check-in"
        Me.cmdEditCheckin.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdEditCheckin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdEditCheckin.UseVisualStyleBackColor = False
        Me.cmdEditCheckin.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdPaymentCheckout
        '
        Me.cmdPaymentCheckout.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdPaymentCheckout.Font = New System.Drawing.Font("Calibri", 9.25!)
        Me.cmdPaymentCheckout.ForeColor = System.Drawing.Color.White
        Me.cmdPaymentCheckout.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPaymentCheckout.Location = New System.Drawing.Point(944, 14)
        Me.cmdPaymentCheckout.MoveToNxtCtrl = Nothing
        Me.cmdPaymentCheckout.Name = "cmdPaymentCheckout"
        Me.cmdPaymentCheckout.SetArticleCode = Nothing
        Me.cmdPaymentCheckout.SetRowIndex = 0
        Me.cmdPaymentCheckout.Size = New System.Drawing.Size(117, 43)
        Me.cmdPaymentCheckout.TabIndex = 2
        Me.cmdPaymentCheckout.Text = "Payments/Check-out"
        Me.cmdPaymentCheckout.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPaymentCheckout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdPaymentCheckout.UseVisualStyleBackColor = False
        Me.cmdPaymentCheckout.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdCancelReservation
        '
        Me.cmdCancelReservation.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdCancelReservation.Font = New System.Drawing.Font("Calibri", 9.25!)
        Me.cmdCancelReservation.ForeColor = System.Drawing.Color.White
        Me.cmdCancelReservation.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdCancelReservation.Location = New System.Drawing.Point(673, 14)
        Me.cmdCancelReservation.MoveToNxtCtrl = Nothing
        Me.cmdCancelReservation.Name = "cmdCancelReservation"
        Me.cmdCancelReservation.SetArticleCode = Nothing
        Me.cmdCancelReservation.SetRowIndex = 0
        Me.cmdCancelReservation.Size = New System.Drawing.Size(117, 43)
        Me.cmdCancelReservation.TabIndex = 1
        Me.cmdCancelReservation.Text = "Cancel Reservation"
        Me.cmdCancelReservation.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdCancelReservation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdCancelReservation.UseVisualStyleBackColor = False
        Me.cmdCancelReservation.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel4, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.grdViewReservationDetails, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(21, 130)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.124011!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.87599!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1207, 366)
        Me.TableLayoutPanel1.TabIndex = 56
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.lblSearchResult, 0, 0)
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(1207, 26)
        Me.TableLayoutPanel4.TabIndex = 1
        '
        'lblSearchResult
        '
        Me.lblSearchResult.AttachedTextBoxName = Nothing
        Me.lblSearchResult.AutoSize = True
        Me.lblSearchResult.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.lblSearchResult.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.lblSearchResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSearchResult.Font = New System.Drawing.Font("Calibri", 9.25!, System.Drawing.FontStyle.Bold)
        Me.lblSearchResult.ForeColor = System.Drawing.Color.White
        Me.lblSearchResult.Location = New System.Drawing.Point(3, 0)
        Me.lblSearchResult.Name = "lblSearchResult"
        Me.lblSearchResult.Padding = New System.Windows.Forms.Padding(4)
        Me.lblSearchResult.Size = New System.Drawing.Size(90, 25)
        Me.lblSearchResult.TabIndex = 2
        Me.lblSearchResult.Tag = Nothing
        Me.lblSearchResult.Text = "Search Result"
        Me.lblSearchResult.TextDetached = True
        Me.lblSearchResult.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'grdViewReservationDetails
        '
        Me.grdViewReservationDetails.AutoGenerateColumns = False
        Me.grdViewReservationDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.grdViewReservationDetails.CellButtonImage = Global.Spectrum.My.Resources.Resources.del_icon
        Me.grdViewReservationDetails.ColumnInfo = resources.GetString("grdViewReservationDetails.ColumnInfo")
        Me.grdViewReservationDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdViewReservationDetails.ExtendLastCol = True
        Me.grdViewReservationDetails.Font = New System.Drawing.Font("Calibri", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdViewReservationDetails.Location = New System.Drawing.Point(3, 29)
        Me.grdViewReservationDetails.Name = "grdViewReservationDetails"
        Me.grdViewReservationDetails.NewRowWatermark = ""
        Me.grdViewReservationDetails.Rows.Count = 1
        Me.grdViewReservationDetails.Rows.DefaultSize = 20
        Me.grdViewReservationDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.grdViewReservationDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.grdViewReservationDetails.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.grdViewReservationDetails.Size = New System.Drawing.Size(1201, 334)
        Me.grdViewReservationDetails.StyleInfo = resources.GetString("grdViewReservationDetails.StyleInfo")
        Me.grdViewReservationDetails.TabIndex = 54
        Me.grdViewReservationDetails.Tag = ""
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.lblStatus)
        Me.Panel1.Controls.Add(Me.CtrlLabel1)
        Me.Panel1.Controls.Add(Me.lblRoomtype)
        Me.Panel1.Controls.Add(Me.cmdClear)
        Me.Panel1.Controls.Add(Me.lblResId)
        Me.Panel1.Controls.Add(Me.txtMobileNo)
        Me.Panel1.Controls.Add(Me.CmbStatus)
        Me.Panel1.Controls.Add(Me.cmdSearch)
        Me.Panel1.Controls.Add(Me.txtReservationId)
        Me.Panel1.Controls.Add(Me.txtName)
        Me.Panel1.Location = New System.Drawing.Point(21, 18)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1207, 88)
        Me.Panel1.TabIndex = 55
        '
        'lblStatus
        '
        Me.lblStatus.AttachedTextBoxName = Nothing
        Me.lblStatus.AutoSize = True
        Me.lblStatus.BackColor = System.Drawing.Color.White
        Me.lblStatus.BorderColor = System.Drawing.Color.White
        Me.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatus.Font = New System.Drawing.Font("Calibri", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.Black
        Me.lblStatus.Location = New System.Drawing.Point(793, 31)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Padding = New System.Windows.Forms.Padding(3)
        Me.lblStatus.Size = New System.Drawing.Size(49, 23)
        Me.lblStatus.TabIndex = 35
        Me.lblStatus.Tag = Nothing
        Me.lblStatus.Text = "Status"
        Me.lblStatus.TextDetached = True
        Me.lblStatus.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel1
        '
        Me.CtrlLabel1.AttachedTextBoxName = Nothing
        Me.CtrlLabel1.AutoSize = True
        Me.CtrlLabel1.BackColor = System.Drawing.Color.White
        Me.CtrlLabel1.BorderColor = System.Drawing.Color.White
        Me.CtrlLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel1.Font = New System.Drawing.Font("Calibri", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlLabel1.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel1.Location = New System.Drawing.Point(21, 31)
        Me.CtrlLabel1.Name = "CtrlLabel1"
        Me.CtrlLabel1.Padding = New System.Windows.Forms.Padding(3)
        Me.CtrlLabel1.Size = New System.Drawing.Size(100, 23)
        Me.CtrlLabel1.TabIndex = 48
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.Text = "Mobile Number"
        Me.CtrlLabel1.TextDetached = True
        Me.CtrlLabel1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblRoomtype
        '
        Me.lblRoomtype.AttachedTextBoxName = Nothing
        Me.lblRoomtype.AutoSize = True
        Me.lblRoomtype.BackColor = System.Drawing.Color.White
        Me.lblRoomtype.BorderColor = System.Drawing.Color.White
        Me.lblRoomtype.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRoomtype.Font = New System.Drawing.Font("Calibri", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoomtype.ForeColor = System.Drawing.Color.Black
        Me.lblRoomtype.Location = New System.Drawing.Point(497, 31)
        Me.lblRoomtype.Name = "lblRoomtype"
        Me.lblRoomtype.Padding = New System.Windows.Forms.Padding(3)
        Me.lblRoomtype.Size = New System.Drawing.Size(46, 23)
        Me.lblRoomtype.TabIndex = 33
        Me.lblRoomtype.Tag = Nothing
        Me.lblRoomtype.Text = "Name"
        Me.lblRoomtype.TextDetached = True
        Me.lblRoomtype.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdClear
        '
        Me.cmdClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdClear.Font = New System.Drawing.Font("Calibri", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClear.ForeColor = System.Drawing.Color.White
        Me.cmdClear.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdClear.Location = New System.Drawing.Point(1094, 34)
        Me.cmdClear.MoveToNxtCtrl = Nothing
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.SetArticleCode = Nothing
        Me.cmdClear.SetRowIndex = 0
        Me.cmdClear.Size = New System.Drawing.Size(79, 25)
        Me.cmdClear.TabIndex = 39
        Me.cmdClear.Text = "Clear"
        Me.cmdClear.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdClear.UseVisualStyleBackColor = False
        Me.cmdClear.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblResId
        '
        Me.lblResId.AttachedTextBoxName = Nothing
        Me.lblResId.AutoSize = True
        Me.lblResId.BackColor = System.Drawing.Color.White
        Me.lblResId.BorderColor = System.Drawing.Color.White
        Me.lblResId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblResId.Font = New System.Drawing.Font("Calibri", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResId.ForeColor = System.Drawing.Color.Black
        Me.lblResId.Location = New System.Drawing.Point(269, 31)
        Me.lblResId.Name = "lblResId"
        Me.lblResId.Padding = New System.Windows.Forms.Padding(3)
        Me.lblResId.Size = New System.Drawing.Size(49, 23)
        Me.lblResId.TabIndex = 41
        Me.lblResId.Tag = Nothing
        Me.lblResId.Text = "Res ID"
        Me.lblResId.TextDetached = True
        Me.lblResId.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtMobileNo
        '
        Me.txtMobileNo.AutoSize = False
        Me.txtMobileNo.BackColor = System.Drawing.Color.White
        Me.txtMobileNo.BorderColor = System.Drawing.Color.Silver
        Me.txtMobileNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMobileNo.Font = New System.Drawing.Font("Calibri", 9.25!)
        Me.txtMobileNo.Location = New System.Drawing.Point(139, 34)
        Me.txtMobileNo.MinimumSize = New System.Drawing.Size(12, 21)
        Me.txtMobileNo.MoveToNxtCtrl = Nothing
        Me.txtMobileNo.Name = "txtMobileNo"
        Me.txtMobileNo.Padding = New System.Windows.Forms.Padding(2)
        Me.txtMobileNo.Size = New System.Drawing.Size(123, 21)
        Me.txtMobileNo.TabIndex = 44
        Me.txtMobileNo.Tag = Nothing
        Me.txtMobileNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CmbStatus
        '
        Me.CmbStatus.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.CmbStatus.AutoCompletion = True
        Me.CmbStatus.AutoDropDown = True
        Me.CmbStatus.Caption = ""
        Me.CmbStatus.CaptionHeight = 17
        Me.CmbStatus.CaptionVisible = False
        Me.CmbStatus.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbStatus.ColumnCaptionHeight = 17
        Me.CmbStatus.ColumnFooterHeight = 17
        Me.CmbStatus.ColumnHeaders = False
        Me.CmbStatus.ContentHeight = 15
        Me.CmbStatus.ctrlTextDbColumn = ""
        Me.CmbStatus.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.CmbStatus.EditorBackColor = System.Drawing.SystemColors.Window
        Me.CmbStatus.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbStatus.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.CmbStatus.EditorHeight = 15
        Me.CmbStatus.Font = New System.Drawing.Font("Calibri", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbStatus.Images.Add(CType(resources.GetObject("CmbStatus.Images"), System.Drawing.Image))
        Me.CmbStatus.ItemHeight = 15
        Me.CmbStatus.Location = New System.Drawing.Point(862, 34)
        Me.CmbStatus.MatchEntryTimeout = CType(2000, Long)
        Me.CmbStatus.MaxDropDownItems = CType(5, Short)
        Me.CmbStatus.MaxLength = 32767
        Me.CmbStatus.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.CmbStatus.MoveToNxtCtrl = Nothing
        Me.CmbStatus.Name = "CmbStatus"
        Me.CmbStatus.Padding = New System.Windows.Forms.Padding(2)
        Me.CmbStatus.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.CmbStatus.Size = New System.Drawing.Size(128, 21)
        Me.CmbStatus.strSelectStmt = ""
        Me.CmbStatus.TabIndex = 36
        Me.CmbStatus.Text = "cmbStatus"
        Me.CmbStatus.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.CmbStatus.PropBag = resources.GetString("CmbStatus.PropBag")
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdSearch.Font = New System.Drawing.Font("Calibri", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.Color.White
        Me.cmdSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSearch.Location = New System.Drawing.Point(1001, 34)
        Me.cmdSearch.MoveToNxtCtrl = Nothing
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.SetArticleCode = Nothing
        Me.cmdSearch.SetRowIndex = 0
        Me.cmdSearch.Size = New System.Drawing.Size(79, 25)
        Me.cmdSearch.TabIndex = 47
        Me.cmdSearch.Text = "Search"
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdSearch.UseVisualStyleBackColor = False
        Me.cmdSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtReservationId
        '
        Me.txtReservationId.AutoSize = False
        Me.txtReservationId.BackColor = System.Drawing.Color.White
        Me.txtReservationId.BorderColor = System.Drawing.Color.Silver
        Me.txtReservationId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReservationId.Font = New System.Drawing.Font("Calibri", 9.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReservationId.Location = New System.Drawing.Point(336, 34)
        Me.txtReservationId.MinimumSize = New System.Drawing.Size(14, 21)
        Me.txtReservationId.MoveToNxtCtrl = Nothing
        Me.txtReservationId.Name = "txtReservationId"
        Me.txtReservationId.Padding = New System.Windows.Forms.Padding(2)
        Me.txtReservationId.Size = New System.Drawing.Size(155, 21)
        Me.txtReservationId.TabIndex = 45
        Me.txtReservationId.Tag = Nothing
        Me.txtReservationId.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtName
        '
        Me.txtName.AutoSize = False
        Me.txtName.BackColor = System.Drawing.Color.White
        Me.txtName.BorderColor = System.Drawing.Color.Silver
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Font = New System.Drawing.Font("Calibri", 9.25!)
        Me.txtName.Location = New System.Drawing.Point(563, 34)
        Me.txtName.MinimumSize = New System.Drawing.Size(14, 21)
        Me.txtName.MoveToNxtCtrl = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.Padding = New System.Windows.Forms.Padding(2)
        Me.txtName.Size = New System.Drawing.Size(219, 21)
        Me.txtName.TabIndex = 46
        Me.txtName.Tag = Nothing
        Me.txtName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmHostViewReservation
        '
        Me.AccessibleDescription = ""
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(172, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1251, 617)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHostViewReservation"
        Me.Text = "View Reservation"
        Me.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Office2007Black
        CType(Me.TextBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.lblSearchResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdViewReservationDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRoomtype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblResId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMobileNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmbStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReservationId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdViewReservationDetails As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents TextBox1 As Spectrum.Controls.TextBox
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblSearchResult As Spectrum.CtrlLabel
    Friend WithEvents lblStatus As Spectrum.CtrlLabel
    Friend WithEvents lblRoomtype As Spectrum.CtrlLabel
    Friend WithEvents lblResId As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel1 As Spectrum.CtrlLabel
    Friend WithEvents cmdSearch As Spectrum.CtrlBtn
    Friend WithEvents txtName As Spectrum.CtrlTextBox
    Friend WithEvents txtReservationId As Spectrum.CtrlTextBox
    Friend WithEvents CmbStatus As Spectrum.ctrlCombo
    Friend WithEvents txtMobileNo As Spectrum.CtrlTextBox
    Friend WithEvents cmdClear As Spectrum.CtrlBtn
    Friend WithEvents cmdViewClosedRes As Spectrum.CtrlBtn
    Friend WithEvents cmdPaymentCheckout As Spectrum.CtrlBtn
    Friend WithEvents cmdCancelReservation As Spectrum.CtrlBtn
    Friend WithEvents cmdEditCheckin As Spectrum.CtrlBtn
    Friend WithEvents cmdCancel As Spectrum.CtrlBtn
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
End Class
