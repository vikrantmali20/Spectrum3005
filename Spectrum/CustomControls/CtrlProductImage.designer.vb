<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrlProductImage
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
        Me.CtrlHeader1 = New Spectrum.CtrlHeader()
        Me.C1Sizer1 = New C1.Win.C1Sizer.C1Sizer()
        Me.CtrlProductImages = New C1.Win.C1Input.C1PictureBox()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer1.SuspendLayout()
        CType(Me.CtrlProductImages, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CtrlHeader1
        '
        Me.CtrlHeader1.HdrText = "Product Image"
        Me.CtrlHeader1.Location = New System.Drawing.Point(5, 5)
        Me.CtrlHeader1.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.CtrlHeader1.Name = "CtrlHeader1"
        Me.CtrlHeader1.Size = New System.Drawing.Size(139, 20)
        Me.CtrlHeader1.TabIndex = 1
        '
        'C1Sizer1
        '
        Me.C1Sizer1.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1Sizer1.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1Sizer1.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1Sizer1.Controls.Add(Me.CtrlProductImages)
        Me.C1Sizer1.Controls.Add(Me.CtrlHeader1)
        Me.C1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Sizer1.GridDefinition = "14.0845070422535:False:True;76.056338028169:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "93.2885906040269:False:F" & _
            "alse;"
        Me.C1Sizer1.Location = New System.Drawing.Point(0, 0)
        Me.C1Sizer1.Name = "C1Sizer1"
        Me.C1Sizer1.Size = New System.Drawing.Size(149, 142)
        Me.C1Sizer1.TabIndex = 4
        Me.C1Sizer1.Text = "C1Sizer1"
        '
        'CtrlProductImages
        '
        Me.CtrlProductImages.BackColor = System.Drawing.Color.White
        Me.CtrlProductImages.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.CtrlProductImages.InitialImage = Global.Spectrum.My.Resources.Resources.product_babyharness
        Me.CtrlProductImages.Location = New System.Drawing.Point(5, 29)
        Me.CtrlProductImages.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrlProductImages.Name = "CtrlProductImages"
        Me.CtrlProductImages.Size = New System.Drawing.Size(139, 108)
        Me.CtrlProductImages.TabIndex = 2
        Me.CtrlProductImages.TabStop = False
        '
        'CtrlProductImage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.C1Sizer1)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "CtrlProductImage"
        Me.Size = New System.Drawing.Size(149, 142)
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer1.ResumeLayout(False)
        CType(Me.CtrlProductImages, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrlHeader1 As CtrlHeader
    Friend WithEvents C1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents CtrlProductImages As C1.Win.C1Input.C1PictureBox

End Class
