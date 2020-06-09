<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalesOrderSearch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalesOrderSearch))
        Me.rbnSOCreatedAtThisSite = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbtSOAssignedToThisSite = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgSearch = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        Me.cmdCancel = New Spectrum.CtrlBtn()
        Me.lblCount = New Spectrum.CtrlLabel()
        Me.cmdOk = New Spectrum.CtrlBtn()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rbnSOCreatedAtThisSite
        '
        Me.rbnSOCreatedAtThisSite.AutoSize = True
        Me.rbnSOCreatedAtThisSite.Location = New System.Drawing.Point(12, 7)
        Me.rbnSOCreatedAtThisSite.Name = "rbnSOCreatedAtThisSite"
        Me.rbnSOCreatedAtThisSite.Size = New System.Drawing.Size(140, 17)
        Me.rbnSOCreatedAtThisSite.TabIndex = 2
        Me.rbnSOCreatedAtThisSite.TabStop = True
        Me.rbnSOCreatedAtThisSite.Text = "SOForCreatedAtThisSite"
        Me.rbnSOCreatedAtThisSite.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbtSOAssignedToThisSite)
        Me.Panel1.Controls.Add(Me.rbnSOCreatedAtThisSite)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(733, 34)
        Me.Panel1.TabIndex = 47
        '
        'rbtSOAssignedToThisSite
        '
        Me.rbtSOAssignedToThisSite.AutoSize = True
        Me.rbtSOAssignedToThisSite.Location = New System.Drawing.Point(183, 7)
        Me.rbtSOAssignedToThisSite.Name = "rbtSOAssignedToThisSite"
        Me.rbtSOAssignedToThisSite.Size = New System.Drawing.Size(134, 17)
        Me.rbtSOAssignedToThisSite.TabIndex = 3
        Me.rbtSOAssignedToThisSite.TabStop = True
        Me.rbtSOAssignedToThisSite.Text = "SOAssignedToThisSite"
        Me.rbtSOAssignedToThisSite.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CtrlLabel1)
        Me.GroupBox1.Controls.Add(Me.cmdCancel)
        Me.GroupBox1.Controls.Add(Me.lblCount)
        Me.GroupBox1.Controls.Add(Me.cmdOk)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 305)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(733, 50)
        Me.GroupBox1.TabIndex = 44
        Me.GroupBox1.TabStop = False
        '
        'dgSearch
        '
        Me.dgSearch.AllowColMove = False
        Me.dgSearch.AllowColSelect = False
        Me.dgSearch.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.dgSearch.AllowUpdate = False
        Me.dgSearch.AllowUpdateOnBlur = False
        Me.dgSearch.BackColor = System.Drawing.SystemColors.Window
        Me.dgSearch.ExtendRightColumn = True
        Me.dgSearch.FilterBar = True
        Me.dgSearch.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.Standard
        Me.dgSearch.GroupByAreaVisible = False
        Me.dgSearch.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgSearch.Images.Add(CType(resources.GetObject("dgSearch.Images"), System.Drawing.Image))
        Me.dgSearch.Images.Add(CType(resources.GetObject("dgSearch.Images1"), System.Drawing.Image))
        Me.dgSearch.Location = New System.Drawing.Point(0, 36)
        Me.dgSearch.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder
        Me.dgSearch.Name = "dgSearch"
        Me.dgSearch.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgSearch.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgSearch.PreviewInfo.ZoomFactor = 75.0R
        Me.dgSearch.PrintInfo.PageSettings = CType(resources.GetObject("dgSearch.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgSearch.RecordSelectors = False
        Me.dgSearch.RowDivider.Color = System.Drawing.Color.Transparent
        Me.dgSearch.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None
        Me.dgSearch.RowSubDividerColor = System.Drawing.Color.Transparent
        Me.dgSearch.Size = New System.Drawing.Size(733, 275)
        Me.dgSearch.TabIndex = 43
        Me.dgSearch.UseColumnStyles = False
        Me.dgSearch.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue
        Me.dgSearch.PropBag = resources.GetString("dgSearch.PropBag")
        '
        'CtrlLabel1
        '
        Me.CtrlLabel1.AttachedTextBoxName = Nothing
        Me.CtrlLabel1.AutoSize = True
        Me.CtrlLabel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.CtrlLabel1.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel1.Location = New System.Drawing.Point(3, 22)
        Me.CtrlLabel1.Name = "CtrlLabel1"
        Me.CtrlLabel1.Size = New System.Drawing.Size(76, 15)
        Me.CtrlLabel1.TabIndex = 37
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.Text = "Total Row:"
        Me.CtrlLabel1.TextDetached = True
        Me.CtrlLabel1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdCancel.Location = New System.Drawing.Point(647, 12)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.SetArticleCode = Nothing
        Me.cmdCancel.Size = New System.Drawing.Size(75, 32)
        Me.cmdCancel.TabIndex = 36
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdCancel.UseVisualStyleBackColor = True
        Me.cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCount
        '
        Me.lblCount.AttachedTextBoxName = Nothing
        Me.lblCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCount.ForeColor = System.Drawing.Color.Black
        Me.lblCount.Location = New System.Drawing.Point(80, 21)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(74, 18)
        Me.lblCount.TabIndex = 38
        Me.lblCount.Tag = Nothing
        Me.lblCount.TextDetached = True
        Me.lblCount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOk.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdOk.Location = New System.Drawing.Point(562, 12)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.SetArticleCode = Nothing
        Me.cmdOk.Size = New System.Drawing.Size(75, 32)
        Me.cmdOk.TabIndex = 35
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdOk.UseVisualStyleBackColor = True
        Me.cmdOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmSalesOrderSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(733, 362)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgSearch)
        Me.Name = "frmSalesOrderSearch"
        Me.Text = "frmSalesOrderSearch"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rbnSOCreatedAtThisSite As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbtSOAssignedToThisSite As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CtrlLabel1 As Spectrum.CtrlLabel
    Friend WithEvents cmdCancel As Spectrum.CtrlBtn
    Friend WithEvents lblCount As Spectrum.CtrlLabel
    Friend WithEvents cmdOk As Spectrum.CtrlBtn
    Friend WithEvents dgSearch As C1.Win.C1TrueDBGrid.C1TrueDBGrid
End Class
