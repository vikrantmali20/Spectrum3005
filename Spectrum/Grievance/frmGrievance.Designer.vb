<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrievance
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGrievance))
        Me.c1Sizer1 = New C1.Win.C1Sizer.C1Sizer()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cbRaisedbyCMF = New System.Windows.Forms.CheckBox()
        Me.RbBtnCMF = New System.Windows.Forms.RadioButton()
        Me.cbRaisedbyHo = New System.Windows.Forms.CheckBox()
        Me.RdBtnAll = New System.Windows.Forms.RadioButton()
        Me.cbRaisedbyFranchisee = New System.Windows.Forms.CheckBox()
        Me.RdBtnFo = New System.Windows.Forms.RadioButton()
        Me.cbAll = New System.Windows.Forms.CheckBox()
        Me.RdbtnBO = New System.Windows.Forms.RadioButton()
        Me.tblpanel = New System.Windows.Forms.TableLayoutPanel()
        Me.ToDate = New Spectrum.ctrlDate()
        Me.lblDate = New Spectrum.CtrlLabel()
        Me.CtrlFromDate = New Spectrum.ctrlDate()
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        Me.lblDepartment = New Spectrum.CtrlLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CtrlBtnClear = New Spectrum.CtrlBtn()
        Me.cmdSearch = New Spectrum.CtrlBtn()
        Me.CtrlLabel4 = New Spectrum.CtrlLabel()
        Me.CtrlUpdatedOn = New Spectrum.ctrlDate()
        Me.CtrlLabel2 = New Spectrum.CtrlLabel()
        Me.lblGrievanceType = New Spectrum.CtrlLabel()
        Me.cbGrievanceType = New Spectrum.ctrlCombo()
        Me.lblGrievanceId = New Spectrum.CtrlLabel()
        Me.txtGrievanceId = New Spectrum.CtrlTextBox()
        Me.lblTicketTitle = New Spectrum.CtrlLabel()
        Me.txtGrievanceTitle = New Spectrum.CtrlTextBox()
        Me.cbStatus = New Spectrum.ctrlCombo()
        Me.lblStatus = New Spectrum.CtrlLabel()
        Me.CtrlLabel3 = New Spectrum.CtrlLabel()
        Me.CtrlTxtCreatedBy = New Spectrum.CtrlTextBox()
        Me.CtrlLabel5 = New Spectrum.CtrlLabel()
        Me.CtrlTxtUpdatedBy = New Spectrum.CtrlTextBox()
        Me.cbDepartment = New Spectrum.ctrlCombo()
        Me.CbRaisedSite = New Spectrum.ctrlCombo()
        Me.chkselectall = New System.Windows.Forms.CheckBox()
        Me.CmdShowFilter = New Spectrum.CtrlBtn()
        Me.cmdDelete = New Spectrum.CtrlBtn()
        Me.cmdNew = New Spectrum.CtrlBtn()
        Me.dgMainGrid = New Spectrum.CtrlGrid()
        Me.txtRemark = New Spectrum.CtrlTextBox()
        Me.TextBox1 = New Spectrum.CtrlTextBox()
        Me.BirthListCLPtransactionTableAdapter1 = New SpectrumBL.POSDBDataSetTableAdapters.BirthListCLPtransactionTableAdapter()
        CType(Me.c1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.c1Sizer1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.tblpanel.SuspendLayout()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.CtrlLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlUpdatedOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGrievanceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbGrievanceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGrievanceId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGrievanceId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTicketTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGrievanceTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlTxtCreatedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlTxtUpdatedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CbRaisedSite, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgMainGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'c1Sizer1
        '
        Me.c1Sizer1.Controls.Add(Me.Panel2)
        Me.c1Sizer1.Controls.Add(Me.tblpanel)
        Me.c1Sizer1.Controls.Add(Me.chkselectall)
        Me.c1Sizer1.Controls.Add(Me.CmdShowFilter)
        Me.c1Sizer1.Controls.Add(Me.cmdDelete)
        Me.c1Sizer1.Controls.Add(Me.cmdNew)
        Me.c1Sizer1.Controls.Add(Me.dgMainGrid)
        Me.c1Sizer1.GridDefinition = "98.8252569750367:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "99.1828396322778:False:False;"
        Me.c1Sizer1.Location = New System.Drawing.Point(6, 7)
        Me.c1Sizer1.Name = "c1Sizer1"
        Me.c1Sizer1.Size = New System.Drawing.Size(979, 681)
        Me.c1Sizer1.TabIndex = 101
        Me.c1Sizer1.Text = "c1Sizer1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.cbRaisedbyCMF)
        Me.Panel2.Controls.Add(Me.RbBtnCMF)
        Me.Panel2.Controls.Add(Me.cbRaisedbyHo)
        Me.Panel2.Controls.Add(Me.RdBtnAll)
        Me.Panel2.Controls.Add(Me.cbRaisedbyFranchisee)
        Me.Panel2.Controls.Add(Me.RdBtnFo)
        Me.Panel2.Controls.Add(Me.cbAll)
        Me.Panel2.Controls.Add(Me.RdbtnBO)
        Me.Panel2.Location = New System.Drawing.Point(131, 14)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(739, 25)
        Me.Panel2.TabIndex = 45
        '
        'cbRaisedbyCMF
        '
        Me.cbRaisedbyCMF.AutoSize = True
        Me.cbRaisedbyCMF.Location = New System.Drawing.Point(536, 6)
        Me.cbRaisedbyCMF.Name = "cbRaisedbyCMF"
        Me.cbRaisedbyCMF.Size = New System.Drawing.Size(110, 17)
        Me.cbRaisedbyCMF.TabIndex = 49
        Me.cbRaisedbyCMF.Text = "Raised by CMF"
        Me.cbRaisedbyCMF.UseVisualStyleBackColor = True
        '
        'RbBtnCMF
        '
        Me.RbBtnCMF.AutoSize = True
        Me.RbBtnCMF.Location = New System.Drawing.Point(558, 6)
        Me.RbBtnCMF.Name = "RbBtnCMF"
        Me.RbBtnCMF.Size = New System.Drawing.Size(109, 17)
        Me.RbBtnCMF.TabIndex = 46
        Me.RbBtnCMF.TabStop = True
        Me.RbBtnCMF.Text = "Raised by CMF"
        Me.RbBtnCMF.UseVisualStyleBackColor = True
        '
        'cbRaisedbyHo
        '
        Me.cbRaisedbyHo.AutoSize = True
        Me.cbRaisedbyHo.Location = New System.Drawing.Point(337, 6)
        Me.cbRaisedbyHo.Name = "cbRaisedbyHo"
        Me.cbRaisedbyHo.Size = New System.Drawing.Size(103, 17)
        Me.cbRaisedbyHo.TabIndex = 48
        Me.cbRaisedbyHo.Text = "Raised by HO"
        Me.cbRaisedbyHo.UseVisualStyleBackColor = True
        '
        'RdBtnAll
        '
        Me.RdBtnAll.AutoSize = True
        Me.RdBtnAll.Location = New System.Drawing.Point(50, 8)
        Me.RdBtnAll.Name = "RdBtnAll"
        Me.RdBtnAll.Size = New System.Drawing.Size(39, 17)
        Me.RdBtnAll.TabIndex = 45
        Me.RdBtnAll.TabStop = True
        Me.RdBtnAll.Text = "All"
        Me.RdBtnAll.UseVisualStyleBackColor = True
        '
        'cbRaisedbyFranchisee
        '
        Me.cbRaisedbyFranchisee.AutoSize = True
        Me.cbRaisedbyFranchisee.Location = New System.Drawing.Point(135, 6)
        Me.cbRaisedbyFranchisee.Name = "cbRaisedbyFranchisee"
        Me.cbRaisedbyFranchisee.Size = New System.Drawing.Size(147, 17)
        Me.cbRaisedbyFranchisee.TabIndex = 47
        Me.cbRaisedbyFranchisee.Text = "Raised by Franchisee"
        Me.cbRaisedbyFranchisee.UseVisualStyleBackColor = True
        '
        'RdBtnFo
        '
        Me.RdBtnFo.AutoSize = True
        Me.RdBtnFo.Location = New System.Drawing.Point(92, 3)
        Me.RdBtnFo.Name = "RdBtnFo"
        Me.RdBtnFo.Size = New System.Drawing.Size(146, 17)
        Me.RdBtnFo.TabIndex = 43
        Me.RdBtnFo.TabStop = True
        Me.RdBtnFo.Text = "Raised by Franchisee"
        Me.RdBtnFo.UseVisualStyleBackColor = True
        '
        'cbAll
        '
        Me.cbAll.AutoSize = True
        Me.cbAll.Location = New System.Drawing.Point(6, 6)
        Me.cbAll.Name = "cbAll"
        Me.cbAll.Size = New System.Drawing.Size(40, 17)
        Me.cbAll.TabIndex = 46
        Me.cbAll.Text = "All"
        Me.cbAll.UseVisualStyleBackColor = True
        '
        'RdbtnBO
        '
        Me.RdbtnBO.AutoSize = True
        Me.RdbtnBO.Location = New System.Drawing.Point(361, 4)
        Me.RdbtnBO.Name = "RdbtnBO"
        Me.RdbtnBO.Size = New System.Drawing.Size(102, 17)
        Me.RdbtnBO.TabIndex = 44
        Me.RdbtnBO.TabStop = True
        Me.RdbtnBO.Text = "Raised by HO"
        Me.RdbtnBO.UseVisualStyleBackColor = True
        '
        'tblpanel
        '
        Me.tblpanel.ColumnCount = 5
        Me.tblpanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblpanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblpanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 143.0!))
        Me.tblpanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125.0!))
        Me.tblpanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblpanel.Controls.Add(Me.ToDate, 3, 0)
        Me.tblpanel.Controls.Add(Me.lblDate, 2, 0)
        Me.tblpanel.Controls.Add(Me.CtrlFromDate, 1, 0)
        Me.tblpanel.Controls.Add(Me.CtrlLabel1, 0, 0)
        Me.tblpanel.Controls.Add(Me.lblDepartment, 0, 1)
        Me.tblpanel.Controls.Add(Me.Panel1, 3, 6)
        Me.tblpanel.Controls.Add(Me.CtrlLabel4, 0, 5)
        Me.tblpanel.Controls.Add(Me.CtrlUpdatedOn, 1, 5)
        Me.tblpanel.Controls.Add(Me.CtrlLabel2, 2, 1)
        Me.tblpanel.Controls.Add(Me.lblGrievanceType, 0, 2)
        Me.tblpanel.Controls.Add(Me.cbGrievanceType, 1, 2)
        Me.tblpanel.Controls.Add(Me.lblGrievanceId, 0, 3)
        Me.tblpanel.Controls.Add(Me.txtGrievanceId, 1, 3)
        Me.tblpanel.Controls.Add(Me.lblTicketTitle, 2, 2)
        Me.tblpanel.Controls.Add(Me.txtGrievanceTitle, 3, 2)
        Me.tblpanel.Controls.Add(Me.cbStatus, 3, 3)
        Me.tblpanel.Controls.Add(Me.lblStatus, 2, 3)
        Me.tblpanel.Controls.Add(Me.CtrlLabel3, 0, 4)
        Me.tblpanel.Controls.Add(Me.CtrlTxtCreatedBy, 1, 4)
        Me.tblpanel.Controls.Add(Me.CtrlLabel5, 2, 4)
        Me.tblpanel.Controls.Add(Me.CtrlTxtUpdatedBy, 3, 4)
        Me.tblpanel.Controls.Add(Me.cbDepartment, 1, 1)
        Me.tblpanel.Controls.Add(Me.CbRaisedSite, 3, 1)
        Me.tblpanel.Location = New System.Drawing.Point(40, 43)
        Me.tblpanel.Name = "tblpanel"
        Me.tblpanel.RowCount = 7
        Me.tblpanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.81481!))
        Me.tblpanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.03704!))
        Me.tblpanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.57407!))
        Me.tblpanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.07729!))
        Me.tblpanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.07729!))
        Me.tblpanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.56039!))
        Me.tblpanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.05314!))
        Me.tblpanel.Size = New System.Drawing.Size(605, 207)
        Me.tblpanel.TabIndex = 0
        Me.tblpanel.Visible = False
        '
        'ToDate
        '
        Me.ToDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.ToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ToDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage"), System.Drawing.Image)
        '
        '
        '
        Me.ToDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.ToDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.ToDate.DisplayFormat.EmptyAsNull = False
        Me.ToDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.ToDate.DisplayFormat.Inherit = CType(((C1.Win.C1Input.FormatInfoInheritFlags.NullText Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.ToDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.ToDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.ToDate.EmptyAsNull = True
        Me.ToDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.ToDate.InterceptArrowKeys = False
        Me.ToDate.Location = New System.Drawing.Point(462, 3)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.Size = New System.Drawing.Size(119, 19)
        Me.ToDate.TabIndex = 14
        Me.ToDate.Tag = Nothing
        Me.ToDate.TrimStart = True
        Me.ToDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        Me.ToDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.ToDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'lblDate
        '
        Me.lblDate.AttachedTextBoxName = Nothing
        Me.lblDate.AutoSize = True
        Me.lblDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDate.ForeColor = System.Drawing.Color.Black
        Me.lblDate.Location = New System.Drawing.Point(319, 0)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(62, 15)
        Me.lblDate.TabIndex = 6
        Me.lblDate.Tag = Nothing
        Me.lblDate.Text = "To Date :"
        Me.lblDate.TextDetached = True
        Me.lblDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlFromDate
        '
        Me.CtrlFromDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.CtrlFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlFromDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage1"), System.Drawing.Image)
        '
        '
        '
        Me.CtrlFromDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.CtrlFromDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.CtrlFromDate.DisplayFormat.EmptyAsNull = False
        Me.CtrlFromDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.CtrlFromDate.DisplayFormat.Inherit = CType(((C1.Win.C1Input.FormatInfoInheritFlags.NullText Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.CtrlFromDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.CtrlFromDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.CtrlFromDate.EmptyAsNull = True
        Me.CtrlFromDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.CtrlFromDate.InterceptArrowKeys = False
        Me.CtrlFromDate.Location = New System.Drawing.Point(161, 3)
        Me.CtrlFromDate.Name = "CtrlFromDate"
        Me.CtrlFromDate.Size = New System.Drawing.Size(118, 19)
        Me.CtrlFromDate.TabIndex = 38
        Me.CtrlFromDate.Tag = Nothing
        Me.CtrlFromDate.TrimStart = True
        Me.CtrlFromDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        Me.CtrlFromDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.CtrlFromDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'CtrlLabel1
        '
        Me.CtrlLabel1.AttachedTextBoxName = Nothing
        Me.CtrlLabel1.AutoSize = True
        Me.CtrlLabel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel1.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel1.Location = New System.Drawing.Point(3, 0)
        Me.CtrlLabel1.Name = "CtrlLabel1"
        Me.CtrlLabel1.Size = New System.Drawing.Size(78, 15)
        Me.CtrlLabel1.TabIndex = 37
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.Text = "From Date :"
        Me.CtrlLabel1.TextDetached = True
        Me.CtrlLabel1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblDepartment
        '
        Me.lblDepartment.AttachedTextBoxName = Nothing
        Me.lblDepartment.AutoSize = True
        Me.lblDepartment.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDepartment.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDepartment.ForeColor = System.Drawing.Color.Black
        Me.lblDepartment.Location = New System.Drawing.Point(3, 30)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(145, 15)
        Me.lblDepartment.TabIndex = 31
        Me.lblDepartment.Tag = Nothing
        Me.lblDepartment.Text = "Raised To Department :"
        Me.lblDepartment.TextDetached = True
        Me.lblDepartment.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.CtrlBtnClear)
        Me.Panel1.Controls.Add(Me.cmdSearch)
        Me.Panel1.Location = New System.Drawing.Point(462, 153)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(119, 27)
        Me.Panel1.TabIndex = 47
        '
        'CtrlBtnClear
        '
        Me.CtrlBtnClear.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlBtnClear.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CtrlBtnClear.Location = New System.Drawing.Point(-4, 3)
        Me.CtrlBtnClear.MoveToNxtCtrl = Nothing
        Me.CtrlBtnClear.Name = "CtrlBtnClear"
        Me.CtrlBtnClear.SetArticleCode = Nothing
        Me.CtrlBtnClear.SetRowIndex = 0
        Me.CtrlBtnClear.Size = New System.Drawing.Size(57, 27)
        Me.CtrlBtnClear.TabIndex = 36
        Me.CtrlBtnClear.Text = "&Clear"
        Me.CtrlBtnClear.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CtrlBtnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CtrlBtnClear.UseVisualStyleBackColor = True
        Me.CtrlBtnClear.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdSearch
        '
        Me.cmdSearch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSearch.Location = New System.Drawing.Point(59, 3)
        Me.cmdSearch.MoveToNxtCtrl = Nothing
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.SetArticleCode = Nothing
        Me.cmdSearch.SetRowIndex = 0
        Me.cmdSearch.Size = New System.Drawing.Size(57, 27)
        Me.cmdSearch.TabIndex = 36
        Me.cmdSearch.Text = "&Search"
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdSearch.UseVisualStyleBackColor = True
        Me.cmdSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel4
        '
        Me.CtrlLabel4.AttachedTextBoxName = Nothing
        Me.CtrlLabel4.AutoSize = True
        Me.CtrlLabel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel4.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel4.Location = New System.Drawing.Point(3, 125)
        Me.CtrlLabel4.Name = "CtrlLabel4"
        Me.CtrlLabel4.Size = New System.Drawing.Size(85, 15)
        Me.CtrlLabel4.TabIndex = 43
        Me.CtrlLabel4.Tag = Nothing
        Me.CtrlLabel4.Text = "Updated On :"
        Me.CtrlLabel4.TextDetached = True
        Me.CtrlLabel4.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlUpdatedOn
        '
        Me.CtrlUpdatedOn.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.CtrlUpdatedOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlUpdatedOn.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage2"), System.Drawing.Image)
        '
        '
        '
        Me.CtrlUpdatedOn.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.CtrlUpdatedOn.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.CtrlUpdatedOn.DisplayFormat.EmptyAsNull = False
        Me.CtrlUpdatedOn.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.CtrlUpdatedOn.DisplayFormat.Inherit = CType(((C1.Win.C1Input.FormatInfoInheritFlags.NullText Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.CtrlUpdatedOn.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.CtrlUpdatedOn.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.CtrlUpdatedOn.EmptyAsNull = True
        Me.CtrlUpdatedOn.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.CtrlUpdatedOn.InterceptArrowKeys = False
        Me.CtrlUpdatedOn.Location = New System.Drawing.Point(161, 128)
        Me.CtrlUpdatedOn.Name = "CtrlUpdatedOn"
        Me.CtrlUpdatedOn.Size = New System.Drawing.Size(118, 19)
        Me.CtrlUpdatedOn.TabIndex = 44
        Me.CtrlUpdatedOn.Tag = Nothing
        Me.CtrlUpdatedOn.TrimStart = True
        Me.CtrlUpdatedOn.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        Me.CtrlUpdatedOn.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.CtrlUpdatedOn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'CtrlLabel2
        '
        Me.CtrlLabel2.AttachedTextBoxName = Nothing
        Me.CtrlLabel2.AutoSize = True
        Me.CtrlLabel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel2.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel2.Location = New System.Drawing.Point(319, 30)
        Me.CtrlLabel2.Name = "CtrlLabel2"
        Me.CtrlLabel2.Size = New System.Drawing.Size(99, 15)
        Me.CtrlLabel2.TabIndex = 48
        Me.CtrlLabel2.Tag = Nothing
        Me.CtrlLabel2.Text = "Raised To Site :"
        Me.CtrlLabel2.TextDetached = True
        Me.CtrlLabel2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblGrievanceType
        '
        Me.lblGrievanceType.AttachedTextBoxName = Nothing
        Me.lblGrievanceType.AutoSize = True
        Me.lblGrievanceType.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGrievanceType.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGrievanceType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGrievanceType.ForeColor = System.Drawing.Color.Black
        Me.lblGrievanceType.Location = New System.Drawing.Point(3, 54)
        Me.lblGrievanceType.Name = "lblGrievanceType"
        Me.lblGrievanceType.Size = New System.Drawing.Size(83, 15)
        Me.lblGrievanceType.TabIndex = 30
        Me.lblGrievanceType.Tag = Nothing
        Me.lblGrievanceType.Text = "Ticket Type :"
        Me.lblGrievanceType.TextDetached = True
        Me.lblGrievanceType.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cbGrievanceType
        '
        Me.cbGrievanceType.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cbGrievanceType.AutoCompletion = True
        Me.cbGrievanceType.AutoDropDown = True
        Me.cbGrievanceType.Caption = ""
        Me.cbGrievanceType.CaptionHeight = 17
        Me.cbGrievanceType.CaptionVisible = False
        Me.cbGrievanceType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cbGrievanceType.ColumnCaptionHeight = 17
        Me.cbGrievanceType.ColumnFooterHeight = 17
        Me.cbGrievanceType.ColumnHeaders = False
        Me.cbGrievanceType.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList
        Me.cbGrievanceType.ContentHeight = 15
        Me.cbGrievanceType.ctrlTextDbColumn = ""
        Me.cbGrievanceType.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cbGrievanceType.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cbGrievanceType.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbGrievanceType.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cbGrievanceType.EditorHeight = 15
        Me.cbGrievanceType.Images.Add(CType(resources.GetObject("cbGrievanceType.Images"), System.Drawing.Image))
        Me.cbGrievanceType.ItemHeight = 15
        Me.cbGrievanceType.Location = New System.Drawing.Point(161, 57)
        Me.cbGrievanceType.MatchEntryTimeout = CType(2000, Long)
        Me.cbGrievanceType.MaxDropDownItems = CType(5, Short)
        Me.cbGrievanceType.MaxLength = 32767
        Me.cbGrievanceType.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cbGrievanceType.MoveToNxtCtrl = Nothing
        Me.cbGrievanceType.Name = "cbGrievanceType"
        Me.cbGrievanceType.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cbGrievanceType.Size = New System.Drawing.Size(118, 21)
        Me.cbGrievanceType.strSelectStmt = ""
        Me.cbGrievanceType.TabIndex = 33
        Me.cbGrievanceType.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cbGrievanceType.PropBag = resources.GetString("cbGrievanceType.PropBag")
        '
        'lblGrievanceId
        '
        Me.lblGrievanceId.AttachedTextBoxName = Nothing
        Me.lblGrievanceId.AutoSize = True
        Me.lblGrievanceId.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGrievanceId.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGrievanceId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGrievanceId.ForeColor = System.Drawing.Color.Black
        Me.lblGrievanceId.Location = New System.Drawing.Point(3, 77)
        Me.lblGrievanceId.Name = "lblGrievanceId"
        Me.lblGrievanceId.Size = New System.Drawing.Size(70, 15)
        Me.lblGrievanceId.TabIndex = 7
        Me.lblGrievanceId.Tag = Nothing
        Me.lblGrievanceId.Text = "Ticket ID :"
        Me.lblGrievanceId.TextDetached = True
        Me.lblGrievanceId.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtGrievanceId
        '
        Me.txtGrievanceId.AutoSize = False
        Me.txtGrievanceId.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtGrievanceId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGrievanceId.Location = New System.Drawing.Point(161, 80)
        Me.txtGrievanceId.MaximumSize = New System.Drawing.Size(1000, 1000)
        Me.txtGrievanceId.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtGrievanceId.MoveToNxtCtrl = Nothing
        Me.txtGrievanceId.Name = "txtGrievanceId"
        Me.txtGrievanceId.Size = New System.Drawing.Size(119, 21)
        Me.txtGrievanceId.TabIndex = 28
        Me.txtGrievanceId.Tag = Nothing
        Me.txtGrievanceId.TextDetached = True
        Me.txtGrievanceId.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtGrievanceId.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblTicketTitle
        '
        Me.lblTicketTitle.AttachedTextBoxName = Nothing
        Me.lblTicketTitle.AutoSize = True
        Me.lblTicketTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTicketTitle.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTicketTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTicketTitle.ForeColor = System.Drawing.Color.Black
        Me.lblTicketTitle.Location = New System.Drawing.Point(319, 54)
        Me.lblTicketTitle.Name = "lblTicketTitle"
        Me.lblTicketTitle.Size = New System.Drawing.Size(80, 15)
        Me.lblTicketTitle.TabIndex = 8
        Me.lblTicketTitle.Tag = Nothing
        Me.lblTicketTitle.Text = "Ticket Title :"
        Me.lblTicketTitle.TextDetached = True
        Me.lblTicketTitle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtGrievanceTitle
        '
        Me.txtGrievanceTitle.AutoSize = False
        Me.txtGrievanceTitle.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtGrievanceTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGrievanceTitle.Location = New System.Drawing.Point(462, 57)
        Me.txtGrievanceTitle.MaximumSize = New System.Drawing.Size(1000, 1000)
        Me.txtGrievanceTitle.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtGrievanceTitle.MoveToNxtCtrl = Nothing
        Me.txtGrievanceTitle.Name = "txtGrievanceTitle"
        Me.txtGrievanceTitle.Size = New System.Drawing.Size(119, 21)
        Me.txtGrievanceTitle.TabIndex = 29
        Me.txtGrievanceTitle.Tag = Nothing
        Me.txtGrievanceTitle.TextDetached = True
        Me.txtGrievanceTitle.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtGrievanceTitle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cbStatus
        '
        Me.cbStatus.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cbStatus.AutoCompletion = True
        Me.cbStatus.AutoDropDown = True
        Me.cbStatus.Caption = ""
        Me.cbStatus.CaptionHeight = 17
        Me.cbStatus.CaptionVisible = False
        Me.cbStatus.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cbStatus.ColumnCaptionHeight = 17
        Me.cbStatus.ColumnFooterHeight = 17
        Me.cbStatus.ColumnHeaders = False
        Me.cbStatus.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList
        Me.cbStatus.ContentHeight = 15
        Me.cbStatus.ctrlTextDbColumn = ""
        Me.cbStatus.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cbStatus.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cbStatus.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbStatus.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cbStatus.EditorHeight = 15
        Me.cbStatus.Images.Add(CType(resources.GetObject("cbStatus.Images"), System.Drawing.Image))
        Me.cbStatus.ItemHeight = 15
        Me.cbStatus.Location = New System.Drawing.Point(462, 80)
        Me.cbStatus.MatchEntryTimeout = CType(2000, Long)
        Me.cbStatus.MaxDropDownItems = CType(5, Short)
        Me.cbStatus.MaxLength = 32767
        Me.cbStatus.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cbStatus.MoveToNxtCtrl = Nothing
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cbStatus.Size = New System.Drawing.Size(118, 21)
        Me.cbStatus.strSelectStmt = ""
        Me.cbStatus.TabIndex = 35
        Me.cbStatus.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cbStatus.PropBag = resources.GetString("cbStatus.PropBag")
        '
        'lblStatus
        '
        Me.lblStatus.AttachedTextBoxName = Nothing
        Me.lblStatus.AutoSize = True
        Me.lblStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblStatus.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatus.ForeColor = System.Drawing.Color.Black
        Me.lblStatus.Location = New System.Drawing.Point(319, 77)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(54, 15)
        Me.lblStatus.TabIndex = 32
        Me.lblStatus.Tag = Nothing
        Me.lblStatus.Text = "Status :"
        Me.lblStatus.TextDetached = True
        Me.lblStatus.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel3
        '
        Me.CtrlLabel3.AttachedTextBoxName = Nothing
        Me.CtrlLabel3.AutoSize = True
        Me.CtrlLabel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel3.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel3.Location = New System.Drawing.Point(3, 101)
        Me.CtrlLabel3.Name = "CtrlLabel3"
        Me.CtrlLabel3.Size = New System.Drawing.Size(83, 15)
        Me.CtrlLabel3.TabIndex = 41
        Me.CtrlLabel3.Tag = Nothing
        Me.CtrlLabel3.Text = "Created By :"
        Me.CtrlLabel3.TextDetached = True
        Me.CtrlLabel3.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlTxtCreatedBy
        '
        Me.CtrlTxtCreatedBy.AutoSize = False
        Me.CtrlTxtCreatedBy.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.CtrlTxtCreatedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlTxtCreatedBy.Location = New System.Drawing.Point(161, 104)
        Me.CtrlTxtCreatedBy.MaximumSize = New System.Drawing.Size(1000, 1000)
        Me.CtrlTxtCreatedBy.MinimumSize = New System.Drawing.Size(10, 21)
        Me.CtrlTxtCreatedBy.MoveToNxtCtrl = Nothing
        Me.CtrlTxtCreatedBy.Name = "CtrlTxtCreatedBy"
        Me.CtrlTxtCreatedBy.Size = New System.Drawing.Size(119, 21)
        Me.CtrlTxtCreatedBy.TabIndex = 42
        Me.CtrlTxtCreatedBy.Tag = Nothing
        Me.CtrlTxtCreatedBy.TextDetached = True
        Me.CtrlTxtCreatedBy.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlTxtCreatedBy.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel5
        '
        Me.CtrlLabel5.AttachedTextBoxName = Nothing
        Me.CtrlLabel5.AutoSize = True
        Me.CtrlLabel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel5.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel5.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel5.Location = New System.Drawing.Point(319, 101)
        Me.CtrlLabel5.Name = "CtrlLabel5"
        Me.CtrlLabel5.Size = New System.Drawing.Size(84, 15)
        Me.CtrlLabel5.TabIndex = 45
        Me.CtrlLabel5.Tag = Nothing
        Me.CtrlLabel5.Text = "Updated By :"
        Me.CtrlLabel5.TextDetached = True
        Me.CtrlLabel5.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlTxtUpdatedBy
        '
        Me.CtrlTxtUpdatedBy.AutoSize = False
        Me.CtrlTxtUpdatedBy.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.CtrlTxtUpdatedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlTxtUpdatedBy.Location = New System.Drawing.Point(462, 104)
        Me.CtrlTxtUpdatedBy.MaximumSize = New System.Drawing.Size(1000, 1000)
        Me.CtrlTxtUpdatedBy.MinimumSize = New System.Drawing.Size(10, 21)
        Me.CtrlTxtUpdatedBy.MoveToNxtCtrl = Nothing
        Me.CtrlTxtUpdatedBy.Name = "CtrlTxtUpdatedBy"
        Me.CtrlTxtUpdatedBy.Size = New System.Drawing.Size(119, 21)
        Me.CtrlTxtUpdatedBy.TabIndex = 46
        Me.CtrlTxtUpdatedBy.Tag = Nothing
        Me.CtrlTxtUpdatedBy.TextDetached = True
        Me.CtrlTxtUpdatedBy.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlTxtUpdatedBy.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cbDepartment
        '
        Me.cbDepartment.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cbDepartment.AutoCompletion = True
        Me.cbDepartment.AutoDropDown = True
        Me.cbDepartment.Caption = ""
        Me.cbDepartment.CaptionHeight = 17
        Me.cbDepartment.CaptionVisible = False
        Me.cbDepartment.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cbDepartment.ColumnCaptionHeight = 17
        Me.cbDepartment.ColumnFooterHeight = 17
        Me.cbDepartment.ColumnHeaders = False
        Me.cbDepartment.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList
        Me.cbDepartment.ContentHeight = 15
        Me.cbDepartment.ctrlTextDbColumn = ""
        Me.cbDepartment.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cbDepartment.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cbDepartment.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbDepartment.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cbDepartment.EditorHeight = 15
        Me.cbDepartment.Images.Add(CType(resources.GetObject("cbDepartment.Images"), System.Drawing.Image))
        Me.cbDepartment.ItemHeight = 15
        Me.cbDepartment.Location = New System.Drawing.Point(161, 33)
        Me.cbDepartment.MatchEntryTimeout = CType(2000, Long)
        Me.cbDepartment.MaxDropDownItems = CType(5, Short)
        Me.cbDepartment.MaxLength = 32767
        Me.cbDepartment.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cbDepartment.MoveToNxtCtrl = Nothing
        Me.cbDepartment.Name = "cbDepartment"
        Me.cbDepartment.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cbDepartment.Size = New System.Drawing.Size(118, 21)
        Me.cbDepartment.strSelectStmt = ""
        Me.cbDepartment.TabIndex = 34
        Me.cbDepartment.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cbDepartment.PropBag = resources.GetString("cbDepartment.PropBag")
        '
        'CbRaisedSite
        '
        Me.CbRaisedSite.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.CbRaisedSite.AutoCompletion = True
        Me.CbRaisedSite.AutoDropDown = True
        Me.CbRaisedSite.Caption = ""
        Me.CbRaisedSite.CaptionHeight = 17
        Me.CbRaisedSite.CaptionVisible = False
        Me.CbRaisedSite.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CbRaisedSite.ColumnCaptionHeight = 17
        Me.CbRaisedSite.ColumnFooterHeight = 17
        Me.CbRaisedSite.ColumnHeaders = False
        Me.CbRaisedSite.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList
        Me.CbRaisedSite.ContentHeight = 15
        Me.CbRaisedSite.ctrlTextDbColumn = ""
        Me.CbRaisedSite.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.CbRaisedSite.EditorBackColor = System.Drawing.SystemColors.Window
        Me.CbRaisedSite.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CbRaisedSite.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.CbRaisedSite.EditorHeight = 15
        Me.CbRaisedSite.Images.Add(CType(resources.GetObject("CbRaisedSite.Images"), System.Drawing.Image))
        Me.CbRaisedSite.ItemHeight = 15
        Me.CbRaisedSite.Location = New System.Drawing.Point(462, 33)
        Me.CbRaisedSite.MatchEntryTimeout = CType(2000, Long)
        Me.CbRaisedSite.MaxDropDownItems = CType(5, Short)
        Me.CbRaisedSite.MaxLength = 32767
        Me.CbRaisedSite.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.CbRaisedSite.MoveToNxtCtrl = Nothing
        Me.CbRaisedSite.Name = "CbRaisedSite"
        Me.CbRaisedSite.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.CbRaisedSite.Size = New System.Drawing.Size(118, 21)
        Me.CbRaisedSite.strSelectStmt = ""
        Me.CbRaisedSite.TabIndex = 49
        Me.CbRaisedSite.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.CbRaisedSite.PropBag = resources.GetString("CbRaisedSite.PropBag")
        '
        'chkselectall
        '
        Me.chkselectall.AccessibleName = "chkBoxSelectALl"
        Me.chkselectall.AutoSize = True
        Me.chkselectall.Location = New System.Drawing.Point(50, 48)
        Me.chkselectall.Name = "chkselectall"
        Me.chkselectall.Size = New System.Drawing.Size(15, 14)
        Me.chkselectall.TabIndex = 42
        Me.chkselectall.UseVisualStyleBackColor = True
        '
        'CmdShowFilter
        '
        Me.CmdShowFilter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmdShowFilter.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdShowFilter.Location = New System.Drawing.Point(40, 14)
        Me.CmdShowFilter.MoveToNxtCtrl = Nothing
        Me.CmdShowFilter.Name = "CmdShowFilter"
        Me.CmdShowFilter.SetArticleCode = Nothing
        Me.CmdShowFilter.SetRowIndex = 0
        Me.CmdShowFilter.Size = New System.Drawing.Size(85, 23)
        Me.CmdShowFilter.TabIndex = 40
        Me.CmdShowFilter.Text = "+ Filter"
        Me.CmdShowFilter.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdShowFilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CmdShowFilter.UseVisualStyleBackColor = True
        Me.CmdShowFilter.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdDelete
        '
        Me.cmdDelete.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdDelete.Location = New System.Drawing.Point(804, 606)
        Me.cmdDelete.MoveToNxtCtrl = Nothing
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.SetArticleCode = Nothing
        Me.cmdDelete.SetRowIndex = 0
        Me.cmdDelete.Size = New System.Drawing.Size(66, 22)
        Me.cmdDelete.TabIndex = 39
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdDelete.UseVisualStyleBackColor = True
        Me.cmdDelete.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdNew
        '
        Me.cmdNew.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdNew.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdNew.Location = New System.Drawing.Point(732, 606)
        Me.cmdNew.MoveToNxtCtrl = Nothing
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.SetArticleCode = Nothing
        Me.cmdNew.SetRowIndex = 0
        Me.cmdNew.Size = New System.Drawing.Size(66, 22)
        Me.cmdNew.TabIndex = 38
        Me.cmdNew.Text = "&Add New"
        Me.cmdNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdNew.UseVisualStyleBackColor = True
        Me.cmdNew.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'dgMainGrid
        '
        Me.dgMainGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.dgMainGrid.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.dgMainGrid.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgMainGrid.AutoResize = True
        Me.dgMainGrid.CellButtonImage = Global.Spectrum.My.Resources.Resources.ok
        Me.dgMainGrid.ColumnInfo = resources.GetString("dgMainGrid.ColumnInfo")
        Me.dgMainGrid.ExtendLastCol = True
        Me.dgMainGrid.Location = New System.Drawing.Point(40, 43)
        Me.dgMainGrid.Name = "dgMainGrid"
        Me.dgMainGrid.Rows.Count = 1
        Me.dgMainGrid.Rows.DefaultSize = 20
        Me.dgMainGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.dgMainGrid.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.dgMainGrid.Size = New System.Drawing.Size(830, 557)
        Me.dgMainGrid.StyleInfo = resources.GetString("dgMainGrid.StyleInfo")
        Me.dgMainGrid.TabIndex = 37
        Me.dgMainGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'txtRemark
        '
        Me.txtRemark.AutoSize = False
        Me.txtRemark.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.txtRemark.Location = New System.Drawing.Point(549, 67)
        Me.txtRemark.MaxLength = 100
        Me.txtRemark.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtRemark.MoveToNxtCtrl = Nothing
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(181, 74)
        Me.txtRemark.TabIndex = 90
        Me.txtRemark.Tag = Nothing
        Me.txtRemark.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        Me.txtRemark.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'TextBox1
        '
        Me.TextBox1.AutoSize = False
        Me.TextBox1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.TextBox1.Location = New System.Drawing.Point(549, 67)
        Me.TextBox1.MaxLength = 100
        Me.TextBox1.MinimumSize = New System.Drawing.Size(10, 21)
        Me.TextBox1.MoveToNxtCtrl = Nothing
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(181, 74)
        Me.TextBox1.TabIndex = 90
        Me.TextBox1.Tag = Nothing
        Me.TextBox1.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        Me.TextBox1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'BirthListCLPtransactionTableAdapter1
        '
        Me.BirthListCLPtransactionTableAdapter1.ClearBeforeFill = True
        '
        'frmGrievance
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(989, 679)
        Me.Controls.Add(Me.c1Sizer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGrievance"
        Me.Text = "Ticketing System"
        Me.TopMost = True
        CType(Me.c1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.c1Sizer1.ResumeLayout(False)
        Me.c1Sizer1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.tblpanel.ResumeLayout(False)
        Me.tblpanel.PerformLayout()
        CType(Me.ToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.CtrlLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlUpdatedOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGrievanceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbGrievanceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGrievanceId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGrievanceId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTicketTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGrievanceTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlTxtCreatedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlTxtUpdatedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CbRaisedSite, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgMainGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents c1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Private WithEvents txtRemark As New Spectrum.CtrlTextBox
    Private WithEvents TextBox1 As New Spectrum.CtrlTextBox
    Friend WithEvents cmdDelete As Spectrum.CtrlBtn
    Friend WithEvents cmdNew As Spectrum.CtrlBtn
    Friend WithEvents dgMainGrid As Spectrum.CtrlGrid
    Friend WithEvents CmdShowFilter As Spectrum.CtrlBtn
    Friend WithEvents chkselectall As System.Windows.Forms.CheckBox
    Friend WithEvents tblpanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CtrlLabel1 As Spectrum.CtrlLabel
    Friend WithEvents CtrlFromDate As Spectrum.ctrlDate
    Friend WithEvents CtrlTxtUpdatedBy As Spectrum.CtrlTextBox
    Friend WithEvents CtrlLabel5 As Spectrum.CtrlLabel
    Friend WithEvents lblDate As Spectrum.CtrlLabel
    Friend WithEvents ToDate As Spectrum.ctrlDate
    Friend WithEvents CtrlTxtCreatedBy As Spectrum.CtrlTextBox
    Friend WithEvents CtrlLabel3 As Spectrum.CtrlLabel
    Friend WithEvents CtrlUpdatedOn As Spectrum.ctrlDate
    Friend WithEvents CtrlLabel4 As Spectrum.CtrlLabel
    Friend WithEvents lblTicketTitle As Spectrum.CtrlLabel
    Friend WithEvents txtGrievanceTitle As Spectrum.CtrlTextBox
    Friend WithEvents lblGrievanceId As Spectrum.CtrlLabel
    Friend WithEvents txtGrievanceId As Spectrum.CtrlTextBox
    Friend WithEvents lblDepartment As Spectrum.CtrlLabel
    Friend WithEvents cbDepartment As Spectrum.ctrlCombo
    Friend WithEvents lblGrievanceType As Spectrum.CtrlLabel
    Friend WithEvents cbGrievanceType As Spectrum.ctrlCombo
    Friend WithEvents lblStatus As Spectrum.CtrlLabel
    Friend WithEvents cbStatus As Spectrum.ctrlCombo
    Friend WithEvents cmdSearch As Spectrum.CtrlBtn
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CtrlBtnClear As Spectrum.CtrlBtn
    Friend WithEvents RdbtnBO As System.Windows.Forms.RadioButton
    Friend WithEvents RdBtnFo As System.Windows.Forms.RadioButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents BirthListCLPtransactionTableAdapter1 As SpectrumBL.POSDBDataSetTableAdapters.BirthListCLPtransactionTableAdapter
    Friend WithEvents RdBtnAll As System.Windows.Forms.RadioButton
    Friend WithEvents RbBtnCMF As System.Windows.Forms.RadioButton
    Friend WithEvents CtrlLabel2 As Spectrum.CtrlLabel
    Friend WithEvents CbRaisedSite As Spectrum.ctrlCombo
    Friend WithEvents cbRaisedbyCMF As System.Windows.Forms.CheckBox
    Friend WithEvents cbRaisedbyHo As System.Windows.Forms.CheckBox
    Friend WithEvents cbRaisedbyFranchisee As System.Windows.Forms.CheckBox
    Friend WithEvents cbAll As System.Windows.Forms.CheckBox
End Class
