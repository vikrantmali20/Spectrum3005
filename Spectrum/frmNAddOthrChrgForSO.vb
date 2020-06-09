Imports SpectrumBL
Public Class frmNAddOthrChrgForSO
    Public objSO As New clsSalesOrder
    Dim _dtOtherCharge As DataTable
    Dim drOtherCharge As DataRow
    Dim _SoNumber As String
    Dim _cancelOthercharges As Boolean = False
    Public ReadOnly Property CancelOthercharges() As Boolean
        Get
            Return _cancelOthercharges
        End Get
    End Property
    Public WriteOnly Property SalesOrderNo() As String
        Set(ByVal value As String)
            _SoNumber = value
        End Set
    End Property
    Public Property dtOtherCharge() As DataTable
        Get
            Return _dtOtherCharge
        End Get
        Set(ByVal value As DataTable)
            _dtOtherCharge = value
        End Set
    End Property

    ''' <summary>
    ''' Load Add Other Charges for Sales Order
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmAddOthrChrgForSO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            If _dtOtherCharge Is Nothing Then
                _dtOtherCharge = objSO.GetDtOtherCharge("")
            End If
            grdOtherCharge.DataSource = _dtOtherCharge

            For colno = 0 To grdOtherCharge.Cols.Count - 1
                If grdOtherCharge.Cols(colno).Name <> "ChargeName" _
                    AndAlso grdOtherCharge.Cols(colno).Name <> "ChargeAmount" _
                    AndAlso grdOtherCharge.Cols(colno).Name <> "TaxName" _
                    AndAlso grdOtherCharge.Cols(colno).Name <> "TaxAmt" Then
                    HideColumns(grdOtherCharge, False, grdOtherCharge.Cols(colno).Name)
                End If
            Next
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                ThemeChange()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
        SetCulture(Me, Me.Name)

    End Sub

    ''' <summary>
    ''' Pass values to parrent form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click

        Try
            For Each dr As DataRow In _dtOtherCharge.Rows
                If dr("ChargeName") Is DBNull.Value AndAlso (dr("ChargeAmount") Is DBNull.Value) AndAlso dr("TaxAmt") Is DBNull.Value AndAlso dr("TaxName") Is DBNull.Value Then
                    dr.Delete()
                    'dr.AcceptChanges()
                Else
                    If dr.RowState <> DataRowState.Deleted Then
                        If Not (dr("ChargeName") Is DBNull.Value Or dr("ChargeName").ToString.Trim = "") AndAlso (dr("ChargeAmount") Is DBNull.Value) Then
                            'ShowMessage("Please enter Other Charge Amount in " & grdOtherCharge.Row & " Row", "Other Charges Information")
                            ShowMessage(String.Format(getValueByKey("AOCSO001"), grdOtherCharge.Row), "AOCSO001 - " & getValueByKey("CLAE04"))
                            Exit Sub
                        ElseIf (dr("ChargeName") Is DBNull.Value Or dr("ChargeName").ToString.Trim = "") AndAlso Not (dr("ChargeAmount") Is DBNull.Value) Then
                            'ShowMessage("Please enter Other Charge Name in " & grdOtherCharge.Row & " Row", "Other Charges Information")
                            ShowMessage(String.Format(getValueByKey("AOCSO002"), grdOtherCharge.Row), "AOCSO002 - " & getValueByKey("CLAE04"))
                            Exit Sub
                        End If
                        ' If TaxDetails name have value then tax Amt must have value 
                        If (dr("TaxName") IsNot DBNull.Value) AndAlso Not String.IsNullOrEmpty(dr("TaxName")) Then
                            If dr("TaxAmt") Is DBNull.Value Then
                                ShowMessage(String.Format(getValueByKey("AOCSO003"), grdOtherCharge.Row), "AOCSO003 - " & getValueByKey("CLAE04"))
                                Exit Sub
                            ElseIf Val(dr("TaxAmt")) <= 0 Then
                                ShowMessage(String.Format(getValueByKey("AOCSO006"), grdOtherCharge.Row), "AOCSO006 - " & getValueByKey("CLAE04"))
                                Exit Sub
                            End If
                        End If

                        If (dr("TaxAmt") IsNot DBNull.Value) AndAlso Val(dr("TaxAmt")) > 0 Then
                            If dr("TaxName") Is DBNull.Value Then
                                ShowMessage(String.Format(getValueByKey("AOCSO003"), grdOtherCharge.Row), "AOCSO003 - " & getValueByKey("CLAE04"))
                                Exit Sub
                            ElseIf String.IsNullOrEmpty(dr("TaxName")) Then
                                ShowMessage(String.Format(getValueByKey("AOCSO003"), grdOtherCharge.Row), "AOCSO003 - " & getValueByKey("CLAE04"))
                                Exit Sub
                            End If
                        ElseIf Val(dr("TaxAmt")) < 0 Then
                            ShowMessage(String.Format(getValueByKey("AOCSO006"), grdOtherCharge.Row), "AOCSO006 - " & getValueByKey("CLAE04"))
                            Exit Sub
                        End If
                        ' If TaxDetails amt have value then tax name must have value 
                        ' If TaxDetails amt not have value then tax name must not have value 

                        'If (dr("TaxName") IsNot DBNull.Value) AndAlso dr("TaxAmt") Is DBNull.Value Then
                        '    'ShowMessage("Please enter Tax Amount in " & grdOtherCharge.Row & " Row", "Other Charges Information")
                        '    If Not String.IsNullOrEmpty(dr("TaxName")) AndAlso Val(dr("TaxAmt")) <= 0 Then
                        '        ShowMessage(String.Format(getValueByKey("AOCSO005"), grdOtherCharge.Row), "AOCSO005 - " & getValueByKey("CLAE04"))
                        '        Exit Sub
                        '    ElseIf (dr("TaxName") Is DBNull.Value) AndAlso (dr("TaxAmt") IsNot DBNull.Value) Then
                        '        'ShowMessage("Please enter Tax Name in " & grdOtherCharge.Row & " Row", "Other Charges Information")
                        '        If Val(dr("TaxAmt")) > 0 And String.IsNullOrEmpty(dr("TaxName")) Then
                        '            ShowMessage(String.Format(getValueByKey("AOCSO005"), grdOtherCharge.Row), "AOCSO005 - " & getValueByKey("CLAE04"))
                        '            Exit Sub
                        '        End If
                        '        If dr("TaxAmt") <> 0 Then
                        '            ShowMessage(String.Format(getValueByKey("AOCSO005"), grdOtherCharge.Row), "AOCSO005 - " & getValueByKey("CLAE04"))
                        '            Exit Sub
                        '        End If

                        '    End If
                        'End If
                        ''---Changed by rama on 10-jun-2009
                        If dr("ChargeAmount") <= 0 AndAlso dr("chargeamount") IsNot DBNull.Value Then
                            'ShowMessage("Please enter Other Charge Amount greater than zero in " & grdOtherCharge.Row & " Row", "Other Charges Information")
                            ShowMessage(String.Format(getValueByKey("AOCSO005"), grdOtherCharge.Row), "AOCSO005 - " & getValueByKey("CLAE04"))
                            Exit Sub
                        End If

                        'If (IIf(IsDBNull(dr("TaxAmt")), 0, dr("TaxAmt")) <= 0) AndAlso Not (dr("TaxName") Is DBNull.Value) Then
                        '    'ShowMessage("Please enter Tax Amount greater than zero in " & grdOtherCharge.Row & " Row", "Other Charges Information")
                        '    ShowMessage(String.Format(getValueByKey("AOCSO006"), grdOtherCharge.Row), "AOCSO006 - " & getValueByKey("CLAE04"))
                        '    Exit Sub
                        'End If
                        ''--
                    End If
                End If
            Next
        Catch ex As Exception
            'ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try

        Me.Close()
    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        _cancelOthercharges = True
        Me.Close()
    End Sub

    ''' <summary>
    ''' Add New Row for add Other Charges
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        Try
            Dim dr As DataRow = _dtOtherCharge.NewRow()
            dr("SiteCode") = clsAdmin.SiteCode
            dr("FinYear") = clsAdmin.Financialyear
            dr("SaleOrderNumber") = _SoNumber
            Dim maxno As Object = _dtOtherCharge.Compute("MAX(SerailNo)", "")
            If maxno Is DBNull.Value Then maxno = 0
            If Not maxno Is Nothing Then
                dr("SerailNo") = Convert.ToInt32(maxno) + 1
            Else
                dr("SerailNo") = 0
            End If
            _dtOtherCharge.Rows.Add(dr)
            grdOtherCharge.DataSource = _dtOtherCharge
        Catch ex As Exception
            LogException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Delete Current Row in Other Charges
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            'Dim dr As DataRow = _dtOtherCharge.Rows(grdOtherCharge.Row - 1)
            '_dtOtherCharge.Rows.Remove(dr)
            If grdOtherCharge.Rows(grdOtherCharge.Row).Item("ChargeName") Is Nothing Then
                grdOtherCharge.Rows(grdOtherCharge.Row).Item("ChargeName") = String.Empty
            End If
            If _dtOtherCharge.Select("ChargeName='" & grdOtherCharge.Rows(grdOtherCharge.Row).Item("ChargeName").ToString().Trim() & "'").Count = 0 Then
                Dim dr As DataRow = _dtOtherCharge.Rows(grdOtherCharge.Row - 1)
                _dtOtherCharge.Rows.Remove(dr)
                Exit Sub
            End If
            For Each drRow As DataRow In _dtOtherCharge.Select("ChargeName='" & grdOtherCharge.Rows(grdOtherCharge.Row).Item("ChargeName").ToString().Trim() & "'")
                drRow.Delete()
            Next
            'If Not (grdOtherCharge.Row = 0) Then
            '    grdOtherCharge.Rows.Remove(grdOtherCharge.Row)
            'End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowIcon = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
    End Sub

    Public Function ThemeChange()
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)


        Me.grdOtherCharge.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        Me.grdOtherCharge.Styles.Highlight.BackColor = Color.FromArgb(153, 255, 255)
        Me.grdOtherCharge.Styles.Highlight.ForeColor = Color.Black
        Me.grdOtherCharge.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdOtherCharge.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        Me.grdOtherCharge.Rows.MinSize = 26
        Me.grdOtherCharge.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        Me.grdOtherCharge.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdOtherCharge.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdOtherCharge.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdOtherCharge.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdOtherCharge.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdOtherCharge.Styles.Fixed.ForeColor = Color.FromArgb(37, 37, 37)
        Me.grdOtherCharge.Styles.SelectedColumnHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdOtherCharge.Styles.SelectedRowHeader.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        Me.grdOtherCharge.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)
        Me.grdOtherCharge.Styles.SelectedRowHeader.BackColor = Color.FromArgb(177, 227, 253)

        Me.BtnAdd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.BtnAdd.BackColor = Color.Transparent
        Me.BtnAdd.BackColor = Color.FromArgb(0, 107, 163)
        Me.BtnAdd.ForeColor = Color.FromArgb(255, 255, 255)
        Me.BtnAdd.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.BtnAdd.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.BtnAdd.FlatStyle = FlatStyle.Flat
        Me.BtnAdd.FlatAppearance.BorderSize = 0
        Me.BtnAdd.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        Me.BtnAdd.TextAlign = ContentAlignment.MiddleCenter

        Me.BtnDelete.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.BtnDelete.BackColor = Color.Transparent
        Me.BtnDelete.BackColor = Color.FromArgb(0, 107, 163)
        Me.BtnDelete.ForeColor = Color.FromArgb(255, 255, 255)
        Me.BtnDelete.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.BtnDelete.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.BtnDelete.FlatStyle = FlatStyle.Flat
        Me.BtnDelete.FlatAppearance.BorderSize = 0
        Me.BtnDelete.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        Me.BtnDelete.TextAlign = ContentAlignment.MiddleCenter

        Me.BtnOK.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.BtnOK.BackColor = Color.Transparent
        Me.BtnOK.BackColor = Color.FromArgb(0, 107, 163)
        Me.BtnOK.ForeColor = Color.FromArgb(255, 255, 255)
        Me.BtnOK.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.BtnOK.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.BtnOK.FlatStyle = FlatStyle.Flat
        Me.BtnOK.FlatAppearance.BorderSize = 0
        Me.BtnOK.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        Me.BtnOK.TextAlign = ContentAlignment.MiddleCenter

        Me.BtnCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        Me.BtnCancel.BackColor = Color.Transparent
        Me.BtnCancel.BackColor = Color.FromArgb(0, 107, 163)
        Me.BtnCancel.ForeColor = Color.FromArgb(255, 255, 255)
        Me.BtnCancel.Font = New Font("Neo Sans", 8, FontStyle.Bold)
        Me.BtnCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        Me.BtnCancel.FlatStyle = FlatStyle.Flat
        Me.BtnCancel.FlatAppearance.BorderSize = 0
        Me.BtnCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 107, 163)
        Me.BtnCancel.TextAlign = ContentAlignment.MiddleCenter
    End Function

End Class
