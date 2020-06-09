Imports SpectrumBL
Public Class frmReportList
    Dim dtList As DataTable
    Dim dtReports As DataTable
    Private Sub frmReportList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Height = MDISpectrum.Height - 10
            Me.Width = MDISpectrum.Width - 5
            Dim obj As New clsReports
            dtList = obj.LoadModules(clsAdmin.LangCode)
            dtReports = obj.LoadReportsName(clsAdmin.LangCode)
            If Not dtList Is Nothing AndAlso dtList.Rows.Count > 0 Then
                For Each dr As DataRow In dtList.Rows
                    Dim Node As New TreeNode(dr("Description").ToString())
                    Node.Name = dr("Code")
                    trvList.Nodes.Add(Node)
                    addNodes(dtReports, dr("Code"), Node)
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub addNodes(ByVal dt As DataTable, ByVal Treecode As String, ByRef nodes As TreeNode)
        Dim dv As New DataView(dt, "ModCode='" & Treecode & "'", "ReportName", DataViewRowState.CurrentRows)
        For Each row As DataRowView In dv
            Dim Node As New TreeNode(row("ReportName"))
            Node.Name = row("Code")
            Node.Tag = 1
            nodes.Nodes.Add(Node) '.Nodes.Add(Node)
            'addchildNodes(dt, Node, 2, Treecode)
        Next
    End Sub
    Private Sub trvList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvList.DoubleClick
        Try
            trvList_KeyDown(trvList, New System.Windows.Forms.KeyEventArgs(Keys.Enter))
        Catch ex As Exception
        End Try
    End Sub
    Private Sub trvList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trvList.KeyDown
        Try
            If e.KeyCode = Keys.Enter AndAlso trvList.SelectedNode.Tag = 1 Then
                Dim direct As Boolean = False
                Dim LangCode As String = ""
                Dim dvRep As New DataView(dtReports, "Code='" & trvList.SelectedNode.Name & "' AND ModCode='" & trvList.SelectedNode.Parent.Name & "'", "", DataViewRowState.CurrentRows)
                direct = IIf(dvRep.Item(0)("DirectReport") Is DBNull.Value, False, dvRep.Item(0)("DirectReport"))
                LangCode = IIf(dvRep.Item(0)("ReportLangCode") Is DBNull.Value, "", dvRep.Item(0)("ReportLangCode").ToString())
                If direct = False Then
                    Dim frm As New frmReportProducer(trvList.SelectedNode.Parent.Name, trvList.SelectedNode.Name, trvList.SelectedNode.Text, dvRep.ToTable(), LangCode)
                    frm.TopMost = False
                    'frm.WindowState = FormWindowState.Maximize
                    frm.Height = Me.Height
                    frm.Width = Me.Width
                    frm.StartPosition = FormStartPosition.CenterParent
                    frm.ShowDialog()
                Else
                    Dim frm As New frmDirectReport(trvList.SelectedNode.Parent.Name, trvList.SelectedNode.Name, trvList.SelectedNode.Text, dvRep.ToTable(), LangCode)
                    frm.TopMost = False
                    frm.Height = Me.Height
                    frm.Width = Me.Width
                    frm.StartPosition = FormStartPosition.CenterParent
                    frm.ShowDialog()
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub
End Class
