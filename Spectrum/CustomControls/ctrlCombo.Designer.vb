<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ctrlCombo
    Inherits C1.Win.C1List.C1Combo

    'Control overrides dispose to clean up the component list.
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

    'Required by the Control Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  Do not modify it
    ' using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ctrlCombo))
        CType(Me._dropDownList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        '_dropDownList
        '
        Me._dropDownList.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(251, Byte), Integer))
        Me._dropDownList.Images.Add(CType(resources.GetObject("_dropDownList.Images"), System.Drawing.Image))
        Me._dropDownList.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me._dropDownList.PropBag = resources.GetString("_dropDownList.PropBag")
        '
        'ctrlCombo
        '
        Me.AutoCompletion = True
        Me.AutoDropDown = True
        Me.Images.Add(CType(resources.GetObject("$this.Images"), System.Drawing.Image))
        Me.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue
        Me.PropBag = resources.GetString("$this.PropBag")
        CType(Me._dropDownList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

End Class

