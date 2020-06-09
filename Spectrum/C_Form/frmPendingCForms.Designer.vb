<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPendingCForms
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPendingCForms))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.CtrlBtnCFormNumber = New Spectrum.CtrlBtn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblState = New System.Windows.Forms.Label()
        Me.CtrlComboState = New Spectrum.ctrlCombo()
        Me.dtpFromDate = New Spectrum.ctrlDate()
        Me.dtpToDate = New Spectrum.ctrlDate()
        Me.txtSearchCustomer = New Spectrum.ctrlCombo()
        Me.CtrlBtnSearch = New Spectrum.CtrlBtn()
        Me.lblCustomerName = New System.Windows.Forms.Label()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.CtrlBtnCformDelete = New Spectrum.CtrlBtn()
        Me.DgPendingCForm = New Spectrum.Controls.FlexGrid(Me.components)
        Me.CtrlBtnAddRemark = New Spectrum.CtrlBtn()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.CtrlComboState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSearchCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgPendingCForm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 494.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 155.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 156.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 156.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlBtnCFormNumber, 4, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlBtnCformDelete, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.DgPendingCForm, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CtrlBtnAddRemark, 2, 2)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(-2, 5)
        Me.TableLayoutPanel1.MinimumSize = New System.Drawing.Size(888, 454)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.60636!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.39364!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(991, 454)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'CtrlBtnCFormNumber
        '
        Me.CtrlBtnCFormNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlBtnCFormNumber.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CtrlBtnCFormNumber.Location = New System.Drawing.Point(820, 419)
        Me.CtrlBtnCFormNumber.Name = "CtrlBtnCFormNumber"
        Me.CtrlBtnCFormNumber.SetArticleCode = Nothing
        Me.CtrlBtnCFormNumber.SetRowIndex = 0
        Me.CtrlBtnCFormNumber.Size = New System.Drawing.Size(150, 32)
        Me.CtrlBtnCFormNumber.TabIndex = 0
        Me.CtrlBtnCFormNumber.Text = "Add C-form Number"
        Me.CtrlBtnCFormNumber.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CtrlBtnCFormNumber.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CtrlBtnCFormNumber.UseVisualStyleBackColor = True
        Me.CtrlBtnCFormNumber.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'Panel1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.Panel1, 5)
        Me.Panel1.Controls.Add(Me.lblState)
        Me.Panel1.Controls.Add(Me.CtrlComboState)
        Me.Panel1.Controls.Add(Me.dtpFromDate)
        Me.Panel1.Controls.Add(Me.dtpToDate)
        Me.Panel1.Controls.Add(Me.txtSearchCustomer)
        Me.Panel1.Controls.Add(Me.CtrlBtnSearch)
        Me.Panel1.Controls.Add(Me.lblCustomerName)
        Me.Panel1.Controls.Add(Me.lblToDate)
        Me.Panel1.Controls.Add(Me.lblFromDate)
        Me.Panel1.Location = New System.Drawing.Point(15, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(783, 113)
        Me.Panel1.TabIndex = 5
        '
        'lblState
        '
        Me.lblState.AutoSize = True
        Me.lblState.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblState.Location = New System.Drawing.Point(380, 57)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(45, 15)
        Me.lblState.TabIndex = 18
        Me.lblState.Text = "State :-"
        '
        'CtrlComboState
        '
        Me.CtrlComboState.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.CtrlComboState.AutoCompletion = True
        Me.CtrlComboState.AutoDropDown = True
        Me.CtrlComboState.Caption = ""
        Me.CtrlComboState.CaptionHeight = 17
        Me.CtrlComboState.CaptionVisible = False
        Me.CtrlComboState.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CtrlComboState.ColumnCaptionHeight = 17
        Me.CtrlComboState.ColumnFooterHeight = 17
        Me.CtrlComboState.ColumnHeaders = False
        Me.CtrlComboState.ContentHeight = 15
        Me.CtrlComboState.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.CtrlComboState.EditorBackColor = System.Drawing.SystemColors.Window
        Me.CtrlComboState.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlComboState.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.CtrlComboState.EditorHeight = 15
        Me.CtrlComboState.Images.Add(CType(resources.GetObject("CtrlComboState.Images"), System.Drawing.Image))
        Me.CtrlComboState.ItemHeight = 15
        Me.CtrlComboState.Location = New System.Drawing.Point(458, 55)
        Me.CtrlComboState.MatchCol = C1.Win.C1List.MatchColEnum.AllCols
        Me.CtrlComboState.MatchEntry = C1.Win.C1List.MatchEntryEnum.Extended
        Me.CtrlComboState.MatchEntryTimeout = CType(2000, Long)
        Me.CtrlComboState.MaxDropDownItems = CType(5, Short)
        Me.CtrlComboState.MaxLength = 32767
        Me.CtrlComboState.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.CtrlComboState.Name = "CtrlComboState"
        Me.CtrlComboState.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.CtrlComboState.Size = New System.Drawing.Size(202, 21)
        Me.CtrlComboState.TabIndex = 17
        Me.CtrlComboState.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.CtrlComboState.PropBag = resources.GetString("CtrlComboState.PropBag")
        '
        'dtpFromDate
        '
        Me.dtpFromDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.dtpFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtpFromDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage"), System.Drawing.Image)
        '
        '
        '
        Me.dtpFromDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtpFromDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtpFromDate.DisplayFormat.EmptyAsNull = False
        Me.dtpFromDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpFromDate.DisplayFormat.Inherit = CType(((C1.Win.C1Input.FormatInfoInheritFlags.NullText Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpFromDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpFromDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpFromDate.EmptyAsNull = True
        Me.dtpFromDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpFromDate.InterceptArrowKeys = False
        Me.dtpFromDate.Location = New System.Drawing.Point(133, 20)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(154, 19)
        Me.dtpFromDate.TabIndex = 16
        Me.dtpFromDate.Tag = Nothing
        Me.dtpFromDate.TrimStart = True
        Me.dtpFromDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        Me.dtpFromDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtpFromDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'dtpToDate
        '
        Me.dtpToDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.dtpToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dtpToDate.ButtonImages.DropImage = CType(resources.GetObject("resource.DropImage1"), System.Drawing.Image)
        '
        '
        '
        Me.dtpToDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dtpToDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtpToDate.DisplayFormat.EmptyAsNull = False
        Me.dtpToDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpToDate.DisplayFormat.Inherit = CType(((C1.Win.C1Input.FormatInfoInheritFlags.NullText Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpToDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpToDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.dtpToDate.EmptyAsNull = True
        Me.dtpToDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.dtpToDate.InterceptArrowKeys = False
        Me.dtpToDate.Location = New System.Drawing.Point(458, 16)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(154, 19)
        Me.dtpToDate.TabIndex = 15
        Me.dtpToDate.Tag = Nothing
        Me.dtpToDate.TrimStart = True
        Me.dtpToDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        Me.dtpToDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        Me.dtpToDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black
        '
        'txtSearchCustomer
        '
        Me.txtSearchCustomer.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.txtSearchCustomer.AutoCompletion = True
        Me.txtSearchCustomer.AutoDropDown = True
        Me.txtSearchCustomer.Caption = ""
        Me.txtSearchCustomer.CaptionHeight = 17
        Me.txtSearchCustomer.CaptionVisible = False
        Me.txtSearchCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtSearchCustomer.ColumnCaptionHeight = 17
        Me.txtSearchCustomer.ColumnFooterHeight = 17
        Me.txtSearchCustomer.ColumnHeaders = False
        Me.txtSearchCustomer.ContentHeight = 15
        Me.txtSearchCustomer.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.txtSearchCustomer.EditorBackColor = System.Drawing.SystemColors.Window
        Me.txtSearchCustomer.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchCustomer.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSearchCustomer.EditorHeight = 15
        Me.txtSearchCustomer.Images.Add(CType(resources.GetObject("txtSearchCustomer.Images"), System.Drawing.Image))
        Me.txtSearchCustomer.ItemHeight = 15
        Me.txtSearchCustomer.Location = New System.Drawing.Point(133, 53)
        Me.txtSearchCustomer.MatchCol = C1.Win.C1List.MatchColEnum.AllCols
        Me.txtSearchCustomer.MatchEntry = C1.Win.C1List.MatchEntryEnum.Extended
        Me.txtSearchCustomer.MatchEntryTimeout = CType(2000, Long)
        Me.txtSearchCustomer.MaxDropDownItems = CType(5, Short)
        Me.txtSearchCustomer.MaxLength = 32767
        Me.txtSearchCustomer.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.txtSearchCustomer.Name = "txtSearchCustomer"
        Me.txtSearchCustomer.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.txtSearchCustomer.Size = New System.Drawing.Size(219, 21)
        Me.txtSearchCustomer.TabIndex = 4
        Me.txtSearchCustomer.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.txtSearchCustomer.PropBag = resources.GetString("txtSearchCustomer.PropBag")
        '
        'CtrlBtnSearch
        '
        Me.CtrlBtnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlBtnSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CtrlBtnSearch.Location = New System.Drawing.Point(667, 53)
        Me.CtrlBtnSearch.Name = "CtrlBtnSearch"
        Me.CtrlBtnSearch.SetArticleCode = Nothing
        Me.CtrlBtnSearch.SetRowIndex = 0
        Me.CtrlBtnSearch.Size = New System.Drawing.Size(87, 23)
        Me.CtrlBtnSearch.TabIndex = 3
        Me.CtrlBtnSearch.Text = "Search"
        Me.CtrlBtnSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CtrlBtnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CtrlBtnSearch.UseVisualStyleBackColor = True
        Me.CtrlBtnSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = True
        Me.lblCustomerName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerName.Location = New System.Drawing.Point(20, 57)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(107, 15)
        Me.lblCustomerName.TabIndex = 2
        Me.lblCustomerName.Text = "Customer Name :-"
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(380, 16)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(60, 15)
        Me.lblToDate.TabIndex = 1
        Me.lblToDate.Text = "To Date :-"
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.Location = New System.Drawing.Point(20, 20)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(75, 15)
        Me.lblFromDate.TabIndex = 0
        Me.lblFromDate.Text = "From Date :-"
        '
        'CtrlBtnCformDelete
        '
        Me.CtrlBtnCformDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlBtnCformDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CtrlBtnCformDelete.Location = New System.Drawing.Point(664, 419)
        Me.CtrlBtnCformDelete.Name = "CtrlBtnCformDelete"
        Me.CtrlBtnCformDelete.SetArticleCode = Nothing
        Me.CtrlBtnCformDelete.SetRowIndex = 0
        Me.CtrlBtnCformDelete.Size = New System.Drawing.Size(150, 32)
        Me.CtrlBtnCformDelete.TabIndex = 6
        Me.CtrlBtnCformDelete.Text = "Delete"
        Me.CtrlBtnCformDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CtrlBtnCformDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CtrlBtnCformDelete.UseVisualStyleBackColor = True
        Me.CtrlBtnCformDelete.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'DgPendingCForm
        '
        Me.DgPendingCForm.AutoResize = True
        Me.DgPendingCForm.ColumnInfo = "7,0,0,0,0,110,Columns:0{Width:65;Style:""TextAlign:GeneralCenter;ImageAlign:Center" & _
    "Center;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.TableLayoutPanel1.SetColumnSpan(Me.DgPendingCForm, 4)
        Me.DgPendingCForm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgPendingCForm.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgPendingCForm.Location = New System.Drawing.Point(15, 122)
        Me.DgPendingCForm.Name = "DgPendingCForm"
        Me.DgPendingCForm.Rows.Count = 1
        Me.DgPendingCForm.Rows.DefaultSize = 22
        Me.DgPendingCForm.Size = New System.Drawing.Size(955, 291)
        Me.DgPendingCForm.StyleInfo = resources.GetString("DgPendingCForm.StyleInfo")
        Me.DgPendingCForm.TabIndex = 7
        Me.DgPendingCForm.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'CtrlBtnAddRemark
        '
        Me.CtrlBtnAddRemark.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlBtnAddRemark.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CtrlBtnAddRemark.Location = New System.Drawing.Point(509, 419)
        Me.CtrlBtnAddRemark.Name = "CtrlBtnAddRemark"
        Me.CtrlBtnAddRemark.SetArticleCode = Nothing
        Me.CtrlBtnAddRemark.SetRowIndex = 0
        Me.CtrlBtnAddRemark.Size = New System.Drawing.Size(149, 32)
        Me.CtrlBtnAddRemark.TabIndex = 8
        Me.CtrlBtnAddRemark.Text = "Add Remark"
        Me.CtrlBtnAddRemark.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CtrlBtnAddRemark.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CtrlBtnAddRemark.UseVisualStyleBackColor = True
        Me.CtrlBtnAddRemark.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmPendingCForms
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(991, 461)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPendingCForms"
        Me.Text = "Pending C-Forms"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.CtrlComboState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSearchCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgPendingCForm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CtrlBtnCFormNumber As Spectrum.CtrlBtn
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CtrlBtnSearch As Spectrum.CtrlBtn
    Friend WithEvents lblCustomerName As System.Windows.Forms.Label
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents txtSearchCustomer As Spectrum.ctrlCombo
    Friend WithEvents dtpFromDate As Spectrum.ctrlDate
    Friend WithEvents dtpToDate As Spectrum.ctrlDate
    Friend WithEvents CtrlBtnCformDelete As Spectrum.CtrlBtn
    Friend WithEvents DgPendingCForm As Spectrum.Controls.FlexGrid
    Friend WithEvents lblState As System.Windows.Forms.Label
    Friend WithEvents CtrlComboState As Spectrum.ctrlCombo
    Friend WithEvents CtrlBtnAddRemark As Spectrum.CtrlBtn
End Class
