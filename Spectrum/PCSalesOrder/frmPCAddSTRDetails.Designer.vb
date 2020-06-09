<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPCAddSTRDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPCAddSTRDetails))
        Me.C1Sizer4 = New C1.Win.C1Sizer.C1Sizer()
        Me.btnAddRemark = New Spectrum.CtrlBtn()
        Me.lblArticleNameVal = New Spectrum.CtrlLabel()
        Me.dgUpperGrid = New Spectrum.CtrlGrid()
        Me.dgLowerGrid = New Spectrum.CtrlGrid()
        Me.lblArticleTypeVal = New Spectrum.CtrlLabel()
        Me.lblArticleType = New Spectrum.CtrlLabel()
        Me.lblArticleName = New Spectrum.CtrlLabel()
        Me.btnCancel = New Spectrum.CtrlBtn()
        Me.btnSave = New Spectrum.CtrlBtn()
        CType(Me.C1Sizer4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer4.SuspendLayout()
        CType(Me.lblArticleNameVal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgUpperGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgLowerGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblArticleTypeVal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblArticleType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblArticleName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'C1Sizer4
        '
        Me.C1Sizer4.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1Sizer4.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1Sizer4.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1Sizer4.Controls.Add(Me.btnAddRemark)
        Me.C1Sizer4.Controls.Add(Me.lblArticleNameVal)
        Me.C1Sizer4.Controls.Add(Me.dgUpperGrid)
        Me.C1Sizer4.Controls.Add(Me.dgLowerGrid)
        Me.C1Sizer4.Controls.Add(Me.lblArticleTypeVal)
        Me.C1Sizer4.Controls.Add(Me.lblArticleType)
        Me.C1Sizer4.Controls.Add(Me.lblArticleName)
        Me.C1Sizer4.Controls.Add(Me.btnCancel)
        Me.C1Sizer4.Controls.Add(Me.btnSave)
        Me.C1Sizer4.Dock = System.Windows.Forms.DockStyle.Top
        Me.C1Sizer4.GridDefinition = resources.GetString("C1Sizer4.GridDefinition")
        Me.C1Sizer4.Location = New System.Drawing.Point(0, 0)
        Me.C1Sizer4.Name = "C1Sizer4"
        Me.C1Sizer4.Size = New System.Drawing.Size(1087, 624)
        Me.C1Sizer4.TabIndex = 70
        Me.C1Sizer4.TabStop = False
        Me.C1Sizer4.Text = "C1Sizer1"
        '
        'btnAddRemark
        '
        Me.btnAddRemark.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddRemark.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnAddRemark.Location = New System.Drawing.Point(703, 563)
        Me.btnAddRemark.Name = "btnAddRemark"
        Me.btnAddRemark.SetArticleCode = Nothing
        Me.btnAddRemark.SetRowIndex = 0
        Me.btnAddRemark.Size = New System.Drawing.Size(138, 46)
        Me.btnAddRemark.TabIndex = 117
        Me.btnAddRemark.Tag = ""
        Me.btnAddRemark.Text = "&Add Remark"
        Me.btnAddRemark.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAddRemark.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAddRemark.UseVisualStyleBackColor = True
        Me.btnAddRemark.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblArticleNameVal
        '
        Me.lblArticleNameVal.AttachedTextBoxName = Nothing
        Me.lblArticleNameVal.BackColor = System.Drawing.Color.Transparent
        Me.lblArticleNameVal.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblArticleNameVal.Font = New System.Drawing.Font("Calibri", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArticleNameVal.ForeColor = System.Drawing.Color.Black
        Me.lblArticleNameVal.Location = New System.Drawing.Point(217, 19)
        Me.lblArticleNameVal.Name = "lblArticleNameVal"
        Me.lblArticleNameVal.Size = New System.Drawing.Size(353, 26)
        Me.lblArticleNameVal.TabIndex = 113
        Me.lblArticleNameVal.Tag = Nothing
        Me.lblArticleNameVal.Text = "Name"
        Me.lblArticleNameVal.TextDetached = True
        Me.lblArticleNameVal.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'dgUpperGrid
        '
        Me.dgUpperGrid.CellButtonImage = CType(resources.GetObject("dgUpperGrid.CellButtonImage"), System.Drawing.Image)
        Me.dgUpperGrid.ColumnInfo = resources.GetString("dgUpperGrid.ColumnInfo")
        Me.dgUpperGrid.ExtendLastCol = True
        Me.dgUpperGrid.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgUpperGrid.Location = New System.Drawing.Point(9, 49)
        Me.dgUpperGrid.Name = "dgUpperGrid"
        Me.dgUpperGrid.Rows.DefaultSize = 25
        Me.dgUpperGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox
        Me.dgUpperGrid.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.dgUpperGrid.Size = New System.Drawing.Size(418, 219)
        Me.dgUpperGrid.StyleInfo = resources.GetString("dgUpperGrid.StyleInfo")
        Me.dgUpperGrid.TabIndex = 116
        Me.dgUpperGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'dgLowerGrid
        '
        Me.dgLowerGrid.CellButtonImage = CType(resources.GetObject("dgLowerGrid.CellButtonImage"), System.Drawing.Image)
        Me.dgLowerGrid.ColumnInfo = resources.GetString("dgLowerGrid.ColumnInfo")
        Me.dgLowerGrid.ExtendLastCol = True
        Me.dgLowerGrid.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgLowerGrid.Location = New System.Drawing.Point(5, 290)
        Me.dgLowerGrid.Name = "dgLowerGrid"
        Me.dgLowerGrid.Rows.Count = 13
        Me.dgLowerGrid.Rows.DefaultSize = 25
        Me.dgLowerGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox
        Me.dgLowerGrid.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.dgLowerGrid.Size = New System.Drawing.Size(1072, 255)
        Me.dgLowerGrid.StyleInfo = resources.GetString("dgLowerGrid.StyleInfo")
        Me.dgLowerGrid.TabIndex = 115
        Me.dgLowerGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'lblArticleTypeVal
        '
        Me.lblArticleTypeVal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblArticleTypeVal.AttachedTextBoxName = Nothing
        Me.lblArticleTypeVal.AutoSize = True
        Me.lblArticleTypeVal.BackColor = System.Drawing.Color.Transparent
        Me.lblArticleTypeVal.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblArticleTypeVal.Font = New System.Drawing.Font("Calibri", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArticleTypeVal.ForeColor = System.Drawing.Color.Black
        Me.lblArticleTypeVal.Location = New System.Drawing.Point(703, 19)
        Me.lblArticleTypeVal.Name = "lblArticleTypeVal"
        Me.lblArticleTypeVal.Size = New System.Drawing.Size(42, 21)
        Me.lblArticleTypeVal.TabIndex = 114
        Me.lblArticleTypeVal.Tag = Nothing
        Me.lblArticleTypeVal.Text = "Type"
        Me.lblArticleTypeVal.TextDetached = True
        Me.lblArticleTypeVal.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblArticleType
        '
        Me.lblArticleType.AttachedTextBoxName = Nothing
        Me.lblArticleType.AutoSize = True
        Me.lblArticleType.BackColor = System.Drawing.Color.Transparent
        Me.lblArticleType.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblArticleType.Font = New System.Drawing.Font("Calibri", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArticleType.ForeColor = System.Drawing.Color.Black
        Me.lblArticleType.Location = New System.Drawing.Point(574, 19)
        Me.lblArticleType.Name = "lblArticleType"
        Me.lblArticleType.Size = New System.Drawing.Size(99, 21)
        Me.lblArticleType.TabIndex = 112
        Me.lblArticleType.Tag = Nothing
        Me.lblArticleType.Text = "Article Type :"
        Me.lblArticleType.TextDetached = True
        Me.lblArticleType.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblArticleName
        '
        Me.lblArticleName.AttachedTextBoxName = Nothing
        Me.lblArticleName.AutoSize = True
        Me.lblArticleName.BackColor = System.Drawing.Color.Transparent
        Me.lblArticleName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblArticleName.Font = New System.Drawing.Font("Calibri", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArticleName.ForeColor = System.Drawing.Color.Black
        Me.lblArticleName.Location = New System.Drawing.Point(95, 19)
        Me.lblArticleName.Name = "lblArticleName"
        Me.lblArticleName.Size = New System.Drawing.Size(108, 21)
        Me.lblArticleName.TabIndex = 111
        Me.lblArticleName.Tag = Nothing
        Me.lblArticleName.Text = "Article Name :"
        Me.lblArticleName.TextDetached = True
        Me.lblArticleName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancel.Location = New System.Drawing.Point(963, 563)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.SetArticleCode = Nothing
        Me.btnCancel.SetRowIndex = 0
        Me.btnCancel.Size = New System.Drawing.Size(114, 46)
        Me.btnCancel.TabIndex = 110
        Me.btnCancel.Tag = ""
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSave.Location = New System.Drawing.Point(845, 563)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.SetArticleCode = Nothing
        Me.btnSave.SetRowIndex = 0
        Me.btnSave.Size = New System.Drawing.Size(114, 46)
        Me.btnSave.TabIndex = 109
        Me.btnSave.Tag = ""
        Me.btnSave.Text = "&Save"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSave.UseVisualStyleBackColor = True
        Me.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmPCAddSTRDetails
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1087, 618)
        Me.Controls.Add(Me.C1Sizer4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPCAddSTRDetails"
        Me.Text = "Add STR Details"
        CType(Me.C1Sizer4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer4.ResumeLayout(False)
        Me.C1Sizer4.PerformLayout()
        CType(Me.lblArticleNameVal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgUpperGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgLowerGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblArticleTypeVal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblArticleType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblArticleName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents C1Sizer4 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents btnSave As Spectrum.CtrlBtn
    Friend WithEvents btnCancel As Spectrum.CtrlBtn
    Friend WithEvents lblArticleTypeVal As Spectrum.CtrlLabel
    Friend WithEvents lblArticleNameVal As Spectrum.CtrlLabel
    Friend WithEvents lblArticleType As Spectrum.CtrlLabel
    Friend WithEvents lblArticleName As Spectrum.CtrlLabel
    Friend WithEvents dgLowerGrid As Spectrum.CtrlGrid
    Friend WithEvents dgUpperGrid As Spectrum.CtrlGrid
    Friend WithEvents btnAddRemark As Spectrum.CtrlBtn
End Class
