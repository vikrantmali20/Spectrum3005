<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNBirthListSearch
    Inherits Spectrum.CtrlRbnBaseForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNBirthListSearch))
        Me.grdSearchBirthListId = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        Me.gbFooter = New System.Windows.Forms.GroupBox
        Me.cmdCancel = New Spectrum.CtrlBtn
        Me.btnOK = New Spectrum.CtrlBtn
        CType(Me.grdSearchBirthListId, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbFooter.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdSearchBirthListId
        '
        Me.grdSearchBirthListId.AllowUpdate = False
        Me.grdSearchBirthListId.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSearchBirthListId.CaptionHeight = 17
        Me.grdSearchBirthListId.CellTips = C1.Win.C1TrueDBGrid.CellTipEnum.Anchored
        Me.grdSearchBirthListId.ExtendRightColumn = True
        Me.grdSearchBirthListId.FilterBar = True
        Me.grdSearchBirthListId.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdSearchBirthListId.Images.Add(CType(resources.GetObject("grdSearchBirthListId.Images"), System.Drawing.Image))
        Me.grdSearchBirthListId.Location = New System.Drawing.Point(0, 3)
        Me.grdSearchBirthListId.Name = "grdSearchBirthListId"
        Me.grdSearchBirthListId.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdSearchBirthListId.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdSearchBirthListId.PreviewInfo.ZoomFactor = 75
        Me.grdSearchBirthListId.PrintInfo.PageSettings = CType(resources.GetObject("grdSearchBirthListId.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdSearchBirthListId.RowHeight = 15
        Me.grdSearchBirthListId.Size = New System.Drawing.Size(925, 495)
        Me.grdSearchBirthListId.TabIndex = 0
        Me.grdSearchBirthListId.Text = "C1TrueDBGrid1"
        Me.grdSearchBirthListId.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue
        Me.grdSearchBirthListId.PropBag = resources.GetString("grdSearchBirthListId.PropBag")
        '
        'gbFooter
        '
        Me.gbFooter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbFooter.BackColor = System.Drawing.Color.Transparent
        Me.gbFooter.Controls.Add(Me.cmdCancel)
        Me.gbFooter.Controls.Add(Me.btnOK)
        Me.gbFooter.Location = New System.Drawing.Point(-1, 504)
        Me.gbFooter.Name = "gbFooter"
        Me.gbFooter.Size = New System.Drawing.Size(926, 42)
        Me.gbFooter.TabIndex = 61
        Me.gbFooter.TabStop = False
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdCancel.Location = New System.Drawing.Point(860, 17)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(51, 21)
        Me.cmdCancel.TabIndex = 2
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Location = New System.Drawing.Point(784, 17)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(64, 21)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "Ok"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'frmNBirthListSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(926, 599)
        Me.Controls.Add(Me.gbFooter)
        Me.Controls.Add(Me.grdSearchBirthListId)
        Me.IsDocToParent = True
        Me.Name = "frmNBirthListSearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BirthList Search"
        Me.Controls.SetChildIndex(Me.grdSearchBirthListId, 0)
        Me.Controls.SetChildIndex(Me.gbFooter, 0)
        CType(Me.grdSearchBirthListId, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbFooter.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdSearchBirthListId As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Friend WithEvents gbFooter As System.Windows.Forms.GroupBox
    Friend WithEvents cmdCancel As Spectrum.CtrlBtn
    Friend WithEvents btnOK As Spectrum.CtrlBtn

End Class
