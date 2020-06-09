Imports SpectrumBL

Public Class frmUpdateCheckDueDate

    Private dsSource As New DataSet
    Private objClsUpdateCheckDueDate As New clsUpdateCheckDueDate
    Dim intCurrRow As Int32 = 0

    Private Sub frmUpdateCheckDueDate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dtFromDate As Date
        Try
            dtFromDate = objClsUpdateCheckDueDate.GetFromDate(clsAdmin.SiteCode)
            If dtFromDate <= Now.Date Then
                dsSource = objClsUpdateCheckDueDate.GetCheckData(clsAdmin.SiteCode, dtFromDate, Now.Date, True)
                grdCheckDetails.DataSource = dsSource.Tables("CheckDtls")
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
        
        grdCheckDetails.CaptionStyle.BackgroundImage = Nothing
        SetCulture(Me, Me.Name)
        GridSettings()
        dtFrom.Value = Now.Date
        dtTo.Value = Nothing
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            If Not dtFrom.ValueIsDbNull And Not dtTo.ValueIsDbNull Then
                dsSource = objClsUpdateCheckDueDate.GetCheckData(clsAdmin.SiteCode, dtFrom.Value, dtTo.Value)
                grdCheckDetails.DataSource = dsSource.Tables("CheckDtls")
                GridSettings()

            Else
                ShowMessage(getValueByKey("UCDD05"), "UCDD05 - " & getValueByKey("CLAE04"))
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("UCDD01"), "UCDD01 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub GridSettings()
        Try

            grdCheckDetails.Columns("BillDate").Caption = IIf(resourceMgr Is Nothing, "Cash Memo Date", getValueByKey("frmupdatecheckduedate.grdcheckdetails.billdate"))
            ' grdCheckDetails.Columns("BillDate").WidthDisplay = 120
            'grdCheckDetails.Columns("BillDate").AllowEditing = False
            grdCheckDetails.Columns("BillNo").Caption = IIf(resourceMgr Is Nothing, "Cash Memo No.", getValueByKey("frmupdatecheckduedate.grdcheckdetails.billno"))
            grdCheckDetails.Splits(0).DisplayColumns("BillNo").Width = 140
            'grdCheckDetails.Columns("BillNo").WidthDisplay = 120
            'grdCheckDetails.Columns("BillNo").AllowEditing = False
            grdCheckDetails.Columns("Amount").Caption = IIf(resourceMgr Is Nothing, "Cash Memo Amt.", getValueByKey("frmupdatecheckduedate.grdcheckdetails.amount"))
            'grdCheckDetails.Columns("Amount").WidthDisplay = 120
            'grdCheckDetails.Columns("Amount").AllowEditing = False
            grdCheckDetails.Columns("CheckNo").Caption = IIf(resourceMgr Is Nothing, "Check No.", getValueByKey("frmupdatecheckduedate.grdcheckdetails.checkno"))
            'grdCheckDetails.Columns("CheckNo").AllowEditing = False
            grdCheckDetails.Columns("DueDate").Caption = IIf(resourceMgr Is Nothing, "Due Date", getValueByKey("frmupdatecheckduedate.grdcheckdetails.duedate"))
            grdCheckDetails.Columns("Remarks").Caption = IIf(resourceMgr Is Nothing, "Remarks", getValueByKey("frmupdatecheckduedate.grdcheckdetails.remarks"))
            'grdCheckDetails.Columns("CashierName").Caption = IIf(resourceMgr Is Nothing, "Cashier Name", getValueByKey("frmupdatecheckduedate.grdcheckdetails.cashiername"))
            'grdCheckDetails.Columns("CashierName").AllowEditing = False

            grdCheckDetails.AllowUpdate = True
            For Each col As DataColumn In dsSource.Tables("CheckDtls").Columns
                If col.ColumnName <> "BillDate" And col.ColumnName <> "BillNo" And col.ColumnName <> "Amount" And col.ColumnName <> "CheckNo" And col.ColumnName <> "DueDate" And col.ColumnName <> "Remarks" And col.ColumnName <> "CashierName" Then
                    grdCheckDetails.Splits(0).DisplayColumns(col.ColumnName).Visible = False
                End If
                If col.ColumnName <> "DueDate" AndAlso col.ColumnName <> "Remarks" Then
                    grdCheckDetails.Columns(col.ColumnName).DataChanged = False
                    grdCheckDetails.Splits(0).DisplayColumns(col.ColumnName).AllowFocus = False
                End If
            Next

            

            
            'Me.grdCheckDetails.HeadingStyle.BackgroundImage = C1.Win.C1TrueDBGrid
            grdCheckDetails.CaptionStyle.BackgroundImage = Nothing
            grdCheckDetails.HeadingStyle.BackgroundImage = Nothing
            grdCheckDetails.Style.BackgroundImage = Nothing
            grdCheckDetails.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.NoMarquee
            grdCheckDetails.MultiSelect = C1.Win.C1TrueDBGrid.MultiSelectEnum.None
            grdCheckDetails.AllowFilter = True
            'grdCheckDetails.AllowRowSelect = True
            grdCheckDetails.FilterBar = True
            grdCheckDetails.Splits(0).DisplayColumns(0).FilterButton = True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    'Private Sub grdCheckDetails_AfterColEdit(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles grdCheckDetails.AfterColEdit
    '    Try
    '        'CType(grdCheckDetails.Columns(e.ColIndex), C1.Win.C1FlexGrid.Column).Name

    '        If grdCheckDetails.Columns(e.ColIndex).DataField.ToString = "DueDate" Then
    '            If grdCheckDetails.Item(grdCheckDetails.Row, e.ColIndex) < Now.Date Then
    '                ShowMessage(getValueByKey("UCDD04"), "UCDD04 - " & getValueByKey("CLAE04"))
    '            End If
    '        End If

    '    Catch ex As Exception
    '        ShowMessage(getValueByKey("UCDD03"), "UCDD03 - " & getValueByKey("CLAE05"))
    '        LogException(ex)
    '    End Try

    'End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click

        grdCheckDetails.MoveRelative(1)

        If objClsUpdateCheckDueDate.SaveCheckData(dsSource) Then
            ShowMessage(getValueByKey("UCDD02"), "UCDD02 - " & getValueByKey("CLAE04"))
        Else
            ShowMessage(getValueByKey("UCDD03"), "UCDD03 - " & getValueByKey("CLAE05"))
        End If
    End Sub

    Private Sub grdCheckDetails_BeforeColEdit(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.BeforeColEditEventArgs) Handles grdCheckDetails.BeforeColEdit
        Dim dtFromDate As Date
        dtFromDate = objClsUpdateCheckDueDate.GetFromDate(clsAdmin.SiteCode)
        Dim rowno As Integer = grdCheckDetails.Row
        Dim dtDueDate As DateTime = dtFromDate
        Try
            dtDueDate = IIf(grdCheckDetails.Columns("DueDate").Value <> String.Empty, grdCheckDetails.Columns("DueDate").Value, dtFromDate)
        Catch ex As Exception

        End Try

        If dtDueDate < dtFromDate Then
            e.Cancel = True
            ShowMessage(getValueByKey("UCDD06"), "UCDD06 - " & getValueByKey("CLAE04"))
        End If
    End Sub
   

    Private Sub grdCheckDetails_BeforeColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs) Handles grdCheckDetails.BeforeColUpdate
        Try
            If grdCheckDetails.Columns(e.ColIndex).DataField.ToString = "DueDate" Then
                If grdCheckDetails.Columns(e.ColIndex).Value < Now.Date Then
                    ShowMessage(getValueByKey("UCDD04"), "UCDD04 - " & getValueByKey("CLAE04"))
                    e.Cancel = True
                End If

            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("UCDD03"), "UCDD03 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try

    End Sub

    Private Sub frmUpdateCheckDueDate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Dim objClsCommon As New clsCommon
                objClsCommon.DisplayHelpFile(ParentForm, "change-credit-check-details.htm")
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Function Themechange() As String

        Me.BackColor = Color.FromArgb(134, 134, 134)

        Me.sizHeader.Grid.Rows(2).Size = 42
        'lblFrom
        '
        Me.lblFrom.ForeColor = Color.White
        Me.lblFrom.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        Me.lblTo.ForeColor = Color.White
        Me.lblTo.Font = New Font("Neo Sans", 9, FontStyle.Bold)
       
        grdCheckDetails.Styles(0).BackColor = Color.FromArgb(255, 255, 255)
        grdCheckDetails.Styles(0).Font = New Font("Neo Sans", 10, FontStyle.Regular)
        grdCheckDetails.Styles(1).Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdCheckDetails.Styles(1).BackColor = Color.FromArgb(177, 227, 253)
        grdCheckDetails.Splits(0).Style.Font = New Font("Neo Sans", 9, FontStyle.Regular)
        grdCheckDetails.Styles(5).Font = New Font("Neo Sans", 9, FontStyle.Bold)
        grdCheckDetails.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Custom
        grdCheckDetails.HighLightRowStyle.BackColor = Color.LightBlue
        grdCheckDetails.HighLightRowStyle.ForeColor = Color.WhiteSmoke
        'btnSubmit
        '
        Me.btnSubmit.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnSubmit.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnSubmit.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnSubmit.MaximumSize = New Size(0, 30)
        Me.btnSubmit.MinimumSize = New Size(0, 30)
        Me.btnSubmit.Size = New System.Drawing.Size(68, 30)
        'Me.btnSubmit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSubmit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSubmit.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnSubmit.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnSubmit.FlatStyle = FlatStyle.Flat
        Me.btnSubmit.FlatAppearance.BorderSize = 0
        Me.btnSubmit.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        'BtnSave
        '
        Me.BtnSave.BackColor = Color.FromArgb(0, 107, 163)
        Me.BtnSave.ForeColor = Color.FromArgb(255, 255, 255)
        Me.BtnSave.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.BtnSave.MaximumSize = New Size(141, 42)
        Me.BtnSave.MinimumSize = New Size(141, 42)
        Me.BtnSave.Size = New System.Drawing.Size(141, 42)
        'Me.BtnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnSave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.BtnSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.BtnSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.BtnSave.FlatStyle = FlatStyle.Flat
        Me.BtnSave.FlatAppearance.BorderSize = 0
        Me.BtnSave.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        'btnClose
        '
        Me.btnClose.BackColor = Color.FromArgb(0, 107, 163)
        Me.btnClose.ForeColor = Color.FromArgb(255, 255, 255)
        Me.btnClose.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        Me.btnClose.MaximumSize = New Size(124, 42)
        Me.btnClose.MinimumSize = New Size(124, 42)
        Me.btnClose.Size = New System.Drawing.Size(124, 42)
        'Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClose.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.btnClose.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.btnClose.FlatStyle = FlatStyle.Flat
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        Return ""
    End Function

    
End Class