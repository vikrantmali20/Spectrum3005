<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDefineKit
    'Inherits Spectrum.CtrlRbnBaseForm
    Inherits Spectrum.CtrlPopupForm
    ' System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDefineKit))
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnSave = New Spectrum.CtrlBtn()
        Me.BtnCancel = New Spectrum.CtrlBtn()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.GrpKitDetail = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.LblStatusActiveOrInActive = New Spectrum.Controls.Label(Me.components)
        Me.LblStatus = New Spectrum.Controls.Label(Me.components)
        Me.LblMaterialType = New Spectrum.Controls.Label(Me.components)
        Me.LblKitShortName = New Spectrum.Controls.Label(Me.components)
        Me.LblSalesPrice = New Spectrum.Controls.Label(Me.components)
        Me.TxtmaterialType = New System.Windows.Forms.TextBox()
        Me.TxtKitShortName = New System.Windows.Forms.TextBox()
        Me.LblKitCode = New Spectrum.Controls.Label(Me.components)
        Me.LblItemType = New Spectrum.Controls.Label(Me.components)
        Me.TxtSalesPrice = New System.Windows.Forms.TextBox()
        Me.txtItemType = New System.Windows.Forms.TextBox()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TxtItemTree = New System.Windows.Forms.TextBox()
        Me.TxtLastNodeCode = New System.Windows.Forms.TextBox()
        Me.TxtParentItemCode = New System.Windows.Forms.TextBox()
        Me.txtFilterArticle = New Spectrum.AndroidSearchTextBox(Me.components)
        Me.lblLastNodeCode = New Spectrum.Controls.Label(Me.components)
        Me.Label11 = New Spectrum.Controls.Label(Me.components)
        Me.LblParentItemCode = New Spectrum.Controls.Label(Me.components)
        Me.LblKitName = New Spectrum.Controls.Label(Me.components)
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label14 = New Spectrum.Controls.Label(Me.components)
        Me.txtSingleItemFliter = New Spectrum.AndroidSearchTextBox(Me.components)
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.GrpShowData = New System.Windows.Forms.GroupBox()
        Me.GrdShowData = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.TableLayoutPanel10.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.GrpKitDetail.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.LblStatusActiveOrInActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblMaterialType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblKitShortName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblSalesPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblKitCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.lblLastNodeCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblParentItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LblKitName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.Label14, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel9.SuspendLayout()
        Me.GrpShowData.SuspendLayout()
        CType(Me.GrdShowData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 1
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel5, 0, 3)
        Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel7, 0, 1)
        Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel6, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.FlowLayoutPanel1, 0, 2)
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(10, 12)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 4
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.03607!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.12625!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.81763!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.02004!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(891, 585)
        Me.TableLayoutPanel10.TabIndex = 10
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 5
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.TableLayoutPanel1, 4, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(3, 528)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(885, 54)
        Me.TableLayoutPanel5.TabIndex = 7
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.BtnSave, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.BtnCancel, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(693, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(189, 48)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'BtnSave
        '
        Me.BtnSave.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnSave.Location = New System.Drawing.Point(3, 3)
        Me.BtnSave.MoveToNxtCtrl = Nothing
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.SetArticleCode = Nothing
        Me.BtnSave.SetRowIndex = 0
        Me.BtnSave.Size = New System.Drawing.Size(88, 42)
        Me.BtnSave.TabIndex = 122
        Me.BtnSave.Text = "Save"
        Me.BtnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnSave.UseVisualStyleBackColor = True
        Me.BtnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnCancel
        '
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCancel.Location = New System.Drawing.Point(97, 3)
        Me.BtnCancel.MoveToNxtCtrl = Nothing
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.SetArticleCode = Nothing
        Me.BtnCancel.SetRowIndex = 0
        Me.BtnCancel.Size = New System.Drawing.Size(89, 42)
        Me.BtnCancel.TabIndex = 123
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnCancel.UseVisualStyleBackColor = True
        Me.BtnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 1
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.GrpKitDetail, 0, 0)
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(3, 108)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 1
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(882, 128)
        Me.TableLayoutPanel7.TabIndex = 9
        '
        'GrpKitDetail
        '
        Me.GrpKitDetail.Controls.Add(Me.TableLayoutPanel4)
        Me.GrpKitDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpKitDetail.Location = New System.Drawing.Point(3, 3)
        Me.GrpKitDetail.Name = "GrpKitDetail"
        Me.GrpKitDetail.Size = New System.Drawing.Size(876, 122)
        Me.GrpKitDetail.TabIndex = 118
        Me.GrpKitDetail.TabStop = False
        Me.GrpKitDetail.Text = "Kit Details"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 4
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 152.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 310.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 442.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.LblStatusActiveOrInActive, 3, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.LblStatus, 2, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.LblMaterialType, 2, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.LblKitShortName, 2, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.LblSalesPrice, 0, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.TxtmaterialType, 3, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.TxtKitShortName, 3, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.LblKitCode, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.LblItemType, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.TxtSalesPrice, 1, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.txtItemType, 1, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.txtItemCode, 1, 0)
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 17)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 3
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(870, 102)
        Me.TableLayoutPanel4.TabIndex = 3
        '
        'LblStatusActiveOrInActive
        '
        Me.LblStatusActiveOrInActive.AutoSize = True
        Me.LblStatusActiveOrInActive.BackColor = System.Drawing.Color.Transparent
        Me.LblStatusActiveOrInActive.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LblStatusActiveOrInActive.ForeColor = System.Drawing.Color.Black
        Me.LblStatusActiveOrInActive.Location = New System.Drawing.Point(613, 66)
        Me.LblStatusActiveOrInActive.Name = "LblStatusActiveOrInActive"
        Me.LblStatusActiveOrInActive.Size = New System.Drawing.Size(135, 16)
        Me.LblStatusActiveOrInActive.TabIndex = 136
        Me.LblStatusActiveOrInActive.Tag = Nothing
        Me.LblStatusActiveOrInActive.Text = "Active Or In Active"
        Me.LblStatusActiveOrInActive.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblStatusActiveOrInActive.TextDetached = True
        Me.LblStatusActiveOrInActive.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'LblStatus
        '
        Me.LblStatus.AutoSize = True
        Me.LblStatus.BackColor = System.Drawing.Color.Transparent
        Me.LblStatus.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LblStatus.ForeColor = System.Drawing.Color.Black
        Me.LblStatus.Location = New System.Drawing.Point(465, 66)
        Me.LblStatus.Name = "LblStatus"
        Me.LblStatus.Size = New System.Drawing.Size(58, 16)
        Me.LblStatus.TabIndex = 139
        Me.LblStatus.Tag = Nothing
        Me.LblStatus.Text = "Status:"
        Me.LblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblStatus.TextDetached = True
        Me.LblStatus.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'LblMaterialType
        '
        Me.LblMaterialType.AutoSize = True
        Me.LblMaterialType.BackColor = System.Drawing.Color.Transparent
        Me.LblMaterialType.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LblMaterialType.ForeColor = System.Drawing.Color.Black
        Me.LblMaterialType.Location = New System.Drawing.Point(465, 32)
        Me.LblMaterialType.Name = "LblMaterialType"
        Me.LblMaterialType.Size = New System.Drawing.Size(103, 16)
        Me.LblMaterialType.TabIndex = 135
        Me.LblMaterialType.Tag = Nothing
        Me.LblMaterialType.Text = "Material Type:"
        Me.LblMaterialType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblMaterialType.TextDetached = True
        Me.LblMaterialType.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'LblKitShortName
        '
        Me.LblKitShortName.AutoSize = True
        Me.LblKitShortName.BackColor = System.Drawing.Color.Transparent
        Me.LblKitShortName.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LblKitShortName.ForeColor = System.Drawing.Color.Black
        Me.LblKitShortName.Location = New System.Drawing.Point(465, 0)
        Me.LblKitShortName.Name = "LblKitShortName"
        Me.LblKitShortName.Size = New System.Drawing.Size(113, 16)
        Me.LblKitShortName.TabIndex = 140
        Me.LblKitShortName.Tag = Nothing
        Me.LblKitShortName.Text = "Kit Short Name:"
        Me.LblKitShortName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblKitShortName.TextDetached = True
        Me.LblKitShortName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'LblSalesPrice
        '
        Me.LblSalesPrice.AutoSize = True
        Me.LblSalesPrice.BackColor = System.Drawing.Color.Transparent
        Me.LblSalesPrice.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LblSalesPrice.ForeColor = System.Drawing.Color.Black
        Me.LblSalesPrice.Location = New System.Drawing.Point(3, 66)
        Me.LblSalesPrice.Name = "LblSalesPrice"
        Me.LblSalesPrice.Size = New System.Drawing.Size(86, 16)
        Me.LblSalesPrice.TabIndex = 140
        Me.LblSalesPrice.Tag = Nothing
        Me.LblSalesPrice.Text = "Sales Price:"
        Me.LblSalesPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblSalesPrice.TextDetached = True
        Me.LblSalesPrice.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'TxtmaterialType
        '
        Me.TxtmaterialType.Location = New System.Drawing.Point(613, 35)
        Me.TxtmaterialType.Name = "TxtmaterialType"
        Me.TxtmaterialType.Size = New System.Drawing.Size(241, 21)
        Me.TxtmaterialType.TabIndex = 17
        '
        'TxtKitShortName
        '
        Me.TxtKitShortName.Location = New System.Drawing.Point(613, 3)
        Me.TxtKitShortName.Name = "TxtKitShortName"
        Me.TxtKitShortName.Size = New System.Drawing.Size(241, 21)
        Me.TxtKitShortName.TabIndex = 38
        '
        'LblKitCode
        '
        Me.LblKitCode.AutoSize = True
        Me.LblKitCode.BackColor = System.Drawing.Color.Transparent
        Me.LblKitCode.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LblKitCode.ForeColor = System.Drawing.Color.Black
        Me.LblKitCode.Location = New System.Drawing.Point(3, 0)
        Me.LblKitCode.Name = "LblKitCode"
        Me.LblKitCode.Size = New System.Drawing.Size(69, 16)
        Me.LblKitCode.TabIndex = 135
        Me.LblKitCode.Tag = Nothing
        Me.LblKitCode.Text = "Kit Code:"
        Me.LblKitCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblKitCode.TextDetached = True
        Me.LblKitCode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'LblItemType
        '
        Me.LblItemType.AutoSize = True
        Me.LblItemType.BackColor = System.Drawing.Color.Transparent
        Me.LblItemType.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LblItemType.ForeColor = System.Drawing.Color.Black
        Me.LblItemType.Location = New System.Drawing.Point(3, 32)
        Me.LblItemType.Name = "LblItemType"
        Me.LblItemType.Size = New System.Drawing.Size(81, 16)
        Me.LblItemType.TabIndex = 139
        Me.LblItemType.Tag = Nothing
        Me.LblItemType.Text = "Item Type:"
        Me.LblItemType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblItemType.TextDetached = True
        Me.LblItemType.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'TxtSalesPrice
        '
        Me.TxtSalesPrice.Location = New System.Drawing.Point(155, 69)
        Me.TxtSalesPrice.Name = "TxtSalesPrice"
        Me.TxtSalesPrice.Size = New System.Drawing.Size(241, 21)
        Me.TxtSalesPrice.TabIndex = 18
        '
        'txtItemType
        '
        Me.txtItemType.Location = New System.Drawing.Point(155, 35)
        Me.txtItemType.Name = "txtItemType"
        Me.txtItemType.Size = New System.Drawing.Size(241, 21)
        Me.txtItemType.TabIndex = 18
        '
        'txtItemCode
        '
        Me.txtItemCode.Location = New System.Drawing.Point(155, 3)
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.Size = New System.Drawing.Size(241, 21)
        Me.txtItemCode.TabIndex = 38
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 1
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.GroupBox2, 0, 0)
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(882, 99)
        Me.TableLayoutPanel6.TabIndex = 8
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TableLayoutPanel2)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(876, 93)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 4
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 151.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 256.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TxtItemTree, 3, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.TxtLastNodeCode, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TxtParentItemCode, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.txtFilterArticle, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblLastNodeCode, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label11, 2, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.LblParentItemCode, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.LblKitName, 0, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 17)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(870, 73)
        Me.TableLayoutPanel2.TabIndex = 3
        '
        'TxtItemTree
        '
        Me.TxtItemTree.Location = New System.Drawing.Point(617, 35)
        Me.TxtItemTree.Name = "TxtItemTree"
        Me.TxtItemTree.Size = New System.Drawing.Size(242, 21)
        Me.TxtItemTree.TabIndex = 13
        '
        'TxtLastNodeCode
        '
        Me.TxtLastNodeCode.Location = New System.Drawing.Point(617, 3)
        Me.TxtLastNodeCode.Name = "TxtLastNodeCode"
        Me.TxtLastNodeCode.Size = New System.Drawing.Size(242, 21)
        Me.TxtLastNodeCode.TabIndex = 11
        '
        'TxtParentItemCode
        '
        Me.TxtParentItemCode.Location = New System.Drawing.Point(154, 35)
        Me.TxtParentItemCode.Name = "TxtParentItemCode"
        Me.TxtParentItemCode.Size = New System.Drawing.Size(241, 21)
        Me.TxtParentItemCode.TabIndex = 12
        '
        'txtFilterArticle
        '
        Me.txtFilterArticle.AllowUpdateListBox = True
        Me.txtFilterArticle.DtSearchData = Nothing
        Me.txtFilterArticle.IsCallFromPosTab = False
        Me.txtFilterArticle.IsItemSelected = False
        Me.txtFilterArticle.IsListBind = True
        Me.txtFilterArticle.IsMouseOverList = False
        Me.txtFilterArticle.IsMovingControl = False
        Me.txtFilterArticle.IsSetLocation = False
        Me.txtFilterArticle.ListBoxXCoordinate = 0
        Me.txtFilterArticle.ListBoxYCoordinate = 0
        Me.txtFilterArticle.Location = New System.Drawing.Point(154, 3)
        Me.txtFilterArticle.lstNames = CType(resources.GetObject("txtFilterArticle.lstNames"), System.Collections.Generic.List(Of String))
        Me.txtFilterArticle.MaxLength = 35
        Me.txtFilterArticle.Name = "txtFilterArticle"
        Me.txtFilterArticle.SearchBasedOnDB = Nothing
        Me.txtFilterArticle.SearchQueryOnDB = Nothing
        Me.txtFilterArticle.Size = New System.Drawing.Size(241, 21)
        Me.txtFilterArticle.TabIndex = 10
        '
        'lblLastNodeCode
        '
        Me.lblLastNodeCode.AutoSize = True
        Me.lblLastNodeCode.BackColor = System.Drawing.Color.Transparent
        Me.lblLastNodeCode.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblLastNodeCode.ForeColor = System.Drawing.Color.Black
        Me.lblLastNodeCode.Location = New System.Drawing.Point(467, 0)
        Me.lblLastNodeCode.Name = "lblLastNodeCode"
        Me.lblLastNodeCode.Size = New System.Drawing.Size(118, 16)
        Me.lblLastNodeCode.TabIndex = 133
        Me.lblLastNodeCode.Tag = Nothing
        Me.lblLastNodeCode.Text = "Last Node Code:"
        Me.lblLastNodeCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLastNodeCode.TextDetached = True
        Me.lblLastNodeCode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(467, 32)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(78, 16)
        Me.Label11.TabIndex = 134
        Me.Label11.Tag = Nothing
        Me.Label11.Text = "Item Tree:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label11.TextDetached = True
        Me.Label11.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'LblParentItemCode
        '
        Me.LblParentItemCode.AutoSize = True
        Me.LblParentItemCode.BackColor = System.Drawing.Color.Transparent
        Me.LblParentItemCode.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LblParentItemCode.ForeColor = System.Drawing.Color.Black
        Me.LblParentItemCode.Location = New System.Drawing.Point(3, 32)
        Me.LblParentItemCode.Name = "LblParentItemCode"
        Me.LblParentItemCode.Size = New System.Drawing.Size(130, 16)
        Me.LblParentItemCode.TabIndex = 132
        Me.LblParentItemCode.Tag = Nothing
        Me.LblParentItemCode.Text = "Parent Item Code:"
        Me.LblParentItemCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblParentItemCode.TextDetached = True
        Me.LblParentItemCode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'LblKitName
        '
        Me.LblKitName.AutoSize = True
        Me.LblKitName.BackColor = System.Drawing.Color.Transparent
        Me.LblKitName.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LblKitName.ForeColor = System.Drawing.Color.Black
        Me.LblKitName.Location = New System.Drawing.Point(3, 0)
        Me.LblKitName.Name = "LblKitName"
        Me.LblKitName.Size = New System.Drawing.Size(72, 16)
        Me.LblKitName.TabIndex = 131
        Me.LblKitName.Tag = Nothing
        Me.LblKitName.Text = "Kit Name:"
        Me.LblKitName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblKitName.TextDetached = True
        Me.LblKitName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.TableLayoutPanel8)
        Me.FlowLayoutPanel1.Controls.Add(Me.TableLayoutPanel9)
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 243)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(885, 279)
        Me.FlowLayoutPanel1.TabIndex = 10
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 1
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.GroupBox4, 0, 0)
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(451, 72)
        Me.TableLayoutPanel8.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.TableLayoutPanel3)
        Me.GroupBox4.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(435, 60)
        Me.GroupBox4.TabIndex = 10
        Me.GroupBox4.TabStop = False
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 144.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 714.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label14, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.txtSingleItemFliter, 1, 0)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(6, 14)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(417, 40)
        Me.TableLayoutPanel3.TabIndex = 3
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(3, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(81, 16)
        Me.Label14.TabIndex = 136
        Me.Label14.Tag = Nothing
        Me.Label14.Text = "Add Items:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label14.TextDetached = True
        Me.Label14.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
        '
        'txtSingleItemFliter
        '
        Me.txtSingleItemFliter.AllowUpdateListBox = True
        Me.txtSingleItemFliter.DtSearchData = Nothing
        Me.txtSingleItemFliter.IsCallFromPosTab = False
        Me.txtSingleItemFliter.IsItemSelected = False
        Me.txtSingleItemFliter.IsListBind = True
        Me.txtSingleItemFliter.IsMouseOverList = False
        Me.txtSingleItemFliter.IsMovingControl = False
        Me.txtSingleItemFliter.IsSetLocation = False
        Me.txtSingleItemFliter.ListBoxXCoordinate = 0
        Me.txtSingleItemFliter.ListBoxYCoordinate = 0
        Me.txtSingleItemFliter.Location = New System.Drawing.Point(147, 3)
        Me.txtSingleItemFliter.lstNames = CType(resources.GetObject("txtSingleItemFliter.lstNames"), System.Collections.Generic.List(Of String))
        Me.txtSingleItemFliter.MaxLength = 35
        Me.txtSingleItemFliter.Name = "txtSingleItemFliter"
        Me.txtSingleItemFliter.SearchBasedOnDB = Nothing
        Me.txtSingleItemFliter.SearchQueryOnDB = Nothing
        Me.txtSingleItemFliter.Size = New System.Drawing.Size(244, 21)
        Me.txtSingleItemFliter.TabIndex = 14
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.AutoSize = True
        Me.TableLayoutPanel9.ColumnCount = 1
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel9.Controls.Add(Me.GrpShowData, 0, 0)
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(3, 81)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 1
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(879, 192)
        Me.TableLayoutPanel9.TabIndex = 1
        '
        'GrpShowData
        '
        Me.GrpShowData.Controls.Add(Me.GrdShowData)
        Me.GrpShowData.Location = New System.Drawing.Point(3, 3)
        Me.GrpShowData.Name = "GrpShowData"
        Me.GrpShowData.Size = New System.Drawing.Size(873, 186)
        Me.GrpShowData.TabIndex = 124
        Me.GrpShowData.TabStop = False
        Me.GrpShowData.Visible = False
        '
        'GrdShowData
        '
        Me.GrdShowData.AutoGenerateColumns = False
        Me.GrdShowData.CellButtonImage = Global.Spectrum.My.Resources.Resources.del_icon
        Me.GrdShowData.ColumnInfo = resources.GetString("GrdShowData.ColumnInfo")
        Me.GrdShowData.ExtendLastCol = True
        Me.GrdShowData.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrdShowData.Location = New System.Drawing.Point(6, 20)
        Me.GrdShowData.Name = "GrdShowData"
        Me.GrdShowData.NewRowWatermark = ""
        Me.GrdShowData.Rows.Count = 1
        Me.GrdShowData.Rows.DefaultSize = 19
        Me.GrdShowData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.GrdShowData.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.GrdShowData.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.GrdShowData.Size = New System.Drawing.Size(858, 156)
        Me.GrdShowData.StyleInfo = resources.GetString("GrdShowData.StyleInfo")
        Me.GrdShowData.TabIndex = 116
        Me.GrdShowData.Tag = ""
        '
        'FrmDefineKit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(905, 602)
        Me.Controls.Add(Me.TableLayoutPanel10)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDefineKit"
        Me.Text = "Define Kit"
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.GrpKitDetail.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.LblStatusActiveOrInActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblMaterialType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblKitShortName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblSalesPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblKitCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblItemType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.lblLastNodeCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblParentItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LblKitName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.Label14, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel9.ResumeLayout(False)
        Me.GrpShowData.ResumeLayout(False)
        CType(Me.GrdShowData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtFilterArticle As Spectrum.AndroidSearchTextBox
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GrpKitDetail As System.Windows.Forms.GroupBox
    Friend WithEvents TxtSalesPrice As System.Windows.Forms.TextBox
    Friend WithEvents TxtLastNodeCode As System.Windows.Forms.TextBox
    Friend WithEvents TxtItemTree As System.Windows.Forms.TextBox
    Friend WithEvents txtItemCode As System.Windows.Forms.TextBox
    Friend WithEvents BtnCancel As Spectrum.CtrlBtn
    Friend WithEvents BtnSave As Spectrum.CtrlBtn
    Friend WithEvents GrdShowData As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents GrpShowData As System.Windows.Forms.GroupBox
    Friend WithEvents LblParentItemCode As Spectrum.Controls.Label
    Friend WithEvents lblLastNodeCode As Spectrum.Controls.Label
    Friend WithEvents Label11 As Spectrum.Controls.Label
    Friend WithEvents LblKitName As Spectrum.Controls.Label
    Friend WithEvents LblKitCode As Spectrum.Controls.Label
    Friend WithEvents LblItemType As Spectrum.Controls.Label
    Friend WithEvents LblSalesPrice As Spectrum.Controls.Label
    Friend WithEvents LblKitShortName As Spectrum.Controls.Label
    Friend WithEvents TxtKitShortName As System.Windows.Forms.TextBox
    Friend WithEvents TxtmaterialType As System.Windows.Forms.TextBox
    Friend WithEvents txtItemType As System.Windows.Forms.TextBox
    Friend WithEvents LblMaterialType As Spectrum.Controls.Label
    Friend WithEvents LblStatus As Spectrum.Controls.Label
    Friend WithEvents TxtParentItemCode As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label14 As Spectrum.Controls.Label
    Friend WithEvents txtSingleItemFliter As Spectrum.AndroidSearchTextBox
    Friend WithEvents LblStatusActiveOrInActive As Spectrum.Controls.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel6 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel10 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents TableLayoutPanel8 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel9 As System.Windows.Forms.TableLayoutPanel
End Class
