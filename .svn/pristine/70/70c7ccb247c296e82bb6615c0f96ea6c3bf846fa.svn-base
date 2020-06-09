Imports SpectrumCommon
Imports SpectrumBL
Public Class frmPCSTRFactoryRemark

    Dim objComn As New clsCommon
#Region "Events"
    Private Sub frmPCSTRFactoryRemark_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call LoadFactoryRemarks()
        '--- Apply Theme Here 
        If clsDefaultConfiguration.ThemeSelect <> "Default" Then
            Select Case clsDefaultConfiguration.ThemeSelect
                Case "Theme 1"
                    Call Themechange()
                Case 2

                Case Else

            End Select
        End If
    End Sub

    Private Sub txt_Keypress(sender As Object, e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub txt_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            txtkeydown = True
            ''change by ketan Auto capital changes
            'If e.KeyCode = Keys.Space Then
            '    sender.Text = CapitalValidation(sender.Text)
            '    sender.SelectionStart = sender.Text.Length
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub
    Private Sub txt_leave(sender As Object, e As EventArgs)
        Try
            'sender.Text = CapitalValidation(sender.Text)
            sender.Text = objComn.CapitalValidationStatement(sender.Text)
            sender.SelectionStart = sender.Text.Length
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

#End Region

#Region "Public Var"
    Dim lblFont As New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Dim lb As Label
    Dim pn As TableLayoutPanel
    Dim txt As TextBox
    Dim txtkeydown As Boolean = False
    Private _DtFactoryRemarks As DataTable
    Public Property DtFactoryRemarks() As DataTable
        Get
            Return _DtFactoryRemarks
        End Get
        Set(ByVal value As DataTable)
            _DtFactoryRemarks = value
        End Set
    End Property



#End Region

#Region "Methods"


    Private Function Themechange()
        Try

            TableLayoutPanel1.BackColor = Color.FromArgb(134, 134, 134)
            TableLayoutPanel2.BackColor = Color.FromArgb(134, 134, 134)
            fpFactoryRemarks.BackColor = Color.FromArgb(134, 134, 134)
            Panel1.BackColor = Color.White
            With btnCancel
                .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                .BackColor = Color.Transparent
                .BackColor = Color.FromArgb(0, 107, 163)
                .ForeColor = Color.FromArgb(255, 255, 255)
                .Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
                .FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
            End With
            With btnSave
                .VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
                .BackColor = Color.Transparent
                .BackColor = Color.FromArgb(0, 107, 163)
                .ForeColor = Color.FromArgb(255, 255, 255)
                .Font = New Font("Neo Sans", 9, FontStyle.Bold)
                .VisualStyle = C1.Win.C1Input.VisualStyle.Custom
                .FlatStyle = FlatStyle.Flat
                .FlatAppearance.BorderSize = 0
                .FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
            End With

        Catch ex As Exception

        End Try
    End Function

    Private Sub LoadFactoryRemarks()
        Try

       
        For Each dr As DataRow In DtFactoryRemarks.Rows
            'Call AddFactory(FacCode:=dr("FactoryCode"), FacName:=dr("FactoryName"),iif( FacRemark:=dr("Remark") is DBNull .Value ,string .Empty ,FacRemark:=dr("Remark") ))
            Call AddFactory(FacCode:=dr("FactoryCode"), FacName:=dr("FactoryName"), FacRemark:=(IIf(dr("Remark") Is DBNull.Value, "", dr("Remark"))))
            'txt.Focus() 
        Next
        'AddHandler txt.KeyDown, AddressOf txt_KeyDown
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Dim FocusforFirstTextbox As Boolean = True
    Private Sub AddFactory(ByVal FacCode As String, ByVal FacName As String, ByVal FacRemark As String)
        Try
            lb = New Label()
            lb.MaximumSize = New Size(0, 0)
            lb.Margin = New Padding(3, 2, 0, 0)
            Dim tip As ToolTip = New ToolTip()
            lb.Name = FacName
            lb.Font = lblFont
            lb.Text = FacName
            lb.Anchor = AnchorStyles.Left
            lb.TextAlign = ContentAlignment.TopLeft
            lb.Dock = DockStyle.Left
            'lb.Anchor = AnchorStyles.Right
            lb.Size = New System.Drawing.Size(230, 20)
            lb.Font = New Font("Calibri", 13, FontStyle.Regular)
            lb.TextAlign = ContentAlignment.MiddleCenter
            txt = New TextBox()
            txt.Text = FacRemark
            txt.Margin = New Padding(3, 0, 0, 0)
            txt.MaxLength = 500
            txt.MinimumSize = New System.Drawing.Size(430, 40)
            txt.Name = FacName
            txt.Size = New System.Drawing.Size(780, 40)
            txt.ScrollBars = ScrollBars.Vertical
            txt.Font = New Font("Verdana", 10, FontStyle.Regular)
            txt.Tag = Nothing
            txt.Dock = DockStyle.Left
            txt.Multiline = True
            'txt.AutoScrollOffset = True
            If FocusforFirstTextbox = True Then
                txt.Select()
                txt.SelectionStart = 0
                FocusforFirstTextbox = False
            End If
            AddHandler txt.KeyPress, AddressOf txt_Keypress
            AddHandler txt.KeyDown, AddressOf txt_KeyDown
            AddHandler txt.Leave, AddressOf txt_leave
            pn = New TableLayoutPanel()
            pn.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single

            'pn.SetCellPosition()
            pn.SuspendLayout()
            pn.MaximumSize = New Size(0, 0)
            pn.Margin = New Padding(0)
            pn.Padding = New Padding(0)
            pn.AutoSize = True
            'pn.Dock = DockStyle.Fill
            pn.Size = New System.Drawing.Size(500, 100)

            'pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
            'pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.0!))
            pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
            pn.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.0!))
            'pn.MinimumSize = New System.Drawing.Size(647, 58)
            pn.MinimumSize = New System.Drawing.Size(950, 126)

            pn.RowCount = 1
            pn.ColumnCount = 2
            pn.Controls.Add(lb, 0, 0)
            pn.Controls.Add(txt, 1, 0)

            pn.Tag = FacCode

            '--- Apply Theme Here 
            If clsDefaultConfiguration.ThemeSelect <> "Default" Then
                Select Case clsDefaultConfiguration.ThemeSelect
                    Case "Theme 1"
                        AddToTheme(lb)
                    Case 2

                    Case Else

                End Select
            End If

            fpFactoryRemarks.Controls.Add(pn)
            fpFactoryRemarks.AutoScroll = True
            fpFactoryRemarks.HorizontalScroll.Enabled = False
            fpFactoryRemarks.HorizontalScroll.Visible = False
            Dim vertScrollWidth = SystemInformation.VerticalScrollBarWidth
            fpFactoryRemarks.Padding = New Padding(0, 0, vertScrollWidth, 0)
            pn.ResumeLayout()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Sub AddToTheme(lbl As Label)
        Dim lblFont As New Font("Neo Sans", 10, FontStyle.Regular)
        With lbl
            .Font = lblFont
            '   .Dock = DockStyle.Top
            .BackColor = Color.FromArgb(212, 212, 212)
            .AutoSize = False
            .TextAlign = ContentAlignment.MiddleLeft
            .BorderStyle = BorderStyle.None
        End With
    End Sub

    'Function CapitalValidation(ByVal str As String) As String
    '    Try
    '        Dim data As String = str
    '        Dim word = data.Split(" ")
    '        Dim temp = word(word.Length - 1)
    '        'Dim text = File.ReadAllText("D:\word.txt")
    '        'text = text.ToUpper()
    '        'Dim result As String() = text.Split(",")

    '        '-------
    '        'If Not temp.Contains(".") Then

    '        If Not temp = String.Empty Then
    '            Dim dtAuto = objComn.GetAutocaptiliseWords()
    '            Dim drHdr() = dtAuto.Select("Word='" & temp & "'")

    '            If drHdr.Count > 0 Then
    '                word(word.Length - 1) = temp
    '                str = String.Join(" ", word)
    '            Else
    '                temp = temp.Substring(0, 1).ToUpper() + temp.Substring(1, temp.Length - 1).ToLower()
    '                word(word.Length - 1) = temp
    '                str = String.Join(" ", word)
    '            End If

    '        End If
    '        'Else
    '        '    temp = temp.Substring(0, 1).ToUpper() + temp.Substring(1, temp.Length - 1).ToUpper()
    '        '    word(word.Length - 1) = temp
    '        '    str = String.Join(" ", word)
    '        'End If
    '        Return str
    '    Catch ex As Exception
    '        LogException(ex)
    '    End Try
    'End Function


#End Region


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
        If txtkeydown = True Then
            If MsgBox(getValueByKey("SO047"), MsgBoxStyle.YesNo, "SO047 - " & getValueByKey("CLAE04")) = MsgBoxResult.Yes Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
                Me.Dispose()
            End If
        Else
            Me.Close()
            Me.Dispose()
        End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            ''Step 1----loop all factories (Controls) ---
            For Each control As Control In Me.fpFactoryRemarks.Controls
                If (TypeOf control Is TableLayoutPanel AndAlso control.Tag IsNot Nothing) Then

                    For Each InnerCtrl As Control In control.Controls
                        If (TypeOf InnerCtrl Is TextBox) Then ' 
                            '-- step 2 Get the factory from dataTable 
                            Dim dr() = DtFactoryRemarks.Select("FactoryCode='" & control.Tag & "'")
                            If dr.Count >= 1 Then
                                'step 3 Update Remark in DataTable from control
                                dr(0)("Remark") = DirectCast(InnerCtrl, TextBox).Text
                            End If
                        End If
                    Next
                End If
            Next
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
     
   

End Class