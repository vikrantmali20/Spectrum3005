<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNReprintSearch
    Inherits CtrlPopupForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNReprintSearch))
        Me.grdDocumentInfo = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.BtnSearchOK = New Spectrum.CtrlBtn
        Me.BtnSearchCancel = New Spectrum.CtrlBtn
        Me.grdDocumentInfo1 = New C1.Win.C1TrueDBGrid.C1TrueDBGrid
        CType(Me.grdDocumentInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdDocumentInfo1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdDocumentInfo
        '
        Me.grdDocumentInfo.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.grdDocumentInfo.AllowEditing = False
        Me.grdDocumentInfo.AutoResize = False
        Me.grdDocumentInfo.ColumnInfo = "1,1,0,0,0,90,Columns:0{Width:1;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.grdDocumentInfo.ExtendLastCol = True
        Me.grdDocumentInfo.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.grdDocumentInfo.Location = New System.Drawing.Point(500, 365)
        Me.grdDocumentInfo.Name = "grdDocumentInfo"
        Me.grdDocumentInfo.NewRowWatermark = ""
        Me.grdDocumentInfo.Rows.Count = 1
        Me.grdDocumentInfo.Rows.DefaultSize = 18
        Me.grdDocumentInfo.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.grdDocumentInfo.Size = New System.Drawing.Size(42, 19)
        Me.grdDocumentInfo.StyleInfo = resources.GetString("grdDocumentInfo.StyleInfo")
        Me.grdDocumentInfo.TabIndex = 4
        Me.grdDocumentInfo.Tag = ""
        Me.grdDocumentInfo.Visible = False
        '
        'BtnSearchOK
        '
        Me.BtnSearchOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnSearchOK.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnSearchOK.Location = New System.Drawing.Point(333, 534)
        Me.BtnSearchOK.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnSearchOK.Name = "BtnSearchOK"
        Me.BtnSearchOK.Size = New System.Drawing.Size(67, 21)
        Me.BtnSearchOK.TabIndex = 62
        Me.BtnSearchOK.Text = "&OK"
        Me.BtnSearchOK.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnSearchOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnSearchOK.UseVisualStyleBackColor = True
        Me.BtnSearchOK.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'BtnSearchCancel
        '
        Me.BtnSearchCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSearchCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnSearchCancel.Location = New System.Drawing.Point(640, 536)
        Me.BtnSearchCancel.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnSearchCancel.Name = "BtnSearchCancel"
        Me.BtnSearchCancel.Size = New System.Drawing.Size(67, 21)
        Me.BtnSearchCancel.TabIndex = 63
        Me.BtnSearchCancel.Text = "&Cancel"
        Me.BtnSearchCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnSearchCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnSearchCancel.UseVisualStyleBackColor = True
        Me.BtnSearchCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        '
        'grdDocumentInfo1
        '
        Me.grdDocumentInfo1.AllowColMove = False
        Me.grdDocumentInfo1.AllowColSelect = False
        Me.grdDocumentInfo1.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.grdDocumentInfo1.AllowUpdate = False
        Me.grdDocumentInfo1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdDocumentInfo1.CaptionHeight = 17
        Me.grdDocumentInfo1.EmptyRows = True
        Me.grdDocumentInfo1.ExtendRightColumn = True
        Me.grdDocumentInfo1.FetchRowStyles = True
        Me.grdDocumentInfo1.FilterBar = True
        Me.grdDocumentInfo1.GroupByCaption = "Drag a column header here to group by that column"
        Me.grdDocumentInfo1.Images.Add(CType(resources.GetObject("grdDocumentInfo1.Images"), System.Drawing.Image))
        Me.grdDocumentInfo1.Location = New System.Drawing.Point(12, 7)
        Me.grdDocumentInfo1.MultiSelect = C1.Win.C1TrueDBGrid.MultiSelectEnum.None
        Me.grdDocumentInfo1.Name = "grdDocumentInfo1"
        Me.grdDocumentInfo1.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdDocumentInfo1.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.grdDocumentInfo1.PreviewInfo.ZoomFactor = 75
        Me.grdDocumentInfo1.PrintInfo.PageSettings = CType(resources.GetObject("grdDocumentInfo1.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.grdDocumentInfo1.RowHeight = 15
        Me.grdDocumentInfo1.Size = New System.Drawing.Size(768, 525)
        Me.grdDocumentInfo1.TabIndex = 64
        Me.grdDocumentInfo1.PropBag = resources.GetString("grdDocumentInfo1.PropBag")
        '
        'frmNReprintSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 566)
        Me.Controls.Add(Me.grdDocumentInfo1)
        Me.Controls.Add(Me.BtnSearchCancel)
        Me.Controls.Add(Me.BtnSearchOK)
        Me.Controls.Add(Me.grdDocumentInfo)
        Me.Name = "frmNReprintSearch"
        Me.Text = "Search Old Invoice"
        CType(Me.grdDocumentInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdDocumentInfo1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdDocumentInfo As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents BtnSearchOK As Spectrum.CtrlBtn
    Friend WithEvents BtnSearchCancel As Spectrum.CtrlBtn
    Friend WithEvents grdDocumentInfo1 As C1.Win.C1TrueDBGrid.C1TrueDBGrid
End Class
