Imports Microsoft.PointOfService
Imports System.Management
Imports SpectrumBL
Imports Microsoft.VisualBasic
Public Class frmPrinterHirearchyMap
     Private dsCombo As New DataSet
    Private dsMain As New DataSet
    Private dvPrinter As DataView
    Private dtPrinter As DataTable
    Private drPrinter As DataRow

    Private fKeyPrinter(1) As Object
    Private clsPMap As New clsPrinterMapping
    Dim _TerminalId As String
    Dim dtHirearchy As New DataTable
    Private IsKotMapToPrinter As Boolean = False
    Public Property TerminalId() As String
        Get
            Return _TerminalId
        End Get
        Set(ByVal value As String)
            _TerminalId = value
        End Set
    End Property
    Dim _DocTypeVal As String
    Public Property DocTypeVal() As String
        Get
            Return _DocTypeVal
        End Get
        Set(ByVal value As String)
            _DocTypeVal = value
        End Set
    End Property
    Dim _DocTypeText As String
    Public Property DocTypeText() As String
        Get
            Return _DocTypeText
        End Get
        Set(ByVal value As String)
            _DocTypeText = value
        End Set
    End Property
    Private Sub frmPrinterHirearchyMap_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        dtHirearchy = clsPMap.GetHirearchyList()
        rbL40.Checked = True
        LoadSubDocPrinterMappingData()
        RefreshPrinterGrid()
        If dtHirearchy.Rows.Count > 0 Then
            
            For Each row As DataRow In dtHirearchy.Rows
                chkHierarchy.Items.Add(row.Item("NodeName"), False)
                'chkHierarchy.Items.IndexOf("fai")
            Next row
            chkHierarchy.CheckOnClick = True
        End If
        dsCombo = clsPMap.GetPrinterComboInfo(clsAdmin.SiteCode)
        If Not (TerminalId = "System.Data.DataRowView") Then
            dvPrinter = New DataView(dsCombo.Tables("DeviceProfile"), "TerminalID='" & TerminalId & "'", String.Empty, DataViewRowState.CurrentRows)
            dtPrinter = dvPrinter.ToTable(False, "DriverType", "LogicalName")

            If dtPrinter.Rows.Count > 0 Then
                CboPrinterInfo.DataSource = dtPrinter
                CboPrinterInfo.ValueMember = dtPrinter.Columns("DriverType").ColumnName
                CboPrinterInfo.DisplayMember = dtPrinter.Columns("LogicalName").ColumnName
                CboPrinterInfo.SelectedIndex = 0
            Else
                CboPrinterInfo.DataSource = Nothing

            End If

            If clsDefaultConfiguration.ThemeSelect = "Theme 1" Then
                Themechange()
            End If

            ' RefreshPrinterGrid()
        End If
         

    End Sub


    Private Sub BtnSetPrinterMapping_Click(sender As Object, e As EventArgs) Handles BtnSetPrinterMapping.Click
        Try
            txtSubDocName.Enabled = True
            Dim strHier As String = ""
            fKeyPrinter(0) = DocTypeVal
            fKeyPrinter(1) = txtSubDocName.Text
            drPrinter = dsMain.Tables("PrinterSubDocMap").Rows.Find(fKeyPrinter)
            Dim str As String = String.Empty
            If IsValidate() Then
                If drPrinter Is Nothing Then
                    drPrinter = dsMain.Tables("PrinterSubDocMap").NewRow

                    drPrinter("DocumentType") = DocTypeVal
                    Dim itemChecked As Object
                    For Each itemChecked In chkHierarchy.CheckedItems
                        strHier &= itemChecked.ToString.Trim() & ","
                    Next
                    If strHier <> "" Then
                        If strHier.LastIndexOf(","c) > 0 Then
                            strHier = Microsoft.VisualBasic.Left(strHier, strHier.Length - 1) ' Left(strHier, strHier.Length - 1)
                        End If
                    End If

                    drPrinter("SubDocumentType") = txtSubDocName.Text
                    drPrinter("DocumentType") = DocTypeText
                    drPrinter("PrinterType") = CboPrinterInfo.SelectedValue
                    drPrinter("PrinterName") = CboPrinterInfo.Text
                    drPrinter("ArticleHierarchy") = strHier
                    drPrinter("CREATEDAT") = clsAdmin.SiteCode
                    drPrinter("CREATEDBY") = clsAdmin.UserName
                    drPrinter("CREATEDON") = clsAdmin.CurrentDate
                    drPrinter("UPDATEDAT") = clsAdmin.SiteCode
                    drPrinter("UPDATEDBY") = clsAdmin.UserName
                    drPrinter("UPDATEDON") = clsAdmin.CurrentDate
                    drPrinter("STATUS") = True
                    If rbA4.Checked Then
                        str = "A4"
                    End If
                    If rbL40.Checked Then
                        str = "L40"
                    End If
                    drPrinter("PrintFormat") = str

                    dsMain.Tables("PrinterSubDocMap").Rows.Add(drPrinter)
                Else
                    drPrinter("DocumentType") = DocTypeVal
                    drPrinter("SubDocumentType") = txtSubDocName.Text
                    drPrinter("PrinterType") = CboPrinterInfo.SelectedValue
                    drPrinter("PrinterName") = CboPrinterInfo.Text
                    Dim itemChecked As Object
                    For Each itemChecked In chkHierarchy.CheckedItems
                        strHier &= itemChecked.ToString & ","
                    Next
                    If strHier <> "" Then
                        If strHier.LastIndexOf(","c) > 0 Then
                            strHier = Microsoft.VisualBasic.Left(strHier, strHier.Length - 1) ' Left(strHier, strHier.Length - 1)
                        End If
                    End If
                    drPrinter("ArticleHierarchy") = strHier
                    If rbA4.Checked Then
                        str = "A4"
                    End If
                    If rbL40.Checked Then
                        str = "L40"
                    End If
                    drPrinter("PrintFormat") = str
                    drPrinter("STATUS") = True
                    drPrinter("UPDATEDAT") = clsAdmin.SiteCode
                    drPrinter("UPDATEDBY") = clsAdmin.UserName
                    drPrinter("UPDATEDON") = clsAdmin.CurrentDate
                End If

                RefreshPrinterGrid()
                ClearForm()
            Else
                Exit Sub
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Sub ClearForm()
        txtSubDocName.Enabled = True
        txtSubDocName.Text = ""
        For i As Integer = 0 To chkHierarchy.Items.Count - 1
            chkHierarchy.SetItemChecked(i, False)
        Next
    End Sub
    Private Sub RefreshPrinterGrid()
        Dim dvPrinterN As DataView
        dvPrinterN = New DataView(dsMain.Tables("PrinterSubDocMap"), "DocumentType='" & DocTypeVal & "' ", Nothing, DataViewRowState.CurrentRows)
        dtPrinter = New DataTable
        dtPrinter = dvPrinterN.ToTable(False, "DocumentType", "SubDocumentType", "PrintFormat", "PrinterName", "ArticleHierarchy")

        grdPrinterMapping.DataSource = dtPrinter
        grdPrinterMapping.Cols("Delete").Caption = ""
        grdPrinterMapping.Cols("Delete").Width = 20
        grdPrinterMapping.Cols("Delete").ComboList = "..."
        grdPrinterMapping.Cols("PrinterDocument").Visible = False
        grdPrinterMapping.AllowSorting = False
        grdPrinterMapping.Cols("ArticleHierarchy").AllowEditing = False
        grdPrinterMapping.AutoSizeCols()
    End Sub
    Private Sub LoadSubDocPrinterMappingData()
        dsMain = clsPMap.GetPrnSubDocMappingStruc()
        'code added by vipul for issue id 3342
        If Not dsMain Is Nothing Then
            If dsMain.Tables(0).Rows.Count > 0 Then
                IsKotMapToPrinter = True
            End If
        End If
        'If dsMain.Tables("PrinterSubDocMap").Columns.Contains("DocDesc") = False Then
        '    dsMain.Tables("PrinterSubDocMap").Columns.Add("DocDesc", System.Type.GetType("System.String"))
        'End If

        'Dim filterDesc As String = String.Empty

        'For Each drUpdate As DataRow In dsMain.Tables("PrinterSubDocMap").Rows
        '    filterDesc = "DocumentType='" & drUpdate("DocumentType") & "'"
        '    'If dsCombo.Tables("PrinterDoc").Select(filterDesc).Length > 0 Then
        '    '    drUpdate("DocDesc") = dsCombo.Tables("PrinterDoc").Select(filterDesc)(0)("DocumentDesc").ToString
        '    'End If
        'Next

    End Sub

    Private Sub grdPrinterMapping_CellButtonClick(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdPrinterMapping.CellButtonClick
        Try
            If MsgBox(getValueByKey("DIN031"), MsgBoxStyle.YesNo, "DIN031 - " & getValueByKey("DIN025")) = MsgBoxResult.Yes Then
                Dim filterDelete As String = String.Empty

                'grdPrinterMapping.Cols("PrinterDocument").Visible = True
                filterDelete = "SubDocumentType='" & grdPrinterMapping.Item(grdPrinterMapping.Row, "SubDocumentType") & "'"

                dvPrinter = New DataView(dsMain.Tables("PrinterSubDocMap"), filterDelete, "", DataViewRowState.CurrentRows)
                If dvPrinter.Count > 0 Then
                    dvPrinter.AllowDelete = True
                    For Each drView As DataRowView In dvPrinter
                        drView.Delete()
                    Next
                End If
                RefreshPrinterGrid()
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub BtnSavePrinterMapping_Click(sender As Object, e As EventArgs) Handles BtnSavePrinterMapping.Click
        Try
            If grdPrinterMapping.Rows.Count = 1 And IsKotMapToPrinter = False Then
                'Please Set Printer Hierarchy Mapping
                ShowMessage(getValueByKey("DIN038"), getValueByKey("CLAE04"))
                Exit Sub
            End If
            If clsPMap.SavePrinterHierarchyMapping(dsMain, clsAdmin.SiteCode) = True Then
                'MsgBox("Update", , getValueByKey("CLAE05"))
                ShowMessage(getValueByKey("PTM001"), "PTM001 - " & getValueByKey("CLAE04"))
                LoadSubDocPrinterMappingData()
            Else
                'MsgBox("Fail", , getValueByKey("CLAE05"))
                ShowMessage(getValueByKey("PTM002"), "PTM002 - " & getValueByKey("CLAE05"))
            End If
            

        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub grdPrinterMapping_DoubleClick(sender As Object, e As EventArgs) Handles grdPrinterMapping.DoubleClick
        Try

            Dim title As String = grdPrinterMapping.Rows(grdPrinterMapping.RowSel)("SubDocumentType").ToString()
            txtSubDocName.Text = title
            Dim hierarchy As String = grdPrinterMapping.Rows(grdPrinterMapping.RowSel)("ArticleHierarchy").ToString()
            If hierarchy <> "" Then
                Dim hierArray() As String = hierarchy.Split(",")
                If hierArray.Length > 0 Then
                    For i As Integer = 0 To chkHierarchy.Items.Count - 1
                        chkHierarchy.Items.Clear()
                    Next
                    If dtHirearchy.Rows.Count > 0 Then

                        For Each row As DataRow In dtHirearchy.Rows
                            If hierArray.Contains(row.Item("NodeName")) Then
                                chkHierarchy.Items.Add(row.Item("NodeName"), True)
                            Else
                                chkHierarchy.Items.Add(row.Item("NodeName"), False)
                            End If

                            'chkHierarchy.Items.IndexOf("fai")
                        Next row
                        chkHierarchy.CheckOnClick = True
                    End If
                    chkHierarchy.CheckOnClick = True
                End If
            End If
            txtSubDocName.Enabled = False
        Catch ex As Exception

        End Try
    End Sub
    Public Function IsValidate() As Boolean
        Try
            Dim itemcheck As Object
            Dim itemUnique As Object
            For Each item In chkHierarchy.CheckedItems

                itemUnique = item
                itemcheck &= item.ToString.Trim() & ","

                For Each drr As DataRow In dsMain.Tables("PrinterSubDocMap").Select("", "", DataViewRowState.CurrentRows)
                    If (drr("ArticleHierarchy").ToString = (itemUnique)) Then
                        ShowMessage(getValueByKey("DIN023"), "DIN023-" & getValueByKey("DIN025"))
                        'ShowMessage("Hierarchy Selected should not Match with Article Hierarchy Present", "Article Hierarchy")
                        Exit Function
                    End If
                Next
            Next

            If itemcheck <> "" Then
                If itemcheck.LastIndexOf(","c) > 0 Then
                    itemcheck = Microsoft.VisualBasic.Left(itemcheck, itemcheck.Length - 1) ' Left(strHier, strHier.Length - 1)
                End If
            End If
            If (txtSubDocName.Text = String.Empty) Then
                ShowMessage(getValueByKey("DIN020"), "DIN020-" & getValueByKey("DIN025"))
                'ShowMessage("Please enter Title", "text")
                Exit Function
            ElseIf (itemcheck = String.Empty) Then
                ShowMessage(getValueByKey("DIN021"), "DIN021-" & getValueByKey("DIN025"))
                'ShowMessage("Please Select Hierarchy", "item")
                Exit Function
            ElseIf (itemcheck = String.Empty And txtSubDocName.Text = String.Empty) Then
                ShowMessage(getValueByKey("DIN022"), "DIN022-" & getValueByKey("DIN025"))
                'ShowMessage("Please Select Hierarchy or Enter Title", "both")
                Exit Function
            Else

                For Each drr As DataRow In dsMain.Tables("PrinterSubDocMap").Select("", "", DataViewRowState.CurrentRows)
                    If (drr("ArticleHierarchy") = itemcheck) Then
                        ShowMessage(getValueByKey("DIN023"), "DIN023-" & getValueByKey("DIN025"))
                        'ShowMessage("Hierarchy Selected should not Match with Article Hierarchy Present", "Artcile Hierarchy")
                        Exit Function
                    End If
                    If (drr("SubDocumentType") = txtSubDocName.Text) Then
                        ShowMessage(getValueByKey("DIN024"), "DDIN024-" & getValueByKey("DIN025"))
                        'ShowMessage("The Title entered should Not Match With Printer SubDocument Type", "Document Type")
                        Exit Function
                    End If
                Next
                Return True
            End If
        Catch ex As Exception
            LogException(ex)
            ShowMessage("Error", "" & getValueByKey("DIN014"))
        End Try
    End Function

    Private Sub txtSubDocName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSubDocName.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Function Themechange()

        Me.BackColor = Color.FromArgb(134, 134, 134)

        rbA4.ForeColor = Color.White
        rbL40.ForeColor = Color.White

        lblTitle.ForeColor = Color.White
        lblTitle.AutoSize = False
        lblTitle.Size = New Size(75, 18)
        lblTitle.BorderStyle = BorderStyle.None
        lblTitle.SendToBack()
        lblTitle.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblTitle.TextAlign = ContentAlignment.MiddleLeft
        'lblTitle.BackColor = Color.FromArgb(212, 212, 212)

        lblPrinter.ForeColor = Color.White
        lblPrinter.AutoSize = False
        lblPrinter.Size = New Size(60, 18)
        lblPrinter.BorderStyle = BorderStyle.None
        lblPrinter.SendToBack()
        lblPrinter.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblPrinter.TextAlign = ContentAlignment.MiddleLeft
        'lblPrinter.BackColor = Color.FromArgb(212, 212, 212)


        lblFormat.ForeColor = Color.White
        lblFormat.AutoSize = False
        lblFormat.Size = New Size(60, 18)
        lblFormat.BorderStyle = BorderStyle.None
        lblFormat.SendToBack()
        lblFormat.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblFormat.TextAlign = ContentAlignment.MiddleLeft
        'lblFormat.BackColor = Color.FromArgb(212, 212, 212)

        lblHierarchy.ForeColor = Color.White
        lblHierarchy.AutoSize = False
        lblHierarchy.Size = New Size(80, 18)
        lblHierarchy.BorderStyle = BorderStyle.None
        lblHierarchy.SendToBack()
        lblHierarchy.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        lblHierarchy.TextAlign = ContentAlignment.MiddleLeft
        'lblHierarchy.BackColor = Color.FromArgb(212, 212, 212)

        grdPrinterMapping.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Custom
        grdPrinterMapping.Styles.Fixed.BackColor = Color.FromArgb(177, 227, 253)
        grdPrinterMapping.Styles.Normal.BackColor = Color.FromArgb(255, 255, 255)
        grdPrinterMapping.Styles.Normal.Font = New Font("Neo Sans", 10, FontStyle.Regular)
        grdPrinterMapping.Styles.Fixed.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdPrinterMapping.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdPrinterMapping.Styles.Highlight.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdPrinterMapping.Styles.Focus.Font = New Font("Neo Sans", 10, FontStyle.Bold)
        grdPrinterMapping.Styles.SelectedColumnHeader.BackColor = Color.FromArgb(177, 227, 253)

        BtnSetPrinterMapping.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnSetPrinterMapping.BackColor = Color.Transparent
        BtnSetPrinterMapping.BackColor = Color.FromArgb(0, 107, 163)
        BtnSetPrinterMapping.ForeColor = Color.FromArgb(255, 255, 255)
        BtnSetPrinterMapping.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnSetPrinterMapping.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnSetPrinterMapping.FlatStyle = FlatStyle.Flat
        BtnSetPrinterMapping.FlatAppearance.BorderSize = 0
        BtnSetPrinterMapping.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        BtnSetPrinterMapping.TextAlign = ContentAlignment.MiddleCenter
        BtnSetPrinterMapping.Size = New Size(85, 30)

        BtnSavePrinterMapping.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.System
        BtnSavePrinterMapping.BackColor = Color.Transparent
        BtnSavePrinterMapping.BackColor = Color.FromArgb(0, 107, 163)
        BtnSavePrinterMapping.ForeColor = Color.FromArgb(255, 255, 255)
        BtnSavePrinterMapping.Font = New Font("Neo Sans", 9, FontStyle.Bold)
        BtnSavePrinterMapping.VisualStyle = C1.Win.C1Input.VisualStyle.Custom
        BtnSavePrinterMapping.FlatStyle = FlatStyle.Flat
        BtnSavePrinterMapping.FlatAppearance.BorderSize = 0
        BtnSavePrinterMapping.FlatAppearance.BorderColor = Color.FromArgb(0, 81, 120)
        BtnSavePrinterMapping.TextAlign = ContentAlignment.MiddleCenter
        BtnSavePrinterMapping.Size = New Size(85, 30)



    End Function
End Class