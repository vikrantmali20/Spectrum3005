<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrlPatientImage
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
        Me.PicBoxImages = New C1.Win.C1Input.C1PictureBox
        Me.C1Sizer1 = New C1.Win.C1Sizer.C1Sizer
        Me.CtrlHeader1 = New Spectrum.CtrlHeader
        CType(Me.PicBoxImages, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PicBoxImages
        '
        Me.PicBoxImages.BackColor = System.Drawing.Color.Ivory
        Me.PicBoxImages.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicBoxImages.InitialImage = Global.Spectrum.My.Resources.Resources.product_babyharness
        Me.PicBoxImages.Location = New System.Drawing.Point(5, 29)
        Me.PicBoxImages.Margin = New System.Windows.Forms.Padding(0)
        Me.PicBoxImages.Name = "PicBoxImages"
        Me.PicBoxImages.Size = New System.Drawing.Size(140, 116)
        Me.PicBoxImages.TabIndex = 2
        Me.PicBoxImages.TabStop = False
        '
        'C1Sizer1
        '
        Me.C1Sizer1.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1Sizer1.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1Sizer1.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1Sizer1.Controls.Add(Me.PicBoxImages)
        Me.C1Sizer1.Controls.Add(Me.CtrlHeader1)
        Me.C1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Sizer1.GridDefinition = "13.3333333333333:False:True;77.3333333333333:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "93.3333333333333:False:" & _
            "False;"
        Me.C1Sizer1.Location = New System.Drawing.Point(0, 0)
        Me.C1Sizer1.Name = "C1Sizer1"
        Me.C1Sizer1.Size = New System.Drawing.Size(150, 150)
        Me.C1Sizer1.TabIndex = 5
        Me.C1Sizer1.Text = "C1Sizer1"
        '
        'CtrlHeader1
        '
        Me.CtrlHeader1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlHeader1.HdrText = "Patient Image"
        Me.CtrlHeader1.Location = New System.Drawing.Point(5, 5)
        Me.CtrlHeader1.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.CtrlHeader1.Name = "CtrlHeader1"
        Me.CtrlHeader1.Size = New System.Drawing.Size(140, 20)
        Me.CtrlHeader1.TabIndex = 1
        '
        'CtrlPatientImage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.C1Sizer1)
        Me.Name = "CtrlPatientImage"
        CType(Me.PicBoxImages, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PicBoxImages As C1.Win.C1Input.C1PictureBox
    Friend WithEvents C1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents CtrlHeader1 As Spectrum.CtrlHeader

End Class
