Imports SpectrumBL
Public Class frmAddPrescription
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Dim objCM As New clsCashMemo
    Dim objPC As New clsSalesOrder
    Dim objClsCommon As New clsCommon
    Dim vSiteCode As String = clsAdmin.SiteCode
    Dim vUserName As String = clsAdmin.UserName
    Private _ConsultantsNoteId As String
    Public Property ConsultantsNoteId() As String
        Get
            Return _ConsultantsNoteId
        End Get
        Set(ByVal value As String)
            _ConsultantsNoteId = value
        End Set
    End Property

    Private _NoteSrNo As String
    Public Property NoteSrNo() As String
        Get
            Return _NoteSrNo
        End Get
        Set(ByVal value As String)
            _NoteSrNo = value
        End Set
    End Property

    Private _PatientId As String
    Public Property PatientId() As String
        Get
            Return _PatientId
        End Get
        Set(ByVal value As String)
            _PatientId = value
        End Set
    End Property

    Private _PrescHdr As DataTable
    Public Property DtPrescHdr() As DataTable
        Get
            Return _PrescHdr
        End Get
        Set(ByVal value As DataTable)
            _PrescHdr = value
        End Set
    End Property

    Private _DtPrescRemarks As DataTable
    Public Property DtPrescRemarks() As DataTable
        Get
            Return _DtPrescRemarks
        End Get
        Set(ByVal value As DataTable)
            _DtPrescRemarks = value
        End Set
    End Property

    Private _DtPrescriptionCopy As DataTable
    Public Property DtPrescriptionCopy() As DataTable
        Get
            Return _DtPrescriptionCopy
        End Get
        Set(ByVal value As DataTable)
            _DtPrescriptionCopy = value
        End Set
    End Property
    Private _PrescDtl As DataTable
    Public Property DtPrescDtl() As DataTable
        Get
            Return _PrescDtl
        End Get
        Set(ByVal value As DataTable)
            _PrescDtl = value
        End Set
    End Property
    Private _editedSrNo As Integer
    Public Property EditedSrNo() As Integer
        Get
            Return _editedSrNo
        End Get
        Set(ByVal value As Integer)
            _editedSrNo = value
        End Set
    End Property
    Private _displaySrNo As Integer
    Public Property displaySrNo() As Integer
        Get
            Return _displaySrNo
        End Get
        Set(ByVal value As Integer)
            _displaySrNo = value
        End Set
    End Property
    Private _IsEdit As Boolean
    Public Property IsEdit() As Boolean
        Get
            Return _IsEdit
        End Get
        Set(ByVal value As Boolean)
            _IsEdit = value
        End Set
    End Property
    Private _ArticleCode As String
    Public Property ArticleCode() As String
        Get
            Return _ArticleCode
        End Get
        Set(ByVal value As String)
            _ArticleCode = value
        End Set
    End Property
    Private _remark As String
    Public Property remark() As String
        Get
            Return _remark
        End Get
        Set(ByVal value As String)
            _remark = value
        End Set
    End Property

    Enum dgArticle
        ' Selects = 0
        Delete
        SrNo
        ArticleCode
        ArticleDescription
        Qty
        ConsumptionRate
        Duration
        Remarks
        EAN
    End Enum

    Public Function PopulateComboBox1(ByVal dtCombo As DataTable, ByRef ObjComboBox As ctrlCombo)

        ObjComboBox.DataSource = dtCombo
        ObjComboBox.ValueMember = dtCombo.Columns(0).ColumnName
        ObjComboBox.DisplayMember = dtCombo.Columns(1).ColumnName
        If dtCombo.Rows.Count > 0 Then
            ObjComboBox.SelectedIndex = 1
        End If

        Return ""
    End Function
    Private Sub btnAddRemark_Click(sender As Object, e As EventArgs) Handles btnAddRemark.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim strArticle As String = ""
            Dim Ean As String = ""
            Dim flag As Integer = 0
            Dim dtAddToGrid As New DataTable
            If (txtAndroidArticleSearchTextBox.Text.Trim() <> String.Empty) Then

                txtAndroidArticleSearchTextBox.Text = txtAndroidArticleSearchTextBox.Text.ToString().Split(" ")(0)
                dtAddToGrid = objClsCommon.GetArticleDetailsById(txtAndroidArticleSearchTextBox.Text.Trim())
                If dtAddToGrid.Rows.Count = 0 Then
                    ShowMessage("Article does not exist", "BOC001 - " & getValueByKey("CLAE04"))
                    Exit Sub
                End If

                If dgGridArticle.Rows.Count > 1 Then
                    For index = 1 To dgGridArticle.Rows.Count - 1
                        If dtAddToGrid.Rows(0)("ArticleCode").ToString() = dgGridArticle.Rows(index)(dgArticle.ArticleCode) Then
                            ShowMessage("Article alreay exist", "CLIST06 - " & getValueByKey("CLIST06"))
                            Exit Sub
                        End If
                    Next
                End If
                Dim ItemDesc As String = String.Empty
                If Not dtAddToGrid Is Nothing AndAlso dtAddToGrid.Rows.Count > 0 Then
                    AddArticleRemarkToGrid(dtAddToGrid, False)
                End If
            End If

            'If DtPrescDtl.Rows.Count > 0 Then

            '    dtAddToGrid = DtPrescDtl.Copy
            '    dgGridArticle.Rows.Count = 1
            '    AddArticleRemarkToGrid(dtAddToGrid, True)
            '    dtAddToGrid.Clear()
            'End If
            If dgGridArticle.Rows.Count = 1 Then
                ShowMessage(" please select article first ", "Information")
                Exit Sub
            End If
            txtAndroidArticleSearchTextBox.Text = ""
            txtRemarks.Text = ""
        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - " & getValueByKey("CLAE05"))
            LogException(ex)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub
    

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If ValidatePrescriptionCombo() Then
                'Dim CurrentDateTime As DateTime = objClsCommon.GetCurrentDate()
                BindDataTableFromArticleGrid()
                'For index = 1 To dgGridArticle.Rows.Count - 1
                '    Dim dtRow As Int32 = -1
                '    Dim result As DataRow() = DtPrescDtl.Select("Articlecode='" + dgGridArticle.Rows(index)("ArticleCode").ToString() + "' and SrNo='" + dgGridArticle.Rows(index)("SrNo").ToString() + "'")
                '    If result.Length > 0 Then

                '    Else
                '        DtPrescDtl.Rows.Add()
                '        dtRow = DtPrescDtl.Rows.Count - 1
                '        DtPrescDtl.Rows(dtRow)("SrNo") = dgGridArticle.Rows(index)(dgArticle.SrNo)
                '        DtPrescDtl.Rows(dtRow)("ArticleCode") = dgGridArticle.Rows(index)(dgArticle.ArticleCode)
                '        DtPrescDtl.Rows(dtRow)("ArticleDescription") = dgGridArticle.Rows(index)(dgArticle.ArticleDescription)
                '        DtPrescDtl.Rows(dtRow)("EAN") = ""
                '        DtPrescDtl.Rows(dtRow)("Qty") = dgGridArticle.Rows(index)(dgArticle.Qty)
                '        DtPrescDtl.Rows(dtRow)("ConsumptionRate") = dgGridArticle.Rows(index)(dgArticle.ConsumptionRate)
                '        DtPrescDtl.Rows(dtRow)("Duration") = dgGridArticle.Rows(index)("Duration").ToString()
                '        DtPrescDtl.Rows(dtRow)("Remarks") = dgGridArticle.Rows(index)(dgArticle.Remarks)
                '        DtPrescDtl.Rows(dtRow)("ConsultantsNoteId") = ConsultantsNoteId
                '        DtPrescDtl.Rows(dtRow)("PatientId") = PatientId
                '        DtPrescDtl.Rows(dtRow)("SiteCode") = clsAdmin.SiteCode
                '        DtPrescDtl.Rows(dtRow)("NoteSrNo") = NoteSrNo
                '        DtPrescDtl.Rows(dtRow)("CREATEDAT") = vSiteCode
                '        DtPrescDtl.Rows(dtRow)("CREATEDBY") = vUserName
                '        DtPrescDtl.Rows(dtRow)("CREATEDON") = clsAdmin.CurrentDate
                '        DtPrescDtl.Rows(dtRow)("UPDATEDAT") = vSiteCode
                '        DtPrescDtl.Rows(dtRow)("UPDATEDBY") = vUserName
                '        DtPrescDtl.Rows(dtRow)("UPDATEDON") = clsAdmin.CurrentDate
                '        DtPrescDtl.Rows(dtRow)("STATUS") = True
                '    End If
                'Next index
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            dgGridArticle.Rows.Count = 0
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
            Me.Dispose()
            IsEdit = False
        Catch ex As Exception
            ShowMessage(ex.Message, getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Private Function ValidatePrescriptionCombo() As Boolean
        Try
            If dgGridArticle.Rows.Count = 1 Then
                'MsgBox("Please add atleast one item ")
                ShowMessage("Add atleast one article.", getValueByKey("CLAE04"))
                Exit Function
                ' added by Khusrao adil 
            ElseIf dgGridArticle.Rows.Count > 1 Then
                For index = 1 To dgGridArticle.Rows.Count - 1
                    Dim _qty As String = dgGridArticle.Rows(index)(dgArticle.Qty)
                    Dim _consmRate As String = dgGridArticle.Rows(index)("ConsumptionRate").ToString()

                    Dim _duration As String = dgGridArticle.Rows(index)("Duration").ToString()
                    If _qty = String.Empty Then
                        ShowMessage("Quantity should not be empty or zero.", getValueByKey("CLAE04"))
                        Exit Function
                    ElseIf _qty = 0 Then
                        ShowMessage("Quantity should not be empty or zero.", getValueByKey("CLAE04"))
                        Exit Function
                    ElseIf _consmRate = String.Empty Or _consmRate = "0" Then
                        ShowMessage("Rate should not be empty or zero.", getValueByKey("CLAE04"))
                        Exit Function
                    ElseIf _duration = "0" Or _duration = String.Empty Then
                        ShowMessage("Please select Duration.", getValueByKey("CLAE04"))
                        Exit Function
                    End If
                Next

            End If
            Return True
        Catch ex As Exception
            LogException(ex)
        End Try
    End Function
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        clearform()
        IsEdit = False
    End Sub
    Public _dtGridPresc As New DataTable
    Private Sub frmAddPrescription_Load(sender As Object, e As EventArgs) Handles Me.Load
        '  Call getBinding()
        AddHandler txtAndroidArticleSearchTextBox.KeyDown, AddressOf txtSearch_KeyDown
        Dim dtBind = objCM.GetItemDetailsForBulkOrder(clsAdmin.SiteCode, "")
        If dtBind.Rows.Count > 1 Then
            'Dim listSource As List(Of [String]) = (From row In dtBind
            '                        Select Convert.ToString(row("ArticleCode")) + " " + Convert.ToString(row("ArticleName"))).Distinct().ToList()
            'txtAddArticle.lstNames = listSource
            Call SetWildSearchTextBox(dtBind, txtAndroidArticleSearchTextBox, key:="ArticleCode", Value:="ArticleName", searchData:="ArticleCodeDesc")
            txtAndroidArticleSearchTextBox.IsMovingControl = True
        End If
        GetDuration()
        If DtPrescDtl Is Nothing AndAlso DtPrescDtl.Rows.Count = 0 Then
            DtPrescDtl = objPC.GetPatientPrescriptionTableStructure(DtPrescDtl)
        End If
        If DtPrescDtl.Rows.Count > 0 Then
            AddArticleRemarkToGrid(DtPrescDtl, True)
        End If

        If _dtGridPresc.Rows.Count = 0 Then
            _dtGridPresc = objPC.GetPatientPrescriptionTableStructure(_dtGridPresc)
        End If

    End Sub
    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim strArticle As String = ""
            Dim Ean As String = ""
            Dim Weight As String = ""
            Dim WeghingScaleBarcode = False
            Dim flag As Integer = 0

            If e.KeyCode = Keys.Delete AndAlso dgGridArticle.Rows.Count > 1 AndAlso txtAndroidArticleSearchTextBox.Text.Length = 0 Then
                dgGridArticle.Rows.Remove(dgGridArticle.Row)
                If (dgGridArticle.Rows.Count > 1) Then
                    dgGridArticle.Select(1, 2)
                End If
                sender.Select()
                sender.Focus()
                Exit Sub
            End If

            If (e.KeyCode = Keys.Enter AndAlso sender.Text <> String.Empty) Then
                Dim dt As New DataTable
                dt = objCM.GetItemDetailsForBulkOrder(clsAdmin.SiteCode, sender.Text.Trim, clsAdmin.LangCode, True)
                Dim dtitem = objCM.GetItemDetails(clsAdmin.SiteCode, dt.Rows(0)("ArticleCode"), False, clsAdmin.LangCode)
                If Val(dtitem.Rows(0)("SELLINGPRICE")) = 0 Or Val(dtitem.Rows(0)("MRP")) = 0 Or Val(dtitem.Rows(0)("CostPrice")) = 0 Then
                    'If dtItemPriceCheck = "0" Then
                    ShowMessage("Item is not available", getValueByKey("CLAE04"))
                    txtAndroidArticleSearchTextBox.Clear()
                    Exit Sub
                    'End If
                End If
                If dgGridArticle.Rows.Count > 1 Then
                    For index = 1 To dgGridArticle.Rows.Count - 1

                        If dt.Rows(0)("ArticleCode").ToString() = dgGridArticle.Rows(index)(dgArticle.ArticleCode) Then
                            ShowMessage("Article alredy exist", "Information " & getValueByKey("CLIST06"))
                            txtAndroidArticleSearchTextBox.Text = ""
                            txtAndroidArticleSearchTextBox.Select()
                            Exit Sub
                        End If
                    Next
                End If
                Dim ItemDesc As String = String.Empty
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then


                    '---------Delete item then immedialty add item
                    'Dim result As DataRow() = DtSoBulkComboDtl.Select("ArticleCode='" + dt.Rows(0)("ArticleCode").ToString() + "' and ComboSrNo='" + EditedSrNo.ToString() + "' and status=false")
                    'If result.Length > 0 Then
                    '    result(0)("Status") = True
                    'End If

                    dgGridArticle.Rows.Add()

                    'dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.Selects) = ""
                    dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.SrNo) = dgGridArticle.Rows.Count - 1
                    dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.ArticleCode) = dt.Rows(0)("ArticleCode").ToString()
                    If clsDefaultConfiguration.PrintItemFullName Then
                        dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.ArticleDescription) = dt.Rows(0)("ArticleName")
                    Else
                        dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.ArticleDescription) = dt.Rows(0)("ArticleShortName")
                    End If
                    dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.Qty) = Val(dt.Rows(0)("Qty"))
                    dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.ConsumptionRate) = Val(dt.Rows(0)("ConsumptionRate"))
                    ' dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.Duration) = Val(dt.Rows(0)("Duration"))
                    dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.Duration) = "Day(s)"
                    dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.Remarks) = txtRemarks.Text
                    sender.Text = String.Empty
                    sender.Focus()
                    sender.Select()

                    If (dgGridArticle.Rows.Count > 1) Then
                        dgGridArticle.Select(dgGridArticle.Rows.Count - 1, 2)
                    End If
                End If
            End If

        Catch ex As Exception
            ShowMessage(getValueByKey("CM020"), "CM020 - exc " & getValueByKey("CLAE05"))
            'MsgBox(ex.InnerException.Message)
            'MsgBox(ex.Message)
            LogException(ex)
            'ShowMessage("Error in Searcing of Item Details", "Error") 
        Finally
            Cursor.Current = Cursors.Default
            'txtSearch.Text = String.Empty
            'CtrlSalesPersons.CtrlTxtBox.Focus()
        End Try

    End Sub
    Private Sub AddArticleRemarkToGrid(ByVal dt As DataTable, ByVal isEdit As Boolean)
        Try
           
            '    Dim result As DataRow() = DtPrescDtl.Select("ArticleCode='" + dt.Rows(0)("ArticleCode").ToString() + "' and NoteSrNo='" + EditedSrNo.ToString() + "' and status=false")
            '    If result.Length > 0 Then
            '        result(0)("Status") = True
            '    End If
            'dgGridArticle.Rows.Count = 1
            For index = 0 To dt.Rows.Count - 1
                dgGridArticle.Rows.Add()
                '  dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.Selects) = ""
                dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.SrNo) = dgGridArticle.Rows.Count - 1
                dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.ArticleCode) = dt.Rows(index)("ArticleCode").ToString()
                If isEdit Then
                    dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.ArticleDescription) = dt.Rows(index)("ArticleDescription")
                    dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.Qty) = dt.Rows(index)("Qty")
                    dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.ConsumptionRate) = dt.Rows(index)("ConsumptionRate")
                    dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.Duration) = dt.Rows(index)("Duration")
                    dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.Remarks) = dt.Rows(index)("Remarks")
                Else
                    dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.ArticleDescription) = dt.Rows(index)("ArticleDescription")
                    dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.Qty) = 1
                    dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.ConsumptionRate) = 1
                    dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.Duration) = "Day(s)"
                    dgGridArticle.Rows(dgGridArticle.Rows.Count - 1)(dgArticle.Remarks) = txtRemarks.Text
                End If
            Next
        Catch ex As Exception
            LogException(ex)
        End Try
    End Sub

    Private Sub BindDataTableFromArticleGrid()
        Try
            If _dtGridPresc.Rows.Count = 0 AndAlso _dtGridPresc Is Nothing Then
                _dtGridPresc = objPC.GetPatientPrescriptionTableStructure(_dtGridPresc)
            End If
            DtPrescDtl.Clear()
            If dgGridArticle.Rows.Count > 0 Then
                For index = 1 To dgGridArticle.Rows.Count - 1
                    Dim dtRow As Int32 = -1
                    Dim result As DataRow() = DtPrescDtl.Select("Articlecode='" + dgGridArticle.Rows(index)("ArticleCode").ToString() + "' and SrNo='" + dgGridArticle.Rows(index)("SrNo").ToString() + "'")
                    If result.Length > 0 Then
                    Else
                        DtPrescDtl.Rows.Add()
                        dtRow = DtPrescDtl.Rows.Count - 1
                        DtPrescDtl.Rows(dtRow)("SrNo") = dgGridArticle.Rows(index)(dgArticle.SrNo)
                        DtPrescDtl.Rows(dtRow)("ArticleCode") = dgGridArticle.Rows(index)(dgArticle.ArticleCode)
                        DtPrescDtl.Rows(dtRow)("ArticleDescription") = dgGridArticle.Rows(index)(dgArticle.ArticleDescription)
                        DtPrescDtl.Rows(dtRow)("EAN") = ""
                        DtPrescDtl.Rows(dtRow)("Qty") = dgGridArticle.Rows(index)(dgArticle.Qty)
                        DtPrescDtl.Rows(dtRow)("ConsumptionRate") = dgGridArticle.Rows(index)(dgArticle.ConsumptionRate)
                        DtPrescDtl.Rows(dtRow)("Duration") = dgGridArticle.Rows(index)("Duration").ToString()
                        DtPrescDtl.Rows(dtRow)("Remarks") = dgGridArticle.Rows(index)(dgArticle.Remarks)
                        DtPrescDtl.Rows(dtRow)("ConsultantsNoteId") = ConsultantsNoteId
                        DtPrescDtl.Rows(dtRow)("PatientId") = PatientId
                        DtPrescDtl.Rows(dtRow)("SiteCode") = clsAdmin.SiteCode
                        DtPrescDtl.Rows(dtRow)("NoteSrNo") = NoteSrNo
                        DtPrescDtl.Rows(dtRow)("CREATEDAT") = vSiteCode
                        DtPrescDtl.Rows(dtRow)("CREATEDBY") = vUserName
                        DtPrescDtl.Rows(dtRow)("CREATEDON") = clsAdmin.CurrentDate
                        DtPrescDtl.Rows(dtRow)("UPDATEDAT") = vSiteCode
                        DtPrescDtl.Rows(dtRow)("UPDATEDBY") = vUserName
                        DtPrescDtl.Rows(dtRow)("UPDATEDON") = clsAdmin.CurrentDate
                        DtPrescDtl.Rows(dtRow)("STATUS") = True
                    End If
                Next index

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub clearform()
        txtRemarks.Text = ""
        txtAndroidArticleSearchTextBox.ReadOnly = False
        txtAndroidArticleSearchTextBox.Text = ""
        dgGridArticle.Rows.RemoveRange(1, dgGridArticle.Rows.Count - 1)
    End Sub
    Private Sub getBinding()
        'Try
        '    If DtPrescDtl.Rows.Count > 0 Then
        '        DtPrescriptionCopy = DtPrescDtl.Copy()
        '        ' Call GridSettings()
        '        GetDuration()
        '        btnAddRemark_Click(Nothing, Nothing)
        '        'GetDuration()
        '        Exit Sub
        '    End If
        '    Dim flp As New FlowLayoutPanel
        '    dgGridArticle.Rows.Count = 1
        '    GetDuration()
        '    ' Call GridSettings()
        '    dgGridArticle.AllowEditing = True
        '    dgGridArticle.Cols(dgArticle.Remarks).AllowEditing = False
        'Catch ex As Exception
        '    ShowMessage(getValueByKey("CM005"), "CM005 - " & getValueByKey("CLAE05"))
        '    LogException(ex)
        'End Try
    End Sub

    Private Sub GridSettings()
        Try
            'dgGridArticle.Rows.MinSize = 28
            'dgGridArticle.Cols(dgArticle.Delete).Width = 50
            'dgGridArticle.Cols(dgArticle.Delete).ComboList = " "
            'dgGridArticle.Cols(dgArticle.Delete).ComboList = "..."
            'dgGridArticle.Cols(dgArticle.Delete).AllowEditing = False


            'dgGridArticle.Cols(dgArticle.SrNo).Width = 50
            'dgGridArticle.Cols(dgArticle.SrNo).Caption = "Sr.No."
            'dgGridArticle.Cols(dgArticle.SrNo).AllowEditing = False

            'dgGridArticle.Cols(dgArticle.ArticleCode).Width = 100
            'dgGridArticle.Cols(dgArticle.ArticleCode).Caption = "Code"
            'dgGridArticle.Cols(dgArticle.ArticleCode).AllowEditing = False

            'dgGridArticle.Cols(dgArticle.ArticleDescription).Width = 150
            'dgGridArticle.Cols(dgArticle.ArticleDescription).Caption = "Article Description"
            'dgGridArticle.Cols(dgArticle.ArticleDescription).AllowEditing = False

            'dgGridArticle.Cols(dgArticle.ConsumptionRate).Width = 110
            'dgGridArticle.Cols(dgArticle.ConsumptionRate).Caption = "Consumption Rate."

            'dgGridArticle.Cols(dgArticle.Qty).Width = 90
            'dgGridArticle.Cols(dgArticle.Qty).Caption = "Qty."
            'dgGridArticle.Cols(dgArticle.Qty).DataType = Type.GetType("System.Decimal")
            'dgGridArticle.Cols(dgArticle.Qty).Format = "0.000"
            'dgGridArticle.Cols(dgArticle.Qty).AllowEditing = True

            ''dgGridArticle.Cols(dgArticle.Duration).Width = 70
            ''dgGridArticle.Cols(dgArticle.Duration).Caption = "Duration"
            ''dgGridArticle.Cols(dgArticle.Duration).AllowEditing = True

            'dgGridArticle.Cols(dgArticle.Remarks).Width = 60
            'dgGridArticle.Cols(dgArticle.Remarks).Caption = "Remarks"
            'dgGridArticle.Cols(dgArticle.Remarks).AllowEditing = False
        Catch ex As Exception
            ShowMessage(getValueByKey("CM006"), "CM006 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub
    Public Sub GetDuration()
        Dim cboDuration As New ComboBox
        cboDuration.DropDownStyle = ComboBoxStyle.DropDownList
        'cboDuration.Items.Insert(0, "")
        cboDuration.Items.Insert(0, "Day(s)")
        cboDuration.Items.Insert(1, "Week(s)")
        cboDuration.Items.Insert(2, "Month(s)")
        dgGridArticle.Cols("Duration").Editor = cboDuration
    End Sub
    Private Function DeleteArticle(srNo As Integer, ByVal articleCode As String) As Boolean
        Try
            If DtPrescDtl.Rows.Count > 0 Then
                Dim drDtl() = DtPrescDtl.Select("SrNo=" & srNo & " and articlecode='" & articleCode & "'")
                If drDtl.Count > 0 Then
                    If Not IsEdit Then
                        For Each row As DataRow In drDtl
                            DtPrescDtl.Rows.Remove(row)
                        Next
                    Else
                        If drDtl(0)("Status") = True Then
                            drDtl(0)("Status") = False
                        Else
                            For Each row As DataRow In drDtl
                                DtPrescDtl.Rows.Remove(row)
                            Next
                        End If
                    End If
                    DtPrescDtl.AcceptChanges()
                End If
                dgGridArticle.Rows.Remove(dgGridArticle.Row)
                If (dgGridArticle.Rows.Count > 1) Then
                    dgGridArticle.Select(dgGridArticle.Rows.Count - 1, 2)

                End If
            Else
                dgGridArticle.Rows.Remove(dgGridArticle.Row)
            End If
            For index = 1 To dgGridArticle.Rows.Count - 1
                dgGridArticle.Rows(index)(dgArticle.SrNo) = index
            Next

        Catch ex As Exception
            LogException(ex)
        End Try

    End Function


    Private Sub dgGridArticle_CellButtonClick(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgGridArticle.CellButtonClick
        Dim ComboSrNo = EditedSrNo
        Dim ArticleCode = dgGridArticle.Item(dgGridArticle.Row, "ArticleCode")
        Dim SrNo = dgGridArticle.Item(dgGridArticle.Row, "SrNo")
        DeleteArticle(SrNo, ArticleCode)
    End Sub

    Private Sub txtRemarks_KeyUp(sender As Object, e As KeyEventArgs) Handles txtRemarks.KeyUp
        If txtRemarks.Text.Trim.Length >= 60 Then
            ShowMessage("60 Characters are allowed in remarks", "Information")
            Dim rmk As String = txtRemarks.Text.Remove(txtRemarks.Text.Length - 1)
            txtRemarks.Text = rmk.Trim
        End If
    End Sub

    Private Sub dgGridArticle_AfterEdit(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles dgGridArticle.AfterEdit
        Try
            Dim CurrentCell As Integer = e.Col
            Dim CurrentRow As Integer = dgGridArticle.Row '-- e.Row
            'DtPrescDtl
            'Dim result As DataRow() = DtSoBulkComboDtl.Select("ArticleCode='" + DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.ArticleCode) + "' and ComboSrNo='" + EditedSrNo.ToString() + "' ")
            Dim strConsumption As String = dgGridArticle.Rows(dgGridArticle.Row)(dgArticle.ConsumptionRate)
            If strConsumption.Length > 30 Then
                ShowMessage("30 Characters are allowed in Consumption rate", "Information")
                dgGridArticle.Rows(CurrentRow)(dgArticle.ConsumptionRate) = ""
                Exit Sub
            End If
            If DtPrescDtl.Rows.Count > 0 Then
                EditedSrNo = Val(dgGridArticle.Rows(dgGridArticle.Row)(dgArticle.SrNo).ToString())
                'Dim result As DataRow() = DtSoBulkComboDtl.Select("ArticleCode='" + DgBulkComboGrid.Rows(DgBulkComboGrid.Row)(DgBulkOrder.ArticleCode) + "' and ComboSrNo='" + EditedSrNo.ToString() + "' ")
                Dim result As DataRow() = DtPrescDtl.Select("ArticleCode='" + dgGridArticle.Rows(dgGridArticle.Row)(dgArticle.ArticleCode).ToString() + "' and SrNo='" + EditedSrNo.ToString() + "' ")
                If result.Count > 0 Then
                    If (e.Col = dgArticle.Qty) Then
                        result(0)(dgArticle.Qty) = dgGridArticle.Rows(dgGridArticle.Row)(dgArticle.Qty)
                    End If
                    If (e.Col = dgArticle.ConsumptionRate) Then
                        result(0)(dgArticle.ConsumptionRate) = dgGridArticle.Rows(dgGridArticle.Row)(dgArticle.ConsumptionRate)
                    End If
                    If (e.Col = dgArticle.Duration) Then
                        result(0)(dgArticle.Duration) = dgGridArticle.Rows(dgGridArticle.Row)(dgArticle.Duration)
                    End If
                    If (e.Col = dgArticle.Remarks) Then
                        result(0)(dgArticle.Remarks) = dgGridArticle.Rows(dgGridArticle.Row)(dgArticle.Remarks)
                    End If
                    DtPrescDtl.AcceptChanges()
                End If
            End If


        Catch ex As Exception
            ShowMessage(getValueByKey("CM006"), "CM006 - " & getValueByKey("CLAE05"))
            LogException(ex)
        End Try
    End Sub


End Class