<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAutoCancel
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
        Me.btnCancel = New Spectrum.CtrlBtn()
        Me.btnExtend = New Spectrum.CtrlBtn()
        Me.lblCustValue = New System.Windows.Forms.Label()
        Me.lblCustName = New System.Windows.Forms.Label()
        Me.lblPhoneValue = New System.Windows.Forms.Label()
        Me.lblPhoneNo = New System.Windows.Forms.Label()
        Me.lblTableNo = New System.Windows.Forms.Label()
        Me.lblTableNoValue = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancel.Location = New System.Drawing.Point(39, 158)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.SetArticleCode = Nothing
        Me.btnCancel.SetRowIndex = 0
        Me.btnCancel.Size = New System.Drawing.Size(115, 42)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "&Cancel Reservation"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnExtend
        '
        Me.btnExtend.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExtend.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnExtend.Location = New System.Drawing.Point(184, 158)
        Me.btnExtend.Margin = New System.Windows.Forms.Padding(0)
        Me.btnExtend.Name = "btnExtend"
        Me.btnExtend.SetArticleCode = Nothing
        Me.btnExtend.SetRowIndex = 0
        Me.btnExtend.Size = New System.Drawing.Size(97, 42)
        Me.btnExtend.TabIndex = 3
        Me.btnExtend.Text = "&Extend"
        Me.btnExtend.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnExtend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExtend.UseVisualStyleBackColor = True
        Me.btnExtend.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCustValue
        '
        Me.lblCustValue.AutoSize = True
        Me.lblCustValue.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustValue.Location = New System.Drawing.Point(148, 0)
        Me.lblCustValue.Name = "lblCustValue"
        Me.lblCustValue.Size = New System.Drawing.Size(0, 18)
        Me.lblCustValue.TabIndex = 44
        '
        'lblCustName
        '
        Me.lblCustName.AutoSize = True
        Me.lblCustName.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustName.Location = New System.Drawing.Point(3, 0)
        Me.lblCustName.Name = "lblCustName"
        Me.lblCustName.Size = New System.Drawing.Size(135, 18)
        Me.lblCustName.TabIndex = 43
        Me.lblCustName.Text = "Customer Name :"
        '
        'lblPhoneValue
        '
        Me.lblPhoneValue.AutoSize = True
        Me.lblPhoneValue.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhoneValue.Location = New System.Drawing.Point(148, 33)
        Me.lblPhoneValue.Name = "lblPhoneValue"
        Me.lblPhoneValue.Size = New System.Drawing.Size(0, 18)
        Me.lblPhoneValue.TabIndex = 46
        '
        'lblPhoneNo
        '
        Me.lblPhoneNo.AutoSize = True
        Me.lblPhoneNo.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhoneNo.Location = New System.Drawing.Point(3, 33)
        Me.lblPhoneNo.Name = "lblPhoneNo"
        Me.lblPhoneNo.Size = New System.Drawing.Size(88, 18)
        Me.lblPhoneNo.TabIndex = 45
        Me.lblPhoneNo.Text = "Phone No :"
        '
        'lblTableNo
        '
        Me.lblTableNo.AutoSize = True
        Me.lblTableNo.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTableNo.Location = New System.Drawing.Point(3, 69)
        Me.lblTableNo.Name = "lblTableNo"
        Me.lblTableNo.Size = New System.Drawing.Size(83, 18)
        Me.lblTableNo.TabIndex = 47
        Me.lblTableNo.Text = "Table No :"
        '
        'lblTableNoValue
        '
        Me.lblTableNoValue.AutoSize = True
        Me.lblTableNoValue.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTableNoValue.Location = New System.Drawing.Point(148, 69)
        Me.lblTableNoValue.Name = "lblTableNoValue"
        Me.lblTableNoValue.Size = New System.Drawing.Size(0, 18)
        Me.lblTableNoValue.TabIndex = 48
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblCustName, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblCustValue, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblPhoneValue, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTableNoValue, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTableNo, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblPhoneNo, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(16, 25)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.22222!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.77778!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(290, 112)
        Me.TableLayoutPanel1.TabIndex = 49
        '
        'frmAutoCancel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(330, 217)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.btnExtend)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAutoCancel"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Auto Cancel"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As Spectrum.CtrlBtn
    Friend WithEvents btnExtend As Spectrum.CtrlBtn
    Friend WithEvents lblCustValue As System.Windows.Forms.Label
    Friend WithEvents lblCustName As System.Windows.Forms.Label
    Friend WithEvents lblPhoneValue As System.Windows.Forms.Label
    Friend WithEvents lblPhoneNo As System.Windows.Forms.Label
    Friend WithEvents lblTableNo As System.Windows.Forms.Label
    Friend WithEvents lblTableNoValue As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
End Class
