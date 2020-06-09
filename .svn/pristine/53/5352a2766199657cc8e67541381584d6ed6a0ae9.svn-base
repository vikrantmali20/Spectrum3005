Imports SpectrumBL
Public Class frmExpeditorScreen
    ' Dim lblOrderHeader, lblOrderHeader2, lblOrderHeader3, lblOrderItemColHdr, lblOrderQtyColHdr, lblarticlename, lblarticleqty As Spectrum.CtrlLabel
    Dim objclsExpeditor As New clsExpeditor
    Dim LastBillStampTime As Nullable(Of DateTime) = Nothing
    ReadOnly DelayOrderTimeLimitInSec As Long = clsDefaultConfiguration.OrdPrepTime * 60
    Dim OrderBillTimer As New Timer()
    Dim LoadNewBillsTimerCount As Integer = 0

    Structure HoldingCell
        Dim cntrl As Control
        Dim pos As TableLayoutPanelCellPosition
    End Structure

    Enum enumScreenMode
        Bump = 0
        Retrive = 1
    End Enum

    Private _ScreenMode As Int16
    Public Property ScreenMode() As Int16
        Get
            Return _ScreenMode
        End Get
        Set(ByVal value As Int16)
            _ScreenMode = value
            If ScreenMode = enumScreenMode.Bump Then
                btnScreenMode.Text = "Retrieve"
                btnScreenMode.BackColor = Color.FromArgb(0, 114, 188)
            Else
                btnScreenMode.Text = "Pending"
                btnScreenMode.BackColor = Color.FromArgb(251, 104, 22)
            End If
            'cmbRetrive.Location = New Point(pnlRetriveBtn.Width - BtnScreenMode.Width - 20, Me.pnlRetriveBtn.Top + 10)
        End Set
    End Property

    Private Sub frmExpeditorScreen_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            OrderBillTimer.Stop()
            OrderBillTimer.Dispose()

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub frmExpeditorScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ScreenMode = enumScreenMode.Bump Then
            btnScreenMode.BackColor = Color.FromArgb(0, 114, 188)
        Else
            btnScreenMode.BackColor = Color.FromArgb(251, 104, 22)
        End If
        pnlRetriveBtn.BackColor = Color.FromArgb(25, 44, 59)
        btnScreenMode.ForeColor = Color.FromArgb(239, 239, 239)
        btnScreenMode.TextAlign = ContentAlignment.MiddleCenter
        pnlRetriveBtn.Location = New Point(Me.Location.X + 1, Me.Top + 1)
        pnlRetriveBtn.Size = New Size(Me.Width - 20, btnScreenMode.Height + 10)
        FlwPanel.Margin = New Padding(0)
        FlwPanel.Location = New Point(Me.Location.X + 1, pnlRetriveBtn.Bottom + 1)
        FlwPanel.Size = New Size(Me.Width - 20, Me.Height - pnlRetriveBtn.Height - 50)

        FlwRetrive.Location = New Point(Me.Location.X + 1, pnlRetriveBtn.Bottom + 1)
        FlwRetrive.Size = New Size(Me.Width - 20, Me.Height - pnlRetriveBtn.Height - 50)
        'btnScreenMode.Location = New Point(pnlRetriveBtn.Right - btnScreenMode.Width, )
        btnScreenMode.Top = 10
        btnScreenMode.Size = New Size(80, 28)
        loadNewBills()

        OrderBillTimer.Start()
        AddHandler OrderBillTimer.Tick, AddressOf BillTimer_Tick
        OrderBillTimer.Interval = Convert.ToInt32(1000)

    End Sub

    Protected Sub AddOrderScreenDetails(ByVal billno As String, ByVal dtArticleDetails As DataTable)
        Dim TblOrder As New TableLayoutPanel
        Dim LineRowCount As Int16 = 0
        'AddHandler TableLayoutPanel1.CellPaint, AddressOf TableLayoutPanel1_CellPaint
        TblOrder.ColumnCount = 2
        TblOrder.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.0!))
        TblOrder.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        TblOrder.CellBorderStyle = TableLayoutPanelCellBorderStyle.None

        If ScreenMode = enumScreenMode.Bump Then
            TblOrder.Width = FlwPanel.Width / 3 - 15
        Else
            TblOrder.Width = FlwRetrive.Width / 3 - 16
        End If

        Dim lblOrderNo, lblTerminalId, lblTimeElasped, lblItem, lblQty, lblArticleName, lblArticleQty, lblTillIdValue, lblElaspedTimeValue, lblblank As New Spectrum.CtrlLabel
        Dim cmdBumb As New Button

        'HEADER ORDER NO Row SET 
        '
        lblOrderNo.VisualStyle = C1.Win.C1Input.VisualStyle.System
        lblOrderNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblOrderNo.BackColor = Color.FromArgb(0, 114, 188)
        lblOrderNo.ForeColor = Color.White
        lblOrderNo.TextDetached = True
        lblOrderNo.Name = "OrderNo"
        lblOrderNo.Tag = Nothing
        lblOrderNo.Text = "Order No: " & billno
        lblOrderNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        lblOrderNo.Margin = New Padding(0)
        lblOrderNo.Padding = New Padding(5)
        lblOrderNo.Anchor = AnchorStyles.Left
        lblOrderNo.Dock = DockStyle.Fill
        lblOrderNo.BorderStyle = BorderStyle.None

        '---SET ROW-1
        With TblOrder
            .SetColumnSpan(lblOrderNo, 2)
            .RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
            .RowCount = 1
            .Controls.Add(lblOrderNo, 0, 0)

        End With

        'Label3
        '
        lblTimeElasped.AutoSize = True
        lblTimeElasped.VisualStyle = C1.Win.C1Input.VisualStyle.System
        lblTimeElasped.BackColor = System.Drawing.Color.White
        lblTimeElasped.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTimeElasped.ForeColor = Color.FromArgb(78, 78, 78)
        lblTimeElasped.Name = "TimeElasped"
        lblTimeElasped.Tag = Nothing
        lblTimeElasped.Text = "Elasped Time "
        lblTimeElasped.Anchor = AnchorStyles.None
        lblTimeElasped.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        lblTimeElasped.Margin = New Padding(0)
        lblTimeElasped.Padding = New Padding(2)
        lblTimeElasped.Dock = DockStyle.Fill

        lblElaspedTimeValue.AutoSize = True
        lblElaspedTimeValue.VisualStyle = C1.Win.C1Input.VisualStyle.System
        lblElaspedTimeValue.BackColor = System.Drawing.Color.White
        lblElaspedTimeValue.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblElaspedTimeValue.ForeColor = Color.FromArgb(78, 78, 78)
        lblElaspedTimeValue.Name = billno
        lblElaspedTimeValue.Tag = Nothing
        lblElaspedTimeValue.Dock = DockStyle.Fill
        lblElaspedTimeValue.Anchor = AnchorStyles.None

        lblElaspedTimeValue.TextAlign = ContentAlignment.MiddleLeft
        lblElaspedTimeValue.TextDetached = True


        lblElaspedTimeValue.Margin = New Padding(0)
        lblElaspedTimeValue.Padding = New Padding(2)

        '---SET ROW-2 
        TblOrder.RowCount += 1
        TblOrder.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        TblOrder.Controls.Add(lblTimeElasped, 0, 1)
        TblOrder.Controls.Add(lblElaspedTimeValue, 1, 1)

        '
        'Label2
        '
        lblTerminalId.AutoSize = True
        lblTerminalId.VisualStyle = C1.Win.C1Input.VisualStyle.System
        lblTerminalId.BackColor = System.Drawing.Color.White
        lblTerminalId.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTerminalId.ForeColor = Color.FromArgb(78, 78, 78)
        lblTerminalId.Name = "TillId"
        lblTerminalId.TabIndex = 2
        lblTerminalId.Tag = Nothing
        lblTerminalId.Text = "Till No "
        lblTerminalId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        lblTerminalId.TextDetached = True
        lblTerminalId.Dock = DockStyle.Fill
        lblTerminalId.Padding = New Padding(5)
        lblTerminalId.Margin = New Padding(0)
        lblTerminalId.Dock = DockStyle.Fill

        'Label4
        '
        lblItem.VisualStyle = C1.Win.C1Input.VisualStyle.System
        'lblItem.AutoSize = True
        lblItem.BackColor = Color.FromArgb(232, 232, 232)
        lblItem.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblItem.ForeColor = Color.FromArgb(0, 114, 188)
        lblItem.Tag = Nothing
        lblItem.Text = "Item"
        lblItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        lblItem.TextDetached = True
        lblItem.Dock = DockStyle.Fill
        lblItem.Margin = New Padding(0)
        lblItem.Padding = New Padding(5)

        '
        'Label5
        '

        lblQty.VisualStyle = C1.Win.C1Input.VisualStyle.System
        ' lblQty.AutoSize = True
        lblQty.BackColor = Color.FromArgb(232, 232, 232)
        lblQty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblQty.ForeColor = Color.FromArgb(0, 114, 188)
        lblQty.Name = "Qty"
        lblQty.Tag = Nothing
        lblQty.Text = "Qty"
        lblQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        lblQty.TextDetached = True
        lblQty.Margin = New Padding(0)
        lblQty.Dock = DockStyle.Fill

        lblQty.Padding = New Padding(5)

        lblTillIdValue.VisualStyle = C1.Win.C1Input.VisualStyle.System
        lblTillIdValue.AutoSize = True
        lblTillIdValue.BackColor = System.Drawing.Color.White
        lblTillIdValue.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblTillIdValue.ForeColor = Color.FromArgb(78, 78, 78)
        lblTillIdValue.Name = ""
        lblTillIdValue.Tag = Nothing
        lblTillIdValue.Text = clsAdmin.TerminalID
        lblTillIdValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        lblTillIdValue.TextDetached = True
        lblTillIdValue.Dock = DockStyle.Fill
        lblElaspedTimeValue.Anchor = AnchorStyles.None
        lblTillIdValue.Margin = New Padding(0)
        lblTillIdValue.Padding = New Padding(5)





        cmdBumb.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        cmdBumb.ForeColor = Color.FromArgb(255, 255, 255)
        cmdBumb.MinimumSize = New System.Drawing.Size(15, 23)
        cmdBumb.Name = "cmdBumb"
        cmdBumb.Size = New System.Drawing.Size(70, 28)
        cmdBumb.Enabled = False
        cmdBumb.TextAlign = ContentAlignment.MiddleCenter
        cmdBumb.FlatStyle = FlatStyle.Flat

        If ScreenMode = enumScreenMode.Bump Then
            cmdBumb.Text = "Bump"
        Else
            cmdBumb.Text = "Retrive"
            cmdBumb.Enabled = True
            cmdBumb.BackColor = Color.FromArgb(0, 114, 188)
        End If

        cmdBumb.Anchor = AnchorStyles.Left
        AddHandler cmdBumb.Click, AddressOf Me.cmdBump_Click

        '---SET Blank Row 
        TblOrder.RowCount += 1
        TblOrder.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2.0!))
        lblblank = New CtrlLabel()
        lblblank.ForeColor = Color.Gray
        lblblank.Dock = DockStyle.Fill
        lblblank.VisualStyle = C1.Win.C1Input.VisualStyle.System
        lblblank.Margin = New Padding(0)
        lblblank.Padding = New Padding(0)
        TblOrder.SetColumnSpan(lblblank, 2)
        TblOrder.Controls.Add(lblblank, 0, 2)


        '---SET ROW-3 
        TblOrder.RowCount += 1
        TblOrder.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        TblOrder.Controls.Add(lblTerminalId, 0, 3)
        TblOrder.Controls.Add(lblTillIdValue, 1, 3)

        '---SET ROW-4
        TblOrder.RowCount += 1
        TblOrder.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44.0!))
        TblOrder.Controls.Add(lblItem, 0, 4)
        TblOrder.Controls.Add(lblQty, 1, 4)

        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font

        Dim IsItemReady As Boolean = True
        Dim IsOrderReady As Boolean = False
        Dim IsDefaultArticle As Boolean = False
        For index = 0 To dtArticleDetails.Rows.Count - 1
            lblArticleName = New Spectrum.CtrlLabel
            lblArticleName.AttachedTextBoxName = Nothing
            lblArticleName.AutoSize = True
            lblArticleName.Anchor = AnchorStyles.Left
            lblArticleName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            lblArticleName.Margin = New Padding(0)
            lblArticleName.BackColor = Color.White
            lblArticleName.BorderStyle = BorderStyle.FixedSingle
            Dim ElaspedTime As DateTime = dtArticleDetails.Rows(index)("CREATEDON")
            lblArticleName.TextAlign = ContentAlignment.MiddleLeft
            lblArticleName.Dock = DockStyle.Fill
            lblArticleName.Padding = New Padding(5)

            lblElaspedTimeValue.Value = ElaspedTime & "@"
            If Not dtArticleDetails.Rows(index)("isItemReady") Then
                IsItemReady = False
            End If
            If String.IsNullOrEmpty(dtArticleDetails.Rows(index)("mstPrepStationID").ToString()) Then
                IsDefaultArticle = True
            End If

            If IsItemReady Then
                If ScreenMode = enumScreenMode.Bump Then
                    If IsDefaultArticle Then
                        lblArticleName.ForeColor = Color.FromArgb(177, 177, 177)
                    Else
                        lblArticleName.ForeColor = Color.FromArgb(0, 166, 19)
                    End If

                Else
                    lblArticleName.ForeColor = Color.FromArgb(177, 177, 177)
                End If
            Else
                'lblArticleName.ForeColor = Color.FromArgb(177, 177, 177)
                If IsDefaultArticle Then
                    lblArticleName.ForeColor = Color.FromArgb(177, 177, 177)
                Else
                    lblArticleName.ForeColor = Color.FromArgb(78, 78, 78)
                End If
            End If

            lblArticleName.BorderStyle = System.Windows.Forms.BorderStyle.None
            lblArticleName.Name = "itemDesc" '& dtArticleDetails.Rows(index)("articleCode")
            lblArticleName.Size = New System.Drawing.Size(36, 10)

            ' Label6.ForeColor = Color.FromArgb(37, 37, 37)
            lblArticleName.Tag = Nothing
            If clsDefaultConfiguration.PrintItemFullName Then
                lblArticleName.Text = dtArticleDetails.Rows(index)("ArticleName")
            Else
                lblArticleName.Text = dtArticleDetails.Rows(index)("ArticleShortName")
            End If
            lblArticleName.TextDetached = True
            lblArticleName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue

            '--lblItemQty
            lblArticleQty = New Spectrum.CtrlLabel
            lblArticleQty.AttachedTextBoxName = Nothing
            lblArticleQty.AutoSize = True
            lblArticleQty.Anchor = AnchorStyles.Left
            lblArticleQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            lblArticleQty.Margin = New Padding(0)
            lblArticleQty.BackColor = Color.White
            lblArticleQty.Dock = DockStyle.Fill
            lblArticleQty.BorderStyle = BorderStyle.FixedSingle

            If IsItemReady AndAlso ScreenMode = enumScreenMode.Bump Then
                If IsDefaultArticle Then
                    lblArticleQty.ForeColor = Color.FromArgb(177, 177, 177)
                Else
                    lblArticleQty.ForeColor = Color.FromArgb(0, 166, 19)
                End If
                ' lblArticleQty.ForeColor = Color.FromArgb(0, 166, 19)
            Else
                If IsDefaultArticle Then
                    lblArticleQty.ForeColor = Color.FromArgb(177, 177, 177)
                Else
                    lblArticleQty.ForeColor = Color.FromArgb(78, 78, 78)
                End If

            End If
            'End If
            lblArticleQty.TextAlign = ContentAlignment.MiddleLeft

            Dim OrderElapsedTimeInSeconds As Long = DateDiff(DateInterval.Second, ElaspedTime, IIf(LastBillStampTime Is Nothing, DateTime.Now, LastBillStampTime))
            Dim OrderElapsedTimeString As String
            If (OrderElapsedTimeInSeconds Mod 60) > 9 Then
                OrderElapsedTimeString = Math.Floor(OrderElapsedTimeInSeconds / 60).ToString() & ":" & (OrderElapsedTimeInSeconds Mod 60).ToString()
            Else
                OrderElapsedTimeString = Math.Floor(OrderElapsedTimeInSeconds / 60).ToString() & ":0" & (OrderElapsedTimeInSeconds Mod 60).ToString()
            End If

            lblElaspedTimeValue.Text = String.Format(OrderElapsedTimeString, OrderElapsedTimeString) & Space(400)

            If OrderElapsedTimeInSeconds > DelayOrderTimeLimitInSec Then
                If ScreenMode = enumScreenMode.Retrive Then
                    lblOrderNo.ForeColor = Color.White
                Else
                    lblOrderNo.ForeColor = Color.FromArgb(255, 242, 0)
                End If
            End If


            lblArticleQty.BorderStyle = System.Windows.Forms.BorderStyle.None
            'Label7.ForeColor = Color.FromArgb(37, 37, 37)
            lblArticleQty.Name = "ItemQty" '& dtArticleDetails.Rows(index)("")
            lblArticleQty.Size = New System.Drawing.Size(36, 10)
            lblArticleQty.Tag = Nothing
            lblArticleQty.Text = dtArticleDetails.Rows(index)("Quantity")
            lblArticleQty.TextDetached = True
            lblArticleQty.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue
            lblArticleQty.Padding = New Padding(5)

            TblOrder.RowCount += 1
            TblOrder.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
            TblOrder.Controls.Add(lblArticleName, 0, 5 + LineRowCount + index)
            TblOrder.Controls.Add(lblArticleQty, 1, 5 + LineRowCount + index)

            TblOrder.RowCount += 1
            LineRowCount += 1
            Dim lblblankItem = New CtrlLabel()
            lblblankItem.ForeColor = Color.Gray
            lblblankItem.Dock = DockStyle.Fill
            lblblankItem.VisualStyle = C1.Win.C1Input.VisualStyle.System
            lblblankItem.Margin = New Padding(0)
            lblblankItem.Padding = New Padding(0)
            lblblankItem.Anchor = AnchorStyles.Left
            TblOrder.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2.0!))
            TblOrder.SetColumnSpan(lblblankItem, 2)
            TblOrder.Controls.Add(lblblankItem, 0, 5 + LineRowCount + index)


            ' IsOrderReady = dtArticleDetails.Rows(index)("isOrderReady")

            IsDefaultArticle = False
        Next

        Dim backgroundColorPanel As New Panel()
        backgroundColorPanel.Dock = DockStyle.Fill
        backgroundColorPanel.Margin = New Padding(0)
        backgroundColorPanel.Anchor = DirectCast(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left, System.Windows.Forms.AnchorStyles)
        backgroundColorPanel.Size = New Size(100, 35)
        backgroundColorPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        backgroundColorPanel.Controls.Add(cmdBumb)
        backgroundColorPanel.Padding = New Padding(5)
        backgroundColorPanel.BackColor = Color.White

        TblOrder.SetColumnSpan(backgroundColorPanel, 2)
        TblOrder.RowCount += 1
        TblOrder.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        TblOrder.Controls.Add(backgroundColorPanel, 0, TblOrder.RowCount - 1)

        TblOrder.Name = billno
        TblOrder.Margin = New Padding(5, 5, 5, 5)
        TblOrder.ForeColor = Color.SkyBlue

        If IsItemReady Then
            cmdBumb.Enabled = True
            cmdBumb.BackColor = Color.FromArgb(0, 166, 19)
        Else
            cmdBumb.Enabled = False
            cmdBumb.BackColor = Color.FromArgb(213, 211, 211)
            cmdBumb.ForeColor = Color.FromArgb(160, 160, 160)
        End If

        Dim TotalRowsHeight As Int16
        For index = 0 To TblOrder.RowCount - 1 Step 1
            TotalRowsHeight += TblOrder.RowStyles(index).Height
        Next
        TblOrder.Height = TotalRowsHeight + 10

        If ScreenMode = enumScreenMode.Retrive Then
            FlwRetrive.Controls.Add(TblOrder)
            FlwRetrive.BackColor = Color.FromArgb(234, 233, 229)
        Else
            FlwPanel.Controls.Add(TblOrder)
            FlwPanel.BackColor = Color.FromArgb(234, 233, 229)
        End If


    End Sub

    Public Sub loadNewBills()
        '---- Load All Bills ...
        Dim DtNewBills As DataTable = objclsExpeditor.GetNewBills(clsAdmin.SiteCode, clsAdmin.PrepStationID, clsAdmin.DayOpenDate, LastBillStampTime)
        If DtNewBills IsNot Nothing AndAlso DtNewBills.Rows.Count > 0 Then
            Dim dvNewBills As New DataView(DtNewBills, "", "", DataViewRowState.CurrentRows)
            Dim dtview As New DataView(DtNewBills, "", "", DataViewRowState.CurrentRows)
            Dim dt As DataTable = dtview.ToTable(True, "BillNo", "IsOrderReady")
            For index = 0 To dt.Rows.Count - 1
                If Not dt.Rows(index)("IsOrderReady") Then
                    AddOrderScreenDetails(dt.Rows(index)("BillNo"), DtNewBills.Select("BillNo='" & dt.Rows(index)("BillNo") & "'").CopyToDataTable)
                End If
            Next
        End If
    End Sub

    Private Sub cmdBump_Click(sender As Object, e As EventArgs)
        Try
            Dim button As Button = TryCast(sender, Button)
            If button IsNot Nothing Then
                Dim ItemCode As String = String.Empty
                Dim BillNo As String = String.Empty

                ItemCode = button.Name
                Dim TLOrder As TableLayoutPanel = TryCast(button.Parent.Parent, TableLayoutPanel)
                BillNo = TLOrder.Name

                If FlwPanel.Controls.Count > 0 Then
                    If ScreenMode = enumScreenMode.Bump Then
                        If objclsExpeditor.UpdateOrderReady(BillNo) Then
                            FlwPanel.Controls.Remove(TLOrder)
                        End If
                    Else
                        Call objclsExpeditor.UpdateOrderRetrive(BillNo)
                        FlwRetrive.Controls.Remove(TLOrder)
                    End If

                End If

            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub LoadRetriveOrders()
        '---- Load All Bills ...
        Dim DtNewBills As DataTable = objclsExpeditor.RetriveBills(clsAdmin.SiteCode, clsAdmin.PrepStationID, clsAdmin.DayOpenDate)
        If DtNewBills IsNot Nothing AndAlso DtNewBills.Rows.Count > 0 Then
            Dim dvNewBills As New DataView(DtNewBills, "", "", DataViewRowState.CurrentRows)
            Dim dtview As New DataView(DtNewBills, "", "", DataViewRowState.CurrentRows)
            Dim dt As DataTable = dtview.ToTable(True, "BillNo")
            For index = 0 To dt.Rows.Count - 1
                AddOrderScreenDetails(dt.Rows(index)("BillNo"), DtNewBills.Select("BillNo='" & dt.Rows(index)("BillNo") & "'").CopyToDataTable)
            Next
        End If
    End Sub



    Private Sub cmbRetrive_Click(sender As Object, e As EventArgs) Handles btnScreenMode.Click
        Try
            If ScreenMode = enumScreenMode.Bump Then
                ScreenMode = enumScreenMode.Retrive
                FlwRetrive.Controls.Clear()
                Call LoadRetriveOrders()
                FlwPanel.Visible = False
                FlwRetrive.Visible = True
            Else
                ScreenMode = enumScreenMode.Bump
                FlwPanel.Controls.Clear()

                Call loadNewBills()
                FlwPanel.Visible = True
                FlwRetrive.Visible = False
            End If
        Catch ex As Exception
            LogException(ex)
        End Try


    End Sub

    Private Sub BillTimer_Tick(sender As Object, e As EventArgs)
        LoadNewBillsTimerCount = LoadNewBillsTimerCount + 1
        '--- Update Time Tick to all orders 
        If LoadNewBillsTimerCount = 10 Then
            LoadNewBillsTimerCount = 0
            '  FlwPanel.Controls.Clear()
            Call loadNewBills()
            '  For Each OrderFlowLayout As Control In FLOrders.Controls
            For Each TblOrder As Control In FlwPanel.Controls
                For Each lblElaspedTimeValue As Control In TblOrder.Controls
                    If TypeOf lblElaspedTimeValue Is CtrlLabel Then
                        ' For Each lblOrderHeader2 As Control In flHeader2.Controls
                        If lblElaspedTimeValue.Name.ToString.ToUpper = TblOrder.Name.ToString.ToUpper Then
                            Dim orderData = DirectCast(lblElaspedTimeValue, Spectrum.CtrlLabel).Value.ToString.Split("@")
                            Dim OrderTime = Convert.ToDateTime(orderData(0))
                            Dim OrderTitle = orderData(1)
                            Dim OrderElapsedTimeInSeconds As Long = DateDiff(DateInterval.Second, OrderTime, IIf(LastBillStampTime Is Nothing, DateTime.Now, LastBillStampTime))
                            Dim OrderElapsedTimeString As String
                            If (OrderElapsedTimeInSeconds Mod 60) > 9 Then
                                OrderElapsedTimeString = Math.Floor(OrderElapsedTimeInSeconds / 60).ToString() & ":" & (OrderElapsedTimeInSeconds Mod 60).ToString()
                            Else
                                OrderElapsedTimeString = Math.Floor(OrderElapsedTimeInSeconds / 60).ToString() & ":0" & (OrderElapsedTimeInSeconds Mod 60).ToString()
                            End If
                            lblElaspedTimeValue.Text = String.Format(OrderElapsedTimeString) & Space(400)
                        End If
                    End If
                Next
            Next
        End If


    End Sub

End Class