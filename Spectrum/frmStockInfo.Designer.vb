<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStockInfo
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.dgStockInfo = New System.Windows.Forms.DataGridView()
        Me.DeliverySiteName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ArticleName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AvailableQuantity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Quantity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbSite = New System.Windows.Forms.ComboBox()
        Me.lblExpectedQty = New System.Windows.Forms.Label()
        Me.lblExpectedQtyValue = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.dgStockInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.FlowLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.dgStockInfo, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.FlowLayoutPanel1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.FlowLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.3913!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.439331!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.26087!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(599, 101)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'dgStockInfo
        '
        Me.dgStockInfo.AllowUserToAddRows = False
        Me.dgStockInfo.AllowUserToDeleteRows = False
        Me.dgStockInfo.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.dgStockInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgStockInfo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DeliverySiteName, Me.ArticleName, Me.AvailableQuantity, Me.Quantity})
        Me.dgStockInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgStockInfo.Location = New System.Drawing.Point(3, 45)
        Me.dgStockInfo.Name = "dgStockInfo"
        Me.dgStockInfo.RowHeadersVisible = False
        Me.dgStockInfo.Size = New System.Drawing.Size(593, 1)
        Me.dgStockInfo.TabIndex = 1
        Me.dgStockInfo.Visible = False
        '
        'DeliverySiteName
        '
        Me.DeliverySiteName.DataPropertyName = "DeliverySiteName"
        Me.DeliverySiteName.HeaderText = "Site Name"
        Me.DeliverySiteName.Name = "DeliverySiteName"
        Me.DeliverySiteName.ReadOnly = True
        Me.DeliverySiteName.Width = 200
        '
        'ArticleName
        '
        Me.ArticleName.DataPropertyName = "ArticleName"
        Me.ArticleName.HeaderText = "Article Name"
        Me.ArticleName.Name = "ArticleName"
        Me.ArticleName.ReadOnly = True
        Me.ArticleName.Width = 200
        '
        'AvailableQuantity
        '
        Me.AvailableQuantity.DataPropertyName = "AvailableQuantity"
        Me.AvailableQuantity.HeaderText = "Available Quantity"
        Me.AvailableQuantity.Name = "AvailableQuantity"
        Me.AvailableQuantity.ReadOnly = True
        Me.AvailableQuantity.Width = 145
        '
        'Quantity
        '
        Me.Quantity.DataPropertyName = "Quantity"
        Me.Quantity.HeaderText = "Entered Quantity"
        Me.Quantity.Name = "Quantity"
        Me.Quantity.Width = 140
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.FlowLayoutPanel1.Controls.Add(Me.btnOk)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnCancel)
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(378, 54)
        Me.FlowLayoutPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(217, 40)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(4, 4)
        Me.btnOk.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(100, 35)
        Me.btnOk.TabIndex = 0
        Me.btnOk.Text = "Ok"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(112, 4)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 35)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.Controls.Add(Me.Label1)
        Me.FlowLayoutPanel2.Controls.Add(Me.cmbSite)
        Me.FlowLayoutPanel2.Controls.Add(Me.lblExpectedQty)
        Me.FlowLayoutPanel2.Controls.Add(Me.lblExpectedQtyValue)
        Me.FlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(593, 36)
        Me.FlowLayoutPanel2.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 11, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 17)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Select Site"
        '
        'cmbSite
        '
        Me.cmbSite.FormattingEnabled = True
        Me.cmbSite.Location = New System.Drawing.Point(84, 9)
        Me.cmbSite.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
        Me.cmbSite.Name = "cmbSite"
        Me.cmbSite.Size = New System.Drawing.Size(264, 24)
        Me.cmbSite.TabIndex = 6
        '
        'lblExpectedQty
        '
        Me.lblExpectedQty.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblExpectedQty.AutoSize = True
        Me.lblExpectedQty.Location = New System.Drawing.Point(354, 9)
        Me.lblExpectedQty.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblExpectedQty.Name = "lblExpectedQty"
        Me.lblExpectedQty.Size = New System.Drawing.Size(159, 17)
        Me.lblExpectedQty.TabIndex = 4
        Me.lblExpectedQty.Text = "Total quantity to enter : "
        Me.lblExpectedQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblExpectedQty.Visible = False
        '
        'lblExpectedQtyValue
        '
        Me.lblExpectedQtyValue.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblExpectedQtyValue.AutoSize = True
        Me.lblExpectedQtyValue.Location = New System.Drawing.Point(516, 9)
        Me.lblExpectedQtyValue.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblExpectedQtyValue.Name = "lblExpectedQtyValue"
        Me.lblExpectedQtyValue.Size = New System.Drawing.Size(16, 17)
        Me.lblExpectedQtyValue.TabIndex = 3
        Me.lblExpectedQtyValue.Text = "0"
        Me.lblExpectedQtyValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblExpectedQtyValue.Visible = False
        '
        'frmStockInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(599, 101)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmStockInfo"
        Me.Text = "Select Delivery Site"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.dgStockInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel2.ResumeLayout(False)
        Me.FlowLayoutPanel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents dgStockInfo As System.Windows.Forms.DataGridView
    Friend WithEvents lblExpectedQtyValue As System.Windows.Forms.Label
    Friend WithEvents FlowLayoutPanel2 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblExpectedQty As System.Windows.Forms.Label
    Friend WithEvents DeliverySiteName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ArticleName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AvailableQuantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Quantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbSite As System.Windows.Forms.ComboBox
End Class
