﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNAcceptPayment
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
    '<System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNAcceptPayment))
        Dim ClsSiteBank1 As SpectrumBL.clsSiteBank = New SpectrumBL.clsSiteBank()
        Dim ClsSiteBank2 As SpectrumBL.clsSiteBank = New SpectrumBL.clsSiteBank()
        Me.lblDefaultCurrencyTrack = New Spectrum.CtrlLabel()
        Me.lblCalCurrencyHeader = New Spectrum.CtrlLabel()
        Me.lblCalCurrencyMiniBalDue = New Spectrum.CtrlLabel()
        Me.lblCurrencyMinimumBalAmt = New Spectrum.CtrlLabel()
        Me.lblCalCurrencyBalanceDue = New Spectrum.CtrlLabel()
        Me.lblCurrencyBalanceDue = New Spectrum.CtrlLabel()
        Me.lblCalCurrencyTotalReciepts = New Spectrum.CtrlLabel()
        Me.lblCurrencyTotalReciept = New Spectrum.CtrlLabel()
        Me.lblCalCurrencyBillAmount = New Spectrum.CtrlLabel()
        Me.lblCurrencyBillAmount = New Spectrum.CtrlLabel()
        Me.lblDefaultCurrency = New Spectrum.CtrlLabel()
        Me.lblCalMinBalDue = New Spectrum.CtrlLabel()
        Me.lblMinimumBalanceAmount = New Spectrum.CtrlLabel()
        Me.lblbalanceDue = New Spectrum.CtrlLabel()
        Me.lblDefaultBalanceDue = New Spectrum.CtrlLabel()
        Me.lblTotalReciept = New Spectrum.CtrlLabel()
        Me.lblTotalReciepts = New Spectrum.CtrlLabel()
        Me.lblBillAmt = New Spectrum.CtrlLabel()
        Me.lblBillAmount = New Spectrum.CtrlLabel()
        Me.dgGridReciept = New Spectrum.CtrlGrid()
        Me.rtxtSwipeCard = New System.Windows.Forms.RichTextBox()
        Me.lblSwapCard = New Spectrum.CtrlLabel()
        Me.sizDetail = New C1.Win.C1Sizer.C1Sizer()
        Me.C1SizerPaymentTypes = New C1.Win.C1Sizer.C1Sizer()
        Me.C1SizerPaymentMode = New C1.Win.C1Sizer.C1Sizer()
        Me.LblPayTerm = New Spectrum.CtrlLabel()
        Me.CtrlPayTerm = New Spectrum.ctrlCombo()
        Me.lblSign = New Spectrum.CtrlLabel()
        Me.ctrlPayCash = New Spectrum.ctrlCash()
        Me.lblAmount = New Spectrum.CtrlLabel()
        Me.btndelete = New Spectrum.CtrlBtn()
        Me.lblRetrunAmount = New Spectrum.CtrlLabel()
        Me.lblReceiptType = New Spectrum.CtrlLabel()
        Me.lblSelectCurrency = New Spectrum.CtrlLabel()
        Me.cboRecieptType = New Spectrum.ctrlCombo()
        Me.cboCurrency = New Spectrum.ctrlCombo()
        Me.btnapprove = New Spectrum.CtrlBtn()
        Me.CtrlChequeDetails = New Spectrum.ctrlChequeDetails()
        Me.CtrlCLPPoint = New Spectrum.CtrlCLPPoint()
        Me.CtrlPayCreditCheque = New Spectrum.ctrlCreditCheck()
        Me.CtrlGiftVoucherIssue = New Spectrum.CtrlGiftVoucher()
        Me.ctrlPayCheque = New Spectrum.ctrlCheque()
        Me.ctrlPayCredit = New Spectrum.ctrlCreditCard()
        Me.btnF3 = New Spectrum.CtrlBtn()
        Me.btnF4CreditNote = New Spectrum.CtrlBtn()
        Me.btnF6 = New Spectrum.CtrlBtn()
        Me.sizSaveBtn = New C1.Win.C1Sizer.C1Sizer()
        Me.btnOK = New Spectrum.CtrlBtn()
        Me.btnCancel = New Spectrum.CtrlBtn()
        Me.btnGift = New Spectrum.CtrlBtn()
        Me.btnSave = New Spectrum.CtrlBtn()
        Me.btnF5Cash = New Spectrum.CtrlBtn()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.remarksTextbox = New System.Windows.Forms.RichTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.C1SizerMain = New C1.Win.C1Sizer.C1Sizer()
        Me.sizboxBottom = New C1.Win.C1Sizer.C1Sizer()
        CType(Me.lblDefaultCurrencyTrack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCalCurrencyHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCalCurrencyMiniBalDue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrencyMinimumBalAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCalCurrencyBalanceDue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrencyBalanceDue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCalCurrencyTotalReciepts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrencyTotalReciept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCalCurrencyBillAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCurrencyBillAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDefaultCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCalMinBalDue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMinimumBalanceAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblbalanceDue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDefaultBalanceDue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalReciept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalReciepts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBillAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBillAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgGridReciept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSwapCard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sizDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sizDetail.SuspendLayout()
        CType(Me.C1SizerPaymentTypes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1SizerPaymentTypes.SuspendLayout()
        CType(Me.C1SizerPaymentMode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1SizerPaymentMode.SuspendLayout()
        CType(Me.LblPayTerm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrlPayTerm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSign, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRetrunAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReceiptType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSelectCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboRecieptType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sizSaveBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sizSaveBtn.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.C1SizerMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1SizerMain.SuspendLayout()
        CType(Me.sizboxBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sizboxBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblDefaultCurrencyTrack
        '
        Me.lblDefaultCurrencyTrack.AttachedTextBoxName = Nothing
        Me.lblDefaultCurrencyTrack.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDefaultCurrencyTrack.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDefaultCurrencyTrack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDefaultCurrencyTrack.ForeColor = System.Drawing.Color.DarkRed
        Me.lblDefaultCurrencyTrack.Location = New System.Drawing.Point(127, 1)
        Me.lblDefaultCurrencyTrack.Name = "lblDefaultCurrencyTrack"
        Me.lblDefaultCurrencyTrack.Size = New System.Drawing.Size(110, 20)
        Me.lblDefaultCurrencyTrack.TabIndex = 10
        Me.lblDefaultCurrencyTrack.Tag = Nothing
        Me.lblDefaultCurrencyTrack.TextDetached = True
        Me.lblDefaultCurrencyTrack.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCalCurrencyHeader
        '
        Me.lblCalCurrencyHeader.AttachedTextBoxName = Nothing
        Me.lblCalCurrencyHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCalCurrencyHeader.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCalCurrencyHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCalCurrencyHeader.ForeColor = System.Drawing.Color.Red
        Me.lblCalCurrencyHeader.Location = New System.Drawing.Point(1, 50)
        Me.lblCalCurrencyHeader.Name = "lblCalCurrencyHeader"
        Me.lblCalCurrencyHeader.Size = New System.Drawing.Size(125, 20)
        Me.lblCalCurrencyHeader.TabIndex = 16
        Me.lblCalCurrencyHeader.Tag = Nothing
        Me.lblCalCurrencyHeader.TextDetached = True
        Me.lblCalCurrencyHeader.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCalCurrencyMiniBalDue
        '
        Me.lblCalCurrencyMiniBalDue.AttachedTextBoxName = Nothing
        Me.lblCalCurrencyMiniBalDue.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCalCurrencyMiniBalDue.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCalCurrencyMiniBalDue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCalCurrencyMiniBalDue.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalCurrencyMiniBalDue.ForeColor = System.Drawing.Color.DarkRed
        Me.lblCalCurrencyMiniBalDue.Location = New System.Drawing.Point(594, 71)
        Me.lblCalCurrencyMiniBalDue.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCalCurrencyMiniBalDue.Name = "lblCalCurrencyMiniBalDue"
        Me.lblCalCurrencyMiniBalDue.Size = New System.Drawing.Size(33, 28)
        Me.lblCalCurrencyMiniBalDue.TabIndex = 15
        Me.lblCalCurrencyMiniBalDue.Tag = Nothing
        Me.lblCalCurrencyMiniBalDue.Text = "0"
        Me.lblCalCurrencyMiniBalDue.TextDetached = True
        Me.lblCalCurrencyMiniBalDue.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCurrencyMinimumBalAmt
        '
        Me.lblCurrencyMinimumBalAmt.AttachedTextBoxName = Nothing
        Me.lblCurrencyMinimumBalAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCurrencyMinimumBalAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCurrencyMinimumBalAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCurrencyMinimumBalAmt.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblCurrencyMinimumBalAmt.ForeColor = System.Drawing.Color.Black
        Me.lblCurrencyMinimumBalAmt.Location = New System.Drawing.Point(469, 71)
        Me.lblCurrencyMinimumBalAmt.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCurrencyMinimumBalAmt.Name = "lblCurrencyMinimumBalAmt"
        Me.lblCurrencyMinimumBalAmt.Size = New System.Drawing.Size(124, 28)
        Me.lblCurrencyMinimumBalAmt.TabIndex = 14
        Me.lblCurrencyMinimumBalAmt.Tag = Nothing
        Me.lblCurrencyMinimumBalAmt.Text = "Min Balance Due:"
        Me.lblCurrencyMinimumBalAmt.TextDetached = True
        Me.lblCurrencyMinimumBalAmt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCalCurrencyBalanceDue
        '
        Me.lblCalCurrencyBalanceDue.AttachedTextBoxName = Nothing
        Me.lblCalCurrencyBalanceDue.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCalCurrencyBalanceDue.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCalCurrencyBalanceDue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCalCurrencyBalanceDue.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalCurrencyBalanceDue.ForeColor = System.Drawing.Color.DarkRed
        Me.lblCalCurrencyBalanceDue.Location = New System.Drawing.Point(396, 71)
        Me.lblCalCurrencyBalanceDue.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCalCurrencyBalanceDue.Name = "lblCalCurrencyBalanceDue"
        Me.lblCalCurrencyBalanceDue.Size = New System.Drawing.Size(72, 28)
        Me.lblCalCurrencyBalanceDue.TabIndex = 13
        Me.lblCalCurrencyBalanceDue.Tag = Nothing
        Me.lblCalCurrencyBalanceDue.Text = "0"
        Me.lblCalCurrencyBalanceDue.TextDetached = True
        Me.lblCalCurrencyBalanceDue.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCurrencyBalanceDue
        '
        Me.lblCurrencyBalanceDue.AttachedTextBoxName = Nothing
        Me.lblCurrencyBalanceDue.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCurrencyBalanceDue.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCurrencyBalanceDue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCurrencyBalanceDue.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblCurrencyBalanceDue.ForeColor = System.Drawing.Color.Black
        Me.lblCurrencyBalanceDue.Location = New System.Drawing.Point(298, 71)
        Me.lblCurrencyBalanceDue.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCurrencyBalanceDue.Name = "lblCurrencyBalanceDue"
        Me.lblCurrencyBalanceDue.Size = New System.Drawing.Size(97, 28)
        Me.lblCurrencyBalanceDue.TabIndex = 12
        Me.lblCurrencyBalanceDue.Tag = Nothing
        Me.lblCurrencyBalanceDue.Text = "Balance Due:"
        Me.lblCurrencyBalanceDue.TextDetached = True
        Me.lblCurrencyBalanceDue.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCalCurrencyTotalReciepts
        '
        Me.lblCalCurrencyTotalReciepts.AttachedTextBoxName = Nothing
        Me.lblCalCurrencyTotalReciepts.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCalCurrencyTotalReciepts.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCalCurrencyTotalReciepts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCalCurrencyTotalReciepts.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalCurrencyTotalReciepts.ForeColor = System.Drawing.Color.DarkRed
        Me.lblCalCurrencyTotalReciepts.Location = New System.Drawing.Point(238, 71)
        Me.lblCalCurrencyTotalReciepts.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCalCurrencyTotalReciepts.Name = "lblCalCurrencyTotalReciepts"
        Me.lblCalCurrencyTotalReciepts.Size = New System.Drawing.Size(59, 28)
        Me.lblCalCurrencyTotalReciepts.TabIndex = 11
        Me.lblCalCurrencyTotalReciepts.Tag = Nothing
        Me.lblCalCurrencyTotalReciepts.Text = "0"
        Me.lblCalCurrencyTotalReciepts.TextDetached = True
        Me.lblCalCurrencyTotalReciepts.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCurrencyTotalReciept
        '
        Me.lblCurrencyTotalReciept.AttachedTextBoxName = Nothing
        Me.lblCurrencyTotalReciept.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCurrencyTotalReciept.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCurrencyTotalReciept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCurrencyTotalReciept.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblCurrencyTotalReciept.ForeColor = System.Drawing.Color.Black
        Me.lblCurrencyTotalReciept.Location = New System.Drawing.Point(127, 71)
        Me.lblCurrencyTotalReciept.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCurrencyTotalReciept.Name = "lblCurrencyTotalReciept"
        Me.lblCurrencyTotalReciept.Size = New System.Drawing.Size(110, 28)
        Me.lblCurrencyTotalReciept.TabIndex = 10
        Me.lblCurrencyTotalReciept.Tag = Nothing
        Me.lblCurrencyTotalReciept.Text = "Total Reciepts:"
        Me.lblCurrencyTotalReciept.TextDetached = True
        Me.lblCurrencyTotalReciept.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCalCurrencyBillAmount
        '
        Me.lblCalCurrencyBillAmount.AttachedTextBoxName = Nothing
        Me.lblCalCurrencyBillAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCalCurrencyBillAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCalCurrencyBillAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCalCurrencyBillAmount.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalCurrencyBillAmount.ForeColor = System.Drawing.Color.DarkRed
        Me.lblCalCurrencyBillAmount.Location = New System.Drawing.Point(68, 71)
        Me.lblCalCurrencyBillAmount.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCalCurrencyBillAmount.Name = "lblCalCurrencyBillAmount"
        Me.lblCalCurrencyBillAmount.Size = New System.Drawing.Size(58, 28)
        Me.lblCalCurrencyBillAmount.TabIndex = 9
        Me.lblCalCurrencyBillAmount.Tag = Nothing
        Me.lblCalCurrencyBillAmount.Text = "0"
        Me.lblCalCurrencyBillAmount.TextDetached = True
        Me.lblCalCurrencyBillAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCurrencyBillAmount
        '
        Me.lblCurrencyBillAmount.AttachedTextBoxName = Nothing
        Me.lblCurrencyBillAmount.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrencyBillAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCurrencyBillAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCurrencyBillAmount.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblCurrencyBillAmount.ForeColor = System.Drawing.Color.Black
        Me.lblCurrencyBillAmount.Location = New System.Drawing.Point(1, 71)
        Me.lblCurrencyBillAmount.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCurrencyBillAmount.Name = "lblCurrencyBillAmount"
        Me.lblCurrencyBillAmount.Size = New System.Drawing.Size(66, 28)
        Me.lblCurrencyBillAmount.TabIndex = 8
        Me.lblCurrencyBillAmount.Tag = Nothing
        Me.lblCurrencyBillAmount.Text = "Amount:"
        Me.lblCurrencyBillAmount.TextDetached = True
        Me.lblCurrencyBillAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblDefaultCurrency
        '
        Me.lblDefaultCurrency.AttachedTextBoxName = Nothing
        Me.lblDefaultCurrency.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDefaultCurrency.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDefaultCurrency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDefaultCurrency.ForeColor = System.Drawing.Color.DarkRed
        Me.lblDefaultCurrency.Location = New System.Drawing.Point(1, 1)
        Me.lblDefaultCurrency.Name = "lblDefaultCurrency"
        Me.lblDefaultCurrency.Size = New System.Drawing.Size(125, 20)
        Me.lblDefaultCurrency.TabIndex = 8
        Me.lblDefaultCurrency.Tag = Nothing
        Me.lblDefaultCurrency.Text = "Default Currency :"
        Me.lblDefaultCurrency.TextDetached = True
        Me.lblDefaultCurrency.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblCalMinBalDue
        '
        Me.lblCalMinBalDue.AttachedTextBoxName = Nothing
        Me.lblCalMinBalDue.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCalMinBalDue.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblCalMinBalDue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCalMinBalDue.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalMinBalDue.ForeColor = System.Drawing.Color.DarkRed
        Me.lblCalMinBalDue.Location = New System.Drawing.Point(594, 22)
        Me.lblCalMinBalDue.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCalMinBalDue.Name = "lblCalMinBalDue"
        Me.lblCalMinBalDue.Size = New System.Drawing.Size(33, 27)
        Me.lblCalMinBalDue.TabIndex = 7
        Me.lblCalMinBalDue.Tag = Nothing
        Me.lblCalMinBalDue.Text = "0"
        Me.lblCalMinBalDue.TextDetached = True
        Me.lblCalMinBalDue.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblMinimumBalanceAmount
        '
        Me.lblMinimumBalanceAmount.AttachedTextBoxName = Nothing
        Me.lblMinimumBalanceAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblMinimumBalanceAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblMinimumBalanceAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMinimumBalanceAmount.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblMinimumBalanceAmount.ForeColor = System.Drawing.Color.Black
        Me.lblMinimumBalanceAmount.Location = New System.Drawing.Point(469, 22)
        Me.lblMinimumBalanceAmount.Margin = New System.Windows.Forms.Padding(0)
        Me.lblMinimumBalanceAmount.Name = "lblMinimumBalanceAmount"
        Me.lblMinimumBalanceAmount.Size = New System.Drawing.Size(124, 27)
        Me.lblMinimumBalanceAmount.TabIndex = 6
        Me.lblMinimumBalanceAmount.Tag = Nothing
        Me.lblMinimumBalanceAmount.Text = "Min Balance Due:"
        Me.lblMinimumBalanceAmount.TextDetached = True
        Me.lblMinimumBalanceAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblbalanceDue
        '
        Me.lblbalanceDue.AttachedTextBoxName = Nothing
        Me.lblbalanceDue.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblbalanceDue.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblbalanceDue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblbalanceDue.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbalanceDue.ForeColor = System.Drawing.Color.DarkRed
        Me.lblbalanceDue.Location = New System.Drawing.Point(396, 22)
        Me.lblbalanceDue.Margin = New System.Windows.Forms.Padding(0)
        Me.lblbalanceDue.Name = "lblbalanceDue"
        Me.lblbalanceDue.Size = New System.Drawing.Size(72, 27)
        Me.lblbalanceDue.TabIndex = 5
        Me.lblbalanceDue.Tag = Nothing
        Me.lblbalanceDue.Text = "0"
        Me.lblbalanceDue.TextDetached = True
        Me.lblbalanceDue.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblDefaultBalanceDue
        '
        Me.lblDefaultBalanceDue.AttachedTextBoxName = Nothing
        Me.lblDefaultBalanceDue.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDefaultBalanceDue.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblDefaultBalanceDue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDefaultBalanceDue.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblDefaultBalanceDue.ForeColor = System.Drawing.Color.Black
        Me.lblDefaultBalanceDue.Location = New System.Drawing.Point(298, 22)
        Me.lblDefaultBalanceDue.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDefaultBalanceDue.Name = "lblDefaultBalanceDue"
        Me.lblDefaultBalanceDue.Size = New System.Drawing.Size(97, 27)
        Me.lblDefaultBalanceDue.TabIndex = 4
        Me.lblDefaultBalanceDue.Tag = Nothing
        Me.lblDefaultBalanceDue.Text = "Balance Due:"
        Me.lblDefaultBalanceDue.TextDetached = True
        Me.lblDefaultBalanceDue.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblTotalReciept
        '
        Me.lblTotalReciept.AttachedTextBoxName = Nothing
        Me.lblTotalReciept.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTotalReciept.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTotalReciept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalReciept.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalReciept.ForeColor = System.Drawing.Color.DarkRed
        Me.lblTotalReciept.Location = New System.Drawing.Point(238, 22)
        Me.lblTotalReciept.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTotalReciept.Name = "lblTotalReciept"
        Me.lblTotalReciept.Size = New System.Drawing.Size(59, 27)
        Me.lblTotalReciept.TabIndex = 3
        Me.lblTotalReciept.Tag = Nothing
        Me.lblTotalReciept.Text = "0"
        Me.lblTotalReciept.TextDetached = True
        Me.lblTotalReciept.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblTotalReciepts
        '
        Me.lblTotalReciepts.AttachedTextBoxName = Nothing
        Me.lblTotalReciepts.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTotalReciepts.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTotalReciepts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalReciepts.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblTotalReciepts.ForeColor = System.Drawing.Color.Black
        Me.lblTotalReciepts.Location = New System.Drawing.Point(127, 22)
        Me.lblTotalReciepts.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTotalReciepts.Name = "lblTotalReciepts"
        Me.lblTotalReciepts.Size = New System.Drawing.Size(110, 27)
        Me.lblTotalReciepts.TabIndex = 2
        Me.lblTotalReciepts.Tag = Nothing
        Me.lblTotalReciepts.Text = "Total Reciepts:"
        Me.lblTotalReciepts.TextDetached = True
        Me.lblTotalReciepts.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblBillAmt
        '
        Me.lblBillAmt.AttachedTextBoxName = Nothing
        Me.lblBillAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblBillAmt.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblBillAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBillAmt.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillAmt.ForeColor = System.Drawing.Color.DarkRed
        Me.lblBillAmt.Location = New System.Drawing.Point(68, 22)
        Me.lblBillAmt.Margin = New System.Windows.Forms.Padding(0)
        Me.lblBillAmt.Name = "lblBillAmt"
        Me.lblBillAmt.Size = New System.Drawing.Size(58, 27)
        Me.lblBillAmt.TabIndex = 1
        Me.lblBillAmt.Tag = Nothing
        Me.lblBillAmt.Text = "0"
        Me.lblBillAmt.TextDetached = True
        Me.lblBillAmt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblBillAmount
        '
        Me.lblBillAmount.AttachedTextBoxName = Nothing
        Me.lblBillAmount.BackColor = System.Drawing.Color.Transparent
        Me.lblBillAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblBillAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBillAmount.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.lblBillAmount.ForeColor = System.Drawing.Color.Black
        Me.lblBillAmount.Location = New System.Drawing.Point(1, 22)
        Me.lblBillAmount.Margin = New System.Windows.Forms.Padding(0)
        Me.lblBillAmount.Name = "lblBillAmount"
        Me.lblBillAmount.Size = New System.Drawing.Size(66, 27)
        Me.lblBillAmount.TabIndex = 0
        Me.lblBillAmount.Tag = Nothing
        Me.lblBillAmount.Text = "Amount:"
        Me.lblBillAmount.TextDetached = True
        Me.lblBillAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'dgGridReciept
        '
        Me.dgGridReciept.AllowEditing = False
        Me.dgGridReciept.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.dgGridReciept.CellButtonImage = CType(resources.GetObject("dgGridReciept.CellButtonImage"), System.Drawing.Image)
        Me.dgGridReciept.ColumnInfo = "7,0,0,0,0,110,Columns:0{Width:142;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.dgGridReciept.ExtendLastCol = True
        Me.dgGridReciept.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.dgGridReciept.Location = New System.Drawing.Point(1, 168)
        Me.dgGridReciept.Name = "dgGridReciept"
        Me.dgGridReciept.Rows.Count = 1
        Me.dgGridReciept.Rows.DefaultSize = 22
        Me.dgGridReciept.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox
        Me.dgGridReciept.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.dgGridReciept.Size = New System.Drawing.Size(754, 115)
        Me.dgGridReciept.StyleInfo = resources.GetString("dgGridReciept.StyleInfo")
        Me.dgGridReciept.TabIndex = 4
        Me.dgGridReciept.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        '
        'rtxtSwipeCard
        '
        Me.rtxtSwipeCard.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.rtxtSwipeCard.ForeColor = System.Drawing.Color.Black
        Me.rtxtSwipeCard.Location = New System.Drawing.Point(1, 297)
        Me.rtxtSwipeCard.Name = "rtxtSwipeCard"
        Me.rtxtSwipeCard.Size = New System.Drawing.Size(754, 28)
        Me.rtxtSwipeCard.TabIndex = 5
        Me.rtxtSwipeCard.Text = ""
        '
        'lblSwapCard
        '
        Me.lblSwapCard.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSwapCard.AttachedTextBoxName = Nothing
        Me.lblSwapCard.AutoSize = True
        Me.lblSwapCard.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblSwapCard.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblSwapCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSwapCard.ForeColor = System.Drawing.Color.Black
        Me.lblSwapCard.Location = New System.Drawing.Point(1, 285)
        Me.lblSwapCard.Name = "lblSwapCard"
        Me.lblSwapCard.Size = New System.Drawing.Size(169, 18)
        Me.lblSwapCard.TabIndex = 14
        Me.lblSwapCard.Tag = Nothing
        Me.lblSwapCard.Text = "Swipe Credit Card Here:"
        Me.lblSwapCard.TextDetached = True
        Me.lblSwapCard.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'sizDetail
        '
        Me.sizDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.sizDetail.Border.Color = System.Drawing.Color.LightSlateGray
        Me.sizDetail.Border.Corners = New C1.Win.C1Sizer.Corners(1, 1, 1, 1)
        Me.sizDetail.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.sizDetail.Controls.Add(Me.C1SizerPaymentTypes)
        Me.sizDetail.Controls.Add(Me.lblSwapCard)
        Me.sizDetail.Controls.Add(Me.dgGridReciept)
        Me.sizDetail.Controls.Add(Me.rtxtSwipeCard)
        Me.sizDetail.GridDefinition = "50.6134969325153:False:True;35.2760736196319:False:False;3.06748466257669:False:T" & _
    "rue;8.58895705521472:False:True;" & Global.Microsoft.VisualBasic.ChrW(9) & "99.7354497354497:False:False;"
        Me.sizDetail.Location = New System.Drawing.Point(2, 104)
        Me.sizDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.sizDetail.Name = "sizDetail"
        Me.sizDetail.Padding = New System.Windows.Forms.Padding(0)
        Me.sizDetail.Size = New System.Drawing.Size(756, 326)
        Me.sizDetail.SplitterWidth = 2
        Me.sizDetail.TabIndex = 0
        Me.sizDetail.Text = "C1Sizer2"
        '
        'C1SizerPaymentTypes
        '
        Me.C1SizerPaymentTypes.Border.Color = System.Drawing.Color.Transparent
        Me.C1SizerPaymentTypes.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1SizerPaymentTypes.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1SizerPaymentTypes.Controls.Add(Me.C1SizerPaymentMode)
        Me.C1SizerPaymentTypes.Controls.Add(Me.CtrlChequeDetails)
        Me.C1SizerPaymentTypes.Controls.Add(Me.CtrlCLPPoint)
        Me.C1SizerPaymentTypes.Controls.Add(Me.CtrlPayCreditCheque)
        Me.C1SizerPaymentTypes.Controls.Add(Me.CtrlGiftVoucherIssue)
        Me.C1SizerPaymentTypes.Controls.Add(Me.ctrlPayCheque)
        Me.C1SizerPaymentTypes.Controls.Add(Me.ctrlPayCredit)
        Me.C1SizerPaymentTypes.GridDefinition = "97.5757575757576:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "59.2838196286472:False:False;40.053050397878:False:" & _
    "False;"
        Me.C1SizerPaymentTypes.Location = New System.Drawing.Point(1, 1)
        Me.C1SizerPaymentTypes.Margin = New System.Windows.Forms.Padding(0)
        Me.C1SizerPaymentTypes.Name = "C1SizerPaymentTypes"
        Me.C1SizerPaymentTypes.Padding = New System.Windows.Forms.Padding(1)
        Me.C1SizerPaymentTypes.Size = New System.Drawing.Size(754, 165)
        Me.C1SizerPaymentTypes.SplitterWidth = 1
        Me.C1SizerPaymentTypes.TabIndex = 33
        '
        'C1SizerPaymentMode
        '
        Me.C1SizerPaymentMode.Border.Color = System.Drawing.Color.Transparent
        Me.C1SizerPaymentMode.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1SizerPaymentMode.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1SizerPaymentMode.Controls.Add(Me.LblPayTerm)
        Me.C1SizerPaymentMode.Controls.Add(Me.CtrlPayTerm)
        Me.C1SizerPaymentMode.Controls.Add(Me.lblSign)
        Me.C1SizerPaymentMode.Controls.Add(Me.ctrlPayCash)
        Me.C1SizerPaymentMode.Controls.Add(Me.lblAmount)
        Me.C1SizerPaymentMode.Controls.Add(Me.btndelete)
        Me.C1SizerPaymentMode.Controls.Add(Me.lblRetrunAmount)
        Me.C1SizerPaymentMode.Controls.Add(Me.lblReceiptType)
        Me.C1SizerPaymentMode.Controls.Add(Me.lblSelectCurrency)
        Me.C1SizerPaymentMode.Controls.Add(Me.cboRecieptType)
        Me.C1SizerPaymentMode.Controls.Add(Me.cboCurrency)
        Me.C1SizerPaymentMode.Controls.Add(Me.btnapprove)
        Me.C1SizerPaymentMode.GridDefinition = resources.GetString("C1SizerPaymentMode.GridDefinition")
        Me.C1SizerPaymentMode.Location = New System.Drawing.Point(2, 2)
        Me.C1SizerPaymentMode.Margin = New System.Windows.Forms.Padding(0)
        Me.C1SizerPaymentMode.Name = "C1SizerPaymentMode"
        Me.C1SizerPaymentMode.Padding = New System.Windows.Forms.Padding(0)
        Me.C1SizerPaymentMode.Size = New System.Drawing.Size(447, 161)
        Me.C1SizerPaymentMode.SplitterWidth = 2
        Me.C1SizerPaymentMode.TabIndex = 33
        '
        'LblPayTerm
        '
        Me.LblPayTerm.AttachedTextBoxName = Nothing
        Me.LblPayTerm.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.LblPayTerm.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.LblPayTerm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPayTerm.ForeColor = System.Drawing.Color.Black
        Me.LblPayTerm.Location = New System.Drawing.Point(298, 28)
        Me.LblPayTerm.Name = "LblPayTerm"
        Me.LblPayTerm.Size = New System.Drawing.Size(71, 30)
        Me.LblPayTerm.TabIndex = 32
        Me.LblPayTerm.Tag = Nothing
        Me.LblPayTerm.Text = "Payment Term"
        Me.LblPayTerm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblPayTerm.TextDetached = True
        Me.LblPayTerm.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlPayTerm
        '
        Me.CtrlPayTerm.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.CtrlPayTerm.AutoCompletion = True
        Me.CtrlPayTerm.AutoDropDown = True
        Me.CtrlPayTerm.Caption = ""
        Me.CtrlPayTerm.CaptionHeight = 17
        Me.CtrlPayTerm.CaptionVisible = False
        Me.CtrlPayTerm.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CtrlPayTerm.ColumnCaptionHeight = 17
        Me.CtrlPayTerm.ColumnFooterHeight = 17
        Me.CtrlPayTerm.ColumnHeaders = False
        Me.CtrlPayTerm.ContentHeight = 16
        Me.CtrlPayTerm.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.CtrlPayTerm.EditorBackColor = System.Drawing.SystemColors.Window
        Me.CtrlPayTerm.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlPayTerm.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.CtrlPayTerm.EditorHeight = 16
        Me.CtrlPayTerm.Images.Add(CType(resources.GetObject("CtrlPayTerm.Images"), System.Drawing.Image))
        Me.CtrlPayTerm.ItemHeight = 15
        Me.CtrlPayTerm.Location = New System.Drawing.Point(385, 28)
        Me.CtrlPayTerm.MatchEntryTimeout = CType(2000, Long)
        Me.CtrlPayTerm.MaxDropDownItems = CType(5, Short)
        Me.CtrlPayTerm.MaximumSize = New System.Drawing.Size(175, 22)
        Me.CtrlPayTerm.MaxLength = 32767
        Me.CtrlPayTerm.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.CtrlPayTerm.Name = "CtrlPayTerm"
        Me.CtrlPayTerm.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.CtrlPayTerm.Size = New System.Drawing.Size(61, 22)
        Me.CtrlPayTerm.TabIndex = 31
        Me.CtrlPayTerm.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.CtrlPayTerm.PropBag = resources.GetString("CtrlPayTerm.PropBag")
        '
        'lblSign
        '
        Me.lblSign.AttachedTextBoxName = Nothing
        Me.lblSign.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblSign.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblSign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSign.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSign.ForeColor = System.Drawing.Color.Black
        Me.lblSign.Location = New System.Drawing.Point(371, 1)
        Me.lblSign.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSign.Name = "lblSign"
        Me.lblSign.Size = New System.Drawing.Size(12, 25)
        Me.lblSign.TabIndex = 28
        Me.lblSign.Tag = Nothing
        Me.lblSign.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSign.TextDetached = True
        Me.lblSign.Value = ""
        Me.lblSign.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'ctrlPayCash
        '
        Me.ctrlPayCash.Location = New System.Drawing.Point(385, 1)
        Me.ctrlPayCash.Name = "ctrlPayCash"
        Me.ctrlPayCash.Size = New System.Drawing.Size(61, 25)
        Me.ctrlPayCash.TabIndex = 1
        '
        'lblAmount
        '
        Me.lblAmount.AttachedTextBoxName = Nothing
        Me.lblAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAmount.ForeColor = System.Drawing.Color.Black
        Me.lblAmount.Location = New System.Drawing.Point(298, 1)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(71, 25)
        Me.lblAmount.TabIndex = 25
        Me.lblAmount.Tag = Nothing
        Me.lblAmount.Text = "Amount"
        Me.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAmount.TextDetached = True
        Me.lblAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btndelete
        '
        Me.btndelete.Image = Global.Spectrum.My.Resources.Resources.ACP_can
        Me.btndelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btndelete.Location = New System.Drawing.Point(1, 1)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.SetArticleCode = Nothing
        Me.btndelete.SetRowIndex = 0
        Me.btndelete.Size = New System.Drawing.Size(30, 25)
        Me.btndelete.TabIndex = 3
        Me.btndelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btndelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btndelete.UseVisualStyleBackColor = True
        Me.btndelete.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblRetrunAmount
        '
        Me.lblRetrunAmount.AttachedTextBoxName = Nothing
        Me.lblRetrunAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblRetrunAmount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblRetrunAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRetrunAmount.ForeColor = System.Drawing.Color.Red
        Me.lblRetrunAmount.Location = New System.Drawing.Point(167, 60)
        Me.lblRetrunAmount.Name = "lblRetrunAmount"
        Me.lblRetrunAmount.Size = New System.Drawing.Size(279, 100)
        Me.lblRetrunAmount.TabIndex = 30
        Me.lblRetrunAmount.Tag = Nothing
        Me.lblRetrunAmount.TextDetached = True
        Me.lblRetrunAmount.Value = ""
        Me.lblRetrunAmount.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblReceiptType
        '
        Me.lblReceiptType.AttachedTextBoxName = Nothing
        Me.lblReceiptType.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblReceiptType.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblReceiptType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReceiptType.ForeColor = System.Drawing.Color.Black
        Me.lblReceiptType.Location = New System.Drawing.Point(65, 1)
        Me.lblReceiptType.Name = "lblReceiptType"
        Me.lblReceiptType.Size = New System.Drawing.Size(100, 25)
        Me.lblReceiptType.TabIndex = 19
        Me.lblReceiptType.Tag = Nothing
        Me.lblReceiptType.Text = "Receipt Type"
        Me.lblReceiptType.TextDetached = True
        Me.lblReceiptType.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'lblSelectCurrency
        '
        Me.lblSelectCurrency.AttachedTextBoxName = Nothing
        Me.lblSelectCurrency.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblSelectCurrency.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblSelectCurrency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSelectCurrency.ForeColor = System.Drawing.Color.Black
        Me.lblSelectCurrency.Location = New System.Drawing.Point(65, 28)
        Me.lblSelectCurrency.Name = "lblSelectCurrency"
        Me.lblSelectCurrency.Size = New System.Drawing.Size(100, 30)
        Me.lblSelectCurrency.TabIndex = 20
        Me.lblSelectCurrency.Tag = Nothing
        Me.lblSelectCurrency.Text = "Currency"
        Me.lblSelectCurrency.TextDetached = True
        Me.lblSelectCurrency.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'cboRecieptType
        '
        Me.cboRecieptType.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboRecieptType.AutoCompletion = True
        Me.cboRecieptType.AutoDropDown = True
        Me.cboRecieptType.AutoSelect = True
        Me.cboRecieptType.Caption = ""
        Me.cboRecieptType.CaptionHeight = 17
        Me.cboRecieptType.CaptionVisible = False
        Me.cboRecieptType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboRecieptType.ColumnCaptionHeight = 17
        Me.cboRecieptType.ColumnFooterHeight = 17
        Me.cboRecieptType.ColumnHeaders = False
        Me.cboRecieptType.ContentHeight = 16
        Me.cboRecieptType.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboRecieptType.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboRecieptType.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRecieptType.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboRecieptType.EditorHeight = 16
        Me.cboRecieptType.Images.Add(CType(resources.GetObject("cboRecieptType.Images"), System.Drawing.Image))
        Me.cboRecieptType.ItemHeight = 15
        Me.cboRecieptType.Location = New System.Drawing.Point(167, 1)
        Me.cboRecieptType.MatchEntryTimeout = CType(2000, Long)
        Me.cboRecieptType.MaxDropDownItems = CType(5, Short)
        Me.cboRecieptType.MaximumSize = New System.Drawing.Size(175, 22)
        Me.cboRecieptType.MaxLength = 32767
        Me.cboRecieptType.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboRecieptType.Name = "cboRecieptType"
        Me.cboRecieptType.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboRecieptType.Size = New System.Drawing.Size(116, 22)
        Me.cboRecieptType.TabIndex = 0
        Me.cboRecieptType.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cboRecieptType.PropBag = resources.GetString("cboRecieptType.PropBag")
        '
        'cboCurrency
        '
        Me.cboCurrency.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.cboCurrency.AutoCompletion = True
        Me.cboCurrency.AutoDropDown = True
        Me.cboCurrency.Caption = ""
        Me.cboCurrency.CaptionHeight = 17
        Me.cboCurrency.CaptionVisible = False
        Me.cboCurrency.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cboCurrency.ColumnCaptionHeight = 17
        Me.cboCurrency.ColumnFooterHeight = 17
        Me.cboCurrency.ColumnHeaders = False
        Me.cboCurrency.ContentHeight = 16
        Me.cboCurrency.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.cboCurrency.EditorBackColor = System.Drawing.SystemColors.Window
        Me.cboCurrency.EditorFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCurrency.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCurrency.EditorHeight = 16
        Me.cboCurrency.Images.Add(CType(resources.GetObject("cboCurrency.Images"), System.Drawing.Image))
        Me.cboCurrency.ItemHeight = 15
        Me.cboCurrency.Location = New System.Drawing.Point(167, 28)
        Me.cboCurrency.MatchEntryTimeout = CType(2000, Long)
        Me.cboCurrency.MaxDropDownItems = CType(5, Short)
        Me.cboCurrency.MaximumSize = New System.Drawing.Size(175, 22)
        Me.cboCurrency.MaxLength = 32767
        Me.cboCurrency.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.cboCurrency.Name = "cboCurrency"
        Me.cboCurrency.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.cboCurrency.Size = New System.Drawing.Size(116, 22)
        Me.cboCurrency.TabIndex = 22
        Me.cboCurrency.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.cboCurrency.PropBag = resources.GetString("cboCurrency.PropBag")
        '
        'btnapprove
        '
        Me.btnapprove.Image = Global.Spectrum.My.Resources.Resources.yes
        Me.btnapprove.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnapprove.Location = New System.Drawing.Point(33, 1)
        Me.btnapprove.Name = "btnapprove"
        Me.btnapprove.SetArticleCode = Nothing
        Me.btnapprove.SetRowIndex = 0
        Me.btnapprove.Size = New System.Drawing.Size(30, 25)
        Me.btnapprove.TabIndex = 2
        Me.btnapprove.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnapprove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnapprove.UseVisualStyleBackColor = True
        Me.btnapprove.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'CtrlChequeDetails
        '
        Me.CtrlChequeDetails.AutoSize = True
        Me.CtrlChequeDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CtrlChequeDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        ClsSiteBank1.DecimalDigits = 0
        ClsSiteBank1.ListOfNumber = Nothing
        ClsSiteBank1.NextShiftId = 0
        ClsSiteBank1.PrevShiftId = 0
        ClsSiteBank1.strLangCode = Nothing
        Me.CtrlChequeDetails.ClsSiteBank = ClsSiteBank1
        Me.CtrlChequeDetails.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrlChequeDetails.Location = New System.Drawing.Point(450, 2)
        Me.CtrlChequeDetails.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrlChequeDetails.Name = "CtrlChequeDetails"
        Me.CtrlChequeDetails.Size = New System.Drawing.Size(302, 161)
        Me.CtrlChequeDetails.TabIndex = 32
        Me.CtrlChequeDetails.Visible = False
        '
        'CtrlCLPPoint
        '
        Me.CtrlCLPPoint.AutoSize = True
        Me.CtrlCLPPoint.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CtrlCLPPoint.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlCLPPoint.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.CtrlCLPPoint.Location = New System.Drawing.Point(450, 2)
        Me.CtrlCLPPoint.Margin = New System.Windows.Forms.Padding(4)
        Me.CtrlCLPPoint.Name = "CtrlCLPPoint"
        Me.CtrlCLPPoint.Size = New System.Drawing.Size(302, 161)
        Me.CtrlCLPPoint.TabIndex = 31
        Me.CtrlCLPPoint.Visible = False
        '
        'CtrlPayCreditCheque
        '
        Me.CtrlPayCreditCheque.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlPayCreditCheque.Location = New System.Drawing.Point(450, 2)
        Me.CtrlPayCreditCheque.Name = "CtrlPayCreditCheque"
        Me.CtrlPayCreditCheque.Size = New System.Drawing.Size(302, 161)
        Me.CtrlPayCreditCheque.TabIndex = 29
        '
        'CtrlGiftVoucherIssue
        '
        Me.CtrlGiftVoucherIssue.AutoSize = True
        Me.CtrlGiftVoucherIssue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CtrlGiftVoucherIssue.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CtrlGiftVoucherIssue.Location = New System.Drawing.Point(450, 2)
        Me.CtrlGiftVoucherIssue.Name = "CtrlGiftVoucherIssue"
        Me.CtrlGiftVoucherIssue.Size = New System.Drawing.Size(302, 161)
        Me.CtrlGiftVoucherIssue.TabIndex = 29
        Me.CtrlGiftVoucherIssue.Visible = False
        '
        'ctrlPayCheque
        '
        Me.ctrlPayCheque.AutoSize = True
        Me.ctrlPayCheque.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ctrlPayCheque.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ctrlPayCheque.Location = New System.Drawing.Point(450, 2)
        Me.ctrlPayCheque.Margin = New System.Windows.Forms.Padding(0)
        Me.ctrlPayCheque.Name = "ctrlPayCheque"
        Me.ctrlPayCheque.PaymentType = Nothing
        Me.ctrlPayCheque.Size = New System.Drawing.Size(302, 161)
        Me.ctrlPayCheque.TabIndex = 24
        Me.ctrlPayCheque.Visible = False
        '
        'ctrlPayCredit
        '
        Me.ctrlPayCredit.AutoSize = True
        Me.ctrlPayCredit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ctrlPayCredit.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        ClsSiteBank2.DecimalDigits = 0
        ClsSiteBank2.ListOfNumber = Nothing
        ClsSiteBank2.NextShiftId = 0
        ClsSiteBank2.PrevShiftId = 0
        ClsSiteBank2.strLangCode = Nothing
        Me.ctrlPayCredit.ClsSiteBank = ClsSiteBank2
        Me.ctrlPayCredit.Location = New System.Drawing.Point(450, 2)
        Me.ctrlPayCredit.Margin = New System.Windows.Forms.Padding(0)
        Me.ctrlPayCredit.Name = "ctrlPayCredit"
        Me.ctrlPayCredit.Size = New System.Drawing.Size(302, 161)
        Me.ctrlPayCredit.TabIndex = 23
        '
        'btnF3
        '
        Me.btnF3.Image = Global.Spectrum.My.Resources.Resources.cheque
        Me.btnF3.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnF3.Location = New System.Drawing.Point(192, 5)
        Me.btnF3.Name = "btnF3"
        Me.btnF3.SetArticleCode = Nothing
        Me.btnF3.SetRowIndex = 0
        Me.btnF3.Size = New System.Drawing.Size(180, 67)
        Me.btnF3.TabIndex = 1
        Me.btnF3.Text = "     F7" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     Cheque" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.btnF3.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnF3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnF3.UseVisualStyleBackColor = True
        Me.btnF3.Visible = False
        Me.btnF3.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnF4CreditNote
        '
        Me.btnF4CreditNote.Image = Global.Spectrum.My.Resources.Resources.credit_note
        Me.btnF4CreditNote.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnF4CreditNote.Location = New System.Drawing.Point(376, 5)
        Me.btnF4CreditNote.Name = "btnF4CreditNote"
        Me.btnF4CreditNote.SetArticleCode = Nothing
        Me.btnF4CreditNote.SetRowIndex = 0
        Me.btnF4CreditNote.Size = New System.Drawing.Size(187, 67)
        Me.btnF4CreditNote.TabIndex = 2
        Me.btnF4CreditNote.Text = "       F4" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "       Credit Voucher" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.btnF4CreditNote.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnF4CreditNote.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnF4CreditNote.UseVisualStyleBackColor = True
        Me.btnF4CreditNote.Visible = False
        Me.btnF4CreditNote.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnF6
        '
        Me.btnF6.Image = CType(resources.GetObject("btnF6.Image"), System.Drawing.Image)
        Me.btnF6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnF6.Location = New System.Drawing.Point(567, 5)
        Me.btnF6.Name = "btnF6"
        Me.btnF6.SetArticleCode = Nothing
        Me.btnF6.SetRowIndex = 0
        Me.btnF6.Size = New System.Drawing.Size(184, 67)
        Me.btnF6.TabIndex = 3
        Me.btnF6.Text = "        F6" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "    Gift Voucher" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.btnF6.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnF6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnF6.UseVisualStyleBackColor = True
        Me.btnF6.Visible = False
        Me.btnF6.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'sizSaveBtn
        '
        Me.sizSaveBtn.Border.Color = System.Drawing.Color.LightSlateGray
        Me.sizSaveBtn.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.sizSaveBtn.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.sizSaveBtn.Controls.Add(Me.btnOK)
        Me.sizSaveBtn.Controls.Add(Me.btnCancel)
        Me.sizSaveBtn.Controls.Add(Me.btnGift)
        Me.sizSaveBtn.Controls.Add(Me.btnSave)
        Me.sizSaveBtn.Controls.Add(Me.btnF6)
        Me.sizSaveBtn.Controls.Add(Me.btnF4CreditNote)
        Me.sizSaveBtn.Controls.Add(Me.btnF3)
        Me.sizSaveBtn.Controls.Add(Me.btnF5Cash)
        Me.sizSaveBtn.GridDefinition = "40.2597402597403:False:False;41.5584415584416:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "24.2063492063492:False" & _
    ":False;23.8095238095238:False:False;24.7354497354497:False:False;24.338624338624" & _
    "3:False:False;"
        Me.sizSaveBtn.Location = New System.Drawing.Point(2, 432)
        Me.sizSaveBtn.Name = "sizSaveBtn"
        Me.sizSaveBtn.Size = New System.Drawing.Size(756, 77)
        Me.sizSaveBtn.TabIndex = 1
        Me.sizSaveBtn.Text = "C1Sizer1"
        '
        'btnOK
        '
        Me.btnOK.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnOK.Location = New System.Drawing.Point(567, 5)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.SetArticleCode = Nothing
        Me.btnOK.SetRowIndex = 0
        Me.btnOK.Size = New System.Drawing.Size(184, 67)
        Me.btnOK.TabIndex = 7
        Me.btnOK.Text = "Ok" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.btnOK.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOK.UseVisualStyleBackColor = True
        Me.btnOK.Visible = False
        Me.btnOK.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Image = Global.Spectrum.My.Resources.Resources.cancel1
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(376, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.SetArticleCode = Nothing
        Me.btnCancel.SetRowIndex = 0
        Me.btnCancel.Size = New System.Drawing.Size(187, 67)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Esc" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnGift
        '
        Me.btnGift.Image = Global.Spectrum.My.Resources.Resources.save_gift_print
        Me.btnGift.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnGift.Location = New System.Drawing.Point(192, 5)
        Me.btnGift.Name = "btnGift"
        Me.btnGift.SetArticleCode = Nothing
        Me.btnGift.SetRowIndex = 0
        Me.btnGift.Size = New System.Drawing.Size(180, 67)
        Me.btnGift.TabIndex = 5
        Me.btnGift.Text = "F11" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Save/Gift Print" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.btnGift.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnGift.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnGift.UseVisualStyleBackColor = True
        Me.btnGift.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnSave
        '
        Me.btnSave.Image = Global.Spectrum.My.Resources.Resources.save_print_btn
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnSave.Location = New System.Drawing.Point(5, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.SetArticleCode = Nothing
        Me.btnSave.SetRowIndex = 0
        Me.btnSave.Size = New System.Drawing.Size(183, 67)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "F10" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Save/ Print" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnSave.UseVisualStyleBackColor = True
        Me.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnF5Cash
        '
        Me.btnF5Cash.Image = CType(resources.GetObject("btnF5Cash.Image"), System.Drawing.Image)
        Me.btnF5Cash.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnF5Cash.Location = New System.Drawing.Point(5, 5)
        Me.btnF5Cash.Name = "btnF5Cash"
        Me.btnF5Cash.SetArticleCode = Nothing
        Me.btnF5Cash.SetRowIndex = 0
        Me.btnF5Cash.Size = New System.Drawing.Size(183, 67)
        Me.btnF5Cash.TabIndex = 0
        Me.btnF5Cash.Text = " F5   " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Cash" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.btnF5Cash.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnF5Cash.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnF5Cash.UseVisualStyleBackColor = True
        Me.btnF5Cash.Visible = False
        Me.btnF5Cash.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.remarksTextbox, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(632, 2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.Padding = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(126, 100)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'remarksTextbox
        '
        Me.remarksTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.remarksTextbox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.remarksTextbox.Location = New System.Drawing.Point(3, 33)
        Me.remarksTextbox.Name = "remarksTextbox"
        Me.remarksTextbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.remarksTextbox.Size = New System.Drawing.Size(115, 64)
        Me.remarksTextbox.TabIndex = 1
        Me.remarksTextbox.Text = ""
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Verdana", 11.25!)
        Me.Label1.Location = New System.Drawing.Point(3, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Remark"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'C1SizerMain
        '
        Me.C1SizerMain.Border.Color = System.Drawing.Color.LightSlateGray
        Me.C1SizerMain.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.C1SizerMain.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.C1SizerMain.Controls.Add(Me.sizboxBottom)
        Me.C1SizerMain.Controls.Add(Me.sizDetail)
        Me.C1SizerMain.Controls.Add(Me.sizSaveBtn)
        Me.C1SizerMain.Controls.Add(Me.TableLayoutPanel1)
        Me.C1SizerMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1SizerMain.GridDefinition = "19.5694716242661:False:True;63.7964774951076:False:False;15.0684931506849:False:T" & _
    "rue;" & Global.Microsoft.VisualBasic.ChrW(9) & "82.6315789473684:False:False;16.5789473684211:False:False;"
        Me.C1SizerMain.Location = New System.Drawing.Point(0, 0)
        Me.C1SizerMain.Margin = New System.Windows.Forms.Padding(1)
        Me.C1SizerMain.Name = "C1SizerMain"
        Me.C1SizerMain.Padding = New System.Windows.Forms.Padding(1)
        Me.C1SizerMain.Size = New System.Drawing.Size(760, 511)
        Me.C1SizerMain.SplitterWidth = 2
        Me.C1SizerMain.TabIndex = 3
        Me.C1SizerMain.Text = "C1Sizer1"
        '
        'sizboxBottom
        '
        Me.sizboxBottom.Border.Color = System.Drawing.Color.LightSlateGray
        Me.sizboxBottom.Border.Corners = New C1.Win.C1Sizer.Corners(4, 4, 4, 4)
        Me.sizboxBottom.Border.Thickness = New System.Windows.Forms.Padding(1)
        Me.sizboxBottom.Controls.Add(Me.lblCalCurrencyHeader)
        Me.sizboxBottom.Controls.Add(Me.lblDefaultCurrencyTrack)
        Me.sizboxBottom.Controls.Add(Me.lblCalCurrencyMiniBalDue)
        Me.sizboxBottom.Controls.Add(Me.lblBillAmount)
        Me.sizboxBottom.Controls.Add(Me.lblCurrencyMinimumBalAmt)
        Me.sizboxBottom.Controls.Add(Me.lblBillAmt)
        Me.sizboxBottom.Controls.Add(Me.lblCalCurrencyBalanceDue)
        Me.sizboxBottom.Controls.Add(Me.lblDefaultCurrency)
        Me.sizboxBottom.Controls.Add(Me.lblCurrencyBalanceDue)
        Me.sizboxBottom.Controls.Add(Me.lblTotalReciepts)
        Me.sizboxBottom.Controls.Add(Me.lblCalCurrencyTotalReciepts)
        Me.sizboxBottom.Controls.Add(Me.lblCalMinBalDue)
        Me.sizboxBottom.Controls.Add(Me.lblCurrencyTotalReciept)
        Me.sizboxBottom.Controls.Add(Me.lblTotalReciept)
        Me.sizboxBottom.Controls.Add(Me.lblCalCurrencyBillAmount)
        Me.sizboxBottom.Controls.Add(Me.lblMinimumBalanceAmount)
        Me.sizboxBottom.Controls.Add(Me.lblCurrencyBillAmount)
        Me.sizboxBottom.Controls.Add(Me.lblDefaultBalanceDue)
        Me.sizboxBottom.Controls.Add(Me.lblbalanceDue)
        Me.sizboxBottom.GridDefinition = resources.GetString("sizboxBottom.GridDefinition")
        Me.sizboxBottom.Location = New System.Drawing.Point(2, 2)
        Me.sizboxBottom.Margin = New System.Windows.Forms.Padding(0)
        Me.sizboxBottom.Name = "sizboxBottom"
        Me.sizboxBottom.Padding = New System.Windows.Forms.Padding(0)
        Me.sizboxBottom.Size = New System.Drawing.Size(628, 100)
        Me.sizboxBottom.SplitterWidth = 1
        Me.sizboxBottom.TabIndex = 3
        '
        'frmNAcceptPayment
        '
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(760, 511)
        Me.Controls.Add(Me.C1SizerMain)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(300, 15)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(760, 545)
        Me.Name = "frmNAcceptPayment"
        Me.Text = "Accept Payment"
        CType(Me.lblDefaultCurrencyTrack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCalCurrencyHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCalCurrencyMiniBalDue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrencyMinimumBalAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCalCurrencyBalanceDue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrencyBalanceDue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCalCurrencyTotalReciepts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrencyTotalReciept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCalCurrencyBillAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCurrencyBillAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDefaultCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCalMinBalDue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMinimumBalanceAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblbalanceDue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDefaultBalanceDue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalReciept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalReciepts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBillAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBillAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgGridReciept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSwapCard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sizDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sizDetail.ResumeLayout(False)
        Me.sizDetail.PerformLayout()
        CType(Me.C1SizerPaymentTypes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1SizerPaymentTypes.ResumeLayout(False)
        Me.C1SizerPaymentTypes.PerformLayout()
        CType(Me.C1SizerPaymentMode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1SizerPaymentMode.ResumeLayout(False)
        Me.C1SizerPaymentMode.PerformLayout()
        CType(Me.LblPayTerm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrlPayTerm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSign, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRetrunAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReceiptType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSelectCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboRecieptType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sizSaveBtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sizSaveBtn.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.C1SizerMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1SizerMain.ResumeLayout(False)
        CType(Me.sizboxBottom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sizboxBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblBillAmount As Spectrum.CtrlLabel
    Friend WithEvents lblBillAmt As Spectrum.CtrlLabel
    Friend WithEvents lblbalanceDue As Spectrum.CtrlLabel
    Friend WithEvents lblDefaultBalanceDue As Spectrum.CtrlLabel
    Friend WithEvents lblTotalReciept As Spectrum.CtrlLabel
    Friend WithEvents lblTotalReciepts As Spectrum.CtrlLabel
    Friend WithEvents lblCalMinBalDue As Spectrum.CtrlLabel
    Friend WithEvents lblMinimumBalanceAmount As Spectrum.CtrlLabel
    Friend WithEvents lblDefaultCurrency As Spectrum.CtrlLabel
    Friend WithEvents lblCalCurrencyHeader As Spectrum.CtrlLabel
    Friend WithEvents lblCalCurrencyMiniBalDue As Spectrum.CtrlLabel
    Friend WithEvents lblCurrencyMinimumBalAmt As Spectrum.CtrlLabel
    Friend WithEvents lblCalCurrencyBalanceDue As Spectrum.CtrlLabel
    Friend WithEvents lblCurrencyBalanceDue As Spectrum.CtrlLabel
    Friend WithEvents lblCalCurrencyTotalReciepts As Spectrum.CtrlLabel
    Friend WithEvents lblCurrencyTotalReciept As Spectrum.CtrlLabel
    Friend WithEvents lblCalCurrencyBillAmount As Spectrum.CtrlLabel
    Friend WithEvents lblCurrencyBillAmount As Spectrum.CtrlLabel
    Friend WithEvents dgGridReciept As Spectrum.CtrlGrid
    Friend WithEvents rtxtSwipeCard As System.Windows.Forms.RichTextBox
    Friend WithEvents lblSwapCard As Spectrum.CtrlLabel
    Friend WithEvents sizDetail As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents btnF3 As Spectrum.CtrlBtn
    Friend WithEvents btnF4CreditNote As Spectrum.CtrlBtn
    Friend WithEvents btnF6 As Spectrum.CtrlBtn
    Friend WithEvents sizSaveBtn As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents btnOK As Spectrum.CtrlBtn
    Friend WithEvents btnCancel As Spectrum.CtrlBtn
    Friend WithEvents btnGift As Spectrum.CtrlBtn
    Friend WithEvents btnSave As Spectrum.CtrlBtn
    Friend WithEvents btndelete As Spectrum.CtrlBtn
    Friend WithEvents btnapprove As Spectrum.CtrlBtn
    Friend WithEvents lblSelectCurrency As Spectrum.CtrlLabel
    Friend WithEvents lblReceiptType As Spectrum.CtrlLabel
    Friend WithEvents cboRecieptType As Spectrum.ctrlCombo
    Friend WithEvents cboCurrency As Spectrum.ctrlCombo
    Friend WithEvents ctrlPayCredit As Spectrum.ctrlCreditCard
    Friend WithEvents ctrlPayCheque As Spectrum.ctrlCheque
    Friend WithEvents ctrlPayCash As Spectrum.ctrlCash
    Friend WithEvents lblAmount As Spectrum.CtrlLabel
    Friend WithEvents lblDefaultCurrencyTrack As Spectrum.CtrlLabel
    Friend WithEvents lblSign As Spectrum.CtrlLabel
    Friend WithEvents CtrlGiftVoucherIssue As Spectrum.CtrlGiftVoucher
    Friend WithEvents CtrlPayCreditCheque As Spectrum.ctrlCreditCheck
    Friend WithEvents lblRetrunAmount As Spectrum.CtrlLabel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents remarksTextbox As System.Windows.Forms.RichTextBox
    Friend WithEvents CtrlCLPPoint As Spectrum.CtrlCLPPoint
    Friend WithEvents CtrlChequeDetails As Spectrum.ctrlChequeDetails
    Friend WithEvents C1SizerMain As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents sizboxBottom As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents C1SizerPaymentTypes As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents C1SizerPaymentMode As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents LblPayTerm As Spectrum.CtrlLabel
    Friend WithEvents CtrlPayTerm As Spectrum.ctrlCombo
    Friend WithEvents btnF5Cash As Spectrum.CtrlBtn

End Class