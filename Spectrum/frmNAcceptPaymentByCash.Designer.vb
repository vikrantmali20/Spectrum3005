<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNAcceptPaymentByCash
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblBillAmount = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCalCollectAmount = New System.Windows.Forms.TextBox()
        Me.btnGift = New Spectrum.CtrlBtn()
        Me.btnSave = New Spectrum.CtrlBtn()
        Me.LbMinAmount = New System.Windows.Forms.Label()
        Me.txtMinAmount = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnCancle = New Spectrum.CtrlBtn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.RichTextBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(12, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(138, 17)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Bill Amount       :"
        '
        'lblBillAmount
        '
        Me.lblBillAmount.AutoSize = True
        Me.lblBillAmount.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblBillAmount.ForeColor = System.Drawing.Color.Red
        Me.lblBillAmount.Location = New System.Drawing.Point(177, 13)
        Me.lblBillAmount.Name = "lblBillAmount"
        Me.lblBillAmount.Size = New System.Drawing.Size(46, 18)
        Me.lblBillAmount.TabIndex = 7
        Me.lblBillAmount.Text = "0,00"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(12, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(137, 17)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Collect Amount :"
        '
        'txtCalCollectAmount
        '
        Me.txtCalCollectAmount.Font = New System.Drawing.Font("Verdana", 12.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCalCollectAmount.Location = New System.Drawing.Point(170, 72)
        Me.txtCalCollectAmount.Name = "txtCalCollectAmount"
        Me.txtCalCollectAmount.Size = New System.Drawing.Size(259, 27)
        Me.txtCalCollectAmount.TabIndex = 9
        '
        'btnGift
        '
        Me.btnGift.Image = Global.Spectrum.My.Resources.Resources.save_gift_print
        Me.btnGift.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnGift.Location = New System.Drawing.Point(153, 3)
        Me.btnGift.MoveToNxtCtrl = Nothing
        Me.btnGift.Name = "btnGift"
        Me.btnGift.SetArticleCode = Nothing
        Me.btnGift.SetRowIndex = 0
        Me.btnGift.Size = New System.Drawing.Size(145, 54)
        Me.btnGift.TabIndex = 16
        Me.btnGift.Text = "F11" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Save/Gift Print" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.btnGift.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnGift.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnGift.UseVisualStyleBackColor = True
        Me.btnGift.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnSave
        '
        Me.btnSave.Image = Global.Spectrum.My.Resources.Resources.save_print_btn
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnSave.Location = New System.Drawing.Point(-3, 3)
        Me.btnSave.MoveToNxtCtrl = Nothing
        Me.btnSave.Name = "btnSave"
        Me.btnSave.SetArticleCode = Nothing
        Me.btnSave.SetRowIndex = 0
        Me.btnSave.Size = New System.Drawing.Size(150, 54)
        Me.btnSave.TabIndex = 15
        Me.btnSave.Text = "F10" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " Save Print" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnSave.UseVisualStyleBackColor = True
        Me.btnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'LbMinAmount
        '
        Me.LbMinAmount.AutoSize = True
        Me.LbMinAmount.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LbMinAmount.Location = New System.Drawing.Point(13, 41)
        Me.LbMinAmount.Name = "LbMinAmount"
        Me.LbMinAmount.Size = New System.Drawing.Size(147, 17)
        Me.LbMinAmount.TabIndex = 17
        Me.LbMinAmount.Text = "Min Adv Amount :"
        '
        'txtMinAmount
        '
        Me.txtMinAmount.AutoSize = True
        Me.txtMinAmount.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold)
        Me.txtMinAmount.ForeColor = System.Drawing.Color.Red
        Me.txtMinAmount.Location = New System.Drawing.Point(176, 41)
        Me.txtMinAmount.Name = "txtMinAmount"
        Me.txtMinAmount.Size = New System.Drawing.Size(46, 18)
        Me.txtMinAmount.TabIndex = 18
        Me.txtMinAmount.Text = "0,00"
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel1.AutoSize = True
        Me.Panel1.Controls.Add(Me.btnCancle)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.btnGift)
        Me.Panel1.Location = New System.Drawing.Point(10, 176)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(453, 60)
        Me.Panel1.TabIndex = 19
        '
        'btnCancle
        '
        Me.btnCancle.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnCancle.Location = New System.Drawing.Point(304, 3)
        Me.btnCancle.MoveToNxtCtrl = Nothing
        Me.btnCancle.Name = "btnCancle"
        Me.btnCancle.SetArticleCode = Nothing
        Me.btnCancle.SetRowIndex = 0
        Me.btnCancle.Size = New System.Drawing.Size(145, 54)
        Me.btnCancle.TabIndex = 17
        Me.btnCancle.Text = "Cancle"
        Me.btnCancle.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCancle.UseVisualStyleBackColor = True
        Me.btnCancle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(12, 117)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 17)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "Remark :"
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(168, 111)
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(261, 51)
        Me.txtRemark.TabIndex = 21
        Me.txtRemark.Text = ""
        '
        'frmNAcceptPaymentByCash
        '
        Me.ClientSize = New System.Drawing.Size(463, 244)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.LbMinAmount)
        Me.Controls.Add(Me.txtMinAmount)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblBillAmount)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtCalCollectAmount)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNAcceptPaymentByCash"
        Me.Text = "Accept Payment By Cash"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblBillAmount As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCalCollectAmount As System.Windows.Forms.TextBox
    Friend WithEvents btnGift As Spectrum.CtrlBtn
    Friend WithEvents btnSave As Spectrum.CtrlBtn
    Friend WithEvents LbMinAmount As System.Windows.Forms.Label
    Friend WithEvents txtMinAmount As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.RichTextBox
    Friend WithEvents btnCancle As Spectrum.CtrlBtn

End Class
