<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FinishedProductDetails
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.dgFinishedProductDetails = New System.Windows.Forms.DataGridView()
        Me.CtrlPagination1 = New Spectrum.CtrlPagination()
        Me.ArticleName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EnteredQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.dgFinishedProductDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.dgFinishedProductDetails, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlPagination1, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(964, 616)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'dgFinishedProductDetails
        '
        Me.dgFinishedProductDetails.AllowUserToAddRows = False
        Me.dgFinishedProductDetails.AllowUserToDeleteRows = False
        Me.dgFinishedProductDetails.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgFinishedProductDetails.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgFinishedProductDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgFinishedProductDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ArticleName, Me.EnteredQty})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgFinishedProductDetails.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgFinishedProductDetails.Location = New System.Drawing.Point(3, 3)
        Me.dgFinishedProductDetails.Name = "dgFinishedProductDetails"
        Me.dgFinishedProductDetails.RowHeadersWidth = 20
        Me.dgFinishedProductDetails.Size = New System.Drawing.Size(958, 560)
        Me.dgFinishedProductDetails.TabIndex = 0
        '
        'CtrlPagination1
        '
        Me.CtrlPagination1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CtrlPagination1.AutoSize = True
        Me.CtrlPagination1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CtrlPagination1.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlPagination1.LastActivePageNo = 1
        Me.CtrlPagination1.Location = New System.Drawing.Point(477, 591)
        Me.CtrlPagination1.Name = "CtrlPagination1"
        Me.CtrlPagination1.Size = New System.Drawing.Size(9, 0)
        Me.CtrlPagination1.TabIndex = 1
        Me.CtrlPagination1.TotalRecords = 0
        '
        'ArticleName
        '
        Me.ArticleName.DataPropertyName = "ArticleName"
        Me.ArticleName.HeaderText = "Product Name"
        Me.ArticleName.Name = "ArticleName"
        Me.ArticleName.ReadOnly = True
        Me.ArticleName.Width = 250
        '
        'EnteredQty
        '
        Me.EnteredQty.DataPropertyName = "EnteredQty"
        Me.EnteredQty.HeaderText = "Quantity Produced"
        Me.EnteredQty.Name = "EnteredQty"
        Me.EnteredQty.Width = 170
        '
        'FinishedProductDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "FinishedProductDetails"
        Me.Size = New System.Drawing.Size(964, 616)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.dgFinishedProductDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents dgFinishedProductDetails As System.Windows.Forms.DataGridView
    Friend WithEvents CtrlPagination1 As Spectrum.CtrlPagination
    Friend WithEvents ArticleName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EnteredQty As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
