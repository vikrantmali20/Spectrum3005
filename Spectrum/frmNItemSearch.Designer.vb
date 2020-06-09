<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNItemSearch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNItemSearch))
        Me.sizTop = New C1.Win.C1Sizer.C1Sizer()
        Me.cmdShow = New Spectrum.CtrlBtn()
        Me.trvArticle = New System.Windows.Forms.TreeView()
        Me.cmdAdvancedSearch = New Spectrum.CtrlBtn()
        Me.sizBottom = New C1.Win.C1Sizer.C1Sizer()
        Me.cmdOK = New Spectrum.CtrlBtn()
        Me.cmdCancel = New Spectrum.CtrlBtn()
        Me.dgItemSearch = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        CType(Me.sizTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sizTop.SuspendLayout()
        CType(Me.sizBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sizBottom.SuspendLayout()
        CType(Me.dgItemSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sizTop
        '
        Me.sizTop.Border.Color = System.Drawing.Color.LightSlateGray
        Me.sizTop.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.sizTop.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.sizTop.Controls.Add(Me.cmdShow)
        Me.sizTop.Controls.Add(Me.trvArticle)
        Me.sizTop.Controls.Add(Me.cmdAdvancedSearch)
        Me.sizTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.sizTop.GridDefinition = "94.8979591836735:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "99.0157480314961:False:False;"
        Me.sizTop.Location = New System.Drawing.Point(0, 0)
        Me.sizTop.Name = "sizTop"
        Me.sizTop.Size = New System.Drawing.Size(1016, 196)
        Me.sizTop.TabIndex = 0
        Me.sizTop.TabStop = False
        Me.sizTop.Text = "C1Sizer1"
        '
        'cmdShow
        '
        Me.cmdShow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdShow.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdShow.Location = New System.Drawing.Point(772, 154)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.SetArticleCode = Nothing
        Me.cmdShow.SetRowIndex = 0
        Me.cmdShow.Size = New System.Drawing.Size(98, 23)
        Me.cmdShow.TabIndex = 2
        Me.cmdShow.Text = "&Show"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdShow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdShow.UseVisualStyleBackColor = True
        Me.cmdShow.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'trvArticle
        '
        Me.trvArticle.Location = New System.Drawing.Point(12, 8)
        Me.trvArticle.Name = "trvArticle"
        Me.trvArticle.Size = New System.Drawing.Size(740, 182)
        Me.trvArticle.TabIndex = 1
        '
        'cmdAdvancedSearch
        '
        Me.cmdAdvancedSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAdvancedSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdAdvancedSearch.Location = New System.Drawing.Point(876, 154)
        Me.cmdAdvancedSearch.Name = "cmdAdvancedSearch"
        Me.cmdAdvancedSearch.SetArticleCode = Nothing
        Me.cmdAdvancedSearch.SetRowIndex = 0
        Me.cmdAdvancedSearch.Size = New System.Drawing.Size(128, 23)
        Me.cmdAdvancedSearch.TabIndex = 0
        Me.cmdAdvancedSearch.Text = "&Advance Search"
        Me.cmdAdvancedSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdAdvancedSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdAdvancedSearch.UseVisualStyleBackColor = True
        Me.cmdAdvancedSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'sizBottom
        '
        Me.sizBottom.Border.Color = System.Drawing.Color.LightSlateGray
        Me.sizBottom.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.sizBottom.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.sizBottom.Controls.Add(Me.cmdOK)
        Me.sizBottom.Controls.Add(Me.cmdCancel)
        Me.sizBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.sizBottom.GridDefinition = "73.6842105263158:False:True;" & Global.Microsoft.VisualBasic.ChrW(9) & "99.0157480314961:False:False;"
        Me.sizBottom.Location = New System.Drawing.Point(0, 528)
        Me.sizBottom.Name = "sizBottom"
        Me.sizBottom.Size = New System.Drawing.Size(1016, 38)
        Me.sizBottom.TabIndex = 0
        Me.sizBottom.TabStop = False
        Me.sizBottom.Text = "C1Sizer1"
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOK.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdOK.Location = New System.Drawing.Point(949, 7)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.SetArticleCode = Nothing
        Me.cmdOK.SetRowIndex = 0
        Me.cmdOK.Size = New System.Drawing.Size(61, 23)
        Me.cmdOK.TabIndex = 0
        Me.cmdOK.Text = "&Ok"
        Me.cmdOK.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdOK.UseVisualStyleBackColor = True
        Me.cmdOK.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdCancel.Location = New System.Drawing.Point(1001, 7)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.SetArticleCode = Nothing
        Me.cmdCancel.SetRowIndex = 0
        Me.cmdCancel.Size = New System.Drawing.Size(9, 23)
        Me.cmdCancel.TabIndex = 0
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdCancel.UseVisualStyleBackColor = True
        Me.cmdCancel.Visible = False
        Me.cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'dgItemSearch
        '
        Me.dgItemSearch.AllowColMove = False
        Me.dgItemSearch.AllowColSelect = False
        Me.dgItemSearch.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.dgItemSearch.AllowUpdate = False
        Me.dgItemSearch.AllowUpdateOnBlur = False
        Me.dgItemSearch.BackColor = System.Drawing.Color.White
        Me.dgItemSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgItemSearch.CaptionHeight = 17
        Me.dgItemSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgItemSearch.ExtendRightColumn = True
        Me.dgItemSearch.FilterBar = True
        Me.dgItemSearch.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.Standard
        Me.dgItemSearch.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgItemSearch.GroupByAreaVisible = False
        Me.dgItemSearch.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgItemSearch.Images.Add(CType(resources.GetObject("dgItemSearch.Images"), System.Drawing.Image))
        Me.dgItemSearch.Location = New System.Drawing.Point(0, 196)
        Me.dgItemSearch.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.dgItemSearch.Name = "dgItemSearch"
        Me.dgItemSearch.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgItemSearch.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgItemSearch.PreviewInfo.ZoomFactor = 75.0R
        Me.dgItemSearch.PrintInfo.PageSettings = CType(resources.GetObject("dgItemSearch.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgItemSearch.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.dgItemSearch.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None
        Me.dgItemSearch.RowHeight = 15
        Me.dgItemSearch.Size = New System.Drawing.Size(1016, 332)
        Me.dgItemSearch.TabIndex = 0
        Me.dgItemSearch.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue
        Me.dgItemSearch.PropBag = resources.GetString("dgItemSearch.PropBag")
        '
        'frmNItemSearch
        '
        Me.AcceptButton = Me.cmdOK
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(1016, 566)
        Me.Controls.Add(Me.dgItemSearch)
        Me.Controls.Add(Me.sizBottom)
        Me.Controls.Add(Me.sizTop)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1000, 500)
        Me.Name = "frmNItemSearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Item search"
        CType(Me.sizTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sizTop.ResumeLayout(False)
        CType(Me.sizBottom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sizBottom.ResumeLayout(False)
        CType(Me.dgItemSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sizTop As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents cmdAdvancedSearch As Spectrum.CtrlBtn
    Friend WithEvents sizBottom As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents cmdCancel As Spectrum.CtrlBtn
    Friend WithEvents cmdOK As Spectrum.CtrlBtn
    Friend WithEvents dgItemSearch As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents trvArticle As System.Windows.Forms.TreeView
    Friend WithEvents cmdShow As Spectrum.CtrlBtn

End Class
