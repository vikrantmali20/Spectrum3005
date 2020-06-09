<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDataArcive
    Inherits Spectrum.CtrlPopupForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDataArcive))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.C1Sizer1 = New C1.Win.C1Sizer.C1Sizer
        Me.dtpToDate = New Spectrum.ctrlDate
        Me.dtpFromDate = New Spectrum.ctrlDate
        Me.cmdStart = New Spectrum.CtrlBtn
        Me.CtrlLabel4 = New Spectrum.CtrlLabel
        Me.txtServerPath = New Spectrum.CtrlTextBox
        Me.CtrlLabel3 = New Spectrum.CtrlLabel
        Me.CtrlLabel2 = New Spectrum.CtrlLabel
        Me.CtrlLabel1 = New Spectrum.CtrlLabel
        Me.txtDBName = New Spectrum.CtrlTextBox
        Me.C1Sizer2 = New C1.Win.C1Sizer.C1Sizer
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.rtxtStatus = New System.Windows.Forms.RichTextBox
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer1.SuspendLayout()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtServerPath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDBName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Sizer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer2.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.C1Sizer1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.C1Sizer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(669, 444)
        Me.SplitContainer1.SplitterDistance = 198
        Me.SplitContainer1.TabIndex = 0
        '
        'C1Sizer1
        '
        Me.C1Sizer1.Controls.Add(Me.dtpToDate)
        Me.C1Sizer1.Controls.Add(Me.dtpFromDate)
        Me.C1Sizer1.Controls.Add(Me.cmdStart)
        Me.C1Sizer1.Controls.Add(Me.CtrlLabel4)
        Me.C1Sizer1.Controls.Add(Me.txtServerPath)
        Me.C1Sizer1.Controls.Add(Me.CtrlLabel3)
        Me.C1Sizer1.Controls.Add(Me.CtrlLabel2)
        Me.C1Sizer1.Controls.Add(Me.CtrlLabel1)
        Me.C1Sizer1.Controls.Add(Me.txtDBName)
        Me.C1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Sizer1.GridDefinition = "95.959595959596:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "98.8041853512705:False:False;"
        Me.C1Sizer1.Location = New System.Drawing.Point(0, 0)
        Me.C1Sizer1.Name = "C1Sizer1"
        Me.C1Sizer1.Size = New System.Drawing.Size(669, 198)
        Me.C1Sizer1.TabIndex = 0
        Me.C1Sizer1.Text = "C1Sizer1"
        '
        'dtpToDate
        '
        Me.dtpToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtpToDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage"), System.Drawing.Image)
        '
        '
        '
        Me.dtpToDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtpToDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.dtpToDate.DisplayFormat.CustomFormat = "dd"
        Me.dtpToDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpToDate.DisplayFormat.Inherit = CType((((C1.Win.C1Input.FormatInfoInheritFlags.NullText Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
                    Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
                    Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpToDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpToDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
                    Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
                    Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
                    Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpToDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpToDate.InterceptArrowKeys = False
        Me.dtpToDate.Location = New System.Drawing.Point(521, 16)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(112, 19)
        Me.dtpToDate.TabIndex = 10
        Me.dtpToDate.Tag = Nothing
        Me.dtpToDate.TrimStart = True
        Me.dtpToDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        Me.dtpToDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtpToDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'dtpFromDate
        '
        Me.dtpFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtpFromDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage1"), System.Drawing.Image)
        '
        '
        '
        Me.dtpFromDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtpFromDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.dtpFromDate.DisplayFormat.CustomFormat = "dd"
        Me.dtpFromDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpFromDate.DisplayFormat.Inherit = CType((((C1.Win.C1Input.FormatInfoInheritFlags.NullText Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
                    Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
                    Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpFromDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpFromDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
                    Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
                    Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
                    Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpFromDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpFromDate.InterceptArrowKeys = False
        Me.dtpFromDate.Location = New System.Drawing.Point(213, 16)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(112, 19)
        Me.dtpFromDate.TabIndex = 9
        Me.dtpFromDate.Tag = Nothing
        Me.dtpFromDate.TrimStart = True
        Me.dtpFromDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        Me.dtpFromDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtpFromDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'cmdStart
        '
        Me.cmdStart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdStart.Location = New System.Drawing.Point(237, 134)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.Size = New System.Drawing.Size(132, 32)
        Me.cmdStart.TabIndex = 8
        Me.cmdStart.Text = "Start"
        Me.cmdStart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdStart.UseVisualStyleBackColor = True
        Me.cmdStart.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel4
        '
        Me.CtrlLabel4.AttachedTextBoxName = Nothing
        Me.CtrlLabel4.AutoSize = True
        Me.CtrlLabel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel4.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel4.Location = New System.Drawing.Point(22, 85)
        Me.CtrlLabel4.Name = "CtrlLabel4"
        Me.CtrlLabel4.Size = New System.Drawing.Size(77, 15)
        Me.CtrlLabel4.TabIndex = 7
        Me.CtrlLabel4.Tag = Nothing
        Me.CtrlLabel4.Text = "Server Path"
        Me.CtrlLabel4.TextDetached = True
        '
        'txtServerPath
        '
        Me.txtServerPath.AutoSize = False
        Me.txtServerPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServerPath.Location = New System.Drawing.Point(213, 84)
        Me.txtServerPath.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtServerPath.Name = "txtServerPath"
        Me.txtServerPath.Size = New System.Drawing.Size(420, 27)
        Me.txtServerPath.TabIndex = 6
        Me.txtServerPath.Tag = Nothing
        Me.txtServerPath.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtServerPath.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel3
        '
        Me.CtrlLabel3.AttachedTextBoxName = Nothing
        Me.CtrlLabel3.AutoSize = True
        Me.CtrlLabel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel3.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel3.Location = New System.Drawing.Point(22, 56)
        Me.CtrlLabel3.Name = "CtrlLabel3"
        Me.CtrlLabel3.Size = New System.Drawing.Size(105, 15)
        Me.CtrlLabel3.TabIndex = 5
        Me.CtrlLabel3.Tag = Nothing
        Me.CtrlLabel3.Text = "Data Base Name"
        Me.CtrlLabel3.TextDetached = True
        '
        'CtrlLabel2
        '
        Me.CtrlLabel2.AttachedTextBoxName = Nothing
        Me.CtrlLabel2.AutoSize = True
        Me.CtrlLabel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel2.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel2.Location = New System.Drawing.Point(22, 16)
        Me.CtrlLabel2.Name = "CtrlLabel2"
        Me.CtrlLabel2.Size = New System.Drawing.Size(74, 15)
        Me.CtrlLabel2.TabIndex = 4
        Me.CtrlLabel2.Tag = Nothing
        Me.CtrlLabel2.Text = "From Date:"
        Me.CtrlLabel2.TextDetached = True
        '
        'CtrlLabel1
        '
        Me.CtrlLabel1.AttachedTextBoxName = Nothing
        Me.CtrlLabel1.AutoSize = True
        Me.CtrlLabel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel1.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel1.Location = New System.Drawing.Point(377, 15)
        Me.CtrlLabel1.Name = "CtrlLabel1"
        Me.CtrlLabel1.Size = New System.Drawing.Size(59, 15)
        Me.CtrlLabel1.TabIndex = 3
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.Text = "To Date:"
        Me.CtrlLabel1.TextDetached = True
        '
        'txtDBName
        '
        Me.txtDBName.AutoSize = False
        Me.txtDBName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDBName.Location = New System.Drawing.Point(213, 56)
        Me.txtDBName.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtDBName.Name = "txtDBName"
        Me.txtDBName.Size = New System.Drawing.Size(420, 27)
        Me.txtDBName.TabIndex = 0
        Me.txtDBName.Tag = Nothing
        Me.txtDBName.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtDBName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Sizer2
        '
        Me.C1Sizer2.Controls.Add(Me.ProgressBar1)
        Me.C1Sizer2.Controls.Add(Me.rtxtStatus)
        Me.C1Sizer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Sizer2.GridDefinition = "96.6942148760331:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "98.8041853512705:False:False;"
        Me.C1Sizer2.Location = New System.Drawing.Point(0, 0)
        Me.C1Sizer2.Name = "C1Sizer2"
        Me.C1Sizer2.Size = New System.Drawing.Size(669, 242)
        Me.C1Sizer2.TabIndex = 1
        Me.C1Sizer2.Text = "C1Sizer2"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 217)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(669, 25)
        Me.ProgressBar1.TabIndex = 11
        '
        'rtxtStatus
        '
        Me.rtxtStatus.Dock = System.Windows.Forms.DockStyle.Top
        Me.rtxtStatus.Location = New System.Drawing.Point(0, 0)
        Me.rtxtStatus.Name = "rtxtStatus"
        Me.rtxtStatus.Size = New System.Drawing.Size(669, 215)
        Me.rtxtStatus.TabIndex = 0
        Me.rtxtStatus.Text = ""
        '
        'BackgroundWorker1
        '
        '
        'frmDataArcive
        '
        Me.ClientSize = New System.Drawing.Size(669, 444)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDataArcive"
        Me.Text = "Data Archive"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer1.ResumeLayout(False)
        Me.C1Sizer1.PerformLayout()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtServerPath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDBName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Sizer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents C1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents C1Sizer2 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents CtrlLabel2 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel1 As Spectrum.CtrlLabel
    Friend WithEvents txtDBName As Spectrum.CtrlTextBox
    Friend WithEvents CtrlLabel4 As Spectrum.CtrlLabel
    Friend WithEvents txtServerPath As Spectrum.CtrlTextBox
    Friend WithEvents CtrlLabel3 As Spectrum.CtrlLabel
    Friend WithEvents rtxtStatus As System.Windows.Forms.RichTextBox
    Friend WithEvents cmdStart As Spectrum.CtrlBtn
    Friend WithEvents dtpToDate As Spectrum.ctrlDate
    Friend WithEvents dtpFromDate As Spectrum.ctrlDate
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker

End Class
