<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProductExpiryDatePopUp
    Inherits System.Windows.Forms.Form
    ' Inherits CtrlRbnBaseForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProductExpiryDatePopUp))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.grdPopUp = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.lblHeading = New Spectrum.CtrlLabel()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.grdPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblHeading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.grdPopUp, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblHeading, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.Padding = New System.Windows.Forms.Padding(10)
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 472.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(811, 520)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'grdPopUp
        '
        Me.grdPopUp.AllowEditing = False
        Me.grdPopUp.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.grdPopUp.AutoResize = True
        Me.grdPopUp.ColumnInfo = "4,0,0,5,0,105,Columns:0{Visible:False;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.grdPopUp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPopUp.ExtendLastCol = True
        Me.grdPopUp.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPopUp.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never
        Me.grdPopUp.Location = New System.Drawing.Point(13, 41)
        Me.grdPopUp.Name = "grdPopUp"
        Me.grdPopUp.Rows.Count = 1
        Me.grdPopUp.Rows.DefaultSize = 21
        Me.grdPopUp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.grdPopUp.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.grdPopUp.Size = New System.Drawing.Size(785, 466)
        Me.grdPopUp.StyleInfo = resources.GetString("grdPopUp.StyleInfo")
        Me.grdPopUp.TabIndex = 3
        Me.grdPopUp.TabStop = False
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
        Me.lblHeading.Size = New System.Drawing.Size(110, 20)
        Me.lblHeading.TabIndex = 2
        Me.lblHeading.Tag = Nothing
        Me.lblHeading.Text = "Expiry Product"
        Me.lblHeading.TextDetached = True
        '
        'btnExport
        '
        Me.btnExport.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExport.Location = New System.Drawing.Point(700, 10)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(98, 25)
        Me.btnExport.TabIndex = 6
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = False
        '
        'frmProductExpiryDatePopUp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(811, 520)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProductExpiryDatePopUp"
        Me.ShowIcon = False
        Me.Text = "ProductExpiryDatePopUp"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.grdPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblHeading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grdPopUp As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents lblHeading As Spectrum.CtrlLabel
    Friend WithEvents btnExport As System.Windows.Forms.Button
End Class
