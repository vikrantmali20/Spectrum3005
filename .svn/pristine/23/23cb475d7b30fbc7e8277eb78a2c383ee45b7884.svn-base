Imports Spectrum
Imports SpectrumBL
Imports C1.Win.C1FlexGrid
Imports System.Text
Imports SpectrumPrint

Public Class frmDinInReprint


    Dim OrderKotDtls As New DataTable
    Dim objClscm As New clsCashMemo
    Dim NewKotDtls As New DataTable
    Dim kotno As String
    Dim str As String
    Dim desc As New StringBuilder
    Dim reason As Object



    Public _currentReprintTableNo As String

    Public Property currentReprintTableNo As String
        Get
            Return _currentReprintTableNo
        End Get
        Set(ByVal value As String)
            _currentReprintTableNo = value
        End Set
    End Property

    Public _currentReprintBillNo As String
    Public Property currentReprintBillNo As String
        Get
            Return _currentReprintBillNo
        End Get
        Set(ByVal value As String)
            _currentReprintBillNo = value
        End Set
    End Property

    Private Sub frmDinInReprint_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ThemeChange()
            End If
            lblTableNoValue.Text = currentReprintTableNo
            lblOrderNoValue.Text = currentReprintBillNo

            OrderKotDtls = objClscm.GetOrderDetails(currentReprintBillNo)
            NewKotDtls = objClscm.GetReprintItemsInfo()
            NewKotDtls.Rows.Clear()
            kotno = OrderKotDtls.Rows(0)("KOTNumber").ToString
            Dim totalRows As Integer = OrderKotDtls.Rows.Count
            Dim startCnt As Integer = 1
            For Each dr As DataRow In OrderKotDtls.Rows
                If Not (kotno = dr("KOTNumber").ToString()) Then
                    Dim newRow As DataRow = NewKotDtls.NewRow()
                    newRow("KOTNumber") = kotno
                    newRow("Description") = str
                    newRow("Reason") = ""
                    NewKotDtls.Rows.Add(newRow)
                    kotno = dr("KOTNumber").ToString()
                    'desc.Clear()
                    str = String.Empty
                End If

                str += dr("Description") & "-" & dr("Quantity") & vbCrLf
                If startCnt = totalRows Then
                    Dim newRow As DataRow = NewKotDtls.NewRow()
                    newRow("KOTNumber") = kotno
                    newRow("Description") = str
                    newRow("Reason") = ""
                    NewKotDtls.Rows.Add(newRow)
                    str = String.Empty
                    'desc.Clear()
                End If
                startCnt = startCnt + 1
            Next


            ShowDetail()
            dgReprint.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
            dgReprint.ScrollBars = ScrollBars.Both
            dgReprint.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns
            dgReprint.SelectionMode = SelectionModeEnum.Row
            dgReprint.AutoSizeRows()
            dgReprint.Focus()
            dgReprint.Select()
            'For Each rowTemp As C1.Win.C1FlexGrid.Row In dgReprint.Rows
            '    rowTemp.AllowResizing = True
            'Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub GridSettings()
        Try

            dgReprint.Cols("IsReprint").Width = 30
            dgReprint.Cols("IsReprint").Caption = ""

            dgReprint.Cols("KOTNumber").Width = 100
            dgReprint.Cols("KOTNumber").AllowEditing = False
            dgReprint.Cols("KOTNumber").AllowMerging = True
            dgReprint.Cols("KOTNumber").Caption = getValueByKey("frmDinInReprint.dgReprint.kotno")

            dgReprint.Cols("Description").Width = 320
            dgReprint.Cols("Description").AllowEditing = False
            dgReprint.Cols("Description").AllowMerging = True
            dgReprint.Cols("Description").Caption = getValueByKey("frmDinInReprint.dgReprint.articledescription")

            dgReprint.Cols("Reason").Width = 50
            dgReprint.Cols("Reason").AllowEditing = True
            dgReprint.Cols("Reason").Caption = getValueByKey("frmDinInReprint.dgReprint.reason")
            dgReprint.Cols("Reason").TextAlign = TextAlignEnum.LeftTop
            dgReprint.Rows(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            '--- Merging cols
            'dgReprint.AllowMerging = AllowMergingEnum.Free
            ' Merge values in column 1.
            'dgReprint.Cols("SELECT").AllowMerging = True
            'dgReprint.Cols("KOTNumber").AllowMerging = True
            '  dgReprint.Cols(2).AllowMerging = True
            'dgReprint.Cols("Reason").AllowMerging = True

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub ShowDetail()
        Try
            'dgReprint.DataSource = OrderKotDtls
            dgReprint.DataSource = NewKotDtls
            GridSettings()
            SetCulture(Me, Me.Name)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub ctrlReprint_Click(sender As Object, e As EventArgs) Handles ctrlReprint.Click
        Try
            Dim selectedRows() As DataRow = NewKotDtls.Select("IsReprint=True", "", DataViewRowState.CurrentRows)
            If (selectedRows.Count = 0) Then

                ShowMessage(getValueByKey("DIN008"), "DIN008-" & getValueByKey("DIN028"))
                ' ShowMessage("Please select at least one KOT for reprint", " " & getValueByKey("CLAE04"))
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If
            
            Dim str As String
            Dim IsUpdate As Boolean = False
            Dim dt As New DataTable
            Dim reprintData As New DataTable
            Dim kotNumber As String
            reprintData = objClscm.GetReprintStructure()
            reprintData.Rows.Clear()
            dt = NewKotDtls.Select("IsReprint=True").CopyToDataTable()
            For Each selectedrow As DataRow In dt.Rows
                str = selectedrow("Reason").ToString().Trim
                If str = String.Empty Then
                    ShowMessage(getValueByKey("DIN010"), "DIN010- " & getValueByKey("DIN028"))
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                End If
            Next
            IsUpdate = objClscm.UpdateDineInReprintReason(dt, currentReprintBillNo)
            If IsUpdate = True Then
                For Each dtrow As DataRow In dt.Rows
                    kotNumber = dtrow("KOTNumber")

                    Dim reprintrow() = OrderKotDtls.Select("KOTNumber= '" & kotNumber & "'  ")
                    For i As Integer = 0 To reprintrow.Count - 1
                        Dim newRow As DataRow = reprintData.NewRow()
                        newRow("EAN") = reprintrow(i)("EAN")
                        newRow("Discription") = reprintrow(i)("Description")
                        newRow("ArticleCode") = reprintrow(i)("ItemCode")
                        newRow("KotQuantity") = reprintrow(i)("Quantity")
                        newRow("Status") = reprintrow(i)("Status")
                        newRow("KotNo") = reprintrow(i)("KOTNumber")
                        newRow("Remark") = reprintrow(i)("Remark")
                        newRow("Reason") = dtrow("Reason")
                        reprintData.Rows.Add(newRow)
                    Next
                Next

                Dim objPrint As New clsCashMemoPrint("", False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving) '0000413
                objPrint.TableNameForDineIn = clsDefaultConfiguration.TableNameForDineIn
                Dim ErrorMsg As String = ""
                objPrint.DineInKOTPrint("REPRINT", ErrorMsg, Nothing, reprintData, currentReprintBillNo, currentReprintTableNo, clsAdmin.SiteCode)
                If ErrorMsg <> String.Empty Then
                    ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
                End If
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            LogException(ex)
        End Try

    End Sub

    Private Sub CtrlCancel_Click(sender As Object, e As EventArgs) Handles CtrlCancel.Click
        Try
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub dgReprint_KeyDown(sender As Object, e As KeyEventArgs) Handles dgReprint.KeyDown
        'If (dgReprint.Rows.Count > 1) Then
        '    dgReprint.Select(dgReprint.SelectionMode, 2)
        'End If
    End Sub

    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        lblTableNo.ForeColor = Color.White
        lblTableNoValue.ForeColor = Color.White
        lblOrderNo.ForeColor = Color.White
        lblOrderNoValue.ForeColor = Color.White
        dgReprint.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgReprint.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgReprint.Rows.MinSize = 25
        dgReprint.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        dgReprint.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgReprint.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgReprint.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgReprint.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgReprint.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        ctrlReprint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        ctrlReprint.BackColor = Color.Transparent
        ctrlReprint.BackColor = Color.FromArgb(0, 107, 163)
        ctrlReprint.ForeColor = Color.FromArgb(255, 255, 255)
        ctrlReprint.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        ctrlReprint.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        ctrlReprint.FlatStyle = FlatStyle.Flat
        ctrlReprint.FlatAppearance.BorderSize = 0
        ctrlReprint.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        ctrlReprint.Size = New Size(85, 30)
        CtrlCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlCancel.BackColor = Color.Transparent
        CtrlCancel.BackColor = Color.FromArgb(0, 107, 163)
        CtrlCancel.ForeColor = Color.FromArgb(255, 255, 255)
        CtrlCancel.Size = New Size(71, 30)
        CtrlCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlCancel.FlatStyle = FlatStyle.Flat
        CtrlCancel.FlatAppearance.BorderSize = 0
        CtrlCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
    End Function
End Class