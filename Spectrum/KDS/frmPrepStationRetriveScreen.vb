Imports SpectrumBL

Public Class frmPrepStationRetriveScreen


    Dim obj As New clsKdsData
    Public FirstBillTimer As New Timer()
    Private FirstBillTimeInterval As Integer = 15
    Dim dtOrderHdr As New DataTable
    Dim dtOrderdtl As New DataTable

    Private Sub FirstBillTimer_Tick(ByVal sender As Object, e As EventArgs)
        'FirstBillTimer.Stop()
        'FirstBillTimer.Start()
    End Sub

    Private Sub DemoFormForKDS_Load(sender As Object, e As EventArgs) Handles Me.Load

        dtOrderHdr = obj.GetArticleHeaderDetails()

        If dtOrderHdr.Rows.Count > 0 Then
            For Each row In dtOrderHdr.Rows
                dtOrderdtl = obj.GetArticleDetails(row("BillNo"))
                AddPanel(row("Result"), row("ElaspedTime"), dtOrderdtl, , True, row("Time"))
                If Not dtOrderdtl Is Nothing AndAlso dtOrderdtl.Rows.Count > 0 Then
                    AddPanel(row("Result"), row("Time"), dtOrderdtl, , False, )
                End If
            Next
        End If

        'FirstBillTimer.Start()
        'AddHandler FirstBillTimer.Tick, AddressOf FirstBillTimer_Tick
        'FirstBillTimer.Interval = Convert.ToInt32(2 * 60 * FirstBillTimeInterval)

    End Sub

    Dim lb As Label
    Dim lb1 As Label
    Dim lb2 As Label
    Dim lb3 As Label
    Dim lb4 As Label
    Dim pn As TableLayoutPanel
    Dim bt As Button

    Private Sub AddPanel(ByVal OrderHdrDetail As String, ByVal OrderTime As String, ByVal dtOrderDtl As DataTable, Optional ByVal Action As String = "", Optional ByVal IsHeader As Boolean = False, Optional ByVal Time As Double = 0.0)
        Try

            lb = New Label()
            lb.MaximumSize = New Size(0, 0)
            lb.AutoSize = True
            lb.Margin = New Padding(3, 2, 0, 0)
            lb.Name = "OrderHderText"
            lb.Text = OrderHdrDetail + OrderTime
            lb.TextAlign = ContentAlignment.TopLeft
            lb.Dock = DockStyle.Fill

            ' lb.BackColor = Color.SkyBlue

            If Not dtOrderDtl Is Nothing AndAlso dtOrderDtl.Rows.Count > 0 Then
                For Each articles In dtOrderDtl.Rows


                    lb1 = New Label()
                    lb1.MaximumSize = New Size(0, 0)
                    lb1.AutoSize = True
                    lb1.Margin = New Padding(3, 2, 0, 0)
                    lb1.Name = "ArticleName"
                    If IsHeader Then
                        lb1.Text = "Item"
                    Else
                        lb1.Text = articles("Item")
                    End If


                    lb1.TextAlign = ContentAlignment.TopLeft
                    lb1.Dock = DockStyle.None

                    lb2 = New Label()
                    lb2.MaximumSize = New Size(0, 0)
                    lb2.AutoSize = True
                    lb2.Margin = New Padding(3, 2, 0, 0)
                    lb2.Name = "Qty"
                    If IsHeader Then
                        lb2.Text = "Qty"
                    Else
                        lb2.Text = articles("Quantity")
                    End If

                    lb2.TextAlign = ContentAlignment.TopLeft
                    lb2.Dock = DockStyle.None

                    lb3 = New Label()
                    lb3.MaximumSize = New Size(0, 0)
                    lb3.AutoSize = True
                    lb3.Margin = New Padding(3, 2, 0, 0)
                    lb3.Name = "Time"
                    If IsHeader Then
                        lb3.Text = "Time"
                    Else
                        lb3.Text = articles("Time")
                    End If

                    lb3.TextAlign = ContentAlignment.TopLeft
                    lb3.Dock = DockStyle.None


                    bt = New Button
                    bt.Anchor = AnchorStyles.Top
                    bt.AutoSize = True
                    bt.MaximumSize = New Size(100, 22)
                    If IsHeader Then
                        bt.Text = "Retrive"
                    Else
                        bt.Text = "Retrive"
                    End If

                    bt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                    bt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
                    bt.UseVisualStyleBackColor = True
                    bt.Name = "Retrive"
                    bt.Tag = "-"
                    bt.UseVisualStyleBackColor = C1.Win.C1Input.VisualStyle.Office2010Blue
                    bt.Dock = DockStyle.Left
                    Time = articles("CheckTime")


                Next
            End If


            'If Not IsHeader = True Then

            'End If

            pn = New TableLayoutPanel()
            pn.SuspendLayout()
            pn.MaximumSize = New Size(0, 0)
            pn.Margin = New Padding(0)
            pn.Padding = New Padding(0)
            pn.Size = New System.Drawing.Size(650, 50)
            '  pn.AutoScroll = True
            pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
            pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
            pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
            pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
            'pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))

            pn.RowCount = 1
            pn.ColumnCount = 4
            'pn.Controls.Add(lb, 0, 0)

            If IsHeader = True Then

                lb4 = New Label()
                lb4.MaximumSize = New Size(0, 0)
                lb4.AutoSize = True
                lb4.Margin = New Padding(3, 2, 0, 0)
                lb4.Name = "Action"
                lb4.Text = "Action"
                lb4.TextAlign = ContentAlignment.TopLeft
                lb4.Dock = DockStyle.Fill
                '  lb4.BackColor = Color.Aqua

                pn.SetColumnSpan(lb, 4)
                pn.Controls.Add(lb, 1, 0)
                pn.Controls.Add(lb1, 1, 1)
                pn.Controls.Add(lb2, 2, 1)
                pn.Controls.Add(lb3, 3, 1)
                pn.Controls.Add(lb4, 4, 1)
            Else
                pn.Controls.Add(lb1, 0, 0)
                pn.Controls.Add(lb2, 1, 0)
                pn.Controls.Add(lb3, 2, 0)
                pn.Controls.Add(bt, 3, 0)
                AddHandler bt.Click, AddressOf Me.Retrive_Click
            End If
            If Time <> 0 Then
                If Time > 15.0 Then
                    pn.BackColor = Color.Red
                End If
            End If

            pn.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            FlwPanel.Controls.Add(pn)

            pn.ResumeLayout()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub complete_Click(sender As Object, e As EventArgs)
        MessageBox.Show("Article completed")
        Dim button As Button = TryCast(sender, Button)
        If button IsNot Nothing Then
            MessageBox.Show("Remove ")
            Dim selectedRows() As DataRow = dtOrderdtl.Select("Select=True", "", DataViewRowState.CurrentRows)
            For Each dr As DataRow In selectedRows
                Dim ArticleCode As String = dr("ArticleCode")
                ' obj.CompleteArticle(ArticleCode)
                dr.Delete()
            Next
        End If
    End Sub

    Private Sub Retrive_Click(sender As Object, e As EventArgs)
        Throw New NotImplementedException
    End Sub


End Class