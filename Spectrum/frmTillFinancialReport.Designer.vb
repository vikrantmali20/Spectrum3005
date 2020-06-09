<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTillFinancialReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTillFinancialReport))
        Me.sizTop = New C1.Win.C1Sizer.C1Sizer()
        Me.cmdDispCash = New Spectrum.CtrlBtn()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblPettyCashRecAmt = New Spectrum.CtrlLabel()
        Me.lbltotalCashCollection = New Spectrum.CtrlLabel()
        Me.lblPettyCashRec = New Spectrum.CtrlLabel()
        Me.lblTotalCashCollectionAmt = New Spectrum.CtrlLabel()
        Me.lblPettyCashExpAmt = New Spectrum.CtrlLabel()
        Me.lblPettyCashExp = New Spectrum.CtrlLabel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblCreditsaleAmt = New Spectrum.CtrlLabel()
        Me.CtrlLabel4 = New Spectrum.CtrlLabel()
        Me.lblTotalCollection = New Spectrum.CtrlLabel()
        Me.lblCreditSale = New Spectrum.CtrlLabel()
        Me.lblHeader = New Spectrum.CtrlLabel()
        Me.CtrlLabel3 = New Spectrum.CtrlLabel()
        Me.dgIssued = New Spectrum.CtrlGrid()
        Me.lblTotalCash = New Spectrum.CtrlLabel()
        Me.CtrlLabel7 = New Spectrum.CtrlLabel()
        Me.CmdNext = New Spectrum.CtrlBtn()
        Me.CmdPrint = New Spectrum.CtrlBtn()
        Me.lblFinalCollection = New Spectrum.CtrlLabel()
        Me.CtrlLabel6 = New Spectrum.CtrlLabel()
        Me.dgPayments = New Spectrum.CtrlGrid()
        Me.CtrlLabel1 = New Spectrum.CtrlLabel()
        Me.dgCash = New Spectrum.CtrlGrid()
        Me.CtrlLabel2 = New Spectrum.CtrlLabel()
        Me.txtOperations = New Spectrum.CtrlTextBox()
        Me.lblOperation = New Spectrum.CtrlLabel()
        Me.cmdBrandWiseSale = New Spectrum.CtrlBtn()
        CType(Me.sizTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sizTop.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.lblPettyCashRecAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbltotalCashCollection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPettyCashRec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalCashCollectionAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPettyCashExpAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPettyCashExp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.lblCreditsaleAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalCollection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreditSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgIssued, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalCash, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFinalCollection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgCash, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtOperations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblOperation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sizTop
        '
        Me.sizTop.Border.Color = System.Drawing.Color.LightSlateGray
        Me.sizTop.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.sizTop.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.sizTop.Controls.Add(Me.cmdBrandWiseSale)
        Me.sizTop.Controls.Add(Me.cmdDispCash)
        Me.sizTop.Controls.Add(Me.FlowLayoutPanel1)
        Me.sizTop.Controls.Add(Me.lblHeader)
        Me.sizTop.Controls.Add(Me.CtrlLabel3)
        Me.sizTop.Controls.Add(Me.dgIssued)
        Me.sizTop.Controls.Add(Me.lblTotalCash)
        Me.sizTop.Controls.Add(Me.CtrlLabel7)
        Me.sizTop.Controls.Add(Me.CmdNext)
        Me.sizTop.Controls.Add(Me.CmdPrint)
        Me.sizTop.Controls.Add(Me.lblFinalCollection)
        Me.sizTop.Controls.Add(Me.CtrlLabel6)
        Me.sizTop.Controls.Add(Me.dgPayments)
        Me.sizTop.Controls.Add(Me.CtrlLabel1)
        Me.sizTop.Controls.Add(Me.dgCash)
        Me.sizTop.Controls.Add(Me.CtrlLabel2)
        Me.sizTop.Controls.Add(Me.txtOperations)
        Me.sizTop.Controls.Add(Me.lblOperation)
        Me.sizTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sizTop.GridDefinition = "97.9633401221996:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "98.7373737373737:False:False;"
        Me.sizTop.Location = New System.Drawing.Point(0, 0)
        Me.sizTop.Name = "sizTop"
        Me.sizTop.Size = New System.Drawing.Size(792, 491)
        Me.sizTop.TabIndex = 1
        Me.sizTop.Text = "C1Sizer1"
        '
        'cmdDispCash
        '
        Me.cmdDispCash.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdDispCash.Location = New System.Drawing.Point(14, 398)
        Me.cmdDispCash.MoveToNxtCtrl = Nothing
        Me.cmdDispCash.Name = "cmdDispCash"
        Me.cmdDispCash.SetArticleCode = Nothing
        Me.cmdDispCash.SetRowIndex = 0
        Me.cmdDispCash.Size = New System.Drawing.Size(125, 37)
        Me.cmdDispCash.TabIndex = 25
        Me.cmdDispCash.Text = "&Display Cash"
        Me.cmdDispCash.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdDispCash.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdDispCash.UseVisualStyleBackColor = True
        Me.cmdDispCash.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.TableLayoutPanel1)
        Me.FlowLayoutPanel1.Controls.Add(Me.TableLayoutPanel2)
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(13, 290)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(382, 95)
        Me.FlowLayoutPanel1.TabIndex = 24
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblPettyCashRecAmt, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lbltotalCashCollection, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblPettyCashRec, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTotalCashCollectionAmt, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblPettyCashExpAmt, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblPettyCashExp, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(379, 53)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'lblPettyCashRecAmt
        '
        Me.lblPettyCashRecAmt.AttachedTextBoxName = Nothing
        Me.lblPettyCashRecAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblPettyCashRecAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblPettyCashRecAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPettyCashRecAmt.ForeColor = System.Drawing.Color.Black
        Me.lblPettyCashRecAmt.Location = New System.Drawing.Point(192, 0)
        Me.lblPettyCashRecAmt.Name = "lblPettyCashRecAmt"
        Me.lblPettyCashRecAmt.Size = New System.Drawing.Size(117, 15)
        Me.lblPettyCashRecAmt.TabIndex = 20
        Me.lblPettyCashRecAmt.Tag = Nothing
        Me.lblPettyCashRecAmt.Text = "0.00"
        Me.lblPettyCashRecAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblPettyCashRecAmt.TextDetached = True
        Me.lblPettyCashRecAmt.Visible = False
        Me.lblPettyCashRecAmt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lbltotalCashCollection
        '
        Me.lbltotalCashCollection.AttachedTextBoxName = Nothing
        Me.lbltotalCashCollection.BackColor = System.Drawing.Color.Transparent
        Me.lbltotalCashCollection.BorderColor = System.Drawing.Color.Transparent
        Me.lbltotalCashCollection.ForeColor = System.Drawing.Color.Black
        Me.lbltotalCashCollection.Location = New System.Drawing.Point(3, 34)
        Me.lbltotalCashCollection.Name = "lbltotalCashCollection"
        Me.lbltotalCashCollection.Size = New System.Drawing.Size(155, 13)
        Me.lbltotalCashCollection.TabIndex = 22
        Me.lbltotalCashCollection.Tag = Nothing
        Me.lbltotalCashCollection.Text = "Total Cash "
        Me.lbltotalCashCollection.TextDetached = True
        Me.lbltotalCashCollection.Visible = False
        Me.lbltotalCashCollection.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblPettyCashRec
        '
        Me.lblPettyCashRec.AttachedTextBoxName = Nothing
        Me.lblPettyCashRec.BackColor = System.Drawing.Color.Transparent
        Me.lblPettyCashRec.BorderColor = System.Drawing.Color.Transparent
        Me.lblPettyCashRec.ForeColor = System.Drawing.Color.Black
        Me.lblPettyCashRec.Location = New System.Drawing.Point(3, 0)
        Me.lblPettyCashRec.Name = "lblPettyCashRec"
        Me.lblPettyCashRec.Size = New System.Drawing.Size(183, 13)
        Me.lblPettyCashRec.TabIndex = 18
        Me.lblPettyCashRec.Tag = Nothing
        Me.lblPettyCashRec.Text = "PettyCashRec"
        Me.lblPettyCashRec.TextDetached = True
        Me.lblPettyCashRec.Visible = False
        Me.lblPettyCashRec.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblTotalCashCollectionAmt
        '
        Me.lblTotalCashCollectionAmt.AttachedTextBoxName = Nothing
        Me.lblTotalCashCollectionAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTotalCashCollectionAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTotalCashCollectionAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalCashCollectionAmt.ForeColor = System.Drawing.Color.Black
        Me.lblTotalCashCollectionAmt.Location = New System.Drawing.Point(192, 34)
        Me.lblTotalCashCollectionAmt.Name = "lblTotalCashCollectionAmt"
        Me.lblTotalCashCollectionAmt.Size = New System.Drawing.Size(117, 15)
        Me.lblTotalCashCollectionAmt.TabIndex = 25
        Me.lblTotalCashCollectionAmt.Tag = Nothing
        Me.lblTotalCashCollectionAmt.Text = "0.00"
        Me.lblTotalCashCollectionAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTotalCashCollectionAmt.TextDetached = True
        Me.lblTotalCashCollectionAmt.Visible = False
        Me.lblTotalCashCollectionAmt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblPettyCashExpAmt
        '
        Me.lblPettyCashExpAmt.AttachedTextBoxName = Nothing
        Me.lblPettyCashExpAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblPettyCashExpAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblPettyCashExpAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPettyCashExpAmt.ForeColor = System.Drawing.Color.Black
        Me.lblPettyCashExpAmt.Location = New System.Drawing.Point(192, 17)
        Me.lblPettyCashExpAmt.Name = "lblPettyCashExpAmt"
        Me.lblPettyCashExpAmt.Size = New System.Drawing.Size(117, 15)
        Me.lblPettyCashExpAmt.TabIndex = 21
        Me.lblPettyCashExpAmt.Tag = Nothing
        Me.lblPettyCashExpAmt.Text = "0.00"
        Me.lblPettyCashExpAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblPettyCashExpAmt.TextDetached = True
        Me.lblPettyCashExpAmt.Visible = False
        Me.lblPettyCashExpAmt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblPettyCashExp
        '
        Me.lblPettyCashExp.AttachedTextBoxName = Nothing
        Me.lblPettyCashExp.BackColor = System.Drawing.Color.Transparent
        Me.lblPettyCashExp.BorderColor = System.Drawing.Color.Transparent
        Me.lblPettyCashExp.ForeColor = System.Drawing.Color.Black
        Me.lblPettyCashExp.Location = New System.Drawing.Point(3, 17)
        Me.lblPettyCashExp.Name = "lblPettyCashExp"
        Me.lblPettyCashExp.Size = New System.Drawing.Size(183, 13)
        Me.lblPettyCashExp.TabIndex = 19
        Me.lblPettyCashExp.Tag = Nothing
        Me.lblPettyCashExp.Text = "Petty Cash Ex"
        Me.lblPettyCashExp.TextDetached = True
        Me.lblPettyCashExp.Visible = False
        Me.lblPettyCashExp.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.lblCreditsaleAmt, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.CtrlLabel4, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblTotalCollection, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.lblCreditSale, 0, 1)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 62)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(379, 31)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'lblCreditsaleAmt
        '
        Me.lblCreditsaleAmt.AttachedTextBoxName = Nothing
        Me.lblCreditsaleAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCreditsaleAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCreditsaleAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCreditsaleAmt.ForeColor = System.Drawing.Color.Black
        Me.lblCreditsaleAmt.Location = New System.Drawing.Point(192, 15)
        Me.lblCreditsaleAmt.Name = "lblCreditsaleAmt"
        Me.lblCreditsaleAmt.Size = New System.Drawing.Size(117, 15)
        Me.lblCreditsaleAmt.TabIndex = 23
        Me.lblCreditsaleAmt.Tag = Nothing
        Me.lblCreditsaleAmt.Text = "0.00"
        Me.lblCreditsaleAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCreditsaleAmt.TextDetached = True
        Me.lblCreditsaleAmt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel4
        '
        Me.CtrlLabel4.AttachedTextBoxName = Nothing
        Me.CtrlLabel4.AutoSize = True
        Me.CtrlLabel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel4.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel4.Location = New System.Drawing.Point(3, 0)
        Me.CtrlLabel4.Name = "CtrlLabel4"
        Me.CtrlLabel4.Size = New System.Drawing.Size(89, 15)
        Me.CtrlLabel4.TabIndex = 7
        Me.CtrlLabel4.Tag = Nothing
        Me.CtrlLabel4.Text = "Till Collection "
        Me.CtrlLabel4.TextDetached = True
        Me.CtrlLabel4.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblTotalCollection
        '
        Me.lblTotalCollection.AttachedTextBoxName = Nothing
        Me.lblTotalCollection.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTotalCollection.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTotalCollection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalCollection.ForeColor = System.Drawing.Color.Black
        Me.lblTotalCollection.Location = New System.Drawing.Point(192, 0)
        Me.lblTotalCollection.Name = "lblTotalCollection"
        Me.lblTotalCollection.Size = New System.Drawing.Size(117, 15)
        Me.lblTotalCollection.TabIndex = 8
        Me.lblTotalCollection.Tag = Nothing
        Me.lblTotalCollection.Text = "0.00"
        Me.lblTotalCollection.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTotalCollection.TextDetached = True
        Me.lblTotalCollection.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCreditSale
        '
        Me.lblCreditSale.AttachedTextBoxName = Nothing
        Me.lblCreditSale.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCreditSale.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCreditSale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCreditSale.ForeColor = System.Drawing.Color.Black
        Me.lblCreditSale.Location = New System.Drawing.Point(3, 15)
        Me.lblCreditSale.Name = "lblCreditSale"
        Me.lblCreditSale.Size = New System.Drawing.Size(183, 15)
        Me.lblCreditSale.TabIndex = 22
        Me.lblCreditSale.Tag = Nothing
        Me.lblCreditSale.Text = "Credit Sale"
        Me.lblCreditSale.TextDetached = True
        Me.lblCreditSale.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblHeader
        '
        Me.lblHeader.AttachedTextBoxName = Nothing
        Me.lblHeader.AutoSize = True
        Me.lblHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblHeader.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(13, 9)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(431, 18)
        Me.lblHeader.TabIndex = 17
        Me.lblHeader.Tag = Nothing
        Me.lblHeader.Text = "Till close is for Site Abc, Terminal 001  and Date 10th Aug 2012"
        Me.lblHeader.TextDetached = True
        Me.lblHeader.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel3
        '
        Me.CtrlLabel3.AttachedTextBoxName = Nothing
        Me.CtrlLabel3.AutoSize = True
        Me.CtrlLabel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel3.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel3.Location = New System.Drawing.Point(427, 246)
        Me.CtrlLabel3.Name = "CtrlLabel3"
        Me.CtrlLabel3.Size = New System.Drawing.Size(99, 15)
        Me.CtrlLabel3.TabIndex = 16
        Me.CtrlLabel3.Tag = Nothing
        Me.CtrlLabel3.Text = "Amount Issued."
        Me.CtrlLabel3.TextDetached = True
        Me.CtrlLabel3.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'dgIssued
        '
        Me.dgIssued.AllowEditing = False
        Me.dgIssued.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.dgIssued.CellButtonImage = CType(resources.GetObject("dgIssued.CellButtonImage"), System.Drawing.Image)
        Me.dgIssued.ColumnInfo = "2,0,0,0,0,100,Columns:0{Width:139;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.dgIssued.ExtendLastCol = True
        Me.dgIssued.Location = New System.Drawing.Point(427, 264)
        Me.dgIssued.Name = "dgIssued"
        Me.dgIssued.Rows.Count = 2
        Me.dgIssued.Rows.DefaultSize = 20
        Me.dgIssued.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgIssued.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.dgIssued.Size = New System.Drawing.Size(359, 160)
        Me.dgIssued.StyleInfo = resources.GetString("dgIssued.StyleInfo")
        Me.dgIssued.TabIndex = 15
        Me.dgIssued.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'lblTotalCash
        '
        Me.lblTotalCash.AttachedTextBoxName = Nothing
        Me.lblTotalCash.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTotalCash.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTotalCash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalCash.ForeColor = System.Drawing.Color.Black
        Me.lblTotalCash.Location = New System.Drawing.Point(661, 215)
        Me.lblTotalCash.Name = "lblTotalCash"
        Me.lblTotalCash.Size = New System.Drawing.Size(117, 15)
        Me.lblTotalCash.TabIndex = 14
        Me.lblTotalCash.Tag = Nothing
        Me.lblTotalCash.Text = "0.00"
        Me.lblTotalCash.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTotalCash.TextDetached = True
        Me.lblTotalCash.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel7
        '
        Me.CtrlLabel7.AttachedTextBoxName = Nothing
        Me.CtrlLabel7.AutoSize = True
        Me.CtrlLabel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel7.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel7.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel7.Location = New System.Drawing.Point(427, 215)
        Me.CtrlLabel7.Name = "CtrlLabel7"
        Me.CtrlLabel7.Size = New System.Drawing.Size(120, 15)
        Me.CtrlLabel7.TabIndex = 13
        Me.CtrlLabel7.Tag = Nothing
        Me.CtrlLabel7.Text = "Total cash collected"
        Me.CtrlLabel7.TextDetached = True
        Me.CtrlLabel7.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CmdNext
        '
        Me.CmdNext.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdNext.Location = New System.Drawing.Point(271, 397)
        Me.CmdNext.MoveToNxtCtrl = Nothing
        Me.CmdNext.Name = "CmdNext"
        Me.CmdNext.SetArticleCode = Nothing
        Me.CmdNext.SetRowIndex = 0
        Me.CmdNext.Size = New System.Drawing.Size(124, 37)
        Me.CmdNext.TabIndex = 12
        Me.CmdNext.Text = "&Continue"
        Me.CmdNext.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CmdNext.UseVisualStyleBackColor = True
        Me.CmdNext.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CmdPrint
        '
        Me.CmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdPrint.Location = New System.Drawing.Point(143, 398)
        Me.CmdPrint.MoveToNxtCtrl = Nothing
        Me.CmdPrint.Name = "CmdPrint"
        Me.CmdPrint.SetArticleCode = Nothing
        Me.CmdPrint.SetRowIndex = 0
        Me.CmdPrint.Size = New System.Drawing.Size(123, 37)
        Me.CmdPrint.TabIndex = 11
        Me.CmdPrint.Text = "&Print"
        Me.CmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CmdPrint.UseVisualStyleBackColor = True
        Me.CmdPrint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblFinalCollection
        '
        Me.lblFinalCollection.AttachedTextBoxName = Nothing
        Me.lblFinalCollection.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblFinalCollection.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblFinalCollection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFinalCollection.ForeColor = System.Drawing.Color.Black
        Me.lblFinalCollection.Location = New System.Drawing.Point(155, 413)
        Me.lblFinalCollection.Name = "lblFinalCollection"
        Me.lblFinalCollection.Size = New System.Drawing.Size(94, 15)
        Me.lblFinalCollection.TabIndex = 10
        Me.lblFinalCollection.Tag = Nothing
        Me.lblFinalCollection.Text = "0.00"
        Me.lblFinalCollection.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFinalCollection.TextDetached = True
        Me.lblFinalCollection.Visible = False
        Me.lblFinalCollection.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlLabel6
        '
        Me.CtrlLabel6.AttachedTextBoxName = Nothing
        Me.CtrlLabel6.AutoSize = True
        Me.CtrlLabel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel6.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel6.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel6.Location = New System.Drawing.Point(149, 415)
        Me.CtrlLabel6.Name = "CtrlLabel6"
        Me.CtrlLabel6.Size = New System.Drawing.Size(115, 15)
        Me.CtrlLabel6.TabIndex = 9
        Me.CtrlLabel6.Tag = Nothing
        Me.CtrlLabel6.Text = "Final Till Collection"
        Me.CtrlLabel6.TextDetached = True
        Me.CtrlLabel6.Visible = False
        Me.CtrlLabel6.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'dgPayments
        '
        Me.dgPayments.AllowEditing = False
        Me.dgPayments.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.dgPayments.CellButtonImage = CType(resources.GetObject("dgPayments.CellButtonImage"), System.Drawing.Image)
        Me.dgPayments.ColumnInfo = "2,0,0,0,0,100,Columns:0{Width:183;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.dgPayments.ExtendLastCol = True
        Me.dgPayments.Location = New System.Drawing.Point(12, 48)
        Me.dgPayments.Name = "dgPayments"
        Me.dgPayments.Rows.Count = 2
        Me.dgPayments.Rows.DefaultSize = 20
        Me.dgPayments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgPayments.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.dgPayments.Size = New System.Drawing.Size(383, 239)
        Me.dgPayments.StyleInfo = resources.GetString("dgPayments.StyleInfo")
        Me.dgPayments.TabIndex = 5
        Me.dgPayments.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'CtrlLabel1
        '
        Me.CtrlLabel1.AttachedTextBoxName = Nothing
        Me.CtrlLabel1.AutoSize = True
        Me.CtrlLabel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel1.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel1.Location = New System.Drawing.Point(427, 30)
        Me.CtrlLabel1.Name = "CtrlLabel1"
        Me.CtrlLabel1.Size = New System.Drawing.Size(160, 15)
        Me.CtrlLabel1.TabIndex = 4
        Me.CtrlLabel1.Tag = Nothing
        Me.CtrlLabel1.Text = "Currency wise calculation:"
        Me.CtrlLabel1.TextDetached = True
        Me.CtrlLabel1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'dgCash
        '
        Me.dgCash.AllowEditing = False
        Me.dgCash.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.dgCash.CellButtonImage = CType(resources.GetObject("dgCash.CellButtonImage"), System.Drawing.Image)
        Me.dgCash.ColumnInfo = "2,0,0,0,0,100,Columns:0{Width:139;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.dgCash.ExtendLastCol = True
        Me.dgCash.Location = New System.Drawing.Point(427, 48)
        Me.dgCash.Name = "dgCash"
        Me.dgCash.Rows.Count = 2
        Me.dgCash.Rows.DefaultSize = 20
        Me.dgCash.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgCash.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.dgCash.Size = New System.Drawing.Size(359, 160)
        Me.dgCash.StyleInfo = resources.GetString("dgCash.StyleInfo")
        Me.dgCash.TabIndex = 3
        Me.dgCash.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'CtrlLabel2
        '
        Me.CtrlLabel2.AttachedTextBoxName = Nothing
        Me.CtrlLabel2.AutoSize = True
        Me.CtrlLabel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrlLabel2.ForeColor = System.Drawing.Color.Black
        Me.CtrlLabel2.Location = New System.Drawing.Point(12, 30)
        Me.CtrlLabel2.Name = "CtrlLabel2"
        Me.CtrlLabel2.Size = New System.Drawing.Size(114, 15)
        Me.CtrlLabel2.TabIndex = 2
        Me.CtrlLabel2.Tag = Nothing
        Me.CtrlLabel2.Text = "Amount Collected."
        Me.CtrlLabel2.TextDetached = True
        Me.CtrlLabel2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'txtOperations
        '
        Me.txtOperations.AutoSize = False
        Me.txtOperations.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(187, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.txtOperations.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOperations.Location = New System.Drawing.Point(155, 390)
        Me.txtOperations.MinimumSize = New System.Drawing.Size(10, 21)
        Me.txtOperations.MoveToNxtCtrl = Nothing
        Me.txtOperations.Name = "txtOperations"
        Me.txtOperations.Size = New System.Drawing.Size(19, 23)
        Me.txtOperations.TabIndex = 1
        Me.txtOperations.Tag = Nothing
        Me.txtOperations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtOperations.Visible = False
        Me.txtOperations.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.txtOperations.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblOperation
        '
        Me.lblOperation.AttachedTextBoxName = Nothing
        Me.lblOperation.AutoSize = True
        Me.lblOperation.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblOperation.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblOperation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOperation.ForeColor = System.Drawing.Color.Black
        Me.lblOperation.Location = New System.Drawing.Point(154, 412)
        Me.lblOperation.Name = "lblOperation"
        Me.lblOperation.Size = New System.Drawing.Size(184, 15)
        Me.lblOperation.TabIndex = 0
        Me.lblOperation.Tag = Nothing
        Me.lblOperation.Text = "Enter amount paid to vendors:"
        Me.lblOperation.TextDetached = True
        Me.lblOperation.Visible = False
        Me.lblOperation.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cmdBrandWiseSale
        '
        Me.cmdBrandWiseSale.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdBrandWiseSale.Location = New System.Drawing.Point(16, 442)
        Me.cmdBrandWiseSale.MoveToNxtCtrl = Nothing
        Me.cmdBrandWiseSale.Name = "cmdBrandWiseSale"
        Me.cmdBrandWiseSale.SetArticleCode = Nothing
        Me.cmdBrandWiseSale.SetRowIndex = 0
        Me.cmdBrandWiseSale.Size = New System.Drawing.Size(125, 37)
        Me.cmdBrandWiseSale.TabIndex = 28
        Me.cmdBrandWiseSale.Text = "Brand Wise Sale"
        Me.cmdBrandWiseSale.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdBrandWiseSale.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdBrandWiseSale.UseVisualStyleBackColor = True
        '
        'frmTillFinancialReport
        '
        Me.ClientSize = New System.Drawing.Size(792, 491)
        Me.Controls.Add(Me.sizTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(800, 525)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(800, 473)
        Me.Name = "frmTillFinancialReport"
        Me.Text = "Financial Report"
        CType(Me.sizTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sizTop.ResumeLayout(False)
        Me.sizTop.PerformLayout()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.lblPettyCashRecAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbltotalCashCollection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPettyCashRec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalCashCollectionAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPettyCashExpAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPettyCashExp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.lblCreditsaleAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalCollection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreditSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgIssued, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalCash, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFinalCollection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgPayments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgCash, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtOperations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblOperation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sizTop As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents CtrlLabel2 As Spectrum.CtrlLabel
    Friend WithEvents txtOperations As Spectrum.CtrlTextBox
    Friend WithEvents lblOperation As Spectrum.CtrlLabel
    Friend WithEvents dgPayments As Spectrum.CtrlGrid
    Friend WithEvents CtrlLabel1 As Spectrum.CtrlLabel
    Friend WithEvents dgCash As Spectrum.CtrlGrid
    Friend WithEvents lblFinalCollection As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel6 As Spectrum.CtrlLabel
    Friend WithEvents lblTotalCollection As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel4 As Spectrum.CtrlLabel
    Friend WithEvents CmdNext As Spectrum.CtrlBtn
    Friend WithEvents CmdPrint As Spectrum.CtrlBtn
    Friend WithEvents lblTotalCash As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel7 As Spectrum.CtrlLabel
    Friend WithEvents CtrlLabel3 As Spectrum.CtrlLabel
    Friend WithEvents dgIssued As Spectrum.CtrlGrid
    Friend WithEvents lblHeader As Spectrum.CtrlLabel
    Friend WithEvents lblPettyCashExp As Spectrum.CtrlLabel
    Friend WithEvents lblPettyCashRec As Spectrum.CtrlLabel
    Friend WithEvents lblPettyCashExpAmt As Spectrum.CtrlLabel
    Friend WithEvents lblPettyCashRecAmt As Spectrum.CtrlLabel
    Friend WithEvents lblCreditsaleAmt As Spectrum.CtrlLabel
    Friend WithEvents lblCreditSale As Spectrum.CtrlLabel
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblTotalCashCollectionAmt As Spectrum.CtrlLabel
    Friend WithEvents lbltotalCashCollection As Spectrum.CtrlLabel
    Friend WithEvents cmdDispCash As Spectrum.CtrlBtn
    Friend WithEvents cmdBrandWiseSale As Spectrum.CtrlBtn

End Class
