Imports C1.Win.C1FlexGrid

Public Class CtrlGrid
    Inherits C1FlexGrid
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        '---- Code commented and Added By  Mahesh For KeyActionEnum for control
        'Me.KeyActionTab = KeyActionEnum.MoveAcrossOut
        Me.KeyActionTab = KeyActionEnum.None
        'Add your custom paint code here
    End Sub

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue
        Me.ExtendLastCol = True
        Me.ShowButtons = ShowButtonsEnum.Always
        Me.CellButtonImage = My.Resources.del_icon
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub pResizeCol()
        Me.AutoSizeCols()
    End Sub

    Private Sub CtrlGrid_AfterAddRow(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles Me.AfterAddRow
        pResizeCol()
    End Sub
End Class
