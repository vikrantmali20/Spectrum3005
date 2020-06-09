
Imports System.IO
Imports SpectrumBL

Public Class frmSalesOrderNotifiactionPopup

    Public id As Integer
    Private _dtSalesOrder As DataTable
    Dim objclsCommon As New clsCommon

    Public Property dtSalesOrder() As DataTable
        Get
            Return _dtSalesOrder
        End Get
        Set(ByVal value As DataTable)
            _dtSalesOrder = value
        End Set
    End Property
    Private _PopupType As String
    Public Property PopupType() As String
        Get
            Return _PopupType
        End Get
        Set(ByVal value As String)
            _PopupType = value
        End Set
    End Property
    Public Shared _SlsOrderNo As String = ""
    Public Shared Property SlsOrderNo() As String
        Get
            Return _SlsOrderNo
        End Get
        Set(value As String)
            _SlsOrderNo = value
        End Set
    End Property


    Private Sub frmSalesOrderNotifiactionPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
            gridSetting()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Public Sub gridSetting()
        Dim rowcount As Integer = dtSalesOrder.Rows.Count



        For i As Integer = 0 To rowcount

            grdPopUp.AutoSizeRow(i)
            lblHeading.Text = "Near to Delivery Orders notification"
            Me.Text = "Near to Delivery Orders notification"
            grdPopUp.DataSource = dtSalesOrder
            grdPopUp.Cols("SaleOrderNumber").Caption = "Sale Order Number"
            grdPopUp.Cols("SaleOrderNumber").Width = 135
            grdPopUp.Cols("CustomerDetails").Caption = "Customer Details"
            grdPopUp.Cols("CustomerDetails").Width = 180
            grdPopUp.Styles("Fixed").WordWrap = True
            grdPopUp.Cols("CustomerAddress").Caption = "Customer Address"
            grdPopUp.Cols("CustomerAddress").Width = 280
            grdPopUp.Styles("Fixed").WordWrap = True
            grdPopUp.Cols("SODate").Caption = "SO Date"
            grdPopUp.Cols("SODate").Width = 100
            grdPopUp.Styles("Fixed").WordWrap = True
            grdPopUp.Cols("SODeliveryDateTime").Caption = "SO Delivery Date & Time"
            grdPopUp.Cols("SODeliveryDateTime").Width = 160
            grdPopUp.Styles("Fixed").WordWrap = True
            grdPopUp.Cols("SODeliveryDateTime").Format = "G"
            ' grdPopUp.Rows(1).Height = 2 * grdPopUp.Rows.DefaultSize
            grdPopUp.Styles("Normal").WordWrap = True
            grdPopUp.Cols("SOTotalAmount").Caption = "SO Total Amount"
            grdPopUp.Cols("SOTotalAmount").Width = 80
            grdPopUp.Styles("Fixed").WordWrap = True


            grdPopUp.Cols("PaidAmount").Caption = "Paid Amount"
            grdPopUp.Cols("PaidAmount").Format = "0.00"
            grdPopUp.Cols("BalanceAmount").Caption = "Balance Amount"
            grdPopUp.Cols("BalanceAmount").Width = 80
            grdPopUp.Styles("Fixed").WordWrap = True
            grdPopUp.Cols("BalanceAmount").Format = "0.00"

            'grdPopUp.Cols("ElapsedDays").Caption = "Dues In Days"
            'grdPopUp.Cols("ElapsedDays").Width = 40
            'grdPopUp.Styles("Fixed").WordWrap = True


            grdPopUp.Cols("BalanceAmount").TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter

        Next
        grdPopUp.Rows(0).Height = 2 * grdPopUp.Rows.DefaultSize
       

    End Sub
    Public Function Themechange() As String
        Me.BackColor = Color.FromArgb(76, 76, 76)
        Me.grdPopUp.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.grdPopUp.Styles.Highlight.BackColor = Color.FromArgb(153, 255, 255)
        Me.grdPopUp.Styles.Highlight.ForeColor = Color.Black
        Me.grdPopUp.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdPopUp.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        Me.grdPopUp.Rows.MinSize = 26
        Me.grdPopUp.AutoResize = True

        Me.grdPopUp.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        Me.grdPopUp.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPopUp.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPopUp.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPopUp.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPopUp.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdPopUp.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.grdPopUp.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPopUp.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdPopUp.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdPopUp.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.lblHeading.BackColor = Color.Transparent
        Me.lblHeading.BorderStyle = BorderStyle.None
        Me.lblHeading.ForeColor = Color.White
        Me.lblHeading.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.lblHeading.Text = Me.lblHeading.Text.ToUpper()
    End Function

    Private Sub frmSalesOrderNotifiactionPopup_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            dtSalesOrder = Nothing
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If
    End Sub

    Private Sub cmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExport.Click


        Try
            ' Dim dt As DataTable = dtSalesOrder

            Dim filedlg As New SaveFileDialog()
            filedlg.DefaultExt = "xlsx"
            filedlg.Filter = "Excel File|*.xlsx"

            filedlg.FileName = "Near to Delivery Orders notification" & DateTime.Now.ToString("dd-MM-yyyy_HH_mm_ss") & ".xlsx"
            Dim result As DialogResult = filedlg.ShowDialog

            If result = Windows.Forms.DialogResult.OK And filedlg.FileName <> "" Then

                Dim vs As C1.Win.C1FlexGrid.VisualStyle
                Dim ps As New System.Drawing.Printing.PrinterSettings

                vs = grdPopUp.VisualStyle
                grdPopUp.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom

                For Each pa As System.Drawing.Printing.PaperSize In ps.PaperSizes
                    If pa.PaperName = "A4″ Then" Then
                        ps.DefaultPageSettings.PaperSize = pa
                        Exit For
                    End If
                Next

                Dim tmp As C1.Win.C1FlexGrid.DrawModeEnum = grdPopUp.DrawMode
                grdPopUp.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.Normal

                grdPopUp.SaveExcel(filedlg.FileName, "Sheet1", CType(C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells Or C1.Win.C1FlexGrid.FileFlags.VisibleOnly Or C1.Win.C1FlexGrid.FileFlags.SaveMergedRanges, C1.Win.C1FlexGrid.FileFlags), ps)
                ' grdPopUp.SaveExcel(filedlg.FileName, "Sheet1", dt ), ps)

                grdPopUp.DrawMode = tmp
                grdPopUp.VisualStyle = vs
                vs = Nothing
                ps = Nothing

                id = System.Diagnostics.Process.Start(filedlg.FileName).Id
            End If
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub


    Private Sub grdPopUp_DoubleClick(sender As Object, e As EventArgs) Handles grdPopUp.DoubleClick
        If grdPopUp.Rows.Count > 0 Then
            _SlsOrderNo = grdPopUp.Item(grdPopUp.Row, "SaleOrderNumber")
            'Dim childform
            If clsDefaultConfiguration.IsNewSalesOrder Then
                'childform = New Spectrum.frmPCNSalesOrderUpdate
                Dim childform As New Spectrum.frmPCNSalesOrderUpdate

                'childform.SalesORderNumberFromPopup = _SlsOrderNo
                'childform.EditCallFromPopup = True
                Try
                    If childform.Name <> String.Empty Then
                        childform.IsBookingEdit = True
                        'MDISpectrum.ShowChildForm(childform, True)

                        ' childform.Size = New Size(1000, 900)
                        childform.Size = New System.Drawing.Size(1340, 700)
                        childform.StartPosition = FormStartPosition.CenterScreen

                        childform.ShowDialog()
                        childform.BringToFront()
                        UpdatedSalesOrderData()

                    End If

                Catch ex As Exception
                    childform.Close()
                End Try

            Else
                'childform = New Spectrum.frmNSalesOrderUpdate
                Dim childform As New Spectrum.frmNSalesOrderUpdate
                childform.SalesORderNumberFromPopup = _SlsOrderNo
                childform.EditCallFromPopup = True
                Try
                    If childform.Name <> String.Empty Then
                        childform.IsBookingEdit = True
                        'MDISpectrum.ShowChildForm(childform, True)

                        ' childform.Size = New Size(1000, 900)
                        childform.Size = New System.Drawing.Size(1359, 709)
                        childform.StartPosition = FormStartPosition.CenterScreen

                        childform.ShowDialog()
                        childform.BringToFront()
                        UpdatedSalesOrderData()

                    End If

                Catch ex As Exception
                    childform.Close()
                End Try

            End If



        End If
    End Sub
    Private Sub UpdatedSalesOrderData()
        dtSalesOrder = objclsCommon.GetSalesOrderPopupDetails(clsAdmin.SiteCode)
        gridSetting()
    End Sub
End Class
