<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCFormNumber
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCFormNumber))
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.txtCformNumber = New Spectrum.CtrlTextBox()
        Me.btnOk = New Spectrum.CtrlBtn()
        Me.lblCformReceivedDate = New System.Windows.Forms.Label()
        Me.dtpReceivedDate = New Spectrum.ctrlDate()
        CType(Me.txtCformNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpReceivedDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.Location = New System.Drawing.Point(106, 25)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(177, 20)
        Me.lblFromDate.TabIndex = 1
        Me.lblFromDate.Text = "Enter C-form number"
        '
        'txtCformNumber
        '
        Me.txtCformNumber.AutoSize = False
        Me.txtCformNumber.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtCformNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCformNumber.Location = New System.Drawing.Point(110, 61)
        Me.txtCformNumber.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtCformNumber.Name = "txtCformNumber"
        Me.txtCformNumber.Size = New System.Drawing.Size(173, 21)
        Me.txtCformNumber.TabIndex = 2
        Me.txtCformNumber.Tag = Nothing
        Me.txtCformNumber.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtCformNumber.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnOk
        '
        Me.btnOk.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOk.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnOk.Location = New System.Drawing.Point(155, 168)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.SetArticleCode = Nothing
        Me.btnOk.SetRowIndex = 0
        Me.btnOk.Size = New System.Drawing.Size(90, 32)
        Me.btnOk.TabIndex = 3
        Me.btnOk.Text = "Ok"
        Me.btnOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOk.UseVisualStyleBackColor = True
        Me.btnOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCformReceivedDate
        '
        Me.lblCformReceivedDate.AutoSize = True
        Me.lblCformReceivedDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCformReceivedDate.Location = New System.Drawing.Point(106, 95)
        Me.lblCformReceivedDate.Name = "lblCformReceivedDate"
        Me.lblCformReceivedDate.Size = New System.Drawing.Size(186, 20)
        Me.lblCformReceivedDate.TabIndex = 6
        Me.lblCformReceivedDate.Text = "C-form Received Date"
        '
        'dtpReceivedDate
        '
        Me.dtpReceivedDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.dtpReceivedDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtpReceivedDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage"), System.Drawing.Image)
        '
        '
        '
        Me.dtpReceivedDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtpReceivedDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtpReceivedDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpReceivedDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpReceivedDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpReceivedDate.Location = New System.Drawing.Point(110, 130)
        Me.dtpReceivedDate.Name = "dtpReceivedDate"
        Me.dtpReceivedDate.Size = New System.Drawing.Size(173, 19)
        Me.dtpReceivedDate.TabIndex = 7
        Me.dtpReceivedDate.Tag = Nothing
        Me.dtpReceivedDate.TrimStart = True
        Me.dtpReceivedDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtpReceivedDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'frmCFormNumber
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(383, 218)
        Me.Controls.Add(Me.dtpReceivedDate)
        Me.Controls.Add(Me.lblCformReceivedDate)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.txtCformNumber)
        Me.Controls.Add(Me.lblFromDate)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCFormNumber"
        CType(Me.txtCformNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpReceivedDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents txtCformNumber As Spectrum.CtrlTextBox
    Friend WithEvents btnOk As Spectrum.CtrlBtn
    Friend WithEvents lblCformReceivedDate As System.Windows.Forms.Label
    Friend WithEvents dtpReceivedDate As Spectrum.ctrlDate
End Class
