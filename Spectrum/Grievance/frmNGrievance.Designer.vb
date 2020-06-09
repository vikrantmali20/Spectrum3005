<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNGrievance
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNGrievance))
        Me.c1Sizer1 = New C1.Win.C1Sizer.C1Sizer()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblStatus = New Spectrum.CtrlLabel()
        Me.cbStatus = New Spectrum.ctrlCombo()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.CtrlLabel6 = New Spectrum.CtrlLabel()
        Me.lblRaisedFromSiteOrdept = New Spectrum.CtrlLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.CtrlLabel3 = New Spectrum.CtrlLabel()
        Me.RbCCDept = New System.Windows.Forms.RadioButton()
        Me.ListViewShowCCDept = New System.Windows.Forms.ListBox()
        Me.RbCCSite = New System.Windows.Forms.RadioButton()
        Me.ChkListCCDept = New System.Windows.Forms.CheckedListBox()
        Me.CtrlLabel5 = New Spectrum.CtrlLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CtrlLabel4 = New Spectrum.CtrlLabel()
        Me.ChkListRaisedDept = New System.Windows.Forms.CheckedListBox()
        Me.ListViewShowRaisedDept = New System.Windows.Forms.ListBox()
        Me.CtrlLabel2 = New Spectrum.CtrlLabel()
        Me.RbRaisedDept = New System.Windows.Forms.RadioButton()
        Me.RbRaisedSite = New System.Windows.Forms.RadioButton()
        Me.cbDepartment = New Spectrum.ctrlCombo()
        Me.CtrlLblSMSNo = New Spectrum.CtrlLabel()
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        Me.lblId = New Spectrum.CtrlLabel()
        Me.lblFile = New Spectrum.CtrlLabel()
        Me.dgGrievanceHistoryGrid = New Spectrum.CtrlGrid()
        Me.RdDesc = New System.Windows.Forms.RadioButton()
        Me.RdAsc = New System.Windows.Forms.RadioButton()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdSubmit = New Spectrum.CtrlBtn()
        Me.cmdCancel = New Spectrum.CtrlBtn()
        Me.CtrlLabelUpdatedOn = New Spectrum.CtrlLabel()
        Me.CtrlLabelUpdatedBy = New Spectrum.CtrlLabel()
        Me.CtrlLabelCreatedOn = New Spectrum.CtrlLabel()
        Me.CtrlLabelCreatedBy = New Spectrum.CtrlLabel()
        Me.CtrlUpdatedOn = New Spectrum.CtrlLabel()
        Me.CtrlUpdatedBy = New Spectrum.CtrlLabel()
        Me.CtrlCreatedOn = New Spectrum.CtrlLabel()
        Me.CtrlCreatedBy = New Spectrum.CtrlLabel()
        Me.txtGrievanceDetail = New Spectrum.CtrlTextBox()
        Me.lblGrievanceId = New Spectrum.CtrlLabel()
        Me.fpRemarks = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblGrievanceDesc = New Spectrum.CtrlLabel()
        Me.lblGrievanceTitle = New Spectrum.CtrlLabel()
        Me.txtGrievanceTitle = New Spectrum.CtrlTextBox()
        Me.lblGrievanceType = New Spectrum.CtrlLabel()
        Me.cbGrievanceType = New Spectrum.ctrlCombo()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        CType(Me.c1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.c1Sizer1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.CtrlLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRaisedFromSiteOrdept, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.CtrlLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLblSMSNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgGrievanceHistoryGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.CtrlLabelUpdatedOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabelUpdatedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabelCreatedOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabelCreatedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlUpdatedOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlUpdatedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlCreatedOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlCreatedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGrievanceDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGrievanceId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGrievanceDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGrievanceTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGrievanceTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGrievanceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbGrievanceType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'c1Sizer1
        '
        Me.c1Sizer1.Controls.Add(Me.Panel4)
        Me.c1Sizer1.Controls.Add(Me.Panel3)
        Me.c1Sizer1.Controls.Add(Me.Panel2)
        Me.c1Sizer1.Controls.Add(Me.Panel1)
        Me.c1Sizer1.Controls.Add(Me.CtrlLblSMSNo)
        Me.c1Sizer1.Controls.Add(Me.CtrlLabel1)
        Me.c1Sizer1.Controls.Add(Me.lblId)
        Me.c1Sizer1.Controls.Add(Me.lblFile)
        Me.c1Sizer1.Controls.Add(Me.dgGrievanceHistoryGrid)
        Me.c1Sizer1.Controls.Add(Me.RdDesc)
        Me.c1Sizer1.Controls.Add(Me.RdAsc)
        Me.c1Sizer1.Controls.Add(Me.TableLayoutPanel1)
        Me.c1Sizer1.Controls.Add(Me.CtrlLabelUpdatedOn)
        Me.c1Sizer1.Controls.Add(Me.CtrlLabelUpdatedBy)
        Me.c1Sizer1.Controls.Add(Me.CtrlLabelCreatedOn)
        Me.c1Sizer1.Controls.Add(Me.CtrlLabelCreatedBy)
        Me.c1Sizer1.Controls.Add(Me.CtrlUpdatedOn)
        Me.c1Sizer1.Controls.Add(Me.CtrlUpdatedBy)
        Me.c1Sizer1.Controls.Add(Me.CtrlCreatedOn)
        Me.c1Sizer1.Controls.Add(Me.CtrlCreatedBy)
        Me.c1Sizer1.Controls.Add(Me.txtGrievanceDetail)
        Me.c1Sizer1.Controls.Add(Me.lblGrievanceId)
        Me.c1Sizer1.Controls.Add(Me.fpRemarks)
        Me.c1Sizer1.Controls.Add(Me.lblGrievanceDesc)
        Me.c1Sizer1.Controls.Add(Me.lblGrievanceTitle)
        Me.c1Sizer1.Controls.Add(Me.txtGrievanceTitle)
        Me.c1Sizer1.Controls.Add(Me.lblGrievanceType)
        Me.c1Sizer1.Controls.Add(Me.cbGrievanceType)
        Me.c1Sizer1.GridDefinition = resources.GetString("c1Sizer1.GridDefinition")
        Me.c1Sizer1.Location = New System.Drawing.Point(3, 2)
        Me.c1Sizer1.Name = "c1Sizer1"
        Me.c1Sizer1.Size = New System.Drawing.Size(832, 622)
        Me.c1Sizer1.TabIndex = 101
        Me.c1Sizer1.Text = "c1Sizer1"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.lblStatus)
        Me.Panel4.Controls.Add(Me.cbStatus)
        Me.Panel4.Location = New System.Drawing.Point(244, 44)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(228, 16)
        Me.Panel4.TabIndex = 86
        '
        'lblStatus
        '
        Me.lblStatus.AttachedTextBoxName = Nothing
        Me.lblStatus.AutoSize = True
        Me.lblStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblStatus.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatus.ForeColor = System.Drawing.Color.Black
        Me.lblStatus.Location = New System.Drawing.Point(0, -1)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(54, 15)
        Me.lblStatus.TabIndex = 22
        Me.lblStatus.Tag = Nothing
        Me.lblStatus.Text = "Status :"
        Me.lblStatus.TextDetached = True
        Me.lblStatus.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
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
        Me.cbStatus.Location = New System.Drawing.Point(60, 0)
        Me.cbStatus.MatchEntryTimeout = CType(2000, Long)
        Me.cbStatus.MaxDropDownItems = CType(5, Short)
        Me.cbStatus.MaxLength = 32767
        Me.cbStatus.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cbStatus.MoveToNxtCtrl = Nothing
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cbStatus.Size = New System.Drawing.Size(124, 21)
        Me.cbStatus.strSelectStmt = ""
        Me.cbStatus.TabIndex = 2
        Me.cbStatus.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cbStatus.PropBag = resources.GetString("cbStatus.PropBag")
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.CtrlLabel6)
        Me.Panel3.Controls.Add(Me.lblRaisedFromSiteOrdept)
        Me.Panel3.Location = New System.Drawing.Point(244, 21)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(333, 19)
        Me.Panel3.TabIndex = 85
        '
        'CtrlLabel6
        '
        Me.CtrlLabel6.AttachedTextBoxName = Nothing
        Me.CtrlLabel6.AutoSize = True
        Me.CtrlLabel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel6.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel6.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel6.Location = New System.Drawing.Point(3, 1)
        Me.CtrlLabel6.Name = "CtrlLabel6"
        Me.CtrlLabel6.Size = New System.Drawing.Size(147, 15)
        Me.CtrlLabel6.TabIndex = 83
        Me.CtrlLabel6.Tag = Nothing
        Me.CtrlLabel6.Text = "Raised From Site/Dept :"
        Me.CtrlLabel6.TextDetached = True
        Me.CtrlLabel6.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblRaisedFromSiteOrdept
        '
        Me.lblRaisedFromSiteOrdept.AttachedTextBoxName = Nothing
        Me.lblRaisedFromSiteOrdept.AutoSize = True
        Me.lblRaisedFromSiteOrdept.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblRaisedFromSiteOrdept.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblRaisedFromSiteOrdept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRaisedFromSiteOrdept.ForeColor = System.Drawing.Color.Black
        Me.lblRaisedFromSiteOrdept.Location = New System.Drawing.Point(145, 1)
        Me.lblRaisedFromSiteOrdept.Name = "lblRaisedFromSiteOrdept"
        Me.lblRaisedFromSiteOrdept.Size = New System.Drawing.Size(38, 15)
        Me.lblRaisedFromSiteOrdept.TabIndex = 84
        Me.lblRaisedFromSiteOrdept.Tag = Nothing
        Me.lblRaisedFromSiteOrdept.Text = "From"
        Me.lblRaisedFromSiteOrdept.TextDetached = True
        Me.lblRaisedFromSiteOrdept.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.CtrlLabel3)
        Me.Panel2.Controls.Add(Me.RbCCDept)
        Me.Panel2.Controls.Add(Me.ListViewShowCCDept)
        Me.Panel2.Controls.Add(Me.RbCCSite)
        Me.Panel2.Controls.Add(Me.ChkListCCDept)
        Me.Panel2.Controls.Add(Me.CtrlLabel5)
        Me.Panel2.Location = New System.Drawing.Point(343, 64)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(357, 101)
        Me.Panel2.TabIndex = 82
        '
        'CtrlLabel3
        '
        Me.CtrlLabel3.AttachedTextBoxName = Nothing
        Me.CtrlLabel3.AutoSize = True
        Me.CtrlLabel3.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel3.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel3.Location = New System.Drawing.Point(3, 1)
        Me.CtrlLabel3.Name = "CtrlLabel3"
        Me.CtrlLabel3.Size = New System.Drawing.Size(36, 15)
        Me.CtrlLabel3.TabIndex = 70
        Me.CtrlLabel3.Tag = Nothing
        Me.CtrlLabel3.Text = "CC :"
        Me.CtrlLabel3.TextDetached = True
        Me.CtrlLabel3.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'RbCCDept
        '
        Me.RbCCDept.AutoSize = True
        Me.RbCCDept.Location = New System.Drawing.Point(45, 1)
        Me.RbCCDept.Name = "RbCCDept"
        Me.RbCCDept.Size = New System.Drawing.Size(93, 17)
        Me.RbCCDept.TabIndex = 73
        Me.RbCCDept.TabStop = True
        Me.RbCCDept.Text = "Department"
        Me.RbCCDept.UseVisualStyleBackColor = True
        '
        'ListViewShowCCDept
        '
        Me.ListViewShowCCDept.HorizontalScrollbar = True
        Me.ListViewShowCCDept.Location = New System.Drawing.Point(152, 33)
        Me.ListViewShowCCDept.Name = "ListViewShowCCDept"
        Me.ListViewShowCCDept.Size = New System.Drawing.Size(136, 69)
        Me.ListViewShowCCDept.TabIndex = 80
        '
        'RbCCSite
        '
        Me.RbCCSite.AutoSize = True
        Me.RbCCSite.Location = New System.Drawing.Point(142, 0)
        Me.RbCCSite.Name = "RbCCSite"
        Me.RbCCSite.Size = New System.Drawing.Size(47, 17)
        Me.RbCCSite.TabIndex = 74
        Me.RbCCSite.TabStop = True
        Me.RbCCSite.Text = "Site"
        Me.RbCCSite.UseVisualStyleBackColor = True
        '
        'ChkListCCDept
        '
        Me.ChkListCCDept.CheckOnClick = True
        Me.ChkListCCDept.FormattingEnabled = True
        Me.ChkListCCDept.HorizontalScrollbar = True
        Me.ChkListCCDept.Location = New System.Drawing.Point(5, 33)
        Me.ChkListCCDept.Name = "ChkListCCDept"
        Me.ChkListCCDept.Size = New System.Drawing.Size(136, 68)
        Me.ChkListCCDept.TabIndex = 78
        '
        'CtrlLabel5
        '
        Me.CtrlLabel5.AttachedTextBoxName = Nothing
        Me.CtrlLabel5.AutoSize = True
        Me.CtrlLabel5.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel5.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel5.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel5.Location = New System.Drawing.Point(3, 17)
        Me.CtrlLabel5.Name = "CtrlLabel5"
        Me.CtrlLabel5.Size = New System.Drawing.Size(156, 15)
        Me.CtrlLabel5.TabIndex = 76
        Me.CtrlLabel5.Tag = Nothing
        Me.CtrlLabel5.Text = "Select Site /Department :"
        Me.CtrlLabel5.TextDetached = True
        Me.CtrlLabel5.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.CtrlLabel4)
        Me.Panel1.Controls.Add(Me.ChkListRaisedDept)
        Me.Panel1.Controls.Add(Me.ListViewShowRaisedDept)
        Me.Panel1.Controls.Add(Me.CtrlLabel2)
        Me.Panel1.Controls.Add(Me.RbRaisedDept)
        Me.Panel1.Controls.Add(Me.RbRaisedSite)
        Me.Panel1.Controls.Add(Me.cbDepartment)
        Me.Panel1.Location = New System.Drawing.Point(13, 64)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(326, 101)
        Me.Panel1.TabIndex = 81
        '
        'CtrlLabel4
        '
        Me.CtrlLabel4.AttachedTextBoxName = Nothing
        Me.CtrlLabel4.AutoSize = True
        Me.CtrlLabel4.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel4.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel4.Location = New System.Drawing.Point(3, 17)
        Me.CtrlLabel4.Name = "CtrlLabel4"
        Me.CtrlLabel4.Size = New System.Drawing.Size(163, 15)
        Me.CtrlLabel4.TabIndex = 75
        Me.CtrlLabel4.Tag = Nothing
        Me.CtrlLabel4.Text = "*Select Site /Department :"
        Me.CtrlLabel4.TextDetached = True
        Me.CtrlLabel4.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'ChkListRaisedDept
        '
        Me.ChkListRaisedDept.CheckOnClick = True
        Me.ChkListRaisedDept.FormattingEnabled = True
        Me.ChkListRaisedDept.HorizontalScrollbar = True
        Me.ChkListRaisedDept.Location = New System.Drawing.Point(3, 33)
        Me.ChkListRaisedDept.Name = "ChkListRaisedDept"
        Me.ChkListRaisedDept.Size = New System.Drawing.Size(136, 68)
        Me.ChkListRaisedDept.TabIndex = 77
        '
        'ListViewShowRaisedDept
        '
        Me.ListViewShowRaisedDept.HorizontalScrollbar = True
        Me.ListViewShowRaisedDept.Location = New System.Drawing.Point(149, 33)
        Me.ListViewShowRaisedDept.Name = "ListViewShowRaisedDept"
        Me.ListViewShowRaisedDept.Size = New System.Drawing.Size(136, 69)
        Me.ListViewShowRaisedDept.TabIndex = 79
        '
        'CtrlLabel2
        '
        Me.CtrlLabel2.AttachedTextBoxName = Nothing
        Me.CtrlLabel2.AutoSize = True
        Me.CtrlLabel2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel2.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel2.Location = New System.Drawing.Point(3, 0)
        Me.CtrlLabel2.Name = "CtrlLabel2"
        Me.CtrlLabel2.Size = New System.Drawing.Size(81, 15)
        Me.CtrlLabel2.TabIndex = 69
        Me.CtrlLabel2.Tag = Nothing
        Me.CtrlLabel2.Text = "*Raised To :"
        Me.CtrlLabel2.TextDetached = True
        Me.CtrlLabel2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'RbRaisedDept
        '
        Me.RbRaisedDept.AutoSize = True
        Me.RbRaisedDept.Location = New System.Drawing.Point(84, 0)
        Me.RbRaisedDept.Name = "RbRaisedDept"
        Me.RbRaisedDept.Size = New System.Drawing.Size(93, 17)
        Me.RbRaisedDept.TabIndex = 71
        Me.RbRaisedDept.TabStop = True
        Me.RbRaisedDept.Text = "Department"
        Me.RbRaisedDept.UseVisualStyleBackColor = True
        '
        'RbRaisedSite
        '
        Me.RbRaisedSite.AutoSize = True
        Me.RbRaisedSite.Location = New System.Drawing.Point(183, 0)
        Me.RbRaisedSite.Name = "RbRaisedSite"
        Me.RbRaisedSite.Size = New System.Drawing.Size(47, 17)
        Me.RbRaisedSite.TabIndex = 72
        Me.RbRaisedSite.TabStop = True
        Me.RbRaisedSite.Text = "Site"
        Me.RbRaisedSite.UseVisualStyleBackColor = True
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
        Me.cbDepartment.Location = New System.Drawing.Point(3, 33)
        Me.cbDepartment.MatchEntryTimeout = CType(2000, Long)
        Me.cbDepartment.MaxDropDownItems = CType(5, Short)
        Me.cbDepartment.MaxLength = 32767
        Me.cbDepartment.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cbDepartment.MoveToNxtCtrl = Nothing
        Me.cbDepartment.Name = "cbDepartment"
        Me.cbDepartment.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cbDepartment.Size = New System.Drawing.Size(198, 21)
        Me.cbDepartment.strSelectStmt = ""
        Me.cbDepartment.TabIndex = 1
        Me.cbDepartment.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cbDepartment.PropBag = resources.GetString("cbDepartment.PropBag")
        '
        'CtrlLblSMSNo
        '
        Me.CtrlLblSMSNo.AttachedTextBoxName = Nothing
        Me.CtrlLblSMSNo.AutoSize = True
        Me.CtrlLblSMSNo.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLblSMSNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLblSMSNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLblSMSNo.ForeColor = System.Drawing.Color.Black
        Me.CtrlLblSMSNo.Location = New System.Drawing.Point(117, 44)
        Me.CtrlLblSMSNo.Name = "CtrlLblSMSNo"
        Me.CtrlLblSMSNo.Size = New System.Drawing.Size(2, 15)
        Me.CtrlLblSMSNo.TabIndex = 68
        Me.CtrlLblSMSNo.Tag = Nothing
        Me.CtrlLblSMSNo.TextDetached = True
        Me.CtrlLblSMSNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel1
        '
        Me.CtrlLabel1.AttachedTextBoxName = Nothing
        Me.CtrlLabel1.AutoSize = True
        Me.CtrlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel1.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel1.Location = New System.Drawing.Point(13, 44)
        Me.CtrlLabel1.Name = "CtrlLabel1"
        Me.CtrlLabel1.Size = New System.Drawing.Size(83, 15)
        Me.CtrlLabel1.TabIndex = 67
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.Text = "SMS on no. :"
        Me.CtrlLabel1.TextDetached = True
        Me.CtrlLabel1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblId
        '
        Me.lblId.AttachedTextBoxName = Nothing
        Me.lblId.AutoSize = True
        Me.lblId.BackColor = System.Drawing.Color.Transparent
        Me.lblId.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblId.ForeColor = System.Drawing.Color.Black
        Me.lblId.Location = New System.Drawing.Point(117, 21)
        Me.lblId.Name = "lblId"
        Me.lblId.Size = New System.Drawing.Size(84, 15)
        Me.lblId.TabIndex = 41
        Me.lblId.Tag = Nothing
        Me.lblId.Text = "Ticket Id 001"
        Me.lblId.TextDetached = True
        Me.lblId.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblFile
        '
        Me.lblFile.AttachedTextBoxName = Nothing
        Me.lblFile.AutoSize = True
        Me.lblFile.BackColor = System.Drawing.Color.Transparent
        Me.lblFile.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblFile.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblFile.ForeColor = System.Drawing.Color.Black
        Me.lblFile.Location = New System.Drawing.Point(117, 169)
        Me.lblFile.Name = "lblFile"
        Me.lblFile.Size = New System.Drawing.Size(0, 16)
        Me.lblFile.TabIndex = 53
        Me.lblFile.Tag = Nothing
        Me.lblFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFile.TextDetached = True
        Me.lblFile.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'dgGrievanceHistoryGrid
        '
        Me.dgGrievanceHistoryGrid.CellButtonImage = CType(resources.GetObject("dgGrievanceHistoryGrid.CellButtonImage"), System.Drawing.Image)
        Me.dgGrievanceHistoryGrid.ColumnInfo = "3,1,0,0,0,100,Columns:0{Visible:False;}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{Width:252;Caption:""Department Change Hi" & _
    "story"";Style:""TextAlign:GeneralCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "2{Visible:False;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.dgGrievanceHistoryGrid.ExtendLastCol = True
        Me.dgGrievanceHistoryGrid.Location = New System.Drawing.Point(13, 283)
        Me.dgGrievanceHistoryGrid.Name = "dgGrievanceHistoryGrid"
        Me.dgGrievanceHistoryGrid.Rows.DefaultSize = 20
        Me.dgGrievanceHistoryGrid.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.dgGrievanceHistoryGrid.Size = New System.Drawing.Size(687, 82)
        Me.dgGrievanceHistoryGrid.StyleInfo = resources.GetString("dgGrievanceHistoryGrid.StyleInfo")
        Me.dgGrievanceHistoryGrid.TabIndex = 66
        Me.dgGrievanceHistoryGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'RdDesc
        '
        Me.RdDesc.AutoSize = True
        Me.RdDesc.Location = New System.Drawing.Point(117, 369)
        Me.RdDesc.Name = "RdDesc"
        Me.RdDesc.Size = New System.Drawing.Size(109, 29)
        Me.RdDesc.TabIndex = 65
        Me.RdDesc.TabStop = True
        Me.RdDesc.Text = "Descending"
        Me.RdDesc.UseVisualStyleBackColor = True
        '
        'RdAsc
        '
        Me.RdAsc.AutoSize = True
        Me.RdAsc.Location = New System.Drawing.Point(13, 369)
        Me.RdAsc.Name = "RdAsc"
        Me.RdAsc.Size = New System.Drawing.Size(79, 29)
        Me.RdAsc.TabIndex = 0
        Me.RdAsc.TabStop = True
        Me.RdAsc.Text = "Ascending"
        Me.RdAsc.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.21239!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.78761!))
        Me.TableLayoutPanel1.Controls.Add(Me.cmdSubmit, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmdCancel, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(704, 575)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(124, 43)
        Me.TableLayoutPanel1.TabIndex = 49
        '
        'cmdSubmit
        '
        Me.cmdSubmit.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSubmit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSubmit.Location = New System.Drawing.Point(3, 3)
        Me.cmdSubmit.MoveToNxtCtrl = Nothing
        Me.cmdSubmit.Name = "cmdSubmit"
        Me.cmdSubmit.SetArticleCode = Nothing
        Me.cmdSubmit.SetRowIndex = 0
        Me.cmdSubmit.Size = New System.Drawing.Size(58, 37)
        Me.cmdSubmit.TabIndex = 39
        Me.cmdSubmit.Text = "Submit"
        Me.cmdSubmit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSubmit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdSubmit.UseVisualStyleBackColor = True
        Me.cmdSubmit.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdCancel.Location = New System.Drawing.Point(67, 3)
        Me.cmdCancel.MoveToNxtCtrl = Nothing
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.SetArticleCode = Nothing
        Me.cmdCancel.SetRowIndex = 0
        Me.cmdCancel.Size = New System.Drawing.Size(54, 37)
        Me.cmdCancel.TabIndex = 40
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdCancel.UseVisualStyleBackColor = True
        Me.cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabelUpdatedOn
        '
        Me.CtrlLabelUpdatedOn.AttachedTextBoxName = Nothing
        Me.CtrlLabelUpdatedOn.AutoSize = True
        Me.CtrlLabelUpdatedOn.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabelUpdatedOn.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabelUpdatedOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabelUpdatedOn.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabelUpdatedOn.Location = New System.Drawing.Point(343, 189)
        Me.CtrlLabelUpdatedOn.Name = "CtrlLabelUpdatedOn"
        Me.CtrlLabelUpdatedOn.Size = New System.Drawing.Size(2, 15)
        Me.CtrlLabelUpdatedOn.TabIndex = 64
        Me.CtrlLabelUpdatedOn.Tag = Nothing
        Me.CtrlLabelUpdatedOn.TextDetached = True
        Me.CtrlLabelUpdatedOn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabelUpdatedBy
        '
        Me.CtrlLabelUpdatedBy.AttachedTextBoxName = Nothing
        Me.CtrlLabelUpdatedBy.AutoSize = True
        Me.CtrlLabelUpdatedBy.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabelUpdatedBy.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabelUpdatedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabelUpdatedBy.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabelUpdatedBy.Location = New System.Drawing.Point(343, 169)
        Me.CtrlLabelUpdatedBy.Name = "CtrlLabelUpdatedBy"
        Me.CtrlLabelUpdatedBy.Size = New System.Drawing.Size(2, 15)
        Me.CtrlLabelUpdatedBy.TabIndex = 63
        Me.CtrlLabelUpdatedBy.Tag = Nothing
        Me.CtrlLabelUpdatedBy.TextDetached = True
        Me.CtrlLabelUpdatedBy.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabelCreatedOn
        '
        Me.CtrlLabelCreatedOn.AttachedTextBoxName = Nothing
        Me.CtrlLabelCreatedOn.AutoSize = True
        Me.CtrlLabelCreatedOn.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabelCreatedOn.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabelCreatedOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabelCreatedOn.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabelCreatedOn.Location = New System.Drawing.Point(581, 169)
        Me.CtrlLabelCreatedOn.Name = "CtrlLabelCreatedOn"
        Me.CtrlLabelCreatedOn.Size = New System.Drawing.Size(2, 15)
        Me.CtrlLabelCreatedOn.TabIndex = 62
        Me.CtrlLabelCreatedOn.Tag = Nothing
        Me.CtrlLabelCreatedOn.TextDetached = True
        Me.CtrlLabelCreatedOn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabelCreatedBy
        '
        Me.CtrlLabelCreatedBy.AttachedTextBoxName = Nothing
        Me.CtrlLabelCreatedBy.AutoSize = True
        Me.CtrlLabelCreatedBy.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabelCreatedBy.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabelCreatedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabelCreatedBy.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabelCreatedBy.Location = New System.Drawing.Point(117, 169)
        Me.CtrlLabelCreatedBy.Name = "CtrlLabelCreatedBy"
        Me.CtrlLabelCreatedBy.Size = New System.Drawing.Size(2, 15)
        Me.CtrlLabelCreatedBy.TabIndex = 61
        Me.CtrlLabelCreatedBy.Tag = Nothing
        Me.CtrlLabelCreatedBy.TextDetached = True
        Me.CtrlLabelCreatedBy.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlUpdatedOn
        '
        Me.CtrlUpdatedOn.AttachedTextBoxName = Nothing
        Me.CtrlUpdatedOn.AutoSize = True
        Me.CtrlUpdatedOn.BackColor = System.Drawing.Color.Transparent
        Me.CtrlUpdatedOn.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlUpdatedOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlUpdatedOn.ForeColor = System.Drawing.Color.Black
        Me.CtrlUpdatedOn.Location = New System.Drawing.Point(244, 189)
        Me.CtrlUpdatedOn.Name = "CtrlUpdatedOn"
        Me.CtrlUpdatedOn.Size = New System.Drawing.Size(85, 15)
        Me.CtrlUpdatedOn.TabIndex = 60
        Me.CtrlUpdatedOn.Tag = Nothing
        Me.CtrlUpdatedOn.Text = "Updated On :"
        Me.CtrlUpdatedOn.TextDetached = True
        Me.CtrlUpdatedOn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlUpdatedBy
        '
        Me.CtrlUpdatedBy.AttachedTextBoxName = Nothing
        Me.CtrlUpdatedBy.AutoSize = True
        Me.CtrlUpdatedBy.BackColor = System.Drawing.Color.Transparent
        Me.CtrlUpdatedBy.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlUpdatedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlUpdatedBy.ForeColor = System.Drawing.Color.Black
        Me.CtrlUpdatedBy.Location = New System.Drawing.Point(244, 169)
        Me.CtrlUpdatedBy.Name = "CtrlUpdatedBy"
        Me.CtrlUpdatedBy.Size = New System.Drawing.Size(84, 15)
        Me.CtrlUpdatedBy.TabIndex = 58
        Me.CtrlUpdatedBy.Tag = Nothing
        Me.CtrlUpdatedBy.Text = "Updated By :"
        Me.CtrlUpdatedBy.TextDetached = True
        Me.CtrlUpdatedBy.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlCreatedOn
        '
        Me.CtrlCreatedOn.AttachedTextBoxName = Nothing
        Me.CtrlCreatedOn.AutoSize = True
        Me.CtrlCreatedOn.BackColor = System.Drawing.Color.Transparent
        Me.CtrlCreatedOn.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlCreatedOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlCreatedOn.ForeColor = System.Drawing.Color.Black
        Me.CtrlCreatedOn.Location = New System.Drawing.Point(495, 169)
        Me.CtrlCreatedOn.Name = "CtrlCreatedOn"
        Me.CtrlCreatedOn.Size = New System.Drawing.Size(84, 15)
        Me.CtrlCreatedOn.TabIndex = 56
        Me.CtrlCreatedOn.Tag = Nothing
        Me.CtrlCreatedOn.Text = "Created On :"
        Me.CtrlCreatedOn.TextDetached = True
        Me.CtrlCreatedOn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlCreatedBy
        '
        Me.CtrlCreatedBy.AttachedTextBoxName = Nothing
        Me.CtrlCreatedBy.AutoSize = True
        Me.CtrlCreatedBy.BackColor = System.Drawing.Color.Transparent
        Me.CtrlCreatedBy.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlCreatedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlCreatedBy.ForeColor = System.Drawing.Color.Black
        Me.CtrlCreatedBy.Location = New System.Drawing.Point(13, 169)
        Me.CtrlCreatedBy.Name = "CtrlCreatedBy"
        Me.CtrlCreatedBy.Size = New System.Drawing.Size(83, 15)
        Me.CtrlCreatedBy.TabIndex = 54
        Me.CtrlCreatedBy.Tag = Nothing
        Me.CtrlCreatedBy.Text = "Created By :"
        Me.CtrlCreatedBy.TextDetached = True
        Me.CtrlCreatedBy.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtGrievanceDetail
        '
        Me.txtGrievanceDetail.AutoSize = False
        Me.txtGrievanceDetail.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtGrievanceDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGrievanceDetail.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.txtGrievanceDetail.Location = New System.Drawing.Point(13, 211)
        Me.txtGrievanceDetail.MaxLength = 5000
        Me.txtGrievanceDetail.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtGrievanceDetail.MoveToNxtCtrl = Nothing
        Me.txtGrievanceDetail.Multiline = True
        Me.txtGrievanceDetail.Name = "txtGrievanceDetail"
        Me.txtGrievanceDetail.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtGrievanceDetail.Size = New System.Drawing.Size(687, 68)
        Me.txtGrievanceDetail.TabIndex = 5
        Me.txtGrievanceDetail.Tag = Nothing
        Me.txtGrievanceDetail.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtGrievanceDetail.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblGrievanceId
        '
        Me.lblGrievanceId.AttachedTextBoxName = Nothing
        Me.lblGrievanceId.AutoSize = True
        Me.lblGrievanceId.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGrievanceId.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGrievanceId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGrievanceId.ForeColor = System.Drawing.Color.Black
        Me.lblGrievanceId.Location = New System.Drawing.Point(13, 21)
        Me.lblGrievanceId.Name = "lblGrievanceId"
        Me.lblGrievanceId.Size = New System.Drawing.Size(70, 15)
        Me.lblGrievanceId.TabIndex = 21
        Me.lblGrievanceId.Tag = Nothing
        Me.lblGrievanceId.Text = "Ticket ID :"
        Me.lblGrievanceId.TextDetached = True
        Me.lblGrievanceId.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'fpRemarks
        '
        Me.fpRemarks.AccessibleName = "FlwPanel"
        Me.fpRemarks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.fpRemarks.AutoScroll = True
        Me.fpRemarks.AutoSize = True
        Me.fpRemarks.Location = New System.Drawing.Point(13, 402)
        Me.fpRemarks.MaximumSize = New System.Drawing.Size(900, 360)
        Me.fpRemarks.MinimumSize = New System.Drawing.Size(800, 0)
        Me.fpRemarks.Name = "fpRemarks"
        Me.fpRemarks.Size = New System.Drawing.Size(815, 169)
        Me.fpRemarks.TabIndex = 48
        '
        'lblGrievanceDesc
        '
        Me.lblGrievanceDesc.AttachedTextBoxName = Nothing
        Me.lblGrievanceDesc.AutoSize = True
        Me.lblGrievanceDesc.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGrievanceDesc.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGrievanceDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGrievanceDesc.ForeColor = System.Drawing.Color.Black
        Me.lblGrievanceDesc.Location = New System.Drawing.Point(13, 189)
        Me.lblGrievanceDesc.Name = "lblGrievanceDesc"
        Me.lblGrievanceDesc.Size = New System.Drawing.Size(102, 15)
        Me.lblGrievanceDesc.TabIndex = 24
        Me.lblGrievanceDesc.Tag = Nothing
        Me.lblGrievanceDesc.Text = "*Ticket Details :"
        Me.lblGrievanceDesc.TextDetached = True
        Me.lblGrievanceDesc.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblGrievanceTitle
        '
        Me.lblGrievanceTitle.AttachedTextBoxName = Nothing
        Me.lblGrievanceTitle.AutoSize = True
        Me.lblGrievanceTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGrievanceTitle.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGrievanceTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGrievanceTitle.ForeColor = System.Drawing.Color.Black
        Me.lblGrievanceTitle.Location = New System.Drawing.Point(495, 44)
        Me.lblGrievanceTitle.Name = "lblGrievanceTitle"
        Me.lblGrievanceTitle.Size = New System.Drawing.Size(87, 15)
        Me.lblGrievanceTitle.TabIndex = 23
        Me.lblGrievanceTitle.Tag = Nothing
        Me.lblGrievanceTitle.Text = "*Ticket Title :"
        Me.lblGrievanceTitle.TextDetached = True
        Me.lblGrievanceTitle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtGrievanceTitle
        '
        Me.txtGrievanceTitle.AutoSize = False
        Me.txtGrievanceTitle.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtGrievanceTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGrievanceTitle.Location = New System.Drawing.Point(581, 44)
        Me.txtGrievanceTitle.MaximumSize = New System.Drawing.Size(1000, 1000)
        Me.txtGrievanceTitle.MaxLength = 200
        Me.txtGrievanceTitle.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtGrievanceTitle.MoveToNxtCtrl = Nothing
        Me.txtGrievanceTitle.Name = "txtGrievanceTitle"
        Me.txtGrievanceTitle.Size = New System.Drawing.Size(247, 21)
        Me.txtGrievanceTitle.TabIndex = 4
        Me.txtGrievanceTitle.Tag = Nothing
        Me.txtGrievanceTitle.TextDetached = True
        Me.txtGrievanceTitle.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtGrievanceTitle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblGrievanceType
        '
        Me.lblGrievanceType.AttachedTextBoxName = Nothing
        Me.lblGrievanceType.AutoSize = True
        Me.lblGrievanceType.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGrievanceType.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGrievanceType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGrievanceType.ForeColor = System.Drawing.Color.Black
        Me.lblGrievanceType.Location = New System.Drawing.Point(581, 21)
        Me.lblGrievanceType.Name = "lblGrievanceType"
        Me.lblGrievanceType.Size = New System.Drawing.Size(91, 15)
        Me.lblGrievanceType.TabIndex = 36
        Me.lblGrievanceType.Tag = Nothing
        Me.lblGrievanceType.Text = "*Ticket Type :"
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
        Me.cbGrievanceType.Location = New System.Drawing.Point(704, 21)
        Me.cbGrievanceType.MatchEntryTimeout = CType(2000, Long)
        Me.cbGrievanceType.MaxDropDownItems = CType(5, Short)
        Me.cbGrievanceType.MaxLength = 32767
        Me.cbGrievanceType.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cbGrievanceType.MoveToNxtCtrl = Nothing
        Me.cbGrievanceType.Name = "cbGrievanceType"
        Me.cbGrievanceType.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cbGrievanceType.Size = New System.Drawing.Size(124, 21)
        Me.cbGrievanceType.strSelectStmt = ""
        Me.cbGrievanceType.TabIndex = 3
        Me.cbGrievanceType.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cbGrievanceType.PropBag = resources.GetString("cbGrievanceType.PropBag")
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmNGrievance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(844, 629)
        Me.Controls.Add(Me.c1Sizer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNGrievance"
        CType(Me.c1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.c1Sizer1.ResumeLayout(False)
        Me.c1Sizer1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.CtrlLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRaisedFromSiteOrdept, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.CtrlLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLblSMSNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgGrievanceHistoryGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.CtrlLabelUpdatedOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabelUpdatedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabelCreatedOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabelCreatedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlUpdatedOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlUpdatedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlCreatedOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlCreatedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGrievanceDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGrievanceId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGrievanceDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGrievanceTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGrievanceTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGrievanceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbGrievanceType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents c1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents cmdCancel As Spectrum.CtrlBtn
    Friend WithEvents cmdSubmit As Spectrum.CtrlBtn
    Friend WithEvents cbDepartment As Spectrum.ctrlCombo
    Friend WithEvents cbGrievanceType As Spectrum.ctrlCombo
    Friend WithEvents lblGrievanceType As Spectrum.CtrlLabel
    Friend WithEvents txtGrievanceTitle As Spectrum.CtrlTextBox
    Friend WithEvents cbStatus As Spectrum.ctrlCombo
    Friend WithEvents lblGrievanceDesc As Spectrum.CtrlLabel
    Friend WithEvents lblGrievanceTitle As Spectrum.CtrlLabel
    Friend WithEvents lblStatus As Spectrum.CtrlLabel
    Friend WithEvents lblGrievanceId As Spectrum.CtrlLabel
    Friend WithEvents lblId As Spectrum.CtrlLabel
    Friend WithEvents fpRemarks As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtGrievanceDetail As New Spectrum.CtrlTextBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblFile As Spectrum.CtrlLabel
    Friend WithEvents CtrlCreatedOn As Spectrum.CtrlLabel
    Friend WithEvents CtrlCreatedBy As Spectrum.CtrlLabel
    Friend WithEvents CtrlUpdatedBy As Spectrum.CtrlLabel
    Friend WithEvents CtrlUpdatedOn As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabelCreatedBy As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabelCreatedOn As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabelUpdatedBy As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabelUpdatedOn As Spectrum.CtrlLabel
    Friend WithEvents RdDesc As System.Windows.Forms.RadioButton
    Friend WithEvents RdAsc As System.Windows.Forms.RadioButton
    Friend WithEvents dgGrievanceHistoryGrid As Spectrum.CtrlGrid
    Friend WithEvents CtrlLblSMSNo As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel1 As Spectrum.CtrlLabel
    Friend WithEvents RbCCSite As System.Windows.Forms.RadioButton
    Friend WithEvents RbCCDept As System.Windows.Forms.RadioButton
    Friend WithEvents RbRaisedSite As System.Windows.Forms.RadioButton
    Friend WithEvents RbRaisedDept As System.Windows.Forms.RadioButton
    Friend WithEvents CtrlLabel3 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel2 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel5 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel4 As Spectrum.CtrlLabel
    Friend WithEvents ListViewShowCCDept As System.Windows.Forms.ListBox
    Friend WithEvents ListViewShowRaisedDept As System.Windows.Forms.ListBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents ChkListCCDept As System.Windows.Forms.CheckedListBox
    Private WithEvents ChkListRaisedDept As System.Windows.Forms.CheckedListBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents CtrlLabel6 As Spectrum.CtrlLabel
    Friend WithEvents lblRaisedFromSiteOrdept As Spectrum.CtrlLabel
End Class
