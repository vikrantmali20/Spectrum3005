<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrlCheque
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctrlCheque))
        Me.ctrlGiftVouc = New Spectrum.CtrlLabel()
        Me.cmbVoucherProgram = New Spectrum.ctrlCombo()
        Me.dtpExpiryDate = New Spectrum.ctrlDate()
        Me.lblExpiryDate = New Spectrum.CtrlLabel()
        Me.lblChequeNo = New Spectrum.CtrlLabel()
        Me.txtChequeNo = New Spectrum.CtrlTextBox()
        CType(Me.ctrlGiftVouc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbVoucherProgram, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpExpiryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExpiryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChequeNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChequeNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ctrlGiftVouc
        '
        Me.ctrlGiftVouc.AttachedTextBoxName = Nothing
        Me.ctrlGiftVouc.AutoSize = True
        Me.ctrlGiftVouc.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ctrlGiftVouc.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ctrlGiftVouc.Location = New System.Drawing.Point(1, 48)
        Me.ctrlGiftVouc.Name = "ctrlGiftVouc"
        Me.ctrlGiftVouc.Size = New System.Drawing.Size(108, 13)
        Me.ctrlGiftVouc.TabIndex = 5
        Me.ctrlGiftVouc.Tag = Nothing
        Me.ctrlGiftVouc.Text = "Gift Voucher Program"
        Me.ctrlGiftVouc.TextDetached = True
        '
        'cmbVoucherProgram
        '
        Me.cmbVoucherProgram.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbVoucherProgram.AutoCompletion = True
        Me.cmbVoucherProgram.AutoDropDown = True
        Me.cmbVoucherProgram.Caption = ""
        Me.cmbVoucherProgram.CaptionHeight = 17
        Me.cmbVoucherProgram.CaptionVisible = False
        Me.cmbVoucherProgram.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbVoucherProgram.ColumnCaptionHeight = 17
        Me.cmbVoucherProgram.ColumnFooterHeight = 17
        Me.cmbVoucherProgram.ColumnHeaders = False
        Me.cmbVoucherProgram.ContentHeight = 15
        Me.cmbVoucherProgram.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbVoucherProgram.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cmbVoucherProgram.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbVoucherProgram.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbVoucherProgram.EditorHeight = 15
        Me.cmbVoucherProgram.Images.Add(CType(resources.GetObject("cmbVoucherProgram.Images"), System.Drawing.Image))
        Me.cmbVoucherProgram.ItemHeight = 15
        Me.cmbVoucherProgram.Location = New System.Drawing.Point(114, 45)
        Me.cmbVoucherProgram.Margin = New System.Windows.Forms.Padding(0)
        Me.cmbVoucherProgram.MatchEntryTimeout = CType(2000, Long)
        Me.cmbVoucherProgram.MaxDropDownItems = CType(5, Short)
        Me.cmbVoucherProgram.MaxLength = 32767
        Me.cmbVoucherProgram.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbVoucherProgram.Name = "cmbVoucherProgram"
        Me.cmbVoucherProgram.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbVoucherProgram.Size = New System.Drawing.Size(112, 21)
        Me.cmbVoucherProgram.TabIndex = 2
        Me.cmbVoucherProgram.Visible = False
        Me.cmbVoucherProgram.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbVoucherProgram.PropBag = resources.GetString("cmbVoucherProgram.PropBag")
        '
        'dtpExpiryDate
        '
        Me.dtpExpiryDate.AcceptsTab = True
        Me.dtpExpiryDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.dtpExpiryDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtpExpiryDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage"), System.Drawing.Image)
        '
        '
        '
        Me.dtpExpiryDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtpExpiryDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtpExpiryDate.DisplayFormat.CustomFormat = "dd"
        Me.dtpExpiryDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpExpiryDate.DisplayFormat.Inherit = CType((((C1.Win.C1Input.FormatInfoInheritFlags.NullText Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpExpiryDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpExpiryDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpExpiryDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpExpiryDate.InterceptArrowKeys = False
        Me.dtpExpiryDate.Location = New System.Drawing.Point(115, 26)
        Me.dtpExpiryDate.Name = "dtpExpiryDate"
        Me.dtpExpiryDate.Size = New System.Drawing.Size(111, 18)
        Me.dtpExpiryDate.TabIndex = 1
        Me.dtpExpiryDate.Tag = Nothing
        Me.dtpExpiryDate.TrimStart = True
        Me.dtpExpiryDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        Me.dtpExpiryDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtpExpiryDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'lblExpiryDate
        '
        Me.lblExpiryDate.AttachedTextBoxName = Nothing
        Me.lblExpiryDate.AutoSize = True
        Me.lblExpiryDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblExpiryDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblExpiryDate.Location = New System.Drawing.Point(1, 28)
        Me.lblExpiryDate.Name = "lblExpiryDate"
        Me.lblExpiryDate.Size = New System.Drawing.Size(61, 13)
        Me.lblExpiryDate.TabIndex = 4
        Me.lblExpiryDate.Tag = Nothing
        Me.lblExpiryDate.Text = "Expiry Date"
        Me.lblExpiryDate.TextDetached = True
        '
        'lblChequeNo
        '
        Me.lblChequeNo.AttachedTextBoxName = Nothing
        Me.lblChequeNo.AutoSize = True
        Me.lblChequeNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblChequeNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblChequeNo.Location = New System.Drawing.Point(2, 8)
        Me.lblChequeNo.Name = "lblChequeNo"
        Me.lblChequeNo.Size = New System.Drawing.Size(61, 13)
        Me.lblChequeNo.TabIndex = 3
        Me.lblChequeNo.Tag = Nothing
        Me.lblChequeNo.Text = "Cheque No"
        Me.lblChequeNo.TextDetached = True
        '
        'txtChequeNo
        '
        Me.txtChequeNo.AutoSize = False
        Me.txtChequeNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtChequeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChequeNo.DateTimeInput = False
        Me.txtChequeNo.EmptyAsNull = True
        Me.txtChequeNo.ErrorInfo.ErrorMessage = "only numeric data required"
        Me.txtChequeNo.Location = New System.Drawing.Point(114, 4)
        Me.txtChequeNo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtChequeNo.MaximumSize = New System.Drawing.Size(200, 21)
        Me.txtChequeNo.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.Size = New System.Drawing.Size(112, 21)
        Me.txtChequeNo.TabIndex = 0
        Me.txtChequeNo.Tag = Nothing
        Me.txtChequeNo.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtChequeNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'ctrlCheque
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Controls.Add(Me.ctrlGiftVouc)
        Me.Controls.Add(Me.cmbVoucherProgram)
        Me.Controls.Add(Me.dtpExpiryDate)
        Me.Controls.Add(Me.lblExpiryDate)
        Me.Controls.Add(Me.lblChequeNo)
        Me.Controls.Add(Me.txtChequeNo)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "ctrlCheque"
        Me.Size = New System.Drawing.Size(229, 66)
        CType(Me.ctrlGiftVouc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbVoucherProgram, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpExpiryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExpiryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChequeNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChequeNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtpExpiryDate As Spectrum.ctrlDate
    Friend WithEvents lblExpiryDate As Spectrum.CtrlLabel
    Friend WithEvents lblChequeNo As Spectrum.CtrlLabel
    Friend WithEvents txtChequeNo As Spectrum.CtrlTextBox
    Friend WithEvents cmbVoucherProgram As Spectrum.ctrlCombo
    Friend WithEvents ctrlGiftVouc As Spectrum.CtrlLabel

End Class
