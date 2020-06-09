Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports SpectrumBL
''' <summary>
''' This Class is used for Item searching
''' </summary>
''' <CreatedBy>Rama Ranjan Jena</CreatedBy>
''' <UpdatedBy></UpdatedBy>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
Public Class frmNItemSearch
#Region "Global Variable's"
    Public SearchResult() As String
    Public ItemRow As DataRow
    Public ItemRows As C1.Win.C1TrueDBGrid.SelectedRowCollection
    Dim StrFilter As String
#End Region
#Region "Global Variable's for Class"
    Dim dtSearch As New DataTable
    Dim objItem As New clsIteamSearch
#End Region
#Region "Class Events"
    Private Sub frmItemSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ThemeChange()
            End If
            objItem.strLangCode = clsAdmin.LangCode
            Dim condition As String
            condition = " AND A.ArticleCode <>'" & clsDefaultConfiguration.GVBaseArticle & "' AND A.ArticleCode <>'" & clsDefaultConfiguration.ClpBaseArticle & "' AND A.ArticleCode <>'" & clsAdmin.CVBaseArticle & "'"
            If dtItemScanData Is Nothing Then
                dtSearch = objItem.GetEANData(clsAdmin.SiteCode, "", clsAdmin.LangCode, condition, dtItemScanData)
            Else
                dtSearch = objItem.GetEANData(clsAdmin.SiteCode, "", clsAdmin.LangCode, condition, dtItemScanData)
                'dtSearch = dtItemScanData
            End If

            'createColumns(dtSearch)
            'fillGrid("EanType='" & EanType & "'")
            fillGrid("DefaultEAN=1")
            GridSetting()
            dgItemSearch.RecordSelectors = True
            'trvArticle.Nodes.Add("root", "Root")
            trvArticle.Nodes.Add("root", getValueByKey("ih001"))
            Dim dtTree As DataTable = objItem.getarticleTrees(clsAdmin.SiteCode)
            Dim dt As DataTable = objItem.GetArticleTree()
            If Not dtTree Is Nothing AndAlso Not dt Is Nothing Then
                For Each dr As DataRow In dtTree.Rows
                    Dim Node As New TreeNode(dr("treename").ToString())
                    Node.Name = dr("treecode")
                    trvArticle.Nodes("root").Nodes.Add(Node)
                    addNodes(dt, dr("Treecode"), Node)
                Next
            End If
            For Each col In dgItemSearch.Columns
                'col.filterdropdown = True
                col.FilterEscape = ""
                dgItemSearch.HeadingStyle.WrapText = False
            Next

            Me.Width = Screen.PrimaryScreen.WorkingArea.Width
            Me.Left = 0
        Catch ex As Exception
            ShowMessage(getValueByKey("IMS001"), "IMS001 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Form is not properly intialized", "Information")
        End Try
        SetCulture(Me, Me.Name)
    End Sub
    Private Sub createColumns(ByRef dt As DataTable)
        Try
            Dim i As Int32
            Dim MaxLen As String = dt.Compute("Max(Nodes)", "")
            'Dim dv As New DataView(dt, "Len(Nodes)=" & i, "", DataViewRowState.CurrentRows)
            'If dv.Count > 0 Then
            Dim tempstr As String = MaxLen
            Dim c(0) As Char
            c(0) = ","
            Dim col() As String = tempstr.Split(c)
            Dim str As String
            i = col.Length
            For Each str In col
                Dim cols As New DataColumn("Level" & i)
                dt.Columns.Add(cols)
                i = i - 1
            Next
            'End If
            For Each row As DataRow In dt.Rows
                tempstr = row("Nodes").ToString()
                Dim cols() As String = tempstr.Split(c)
                i = cols.Length
                For Each str In cols
                    Dim strData As String = str.Replace("'", "")
                    row("Level" & i) = strData
                    i = i - 1
                Next
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
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
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Try            
            Dim i As Integer
            If dgItemSearch.Row = -1 Then Exit Sub
            If dgItemSearch.Row >= 0 Then
                Array.Resize(SearchResult, dgItemSearch.Columns.Count)
                For i = 0 To dgItemSearch.Columns.Count - 1
                    SearchResult(i) = IIf(dgItemSearch.Item(dgItemSearch.Row, i) Is System.DBNull.Value, "", dgItemSearch.Item(dgItemSearch.Row, i))
                Next
                'code added for issue id 1672,1673 by vipul
                If SearchResult(0) <> "" Then
                    ItemRow = dtSearch.Select("EAN='" & dgItemSearch.Item(dgItemSearch.Row)("EAN") & "'")(0)
                    'ItemRow = dtSearch.Rows(dgItemSearch.Row)
                    ItemRows = dgItemSearch.SelectedRows
                End If

            End If
            Me.Close()
        Catch ex As Exception
            ShowMessage(getValueByKey("IMS002"), "IMS002 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Result is not properly intialized", "Information")
        End Try
    End Sub
    Private Sub cmdAdvancedSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdvancedSearch.Click
        Try
            Dim obj As New frmNAdvanceSearch
            obj.ShowDialog()
            If obj.Ean <> String.Empty Then
                Dim dv As New DataView(dtSearch, "ArticleCode='" & obj.Ean & "'", "", DataViewRowState.CurrentRows)
                If dv.Count > 0 Then
                    dgItemSearch.DataSource = Nothing
                    dgItemSearch.SetDataBinding(dv, "", True)
                    cmdOK_Click(cmdOK, New EventArgs)
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub dgItemSearch_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgItemSearch.DoubleClick
        Try
            Dim i As Integer
            If dgItemSearch.Row = -1 Then Exit Sub
            If dgItemSearch.Row >= 0 Then
                Array.Resize(SearchResult, dgItemSearch.Columns.Count)
                For i = 0 To dgItemSearch.Columns.Count - 1
                    SearchResult(i) = IIf(dgItemSearch.Item(dgItemSearch.Row, i) Is System.DBNull.Value, "", dgItemSearch.Item(dgItemSearch.Row, i))
                Next
                'code added for issue id 1672,1673 by vipul
                If SearchResult(0) <> "" Then
                    For Each row As DataRow In dtSearch.Select("Ean='" & dgItemSearch.Item(dgItemSearch.Row)("EAN") & "'", "EAN", DataViewRowState.CurrentRows)
                        ItemRow = row '0000204
                    Next
                    ItemRows = dgItemSearch.SelectedRows
                End If

            Else
                SearchResult = Nothing
            End If
            Me.Close()
        Catch ex As Exception
            ShowMessage(getValueByKey("IMS002"), "IMS002 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Result is not properly intialized", "Information")
        End Try

    End Sub
#End Region
#Region "Private Methods & Functions"
    ''' <summary>
    ''' Set the grid Layout
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GridSetting()
        Try
            If Not dgItemSearch.Columns(0) Is Nothing Then
                dgItemSearch.Columns("ArticleCode").Caption = getValueByKey("frmnitemsearch.dgitemsearch.item code")
                'dgItemSearch.Columns("EAN").Caption = "EAN"
                dgItemSearch.Columns("DISCRIPTION").Caption = getValueByKey("frmnitemsearch.dgitemsearch.description")
                dgItemSearch.Columns("SELLINGPRICE").Caption = getValueByKey("frmnitemsearch.dgitemsearch.price")
                dgItemSearch.Columns("SUPPLIERREF").Caption = getValueByKey("frmnitemsearch.dgitemsearch.supplier ref.")
                dgItemSearch.Columns("PhysicalQty").Caption = getValueByKey("frmnitemsearch.dgitemsearch.physical qty")
                dgItemSearch.Columns("OrderQty").Caption = getValueByKey("frmnitemsearch.dgitemsearch.order qty")
                dgItemSearch.Columns("NodeName").Caption = getValueByKey("frmnitemsearch.dgitemsearch.node name")
                dgItemSearch.Columns("Season").Caption = getValueByKey("frmnitemsearch.dgitemsearch.season")
                dgItemSearch.Columns("Theme").Caption = getValueByKey("frmnitemsearch.dgitemsearch.theme")

                'added by Khusrao Adil
                ' for Savoy
                If clsDefaultConfiguration.BarcodeDisplayAllowed Then
                    If Not clsDefaultConfiguration.IsSavoy Then
                        dgItemSearch.Splits(0).DisplayColumns("EAN").Visible = False
                    Else
                        dgItemSearch.Columns("EAN").Caption = "Model No."
                        dgItemSearch.Splits(0).DisplayColumns("EAN").Width = 100
                    End If
                Else
                    dgItemSearch.Splits(0).DisplayColumns("EAN").Visible = False
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Attach data to grid
    ''' </summary>
    ''' <param name="filter">Item Filter</param>
    ''' <remarks></remarks>
    Private Sub fillGrid(ByVal filter As String)
        Try
            Dim dv As New DataView(dtSearch, filter, "", DataViewRowState.CurrentRows)
            'Dim dtItemInformation As DataTable = dv.Table()
            'Dim _dvDisplayItem As DataView
            If dv.Count > 0 Then
                '_dvDisplayItem = New DataView(dtItemInformation, "", "", DataViewRowState.CurrentRows)
                'Dim dtSortList As DataTable = _dvDisplayItem.ToTable(True, "ArticleCode", "EAN", "Discription", "SellingPrice", "Season", "Theme", "SUPPLIERREF", "NODENAME", "PhysicalQty", "OrderQty")
                dgItemSearch.DataSource = Nothing
                dgItemSearch.SetDataBinding(dv, "", True)
                'grdScanItem.Cols("ArticleCode").Visible = False
                'grdScanItem.Cols("ExpDelDate").StyleDisplay.Format = DateFormat.ShortDate
                'dgItemSearch.Columns(1).
            Else
                dgItemSearch.DataSource = Nothing
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function ThemeChange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        dgItemSearch.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Custom
        dgItemSearch.Splits(0).Style.BackColor = Color.FromArgb(255, 255, 255)
        dgItemSearch.Splits(0).Style.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdShow.VisualStyle = C1.Win.C1Input.VisualStyle.System
        cmdAdvancedSearch.VisualStyle = C1.Win.C1Input.VisualStyle.System
        cmdOK.VisualStyle = C1.Win.C1Input.VisualStyle.System
        cmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.System
        cmdShow.BackColor = Color.Transparent
        cmdShow.BackColor = Color.FromArgb(0, 107, 163)
        cmdShow.ForeColor = Color.FromArgb(255, 255, 255)
        cmdShow.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmdShow.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdShow.FlatStyle = FlatStyle.Flat
        cmdShow.TextAlign = ContentAlignment.MiddleCenter
        cmdShow.FlatAppearance.BorderSize = 0
        cmdShow.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdShow.Size = New Size(98, 28)
        cmdAdvancedSearch.BackColor = Color.Transparent
        cmdAdvancedSearch.BackColor = Color.FromArgb(0, 107, 163)
        cmdAdvancedSearch.ForeColor = Color.FromArgb(255, 255, 255)
        cmdAdvancedSearch.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmdAdvancedSearch.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdAdvancedSearch.FlatStyle = FlatStyle.Flat
        cmdAdvancedSearch.FlatAppearance.BorderSize = 0
        cmdAdvancedSearch.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdAdvancedSearch.Size = New Size(138, 28)
        cmdAdvancedSearch.TextAlign = ContentAlignment.MiddleCenter
        cmdOK.BackColor = Color.Transparent
        cmdOK.BackColor = Color.FromArgb(0, 107, 163)
        cmdOK.ForeColor = Color.FromArgb(255, 255, 255)
        cmdOK.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmdOK.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdOK.FlatStyle = FlatStyle.Flat
        cmdOK.FlatAppearance.BorderSize = 0
        cmdOK.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdOK.Size = New Size(61, 28)
        cmdOK.TextAlign = ContentAlignment.MiddleCenter
        cmdOK.Location = New Point(330, 7)
        'cmdCancel.BackColor = Color.Transparent
        'cmdCancel.BackColor = Color.FromArgb(0, 107, 163)
        ' cmdCancel.ForeColor = Color.FromArgb(255, 255, 255)
        ' cmdCancel.ForeColor = Color.FromArgb(255, 255, 255)
        'cmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        'cmdCancel.FlatStyle = FlatStyle.Popup
        'cmdCancel.FlatAppearance.BorderSize =2
        'cmdCancel.FlatAppearance.BorderColor =  Color.FromArgb(0, 81, 120)
        dgItemSearch.Font = New Font("Neo Sans", 8, FontStyle.Regular)
        dgItemSearch.Splits(0).HeadingStyle.BackColor = Color.FromArgb(177, 227, 253)
        dgItemSearch.Splits(0).HighLightRowStyle.BackColor = Color.LightBlue
        dgItemSearch.Splits(0).HighLightRowStyle.BackColor2 = Color.LightBlue
        dgItemSearch.RowHeight = 24
        dgItemSearch.Styles(0).BackColor = Color.FromArgb(255, 255, 255)
        dgItemSearch.Styles(0).Font = New Font("Neo Sans", 9, FontStyle.Bold)
        dgItemSearch.Styles(1).BackColor = Color.FromArgb(177, 227, 253)
        dgItemSearch.Styles(5).BackColor = Color.FromArgb(242, 242, 242)
        dgItemSearch.Styles(7).BackColor = Color.FromArgb(242, 242, 242)
        trvArticle.Font = New Font("Neo Sans", 12, FontStyle.Regular)

    End Function
#End Region

    Private Sub dgItemSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgItemSearch.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                cmdOK_Click(cmdOK, New EventArgs)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShow.Click
        Try
            StrFilter = ""
            FindLastNodes(trvArticle.SelectedNode)
            'Dim sftr As String = "isnull(Level" & trvArticle.SelectedNode.Tag & ",'') in ('" & trvArticle.SelectedNode.Name & "')"
            Dim sftr As String = "Nodes in ('" & StrFilter & "')"
            fillGrid(sftr)
            GridSetting()
            dgItemSearch.RecordSelectors = True
        Catch ex As Exception

        End Try
    End Sub
    Private Function FindLastNodes(ByVal nod As TreeNode) As String
        Try
            Dim strLastNodes As String
            If nod.Nodes.Count > 0 Then
                For Each node As TreeNode In nod.Nodes
                    If Not node.LastNode Is Nothing Then
                        FindLastNodes(node)
                    Else
                        StrFilter = StrFilter & node.Name.ToString() & "','"
                    End If
                Next
            Else
                StrFilter = StrFilter & nod.Name.ToString() & "','"
            End If

            Return strLastNodes
        Catch ex As Exception

        End Try
    End Function

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
