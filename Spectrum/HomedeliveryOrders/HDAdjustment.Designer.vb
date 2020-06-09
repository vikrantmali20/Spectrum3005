<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HDAdjustment
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HDAdjustment))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.grdMain = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.C1Sizer2 = New C1.Win.C1Sizer.C1Sizer()
        Me.btnCancel = New Spectrum.CtrlBtn()
        Me.btnPay = New Spectrum.CtrlBtn()
        Me.C1Sizer1 = New C1.Win.C1Sizer.C1Sizer()
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        Me.btnSearch = New Spectrum.CtrlBtn()
        Me.cmbType = New Spectrum.ctrlCombo()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Sizer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer2.SuspendLayout()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer1.SuspendLayout()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 95.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.grdMain, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.C1Sizer2, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.C1Sizer1, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.22222!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.91667!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(651, 288)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'grdMain
        '
        Me.grdMain.AllowEditing = False
        Me.grdMain.ColumnInfo = "10,1,0,0,0,85,Columns:"
        Me.grdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdMain.Location = New System.Drawing.Point(35, 89)
        Me.grdMain.Name = "grdMain"
        Me.grdMain.Rows.DefaultSize = 17
        Me.grdMain.Size = New System.Drawing.Size(613, 129)
        Me.grdMain.TabIndex = 4
        Me.grdMain.Visible = False
        '
        'C1Sizer2
        '
        Me.C1Sizer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.C1Sizer2.Controls.Add(Me.btnCancel)
        Me.C1Sizer2.Controls.Add(Me.btnPay)
        Me.C1Sizer2.GridDefinition = resources.GetString("C1Sizer2.GridDefinition")
        Me.C1Sizer2.Location = New System.Drawing.Point(35, 224)
        Me.C1Sizer2.Name = "C1Sizer2"
        Me.C1Sizer2.Size = New System.Drawing.Size(613, 61)
        Me.C1Sizer2.TabIndex = 5
        Me.C1Sizer2.Text = "C1Sizer2"
        '
        'btnCancel
        '
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancel.Location = New System.Drawing.Point(278, 16)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.SetArticleCode = Nothing
        Me.btnCancel.Size = New System.Drawing.Size(127, 30)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.btnCancel.Text = getValueByKey("frmncommonsearch.cmdcancel")
        '
        'btnPay
        '
        Me.btnPay.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnPay.Location = New System.Drawing.Point(137, 16)
        Me.btnPay.Margin = New System.Windows.Forms.Padding(5)
        Me.btnPay.Name = "btnPay"
        Me.btnPay.SetArticleCode = Nothing
        Me.btnPay.Size = New System.Drawing.Size(137, 30)
        Me.btnPay.TabIndex = 0
        Me.btnPay.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnPay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnPay.UseVisualStyleBackColor = True
        Me.btnPay.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.btnPay.Text = getValueByKey("Crs016")
        '
        'C1Sizer1
        '
        Me.C1Sizer1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.C1Sizer1.Controls.Add(Me.CtrlLabel1)
        Me.C1Sizer1.Controls.Add(Me.btnSearch)
        Me.C1Sizer1.Controls.Add(Me.cmbType)
        Me.C1Sizer1.GridDefinition = "70.3703703703704:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "20.7482993197279:False:False;57.4829931972789:False" & _
    ":False;16.3265306122449:False:False;"
        Me.C1Sizer1.Location = New System.Drawing.Point(194, 29)
        Me.C1Sizer1.Margin = New System.Windows.Forms.Padding(0)
        Me.C1Sizer1.Name = "C1Sizer1"
        Me.C1Sizer1.Size = New System.Drawing.Size(294, 27)
        Me.C1Sizer1.TabIndex = 3
        Me.C1Sizer1.Text = "C1Sizer1"

        '
        'CtrlLabel1
        '
        Me.CtrlLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlLabel1.AttachedTextBoxName = Nothing
        Me.CtrlLabel1.AutoSize = True
        Me.CtrlLabel1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlLabel1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.CtrlLabel1.Location = New System.Drawing.Point(4, 4)
        Me.CtrlLabel1.Name = "CtrlLabel1"
        Me.CtrlLabel1.Size = New System.Drawing.Size(0, 15)
        Me.CtrlLabel1.TabIndex = 3
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.TextDetached = True
        Me.CtrlLabel1.Text = getValueByKey("Crs017")
        '
        'btnSearch
        '
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSearch.Location = New System.Drawing.Point(242, 4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.SetArticleCode = Nothing
        Me.btnSearch.Size = New System.Drawing.Size(48, 19)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnSearch.UseVisualStyleBackColor = True
        Me.btnSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.btnSearch.Text = getValueByKey("Crs018")
        '
        'cmbType
        '
        Me.cmbType.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbType.AutoCompletion = True
        Me.cmbType.AutoDropDown = True
        Me.cmbType.Caption = ""
        Me.cmbType.CaptionHeight = 17
        Me.cmbType.CaptionVisible = False
        Me.cmbType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbType.ColumnCaptionHeight = 17
        Me.cmbType.ColumnFooterHeight = 17
        Me.cmbType.ColumnHeaders = False
        Me.cmbType.ContentHeight = 15
        Me.cmbType.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbType.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cmbType.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbType.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbType.EditorHeight = 15
        Me.cmbType.ExtendRightColumn = True
        Me.cmbType.Images.Add(CType(resources.GetObject("cmbType.Images"), System.Drawing.Image))
        Me.cmbType.ItemHeight = 15
        Me.cmbType.Location = New System.Drawing.Point(69, 4)
        Me.cmbType.Margin = New System.Windows.Forms.Padding(5)
        Me.cmbType.MatchEntryTimeout = CType(2000, Long)
        Me.cmbType.MaxDropDownItems = CType(5, Short)
        Me.cmbType.MaxLength = 32767
        Me.cmbType.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbType.Name = "cmbType"
        Me.cmbType.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbType.Size = New System.Drawing.Size(169, 21)
        Me.cmbType.TabIndex = 1
        Me.cmbType.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbType.PropBag = resources.GetString("cmbType.PropBag")
        '
        'CreditSaleAdjustment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(651, 288)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "CreditSaleAdjustment"
        Me.Text = getValueByKey("Crs019")
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Sizer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer2.ResumeLayout(False)
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer1.ResumeLayout(False)
        Me.C1Sizer1.PerformLayout()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblType As Spectrum.CtrlLabel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmbType As Spectrum.ctrlCombo
    Friend WithEvents C1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents btnSearch As Spectrum.CtrlBtn
    Friend WithEvents grdMain As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1Sizer2 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents btnCancel As Spectrum.CtrlBtn
    Friend WithEvents btnPay As Spectrum.CtrlBtn
    Friend WithEvents CtrlLabel1 As Spectrum.CtrlLabel

End Class
