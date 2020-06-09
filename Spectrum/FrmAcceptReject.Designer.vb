<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmacceptReject
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmacceptReject))
        Me.TxtDeliveryTime = New Spectrum.CtrlTextBox()
        Me.cmdCancel = New Spectrum.CtrlBtn()
        Me.cmdOk = New Spectrum.CtrlBtn()
        Me.lblRemark = New Spectrum.CtrlLabel()
        Me.PnRejectReason = New System.Windows.Forms.Panel()
        Me.CboRejectReason = New Spectrum.ctrlCombo()
        Me.CboRejectionReason = New Spectrum.CtrlLabel()
        Me.pnDeliveryTime = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.PaRejectReamrk = New System.Windows.Forms.Panel()
        Me.CtrlRejectionRemark = New Spectrum.CtrlTextBox()
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        Me.pnButtons = New System.Windows.Forms.Panel()
        CType(Me.TxtDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnRejectReason.SuspendLayout()
        CType(Me.CboRejectReason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboRejectionReason, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnDeliveryTime.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.PaRejectReamrk.SuspendLayout()
        CType(Me.CtrlRejectionRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'TxtDeliveryTime
        '
        Me.TxtDeliveryTime.AutoSize = False
        Me.TxtDeliveryTime.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.TxtDeliveryTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtDeliveryTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDeliveryTime.Location = New System.Drawing.Point(3, 19)
        Me.TxtDeliveryTime.MinimumSize = New System.Drawing.Size(10, 21)
        Me.TxtDeliveryTime.MoveToNxtCtrl = Nothing
        Me.TxtDeliveryTime.Name = "TxtDeliveryTime"
        Me.TxtDeliveryTime.Size = New System.Drawing.Size(208, 21)
        Me.TxtDeliveryTime.TabIndex = 0
        Me.TxtDeliveryTime.Tag = Nothing
        Me.TxtDeliveryTime.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.TxtDeliveryTime.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdCancel.Location = New System.Drawing.Point(110, 0)
        Me.cmdCancel.MoveToNxtCtrl = Nothing
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.SetArticleCode = Nothing
        Me.cmdCancel.SetRowIndex = 0
        Me.cmdCancel.Size = New System.Drawing.Size(59, 24)
        Me.cmdCancel.TabIndex = 2
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdCancel.UseVisualStyleBackColor = True
        Me.cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOk.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdOk.Location = New System.Drawing.Point(38, 0)
        Me.cmdOk.MoveToNxtCtrl = Nothing
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.SetArticleCode = Nothing
        Me.cmdOk.SetRowIndex = 0
        Me.cmdOk.Size = New System.Drawing.Size(56, 24)
        Me.cmdOk.TabIndex = 1
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdOk.UseVisualStyleBackColor = True
        Me.cmdOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblRemark
        '
        Me.lblRemark.AttachedTextBoxName = Nothing
        Me.lblRemark.AutoSize = True
        Me.lblRemark.BackColor = System.Drawing.Color.Transparent
        Me.lblRemark.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblRemark.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemark.ForeColor = System.Drawing.Color.Black
        Me.lblRemark.Location = New System.Drawing.Point(0, 0)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(104, 16)
        Me.lblRemark.TabIndex = 38
        Me.lblRemark.Tag = Nothing
        Me.lblRemark.Text = "EnterRemark *"
        Me.lblRemark.TextDetached = True
        Me.lblRemark.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'PnRejectReason
        '
        Me.PnRejectReason.AllowDrop = True
        Me.PnRejectReason.AutoScroll = True
        Me.PnRejectReason.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.PnRejectReason.Controls.Add(Me.CboRejectReason)
        Me.PnRejectReason.Controls.Add(Me.CboRejectionReason)
        Me.PnRejectReason.Location = New System.Drawing.Point(3, 56)
        Me.PnRejectReason.Name = "PnRejectReason"
        Me.PnRejectReason.Size = New System.Drawing.Size(214, 47)
        Me.PnRejectReason.TabIndex = 39
        '
        'CboRejectReason
        '
        Me.CboRejectReason.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.CboRejectReason.AutoCompletion = True
        Me.CboRejectReason.AutoDropDown = True
        Me.CboRejectReason.Caption = ""
        Me.CboRejectReason.CaptionHeight = 17
        Me.CboRejectReason.CaptionVisible = False
        Me.CboRejectReason.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CboRejectReason.ColumnCaptionHeight = 17
        Me.CboRejectReason.ColumnFooterHeight = 17
        Me.CboRejectReason.ColumnHeaders = False
        Me.CboRejectReason.ContentHeight = 16
        Me.CboRejectReason.ctrlTextDbColumn = ""
        Me.CboRejectReason.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.CboRejectReason.EditorBackColor = System.Drawing.SystemColors.Window
        Me.CboRejectReason.EditorFont = New System.Drawing.Font("Verdana", 8.25!)
        Me.CboRejectReason.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.CboRejectReason.EditorHeight = 16
        Me.CboRejectReason.Images.Add(CType(resources.GetObject("CboRejectReason.Images"), System.Drawing.Image))
        Me.CboRejectReason.ItemHeight = 15
        Me.CboRejectReason.Location = New System.Drawing.Point(6, 19)
        Me.CboRejectReason.MatchEntryTimeout = CType(2000, Long)
        Me.CboRejectReason.MaxDropDownItems = CType(5, Short)
        Me.CboRejectReason.MaxLength = 32767
        Me.CboRejectReason.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.CboRejectReason.MoveToNxtCtrl = Nothing
        Me.CboRejectReason.Name = "CboRejectReason"
        Me.CboRejectReason.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.CboRejectReason.Size = New System.Drawing.Size(208, 22)
        Me.CboRejectReason.strSelectStmt = ""
        Me.CboRejectReason.TabIndex = 0
        Me.CboRejectReason.Text = "CtrlCombo1"
        Me.CboRejectReason.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.CboRejectReason.PropBag = resources.GetString("CboRejectReason.PropBag")
        '
        'CboRejectionReason
        '
        Me.CboRejectionReason.AttachedTextBoxName = Nothing
        Me.CboRejectionReason.AutoSize = True
        Me.CboRejectionReason.BackColor = System.Drawing.Color.Transparent
        Me.CboRejectionReason.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CboRejectionReason.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboRejectionReason.ForeColor = System.Drawing.Color.Black
        Me.CboRejectionReason.Location = New System.Drawing.Point(3, 0)
        Me.CboRejectionReason.Name = "CboRejectionReason"
        Me.CboRejectionReason.Size = New System.Drawing.Size(129, 16)
        Me.CboRejectionReason.TabIndex = 39
        Me.CboRejectionReason.Tag = Nothing
        Me.CboRejectionReason.Text = "Rejection Reason*"
        Me.CboRejectionReason.TextDetached = True
        Me.CboRejectionReason.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'pnDeliveryTime
        '
        Me.pnDeliveryTime.AllowDrop = True
        Me.pnDeliveryTime.AutoScroll = True
        Me.pnDeliveryTime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnDeliveryTime.Controls.Add(Me.lblRemark)
        Me.pnDeliveryTime.Controls.Add(Me.TxtDeliveryTime)
        Me.pnDeliveryTime.Location = New System.Drawing.Point(3, 3)
        Me.pnDeliveryTime.Name = "pnDeliveryTime"
        Me.pnDeliveryTime.Size = New System.Drawing.Size(214, 47)
        Me.pnDeliveryTime.TabIndex = 39
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.pnDeliveryTime)
        Me.FlowLayoutPanel1.Controls.Add(Me.PnRejectReason)
        Me.FlowLayoutPanel1.Controls.Add(Me.PaRejectReamrk)
        Me.FlowLayoutPanel1.Controls.Add(Me.pnButtons)
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(1, 3)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(226, 220)
        Me.FlowLayoutPanel1.TabIndex = 40
        '
        'PaRejectReamrk
        '
        Me.PaRejectReamrk.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.PaRejectReamrk.Controls.Add(Me.CtrlRejectionRemark)
        Me.PaRejectReamrk.Controls.Add(Me.CtrlLabel1)
        Me.PaRejectReamrk.Location = New System.Drawing.Point(3, 109)
        Me.PaRejectReamrk.Name = "PaRejectReamrk"
        Me.PaRejectReamrk.Size = New System.Drawing.Size(214, 68)
        Me.PaRejectReamrk.TabIndex = 40
        '
        'CtrlRejectionRemark
        '
        Me.CtrlRejectionRemark.AutoSize = False
        Me.CtrlRejectionRemark.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.CtrlRejectionRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlRejectionRemark.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlRejectionRemark.Location = New System.Drawing.Point(6, 19)
        Me.CtrlRejectionRemark.MinimumSize = New System.Drawing.Size(10, 21)
        Me.CtrlRejectionRemark.MoveToNxtCtrl = Nothing
        Me.CtrlRejectionRemark.Multiline = True
        Me.CtrlRejectionRemark.Name = "CtrlRejectionRemark"
        Me.CtrlRejectionRemark.Size = New System.Drawing.Size(205, 46)
        Me.CtrlRejectionRemark.TabIndex = 39
        Me.CtrlRejectionRemark.Tag = Nothing
        Me.CtrlRejectionRemark.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlRejectionRemark.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel1
        '
        Me.CtrlLabel1.AttachedTextBoxName = Nothing
        Me.CtrlLabel1.AutoSize = True
        Me.CtrlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlLabel1.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel1.Location = New System.Drawing.Point(3, 0)
        Me.CtrlLabel1.Name = "CtrlLabel1"
        Me.CtrlLabel1.Size = New System.Drawing.Size(122, 16)
        Me.CtrlLabel1.TabIndex = 39
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.Text = "Rejection Remark"
        Me.CtrlLabel1.TextDetached = True
        Me.CtrlLabel1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'pnButtons
        '
        Me.pnButtons.Controls.Add(Me.cmdCancel)
        Me.pnButtons.Controls.Add(Me.cmdOk)
        Me.pnButtons.Location = New System.Drawing.Point(3, 183)
        Me.pnButtons.Name = "pnButtons"
        Me.pnButtons.Size = New System.Drawing.Size(197, 27)
        Me.pnButtons.TabIndex = 40
        '
        'frmacceptReject
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(227, 226)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmacceptReject"
        Me.Text = "Track Customers"
        CType(Me.TxtDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnRejectReason.ResumeLayout(False)
        Me.PnRejectReason.PerformLayout()
        CType(Me.CboRejectReason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboRejectionReason, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnDeliveryTime.ResumeLayout(False)
        Me.pnDeliveryTime.PerformLayout()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.PaRejectReamrk.ResumeLayout(False)
        Me.PaRejectReamrk.PerformLayout()
        CType(Me.CtrlRejectionRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TxtDeliveryTime As Spectrum.CtrlTextBox
    Friend WithEvents cmdCancel As Spectrum.CtrlBtn
    Friend WithEvents cmdOk As Spectrum.CtrlBtn
    Friend WithEvents lblRemark As Spectrum.CtrlLabel
    Friend WithEvents PnRejectReason As System.Windows.Forms.Panel
    Friend WithEvents pnDeliveryTime As System.Windows.Forms.Panel
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnButtons As System.Windows.Forms.Panel
    Friend WithEvents CboRejectReason As Spectrum.ctrlCombo
    Friend WithEvents CboRejectionReason As Spectrum.CtrlLabel
    Friend WithEvents PaRejectReamrk As System.Windows.Forms.Panel
    Friend WithEvents CtrlLabel1 As Spectrum.CtrlLabel
    Friend WithEvents CtrlRejectionRemark As Spectrum.CtrlTextBox
End Class
