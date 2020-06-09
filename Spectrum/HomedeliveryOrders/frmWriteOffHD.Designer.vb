<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWriteOffHD
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
        Me.lblRemarks = New Spectrum.CtrlLabel()
        Me.lblBillAmt = New Spectrum.CtrlLabel()
        Me.lblAmtPaid = New Spectrum.CtrlLabel()
        Me.lblBalAmt = New Spectrum.CtrlLabel()
        Me.lblWriteOffAmt = New Spectrum.CtrlLabel()
        Me.txtWriteOffAmt = New Spectrum.CtrlTextBox()
        Me.txtRemark = New Spectrum.CtrlTextBox()
        Me.CmdCancel = New Spectrum.CtrlBtn()
        Me.CmdOk = New Spectrum.CtrlBtn()
        Me.lblBillAmtValue = New Spectrum.CtrlLabel()
        Me.lblAmtPaidValue = New Spectrum.CtrlLabel()
        Me.lblBalAmtValue = New Spectrum.CtrlLabel()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBillAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtPaid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBalAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblWriteOffAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWriteOffAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBillAmtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmtPaidValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBalAmtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.6749!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.3251!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblRemarks, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.lblBillAmt, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblAmtPaid, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblBalAmt, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblWriteOffAmt, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.txtWriteOffAmt, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.txtRemark, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.CmdCancel, 3, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.CmdOk, 2, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.lblBillAmtValue, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblAmtPaidValue, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblBalAmtValue, 1, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 7
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.2198!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.36612!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.51245!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.36612!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.8734!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.66211!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(464, 259)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'lblRemarks
        '
        Me.lblRemarks.AttachedTextBoxName = Nothing
        Me.lblRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblRemarks.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRemarks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRemarks.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(3, 153)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(122, 52)
        Me.lblRemarks.TabIndex = 13
        Me.lblRemarks.Tag = Nothing
        Me.lblRemarks.Text = "Remarks :"
        Me.lblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblRemarks.TextDetached = True
        '
        'lblBillAmt
        '
        Me.lblBillAmt.AttachedTextBoxName = Nothing
        Me.lblBillAmt.AutoSize = True
        Me.lblBillAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblBillAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblBillAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBillAmt.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblBillAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillAmt.ForeColor = System.Drawing.Color.Black
        Me.lblBillAmt.Location = New System.Drawing.Point(3, 24)
        Me.lblBillAmt.Name = "lblBillAmt"
        Me.lblBillAmt.Size = New System.Drawing.Size(122, 17)
        Me.lblBillAmt.TabIndex = 2
        Me.lblBillAmt.Tag = Nothing
        Me.lblBillAmt.Text = "Bill Amount :"
        Me.lblBillAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblBillAmt.TextDetached = True
        Me.lblBillAmt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblAmtPaid
        '
        Me.lblAmtPaid.AttachedTextBoxName = Nothing
        Me.lblAmtPaid.AutoSize = True
        Me.lblAmtPaid.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblAmtPaid.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblAmtPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAmtPaid.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblAmtPaid.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmtPaid.Location = New System.Drawing.Point(3, 62)
        Me.lblAmtPaid.Name = "lblAmtPaid"
        Me.lblAmtPaid.Size = New System.Drawing.Size(122, 17)
        Me.lblAmtPaid.TabIndex = 3
        Me.lblAmtPaid.Tag = Nothing
        Me.lblAmtPaid.Text = "Amount Paid :"
        Me.lblAmtPaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAmtPaid.TextDetached = True
        '
        'lblBalAmt
        '
        Me.lblBalAmt.AttachedTextBoxName = Nothing
        Me.lblBalAmt.AutoSize = True
        Me.lblBalAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblBalAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblBalAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBalAmt.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblBalAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalAmt.Location = New System.Drawing.Point(3, 98)
        Me.lblBalAmt.Name = "lblBalAmt"
        Me.lblBalAmt.Size = New System.Drawing.Size(122, 17)
        Me.lblBalAmt.TabIndex = 4
        Me.lblBalAmt.Tag = Nothing
        Me.lblBalAmt.Text = "Balance Amount :"
        Me.lblBalAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblBalAmt.TextDetached = True
        '
        'lblWriteOffAmt
        '
        Me.lblWriteOffAmt.AttachedTextBoxName = Nothing
        Me.lblWriteOffAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblWriteOffAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblWriteOffAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblWriteOffAmt.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblWriteOffAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWriteOffAmt.Location = New System.Drawing.Point(3, 136)
        Me.lblWriteOffAmt.Name = "lblWriteOffAmt"
        Me.lblWriteOffAmt.Size = New System.Drawing.Size(122, 17)
        Me.lblWriteOffAmt.TabIndex = 5
        Me.lblWriteOffAmt.Tag = Nothing
        Me.lblWriteOffAmt.Text = "Write Off Amount :"
        Me.lblWriteOffAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblWriteOffAmt.TextDetached = True
        '
        'txtWriteOffAmt
        '
        Me.txtWriteOffAmt.AutoSize = False
        Me.txtWriteOffAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtWriteOffAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtWriteOffAmt, 3)
        Me.txtWriteOffAmt.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtWriteOffAmt.Location = New System.Drawing.Point(131, 129)
        Me.txtWriteOffAmt.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtWriteOffAmt.Name = "txtWriteOffAmt"
        Me.txtWriteOffAmt.Size = New System.Drawing.Size(302, 21)
        Me.txtWriteOffAmt.TabIndex = 0
        Me.txtWriteOffAmt.Tag = Nothing
        Me.txtWriteOffAmt.VerticalAlign = C1.Win.C1Input.VerticalAlignEnum.Middle
        Me.txtWriteOffAmt.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtWriteOffAmt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtRemark
        '
        Me.txtRemark.AutoSize = False
        Me.txtRemark.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtRemark, 3)
        Me.txtRemark.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtRemark.Location = New System.Drawing.Point(131, 164)
        Me.txtRemark.MaxLength = 2000
        Me.txtRemark.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(302, 38)
        Me.txtRemark.TabIndex = 1
        Me.txtRemark.Tag = Nothing
        Me.txtRemark.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtRemark.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CmdCancel
        '
        Me.CmdCancel.Dock = System.Windows.Forms.DockStyle.Top
        Me.CmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdCancel.Location = New System.Drawing.Point(342, 208)
        Me.CmdCancel.Name = "CmdCancel"
        Me.CmdCancel.SetArticleCode = Nothing
        Me.CmdCancel.SetRowIndex = 0
        Me.CmdCancel.Size = New System.Drawing.Size(91, 25)
        Me.CmdCancel.TabIndex = 3
        Me.CmdCancel.Text = "&Cancel"
        Me.CmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CmdCancel.UseVisualStyleBackColor = True
        Me.CmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CmdOk
        '
        Me.CmdOk.Dock = System.Windows.Forms.DockStyle.Top
        Me.CmdOk.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdOk.Location = New System.Drawing.Point(246, 208)
        Me.CmdOk.Name = "CmdOk"
        Me.CmdOk.SetArticleCode = Nothing
        Me.CmdOk.SetRowIndex = 0
        Me.CmdOk.Size = New System.Drawing.Size(90, 25)
        Me.CmdOk.TabIndex = 2
        Me.CmdOk.Text = "&Ok"
        Me.CmdOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CmdOk.UseVisualStyleBackColor = True
        Me.CmdOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblBillAmtValue
        '
        Me.lblBillAmtValue.AttachedTextBoxName = Nothing
        Me.lblBillAmtValue.AutoSize = True
        Me.lblBillAmtValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblBillAmtValue.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.lblBillAmtValue, 3)
        Me.lblBillAmtValue.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblBillAmtValue.Location = New System.Drawing.Point(131, 28)
        Me.lblBillAmtValue.Name = "lblBillAmtValue"
        Me.lblBillAmtValue.Size = New System.Drawing.Size(302, 13)
        Me.lblBillAmtValue.TabIndex = 14
        Me.lblBillAmtValue.Tag = Nothing
        Me.lblBillAmtValue.Text = "lblBillAmtValue"
        Me.lblBillAmtValue.TextDetached = True
        '
        'lblAmtPaidValue
        '
        Me.lblAmtPaidValue.AttachedTextBoxName = Nothing
        Me.lblAmtPaidValue.AutoSize = True
        Me.lblAmtPaidValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblAmtPaidValue.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.lblAmtPaidValue, 3)
        Me.lblAmtPaidValue.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblAmtPaidValue.Location = New System.Drawing.Point(131, 66)
        Me.lblAmtPaidValue.Name = "lblAmtPaidValue"
        Me.lblAmtPaidValue.Size = New System.Drawing.Size(302, 13)
        Me.lblAmtPaidValue.TabIndex = 15
        Me.lblAmtPaidValue.Tag = Nothing
        Me.lblAmtPaidValue.Text = "lblAmtPaidValue"
        Me.lblAmtPaidValue.TextDetached = True
        '
        'lblBalAmtValue
        '
        Me.lblBalAmtValue.AttachedTextBoxName = Nothing
        Me.lblBalAmtValue.AutoSize = True
        Me.lblBalAmtValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblBalAmtValue.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TableLayoutPanel1.SetColumnSpan(Me.lblBalAmtValue, 3)
        Me.lblBalAmtValue.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblBalAmtValue.Location = New System.Drawing.Point(131, 102)
        Me.lblBalAmtValue.Name = "lblBalAmtValue"
        Me.lblBalAmtValue.Size = New System.Drawing.Size(302, 13)
        Me.lblBalAmtValue.TabIndex = 16
        Me.lblBalAmtValue.Tag = Nothing
        Me.lblBalAmtValue.Text = "lblBalAmtValue"
        Me.lblBalAmtValue.TextDetached = True
        '
        'frmWriteOffCreditSales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(464, 259)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmWriteOffCreditSales"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Write Off"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBillAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtPaid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBalAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblWriteOffAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWriteOffAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBillAmtValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmtPaidValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBalAmtValue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblRemarks As Spectrum.CtrlLabel
    Friend WithEvents lblBillAmt As Spectrum.CtrlLabel
    Friend WithEvents lblAmtPaid As Spectrum.CtrlLabel
    Friend WithEvents lblBalAmt As Spectrum.CtrlLabel
    Friend WithEvents lblWriteOffAmt As Spectrum.CtrlLabel
    Friend WithEvents txtWriteOffAmt As Spectrum.CtrlTextBox
    Friend WithEvents txtRemark As Spectrum.CtrlTextBox
    Friend WithEvents CmdCancel As Spectrum.CtrlBtn
    Friend WithEvents CmdOk As Spectrum.CtrlBtn
    Friend WithEvents lblBillAmtValue As Spectrum.CtrlLabel
    Friend WithEvents lblAmtPaidValue As Spectrum.CtrlLabel
    Friend WithEvents lblBalAmtValue As Spectrum.CtrlLabel
End Class
