<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrlSalesPerson
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrlSalesPerson))
        Me.C1Sizer1 = New C1.Win.C1Sizer.C1Sizer()
        Me.AndroidSearchTextBox = New Spectrum.AndroidSearchTextBox(Me.components)
        Me.CtrlSalesPersons = New C1.Win.C1List.C1Combo()
        Me.CtrlCmdSearch = New Spectrum.CtrlBtn()
        Me.CtrlTxtBox = New Spectrum.CtrlTextBox()
        Me.CtrllabelSalesPerson = New Spectrum.CtrlLabel()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer1.SuspendLayout()
        CType(Me.CtrlSalesPersons, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlTxtBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrllabelSalesPerson, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'C1Sizer1
        '
        Me.C1Sizer1.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1Sizer1.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1Sizer1.Controls.Add(Me.AndroidSearchTextBox)
        Me.C1Sizer1.Controls.Add(Me.CtrlSalesPersons)
        Me.C1Sizer1.Controls.Add(Me.CtrlCmdSearch)
        Me.C1Sizer1.Controls.Add(Me.CtrlTxtBox)
        Me.C1Sizer1.Controls.Add(Me.CtrllabelSalesPerson)
        Me.C1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Sizer1.GridDefinition = "83.3333333333333:False:True;" & Global.Microsoft.VisualBasic.ChrW(9) & "20.1680672268908:False:True;29.7478991596639:False:F" & _
    "alse;31.2605042016807:False:False;16.8067226890756:False:True;"
        Me.C1Sizer1.Location = New System.Drawing.Point(0, 0)
        Me.C1Sizer1.Margin = New System.Windows.Forms.Padding(0)
        Me.C1Sizer1.Name = "C1Sizer1"
        Me.C1Sizer1.Padding = New System.Windows.Forms.Padding(0)
        Me.C1Sizer1.Size = New System.Drawing.Size(595, 24)
        Me.C1Sizer1.TabIndex = 0
        Me.C1Sizer1.TabStop = False
        Me.C1Sizer1.Text = "C1Sizer1"
        '
        'AndroidSearchTextBox
        '
        Me.AndroidSearchTextBox.AllowUpdateListBox = True
        Me.AndroidSearchTextBox.DtSearchData = Nothing
        Me.AndroidSearchTextBox.IsItemSelected = False
        Me.AndroidSearchTextBox.IsListBind = True
        Me.AndroidSearchTextBox.IsMouseOverList = False
        Me.AndroidSearchTextBox.IsMovingControl = False
        Me.AndroidSearchTextBox.Location = New System.Drawing.Point(305, 0)
        Me.AndroidSearchTextBox.lstNames = CType(resources.GetObject("AndroidSearchTextBox.lstNames"), System.Collections.Generic.List(Of String))
        Me.AndroidSearchTextBox.MaxLength = 35
        Me.AndroidSearchTextBox.Name = "AndroidSearchTextBox"
        Me.AndroidSearchTextBox.SearchBasedOnDB = Nothing
        Me.AndroidSearchTextBox.SearchQueryOnDB = Nothing
        Me.AndroidSearchTextBox.Size = New System.Drawing.Size(186, 20)
        Me.AndroidSearchTextBox.TabIndex = 4
        '
        'CtrlSalesPersons
        '
        Me.CtrlSalesPersons.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.CtrlSalesPersons.Caption = "Sales Person"
        Me.CtrlSalesPersons.CaptionHeight = 17
        Me.CtrlSalesPersons.CaptionVisible = False
        Me.CtrlSalesPersons.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CtrlSalesPersons.ColumnCaptionHeight = 17
        Me.CtrlSalesPersons.ColumnFooterHeight = 17
        Me.CtrlSalesPersons.ColumnHeaders = False
        Me.CtrlSalesPersons.ContentHeight = 15
        Me.CtrlSalesPersons.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.CtrlSalesPersons.EditorBackColor = System.Drawing.SystemColors.Window
        Me.CtrlSalesPersons.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlSalesPersons.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.CtrlSalesPersons.EditorHeight = 15
        Me.CtrlSalesPersons.FlatStyle = C1.Win.C1List.FlatModeEnum.Popup
        Me.CtrlSalesPersons.Images.Add(CType(resources.GetObject("CtrlSalesPersons.Images"), System.Drawing.Image))
        Me.CtrlSalesPersons.ItemHeight = 15
        Me.CtrlSalesPersons.Location = New System.Drawing.Point(124, 0)
        Me.CtrlSalesPersons.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrlSalesPersons.MatchEntryTimeout = CType(2000, Long)
        Me.CtrlSalesPersons.MaxDropDownItems = CType(5, Short)
        Me.CtrlSalesPersons.MaxLength = 32767
        Me.CtrlSalesPersons.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.CtrlSalesPersons.Name = "CtrlSalesPersons"
        Me.CtrlSalesPersons.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.CtrlSalesPersons.Size = New System.Drawing.Size(177, 21)
        Me.CtrlSalesPersons.TabIndex = 0
        Me.CtrlSalesPersons.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.CtrlSalesPersons.PropBag = resources.GetString("CtrlSalesPersons.PropBag")
        '
        'CtrlCmdSearch
        '
        Me.CtrlCmdSearch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlCmdSearch.Image = Global.Spectrum.My.Resources.Resources.search_2
        Me.CtrlCmdSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CtrlCmdSearch.Location = New System.Drawing.Point(495, 0)
        Me.CtrlCmdSearch.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrlCmdSearch.Name = "CtrlCmdSearch"
        Me.CtrlCmdSearch.SetArticleCode = Nothing
        Me.CtrlCmdSearch.SetRowIndex = 0
        Me.CtrlCmdSearch.Size = New System.Drawing.Size(100, 20)
        Me.CtrlCmdSearch.TabIndex = 2
        Me.CtrlCmdSearch.Text = "(Ctrl+F)"
        Me.CtrlCmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CtrlCmdSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.CtrlCmdSearch.UseVisualStyleBackColor = True
        Me.CtrlCmdSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlCmdSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlTxtBox
        '
        Me.CtrlTxtBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlTxtBox.AutoSize = False
        Me.CtrlTxtBox.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.CtrlTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlTxtBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlTxtBox.Location = New System.Drawing.Point(305, 0)
        Me.CtrlTxtBox.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrlTxtBox.MinimumSize = New System.Drawing.Size(10, 21)
        Me.CtrlTxtBox.Name = "CtrlTxtBox"
        Me.CtrlTxtBox.Size = New System.Drawing.Size(186, 21)
        Me.CtrlTxtBox.TabIndex = 1
        Me.CtrlTxtBox.Tag = "NO"
        Me.CtrlTxtBox.TextDetached = True
        Me.CtrlTxtBox.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlTxtBox.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrllabelSalesPerson
        '
        Me.CtrllabelSalesPerson.AttachedTextBoxName = Nothing
        Me.CtrllabelSalesPerson.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.CtrllabelSalesPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrllabelSalesPerson.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrllabelSalesPerson.ForeColor = System.Drawing.Color.Black
        Me.CtrllabelSalesPerson.Location = New System.Drawing.Point(0, 0)
        Me.CtrllabelSalesPerson.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrllabelSalesPerson.Name = "CtrllabelSalesPerson"
        Me.CtrllabelSalesPerson.Size = New System.Drawing.Size(120, 20)
        Me.CtrllabelSalesPerson.TabIndex = 3
        Me.CtrllabelSalesPerson.Tag = Nothing
        Me.CtrllabelSalesPerson.Text = "Sales Person"
        Me.CtrllabelSalesPerson.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CtrllabelSalesPerson.TextDetached = True
        Me.CtrllabelSalesPerson.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrllabelSalesPerson.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlSalesPerson
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.C1Sizer1)
        Me.Name = "CtrlSalesPerson"
        Me.Size = New System.Drawing.Size(595, 24)
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer1.ResumeLayout(False)
        Me.C1Sizer1.PerformLayout()
        CType(Me.CtrlSalesPersons, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlTxtBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrllabelSalesPerson, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents C1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents CtrlCmdSearch As CtrlBtn
    Friend WithEvents CtrlTxtBox As CtrlTextBox
    Friend WithEvents CtrllabelSalesPerson As CtrlLabel
    Friend WithEvents CtrlSalesPersons As C1.Win.C1List.C1Combo
    Friend WithEvents AndroidSearchTextBox As Spectrum.AndroidSearchTextBox

End Class
