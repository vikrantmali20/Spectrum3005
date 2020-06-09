<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPromotion
    Inherits Spectrum.CtrlPopupForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPromotion))
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.CtrlBtn5 = New Spectrum.CtrlBtn()
        Me.CtrlLabel6 = New Spectrum.CtrlLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDefaultPromo = New Spectrum.Controls.Button(Me.components)
        Me.btnClearPromo = New Spectrum.Controls.Button(Me.components)
        Me.btnSelectPromo = New Spectrum.Controls.Button(Me.components)
        Me.txtCalCollectAmount = New Spectrum.CtrlTextBox()
        Me.txtCardNo = New Spectrum.CtrlTextBox()
        Me.dtpExpiryDate = New Spectrum.ctrlDate()
        Me.txtTotalAmt = New Spectrum.CtrlTextBox()
        Me.txtCollectAmt = New Spectrum.CtrlTextBox()
        Me.txtRemark = New Spectrum.CtrlTextBox()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.CtrlLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.txtCalCollectAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCardNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpExpiryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCollectAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.BackColor = System.Drawing.Color.DimGray
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Panel4, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Panel1, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.27273!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(432, 214)
        Me.TableLayoutPanel2.TabIndex = 22
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(191, Byte), Integer))
        Me.Panel4.Controls.Add(Me.CtrlBtn5)
        Me.Panel4.Controls.Add(Me.CtrlLabel6)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(432, 39)
        Me.Panel4.TabIndex = 24
        '
        'CtrlBtn5
        '
        Me.CtrlBtn5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.CtrlBtn5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.CtrlBtn5.FlatAppearance.BorderSize = 0
        Me.CtrlBtn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CtrlBtn5.Font = New System.Drawing.Font("Verdana", 9.25!, System.Drawing.FontStyle.Bold)
        Me.CtrlBtn5.ForeColor = System.Drawing.Color.White
        Me.CtrlBtn5.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CtrlBtn5.Location = New System.Drawing.Point(403, 4)
        Me.CtrlBtn5.Name = "CtrlBtn5"
        Me.CtrlBtn5.SetArticleCode = Nothing
        Me.CtrlBtn5.SetRowIndex = 0
        Me.CtrlBtn5.Size = New System.Drawing.Size(22, 23)
        Me.CtrlBtn5.TabIndex = 1
        Me.CtrlBtn5.Text = "X"
        Me.CtrlBtn5.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CtrlBtn5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CtrlBtn5.UseVisualStyleBackColor = False
        Me.CtrlBtn5.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel6
        '
        Me.CtrlLabel6.AttachedTextBoxName = Nothing
        Me.CtrlLabel6.AutoSize = True
        Me.CtrlLabel6.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel6.BorderColor = System.Drawing.Color.Transparent
        Me.CtrlLabel6.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold)
        Me.CtrlLabel6.ForeColor = System.Drawing.Color.White
        Me.CtrlLabel6.Location = New System.Drawing.Point(167, 11)
        Me.CtrlLabel6.Name = "CtrlLabel6"
        Me.CtrlLabel6.Size = New System.Drawing.Size(93, 18)
        Me.CtrlLabel6.TabIndex = 0
        Me.CtrlLabel6.Tag = Nothing
        Me.CtrlLabel6.Text = "Promotion"
        Me.CtrlLabel6.TextDetached = True
        Me.CtrlLabel6.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Gray
        Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 39)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(432, 175)
        Me.Panel1.TabIndex = 25
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnDefaultPromo, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnClearPromo, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnSelectPromo, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(38, 33)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(347, 100)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'btnDefaultPromo
        '
        Me.btnDefaultPromo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDefaultPromo.ForeColor = System.Drawing.Color.Black
        Me.btnDefaultPromo.Location = New System.Drawing.Point(3, 3)
        Me.btnDefaultPromo.MinimumSize = New System.Drawing.Size(15, 23)
        Me.btnDefaultPromo.Name = "btnDefaultPromo"
        Me.btnDefaultPromo.Size = New System.Drawing.Size(102, 90)
        Me.btnDefaultPromo.TabIndex = 0
        Me.btnDefaultPromo.Text = "Default Promotion  (Ctrl + D)"
        Me.btnDefaultPromo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDefaultPromo.UseVisualStyleBackColor = True
        Me.btnDefaultPromo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'btnClearPromo
        '
        Me.btnClearPromo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearPromo.ForeColor = System.Drawing.Color.Black
        Me.btnClearPromo.Location = New System.Drawing.Point(233, 3)
        Me.btnClearPromo.MinimumSize = New System.Drawing.Size(15, 23)
        Me.btnClearPromo.Name = "btnClearPromo"
        Me.btnClearPromo.Size = New System.Drawing.Size(102, 90)
        Me.btnClearPromo.TabIndex = 2
        Me.btnClearPromo.Text = "Clear   Promo    (Ctrl + C)"
        Me.btnClearPromo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClearPromo.UseVisualStyleBackColor = True
        Me.btnClearPromo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'btnSelectPromo
        '
        Me.btnSelectPromo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectPromo.ForeColor = System.Drawing.Color.Black
        Me.btnSelectPromo.Location = New System.Drawing.Point(118, 3)
        Me.btnSelectPromo.MinimumSize = New System.Drawing.Size(15, 23)
        Me.btnSelectPromo.Name = "btnSelectPromo"
        Me.btnSelectPromo.Size = New System.Drawing.Size(102, 90)
        Me.btnSelectPromo.TabIndex = 1
        Me.btnSelectPromo.Text = "Select Promo    (Ctrl + P)"
        Me.btnSelectPromo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSelectPromo.UseVisualStyleBackColor = True
        Me.btnSelectPromo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'txtCalCollectAmount
        '
        Me.txtCalCollectAmount.AcceptsEscape = False
        Me.txtCalCollectAmount.AutoSize = False
        Me.txtCalCollectAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtCalCollectAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalCollectAmount.DataType = GetType(Long)
        Me.txtCalCollectAmount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCalCollectAmount.Location = New System.Drawing.Point(139, 1)
        Me.txtCalCollectAmount.Margin = New System.Windows.Forms.Padding(1)
        Me.txtCalCollectAmount.MaxLength = 15
        Me.txtCalCollectAmount.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtCalCollectAmount.Name = "txtCalCollectAmount"
        Me.txtCalCollectAmount.Size = New System.Drawing.Size(198, 28)
        Me.txtCalCollectAmount.TabIndex = 37
        Me.txtCalCollectAmount.Tag = Nothing
        Me.txtCalCollectAmount.TextDetached = True
        Me.txtCalCollectAmount.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtCalCollectAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtCardNo
        '
        Me.txtCardNo.AutoSize = False
        Me.txtCardNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtCardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCardNo.CustomFormat = "0"
        Me.txtCardNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCardNo.Location = New System.Drawing.Point(139, 61)
        Me.txtCardNo.Margin = New System.Windows.Forms.Padding(1)
        Me.txtCardNo.MaxLength = 18
        Me.txtCardNo.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtCardNo.Name = "txtCardNo"
        Me.txtCardNo.Size = New System.Drawing.Size(198, 28)
        Me.txtCardNo.TabIndex = 39
        Me.txtCardNo.Tag = "NO"
        Me.txtCardNo.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtCardNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'dtpExpiryDate
        '
        Me.dtpExpiryDate.AutoSize = False
        Me.dtpExpiryDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.dtpExpiryDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtpExpiryDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage"), System.Drawing.Image)
        '
        '
        '
        Me.dtpExpiryDate.Calendar.AccessibleRole = System.Windows.Forms.AccessibleRole.Table
        Me.dtpExpiryDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtpExpiryDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtpExpiryDate.DisplayFormat.CustomFormat = "MM-yyyy"
        Me.dtpExpiryDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.dtpExpiryDate.DisplayFormat.Inherit = CType((((C1.Win.C1Input.FormatInfoInheritFlags.NullText Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpExpiryDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpExpiryDate.EditFormat.CustomFormat = "MM-yyyy"
        Me.dtpExpiryDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.dtpExpiryDate.EditFormat.Inherit = CType((((C1.Win.C1Input.FormatInfoInheritFlags.NullText Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpExpiryDate.EmptyAsNull = True
        Me.dtpExpiryDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.dtpExpiryDate.Location = New System.Drawing.Point(139, 91)
        Me.dtpExpiryDate.Margin = New System.Windows.Forms.Padding(1)
        Me.dtpExpiryDate.MaxLength = 35
        Me.dtpExpiryDate.Name = "dtpExpiryDate"
        Me.dtpExpiryDate.Size = New System.Drawing.Size(198, 30)
        Me.dtpExpiryDate.TabIndex = 40
        Me.dtpExpiryDate.Tag = Nothing
        Me.dtpExpiryDate.TrimStart = True
        Me.dtpExpiryDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        Me.dtpExpiryDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtpExpiryDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'txtTotalAmt
        '
        Me.txtTotalAmt.AcceptsEscape = False
        Me.txtTotalAmt.AutoSize = False
        Me.txtTotalAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtTotalAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalAmt.DataType = GetType(Long)
        Me.txtTotalAmt.Location = New System.Drawing.Point(137, 1)
        Me.txtTotalAmt.Margin = New System.Windows.Forms.Padding(1)
        Me.txtTotalAmt.MaxLength = 15
        Me.txtTotalAmt.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtTotalAmt.Name = "txtTotalAmt"
        Me.txtTotalAmt.Size = New System.Drawing.Size(201, 24)
        Me.txtTotalAmt.TabIndex = 52
        Me.txtTotalAmt.Tag = Nothing
        Me.txtTotalAmt.TextDetached = True
        Me.txtTotalAmt.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtTotalAmt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtCollectAmt
        '
        Me.txtCollectAmt.AcceptsEscape = False
        Me.txtCollectAmt.AutoSize = False
        Me.txtCollectAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtCollectAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCollectAmt.DataType = GetType(Long)
        Me.txtCollectAmt.Location = New System.Drawing.Point(137, 28)
        Me.txtCollectAmt.Margin = New System.Windows.Forms.Padding(1)
        Me.txtCollectAmt.MaxLength = 15
        Me.txtCollectAmt.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtCollectAmt.Name = "txtCollectAmt"
        Me.txtCollectAmt.Size = New System.Drawing.Size(201, 25)
        Me.txtCollectAmt.TabIndex = 50
        Me.txtCollectAmt.Tag = Nothing
        Me.txtCollectAmt.TextDetached = True
        Me.txtCollectAmt.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtCollectAmt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtRemark
        '
        Me.txtRemark.AcceptsEscape = False
        Me.txtRemark.AutoSize = False
        Me.txtRemark.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark.DataType = GetType(Long)
        Me.txtRemark.Location = New System.Drawing.Point(137, 55)
        Me.txtRemark.Margin = New System.Windows.Forms.Padding(1)
        Me.txtRemark.MaxLength = 15
        Me.txtRemark.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(201, 43)
        Me.txtRemark.TabIndex = 49
        Me.txtRemark.Tag = Nothing
        Me.txtRemark.TextDetached = True
        Me.txtRemark.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtRemark.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'FrmPromotion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(432, 214)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmPromotion"
        Me.Text = "Accept Payment"
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.CtrlLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.txtCalCollectAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCardNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpExpiryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCollectAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents CtrlLabel6 As Spectrum.CtrlLabel
    Friend WithEvents CtrlBtn5 As Spectrum.CtrlBtn
    Friend WithEvents txtCalCollectAmount As Spectrum.CtrlTextBox
    Friend WithEvents txtCardNo As Spectrum.CtrlTextBox
    Friend WithEvents dtpExpiryDate As Spectrum.ctrlDate
    Friend WithEvents txtTotalAmt As Spectrum.CtrlTextBox
    Friend WithEvents txtCollectAmt As Spectrum.CtrlTextBox
    Friend WithEvents txtRemark As Spectrum.CtrlTextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnClearPromo As Spectrum.Controls.Button
    Friend WithEvents btnSelectPromo As Spectrum.Controls.Button
    Friend WithEvents btnDefaultPromo As Spectrum.Controls.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
End Class
