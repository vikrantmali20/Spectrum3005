<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNStockCheck
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
        Me.txtItemCode = New Spectrum.AndroidSearchTextBox(Me.components)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNStockCheck))
        Me.C1Sizer1 = New C1.Win.C1Sizer.C1Sizer()
        Me.txtItemCode = New Spectrum.AndroidSearchTextBox(Me.components)
        Me.lblPhysicalQty = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblAvailableQty = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblNextAvailabelDate = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblCalCWSStatus = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblOR = New System.Windows.Forms.Label()
        Me.lblEAN = New System.Windows.Forms.Label()
        Me.BtnGetStock = New Spectrum.CtrlBtn()
        Me.btnSearchBirthListID = New Spectrum.CtrlBtn()
        Me.lblCalArticleDescri = New System.Windows.Forms.Label()
        'Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtEAN = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnCancel = New Spectrum.CtrlBtn()
        Me.btnOK = New Spectrum.CtrlBtn()
        Me.dgItemSearch = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgItemSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'C1Sizer1
        '
        Me.C1Sizer1.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1Sizer1.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1Sizer1.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1Sizer1.Controls.Add(Me.txtItemCode)
        Me.C1Sizer1.Controls.Add(Me.lblPhysicalQty)
        Me.C1Sizer1.Controls.Add(Me.Label6)
        Me.C1Sizer1.Controls.Add(Me.lblAvailableQty)
        Me.C1Sizer1.Controls.Add(Me.Label2)
        Me.C1Sizer1.Controls.Add(Me.lblNextAvailabelDate)
        Me.C1Sizer1.Controls.Add(Me.Label5)
        Me.C1Sizer1.Controls.Add(Me.lblCalCWSStatus)
        Me.C1Sizer1.Controls.Add(Me.Label1)
        Me.C1Sizer1.Controls.Add(Me.Label4)
        Me.C1Sizer1.Controls.Add(Me.lblOR)
        Me.C1Sizer1.Controls.Add(Me.lblEAN)
        Me.C1Sizer1.Controls.Add(Me.BtnGetStock)
        Me.C1Sizer1.Controls.Add(Me.btnSearchBirthListID)
        Me.C1Sizer1.Controls.Add(Me.lblCalArticleDescri)
        'Me.C1Sizer1.Controls.Add(Me.txtItemCode)
        Me.C1Sizer1.Controls.Add(Me.Label3)
        Me.C1Sizer1.Controls.Add(Me.txtEAN)
        Me.C1Sizer1.Dock = System.Windows.Forms.DockStyle.Top
        Me.C1Sizer1.GridDefinition = resources.GetString("C1Sizer1.GridDefinition")
        Me.C1Sizer1.Location = New System.Drawing.Point(0, 0)
        Me.C1Sizer1.Name = "C1Sizer1"
        Me.C1Sizer1.Size = New System.Drawing.Size(861, 178)
        Me.C1Sizer1.TabIndex = 0
        Me.C1Sizer1.TabStop = False
        Me.C1Sizer1.Text = "C1Sizer1"
        '
        'txtItemCode
        '
        Me.txtItemCode.IsItemSelected = False
        Me.txtItemCode.Location = New System.Drawing.Point(206, 5)
        Me.txtItemCode.lstNames = CType(resources.GetObject("txtItemCode.lstNames"), System.Collections.Generic.List(Of String))
        Me.txtItemCode.MaxLength = 35
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.Size = New System.Drawing.Size(363, 21)
        Me.txtItemCode.TabIndex = 18
        '
        'lblPhysicalQty
        '
        Me.lblPhysicalQty.AutoSize = True
        Me.lblPhysicalQty.Location = New System.Drawing.Point(725, 117)
        Me.lblPhysicalQty.Name = "lblPhysicalQty"
        Me.lblPhysicalQty.Size = New System.Drawing.Size(28, 13)
        Me.lblPhysicalQty.TabIndex = 17
        Me.lblPhysicalQty.Text = "N/A"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(618, 117)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Physical Qty"
        '
        'lblAvailableQty
        '
        Me.lblAvailableQty.AutoSize = True
        Me.lblAvailableQty.Location = New System.Drawing.Point(725, 151)
        Me.lblAvailableQty.Name = "lblAvailableQty"
        Me.lblAvailableQty.Size = New System.Drawing.Size(28, 13)
        Me.lblAvailableQty.TabIndex = 15
        Me.lblAvailableQty.Text = "N/A"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(618, 151)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Available Qty"
        '
        'lblNextAvailabelDate
        '
        Me.lblNextAvailabelDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNextAvailabelDate.AutoSize = True
        Me.lblNextAvailabelDate.Location = New System.Drawing.Point(206, 151)
        Me.lblNextAvailabelDate.Name = "lblNextAvailabelDate"
        Me.lblNextAvailabelDate.Size = New System.Drawing.Size(28, 13)
        Me.lblNextAvailabelDate.TabIndex = 13
        Me.lblNextAvailabelDate.Text = "N/A"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(5, 151)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(197, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Next Available Date                  :"
        '
        'lblCalCWSStatus
        '
        Me.lblCalCWSStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCalCWSStatus.AutoSize = True
        Me.lblCalCWSStatus.Location = New System.Drawing.Point(206, 117)
        Me.lblCalCWSStatus.Name = "lblCalCWSStatus"
        Me.lblCalCWSStatus.Size = New System.Drawing.Size(28, 13)
        Me.lblCalCWSStatus.TabIndex = 11
        Me.lblCalCWSStatus.Text = "N/A"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 117)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(201, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Central Warehouse Stock Status :"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(5, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(197, 21)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Item Code"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOR
        '
        Me.lblOR.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOR.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOR.Location = New System.Drawing.Point(206, 30)
        Me.lblOR.Name = "lblOR"
        Me.lblOR.Size = New System.Drawing.Size(363, 15)
        Me.lblOR.TabIndex = 6
        Me.lblOR.Text = "OR"
        Me.lblOR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblEAN
        '
        Me.lblEAN.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEAN.Location = New System.Drawing.Point(5, 49)
        Me.lblEAN.Name = "lblEAN"
        Me.lblEAN.Size = New System.Drawing.Size(197, 26)
        Me.lblEAN.TabIndex = 9
        Me.lblEAN.Text = "BarCode No."
        Me.lblEAN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnGetStock
        '
        Me.BtnGetStock.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnGetStock.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnGetStock.Location = New System.Drawing.Point(618, 49)
        Me.BtnGetStock.Name = "BtnGetStock"
        Me.BtnGetStock.SetArticleCode = Nothing
        Me.BtnGetStock.SetRowIndex = 0
        Me.BtnGetStock.Size = New System.Drawing.Size(103, 26)
        Me.BtnGetStock.TabIndex = 3
        Me.BtnGetStock.Text = "Network Check"
        Me.BtnGetStock.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnGetStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnGetStock.UseVisualStyleBackColor = True
        Me.BtnGetStock.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnSearchBirthListID
        '
        Me.btnSearchBirthListID.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnSearchBirthListID.Image = Global.Spectrum.My.Resources.Resources.search_2
        Me.btnSearchBirthListID.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnSearchBirthListID.Location = New System.Drawing.Point(573, 49)
        Me.btnSearchBirthListID.Name = "btnSearchBirthListID"
        Me.btnSearchBirthListID.SetArticleCode = Nothing
        Me.btnSearchBirthListID.SetRowIndex = 0
        Me.btnSearchBirthListID.Size = New System.Drawing.Size(41, 26)
        Me.btnSearchBirthListID.TabIndex = 2
        Me.btnSearchBirthListID.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSearchBirthListID.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnSearchBirthListID.UseVisualStyleBackColor = True
        Me.btnSearchBirthListID.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCalArticleDescri
        '
        Me.lblCalArticleDescri.AutoSize = True
        Me.lblCalArticleDescri.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalArticleDescri.Location = New System.Drawing.Point(206, 79)
        Me.lblCalArticleDescri.Name = "lblCalArticleDescri"
        Me.lblCalArticleDescri.Size = New System.Drawing.Size(0, 13)
        Me.lblCalArticleDescri.TabIndex = 8
        ''
        ''txtItemCode
        ''
        'Me.txtItemCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        'Me.txtItemCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Me.txtItemCode.Location = New System.Drawing.Point(206, 5)
        'Me.txtItemCode.Name = "txtItemCode"
        'Me.txtItemCode.Size = New System.Drawing.Size(363, 21)
        'Me.txtItemCode.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Item Description"
        '
        'txtEAN
        '
        Me.txtEAN.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEAN.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEAN.Location = New System.Drawing.Point(206, 49)
        Me.txtEAN.Name = "txtEAN"
        Me.txtEAN.Size = New System.Drawing.Size(363, 21)
        Me.txtEAN.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.btnOK)
        Me.Panel1.Location = New System.Drawing.Point(0, 466)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(861, 44)
        Me.Panel1.TabIndex = 2
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancel.Location = New System.Drawing.Point(463, 9)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.SetArticleCode = Nothing
        Me.btnCancel.SetRowIndex = 0
        Me.btnCancel.Size = New System.Drawing.Size(120, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.Visible = False
        Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnOK.Location = New System.Drawing.Point(286, 9)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.SetArticleCode = Nothing
        Me.btnOK.SetRowIndex = 0
        Me.btnOK.Size = New System.Drawing.Size(134, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "Ok"
        Me.btnOK.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOK.UseVisualStyleBackColor = True
        Me.btnOK.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'dgItemSearch
        '
        Me.dgItemSearch.AllowArrows = False
        Me.dgItemSearch.AllowColMove = False
        Me.dgItemSearch.AllowColSelect = False
        Me.dgItemSearch.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.dgItemSearch.AllowUpdate = False
        Me.dgItemSearch.AllowUpdateOnBlur = False
        Me.dgItemSearch.BackColor = System.Drawing.Color.White
        Me.dgItemSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgItemSearch.CaptionHeight = 17
        Me.dgItemSearch.ExtendRightColumn = True
        Me.dgItemSearch.FilterBar = True
        Me.dgItemSearch.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.Standard
        Me.dgItemSearch.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgItemSearch.GroupByAreaVisible = False
        Me.dgItemSearch.GroupByCaption = "Drag a column header here to group by that column"
        Me.dgItemSearch.Images.Add(CType(resources.GetObject("dgItemSearch.Images"), System.Drawing.Image))
        Me.dgItemSearch.Location = New System.Drawing.Point(3, 184)
        Me.dgItemSearch.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.dgItemSearch.Name = "dgItemSearch"
        Me.dgItemSearch.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.dgItemSearch.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.dgItemSearch.PreviewInfo.ZoomFactor = 75.0R
        Me.dgItemSearch.PrintInfo.PageSettings = CType(resources.GetObject("dgItemSearch.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.dgItemSearch.RowDivider.Color = System.Drawing.Color.DarkGray
        Me.dgItemSearch.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None
        Me.dgItemSearch.RowHeight = 15
        Me.dgItemSearch.Size = New System.Drawing.Size(858, 276)
        Me.dgItemSearch.TabIndex = 1
        Me.dgItemSearch.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue
        Me.dgItemSearch.PropBag = resources.GetString("dgItemSearch.PropBag")
        '
        'frmNStockCheck
        '
        Me.ClientSize = New System.Drawing.Size(861, 510)
        Me.Controls.Add(Me.dgItemSearch)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.C1Sizer1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNStockCheck"
        Me.Text = "Stock Check"
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer1.ResumeLayout(False)
        Me.C1Sizer1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgItemSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents C1Sizer1 As C1.Win.C1Sizer.C1Sizer
    'Friend WithEvents txtItemCode As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtEAN As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents dgItemSearch As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents lblEAN As System.Windows.Forms.Label
    Friend WithEvents lblCalArticleDescri As System.Windows.Forms.Label
    Friend WithEvents lblOR As System.Windows.Forms.Label
    Friend WithEvents btnSearchBirthListID As Spectrum.CtrlBtn
    Friend WithEvents btnCancel As Spectrum.CtrlBtn
    Friend WithEvents btnOK As Spectrum.CtrlBtn
    Friend WithEvents BtnGetStock As Spectrum.CtrlBtn
    Friend WithEvents lblNextAvailabelDate As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblCalCWSStatus As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblAvailableQty As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblPhysicalQty As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtItemCode As Spectrum.AndroidSearchTextBox

End Class
