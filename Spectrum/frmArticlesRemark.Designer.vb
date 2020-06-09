<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmArticlesRemark
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmArticlesRemark))
        Me.CtrllblRemark = New Spectrum.CtrlLabel()
        Me.CtrlTxtRemark = New Spectrum.CtrlTextBox()
        Me.pnButtons = New System.Windows.Forms.Panel()
        Me.cmdCancel = New Spectrum.CtrlBtn()
        Me.cmdOk = New Spectrum.CtrlBtn()
        Me.lblStatus = New Spectrum.CtrlLabel()
        Me.cbStatus = New Spectrum.ctrlCombo()
        CType(Me.CtrllblRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlTxtRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnButtons.SuspendLayout()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CtrllblRemark
        '
        Me.CtrllblRemark.AttachedTextBoxName = Nothing
        Me.CtrllblRemark.AutoSize = True
        Me.CtrllblRemark.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrllblRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrllblRemark.ForeColor = System.Drawing.Color.Black
        Me.CtrllblRemark.Location = New System.Drawing.Point(79, 51)
        Me.CtrllblRemark.Name = "CtrllblRemark"
        Me.CtrllblRemark.Size = New System.Drawing.Size(54, 15)
        Me.CtrllblRemark.TabIndex = 0
        Me.CtrllblRemark.Tag = Nothing
        Me.CtrllblRemark.Text = "Remark"
        Me.CtrllblRemark.TextDetached = True
        '
        'CtrlTxtRemark
        '
        Me.CtrlTxtRemark.AutoSize = False
        Me.CtrlTxtRemark.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.CtrlTxtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlTxtRemark.Location = New System.Drawing.Point(177, 51)
        Me.CtrlTxtRemark.MinimumSize = New System.Drawing.Size(10, 21)
        Me.CtrlTxtRemark.MoveToNxtCtrl = Nothing
        Me.CtrlTxtRemark.Name = "CtrlTxtRemark"
        Me.CtrlTxtRemark.Size = New System.Drawing.Size(209, 66)
        Me.CtrlTxtRemark.TabIndex = 1
        Me.CtrlTxtRemark.Tag = Nothing
        Me.CtrlTxtRemark.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlTxtRemark.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'pnButtons
        '
        Me.pnButtons.Controls.Add(Me.cmdCancel)
        Me.pnButtons.Controls.Add(Me.cmdOk)
        Me.pnButtons.Location = New System.Drawing.Point(12, 138)
        Me.pnButtons.Name = "pnButtons"
        Me.pnButtons.Size = New System.Drawing.Size(374, 30)
        Me.pnButtons.TabIndex = 41
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdCancel.Location = New System.Drawing.Point(307, 3)
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
        Me.cmdOk.Location = New System.Drawing.Point(244, 3)
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
        'lblStatus
        '
        Me.lblStatus.AttachedTextBoxName = Nothing
        Me.lblStatus.AutoSize = True
        Me.lblStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblStatus.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatus.ForeColor = System.Drawing.Color.Black
        Me.lblStatus.Location = New System.Drawing.Point(78, 28)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(54, 15)
        Me.lblStatus.TabIndex = 45
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
        Me.cbStatus.Location = New System.Drawing.Point(176, 28)
        Me.cbStatus.MatchEntryTimeout = CType(2000, Long)
        Me.cbStatus.MaxDropDownItems = CType(5, Short)
        Me.cbStatus.MaxLength = 32767
        Me.cbStatus.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cbStatus.MoveToNxtCtrl = Nothing
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cbStatus.Size = New System.Drawing.Size(165, 21)
        Me.cbStatus.strSelectStmt = ""
        Me.cbStatus.TabIndex = 44
        Me.cbStatus.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cbStatus.PropBag = resources.GetString("cbStatus.PropBag")
        '
        'frmArticlesRemark
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(418, 170)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.cbStatus)
        Me.Controls.Add(Me.pnButtons)
        Me.Controls.Add(Me.CtrlTxtRemark)
        Me.Controls.Add(Me.CtrllblRemark)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmArticlesRemark"
        Me.Text = "Add Remark"
        CType(Me.CtrllblRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlTxtRemark, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnButtons.ResumeLayout(False)
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CtrllblRemark As Spectrum.CtrlLabel
    Friend WithEvents CtrlTxtRemark As Spectrum.CtrlTextBox
    Friend WithEvents pnButtons As System.Windows.Forms.Panel
    Friend WithEvents cmdCancel As Spectrum.CtrlBtn
    Friend WithEvents cmdOk As Spectrum.CtrlBtn
    Friend WithEvents lblStatus As Spectrum.CtrlLabel
    Friend WithEvents cbStatus As Spectrum.ctrlCombo
End Class
