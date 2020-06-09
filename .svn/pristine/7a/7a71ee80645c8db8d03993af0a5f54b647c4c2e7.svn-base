Partial Class AutoCompleteTextBox
    Inherits TextBox

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        MyClass.New()

        'Required for Windows.Forms Class Composition Designer support
        If (container IsNot Nothing) Then
            container.Add(Me)
        End If

    End Sub

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()
        AddHandler Me.KeyDown, AddressOf Me.this_KeyDown
        AddHandler Me.KeyUp, AddressOf Me.this_KeyUp

    End Sub

    'Component overrides dispose to clean up the component list.
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

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me._listBox = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        '_listBox
        '
        Me._listBox.Location = New System.Drawing.Point(0, 0)
        Me._listBox.Name = "_listBox"
        Me._listBox.Size = New System.Drawing.Size(120, 95)
        Me._listBox.TabIndex = 0
        '
        'AutoCompleteTextBox
        '
        Me.MaxLength = 35
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents _listBox As System.Windows.Forms.ListBox

End Class
