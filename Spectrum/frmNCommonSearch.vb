Imports SpectrumBL
''' <summary>
''' This form Is Used For Searching
''' </summary>
''' <CreatedBy>Rama Ranjan Jena</CreatedBy>
''' <UpdatedBy></UpdatedBy>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
Public Class frmNCommonSearch
#Region "Global Variable's & Property's"
    Public search() As String
    Public _IsRemarkVisible As Boolean
    ''' <summary>
    ''' Set the data from Outside to Show
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property SetData() As DataTable
        Set(ByVal value As DataTable)
            dtData = value
        End Set
    End Property
    'added by vipul to check from where from get called
    Public Property _RequestFromPage As String
    Public Property RequestFromPage As String
        Get
            Return _RequestFromPage
        End Get
        Set(value As String)
            _RequestFromPage = value
        End Set
    End Property

    Public Property _ArtRemark As String
    Public Property ArtRemark As String
        Get
            Return _ArtRemark
        End Get
        Set(value As String)
            _ArtRemark = value
        End Set
    End Property
    Private Property _altWasPressed As Boolean = False
    Dim xFrmWidth As Integer = 0

#End Region
#Region "Global Variable's for Class"
    Dim dtData As DataTable
#End Region
#Region "Class Events"
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub frmCommonSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If _IsRemarkVisible Then
                dgSearch.Height = 275
                txtReason.Visible = True
                lblReason.Visible = True
            End If
            FillGrid()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ThemeChange()
            End If
            If xFrmWidth > 741 Then
                dgSearch.Width = xFrmWidth
                dgSearch.ExtendRightColumn = True
                xFrmWidth = My.Computer.Screen.WorkingArea.Width - 250 'added by sagar for pc issue list
                Me.Width = xFrmWidth + 44
                GroupBox1.Width = xFrmWidth + 35
                'GroupBox1.Width = 990
            End If
            dgSearch.Focus()
        Catch ex As Exception
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)
    End Sub
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Try
            Dim i As Integer
            If dgSearch.Row = -1 Then Exit Sub
            If txtReason.Visible AndAlso (String.IsNullOrEmpty(txtReason.Text) Or HasAlphaNumericChar(txtReason.Text) = False) Then
                ShowMessage(getValueByKey("frmncommonsearch.txtreasonvalidation"), getValueByKey("CLAE04"))
                Exit Sub
            End If
            If dgSearch.RowCount = 0 Then
                ShowMessage("Please select the record first", getValueByKey("CLAE04"))
                Exit Sub
            End If
            If dgSearch.Row >= 0 Then
                Array.Resize(search, dgSearch.Columns.Count)
                For i = 0 To dgSearch.Columns.Count - 1
                    search(i) = IIf(dgSearch.Item(dgSearch.Row, i) Is System.DBNull.Value, "", dgSearch.Item(dgSearch.Row, i))
                Next
            End If
            '   Me.DialogResult = Windows.Forms.DialogResult.OK
            '  Me.Close()
          If RequestFromPage = enumOperationOnBill.VoidBill Then
                If dgSearch.Row >= 0 Then
                    Dim _strTender As String = search(5).ToString()
                    If _strTender.Contains("Credit Sales") Then
                        Dim dtinfo As New DataTable
                        dtinfo = GetBillTenderInfo(search(0))
                        If dtinfo.Rows.Count > 0 Then
                            ShowMessage("Credit sale has been adjusted for selected bill so you cannot void the bill", "Information")
                            Exit Sub
                        End If
                    End If
                End If
                AfterSelectionBillReasonPopPup_OnVoidBill()
                If ArtRemark <> "" Then
                Else
                    Exit Sub
                End If

            ElseIf RequestFromPage = enumOperationOnBill.EditBill Then
                If dgSearch.Row >= 0 Then
                    Dim _strTender As String = search(5).ToString()
                    If _strTender.Contains("Credit Sales") Then
                        Dim dtinfo As New DataTable
                        dtinfo = GetBillTenderInfo(search(0))
                        If dtinfo.Rows.Count > 0 Then
                            ShowMessage("Credit sale has been adjusted for selected bill so you can't do the tender change", "Information")
                            Exit Sub
                        End If
                    End If
                End If
            End If


            Me.Close()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            ShowMessage(getValueByKey("CS001"), "CS001 - " & getValueByKey("CLAE05"))
            'ShowMessage("Result is not properly intialized", "Information")
        End Try
    End Sub
    Private Sub dgSearch_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgSearch.DoubleClick
        Try
            Dim i As Integer
            If dgSearch.Row = -1 Then Exit Sub
            If txtReason.Visible AndAlso (String.IsNullOrEmpty(txtReason.Text) Or HasAlphaNumericChar(txtReason.Text) = False) Then
                ShowMessage(getValueByKey("frmncommonsearch.txtreasonvalidation"), getValueByKey("CLAE04"))
                Exit Sub
            End If
            If dgSearch.RowCount = 0 Then
                ShowMessage("Please select the record first", getValueByKey("CLAE04"))
                Exit Sub
            End If
            If dgSearch.Row >= 0 Then
                Array.Resize(search, dgSearch.Columns.Count)
                For i = 0 To dgSearch.Columns.Count - 1
                    search(i) = IIf(dgSearch.Item(dgSearch.Row, i) Is System.DBNull.Value, "", dgSearch.Item(dgSearch.Row, i))
                Next
            End If
            If RequestFromPage = enumOperationOnBill.VoidBill Then
                If dgSearch.Row >= 0 Then
                    Dim _strTender As String = search(5).ToString()
                    If _strTender.Contains("Credit Sales") Then
                        Dim dtinfo As New DataTable
                        dtinfo = GetBillTenderInfo(search(0))
                        If dtinfo.Rows.Count > 0 Then
                            ShowMessage("Credit sale has been adjusted for selected bill so you cannot void the bill", "Information")
                            Exit Sub
                        End If
                    End If
                End If
                AfterSelectionBillReasonPopPup_OnVoidBill()
                If ArtRemark <> "" Then
                Else
                    Exit Sub
                End If

            ElseIf RequestFromPage = enumOperationOnBill.EditBill Then
                If dgSearch.Row >= 0 Then
                    Dim _strTender As String = search(5).ToString()
                    If _strTender.Contains("Credit Sales") Then
                        Dim dtinfo As New DataTable
                        dtinfo = GetBillTenderInfo(search(0))
                        If dtinfo.Rows.Count > 0 Then
                            ShowMessage("Credit sale has been adjusted for selected bill so you can't do the tender change", "Information")
                            Exit Sub
                        End If
                    End If
                End If
            End If


            Me.Close()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            ShowMessage(getValueByKey("CS001"), "CS001 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
#End Region
#Region "Private Method's & Functions"
    ''' <summary>
    ''' Attach the Data to Grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillGrid()
        Try
            If Not dtData Is Nothing Then
                Me.dgSearch.RowHeight = 24
                Me.dgSearch.Style.Borders.BorderType = C1.Win.C1TrueDBGrid.BorderTypeEnum.Raised
                Me.dgSearch.Splits(0).ColumnCaptionHeight = 34
                Dim dv As New DataView(dtData, "", "", DataViewRowState.CurrentRows)
                dgSearch.DataSource = dv
                Dim count As Int32 = dv.Count
                lblCount.Text = count

                If (Me.dgSearch.Splits(0).DisplayColumns.Count > 0) Then
                    Me.dgSearch.Splits(0).DisplayColumns(0).AutoSize()
                End If
                For index = 0 To Me.dgSearch.Splits(0).DisplayColumns.Count - 1
                    If dgSearch.Splits(0).DisplayColumns(index).DataColumn.DataType = GetType(DateTime) Then
                        dgSearch.Splits(0).DisplayColumns(index).DataColumn.EnableDateTimeEditor = True
                        dgSearch.Splits(0).DisplayColumns(index).DataColumn.NumberFormat = "g"
                    End If
                    Me.dgSearch.Splits(0).DisplayColumns(index).AutoSize()
                    Me.dgSearch.Splits(0).DisplayColumns(index).Style.WrapText = True
                    xFrmWidth = xFrmWidth + dgSearch.Splits(0).DisplayColumns(index).Width
                Next

            Else
                ShowMessage(getValueByKey("CS002"), "CS002 - " & getValueByKey("CLAE05"))
                'ShowMessage("No Data found", "Information")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function ThemeChange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        cmdOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdOk.BackColor = Color.Transparent
        cmdOk.BackColor = Color.FromArgb(0, 107, 163)
        cmdOk.ForeColor = Color.FromArgb(255, 255, 255)
        cmdOk.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmdOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdOk.FlatStyle = FlatStyle.Flat
        cmdOk.FlatAppearance.BorderSize = 0
        cmdOk.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdOk.Location = New Point(570, 14)
        cmdOk.TextAlign = ContentAlignment.MiddleCenter
        cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdCancel.BackColor = Color.Transparent
        cmdCancel.BackColor = Color.FromArgb(0, 107, 163)
        cmdCancel.ForeColor = Color.FromArgb(255, 255, 255)
        cmdCancel.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        cmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdCancel.TextAlign = ContentAlignment.MiddleCenter
        cmdCancel.FlatStyle = FlatStyle.Flat
        cmdCancel.FlatAppearance.BorderSize = 0
        cmdCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdCancel.Location = New Point(650, 14)
        dgSearch.Font = New Font("Neo Sans", 8, FontStyle.Regular)

        'dgSearch.Splits(0).HeadingStyle.BackColor = Color.FromArgb(177, 227, 253)
        'dgSearch.Splits(0).HighLightRowStyle.BackColor = Color.LightBlue
        'dgSearch.Splits(0).HighLightRowStyle.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        'dgSearch.Splits(0).HighLightRowStyle.BackColor2 = Color.LightBlue
        Me.dgSearch.Splits(0).ColumnCaptionHeight = 40
        dgSearch.Styles(0).BackColor = Color.FromArgb(255, 255, 255)
        dgSearch.Styles(0).Font = New Font("Neo Sans", 8.5, FontStyle.Regular)
        dgSearch.Styles(1).Font = New Font("Neo Sans", 8.5, FontStyle.Bold)
        dgSearch.Styles(1).BackColor = Color.FromArgb(177, 227, 253)
        dgSearch.Splits(0).Style.Font = New Font("Neo Sans", 8.5, FontStyle.Regular)
        dgSearch.Styles(5).Font = New Font("Neo Sans", 8.5, FontStyle.Bold)
        dgSearch.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Custom
        dgSearch.HighLightRowStyle.BackColor = Color.LightBlue
        dgSearch.HighLightRowStyle.ForeColor = Color.WhiteSmoke
        CtrlLabel1.ForeColor = Color.FromArgb(255, 255, 255)
        CtrlLabel1.BorderStyle = BorderStyle.None
        CtrlLabel1.BackColor = Color.Transparent
        CtrlLabel1.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        lblCount.ForeColor = Color.FromArgb(255, 255, 255)
        lblCount.BorderStyle = BorderStyle.None
        lblCount.BackColor = Color.Transparent
        lblCount.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        lblReason.ForeColor = Color.FromArgb(255, 255, 255)
        lblReason.BorderStyle = BorderStyle.None
        lblReason.BackColor = Color.Transparent
        lblReason.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        txtReason.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        GroupBox1.MaximumSize = New Size(985, 50)
        Me.dgSearch.Style.Borders.BorderType = C1.Win.C1TrueDBGrid.BorderTypeEnum.None
    End Function
#End Region

    Private Sub dgSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgSearch.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                cmdOK_Click(cmdOk, New EventArgs)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.MaximizeBox = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub





    Private Sub txtReason_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtReason.KeyUp
        Try
            If e.Alt AndAlso (e.KeyCode = Keys.D0 Or e.KeyCode = Keys.D1 Or e.KeyCode = Keys.D2 _
                       Or e.KeyCode = Keys.D3 Or e.KeyCode = Keys.D4 Or e.KeyCode = Keys.D5 _
                       Or e.KeyCode = Keys.D6 Or e.KeyCode = Keys.D7 Or e.KeyCode = Keys.D8 _
                       Or e.KeyCode = Keys.D9 Or e.KeyCode = Keys.NumPad0 _
                       Or e.KeyCode = Keys.NumPad1 Or e.KeyCode = Keys.NumPad2 Or e.KeyCode = Keys.NumPad3 _
                       Or e.KeyCode = Keys.NumPad4 Or e.KeyCode = Keys.NumPad5 Or e.KeyCode = Keys.NumPad6 _
                       Or e.KeyCode = Keys.NumPad7 Or e.KeyCode = Keys.NumPad8 Or e.KeyCode = Keys.NumPad9) Then

                _altWasPressed = True
                'Else

                '    Dim ax As New ApplicationException(e.Alt.ToString & "/" & e.KeyCode.ToString)
                '    LogException(ax)
                '    If test = True Then
                '        MsgBox("testit")
                '    End If

                '    If e.Alt.ToString = "False" AndAlso e.KeyCode.ToString = "Menu" Then
                '        test = True
                '    End If


            End If
        Catch ex As Exception

        End Try
    End Sub
    Dim test As Boolean = False
    Private Sub txtReason_TextChanged(sender As Object, e As System.EventArgs) Handles txtReason.TextChanged
        Try
            Dim textEnd As String = txtReason.Text.Substring(txtReason.SelectionStart - 1, 1)
            If textEnd = "☺" Or textEnd = "☻" Or textEnd = "♥" Or textEnd = "♦" _
                                        Or textEnd = "♣" Or textEnd = "♠" Or textEnd = "•" Or textEnd = "◘" Or textEnd = "○" Or textEnd = "§" Or textEnd = "╚" Or textEnd = "▲" Or textEnd = "ä" Or textEnd = "╤" Or textEnd = "♀" Then

                _altWasPressed = True
            End If

            If (_altWasPressed) Then
                ' remove the added character
                Dim textBox = CType(sender, RichTextBox)
                Dim caretPos = txtReason.SelectionStart
                If caretPos = 0 Then Exit Sub
                Dim text = txtReason.Text
                Dim textStart = text.Substring(0, caretPos - 1)
                If (caretPos <= text.Length) Then
                    textEnd = text.Substring(caretPos, text.Length - caretPos)
                    txtReason.Text = textStart + textEnd
                    txtReason.SelectionStart = caretPos - 1
                    ' Dim ax As New ApplicationException(text & "/" & textStart & "/" & caretPos & "/" & textEnd & "/" & Me.Text)
                    'LogException(ax)
                    _altWasPressed = False
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmNCommonSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "void-cash-memo.htm")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub AfterSelectionBillReasonPopPup_OnVoidBill()
        Dim objarticleremark As New frmArticlesRemark
        objarticleremark.IsKOTReason = True
        ArtRemark = ""
        If objarticleremark.ShowDialog() = Windows.Forms.DialogResult.OK Then
            '_AuthUserRemarks1 = objarticleremark.AuthUserRemarks
            _ArtRemark = objarticleremark.KOTReasonDetails
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        ElseIf DialogResult = Windows.Forms.DialogResult.Cancel Then
            objarticleremark.Close()

            Exit Sub
        End If
    End Sub
End Class
