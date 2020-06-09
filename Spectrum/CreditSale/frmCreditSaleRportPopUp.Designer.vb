<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreditSaleRportPopUp
    'Inherits System.Windows.Forms.Form
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmdDownload = New Spectrum.CtrlBtn()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdPDF = New System.Windows.Forms.RadioButton()
        Me.cmdExcel = New System.Windows.Forms.RadioButton()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdDownload
        '
        Me.cmdDownload.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdDownload.Location = New System.Drawing.Point(142, 108)
        Me.cmdDownload.Name = "cmdDownload"
        Me.cmdDownload.SetArticleCode = Nothing
        Me.cmdDownload.SetRowIndex = 0
        Me.cmdDownload.Size = New System.Drawing.Size(103, 23)
        Me.cmdDownload.TabIndex = 3
        Me.cmdDownload.Text = "Download"
        Me.cmdDownload.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdDownload.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.55752!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.44248!))
        Me.TableLayoutPanel1.Controls.Add(Me.cmdPDF, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmdExcel, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(58, 30)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58.18182!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41.81818!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(264, 55)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'cmdPDF
        '
        Me.cmdPDF.AutoSize = True
        Me.cmdPDF.Checked = True
        Me.cmdPDF.Location = New System.Drawing.Point(3, 3)
        Me.cmdPDF.Name = "cmdPDF"
        Me.cmdPDF.Size = New System.Drawing.Size(47, 17)
        Me.cmdPDF.TabIndex = 0
        Me.cmdPDF.TabStop = True
        Me.cmdPDF.Text = "PDF"
        Me.cmdPDF.UseVisualStyleBackColor = True
        '
        'cmdExcel
        '
        Me.cmdExcel.AutoSize = True
        Me.cmdExcel.Location = New System.Drawing.Point(133, 3)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(62, 17)
        Me.cmdExcel.TabIndex = 1
        Me.cmdExcel.TabStop = True
        Me.cmdExcel.Text = "EXCEL"
        Me.cmdExcel.UseVisualStyleBackColor = True
        '
        'frmCreditSaleRportPopUp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(387, 163)
        Me.Controls.Add(Me.cmdDownload)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "frmCreditSaleRportPopUp"
        Me.Text = "Credit Sale Report"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdDownload As Spectrum.CtrlBtn
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmdPDF As System.Windows.Forms.RadioButton
    Friend WithEvents cmdExcel As System.Windows.Forms.RadioButton
End Class
