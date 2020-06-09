<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrlTextBox
    Inherits C1.Win.C1Input.C1TextBox

    'Control overrides dispose to clean up the component list.
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

    'Required by the Control Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  Do not modify it
    ' using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CtrlTextBox
        '
        Me.AutoSize = False
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MinimumSize = New System.Drawing.Size(10, 21)
        Me.Size = New System.Drawing.Size(100, 21)
        Me.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

End Class

