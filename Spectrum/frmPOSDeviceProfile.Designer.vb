<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPOSDeviceProfile
    Inherits CtrlPopupForm

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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.tcPOSDevice = New Spectrum.CtrlTab()
        Me.tbPrinter = New C1.Win.C1Command.C1DockingTabPage()
        Me.grdprinter = New System.Windows.Forms.DataGridView()
        Me.GrpAssignedPrn = New System.Windows.Forms.GroupBox()
        Me.lblOtherWinPrnName = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblCurrOPOSPrnName = New System.Windows.Forms.Label()
        Me.lblCurrWinPrnName = New System.Windows.Forms.Label()
        Me.lblOPOSWinPrn = New System.Windows.Forms.Label()
        Me.lblCurrWinPrn = New System.Windows.Forms.Label()
        Me.btnSavePrinter = New C1.Win.C1Input.C1Button()
        Me.gbTestPrinter = New System.Windows.Forms.GroupBox()
        Me.lblCurrentState = New System.Windows.Forms.Label()
        Me.lblStatusPrinter = New System.Windows.Forms.Label()
        Me.lblStatePrinter = New System.Windows.Forms.Label()
        Me.btnCloseDevicePrinter = New C1.Win.C1Input.C1Button()
        Me.btnOpenDevicePrinter = New C1.Win.C1Input.C1Button()
        Me.btnPrintSlip = New C1.Win.C1Input.C1Button()
        Me.cmbPrinterName = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbPrinter = New System.Windows.Forms.ComboBox()
        Me.tbDrawer = New C1.Win.C1Command.C1DockingTabPage()
        Me.GrpDrawer = New System.Windows.Forms.GroupBox()
        Me.lblDisplCashDrawar = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnSaveDrawer = New C1.Win.C1Input.C1Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.gbTestDrawer = New System.Windows.Forms.GroupBox()
        Me.lblStateDrawer = New System.Windows.Forms.Label()
        Me.btnCloseDeviceDrawer = New C1.Win.C1Input.C1Button()
        Me.btnOpenDeviceDrawer = New C1.Win.C1Input.C1Button()
        Me.btnOpenDrawer = New C1.Win.C1Input.C1Button()
        Me.cmbDrawerName = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblDrawer = New System.Windows.Forms.Label()
        Me.cmbDrawer = New System.Windows.Forms.ComboBox()
        Me.tbScanner = New C1.Win.C1Command.C1DockingTabPage()
        Me.btnSaveScanner = New C1.Win.C1Input.C1Button()
        Me.gbTestScanner = New System.Windows.Forms.GroupBox()
        Me.txtScannerOutput = New System.Windows.Forms.TextBox()
        Me.lblStateScanner = New System.Windows.Forms.Label()
        Me.btnCLoseDeviceScanner = New C1.Win.C1Input.C1Button()
        Me.btnOpenDeviceScanner = New C1.Win.C1Input.C1Button()
        Me.cmbScannerName = New System.Windows.Forms.ComboBox()
        Me.lblScannerName = New System.Windows.Forms.Label()
        Me.lblScanner = New System.Windows.Forms.Label()
        Me.cmbscanner = New System.Windows.Forms.ComboBox()
        Me.tbLineDisplay = New C1.Win.C1Command.C1DockingTabPage()
        Me.btnSaveDisplay = New C1.Win.C1Input.C1Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblstateDisplay = New System.Windows.Forms.Label()
        Me.btnCloseDeviceDisplay = New C1.Win.C1Input.C1Button()
        Me.btnOpenDeviceDisplay = New C1.Win.C1Input.C1Button()
        Me.btnShowMessage = New C1.Win.C1Input.C1Button()
        Me.cmbDisplayName = New System.Windows.Forms.ComboBox()
        Me.lblDisplayDeviceName = New System.Windows.Forms.Label()
        Me.lblDisplay = New System.Windows.Forms.Label()
        Me.cmbDisplay = New System.Windows.Forms.ComboBox()
        Me.tbMSR = New C1.Win.C1Command.C1DockingTabPage()
        Me.lblMsr = New Spectrum.CtrlLabel()
        Me.MsrName = New Spectrum.CtrlLabel()
        Me.GrpMsrCurrAssigned = New System.Windows.Forms.GroupBox()
        Me.lblCurrentMsr = New Spectrum.CtrlLabel()
        Me.lblCurrentMSRlogicalName = New Spectrum.CtrlLabel()
        Me.ChkpwayEnabled = New System.Windows.Forms.CheckBox()
        Me.ChkEnabledMsr = New System.Windows.Forms.CheckBox()
        Me.btnSaveMsr = New C1.Win.C1Input.C1Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblTrackNo = New Spectrum.CtrlLabel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.lblstateMsr = New Spectrum.CtrlLabel()
        Me.Label15 = New Spectrum.CtrlLabel()
        Me.lblCardDtls = New Spectrum.CtrlLabel()
        Me.txtMsrHealthCheck = New System.Windows.Forms.TextBox()
        Me.btnCloseMsr = New C1.Win.C1Input.C1Button()
        Me.btnOpenMsr = New C1.Win.C1Input.C1Button()
        Me.btnSwipeCard = New C1.Win.C1Input.C1Button()
        Me.cmbMsrName = New System.Windows.Forms.ComboBox()
        Me.cmbMsr = New System.Windows.Forms.ComboBox()
        Me.tbMiscellaneous = New C1.Win.C1Command.C1DockingTabPage()
        Me.btnProductReset = New C1.Win.C1Input.C1Button()
        Me.btnBackgroundReset = New C1.Win.C1Input.C1Button()
        Me.btnSave = New C1.Win.C1Input.C1Button()
        Me.btnProductButton = New C1.Win.C1Input.C1Button()
        Me.btnBackgroundButton = New C1.Win.C1Input.C1Button()
        Me.txtProduct = New System.Windows.Forms.TextBox()
        Me.txtBackground = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.PosdbDataSet = New SpectrumBL.POSDBDataSet()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblTerminalId = New System.Windows.Forms.Label()
        Me.ProductFolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.BackgroundFileDialog = New System.Windows.Forms.OpenFileDialog()
        CType(Me.tcPOSDevice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tcPOSDevice.SuspendLayout()
        Me.tbPrinter.SuspendLayout()
        CType(Me.grdprinter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpAssignedPrn.SuspendLayout()
        Me.gbTestPrinter.SuspendLayout()
        Me.tbDrawer.SuspendLayout()
        Me.GrpDrawer.SuspendLayout()
        Me.gbTestDrawer.SuspendLayout()
        Me.tbScanner.SuspendLayout()
        Me.gbTestScanner.SuspendLayout()
        Me.tbLineDisplay.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.tbMSR.SuspendLayout()
        CType(Me.lblMsr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MsrName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpMsrCurrAssigned.SuspendLayout()
        CType(Me.lblCurrentMsr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrentMSRlogicalName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.lblTrackNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblstateMsr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCardDtls, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbMiscellaneous.SuspendLayout()
        CType(Me.PosdbDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tcPOSDevice
        '
        Me.tcPOSDevice.Controls.Add(Me.tbPrinter)
        Me.tcPOSDevice.Controls.Add(Me.tbDrawer)
        Me.tcPOSDevice.Controls.Add(Me.tbScanner)
        Me.tcPOSDevice.Controls.Add(Me.tbLineDisplay)
        Me.tcPOSDevice.Controls.Add(Me.tbMSR)
        Me.tcPOSDevice.Controls.Add(Me.tbMiscellaneous)
        Me.tcPOSDevice.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tcPOSDevice.HotTrack = True
        Me.tcPOSDevice.Location = New System.Drawing.Point(0, 32)
        Me.tcPOSDevice.Name = "tcPOSDevice"
        Me.tcPOSDevice.SelectedIndex = 5
        Me.tcPOSDevice.Size = New System.Drawing.Size(809, 470)
        Me.tcPOSDevice.TabIndex = 0
        Me.tcPOSDevice.TabsSpacing = 5
        Me.tcPOSDevice.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007
        Me.tcPOSDevice.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Blue
        '
        'tbPrinter
        '
        Me.tbPrinter.Controls.Add(Me.GrpAssignedPrn)
        Me.tbPrinter.Controls.Add(Me.btnSavePrinter)
        Me.tbPrinter.Controls.Add(Me.gbTestPrinter)
        Me.tbPrinter.Controls.Add(Me.cmbPrinterName)
        Me.tbPrinter.Controls.Add(Me.Label2)
        Me.tbPrinter.Controls.Add(Me.Label1)
        Me.tbPrinter.Controls.Add(Me.cmbPrinter)
        Me.tbPrinter.Location = New System.Drawing.Point(1, 25)
        Me.tbPrinter.Name = "tbPrinter"
        Me.tbPrinter.Padding = New System.Windows.Forms.Padding(3)
        Me.tbPrinter.Size = New System.Drawing.Size(807, 444)
        Me.tbPrinter.TabIndex = 0
        Me.tbPrinter.Text = "Printer"
        '
        'grdprinter
        '
        Me.grdprinter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 8.25!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlLight
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdprinter.DefaultCellStyle = DataGridViewCellStyle1
        Me.grdprinter.Location = New System.Drawing.Point(9, 19)
        Me.grdprinter.Name = "grdprinter"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Verdana", 8.25!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdprinter.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdprinter.Size = New System.Drawing.Size(728, 145)
        Me.grdprinter.TabIndex = 13
        '
        'GrpAssignedPrn
        '
        Me.GrpAssignedPrn.BackColor = System.Drawing.Color.Transparent
        Me.GrpAssignedPrn.Controls.Add(Me.grdprinter)
        Me.GrpAssignedPrn.Controls.Add(Me.lblOtherWinPrnName)
        Me.GrpAssignedPrn.Controls.Add(Me.Label11)
        Me.GrpAssignedPrn.Controls.Add(Me.lblCurrOPOSPrnName)
        Me.GrpAssignedPrn.Controls.Add(Me.lblCurrWinPrnName)
        Me.GrpAssignedPrn.Controls.Add(Me.lblOPOSWinPrn)
        Me.GrpAssignedPrn.Controls.Add(Me.lblCurrWinPrn)
        Me.GrpAssignedPrn.Location = New System.Drawing.Point(33, 258)
        Me.GrpAssignedPrn.Name = "GrpAssignedPrn"
        Me.GrpAssignedPrn.Size = New System.Drawing.Size(756, 178)
        Me.GrpAssignedPrn.TabIndex = 12
        Me.GrpAssignedPrn.TabStop = False
        Me.GrpAssignedPrn.Text = "Current Assigned Printer"
        '
        'lblOtherWinPrnName
        '
        Me.lblOtherWinPrnName.AutoSize = True
        Me.lblOtherWinPrnName.Location = New System.Drawing.Point(107, 61)
        Me.lblOtherWinPrnName.Name = "lblOtherWinPrnName"
        Me.lblOtherWinPrnName.Size = New System.Drawing.Size(12, 13)
        Me.lblOtherWinPrnName.TabIndex = 5
        Me.lblOtherWinPrnName.Text = ":"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 61)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(102, 13)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "Other Printer    :"
        '
        'lblCurrOPOSPrnName
        '
        Me.lblCurrOPOSPrnName.AutoSize = True
        Me.lblCurrOPOSPrnName.Location = New System.Drawing.Point(107, 41)
        Me.lblCurrOPOSPrnName.Name = "lblCurrOPOSPrnName"
        Me.lblCurrOPOSPrnName.Size = New System.Drawing.Size(12, 13)
        Me.lblCurrOPOSPrnName.TabIndex = 3
        Me.lblCurrOPOSPrnName.Text = ":"
        '
        'lblCurrWinPrnName
        '
        Me.lblCurrWinPrnName.AutoSize = True
        Me.lblCurrWinPrnName.Location = New System.Drawing.Point(107, 19)
        Me.lblCurrWinPrnName.Name = "lblCurrWinPrnName"
        Me.lblCurrWinPrnName.Size = New System.Drawing.Size(12, 13)
        Me.lblCurrWinPrnName.TabIndex = 2
        Me.lblCurrWinPrnName.Text = ":"
        '
        'lblOPOSWinPrn
        '
        Me.lblOPOSWinPrn.AutoSize = True
        Me.lblOPOSWinPrn.Location = New System.Drawing.Point(6, 41)
        Me.lblOPOSWinPrn.Name = "lblOPOSWinPrn"
        Me.lblOPOSWinPrn.Size = New System.Drawing.Size(103, 13)
        Me.lblOPOSWinPrn.TabIndex = 1
        Me.lblOPOSWinPrn.Text = "OPOS Printer    :"
        '
        'lblCurrWinPrn
        '
        Me.lblCurrWinPrn.AutoSize = True
        Me.lblCurrWinPrn.Location = New System.Drawing.Point(6, 19)
        Me.lblCurrWinPrn.Name = "lblCurrWinPrn"
        Me.lblCurrWinPrn.Size = New System.Drawing.Size(102, 13)
        Me.lblCurrWinPrn.TabIndex = 0
        Me.lblCurrWinPrn.Text = "Window Printer :"
        '
        'btnSavePrinter
        '
        Me.btnSavePrinter.Location = New System.Drawing.Point(330, 229)
        Me.btnSavePrinter.Name = "btnSavePrinter"
        Me.btnSavePrinter.Size = New System.Drawing.Size(87, 23)
        Me.btnSavePrinter.TabIndex = 9
        Me.btnSavePrinter.Text = "Save"
        Me.btnSavePrinter.UseVisualStyleBackColor = True
        '
        'gbTestPrinter
        '
        Me.gbTestPrinter.BackColor = System.Drawing.Color.Transparent
        Me.gbTestPrinter.Controls.Add(Me.lblCurrentState)
        Me.gbTestPrinter.Controls.Add(Me.lblStatusPrinter)
        Me.gbTestPrinter.Controls.Add(Me.lblStatePrinter)
        Me.gbTestPrinter.Controls.Add(Me.btnCloseDevicePrinter)
        Me.gbTestPrinter.Controls.Add(Me.btnOpenDevicePrinter)
        Me.gbTestPrinter.Controls.Add(Me.btnPrintSlip)
        Me.gbTestPrinter.Location = New System.Drawing.Point(178, 106)
        Me.gbTestPrinter.Name = "gbTestPrinter"
        Me.gbTestPrinter.Size = New System.Drawing.Size(239, 106)
        Me.gbTestPrinter.TabIndex = 8
        Me.gbTestPrinter.TabStop = False
        Me.gbTestPrinter.Text = "Test"
        '
        'lblCurrentState
        '
        Me.lblCurrentState.AutoSize = True
        Me.lblCurrentState.Location = New System.Drawing.Point(176, 22)
        Me.lblCurrentState.Name = "lblCurrentState"
        Me.lblCurrentState.Size = New System.Drawing.Size(0, 13)
        Me.lblCurrentState.TabIndex = 9
        '
        'lblStatusPrinter
        '
        Me.lblStatusPrinter.AutoSize = True
        Me.lblStatusPrinter.Location = New System.Drawing.Point(187, 22)
        Me.lblStatusPrinter.Name = "lblStatusPrinter"
        Me.lblStatusPrinter.Size = New System.Drawing.Size(0, 13)
        Me.lblStatusPrinter.TabIndex = 8
        '
        'lblStatePrinter
        '
        Me.lblStatePrinter.AutoSize = True
        Me.lblStatePrinter.Location = New System.Drawing.Point(132, 22)
        Me.lblStatePrinter.Name = "lblStatePrinter"
        Me.lblStatePrinter.Size = New System.Drawing.Size(37, 13)
        Me.lblStatePrinter.TabIndex = 9
        Me.lblStatePrinter.Text = "State"
        '
        'btnCloseDevicePrinter
        '
        Me.btnCloseDevicePrinter.Location = New System.Drawing.Point(19, 75)
        Me.btnCloseDevicePrinter.Name = "btnCloseDevicePrinter"
        Me.btnCloseDevicePrinter.Size = New System.Drawing.Size(98, 23)
        Me.btnCloseDevicePrinter.TabIndex = 4
        Me.btnCloseDevicePrinter.Text = "Close Device"
        Me.btnCloseDevicePrinter.UseVisualStyleBackColor = True
        '
        'btnOpenDevicePrinter
        '
        Me.btnOpenDevicePrinter.Location = New System.Drawing.Point(19, 17)
        Me.btnOpenDevicePrinter.Name = "btnOpenDevicePrinter"
        Me.btnOpenDevicePrinter.Size = New System.Drawing.Size(98, 23)
        Me.btnOpenDevicePrinter.TabIndex = 2
        Me.btnOpenDevicePrinter.Text = "Open Device"
        Me.btnOpenDevicePrinter.UseVisualStyleBackColor = True
        '
        'btnPrintSlip
        '
        Me.btnPrintSlip.Location = New System.Drawing.Point(19, 46)
        Me.btnPrintSlip.Name = "btnPrintSlip"
        Me.btnPrintSlip.Size = New System.Drawing.Size(98, 23)
        Me.btnPrintSlip.TabIndex = 3
        Me.btnPrintSlip.Text = "Print Slip"
        Me.btnPrintSlip.UseVisualStyleBackColor = True
        '
        'cmbPrinterName
        '
        Me.cmbPrinterName.FormattingEnabled = True
        Me.cmbPrinterName.Location = New System.Drawing.Point(169, 67)
        Me.cmbPrinterName.Name = "cmbPrinterName"
        Me.cmbPrinterName.Size = New System.Drawing.Size(248, 21)
        Me.cmbPrinterName.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(30, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Printer Device Name"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(30, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Printer"
        '
        'cmbPrinter
        '
        Me.cmbPrinter.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox
        Me.cmbPrinter.FormattingEnabled = True
        Me.cmbPrinter.Location = New System.Drawing.Point(169, 40)
        Me.cmbPrinter.Name = "cmbPrinter"
        Me.cmbPrinter.Size = New System.Drawing.Size(140, 21)
        Me.cmbPrinter.TabIndex = 0
        '
        'tbDrawer
        '
        Me.tbDrawer.Controls.Add(Me.GrpDrawer)
        Me.tbDrawer.Controls.Add(Me.btnSaveDrawer)
        Me.tbDrawer.Controls.Add(Me.Label6)
        Me.tbDrawer.Controls.Add(Me.gbTestDrawer)
        Me.tbDrawer.Controls.Add(Me.cmbDrawerName)
        Me.tbDrawer.Controls.Add(Me.Label8)
        Me.tbDrawer.Controls.Add(Me.lblDrawer)
        Me.tbDrawer.Controls.Add(Me.cmbDrawer)
        Me.tbDrawer.Location = New System.Drawing.Point(1, 25)
        Me.tbDrawer.Name = "tbDrawer"
        Me.tbDrawer.Padding = New System.Windows.Forms.Padding(3)
        Me.tbDrawer.Size = New System.Drawing.Size(807, 444)
        Me.tbDrawer.TabIndex = 1
        Me.tbDrawer.Text = "Drawer"
        '
        'GrpDrawer
        '
        Me.GrpDrawer.BackColor = System.Drawing.Color.Transparent
        Me.GrpDrawer.Controls.Add(Me.lblDisplCashDrawar)
        Me.GrpDrawer.Controls.Add(Me.Label10)
        Me.GrpDrawer.Controls.Add(Me.Label12)
        Me.GrpDrawer.Location = New System.Drawing.Point(33, 291)
        Me.GrpDrawer.Name = "GrpDrawer"
        Me.GrpDrawer.Size = New System.Drawing.Size(464, 127)
        Me.GrpDrawer.TabIndex = 18
        Me.GrpDrawer.TabStop = False
        Me.GrpDrawer.Text = "Current Assigned Cash Drawer"
        '
        'lblDisplCashDrawar
        '
        Me.lblDisplCashDrawar.AutoSize = True
        Me.lblDisplCashDrawar.Location = New System.Drawing.Point(164, 28)
        Me.lblDisplCashDrawar.Name = "lblDisplCashDrawar"
        Me.lblDisplCashDrawar.Size = New System.Drawing.Size(12, 13)
        Me.lblDisplCashDrawar.TabIndex = 3
        Me.lblDisplCashDrawar.Text = ":"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(146, 28)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(12, 13)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = ":"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(11, 28)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(129, 13)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Drawer Logical Name"
        '
        'btnSaveDrawer
        '
        Me.btnSaveDrawer.Location = New System.Drawing.Point(330, 228)
        Me.btnSaveDrawer.Name = "btnSaveDrawer"
        Me.btnSaveDrawer.Size = New System.Drawing.Size(87, 23)
        Me.btnSaveDrawer.TabIndex = 17
        Me.btnSaveDrawer.Text = "Save"
        Me.btnSaveDrawer.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(44, 93)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(0, 13)
        Me.Label6.TabIndex = 16
        '
        'gbTestDrawer
        '
        Me.gbTestDrawer.BackColor = System.Drawing.Color.Transparent
        Me.gbTestDrawer.Controls.Add(Me.lblStateDrawer)
        Me.gbTestDrawer.Controls.Add(Me.btnCloseDeviceDrawer)
        Me.gbTestDrawer.Controls.Add(Me.btnOpenDeviceDrawer)
        Me.gbTestDrawer.Controls.Add(Me.btnOpenDrawer)
        Me.gbTestDrawer.Location = New System.Drawing.Point(178, 106)
        Me.gbTestDrawer.Name = "gbTestDrawer"
        Me.gbTestDrawer.Size = New System.Drawing.Size(239, 106)
        Me.gbTestDrawer.TabIndex = 15
        Me.gbTestDrawer.TabStop = False
        Me.gbTestDrawer.Text = "Test"
        '
        'lblStateDrawer
        '
        Me.lblStateDrawer.AutoSize = True
        Me.lblStateDrawer.Location = New System.Drawing.Point(132, 22)
        Me.lblStateDrawer.Name = "lblStateDrawer"
        Me.lblStateDrawer.Size = New System.Drawing.Size(37, 13)
        Me.lblStateDrawer.TabIndex = 7
        Me.lblStateDrawer.Text = "State"
        '
        'btnCloseDeviceDrawer
        '
        Me.btnCloseDeviceDrawer.Location = New System.Drawing.Point(19, 75)
        Me.btnCloseDeviceDrawer.Name = "btnCloseDeviceDrawer"
        Me.btnCloseDeviceDrawer.Size = New System.Drawing.Size(98, 23)
        Me.btnCloseDeviceDrawer.TabIndex = 9
        Me.btnCloseDeviceDrawer.Text = "Close Device"
        Me.btnCloseDeviceDrawer.UseVisualStyleBackColor = True
        '
        'btnOpenDeviceDrawer
        '
        Me.btnOpenDeviceDrawer.Location = New System.Drawing.Point(19, 17)
        Me.btnOpenDeviceDrawer.Name = "btnOpenDeviceDrawer"
        Me.btnOpenDeviceDrawer.Size = New System.Drawing.Size(98, 23)
        Me.btnOpenDeviceDrawer.TabIndex = 7
        Me.btnOpenDeviceDrawer.Text = "Open Device"
        Me.btnOpenDeviceDrawer.UseVisualStyleBackColor = True
        '
        'btnOpenDrawer
        '
        Me.btnOpenDrawer.Location = New System.Drawing.Point(19, 46)
        Me.btnOpenDrawer.Name = "btnOpenDrawer"
        Me.btnOpenDrawer.Size = New System.Drawing.Size(98, 23)
        Me.btnOpenDrawer.TabIndex = 8
        Me.btnOpenDrawer.Text = "Open Drawer"
        Me.btnOpenDrawer.UseVisualStyleBackColor = True
        '
        'cmbDrawerName
        '
        Me.cmbDrawerName.FormattingEnabled = True
        Me.cmbDrawerName.Location = New System.Drawing.Point(169, 67)
        Me.cmbDrawerName.Name = "cmbDrawerName"
        Me.cmbDrawerName.Size = New System.Drawing.Size(248, 21)
        Me.cmbDrawerName.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(30, 70)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(129, 13)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Drawer Device Name"
        '
        'lblDrawer
        '
        Me.lblDrawer.AutoSize = True
        Me.lblDrawer.BackColor = System.Drawing.Color.Transparent
        Me.lblDrawer.Location = New System.Drawing.Point(30, 48)
        Me.lblDrawer.Name = "lblDrawer"
        Me.lblDrawer.Size = New System.Drawing.Size(49, 13)
        Me.lblDrawer.TabIndex = 12
        Me.lblDrawer.Text = "Drawer"
        '
        'cmbDrawer
        '
        Me.cmbDrawer.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox
        Me.cmbDrawer.FormattingEnabled = True
        Me.cmbDrawer.Items.AddRange(New Object() {"OPOS"})
        Me.cmbDrawer.Location = New System.Drawing.Point(169, 40)
        Me.cmbDrawer.Name = "cmbDrawer"
        Me.cmbDrawer.Size = New System.Drawing.Size(140, 21)
        Me.cmbDrawer.TabIndex = 5
        '
        'tbScanner
        '
        Me.tbScanner.Controls.Add(Me.btnSaveScanner)
        Me.tbScanner.Controls.Add(Me.gbTestScanner)
        Me.tbScanner.Controls.Add(Me.cmbScannerName)
        Me.tbScanner.Controls.Add(Me.lblScannerName)
        Me.tbScanner.Controls.Add(Me.lblScanner)
        Me.tbScanner.Controls.Add(Me.cmbscanner)
        Me.tbScanner.Location = New System.Drawing.Point(1, 25)
        Me.tbScanner.Name = "tbScanner"
        Me.tbScanner.Size = New System.Drawing.Size(807, 444)
        Me.tbScanner.TabIndex = 2
        Me.tbScanner.Text = "Scanner"
        '
        'btnSaveScanner
        '
        Me.btnSaveScanner.Enabled = False
        Me.btnSaveScanner.Location = New System.Drawing.Point(331, 229)
        Me.btnSaveScanner.Name = "btnSaveScanner"
        Me.btnSaveScanner.Size = New System.Drawing.Size(87, 23)
        Me.btnSaveScanner.TabIndex = 24
        Me.btnSaveScanner.Text = "Save"
        Me.btnSaveScanner.UseVisualStyleBackColor = True
        '
        'gbTestScanner
        '
        Me.gbTestScanner.BackColor = System.Drawing.Color.Transparent
        Me.gbTestScanner.Controls.Add(Me.txtScannerOutput)
        Me.gbTestScanner.Controls.Add(Me.lblStateScanner)
        Me.gbTestScanner.Controls.Add(Me.btnCLoseDeviceScanner)
        Me.gbTestScanner.Controls.Add(Me.btnOpenDeviceScanner)
        Me.gbTestScanner.Location = New System.Drawing.Point(178, 106)
        Me.gbTestScanner.Name = "gbTestScanner"
        Me.gbTestScanner.Size = New System.Drawing.Size(239, 106)
        Me.gbTestScanner.TabIndex = 23
        Me.gbTestScanner.TabStop = False
        Me.gbTestScanner.Text = "Test"
        '
        'txtScannerOutput
        '
        Me.txtScannerOutput.Location = New System.Drawing.Point(19, 75)
        Me.txtScannerOutput.Name = "txtScannerOutput"
        Me.txtScannerOutput.Size = New System.Drawing.Size(213, 21)
        Me.txtScannerOutput.TabIndex = 14
        '
        'lblStateScanner
        '
        Me.lblStateScanner.AutoSize = True
        Me.lblStateScanner.Location = New System.Drawing.Point(132, 22)
        Me.lblStateScanner.Name = "lblStateScanner"
        Me.lblStateScanner.Size = New System.Drawing.Size(37, 13)
        Me.lblStateScanner.TabIndex = 7
        Me.lblStateScanner.Text = "State"
        '
        'btnCLoseDeviceScanner
        '
        Me.btnCLoseDeviceScanner.Location = New System.Drawing.Point(19, 46)
        Me.btnCLoseDeviceScanner.Name = "btnCLoseDeviceScanner"
        Me.btnCLoseDeviceScanner.Size = New System.Drawing.Size(98, 23)
        Me.btnCLoseDeviceScanner.TabIndex = 13
        Me.btnCLoseDeviceScanner.Text = "Close Device"
        Me.btnCLoseDeviceScanner.UseVisualStyleBackColor = True
        '
        'btnOpenDeviceScanner
        '
        Me.btnOpenDeviceScanner.Location = New System.Drawing.Point(19, 17)
        Me.btnOpenDeviceScanner.Name = "btnOpenDeviceScanner"
        Me.btnOpenDeviceScanner.Size = New System.Drawing.Size(98, 23)
        Me.btnOpenDeviceScanner.TabIndex = 12
        Me.btnOpenDeviceScanner.Text = "Open Device"
        Me.btnOpenDeviceScanner.UseVisualStyleBackColor = True
        '
        'cmbScannerName
        '
        Me.cmbScannerName.FormattingEnabled = True
        Me.cmbScannerName.Location = New System.Drawing.Point(170, 67)
        Me.cmbScannerName.Name = "cmbScannerName"
        Me.cmbScannerName.Size = New System.Drawing.Size(248, 21)
        Me.cmbScannerName.TabIndex = 11
        '
        'lblScannerName
        '
        Me.lblScannerName.AutoSize = True
        Me.lblScannerName.BackColor = System.Drawing.Color.Transparent
        Me.lblScannerName.Location = New System.Drawing.Point(30, 70)
        Me.lblScannerName.Name = "lblScannerName"
        Me.lblScannerName.Size = New System.Drawing.Size(134, 13)
        Me.lblScannerName.TabIndex = 21
        Me.lblScannerName.Text = "Scanner Device Name"
        '
        'lblScanner
        '
        Me.lblScanner.AutoSize = True
        Me.lblScanner.BackColor = System.Drawing.Color.Transparent
        Me.lblScanner.Location = New System.Drawing.Point(30, 48)
        Me.lblScanner.Name = "lblScanner"
        Me.lblScanner.Size = New System.Drawing.Size(54, 13)
        Me.lblScanner.TabIndex = 20
        Me.lblScanner.Text = "Scanner"
        '
        'cmbscanner
        '
        Me.cmbscanner.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox
        Me.cmbscanner.FormattingEnabled = True
        Me.cmbscanner.Items.AddRange(New Object() {"OPOS"})
        Me.cmbscanner.Location = New System.Drawing.Point(170, 40)
        Me.cmbscanner.Name = "cmbscanner"
        Me.cmbscanner.Size = New System.Drawing.Size(140, 21)
        Me.cmbscanner.TabIndex = 10
        '
        'tbLineDisplay
        '
        Me.tbLineDisplay.Controls.Add(Me.btnSaveDisplay)
        Me.tbLineDisplay.Controls.Add(Me.GroupBox1)
        Me.tbLineDisplay.Controls.Add(Me.cmbDisplayName)
        Me.tbLineDisplay.Controls.Add(Me.lblDisplayDeviceName)
        Me.tbLineDisplay.Controls.Add(Me.lblDisplay)
        Me.tbLineDisplay.Controls.Add(Me.cmbDisplay)
        Me.tbLineDisplay.Location = New System.Drawing.Point(1, 25)
        Me.tbLineDisplay.Name = "tbLineDisplay"
        Me.tbLineDisplay.Padding = New System.Windows.Forms.Padding(3)
        Me.tbLineDisplay.Size = New System.Drawing.Size(807, 444)
        Me.tbLineDisplay.TabIndex = 3
        Me.tbLineDisplay.Text = "Line Display"
        '
        'btnSaveDisplay
        '
        Me.btnSaveDisplay.Enabled = False
        Me.btnSaveDisplay.Location = New System.Drawing.Point(354, 256)
        Me.btnSaveDisplay.Name = "btnSaveDisplay"
        Me.btnSaveDisplay.Size = New System.Drawing.Size(87, 23)
        Me.btnSaveDisplay.TabIndex = 3
        Me.btnSaveDisplay.Text = "Save"
        Me.btnSaveDisplay.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtMessage)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.lblstateDisplay)
        Me.GroupBox1.Controls.Add(Me.btnCloseDeviceDisplay)
        Me.GroupBox1.Controls.Add(Me.btnOpenDeviceDisplay)
        Me.GroupBox1.Controls.Add(Me.btnShowMessage)
        Me.GroupBox1.Location = New System.Drawing.Point(168, 105)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(291, 138)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Test"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(26, 82)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Enter Message"
        '
        'txtMessage
        '
        Me.txtMessage.Location = New System.Drawing.Point(152, 74)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(121, 21)
        Me.txtMessage.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(176, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 13)
        Me.Label3.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(187, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(0, 13)
        Me.Label4.TabIndex = 8
        '
        'lblstateDisplay
        '
        Me.lblstateDisplay.AutoSize = True
        Me.lblstateDisplay.Location = New System.Drawing.Point(149, 22)
        Me.lblstateDisplay.Name = "lblstateDisplay"
        Me.lblstateDisplay.Size = New System.Drawing.Size(37, 13)
        Me.lblstateDisplay.TabIndex = 9
        Me.lblstateDisplay.Text = "State"
        '
        'btnCloseDeviceDisplay
        '
        Me.btnCloseDeviceDisplay.Location = New System.Drawing.Point(19, 109)
        Me.btnCloseDeviceDisplay.Name = "btnCloseDeviceDisplay"
        Me.btnCloseDeviceDisplay.Size = New System.Drawing.Size(113, 23)
        Me.btnCloseDeviceDisplay.TabIndex = 20
        Me.btnCloseDeviceDisplay.Text = "Close Device"
        Me.btnCloseDeviceDisplay.UseVisualStyleBackColor = True
        '
        'btnOpenDeviceDisplay
        '
        Me.btnOpenDeviceDisplay.Location = New System.Drawing.Point(19, 17)
        Me.btnOpenDeviceDisplay.Name = "btnOpenDeviceDisplay"
        Me.btnOpenDeviceDisplay.Size = New System.Drawing.Size(113, 23)
        Me.btnOpenDeviceDisplay.TabIndex = 17
        Me.btnOpenDeviceDisplay.Text = "Open Device"
        Me.btnOpenDeviceDisplay.UseVisualStyleBackColor = True
        '
        'btnShowMessage
        '
        Me.btnShowMessage.Location = New System.Drawing.Point(19, 46)
        Me.btnShowMessage.Name = "btnShowMessage"
        Me.btnShowMessage.Size = New System.Drawing.Size(113, 23)
        Me.btnShowMessage.TabIndex = 18
        Me.btnShowMessage.Text = "Show Message"
        Me.btnShowMessage.UseVisualStyleBackColor = True
        '
        'cmbDisplayName
        '
        Me.cmbDisplayName.FormattingEnabled = True
        Me.cmbDisplayName.Location = New System.Drawing.Point(168, 66)
        Me.cmbDisplayName.Name = "cmbDisplayName"
        Me.cmbDisplayName.Size = New System.Drawing.Size(248, 21)
        Me.cmbDisplayName.TabIndex = 16
        '
        'lblDisplayDeviceName
        '
        Me.lblDisplayDeviceName.AutoSize = True
        Me.lblDisplayDeviceName.BackColor = System.Drawing.Color.Transparent
        Me.lblDisplayDeviceName.Location = New System.Drawing.Point(29, 69)
        Me.lblDisplayDeviceName.Name = "lblDisplayDeviceName"
        Me.lblDisplayDeviceName.Size = New System.Drawing.Size(129, 13)
        Me.lblDisplayDeviceName.TabIndex = 12
        Me.lblDisplayDeviceName.Text = "Display Device Name"
        '
        'lblDisplay
        '
        Me.lblDisplay.AutoSize = True
        Me.lblDisplay.BackColor = System.Drawing.Color.Transparent
        Me.lblDisplay.Location = New System.Drawing.Point(29, 47)
        Me.lblDisplay.Name = "lblDisplay"
        Me.lblDisplay.Size = New System.Drawing.Size(92, 13)
        Me.lblDisplay.TabIndex = 11
        Me.lblDisplay.Text = "Display Device"
        '
        'cmbDisplay
        '
        Me.cmbDisplay.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox
        Me.cmbDisplay.FormattingEnabled = True
        Me.cmbDisplay.Items.AddRange(New Object() {"OPOS"})
        Me.cmbDisplay.Location = New System.Drawing.Point(168, 39)
        Me.cmbDisplay.Name = "cmbDisplay"
        Me.cmbDisplay.Size = New System.Drawing.Size(140, 21)
        Me.cmbDisplay.TabIndex = 15
        '
        'tbMSR
        '
        Me.tbMSR.Controls.Add(Me.lblMsr)
        Me.tbMSR.Controls.Add(Me.MsrName)
        Me.tbMSR.Controls.Add(Me.GrpMsrCurrAssigned)
        Me.tbMSR.Controls.Add(Me.btnSaveMsr)
        Me.tbMSR.Controls.Add(Me.GroupBox2)
        Me.tbMSR.Controls.Add(Me.cmbMsrName)
        Me.tbMSR.Controls.Add(Me.cmbMsr)
        Me.tbMSR.Location = New System.Drawing.Point(1, 25)
        Me.tbMSR.Name = "tbMSR"
        Me.tbMSR.Size = New System.Drawing.Size(807, 444)
        Me.tbMSR.TabIndex = 5
        Me.tbMSR.Text = "MSR"
        '
        'lblMsr
        '
        Me.lblMsr.AttachedTextBoxName = Nothing
        Me.lblMsr.AutoSize = True
        Me.lblMsr.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.lblMsr.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblMsr.ForeColor = System.Drawing.Color.Black
        Me.lblMsr.Location = New System.Drawing.Point(30, 48)
        Me.lblMsr.Name = "lblMsr"
        Me.lblMsr.Size = New System.Drawing.Size(92, 13)
        Me.lblMsr.TabIndex = 29
        Me.lblMsr.Tag = Nothing
        Me.lblMsr.Text = "Display Device"
        Me.lblMsr.TextDetached = True
        Me.lblMsr.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'MsrName
        '
        Me.MsrName.AttachedTextBoxName = Nothing
        Me.MsrName.AutoSize = True
        Me.MsrName.BackColor = System.Drawing.Color.Transparent
        Me.MsrName.BorderColor = System.Drawing.Color.Transparent
        Me.MsrName.ForeColor = System.Drawing.Color.Black
        Me.MsrName.Location = New System.Drawing.Point(30, 70)
        Me.MsrName.Name = "MsrName"
        Me.MsrName.Size = New System.Drawing.Size(107, 13)
        Me.MsrName.TabIndex = 31
        Me.MsrName.Tag = Nothing
        Me.MsrName.Text = "Msr Device Name"
        Me.MsrName.TextDetached = True
        Me.MsrName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'GrpMsrCurrAssigned
        '
        Me.GrpMsrCurrAssigned.BackColor = System.Drawing.Color.Transparent
        Me.GrpMsrCurrAssigned.Controls.Add(Me.lblCurrentMsr)
        Me.GrpMsrCurrAssigned.Controls.Add(Me.lblCurrentMSRlogicalName)
        Me.GrpMsrCurrAssigned.Controls.Add(Me.ChkpwayEnabled)
        Me.GrpMsrCurrAssigned.Controls.Add(Me.ChkEnabledMsr)
        Me.GrpMsrCurrAssigned.Location = New System.Drawing.Point(11, 269)
        Me.GrpMsrCurrAssigned.Name = "GrpMsrCurrAssigned"
        Me.GrpMsrCurrAssigned.Size = New System.Drawing.Size(501, 71)
        Me.GrpMsrCurrAssigned.TabIndex = 35
        Me.GrpMsrCurrAssigned.TabStop = False
        Me.GrpMsrCurrAssigned.Text = "Current Assigned MSR "
        '
        'lblCurrentMsr
        '
        Me.lblCurrentMsr.AttachedTextBoxName = Nothing
        Me.lblCurrentMsr.AutoSize = True
        Me.lblCurrentMsr.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrentMsr.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCurrentMsr.ForeColor = System.Drawing.Color.Black
        Me.lblCurrentMsr.Location = New System.Drawing.Point(124, 19)
        Me.lblCurrentMsr.Name = "lblCurrentMsr"
        Me.lblCurrentMsr.Size = New System.Drawing.Size(12, 13)
        Me.lblCurrentMsr.TabIndex = 2
        Me.lblCurrentMsr.Tag = Nothing
        Me.lblCurrentMsr.Text = ":"
        Me.lblCurrentMsr.TextDetached = True
        Me.lblCurrentMsr.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCurrentMSRlogicalName
        '
        Me.lblCurrentMSRlogicalName.AttachedTextBoxName = Nothing
        Me.lblCurrentMSRlogicalName.AutoSize = True
        Me.lblCurrentMSRlogicalName.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrentMSRlogicalName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCurrentMSRlogicalName.ForeColor = System.Drawing.Color.Black
        Me.lblCurrentMSRlogicalName.Location = New System.Drawing.Point(6, 19)
        Me.lblCurrentMSRlogicalName.Name = "lblCurrentMSRlogicalName"
        Me.lblCurrentMSRlogicalName.Size = New System.Drawing.Size(116, 13)
        Me.lblCurrentMSRlogicalName.TabIndex = 0
        Me.lblCurrentMSRlogicalName.Tag = Nothing
        Me.lblCurrentMSRlogicalName.Text = "Msr Logical Name :"
        Me.lblCurrentMSRlogicalName.TextDetached = True
        Me.lblCurrentMSRlogicalName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'ChkpwayEnabled
        '
        Me.ChkpwayEnabled.AutoSize = True
        Me.ChkpwayEnabled.Checked = True
        Me.ChkpwayEnabled.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkpwayEnabled.Location = New System.Drawing.Point(127, 48)
        Me.ChkpwayEnabled.Name = "ChkpwayEnabled"
        Me.ChkpwayEnabled.Size = New System.Drawing.Size(175, 17)
        Me.ChkpwayEnabled.TabIndex = 33
        Me.ChkpwayEnabled.Text = "Payment GatewayEnabled"
        Me.ChkpwayEnabled.UseVisualStyleBackColor = True
        Me.ChkpwayEnabled.Visible = False
        '
        'ChkEnabledMsr
        '
        Me.ChkEnabledMsr.AutoSize = True
        Me.ChkEnabledMsr.Checked = True
        Me.ChkEnabledMsr.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkEnabledMsr.Location = New System.Drawing.Point(424, 19)
        Me.ChkEnabledMsr.Name = "ChkEnabledMsr"
        Me.ChkEnabledMsr.Size = New System.Drawing.Size(71, 17)
        Me.ChkEnabledMsr.TabIndex = 30
        Me.ChkEnabledMsr.Text = "Enabled"
        Me.ChkEnabledMsr.UseVisualStyleBackColor = True
        '
        'btnSaveMsr
        '
        Me.btnSaveMsr.Enabled = False
        Me.btnSaveMsr.Location = New System.Drawing.Point(398, 235)
        Me.btnSaveMsr.Name = "btnSaveMsr"
        Me.btnSaveMsr.Size = New System.Drawing.Size(87, 23)
        Me.btnSaveMsr.TabIndex = 33
        Me.btnSaveMsr.Text = "Save"
        Me.btnSaveMsr.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.lblTrackNo)
        Me.GroupBox2.Controls.Add(Me.TextBox1)
        Me.GroupBox2.Controls.Add(Me.lblstateMsr)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.lblCardDtls)
        Me.GroupBox2.Controls.Add(Me.txtMsrHealthCheck)
        Me.GroupBox2.Controls.Add(Me.btnCloseMsr)
        Me.GroupBox2.Controls.Add(Me.btnOpenMsr)
        Me.GroupBox2.Controls.Add(Me.btnSwipeCard)
        Me.GroupBox2.Location = New System.Drawing.Point(30, 109)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(462, 114)
        Me.GroupBox2.TabIndex = 34
        Me.GroupBox2.TabStop = False
        '
        'lblTrackNo
        '
        Me.lblTrackNo.AttachedTextBoxName = Nothing
        Me.lblTrackNo.AutoSize = True
        Me.lblTrackNo.BackColor = System.Drawing.Color.Transparent
        Me.lblTrackNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTrackNo.ForeColor = System.Drawing.Color.Black
        Me.lblTrackNo.Location = New System.Drawing.Point(139, 51)
        Me.lblTrackNo.Name = "lblTrackNo"
        Me.lblTrackNo.Size = New System.Drawing.Size(61, 13)
        Me.lblTrackNo.TabIndex = 21
        Me.lblTrackNo.Tag = Nothing
        Me.lblTrackNo.Text = "Track No."
        Me.lblTrackNo.TextDetached = True
        Me.lblTrackNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(281, 47)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(43, 21)
        Me.TextBox1.TabIndex = 22
        '
        'lblstateMsr
        '
        Me.lblstateMsr.AttachedTextBoxName = Nothing
        Me.lblstateMsr.AutoSize = True
        Me.lblstateMsr.BackColor = System.Drawing.Color.Transparent
        Me.lblstateMsr.BorderColor = System.Drawing.Color.Transparent
        Me.lblstateMsr.ForeColor = System.Drawing.Color.Black
        Me.lblstateMsr.Location = New System.Drawing.Point(138, 22)
        Me.lblstateMsr.Name = "lblstateMsr"
        Me.lblstateMsr.Size = New System.Drawing.Size(37, 13)
        Me.lblstateMsr.TabIndex = 9
        Me.lblstateMsr.Tag = Nothing
        Me.lblstateMsr.Text = "State"
        Me.lblstateMsr.TextDetached = True
        Me.lblstateMsr.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'Label15
        '
        Me.Label15.AttachedTextBoxName = Nothing
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(187, 22)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(0, 13)
        Me.Label15.TabIndex = 8
        Me.Label15.Tag = Nothing
        Me.Label15.TextDetached = True
        Me.Label15.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCardDtls
        '
        Me.lblCardDtls.AttachedTextBoxName = Nothing
        Me.lblCardDtls.AutoSize = True
        Me.lblCardDtls.BackColor = System.Drawing.Color.Transparent
        Me.lblCardDtls.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCardDtls.ForeColor = System.Drawing.Color.Black
        Me.lblCardDtls.Location = New System.Drawing.Point(138, 79)
        Me.lblCardDtls.Name = "lblCardDtls"
        Me.lblCardDtls.Size = New System.Drawing.Size(106, 13)
        Me.lblCardDtls.TabIndex = 11
        Me.lblCardDtls.Tag = Nothing
        Me.lblCardDtls.Text = "Swiped Card Dtls"
        Me.lblCardDtls.TextDetached = True
        Me.lblCardDtls.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtMsrHealthCheck
        '
        Me.txtMsrHealthCheck.Location = New System.Drawing.Point(281, 75)
        Me.txtMsrHealthCheck.Name = "txtMsrHealthCheck"
        Me.txtMsrHealthCheck.Size = New System.Drawing.Size(171, 21)
        Me.txtMsrHealthCheck.TabIndex = 19
        '
        'btnCloseMsr
        '
        Me.btnCloseMsr.Location = New System.Drawing.Point(19, 75)
        Me.btnCloseMsr.Name = "btnCloseMsr"
        Me.btnCloseMsr.Size = New System.Drawing.Size(98, 23)
        Me.btnCloseMsr.TabIndex = 20
        Me.btnCloseMsr.Text = "Close Device"
        Me.btnCloseMsr.UseVisualStyleBackColor = True
        '
        'btnOpenMsr
        '
        Me.btnOpenMsr.Location = New System.Drawing.Point(19, 17)
        Me.btnOpenMsr.Name = "btnOpenMsr"
        Me.btnOpenMsr.Size = New System.Drawing.Size(98, 23)
        Me.btnOpenMsr.TabIndex = 17
        Me.btnOpenMsr.Text = "Open Device"
        Me.btnOpenMsr.UseVisualStyleBackColor = True
        '
        'btnSwipeCard
        '
        Me.btnSwipeCard.Location = New System.Drawing.Point(19, 46)
        Me.btnSwipeCard.Name = "btnSwipeCard"
        Me.btnSwipeCard.Size = New System.Drawing.Size(98, 23)
        Me.btnSwipeCard.TabIndex = 18
        Me.btnSwipeCard.Text = "Swipe Card"
        Me.btnSwipeCard.UseVisualStyleBackColor = True
        '
        'cmbMsrName
        '
        Me.cmbMsrName.FormattingEnabled = True
        Me.cmbMsrName.Location = New System.Drawing.Point(169, 67)
        Me.cmbMsrName.Name = "cmbMsrName"
        Me.cmbMsrName.Size = New System.Drawing.Size(248, 21)
        Me.cmbMsrName.TabIndex = 32
        '
        'cmbMsr
        '
        Me.cmbMsr.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox
        Me.cmbMsr.FormattingEnabled = True
        Me.cmbMsr.Items.AddRange(New Object() {"OPOS"})
        Me.cmbMsr.Location = New System.Drawing.Point(169, 40)
        Me.cmbMsr.Name = "cmbMsr"
        Me.cmbMsr.Size = New System.Drawing.Size(140, 21)
        Me.cmbMsr.TabIndex = 30
        '
        'tbMiscellaneous
        '
        Me.tbMiscellaneous.Controls.Add(Me.btnProductReset)
        Me.tbMiscellaneous.Controls.Add(Me.btnBackgroundReset)
        Me.tbMiscellaneous.Controls.Add(Me.btnSave)
        Me.tbMiscellaneous.Controls.Add(Me.btnProductButton)
        Me.tbMiscellaneous.Controls.Add(Me.btnBackgroundButton)
        Me.tbMiscellaneous.Controls.Add(Me.txtProduct)
        Me.tbMiscellaneous.Controls.Add(Me.txtBackground)
        Me.tbMiscellaneous.Controls.Add(Me.Label13)
        Me.tbMiscellaneous.Controls.Add(Me.Label9)
        Me.tbMiscellaneous.Location = New System.Drawing.Point(1, 25)
        Me.tbMiscellaneous.Name = "tbMiscellaneous"
        Me.tbMiscellaneous.Padding = New System.Windows.Forms.Padding(3)
        Me.tbMiscellaneous.Size = New System.Drawing.Size(807, 444)
        Me.tbMiscellaneous.TabIndex = 4
        Me.tbMiscellaneous.Text = "Miscellaneous"
        '
        'btnProductReset
        '
        Me.btnProductReset.Location = New System.Drawing.Point(434, 75)
        Me.btnProductReset.Name = "btnProductReset"
        Me.btnProductReset.Size = New System.Drawing.Size(75, 23)
        Me.btnProductReset.TabIndex = 8
        Me.btnProductReset.Text = "Reset"
        Me.btnProductReset.UseVisualStyleBackColor = True
        '
        'btnBackgroundReset
        '
        Me.btnBackgroundReset.Location = New System.Drawing.Point(434, 43)
        Me.btnBackgroundReset.Name = "btnBackgroundReset"
        Me.btnBackgroundReset.Size = New System.Drawing.Size(75, 23)
        Me.btnBackgroundReset.TabIndex = 7
        Me.btnBackgroundReset.Text = "Reset"
        Me.btnBackgroundReset.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(194, 104)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 25)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnProductButton
        '
        Me.btnProductButton.Location = New System.Drawing.Point(354, 75)
        Me.btnProductButton.Name = "btnProductButton"
        Me.btnProductButton.Size = New System.Drawing.Size(75, 23)
        Me.btnProductButton.TabIndex = 5
        Me.btnProductButton.Text = "Browse"
        Me.btnProductButton.UseVisualStyleBackColor = True
        '
        'btnBackgroundButton
        '
        Me.btnBackgroundButton.Location = New System.Drawing.Point(354, 43)
        Me.btnBackgroundButton.Name = "btnBackgroundButton"
        Me.btnBackgroundButton.Size = New System.Drawing.Size(75, 23)
        Me.btnBackgroundButton.TabIndex = 4
        Me.btnBackgroundButton.Text = "Browse"
        Me.btnBackgroundButton.UseVisualStyleBackColor = True
        '
        'txtProduct
        '
        Me.txtProduct.Location = New System.Drawing.Point(194, 77)
        Me.txtProduct.Name = "txtProduct"
        Me.txtProduct.ReadOnly = True
        Me.txtProduct.Size = New System.Drawing.Size(154, 21)
        Me.txtProduct.TabIndex = 3
        '
        'txtBackground
        '
        Me.txtBackground.Location = New System.Drawing.Point(194, 45)
        Me.txtBackground.Name = "txtBackground"
        Me.txtBackground.ReadOnly = True
        Me.txtBackground.Size = New System.Drawing.Size(154, 21)
        Me.txtBackground.TabIndex = 2
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Location = New System.Drawing.Point(30, 80)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(155, 13)
        Me.Label13.TabIndex = 1
        Me.Label13.Text = "Product image folder path"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(30, 48)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(143, 13)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Background image path"
        '
        'PosdbDataSet
        '
        Me.PosdbDataSet.DataSetName = "POSDBDataSet"
        Me.PosdbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(1, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(99, 17)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Terminal Id :"
        '
        'lblTerminalId
        '
        Me.lblTerminalId.AutoSize = True
        Me.lblTerminalId.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTerminalId.Location = New System.Drawing.Point(112, 9)
        Me.lblTerminalId.Name = "lblTerminalId"
        Me.lblTerminalId.Size = New System.Drawing.Size(0, 17)
        Me.lblTerminalId.TabIndex = 2
        '
        'BackgroundFileDialog
        '
        Me.BackgroundFileDialog.FileName = "OpenFileDialog1"
        '
        'frmPOSDeviceProfile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(809, 502)
        Me.Controls.Add(Me.lblTerminalId)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.tcPOSDevice)
        Me.MinimizeBox = False
        Me.Name = "frmPOSDeviceProfile"
        Me.Text = "POS Device Profile"
        CType(Me.tcPOSDevice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tcPOSDevice.ResumeLayout(False)
        Me.tbPrinter.ResumeLayout(False)
        Me.tbPrinter.PerformLayout()
        CType(Me.grdprinter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpAssignedPrn.ResumeLayout(False)
        Me.GrpAssignedPrn.PerformLayout()
        Me.gbTestPrinter.ResumeLayout(False)
        Me.gbTestPrinter.PerformLayout()
        Me.tbDrawer.ResumeLayout(False)
        Me.tbDrawer.PerformLayout()
        Me.GrpDrawer.ResumeLayout(False)
        Me.GrpDrawer.PerformLayout()
        Me.gbTestDrawer.ResumeLayout(False)
        Me.gbTestDrawer.PerformLayout()
        Me.tbScanner.ResumeLayout(False)
        Me.tbScanner.PerformLayout()
        Me.gbTestScanner.ResumeLayout(False)
        Me.gbTestScanner.PerformLayout()
        Me.tbLineDisplay.ResumeLayout(False)
        Me.tbLineDisplay.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.tbMSR.ResumeLayout(False)
        Me.tbMSR.PerformLayout()
        CType(Me.lblMsr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MsrName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpMsrCurrAssigned.ResumeLayout(False)
        Me.GrpMsrCurrAssigned.PerformLayout()
        CType(Me.lblCurrentMsr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrentMSRlogicalName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.lblTrackNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblstateMsr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCardDtls, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbMiscellaneous.ResumeLayout(False)
        Me.tbMiscellaneous.PerformLayout()
        CType(Me.PosdbDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tcPOSDevice As Spectrum.CtrlTab
    Friend WithEvents tbPrinter As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents tbDrawer As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents tbScanner As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbPrinter As System.Windows.Forms.ComboBox
    Friend WithEvents gbTestPrinter As System.Windows.Forms.GroupBox
    Friend WithEvents btnCloseDevicePrinter As C1.Win.C1Input.C1Button
    Friend WithEvents btnOpenDevicePrinter As C1.Win.C1Input.C1Button
    Friend WithEvents btnPrintSlip As C1.Win.C1Input.C1Button
    Friend WithEvents cmbPrinterName As System.Windows.Forms.ComboBox
    Friend WithEvents lblStatePrinter As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents gbTestDrawer As System.Windows.Forms.GroupBox
    Friend WithEvents lblStateDrawer As System.Windows.Forms.Label
    Friend WithEvents btnCloseDeviceDrawer As C1.Win.C1Input.C1Button
    Friend WithEvents btnOpenDeviceDrawer As C1.Win.C1Input.C1Button
    Friend WithEvents btnOpenDrawer As C1.Win.C1Input.C1Button
    Friend WithEvents cmbDrawerName As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblDrawer As System.Windows.Forms.Label
    Friend WithEvents cmbDrawer As System.Windows.Forms.ComboBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents gbTestScanner As System.Windows.Forms.GroupBox
    Friend WithEvents lblStateScanner As System.Windows.Forms.Label
    Friend WithEvents btnCLoseDeviceScanner As C1.Win.C1Input.C1Button
    Friend WithEvents btnOpenDeviceScanner As C1.Win.C1Input.C1Button
    Friend WithEvents cmbScannerName As System.Windows.Forms.ComboBox
    Friend WithEvents lblScannerName As System.Windows.Forms.Label
    Friend WithEvents lblScanner As System.Windows.Forms.Label
    Friend WithEvents cmbscanner As System.Windows.Forms.ComboBox
    Friend WithEvents txtScannerOutput As System.Windows.Forms.TextBox
    Friend WithEvents lblStatusPrinter As System.Windows.Forms.Label
    Friend WithEvents lblCurrentState As System.Windows.Forms.Label
    Friend WithEvents tbLineDisplay As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblstateDisplay As System.Windows.Forms.Label
    Friend WithEvents btnCloseDeviceDisplay As C1.Win.C1Input.C1Button
    Friend WithEvents btnOpenDeviceDisplay As C1.Win.C1Input.C1Button
    Friend WithEvents btnShowMessage As C1.Win.C1Input.C1Button
    Friend WithEvents cmbDisplayName As System.Windows.Forms.ComboBox
    Private WithEvents lblDisplayDeviceName As System.Windows.Forms.Label
    Friend WithEvents lblDisplay As System.Windows.Forms.Label
    Friend WithEvents cmbDisplay As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents PosdbDataSet As SpectrumBL.POSDBDataSet
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblTerminalId As System.Windows.Forms.Label
    Friend WithEvents btnSaveDisplay As C1.Win.C1Input.C1Button
    Friend WithEvents btnSaveDrawer As C1.Win.C1Input.C1Button
    Friend WithEvents btnSaveScanner As C1.Win.C1Input.C1Button
    Friend WithEvents btnSavePrinter As C1.Win.C1Input.C1Button
    Friend WithEvents GrpAssignedPrn As System.Windows.Forms.GroupBox
    Friend WithEvents lblOPOSWinPrn As System.Windows.Forms.Label
    Friend WithEvents lblCurrWinPrn As System.Windows.Forms.Label
    Friend WithEvents lblCurrOPOSPrnName As System.Windows.Forms.Label
    Friend WithEvents lblCurrWinPrnName As System.Windows.Forms.Label
    Friend WithEvents GrpDrawer As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblOtherWinPrnName As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblDisplCashDrawar As System.Windows.Forms.Label
    Friend WithEvents tbMiscellaneous As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents btnSave As C1.Win.C1Input.C1Button
    Friend WithEvents btnProductButton As C1.Win.C1Input.C1Button
    Friend WithEvents btnBackgroundButton As C1.Win.C1Input.C1Button
    Friend WithEvents txtProduct As System.Windows.Forms.TextBox
    Friend WithEvents txtBackground As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ProductFolderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents BackgroundFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnProductReset As C1.Win.C1Input.C1Button
    Friend WithEvents btnBackgroundReset As C1.Win.C1Input.C1Button
    Friend WithEvents tbMSR As C1.Win.C1Command.C1DockingTabPage
    Private WithEvents MsrName As Spectrum.CtrlLabel
    Friend WithEvents lblMsr As Spectrum.CtrlLabel
    Friend WithEvents GrpMsrCurrAssigned As System.Windows.Forms.GroupBox
    Friend WithEvents lblCurrentMsr As Spectrum.CtrlLabel
    Friend WithEvents lblCurrentMSRlogicalName As Spectrum.CtrlLabel
    Friend WithEvents ChkpwayEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents ChkEnabledMsr As System.Windows.Forms.CheckBox
    Friend WithEvents btnSaveMsr As C1.Win.C1Input.C1Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label15 As Spectrum.CtrlLabel
    Friend WithEvents lblCardDtls As Spectrum.CtrlLabel
    Friend WithEvents lblstateMsr As Spectrum.CtrlLabel
    Friend WithEvents txtMsrHealthCheck As System.Windows.Forms.TextBox
    Friend WithEvents btnCloseMsr As C1.Win.C1Input.C1Button
    Friend WithEvents btnOpenMsr As C1.Win.C1Input.C1Button
    Friend WithEvents btnSwipeCard As C1.Win.C1Input.C1Button
    Friend WithEvents cmbMsrName As System.Windows.Forms.ComboBox
    Friend WithEvents cmbMsr As System.Windows.Forms.ComboBox
    Friend WithEvents lblTrackNo As Spectrum.CtrlLabel
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents grdprinter As System.Windows.Forms.DataGridView
End Class
