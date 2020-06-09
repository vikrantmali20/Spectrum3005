<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPCApplyTax
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPCApplyTax))
        Me.c1Sizer1 = New C1.Win.C1Sizer.C1Sizer()
        Me.dgMainGrid = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.cmdCancel = New Spectrum.CtrlBtn()
        Me.cmdSave = New Spectrum.CtrlBtn()
        Me.cmdAdd = New Spectrum.CtrlBtn()
        Me.cboTax = New Spectrum.ctrlCombo()
        Me.lblSelectTax = New Spectrum.CtrlLabel()
        Me.lblGrossValue = New Spectrum.CtrlLabel()
        Me.lblGrossAmt = New Spectrum.CtrlLabel()
        CType(Me.c1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.c1Sizer1.SuspendLayout()
        CType(Me.dgMainGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSelectTax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGrossValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGrossAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'c1Sizer1
        '
        Me.c1Sizer1.Controls.Add(Me.dgMainGrid)
        Me.c1Sizer1.Controls.Add(Me.cmdCancel)
        Me.c1Sizer1.Controls.Add(Me.cmdSave)
        Me.c1Sizer1.Controls.Add(Me.cmdAdd)
        Me.c1Sizer1.Controls.Add(Me.cboTax)
        Me.c1Sizer1.Controls.Add(Me.lblSelectTax)
        Me.c1Sizer1.Controls.Add(Me.lblGrossValue)
        Me.c1Sizer1.Controls.Add(Me.lblGrossAmt)
        Me.c1Sizer1.GridDefinition = "97.9848866498741:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "98.7860394537177:False:False;"
        Me.c1Sizer1.Location = New System.Drawing.Point(1, 5)
        Me.c1Sizer1.Name = "c1Sizer1"
        Me.c1Sizer1.Size = New System.Drawing.Size(659, 397)
        Me.c1Sizer1.TabIndex = 103
        Me.c1Sizer1.Text = "c1Sizer1"
        '
        'dgMainGrid
        '
        Me.dgMainGrid.AutoGenerateColumns = False
        Me.dgMainGrid.CellButtonImage = Global.Spectrum.My.Resources.Resources.del_icon
        Me.dgMainGrid.ColumnInfo = resources.GetString("dgMainGrid.ColumnInfo")
        Me.dgMainGrid.ExtendLastCol = True
        Me.dgMainGrid.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgMainGrid.Location = New System.Drawing.Point(20, 122)
        Me.dgMainGrid.Name = "dgMainGrid"
        Me.dgMainGrid.NewRowWatermark = ""
        Me.dgMainGrid.Rows.Count = 1
        Me.dgMainGrid.Rows.DefaultSize = 20
        Me.dgMainGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgMainGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.dgMainGrid.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.dgMainGrid.Size = New System.Drawing.Size(626, 198)
        Me.dgMainGrid.StyleInfo = resources.GetString("dgMainGrid.StyleInfo")
        Me.dgMainGrid.TabIndex = 112
        Me.dgMainGrid.Tag = ""
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdCancel.Location = New System.Drawing.Point(537, 344)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.SetArticleCode = Nothing
        Me.cmdCancel.SetRowIndex = 0
        Me.cmdCancel.Size = New System.Drawing.Size(94, 28)
        Me.cmdCancel.TabIndex = 40
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdCancel.UseVisualStyleBackColor = True
        Me.cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSave.Location = New System.Drawing.Point(416, 344)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.SetArticleCode = Nothing
        Me.cmdSave.SetRowIndex = 0
        Me.cmdSave.Size = New System.Drawing.Size(94, 28)
        Me.cmdSave.TabIndex = 39
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdSave.UseVisualStyleBackColor = True
        Me.cmdSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdAdd
        '
        Me.cmdAdd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdAdd.Location = New System.Drawing.Point(502, 72)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.SetArticleCode = Nothing
        Me.cmdAdd.SetRowIndex = 0
        Me.cmdAdd.Size = New System.Drawing.Size(94, 22)
        Me.cmdAdd.TabIndex = 37
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdAdd.UseVisualStyleBackColor = True
        Me.cmdAdd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cboTax
        '
        Me.cboTax.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboTax.AutoCompletion = True
        Me.cboTax.AutoDropDown = True
        Me.cboTax.Caption = ""
        Me.cboTax.CaptionHeight = 17
        Me.cboTax.CaptionVisible = False
        Me.cboTax.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboTax.ColumnCaptionHeight = 17
        Me.cboTax.ColumnFooterHeight = 17
        Me.cboTax.ColumnHeaders = False
        Me.cboTax.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList
        Me.cboTax.ContentHeight = 15
        Me.cboTax.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboTax.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboTax.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTax.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTax.EditorHeight = 15
        Me.cboTax.Images.Add(CType(resources.GetObject("cboTax.Images"), System.Drawing.Image))
        Me.cboTax.ItemHeight = 15
        Me.cboTax.Location = New System.Drawing.Point(152, 72)
        Me.cboTax.MatchEntryTimeout = CType(2000, Long)
        Me.cboTax.MaxDropDownItems = CType(5, Short)
        Me.cboTax.MaxLength = 32767
        Me.cboTax.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboTax.Name = "cboTax"
        Me.cboTax.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboTax.Size = New System.Drawing.Size(313, 21)
        Me.cboTax.TabIndex = 35
        Me.cboTax.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cboTax.PropBag = resources.GetString("cboTax.PropBag")
        '
        'lblSelectTax
        '
        Me.lblSelectTax.AttachedTextBoxName = Nothing
        Me.lblSelectTax.AutoSize = True
        Me.lblSelectTax.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblSelectTax.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblSelectTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSelectTax.ForeColor = System.Drawing.Color.Black
        Me.lblSelectTax.Location = New System.Drawing.Point(38, 72)
        Me.lblSelectTax.Name = "lblSelectTax"
        Me.lblSelectTax.Size = New System.Drawing.Size(77, 15)
        Me.lblSelectTax.TabIndex = 9
        Me.lblSelectTax.Tag = Nothing
        Me.lblSelectTax.Text = "Select Tax :"
        Me.lblSelectTax.TextDetached = True
        Me.lblSelectTax.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblGrossValue
        '
        Me.lblGrossValue.AttachedTextBoxName = Nothing
        Me.lblGrossValue.AutoSize = True
        Me.lblGrossValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGrossValue.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGrossValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGrossValue.ForeColor = System.Drawing.Color.Black
        Me.lblGrossValue.Location = New System.Drawing.Point(175, 24)
        Me.lblGrossValue.Name = "lblGrossValue"
        Me.lblGrossValue.Size = New System.Drawing.Size(40, 15)
        Me.lblGrossValue.TabIndex = 8
        Me.lblGrossValue.Tag = Nothing
        Me.lblGrossValue.Text = "value"
        Me.lblGrossValue.TextDetached = True
        Me.lblGrossValue.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblGrossAmt
        '
        Me.lblGrossAmt.AttachedTextBoxName = Nothing
        Me.lblGrossAmt.AutoSize = True
        Me.lblGrossAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGrossAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblGrossAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGrossAmt.ForeColor = System.Drawing.Color.Black
        Me.lblGrossAmt.Location = New System.Drawing.Point(38, 24)
        Me.lblGrossAmt.Name = "lblGrossAmt"
        Me.lblGrossAmt.Size = New System.Drawing.Size(102, 15)
        Me.lblGrossAmt.TabIndex = 7
        Me.lblGrossAmt.Tag = Nothing
        Me.lblGrossAmt.Text = "Gross Amt(Rs) :"
        Me.lblGrossAmt.TextDetached = True
        Me.lblGrossAmt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmPCApplyTax
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(663, 404)
        Me.Controls.Add(Me.c1Sizer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPCApplyTax"
        Me.Text = "Apply Tax"
        CType(Me.c1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.c1Sizer1.ResumeLayout(False)
        Me.c1Sizer1.PerformLayout()
        CType(Me.dgMainGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSelectTax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGrossValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGrossAmt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents c1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents dgMainGrid As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents cmdCancel As Spectrum.CtrlBtn
    Friend WithEvents cmdSave As Spectrum.CtrlBtn
    Friend WithEvents cmdAdd As Spectrum.CtrlBtn
    Friend WithEvents cboTax As Spectrum.ctrlCombo
    Friend WithEvents lblSelectTax As Spectrum.CtrlLabel
    Friend WithEvents lblGrossValue As Spectrum.CtrlLabel
    Friend WithEvents lblGrossAmt As Spectrum.CtrlLabel
End Class
