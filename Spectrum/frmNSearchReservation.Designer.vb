<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNSearchReservation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNSearchReservation))
        Me.c1Sizer1 = New C1.Win.C1Sizer.C1Sizer()
        Me.btnCheckIn = New Spectrum.CtrlBtn()
        Me.btnChangeTable = New Spectrum.CtrlBtn()
        Me.btnChangeTime = New Spectrum.CtrlBtn()
        Me.cmdDelete = New Spectrum.CtrlBtn()
        Me.txtPeople = New Spectrum.CtrlTextBox()
        Me.tblpanel = New System.Windows.Forms.TableLayoutPanel()
        Me.lblName = New Spectrum.CtrlLabel()
        Me.CtrlLabel2 = New Spectrum.CtrlLabel()
        Me.CtrlLabel5 = New Spectrum.CtrlLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CtrlBtnClear = New Spectrum.CtrlBtn()
        Me.cmdSearch = New Spectrum.CtrlBtn()
        Me.txtDate = New Spectrum.ctrlDate()
        Me.txtTime = New Spectrum.ctrlDate()
        Me.lblTabelNo = New Spectrum.CtrlLabel()
        Me.txtTableNo = New Spectrum.CtrlTextBox()
        Me.lblPhoneNo = New Spectrum.CtrlLabel()
        Me.txtName = New Spectrum.CtrlTextBox()
        Me.txtPhone = New Spectrum.CtrlTextBox()
        Me.CmdShowFilter = New Spectrum.CtrlBtn()
        Me.dgMainGrid = New Spectrum.CtrlGrid()
        Me.lblTicketTitle = New Spectrum.CtrlLabel()
        Me.lblGrievanceId = New Spectrum.CtrlLabel()
        CType(Me.c1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.c1Sizer1.SuspendLayout()
        CType(Me.txtPeople, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tblpanel.SuspendLayout()
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTabelNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTableNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPhoneNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPhone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgMainGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTicketTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGrievanceId, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'c1Sizer1
        '
        Me.c1Sizer1.Controls.Add(Me.btnCheckIn)
        Me.c1Sizer1.Controls.Add(Me.btnChangeTable)
        Me.c1Sizer1.Controls.Add(Me.btnChangeTime)
        Me.c1Sizer1.Controls.Add(Me.cmdDelete)
        Me.c1Sizer1.Controls.Add(Me.txtPeople)
        Me.c1Sizer1.Controls.Add(Me.tblpanel)
        Me.c1Sizer1.Controls.Add(Me.CmdShowFilter)
        Me.c1Sizer1.Controls.Add(Me.dgMainGrid)
        Me.c1Sizer1.GridDefinition = "98.6754966887417:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "99.1304347826087:False:False;"
        Me.c1Sizer1.Location = New System.Drawing.Point(4, 2)
        Me.c1Sizer1.Name = "c1Sizer1"
        Me.c1Sizer1.Size = New System.Drawing.Size(920, 604)
        Me.c1Sizer1.TabIndex = 102
        Me.c1Sizer1.Text = "c1Sizer1"
        '
        'btnCheckIn
        '
        Me.btnCheckIn.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCheckIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCheckIn.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCheckIn.Location = New System.Drawing.Point(698, 530)
        Me.btnCheckIn.MoveToNxtCtrl = Nothing
        Me.btnCheckIn.Name = "btnCheckIn"
        Me.btnCheckIn.SetArticleCode = Nothing
        Me.btnCheckIn.SetRowIndex = 0
        Me.btnCheckIn.Size = New System.Drawing.Size(92, 34)
        Me.btnCheckIn.TabIndex = 50
        Me.btnCheckIn.Text = "&CheckIn"
        Me.btnCheckIn.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCheckIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCheckIn.UseVisualStyleBackColor = True
        Me.btnCheckIn.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.btnCheckIn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnChangeTable
        '
        Me.btnChangeTable.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChangeTable.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangeTable.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnChangeTable.Location = New System.Drawing.Point(502, 530)
        Me.btnChangeTable.MoveToNxtCtrl = Nothing
        Me.btnChangeTable.Name = "btnChangeTable"
        Me.btnChangeTable.SetArticleCode = Nothing
        Me.btnChangeTable.SetRowIndex = 0
        Me.btnChangeTable.Size = New System.Drawing.Size(92, 34)
        Me.btnChangeTable.TabIndex = 48
        Me.btnChangeTable.Text = "&Change Table"
        Me.btnChangeTable.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnChangeTable.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnChangeTable.UseVisualStyleBackColor = True
        Me.btnChangeTable.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.btnChangeTable.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnChangeTime
        '
        Me.btnChangeTime.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChangeTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangeTime.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnChangeTime.Location = New System.Drawing.Point(600, 530)
        Me.btnChangeTime.MoveToNxtCtrl = Nothing
        Me.btnChangeTime.Name = "btnChangeTime"
        Me.btnChangeTime.SetArticleCode = Nothing
        Me.btnChangeTime.SetRowIndex = 0
        Me.btnChangeTime.Size = New System.Drawing.Size(92, 34)
        Me.btnChangeTime.TabIndex = 47
        Me.btnChangeTime.Text = "&Change Time"
        Me.btnChangeTime.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnChangeTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnChangeTime.UseVisualStyleBackColor = True
        Me.btnChangeTime.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.btnChangeTime.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdDelete
        '
        Me.cmdDelete.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdDelete.Location = New System.Drawing.Point(796, 530)
        Me.cmdDelete.MoveToNxtCtrl = Nothing
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.SetArticleCode = Nothing
        Me.cmdDelete.SetRowIndex = 0
        Me.cmdDelete.Size = New System.Drawing.Size(92, 34)
        Me.cmdDelete.TabIndex = 46
        Me.cmdDelete.Text = "&Cancel Reservation"
        Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdDelete.UseVisualStyleBackColor = True
        Me.cmdDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.cmdDelete.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtPeople
        '
        Me.txtPeople.AutoSize = False
        Me.txtPeople.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtPeople.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPeople.Location = New System.Drawing.Point(127, 14)
        Me.txtPeople.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtPeople.MoveToNxtCtrl = Nothing
        Me.txtPeople.Name = "txtPeople"
        Me.txtPeople.Size = New System.Drawing.Size(100, 21)
        Me.txtPeople.TabIndex = 43
        Me.txtPeople.Tag = Nothing
        Me.txtPeople.Visible = False
        Me.txtPeople.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtPeople.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'tblpanel
        '
        Me.tblpanel.ColumnCount = 3
        Me.tblpanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108.0!))
        Me.tblpanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 641.0!))
        Me.tblpanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 14.0!))
        Me.tblpanel.Controls.Add(Me.lblName, 0, 2)
        Me.tblpanel.Controls.Add(Me.CtrlLabel2, 0, 3)
        Me.tblpanel.Controls.Add(Me.CtrlLabel5, 0, 4)
        Me.tblpanel.Controls.Add(Me.Panel1, 1, 5)
        Me.tblpanel.Controls.Add(Me.txtDate, 1, 3)
        Me.tblpanel.Controls.Add(Me.txtTime, 1, 4)
        Me.tblpanel.Controls.Add(Me.lblTabelNo, 0, 0)
        Me.tblpanel.Controls.Add(Me.txtTableNo, 1, 0)
        Me.tblpanel.Controls.Add(Me.lblPhoneNo, 0, 1)
        Me.tblpanel.Controls.Add(Me.txtName, 1, 2)
        Me.tblpanel.Controls.Add(Me.txtPhone, 1, 1)
        Me.tblpanel.Location = New System.Drawing.Point(27, 68)
        Me.tblpanel.Name = "tblpanel"
        Me.tblpanel.RowCount = 6
        Me.tblpanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.26361!))
        Me.tblpanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.26343!))
        Me.tblpanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.2648!))
        Me.tblpanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.2648!))
        Me.tblpanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.2648!))
        Me.tblpanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.67855!))
        Me.tblpanel.Size = New System.Drawing.Size(763, 186)
        Me.tblpanel.TabIndex = 0
        Me.tblpanel.Visible = False
        '
        'lblName
        '
        Me.lblName.AttachedTextBoxName = Nothing
        Me.lblName.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblName.ForeColor = System.Drawing.Color.Black
        Me.lblName.Location = New System.Drawing.Point(3, 59)
        Me.lblName.Margin = New System.Windows.Forms.Padding(3)
        Me.lblName.Name = "lblName"
        Me.lblName.Padding = New System.Windows.Forms.Padding(3)
        Me.lblName.Size = New System.Drawing.Size(102, 22)
        Me.lblName.TabIndex = 52
        Me.lblName.Tag = Nothing
        Me.lblName.Text = "Name  :"
        Me.lblName.TextDetached = True
        Me.lblName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel2
        '
        Me.CtrlLabel2.AttachedTextBoxName = Nothing
        Me.CtrlLabel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel2.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel2.Location = New System.Drawing.Point(3, 87)
        Me.CtrlLabel2.Margin = New System.Windows.Forms.Padding(3)
        Me.CtrlLabel2.Name = "CtrlLabel2"
        Me.CtrlLabel2.Padding = New System.Windows.Forms.Padding(3)
        Me.CtrlLabel2.Size = New System.Drawing.Size(102, 22)
        Me.CtrlLabel2.TabIndex = 44
        Me.CtrlLabel2.Tag = Nothing
        Me.CtrlLabel2.Text = "Date :"
        Me.CtrlLabel2.TextDetached = True
        Me.CtrlLabel2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel5
        '
        Me.CtrlLabel5.AttachedTextBoxName = Nothing
        Me.CtrlLabel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel5.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel5.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel5.Location = New System.Drawing.Point(3, 115)
        Me.CtrlLabel5.Margin = New System.Windows.Forms.Padding(3)
        Me.CtrlLabel5.Name = "CtrlLabel5"
        Me.CtrlLabel5.Padding = New System.Windows.Forms.Padding(3)
        Me.CtrlLabel5.Size = New System.Drawing.Size(102, 22)
        Me.CtrlLabel5.TabIndex = 45
        Me.CtrlLabel5.Tag = Nothing
        Me.CtrlLabel5.Text = "Time  :"
        Me.CtrlLabel5.TextDetached = True
        Me.CtrlLabel5.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.CtrlBtnClear)
        Me.Panel1.Controls.Add(Me.cmdSearch)
        Me.Panel1.Location = New System.Drawing.Point(111, 143)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(170, 35)
        Me.Panel1.TabIndex = 47
        '
        'CtrlBtnClear
        '
        Me.CtrlBtnClear.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlBtnClear.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CtrlBtnClear.Location = New System.Drawing.Point(3, 2)
        Me.CtrlBtnClear.MoveToNxtCtrl = Nothing
        Me.CtrlBtnClear.Name = "CtrlBtnClear"
        Me.CtrlBtnClear.SetArticleCode = Nothing
        Me.CtrlBtnClear.SetRowIndex = 0
        Me.CtrlBtnClear.Size = New System.Drawing.Size(76, 32)
        Me.CtrlBtnClear.TabIndex = 6
        Me.CtrlBtnClear.Text = "&Clear"
        Me.CtrlBtnClear.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CtrlBtnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CtrlBtnClear.UseVisualStyleBackColor = True
        Me.CtrlBtnClear.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlBtnClear.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdSearch
        '
        Me.cmdSearch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSearch.Location = New System.Drawing.Point(85, 3)
        Me.cmdSearch.MoveToNxtCtrl = Nothing
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.SetArticleCode = Nothing
        Me.cmdSearch.SetRowIndex = 0
        Me.cmdSearch.Size = New System.Drawing.Size(76, 32)
        Me.cmdSearch.TabIndex = 5
        Me.cmdSearch.Text = "&Search"
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdSearch.UseVisualStyleBackColor = True
        Me.cmdSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.cmdSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtDate
        '
        Me.txtDate.AutoSize = False
        Me.txtDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage"), System.Drawing.Image)
        '
        '
        '
        Me.txtDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.txtDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.txtDate.DisplayFormat.EmptyAsNull = False
        Me.txtDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.txtDate.DisplayFormat.Inherit = CType(((C1.Win.C1Input.FormatInfoInheritFlags.NullText Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.txtDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.txtDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.txtDate.EmptyAsNull = True
        Me.txtDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.txtDate.InterceptArrowKeys = False
        Me.txtDate.Location = New System.Drawing.Point(111, 87)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(170, 21)
        Me.txtDate.TabIndex = 3
        Me.txtDate.Tag = Nothing
        Me.txtDate.TrimStart = True
        Me.txtDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        Me.txtDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.txtDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'txtTime
        '
        Me.txtTime.AutoSize = False
        Me.txtTime.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.txtTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTime.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage1"), System.Drawing.Image)
        '
        '
        '
        Me.txtTime.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.txtTime.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.txtTime.DisplayFormat.EmptyAsNull = False
        Me.txtTime.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortTime
        Me.txtTime.DisplayFormat.Inherit = CType(((C1.Win.C1Input.FormatInfoInheritFlags.NullText Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.txtTime.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortTime
        Me.txtTime.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.txtTime.EmptyAsNull = True
        Me.txtTime.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortTime
        Me.txtTime.InterceptArrowKeys = False
        Me.txtTime.Location = New System.Drawing.Point(111, 115)
        Me.txtTime.Name = "txtTime"
        Me.txtTime.Size = New System.Drawing.Size(170, 18)
        Me.txtTime.TabIndex = 4
        Me.txtTime.Tag = Nothing
        Me.txtTime.TrimStart = True
        Me.txtTime.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown
        Me.txtTime.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.txtTime.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'lblTabelNo
        '
        Me.lblTabelNo.AttachedTextBoxName = Nothing
        Me.lblTabelNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTabelNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTabelNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTabelNo.ForeColor = System.Drawing.Color.Black
        Me.lblTabelNo.Location = New System.Drawing.Point(3, 3)
        Me.lblTabelNo.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.lblTabelNo.Name = "lblTabelNo"
        Me.lblTabelNo.Padding = New System.Windows.Forms.Padding(3)
        Me.lblTabelNo.Size = New System.Drawing.Size(102, 22)
        Me.lblTabelNo.TabIndex = 6
        Me.lblTabelNo.Tag = Nothing
        Me.lblTabelNo.Text = "Table Number :"
        Me.lblTabelNo.TextDetached = True
        Me.lblTabelNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtTableNo
        '
        Me.txtTableNo.AutoSize = False
        Me.txtTableNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtTableNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTableNo.Location = New System.Drawing.Point(111, 3)
        Me.txtTableNo.MaximumSize = New System.Drawing.Size(1000, 1000)
        Me.txtTableNo.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtTableNo.MoveToNxtCtrl = Nothing
        Me.txtTableNo.Name = "txtTableNo"
        Me.txtTableNo.Size = New System.Drawing.Size(44, 22)
        Me.txtTableNo.TabIndex = 0
        Me.txtTableNo.Tag = Nothing
        Me.txtTableNo.TextDetached = True
        Me.txtTableNo.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtTableNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblPhoneNo
        '
        Me.lblPhoneNo.AttachedTextBoxName = Nothing
        Me.lblPhoneNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblPhoneNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblPhoneNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPhoneNo.ForeColor = System.Drawing.Color.Black
        Me.lblPhoneNo.Location = New System.Drawing.Point(3, 31)
        Me.lblPhoneNo.Margin = New System.Windows.Forms.Padding(3)
        Me.lblPhoneNo.Name = "lblPhoneNo"
        Me.lblPhoneNo.Padding = New System.Windows.Forms.Padding(3)
        Me.lblPhoneNo.Size = New System.Drawing.Size(102, 22)
        Me.lblPhoneNo.TabIndex = 41
        Me.lblPhoneNo.Tag = Nothing
        Me.lblPhoneNo.Text = "Phone Number :"
        Me.lblPhoneNo.TextDetached = True
        Me.lblPhoneNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtName
        '
        Me.txtName.AutoSize = False
        Me.txtName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Location = New System.Drawing.Point(111, 59)
        Me.txtName.MaximumSize = New System.Drawing.Size(1000, 1000)
        Me.txtName.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtName.MoveToNxtCtrl = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(170, 21)
        Me.txtName.TabIndex = 2
        Me.txtName.Tag = Nothing
        Me.txtName.TextDetached = True
        Me.txtName.Value = ""
        Me.txtName.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtPhone
        '
        Me.txtPhone.AutoSize = False
        Me.txtPhone.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhone.Location = New System.Drawing.Point(111, 31)
        Me.txtPhone.MaximumSize = New System.Drawing.Size(1000, 1000)
        Me.txtPhone.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtPhone.MoveToNxtCtrl = Nothing
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(111, 21)
        Me.txtPhone.TabIndex = 1
        Me.txtPhone.Tag = Nothing
        Me.txtPhone.TextDetached = True
        Me.txtPhone.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtPhone.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CmdShowFilter
        '
        Me.CmdShowFilter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmdShowFilter.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdShowFilter.Location = New System.Drawing.Point(23, 14)
        Me.CmdShowFilter.MoveToNxtCtrl = Nothing
        Me.CmdShowFilter.Name = "CmdShowFilter"
        Me.CmdShowFilter.SetArticleCode = Nothing
        Me.CmdShowFilter.SetRowIndex = 0
        Me.CmdShowFilter.Size = New System.Drawing.Size(26, 21)
        Me.CmdShowFilter.TabIndex = 40
        Me.CmdShowFilter.Text = "+ Filter"
        Me.CmdShowFilter.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdShowFilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CmdShowFilter.UseVisualStyleBackColor = True
        Me.CmdShowFilter.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CmdShowFilter.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'dgMainGrid
        '
        Me.dgMainGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.dgMainGrid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.dgMainGrid.AutoResize = True
        Me.dgMainGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
        Me.dgMainGrid.CellButtonImage = Global.Spectrum.My.Resources.Resources.ok
        Me.dgMainGrid.ColumnInfo = resources.GetString("dgMainGrid.ColumnInfo")
        Me.dgMainGrid.ExtendLastCol = True
        Me.dgMainGrid.Location = New System.Drawing.Point(23, 41)
        Me.dgMainGrid.Name = "dgMainGrid"
        Me.dgMainGrid.Rows.Count = 1
        Me.dgMainGrid.Rows.DefaultSize = 20
        Me.dgMainGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.dgMainGrid.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.dgMainGrid.Size = New System.Drawing.Size(889, 483)
        Me.dgMainGrid.StyleInfo = resources.GetString("dgMainGrid.StyleInfo")
        Me.dgMainGrid.TabIndex = 45
        Me.dgMainGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'lblTicketTitle
        '
        Me.lblTicketTitle.AttachedTextBoxName = Nothing
        Me.lblTicketTitle.AutoSize = True
        Me.lblTicketTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTicketTitle.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTicketTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTicketTitle.ForeColor = System.Drawing.Color.Black
        Me.lblTicketTitle.Location = New System.Drawing.Point(3, 59)
        Me.lblTicketTitle.Name = "lblTicketTitle"
        Me.lblTicketTitle.Padding = New System.Windows.Forms.Padding(3)
        Me.lblTicketTitle.Size = New System.Drawing.Size(92, 21)
        Me.lblTicketTitle.TabIndex = 8
        Me.lblTicketTitle.Tag = Nothing
        Me.lblTicketTitle.Text = "Phone Number :"
        Me.lblTicketTitle.TextDetached = True
        Me.lblTicketTitle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblGrievanceId
        '
        Me.lblGrievanceId.AttachedTextBoxName = Nothing
        Me.lblGrievanceId.AutoSize = True
        Me.lblGrievanceId.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGrievanceId.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGrievanceId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGrievanceId.ForeColor = System.Drawing.Color.Black
        Me.lblGrievanceId.Location = New System.Drawing.Point(3, 27)
        Me.lblGrievanceId.Name = "lblGrievanceId"
        Me.lblGrievanceId.Padding = New System.Windows.Forms.Padding(3)
        Me.lblGrievanceId.Size = New System.Drawing.Size(49, 21)
        Me.lblGrievanceId.TabIndex = 7
        Me.lblGrievanceId.Tag = Nothing
        Me.lblGrievanceId.Text = "Name :"
        Me.lblGrievanceId.TextDetached = True
        Me.lblGrievanceId.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmNSearchReservation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(928, 618)
        Me.Controls.Add(Me.c1Sizer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNSearchReservation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SearchReservation"
        CType(Me.c1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.c1Sizer1.ResumeLayout(False)
        CType(Me.txtPeople, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tblpanel.ResumeLayout(False)
        CType(Me.lblName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTabelNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTableNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPhoneNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPhone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgMainGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTicketTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGrievanceId, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents c1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents tblpanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CtrlLabel5 As Spectrum.CtrlLabel
    Friend WithEvents lblPhoneNo As Spectrum.CtrlLabel
    Friend WithEvents txtPhone As Spectrum.CtrlTextBox
    Friend WithEvents lblTicketTitle As Spectrum.CtrlLabel
    Friend WithEvents lblGrievanceId As Spectrum.CtrlLabel
    Friend WithEvents lblTabelNo As Spectrum.CtrlLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CtrlBtnClear As Spectrum.CtrlBtn
    Friend WithEvents cmdSearch As Spectrum.CtrlBtn
    Friend WithEvents txtDate As Spectrum.ctrlDate
    Friend WithEvents txtTableNo As Spectrum.CtrlTextBox
    Friend WithEvents CmdShowFilter As Spectrum.CtrlBtn
    Friend WithEvents txtTime As Spectrum.ctrlDate
    Friend WithEvents txtPeople As Spectrum.CtrlTextBox
    Friend WithEvents lblName As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel2 As Spectrum.CtrlLabel
    Friend WithEvents dgMainGrid As Spectrum.CtrlGrid
    Friend WithEvents btnCheckIn As Spectrum.CtrlBtn
    Friend WithEvents btnChangeTable As Spectrum.CtrlBtn
    Friend WithEvents btnChangeTime As Spectrum.CtrlBtn
    Friend WithEvents cmdDelete As Spectrum.CtrlBtn
    Friend WithEvents txtName As Spectrum.CtrlTextBox
End Class
