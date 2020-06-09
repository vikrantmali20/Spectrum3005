<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearchCustomer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSearchCustomer))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.grdCLPCustomerList = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.buttomButtonPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnViewOrderHistory = New Spectrum.CtrlBtn()
        Me.btnExit = New Spectrum.CtrlBtn()
        Me.btnOk = New Spectrum.CtrlBtn()
        Me.btnViewSalesOrderHistory = New Spectrum.CtrlBtn()
        Me.BtnEditCustomer = New Spectrum.CtrlBtn()
        Me.BtnNewCustomer = New Spectrum.CtrlBtn()
        Me.topButtonPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.lblsearch = New Spectrum.CtrlLabel()
        Me.RadioBtnSalesCustm = New System.Windows.Forms.RadioButton()
        Me.RadioBtnCLPCustm = New System.Windows.Forms.RadioButton()
        Me.txtFilterCustomer = New Spectrum.CtrlTextBox()
        Me.lblPhone = New Spectrum.CtrlLabel()
        Me.txtSearchByPhn = New Spectrum.CtrlTextBox()
        Me.btnViewVaidyaNotes = New Spectrum.CtrlBtn()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.grdCLPCustomerList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.buttomButtonPanel.SuspendLayout()
        Me.topButtonPanel.SuspendLayout()
        CType(Me.lblsearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFilterCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPhone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSearchByPhn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.30853!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.69147!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.grdCLPCustomerList, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.buttomButtonPanel, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.topButtonPanel, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(902, 474)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'grdCLPCustomerList
        '
        Me.grdCLPCustomerList.AllowEditing = False
        Me.grdCLPCustomerList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.grdCLPCustomerList.BackColor = System.Drawing.Color.White
        Me.grdCLPCustomerList.ColumnInfo = "7,1,0,0,0,105,Columns:"
        Me.TableLayoutPanel1.SetColumnSpan(Me.grdCLPCustomerList, 3)
        Me.grdCLPCustomerList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdCLPCustomerList.ExtendLastCol = True
        Me.grdCLPCustomerList.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdCLPCustomerList.Location = New System.Drawing.Point(3, 38)
        Me.grdCLPCustomerList.Name = "grdCLPCustomerList"
        Me.grdCLPCustomerList.Rows.Count = 1
        Me.grdCLPCustomerList.Rows.DefaultSize = 21
        Me.grdCLPCustomerList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox
        Me.grdCLPCustomerList.Size = New System.Drawing.Size(896, 378)
        Me.grdCLPCustomerList.StyleInfo = resources.GetString("grdCLPCustomerList.StyleInfo")
        Me.grdCLPCustomerList.TabIndex = 1
        '
        'buttomButtonPanel
        '
        Me.buttomButtonPanel.ColumnCount = 8
        Me.TableLayoutPanel1.SetColumnSpan(Me.buttomButtonPanel, 3)
        Me.buttomButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.buttomButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 154.0!))
        Me.buttomButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72.0!))
        Me.buttomButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74.0!))
        Me.buttomButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113.0!))
        Me.buttomButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.buttomButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.1215!))
        Me.buttomButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.11215!))
        Me.buttomButtonPanel.Controls.Add(Me.btnViewVaidyaNotes, 4, 0)
        Me.buttomButtonPanel.Controls.Add(Me.BtnViewOrderHistory, 0, 0)
        Me.buttomButtonPanel.Controls.Add(Me.btnExit, 6, 0)
        Me.buttomButtonPanel.Controls.Add(Me.btnOk, 5, 0)
        Me.buttomButtonPanel.Controls.Add(Me.btnViewSalesOrderHistory, 1, 0)
        Me.buttomButtonPanel.Controls.Add(Me.BtnEditCustomer, 3, 0)
        Me.buttomButtonPanel.Controls.Add(Me.BtnNewCustomer, 2, 0)
        Me.buttomButtonPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.buttomButtonPanel.Location = New System.Drawing.Point(3, 422)
        Me.buttomButtonPanel.Name = "buttomButtonPanel"
        Me.buttomButtonPanel.RowCount = 1
        Me.buttomButtonPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.buttomButtonPanel.Size = New System.Drawing.Size(896, 29)
        Me.buttomButtonPanel.TabIndex = 6
        '
        'BtnViewOrderHistory
        '
        Me.BtnViewOrderHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnViewOrderHistory.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.BtnViewOrderHistory.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnViewOrderHistory.Location = New System.Drawing.Point(3, 3)
        Me.BtnViewOrderHistory.MoveToNxtCtrl = Nothing
        Me.BtnViewOrderHistory.Name = "BtnViewOrderHistory"
        Me.BtnViewOrderHistory.SetArticleCode = Nothing
        Me.BtnViewOrderHistory.SetRowIndex = 0
        Me.BtnViewOrderHistory.Size = New System.Drawing.Size(100, 23)
        Me.BtnViewOrderHistory.TabIndex = 6
        Me.BtnViewOrderHistory.Text = "&Order History"
        Me.BtnViewOrderHistory.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnViewOrderHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnViewOrderHistory.UseVisualStyleBackColor = True
        Me.BtnViewOrderHistory.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnExit
        '
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnExit.Location = New System.Drawing.Point(577, 3)
        Me.btnExit.MoveToNxtCtrl = Nothing
        Me.btnExit.Name = "btnExit"
        Me.btnExit.SetArticleCode = Nothing
        Me.btnExit.SetRowIndex = 0
        Me.btnExit.Size = New System.Drawing.Size(62, 23)
        Me.btnExit.TabIndex = 1
        Me.btnExit.Text = "Cancel"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnExit.UseVisualStyleBackColor = True
        Me.btnExit.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnOk
        '
        Me.btnOk.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnOk.Location = New System.Drawing.Point(522, 3)
        Me.btnOk.MoveToNxtCtrl = Nothing
        Me.btnOk.Name = "btnOk"
        Me.btnOk.SetArticleCode = Nothing
        Me.btnOk.SetRowIndex = 0
        Me.btnOk.Size = New System.Drawing.Size(49, 23)
        Me.btnOk.TabIndex = 0
        Me.btnOk.Text = "Ok"
        Me.btnOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOk.UseVisualStyleBackColor = True
        Me.btnOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnViewSalesOrderHistory
        '
        Me.btnViewSalesOrderHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnViewSalesOrderHistory.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.btnViewSalesOrderHistory.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnViewSalesOrderHistory.Location = New System.Drawing.Point(109, 3)
        Me.btnViewSalesOrderHistory.MoveToNxtCtrl = Nothing
        Me.btnViewSalesOrderHistory.Name = "btnViewSalesOrderHistory"
        Me.btnViewSalesOrderHistory.SetArticleCode = Nothing
        Me.btnViewSalesOrderHistory.SetRowIndex = 0
        Me.btnViewSalesOrderHistory.Size = New System.Drawing.Size(148, 23)
        Me.btnViewSalesOrderHistory.TabIndex = 8
        Me.btnViewSalesOrderHistory.Text = "&Sales Order History"
        Me.btnViewSalesOrderHistory.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnViewSalesOrderHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnViewSalesOrderHistory.UseVisualStyleBackColor = True
        Me.btnViewSalesOrderHistory.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnEditCustomer
        '
        Me.BtnEditCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnEditCustomer.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.BtnEditCustomer.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnEditCustomer.Location = New System.Drawing.Point(335, 3)
        Me.BtnEditCustomer.MoveToNxtCtrl = Nothing
        Me.BtnEditCustomer.Name = "BtnEditCustomer"
        Me.BtnEditCustomer.SetArticleCode = Nothing
        Me.BtnEditCustomer.SetRowIndex = 0
        Me.BtnEditCustomer.Size = New System.Drawing.Size(68, 23)
        Me.BtnEditCustomer.TabIndex = 5
        Me.BtnEditCustomer.Text = "&Edit"
        Me.BtnEditCustomer.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnEditCustomer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnEditCustomer.UseVisualStyleBackColor = True
        Me.BtnEditCustomer.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnNewCustomer
        '
        Me.BtnNewCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnNewCustomer.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.BtnNewCustomer.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnNewCustomer.Location = New System.Drawing.Point(263, 3)
        Me.BtnNewCustomer.MoveToNxtCtrl = Nothing
        Me.BtnNewCustomer.Name = "BtnNewCustomer"
        Me.BtnNewCustomer.SetArticleCode = Nothing
        Me.BtnNewCustomer.SetRowIndex = 0
        Me.BtnNewCustomer.Size = New System.Drawing.Size(66, 23)
        Me.BtnNewCustomer.TabIndex = 4
        Me.BtnNewCustomer.Text = "&New"
        Me.BtnNewCustomer.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnNewCustomer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnNewCustomer.UseVisualStyleBackColor = True
        Me.BtnNewCustomer.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'topButtonPanel
        '
        Me.topButtonPanel.ColumnCount = 9
        Me.TableLayoutPanel1.SetColumnSpan(Me.topButtonPanel, 3)
        Me.topButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.topButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.topButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.topButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 19.0!))
        Me.topButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.topButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230.0!))
        Me.topButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78.0!))
        Me.topButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142.0!))
        Me.topButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.topButtonPanel.Controls.Add(Me.lblsearch, 4, 0)
        Me.topButtonPanel.Controls.Add(Me.RadioBtnSalesCustm, 2, 0)
        Me.topButtonPanel.Controls.Add(Me.RadioBtnCLPCustm, 1, 0)
        Me.topButtonPanel.Controls.Add(Me.txtFilterCustomer, 5, 0)
        Me.topButtonPanel.Controls.Add(Me.lblPhone, 6, 0)
        Me.topButtonPanel.Controls.Add(Me.txtSearchByPhn, 7, 0)
        Me.topButtonPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.topButtonPanel.Location = New System.Drawing.Point(3, 3)
        Me.topButtonPanel.Name = "topButtonPanel"
        Me.topButtonPanel.RowCount = 1
        Me.topButtonPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.topButtonPanel.Size = New System.Drawing.Size(896, 29)
        Me.topButtonPanel.TabIndex = 7
        '
        'lblsearch
        '
        Me.lblsearch.AttachedTextBoxName = Nothing
        Me.lblsearch.AutoSize = True
        Me.lblsearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblsearch.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblsearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblsearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblsearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsearch.ForeColor = System.Drawing.Color.Black
        Me.lblsearch.Location = New System.Drawing.Point(331, 0)
        Me.lblsearch.Name = "lblsearch"
        Me.lblsearch.Size = New System.Drawing.Size(74, 29)
        Me.lblsearch.TabIndex = 30
        Me.lblsearch.Tag = Nothing
        Me.lblsearch.Text = "Search "
        Me.lblsearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblsearch.TextDetached = True
        Me.lblsearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'RadioBtnSalesCustm
        '
        Me.RadioBtnSalesCustm.AutoSize = True
        Me.RadioBtnSalesCustm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioBtnSalesCustm.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.RadioBtnSalesCustm.Location = New System.Drawing.Point(162, 3)
        Me.RadioBtnSalesCustm.Name = "RadioBtnSalesCustm"
        Me.RadioBtnSalesCustm.Size = New System.Drawing.Size(144, 23)
        Me.RadioBtnSalesCustm.TabIndex = 3
        Me.RadioBtnSalesCustm.TabStop = True
        Me.RadioBtnSalesCustm.Text = "Other Customer"
        Me.RadioBtnSalesCustm.UseVisualStyleBackColor = True
        '
        'RadioBtnCLPCustm
        '
        Me.RadioBtnCLPCustm.AutoSize = True
        Me.RadioBtnCLPCustm.Checked = True
        Me.RadioBtnCLPCustm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioBtnCLPCustm.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.RadioBtnCLPCustm.Location = New System.Drawing.Point(12, 3)
        Me.RadioBtnCLPCustm.Name = "RadioBtnCLPCustm"
        Me.RadioBtnCLPCustm.Size = New System.Drawing.Size(144, 23)
        Me.RadioBtnCLPCustm.TabIndex = 2
        Me.RadioBtnCLPCustm.TabStop = True
        Me.RadioBtnCLPCustm.Text = "CLP Customer"
        Me.RadioBtnCLPCustm.UseVisualStyleBackColor = True
        '
        'txtFilterCustomer
        '
        Me.txtFilterCustomer.AutoSize = False
        Me.txtFilterCustomer.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtFilterCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilterCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtFilterCustomer.Location = New System.Drawing.Point(411, 3)
        Me.txtFilterCustomer.MaximumSize = New System.Drawing.Size(400, 21)
        Me.txtFilterCustomer.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtFilterCustomer.MoveToNxtCtrl = Nothing
        Me.txtFilterCustomer.Name = "txtFilterCustomer"
        Me.txtFilterCustomer.Size = New System.Drawing.Size(224, 21)
        Me.txtFilterCustomer.TabIndex = 8
        Me.txtFilterCustomer.Tag = Nothing
        Me.txtFilterCustomer.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtFilterCustomer.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblPhone
        '
        Me.lblPhone.AttachedTextBoxName = Nothing
        Me.lblPhone.AutoSize = True
        Me.lblPhone.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblPhone.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPhone.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhone.ForeColor = System.Drawing.Color.Black
        Me.lblPhone.Location = New System.Drawing.Point(641, 0)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(72, 29)
        Me.lblPhone.TabIndex = 31
        Me.lblPhone.Tag = Nothing
        Me.lblPhone.Text = "   Swipe "
        Me.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPhone.TextDetached = True
        Me.lblPhone.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtSearchByPhn
        '
        Me.txtSearchByPhn.AutoSize = False
        Me.txtSearchByPhn.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtSearchByPhn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchByPhn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchByPhn.Location = New System.Drawing.Point(719, 3)
        Me.txtSearchByPhn.MaximumSize = New System.Drawing.Size(400, 21)
        Me.txtSearchByPhn.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtSearchByPhn.MoveToNxtCtrl = Nothing
        Me.txtSearchByPhn.Name = "txtSearchByPhn"
        Me.txtSearchByPhn.Size = New System.Drawing.Size(136, 21)
        Me.txtSearchByPhn.TabIndex = 32
        Me.txtSearchByPhn.Tag = Nothing
        Me.txtSearchByPhn.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtSearchByPhn.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnViewVaidyaNotes
        '
        Me.btnViewVaidyaNotes.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnViewVaidyaNotes.Location = New System.Drawing.Point(409, 3)
        Me.btnViewVaidyaNotes.MoveToNxtCtrl = Nothing
        Me.btnViewVaidyaNotes.Name = "btnViewVaidyaNotes"
        Me.btnViewVaidyaNotes.SetArticleCode = Nothing
        Me.btnViewVaidyaNotes.SetRowIndex = 0
        Me.btnViewVaidyaNotes.Size = New System.Drawing.Size(107, 23)
        Me.btnViewVaidyaNotes.TabIndex = 10
        Me.btnViewVaidyaNotes.Text = "View Vaidya Notes"
        Me.btnViewVaidyaNotes.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnViewVaidyaNotes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnViewVaidyaNotes.UseVisualStyleBackColor = True
        '
        'frmSearchCustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(902, 474)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.KeyPreview = True
        Me.Name = "frmSearchCustomer"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Search Customer"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.grdCLPCustomerList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.buttomButtonPanel.ResumeLayout(False)
        Me.topButtonPanel.ResumeLayout(False)
        Me.topButtonPanel.PerformLayout()
        CType(Me.lblsearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFilterCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPhone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSearchByPhn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grdCLPCustomerList As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents RadioBtnCLPCustm As System.Windows.Forms.RadioButton
    Friend WithEvents RadioBtnSalesCustm As System.Windows.Forms.RadioButton
    Friend WithEvents BtnNewCustomer As Spectrum.CtrlBtn
    Friend WithEvents btnExit As Spectrum.CtrlBtn
    Friend WithEvents btnOk As Spectrum.CtrlBtn
    Friend WithEvents buttomButtonPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents topButtonPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents BtnEditCustomer As Spectrum.CtrlBtn
    Friend WithEvents txtFilterCustomer As Spectrum.CtrlTextBox
    Friend WithEvents lblsearch As Spectrum.CtrlLabel
    Friend WithEvents BtnViewOrderHistory As Spectrum.CtrlBtn
    Friend WithEvents btnViewSalesOrderHistory As Spectrum.CtrlBtn
    Friend WithEvents lblPhone As Spectrum.CtrlLabel
    Friend WithEvents txtSearchByPhn As Spectrum.CtrlTextBox
    Friend WithEvents btnViewVaidyaNotes As Spectrum.CtrlBtn
End Class
