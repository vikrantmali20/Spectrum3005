<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrlPromo
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
        Me.lblname = New System.Windows.Forms.Label()
        Me.lblvalue = New System.Windows.Forms.Label()
        Me.ctrlbtnApply = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblname
        '
        Me.lblname.AutoSize = True
        Me.lblname.Location = New System.Drawing.Point(26, 4)
        Me.lblname.Name = "lblname"
        Me.lblname.Size = New System.Drawing.Size(39, 13)
        Me.lblname.TabIndex = 0
        Me.lblname.Text = "Label1"
        '
        'lblvalue
        '
        Me.lblvalue.AutoSize = True
        Me.lblvalue.Location = New System.Drawing.Point(264, 4)
        Me.lblvalue.Name = "lblvalue"
        Me.lblvalue.Size = New System.Drawing.Size(39, 13)
        Me.lblvalue.TabIndex = 1
        Me.lblvalue.Text = "Label2"
        '
        'ctrlbtnApply
        '
        Me.ctrlbtnApply.Location = New System.Drawing.Point(566, 4)
        Me.ctrlbtnApply.Name = "ctrlbtnApply"
        Me.ctrlbtnApply.Size = New System.Drawing.Size(131, 39)
        Me.ctrlbtnApply.TabIndex = 2
        Me.ctrlbtnApply.Text = "Apply"
        Me.ctrlbtnApply.UseVisualStyleBackColor = True
        '
        'CtrlPromo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ctrlbtnApply)
        Me.Controls.Add(Me.lblvalue)
        Me.Controls.Add(Me.lblname)
        Me.name = "CtrlPromo"
        Me.Size = New System.Drawing.Size(704, 42)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblname As System.Windows.Forms.Label
    Friend WithEvents lblvalue As System.Windows.Forms.Label
    Friend WithEvents ctrlbtnApply As System.Windows.Forms.Button

End Class
