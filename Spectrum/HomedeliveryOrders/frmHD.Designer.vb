﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHD
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHD))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnRejectOrder = New Spectrum.CtrlBtn()
        Me.cmdPrint = New Spectrum.CtrlBtn()
        Me.cmdDispatch = New Spectrum.CtrlBtn()
        Me.cmdAccept = New Spectrum.CtrlBtn()
        Me.CmdViewOrder = New Spectrum.CtrlBtn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.grdCreditSales = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.topButtonPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.lblsearch = New Spectrum.CtrlLabel()
        Me.txtFilterCreditSales = New System.Windows.Forms.TextBox()
        Me.DtToDate = New Spectrum.ctrlDate()
        Me.DtFromDate = New Spectrum.ctrlDate()
        Me.lblToDeliveryDate = New Spectrum.CtrlLabel()
        Me.lblFromDeliveryDate = New Spectrum.CtrlLabel()
        Me.BtnFilterSearch = New Spectrum.CtrlBtn()
        Me.lblOrderStatus = New Spectrum.CtrlLabel()
        Me.lblChannelPartner = New Spectrum.CtrlLabel()
        Me.cmbOrderStatus = New Spectrum.ctrlCombo()
        Me.lblDeliveryPerson = New Spectrum.CtrlLabel()
        Me.cmbChannelPartner = New Spectrum.ctrlCombo()
        Me.txtDeliveryId = New Spectrum.CtrlTextBox()
        Me.cmdRefreshGrid = New Spectrum.CtrlBtn()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.grdCreditSales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.topButtonPanel.SuspendLayout()
        CType(Me.lblsearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtToDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtFromDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToDeliveryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromDeliveryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOrderStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblChannelPartner, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbOrderStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDeliveryPerson, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbChannelPartner, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeliveryId, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.73684!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.26316!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 438.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.grdCreditSales, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.topButtonPanel, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1007, 530)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 7
        Me.TableLayoutPanel1.SetColumnSpan(Me.TableLayoutPanel2, 3)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnRejectOrder, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.cmdPrint, 5, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.cmdDispatch, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.cmdAccept, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.CmdViewOrder, 4, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Panel1, 6, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Panel2, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 477)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1001, 50)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'btnRejectOrder
        '
        Me.btnRejectOrder.AutoSize = True
        Me.btnRejectOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRejectOrder.Image = Global.Spectrum.My.Resources.Resources.can
        Me.btnRejectOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRejectOrder.Location = New System.Drawing.Point(446, 3)
        Me.btnRejectOrder.MoveToNxtCtrl = Nothing
        Me.btnRejectOrder.Name = "btnRejectOrder"
        Me.btnRejectOrder.SetArticleCode = Nothing
        Me.btnRejectOrder.SetRowIndex = 0
        Me.btnRejectOrder.Size = New System.Drawing.Size(109, 44)
        Me.btnRejectOrder.TabIndex = 3
        Me.btnRejectOrder.Text = "Reject Order"
        Me.btnRejectOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRejectOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnRejectOrder.UseVisualStyleBackColor = True
        Me.btnRejectOrder.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdPrint
        '
        Me.cmdPrint.AutoSize = True
        Me.cmdPrint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdPrint.Location = New System.Drawing.Point(676, 3)
        Me.cmdPrint.MoveToNxtCtrl = Nothing
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.SetArticleCode = Nothing
        Me.cmdPrint.SetRowIndex = 0
        Me.cmdPrint.Size = New System.Drawing.Size(109, 44)
        Me.cmdPrint.TabIndex = 4
        Me.cmdPrint.Text = "Print(F3)"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdPrint.UseVisualStyleBackColor = True
        Me.cmdPrint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdDispatch
        '
        Me.cmdDispatch.AutoSize = True
        Me.cmdDispatch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmdDispatch.Image = Global.Spectrum.My.Resources.Resources.icoOutbound_Delivery1
        Me.cmdDispatch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdDispatch.Location = New System.Drawing.Point(216, 3)
        Me.cmdDispatch.MoveToNxtCtrl = Nothing
        Me.cmdDispatch.Name = "cmdDispatch"
        Me.cmdDispatch.SetArticleCode = Nothing
        Me.cmdDispatch.SetRowIndex = 0
        Me.cmdDispatch.Size = New System.Drawing.Size(109, 44)
        Me.cmdDispatch.TabIndex = 2
        Me.cmdDispatch.Text = "Dispatch"
        Me.cmdDispatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdDispatch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdDispatch.UseVisualStyleBackColor = True
        Me.cmdDispatch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdAccept
        '
        Me.cmdAccept.AutoSize = True
        Me.cmdAccept.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmdAccept.Image = Global.Spectrum.My.Resources.Resources.icoOutbound_Delivery1
        Me.cmdAccept.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdAccept.Location = New System.Drawing.Point(331, 3)
        Me.cmdAccept.MoveToNxtCtrl = Nothing
        Me.cmdAccept.Name = "cmdAccept"
        Me.cmdAccept.SetArticleCode = Nothing
        Me.cmdAccept.SetRowIndex = 0
        Me.cmdAccept.Size = New System.Drawing.Size(109, 44)
        Me.cmdAccept.TabIndex = 5
        Me.cmdAccept.Text = "Accept"
        Me.cmdAccept.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdAccept.UseVisualStyleBackColor = True
        Me.cmdAccept.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CmdViewOrder
        '
        Me.CmdViewOrder.AutoSize = True
        Me.CmdViewOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CmdViewOrder.Image = Global.Spectrum.My.Resources.Resources.icoOutbound_Delivery1
        Me.CmdViewOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmdViewOrder.Location = New System.Drawing.Point(561, 3)
        Me.CmdViewOrder.MoveToNxtCtrl = Nothing
        Me.CmdViewOrder.Name = "CmdViewOrder"
        Me.CmdViewOrder.SetArticleCode = Nothing
        Me.CmdViewOrder.SetRowIndex = 0
        Me.CmdViewOrder.Size = New System.Drawing.Size(109, 44)
        Me.CmdViewOrder.TabIndex = 6
        Me.CmdViewOrder.Text = "View Order"
        Me.CmdViewOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CmdViewOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CmdViewOrder.UseVisualStyleBackColor = True
        Me.CmdViewOrder.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(791, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(200, 44)
        Me.Panel1.TabIndex = 7
        '
        'Panel2
        '
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(200, 44)
        Me.Panel2.TabIndex = 8
        '
        'grdCreditSales
        '
        Me.grdCreditSales.AllowEditing = False
        Me.grdCreditSales.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.grdCreditSales.ColumnInfo = "7,1,0,0,0,105,Columns:5{Style:""TextAlign:GeneralCenter;ImageAlign:CenterCenter;"";" & _
    "}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.TableLayoutPanel1.SetColumnSpan(Me.grdCreditSales, 3)
        Me.grdCreditSales.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdCreditSales.ExtendLastCol = True
        Me.grdCreditSales.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdCreditSales.Location = New System.Drawing.Point(3, 78)
        Me.grdCreditSales.Name = "grdCreditSales"
        Me.grdCreditSales.Rows.Count = 1
        Me.grdCreditSales.Rows.DefaultSize = 21
        Me.grdCreditSales.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.grdCreditSales.Size = New System.Drawing.Size(1001, 385)
        Me.grdCreditSales.StyleInfo = resources.GetString("grdCreditSales.StyleInfo")
        Me.grdCreditSales.TabIndex = 2
        Me.grdCreditSales.TabStop = False
        '
        'topButtonPanel
        '
        Me.topButtonPanel.ColumnCount = 8
        Me.TableLayoutPanel1.SetColumnSpan(Me.topButtonPanel, 3)
        Me.topButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 121.0!))
        Me.topButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 149.0!))
        Me.topButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105.0!))
        Me.topButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 159.0!))
        Me.topButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111.0!))
        Me.topButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 178.0!))
        Me.topButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89.0!))
        Me.topButtonPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89.0!))
        Me.topButtonPanel.Controls.Add(Me.lblsearch, 4, 1)
        Me.topButtonPanel.Controls.Add(Me.txtFilterCreditSales, 5, 1)
        Me.topButtonPanel.Controls.Add(Me.DtToDate, 3, 1)
        Me.topButtonPanel.Controls.Add(Me.DtFromDate, 1, 1)
        Me.topButtonPanel.Controls.Add(Me.lblToDeliveryDate, 2, 1)
        Me.topButtonPanel.Controls.Add(Me.lblFromDeliveryDate, 0, 1)
        Me.topButtonPanel.Controls.Add(Me.BtnFilterSearch, 6, 0)
        Me.topButtonPanel.Controls.Add(Me.lblOrderStatus, 0, 0)
        Me.topButtonPanel.Controls.Add(Me.lblChannelPartner, 2, 0)
        Me.topButtonPanel.Controls.Add(Me.cmbOrderStatus, 1, 0)
        Me.topButtonPanel.Controls.Add(Me.lblDeliveryPerson, 4, 0)
        Me.topButtonPanel.Controls.Add(Me.cmbChannelPartner, 3, 0)
        Me.topButtonPanel.Controls.Add(Me.txtDeliveryId, 5, 0)
        Me.topButtonPanel.Controls.Add(Me.cmdRefreshGrid, 7, 0)
        Me.topButtonPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.topButtonPanel.Location = New System.Drawing.Point(3, 3)
        Me.topButtonPanel.Name = "topButtonPanel"
        Me.topButtonPanel.RowCount = 2
        Me.topButtonPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.topButtonPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.topButtonPanel.Size = New System.Drawing.Size(1001, 69)
        Me.topButtonPanel.TabIndex = 0
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
        Me.lblsearch.Location = New System.Drawing.Point(537, 34)
        Me.lblsearch.Name = "lblsearch"
        Me.lblsearch.Size = New System.Drawing.Size(105, 35)
        Me.lblsearch.TabIndex = 1
        Me.lblsearch.Tag = Nothing
        Me.lblsearch.Text = "Search "
        Me.lblsearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblsearch.TextDetached = True
        Me.lblsearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtFilterCreditSales
        '
        Me.txtFilterCreditSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilterCreditSales.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtFilterCreditSales.Location = New System.Drawing.Point(648, 37)
        Me.txtFilterCreditSales.MaximumSize = New System.Drawing.Size(400, 21)
        Me.txtFilterCreditSales.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtFilterCreditSales.Name = "txtFilterCreditSales"
        Me.txtFilterCreditSales.Size = New System.Drawing.Size(172, 20)
        Me.txtFilterCreditSales.TabIndex = 0
        '
        'DtToDate
        '
        Me.DtToDate.AutoSize = False
        Me.DtToDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.DtToDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        '
        '
        '
        Me.DtToDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.DtToDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.DtToDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.DtToDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.DtToDate.DisplayFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.DtToDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DtToDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDateShortTime
        Me.DtToDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.DtToDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtToDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDateShortTime
        Me.DtToDate.Location = New System.Drawing.Point(376, 35)
        Me.DtToDate.Margin = New System.Windows.Forms.Padding(1, 1, 1, 3)
        Me.DtToDate.Name = "DtToDate"
        Me.DtToDate.Size = New System.Drawing.Size(157, 31)
        Me.DtToDate.TabIndex = 129
        Me.DtToDate.Tag = Nothing
        Me.DtToDate.TrimStart = True
        Me.DtToDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        Me.DtToDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.DtToDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'DtFromDate
        '
        Me.DtFromDate.AutoSize = False
        Me.DtFromDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.DtFromDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        '
        '
        '
        Me.DtFromDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.DtFromDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.DtFromDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.DtFromDate.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.GeneralDate
        Me.DtFromDate.DisplayFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.DtFromDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DtFromDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDateShortTime
        Me.DtFromDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.DtFromDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtFromDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDateShortTime
        Me.DtFromDate.Location = New System.Drawing.Point(122, 35)
        Me.DtFromDate.Margin = New System.Windows.Forms.Padding(1, 1, 1, 3)
        Me.DtFromDate.Name = "DtFromDate"
        Me.DtFromDate.Size = New System.Drawing.Size(147, 31)
        Me.DtFromDate.TabIndex = 129
        Me.DtFromDate.Tag = Nothing
        Me.DtFromDate.TrimStart = True
        Me.DtFromDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        Me.DtFromDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.DtFromDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblToDeliveryDate
        '
        Me.lblToDeliveryDate.AttachedTextBoxName = Nothing
        Me.lblToDeliveryDate.AutoSize = True
        Me.lblToDeliveryDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblToDeliveryDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblToDeliveryDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblToDeliveryDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblToDeliveryDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDeliveryDate.Location = New System.Drawing.Point(273, 34)
        Me.lblToDeliveryDate.MinimumSize = New System.Drawing.Size(50, 2)
        Me.lblToDeliveryDate.Name = "lblToDeliveryDate"
        Me.lblToDeliveryDate.Size = New System.Drawing.Size(99, 35)
        Me.lblToDeliveryDate.TabIndex = 40
        Me.lblToDeliveryDate.Tag = Nothing
        Me.lblToDeliveryDate.Text = "To Delivery Date:"
        Me.lblToDeliveryDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblToDeliveryDate.TextDetached = True
        '
        'lblFromDeliveryDate
        '
        Me.lblFromDeliveryDate.AttachedTextBoxName = Nothing
        Me.lblFromDeliveryDate.AutoSize = True
        Me.lblFromDeliveryDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblFromDeliveryDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblFromDeliveryDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFromDeliveryDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblFromDeliveryDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDeliveryDate.Location = New System.Drawing.Point(3, 34)
        Me.lblFromDeliveryDate.MinimumSize = New System.Drawing.Size(50, 2)
        Me.lblFromDeliveryDate.Name = "lblFromDeliveryDate"
        Me.lblFromDeliveryDate.Size = New System.Drawing.Size(115, 35)
        Me.lblFromDeliveryDate.TabIndex = 44
        Me.lblFromDeliveryDate.Tag = Nothing
        Me.lblFromDeliveryDate.Text = "From Delivery Date:"
        Me.lblFromDeliveryDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblFromDeliveryDate.TextDetached = True
        '
        'BtnFilterSearch
        '
        Me.BtnFilterSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.BtnFilterSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFilterSearch.Location = New System.Drawing.Point(827, 3)
        Me.BtnFilterSearch.MoveToNxtCtrl = Nothing
        Me.BtnFilterSearch.Name = "BtnFilterSearch"
        Me.BtnFilterSearch.SetArticleCode = Nothing
        Me.BtnFilterSearch.SetRowIndex = 0
        Me.BtnFilterSearch.Size = New System.Drawing.Size(82, 28)
        Me.BtnFilterSearch.TabIndex = 48
        Me.BtnFilterSearch.Text = "Search"
        Me.BtnFilterSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnFilterSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BtnFilterSearch.UseVisualStyleBackColor = True
        Me.BtnFilterSearch.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblOrderStatus
        '
        Me.lblOrderStatus.AttachedTextBoxName = Nothing
        Me.lblOrderStatus.AutoSize = True
        Me.lblOrderStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblOrderStatus.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblOrderStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOrderStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOrderStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrderStatus.Location = New System.Drawing.Point(3, 0)
        Me.lblOrderStatus.Name = "lblOrderStatus"
        Me.lblOrderStatus.Size = New System.Drawing.Size(115, 34)
        Me.lblOrderStatus.TabIndex = 8
        Me.lblOrderStatus.Tag = Nothing
        Me.lblOrderStatus.Text = "Order Status:"
        Me.lblOrderStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblOrderStatus.TextDetached = True
        '
        'lblChannelPartner
        '
        Me.lblChannelPartner.AttachedTextBoxName = Nothing
        Me.lblChannelPartner.AutoSize = True
        Me.lblChannelPartner.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblChannelPartner.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblChannelPartner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblChannelPartner.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblChannelPartner.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChannelPartner.Location = New System.Drawing.Point(273, 0)
        Me.lblChannelPartner.Name = "lblChannelPartner"
        Me.lblChannelPartner.Size = New System.Drawing.Size(99, 34)
        Me.lblChannelPartner.TabIndex = 3
        Me.lblChannelPartner.Tag = Nothing
        Me.lblChannelPartner.Text = "Channel Partner"
        Me.lblChannelPartner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblChannelPartner.TextDetached = True
        '
        'cmbOrderStatus
        '
        Me.cmbOrderStatus.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbOrderStatus.AutoCompletion = True
        Me.cmbOrderStatus.AutoDropDown = True
        Me.cmbOrderStatus.Caption = ""
        Me.cmbOrderStatus.CaptionHeight = 17
        Me.cmbOrderStatus.CaptionVisible = False
        Me.cmbOrderStatus.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbOrderStatus.ColumnCaptionHeight = 17
        Me.cmbOrderStatus.ColumnFooterHeight = 17
        Me.cmbOrderStatus.ColumnHeaders = False
        Me.cmbOrderStatus.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList
        Me.cmbOrderStatus.ContentHeight = 15
        Me.cmbOrderStatus.ctrlTextDbColumn = ""
        Me.cmbOrderStatus.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbOrderStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbOrderStatus.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cmbOrderStatus.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOrderStatus.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOrderStatus.EditorHeight = 15
        Me.cmbOrderStatus.Images.Add(CType(resources.GetObject("cmbOrderStatus.Images"), System.Drawing.Image))
        Me.cmbOrderStatus.ItemHeight = 15
        Me.cmbOrderStatus.Location = New System.Drawing.Point(124, 3)
        Me.cmbOrderStatus.MatchEntryTimeout = CType(2000, Long)
        Me.cmbOrderStatus.MaxDropDownItems = CType(5, Short)
        Me.cmbOrderStatus.MaxLength = 32767
        Me.cmbOrderStatus.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbOrderStatus.MoveToNxtCtrl = Nothing
        Me.cmbOrderStatus.Name = "cmbOrderStatus"
        Me.cmbOrderStatus.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbOrderStatus.Size = New System.Drawing.Size(143, 21)
        Me.cmbOrderStatus.strSelectStmt = ""
        Me.cmbOrderStatus.TabIndex = 35
        Me.cmbOrderStatus.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbOrderStatus.PropBag = resources.GetString("cmbOrderStatus.PropBag")
        '
        'lblDeliveryPerson
        '
        Me.lblDeliveryPerson.AttachedTextBoxName = Nothing
        Me.lblDeliveryPerson.AutoSize = True
        Me.lblDeliveryPerson.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDeliveryPerson.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDeliveryPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDeliveryPerson.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDeliveryPerson.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeliveryPerson.Location = New System.Drawing.Point(537, 0)
        Me.lblDeliveryPerson.Name = "lblDeliveryPerson"
        Me.lblDeliveryPerson.Size = New System.Drawing.Size(105, 34)
        Me.lblDeliveryPerson.TabIndex = 7
        Me.lblDeliveryPerson.Tag = Nothing
        Me.lblDeliveryPerson.Text = "Delivery Person:"
        Me.lblDeliveryPerson.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblDeliveryPerson.TextDetached = True
        '
        'cmbChannelPartner
        '
        Me.cmbChannelPartner.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cmbChannelPartner.AutoCompletion = True
        Me.cmbChannelPartner.AutoDropDown = True
        Me.cmbChannelPartner.Caption = ""
        Me.cmbChannelPartner.CaptionHeight = 17
        Me.cmbChannelPartner.CaptionVisible = False
        Me.cmbChannelPartner.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbChannelPartner.ColumnCaptionHeight = 17
        Me.cmbChannelPartner.ColumnFooterHeight = 17
        Me.cmbChannelPartner.ColumnHeaders = False
        Me.cmbChannelPartner.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList
        Me.cmbChannelPartner.ContentHeight = 15
        Me.cmbChannelPartner.ctrlTextDbColumn = ""
        Me.cmbChannelPartner.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cmbChannelPartner.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbChannelPartner.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cmbChannelPartner.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbChannelPartner.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbChannelPartner.EditorHeight = 15
        Me.cmbChannelPartner.Images.Add(CType(resources.GetObject("cmbChannelPartner.Images"), System.Drawing.Image))
        Me.cmbChannelPartner.ItemHeight = 15
        Me.cmbChannelPartner.Location = New System.Drawing.Point(378, 3)
        Me.cmbChannelPartner.MatchEntryTimeout = CType(2000, Long)
        Me.cmbChannelPartner.MaxDropDownItems = CType(5, Short)
        Me.cmbChannelPartner.MaxLength = 32767
        Me.cmbChannelPartner.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cmbChannelPartner.MoveToNxtCtrl = Nothing
        Me.cmbChannelPartner.Name = "cmbChannelPartner"
        Me.cmbChannelPartner.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cmbChannelPartner.Size = New System.Drawing.Size(153, 21)
        Me.cmbChannelPartner.strSelectStmt = ""
        Me.cmbChannelPartner.TabIndex = 36
        Me.cmbChannelPartner.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cmbChannelPartner.PropBag = resources.GetString("cmbChannelPartner.PropBag")
        '
        'txtDeliveryId
        '
        Me.txtDeliveryId.AutoSize = False
        Me.txtDeliveryId.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtDeliveryId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeliveryId.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDeliveryId.Location = New System.Drawing.Point(648, 3)
        Me.txtDeliveryId.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtDeliveryId.MoveToNxtCtrl = Nothing
        Me.txtDeliveryId.Name = "txtDeliveryId"
        Me.txtDeliveryId.Size = New System.Drawing.Size(172, 28)
        Me.txtDeliveryId.TabIndex = 6
        Me.txtDeliveryId.Tag = Nothing
        Me.txtDeliveryId.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtDeliveryId.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdRefreshGrid
        '
        Me.cmdRefreshGrid.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmdRefreshGrid.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdRefreshGrid.Location = New System.Drawing.Point(916, 3)
        Me.cmdRefreshGrid.MoveToNxtCtrl = Nothing
        Me.cmdRefreshGrid.Name = "cmdRefreshGrid"
        Me.cmdRefreshGrid.SetArticleCode = Nothing
        Me.cmdRefreshGrid.SetRowIndex = 0
        Me.cmdRefreshGrid.Size = New System.Drawing.Size(82, 28)
        Me.cmdRefreshGrid.TabIndex = 2
        Me.cmdRefreshGrid.Text = "Refresh"
        Me.cmdRefreshGrid.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdRefreshGrid.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdRefreshGrid.UseVisualStyleBackColor = True
        Me.cmdRefreshGrid.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'frmHD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1007, 530)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.KeyPreview = True
        Me.Name = "frmHD"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Dispatch And Delivery"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.grdCreditSales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.topButtonPanel.ResumeLayout(False)
        Me.topButtonPanel.PerformLayout()
        CType(Me.lblsearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtToDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtFromDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToDeliveryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromDeliveryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOrderStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblChannelPartner, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbOrderStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDeliveryPerson, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbChannelPartner, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeliveryId, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grdCreditSales As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents topButtonPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblsearch As Spectrum.CtrlLabel
    Friend WithEvents cmdPrint As Spectrum.CtrlBtn
    Friend WithEvents cmdDispatch As Spectrum.CtrlBtn
    Friend WithEvents lblChannelPartner As Spectrum.CtrlLabel
    Friend WithEvents btnRejectOrder As Spectrum.CtrlBtn
    Friend WithEvents lblDeliveryPerson As Spectrum.CtrlLabel
    Friend WithEvents txtDeliveryId As Spectrum.CtrlTextBox
    Friend WithEvents cmdAccept As Spectrum.CtrlBtn
    Friend WithEvents CmdViewOrder As Spectrum.CtrlBtn
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents txtFilterCreditSales As System.Windows.Forms.TextBox
    Friend WithEvents lblOrderStatus As Spectrum.CtrlLabel
    Friend WithEvents cmbOrderStatus As Spectrum.ctrlCombo
    Friend WithEvents cmbChannelPartner As Spectrum.ctrlCombo
    Friend WithEvents lblFromDeliveryDate As Spectrum.CtrlLabel
    Friend WithEvents DtFromDate As Spectrum.ctrlDate
    Friend WithEvents lblToDeliveryDate As Spectrum.CtrlLabel
    Friend WithEvents DtToDate As Spectrum.ctrlDate
    Friend WithEvents BtnFilterSearch As Spectrum.CtrlBtn
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cmdRefreshGrid As Spectrum.CtrlBtn
End Class
