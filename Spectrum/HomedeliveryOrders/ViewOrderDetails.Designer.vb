<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ViewOrderDetails
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ViewOrderDetails))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.grdViewOrderDetails = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblITotalItem = New Spectrum.Controls.Label(Me.components)
        Me.lblITotalItemValue = New Spectrum.Controls.Label(Me.components)
        Me.lblTotalQuantity = New Spectrum.Controls.Label(Me.components)
        Me.lblTotalQuantityValue = New Spectrum.Controls.Label(Me.components)
        Me.lblTotalAmount = New Spectrum.Controls.Label(Me.components)
        Me.lblTotalAmountValue = New Spectrum.Controls.Label(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.grdViewOrderDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.lblITotalItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblITotalItemValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalQuantityValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalAmountValue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.460838!))
        Me.TableLayoutPanel1.Controls.Add(Me.grdViewOrderDetails, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.539823!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 96.46017!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1014, 449)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'grdViewOrderDetails
        '
        Me.grdViewOrderDetails.AllowEditing = False
        Me.grdViewOrderDetails.ColumnInfo = "7,1,0,0,0,105,Columns:0{Visible:False;}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{Width:196;}" & Global.Microsoft.VisualBasic.ChrW(9) & "2{Width:135;}" & Global.Microsoft.VisualBasic.ChrW(9) & "3{Width:140;}" & _
    "" & Global.Microsoft.VisualBasic.ChrW(9) & "4{Width:107;}" & Global.Microsoft.VisualBasic.ChrW(9) & "5{Width:106;Style:""TextAlign:GeneralCenter;ImageAlign:CenterCente" & _
    "r;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.TableLayoutPanel1.SetColumnSpan(Me.grdViewOrderDetails, 3)
        Me.grdViewOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdViewOrderDetails.ExtendLastCol = True
        Me.grdViewOrderDetails.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdViewOrderDetails.Location = New System.Drawing.Point(3, 17)
        Me.grdViewOrderDetails.Name = "grdViewOrderDetails"
        Me.grdViewOrderDetails.Rows.Count = 1
        Me.grdViewOrderDetails.Rows.DefaultSize = 21
        Me.grdViewOrderDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox
        Me.grdViewOrderDetails.Size = New System.Drawing.Size(1008, 389)
        Me.grdViewOrderDetails.StyleInfo = resources.GetString("grdViewOrderDetails.StyleInfo")
        Me.grdViewOrderDetails.TabIndex = 6
        Me.grdViewOrderDetails.TabStop = False
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 6
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 203.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 137.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 179.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 127.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.lblTotalAmountValue, 5, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblTotalAmount, 4, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblTotalQuantityValue, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblTotalQuantity, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblITotalItemValue, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblITotalItem, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 412)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1008, 34)
        Me.TableLayoutPanel2.TabIndex = 7
        '
        'lblITotalItem
        '
        Me.lblITotalItem.AutoSize = True
        Me.lblITotalItem.BackColor = System.Drawing.Color.Transparent
        Me.lblITotalItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblITotalItem.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblITotalItem.ForeColor = System.Drawing.Color.Black
        Me.lblITotalItem.Location = New System.Drawing.Point(3, 0)
        Me.lblITotalItem.Name = "lblITotalItem"
        Me.lblITotalItem.Size = New System.Drawing.Size(116, 34)
        Me.lblITotalItem.TabIndex = 0
        Me.lblITotalItem.Tag = Nothing
        Me.lblITotalItem.Text = "Total Item:"
        Me.lblITotalItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblITotalItem.TextDetached = True
        Me.lblITotalItem.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'lblITotalItemValue
        '
        Me.lblITotalItemValue.AutoSize = True
        Me.lblITotalItemValue.BackColor = System.Drawing.Color.Transparent
        Me.lblITotalItemValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblITotalItemValue.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblITotalItemValue.ForeColor = System.Drawing.Color.Black
        Me.lblITotalItemValue.Location = New System.Drawing.Point(125, 0)
        Me.lblITotalItemValue.Name = "lblITotalItemValue"
        Me.lblITotalItemValue.Size = New System.Drawing.Size(197, 34)
        Me.lblITotalItemValue.TabIndex = 1
        Me.lblITotalItemValue.Tag = Nothing
        Me.lblITotalItemValue.Text = "00.00"
        Me.lblITotalItemValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblITotalItemValue.TextDetached = True
        Me.lblITotalItemValue.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'lblTotalQuantity
        '
        Me.lblTotalQuantity.AutoSize = True
        Me.lblTotalQuantity.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalQuantity.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTotalQuantity.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblTotalQuantity.ForeColor = System.Drawing.Color.Black
        Me.lblTotalQuantity.Location = New System.Drawing.Point(328, 0)
        Me.lblTotalQuantity.Name = "lblTotalQuantity"
        Me.lblTotalQuantity.Size = New System.Drawing.Size(131, 34)
        Me.lblTotalQuantity.TabIndex = 2
        Me.lblTotalQuantity.Tag = Nothing
        Me.lblTotalQuantity.Text = "Total Quantity :"
        Me.lblTotalQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTotalQuantity.TextDetached = True
        Me.lblTotalQuantity.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'lblTotalQuantityValue
        '
        Me.lblTotalQuantityValue.AutoSize = True
        Me.lblTotalQuantityValue.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalQuantityValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTotalQuantityValue.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblTotalQuantityValue.ForeColor = System.Drawing.Color.Black
        Me.lblTotalQuantityValue.Location = New System.Drawing.Point(465, 0)
        Me.lblTotalQuantityValue.Name = "lblTotalQuantityValue"
        Me.lblTotalQuantityValue.Size = New System.Drawing.Size(173, 34)
        Me.lblTotalQuantityValue.TabIndex = 3
        Me.lblTotalQuantityValue.Tag = Nothing
        Me.lblTotalQuantityValue.Text = "00.00"
        Me.lblTotalQuantityValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTotalQuantityValue.TextDetached = True
        Me.lblTotalQuantityValue.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.AutoSize = True
        Me.lblTotalAmount.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalAmount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTotalAmount.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblTotalAmount.ForeColor = System.Drawing.Color.Black
        Me.lblTotalAmount.Location = New System.Drawing.Point(644, 0)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(121, 34)
        Me.lblTotalAmount.TabIndex = 4
        Me.lblTotalAmount.Tag = Nothing
        Me.lblTotalAmount.Text = "Total Amount:"
        Me.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTotalAmount.TextDetached = True
        Me.lblTotalAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'lblTotalAmountValue
        '
        Me.lblTotalAmountValue.AutoSize = True
        Me.lblTotalAmountValue.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalAmountValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTotalAmountValue.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblTotalAmountValue.ForeColor = System.Drawing.Color.Black
        Me.lblTotalAmountValue.Location = New System.Drawing.Point(771, 0)
        Me.lblTotalAmountValue.Name = "lblTotalAmountValue"
        Me.lblTotalAmountValue.Size = New System.Drawing.Size(234, 34)
        Me.lblTotalAmountValue.TabIndex = 5
        Me.lblTotalAmountValue.Tag = Nothing
        Me.lblTotalAmountValue.Text = "00.00"
        Me.lblTotalAmountValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTotalAmountValue.TextDetached = True
        Me.lblTotalAmountValue.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'ViewOrderDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1014, 449)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ViewOrderDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ViewOrderDetails"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.grdViewOrderDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.lblITotalItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblITotalItemValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalQuantityValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalAmountValue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grdViewOrderDetails As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblTotalAmountValue As Spectrum.Controls.Label
    Friend WithEvents lblTotalAmount As Spectrum.Controls.Label
    Friend WithEvents lblTotalQuantityValue As Spectrum.Controls.Label
    Friend WithEvents lblTotalQuantity As Spectrum.Controls.Label
    Friend WithEvents lblITotalItemValue As Spectrum.Controls.Label
    Friend WithEvents lblITotalItem As Spectrum.Controls.Label
End Class
