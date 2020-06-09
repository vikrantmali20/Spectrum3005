<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddTemplate
    'Inherits System.Windows.Forms.Form
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddTemplate))
        Me.txtTemplateName = New Spectrum.Controls.TextBox(Me.components)
        Me.txtTemplateShortDesc = New Spectrum.Controls.TextBox(Me.components)
        Me.txtDescription = New Spectrum.Controls.TextBox(Me.components)
        Me.btnSave = New Spectrum.Controls.Button(Me.components)
        Me.btnCancel = New Spectrum.Controls.Button(Me.components)
        Me.rbbtnActive = New System.Windows.Forms.RadioButton()
        Me.rbbtnInactive = New System.Windows.Forms.RadioButton()
        Me.btnCreate = New Spectrum.Controls.Button(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CtrlGrid1 = New Spectrum.CtrlGrid()
        Me.Label1 = New Spectrum.Controls.LabelMandatory()
        Me.Label3 = New Spectrum.Controls.LabelMandatory()
        Me.Label2 = New Spectrum.Controls.LabelMandatory()
        Me.label4 = New Spectrum.Controls.LabelMandatory()
        CType(Me.txtTemplateName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTemplateShortDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.CtrlGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTemplateName
        '
        Me.txtTemplateName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.txtTemplateName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTemplateName.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.txtTemplateName.Location = New System.Drawing.Point(219, 18)
        Me.txtTemplateName.Name = "txtTemplateName"
        Me.txtTemplateName.Size = New System.Drawing.Size(117, 21)
        Me.txtTemplateName.TabIndex = 1
        Me.txtTemplateName.Tag = Nothing
        Me.txtTemplateName.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        Me.txtTemplateName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'txtTemplateShortDesc
        '
        Me.txtTemplateShortDesc.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.txtTemplateShortDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTemplateShortDesc.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.txtTemplateShortDesc.Location = New System.Drawing.Point(621, 18)
        Me.txtTemplateShortDesc.Name = "txtTemplateShortDesc"
        Me.txtTemplateShortDesc.Size = New System.Drawing.Size(162, 21)
        Me.txtTemplateShortDesc.TabIndex = 2
        Me.txtTemplateShortDesc.Tag = Nothing
        Me.txtTemplateShortDesc.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        Me.txtTemplateShortDesc.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'txtDescription
        '
        Me.txtDescription.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.txtDescription.Location = New System.Drawing.Point(219, 58)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(117, 21)
        Me.txtDescription.TabIndex = 3
        Me.txtDescription.Tag = Nothing
        Me.txtDescription.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        Me.txtDescription.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Location = New System.Drawing.Point(617, 342)
        Me.btnSave.MinimumSize = New System.Drawing.Size(17, 23)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(87, 23)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        Me.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Location = New System.Drawing.Point(737, 342)
        Me.btnCancel.MinimumSize = New System.Drawing.Size(17, 23)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(87, 23)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'rbbtnActive
        '
        Me.rbbtnActive.AutoSize = True
        Me.rbbtnActive.Checked = True
        Me.rbbtnActive.Location = New System.Drawing.Point(621, 63)
        Me.rbbtnActive.Name = "rbbtnActive"
        Me.rbbtnActive.Size = New System.Drawing.Size(60, 17)
        Me.rbbtnActive.TabIndex = 4
        Me.rbbtnActive.TabStop = True
        Me.rbbtnActive.Text = "Active"
        Me.rbbtnActive.UseVisualStyleBackColor = True
        '
        'rbbtnInactive
        '
        Me.rbbtnInactive.AutoSize = True
        Me.rbbtnInactive.Location = New System.Drawing.Point(707, 60)
        Me.rbbtnInactive.Name = "rbbtnInactive"
        Me.rbbtnInactive.Size = New System.Drawing.Size(72, 17)
        Me.rbbtnInactive.TabIndex = 10
        Me.rbbtnInactive.Text = "InActive"
        Me.rbbtnInactive.UseVisualStyleBackColor = True
        '
        'btnCreate
        '
        Me.btnCreate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreate.ForeColor = System.Drawing.Color.Black
        Me.btnCreate.Location = New System.Drawing.Point(737, 191)
        Me.btnCreate.MinimumSize = New System.Drawing.Size(17, 23)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(87, 23)
        Me.btnCreate.TabIndex = 7
        Me.btnCreate.Text = "Create"
        Me.btnCreate.UseVisualStyleBackColor = True
        Me.btnCreate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.label4)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtTemplateName)
        Me.Panel1.Controls.Add(Me.txtDescription)
        Me.Panel1.Controls.Add(Me.rbbtnInactive)
        Me.Panel1.Controls.Add(Me.rbbtnActive)
        Me.Panel1.Controls.Add(Me.txtTemplateShortDesc)
        Me.Panel1.Location = New System.Drawing.Point(30, 229)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(794, 97)
        Me.Panel1.TabIndex = 13
        '
        'CtrlGrid1
        '
        Me.CtrlGrid1.CellButtonImage = CType(resources.GetObject("CtrlGrid1.CellButtonImage"), System.Drawing.Image)
        Me.CtrlGrid1.ColumnInfo = resources.GetString("CtrlGrid1.ColumnInfo")
        Me.CtrlGrid1.ExtendLastCol = True
        Me.CtrlGrid1.Location = New System.Drawing.Point(30, 12)
        Me.CtrlGrid1.Name = "CtrlGrid1"
        Me.CtrlGrid1.Rows.DefaultSize = 20
        Me.CtrlGrid1.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.CtrlGrid1.Size = New System.Drawing.Size(794, 150)
        Me.CtrlGrid1.StyleInfo = resources.GetString("CtrlGrid1.StyleInfo")
        Me.CtrlGrid1.TabIndex = 14
        Me.CtrlGrid1.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 18)
        Me.Label1.MandatoryLabelText = "*"
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.NormalLabelText = "Template Name:"
        Me.Label1.Size = New System.Drawing.Size(136, 21)
        Me.Label1.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(18, 58)
        Me.Label3.MandatoryLabelText = "*"
        Me.Label3.Margin = New System.Windows.Forms.Padding(0)
        Me.Label3.Name = "Label3"
        Me.Label3.NormalLabelText = "Template Description:"
        Me.Label3.Size = New System.Drawing.Size(150, 21)
        Me.Label3.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(399, 18)
        Me.Label2.MandatoryLabelText = "*"
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.NormalLabelText = "Template Short Description:"
        Me.Label2.Size = New System.Drawing.Size(203, 21)
        Me.Label2.TabIndex = 13
        '
        'label4
        '
        Me.label4.BackColor = System.Drawing.Color.Transparent
        Me.label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.Location = New System.Drawing.Point(399, 56)
        Me.label4.MandatoryLabelText = ""
        Me.label4.Margin = New System.Windows.Forms.Padding(0)
        Me.label4.Name = "label4"
        Me.label4.NormalLabelText = "Status:"
        Me.label4.Size = New System.Drawing.Size(136, 21)
        Me.label4.TabIndex = 14
        '
        'frmAddTemplate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.ClientSize = New System.Drawing.Size(858, 377)
        Me.ControlBox = False
        Me.Controls.Add(Me.CtrlGrid1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnCreate)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Name = "frmAddTemplate"
        Me.Text = "Template Management"
        CType(Me.txtTemplateName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTemplateShortDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.CtrlGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtTemplateName As Spectrum.Controls.TextBox
    Friend WithEvents txtTemplateShortDesc As Spectrum.Controls.TextBox
    Friend WithEvents txtDescription As Spectrum.Controls.TextBox
    Friend WithEvents btnSave As Spectrum.Controls.Button
    Friend WithEvents btnCancel As Spectrum.Controls.Button
    Friend WithEvents rbbtnActive As System.Windows.Forms.RadioButton
    Friend WithEvents rbbtnInactive As System.Windows.Forms.RadioButton
    Friend WithEvents btnCreate As Spectrum.Controls.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CtrlGrid1 As Spectrum.CtrlGrid
    Friend WithEvents label4 As Spectrum.Controls.LabelMandatory
    Friend WithEvents Label2 As Spectrum.Controls.LabelMandatory
    Friend WithEvents Label3 As Spectrum.Controls.LabelMandatory
    Friend WithEvents Label1 As Spectrum.Controls.LabelMandatory
End Class
