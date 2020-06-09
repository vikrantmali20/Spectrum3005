Imports SpectrumBL
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Text
Imports C1.Win.C1FlexGrid
Imports System.Globalization




Public Class frmOrderPackageScreen
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ScreenTimer.Start()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
#Region "------- Properties"
    Public Shared _SlsOrderNo As String = ""
    Public Shared Property SlsOrderNo() As String
        Get
            Return _SlsOrderNo
        End Get
        Set(value As String)
            _SlsOrderNo = value
        End Set
    End Property
#End Region
#Region "-------- Variables"
    Dim objorder As New clsCommon()
    Dim objSearch As New frmNCommonSearch
    Dim dt As New DataTable()
    Private WithEvents ScreenTimer As New Timer()
    Dim RefreshSeconds As Date = DateTime.Now
    Dim PagingSeconds As Date = DateTime.Now
    Dim IsFilterchangeFromCode As Boolean = False
    Dim TotalBalAmt As String = "ASC"

#End Region
#Region "--------   Methods"
    Public Sub gridsetting()
        Try


            Me.dgorderSearch.Splits(0).DisplayColumns(12).Visible = False
            Me.dgorderSearch.Splits(0).DisplayColumns(13).Visible = False

            dgorderSearch.Dock = DockStyle.Fill
            dgorderSearch.HeadingStyle.ForeColor = Color.Black
            dgorderSearch.HeadingStyle.BackColor = Color.DarkGray



            ' Me.dgorderSearch.Splits(0).DisplayColumns(11).Width = 80
            Me.dgorderSearch.Splits(0).DisplayColumns(0).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
            Me.dgorderSearch.Splits(0).DisplayColumns(1).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
            Me.dgorderSearch.Splits(0).DisplayColumns(2).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
            Me.dgorderSearch.Splits(0).DisplayColumns(3).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
            Me.dgorderSearch.Splits(0).DisplayColumns(4).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
            Me.dgorderSearch.Splits(0).DisplayColumns(5).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
            Me.dgorderSearch.Splits(0).DisplayColumns(6).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
            Me.dgorderSearch.Splits(0).DisplayColumns(7).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
            Me.dgorderSearch.Splits(0).DisplayColumns(8).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
            Me.dgorderSearch.Splits(0).DisplayColumns(9).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
            Me.dgorderSearch.Splits(0).DisplayColumns(10).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
            Me.dgorderSearch.Splits(0).DisplayColumns(11).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

            Me.dgorderSearch.Splits(0).DisplayColumns(11).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Far
            Me.dgorderSearch.Splits(0).DisplayColumns(9).Style.BackColor = Color.Black
            Me.dgorderSearch.Splits(0).DisplayColumns(9).Style.ForeColor = Color.DeepSkyBlue
            '  Me.dgorderSearch.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder
            dgorderSearch.BackColor = Color.AliceBlue
            Me.dgorderSearch.Columns(0).Caption = "Sr. No"
            Me.dgorderSearch.Columns(1).Caption = "Customer Name"
            Me.dgorderSearch.Columns(2).Caption = "Company Name"
            Me.dgorderSearch.Columns(3).Caption = "Sales Order Number"
            Me.dgorderSearch.Columns(4).Caption = "Factory-Snacks (STR Details)"
            Me.dgorderSearch.Columns(5).Caption = "Factory-Sweets (STR Details)"
            Me.dgorderSearch.Columns(6).Caption = "WareHouse-DF Namkeen (STR Details)"
            Me.dgorderSearch.Columns(7).Caption = "Delivery Type"
            Me.dgorderSearch.Columns(8).Caption = "Multi Delivery"
            Me.dgorderSearch.Columns(9).Caption = "Delivery Date and Time"
            Me.dgorderSearch.Columns(10).Caption = "Total SO Amt"
            Me.dgorderSearch.Columns(11).Caption = "Balance"
            For Each col In dgorderSearch.Columns
                col.FilterEscape = ""

                dgorderSearch.HeadingStyle.WrapText = True
                dgorderSearch.HeadingStyle.Locked = True

            Next

            Dim dd = dgorderSearch.Columns.Count - 2
            TableLayoutPanel1.AutoSize = False
            Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
            Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
            TableLayoutPanel1.Width = screenWidth
            TableLayoutPanel1.Height = screenHeight
            TableLayoutPanel4.Width = screenWidth
            TableLayoutPanel4.Height = screenHeight
            TableLayoutPanel2.Width = screenWidth - 35 'vipin
            Dim Gridwid = screenWidth

            dgorderSearch.Width = Gridwid
            dgorderSearch.Height = screenHeight - 160
            Dim pass = Gridwid / (dd + 0.9)
            ' Me.dgorderSearch.FilterBar = True
            Dim Count = dgorderSearch.Columns.Count - 2

            '      dgorderSearch.Style.Borders.BorderType = C1.Win.C1TrueDBGrid.BorderTypeEnum.None
            For index = 0 To Count - 1
                If index = 0 Then
                    dgorderSearch.Splits(0).DisplayColumns(index).Width = pass - 60
                ElseIf index = 3 Then
                    dgorderSearch.Splits(0).DisplayColumns(index).Width = pass + 30
                ElseIf index = 4 Then
                    dgorderSearch.Splits(0).DisplayColumns(index).Width = pass + 38

                ElseIf index = 5 Then
                    dgorderSearch.Splits(0).DisplayColumns(index).Width = pass + 38
                ElseIf index = 6 Then
                    dgorderSearch.Splits(0).DisplayColumns(index).Width = pass + 38
                ElseIf index = 7 Then
                    dgorderSearch.Splits(0).DisplayColumns(index).Width = pass - 30
                ElseIf index = 8 Then
                    dgorderSearch.Splits(0).DisplayColumns(index).Width = pass - 30
                ElseIf index = 11 Then
                    dgorderSearch.Splits(0).DisplayColumns(index).Width = pass - 10
                ElseIf index = 9 Then
                    dgorderSearch.Splits(0).DisplayColumns(index).Width = pass + 10
                Else
                    dgorderSearch.Splits(0).DisplayColumns(index).Width = pass
                End If

                Me.dgorderSearch.Splits(0).DisplayColumns(index).Style.WrapText = True
            Next
            Me.dgorderSearch.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows
            dgorderSearch.Style.Padding.Top = 5
            dgorderSearch.RowHeight = 61
            '    Me.dgorderSearch.Columns("Balance").NumberFormat = "Standard"
            '   Me.dgorderSearch.Columns("Total SO Amt").NumberFormat = "Standard"
            Me.dgorderSearch.Splits(0).DisplayColumns("Total SO Amt").Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
            Me.dgorderSearch.Splits(0).DisplayColumns("Balance").Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
            Me.dgorderSearch.Splits(0).DisplayColumns(0).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center

            Dim C As C1.Win.C1TrueDBGrid.C1DisplayColumn
            For Each C In Me.dgorderSearch.Splits(0).DisplayColumns
                C.ColumnDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None
            Next

            dgorderSearch.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Raised
            dgorderSearch.RowDivider.Color = Color.Black


            With Me.dgorderSearch.Splits(0).DisplayColumns(9).Style.Borders
                .Color = Color.FromArgb(70, 70, 70)
                .BorderType = C1.Win.C1TrueDBGrid.BorderTypeEnum.Flat
                .Bottom = 2
                .Left = 2
                .Right = 2
                .Top = 2
            End With
            '  Me.dgorderSearch.Splits(0).DisplayColumns(9).Style.Font = New Font("Ariral", 15)

            Me.dgorderSearch.Splits(0).ColumnCaptionHeight = 50
            If screenWidth <= 1366 Then
                lblSiteName.Font = New Font("Verdana", CInt((screenWidth * 1.09) / 100), FontStyle.Bold)
            End If

            dgorderSearch.Splits(0).DisplayColumns(9).DataColumn.EnableDateTimeEditor = True
            dgorderSearch.Splits(0).DisplayColumns(9).DataColumn.NumberFormat = "g"
            dgorderSearch.Splits(0).DisplayColumns(9).Style.WrapText = True

            '  dgorderSearch.Columns(1).NumberFormat = "FormatTextEvent"
            '  dgorderSearch.Splits(0).DisplayColumns(10).DataColumn.NumberFormat = "Scientific"
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    'added by khusrao adil on 18-09-2017 
    Public Sub GetOrderPackaingScreenDetails()
        Try

            dgorderSearch.FilterBar = True
            dgorderSearch.FilterActive = True


            dt = objorder.getPackageScreen(clsAdmin.SiteCode, clsAdmin.DayOpenDate.Date)
            dgorderSearch.DataSource = dt
            gridsetting()
       
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
#End Region
#Region "--------    events"
    Dim DescCol As C1.Win.C1TrueDBGrid.C1DataColumn
    Private Sub frmOrderPackageScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.BackColor = Color.Black
        lblCurrentDateValue.Text = Date.Today.ToString("dd-MMMM-yyyy")
        lblCurrentTimeValue.Text = DateTime.Now.ToString("HH:mm")

        dgorderSearch.Splits(0).DisplayColumns(9).DataColumn.DataType = GetType(DateTime)
        dgorderSearch.Splits(0).DisplayColumns(3).DataColumn.DataType = GetType(String)
        'dgorderSearch.Splits(0).DisplayColumns(10).DataColumn.DataType = GetType(Integer)
        GetOrderPackaingScreenDetails()

        Dim sitename As String = objorder.GetSiteName(clsAdmin.SiteCode)
        Dim PackagingCount As Integer = 0
        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
            PackagingCount = dt.Rows.Count
        End If
        lblSiteName.Text = sitename + " : SALES ORDER DETAILS (" + PackagingCount.ToString.Trim + " Rows)"
        Me.dgorderSearch.CellTips = C1.Win.C1TrueDBGrid.CellTipEnum.Floating
        Me.Dock = DockStyle.Fill 'vipin 
    End Sub
    Private Sub dgorderSearch_DoubleClick_1(sender As Object, e As EventArgs) Handles dgorderSearch.DoubleClick
        If dgorderSearch.DestinationRow.ToString <> "-1" Then
            If dgorderSearch.RowCount > 0 Then
                _SlsOrderNo = dgorderSearch.Item(dgorderSearch.Row, "Sales Order Number")
                Dim ChildForm As New Spectrum.frmPCNSalesOrderUpdate
                ChildForm.SalesORderNumberFromOrderPackagingScreen = _SlsOrderNo
                ChildForm.EditCallFromOrderPackagingScreen = True

                Try
                    If ChildForm.Name <> String.Empty Then
                        ChildForm.IsBookingEdit = True
                        MDISpectrum.ShowChildForm(ChildForm, True)
                    End If
                Catch ex As Exception
                    ChildForm.Close()
                End Try
            End If
        End If
    End Sub
    Private Sub ScreenTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScreenTimer.Tick
        lblCurrentTimeValue.Text = DateTime.Now.ToString("HH:mm:ss")
        If (CInt(dgorderSearch.FirstRow + 10) >= dgorderSearch.RowCount) Then
            If clsDefaultConfiguration.OrderPackagingScreenScrollTimeInterval = DateDiff(DateInterval.Second, PagingSeconds, DateTime.Now) Then
                RefreshSeconds = DateTime.Now()
                PagingSeconds = DateTime.Now()
                GetOrderPackaingScreenDetails()
            End If
        End If

        If clsDefaultConfiguration.OrderPackagingScreenScrollTimeInterval = DateDiff(DateInterval.Second, PagingSeconds, DateTime.Now) Then
            PagingSeconds = DateTime.Now()
            '  If CInt(dgorderSearch.FirstRow + 9) > dgorderSearch.RowCount Then
            dgorderSearch.FirstRow = dgorderSearch.FirstRow + 9
            '  End If
        End If

    End Sub
    Private Sub dgorderSearch_FilterChange(sender As Object, e As EventArgs) Handles dgorderSearch.FilterChange
        Try

            dgorderSearch.AllowFilter = False
            Dim sb As New System.Text.StringBuilder()
            Dim dc As C1.Win.C1TrueDBGrid.C1DataColumn
            For Each dc In Me.dgorderSearch.Columns
                If dc.FilterText.Length > 0 Then
                    If sb.Length > 0 Then
                        sb.Append(" AND ")
                    End If
                    If dc.DataField = "DeliveryDateandTime" Then
                        sb.Append("Convert( DeliveryDateandTime, 'System.String') LIKE '%" & dc.FilterText & "%'")
                    ElseIf dc.DataField = "SrNo" Then
                        sb.Append("Convert( SrNo, 'System.String') LIKE '%" & dc.FilterText & "%'")
                    ElseIf dc.DataField = "TotalSOAmt" Then
                        sb.Append("Convert( TotalSOAmt, 'System.String') LIKE '%" & dc.FilterText & "%'")
                    ElseIf dc.DataField = "TotalSOAmt1" Then
                        sb.Append("Convert( TotalSOAmt1, 'System.String') LIKE '%" & dc.FilterText & "%'")
                    Else
                        sb.Append((dc.DataField + " like " + "'%" + dc.FilterText + "%'"))
                    End If
                End If
            Next dc
            Me.dt.DefaultView.RowFilter = sb.ToString()
            '   Me.dt.DefaultView.RowFilter = "Convert( DeliveryDateandTime, 'System.String') LIKE '%" & dc.FilterText & "%'"
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

