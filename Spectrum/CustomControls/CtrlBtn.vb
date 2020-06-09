Imports C1.Win.C1Input.C1Button

Public Class CtrlBtn
    Inherits C1.Win.C1Input.C1Button

    Public WithEvents DbtnPayNEFT As New C1.Win.C1Ribbon.RibbonButton  '' added by nikhil for PC
    Public WithEvents DbtnPayRTGS As New C1.Win.C1Ribbon.RibbonButton

    Private _SetArticleCode As String
    Public Property SetArticleCode() As String
        Get
            Return _SetArticleCode
        End Get
        Set(ByVal value As String)
            _SetArticleCode = value
        End Set
    End Property

    Private _SetRowIndex As Integer
    Public Property SetRowIndex() As Integer
        Get
            Return _SetRowIndex
        End Get
        Set(ByVal value As Integer)
            _SetRowIndex = value
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

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        'Add your custom paint code here
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue
        Me.TextImageRelation = Windows.Forms.TextImageRelation.ImageAboveText
        Me.TextAlign = ContentAlignment.BottomCenter
        Me.ImageAlign = ContentAlignment.TopCenter

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    'Private Sub CtrlBtn_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.TextChanged
    '    If UCase(Me.Text) = UCase("ok") Or UCase(Me.Text) = UCase("Cancel") Then
    '        Me.Text = "&" & Me.Text
    '    End If
    'End Sub

End Class
