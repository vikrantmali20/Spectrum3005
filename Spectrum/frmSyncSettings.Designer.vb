<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSyncSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSyncSettings))
        Me.dgSync = New Spectrum.CtrlGrid
        Me.cmdSave = New Spectrum.CtrlBtn
        Me.cmdAdd = New Spectrum.CtrlBtn
        CType(Me.dgSync, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgSync
        '
        Me.dgSync.CellButtonImage = CType(resources.GetObject("dgSync.CellButtonImage"), System.Drawing.Image)
        Me.dgSync.ColumnInfo = resources.GetString("dgSync.ColumnInfo")
        Me.dgSync.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgSync.ExtendLastCol = True
        Me.dgSync.Location = New System.Drawing.Point(0, 0)
        Me.dgSync.Name = "dgSync"
        Me.dgSync.Rows.Count = 2
        Me.dgSync.Rows.DefaultSize = 18
        Me.dgSync.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.dgSync.Size = New System.Drawing.Size(604, 329)
        Me.dgSync.StyleInfo = resources.GetString("dgSync.StyleInfo")
        Me.dgSync.TabIndex = 0
        Me.dgSync.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'cmdSave
        '
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSave.Location = New System.Drawing.Point(320, 335)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(142, 31)
        Me.cmdSave.TabIndex = 1
        Me.cmdSave.Text = "Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdSave.UseVisualStyleBackColor = True
        Me.cmdSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdAdd
        '
        Me.cmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdAdd.Location = New System.Drawing.Point(161, 335)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(142, 31)
        Me.cmdAdd.TabIndex = 2
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'frmSyncSettings
        '
        Me.ClientSize = New System.Drawing.Size(604, 373)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.dgSync)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSyncSettings"
        Me.Text = "Terminal Sync Setting"
        CType(Me.dgSync, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgSync As Spectrum.CtrlGrid
    Friend WithEvents cmdSave As Spectrum.CtrlBtn
    Friend WithEvents cmdAdd As Spectrum.CtrlBtn

End Class
