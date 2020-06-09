<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrlCreditCheck
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctrlCreditCheck))
        Me.lblRemarks = New Spectrum.CtrlLabel()
        Me.txtRemarks = New Spectrum.CtrlTextBox()
        Me.dtpDueDate = New Spectrum.ctrlDate()
        Me.lblDueDate = New Spectrum.CtrlLabel()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblRemarks
        '
        Me.lblRemarks.AttachedTextBoxName = Nothing
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblRemarks.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblRemarks.Location = New System.Drawing.Point(8, 31)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(49, 13)
        Me.lblRemarks.TabIndex = 3
        Me.lblRemarks.Tag = Nothing
        Me.lblRemarks.Text = "Remarks"
        Me.lblRemarks.TextDetached = True
        '
        'txtRemarks
        '
        Me.txtRemarks.AutoSize = False
        Me.txtRemarks.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.DateTimeInput = False
        Me.txtRemarks.EmptyAsNull = True
        Me.txtRemarks.ErrorInfo.ErrorMessage = "only numeric data required"
        Me.txtRemarks.Location = New System.Drawing.Point(126, 31)
        Me.txtRemarks.MaximumSize = New System.Drawing.Size(500, 45)
        Me.txtRemarks.MaxLength = 500
        Me.txtRemarks.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(152, 45)
        Me.txtRemarks.TabIndex = 1
        Me.txtRemarks.Tag = Nothing
        Me.txtRemarks.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtRemarks.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'dtpDueDate
        '
        Me.dtpDueDate.AcceptsTab = True
        Me.dtpDueDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.dtpDueDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtpDueDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage"), System.Drawing.Image)
        '
        '
        '
        Me.dtpDueDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtpDueDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtpDueDate.DisplayFormat.CustomFormat = "dd"
        Me.dtpDueDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpDueDate.DisplayFormat.Inherit = CType((((C1.Win.C1Input.FormatInfoInheritFlags.NullText Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpDueDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpDueDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpDueDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpDueDate.InterceptArrowKeys = False
        Me.dtpDueDate.Location = New System.Drawing.Point(126, 6)
        Me.dtpDueDate.Name = "dtpDueDate"
        Me.dtpDueDate.Size = New System.Drawing.Size(97, 18)
        Me.dtpDueDate.TabIndex = 0
        Me.dtpDueDate.Tag = Nothing
        Me.dtpDueDate.TrimStart = True
        Me.dtpDueDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        Me.dtpDueDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtpDueDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'lblDueDate
        '
        Me.lblDueDate.AttachedTextBoxName = Nothing
        Me.lblDueDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDueDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDueDate.Location = New System.Drawing.Point(8, 6)
        Me.lblDueDate.Name = "lblDueDate"
        Me.lblDueDate.Size = New System.Drawing.Size(91, 17)
        Me.lblDueDate.TabIndex = 2
        Me.lblDueDate.Tag = Nothing
        Me.lblDueDate.Text = "Due Date"
        Me.lblDueDate.TextDetached = True
        '
        'ctrlCreditCheck
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Controls.Add(Me.lblRemarks)
        Me.Controls.Add(Me.txtRemarks)
        Me.Controls.Add(Me.dtpDueDate)
        Me.Controls.Add(Me.lblDueDate)
        Me.Name = "ctrlCreditCheck"
        Me.Size = New System.Drawing.Size(282, 79)
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDueDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDueDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblRemarks As Spectrum.CtrlLabel
    Friend WithEvents txtRemarks As Spectrum.CtrlTextBox
    Friend WithEvents dtpDueDate As Spectrum.ctrlDate
    Friend WithEvents lblDueDate As Spectrum.CtrlLabel

End Class
