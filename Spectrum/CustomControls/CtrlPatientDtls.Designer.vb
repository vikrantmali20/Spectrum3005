<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrlPatientDtls
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LblPatientID = New Spectrum.CtrlLabel()
        Me.LblPatientName = New Spectrum.CtrlLabel()
        Me.LblReferedByDr = New Spectrum.CtrlLabel()
        Me.LblGender = New Spectrum.CtrlLabel()
        Me.LblAge = New Spectrum.CtrlLabel()
        Me.LblHeight = New Spectrum.CtrlLabel()
        Me.LblWeight = New Spectrum.CtrlLabel()
        Me.LblMobileNo = New C1.Win.C1Input.C1Label()
        Me.LblCity = New C1.Win.C1Input.C1Label()
        Me.LblPrimaryPhone = New C1.Win.C1Input.C1Label()
        Me.txtCity = New Spectrum.CtrlTextBox()
        Me.txtPrimaryPhone = New Spectrum.CtrlTextBox()
        Me.txtReferedByDr = New Spectrum.CtrlTextBox()
        Me.txtPatientID = New Spectrum.CtrlTextBox()
        Me.BtnSearchPatient = New Spectrum.CtrlBtn()
        Me.txtPatientName = New Spectrum.CtrlTextBox()
        Me.txtGender = New Spectrum.CtrlTextBox()
        Me.txtAge = New Spectrum.CtrlTextBox()
        Me.txtHeight = New Spectrum.CtrlTextBox()
        Me.txtWeight = New Spectrum.CtrlTextBox()
        Me.txtMobileNo = New Spectrum.CtrlTextBox()
        Me.BtnClinicalHistory = New Spectrum.CtrlBtn()
        Me.BtnMoreInfo = New Spectrum.CtrlBtn()
        Me.TreeViewPatient = New System.Windows.Forms.TreeView()
        Me.CtrlFileList = New Spectrum.CtrlHeader()
        Me.CtrlPatientImage1 = New Spectrum.CtrlPatientImage()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.LblPatientID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblPatientName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblReferedByDr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblGender, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblAge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblMobileNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblPrimaryPhone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrimaryPhone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReferedByDr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPatientID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPatientName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGender, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMobileNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.TableLayoutPanel1.ColumnCount = 9
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 257.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.LblPatientID, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LblPatientName, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LblReferedByDr, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LblGender, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LblAge, 5, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LblHeight, 5, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LblWeight, 5, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LblMobileNo, 5, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.LblCity, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.LblPrimaryPhone, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.txtCity, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.txtPrimaryPhone, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.txtReferedByDr, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.txtPatientID, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnSearchPatient, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtPatientName, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.txtGender, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtAge, 6, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.txtHeight, 6, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.txtWeight, 6, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.txtMobileNo, 6, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnClinicalHistory, 6, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnMoreInfo, 2, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.TreeViewPatient, 8, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlFileList, 7, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlPatientImage1, 7, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.Padding = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(771, 169)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'LblPatientID
        '
        Me.LblPatientID.AttachedTextBoxName = Nothing
        Me.LblPatientID.BackColor = System.Drawing.Color.Transparent
        Me.LblPatientID.BorderColor = System.Drawing.Color.Transparent
        Me.LblPatientID.ForeColor = System.Drawing.Color.Black
        Me.LblPatientID.Location = New System.Drawing.Point(5, 2)
        Me.LblPatientID.MinimumSize = New System.Drawing.Size(10, 22)
        Me.LblPatientID.Name = "LblPatientID"
        Me.LblPatientID.Size = New System.Drawing.Size(84, 22)
        Me.LblPatientID.TabIndex = 80
        Me.LblPatientID.Tag = Nothing
        Me.LblPatientID.Text = "Patient ID :"
        Me.LblPatientID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblPatientID.TextDetached = True
        Me.LblPatientID.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'LblPatientName
        '
        Me.LblPatientName.AttachedTextBoxName = Nothing
        Me.LblPatientName.BackColor = System.Drawing.Color.Transparent
        Me.LblPatientName.BorderColor = System.Drawing.Color.Transparent
        Me.LblPatientName.ForeColor = System.Drawing.Color.Black
        Me.LblPatientName.Location = New System.Drawing.Point(5, 30)
        Me.LblPatientName.MinimumSize = New System.Drawing.Size(10, 22)
        Me.LblPatientName.Name = "LblPatientName"
        Me.LblPatientName.Size = New System.Drawing.Size(84, 22)
        Me.LblPatientName.TabIndex = 89
        Me.LblPatientName.Tag = Nothing
        Me.LblPatientName.Text = "Patient Name :"
        Me.LblPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblPatientName.TextDetached = True
        Me.LblPatientName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'LblReferedByDr
        '
        Me.LblReferedByDr.AttachedTextBoxName = Nothing
        Me.LblReferedByDr.BackColor = System.Drawing.Color.Transparent
        Me.LblReferedByDr.BorderColor = System.Drawing.Color.Transparent
        Me.LblReferedByDr.ForeColor = System.Drawing.Color.Black
        Me.LblReferedByDr.Location = New System.Drawing.Point(5, 58)
        Me.LblReferedByDr.MinimumSize = New System.Drawing.Size(10, 22)
        Me.LblReferedByDr.Name = "LblReferedByDr"
        Me.LblReferedByDr.Size = New System.Drawing.Size(85, 22)
        Me.LblReferedByDr.TabIndex = 90
        Me.LblReferedByDr.Tag = Nothing
        Me.LblReferedByDr.Text = "Referred by Dr. :"
        Me.LblReferedByDr.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblReferedByDr.TextDetached = True
        Me.LblReferedByDr.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'LblGender
        '
        Me.LblGender.AttachedTextBoxName = Nothing
        Me.LblGender.BackColor = System.Drawing.Color.Transparent
        Me.LblGender.BorderColor = System.Drawing.Color.Transparent
        Me.LblGender.ForeColor = System.Drawing.Color.Black
        Me.LblGender.Location = New System.Drawing.Point(281, 2)
        Me.LblGender.MinimumSize = New System.Drawing.Size(10, 22)
        Me.LblGender.Name = "LblGender"
        Me.LblGender.Size = New System.Drawing.Size(59, 22)
        Me.LblGender.TabIndex = 103
        Me.LblGender.Tag = Nothing
        Me.LblGender.Text = "Gender :"
        Me.LblGender.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblGender.TextDetached = True
        Me.LblGender.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'LblAge
        '
        Me.LblAge.AttachedTextBoxName = Nothing
        Me.LblAge.BackColor = System.Drawing.Color.Transparent
        Me.LblAge.BorderColor = System.Drawing.Color.Transparent
        Me.LblAge.ForeColor = System.Drawing.Color.Black
        Me.LblAge.Location = New System.Drawing.Point(281, 30)
        Me.LblAge.MinimumSize = New System.Drawing.Size(10, 22)
        Me.LblAge.Name = "LblAge"
        Me.LblAge.Size = New System.Drawing.Size(59, 22)
        Me.LblAge.TabIndex = 105
        Me.LblAge.Tag = Nothing
        Me.LblAge.Text = "Age :"
        Me.LblAge.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblAge.TextDetached = True
        Me.LblAge.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'LblHeight
        '
        Me.LblHeight.AttachedTextBoxName = Nothing
        Me.LblHeight.BackColor = System.Drawing.Color.Transparent
        Me.LblHeight.BorderColor = System.Drawing.Color.Transparent
        Me.LblHeight.ForeColor = System.Drawing.Color.Black
        Me.LblHeight.Location = New System.Drawing.Point(281, 58)
        Me.LblHeight.MinimumSize = New System.Drawing.Size(10, 22)
        Me.LblHeight.Name = "LblHeight"
        Me.LblHeight.Size = New System.Drawing.Size(59, 22)
        Me.LblHeight.TabIndex = 107
        Me.LblHeight.Tag = Nothing
        Me.LblHeight.Text = "Height :"
        Me.LblHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblHeight.TextDetached = True
        Me.LblHeight.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'LblWeight
        '
        Me.LblWeight.AttachedTextBoxName = Nothing
        Me.LblWeight.BackColor = System.Drawing.Color.Transparent
        Me.LblWeight.BorderColor = System.Drawing.Color.Transparent
        Me.LblWeight.ForeColor = System.Drawing.Color.Black
        Me.LblWeight.Location = New System.Drawing.Point(281, 86)
        Me.LblWeight.MinimumSize = New System.Drawing.Size(10, 22)
        Me.LblWeight.Name = "LblWeight"
        Me.LblWeight.Size = New System.Drawing.Size(59, 22)
        Me.LblWeight.TabIndex = 109
        Me.LblWeight.Tag = Nothing
        Me.LblWeight.Text = "Weight :"
        Me.LblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblWeight.TextDetached = True
        Me.LblWeight.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'LblMobileNo
        '
        Me.LblMobileNo.BackColor = System.Drawing.Color.Transparent
        Me.LblMobileNo.BorderColor = System.Drawing.Color.Transparent
        Me.LblMobileNo.ForeColor = System.Drawing.Color.Black
        Me.LblMobileNo.Location = New System.Drawing.Point(281, 114)
        Me.LblMobileNo.MinimumSize = New System.Drawing.Size(10, 22)
        Me.LblMobileNo.Name = "LblMobileNo"
        Me.LblMobileNo.Size = New System.Drawing.Size(59, 22)
        Me.LblMobileNo.TabIndex = 111
        Me.LblMobileNo.Tag = Nothing
        Me.LblMobileNo.Text = "Mobile No. :"
        Me.LblMobileNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblMobileNo.TextDetached = True
        Me.LblMobileNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'LblCity
        '
        Me.LblCity.BackColor = System.Drawing.Color.Transparent
        Me.LblCity.ForeColor = System.Drawing.Color.Black
        Me.LblCity.Location = New System.Drawing.Point(5, 114)
        Me.LblCity.MinimumSize = New System.Drawing.Size(10, 22)
        Me.LblCity.Name = "LblCity"
        Me.LblCity.Size = New System.Drawing.Size(84, 22)
        Me.LblCity.TabIndex = 97
        Me.LblCity.Tag = Nothing
        Me.LblCity.Text = "City :"
        Me.LblCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblCity.TextDetached = True
        Me.LblCity.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'LblPrimaryPhone
        '
        Me.LblPrimaryPhone.BackColor = System.Drawing.Color.Transparent
        Me.LblPrimaryPhone.ForeColor = System.Drawing.Color.Black
        Me.LblPrimaryPhone.Location = New System.Drawing.Point(5, 86)
        Me.LblPrimaryPhone.MinimumSize = New System.Drawing.Size(10, 22)
        Me.LblPrimaryPhone.Name = "LblPrimaryPhone"
        Me.LblPrimaryPhone.Size = New System.Drawing.Size(84, 22)
        Me.LblPrimaryPhone.TabIndex = 91
        Me.LblPrimaryPhone.Tag = Nothing
        Me.LblPrimaryPhone.Text = "Phone No :"
        Me.LblPrimaryPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblPrimaryPhone.TextDetached = True
        Me.LblPrimaryPhone.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtCity
        '
        Me.txtCity.AcceptsTab = True
        Me.txtCity.AutoSize = False
        Me.txtCity.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.txtCity.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtCity, 3)
        Me.txtCity.Location = New System.Drawing.Point(98, 117)
        Me.txtCity.MinimumSize = New System.Drawing.Size(10, 22)
        Me.txtCity.MoveToNxtCtrl = Nothing
        Me.txtCity.Name = "txtCity"
        Me.txtCity.ReadOnly = True
        Me.txtCity.Size = New System.Drawing.Size(169, 22)
        Me.txtCity.TabIndex = 102
        Me.txtCity.Tag = Nothing
        Me.txtCity.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtCity.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtPrimaryPhone
        '
        Me.txtPrimaryPhone.AcceptsTab = True
        Me.txtPrimaryPhone.AutoSize = False
        Me.txtPrimaryPhone.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.txtPrimaryPhone.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtPrimaryPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtPrimaryPhone, 3)
        Me.txtPrimaryPhone.Location = New System.Drawing.Point(98, 89)
        Me.txtPrimaryPhone.MinimumSize = New System.Drawing.Size(10, 22)
        Me.txtPrimaryPhone.MoveToNxtCtrl = Nothing
        Me.txtPrimaryPhone.Name = "txtPrimaryPhone"
        Me.txtPrimaryPhone.ReadOnly = True
        Me.txtPrimaryPhone.Size = New System.Drawing.Size(169, 22)
        Me.txtPrimaryPhone.TabIndex = 101
        Me.txtPrimaryPhone.Tag = Nothing
        Me.txtPrimaryPhone.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtPrimaryPhone.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtReferedByDr
        '
        Me.txtReferedByDr.AcceptsTab = True
        Me.txtReferedByDr.AutoSize = False
        Me.txtReferedByDr.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.txtReferedByDr.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtReferedByDr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtReferedByDr, 3)
        Me.txtReferedByDr.Location = New System.Drawing.Point(98, 61)
        Me.txtReferedByDr.MinimumSize = New System.Drawing.Size(10, 22)
        Me.txtReferedByDr.MoveToNxtCtrl = Nothing
        Me.txtReferedByDr.Name = "txtReferedByDr"
        Me.txtReferedByDr.ReadOnly = True
        Me.txtReferedByDr.Size = New System.Drawing.Size(169, 22)
        Me.txtReferedByDr.TabIndex = 100
        Me.txtReferedByDr.Tag = Nothing
        Me.txtReferedByDr.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtReferedByDr.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtPatientID
        '
        Me.txtPatientID.AcceptsTab = True
        Me.txtPatientID.AutoSize = False
        Me.txtPatientID.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.txtPatientID.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtPatientID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPatientID.Location = New System.Drawing.Point(98, 5)
        Me.txtPatientID.MinimumSize = New System.Drawing.Size(10, 22)
        Me.txtPatientID.MoveToNxtCtrl = Nothing
        Me.txtPatientID.Name = "txtPatientID"
        Me.txtPatientID.ReadOnly = True
        Me.txtPatientID.Size = New System.Drawing.Size(94, 22)
        Me.txtPatientID.TabIndex = 98
        Me.txtPatientID.Tag = Nothing
        Me.txtPatientID.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtPatientID.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnSearchPatient
        '
        Me.BtnSearchPatient.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnSearchPatient.Image = Global.Spectrum.My.Resources.Resources.search_2
        Me.BtnSearchPatient.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnSearchPatient.Location = New System.Drawing.Point(198, 5)
        Me.BtnSearchPatient.MoveToNxtCtrl = Nothing
        Me.BtnSearchPatient.Name = "BtnSearchPatient"
        Me.BtnSearchPatient.SetArticleCode = Nothing
        Me.BtnSearchPatient.SetRowIndex = 0
        Me.BtnSearchPatient.Size = New System.Drawing.Size(24, 22)
        Me.BtnSearchPatient.TabIndex = 66
        Me.BtnSearchPatient.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnSearchPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnSearchPatient.UseVisualStyleBackColor = True
        Me.BtnSearchPatient.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtPatientName
        '
        Me.txtPatientName.AcceptsTab = True
        Me.txtPatientName.AutoSize = False
        Me.txtPatientName.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.txtPatientName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtPatientName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel1.SetColumnSpan(Me.txtPatientName, 3)
        Me.txtPatientName.Location = New System.Drawing.Point(98, 33)
        Me.txtPatientName.MinimumSize = New System.Drawing.Size(10, 22)
        Me.txtPatientName.MoveToNxtCtrl = Nothing
        Me.txtPatientName.Name = "txtPatientName"
        Me.txtPatientName.ReadOnly = True
        Me.txtPatientName.Size = New System.Drawing.Size(169, 22)
        Me.txtPatientName.TabIndex = 99
        Me.txtPatientName.Tag = Nothing
        Me.txtPatientName.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtPatientName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtGender
        '
        Me.txtGender.AcceptsTab = True
        Me.txtGender.AutoSize = False
        Me.txtGender.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.txtGender.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtGender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGender.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtGender.Location = New System.Drawing.Point(346, 5)
        Me.txtGender.MinimumSize = New System.Drawing.Size(10, 22)
        Me.txtGender.MoveToNxtCtrl = Nothing
        Me.txtGender.Name = "txtGender"
        Me.txtGender.ReadOnly = True
        Me.txtGender.Size = New System.Drawing.Size(114, 22)
        Me.txtGender.TabIndex = 104
        Me.txtGender.Tag = Nothing
        Me.txtGender.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtGender.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtAge
        '
        Me.txtAge.AcceptsTab = True
        Me.txtAge.AutoSize = False
        Me.txtAge.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.txtAge.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtAge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAge.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtAge.Location = New System.Drawing.Point(346, 33)
        Me.txtAge.MinimumSize = New System.Drawing.Size(10, 22)
        Me.txtAge.MoveToNxtCtrl = Nothing
        Me.txtAge.Name = "txtAge"
        Me.txtAge.ReadOnly = True
        Me.txtAge.Size = New System.Drawing.Size(114, 22)
        Me.txtAge.TabIndex = 106
        Me.txtAge.Tag = Nothing
        Me.txtAge.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtAge.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtHeight
        '
        Me.txtHeight.AcceptsTab = True
        Me.txtHeight.AutoSize = False
        Me.txtHeight.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.txtHeight.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHeight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtHeight.Location = New System.Drawing.Point(346, 61)
        Me.txtHeight.MinimumSize = New System.Drawing.Size(10, 22)
        Me.txtHeight.MoveToNxtCtrl = Nothing
        Me.txtHeight.Name = "txtHeight"
        Me.txtHeight.ReadOnly = True
        Me.txtHeight.Size = New System.Drawing.Size(114, 22)
        Me.txtHeight.TabIndex = 108
        Me.txtHeight.Tag = Nothing
        Me.txtHeight.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtHeight.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtWeight
        '
        Me.txtWeight.AcceptsTab = True
        Me.txtWeight.AutoSize = False
        Me.txtWeight.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.txtWeight.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtWeight.Location = New System.Drawing.Point(346, 89)
        Me.txtWeight.MinimumSize = New System.Drawing.Size(10, 22)
        Me.txtWeight.MoveToNxtCtrl = Nothing
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.ReadOnly = True
        Me.txtWeight.Size = New System.Drawing.Size(114, 22)
        Me.txtWeight.TabIndex = 110
        Me.txtWeight.Tag = Nothing
        Me.txtWeight.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtWeight.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtMobileNo
        '
        Me.txtMobileNo.AcceptsTab = True
        Me.txtMobileNo.AutoSize = False
        Me.txtMobileNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.txtMobileNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtMobileNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMobileNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMobileNo.Location = New System.Drawing.Point(346, 117)
        Me.txtMobileNo.MinimumSize = New System.Drawing.Size(10, 22)
        Me.txtMobileNo.MoveToNxtCtrl = Nothing
        Me.txtMobileNo.Name = "txtMobileNo"
        Me.txtMobileNo.ReadOnly = True
        Me.txtMobileNo.Size = New System.Drawing.Size(114, 22)
        Me.txtMobileNo.TabIndex = 112
        Me.txtMobileNo.Tag = Nothing
        Me.txtMobileNo.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtMobileNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnClinicalHistory
        '
        Me.BtnClinicalHistory.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnClinicalHistory.Location = New System.Drawing.Point(346, 145)
        Me.BtnClinicalHistory.MoveToNxtCtrl = Nothing
        Me.BtnClinicalHistory.Name = "BtnClinicalHistory"
        Me.BtnClinicalHistory.SetArticleCode = Nothing
        Me.BtnClinicalHistory.SetRowIndex = 0
        Me.BtnClinicalHistory.Size = New System.Drawing.Size(94, 19)
        Me.BtnClinicalHistory.TabIndex = 76
        Me.BtnClinicalHistory.Text = "Clinical History"
        Me.BtnClinicalHistory.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnClinicalHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnClinicalHistory.UseVisualStyleBackColor = True
        Me.BtnClinicalHistory.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnMoreInfo
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.BtnMoreInfo, 4)
        Me.BtnMoreInfo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnMoreInfo.Location = New System.Drawing.Point(198, 145)
        Me.BtnMoreInfo.MoveToNxtCtrl = Nothing
        Me.BtnMoreInfo.Name = "BtnMoreInfo"
        Me.BtnMoreInfo.SetArticleCode = Nothing
        Me.BtnMoreInfo.SetRowIndex = 0
        Me.BtnMoreInfo.Size = New System.Drawing.Size(84, 19)
        Me.BtnMoreInfo.TabIndex = 77
        Me.BtnMoreInfo.Text = "More Info"
        Me.BtnMoreInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnMoreInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnMoreInfo.UseVisualStyleBackColor = True
        Me.BtnMoreInfo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'TreeViewPatient
        '
        Me.TreeViewPatient.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeViewPatient.Location = New System.Drawing.Point(611, 30)
        Me.TreeViewPatient.Margin = New System.Windows.Forms.Padding(0)
        Me.TreeViewPatient.Name = "TreeViewPatient"
        Me.TableLayoutPanel1.SetRowSpan(Me.TreeViewPatient, 5)
        Me.TreeViewPatient.Size = New System.Drawing.Size(257, 137)
        Me.TreeViewPatient.TabIndex = 113
        '
        'CtrlFileList
        '
        Me.CtrlFileList.BackColor = System.Drawing.Color.Transparent
        Me.CtrlFileList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CtrlFileList.HdrText = "Documents"
        Me.CtrlFileList.Location = New System.Drawing.Point(611, 5)
        Me.CtrlFileList.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.CtrlFileList.Name = "CtrlFileList"
        Me.CtrlFileList.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CtrlFileList.Size = New System.Drawing.Size(257, 25)
        Me.CtrlFileList.TabIndex = 115
        '
        'CtrlPatientImage1
        '
        Me.CtrlPatientImage1.Location = New System.Drawing.Point(466, 5)
        Me.CtrlPatientImage1.Name = "CtrlPatientImage1"
        Me.TableLayoutPanel1.SetRowSpan(Me.CtrlPatientImage1, 7)
        Me.CtrlPatientImage1.Size = New System.Drawing.Size(141, 159)
        Me.CtrlPatientImage1.TabIndex = 116
        '
        'CtrlPatientDtls
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "CtrlPatientDtls"
        Me.Size = New System.Drawing.Size(771, 169)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.LblPatientID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblPatientName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblReferedByDr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblGender, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblAge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblMobileNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblPrimaryPhone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrimaryPhone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReferedByDr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPatientID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPatientName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGender, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMobileNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnSearchPatient As Spectrum.CtrlBtn
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents BtnMoreInfo As Spectrum.CtrlBtn
    Friend WithEvents BtnClinicalHistory As Spectrum.CtrlBtn
    Friend WithEvents LblPatientID As Spectrum.CtrlLabel
    Friend WithEvents LblPatientName As Spectrum.CtrlLabel
    Friend WithEvents LblReferedByDr As Spectrum.CtrlLabel
    Friend WithEvents LblPrimaryPhone As C1.Win.C1Input.C1Label
    Friend WithEvents LblCity As C1.Win.C1Input.C1Label
    Friend WithEvents txtPatientID As Spectrum.CtrlTextBox
    Friend WithEvents txtCity As Spectrum.CtrlTextBox
    Friend WithEvents txtPrimaryPhone As Spectrum.CtrlTextBox
    Friend WithEvents txtReferedByDr As Spectrum.CtrlTextBox
    Friend WithEvents txtPatientName As Spectrum.CtrlTextBox
    Friend WithEvents txtGender As Spectrum.CtrlTextBox
    Friend WithEvents LblGender As Spectrum.CtrlLabel
    Friend WithEvents txtAge As Spectrum.CtrlTextBox
    Friend WithEvents LblAge As Spectrum.CtrlLabel
    Friend WithEvents LblHeight As Spectrum.CtrlLabel
    Friend WithEvents txtHeight As Spectrum.CtrlTextBox
    Friend WithEvents LblWeight As Spectrum.CtrlLabel
    Friend WithEvents txtWeight As Spectrum.CtrlTextBox
    Friend WithEvents LblMobileNo As C1.Win.C1Input.C1Label
    Friend WithEvents txtMobileNo As Spectrum.CtrlTextBox
    Friend WithEvents TreeViewPatient As System.Windows.Forms.TreeView
    Friend WithEvents CtrlFileList As Spectrum.CtrlHeader
    Friend WithEvents CtrlPatientImage1 As Spectrum.CtrlPatientImage

End Class
