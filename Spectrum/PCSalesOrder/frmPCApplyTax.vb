Imports Spectrum
Imports SpectrumBL
Imports C1.Win.C1FlexGrid
Public Class frmPCApplyTax
    Dim objComn As New clsCommon
    Dim dtList As DataTable
    Public dtBind As DataTable
    Dim rowTax As DataRow

    Private _GrossAmt As String
    Public Property GrossAmt() As String
        Get
            Return _GrossAmt
        End Get
        Set(ByVal value As String)
            _GrossAmt = value
        End Set
    End Property


    Private Sub frmPCApplyTax_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetCulture(Me, Me.Name)
            cmdAdd.Enabled = False
            lblGrossValue.Text = GrossAmt
            dtList = objComn.GetAllTaxesAppliedToSiteDocumentLevel(clsAdmin.SiteCode, "SO201")
            PopulateComboBox(dtList, cboTax)
            pC1ComboSetDisplayMember(cboTax)

            With dgMainGrid
                .VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
                .ScrollBars = ScrollBars.Both
                .AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns
                .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
            End With

            If dtBind.Rows.Count > 0 Then
                dgMainGrid.Rows.RemoveRange(1, dgMainGrid.Rows.Count - 1)
                ShowDetail()

            Else
                dtBind = objComn.GetDetailsForTax()
                dtBind.Rows.Clear()
                GridSettings()
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Try
            For index = 0 To dtBind.Rows.Count - 1
                If dtBind.Rows(index)("TaxName") = cboTax.SelectedText Then
                    ShowMessage("Tax is already added", "Tax")
                    Exit Sub
                End If
            Next
            If dtBind.Rows.Count > 0 Then
                dgMainGrid.Rows.RemoveRange(1, dgMainGrid.Rows.Count - 1)
            End If


            Dim dtTaxDetails = objComn.GetAllTaxesDetailAppliedToSite(cboTax.SelectedText, "SO201")

            Dim selectedRows() As DataRow = dtTaxDetails.Select("", "", DataViewRowState.CurrentRows)
            For Each dr As DataRow In selectedRows
                rowTax = dtBind.NewRow()
                rowTax("TaxName") = dr("TaxName")
                rowTax("TaxCode") = dr("TaxCode")
                rowTax("TaxPercent") = dr("TaxValue")
                rowTax("Type") = IIf(dr("Inclusive") = False, "Exclusive", "Inclusive")
                If dr("IsPercentageValue") Then
                    rowTax("TaxAmt") = ((GrossAmt * dr("TaxValue")) / 100)
                Else
                    rowTax("TaxAmt") = dr("TaxValue")
                End If
                rowTax("Status") = "New"
                dtBind.Rows.Add(rowTax)
            Next

            ShowDetail()
            cboTax.SelectedIndex = -1

            'dgMainGrid.SubtotalPosition = C1.Win.C1FlexGrid.SubtotalPositionEnum.BelowData

            'Dim totalOn = dgMainGrid.Cols.Count - 1
            'dgMainGrid.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, 0, totalOn)
            'dgMainGrid(dgMainGrid.Rows.Count - 1, 4) = "Total"
            'dgMainGrid.Rows(dgMainGrid.Rows.Count - 1).AllowEditing = False

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub ShowDetail()
        Try
            GridSettings()
            'dgMainGrid.DataSource = dtBind
            Dim indexP As Integer = 1
            For Each dr As DataRow In dtBind.Rows

                dgMainGrid.Rows.Add()
                dgMainGrid.Rows(indexP)("TaxName") = dr("TaxName")
                dgMainGrid.Rows(indexP)("TaxCode") = dr("TaxCode").ToString()
                dgMainGrid.Rows(indexP)("TaxPercent") = dr("TaxPercent")
                dgMainGrid.Rows(indexP)("Type") = dr("Type").ToString()
                dgMainGrid.Rows(indexP)("TaxAmt") = dr("TaxAmt").ToString()

                indexP = indexP + 1

            Next

            dgMainGrid.SubtotalPosition = C1.Win.C1FlexGrid.SubtotalPositionEnum.BelowData

            Dim totalOn = dgMainGrid.Cols.Count - 3
            dgMainGrid.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, 0, totalOn)
            dgMainGrid(dgMainGrid.Rows.Count - 1, 4) = "Total"
            dgMainGrid.Rows(dgMainGrid.Rows.Count - 1).AllowEditing = False

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub GridSettings()
        Try

            dgMainGrid.Cols("Delete").Caption = ""
            dgMainGrid.Cols("Delete").Width = 20
            dgMainGrid.Cols("Delete").ComboList = "..."

            dgMainGrid.Cols("TaxName").Width = 130
            dgMainGrid.Cols("TaxName").AllowEditing = False
            dgMainGrid.Cols("TaxName").Caption = "Tax Name"

            dgMainGrid.Cols("TaxPercent").Width = 150
            dgMainGrid.Cols("TaxPercent").AllowEditing = False
            dgMainGrid.Cols("TaxPercent").Caption = "Tax Percentage"

            dgMainGrid.Cols("Type").Width = 120
            dgMainGrid.Cols("Type").AllowEditing = False
            dgMainGrid.Cols("Type").Caption = "Type"

            dgMainGrid.Cols("TaxAmt").Width = 180
            dgMainGrid.Cols("TaxAmt").AllowEditing = False
            dgMainGrid.Cols("TaxAmt").Caption = "Tax Amt."

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cboTax_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboTax.SelectedValueChanged
        If Not String.IsNullOrEmpty(cboTax.SelectedValue) Then
            cmdAdd.Enabled = True
        Else
            cmdAdd.Enabled = False
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Try
            For index = 0 To dtBind.Rows.Count - 1
                dtBind.Rows(index)("Status") = "Saved"
            Next

            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dgMainGrid_CellButtonClick(sender As Object, e As RowColEventArgs) Handles dgMainGrid.CellButtonClick
        Dim selectedRows() As DataRow = dtBind.Select("TaxName='" & dgMainGrid.Rows(dgMainGrid.RowSel)("TaxName").ToString() & "'", "", DataViewRowState.CurrentRows)
        For Each dr As DataRow In selectedRows
            dr.Delete()
        Next
        dtBind.AcceptChanges()
        dgMainGrid.Rows.RemoveRange(1, dgMainGrid.Rows.Count - 1)
        ShowDetail()
    End Sub
    Private Function Themechange()

        Me.BackColor = Color.FromArgb(134, 134, 134)

        lblGrossAmt.ForeColor = Color.White
        lblGrossAmt.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblGrossAmt.BackColor = Color.Transparent
        lblGrossAmt.BorderStyle = BorderStyle.None

        lblGrossValue.ForeColor = Color.White
        lblGrossValue.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblGrossValue.BackColor = Color.Transparent
        lblGrossValue.BorderStyle = BorderStyle.None

        cmdAdd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdAdd.BackColor = Color.Transparent
        cmdAdd.BackColor = Color.FromArgb(0, 107, 163)
        cmdAdd.ForeColor = Color.FromArgb(255, 255, 255)
        cmdAdd.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        cmdAdd.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdAdd.FlatStyle = FlatStyle.Flat
        cmdAdd.FlatAppearance.BorderSize = 0
        cmdAdd.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdAdd.TextAlign = ContentAlignment.MiddleCenter
        cmdAdd.Size = New Size(94, 25)

        cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdCancel.BackColor = Color.Transparent
        cmdCancel.BackColor = Color.FromArgb(0, 107, 163)
        cmdCancel.ForeColor = Color.FromArgb(255, 255, 255)
        cmdCancel.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        cmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdCancel.FlatStyle = FlatStyle.Flat
        cmdCancel.FlatAppearance.BorderSize = 0
        cmdCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdCancel.TextAlign = ContentAlignment.MiddleCenter

        cmdSave.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdSave.BackColor = Color.Transparent
        cmdSave.BackColor = Color.FromArgb(0, 107, 163)
        cmdSave.ForeColor = Color.FromArgb(255, 255, 255)
        cmdSave.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        cmdSave.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdSave.FlatStyle = FlatStyle.Flat
        cmdSave.FlatAppearance.BorderSize = 0
        cmdSave.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        cmdSave.TextAlign = ContentAlignment.MiddleCenter

        lblSelectTax.ForeColor = Color.Black
        lblSelectTax.AutoSize = False
        lblSelectTax.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblSelectTax.BorderStyle = BorderStyle.None
        lblSelectTax.BackColor = Color.FromArgb(212, 212, 212)
        lblSelectTax.SendToBack()
        lblSelectTax.Size = New Size(115, 21)

        dgMainGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        dgMainGrid.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgMainGrid.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgMainGrid.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        dgMainGrid.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgMainGrid.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)

    End Function
End Class