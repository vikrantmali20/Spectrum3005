<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHCPatientRegistration
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHCPatientRegistration))
        Me.C1Sizer2main = New C1.Win.C1Sizer.C1Sizer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        Me.C1Sizer1 = New C1.Win.C1Sizer.C1Sizer()
        Me.C1Sizer4 = New C1.Win.C1Sizer.C1Sizer()
        Me.BtnDeleteRegn = New Spectrum.CtrlBtn()
        Me.BtnCloseRegn = New Spectrum.CtrlBtn()
        Me.BtnEditRegn = New Spectrum.CtrlBtn()
        Me.BtnNewRegn = New Spectrum.CtrlBtn()
        Me.TabPatientMain = New Spectrum.CtrlTab()
        Me.TabPageAddressInfo = New C1.Win.C1Command.C1DockingTabPage()
        Me.C1Sizer3 = New C1.Win.C1Sizer.C1Sizer()
        Me.GroupBoxPermanentAddress = New System.Windows.Forms.GroupBox()
        Me.C1Label47 = New Spectrum.CtrlLabel()
        Me.C1Label48 = New Spectrum.CtrlLabel()
        Me.txtPPincode = New Spectrum.CtrlTextBox()
        Me.cmbPStayMonths = New Spectrum.ctrlCombo()
        Me.cmbPStayYears = New Spectrum.ctrlCombo()
        Me.C1Label39 = New Spectrum.CtrlLabel()
        Me.chkCopyAddress = New System.Windows.Forms.CheckBox()
        Me.cmbPCity = New Spectrum.ctrlCombo()
        Me.cmbPState = New Spectrum.ctrlCombo()
        Me.cmbPCountry = New Spectrum.ctrlCombo()
        Me.C1Label14 = New Spectrum.CtrlLabel()
        Me.C1Label15 = New Spectrum.CtrlLabel()
        Me.C1Label16 = New Spectrum.CtrlLabel()
        Me.C1Label17 = New Spectrum.CtrlLabel()
        Me.txtPAddressLn2 = New Spectrum.CtrlTextBox()
        Me.txtPAddressLn1 = New Spectrum.CtrlTextBox()
        Me.C1Label18 = New Spectrum.CtrlLabel()
        Me.C1Label19 = New Spectrum.CtrlLabel()
        Me.GroupBoxLocalAddress = New System.Windows.Forms.GroupBox()
        Me.C1Label46 = New Spectrum.CtrlLabel()
        Me.C1Label45 = New Spectrum.CtrlLabel()
        Me.txtLPincode = New Spectrum.CtrlTextBox()
        Me.cmbLStayMonths = New Spectrum.ctrlCombo()
        Me.cmbLStayYears = New Spectrum.ctrlCombo()
        Me.C1Label40 = New Spectrum.CtrlLabel()
        Me.C1Label13 = New Spectrum.CtrlLabel()
        Me.C1Label10 = New Spectrum.CtrlLabel()
        Me.C1Label11 = New Spectrum.CtrlLabel()
        Me.C1Label12 = New Spectrum.CtrlLabel()
        Me.C1Label9 = New Spectrum.CtrlLabel()
        Me.C1Label8 = New Spectrum.CtrlLabel()
        Me.cmbLCity = New Spectrum.ctrlCombo()
        Me.cmbLState = New Spectrum.ctrlCombo()
        Me.cmbLCountry = New Spectrum.ctrlCombo()
        Me.txtLAddressLn2 = New Spectrum.CtrlTextBox()
        Me.txtLAddressLn1 = New Spectrum.CtrlTextBox()
        Me.TabPagePatientInfo = New C1.Win.C1Command.C1DockingTabPage()
        Me.C1Sizer5 = New C1.Win.C1Sizer.C1Sizer()
        Me.GroupBoxOtherInformation = New System.Windows.Forms.GroupBox()
        Me.txtMonthlyIncome = New Spectrum.CtrlTextBox()
        Me.cmbMotherTounge = New Spectrum.ctrlCombo()
        Me.C1Label43 = New Spectrum.CtrlLabel()
        Me.C1Label26 = New Spectrum.CtrlLabel()
        Me.C1Label6 = New Spectrum.CtrlLabel()
        Me.txtNationality = New Spectrum.CtrlTextBox()
        Me.txtNearestRelative = New Spectrum.CtrlTextBox()
        Me.C1Label32 = New Spectrum.CtrlLabel()
        Me.cmbRelation = New Spectrum.ctrlCombo()
        Me.txtRelation = New Spectrum.CtrlTextBox()
        Me.txtReligon = New Spectrum.CtrlTextBox()
        Me.C1Label31 = New Spectrum.CtrlLabel()
        Me.cmbMaritalStatus = New Spectrum.ctrlCombo()
        Me.C1Label21 = New Spectrum.CtrlLabel()
        Me.C1Label28 = New Spectrum.CtrlLabel()
        Me.cmbQualification = New Spectrum.ctrlCombo()
        Me.cmbOccupation = New Spectrum.ctrlCombo()
        Me.C1Label22 = New Spectrum.CtrlLabel()
        Me.C1Label27 = New Spectrum.CtrlLabel()
        Me.groupboxContactDetails = New System.Windows.Forms.GroupBox()
        Me.C1Label44 = New Spectrum.CtrlLabel()
        Me.txtOffPhone = New Spectrum.CtrlTextBox()
        Me.C1Label20 = New Spectrum.CtrlLabel()
        Me.txtEmail = New Spectrum.CtrlTextBox()
        Me.C1Label23 = New Spectrum.CtrlLabel()
        Me.txtResPhone = New Spectrum.CtrlTextBox()
        Me.txtMobile = New Spectrum.CtrlTextBox()
        Me.C1Label24 = New Spectrum.CtrlLabel()
        Me.C1Label25 = New Spectrum.CtrlLabel()
        Me.TabPageDocsInfo = New C1.Win.C1Command.C1DockingTabPage()
        Me.C1Sizer7 = New C1.Win.C1Sizer.C1Sizer()
        Me.GroupBoxDocuments = New System.Windows.Forms.GroupBox()
        Me.txtAuthor = New Spectrum.CtrlTextBox()
        Me.BtnSearchAuthor = New Spectrum.CtrlBtn()
        Me.dtpDocDate = New Spectrum.ctrlDate()
        Me.TreeViewDocs = New System.Windows.Forms.TreeView()
        Me.BtnNewDoc = New Spectrum.CtrlBtn()
        Me.cmbDocumentType = New Spectrum.ctrlCombo()
        Me.BtnUploadDoc = New Spectrum.CtrlBtn()
        Me.C1Label38 = New Spectrum.CtrlLabel()
        Me.txtFilePath = New Spectrum.CtrlTextBox()
        Me.C1Label33 = New Spectrum.CtrlLabel()
        Me.C1Label35 = New Spectrum.CtrlLabel()
        Me.txtDocDescription = New Spectrum.CtrlTextBox()
        Me.C1Label36 = New Spectrum.CtrlLabel()
        Me.C1Label37 = New Spectrum.CtrlLabel()
        Me.BtnDeleteDoc = New Spectrum.CtrlBtn()
        Me.BtnAddDoc = New Spectrum.CtrlBtn()
        Me.C1Sizer2 = New C1.Win.C1Sizer.C1Sizer()
        Me.GroupBoxPatientDetails = New System.Windows.Forms.GroupBox()
        Me.C1SizerPatientInfo = New C1.Win.C1Sizer.C1Sizer()
        Me.CtrlLabel3 = New Spectrum.CtrlLabel()
        Me.C1Label34 = New Spectrum.CtrlLabel()
        Me.CtrlLabel2 = New Spectrum.CtrlLabel()
        Me.C1Label30 = New Spectrum.CtrlLabel()
        Me.C1Label42 = New Spectrum.CtrlLabel()
        Me.C1Label41 = New Spectrum.CtrlLabel()
        Me.C1Label29 = New Spectrum.CtrlLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chckKaff = New System.Windows.Forms.CheckBox()
        Me.chckPitta = New System.Windows.Forms.CheckBox()
        Me.chckVaat = New System.Windows.Forms.CheckBox()
        Me.cmbPatientBloodGroup = New Spectrum.ctrlCombo()
        Me.C1Label50 = New Spectrum.CtrlLabel()
        Me.C1Label49 = New Spectrum.CtrlLabel()
        Me.txtPatientID = New Spectrum.CtrlTextBox()
        Me.txtHeightCm = New Spectrum.CtrlTextBox()
        Me.txtWeightKg = New Spectrum.CtrlTextBox()
        Me.dtpDob = New Spectrum.ctrlDate()
        Me.txtRefDoctorName = New Spectrum.CtrlTextBox()
        Me.lblPatientidname = New Spectrum.CtrlLabel()
        Me.cboGender = New Spectrum.ctrlCombo()
        Me.cmbAgeMonths = New Spectrum.ctrlCombo()
        Me.cmbAgeYears = New Spectrum.ctrlCombo()
        Me.C1Label1 = New Spectrum.CtrlLabel()
        Me.BtnSearchPatient = New Spectrum.CtrlBtn()
        Me.cmbSalutation = New Spectrum.ctrlCombo()
        Me.BtnNewRefDr = New Spectrum.CtrlBtn()
        Me.C1Label2 = New Spectrum.CtrlLabel()
        Me.BtnSearchRefDr = New Spectrum.CtrlBtn()
        Me.s = New Spectrum.CtrlLabel()
        Me.txtFirstName = New Spectrum.CtrlTextBox()
        Me.C1Label5 = New Spectrum.CtrlLabel()
        Me.C1Label4 = New Spectrum.CtrlLabel()
        Me.txtMiddleName = New Spectrum.CtrlTextBox()
        Me.C1Label7 = New Spectrum.CtrlLabel()
        Me.C1Label3 = New Spectrum.CtrlLabel()
        Me.txtLastName = New Spectrum.CtrlTextBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.BtnClearImage = New Spectrum.CtrlBtn()
        Me.pbPhoto = New C1.Win.C1Input.C1PictureBox()
        Me.BtnUploadImage = New Spectrum.CtrlBtn()
        CType(Me.C1Sizer2main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer2main.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer1.SuspendLayout()
        CType(Me.C1Sizer4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer4.SuspendLayout()
        CType(Me.TabPatientMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPatientMain.SuspendLayout()
        Me.TabPageAddressInfo.SuspendLayout()
        CType(Me.C1Sizer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer3.SuspendLayout()
        Me.GroupBoxPermanentAddress.SuspendLayout()
        CType(Me.C1Label47, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label48, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPPincode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPStayMonths, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPStayYears, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPAddressLn2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPAddressLn1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label19, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxLocalAddress.SuspendLayout()
        CType(Me.C1Label46, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label45, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLPincode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbLStayMonths, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbLStayYears, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbLCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbLState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbLCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLAddressLn2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLAddressLn1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPagePatientInfo.SuspendLayout()
        CType(Me.C1Sizer5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer5.SuspendLayout()
        Me.GroupBoxOtherInformation.SuspendLayout()
        CType(Me.txtMonthlyIncome, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbMotherTounge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNationality, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNearestRelative, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbRelation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRelation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReligon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbMaritalStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbQualification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbOccupation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label27, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupboxContactDetails.SuspendLayout()
        CType(Me.C1Label44, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOffPhone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtResPhone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMobile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label25, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageDocsInfo.SuspendLayout()
        CType(Me.C1Sizer7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer7.SuspendLayout()
        Me.GroupBoxDocuments.SuspendLayout()
        CType(Me.txtAuthor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDocDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbDocumentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFilePath, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDocDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label36, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Sizer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer2.SuspendLayout()
        Me.GroupBoxPatientDetails.SuspendLayout()
        CType(Me.C1SizerPatientInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1SizerPatientInfo.SuspendLayout()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label42, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label29, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.cmbPatientBloodGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label50, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label49, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPatientID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHeightCm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWeightKg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpDob, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRefDoctorName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPatientidname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboGender, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbAgeMonths, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbAgeYears, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSalutation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.s, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFirstName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMiddleName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Label3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLastName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        CType(Me.pbPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'C1Sizer2main
        '
        Me.C1Sizer2main.BackColor = System.Drawing.Color.Gray
        Me.C1Sizer2main.Controls.Add(Me.Panel1)
        Me.C1Sizer2main.Controls.Add(Me.C1Sizer1)
        Me.C1Sizer2main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Sizer2main.GridDefinition = "4.55284552845528:False:False;93.4959349593496:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "98.936170212766:False:" & _
    "False;"
        Me.C1Sizer2main.Location = New System.Drawing.Point(0, 0)
        Me.C1Sizer2main.Name = "C1Sizer2main"
        Me.C1Sizer2main.Padding = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.C1Sizer2main.Size = New System.Drawing.Size(940, 615)
        Me.C1Sizer2main.TabIndex = 2
        Me.C1Sizer2main.TabStop = False
        Me.C1Sizer2main.Text = "C1Sizer2"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Gray
        Me.Panel1.Controls.Add(Me.CtrlLabel1)
        Me.Panel1.Location = New System.Drawing.Point(5, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(930, 28)
        Me.Panel1.TabIndex = 0
        '
        'CtrlLabel1
        '
        Me.CtrlLabel1.AttachedTextBoxName = Nothing
        Me.CtrlLabel1.AutoSize = True
        Me.CtrlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel1.BorderColor = System.Drawing.Color.Transparent
        Me.CtrlLabel1.Font = New System.Drawing.Font("Verdana", 9.25!, System.Drawing.FontStyle.Bold)
        Me.CtrlLabel1.ForeColor = System.Drawing.Color.White
        Me.CtrlLabel1.Location = New System.Drawing.Point(364, 5)
        Me.CtrlLabel1.Name = "CtrlLabel1"
        Me.CtrlLabel1.Size = New System.Drawing.Size(152, 16)
        Me.CtrlLabel1.TabIndex = 0
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.Text = "Patient Registration"
        Me.CtrlLabel1.TextDetached = True
        Me.CtrlLabel1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Sizer1
        '
        Me.C1Sizer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C1Sizer1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.C1Sizer1.Controls.Add(Me.C1Sizer4)
        Me.C1Sizer1.Controls.Add(Me.TabPatientMain)
        Me.C1Sizer1.Controls.Add(Me.C1Sizer2)
        Me.C1Sizer1.GridDefinition = "38.4347826086956:False:True;56.5217391304348:False:True;6.78260869565217:False:Tr" & _
    "ue;" & Global.Microsoft.VisualBasic.ChrW(9) & "98.9247311827957:False:False;"
        Me.C1Sizer1.Location = New System.Drawing.Point(5, 36)
        Me.C1Sizer1.Name = "C1Sizer1"
        Me.C1Sizer1.Padding = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.C1Sizer1.Size = New System.Drawing.Size(930, 575)
        Me.C1Sizer1.TabIndex = 1
        Me.C1Sizer1.Text = "C1Sizer1"
        '
        'C1Sizer4
        '
        Me.C1Sizer4.BackColor = System.Drawing.SystemColors.ControlLight
        Me.C1Sizer4.Controls.Add(Me.BtnDeleteRegn)
        Me.C1Sizer4.Controls.Add(Me.BtnCloseRegn)
        Me.C1Sizer4.Controls.Add(Me.BtnEditRegn)
        Me.C1Sizer4.Controls.Add(Me.BtnNewRegn)
        Me.C1Sizer4.GridDefinition = "79.4871794871795:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "19.4565217391304:False:False;19.3478260869565:False" & _
    ":False;19.5652173913043:False:False;19.3478260869565:False:False;19.456521739130" & _
    "4:False:False;"
        Me.C1Sizer4.Location = New System.Drawing.Point(5, 558)
        Me.C1Sizer4.Name = "C1Sizer4"
        Me.C1Sizer4.Padding = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.C1Sizer4.Size = New System.Drawing.Size(920, 39)
        Me.C1Sizer4.TabIndex = 2
        Me.C1Sizer4.Text = "C1Sizer4"
        '
        'BtnDeleteRegn
        '
        Me.BtnDeleteRegn.BackColor = System.Drawing.Color.DimGray
        Me.BtnDeleteRegn.FlatAppearance.BorderSize = 0
        Me.BtnDeleteRegn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDeleteRegn.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDeleteRegn.ForeColor = System.Drawing.Color.White
        Me.BtnDeleteRegn.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnDeleteRegn.Location = New System.Drawing.Point(554, 4)
        Me.BtnDeleteRegn.MoveToNxtCtrl = Nothing
        Me.BtnDeleteRegn.Name = "BtnDeleteRegn"
        Me.BtnDeleteRegn.SetArticleCode = Nothing
        Me.BtnDeleteRegn.SetRowIndex = 0
        Me.BtnDeleteRegn.Size = New System.Drawing.Size(178, 31)
        Me.BtnDeleteRegn.TabIndex = 2
        Me.BtnDeleteRegn.Tag = "Delete"
        Me.BtnDeleteRegn.Text = "Delete"
        Me.BtnDeleteRegn.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnDeleteRegn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnDeleteRegn.UseVisualStyleBackColor = False
        Me.BtnDeleteRegn.Visible = False
        Me.BtnDeleteRegn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnCloseRegn
        '
        Me.BtnCloseRegn.BackColor = System.Drawing.Color.DimGray
        Me.BtnCloseRegn.FlatAppearance.BorderSize = 0
        Me.BtnCloseRegn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCloseRegn.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCloseRegn.ForeColor = System.Drawing.Color.White
        Me.BtnCloseRegn.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCloseRegn.Location = New System.Drawing.Point(736, 4)
        Me.BtnCloseRegn.MoveToNxtCtrl = Nothing
        Me.BtnCloseRegn.Name = "BtnCloseRegn"
        Me.BtnCloseRegn.SetArticleCode = Nothing
        Me.BtnCloseRegn.SetRowIndex = 0
        Me.BtnCloseRegn.Size = New System.Drawing.Size(179, 31)
        Me.BtnCloseRegn.TabIndex = 3
        Me.BtnCloseRegn.Tag = "Close"
        Me.BtnCloseRegn.Text = "Close"
        Me.BtnCloseRegn.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnCloseRegn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnCloseRegn.UseVisualStyleBackColor = False
        Me.BtnCloseRegn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnEditRegn
        '
        Me.BtnEditRegn.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEditRegn.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnEditRegn.Location = New System.Drawing.Point(188, 4)
        Me.BtnEditRegn.MoveToNxtCtrl = Nothing
        Me.BtnEditRegn.Name = "BtnEditRegn"
        Me.BtnEditRegn.SetArticleCode = Nothing
        Me.BtnEditRegn.SetRowIndex = 0
        Me.BtnEditRegn.Size = New System.Drawing.Size(178, 31)
        Me.BtnEditRegn.TabIndex = 0
        Me.BtnEditRegn.Tag = "Edit"
        Me.BtnEditRegn.Text = "Edit"
        Me.BtnEditRegn.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnEditRegn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnEditRegn.UseVisualStyleBackColor = True
        Me.BtnEditRegn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnNewRegn
        '
        Me.BtnNewRegn.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnNewRegn.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnNewRegn.Location = New System.Drawing.Point(5, 4)
        Me.BtnNewRegn.MoveToNxtCtrl = Nothing
        Me.BtnNewRegn.Name = "BtnNewRegn"
        Me.BtnNewRegn.SetArticleCode = Nothing
        Me.BtnNewRegn.SetRowIndex = 0
        Me.BtnNewRegn.Size = New System.Drawing.Size(179, 31)
        Me.BtnNewRegn.TabIndex = 1
        Me.BtnNewRegn.Tag = "New"
        Me.BtnNewRegn.Text = "New"
        Me.BtnNewRegn.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnNewRegn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnNewRegn.UseVisualStyleBackColor = True
        Me.BtnNewRegn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'TabPatientMain
        '
        Me.TabPatientMain.BackColor = System.Drawing.Color.Silver
        Me.TabPatientMain.Controls.Add(Me.TabPageAddressInfo)
        Me.TabPatientMain.Controls.Add(Me.TabPagePatientInfo)
        Me.TabPatientMain.Controls.Add(Me.TabPageDocsInfo)
        Me.TabPatientMain.Location = New System.Drawing.Point(5, 229)
        Me.TabPatientMain.Name = "TabPatientMain"
        Me.TabPatientMain.Size = New System.Drawing.Size(920, 325)
        Me.TabPatientMain.TabIndex = 1
        Me.TabPatientMain.TabsSpacing = -10
        Me.TabPatientMain.TabStyle = C1.Win.C1Command.TabStyleEnum.Sloping
        Me.TabPatientMain.VisualStyle = C1.Win.C1Command.VisualStyle.Custom
        Me.TabPatientMain.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP
        '
        'TabPageAddressInfo
        '
        Me.TabPageAddressInfo.BackColor = System.Drawing.Color.Silver
        Me.TabPageAddressInfo.Controls.Add(Me.C1Sizer3)
        Me.TabPageAddressInfo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPageAddressInfo.Location = New System.Drawing.Point(1, 25)
        Me.TabPageAddressInfo.Name = "TabPageAddressInfo"
        Me.TabPageAddressInfo.Size = New System.Drawing.Size(918, 299)
        Me.TabPageAddressInfo.TabBackColor = System.Drawing.Color.Gray
        Me.TabPageAddressInfo.TabBackColorSelected = System.Drawing.Color.Green
        Me.TabPageAddressInfo.TabForeColor = System.Drawing.Color.White
        Me.TabPageAddressInfo.TabForeColorSelected = System.Drawing.Color.White
        Me.TabPageAddressInfo.TabIndex = 14
        Me.TabPageAddressInfo.TabStop = False
        Me.TabPageAddressInfo.Text = "Address Details"
        '
        'C1Sizer3
        '
        Me.C1Sizer3.BackColor = System.Drawing.Color.White
        Me.C1Sizer3.Controls.Add(Me.GroupBoxPermanentAddress)
        Me.C1Sizer3.Controls.Add(Me.GroupBoxLocalAddress)
        Me.C1Sizer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Sizer3.GridDefinition = "97.3244147157191:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "49.4553376906318:False:False;49.0196078431373:False" & _
    ":False;"
        Me.C1Sizer3.Location = New System.Drawing.Point(0, 0)
        Me.C1Sizer3.Name = "C1Sizer3"
        Me.C1Sizer3.Padding = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.C1Sizer3.Size = New System.Drawing.Size(918, 299)
        Me.C1Sizer3.TabIndex = 0
        Me.C1Sizer3.Text = "C1Sizer3"
        '
        'GroupBoxPermanentAddress
        '
        Me.GroupBoxPermanentAddress.BackColor = System.Drawing.Color.White
        Me.GroupBoxPermanentAddress.Controls.Add(Me.C1Label47)
        Me.GroupBoxPermanentAddress.Controls.Add(Me.C1Label48)
        Me.GroupBoxPermanentAddress.Controls.Add(Me.txtPPincode)
        Me.GroupBoxPermanentAddress.Controls.Add(Me.cmbPStayMonths)
        Me.GroupBoxPermanentAddress.Controls.Add(Me.cmbPStayYears)
        Me.GroupBoxPermanentAddress.Controls.Add(Me.C1Label39)
        Me.GroupBoxPermanentAddress.Controls.Add(Me.chkCopyAddress)
        Me.GroupBoxPermanentAddress.Controls.Add(Me.cmbPCity)
        Me.GroupBoxPermanentAddress.Controls.Add(Me.cmbPState)
        Me.GroupBoxPermanentAddress.Controls.Add(Me.cmbPCountry)
        Me.GroupBoxPermanentAddress.Controls.Add(Me.C1Label14)
        Me.GroupBoxPermanentAddress.Controls.Add(Me.C1Label15)
        Me.GroupBoxPermanentAddress.Controls.Add(Me.C1Label16)
        Me.GroupBoxPermanentAddress.Controls.Add(Me.C1Label17)
        Me.GroupBoxPermanentAddress.Controls.Add(Me.txtPAddressLn2)
        Me.GroupBoxPermanentAddress.Controls.Add(Me.txtPAddressLn1)
        Me.GroupBoxPermanentAddress.Controls.Add(Me.C1Label18)
        Me.GroupBoxPermanentAddress.Controls.Add(Me.C1Label19)
        Me.GroupBoxPermanentAddress.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxPermanentAddress.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.GroupBoxPermanentAddress.Location = New System.Drawing.Point(463, 4)
        Me.GroupBoxPermanentAddress.Name = "GroupBoxPermanentAddress"
        Me.GroupBoxPermanentAddress.Size = New System.Drawing.Size(450, 291)
        Me.GroupBoxPermanentAddress.TabIndex = 3
        Me.GroupBoxPermanentAddress.TabStop = False
        Me.GroupBoxPermanentAddress.Text = "Permanent Address"
        '
        'C1Label47
        '
        Me.C1Label47.AttachedTextBoxName = Nothing
        Me.C1Label47.AutoSize = True
        Me.C1Label47.BackColor = System.Drawing.Color.Transparent
        Me.C1Label47.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label47.ForeColor = System.Drawing.Color.Black
        Me.C1Label47.Location = New System.Drawing.Point(342, 203)
        Me.C1Label47.Name = "C1Label47"
        Me.C1Label47.Size = New System.Drawing.Size(47, 13)
        Me.C1Label47.TabIndex = 45
        Me.C1Label47.Tag = Nothing
        Me.C1Label47.Text = "Months"
        Me.C1Label47.TextDetached = True
        '
        'C1Label48
        '
        Me.C1Label48.AttachedTextBoxName = Nothing
        Me.C1Label48.AutoSize = True
        Me.C1Label48.BackColor = System.Drawing.Color.Transparent
        Me.C1Label48.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label48.ForeColor = System.Drawing.Color.Black
        Me.C1Label48.Location = New System.Drawing.Point(216, 203)
        Me.C1Label48.Name = "C1Label48"
        Me.C1Label48.Size = New System.Drawing.Size(39, 13)
        Me.C1Label48.TabIndex = 44
        Me.C1Label48.Tag = Nothing
        Me.C1Label48.Text = "Years"
        Me.C1Label48.TextDetached = True
        '
        'txtPPincode
        '
        Me.txtPPincode.AcceptsTab = True
        Me.txtPPincode.AutoSize = False
        Me.txtPPincode.BackColor = System.Drawing.Color.White
        Me.txtPPincode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPPincode.DataType = GetType(Double)
        Me.txtPPincode.EmptyAsNull = True
        Me.txtPPincode.FormatType = C1.Win.C1Input.FormatTypeEnum.GeneralNumber
        Me.txtPPincode.Location = New System.Drawing.Point(141, 172)
        Me.txtPPincode.MaxLength = 10
        Me.txtPPincode.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtPPincode.MoveToNxtCtrl = Nothing
        Me.txtPPincode.Name = "txtPPincode"
        Me.txtPPincode.Size = New System.Drawing.Size(248, 21)
        Me.txtPPincode.TabIndex = 6
        Me.txtPPincode.Tag = Nothing
        Me.txtPPincode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmbPStayMonths
        '
        Me.cmbPStayMonths.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbPStayMonths.AutoCompletion = True
        Me.cmbPStayMonths.AutoDropDown = True
        Me.cmbPStayMonths.Caption = ""
        Me.cmbPStayMonths.CaptionHeight = 17
        Me.cmbPStayMonths.CaptionVisible = False
        Me.cmbPStayMonths.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbPStayMonths.ColumnCaptionHeight = 17
        Me.cmbPStayMonths.ColumnFooterHeight = 17
        Me.cmbPStayMonths.ColumnHeaders = False
        Me.cmbPStayMonths.ColumnWidth = 100
        Me.cmbPStayMonths.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList
        Me.cmbPStayMonths.ContentHeight = 16
        Me.cmbPStayMonths.ctrlTextDbColumn = ""
        Me.cmbPStayMonths.DataMode = C1.Win.C1List.DataModeEnum.AddItem
        Me.cmbPStayMonths.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbPStayMonths.EditorBackColor = System.Drawing.Color.White
        Me.cmbPStayMonths.EditorFont = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmbPStayMonths.EditorForeColor = System.Drawing.Color.Black
        Me.cmbPStayMonths.EditorHeight = 16
        Me.cmbPStayMonths.Images.Add(CType(resources.GetObject("cmbPStayMonths.Images"), System.Drawing.Image))
        Me.cmbPStayMonths.ItemHeight = 15
        Me.cmbPStayMonths.LimitToList = True
        Me.cmbPStayMonths.Location = New System.Drawing.Point(268, 200)
        Me.cmbPStayMonths.MatchEntryTimeout = CType(2000, Long)
        Me.cmbPStayMonths.MaxDropDownItems = CType(5, Short)
        Me.cmbPStayMonths.MaxLength = 32767
        Me.cmbPStayMonths.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbPStayMonths.MoveToNxtCtrl = Nothing
        Me.cmbPStayMonths.Name = "cmbPStayMonths"
        Me.cmbPStayMonths.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbPStayMonths.Size = New System.Drawing.Size(68, 22)
        Me.cmbPStayMonths.strSelectStmt = ""
        Me.cmbPStayMonths.SuperBack = True
        Me.cmbPStayMonths.TabIndex = 8
        Me.cmbPStayMonths.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbPStayMonths.PropBag = resources.GetString("cmbPStayMonths.PropBag")
        '
        'cmbPStayYears
        '
        Me.cmbPStayYears.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbPStayYears.AutoCompletion = True
        Me.cmbPStayYears.AutoDropDown = True
        Me.cmbPStayYears.Caption = ""
        Me.cmbPStayYears.CaptionHeight = 17
        Me.cmbPStayYears.CaptionVisible = False
        Me.cmbPStayYears.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbPStayYears.ColumnCaptionHeight = 17
        Me.cmbPStayYears.ColumnFooterHeight = 17
        Me.cmbPStayYears.ColumnHeaders = False
        Me.cmbPStayYears.ColumnWidth = 100
        Me.cmbPStayYears.ContentHeight = 16
        Me.cmbPStayYears.ctrlTextDbColumn = ""
        Me.cmbPStayYears.DataMode = C1.Win.C1List.DataModeEnum.AddItem
        Me.cmbPStayYears.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbPStayYears.EditorBackColor = System.Drawing.Color.White
        Me.cmbPStayYears.EditorFont = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmbPStayYears.EditorForeColor = System.Drawing.Color.Black
        Me.cmbPStayYears.EditorHeight = 16
        Me.cmbPStayYears.Images.Add(CType(resources.GetObject("cmbPStayYears.Images"), System.Drawing.Image))
        Me.cmbPStayYears.ItemHeight = 15
        Me.cmbPStayYears.LimitToList = True
        Me.cmbPStayYears.Location = New System.Drawing.Point(141, 199)
        Me.cmbPStayYears.MatchEntryTimeout = CType(2000, Long)
        Me.cmbPStayYears.MaxDropDownItems = CType(5, Short)
        Me.cmbPStayYears.MaxLength = 32767
        Me.cmbPStayYears.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbPStayYears.MoveToNxtCtrl = Nothing
        Me.cmbPStayYears.Name = "cmbPStayYears"
        Me.cmbPStayYears.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbPStayYears.Size = New System.Drawing.Size(69, 22)
        Me.cmbPStayYears.strSelectStmt = ""
        Me.cmbPStayYears.SuperBack = True
        Me.cmbPStayYears.TabIndex = 7
        Me.cmbPStayYears.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbPStayYears.PropBag = resources.GetString("cmbPStayYears.PropBag")
        '
        'C1Label39
        '
        Me.C1Label39.AttachedTextBoxName = Nothing
        Me.C1Label39.BackColor = System.Drawing.Color.Transparent
        Me.C1Label39.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label39.ForeColor = System.Drawing.Color.Black
        Me.C1Label39.Location = New System.Drawing.Point(28, 199)
        Me.C1Label39.Name = "C1Label39"
        Me.C1Label39.Size = New System.Drawing.Size(105, 13)
        Me.C1Label39.TabIndex = 25
        Me.C1Label39.Tag = Nothing
        Me.C1Label39.Text = "Stay Duration"
        Me.C1Label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label39.TextDetached = True
        '
        'chkCopyAddress
        '
        Me.chkCopyAddress.AutoSize = True
        Me.chkCopyAddress.ForeColor = System.Drawing.Color.Black
        Me.chkCopyAddress.Location = New System.Drawing.Point(141, 20)
        Me.chkCopyAddress.Name = "chkCopyAddress"
        Me.chkCopyAddress.Size = New System.Drawing.Size(159, 17)
        Me.chkCopyAddress.TabIndex = 0
        Me.chkCopyAddress.Text = "Same as Local Address"
        Me.chkCopyAddress.UseVisualStyleBackColor = True
        '
        'cmbPCity
        '
        Me.cmbPCity.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbPCity.AutoCompletion = True
        Me.cmbPCity.AutoDropDown = True
        Me.cmbPCity.Caption = ""
        Me.cmbPCity.CaptionHeight = 17
        Me.cmbPCity.CaptionVisible = False
        Me.cmbPCity.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbPCity.ColumnCaptionHeight = 17
        Me.cmbPCity.ColumnFooterHeight = 17
        Me.cmbPCity.ColumnHeaders = False
        Me.cmbPCity.ContentHeight = 16
        Me.cmbPCity.ctrlTextDbColumn = ""
        Me.cmbPCity.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbPCity.EditorBackColor = System.Drawing.Color.White
        Me.cmbPCity.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPCity.EditorForeColor = System.Drawing.Color.Black
        Me.cmbPCity.EditorHeight = 16
        Me.cmbPCity.Images.Add(CType(resources.GetObject("cmbPCity.Images"), System.Drawing.Image))
        Me.cmbPCity.ItemHeight = 15
        Me.cmbPCity.LimitToList = True
        Me.cmbPCity.Location = New System.Drawing.Point(141, 145)
        Me.cmbPCity.MatchEntryTimeout = CType(2000, Long)
        Me.cmbPCity.MaxDropDownItems = CType(5, Short)
        Me.cmbPCity.MaxLength = 32767
        Me.cmbPCity.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbPCity.MoveToNxtCtrl = Nothing
        Me.cmbPCity.Name = "cmbPCity"
        Me.cmbPCity.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbPCity.Size = New System.Drawing.Size(248, 22)
        Me.cmbPCity.strSelectStmt = ""
        Me.cmbPCity.SuperBack = True
        Me.cmbPCity.TabIndex = 5
        Me.cmbPCity.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbPCity.PropBag = resources.GetString("cmbPCity.PropBag")
        '
        'cmbPState
        '
        Me.cmbPState.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbPState.AutoCompletion = True
        Me.cmbPState.AutoDropDown = True
        Me.cmbPState.Caption = ""
        Me.cmbPState.CaptionHeight = 17
        Me.cmbPState.CaptionVisible = False
        Me.cmbPState.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbPState.ColumnCaptionHeight = 17
        Me.cmbPState.ColumnFooterHeight = 17
        Me.cmbPState.ColumnHeaders = False
        Me.cmbPState.ContentHeight = 16
        Me.cmbPState.ctrlTextDbColumn = ""
        Me.cmbPState.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbPState.EditorBackColor = System.Drawing.Color.White
        Me.cmbPState.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPState.EditorForeColor = System.Drawing.Color.Black
        Me.cmbPState.EditorHeight = 16
        Me.cmbPState.Images.Add(CType(resources.GetObject("cmbPState.Images"), System.Drawing.Image))
        Me.cmbPState.ItemHeight = 15
        Me.cmbPState.LimitToList = True
        Me.cmbPState.Location = New System.Drawing.Point(141, 118)
        Me.cmbPState.MatchEntryTimeout = CType(2000, Long)
        Me.cmbPState.MaxDropDownItems = CType(5, Short)
        Me.cmbPState.MaxLength = 32767
        Me.cmbPState.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbPState.MoveToNxtCtrl = Nothing
        Me.cmbPState.Name = "cmbPState"
        Me.cmbPState.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbPState.Size = New System.Drawing.Size(248, 22)
        Me.cmbPState.strSelectStmt = ""
        Me.cmbPState.SuperBack = True
        Me.cmbPState.TabIndex = 4
        Me.cmbPState.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbPState.PropBag = resources.GetString("cmbPState.PropBag")
        '
        'cmbPCountry
        '
        Me.cmbPCountry.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbPCountry.AutoCompletion = True
        Me.cmbPCountry.AutoDropDown = True
        Me.cmbPCountry.Caption = ""
        Me.cmbPCountry.CaptionHeight = 17
        Me.cmbPCountry.CaptionVisible = False
        Me.cmbPCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbPCountry.ColumnCaptionHeight = 17
        Me.cmbPCountry.ColumnFooterHeight = 17
        Me.cmbPCountry.ColumnHeaders = False
        Me.cmbPCountry.ContentHeight = 16
        Me.cmbPCountry.ctrlTextDbColumn = ""
        Me.cmbPCountry.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbPCountry.EditorBackColor = System.Drawing.Color.White
        Me.cmbPCountry.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPCountry.EditorForeColor = System.Drawing.Color.Black
        Me.cmbPCountry.EditorHeight = 16
        Me.cmbPCountry.Images.Add(CType(resources.GetObject("cmbPCountry.Images"), System.Drawing.Image))
        Me.cmbPCountry.ItemHeight = 15
        Me.cmbPCountry.LimitToList = True
        Me.cmbPCountry.Location = New System.Drawing.Point(141, 92)
        Me.cmbPCountry.MatchEntryTimeout = CType(2000, Long)
        Me.cmbPCountry.MaxDropDownItems = CType(5, Short)
        Me.cmbPCountry.MaxLength = 32767
        Me.cmbPCountry.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbPCountry.MoveToNxtCtrl = Nothing
        Me.cmbPCountry.Name = "cmbPCountry"
        Me.cmbPCountry.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbPCountry.Size = New System.Drawing.Size(248, 22)
        Me.cmbPCountry.strSelectStmt = ""
        Me.cmbPCountry.SuperBack = True
        Me.cmbPCountry.TabIndex = 3
        Me.cmbPCountry.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbPCountry.PropBag = resources.GetString("cmbPCountry.PropBag")
        '
        'C1Label14
        '
        Me.C1Label14.AttachedTextBoxName = Nothing
        Me.C1Label14.BackColor = System.Drawing.Color.Transparent
        Me.C1Label14.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label14.ForeColor = System.Drawing.Color.Black
        Me.C1Label14.Location = New System.Drawing.Point(28, 176)
        Me.C1Label14.Name = "C1Label14"
        Me.C1Label14.Size = New System.Drawing.Size(105, 13)
        Me.C1Label14.TabIndex = 23
        Me.C1Label14.Tag = Nothing
        Me.C1Label14.Text = "Pin code"
        Me.C1Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label14.TextDetached = True
        '
        'C1Label15
        '
        Me.C1Label15.AttachedTextBoxName = Nothing
        Me.C1Label15.BackColor = System.Drawing.Color.Transparent
        Me.C1Label15.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label15.ForeColor = System.Drawing.Color.Black
        Me.C1Label15.Location = New System.Drawing.Point(28, 151)
        Me.C1Label15.Name = "C1Label15"
        Me.C1Label15.Size = New System.Drawing.Size(105, 13)
        Me.C1Label15.TabIndex = 18
        Me.C1Label15.Tag = Nothing
        Me.C1Label15.Text = "City"
        Me.C1Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label15.TextDetached = True
        '
        'C1Label16
        '
        Me.C1Label16.AttachedTextBoxName = Nothing
        Me.C1Label16.BackColor = System.Drawing.Color.Transparent
        Me.C1Label16.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label16.ForeColor = System.Drawing.Color.Black
        Me.C1Label16.Location = New System.Drawing.Point(28, 125)
        Me.C1Label16.Name = "C1Label16"
        Me.C1Label16.Size = New System.Drawing.Size(105, 13)
        Me.C1Label16.TabIndex = 17
        Me.C1Label16.Tag = Nothing
        Me.C1Label16.Text = "State"
        Me.C1Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label16.TextDetached = True
        '
        'C1Label17
        '
        Me.C1Label17.AttachedTextBoxName = Nothing
        Me.C1Label17.BackColor = System.Drawing.Color.Transparent
        Me.C1Label17.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label17.ForeColor = System.Drawing.Color.Black
        Me.C1Label17.Location = New System.Drawing.Point(28, 98)
        Me.C1Label17.Name = "C1Label17"
        Me.C1Label17.Size = New System.Drawing.Size(105, 13)
        Me.C1Label17.TabIndex = 16
        Me.C1Label17.Tag = Nothing
        Me.C1Label17.Text = "Country"
        Me.C1Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label17.TextDetached = True
        '
        'txtPAddressLn2
        '
        Me.txtPAddressLn2.AcceptsTab = True
        Me.txtPAddressLn2.AutoSize = False
        Me.txtPAddressLn2.BackColor = System.Drawing.Color.White
        Me.txtPAddressLn2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPAddressLn2.Location = New System.Drawing.Point(141, 67)
        Me.txtPAddressLn2.MaxLength = 60
        Me.txtPAddressLn2.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtPAddressLn2.MoveToNxtCtrl = Nothing
        Me.txtPAddressLn2.Name = "txtPAddressLn2"
        Me.txtPAddressLn2.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtPAddressLn2.PreValidation.TrimStart = True
        Me.txtPAddressLn2.Size = New System.Drawing.Size(248, 21)
        Me.txtPAddressLn2.TabIndex = 2
        Me.txtPAddressLn2.Tag = Nothing
        Me.txtPAddressLn2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtPAddressLn1
        '
        Me.txtPAddressLn1.AcceptsTab = True
        Me.txtPAddressLn1.AutoSize = False
        Me.txtPAddressLn1.BackColor = System.Drawing.Color.White
        Me.txtPAddressLn1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPAddressLn1.Location = New System.Drawing.Point(141, 41)
        Me.txtPAddressLn1.MaxLength = 60
        Me.txtPAddressLn1.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtPAddressLn1.MoveToNxtCtrl = Nothing
        Me.txtPAddressLn1.Name = "txtPAddressLn1"
        Me.txtPAddressLn1.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtPAddressLn1.PreValidation.TrimStart = True
        Me.txtPAddressLn1.Size = New System.Drawing.Size(248, 21)
        Me.txtPAddressLn1.TabIndex = 1
        Me.txtPAddressLn1.Tag = Nothing
        Me.txtPAddressLn1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label18
        '
        Me.C1Label18.AttachedTextBoxName = Nothing
        Me.C1Label18.BackColor = System.Drawing.Color.Transparent
        Me.C1Label18.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label18.ForeColor = System.Drawing.Color.Black
        Me.C1Label18.Location = New System.Drawing.Point(28, 72)
        Me.C1Label18.Name = "C1Label18"
        Me.C1Label18.Size = New System.Drawing.Size(105, 13)
        Me.C1Label18.TabIndex = 13
        Me.C1Label18.Tag = Nothing
        Me.C1Label18.Text = "Address Line 2"
        Me.C1Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label18.TextDetached = True
        '
        'C1Label19
        '
        Me.C1Label19.AttachedTextBoxName = Nothing
        Me.C1Label19.BackColor = System.Drawing.Color.Transparent
        Me.C1Label19.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label19.ForeColor = System.Drawing.Color.Black
        Me.C1Label19.Location = New System.Drawing.Point(28, 44)
        Me.C1Label19.Name = "C1Label19"
        Me.C1Label19.Size = New System.Drawing.Size(105, 13)
        Me.C1Label19.TabIndex = 12
        Me.C1Label19.Tag = Nothing
        Me.C1Label19.Text = "Address Line 1"
        Me.C1Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label19.TextDetached = True
        '
        'GroupBoxLocalAddress
        '
        Me.GroupBoxLocalAddress.BackColor = System.Drawing.Color.White
        Me.GroupBoxLocalAddress.Controls.Add(Me.C1Label46)
        Me.GroupBoxLocalAddress.Controls.Add(Me.C1Label45)
        Me.GroupBoxLocalAddress.Controls.Add(Me.txtLPincode)
        Me.GroupBoxLocalAddress.Controls.Add(Me.cmbLStayMonths)
        Me.GroupBoxLocalAddress.Controls.Add(Me.cmbLStayYears)
        Me.GroupBoxLocalAddress.Controls.Add(Me.C1Label40)
        Me.GroupBoxLocalAddress.Controls.Add(Me.C1Label13)
        Me.GroupBoxLocalAddress.Controls.Add(Me.C1Label10)
        Me.GroupBoxLocalAddress.Controls.Add(Me.C1Label11)
        Me.GroupBoxLocalAddress.Controls.Add(Me.C1Label12)
        Me.GroupBoxLocalAddress.Controls.Add(Me.C1Label9)
        Me.GroupBoxLocalAddress.Controls.Add(Me.C1Label8)
        Me.GroupBoxLocalAddress.Controls.Add(Me.cmbLCity)
        Me.GroupBoxLocalAddress.Controls.Add(Me.cmbLState)
        Me.GroupBoxLocalAddress.Controls.Add(Me.cmbLCountry)
        Me.GroupBoxLocalAddress.Controls.Add(Me.txtLAddressLn2)
        Me.GroupBoxLocalAddress.Controls.Add(Me.txtLAddressLn1)
        Me.GroupBoxLocalAddress.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxLocalAddress.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.GroupBoxLocalAddress.Location = New System.Drawing.Point(5, 4)
        Me.GroupBoxLocalAddress.Name = "GroupBoxLocalAddress"
        Me.GroupBoxLocalAddress.Size = New System.Drawing.Size(454, 291)
        Me.GroupBoxLocalAddress.TabIndex = 2
        Me.GroupBoxLocalAddress.TabStop = False
        Me.GroupBoxLocalAddress.Text = "Local Address"
        '
        'C1Label46
        '
        Me.C1Label46.AttachedTextBoxName = Nothing
        Me.C1Label46.AutoSize = True
        Me.C1Label46.BackColor = System.Drawing.Color.Transparent
        Me.C1Label46.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label46.ForeColor = System.Drawing.Color.Black
        Me.C1Label46.Location = New System.Drawing.Point(347, 203)
        Me.C1Label46.Name = "C1Label46"
        Me.C1Label46.Size = New System.Drawing.Size(47, 13)
        Me.C1Label46.TabIndex = 43
        Me.C1Label46.Tag = Nothing
        Me.C1Label46.Text = "Months"
        Me.C1Label46.TextDetached = True
        '
        'C1Label45
        '
        Me.C1Label45.AttachedTextBoxName = Nothing
        Me.C1Label45.AutoSize = True
        Me.C1Label45.BackColor = System.Drawing.Color.Transparent
        Me.C1Label45.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label45.ForeColor = System.Drawing.Color.Black
        Me.C1Label45.Location = New System.Drawing.Point(215, 203)
        Me.C1Label45.Name = "C1Label45"
        Me.C1Label45.Size = New System.Drawing.Size(39, 13)
        Me.C1Label45.TabIndex = 26
        Me.C1Label45.Tag = Nothing
        Me.C1Label45.Text = "Years"
        Me.C1Label45.TextDetached = True
        '
        'txtLPincode
        '
        Me.txtLPincode.AcceptsTab = True
        Me.txtLPincode.AutoSize = False
        Me.txtLPincode.BackColor = System.Drawing.Color.White
        Me.txtLPincode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLPincode.DataType = GetType(Double)
        Me.txtLPincode.EmptyAsNull = True
        Me.txtLPincode.FormatType = C1.Win.C1Input.FormatTypeEnum.GeneralNumber
        Me.txtLPincode.Location = New System.Drawing.Point(146, 173)
        Me.txtLPincode.MaxLength = 10
        Me.txtLPincode.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtLPincode.MoveToNxtCtrl = Nothing
        Me.txtLPincode.Name = "txtLPincode"
        Me.txtLPincode.Size = New System.Drawing.Size(248, 21)
        Me.txtLPincode.TabIndex = 5
        Me.txtLPincode.Tag = Nothing
        Me.txtLPincode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmbLStayMonths
        '
        Me.cmbLStayMonths.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbLStayMonths.AutoCompletion = True
        Me.cmbLStayMonths.AutoDropDown = True
        Me.cmbLStayMonths.Caption = ""
        Me.cmbLStayMonths.CaptionHeight = 17
        Me.cmbLStayMonths.CaptionVisible = False
        Me.cmbLStayMonths.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbLStayMonths.ColumnCaptionHeight = 17
        Me.cmbLStayMonths.ColumnFooterHeight = 17
        Me.cmbLStayMonths.ColumnHeaders = False
        Me.cmbLStayMonths.ColumnWidth = 100
        Me.cmbLStayMonths.ContentHeight = 16
        Me.cmbLStayMonths.ctrlTextDbColumn = ""
        Me.cmbLStayMonths.DataMode = C1.Win.C1List.DataModeEnum.AddItem
        Me.cmbLStayMonths.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbLStayMonths.EditorBackColor = System.Drawing.Color.White
        Me.cmbLStayMonths.EditorFont = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmbLStayMonths.EditorForeColor = System.Drawing.Color.Black
        Me.cmbLStayMonths.EditorHeight = 16
        Me.cmbLStayMonths.Images.Add(CType(resources.GetObject("cmbLStayMonths.Images"), System.Drawing.Image))
        Me.cmbLStayMonths.ItemHeight = 15
        Me.cmbLStayMonths.LimitToList = True
        Me.cmbLStayMonths.Location = New System.Drawing.Point(272, 200)
        Me.cmbLStayMonths.MatchEntryTimeout = CType(2000, Long)
        Me.cmbLStayMonths.MaxDropDownItems = CType(5, Short)
        Me.cmbLStayMonths.MaxLength = 32767
        Me.cmbLStayMonths.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbLStayMonths.MoveToNxtCtrl = Nothing
        Me.cmbLStayMonths.Name = "cmbLStayMonths"
        Me.cmbLStayMonths.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbLStayMonths.Size = New System.Drawing.Size(64, 22)
        Me.cmbLStayMonths.strSelectStmt = ""
        Me.cmbLStayMonths.SuperBack = True
        Me.cmbLStayMonths.TabIndex = 7
        Me.cmbLStayMonths.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbLStayMonths.PropBag = resources.GetString("cmbLStayMonths.PropBag")
        '
        'cmbLStayYears
        '
        Me.cmbLStayYears.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbLStayYears.AutoCompletion = True
        Me.cmbLStayYears.AutoDropDown = True
        Me.cmbLStayYears.Caption = ""
        Me.cmbLStayYears.CaptionHeight = 17
        Me.cmbLStayYears.CaptionVisible = False
        Me.cmbLStayYears.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbLStayYears.ColumnCaptionHeight = 17
        Me.cmbLStayYears.ColumnFooterHeight = 17
        Me.cmbLStayYears.ColumnHeaders = False
        Me.cmbLStayYears.ColumnWidth = 100
        Me.cmbLStayYears.ContentHeight = 16
        Me.cmbLStayYears.ctrlTextDbColumn = ""
        Me.cmbLStayYears.DataMode = C1.Win.C1List.DataModeEnum.AddItem
        Me.cmbLStayYears.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbLStayYears.EditorBackColor = System.Drawing.Color.White
        Me.cmbLStayYears.EditorFont = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmbLStayYears.EditorForeColor = System.Drawing.Color.Black
        Me.cmbLStayYears.EditorHeight = 16
        Me.cmbLStayYears.Images.Add(CType(resources.GetObject("cmbLStayYears.Images"), System.Drawing.Image))
        Me.cmbLStayYears.ItemHeight = 15
        Me.cmbLStayYears.LimitToList = True
        Me.cmbLStayYears.Location = New System.Drawing.Point(146, 199)
        Me.cmbLStayYears.MatchEntryTimeout = CType(2000, Long)
        Me.cmbLStayYears.MaxDropDownItems = CType(5, Short)
        Me.cmbLStayYears.MaxLength = 32767
        Me.cmbLStayYears.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbLStayYears.MoveToNxtCtrl = Nothing
        Me.cmbLStayYears.Name = "cmbLStayYears"
        Me.cmbLStayYears.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbLStayYears.Size = New System.Drawing.Size(67, 22)
        Me.cmbLStayYears.strSelectStmt = ""
        Me.cmbLStayYears.SuperBack = True
        Me.cmbLStayYears.TabIndex = 6
        Me.cmbLStayYears.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbLStayYears.PropBag = resources.GetString("cmbLStayYears.PropBag")
        '
        'C1Label40
        '
        Me.C1Label40.AttachedTextBoxName = Nothing
        Me.C1Label40.BackColor = System.Drawing.Color.Transparent
        Me.C1Label40.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label40.ForeColor = System.Drawing.Color.Black
        Me.C1Label40.Location = New System.Drawing.Point(40, 205)
        Me.C1Label40.Name = "C1Label40"
        Me.C1Label40.Size = New System.Drawing.Size(105, 13)
        Me.C1Label40.TabIndex = 42
        Me.C1Label40.Tag = Nothing
        Me.C1Label40.Text = "  Stay Duration"
        Me.C1Label40.TextDetached = True
        '
        'C1Label13
        '
        Me.C1Label13.AttachedTextBoxName = Nothing
        Me.C1Label13.BackColor = System.Drawing.Color.White
        Me.C1Label13.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label13.ForeColor = System.Drawing.Color.Black
        Me.C1Label13.Location = New System.Drawing.Point(40, 176)
        Me.C1Label13.Name = "C1Label13"
        Me.C1Label13.Size = New System.Drawing.Size(105, 13)
        Me.C1Label13.TabIndex = 11
        Me.C1Label13.Tag = Nothing
        Me.C1Label13.Text = "*Pin code"
        Me.C1Label13.TextDetached = True
        Me.C1Label13.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label10
        '
        Me.C1Label10.AttachedTextBoxName = Nothing
        Me.C1Label10.BackColor = System.Drawing.Color.White
        Me.C1Label10.BorderColor = System.Drawing.Color.White
        Me.C1Label10.ForeColor = System.Drawing.Color.Black
        Me.C1Label10.Location = New System.Drawing.Point(40, 98)
        Me.C1Label10.Name = "C1Label10"
        Me.C1Label10.Size = New System.Drawing.Size(105, 13)
        Me.C1Label10.TabIndex = 4
        Me.C1Label10.Tag = Nothing
        Me.C1Label10.Text = "*Country"
        Me.C1Label10.TextDetached = True
        Me.C1Label10.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label11
        '
        Me.C1Label11.AttachedTextBoxName = Nothing
        Me.C1Label11.BackColor = System.Drawing.Color.White
        Me.C1Label11.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label11.ForeColor = System.Drawing.Color.Black
        Me.C1Label11.Location = New System.Drawing.Point(40, 128)
        Me.C1Label11.Name = "C1Label11"
        Me.C1Label11.Size = New System.Drawing.Size(105, 13)
        Me.C1Label11.TabIndex = 5
        Me.C1Label11.Tag = Nothing
        Me.C1Label11.Text = "*State"
        Me.C1Label11.TextDetached = True
        Me.C1Label11.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label12
        '
        Me.C1Label12.AttachedTextBoxName = Nothing
        Me.C1Label12.BackColor = System.Drawing.Color.White
        Me.C1Label12.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label12.ForeColor = System.Drawing.Color.Black
        Me.C1Label12.Location = New System.Drawing.Point(40, 154)
        Me.C1Label12.Name = "C1Label12"
        Me.C1Label12.Size = New System.Drawing.Size(105, 13)
        Me.C1Label12.TabIndex = 6
        Me.C1Label12.Tag = Nothing
        Me.C1Label12.Text = "*City"
        Me.C1Label12.TextDetached = True
        Me.C1Label12.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label9
        '
        Me.C1Label9.AttachedTextBoxName = Nothing
        Me.C1Label9.BackColor = System.Drawing.Color.White
        Me.C1Label9.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label9.ForeColor = System.Drawing.Color.Black
        Me.C1Label9.Location = New System.Drawing.Point(40, 72)
        Me.C1Label9.Name = "C1Label9"
        Me.C1Label9.Size = New System.Drawing.Size(105, 13)
        Me.C1Label9.TabIndex = 1
        Me.C1Label9.Tag = Nothing
        Me.C1Label9.Text = "*Address Line 2"
        Me.C1Label9.TextDetached = True
        Me.C1Label9.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label8
        '
        Me.C1Label8.AttachedTextBoxName = Nothing
        Me.C1Label8.BackColor = System.Drawing.Color.White
        Me.C1Label8.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label8.ForeColor = System.Drawing.Color.Black
        Me.C1Label8.Location = New System.Drawing.Point(40, 44)
        Me.C1Label8.Name = "C1Label8"
        Me.C1Label8.Size = New System.Drawing.Size(105, 13)
        Me.C1Label8.TabIndex = 0
        Me.C1Label8.Tag = Nothing
        Me.C1Label8.Text = "*Address Line 1"
        Me.C1Label8.TextDetached = True
        Me.C1Label8.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmbLCity
        '
        Me.cmbLCity.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbLCity.AutoCompletion = True
        Me.cmbLCity.AutoDropDown = True
        Me.cmbLCity.Caption = ""
        Me.cmbLCity.CaptionHeight = 17
        Me.cmbLCity.CaptionVisible = False
        Me.cmbLCity.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbLCity.ColumnCaptionHeight = 17
        Me.cmbLCity.ColumnFooterHeight = 17
        Me.cmbLCity.ColumnHeaders = False
        Me.cmbLCity.ContentHeight = 16
        Me.cmbLCity.ctrlTextDbColumn = ""
        Me.cmbLCity.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbLCity.EditorBackColor = System.Drawing.Color.White
        Me.cmbLCity.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLCity.EditorForeColor = System.Drawing.Color.Black
        Me.cmbLCity.EditorHeight = 16
        Me.cmbLCity.Images.Add(CType(resources.GetObject("cmbLCity.Images"), System.Drawing.Image))
        Me.cmbLCity.ItemHeight = 15
        Me.cmbLCity.LimitToList = True
        Me.cmbLCity.Location = New System.Drawing.Point(146, 147)
        Me.cmbLCity.MatchEntryTimeout = CType(2000, Long)
        Me.cmbLCity.MaxDropDownItems = CType(5, Short)
        Me.cmbLCity.MaxLength = 32767
        Me.cmbLCity.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbLCity.MoveToNxtCtrl = Nothing
        Me.cmbLCity.Name = "cmbLCity"
        Me.cmbLCity.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbLCity.Size = New System.Drawing.Size(248, 22)
        Me.cmbLCity.strSelectStmt = ""
        Me.cmbLCity.SuperBack = True
        Me.cmbLCity.TabIndex = 4
        Me.cmbLCity.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbLCity.PropBag = resources.GetString("cmbLCity.PropBag")
        '
        'cmbLState
        '
        Me.cmbLState.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbLState.AutoCompletion = True
        Me.cmbLState.AutoDropDown = True
        Me.cmbLState.Caption = ""
        Me.cmbLState.CaptionHeight = 17
        Me.cmbLState.CaptionVisible = False
        Me.cmbLState.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbLState.ColumnCaptionHeight = 17
        Me.cmbLState.ColumnFooterHeight = 17
        Me.cmbLState.ColumnHeaders = False
        Me.cmbLState.ContentHeight = 16
        Me.cmbLState.ctrlTextDbColumn = ""
        Me.cmbLState.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbLState.EditorBackColor = System.Drawing.Color.White
        Me.cmbLState.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLState.EditorForeColor = System.Drawing.Color.Black
        Me.cmbLState.EditorHeight = 16
        Me.cmbLState.Images.Add(CType(resources.GetObject("cmbLState.Images"), System.Drawing.Image))
        Me.cmbLState.ItemHeight = 15
        Me.cmbLState.LimitToList = True
        Me.cmbLState.Location = New System.Drawing.Point(146, 120)
        Me.cmbLState.MatchEntryTimeout = CType(2000, Long)
        Me.cmbLState.MaxDropDownItems = CType(5, Short)
        Me.cmbLState.MaxLength = 32767
        Me.cmbLState.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbLState.MoveToNxtCtrl = Nothing
        Me.cmbLState.Name = "cmbLState"
        Me.cmbLState.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbLState.Size = New System.Drawing.Size(248, 22)
        Me.cmbLState.strSelectStmt = ""
        Me.cmbLState.SuperBack = True
        Me.cmbLState.TabIndex = 3
        Me.cmbLState.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbLState.PropBag = resources.GetString("cmbLState.PropBag")
        '
        'cmbLCountry
        '
        Me.cmbLCountry.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbLCountry.AutoCompletion = True
        Me.cmbLCountry.AutoDropDown = True
        Me.cmbLCountry.Caption = ""
        Me.cmbLCountry.CaptionHeight = 17
        Me.cmbLCountry.CaptionVisible = False
        Me.cmbLCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbLCountry.ColumnCaptionHeight = 17
        Me.cmbLCountry.ColumnFooterHeight = 17
        Me.cmbLCountry.ColumnHeaders = False
        Me.cmbLCountry.ContentHeight = 16
        Me.cmbLCountry.ctrlTextDbColumn = ""
        Me.cmbLCountry.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbLCountry.EditorBackColor = System.Drawing.Color.White
        Me.cmbLCountry.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLCountry.EditorForeColor = System.Drawing.Color.Black
        Me.cmbLCountry.EditorHeight = 16
        Me.cmbLCountry.Images.Add(CType(resources.GetObject("cmbLCountry.Images"), System.Drawing.Image))
        Me.cmbLCountry.ItemHeight = 15
        Me.cmbLCountry.LimitToList = True
        Me.cmbLCountry.Location = New System.Drawing.Point(146, 93)
        Me.cmbLCountry.MatchEntryTimeout = CType(2000, Long)
        Me.cmbLCountry.MaxDropDownItems = CType(5, Short)
        Me.cmbLCountry.MaxLength = 32767
        Me.cmbLCountry.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbLCountry.MoveToNxtCtrl = Nothing
        Me.cmbLCountry.Name = "cmbLCountry"
        Me.cmbLCountry.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbLCountry.Size = New System.Drawing.Size(248, 22)
        Me.cmbLCountry.strSelectStmt = ""
        Me.cmbLCountry.SuperBack = True
        Me.cmbLCountry.TabIndex = 2
        Me.cmbLCountry.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbLCountry.PropBag = resources.GetString("cmbLCountry.PropBag")
        '
        'txtLAddressLn2
        '
        Me.txtLAddressLn2.AcceptsTab = True
        Me.txtLAddressLn2.AutoSize = False
        Me.txtLAddressLn2.BackColor = System.Drawing.Color.White
        Me.txtLAddressLn2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLAddressLn2.Location = New System.Drawing.Point(146, 67)
        Me.txtLAddressLn2.MaxLength = 60
        Me.txtLAddressLn2.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtLAddressLn2.MoveToNxtCtrl = Nothing
        Me.txtLAddressLn2.Name = "txtLAddressLn2"
        Me.txtLAddressLn2.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtLAddressLn2.PreValidation.TrimStart = True
        Me.txtLAddressLn2.Size = New System.Drawing.Size(248, 21)
        Me.txtLAddressLn2.TabIndex = 1
        Me.txtLAddressLn2.Tag = Nothing
        Me.txtLAddressLn2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtLAddressLn1
        '
        Me.txtLAddressLn1.AcceptsTab = True
        Me.txtLAddressLn1.AutoSize = False
        Me.txtLAddressLn1.BackColor = System.Drawing.Color.White
        Me.txtLAddressLn1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLAddressLn1.Location = New System.Drawing.Point(146, 41)
        Me.txtLAddressLn1.MaxLength = 60
        Me.txtLAddressLn1.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtLAddressLn1.MoveToNxtCtrl = Nothing
        Me.txtLAddressLn1.Name = "txtLAddressLn1"
        Me.txtLAddressLn1.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtLAddressLn1.PreValidation.TrimStart = True
        Me.txtLAddressLn1.Size = New System.Drawing.Size(248, 21)
        Me.txtLAddressLn1.TabIndex = 0
        Me.txtLAddressLn1.Tag = Nothing
        Me.txtLAddressLn1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'TabPagePatientInfo
        '
        Me.TabPagePatientInfo.Controls.Add(Me.C1Sizer5)
        Me.TabPagePatientInfo.Location = New System.Drawing.Point(1, 25)
        Me.TabPagePatientInfo.Name = "TabPagePatientInfo"
        Me.TabPagePatientInfo.Size = New System.Drawing.Size(918, 299)
        Me.TabPagePatientInfo.TabBackColor = System.Drawing.Color.Gray
        Me.TabPagePatientInfo.TabBackColorSelected = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.TabPagePatientInfo.TabForeColor = System.Drawing.Color.White
        Me.TabPagePatientInfo.TabForeColorSelected = System.Drawing.Color.White
        Me.TabPagePatientInfo.TabIndex = 21
        Me.TabPagePatientInfo.Text = "Personal Details"
        '
        'C1Sizer5
        '
        Me.C1Sizer5.BackColor = System.Drawing.Color.White
        Me.C1Sizer5.Controls.Add(Me.GroupBoxOtherInformation)
        Me.C1Sizer5.Controls.Add(Me.groupboxContactDetails)
        Me.C1Sizer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Sizer5.GridDefinition = "97.3244147157191:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "49.1285403050109:False:False;49.3464052287582:False" & _
    ":False;"
        Me.C1Sizer5.Location = New System.Drawing.Point(0, 0)
        Me.C1Sizer5.Name = "C1Sizer5"
        Me.C1Sizer5.Padding = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.C1Sizer5.Size = New System.Drawing.Size(918, 299)
        Me.C1Sizer5.TabIndex = 0
        Me.C1Sizer5.Text = "C1Sizer5"
        '
        'GroupBoxOtherInformation
        '
        Me.GroupBoxOtherInformation.BackColor = System.Drawing.Color.White
        Me.GroupBoxOtherInformation.Controls.Add(Me.txtMonthlyIncome)
        Me.GroupBoxOtherInformation.Controls.Add(Me.cmbMotherTounge)
        Me.GroupBoxOtherInformation.Controls.Add(Me.C1Label43)
        Me.GroupBoxOtherInformation.Controls.Add(Me.C1Label26)
        Me.GroupBoxOtherInformation.Controls.Add(Me.C1Label6)
        Me.GroupBoxOtherInformation.Controls.Add(Me.txtNationality)
        Me.GroupBoxOtherInformation.Controls.Add(Me.txtNearestRelative)
        Me.GroupBoxOtherInformation.Controls.Add(Me.C1Label32)
        Me.GroupBoxOtherInformation.Controls.Add(Me.cmbRelation)
        Me.GroupBoxOtherInformation.Controls.Add(Me.txtRelation)
        Me.GroupBoxOtherInformation.Controls.Add(Me.txtReligon)
        Me.GroupBoxOtherInformation.Controls.Add(Me.C1Label31)
        Me.GroupBoxOtherInformation.Controls.Add(Me.cmbMaritalStatus)
        Me.GroupBoxOtherInformation.Controls.Add(Me.C1Label21)
        Me.GroupBoxOtherInformation.Controls.Add(Me.C1Label28)
        Me.GroupBoxOtherInformation.Controls.Add(Me.cmbQualification)
        Me.GroupBoxOtherInformation.Controls.Add(Me.cmbOccupation)
        Me.GroupBoxOtherInformation.Controls.Add(Me.C1Label22)
        Me.GroupBoxOtherInformation.Controls.Add(Me.C1Label27)
        Me.GroupBoxOtherInformation.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxOtherInformation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.GroupBoxOtherInformation.Location = New System.Drawing.Point(460, 4)
        Me.GroupBoxOtherInformation.Name = "GroupBoxOtherInformation"
        Me.GroupBoxOtherInformation.Size = New System.Drawing.Size(453, 291)
        Me.GroupBoxOtherInformation.TabIndex = 1
        Me.GroupBoxOtherInformation.TabStop = False
        Me.GroupBoxOtherInformation.Text = "Other Information"
        '
        'txtMonthlyIncome
        '
        Me.txtMonthlyIncome.AcceptsTab = True
        Me.txtMonthlyIncome.AutoSize = False
        Me.txtMonthlyIncome.BackColor = System.Drawing.Color.White
        Me.txtMonthlyIncome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMonthlyIncome.DataType = GetType(Double)
        Me.txtMonthlyIncome.EmptyAsNull = True
        Me.txtMonthlyIncome.FormatType = C1.Win.C1Input.FormatTypeEnum.StandardNumber
        Me.txtMonthlyIncome.Location = New System.Drawing.Point(206, 90)
        Me.txtMonthlyIncome.MaxLength = 11
        Me.txtMonthlyIncome.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtMonthlyIncome.MoveToNxtCtrl = Nothing
        Me.txtMonthlyIncome.Name = "txtMonthlyIncome"
        Me.txtMonthlyIncome.Size = New System.Drawing.Size(220, 21)
        Me.txtMonthlyIncome.TabIndex = 2
        Me.txtMonthlyIncome.Tag = Nothing
        Me.txtMonthlyIncome.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmbMotherTounge
        '
        Me.cmbMotherTounge.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbMotherTounge.AutoCompletion = True
        Me.cmbMotherTounge.AutoDropDown = True
        Me.cmbMotherTounge.Caption = ""
        Me.cmbMotherTounge.CaptionHeight = 17
        Me.cmbMotherTounge.CaptionVisible = False
        Me.cmbMotherTounge.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbMotherTounge.ColumnCaptionHeight = 17
        Me.cmbMotherTounge.ColumnFooterHeight = 17
        Me.cmbMotherTounge.ColumnHeaders = False
        Me.cmbMotherTounge.ContentHeight = 16
        Me.cmbMotherTounge.ctrlTextDbColumn = ""
        Me.cmbMotherTounge.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbMotherTounge.EditorBackColor = System.Drawing.Color.White
        Me.cmbMotherTounge.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMotherTounge.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbMotherTounge.EditorHeight = 16
        Me.cmbMotherTounge.Images.Add(CType(resources.GetObject("cmbMotherTounge.Images"), System.Drawing.Image))
        Me.cmbMotherTounge.ItemHeight = 15
        Me.cmbMotherTounge.LimitToList = True
        Me.cmbMotherTounge.Location = New System.Drawing.Point(187, 172)
        Me.cmbMotherTounge.MatchEntryTimeout = CType(2000, Long)
        Me.cmbMotherTounge.MaxDropDownItems = CType(5, Short)
        Me.cmbMotherTounge.MaxLength = 32767
        Me.cmbMotherTounge.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbMotherTounge.MoveToNxtCtrl = Nothing
        Me.cmbMotherTounge.Name = "cmbMotherTounge"
        Me.cmbMotherTounge.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbMotherTounge.Size = New System.Drawing.Size(239, 22)
        Me.cmbMotherTounge.strSelectStmt = ""
        Me.cmbMotherTounge.SuperBack = True
        Me.cmbMotherTounge.TabIndex = 5
        Me.cmbMotherTounge.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbMotherTounge.PropBag = resources.GetString("cmbMotherTounge.PropBag")
        '
        'C1Label43
        '
        Me.C1Label43.AttachedTextBoxName = Nothing
        Me.C1Label43.AutoSize = True
        Me.C1Label43.BackColor = System.Drawing.Color.Transparent
        Me.C1Label43.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label43.ForeColor = System.Drawing.Color.Black
        Me.C1Label43.Location = New System.Drawing.Point(184, 94)
        Me.C1Label43.Name = "C1Label43"
        Me.C1Label43.Size = New System.Drawing.Size(21, 13)
        Me.C1Label43.TabIndex = 28
        Me.C1Label43.Tag = Nothing
        Me.C1Label43.Text = "Rs"
        Me.C1Label43.TextDetached = True
        '
        'C1Label26
        '
        Me.C1Label26.AttachedTextBoxName = Nothing
        Me.C1Label26.BackColor = System.Drawing.Color.Transparent
        Me.C1Label26.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label26.ForeColor = System.Drawing.Color.Black
        Me.C1Label26.Location = New System.Drawing.Point(31, 172)
        Me.C1Label26.Name = "C1Label26"
        Me.C1Label26.Size = New System.Drawing.Size(125, 13)
        Me.C1Label26.TabIndex = 26
        Me.C1Label26.Tag = Nothing
        Me.C1Label26.Text = "Mother Tongue"
        Me.C1Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label26.TextDetached = True
        '
        'C1Label6
        '
        Me.C1Label6.AttachedTextBoxName = Nothing
        Me.C1Label6.AutoSize = True
        Me.C1Label6.BackColor = System.Drawing.Color.Transparent
        Me.C1Label6.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label6.ForeColor = System.Drawing.Color.Black
        Me.C1Label6.Location = New System.Drawing.Point(30, 257)
        Me.C1Label6.Name = "C1Label6"
        Me.C1Label6.Size = New System.Drawing.Size(147, 13)
        Me.C1Label6.TabIndex = 16
        Me.C1Label6.Tag = Nothing
        Me.C1Label6.Text = "Nearest Relative's Name"
        Me.C1Label6.TextDetached = True
        '
        'txtNationality
        '
        Me.txtNationality.AcceptsTab = True
        Me.txtNationality.AutoSize = False
        Me.txtNationality.BackColor = System.Drawing.Color.White
        Me.txtNationality.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNationality.Location = New System.Drawing.Point(187, 199)
        Me.txtNationality.MaxLength = 25
        Me.txtNationality.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtNationality.MoveToNxtCtrl = Nothing
        Me.txtNationality.Name = "txtNationality"
        Me.txtNationality.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtNationality.PreValidation.TrimStart = True
        Me.txtNationality.Size = New System.Drawing.Size(239, 21)
        Me.txtNationality.TabIndex = 6
        Me.txtNationality.Tag = Nothing
        Me.txtNationality.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtNearestRelative
        '
        Me.txtNearestRelative.AcceptsTab = True
        Me.txtNearestRelative.AutoSize = False
        Me.txtNearestRelative.BackColor = System.Drawing.Color.White
        Me.txtNearestRelative.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNearestRelative.Location = New System.Drawing.Point(187, 257)
        Me.txtNearestRelative.MaxLength = 120
        Me.txtNearestRelative.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtNearestRelative.MoveToNxtCtrl = Nothing
        Me.txtNearestRelative.Name = "txtNearestRelative"
        Me.txtNearestRelative.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtNearestRelative.PreValidation.TrimStart = True
        Me.txtNearestRelative.Size = New System.Drawing.Size(239, 21)
        Me.txtNearestRelative.TabIndex = 9
        Me.txtNearestRelative.Tag = Nothing
        Me.txtNearestRelative.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label32
        '
        Me.C1Label32.AttachedTextBoxName = Nothing
        Me.C1Label32.BackColor = System.Drawing.Color.Transparent
        Me.C1Label32.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label32.ForeColor = System.Drawing.Color.Black
        Me.C1Label32.Location = New System.Drawing.Point(31, 200)
        Me.C1Label32.Name = "C1Label32"
        Me.C1Label32.Size = New System.Drawing.Size(125, 13)
        Me.C1Label32.TabIndex = 24
        Me.C1Label32.Tag = Nothing
        Me.C1Label32.Text = "Nationality"
        Me.C1Label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label32.TextDetached = True
        '
        'cmbRelation
        '
        Me.cmbRelation.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbRelation.AutoCompletion = True
        Me.cmbRelation.AutoDropDown = True
        Me.cmbRelation.Caption = ""
        Me.cmbRelation.CaptionHeight = 17
        Me.cmbRelation.CaptionVisible = False
        Me.cmbRelation.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbRelation.ColumnCaptionHeight = 17
        Me.cmbRelation.ColumnFooterHeight = 17
        Me.cmbRelation.ColumnHeaders = False
        Me.cmbRelation.ContentHeight = 16
        Me.cmbRelation.ctrlTextDbColumn = ""
        Me.cmbRelation.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbRelation.EditorBackColor = System.Drawing.Color.White
        Me.cmbRelation.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRelation.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbRelation.EditorHeight = 16
        Me.cmbRelation.Images.Add(CType(resources.GetObject("cmbRelation.Images"), System.Drawing.Image))
        Me.cmbRelation.ItemHeight = 15
        Me.cmbRelation.LimitToList = True
        Me.cmbRelation.Location = New System.Drawing.Point(33, 225)
        Me.cmbRelation.MatchEntryTimeout = CType(2000, Long)
        Me.cmbRelation.MaxDropDownItems = CType(5, Short)
        Me.cmbRelation.MaxLength = 32767
        Me.cmbRelation.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbRelation.MoveToNxtCtrl = Nothing
        Me.cmbRelation.Name = "cmbRelation"
        Me.cmbRelation.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbRelation.Size = New System.Drawing.Size(141, 22)
        Me.cmbRelation.strSelectStmt = ""
        Me.cmbRelation.SuperBack = True
        Me.cmbRelation.TabIndex = 7
        Me.cmbRelation.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbRelation.PropBag = resources.GetString("cmbRelation.PropBag")
        '
        'txtRelation
        '
        Me.txtRelation.AcceptsTab = True
        Me.txtRelation.AutoSize = False
        Me.txtRelation.BackColor = System.Drawing.Color.White
        Me.txtRelation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRelation.Location = New System.Drawing.Point(187, 226)
        Me.txtRelation.MaxLength = 120
        Me.txtRelation.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtRelation.MoveToNxtCtrl = Nothing
        Me.txtRelation.Name = "txtRelation"
        Me.txtRelation.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtRelation.PreValidation.TrimStart = True
        Me.txtRelation.Size = New System.Drawing.Size(239, 21)
        Me.txtRelation.TabIndex = 8
        Me.txtRelation.Tag = Nothing
        Me.txtRelation.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtReligon
        '
        Me.txtReligon.AcceptsTab = True
        Me.txtReligon.AutoSize = False
        Me.txtReligon.BackColor = System.Drawing.Color.White
        Me.txtReligon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReligon.Location = New System.Drawing.Point(187, 143)
        Me.txtReligon.MaxLength = 15
        Me.txtReligon.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtReligon.MoveToNxtCtrl = Nothing
        Me.txtReligon.Name = "txtReligon"
        Me.txtReligon.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtReligon.PreValidation.TrimStart = True
        Me.txtReligon.Size = New System.Drawing.Size(239, 21)
        Me.txtReligon.TabIndex = 4
        Me.txtReligon.Tag = Nothing
        Me.txtReligon.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label31
        '
        Me.C1Label31.AttachedTextBoxName = Nothing
        Me.C1Label31.BackColor = System.Drawing.Color.Transparent
        Me.C1Label31.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label31.ForeColor = System.Drawing.Color.Black
        Me.C1Label31.Location = New System.Drawing.Point(31, 144)
        Me.C1Label31.Name = "C1Label31"
        Me.C1Label31.Size = New System.Drawing.Size(125, 13)
        Me.C1Label31.TabIndex = 22
        Me.C1Label31.Tag = Nothing
        Me.C1Label31.Text = "Religion"
        Me.C1Label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label31.TextDetached = True
        '
        'cmbMaritalStatus
        '
        Me.cmbMaritalStatus.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbMaritalStatus.AutoCompletion = True
        Me.cmbMaritalStatus.AutoDropDown = True
        Me.cmbMaritalStatus.Caption = ""
        Me.cmbMaritalStatus.CaptionHeight = 17
        Me.cmbMaritalStatus.CaptionVisible = False
        Me.cmbMaritalStatus.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbMaritalStatus.ColumnCaptionHeight = 17
        Me.cmbMaritalStatus.ColumnFooterHeight = 17
        Me.cmbMaritalStatus.ColumnHeaders = False
        Me.cmbMaritalStatus.ContentHeight = 16
        Me.cmbMaritalStatus.ctrlTextDbColumn = ""
        Me.cmbMaritalStatus.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbMaritalStatus.EditorBackColor = System.Drawing.Color.White
        Me.cmbMaritalStatus.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMaritalStatus.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbMaritalStatus.EditorHeight = 16
        Me.cmbMaritalStatus.Images.Add(CType(resources.GetObject("cmbMaritalStatus.Images"), System.Drawing.Image))
        Me.cmbMaritalStatus.ItemHeight = 15
        Me.cmbMaritalStatus.LimitToList = True
        Me.cmbMaritalStatus.Location = New System.Drawing.Point(187, 117)
        Me.cmbMaritalStatus.MatchEntryTimeout = CType(2000, Long)
        Me.cmbMaritalStatus.MaxDropDownItems = CType(5, Short)
        Me.cmbMaritalStatus.MaxLength = 32767
        Me.cmbMaritalStatus.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbMaritalStatus.MoveToNxtCtrl = Nothing
        Me.cmbMaritalStatus.Name = "cmbMaritalStatus"
        Me.cmbMaritalStatus.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbMaritalStatus.Size = New System.Drawing.Size(239, 22)
        Me.cmbMaritalStatus.strSelectStmt = ""
        Me.cmbMaritalStatus.SuperBack = True
        Me.cmbMaritalStatus.TabIndex = 3
        Me.cmbMaritalStatus.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbMaritalStatus.PropBag = resources.GetString("cmbMaritalStatus.PropBag")
        '
        'C1Label21
        '
        Me.C1Label21.AttachedTextBoxName = Nothing
        Me.C1Label21.BackColor = System.Drawing.Color.Transparent
        Me.C1Label21.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label21.ForeColor = System.Drawing.Color.Black
        Me.C1Label21.Location = New System.Drawing.Point(31, 117)
        Me.C1Label21.Name = "C1Label21"
        Me.C1Label21.Size = New System.Drawing.Size(125, 13)
        Me.C1Label21.TabIndex = 21
        Me.C1Label21.Tag = Nothing
        Me.C1Label21.Text = "Marital Status"
        Me.C1Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label21.TextDetached = True
        '
        'C1Label28
        '
        Me.C1Label28.AttachedTextBoxName = Nothing
        Me.C1Label28.BackColor = System.Drawing.Color.Transparent
        Me.C1Label28.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label28.ForeColor = System.Drawing.Color.Black
        Me.C1Label28.Location = New System.Drawing.Point(31, 90)
        Me.C1Label28.Name = "C1Label28"
        Me.C1Label28.Size = New System.Drawing.Size(125, 13)
        Me.C1Label28.TabIndex = 18
        Me.C1Label28.Tag = Nothing
        Me.C1Label28.Text = "Monthly Income"
        Me.C1Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label28.TextDetached = True
        '
        'cmbQualification
        '
        Me.cmbQualification.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbQualification.AutoCompletion = True
        Me.cmbQualification.AutoDropDown = True
        Me.cmbQualification.Caption = ""
        Me.cmbQualification.CaptionHeight = 17
        Me.cmbQualification.CaptionVisible = False
        Me.cmbQualification.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbQualification.ColumnCaptionHeight = 17
        Me.cmbQualification.ColumnFooterHeight = 17
        Me.cmbQualification.ColumnHeaders = False
        Me.cmbQualification.ContentHeight = 16
        Me.cmbQualification.ctrlTextDbColumn = ""
        Me.cmbQualification.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbQualification.EditorBackColor = System.Drawing.Color.White
        Me.cmbQualification.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbQualification.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbQualification.EditorHeight = 16
        Me.cmbQualification.Images.Add(CType(resources.GetObject("cmbQualification.Images"), System.Drawing.Image))
        Me.cmbQualification.ItemHeight = 15
        Me.cmbQualification.LimitToList = True
        Me.cmbQualification.Location = New System.Drawing.Point(187, 33)
        Me.cmbQualification.MatchEntryTimeout = CType(2000, Long)
        Me.cmbQualification.MaxDropDownItems = CType(5, Short)
        Me.cmbQualification.MaxLength = 32767
        Me.cmbQualification.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbQualification.MoveToNxtCtrl = Nothing
        Me.cmbQualification.Name = "cmbQualification"
        Me.cmbQualification.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbQualification.Size = New System.Drawing.Size(239, 22)
        Me.cmbQualification.strSelectStmt = ""
        Me.cmbQualification.SuperBack = True
        Me.cmbQualification.TabIndex = 0
        Me.cmbQualification.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbQualification.PropBag = resources.GetString("cmbQualification.PropBag")
        '
        'cmbOccupation
        '
        Me.cmbOccupation.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbOccupation.AutoCompletion = True
        Me.cmbOccupation.AutoDropDown = True
        Me.cmbOccupation.Caption = ""
        Me.cmbOccupation.CaptionHeight = 17
        Me.cmbOccupation.CaptionVisible = False
        Me.cmbOccupation.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbOccupation.ColumnCaptionHeight = 17
        Me.cmbOccupation.ColumnFooterHeight = 17
        Me.cmbOccupation.ColumnHeaders = False
        Me.cmbOccupation.ContentHeight = 16
        Me.cmbOccupation.ctrlTextDbColumn = ""
        Me.cmbOccupation.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbOccupation.EditorBackColor = System.Drawing.Color.White
        Me.cmbOccupation.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOccupation.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOccupation.EditorHeight = 16
        Me.cmbOccupation.Images.Add(CType(resources.GetObject("cmbOccupation.Images"), System.Drawing.Image))
        Me.cmbOccupation.ItemHeight = 15
        Me.cmbOccupation.LimitToList = True
        Me.cmbOccupation.Location = New System.Drawing.Point(187, 61)
        Me.cmbOccupation.MatchEntryTimeout = CType(2000, Long)
        Me.cmbOccupation.MaxDropDownItems = CType(5, Short)
        Me.cmbOccupation.MaxLength = 32767
        Me.cmbOccupation.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbOccupation.MoveToNxtCtrl = Nothing
        Me.cmbOccupation.Name = "cmbOccupation"
        Me.cmbOccupation.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbOccupation.Size = New System.Drawing.Size(239, 22)
        Me.cmbOccupation.strSelectStmt = ""
        Me.cmbOccupation.SuperBack = True
        Me.cmbOccupation.TabIndex = 1
        Me.cmbOccupation.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbOccupation.PropBag = resources.GetString("cmbOccupation.PropBag")
        '
        'C1Label22
        '
        Me.C1Label22.AttachedTextBoxName = Nothing
        Me.C1Label22.BackColor = System.Drawing.Color.Transparent
        Me.C1Label22.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label22.ForeColor = System.Drawing.Color.Black
        Me.C1Label22.Location = New System.Drawing.Point(23, 34)
        Me.C1Label22.Name = "C1Label22"
        Me.C1Label22.Size = New System.Drawing.Size(125, 13)
        Me.C1Label22.TabIndex = 4
        Me.C1Label22.Tag = Nothing
        Me.C1Label22.Text = "*Qualification"
        Me.C1Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label22.TextDetached = True
        '
        'C1Label27
        '
        Me.C1Label27.AttachedTextBoxName = Nothing
        Me.C1Label27.BackColor = System.Drawing.Color.Transparent
        Me.C1Label27.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label27.ForeColor = System.Drawing.Color.Black
        Me.C1Label27.Location = New System.Drawing.Point(23, 63)
        Me.C1Label27.Name = "C1Label27"
        Me.C1Label27.Size = New System.Drawing.Size(125, 13)
        Me.C1Label27.TabIndex = 0
        Me.C1Label27.Tag = Nothing
        Me.C1Label27.Text = "*Occupation"
        Me.C1Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label27.TextDetached = True
        '
        'groupboxContactDetails
        '
        Me.groupboxContactDetails.BackColor = System.Drawing.Color.White
        Me.groupboxContactDetails.Controls.Add(Me.C1Label44)
        Me.groupboxContactDetails.Controls.Add(Me.txtOffPhone)
        Me.groupboxContactDetails.Controls.Add(Me.C1Label20)
        Me.groupboxContactDetails.Controls.Add(Me.txtEmail)
        Me.groupboxContactDetails.Controls.Add(Me.C1Label23)
        Me.groupboxContactDetails.Controls.Add(Me.txtResPhone)
        Me.groupboxContactDetails.Controls.Add(Me.txtMobile)
        Me.groupboxContactDetails.Controls.Add(Me.C1Label24)
        Me.groupboxContactDetails.Controls.Add(Me.C1Label25)
        Me.groupboxContactDetails.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.groupboxContactDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.groupboxContactDetails.Location = New System.Drawing.Point(5, 4)
        Me.groupboxContactDetails.Name = "groupboxContactDetails"
        Me.groupboxContactDetails.Size = New System.Drawing.Size(451, 291)
        Me.groupboxContactDetails.TabIndex = 0
        Me.groupboxContactDetails.TabStop = False
        Me.groupboxContactDetails.Text = "Contact Details"
        '
        'C1Label44
        '
        Me.C1Label44.AttachedTextBoxName = Nothing
        Me.C1Label44.AutoSize = True
        Me.C1Label44.BackColor = System.Drawing.Color.Transparent
        Me.C1Label44.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label44.ForeColor = System.Drawing.Color.Red
        Me.C1Label44.Location = New System.Drawing.Point(21, 265)
        Me.C1Label44.Name = "C1Label44"
        Me.C1Label44.Size = New System.Drawing.Size(229, 13)
        Me.C1Label44.TabIndex = 12
        Me.C1Label44.Tag = Nothing
        Me.C1Label44.Text = "*Atleast one telephone no. is required "
        Me.C1Label44.TextDetached = True
        '
        'txtOffPhone
        '
        Me.txtOffPhone.AcceptsTab = True
        Me.txtOffPhone.AutoSize = False
        Me.txtOffPhone.BackColor = System.Drawing.Color.White
        Me.txtOffPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOffPhone.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.[Integer]
        Me.txtOffPhone.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.txtOffPhone.Location = New System.Drawing.Point(129, 89)
        Me.txtOffPhone.MaxLength = 11
        Me.txtOffPhone.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtOffPhone.MoveToNxtCtrl = Nothing
        Me.txtOffPhone.Name = "txtOffPhone"
        Me.txtOffPhone.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtOffPhone.PreValidation.TrimStart = True
        Me.txtOffPhone.Size = New System.Drawing.Size(239, 21)
        Me.txtOffPhone.TabIndex = 2
        Me.txtOffPhone.Tag = Nothing
        Me.txtOffPhone.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label20
        '
        Me.C1Label20.AttachedTextBoxName = Nothing
        Me.C1Label20.BackColor = System.Drawing.Color.Transparent
        Me.C1Label20.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label20.ForeColor = System.Drawing.Color.Black
        Me.C1Label20.Location = New System.Drawing.Point(21, 116)
        Me.C1Label20.Name = "C1Label20"
        Me.C1Label20.Size = New System.Drawing.Size(105, 13)
        Me.C1Label20.TabIndex = 11
        Me.C1Label20.Tag = Nothing
        Me.C1Label20.Text = " E-mail Address"
        Me.C1Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label20.TextDetached = True
        '
        'txtEmail
        '
        Me.txtEmail.AcceptsTab = True
        Me.txtEmail.AutoSize = False
        Me.txtEmail.BackColor = System.Drawing.Color.White
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.Location = New System.Drawing.Point(129, 116)
        Me.txtEmail.MaxLength = 55
        Me.txtEmail.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtEmail.MoveToNxtCtrl = Nothing
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtEmail.PreValidation.TrimStart = True
        Me.txtEmail.Size = New System.Drawing.Size(239, 21)
        Me.txtEmail.TabIndex = 3
        Me.txtEmail.Tag = Nothing
        Me.txtEmail.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label23
        '
        Me.C1Label23.AttachedTextBoxName = Nothing
        Me.C1Label23.BackColor = System.Drawing.Color.Transparent
        Me.C1Label23.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label23.ForeColor = System.Drawing.Color.Black
        Me.C1Label23.Location = New System.Drawing.Point(21, 90)
        Me.C1Label23.Name = "C1Label23"
        Me.C1Label23.Size = New System.Drawing.Size(105, 13)
        Me.C1Label23.TabIndex = 4
        Me.C1Label23.Tag = Nothing
        Me.C1Label23.Text = " Landline No. (O)"
        Me.C1Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label23.TextDetached = True
        '
        'txtResPhone
        '
        Me.txtResPhone.AcceptsTab = True
        Me.txtResPhone.AutoSize = False
        Me.txtResPhone.BackColor = System.Drawing.Color.White
        Me.txtResPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtResPhone.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.[Integer]
        Me.txtResPhone.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.txtResPhone.Location = New System.Drawing.Point(129, 62)
        Me.txtResPhone.MaxLength = 10
        Me.txtResPhone.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtResPhone.MoveToNxtCtrl = Nothing
        Me.txtResPhone.Name = "txtResPhone"
        Me.txtResPhone.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtResPhone.PreValidation.TrimStart = True
        Me.txtResPhone.Size = New System.Drawing.Size(239, 21)
        Me.txtResPhone.TabIndex = 1
        Me.txtResPhone.Tag = Nothing
        Me.txtResPhone.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtMobile
        '
        Me.txtMobile.AcceptsTab = True
        Me.txtMobile.AutoSize = False
        Me.txtMobile.BackColor = System.Drawing.Color.White
        Me.txtMobile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMobile.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.[Integer]
        Me.txtMobile.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.txtMobile.Location = New System.Drawing.Point(129, 34)
        Me.txtMobile.MaxLength = 10
        Me.txtMobile.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtMobile.MoveToNxtCtrl = Nothing
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtMobile.PreValidation.TrimStart = True
        Me.txtMobile.Size = New System.Drawing.Size(239, 21)
        Me.txtMobile.TabIndex = 0
        Me.txtMobile.Tag = Nothing
        Me.txtMobile.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label24
        '
        Me.C1Label24.AttachedTextBoxName = Nothing
        Me.C1Label24.BackColor = System.Drawing.Color.Transparent
        Me.C1Label24.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label24.ForeColor = System.Drawing.Color.Black
        Me.C1Label24.Location = New System.Drawing.Point(19, 62)
        Me.C1Label24.Name = "C1Label24"
        Me.C1Label24.Size = New System.Drawing.Size(105, 13)
        Me.C1Label24.TabIndex = 1
        Me.C1Label24.Tag = Nothing
        Me.C1Label24.Text = "*Landline No. (R)"
        Me.C1Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label24.TextDetached = True
        '
        'C1Label25
        '
        Me.C1Label25.AttachedTextBoxName = Nothing
        Me.C1Label25.BackColor = System.Drawing.Color.Transparent
        Me.C1Label25.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label25.ForeColor = System.Drawing.Color.Black
        Me.C1Label25.Location = New System.Drawing.Point(19, 34)
        Me.C1Label25.Name = "C1Label25"
        Me.C1Label25.Size = New System.Drawing.Size(105, 13)
        Me.C1Label25.TabIndex = 0
        Me.C1Label25.Tag = Nothing
        Me.C1Label25.Text = "*Mobile No."
        Me.C1Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label25.TextDetached = True
        '
        'TabPageDocsInfo
        '
        Me.TabPageDocsInfo.Controls.Add(Me.C1Sizer7)
        Me.TabPageDocsInfo.Location = New System.Drawing.Point(1, 25)
        Me.TabPageDocsInfo.Name = "TabPageDocsInfo"
        Me.TabPageDocsInfo.Size = New System.Drawing.Size(918, 299)
        Me.TabPageDocsInfo.TabBackColor = System.Drawing.Color.Gray
        Me.TabPageDocsInfo.TabBackColorSelected = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.TabPageDocsInfo.TabForeColor = System.Drawing.Color.White
        Me.TabPageDocsInfo.TabForeColorSelected = System.Drawing.Color.White
        Me.TabPageDocsInfo.TabIndex = 2
        Me.TabPageDocsInfo.Text = "File List"
        '
        'C1Sizer7
        '
        Me.C1Sizer7.BackColor = System.Drawing.Color.White
        Me.C1Sizer7.Controls.Add(Me.GroupBoxDocuments)
        Me.C1Sizer7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Sizer7.GridDefinition = "97.3244147157191:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "49.4553376906318:False:False;49.0196078431373:False" & _
    ":False;"
        Me.C1Sizer7.Location = New System.Drawing.Point(0, 0)
        Me.C1Sizer7.Name = "C1Sizer7"
        Me.C1Sizer7.Padding = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.C1Sizer7.Size = New System.Drawing.Size(918, 299)
        Me.C1Sizer7.TabIndex = 2
        Me.C1Sizer7.Text = "C1Sizer7"
        '
        'GroupBoxDocuments
        '
        Me.GroupBoxDocuments.BackColor = System.Drawing.Color.White
        Me.GroupBoxDocuments.Controls.Add(Me.txtAuthor)
        Me.GroupBoxDocuments.Controls.Add(Me.BtnSearchAuthor)
        Me.GroupBoxDocuments.Controls.Add(Me.dtpDocDate)
        Me.GroupBoxDocuments.Controls.Add(Me.TreeViewDocs)
        Me.GroupBoxDocuments.Controls.Add(Me.BtnNewDoc)
        Me.GroupBoxDocuments.Controls.Add(Me.cmbDocumentType)
        Me.GroupBoxDocuments.Controls.Add(Me.BtnUploadDoc)
        Me.GroupBoxDocuments.Controls.Add(Me.C1Label38)
        Me.GroupBoxDocuments.Controls.Add(Me.txtFilePath)
        Me.GroupBoxDocuments.Controls.Add(Me.C1Label33)
        Me.GroupBoxDocuments.Controls.Add(Me.C1Label35)
        Me.GroupBoxDocuments.Controls.Add(Me.txtDocDescription)
        Me.GroupBoxDocuments.Controls.Add(Me.C1Label36)
        Me.GroupBoxDocuments.Controls.Add(Me.C1Label37)
        Me.GroupBoxDocuments.Controls.Add(Me.BtnDeleteDoc)
        Me.GroupBoxDocuments.Controls.Add(Me.BtnAddDoc)
        Me.GroupBoxDocuments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBoxDocuments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.GroupBoxDocuments.Location = New System.Drawing.Point(5, 4)
        Me.GroupBoxDocuments.Name = "GroupBoxDocuments"
        Me.GroupBoxDocuments.Size = New System.Drawing.Size(908, 291)
        Me.GroupBoxDocuments.TabIndex = 0
        Me.GroupBoxDocuments.TabStop = False
        Me.GroupBoxDocuments.Text = "Documents"
        '
        'txtAuthor
        '
        Me.txtAuthor.AcceptsTab = True
        Me.txtAuthor.AutoSize = False
        Me.txtAuthor.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtAuthor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAuthor.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAuthor.Location = New System.Drawing.Point(505, 87)
        Me.txtAuthor.MaxLength = 15
        Me.txtAuthor.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtAuthor.MoveToNxtCtrl = Nothing
        Me.txtAuthor.Name = "txtAuthor"
        Me.txtAuthor.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtAuthor.PreValidation.TrimStart = True
        Me.txtAuthor.Size = New System.Drawing.Size(228, 22)
        Me.txtAuthor.TabIndex = 2
        Me.txtAuthor.Tag = Nothing
        Me.txtAuthor.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnSearchAuthor
        '
        Me.BtnSearchAuthor.Image = Global.Spectrum.My.Resources.Resources.search_2
        Me.BtnSearchAuthor.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnSearchAuthor.Location = New System.Drawing.Point(739, 87)
        Me.BtnSearchAuthor.MoveToNxtCtrl = Nothing
        Me.BtnSearchAuthor.Name = "BtnSearchAuthor"
        Me.BtnSearchAuthor.SetArticleCode = Nothing
        Me.BtnSearchAuthor.SetRowIndex = 0
        Me.BtnSearchAuthor.Size = New System.Drawing.Size(35, 22)
        Me.BtnSearchAuthor.TabIndex = 3
        Me.BtnSearchAuthor.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnSearchAuthor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnSearchAuthor.UseVisualStyleBackColor = True
        Me.BtnSearchAuthor.Visible = False
        Me.BtnSearchAuthor.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'dtpDocDate
        '
        Me.dtpDocDate.BackColor = System.Drawing.Color.White
        Me.dtpDocDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(137, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(137, Byte), Integer))
        Me.dtpDocDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtpDocDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage"), System.Drawing.Image)
        '
        '
        '
        Me.dtpDocDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtpDocDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.dtpDocDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDocDate.DisplayFormat.EmptyAsNull = True
        Me.dtpDocDate.DisplayFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.FormatType Or C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpDocDate.EditFormat.EmptyAsNull = True
        Me.dtpDocDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.FormatType Or C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpDocDate.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.dtpDocDate.Location = New System.Drawing.Point(505, 117)
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.ParseInfo.EmptyAsNull = True
        Me.dtpDocDate.ParseInfo.Inherit = CType((((((C1.Win.C1Input.ParseInfoInheritFlags.CaseSensitive Or C1.Win.C1Input.ParseInfoInheritFlags.FormatType) _
            Or C1.Win.C1Input.ParseInfoInheritFlags.CustomFormat) _
            Or C1.Win.C1Input.ParseInfoInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.ParseInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.ParseInfoInheritFlags.TrimEnd), C1.Win.C1Input.ParseInfoInheritFlags)
        Me.dtpDocDate.ParseInfo.NullText = """"""
        Me.dtpDocDate.Size = New System.Drawing.Size(269, 19)
        Me.dtpDocDate.TabIndex = 4
        Me.dtpDocDate.Tag = Nothing
        Me.dtpDocDate.TrimStart = True
        Me.dtpDocDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'TreeViewDocs
        '
        Me.TreeViewDocs.Location = New System.Drawing.Point(22, 34)
        Me.TreeViewDocs.Name = "TreeViewDocs"
        Me.TreeViewDocs.Size = New System.Drawing.Size(167, 238)
        Me.TreeViewDocs.TabIndex = 15
        Me.TreeViewDocs.TabStop = False
        '
        'BtnNewDoc
        '
        Me.BtnNewDoc.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnNewDoc.Location = New System.Drawing.Point(505, 176)
        Me.BtnNewDoc.MoveToNxtCtrl = Nothing
        Me.BtnNewDoc.Name = "BtnNewDoc"
        Me.BtnNewDoc.SetArticleCode = Nothing
        Me.BtnNewDoc.SetRowIndex = 0
        Me.BtnNewDoc.Size = New System.Drawing.Size(70, 23)
        Me.BtnNewDoc.TabIndex = 7
        Me.BtnNewDoc.Tag = "New"
        Me.BtnNewDoc.Text = "New"
        Me.BtnNewDoc.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnNewDoc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnNewDoc.UseVisualStyleBackColor = True
        Me.BtnNewDoc.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmbDocumentType
        '
        Me.cmbDocumentType.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbDocumentType.AutoCompletion = True
        Me.cmbDocumentType.AutoDropDown = True
        Me.cmbDocumentType.Caption = ""
        Me.cmbDocumentType.CaptionHeight = 17
        Me.cmbDocumentType.CaptionVisible = False
        Me.cmbDocumentType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbDocumentType.ColumnCaptionHeight = 17
        Me.cmbDocumentType.ColumnFooterHeight = 17
        Me.cmbDocumentType.ColumnHeaders = False
        Me.cmbDocumentType.ContentHeight = 16
        Me.cmbDocumentType.ctrlTextDbColumn = ""
        Me.cmbDocumentType.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbDocumentType.EditorBackColor = System.Drawing.Color.White
        Me.cmbDocumentType.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDocumentType.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbDocumentType.EditorHeight = 16
        Me.cmbDocumentType.Images.Add(CType(resources.GetObject("cmbDocumentType.Images"), System.Drawing.Image))
        Me.cmbDocumentType.ItemHeight = 15
        Me.cmbDocumentType.LimitToList = True
        Me.cmbDocumentType.Location = New System.Drawing.Point(505, 33)
        Me.cmbDocumentType.MatchEntryTimeout = CType(2000, Long)
        Me.cmbDocumentType.MaxDropDownItems = CType(5, Short)
        Me.cmbDocumentType.MaxLength = 32767
        Me.cmbDocumentType.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbDocumentType.MoveToNxtCtrl = Nothing
        Me.cmbDocumentType.Name = "cmbDocumentType"
        Me.cmbDocumentType.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbDocumentType.Size = New System.Drawing.Size(269, 22)
        Me.cmbDocumentType.strSelectStmt = ""
        Me.cmbDocumentType.SuperBack = True
        Me.cmbDocumentType.TabIndex = 0
        Me.cmbDocumentType.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbDocumentType.PropBag = resources.GetString("cmbDocumentType.PropBag")
        '
        'BtnUploadDoc
        '
        Me.BtnUploadDoc.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnUploadDoc.Location = New System.Drawing.Point(733, 140)
        Me.BtnUploadDoc.MoveToNxtCtrl = Nothing
        Me.BtnUploadDoc.Name = "BtnUploadDoc"
        Me.BtnUploadDoc.SetArticleCode = Nothing
        Me.BtnUploadDoc.SetRowIndex = 0
        Me.BtnUploadDoc.Size = New System.Drawing.Size(41, 23)
        Me.BtnUploadDoc.TabIndex = 5
        Me.BtnUploadDoc.Text = "..."
        Me.BtnUploadDoc.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnUploadDoc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnUploadDoc.UseVisualStyleBackColor = True
        Me.BtnUploadDoc.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label38
        '
        Me.C1Label38.AttachedTextBoxName = Nothing
        Me.C1Label38.AutoSize = True
        Me.C1Label38.BackColor = System.Drawing.Color.Transparent
        Me.C1Label38.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label38.ForeColor = System.Drawing.Color.Black
        Me.C1Label38.Location = New System.Drawing.Point(348, 141)
        Me.C1Label38.Name = "C1Label38"
        Me.C1Label38.Size = New System.Drawing.Size(26, 13)
        Me.C1Label38.TabIndex = 13
        Me.C1Label38.Tag = Nothing
        Me.C1Label38.Text = "File"
        Me.C1Label38.TextDetached = True
        '
        'txtFilePath
        '
        Me.txtFilePath.AcceptsTab = True
        Me.txtFilePath.AutoSize = False
        Me.txtFilePath.BackColor = System.Drawing.Color.White
        Me.txtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilePath.Location = New System.Drawing.Point(505, 141)
        Me.txtFilePath.MaxLength = 200
        Me.txtFilePath.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtFilePath.MoveToNxtCtrl = Nothing
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtFilePath.PreValidation.TrimStart = True
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(222, 21)
        Me.txtFilePath.TabIndex = 14
        Me.txtFilePath.TabStop = False
        Me.txtFilePath.Tag = Nothing
        Me.txtFilePath.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label33
        '
        Me.C1Label33.AttachedTextBoxName = Nothing
        Me.C1Label33.AutoSize = True
        Me.C1Label33.BackColor = System.Drawing.Color.Transparent
        Me.C1Label33.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label33.ForeColor = System.Drawing.Color.Black
        Me.C1Label33.Location = New System.Drawing.Point(348, 116)
        Me.C1Label33.Name = "C1Label33"
        Me.C1Label33.Size = New System.Drawing.Size(96, 13)
        Me.C1Label33.TabIndex = 12
        Me.C1Label33.Tag = Nothing
        Me.C1Label33.Text = "Document Date"
        Me.C1Label33.TextDetached = True
        '
        'C1Label35
        '
        Me.C1Label35.AttachedTextBoxName = Nothing
        Me.C1Label35.AutoSize = True
        Me.C1Label35.BackColor = System.Drawing.Color.Transparent
        Me.C1Label35.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label35.ForeColor = System.Drawing.Color.Black
        Me.C1Label35.Location = New System.Drawing.Point(348, 89)
        Me.C1Label35.Name = "C1Label35"
        Me.C1Label35.Size = New System.Drawing.Size(45, 13)
        Me.C1Label35.TabIndex = 11
        Me.C1Label35.Tag = Nothing
        Me.C1Label35.Text = "Author"
        Me.C1Label35.TextDetached = True
        '
        'txtDocDescription
        '
        Me.txtDocDescription.AcceptsTab = True
        Me.txtDocDescription.AutoSize = False
        Me.txtDocDescription.BackColor = System.Drawing.Color.White
        Me.txtDocDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDocDescription.Location = New System.Drawing.Point(505, 62)
        Me.txtDocDescription.MaxLength = 100
        Me.txtDocDescription.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtDocDescription.MoveToNxtCtrl = Nothing
        Me.txtDocDescription.Name = "txtDocDescription"
        Me.txtDocDescription.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtDocDescription.PreValidation.TrimStart = True
        Me.txtDocDescription.Size = New System.Drawing.Size(269, 21)
        Me.txtDocDescription.TabIndex = 1
        Me.txtDocDescription.Tag = Nothing
        Me.txtDocDescription.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label36
        '
        Me.C1Label36.AttachedTextBoxName = Nothing
        Me.C1Label36.AutoSize = True
        Me.C1Label36.BackColor = System.Drawing.Color.Transparent
        Me.C1Label36.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label36.ForeColor = System.Drawing.Color.Black
        Me.C1Label36.Location = New System.Drawing.Point(348, 62)
        Me.C1Label36.Name = "C1Label36"
        Me.C1Label36.Size = New System.Drawing.Size(71, 13)
        Me.C1Label36.TabIndex = 10
        Me.C1Label36.Tag = Nothing
        Me.C1Label36.Text = "Description"
        Me.C1Label36.TextDetached = True
        '
        'C1Label37
        '
        Me.C1Label37.AttachedTextBoxName = Nothing
        Me.C1Label37.AutoSize = True
        Me.C1Label37.BackColor = System.Drawing.Color.Transparent
        Me.C1Label37.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label37.ForeColor = System.Drawing.Color.Black
        Me.C1Label37.Location = New System.Drawing.Point(348, 34)
        Me.C1Label37.Name = "C1Label37"
        Me.C1Label37.Size = New System.Drawing.Size(97, 13)
        Me.C1Label37.TabIndex = 9
        Me.C1Label37.Tag = Nothing
        Me.C1Label37.Text = "Document Type"
        Me.C1Label37.TextDetached = True
        '
        'BtnDeleteDoc
        '
        Me.BtnDeleteDoc.ForeColor = System.Drawing.Color.White
        Me.BtnDeleteDoc.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnDeleteDoc.Location = New System.Drawing.Point(704, 176)
        Me.BtnDeleteDoc.MoveToNxtCtrl = Nothing
        Me.BtnDeleteDoc.Name = "BtnDeleteDoc"
        Me.BtnDeleteDoc.SetArticleCode = Nothing
        Me.BtnDeleteDoc.SetRowIndex = 0
        Me.BtnDeleteDoc.Size = New System.Drawing.Size(70, 23)
        Me.BtnDeleteDoc.TabIndex = 8
        Me.BtnDeleteDoc.Text = "Delete"
        Me.BtnDeleteDoc.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnDeleteDoc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnDeleteDoc.UseVisualStyleBackColor = True
        Me.BtnDeleteDoc.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnAddDoc
        '
        Me.BtnAddDoc.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnAddDoc.Location = New System.Drawing.Point(603, 176)
        Me.BtnAddDoc.MoveToNxtCtrl = Nothing
        Me.BtnAddDoc.Name = "BtnAddDoc"
        Me.BtnAddDoc.SetArticleCode = Nothing
        Me.BtnAddDoc.SetRowIndex = 0
        Me.BtnAddDoc.Size = New System.Drawing.Size(70, 23)
        Me.BtnAddDoc.TabIndex = 6
        Me.BtnAddDoc.Text = "Add"
        Me.BtnAddDoc.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnAddDoc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnAddDoc.UseVisualStyleBackColor = True
        Me.BtnAddDoc.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Sizer2
        '
        Me.C1Sizer2.Controls.Add(Me.GroupBoxPatientDetails)
        Me.C1Sizer2.GridDefinition = "98.6509274873524:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "98.9130434782609:False:False;"
        Me.C1Sizer2.Location = New System.Drawing.Point(5, 4)
        Me.C1Sizer2.Name = "C1Sizer2"
        Me.C1Sizer2.Padding = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.C1Sizer2.Size = New System.Drawing.Size(920, 593)
        Me.C1Sizer2.TabIndex = 0
        Me.C1Sizer2.TabStop = False
        Me.C1Sizer2.Text = "C1Sizer2"
        '
        'GroupBoxPatientDetails
        '
        Me.GroupBoxPatientDetails.BackColor = System.Drawing.SystemColors.ControlLight
        Me.GroupBoxPatientDetails.Controls.Add(Me.C1SizerPatientInfo)
        Me.GroupBoxPatientDetails.Controls.Add(Me.GroupBox5)
        Me.GroupBoxPatientDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBoxPatientDetails.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxPatientDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.GroupBoxPatientDetails.Location = New System.Drawing.Point(0, 0)
        Me.GroupBoxPatientDetails.Name = "GroupBoxPatientDetails"
        Me.GroupBoxPatientDetails.Size = New System.Drawing.Size(920, 593)
        Me.GroupBoxPatientDetails.TabIndex = 0
        Me.GroupBoxPatientDetails.TabStop = False
        Me.GroupBoxPatientDetails.Text = "Patient Details"
        '
        'C1SizerPatientInfo
        '
        Me.C1SizerPatientInfo.Controls.Add(Me.CtrlLabel3)
        Me.C1SizerPatientInfo.Controls.Add(Me.C1Label34)
        Me.C1SizerPatientInfo.Controls.Add(Me.CtrlLabel2)
        Me.C1SizerPatientInfo.Controls.Add(Me.C1Label30)
        Me.C1SizerPatientInfo.Controls.Add(Me.C1Label42)
        Me.C1SizerPatientInfo.Controls.Add(Me.C1Label41)
        Me.C1SizerPatientInfo.Controls.Add(Me.C1Label29)
        Me.C1SizerPatientInfo.Controls.Add(Me.Panel2)
        Me.C1SizerPatientInfo.Controls.Add(Me.cmbPatientBloodGroup)
        Me.C1SizerPatientInfo.Controls.Add(Me.C1Label50)
        Me.C1SizerPatientInfo.Controls.Add(Me.C1Label49)
        Me.C1SizerPatientInfo.Controls.Add(Me.txtPatientID)
        Me.C1SizerPatientInfo.Controls.Add(Me.txtHeightCm)
        Me.C1SizerPatientInfo.Controls.Add(Me.txtWeightKg)
        Me.C1SizerPatientInfo.Controls.Add(Me.dtpDob)
        Me.C1SizerPatientInfo.Controls.Add(Me.txtRefDoctorName)
        Me.C1SizerPatientInfo.Controls.Add(Me.lblPatientidname)
        Me.C1SizerPatientInfo.Controls.Add(Me.cboGender)
        Me.C1SizerPatientInfo.Controls.Add(Me.cmbAgeMonths)
        Me.C1SizerPatientInfo.Controls.Add(Me.cmbAgeYears)
        Me.C1SizerPatientInfo.Controls.Add(Me.C1Label1)
        Me.C1SizerPatientInfo.Controls.Add(Me.BtnSearchPatient)
        Me.C1SizerPatientInfo.Controls.Add(Me.cmbSalutation)
        Me.C1SizerPatientInfo.Controls.Add(Me.BtnNewRefDr)
        Me.C1SizerPatientInfo.Controls.Add(Me.C1Label2)
        Me.C1SizerPatientInfo.Controls.Add(Me.BtnSearchRefDr)
        Me.C1SizerPatientInfo.Controls.Add(Me.s)
        Me.C1SizerPatientInfo.Controls.Add(Me.txtFirstName)
        Me.C1SizerPatientInfo.Controls.Add(Me.C1Label5)
        Me.C1SizerPatientInfo.Controls.Add(Me.C1Label4)
        Me.C1SizerPatientInfo.Controls.Add(Me.txtMiddleName)
        Me.C1SizerPatientInfo.Controls.Add(Me.C1Label7)
        Me.C1SizerPatientInfo.Controls.Add(Me.C1Label3)
        Me.C1SizerPatientInfo.Controls.Add(Me.txtLastName)
        Me.C1SizerPatientInfo.GridDefinition = resources.GetString("C1SizerPatientInfo.GridDefinition")
        Me.C1SizerPatientInfo.Location = New System.Drawing.Point(6, 25)
        Me.C1SizerPatientInfo.Margin = New System.Windows.Forms.Padding(0)
        Me.C1SizerPatientInfo.Name = "C1SizerPatientInfo"
        Me.C1SizerPatientInfo.Padding = New System.Windows.Forms.Padding(0)
        Me.C1SizerPatientInfo.Size = New System.Drawing.Size(682, 188)
        Me.C1SizerPatientInfo.SplitterWidth = 2
        Me.C1SizerPatientInfo.TabIndex = 0
        Me.C1SizerPatientInfo.TabStop = False
        Me.C1SizerPatientInfo.Text = "C1Sizer6"
        '
        'CtrlLabel3
        '
        Me.CtrlLabel3.AttachedTextBoxName = Nothing
        Me.CtrlLabel3.BackColor = System.Drawing.SystemColors.ControlLight
        Me.CtrlLabel3.BorderColor = System.Drawing.SystemColors.ControlLight
        Me.CtrlLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel3.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel3.Location = New System.Drawing.Point(351, 127)
        Me.CtrlLabel3.Name = "CtrlLabel3"
        Me.CtrlLabel3.Size = New System.Drawing.Size(83, 30)
        Me.CtrlLabel3.TabIndex = 30
        Me.CtrlLabel3.Tag = Nothing
        Me.CtrlLabel3.Text = "Blood Group"
        Me.CtrlLabel3.TextDetached = True
        Me.CtrlLabel3.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label34
        '
        Me.C1Label34.AttachedTextBoxName = Nothing
        Me.C1Label34.BackColor = System.Drawing.Color.Transparent
        Me.C1Label34.BorderColor = System.Drawing.Color.LightGray
        Me.C1Label34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.C1Label34.ForeColor = System.Drawing.Color.Black
        Me.C1Label34.Location = New System.Drawing.Point(0, 159)
        Me.C1Label34.Name = "C1Label34"
        Me.C1Label34.Size = New System.Drawing.Size(92, 21)
        Me.C1Label34.TabIndex = 19
        Me.C1Label34.Tag = Nothing
        Me.C1Label34.Text = "Referred by Dr. :"
        Me.C1Label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.C1Label34.TextDetached = True
        Me.C1Label34.Visible = False
        Me.C1Label34.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel2
        '
        Me.CtrlLabel2.AttachedTextBoxName = Nothing
        Me.CtrlLabel2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel2.BorderColor = System.Drawing.Color.LightGray
        Me.CtrlLabel2.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel2.Location = New System.Drawing.Point(0, 127)
        Me.CtrlLabel2.Name = "CtrlLabel2"
        Me.CtrlLabel2.Size = New System.Drawing.Size(92, 30)
        Me.CtrlLabel2.TabIndex = 32
        Me.CtrlLabel2.Tag = Nothing
        Me.CtrlLabel2.Text = "Nature Of Body"
        Me.CtrlLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CtrlLabel2.TextDetached = True
        Me.CtrlLabel2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label30
        '
        Me.C1Label30.AttachedTextBoxName = Nothing
        Me.C1Label30.BackColor = System.Drawing.Color.Transparent
        Me.C1Label30.BorderColor = System.Drawing.Color.Transparent
        Me.C1Label30.ForeColor = System.Drawing.Color.Black
        Me.C1Label30.Location = New System.Drawing.Point(351, 103)
        Me.C1Label30.Name = "C1Label30"
        Me.C1Label30.Size = New System.Drawing.Size(83, 22)
        Me.C1Label30.TabIndex = 24
        Me.C1Label30.Tag = Nothing
        Me.C1Label30.Text = "Weight"
        Me.C1Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.C1Label30.TextDetached = True
        Me.C1Label30.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label42
        '
        Me.C1Label42.AttachedTextBoxName = Nothing
        Me.C1Label42.AutoSize = True
        Me.C1Label42.BackColor = System.Drawing.Color.Transparent
        Me.C1Label42.BorderColor = System.Drawing.Color.Transparent
        Me.C1Label42.ForeColor = System.Drawing.Color.Black
        Me.C1Label42.Location = New System.Drawing.Point(495, 103)
        Me.C1Label42.Name = "C1Label42"
        Me.C1Label42.Size = New System.Drawing.Size(21, 13)
        Me.C1Label42.TabIndex = 28
        Me.C1Label42.Tag = Nothing
        Me.C1Label42.Text = "kg"
        Me.C1Label42.TextDetached = True
        Me.C1Label42.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label41
        '
        Me.C1Label41.AttachedTextBoxName = Nothing
        Me.C1Label41.AutoSize = True
        Me.C1Label41.BackColor = System.Drawing.Color.Transparent
        Me.C1Label41.BorderColor = System.Drawing.Color.Transparent
        Me.C1Label41.ForeColor = System.Drawing.Color.Black
        Me.C1Label41.Location = New System.Drawing.Point(495, 79)
        Me.C1Label41.Name = "C1Label41"
        Me.C1Label41.Size = New System.Drawing.Size(24, 13)
        Me.C1Label41.TabIndex = 27
        Me.C1Label41.Tag = Nothing
        Me.C1Label41.Text = "cm"
        Me.C1Label41.TextDetached = True
        Me.C1Label41.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label29
        '
        Me.C1Label29.AttachedTextBoxName = Nothing
        Me.C1Label29.BackColor = System.Drawing.Color.Transparent
        Me.C1Label29.BorderColor = System.Drawing.Color.Transparent
        Me.C1Label29.ForeColor = System.Drawing.Color.Black
        Me.C1Label29.Location = New System.Drawing.Point(351, 79)
        Me.C1Label29.Name = "C1Label29"
        Me.C1Label29.Size = New System.Drawing.Size(83, 22)
        Me.C1Label29.TabIndex = 23
        Me.C1Label29.Tag = Nothing
        Me.C1Label29.Text = "Height"
        Me.C1Label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.C1Label29.TextDetached = True
        Me.C1Label29.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chckKaff)
        Me.Panel2.Controls.Add(Me.chckPitta)
        Me.Panel2.Controls.Add(Me.chckVaat)
        Me.Panel2.Location = New System.Drawing.Point(94, 127)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(255, 30)
        Me.Panel2.TabIndex = 33
        '
        'chckKaff
        '
        Me.chckKaff.AutoSize = True
        Me.chckKaff.ForeColor = System.Drawing.Color.Black
        Me.chckKaff.Location = New System.Drawing.Point(158, 6)
        Me.chckKaff.Name = "chckKaff"
        Me.chckKaff.Size = New System.Drawing.Size(49, 17)
        Me.chckKaff.TabIndex = 2
        Me.chckKaff.Text = "Kaff"
        Me.chckKaff.UseVisualStyleBackColor = True
        '
        'chckPitta
        '
        Me.chckPitta.AutoSize = True
        Me.chckPitta.ForeColor = System.Drawing.Color.Black
        Me.chckPitta.Location = New System.Drawing.Point(89, 6)
        Me.chckPitta.Name = "chckPitta"
        Me.chckPitta.Size = New System.Drawing.Size(51, 17)
        Me.chckPitta.TabIndex = 1
        Me.chckPitta.Text = "Pitta"
        Me.chckPitta.UseVisualStyleBackColor = True
        '
        'chckVaat
        '
        Me.chckVaat.AutoSize = True
        Me.chckVaat.ForeColor = System.Drawing.Color.Black
        Me.chckVaat.Location = New System.Drawing.Point(14, 6)
        Me.chckVaat.Name = "chckVaat"
        Me.chckVaat.Size = New System.Drawing.Size(52, 17)
        Me.chckVaat.TabIndex = 0
        Me.chckVaat.Text = "Vaat"
        Me.chckVaat.UseVisualStyleBackColor = True
        '
        'cmbPatientBloodGroup
        '
        Me.cmbPatientBloodGroup.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbPatientBloodGroup.AutoCompletion = True
        Me.cmbPatientBloodGroup.AutoDropDown = True
        Me.cmbPatientBloodGroup.Caption = ""
        Me.cmbPatientBloodGroup.CaptionHeight = 17
        Me.cmbPatientBloodGroup.CaptionVisible = False
        Me.cmbPatientBloodGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbPatientBloodGroup.ColumnCaptionHeight = 17
        Me.cmbPatientBloodGroup.ColumnFooterHeight = 17
        Me.cmbPatientBloodGroup.ColumnHeaders = False
        Me.cmbPatientBloodGroup.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList
        Me.cmbPatientBloodGroup.ContentHeight = 16
        Me.cmbPatientBloodGroup.ctrlTextDbColumn = ""
        Me.cmbPatientBloodGroup.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbPatientBloodGroup.EditorBackColor = System.Drawing.Color.White
        Me.cmbPatientBloodGroup.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPatientBloodGroup.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbPatientBloodGroup.EditorHeight = 16
        Me.cmbPatientBloodGroup.Images.Add(CType(resources.GetObject("cmbPatientBloodGroup.Images"), System.Drawing.Image))
        Me.cmbPatientBloodGroup.ItemHeight = 15
        Me.cmbPatientBloodGroup.LimitToList = True
        Me.cmbPatientBloodGroup.Location = New System.Drawing.Point(436, 127)
        Me.cmbPatientBloodGroup.MatchEntryTimeout = CType(2000, Long)
        Me.cmbPatientBloodGroup.MaxDropDownItems = CType(5, Short)
        Me.cmbPatientBloodGroup.MaxLength = 32767
        Me.cmbPatientBloodGroup.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbPatientBloodGroup.MoveToNxtCtrl = Nothing
        Me.cmbPatientBloodGroup.Name = "cmbPatientBloodGroup"
        Me.cmbPatientBloodGroup.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbPatientBloodGroup.Size = New System.Drawing.Size(57, 22)
        Me.cmbPatientBloodGroup.strSelectStmt = ""
        Me.cmbPatientBloodGroup.SuperBack = True
        Me.cmbPatientBloodGroup.TabIndex = 31
        Me.cmbPatientBloodGroup.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbPatientBloodGroup.PropBag = resources.GetString("cmbPatientBloodGroup.PropBag")
        '
        'C1Label50
        '
        Me.C1Label50.AttachedTextBoxName = Nothing
        Me.C1Label50.BackColor = System.Drawing.Color.Transparent
        Me.C1Label50.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label50.ForeColor = System.Drawing.Color.Black
        Me.C1Label50.Location = New System.Drawing.Point(593, 55)
        Me.C1Label50.Name = "C1Label50"
        Me.C1Label50.Size = New System.Drawing.Size(51, 22)
        Me.C1Label50.TabIndex = 26
        Me.C1Label50.Tag = Nothing
        Me.C1Label50.Text = "Months"
        Me.C1Label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label50.TextDetached = True
        Me.C1Label50.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label49
        '
        Me.C1Label49.AttachedTextBoxName = Nothing
        Me.C1Label49.BackColor = System.Drawing.Color.Transparent
        Me.C1Label49.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label49.ForeColor = System.Drawing.Color.Black
        Me.C1Label49.Location = New System.Drawing.Point(495, 55)
        Me.C1Label49.Name = "C1Label49"
        Me.C1Label49.Size = New System.Drawing.Size(45, 22)
        Me.C1Label49.TabIndex = 25
        Me.C1Label49.Tag = Nothing
        Me.C1Label49.Text = "Years"
        Me.C1Label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.C1Label49.TextDetached = True
        Me.C1Label49.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtPatientID
        '
        Me.txtPatientID.AcceptsTab = True
        Me.txtPatientID.AutoSize = False
        Me.txtPatientID.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtPatientID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPatientID.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPatientID.Location = New System.Drawing.Point(94, 0)
        Me.txtPatientID.MaxLength = 15
        Me.txtPatientID.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtPatientID.MoveToNxtCtrl = Nothing
        Me.txtPatientID.Name = "txtPatientID"
        Me.txtPatientID.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtPatientID.PreValidation.TrimStart = True
        Me.txtPatientID.Size = New System.Drawing.Size(146, 29)
        Me.txtPatientID.TabIndex = 0
        Me.txtPatientID.Tag = Nothing
        Me.txtPatientID.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtHeightCm
        '
        Me.txtHeightCm.AcceptsTab = True
        Me.txtHeightCm.AutoSize = False
        Me.txtHeightCm.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtHeightCm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHeightCm.DataType = GetType(Short)
        Me.txtHeightCm.EmptyAsNull = True
        Me.txtHeightCm.FormatType = C1.Win.C1Input.FormatTypeEnum.GeneralNumber
        Me.txtHeightCm.Location = New System.Drawing.Point(436, 79)
        Me.txtHeightCm.MaxLength = 3
        Me.txtHeightCm.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtHeightCm.MoveToNxtCtrl = Nothing
        Me.txtHeightCm.Name = "txtHeightCm"
        Me.txtHeightCm.Size = New System.Drawing.Size(57, 22)
        Me.txtHeightCm.TabIndex = 12
        Me.txtHeightCm.Tag = Nothing
        Me.txtHeightCm.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtWeightKg
        '
        Me.txtWeightKg.AcceptsTab = True
        Me.txtWeightKg.AutoSize = False
        Me.txtWeightKg.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtWeightKg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeightKg.DataType = GetType(Double)
        Me.txtWeightKg.EmptyAsNull = True
        Me.txtWeightKg.FormatType = C1.Win.C1Input.FormatTypeEnum.GeneralNumber
        Me.txtWeightKg.Location = New System.Drawing.Point(436, 103)
        Me.txtWeightKg.MaxLength = 3
        Me.txtWeightKg.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtWeightKg.MoveToNxtCtrl = Nothing
        Me.txtWeightKg.Name = "txtWeightKg"
        Me.txtWeightKg.Size = New System.Drawing.Size(57, 22)
        Me.txtWeightKg.TabIndex = 13
        Me.txtWeightKg.Tag = Nothing
        Me.txtWeightKg.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'dtpDob
        '
        Me.dtpDob.BackColor = System.Drawing.Color.White
        Me.dtpDob.BorderColor = System.Drawing.Color.Transparent
        Me.dtpDob.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dtpDob.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage1"), System.Drawing.Image)
        '
        '
        '
        Me.dtpDob.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtpDob.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.dtpDob.CustomFormat = "dd/MM/yyyy"
        Me.dtpDob.DisplayFormat.EmptyAsNull = True
        Me.dtpDob.DisplayFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.FormatType Or C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpDob.EditFormat.EmptyAsNull = True
        Me.dtpDob.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.FormatType Or C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpDob.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat
        Me.dtpDob.Location = New System.Drawing.Point(436, 31)
        Me.dtpDob.Name = "dtpDob"
        Me.dtpDob.ParseInfo.EmptyAsNull = True
        Me.dtpDob.ParseInfo.Inherit = CType((((((C1.Win.C1Input.ParseInfoInheritFlags.CaseSensitive Or C1.Win.C1Input.ParseInfoInheritFlags.FormatType) _
            Or C1.Win.C1Input.ParseInfoInheritFlags.CustomFormat) _
            Or C1.Win.C1Input.ParseInfoInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.ParseInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.ParseInfoInheritFlags.TrimEnd), C1.Win.C1Input.ParseInfoInheritFlags)
        Me.dtpDob.ParseInfo.NullText = """"""
        Me.dtpDob.Size = New System.Drawing.Size(155, 14)
        Me.dtpDob.TabIndex = 9
        Me.dtpDob.Tag = Nothing
        Me.dtpDob.TrimStart = True
        Me.dtpDob.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtRefDoctorName
        '
        Me.txtRefDoctorName.AcceptsTab = True
        Me.txtRefDoctorName.AutoSize = False
        Me.txtRefDoctorName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtRefDoctorName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefDoctorName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefDoctorName.Location = New System.Drawing.Point(94, 159)
        Me.txtRefDoctorName.MaxLength = 15
        Me.txtRefDoctorName.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtRefDoctorName.MoveToNxtCtrl = Nothing
        Me.txtRefDoctorName.Name = "txtRefDoctorName"
        Me.txtRefDoctorName.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtRefDoctorName.PreValidation.TrimStart = True
        Me.txtRefDoctorName.Size = New System.Drawing.Size(146, 21)
        Me.txtRefDoctorName.TabIndex = 29
        Me.txtRefDoctorName.TabStop = False
        Me.txtRefDoctorName.Tag = Nothing
        Me.txtRefDoctorName.Visible = False
        Me.txtRefDoctorName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblPatientidname
        '
        Me.lblPatientidname.AttachedTextBoxName = Nothing
        Me.lblPatientidname.BackColor = System.Drawing.SystemColors.ControlLight
        Me.lblPatientidname.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblPatientidname.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientidname.ForeColor = System.Drawing.Color.Black
        Me.lblPatientidname.Location = New System.Drawing.Point(0, 6)
        Me.lblPatientidname.Name = "lblPatientidname"
        Me.lblPatientidname.Size = New System.Drawing.Size(92, 23)
        Me.lblPatientidname.TabIndex = 14
        Me.lblPatientidname.Tag = Nothing
        Me.lblPatientidname.Text = "Patient ID"
        Me.lblPatientidname.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblPatientidname.TextDetached = True
        Me.lblPatientidname.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cboGender
        '
        Me.cboGender.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboGender.AutoCompletion = True
        Me.cboGender.AutoDropDown = True
        Me.cboGender.Caption = ""
        Me.cboGender.CaptionHeight = 17
        Me.cboGender.CaptionVisible = False
        Me.cboGender.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboGender.ColumnCaptionHeight = 17
        Me.cboGender.ColumnFooterHeight = 17
        Me.cboGender.ColumnHeaders = False
        Me.cboGender.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList
        Me.cboGender.ContentHeight = 16
        Me.cboGender.ctrlTextDbColumn = ""
        Me.cboGender.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboGender.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboGender.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGender.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboGender.EditorHeight = 16
        Me.cboGender.Images.Add(CType(resources.GetObject("cboGender.Images"), System.Drawing.Image))
        Me.cboGender.ItemHeight = 15
        Me.cboGender.Location = New System.Drawing.Point(436, 6)
        Me.cboGender.MatchEntryTimeout = CType(2000, Long)
        Me.cboGender.MaxDropDownItems = CType(5, Short)
        Me.cboGender.MaxLength = 32767
        Me.cboGender.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboGender.MoveToNxtCtrl = Nothing
        Me.cboGender.Name = "cboGender"
        Me.cboGender.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboGender.Size = New System.Drawing.Size(155, 22)
        Me.cboGender.strSelectStmt = ""
        Me.cboGender.TabIndex = 8
        Me.cboGender.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cboGender.PropBag = resources.GetString("cboGender.PropBag")
        '
        'cmbAgeMonths
        '
        Me.cmbAgeMonths.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbAgeMonths.AutoCompletion = True
        Me.cmbAgeMonths.AutoDropDown = True
        Me.cmbAgeMonths.Caption = ""
        Me.cmbAgeMonths.CaptionHeight = 17
        Me.cmbAgeMonths.CaptionVisible = False
        Me.cmbAgeMonths.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbAgeMonths.ColumnCaptionHeight = 17
        Me.cmbAgeMonths.ColumnFooterHeight = 17
        Me.cmbAgeMonths.ColumnHeaders = False
        Me.cmbAgeMonths.ColumnWidth = 100
        Me.cmbAgeMonths.ContentHeight = 16
        Me.cmbAgeMonths.ctrlTextDbColumn = ""
        Me.cmbAgeMonths.DataMode = C1.Win.C1List.DataModeEnum.AddItem
        Me.cmbAgeMonths.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbAgeMonths.EditorBackColor = System.Drawing.Color.White
        Me.cmbAgeMonths.EditorFont = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmbAgeMonths.EditorForeColor = System.Drawing.Color.Black
        Me.cmbAgeMonths.EditorHeight = 16
        Me.cmbAgeMonths.Images.Add(CType(resources.GetObject("cmbAgeMonths.Images"), System.Drawing.Image))
        Me.cmbAgeMonths.ItemHeight = 15
        Me.cmbAgeMonths.LimitToList = True
        Me.cmbAgeMonths.Location = New System.Drawing.Point(542, 55)
        Me.cmbAgeMonths.MatchEntryTimeout = CType(2000, Long)
        Me.cmbAgeMonths.MaxDropDownItems = CType(5, Short)
        Me.cmbAgeMonths.MaxLength = 32767
        Me.cmbAgeMonths.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbAgeMonths.MoveToNxtCtrl = Nothing
        Me.cmbAgeMonths.Name = "cmbAgeMonths"
        Me.cmbAgeMonths.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbAgeMonths.Size = New System.Drawing.Size(49, 22)
        Me.cmbAgeMonths.strSelectStmt = ""
        Me.cmbAgeMonths.SuperBack = True
        Me.cmbAgeMonths.TabIndex = 11
        Me.cmbAgeMonths.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbAgeMonths.PropBag = resources.GetString("cmbAgeMonths.PropBag")
        '
        'cmbAgeYears
        '
        Me.cmbAgeYears.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbAgeYears.AutoCompletion = True
        Me.cmbAgeYears.AutoDropDown = True
        Me.cmbAgeYears.Caption = ""
        Me.cmbAgeYears.CaptionHeight = 17
        Me.cmbAgeYears.CaptionVisible = False
        Me.cmbAgeYears.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbAgeYears.ColumnCaptionHeight = 17
        Me.cmbAgeYears.ColumnFooterHeight = 17
        Me.cmbAgeYears.ColumnHeaders = False
        Me.cmbAgeYears.ColumnWidth = 100
        Me.cmbAgeYears.ContentHeight = 16
        Me.cmbAgeYears.ctrlTextDbColumn = ""
        Me.cmbAgeYears.DataMode = C1.Win.C1List.DataModeEnum.AddItem
        Me.cmbAgeYears.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbAgeYears.EditorBackColor = System.Drawing.Color.White
        Me.cmbAgeYears.EditorFont = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmbAgeYears.EditorForeColor = System.Drawing.Color.Black
        Me.cmbAgeYears.EditorHeight = 16
        Me.cmbAgeYears.Images.Add(CType(resources.GetObject("cmbAgeYears.Images"), System.Drawing.Image))
        Me.cmbAgeYears.ItemHeight = 15
        Me.cmbAgeYears.LimitToList = True
        Me.cmbAgeYears.Location = New System.Drawing.Point(436, 55)
        Me.cmbAgeYears.MatchEntryTimeout = CType(2000, Long)
        Me.cmbAgeYears.MaxDropDownItems = CType(5, Short)
        Me.cmbAgeYears.MaxLength = 32767
        Me.cmbAgeYears.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbAgeYears.MoveToNxtCtrl = Nothing
        Me.cmbAgeYears.Name = "cmbAgeYears"
        Me.cmbAgeYears.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbAgeYears.Size = New System.Drawing.Size(57, 22)
        Me.cmbAgeYears.strSelectStmt = ""
        Me.cmbAgeYears.SuperBack = True
        Me.cmbAgeYears.TabIndex = 10
        Me.cmbAgeYears.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbAgeYears.PropBag = resources.GetString("cmbAgeYears.PropBag")
        '
        'C1Label1
        '
        Me.C1Label1.AttachedTextBoxName = Nothing
        Me.C1Label1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.C1Label1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Label1.ForeColor = System.Drawing.Color.Black
        Me.C1Label1.Location = New System.Drawing.Point(0, 31)
        Me.C1Label1.Name = "C1Label1"
        Me.C1Label1.Size = New System.Drawing.Size(92, 22)
        Me.C1Label1.TabIndex = 15
        Me.C1Label1.Tag = Nothing
        Me.C1Label1.Text = "Salutation"
        Me.C1Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.C1Label1.TextDetached = True
        Me.C1Label1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnSearchPatient
        '
        Me.BtnSearchPatient.Image = Global.Spectrum.My.Resources.Resources.search_2
        Me.BtnSearchPatient.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnSearchPatient.Location = New System.Drawing.Point(242, 6)
        Me.BtnSearchPatient.MoveToNxtCtrl = Nothing
        Me.BtnSearchPatient.Name = "BtnSearchPatient"
        Me.BtnSearchPatient.SetArticleCode = Nothing
        Me.BtnSearchPatient.SetRowIndex = 0
        Me.BtnSearchPatient.Size = New System.Drawing.Size(35, 23)
        Me.BtnSearchPatient.TabIndex = 1
        Me.BtnSearchPatient.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnSearchPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnSearchPatient.UseVisualStyleBackColor = True
        Me.BtnSearchPatient.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmbSalutation
        '
        Me.cmbSalutation.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbSalutation.AutoCompletion = True
        Me.cmbSalutation.AutoDropDown = True
        Me.cmbSalutation.Caption = ""
        Me.cmbSalutation.CaptionHeight = 17
        Me.cmbSalutation.CaptionVisible = False
        Me.cmbSalutation.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbSalutation.ColumnCaptionHeight = 17
        Me.cmbSalutation.ColumnFooterHeight = 17
        Me.cmbSalutation.ColumnHeaders = False
        Me.cmbSalutation.ContentHeight = 16
        Me.cmbSalutation.ctrlTextDbColumn = ""
        Me.cmbSalutation.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbSalutation.EditorBackColor = System.Drawing.Color.White
        Me.cmbSalutation.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSalutation.EditorForeColor = System.Drawing.Color.Black
        Me.cmbSalutation.EditorHeight = 16
        Me.cmbSalutation.Images.Add(CType(resources.GetObject("cmbSalutation.Images"), System.Drawing.Image))
        Me.cmbSalutation.ItemHeight = 15
        Me.cmbSalutation.LimitToList = True
        Me.cmbSalutation.Location = New System.Drawing.Point(94, 31)
        Me.cmbSalutation.MatchEntryTimeout = CType(2000, Long)
        Me.cmbSalutation.MaxDropDownItems = CType(5, Short)
        Me.cmbSalutation.MaxLength = 32767
        Me.cmbSalutation.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbSalutation.MoveToNxtCtrl = Nothing
        Me.cmbSalutation.Name = "cmbSalutation"
        Me.cmbSalutation.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbSalutation.Size = New System.Drawing.Size(146, 22)
        Me.cmbSalutation.strSelectStmt = ""
        Me.cmbSalutation.SuperBack = True
        Me.cmbSalutation.TabIndex = 2
        Me.cmbSalutation.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbSalutation.PropBag = resources.GetString("cmbSalutation.PropBag")
        '
        'BtnNewRefDr
        '
        Me.BtnNewRefDr.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnNewRefDr.Location = New System.Drawing.Point(279, 159)
        Me.BtnNewRefDr.MoveToNxtCtrl = Nothing
        Me.BtnNewRefDr.Name = "BtnNewRefDr"
        Me.BtnNewRefDr.SetArticleCode = Nothing
        Me.BtnNewRefDr.SetRowIndex = 0
        Me.BtnNewRefDr.Size = New System.Drawing.Size(70, 21)
        Me.BtnNewRefDr.TabIndex = 7
        Me.BtnNewRefDr.Text = "Add New"
        Me.BtnNewRefDr.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnNewRefDr.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnNewRefDr.UseVisualStyleBackColor = True
        Me.BtnNewRefDr.Visible = False
        Me.BtnNewRefDr.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label2
        '
        Me.C1Label2.AttachedTextBoxName = Nothing
        Me.C1Label2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.C1Label2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Label2.ForeColor = System.Drawing.Color.Black
        Me.C1Label2.Location = New System.Drawing.Point(0, 55)
        Me.C1Label2.Name = "C1Label2"
        Me.C1Label2.Size = New System.Drawing.Size(92, 22)
        Me.C1Label2.TabIndex = 16
        Me.C1Label2.Tag = Nothing
        Me.C1Label2.Text = "*First Name"
        Me.C1Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.C1Label2.TextDetached = True
        Me.C1Label2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnSearchRefDr
        '
        Me.BtnSearchRefDr.Image = Global.Spectrum.My.Resources.Resources.search_2
        Me.BtnSearchRefDr.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnSearchRefDr.Location = New System.Drawing.Point(242, 159)
        Me.BtnSearchRefDr.MoveToNxtCtrl = Nothing
        Me.BtnSearchRefDr.Name = "BtnSearchRefDr"
        Me.BtnSearchRefDr.SetArticleCode = Nothing
        Me.BtnSearchRefDr.SetRowIndex = 0
        Me.BtnSearchRefDr.Size = New System.Drawing.Size(35, 21)
        Me.BtnSearchRefDr.TabIndex = 6
        Me.BtnSearchRefDr.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnSearchRefDr.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnSearchRefDr.UseVisualStyleBackColor = True
        Me.BtnSearchRefDr.Visible = False
        Me.BtnSearchRefDr.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        's
        '
        Me.s.AttachedTextBoxName = Nothing
        Me.s.BackColor = System.Drawing.SystemColors.ControlLight
        Me.s.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.s.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.s.ForeColor = System.Drawing.Color.Black
        Me.s.Location = New System.Drawing.Point(351, 31)
        Me.s.Name = "s"
        Me.s.Size = New System.Drawing.Size(83, 22)
        Me.s.TabIndex = 21
        Me.s.Tag = Nothing
        Me.s.Text = "D.O.B"
        Me.s.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.s.TextDetached = True
        Me.s.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtFirstName
        '
        Me.txtFirstName.AcceptsTab = True
        Me.txtFirstName.AutoSize = False
        Me.txtFirstName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtFirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFirstName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFirstName.Location = New System.Drawing.Point(94, 55)
        Me.txtFirstName.MaxLength = 30
        Me.txtFirstName.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtFirstName.MoveToNxtCtrl = Nothing
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtFirstName.PreValidation.TrimStart = True
        Me.txtFirstName.Size = New System.Drawing.Size(146, 22)
        Me.txtFirstName.TabIndex = 3
        Me.txtFirstName.Tag = Nothing
        Me.txtFirstName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label5
        '
        Me.C1Label5.AttachedTextBoxName = Nothing
        Me.C1Label5.BackColor = System.Drawing.SystemColors.ControlLight
        Me.C1Label5.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Label5.ForeColor = System.Drawing.Color.Black
        Me.C1Label5.Location = New System.Drawing.Point(351, 55)
        Me.C1Label5.Name = "C1Label5"
        Me.C1Label5.Size = New System.Drawing.Size(83, 22)
        Me.C1Label5.TabIndex = 22
        Me.C1Label5.Tag = Nothing
        Me.C1Label5.Text = "Age"
        Me.C1Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.C1Label5.TextDetached = True
        Me.C1Label5.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label4
        '
        Me.C1Label4.AttachedTextBoxName = Nothing
        Me.C1Label4.BackColor = System.Drawing.SystemColors.ControlLight
        Me.C1Label4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Label4.ForeColor = System.Drawing.Color.Black
        Me.C1Label4.Location = New System.Drawing.Point(0, 79)
        Me.C1Label4.Name = "C1Label4"
        Me.C1Label4.Size = New System.Drawing.Size(92, 22)
        Me.C1Label4.TabIndex = 17
        Me.C1Label4.Tag = Nothing
        Me.C1Label4.Text = "Middle Name"
        Me.C1Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.C1Label4.TextDetached = True
        Me.C1Label4.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtMiddleName
        '
        Me.txtMiddleName.AcceptsTab = True
        Me.txtMiddleName.AutoSize = False
        Me.txtMiddleName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtMiddleName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMiddleName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMiddleName.Location = New System.Drawing.Point(94, 79)
        Me.txtMiddleName.MaxLength = 30
        Me.txtMiddleName.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtMiddleName.MoveToNxtCtrl = Nothing
        Me.txtMiddleName.Name = "txtMiddleName"
        Me.txtMiddleName.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtMiddleName.PreValidation.TrimStart = True
        Me.txtMiddleName.Size = New System.Drawing.Size(146, 22)
        Me.txtMiddleName.TabIndex = 4
        Me.txtMiddleName.Tag = Nothing
        Me.txtMiddleName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label7
        '
        Me.C1Label7.AttachedTextBoxName = Nothing
        Me.C1Label7.BackColor = System.Drawing.SystemColors.ControlLight
        Me.C1Label7.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Label7.ForeColor = System.Drawing.Color.Black
        Me.C1Label7.Location = New System.Drawing.Point(0, 103)
        Me.C1Label7.Name = "C1Label7"
        Me.C1Label7.Size = New System.Drawing.Size(92, 22)
        Me.C1Label7.TabIndex = 18
        Me.C1Label7.Tag = Nothing
        Me.C1Label7.Text = "*Last Name"
        Me.C1Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.C1Label7.TextDetached = True
        Me.C1Label7.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Label3
        '
        Me.C1Label3.AttachedTextBoxName = Nothing
        Me.C1Label3.BackColor = System.Drawing.SystemColors.ControlLight
        Me.C1Label3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.C1Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Label3.ForeColor = System.Drawing.Color.Black
        Me.C1Label3.Location = New System.Drawing.Point(351, 6)
        Me.C1Label3.Name = "C1Label3"
        Me.C1Label3.Size = New System.Drawing.Size(83, 23)
        Me.C1Label3.TabIndex = 20
        Me.C1Label3.Tag = Nothing
        Me.C1Label3.Text = "*Gender"
        Me.C1Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.C1Label3.TextDetached = True
        Me.C1Label3.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtLastName
        '
        Me.txtLastName.AcceptsTab = True
        Me.txtLastName.AutoSize = False
        Me.txtLastName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtLastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLastName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastName.Location = New System.Drawing.Point(94, 103)
        Me.txtLastName.MaxLength = 30
        Me.txtLastName.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtLastName.MoveToNxtCtrl = Nothing
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.PreValidation.Inherit = CType(((C1.Win.C1Input.PreValidationInheritFlags.CaseSensitive Or C1.Win.C1Input.PreValidationInheritFlags.ErrorMessage) _
            Or C1.Win.C1Input.PreValidationInheritFlags.TrimEnd), C1.Win.C1Input.PreValidationInheritFlags)
        Me.txtLastName.PreValidation.TrimStart = True
        Me.txtLastName.Size = New System.Drawing.Size(146, 22)
        Me.txtLastName.TabIndex = 5
        Me.txtLastName.Tag = Nothing
        Me.txtLastName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.BtnClearImage)
        Me.GroupBox5.Controls.Add(Me.pbPhoto)
        Me.GroupBox5.Controls.Add(Me.BtnUploadImage)
        Me.GroupBox5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.GroupBox5.Location = New System.Drawing.Point(700, 24)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(163, 194)
        Me.GroupBox5.TabIndex = 0
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Patient Image"
        '
        'BtnClearImage
        '
        Me.BtnClearImage.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnClearImage.Location = New System.Drawing.Point(86, 165)
        Me.BtnClearImage.MoveToNxtCtrl = Nothing
        Me.BtnClearImage.Name = "BtnClearImage"
        Me.BtnClearImage.SetArticleCode = Nothing
        Me.BtnClearImage.SetRowIndex = 0
        Me.BtnClearImage.Size = New System.Drawing.Size(65, 24)
        Me.BtnClearImage.TabIndex = 1
        Me.BtnClearImage.Text = "Clear"
        Me.BtnClearImage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnClearImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnClearImage.UseVisualStyleBackColor = True
        Me.BtnClearImage.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'pbPhoto
        '
        Me.pbPhoto.BackColor = System.Drawing.Color.White
        Me.pbPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbPhoto.Location = New System.Drawing.Point(13, 15)
        Me.pbPhoto.Name = "pbPhoto"
        Me.pbPhoto.Size = New System.Drawing.Size(138, 149)
        Me.pbPhoto.TabIndex = 28
        Me.pbPhoto.TabStop = False
        '
        'BtnUploadImage
        '
        Me.BtnUploadImage.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnUploadImage.Location = New System.Drawing.Point(13, 165)
        Me.BtnUploadImage.MoveToNxtCtrl = Nothing
        Me.BtnUploadImage.Name = "BtnUploadImage"
        Me.BtnUploadImage.SetArticleCode = Nothing
        Me.BtnUploadImage.SetRowIndex = 0
        Me.BtnUploadImage.Size = New System.Drawing.Size(65, 24)
        Me.BtnUploadImage.TabIndex = 0
        Me.BtnUploadImage.Text = "Upload"
        Me.BtnUploadImage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnUploadImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnUploadImage.UseVisualStyleBackColor = True
        Me.BtnUploadImage.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmHCPatientRegistration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundColor = System.Drawing.Color.LightGray
        Me.ClientSize = New System.Drawing.Size(940, 615)
        Me.ControlBox = False
        Me.Controls.Add(Me.C1Sizer2main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHCPatientRegistration"
        Me.Text = "Patient Registration"
        CType(Me.C1Sizer2main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer2main.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer1.ResumeLayout(False)
        CType(Me.C1Sizer4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer4.ResumeLayout(False)
        CType(Me.TabPatientMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPatientMain.ResumeLayout(False)
        Me.TabPageAddressInfo.ResumeLayout(False)
        CType(Me.C1Sizer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer3.ResumeLayout(False)
        Me.GroupBoxPermanentAddress.ResumeLayout(False)
        Me.GroupBoxPermanentAddress.PerformLayout()
        CType(Me.C1Label47, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label48, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPPincode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPStayMonths, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPStayYears, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPAddressLn2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPAddressLn1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label19, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxLocalAddress.ResumeLayout(False)
        Me.GroupBoxLocalAddress.PerformLayout()
        CType(Me.C1Label46, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label45, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLPincode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbLStayMonths, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbLStayYears, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbLCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbLState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbLCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLAddressLn2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLAddressLn1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPagePatientInfo.ResumeLayout(False)
        CType(Me.C1Sizer5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer5.ResumeLayout(False)
        Me.GroupBoxOtherInformation.ResumeLayout(False)
        Me.GroupBoxOtherInformation.PerformLayout()
        CType(Me.txtMonthlyIncome, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbMotherTounge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNationality, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNearestRelative, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbRelation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRelation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReligon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbMaritalStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbQualification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbOccupation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label27, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupboxContactDetails.ResumeLayout(False)
        Me.groupboxContactDetails.PerformLayout()
        CType(Me.C1Label44, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOffPhone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtResPhone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMobile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label25, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageDocsInfo.ResumeLayout(False)
        CType(Me.C1Sizer7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer7.ResumeLayout(False)
        Me.GroupBoxDocuments.ResumeLayout(False)
        Me.GroupBoxDocuments.PerformLayout()
        CType(Me.txtAuthor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDocDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbDocumentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFilePath, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDocDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label36, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Sizer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer2.ResumeLayout(False)
        Me.GroupBoxPatientDetails.ResumeLayout(False)
        CType(Me.C1SizerPatientInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1SizerPatientInfo.ResumeLayout(False)
        Me.C1SizerPatientInfo.PerformLayout()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label42, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label29, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.cmbPatientBloodGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label50, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label49, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPatientID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHeightCm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWeightKg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpDob, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRefDoctorName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPatientidname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboGender, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbAgeMonths, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbAgeYears, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSalutation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.s, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFirstName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMiddleName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Label3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLastName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.pbPhoto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents C1Sizer2main As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CtrlLabel1 As Spectrum.CtrlLabel
    Friend WithEvents C1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents C1Sizer4 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents BtnDeleteRegn As Spectrum.CtrlBtn
    Friend WithEvents BtnCloseRegn As Spectrum.CtrlBtn
    Friend WithEvents BtnEditRegn As Spectrum.CtrlBtn
    Friend WithEvents BtnNewRegn As Spectrum.CtrlBtn
    Friend WithEvents TabPatientMain As Spectrum.CtrlTab
    Friend WithEvents TabPageAddressInfo As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents C1Sizer3 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents GroupBoxPermanentAddress As System.Windows.Forms.GroupBox
    Friend WithEvents C1Label47 As Spectrum.CtrlLabel
    Friend WithEvents C1Label48 As Spectrum.CtrlLabel
    Friend WithEvents txtPPincode As Spectrum.CtrlTextBox
    Friend WithEvents cmbPStayMonths As Spectrum.ctrlCombo
    Friend WithEvents cmbPStayYears As Spectrum.ctrlCombo
    Friend WithEvents C1Label39 As Spectrum.CtrlLabel
    Friend WithEvents chkCopyAddress As System.Windows.Forms.CheckBox
    Friend WithEvents cmbPCity As Spectrum.ctrlCombo
    Friend WithEvents cmbPState As Spectrum.ctrlCombo
    Friend WithEvents cmbPCountry As Spectrum.ctrlCombo
    Friend WithEvents C1Label14 As Spectrum.CtrlLabel
    Friend WithEvents C1Label15 As Spectrum.CtrlLabel
    Friend WithEvents C1Label16 As Spectrum.CtrlLabel
    Friend WithEvents C1Label17 As Spectrum.CtrlLabel
    Friend WithEvents txtPAddressLn2 As Spectrum.CtrlTextBox
    Friend WithEvents txtPAddressLn1 As Spectrum.CtrlTextBox
    Friend WithEvents C1Label18 As Spectrum.CtrlLabel
    Friend WithEvents C1Label19 As Spectrum.CtrlLabel
    Friend WithEvents GroupBoxLocalAddress As System.Windows.Forms.GroupBox
    Friend WithEvents C1Label46 As Spectrum.CtrlLabel
    Friend WithEvents C1Label45 As Spectrum.CtrlLabel
    Friend WithEvents txtLPincode As Spectrum.CtrlTextBox
    Friend WithEvents cmbLStayMonths As Spectrum.ctrlCombo
    Friend WithEvents cmbLStayYears As Spectrum.ctrlCombo
    Friend WithEvents C1Label40 As Spectrum.CtrlLabel
    Friend WithEvents C1Label13 As Spectrum.CtrlLabel
    Friend WithEvents C1Label10 As Spectrum.CtrlLabel
    Friend WithEvents C1Label11 As Spectrum.CtrlLabel
    Friend WithEvents C1Label12 As Spectrum.CtrlLabel
    Friend WithEvents C1Label9 As Spectrum.CtrlLabel
    Friend WithEvents C1Label8 As Spectrum.CtrlLabel
    Friend WithEvents cmbLCity As Spectrum.ctrlCombo
    Friend WithEvents cmbLState As Spectrum.ctrlCombo
    Friend WithEvents cmbLCountry As Spectrum.ctrlCombo
    Friend WithEvents txtLAddressLn2 As Spectrum.CtrlTextBox
    Friend WithEvents txtLAddressLn1 As Spectrum.CtrlTextBox
    Friend WithEvents TabPagePatientInfo As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents C1Sizer5 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents GroupBoxOtherInformation As System.Windows.Forms.GroupBox
    Friend WithEvents txtMonthlyIncome As Spectrum.CtrlTextBox
    Friend WithEvents cmbMotherTounge As Spectrum.ctrlCombo
    Friend WithEvents C1Label43 As Spectrum.CtrlLabel
    Friend WithEvents C1Label26 As Spectrum.CtrlLabel
    Friend WithEvents C1Label6 As Spectrum.CtrlLabel
    Friend WithEvents txtNationality As Spectrum.CtrlTextBox
    Friend WithEvents txtNearestRelative As Spectrum.CtrlTextBox
    Friend WithEvents C1Label32 As Spectrum.CtrlLabel
    Friend WithEvents cmbRelation As Spectrum.ctrlCombo
    Friend WithEvents txtRelation As Spectrum.CtrlTextBox
    Friend WithEvents txtReligon As Spectrum.CtrlTextBox
    Friend WithEvents C1Label31 As Spectrum.CtrlLabel
    Friend WithEvents cmbMaritalStatus As Spectrum.ctrlCombo
    Friend WithEvents C1Label21 As Spectrum.CtrlLabel
    Friend WithEvents C1Label28 As Spectrum.CtrlLabel
    Friend WithEvents cmbQualification As Spectrum.ctrlCombo
    Friend WithEvents cmbOccupation As Spectrum.ctrlCombo
    Friend WithEvents C1Label22 As Spectrum.CtrlLabel
    Friend WithEvents C1Label27 As Spectrum.CtrlLabel
    Friend WithEvents groupboxContactDetails As System.Windows.Forms.GroupBox
    Friend WithEvents C1Label44 As Spectrum.CtrlLabel
    Friend WithEvents txtOffPhone As Spectrum.CtrlTextBox
    Friend WithEvents C1Label20 As Spectrum.CtrlLabel
    Friend WithEvents txtEmail As Spectrum.CtrlTextBox
    Friend WithEvents C1Label23 As Spectrum.CtrlLabel
    Friend WithEvents txtResPhone As Spectrum.CtrlTextBox
    Friend WithEvents txtMobile As Spectrum.CtrlTextBox
    Friend WithEvents C1Label24 As Spectrum.CtrlLabel
    Friend WithEvents C1Label25 As Spectrum.CtrlLabel
    Friend WithEvents TabPageDocsInfo As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents C1Sizer7 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents GroupBoxDocuments As System.Windows.Forms.GroupBox
    Friend WithEvents txtAuthor As Spectrum.CtrlTextBox
    Friend WithEvents BtnSearchAuthor As Spectrum.CtrlBtn
    Friend WithEvents dtpDocDate As Spectrum.ctrlDate
    Friend WithEvents TreeViewDocs As System.Windows.Forms.TreeView
    Friend WithEvents BtnNewDoc As Spectrum.CtrlBtn
    Friend WithEvents cmbDocumentType As Spectrum.ctrlCombo
    Friend WithEvents BtnUploadDoc As Spectrum.CtrlBtn
    Friend WithEvents C1Label38 As Spectrum.CtrlLabel
    Friend WithEvents txtFilePath As Spectrum.CtrlTextBox
    Friend WithEvents C1Label33 As Spectrum.CtrlLabel
    Friend WithEvents C1Label35 As Spectrum.CtrlLabel
    Friend WithEvents txtDocDescription As Spectrum.CtrlTextBox
    Friend WithEvents C1Label36 As Spectrum.CtrlLabel
    Friend WithEvents C1Label37 As Spectrum.CtrlLabel
    Friend WithEvents BtnDeleteDoc As Spectrum.CtrlBtn
    Friend WithEvents BtnAddDoc As Spectrum.CtrlBtn
    Friend WithEvents C1Sizer2 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents GroupBoxPatientDetails As System.Windows.Forms.GroupBox
    Friend WithEvents C1SizerPatientInfo As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents C1Label34 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel2 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel3 As Spectrum.CtrlLabel
    Friend WithEvents C1Label30 As Spectrum.CtrlLabel
    Friend WithEvents C1Label42 As Spectrum.CtrlLabel
    Friend WithEvents C1Label41 As Spectrum.CtrlLabel
    Friend WithEvents C1Label29 As Spectrum.CtrlLabel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chckKaff As System.Windows.Forms.CheckBox
    Friend WithEvents chckPitta As System.Windows.Forms.CheckBox
    Friend WithEvents chckVaat As System.Windows.Forms.CheckBox
    Friend WithEvents cmbPatientBloodGroup As Spectrum.ctrlCombo
    Friend WithEvents C1Label50 As Spectrum.CtrlLabel
    Friend WithEvents C1Label49 As Spectrum.CtrlLabel
    Friend WithEvents txtPatientID As Spectrum.CtrlTextBox
    Friend WithEvents txtHeightCm As Spectrum.CtrlTextBox
    Friend WithEvents txtWeightKg As Spectrum.CtrlTextBox
    Friend WithEvents dtpDob As Spectrum.ctrlDate
    Friend WithEvents txtRefDoctorName As Spectrum.CtrlTextBox
    Friend WithEvents lblPatientidname As Spectrum.CtrlLabel
    Friend WithEvents cboGender As Spectrum.ctrlCombo
    Friend WithEvents cmbAgeMonths As Spectrum.ctrlCombo
    Friend WithEvents cmbAgeYears As Spectrum.ctrlCombo
    Friend WithEvents C1Label1 As Spectrum.CtrlLabel
    Friend WithEvents BtnSearchPatient As Spectrum.CtrlBtn
    Friend WithEvents cmbSalutation As Spectrum.ctrlCombo
    Friend WithEvents BtnNewRefDr As Spectrum.CtrlBtn
    Friend WithEvents C1Label2 As Spectrum.CtrlLabel
    Friend WithEvents BtnSearchRefDr As Spectrum.CtrlBtn
    Friend WithEvents s As Spectrum.CtrlLabel
    Friend WithEvents txtFirstName As Spectrum.CtrlTextBox
    Friend WithEvents C1Label5 As Spectrum.CtrlLabel
    Friend WithEvents C1Label4 As Spectrum.CtrlLabel
    Friend WithEvents txtMiddleName As Spectrum.CtrlTextBox
    Friend WithEvents C1Label7 As Spectrum.CtrlLabel
    Friend WithEvents C1Label3 As Spectrum.CtrlLabel
    Friend WithEvents txtLastName As Spectrum.CtrlTextBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnClearImage As Spectrum.CtrlBtn
    Friend WithEvents pbPhoto As C1.Win.C1Input.C1PictureBox
    Friend WithEvents BtnUploadImage As Spectrum.CtrlBtn
End Class
