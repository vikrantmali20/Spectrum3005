Imports SpectrumBL
Public Class frmPrepStation
    Dim objKdsData As New clsKdsData
    Dim OrderBillTimer As New Timer()
    Dim LoadNewBillsTimerCount As Integer = 0
    Dim LastBillStampTime As Nullable(Of DateTime) = Nothing
    ReadOnly DelayOrderTimeLimitInSec As Long = clsDefaultConfiguration.OrdPrepTime * 60
    Dim tipimage As ToolTip = New ToolTip()
    Dim tempToolText As String = "" 'vipin
    'Dim MarginOrBorderRatio As Integer = 0

    Structure HoldingCell
        Dim cntrl As Control
        Dim pos As TableLayoutPanelCellPosition
    End Structure

    Public Sub New(ByVal IsMdiformCall As Boolean)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False

        If (Not IsMdiformCall) Then
            Me.WindowState = FormWindowState.Maximized
            Me.AutoSize = False
            Me.MaximumSize = New Point(Screen.PrimaryScreen.Bounds.Width - 10, Screen.PrimaryScreen.Bounds.Height - 20)
            Me.MinimumSize = New Point(Screen.PrimaryScreen.Bounds.Width - 10, Screen.PrimaryScreen.Bounds.Height - 20)
        Else
            Me.WindowState = FormWindowState.Normal
        End If
        BtnScreenMode.ForeColor = Color.FromArgb(239, 239, 239)
        BtnScreenMode.TextAlign = ContentAlignment.MiddleCenter
        pnlRetriveBtn.BackColor = Color.FromArgb(25, 44, 59)

        pnlRetriveBtn.Location = New Point(Me.Location.X + 1, Me.Top + 1)
        pnlRetriveBtn.Size = New Size(Me.Width - 20, BtnScreenMode.Height + 20)
        FLOrders.Margin = New Padding(0)
        FLOrders.Location = New Point(Me.Location.X + 1, pnlRetriveBtn.Bottom + 1)
        FLOrders.Size = New Size(Me.Width - 20, Me.Height - pnlRetriveBtn.Height - 50)

        FLOrderRetrive.Location = New Point(Me.Location.X + 1, pnlRetriveBtn.Bottom + 1)
        FLOrderRetrive.Size = New Size(Me.Width - 20, Me.Height - pnlRetriveBtn.Height - 50)

    End Sub
    Dim proposedSize As Size = New Size(500, 500)
    Private Sub toolTip1_Draw(ByVal sender As Object, ByVal e As DrawToolTipEventArgs)
        Dim f As Font
        f = New Font("calibri", 13.0F, FontStyle.Bold)
        e.DrawBackground()
        e.DrawBorder()

        tempToolText = e.ToolTipText
        'Dim textBounds As Rectangle = New Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width + 20, e.Bounds.Height)
        If tempToolText.Length > 105 Then
            proposedSize = New Size(1000, 1000)
        Else
            proposedSize = New Size(500, 500)
        End If


        Dim textBounds As Rectangle = New Rectangle(e.Bounds.Location, e.Bounds.Size)
        e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, textBounds)
    End Sub
    Private Sub toolTip1_Popup(ByVal sender As Object, ByVal e As PopupEventArgs)
        'e.ToolTipSize = New Size(200, 100)
        ' on popip set the size of tool tip
        'e.ToolTipSize = TextRenderer.MeasureText(tipimage.GetToolTip(e.AssociatedControl), New Font("calibri", 20.0F)) 'TextRenderer.MeasureText(strArt, New Font("Arial", 25.0F))
        'e.
        ' Dim proposedSize As Size = New Size(100, 100)
        tipimage.UseAnimation = True
        tipimage.GetToolTip(e.AssociatedControl)
        Dim flags As TextFormatFlags = TextFormatFlags.WordBreak

        e.ToolTipSize = TextRenderer.MeasureText(tipimage.GetToolTip(e.AssociatedControl), New Font("calibri", 13.0F, FontStyle.Bold), proposedSize, flags) 'TextRenderer.MeasureText(strArt, New Font("Arial", 25.0F))

    End Sub
    Private _ScreenMode As Int16
    Public Property ScreenMode() As Int16
        Get
            Return _ScreenMode
        End Get
        Set(ByVal value As Int16)
            _ScreenMode = value
            If ScreenMode = enumScreenMode.Order Then
                BtnScreenMode.Text = "Retrieve"
                BtnScreenMode.BackColor = Color.FromArgb(0, 114, 188)
            Else
                BtnScreenMode.Text = "Pending Orders"
                BtnScreenMode.BackColor = Color.FromArgb(251, 104, 22)
            End If
            BtnScreenMode.Location = New Point(pnlRetriveBtn.Width - BtnScreenMode.Width - 20, Me.pnlRetriveBtn.Top + 10)
        End Set
    End Property

    Enum enumScreenMode
        Order = 1
        Retrive = 2
    End Enum

    Private Sub frmPrepStation_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ScreenMode = enumScreenMode.Order
            FLOrders.Visible = True
            FLOrderRetrive.Visible = False
            FLOrders.BackColor = Color.FromArgb(255, 255, 255)
            FLOrderRetrive.BackColor = Color.FromArgb(255, 255, 255)
            Call AddPanelHeading(FLOrders)
            Call loadNewBills()
            'If Val(clsDefaultConfiguration.OrdPrepTime) > 0 Then
            '    DelayOrderTimeLimitInSec = clsDefaultConfiguration.OrdPrepTime * 60
            'Else
            '    DelayOrderTimeLimitInSec = 15 * 60
            'End If
            OrderBillTimer.Start()
            AddHandler OrderBillTimer.Tick, AddressOf BillTimer_Tick
            OrderBillTimer.Interval = Convert.ToInt32(1000)
            Me.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub complete_Click(sender As Object, e As EventArgs)
        Try
            Dim flowlayout As FlowLayoutPanel
            Dim button As Button = TryCast(sender, Button)
            If button IsNot Nothing Then
                If ScreenMode = enumScreenMode.Order Then
                    flowlayout = FLOrders
                Else
                    flowlayout = FLOrderRetrive
                End If
                'flowlayout.SuspendLayout()
                'flowlayout.AutoScroll = False
                Dim ItemCode As String = String.Empty
                Dim BillNo As String = String.Empty

                ItemCode = button.Name
                Dim TLOrder As TableLayoutPanel = TryCast(button.Parent, TableLayoutPanel)
                BillNo = TLOrder.Name

                If TLOrder.RowCount > 2 Then
                    Dim ReadyItemRowNo As Integer = TLOrder.GetRow(button)
                    remove_row(TLOrder, ReadyItemRowNo)
                    'Dim MarginOrBorderRatio = TLOrder.Tag / 18
                    'TLOrder.Height -= (35 + MarginOrBorderRatio)
                    Dim MarginOrBorderRatio = TLOrder.RowCount / 16
                    TLOrder.Height = (TLOrder.RowCount + MarginOrBorderRatio) * 35
                Else
                    flowlayout.Controls.Remove(TLOrder)
                End If
                'flowlayout.ResumeLayout()
                'flowlayout.AutoScroll = True
                If ScreenMode = enumScreenMode.Order Then
                    Call objKdsData.UpdateItemReady(BillNo, ItemCode)
                Else
                    Call objKdsData.UpdateItemRetrive(BillNo, ItemCode)
                End If
            End If

        Catch ex As Exception
            LogException(ex)
        End Try

        'DtBillsDtls
        'Dim selectedRows() As DataRow = dtOrderdtl.Select("Select=True", "", DataViewRowState.CurrentRows)
        'For Each dr As DataRow In selectedRows
        '    Dim ArticleCode As String = dr("ArticleCode")
        '    ' obj.CompleteArticle(ArticleCode)
        '    dr.Delete()
        'Next

    End Sub
    Private Sub BtnKitArticle_Click(sender As Object, e As EventArgs)
        Try
            Dim button As Button = TryCast(sender, Button)
            Dim Ingredients As String = button.Name
            ShowFullScreenMessage(Ingredients, "Ingredients Details")
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub remove_row(panel As TableLayoutPanel, row_index_to_remove As Integer)
        Dim c As Control
        Dim tempHolding As New List(Of HoldingCell)
        Dim cell As HoldingCell

        With panel
            'Delete all controls on selected row 
            For col As Int32 = 0 To .ColumnCount - 1
                c = .GetControlFromPosition(column:=col, row:=row_index_to_remove)
                If c IsNot Nothing Then
                    .Controls.RemoveByKey(c.Name) 'remove it from the controls collection 
                    c.Dispose() 'get rid of it 
                End If
            Next col

            'Temporarly Store the Positions 
            For row As Int32 = row_index_to_remove + 1 To panel.RowCount - 1
                For col As Int32 = 0 To panel.ColumnCount - 1
                    cell = New HoldingCell
                    cell.cntrl = .GetControlFromPosition(col, row)
                    'setup position for restore = current row -1 
                    cell.pos = New TableLayoutPanelCellPosition(col, row - 1)
                    tempHolding.Add(cell)
                Next col
            Next row

            'delete the row 
            .RowStyles.RemoveAt(index:=row_index_to_remove) 'deletes the style only 
            .RowCount -= 1

            'adjust control positions 
            For Each cell In tempHolding
                If cell.cntrl IsNot Nothing Then
                    .SetCellPosition(cell.cntrl, cell.pos)
                End If
            Next cell
        End With
        tempHolding = Nothing

    End Sub

    Private Sub BillTimer_Tick(ByVal sender As Object, e As EventArgs)
        Try
            LoadNewBillsTimerCount = LoadNewBillsTimerCount + 1
            '--- Update Time Tick to all orders 
            If LoadNewBillsTimerCount = 10 Then
                LoadNewBillsTimerCount = 0
                Call loadNewBills()
                '  For Each OrderFlowLayout As Control In FLOrders.Controls
                For Each TLOrder As Control In FLOrders.Controls
                    For Each lblOrderHeader2 As Control In TLOrder.Controls
                        If TypeOf lblOrderHeader2 Is CtrlLabel Then
                            ' For Each lblOrderHeader2 As Control In flHeader2.Controls
                            If lblOrderHeader2.Name.ToString.ToUpper = TLOrder.Name.ToString.ToUpper Then
                                Dim orderData = DirectCast(lblOrderHeader2, Spectrum.CtrlLabel).Value.ToString.Split("@")
                                Dim OrderTime = Convert.ToDateTime(orderData(0))
                                Dim OrderTitle = orderData(1)
                                Dim OrderElapsedTimeInSeconds As Long = DateDiff(DateInterval.Second, OrderTime, IIf(LastBillStampTime Is Nothing, DateTime.Now, LastBillStampTime))
                                Dim OrderElapsedTimeString As String
                                If (OrderElapsedTimeInSeconds Mod 60) > 9 Then
                                    OrderElapsedTimeString = Math.Floor(OrderElapsedTimeInSeconds / 60).ToString() & ":" & (OrderElapsedTimeInSeconds Mod 60).ToString()
                                Else
                                    OrderElapsedTimeString = Math.Floor(OrderElapsedTimeInSeconds / 60).ToString() & ":0" & (OrderElapsedTimeInSeconds Mod 60).ToString()
                                End If
                                'FLOrders.SuspendLayout()
                                'FLOrders.AutoScroll = False
                                lblOrderHeader2.Text = String.Format(OrderTitle, OrderElapsedTimeString) & Space(400)
                                If OrderElapsedTimeInSeconds > DelayOrderTimeLimitInSec Then
                                    lblOrderHeader2.ForeColor = Color.FromArgb(237, 28, 36)
                                Else
                                    lblOrderHeader2.ForeColor = Color.FromArgb(0, 114, 188)
                                End If

                                ' FLOrders.AutoScroll = True
                                'FLOrders.ResumeLayout()
                                'FLOrders.PerformLayout()
                                'Me.PerformLayout()
                                'Exit For
                            End If
                            '   Next
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub frmPrepStation_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            OrderBillTimer.Stop()
            OrderBillTimer.Dispose()

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub loadNewBills()
        '---- Load All Bills ...
        Dim DtNewBills As DataTable = objKdsData.GetNewBills(clsAdmin.SiteCode, clsAdmin.PrepStationID, clsAdmin.DayOpenDate, LastBillStampTime)
        If DtNewBills IsNot Nothing AndAlso DtNewBills.Rows.Count > 0 Then
            Dim dvNewBills As New DataView(DtNewBills, "", "", DataViewRowState.CurrentRows)

            Dim DtOrderList As DataTable = dvNewBills.ToTable(True, "BillNo", "TerminalName", "CREATEDON")
            For Each Ordrow As DataRow In DtOrderList.Rows
                Dim DvOrderDtls As New DataView(DtNewBills, "BillNo='" & Ordrow("BillNo") & "'", "", DataViewRowState.CurrentRows)
                'FLOrders.SuspendLayout()
                'FLOrders.AutoScroll = False

                Call AddPanel(FLOrders, Ordrow("BillNo"), Ordrow("TerminalName"), Ordrow("CREATEDON"), DvOrderDtls.ToTable())
                'FLOrders.ResumeLayout()
                'FLOrders.AutoScroll = True
            Next Ordrow
        End If
    End Sub


    Dim TLOrder As System.Windows.Forms.TableLayoutPanel
    '''---Friend WithEvents TLOrder As System.Windows.Forms.TableLayoutPanel
    Dim lblOrderItemColHdr As Spectrum.CtrlLabel
    Dim lblOrderHeader, lblOrderHeader2 As Spectrum.CtrlLabel
    Dim lblOrderHeaderCreatedOn As Spectrum.CtrlLabel
    Dim lblOrderQtyColHdr As Spectrum.CtrlLabel
    Dim lblOrderTimeColHdr As Spectrum.CtrlLabel
    Dim lblOrderActionColHdr As Spectrum.CtrlLabel
    Dim LblKitArticleHdr As Spectrum.CtrlLabel
    Dim btnComplete As System.Windows.Forms.Button
    Dim lblItemDesc As Spectrum.CtrlLabel
    Dim lblItemQty As Spectrum.CtrlLabel
    Dim btnKitArticle As System.Windows.Forms.Button
    Dim FLowKitArticle As FlowLayoutPanel

    Dim FontTableHdr = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Dim FontTitleHdr = New System.Drawing.Font("Arial", 21.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Dim FontContent = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Dim btnfont = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

    Private Sub AddPanelHeading(ByRef FLOrder As FlowLayoutPanel)
        TLOrder = New System.Windows.Forms.TableLayoutPanel
        TLOrder.Width = FLOrder.Width - 20
        TLOrder.Name = "TableHead"
        TLOrder.Height = 30
        TLOrder.BackColor = Color.FromArgb(192, 192, 192)
        'TLOrder.CellBorderStyle = TableLayoutPanelCellBorderStyle.None
        '  AddHandler TLOrder.CellPaint, AddressOf TLOrder_CellPaint
        Dim myMargin As New Padding(0, 0, 0, 0)
        TLOrder.Margin = myMargin
        TLOrder.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.None

        TLOrder.ColumnCount = 4
        TLOrder.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.5!))
        TLOrder.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.5!))
        TLOrder.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.5!))
        TLOrder.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.5!))
        ' TLOrder.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.0!))

        'lblOrderItemColHdr
        '
        lblOrderItemColHdr = New Spectrum.CtrlLabel

        lblOrderItemColHdr.AttachedTextBoxName = Nothing
        lblOrderItemColHdr.AutoSize = True
        lblOrderItemColHdr.Anchor = AnchorStyles.Left
        lblOrderItemColHdr.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ' lblOrderHeader.Font = FontTableHdr
        lblOrderItemColHdr.BackColor = Color.Transparent
        lblOrderItemColHdr.BorderStyle = System.Windows.Forms.BorderStyle.None
        lblOrderItemColHdr.ForeColor = System.Drawing.Color.Black
        lblOrderItemColHdr.Name = "lblOrderItemColHdr"
        lblOrderItemColHdr.Size = New System.Drawing.Size(36, 15)
        lblOrderItemColHdr.Tag = Nothing
        lblOrderItemColHdr.Text = "ITEM"
        lblOrderItemColHdr.TextDetached = True
        '
        'lblOrderQtyColHdr
        '
        lblOrderQtyColHdr = New Spectrum.CtrlLabel
        lblOrderQtyColHdr.AttachedTextBoxName = Nothing
        lblOrderQtyColHdr.AutoSize = True
        lblOrderQtyColHdr.Anchor = AnchorStyles.Right
        lblOrderQtyColHdr.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblOrderQtyColHdr.BackColor = Color.Transparent
        lblOrderQtyColHdr.BorderStyle = System.Windows.Forms.BorderStyle.None
        lblOrderQtyColHdr.ForeColor = System.Drawing.Color.Black
        lblOrderQtyColHdr.Name = "lblOrderQtyColHdr"
        lblOrderQtyColHdr.Size = New System.Drawing.Size(29, 15)
        lblOrderQtyColHdr.Tag = Nothing
        lblOrderQtyColHdr.Text = "QUANTITY"
        lblOrderQtyColHdr.TextDetached = True

        '
        'CtrlOrderActionColHdr
        '

        lblOrderActionColHdr = New Spectrum.CtrlLabel
        lblOrderActionColHdr.AttachedTextBoxName = Nothing
        lblOrderActionColHdr.AutoSize = True
        lblOrderActionColHdr.Anchor = AnchorStyles.None
        lblOrderActionColHdr.BackColor = Color.Transparent
        lblOrderActionColHdr.BorderStyle = System.Windows.Forms.BorderStyle.None
        lblOrderActionColHdr.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblOrderActionColHdr.ForeColor = System.Drawing.Color.Black
        lblOrderActionColHdr.Name = "lblOrderActionColHdr"
        lblOrderActionColHdr.Size = New System.Drawing.Size(129, 15)
        lblOrderActionColHdr.Tag = Nothing
        lblOrderActionColHdr.Text = "ACTION"
        lblOrderActionColHdr.TextDetached = True

        LblKitArticleHdr = New Spectrum.CtrlLabel
        LblKitArticleHdr.AttachedTextBoxName = Nothing
        LblKitArticleHdr.AutoSize = True
        LblKitArticleHdr.Anchor = AnchorStyles.None
        LblKitArticleHdr.BackColor = Color.Transparent
        LblKitArticleHdr.BorderStyle = System.Windows.Forms.BorderStyle.None
        LblKitArticleHdr.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        LblKitArticleHdr.ForeColor = System.Drawing.Color.Black
        LblKitArticleHdr.Name = "lblOrderActionColHdr"
        LblKitArticleHdr.Size = New System.Drawing.Size(129, 15)
        LblKitArticleHdr.Tag = Nothing
        LblKitArticleHdr.Text = "Ingredients"
        LblKitArticleHdr.TextDetached = True


        TLOrder.RowCount = 1
        TLOrder.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        TLOrder.Controls.Add(lblOrderItemColHdr, 0, 0)
        TLOrder.Controls.Add(lblOrderQtyColHdr, 1, 0)
        TLOrder.Controls.Add(lblOrderActionColHdr, 2, 0)
        TLOrder.Controls.Add(LblKitArticleHdr, 3, 0)
        FLOrder.Controls.Add(TLOrder)
    End Sub


    Private Sub AddPanel(ByRef FLOrder As FlowLayoutPanel, ByVal BillNo As String, ByVal Terminal As String, ByVal OrderTime As DateTime, ByRef dtOrderDtl As DataTable)
        Try
            Dim OrderTitleHdr1 As String = "Order No: " & BillNo & "-" & Terminal
            Dim OrderTitleHdr2 As String = " ( Elapsed Time: {0} )"
            'TLOrder
            '

            TLOrder = New System.Windows.Forms.TableLayoutPanel
            TLOrder.Width = FLOrder.Width - 20
            TLOrder.Name = BillNo
            TLOrder.BackColor = Color.FromArgb(255, 255, 255)
            '  AddHandler TLOrder.CellPaint, AddressOf TLOrder_CellPaint
            '  TLOrder.CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            Dim myMargin As New Padding(0, 0, 0, 0)
            TLOrder.Margin = myMargin
            TLOrder.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset

            TLOrder.ColumnCount = 4
            TLOrder.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.5!))
            TLOrder.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.5!))
            TLOrder.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.5!))
            TLOrder.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.5!))
            '  TLOrder.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.0!))


            lblOrderHeader = New Spectrum.CtrlLabel
            lblOrderHeader.AttachedTextBoxName = Nothing
            lblOrderHeader.AutoSize = True
            lblOrderHeader.Margin = New Padding(0, 0, 0, 0)
            lblOrderHeader.Dock = DockStyle.Fill
            lblOrderHeader.Anchor = AnchorStyles.Left
            lblOrderHeader.TextAlign = ContentAlignment.BottomLeft
            lblOrderHeader.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            lblOrderHeader.BackColor = Color.FromArgb(244, 244, 244)
            lblOrderHeader.BorderStyle = System.Windows.Forms.BorderStyle.None
            lblOrderHeader.ForeColor = Color.FromArgb(25, 44, 59)
            lblOrderHeader.Name = "lblOrderHeader"
            lblOrderHeader.Tag = Nothing
            lblOrderHeader.TextDetached = True
            lblOrderHeader.Text = OrderTitleHdr1 & Space(500)

            If ScreenMode = enumScreenMode.Order Then

                lblOrderHeader2 = New Spectrum.CtrlLabel
                lblOrderHeader2.AttachedTextBoxName = Nothing
                lblOrderHeader2.AutoSize = True
                lblOrderHeader2.Margin = New Padding(0, 0, 0, 0)
                lblOrderHeader2.Dock = DockStyle.Fill
                lblOrderHeader2.Anchor = AnchorStyles.Left
                lblOrderHeader2.TextAlign = ContentAlignment.MiddleLeft
                lblOrderHeader2.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                lblOrderHeader2.BackColor = Color.FromArgb(244, 244, 244)
                TLOrder.SetColumnSpan(lblOrderHeader2, 2)
                lblOrderHeader2.BorderStyle = BorderStyle.None
                lblOrderHeader2.Name = BillNo
                lblOrderHeader2.Size = New System.Drawing.Size(258, 15)
                lblOrderHeader2.Tag = BillNo
                lblOrderHeader2.TextDetached = True
                lblOrderHeader2.Value = OrderTime & "@" & OrderTitleHdr2
                Dim OrderElapsedTimeInSeconds As Long = DateDiff(DateInterval.Second, OrderTime, IIf(LastBillStampTime Is Nothing, DateTime.Now, LastBillStampTime))
                Dim OrderElapsedTimeString As String
                If (OrderElapsedTimeInSeconds Mod 60) > 9 Then
                    OrderElapsedTimeString = Math.Floor(OrderElapsedTimeInSeconds / 60).ToString() & ":" & (OrderElapsedTimeInSeconds Mod 60).ToString()
                Else
                    OrderElapsedTimeString = Math.Floor(OrderElapsedTimeInSeconds / 60).ToString() & ":0" & (OrderElapsedTimeInSeconds Mod 60).ToString()
                End If


                lblOrderHeader2.Text = String.Format(OrderTitleHdr2, OrderElapsedTimeString) & Space(400)
                If OrderElapsedTimeInSeconds > DelayOrderTimeLimitInSec Then
                    lblOrderHeader2.ForeColor = Color.FromArgb(237, 28, 36)
                Else
                    lblOrderHeader2.ForeColor = Color.FromArgb(0, 114, 188)
                End If
            Else
                TLOrder.SetColumnSpan(lblOrderHeader, 3)
            End If

            'Dim fp As New FlowLayoutPanel()
            'fp.FlowDirection = FlowDirection.LeftToRight
            'TLOrder.SetColumnSpan(fp, 4)
            '   fp.AutoSize = True
            '


            TLOrder.RowCount = 1
            TLOrder.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
            TLOrder.Controls.Add(lblOrderHeader, 0, 0)
            If ScreenMode = enumScreenMode.Order Then
                TLOrder.Controls.Add(lblOrderHeader2, 1, 0)
            End If

            'fp.Controls.Add(lblOrderHeader)
            'fp.Controls.Add(lblOrderHeader2)

            '    Dim FlowLayoutPanelHeight As Decimal = 0
            For index = 0 To dtOrderDtl.Rows.Count - 1
                '-lblItemDesc
                lblItemDesc = New Spectrum.CtrlLabel
                lblItemDesc.AttachedTextBoxName = Nothing
                lblItemDesc.AutoSize = True
                lblItemDesc.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                lblItemDesc.BorderStyle = System.Windows.Forms.BorderStyle.None
                lblItemDesc.ForeColor = Color.FromArgb(37, 37, 37)
                lblItemDesc.Name = "itemDesc" & dtOrderDtl.Rows(index)("articleCode")
                lblItemDesc.Size = New System.Drawing.Size(36, 15)
                lblItemDesc.BackColor = Color.Transparent
                lblItemDesc.Anchor = AnchorStyles.Left
                lblItemDesc.Tag = Nothing
                If clsDefaultConfiguration.PrintItemFullName Then
                    lblItemDesc.Text = dtOrderDtl.Rows(index)("ArticleName") & IIf(String.IsNullOrEmpty(dtOrderDtl.Rows(index)("Remark")), "", "(" & dtOrderDtl.Rows(index)("Remark") & ")")
                Else
                    lblItemDesc.Text = dtOrderDtl.Rows(index)("ArticleShortName") & IIf(String.IsNullOrEmpty(dtOrderDtl.Rows(index)("Remark")), "", "(" & dtOrderDtl.Rows(index)("Remark") & ")")
                End If
                lblItemDesc.TextDetached = True

                '--lblItemQty
                lblItemQty = New Spectrum.CtrlLabel
                lblItemQty.AttachedTextBoxName = Nothing
                lblItemQty.AutoSize = True
                lblItemQty.Anchor = AnchorStyles.Right
                lblItemQty.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                lblItemQty.BackColor = Color.Transparent
                lblItemQty.BorderStyle = System.Windows.Forms.BorderStyle.None
                lblItemQty.ForeColor = Color.FromArgb(37, 37, 37)
                lblItemQty.Name = "ItemQty" & dtOrderDtl.Rows(index)("articleCode")
                lblItemQty.Size = New System.Drawing.Size(36, 15)
                lblItemQty.Tag = Nothing
                lblItemQty.Text = dtOrderDtl.Rows(index)("Quantity")
                lblItemQty.TextDetached = True


                'LblKitArticle = New Spectrum.CtrlLabel
                'tipimage.BackColor = Color.White
                'If dtOrderDtl.Rows(index)("KitArticle").ToString.Length >= 210 Then
                '    tipimage.OwnerDraw = True
                '    AddHandler tipimage.Draw, AddressOf toolTip1_Draw
                '    AddHandler tipimage.Popup, AddressOf toolTip1_Popup
                '    tipimage.SetToolTip(LblKitArticle, dtOrderDtl.Rows(index)("KitArticle").ToString)
                'End If
                'LblKitArticle.AttachedTextBoxName = Nothing
                'LblKitArticle.AutoSize = True
                'LblKitArticle.Anchor = AnchorStyles.Right
                'LblKitArticle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                'LblKitArticle.BackColor = Color.Transparent
                'LblKitArticle.BorderStyle = System.Windows.Forms.BorderStyle.None
                'LblKitArticle.ForeColor = Color.FromArgb(37, 37, 37)
                'LblKitArticle.Name = "ItemQty" & dtOrderDtl.Rows(index)("articleCode")
                'LblKitArticle.Size = New System.Drawing.Size(36, 15)
                'LblKitArticle.Tag = Nothing
                'LblKitArticle.Text = dtOrderDtl.Rows(index)("KitArticle").ToString
                'LblKitArticle.TextDetached = True

                btnKitArticle = New System.Windows.Forms.Button
                btnKitArticle.Name = dtOrderDtl.Rows(index)("KitArticle").ToString.Replace(",", ", ")
                btnKitArticle.Dock = DockStyle.Fill
                btnKitArticle.Margin = New Padding(4, 4, 4, 4)
                btnKitArticle.FlatStyle = FlatStyle.Flat
                btnKitArticle.FlatAppearance.BorderSize = 0
                btnKitArticle.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                btnKitArticle.ForeColor = Color.FromArgb(255, 255, 255)
                'btnComplete.Dock = DockStyle.Fill
                AddHandler btnKitArticle.Click, AddressOf BtnKitArticle_Click
                btnKitArticle.Text = "View Ingredient"
                btnKitArticle.BackColor = Color.Blue
                '          Dim dt = dtOrderDtl.Rows(index)("KitArticle").ToString



                btnComplete = New System.Windows.Forms.Button
                btnComplete.Name = dtOrderDtl.Rows(index)("articleCode")
                btnComplete.Dock = DockStyle.Fill
                btnComplete.Margin = New Padding(4, 4, 4, 4)
                btnComplete.FlatStyle = FlatStyle.Flat
                btnComplete.FlatAppearance.BorderSize = 0
                btnComplete.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                btnComplete.ForeColor = Color.FromArgb(255, 255, 255)
                'btnComplete.Dock = DockStyle.Fill
                AddHandler btnComplete.Click, AddressOf Me.complete_Click
                btnComplete.UseVisualStyleBackColor = True
                If ScreenMode = enumScreenMode.Order Then
                    btnComplete.Text = "Complete"
                    btnComplete.BackColor = Color.FromArgb(0, 166, 19)
                Else
                    btnComplete.Text = "Retrive"
                    btnComplete.BackColor = Color.FromArgb(0, 166, 19)
                End If
                TLOrder.RowCount += 1
                TLOrder.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
                TLOrder.Controls.Add(lblItemDesc, 0, index + 1)
                TLOrder.Controls.Add(lblItemQty, 1, index + 1)
                TLOrder.Controls.Add(btnComplete, 2, index + 1)
                TLOrder.Controls.Add(btnKitArticle, 3, index + 1)
            Next

            Dim MarginOrBorderRatio = TLOrder.RowCount / 16
            TLOrder.Height = (TLOrder.RowCount + MarginOrBorderRatio) * 35
            TLOrder.Tag = TLOrder.RowCount
            FLOrder.Controls.Add(TLOrder)

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Sub TLOrder_CellPaint(sender As Object, e As TableLayoutCellPaintEventArgs)
        ' e.Graphics.DrawLine(Pens.Black, e.CellBounds.Location, New Point(e.CellBounds.Right, e.CellBounds.Top))
        Dim panel = TryCast(sender, TableLayoutPanel)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        Dim rectangle = e.CellBounds
        Using pen = New Pen(Brushes.Green, 0.7)
            pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid

            If e.Row = (panel.RowCount - 1) Then
                rectangle.Height -= 1
            End If

            If e.Column = (panel.ColumnCount - 1) Then
                rectangle.Width -= 1
            End If

            e.Graphics.DrawRectangle(pen, rectangle)
        End Using
    End Sub


#Region "Retrive"
    Private Sub BtnScreenMode_Click(sender As Object, e As EventArgs) Handles BtnScreenMode.Click
        Try
            If ScreenMode = enumScreenMode.Order Then
                ScreenMode = enumScreenMode.Retrive
                FLOrderRetrive.Controls.Clear()
                Call AddPanelHeading(FLOrderRetrive)
                Call LoadRetriveOrders()
                FLOrderRetrive.Visible = True
                FLOrders.Visible = False
            Else
                ScreenMode = enumScreenMode.Order
                LastBillStampTime = Nothing
                FLOrders.Controls.Clear()
                Call AddPanelHeading(FLOrders)
                Call loadNewBills()
                FLOrderRetrive.Visible = False
                FLOrders.Visible = True
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub LoadRetriveOrders()
        '---- Load All Bills ...
        Dim DtRetriveBills As DataTable = objKdsData.GetRetriveBills(clsAdmin.SiteCode, clsAdmin.PrepStationID, clsAdmin.DayOpenDate)
        'If DtBillsDtls.Rows.Count > 0 Then
        '    DtBillsDtls.Merge(DtRetriveBills)
        'Else
        '    DtBillsDtls = DtRetriveBills.Copy
        'End If

        If DtRetriveBills IsNot Nothing AndAlso DtRetriveBills.Rows.Count > 0 Then
            Dim dvNewBills As New DataView(DtRetriveBills, "", "", DataViewRowState.CurrentRows)
            Dim DtOrderList As DataTable = dvNewBills.ToTable(True, "BillNo", "TerminalName", "CREATEDON")
            For Each Ordrow As DataRow In DtOrderList.Rows
                Dim DvOrderDtls As New DataView(DtRetriveBills, "BillNo='" & Ordrow("BillNo") & "'", "", DataViewRowState.CurrentRows)
                Call AddPanel(FLOrderRetrive, Ordrow("BillNo"), Ordrow("TerminalName"), Ordrow("CREATEDON"), DvOrderDtls.ToTable())
            Next Ordrow
        End If
    End Sub

#End Region

End Class


#Region "OldCode"

'Private Sub AddHeader(ByVal ArticleName As String, ByVal Qty As String, Optional ByVal Time As String = "", Optional ByVal Action As String = "", Optional ByVal IsHeader As Boolean = False)
'    Try

'        lb = New Label()

'        lb.MaximumSize = New Size(0, 0)
'        lb.AutoSize = True
'        lb.Margin = New Padding(3, 2, 0, 0)
'        lb.Name = "Remark"
'        lb.Text = ArticleName
'        lb.TextAlign = ContentAlignment.TopLeft
'        lb.Dock = DockStyle.Fill
'        lb.BackColor = Color.Aqua


'        pn = New TableLayoutPanel()
'        pn.SuspendLayout()
'        pn.MaximumSize = New Size(0, 0)
'        pn.Margin = New Padding(0)
'        pn.Padding = New Padding(0)
'        pn.Size = New System.Drawing.Size(700, 30)

'        pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
'        pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
'        pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
'        pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))

'        pn.RowCount = 1
'        pn.ColumnCount = 4
'        pn.Controls.Add(lb, 0, 0)

'        pn.SetColumnSpan(lb, 4)

'        pn.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
'        FlwPanel.Controls.Add(pn)

'        pn.ResumeLayout()
'    Catch ex As Exception
'        LogException(ex)
'    End Try
'End Sub

'Dim lb As Label
'Dim lb1 As Label
'Dim lb2 As Label
'Dim lb3 As Label
'Dim lb4 As Label
'Dim pn As TableLayoutPanel
'Dim bt As Button

'Private Sub AddPanel(ByVal OrderHdrDetail As String, ByVal OrderTime As String, ByVal dtOrderDtl As DataTable, Optional ByVal Action As String = "", Optional ByVal IsHeader As Boolean = False, Optional ByVal Time As Double = 0.0)
'    Try

'        lb = New Label()
'        lb.MaximumSize = New Size(0, 0)
'        lb.AutoSize = True
'        lb.Margin = New Padding(3, 2, 0, 5)
'        lb.Name = "OrderHderText"
'        lb.Text = OrderHdrDetail + OrderTime
'        lb.TextAlign = ContentAlignment.TopLeft
'        lb.Dock = DockStyle.Fill
'        lb.Font = New Font(lb.Font, FontStyle.Bold)

'        ' lb.BackColor = Color.SkyBlue

'        If Not dtOrderDtl Is Nothing AndAlso dtOrderDtl.Rows.Count > 0 Then
'            For Each articles In dtOrderDtl.Rows


'                lb1 = New Label()
'                lb1.MaximumSize = New Size(0, 0)
'                lb1.AutoSize = True
'                lb1.Margin = New Padding(3, 2, 0, 0)
'                lb1.Name = "ArticleName"
'                If IsHeader Then
'                    lb1.Text = "Item"
'                    lb1.Font = New Font(lb1.Font, FontStyle.Bold)
'                Else
'                    lb1.Text = articles("Item")
'                End If


'                lb1.TextAlign = ContentAlignment.TopLeft
'                lb1.Dock = DockStyle.None

'                lb2 = New Label()
'                lb2.MaximumSize = New Size(0, 0)
'                lb2.AutoSize = True
'                lb2.Margin = New Padding(3, 2, 0, 0)
'                lb2.Name = "Qty"
'                If IsHeader Then
'                    lb2.Text = "Qty"
'                    lb2.Font = New Font(lb2.Font, FontStyle.Bold)
'                Else
'                    lb2.Text = articles("Quantity")
'                End If

'                lb2.TextAlign = ContentAlignment.TopLeft
'                lb2.Dock = DockStyle.None

'                lb3 = New Label()
'                lb3.MaximumSize = New Size(0, 0)
'                lb3.AutoSize = True
'                lb3.Margin = New Padding(3, 2, 0, 0)
'                lb3.Name = "Time"
'                If IsHeader Then
'                    lb3.Text = "Time"
'                    lb3.Font = New Font(lb3.Font, FontStyle.Bold)
'                Else
'                    lb3.Text = articles("Time")
'                End If

'                lb3.TextAlign = ContentAlignment.TopLeft
'                lb3.Dock = DockStyle.None


'                bt = New Button
'                bt.Anchor = AnchorStyles.Top
'                bt.AutoSize = True
'                bt.MaximumSize = New Size(100, 22)
'                If IsHeader Then
'                    bt.Text = "Action"
'                Else
'                    bt.Text = "Complete"
'                End If

'                bt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'                bt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
'                bt.UseVisualStyleBackColor = True
'                bt.Name = "complete"
'                bt.Tag = "-"
'                bt.UseVisualStyleBackColor = C1.Win.C1Input.VisualStyle.Office2010Blue
'                bt.Dock = DockStyle.Left
'                Time = articles("CheckTime")


'            Next
'        End If


'        'If Not IsHeader = True Then

'        'End If

'        pn = New TableLayoutPanel()
'        pn.SuspendLayout()
'        pn.MaximumSize = New Size(0, 0)
'        pn.Margin = New Padding(0)
'        pn.Padding = New Padding(0)
'        pn.Size = New System.Drawing.Size(650, 50)
'        '  pn.AutoScroll = True
'        pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
'        pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
'        pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
'        pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
'        'pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))

'        pn.RowCount = 1
'        pn.ColumnCount = 4
'        'pn.Controls.Add(lb, 0, 0)

'        If IsHeader = True Then

'            lb4 = New Label()
'            lb4.MaximumSize = New Size(0, 0)
'            lb4.AutoSize = True
'            lb4.Margin = New Padding(3, 2, 0, 0)
'            lb4.Name = "Action"
'            lb4.Text = "Action"
'            lb4.TextAlign = ContentAlignment.TopLeft
'            lb4.Dock = DockStyle.Fill
'            lb4.Font = New Font(lb4.Font, FontStyle.Bold)
'            '  lb4.BackColor = Color.Aqua

'            pn.SetColumnSpan(lb, 4)
'            pn.Controls.Add(lb, 1, 0)
'            pn.Controls.Add(lb1, 1, 1)
'            pn.Controls.Add(lb2, 2, 1)
'            pn.Controls.Add(lb3, 3, 1)
'            pn.Controls.Add(lb4, 4, 1)
'        Else
'            pn.Controls.Add(lb1, 0, 0)
'            pn.Controls.Add(lb2, 1, 0)
'            pn.Controls.Add(lb3, 2, 0)
'            pn.Controls.Add(bt, 3, 0)
'            AddHandler bt.Click, AddressOf Me.complete_Click
'        End If
'        'If Time <> 0 Then
'        '    If Time > 15.0 Then
'        '        pn.BackColor = Color.Red
'        '    End If
'        'End If


'        '  FlwPanel.AutoScroll = True
'        ' FlwPanel.AutoSize = True
'        ' FlwPanel.Size = New System.Drawing.Size(800, 800)
'        'FlwPanel.MaximumSize = New System.Drawing.Size(684, 800)
'        ' FlwPanel.MinimumSize = New System.Drawing.Size(684, 500)

'        pn.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
'        FLOrders.Controls.Add(pn)

'        pn.ResumeLayout()
'    Catch ex As Exception
'        LogException(ex)
'    End Try
'End Sub

#End Region

