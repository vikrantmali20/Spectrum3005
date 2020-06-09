<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrlSalesInfo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrlSalesInfo))
        Me.C1Sizer1 = New C1.Win.C1Sizer.C1Sizer()
        Me.CtrlDtExpDelDate = New Spectrum.ctrlDate()
        Me.CtrlTxtInvoice = New Spectrum.CtrlTextBox()
        Me.lblInvoiceTo = New Spectrum.CtrlLabel()
        Me.CtrlBtn1 = New Spectrum.CtrlBtn()
        Me.CtrlTxtCustOrdRef = New Spectrum.CtrlTextBox()
        Me.CtrlTxtRemarks = New Spectrum.CtrlTextBox()
        Me.CtrldtOrderDt = New Spectrum.ctrlDate()
        Me.CtrlTxtOrderNo = New Spectrum.CtrlTextBox()
        Me.lblSalesOrderNo = New Spectrum.CtrlLabel()
        Me.lblOrderDate = New Spectrum.CtrlLabel()
        Me.lblEpectedDeliveryDt = New Spectrum.CtrlLabel()
        Me.lblRemarks = New Spectrum.CtrlLabel()
        Me.lblOrderRef = New Spectrum.CtrlLabel()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer1.SuspendLayout()
        CType(Me.CtrlDtExpDelDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlTxtInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblInvoiceTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlTxtCustOrdRef, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlTxtRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrldtOrderDt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlTxtOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalesOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOrderDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEpectedDeliveryDt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOrderRef, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'C1Sizer1
        '
        Me.C1Sizer1.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1Sizer1.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1Sizer1.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1Sizer1.Controls.Add(Me.CtrlDtExpDelDate)
        Me.C1Sizer1.Controls.Add(Me.CtrlTxtInvoice)
        Me.C1Sizer1.Controls.Add(Me.lblInvoiceTo)
        Me.C1Sizer1.Controls.Add(Me.CtrlBtn1)
        Me.C1Sizer1.Controls.Add(Me.CtrlTxtCustOrdRef)
        Me.C1Sizer1.Controls.Add(Me.CtrlTxtRemarks)
        Me.C1Sizer1.Controls.Add(Me.CtrldtOrderDt)
        Me.C1Sizer1.Controls.Add(Me.CtrlTxtOrderNo)
        Me.C1Sizer1.Controls.Add(Me.lblSalesOrderNo)
        Me.C1Sizer1.Controls.Add(Me.lblOrderDate)
        Me.C1Sizer1.Controls.Add(Me.lblEpectedDeliveryDt)
        Me.C1Sizer1.Controls.Add(Me.lblRemarks)
        Me.C1Sizer1.Controls.Add(Me.lblOrderRef)
        Me.C1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Sizer1.GridDefinition = resources.GetString("C1Sizer1.GridDefinition")
        Me.C1Sizer1.Location = New System.Drawing.Point(0, 0)
        Me.C1Sizer1.Margin = New System.Windows.Forms.Padding(0)
        Me.C1Sizer1.Name = "C1Sizer1"
        Me.C1Sizer1.Size = New System.Drawing.Size(327, 150)
        Me.C1Sizer1.SplitterWidth = 1
        Me.C1Sizer1.TabIndex = 0
        Me.C1Sizer1.Tag = "v"
        Me.C1Sizer1.Text = "C1Sizer2"
        '
        'CtrlDtExpDelDate
        '
        Me.CtrlDtExpDelDate.AutoSize = False
        Me.CtrlDtExpDelDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.CtrlDtExpDelDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        '
        '
        '
        Me.CtrlDtExpDelDate.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.CtrlDtExpDelDate.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlDtExpDelDate.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlDtExpDelDate.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDateShortTime
        Me.CtrlDtExpDelDate.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.CtrlDtExpDelDate.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDateShortTime
        Me.CtrlDtExpDelDate.Location = New System.Drawing.Point(99, 53)
        Me.CtrlDtExpDelDate.Margin = New System.Windows.Forms.Padding(1)
        Me.CtrlDtExpDelDate.Name = "CtrlDtExpDelDate"
        Me.CtrlDtExpDelDate.Size = New System.Drawing.Size(223, 23)
        Me.CtrlDtExpDelDate.TabIndex = 3
        Me.CtrlDtExpDelDate.Tag = Nothing
        Me.CtrlDtExpDelDate.TrimStart = True
        Me.CtrlDtExpDelDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        Me.CtrlDtExpDelDate.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlDtExpDelDate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlTxtInvoice
        '
        Me.CtrlTxtInvoice.AutoSize = False
        Me.CtrlTxtInvoice.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.CtrlTxtInvoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlTxtInvoice.Label = Me.lblInvoiceTo
        Me.CtrlTxtInvoice.Location = New System.Drawing.Point(99, 123)
        Me.CtrlTxtInvoice.Margin = New System.Windows.Forms.Padding(1)
        Me.CtrlTxtInvoice.MinimumSize = New System.Drawing.Size(10, 21)
        Me.CtrlTxtInvoice.Name = "CtrlTxtInvoice"
        Me.CtrlTxtInvoice.Size = New System.Drawing.Size(223, 22)
        Me.CtrlTxtInvoice.TabIndex = 6
        Me.CtrlTxtInvoice.Tag = Nothing
        Me.CtrlTxtInvoice.TextDetached = True
        Me.CtrlTxtInvoice.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlTxtInvoice.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblInvoiceTo
        '
        Me.lblInvoiceTo.AttachedTextBoxName = Nothing
        Me.lblInvoiceTo.BackColor = System.Drawing.SystemColors.Control
        Me.lblInvoiceTo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblInvoiceTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInvoiceTo.Location = New System.Drawing.Point(5, 123)
        Me.lblInvoiceTo.Margin = New System.Windows.Forms.Padding(1)
        Me.lblInvoiceTo.Name = "lblInvoiceTo"
        Me.lblInvoiceTo.Size = New System.Drawing.Size(93, 22)
        Me.lblInvoiceTo.TabIndex = 12
        Me.lblInvoiceTo.Tag = Nothing
        Me.lblInvoiceTo.Text = "Invoice To"
        Me.lblInvoiceTo.TextDetached = True
        '
        'CtrlBtn1
        '
        Me.CtrlBtn1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CtrlBtn1.Image = Global.Spectrum.My.Resources.Resources.search_2
        Me.CtrlBtn1.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.CtrlBtn1.Location = New System.Drawing.Point(293, 5)
        Me.CtrlBtn1.Name = "CtrlBtn1"
        Me.CtrlBtn1.SetArticleCode = Nothing
        Me.CtrlBtn1.SetRowIndex = 0
        Me.CtrlBtn1.Size = New System.Drawing.Size(29, 23)
        Me.CtrlBtn1.TabIndex = 1
        Me.CtrlBtn1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CtrlBtn1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CtrlBtn1.UseVisualStyleBackColor = True
        Me.CtrlBtn1.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlBtn1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlTxtCustOrdRef
        '
        Me.CtrlTxtCustOrdRef.AutoSize = False
        Me.CtrlTxtCustOrdRef.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.CtrlTxtCustOrdRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlTxtCustOrdRef.Location = New System.Drawing.Point(99, 77)
        Me.CtrlTxtCustOrdRef.Margin = New System.Windows.Forms.Padding(1)
        Me.CtrlTxtCustOrdRef.MinimumSize = New System.Drawing.Size(10, 21)
        Me.CtrlTxtCustOrdRef.Name = "CtrlTxtCustOrdRef"
        Me.CtrlTxtCustOrdRef.Size = New System.Drawing.Size(223, 22)
        Me.CtrlTxtCustOrdRef.TabIndex = 4
        Me.CtrlTxtCustOrdRef.Tag = Nothing
        Me.CtrlTxtCustOrdRef.TextDetached = True
        Me.CtrlTxtCustOrdRef.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlTxtCustOrdRef.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlTxtRemarks
        '
        Me.CtrlTxtRemarks.AutoSize = False
        Me.CtrlTxtRemarks.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.CtrlTxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlTxtRemarks.Location = New System.Drawing.Point(99, 100)
        Me.CtrlTxtRemarks.Margin = New System.Windows.Forms.Padding(1)
        Me.CtrlTxtRemarks.MinimumSize = New System.Drawing.Size(10, 21)
        Me.CtrlTxtRemarks.Multiline = True
        Me.CtrlTxtRemarks.Name = "CtrlTxtRemarks"
        Me.CtrlTxtRemarks.Size = New System.Drawing.Size(223, 22)
        Me.CtrlTxtRemarks.TabIndex = 5
        Me.CtrlTxtRemarks.Tag = Nothing
        Me.CtrlTxtRemarks.TextDetached = True
        Me.CtrlTxtRemarks.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlTxtRemarks.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrldtOrderDt
        '
        Me.CtrldtOrderDt.AutoSize = False
        Me.CtrldtOrderDt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.CtrldtOrderDt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        '
        '
        '
        Me.CtrldtOrderDt.Calendar.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.CtrldtOrderDt.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrldtOrderDt.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrldtOrderDt.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.CtrldtOrderDt.EditFormat.Inherit = CType(((((C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat Or C1.Win.C1Input.FormatInfoInheritFlags.NullText) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.EmptyAsNull) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimStart) _
            Or C1.Win.C1Input.FormatInfoInheritFlags.TrimEnd), C1.Win.C1Input.FormatInfoInheritFlags)
        Me.CtrldtOrderDt.FormatType = C1.Win.C1Input.FormatTypeEnum.ShortDate
        Me.CtrldtOrderDt.Location = New System.Drawing.Point(99, 29)
        Me.CtrldtOrderDt.Margin = New System.Windows.Forms.Padding(1)
        Me.CtrldtOrderDt.Name = "CtrldtOrderDt"
        Me.CtrldtOrderDt.Size = New System.Drawing.Size(223, 23)
        Me.CtrldtOrderDt.TabIndex = 2
        Me.CtrldtOrderDt.Tag = Nothing
        Me.CtrldtOrderDt.TrimStart = True
        Me.CtrldtOrderDt.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
        Me.CtrldtOrderDt.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrldtOrderDt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlTxtOrderNo
        '
        Me.CtrlTxtOrderNo.AutoSize = False
        Me.CtrlTxtOrderNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.CtrlTxtOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlTxtOrderNo.Location = New System.Drawing.Point(99, 5)
        Me.CtrlTxtOrderNo.Margin = New System.Windows.Forms.Padding(1)
        Me.CtrlTxtOrderNo.MinimumSize = New System.Drawing.Size(10, 21)
        Me.CtrlTxtOrderNo.Name = "CtrlTxtOrderNo"
        Me.CtrlTxtOrderNo.Size = New System.Drawing.Size(193, 23)
        Me.CtrlTxtOrderNo.TabIndex = 0
        Me.CtrlTxtOrderNo.Tag = "NO"
        Me.CtrlTxtOrderNo.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.CtrlTxtOrderNo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblSalesOrderNo
        '
        Me.lblSalesOrderNo.AttachedTextBoxName = Nothing
        Me.lblSalesOrderNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblSalesOrderNo.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblSalesOrderNo.Location = New System.Drawing.Point(5, 5)
        Me.lblSalesOrderNo.Margin = New System.Windows.Forms.Padding(1)
        Me.lblSalesOrderNo.Name = "lblSalesOrderNo"
        Me.lblSalesOrderNo.Size = New System.Drawing.Size(93, 23)
        Me.lblSalesOrderNo.TabIndex = 7
        Me.lblSalesOrderNo.Tag = Nothing
        Me.lblSalesOrderNo.Text = "Order No."
        Me.lblSalesOrderNo.TextDetached = True
        '
        'lblOrderDate
        '
        Me.lblOrderDate.AttachedTextBoxName = Nothing
        Me.lblOrderDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblOrderDate.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblOrderDate.Location = New System.Drawing.Point(5, 29)
        Me.lblOrderDate.Margin = New System.Windows.Forms.Padding(1)
        Me.lblOrderDate.Name = "lblOrderDate"
        Me.lblOrderDate.Size = New System.Drawing.Size(93, 23)
        Me.lblOrderDate.TabIndex = 8
        Me.lblOrderDate.Tag = Nothing
        Me.lblOrderDate.Text = "Order Date"
        Me.lblOrderDate.TextDetached = True
        '
        'lblEpectedDeliveryDt
        '
        Me.lblEpectedDeliveryDt.AttachedTextBoxName = Nothing
        Me.lblEpectedDeliveryDt.BackColor = System.Drawing.SystemColors.Control
        Me.lblEpectedDeliveryDt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblEpectedDeliveryDt.Location = New System.Drawing.Point(5, 53)
        Me.lblEpectedDeliveryDt.Margin = New System.Windows.Forms.Padding(1)
        Me.lblEpectedDeliveryDt.Name = "lblEpectedDeliveryDt"
        Me.lblEpectedDeliveryDt.Size = New System.Drawing.Size(93, 23)
        Me.lblEpectedDeliveryDt.TabIndex = 9
        Me.lblEpectedDeliveryDt.Tag = Nothing
        Me.lblEpectedDeliveryDt.Text = "Expct Delivery Dt"
        Me.lblEpectedDeliveryDt.TextDetached = True
        '
        'lblRemarks
        '
        Me.lblRemarks.AttachedTextBoxName = Nothing
        Me.lblRemarks.BackColor = System.Drawing.SystemColors.Control
        Me.lblRemarks.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblRemarks.Location = New System.Drawing.Point(5, 100)
        Me.lblRemarks.Margin = New System.Windows.Forms.Padding(1)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(93, 22)
        Me.lblRemarks.TabIndex = 11
        Me.lblRemarks.Tag = Nothing
        Me.lblRemarks.Text = "Remarks"
        Me.lblRemarks.TextDetached = True
        '
        'lblOrderRef
        '
        Me.lblOrderRef.AttachedTextBoxName = Nothing
        Me.lblOrderRef.BackColor = System.Drawing.SystemColors.Control
        Me.lblOrderRef.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblOrderRef.Location = New System.Drawing.Point(5, 77)
        Me.lblOrderRef.Margin = New System.Windows.Forms.Padding(1)
        Me.lblOrderRef.Name = "lblOrderRef"
        Me.lblOrderRef.Size = New System.Drawing.Size(93, 22)
        Me.lblOrderRef.TabIndex = 10
        Me.lblOrderRef.Tag = Nothing
        Me.lblOrderRef.Text = "Cust Order Ref"
        Me.lblOrderRef.TextDetached = True
        Me.lblOrderRef.Value = ""
        '
        'CtrlSalesInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.C1Sizer1)
        Me.Name = "CtrlSalesInfo"
        Me.Size = New System.Drawing.Size(327, 150)
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer1.ResumeLayout(False)
        CType(Me.CtrlDtExpDelDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlTxtInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblInvoiceTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlTxtCustOrdRef, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlTxtRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrldtOrderDt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlTxtOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalesOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOrderDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEpectedDeliveryDt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOrderRef, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents C1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents lblSalesOrderNo As Spectrum.CtrlLabel
    Friend WithEvents lblOrderDate As Spectrum.CtrlLabel
    Friend WithEvents lblEpectedDeliveryDt As Spectrum.CtrlLabel
    Friend WithEvents lblRemarks As Spectrum.CtrlLabel
    Friend WithEvents lblOrderRef As Spectrum.CtrlLabel
    Friend WithEvents CtrlTxtOrderNo As Spectrum.CtrlTextBox
    Friend WithEvents CtrldtOrderDt As Spectrum.ctrlDate
    Friend WithEvents CtrlTxtRemarks As Spectrum.CtrlTextBox
    Friend WithEvents CtrlTxtCustOrdRef As Spectrum.CtrlTextBox
    Friend WithEvents CtrlBtn1 As Spectrum.CtrlBtn
    Friend WithEvents CtrlTxtInvoice As Spectrum.CtrlTextBox
    Friend WithEvents lblInvoiceTo As Spectrum.CtrlLabel
    Friend WithEvents CtrlDtExpDelDate As Spectrum.ctrlDate

End Class
