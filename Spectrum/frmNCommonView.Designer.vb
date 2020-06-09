<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNCommonView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNCommonView))
        Me.lblCount = New Spectrum.CtrlLabel()
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        Me.cmdCancel = New Spectrum.CtrlBtn()
        Me.cmdOk = New Spectrum.CtrlBtn()
        Me.dgSearch = New Spectrum.CtrlGrid()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        CType(Me.lblCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblCount
        '
        Me.lblCount.AttachedTextBoxName = Nothing
        Me.lblCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCount.ForeColor = System.Drawing.Color.Black
        Me.lblCount.Location = New System.Drawing.Point(74, 22)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(74, 19)
        Me.lblCount.TabIndex = 42
        Me.lblCount.Tag = Nothing
        Me.lblCount.TextDetached = True
        Me.lblCount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel1
        '
        Me.CtrlLabel1.AttachedTextBoxName = Nothing
        Me.CtrlLabel1.AutoSize = True
        Me.CtrlLabel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel1.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlLabel1.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel1.Location = New System.Drawing.Point(6, 23)
        Me.CtrlLabel1.Name = "CtrlLabel1"
        Me.CtrlLabel1.Size = New System.Drawing.Size(62, 14)
        Me.CtrlLabel1.TabIndex = 41
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.Text = "Total Row:"
        Me.CtrlLabel1.TextDetached = True
        Me.CtrlLabel1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdCancel.Location = New System.Drawing.Point(719, 13)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 32)
        Me.cmdCancel.TabIndex = 40
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdCancel.UseVisualStyleBackColor = True
        Me.cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOk.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdOk.Location = New System.Drawing.Point(633, 13)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(75, 32)
        Me.cmdOk.TabIndex = 39
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdOk.UseVisualStyleBackColor = True
        Me.cmdOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'dgSearch
        '
        Me.dgSearch.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.dgSearch.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.dgSearch.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn
        Me.dgSearch.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.dgSearch.CellButtonImage = CType(resources.GetObject("dgSearch.CellButtonImage"), System.Drawing.Image)
        Me.dgSearch.ColumnInfo = "3,0,0,0,0,100,Columns:"
        Me.dgSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgSearch.ExtendLastCol = True
        Me.dgSearch.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.dgSearch.Location = New System.Drawing.Point(0, 0)
        Me.dgSearch.Name = "dgSearch"
        Me.dgSearch.Rows.Count = 2
        Me.dgSearch.Rows.DefaultSize = 20
        Me.dgSearch.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox
        Me.dgSearch.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.dgSearch.Size = New System.Drawing.Size(800, 421)
        Me.dgSearch.StyleInfo = resources.GetString("dgSearch.StyleInfo")
        Me.dgSearch.TabIndex = 43
        Me.dgSearch.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Black
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CtrlLabel1)
        Me.GroupBox1.Controls.Add(Me.lblCount)
        Me.GroupBox1.Controls.Add(Me.cmdCancel)
        Me.GroupBox1.Controls.Add(Me.cmdOk)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 422)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(800, 50)
        Me.GroupBox1.TabIndex = 44
        Me.GroupBox1.TabStop = False
        '
        'frmNCommonView
        '
        Me.AcceptButton = Me.cmdOk
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(800, 472)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgSearch)
        Me.MaximumSize = New System.Drawing.Size(808, 506)
        Me.MinimumSize = New System.Drawing.Size(808, 506)
        Me.Name = "frmNCommonView"
        Me.Text = "Lookup"
        CType(Me.lblCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblCount As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel1 As Spectrum.CtrlLabel
    Friend WithEvents cmdCancel As Spectrum.CtrlBtn
    Friend WithEvents cmdOk As Spectrum.CtrlBtn
    Friend WithEvents dgSearch As Spectrum.CtrlGrid
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox

End Class
