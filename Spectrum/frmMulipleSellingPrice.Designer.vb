<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMulipleSellingPrice
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMulipleSellingPrice))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.articleGrid = New Spectrum.CtrlGrid()
        Me.cancelBtn = New Spectrum.CtrlBtn()
        Me.okBtn = New Spectrum.CtrlBtn()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.articleGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.articleGrid, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.55285!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.44715!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(485, 246)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel1.AutoSize = True
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.Controls.Add(Me.cancelBtn)
        Me.Panel1.Controls.Add(Me.okBtn)
        Me.Panel1.Location = New System.Drawing.Point(161, 211)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(162, 32)
        Me.Panel1.TabIndex = 1
        '
        'articleGrid
        '
        Me.articleGrid.CellButtonImage = CType(resources.GetObject("articleGrid.CellButtonImage"), System.Drawing.Image)
        Me.articleGrid.ColumnInfo = "10,1,0,0,0,105,Columns:"
        Me.articleGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.articleGrid.ExtendLastCol = True
        Me.articleGrid.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid
        Me.articleGrid.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.articleGrid.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.articleGrid.Location = New System.Drawing.Point(3, 3)
        Me.articleGrid.Name = "articleGrid"
        Me.articleGrid.Rows.DefaultSize = 21
        Me.articleGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.articleGrid.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.articleGrid.Size = New System.Drawing.Size(479, 202)
        Me.articleGrid.StyleInfo = resources.GetString("articleGrid.StyleInfo")
        Me.articleGrid.TabIndex = 0
        Me.articleGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'cancelBtn
        '
        Me.cancelBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cancelBtn.Location = New System.Drawing.Point(84, 6)
        Me.cancelBtn.Name = "cancelBtn"
        Me.cancelBtn.SetArticleCode = Nothing
        Me.cancelBtn.Size = New System.Drawing.Size(75, 23)
        Me.cancelBtn.TabIndex = 1
        Me.cancelBtn.Text = "Cancel"
        Me.cancelBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cancelBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cancelBtn.UseVisualStyleBackColor = True
        Me.cancelBtn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'okBtn
        '
        Me.okBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.okBtn.Location = New System.Drawing.Point(3, 6)
        Me.okBtn.Name = "okBtn"
        Me.okBtn.SetArticleCode = Nothing
        Me.okBtn.Size = New System.Drawing.Size(75, 23)
        Me.okBtn.TabIndex = 0
        Me.okBtn.Text = "OK"
        Me.okBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.okBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.okBtn.UseVisualStyleBackColor = True
        Me.okBtn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmMulipleSellingPrice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(485, 246)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "frmMulipleSellingPrice"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Multiple Selling Price"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.articleGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents articleGrid As Spectrum.CtrlGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cancelBtn As Spectrum.CtrlBtn
    Friend WithEvents okBtn As Spectrum.CtrlBtn
End Class
