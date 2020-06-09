Imports Spectrum
Imports SpectrumBL
Imports System.Data
Imports System.Data.SqlClient
Imports C1.Win.C1FlexGrid
Imports System.Text
Imports System.IO
Imports Microsoft.Reporting.WinForms

Public Class frmNHierarchyWiseNetSales

    Dim objArticle As New clsArticleCombo
    Dim dtCustmData As New DataTable
    Dim dtArticle As New DataTable
    Dim objItem As New clsIteamSearch
    Dim dtGuest As New DataTable
    Dim StrFilter As String
    Dim dtTree As DataTable
    Dim dt As DataTable
    Dim opendate As Date
    Dim _IsNetSale As Boolean = False
    Public Property IsNetSale() As Boolean
        Get
            Return _IsNetSale
        End Get
        Set(ByVal value As Boolean)
            _IsNetSale = value
        End Set
    End Property

    Private Sub gridArticleDetailsSetting()
        Try
            grdArticleSearch.DataSource = dtGuest ' dtArticle
            'grdArticleSearch.Cols("Del").Caption = ""
            'grdArticleSearch.Cols("Del").Width = 20
            'grdArticleSearch.Cols("Del").ComboList = "..."
            'grdArticleSearch.Cols("Del").Visible = True
            grdArticleSearch.Cols("SrNo").Visible = True
            grdArticleSearch.Cols("ArticleCode").Width = 150
            grdArticleSearch.Cols("ArticleCode").DataType = Type.GetType("System.String")
            grdArticleSearch.Cols("ArticleCode").AllowEditing = False
            grdArticleSearch.Cols("ArticleCode").Name = "ArticleCode"
            grdArticleSearch.Cols("ArticleCode").Caption = "Article Code"
            grdArticleSearch.Cols("ArticleCode").TextAlign = TextAlignEnum.LeftCenter

            grdArticleSearch.Cols("ArticleName").Caption = "Article Name"
            grdArticleSearch.Cols("ArticleName").Width = 300
            grdArticleSearch.Cols("ArticleName").AllowEditing = False
            grdArticleSearch.Cols("ArticleName").DataType = Type.GetType("System.String")
            grdArticleSearch.Cols("ArticleName").Name = "ArticleName"
            grdArticleSearch.Cols("ArticleName").TextAlign = TextAlignEnum.LeftCenter

            grdArticleSearch.Cols("LastNodeCode").Width = 150
            grdArticleSearch.Cols("LastNodeCode").Caption = "Last Node"
            grdArticleSearch.Cols("LastNodeCode").AllowEditing = False
            grdArticleSearch.Cols("LastNodeCode").DataType = Type.GetType("System.String")


        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub



    Private Sub getReportName()
        cmbReportType.Items.Clear()
        cmbReportType.Items.Insert(0, "Select")
        cmbReportType.Items.Add("Consolidated")
        cmbReportType.Items.Add("Day wise")
        If cmbReportType.Items.Count > 0 Then
            cmbReportType.SelectedIndex = 0    ' The first item has index 0 '
        End If
    End Sub

    Private Sub frmNHierarchyWiseNetSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IsNetSale = True Then
            lblReport.Visible = False
            cmbReportType.Visible = False
            grdArticleSearch.Visible = True
            Me.Text = "Hierarchy wise Net-Sales Detailed Report"
        Else
            getReportName()
            cmbReportType.Visible = True
            grdArticleSearch.Visible = True
            Me.Text = "Hierarchy wise Net-Sales Summary Report"
        End If

        dtGuest.Clear()
        dtGuest = objArticle.GetDetailsForGuest()
        RadioArticle.Checked = True
        If RadioArticle.Checked = True Then
            txtFilterArticle.Visible = True
        End If
        trvArticle.Visible = False
        '  cmdPdf.Enabled = False
        '  cmdExcel.Enabled = False
        Dim condition As String
        Dim objItem As New clsIteamSearch
        Dim objReportBase As New ReportBase
        opendate = objReportBase.GetOpenDateAndStatus(clsAdmin.SiteCode, clsDefaultConfiguration.ClientForMail).ToString("yyyy-MM-dd")
        If opendate <> Nothing Then
            '  Me.dtToDate1.Calendar.MaxDate = opendate
            cmdFromDate.Value = opendate.ToString("yyyy-MM-dd")

            CmdToDate.Value = opendate.ToString("yyyy-MM-dd")
        Else
            CmdToDate.Value = Nothing
            cmdFromDate.Value = Nothing
            MsgBox("there is no day Close Data", getValueByKey("CLAE04"))
            Me.Close()
        End If
        condition = " AND A.ArticleCode <>'" & clsDefaultConfiguration.GVBaseArticle & "' AND A.ArticleCode <>'" & clsDefaultConfiguration.ClpBaseArticle & "' AND A.ArticleCode <>'" & clsAdmin.CVBaseArticle & "'"
        Dim dtBind = objItem.GetEANData(clsAdmin.SiteCode, "", clsAdmin.LangCode, condition, dtItemScanData)
        If dtBind.Rows.Count > 1 Then
            Call SetWildSearchTextBox(dtBind, txtFilterArticle, key:="ArticleCode", Value:="Discription", searchData:="ArticleCodeDesc")
            ' txtFilterArticle.IsMovingControl = True
        End If
        dtGuest.Clear()
        gridArticleDetailsSetting()

        'AddHandler txtFilterArticle.KeyDown, AddressOf txtSearch_KeyDown
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            Cursor.Current = Cursors.WaitCursor
            'Dim openMrp As Boolean = False
            'WeghingScaleBarcode = False
            'txtFilterArticle.Text = txtFilterArticle.Text.ToString().Split(" ")(0)
            'Dim membershipmaparticle = txtFilterArticle.Text
            'Dim objItemSch As New clsIteamSearch
            'If e.KeyCode = Keys.Enter AndAlso txtFilterArticle.Text <> String.Empty Then
            '    Call bindSelectedArticle(txtFilterArticle.Text)
            'End If
            'txtFilterArticle.Focus()
        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub bindSelectedArticle(ByVal article As String) ''checkout As DateTime,
        Try
            'grdArticleSearch.Clear()
            ' dtCustmData.Clear()

            dtCustmData = objArticle.GetArticleDetails(article)
            If dtCustmData IsNot Nothing And dtCustmData.Rows.Count > 0 Then
                If dtCustmData IsNot Nothing And dtCustmData.Rows.Count > 0 Then
                    If dtCustmData.Rows.Count > 0 Then
                        For index = 0 To dtGuest.Rows.Count - 1
                            Dim dtRow As Int32 = -1
                            Dim result As DataRow() = dtGuest.Select("ArticleCode='" + article.Trim + "' ")
                            If result.Length > 0 Then
                                ShowMessage("Record Already exist", "Information")
                                txtFilterArticle.Clear()
                                Exit Sub
                            End If


                        Next

                    End If
                    Dim rowGuest As DataRow

                    rowGuest = dtGuest.NewRow()
                    rowGuest("Srno") = dtGuest.Rows.Count + 1
                    rowGuest("ArticleCode") = dtCustmData.Rows(0)("ArticleCode")
                    rowGuest("ArticleName") = dtCustmData.Rows(0)("ArticleName")
                    rowGuest("LastNodeCode") = dtCustmData.Rows(0)("NodeName")
                    dtGuest.Rows.Add(rowGuest)


                Else
                    grdArticleSearch.DataSource = dtCustmData
                    dtArticle = dtCustmData
                    grdArticleSearch.Refresh()
                End If
                gridArticleDetailsSetting()
                txtFilterArticle.Clear()


            ElseIf Not dtCustmData Is Nothing And dtCustmData.Rows.Count = 0 Then
                ShowMessage(getValueByKey("CM016"), "CM016 - " & getValueByKey("CLAE04"))
                txtFilterArticle.Text = String.Empty
                txtFilterArticle.Focus()
                Exit Sub
            Else
                grdArticleSearch.DataSource = dtCustmData
                dtArticle = dtCustmData
                grdArticleSearch.Refresh()
            End If
            If dtGuest.Rows.Count > 0 Then
                cmdPdf.Enabled = True
                cmdExcel.Enabled = True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub RadioHierarchy_Click(sender As Object, e As EventArgs) Handles RadioHierarchy.Click
        Try

            If txtFilterArticle.IsListBoxVisible Then
                txtFilterArticle.Text = ""
                txtFilterArticle.ResetListBox()
            End If
            grdArticleSearch.Visible = False

            lblSearch.Visible = False
            txtFilterArticle.Visible = False
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


    Private Sub cmdPdf_Click(sender As Object, e As EventArgs) Handles cmdPdf.Click
        Try
            If IsNetSale = True Then

            Else
                If cmbReportType.Text.Trim <> "Select" Then
                Else
                    ShowMessage("Please select Report", "Information")
                    Exit Sub
                End If
            End If
            If (cmdFromDate.Value Is DBNull.Value) Or (cmdFromDate.Text) = "" Then
                ShowMessage("Please Select Date", "Information")


            End If
            If cmdFromDate.Value <= Now.Date Then

            Else
                ShowMessage("Date should be less than current date", "Information")
                cmdFromDate.Focus()
                Exit Sub
            End If
            If (CmdToDate.Value Is DBNull.Value) Or (CmdToDate.Text) = "" Then
                ShowMessage("Please select Date", "Information")
                CmdToDate.Focus()
                Exit Sub

            End If
            If CmdToDate.Value < cmdFromDate.Value Then
                ShowMessage("Date should be greater than from date ", "Information")
                CmdToDate.Focus()
                Exit Sub

            End If
            If RadioHierarchy.Checked = True Then

                Dim articleCode As String = ""
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
                                articleCode = dtnodetree("nodecode").ToString.Trim
                            Else
                                articleCode = articleCode & "," & dtnodetree("nodecode").ToString.Trim.ToString.Trim
                            End If
                        Next
                        ' dtTreeView.Rows.Add(treeNode.Text)
                    End If
                Next
                If articleCode.ToString.Trim = "" Then
                    ShowMessage("Please select atleast one Hierarchy.", "Information")
                    CmdToDate.Focus()
                    Exit Sub
                End If
                'Dim dtArticleSearch As New DataTable
                'dtArticleSearch = objArticle.FillArticles(articleCode, clsAdmin.SiteCode)
                HierarchyWiseNetSalesReportInPDF(clsAdmin.SiteCode, cmdFromDate.Value, CmdToDate.Value, articleCode)
            Else
                Dim ArticlName As String = ""
                Dim dt As New DataTable
                Dim arr As New List(Of String)()
                Dim article As String = ""
                For Each dr As DataRow In dtGuest.Rows
                    arr.Add(dr(1).ToString)
                Next

                For Each article In arr
                    If ArticlName.Trim() = "" Or ArticlName.Trim Is Nothing Then
                        ArticlName = arr(0)
                    Else
                        ArticlName = ArticlName & "," & article
                    End If
                Next
                'dt = objArticle.FillArticles(ArticlName, clsAdmin.SiteCode)
                HierarchyWiseNetSalesReportInPDF(clsAdmin.SiteCode, cmdFromDate.Value, CmdToDate.Value, ArticlName)
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function HierarchyWiseNetSalesReportInPDF(ByVal SiteCode As String, ByVal frmDate As String, ByVal toDate As String, ByVal ArticleCode As String)
        Try
            Dim fDate = cmdFromDate.Value
            fDate = Convert.ToDateTime(fDate).ToString("dd-MMM-yyyy")
            Dim tDate = CmdToDate.Value
            tDate = Convert.ToDateTime(tDate).ToString("dd-MMM-yyyy")
            Dim currentshiftid As String = ""
            Dim dt As DataTable = clsCommon.GetPreviousShiftDetails(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
            If dt.Rows.Count > 0 Then
                currentshiftid = dt.Rows(0)("ShiftId").ToString()
                '' createdon = dt.Rows(0)("CREATEDON")
            End If
            Dim path As String = ""
            Dim ReportType As String = ""
            Dim dsHierarchy As DataSet
            If IsNetSale = True Then
                ReportType = "Details Report"
            Else
                If cmbReportType.SelectedValue <> "Select" Then
                    ReportType = cmbReportType.SelectedItem
                Else

                End If
            End If
            Dim Leval As String = ""
            Dim IsReport As Boolean
            If RadioArticle.Checked = True Then
                Leval = "Article Wise"
                IsReport = False
            Else
                Leval = "Hierarchy Wise"
                IsReport = True
            End If

            Dim clsObj As New ReportBase
            Dim Time = DateTime.Now.ToString("dd-MMM-yyyy   HH:mm")
            If IsNetSale = True Then
                dsHierarchy = clsObj.getHierarchyWiseDetailsReportData(clsAdmin.SiteCode, cmdFromDate.Value, CmdToDate.Value, ArticleCode, ReportType, IsReport)
            Else
                dsHierarchy = clsObj.getHierarchyWiseReportDate(clsAdmin.SiteCode, cmdFromDate.Value, CmdToDate.Value, ArticleCode, ReportType, IsReport)
            End If
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String
            If IsNetSale = True Then
                appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\HierarchywiseNetSalesDetailsReport.rdl")
            Else

                If RadioArticle.Checked = True Then
                    If Leval = "Article Wise" And ReportType = "Consolidated" Then
                        appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\HierarchywiseNetSalesReport.rdl")
                    Else
                        appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\HierarchywiseReport.rdl")
                    End If


                End If

                If RadioHierarchy.Checked = True Then
                    If Leval = "Hierarchy Wise" And ReportType = "Consolidated" Then
                        appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\HierarchywiseConsolidatedReport.rdl")
                    Else
                        appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\HierarchywiseReport.rdl")
                    End If
                End If
            End If
            reportViewer2.LocalReport.ReportPath = appPath
            Dim f1Date = Convert.ToDateTime(fDate).ToString("dd-MMM-yyyy")
            Dim T1Date = Convert.ToDateTime(tDate).ToString("dd-MMM-yyyy")
            Dim SiteCodeParam As New ReportParameter("V_SiteCode", SiteCode)
            Dim FromDateParam As New ReportParameter("V_FromDate", f1Date)
            Dim ToDateParam As New ReportParameter("V_Todate", T1Date)
            Dim Article As New ReportParameter("V_ARTICLECODE", ArticleCode)
            Dim Report As New ReportParameter("V_ReportType", ReportType)
            Dim HierarchyLevel As New ReportParameter("Level", Leval)
            Dim paratime As New ReportParameter("GTime", Time)
            Dim ReportID As New ReportParameter("V_ReportID", IsReport)
            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, FromDateParam, ToDateParam, Article, Report, HierarchyLevel, paratime, ReportID})  '', 
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()

            Dim datasource1 As New ReportDataSource("NetSalesData", dsHierarchy.Tables(0))
            Dim datasource2 As New ReportDataSource("dsFooter", dsHierarchy.Tables(1))
            Dim DataSource3 As New ReportDataSource("dsHeader", dsHierarchy.Tables(2))
            reportViewer2.LocalReport.DataSources.Add(datasource1)
            reportViewer2.LocalReport.DataSources.Add(datasource2)
            reportViewer2.LocalReport.DataSources.Add(DataSource3)
            Dim mybytes As [Byte]()
            mybytes = reportViewer2.LocalReport.Render("Pdf")
            If IsNetSale = True Then
                path = clsDefaultConfiguration.DayCloseReportPath & "\" & fDate & "_" & tDate & "_HierarchywiseNetSalesDetailsReport" & ".pdf"
            Else
                path = clsDefaultConfiguration.DayCloseReportPath & "\" & fDate & "_" & tDate & "_HierarchywiseNetSalesReport" & ".pdf"
            End If
            If System.IO.File.Exists(path) Then
                File.Delete(path)
            Else
            End If
            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

    Private Function HierarchyWiseNetSalesReportInEXCEL(ByVal SiteCode As String, ByVal frmDate As String, ByVal toDate As String, ByVal ArticleCode As String)
        Try
            Dim fDate = cmdFromDate.Value
            fDate = Convert.ToDateTime(fDate).ToString("dd-MMM-yyyy")
            Dim tDate = CmdToDate.Value
            tDate = Convert.ToDateTime(tDate).ToString("dd-MMM-yyyy")
            Dim currentshiftid As String = ""
            Dim dt As DataTable = clsCommon.GetPreviousShiftDetails(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsAdmin.TerminalID)
            If dt.Rows.Count > 0 Then
                currentshiftid = dt.Rows(0)("ShiftId").ToString()
                '' createdon = dt.Rows(0)("CREATEDON")
            End If
            Dim path As String = ""
            Dim ReportType As String = ""
            Dim dsHierarchy As DataSet
            If IsNetSale = True Then
                ReportType = "Details Report"
            Else
                If cmbReportType.SelectedValue <> "Select" Then
                    ReportType = cmbReportType.SelectedItem
                Else

                End If
            End If
            Dim Leval As String = ""
            Dim IsReport As Boolean
            If RadioArticle.Checked = True Then
                Leval = "Article Wise"
                IsReport = False
            Else
                Leval = "Hierarchy Wise"
                IsReport = True
            End If

            Dim clsObj As New ReportBase
            Dim Time = DateTime.Now.ToString("dd-MMM-yyyy   HH:mm")
            If IsNetSale = True Then
                dsHierarchy = clsObj.getHierarchyWiseDetailsReportData(clsAdmin.SiteCode, cmdFromDate.Value, CmdToDate.Value, ArticleCode, ReportType, IsReport)
            Else
                dsHierarchy = clsObj.getHierarchyWiseReportDate(clsAdmin.SiteCode, cmdFromDate.Value, CmdToDate.Value, ArticleCode, ReportType, IsReport)
            End If
            Dim reportViewer2 As New ReportViewer()
            Dim appPath As String
            If IsNetSale = True Then
                appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\HierarchywiseNetSalesDetailsReport.rdl")
            Else

                If RadioArticle.Checked = True Then
                    If Leval = "Article Wise" And ReportType = "Consolidated" Then
                        appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\HierarchywiseNetSalesReport.rdl")
                    Else
                        appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\HierarchywiseReport.rdl")
                    End If


                End If

                If RadioHierarchy.Checked = True Then
                    If Leval = "Hierarchy Wise" And ReportType = "Consolidated" Then
                        appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\HierarchywiseConsolidatedReport.rdl")
                    Else
                        appPath = System.IO.Path.Combine(Application.StartupPath, "Reports\HierarchywiseReport.rdl")
                    End If
                End If
            End If
            reportViewer2.LocalReport.ReportPath = appPath
            Dim f1Date = Convert.ToDateTime(fDate).ToString("dd-MMM-yyyy")
            Dim T1Date = Convert.ToDateTime(tDate).ToString("dd-MMM-yyyy")
            Dim SiteCodeParam As New ReportParameter("V_SiteCode", SiteCode)
            Dim FromDateParam As New ReportParameter("V_FromDate", f1Date)
            Dim ToDateParam As New ReportParameter("V_Todate", T1Date)
            Dim Article As New ReportParameter("V_ARTICLECODE", ArticleCode)
            Dim Report As New ReportParameter("V_ReportType", ReportType)
            Dim HierarchyLevel As New ReportParameter("Level", Leval)
            Dim paratime As New ReportParameter("GTime", Time)
            Dim ReportID As New ReportParameter("V_ReportID", IsReport)
            reportViewer2.LocalReport.SetParameters(New ReportParameter() {SiteCodeParam, FromDateParam, ToDateParam, Article, Report, HierarchyLevel, paratime, ReportID})
            reportViewer2.ProcessingMode = ProcessingMode.Local
            reportViewer2.LocalReport.DataSources.Clear()
            Dim datasource1 As New ReportDataSource("NetSalesData", dsHierarchy.Tables(0))
            Dim datasource2 As New ReportDataSource("dsFooter", dsHierarchy.Tables(1))
            Dim DataSource3 As New ReportDataSource("dsHeader", dsHierarchy.Tables(2))
            reportViewer2.LocalReport.DataSources.Add(datasource1)
            reportViewer2.LocalReport.DataSources.Add(datasource2)
            reportViewer2.LocalReport.DataSources.Add(DataSource3)
            Dim mybytes As [Byte]()
            mybytes = reportViewer2.LocalReport.Render("Excel")
            If IsNetSale = True Then
                path = clsDefaultConfiguration.DayCloseReportPath & "\" & fDate & "_" & tDate & "_HierarchywiseNetSalesDetailsReport" & ".xls"
            Else
                path = clsDefaultConfiguration.DayCloseReportPath & "\" & fDate & "_" & tDate & "_HierarchywiseNetSalesReport" & ".xls"
            End If
            If System.IO.File.Exists(path) Then
                File.Delete(path)
            Else

            End If

            Using fs As FileStream = File.Create(path)
                fs.Write(mybytes, 0, mybytes.Length)
            End Using

            Process.Start(path)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

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

    Private Function GetCheck(ByVal node As TreeNodeCollection) As ArrayList
        Dim lN As New ArrayList
        For Each n As TreeNode In node
            If n.Checked Then lN.Add(n)
            lN.AddRange(GetCheck(n.Nodes))
        Next

        Return lN
    End Function


    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        Try
            If IsNetSale = True Then

            Else
                If cmbReportType.Text.Trim <> "Select" Then
                Else
                    ShowMessage("Please select Report", "Information")
                    Exit Sub
                End If
            End If
            If (cmdFromDate.Value Is DBNull.Value) Or (cmdFromDate.Text) = "" Then
                ShowMessage("Please Select Date", "Information")


            End If
            If cmdFromDate.Value <= Now.Date Then

            Else
                ShowMessage("Date should be less than current date", "Information")
                cmdFromDate.Focus()
                Exit Sub
            End If
            If (CmdToDate.Value Is DBNull.Value) Or (CmdToDate.Text) = "" Then
                ShowMessage("Please select Date", "Information")
                CmdToDate.Focus()
                Exit Sub

            End If
            If CmdToDate.Value < cmdFromDate.Value Then
                ShowMessage("Date should be greater than from date ", "Information")
                CmdToDate.Focus()
                Exit Sub

            End If
            If RadioHierarchy.Checked = True Then

                Dim articleCode As String = ""
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
                                articleCode = dtnodetree("nodecode").ToString.Trim
                            Else
                                articleCode = articleCode & "," & dtnodetree("nodecode").ToString.Trim.ToString.Trim
                            End If
                        Next

                        ' dtTreeView.Rows.Add(treeNode.Text)
                    End If
                Next

                If articleCode.ToString.Trim = "" Then
                    ShowMessage("Please select atleast one Hierarchy.", "Information")
                    CmdToDate.Focus()
                    Exit Sub
                End If
                'Dim dtArticleSearch As New DataTable
                'dtArticleSearch = objArticle.FillArticles(articleCode, clsAdmin.SiteCode)
                HierarchyWiseNetSalesReportInEXCEL(clsAdmin.SiteCode, cmdFromDate.Text, CmdToDate.Text, articleCode)
            Else
                Dim ArticlName As String = ""
                Dim dt As New DataTable
                Dim arr As New List(Of String)()
                Dim article As String = ""
                For Each dr As DataRow In dtGuest.Rows
                    arr.Add(dr(1).ToString)
                Next

                For Each article In arr
                    If ArticlName.Trim() = "" Or ArticlName.Trim Is Nothing Then
                        ArticlName = arr(0)
                    Else
                        ArticlName = ArticlName & "," & article
                    End If
                Next
                ' dt = objArticle.FillArticles(ArticlName, clsAdmin.SiteCode)
                HierarchyWiseNetSalesReportInEXCEL(clsAdmin.SiteCode, cmdFromDate.Text, CmdToDate.Text, ArticlName)
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    'Private Sub trvArticle_BeforeCheck(sender As Object, e As TreeViewCancelEventArgs) Handles trvArticle.BeforeCheck
    '    If Not e.Node.Checked Then
    '        '  Me.addchildNodes(e.Node)
    '    End If
    'End Sub

    Private Sub RadioArticle_Click(sender As Object, e As EventArgs) Handles RadioArticle.Click
        Try
            grdArticleSearch.Visible = True
            dtGuest.Clear()
            ' cmdFromDate.Value = ""
            ' CmdToDate.Value = ""
            lblSearch.Visible = True
            txtFilterArticle.Visible = True
            trvArticle.Visible = False

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

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

    Private Sub txtFilterArticle_TextChanged(sender As Object, e As EventArgs) Handles txtFilterArticle.TextChanged, txtFilterArticle.TextChanged
        If Not String.IsNullOrEmpty(txtFilterArticle.Text) AndAlso txtFilterArticle.IsItemSelected Then
            txtFilterArticle.IsItemSelected = False
            'SendKeys.Send("{Enter}")
            Dim eKeyDown = New System.Windows.Forms.KeyEventArgs(Keys.Enter)
            Call txtFilterArticle_Leave(sender, eKeyDown)
        End If
    End Sub


    Private Sub grdArticleSearch_CellButtonClick(sender As Object, e As RowColEventArgs)
        Try
            Dim SrNo = grdArticleSearch.Item(grdArticleSearch.Row, "Del")
            DeleteGuestDetails(SrNo)
        Catch ex As Exception
            LogException(ex)
        End Try
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
            gridArticleDetailsSetting()

        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

    Private Sub grdArticleSearch_CellButtonClick_1(sender As Object, e As RowColEventArgs) Handles grdArticleSearch.CellButtonClick
        Try
            Dim SrNo = grdArticleSearch.Item(grdArticleSearch.Row, "SrNo")
            DeleteGuestDetails(SrNo)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub txtFilterArticle_Leave(sender As System.Object, e As System.EventArgs) 'Handles txtFilterArticle.Leave
        Try
            Cursor.Current = Cursors.WaitCursor
            txtFilterArticle.Text = txtFilterArticle.Text.ToString().Split(" ")(0)
            If txtFilterArticle.Text.Length >= 1 Then
                Dim membershipmaparticle = txtFilterArticle.Text
                Dim objItemSch As New clsIteamSearch
                ' If e.KeyCode = Keys.Enter AndAlso txtFilterArticle.Text <> String.Empty Then
                Call bindSelectedArticle(txtFilterArticle.Text)
                'End If
                'End If
                txtFilterArticle.Focus()
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    Private Sub RadioHierarchy_CheckedChanged(sender As Object, e As EventArgs) Handles RadioHierarchy.CheckedChanged

    End Sub
End Class