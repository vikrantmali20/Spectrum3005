<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrlCreditCard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctrlCreditCard))
        Me.dtpExpiryDate = New Spectrum.ctrlDate()
        Me.lblExpiryDate = New Spectrum.CtrlLabel()
        Me.lblCreditCardNo = New Spectrum.CtrlLabel()
        Me.txtCreditCardNo = New Spectrum.CtrlTextBox()
        Me.lblBankName = New Spectrum.CtrlLabel()
        Me.cmbBankName = New Spectrum.ctrlCombo()
        CType(Me.dtpExpiryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblExpiryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreditCardNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCreditCardNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtpExpiryDate
        '
        Me.dtpExpiryDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.dtpExpiryDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtpExpiryDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage"), System.Drawing.Image)
        '
        '
        '
        Me.dtpExpiryDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtpExpiryDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtpExpiryDate.DisplayFormat.CustomFormat = "MM-yyyy"
        Me.dtpExpiryDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.dtpExpiryDate.DisplayFormat.Inherit = CType((((C1.Win.C1Input.FormatInfoInheritFlags.NullText Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpExpiryDate.EditFormat.CustomFormat = "MM-yyyy"
        Me.dtpExpiryDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.dtpExpiryDate.EditFormat.Inherit = CType((((C1.Win.C1Input.FormatInfoInheritFlags.NullText Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpExpiryDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.dtpExpiryDate.InterceptArrowKeys = False
        Me.dtpExpiryDate.Location = New System.Drawing.Point(97, 48)
        Me.dtpExpiryDate.Name = "dtpExpiryDate"
        Me.dtpExpiryDate.Size = New System.Drawing.Size(115, 18)
        Me.dtpExpiryDate.TabIndex = 3
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
        Me.lblExpiryDate.Location = New System.Drawing.Point(4, 49)
        Me.lblExpiryDate.Name = "lblExpiryDate"
        Me.lblExpiryDate.Size = New System.Drawing.Size(61, 13)
        Me.lblExpiryDate.TabIndex = 8
        Me.lblExpiryDate.Tag = Nothing
        Me.lblExpiryDate.Text = "Expiry Date"
        Me.lblExpiryDate.TextDetached = True
        '
        'lblCreditCardNo
        '
        Me.lblCreditCardNo.AttachedTextBoxName = Nothing
        Me.lblCreditCardNo.AutoSize = True
        Me.lblCreditCardNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCreditCardNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCreditCardNo.Location = New System.Drawing.Point(3, 29)
        Me.lblCreditCardNo.Name = "lblCreditCardNo"
        Me.lblCreditCardNo.Size = New System.Drawing.Size(46, 13)
        Me.lblCreditCardNo.TabIndex = 6
        Me.lblCreditCardNo.Tag = Nothing
        Me.lblCreditCardNo.Text = "Card No"
        Me.lblCreditCardNo.TextDetached = True
        '
        'txtCreditCardNo
        '
        Me.txtCreditCardNo.AutoSize = False
        Me.txtCreditCardNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtCreditCardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCreditCardNo.EmptyAsNull = True
        Me.txtCreditCardNo.ErrorInfo.ShowErrorMessage = False
        Me.txtCreditCardNo.Location = New System.Drawing.Point(97, 25)
        Me.txtCreditCardNo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtCreditCardNo.MaximumSize = New System.Drawing.Size(200, 21)
        Me.txtCreditCardNo.MaxLength = 16
        Me.txtCreditCardNo.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtCreditCardNo.Name = "txtCreditCardNo"
        Me.txtCreditCardNo.Size = New System.Drawing.Size(120, 21)
        Me.txtCreditCardNo.TabIndex = 1
        Me.txtCreditCardNo.Tag = Nothing
        Me.txtCreditCardNo.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtCreditCardNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblBankName
        '
        Me.lblBankName.AttachedTextBoxName = Nothing
        Me.lblBankName.AutoSize = True
        Me.lblBankName.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblBankName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblBankName.Location = New System.Drawing.Point(3, 8)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(63, 13)
        Me.lblBankName.TabIndex = 5
        Me.lblBankName.Tag = Nothing
        Me.lblBankName.Text = "Bank Name"
        Me.lblBankName.TextDetached = True
        '
        'cmbBankName
        '
        Me.cmbBankName.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbBankName.AutoCompletion = True
        Me.cmbBankName.AutoDropDown = True
        Me.cmbBankName.Caption = ""
        Me.cmbBankName.CaptionHeight = 17
        Me.cmbBankName.CaptionVisible = False
        Me.cmbBankName.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbBankName.ColumnCaptionHeight = 17
        Me.cmbBankName.ColumnFooterHeight = 17
        Me.cmbBankName.ColumnHeaders = False
        Me.cmbBankName.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList
        Me.cmbBankName.ContentHeight = 15
        Me.cmbBankName.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbBankName.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cmbBankName.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBankName.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbBankName.EditorHeight = 15
        Me.cmbBankName.ExtendRightColumn = True
        Me.cmbBankName.Images.Add(CType(resources.GetObject("cmbBankName.Images"), System.Drawing.Image))
        Me.cmbBankName.ItemHeight = 15
        Me.cmbBankName.Location = New System.Drawing.Point(97, 3)
        Me.cmbBankName.MatchEntryTimeout = CType(2000, Long)
        Me.cmbBankName.MaxDropDownItems = CType(5, Short)
        Me.cmbBankName.MaxLength = 32767
        Me.cmbBankName.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbBankName.Name = "cmbBankName"
        Me.cmbBankName.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbBankName.Size = New System.Drawing.Size(121, 21)
        Me.cmbBankName.TabIndex = 0
        Me.cmbBankName.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbBankName.PropBag = resources.GetString("cmbBankName.PropBag")
        '
        'ctrlCreditCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Controls.Add(Me.cmbBankName)
        Me.Controls.Add(Me.lblBankName)
        Me.Controls.Add(Me.dtpExpiryDate)
        Me.Controls.Add(Me.lblExpiryDate)
        Me.Controls.Add(Me.lblCreditCardNo)
        Me.Controls.Add(Me.txtCreditCardNo)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "ctrlCreditCard"
        Me.Size = New System.Drawing.Size(221, 69)
        CType(Me.dtpExpiryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblExpiryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreditCardNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCreditCardNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBankName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtCreditCardNo As Spectrum.CtrlTextBox
    Friend WithEvents lblCreditCardNo As Spectrum.CtrlLabel
    Friend WithEvents lblExpiryDate As Spectrum.CtrlLabel
    Friend WithEvents dtpExpiryDate As Spectrum.ctrlDate
    Friend WithEvents lblBankName As Spectrum.CtrlLabel
    Friend WithEvents cmbBankName As Spectrum.ctrlCombo

End Class
