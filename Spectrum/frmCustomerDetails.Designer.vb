<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerDetails
    Inherits Spectrum.CtrlPopupForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCustomerDetails))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblCustomerID = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblCustomerType = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCustomerName = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblMobileNumber = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblEmailID = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.rtxtAddress = New System.Windows.Forms.RichTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboAddressType = New C1.Win.C1List.C1Combo()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblGender = New System.Windows.Forms.Label()
        Me.lblBirthDate = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblCardType = New System.Windows.Forms.Label()
        Me.grpBoxCLpCustomerDetails = New System.Windows.Forms.GroupBox()
        Me.lblBalancePoint = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblPointsAccumlated = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmdOK = New Spectrum.CtrlBtn()
        CType(Me.cboAddressType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBoxCLpCustomerDetails.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Customer ID"
        '
        'lblCustomerID
        '
        Me.lblCustomerID.Location = New System.Drawing.Point(184, 23)
        Me.lblCustomerID.Name = "lblCustomerID"
        Me.lblCustomerID.Size = New System.Drawing.Size(278, 14)
        Me.lblCustomerID.TabIndex = 1
        Me.lblCustomerID.Text = "1"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Customer Type"
        '
        'lblCustomerType
        '
        Me.lblCustomerType.Location = New System.Drawing.Point(184, 53)
        Me.lblCustomerType.Name = "lblCustomerType"
        Me.lblCustomerType.Size = New System.Drawing.Size(278, 14)
        Me.lblCustomerType.TabIndex = 3
        Me.lblCustomerType.Text = "1"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(12, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(136, 14)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Customer Name "
        '
        'lblCustomerName
        '
        Me.lblCustomerName.Location = New System.Drawing.Point(184, 83)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(278, 14)
        Me.lblCustomerName.TabIndex = 5
        Me.lblCustomerName.Text = "1"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(12, 113)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(136, 14)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Contact Number "
        '
        'lblMobileNumber
        '
        Me.lblMobileNumber.Location = New System.Drawing.Point(184, 113)
        Me.lblMobileNumber.Name = "lblMobileNumber"
        Me.lblMobileNumber.Size = New System.Drawing.Size(278, 14)
        Me.lblMobileNumber.TabIndex = 7
        Me.lblMobileNumber.Text = "1"
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(12, 203)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(136, 14)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "EmailId"
        '
        'lblEmailID
        '
        Me.lblEmailID.Location = New System.Drawing.Point(184, 203)
        Me.lblEmailID.Name = "lblEmailID"
        Me.lblEmailID.Size = New System.Drawing.Size(278, 14)
        Me.lblEmailID.TabIndex = 9
        Me.lblEmailID.Text = "1"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(12, 234)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(136, 14)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Address Type"
        '
        'rtxtAddress
        '
        Me.rtxtAddress.Location = New System.Drawing.Point(187, 278)
        Me.rtxtAddress.Name = "rtxtAddress"
        Me.rtxtAddress.ReadOnly = True
        Me.rtxtAddress.Size = New System.Drawing.Size(309, 63)
        Me.rtxtAddress.TabIndex = 12
        Me.rtxtAddress.Text = ""
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(12, 278)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(136, 14)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Address  "
        '
        'cboAddressType
        '
        Me.cboAddressType.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboAddressType.AutoSelect = True
        Me.cboAddressType.AutoSize = False
        Me.cboAddressType.Caption = ""
        Me.cboAddressType.CaptionHeight = 17
        Me.cboAddressType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboAddressType.ColumnCaptionHeight = 17
        Me.cboAddressType.ColumnFooterHeight = 17
        Me.cboAddressType.ColumnHeaders = False
        Me.cboAddressType.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList
        Me.cboAddressType.ContentHeight = 24
        Me.cboAddressType.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboAddressType.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboAddressType.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAddressType.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAddressType.EditorHeight = 24
        Me.cboAddressType.Images.Add(CType(resources.GetObject("cboAddressType.Images"), System.Drawing.Image))
        Me.cboAddressType.ItemHeight = 15
        Me.cboAddressType.Location = New System.Drawing.Point(187, 234)
        Me.cboAddressType.MatchEntryTimeout = CType(2000, Long)
        Me.cboAddressType.MaxDropDownItems = CType(5, Short)
        Me.cboAddressType.MaxLength = 32767
        Me.cboAddressType.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboAddressType.Name = "cboAddressType"
        Me.cboAddressType.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboAddressType.Size = New System.Drawing.Size(169, 30)
        Me.cboAddressType.TabIndex = 56
        Me.cboAddressType.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cboAddressType.PropBag = resources.GetString("cboAddressType.PropBag")
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(12, 143)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(136, 14)
        Me.Label8.TabIndex = 57
        Me.Label8.Text = "Gender"
        '
        'lblGender
        '
        Me.lblGender.Location = New System.Drawing.Point(184, 143)
        Me.lblGender.Name = "lblGender"
        Me.lblGender.Size = New System.Drawing.Size(278, 14)
        Me.lblGender.TabIndex = 58
        Me.lblGender.Text = "1"
        '
        'lblBirthDate
        '
        Me.lblBirthDate.Location = New System.Drawing.Point(184, 173)
        Me.lblBirthDate.Name = "lblBirthDate"
        Me.lblBirthDate.Size = New System.Drawing.Size(278, 14)
        Me.lblBirthDate.TabIndex = 59
        Me.lblBirthDate.Text = "1"
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(12, 173)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(136, 14)
        Me.Label10.TabIndex = 60
        Me.Label10.Text = "Birth Date"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 14)
        Me.Label9.TabIndex = 61
        Me.Label9.Text = "Tier Type"
        '
        'lblCardType
        '
        Me.lblCardType.AutoSize = True
        Me.lblCardType.Location = New System.Drawing.Point(175, 17)
        Me.lblCardType.Name = "lblCardType"
        Me.lblCardType.Size = New System.Drawing.Size(25, 14)
        Me.lblCardType.TabIndex = 62
        Me.lblCardType.Text = "00"
        '
        'grpBoxCLpCustomerDetails
        '
        Me.grpBoxCLpCustomerDetails.Controls.Add(Me.lblBalancePoint)
        Me.grpBoxCLpCustomerDetails.Controls.Add(Me.Label12)
        Me.grpBoxCLpCustomerDetails.Controls.Add(Me.lblPointsAccumlated)
        Me.grpBoxCLpCustomerDetails.Controls.Add(Me.Label11)
        Me.grpBoxCLpCustomerDetails.Controls.Add(Me.Label9)
        Me.grpBoxCLpCustomerDetails.Controls.Add(Me.lblCardType)
        Me.grpBoxCLpCustomerDetails.Location = New System.Drawing.Point(119, 347)
        Me.grpBoxCLpCustomerDetails.Name = "grpBoxCLpCustomerDetails"
        Me.grpBoxCLpCustomerDetails.Size = New System.Drawing.Size(271, 94)
        Me.grpBoxCLpCustomerDetails.TabIndex = 63
        Me.grpBoxCLpCustomerDetails.TabStop = False
        '
        'lblBalancePoint
        '
        Me.lblBalancePoint.AutoSize = True
        Me.lblBalancePoint.Location = New System.Drawing.Point(175, 70)
        Me.lblBalancePoint.Name = "lblBalancePoint"
        Me.lblBalancePoint.Size = New System.Drawing.Size(25, 14)
        Me.lblBalancePoint.TabIndex = 66
        Me.lblBalancePoint.Text = "00"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 70)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(97, 14)
        Me.Label12.TabIndex = 65
        Me.Label12.Text = "Balance Point"
        '
        'lblPointsAccumlated
        '
        Me.lblPointsAccumlated.AutoSize = True
        Me.lblPointsAccumlated.Location = New System.Drawing.Point(175, 45)
        Me.lblPointsAccumlated.Name = "lblPointsAccumlated"
        Me.lblPointsAccumlated.Size = New System.Drawing.Size(25, 14)
        Me.lblPointsAccumlated.TabIndex = 64
        Me.lblPointsAccumlated.Text = "00"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 45)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(128, 14)
        Me.Label11.TabIndex = 63
        Me.Label11.Text = "Points Accumlated"
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOK.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdOK.Location = New System.Drawing.Point(224, 460)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.SetArticleCode = Nothing
        Me.cmdOK.SetRowIndex = 0
        Me.cmdOK.Size = New System.Drawing.Size(61, 23)
        Me.cmdOK.TabIndex = 64
        Me.cmdOK.Text = "Close"
        Me.cmdOK.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdOK.UseVisualStyleBackColor = True
        Me.cmdOK.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmCustomerDetails
        '
        Me.ClientSize = New System.Drawing.Size(508, 495)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.grpBoxCLpCustomerDetails)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.lblBirthDate)
        Me.Controls.Add(Me.lblGender)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cboAddressType)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.rtxtAddress)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblEmailID)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lblMobileNumber)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblCustomerName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblCustomerType)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblCustomerID)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmCustomerDetails"
        Me.Text = "Customer Details"
        CType(Me.cboAddressType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBoxCLpCustomerDetails.ResumeLayout(False)
        Me.grpBoxCLpCustomerDetails.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblCustomerID As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblCustomerType As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblCustomerName As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblMobileNumber As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblEmailID As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents rtxtAddress As System.Windows.Forms.RichTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboAddressType As C1.Win.C1List.C1Combo
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblGender As System.Windows.Forms.Label
    Friend WithEvents lblBirthDate As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblCardType As System.Windows.Forms.Label
    Friend WithEvents grpBoxCLpCustomerDetails As System.Windows.Forms.GroupBox
    Friend WithEvents lblBalancePoint As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblPointsAccumlated As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmdOK As Spectrum.CtrlBtn

End Class
