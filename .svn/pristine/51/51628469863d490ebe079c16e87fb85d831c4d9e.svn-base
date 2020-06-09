Imports System.Globalization
Imports SpectrumCommon
Imports SpectrumBL
Public Class frmReportFilter
    Dim opendate As Date
    Private _CallFromDirectMenu As Boolean
    Public Property CallFromDirectMenu() As Boolean
        Get
            Return _CallFromDirectMenu
        End Get
        Set(ByVal value As Boolean)
            _CallFromDirectMenu = value
        End Set
    End Property
    Public Shared _selectedReport As String
    Public Property selectedReport() As String
        Get
            Return _selectedReport
        End Get
        Set(ByVal value As String)
            _selectedReport = value
        End Set
    End Property
    Public _selectedmonth As String
    Public Property selectedmonth() As String
        Get
            Return _selectedmonth
        End Get
        Set(ByVal value As String)
            _selectedmonth = value
        End Set
    End Property
    Public _selectedyear As String
    Public Property selectedyear() As String
        Get
            Return _selectedyear
        End Get
        Set(ByVal value As String)
            _selectedyear = value
        End Set
    End Property
    Public _selectedPromotions As String
    Public Property SelectedPromotions() As String
        Get
            Return _selectedPromotions
        End Get
        Set(ByVal value As String)
            _selectedPromotions = value
        End Set
    End Property
    Public _selectedClassify As String
    Public Property SelectedClassify() As String
        Get
            Return _selectedClassify
        End Get
        Set(ByVal value As String)
            _selectedClassify = value
        End Set
    End Property
    'added by khusrao adil on 29-11-2017 for jk sprint 32
    Public _selectedTerminals As String
    Public Property SelectedTerminals() As String
        Get
            Return _selectedTerminals
        End Get
        Set(ByVal value As String)
            _selectedTerminals = value
        End Set
    End Property
    Public _selectedPartner As String
    Public Property SelectedPartner() As String
        Get
            Return _selectedPartner
        End Get
        Set(ByVal value As String)
            _selectedPartner = value
        End Set
    End Property
    Public _selectedTender As String
    Public Property SelectedTender() As String
        Get
            Return _selectedTender
        End Get
        Set(ByVal value As String)
            _selectedTender = value
        End Set
    End Property
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim objReportBase As New ReportBase
            If selectedReport = "DayCloseReport" Then
                Me.Cursor = Cursors.WaitCursor
                If (dtToDate.Value) <> Nothing Then
                    If (dtToDate.Value > opendate) Then
                        dtToDate.Value = opendate
                    ElseIf (objReportBase.GetOpenDate(clsAdmin.SiteCode, dtToDate.Value, clsDefaultConfiguration.ClientForMail) <> Nothing) Then
                        Me.Cursor = Cursors.Default
                    Else
                        ShowMessage("Day close report not found as Day Open/ Close activity was not performed for " & dtToDate.Text.ToString(), getValueByKey("CLAE04"))
                        'dtToDate.Value = opendate
                    End If
                End If
                Me.Cursor = Cursors.Default
            Else
                If txtTimeSpan.Visible Then
                    Dim _timeSpan As Integer
                    If String.IsNullOrEmpty(txtTimeSpan.Text) OrElse Int32.TryParse(txtTimeSpan.Text, _timeSpan) = False OrElse _timeSpan <= 0 OrElse _timeSpan > 24 Then
                        ShowMessage("Time span can be between 1 to 24 and it can not be in decimals", getValueByKey("CLAE04"))
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                End If
            End If
            If selectedReport = "JKOftheDayPayoutReport" Then
                Dim PromotionNamesList As String = ""
                For Each itemChecked In chkListPromotions.CheckedItems
                    PromotionNamesList &= "'" + itemChecked.ToString.Trim() & "',"
                Next
                PromotionNamesList = PromotionNamesList.Substring(0, PromotionNamesList.Length - 1)
                Dim dtPromotionId As New DataTable
                dtPromotionId = objReportBase.GetSitePromotions(clsAdmin.SiteCode, PromotionNamesList)
                For Each row In dtPromotionId.Rows
                    SelectedPromotions &= row("PromotionNo").ToString.Trim() & ","
                Next
                SelectedPromotions = SelectedPromotions.Substring(0, SelectedPromotions.Length - 1)
                'SelectedPromotions = "'" + SelectedPromotions + "'"
            Else
                SelectedPromotions = ""
            End If
            If selectedReport = "DeliveryPartnerWiseSalesReport" Then
                Dim PartnerNamesList As String = ""
                For Each itemChecked In chkListPromotions.CheckedItems
                    PartnerNamesList &= "'" + itemChecked.ToString.Trim() & "',"
                Next
                If PartnerNamesList = "" Then
                Else
                    PartnerNamesList = PartnerNamesList.Substring(0, PartnerNamesList.Length - 1)
                End If

                Dim dtPartner As New DataTable
                dtPartner = objReportBase.getpartner(clsAdmin.SiteCode, PartnerNamesList)
                For Each row In dtPartner.Rows
                    SelectedPartner &= row("DelieveryPartnerId").ToString.Trim() & ","
                Next
                SelectedPartner = SelectedPartner.Substring(0, SelectedPartner.Length - 1)
            Else
                SelectedPartner = ""
            End If

            'vipul 06-09-2018 tenderwisecommisionreport
            If selectedReport = "TenderWiseCommisionReport" Then
                Dim PartnerNamesList As String = ""
                For Each itemChecked In chkListPromotions.CheckedItems
                    PartnerNamesList &= "'" + itemChecked.ToString.Trim() & "',"
                Next
                If PartnerNamesList = "" Then
                Else
                    PartnerNamesList = PartnerNamesList.Substring(0, PartnerNamesList.Length - 1)
                End If

                Dim dtTender As New DataTable
                dtTender = objReportBase.GetTender(clsAdmin.SiteCode, PartnerNamesList)
                For Each row In dtTender.Rows
                    SelectedTender &= row("TenderType").ToString.Trim() & ","
                Next
                SelectedTender = SelectedTender.Substring(0, SelectedTender.Length - 1)
            Else
                SelectedTender = ""
            End If
            'If selectedReport = "JKProductMixReportTillWise" Then  ' added by khusrao adil on 29-11-2017 for jk sprint 32
            '    'SelectedTerminals = ""
            '    'For Each itemChecked In chkListTerminals.CheckedItems
            '    '    SelectedTerminals &= "" + itemChecked.ToString.Trim() & ","
            '    'Next
            '    'If SelectedTerminals = "" Then
            '    '    ShowMessage("please select atleast one terminal", "Information")
            '    '    Me.Cursor = Cursors.Default
            '    '    Exit Sub
            '    '    Me.Close()
            '    'End If
            '    'SelectedTerminals = SelectedTerminals.Substring(0, SelectedTerminals.Length - 1)
            'Else
            '    SelectedTerminals = ""
            'End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Cursor = Cursors.Default
            Me.Close()
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            LogException(ex)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        dtToDate.Value = Nothing
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmReportFilter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            If CallFromDirectMenu = True Then
                Me.Location = New Point(600, 45)
            End If
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            Me.dtToDate.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown
            If selectedReport = "DayCloseReport" Then
                Dim objReportBase As New ReportBase
                opendate = objReportBase.GetOpenDateAndStatus(clsAdmin.SiteCode, clsDefaultConfiguration.ClientForMail)
                If opendate <> Nothing Then
                    Me.dtToDate.Calendar.MaxDate = opendate
                    dtToDate.Value = opendate
                Else
                    dtToDate.Value = Nothing
                    MsgBox("there is no day Close Data", getValueByKey("CLAE04"))
                    Me.Close()
                End If
            End If
            If selectedReport = "TargetVsActualSales" Then
                GetMonhtsAndYears()
                ComboBoxMonth.Visible = True
                ComboBoxYear.Visible = True
                lblFromDate.Text = "Month"
                lblFromDate.Visible = True
                lblToDate.Text = "Year"
                dtFromDate.Visible = False
                dtToDate.Visible = False
            Else
                ComboBoxMonth.Visible = False
                ComboBoxYear.Visible = False
            End If
            If selectedReport = "JKOftheDayPayoutReport" Then
                Dim objReportBase As New ReportBase
                lblFromDate.Visible = True
                lblToDate.Visible = True
                dtFromDate.Visible = True
                dtToDate.Visible = True
                pnlPromotions.Visible = True
                lblPrormotions.Visible = True
                chkListPromotions.Visible = True
                Dim dtPromotions As New DataTable
                dtPromotions = objReportBase.GetSitePromotions(clsAdmin.SiteCode)
                If dtPromotions.Rows.Count > 0 Then
                    For Each row As DataRow In dtPromotions.Rows
                        chkListPromotions.Items.Add(row.Item("PromotionName"), False)
                        chkListPromotions.Items.IndexOf(row.Item("PromotionNo"))
                        'chkListPromotions.DataSource = dtPromotions
                        'chkListPromotions.DisplayMember = "PromotionName"
                        'chkListPromotions.ValueMember = "PromotionName"
                    Next row
                    chkListPromotions.CheckOnClick = True
                End If
            Else
                pnlPromotions.Visible = False
                lblPrormotions.Visible = False
                chkListPromotions.Visible = False
            End If
            'added by khusrao adil on 29-11-2017 for jk sprint 32
            If selectedReport = "ConversionReport" Then
                lblFromDate.Visible = True
                lblToDate.Visible = True
                dtFromDate.Visible = True
                dtToDate.Visible = True
                'commented by khusrao adil on 06-12-2017 jk sprint 32-changes
                'Dim dtTerminals As New DataTable
                'dtTerminals = objcomm.GetTerminals(clsAdmin.SiteCode, False)
                'If dtTerminals.Rows.Count > 0 Then
                '    For Each row As DataRow In dtTerminals.Rows
                '        chkListTerminals.Items.Add(row.Item("TERMINALNAME"), False)
                '        chkListTerminals.Items.IndexOf(row.Item("TERMINALID"))
                '    Next row
                '    chkListTerminals.CheckOnClick = True
                '    'chkListTerminals.SetItemChecked(
                '    For index As Integer = 0 To chkListTerminals.Items.Count - 1
                '        chkListTerminals.SetItemChecked(index, True)
                '    Next
                'End If
            End If
            If selectedReport = "JKProductMixReport" Then
                lblFromDate.Visible = True
                lblToDate.Visible = True
                dtFromDate.Visible = True
                dtToDate.Visible = True
            End If
            If selectedReport = "SalesReconciliationReport" Then
                lblFromDate.Visible = True
                lblToDate.Visible = True
                dtFromDate.Visible = True
                dtToDate.Visible = True
            End If
            If selectedReport = "HcCustomerDetailsReport" Then
                GetCustomerClassify()
                pnlPromotions.Visible = False
                pnlCustomerClass.Visible = True
            Else
                pnlCustomerClass.Visible = False
            End If
            If selectedReport = "BillSummaryReport" Then
                lblFromDate.Visible = True
                lblToDate.Visible = True
                dtFromDate.Visible = True
                dtToDate.Visible = True
            End If
            If selectedReport = "BillWiseGSTReport" Then
                lblFromDate.Visible = True
                lblToDate.Visible = True
                dtFromDate.Visible = True
                dtToDate.Visible = True

            End If
            If selectedReport = "BillWiseTenderReport" Then
                lblFromDate.Visible = True
                lblToDate.Visible = True
                dtFromDate.Visible = True
                dtToDate.Visible = True

            End If
            If selectedReport = "SpectrumKOTReport" Then
                lblFromDate.Visible = True
                lblToDate.Visible = True
                dtFromDate.Visible = True
                dtToDate.Visible = True

            End If
            If selectedReport = "DeliveryPartnerWiseSalesReport" Then
                Dim dtpartner As DataTable
                Dim objReportBase As New ReportBase
                ' dtpartner = objReportBase.getpartner()
                ' PopulateComboBox(dtpartner, cmbpartner)
                ' pC1ComboSetDisplayMember(cmbpartner)
                pnlPromotions.Visible = True
                lblepartner.Visible = True
                chkListPromotions.Visible = True
                lblFromDate.Visible = True
                lblToDate.Visible = True
                dtFromDate.Visible = True
                dtToDate.Visible = True
                Dim dtPromotions As New DataTable
                dtpartner = objReportBase.getpartner(clsAdmin.SiteCode)
                If dtpartner.Rows.Count > 0 Then
                    For Each row As DataRow In dtpartner.Rows
                        chkListPromotions.Items.Add(row.Item("DelieveryPartnerName"), False)
                        chkListPromotions.Items.IndexOf(row.Item("DelieveryPartnerId"))
                        'chkListPromotions.DataSource = dtPromotions
                        'chkListPromotions.DisplayMember = "PromotionName"
                        'chkListPromotions.ValueMember = "PromotionName"

                    Next row
                    chkListPromotions.CheckOnClick = True
                End If
            Else
                lblepartner.Visible = False
                ' pnlPromotions.Visible = False
                ' lblPrormotions.Visible = False
                'chkListPromotions.Visible = False

            End If


            'vipul 06-09-2018 tenderwisecommisionreport
            If selectedReport = "TenderWiseCommisionReport" Then
                Dim dtTender As DataTable
                Dim objReportBase As New ReportBase

                pnlPromotions.Visible = True
                lblepartner.Visible = True
                chkListPromotions.Visible = True
                lblFromDate.Visible = True
                lblToDate.Visible = True
                dtFromDate.Visible = True
                dtToDate.Visible = True
                Dim dtPromotions As New DataTable
                dtTender = objReportBase.GetTender(clsAdmin.SiteCode)
                If dtTender.Rows.Count > 0 Then
                    For Each row As DataRow In dtTender.Rows
                        chkListPromotions.Items.Add(row.Item("TenderType"), False)
                        chkListPromotions.Items.IndexOf(row.Item("TenderType"))

                    Next row
                    chkListPromotions.CheckOnClick = True
                End If
            Else
                lblepartner.Visible = False

            End If


        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Sub GetMonhtsAndYears()
        Try
            ComboBoxMonth.Items.Clear()
            Dim dt = Convert.ToDateTime("2016-01-01")
            For month As Integer = 0 To 11
                Dim _month As String
                _month = dt.ToString("MMMM")
                ComboBoxMonth.Items.Insert(month, _month)
                dt = dt.Date.AddMonths(1)
            Next
            Dim y As Integer = 2001
            ComboBoxYear.Items.Clear()
            For year As Integer = 0 To 25
                ComboBoxYear.Items.Insert(year, y)
                y = y + 1
            Next
            'ComboBoxMonth.DataSource = ReportSource
            ComboBoxMonth.SelectedIndex = 0
            ComboBoxYear.SelectedIndex = 0
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Sub GetCustomerClassify()
        ComboBoxClass.Items.Clear()
        ComboBoxClass.Items.Insert(0, "ALL")
        ComboBoxClass.Items.Insert(1, "Patient")
        ComboBoxClass.Items.Insert(2, "Consumer")
        ComboBoxClass.Items.Insert(3, "Both Patient And Consumer")
    End Sub
    Private Sub dtToDate_TextChanged(sender As Object, e As EventArgs) Handles dtToDate.TextChanged
        Try
            Dim objReportBase As New ReportBase
            If selectedReport = "DayCloseReport" Then
                If (dtToDate.Value) <> Nothing Then
                    If (dtToDate.Text > opendate) Then
                        dtToDate.Value = opendate
                        'ElseIf (objReportBase.GetOpenDate(clsAdmin.SiteCode, dtToDate.Text) = Nothing) Then
                        '    ShowMessage(" Day close report not found as Day Open/ Close activity was not performed for " & dtToDate.Text.ToString(), getValueByKey("CLAE04"))
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub ComboBoxMonth_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBoxMonth.SelectedIndexChanged
        _selectedmonth = ComboBoxMonth.SelectedIndex + 1
    End Sub

    Private Sub ComboBoxYear_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBoxYear.SelectedIndexChanged
        _selectedyear = ComboBoxYear.SelectedItem
    End Sub
    Private Function Themechange()
        'Me.Size = New Size(847, 410)
        Me.BackColor = Color.FromArgb(134, 134, 134)
        lblFromDate.ForeColor = Color.White
        ' lblFromDate.BackColor = Color.FromArgb(212, 212, 212)
        lblFromDate.AutoSize = False
        lblFromDate.Size = New Size(65, 16)
        lblFromDate.SendToBack()
        lblFromDate.TextAlign = ContentAlignment.MiddleLeft
        lblFromDate.Font = New Font("Neo Sans", 8, FontStyle.Bold)

        lblToDate.ForeColor = Color.White
        'lblToDate.BackColor = Color.FromArgb(212, 212, 212)
        lblToDate.AutoSize = False
        lblToDate.Size = New Size(65, 16)
        lblToDate.SendToBack()
        lblToDate.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        lblToDate.TextAlign = ContentAlignment.MiddleLeft

        lblExpryDate.ForeColor = Color.White
        ' lblExpryDate.BackColor = Color.FromArgb(212, 212, 212)
        lblExpryDate.AutoSize = False
        lblExpryDate.Size = New Size(65, 16)
        lblExpryDate.SendToBack()
        lblExpryDate.TextAlign = ContentAlignment.MiddleLeft
        lblExpryDate.Font = New Font("Neo Sans", 8, FontStyle.Bold)

        lblTimeSpan.ForeColor = Color.White
        ' lblTimeSpan.BackColor = Color.FromArgb(212, 212, 212)
        lblTimeSpan.AutoSize = False
        lblTimeSpan.Size = New Size(65, 16)
        lblTimeSpan.SendToBack()
        lblTimeSpan.TextAlign = ContentAlignment.MiddleLeft
        lblTimeSpan.Font = New Font("Neo Sans", 8, FontStyle.Bold)

        'Button1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Button1.BackColor = Color.Transparent
        Button1.BackColor = Color.FromArgb(0, 107, 163)
        Button1.ForeColor = Color.FromArgb(255, 255, 255)
        Button1.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        ' Button1.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0
        Button1.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        Button1.Size = New Size(80, 30)

        'Button2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Button2.BackColor = Color.Transparent
        Button2.BackColor = Color.FromArgb(0, 107, 163)
        Button2.ForeColor = Color.FromArgb(255, 255, 255)
        Button2.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'Button2.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Button2.FlatStyle = FlatStyle.Flat
        Button2.FlatAppearance.BorderSize = 0
        Button2.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        Button2.Size = New Size(80, 30)

    End Function

    Private Sub ComboBoxClass_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBoxClass.SelectedIndexChanged
        If ComboBoxClass.SelectedIndex = 3 Then
            _selectedClassify = "Both"
        Else
            _selectedClassify = ComboBoxClass.SelectedItem
        End If
    End Sub
 
End Class