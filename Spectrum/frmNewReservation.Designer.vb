<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewReservation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNewReservation))
        Me.ReservationPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSearch = New Spectrum.CtrlBtn()
        Me.dtpDate = New Spectrum.ctrlDate()
        Me.dtpFromTime = New Spectrum.ctrlDate()
        Me.CtrlLabel6 = New Spectrum.CtrlLabel()
        Me.txtName = New Spectrum.CtrlTextBox()
        Me.CtrlLabel7 = New Spectrum.CtrlLabel()
        Me.CtrlLabel10 = New Spectrum.CtrlLabel()
        Me.txtPeople = New Spectrum.CtrlTextBox()
        Me.CtrlLabel8 = New Spectrum.CtrlLabel()
        Me.txtPhone = New Spectrum.CtrlTextBox()
        Me.lblOldTableNo = New Spectrum.CtrlLabel()
        Me.txtOldTableNo = New Spectrum.CtrlTextBox()
        Me.lblNewTableNo = New Spectrum.CtrlLabel()
        Me.txtNewTableNo = New Spectrum.CtrlTextBox()
        Me.CtrlLabel9 = New Spectrum.CtrlLabel()
        Me.btnBook = New Spectrum.CtrlBtn()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPeople, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPhone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOldTableNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOldTableNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNewTableNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNewTableNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReservationPanel
        '
        Me.ReservationPanel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ReservationPanel.AutoScroll = True
        Me.ReservationPanel.Location = New System.Drawing.Point(6, 74)
        Me.ReservationPanel.Name = "ReservationPanel"
        Me.ReservationPanel.Size = New System.Drawing.Size(1090, 453)
        Me.ReservationPanel.TabIndex = 8
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 14
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.862385!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.633027!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.981651!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.688073!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.614679!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.788991!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.082569!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.862385!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.990826!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.678899!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.678899!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.10092!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.440333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.158187!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.dtpDate, 11, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.dtpFromTime, 13, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel6, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtName, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel7, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel10, 12, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtPeople, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel8, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtPhone, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblOldTableNo, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtOldTableNo, 7, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblNewTableNo, 8, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtNewTableNo, 9, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel9, 10, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(6, 8)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1090, 26)
        Me.TableLayoutPanel1.TabIndex = 28
        '
        'btnSearch
        '
        Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSearch.Location = New System.Drawing.Point(974, 40)
        Me.btnSearch.MoveToNxtCtrl = Nothing
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.SetArticleCode = Nothing
        Me.btnSearch.SetRowIndex = 0
        Me.btnSearch.Size = New System.Drawing.Size(122, 28)
        Me.btnSearch.TabIndex = 7
        Me.btnSearch.Text = "Search Tables"
        Me.btnSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnSearch.UseVisualStyleBackColor = True
        Me.btnSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.btnSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'dtpDate
        '
        Me.dtpDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtpDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage"), System.Drawing.Image)
        '
        '
        '
        Me.dtpDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtpDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtpDate.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dtpDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpDate.EmptyAsNull = True
        Me.dtpDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpDate.InterceptArrowKeys = False
        Me.dtpDate.Location = New System.Drawing.Point(819, 3)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(115, 18)
        Me.dtpDate.TabIndex = 5
        Me.dtpDate.Tag = Nothing
        Me.dtpDate.TrimStart = True
        Me.dtpDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        Me.dtpDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'dtpFromTime
        '
        Me.dtpFromTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtpFromTime.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage1"), System.Drawing.Image)
        '
        '
        '
        Me.dtpFromTime.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtpFromTime.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtpFromTime.DisabledForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dtpFromTime.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.LongTime
        Me.dtpFromTime.DisplayFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpFromTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpFromTime.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.LongTime
        Me.dtpFromTime.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpFromTime.EmptyAsNull = True
        Me.dtpFromTime.FormatType = C1.Win.C1Input.FormatTypeEnum.LongTime
        Me.dtpFromTime.InterceptArrowKeys = False
        Me.dtpFromTime.Location = New System.Drawing.Point(988, 3)
        Me.dtpFromTime.Name = "dtpFromTime"
        Me.dtpFromTime.Size = New System.Drawing.Size(99, 18)
        Me.dtpFromTime.TabIndex = 6
        Me.dtpFromTime.Tag = Nothing
        Me.dtpFromTime.TrimStart = True
        Me.dtpFromTime.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown
        Me.dtpFromTime.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'CtrlLabel6
        '
        Me.CtrlLabel6.AttachedTextBoxName = Nothing
        Me.CtrlLabel6.AutoSize = True
        Me.CtrlLabel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel6.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlLabel6.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel6.Location = New System.Drawing.Point(3, 0)
        Me.CtrlLabel6.Name = "CtrlLabel6"
        Me.CtrlLabel6.Padding = New System.Windows.Forms.Padding(3)
        Me.CtrlLabel6.Size = New System.Drawing.Size(47, 26)
        Me.CtrlLabel6.TabIndex = 9
        Me.CtrlLabel6.Tag = Nothing
        Me.CtrlLabel6.Text = "Name :"
        Me.CtrlLabel6.TextDetached = True
        '
        'txtName
        '
        Me.txtName.AutoSize = False
        Me.txtName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtName.Location = New System.Drawing.Point(56, 3)
        Me.txtName.MinimumSize = New System.Drawing.Size(10, 18)
        Me.txtName.MoveToNxtCtrl = Nothing
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(99, 20)
        Me.txtName.TabIndex = 0
        Me.txtName.Tag = Nothing
        Me.txtName.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel7
        '
        Me.CtrlLabel7.AttachedTextBoxName = Nothing
        Me.CtrlLabel7.AutoSize = True
        Me.CtrlLabel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel7.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlLabel7.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel7.Location = New System.Drawing.Point(161, 0)
        Me.CtrlLabel7.Name = "CtrlLabel7"
        Me.CtrlLabel7.Padding = New System.Windows.Forms.Padding(3)
        Me.CtrlLabel7.Size = New System.Drawing.Size(81, 26)
        Me.CtrlLabel7.TabIndex = 11
        Me.CtrlLabel7.Tag = Nothing
        Me.CtrlLabel7.Text = "No of People :"
        Me.CtrlLabel7.TextDetached = True
        '
        'CtrlLabel10
        '
        Me.CtrlLabel10.AttachedTextBoxName = Nothing
        Me.CtrlLabel10.AutoSize = True
        Me.CtrlLabel10.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel10.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlLabel10.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel10.Location = New System.Drawing.Point(940, 0)
        Me.CtrlLabel10.Name = "CtrlLabel10"
        Me.CtrlLabel10.Padding = New System.Windows.Forms.Padding(3)
        Me.CtrlLabel10.Size = New System.Drawing.Size(42, 26)
        Me.CtrlLabel10.TabIndex = 17
        Me.CtrlLabel10.Tag = Nothing
        Me.CtrlLabel10.Text = "Time :"
        Me.CtrlLabel10.TextDetached = True
        '
        'txtPeople
        '
        Me.txtPeople.AutoSize = False
        Me.txtPeople.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtPeople.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPeople.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPeople.Location = New System.Drawing.Point(248, 3)
        Me.txtPeople.MinimumSize = New System.Drawing.Size(10, 18)
        Me.txtPeople.MoveToNxtCtrl = Nothing
        Me.txtPeople.Name = "txtPeople"
        Me.txtPeople.Size = New System.Drawing.Size(56, 20)
        Me.txtPeople.TabIndex = 1
        Me.txtPeople.Tag = Nothing
        Me.txtPeople.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtPeople.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel8
        '
        Me.CtrlLabel8.AttachedTextBoxName = Nothing
        Me.CtrlLabel8.AutoSize = True
        Me.CtrlLabel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel8.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlLabel8.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel8.Location = New System.Drawing.Point(310, 0)
        Me.CtrlLabel8.Name = "CtrlLabel8"
        Me.CtrlLabel8.Padding = New System.Windows.Forms.Padding(3)
        Me.CtrlLabel8.Size = New System.Drawing.Size(77, 26)
        Me.CtrlLabel8.TabIndex = 13
        Me.CtrlLabel8.Tag = Nothing
        Me.CtrlLabel8.Text = "Phone No. :"
        Me.CtrlLabel8.TextDetached = True
        '
        'txtPhone
        '
        Me.txtPhone.AutoSize = False
        Me.txtPhone.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhone.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPhone.Location = New System.Drawing.Point(393, 3)
        Me.txtPhone.MaxLength = 10
        Me.txtPhone.MinimumSize = New System.Drawing.Size(10, 18)
        Me.txtPhone.MoveToNxtCtrl = Nothing
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(68, 20)
        Me.txtPhone.TabIndex = 2
        Me.txtPhone.Tag = Nothing
        Me.txtPhone.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtPhone.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblOldTableNo
        '
        Me.lblOldTableNo.AttachedTextBoxName = Nothing
        Me.lblOldTableNo.AutoSize = True
        Me.lblOldTableNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblOldTableNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblOldTableNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOldTableNo.ForeColor = System.Drawing.Color.Black
        Me.lblOldTableNo.Location = New System.Drawing.Point(467, 0)
        Me.lblOldTableNo.Name = "lblOldTableNo"
        Me.lblOldTableNo.Padding = New System.Windows.Forms.Padding(3)
        Me.lblOldTableNo.Size = New System.Drawing.Size(93, 26)
        Me.lblOldTableNo.TabIndex = 28
        Me.lblOldTableNo.Tag = Nothing
        Me.lblOldTableNo.Text = "Old Table No. :"
        Me.lblOldTableNo.TextDetached = True
        '
        'txtOldTableNo
        '
        Me.txtOldTableNo.AutoSize = False
        Me.txtOldTableNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtOldTableNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOldTableNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtOldTableNo.Location = New System.Drawing.Point(566, 3)
        Me.txtOldTableNo.MaxLength = 10
        Me.txtOldTableNo.MinimumSize = New System.Drawing.Size(10, 18)
        Me.txtOldTableNo.MoveToNxtCtrl = Nothing
        Me.txtOldTableNo.Name = "txtOldTableNo"
        Me.txtOldTableNo.Size = New System.Drawing.Size(47, 20)
        Me.txtOldTableNo.TabIndex = 3
        Me.txtOldTableNo.Tag = Nothing
        Me.txtOldTableNo.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtOldTableNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblNewTableNo
        '
        Me.lblNewTableNo.AttachedTextBoxName = Nothing
        Me.lblNewTableNo.AutoSize = True
        Me.lblNewTableNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblNewTableNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblNewTableNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblNewTableNo.ForeColor = System.Drawing.Color.Black
        Me.lblNewTableNo.Location = New System.Drawing.Point(619, 0)
        Me.lblNewTableNo.Name = "lblNewTableNo"
        Me.lblNewTableNo.Padding = New System.Windows.Forms.Padding(3)
        Me.lblNewTableNo.Size = New System.Drawing.Size(92, 26)
        Me.lblNewTableNo.TabIndex = 30
        Me.lblNewTableNo.Tag = Nothing
        Me.lblNewTableNo.Text = "New Table No. :"
        Me.lblNewTableNo.TextDetached = True
        '
        'txtNewTableNo
        '
        Me.txtNewTableNo.AutoSize = False
        Me.txtNewTableNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtNewTableNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewTableNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtNewTableNo.Location = New System.Drawing.Point(717, 3)
        Me.txtNewTableNo.MaxLength = 10
        Me.txtNewTableNo.MinimumSize = New System.Drawing.Size(10, 18)
        Me.txtNewTableNo.MoveToNxtCtrl = Nothing
        Me.txtNewTableNo.Name = "txtNewTableNo"
        Me.txtNewTableNo.Size = New System.Drawing.Size(45, 20)
        Me.txtNewTableNo.TabIndex = 4
        Me.txtNewTableNo.Tag = Nothing
        Me.txtNewTableNo.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtNewTableNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel9
        '
        Me.CtrlLabel9.AttachedTextBoxName = Nothing
        Me.CtrlLabel9.AutoSize = True
        Me.CtrlLabel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel9.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlLabel9.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel9.Location = New System.Drawing.Point(768, 0)
        Me.CtrlLabel9.Name = "CtrlLabel9"
        Me.CtrlLabel9.Padding = New System.Windows.Forms.Padding(3)
        Me.CtrlLabel9.Size = New System.Drawing.Size(45, 26)
        Me.CtrlLabel9.TabIndex = 15
        Me.CtrlLabel9.Tag = Nothing
        Me.CtrlLabel9.Text = "Date :"
        Me.CtrlLabel9.TextDetached = True
        '
        'btnBook
        '
        Me.btnBook.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnBook.Location = New System.Drawing.Point(989, 533)
        Me.btnBook.MoveToNxtCtrl = Nothing
        Me.btnBook.Name = "btnBook"
        Me.btnBook.SetArticleCode = Nothing
        Me.btnBook.SetRowIndex = 0
        Me.btnBook.Size = New System.Drawing.Size(107, 37)
        Me.btnBook.TabIndex = 9
        Me.btnBook.Text = "Book"
        Me.btnBook.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnBook.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnBook.UseVisualStyleBackColor = True
        Me.btnBook.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.btnBook.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmNewReservation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1103, 572)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.ReservationPanel)
        Me.Controls.Add(Me.btnBook)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNewReservation"
        Me.Text = "Table Reservation"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.dtpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPeople, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPhone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOldTableNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOldTableNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNewTableNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNewTableNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrlLabel10 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel9 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel8 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel7 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel6 As Spectrum.CtrlLabel
    Friend WithEvents txtName As Spectrum.CtrlTextBox
    Friend WithEvents txtPeople As Spectrum.CtrlTextBox
    Friend WithEvents txtPhone As Spectrum.CtrlTextBox
    Friend WithEvents btnBook As Spectrum.CtrlBtn
    Friend WithEvents dtpDate As Spectrum.ctrlDate
    Friend WithEvents ReservationPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblNewTableNo As Spectrum.CtrlLabel
    Friend WithEvents txtNewTableNo As Spectrum.CtrlTextBox
    Friend WithEvents lblOldTableNo As Spectrum.CtrlLabel
    Friend WithEvents txtOldTableNo As Spectrum.CtrlTextBox
    Friend WithEvents dtpFromTime As Spectrum.ctrlDate
    Friend WithEvents btnSearch As Spectrum.CtrlBtn
End Class
