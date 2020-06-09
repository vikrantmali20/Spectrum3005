<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateCheckDueDate
    'Inherits System.Windows.Forms.Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdateCheckDueDate))
        Me.sizHeader = New C1.Win.C1Sizer.C1Sizer()
        Me.BtnSave = New Spectrum.CtrlBtn()
        Me.grdCheckDetails = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.btnClose = New Spectrum.CtrlBtn()
        Me.C1Sizer1 = New C1.Win.C1Sizer.C1Sizer()
        Me.btnSubmit = New Spectrum.CtrlBtn()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.dtTo = New Spectrum.ctrlDate()
        Me.dtFrom = New Spectrum.ctrlDate()
        CType(Me.sizHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sizHeader.SuspendLayout()
        CType(Me.grdCheckDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer1.SuspendLayout()
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sizHeader
        '
        Me.sizHeader.Border.Color = System.Drawing.Color.LightSlateGray
        Me.sizHeader.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.sizHeader.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.sizHeader.Controls.Add(Me.BtnSave)
        Me.sizHeader.Controls.Add(Me.grdCheckDetails)
        Me.sizHeader.Controls.Add(Me.btnClose)
        Me.sizHeader.Controls.Add(Me.C1Sizer1)
        Me.sizHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sizHeader.GridDefinition = resources.GetString("sizHeader.GridDefinition")
        Me.sizHeader.Location = New System.Drawing.Point(0, 0)
        Me.sizHeader.Name = "sizHeader"
        Me.sizHeader.Size = New System.Drawing.Size(1068, 498)
        Me.sizHeader.TabIndex = 0
        Me.sizHeader.TabStop = False
        Me.sizHeader.Text = "C1Sizer2"
        '
        'BtnSave
        '
        Me.BtnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnSave.Location = New System.Drawing.Point(794, 463)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.SetArticleCode = Nothing
        Me.BtnSave.SetRowIndex = 0
        Me.BtnSave.Size = New System.Drawing.Size(141, 30)
        Me.BtnSave.TabIndex = 2
        Me.BtnSave.Text = "Save"
        Me.BtnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnSave.UseVisualStyleBackColor = True
        Me.BtnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'grdCheckDetails
        '
        Me.grdCheckDetails.AllowColMove = False
        Me.grdCheckDetails.AllowColSelect = False
        Me.grdCheckDetails.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.grdCheckDetails.AllowUpdate = False
        Me.grdCheckDetails.AllowUpdateOnBlur = False
        Me.grdCheckDetails.BackColor = System.Drawing.Color.White
        Me.grdCheckDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.grdCheckDetails.CaptionHeight = 17
        Me.grdCheckDetails.ExtendRightColumn = True
        Me.grdCheckDetails.FilterBar = True
        Me.grdCheckDetails.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.Standard
        Me.grdCheckDetails.GroupByAreaVisible = False
        Me.grdCheckDetails.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdCheckDetails.Images.Add(CType(resources.GetObject("grdCheckDetails.Images"), System.Drawing.Image))
        Me.grdCheckDetails.Images.Add(CType(resources.GetObject("grdCheckDetails.Images1"), System.Drawing.Image))
        Me.grdCheckDetails.Location = New System.Drawing.Point(5, 44)
        Me.grdCheckDetails.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow
        Me.grdCheckDetails.Name = "grdCheckDetails"
        Me.grdCheckDetails.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdCheckDetails.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdCheckDetails.PreviewInfo.ZoomFactor = 75.0R
        Me.grdCheckDetails.PrintInfo.PageSettings = CType(resources.GetObject("grdCheckDetails.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdCheckDetails.RecordSelectorWidth = 25
        Me.grdCheckDetails.RowDivider.Color = System.Drawing.Color.DimGray
        Me.grdCheckDetails.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None
        Me.grdCheckDetails.RowHeight = 25
        Me.grdCheckDetails.Size = New System.Drawing.Size(1058, 415)
        Me.grdCheckDetails.TabIndex = 1
        Me.grdCheckDetails.UseColumnStyles = False
        Me.grdCheckDetails.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue
        Me.grdCheckDetails.PropBag = resources.GetString("grdCheckDetails.PropBag")
        '
        'btnClose
        '
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnClose.Location = New System.Drawing.Point(939, 463)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.SetArticleCode = Nothing
        Me.btnClose.SetRowIndex = 0
        Me.btnClose.Size = New System.Drawing.Size(124, 30)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnClose.UseVisualStyleBackColor = True
        Me.btnClose.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Sizer1
        '
        Me.C1Sizer1.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1Sizer1.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1Sizer1.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1Sizer1.Controls.Add(Me.btnSubmit)
        Me.C1Sizer1.Controls.Add(Me.lblTo)
        Me.C1Sizer1.Controls.Add(Me.lblFrom)
        Me.C1Sizer1.Controls.Add(Me.dtTo)
        Me.C1Sizer1.Controls.Add(Me.dtFrom)
        Me.C1Sizer1.GridDefinition = resources.GetString("C1Sizer1.GridDefinition")
        Me.C1Sizer1.Location = New System.Drawing.Point(5, 5)
        Me.C1Sizer1.Name = "C1Sizer1"
        Me.C1Sizer1.Size = New System.Drawing.Size(1058, 35)
        Me.C1Sizer1.TabIndex = 0
        Me.C1Sizer1.TabStop = False
        Me.C1Sizer1.Text = "C1Sizer2"
        '
        'btnSubmit
        '
        Me.btnSubmit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSubmit.Location = New System.Drawing.Point(501, 5)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.SetArticleCode = Nothing
        Me.btnSubmit.SetRowIndex = 0
        Me.btnSubmit.Size = New System.Drawing.Size(83, 25)
        Me.btnSubmit.TabIndex = 2
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSubmit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnSubmit.UseVisualStyleBackColor = True
        Me.btnSubmit.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblTo
        '
        Me.lblTo.Location = New System.Drawing.Point(253, 5)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(100, 25)
        Me.lblTo.TabIndex = 4
        Me.lblTo.Text = "To"
        Me.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFrom
        '
        Me.lblFrom.Location = New System.Drawing.Point(5, 5)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(100, 25)
        Me.lblFrom.TabIndex = 3
        Me.lblFrom.Text = "From"
        Me.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtTo
        '
        Me.dtTo.AutoSize = False
        Me.dtTo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.dtTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtTo.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage"), System.Drawing.Image)
        '
        '
        '
        Me.dtTo.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtTo.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtTo.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtTo.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtTo.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtTo.Location = New System.Drawing.Point(357, 5)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(140, 25)
        Me.dtTo.TabIndex = 1
        Me.dtTo.Tag = Nothing
        Me.dtTo.TrimStart = True
        Me.dtTo.Value = New Date(2010, 12, 3, 17, 20, 35, 0)
        Me.dtTo.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtTo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'dtFrom
        '
        Me.dtFrom.AutoSize = False
        Me.dtFrom.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.dtFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtFrom.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage1"), System.Drawing.Image)
        '
        '
        '
        Me.dtFrom.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtFrom.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtFrom.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtFrom.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtFrom.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtFrom.Location = New System.Drawing.Point(109, 5)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(140, 25)
        Me.dtFrom.TabIndex = 0
        Me.dtFrom.Tag = Nothing
        Me.dtFrom.TrimStart = True
        Me.dtFrom.Value = New Date(2010, 12, 3, 17, 20, 17, 0)
        Me.dtFrom.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtFrom.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'frmUpdateCheckDueDate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1068, 498)
        Me.Controls.Add(Me.sizHeader)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUpdateCheckDueDate"
        Me.Text = "Check Due Date"
        CType(Me.sizHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sizHeader.ResumeLayout(False)
        CType(Me.grdCheckDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer1.ResumeLayout(False)
        CType(Me.dtTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFrom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sizHeader As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents C1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents dtTo As Spectrum.ctrlDate
    Friend WithEvents dtFrom As Spectrum.ctrlDate
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents btnSubmit As Spectrum.CtrlBtn
    Friend WithEvents btnClose As Spectrum.CtrlBtn
    Friend WithEvents grdCheckDetails As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents BtnSave As Spectrum.CtrlBtn
End Class
