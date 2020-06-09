<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentHD
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPaymentHD))
        Me.sizTop = New C1.Win.C1Sizer.C1Sizer()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnOk = New Spectrum.CtrlBtn()
        Me.btnExit = New Spectrum.CtrlBtn()
        Me.CtrlCreditCard1 = New Spectrum.ctrlCreditCard()
        Me.CtrlChequeDetails1 = New Spectrum.ctrlChequeDetails()
        Me.txtRemark = New Spectrum.CtrlTextBox()
        Me.txtCollectAmount = New Spectrum.CtrlTextBox()
        Me.txttotalAmount = New Spectrum.CtrlTextBox()
        Me.lblRemark = New Spectrum.CtrlLabel()
        Me.lblCollectAmount = New Spectrum.CtrlLabel()
        Me.lbltotalAmount = New Spectrum.CtrlLabel()
        CType(Me.sizTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sizTop.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCollectAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCollectAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbltotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sizTop
        '
        Me.sizTop.Border.Color = System.Drawing.Color.LightSlateGray
        Me.sizTop.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.sizTop.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.sizTop.Controls.Add(Me.TableLayoutPanel1)
        Me.sizTop.Controls.Add(Me.CtrlCreditCard1)
        Me.sizTop.Controls.Add(Me.CtrlChequeDetails1)
        Me.sizTop.Controls.Add(Me.txtRemark)
        Me.sizTop.Controls.Add(Me.txtCollectAmount)
        Me.sizTop.Controls.Add(Me.txttotalAmount)
        Me.sizTop.Controls.Add(Me.lblRemark)
        Me.sizTop.Controls.Add(Me.lblCollectAmount)
        Me.sizTop.Controls.Add(Me.lbltotalAmount)
        Me.sizTop.GridDefinition = resources.GetString("sizTop.GridDefinition")
        Me.sizTop.Location = New System.Drawing.Point(0, 0)
        Me.sizTop.Margin = New System.Windows.Forms.Padding(0)
        Me.sizTop.Name = "sizTop"
        Me.sizTop.Padding = New System.Windows.Forms.Padding(2)
        Me.sizTop.Size = New System.Drawing.Size(494, 265)
        Me.sizTop.SplitterWidth = 1
        Me.sizTop.TabIndex = 1
        Me.sizTop.TabStop = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnOk, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnExit, 2, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(91, 226)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(379, 36)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'btnOk
        '
        Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOk.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnOk.Location = New System.Drawing.Point(109, 3)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.SetArticleCode = Nothing
        Me.btnOk.SetRowIndex = 0
        Me.btnOk.Size = New System.Drawing.Size(94, 30)
        Me.btnOk.TabIndex = 1
        Me.btnOk.Text = "Ok"
        Me.btnOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOk.UseVisualStyleBackColor = True
        Me.btnOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnExit
        '
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnExit.Location = New System.Drawing.Point(209, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.SetArticleCode = Nothing
        Me.btnExit.SetRowIndex = 0
        Me.btnExit.Size = New System.Drawing.Size(114, 30)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "Cancel"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnExit.UseVisualStyleBackColor = True
        Me.btnExit.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlCreditCard1
        '
        Me.CtrlCreditCard1.AutoSize = True
        Me.CtrlCreditCard1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CtrlCreditCard1.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlCreditCard1.ClsSiteBank = Nothing
        Me.CtrlCreditCard1.Location = New System.Drawing.Point(91, 81)
        Me.CtrlCreditCard1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrlCreditCard1.Name = "CtrlCreditCard1"
        Me.CtrlCreditCard1.Size = New System.Drawing.Size(379, 144)
        Me.CtrlCreditCard1.TabIndex = 3
        '
        'CtrlChequeDetails1
        '
        Me.CtrlChequeDetails1.AutoSize = True
        Me.CtrlChequeDetails1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CtrlChequeDetails1.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlChequeDetails1.ClsSiteBank = Nothing
        Me.CtrlChequeDetails1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlChequeDetails1.Location = New System.Drawing.Point(91, 81)
        Me.CtrlChequeDetails1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrlChequeDetails1.Name = "CtrlChequeDetails1"
        Me.CtrlChequeDetails1.Size = New System.Drawing.Size(379, 144)
        Me.CtrlChequeDetails1.TabIndex = 0
        '
        'txtRemark
        '
        Me.txtRemark.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.txtRemark.AutoSize = False
        Me.txtRemark.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemark.Location = New System.Drawing.Point(91, 55)
        Me.txtRemark.MaximumSize = New System.Drawing.Size(400, 21)
        Me.txtRemark.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemark.Size = New System.Drawing.Size(379, 21)
        Me.txtRemark.TabIndex = 2
        Me.txtRemark.Tag = Nothing
        Me.txtRemark.TextDetached = True
        Me.txtRemark.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtRemark.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtCollectAmount
        '
        Me.txtCollectAmount.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.txtCollectAmount.AutoSize = False
        Me.txtCollectAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtCollectAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCollectAmount.DataType = GetType(Double)
        Me.txtCollectAmount.FormatType = C1.Win.C1Input.FormatTypeEnum.StandardNumber
        Me.txtCollectAmount.Location = New System.Drawing.Point(91, 29)
        Me.txtCollectAmount.MaximumSize = New System.Drawing.Size(400, 21)
        Me.txtCollectAmount.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtCollectAmount.Name = "txtCollectAmount"
        Me.txtCollectAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCollectAmount.Size = New System.Drawing.Size(379, 21)
        Me.txtCollectAmount.TabIndex = 1
        Me.txtCollectAmount.Tag = Nothing
        Me.txtCollectAmount.TextDetached = True
        Me.txtCollectAmount.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtCollectAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txttotalAmount
        '
        Me.txttotalAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txttotalAmount.AutoSize = False
        Me.txttotalAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txttotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txttotalAmount.Location = New System.Drawing.Point(91, 3)
        Me.txttotalAmount.MaximumSize = New System.Drawing.Size(400, 21)
        Me.txttotalAmount.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txttotalAmount.Name = "txttotalAmount"
        Me.txttotalAmount.ReadOnly = True
        Me.txttotalAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txttotalAmount.Size = New System.Drawing.Size(379, 21)
        Me.txttotalAmount.TabIndex = 0
        Me.txttotalAmount.TabStop = False
        Me.txttotalAmount.Tag = Nothing
        Me.txttotalAmount.TextDetached = True
        Me.txttotalAmount.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txttotalAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblRemark
        '
        Me.lblRemark.AttachedTextBoxName = Nothing
        Me.lblRemark.AutoSize = True
        Me.lblRemark.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblRemark.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRemark.ForeColor = System.Drawing.Color.Black
        Me.lblRemark.Location = New System.Drawing.Point(3, 55)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(46, 15)
        Me.lblRemark.TabIndex = 5
        Me.lblRemark.Tag = Nothing
        Me.lblRemark.Text = "Remark"
        Me.lblRemark.TextDetached = True
        Me.lblRemark.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCollectAmount
        '
        Me.lblCollectAmount.AttachedTextBoxName = Nothing
        Me.lblCollectAmount.AutoSize = True
        Me.lblCollectAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCollectAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCollectAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCollectAmount.ForeColor = System.Drawing.Color.Black
        Me.lblCollectAmount.Location = New System.Drawing.Point(3, 29)
        Me.lblCollectAmount.Name = "lblCollectAmount"
        Me.lblCollectAmount.Size = New System.Drawing.Size(80, 15)
        Me.lblCollectAmount.TabIndex = 4
        Me.lblCollectAmount.Tag = Nothing
        Me.lblCollectAmount.Text = "Collect Amount"
        Me.lblCollectAmount.TextDetached = True
        Me.lblCollectAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lbltotalAmount
        '
        Me.lbltotalAmount.AttachedTextBoxName = Nothing
        Me.lbltotalAmount.AutoSize = True
        Me.lbltotalAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lbltotalAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lbltotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbltotalAmount.ForeColor = System.Drawing.Color.Black
        Me.lbltotalAmount.Location = New System.Drawing.Point(3, 3)
        Me.lbltotalAmount.Name = "lbltotalAmount"
        Me.lbltotalAmount.Size = New System.Drawing.Size(72, 15)
        Me.lbltotalAmount.TabIndex = 3
        Me.lbltotalAmount.Tag = Nothing
        Me.lbltotalAmount.Text = "Total Amount"
        Me.lbltotalAmount.TextDetached = True
        Me.lbltotalAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmPaymentCreditSales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(499, 267)
        Me.Controls.Add(Me.sizTop)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPaymentCreditSales"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Payment Credit Sales"
        CType(Me.sizTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sizTop.ResumeLayout(False)
        Me.sizTop.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.txtRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCollectAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCollectAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbltotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sizTop As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents lblRemark As Spectrum.CtrlLabel
    Friend WithEvents lblCollectAmount As Spectrum.CtrlLabel
    Friend WithEvents lbltotalAmount As Spectrum.CtrlLabel
    Friend WithEvents txtRemark As Spectrum.CtrlTextBox
    Friend WithEvents txtCollectAmount As Spectrum.CtrlTextBox
    Friend WithEvents txttotalAmount As Spectrum.CtrlTextBox
    Friend WithEvents CtrlCreditCard1 As Spectrum.ctrlCreditCard
    Friend WithEvents CtrlChequeDetails1 As Spectrum.ctrlChequeDetails
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnOk As Spectrum.CtrlBtn
    Friend WithEvents btnExit As Spectrum.CtrlBtn
End Class
