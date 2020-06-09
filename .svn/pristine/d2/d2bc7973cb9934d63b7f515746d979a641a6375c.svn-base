Imports Spectrum
Imports SpectrumBL
Imports System.Data
Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid
Imports System.Text
Imports System.IO
Imports Microsoft.Reporting.WinForms

Public Class frmHierarchyPopUp

    Dim objArticle As New clsArticleCombo
    Dim dtCustmData As New DataTable
    Dim dtArticle As New DataTable
    Dim objItem As New clsIteamSearch
    Dim dtGuest As New DataTable
    Dim StrFilter As String
    Dim dtTree As DataTable
    Dim dt As DataTable
    Dim opendate As Date
    Dim _SelectedNodeCode As String
    Dim _SelectedNodeName As String
    Dim _CheckedHierarachy As Boolean = False
    Public Property SelectedNodeCode() As String
        Get
            Return _SelectedNodeCode
        End Get
        Set(ByVal value As String)
            _SelectedNodeCode = value
        End Set
    End Property
    Public Property SelectedNodeName() As String
        Get
            Return _SelectedNodeName
        End Get
        Set(ByVal value As String)
            _SelectedNodeName = value
        End Set
    End Property
    Public Property CheckedHierarachy() As Boolean
        Get
            Return _CheckedHierarachy
        End Get
        Set(ByVal value As Boolean)
            _CheckedHierarachy = value
        End Set
    End Property
    Public _IsCallFromCombo As Boolean = False
    Public Property IsCallFromCombo() As Boolean
        Get
            Return _IsCallFromCombo
        End Get
        Set(ByVal value As Boolean)
            _IsCallFromCombo = value
        End Set
    End Property

    Private Sub frmHierarchyPopUp_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            If MessageBox.Show("Your will loose unsaved data, Do you wish to continue?", "Information", MessageBoxButtons.YesNoCancel) = DialogResult.Yes Then
                Me.Close()
            End If
        End If
    End Sub



    Private Sub frmNHierarchyWiseNetSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            trvArticle.Visible = True
            trvArticle.Nodes.Clear()
            trvArticle.Nodes.Add("root", getValueByKey("ih001"))
            dtTree = objItem.getarticleTrees(clsAdmin.SiteCode)
            dt = objItem.GetArticleTree()
            If Not dtTree Is Nothing AndAlso Not dt Is Nothing Then
                For Each dr As DataRow In dtTree.Rows
                    Dim Node As New TreeNode(dr("treename").ToString())
                    Node.Name = dr("treecode")
                    '  Node .Parent =dr("")
                    trvArticle.Nodes("root").Nodes.Add(Node)
                    addNodes(dt, dr("Treecode"), Node)
                Next
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            If CheckedHierarachy Then
                trvArticle.CheckBoxes = True
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function GetCheck(ByVal node As TreeNodeCollection) As ArrayList
        Dim lN As New ArrayList
        For Each n As TreeNode In node
            If n.Checked Then lN.Add(n)
            lN.AddRange(GetCheck(n.Nodes))
        Next

        Return lN
    End Function
    Public Function Themechange() As String
        Me.BackColor = Color.FromArgb(76, 76, 76)

        'cmdok button
        CmdOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CmdOk.BackColor = Color.Transparent
        CmdOk.BackColor = Color.FromArgb(0, 107, 163)
        CmdOk.ForeColor = Color.FromArgb(255, 255, 255)
        CmdOk.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CmdOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CmdOk.FlatStyle = FlatStyle.Flat
        CmdOk.FlatAppearance.BorderSize = 0
        CmdOk.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
    End Function


    'Private Sub trvArticle_BeforeCheck(sender As Object, e As TreeViewCancelEventArgs) Handles trvArticle.BeforeCheck
    '    If Not e.Node.Checked Then
    '        '  Me.addchildNodes(e.Node)
    '    End If
    'End Sub



    Private Sub trvArticle_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles trvArticle.AfterCheck
        Try
            If e.Action <> TreeViewAction.Unknown Then

                If e.Node.Nodes.Count > 0 Then

                    Me.CheckAllChildNodes(e.Node, e.Node.Checked)

                End If

            End If

            SelectParents(e.Node, e.Node.Checked)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub CheckAllChildNodes(treeNode As TreeNode, nodeChecked As Boolean)

        For Each node As TreeNode In treeNode.Nodes

            node.Checked = nodeChecked

            If node.Nodes.Count > 0 Then

                ' If the current node has child nodes, call the CheckAllChildsNodes method recursively.


                Me.CheckAllChildNodes(node, nodeChecked)

            End If
        Next

    End Sub

    Private Sub SelectParents(node As TreeNode, isChecked As [Boolean])
        Dim parent = node.Parent
        If isChecked = True Then
            ' cmdPdf.Enabled = True
            ' cmdExcel.Enabled = True
        Else
            ' cmdPdf.Enabled = False
            ' cmdExcel.Enabled = False
        End If
        If parent Is Nothing Then
            Return
        End If

        If Not isChecked AndAlso HasCheckedNode(parent) Then
            Return
        End If

        parent.Checked = isChecked
        SelectParents(parent, isChecked)
    End Sub
    Private Sub addNodes(ByVal dt As DataTable, ByVal Treecode As String, ByRef nodes As TreeNode)
        Dim dv As New DataView(dt, "isnull(ParentNodeCode,'')='' And TreeCode='" & Treecode & "'", "NodeCode", DataViewRowState.CurrentRows)
        For Each row As DataRowView In dv
            Dim Node As New TreeNode(row("NodeName"))
            Node.Name = row("NodeCode")
            Node.Tag = 1
            nodes.Nodes.Add(Node) '.Nodes.Add(Node)
            addchildNodes(dt, Node, 2, Treecode)
        Next
    End Sub
    Private Sub addchildNodes(ByVal dt As DataTable, ByRef nod As TreeNode, ByVal Level As Int16, ByVal treecode As String)
        Dim dv1 As New DataView(dt, "isnull(ParentNodeCode,'')='" & nod.Name & "' And TreeCode='" & treecode & "'", "NodeCode", DataViewRowState.CurrentRows)
        For Each row As DataRowView In dv1
            Dim Node As New TreeNode(row("NodeName"))
            Node.Name = row("NodeCode")

            Node.Tag = Level
            nod.Nodes.Add(Node)
            If row("IsThisLastNode") = True Then
                'Exit For
            Else
                addchildNodes(dt, Node, Level + 1, treecode)
            End If
        Next
    End Sub
    Private Function HasCheckedNode(node As TreeNode) As Boolean
        Return node.Nodes.Cast(Of TreeNode)().Any(Function(n) n.Checked)
    End Function

    Private Sub HandleOnTreeViewAfterCheck(sender As [Object], e As TreeViewEventArgs)
        CheckTreeViewNode(e.Node, e.Node.Checked)
    End Sub

    Private Sub CheckTreeViewNode(node As TreeNode, isChecked As [Boolean])
        For Each item As TreeNode In node.Nodes
            item.Checked = isChecked

            If item.Nodes.Count > 0 Then
                Me.CheckTreeViewNode(item, isChecked)
            End If
        Next
    End Sub

    Private Sub CheckChildNodes(ByVal parent As TreeNode, checked As Boolean)
        For Each child As TreeNode In parent.Nodes
            child.Checked = checked
            If child.Nodes.Count > 0 Then CheckChildNodes(child, checked)
        Next
    End Sub
    Private Function DeleteGuestDetails(srNo As Integer) As Boolean
        Try
            If dtGuest.Rows.Count > 0 Then
                Dim drDtl() = dtGuest.Select("SrNo=" & srNo & "")
                If drDtl.Count > 0 Then
                    For Each row As DataRow In drDtl
                        dtGuest.Rows.Remove(row)
                    Next
                End If
                dtGuest.AcceptChanges()
                Dim count = 1
                For index = 0 To dtGuest.Rows.Count - 1
                    dtGuest.Rows(index)("SrNo") = count
                    count += 1
                Next
                srNo = count
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

   

    Private Sub CmdOk_Click(sender As Object, e As EventArgs) Handles CmdOk.Click
        _SelectedNodeCode = trvArticle.SelectedNode.Name


        Dim articleCode As String = ""
        Dim articleName As String = ""
        Dim dtTreeView As New DataTable
        dtTreeView.Clear()
        dtTreeView.Columns.Add("ArticleName", GetType(String))

        Dim treeList As New List(Of TreeNode)()
        For Each treeNode As TreeNode In GetCheck(trvArticle.Nodes)

            If treeNode.Nodes.Count = 0 Then
                dtTreeView = dt.Select("NODECODE='" & treeNode.Name.Trim & "'").CopyToDataTable
                For Each dtnodetree In dtTreeView.Rows
                    If articleCode.Trim() = "" Or articleCode.Trim Is Nothing Then
                        'articleCode = dtTreeView.Rows(0)(0).ToString.
                        articleCode = "'" + dtnodetree("nodecode").ToString.Trim + "'"
                        articleName = dtnodetree("nodename").ToString.Trim
                    Else
                        articleCode = articleCode & ",'" & dtnodetree("nodecode").ToString.Trim.ToString.Trim + "'"
                        articleName = articleName & "," & dtnodetree("nodename").ToString.Trim.ToString.Trim
                    End If
                Next
                ' dtTreeView.Rows.Add(treeNode.Text)
            End If
        Next



        If CheckedHierarachy Then
            _SelectedNodeCode = articleCode
        End If
        _SelectedNodeName = articleName

        If _SelectedNodeCode = "root" Then
            If Not CheckedHierarachy Then
                ShowMessage("Please select Items from the Hierarchy.", "Information.")
                _SelectedNodeCode = ""
                Exit Sub
            End If
        End If
        If _IsCallFromCombo Then
            If Not CheckedHierarachy Then
                If objArticle.IsCodeLastNode(_SelectedNodeCode) = False Then
                    ShowMessage("Please Select Valid Node.", "Information.")
                    Exit Sub
                End If
            End If
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK

        Me.Close()
    End Sub

   
End Class