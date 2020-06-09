Imports SpectrumBL
Imports System.IO
Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient
Imports SpectrumPrint
Imports SpectrumCommon
Imports System.Text


Public Class frmDineIn


    Dim x As Integer = 0
    Dim y As Integer = 0

    Dim NewOrderOfTable As New Button
    Dim NewLabelOfTable As New Label
    Dim objCM As New clsCashMemo
    Dim dtDinNumber As New DataTable
    Dim dtOrderNumber As New DataTable
    Dim dineInTableColor As Object
    Dim assignTableProperty As Boolean = False
    Dim prev As Object
    Dim current As Object
    Dim isItemMerge As Boolean = False
    Public Shared _IsGenerateBillColor As Integer = 0
    Dim isTableLoad As Boolean = False

    Public Shared _SwitchTable As Boolean = False
    Public Shared Property SwitchTable As Boolean
        Get
            Return _SwitchTable
        End Get
        Set(ByVal value As Boolean)
            _SwitchTable = value
        End Set
    End Property

    Public Shared Property IsGenerateBillColor As Integer
        Get
            Return _IsGenerateBillColor
        End Get
        Set(ByVal value As Integer)
            _IsGenerateBillColor = value
        End Set
    End Property
    Public Shared _DinInBillNo As String
    Public Shared Property DinInBillNo As String
        Get
            Return _DinInBillNo
        End Get
        Set(ByVal value As String)
            _DinInBillNo = value
        End Set
    End Property

    Public Shared _OldDinInBillNo As String
    Public Shared Property OldDinInBillNo As String
        Get
            Return _OldDinInBillNo
        End Get
        Set(ByVal value As String)
            _OldDinInBillNo = value
        End Set
    End Property

    Public Shared _DineInProcess As String
    Public Shared Property DineInProcess As String
        Get
            Return _DineInProcess
        End Get
        Set(ByVal value As String)
            _DineInProcess = value
        End Set
    End Property

    Public Shared _AddToDineInTable As Boolean
    Public Shared Property AddToDineInTable As Boolean
        Get
            Return _AddToDineInTable
        End Get
        Set(ByVal value As Boolean)
            _AddToDineInTable = value
        End Set
    End Property

    Public Shared _DineInNumber As Int16
    Public Shared Property DineInNumber As Int16
        Get
            Return _DineInNumber
        End Get
        Set(ByVal value As Int16)
            _DineInNumber = value
        End Set
    End Property

    Public Shared _OldDineInNumber As Int16
    Public Shared Property OldDineInNumber As Int16
        Get
            Return _OldDineInNumber
        End Get
        Set(ByVal value As Int16)
            _OldDineInNumber = value
        End Set
    End Property

    Private ReadOnly Property ButtonSize As System.Drawing.Size
        Get
            Return New Size(80, 80)
        End Get
    End Property
    Private ReadOnly Property ButtonSize1 As System.Drawing.Size
        Get
            Return New Size(150, 40)
        End Get
    End Property
    Dim NewDineInTable As Button
    Private Sub AddDineInTable(ByVal DineInNumber As Integer)
        Try
            Dim Str As New StringBuilder
            NewDineInTable = New Button
            'New Spectrum.CtrlBtn
            NewDineInTable.Enabled = True
            NewDineInTable.Size = New Size(65, 65)
            NewDineInTable.Name = "btn" & DineInNumber.ToString

            'NewDineInTable.Location = New Point(((DineInNumber * 120) + y), ((5 * 10) + x))
            'If (DineInNumber Mod 4) = 0 Then
            '    x = x + 100
            '    y = y - 480
            'End If
            ' NewDineInTable.Anchor = AnchorStyles.Top
            'NewDineInTable.TabIndex = 1

            Dim drDineInTable As DataRow = dtDinNumber.Rows(DineInNumber - 1)
            'If drDineInTable("Status").ToString() = "True" Then
            '    If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            '        NewDineInTable.BackgroundImage = My.Resources.DineRed
            '        NewDineInTable.BackColor = Color.Red
            '    Else
            '        NewDineInTable.BackgroundImage = My.Resources.red
            '        NewDineInTable.BackColor = Color.Red
            '    End If
            '    'NewDineInTable.BackgroundImage = System.Drawing.Image.FromFile("Product.jpg")
            'Else
            '    If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
            '        NewDineInTable.BackgroundImage = My.Resources.DineGreen
            '        NewDineInTable.BackColor = Color.Green
            '    Else
            '        NewDineInTable.BackgroundImage = My.Resources.green
            '        NewDineInTable.BackColor = Color.Green
            '    End If
            'End If
            If drDineInTable("TableStatus").ToString() = "Reserved" Then
                If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                    NewDineInTable.BackgroundImage = My.Resources.DineRed
                    NewDineInTable.BackColor = Color.Red
                Else
                    NewDineInTable.BackgroundImage = My.Resources.red
                    NewDineInTable.BackColor = Color.Red
                End If
                'NewDineInTable.BackgroundImage = System.Drawing.Image.FromFile("Product.jpg")
            ElseIf drDineInTable("TableStatus").ToString() = "Available" Then
                If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                    NewDineInTable.BackgroundImage = My.Resources.DineGreen
                    NewDineInTable.BackColor = Color.Green
                Else
                    NewDineInTable.BackgroundImage = My.Resources.green
                    NewDineInTable.BackColor = Color.Green
                End If
            End If
            Str.Append(drDineInTable("DineInNumber").ToString())
            'NewDineInTable.Text = "T" & drDineInTable("DineInNumber").ToString()
            NewDineInTable.Text = Str.ToString()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                NewDineInTable.Text = NewDineInTable.Text.ToUpper
                NewDineInTable.Font = New Font("New Sans Intel", 18.0F, FontStyle.Bold)
            Else
                NewDineInTable.Font = New Font("New Sans Intel", 18.0F, FontStyle.Bold)
            End If

            NewDineInTable.ForeColor = Color.White
            ' Use Tag property to store "which button" information
            NewDineInTable.Tag = DineInNumber
            ' Add button click handler
            AddHandler NewDineInTable.Click, AddressOf NewDineInTable_Click
            Me.DineInTablePanel.Controls.Add(NewDineInTable)

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub



    Private Sub frmDineIn_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Try
            IsGenerateBillColor = 0
            lblTableName.Visible = False
            cmdAssign.Visible = False
            'dtDinNumber = objCM.GetTableNumber(clsAdmin.SiteCode, clsAdmin.DayOpenDate)
            'For DineInNumber As Int16 = 1 To dtDinNumber.Rows.Count
            '    AddDineInTable(DineInNumber:=DineInNumber)
            'Next DineInNumber
            dtDinNumber = objCM.GetTableNumber(clsAdmin.SiteCode, clsAdmin.DayOpenDate, clsDefaultConfiguration.LockTime, clsDefaultConfiguration.CustStayTime, IsSwitchTable:=True)
            For DineInNumber As Int16 = 1 To dtDinNumber.Rows.Count
                If Not dtDinNumber.Rows(DineInNumber - 1)("TableStatus") = "Booked" Then
                    AddDineInTable(DineInNumber:=DineInNumber)
                End If
            Next DineInNumber
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Me.Button1.BackgroundImage = Spectrum.My.Resources.Resources.DineOrange
                Themechange()
            Else
                Me.Button1.BackgroundImage = Spectrum.My.Resources.Resources.blue
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(255, 255, 255)
        cmdAssign.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdAssign.TextAlign = ContentAlignment.MiddleCenter
        cmdAssign.BackColor = Color.Transparent
        cmdAssign.BackColor = Color.FromArgb(0, 107, 163)
        cmdAssign.BackColor = Color.FromArgb(102, 102, 255)
        cmdAssign.ForeColor = Color.FromArgb(255, 255, 255)
        cmdAssign.Font = New Font("Neo Sans", 11, FontStyle.Bold)
        cmdAssign.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdAssign.FlatStyle = FlatStyle.Flat
        cmdAssign.FlatAppearance.BorderSize = 0
        cmdAssign.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        Button1.FlatAppearance.BorderSize = 5
        lblTableName.BackColor = Color.FromArgb(229, 229, 229)
        lblTableName.AutoSize = False
        lblTableName.TextAlign = ContentAlignment.MiddleCenter
        lblTableName.Dock = DockStyle.Top
        lblTableName.BorderStyle = BorderStyle.None
        Panel1.BorderStyle = BorderStyle.FixedSingle
        cmdAssign.Location = New Point(100, 47)
        cmdAssign.Size = New Size(140, 37)
        cmdAssign.TextAlign = ContentAlignment.MiddleCenter
        articlePanel.BorderStyle = BorderStyle.FixedSingle
        lblTableName.ForeColor = Color.FromArgb(0, 81, 120)
        lblTableName.Font = New Font("Neo Sans", 13, FontStyle.Bold)
        lblTableName.MinimumSize = New Size(0, 50)
        lblTableName.Size = New Size(520, 50)
    End Function
    Private Sub NewDineInTable_Click(sender As Object, e As EventArgs)

        Try
            OldDinInBillNo = DinInBillNo
            OldDineInNumber = DineInNumber
            DineInNumber = sender.tag
            If (OldDineInNumber = DineInNumber) And isTableLoad Then
                Exit Sub
            End If
            dineInTableColor = sender.backcolor
            '------For Color blue
            If Not prev Is Nothing Then
                If prev.Backcolor = Color.Green Then
                    If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                        prev.BackgroundImage = Spectrum.My.Resources.Resources.DineGreen
                    Else
                        prev.BackgroundImage = Spectrum.My.Resources.Resources.green
                    End If
                ElseIf prev.Backcolor = Color.Red Then
                    If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                        prev.BackgroundImage = Spectrum.My.Resources.Resources.DineRed
                    Else
                        prev.BackgroundImage = Spectrum.My.Resources.Resources.red
                    End If
                End If
                End If
            prev = sender
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                prev.BackgroundImage = Spectrum.My.Resources.Resources.DineOrange
            Else
                prev.BackgroundImage = Spectrum.My.Resources.Resources.blue
            End If
            lblTableName.Visible = True
            cmdAssign.Visible = True
            If AddToDineInTable = True Then
                cmdAssign.Enabled = True
            Else
                cmdAssign.Enabled = False
            End If

            lblTableName.Text = "Table No." & sender.tag

            articlePanel.Controls.Clear()
            dtOrderNumber = objCM.GetOrderTableWise(clsAdmin.SiteCode, clsAdmin.DayOpenDate, DineInNumber)
            If dtOrderNumber.Rows.Count > 0 Then
                For OrderNumber As Int16 = 0 To dtOrderNumber.Rows.Count - 1
                    Dim orderno As String = dtOrderNumber(OrderNumber)("BillNo").ToString()
                    Dim orderstatus = dtOrderNumber(OrderNumber)("orderstatus")
                    Dim totalprice As String = objCM.GetTotalAmountOrderWise(clsAdmin.SiteCode, clsAdmin.DayOpenDate, orderno)
                    AddButton(orderno, FormatNumber(totalprice, 2), orderstatus)
                Next OrderNumber
                isTableLoad = True
            Else
                articlePanel.Controls.Clear()
            End If
            cmdAssign.ForeColor = Color.White
        Catch ex As Exception
            'ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    'Private Sub NewDineInTable_Click(sender As Object, e As EventArgs)

    '    Try

    '        OldDinInBillNo = DinInBillNo
    '        OldDineInNumber = DineInNumber
    '        DineInNumber = sender.tag
    '        Me.DialogResult = Windows.Forms.DialogResult.OK
    '        If Not String.IsNullOrEmpty(OldDinInBillNo) Then
    '            Dim drDinIn() = dtDinNumber.Select("BillNo= '" & DinInBillNo & "'  ")
    '            If drDinIn.Length > 0 Then
    '                OldDineInNumber = drDinIn(0)("DineInNumber").ToString()
    '            End If
    '        End If

    '        If sender.backcolor = Color.Red Then
    '            Dim dr() = dtDinNumber.Select("DineInNumber=" & DineInNumber)
    '            If dr.Length > 0 Then
    '                DinInBillNo = dr(0)("BillNo").ToString()
    '            End If
    '        End If

    '        If sender.backcolor = Color.Green AndAlso Not String.IsNullOrEmpty(DinInBillNo) Then
    '            If MsgBox("You want to switch the table??", MsgBoxStyle.YesNo, getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
    '                DineInProcess = enumDineInProcess.SwitchTable
    '            Else
    '                Me.DialogResult = Windows.Forms.DialogResult.Cancel
    '            End If
    '        ElseIf sender.backcolor = Color.Green Then
    '            DineInProcess = enumDineInProcess.NewHold

    '        ElseIf sender.backcolor = Color.Red AndAlso AddToDineInTable AndAlso Not String.IsNullOrEmpty(OldDinInBillNo) Then
    '            DineInProcess = enumDineInProcess.EditAndLoad

    '        ElseIf sender.backcolor = Color.Red AndAlso AddToDineInTable Then
    '            If MsgBox("There is already an order assigned to this table. Do you want to merge these items to the existing order?", MsgBoxStyle.YesNo, getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
    '                DineInProcess = enumDineInProcess.MergeHold
    '            Else
    '                Me.DialogResult = Windows.Forms.DialogResult.Cancel
    '            End If
    '        Else
    '            DineInProcess = enumDineInProcess.EditHold
    '        End If



    '    Catch ex As Exception
    '        'ShowMessage(ex.Message, getValueByKey("CLAE05"))
    '        LogException(ex)
    '    End Try
    'End Sub

    Private Sub NewOrderOfTable_Click(sender As Object, e As EventArgs, Optional ByVal IsAssign As Boolean = False)
        Try
            If Not IsAssign Then
                If SwitchTable Then Exit Sub
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK

            If Not sender Is Nothing Then
                DinInBillNo = sender.tag
            End If

            If dineInTableColor = Color.Green AndAlso Not String.IsNullOrEmpty(OldDinInBillNo) AndAlso Not isItemMerge Then
                'You want to switch the table??
                If MsgBox(getValueByKey("DIN032"), MsgBoxStyle.YesNo, getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                    DineInProcess = enumDineInProcess.SwitchTable
                Else
                    Me.DialogResult = Windows.Forms.DialogResult.None
                End If
            ElseIf dineInTableColor = Color.Green Then
                DineInProcess = enumDineInProcess.NewHold
                DinInBillNo = ""
            ElseIf dineInTableColor = Color.Red AndAlso assignTableProperty AndAlso Not String.IsNullOrEmpty(OldDinInBillNo) Then
                ' You want to switch the table??
                If MsgBox(getValueByKey("DIN032"), MsgBoxStyle.YesNo, getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                    DineInProcess = enumDineInProcess.SwitchTable
                Else
                    Me.DialogResult = Windows.Forms.DialogResult.None
                End If
            ElseIf dineInTableColor = Color.Red AndAlso assignTableProperty Then
                DineInProcess = enumDineInProcess.NewHold
            ElseIf dineInTableColor = Color.Red AndAlso AddToDineInTable AndAlso Not String.IsNullOrEmpty(OldDinInBillNo) AndAlso Not isItemMerge Then
                DineInProcess = enumDineInProcess.EditAndLoad
            ElseIf dineInTableColor = Color.Red AndAlso AddToDineInTable Then
                'Do you want to assign items in this order? 
                If MsgBox(getValueByKey("DIN033"), MsgBoxStyle.YesNo, getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                    DineInProcess = enumDineInProcess.MergeHold
                    IsGenerateBillColor = 2
                Else
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    isItemMerge = True
                End If
            Else
                DineInProcess = enumDineInProcess.EditHold
            End If


        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    'Private Sub CtrlClose_Click(sender As System.Object, e As System.EventArgs)
    '    Me.DialogResult = Windows.Forms.DialogResult.Cancel
    '    Me.Close()
    'End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdAssign_Click(sender As System.Object, e As System.EventArgs) Handles cmdAssign.Click
        Try

            Dim TableNo = objCM.GetTableNum(clsAdmin.SiteCode, clsAdmin.DayOpenDate, OldDinInBillNo)
            If TableNo = DineInNumber Then
                'ShowMessage("The order is already assigned to this table", getValueByKey("CLAE04"))
                ShowMessage(getValueByKey("DIN034"), getValueByKey("CLAE04"))
                Exit Sub
            End If
            assignTableProperty = True
            NewOrderOfTable_Click(Nothing, Nothing, True)
            cmdAssign.Enabled = True
            assignTableProperty = False
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Dim btnFont As New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Dim btnLoc As New System.Drawing.Point(3, 3)
    Dim btnPadding As New Padding(3, 3, 0, 0)
    Dim lblFont As New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Dim lb As Label
    Dim pn As TableLayoutPanel
    Dim bt As Button
    Private Sub AddButton(ByVal orderno As String, ByVal orderPrice As String, Optional ByVal orderstatus As Integer = 0)
        Try
            bt = New Button
            '  bt.SuspendLayout()
            bt.Font = btnFont
            bt.ImageAlign = System.Drawing.ContentAlignment.TopCenter
            bt.Location = btnLoc
            bt.Name = "CtrlBtnGoods"
            bt.Anchor = AnchorStyles.Top
            bt.AutoSize = True
            bt.TabIndex = 1
            bt.Tag = orderno
            bt.Text = orderno
            bt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            bt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
            bt.UseVisualStyleBackColor = True
            bt.UseVisualStyleBackColor = C1.Win.C1Input.VisualStyle.Office2010Blue
            bt.Dock = DockStyle.Fill
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                bt.BackColor = Color.Orange
                bt.Font = New Font("Tahoma", 10, FontStyle.Bold)
                bt.ForeColor = Color.White
                bt.AutoSize = False
                bt.Anchor = AnchorStyles.Top
                bt.MaximumSize = New Size(140, 50)
                bt.Dock = DockStyle.None
                bt.Size = New Size(140, 35)
                bt.TextImageRelation = TextImageRelation.Overlay
            End If
            If orderstatus = 1 Then
                'bt.Image = Spectrum.My.Resources.Resources.red
                If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                    bt.BackColor = Color.FromArgb(76, 153, 0)
                Else
                    bt.BackColor = Color.Yellow
                End If

            ElseIf orderstatus = 2 Then
                If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                    bt.BackColor = Color.FromArgb(152, 152, 152)
                Else
                    bt.BackColor = Color.Cyan
                End If

            End If
            bt.Margin = btnPadding
            AddHandler bt.Click, AddressOf NewOrderOfTable_Click
            lb = New Label()
            '  lb.SuspendLayout()

            lb.MaximumSize = New Size(ButtonSize.Width + 100, 0)
            'lb.Size = New Size(ButtonSize.Width + 20, 30)
            lb.AutoSize = True
            lb.Margin = New Padding(3, 0, 0, 0)
            'lb.AutoSize = True
            lb.Text = clsAdmin.CurrencyCode & " " & UCase(orderPrice)
            Dim tip As ToolTip = New ToolTip()
            tip.SetToolTip(lb, orderPrice)
            lb.Name = "CtrlBtnGoods"
            lb.ForeColor = Color.DarkBlue
            lb.Font = lblFont

            lb.TextAlign = ContentAlignment.MiddleCenter
            lb.Dock = DockStyle.Right
            'lb.Anchor = AnchorStyles.Right



            pn = New TableLayoutPanel()
            pn.SuspendLayout()
            pn.Margin = New Padding(0)
            pn.Padding = New Padding(0)
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                pn.Size = New System.Drawing.Size(338, 70)
                lb.Font = New Font("Neo Sans", 12.5, FontStyle.Bold)
                lb.ForeColor = Color.Black
                lb.Dock = DockStyle.Fill
                lb.Size = New Size(300, 30)
                lb.MaximumSize = New Size(338, 30)
                lb.TextAlign = ContentAlignment.MiddleCenter
                lb.AutoSize = False
            Else
                pn.Size = New System.Drawing.Size(320, 50)
            End If

            pn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                pn.RowCount = 2
                pn.ColumnCount = 1
                pn.Controls.Add(bt, 0, 1)
                pn.Controls.Add(lb, 0, 0)
            Else
                pn.RowCount = 1
                pn.ColumnCount = 2
                pn.Controls.Add(bt, 0, 0)
                pn.Controls.Add(lb, 1, 0)
            End If
            pn.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            pn.AutoSize = False
            articlePanel.Controls.Add(pn)
          
            pn.ResumeLayout()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
   
   
End Class