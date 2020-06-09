<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScheduler
    Inherits Spectrum.CtrlRbnBaseForm

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
        Me.txtUrl = New Spectrum.CtrlTextBox()
        Me.txtDuration = New Spectrum.CtrlTextBox()
        Me.btnSave = New Spectrum.CtrlBtn()
        Me.lblInterval = New Spectrum.CtrlLabel()
        Me.lblUrl = New Spectrum.CtrlLabel()
        Me.lblNextRunTime = New Spectrum.CtrlLabel()
        Me.lblLastRunTime = New Spectrum.CtrlLabel()
        Me.lblDispLRT = New Spectrum.CtrlLabel()
        Me.lblDispNRT = New Spectrum.CtrlLabel()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.btnBrowse = New Spectrum.CtrlBtn()
        CType(Me.C1StatusBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUrl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDuration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUrl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNextRunTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLastRunTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDispLRT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDispNRT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblLoggedIn
        '
        Me.lblLoggedIn.Text = Nothing
        '
        'txtUrl
        '
        Me.txtUrl.AutoSize = False
        Me.txtUrl.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUrl.DataType = GetType(Long)
        Me.txtUrl.Location = New System.Drawing.Point(191, 134)
        Me.txtUrl.MaximumSize = New System.Drawing.Size(500, 21)
        Me.txtUrl.MaxLength = 15
        Me.txtUrl.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtUrl.Name = "txtUrl"
        Me.txtUrl.Size = New System.Drawing.Size(361, 21)
        Me.txtUrl.TabIndex = 6
        Me.txtUrl.Tag = Nothing
        Me.txtUrl.TextDetached = True
        Me.txtUrl.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtUrl.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtDuration
        '
        Me.txtDuration.AutoSize = False
        Me.txtDuration.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDuration.DataType = GetType(Integer)
        Me.txtDuration.Location = New System.Drawing.Point(191, 174)
        Me.txtDuration.MaximumSize = New System.Drawing.Size(200, 21)
        Me.txtDuration.MaxLength = 15
        Me.txtDuration.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtDuration.Name = "txtDuration"
        Me.txtDuration.Size = New System.Drawing.Size(89, 21)
        Me.txtDuration.TabIndex = 61
        Me.txtDuration.Tag = Nothing
        Me.txtDuration.TextDetached = True
        Me.txtDuration.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtDuration.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnSave
        '
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSave.Location = New System.Drawing.Point(474, 275)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.SetArticleCode = Nothing
        Me.btnSave.SetRowIndex = 0
        Me.btnSave.Size = New System.Drawing.Size(78, 23)
        Me.btnSave.TabIndex = 63
        Me.btnSave.Text = "Save"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSave.UseVisualStyleBackColor = True
        Me.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblInterval
        '
        Me.lblInterval.AttachedTextBoxName = Nothing
        Me.lblInterval.AutoSize = True
        Me.lblInterval.BackColor = System.Drawing.Color.Transparent
        Me.lblInterval.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblInterval.ForeColor = System.Drawing.Color.Black
        Me.lblInterval.Location = New System.Drawing.Point(93, 174)
        Me.lblInterval.Name = "lblInterval"
        Me.lblInterval.Size = New System.Drawing.Size(48, 13)
        Me.lblInterval.TabIndex = 66
        Me.lblInterval.Tag = Nothing
        Me.lblInterval.Text = "Interval :"
        Me.lblInterval.TextDetached = True
        Me.lblInterval.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblUrl
        '
        Me.lblUrl.AttachedTextBoxName = Nothing
        Me.lblUrl.AutoSize = True
        Me.lblUrl.BackColor = System.Drawing.Color.Transparent
        Me.lblUrl.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblUrl.ForeColor = System.Drawing.Color.Black
        Me.lblUrl.Location = New System.Drawing.Point(93, 134)
        Me.lblUrl.Name = "lblUrl"
        Me.lblUrl.Size = New System.Drawing.Size(35, 13)
        Me.lblUrl.TabIndex = 67
        Me.lblUrl.Tag = Nothing
        Me.lblUrl.Text = "Path :"
        Me.lblUrl.TextDetached = True
        Me.lblUrl.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblNextRunTime
        '
        Me.lblNextRunTime.AttachedTextBoxName = Nothing
        Me.lblNextRunTime.AutoSize = True
        Me.lblNextRunTime.BackColor = System.Drawing.Color.Transparent
        Me.lblNextRunTime.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblNextRunTime.ForeColor = System.Drawing.Color.Black
        Me.lblNextRunTime.Location = New System.Drawing.Point(93, 88)
        Me.lblNextRunTime.Name = "lblNextRunTime"
        Me.lblNextRunTime.Size = New System.Drawing.Size(84, 13)
        Me.lblNextRunTime.TabIndex = 68
        Me.lblNextRunTime.Tag = Nothing
        Me.lblNextRunTime.Text = "Next Run Time :"
        Me.lblNextRunTime.TextDetached = True
        Me.lblNextRunTime.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblLastRunTime
        '
        Me.lblLastRunTime.AttachedTextBoxName = Nothing
        Me.lblLastRunTime.AutoSize = True
        Me.lblLastRunTime.BackColor = System.Drawing.Color.Transparent
        Me.lblLastRunTime.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblLastRunTime.ForeColor = System.Drawing.Color.Black
        Me.lblLastRunTime.Location = New System.Drawing.Point(93, 43)
        Me.lblLastRunTime.Name = "lblLastRunTime"
        Me.lblLastRunTime.Size = New System.Drawing.Size(85, 13)
        Me.lblLastRunTime.TabIndex = 69
        Me.lblLastRunTime.Tag = Nothing
        Me.lblLastRunTime.Text = "Last Run Time : "
        Me.lblLastRunTime.TextDetached = True
        Me.lblLastRunTime.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblDispLRT
        '
        Me.lblDispLRT.AttachedTextBoxName = Nothing
        Me.lblDispLRT.AutoSize = True
        Me.lblDispLRT.BackColor = System.Drawing.Color.Transparent
        Me.lblDispLRT.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDispLRT.ForeColor = System.Drawing.Color.Black
        Me.lblDispLRT.Location = New System.Drawing.Point(191, 43)
        Me.lblDispLRT.Name = "lblDispLRT"
        Me.lblDispLRT.Size = New System.Drawing.Size(0, 13)
        Me.lblDispLRT.TabIndex = 70
        Me.lblDispLRT.Tag = Nothing
        Me.lblDispLRT.TextDetached = True
        Me.lblDispLRT.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblDispNRT
        '
        Me.lblDispNRT.AttachedTextBoxName = Nothing
        Me.lblDispNRT.AutoSize = True
        Me.lblDispNRT.BackColor = System.Drawing.Color.Transparent
        Me.lblDispNRT.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDispNRT.ForeColor = System.Drawing.Color.Black
        Me.lblDispNRT.Location = New System.Drawing.Point(191, 88)
        Me.lblDispNRT.Name = "lblDispNRT"
        Me.lblDispNRT.Size = New System.Drawing.Size(0, 13)
        Me.lblDispNRT.TabIndex = 71
        Me.lblDispNRT.Tag = Nothing
        Me.lblDispNRT.TextDetached = True
        Me.lblDispNRT.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btnBrowse
        '
        Me.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnBrowse.Location = New System.Drawing.Point(570, 134)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.SetArticleCode = Nothing
        Me.btnBrowse.SetRowIndex = 0
        Me.btnBrowse.Size = New System.Drawing.Size(78, 21)
        Me.btnBrowse.TabIndex = 72
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnBrowse.UseVisualStyleBackColor = True
        Me.btnBrowse.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmScheduler
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(692, 418)
        Me.ControlBox = True
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.lblDispNRT)
        Me.Controls.Add(Me.lblDispLRT)
        Me.Controls.Add(Me.lblLastRunTime)
        Me.Controls.Add(Me.lblNextRunTime)
        Me.Controls.Add(Me.lblUrl)
        Me.Controls.Add(Me.lblInterval)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtDuration)
        Me.Controls.Add(Me.txtUrl)
        Me.Name = "frmScheduler"
        Me.Text = "Scheduler"
        Me.Controls.SetChildIndex(Me.C1StatusBar1, 0)
        Me.Controls.SetChildIndex(Me.txtUrl, 0)
        Me.Controls.SetChildIndex(Me.txtDuration, 0)
        Me.Controls.SetChildIndex(Me.btnSave, 0)
        Me.Controls.SetChildIndex(Me.lblInterval, 0)
        Me.Controls.SetChildIndex(Me.lblUrl, 0)
        Me.Controls.SetChildIndex(Me.lblNextRunTime, 0)
        Me.Controls.SetChildIndex(Me.lblLastRunTime, 0)
        Me.Controls.SetChildIndex(Me.lblDispLRT, 0)
        Me.Controls.SetChildIndex(Me.lblDispNRT, 0)
        Me.Controls.SetChildIndex(Me.btnBrowse, 0)
        CType(Me.C1StatusBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUrl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDuration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInterval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUrl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNextRunTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLastRunTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDispLRT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDispNRT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtUrl As Spectrum.CtrlTextBox
    Friend WithEvents txtDuration As Spectrum.CtrlTextBox
    Friend WithEvents btnSave As Spectrum.CtrlBtn
    Friend WithEvents lblInterval As Spectrum.CtrlLabel
    Friend WithEvents lblUrl As Spectrum.CtrlLabel
    Friend WithEvents lblNextRunTime As Spectrum.CtrlLabel
    Friend WithEvents lblLastRunTime As Spectrum.CtrlLabel
    Friend WithEvents lblDispLRT As Spectrum.CtrlLabel
    Friend WithEvents lblDispNRT As Spectrum.CtrlLabel
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnBrowse As Spectrum.CtrlBtn
End Class
