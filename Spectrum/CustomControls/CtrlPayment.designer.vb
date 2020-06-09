<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrlPayment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrlPayment))
        Me.C1Sizer1 = New C1.Win.C1Sizer.C1Sizer
        Me.CtrlListPayment = New C1.Win.C1List.C1List
        Me.CtrlHeader1 = New Spectrum.CtrlHeader
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer1.SuspendLayout()
        CType(Me.CtrlListPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'C1Sizer1
        '
        Me.C1Sizer1.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1Sizer1.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1Sizer1.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1Sizer1.Controls.Add(Me.CtrlListPayment)
        Me.C1Sizer1.Controls.Add(Me.CtrlHeader1)
        Me.C1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Sizer1.GridDefinition = "16.8918918918919:False:True;73.6486486486486:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "94.9494949494949:False:" & _
            "False;"
        Me.C1Sizer1.Location = New System.Drawing.Point(0, 0)
        Me.C1Sizer1.Name = "C1Sizer1"
        Me.C1Sizer1.Size = New System.Drawing.Size(198, 148)
        Me.C1Sizer1.TabIndex = 3
        Me.C1Sizer1.Text = "C1Sizer1"
        '
        'CtrlListPayment
        '
        Me.CtrlListPayment.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.CtrlListPayment.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlListPayment.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.CtrlListPayment.Caption = ""
        Me.CtrlListPayment.CaptionHeight = 18
        Me.CtrlListPayment.ColumnCaptionHeight = 17
        Me.CtrlListPayment.ColumnFooterHeight = 17
        Me.CtrlListPayment.DeadAreaBackColor = System.Drawing.Color.Transparent
        Me.CtrlListPayment.EmptyRows = True
        Me.CtrlListPayment.ExtendRightColumn = True
        Me.CtrlListPayment.FlatStyle = C1.Win.C1List.FlatModeEnum.Popup
        Me.CtrlListPayment.Images.Add(CType(resources.GetObject("CtrlListPayment.Images"), System.Drawing.Image))
        Me.CtrlListPayment.ItemHeight = 15
        Me.CtrlListPayment.Location = New System.Drawing.Point(5, 34)
        Me.CtrlListPayment.MatchEntryTimeout = CType(2000, Long)
        Me.CtrlListPayment.Name = "CtrlListPayment"
        Me.CtrlListPayment.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.CtrlListPayment.Size = New System.Drawing.Size(188, 109)
        Me.CtrlListPayment.TabIndex = 2
        Me.CtrlListPayment.Text = "C1List1"
        Me.CtrlListPayment.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.CtrlListPayment.PropBag = resources.GetString("CtrlListPayment.PropBag")
        '
        'CtrlHeader1
        '
        Me.CtrlHeader1.HdrText = "Payment Summary"
        Me.CtrlHeader1.Location = New System.Drawing.Point(5, 5)
        Me.CtrlHeader1.Name = "CtrlHeader1"
        Me.CtrlHeader1.Size = New System.Drawing.Size(188, 25)
        Me.CtrlHeader1.TabIndex = 1
        '
        'CtrlPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.C1Sizer1)
        Me.Name = "CtrlPayment"
        Me.Size = New System.Drawing.Size(198, 148)
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer1.ResumeLayout(False)
        CType(Me.CtrlListPayment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents C1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents CtrlListPayment As C1.Win.C1List.C1List
    Friend WithEvents CtrlHeader1 As CtrlHeader

End Class
