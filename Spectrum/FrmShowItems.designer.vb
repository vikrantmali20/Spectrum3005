<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmShowItems
    'Inherits Spectrum.CtrlRbnBaseForm
    Inherits Spectrum.CtrlPopupForm
    ' System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmShowItems))
        Me.GrdShowData = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.CtrlBtn1 = New Spectrum.CtrlBtn()
        CType(Me.GrdShowData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GrdShowData
        '
        Me.GrdShowData.AutoGenerateColumns = False
        Me.GrdShowData.CellButtonImage = Global.Spectrum.My.Resources.Resources.del_icon
        Me.GrdShowData.ColumnInfo = resources.GetString("GrdShowData.ColumnInfo")
        Me.GrdShowData.ExtendLastCol = True
        Me.GrdShowData.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrdShowData.Location = New System.Drawing.Point(29, 21)
        Me.GrdShowData.Name = "GrdShowData"
        Me.GrdShowData.NewRowWatermark = ""
        Me.GrdShowData.Rows.Count = 1
        Me.GrdShowData.Rows.DefaultSize = 19
        Me.GrdShowData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GrdShowData.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.GrdShowData.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.GrdShowData.Size = New System.Drawing.Size(665, 156)
        Me.GrdShowData.StyleInfo = resources.GetString("GrdShowData.StyleInfo")
        Me.GrdShowData.TabIndex = 116
        Me.GrdShowData.Tag = ""
        '
        'CtrlBtn1
        '
        Me.CtrlBtn1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.CtrlBtn1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CtrlBtn1.Location = New System.Drawing.Point(290, 187)
        Me.CtrlBtn1.MoveToNxtCtrl = Nothing
        Me.CtrlBtn1.Name = "CtrlBtn1"
        Me.CtrlBtn1.SetArticleCode = Nothing
        Me.CtrlBtn1.SetRowIndex = 0
        Me.CtrlBtn1.Size = New System.Drawing.Size(75, 23)
        Me.CtrlBtn1.TabIndex = 117
        Me.CtrlBtn1.Text = "Ok"
        Me.CtrlBtn1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CtrlBtn1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CtrlBtn1.UseVisualStyleBackColor = True
        Me.CtrlBtn1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'FrmShowItems
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(742, 222)
        Me.Controls.Add(Me.CtrlBtn1)
        Me.Controls.Add(Me.GrdShowData)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmShowItems"
        Me.Text = "Updated Item Details"
        CType(Me.GrdShowData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GrdShowData As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents CtrlBtn1 As Spectrum.CtrlBtn
End Class