#End Region

    Private Sub dgorderSearch_FetchCellTips(sender As System.Object, e As C1.Win.C1TrueDBGrid.FetchCellTipsEventArgs) Handles dgorderSearch.FetchCellTips
        DescCol = Me.dgorderSearch.Columns(e.ColIndex)
        Dim aString As String = Replace(DescCol.CellText(e.Row), "              ", " ")
        If Not aString Is Nothing Then
            aString = Replace(aString.ToString.Trim(), ", ", "" & vbCrLf)
            e.TipStyle.Font = New Font(e.TipStyle.Font.FontFamily, 20)
            e.CellTip = aString

        End If
    End Sub
    Private Sub lblCurrentDate_Click(sender As Object, e As EventArgs) Handles lblCurrentDate.Click

    End Sub

    Private Sub lblCurrentTimeValue_Click(sender As Object, e As EventArgs) Handles lblCurrentTimeValue.Click

    End Sub


    Private Sub TableLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub
    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        Me.Close()
    End Sub


    Private Sub dgorderSearch_AfterSort(sender As Object, e As C1.Win.C1TrueDBGrid.FilterEventArgs) Handles dgorderSearch.AfterSort
        dgorderSearch.AllowSort = True
    End Sub

    Private Sub dgorderSearch_HeadClick(sender As Object, e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgorderSearch.HeadClick
        dgorderSearch.AllowSort = True
        Dim dc As C1.Win.C1TrueDBGrid.C1DisplayColumn = Me.dgorderSearch.Splits(0).DisplayColumns(e.ColIndex)
        Dim Tem123 As DataTable
        Dim dataView As New DataView(dt)
        If dc.ToString.Trim = "Total SO Amt" Then
            dgorderSearch.AllowSort = False
            If TotalBalAmt = "ASC" Then
                dataView.Sort = "TotalSoamtTemp"
                TotalBalAmt = "DESC"
            ElseIf TotalBalAmt = "DESC" Then
                dataView.Sort = "TotalSoamtTemp DESC"
                TotalBalAmt = "ASC"
            End If
            dt = dataView.ToTable()
            dgorderSearch.DataSource = dt
            gridsetting()
        ElseIf dc.ToString.Trim = "Balance" Then
            dgorderSearch.AllowSort = False
            If TotalBalAmt = "ASC" Then
                dataView.Sort = "BalanceTemp"
                TotalBalAmt = "DESC"
            ElseIf TotalBalAmt = "DESC" Then
                dataView.Sort = "BalanceTemp DESC"
                TotalBalAmt = "ASC"
            End If
            dt = dataView.ToTable()
            dgorderSearch.DataSource = dt
            gridsetting()
        Else
            dgorderSearch.AllowSort = True
        End If


    End Sub

    Private Sub dgorderSearch_Filter(sender As Object, e As C1.Win.C1TrueDBGrid.FilterEventArgs) Handles dgorderSearch.Filter

    End Sub

    Private Sub dgorderSearch_FilterButtonClick(sender As Object, e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles dgorderSearch.FilterButtonClick

    End Sub

    Private Sub dgorderSearch_Click(sender As Object, e As EventArgs) Handles dgorderSearch.Click
    End Sub
    Private Sub c1TrueDBGrid1_FormatText(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.FormatTextEventArgs) Handles dgorderSearch.FormatText

    End Sub

    Private Sub dgorderSearch_Sort(ByVal sender As Object, e As C1.Win.C1TrueDBGrid.FilterEventArgs) Handles dgorderSearch.Sort

    End Sub

    Private Sub dgorderSearch_FetchCellStyle(sender As Object, e As C1.Win.C1TrueDBGrid.FetchCellStyleEventArgs) Handles dgorderSearch.FetchCellStyle

    End Sub

    Private Sub frmOrderPackageScreen_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If
    End Sub
End Class