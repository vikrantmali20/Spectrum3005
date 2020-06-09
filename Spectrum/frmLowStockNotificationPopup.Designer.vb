<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLowStockNotificationPopup
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

    'Private Sub InitializeComponent()
    '    components = New System.ComponentModel.Container
    '    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    '    Me.Text = "frmLowStockNotificationPopup"
    'End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLowStockNotificationPopup))
        Me.grdPopUp = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblHeading = New Spectrum.CtrlLabel()
        Me.cmdExport = New System.Windows.Forms.Button()
        CType(Me.grdPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.lblHeading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdPopUp
        '
        Me.grdPopUp.AllowEditing = False
        Me.grdPopUp.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.grdPopUp.ColumnInfo = resources.GetString("grdPopUp.ColumnInfo")
        Me.grdPopUp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPopUp.ExtendLastCol = True
        Me.grdPopUp.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPopUp.Location = New System.Drawing.Point(13, 39)
        Me.grdPopUp.Name = "grdPopUp"
        Me.grdPopUp.Rows.Count = 1
        Me.grdPopUp.Rows.DefaultSize = 21
        Me.grdPopUp.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.grdPopUp.Size = New System.Drawing.Size(719, 436)
        Me.grdPopUp.StyleInfo = resources.GetString("grdPopUp.StyleInfo")
        Me.grdPopUp.TabIndex = 3
        Me.grdPopUp.TabStop = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.grdPopUp, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblHeading, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.Padding = New System.Windows.Forms.Padding(10)
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 442.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(745, 488)
        Me.TableLayoutPanel1.TabIndex = 4
        '
        'lblHeading
        '
        Me.lblHeading.AttachedTextBoxName = Nothing
        Me.lblHeading.AutoSize = True
        Me.lblHeading.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblHeading.BorderColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeading.Location = New System.Drawing.Point(13, 10)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(13, 20)
        Me.lblHeading.TabIndex = 2
        Me.lblHeading.Tag = Nothing
        Me.lblHeading.Text = ":"
        Me.lblHeading.TextDetached = True
        '
        'cmdExport
        '
        Me.cmdExport.BackColor = System.Drawing.Color.CornflowerBlue
        Me.cmdExport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExport.Location = New System.Drawing.Point(614, 0)
        Me.cmdExport.Name = "cmdExport"
        Me.cmdExport.Size = New System.Drawing.Size(104, 30)
        Me.cmdExport.TabIndex = 5
        Me.cmdExport.Text = "EXPORT"
        Me.cmdExport.UseVisualStyleBackColor = False
        '
        'frmLowStockNotificationPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(745, 488)
        Me.Controls.Add(Me.cmdExport)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLowStockNotificationPopup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Stocks below minimum quantity notifications"
        CType(Me.grdPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.lblHeading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    'Private Sub InitializeComponent()
    '    components = New System.ComponentModel.Container
    '    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    '    Me.Text = "frmLowStockNotificationPopup"
    'End Sub

    Friend WithEvents lblHeading As Spectrum.CtrlLabel
    Friend WithEvents grdPopUp As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmdExport As System.Windows.Forms.Button
End Class
