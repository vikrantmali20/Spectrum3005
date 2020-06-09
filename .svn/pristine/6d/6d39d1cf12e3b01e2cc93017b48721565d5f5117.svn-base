Imports SpectrumBL
Imports SpectrumPrint
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Imports Spire.Pdf
Imports System.IO
Imports C1.Win.C1BarCode
Imports System.Text
Imports System.Drawing.Printing
Imports System.Drawing.Imaging
Imports C1.Win.C1FlexGrid

Public Class FrmSplitBill
    Dim DtAddCustomer As DataTable
    Dim DtBindDocumentData As DataTable
    Dim dtCustmInfo As New DataTable
    Dim objCm As New clsCommon
    Dim ObjCashMemoPrint As New clsCashMemoPrint(Nothing, False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving)
    Dim obj As New SpectrumBL.clsCashMemo()

    Private Function Themechange()
        Try
            Me.BackgroundColor = Color.FromArgb(134, 134, 134)
            grdInvoiceInfo.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
            grdInvoiceInfo.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
            grdInvoiceInfo.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
            grdInvoiceInfo.Rows.MinSize = 25
            grdInvoiceInfo.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
            grdInvoiceInfo.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            grdInvoiceInfo.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            grdInvoiceInfo.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            grdInvoiceInfo.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            grdInvoiceInfo.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)

            GrdAddCustomer.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
            GrdAddCustomer.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
            GrdAddCustomer.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
            GrdAddCustomer.Rows.MinSize = 25
            GrdAddCustomer.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
            GrdAddCustomer.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            GrdAddCustomer.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            GrdAddCustomer.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            GrdAddCustomer.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
            GrdAddCustomer.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)


            BtnSplitBill.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
            BtnSplitBill.BackColor = Color.Transparent
            BtnSplitBill.BackColor = Color.FromArgb(0, 107, 163)
            BtnSplitBill.ForeColor = Color.FromArgb(255, 255, 255)
            BtnSplitBill.Size = New Size(124, 32)
            BtnSplitBill.Font = New Font("Neo Sans", 9, FontStyle.Bold)
            BtnSplitBill.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
            BtnSplitBill.FlatStyle = FlatStyle.Flat
            BtnSplitBill.FlatAppearance.BorderSize = 0
            BtnSplitBill.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
            BtnSplitBill.TextAlign = ContentAlignment.MiddleCenter

            BtnAddCustomer.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
            BtnAddCustomer.BackColor = Color.Transparent
            BtnAddCustomer.BackColor = Color.FromArgb(0, 107, 163)
            BtnAddCustomer.ForeColor = Color.FromArgb(255, 255, 255)
            BtnAddCustomer.Size = New Size(124, 32)
            BtnAddCustomer.Font = New Font("Neo Sans", 9, FontStyle.Bold)
            BtnAddCustomer.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
            BtnAddCustomer.FlatStyle = FlatStyle.Flat
            BtnAddCustomer.FlatAppearance.BorderSize = 0
            BtnAddCustomer.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
            BtnAddCustomer.TextAlign = ContentAlignment.MiddleCenter


            BtnSearchInvoice.Image = Nothing
            BtnSearchInvoice.VisualStyle = C1.Win.C1Input.VisualStyle.System
            BtnSearchInvoice.BackgroundImage = My.Resources.SearchItems1
            BtnSearchInvoice.FlatStyle = FlatStyle.Flat
            BtnSearchInvoice.BackgroundImageLayout = ImageLayout.Stretch
            BtnSearchInvoice.Size = New Size(40, 21)
            CtrlLabel3.Size = New Size(110, 21)
            CtrlLabel3.Location = New Point(15, 14)
            CtrlLabel3.BackColor = Color.FromArgb(212, 212, 212)

            For i = 0 To grdInvoiceInfo.Cols.Count - 1
                grdInvoiceInfo.Cols(i).Caption = grdInvoiceInfo.Cols(i).Caption.ToUpper
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function

#Region "Function"
    Public Sub LoadDocumentData(ByVal DocumentNo As String)
        Try
            DtBindDocumentData = objCm.SplitBillDetail(DocumentNo, clsAdmin.SiteCode)
            Dim SummaryDtl = objCm.CashMemoHdrDetail(DocumentNo, clsAdmin.SiteCode)

            grdInvoiceInfo.DataSource = DtBindDocumentData

            If Not SummaryDtl Is Nothing AndAlso SummaryDtl.Rows.Count > 0 Then
                CashSummary.CtrllblGrossAmt.Text = SummaryDtl.Rows(0)("GrossAmt")
                CashSummary.CtrllblNetAmt.Text = SummaryDtl.Rows(0)("NetAmt")
                CashSummary.CtrllblTaxAmt.Text = SummaryDtl.Rows(0)("TaxAmount")
                CashSummary.CtrllblDiscAmt.Text = SummaryDtl.Rows(0)("TotalDiscount")
            End If

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Function DeleteGuestDetails(CUSTOMERNO As String) As Boolean '...
        Try
            If dtCustmInfo.Rows.Count > 0 Then
                Dim drDtl() = dtCustmInfo.Select("CUSTOMERNO='" & CUSTOMERNO & "'")
                If drDtl.Count > 0 Then
                    For Each row As DataRow In drDtl
                        dtCustmInfo.Rows.Remove(row)
                    Next
                End If
                dtCustmInfo.AcceptChanges()
                Dim count = 1
                For index = 0 To dtCustmInfo.Rows.Count - 1
                    dtCustmInfo.Rows(index)("CUSTOMERNO") = count
                    count += 1
                Next
                CUSTOMERNO = count
            End If
            'If dtCustmInfo.Rows.Count = 0 Then

            '    GrdAddCustomer.Clear()


            'End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Function

