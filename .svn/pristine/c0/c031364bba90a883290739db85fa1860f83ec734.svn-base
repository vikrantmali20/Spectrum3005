Public Class ctrlCombo

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        'Add your custom paint code here
    End Sub

    Private Sub ctrlCombo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim ctl As Control = DirectCast(sender, Control)
            Me.Parent.SelectNextControl(ctl, True, True, True, True)
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.ColumnHeaders = False
        Me.CaptionVisible = False

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private _strTxtDBColumn As String
    Public Property ctrlTextDbColumn() As String
        Get
            Return UCase(_strTxtDBColumn)
        End Get
        Set(ByVal value As String)
            _strTxtDBColumn = value
        End Set
    End Property

    Private _strSelectStmt As String
    Public Property strSelectStmt() As String
        Get
            Return UCase(_strSelectStmt)
        End Get
        Set(ByVal value As String)
            _strSelectStmt = value
        End Set
    End Property
    Private _MoveToNxtCtrl As Object
    Public Property MoveToNxtCtrl() As Object
        Get
            Return _MoveToNxtCtrl
        End Get
        Set(ByVal value As Object)
            _MoveToNxtCtrl = value
        End Set
    End Property
End Class
