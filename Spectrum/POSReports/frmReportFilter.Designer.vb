<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportFilter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReportFilter))
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.lblExpryDate = New System.Windows.Forms.Label()
        Me.lblTimeSpan = New System.Windows.Forms.Label()
        Me.txtTimeSpan = New System.Windows.Forms.TextBox()
        Me.ComboBoxMonth = New System.Windows.Forms.ComboBox()
        Me.ComboBoxYear = New System.Windows.Forms.ComboBox()
        Me.pnlPromotions = New System.Windows.Forms.Panel()
        Me.chkListPromotions = New System.Windows.Forms.CheckedListBox()
        Me.pnlCustomerClass = New System.Windows.Forms.Panel()
        Me.ComboBoxClass = New System.Windows.Forms.ComboBox()
        Me.lblSelectClass = New Spectrum.Controls.Label(Me.components)
        Me.lblPrormotions = New Spectrum.Controls.Label(Me.components)
        Me.lblepartner = New Spectrum.Controls.Label(Me.components)
        Me.dtExpiryDate = New Spectrum.ctrlDate()
        Me.dtFromDate = New Spectrum.ctrlDate()
        Me.dtToDate = New Spectrum.ctrlDate()
        Me.pnlPromotions.SuspendLayout()
        Me.pnlCustomerClass.SuspendLayout()
        CType(Me.lblSelectClass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPrormotions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblepartner, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtExpiryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Location = New System.Drawing.Point(12, 74)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(46, 13)
        Me.lblToDate.TabIndex = 0
        Me.lblToDate.Text = "To Date"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(61, 218)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Generate"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(142, 218)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Location = New System.Drawing.Point(12, 44)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(56, 13)
        Me.lblFromDate.TabIndex = 4
        Me.lblFromDate.Text = "From Date"
        Me.lblFromDate.Visible = False
        '
        'lblExpryDate
        '
        Me.lblExpryDate.AutoSize = True
        Me.lblExpryDate.Location = New System.Drawing.Point(12, 104)
        Me.lblExpryDate.Name = "lblExpryDate"
        Me.lblExpryDate.Size = New System.Drawing.Size(61, 13)
        Me.lblExpryDate.TabIndex = 6
        Me.lblExpryDate.Text = "Expiry Date"
        Me.lblExpryDate.Visible = False
        '
        'lblTimeSpan
        '
        Me.lblTimeSpan.AutoSize = True
        Me.lblTimeSpan.Location = New System.Drawing.Point(12, 137)
        Me.lblTimeSpan.Name = "lblTimeSpan"
        Me.lblTimeSpan.Size = New System.Drawing.Size(58, 13)
        Me.lblTimeSpan.TabIndex = 8
        Me.lblTimeSpan.Text = "Time Span"
        Me.lblTimeSpan.Visible = False
        '
        'txtTimeSpan
        '
        Me.txtTimeSpan.Location = New System.Drawing.Point(78, 134)
        Me.txtTimeSpan.Name = "txtTimeSpan"
        Me.txtTimeSpan.Size = New System.Drawing.Size(40, 20)
        Me.txtTimeSpan.TabIndex = 9
        Me.txtTimeSpan.Visible = False
        '
        'ComboBoxMonth
        '
        Me.ComboBoxMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxMonth.FormattingEnabled = True
        Me.ComboBoxMonth.Location = New System.Drawing.Point(78, 44)
        Me.ComboBoxMonth.Name = "ComboBoxMonth"
        Me.ComboBoxMonth.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxMonth.TabIndex = 10
        '
        'ComboBoxYear
        '
        Me.ComboBoxYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxYear.FormattingEnabled = True
        Me.ComboBoxYear.Location = New System.Drawing.Point(77, 71)
        Me.ComboBoxYear.Name = "ComboBoxYear"
        Me.ComboBoxYear.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxYear.TabIndex = 11
        '
        'pnlPromotions
        '
        Me.pnlPromotions.Controls.Add(Me.lblepartner)
        Me.pnlPromotions.Controls.Add(Me.lblPrormotions)
        Me.pnlPromotions.Controls.Add(Me.chkListPromotions)
        Me.pnlPromotions.Location = New System.Drawing.Point(12, 98)
        Me.pnlPromotions.Name = "pnlPromotions"
        Me.pnlPromotions.Size = New System.Drawing.Size(253, 104)
        Me.pnlPromotions.TabIndex = 12
        '
        'chkListPromotions
        '
        Me.chkListPromotions.FormattingEnabled = True
        Me.chkListPromotions.Location = New System.Drawing.Point(3, 24)
        Me.chkListPromotions.MinimumSize = New System.Drawing.Size(81, 4)
        Me.chkListPromotions.Name = "chkListPromotions"
        Me.chkListPromotions.Size = New System.Drawing.Size(247, 79)
        Me.chkListPromotions.TabIndex = 0
        '
        'pnlCustomerClass
        '
        Me.pnlCustomerClass.Controls.Add(Me.ComboBoxClass)
        Me.pnlCustomerClass.Controls.Add(Me.lblSelectClass)
        Me.pnlCustomerClass.Location = New System.Drawing.Point(11, 98)
        Me.pnlCustomerClass.Name = "pnlCustomerClass"
        Me.pnlCustomerClass.Size = New System.Drawing.Size(254, 32)
        Me.pnlCustomerClass.TabIndex = 13
        '
        'ComboBoxClass
        '
        Me.ComboBoxClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxClass.FormattingEnabled = True
        Me.ComboBoxClass.Location = New System.Drawing.Point(67, 4)
        Me.ComboBoxClass.Name = "ComboBoxClass"
        Me.ComboBoxClass.Size = New System.Drawing.Size(175, 21)
        Me.ComboBoxClass.TabIndex = 1
        '
        'lblSelectClass
        '
        Me.lblSelectClass.AutoSize = True
        Me.lblSelectClass.BackColor = System.Drawing.Color.Transparent
        Me.lblSelectClass.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblSelectClass.ForeColor = System.Drawing.Color.Black
        Me.lblSelectClass.Location = New System.Drawing.Point(0, 4)
        Me.lblSelectClass.Name = "lblSelectClass"
        Me.lblSelectClass.Size = New System.Drawing.Size(65, 13)
        Me.lblSelectClass.TabIndex = 0
        Me.lblSelectClass.Tag = Nothing
        Me.lblSelectClass.Text = "Select Class"
        Me.lblSelectClass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSelectClass.TextDetached = True
        Me.lblSelectClass.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'lblPrormotions
        '
        Me.lblPrormotions.AutoSize = True
        Me.lblPrormotions.BackColor = System.Drawing.Color.Transparent
        Me.lblPrormotions.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblPrormotions.ForeColor = System.Drawing.Color.Black
        Me.lblPrormotions.Location = New System.Drawing.Point(3, 6)
        Me.lblPrormotions.Name = "lblPrormotions"
        Me.lblPrormotions.Size = New System.Drawing.Size(86, 16)
        Me.lblPrormotions.TabIndex = 1
        Me.lblPrormotions.Tag = Nothing
        Me.lblPrormotions.Text = "Promotions:"
        Me.lblPrormotions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPrormotions.TextDetached = True
        Me.lblPrormotions.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'lblepartner
        '
        Me.lblepartner.AutoSize = True
        Me.lblepartner.BackColor = System.Drawing.Color.Transparent
        Me.lblepartner.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblepartner.ForeColor = System.Drawing.Color.Black
        Me.lblepartner.Location = New System.Drawing.Point(3, 4)
        Me.lblepartner.Name = "lblepartner"
        Me.lblepartner.Size = New System.Drawing.Size(62, 16)
        Me.lblepartner.TabIndex = 2
        Me.lblepartner.Tag = Nothing
        Me.lblepartner.Text = "Partner:"
        Me.lblepartner.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblepartner.TextDetached = True
        Me.lblepartner.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'dtExpiryDate
        '
        Me.dtExpiryDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.dtExpiryDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtExpiryDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage"), System.Drawing.Image)
        '
        '
        '
        Me.dtExpiryDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtExpiryDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtExpiryDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtExpiryDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtExpiryDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtExpiryDate.Location = New System.Drawing.Point(78, 102)
        Me.dtExpiryDate.Name = "dtExpiryDate"
        Me.dtExpiryDate.Size = New System.Drawing.Size(187, 18)
        Me.dtExpiryDate.TabIndex = 7
        Me.dtExpiryDate.Tag = Nothing
        Me.dtExpiryDate.TrimStart = True
        Me.dtExpiryDate.Visible = False
        Me.dtExpiryDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtExpiryDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'dtFromDate
        '
        Me.dtFromDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.dtFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtFromDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage1"), System.Drawing.Image)
        '
        '
        '
        Me.dtFromDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtFromDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtFromDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtFromDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtFromDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtFromDate.Location = New System.Drawing.Point(77, 43)
        Me.dtFromDate.Name = "dtFromDate"
        Me.dtFromDate.Size = New System.Drawing.Size(188, 18)
        Me.dtFromDate.TabIndex = 5
        Me.dtFromDate.Tag = Nothing
        Me.dtFromDate.TrimStart = True
        Me.dtFromDate.Visible = False
        Me.dtFromDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtFromDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'dtToDate
        '
        Me.dtToDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.dtToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtToDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage2"), System.Drawing.Image)
        '
        '
        '
        Me.dtToDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtToDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtToDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtToDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtToDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtToDate.Location = New System.Drawing.Point(78, 72)
        Me.dtToDate.Name = "dtToDate"
        Me.dtToDate.Size = New System.Drawing.Size(187, 18)
        Me.dtToDate.TabIndex = 1
        Me.dtToDate.Tag = Nothing
        Me.dtToDate.TrimStart = True
        Me.dtToDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtToDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'frmReportFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlCustomerClass)
        Me.Controls.Add(Me.pnlPromotions)
        Me.Controls.Add(Me.ComboBoxYear)
        Me.Controls.Add(Me.ComboBoxMonth)
        Me.Controls.Add(Me.txtTimeSpan)
        Me.Controls.Add(Me.lblTimeSpan)
        Me.Controls.Add(Me.dtExpiryDate)
        Me.Controls.Add(Me.lblExpryDate)
        Me.Controls.Add(Me.dtFromDate)
        Me.Controls.Add(Me.lblFromDate)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.dtToDate)
        Me.Controls.Add(Me.lblToDate)
        Me.Name = "frmReportFilter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report Filter"
        Me.pnlPromotions.ResumeLayout(False)
        Me.pnlPromotions.PerformLayout()
        Me.pnlCustomerClass.ResumeLayout(False)
        Me.pnlCustomerClass.PerformLayout()
        CType(Me.lblSelectClass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPrormotions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblepartner, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtExpiryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents dtToDate As Spectrum.ctrlDate
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents dtFromDate As Spectrum.ctrlDate
    Friend WithEvents dtExpiryDate As Spectrum.ctrlDate
    Friend WithEvents lblExpryDate As System.Windows.Forms.Label
    Friend WithEvents lblTimeSpan As System.Windows.Forms.Label
    Friend WithEvents txtTimeSpan As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxMonth As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxYear As System.Windows.Forms.ComboBox
    Friend WithEvents pnlPromotions As System.Windows.Forms.Panel
    Friend WithEvents lblPrormotions As Spectrum.Controls.Label
    Friend WithEvents chkListPromotions As System.Windows.Forms.CheckedListBox
    Friend WithEvents pnlCustomerClass As System.Windows.Forms.Panel
    Friend WithEvents ComboBoxClass As System.Windows.Forms.ComboBox
    Friend WithEvents lblSelectClass As Spectrum.Controls.Label
    Friend WithEvents lblepartner As Spectrum.Controls.Label
End Class
