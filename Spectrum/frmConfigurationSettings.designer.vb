<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigurationSettings
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
        Me.CtrlTabMain = New Spectrum.CtrlTab()
        Me.TabPageDefaults = New C1.Win.C1Command.C1DockingTabPage()
        Me.gridDefaults = New Spectrum.CtrlConfigDataGrid()
        Me.TabPageCashMemo = New C1.Win.C1Command.C1DockingTabPage()
        Me.gridCashMemo = New Spectrum.CtrlConfigDataGrid()
        Me.TabPageSalesOrder = New C1.Win.C1Command.C1DockingTabPage()
        Me.gridSalesOrder = New Spectrum.CtrlConfigDataGrid()
        Me.TabPageBirthList = New C1.Win.C1Command.C1DockingTabPage()
        Me.gridBirthList = New Spectrum.CtrlConfigDataGrid()
        Me.TabPageTillOpenClose = New C1.Win.C1Command.C1DockingTabPage()
        Me.gridTillOpenClose = New Spectrum.CtrlConfigDataGrid()
        Me.TabPagePCM = New C1.Win.C1Command.C1DockingTabPage()
        Me.gridPCM = New Spectrum.CtrlConfigDataGrid()
        Me.TabPageDC = New C1.Win.C1Command.C1DockingTabPage()
        Me.gridDC = New Spectrum.CtrlConfigDataGrid()
        Me.gridintegration = New Spectrum.CtrlConfigDataGrid()
        Me.Integration = New C1.Win.C1Command.C1DockingTabPage()
        Me.btnSaveSetting = New Spectrum.CtrlBtn()
        Me.btnCloseSetting = New Spectrum.CtrlBtn()
        CType(Me.CtrlTabMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CtrlTabMain.SuspendLayout()
        Me.TabPageDefaults.SuspendLayout()
        Me.TabPageCashMemo.SuspendLayout()
        Me.TabPageSalesOrder.SuspendLayout()
        Me.TabPageBirthList.SuspendLayout()
        Me.TabPageTillOpenClose.SuspendLayout()
        Me.TabPagePCM.SuspendLayout()
        Me.TabPageDC.SuspendLayout()
        Me.SuspendLayout()
        '
        'CtrlTabMain
        '
        Me.CtrlTabMain.Controls.Add(Me.TabPageDefaults)
        Me.CtrlTabMain.Controls.Add(Me.TabPageCashMemo)
        Me.CtrlTabMain.Controls.Add(Me.TabPageSalesOrder)
        Me.CtrlTabMain.Controls.Add(Me.TabPageBirthList)
        Me.CtrlTabMain.Controls.Add(Me.TabPageTillOpenClose)
        Me.CtrlTabMain.Controls.Add(Me.TabPagePCM)
        Me.CtrlTabMain.Controls.Add(Me.TabPageDC)
        Me.CtrlTabMain.Controls.Add(Me.Integration)
        Me.CtrlTabMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.CtrlTabMain.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlTabMain.Location = New System.Drawing.Point(0, 0)
        Me.CtrlTabMain.Name = "CtrlTabMain"
        Me.CtrlTabMain.SelectedIndex = 7
        Me.CtrlTabMain.SelectedTabBold = True
        Me.CtrlTabMain.Size = New System.Drawing.Size(867, 530)
        Me.CtrlTabMain.TabIndex = 0
        Me.CtrlTabMain.TabsSpacing = 5
        Me.CtrlTabMain.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007
        Me.CtrlTabMain.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Blue
        '
        'TabPageDefaults
        '
        Me.TabPageDefaults.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TabPageDefaults.Controls.Add(Me.gridDefaults)
        Me.TabPageDefaults.Location = New System.Drawing.Point(1, 28)
        Me.TabPageDefaults.Name = "TabPageDefaults"
        Me.TabPageDefaults.Size = New System.Drawing.Size(865, 501)
        Me.TabPageDefaults.TabForeColor = System.Drawing.Color.Black
        Me.TabPageDefaults.TabForeColorSelected = System.Drawing.Color.Olive
        Me.TabPageDefaults.TabIndex = 0
        Me.TabPageDefaults.Text = "General"
        '
        'gridDefaults
        '
        Me.gridDefaults.DefaultFlexGrid = Nothing
        Me.gridDefaults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridDefaults.Location = New System.Drawing.Point(0, 0)
        Me.gridDefaults.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.gridDefaults.Name = "gridDefaults"
        Me.gridDefaults.Size = New System.Drawing.Size(865, 501)
        Me.gridDefaults.TabIndex = 0
        '
        'TabPageCashMemo
        '
        Me.TabPageCashMemo.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TabPageCashMemo.Controls.Add(Me.gridCashMemo)
        Me.TabPageCashMemo.Location = New System.Drawing.Point(1, 28)
        Me.TabPageCashMemo.Name = "TabPageCashMemo"
        Me.TabPageCashMemo.Size = New System.Drawing.Size(865, 501)
        Me.TabPageCashMemo.TabForeColor = System.Drawing.Color.Black
        Me.TabPageCashMemo.TabForeColorSelected = System.Drawing.Color.Olive
        Me.TabPageCashMemo.TabIndex = 1
        Me.TabPageCashMemo.Text = "Cash Memo"
        '
        'gridCashMemo
        '
        Me.gridCashMemo.DefaultFlexGrid = Nothing
        Me.gridCashMemo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridCashMemo.Location = New System.Drawing.Point(0, 0)
        Me.gridCashMemo.Margin = New System.Windows.Forms.Padding(4)
        Me.gridCashMemo.Name = "gridCashMemo"
        Me.gridCashMemo.Size = New System.Drawing.Size(865, 501)
        Me.gridCashMemo.TabIndex = 0
        '
        'TabPageSalesOrder
        '
        Me.TabPageSalesOrder.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TabPageSalesOrder.Controls.Add(Me.gridSalesOrder)
        Me.TabPageSalesOrder.Location = New System.Drawing.Point(1, 28)
        Me.TabPageSalesOrder.Name = "TabPageSalesOrder"
        Me.TabPageSalesOrder.Size = New System.Drawing.Size(865, 501)
        Me.TabPageSalesOrder.TabForeColor = System.Drawing.Color.Black
        Me.TabPageSalesOrder.TabForeColorSelected = System.Drawing.Color.Olive
        Me.TabPageSalesOrder.TabIndex = 2
        Me.TabPageSalesOrder.Text = "Sales Order"
        '
        'gridSalesOrder
        '
        Me.gridSalesOrder.DefaultFlexGrid = Nothing
        Me.gridSalesOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridSalesOrder.Location = New System.Drawing.Point(0, 0)
        Me.gridSalesOrder.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.gridSalesOrder.Name = "gridSalesOrder"
        Me.gridSalesOrder.Size = New System.Drawing.Size(865, 501)
        Me.gridSalesOrder.TabIndex = 0
        '
        'TabPageBirthList
        '
        Me.TabPageBirthList.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TabPageBirthList.Controls.Add(Me.gridBirthList)
        Me.TabPageBirthList.Location = New System.Drawing.Point(1, 28)
        Me.TabPageBirthList.Name = "TabPageBirthList"
        Me.TabPageBirthList.Size = New System.Drawing.Size(865, 501)
        Me.TabPageBirthList.TabForeColor = System.Drawing.Color.Black
        Me.TabPageBirthList.TabForeColorSelected = System.Drawing.Color.Olive
        Me.TabPageBirthList.TabIndex = 3
        Me.TabPageBirthList.Text = "Birth-list"
        '
        'gridBirthList
        '
        Me.gridBirthList.DefaultFlexGrid = Nothing
        Me.gridBirthList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridBirthList.Location = New System.Drawing.Point(0, 0)
        Me.gridBirthList.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.gridBirthList.Name = "gridBirthList"
        Me.gridBirthList.Size = New System.Drawing.Size(865, 501)
        Me.gridBirthList.TabIndex = 0
        '
        'TabPageTillOpenClose
        '
        Me.TabPageTillOpenClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.TabPageTillOpenClose.Controls.Add(Me.gridTillOpenClose)
        Me.TabPageTillOpenClose.Location = New System.Drawing.Point(1, 28)
        Me.TabPageTillOpenClose.Name = "TabPageTillOpenClose"
        Me.TabPageTillOpenClose.Size = New System.Drawing.Size(865, 501)
        Me.TabPageTillOpenClose.TabForeColor = System.Drawing.Color.Black
        Me.TabPageTillOpenClose.TabForeColorSelected = System.Drawing.Color.Olive
        Me.TabPageTillOpenClose.TabIndex = 4
        Me.TabPageTillOpenClose.Text = "Till Open/Close"
        '
        'gridTillOpenClose
        '
        Me.gridTillOpenClose.DefaultFlexGrid = Nothing
        Me.gridTillOpenClose.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridTillOpenClose.Location = New System.Drawing.Point(0, 0)
        Me.gridTillOpenClose.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.gridTillOpenClose.Name = "gridTillOpenClose"
        Me.gridTillOpenClose.Size = New System.Drawing.Size(865, 501)
        Me.gridTillOpenClose.TabIndex = 0
        '
        'TabPagePCM
        '
        Me.TabPagePCM.Controls.Add(Me.gridPCM)
        Me.TabPagePCM.Location = New System.Drawing.Point(1, 28)
        Me.TabPagePCM.Name = "TabPagePCM"
        Me.TabPagePCM.Size = New System.Drawing.Size(865, 501)
        Me.TabPagePCM.TabForeColor = System.Drawing.Color.Black
        Me.TabPagePCM.TabForeColorSelected = System.Drawing.Color.Olive
        Me.TabPagePCM.TabIndex = 5
        Me.TabPagePCM.Text = "Petty Cash"
        '
        'gridPCM
        '
        Me.gridPCM.DefaultFlexGrid = Nothing
        Me.gridPCM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridPCM.Location = New System.Drawing.Point(0, 0)
        Me.gridPCM.Margin = New System.Windows.Forms.Padding(9, 6, 9, 6)
        Me.gridPCM.Name = "gridPCM"
        Me.gridPCM.Size = New System.Drawing.Size(865, 501)
        Me.gridPCM.TabIndex = 0
        '
        'TabPageDC
        '
        Me.TabPageDC.Controls.Add(Me.gridDC)
        Me.TabPageDC.Location = New System.Drawing.Point(1, 28)
        Me.TabPageDC.Name = "TabPageDC"
        Me.TabPageDC.Size = New System.Drawing.Size(865, 501)
        Me.TabPageDC.TabForeColor = System.Drawing.Color.Black
        Me.TabPageDC.TabForeColorSelected = System.Drawing.Color.Olive
        Me.TabPageDC.TabIndex = 6
        Me.TabPageDC.Text = "Day Close"
        '
        'gridDC
        '
        Me.gridDC.DefaultFlexGrid = Nothing
        Me.gridDC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridDC.Location = New System.Drawing.Point(0, 0)
        Me.gridDC.Margin = New System.Windows.Forms.Padding(14, 7, 14, 7)
        Me.gridDC.Name = "gridDC"
        Me.gridDC.Size = New System.Drawing.Size(865, 501)
        Me.gridDC.TabIndex = 0
        '
        'Integration
        '

        Me.Integration.Location = New System.Drawing.Point(1, 28)
        Me.Integration.Controls.Add(Me.gridintegration)
        Me.Integration.Name = "Integration"
        Me.Integration.Size = New System.Drawing.Size(865, 501)
        Me.Integration.TabForeColor = System.Drawing.Color.Black
        Me.Integration.TabForeColorSelected = System.Drawing.Color.Olive
        Me.Integration.TabIndex = 7
        Me.Integration.Text = "Integration"

        Me.GridIntegration.DefaultFlexGrid = Nothing
        Me.gridintegration.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridintegration.Location = New System.Drawing.Point(0, 0)
        Me.gridintegration.Margin = New System.Windows.Forms.Padding(14, 7, 14, 7)
        Me.gridintegration.Name = "gridintegration"
        Me.gridintegration.Size = New System.Drawing.Size(865, 501)
        Me.gridintegration.TabIndex = 0
        '
        'btnSaveSetting
        '
        Me.btnSaveSetting.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSaveSetting.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSaveSetting.Location = New System.Drawing.Point(335, 531)
        Me.btnSaveSetting.MoveToNxtCtrl = Nothing
        Me.btnSaveSetting.Name = "btnSaveSetting"
        Me.btnSaveSetting.SetArticleCode = Nothing
        Me.btnSaveSetting.SetRowIndex = 0
        Me.btnSaveSetting.Size = New System.Drawing.Size(111, 36)
        Me.btnSaveSetting.TabIndex = 1
        Me.btnSaveSetting.Text = "Save"
        Me.btnSaveSetting.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSaveSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnSaveSetting.UseVisualStyleBackColor = True
        Me.btnSaveSetting.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnCloseSetting
        '
        Me.btnCloseSetting.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnCloseSetting.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCloseSetting.Location = New System.Drawing.Point(452, 531)
        Me.btnCloseSetting.MoveToNxtCtrl = Nothing
        Me.btnCloseSetting.Name = "btnCloseSetting"
        Me.btnCloseSetting.SetArticleCode = Nothing
        Me.btnCloseSetting.SetRowIndex = 0
        Me.btnCloseSetting.Size = New System.Drawing.Size(111, 36)
        Me.btnCloseSetting.TabIndex = 2
        Me.btnCloseSetting.Text = "Close"
        Me.btnCloseSetting.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCloseSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCloseSetting.UseVisualStyleBackColor = True
        Me.btnCloseSetting.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmConfigurationSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(867, 571)
        Me.Controls.Add(Me.btnCloseSetting)
        Me.Controls.Add(Me.btnSaveSetting)
        Me.Controls.Add(Me.CtrlTabMain)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfigurationSettings"
        Me.Text = "Configuration Settings"
        CType(Me.CtrlTabMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CtrlTabMain.ResumeLayout(False)
        Me.TabPageDefaults.ResumeLayout(False)
        Me.TabPageCashMemo.ResumeLayout(False)
        Me.TabPageSalesOrder.ResumeLayout(False)
        Me.TabPageBirthList.ResumeLayout(False)
        Me.TabPageTillOpenClose.ResumeLayout(False)
        Me.TabPagePCM.ResumeLayout(False)
        Me.TabPageDC.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrlTabMain As Spectrum.CtrlTab
    Friend WithEvents TabPageDefaults As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents TabPageCashMemo As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents TabPageSalesOrder As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents TabPageBirthList As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents TabPageTillOpenClose As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents gridCashMemo As Spectrum.CtrlConfigDataGrid
    Friend WithEvents gridSalesOrder As Spectrum.CtrlConfigDataGrid
    Friend WithEvents gridBirthList As Spectrum.CtrlConfigDataGrid
    Friend WithEvents gridTillOpenClose As Spectrum.CtrlConfigDataGrid
    Friend WithEvents gridDefaults As Spectrum.CtrlConfigDataGrid
    Friend WithEvents btnSaveSetting As Spectrum.CtrlBtn
    Friend WithEvents btnCloseSetting As Spectrum.CtrlBtn
    Friend WithEvents TabPagePCM As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents gridPCM As Spectrum.CtrlConfigDataGrid
    Friend WithEvents TabPageDC As C1.Win.C1Command.C1DockingTabPage
    Friend WithEvents gridintegration As Spectrum.CtrlConfigDataGrid
    Friend WithEvents gridDC As Spectrum.CtrlConfigDataGrid
    Friend WithEvents Integration As C1.Win.C1Command.C1DockingTabPage
End Class
