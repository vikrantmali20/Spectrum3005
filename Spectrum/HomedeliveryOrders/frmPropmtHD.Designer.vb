<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPropmtHD
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.grdPopup = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.C1Sizer1 = New C1.Win.C1Sizer.C1Sizer()
        Me.btnCancel = New Spectrum.CtrlBtn()
        Me.btnSelect = New Spectrum.CtrlBtn()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.grdPopup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.C1Sizer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.246106!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 98.75389!))
        Me.TableLayoutPanel1.Controls.Add(Me.grdPopup, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.C1Sizer1, 1, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.29771!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.70229!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(741, 215)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'grdPopup
        '
        Me.grdPopup.AllowEditing = False
        Me.grdPopup.AutoResize = True
        Me.grdPopup.ColumnInfo = "10,1,0,0,0,85,Columns:"
        Me.grdPopup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPopup.Location = New System.Drawing.Point(12, 3)
        Me.grdPopup.Name = "grdPopup"
        Me.grdPopup.Rows.DefaultSize = 17
        Me.grdPopup.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.grdPopup.Size = New System.Drawing.Size(726, 168)
        Me.grdPopup.TabIndex = 0
        '
        'C1Sizer1
        '
        Me.C1Sizer1.Controls.Add(Me.btnCancel)
        Me.C1Sizer1.Controls.Add(Me.btnSelect)
        Me.C1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Sizer1.GridDefinition = "80:False:True;5.71428571428571:False:False;" & Global.Microsoft.VisualBasic.ChrW(9) & "32.7823691460055:False:False;16.52892" & _
    "56198347:False:True;16.5289256198347:False:True;33.1955922865014:False:False;"
        Me.C1Sizer1.Location = New System.Drawing.Point(12, 177)
        Me.C1Sizer1.Name = "C1Sizer1"
        Me.C1Sizer1.Padding = New System.Windows.Forms.Padding(2)
        Me.C1Sizer1.Size = New System.Drawing.Size(726, 35)
        Me.C1Sizer1.SplitterWidth = 1
        Me.C1Sizer1.TabIndex = 1
        Me.C1Sizer1.Text = "C1Sizer1"
        '
        'btnCancel
        '
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancel.Location = New System.Drawing.Point(362, 2)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.SetArticleCode = Nothing
        Me.btnCancel.SetRowIndex = 0
        Me.btnCancel.Size = New System.Drawing.Size(120, 28)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'btnSelect
        '
        Me.btnSelect.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSelect.Location = New System.Drawing.Point(241, 2)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.SetArticleCode = Nothing
        Me.btnSelect.SetRowIndex = 0
        Me.btnSelect.Size = New System.Drawing.Size(120, 28)
        Me.btnSelect.TabIndex = 0
        Me.btnSelect.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnSelect.UseVisualStyleBackColor = True
        Me.btnSelect.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'frmPropmt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(741, 215)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "frmPropmt"
        Me.Text = "frmPropmt"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.grdPopup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Sizer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.C1Sizer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents grdPopup As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1Sizer1 As C1.Win.C1Sizer.C1Sizer
    Friend WithEvents btnSelect As Spectrum.CtrlBtn
    Friend WithEvents btnCancel As Spectrum.CtrlBtn
End Class
