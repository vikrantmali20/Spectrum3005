<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCardSummary
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
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdCancel = New Spectrum.CtrlBtn()
        Me.cmdClear = New Spectrum.CtrlBtn()
        Me.cmdPrint = New Spectrum.CtrlBtn()
        Me.cmdNext = New Spectrum.CtrlBtn()
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        Me.txtSummaryAmt = New Spectrum.CtrlTextBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSummaryAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.49351!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.50649!))
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlLabel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtSummaryAmt, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(59, 56)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(482, 32)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 4
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.75!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.25!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.cmdCancel, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.cmdClear, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.cmdPrint, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.cmdNext, 1, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(59, 191)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(482, 38)
        Me.TableLayoutPanel2.TabIndex = 3
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdCancel.Location = New System.Drawing.Point(354, 3)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.SetArticleCode = Nothing
        Me.cmdCancel.SetRowIndex = 0
        Me.cmdCancel.Size = New System.Drawing.Size(116, 29)
        Me.cmdCancel.TabIndex = 4
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdCancel.UseVisualStyleBackColor = True
        Me.cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdClear
        '
        Me.cmdClear.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdClear.Location = New System.Drawing.Point(229, 3)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.SetArticleCode = Nothing
        Me.cmdClear.SetRowIndex = 0
        Me.cmdClear.Size = New System.Drawing.Size(116, 29)
        Me.cmdClear.TabIndex = 3
        Me.cmdClear.Text = "Clear"
        Me.cmdClear.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdClear.UseVisualStyleBackColor = True
        Me.cmdClear.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdPrint
        '
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(3, 3)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.SetArticleCode = Nothing
        Me.cmdPrint.SetRowIndex = 0
        Me.cmdPrint.Size = New System.Drawing.Size(104, 29)
        Me.cmdPrint.TabIndex = 1
        Me.cmdPrint.Text = "Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdPrint.UseVisualStyleBackColor = True
        Me.cmdPrint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdNext
        '
        Me.cmdNext.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdNext.Location = New System.Drawing.Point(113, 3)
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.SetArticleCode = Nothing
        Me.cmdNext.SetRowIndex = 0
        Me.cmdNext.Size = New System.Drawing.Size(110, 29)
        Me.cmdNext.TabIndex = 2
        Me.cmdNext.Text = "Next"
        Me.cmdNext.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdNext.UseVisualStyleBackColor = True
        Me.cmdNext.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel1
        '
        Me.CtrlLabel1.AttachedTextBoxName = Nothing
        Me.CtrlLabel1.AutoSize = True
        Me.CtrlLabel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel1.Font = New System.Drawing.Font("Roboto", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlLabel1.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel1.Location = New System.Drawing.Point(3, 0)
        Me.CtrlLabel1.Name = "CtrlLabel1"
        Me.CtrlLabel1.Padding = New System.Windows.Forms.Padding(4)
        Me.CtrlLabel1.Size = New System.Drawing.Size(261, 29)
        Me.CtrlLabel1.TabIndex = 0
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.Text = "Enter Card Summary as per EDC -"
        Me.CtrlLabel1.TextDetached = True
        Me.CtrlLabel1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtSummaryAmt
        '
        Me.txtSummaryAmt.AutoSize = False
        Me.txtSummaryAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtSummaryAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSummaryAmt.DataType = GetType(Integer)
        Me.txtSummaryAmt.Font = New System.Drawing.Font("Roboto", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSummaryAmt.Location = New System.Drawing.Point(275, 3)
        Me.txtSummaryAmt.MaxLength = 14
        Me.txtSummaryAmt.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtSummaryAmt.Name = "txtSummaryAmt"
        Me.txtSummaryAmt.Size = New System.Drawing.Size(204, 26)
        Me.txtSummaryAmt.TabIndex = 1
        Me.txtSummaryAmt.Tag = Nothing
        Me.txtSummaryAmt.TextDetached = True
        Me.txtSummaryAmt.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtSummaryAmt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmCardSummary
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(614, 264)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Roboto", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCardSummary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Card Summary"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSummaryAmt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CtrlLabel1 As Spectrum.CtrlLabel
    Friend WithEvents txtSummaryAmt As Spectrum.CtrlTextBox
    Friend WithEvents cmdPrint As Spectrum.CtrlBtn
    Friend WithEvents cmdNext As Spectrum.CtrlBtn
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmdCancel As Spectrum.CtrlBtn
    Friend WithEvents cmdClear As Spectrum.CtrlBtn
End Class
