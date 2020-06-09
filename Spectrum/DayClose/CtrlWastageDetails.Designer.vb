<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrlWastageDetails
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
        Me.dgWastageDetails = New CtrlDataGridView()
        Me.IsSelected = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ArticleName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WastageUOMCode = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.EnteredQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReasonCode = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.CtrlPagination1 = New Spectrum.CtrlPagination()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnAddItem = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.dgWastageDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.dgWastageDetails, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlPagination1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(969, 628)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'dgWastageDetails
        '
        Me.dgWastageDetails.AllowUserToAddRows = False
        Me.dgWastageDetails.AllowUserToDeleteRows = False
        Me.dgWastageDetails.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgWastageDetails.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgWastageDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgWastageDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IsSelected, Me.ArticleName, Me.WastageUOMCode, Me.EnteredQty, Me.ReasonCode})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgWastageDetails.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgWastageDetails.Location = New System.Drawing.Point(3, 30)
        Me.dgWastageDetails.Name = "dgWastageDetails"
        Me.dgWastageDetails.RowHeadersWidth = 20
        Me.dgWastageDetails.Size = New System.Drawing.Size(958, 555)
        Me.dgWastageDetails.TabIndex = 0
        '
        'IsSelected
        '
        Me.IsSelected.DataPropertyName = "IsSelected"
        Me.IsSelected.HeaderText = "Select"
        Me.IsSelected.Name = "IsSelected"
        Me.IsSelected.Width = 70
        '
        'ArticleName
        '
        Me.ArticleName.DataPropertyName = "ArticleName"
        Me.ArticleName.HeaderText = "Product Name"
        Me.ArticleName.Name = "ArticleName"
        Me.ArticleName.ReadOnly = True
        Me.ArticleName.Width = 250
        '
        'WastageUOMCode
        '
        Me.WastageUOMCode.DataPropertyName = "WastageUOMCode"
        Me.WastageUOMCode.HeaderText = "UOM"
        Me.WastageUOMCode.Name = "WastageUOMCode"
        '
        'EnteredQty
        '
        Me.EnteredQty.DataPropertyName = "EnteredQty"
        Me.EnteredQty.HeaderText = "Quantity Produced"
        Me.EnteredQty.Name = "EnteredQty"
        Me.EnteredQty.Width = 170
        '
        'ReasonCode
        '
        Me.ReasonCode.DataPropertyName = "ReasonCode"
        Me.ReasonCode.HeaderText = "Reason"
        Me.ReasonCode.Name = "ReasonCode"
        Me.ReasonCode.Width = 220
        '
        'CtrlPagination1
        '
        Me.CtrlPagination1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CtrlPagination1.AutoSize = True
        Me.CtrlPagination1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CtrlPagination1.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlPagination1.LastActivePageNo = 1
        Me.CtrlPagination1.Location = New System.Drawing.Point(3, 591)
        Me.CtrlPagination1.Name = "CtrlPagination1"
        Me.CtrlPagination1.Size = New System.Drawing.Size(963, 34)
        Me.CtrlPagination1.TabIndex = 1
        Me.CtrlPagination1.TotalRecords = 0
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.Controls.Add(Me.btnAddItem)
        Me.Panel1.Controls.Add(Me.btnDelete)
        Me.Panel1.Location = New System.Drawing.Point(3, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(162, 26)
        Me.Panel1.TabIndex = 2
        '
        'btnAddItem
        '
        Me.btnAddItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.btnAddItem.Location = New System.Drawing.Point(84, 3)
        Me.btnAddItem.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.btnAddItem.Name = "btnAddItem"
        Me.btnAddItem.Size = New System.Drawing.Size(75, 23)
        Me.btnAddItem.TabIndex = 1
        Me.btnAddItem.Text = "Add Item"
        Me.btnAddItem.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
        Me.btnDelete.Location = New System.Drawing.Point(3, 3)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 0
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'CtrlWastageDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "CtrlWastageDetails"
        Me.Size = New System.Drawing.Size(969, 628)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.dgWastageDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents dgWastageDetails As System.Windows.Forms.DataGridView
    Friend WithEvents CtrlPagination1 As Spectrum.CtrlPagination
    Friend WithEvents IsSelected As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ArticleName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WastageUOMCode As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents EnteredQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReasonCode As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnAddItem As System.Windows.Forms.Button

End Class
