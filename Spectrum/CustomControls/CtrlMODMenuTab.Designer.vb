<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrlMODMenuTab
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Me.ArticleContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.C1DockingTabPage2 = New C1.Win.C1Command.C1DockingTabPage()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.FlowPanelMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddNewArticleMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tabControlMain = New C1.Win.C1Command.C1DockingTab()
        Me.C1DockingTabPage1 = New C1.Win.C1Command.C1DockingTabPage()
        Me.flpMenuButton = New System.Windows.Forms.FlowLayoutPanel()
        Me.ArticleCategoryMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddCategoryMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.CategoryField = New System.Windows.Forms.TextBox()
        Me.ArticleContextMenuStrip.SuspendLayout()
        Me.C1DockingTabPage2.SuspendLayout()
        Me.FlowPanelMenuStrip.SuspendLayout()
        CType(Me.tabControlMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControlMain.SuspendLayout()
        Me.C1DockingTabPage1.SuspendLayout()
        Me.ArticleCategoryMenuStrip.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ArticleContextMenuStrip
        '
        Me.ArticleContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem, Me.RemoveToolStripMenuItem1})
        Me.ArticleContextMenuStrip.Name = "ArticleContextMenuStrip"
        Me.ArticleContextMenuStrip.Size = New System.Drawing.Size(118, 48)
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'RemoveToolStripMenuItem1
        '
        Me.RemoveToolStripMenuItem1.Name = "RemoveToolStripMenuItem1"
        Me.RemoveToolStripMenuItem1.Size = New System.Drawing.Size(117, 22)
        Me.RemoveToolStripMenuItem1.Text = "Remove"
        '
        'C1DockingTabPage2
        '
        Me.C1DockingTabPage2.Controls.Add(Me.FlowLayoutPanel1)
        Me.C1DockingTabPage2.Location = New System.Drawing.Point(2, 25)
        Me.C1DockingTabPage2.Name = "C1DockingTabPage2"
        Me.C1DockingTabPage2.Size = New System.Drawing.Size(694, 461)
        Me.C1DockingTabPage2.TabIndex = 1
        Me.C1DockingTabPage2.Text = "Jumbo"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(694, 461)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'FlowPanelMenuStrip
        '
        Me.FlowPanelMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddNewArticleMenuItem})
        Me.FlowPanelMenuStrip.Name = "FlowPanelMenuStrip"
        Me.FlowPanelMenuStrip.Size = New System.Drawing.Size(97, 26)
        '
        'AddNewArticleMenuItem
        '
        Me.AddNewArticleMenuItem.Name = "AddNewArticleMenuItem"
        Me.AddNewArticleMenuItem.Size = New System.Drawing.Size(96, 22)
        Me.AddNewArticleMenuItem.Text = "Add"
        '
        'tabControlMain
        '
        Me.tabControlMain.Controls.Add(Me.C1DockingTabPage1)
        Me.tabControlMain.Controls.Add(Me.C1DockingTabPage2)
        Me.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabControlMain.Location = New System.Drawing.Point(3, 29)
        Me.tabControlMain.MultiLine = True
        Me.tabControlMain.Name = "tabControlMain"
        Me.tabControlMain.SelectedTabBold = True
        Me.tabControlMain.ShowToolTips = True
        Me.tabControlMain.Size = New System.Drawing.Size(698, 488)
        Me.tabControlMain.TabIndex = 0
        Me.tabControlMain.TabLook = CType((C1.Win.C1Command.ButtonLookFlags.Text Or C1.Win.C1Command.ButtonLookFlags.Image), C1.Win.C1Command.ButtonLookFlags)
        Me.tabControlMain.TabsSpacing = 5
        Me.tabControlMain.TabStyle = C1.Win.C1Command.TabStyleEnum.Classic
        Me.tabControlMain.VisualStyle = C1.Win.C1Command.VisualStyle.Classic
        Me.tabControlMain.VisualStyleBase = C1.Win.C1Command.VisualStyle.Classic
        '
        'C1DockingTabPage1
        '
        Me.C1DockingTabPage1.Controls.Add(Me.flpMenuButton)
        Me.C1DockingTabPage1.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1DockingTabPage1.Location = New System.Drawing.Point(2, 25)
        Me.C1DockingTabPage1.Name = "C1DockingTabPage1"
        Me.C1DockingTabPage1.Size = New System.Drawing.Size(694, 461)
        Me.C1DockingTabPage1.TabIndex = 0
        Me.C1DockingTabPage1.Text = "Today"
        '
        'flpMenuButton
        '
        Me.flpMenuButton.AutoSize = True
        Me.flpMenuButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.flpMenuButton.ContextMenuStrip = Me.FlowPanelMenuStrip
        Me.flpMenuButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpMenuButton.Location = New System.Drawing.Point(0, 0)
        Me.flpMenuButton.Name = "flpMenuButton"
        Me.flpMenuButton.Size = New System.Drawing.Size(694, 461)
        Me.flpMenuButton.TabIndex = 1
        '
        'ArticleCategoryMenuStrip
        '
        Me.ArticleCategoryMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddCategoryMenuItem, Me.RemoveMenuItem})
        Me.ArticleCategoryMenuStrip.Name = "ArticleCategoryMenuStrip"
        Me.ArticleCategoryMenuStrip.Size = New System.Drawing.Size(118, 48)
        '
        'AddCategoryMenuItem
        '
        Me.AddCategoryMenuItem.Name = "AddCategoryMenuItem"
        Me.AddCategoryMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.AddCategoryMenuItem.Text = "Add"
        '
        'RemoveMenuItem
        '
        Me.RemoveMenuItem.Name = "RemoveMenuItem"
        Me.RemoveMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.RemoveMenuItem.Text = "Remove"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoScroll = True
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.CategoryField, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.tabControlMain, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(704, 520)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'CategoryField
        '
        Me.CategoryField.Dock = System.Windows.Forms.DockStyle.Right
        Me.CategoryField.Location = New System.Drawing.Point(601, 3)
        Me.CategoryField.Name = "CategoryField"
        Me.CategoryField.Size = New System.Drawing.Size(100, 20)
        Me.CategoryField.TabIndex = 0
        Me.CategoryField.Visible = False
        '
        'CtrlMODMenuTab
        '
        ' Me.TabParent = Nothing
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "CtrlMODMenuTab"
        Me.Size = New System.Drawing.Size(704, 520)
        Me.ArticleContextMenuStrip.ResumeLayout(False)
        Me.C1DockingTabPage2.ResumeLayout(False)
        Me.FlowPanelMenuStrip.ResumeLayout(False)
        CType(Me.tabControlMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControlMain.ResumeLayout(False)
        Me.C1DockingTabPage1.ResumeLayout(False)
        Me.C1DockingTabPage1.PerformLayout()
        Me.ArticleCategoryMenuStrip.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ArticleContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents C1DockingTabPage2 As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents tabControlMain As C1.Win.C1Command.C1DockingTab
    Friend WithEvents C1DockingTabPage1 As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents ArticleCategoryMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddCategoryMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents flpMenuButton As System.Windows.Forms.FlowLayoutPanel
    'Friend WithEvents CategoryField As System.Windows.Forms.TextBox
    'Friend WithEvents CategoryFieldLabel As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CategoryField As System.Windows.Forms.TextBox
    Friend WithEvents FlowPanelMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddNewArticleMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents RemoveMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
