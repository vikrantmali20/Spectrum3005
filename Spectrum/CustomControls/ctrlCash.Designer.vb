<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrlCash
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
        Me.txtCash = New Spectrum.CtrlTextBox
        CType(Me.txtCash, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCash
        '
        Me.txtCash.AutoSize = False
        Me.txtCash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCash.DataType = GetType(Double)
        Me.txtCash.DateTimeInput = False
        Me.txtCash.EmptyAsNull = True
        Me.txtCash.ErrorInfo.ShowErrorMessage = False
        Me.txtCash.Location = New System.Drawing.Point(3, 2)
        Me.txtCash.MaximumSize = New System.Drawing.Size(200, 21)
        Me.txtCash.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtCash.Name = "txtCash"
        Me.txtCash.Size = New System.Drawing.Size(72, 21)
        Me.txtCash.TabIndex = 0
        Me.txtCash.Tag = Nothing
        Me.txtCash.Value = 0
        Me.txtCash.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtCash.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'ctrlCash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.txtCash)
        Me.Name = "ctrlCash"
        Me.Size = New System.Drawing.Size(79, 27)
        CType(Me.txtCash, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtCash As Spectrum.CtrlTextBox

End Class
