Imports SpectrumPrint

Public Class frmNReprintSearch
    Dim dtDocInfo As New DataTable
    Dim objSPrint As New clsReprintBill
    Dim _DocumentNo As String
    Public Property DocumentNo() As String
        Get
            Return _DocumentNo
        End Get
        Set(ByVal value As String)
            _DocumentNo = value
        End Set
    End Property

    'Added by Rohit for Issue No. 0006119 Reprint BL Error
    Dim _SaleInvNo As String
    Public Property SaleInvNo() As String
        Get
            Return _SaleInvNo
        End Get
        Set(ByVal value As String)
            _SaleInvNo = value
        End Set
    End Property

    Private Sub frmNReprintSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            dtDocInfo = objSPrint.GetAllDocumentInfo(clsAdmin.SiteCode, clsAdmin.Financialyear, PrintTransType)
            'grdDocumentInfo.DataSource = dtDocInfo

            grdDocumentInfo1.DataSource = dtDocInfo

            For colIndex = 0 To grdDocumentInfo1.Splits(0).DisplayColumns.Count - 1
                grdDocumentInfo1.Splits(0).DisplayColumns(colIndex).AutoSize()
            Next

            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
        End Try
        SetCulture(Me, Me.Name)
    End Sub

    Private Sub SetGridColumnCaption()
        Try
            grdDocumentInfo.Cols("TenderTypeCode").Caption = getValueByKey("frmnreprint.grdinvoiceinfo.tendertypecode")
            grdDocumentInfo.Cols("TenderTypeCode").Width = 220
            grdDocumentInfo.Cols("AmountTendered").Caption = getValueByKey("frmnreprint.grdinvoiceinfo.amounttendered")
            grdDocumentInfo.Cols("AmountTendered").Width = 200
            grdDocumentInfo.Cols("SOInvDate").Caption = getValueByKey("frmnreprint.grdinvoiceinfo.soinvdate")
            grdDocumentInfo.Cols("SOInvDate").Width = 120

            Dim ShowColumns As C1.Win.C1TrueDBGrid.C1DisplayColumnCollection
            ShowColumns = grdDocumentInfo1.Splits.Item(0).DisplayColumns

            ShowColumns.Item("TenderTypeCode").DataColumn.Caption = getValueByKey("frmnreprint.grdinvoiceinfo.tendertypecode")
            ShowColumns.Item("TenderTypeCode").Width = 220
            ShowColumns.Item("AmountTendered").DataColumn.Caption = getValueByKey("frmnreprint.grdinvoiceinfo.amounttendered")
            ShowColumns.Item("AmountTendered").Width = 200
            ShowColumns.Item("SOInvDate").DataColumn.Caption = getValueByKey("frmnreprint.grdinvoiceinfo.soinvdate")
            ShowColumns.Item("SOInvDate").Width = 120
        Catch ex As Exception
        End Try
    End Sub

    Private Sub grdDocumentInfo1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDocumentInfo1.Click
        'Try
        '    If grdDocumentInfo1.RowCount > 0 Then
        '        _DocumentNo = grdDocumentInfo1.Item(grdDocumentInfo1.Row, "DocumentNo").ToString

        '        'Added by Rohit for Issue No. 0006119 Reprint BL Error
        '        Try
        '            SaleInvNo = grdDocumentInfo1.Item(grdDocumentInfo1.Row, "SaleInvNumber")
        '        Catch ex As Exception
        '        End Try

        '        'Me.DialogResult = Windows.Forms.DialogResult.OK
        '        'Me.Close()
        '        'Change end
        '    End If
        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub grdDocumentInfo1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDocumentInfo1.DoubleClick
        Try
            If grdDocumentInfo1.RowCount > 0 Then
                _DocumentNo = grdDocumentInfo1.Item(grdDocumentInfo1.Row, "DocumentNo").ToString

                'Added by Rohit for Issue No. 0006119 Reprint BL Error
                Try
                    SaleInvNo = grdDocumentInfo1.Item(grdDocumentInfo1.Row, "SaleInvNumber")
                Catch ex As Exception
                End Try
                'Change end

                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub grdDocumentInfo1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdDocumentInfo1.MouseClick
        
    End Sub

    Private Sub grdDocumentInfo1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdDocumentInfo1.MouseDoubleClick
        
    End Sub

    Private Sub grdDocumentInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDocumentInfo.Click
        Try
            'If grdDocumentInfo.Rows.Count > 0 Then
            '    _DocumentNo = grdDocumentInfo.Item(grdDocumentInfo.Row, "DocumentNo").ToString

            '    'Added by Rohit for Issue No. 0006119 Reprint BL Error
            '    Try
            '        SaleInvNo = grdDocumentInfo1.Item(grdDocumentInfo1.Row, "SaleInvNumber")
            '    Catch ex As Exception
            '    End Try
            '    'Change end
            'End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub grdDocumentInfo_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDocumentInfo.DoubleClick
        Try
            If grdDocumentInfo.Rows.Count > 0 Then
                _DocumentNo = grdDocumentInfo.Item(grdDocumentInfo.Row, "DocumentNo").ToString
                SaleInvNo = grdDocumentInfo1.Item(grdDocumentInfo1.Row, "SaleInvNumber")
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub grdDocumentInfo_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdDocumentInfo.MouseClick
        
    End Sub
    Private Sub grdDocumentInfo_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdDocumentInfo.MouseDoubleClick
        
    End Sub

    Private Sub BtnSearchOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchOK.Click
        Try
            If grdDocumentInfo.Rows.Count > 0 Then
                Try
                    _DocumentNo = grdDocumentInfo1.Item(grdDocumentInfo1.Row, "DocumentNo").ToString
                Catch ex As Exception

                End Try

                Try
                    SaleInvNo = grdDocumentInfo1.Item(grdDocumentInfo1.Row, "SaleInvNumber")
                Catch ex As Exception

                End Try

                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnSearchCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        grdDocumentInfo.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.System
        grdDocumentInfo.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        grdDocumentInfo.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        grdDocumentInfo.Rows.MinSize = 25
        grdDocumentInfo.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        grdDocumentInfo.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdDocumentInfo.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdDocumentInfo.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdDocumentInfo.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdDocumentInfo.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        BtnSearchOK.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnSearchOK.BackColor = Color.Transparent
        BtnSearchOK.BackColor = Color.FromArgb(0, 107, 163)
        BtnSearchOK.ForeColor = Color.FromArgb(255, 255, 255)
        BtnSearchOK.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnSearchOK.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnSearchOK.FlatStyle = FlatStyle.Flat
        BtnSearchOK.FlatAppearance.BorderSize = 0
        BtnSearchOK.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        BtnSearchOK.Size = New Size(67, 27)
        BtnSearchCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnSearchCancel.BackColor = Color.Transparent
        BtnSearchCancel.BackColor = Color.FromArgb(0, 107, 163)
        BtnSearchCancel.ForeColor = Color.FromArgb(255, 255, 255)
        BtnSearchCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnSearchCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnSearchCancel.FlatStyle = FlatStyle.Flat
        BtnSearchCancel.FlatAppearance.BorderSize = 0
        BtnSearchCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        BtnSearchCancel.Size = New Size(67, 27)
        BtnSearchCancel.Location = New Point(440, 534)
    End Function
   
End Class