<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrlCustInfo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrlCustInfo))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.C1SizerCustomerInfo = New C1.Win.C1Sizer.C1Sizer()
        Me.CtrlTxtSwape = New System.Windows.Forms.TextBox()
        Me.CtrlLastVisit = New Spectrum.CtrlTextBox()
        Me.CtrlLabel6 = New Spectrum.CtrlLabel()
        Me.BtnClearCustmInfo = New C1.Win.C1Input.C1Button()
        Me.CtrlLabel4 = New Spectrum.CtrlLabel()
        Me.CtrlHeader1 = New Spectrum.CtrlHeader()
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        Me.ctrlTxtPoints = New Spectrum.CtrlTextBox()
        Me.CtrltxtCustomerName = New Spectrum.CtrlTextBox()
        Me.CtrlLabel2 = New Spectrum.CtrlLabel()
        Me.CtrlLabel3 = New Spectrum.CtrlLabel()
        Me.CtrlLabel5 = New Spectrum.CtrlLabel()
        Me.CtrlTxtCustomerNo = New Spectrum.CtrlTextBox()
        Me.ctrlTxtAddress = New Spectrum.CtrlTextBox()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.C1SizerCustomerInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1SizerCustomerInfo.SuspendLayout()
        CType(Me.CtrlLastVisit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ctrlTxtPoints, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrltxtCustomerName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlTxtCustomerNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ctrlTxtAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.C1SizerCustomerInfo, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 157.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(181, 157)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'C1SizerCustomerInfo
        '
        Me.C1SizerCustomerInfo.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1SizerCustomerInfo.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1SizerCustomerInfo.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1SizerCustomerInfo.Controls.Add(Me.ctrlTxtAddress)
        Me.C1SizerCustomerInfo.Controls.Add(Me.CtrlTxtSwape)
        Me.C1SizerCustomerInfo.Controls.Add(Me.CtrlLastVisit)
        Me.C1SizerCustomerInfo.Controls.Add(Me.CtrlLabel6)
        Me.C1SizerCustomerInfo.Controls.Add(Me.BtnClearCustmInfo)
        Me.C1SizerCustomerInfo.Controls.Add(Me.CtrlLabel4)
        Me.C1SizerCustomerInfo.Controls.Add(Me.CtrlHeader1)
        Me.C1SizerCustomerInfo.Controls.Add(Me.CtrlLabel1)
        Me.C1SizerCustomerInfo.Controls.Add(Me.ctrlTxtPoints)
        Me.C1SizerCustomerInfo.Controls.Add(Me.CtrltxtCustomerName)
        Me.C1SizerCustomerInfo.Controls.Add(Me.CtrlLabel2)
        Me.C1SizerCustomerInfo.Controls.Add(Me.CtrlLabel3)
        Me.C1SizerCustomerInfo.Controls.Add(Me.CtrlLabel5)
        Me.C1SizerCustomerInfo.Controls.Add(Me.CtrlTxtCustomerNo)
        Me.C1SizerCustomerInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1SizerCustomerInfo.GridDefinition = resources.GetString("C1SizerCustomerInfo.GridDefinition")
        Me.C1SizerCustomerInfo.Location = New System.Drawing.Point(0, 0)
        Me.C1SizerCustomerInfo.Margin = New System.Windows.Forms.Padding(0)
        Me.C1SizerCustomerInfo.Name = "C1SizerCustomerInfo"
        Me.C1SizerCustomerInfo.Padding = New System.Windows.Forms.Padding(0)
        Me.C1SizerCustomerInfo.Size = New System.Drawing.Size(181, 157)
        Me.C1SizerCustomerInfo.SplitterWidth = 2
        Me.C1SizerCustomerInfo.TabIndex = 1
        '
        'CtrlTxtSwape
        '
        Me.CtrlTxtSwape.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlTxtSwape.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.CtrlTxtSwape.Location = New System.Drawing.Point(71, 58)
        Me.CtrlTxtSwape.MinimumSize = New System.Drawing.Size(10, 22)
        Me.CtrlTxtSwape.Multiline = True
        Me.CtrlTxtSwape.Name = "CtrlTxtSwape"
        Me.CtrlTxtSwape.Size = New System.Drawing.Size(109, 22)
        Me.CtrlTxtSwape.TabIndex = 13
        '
        'CtrlLastVisit
        '
        Me.CtrlLastVisit.AutoChangePosition = False
        Me.CtrlLastVisit.AutoSize = False
        Me.CtrlLastVisit.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.CtrlLastVisit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLastVisit.Location = New System.Drawing.Point(71, 133)
        Me.CtrlLastVisit.Margin = New System.Windows.Forms.Padding(3, 1, 0, 1)
        Me.CtrlLastVisit.MinimumSize = New System.Drawing.Size(10, 21)
        Me.CtrlLastVisit.MoveToNxtCtrl = Nothing
        Me.CtrlLastVisit.Name = "CtrlLastVisit"
        Me.CtrlLastVisit.Size = New System.Drawing.Size(49, 23)
        Me.CtrlLastVisit.TabIndex = 12
        Me.CtrlLastVisit.Tag = Nothing
        Me.CtrlLastVisit.TextDetached = True
        Me.CtrlLastVisit.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlLastVisit.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel6
        '
        Me.CtrlLabel6.AttachedTextBoxName = Nothing
        Me.CtrlLabel6.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel6.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.CtrlLabel6.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel6.Location = New System.Drawing.Point(1, 133)
        Me.CtrlLabel6.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.CtrlLabel6.Name = "CtrlLabel6"
        Me.CtrlLabel6.Size = New System.Drawing.Size(68, 23)
        Me.CtrlLabel6.TabIndex = 11
        Me.CtrlLabel6.Tag = Nothing
        Me.CtrlLabel6.Text = "Last Visit"
        Me.CtrlLabel6.TextDetached = True
        Me.CtrlLabel6.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnClearCustmInfo
        '
        Me.BtnClearCustmInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClearCustmInfo.Location = New System.Drawing.Point(122, 133)
        Me.BtnClearCustmInfo.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnClearCustmInfo.Name = "BtnClearCustmInfo"
        Me.BtnClearCustmInfo.Size = New System.Drawing.Size(58, 23)
        Me.BtnClearCustmInfo.TabIndex = 5
        Me.BtnClearCustmInfo.Text = "Clear"
        Me.BtnClearCustmInfo.UseVisualStyleBackColor = True
        Me.BtnClearCustmInfo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel4
        '
        Me.CtrlLabel4.AttachedTextBoxName = Nothing
        Me.CtrlLabel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrlLabel4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel4.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.CtrlLabel4.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel4.Location = New System.Drawing.Point(122, 106)
        Me.CtrlLabel4.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.CtrlLabel4.Name = "CtrlLabel4"
        Me.CtrlLabel4.Size = New System.Drawing.Size(58, 25)
        Me.CtrlLabel4.TabIndex = 4
        Me.CtrlLabel4.Tag = Nothing
        Me.CtrlLabel4.Text = "More Info (Ctrl+M)"
        Me.CtrlLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CtrlLabel4.TextDetached = True
        Me.CtrlLabel4.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlHeader1
        '
        Me.CtrlHeader1.HdrText = "Customer Info"
        Me.CtrlHeader1.Location = New System.Drawing.Point(1, 4)
        Me.CtrlHeader1.Margin = New System.Windows.Forms.Padding(3, 137, 3, 137)
        Me.CtrlHeader1.Name = "CtrlHeader1"
        Me.CtrlHeader1.Size = New System.Drawing.Size(179, 23)
        Me.CtrlHeader1.TabIndex = 6
        '
        'CtrlLabel1
        '
        Me.CtrlLabel1.AttachedTextBoxName = Nothing
        Me.CtrlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlLabel1.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel1.Location = New System.Drawing.Point(1, 29)
        Me.CtrlLabel1.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.CtrlLabel1.Name = "CtrlLabel1"
        Me.CtrlLabel1.Size = New System.Drawing.Size(68, 27)
        Me.CtrlLabel1.TabIndex = 7
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.Text = "Cust No."
        Me.CtrlLabel1.TextDetached = True
        Me.CtrlLabel1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'ctrlTxtPoints
        '
        Me.ctrlTxtPoints.AutoChangePosition = False
        Me.ctrlTxtPoints.AutoSize = False
        Me.ctrlTxtPoints.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.ctrlTxtPoints.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ctrlTxtPoints.Location = New System.Drawing.Point(71, 106)
        Me.ctrlTxtPoints.Margin = New System.Windows.Forms.Padding(3, 1, 0, 1)
        Me.ctrlTxtPoints.MinimumSize = New System.Drawing.Size(10, 21)
        Me.ctrlTxtPoints.MoveToNxtCtrl = Nothing
        Me.ctrlTxtPoints.Name = "ctrlTxtPoints"
        Me.ctrlTxtPoints.Size = New System.Drawing.Size(49, 25)
        Me.ctrlTxtPoints.TabIndex = 3
        Me.ctrlTxtPoints.Tag = Nothing
        Me.ctrlTxtPoints.TextDetached = True
        Me.ctrlTxtPoints.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.ctrlTxtPoints.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrltxtCustomerName
        '
        Me.CtrltxtCustomerName.AutoChangePosition = False
        Me.CtrltxtCustomerName.AutoSize = False
        Me.CtrltxtCustomerName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.CtrltxtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrltxtCustomerName.Location = New System.Drawing.Point(71, 82)
        Me.CtrltxtCustomerName.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.CtrltxtCustomerName.MinimumSize = New System.Drawing.Size(10, 21)
        Me.CtrltxtCustomerName.MoveToNxtCtrl = Nothing
        Me.CtrltxtCustomerName.Name = "CtrltxtCustomerName"
        Me.CtrltxtCustomerName.Size = New System.Drawing.Size(109, 22)
        Me.CtrltxtCustomerName.TabIndex = 2
        Me.CtrltxtCustomerName.Tag = Nothing
        Me.CtrltxtCustomerName.TextDetached = True
        Me.CtrltxtCustomerName.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrltxtCustomerName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel2
        '
        Me.CtrlLabel2.AttachedTextBoxName = Nothing
        Me.CtrlLabel2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.CtrlLabel2.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel2.Location = New System.Drawing.Point(1, 58)
        Me.CtrlLabel2.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.CtrlLabel2.Name = "CtrlLabel2"
        Me.CtrlLabel2.Size = New System.Drawing.Size(68, 22)
        Me.CtrlLabel2.TabIndex = 8
        Me.CtrlLabel2.Tag = Nothing
        Me.CtrlLabel2.Text = "Swipe"
        Me.CtrlLabel2.TextDetached = True
        Me.CtrlLabel2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel3
        '
        Me.CtrlLabel3.AttachedTextBoxName = Nothing
        Me.CtrlLabel3.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.CtrlLabel3.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel3.Location = New System.Drawing.Point(1, 82)
        Me.CtrlLabel3.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.CtrlLabel3.Name = "CtrlLabel3"
        Me.CtrlLabel3.Size = New System.Drawing.Size(68, 22)
        Me.CtrlLabel3.TabIndex = 9
        Me.CtrlLabel3.Tag = Nothing
        Me.CtrlLabel3.Text = "Name"
        Me.CtrlLabel3.TextDetached = True
        Me.CtrlLabel3.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel5
        '
        Me.CtrlLabel5.AttachedTextBoxName = Nothing
        Me.CtrlLabel5.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel5.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.CtrlLabel5.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel5.Location = New System.Drawing.Point(1, 106)
        Me.CtrlLabel5.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.CtrlLabel5.Name = "CtrlLabel5"
        Me.CtrlLabel5.Size = New System.Drawing.Size(68, 25)
        Me.CtrlLabel5.TabIndex = 10
        Me.CtrlLabel5.Tag = Nothing
        Me.CtrlLabel5.Text = "Bal.Point"
        Me.CtrlLabel5.TextDetached = True
        Me.CtrlLabel5.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlTxtCustomerNo
        '
        Me.CtrlTxtCustomerNo.AutoChangePosition = False
        Me.CtrlTxtCustomerNo.AutoSize = False
        Me.CtrlTxtCustomerNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.CtrlTxtCustomerNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlTxtCustomerNo.Label = Me.CtrlLabel1
        Me.CtrlTxtCustomerNo.Location = New System.Drawing.Point(71, 29)
        Me.CtrlTxtCustomerNo.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.CtrlTxtCustomerNo.MinimumSize = New System.Drawing.Size(10, 21)
        Me.CtrlTxtCustomerNo.MoveToNxtCtrl = Nothing
        Me.CtrlTxtCustomerNo.Name = "CtrlTxtCustomerNo"
        Me.CtrlTxtCustomerNo.Size = New System.Drawing.Size(109, 27)
        Me.CtrlTxtCustomerNo.TabIndex = 0
        Me.CtrlTxtCustomerNo.Tag = "NO"
        Me.CtrlTxtCustomerNo.TextDetached = True
        Me.CtrlTxtCustomerNo.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlTxtCustomerNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'ctrlTxtAddress
        '
        Me.ctrlTxtAddress.AutoSize = False
        Me.ctrlTxtAddress.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.ctrlTxtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ctrlTxtAddress.Location = New System.Drawing.Point(71, 106)
        Me.ctrlTxtAddress.MinimumSize = New System.Drawing.Size(10, 21)
        Me.ctrlTxtAddress.MoveToNxtCtrl = Nothing
        Me.ctrlTxtAddress.Name = "ctrlTxtAddress"
        Me.ctrlTxtAddress.Size = New System.Drawing.Size(109, 25)
        Me.ctrlTxtAddress.TabIndex = 15
        Me.ctrlTxtAddress.Tag = Nothing
        Me.ctrlTxtAddress.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.ctrlTxtAddress.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlCustInfo
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "CtrlCustInfo"
        Me.Size = New System.Drawing.Size(181, 157)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.C1SizerCustomerInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1SizerCustomerInfo.ResumeLayout(False)
        Me.C1SizerCustomerInfo.PerformLayout()
        CType(Me.CtrlLastVisit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ctrlTxtPoints, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrltxtCustomerName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlTxtCustomerNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ctrlTxtAddress, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents C1SizerCustomerInfo As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents CtrlTxtSwape As System.Windows.Forms.TextBox
    Friend WithEvents CtrlLastVisit As Spectrum.CtrlTextBox
    Friend WithEvents CtrlLabel6 As Spectrum.CtrlLabel
    Friend WithEvents BtnClearCustmInfo As C1.Win.C1Input.C1Button
    Friend WithEvents CtrlLabel4 As Spectrum.CtrlLabel
    Friend WithEvents CtrlHeader1 As Spectrum.CtrlHeader
    Friend WithEvents CtrlLabel1 As Spectrum.CtrlLabel
    Friend WithEvents ctrlTxtPoints As Spectrum.CtrlTextBox
    Friend WithEvents CtrltxtCustomerName As Spectrum.CtrlTextBox
    Friend WithEvents CtrlLabel2 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel3 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel5 As Spectrum.CtrlLabel
    Friend WithEvents CtrlTxtCustomerNo As Spectrum.CtrlTextBox
    Friend WithEvents ctrlTxtAddress As Spectrum.CtrlTextBox

End Class
