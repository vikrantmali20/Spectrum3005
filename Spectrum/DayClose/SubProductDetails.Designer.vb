<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SubProductDetails
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgSubProductDetails = New CtrlDataGridView()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.CtrlPagination1 = New Spectrum.CtrlPagination()
        Me.ArticleName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RecipeCode = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.BatchCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EnteredQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ArticleBaseUOM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CalculatedQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgSubProductDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgSubProductDetails
        '
        Me.dgSubProductDetails.AllowUserToAddRows = False
        Me.dgSubProductDetails.AllowUserToDeleteRows = False
        Me.dgSubProductDetails.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgSubProductDetails.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgSubProductDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSubProductDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ArticleName, Me.RecipeCode, Me.BatchCount, Me.EnteredQty, Me.ArticleBaseUOM, Me.CalculatedQty})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgSubProductDetails.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgSubProductDetails.Location = New System.Drawing.Point(3, 3)
        Me.dgSubProductDetails.Name = "dgSubProductDetails"
        Me.dgSubProductDetails.RowHeadersWidth = 20
        Me.dgSubProductDetails.Size = New System.Drawing.Size(958, 560)
        Me.dgSubProductDetails.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.dgSubProductDetails, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlPagination1, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(969, 616)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'CtrlPagination1
        '
        Me.CtrlPagination1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CtrlPagination1.AutoSize = True
        Me.CtrlPagination1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CtrlPagination1.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlPagination1.LastActivePageNo = 1
        Me.CtrlPagination1.Location = New System.Drawing.Point(3, 570)
        Me.CtrlPagination1.Name = "CtrlPagination1"
        Me.CtrlPagination1.Size = New System.Drawing.Size(963, 41)
        Me.CtrlPagination1.TabIndex = 1
        Me.CtrlPagination1.TotalRecords = 0
        '
        'ArticleName
        '
        Me.ArticleName.DataPropertyName = "ArticleName"
        Me.ArticleName.HeaderText = "Sub Ingredient Name"
        Me.ArticleName.Name = "ArticleName"
        Me.ArticleName.ReadOnly = True
        Me.ArticleName.Width = 220
        '
        'RecipeCode
        '
        Me.RecipeCode.DataPropertyName = "RecipeCode"
        Me.RecipeCode.HeaderText = "Batch Type"
        Me.RecipeCode.Name = "RecipeCode"
        Me.RecipeCode.Width = 170
        '
        'BatchCount
        '
        Me.BatchCount.DataPropertyName = "BatchCount"
        Me.BatchCount.HeaderText = "No. Of Batches"
        Me.BatchCount.Name = "BatchCount"
        Me.BatchCount.Width = 150
        '
        'EnteredQty
        '
        Me.EnteredQty.DataPropertyName = "EnteredQty"
        Me.EnteredQty.HeaderText = "Produced Qty."
        Me.EnteredQty.Name = "EnteredQty"
        Me.EnteredQty.Width = 140
        '
        'ArticleBaseUOM
        '
        Me.ArticleBaseUOM.DataPropertyName = "ArticleBaseUOM"
        Me.ArticleBaseUOM.HeaderText = "UOM"
        Me.ArticleBaseUOM.Name = "ArticleBaseUOM"
        Me.ArticleBaseUOM.ReadOnly = True
        '
        'CalculatedQty
        '
        Me.CalculatedQty.DataPropertyName = "CalculatedQty"
        Me.CalculatedQty.HeaderText = "Expected Qty."
        Me.CalculatedQty.Name = "CalculatedQty"
        Me.CalculatedQty.ReadOnly = True
        Me.CalculatedQty.Width = 140
        '
        'SubProductDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "SubProductDetails"
        Me.Size = New System.Drawing.Size(969, 616)
        CType(Me.dgSubProductDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgSubProductDetails As System.Windows.Forms.DataGridView
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CtrlPagination1 As Spectrum.CtrlPagination
    Friend WithEvents ArticleName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RecipeCode As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents BatchCount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EnteredQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ArticleBaseUOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CalculatedQty As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
