<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProcScheduler
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
        Me.lblLastRunTime = New Spectrum.CtrlLabel()
        Me.lblNextRunTime = New Spectrum.CtrlLabel()
        Me.lblInterval = New Spectrum.CtrlLabel()
        Me.btnSave = New Spectrum.CtrlBtn()
        Me.txtDuration = New Spectrum.CtrlTextBox()
        Me.lblDays = New Spectrum.CtrlLabel()
        Me.lblDisplayLRT = New Spectrum.CtrlLabel()
        Me.lblDisplayNRT = New Spectrum.CtrlLabel()
        CType(Me.C1StatusBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLastRunTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNextRunTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDuration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDisplayLRT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDisplayNRT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblLoggedIn
        '
        Me.lblLoggedIn.Text = Nothing
        '
        'lblLastRunTime
        '
        Me.lblLastRunTime.AttachedTextBoxName = Nothing
        Me.lblLastRunTime.AutoSize = True
        Me.lblLastRunTime.BackColor = System.Drawing.Color.Transparent
        Me.lblLastRunTime.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblLastRunTime.ForeColor = System.Drawing.Color.Black
        Me.lblLastRunTime.Location = New System.Drawing.Point(115, 60)
        Me.lblLastRunTime.Name = "lblLastRunTime"
        Me.lblLastRunTime.Size = New System.Drawing.Size(85, 13)
        Me.lblLastRunTime.TabIndex = 70
        Me.lblLastRunTime.Tag = Nothing
        Me.lblLastRunTime.Text = "Last Run Time : "
        Me.lblLastRunTime.TextDetached = True
        Me.lblLastRunTime.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblNextRunTime
        '
        Me.lblNextRunTime.AttachedTextBoxName = Nothing
        Me.lblNextRunTime.AutoSize = True
        Me.lblNextRunTime.BackColor = System.Drawing.Color.Transparent
        Me.lblNextRunTime.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblNextRunTime.ForeColor = System.Drawing.Color.Black
        Me.lblNextRunTime.Location = New System.Drawing.Point(116, 102)
        Me.lblNextRunTime.Name = "lblNextRunTime"
        Me.lblNextRunTime.Size = New System.Drawing.Size(84, 13)
        Me.lblNextRunTime.TabIndex = 74
        Me.lblNextRunTime.Tag = Nothing
        Me.lblNextRunTime.Text = "Next Run Time :"
        Me.lblNextRunTime.TextDetached = True
        Me.lblNextRunTime.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblInterval
        '
        Me.lblInterval.AttachedTextBoxName = Nothing
        Me.lblInterval.AutoSize = True
        Me.lblInterval.BackColor = System.Drawing.Color.Transparent
        Me.lblInterval.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblInterval.ForeColor = System.Drawing.Color.Black
        Me.lblInterval.Location = New System.Drawing.Point(116, 162)
        Me.lblInterval.Name = "lblInterval"
        Me.lblInterval.Size = New System.Drawing.Size(48, 13)
        Me.lblInterval.TabIndex = 73
        Me.lblInterval.Tag = Nothing
        Me.lblInterval.Text = "Interval :"
        Me.lblInterval.TextDetached = True
        Me.lblInterval.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnSave
        '
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSave.Location = New System.Drawing.Point(417, 253)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.SetArticleCode = Nothing
        Me.btnSave.SetRowIndex = 0
        Me.btnSave.Size = New System.Drawing.Size(78, 23)
        Me.btnSave.TabIndex = 72
        Me.btnSave.Text = "Save"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSave.UseVisualStyleBackColor = True
        Me.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtDuration
        '
        Me.txtDuration.AutoSize = False
        Me.txtDuration.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDuration.DataType = GetType(Integer)
        Me.txtDuration.Location = New System.Drawing.Point(227, 162)
        Me.txtDuration.MaximumSize = New System.Drawing.Size(200, 21)
        Me.txtDuration.MaxLength = 15
        Me.txtDuration.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtDuration.Name = "txtDuration"
        Me.txtDuration.Size = New System.Drawing.Size(89, 21)
        Me.txtDuration.TabIndex = 71
        Me.txtDuration.Tag = Nothing
        Me.txtDuration.TextDetached = True
        Me.txtDuration.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtDuration.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblDays
        '
        Me.lblDays.AttachedTextBoxName = Nothing
        Me.lblDays.AutoSize = True
        Me.lblDays.BackColor = System.Drawing.Color.Transparent
        Me.lblDays.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDays.ForeColor = System.Drawing.Color.Black
        Me.lblDays.Location = New System.Drawing.Point(322, 170)
        Me.lblDays.Name = "lblDays"
        Me.lblDays.Size = New System.Drawing.Size(29, 13)
        Me.lblDays.TabIndex = 75
        Me.lblDays.Tag = Nothing
        Me.lblDays.Text = "days"
        Me.lblDays.TextDetached = True
        Me.lblDays.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblDisplayLRT
        '
        Me.lblDisplayLRT.AttachedTextBoxName = Nothing
        Me.lblDisplayLRT.AutoSize = True
        Me.lblDisplayLRT.BackColor = System.Drawing.Color.Transparent
        Me.lblDisplayLRT.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDisplayLRT.ForeColor = System.Drawing.Color.Black
        Me.lblDisplayLRT.Location = New System.Drawing.Point(227, 60)
        Me.lblDisplayLRT.Name = "lblDisplayLRT"
        Me.lblDisplayLRT.Size = New System.Drawing.Size(0, 13)
        Me.lblDisplayLRT.TabIndex = 76
        Me.lblDisplayLRT.Tag = Nothing
        Me.lblDisplayLRT.TextDetached = True
        Me.lblDisplayLRT.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblDisplayNRT
        '
        Me.lblDisplayNRT.AttachedTextBoxName = Nothing
        Me.lblDisplayNRT.AutoSize = True
        Me.lblDisplayNRT.BackColor = System.Drawing.Color.Transparent
        Me.lblDisplayNRT.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDisplayNRT.ForeColor = System.Drawing.Color.Black
        Me.lblDisplayNRT.Location = New System.Drawing.Point(227, 102)
        Me.lblDisplayNRT.Name = "lblDisplayNRT"
        Me.lblDisplayNRT.Size = New System.Drawing.Size(0, 13)
        Me.lblDisplayNRT.TabIndex = 77
        Me.lblDisplayNRT.Tag = Nothing
        Me.lblDisplayNRT.TextDetached = True
        Me.lblDisplayNRT.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmProcScheduler
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(667, 454)
        Me.ControlBox = True
        Me.Controls.Add(Me.lblDisplayNRT)
        Me.Controls.Add(Me.lblDisplayLRT)
        Me.Controls.Add(Me.txtDuration)
        Me.Controls.Add(Me.lblDays)
        Me.Controls.Add(Me.lblNextRunTime)
        Me.Controls.Add(Me.lblInterval)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.lblLastRunTime)
        Me.Name = "frmProcScheduler"
        Me.Text = "ProcScheduler"
        Me.Controls.SetChildIndex(Me.lblLastRunTime, 0)
        Me.Controls.SetChildIndex(Me.btnSave, 0)
        Me.Controls.SetChildIndex(Me.lblInterval, 0)
        Me.Controls.SetChildIndex(Me.lblNextRunTime, 0)
        Me.Controls.SetChildIndex(Me.lblDays, 0)
        Me.Controls.SetChildIndex(Me.txtDuration, 0)
        Me.Controls.SetChildIndex(Me.C1StatusBar1, 0)
        Me.Controls.SetChildIndex(Me.lblDisplayLRT, 0)
        Me.Controls.SetChildIndex(Me.lblDisplayNRT, 0)
        CType(Me.C1StatusBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLastRunTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNextRunTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInterval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDuration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDisplayLRT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDisplayNRT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblLastRunTime As Spectrum.CtrlLabel
    Friend WithEvents lblNextRunTime As Spectrum.CtrlLabel
    Friend WithEvents lblInterval As Spectrum.CtrlLabel
    Friend WithEvents btnSave As Spectrum.CtrlBtn
    Friend WithEvents txtDuration As Spectrum.CtrlTextBox
    Friend WithEvents lblDays As Spectrum.CtrlLabel
    Friend WithEvents lblDisplayLRT As Spectrum.CtrlLabel
    Friend WithEvents lblDisplayNRT As Spectrum.CtrlLabel
End Class
