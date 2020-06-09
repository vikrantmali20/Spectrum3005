<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ArticleComboPopup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ArticleComboPopup))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.okCancelPanel = New System.Windows.Forms.Panel()
        Me.cancelBtn = New System.Windows.Forms.Button()
        Me.okBtn = New System.Windows.Forms.Button()
        Me.articlePanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.CtrlGrid1 = New Spectrum.CtrlGrid()
        Me.summaryPanel = New System.Windows.Forms.Panel()
        Me.amount = New System.Windows.Forms.Label()
        Me.totalAmount = New System.Windows.Forms.Label()
        Me.itemCount = New System.Windows.Forms.Label()
        Me.totalItem = New System.Windows.Forms.Label()
        Me.upgradeNextPanel = New System.Windows.Forms.Panel()
        Me.upgradeBtn = New System.Windows.Forms.Button()
        Me.upgradePanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CtrlNumberPad = New Spectrum.ctrlNumberPad()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtComboQuantity = New System.Windows.Forms.TextBox()
        Me.lblComboQuantity = New System.Windows.Forms.Label()
        Me.backBtn = New System.Windows.Forms.Button()
        Me.hdrLbl = New System.Windows.Forms.Label()
        Me.nextBtn = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.okCancelPanel.SuspendLayout()
        CType(Me.CtrlGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.summaryPanel.SuspendLayout()
        Me.upgradeNextPanel.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.94054!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.05946!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.okCancelPanel, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.articlePanel, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlGrid1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.summaryPanel, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.upgradeNextPanel, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.upgradePanel, 2, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlNumberPad, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.999774!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.53969!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41.78805!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.670611!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.735799!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.26608!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1234, 607)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.Label1.Location = New System.Drawing.Point(349, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Base Item"
        Me.Label1.Visible = False
        '
        'okCancelPanel
        '
        Me.okCancelPanel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.okCancelPanel.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.okCancelPanel, 3)
        Me.okCancelPanel.Controls.Add(Me.cancelBtn)
        Me.okCancelPanel.Controls.Add(Me.okBtn)
        Me.okCancelPanel.Location = New System.Drawing.Point(533, 557)
        Me.okCancelPanel.Name = "okCancelPanel"
        Me.okCancelPanel.Size = New System.Drawing.Size(167, 43)
        Me.okCancelPanel.TabIndex = 0
        '
        'cancelBtn
        '
        Me.cancelBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.cancelBtn.Location = New System.Drawing.Point(84, 5)
        Me.cancelBtn.Name = "cancelBtn"
        Me.cancelBtn.Size = New System.Drawing.Size(80, 35)
        Me.cancelBtn.TabIndex = 1
        Me.cancelBtn.Text = "Cancel"
        Me.cancelBtn.UseVisualStyleBackColor = True
        '
        'okBtn
        '
        Me.okBtn.Enabled = False
        Me.okBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.okBtn.Location = New System.Drawing.Point(3, 5)
        Me.okBtn.Name = "okBtn"
        Me.okBtn.Size = New System.Drawing.Size(80, 35)
        Me.okBtn.TabIndex = 0
        Me.okBtn.Text = "Ok"
        Me.okBtn.UseVisualStyleBackColor = True
        '
        'articlePanel
        '
        Me.articlePanel.AutoScroll = True
        Me.articlePanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.articlePanel.Location = New System.Drawing.Point(349, 68)
        Me.articlePanel.Name = "articlePanel"
        Me.articlePanel.Size = New System.Drawing.Size(882, 138)
        Me.articlePanel.TabIndex = 2
        '
        'CtrlGrid1
        '
        Me.CtrlGrid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.CtrlGrid1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.CtrlGrid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D
        Me.CtrlGrid1.CellButtonImage = CType(resources.GetObject("CtrlGrid1.CellButtonImage"), System.Drawing.Image)
        Me.CtrlGrid1.ColumnInfo = "10,1,0,0,0,120,Columns:0{Visible:False;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.CtrlGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlGrid1.ExtendLastCol = True
        Me.CtrlGrid1.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid
        Me.CtrlGrid1.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!)
        Me.CtrlGrid1.Location = New System.Drawing.Point(3, 68)
        Me.CtrlGrid1.Name = "CtrlGrid1"
        Me.CtrlGrid1.Rows.DefaultSize = 24
        Me.TableLayoutPanel1.SetRowSpan(Me.CtrlGrid1, 3)
        Me.CtrlGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.CtrlGrid1.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.CtrlGrid1.Size = New System.Drawing.Size(338, 394)
        Me.CtrlGrid1.StyleInfo = resources.GetString("CtrlGrid1.StyleInfo")
        Me.CtrlGrid1.TabIndex = 5
        Me.CtrlGrid1.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'summaryPanel
        '
        Me.summaryPanel.Controls.Add(Me.amount)
        Me.summaryPanel.Controls.Add(Me.totalAmount)
        Me.summaryPanel.Controls.Add(Me.itemCount)
        Me.summaryPanel.Controls.Add(Me.totalItem)
        Me.summaryPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.summaryPanel.Location = New System.Drawing.Point(3, 468)
        Me.summaryPanel.Name = "summaryPanel"
        Me.summaryPanel.Size = New System.Drawing.Size(338, 31)
        Me.summaryPanel.TabIndex = 6
        '
        'amount
        '
        Me.amount.AutoSize = True
        Me.amount.Location = New System.Drawing.Point(220, 9)
        Me.amount.Name = "amount"
        Me.amount.Size = New System.Drawing.Size(13, 13)
        Me.amount.TabIndex = 3
        Me.amount.Text = "0"
        Me.amount.Visible = False
        '
        'totalAmount
        '
        Me.totalAmount.AutoSize = True
        Me.totalAmount.Location = New System.Drawing.Point(7, 9)
        Me.totalAmount.Name = "totalAmount"
        Me.totalAmount.Size = New System.Drawing.Size(73, 13)
        Me.totalAmount.TabIndex = 2
        Me.totalAmount.Text = "Total Amount:"
        Me.totalAmount.Visible = False
        '
        'itemCount
        '
        Me.itemCount.AutoSize = True
        Me.itemCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.itemCount.Location = New System.Drawing.Point(176, 6)
        Me.itemCount.Name = "itemCount"
        Me.itemCount.Size = New System.Drawing.Size(16, 17)
        Me.itemCount.TabIndex = 1
        Me.itemCount.Text = "0"
        '
        'totalItem
        '
        Me.totalItem.AutoSize = True
        Me.totalItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.totalItem.Location = New System.Drawing.Point(106, 6)
        Me.totalItem.Name = "totalItem"
        Me.totalItem.Size = New System.Drawing.Size(74, 17)
        Me.totalItem.TabIndex = 0
        Me.totalItem.Text = "Total Item:"
        '
        'upgradeNextPanel
        '
        Me.upgradeNextPanel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.upgradeNextPanel.AutoSize = True
        Me.upgradeNextPanel.Controls.Add(Me.upgradeBtn)
        Me.upgradeNextPanel.Location = New System.Drawing.Point(131, 508)
        Me.upgradeNextPanel.Name = "upgradeNextPanel"
        Me.upgradeNextPanel.Size = New System.Drawing.Size(81, 37)
        Me.upgradeNextPanel.TabIndex = 4
        '
        'upgradeBtn
        '
        Me.upgradeBtn.Enabled = False
        Me.upgradeBtn.Location = New System.Drawing.Point(3, 4)
        Me.upgradeBtn.Name = "upgradeBtn"
        Me.upgradeBtn.Size = New System.Drawing.Size(75, 30)
        Me.upgradeBtn.TabIndex = 0
        Me.upgradeBtn.Text = "Upgrade"
        Me.upgradeBtn.UseVisualStyleBackColor = True
        Me.upgradeBtn.Visible = False
        '
        'upgradePanel
        '
        Me.upgradePanel.AutoScroll = True
        Me.upgradePanel.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.upgradePanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.upgradePanel.Location = New System.Drawing.Point(349, 232)
        Me.upgradePanel.Name = "upgradePanel"
        Me.TableLayoutPanel1.SetRowSpan(Me.upgradePanel, 3)
        Me.upgradePanel.Size = New System.Drawing.Size(882, 316)
        Me.upgradePanel.TabIndex = 7
        Me.upgradePanel.Visible = False
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.Label2.Location = New System.Drawing.Point(349, 209)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 20)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Upgradable Items"
        Me.Label2.Visible = False
        '
        'CtrlNumberPad
        '
        Me.CtrlNumberPad.AutoSize = True
        Me.CtrlNumberPad.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CtrlNumberPad.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlNumberPad.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.CtrlNumberPad.Location = New System.Drawing.Point(345, 66)
        Me.CtrlNumberPad.Margin = New System.Windows.Forms.Padding(1)
        Me.CtrlNumberPad.Name = "CtrlNumberPad"
        Me.TableLayoutPanel1.SetRowSpan(Me.CtrlNumberPad, 5)
        Me.CtrlNumberPad.Size = New System.Drawing.Size(0, 0)
        Me.CtrlNumberPad.TabIndex = 9
        '
        'Panel1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.Panel1, 3)
        Me.Panel1.Controls.Add(Me.txtComboQuantity)
        Me.Panel1.Controls.Add(Me.lblComboQuantity)
        Me.Panel1.Controls.Add(Me.backBtn)
        Me.Panel1.Controls.Add(Me.hdrLbl)
        Me.Panel1.Controls.Add(Me.nextBtn)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1234, 45)
        Me.Panel1.TabIndex = 10
        '
        'txtComboQuantity
        '
        Me.txtComboQuantity.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.txtComboQuantity.Location = New System.Drawing.Point(139, 10)
        Me.txtComboQuantity.Name = "txtComboQuantity"
        Me.txtComboQuantity.Size = New System.Drawing.Size(59, 26)
        Me.txtComboQuantity.TabIndex = 4
        '
        'lblComboQuantity
        '
        Me.lblComboQuantity.AutoSize = True
        Me.lblComboQuantity.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.lblComboQuantity.Location = New System.Drawing.Point(12, 11)
        Me.lblComboQuantity.Name = "lblComboQuantity"
        Me.lblComboQuantity.Size = New System.Drawing.Size(130, 20)
        Me.lblComboQuantity.TabIndex = 3
        Me.lblComboQuantity.Text = "Combo Quanity : "
        '
        'backBtn
        '
        Me.backBtn.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.backBtn.Enabled = False
        Me.backBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.backBtn.Location = New System.Drawing.Point(1071, 2)
        Me.backBtn.Margin = New System.Windows.Forms.Padding(0)
        Me.backBtn.Name = "backBtn"
        Me.backBtn.Size = New System.Drawing.Size(80, 35)
        Me.backBtn.TabIndex = 2
        Me.backBtn.Text = "Back"
        Me.backBtn.UseVisualStyleBackColor = True
        '
        'hdrLbl
        '
        Me.hdrLbl.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.hdrLbl.AutoSize = True
        Me.hdrLbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.hdrLbl.Location = New System.Drawing.Point(510, 15)
        Me.hdrLbl.Name = "hdrLbl"
        Me.hdrLbl.Size = New System.Drawing.Size(116, 17)
        Me.hdrLbl.TabIndex = 1
        Me.hdrLbl.Text = "Select 12 Donuts"
        '
        'nextBtn
        '
        Me.nextBtn.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.nextBtn.Enabled = False
        Me.nextBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.nextBtn.Location = New System.Drawing.Point(1152, 2)
        Me.nextBtn.Margin = New System.Windows.Forms.Padding(0)
        Me.nextBtn.Name = "nextBtn"
        Me.nextBtn.Size = New System.Drawing.Size(80, 35)
        Me.nextBtn.TabIndex = 1
        Me.nextBtn.Text = "Next"
        Me.nextBtn.UseVisualStyleBackColor = True
        '
        'ArticleComboPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1234, 607)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.KeyPreview = True
        Me.Name = "ArticleComboPopup"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Combo Popup"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.okCancelPanel.ResumeLayout(False)
        CType(Me.CtrlGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.summaryPanel.ResumeLayout(False)
        Me.summaryPanel.PerformLayout()
        Me.upgradeNextPanel.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents okCancelPanel As System.Windows.Forms.Panel
    Friend WithEvents okBtn As System.Windows.Forms.Button
    Friend WithEvents cancelBtn As System.Windows.Forms.Button
    Friend WithEvents hdrLbl As System.Windows.Forms.Label
    Friend WithEvents articlePanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents upgradeNextPanel As System.Windows.Forms.Panel
    Friend WithEvents nextBtn As System.Windows.Forms.Button
    Friend WithEvents upgradeBtn As System.Windows.Forms.Button
    Friend WithEvents CtrlGrid1 As Spectrum.CtrlGrid
    Friend WithEvents summaryPanel As System.Windows.Forms.Panel
    Friend WithEvents amount As System.Windows.Forms.Label
    Friend WithEvents totalAmount As System.Windows.Forms.Label
    Friend WithEvents itemCount As System.Windows.Forms.Label
    Friend WithEvents totalItem As System.Windows.Forms.Label
    Friend WithEvents backBtn As System.Windows.Forms.Button
    Friend WithEvents upgradePanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CtrlNumberPad As Spectrum.ctrlNumberPad
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblComboQuantity As System.Windows.Forms.Label
    Friend WithEvents txtComboQuantity As System.Windows.Forms.TextBox

End Class
