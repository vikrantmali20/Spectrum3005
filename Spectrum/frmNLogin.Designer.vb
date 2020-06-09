<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNLogin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNLogin))
        Dim Blend1 As System.Drawing.Drawing2D.Blend = New System.Drawing.Drawing2D.Blend()
        Me.C1Sizer1 = New C1.Win.C1Sizer.C1Sizer()
        Me.lblSite = New Spectrum.CtrlLabel()
        Me.cmbSite = New Spectrum.ctrlCombo()
        Me.lblErrortxtPassward = New Spectrum.CtrlLabel()
        Me.lblErrorTxtUsername = New Spectrum.CtrlLabel()
        Me.txtIDcard = New Spectrum.CtrlTextBox()
        Me.btnLogin = New Spectrum.CtrlBtn()
        Me.btnCancel = New Spectrum.CtrlBtn()
        Me.lblMessage = New Spectrum.CtrlLabel()
        Me.lblUserId = New Spectrum.CtrlLabel()
        Me.cboLanguage = New Spectrum.ctrlCombo()
        Me.lblPassword = New Spectrum.CtrlLabel()
        Me.lblID = New Spectrum.CtrlLabel()
        Me.txtusername = New Spectrum.CtrlTextBox()
        Me.pwduserpassword = New Spectrum.CtrlTextBox()
        Me.lblError = New Spectrum.CtrlLabel()
        Me.ctrlBtnDbConnection = New Spectrum.CtrlBtn()
        Me.ctrlBtnAutoUpdate = New Spectrum.CtrlBtn()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer1.SuspendLayout()
        CType(Me.lblSite, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSite, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblErrortxtPassward, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblErrorTxtUsername, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIDcard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMessage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUserId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboLanguage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPassword, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtusername, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pwduserpassword, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblError, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'C1Sizer1
        '
        resources.ApplyResources(Me.C1Sizer1, "C1Sizer1")
        Me.C1Sizer1.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1Sizer1.Controls.Add(Me.lblSite)
        Me.C1Sizer1.Controls.Add(Me.cmbSite)
        Me.C1Sizer1.Controls.Add(Me.lblErrortxtPassward)
        Me.C1Sizer1.Controls.Add(Me.lblErrorTxtUsername)
        Me.C1Sizer1.Controls.Add(Me.txtIDcard)
        Me.C1Sizer1.Controls.Add(Me.btnLogin)
        Me.C1Sizer1.Controls.Add(Me.btnCancel)
        Me.C1Sizer1.Controls.Add(Me.lblMessage)
        Me.C1Sizer1.Controls.Add(Me.lblUserId)
        Me.C1Sizer1.Controls.Add(Me.cboLanguage)
        Me.C1Sizer1.Controls.Add(Me.lblPassword)
        Me.C1Sizer1.Controls.Add(Me.lblID)
        Me.C1Sizer1.Controls.Add(Me.txtusername)
        Me.C1Sizer1.Controls.Add(Me.pwduserpassword)
        Me.C1Sizer1.Gradient.BackColor2 = System.Drawing.Color.AliceBlue
        Blend1.Factors = New Single() {0.0!, 1.0!}
        Blend1.Positions = New Single() {0.0!, 1.0!}
        Me.C1Sizer1.Gradient.Blend = Blend1
        Me.C1Sizer1.Gradient.Mode = C1.Win.C1Sizer.GradientMode.Radial
        Me.C1Sizer1.GridDefinition = resources.GetString("C1Sizer1.GridDefinition")
        Me.C1Sizer1.Name = "C1Sizer1"
        Me.C1Sizer1.SplitterWidth = 0
        Me.C1Sizer1.TabStop = False
        '
        'lblSite
        '
        Me.lblSite.AttachedTextBoxName = Nothing
        resources.ApplyResources(Me.lblSite, "lblSite")
        Me.lblSite.BackColor = System.Drawing.Color.Transparent
        Me.lblSite.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblSite.ForeColor = System.Drawing.Color.Black
        Me.lblSite.Name = "lblSite"
        Me.lblSite.TextDetached = True
        Me.lblSite.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmbSite
        '
        Me.cmbSite.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbSite.AutoCompletion = True
        Me.cmbSite.AutoDropDown = True
        resources.ApplyResources(Me.cmbSite, "cmbSite")
        Me.cmbSite.CaptionHeight = 17
        Me.cmbSite.CaptionVisible = False
        Me.cmbSite.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbSite.ColumnCaptionHeight = 17
        Me.cmbSite.ColumnFooterHeight = 17
        Me.cmbSite.ColumnHeaders = False
        Me.cmbSite.ContentHeight = 16
        Me.cmbSite.ctrlTextDbColumn = ""
        Me.cmbSite.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbSite.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cmbSite.EditorFont = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmbSite.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbSite.EditorHeight = 16
        Me.cmbSite.Images.Add(CType(resources.GetObject("cmbSite.Images"), System.Drawing.Image))
        Me.cmbSite.ItemHeight = 15
        Me.cmbSite.MatchEntryTimeout = CType(2000, Long)
        Me.cmbSite.MaxDropDownItems = CType(5, Short)
        Me.cmbSite.MaxLength = 32767
        Me.cmbSite.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbSite.MoveToNxtCtrl = Nothing
        Me.cmbSite.Name = "cmbSite"
        Me.cmbSite.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbSite.strSelectStmt = ""
        Me.cmbSite.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbSite.PropBag = resources.GetString("cmbSite.PropBag")
        '
        'lblErrortxtPassward
        '
        Me.lblErrortxtPassward.AttachedTextBoxName = Nothing
        resources.ApplyResources(Me.lblErrortxtPassward, "lblErrortxtPassward")
        Me.lblErrortxtPassward.BackColor = System.Drawing.Color.Transparent
        Me.lblErrortxtPassward.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblErrortxtPassward.ForeColor = System.Drawing.Color.Black
        Me.lblErrortxtPassward.Name = "lblErrortxtPassward"
        Me.lblErrortxtPassward.TextDetached = True
        Me.lblErrortxtPassward.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblErrorTxtUsername
        '
        Me.lblErrorTxtUsername.AttachedTextBoxName = Nothing
        resources.ApplyResources(Me.lblErrorTxtUsername, "lblErrorTxtUsername")
        Me.lblErrorTxtUsername.BackColor = System.Drawing.Color.Transparent
        Me.lblErrorTxtUsername.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblErrorTxtUsername.ForeColor = System.Drawing.Color.Black
        Me.lblErrorTxtUsername.Name = "lblErrorTxtUsername"
        Me.lblErrorTxtUsername.TextDetached = True
        Me.lblErrorTxtUsername.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtIDcard
        '
        resources.ApplyResources(Me.txtIDcard, "txtIDcard")
        Me.txtIDcard.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.txtIDcard.HideSelection = False
        Me.txtIDcard.MoveToNxtCtrl = Nothing
        Me.txtIDcard.Name = "txtIDcard"
        Me.txtIDcard.TextDetached = True
        Me.txtIDcard.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        Me.txtIDcard.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'btnLogin
        '
        resources.ApplyResources(Me.btnLogin, "btnLogin")
        Me.btnLogin.MoveToNxtCtrl = Nothing
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.SetArticleCode = Nothing
        Me.btnLogin.SetRowIndex = 0
        Me.btnLogin.UseVisualStyleBackColor = True
        Me.btnLogin.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnCancel
        '
        resources.ApplyResources(Me.btnCancel, "btnCancel")
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.MoveToNxtCtrl = Nothing
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.SetArticleCode = Nothing
        Me.btnCancel.SetRowIndex = 0
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblMessage
        '
        Me.lblMessage.AttachedTextBoxName = Nothing
        resources.ApplyResources(Me.lblMessage, "lblMessage")
        Me.lblMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblMessage.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblMessage.ForeColor = System.Drawing.Color.Black
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.TextDetached = True
        Me.lblMessage.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblUserId
        '
        Me.lblUserId.AttachedTextBoxName = Nothing
        resources.ApplyResources(Me.lblUserId, "lblUserId")
        Me.lblUserId.BackColor = System.Drawing.Color.Transparent
        Me.lblUserId.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblUserId.ForeColor = System.Drawing.Color.Black
        Me.lblUserId.Name = "lblUserId"
        Me.lblUserId.TextDetached = True
        Me.lblUserId.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cboLanguage
        '
        Me.cboLanguage.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboLanguage.AutoCompletion = True
        Me.cboLanguage.AutoDropDown = True
        resources.ApplyResources(Me.cboLanguage, "cboLanguage")
        Me.cboLanguage.CaptionHeight = 17
        Me.cboLanguage.CaptionVisible = False
        Me.cboLanguage.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboLanguage.ColumnCaptionHeight = 17
        Me.cboLanguage.ColumnFooterHeight = 17
        Me.cboLanguage.ColumnHeaders = False
        Me.cboLanguage.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList
        Me.cboLanguage.ContentHeight = 16
        Me.cboLanguage.ctrlTextDbColumn = ""
        Me.cboLanguage.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboLanguage.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboLanguage.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLanguage.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboLanguage.EditorHeight = 16
        Me.cboLanguage.Images.Add(CType(resources.GetObject("cboLanguage.Images"), System.Drawing.Image))
        Me.cboLanguage.ItemHeight = 15
        Me.cboLanguage.MatchEntryTimeout = CType(2000, Long)
        Me.cboLanguage.MaxDropDownItems = CType(5, Short)
        Me.cboLanguage.MaxLength = 32767
        Me.cboLanguage.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboLanguage.MoveToNxtCtrl = Nothing
        Me.cboLanguage.Name = "cboLanguage"
        Me.cboLanguage.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboLanguage.strSelectStmt = ""
        Me.cboLanguage.SuperBack = True
        Me.cboLanguage.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cboLanguage.PropBag = resources.GetString("cboLanguage.PropBag")
        '
        'lblPassword
        '
        Me.lblPassword.AttachedTextBoxName = Nothing
        resources.ApplyResources(Me.lblPassword, "lblPassword")
        Me.lblPassword.BackColor = System.Drawing.Color.Transparent
        Me.lblPassword.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblPassword.ForeColor = System.Drawing.Color.Black
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.TextDetached = True
        Me.lblPassword.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblID
        '
        Me.lblID.AttachedTextBoxName = Nothing
        resources.ApplyResources(Me.lblID, "lblID")
        Me.lblID.BackColor = System.Drawing.Color.Transparent
        Me.lblID.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblID.ForeColor = System.Drawing.Color.Black
        Me.lblID.Name = "lblID"
        Me.lblID.TextDetached = True
        Me.lblID.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtusername
        '
        resources.ApplyResources(Me.txtusername, "txtusername")
        Me.txtusername.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.txtusername.HideSelection = False
        Me.txtusername.MoveToNxtCtrl = Nothing
        Me.txtusername.Name = "txtusername"
        Me.txtusername.TextDetached = True
        Me.txtusername.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        Me.txtusername.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'pwduserpassword
        '
        resources.ApplyResources(Me.pwduserpassword, "pwduserpassword")
        Me.pwduserpassword.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(214, Byte), Integer))
        Me.pwduserpassword.HideSelection = False
        Me.pwduserpassword.MoveToNxtCtrl = Nothing
        Me.pwduserpassword.Name = "pwduserpassword"
        Me.pwduserpassword.TextDetached = True
        Me.pwduserpassword.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        Me.pwduserpassword.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'lblError
        '
        Me.lblError.AttachedTextBoxName = Nothing
        Me.lblError.BackColor = System.Drawing.Color.Transparent
        Me.lblError.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblError.ForeColor = System.Drawing.Color.Red
        resources.ApplyResources(Me.lblError, "lblError")
        Me.lblError.Name = "lblError"
        Me.lblError.TextDetached = True
        Me.lblError.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'ctrlBtnDbConnection
        '
        resources.ApplyResources(Me.ctrlBtnDbConnection, "ctrlBtnDbConnection")
        Me.ctrlBtnDbConnection.MoveToNxtCtrl = Nothing
        Me.ctrlBtnDbConnection.Name = "ctrlBtnDbConnection"
        Me.ctrlBtnDbConnection.SetArticleCode = Nothing
        Me.ctrlBtnDbConnection.SetRowIndex = 0
        Me.ctrlBtnDbConnection.UseVisualStyleBackColor = True
        Me.ctrlBtnDbConnection.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'ctrlBtnAutoUpdate
        '
        resources.ApplyResources(Me.ctrlBtnAutoUpdate, "ctrlBtnAutoUpdate")
        Me.ctrlBtnAutoUpdate.MoveToNxtCtrl = Nothing
        Me.ctrlBtnAutoUpdate.Name = "ctrlBtnAutoUpdate"
        Me.ctrlBtnAutoUpdate.SetArticleCode = Nothing
        Me.ctrlBtnAutoUpdate.SetRowIndex = 0
        Me.ctrlBtnAutoUpdate.UseVisualStyleBackColor = True
        Me.ctrlBtnAutoUpdate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmNLogin
        '
        Me.AcceptButton = Me.btnLogin
        Me.BackgroundImage = Global.Spectrum.My.Resources.Resources.login
        resources.ApplyResources(Me, "$this")
        Me.CancelButton = Me.btnCancel
        Me.ControlBox = False
        Me.Controls.Add(Me.ctrlBtnAutoUpdate)
        Me.Controls.Add(Me.ctrlBtnDbConnection)
        Me.Controls.Add(Me.lblError)
        Me.Controls.Add(Me.C1Sizer1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNLogin"
        Me.TransparencyKey = System.Drawing.Color.Gray
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer1.ResumeLayout(False)
        Me.C1Sizer1.PerformLayout()
        CType(Me.lblSite, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSite, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblErrortxtPassward, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblErrorTxtUsername, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIDcard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMessage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUserId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboLanguage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPassword, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtusername, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pwduserpassword, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblError, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents C1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents lblUserId As Spectrum.CtrlLabel
    Friend WithEvents lblID As Spectrum.CtrlLabel
    Friend WithEvents lblPassword As Spectrum.CtrlLabel
    Friend WithEvents pwduserpassword As Spectrum.CtrlTextBox
    Friend WithEvents txtusername As Spectrum.CtrlTextBox
    Friend WithEvents btnCancel As Spectrum.CtrlBtn
    Friend WithEvents btnLogin As Spectrum.CtrlBtn
    Friend WithEvents lblMessage As Spectrum.CtrlLabel
    Friend WithEvents txtIDcard As Spectrum.CtrlTextBox
    Friend WithEvents cboLanguage As Spectrum.ctrlCombo
    Friend WithEvents lblErrortxtPassward As Spectrum.CtrlLabel
    Friend WithEvents lblErrorTxtUsername As Spectrum.CtrlLabel
    Friend WithEvents lblError As Spectrum.CtrlLabel
    Friend WithEvents ctrlBtnDbConnection As Spectrum.CtrlBtn
    Friend WithEvents cmbSite As Spectrum.ctrlCombo
    Friend WithEvents lblSite As Spectrum.CtrlLabel
    Friend WithEvents ctrlBtnAutoUpdate As Spectrum.CtrlBtn

End Class
