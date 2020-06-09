Imports SpectrumBL
Imports System.Data
Imports System.Data.SqlClient
Imports SpectrumPrint
Public Class FrmVoidKot
    Dim objCM As New clsCashMemo
    Dim objCMCommon As New clsCommon
    Dim ReturnResult As New DataTable
    Dim _currenttableNo As String
    Public Property currenttableNo As String
        Get
            Return _currenttableNo
        End Get
        Set(ByVal value As String)
            _currenttableNo = value
        End Set
    End Property

    Dim _currentBillNo As String
    Public Property currentBillNo As String
        Get
            Return _currentBillNo
        End Get
        Set(ByVal value As String)
            _currentBillNo = value
        End Set
    End Property
  

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmdVoidKot_Click(sender As Object, e As EventArgs) Handles cmdVoidKot.Click
        Try
            Dim isUpdated As Integer = 0
            Dim dt As New DataTable
            For Each drresult As DataRow In ReturnResult.Rows
                If (drresult("VoidQuantity") = 0) Then
                    If (drresult("Reason") <> Nothing) Then
                        ShowMessage(getValueByKey("DIN001") + drresult("ArticleCode") + " of Kot No." + drresult("KotNO"), "DIN001 - " & getValueByKey("DIN002"))
                        'ShowMessage("Enter Quantity for Item Code  " + drresult("ArticleCode") + " of Kot No." + drresult("KotNO"), "Void Quantity Information")
                        Exit Sub
                    End If
                Else
                    isUpdated = isUpdated + 1
                End If
            Next
            If isUpdated = 0 Then
                ShowMessage(getValueByKey("DIN003"), "DIN003 - " & getValueByKey("DIN002"))
                'ShowMessage("Update Void Quantity", "Void Quantity Information")
                Exit Sub
            End If
            dt = ReturnResult.Select("VoidQuantity>0").CopyToDataTable()
            For Each dr As DataRow In dt.Rows
                If (dr("VoidQuantity") > 0) Then

                    If (dr("Reason")) = Nothing Or String.IsNullOrWhiteSpace(dr("Reason")) Then
                        ShowMessage(getValueByKey("DIN004") + dr("ArticleCode") + " of Kot No." + dr("KotNO"), "DIN004 -" & getValueByKey("DIN002"))
                        Exit Sub
                    End If
                End If

            Next
            If dt.Rows.Count > 0 Then
                If objCM.UpdateDineInKotQty(dt, currentBillNo, clsAdmin.UserName, clsAdmin.SiteCode) Then
                    Dim objPrint As New clsCashMemoPrint("", False, False, clsDefaultConfiguration.SalesPersonApplicable, clsDefaultConfiguration.TaxDetailsRequired, clsDefaultConfiguration.PrintPreivewReq, "L40", dtPrinterInfo, clsDefaultConfiguration.IsDisplayTotalSaving) '0000413
                    objPrint.TableNameForDineIn = clsDefaultConfiguration.TableNameForDineIn
                    Dim ErrorMsg As String = ""
                    objPrint.DineInKOTPrint("VOID", ErrorMsg, Nothing, dt, currentBillNo, currenttableNo, clsAdmin.SiteCode)
                    If ErrorMsg <> String.Empty Then
                        ShowMessage(ErrorMsg, getValueByKey("CLAE05"))
                    End If
                    If objCMCommon.GetBillStatus(currentBillNo) = False Then
                        If objCMCommon.UpdateDinInStatus(clsAdmin.UserName, clsAdmin.SiteCode, currentBillNo) Then
                        End If
                    End If
                End If
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK

        Catch ex As Exception
            ShowMessage(False, "Void KOT Failed")
            LogException(ex)
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End Try
    End Sub

    Private Sub FrmVoidKot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lblOrderNoValue.Text = currentBillNo
            lblTableNoValue.Text = currenttableNo
            ReturnResult = objCM.GetDineInKotQty(currentBillNo)
            ShowDetail()
            With dgOrders
                .VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
                .ScrollBars = ScrollBars.Both
                .AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns
                .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
            End With
            dgOrders.Focus()
            dgOrders.Select()
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ThemeChange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub GridSettings()
        Try

            dgOrders.Cols("ArticleCode").Width = 140
            dgOrders.Cols("ArticleCode").AllowEditing = False
            dgOrders.Cols("ArticleCode").Caption = getValueByKey("FrmVoidKot.dgOrders.itemcode")
            dgOrders.Cols("ArticleCode").TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            dgOrders.Cols("ArticleCode").TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            dgOrders.Cols("DISCRIPTION").Width = 195
            dgOrders.Cols("DISCRIPTION").AllowEditing = False
            dgOrders.Cols("DISCRIPTION").Caption = getValueByKey("FrmVoidKot.dgOrders.description")
            dgOrders.Cols("DISCRIPTION").TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            dgOrders.Cols("DISCRIPTION").TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            dgOrders.Cols("EAN").Visible = False
            dgOrders.Cols("KotNO").Width = 100
            dgOrders.Cols("KotNO").AllowEditing = False
            dgOrders.Cols("KotNO").Caption = getValueByKey("FrmVoidKot.dgOrders.kotno")
            dgOrders.Cols("KotNO").TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
            dgOrders.Cols("KotNO").TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter

            dgOrders.Cols("Quantity").Width = 100
            dgOrders.Cols("Quantity").AllowEditing = False
            dgOrders.Cols("Quantity").TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
            dgOrders.Cols("Quantity").TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter

            dgOrders.Cols("VoidQuantity").Width = 100
            dgOrders.Cols("VoidQuantity").AllowEditing = True
            dgOrders.Cols("VoidQuantity").Caption = getValueByKey("FrmVoidKot.dgOrders.voidquantity")
            dgOrders.Cols("VoidQuantity").TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
            dgOrders.Cols("VoidQuantity").TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
            dgOrders.Cols("VoidQuantity").Format = "0.000"
            dgOrders.Cols("VoidQuantity").DataType = Type.GetType("System.Decimal")

            dgOrders.Cols("Reason").Width = 110
            dgOrders.Cols("Reason").AllowEditing = True
            dgOrders.Cols("Reason").TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            dgOrders.Cols("Reason").TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            dgOrders.Cols("Remark").Visible = False

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub ShowDetail()
        Try
            dgOrders.DataSource = ReturnResult
            GridSettings()
            SetCulture(Me, Me.Name)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub dgOrders_AfterEdit(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgOrders.AfterEdit
        If dgOrders.Row = -1 Then Exit Sub
        Dim currentrow = dgOrders.Row
        Dim currentcell = e.Col
        Dim dgreason = dgOrders.Item(currentrow, "Reason")
        If dgOrders.Cols(currentcell).Name = "VoidQuantity" Then
            Try

                Dim voidQty As Double = IIf(dgOrders.Item(currentrow, "VoidQuantity") Is DBNull.Value, 0, dgOrders.Item(currentrow, "VoidQuantity"))
                Dim Qty As Double = dgOrders.Item(currentrow, "Quantity")

                If Not (voidQty >= 0) Then
                    ShowMessage(getValueByKey("DIN006"), "DIN006 - " & getValueByKey("DIN002"))
                    'ShowMessage("Void Quantity cannot less than 0.", "Void Quantity Information")
                    dgOrders.Item(currentrow, "VoidQuantity") = 0
                ElseIf (voidQty > Qty) Then
                    ShowMessage(getValueByKey("DIN007"), "DIN006 - " & getValueByKey("DIN002"))
                    'ShowMessage("Void Quantity cannot be more than Quantity", "Void Quantity Information")
                    dgOrders.Item(currentrow, "VoidQuantity") = 0
                ElseIf (voidQty = Nothing) Then
                    dgOrders.Item(currentrow, "VoidQuantity") = 0
                End If


            Catch ex As Exception
                LogException(ex)
                ShowMessage(ex.Message, getValueByKey("CLAE05"))
            End Try
        End If
    End Sub

    Private Sub dgOrders_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dgOrders.KeyPress
        Try
            If Asc(e.KeyChar) <> 8 Then
                If Asc(e.KeyChar) = 127 Then
                    e.Handled = True
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Function Themechange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)
        lblTableNoText.ForeColor = Color.White
        lblTableNoValue.ForeColor = Color.White
        lblOrderNoText.ForeColor = Color.White
        lblOrderNoValue.ForeColor = Color.White
        dgOrders.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        dgOrders.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        dgOrders.Rows.MinSize = 25
        dgOrders.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        dgOrders.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgOrders.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgOrders.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        dgOrders.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        cmdVoidKot.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdVoidKot.BackColor = Color.Transparent
        cmdVoidKot.BackColor = Color.FromArgb(0, 107, 163)
        cmdVoidKot.ForeColor = Color.FromArgb(255, 255, 255)
        cmdVoidKot.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdVoidKot.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdVoidKot.FlatStyle = FlatStyle.Flat
        cmdVoidKot.FlatAppearance.BorderSize = 0
        cmdVoidKot.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdCancel.BackColor = Color.Transparent
        cmdCancel.BackColor = Color.FromArgb(0, 107, 163)
        cmdCancel.ForeColor = Color.FromArgb(255, 255, 255)
        cmdCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdCancel.FlatStyle = FlatStyle.Flat
        cmdCancel.FlatAppearance.BorderSize = 0
        cmdCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

    End Function
End Class