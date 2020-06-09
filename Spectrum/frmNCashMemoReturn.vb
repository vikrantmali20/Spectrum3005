Imports SpectrumBL
''' <summary>
''' This Class is Used for CashMemo Return
''' </summary>
''' <CreatedBy>Rama Ranjan Jena</CreatedBy>
''' <UpdatedBy></UpdatedBy>
''' <UpdatedOn></UpdatedOn>
''' <remarks></remarks>
Public Class frmNCashMemoReturn
#Region "Global Variable For Class"
    Dim _ReturnResultSet As New DataTable
    Dim dtReason As DataTable
    Dim objReturn As New clsCashMemoReturn
    Dim StrReasons As String
    Public BillNo, Customer As String
    Dim FormNormalHeight, SpliterDistance As Integer
    Public GVBasedAricleReturnList As New Dictionary(Of String, String)
    Dim objCM As New clsCashMemo
    Dim RETUNTAMT As DataTable
#End Region
#Region "Public Property"
    ''' <summary>
    ''' Used For Returning Item Details to Main Cash Memo
    ''' </summary>
    ''' <value></value>
    ''' <UsedIn>CashMemo</UsedIn>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetResultData() As DataTable
        Get
            Return _ReturnResultSet
        End Get
    End Property
#End Region
#Region "Class Events"
    'Private Sub frmCashMemoReturn_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    '    GetSubString(strTitle, Me.Text, False)
    'End Sub

    Private Sub frmNCashMemoReturn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F12 Then
            ChangePrice()
            'If CheckInterTransactionAuth("PriceChange") = True Then
            '    Dim frm As New frmSpecialPrompt("Enter Price...")
            '    frm.ShowTextBox = True
            '    frm.ShowDialog()
            '    dgMainGrid.Rows(dgMainGrid.Row)("SellingPrice") = IIf(frm.GetResult Is Nothing, 1, frm.GetResult)
            '    dgMainGrid.Rows(dgMainGrid.Row)("GROSSAMT") = dgMainGrid.Rows(dgMainGrid.Row)("QUANTITY") * dgMainGrid.Rows(dgMainGrid.Row)("SellingPrice")
            '    dgMainGrid.Rows(dgMainGrid.Row)("NETAMOUNT") = dgMainGrid.Rows(dgMainGrid.Row)("QUANTITY") * dgMainGrid.Rows(dgMainGrid.Row)("SellingPrice")
            'End If
        End If
        If e.KeyCode = Keys.F2 Then
            ChangeQty()
            '    Dim frm As New frmSpecialPrompt("Enter Qty...")
            '    frm.ShowTextBox = True
            '    frm.ShowDialog()
            '    dgMainGrid.Rows(dgMainGrid.Row)("QUANTITY") = IIf(frm.GetResult Is Nothing, 1, frm.GetResult)
            '    dgMainGrid.Rows(dgMainGrid.Row)("GROSSAMT") = dgMainGrid.Rows(dgMainGrid.Row)("QUANTITY") * dgMainGrid.Rows(dgMainGrid.Row)("SellingPrice")
            '    dgMainGrid.Rows(dgMainGrid.Row)("NETAMOUNT") = dgMainGrid.Rows(dgMainGrid.Row)("QUANTITY") * dgMainGrid.Rows(dgMainGrid.Row)("SellingPrice")
        End If
        If e.KeyCode = Keys.F1 Then
            Dim objClsCommon As New clsCommon
            objClsCommon.DisplayHelpFile(ParentForm, "returns.htm")
        End If
    End Sub
    Private Sub frmCashMemoReturn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            FormNormalHeight = Me.Height
            SpliterDistance = SplitContainer1.SplitterDistance
            SetformLayout(1)
            AddHandler rbCashMemo.CheckedChanged, AddressOf BillTyp_change
            AddHandler rbBirthList.CheckedChanged, AddressOf BillTyp_change
            AddHandler rbSalesOrder.CheckedChanged, AddressOf BillTyp_change
            AddHandler rbWithoutBill.CheckedChanged, AddressOf BillTyp_change

            If clsDefaultConfiguration.WithoutBillAllowed = False Then
                rbWithoutBill.Enabled = False
            Else
                rbWithoutBill.Enabled = True
            End If

            Try
                dtReason = objReturn.GetReason("CMR")
                SizFooter.Visible = False
            Catch ex As Exception
            End Try
            SetCulture(Me, Me.Name)
            rbCashMemo.Enabled = True
            rbCashMemo.Checked = True
            If CheckAuthorisation(clsAdmin.UserCode, "BLMain") = False Then
                rbBirthList.Visible = False
            End If
            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
       
    End Sub
    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Try
            If Fp_Validate() = False Then Exit Sub
            If rbCashMemo.Checked = True Then
                If dtpCashmemoDate.Value Is DBNull.Value Then
                    ShowMessage(getValueByKey("CMR01"), "CMR01 - " & getValueByKey("CLAE04"))
                    dtpCashmemoDate.Focus()
                    Exit Sub
                End If
                If objReturn.ValidateCashMemo(txtOldCashMemoNo.Text, dtpCashmemoDate.Value, BillNo) = False Then
                    ShowMessage(getValueByKey("CMR02"), "CMR02 - " & getValueByKey("CLAE04"))
                    txtOldCashMemoNo.Focus()
                    Exit Sub
                End If
                _ReturnResultSet = objReturn.GetDetails(txtOldCashMemoNo.Text.Trim, clsAdmin.SiteCode)
            ElseIf rbSalesOrder.Checked = True Then
                Dim msg As String = ""
                _ReturnResultSet = objReturn.ValidateSO(txtOldCashMemoNo.Text.Trim, clsAdmin.SiteCode, msg)
                If msg <> "" Then
                    ShowMessage(msg, getValueByKey("CLAE04"))
                    txtOldCashMemoNo.Focus()
                    Exit Sub
                End If
            ElseIf rbBirthList.Checked = True Then
                Dim msg As String = ""
                _ReturnResultSet = objReturn.ValidateBL(txtOldCashMemoNo.Text.Trim, clsAdmin.SiteCode, msg)
                If msg <> "" Then
                    ShowMessage(msg, getValueByKey("CLAE04"))
                    txtOldCashMemoNo.Focus()
                    Exit Sub
                End If
            ElseIf rbWithoutBill.Checked = True Then
                If CheckInterTransactionAuth("WithoutBill", Nothing) Then
                    _ReturnResultSet = objReturn.GetDetails(0, clsAdmin.SiteCode)
                End If
            End If
            SetformLayout(2)
            If rbCashMemo.Checked = True Or rbBirthList.Checked = True Or rbSalesOrder.Checked = True Then
                If Not _ReturnResultSet Is Nothing AndAlso _ReturnResultSet.Rows.Count > 0 Then
                    ShowDetail()
                ElseIf Not _ReturnResultSet Is Nothing AndAlso _ReturnResultSet.Rows.Count = 0 Then
                    ShowMessage(getValueByKey("CMR03"), "CMR03 - " & getValueByKey("CLAE04"))
                End If
            ElseIf rbWithoutBill.Checked = True Then
                ShowDetail()
                HideColumns(dgMainGrid, False, "SELECT", "TOTALDISCPERCENTAGE", "NetAmount")
            End If
            'If clsDefaultConfiguration.IsBatchManagementReq Then
            '    For Each dr As DataRow In _ReturnResultSet.Rows
            '        dr("QUANTITY") = 0
            '    Next
            'End If

            checkPromotion()
            dgMainGrid.AutoSizeCols()

        Catch ex As Exception
            ShowMessage(getValueByKey("CMR04"), "CMR04 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub
    Private Sub cmdFlush_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFlush.Click
        Try
            Dim selectedRows() As DataRow = _ReturnResultSet.Select("Select=True", "Ean", DataViewRowState.CurrentRows)
            If (selectedRows.Count = 0) Then
                ShowMessage(getValueByKey("CMR15"), "CMR15 - " & getValueByKey("CLAE04"))
                Exit Sub
            End If

            Dim dt As DataTable = _ReturnResultSet.Clone()
            For Each dr As DataRow In selectedRows
                dr("QUANTITY") = dr("QUANTITY") * -1
                If dr("GROSSAMT").ToString() <> "" Then
                    dr("GROSSAMT") = dr("GROSSAMT") * -1
                End If
                If dr("TOTALDISCOUNT").ToString() <> "" Then
                    dr("TOTALDISCOUNT") = dr("TOTALDISCOUNT") * -1
                End If
                If dr("LINEDISCOUNT").ToString() <> "" Then
                    'dr("LINEDISCOUNT") = dr("LINEDISCOUNT") * -1 'Change By ketan total discount not correct in cash memo return case 
                    dr("LINEDISCOUNT") = (dr("LINEDISCOUNT") / dr("ORIGINALQTY")) * dr("QUANTITY")
                End If
                If dr("TOTALDISCPERCENTAGE").ToString() <> "" Then
                    dr("TOTALDISCPERCENTAGE") = dr("TOTALDISCPERCENTAGE") * -1
                End If
                'commented for bug no 526
                'If dr("SELLINGPRICE").ToString() <> "" Then
                '    dr("SELLINGPRICE") = dr("SELLINGPRICE") * -1
                'End If
                'code is added by irfan on 31/01/2018 for mantis issue
                If rbWithoutBill.Checked = True Then
                    If dr("EXCLUSIVETAX").ToString() <> "" Then
                        dr("EXCLUSIVETAX") = dr("EXCLUSIVETAX") * 1
                    End If
                Else
                    If dr("EXCLUSIVETAX").ToString() <> "" Then
                        dr("EXCLUSIVETAX") = dr("EXCLUSIVETAX") * -1
                    End If
                End If
                dr("TOTALTAXAMOUNT") = dr("EXCLUSIVETAX")                              'Code Added by irfan for Mantis issue on 9/1/2018
                If rbCashMemo.Checked = True Then
                    'ReturnDocumentType
                    dr("ReturnDocumentType") = "CM"
                ElseIf rbSalesOrder.Checked = True Then
                    dr("ReturnDocumentType") = "SO"
                ElseIf rbBirthList.Checked = True Then
                    dr("ReturnDocumentType") = "BL"
                End If
                'Code Added by irfan for Mantis issue on 25/1/2018===================================================================
                If Not RETUNTAMT Is Nothing Then
                    dr("NETAMOUNT") = IIf(dr("NETAMOUNT") Is DBNull.Value, dr("QUANTITY") * dr("SELLINGPRICE"), dr("NETAMOUNT")) * 1
                Else
                    dr("NETAMOUNT") = IIf(dr("NETAMOUNT") Is DBNull.Value, dr("QUANTITY") * dr("SELLINGPRICE"), dr("NETAMOUNT")) * -1
                End If
                '  dr("NETAMOUNT") = IIf(dr("NETAMOUNT") Is DBNull.Value, dr("QUANTITY") * dr("SELLINGPRICE"), dr("NETAMOUNT")) * -1
                '===================================================================================================================
                dr("SALESRETURNREASON") = txtReason.Text
                dr("RETURNCMNO") = txtOldCashMemoNo.Text.Trim
                dr("RETURNCMDATE") = dtpCashmemoDate.Value
                dr("CLPREQUIRE") = IIf(dr("CLPREQUIRE") Is DBNull.Value, 0, dr("CLPREQUIRE"))
                dr("CLPPOINTS") = IIf(dr("CLPPOINTS") Is DBNull.Value, 0, dr("CLPPOINTS"))
                dr("CLPDISCOUNT") = IIf(dr("CLPDISCOUNT") Is DBNull.Value, 0, dr("CLPDISCOUNT"))
                ' add to handle is null value and divid zero error
                dr("OLDQTY") = IIf(dr("OLDQTY") Is DBNull.Value, 0, dr("OLDQTY"))
                If dr("OLDQTY") > 0 Then
                    dr("CLPPOINTS") = FormatNumber(((dr("CLPPOINTS") / dr("OLDQTY")) * dr("QUANTITY")), 2)
                    dr("CLPDISCOUNT") = FormatNumber(((dr("CLPDISCOUNT") / dr("OLDQTY")) * dr("QUANTITY")), 2)
                End If
                ' add to handle is null value and divid zero error
                dr("SECTION") = Customer
                dt.ImportRow(dr)
            Next
            _ReturnResultSet.Clear()
            _ReturnResultSet.Merge(dt, False, MissingSchemaAction.Ignore)
            Me.Close()
        Catch ex As Exception
            ShowMessage(getValueByKey("CMR05"), "CMR05 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub dgMainGrid_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgMainGrid.AfterEdit
        Try
            If dgMainGrid.Cols(e.Col).Name.ToUpper() = "SELECT".ToUpper() Then
                If e.Row > 0 AndAlso dgMainGrid.Rows(e.Row)("FreezeSR") = True Then
                    dgMainGrid.Rows(e.Row)("SELECT") = False
                    ShowMessage(getValueByKey("BL100"), "BL100 - " & getValueByKey("CLAE04"))
                End If
            End If
            If dgMainGrid.Cols(e.Col).Name.ToUpper() = "QUANTITY" Then
                If dgMainGrid.Rows(e.Row)("QUANTITY") <= 0 Then
                    ShowMessage(getValueByKey("CMR07"), "CMR07 - " & getValueByKey("CLAE04"))
                    dgMainGrid.Rows(e.Row)("Quantity") = 1
                End If
            End If
            If dgMainGrid.Rows(e.Row)("SELECT") = True Then
                If rbWithoutBill.Checked = False Then
                    If clsDefaultConfiguration.IsBatchManagementReq Then
                        dgMainGrid.Rows(e.Row)("SELECT") = False
                        e.Cancel = True
                        Exit Sub
                    End If

                    If dgMainGrid.Rows(e.Row)("QUANTITY") > dgMainGrid.Rows(e.Row)("OLDQTY") Then
                        ShowMessage(getValueByKey("CMR06"), "CMR06 - " & getValueByKey("CLAE04"))
                        dgMainGrid.Rows(e.Row)("Quantity") = 1
                    ElseIf dgMainGrid.Rows(e.Row)("QUANTITY") < 0 Then
                        ShowMessage(getValueByKey("CMR07"), "CMR07 - " & getValueByKey("CLAE04"))
                        dgMainGrid.Rows(e.Row)("Quantity") = 1
                    Else
                        dgMainGrid.Rows(e.Row)("GROSSAMT") = dgMainGrid.Rows(e.Row)("QUANTITY") * dgMainGrid.Rows(e.Row)("SellingPrice")
                        If CDbl(IIf(dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE").ToString() <> "", dgMainGrid.Rows(e.Row)("TOTALDISCPERCENTAGE").ToString(), "0")) <> "0" Then
                            'dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") = (dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") / dgMainGrid.Rows(e.Row)("OriginalQty")) * dgMainGrid.Rows(e.Row)("Quantity") ''' Change By ketan total discount not correct in cash memo return case 
                            dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") = (dgMainGrid.Rows(e.Row)("LINEDISCOUNT") / dgMainGrid.Rows(e.Row)("OriginalQty")) * dgMainGrid.Rows(e.Row)("Quantity")
                        Else
                            dgMainGrid.Rows(e.Row)("TOTALDISCOUNT") = 0
                        End If
                        dgMainGrid.Rows(e.Row)("TOTALTAXAMOUNT") = (IIf(Not dgMainGrid.Rows(e.Row)("OldTotalTax") Is DBNull.Value, dgMainGrid.Rows(e.Row)("OldTotalTax"), 0) / dgMainGrid.Rows(e.Row)("OriginalQty")) * dgMainGrid.Rows(e.Row)("QUANTITY")
                        If CDbl(IIf(dgMainGrid.Rows(e.Row)("EXCLUSIVETAX").ToString() <> "", dgMainGrid.Rows(e.Row)("EXCLUSIVETAX").ToString(), "0")) <> "0" Then
                            dgMainGrid.Rows(e.Row)("EXCLUSIVETAX") = (IIf(dgMainGrid.Rows(e.Row)("OLDETAX") Is DBNull.Value, 0, dgMainGrid.Rows(e.Row)("OLDETAX")) / dgMainGrid.Rows(e.Row)("OriginalQty")) * dgMainGrid.Rows(e.Row)("Quantity")
                        End If
                        'Code Added by irfan for Mantis issue on 25/1/2018===================================================================
                        If dgMainGrid.Rows(e.Row)("QUANTITY") < dgMainGrid.Rows(e.Row)("OLDQTY") Then
                            Dim billno As String = _ReturnResultSet.Rows(0)("BILLNO")
                           
                            RETUNTAMT = objCM.ReturnCreditSellAmount(billno)
                            Dim AmtR = 0.0
                            If Not RETUNTAMT Is Nothing Then
                                AmtR = RETUNTAMT.Rows(0)("AmountTendered")
                            End If
                            'dgMainGrid.Rows(e.Row)("NETAMOUNT") = ((dgMainGrid.Rows(e.Row)("OLDNETAMT") / dgMainGrid.Rows(e.Row)("OriginalQty")) * dgMainGrid.Rows(e.Row)("Quantity")) - RETUNTAMT.Rows(0)("AmountTendered")
                            dgMainGrid.Rows(e.Row)("NETAMOUNT") = ((dgMainGrid.Rows(e.Row)("OLDNETAMT") / dgMainGrid.Rows(e.Row)("OriginalQty")) * dgMainGrid.Rows(e.Row)("Quantity")) - AmtR
                        Else
                            dgMainGrid.Rows(e.Row)("NETAMOUNT") = (dgMainGrid.Rows(e.Row)("OLDNETAMT") / dgMainGrid.Rows(e.Row)("OriginalQty")) * dgMainGrid.Rows(e.Row)("Quantity")
                        End If
                        'dgMainGrid.Rows(e.Row)("NETAMOUNT") = (dgMainGrid.Rows(e.Row)("OLDNETAMT") / dgMainGrid.Rows(e.Row)("OriginalQty")) * dgMainGrid.Rows(e.Row)("Quantity")
                        '====================================================================================================================
                    End If
                ElseIf rbWithoutBill.Checked = True Then
                    dgMainGrid.Rows(e.Row)("GROSSAMT") = dgMainGrid.Rows(e.Row)("QUANTITY") * dgMainGrid.Rows(e.Row)("SellingPrice")
                    dgMainGrid.Rows(e.Row)("NETAMOUNT") = dgMainGrid.Rows(e.Row)("QUANTITY") * dgMainGrid.Rows(e.Row)("SellingPrice")
                    ReCalculateCM(dgMainGrid.Rows(dgMainGrid.Row)("EAN").ToString())
                End If
            End If

            If dgMainGrid.Cols(e.Col).Name.ToUpper() = "QUANTITY" Then
                If dgMainGrid.Rows(e.Row)("QUANTITY") <= 0 Then
                    ShowMessage(getValueByKey("CMR07"), "CMR07 - " & getValueByKey("CLAE04"))
                    dgMainGrid.Rows(e.Row)("Quantity") = 1
                End If
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CMR08"), "CMR08 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub cmdSelReason_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelReason.Click
        Try
            If Not dtReason Is Nothing Then
                txtReason.Text = ShowReason(dtReason)
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CMR09"), "CMR09 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub checkPromotion()
        Try
            For Each dr As DataRow In _ReturnResultSet.Rows
                If dr("TOTALDISCPERCENTAGE") = 100 Then
                    ShowMessage(getValueByKey("CMR10"), "CMR10 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cmdSearchItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearchItem.Click
        Dim obj As New frmNItemSearch
        obj.ShowDialog()
        If Not obj.SearchResult Is Nothing Then
            Dim ean As String = String.Empty
            If clsDefaultConfiguration.IsBatchManagementReq Then
                Dim objFrmBarcode As New frmSelectBarcode
                objFrmBarcode.ArticleCode = obj.ItemRow("ArticleCode").ToString()
                objFrmBarcode.ShowDialog()
                If objFrmBarcode.SelectedRow IsNot Nothing Then
                    ean = objFrmBarcode.SelectedRow.Cells("BatchBarcode").Value
                Else
                    Exit Sub
                End If
            End If
            If String.IsNullOrEmpty(ean) Then
                ean = obj.SearchResult(0)
            End If
            txtScanItem.Text = ean
            txtScanItem_KeyDown(txtScanItem, New KeyEventArgs(Keys.Enter))
        End If
    End Sub
    Private Sub txtScanItem_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtScanItem.KeyDown
        Try
            If clsDefaultConfiguration.IsBatchManagementReq Then
                If e.KeyCode = Keys.Enter AndAlso txtScanItem.Text <> String.Empty Then
                    
                    If rbWithoutBill.Checked Then
                        If _ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").Count() > 0 Then
                            'If Not _ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").FirstOrDefault()("QUANTITY") >= _ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").FirstOrDefault()("OLDQTY") Then
                            _ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").FirstOrDefault()("Select") = True
                            _ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").FirstOrDefault()("QUANTITY") += 1
                            _ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").FirstOrDefault()("NetAmount") = _ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").FirstOrDefault()("Quantity") * _ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").FirstOrDefault()("SellingPrice")
                            _ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").FirstOrDefault()("GrossAmt") = _ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").FirstOrDefault()("Quantity") * _ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").FirstOrDefault()("SellingPrice")
                            txtScanItem.Text = ""
                            txtScanItem.Focus()
                            'Else
                            '   MessageBox.Show(getValueByKey("BatchBarcode003"))
                            'End If

                        Else
                            Dim objCM As New clsCashMemo
                            Dim openMrp As Boolean = False
                            Dim dtarticle = objCM.GetItemDetails(clsAdmin.SiteCode, txtScanItem.Text.Trim, openMrp, clsAdmin.LangCode, clsDefaultConfiguration.IsBatchManagementReq)
                            If dtarticle.Rows.Count > 0 Then
                                dtarticle.Rows(0)("Quantity") = 1
                                _ReturnResultSet.Merge(dtarticle, False, MissingSchemaAction.Ignore)
                                _ReturnResultSet.Rows(_ReturnResultSet.Rows.Count - 1)("Select") = True
                                _ReturnResultSet.Rows(_ReturnResultSet.Rows.Count - 1)("BTYPE") = "R"
                                '_ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").FirstOrDefault()("Quantity") = 1

                                _ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").FirstOrDefault()("NetAmount") = dtarticle.Rows(0)("Quantity") * dtarticle.Rows(0)("SellingPrice")
                                _ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").FirstOrDefault()("GrossAmt") = dtarticle.Rows(0)("Quantity") * dtarticle.Rows(0)("SellingPrice")
                                txtScanItem.Text = ""
                                txtScanItem.Focus()
                            Else

                                MessageBox.Show(getValueByKey("BatchBarcode004"))
                            End If
                        End If
                    Else
                        If _ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").Count() > 0 Then

                            If Not _ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").FirstOrDefault()("QUANTITY") >= _ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").FirstOrDefault()("OLDQTY") Then
                                _ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").FirstOrDefault()("Select") = True
                                _ReturnResultSet.Select("Batchbarcode='" & txtScanItem.Text.Trim() & "'").FirstOrDefault()("QUANTITY") += 1
                                txtScanItem.Text = ""
                                txtScanItem.Focus()
                            Else
                                MessageBox.Show(getValueByKey("CMR06"))
                            End If

                        Else
                            MessageBox.Show(getValueByKey("BatchBarcode004"))
                        End If
                    End If

                End If
            Else
                Dim price As Double
                Dim strArticle As String = ""
                Dim Ean As String = ""
                Dim tax As Object
                Dim objCM As New clsCashMemo
                If e.KeyCode = Keys.Enter AndAlso txtScanItem.Text <> String.Empty Then
                    Dim dt As New DataTable
                    Dim openMrp As Boolean = False
                    dt = objCM.GetItemDetails(clsAdmin.SiteCode, txtScanItem.Text.Trim, openMrp, clsAdmin.LangCode, clsDefaultConfiguration.IsBatchManagementReq)
                    If Not dt Is Nothing And dt.Rows.Count = 0 Then
                        ShowMessage(getValueByKey("CMR11"), "CMR11 - " & getValueByKey("CLAE04"))
                        txtScanItem.Focus()
                        Exit Sub
                    ElseIf Not dt Is Nothing AndAlso dt.Rows.Count > 1 Then
                        Dim dvEan As New DataView(dt, "Ean='" & txtScanItem.Text & "'", "", DataViewRowState.CurrentRows)
                        If dvEan.Count > 0 Then
                            dvEan.RowFilter = "EAN<>'" & txtScanItem.Text & "'"
                            If dvEan.Count > 0 Then
                                dvEan.AllowDelete = True
                                For Each dr As DataRowView In dvEan
                                    dr.Delete()
                                Next
                                dt.AcceptChanges()
                            End If
                        Else
                            Dim dv As New DataView(dt, "DefaultEAN <> 1", "", DataViewRowState.CurrentRows)
                            'Dim dv As New DataView(dt, "EanType<>'" & EanType & "'", "", DataViewRowState.CurrentRows)
                            If dv.Count > 0 Then
                                dv.AllowDelete = True
                                For Each dr As DataRowView In dv
                                    dr.Delete()
                                Next
                                dt.AcceptChanges()
                                If dt.Rows.Count <= 0 Then
                                    ShowMessage(getValueByKey("CMR12"), "CMR12 - " & getValueByKey("CLAE04"))
                                End If
                                If dt.Rows.Count > 1 Then
                                    Dim objEan As New frmNCommonView
                                    objEan.SetData = dt
                                    Array.Resize(objEan.ColumnName, dt.Columns.Count)
                                    Dim i As Integer = 0
                                    For Each col As DataColumn In dt.Columns
                                        If col.ColumnName <> "EAN" And col.ColumnName <> "ARTICLECODE" And col.ColumnName <> "SELLINGPRICE" Then
                                            objEan.ColumnName(i) = col.ColumnName
                                        End If
                                        i = i + 1
                                    Next
                                    objEan.ShowDialog()
                                    Dim dtTemp As DataTable = dt.Clone()
                                    dtTemp.ImportRow(objEan.GetResultRow)
                                    dt.Clear()
                                    dt = dtTemp
                                End If
                            End If
                        End If
                        'commented by rama on 15 th Sep 2009
                        'Dim objEan As New frmNCommonView
                        'objEan.SetData = dt
                        'Array.Resize(objEan.ColumnName, dt.Columns.Count)
                        'Dim i As Integer = 0
                        'For Each col As DataColumn In dt.Columns
                        '    If col.ColumnName <> "EAN" And col.ColumnName <> "ARTICLECODE" And col.ColumnName <> "SELLINGPRICE" Then
                        '        objEan.ColumnName(i) = col.ColumnName
                        '    End If
                        '    i = i + 1
                        'Next
                        'objEan.ShowDialog()
                        'For i = dt.Rows.Count - 1 To 1 Step -1
                        '    dt.Rows.RemoveAt(i)
                        'Next
                        'If Not objEan.search Is Nothing Then
                        '    dt.Rows(0)("SellingPrice") = objEan.search(5)
                        '    dt.Rows(0)("EAN") = objEan.search(0)
                        'Else
                        '    Exit Sub
                        'End If
                    End If
                    'Dim Stock As Double = objReturn.GetStocks(txtScanItem.Text.Trim, clsAdmin.SiteCode)
                    'If CDbl(Stock) <= 0 Then
                    '    ShowMessage("Article out of Stock.", "Information")
                    '    dt = Nothing
                    '    Exit Sub
                    'End If
                    If dt.Rows(0)("FreezeSR") = True Then
                        dt = Nothing
                        ShowMessage(getValueByKey("BL100"), "BL100 - " & getValueByKey("CLAE04"))
                        txtScanItem.Focus()
                        Exit Sub
                    End If
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        Ean = dt.Rows(0)("EAN").ToString()
                        strArticle = dt.Rows(0)("ArticleCode").ToString()
                        price = dt.Rows(0)("SellingPrice")
                        If openMrp = False Then
                            If dt.Rows.Count > 1 Then
                                Dim objPrice As New frmNCommonView
                                objPrice.SetData = dt
                                Array.Resize(objPrice.ColumnName, dt.Columns.Count)
                                Dim i As Integer = 0
                                For Each col As DataColumn In dt.Columns
                                    If col.ColumnName <> "EAN" And col.ColumnName <> "SELLINGPRICE" Then
                                        objPrice.ColumnName(i) = col.ColumnName
                                    End If
                                    i = i + 1
                                Next
                                objPrice.ShowDialog()

                                For i = dt.Rows.Count - 1 To 1 Step -1
                                    dt.Rows.RemoveAt(i)
                                Next

                                If Not objPrice.search Is Nothing Then
                                    dt.Rows(0)("SellingPrice") = objPrice.search(5)
                                Else
                                    dt.Rows(0)("SellingPrice") = 0
                                End If
                            End If
                            Dim strCondition As String = "EAN='" & Ean & "' AND SellingPrice=" & ConvertToEnglish(dt.Rows(0)("SellingPrice"))
                            Dim dv As New DataView(_ReturnResultSet, strCondition, "EAN", DataViewRowState.CurrentRows)
                            If dv.Count > 0 Then
                                dv.AllowEdit = True
                                For Each drView As DataRowView In dv
                                    drView("Quantity") = drView("Quantity") + 1
                                    '--price = drView("GrossAmt")
                                    price = drView("SELLINGPRICE") * drView("Quantity")
                                    drView("GrossAmt") = price
                                    'If cbManualDisc.Enabled = True AndAlso cbManualDisc.SelectedIndex > -1 Then
                                    '    CalculateManualPromo(drView("EAN").ToString)
                                    'End If
                                    'byram CreateDataSetForTaxCalculation(txtSearch.Text.Trim(), price, drView.Row, drView("EAN").ToString())
                                    'If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                                    '    CreateDataSetForTaxCalculation(strArticle, price - CDbl(drView("TOTALDISCOUNT").ToString()), drView.Row, drView("EAN").ToString())
                                    'Else
                                    CreateDataSetForTaxCalculation(strArticle, price, drView.Row, drView("EAN").ToString())
                                    'End If
                                    ReCalculateCM(Ean)
                                    'calculateTotalbill()
                                    'ShowLastOper(drView("EAN").ToString, drView("Discription").ToString, drView("Quantity"))
                                    'ShowArticleImage(strArticle)
                                Next
                                txtScanItem.Text = String.Empty
                                txtScanItem.Focus()
                                Exit Sub
                            End If
                            If CInt(dt.Rows(0)("SellingPrice")) = 0 Then
                                ShowMessage(getValueByKey("CMR13"), "CMR13 - " & getValueByKey("CLAE04"))
                                dt = Nothing
                            End If
                        ElseIf openMrp = True Then
                            Dim objPrompt As New frmSpecialPrompt(getValueByKey("CMR15"))
                            objPrompt.ShowMessage = False
                            objPrompt.ShowTextBox = True
                            objPrompt.AllowDecimal = True
                            objPrompt.txtValue.MaxLength = 14
                            objPrompt.ShowDialog()
                            price = objPrompt.GetResult()
                            objPrompt.Dispose()
                            If CInt(price) = 0 Then
                                ShowMessage(getValueByKey("CMR13"), "CMR13 - " & getValueByKey("CLAE04"))
                                dt = Nothing
                            ElseIf CInt(price) > 0 Then
                                dt.Rows(0)("SellingPrice") = CDbl(price)
                            End If
                        End If

                        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                            Dim dr As DataRow = dt.Rows(0)
                            dr("Quantity") = 1
                            'If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                            '    tax = CreateDataSetForTaxCalculation(strArticle, price - CDbl(dr("TOTALDISCOUNT").ToString()), dr, dr("EAN").ToString())
                            'Else
                            tax = CreateDataSetForTaxCalculation(strArticle, price, dr, dr("EAN").ToString())
                            'End If
                            If clsDefaultConfiguration.ArticleTaxAllowed = False AndAlso tax Is Nothing Then
                                ShowMessage(getValueByKey("CM019"), "CM019 - " & getValueByKey("CLAE04"))
                                'ShowMessage("Article is Removing As no Tax found on it.", "Information")
                                dt = Nothing
                            Else
                                If tax Is Nothing Then tax = 0
                            End If
                            'If tax Is Nothing Then
                            '    ShowMessage("Article is Removing As no Tax found on it.", "Information")
                            '    dt = Nothing
                            'End If
                        End If
                    End If
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim strCondition As String = "EAN='" & Ean & "' AND SellingPrice=" & ConvertToEnglish(dt.Rows(0)("SellingPrice"))
                        Dim dv As New DataView(_ReturnResultSet, strCondition, "EAN", DataViewRowState.CurrentRows)
                        If dv.Count > 0 Then
                            dv.AllowEdit = True
                            For Each drView As DataRowView In dv
                                drView("Quantity") = drView("Quantity") + 1
                                price = drView("GrossAmt")
                                'If cbManualDisc.Enabled = True AndAlso cbManualDisc.SelectedIndex > -1 Then
                                '    CalculateManualPromo(drView("EAN").ToString)
                                'End If
                                'If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                                '    CreateDataSetForTaxCalculation(strArticle, price - CDbl(drView("TOTALDISCOUNT").ToString()), drView.Row, drView("EAN").ToString())
                                'Else
                                CreateDataSetForTaxCalculation(strArticle, price, drView.Row, drView("EAN").ToString())
                                'End If

                                ReCalculateCM(Ean)
                                'calculateTotalbill()
                                'ShowLastOper(drView("EAN").ToString, drView("Discription").ToString, drView("Quantity"))
                                'ShowArticleImage(strArticle)
                                'DisplayText("Name: " & drView("Discription") & "  Price  " & drView("SellingPrice"))
                            Next
                            txtScanItem.Text = String.Empty
                            txtScanItem.Focus()
                            Exit Sub
                        Else
                            dt.Rows(0)("Quantity") = 1
                            _ReturnResultSet.Merge(dt, False, MissingSchemaAction.Ignore)
                            _ReturnResultSet.Rows(_ReturnResultSet.Rows.Count - 1)("Select") = True
                            _ReturnResultSet.Rows(_ReturnResultSet.Rows.Count - 1)("BTYPE") = "R"
                            _ReturnResultSet.Rows(_ReturnResultSet.Rows.Count - 1)("GrossAmt") = dt.Rows(0)("Quantity") * dt.Rows(0)("SellingPrice")
                            _ReturnResultSet.Rows(_ReturnResultSet.Rows.Count - 1)("NetAmount") = dt.Rows(0)("Quantity") * dt.Rows(0)("SellingPrice")
                            'dsMain.Tables("CASHMEMODTL").Merge(dt, False, MissingSchemaAction.Ignore)
                            'dsMain.Tables("CASHMEMODTL").Rows(dsMain.Tables("CASHMEMODTL").Rows.Count - 1)("BillLineNO") = dsMain.Tables("CASHMEMODTL").Rows.Count
                            GridSettings()
                            ReCalculateCM(Ean)
                            'ApplyManualPromotion()
                            'calculateTotalbill()
                            'ShowLastOper(dt.Rows(0)("EAN").ToString, dt.Rows(0)("Discription").ToString, 1)
                            'ShowArticleImage(strArticle)
                            'DisplayText("Name: " & dt.Rows(0)("Discription") & "  Price  " & dt.Rows(0)("SellingPrice"))
                        End If
                    End If
                    txtScanItem.Text = String.Empty
                    txtScanItem.Focus()
                End If
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CMR14"), "CMR14 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Function CreateDataSetForTaxCalculation(ByVal strMatcode As String, ByVal TaxableAmount As Double, ByRef dr As DataRow, Optional ByVal EAN As String = "") As Object
        Try

            Dim StrTaxCode As Double = 0
            Dim dtTaxCalc As DataTable
            dtTaxCalc = New DataTable
            Dim ExlusiveFound As Boolean = False
            dtTaxCalc = objReturn.getTax(clsAdmin.SiteCode, strMatcode, "CMS", dr("Quantity"), EAN)
            Dim dbIncTotalTax As Double = 0
            Dim dbExclTotalTax As Double = 0
            If dtTaxCalc.Rows.Count = 0 Then
                Return Nothing
            End If
            If dtTaxCalc.Rows.Count <> 0 Then
                dtTaxCalc.Rows(0)("TAXABLE_AMOUNT") = TaxableAmount
                objReturn.getCalculatedDataSet(dtTaxCalc)
                Dim iRowTax As Int16
                With dtTaxCalc
                    For iRowTax = 0 To dtTaxCalc.Rows.Count - 1
                        If CDbl(dtTaxCalc.Rows(iRowTax)("VALUE").ToString) <> 0 Then
                            If dtTaxCalc.Rows(iRowTax)("INCLUSIVE") = "0" Then
                                dbExclTotalTax = dbExclTotalTax + CDbl(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT"))
                            Else
                                dbIncTotalTax = dbIncTotalTax + CDbl(dtTaxCalc.Rows(iRowTax)("TAXAMOUNT"))
                            End If
                        End If
                    Next
                End With
                dr("EXCLUSIVETAX") = dbExclTotalTax * -1
                StrTaxCode = dtTaxCalc.Compute("SUM(TAXAMOUNT)", "")
                dr("TOTALTAXAMOUNT") = StrTaxCode * -1
                Return StrTaxCode
            End If
        Catch ex As Exception
            ShowMessage(getValueByKey("CM012"), "CM012 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Error in updating Tax Calculation", "Error")
            Return Nothing
        End Try
    End Function
    Private Sub ReCalculateCM(ByVal strItem As String)
        Try
            Dim filter As String = ""
            If strItem <> String.Empty Then
                filter = filter & " EAN='" & strItem & "'"
            End If
            For Each dr As DataRow In _ReturnResultSet.Select(filter, "Ean", DataViewRowState.CurrentRows)
                dr("TOTALDISCOUNT") = CDbl(IIf(dr("LINEDISCOUNT").ToString() <> String.Empty, dr("LINEDISCOUNT").ToString(), "0")) + CDbl(IIf(dr("CLPDISCOUNT").ToString() <> String.Empty, dr("CLPDISCOUNT").ToString(), "0"))
                dr("GrossAmt") = dr("Quantity") * dr("SellingPrice")
                If clsDefaultConfiguration.ExclusiveTaxAfterDisc = True Then
                    CreateDataSetForTaxCalculation(dr("ArticleCode").ToString(), CDbl(dr("GrossAmt").ToString()) - CDbl(dr("TOTALDISCOUNT").ToString()), dr, dr("EAN").ToString())
                Else
                    CreateDataSetForTaxCalculation(dr("ArticleCode").ToString(), CDbl(dr("GrossAmt").ToString()), dr, dr("EAN").ToString())
                End If
                dr("NETAMOUNT") = (dr("Quantity") * dr("SellingPrice")) - CDbl(dr("TOTALDISCOUNT").ToString())
                If Not dr("EXCLUSIVETAX") Is DBNull.Value Then
                    If rbWithoutBill.Checked = True Then               'code is added by irfan for mantis issue on 31/1/2018
                        dr("NETAMOUNT") = dr("NETAMOUNT") - CDbl(dr("EXCLUSIVETAX"))
                    Else
                        dr("NETAMOUNT") = dr("NETAMOUNT") + CDbl(dr("EXCLUSIVETAX"))
                    End If
                    ' dr("NETAMOUNT") = dr("NETAMOUNT") + CDbl(dr("EXCLUSIVETAX"))
                End If


            Next
        Catch ex As Exception
            ShowMessage(getValueByKey("CM044"), "CM044 - " & getValueByKey("CLAE05"))
            LogException(ex)
            'ShowMessage("Iteam Details calculation not done Properly", "Error")
        End Try
    End Sub
#End Region
#Region "Private Methods & Functions"
    Private Sub BillTyp_change(ByVal Sender As Object, ByVal e As System.EventArgs)
        If rbCashMemo.Checked = True Then
            txtOldCashMemoNo.Enabled = True
            dtpCashmemoDate.Enabled = True
            cmdSearchBill.Enabled = True
            txtScanItem.Enabled = clsDefaultConfiguration.IsBatchManagementReq
            cmdSearchItem.Enabled = False
            lblCashmemo.Text = getValueByKey("frmncashmemoreturn.lblcashmemo_0")
            lblBillDate.Text = getValueByKey("frmncashmemoreturn.lblbilldate_0")
        ElseIf rbSalesOrder.Checked = True Then
            txtOldCashMemoNo.Enabled = True
            dtpCashmemoDate.Enabled = True
            cmdSearchBill.Enabled = True
            txtScanItem.Enabled = clsDefaultConfiguration.IsBatchManagementReq
            cmdSearchItem.Enabled = False
            lblCashmemo.Text = getValueByKey("frmncashmemoreturn.lblcashmemo_1")
            lblBillDate.Text = getValueByKey("frmncashmemoreturn.lblbilldate_1")
        ElseIf rbBirthList.Checked = True Then
            txtOldCashMemoNo.Enabled = True
            dtpCashmemoDate.Enabled = True
            cmdSearchBill.Enabled = True
            txtScanItem.Enabled = clsDefaultConfiguration.IsBatchManagementReq
            cmdSearchItem.Enabled = False
            lblCashmemo.Text = getValueByKey("frmncashmemoreturn.lblcashmemo")
            lblBillDate.Text = getValueByKey("frmncashmemoreturn.lblbilldate")
        ElseIf rbWithoutBill.Checked = True Then
            txtOldCashMemoNo.Enabled = False
            dtpCashmemoDate.Enabled = False
            cmdSearchBill.Enabled = False
            txtScanItem.Enabled = True
            cmdSearchItem.Enabled = True
            lblCashmemo.Text = getValueByKey("frmncashmemoreturn.lblcashmemo_0")
            lblBillDate.Text = getValueByKey("frmncashmemoreturn.lblbilldate_0")
        End If
    End Sub
    Private Sub GridSettings()
        Try
            dgMainGrid.Cols("Select").Width = 45

            If (clsDefaultConfiguration.BarcodeDisplayAllowed) Then
                dgMainGrid.Cols("EAN").Width = 100
                dgMainGrid.Cols("EAN").Caption = IIf(resourceMgr Is Nothing, "Barcode", getValueByKey("frmncashmemoreturn.dgmaingrid.ean"))
                dgMainGrid.Cols("EAN").AllowEditing = False
                dgMainGrid.Cols("EAN").Visible = True
            Else
                dgMainGrid.Cols("EAN").Visible = False
            End If

            dgMainGrid.Cols("DISCRIPTION").Width = 200
            'dgMainGrid.Cols("DISCRIPTION").Caption = "Item Discription"
            dgMainGrid.Cols("DISCRIPTION").AllowEditing = False
            dgMainGrid.Cols("ArticleCode").Width = 100
            'dgMainGrid.Cols("EAN").Caption = "Item"
            dgMainGrid.Cols("ArticleCode").AllowEditing = False
            dgMainGrid.Cols("Quantity").Width = 62

            If clsDefaultConfiguration.AllowDecimalQty Then
                If clsDefaultConfiguration.WeightScaleEnabled Then
                    dgMainGrid.Cols("Quantity").Format = "0.000"
                Else
                    dgMainGrid.Cols("Quantity").Format = "0.00"
                End If
            Else
                dgMainGrid.Cols("Quantity").DataType = Type.GetType("System.Int32")
                dgMainGrid.Cols("Quantity").Format = "0"
            End If

            dgMainGrid.Cols("SellingPrice").Width = 58
            'dgMainGrid.Cols("SellingPrice").Caption = "Price"
            dgMainGrid.Cols("SellingPrice").Format = "0.00"
            If rbWithoutBill.Checked = True Then
                dgMainGrid.Cols("SellingPrice").AllowEditing = False
            Else
                dgMainGrid.Cols("SellingPrice").AllowEditing = False
            End If
            dgMainGrid.Cols("TOTALDISCPERCENTAGE").Width = 56
            'dgMainGrid.Cols("TOTALDISCPERCENTAGE").Caption = "Disc %"
            dgMainGrid.Cols("TOTALDISCPERCENTAGE").AllowEditing = False
            dgMainGrid.Cols("TOTALDISCPERCENTAGE").Format = "0.00"
            dgMainGrid.Cols("NETAMOUNT").Width = 75
            'dgMainGrid.Cols("NETAMOUNT").Caption = "Net Amount"
            dgMainGrid.Cols("NETAMOUNT").Format = "0.00"
            dgMainGrid.Cols("NETAMOUNT").AllowEditing = False
            dgMainGrid.Cols("ArticleCode").Caption = IIf(resourceMgr Is Nothing, "Article Code", getValueByKey("frmcashmemo.dgmaingrid.articlecode"))
            'dgMainGrid.Cols("Select").Visible = Not clsDefaultConfiguration.IsBatchManagementReq
            ' dgMainGrid.Cols("Quantity").Visible = Not clsDefaultConfiguration.IsBatchManagementReq
            dgMainGrid.Cols("BatchBarcode").Visible = clsDefaultConfiguration.IsBatchManagementReq

        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
    Private Sub SetformLayout(ByVal Level As Integer)
        If Level = 1 Then
            Dim i As Integer
            i = SplitContainer1.Panel2.Height
            i = FormNormalHeight - i
            Me.Height = i
            SplitContainer1.Panel2Collapsed = True
        ElseIf Level = 2 Then
            Me.Height = FormNormalHeight
            SplitContainer1.Panel2Collapsed = False
            SplitContainer1.SplitterDistance = SpliterDistance
        End If
    End Sub
    Private Function Fp_Validate() As Boolean
        If clsDefaultConfiguration.ReasonRequired = True AndAlso txtReason.Text = String.Empty Then
            ShowMessage(getValueByKey("CM045"), "CM045 - " & getValueByKey("CLAE04"))
            'ShowMessage("You have not selected or Entered the reason", "Information")
            Return False
        End If
        If rbWithoutBill.Checked = False And txtOldCashMemoNo.Text = String.Empty Then
            If rbBirthList.Checked Then
                ShowMessage(getValueByKey("frmcashmemo.returmns.BLNo"), "frmcashmemo.returmns.BLNo - " & getValueByKey("CLAE04"))
            ElseIf rbSalesOrder.Checked Then
                ShowMessage(getValueByKey("frmcashmemo.returmns.salesorderNo"), "frmcashmemo.returmns.salesorderNo - " & getValueByKey("CLAE04"))
            Else
                ShowMessage(getValueByKey("CM046"), "CM046 - " & getValueByKey("CLAE04"))
            End If


            'ShowMessage("You have not entered the Bill Number", "Information")
            Return False
        End If
        Return True
    End Function
    Private Sub ShowDetail()
        Try
            If clsDefaultConfiguration.IsBatchManagementReq Then
                For rowIndex = 0 To _ReturnResultSet.Rows.Count - 1
                    _ReturnResultSet.Rows(rowIndex)("Quantity") = 0
                Next
            End If

            dgMainGrid.DataSource = _ReturnResultSet
            For colno = 0 To dgMainGrid.Cols.Count - 1
                If dgMainGrid.Cols(colno).Name.ToUpper() <> "DISCRIPTION".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "EAN".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "ArticleCode".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "SellingPrice".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "Quantity".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "TOTALDISCPERCENTAGE".ToUpper() _
                    AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "Select".ToUpper() _
                     AndAlso dgMainGrid.Cols(colno).Name.ToUpper() <> "NetAmount".ToUpper() Then
                    HideColumns(dgMainGrid, False, dgMainGrid.Cols(colno).Name)
                End If
                If dgMainGrid.Cols(colno).DataType.ToString() = "System.Decimal" Then
                    dgMainGrid.Cols(colno).DataType = Type.GetType("System.Double")
                End If
            Next
            GridSettings()
            SizFooter.Visible = True
            SizHeader.Enabled = False

            If clsDefaultConfiguration.IsBatchManagementReq Then
                dgMainGrid.Cols("Select").AllowEditing = False
                dgMainGrid.Cols("BatchBarcode").AllowEditing = False
            End If

            SetCulture(Me, Me.Name)
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub
#End Region
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.MaximizeBox = False
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog

        dgMainGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom

    End Sub
    Private Sub cmdSearchBill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearchBill.Click
        Try
            Dim dtData As DataTable
            If rbCashMemo.Checked = True Then
                Dim obj As New clsCashMemo
                dtData = obj.GetOldCashMemo(clsAdmin.SiteCode, IsReturnCashMemo:=True, NoOfDaysForReprint:=clsDefaultConfiguration.NumberOfDaysApplicableForReprint)
            ElseIf rbSalesOrder.Checked = True Then
                dtData = objReturn.GetClosedSODetails(clsAdmin.SiteCode, IsReturnSales:=True)
            ElseIf rbBirthList.Checked = True Then
                dtData = objReturn.GetClosedBirtHListDetails(clsAdmin.SiteCode)
            End If
            If Not dtData Is Nothing Then
                Dim frm As New frmNCommonSearch
                frm.SetData = dtData
                frm.ShowDialog()
                If Not frm.search Is Nothing AndAlso frm.search.Length > 0 Then
                    If rbCashMemo.Checked = True Then
                        txtOldCashMemoNo.Text = frm.search(0).ToString()
                        ' Dim dtp As DateTime
                        'dtp.
                        Dim theCultureInfo As IFormatProvider
                        theCultureInfo = New System.Globalization.CultureInfo("en-GB", True)
                        Dim theDateTime As DateTime = DateTime.ParseExact(frm.search(4), "MM/dd/yyyy", theCultureInfo)
                        dtpCashmemoDate.Value = theDateTime
                        Customer = frm.search(frm.search.Length - 4).ToString()
                    Else
                        txtOldCashMemoNo.Text = frm.search(0).ToString()
                        Customer = frm.search(1).ToString()
                        Try
                            dtpCashmemoDate.Value = frm.search(2)
                        Catch ex As Exception
                        End Try
                    End If

                    'cmdOk_Click(cmdOk, e)
                End If
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub dgMainGrid_BeforeEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgMainGrid.BeforeEdit
        Try

            If dgMainGrid.Cols(e.Col).Name.ToUpper() = "Quantity".ToUpper() Then
                If e.Row > 0 AndAlso dgMainGrid.Rows(e.Row)("SELECT") = False Then
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub cmdCancelDtl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelDtl.Click
        Try
            _ReturnResultSet.Clear()
            Me.Close()
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Protected Overrides Function ProcessKeyPreview(ByRef m As System.Windows.Forms.Message) As Boolean
        Const WM_KEYDOWN As Integer = &H100
        If m.Msg = WM_KEYDOWN Then
            Select Case m.WParam.ToInt32
                Case Keys.F
                    If My.Computer.Keyboard.CtrlKeyDown Then
                        'Debug.Print("Ctrl + F")
                        cmdSearchItem_Click(Nothing, New KeyEventArgs(Keys.Enter))
                    End If

            End Select
        End If
        Return MyBase.ProcessKeyPreview(m)
    End Function

    Private Sub CtrlBtnQtyChanged_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlBtnQtyChanged.Click
        ChangeQty()

    End Sub
    Private Sub ChangePrice()
        If dgMainGrid.Row >= 0 Then
            If dgMainGrid.Rows(dgMainGrid.Row)("SELECT") = True Then
                If CheckInterTransactionAuth("PriceChange") = True Then
                    Dim frm As New frmSpecialPrompt(getValueByKey("SP002"))
                    frm.ShowTextBox = True
                    frm.AllowDecimal = True
                    frm.IsNumeric = True
                    frm.txtValue.MaxLength = 14
                    frm.ShowDialog()
                    If frm.getEventType <> 2 Then
                        dgMainGrid.Rows(dgMainGrid.Row)("SellingPrice") = IIf(frm.GetResult Is Nothing, 1, frm.GetResult)
                        dgMainGrid.Rows(dgMainGrid.Row)("GROSSAMT") = dgMainGrid.Rows(dgMainGrid.Row)("QUANTITY") * dgMainGrid.Rows(dgMainGrid.Row)("SellingPrice")
                        dgMainGrid.Rows(dgMainGrid.Row)("NETAMOUNT") = dgMainGrid.Rows(dgMainGrid.Row)("QUANTITY") * dgMainGrid.Rows(dgMainGrid.Row)("SellingPrice")
                        ReCalculateCM(dgMainGrid.Rows(dgMainGrid.Row)("EAN").ToString())
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub ChangeQty()
        Try
            If dgMainGrid.Row >= 0 Then
                If dgMainGrid.Rows(dgMainGrid.Row)("SELECT") = True Then
                    Dim frm As New frmSpecialPrompt(getValueByKey("SP003"))
                    frm.ShowTextBox = True
                    frm.IsNumeric = True
                    frm.txtValue.MaxLength = 9
                    If clsDefaultConfiguration.AllowDecimalQty Then
                        frm.AllowDecimal = True
                    End If

                    frm.ShowDialog()
                    If frm.getEventType <> 2 Then
                        Dim NewQty As Double = IIf(frm.GetResult Is Nothing, 1, frm.GetResult)
                        '--- Added By Mahesh 0 or less quantity not allowed ...
                        If NewQty <= 0 Then
                            ShowMessage(getValueByKey("CMR07"), "CMR07 - " & getValueByKey("CLAE04"))
                            dgMainGrid.Rows(dgMainGrid.Row)("Quantity") = 1
                            If rbWithoutBill.Checked = True Then Exit Sub
                            dgMainGrid.Rows(dgMainGrid.Row)("GROSSAMT") = dgMainGrid.Rows(dgMainGrid.Row)("QUANTITY") * dgMainGrid.Rows(dgMainGrid.Row)("SellingPrice")
                            dgMainGrid.Rows(dgMainGrid.Row)("NETAMOUNT") = (dgMainGrid.Rows(dgMainGrid.Row)("OLDNETAMT") / dgMainGrid.Rows(dgMainGrid.Row)("OriginalQty")) * dgMainGrid.Rows(dgMainGrid.Row)("Quantity")
                            dgMainGrid.Rows(dgMainGrid.Row)("SELECT") = True
                            Exit Sub
                        End If

                        If rbWithoutBill.Checked = False Then
                            If NewQty > dgMainGrid.Rows(dgMainGrid.Row)("OLDQTY") Then
                                'ShowMessage("You are Entering more than the Original Quantity", "Information")
                                ShowMessage(getValueByKey("CMR06"), "CMR06 - " & getValueByKey("CLAE04"))
                                dgMainGrid.Rows(dgMainGrid.Row)("SELECT") = True
                                Exit Sub
                            End If
                        End If
                        If rbWithoutBill.Checked = True Then
                            dgMainGrid.Rows(dgMainGrid.Row)("QUANTITY") = NewQty
                            dgMainGrid.Rows(dgMainGrid.Row)("GROSSAMT") = dgMainGrid.Rows(dgMainGrid.Row)("QUANTITY") * dgMainGrid.Rows(dgMainGrid.Row)("SellingPrice")
                            dgMainGrid.Rows(dgMainGrid.Row)("NETAMOUNT") = dgMainGrid.Rows(dgMainGrid.Row)("QUANTITY") * dgMainGrid.Rows(dgMainGrid.Row)("SellingPrice")
                            ReCalculateCM(dgMainGrid.Rows(dgMainGrid.Row)("EAN").ToString())
                            ''Total Discount Added By ketan And Sagar Discount Amount Issue in grid in return case
                            ' dgMainGrid.Rows(dgMainGrid.Row)("TotalDiscount") = (dgMainGrid.Rows(dgMainGrid.Row)("LINEDISCOUNT") / dgMainGrid.Rows(dgMainGrid.Row)("OriginalQty")) * NewQty
                            ' dgMainGrid.Rows(dgMainGrid.Row)("TotalDiscount") = (IIf(dgMainGrid.Rows(dgMainGrid.Row)("LINEDISCOUNT") Is DBNull.Value, 0, dgMainGrid.Rows(dgMainGrid.Row)("LINEDISCOUNT")) / dgMainGrid.Rows(dgMainGrid.Row)("OriginalQty")) * NewQty       this code is commented by irfan
                            Dim linedisc As Double = Convert.ToDouble(IIf(dgMainGrid.Rows(dgMainGrid.Row)("LINEDISCOUNT") Is DBNull.Value, 0, dgMainGrid.Rows(dgMainGrid.Row)("LINEDISCOUNT")))    ' code is created by irfan for quantity on 12/19/2017
                            Dim originalqty As Double = Convert.ToDouble(IIf(dgMainGrid.Rows(dgMainGrid.Row)("OriginalQty") Is DBNull.Value, 1, dgMainGrid.Rows(dgMainGrid.Row)("OriginalQty")))      ' code is created by irfan for quantity on 12/19/2017           ' CDbl(dgMainGrid.Rows(dgMainGrid.Row)("OriginalQty"))
                            dgMainGrid.Rows(dgMainGrid.Row)("TotalDiscount") = (linedisc / originalqty) * NewQty
                        Else
                            dgMainGrid.FinishEditing()
                            dgMainGrid.Rows(dgMainGrid.Row)("QUANTITY") = IIf(frm.GetResult Is Nothing, 1, frm.GetResult)
                            dgMainGrid.Rows(dgMainGrid.Row)("GROSSAMT") = dgMainGrid.Rows(dgMainGrid.Row)("QUANTITY") * dgMainGrid.Rows(dgMainGrid.Row)("SellingPrice")
                            'Added by irfan on 21/12/2017 for tax calculation===============================================
                            dgMainGrid.Rows(dgMainGrid.Row)("TOTALTAXAMOUNT") = (IIf(Not dgMainGrid.Rows(dgMainGrid.Row)("OldTotalTax") Is DBNull.Value, dgMainGrid.Rows(dgMainGrid.Row)("OldTotalTax"), 0) / dgMainGrid.Rows(dgMainGrid.Row)("OriginalQty")) * dgMainGrid.Rows(dgMainGrid.Row)("QUANTITY")
                            If CDbl(IIf(dgMainGrid.Rows(dgMainGrid.Row)("EXCLUSIVETAX").ToString() <> "", dgMainGrid.Rows(dgMainGrid.Row)("EXCLUSIVETAX").ToString(), "0")) <> "0" Then
                                dgMainGrid.Rows(dgMainGrid.Row)("EXCLUSIVETAX") = (IIf(dgMainGrid.Rows(dgMainGrid.Row)("OLDETAX") Is DBNull.Value, 0, dgMainGrid.Rows(dgMainGrid.Row)("OLDETAX")) / dgMainGrid.Rows(dgMainGrid.Row)("OriginalQty")) * dgMainGrid.Rows(dgMainGrid.Row)("Quantity")
                            End If
                            '================================================================================
                            'dgMainGrid.Rows(dgMainGrid.Row)("NETAMOUNT") = dgMainGrid.Rows(dgMainGrid.Row)("QUANTITY") * dgMainGrid.Rows(dgMainGrid.Row)("SellingPrice")
                            dgMainGrid.Rows(dgMainGrid.Row)("NETAMOUNT") = (dgMainGrid.Rows(dgMainGrid.Row)("OLDNETAMT") / dgMainGrid.Rows(dgMainGrid.Row)("OriginalQty")) * dgMainGrid.Rows(dgMainGrid.Row)("Quantity")
                            ''Total Discount Added By ketan And Sagar Discount Amount Issue in grid in return case
                            dgMainGrid.Rows(dgMainGrid.Row)("TotalDiscount") = (IIf(IsDBNull(dgMainGrid.Rows(dgMainGrid.Row)("LINEDISCOUNT")), 0, dgMainGrid.Rows(dgMainGrid.Row)("LINEDISCOUNT")) / dgMainGrid.Rows(dgMainGrid.Row)("OriginalQty")) * NewQty

                        End If
                        dgMainGrid.Rows(dgMainGrid.Row)("SELECT") = True
                    End If
                End If
            End If
        Catch ex As Exception
            LogException(ex)
        End Try
       
    End Sub
    Private Sub CtrlBtnChangePrice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlBtnChangePrice.Click
        ChangePrice()
    End Sub

    Private Function Themechange()
        'Me.Size = New Size(430, 238)
        Me.BackgroundColor = Color.FromArgb(134, 134, 134)

        rbCashMemo.ForeColor = Color.White
        rbCashMemo.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        rbSalesOrder.ForeColor = Color.White
        rbSalesOrder.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        rbWithoutBill.ForeColor = Color.White
        rbWithoutBill.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        rbBirthList.ForeColor = Color.White
        rbBirthList.Font = New Font("Neo Sans", 9, FontStyle.Bold)

        lblCashmemo.ForeColor = Color.Black
        lblCashmemo.AutoSize = False
        lblCashmemo.Size = New Size(129, 20)
        lblCashmemo.BackColor = Color.FromArgb(212, 212, 212)
        lblCashmemo.BorderStyle = BorderStyle.None

        lblBillDate.ForeColor = Color.Black
        lblBillDate.BackColor = Color.FromArgb(212, 212, 212)
        lblBillDate.BorderStyle = BorderStyle.None
        lblBillDate.AutoSize = False
        lblBillDate.Size = New Size(91, 20)

        lblName.ForeColor = Color.Black
        lblName.BackColor = Color.FromArgb(212, 212, 212)
        lblName.BorderStyle = BorderStyle.None
        lblName.AutoSize = False
        lblName.Size = New Size(129, 20)

        lblReason.ForeColor = Color.Black
        lblReason.BackColor = Color.FromArgb(212, 212, 212)
        lblReason.BorderStyle = BorderStyle.None
        lblReason.AutoSize = False
        lblReason.Size = New Size(129, 20)

        cmdSearchBill.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdSearchBill.BackColor = Color.Transparent
        cmdSearchBill.BackColor = Color.FromArgb(0, 107, 163)
        cmdSearchBill.ForeColor = Color.FromArgb(255, 255, 255)
        cmdSearchBill.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdSearchBill.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdSearchBill.FlatStyle = FlatStyle.Flat
        cmdSearchBill.FlatAppearance.BorderSize = 0
        cmdSearchBill.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdSearchBill.Size = New Size(63, 29)

        cmdSelReason.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdSelReason.BackColor = Color.Transparent
        cmdSelReason.BackColor = Color.FromArgb(0, 107, 163)
        cmdSelReason.ForeColor = Color.FromArgb(255, 255, 255)
        cmdSelReason.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdSelReason.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdSelReason.FlatStyle = FlatStyle.Flat
        cmdSelReason.FlatAppearance.BorderSize = 0
        cmdSelReason.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdSelReason.Size = New Size(63, 29)

        cmdCancel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdCancel.BackColor = Color.Transparent
        cmdCancel.BackColor = Color.FromArgb(0, 107, 163)
        cmdCancel.ForeColor = Color.FromArgb(255, 255, 255)
        cmdCancel.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdCancel.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdCancel.FlatStyle = FlatStyle.Flat
        cmdCancel.FlatAppearance.BorderSize = 0
        cmdCancel.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdCancel.Size = New Size(63, 29)


        cmdOk.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdOk.BackColor = Color.Transparent
        cmdOk.BackColor = Color.FromArgb(0, 107, 163)
        cmdOk.ForeColor = Color.FromArgb(255, 255, 255)
        cmdOk.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdOk.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdOk.FlatStyle = FlatStyle.Flat
        cmdOk.FlatAppearance.BorderSize = 0
        cmdOk.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        cmdOk.Size = New Size(63, 29)


        CtrlBtnChangePrice.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnChangePrice.BackColor = Color.Transparent
        CtrlBtnChangePrice.BackColor = Color.FromArgb(0, 107, 163)
        CtrlBtnChangePrice.ForeColor = Color.FromArgb(255, 255, 255)
        CtrlBtnChangePrice.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlBtnChangePrice.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlBtnChangePrice.FlatStyle = FlatStyle.Flat
        CtrlBtnChangePrice.FlatAppearance.BorderSize = 0
        CtrlBtnChangePrice.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        'CtrlBtnChangePrice.Size = New Size(63, 29)



        CtrlBtnQtyChanged.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        CtrlBtnQtyChanged.BackColor = Color.Transparent
        CtrlBtnQtyChanged.BackColor = Color.FromArgb(0, 107, 163)
        CtrlBtnQtyChanged.ForeColor = Color.FromArgb(255, 255, 255)
        CtrlBtnQtyChanged.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        CtrlBtnQtyChanged.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        CtrlBtnQtyChanged.FlatStyle = FlatStyle.Flat
        CtrlBtnQtyChanged.FlatAppearance.BorderSize = 0
        CtrlBtnQtyChanged.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        ' CtrlBtnQtyChanged.Size = New Size(63, 29)




        cmdCancelDtl.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdCancelDtl.BackColor = Color.Transparent
        cmdCancelDtl.BackColor = Color.FromArgb(0, 107, 163)
        cmdCancelDtl.ForeColor = Color.FromArgb(255, 255, 255)
        cmdCancelDtl.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdCancelDtl.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdCancelDtl.FlatStyle = FlatStyle.Flat
        cmdCancelDtl.FlatAppearance.BorderSize = 0
        cmdCancelDtl.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        ' cmdCancelDtl.Size = New Size(63, 29)



        cmdFlush.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdFlush.BackColor = Color.Transparent
        cmdFlush.BackColor = Color.FromArgb(0, 107, 163)
        cmdFlush.ForeColor = Color.FromArgb(255, 255, 255)
        cmdFlush.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdFlush.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdFlush.FlatStyle = FlatStyle.Flat
        cmdFlush.FlatAppearance.BorderSize = 0
        cmdFlush.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        'cmdFlush.Size = New Size(63, 29)

        cmdSearchItem.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        cmdSearchItem.BackColor = Color.Transparent
        cmdSearchItem.BackColor = Color.FromArgb(0, 107, 163)
        cmdSearchItem.ForeColor = Color.FromArgb(255, 255, 255)
        cmdSearchItem.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        cmdSearchItem.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        cmdSearchItem.FlatStyle = FlatStyle.Flat
        cmdSearchItem.FlatAppearance.BorderSize = 0
        cmdSearchItem.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        lblScanItem.ForeColor = Color.Black
        lblScanItem.BackColor = Color.FromArgb(212, 212, 212)
        lblScanItem.BorderStyle = BorderStyle.None
        lblScanItem.AutoSize = False
        lblScanItem.Size = New Size(91, 20)

        'lblMessage.ForeColor = Color.White
        'lblMessage.Font = New Font("Neo Sans", 12, FontStyle.Bold)
        'cmdHold.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        'cmdHold.BackColor = Color.Transparent
        'cmdHold.BackColor = Color.FromArgb(0, 107, 163)
        'cmdHold.ForeColor = Color.FromArgb(255, 255, 255)
        'cmdHold.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        'cmdHold.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        'cmdHold.FlatStyle = FlatStyle.Flat
        'cmdHold.FlatAppearance.BorderSize = 0
        'cmdHold.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)

        'lblMessage.ForeColor = Color.White
        'lblMessage.Font = New Font("Neo Sans", 12, FontStyle.Bold)
    End Function
End Class
