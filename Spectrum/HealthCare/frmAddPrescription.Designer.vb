<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddPrescription
    'Inherits C1.Win.C1Ribbon.C1RibbonForm
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddPrescription))
        Me.C1Sizer2main = New C1.Win.C1Sizer.C1Sizer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        Me.C1Sizer1 = New C1.Win.C1Sizer.C1Sizer()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.dgGridArticle = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnClear = New Spectrum.CtrlBtn()
        Me.btnCancel = New Spectrum.CtrlBtn()
        Me.btnSave = New Spectrum.CtrlBtn()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblAddArticle = New Spectrum.CtrlLabel()
        Me.txtAndroidArticleSearchTextBox = New Spectrum.AndroidSearchTextBox(Me.components)
        Me.lblRemark = New Spectrum.CtrlLabel()
        Me.txtRemarks = New System.Windows.Forms.RichTextBox()
        Me.btnAddRemark = New Spectrum.CtrlBtn()
        CType(Me.C1Sizer2main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer2main.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.dgGridArticle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.lblAddArticle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'C1Sizer2main
        '
        Me.C1Sizer2main.BackColor = System.Drawing.Color.Gray
        Me.C1Sizer2main.Controls.Add(Me.Panel1)
        Me.C1Sizer2main.Controls.Add(Me.C1Sizer1)
        Me.C1Sizer2main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Sizer2main.GridDefinition = "4.50097847358121:False:False;93.1506849315068:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "98.8165680473373:False" & _
    ":False;"
        Me.C1Sizer2main.Location = New System.Drawing.Point(0, 0)
        Me.C1Sizer2main.Name = "C1Sizer2main"
        Me.C1Sizer2main.Padding = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.C1Sizer2main.Size = New System.Drawing.Size(845, 511)
        Me.C1Sizer2main.TabIndex = 10
        Me.C1Sizer2main.TabStop = False
        Me.C1Sizer2main.Text = "C1Sizer2"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Gray
        Me.Panel1.Controls.Add(Me.CtrlLabel1)
        Me.Panel1.Location = New System.Drawing.Point(5, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(835, 23)
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
        Me.CtrlLabel1.Size = New System.Drawing.Size(127, 16)
        Me.CtrlLabel1.TabIndex = 0
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.Text = "Add Prescription"
        Me.CtrlLabel1.TextDetached = True
        Me.CtrlLabel1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'C1Sizer1
        '
        Me.C1Sizer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C1Sizer1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.C1Sizer1.Controls.Add(Me.Panel4)
        Me.C1Sizer1.Controls.Add(Me.Panel3)
        Me.C1Sizer1.Controls.Add(Me.Panel2)
        Me.C1Sizer1.GridDefinition = "35.9243697478992:False:True;52.7310924369748:False:True;34.2436974789916:False:Tr" & _
    "ue;0:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "98.8023952095808:False:False;"
        Me.C1Sizer1.Location = New System.Drawing.Point(5, 31)
        Me.C1Sizer1.Name = "C1Sizer1"
        Me.C1Sizer1.Padding = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.C1Sizer1.Size = New System.Drawing.Size(835, 476)
        Me.C1Sizer1.TabIndex = 1
        Me.C1Sizer1.Text = "C1Sizer1"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.Controls.Add(Me.dgGridArticle)
        Me.Panel4.Location = New System.Drawing.Point(5, 179)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(825, 251)
        Me.Panel4.TabIndex = 8
        '
        'dgGridArticle
        '
        Me.dgGridArticle.AutoGenerateColumns = False
        Me.dgGridArticle.CellButtonImage = Global.Spectrum.My.Resources.Resources.del_icon
        Me.dgGridArticle.ColumnInfo = resources.GetString("dgGridArticle.ColumnInfo")
        Me.dgGridArticle.ExtendLastCol = True
        Me.dgGridArticle.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.dgGridArticle.Location = New System.Drawing.Point(35, 10)
        Me.dgGridArticle.Name = "dgGridArticle"
        Me.dgGridArticle.NewRowWatermark = ""
        Me.dgGridArticle.Rows.Count = 1
        Me.dgGridArticle.Rows.DefaultSize = 20
        Me.dgGridArticle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgGridArticle.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox
        Me.dgGridArticle.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.dgGridArticle.Size = New System.Drawing.Size(746, 228)
        Me.dgGridArticle.StyleInfo = resources.GetString("dgGridArticle.StyleInfo")
        Me.dgGridArticle.TabIndex = 6
        Me.dgGridArticle.Tag = ""
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.Controls.Add(Me.btnClear)
        Me.Panel3.Controls.Add(Me.btnCancel)
        Me.Panel3.Controls.Add(Me.btnSave)
        Me.Panel3.Location = New System.Drawing.Point(5, 434)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(825, 167)
        Me.Panel3.TabIndex = 7
        '
        'btnClear
        '
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnClear.Location = New System.Drawing.Point(705, 6)
        Me.btnClear.MoveToNxtCtrl = Nothing
        Me.btnClear.Name = "btnClear"
        Me.btnClear.SetArticleCode = Nothing
        Me.btnClear.SetRowIndex = 0
        Me.btnClear.Size = New System.Drawing.Size(76, 24)
        Me.btnClear.TabIndex = 9
        Me.btnClear.Text = "Clear"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnClear.UseVisualStyleBackColor = True
        Me.btnClear.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnCancel
        '
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancel.Location = New System.Drawing.Point(623, 6)
        Me.btnCancel.MoveToNxtCtrl = Nothing
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.SetArticleCode = Nothing
        Me.btnCancel.SetRowIndex = 0
        Me.btnCancel.Size = New System.Drawing.Size(76, 24)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnSave
        '
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSave.Location = New System.Drawing.Point(541, 6)
        Me.btnSave.MoveToNxtCtrl = Nothing
        Me.btnSave.Name = "btnSave"
        Me.btnSave.SetArticleCode = Nothing
        Me.btnSave.SetRowIndex = 0
        Me.btnSave.Size = New System.Drawing.Size(76, 24)
        Me.btnSave.TabIndex = 7
        Me.btnSave.Text = "Save"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnSave.UseVisualStyleBackColor = True
        Me.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.lblAddArticle)
        Me.Panel2.Controls.Add(Me.txtAndroidArticleSearchTextBox)
        Me.Panel2.Controls.Add(Me.lblRemark)
        Me.Panel2.Controls.Add(Me.txtRemarks)
        Me.Panel2.Controls.Add(Me.btnAddRemark)
        Me.Panel2.Location = New System.Drawing.Point(5, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(825, 171)
        Me.Panel2.TabIndex = 0
        '
        'lblAddArticle
        '
        Me.lblAddArticle.AttachedTextBoxName = Nothing
        Me.lblAddArticle.AutoSize = True
        Me.lblAddArticle.BackColor = System.Drawing.Color.Transparent
        Me.lblAddArticle.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblAddArticle.Font = New System.Drawing.Font("Calibri", 12.75!)
        Me.lblAddArticle.ForeColor = System.Drawing.Color.Black
        Me.lblAddArticle.Location = New System.Drawing.Point(73, 13)
        Me.lblAddArticle.Name = "lblAddArticle"
        Me.lblAddArticle.Size = New System.Drawing.Size(103, 21)
        Me.lblAddArticle.TabIndex = 1
        Me.lblAddArticle.Tag = Nothing
        Me.lblAddArticle.Text = "Add Article :*"
        Me.lblAddArticle.TextDetached = True
        Me.lblAddArticle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtAndroidArticleSearchTextBox
        '
        Me.txtAndroidArticleSearchTextBox.AllowUpdateListBox = True
        Me.txtAndroidArticleSearchTextBox.DtSearchData = Nothing
        Me.txtAndroidArticleSearchTextBox.IsItemSelected = False
        Me.txtAndroidArticleSearchTextBox.IsListBind = True
        Me.txtAndroidArticleSearchTextBox.IsMouseOverList = False
        Me.txtAndroidArticleSearchTextBox.IsMovingControl = False
        Me.txtAndroidArticleSearchTextBox.Location = New System.Drawing.Point(215, 15)
        Me.txtAndroidArticleSearchTextBox.lstNames = CType(resources.GetObject("txtAndroidArticleSearchTextBox.lstNames"), System.Collections.Generic.List(Of String))
        Me.txtAndroidArticleSearchTextBox.MaxLength = 35
        Me.txtAndroidArticleSearchTextBox.Name = "txtAndroidArticleSearchTextBox"
        Me.txtAndroidArticleSearchTextBox.SearchBasedOnDB = Nothing
        Me.txtAndroidArticleSearchTextBox.SearchQueryOnDB = Nothing
        Me.txtAndroidArticleSearchTextBox.Size = New System.Drawing.Size(453, 21)
        Me.txtAndroidArticleSearchTextBox.TabIndex = 2
        '
        'lblRemark
        '
        Me.lblRemark.AttachedTextBoxName = Nothing
        Me.lblRemark.AutoSize = True
        Me.lblRemark.BackColor = System.Drawing.Color.Transparent
        Me.lblRemark.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblRemark.Font = New System.Drawing.Font("Calibri", 12.75!)
        Me.lblRemark.ForeColor = System.Drawing.Color.Black
        Me.lblRemark.Location = New System.Drawing.Point(73, 59)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(79, 21)
        Me.lblRemark.TabIndex = 3
        Me.lblRemark.Tag = Nothing
        Me.lblRemark.Text = "Remarks :"
        Me.lblRemark.TextDetached = True
        Me.lblRemark.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(215, 59)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(453, 96)
        Me.txtRemarks.TabIndex = 4
        Me.txtRemarks.Text = ""
        '
        'btnAddRemark
        '
        Me.btnAddRemark.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnAddRemark.Location = New System.Drawing.Point(694, 131)
        Me.btnAddRemark.MoveToNxtCtrl = Nothing
        Me.btnAddRemark.Name = "btnAddRemark"
        Me.btnAddRemark.SetArticleCode = Nothing
        Me.btnAddRemark.SetRowIndex = 0
        Me.btnAddRemark.Size = New System.Drawing.Size(87, 23)
        Me.btnAddRemark.TabIndex = 5
        Me.btnAddRemark.Text = "Add"
        Me.btnAddRemark.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAddRemark.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnAddRemark.UseVisualStyleBackColor = True
        Me.btnAddRemark.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmAddPrescription
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(845, 511)
        Me.Controls.Add(Me.C1Sizer2main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmAddPrescription"
        Me.Text = "frmAddPrescription"
        CType(Me.C1Sizer2main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer2main.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.dgGridArticle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.lblAddArticle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemark, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblAddArticle As Spectrum.CtrlLabel
    Friend WithEvents lblRemark As Spectrum.CtrlLabel
    Friend WithEvents txtRemarks As System.Windows.Forms.RichTextBox
    Friend WithEvents btnAddRemark As Spectrum.CtrlBtn
    Friend WithEvents btnSave As Spectrum.CtrlBtn
    Friend WithEvents btnCancel As Spectrum.CtrlBtn
    Friend WithEvents btnClear As Spectrum.CtrlBtn
    Friend WithEvents dgGridArticle As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents txtAndroidArticleSearchTextBox As Spectrum.AndroidSearchTextBox
    Friend WithEvents C1Sizer2main As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CtrlLabel1 As Spectrum.CtrlLabel
    Friend WithEvents C1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
End Class