#End Region
    Private Sub FrmSplitBill_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            dtCustmInfo.Columns.Add("MOBILENO")
            dtCustmInfo.Columns.Add("CUSTOMERNAME")
            dtCustmInfo.Columns.Add("CUSTOMERNO")
            dtCustmInfo.Columns.Add("SplitAmt")
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnSearchInvoice_Click(sender As Object, e As EventArgs) Handles BtnSearchInvoice.Click
        Try
            Dim objSearch As New frmNReprintSearch
            ' objSearch.PrintTransType = "CashMemo"
            If objSearch.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim SelectDocNo As String = objSearch.DocumentNo
                txtDocNumber.Text = SelectDocNo
                LoadDocumentData(txtDocNumber.Text)
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub


    Private Sub CtrlBtn1_Click(sender As Object, e As EventArgs) Handles BtnAddCustomer.Click
        Try
            Dim objSearch As New frmSearchCustomer
            If objSearch.ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not objSearch.dtCustmInfo Is Nothing AndAlso objSearch.dtCustmInfo.Rows.Count > 0 Then
                    Dim DrCustRow As DataRow = objSearch.dtCustmInfo.Rows(0)

                    Dim Dr = dtCustmInfo.NewRow
                    Dr("MOBILENO") = DrCustRow("MOBILENO")
                    Dr("CUSTOMERNAME") = DrCustRow("CUSTOMERNAME")
                    Dr("CUSTOMERNO") = DrCustRow("CUSTOMERNO")
                    Dr("SplitAmt") = "0"
                    'dtCustmInfo
                    dtCustmInfo.Rows.Add(Dr)
                    GrdAddCustomer.DataSource = dtCustmInfo
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub GrdAddCustomer_CellButtonClick(sender As Object, e As RowColEventArgs) Handles GrdAddCustomer.CellButtonClick
        Try
            Dim CUSTOMERNO = GrdAddCustomer.Item(GrdAddCustomer.Row, "CUSTOMERNO")

            DeleteGuestDetails(CUSTOMERNO)
            GrdAddCustomer.DataSource = dtCustmInfo
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnSplitBill_Click(sender As Object, e As EventArgs) Handles BtnSplitBill.Click
        Try
            Dim TotalSplitAmount As Decimal = 0
            'Dim DtSplitBillCustDetail = objCm.GetCashMemoSplitDtlStruct()
            'For Each drcust In GrdAddCustomer.Rows
            '    If ChkCustomer.Checked = True Then
            '        If drcust("SplitAmt").ToString.Trim() = "0" Then
            '            ShowMessage("Please Enter Split Amount", getValueByKey("CLAE05"))
            '            Exit Sub
            '        End If
            '    End If
            '    If drcust("CustomerNO") <> "CUSTOMERNO" Then
            '        Dim Dr1 = DtSplitBillCustDetail.NewRow()
            '        Dr1("BillNo") = txtDocNumber.Text
            '        Dr1("Customerid") = drcust("CustomerNO")
            '        Dr1("SplitAmt") = drcust("SplitAmt")
            '        TotalSplitAmount = TotalSplitAmount + CDbl(drcust("SplitAmt"))
            '        Dr1("SplitPer") = Math.Round((drcust("SplitAmt") / CDbl(CashSummary.CtrllblNetAmt.Text)) * 100, 2)
            '        Dr1("CREATEDAT") = clsAdmin.SiteCode
            '        Dr1("CREATEDBY") = clsAdmin.UserCode
            '        Dr1("CREATEDON") = DateTime.Now()
            '        Dr1("UPDATEDAT") = clsAdmin.SiteCode
            '        Dr1("UPDATEDBY") = clsAdmin.UserCode
            '        Dr1("UPDATEDON") = DateTime.Now()
            '        Dr1("Status") = True
            '        DtSplitBillCustDetail.Rows.Add(Dr1)
            '    End If
            'Next
            'If ChkCustomer.Checked = True Then
            '    If TotalSplitAmount <> CDbl(CashSummary.CtrllblNetAmt.Text) Then
            '        ShowMessage("Split bill amount should be equal to bill amount", getValueByKey("CLAE05"))
            '        Exit Sub
            '    End If
            'End If
            'DtSplitBillCustDetail.TableName = "CashMemoSplitDtl"
            'If objCm.SaveSplitBillDtl(DtSplitBillCustDetail) Then
            Dim DSTaxInvoicedtl = obj.GetPCTaxInvoiceDetailsSplitBill(clsAdmin.SiteCode, txtDocNumber.Text, False)
            ObjCashMemoPrint.GeneratePCTaxInvoicSplitBillPrint(DSTaxInvoicedtl, txtDocNumber.Text.ToString.Trim, clsAdmin.SiteCode, clsDefaultConfiguration.DayCloseReportPath, False)

            If Not DSTaxInvoicedtl.Tables(7) Is Nothing AndAlso DSTaxInvoicedtl.Tables(7).Rows.Count > 0 Then
                Dim DSTaxInvoicedtl1 = obj.GetPCTaxInvoiceDetailsSplitBill(clsAdmin.SiteCode, txtDocNumber.Text, True)
                ObjCashMemoPrint.GeneratePCTaxInvoicSplitBillPrint(DSTaxInvoicedtl1, txtDocNumber.Text.ToString.Trim, clsAdmin.SiteCode, clsDefaultConfiguration.DayCloseReportPath, True)
            End If
            ' End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
End Class