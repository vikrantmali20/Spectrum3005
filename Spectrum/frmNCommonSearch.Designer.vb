﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNCommonSearch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNCommonSearch))
        Me.dgSearch = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.cmdOk = New Spectrum.CtrlBtn()
        Me.cmdCancel = New Spectrum.CtrlBtn()
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        Me.lblCount = New Spectrum.CtrlLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblReason = New Spectrum.CtrlLabel()
        Me.txtReason = New Spectrum.CtrlTextBox()
        CType(Me.dgSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.lblReason, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgSearch
        '
        Me.dgSearch.AllowColMove = False
        Me.dgSearch.AllowColSelect = False
        Me.dgSearch.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.dgSearch.AllowUpdate = False
        Me.dgSearch.AllowUpdateOnBlur = False
        Me.dgSearch.BackColor = System.Drawing.SystemColors.Window
        Me.dgSearch.CaptionHeight = 17
        Me.dgSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgSearch.ExtendRightColumn = True
        Me.dgSearch.FilterBar = True
        Me.dgSearch.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.Standard
        Me.dgSearch.GroupByAreaVisible = False
        Me.dgSearch.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgSearch.Images.Add(CType(resources.GetObject("dgSearch.Images"), System.Drawing.Image))
        Me.dgSearch.Images.Add(CType(resources.GetObject("dgSearch.Images1"), System.Drawing.Image))
        Me.dgSearch.Location = New System.Drawing.Point(0, 0)
        Me.dgSearch.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder
        Me.dgSearch.Name = "dgSearch"
        Me.dgSearch.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgSearch.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgSearch.PreviewInfo.ZoomFactor = 75.0R
        Me.dgSearch.PrintInfo.PageSettings = CType(resources.GetObject("dgSearch.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgSearch.RecordSelectors = False
        Me.dgSearch.RowDivider.Color = System.Drawing.Color.Transparent
        Me.dgSearch.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None
        Me.dgSearch.RowHeight = 15
        Me.dgSearch.RowSubDividerColor = System.Drawing.Color.Transparent
        Me.dgSearch.Size = New System.Drawing.Size(741, 312)
        Me.dgSearch.TabIndex = 34
        Me.dgSearch.UseColumnStyles = False
        Me.dgSearch.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue
        Me.dgSearch.PropBag = resources.GetString("dgSearch.PropBag")
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOk.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdOk.Location = New System.Drawing.Point(576, 14)
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
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdCancel.Location = New System.Drawing.Point(661, 14)
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CtrlLabel1)
        Me.GroupBox1.Controls.Add(Me.cmdCancel)
        Me.GroupBox1.Controls.Add(Me.lblCount)
        Me.GroupBox1.Controls.Add(Me.cmdOk)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 308)
        Me.GroupBox1.MaximumSize = New System.Drawing.Size(995, 50)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(741, 50)
        Me.GroupBox1.TabIndex = 39
        Me.GroupBox1.TabStop = False
        '
        'lblReason
        '
        Me.lblReason.AttachedTextBoxName = Nothing
        Me.lblReason.AutoSize = True
        Me.lblReason.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblReason.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReason.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblReason.ForeColor = System.Drawing.Color.Black
        Me.lblReason.Location = New System.Drawing.Point(0, 289)
        Me.lblReason.Name = "lblReason"
        Me.lblReason.Size = New System.Drawing.Size(86, 15)
        Me.lblReason.TabIndex = 40
        Me.lblReason.Tag = Nothing
        Me.lblReason.Text = "*Remarks : "
        Me.lblReason.TextDetached = True
        Me.lblReason.Visible = False
        Me.lblReason.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtReason
        '
        Me.txtReason.Location = New System.Drawing.Point(83, 281)
        Me.txtReason.MaxLength = 500
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(175, 30)
        Me.txtReason.TabIndex = 41
        Me.txtReason.Text = ""
        Me.txtReason.Multiline = True
        Me.txtReason.Visible = False
        '
        'frmNCommonSearch
        '
        Me.AcceptButton = Me.cmdOk
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(741, 366)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtReason)
        Me.Controls.Add(Me.lblReason)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgSearch)
        Me.MaximumSize = New System.Drawing.Size(1000, 600)
        Me.MinimumSize = New System.Drawing.Size(618, 400)
        Me.Name = "frmNCommonSearch"
        Me.Text = "Search"
        CType(Me.dgSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.lblReason, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgSearch As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents cmdOk As Spectrum.CtrlBtn
    Friend WithEvents cmdCancel As Spectrum.CtrlBtn
    Friend WithEvents CtrlLabel1 As Spectrum.CtrlLabel
    Friend WithEvents lblCount As Spectrum.CtrlLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblReason As Spectrum.CtrlLabel
    Friend WithEvents txtReason As Spectrum.CtrlTextBox

End Class
