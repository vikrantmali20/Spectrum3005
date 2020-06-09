<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OfflineLicenseActivator
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
        Me.btnCancel = New Spectrum.CtrlBtn()
        Me.btnNext = New Spectrum.CtrlBtn()
        Me.btnBack = New Spectrum.CtrlBtn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtLicenseKey = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancel.Location = New System.Drawing.Point(192, 111)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.SetArticleCode = Nothing
        Me.btnCancel.SetRowIndex = 0
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 9
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnNext
        '
        Me.btnNext.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnNext.Location = New System.Drawing.Point(105, 111)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.SetArticleCode = Nothing
        Me.btnNext.SetRowIndex = 0
        Me.btnNext.Size = New System.Drawing.Size(75, 23)
        Me.btnNext.TabIndex = 8
        Me.btnNext.Text = "Continue"
        Me.btnNext.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnNext.UseVisualStyleBackColor = True
        Me.btnNext.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.btnNext.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnBack
        '
        Me.btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnBack.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnBack.Location = New System.Drawing.Point(18, 111)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.SetArticleCode = Nothing
        Me.btnBack.SetRowIndex = 0
        Me.btnBack.Size = New System.Drawing.Size(75, 23)
        Me.btnBack.TabIndex = 7
        Me.btnBack.Text = "Back"
        Me.btnBack.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnBack.UseVisualStyleBackColor = True
        Me.btnBack.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.btnBack.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(206, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Enter 25 digit License key received Offline"
        '
        'txtLicenseKey
        '
        Me.txtLicenseKey.Location = New System.Drawing.Point(24, 53)
        Me.txtLicenseKey.Name = "txtLicenseKey"
        Me.txtLicenseKey.Size = New System.Drawing.Size(226, 20)
        Me.txtLicenseKey.TabIndex = 5
        '
        'OfflineLicenseActivator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 146)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtLicenseKey)
        Me.Name = "OfflineLicenseActivator"
        Me.Text = "Offline Activation"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancel As Spectrum.CtrlBtn
    Friend WithEvents btnNext As Spectrum.CtrlBtn
    Friend WithEvents btnBack As Spectrum.CtrlBtn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtLicenseKey As System.Windows.Forms.TextBox
End Class
